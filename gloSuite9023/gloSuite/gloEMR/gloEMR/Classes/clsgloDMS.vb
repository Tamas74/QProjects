Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports gloSettings
Namespace gloStream
    Namespace gloDMS
        Public Class DMS
            Private _ErrorMessage As String
            Public Event ReportPercentage(ByVal nPercentage As Integer)
            Public Event ReportImportDocument(ByVal DocumentPath As String)
            Public nDocumentID As Long
            Dim oProcessor As BEPPROCLib.PDFProcessor

            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public Function RenameDocument(ByVal DocPatientID As Long, ByVal DocCategory As String, ByVal DocMonth As String, ByVal DocYear As String, ByVal DocumentFileName As Long, ByVal NewDocumentName As String) As Boolean

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Result As Boolean = False

                Try

                    Dim _strQry As String = "UPDATE  DMS_MST SET  DocumentName = '" & NewDocumentName & "' where Category = '" & DocCategory & "' AND PatientID = " & DocPatientID & " AND Year = '" & DocYear & "' AND Month = '" & DocMonth & "' AND DocumentFileName =" & DocumentFileName & " "
                    'Insert New Record for New Document Command
                    oDB.Connect(GetConnectionString)
                    _Result = oDB.ExecuteNonSQLQuery(_strQry)
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing


                    Return _Result
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    RenameDocument = Nothing
                    Exit Function
                Finally

                    oDB = Nothing
                End Try
            End Function

            Public Function SendToGeneralBin(ByVal oParameters As gloStream.gloDMS.Supporting.ImportProcessParameters) As Boolean
                ' Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oDocument As New gloStream.gloDMS.Document.Document

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

                        '3. Destination Document
                        '3.2 Category Document Folder Path
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            _DestinationDocumentFolderPath = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & Supporting.PathsAndFolders.SystemFolder & "\" & Supporting.PathsAndFolders.ContainerFolder & "\" & .Category & "\" & .PatientID & "\" & .Year & "\" & .Month
                            If Directory.Exists(_DestinationDocumentFolderPath) = False Then
                                MkDir(_DestinationDocumentFolderPath)
                            End If
                        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            _DestinationDocumentFolderPath = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & Supporting.PathsAndFolders.SystemFolder & "\" & Supporting.PathsAndFolders.ScanFolder & "\" & .PatientID & "\" & .Year & "\" & .Month
                            If Directory.Exists(_DestinationDocumentFolderPath) = False Then
                                MkDir(_DestinationDocumentFolderPath)
                            End If
                        End If

                        '3.3 Destination Document Exits or not
                        _DestinationDocumentFileName = oSupport.NewDocumentFileNameOrID(.PatientID, False)
                        _DestinationDocument = _DestinationDocumentFolderPath & "\" & _DestinationDocumentFileName & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_DestinationDocument) = True
                            _DestinationDocumentFileName = oSupport.NewDocumentFileNameOrID(.PatientID, False)
                            _DestinationDocument = _DestinationDocumentFolderPath & "\" & _DestinationDocumentFileName & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        '//_DestinationDocument = _DestinationDocumentFolderPath & "\" & .DocumentName & "." & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)// Document Name Changes Aug 2006
                        'If File.Exists(_DestinationDocument) = True Then
                        '    _ErrorMessage = "Document with same name already exists, please enter another name"
                        '    Exit Function
                        'End If

                        '//If oDocument.FindDocument(.DocumentName, oParameters.PatientID) = True Then// Document Name Changes Aug 2006
                        If oDocument.FindDocument(.DocumentName, oParameters.PatientID, .Category, .DocumentType) = True Then
                            _ErrorMessage = "Document with same name already exists, please enter another name"
                            SendToGeneralBin = Nothing
                            Exit Function
                        End If

                        '4. Process Document
                        _ProcessDocument = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocument) = True
                            _ProcessDocument = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        '//Document Name Changes Aug 2006 - Start//
                        '_ProcessDocument = oPathFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID) & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        'While File.Exists(_ProcessDocument) = True
                        '    _ProcessDocument = oPathFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID) & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        'End While
                        '//Document Name Changes Aug 2006 - Finish//

                        'Report Percentage
                        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '6. Create Import File
                        oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                        oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"

                        If .Documents.Count = 1 Then ' Direcly copy if single file
                            File.Copy(.Documents(1), _ProcessDocument, True)
                        ElseIf .Documents.Count = 2 Then ' Merge 2 files
                            oProcessor.Merge(.Documents(1), .Documents(2), _ProcessDocument)
                        ElseIf .Documents.Count > 2 Then ' Merge 2 files
                            oProcessor.Merge(.Documents(1), .Documents(2), _ProcessDocument)
                            For i = 3 To .Documents.Count
                                oProcessor.Merge(_ProcessDocument, .Documents(i), _ProcessDocument)
                            Next
                        End If

                        oProcessor.Optimize(_ProcessDocument, _ProcessDocument)
                        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)


                        '8.2 Send Document to Destination
                        File.Copy(_ProcessDocument, _DestinationDocument) : File.SetAttributes(_DestinationDocument, FileAttributes.Normal)

                        oProcessor = Nothing

                        '10 Send To Database
                        oParameters.DocumentFileName = _DestinationDocumentFileName
                        If Save_SendToGeneralBin(oParameters, _DestinationDocument) = True Then
                            Application.DoEvents() : RaiseEvent ReportImportDocument(_DestinationDocument)
                        End If

                        '13. Delete Temporary Process Files
                        If File.Exists(_ProcessDocument) = True Then File.SetAttributes(_ProcessDocument, FileAttributes.Normal)
                        If File.Exists(_ProcessDocument) = True Then Kill(_ProcessDocument)

                        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _Success = True
                    End With

                    Return _Success
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = oError.Message
                    SendToGeneralBin = Nothing
                    Exit Function
                Finally
                    'oPathFolders = Nothing
                    oSupport = Nothing
                    oProcessor = Nothing
                    oDocument = Nothing

                    _DestinationDocument = Nothing
                    i = Nothing
                End Try
            End Function

            Public Function SendToCategory(ByVal oParameters As gloStream.gloDMS.Supporting.UncategoryProcessParameters, ByVal DocumentDispName As String, Optional ByVal NewDocumentName As String = "", Optional ByVal MergeDocumentPath As String = "") As Boolean
                ' Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oDocument As New gloStream.gloDMS.Document.Document

                Dim _Success As Boolean = False
                Dim _DestinationDocument As String = ""

                Dim _ProcessDocumentUncat As String = ""
                Dim _ProcessDocumentCat As String = ""

                Dim _UncategoryDocumentFolderPath As String = ""
                Dim _CategoryDocumentFolderPath As String = ""

                Dim _SendUncategoryDocument As String = ""
                Dim _SendCategoryDocument As String = ""

                Dim _PercentageFactor As Integer = 0, _Percentage As Integer = 0
                Dim _DestDocPageCountBeforeMerge As Integer = 0

                Dim _DestDocumentFileName As String = ""
                Dim _CategoryVersionNo As Integer = 0, _UncategoryVersionNo As Integer = 0
                Dim i As Integer

                Try
                    With oParameters
                        If checkFileopen(.Document.Path) = True Then
                            MessageBox.Show("This file is being used by another user.you cannot categorized it now", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''''Temprary retrun sucess because showing other Messagebox. 
                            _Success = True
                            Return _Success
                            Exit Function
                        End If
                        '0 Percentage Factor
                        _PercentageFactor = Math.Round((.PageCount * 40) / 100)

                        '1. Check Uncategory Document Exists or not
                        If File.Exists(.Document.Path) = False Then
                            _ErrorMessage = "Document not exists in DMS system"
                            SendToCategory = Nothing
                            Exit Function
                        End If
                        '2. Necessary Fiels
                        If .PatientID = 0 Or .SelectedPages.Count = 0 Or .DestinationCategory = "" Then
                            _ErrorMessage = "Incorrect document information"
                            SendToCategory = Nothing
                            Exit Function
                        End If

                        '3. Destination Document
                        '3.1 Uncategory Document Folder Path
                        Dim oFileUncat As FileInfo = New FileInfo(.Document.Path)
                        _UncategoryDocumentFolderPath = oFileUncat.Directory.FullName
                        oFileUncat = Nothing
                        _UncategoryVersionNo = oParameters.Document.VersionNo + 1


                        '3.2 Category Document Folder Path
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _CategoryDocumentFolderPath = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & Supporting.PathsAndFolders.SystemFolder & "\" & Supporting.PathsAndFolders.ContainerFolder & "\" & .DestinationCategory & "\" & .PatientID & "\" & .ShowDocumentYear & "\" & .DestinationMonth
                            If Directory.Exists(_CategoryDocumentFolderPath) = False Then
                                MkDir(_CategoryDocumentFolderPath)
                            End If
                        Else
                            Dim oFileCat As FileInfo = New FileInfo(MergeDocumentPath)
                            _CategoryDocumentFolderPath = oFileCat.Directory.FullName
                            oFileCat = Nothing
                        End If

                        '3.3 Destination Document Exits or not
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _DestinationDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentFileNameOrID(oParameters.PatientID, False) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                            If File.Exists(_DestinationDocument) = True Then
                                _ErrorMessage = "Document with same name already exists, please enter another name"
                                SendToCategory = Nothing
                                Exit Function
                            End If
                            If oDocument.FindDocument(NewDocumentName, oParameters.PatientID, oParameters.DestinationCategory, Supporting.enumDocumentType.CategorisedDocument) = True Then
                                _ErrorMessage = "Document with same name already exists, please enter another name"
                                SendToCategory = Nothing
                                Exit Function
                            End If
                        Else
                            _DestinationDocument = MergeDocumentPath
                            If File.Exists(_DestinationDocument) = False Then
                                _ErrorMessage = "Merge Document not exist in DMS system"
                                SendToCategory = Nothing
                                Exit Function
                            End If
                        End If

                        'Category Version No
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _CategoryVersionNo = 1
                        Else
                            _CategoryVersionNo = oDocument.GetCategorisedDocumentVersion(MergeDocumentPath, False, False) + 1
                        End If

                        Dim oFileDestDoc As FileInfo = New FileInfo(_DestinationDocument)
                        _DestDocumentFileName = Replace(oFileDestDoc.Name, oFileDestDoc.Extension, "")

                        '4. Make a copy of existing uncategory document to process on it
                        _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentUncat) = True
                            _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentCat) = True
                            _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        File.Copy(.Document.Path, _ProcessDocumentUncat, True) : File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                        File.Copy(.Document.Path, _ProcessDocumentCat, True) : File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)

                        '5. Check All pages selected or not
                        If .SelectedPages.Count = .PageCount Then
                            Kill(_ProcessDocumentUncat)
                        End If

                        'Report Percentage
                        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '6. Create Uncategory File
                        oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                        oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"
                        If File.Exists(_ProcessDocumentUncat) = True Then
                            For i = .PageCount To 1 Step -1
                                If oSupport.IsItemExists(i, .SelectedPages) = True Then
                                    oProcessor.DeletePages(_ProcessDocumentUncat, _ProcessDocumentUncat, (i) - 1, (i) - 1)
                                End If
                                'Percentage
                                If i Mod _PercentageFactor = 0 Then
                                    _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                End If
                            Next
                            oProcessor.Optimize(_ProcessDocumentUncat, _ProcessDocumentUncat)
                        End If
                        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)


                        '7. Create Category File
                        If .SelectedPages.Count <> .PageCount Then
                            If File.Exists(_ProcessDocumentCat) = True Then
                                For i = .PageCount To 1 Step -1
                                    If oSupport.IsItemExists(i, .UnselectedPages) = True Then
                                        oProcessor.DeletePages(_ProcessDocumentCat, _ProcessDocumentCat, (i) - 1, (i) - 1)
                                    End If
                                    'Report Percentage
                                    If i Mod _PercentageFactor = 0 Then
                                        _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                    End If
                                Next
                            End If
                            oProcessor.Optimize(_ProcessDocumentCat, _ProcessDocumentCat)
                        End If
                        _Percentage = 80 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '8 Send Document To There Location
                        '8.1 Make Uncategory & Category Document Names
                        _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendUncategoryDocument) = True
                            _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendCategoryDocument) = True
                            _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        '8.2 Send Document to Category and Uncategory
                        File.Copy(_ProcessDocumentCat, _SendCategoryDocument) : File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal)
                        If .SelectedPages.Count <> .PageCount Then
                            File.Copy(_ProcessDocumentUncat, _SendUncategoryDocument) : File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal)
                        End If

                        '9. Make Chnages in Uncategory Document
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Rename(_SendCategoryDocument, _DestinationDocument)
                        Else
                            _DestDocPageCountBeforeMerge = oProcessor.GetPageCount(_DestinationDocument)
                            oProcessor.Merge(_DestinationDocument, _SendCategoryDocument, _DestinationDocument)
                            File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Kill(_SendCategoryDocument)
                        End If
                        oProcessor = Nothing

                        '10 Send To Database
                        If Save_SendToCategory(oParameters, _UncategoryDocumentFolderPath, _CategoryDocumentFolderPath, DocumentDispName, _DestDocumentFileName, _UncategoryVersionNo, _CategoryVersionNo, NewDocumentName, MergeDocumentPath, _DestDocPageCountBeforeMerge) = True Then
                            '11. After Sucessfully process work with uncategory document
                            If .SelectedPages.Count = .PageCount Then
                                If File.Exists(_SendUncategoryDocument) = True Then
                                    File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Kill(_SendUncategoryDocument)
                                End If
                                If File.Exists(.Document.Path) = True Then
                                    File.SetAttributes(.Document.Path, FileAttributes.Normal) : Kill(.Document.Path)
                                End If
                            Else
                                If File.Exists(.Document.Path) = True Then
                                    File.SetAttributes(.Document.Path, FileAttributes.Normal) : Kill(.Document.Path)
                                End If
                                File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Rename(_SendUncategoryDocument, .Document.Path)
                            End If
                        End If

                        '12. Uncategory Month Folder
                        Dim oMonthFolder As String = _UncategoryDocumentFolderPath
                        Dim oMonthDirectory As DirectoryInfo = New DirectoryInfo(_UncategoryDocumentFolderPath)
                        Dim oYearFolder As String = oMonthDirectory.Parent.FullName
                        Dim oYearDirectory As DirectoryInfo = New DirectoryInfo(oYearFolder)
                        Dim oPatientID As String = oYearDirectory.Parent.FullName
                        Dim oPatientDirectory As DirectoryInfo = New DirectoryInfo(oPatientID)

                        Dim oMonthFiles As FileInfo() = oMonthDirectory.GetFiles()
                        If oMonthFiles.Length = 0 Then
                            Directory.Delete(oMonthFolder, True)
                            Dim oYearFiles As DirectoryInfo() = oYearDirectory.GetDirectories
                            If oYearFiles.Length = 0 Then
                                Directory.Delete(oYearFolder, True)
                                Dim oPatientFiles As DirectoryInfo() = oPatientDirectory.GetDirectories()
                                If oPatientFiles.Length = 0 Then
                                    Directory.Delete(oPatientID, True)
                                End If
                                oPatientFiles = Nothing
                            End If
                            oYearFiles = Nothing
                        End If
                        oMonthFiles = Nothing


                        '13. Delete Temporary Process Files
                        If File.Exists(_ProcessDocumentCat) = True Then File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentUncat) = True Then File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentCat) = True Then Kill(_ProcessDocumentCat)
                        If File.Exists(_ProcessDocumentUncat) = True Then Kill(_ProcessDocumentUncat)

                        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _Success = True
                    End With

                    Return _Success
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = oError.Message
                    SendToCategory = Nothing
                    Exit Function
                Finally
                    ' oPathFolders = Nothing
                    oSupport = Nothing
                    oProcessor = Nothing
                    oDocument = Nothing

                    _DestinationDocument = Nothing
                    i = Nothing
                End Try
            End Function

            Public Function DeletePages(ByVal oParameters As gloStream.gloDMS.Supporting.PrintFaxDeleteParameters) As Boolean
                ' Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oDocument As New gloStream.gloDMS.Document.Document

                Dim _Success As Boolean = False
                Dim _DestinationDocument As String = ""

                Dim _ProcessDocumentUncat As String = ""
                Dim _ProcessDocumentCat As String = ""

                Dim _UncategoryDocumentFolderPath As String = ""
                Dim _CategoryDocumentFolderPath As String = ""

                Dim _SendUncategoryDocument As String = ""
                Dim _SendCategoryDocument As String = ""

                Dim _PercentageFactor As Integer = 0, _Percentage As Integer = 0
                Dim _DestDocPageCountBeforeMerge As Integer = 0

                Dim _CatUnCatDelDocVersionNo As Integer = 0
                Dim _DestinationDocumentName As String = ""
                Dim _DestinationDocumentDispName As String = ""
                Dim _DocumentFilePath As String = ""

                Dim i As Integer

                Try
                    With oParameters
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            _DocumentFilePath = oParameters.CategorisedDocument.Path
                        Else
                            _DocumentFilePath = oParameters.UncategorisedDocument.Path
                        End If
                        If checkFileopen(_DocumentFilePath) = True Then
                            MessageBox.Show("This file is being used by another user,you cannot delete it now", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''''Temprary retrun sucess because showing other Messagebox. 
                            _Success = True
                            Return _Success
                            Exit Function
                        End If
                        '0 Percentage Factor
                        _PercentageFactor = Math.Round((.PageCount * 40) / 100)

                        '1. Check Uncategory Document Exists or not
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            If File.Exists(.CategorisedDocument.Path) = False Then
                                _ErrorMessage = "Document not exists in DMS system"
                                DeletePages = Nothing
                                Exit Function
                            End If
                        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            If File.Exists(.UncategorisedDocument.Path) = False Then
                                _ErrorMessage = "Document not exists in DMS system"
                                DeletePages = Nothing
                                Exit Function
                            End If
                        End If

                        '2. Necessary Fiels
                        If .PatientID = 0 Or .SelectedPages.Count = 0 Then
                            _ErrorMessage = "Incorrect document information"
                            DeletePages = Nothing
                            Exit Function
                        End If

                        '3. Destination Document
                        '3.1 Uncategory Document Folder Path
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            Dim oFileUncat As FileInfo = New FileInfo(.CategorisedDocument.Path)
                            _UncategoryDocumentFolderPath = oFileUncat.Directory.FullName
                            oFileUncat = Nothing
                            _DestinationDocumentDispName = .CategorisedDocument.Name
                            _CatUnCatDelDocVersionNo = oDocument.GetCategorisedDocumentVersion(.CategorisedDocument.Path, False, False) + 1
                        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            Dim oFileUncat As FileInfo = New FileInfo(.UncategorisedDocument.Path)
                            _UncategoryDocumentFolderPath = oFileUncat.Directory.FullName
                            oFileUncat = Nothing
                            _DestinationDocumentDispName = .UncategorisedDocument.Name
                            _CatUnCatDelDocVersionNo = oDocument.GetUncategorisedDocumentVersion(.UncategorisedDocument.Path, False, False) + 1
                        End If



                        '3.2 Category Document Folder Path
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            _CategoryDocumentFolderPath = Supporting.PathsAndFolders.GetRecyclePath(DMSRootPath) & "\" & .CategorisedDocument.Category & "\" & .PatientID & "\" & .Year & "\" & .Month
                        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            _CategoryDocumentFolderPath = Supporting.PathsAndFolders.GetRecyclePath(DMSRootPath) & "\" & Supporting.PathsAndFolders.ScanFolder & "\" & .PatientID & "\" & .Year & "\" & .Month
                        End If
                        If Directory.Exists(_CategoryDocumentFolderPath) = False Then
                            MkDir(_CategoryDocumentFolderPath)
                        End If

                        '3.3 Destination Document Exits or not
                        _DestinationDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentFileNameOrID(oParameters.PatientID, False) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_DestinationDocument) = True
                            _DestinationDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentFileNameOrID(oParameters.PatientID, False) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        Dim oDestFileInfo As FileInfo = New FileInfo(_DestinationDocument)
                        _DestinationDocumentName = Replace(oDestFileInfo.Name, oDestFileInfo.Extension, "")
                        oDestFileInfo = Nothing

                        '4. Make a copy of existing uncategory document to process on it
                        _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentUncat) = True
                            _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentCat) = True
                            _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            File.Copy(.CategorisedDocument.Path, _ProcessDocumentUncat, True) : File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                            File.Copy(.CategorisedDocument.Path, _ProcessDocumentCat, True) : File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            File.Copy(.UncategorisedDocument.Path, _ProcessDocumentUncat, True) : File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                            File.Copy(.UncategorisedDocument.Path, _ProcessDocumentCat, True) : File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        End If

                        '5. Check All pages selected or not
                        If .SelectedPages.Count = .PageCount Then
                            Kill(_ProcessDocumentUncat)
                        End If

                        'Report Percentage
                        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '6. Create Uncategory File
                        oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                        oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"
                        If File.Exists(_ProcessDocumentUncat) = True Then
                            For i = .PageCount To 1 Step -1
                                If oSupport.IsItemExists(i, .SelectedPages) = True Then
                                    oProcessor.DeletePages(_ProcessDocumentUncat, _ProcessDocumentUncat, (i) - 1, (i) - 1)
                                End If
                                'Percentage
                                If i Mod _PercentageFactor = 0 Then
                                    _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                End If
                            Next
                            oProcessor.Optimize(_ProcessDocumentUncat, _ProcessDocumentUncat)
                        End If
                        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)


                        '7. Create Category File
                        If .SelectedPages.Count <> .PageCount Then
                            If File.Exists(_ProcessDocumentCat) = True Then
                                For i = .PageCount To 1 Step -1
                                    If oSupport.IsItemExists(i, .UnselectedPages) = True Then
                                        oProcessor.DeletePages(_ProcessDocumentCat, _ProcessDocumentCat, (i) - 1, (i) - 1)
                                    End If
                                    'Report Percentage
                                    If i Mod _PercentageFactor = 0 Then
                                        _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                    End If
                                Next
                                oProcessor.Optimize(_ProcessDocumentCat, _ProcessDocumentCat)
                            End If
                        End If
                        _Percentage = 80 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '8 Send Document To There Location
                        '8.1 Make Uncategory & Category Document Names
                        _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendUncategoryDocument) = True
                            _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendCategoryDocument) = True
                            _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While

                        '8.2 Send Document to Category and Uncategory
                        File.Copy(_ProcessDocumentCat, _SendCategoryDocument) : File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal)
                        If .SelectedPages.Count <> .PageCount Then
                            File.Copy(_ProcessDocumentUncat, _SendUncategoryDocument) : File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal)
                        End If

                        '9. Make Chnages in Uncategory Document
                        'If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                        File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Rename(_SendCategoryDocument, _DestinationDocument)
                        'Else
                        '    _DestDocPageCountBeforeMerge = oProcessor.GetPageCount(_DestinationDocument)
                        '    oProcessor.Merge(_DestinationDocument, _SendCategoryDocument, _DestinationDocument)
                        '    File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Kill(_SendCategoryDocument)
                        'End If
                        oProcessor = Nothing


                        '10 Send To Database
                        If Save_DeletePages(oParameters, _UncategoryDocumentFolderPath, _CategoryDocumentFolderPath, _DestinationDocumentDispName, _DestinationDocumentName, 0, _CatUnCatDelDocVersionNo) = True Then
                            '11. After Sucessfully process work with uncategory document
                            If .SelectedPages.Count = .PageCount Then
                                If File.Exists(_SendUncategoryDocument) = True Then
                                    File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Kill(_SendUncategoryDocument)
                                End If
                                If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                                    If File.Exists(.CategorisedDocument.Path) = True Then
                                        File.SetAttributes(.CategorisedDocument.Path, FileAttributes.Normal) : Kill(.CategorisedDocument.Path)
                                    End If
                                ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                                    If File.Exists(.UncategorisedDocument.Path) = True Then
                                        File.SetAttributes(.UncategorisedDocument.Path, FileAttributes.Normal) : Kill(.UncategorisedDocument.Path)
                                    End If
                                End If
                            Else
                                If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                                    If File.Exists(.CategorisedDocument.Path) = True Then
                                        File.SetAttributes(.CategorisedDocument.Path, FileAttributes.Normal) : Kill(.CategorisedDocument.Path)
                                    End If
                                    File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Rename(_SendUncategoryDocument, .CategorisedDocument.Path)
                                ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                                    If File.Exists(.UncategorisedDocument.Path) = True Then
                                        File.SetAttributes(.UncategorisedDocument.Path, FileAttributes.Normal) : Kill(.UncategorisedDocument.Path)
                                    End If
                                    File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Rename(_SendUncategoryDocument, .UncategorisedDocument.Path)
                                End If
                            End If
                        End If

                        '12. Uncategory Month Folder
                        Dim oMonthFolder As String = _UncategoryDocumentFolderPath
                        Dim oMonthDirectory As DirectoryInfo = New DirectoryInfo(_UncategoryDocumentFolderPath)
                        Dim oYearFolder As String = oMonthDirectory.Parent.FullName
                        Dim oYearDirectory As DirectoryInfo = New DirectoryInfo(oYearFolder)
                        Dim oPatientID As String = oYearDirectory.Parent.FullName
                        Dim oPatientDirectory As DirectoryInfo = New DirectoryInfo(oPatientID)

                        Dim oMonthFiles As FileInfo() = oMonthDirectory.GetFiles()
                        If oMonthFiles.Length = 0 Then
                            Directory.Delete(oMonthFolder, True)
                            Dim oYearFiles As DirectoryInfo() = oYearDirectory.GetDirectories
                            If oYearFiles.Length = 0 Then
                                Directory.Delete(oYearFolder, True)
                                Dim oPatientFiles As DirectoryInfo() = oPatientDirectory.GetDirectories()
                                If oPatientFiles.Length = 0 Then
                                    Directory.Delete(oPatientID, True)
                                End If
                                oPatientFiles = Nothing
                            End If
                            oYearFiles = Nothing
                        End If
                        oMonthFiles = Nothing


                        '13. Delete Temporary Process Files
                        If File.Exists(_ProcessDocumentCat) = True Then File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentUncat) = True Then File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentCat) = True Then Kill(_ProcessDocumentCat)
                        If File.Exists(_ProcessDocumentUncat) = True Then Kill(_ProcessDocumentUncat)

                        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _Success = True
                    End With

                    Return _Success
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = oError.Message
                    DeletePages = Nothing
                    Exit Function
                Finally
                    'oPathFolders = Nothing
                    oSupport = Nothing
                    oProcessor = Nothing

                    _DestinationDocument = Nothing
                    i = Nothing
                End Try
            End Function
            Public Function checkFileopen(ByVal strPath As String) As Boolean
                Dim ofile As System.IO.FileStream = Nothing
                Try
                    ofile = IO.File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.None)
                    Return False
                Catch ex As IO.IOException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return True
                Finally
                    If Not ofile Is Nothing Then
                        ofile.Close()
                    End If

                End Try
            End Function

            Public Function MoveFromCategory(ByVal oParameters As gloStream.gloDMS.Supporting.CategoryProcessParameters, ByVal DocumentDispName As String, Optional ByVal NewDocumentName As String = "", Optional ByVal MergeDocumentPath As String = "") As Boolean
                ' Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oDocument As New gloStream.gloDMS.Document.Document

                Dim _Success As Boolean = False
                Dim _DestinationDocument As String = ""

                Dim _ProcessDocumentUncat As String = ""
                Dim _ProcessDocumentCat As String = ""

                Dim _UncategoryDocumentFolderPath As String = ""
                Dim _CategoryDocumentFolderPath As String = ""

                Dim _SendUncategoryDocument As String = ""
                Dim _SendCategoryDocument As String = ""

                Dim _PercentageFactor As Integer = 0, _Percentage As Integer = 0
                Dim _DestDocPageCountBeforeMerge As Integer = 0

                Dim _DestDocumentFileName As String = ""
                Dim _CategoryVersionNo As Integer = 0, _UncategoryVersionNo As Integer = 0
                Dim i As Integer
                Dim _ErrorLog As String = "---Move From Category---" & Date.Now

                Try
                    With oParameters

                        If checkFileopen(.Document.Path) = True Then
                            MessageBox.Show("This file is being used by another user,you cannot move it now", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''''Temprary retrun sucess because showing other Messagebox. 
                            _Success = True
                            Return _Success
                            Exit Function
                        End If

                        '0 Percentage Factor
                        _PercentageFactor = Math.Round((.PageCount * 40) / 100)

                        '1. Check Uncategory Document Exists or not
                        If File.Exists(.Document.Path) = False Then
                            _ErrorMessage = "Document not exists in DMS system"
                            MoveFromCategory = Nothing
                            Exit Function
                        End If
                        '2. Necessary Fiels
                        If .PatientID = 0 Or .SelectedPages.Count = 0 Or .DestinationCategory = "" Then
                            _ErrorMessage = "Incorrect document information"
                            MoveFromCategory = Nothing
                            Exit Function
                        End If

                        '3. Destination Document
                        '3.1 Uncategory Document Folder Path
                        Dim oFileUncat As FileInfo = New FileInfo(.Document.Path)
                        _UncategoryDocumentFolderPath = oFileUncat.Directory.FullName
                        oFileUncat = Nothing
                        _UncategoryVersionNo = oParameters.Document.VersionNo + 1

                        _ErrorLog = _ErrorLog & vbCrLf & "Uncategory Document Folder Path : " & _UncategoryDocumentFolderPath

                        '3.2 Category Document Folder Path
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _CategoryDocumentFolderPath = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & Supporting.PathsAndFolders.SystemFolder & "\" & Supporting.PathsAndFolders.ContainerFolder & "\" & .DestinationCategory & "\" & .PatientID & "\" & .ShowDocumentYear & "\" & .DestinationMonth
                            If Directory.Exists(_CategoryDocumentFolderPath) = False Then
                                MkDir(_CategoryDocumentFolderPath)
                            End If
                            _ErrorLog = _ErrorLog & vbCrLf & "Category Document Folder Path New Event: " & _CategoryDocumentFolderPath
                        Else
                            Dim oFileCat As FileInfo = New FileInfo(MergeDocumentPath)
                            _CategoryDocumentFolderPath = oFileCat.Directory.FullName
                            oFileCat = Nothing
                            _ErrorLog = _ErrorLog & vbCrLf & "Category Document Folder Path Merge Event: " & _CategoryDocumentFolderPath
                        End If

                        '3.3 Destination Document Exits or not
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _DestinationDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentFileNameOrID(oParameters.PatientID, False) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                            If File.Exists(_DestinationDocument) = True Then
                                _ErrorMessage = "Document with same name already exists, please enter another name"
                                MoveFromCategory = Nothing
                                Exit Function
                            End If
                            If oDocument.FindDocument(NewDocumentName, oParameters.PatientID, oParameters.DestinationCategory, Supporting.enumDocumentType.CategorisedDocument) = True Then
                                _ErrorMessage = "Document with same name already exists, please enter another name"
                                MoveFromCategory = Nothing
                                Exit Function
                            End If
                            _ErrorLog = _ErrorLog & vbCrLf & "Destination Document Path New Event: " & _DestinationDocument
                        Else
                            _DestinationDocument = MergeDocumentPath
                            If File.Exists(_DestinationDocument) = False Then
                                _ErrorMessage = "Merge Document not exist in DMS system"
                                MoveFromCategory = Nothing
                                Exit Function
                            End If
                            _ErrorLog = _ErrorLog & vbCrLf & "Destination Document Path Merge Event: " & _DestinationDocument
                        End If

                        'Category Version No
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _CategoryVersionNo = 1
                        Else
                            _CategoryVersionNo = oDocument.GetCategorisedDocumentVersion(MergeDocumentPath, False, False) + 1
                        End If

                        Dim oFileDestDoc As FileInfo = New FileInfo(_DestinationDocument)
                        _DestDocumentFileName = Replace(oFileDestDoc.Name, oFileDestDoc.Extension, "")
                        _ErrorLog = _ErrorLog & vbCrLf & "Destination Document Name : " & _DestDocumentFileName

                        '4. Make a copy of existing uncategory document to process on it
                        _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentUncat) = True
                            _ProcessDocumentUncat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Uncat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Document Uncategory : " & _ProcessDocumentUncat

                        _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_ProcessDocumentCat) = True
                            _ProcessDocumentCat = Supporting.PathsAndFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "Cat." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Document Category : " & _ProcessDocumentCat

                        File.Copy(.Document.Path, _ProcessDocumentUncat, True) : File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                        _ErrorLog = _ErrorLog & vbCrLf & "Copy Process Document Uncategory File"
                        File.Copy(.Document.Path, _ProcessDocumentCat, True) : File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        _ErrorLog = _ErrorLog & vbCrLf & "Copy Process Document Category File"

                        '5. Check All pages selected or not
                        If .SelectedPages.Count = .PageCount Then
                            Kill(_ProcessDocumentUncat)
                            _ErrorLog = _ErrorLog & vbCrLf & "All Pages Selected so delete Process Document Uncategory File"
                        End If

                        'Report Percentage
                        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                        '6. Create Uncategory File
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Uncategory File - Start"
                        oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                        oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"
                        If File.Exists(_ProcessDocumentUncat) = True Then
                            For i = .PageCount To 1 Step -1
                                If oSupport.IsItemExists(i, .SelectedPages) = True Then
                                    oProcessor.DeletePages(_ProcessDocumentUncat, _ProcessDocumentUncat, (i) - 1, (i) - 1)
                                End If
                                'Percentage
                                If i Mod _PercentageFactor = 0 Then
                                    _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                End If
                            Next
                            oProcessor.Optimize(_ProcessDocumentUncat, _ProcessDocumentUncat)
                        End If
                        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Uncategory File - Finish"

                        '7. Create Category File
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Category File - Start"
                        If .SelectedPages.Count <> .PageCount Then
                            If File.Exists(_ProcessDocumentCat) = True Then
                                For i = .PageCount To 1 Step -1
                                    If oSupport.IsItemExists(i, .UnselectedPages) = True Then
                                        oProcessor.DeletePages(_ProcessDocumentCat, _ProcessDocumentCat, (i) - 1, (i) - 1)
                                    End If
                                    'Report Percentage
                                    If i Mod _PercentageFactor = 0 Then
                                        _Percentage = _Percentage + 1 : Application.DoEvents() : If _Percentage < 100 Then RaiseEvent ReportPercentage(_Percentage)
                                    End If
                                Next
                                oProcessor.Optimize(_ProcessDocumentCat, _ProcessDocumentCat)
                            End If
                        End If
                        _Percentage = 80 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _ErrorLog = _ErrorLog & vbCrLf & "Process Category File - Finish"

                        '8 Send Document To There Location
                        '8.1 Make Uncategory & Category Document Names
                        _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendUncategoryDocument) = True
                            _SendUncategoryDocument = _UncategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _ErrorLog = _ErrorLog & vbCrLf & "Send Uncategory Document : " & _SendUncategoryDocument

                        _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        While File.Exists(_SendCategoryDocument) = True
                            _SendCategoryDocument = _CategoryDocumentFolderPath & "\" & oSupport.NewDocumentName(oParameters.PatientID, "", Supporting.enumDocumentType.None) & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                        End While
                        _ErrorLog = _ErrorLog & vbCrLf & "Send Category Document : " & _SendCategoryDocument

                        '8.2 Send Document to Category and Uncategory

                        File.Copy(_ProcessDocumentCat, _SendCategoryDocument) : File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal)
                        _ErrorLog = _ErrorLog & vbCrLf & "Copy Send Category Document: "
                        If .SelectedPages.Count <> .PageCount Then
                            File.Copy(_ProcessDocumentUncat, _SendUncategoryDocument) : File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal)
                        End If
                        _ErrorLog = _ErrorLog & vbCrLf & "Copy Send Uncategory Document: "

                        '9. Make Chnages in Uncategory Document
                        If .CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                            _ErrorLog = _ErrorLog & vbCrLf & "Change Send Categoey to Destination Document - Start [New Event] "
                            File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Rename(_SendCategoryDocument, _DestinationDocument)
                            _ErrorLog = _ErrorLog & vbCrLf & "Change Send Categoey to Destination Document - Finish [New Event] "
                        Else
                            _ErrorLog = _ErrorLog & vbCrLf & "Change Send Categoey to Destination Document - Start [Merge Event] "
                            _DestDocPageCountBeforeMerge = oProcessor.GetPageCount(_DestinationDocument)
                            oProcessor.Merge(_DestinationDocument, _SendCategoryDocument, _DestinationDocument)
                            File.SetAttributes(_SendCategoryDocument, FileAttributes.Normal) : Kill(_SendCategoryDocument)
                            _ErrorLog = _ErrorLog & vbCrLf & "Change Send Categoey to Destination Document - Finish [Merge Event] "
                        End If
                        oProcessor = Nothing

                        '10 Send To Database
                        _ErrorLog = _ErrorLog & vbCrLf & "Save to database"
                        If Save_MoveFromCategory(oParameters, _UncategoryDocumentFolderPath, _CategoryDocumentFolderPath, DocumentDispName, _DestDocumentFileName, _UncategoryVersionNo, _CategoryVersionNo, NewDocumentName, MergeDocumentPath, _DestDocPageCountBeforeMerge) = True Then
                            '11. After Sucessfully process work with uncategory document
                            If .SelectedPages.Count = .PageCount Then
                                If File.Exists(_SendUncategoryDocument) = True Then
                                    File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Kill(_SendUncategoryDocument)
                                End If
                                If File.Exists(.Document.Path) = True Then
                                    File.SetAttributes(.Document.Path, FileAttributes.Normal) : Kill(.Document.Path)
                                End If
                            Else
                                If File.Exists(.Document.Path) = True Then
                                    File.SetAttributes(.Document.Path, FileAttributes.Normal) : Kill(.Document.Path)
                                End If
                                File.SetAttributes(_SendUncategoryDocument, FileAttributes.Normal) : Rename(_SendUncategoryDocument, .Document.Path)
                            End If
                        End If

                        '12. Uncategory Month Folder
                        Dim oMonthFolder As String = _UncategoryDocumentFolderPath
                        Dim oMonthDirectory As DirectoryInfo = New DirectoryInfo(_UncategoryDocumentFolderPath)
                        Dim oYearFolder As String = oMonthDirectory.Parent.FullName
                        Dim oYearDirectory As DirectoryInfo = New DirectoryInfo(oYearFolder)
                        Dim oPatientID As String = oYearDirectory.Parent.FullName
                        Dim oPatientDirectory As DirectoryInfo = New DirectoryInfo(oPatientID)

                        Dim oMonthFiles As FileInfo() = oMonthDirectory.GetFiles()
                        If oMonthFiles.Length = 0 Then
                            Directory.Delete(oMonthFolder, True)
                            Dim oYearFiles As DirectoryInfo() = oYearDirectory.GetDirectories
                            If oYearFiles.Length = 0 Then
                                Directory.Delete(oYearFolder, True)
                                Dim oPatientFiles As DirectoryInfo() = oPatientDirectory.GetDirectories()
                                If oPatientFiles.Length = 0 Then
                                    Directory.Delete(oPatientID, True)
                                End If
                                oPatientFiles = Nothing
                            End If
                            oYearFiles = Nothing
                        End If
                        oMonthFiles = Nothing


                        '13. Delete Temporary Process Files
                        If File.Exists(_ProcessDocumentCat) = True Then File.SetAttributes(_ProcessDocumentCat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentUncat) = True Then File.SetAttributes(_ProcessDocumentUncat, FileAttributes.Normal)
                        If File.Exists(_ProcessDocumentCat) = True Then Kill(_ProcessDocumentCat)
                        If File.Exists(_ProcessDocumentUncat) = True Then Kill(_ProcessDocumentUncat)

                        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                        _Success = True
                    End With

                    Return _Success
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Dim oLogFile As String = ""
                    oLogFile = Application.StartupPath & "\" & Replace(Replace(Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt"), "/", " "), ":", " ") & ".dms"
                    While System.IO.File.Exists(oLogFile) = True
                        oLogFile = Application.StartupPath & "\" & Replace(Replace(Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt"), "/", " "), ":", " ") & ".dms"
                    End While
                    Dim oFile As System.IO.StreamWriter = System.IO.File.CreateText(oLogFile)
                    oFile.WriteLine(_ErrorLog)
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                    Application.DoEvents()
                    RaiseEvent ReportPercentage(0)
                    _ErrorMessage = oError.Message
                    MoveFromCategory = Nothing
                    Exit Function
                Finally


                    ' oPathFolders = Nothing
                    oSupport = Nothing
                    oProcessor = Nothing
                    oDocument = Nothing

                    _DestinationDocument = Nothing
                    i = Nothing
                End Try
            End Function

            '---DMS External Functionality---

            Public Function SendToDMS(ByVal PatientID As Long, ByVal Category As String, ByVal DocumentPath As String) As Boolean
                '//REMARK -> it is commented by vinayak for some reason, when back please clear with sachin//

                'Dim oParameters As New gloStream.gloDMS.Supporting.ImportProcessParameters
                'Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                'Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                'Dim oDocument As New gloStream.gloDMS.Document.Document

                'Dim _Success As Boolean = False
                'Dim _DestinationDocumentFolderPath As String = ""
                'Dim _DestinationDocument As String = ""
                'Dim _ProcessDocument As String = ""

                'Dim _PercentageFactor As Integer = 0, _Percentage As Integer = 0
                'Dim i As Integer, nFileCntr As Integer = 0

                ''Optional ByVal Month As String = MonthName(Month(Date.Now)), Optional ByVal Year As String = Date.Now.Year
                'With oParameters
                '    .Category = Category
                '    .DocumentName = oSupport.NewDocumentName(PatientID )
                '    .Documents.Add(DocumentPath)
                '    .DocumentType = Supporting.enumDocumentType.CategorisedDocument
                '    .Month = MonthName(System.DateTime.Now.Month)
                '    .Year = Date.Now.Year
                '    .PatientID = PatientID
                'End With

                'Try
                '    With oParameters
                '        '0 Percentage Factor
                '        _PercentageFactor = (.Documents.Count * 40) \ 100 ' its change every where to reduce time

                '        '1. Category
                '        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                '            If Trim(.Category) = "" Then
                '                _ErrorMessage = "Category not found"
                '                Exit Function
                '            End If
                '        End If

                '        '2. Necessary Fiels
                '        If .PatientID = 0 Or Trim(.Month) = "" Or Trim(.Year) = "" Then
                '            _ErrorMessage = "Incorrect document information"
                '            Exit Function
                '        End If

                '        '3. Destination Document
                '        '3.2 Category Document Folder Path
                '        If .DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                '            _DestinationDocumentFolderPath = oPathFolders.GetDMSPath(DMSRootPath) & oPathFolders.SystemFolder & "\" & oPathFolders.ContainerFolder & "\" & .Category & "\" & .PatientID & "\" & .Year & "\" & .Month
                '            If Directory.Exists(_DestinationDocumentFolderPath) = False Then
                '                MkDir(_DestinationDocumentFolderPath)
                '            End If
                '        ElseIf .DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                '            _DestinationDocumentFolderPath = oPathFolders.GetDMSPath(DMSRootPath) & oPathFolders.SystemFolder & "\" & oPathFolders.ScanFolder & "\" & .PatientID & "\" & .Year & "\" & .Month
                '            If Directory.Exists(_DestinationDocumentFolderPath) = False Then
                '                MkDir(_DestinationDocumentFolderPath)
                '            End If
                '        End If

                '        '3.3 Destination Document Exits or not
                '        _DestinationDocument = _DestinationDocumentFolderPath & "\" & .DocumentName & "." & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                '        While File.Exists(_DestinationDocument) = True
                '            .DocumentName = oSupport.NewDocumentName(PatientID)
                '            _DestinationDocument = _DestinationDocumentFolderPath & "\" & .DocumentName & "." & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                '        End While

                '        'If File.Exists(_DestinationDocument) = True Then
                '        '    _ErrorMessage = "Document with same name already exists, please enter another name"
                '        '    Exit Function
                '        'End If

                '        If oDocument.FindDocument(.DocumentName, oParameters.PatientID) = True Then
                '            MessageBox.Show("Document with same name already exists, please enter another name")
                '            Exit Function
                '        End If

                '        '4. Process Document
                '        _ProcessDocument = oPathFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID) & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                '        While File.Exists(_ProcessDocument) = True
                '            _ProcessDocument = oPathFolders.TempProcessFolder & "\" & oSupport.NewDocumentName(oParameters.PatientID) & oSupport.ExtensionAsString(Supporting.enumDocumentExtension.PDF)
                '        End While

                '        'Report Percentage
                '        _Percentage = 10 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)

                '        '6. Create Import File
                '        oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                '        oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"

                '        If .Documents.Count = 1 Then ' Direcly copy if single file
                '            File.Copy(.Documents(1), _ProcessDocument, True)
                '        ElseIf .Documents.Count = 2 Then ' Merge 2 files
                '            oProcessor.Merge(.Documents(1), .Documents(2), _ProcessDocument)
                '        ElseIf .Documents.Count > 2 Then ' Merge 2 files
                '            oProcessor.Merge(.Documents(1), .Documents(2), _ProcessDocument)
                '            For i = 3 To .Documents.Count
                '                oProcessor.Merge(_ProcessDocument, .Documents(i), _ProcessDocument)
                '            Next
                '        End If

                '        oProcessor.Optimize(_ProcessDocument, _ProcessDocument)
                '        _Percentage = 40 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)


                '        '8.2 Send Document to Destination
                '        File.Copy(_ProcessDocument, _DestinationDocument) : File.SetAttributes(_DestinationDocument, FileAttributes.Normal)

                '        oProcessor = Nothing

                '        '10 Send To Database
                '        If Save_SendToGeneralBin(oParameters, _DestinationDocument) = True Then
                '            Application.DoEvents() : RaiseEvent ReportImportDocument(_DestinationDocument)
                '        End If

                '        '13. Delete Temporary Process Files
                '        If File.Exists(_ProcessDocument) = True Then File.SetAttributes(_ProcessDocument, FileAttributes.Normal)
                '        If File.Exists(_ProcessDocument) = True Then Kill(_ProcessDocument)

                '        _Percentage = 100 : Application.DoEvents() : RaiseEvent ReportPercentage(_Percentage)
                '        _Success = True
                '    End With

                '    Return _Success
                'Catch oError As Exception
                '    Application.DoEvents()
                '    RaiseEvent ReportPercentage(0)
                '    _ErrorMessage = oError.Message
                '    Exit Function
                'Finally
                '    oPathFolders = Nothing
                '    oSupport = Nothing
                '    oProcessor = Nothing
                '    oDocument = Nothing

                '    _DestinationDocument = Nothing
                '    i = Nothing
                'End Try
                Return Nothing
            End Function


            Public Function MoveDMS(ByVal SourceStorageMachine As String, ByVal SourceDriveName As String, ByVal SourceStoragePath As String, ByVal DestinationStorageMachine As String, ByVal DestinationDriveName As String, ByVal DestinationStoragePath As String, ByVal DeleteSourceDocument As Boolean)
                Return Nothing
            End Function

            Public Function RenameDMSSource(ByVal OldStorageMachine As String, ByVal OldDriveName As String, ByVal OldStoragePath As String, ByVal NewStorageMachine As String, ByVal NewDriveName As String, ByVal NewStoragePath As String)
                Return Nothing
            End Function

            Public Function SetDMSRootPath(ByVal Path As String) As Boolean
                Return Nothing
            End Function

            Public Function GetDMSRootPath() As String
                Return Nothing
            End Function

            '---Fill Procedures---

            Public Function UnCategorisedDocuments(ByVal PatientID As Long, ByVal Year As String, ByVal Month As String, Optional ByVal Archived As Boolean = False) As Document.UncategorisedDocuments
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDocument As New gloStream.gloDMS.Document.Document
                Dim oUncategorisedDocuments As New Document.UncategorisedDocuments
                Dim oUncategorisedDocument As Document.UncategorisedDocument
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Try

                    oDB.Connect(GetConnectionString)

                    'Parameters - Patient ID
                    oDB.DBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Parameters - Year
                    oDB.DBParameters.Add("@sYear", Year, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Month
                    oDB.DBParameters.Add("@sMonth", Month, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Archived
                    oDB.DBParameters.Add("@bitArchived", Archived, ParameterDirection.Input, SqlDbType.Bit)

                    oDataReader = oDB.ReadRecords("gsp_DMS_FillUncategorisedDocuments")

                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            oUncategorisedDocument = New Document.UncategorisedDocument
                            With oUncategorisedDocument
                                .FileName = Trim(oDataReader.Item("DocumentFileName") & "")
                                .Name = Trim(oDataReader.Item("DocumentName") & "")
                                .Extension = Trim(oDataReader.Item("Extension") & "")
                                .SourceMachine = Trim(oDataReader.Item("SourceMachine") & "")
                                .SystemFolder = Trim(oDataReader.Item("SystemFolder") & "")
                                .Container = Trim(oDataReader.Item("Container") & "")
                                .PatientID = CLng(oDataReader.Item("PatientID") & "")
                                .Year = Trim(oDataReader.Item("Year") & "")
                                .Month = Trim(oDataReader.Item("Month") & "")
                                .Format = Val(oDataReader.Item("DocumentFormat") & "")
                                .SourceBin = Val(oDataReader.Item("SourceBin") & "")
                                .Pages = Val(oDataReader.Item("Pages") & "")
                                .InUsed = oDataReader.Item("UsedStatus")
                                .UsedMachine = Trim(oDataReader.Item("UsedMachine") & "")
                                .UsedType = Val(oDataReader.Item("UsedType") & "")
                                .Type = Val(oDataReader.Item("DocumentType") & "")
                                .MachineID = CLng(oDataReader.Item("MachineID") & "")
                                .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                .ModifiedDateTime = oDataReader.Item("ModifyDateTime") & ""
                            End With
                            'Check Document Available or Not
                            If oDocument.IsExistInExplorer(DMSRootPath, oUncategorisedDocument) = True Then
                                oUncategorisedDocuments.Add(oUncategorisedDocument)
                            End If
                            oUncategorisedDocument = Nothing
                        End While
                    End If

                    oDB.Disconnect()
                    Return oUncategorisedDocuments
                Catch objError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    If oDB.ErrorMessage <> "" Then
                        _ErrorMessage = oDB.ErrorMessage
                    Else
                        _ErrorMessage = objError.Message
                    End If
                    UnCategorisedDocuments = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDocument = Nothing
                    oUncategorisedDocument = Nothing
                    oUncategorisedDocuments = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function UnCategorisedDocuments(ByVal PatientID As Long, Optional ByVal Archived As Boolean = False) As Document.UncategorisedDocuments
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDocument As New gloStream.gloDMS.Document.Document
                Dim oUncategorisedDocuments As New Document.UncategorisedDocuments
                Dim oUncategorisedDocument As Document.UncategorisedDocument
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Try

                    oDB.Connect(GetConnectionString)

                    'Parameters - Patient ID
                    oDB.DBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Parameters - Archived
                    oDB.DBParameters.Add("@bitArchived", Archived, ParameterDirection.Input, SqlDbType.Bit)

                    oDataReader = oDB.ReadRecords("gsp_DMS_FillAllUncategorisedDocuments")

                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            oUncategorisedDocument = New Document.UncategorisedDocument
                            With oUncategorisedDocument
                                .FileName = Trim(oDataReader.Item("DocumentFileName") & "")
                                .Name = Trim(oDataReader.Item("DocumentName") & "")
                                .Extension = Trim(oDataReader.Item("Extension") & "")
                                .SourceMachine = Trim(oDataReader.Item("SourceMachine") & "")
                                .SystemFolder = Trim(oDataReader.Item("SystemFolder") & "")
                                .Container = Trim(oDataReader.Item("Container") & "")
                                .PatientID = CLng(oDataReader.Item("PatientID") & "")
                                .Year = Trim(oDataReader.Item("Year") & "")
                                .Month = Trim(oDataReader.Item("Month") & "")
                                .Format = Val(oDataReader.Item("DocumentFormat") & "")
                                .SourceBin = Val(oDataReader.Item("SourceBin") & "")
                                .Pages = Val(oDataReader.Item("Pages") & "")
                                .InUsed = oDataReader.Item("UsedStatus")
                                .UsedMachine = Trim(oDataReader.Item("UsedMachine") & "")
                                .UsedType = Val(oDataReader.Item("UsedType") & "")
                                .Type = Val(oDataReader.Item("DocumentType") & "")
                                .MachineID = CLng(oDataReader.Item("MachineID") & "")
                                .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                .ModifiedDateTime = oDataReader.Item("ModifyDateTime") & ""
                            End With
                            'Check Document Available or Not
                            If oDocument.IsExistInExplorer(DMSRootPath, oUncategorisedDocument) = True Then
                                oUncategorisedDocuments.Add(oUncategorisedDocument)
                            End If
                            oUncategorisedDocument = Nothing
                        End While
                    End If

                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return oUncategorisedDocuments
                Catch objError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    If oDB.ErrorMessage <> "" Then
                        _ErrorMessage = oDB.ErrorMessage
                    Else
                        _ErrorMessage = objError.Message
                    End If
                    UnCategorisedDocuments = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDocument = Nothing
                    oUncategorisedDocument = Nothing
                    oUncategorisedDocuments = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function CategorisedDocuments(ByVal PatientID As Long, ByVal Category As String, ByVal Year As String, ByVal Month As String, Optional ByVal Archived As Boolean = False) As Document.CategorisedDocuments
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDocument As New gloStream.gloDMS.Document.Document
                Dim oCategorisedDocuments As New Document.CategorisedDocuments
                Dim oCategorisedDocument As Document.CategorisedDocument
                Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim _DocumentID As Long, _DoucmentFileName As Long

                Try

                    oDB.Connect(GetConnectionString)

                    'Parameters - Patient ID
                    oDB.DBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Parameters - Category
                    oDB.DBParameters.Add("@sCategory", Category, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Year
                    oDB.DBParameters.Add("@sYear", Year, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Month
                    oDB.DBParameters.Add("@sMonth", Month, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Archived
                    oDB.DBParameters.Add("@bitArchived", Archived, ParameterDirection.Input, SqlDbType.Bit)

                    oDataReader = oDB.ReadRecords("gsp_DMS_FillCategorisedDocuments")

                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            oCategorisedDocument = New Document.CategorisedDocument
                            With oCategorisedDocument
                                _DocumentID = oDataReader.Item("DocumentID")
                                _DoucmentFileName = oDataReader.Item("DocumentFileName")

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
                                .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                .ModifiedDateTime = oDataReader.Item("ModifyDateTime") & ""
                                If Not IsDBNull(oDataReader.Item("IsReviewed")) Then
                                    .IsReviwed = oDataReader.Item("IsReviewed")
                                End If
                                .ReviwedDetails = GetDocumentReviwedDetails(_DocumentID, _DoucmentFileName)
                            End With
                            'Check Document Available or Not
                            If oDocument.IsExistInExplorer(DMSRootPath, oCategorisedDocument) = True Then
                                oCategorisedDocuments.Add(oCategorisedDocument)
                            End If
                            oCategorisedDocument = Nothing
                        End While
                    End If

                    oDB.Disconnect()
                    Return oCategorisedDocuments
                Catch objError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    If oDB.ErrorMessage <> "" Then
                        _ErrorMessage = oDB.ErrorMessage
                    Else
                        _ErrorMessage = objError.Message
                    End If
                    CategorisedDocuments = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDocument = Nothing
                    oCategorisedDocument = Nothing
                    oCategorisedDocuments = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function CategorisedDocuments(ByVal PatientID As Long, ByVal Category As String, Optional ByVal Archived As Boolean = False) As Document.CategorisedDocuments
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDocument As New gloStream.gloDMS.Document.Document
                Dim oCategorisedDocuments As New Document.CategorisedDocuments
                Dim oCategorisedDocument As Document.CategorisedDocument
                Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim _DocumentID As Long, _DoucmentFileName As Long

                Try

                    oDB.Connect(GetConnectionString)

                    'Parameters - Patient ID
                    oDB.DBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Parameters - Category 
                    oDB.DBParameters.Add("@sCategory", Category, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Archived
                    oDB.DBParameters.Add("@bitArchived", Archived, ParameterDirection.Input, SqlDbType.Bit)


                    oDataReader = oDB.ReadRecords("gsp_DMS_FillAllCategorisedDocuments")

                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            oCategorisedDocument = New Document.CategorisedDocument
                            With oCategorisedDocument
                                _DocumentID = oDataReader.Item("DocumentID")
                                _DoucmentFileName = oDataReader.Item("DocumentFileName")

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
                                .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                .ModifiedDateTime = oDataReader.Item("ModifyDateTime") & ""
                                If Not IsDBNull(oDataReader.Item("IsReviewed")) Then
                                    .IsReviwed = oDataReader.Item("IsReviewed")
                                End If
                                .ReviwedDetails = GetDocumentReviwedDetails(_DocumentID, _DoucmentFileName)
                            End With
                            'Check Document Available or Not
                            If oDocument.IsExistInExplorer(DMSRootPath, oCategorisedDocument) = True Then
                                oCategorisedDocuments.Add(oCategorisedDocument)
                            End If
                            oCategorisedDocument = Nothing
                        End While
                    End If

                    oDB.Disconnect()
                    Return oCategorisedDocuments
                Catch objError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    If oDB.ErrorMessage <> "" Then
                        _ErrorMessage = oDB.ErrorMessage
                    Else
                        _ErrorMessage = objError.Message
                    End If
                    CategorisedDocuments = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDocument = Nothing
                    oCategorisedDocument = Nothing
                    oCategorisedDocuments = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function DocumentMonths(ByVal PatientID As Long, ByVal Category As String, ByVal Year As String, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType, Optional ByVal Archived As Boolean = False) As Collection
                Dim _TempMonths As New Collection
                Dim _Months As New Collection
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader

                Try
                    oDB.Connect(GetConnectionString)
                    'Parameters - Patient ID
                    oDB.DBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Parameters - Category
                    oDB.DBParameters.Add("@sCategory", Category, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Year
                    oDB.DBParameters.Add("@sYear", Year, ParameterDirection.Input, SqlDbType.VarChar)
                    'Parameters - Document Type
                    oDB.DBParameters.Add("@nDocumentType", DocumentType, ParameterDirection.Input, SqlDbType.Int)
                    'Parameters - Archived
                    oDB.DBParameters.Add("@bitArchived", Archived, ParameterDirection.Input, SqlDbType.Bit)

                    oDataReader = oDB.ReadRecords("gsp_DMS_FillDocumentMonths")

                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            If Trim(oDataReader.Item(0) & "") <> "" Then
                                _TempMonths.Add(Trim(oDataReader.Item(0) & ""))
                            End If
                        End While
                    End If

                    oDB.Disconnect()

                    'Sort Months
                    Dim nMonths As Integer, nCounter As Integer
                    Dim sMonth As String
                    If _TempMonths.Count > 0 Then
                        For nMonths = 1 To 12
                            sMonth = ""
                            sMonth = MonthName(nMonths)
                            If Trim(sMonth) <> "" Then
                                For nCounter = 1 To _TempMonths.Count
                                    If UCase(_TempMonths(nCounter)) = UCase(sMonth) Then
                                        _Months.Add(sMonth)
                                    End If
                                Next
                            End If
                        Next
                    End If
                    Return _Months
                Catch objError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = objError.Message
                    DocumentMonths = Nothing
                    Exit Function
                Finally
                    _TempMonths = Nothing
                    _Months = Nothing
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function GetDocumentReviwedDetails(ByVal DocumentID As Long, ByVal DocumentFileName As Long) As gloStream.gloDMS.Document.ReviwedDetails
                Dim _SQLQuery As String
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim oResult As New gloStream.gloDMS.Document.ReviwedDetails

                _SQLQuery = "SELECT DMS_ReviwedDetail.ReviwedBy, DMS_ReviwedDetail.ReviwedDateTime, DMS_ReviwedDetail.Comments, " _
                & " User_MST.sLoginName FROM User_MST RIGHT OUTER JOIN DMS_ReviwedDetail ON User_MST.nUserID = DMS_ReviwedDetail.ReviwedBy " _
                & " WHERE (DMS_ReviwedDetail.DocumentID = " & DocumentID & ") AND (DMS_ReviwedDetail.DocumentFileName = " & DocumentFileName & ") ORDER BY DMS_ReviwedDetail.ReviwedDateTime"

                oDB.Connect(GetConnectionString)

                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        With oResult
                            If Not (IsDBNull(oDataReader.Item("ReviwedBy")) And IsDBNull(oDataReader.Item("sLoginName"))) Then
                                Dim _tmpReviwedDateTime As DateTime = Now.Date
                                Dim _tmpReviwedComments As String = ""

                                If Not IsDBNull(oDataReader.Item("ReviwedDateTime")) Then
                                    _tmpReviwedDateTime = oDataReader.Item("ReviwedDateTime")
                                End If
                                If Not IsDBNull(oDataReader.Item("Comments")) Then
                                    _tmpReviwedComments = oDataReader.Item("Comments")
                                End If

                                .Add(oDataReader.Item("ReviwedBy"), oDataReader.Item("sLoginName"), _tmpReviwedDateTime, _tmpReviwedComments)
                            End If
                        End With
                    End While
                End If
                oDataReader.Close()
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing

                Return oResult
            End Function

            'Private FUNCTIONS

            '---SEND TO---
            Private Function Save_SendToGeneralBin(ByVal oParameters As gloStream.gloDMS.Supporting.ImportProcessParameters, ByVal ImportDocumentPath As String) As Boolean
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                '  Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oDocument As New gloStream.gloDMS.Document.Document
                Dim oCategorisedDocument As New gloStream.gloDMS.Document.CategorisedDocument
                Dim oUncategorisedDocument As New gloStream.gloDMS.Document.UncategorisedDocument
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _SaveRecord As Boolean = False
                Dim i As Integer = 0, nPageCount As Integer
                Dim _DocumentID As Long ' //Document Name Changes//
                'Dim _ModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity  ' //Document Name Changes//
                'Dim _SendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag  ' //Document Name Changes//

                Save_SendToGeneralBin = False
                Try
                    If File.Exists(ImportDocumentPath) = False Then
                        Return False
                        Exit Function
                    End If

                    'Insert New Record for New Document Command
                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Add("@PrefixID", GetPrefixTransactionID(oParameters.PatientID), ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@DocumentName", oParameters.DocumentName, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@DocumentFileName", oParameters.DocumentFileName, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Extension", Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@SourceMachine", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@SystemFolder", Supporting.PathsAndFolders.SystemFolder, ParameterDirection.Input, SqlDbType.VarChar)
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDB.DBParameters.Add("@Container", Supporting.PathsAndFolders.ContainerFolder, ParameterDirection.Input, SqlDbType.VarChar)
                    Else
                        oDB.DBParameters.Add("@Container", Supporting.PathsAndFolders.ScanFolder, ParameterDirection.Input, SqlDbType.VarChar)
                    End If
                    oDB.DBParameters.Add("@Category", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@PatientID", oParameters.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Year", oParameters.Year, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@Month", oParameters.Month, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@DocumentFormat", gloStream.gloDMS.Supporting.enumDocumentFormat.PDF, ParameterDirection.Input, SqlDbType.TinyInt)
                    oDB.DBParameters.Add("@SourceBin", gloStream.gloDMS.Supporting.enumDocumentSourceBin.ExternalPDF, ParameterDirection.Input, SqlDbType.TinyInt)
                    oDB.DBParameters.Add("@Pages", 0, ParameterDirection.Input, SqlDbType.Int)
                    oDB.DBParameters.Add("@ArchiveStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@ArchiveDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@UsedStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@UsedMachine", "", ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@UsedType", gloStream.gloDMS.Supporting.enumDocumentUsedType.None, ParameterDirection.Input, SqlDbType.TinyInt)
                    oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)

                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                    Else
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.UnCategorisedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                    End If
                    oDB.DBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@VersionNo", 1, ParameterDirection.Input, SqlDbType.Int)
                    oDB.DBParameters.Add("@ModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
                    oDB.DBParameters.Add("@IsReviewed", False, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.ExecuteNonQuery("gsp_DMS_NewDocument")
                    oDB.Disconnect()

                    'Update Pages
                    oProcessor = CreateObject("easyPdfSdk.PDFProcessor")
                    oProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"
                    nPageCount = oProcessor.GetPageCount(ImportDocumentPath)
                    oProcessor = Nothing

                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oCategorisedDocument = oDocument.GetCategorisedDocument(ImportDocumentPath, False, False)
                        _DocumentID = UpdatePages(nPageCount, oCategorisedDocument)
                        nDocumentID = _DocumentID
                        '//oCategorisedDocument = oDocument.GetCategorisedDocument(ImportDocumentPath)// Document Name Changes Aug 2006
                        '//UpdatePages(nPageCount, oCategorisedDocument)//

                    Else
                        oUncategorisedDocument = oDocument.GetUncategorisedDocument(ImportDocumentPath, False, False)
                        _DocumentID = UpdatePages(nPageCount, oUncategorisedDocument)
                        nDocumentID = _DocumentID
                        '//oUncategorisedDocument = oDocument.GetUncategorisedDocument(ImportDocumentPath)// Document Name Changes Aug 2006
                        '//UpdatePages(nPageCount, oUncategorisedDocument)//
                    End If

                    '// Document Name Changes - Start//
                    'Update Synchronization Information
                    If _DocumentID <> 0 Then
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None), ParameterDirection.Input, SqlDbType.VarChar)
                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            If oParameters.ScanDocument = True Then
                                oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromScan, ParameterDirection.Input, SqlDbType.TinyInt)
                            Else
                                oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromImport, ParameterDirection.Input, SqlDbType.TinyInt)
                            End If
                            oDB.DBParameters.Add("@OldCategory", "", ParameterDirection.Input, SqlDbType.VarChar)
                            oDB.DBParameters.Add("@NewCategory", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            If oParameters.ScanDocument = True Then
                                oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToGBFromScan, ParameterDirection.Input, SqlDbType.TinyInt)
                            Else
                                oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToGBFromImport, ParameterDirection.Input, SqlDbType.TinyInt)
                            End If
                            oDB.DBParameters.Add("@OldCategory", "", ParameterDirection.Input, SqlDbType.VarChar)
                            oDB.DBParameters.Add("@NewCategory", "", ParameterDirection.Input, SqlDbType.VarChar)
                        End If
                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.ReceivedFrom, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()
                        '// Document Name Changes - Finish//
                    End If

                    _SaveRecord = True
                    Return _SaveRecord
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    Exit Function
                Finally
                    oSupport = Nothing
                    ' oPathFolders = Nothing
                    oDB.Dispose()
                    oDB = Nothing
                    oDocument = Nothing
                    oCategorisedDocument = Nothing
                    oUncategorisedDocument = Nothing
                    _SaveRecord = Nothing
                    i = Nothing
                End Try
            End Function

            Private Function Save_SendToCategory(ByVal oParameters As gloStream.gloDMS.Supporting.UncategoryProcessParameters, ByVal UncategoryFolderPath As String, ByVal CategoryFolderPath As String, ByVal DocumentDispName As String, ByVal DestDocumentFileName As String, ByVal UncateVersionNo As Integer, ByVal CateVersionNo As Integer, Optional ByVal NewDocumentName As String = "", Optional ByVal MergeDocumentPath As String = "", Optional ByVal DestDocPageCountBeforeMerge As Integer = 0) As Boolean
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oCategoryDocument As New gloStream.gloDMS.Document.CategorisedDocument
                Dim oDocument As New gloStream.gloDMS.Document.Document
                '  Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim _SaveRecord As Boolean = False
                Dim _SQLQuery As String = ""
                Dim i As Integer = 0
                Dim _NoteStartPageID As Integer = 0
                Dim _Note As String = ""
                Dim _UncategoryPageCount As Integer = 0, _CategoryPageCount As Integer = 0
                Dim _DestDocID As Long, _SourceDocID As Long
                Dim _SynchDateTime As String = ""
                'Dim _ModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity
                'Dim _SendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag

                Save_SendToCategory = False
                Try
                    'Insert New Record for New Document Command
                    If oParameters.CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                        oDB.Connect(GetConnectionString)

                        oDB.DBParameters.Add("@PrefixID", GetPrefixTransactionID(oParameters.PatientID), ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@DocumentName", DocumentDispName, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@DocumentFileName", DestDocumentFileName, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Extension", Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@SourceMachine", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@SystemFolder", Supporting.PathsAndFolders.SystemFolder, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Container", Supporting.PathsAndFolders.ContainerFolder, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Category", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@PatientID", oParameters.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Year", oParameters.ShowDocumentYear, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Month", oParameters.DestinationMonth, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@DocumentFormat", gloStream.gloDMS.Supporting.enumDocumentFormat.PDF, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SourceBin", gloStream.gloDMS.Supporting.enumDocumentSourceBin.GeneralBin, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@Pages", 0, ParameterDirection.Input, SqlDbType.Int)
                        oDB.DBParameters.Add("@ArchiveStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ArchiveDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@UsedStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@UsedMachine", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@UsedType", gloStream.gloDMS.Supporting.enumDocumentUsedType.None, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@VersionNo", CateVersionNo, ParameterDirection.Input, SqlDbType.Int)
                        oDB.DBParameters.Add("@ModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
                        oDB.DBParameters.Add("@IsReviewed", False, ParameterDirection.Input, SqlDbType.Bit)

                        oDB.ExecuteNonQuery("gsp_DMS_NewDocument")
                        oDB.Disconnect()
                    End If

                    'Transfer Notes from Uncategory Document to New Document
                    If oParameters.CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                        oCategoryDocument = oDocument.GetCategorisedDocument(CategoryFolderPath & "\" & DestDocumentFileName & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), False, False)
                        '//oCategoryDocument = oDocument.GetCategorisedDocument(CategoryFolderPath & "\" & NewDocumentName & "." & oSupporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF))// Document Name Changes Aug 2006
                        _NoteStartPageID = 1
                    Else
                        oCategoryDocument = oDocument.GetCategorisedDocument(MergeDocumentPath, False, False)
                        '//oCategoryDocument = oDocument.GetCategorisedDocument(MergeDocumentPath)// Document Name Changes Aug 2006
                        _NoteStartPageID = DestDocPageCountBeforeMerge + 1
                    End If

                    'Update Version in Destination Doucment
                    If oParameters.CommandEventType <> mdlgloDMS.MenuEventType.NewDocument Then
                        oDocument.SetCategorisedDocumentVersionAndModifyDateTime(MergeDocumentPath, CateVersionNo, False)
                    End If
                    'Update Version in Source Uncategory Document
                    oDocument.SetUncategorisedDocumentVersionAndModifyDateTime(oParameters.Document.Path, UncateVersionNo, False)

                    If Not oCategoryDocument Is Nothing Then
                        For i = 1 To oParameters.SelectedPages.Count
                            _Note = Trim(oDocument.ViewNote(oParameters.SelectedPages(i), oParameters.Document))
                            If _Note <> "" Then
                                oDocument.AddNote(_Note, _NoteStartPageID, oCategoryDocument)
                            End If
                            _NoteStartPageID = _NoteStartPageID + 1
                        Next
                    End If

                    'Delete Note from Uncategory Document
                    For i = 1 To oParameters.SelectedPages.Count
                        oDocument.DeleteNote(oParameters.SelectedPages(i), oParameters.Document)
                    Next

                    'Shift Notes of uncategory document
                    Dim _StartNote As Integer = 0, _EndNote As Integer = oParameters.PageCount
                    If oParameters.SelectedPages.Count > 0 Then
                        _StartNote = oParameters.SelectedPages(1)
                    End If
                    For i = _StartNote To _EndNote
                        _Note = Trim(oDocument.ViewNote(i, oParameters.Document))
                        If _Note <> "" Then
                            oDocument.AddNote(_Note, i - PagesBeforeCurrentPage(oParameters.SelectedPages, i), oParameters.Document)
                            oDocument.DeleteNote(i, oParameters.Document)
                        End If
                    Next

                    'Update Pages
                    _UncategoryPageCount = oParameters.PageCount - oParameters.SelectedPages.Count
                    _CategoryPageCount = DestDocPageCountBeforeMerge + oParameters.SelectedPages.Count
                    _DestDocID = UpdatePages(_CategoryPageCount, oCategoryDocument)
                    _SourceDocID = UpdatePages(_UncategoryPageCount, oParameters.Document)

                    'Delete Uncategory Document if All Pages are selected
                    If oParameters.SelectedPages.Count = oParameters.PageCount Then
                        DeleteDocument(oParameters.Document)
                    End If

                    '//Document Changes - Start"
                    'Synchronization Infromation Add
                    If _DestDocID <> 0 AndAlso _SourceDocID <> 0 Then
                        _SynchDateTime = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)
                        'Add Record to Dest Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)

                        If NewDocumentName = "" Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromGBInExtDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        Else
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromGBInNewDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If
                        oDB.DBParameters.Add("@OldCategory", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.ReceivedFrom, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()

                        'Add Record to Source Document

                        'Modify in Source Master Document
                        oDB.ExecuteQueryNonQuery("UPDATE DMS_MST SET Modified = " & True & ", Synchronized = " & False & " WHERE DocumentID = " & _SourceDocID & "")

                        'Modify in Source Detail Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)

                        If NewDocumentName = "" Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToNewDocumentInCatFormGB, ParameterDirection.Input, SqlDbType.TinyInt)
                        Else
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToExtDocumentInCatFormGB, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If
                        oDB.DBParameters.Add("@OldCategory", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.SendTo, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()
                        '// Document Name Changes - Finish//

                    End If
                    '//Document Changes - Finish"


                    _SaveRecord = True
                    Return _SaveRecord
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    Exit Function
                Finally
                    oSupport = Nothing
                    oCategoryDocument = Nothing
                    oDocument = Nothing
                    ' oPathFolders = Nothing
                    oDB.Dispose()
                    oDB = Nothing
                    _SaveRecord = Nothing
                    _SQLQuery = Nothing
                    i = Nothing
                    _Note = Nothing
                End Try
            End Function

            Private Function Save_MoveFromCategory(ByVal oParameters As gloStream.gloDMS.Supporting.CategoryProcessParameters, ByVal UncategoryFolderPath As String, ByVal CategoryFolderPath As String, ByVal DocumentDispName As String, ByVal DestDocumentFileName As String, ByVal UncateVersionNo As Integer, ByVal CateVersionNo As Integer, Optional ByVal NewDocumentName As String = "", Optional ByVal MergeDocumentPath As String = "", Optional ByVal DestDocPageCountBeforeMerge As Integer = 0) As Boolean
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oCategoryDocument As New gloStream.gloDMS.Document.CategorisedDocument
                Dim oDocument As New gloStream.gloDMS.Document.Document
                '   Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim _SaveRecord As Boolean = False
                Dim _SQLQuery As String = ""
                Dim i As Integer = 0
                Dim _NoteStartPageID As Integer = 0
                Dim _Note As String = ""
                Dim _UncategoryPageCount As Integer = 0, _CategoryPageCount As Integer = 0
                Dim _DestDocID As Long, _SourceDocID As Long
                Dim _SynchDateTime As String = ""
                'Dim _ModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity
                'Dim _SendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag

                Save_MoveFromCategory = False
                Try
                    'Insert New Record for New Document Command
                    If oParameters.CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                        oDB.Connect(GetConnectionString)

                        oDB.DBParameters.Add("@PrefixID", GetPrefixTransactionID(oParameters.PatientID), ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@DocumentName", DocumentDispName, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@DocumentFileName", DestDocumentFileName, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Extension", Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@SourceMachine", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@SystemFolder", Supporting.PathsAndFolders.SystemFolder, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Container", Supporting.PathsAndFolders.ContainerFolder, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Category", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@PatientID", oParameters.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Year", oParameters.ShowDocumentYear, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@Month", oParameters.DestinationMonth, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@DocumentFormat", gloStream.gloDMS.Supporting.enumDocumentFormat.PDF, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SourceBin", gloStream.gloDMS.Supporting.enumDocumentSourceBin.Category, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@Pages", 0, ParameterDirection.Input, SqlDbType.Int)
                        oDB.DBParameters.Add("@ArchiveStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ArchiveDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@UsedStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@UsedMachine", "", ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@UsedType", gloStream.gloDMS.Supporting.enumDocumentUsedType.None, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@VersionNo", CateVersionNo, ParameterDirection.Input, SqlDbType.Int)
                        oDB.DBParameters.Add("@ModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
                        oDB.DBParameters.Add("@IsReviewed", False, ParameterDirection.Input, SqlDbType.Bit)

                        oDB.ExecuteNonQuery("gsp_DMS_NewDocument")
                        oDB.Disconnect()
                    End If

                    'Transfer Notes from Uncategory Document to New Document
                    If oParameters.CommandEventType = mdlgloDMS.MenuEventType.NewDocument Then
                        oCategoryDocument = oDocument.GetCategorisedDocument(CategoryFolderPath & "\" & DestDocumentFileName & "." & Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), False, False)
                        '//oCategoryDocument = oDocument.GetCategorisedDocument(CategoryFolderPath & "\" & NewDocumentName & "." & oSupporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF))// Document Name Changes Aug 2006
                        _NoteStartPageID = 1
                    Else
                        oCategoryDocument = oDocument.GetCategorisedDocument(MergeDocumentPath, False, False)
                        '//oCategoryDocument = oDocument.GetCategorisedDocument(MergeDocumentPath)// Document Name Changes Aug 2006
                        _NoteStartPageID = DestDocPageCountBeforeMerge + 1
                    End If

                    'Update Version in Destination Doucment
                    If oParameters.CommandEventType <> mdlgloDMS.MenuEventType.NewDocument Then
                        oDocument.SetCategorisedDocumentVersionAndModifyDateTime(MergeDocumentPath, CateVersionNo, False)
                    End If
                    'Update Version in Source Uncategory Document (which is actuall category document)
                    oDocument.SetCategorisedDocumentVersionAndModifyDateTime(oParameters.Document.Path, UncateVersionNo, False)


                    If Not oCategoryDocument Is Nothing Then
                        For i = 1 To oParameters.SelectedPages.Count
                            _Note = Trim(oDocument.ViewNote(oParameters.SelectedPages(i), oParameters.Document))
                            If _Note <> "" Then
                                oDocument.AddNote(_Note, _NoteStartPageID, oCategoryDocument)
                            End If
                            _NoteStartPageID = _NoteStartPageID + 1
                        Next
                    End If

                    'Delete Note from Uncategory Document
                    For i = 1 To oParameters.SelectedPages.Count
                        oDocument.DeleteNote(oParameters.SelectedPages(i), oParameters.Document)
                    Next

                    'Shift Notes of uncategory document
                    Dim _StartNote As Integer = 0, _EndNote As Integer = oParameters.PageCount
                    If oParameters.SelectedPages.Count > 0 Then
                        _StartNote = oParameters.SelectedPages(1)
                    End If
                    For i = _StartNote To _EndNote
                        _Note = Trim(oDocument.ViewNote(i, oParameters.Document))
                        If _Note <> "" Then
                            oDocument.AddNote(_Note, i - PagesBeforeCurrentPage(oParameters.SelectedPages, i), oParameters.Document)
                            oDocument.DeleteNote(i, oParameters.Document)
                        End If
                    Next

                    'Update Pages
                    _UncategoryPageCount = oParameters.PageCount - oParameters.SelectedPages.Count
                    _CategoryPageCount = DestDocPageCountBeforeMerge + oParameters.SelectedPages.Count
                    _DestDocID = UpdatePages(_CategoryPageCount, oCategoryDocument)
                    _SourceDocID = UpdatePages(_UncategoryPageCount, oParameters.Document)

                    'Delete Uncategory Document if All Pages are selected
                    If oParameters.SelectedPages.Count = oParameters.PageCount Then
                        DeleteDocument(oParameters.Document)
                    End If

                    '//Document Changes - Start"
                    'Synchronization Infromation Add
                    If _DestDocID <> 0 AndAlso _SourceDocID <> 0 Then
                        _SynchDateTime = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)
                        'Add Record to Dest Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)

                        If NewDocumentName = "" Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromCatInExtDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        Else
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToCatFromCatInNewDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If
                        oDB.DBParameters.Add("@OldCategory", oParameters.Document.Category, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.ReceivedFrom, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()

                        'Add Record to Source Document

                        'Modify in Source Master Document
                        oDB.ExecuteQueryNonQuery("UPDATE DMS_MST SET Modified = " & True & ", Synchronized = " & False & " WHERE DocumentID = " & _SourceDocID & "")

                        'Modify in Source Detail Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)

                        If NewDocumentName = "" Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToNewDocumentToCatFromCat, ParameterDirection.Input, SqlDbType.TinyInt)
                        Else
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToExtDocumentToCatFromCat, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If
                        oDB.DBParameters.Add("@OldCategory", oParameters.Document.Category, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.DestinationCategory, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.SendTo, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()
                        '// Document Name Changes - Finish//
                    End If
                    '//Document Changes - Finish"

                    _SaveRecord = True
                    Return _SaveRecord
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    Exit Function
                Finally
                    oSupport = Nothing
                    oCategoryDocument = Nothing
                    oDocument = Nothing
                    ' oPathFolders = Nothing
                    oDB.Dispose()
                    oDB = Nothing

                    _SaveRecord = Nothing
                    _SQLQuery = Nothing
                    i = Nothing
                    _Note = Nothing
                End Try
            End Function

            Private Function Save_DeletePages(ByVal oParameters As gloStream.gloDMS.Supporting.PrintFaxDeleteParameters, ByVal UncategoryFolderPath As String, ByVal CategoryFolderPath As String, ByVal DocumentDispName As String, ByVal DestDocumentFileName As String, ByVal DestDocPageCountBeforeMerge As Int16, ByVal CatUnCatDelDocVersionNo As Integer) As Boolean
                Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                Dim oCategoryDocument As New gloStream.gloDMS.Document.CategorisedDocument
                Dim oUncategoryDocument As New gloStream.gloDMS.Document.UncategorisedDocument
                Dim oDocument As New gloStream.gloDMS.Document.Document
                '  Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim _SaveRecord As Boolean = False
                Dim _SQLQuery As String = ""
                Dim i As Integer = 0
                Dim _NoteStartPageID As Integer = 0
                Dim _Note As String = ""
                Dim _UncategoryPageCount As Integer = 0, _CategoryPageCount As Integer = 0
                Dim _DestDocID As Long, _SourceDocID As Long
                Dim _SynchDateTime As String = ""
                'Dim _ModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity
                'Dim _SendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag
                Dim _DeleteDoucmentName As String = DocumentDispName
                Dim oDataReader As System.Data.SqlClient.SqlDataReader
                Dim _NoteID As Integer = 1
                Dim _NoteDateTime As String = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)

                Save_DeletePages = False
                Try
                    '//Document Name Changes - Start//
                    'Insert New Record for New Document Command
                    oDB.Connect(GetConnectionString)

                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        While oDocument.FindDocument(_DeleteDoucmentName, oParameters.PatientID, oParameters.CategorisedDocument.Category, Supporting.enumDocumentType.CategorisedDeletedDocument) = True
                            i = i + 1 'This is temp fix on 19/09/2007, pls verify it when done
                            _DeleteDoucmentName = DocumentDispName & "_" & i
                        End While
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        While oDocument.FindDocument(_DeleteDoucmentName, oParameters.PatientID, "", Supporting.enumDocumentType.UnCategorisedDeletedDocument) = True
                            i = i + 1 'This is temp fix on 19/09/2007, pls verify it when done
                            _DeleteDoucmentName = DocumentDispName & "_" & i
                        End While
                    End If

                    i = 0

                    oDB.DBParameters.Add("@PrefixID", GetPrefixTransactionID(oParameters.PatientID), ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@DocumentName", _DeleteDoucmentName, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@DocumentFileName", DestDocumentFileName, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Extension", Supporting.Supporting.ExtensionAsString(Supporting.enumDocumentExtension.PDF), ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@SourceMachine", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@SystemFolder", Supporting.PathsAndFolders.SystemFolder, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@Container", Supporting.PathsAndFolders.RecycleFolder, ParameterDirection.Input, SqlDbType.VarChar)
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDB.DBParameters.Add("@Category", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        oDB.DBParameters.Add("@Category", "", ParameterDirection.Input, SqlDbType.VarChar)
                    End If
                    oDB.DBParameters.Add("@PatientID", oParameters.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Year", oParameters.Year, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@Month", oParameters.Month, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@DocumentFormat", gloStream.gloDMS.Supporting.enumDocumentFormat.PDF, ParameterDirection.Input, SqlDbType.TinyInt)
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDB.DBParameters.Add("@SourceBin", gloStream.gloDMS.Supporting.enumDocumentSourceBin.Category, ParameterDirection.Input, SqlDbType.TinyInt)
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        oDB.DBParameters.Add("@SourceBin", gloStream.gloDMS.Supporting.enumDocumentSourceBin.GeneralBin, ParameterDirection.Input, SqlDbType.TinyInt)
                    End If
                    oDB.DBParameters.Add("@Pages", 0, ParameterDirection.Input, SqlDbType.Int)
                    oDB.DBParameters.Add("@ArchiveStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@ArchiveDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@UsedStatus", False, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@UsedMachine", "", ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@UsedType", gloStream.gloDMS.Supporting.enumDocumentUsedType.None, ParameterDirection.Input, SqlDbType.TinyInt)
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDeletedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        oDB.DBParameters.Add("@DocumentType", gloStream.gloDMS.Supporting.enumDocumentType.UnCategorisedDeletedDocument, ParameterDirection.Input, SqlDbType.TinyInt)
                    End If
                    oDB.DBParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.DBParameters.Add("@VersionNo", 1, ParameterDirection.Input, SqlDbType.Int)
                    oDB.DBParameters.Add("@ModifiedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
                    oDB.DBParameters.Add("@IsReviewed", False, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.ExecuteNonQuery("gsp_DMS_NewDocument")
                    oDB.Disconnect()

                    'Update CatUnCatDelDocument Version
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        oDocument.SetCategorisedDocumentVersionAndModifyDateTime(oParameters.CategorisedDocument.Path, CatUnCatDelDocVersionNo, False)
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        oDocument.SetUncategorisedDocumentVersionAndModifyDateTime(oParameters.UncategorisedDocument.Path, CatUnCatDelDocVersionNo, False)
                    End If

                    'Transfer Notes from Uncategory Document to New Document
                    'Get Document ID
                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Supporting.PathsAndFolders.SystemFolder & "' AND Container = '" & Supporting.PathsAndFolders.RecycleFolder & "' AND PatientID = " & oParameters.PatientID & " AND [Year] = '" & oParameters.Year & "' AND [Month] = '" & oParameters.Month & "' AND DocumentFileName  = " & DestDocumentFileName & " AND DocumentName  = '" & _DeleteDoucmentName & "' AND Extension = '" & oParameters.CategorisedDocument.Extension & "' AND DocumentID <> 0 AND DocumentType = " & gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDeletedDocument & " "
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Supporting.PathsAndFolders.SystemFolder & "' AND Container = '" & Supporting.PathsAndFolders.RecycleFolder & "' AND PatientID = " & oParameters.PatientID & " AND [Year] = '" & oParameters.Year & "' AND [Month] = '" & oParameters.Month & "' AND DocumentFileName  = " & DestDocumentFileName & " AND DocumentName  = '" & _DeleteDoucmentName & "' AND Extension = '" & oParameters.UncategorisedDocument.Extension & "' AND DocumentID <> 0 AND DocumentType = " & gloStream.gloDMS.Supporting.enumDocumentType.UnCategorisedDeletedDocument & " "
                    End If
                    oDB.DBParameters.Clear()
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _DestDocID = CLng(oDataReader.Item(0) & "")
                        End While
                    End If
                    oDB.Disconnect()

                    If _DestDocID <> 0 Then
                        _NoteStartPageID = 1
                        For i = 1 To oParameters.SelectedPages.Count
                            If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                                _Note = Trim(oDocument.ViewNote(oParameters.SelectedPages(i), oParameters.CategorisedDocument))
                            ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                                _Note = Trim(oDocument.ViewNote(oParameters.SelectedPages(i), oParameters.UncategorisedDocument))
                            End If
                            If _Note <> "" Then
                                oDB.Connect(GetConnectionString)
                                oDB.DBParameters.Add("@DocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@PageID", _NoteStartPageID, ParameterDirection.Input, SqlDbType.Int)
                                oDB.DBParameters.Add("@NoteDateTime", _NoteDateTime, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@Note", _Note, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.ExecuteNonQuery("gsp_DMS_AddNote")
                                oDB.Disconnect()
                            End If
                            _NoteStartPageID = _NoteStartPageID + 1
                        Next
                    End If
                    '//Document Name Changes - Finish//

                    'Delete Note from Uncategory Document
                    For i = 1 To oParameters.SelectedPages.Count
                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            oDocument.DeleteNote(oParameters.SelectedPages(i), oParameters.CategorisedDocument)
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            oDocument.DeleteNote(oParameters.SelectedPages(i), oParameters.UncategorisedDocument)
                        End If
                    Next

                    'Shift Notes of uncategory document
                    Dim _StartNote As Integer = 0, _EndNote As Integer = oParameters.PageCount
                    If oParameters.SelectedPages.Count > 0 Then
                        _StartNote = oParameters.SelectedPages(1)
                    End If
                    For i = _StartNote To _EndNote
                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            _Note = Trim(oDocument.ViewNote(i, oParameters.CategorisedDocument))
                            If _Note <> "" Then
                                oDocument.AddNote(_Note, i - PagesBeforeCurrentPage(oParameters.SelectedPages, i), oParameters.CategorisedDocument)
                                oDocument.DeleteNote(i, oParameters.CategorisedDocument)
                            End If
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            _Note = Trim(oDocument.ViewNote(i, oParameters.UncategorisedDocument))
                            If _Note <> "" Then
                                oDocument.AddNote(_Note, i - PagesBeforeCurrentPage(oParameters.SelectedPages, i), oParameters.UncategorisedDocument)
                                oDocument.DeleteNote(i, oParameters.UncategorisedDocument)
                            End If
                        End If
                    Next

                    'Update Pages
                    _UncategoryPageCount = oParameters.PageCount - oParameters.SelectedPages.Count
                    _CategoryPageCount = DestDocPageCountBeforeMerge + oParameters.SelectedPages.Count
                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Clear()
                    oDB.DBParameters.Add("@DocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Pages", _CategoryPageCount, ParameterDirection.Input, SqlDbType.Int)
                    oDB.ExecuteNonQuery("gsp_DMS_UpdatePages")
                    oDB.Disconnect()

                    If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                        _SourceDocID = UpdatePages(_UncategoryPageCount, oParameters.CategorisedDocument)
                    ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                        _SourceDocID = UpdatePages(_UncategoryPageCount, oParameters.UncategorisedDocument)
                    End If


                    'Delete Uncategory Document if All Pages are selected
                    If oParameters.SelectedPages.Count = oParameters.PageCount Then
                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            DeleteDocument(oParameters.CategorisedDocument)
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            DeleteDocument(oParameters.UncategorisedDocument)
                        End If
                    End If

                    '//Document Changes - Start"
                    'Synchronization Infromation Add
                    If _DestDocID <> 0 AndAlso _SourceDocID <> 0 Then
                        _SynchDateTime = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)
                        'Add Record to Dest Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)

                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToRecycleFromCat, ParameterDirection.Input, SqlDbType.TinyInt)
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.ReceivedToRecycleFromGB, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If

                        oDB.DBParameters.Add("@OldCategory", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.ReceivedFrom, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()

                        'Add Record to Source Document

                        'Modify in Source Master Document
                        oDB.ExecuteQueryNonQuery("UPDATE DMS_MST SET Modified = " & True & ", Synchronized = " & False & " WHERE DocumentID = " & _SourceDocID & "")

                        'Modify in Source Detail Document
                        oDB.DBParameters.Clear()
                        oDB.Connect(GetConnectionString)
                        oDB.DBParameters.Add("@DocumentID", _SourceDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Modified", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.DBParameters.Add("@ModifiedDateTime", _SynchDateTime, ParameterDirection.Input, SqlDbType.VarChar)
                        If oParameters.DocumentType = Supporting.enumDocumentType.CategorisedDocument Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToRecycleFromCat, ParameterDirection.Input, SqlDbType.TinyInt)
                        ElseIf oParameters.DocumentType = Supporting.enumDocumentType.UnCategorisedDocument Then
                            oDB.DBParameters.Add("@ModifyActivity", Supporting.ModifyActivity.SendToRecycleFromGB, ParameterDirection.Input, SqlDbType.TinyInt)
                        End If
                        oDB.DBParameters.Add("@OldCategory", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)
                        oDB.DBParameters.Add("@NewCategory", oParameters.Category, ParameterDirection.Input, SqlDbType.VarChar)

                        oDB.DBParameters.Add("@SendReceivedFlag", Supporting.SendRecivedFlag.SendTo, ParameterDirection.Input, SqlDbType.TinyInt)
                        oDB.DBParameters.Add("@SendReceivedDocumentID", _DestDocID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@Synchronized", False, ParameterDirection.Input, SqlDbType.Bit)
                        _SaveRecord = oDB.ExecuteNonQuery("gsp_DMS_NewSynchronize")
                        oDB.Disconnect()
                        '// Document Name Changes - Finish//
                    End If
                    '//Document Changes - Finish"


                    _SaveRecord = True
                    Return _SaveRecord
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    Exit Function
                Finally
                    oSupport = Nothing
                    oCategoryDocument = Nothing
                    oUncategoryDocument = Nothing
                    oDocument = Nothing
                    '   oPathFolders = Nothing
                    oDB.Dispose()
                    oDB = Nothing

                    _SaveRecord = Nothing
                    _SQLQuery = Nothing
                    i = Nothing
                    _Note = Nothing
                End Try
            End Function

            '---UPDATE PAGES---
            Private Function UpdatePages(ByVal Pages As Integer, ByVal Document As gloStream.gloDMS.Document.CategorisedDocument) As Long
                ' Dim oSupporting As New gloStream.gloDMS.Supporting.Supporting
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Dim _SQLQuery As String
                Dim _DocumentID As Long
                Dim _Result As Boolean = False

                Try
                    'Get Document ID
                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName  = " & Document.FileName & " AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                        End While
                    End If
                    oDB.Disconnect()

                    'Set Note with Document ID
                    If _DocumentID <> 0 Then
                        oDB.Connect(GetConnectionString)
                        'Document ID
                        oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                        'Pages
                        oDB.DBParameters.Add("@Pages", Pages, ParameterDirection.Input, SqlDbType.Int)

                        If oDB.ExecuteNonQuery("gsp_DMS_UpdatePages") = True Then
                            _Result = True
                        End If
                        oDB.Disconnect()
                    End If

                    If _Result = True Then
                        Return _DocumentID
                    Else
                        Return 0
                    End If

                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    UpdatePages = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    '  oSupporting = Nothing
                    oDataReader = Nothing

                    _SQLQuery = Nothing
                    _DocumentID = Nothing
                End Try
            End Function

            Private Function UpdatePages(ByVal Pages As Integer, ByVal Document As gloStream.gloDMS.Document.UncategorisedDocument) As Long
                ' Dim oSupporting As New gloStream.gloDMS.Supporting.Supporting
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Dim _SQLQuery As String
                Dim _DocumentID As Long
                Dim _Result As Boolean = False

                Try
                    'Get Document ID
                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName  = " & Document.FileName & " AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                        End While
                    End If
                    oDB.Disconnect()

                    'Set Note with Document ID
                    If _DocumentID <> 0 Then
                        oDB.Connect(GetConnectionString)
                        'Document ID
                        oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                        'Pages
                        oDB.DBParameters.Add("@Pages", Pages, ParameterDirection.Input, SqlDbType.Int)

                        If oDB.ExecuteNonQuery("gsp_DMS_UpdatePages") = True Then
                            _Result = True
                        End If
                        oDB.Disconnect()
                    End If

                    If _Result = True Then
                        Return _DocumentID
                    Else
                        Return 0
                    End If
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    UpdatePages = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    '  oSupporting = Nothing
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing

                    _SQLQuery = Nothing
                    _DocumentID = Nothing
                End Try
            End Function

            Private Function PagesBeforeCurrentPage(ByVal PageCollection As Collection, ByVal PageNo As Integer) As Integer
                Dim i As Integer
                Dim _Pages As Integer = 0
                For i = 1 To PageCollection.Count
                    If CInt(PageCollection(i)) < CInt(PageNo) Then
                        _Pages = _Pages + 1
                    End If
                Next
                Return _Pages
            End Function

            '---DELETE DOCUMENT---
            Private Function DeleteDocument(ByVal Document As gloStream.gloDMS.Document.CategorisedDocument) As Boolean
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Dim _SQLQuery As String
                Dim _DocumentID As Long
                Dim _Result As Boolean = False

                Try
                    'Get Document ID
                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName  = " & Document.FileName & " AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                        End While
                    End If
                    oDB.Disconnect()

                    'Set Note with Document ID
                    If _DocumentID <> 0 Then
                        oDB.Connect(GetConnectionString)
                        'Document ID
                        oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                        If oDB.ExecuteNonQuery("gsp_DMS_DeleteDocument") = True Then
                            _Result = True
                        End If
                        oDB.Disconnect()
                    End If

                    Return _Result
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    DeleteDocument = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing

                    _SQLQuery = Nothing
                    _DocumentID = Nothing
                End Try
            End Function

            Private Function DeleteDocument(ByVal Document As gloStream.gloDMS.Document.UncategorisedDocument) As Boolean
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Dim _SQLQuery As String
                Dim _DocumentID As Long
                Dim _Result As Boolean = False

                Try
                    'Get Document ID
                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName  = " & Document.FileName & " AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                        End While
                    End If
                    oDB.Disconnect()

                    'Set Note with Document ID
                    If _DocumentID <> 0 Then
                        oDB.Connect(GetConnectionString)
                        'Document ID
                        oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                        If oDB.ExecuteNonQuery("gsp_DMS_DeleteDocument") = True Then
                            _Result = True
                        End If
                        '  oDB.Disconnect()
                    End If

                    Return _Result
                Catch oError As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = oError.Message
                    DeleteDocument = Nothing
                    Exit Function
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing

                    _SQLQuery = Nothing
                    _DocumentID = Nothing
                End Try
            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Namespace Document
            Public Class Document
                Private _ErrorMessage As String
                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function IsCategoryExistInExplorer(ByVal RootPath As String, ByVal oCategory As String) As Boolean
                    '   Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Try
                        IsCategoryExistInExplorer = False
                        If Not oCategory Is Nothing Then
                            Dim _TempPath As String = Supporting.PathsAndFolders.GetDMSPath(RootPath) & Supporting.PathsAndFolders.SystemFolder & "\" & Supporting.PathsAndFolders.ContainerFolder & "\" & oCategory
                            If Directory.Exists(_TempPath) = True Then
                                Return True
                            End If
                        End If
                    Catch objError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        IsCategoryExistInExplorer = False
                        Exit Function
                    Finally
                        '  oSupport = Nothing
                    End Try
                End Function

                Public Function IsExistInExplorer(ByVal RootPath As String, ByVal oDocument As gloStream.gloDMS.Document.CategorisedDocument) As Boolean
                    '  Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Try
                        IsExistInExplorer = False
                        If Not oDocument Is Nothing Then
                            Dim _TempPath As String = Supporting.PathsAndFolders.GetDMSPath(RootPath) & oDocument.SystemFolder & "\" & oDocument.Container & "\" & oDocument.Category & "\" & oDocument.PatientID & "\" & oDocument.Year & "\" & oDocument.Month & "\" & oDocument.FileName & "." & oDocument.Extension
                            If File.Exists(_TempPath) = True Then
                                Return True
                            End If
                        End If
                    Catch objError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        IsExistInExplorer = False
                        Exit Function
                    Finally
                        '  oSupport = Nothing
                    End Try
                End Function

                Public Function IsExistInExplorer(ByVal RootPath As String, ByVal oDocument As gloStream.gloDMS.Document.UncategorisedDocument) As Boolean
                    ' Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Try
                        IsExistInExplorer = False
                        If Not oDocument Is Nothing Then
                            Dim _TempPath As String = Supporting.PathsAndFolders.GetDMSPath(RootPath) & oDocument.SystemFolder & "\" & oDocument.Container & "\" & oDocument.PatientID & "\" & oDocument.Year & "\" & oDocument.Month & "\" & oDocument.FileName & "." & oDocument.Extension
                            If File.Exists(_TempPath) = True Then
                                Return True
                            End If
                        End If
                    Catch objError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        IsExistInExplorer = False
                        Exit Function
                    Finally
                        '  oSupport = Nothing
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

                Public Function HasNote(ByVal Document As gloStream.gloDMS.Document.CategorisedDocument) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Records As Integer = 0
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
                            'Document ID
                            oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                            _Records = CInt(oDB.ExecuteScaler("gsp_DMS_HasNote"))
                            If _Records > 0 Then
                                _Result = True
                            End If
                            ' oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        HasNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function HasNote(ByVal Document As gloStream.gloDMS.Document.UncategorisedDocument) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Records As Integer = 0
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
                            'Document ID
                            oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                            _Records = CInt(oDB.ExecuteScaler("gsp_DMS_HasNote"))
                            If _Records > 0 Then
                                _Result = True
                            End If
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        HasNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function AddNote(ByVal Note As String, ByVal PageNo As Integer, ByVal Document As CategorisedDocument) As Boolean
                    Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _NoteID As Integer = 1
                    Dim _NoteDateTime As String = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
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
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        AddNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oSupport = Nothing
                        'oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                        _NoteID = Nothing
                        _NoteDateTime = Nothing
                    End Try
                End Function

                Public Function AddNote(ByVal Note As String, ByVal PageNo As Integer, ByVal Document As UncategorisedDocument) As Boolean
                    Dim oSupport As New gloStream.gloDMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _NoteID As Integer = 1
                    Dim _NoteDateTime As String = oSupport.NewDocumentName(0, "", Supporting.enumDocumentType.None)
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
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
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        AddNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oSupport = Nothing
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                        _NoteID = Nothing
                        _NoteDateTime = Nothing
                    End Try
                End Function

                Public Function DeleteNote(ByVal PageNo As Integer, ByVal Document As CategorisedDocument) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
                            'Document ID
                            oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                            'Page ID
                            oDB.DBParameters.Add("@PageID", PageNo, ParameterDirection.Input, SqlDbType.Int)

                            If oDB.ExecuteNonQuery("gsp_DMS_DeleteNote") = True Then
                                _Result = True
                            End If
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        DeleteNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function DeleteNote(ByVal PageNo As Integer, ByVal Document As UncategorisedDocument) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Result As Boolean = False

                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
                            'Document ID
                            oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                            'Page ID
                            oDB.DBParameters.Add("@PageID", PageNo, ParameterDirection.Input, SqlDbType.Int)

                            If oDB.ExecuteNonQuery("gsp_DMS_DeleteNote") = True Then
                                _Result = True
                            End If
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        DeleteNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function ViewNote(ByVal PageNo As Integer, ByVal Document As CategorisedDocument) As String
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Note As String = ""
                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 2"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
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
                            'oDB.Disconnect()
                        End If
                        Return _Note
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        ViewNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                        _Note = Nothing
                    End Try
                End Function

                Public Function ViewNote(ByVal PageNo As Integer, ByVal Document As UncategorisedDocument) As String
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _Note As String = ""
                    Try
                        'Get Document ID
                        _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & Document.SystemFolder & "' AND Container = '" & Document.Container & "' AND PatientID = " & Document.PatientID & " AND [Year] = '" & Document.Year & "' AND [Month] = '" & Document.Month & "' AND DocumentFileName = '" & Document.FileName & "' AND DocumentName  = '" & Document.Name & "' AND Extension = '" & Document.Extension & "' AND DocumentID <> 0 AND DocumentType = 1"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                            End While
                        End If
                        oDB.Disconnect()

                        'Set Note with Document ID
                        If _DocumentID <> 0 Then
                            oDB.Connect(GetConnectionString)
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
                            'oDB.Disconnect()
                        End If
                        Return _Note
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        ViewNote = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                        _Note = Nothing
                    End Try
                End Function

                Public Function IsReviwed(ByVal Path As String) As Boolean
                    '  Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0
                    Dim _SQLQuery As String = ""
                    Dim _Reviwed As Int16 = 0

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
                                _SQLQuery = "SELECT IsReviewed FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 2 "

                                oDB.Connect(GetConnectionString)
                                _Reviwed = Val(oDB.ExecuteQueryScaler(_SQLQuery))
                                oDB.Disconnect()
                                If _Reviwed = 0 Then
                                    Return False
                                Else
                                    Return True
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        IsReviwed = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        ' _PathFolder = Nothing
                        _Path = Nothing
                        _DMSPath = Nothing
                        _PathSplitter = Nothing
                        _FileName = Nothing : _Name = Nothing : _Extension = Nothing : _Month = Nothing : _Year = Nothing : _Category = Nothing : _Container = Nothing : _SystemFolder = Nothing
                        _PatientID = Nothing
                        oDB = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Function UpdateReviwed(ByVal Path As String, ByVal ReviwedUserID As Long, ByVal ReviwedDateTime As DateTime, ByVal Comments As String) As Boolean
                    ' Dim oSupporting As New gloStream.gloDMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _DocumentFileName As Long
                    Dim _Result As Boolean = False

                    '//
                    '  Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0

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
                                _SQLQuery = "SELECT DocumentID,DocumentFileName FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 2 AND DocumentID IS NOT NULL AND DocumentFileName IS NOT NULL"
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
                            _Result = oDB.ExecuteQueryNonQuery("INSERT INTO DMS_ReviwedDetail (DocumentID,DocumentFileName,ReviwedBy,ReviwedDateTime,Comments) VALUES (" & _DocumentID & "," & _DocumentFileName & "," & ReviwedUserID & ",'" & ReviwedDateTime & "','" & Comments & "')")
                            'Update DMS_MST
                            If _Result = True Then
                                _Result = oDB.ExecuteQueryNonQuery("UPDATE DMS_MST SET IsReviewed = 1 WHERE DocumentID = " & _DocumentID & "")
                            End If
                            'oDB.Disconnect()
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        UpdateReviwed = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '  oSupporting = Nothing
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function ViewReviwed(ByVal Path As String) As gloStream.gloDMS.Document.ReviwedDetail
                    '  Dim oSupporting As New gloStream.gloDMS.Supporting.Supporting
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim oResult As New gloStream.gloDMS.Document.ReviwedDetail

                    Dim _SQLQuery As String
                    Dim _DocumentID As Long
                    Dim _DocumentFileName As Long

                    '//
                    '   Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0

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
                                _SQLQuery = "SELECT DocumentID,DocumentFileName FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 2 AND DocumentID IS NOT NULL AND DocumentFileName IS NOT NULL"
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
                            _SQLQuery = "SELECT DMS_ReviwedDetail.ReviwedBy, User_MST.sLoginName, DMS_ReviwedDetail.ReviwedDateTime, DMS_ReviwedDetail.Comments " _
                            & " FROM DMS_ReviwedDetail INNER JOIN User_MST ON DMS_ReviwedDetail.ReviwedBy = User_MST.nUserID " _
                            & " WHERE (DMS_ReviwedDetail.DocumentID = " & _DocumentID & ") AND (DMS_ReviwedDetail.DocumentFileName = " & _DocumentFileName & ") ORDER BY DMS_ReviwedDetail.ReviwedDateTime DESC"
                            oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    If _Read = False Then
                                        If Not IsDBNull(oDataReader.Item("ReviwedBy")) Then
                                            oResult.ReviwedByUserID = oDataReader.Item("ReviwedBy")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sLoginName")) Then
                                            oResult.ReviwedByUserName = oDataReader.Item("sLoginName")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("ReviwedDateTime")) Then
                                            oResult.ReviwedDateTime = oDataReader.Item("ReviwedDateTime")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("Comments")) Then
                                            oResult.Comments = oDataReader.Item("Comments")
                                        End If
                                        Exit While
                                    End If
                                End While
                            End If
                            'oDB.Disconnect()
                        End If

                        Return oResult
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        ViewReviwed = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '    oSupporting = Nothing
                        oDB = Nothing
                        oDataReader = Nothing

                        _SQLQuery = Nothing
                        _DocumentID = Nothing
                    End Try
                End Function

                Public Function GetCategorisedDocument(ByVal Path As String, ByVal SynchronizationInfo As Boolean, Optional ByVal Archived As Boolean = False) As CategorisedDocument
                    Dim _CategorisedDocument As New gloStream.gloDMS.Document.CategorisedDocument
                    '   Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    'DMS System\Container\Corresponds\388335464231046001\2006\April\388335464231046001.PDF
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _DocumentID As Long, _DoucmentFileName As Long
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
                                    _SQLQuery = "SELECT DocumentID, DocumentName, Extension, SourceMachine, SystemFolder, Container, Category, " _
                                                & "PatientID, Year, Month, DocumentFormat, SourceBin, Pages, ArchiveStatus, ArchiveDescription," _
                                                & "UsedStatus, UsedMachine, UsedType, DocumentType, DocumentFileName, MachineID, " _
                                                & "Synchronized, VersionNo, IsReviewed FROM DMS_MST  " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "'  " _
                                                & "AND Category = '" & _Category & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 1 " _
                                                & "AND DocumentType = 2 "
                                Else
                                    _SQLQuery = "SELECT DocumentID, DocumentName, Extension, SourceMachine, SystemFolder, Container, Category, " _
                                                & "PatientID, Year, Month, DocumentFormat, SourceBin, Pages, ArchiveStatus, ArchiveDescription," _
                                                & "UsedStatus, UsedMachine, UsedType, DocumentType, DocumentFileName, MachineID," _
                                                & "Synchronized, VersionNo, IsReviewed FROM DMS_MST  " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND Category = '" & _Category & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 0 " _
                                                & "AND DocumentType = 2 "
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        With _CategorisedDocument
                                            _DocumentID = oDataReader.Item("DocumentID")
                                            _DoucmentFileName = oDataReader.Item("DocumentFileName")

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
                                            .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                            If Not IsDBNull(oDataReader.Item("IsReviewed")) Then
                                                .IsReviwed = oDataReader.Item("IsReviewed")
                                            End If
                                        End With
                                    End While
                                End If
                                oDataReader.Close()

                                _SQLQuery = "SELECT DMS_ReviwedDetail.ReviwedBy, DMS_ReviwedDetail.ReviwedDateTime, DMS_ReviwedDetail.Comments, " _
                                & " User_MST.sLoginName FROM User_MST RIGHT OUTER JOIN DMS_ReviwedDetail ON User_MST.nUserID = DMS_ReviwedDetail.ReviwedBy " _
                                & " WHERE (DMS_ReviwedDetail.DocumentID = " & _DocumentID & ") AND (DMS_ReviwedDetail.DocumentFileName = " & _DoucmentFileName & ") ORDER BY DMS_ReviwedDetail.ReviwedDateTime"
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        With _CategorisedDocument.ReviwedDetails
                                            If IsDBNull(oDataReader.Item("ReviwedBy")) = False AndAlso IsDBNull(oDataReader.Item("sLoginName")) = False Then
                                                Dim _tmpReviwedDateTime As DateTime = Now.Date
                                                Dim _tmpReviwedComments As String = ""

                                                If Not IsDBNull(oDataReader.Item("ReviwedDateTime")) Then
                                                    _tmpReviwedDateTime = oDataReader.Item("ReviwedDateTime")
                                                End If
                                                If Not IsDBNull(oDataReader.Item("Comments")) Then
                                                    _tmpReviwedComments = oDataReader.Item("Comments")
                                                End If

                                                .Add(oDataReader.Item("ReviwedBy"), oDataReader.Item("sLoginName"), _tmpReviwedDateTime, _tmpReviwedComments)
                                            End If
                                        End With
                                    End While
                                End If
                                oDataReader.Close()

                                'oDB.Disconnect()
                                Return _CategorisedDocument
                            End If
                        End If
                        GetCategorisedDocument = Nothing
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        GetCategorisedDocument = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        _CategorisedDocument = Nothing
                        '_PathFolder = Nothing
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

                Public Function GetUncategorisedDocument(ByVal Path As String, ByVal SynchronizationInfo As Boolean, Optional ByVal Archived As Boolean = False) As UncategorisedDocument
                    Dim _UncategorisedDocument As New gloStream.gloDMS.Document.UncategorisedDocument
                    '  Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader

                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
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
                                            'Case 2 ' Category
                                            '    _Category = ""
                                        Case 2 ' PatientID
                                            _PatientID = CLng(_PathSplitter(i))
                                        Case 3 ' Year
                                            _Year = Trim(_PathSplitter(i))
                                        Case 4 ' Month
                                            _Month = Trim(_PathSplitter(i))
                                        Case 5 ' Document Name
                                            _FileName = Mid(Trim(_PathSplitter(i)), 1, InStr(Trim(_PathSplitter(i)), ".") - 1)
                                            'Case 6 ' Document Extension
                                            _Extension = Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                                    End Select
                                Next
                                'Pick Record From Database for varification
                                If Archived = True Then
                                    _SQLQuery = "SELECT DocumentFileName,DocumentName,Extension,SourceMachine,SystemFolder" _
                                                & "Container,PatientID,Year,Month,DocumentFormat,SourceBin,Pages,UsedStatus" _
                                                & "UsedType,DocumentType,MachineID,VersionNo FROM(DMS_MST) " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 1 " _
                                                & "AND DocumentType = 1"
                                Else
                                    _SQLQuery = "SELECT DocumentFileName,DocumentName,Extension,SourceMachine,SystemFolder" _
                                                & "Container,PatientID,Year,Month,DocumentFormat,SourceBin,Pages,UsedStatus" _
                                                & "UsedType,DocumentType,MachineID,VersionNo FROM(DMS_MST) " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 0 " _
                                                & "AND DocumentType = 1"
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        With _UncategorisedDocument
                                            .FileName = Trim(oDataReader.Item("DocumentFileName") & "")
                                            .Name = Trim(oDataReader.Item("DocumentName") & "")
                                            .Extension = Trim(oDataReader.Item("Extension") & "")
                                            .SourceMachine = Trim(oDataReader.Item("SourceMachine") & "")
                                            .SystemFolder = Trim(oDataReader.Item("SystemFolder") & "")
                                            .Container = Trim(oDataReader.Item("Container") & "")
                                            .PatientID = CLng(oDataReader.Item("PatientID") & "")
                                            .Year = Trim(oDataReader.Item("Year") & "")
                                            .Month = Trim(oDataReader.Item("Month") & "")
                                            .Format = Val(oDataReader.Item("DocumentFormat") & "")
                                            .SourceBin = Val(oDataReader.Item("SourceBin") & "")
                                            .Pages = Val(oDataReader.Item("Pages") & "")
                                            .InUsed = oDataReader.Item("UsedStatus")
                                            .UsedMachine = Trim(oDataReader.Item("UsedMachine") & "")
                                            .UsedType = Val(oDataReader.Item("UsedType") & "")
                                            .Type = Val(oDataReader.Item("DocumentType") & "")
                                            .MachineID = CLng(oDataReader.Item("MachineID") & "")
                                            .VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                        End With
                                    End While
                                End If
                                'oDB.Disconnect()
                                Return _UncategorisedDocument
                            End If
                        End If
                        GetUncategorisedDocument = Nothing
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        GetUncategorisedDocument = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        _UncategorisedDocument = Nothing
                        '   _PathFolder = Nothing
                        _Path = Nothing
                        _DMSPath = Nothing
                        _PathSplitter = Nothing
                        _Name = Nothing : _Extension = Nothing : _Month = Nothing : _Year = Nothing : _Category = Nothing : _Container = Nothing : _SystemFolder = Nothing
                        _PatientID = Nothing
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Function GetCategorisedDocumentVersion(ByVal Path As String, ByVal SynchronizationInfo As Boolean, Optional ByVal Archived As Boolean = False) As Integer
                    ' Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer
                    Dim _VersionNo As Integer = 0
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
                                    _SQLQuery = "SELECT VersionNo FROM DMS_MST " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND Category = '" & _Category & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 1 " _
                                                & "AND DocumentType = 2 "
                                Else
                                    _SQLQuery = "SELECT VersionNo FROM DMS_MST " _
                                               & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                               & "AND Container = '" & _Container & "' " _
                                               & "AND Category = '" & _Category & "' " _
                                               & "AND PatientID = " & _PatientID & " " _
                                               & "AND [Year] = '" & _Year & "' " _
                                               & "AND [Month] = '" & _Month & "' " _
                                               & "AND DocumentFileName  = '" & _FileName & "' " _
                                               & "AND Extension = '" & _Extension & "' " _
                                               & "AND DocumentID <> 0 " _
                                               & "AND ArchiveStatus = 0 " _
                                               & "AND DocumentType = 2 "
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                    End While
                                End If
                                'oDB.Disconnect()
                                Return _VersionNo
                            Else
                                Return Nothing
                            End If
                        Else
                            Return Nothing
                        End If
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        GetCategorisedDocumentVersion = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        ' _PathFolder = Nothing
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

                Public Function GetUncategorisedDocumentVersion(ByVal Path As String, ByVal SynchronizationInfo As Boolean, Optional ByVal Archived As Boolean = False) As Integer
                    'Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _VersionNo As Integer = 0
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
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
                                            'Case 2 ' Category
                                            '    _Category = ""
                                        Case 2 ' PatientID
                                            _PatientID = CLng(_PathSplitter(i))

                                        Case 3 ' Year
                                            _Year = Trim(_PathSplitter(i))
                                        Case 4 ' Month
                                            _Month = Trim(_PathSplitter(i))
                                        Case 5 ' Document Name
                                            _FileName = Mid(Trim(_PathSplitter(i)), 1, InStr(Trim(_PathSplitter(i)), ".") - 1)
                                            'Case 6 ' Document Extension
                                            _Extension = Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                                    End Select
                                Next
                                'Pick Record From Database for varification
                                If Archived = True Then
                                    _SQLQuery = "SELECT VersionNo FROM DMS_MST " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND Category = '" & _Category & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 1 " _
                                                & "AND DocumentType = 1 "
                                Else
                                    _SQLQuery = "SELECT VersionNo FROM DMS_MST " _
                                                & "WHERE SystemFolder = '" & _SystemFolder & "' " _
                                                & "AND Container = '" & _Container & "' " _
                                                & "AND Category = '" & _Category & "' " _
                                                & "AND PatientID = " & _PatientID & " " _
                                                & "AND [Year] = '" & _Year & "' " _
                                                & "AND [Month] = '" & _Month & "' " _
                                                & "AND DocumentFileName  = '" & _FileName & "' " _
                                                & "AND Extension = '" & _Extension & "' " _
                                                & "AND DocumentID <> 0 " _
                                                & "AND ArchiveStatus = 0 " _
                                                & "AND DocumentType = 1 "
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _VersionNo = CInt(oDataReader.Item("VersionNo") & "")
                                    End While
                                End If
                                'oDB.Disconnect()
                                Return _VersionNo
                            Else
                                Return Nothing
                            End If
                        Else
                            Return Nothing
                        End If
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        GetUncategorisedDocumentVersion = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '   _PathFolder = Nothing
                        _Path = Nothing
                        _DMSPath = Nothing
                        _PathSplitter = Nothing
                        _Name = Nothing : _Extension = Nothing : _Month = Nothing : _Year = Nothing : _Category = Nothing : _Container = Nothing : _SystemFolder = Nothing
                        _PatientID = Nothing
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Function SetUsed(ByVal Used As Boolean, ByVal MachineName As String, ByVal Type As Supporting.enumDocumentUsedType, ByVal Document As CategorisedDocument) As Boolean
                    Return Nothing
                End Function

                Public Function SetUsed(ByVal Used As Boolean, ByVal MachineName As String, ByVal Type As Supporting.enumDocumentUsedType, ByVal Document As UncategorisedDocument) As Boolean
                    Return Nothing
                End Function

                Public Function GetDocumentMonth(ByVal DocumentFileName As String, Optional ByVal Archived As Boolean = False) As String
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String = ""
                    Dim _DocumentMonth As String = ""
                    Try
                        'Pick Record From Database for varification
                        If Archived = True Then
                            _SQLQuery = "SELECT [Month] FROM DMS_MST WHERE DocumentFileName = '" & DocumentFileName & "' AND ArchiveStatus = 1 "
                        Else
                            _SQLQuery = "SELECT [Month] FROM DMS_MST WHERE DocumentFileName = '" & DocumentFileName & "' AND ArchiveStatus = 0 "
                        End If

                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _DocumentMonth = Trim(oDataReader.Item(0) & "")
                            End While
                        End If
                        'oDB.Disconnect()
                        Return _DocumentMonth
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        GetDocumentMonth = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Function SetCategorisedDocumentVersionAndModifyDateTime(ByVal Path As String, ByVal Version As Integer, Optional ByVal Archived As Boolean = False) As Boolean
                    '   Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer
                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0
                    Dim _SQLQuery As String = ""
                    Dim _DocumentID As Long
                    Dim _Result As Boolean = False

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
                                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 1 AND DocumentType = 2 "
                                Else
                                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 2 "
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                                    End While
                                End If
                                oDB.Disconnect()

                                _SQLQuery = "UPDATE DMS_MST SET VersionNo = " & Version & ", ModifyDateTime = '" & System.DateTime.Now & "' WHERE DocumentID = " & _DocumentID & " "
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteQueryNonQuery(_SQLQuery)
                                'oDB.Disconnect()

                                Return _Result
                            Else
                                Return Nothing
                            End If
                        Else
                            Return Nothing
                        End If
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        SetCategorisedDocumentVersionAndModifyDateTime = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '  _PathFolder = Nothing
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

                Public Function SetUncategorisedDocumentVersionAndModifyDateTime(ByVal Path As String, ByVal Version As Integer, Optional ByVal Archived As Boolean = False) As Boolean
                    ' Dim _PathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _Path As String
                    Dim _DMSPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                    Dim _PathSplitter() As String
                    Dim i As Integer

                    Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
                    Dim _PatientID As Long = 0
                    Dim _SQLQuery As String = ""
                    Dim _DocumentID As Long
                    Dim _Result As Boolean = False

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
                                            'Case 2 ' Category
                                            '    _Category = ""
                                        Case 2 ' PatientID
                                            _PatientID = CLng(_PathSplitter(i))
                                        Case 3 ' Year
                                            _Year = Trim(_PathSplitter(i))
                                        Case 4 ' Month
                                            _Month = Trim(_PathSplitter(i))
                                        Case 5 ' Document Name
                                            _FileName = Mid(Trim(_PathSplitter(i)), 1, InStr(Trim(_PathSplitter(i)), ".") - 1)
                                            'Case 6 ' Document Extension
                                            _Extension = Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                                    End Select
                                Next
                                'Pick Record From Database for varification
                                If Archived = True Then
                                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 1 AND DocumentType = 1"
                                Else
                                    _SQLQuery = "SELECT DocumentID FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 1"
                                End If

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        _DocumentID = CLng(oDataReader.Item("DocumentID") & "")
                                    End While
                                End If
                                oDB.Disconnect()

                                _SQLQuery = "UPDATE DMS_MST SET VersionNo = " & Version & ", ModifyDateTime = '" & System.DateTime.Now & "' WHERE DocumentID = " & _DocumentID & " "
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteQueryNonQuery(_SQLQuery)
                                'oDB.Disconnect()

                                Return _Result
                            Else
                                Return Nothing
                            End If
                        Else
                            Return Nothing
                        End If
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        SetUncategorisedDocumentVersionAndModifyDateTime = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '  _PathFolder = Nothing
                        _Path = Nothing
                        _DMSPath = Nothing
                        _PathSplitter = Nothing
                        _Name = Nothing : _Extension = Nothing : _Month = Nothing : _Year = Nothing : _Category = Nothing : _Container = Nothing : _SystemFolder = Nothing
                        _PatientID = Nothing
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                'Its use to find document with same already exists or not in given criteria, 
                'as per new logic for DocumentFileName its not necessary, bcz its already cover in NewDocumentFileNameOrID function
                'and document name will not be duplicate if document is archived, so this field not compare in sql query
                Public Function FindDocument(ByVal DocumentName As String, ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String = ""
                    FindDocument = True

                    Try
                        'Pick Record From Database for varification
                        _SQLQuery = "SELECT DocumentName FROM DMS_MST WHERE DocumentName = '" & DocumentName & "' AND PatientID = " & PatientID & " AND Category = '" & Category & "' AND DocumentType = " & DocumentType & ""
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = False Then
                            Return False
                        Else
                            Return True
                        End If
                        'oDB.Disconnect()
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Function FindDocumentForRename(ByVal DocumentName As String, ByVal DocumentOldName As String, ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim _SQLQuery As String = ""
                    ' FindDocument = True

                    Try
                        'Pick Record From Database for varification
                        '_SQLQuery = "SELECT DocumentName FROM DMS_MST WHERE DocumentName = '" & DocumentName & "' AND PatientID = " & PatientID & " AND Category = '" & Category & "' AND DocumentType = " & DocumentType & ""
                        _SQLQuery = "SELECT DocumentName FROM DMS_MST WHERE DocumentName = '" & DocumentOldName & "' AND DocumentName <> '" & DocumentName & "'  AND PatientID = " & PatientID & " AND Category = '" & Category & "' AND DocumentType = " & DocumentType & ""
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = False Then
                            Return False
                        Else
                            Return True
                        End If
                        'oDB.Disconnect()
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        FindDocumentForRename = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function


                'use this function to enabled\disabled scan document\view document button
                Public Function DocumentCount(ByVal PatientID As Long, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType, Optional ByVal Archived As Boolean = False) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _SQLQuery As String = ""
                    Dim _DocumentCount As Long = 0
                    Try
                        'Pick Record From Database for varification
                        If Archived = True Then
                            _SQLQuery = "SELECT PatientID FROM DMS_MST WHERE PatientID = " & PatientID & " AND ArchiveStatus = 1 AND DocumentType = " & DocumentType & ""
                        Else
                            _SQLQuery = "SELECT PatientID FROM DMS_MST WHERE PatientID = " & PatientID & " AND ArchiveStatus = 0 AND DocumentType = " & DocumentType & ""
                        End If

                        oDB.Connect(GetConnectionString)
                        _DocumentCount = CType(oDB.ExecuteQueryScaler(_SQLQuery), Long)
                        oDB.Disconnect()
                        Return _DocumentCount
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        DocumentCount = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB = Nothing
                        _SQLQuery = Nothing
                    End Try
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
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
                Private _SynchronizationParameters As gloStream.gloDMS.Document.SynchronizationParameters
                Private _MachineID As Int16
                Private _VersionNo As Integer
                Private _ModifiedDateTime As DateTime

                Private _IsReviwed As Boolean = False
                Private _ReviwedDetails As gloStream.gloDMS.Document.ReviwedDetails
                Private bReviwedDetails As Boolean = False
                Private bSynchronizationParameters As Boolean = False
                Public Sub Dispose()
                    If (bReviwedDetails) Then
                        _ReviwedDetails.Dispose()
                        bReviwedDetails = False
                    End If
                    If (bSynchronizationParameters) Then
                        _SynchronizationParameters.Dispose()
                        bSynchronizationParameters = False
                    End If
                End Sub
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
                        '  Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                        Dim _TempPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & _SystemFolder & "\" & _Container & "\" & _Category & "\" & _PatientID & "\" & _Year & "\" & _Month & "\" & _FileName & "." & _Extension
                        ' oSupport = Nothing
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

                'Synchronization Parameter
                Public Property SynchronizationParameters() As gloStream.gloDMS.Document.SynchronizationParameters
                    Get
                        Return _SynchronizationParameters
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.SynchronizationParameters)
                        If (bSynchronizationParameters) Then
                            _SynchronizationParameters.Dispose()
                            bSynchronizationParameters = False
                        End If
                        _SynchronizationParameters = Value
                    End Set
                End Property

                Public Property IsReviwed() As Boolean
                    Get
                        Return _IsReviwed
                    End Get
                    Set(ByVal Value As Boolean)
                        _IsReviwed = Value
                    End Set
                End Property

                Public Property ReviwedDetails() As gloStream.gloDMS.Document.ReviwedDetails
                    Get
                        Return _ReviwedDetails
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.ReviwedDetails)
                        If (bReviwedDetails) Then
                            _ReviwedDetails.Dispose()
                            bReviwedDetails = False
                        End If
                        _ReviwedDetails = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _SynchronizationParameters = New gloStream.gloDMS.Document.SynchronizationParameters
                    bSynchronizationParameters = True
                    _ReviwedDetails = New gloStream.gloDMS.Document.ReviwedDetails
                    bReviwedDetails = True
                End Sub

                Protected Overrides Sub Finalize()
                    _SynchronizationParameters = Nothing
                    _ReviwedDetails = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            'Categorised Documents Collection
            Public Class CategorisedDocuments
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oCategorisedDocument As CategorisedDocument) As CategorisedDocument
                    'create a new object
                    Dim objNewMember As CategorisedDocument
                    objNewMember = New CategorisedDocument

                    'set the properties passed into the method
                    objNewMember.FileName = oCategorisedDocument.FileName
                    objNewMember.Name = oCategorisedDocument.Name
                    objNewMember.Extension = oCategorisedDocument.Extension
                    objNewMember.SourceMachine = oCategorisedDocument.SourceMachine
                    objNewMember.SystemFolder = oCategorisedDocument.SystemFolder
                    objNewMember.Container = oCategorisedDocument.Container
                    objNewMember.Category = oCategorisedDocument.Category
                    objNewMember.PatientID = oCategorisedDocument.PatientID
                    objNewMember.Year = oCategorisedDocument.Year
                    objNewMember.Month = oCategorisedDocument.Month
                    objNewMember.Format = oCategorisedDocument.Format
                    objNewMember.SourceBin = oCategorisedDocument.SourceBin
                    ' objNewMember.Blocked = oCategorisedDocument.Blocked
                    objNewMember.Pages = oCategorisedDocument.Pages
                    objNewMember.Archived = oCategorisedDocument.Archived
                    objNewMember.ArchiveDescription = oCategorisedDocument.ArchiveDescription
                    objNewMember.InUsed = oCategorisedDocument.InUsed
                    objNewMember.UsedMachine = oCategorisedDocument.UsedMachine
                    objNewMember.UsedType = oCategorisedDocument.UsedType
                    objNewMember.Type = oCategorisedDocument.Type
                    objNewMember.MachineID = oCategorisedDocument.MachineID
                    objNewMember.SynchronizationParameters = oCategorisedDocument.SynchronizationParameters
                    objNewMember.VersionNo = oCategorisedDocument.VersionNo
                    objNewMember.ModifiedDateTime = oCategorisedDocument.ModifiedDateTime
                    objNewMember.IsReviwed = oCategorisedDocument.IsReviwed
                    objNewMember.ReviwedDetails = oCategorisedDocument.ReviwedDetails

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

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As CategorisedDocument
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

            'Reviwed Details
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

            'Categorised Documents Collection
            Public Class ReviwedDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal UserID As Long, ByVal UserName As String, ByVal ReviwedDateTime As DateTime, ByVal Comments As String) As ReviwedDetail
                    'create a new object
                    Dim objNewMember As ReviwedDetail
                    objNewMember = New ReviwedDetail

                    'set the properties passed into the method
                    objNewMember.ReviwedByUserID = UserID
                    objNewMember.ReviwedByUserName = UserName
                    objNewMember.ReviwedDateTime = ReviwedDateTime
                    objNewMember.Comments = Comments

                    mCol.Add(objNewMember)

                    'return the object created
                    Add = objNewMember
                    'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ReviwedDetail
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

            'Uncategorised Document
            Public Class UncategorisedDocument
                Private _FileName As String
                Private _Name As String
                Private _Extension As String
                Private _SourceMachine As String
                Private _SystemFolder As String
                Private _Container As String
                'Private _Category As String
                Private _PatientID As Long
                Private _Year As String
                Private _Month As String
                Private _Format As Supporting.enumDocumentFormat
                Private _SourceBin As Supporting.enumDocumentSourceBin
                '  Private _Blocked As Boolean
                Private _Pages As Long
                'Private _Archived As Boolean
                'Private _ArchiveDescription As String
                Private _InUsed As Boolean
                Private _UsedMachine As String
                Private _UsedType As Supporting.enumDocumentUsedType
                Private _Type As Supporting.enumDocumentType
                Private _Path As String ' to retrive document, not for store
                Private _MachineID As Int16
                Private _SynchronizationParameters As gloStream.gloDMS.Document.SynchronizationParameters
                Private _VersionNo As Integer
                Private _ModifiedDateTime As DateTime
                Private bSynchronizationParameters As Boolean = False
                Public Sub Dispose()
                    If (bSynchronizationParameters) Then
                        _SynchronizationParameters.Dispose()
                        bSynchronizationParameters = False
                    End If
                End Sub
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
                'Public Property Category() As String
                '    Get
                '        Return _Category
                '    End Get
                '    Set(ByVal Value As String)
                '        _Category = Value
                '    End Set
                'End Property

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
                        ' Dim oSupport As New gloStream.gloDMS.Supporting.PathsAndFolders
                        Dim _TempPath As String = Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & _SystemFolder & "\" & _Container & "\" & _PatientID & "\" & _Year & "\" & _Month & "\" & _FileName & "." & _Extension
                        ' oSupport = Nothing
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

                'Synchronization Parameter
                Public Property SynchronizationParameters() As gloStream.gloDMS.Document.SynchronizationParameters
                    Get
                        Return _SynchronizationParameters
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.SynchronizationParameters)
                        If (bSynchronizationParameters) Then
                            _SynchronizationParameters.Dispose()
                            bSynchronizationParameters = False
                        End If
                        _SynchronizationParameters = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _SynchronizationParameters = New gloStream.gloDMS.Document.SynchronizationParameters
                    bSynchronizationParameters = True
                End Sub

                Protected Overrides Sub Finalize()
                    _SynchronizationParameters = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            'Uncategorised Documents Collection
            Public Class UncategorisedDocuments
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oUncategorisedDocument As UncategorisedDocument) As UncategorisedDocument
                    'create a new object
                    Dim objNewMember As UncategorisedDocument
                    objNewMember = New UncategorisedDocument

                    'set the properties passed into the method
                    objNewMember.FileName = oUncategorisedDocument.FileName
                    objNewMember.Name = oUncategorisedDocument.Name
                    objNewMember.Extension = oUncategorisedDocument.Extension
                    objNewMember.SourceMachine = oUncategorisedDocument.SourceMachine
                    objNewMember.SystemFolder = oUncategorisedDocument.SystemFolder
                    objNewMember.Container = oUncategorisedDocument.Container
                    'objNewMember.Category = oUncategorisedDocument.Category
                    objNewMember.PatientID = oUncategorisedDocument.PatientID
                    objNewMember.Year = oUncategorisedDocument.Year
                    objNewMember.Month = oUncategorisedDocument.Month
                    objNewMember.Format = oUncategorisedDocument.Format
                    objNewMember.SourceBin = oUncategorisedDocument.SourceBin
                    ' objNewMember.Blocked = oUncategorisedDocument.Blocked
                    objNewMember.Pages = oUncategorisedDocument.Pages
                    'objNewMember.Archived = oUncategorisedDocument.Archived
                    'objNewMember.ArchiveDescription = oUncategorisedDocument.ArchiveDescription
                    objNewMember.InUsed = oUncategorisedDocument.InUsed
                    objNewMember.UsedMachine = oUncategorisedDocument.UsedMachine
                    objNewMember.UsedType = oUncategorisedDocument.UsedType
                    objNewMember.Type = oUncategorisedDocument.Type
                    objNewMember.MachineID = oUncategorisedDocument.MachineID
                    objNewMember.SynchronizationParameters = oUncategorisedDocument.SynchronizationParameters
                    'objNewMember.Path = oUncategorisedDocument.path
                    objNewMember.VersionNo = oUncategorisedDocument.VersionNo
                    objNewMember.ModifiedDateTime = oUncategorisedDocument.ModifiedDateTime

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

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As UncategorisedDocument
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

            'Synchronization Parameter
            Public Class SynchronizationParameter
                Private _Modified As Boolean
                Private _ModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity
                Private _ModifyDateTime As DateTime
                Private _OldCategory As String
                Private _NewCategory As String
                Private _SendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag
                Private _SendReceivedDocumentName As String
                Private _SendReceivedDocumentPath As String
                Private _Synchronized As Boolean

                Public Property Modified() As Boolean
                    Get
                        Return _Modified
                    End Get
                    Set(ByVal Value As Boolean)
                        _Modified = Value
                    End Set
                End Property

                Public Property ModifyActivity() As gloStream.gloDMS.Supporting.ModifyActivity
                    Get
                        Return _ModifyActivity
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.ModifyActivity)
                        _ModifyActivity = Value
                    End Set
                End Property

                Public Property ModifyDateTime() As DateTime
                    Get
                        Return _ModifyDateTime
                    End Get
                    Set(ByVal Value As DateTime)
                        _ModifyDateTime = Value
                    End Set
                End Property

                Public Property OldCategory() As String
                    Get
                        Return _OldCategory
                    End Get
                    Set(ByVal Value As String)
                        _OldCategory = Value
                    End Set
                End Property

                Public Property NewCategory() As String
                    Get
                        Return _NewCategory
                    End Get
                    Set(ByVal Value As String)
                        _NewCategory = Value
                    End Set
                End Property

                Public Property SendReceivedFlag() As gloStream.gloDMS.Supporting.SendRecivedFlag
                    Get
                        Return _SendReceivedFlag
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.SendRecivedFlag)
                        _SendReceivedFlag = Value
                    End Set
                End Property

                Public Property SendReceivedDocumentName() As String
                    Get
                        Return _SendReceivedDocumentName
                    End Get
                    Set(ByVal Value As String)
                        _SendReceivedDocumentName = Value
                    End Set
                End Property

                Public Property SendReceivedDocumentPath() As String
                    Get
                        Return _SendReceivedDocumentPath
                    End Get
                    Set(ByVal Value As String)
                        _SendReceivedDocumentPath = Value
                    End Set
                End Property

                Public Property Synchronized() As Boolean
                    Get
                        Return _Synchronized
                    End Get
                    Set(ByVal Value As Boolean)
                        _Synchronized = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Uncategorised Documents Collection
            Public Class SynchronizationParameters
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oSynchronizationParameter As SynchronizationParameter) As SynchronizationParameter
                    mCol.Add(oSynchronizationParameter)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As SynchronizationParameter
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

        Namespace Supporting
            'Supporting Function & Methods & Constants
            Public Class Supporting

#Region "General Supporting"
                '---Constants---

                '---Functions---
                'Month As String
                Public Function MonthAsString(ByVal MonthNo As enumMonth) As String
                    MonthAsString = MonthName(MonthNo)
                End Function

                'Zoom As String
                Public Function ZoomAsString(ByVal ZoomFactor As enumZoom) As String
                    Select Case ZoomFactor
                        Case enumZoom.Zoom_25
                            Return "25 %"
                        Case enumZoom.Zoom_50
                            Return "50 %"
                        Case enumZoom.Zoom_75
                            Return "75 %"
                        Case enumZoom.Zoom_100
                            Return "100 %"
                        Case enumZoom.Zoom_125
                            Return "125 %"
                        Case enumZoom.Zoom_150
                            Return "150 %"
                        Case enumZoom.Zoom_175
                            Return "175 %"
                        Case enumZoom.Zoom_200
                            Return "200 %"
                        Case enumZoom.Zoom_225
                            Return "225 %"
                        Case enumZoom.Zoom_250
                            Return "250 %"
                        Case enumZoom.Zoom_275
                            Return "275 %"
                        Case enumZoom.Zoom_300
                            Return "300 %"
                        Case enumZoom.Zoom_325
                            Return "325 %"
                        Case enumZoom.Zoom_350
                            Return "350 %"
                        Case enumZoom.Zoom_375
                            Return "375 %"
                        Case enumZoom.Zoom_400
                            Return "400 %"
                        Case enumZoom.Zoom_BestFit
                            Return "Best Fit"
                        Case enumZoom.Zoom_FitToWidth
                            Return "Fit To Width"
                        Case Else
                            Return ""
                    End Select
                End Function

                'Zoom As Factor
                Public Function ZoomAsEnumFactor(ByVal ZoomValue As String) As enumZoom
                    Select Case Trim(ZoomValue)
                        Case "25 %"
                            Return enumZoom.Zoom_25
                        Case "50 %"
                            Return enumZoom.Zoom_50
                        Case "75 %"
                            Return enumZoom.Zoom_75
                        Case "100 %"
                            Return enumZoom.Zoom_100
                        Case "125 %"
                            Return enumZoom.Zoom_125
                        Case "150 %"
                            Return enumZoom.Zoom_150
                        Case "175 %"
                            Return enumZoom.Zoom_175
                        Case "200 %"
                            Return enumZoom.Zoom_200
                        Case "225 %"
                            Return enumZoom.Zoom_225
                        Case "250 %"
                            Return enumZoom.Zoom_250
                        Case "275 %"
                            Return enumZoom.Zoom_275
                        Case "300 %"
                            Return enumZoom.Zoom_300
                        Case "325 %"
                            Return enumZoom.Zoom_325
                        Case "350 %"
                            Return enumZoom.Zoom_350
                        Case "375 %"
                            Return enumZoom.Zoom_375
                        Case "400 %"
                            Return enumZoom.Zoom_400
                        Case "Best Fit"
                            Return enumZoom.Zoom_BestFit
                        Case "Fit To Width"
                            Return enumZoom.Zoom_FitToWidth
                        Case Else
                            Return Nothing

                    End Select

                End Function

                'Zoom Out Factor
                Public Function ZoomOut(ByVal ZoomFactor As Integer) As Integer
                    If ZoomFactor < 0 Then Return 100 : Exit Function
                    If ZoomFactor > 400 Then Return 400 : Exit Function

                    Select Case ZoomFactor
                        Case 0 To 24
                            Return 25
                        Case 25 To 49
                            Return 50
                        Case 50 To 74
                            Return 75
                        Case 75 To 99
                            Return 100
                        Case 100 To 124
                            Return 125
                        Case 125 To 149
                            Return 150
                        Case 150 To 174
                            Return 175
                        Case 175 To 199
                            Return 200
                        Case 200 To 224
                            Return 225
                        Case 225 To 249
                            Return 250
                        Case 250 To 274
                            Return 275
                        Case 275 To 299
                            Return 300
                        Case 300 To 324
                            Return 325
                        Case 325 To 349
                            Return 350
                        Case 350 To 374
                            Return 375
                        Case 375 To 400
                            Return 400
                        Case Else
                            Return Nothing
                    End Select

                End Function

                'Zoom In Factor
                Public Function ZoomIn(ByVal ZoomFactor As Integer) As Integer
                    If ZoomFactor < 0 Then Return 100 : Exit Function
                    If ZoomFactor > 400 Then Return 400 : Exit Function

                    Select Case ZoomFactor
                        Case 0 To 25
                            Return 25
                        Case 26 To 50
                            Return 25
                        Case 51 To 75
                            Return 50
                        Case 76 To 100
                            Return 75
                        Case 101 To 125
                            Return 100
                        Case 126 To 150
                            Return 125
                        Case 151 To 175
                            Return 150
                        Case 176 To 200
                            Return 175
                        Case 201 To 225
                            Return 200
                        Case 226 To 250
                            Return 225
                        Case 251 To 275
                            Return 250
                        Case 276 To 300
                            Return 275
                        Case 301 To 325
                            Return 300
                        Case 326 To 350
                            Return 325
                        Case 351 To 375
                            Return 350
                        Case 376 To 400
                            Return 375
                        Case Else
                            Return Nothing
                    End Select

                End Function

                'Extension As String
                Public Shared Function ExtensionAsString(ByVal Extension As enumDocumentExtension) As String
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
                    '    Dim oFolderPaths As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _RootPath As String
                    Dim _SystemFolder As String
                    Dim _ContainerFolder As String
                    Dim _RecycleFolder As String
                    Dim _ScanFolder As String
                    Try
                        IsDMSSystem = False
                        'DMS Root Path
                        _RootPath = Supporting.PathsAndFolders.GetDMSPath(Path)
                        If Directory.Exists(_RootPath) = False Then Exit Function
                        'Check For System Folder
                        _SystemFolder = _RootPath & Supporting.PathsAndFolders.SystemFolder
                        If Directory.Exists(_SystemFolder) = False Then Exit Function
                        'Check For Container Folder
                        _ContainerFolder = _SystemFolder & "\" & Supporting.PathsAndFolders.ContainerFolder
                        If Directory.Exists(_ContainerFolder) = False Then Exit Function
                        'Check For Recycle Folder
                        _RecycleFolder = _SystemFolder & "\" & Supporting.PathsAndFolders.RecycleFolder
                        If Directory.Exists(_RecycleFolder) = False Then Exit Function
                        'Check For Scan Folder
                        _ScanFolder = _SystemFolder & "\" & Supporting.PathsAndFolders.ScanFolder
                        If Directory.Exists(_ScanFolder) = False Then Exit Function
                        'Set Tru
                        IsDMSSystem = True
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        IsDMSSystem = False
                        Exit Function
                    Finally
                        ' oFolderPaths = Nothing
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

#End Region

                '---Settings---
#Region "Settings"
                Public Function SaveDMSSettings(ByVal Settings As gloStream.gloDMS.Supporting.DMSSettings) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        'Main Key
                        ' Dim regKey As RegistryKey
                        'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\" & DMSReg_MainName)) = True Then
                        '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
                        '    regKey.CreateSubKey(DMSReg_MainName)
                        '    regKey.Close()
                        'End If
                        'regKey = Registry.LocalMachine.OpenSubKey("Software\" & DMSReg_MainName, True)
                        If gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoft & DMSReg_MainName) = False Then
                            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                            gloRegistrySetting.CreateSubKey(DMSReg_MainName)
                        End If

                        'Root Path
                        'If IsNothing(regKey.GetValue(DMSReg_RootPath)) = True Then
                        '    regKey.SetValue(DMSReg_RootPath, Settings.DMSRootPath)
                        'Else
                        '    regKey.SetValue(DMSReg_RootPath, Settings.DMSRootPath)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_RootPath)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_RootPath, Settings.DMSRootPath)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_RootPath, Settings.DMSRootPath)
                        End If

                        'Scan Document Month View
                        'If IsNothing(regKey.GetValue(DMSReg_ScanDocumentMonthView)) = True Then
                        '    regKey.SetValue(DMSReg_ScanDocumentMonthView, Settings.ScanDocumentMonthView)
                        'Else
                        '    regKey.SetValue(DMSReg_ScanDocumentMonthView, Settings.ScanDocumentMonthView)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_ScanDocumentMonthView)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_ScanDocumentMonthView, Settings.ScanDocumentMonthView)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_ScanDocumentMonthView, Settings.ScanDocumentMonthView)
                        End If

                        ''Viwe Document Month View
                        'If IsNothing(regKey.GetValue(DMSReg_ViewDocumentMonthView)) = True Then
                        '    regKey.SetValue(DMSReg_ViewDocumentMonthView, Settings.ViewDocumentMonthView)
                        'Else
                        '    regKey.SetValue(DMSReg_ViewDocumentMonthView, Settings.ViewDocumentMonthView)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_ViewDocumentMonthView)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_ViewDocumentMonthView, Settings.ViewDocumentMonthView)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_ViewDocumentMonthView, Settings.ViewDocumentMonthView)
                        End If


                        'General Bin Merge Menu
                        'If IsNothing(regKey.GetValue(DMSReg_GeneralBinMergeMenu)) = True Then
                        '    regKey.SetValue(DMSReg_GeneralBinMergeMenu, Settings.GeneralBinMergeMenu)
                        'Else
                        '    regKey.SetValue(DMSReg_GeneralBinMergeMenu, Settings.GeneralBinMergeMenu)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_GeneralBinMergeMenu)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_GeneralBinMergeMenu, Settings.GeneralBinMergeMenu)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_GeneralBinMergeMenu, Settings.GeneralBinMergeMenu)
                        End If


                        'Category Doc Merge Menu
                        'If IsNothing(regKey.GetValue(DMSReg_CategoryDocMergeMenu)) = True Then
                        '    regKey.SetValue(DMSReg_CategoryDocMergeMenu, Settings.CategoryDocMergeMenu)
                        'Else
                        '    regKey.SetValue(DMSReg_CategoryDocMergeMenu, Settings.CategoryDocMergeMenu)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_CategoryDocMergeMenu)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_CategoryDocMergeMenu, Settings.CategoryDocMergeMenu)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_CategoryDocMergeMenu, Settings.CategoryDocMergeMenu)
                        End If

                        'General Bin 12 Months Menu
                        'If IsNothing(regKey.GetValue(DMSReg_GeneralBin12MonthMenu)) = True Then
                        '    regKey.SetValue(DMSReg_GeneralBin12MonthMenu, Settings.GeneralBin12MonthsMenu)
                        'Else
                        '    regKey.SetValue(DMSReg_GeneralBin12MonthMenu, Settings.GeneralBin12MonthsMenu)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_GeneralBin12MonthMenu)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_GeneralBin12MonthMenu, Settings.GeneralBin12MonthsMenu)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_GeneralBin12MonthMenu, Settings.GeneralBin12MonthsMenu)
                        End If

                        'category Doc 12 Months Menu
                        'If IsNothing(regKey.GetValue(DMSReg_Category12MonthMenu)) = True Then
                        '    regKey.SetValue(DMSReg_Category12MonthMenu, Settings.CategoryDoc12MonthsMenu)
                        'Else
                        '    regKey.SetValue(DMSReg_Category12MonthMenu, Settings.CategoryDoc12MonthsMenu)
                        'End If
                        If IsNothing(gloRegistrySetting.GetRegistryValue(DMSReg_Category12MonthMenu)) = True Then
                            gloRegistrySetting.SetRegistryValue(DMSReg_Category12MonthMenu, Settings.CategoryDoc12MonthsMenu)
                        Else
                            gloRegistrySetting.SetRegistryValue(DMSReg_Category12MonthMenu, Settings.CategoryDoc12MonthsMenu)
                        End If
                        gloRegistrySetting.CloseRegistryKey()
                        'regKey.Close()
                        _Result = True
                        Return _Result
                    Catch objError As Exception
                        Return Nothing
                    End Try
                End Function

                Public Function GetDMSSettings() As gloStream.gloDMS.Supporting.DMSSettings
                    Dim _Settings As New gloStream.gloDMS.Supporting.DMSSettings
                    'Set Default Values
                    With _Settings
                        .ScanDocumentMonthView = True
                        .ViewDocumentMonthView = True
                        .GeneralBinMergeMenu = True
                        .GeneralBin12MonthsMenu = False
                        .CategoryDocMergeMenu = True
                        .CategoryDoc12MonthsMenu = False
                    End With

                    Try
                        'SLR: Added code on resolving registry memories
                        Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\" & DMSReg_MainName)
                        If IsNothing(regKey) = True Then
                            GetDMSSettings = Nothing
                            Exit Function
                        End If
                        regKey.Close()
                        regKey.Dispose()
                        regKey = Registry.LocalMachine.OpenSubKey("Software\" & DMSReg_MainName, True)

                        'Root Path
                        If IsNothing(regKey.GetValue(DMSReg_RootPath)) = False Then
                            _Settings.DMSRootPath = regKey.GetValue(DMSReg_RootPath)
                        End If

                        'Scan Document Month View
                        If IsNothing(regKey.GetValue(DMSReg_ScanDocumentMonthView)) = False Then
                            _Settings.ScanDocumentMonthView = regKey.GetValue(DMSReg_ScanDocumentMonthView)
                        End If

                        'View Document Month View
                        If IsNothing(regKey.GetValue(DMSReg_ViewDocumentMonthView)) = False Then
                            _Settings.ViewDocumentMonthView = regKey.GetValue(DMSReg_ViewDocumentMonthView)
                        End If

                        'General Bin Merge Menu
                        If IsNothing(regKey.GetValue(DMSReg_GeneralBinMergeMenu)) = False Then
                            _Settings.GeneralBinMergeMenu = regKey.GetValue(DMSReg_GeneralBinMergeMenu)
                        End If

                        'Category Document Merge Menu
                        If IsNothing(regKey.GetValue(DMSReg_CategoryDocMergeMenu)) = False Then
                            _Settings.CategoryDocMergeMenu = regKey.GetValue(DMSReg_CategoryDocMergeMenu)
                        End If

                        'General Bin 12 Month Menu
                        If IsNothing(regKey.GetValue(DMSReg_GeneralBin12MonthMenu)) = False Then
                            _Settings.GeneralBin12MonthsMenu = regKey.GetValue(DMSReg_GeneralBin12MonthMenu)
                        End If

                        'Category Document 12 Month Menu
                        If IsNothing(regKey.GetValue(DMSReg_Category12MonthMenu)) = False Then
                            _Settings.CategoryDoc12MonthsMenu = regKey.GetValue(DMSReg_Category12MonthMenu)
                        End If

                        regKey.Close()
                        regKey.Dispose()
                        Return _Settings
                    Catch objError As Exception
                        GetDMSSettings = Nothing
                    Finally

                        _Settings = Nothing
                    End Try
                End Function

#End Region


                '---Property---
                'New Document Name
                'Public ReadOnly Property NewDocumentName() As String
                '    Get
                '        Threading.Thread.Sleep(1000)
                '        Dim _NewDocumentName As String = ""
                '        Dim _Date As String = Format(Date.Now, "MM/dd/yyyy")
                '        Dim _Time As String = Format(Date.Now, "hh:mm:ss tt")
                '        Dim i As Integer = 0
                '        Dim oDocument As New gloStream.gloDMS.Document.Document

                '        _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ")
                '        While oDocument.FindDocument(_NewDocumentName) = True
                '            i = i + 1
                '            _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ") & "-" & i
                '        End While

                '        Return _NewDocumentName
                '    End Get
                'End Property

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
                        _ReturnID = oDB.ExecuteNonQueryForOutput("gsp_DMS_GetNewDocumentIDAndFileName")
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        Return _ReturnID
                    End Get
                End Property



                '---Class New & Finalise---
                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

                Private Shared Function PathsAndFolders() As Object
                    Throw New NotImplementedException
                End Function

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

                Private Shared _TempProcessFolder As String

                Public Shared ReadOnly Property TempProcessFolder() As String
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

                Public Shared Function GetDMSPath(ByVal RootPath As String) As String
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        GetDMSPath = ""
                        Exit Function
                    End Try
                End Function

                Public Shared Function GetContainerPath(ByVal DMSSystemRootPath As String) As String
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        GetContainerPath = ""
                        Exit Function
                    End Try
                End Function

                Public Function GetScanPath(ByVal DMSSystemRootPath As String) As String
                    Return Nothing
                End Function

                Public Shared Function GetRecyclePath(ByVal DMSSystemRootPath As String) As String
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        GetRecyclePath = ""
                        Exit Function
                    End Try
                End Function

                Public Function GetRecycleScanPath(ByVal DMSSystemRootPath As String) As String
                    Return Nothing
                End Function

                Public Function GetRecycleGeneralPath(ByVal DMSSystemRootPath As String) As String
                    Return Nothing
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

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

            'Zoom Values
            Public Enum enumZoom
                Zoom_25 = 1
                Zoom_50 = 2
                Zoom_75 = 3
                Zoom_100 = 4
                Zoom_125 = 5
                Zoom_150 = 6
                Zoom_175 = 7
                Zoom_200 = 8
                Zoom_225 = 9
                Zoom_250 = 10
                Zoom_275 = 11
                Zoom_300 = 12
                Zoom_325 = 13
                Zoom_350 = 14
                Zoom_375 = 15
                Zoom_400 = 16
                Zoom_FitToWidth = 17
                Zoom_BestFit = 18
            End Enum

#End Region


#Region "Process Parameters"
            'Uncategorised Document Process Parameter
            Public Class UncategoryProcessParameters
                Private _UncategorizedDocument As gloStream.gloDMS.Document.UncategorisedDocument   ' UnCategory Document
                Private _MenuEvenetType As MenuEventType                                            ' Operation Type
                Private _DestPatientID As Long = 0                                                      ' Patient ID
                Private _DestCategory As String = ""                                                    ' Destination Category
                Private _DestMonth As String = ""                                                       ' Destination Month
                Private _DestDocument As String = ""                                                    ' Destination Document
                Private _DestDocumentFileName As String = ""                                                    ' Destination Document
                Private _SelectedPages As Collection
                Private _UnselectedPages As Collection
                Private _PageCount As Integer = 0
                Private _ShowDocumentYear As String
                Private bUnCategorizedDocument As Boolean = False
                Private bSelectedPages As Boolean = False
                Private bUnSelectedPages As Boolean = False
                Private bMenuEventType As Boolean = False
                Public Sub Dispose()
                    If (bUnCategorizedDocument) Then
                        _UncategorizedDocument.Dispose()
                        bUnCategorizedDocument = False
                    End If
                    If (bSelectedPages) Then
                        _SelectedPages.Clear()
                        bSelectedPages = False
                    End If
                    If (bUnSelectedPages) Then
                        _UnselectedPages.Clear()
                        bUnSelectedPages = False
                    End If
                    If (bMenuEventType) Then
                        _MenuEvenetType = Nothing
                        bMenuEventType = False
                    End If
                End Sub
                Public Property Document() As gloStream.gloDMS.Document.UncategorisedDocument
                    Get
                        Return _UncategorizedDocument
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.UncategorisedDocument)
                        If (bUnCategorizedDocument) Then
                            _UncategorizedDocument.Dispose()
                            bUnCategorizedDocument = False
                        End If
                        _UncategorizedDocument = Value
                    End Set
                End Property

                Friend Property CommandEventType() As MenuEventType
                    Get
                        Return _MenuEvenetType
                    End Get
                    Set(ByVal Value As MenuEventType)
                        If (bMenuEventType) Then
                            _MenuEvenetType = Nothing
                            bMenuEventType = False
                        End If
                        _MenuEvenetType = Value
                    End Set
                End Property

                Public Property PatientID() As Long
                    Get
                        Return _DestPatientID
                    End Get
                    Set(ByVal Value As Long)
                        _DestPatientID = Value
                    End Set
                End Property

                Public Property DestinationCategory() As String
                    Get
                        Return _DestCategory
                    End Get
                    Set(ByVal Value As String)
                        _DestCategory = Value
                    End Set
                End Property

                Public Property DestinationMonth() As String
                    Get
                        Return _DestMonth
                    End Get
                    Set(ByVal Value As String)
                        _DestMonth = Value
                    End Set
                End Property

                Public Property DestinationDocument() As String
                    Get
                        Return _DestDocument
                    End Get
                    Set(ByVal Value As String)
                        _DestDocument = Value
                    End Set
                End Property

                Public Property DestinationDocumentFileName() As String
                    Get
                        Return _DestDocumentFileName
                    End Get
                    Set(ByVal Value As String)
                        _DestDocumentFileName = Value
                    End Set
                End Property

                Public Property SelectedPages() As Collection
                    Get
                        Return _SelectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bSelectedPages) Then
                            _SelectedPages.Clear()
                            bSelectedPages = False
                        End If
                      
                        _SelectedPages = Value
                    End Set
                End Property

                Public Property UnselectedPages() As Collection
                    Get
                        Return _UnselectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bUnSelectedPages) Then
                            _UnselectedPages.Clear()
                            bUnSelectedPages = False
                        End If
                        _UnselectedPages = Value
                    End Set
                End Property

                Public Property PageCount() As Integer
                    Get
                        Return _PageCount
                    End Get
                    Set(ByVal Value As Integer)
                        _PageCount = Value
                    End Set
                End Property

                Public Property ShowDocumentYear() As String
                    Get
                        Return _ShowDocumentYear
                    End Get
                    Set(ByVal Value As String)
                        _ShowDocumentYear = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _UncategorizedDocument = New gloStream.gloDMS.Document.UncategorisedDocument
                    bUnCategorizedDocument = True
                    _MenuEvenetType = New MenuEventType
                    bMenuEventType = True
                    _SelectedPages = New Collection
                    bSelectedPages = True
                    _UnselectedPages = New Collection
                    bUnSelectedPages = True
                End Sub

                Protected Overrides Sub Finalize()
                    _UncategorizedDocument = Nothing
                    _MenuEvenetType = Nothing
                    _SelectedPages = Nothing
                    _UnselectedPages = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            'Categorised Document Process parameters
            Public Class CategoryProcessParameters
                Private _CategorizedDocument As gloStream.gloDMS.Document.CategorisedDocument   ' UnCategory Document
                Private _MenuEvenetType As MenuEventType                                            ' Operation Type
                Private _DestPatientID As Long = 0                                                      ' Patient ID
                Private _DestCategory As String = ""                                                    ' Destination Category
                Private _DestMonth As String = ""                                                       ' Destination Month
                Private _DestDocument As String = ""                                                    ' Destination Document
                Private _DestDocumentFileName As String = ""
                Private _SelectedPages As Collection
                Private _UnselectedPages As Collection
                Private _PageCount As Integer = 0
                Private _ShowDocumentYear As String
                Private bCategorizedDocument As Boolean = False
                Private bSelectedPages As Boolean = False
                Private bUnSelectedPages As Boolean = False
                Private bMenuEventType As Boolean = False
                Public Sub Dispose()
                    If (bCategorizedDocument) Then
                        _CategorizedDocument.Dispose()
                        bCategorizedDocument = False
                    End If
                    If (bSelectedPages) Then
                        _SelectedPages.Clear()
                        bSelectedPages = False
                    End If
                    If (bUnSelectedPages) Then
                        _UnselectedPages.Clear()
                        bUnSelectedPages = False
                    End If
                    If (bMenuEventType) Then
                        _MenuEvenetType = Nothing
                        bMenuEventType = False
                    End If
                End Sub
                Public Property Document() As gloStream.gloDMS.Document.CategorisedDocument
                    Get
                        Return _CategorizedDocument
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.CategorisedDocument)
                        If (bCategorizedDocument) Then
                            _CategorizedDocument.Dispose()
                            bCategorizedDocument = False
                        End If
                       
                        _CategorizedDocument = Value
                    End Set
                End Property

                Friend Property CommandEventType() As MenuEventType
                    Get
                        Return _MenuEvenetType
                    End Get
                    Set(ByVal Value As MenuEventType)
                      
                        If (bMenuEventType) Then
                            _MenuEvenetType = Nothing
                            bMenuEventType = False
                        End If
                        _MenuEvenetType = Value
                    End Set
                End Property

                Public Property PatientID() As Long
                    Get
                        Return _DestPatientID
                    End Get
                    Set(ByVal Value As Long)
                        _DestPatientID = Value
                    End Set
                End Property

                Public Property DestinationCategory() As String
                    Get
                        Return _DestCategory
                    End Get
                    Set(ByVal Value As String)
                        _DestCategory = Value
                    End Set
                End Property

                Public Property DestinationMonth() As String
                    Get
                        Return _DestMonth
                    End Get
                    Set(ByVal Value As String)
                        _DestMonth = Value
                    End Set
                End Property

                Public Property DestinationDocument() As String
                    Get
                        Return _DestDocument
                    End Get
                    Set(ByVal Value As String)
                        _DestDocument = Value
                    End Set
                End Property

                Public Property DestinationDocumentFileName() As String
                    Get
                        Return _DestDocumentFileName
                    End Get
                    Set(ByVal Value As String)
                        _DestDocumentFileName = Value
                    End Set
                End Property

                Public Property SelectedPages() As Collection
                    Get
                        Return _SelectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bSelectedPages) Then
                            _SelectedPages.Clear()
                            bSelectedPages = False
                        End If
                       
                        _SelectedPages = Value
                    End Set
                End Property

                Public Property UnselectedPages() As Collection
                    Get
                        Return _UnselectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bUnSelectedPages) Then
                            _UnselectedPages.Clear()
                            bUnSelectedPages = False
                        End If
                        _UnselectedPages = Value
                    End Set
                End Property

                Public Property PageCount() As Integer
                    Get
                        Return _PageCount
                    End Get
                    Set(ByVal Value As Integer)
                        _PageCount = Value
                    End Set
                End Property

                Public Property ShowDocumentYear() As String
                    Get
                        Return _ShowDocumentYear
                    End Get
                    Set(ByVal Value As String)
                        _ShowDocumentYear = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _CategorizedDocument = New gloStream.gloDMS.Document.CategorisedDocument
                    bCategorizedDocument = True
                    _MenuEvenetType = New MenuEventType
                    bMenuEventType = True
                    _SelectedPages = New Collection
                    bSelectedPages = True
                    _UnselectedPages = New Collection
                    bUnSelectedPages = True
                End Sub

                Protected Overrides Sub Finalize()
                    _CategorizedDocument = Nothing
                    _MenuEvenetType = Nothing
                    _SelectedPages = Nothing
                    _UnselectedPages = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            'Print/Fax Process Parameters
            Public Class PrintFaxDeleteParameters
                Private _CategorizedDocument As gloStream.gloDMS.Document.CategorisedDocument           ' UnCategory Document
                Private _UncategorizedDocument As gloStream.gloDMS.Document.UncategorisedDocument       ' UnCategory Document
                Private _CommandType As gloStream.gloDMS.Supporting.enumDocumentCriteria                ' Operation Type
                Private _DocumentType As gloStream.gloDMS.Supporting.enumDocumentType
                Private _PatientID As Long = 0                                                      ' Patient ID
                Private _Category As String = ""                                                    ' Destination Category
                Private _Month As String = ""                                                       ' Destination Month
                Private _SelectedPages As Collection
                Private _UnselectedPages As Collection
                Private _PageCount As Integer = 0
                Private _Year As String
                Private bCategorizedDocument As Boolean = False
                Private bUnCategorizedDocument As Boolean = False
                Private bSelectedPages As Boolean = False
                Private bUnSelectedPages As Boolean = False

                Public Sub Dispose()
                    If (bUnCategorizedDocument) Then
                        _UncategorizedDocument.Dispose()
                        bUnCategorizedDocument = False
                    End If
                    If (bCategorizedDocument) Then
                        _CategorizedDocument.Dispose()
                        bCategorizedDocument = False
                    End If
                    If (bSelectedPages) Then
                        _SelectedPages.Clear()
                        bSelectedPages = False
                    End If
                    If (bUnSelectedPages) Then
                        _UnselectedPages.Clear()
                        bUnSelectedPages = False
                    End If
               
                End Sub
                Public Property CategorisedDocument() As gloStream.gloDMS.Document.CategorisedDocument
                    Get
                        Return _CategorizedDocument
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.CategorisedDocument)
                        
                        If (bCategorizedDocument) Then
                            _CategorizedDocument.Dispose()
                            bCategorizedDocument = False
                        End If
                        _CategorizedDocument = Value
                    End Set
                End Property

                Public Property UncategorisedDocument() As gloStream.gloDMS.Document.UncategorisedDocument
                    Get
                        Return _UncategorizedDocument
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Document.UncategorisedDocument)
                        If (bUnCategorizedDocument) Then
                            _UncategorizedDocument.Dispose()
                            bUnCategorizedDocument = False
                        End If
                     
                        _UncategorizedDocument = Value
                    End Set
                End Property

                Public Property CommandType() As gloStream.gloDMS.Supporting.enumDocumentCriteria
                    Get
                        Return _CommandType
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentCriteria)
                        _CommandType = Value
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

                Public Property PatientID() As Long
                    Get
                        Return _PatientID
                    End Get
                    Set(ByVal Value As Long)
                        _PatientID = Value
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

                Public Property SelectedPages() As Collection
                    Get
                        Return _SelectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bSelectedPages) Then
                            _SelectedPages.Clear()
                            bSelectedPages = False
                        End If
                      
                        _SelectedPages = Value
                    End Set
                End Property

                Public Property UnselectedPages() As Collection
                    Get
                        Return _UnselectedPages
                    End Get
                    Set(ByVal Value As Collection)
                        If (bUnSelectedPages) Then
                            _UnselectedPages.Clear()
                            bUnSelectedPages = False
                        End If
                        _UnselectedPages = Value
                    End Set
                End Property

                Public Property PageCount() As Integer
                    Get
                        Return _PageCount
                    End Get
                    Set(ByVal Value As Integer)
                        _PageCount = Value
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

                Public Sub New()
                    MyBase.new()
                    _CategorizedDocument = New gloStream.gloDMS.Document.CategorisedDocument
                    bCategorizedDocument = True
                    _UncategorizedDocument = New gloStream.gloDMS.Document.UncategorisedDocument
                    bUnCategorizedDocument = True
                    _SelectedPages = New Collection
                    bSelectedPages = True
                    _UnselectedPages = New Collection
                    bUnSelectedPages = True
                End Sub

                Protected Overrides Sub Finalize()
                    _CategorizedDocument = Nothing
                    _UncategorizedDocument = Nothing
                    _SelectedPages = Nothing
                    _UnselectedPages = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            'General Bin Parameters
            Public Class ImportProcessParameters
                Private _PatientID As Long = 0                                                      ' Patient ID
                Private _Category As String = ""                                                    ' Destination Category
                Private _Month As String = ""                                                       ' Destination Month
                Private _Year As String
                Private _DocumentName As String = ""                                                    ' Destination Document
                Private _DocumentFileName As String = ""
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
                Public Property PatientID() As Long
                    Get
                        Return _PatientID
                    End Get
                    Set(ByVal Value As Long)
                        _PatientID = Value
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

                Public Property DocumentName() As String
                    Get
                        Return _DocumentName
                    End Get
                    Set(ByVal Value As String)
                        _DocumentName = Value
                    End Set
                End Property

                Public Property DocumentFileName() As String
                    Get
                        Return _DocumentFileName
                    End Get
                    Set(ByVal Value As String)
                        _DocumentFileName = Value
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

#End Region

            'Settings
            'Settins
            Public Class DMSSettings
                Private _DMSRootPath As String
                Private _ScanDocumentMonthView As Boolean
                Private _ViewDocumentMonthView As Boolean
                Private _GeneralBinMergeMenu As Boolean
                Private _CategoryDocMergeMenu As Boolean
                Private _GeneralBin12MonthMenu As Boolean
                Private _CategoryDoc12MonthMenu As Boolean

                Public Property DMSRootPath() As String
                    Get
                        Return _DMSRootPath
                    End Get
                    Set(ByVal Value As String)
                        _DMSRootPath = Value
                    End Set
                End Property

                Public Property ScanDocumentMonthView() As Boolean
                    Get
                        Return _ScanDocumentMonthView
                    End Get
                    Set(ByVal Value As Boolean)
                        _ScanDocumentMonthView = Value
                    End Set
                End Property

                Public Property ViewDocumentMonthView() As Boolean
                    Get
                        Return _ViewDocumentMonthView
                    End Get
                    Set(ByVal Value As Boolean)
                        _ViewDocumentMonthView = Value
                    End Set
                End Property

                Public Property GeneralBinMergeMenu() As Boolean
                    Get
                        Return _GeneralBinMergeMenu
                    End Get
                    Set(ByVal Value As Boolean)
                        _GeneralBinMergeMenu = Value
                    End Set
                End Property

                Public Property CategoryDocMergeMenu() As Boolean
                    Get
                        Return _CategoryDocMergeMenu
                    End Get
                    Set(ByVal Value As Boolean)
                        _CategoryDocMergeMenu = Value
                    End Set
                End Property

                Public Property GeneralBin12MonthsMenu() As Boolean
                    Get
                        Return _GeneralBin12MonthMenu
                    End Get
                    Set(ByVal Value As Boolean)
                        _GeneralBin12MonthMenu = Value
                    End Set
                End Property

                Public Property CategoryDoc12MonthsMenu() As Boolean
                    Get
                        Return _CategoryDoc12MonthMenu
                    End Get
                    Set(ByVal Value As Boolean)
                        _CategoryDoc12MonthMenu = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Menu Item Properties
            Public Class MenuItemDocument
                Private _DocumentName As String
                Private _DocumentFileName As String
                Private _DocumentPath As String
                Private _DocumentType As gloStream.gloDMS.Supporting.enumDocumentType
                Private _DocumentSourceBin As gloStream.gloDMS.Supporting.enumDocumentSourceBin
                Private _DocumentUsedType As gloStream.gloDMS.Supporting.enumDocumentUsedType
                Private _DocumentCriteria As gloStream.gloDMS.Supporting.enumDocumentCriteria
                Private _DocumentModifyActivity As gloStream.gloDMS.Supporting.ModifyActivity
                Private _DocumentSendReceivedFlag As gloStream.gloDMS.Supporting.SendRecivedFlag

                Public Property Name() As String
                    Get
                        Return _DocumentName
                    End Get
                    Set(ByVal Value As String)
                        _DocumentName = Value
                    End Set
                End Property

                Public Property FileName() As String
                    Get
                        Return _DocumentFileName
                    End Get
                    Set(ByVal Value As String)
                        _DocumentFileName = Value
                    End Set
                End Property

                Public Property Path() As String
                    Get
                        Return _DocumentPath
                    End Get
                    Set(ByVal Value As String)
                        _DocumentPath = Value
                    End Set
                End Property

                Public Property Type() As gloStream.gloDMS.Supporting.enumDocumentType
                    Get
                        Return _DocumentType
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentType)
                        _DocumentType = Value
                    End Set
                End Property

                Public Property SourceBin() As gloStream.gloDMS.Supporting.enumDocumentSourceBin
                    Get
                        Return _DocumentSourceBin
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentSourceBin)
                        _DocumentSourceBin = Value
                    End Set
                End Property

                Public Property UsedType() As gloStream.gloDMS.Supporting.enumDocumentUsedType
                    Get
                        Return _DocumentUsedType
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentUsedType)
                        _DocumentUsedType = Value
                    End Set
                End Property

                Public Property Criteria() As gloStream.gloDMS.Supporting.enumDocumentCriteria
                    Get
                        Return _DocumentCriteria
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.enumDocumentCriteria)
                        _DocumentCriteria = Value
                    End Set
                End Property

                Public Property ModifyActivity() As gloStream.gloDMS.Supporting.ModifyActivity
                    Get
                        Return _DocumentModifyActivity
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.ModifyActivity)
                        _DocumentModifyActivity = Value
                    End Set
                End Property

                Public Property SendRecivedFlag() As gloStream.gloDMS.Supporting.SendRecivedFlag
                    Get
                        Return _DocumentSendReceivedFlag
                    End Get
                    Set(ByVal Value As gloStream.gloDMS.Supporting.SendRecivedFlag)
                        _DocumentSendReceivedFlag = Value
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
                    ' Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
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
                            '1. Save category database
                            oDB.Connect(GetConnectionString)
                            'Category Name
                            oDB.DBParameters.Add("@CategoryName", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                            'Modify Name
                            oDB.DBParameters.Add("@ModifyName", "", ParameterDirection.Input, SqlDbType.VarChar)
                            'Excute Query
                            _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_SaveCategory")
                            oDB.Disconnect()

                            '2. Make Category in Explorer
                            If _RecordAdd = True Then
                                'Paths
                                _Category = oCategory.Name
                                _ContainerPath = Supporting.PathsAndFolders.GetContainerPath(DMSRootPath)
                                _RecycledPath = Supporting.PathsAndFolders.GetRecyclePath(DMSRootPath)

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
                        If _Result Then
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "DMS Category Added", gloAuditTrail.ActivityOutCome.Success)
                            ''Added Rahul P on 20101009
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "DMS Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''
                            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "DMS Category Added", gstrLoginName, gstrClientMachineName)
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Add = Nothing
                        Exit Function
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                        ' oPathFolder = Nothing
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
                    '   Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
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
                            oDB.Connect(GetConnectionString)
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
                                _ContainerPath = Supporting.PathsAndFolders.GetContainerPath(DMSRootPath)
                                _RecycledPath = Supporting.PathsAndFolders.GetRecyclePath(DMSRootPath)

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
                        If _Result Then
                            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "DMS Category Modified", gstrLoginName, gstrClientMachineName)
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Modify, "DMS Category Modified", gloAuditTrail.ActivityOutCome.Failure)
                        End If
                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Modify, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        Modify = Nothing
                        Exit Function
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                        ' oPathFolder = Nothing
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
                    ' Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
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
                            oDB.Connect(GetConnectionString)
                            'Category Name
                            oDB.DBParameters.Add("@CategoryName", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)
                            'Excute Query
                            _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_DeleteCategory")
                            oDB.Disconnect()

                            '2. Shift Category from Continer to Recycled Bin Explorer
                            If _RecordAdd = True Then
                                'Paths
                                _Category = oCategory.Name
                                _ContainerPath = Supporting.PathsAndFolders.GetContainerPath(DMSRootPath)
                                _RecycledPath = Supporting.PathsAndFolders.GetRecyclePath(DMSRootPath)

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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        Delete = Nothing
                        Exit Function
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                        '  oPathFolder = Nothing
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
                Public Function IsExists(ByVal oCategory As Category) As Boolean
                    Dim _Result As Boolean = False
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlDataReader
                    ' Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _CategoryPath As String

                    Try
                        'Check Category exists in database or not
                        oDB.Connect(GetConnectionString)
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
                        ' oDB.Disconnect()

                        'Check category exists in explorer or not
                        If oCategory.Name.Trim <> "" Then
                            _CategoryPath = Supporting.PathsAndFolders.GetContainerPath(DMSRootPath) & "\" & oCategory.Name
                            If Directory.Exists(_CategoryPath) = True Then
                                _Result = True
                            End If
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        IsExists = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                        '   oPathFolder = Nothing
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
                        oDB.Connect(GetConnectionString)
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
                        'oDB.Disconnect()
                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Modify, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        IsModify = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oDataReader = Nothing
                    End Try
                End Function

                'Check Category Delete or not
                Public Function IsDelete(ByVal oCategory As Category) As Boolean
                    Dim _Result As Boolean = True
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    '  Dim oPathFolder As New gloStream.gloDMS.Supporting.PathsAndFolders
                    Dim _CategoryPath As String
                    Dim _Records As Integer = 0

                    Try
                        'Check Category exists in database or not
                        oDB.Connect(GetConnectionString)
                        'Document ID
                        oDB.DBParameters.Add("@Category", oCategory.Name, ParameterDirection.Input, SqlDbType.VarChar)

                        _Records = CInt(oDB.ExecuteScaler("gsp_DMS_IsCategoryDelete"))
                        If _Records > 0 Then
                            _Result = False
                        End If
                        'oDB.Disconnect()

                        'Check category exists in explorer or not
                        _CategoryPath = Supporting.PathsAndFolders.GetContainerPath(DMSRootPath) & "\" & oCategory.Name
                        If Directory.Exists(_CategoryPath) = True Then
                            Dim oFolder As DirectoryInfo = New DirectoryInfo(_CategoryPath)
                            Dim oFolders As DirectoryInfo() = oFolder.GetDirectories()

                            If oFolders.Length > 0 Then
                                _Result = False
                            End If
                        End If

                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        IsDelete = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        '  oPathFolder = Nothing
                        _CategoryPath = Nothing
                    End Try
                End Function

                'Category List
                Public Function Categories(ByVal CategoryType As enumCategoryType) As Categories
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim oCategory As Category
                    Dim oCategories As New Categories
                    Dim oDataReader As SqlDataReader                        ' Data Reader
                    Dim oDocument As New gloStream.gloDMS.Document.Document

                    Try

                        oDB.Connect(GetConnectionString)
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

                        ' oDB.Disconnect()
                        Return oCategories
                    Catch objError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Modify, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        If oDB.ErrorMessage <> "" Then
                            _ErrorMessage = oDB.ErrorMessage
                        Else
                            _ErrorMessage = objError.Message
                        End If
                        Categories = Nothing
                        Exit Function
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
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
                    Dim oDocument As New gloStream.gloDMS.Document.Document

                    _strSQL = "SELECT DISTINCT DMS_Category_MST.CategoryId, DMS_Category_MST.CategoryName, DMS_Category_MST.IsDeleted " _
                    & " FROM DMS_MST INNER JOIN DMS_Category_MST ON DMS_MST.Category = DMS_Category_MST.CategoryName " _
                    & " WHERE (DMS_MST.PatientID = " & PatientID & ") AND (DMS_Category_MST.CategoryName IS NOT NULL) AND (DMS_Category_MST.IsDeleted = 0) " _
                    & " ORDER BY DMS_Category_MST.CategoryName"

                    Try

                        oDB.Connect(GetConnectionString)
                        'oDB.DBParameters.Add("@GetStatus", CategoryType, ParameterDirection.Input, SqlDbType.Int)
                        'oDataReader = oDB.ReadRecords("sp_DMS_FillCategories")
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        If oDB.ErrorMessage <> "" Then
                            _ErrorMessage = oDB.ErrorMessage
                        Else
                            _ErrorMessage = objError.Message
                        End If
                        Categories = Nothing
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
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
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