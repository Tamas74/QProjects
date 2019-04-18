Imports C1.Win.C1FlexGrid
Imports C1.Win.C1FlexGrid.StyleElementFlags

Module mdlgloDMS

#Region "DMS Default Category"
    Dim _blnAskForDefaultCategory As Boolean = True

    Dim _mDC_PatientDirective As String = "Patient Directive"
    Dim _mDC_DisabilityForms As String = "Disability Forms"
    Dim _mDC_RegistrationForms As String = "Registration Forms"
    Dim _mDC_Correspondence As String = "Correspondence"
    Dim _mDC_XRayReports As String = "X-Ray Reports"
    Dim _mDC_SurgeryInformation As String = "Surgery Information"
    Dim _mDC_LabResults As String = "Lab Results"
    Dim _mDC_ReferralsConsults As String = "Referrals/Consults"
    Dim _mDC_OPReports As String = "OP Reports"
    Dim _mDC_FinanceAgreement As String = "Finance Agreement"
    Dim _mDC_InsuranceCards As String = "Insurance Cards"
    Dim _mDC_MedicalRecords As String = "Medical Records"
    Dim _mDC_OfficeNotes As String = "Office Notes"
    Dim _mDC_HealthHistory As String = "Health History"
    Dim _mDC_PhysicalTherapy As String = "Physical Therapy"
    Dim _mDC_TestResults As String = "Test Results"
    Dim _mDC_Miscellaneous As String = "Miscellaneous"
    Dim _mDC_DEXA As String = "DEXA"

#End Region

#Region "Category Column"
    Public Const COL_CAT_ID = 0 ' ID
    Public Const COL_CAT_NAME = 1 ' Name
    Public Const COL_CAT_NOTEFLAG = 2 ' Note Flag
    Public Const COL_CAT_EXTRAFLAG = 3 ' Extra Col
    Public Const COL_CAT_SOURCEMACHINE = 4 ' Source Machine
    Public Const COL_CAT_SYSTEMFOLDER = 5 ' System Folder
    Public Const COL_CAT_CONTAINER = 6 ' Container
    Public Const COL_CAT_CATEGORY = 7 ' Category
    Public Const COL_CAT_PATIENTID = 8 ' Patient ID
    Public Const COL_CAT_YEAR = 9 ' Year
    Public Const COL_CAT_MONTH = 10 ' Month
    Public Const COL_CAT_SOURCEBIN = 11 ' Source Bin
    Public Const COL_CAT_INUSED = 12 ' In Used
    Public Const COL_CAT_USEDMACHINE = 13 ' Used Machine
    Public Const COL_CAT_USEDTYPE = 14 ' Used Type
    Public Const COL_CAT_PATH = 15 ' Path
    Public Const COL_CAT_COLTYPE = 16
    Public Const COL_CAT_FILENAME = 17 ' File Name
    Public Const COL_CAT_MACHINEID = 18 ' File Name
    Public Const COL_CAT_VERSIONNO = 19 ' Version No
    Public Const COL_CAT_ISREVIWED = 20 ' Reviwed
    Public Const COL_CAT_REVIWEDFLAG = 21 ' Reviwed
    Public Const COL_CAT_COUNT = 22
#End Region

#Region "Uncategory Column"
    Public Const COL_UNCAT_ID = 0 ' ID
    Public Const COL_UNCAT_NAME = 1 ' Name
    Public Const COL_UNCAT_NOTEFLAG = 2 ' Note Flag
    Public Const COL_UNCAT_EXTRAFLAG = 3 ' Extra Col
    Public Const COL_UNCAT_SOURCEMACHINE = 4 ' Source Machine
    Public Const COL_UNCAT_SYSTEMFOLDER = 5 ' System Folder
    Public Const COL_UNCAT_CONTAINER = 6 ' Container
    Public Const COL_UNCAT_PATIENTID = 7 ' Patient ID
    Public Const COL_UNCAT_YEAR = 8 ' Year
    Public Const COL_UNCAT_MONTH = 9 ' Month
    Public Const COL_UNCAT_SOURCEBIN = 10 ' Source Bin
    Public Const COL_UNCAT_INUSED = 11 ' In Used
    Public Const COL_UNCAT_USEDMACHINE = 12 ' Used Machine
    Public Const COL_UNCAT_USEDTYPE = 13 ' Used Type
    Public Const COL_UNCAT_PATH = 14 ' Path
    Public Const COL_UNCAT_COLTYPE = 15
    Public Const COL_UNCAT_FILENAME = 16 ' File Name
    Public Const COL_UNCAT_MACHINEID = 17 ' File Name
    Public Const COL_UNCAT_VERSIONNO = 18 ' Version No
    Public Const COL_UNCAT_ISREVIWED = 19 ' Reviwed
    Public Const COL_UNCAT_REVIWEDFLAG = 20 ' Reviwed
    Public Const COL_UNCAT_COUNT = 21
