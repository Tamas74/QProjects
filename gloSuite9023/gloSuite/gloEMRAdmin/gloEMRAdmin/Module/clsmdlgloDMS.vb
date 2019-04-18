'Imports C1.Win.C1FlexGrid
'Imports C1.Win.C1FlexGrid.StyleElementFlags
Imports System.IO
Imports System.Data.SqlClient

Module mdlgloDMS

#Region "Category Column"
    Public Const COL_ID = 0 ' ID
    Public Const COL_NAME = 1 ' Name
    Public Const COL_EXTENSION = 2 ' Name
    Public Const COL_SOURCEMACHINE = 3 ' Source Machine
    Public Const COL_SYSTEMFOLDER = 4 ' System Folder
    Public Const COL_CONTAINER = 5 ' Container
    Public Const COL_CATEGORY = 6 ' Category
    Public Const COL_PATIENTID = 7 ' Patient ID
    Public Const COL_YEAR = 8 ' Year
    Public Const COL_MONTH = 9 ' Month
    Public Const COL_DOCUMENTFORMAT = 10 ' Month
    Public Const COL_SOURCEBIN = 11 ' Source Bin
    Public Const COL_PAGES = 12 ' Source Bin
    Public Const COL_ARCHSTATUS = 13 ' Source Bin
    Public Const COL_ARCHDESC = 14 ' Source Bin
    Public Const COL_USEDSTATUS = 15 ' In Used
    Public Const COL_USEDMACHINE = 16 ' Used Machine
    Public Const COL_USEDTYPE = 17 ' Used Type
    Public Const COL_DOCUMENTTYPE = 18 ' Used Type
    Public Const COL_DOCUMENTFILENAME = 19 ' File Name
    Public Const COL_MACHINEID = 20 ' File Name
    Public Const COL_MODIFIED = 21 ' File Name
    Public Const COL_SYNCHRONIZED = 22 ' File Name
    Public Const COL_PATH = 23 ' Path
    Public Const COL_VERSIONNO = 24 ' Version No
    Public Const COL_MODYDATETIME = 25 ' Modified Date Time
    Public Const COL_SYNCHSTATUS = 26 ' File Name
    Public Const COL_ISREVIWED = 27 ' File Name
    Public Const COL_COUNT = 28
