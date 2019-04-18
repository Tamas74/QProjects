Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.Win32

Namespace gloStream
    Namespace gloDMS

        Namespace Supporting
            'Supporting Function & Methods & Constants
            Public Class Supporting
                '---Functions---

                'Check Valid DMS Path
                Public Function IsDMSSystem(ByVal Path As String) As Boolean
                    Dim oFolderPaths As New gloStream.gloDMS.Supporting.PathsAndFolders
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


                'Check Valid DMS Path
                Public Function CreateDMSSystem(ByVal Path As String) As Boolean
                    Dim oFolderPaths As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _RootPath As String
                    Dim _SystemFolder As String
                    Dim _ContainerFolder As String
                    Dim _RecycleFolder As String
                    Dim _ScanFolder As String
                    Dim _Result As Boolean
                    Try
                        _Result = False
                        'DMS Root Path
                        _RootPath = oFolderPaths.GetDMSPath(Path)
                        If Directory.Exists(_RootPath) = False Then Directory.CreateDirectory(_RootPath)
                        'Check For System Folder
                        _SystemFolder = _RootPath & oFolderPaths.SystemFolder
                        If Directory.Exists(_SystemFolder) = False Then Directory.CreateDirectory(_SystemFolder)
                        'Check For Container Folder
                        _ContainerFolder = _SystemFolder & "\" & oFolderPaths.ContainerFolder
                        If Directory.Exists(_ContainerFolder) = False Then Directory.CreateDirectory(_ContainerFolder)
                        'Check For Recycle Folder
                        _RecycleFolder = _SystemFolder & "\" & oFolderPaths.RecycleFolder
                        If Directory.Exists(_RecycleFolder) = False Then Directory.CreateDirectory(_RecycleFolder)
                        'Check For Scan Folder
                        _ScanFolder = _SystemFolder & "\" & oFolderPaths.ScanFolder
                        If Directory.Exists(_ScanFolder) = False Then Directory.CreateDirectory(_ScanFolder)
                        'Set Tru
                        _Result = True
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


                Public Function IsCategoryExistInExplorer(ByVal RootPath As String, ByVal oCategory As String) As Boolean
                    Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Try
                        IsCategoryExistInExplorer = False
                        If Not oCategory Is Nothing Then
                            Dim _TempPath As String = oSupport.GetDMSPath(RootPath) & oSupport.SystemFolder & "\" & oSupport.ContainerFolder & "\" & oCategory
                            If Directory.Exists(_TempPath) = True Then
                                Return True
                            End If
                        End If
                    Catch objError As Exception
                        Exit Function
                    Finally
                        oSupport = Nothing
                    End Try
                End Function

                '---Class New & Finalise---
                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'DMS Paths
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

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class


        End Namespace

        Namespace DocumentCategory
            'Enum Values
            Public Enum enumCategoryType
                All = 0
                Deleted = 1
                NotDeleted = 2
            End Enum

            'Category Functions
            Public Class DocumentCategory
                Private _ErrorMessage As String

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                'Add Category
                Public Function Add(ByVal oCategory As Category) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _Result As Boolean = False
                    Dim _RecordAdd As Boolean = False

                    Dim _Category As String = ""
                    Dim _CategoryPath As String = ""
                    Dim _CategoryRecycledPath As String = ""

                    Dim _ContainerPath As String = ""
                    Dim _RecycledPath As String = ""

                    Dim i As Integer = 0

                    Try
                        If Not oCategory Is Nothing Then
                            If IsExists(oCategory, False) = False Then
                                '1. Save category database
                                oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
                                'Category Name
                                oDB.DBParameters.Add("@CategoryName", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                                'Modify Name
                                oDB.DBParameters.Add("@ModifyName", "", ParameterDirection.Input, SqlDbType.VarChar)
                                'Excute Query
                                _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_SaveCategory")
                                oDB.Disconnect()
                            Else
                                _Result = True
                            End If


                            '2. Make Category in Explorer
                            If IsExists(oCategory, True) = False Then
                                'Paths
                                _Category = oCategory.Name
                                _ContainerPath = oPathFolder.GetContainerPath(DMSRootPath)
                                _RecycledPath = oPathFolder.GetRecyclePath(DMSRootPath)

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
                            Else
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

                'Modify Category
                Public Function Modify(ByVal oOldCategory As Category, ByVal oModifyCategory As Category) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _Result As Boolean = False
                    Dim _RecordAdd As Boolean = False

                    Dim _Category As String = ""
                    Dim _CategoryPath As String = ""
                    Dim _CategoryRecycledPath As String = ""

                    Dim _ContainerPath As String = ""
                    Dim _RecycledPath As String = ""

                    Dim i As Integer = 0

                    Try
                        If Not oModifyCategory Is Nothing Then
                            '1. Save category database
                            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                            'Category Name
                            oDB.DBParameters.Add("@CategoryName", oOldCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                            'Modify Name
                            oDB.DBParameters.Add("@ModifyName", oModifyCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                            'Excute Query
                            _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_SaveCategory")
                            oDB.Disconnect()

                            '2. Make Category in Explorer
                            If _RecordAdd = True Then
                                'Paths
                                _Category = oModifyCategory.Name
                                _ContainerPath = oPathFolder.GetContainerPath(DMSRootPath)
                                _RecycledPath = oPathFolder.GetRecyclePath(DMSRootPath)

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
                                Rename(_ContainerPath & "\" & oOldCategory.Name, _ContainerPath & "\" & oModifyCategory.Name)
                                Rename(_RecycledPath & "\" & oOldCategory.Name, _RecycledPath & "\" & oModifyCategory.Name)

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

                'Delete Category
                Public Function Delete(ByVal oCategory As Category) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _Result As Boolean = False
                    Dim _RecordAdd As Boolean = False

                    Dim _Category As String = ""
                    Dim _CategoryPath As String = ""
                    Dim _CategoryRecycledPath As String = ""

                    Dim _ContainerPath As String = ""
                    Dim _RecycledPath As String = ""

                    Dim i As Integer = 0

                    Try
                        If Not oCategory Is Nothing Then
                            '1. Delete category database
                            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                            'Category Name
                            oDB.DBParameters.Add("@CategoryName", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                            'Excute Query
                            _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_DeleteCategory")
                            oDB.Disconnect()

                            '2. Shift Category from Continer to Recycled Bin Explorer
                            If _RecordAdd = True Then
                                'Paths
                                _Category = oCategory.Name
                                _ContainerPath = oPathFolder.GetContainerPath(DMSRootPath)
                                _RecycledPath = oPathFolder.GetRecyclePath(DMSRootPath)

                                'Category Folder Path
                                _CategoryPath = _ContainerPath & "\" & _Category

                                'Recycle Folder Path
                                i = 0
                                _CategoryRecycledPath = _RecycledPath & "\" & _Category
                                If Directory.Exists(_CategoryRecycledPath) = True Then
                                    i = i + 1
                                    _CategoryRecycledPath = _RecycledPath & "\" & _Category & i
                                    While Directory.Exists(_CategoryRecycledPath) = True
                                        i = i + 1
                                        _CategoryRecycledPath = _RecycledPath & "\" & _Category & i
                                    End While

                                    Rename(_RecycledPath & "\" & _Category, _CategoryRecycledPath)
                                End If


                                'Shift From Container
                                If Directory.Exists(_CategoryPath) = True Then
                                    Directory.Move(_CategoryPath, _RecycledPath & "\" & _Category)
                                End If

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

                'Check Category Exists
                Public Function IsExists(ByVal oCategory As Category, Optional ByVal CheckInExplorer As Boolean = True) As Boolean
                    Dim _Result As Boolean = False
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader
                    Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _CategoryPath As String

                    Try
                        'Check Category exists in database or not
                        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        'Document ID
                        oDB.DBParameters.Add("@Category", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)

                        oDataReader = oDB.ReadRecords("gsp_DMS_IsCategoryExists")
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Trim(oDataReader.Item(1) & "") = oCategory.Name Then
                                    _Result = True
                                    Exit While
                                End If
                            End While
                        End If
                        oDB.Disconnect()

                        If CheckInExplorer = True Then
                            'Check category exists in explorer or not
                            If oCategory.Name.Trim <> "" Then
                                _CategoryPath = oPathFolder.GetContainerPath(DMSRootPath) & "\" & oCategory.Name
                                If Directory.Exists(_CategoryPath) = True Then
                                    _Result = True
                                Else
                                    _Result = False
                                End If
                            End If
                        End If

                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        oDataReader = Nothing
                        oPathFolder = Nothing
                        _CategoryPath = Nothing
                    End Try
                End Function

                'Check Category Exists
                Public Function IsModify(ByVal oCategory As String) As Boolean
                    Dim _Result As Boolean = True
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader
                    Dim _strSQL As String = ""

                    Try
                        'Check Category exists in database or not
                        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        _strSQL = "SELECT COUNT(DocumentID) FROM DMS_MST WHERE Category = '" & oCategory & "'"
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If CLng(Trim(oDataReader.Item(0) & "")) > 0 Then
                                    _Result = False
                                    Exit While
                                End If
                            End While
                        End If
                        oDB.Disconnect()
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        oDataReader = Nothing
                    End Try
                End Function

                'Check Category Delete or not
                Public Function IsDelete(ByVal oCategory As Category) As Boolean
                    Dim _Result As Boolean = True
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _CategoryPath As String
                    Dim _Records As Integer = 0

                    Try
                        'Check Category exists in database or not
                        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        'Document ID
                        oDB.DBParameters.Add("@Category", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)

                        _Records = CInt(oDB.ExecuteScaler("gsp_DMS_IsCategoryDelete"))
                        If _Records > 0 Then
                            _Result = False
                        End If
                        oDB.Disconnect()

                        'Check category exists in explorer or not
                        _CategoryPath = oPathFolder.GetContainerPath(DMSRootPath) & "\" & oCategory.Name
                        If Directory.Exists(_CategoryPath) = True Then
                            Dim oFolder As DirectoryInfo = New DirectoryInfo(_CategoryPath)
                            Dim oFolders As DirectoryInfo() = oFolder.GetDirectories()

                            If oFolders.Length > 0 Then
                                _Result = False
                            End If
                        End If

                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        oPathFolder = Nothing
                        _CategoryPath = Nothing
                    End Try
                End Function

                'Category List
                Public Function Categories(ByVal CategoryType As enumCategoryType) As Categories
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oCategory As Category
                    Dim oCategories As New Categories
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim oDocument As New gloStream.gloDMS.Supporting.Supporting

                    Try

                        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        oDB.DBParameters.Add("@GetStatus", CategoryType, ParameterDirection.Input, SqlDbType.Int)
                        oDataReader = oDB.ReadRecords("gsp_DMS_FillCategories")

                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                oCategory = New Category
                                oCategory.Name = Trim(oDataReader.Item(1) & "")
                                oCategory.IsDeleted = oDataReader.Item(2)

                                If oDocument.IsCategoryExistInExplorer(DMSRootPath, Trim(oDataReader.Item(1) & "")) = True Then
                                    oCategories.Add(oCategory)
                                End If

                                oCategory = Nothing
                            End While
                        End If

                        oDB.Disconnect()
                        Return oCategories
                    Catch objError As Exception
                        If oDB.ErrorMessage <> "" Then
                            _ErrorMessage = oDB.ErrorMessage
                        Else
                            _ErrorMessage = objError.Message
                        End If
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        oCategory = Nothing
                        oCategories = Nothing
                        oDataReader = Nothing
                    End Try
                End Function

                'Category List
                Public Function Categories(ByVal CategoryType As enumCategoryType, ByVal PatientID As Long) As Categories
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oCategory As Category
                    Dim oCategories As New Categories
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _strSQL As String = ""
                    Dim oDocument As New gloStream.gloDMS.Supporting.Supporting

                    _strSQL = "SELECT DISTINCT DMS_Category_MST.CategoryId, DMS_Category_MST.CategoryName, DMS_Category_MST.IsDeleted " _
                    & " FROM DMS_MST INNER JOIN DMS_Category_MST ON DMS_MST.Category = DMS_Category_MST.CategoryName " _
                    & " WHERE (DMS_MST.PatientID = " & PatientID & ") AND (DMS_Category_MST.CategoryName IS NOT NULL) AND (DMS_Category_MST.IsDeleted = 0) " _
                    & " ORDER BY DMS_Category_MST.CategoryName"

                    Try

                        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        'oDB.DBParameters.Add("@GetStatus", CategoryType, ParameterDirection.Input, SqlDbType.Int)
                        'oDataReader = oDB.ReadRecords("gsp_DMS_FillCategories")
                        oDataReader = oDB.ReadQueryRecords(_strSQL)

                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                oCategory = New Category
                                oCategory.Name = Trim(oDataReader.Item(1) & "")
                                oCategory.IsDeleted = oDataReader.Item(2)

                                If oDocument.IsCategoryExistInExplorer(DMSRootPath, Trim(oDataReader.Item(1) & "")) = True Then
                                    oCategories.Add(oCategory)
                                End If

                                oCategory = Nothing
                            End While
                        End If

                        oDB.Disconnect()
                        Return oCategories
                    Catch objError As Exception
                        If oDB.ErrorMessage <> "" Then
                            _ErrorMessage = oDB.ErrorMessage
                        Else
                            _ErrorMessage = objError.Message
                        End If
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        oCategory = Nothing
                        oCategories = Nothing
                        oDataReader = Nothing
                    End Try
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Category
            Public Class Category
                Private _Name As String
                Private _IsDeleted As Boolean

                Public Property Name() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property IsDeleted() As Boolean
                    Get
                        Return _IsDeleted
                    End Get
                    Set(ByVal Value As Boolean)
                        _IsDeleted = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Category Collection
            Public Class Categories
                Implements System.Collections.IEnumerable
                Private mCol As Collection

                Public Function Add(ByRef oCategory As Category) As Category
                    'create a new object
                    Dim objNewMember As Category
                    objNewMember = New Category

                    'set the properties passed into the method
                    objNewMember.Name = oCategory.Name
                    objNewMember.IsDeleted = oCategory.IsDeleted
                    'objNewMember.Path = oCategorisedDocument.Path

                    'If Len(sKey) = 0 Then
                    mCol.Add(objNewMember)
                    'Else
                    '    mCol.Add objNewMember, sKey
                    'End If


                    'return the object created
                    Add = objNewMember
                    'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Category
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

        Namespace gloEMRAutoUpdate
            Public Class EMRUpdateOption
#Region "constructor"


                Public Sub New()
                    _IntervalList = New System.Collections.ArrayList()
                    _Updated = New System.Collections.ArrayList()
                    _IntervalList.Add("10 minutes")
                    _IntervalList.Add("15 minutes")
                    _IntervalList.Add("30 minutes")
                    _IntervalList.Add("1 hour")
                    _IntervalList.Add("2 hours")
                    _IntervalList.Add("3 hours")
                    _IntervalList.Add("4 hours")
                    _IntervalList.Add("5 hours")
                    _IntervalList.Add("6 hours")
                    _IntervalList.Add("7 hours")
                    _IntervalList.Add("8 hours")
                    _IntervalList.Add("9 hours")
                    _IntervalList.Add("10 hours")
                    _IntervalList.Add("11 hours")
                End Sub
#End Region

#Region "private variables"


                Private _sourcepath As String = ""
                Private _sourceupdatedpath As String = ""
                Private _destinationpath As String = ""
                Private _databasename As String = ""
                Private _sqlservername As String = ""
                Private _updateinterval As String = ""
                Private _exitafterupdate As Boolean = False
                Private _IntervalList As System.Collections.ArrayList
                Private _Updated As System.Collections.ArrayList

                'sarika
                Private _HL7RootPath As String = ""
#End Region

#Region "properties"


                Public Property SourcePath() As String
                    Get
                        Return _sourcepath
                    End Get
                    Set(ByVal value As String)
                        _sourcepath = value
                    End Set
                End Property
                Public ReadOnly Property SourceUpdatedPath() As String

                    Get
                        Return _sourcepath + "\UpdatedEMRs"
                    End Get
                End Property
                Public Property DestinationPath() As String

                    Get
                        Return _destinationpath
                    End Get
                    Set(ByVal value As String)
                        _destinationpath = value
                    End Set
                End Property
                Public Property DatabaseName() As String

                    Get
                        Return _databasename
                    End Get
                    Set(ByVal value As String)
                        _databasename = value
                    End Set
                End Property
                Public Property SQLServerName() As String

                    Get
                        Return _sqlservername
                    End Get
                    Set(ByVal value As String)
                        _sqlservername = value
                    End Set
                End Property
                Public Property UpdateInterval() As String

                    Get
                        Return _updateinterval
                    End Get
                    Set(ByVal value As String)
                        _updateinterval = value
                    End Set
                End Property
                Public Property ExitAfterUpdate() As Boolean

                    Get
                        Return _exitafterupdate
                    End Get
                    Set(ByVal value As Boolean)
                        _exitafterupdate = value
                    End Set
                End Property
                Public ReadOnly Property IntervalList() As System.Collections.ArrayList

                    Get
                        Return _IntervalList
                    End Get
                End Property
                Public Property Updated() As System.Collections.ArrayList

                    Get
                        Return _Updated
                    End Get
                    Set(ByVal value As System.Collections.ArrayList)
                        _Updated = value
                    End Set
                End Property


                'sarika
                Public Property HL7RootPath() As String

                    Get
                        Return _HL7RootPath
                    End Get
                    Set(ByVal value As String)
                        _HL7RootPath = value
                    End Set
                End Property

#End Region

                'Public Function UpdateAutoUpdHL7Settings(ByVal sourcepath As String, ByVal destinationpath As String, ByVal databasename As String, ByVal sqlservername As String, ByVal updateinterval As String, ByVal exitafterupdate As Boolean) As Boolean
                '    Dim objCon As New SqlConnection
                '    objCon.ConnectionString = GetConnectionString()
                '    Dim objCmd As New SqlCommand
                '    'Dim objSQLDataReader As SqlDataReader
                '    objCmd.CommandType = CommandType.StoredProcedure
                '    objCmd.CommandText = "gsp_UpdateSettings"
                '    Dim objParaSettingsName As New SqlParameter
                '    Dim objParaSettingsValue As New SqlParameter
                '    objCmd.Connection = objCon

                '    Try

                '        objCon.Open()
                '        'Clinic Working Time
                '        objCmd.Parameters.Clear()
                '        With objParaSettingsName
                '            .ParameterName = "@SettingsName"
                '            .Value = "AutoUpdate Source"
                '            .Direction = ParameterDirection.Input
                '            .SqlDbType = SqlDbType.VarChar
                '        End With
                '        objCmd.Parameters.Add(objParaSettingsName)

                '        With objParaSettingsValue
                '            .ParameterName = "@SettingsValue"
                '            .Value = tmAppointmentStartTime
                '            .Direction = ParameterDirection.Input
                '            .SqlDbType = SqlDbType.DateTime
                '        End With
                '        objCmd.Parameters.Add(objParaSettingsValue)
                '        objCmd.ExecuteNonQuery()

                '    Catch ex As Exception
                '    Finally
                '        objCon.Close()
                '    End Try
                'End Function

                'Convert Interval to Timer Seconds 
                Public Function ConvertIntervalToInt(ByVal sinterval As String) As Integer
                    Dim _1Minute As Integer = 60000
                    Dim _1Hour As Integer = 3600000
                    Dim _Result As Integer = _1Minute * 10

                    Try
                        If sinterval = "10 minutes" Then
                            _Result = _1Minute * 10
                        ElseIf sinterval = "15 minutes" Then
                            _Result = _1Minute * 15
                        ElseIf sinterval = "30 minutes" Then
                            _Result = _1Minute * 30
                        ElseIf sinterval = "1 hour" Then
                            _Result = _1Hour * 1
                        ElseIf sinterval = "2 hours" Then
                            _Result = _1Hour * 2
                        ElseIf sinterval = "3 hours" Then
                            _Result = _1Hour * 3
                        ElseIf sinterval = "4 hours" Then
                            _Result = _1Hour * 4
                        ElseIf sinterval = "5 hours" Then
                            _Result = _1Hour * 5
                        ElseIf sinterval = "6 hours" Then
                            _Result = _1Hour * 6
                        ElseIf sinterval = "7 hours" Then
                            _Result = _1Hour * 7
                        ElseIf sinterval = "8 hours" Then
                            _Result = _1Hour * 8
                        ElseIf sinterval = "9 hours" Then
                            _Result = _1Hour * 9
                        ElseIf sinterval = "10 hours" Then
                            _Result = _1Hour * 10
                        ElseIf sinterval = "11 hours" Then
                            _Result = _1Hour * 11


                        End If
                    Catch
                        Return 1000
                    End Try

                    Return _Result
                End Function

                Public Function ReadgloEMRUpdateOption() As gloEMRAutoUpdate.EMRUpdateOption
                    Dim _Result As gloEMRAutoUpdate.EMRUpdateOption
                    Dim conn As New SqlConnection
                    Dim cmd As SqlCommand
                    Dim dt As DataTable
                    Dim da As SqlDataAdapter
                    conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

                    Try
                        dt = New DataTable
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "select sSettingsName,sSettingsValue from Settings where sSettingsName in ('AutoUpdate Source','AutoUpdate Interval','Exit AutoUpdate','HL7 System Path')"

                      

                        da = New SqlDataAdapter(cmd)
                        da.Fill(dt)

                        If Not IsNothing(dt) Then
                            If dt.Rows.Count > 0 Then
                                _Result = New gloEMRAutoUpdate.EMRUpdateOption
                                For i As Integer = 0 To dt.Rows.Count - 1

                                    If dt.Rows(i)(0) = "AutoUpdate Source" Then
                                        _Result.SourcePath = dt.Rows(i)(1)
                                    End If
                                    If dt.Rows(i)(0) = "AutoUpdate Interval" Then
                                        _Result.UpdateInterval = dt.Rows(i)(1)
                                    End If
                                    If dt.Rows(i)(0) = "Exit AutoUpdate" Then
                                        If dt.Rows(i)(1) = 0 Then
                                            _Result.ExitAfterUpdate = False
                                        Else
                                            _Result.ExitAfterUpdate = True
                                        End If
                                    End If
                                    If dt.Rows(i)(0) = "HL7 System Path" Then
                                        _Result.HL7RootPath = dt.Rows(i)(1)
                                    End If
                                Next

                                _Result.DestinationPath = ""
                            End If
                        End If
                        'conn.Close()

                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show("Error while reading AutoUpdate settings" & ex.ToString, "gloEMR Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.[Error])
                        Return Nothing
                    End Try
                    Return (_Result)
                End Function

                'Update Option 
                Public Function SavegloEMRUpdateOption(ByVal sourcepath As String, ByVal destinationpath As String, ByVal databasename As String, ByVal sqlservername As String, ByVal updateinterval As String, ByVal exitafterupdate As Boolean, ByVal sHL7SystemPath As String) As Boolean
                    Dim _Result As Boolean = False

                    Dim objCon As New SqlConnection
                    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
                    Dim objCmd As New SqlCommand
                    'Dim objSQLDataReader As SqlDataReader
                    objCmd.CommandType = CommandType.StoredProcedure
                    objCmd.CommandText = "gsp_UpdateSettings"
                    Dim objParaSettingsName As New SqlParameter
                    Dim objParaSettingsValue As New SqlParameter
                    objCmd.Connection = objCon


                  

                    Try

                        objCon.Open()

                        objCmd.Parameters.Clear()
                        With objParaSettingsName
                            .ParameterName = "@SettingsName"
                            .Value = "AutoUpdate Source"
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsName)

                        With objParaSettingsValue
                            .ParameterName = "@SettingsValue"
                            .Value = sourcepath
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsValue)
                        objCmd.ExecuteNonQuery()


                        objCmd.Parameters.Clear()
                        With objParaSettingsName
                            .ParameterName = "@SettingsName"
                            .Value = "AutoUpdate Interval"
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsName)

                        With objParaSettingsValue
                            .ParameterName = "@SettingsValue"
                            .Value = updateinterval
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsValue)
                        objCmd.ExecuteNonQuery()

                        objCmd.Parameters.Clear()
                        With objParaSettingsName
                            .ParameterName = "@SettingsName"
                            .Value = "Exit AutoUpdate"
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsName)

                        With objParaSettingsValue
                            .ParameterName = "@SettingsValue"
                            If exitafterupdate = True Then
                                .Value = 1
                            Else
                                .Value = 0
                            End If

                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.Bit
                        End With
                        objCmd.Parameters.Add(objParaSettingsValue)
                        objCmd.ExecuteNonQuery()

                        objCmd.Parameters.Clear()
                        With objParaSettingsName
                            .ParameterName = "@SettingsName"
                            .Value = "HL7 System Path"
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsName)

                        With objParaSettingsValue
                            .ParameterName = "@SettingsValue"
                            .Value = sHL7SystemPath
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        objCmd.Parameters.Add(objParaSettingsValue)
                        objCmd.ExecuteNonQuery()


                        If System.IO.Directory.Exists(sourcepath) = False Then
                            System.IO.Directory.CreateDirectory(sourcepath)
                        End If

                        'If System.IO.Directory.Exists(sHL7SystemPath) = True Then
                        Dim _RootPath As String = sHL7SystemPath
                        Dim _MessageFolder As String = sHL7SystemPath & "\HL7 Message Box"
                        Dim _InBox As String = _MessageFolder & "\Inbox"
                        Dim _OutBox As String = _MessageFolder & "\Outbox"
                        Dim _Error As String = _MessageFolder & "\Errors"

                        If System.IO.Directory.Exists(_RootPath) = False Then
                            System.IO.Directory.CreateDirectory(_RootPath)
                        End If
                        If System.IO.Directory.Exists(_MessageFolder) = False Then
                            System.IO.Directory.CreateDirectory(_MessageFolder)
                        End If
                        If System.IO.Directory.Exists(_InBox) = False Then
                            System.IO.Directory.CreateDirectory(_InBox)
                        End If
                        If System.IO.Directory.Exists(_OutBox) = False Then
                            System.IO.Directory.CreateDirectory(_OutBox)
                        End If
                        If System.IO.Directory.Exists(_Error) = False Then
                            System.IO.Directory.CreateDirectory(_Error)
                        End If
                        'End If

                        Return True
                    Catch ex As Exception
                        MessageBox.Show("Error while saving AutoUpdate settings", "gloEMR Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.[Error])
                        Return False
                    Finally
                        objCon.Close()
                    End Try
                    Return _Result
                End Function

                Public Function ReadProfile(ByVal ProfileName As String, ByVal ElementName As String, ByVal FilePath As String) As String
                    Dim _result As String = ""
                    Try
                        Dim _line As String = ""
                        Dim _linecount As Int16 = 1


                        If File.Exists(FilePath) Then
                            Dim sr1 As New StreamReader(FilePath)
                            Do
                                _line = sr1.ReadLine()
                                If _line IsNot Nothing Then
                                    If _linecount = 1 Then
                                        If _line <> "[" + ProfileName + "]" Then
                                            Exit Do
                                        End If
                                    Else
                                        If _line.Trim() <> "" Then
                                            Dim _Length As Integer = _line.IndexOf("=")
                                            If _Length <= 0 Then
                                                _Length = 0
                                            End If

                                            If ElementName = _line.Substring(0, _Length) Then
                                                _result = _line.Substring(_line.IndexOf("=") + 1).ToString()
                                                Exit Do
                                            End If
                                        End If

                                    End If
                                End If
                                _linecount += 1
                            Loop While Not (_line Is Nothing)
                            sr1.Close()
                        End If
                    Catch ex As Exception
                        Dim dlg As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(ex.Message, "gloEMR Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.[Error])
                        Return _result
                    End Try

                    Return _result

                End Function

                Public Function WriteProfile(ByVal ProfileName As String, ByVal ElementName As String, ByVal ElementValue As String, ByVal FilePath As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        Dim _writedata As String = ""
                        Dim _line As String = ""
                        Dim _linecount As Int16 = 1

                        If File.Exists(FilePath) Then
                            Dim sr1 As New StreamReader(FilePath)
                            Do
                                _line = sr1.ReadLine()
                                If _line IsNot Nothing Then
                                    If _linecount = 1 Then
                                        If _line <> "[" + ProfileName + "]" Then
                                            Exit Do
                                        Else
                                            _writedata = "[" + ProfileName + "]" + Environment.NewLine
                                        End If
                                    Else
                                        If _line.Trim() <> "" Then
                                            If ElementName IsNot Nothing Then
                                                If ElementName <> _line.Substring(0, _line.IndexOf("=")) Then
                                                    _writedata = _writedata + _line + Environment.NewLine
                                                End If
                                            End If
                                        End If
                                    End If
                                    'if (_linecount == 1) 
                                    _linecount += 1
                                End If
                            Loop While Not (_line Is Nothing)

                            sr1.Close()

                            If _writedata.Trim() <> "" Then
                                _writedata = _writedata + ElementName + "=" + ElementValue

                            End If
                        Else
                            _writedata = "[" + ProfileName + "]" + Environment.NewLine
                            _writedata = _writedata + ElementName + "=" + ElementValue
                        End If

                        If File.Exists(FilePath) Then
                            File.Delete(FilePath)
                        End If

                        Dim sw As New StreamWriter(FilePath)
                        sw.Write(_writedata)
                        sw.Close()

                        sw.Dispose()
                    Catch ex As Exception
                        Dim dlg As System.Windows.Forms.DialogResult = System.Windows.Forms.MessageBox.Show(ex.Message, "gloEMR Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.[Error])
                        Return False
                    End Try
                    Return _Result

                End Function
            End Class

            Class ElementEMRUpdateOption
#Region "constructor"
                Public Sub New()
                End Sub
#End Region

#Region "variables"



                Private _ProfileName As String = "EMR Update Profile"
                Private _SourcePath As String = "Source Path"
                Private _DestinationPath As String = "Destination Path"
                Private _DatabasePath As String = "Database Path"
                Private _SQLServerName As String = "SQL Server Name"
                Private _UpdateInterval As String = "Update Interval"
                Private _ExitAfterUpdate As String = "Exit After Update"
                Private _EmrOptionProfilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString() + "\geau40.log"
                Private _SourceUpdatedPath As String = "Source Updated Path"
                Private _UpdatedName As String = "Updated"
                Private _EmrOptionUpdatedPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString() + "\geau40ed.log"
#End Region

#Region "properties"


                Public ReadOnly Property ProfileName() As String
                    Get
                        Return _ProfileName
                    End Get
                End Property

                Public ReadOnly Property SourcePath() As String
                    Get
                        Return _SourcePath
                    End Get
                End Property

                Public ReadOnly Property DestinationPath() As String
                    Get
                        Return _DestinationPath
                    End Get
                End Property

                Public ReadOnly Property DatabasePath() As String
                    Get
                        Return _DatabasePath
                    End Get
                End Property

                Public ReadOnly Property SQLServerName() As String
                    Get
                        Return _SQLServerName
                    End Get
                End Property

                Public ReadOnly Property UpdateInterval() As String
                    Get
                        Return _UpdateInterval
                    End Get
                End Property

                Public ReadOnly Property ExitAfterUpdate() As String
                    Get
                        Return _ExitAfterUpdate
                    End Get
                End Property

                Public ReadOnly Property EmrOptionProfilePath() As String
                    Get
                        Return _EmrOptionProfilePath
                    End Get
                End Property

                Public ReadOnly Property SourceUpdatedPath() As String
                    Get
                        Return _SourceUpdatedPath
                    End Get
                End Property

                Public ReadOnly Property Updated() As String
                    Get
                        Return _UpdatedName
                    End Get
                End Property

                Public ReadOnly Property EMROptionUpdatedPath() As String
                    Get
                        Return _EmrOptionUpdatedPath
                    End Get
                End Property
#End Region

            End Class

        End Namespace

        Namespace gloSync
            Namespace Subscriber

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

                Public Class Subscriber
                    Private m_ErrorMessage As String = ""
                    Private m_XMLDatabaseFileName As String = Application.StartupPath & "\gloDMSSubscriber.xml"

                    Public ReadOnly Property ErrorMessage() As String
                        Get
                            Return m_ErrorMessage
                        End Get
                    End Property

                    Public Function Add(ByVal SubscriberName As String, ByVal ServerName As String, ByVal DatabaseName As String, ByVal DMSPath As String, ByVal ConnectionAuthontication As enmConnectionAuthentication, Optional ByVal ConnectionUserName As String = "", Optional ByVal ConnectionPassword As String = "", Optional ByVal IsSelect As Boolean = False, Optional ByVal IsSubscriber As Boolean = True) As Boolean
                        m_ErrorMessage = ""
                        Try
                            Dim dsSubscribers As New DataSet
                            dsSubscribers.ReadXml(m_XMLDatabaseFileName)

                            Dim nCount As Int16
                            If IsSubscriber = False Then
                                For nCount = dsSubscribers.Tables(0).Rows.Count - 1 To 0 Step -1
                                    If dsSubscribers.Tables(0).Rows(nCount).Item(8) = False Then
                                        dsSubscribers.Tables(0).Rows(nCount).Delete()
                                    End If
                                Next
                            End If


                            Dim drSubscriber As DataRow
                            drSubscriber = dsSubscribers.Tables(0).NewRow
                            drSubscriber(0) = SubscriberName
                            drSubscriber(1) = ServerName
                            drSubscriber(2) = DatabaseName
                            drSubscriber(3) = DMSPath
                            Select Case ConnectionAuthontication
                                Case enmConnectionAuthentication.WindowsAuthentication
                                    drSubscriber(4) = "Windows"
                                Case enmConnectionAuthentication.SQLAuthentication
                                    drSubscriber(4) = "SQL"
                            End Select
                            drSubscriber(5) = ConnectionUserName
                            drSubscriber(6) = ConnectionPassword
                            If IsSubscriber = False Then
                                drSubscriber(7) = True
                            Else
                                drSubscriber(7) = IsSelect
                            End If
                            drSubscriber(8) = IsSubscriber
                            dsSubscribers.Tables(0).Rows.Add(drSubscriber)
                            dsSubscribers.WriteXml(m_XMLDatabaseFileName)
                            Return True
                        Catch ex As Exception
                            m_ErrorMessage = ex.Message
                            Return False
                        End Try
                    End Function

                    Public Function Modify(ByVal SubscriberToModifyName As String, ByVal SubscriberName As String, ByVal ServerName As String, ByVal DatabaseName As String, ByVal DMSPath As String, ByVal ConnectionAuthontication As enmConnectionAuthentication, Optional ByVal ConnectionUserName As String = "", Optional ByVal ConnectionPassword As String = "", Optional ByVal IsSelect As Boolean = False, Optional ByVal IsSubscriber As Boolean = True) As Boolean
                        m_ErrorMessage = ""
                        Try
                            Dim dsSubscribers As New DataSet
                            dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                            Dim nCount As Int16
                            Dim objSubscribers As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail

                            For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                                If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberToModifyName).ToLower Then
                                    dsSubscribers.Tables(0).Rows(nCount).Item(0) = SubscriberName
                                    dsSubscribers.Tables(0).Rows(nCount).Item(1) = ServerName
                                    dsSubscribers.Tables(0).Rows(nCount).Item(2) = DatabaseName
                                    dsSubscribers.Tables(0).Rows(nCount).Item(3) = DMSPath
                                    Select Case ConnectionAuthontication
                                        Case enmConnectionAuthentication.WindowsAuthentication
                                            dsSubscribers.Tables(0).Rows(nCount).Item(4) = "Windows"
                                        Case enmConnectionAuthentication.SQLAuthentication
                                            dsSubscribers.Tables(0).Rows(nCount).Item(4) = "SQL"
                                    End Select
                                    dsSubscribers.Tables(0).Rows(nCount).Item(5) = ConnectionUserName
                                    dsSubscribers.Tables(0).Rows(nCount).Item(6) = ConnectionPassword
                                    dsSubscribers.Tables(0).Rows(nCount).Item(7) = IsSelect
                                    dsSubscribers.Tables(0).Rows(nCount).Item(8) = IsSubscriber
                                    Exit For
                                End If
                            Next
                            dsSubscribers.WriteXml(m_XMLDatabaseFileName)
                            Return True
                        Catch ex As Exception
                            m_ErrorMessage = ex.Message
                            Return False
                        End Try
                    End Function

                    Public Function Delete(ByVal SubscriberName As String) As Boolean
                        m_ErrorMessage = ""
                        Try
                            Dim dsSubscribers As New DataSet
                            dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                            Dim nCount As Int16
                            For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                                If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberName).ToLower Then
                                    dsSubscribers.Tables(0).Rows(nCount).Delete()
                                    Exit For
                                End If
                            Next
                            dsSubscribers.WriteXml(m_XMLDatabaseFileName)
                            Return True

                        Catch ex As Exception
                            m_ErrorMessage = ex.Message
                            Return False
                        End Try
                    End Function

                    Public Function IsExists(ByVal SubscriberName As String, ByVal ServerName As String, ByVal DatabaseName As String, ByVal DMSPath As String, Optional ByVal SubscriberOriginalName As String = "") As Boolean
                        m_ErrorMessage = ""
                        Try
                            Dim dsSubscribers As New DataSet
                            dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                            Dim nCount As Int16
                            For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                                If SubscriberOriginalName = "" Then
                                    'Check for Add
                                    'Name
                                    If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberName).ToLower Then
                                        m_ErrorMessage = SubscriberName & " already exists"
                                        Return True
                                    End If
                                    'Server Name & Database Name
                                    If (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(1)).ToLower = Trim(ServerName).ToLower) AndAlso (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(2)).ToLower = Trim(DatabaseName).ToLower) Then
                                        m_ErrorMessage = "SQL Server " & ServerName & " and database " & DatabaseName & " already configured as subscriber."
                                        Return True
                                    End If
                                    'Server Name & DMS Path
                                    If (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(1)).ToLower = Trim(ServerName).ToLower) AndAlso (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(3)).ToLower = Trim(DMSPath).ToLower) Then
                                        m_ErrorMessage = "DMS Path " & DMSPath & " already configured as subscriber."
                                        Return True
                                    End If
                                Else
                                    'Check for Updates
                                    If SubscriberName <> SubscriberOriginalName Then
                                        If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberName).ToLower Then
                                            m_ErrorMessage = SubscriberName & " already exists"
                                            Return True
                                        End If
                                    End If
                                    If SubscriberOriginalName <> dsSubscribers.Tables(0).Rows(nCount).Item(0) Then
                                        'Server Name & Database Name
                                        If (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(1)).ToLower = Trim(ServerName).ToLower) AndAlso (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(2)).ToLower = Trim(DatabaseName).ToLower) Then
                                            m_ErrorMessage = "SQL Server " & ServerName & " and database " & DatabaseName & " already configured as subscriber."
                                            Return True
                                        End If
                                        'Server Name & DMS Path
                                        If (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(1)).ToLower = Trim(ServerName).ToLower) AndAlso (Trim(dsSubscribers.Tables(0).Rows(nCount).Item(3)).ToLower = Trim(DMSPath).ToLower) Then
                                            m_ErrorMessage = "DMS Path " & DMSPath & " already configured as subscriber."
                                            Return True
                                        End If
                                    End If
                                End If
                            Next
                            Return False
                        Catch ex As Exception
                            m_ErrorMessage = ex.Message
                            Return False
                        End Try
                    End Function

                    Public Function SetSelected(ByVal SubscriberName As String, ByVal Selected As Boolean) As Boolean
                        m_ErrorMessage = ""
                        Try
                            Dim dsSubscribers As New DataSet
                            dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                            Dim nCount As Int16
                            Dim objSubscribers As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                            For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                                If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberName).ToLower Then
                                    dsSubscribers.Tables(0).Rows(nCount).Item(7) = Selected
                                    Exit For
                                End If
                            Next
                            dsSubscribers.WriteXml(m_XMLDatabaseFileName)
                            Return True
                        Catch ex As Exception
                            m_ErrorMessage = ex.Message
                            Return False
                        End Try
                    End Function

                    Public Function IsDelete(ByVal SubscriberName As String) As Boolean
                        '//////
                    End Function

                    Public Function Subscribers() As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
                        Dim oSubscriberDetails As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
                        Dim oSubscriberDetail As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                        Dim dsSubscribers As New DataSet
                        dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                        Dim nCount As Int16
                        For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                            If dsSubscribers.Tables(0).Rows(nCount).Item(8) = True Then
                                oSubscriberDetail = New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                                oSubscriberDetail.Name = dsSubscribers.Tables(0).Rows(nCount).Item(0)
                                oSubscriberDetail.ServerName = dsSubscribers.Tables(0).Rows(nCount).Item(1)
                                oSubscriberDetail.DatabaseName = dsSubscribers.Tables(0).Rows(nCount).Item(2)
                                oSubscriberDetail.DMSPath = dsSubscribers.Tables(0).Rows(nCount).Item(3)
                                Select Case dsSubscribers.Tables(0).Rows(nCount).Item(4).ToString()
                                    Case "Windows"
                                        oSubscriberDetail.ConnectionAuthontication = enmConnectionAuthentication.WindowsAuthentication
                                    Case "SQL"
                                        oSubscriberDetail.ConnectionAuthontication = enmConnectionAuthentication.SQLAuthentication
                                End Select
                                oSubscriberDetail.ConnectionLoginName = dsSubscribers.Tables(0).Rows(nCount).Item(5)
                                oSubscriberDetail.ConnectionPassword = dsSubscribers.Tables(0).Rows(nCount).Item(6)
                                oSubscriberDetail.IsSelect = dsSubscribers.Tables(0).Rows(nCount).Item(7)
                                oSubscriberDetail.IsSubscriber = dsSubscribers.Tables(0).Rows(nCount).Item(8)
                                oSubscriberDetails.Add(oSubscriberDetail)
                                oSubscriberDetail = Nothing
                            End If
                        Next
                        Return oSubscriberDetails
                    End Function

                    Public Function Publishers() As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
                        Dim oSubscriberDetails As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
                        Dim oSubscriberDetail As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                        Dim dsSubscribers As New DataSet
                        dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                        Dim nCount As Int16
                        For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                            If dsSubscribers.Tables(0).Rows(nCount).Item(8) = False Then
                                oSubscriberDetail = New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                                oSubscriberDetail.Name = dsSubscribers.Tables(0).Rows(nCount).Item(0)
                                oSubscriberDetail.ServerName = dsSubscribers.Tables(0).Rows(nCount).Item(1)
                                oSubscriberDetail.DatabaseName = dsSubscribers.Tables(0).Rows(nCount).Item(2)
                                oSubscriberDetail.DMSPath = dsSubscribers.Tables(0).Rows(nCount).Item(3)
                                Select Case dsSubscribers.Tables(0).Rows(nCount).Item(4).ToString()
                                    Case "Windows"
                                        oSubscriberDetail.ConnectionAuthontication = enmConnectionAuthentication.WindowsAuthentication
                                    Case "SQL"
                                        oSubscriberDetail.ConnectionAuthontication = enmConnectionAuthentication.SQLAuthentication
                                End Select
                                oSubscriberDetail.ConnectionLoginName = dsSubscribers.Tables(0).Rows(nCount).Item(5)
                                oSubscriberDetail.ConnectionPassword = dsSubscribers.Tables(0).Rows(nCount).Item(6)
                                If Not dsSubscribers.Tables(0).Rows(nCount).Item(7) Is Nothing Then
                                    If dsSubscribers.Tables(0).Rows(nCount).Item(7).ToString.Trim() = "True" Or dsSubscribers.Tables(0).Rows(nCount).Item(7).ToString.Trim() = "False" Then
                                        oSubscriberDetail.IsSelect = dsSubscribers.Tables(0).Rows(nCount).Item(7)
                                    Else
                                        oSubscriberDetail.IsSelect = False
                                    End If
                                End If
                                oSubscriberDetail.IsSubscriber = dsSubscribers.Tables(0).Rows(nCount).Item(8)
                                oSubscriberDetails.Add(oSubscriberDetail)
                                oSubscriberDetail = Nothing
                            End If
                        Next
                        Return oSubscriberDetails
                    End Function

                    Public Function GetSubscriber(ByVal SubscriberName As String) As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                        Dim dsSubscribers As New DataSet
                        dsSubscribers.ReadXml(m_XMLDatabaseFileName)
                        Dim nCount As Int16
                        Dim objSubscribers As New gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                        For nCount = 0 To dsSubscribers.Tables(0).Rows.Count - 1
                            If Trim(dsSubscribers.Tables(0).Rows(nCount).Item(0)).ToLower = Trim(SubscriberName).ToLower Then
                                objSubscribers.Name = SubscriberName
                                objSubscribers.ServerName = dsSubscribers.Tables(0).Rows(nCount).Item(1)
                                objSubscribers.DatabaseName = dsSubscribers.Tables(0).Rows(nCount).Item(2)
                                objSubscribers.DMSPath = dsSubscribers.Tables(0).Rows(nCount).Item(3)
                                Select Case dsSubscribers.Tables(0).Rows(nCount).Item(4).ToString()
                                    Case "Windows"
                                        objSubscribers.ConnectionAuthontication = enmConnectionAuthentication.WindowsAuthentication
                                    Case "SQL"
                                        objSubscribers.ConnectionAuthontication = enmConnectionAuthentication.SQLAuthentication
                                End Select
                                objSubscribers.ConnectionLoginName = dsSubscribers.Tables(0).Rows(nCount).Item(5)
                                objSubscribers.ConnectionPassword = dsSubscribers.Tables(0).Rows(nCount).Item(6)
                                objSubscribers.IsSelect = dsSubscribers.Tables(0).Rows(nCount).Item(7)
                                objSubscribers.IsSubscriber = dsSubscribers.Tables(0).Rows(nCount).Item(8)
                                Exit For
                            End If
                        Next
                        Return objSubscribers
                    End Function

                    Public Function GetPublisher(ByVal PublisherName As String) As gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail
                        Return GetSubscriber(PublisherName)
                    End Function

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                Public Class PublisherSubscriberDetail
                    Private _Name As String
                    Private _ServerName As String
                    Private _DatabaseName As String
                    Private _DMSPath As String
                    Private _ConnectionAuthontication As enmConnectionAuthentication
                    Private _ConnectionLoginName As String
                    Private _ConnectionPassword As String
                    Private _IsSelect As Boolean
                    Private _IsSubscriber As Boolean

                    Public Property Name() As String
                        Get
                            Return _Name
                        End Get
                        Set(ByVal Value As String)
                            _Name = Value
                        End Set
                    End Property

                    Public Property ServerName() As String
                        Get
                            Return _ServerName
                        End Get
                        Set(ByVal Value As String)
                            _ServerName = Value
                        End Set
                    End Property

                    Public Property DatabaseName() As String
                        Get
                            Return _DatabaseName
                        End Get
                        Set(ByVal Value As String)
                            _DatabaseName = Value
                        End Set
                    End Property

                    Public Property DMSPath() As String
                        Get
                            Return _DMSPath
                        End Get
                        Set(ByVal Value As String)
                            _DMSPath = Value
                        End Set
                    End Property

                    Public Property ConnectionAuthontication() As enmConnectionAuthentication
                        Get
                            Return _ConnectionAuthontication
                        End Get
                        Set(ByVal Value As enmConnectionAuthentication)
                            _ConnectionAuthontication = Value
                        End Set
                    End Property

                    Public Property ConnectionLoginName() As String
                        Get
                            Return _ConnectionLoginName
                        End Get
                        Set(ByVal Value As String)
                            _ConnectionLoginName = Value
                        End Set
                    End Property

                    Public Property ConnectionPassword() As String
                        Get
                            Return _ConnectionPassword
                        End Get
                        Set(ByVal Value As String)
                            _ConnectionPassword = Value
                        End Set
                    End Property

                    Public Property IsSelect() As Boolean
                        Get
                            Return _IsSelect
                        End Get
                        Set(ByVal Value As Boolean)
                            _IsSelect = Value
                        End Set
                    End Property

                    Public Property IsSubscriber() As Boolean
                        Get
                            Return _IsSubscriber
                        End Get
                        Set(ByVal Value As Boolean)
                            _IsSubscriber = Value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                'Categorised Documents Collection
                Public Class PublisherSubscriberDetails
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection

                    Public Sub Add(ByRef oPublisherSubscriberDetail As PublisherSubscriberDetail)
                        mCol.Add(oPublisherSubscriberDetail)
                    End Sub

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As PublisherSubscriberDetail
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

                            If SynchDataType = gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType.NotInPublisher Then
                                For i As Int16 = 0 To dtPublisherNew.Rows.Count - 1
                                    oCollection.Add(dtPublisherNew.Rows(i)(1))
                                Next
                            ElseIf SynchDataType = gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType.NotInSubscriber Then
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
                                Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Publisher
                                    oDocument = New gloStream.Document(True)
                                Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Subscriber
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
                                Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Publisher
                                    oDocument = New gloStream.Document(True)
                                Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Subscriber
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

    End Namespace
End Namespace