#End Region

#Region "Context Menu Constant Caption"
    Public gMnu_SendToNewFile As String = "Send to new file"            ' Shortcut - Ctrl + Shift + N
    Public gMnu_SendToMergeFile As String = "Merge in existing file"    ' Shortcut - Ctrl + Shift + M
    'Merge in Existing Document with document name                      ' Shortcut - Ctrl + Shift + E
    'Send To Category                                                   ' Shortcut - Ctrl + Shift + C
    'Send To New File Without Month Name                                ' Shortcut - Ctrl + Shift + W

    Public gMnu_SendToPrint As String = "Print"                         ' Shortcut - Ctrl + Shift + P
    Public gMnu_SendToPrintAll As String = "Print All"                  ' Shortcut - Ctrl + Shift + R
    Public gMnu_SendToFax As String = "Fax"                             ' Shortcut - Ctrl + Shift + F
    Public gMnu_SendToFaxAll As String = "Fax All"                      ' Shortcut - Ctrl + Shift + X

    Public gMnu_SendToGeneralBin As String = "Send to General Bin"
    Public gMnu_SendToAnotherPatient As String = "Send to another patient"

#Region "Following are cover in command button in current version"
    Public gMnu_SendToDelete As String = "Delete Page"
    Public gMnu_AddNotes As String = "Add Note"
    Public gMnu_DeleteNote As String = "Delete Note"
    Public gMnu_SelectAllPages As String = "Select All"
    Public gMnu_PrintFile As String = "Print"
    Public gMnu_FaxFile As String = "Fax"
    Public gMnu_PrintFileAll As String = "Print All"
    Public gMnu_FaxFileAll As String = "Fax All"
    Public gMnu_PrintFaxFile As String = "Print and Fax"
#End Region

#End Region

#Region "DMS Settings Variables"
    'Month View
    Public DMS_ScanDoc_MonthView As Boolean = True
    Public DMS_ViewDoc_MonthView As Boolean = True

    'DMS Root Path for globally
    Public DMSRootPath As String = ""  ' Please check this varible in gloDMS class before chnage to it

#End Region

#Region "VMS Settings Variables"
    '' VMS Root Path 
    Public VMSRootPath As String = "" ' =      "\\sakarserver\gloVideo Library 4.0\"
    Public VMSSystem As String = "VMS System"
    Public VMSContainer As String = "Container"
    Public VMSCategory As String = "General"

#End Region

#Region "DMS Settings Name in Registry"
    Public Const DMSReg_MainName As String = "gloEMR"
    Public Const DMSReg_RootPath As String = "DMSPath"
    Public Const DMSReg_ScanDocumentMonthView As String = "DMS_ScanDoc_MonthView"
    Public Const DMSReg_ViewDocumentMonthView As String = "DMS_ViewDoc_MonthView"
    Public Const DMSReg_GeneralBinMergeMenu As String = "DMS_GenBin_MergeMenu"
    Public Const DMSReg_CategoryDocMergeMenu As String = "DMS_CatDoc_MergeMenu"
    Public Const DMSReg_GeneralBin12MonthMenu As String = "DMS_GenBin_12MonthMenu"
    Public Const DMSReg_Category12MonthMenu As String = "DMS_CatDoc_12MonthMenu"