#End Region

    Public DMSPublisherRootPath As String ' Please check this varible in gloDMS class befire chnage to it
    Public DMSSubscriberRootPath As String ' Please check this varible in gloDMS class befire chnage to it

    Public Enum enumColType
        Category = 0
        Month = 1
        Document = 2
    End Enum

    Public Enum MenuEventType
        NewDocument = 0
        MergeWithDocument = 1
        MergeInExisting = 2
    End Enum

    Public gSynchronizeMode As gloStream.gloDMS.gloSync.Subscriber.SynchronizationMode = gloStream.gloDMS.gloSync.Subscriber.SynchronizationMode.Winner
    Public gSynchronizeWinner As gloStream.gloDMS.gloSync.Subscriber.SynchronizeWinner = gloStream.gloDMS.gloSync.Subscriber.SynchronizeWinner.Publisher

    'Public gstrMessageBoxCaption As String = "gloEMR Synchronization"

    'Enum enmConnectionAuthentication
    '    WindowsAuthentication
    '    SQLAuthentication
    'End Enum

    'Enum ConnectionType
    '    None = 0
    '    Publisher = 1
    '    Subscriber = 2
    'End Enum

    Friend gPublisherConnectionString As String = ""
    Friend gSubscriberConnectionString As String = ""

    '//Document Name Changes Aug 2006//
    Public Function GetPrefixTransactionID(ByVal PatientID As Long, ByVal enumConnectionType As gloStream.gloDMS.gloSync.Subscriber.ConnectionType) As Long
        Dim PatientDOB As DateTime
        Dim strID As String
        Dim dtDate As DateTime

        'Get Patient Date Of Birth
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(GetConnectionString_DMS(enumConnectionType))
        PatientDOB = oDB.ExecuteQueryScaler("SELECT dtDOB FROM Patient WHERE nPatientID = " & PatientID & "")
        oDB.Disconnect()
        oDB = Nothing


        dtDate = System.DateTime.Now
        strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date)
        Return CLng(strID)
    End Function

    'To Build Connection String for connecting to SQL Server - By Windows Authentication or SQL Server Authentication
    Friend Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal enmConnectionType As gloStream.gloDMS.gloSync.Subscriber.enmConnectionAuthentication, Optional ByVal strUserName As String = "", Optional ByVal strPassword As String = "") As String
        ' Variable to store SQL Connection String
        Dim strConnectionString As String

        'Check the SQL Server Authentication
        If enmConnectionType = gloStream.gloDMS.gloSync.Subscriber.enmConnectionAuthentication.WindowsAuthentication Then
            'Build Connection String by Windows Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            'Build Connection String by SQL Server Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";UID=" & strUserName & ";PWD=" & strPassword & ";DATABASE=" & strDatabase & ""
        End If

        'Return Builded connection string
        Return strConnectionString
    End Function

    'To Build Connection String for connecting to SQL Server - By Windows Authentication or SQL Server Authentication
    Friend Function GetConnectionString_DMS(ByVal enumConnectionType As gloStream.gloDMS.gloSync.Subscriber.ConnectionType) As String
        Select Case enumConnectionType
            Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.None
                Return ""
            Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Publisher
                Return gPublisherConnectionString
            Case gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Subscriber
                Return gSubscriberConnectionString
        End Select
    End Function

    Public Sub UpdateLog(ByVal strLogMessage As String)
        Try

            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRFAXLogTest.txt", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub

    'Check  -- Connection to SQL Server database can be establish or not by using connection string
    ' Returns True - Connection to SQL Server database can be successfully established
    ' Returns False - Connection to SQL Server database can't be established
    'If the connection is not establish then it generates the error and pointer will be moved to catch block
    Friend Function IsConnect(ByVal strConnectionString As String) As Boolean
        'Create the object of SQL Connection class
        Dim objCon As New SqlConnection
        Try
            'Assign the connection string
            objCon.ConnectionString = strConnectionString
            'Open the connection
            objCon.Open()

            'Connection to SQL Server database successfully established
            Return True
        Catch ex As Exception

            Return False
        Finally
            'Close the  connection
            objCon.Close()
            'Connection to SQL Server database is not established
            objCon = Nothing
        End Try

    End Function


    'Public Sub Fill_Categories(ByVal FillControl As C1FlexGrid)
    '    Dim oCategories As New gloStream.gloDMS.DocumentCategory.Categories
    '    Dim oDocumentCategory As New gloStream.gloDMS.DocumentCategory.DocumentCategory
    '    Dim i As Integer

    '    Try
    '        With FillControl
    '            .Clear(ClearFlags.Content)
    '            oCategories = oDocumentCategory.Categories(gloStream.gloDMS.DocumentCategory.enumCategoryType.NotDeleted)
    '            If oCategories.Count <> 0 Then
    '                .Tree.Column = COL_CAT_NAME
    '                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '                .Tree.Indent = 15
    '                .Cols(0).AllowEditing = False
    '                .Cols(1).AllowEditing = False
    '                .AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
    '                '.Redraw = False

    '                For i = 1 To oCategories.Count
    '                    .Rows.Add()
    '                    'Category Node
    '                    With .Rows(.Rows.Count - 1)
    '                        .ImageAndText = True
    '                        .Height = 24
    '                        .IsNode = True
    '                        .Style = FillControl.Styles("CS_Category")
    '                        .Node.Level = 0
    '                        .Node.Image = oImage.Img_Category.Image
    '                        '.Node.Image = Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Category\Category3.JPG")
    '                        .Node.Data = oCategories(i).Name
    '                    End With
    '                    .SetData(.Rows.Count - 1, COL_CAT_COLTYPE, CType(enumColType.Category, Integer))

    '                Next
    '            End If
    '        End With
    '    Catch objError As Exception
    '        If oDocumentCategory.ErrorMessage <> "" Then
    '            MessageBox.Show(oDocumentCategory.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Else
    '            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        End If
    '        Exit Sub
    '    Finally
    '        oCategories = Nothing
    '        oDocumentCategory = Nothing
    '    End Try
    'End Sub

    ''Year & Month Wise
    'Public Sub Fill_CategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Category As String, ByVal Year As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oCategorisedDocuments As New gloStream.gloDMS.Document.CategorisedDocuments
    '    Dim i As Integer, nCat As Integer, nStyleRow As Integer, nMonth As Integer
    '    Dim nCatStart As Integer, nCatEnd As Integer, nCatCount As Integer
    '    Dim sMonths As New Collection
    '    'Dim oNode As Node

    '    Try
    '        With FillControl
    '            For nCat = 0 To .Rows.Count - 1
    '                If UCase(Trim(.GetData(nCat, COL_CAT_NAME) & "")) = UCase(Category) Then
    '                    '---Clear Node - its temporary solution please find node clear solution of grid - Remark
    '                    Dim oRange As New C1.Win.C1FlexGrid.CellRange
    '                    oRange = .Rows(nCat).Node.GetCellRange
    '                    nCatStart = oRange.TopRow : nCatEnd = oRange.BottomRow : nCatCount = nCatEnd - nCatStart
    '                    If nCatStart <> nCatEnd Then
    '                        oRange.r1 = nCatStart + 1
    '                        .Rows.RemoveRange(oRange.r1, nCatCount)
    '                    End If
    '                    oRange = Nothing

    '                    nStyleRow = nCat ' Set Category Node

    '                    '---Get Months
    '                    sMonths = oDMS.DocumentMonths(PatientID, Category, Year, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument)

    '                    '---Fill Months & then documents in months folder
    '                    For nMonth = 1 To sMonths.Count
    '                        oCategorisedDocuments = oDMS.CategorisedDocuments(PatientID, Category, Year, sMonths(nMonth))
    '                        If Not oCategorisedDocuments Is Nothing Then
    '                            If oCategorisedDocuments.Count > 0 Then
    '                                'Fill Month
    '                                nStyleRow = nStyleRow + 1
    '                                .Rows(nCat).Node.AddNode(NodeTypeEnum.LastChild, Space(7) & sMonths(nMonth), nMonth, oImage.Img_Month.Image) '   Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Category\Line5.JPG"))
    '                                With .Rows(nStyleRow)
    '                                    .ImageAndText = True
    '                                    .Height = 25
    '                                    .Style = FillControl.Styles("CS_Folder")
    '                                End With
    '                                .SetData(nStyleRow, COL_CAT_COLTYPE, CType(enumColType.Month, Integer))

    '                                'Fill Document
    '                                Dim oC1Node As C1.Win.C1FlexGrid.Node = .Rows(nCat).Node.GetNode(NodeTypeEnum.LastChild)
    '                                For i = 1 To oCategorisedDocuments.Count
    '                                    nStyleRow = nStyleRow + 1
    '                                    oC1Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document.Image) 'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Document2.JPG"))
    '                                    'Show Note or Not
    '                                    If oDocument.HasNote(oCategorisedDocuments(i)) = True Then
    '                                        .SetCellImage(nStyleRow, COL_CAT_NOTEFLAG, oImage.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                    End If
    '                                    'Other Document Property
    '                                    .SetData(nStyleRow, COL_CAT_SOURCEMACHINE, oCategorisedDocuments(i).SourceMachine)  ' Source Machine
    '                                    .SetData(nStyleRow, COL_CAT_SYSTEMFOLDER, oCategorisedDocuments(i).SystemFolder)    ' System Folder
    '                                    .SetData(nStyleRow, COL_CAT_CONTAINER, oCategorisedDocuments(i).Container)          ' Container
    '                                    .SetData(nStyleRow, COL_CAT_CATEGORY, oCategorisedDocuments(i).Category)            ' Category
    '                                    .SetData(nStyleRow, COL_CAT_PATIENTID, oCategorisedDocuments(i).PatientID)          ' Patient ID
    '                                    .SetData(nStyleRow, COL_CAT_YEAR, oCategorisedDocuments(i).Year)                    ' Year
    '                                    .SetData(nStyleRow, COL_CAT_MONTH, oCategorisedDocuments(i).Month)                  ' Month
    '                                    .SetData(nStyleRow, COL_CAT_SOURCEBIN, oCategorisedDocuments(i).SourceBin)          ' Source Bin
    '                                    .SetData(nStyleRow, COL_CAT_INUSED, oCategorisedDocuments(i).InUsed)                ' In Used
    '                                    .SetData(nStyleRow, COL_CAT_USEDMACHINE, oCategorisedDocuments(i).UsedMachine)      ' Used Machine
    '                                    .SetData(nStyleRow, COL_CAT_USEDTYPE, oCategorisedDocuments(i).UsedType)            ' Used Type
    '                                    .SetData(nStyleRow, COL_CAT_PATH, oCategorisedDocuments(i).Path)                    ' Path
    '                                    .SetData(nStyleRow, COL_CAT_COLTYPE, CType(enumColType.Document, Integer))
    '                                    .SetData(nStyleRow, COL_CAT_FILENAME, oCategorisedDocuments(i).FileName)
    '                                    .SetData(nStyleRow, COL_CAT_MACHINEID, oCategorisedDocuments(i).MachineID)

    '                                    With .Rows(nStyleRow)
    '                                        .ImageAndText = True
    '                                        .Height = 25
    '                                        .Style = FillControl.Styles("CS_File")
    '                                    End With
    '                                Next
    '                                oC1Node = Nothing
    '                            End If
    '                        End If
    '                    Next
    '                    Exit For
    '                End If ' If UCase(Trim(.GetData(nCat, COL_CAT_DOCNAME) & "")) = UCase(Category) Then
    '            Next ' For nCat = 1 To .Rows.Count - 1
    '        End With
    '    Catch objError As Exception
    '        If oDMS.ErrorMessage <> "" Then
    '            MessageBox.Show(oDMS.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Else
    '            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        End If
    '        Exit Sub
    '    Finally
    '        oDMS = Nothing
    '        oDocument = Nothing
    '        oCategorisedDocuments = Nothing
    '        sMonths = Nothing
    '    End Try
    'End Sub

    ''Without Year & Month Wise
    'Public Sub Fill_CategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Category As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oCategorisedDocuments As New gloStream.gloDMS.Document.CategorisedDocuments
    '    Dim i As Integer, nCat As Integer, nStyleRow As Integer, nMonth As Integer
    '    Dim nCatStart As Integer, nCatEnd As Integer, nCatCount As Integer
    '    Dim sMonths As New Collection
    '    'Dim oNode As Node

    '    Try
    '        With FillControl
    '            For nCat = 0 To .Rows.Count - 1
    '                If UCase(Trim(.GetData(nCat, COL_CAT_NAME) & "")) = UCase(Category) Then
    '                    '---Clear Node - its temporary solution please find node clear solution of grid - Remark
    '                    Dim oRange As New C1.Win.C1FlexGrid.CellRange
    '                    oRange = .Rows(nCat).Node.GetCellRange
    '                    nCatStart = oRange.TopRow : nCatEnd = oRange.BottomRow : nCatCount = nCatEnd - nCatStart
    '                    If nCatStart <> nCatEnd Then
    '                        oRange.r1 = nCatStart + 1
    '                        .Rows.RemoveRange(oRange.r1, nCatCount)
    '                    End If
    '                    oRange = Nothing

    '                    nStyleRow = nCat ' Set Category Node

    '                    '---Fill Months & then documents in months folder
    '                    oCategorisedDocuments = oDMS.CategorisedDocuments(PatientID, Category)

    '                    If Not oCategorisedDocuments Is Nothing Then
    '                        If oCategorisedDocuments.Count > 0 Then
    '                            'Fill Document
    '                            For i = 1 To oCategorisedDocuments.Count
    '                                nStyleRow = nStyleRow + 1
    '                                .Rows(nCat).Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document.Image)
    '                                'Show Note or Not
    '                                If oDocument.HasNote(oCategorisedDocuments(i)) = True Then
    '                                    .SetCellImage(nStyleRow, COL_CAT_NOTEFLAG, oImage.Img_Note.Image)
    '                                End If
    '                                'Other Document Property
    '                                .SetData(nStyleRow, COL_CAT_SOURCEMACHINE, oCategorisedDocuments(i).SourceMachine)  ' Source Machine
    '                                .SetData(nStyleRow, COL_CAT_SYSTEMFOLDER, oCategorisedDocuments(i).SystemFolder)    ' System Folder
    '                                .SetData(nStyleRow, COL_CAT_CONTAINER, oCategorisedDocuments(i).Container)          ' Container
    '                                .SetData(nStyleRow, COL_CAT_CATEGORY, oCategorisedDocuments(i).Category)            ' Category
    '                                .SetData(nStyleRow, COL_CAT_PATIENTID, oCategorisedDocuments(i).PatientID)          ' Patient ID
    '                                .SetData(nStyleRow, COL_CAT_YEAR, oCategorisedDocuments(i).Year)                    ' Year
    '                                .SetData(nStyleRow, COL_CAT_MONTH, oCategorisedDocuments(i).Month)                  ' Month
    '                                .SetData(nStyleRow, COL_CAT_SOURCEBIN, oCategorisedDocuments(i).SourceBin)          ' Source Bin
    '                                .SetData(nStyleRow, COL_CAT_INUSED, oCategorisedDocuments(i).InUsed)                ' In Used
    '                                .SetData(nStyleRow, COL_CAT_USEDMACHINE, oCategorisedDocuments(i).UsedMachine)      ' Used Machine
    '                                .SetData(nStyleRow, COL_CAT_USEDTYPE, oCategorisedDocuments(i).UsedType)            ' Used Type
    '                                .SetData(nStyleRow, COL_CAT_PATH, oCategorisedDocuments(i).Path)                    ' Path
    '                                .SetData(nStyleRow, COL_CAT_COLTYPE, CType(enumColType.Document, Integer))
    '                                .SetData(nStyleRow, COL_CAT_FILENAME, oCategorisedDocuments(i).FileName)
    '                                .SetData(nStyleRow, COL_CAT_MACHINEID, oCategorisedDocuments(i).MachineID)

    '                                With .Rows(nStyleRow)
    '                                    .ImageAndText = True
    '                                    .Height = 25
    '                                    .Style = FillControl.Styles("CS_File")
    '                                End With
    '                            Next ' For i = 1 To oCategorisedDocuments.Count
    '                        End If ' If oCategorisedDocuments.Count > 0 Then
    '                    End If ' If Not oCategorisedDocuments Is Nothing Then
    '                End If ' If UCase(Trim(.GetData(nCat, COL_CAT_DOCNAME) & "")) = UCase(Category) Then
    '            Next ' For nCat = 1 To .Rows.Count - 1
    '        End With
    '    Catch objError As Exception
    '        If oDMS.ErrorMessage <> "" Then
    '            MessageBox.Show(oDMS.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Else
    '            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        End If
    '        Exit Sub
    '    Finally
    '        oDMS = Nothing
    '        oDocument = Nothing
    '        oCategorisedDocuments = Nothing
    '        sMonths = Nothing
    '    End Try
    'End Sub

    ''Year & Month Wise
    'Public Sub Fill_UncategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Year As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oUncategorisedDocuments As New gloStream.gloDMS.Document.UncategorisedDocuments
    '    Dim i As Integer, nMonth As Integer
    '    Dim sMonths As New Collection

    '    Try
    '        With FillControl
    '            .Clear(ClearFlags.Content)
    '            .Rows.Count = 0
    '            .Tree.Column = COL_UNCAT_NAME
    '            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '            .Tree.Indent = 15
    '            .Cols(0).AllowEditing = False
    '            .Cols(1).AllowEditing = False
    '            .AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes

    '            '---Get Months
    '            sMonths = oDMS.DocumentMonths(PatientID, "", Year, gloStream.gloDMS.Supporting.enumDocumentType.UnCategorisedDocument)

    '            '---Fill Months & then documents in months folder
    '            For nMonth = 1 To sMonths.Count
    '                oUncategorisedDocuments = oDMS.UnCategorisedDocuments(PatientID, Year, sMonths(nMonth))
    '                If Not oUncategorisedDocuments Is Nothing Then
    '                    If oUncategorisedDocuments.Count > 0 Then
    '                        .Rows.Add()
    '                        With .Rows(.Rows.Count - 1)
    '                            .ImageAndText = True
    '                            .Height = 25
    '                            .IsNode = True
    '                            .Style = FillControl.Styles("CS_Folder")
    '                            .Node.Level = 0
    '                            .Node.Image = oImage.Img_MonthUncategory.Image
    '                            .Node.Data = Space(7) & sMonths(nMonth)
    '                        End With
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_COLTYPE, CType(enumColType.Month, Integer))

    '                        'Fill Document
    '                        For i = 1 To oUncategorisedDocuments.Count
    '                            .Rows.Add()
    '                            With .Rows(.Rows.Count - 1)
    '                                'Document Information
    '                                .ImageAndText = True
    '                                .Height = 25
    '                                .IsNode = True
    '                                .Style = FillControl.Styles("CS_File")
    '                                .Node.Level = 1
    '                                .Node.Image = oImage.Img_Document.Image
    '                                .Node.Data = oUncategorisedDocuments(i).Name
    '                            End With

    '                            'Show Note or Not
    '                            If oDocument.HasNote(oUncategorisedDocuments(i)) = True Then
    '                                .SetCellImage(.Rows.Count - 1, COL_UNCAT_NOTEFLAG, oImage.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                            End If
    '                            'Other Document Property
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEMACHINE, oUncategorisedDocuments(i).SourceMachine)  ' Source Machine
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_SYSTEMFOLDER, oUncategorisedDocuments(i).SystemFolder)    ' System Folder
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_CONTAINER, oUncategorisedDocuments(i).Container)          ' Container
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_PATIENTID, oUncategorisedDocuments(i).PatientID)          ' Patient ID
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_YEAR, oUncategorisedDocuments(i).Year)                    ' Year
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_MONTH, oUncategorisedDocuments(i).Month)                  ' Month
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEBIN, oUncategorisedDocuments(i).SourceBin)          ' Source Bin
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_INUSED, oUncategorisedDocuments(i).InUsed)                ' In Used
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_USEDMACHINE, oUncategorisedDocuments(i).UsedMachine)      ' Used Machine
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_USEDTYPE, oUncategorisedDocuments(i).UsedType)            ' Used Type
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_PATH, oUncategorisedDocuments(i).Path)                    ' Path
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_COLTYPE, CType(enumColType.Document, Integer))
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_FILENAME, oUncategorisedDocuments(i).FileName)
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_MACHINEID, oUncategorisedDocuments(i).MachineID)

    '                        Next
    '                    End If
    '                End If
    '            Next
    '        End With
    '    Catch objError As Exception
    '        If oDMS.ErrorMessage <> "" Then
    '            MessageBox.Show(oDMS.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Else
    '            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        End If
    '        Exit Sub
    '    Finally
    '        oDMS = Nothing
    '        oDocument = Nothing
    '        oUncategorisedDocuments = Nothing
    '        sMonths = Nothing
    '    End Try
    'End Sub

    ''Without Year & Month Wise
    'Public Sub Fill_UncategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oUncategorisedDocuments As New gloStream.gloDMS.Document.UncategorisedDocuments
    '    Dim i As Integer, nMonth As Integer
    '    Dim sMonths As New Collection

    '    Try
    '        With FillControl
    '            .Clear(ClearFlags.Content)
    '            .Rows.Count = 0
    '            .Tree.Column = COL_UNCAT_NAME
    '            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '            .Tree.Indent = 15
    '            .Cols(0).AllowEditing = False
    '            .Cols(1).AllowEditing = False
    '            .AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes

    '            '---Fill Months & then documents in months folder
    '            oUncategorisedDocuments = oDMS.UnCategorisedDocuments(PatientID)

    '            If Not oUncategorisedDocuments Is Nothing Then
    '                If oUncategorisedDocuments.Count > 0 Then
    '                    For i = 1 To oUncategorisedDocuments.Count
    '                        .Rows.Add()
    '                        With .Rows(.Rows.Count - 1)
    '                            'Document Information
    '                            .ImageAndText = True
    '                            .Height = 25
    '                            .IsNode = True
    '                            .Style = FillControl.Styles("CS_File")
    '                            .Node.Level = 1
    '                            .Node.Image = oImage.Img_Document.Image
    '                            .Node.Data = oUncategorisedDocuments(i).Name
    '                        End With

    '                        'Show Note or Not
    '                        If oDocument.HasNote(oUncategorisedDocuments(i)) = True Then
    '                            .SetCellImage(.Rows.Count - 1, COL_UNCAT_NOTEFLAG, oImage.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                        End If
    '                        'Other Document Property
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEMACHINE, oUncategorisedDocuments(i).SourceMachine)  ' Source Machine
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_SYSTEMFOLDER, oUncategorisedDocuments(i).SystemFolder)    ' System Folder
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_CONTAINER, oUncategorisedDocuments(i).Container)          ' Container
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_PATIENTID, oUncategorisedDocuments(i).PatientID)          ' Patient ID
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_YEAR, oUncategorisedDocuments(i).Year)                    ' Year
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_MONTH, oUncategorisedDocuments(i).Month)                  ' Month
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEBIN, oUncategorisedDocuments(i).SourceBin)          ' Source Bin
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_INUSED, oUncategorisedDocuments(i).InUsed)                ' In Used
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_USEDMACHINE, oUncategorisedDocuments(i).UsedMachine)      ' Used Machine
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_USEDTYPE, oUncategorisedDocuments(i).UsedType)            ' Used Type
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_PATH, oUncategorisedDocuments(i).Path)                    ' Path
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_COLTYPE, CType(enumColType.Document, Integer))
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_FILENAME, oUncategorisedDocuments(i).FileName)
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_MACHINEID, oUncategorisedDocuments(i).MachineID)
    '                    Next ' For i = 1 To oUncategorisedDocuments.Count
    '                End If ' If oUncategorisedDocuments.Count > 0 Then
    '            End If ' If Not oUncategorisedDocuments Is Nothing Then

    '        End With
    '    Catch objError As Exception
    '        If oDMS.ErrorMessage <> "" Then
    '            MessageBox.Show(oDMS.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Else
    '            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        End If
    '        Exit Sub
    '    Finally
    '        oDMS = Nothing
    '        oDocument = Nothing
    '        oUncategorisedDocuments = Nothing
    '        sMonths = Nothing
    '    End Try
    'End Sub

End Module

