Imports System.IO
Imports gloEMRGeneralLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase


Namespace gloStream

    Namespace gloVMS


        Public Class gloVMS
            Private _ErrorMessage As String
            Dim _DestinationFolder As String = ""


            Public Event ReportPercentage(ByVal nPercentage As Integer)
            Public Event ReportImportDocument(ByVal DocumentPath As String)
            Dim _PatientID As Long


            Public Function UploadVideo_DB(ByVal fSourceFile As System.IO.FileInfo, ByVal oParameters As gloStream.gloVMS.Supporting.ImportProcessParameters, ByVal PatientID As Long) As Boolean
                '' Save the Media to the Database in VMS_MST Table
                '    Dim oDMS As gloStream.gloDMS.Supporting.Supporting
                Dim oMedia As Media.Media
                Try
                    oMedia = New Media.Media
                    '   oDMS = New gloStream.gloDMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oMedia.DocumentID = oParameters.DocumentID
                    oMedia.MediaFileName = oParameters.MediaFileName
                    oMedia.MediaFileDisplayName = oParameters.MediaFileDisplayName
                    oMedia.Extension = oParameters.Extension
                    oMedia.SourceMachine = gstrClientMachineName
                    oMedia.SystemFolder = oParameters.SystemFolder
                    oMedia.Container = oParameters.Container  '"Container"
                    oMedia.Category = oParameters.Category   '"General"
                    oMedia.PatientID = PatientID
                    oMedia.Year = oParameters.Year         'Now.Year.ToString
                    oMedia.Month = oParameters.Month      'oDMS.MonthAsString(Now.Month)
                    oMedia.UploadDateTime = Now
                    oMedia.ModifiedDateTime = fSourceFile.LastWriteTime





                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Add("@DocumentID", oMedia.DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                    oDB.DBParameters.Add("@DocumentFileName", oMedia.MediaFileName, ParameterDirection.Input, SqlDbType.VarChar, 60)

                    oDB.DBParameters.Add("@DocumentDisplayName", oMedia.MediaFileDisplayName, ParameterDirection.Input, SqlDbType.VarChar, 100)

                    oDB.DBParameters.Add("@Extension", oMedia.Extension, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@SourceMachine", oMedia.SourceMachine, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@SystemFolder", oMedia.SystemFolder, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@Container", oMedia.Container, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@Category", oMedia.Category, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@PatientID", oMedia.PatientID, ParameterDirection.Input, SqlDbType.BigInt)

                    oDB.DBParameters.Add("@Year", oMedia.Year, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@Month", oMedia.Month, ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@ArchiveStatus", 0, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.DBParameters.Add("@ArchiveDescription", "", ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@UsedStatus", 0, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.DBParameters.Add("@UsedMachine", "", ParameterDirection.Input, SqlDbType.VarChar)

                    oDB.DBParameters.Add("@UsedType", 0, ParameterDirection.Input, SqlDbType.TinyInt)

                    oDB.DBParameters.Add("@UploadDateTime", oMedia.UploadDateTime, ParameterDirection.Input, SqlDbType.DateTime)

                    oDB.DBParameters.Add("@VersionNo", 0, ParameterDirection.Input, SqlDbType.BigInt)

                    oDB.DBParameters.Add("@ModifyDateTime", oMedia.ModifiedDateTime, ParameterDirection.Input, SqlDbType.DateTime)

                    oDB.DBParameters.Add("@IsReviewed", 0, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.ExecuteNonQuery("gsp_VMS_InsertVMS_Mst")

                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing


                    Return True
                Catch ex As SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateLog(ex.ToString)
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    '    oDMS = Nothing
                    oMedia = Nothing
                End Try

            End Function

            Public Function GetVideoList(ByVal PatientID As Long) As DataTable
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim strSelectQry As String  ' = "SELECT * FROM VMS_MST WHERE PatientID = " & PatientID & " Order by [Year],[Month]"


                strSelectQry = " Select DocumentID, DocumentFileName, DocumentDisplayName, Extension, SourceMachine, SystemFolder, Container, Category, PatientID, [Year], [Month], dbo.GetMonth([Month]) as nMonth , ArchiveStatus, ArchiveDescription, UsedStatus, UsedMachine, UsedType, UploadDateTime, VersionNo, ModifyDateTime, IsReviewed From VMS_MST  WHERE PatientID = " & PatientID & " Order by [Year], nMonth "
                Dim dt As DataTable
                oDB.Connect(GetConnectionString)
                dt = oDB.ReadQueryDataTable(strSelectQry)
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing

                If Not IsNothing(dt) = True Then
                    Return dt
                End If
                Return Nothing
            End Function
            'function commented by dipak as not in use.

            'Public Function UploadVideo_Library(ByVal fSourceFile As System.IO.FileInfo) As Boolean
            '    '' Save the Media to the gloVideo Library
            '    oMedia = New Media.Media
            '    oDMS = New gloStream.gloDMS.Supporting.Supporting

            '    oMedia.MediaFileName = fSourceFile.Name
            '    oMedia.Month = oDMS.MonthAsString(Now.Month)
            '    oMedia.ModifiedDateTime = fSourceFile.LastWriteTime
            '    oMedia.MediaFileDisplayName = Now.ToString
            '    oMedia.PatientID = gnPatientID
            '    oMedia.SourceMachine = gstrClientMachineName
            '    oMedia.SystemFolder = fSourceFile.DirectoryName
            '    oMedia.Year = Now.Year.ToString


            '    If IO.Directory.Exists(VMSRootPath) = False Then
            '        _DestinationFolder = VMSRootPath
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System") = False Then
            '        _DestinationFolder = VMSRootPath & "VMS System"
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System" & "\Container\") = False Then
            '        _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\"
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System" & "\Container\" & "General\") = False Then
            '        _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\" & "General\"
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID) = False Then
            '        _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID & "\" & oMedia.Year) = False Then
            '        _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID & "\" & oMedia.Year
            '        MkDir(_DestinationFolder)
            '    End If
            '    If IO.Directory.Exists(VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID & "\" & oMedia.Year & "\" & oMedia.Month) = False Then
            '        _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID & "\" & oMedia.Year & "\" & oMedia.Month & "\"
            '        MkDir(_DestinationFolder)
            '    End If
            '    _DestinationFolder = VMSRootPath & "\VMS System" & "\Container\" & "General\" & oMedia.PatientID & "\" & oMedia.Year & "\" & oMedia.Month & "\"
            '    File.Copy(oMedia.SystemFolder & "\" & oMedia.MediaFileName, _DestinationFolder & "\" & oMedia.MediaFileName)


            '    Return True
            'End Function
            Public Function SendToGeneralBin(ByVal oParameters As gloStream.gloVMS.Supporting.ImportProcessParameters) As Boolean
                Dim oPathFolders As New gloStream.gloVMS.Supporting.PathAndFolder
                Dim oSupport As New gloStream.gloVMS.Supporting.Supporting
                Dim oDocument As New gloStream.gloVMS.Document.document

                Dim _Success As Boolean = False
                Dim _DestinationDocumentFolderPath As String = ""
                Dim _DestinationDocument As String = ""
                Dim _DestinationDocumentFileName As String = ""
                Dim _ProcessDocument As String = ""

                Dim _PercentageFactor As Integer = 0, _Percentage As Integer = 0
                Dim i As Integer, nFileCntr As Integer = 0


                Try
                    With oParameters
                        '0 Percentage Factor
                        _PercentageFactor = Math.Round((.Documents.Count * 40) / 100)

                        '1. Category
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            If Trim(.Category) = "" Then
                                _ErrorMessage = "Category not found"
                                SendToGeneralBin = Nothing
                                Exit Function
                            End If
                        End If

                        '2. Necessary Fiels
                        If .PatientID = 0 Or Trim(.Month) = "" Or Trim(.Year) = "" Then
                            _ErrorMessage = "Incorrect document information"
                            SendToGeneralBin = Nothing
                            Exit Function
                        End If


                        _DestinationDocumentFolderPath = oPathFolders.GetVMSPath(VMSRootPath) & oParameters.SystemFolder & "\" & oParameters.Container & "\" & .Category & "\" & .PatientID & "\" & .Year & "\" & .Month
                        If Directory.Exists(_DestinationDocumentFolderPath) = False Then
                            MkDir(_DestinationDocumentFolderPath)
                        End If

                        _DestinationDocumentFileName = oSupport.NewDocumentFileNameOrID(.PatientID, False)
                        oParameters.DocumentID = _DestinationDocumentFileName

                        _DestinationDocument = _DestinationDocumentFolderPath & "\" & _DestinationDocumentFileName & oParameters.Extension 'oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_DestinationDocument) = True
                            _DestinationDocumentFileName = oSupport.NewDocumentFileNameOrID(.PatientID, False)
                            ' _DestinationDocument = _DestinationDocumentFolderPath & "\" & oParameters.DocumentFileName       '& _DestinationDocumentFileName & "." & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While


                        'Report Percentage
                        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '6. Create Import File



                        File.Copy(.Documents(1), _DestinationDocumentFolderPath & "\" & _DestinationDocumentFileName & oParameters.Extension) '  _ProcessDocument, True)




                        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)


                        '8.2 Send Document to Destination
                        ' File.Copy(oParameters.SystemFolder, _DestinationDocumentFileName) ' _ProcessDocument, _DestinationDocument) : File.SetAttributes(_DestinationDocument, FileAttributes.Normal)



                        '10 Send To Database
                        oParameters.MediaFileName = _DestinationDocumentFileName


                        '13. Delete Temporary Process Files
                        If File.Exists(_ProcessDocument) = True Then File.SetAttributes(_ProcessDocument, FileAttributes.Normal)
                        If File.Exists(_ProcessDocument) = True Then Kill(_ProcessDocument)

                        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _Success = True
                    End With

                    Return _Success
                Catch ex As SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateLog(ex.ToString)
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = ex.Message
                    SendToGeneralBin = Nothing
                    Exit Function
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = oError.Message
                    SendToGeneralBin = Nothing
                    Exit Function
                Finally
                    oPathFolders = Nothing
                    oSupport = Nothing

                    oDocument = Nothing

                    _DestinationDocument = Nothing
                    i = Nothing
                End Try
            End Function
            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public Sub New(ByVal PatientID As Long)
                _PatientID = PatientID
            End Sub
        End Class

        Namespace Media

            Public Class Media
                Public Sub New()
                End Sub
                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

                Private _DocumentID As Long
                Private _MediaFileName As String
                Private _MediaFileDisplayName As String
                Private _Extension As String
                Private _SourceMachine As String
                Private _SystemFolder As String
                Private _Container As String
                Private _Category As String
                Private _PatientID As Long
                Private _Year As String
                Private _Month As String
                Private _Archived As Boolean
                Private _ArchiveDescription As String
                Private _MediaType As Supporting.enumMediaType

                Private _InUsed As Boolean
                Private _UsedMachine As String
                ' Private _UsedType As Supporting.enumMediaType

                Private _Path As String ' to retrive document, not for store
                Private _UploadDateTime As DateTime

                Private _MachineID As Int16
                Private _VersionNo As Integer
                Private _ModifiedDateTime As DateTime

                Private _IsReviwed As Boolean = False
                Private _ReviwedDetails As gloStream.gloDMS.Document.ReviwedDetails

                Public Property DocumentID() As Long
                    Get
                        Return _DocumentID
                    End Get
                    Set(ByVal value As Long)
                        _DocumentID = value
                    End Set
                End Property

                'Media File Name 
                Public Property MediaFileName() As String
                    Get
                        Return _MediaFileName
                    End Get
                    Set(ByVal Value As String)
                        _MediaFileName = Value
                    End Set
                End Property

                'Media File Display Name - Maximum Length - 60
                Public Property MediaFileDisplayName() As String
                    Get
                        Return _MediaFileDisplayName
                    End Get
                    Set(ByVal Value As String)
                        _MediaFileDisplayName = Value
                    End Set
                End Property

                'Media File's Extension - Maximum Length - 5
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

                'Document Added in DMS, this Source Machine - Maximum Length - 255
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


                ''No of pages contain in document
                'Public Property Chapters() As Long
                '    Get
                '        Return _Chapters
                '    End Get
                '    Set(ByVal Value As Long)
                '        _Chapters = Value
                '    End Set
                'End Property

                'is this document is archived, means shift from current location to another, or its not in use from longer period
                'Public Property Archived() As Boolean
                '    Get
                '        Return _Archived
                '    End Get
                '    Set(ByVal Value As Boolean)
                '        _Archived = Value
                '    End Set
                'End Property

                'If archive then archive path for imergency access
                'Public Property ArchiveDescription() As String
                '    Get
                '        Return _ArchiveDescription
                '    End Get
                '    Set(ByVal Value As String)
                '        _ArchiveDescription = Value
                '    End Set
                'End Property

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

                'Media Type whether it is Patient's Video or Education Video 
                Public Property MediaType() As Supporting.enumMediaType
                    Get
                        Return _MediaType
                    End Get
                    Set(ByVal Value As Supporting.enumMediaType)
                        _MediaType = Value
                    End Set
                End Property

                'Document Type, means its Categorized, Uncategorized, or scan doucment, etc
                'Public Property Type() As Supporting.enumDocumentType
                '    Get
                '        Return _Type
                '    End Get
                '    Set(ByVal Value As Supporting.enumDocumentType)
                '        _Type = Value
                '    End Set
                'End Property

                'Document Path
                Public ReadOnly Property Path() As String
                    Get
                        Dim oSupport As New gloStream.gloVMS.Supporting.PathAndFolder
                        Dim _TempPath As String = oSupport.GetVMSPath(DMSRootPath) & _SystemFolder & "\" & _Container & "\" & _PatientID & "\" & _Year & "\" & _Month & "\" & _MediaFileDisplayName & "." & _Extension
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

                Public Property VersionNo() As Integer
                    Get
                        Return _VersionNo
                    End Get
                    Set(ByVal Value As Integer)
                        _VersionNo = Value
                    End Set
                End Property

                Public Property ModifiedDateTime() As DateTime
                    Get
                        Return _ModifiedDateTime
                    End Get
                    Set(ByVal Value As DateTime)
                        _ModifiedDateTime = Value
                    End Set
                End Property

                Public Property UploadDateTime() As DateTime
                    Get
                        Return _UploadDateTime
                    End Get
                    Set(ByVal Value As DateTime)
                        _UploadDateTime = Value
                    End Set
                End Property


            End Class

        End Namespace  ''  Namespace Media

        Namespace Supporting
            'Media Type
            Public Enum enumMediaType
                None = 0
                PatientMedia = 1
                EducationMedia = 2
            End Enum

            Public Class PathAndFolder

                'Public Const SystemFolder As String = "VMS System"
                'Public Const ContainerFolder As String = "Container"
                ''Public Const RecycleFolder As String = "Recycle Bin"
                'Public Const ScanFolder As String = "Scan"
                'Public Const GeneralFolder As String = "General"
                'Public Const TempExtractFolder As String = "Temp"
                'Public Const NoteDocumentFile As String = "Note.Doc"

                Private _TempProcessFolder As String


                Public Function GetVMSPath(ByVal RootPath As String) As String
                    Try
                        GetVMSPath = ""
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        GetVMSPath = ""
                        Exit Function
                    End Try
                End Function

            End Class ''PathAndFolder

            Public Enum enumDocumentType
                None = 0
                UnCategorisedDocument = 1
                CategorisedDocument = 2
                UnCategorisedDeletedDocument = 3
                CategorisedDeletedDocument = 4
            End Enum
            Public Class Supporting
                Public ReadOnly Property NewDocumentName(ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType) As String
                    Get
                        Threading.Thread.Sleep(1000)
                        Dim _NewDocumentName As String = ""
                        Dim _Date As String = Format(Date.Now, "MM/dd/yyyy")
                        Dim _Time As String = Format(Date.Now, "hh:mm:ss tt")
                        Dim i As Integer = 0
                        Dim oDocument As New gloStream.gloDMS.Document.Document


                        _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ")
                        While oDocument.FindDocument(_NewDocumentName, PatientID, Category, DocumentType) = True
                            i = i + 1
                            _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ") & "-" & i
                        End While

                        Return _NewDocumentName
                    End Get
                End Property
                Public ReadOnly Property NewDocumentFileNameOrID(ByVal PatientID As Long, ByVal GetDocID As Boolean) As String
                    Get
                        Dim _ReturnID As Long = 0
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@MachineID", GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@GetID", GetDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@ReturnID", _ReturnID, ParameterDirection.Output, SqlDbType.BigInt)
                        _ReturnID = oDB.ExecuteNonQueryForOutput("gsp_VMS_GetNewDocumentIDAndFileName")
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        Return _ReturnID
                    End Get
                End Property

            End Class
            Public Class ImportProcessParameters

                Private _DoucmentID As Long
                Private _MediaFileName As String
                Private _MediaFileDisplayName As String
                Private _Extension As String
                Private _SourceMachine As String
                Private _SystemFolder As String
                Private _Container As String
                Private _Category As String
                Private _Year As String
                Private _Month As String
                Private _Archived As Boolean
                Private _ArchiveDescription As String


                Private _InUsed As Boolean
                Private _UsedMachine As String
                ' Private _UsedType As Supporting.enumMediaType

                Private _Path As String ' to retrive document, not for store
                Private _UploadDateTime As DateTime

                Private _MachineID As Int16
                Private _VersionNo As Integer
                Private _ModifiedDateTime As DateTime
                Private _PatientID As Long = 0                                                      ' Patient ID





                Private _DocumentType As gloStream.gloDMS.Supporting.enumDocumentType
                Private _Documents As Collection
                Private _ScanDocument As Boolean
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _Documents.Clear()
                        bAssigned = False
                    End If
                End Sub
                Public Property DocumentID() As Long
                    Get
                        Return _DoucmentID
                    End Get
                    Set(ByVal value As Long)
                        _DoucmentID = value
                    End Set
                End Property

                Public Property MediaFileName() As String
                    Get
                        Return _MediaFileName
                    End Get
                    Set(ByVal value As String)
                        _MediaFileName = value
                    End Set
                End Property
                Public Property MediaFileDisplayName() As String
                    Get
                        Return _MediaFileDisplayName
                    End Get
                    Set(ByVal value As String)
                        _MediaFileDisplayName = value
                    End Set
                End Property
                Public Property Extension() As String
                    Get
                        Return _Extension
                    End Get
                    Set(ByVal value As String)
                        _Extension = value
                    End Set
                End Property
                Public Property SourceMachine() As String
                    Get
                        Return _SourceMachine
                    End Get
                    Set(ByVal value As String)
                        _SourceMachine = value
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
                Public Property SystemFolder() As String
                    Get
                        Return _SystemFolder
                    End Get
                    Set(ByVal value As String)
                        _SystemFolder = value
                    End Set
                End Property

                Public Property Container() As String
                    Get
                        Return _Container
                    End Get
                    Set(ByVal Value As String)
                        _Container = Value
                    End Set
                End Property

                Public Property Category() As String
                    Get
                        Return _Category
                    End Get
                    Set(ByVal Value As String)
                        _Category = Value
                    End Set
                End Property

                Public Property Month() As String
                    Get
                        Return _Month
                    End Get
                    Set(ByVal Value As String)
                        _Month = Value
                    End Set
                End Property

                Public Property Year() As String
                    Get
                        Return _Year
                    End Get
                    Set(ByVal Value As String)
                        _Year = Value
                    End Set
                End Property
                Public Property Archived() As Boolean
                    Get
                        Return _Archived
                    End Get
                    Set(ByVal value As Boolean)
                        _Archived = value
                    End Set
                End Property

                Public Property ArchiveDescription() As String
                    Get
                        Return _ArchiveDescription
                    End Get
                    Set(ByVal value As String)
                        _ArchiveDescription = value
                    End Set
                End Property

                Public Property DocumentType() As gloStream.gloDMS.Supporting.enumDocumentType
                    Get
                        Return _DocumentType
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentType)
                        _DocumentType = Value
                    End Set
                End Property

                Public Property Documents() As Collection
                    Get
                        Return _Documents
                    End Get
                    Set(ByVal Value As Collection)
                        If (bAssigned) Then
                            _Documents.Clear()
                            bAssigned = False
                        End If
                        _Documents = Value
                    End Set
                End Property

                Public Property ScanDocument() As Boolean
                    Get
                        Return _ScanDocument
                    End Get
                    Set(ByVal Value As Boolean)
                        _ScanDocument = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Documents = New Collection
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Documents = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

        End Namespace  ''  Namespace Supporting

        Namespace Document
            Public Class document
                Private _ErrorMessage As String
                Public Function FindDocument(ByVal DocumentName As String, ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String = ""
                    FindDocument = True

                    Try

                        'Pick Record From Database for varification
                        _SQLQuery = "SELECT DocumentDisplayName FROM VMS_MST WHERE DocumentDisplayName = '" & DocumentName & "' AND PatientID = " & PatientID & " AND Category = '" & Category & "' "
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = False Then
                            Return False
                        Else
                            Return True
                        End If
                        'oDB.Disconnect()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function
                Public Function UpdateReviwed(ByVal Path As String, ByVal ReviwedUserID As Long, ByVal ReviwedDateTime As DateTime, ByVal Comments As String) As Boolean
                    Dim oSupporting As New gloStream.gloVMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _DocumentFileName As Long
                    Dim _Result As Boolean = False

                    '//
                    Dim _PathFolder As New gloStream.gloVMS.Supporting.PathAndFolder
                    Dim _Path As String
                    Dim _VMSPath As String = _PathFolder.GetVMSPath(VMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0

                    Try
                        If File.Exists(Path) = True Then
                            _Path = Replace(Path, _VMSPath, "")
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
                                            _Extension = "." & Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                                    End Select
                                Next
                                'Pick Record From Database for varification
                                _SQLQuery = "SELECT DocumentID,DocumentFileName FROM VMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentID IS NOT NULL AND DocumentFileName IS NOT NULL"
                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _DocumentID = oDataReader.Item("DocumentID")
                                        _DocumentFileName = oDataReader.Item("DocumentFileName")
                                    End While
                                End If
                                oDataReader.Close()
                                oDB.Disconnect()
                            End If
                        End If
                        '//

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
                            'Add Reviwed Detail
                            If IsDate(ReviwedDateTime) = False Then
                                ReviwedDateTime = Now.Date
                            End If
                            _Result = oDB.ExecuteQueryNonQuery("INSERT INTO VMS_ReviewedDetail (nDocumentID,nDocumentFileName,nReviwedBy,ReviwedDateTime,sComments) VALUES (" & _DocumentID & "," & _DocumentFileName & "," & ReviwedUserID & ",'" & ReviwedDateTime & "','" & Comments & "')")
                            'Update DMS_MST
                            If _Result = True Then
                                _Result = oDB.ExecuteQueryNonQuery("UPDATE VMS_MST SET IsReviewed = 1 WHERE DocumentID = " & _DocumentID & "")
                            End If
                            'oDB.Disconnect()
                        End If

                        Return _Result
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "video acknowledgement added.  ", gloAuditTrail.ActivityOutCome.Success)


                        If _Result = True Then
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "video acknowledgement added.  ", gloAuditTrail.ActivityOutCome.Success)
                            ''Added Rahul P on 20101009
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "video acknowledgement added.  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''
                        End If

                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateReviwed = Nothing
                        Exit Function

                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        UpdateReviwed = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oSupporting = Nothing
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function
                Public Function ViewReviwed(ByVal Path As String) As gloStream.gloVMS.Document.ReviwedDetail
                    Dim oSupporting As New gloStream.gloVMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim oResult As New gloStream.gloVMS.Document.ReviwedDetail

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _DocumentFileName As Long

                    '//
                    Dim _PathFolder As New gloStream.gloVMS.Supporting.PathAndFolder
                    Dim _Path As String
                    Dim _VMSPath As String = _PathFolder.GetVMSPath(VMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0

                    Try
                        If File.Exists(Path) = True Then
                            _Path = Replace(Path, _VMSPath, "")
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
                                            _Extension = "." & Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                                    End Select
                                Next
                                'Pick Record From Database for varification
                                _SQLQuery = "SELECT DocumentID,DocumentFileName FROM VMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentID IS NOT NULL AND DocumentFileName IS NOT NULL"
                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _DocumentID = oDataReader.Item("DocumentID")
                                        _DocumentFileName = oDataReader.Item("DocumentFileName")
                                    End While
                                End If
                                oDataReader.Close()
                                oDB.Disconnect()
                            End If
                        End If
                        '//

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            Dim _Read As Boolean = False
                            oDB.Connect(GetConnectionString)
                            _SQLQuery = "SELECT VMS_ReviewedDetail.nReviwedBy, User_MST.sLoginName, VMS_ReviewedDetail.ReviwedDateTime, VMS_ReviewedDetail.sComments " _
                            & " FROM VMS_ReviewedDetail INNER JOIN User_MST ON VMS_ReviewedDetail.nReviwedBy = User_MST.nUserID " _
                            & " WHERE (VMS_ReviewedDetail.nDocumentID = " & _DocumentID & ") AND (VMS_ReviewedDetail.nDocumentFileName = " & _DocumentFileName & ") ORDER BY VMS_ReviewedDetail.ReviwedDateTime DESC"
                            oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    If _Read = False Then
                                        If Not IsDBNull(oDataReader.Item("nReviwedBy")) Then
                                            oResult.ReviwedByUserID = oDataReader.Item("nReviwedBy")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sLoginName")) Then
                                            oResult.ReviwedByUserName = oDataReader.Item("sLoginName")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("ReviwedDateTime")) Then
                                            oResult.ReviwedDateTime = oDataReader.Item("ReviwedDateTime")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sComments")) Then
                                            oResult.Comments = oDataReader.Item("sComments")
                                        End If
                                        Exit While
                                    End If
                                End While
                            End If
                            'oDB.Disconnect()
                        End If

                        Return oResult
                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ViewReviwed = Nothing
                        Exit Function
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        ViewReviwed = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oSupporting = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function
            End Class
            Public Class ReviwedDetail
                Private _ReviwedByUsedID As Long
                Private _ReviwedByUserName As String
                Private _ReviwedDateTime As DateTime
                Private _Comments As String

                Public Property ReviwedByUserID() As Long
                    Get
                        Return _ReviwedByUsedID
                    End Get
                    Set(ByVal Value As Long)
                        _ReviwedByUsedID = Value
                    End Set
                End Property

                Public Property ReviwedByUserName() As String
                    Get
                        Return _ReviwedByUserName
                    End Get
                    Set(ByVal Value As String)
                        _ReviwedByUserName = Value
                    End Set
                End Property

                Public Property ReviwedDateTime() As DateTime
                    Get
                        Return _ReviwedDateTime
                    End Get
                    Set(ByVal Value As DateTime)
                        _ReviwedDateTime = Value
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

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class
        End Namespace

        Namespace VMSTranscation


            Public Class Transaction_Master

                'Dim oRead As System.Data.SqlClient.SqlDataReader
                Public Sub New()

                End Sub
                Public Function SaveTransaction_MSTRecord(ByVal MachineID As Int64, ByVal VisitID As Int64, ByVal PatientID As Int64, ByVal DateofService As DateTime) As Boolean
                    Dim ODB As gloStream.gloDataBase.gloDataBase = Nothing
                    Try

                        ODB = New gloStream.gloDataBase.gloDataBase

                        ODB.Connect(GetConnectionString)

                        ODB.DBParameters.Add("@MachineID", MachineID, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.DBParameters.Add("@vtm_VisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.DBParameters.Add("@vtm_PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.DBParameters.Add("@vtm_DOS", DateofService, ParameterDirection.Input, SqlDbType.DateTime)

                        ODB.ExecuteNonQuery("gsp_VMSInsertTranscation_MST")

                        Return True

                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End Try
                End Function

                Public Function GetUserID(ByVal UserName As String) As Long
                    Dim ODB As gloStream.gloDataBase.gloDataBase = Nothing
                    Try
                        ODB = New gloStream.gloDataBase.gloDataBase

                        ODB.Connect(GetConnectionString)
                        Dim strSelectQry As String = "SELECT nUserID from User_MST WHERE sLoginName = '" & UserName.Replace("'", "''") & "' "
                        Dim oDataReader As SqlDataReader
                        'oRead = New System.Data.SqlClient.SqlDataReader
                        oDataReader = ODB.ReadQueryRecords(strSelectQry)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                Return oDataReader.Item("nUserID")
                            End While
                        End If
                        oDataReader.Close()
                        oDataReader.Dispose()
                        ODB.Disconnect()
                        Return Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        Return Nothing
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                    End Try

                End Function

                Public Function GetTransctionID(ByVal VisitID As Int64, ByVal PatientID As Int64) As Long
                    Dim ODB As gloStream.gloDataBase.gloDataBase = Nothing
                    Try
                        ODB = New gloStream.gloDataBase.gloDataBase
                        ODB.Connect(GetConnectionString)
                        Dim strSelectQry = "SELECT vtm_ID FROM VMS_Trn_MST WHERE vtm_VisitID = " & VisitID & " and vtm_PatientID=" & PatientID & ""
                        Dim oDataReader As SqlDataReader
                        oDataReader = ODB.ReadQueryRecords(strSelectQry)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                Return oDataReader.Item("vtm_ID")
                            End While
                        End If
                        oDataReader.Close()
                        oDataReader.Dispose()
                        ODB.Disconnect()
                        Return Nothing
                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GetTransctionID = Nothing
                        Exit Function
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                    End Try
                End Function
                Public Function InsertVMSDetails(ByVal Trn_ID As Int64, ByVal startTime As String, ByVal EndTime As String, ByVal VideoName As String, ByVal VideoDate As DateTime, ByVal Note As String, ByVal Reason As String, ByVal comments As String, ByVal UserID As Int64) As Boolean
                    Dim ODB As gloStream.gloDataBase.gloDataBase = Nothing
                    Try
                        If startTime = "" Then
                            startTime = "00:00"
                        End If
                        If EndTime = "" Then
                            EndTime = "00:00"
                        End If


                        ODB = New gloStream.gloDataBase.gloDataBase
                        ODB.Connect(GetConnectionString)

                        ODB.DBParameters.Add("@vtd_trnID", Trn_ID, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.DBParameters.Add("@vtd_StartTime", startTime, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_EndTime", EndTime, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_VideoName", VideoName, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_Date", VideoDate, ParameterDirection.Input, SqlDbType.DateTime)

                        ODB.DBParameters.Add("@vtd_Note", Note, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_Reason", Reason, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_Comments", comments, ParameterDirection.Input, SqlDbType.VarChar)

                        ODB.DBParameters.Add("@vtd_UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.ExecuteNonQuery("gsp_VMSInsertTranscation_DTL")

                        ODB.Disconnect()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "video video deleted.  ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "video video deleted.  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                    End Try
                    Return Nothing
                End Function

                Public Function GetVideodetailsList(ByVal PatientID As Long) As DataTable
                    Dim ODB As gloStream.gloDataBase.gloDataBase = Nothing
                    Try

                        ODB = New gloStream.gloDataBase.gloDataBase
                        ODB.Connect(GetConnectionString)
                        Dim strSelectQry As String = "SELECT VMS_Trn_DTL.vtd_ID, VMS_Trn_DTL.vtd_trnID, VMS_Trn_DTL.vtd_StartTime, VMS_Trn_DTL.vtd_EndTime, VMS_Trn_DTL.vtd_VideoName, " _
                                           & "  VMS_Trn_DTL.vtd_Date, VMS_Trn_DTL.vtd_Note, VMS_Trn_DTL.vtd_Reason, VMS_Trn_DTL.vtd_Comments, VMS_Trn_DTL.vtd_UserID,  " _
                                           & " VMS_Trn_MST.vtm_VisitID, VMS_Trn_MST.vtm_DOS, VMS_Trn_MST.vtm_ID FROM VMS_Trn_DTL INNER JOIN VMS_Trn_MST ON VMS_Trn_DTL.vtd_trnID = VMS_Trn_MST.vtm_ID Where VMS_Trn_MST.vtm_PatientID = " & PatientID & " ORDER BY VMS_Trn_DTL.vtd_trnID "
                        Dim dt As DataTable
                        dt = ODB.ReadQueryDataTable(strSelectQry)
                        If Not IsNothing(dt) = True Then
                            Return dt
                        End If
                        Return Nothing
                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End Try

                End Function

                Public Function DeletePatientVideo(ByVal vtd_ID As Long, ByVal transaction_ID As Long) As Boolean

                    Dim oDB As New DataBaseLayer
                    Dim oParamater As DBParameter


                    Try

                        oParamater = New DBParameter
                        oParamater.DataType = SqlDbType.BigInt
                        oParamater.Direction = ParameterDirection.Input
                        oParamater.Name = "@vtd_ID"
                        oParamater.Value = vtd_ID
                        oDB.DBParametersCol.Add(oParamater)
                        oParamater = Nothing

                        oParamater = New DBParameter
                        oParamater.DataType = SqlDbType.BigInt
                        oParamater.Direction = ParameterDirection.Input
                        oParamater.Name = "@vtd_trnID"
                        oParamater.Value = transaction_ID

                        oDB.DBParametersCol.Add(oParamater)
                        oParamater = Nothing

                        oDB.Delete("gsp_VMSDeletePatientVideoDetails")

                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient video added.  ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient video added.  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                    Catch ex As SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog(ex.ToString)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                    Return Nothing
                End Function
            End Class

            'Public Class Details
            '    Implements ICollection(Of TransactionDetail)'ICollection(Of TransactionDetail)
            '    Dim oCol As Collection

            '    Public Sub Add(ByVal item As TransactionDetail) Implements System.Collections.Generic.ICollection(Of TransactionDetail).Add
            '        oCol.Add(item)
            '    End Sub

            '    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of TransactionDetail).Clear
            '        oCol.Clear()
            '    End Sub

            '    Public Function Contains(ByVal item As TransactionDetail) As Boolean Implements System.Collections.Generic.ICollection(Of TransactionDetail).Contains

            '    End Function

            '    Public Sub CopyTo(ByVal array() As TransactionDetail, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of TransactionDetail).CopyTo

            '    End Sub

            '    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of TransactionDetail).Count
            '        Get
            '            Return oCol.Count
            '        End Get
            '    End Property

            '    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of Details).IsReadOnly
            '        Get

            '        End Get
            '    End Property

            '    Public Function Remove(ByVal item As Detail) As Boolean Implements System.Collections.Generic.ICollection(Of Detail).Remove

            '    End Function

            '    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Details) Implements System.Collections.Generic.IEnumerable(Of Details).GetEnumerator

            '    End Function

            '    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator

            '    End Function

            'End Class

            Public Class TransactionDetail
                Private _StartTime As String
                Private _EndTime As String
                Private _VideoName As String
                Private _dos As DateTime
                Private _Title As String
                'Private _Reason As String
                Private _Comments As String
                Private _UserID As Long

                Public Property StartTime() As String
                    Get
                        Return _StartTime
                    End Get
                    Set(ByVal value As String)
                        _StartTime = value
                    End Set
                End Property

                Public Property EndTime() As String
                    Get
                        Return _EndTime
                    End Get
                    Set(ByVal value As String)
                        _EndTime = value
                    End Set
                End Property
                Public Property VideoName() As String
                    Get
                        Return _VideoName
                    End Get
                    Set(ByVal value As String)
                        _VideoName = value
                    End Set
                End Property
                Public Property dos() As DateTime
                    Get
                        Return _dos
                    End Get
                    Set(ByVal value As DateTime)
                        _dos = value
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
                'Public Property Reason() As String
                '    Get
                '        Return _Reason
                '    End Get
                '    Set(ByVal value As String)
                '        _Reason = value
                '    End Set
                'End Property
                Public Property Comments() As String
                    Get
                        Return _Comments
                    End Get
                    Set(ByVal value As String)
                        _Comments = value
                    End Set
                End Property

                Public Property UserID() As Long
                    Get
                        Return _UserID
                    End Get
                    Set(ByVal value As Long)
                        _UserID = value
                    End Set
                End Property

                Public Sub New()

                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class
        End Namespace '' Namespace VMSTransaction

    End Namespace  ''  Namespace gloVMS


End Namespace   ''  Namespace gloStream