#End Region

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

    'Dim oImage As New frmDMS_Support

    Public Sub Fill_Categories(ByVal FillControl As C1FlexGrid)
        Dim oCategories As gloStream.gloDMS.DocumentCategory.Categories = Nothing
        Dim oDocumentCategory As New gloStream.gloDMS.DocumentCategory.DocumentCategory
        Dim i As Integer
        Dim oImage As New frmDMS_Support

        Try
            With FillControl
                .Clear(ClearFlags.Content)
                oCategories = oDocumentCategory.Categories(gloStream.gloDMS.DocumentCategory.enumCategoryType.NotDeleted)
                If oCategories.Count <> 0 Then
                    .Tree.Column = COL_CAT_NAME
                    .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                    .Tree.Indent = 15
                    .Cols(0).AllowEditing = False
                    .Cols(1).AllowEditing = False
                    .AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
                    '.Redraw = False

                    For i = 1 To oCategories.Count
                        .Rows.Add()
                        'Category Node
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Image = oImage.Img_Category.Image
                            .Node.Data = oCategories(i).Name
                        End With
                        .SetData(.Rows.Count - 1, COL_CAT_COLTYPE, CType(enumColType.Category, Integer))

                    Next
                End If
            End With
        Catch objError As Exception
            If oDocumentCategory.ErrorMessage <> "" Then
                MessageBox.Show(oDocumentCategory.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
            Else
                MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
            Exit Sub
        Finally
            If (IsNothing(oCategories) = False) Then
                oCategories.Clear()
            End If
            oCategories = Nothing
            If (IsNothing(oDocumentCategory) = False) Then
                oDocumentCategory = Nothing
            End If
            oDocumentCategory = Nothing

            oImage.Dispose()
            oImage = Nothing
        End Try
    End Sub

    Public Sub Fill_Categories(ByVal FillControl As C1FlexGrid, ByVal PatientID As Long)
        Dim oCategories As gloStream.gloDMS.DocumentCategory.Categories = Nothing
        Dim oDocumentCategory As New gloStream.gloDMS.DocumentCategory.DocumentCategory
        Dim i As Integer
        Dim oImage As New frmDMS_Support
        Try
            With FillControl
                .Clear(ClearFlags.Content)
                oCategories = oDocumentCategory.Categories(gloStream.gloDMS.DocumentCategory.enumCategoryType.NotDeleted, PatientID)
                If oCategories.Count <> 0 Then
                    .Tree.Column = COL_CAT_NAME
                    .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                    .Tree.Indent = 15
                    .Cols(0).AllowEditing = False
                    .Cols(1).AllowEditing = False
                    .AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
                    '.Redraw = False

                    For i = 1 To oCategories.Count
                        .Rows.Add()
                        'Category Node
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Image = oImage.Img_Category.Image
                            .Node.Data = oCategories(i).Name
                        End With
                        .SetData(.Rows.Count - 1, COL_CAT_COLTYPE, CType(enumColType.Category, Integer))
                    Next
                End If
            End With
        Catch objError As Exception
            If oDocumentCategory.ErrorMessage <> "" Then
                MessageBox.Show(oDocumentCategory.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
            Else
                MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
            Exit Sub
        Finally
            If (IsNothing(oCategories) = False) Then
                oCategories.Clear()
            End If
            oCategories = Nothing
            oDocumentCategory = Nothing

            oImage.Dispose()
            oImage = Nothing
        End Try
    End Sub


    'Year & Month Wise
    'Public Sub Fill_CategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Category As String, ByVal Year As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oCategorisedDocuments As gloStream.gloDMS.Document.CategorisedDocuments = Nothing
    '    Dim i As Integer, nCat As Integer, nStyleRow As Integer, nMonth As Integer
    '    Dim nCatStart As Integer, nCatEnd As Integer, nCatCount As Integer
    '    Dim sMonths As New Collection
    '    Dim oImage As New frmDMS_Support

    '    'Dim oNode As Node

    '    Try
    '        With FillControl
    '            For nCat = 0 To .Rows.Count - 1
    '                '.Rows.Count - 1, COL_CAT_COLTYPE, CType(enumColType.Category, Integer)
    '                If UCase(Trim(.GetData(nCat, COL_CAT_NAME) & "")) = UCase(Category) AndAlso CInt(.GetData(nCat, COL_CAT_COLTYPE)) = CType(enumColType.Category, Integer) Then
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
    '                                    If oCategorisedDocuments(i).IsReviwed = True Then
    '                                        oC1Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Document2.JPG"))
    '                                    Else
    '                                        oC1Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document_NotReviwed.Image) 'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Document2.JPG"))
    '                                    End If
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
    '                                    .SetData(nStyleRow, COL_CAT_VERSIONNO, oCategorisedDocuments(i).VersionNo)
    '                                    If oCategorisedDocuments(i).IsReviwed = True Then
    '                                        .SetData(nStyleRow, COL_CAT_ISREVIWED, 1)
    '                                        .Rows(nStyleRow).Style = FillControl.Styles("CS_File")
    '                                        .SetCellImage(nStyleRow, COL_CAT_REVIWEDFLAG, oImage.Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                    Else
    '                                        .SetData(nStyleRow, COL_CAT_ISREVIWED, 0)
    '                                        .Rows(nStyleRow).Style = FillControl.Styles("CS_File_UnReviwed")
    '                                        .SetCellImage(nStyleRow, COL_CAT_REVIWEDFLAG, oImage.Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                    End If
    '                                    With .Rows(nStyleRow)
    '                                        .ImageAndText = True
    '                                        .Height = 25
    '                                        '//.Style = FillControl.Styles("CS_File")
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

    '        oImage.Dispose()
    '        oImage = Nothing
    '    End Try
    'End Sub

    ''Without Year & Month Wise
    'Public Sub Fill_CategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Category As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oCategorisedDocuments As gloStream.gloDMS.Document.CategorisedDocuments = Nothing
    '    Dim i As Integer, nCat As Integer, nStyleRow As Integer ', nMonth As Integer
    '    Dim nCatStart As Integer, nCatEnd As Integer, nCatCount As Integer
    '    Dim sMonths As New Collection
    '    'Dim oNode As Node

    '    Dim oImage As New frmDMS_Support

    '    Try
    '        With FillControl
    '            For nCat = 0 To .Rows.Count - 1
    '                If UCase(Trim(.GetData(nCat, COL_CAT_NAME) & "")) = UCase(Category) AndAlso CInt(.GetData(nCat, COL_CAT_COLTYPE)) = CType(enumColType.Category, Integer) Then
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
    '                                If oCategorisedDocuments(i).IsReviwed = True Then
    '                                    .Rows(nCat).Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document.Image)
    '                                Else
    '                                    .Rows(nCat).Node.AddNode(NodeTypeEnum.LastChild, oCategorisedDocuments(i).Name, i, oImage.Img_Document_NotReviwed.Image)
    '                                End If

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
    '                                .SetData(nStyleRow, COL_CAT_VERSIONNO, oCategorisedDocuments(i).VersionNo)
    '                                If oCategorisedDocuments(i).IsReviwed = True Then
    '                                    .SetData(nStyleRow, COL_CAT_ISREVIWED, 1)
    '                                    .Rows(nStyleRow).Style = FillControl.Styles("CS_File")
    '                                    .SetCellImage(nStyleRow, COL_CAT_REVIWEDFLAG, oImage.Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                Else
    '                                    .SetData(nStyleRow, COL_CAT_ISREVIWED, 0)
    '                                    .Rows(nStyleRow).Style = FillControl.Styles("CS_File_UnReviwed")
    '                                    .SetCellImage(nStyleRow, COL_CAT_REVIWEDFLAG, oImage.Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                End If

    '                                With .Rows(nStyleRow)
    '                                    .ImageAndText = True
    '                                    .Height = 25
    '                                    '//.Style = FillControl.Styles("CS_File")
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

    '        oImage.Dispose()
    '        oImage = Nothing
    '    End Try
    'End Sub

    ''Year & Month Wise
    'Public Sub Fill_UncategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal Year As String, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oUncategorisedDocuments As gloStream.gloDMS.Document.UncategorisedDocuments = Nothing
    '    Dim i As Integer, nMonth As Integer
    '    Dim sMonths As New Collection
    '    Dim oImage As New frmDMS_Support

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
    '            If sMonths.Count > 0 Then
    '                For nMonth = 1 To sMonths.Count
    '                    oUncategorisedDocuments = oDMS.UnCategorisedDocuments(PatientID, Year, sMonths(nMonth))
    '                    If Not oUncategorisedDocuments Is Nothing Then
    '                        If oUncategorisedDocuments.Count > 0 Then
    '                            .Rows.Add()
    '                            With .Rows(.Rows.Count - 1)
    '                                .ImageAndText = True
    '                                .Height = 25
    '                                .IsNode = True
    '                                .Style = FillControl.Styles("CS_Folder")
    '                                .Node.Level = 0
    '                                .Node.Image = oImage.Img_MonthUncategory.Image
    '                                .Node.Data = Space(7) & sMonths(nMonth)
    '                            End With
    '                            .SetData(.Rows.Count - 1, COL_UNCAT_COLTYPE, CType(enumColType.Month, Integer))

    '                            'Fill Document
    '                            For i = 1 To oUncategorisedDocuments.Count
    '                                .Rows.Add()
    '                                With .Rows(.Rows.Count - 1)
    '                                    'Document Information
    '                                    .ImageAndText = True
    '                                    .Height = 25
    '                                    .IsNode = True
    '                                    .Style = FillControl.Styles("CS_File")
    '                                    .Node.Level = 1
    '                                    .Node.Image = oImage.Img_Document.Image
    '                                    .Node.Data = oUncategorisedDocuments(i).Name
    '                                End With

    '                                'Show Note or Not
    '                                If oDocument.HasNote(oUncategorisedDocuments(i)) = True Then
    '                                    .SetCellImage(.Rows.Count - 1, COL_UNCAT_NOTEFLAG, oImage.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
    '                                End If
    '                                'Other Document Property
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEMACHINE, oUncategorisedDocuments(i).SourceMachine)  ' Source Machine
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_SYSTEMFOLDER, oUncategorisedDocuments(i).SystemFolder)    ' System Folder
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_CONTAINER, oUncategorisedDocuments(i).Container)          ' Container
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_PATIENTID, oUncategorisedDocuments(i).PatientID)          ' Patient ID
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_YEAR, oUncategorisedDocuments(i).Year)                    ' Year
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_MONTH, oUncategorisedDocuments(i).Month)                  ' Month
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_SOURCEBIN, oUncategorisedDocuments(i).SourceBin)          ' Source Bin
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_INUSED, oUncategorisedDocuments(i).InUsed)                ' In Used
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_USEDMACHINE, oUncategorisedDocuments(i).UsedMachine)      ' Used Machine
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_USEDTYPE, oUncategorisedDocuments(i).UsedType)            ' Used Type
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_PATH, oUncategorisedDocuments(i).Path)                    ' Path
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_COLTYPE, CType(enumColType.Document, Integer))
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_FILENAME, oUncategorisedDocuments(i).FileName)
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_MACHINEID, oUncategorisedDocuments(i).MachineID)
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_VERSIONNO, oUncategorisedDocuments(i).VersionNo)
    '                                .SetData(.Rows.Count - 1, COL_UNCAT_ISREVIWED, 0)
    '                            Next
    '                        End If
    '                    End If
    '                Next
    '            End If

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

    '        oImage.Dispose()
    '        oImage = Nothing
    '    End Try
    'End Sub

    ''Without Year & Month Wise
    'Public Sub Fill_UncategorisedDocuments(ByVal FillControl As C1FlexGrid, ByVal PatientID As Long)
    '    Dim oDMS As New gloStream.gloDMS.DMS
    '    Dim oDocument As New gloStream.gloDMS.Document.Document
    '    Dim oUncategorisedDocuments As gloStream.gloDMS.Document.UncategorisedDocuments = Nothing
    '    Dim i As Integer ', nMonth As Integer
    '    Dim sMonths As New Collection
    '    Dim oImage As New frmDMS_Support

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
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_VERSIONNO, oUncategorisedDocuments(i).VersionNo)
    '                        .SetData(.Rows.Count - 1, COL_UNCAT_ISREVIWED, False)
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
    '        oImage.Dispose()
    '        oImage = Nothing
    '    End Try
    'End Sub

End Module
