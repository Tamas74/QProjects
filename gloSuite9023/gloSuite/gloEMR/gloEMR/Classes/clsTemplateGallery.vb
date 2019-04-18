
Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase



Public Class clsTemplateGallery

    Implements IDisposable

    'Private ds As New DataSet
    ' Private dt As DataTable
    Private dv As DataView = Nothing


    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        Return ds
    '    End Get
    'End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            Return dv
        End Get
    End Property

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        Return dv
    End Function

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String, ByVal Dview As DataView)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(Dview) = False) Then
            str = Dview.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & Dview.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            Dview.RowFilter = strexpr
        End If

    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String

        Dim arrstring() As String
        Try
            If Trim(strsplittext) <> "" Then

                arrstring = Split(strsplittext, " ")
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return strsplittext
        End Try
    End Function

    Public Function CheckDuplicate(ByVal ID As Long, ByVal TemplateName As String, ByVal CategoryID As Int64, ByVal ProviderID As Int64) As Boolean

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '  Dim oResultTable As New DataTable 'Slr not used
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryID"
            oParamater.Value = CategoryID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            oParamater.Value = ProviderID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            Dim rowAffected As Int64

            rowAffected = CType(oDB.GetDataValue("gsp_CheckTemplateGallery_MST"), Int64)

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function
    'Public Function IsTemplateExits(ByVal sTemplateName As String) As Boolean
    '    Try
    '        'This condition Added by Mayuri:20091104
    '        'To Fix Bug:#4317-"Double saving SmtDX throws Object Reference error."
    '        If sTemplateName <> "" Then
    '            'Dim strQRY As String = "SELECT count(TemplateGallery_MST.nTemplateID) FROM TemplateGallery_MST INNER JOIN " _
    '            '                       & "Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID " _
    '            '                       & " where Category_MST.sDescription = 'SOAP' AND TemplateGallery_MST.sTemplateName = '" & sTemplateName.Replace("'", "''") & "'"

    '            Dim strQRY As String = "SELECT count(TemplateGallery_MST.nTemplateID) FROM TemplateGallery_MST INNER JOIN " _
    '                                  & "Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID " _
    '                                  & " where TemplateGallery_MST.sTemplateName = '" & sTemplateName.Replace("'", "''") & "'"


    '            Dim oDB As New DataBaseLayer
    '            Dim nCount As Integer = CType(oDB.GetRecord_Query(strQRY), Integer)
    '            If nCount > 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function
    Public Function IsTemplateExist(ByVal sTemplateName As String) As Boolean
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing  'Slr New not needed
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sTemplateName"
            oParamater.Value = sTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("CheckTemplateExists")
            Dim TemplateExist As Int64 = 0
            If (IsNothing(oResultTable) = False) Then
                If oResultTable.Rows.Count > 0 Then
                    TemplateExist = Convert.ToInt64(oResultTable(0)(0).ToString())
                End If
            End If
            If (TemplateExist = 0) Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oResultTable) Then  ''slr free oResultTable
                oResultTable.Dispose()
            End If
            oResultTable = Nothing
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    Public Function GetAllTemplateGallery(ByVal ID As Long, Optional ByVal ProviderID As Long = 0) As DataView

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing ''Slr new not needed
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            If ProviderID <> 0 Then
                oParamater.Value = ProviderID

            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            ' ds.Clear()
            oResultTable = oDB.GetDataTable("gsp_ViewTemplateGallery_MST")
            'If IsNothing(dv) = False Then ''global variable so not disposed 
            '    dv.Dispose()
            '    dv = Nothing
            'End If
            If Not oResultTable Is Nothing Then
                dv = oResultTable.Copy().DefaultView
                Return dv
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
            If (IsNothing(oResultTable) = False) Then
                oResultTable.Dispose()
            End If

            oResultTable = Nothing  ''slr free oResultTable
        End Try
    End Function

    Public Function GetAllTemplates(ByVal ID As Long, Optional ByVal ProviderID As Long = 0) As DataSet

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable = Nothing 'slr new not needed
        Dim dsTemplates As New DataSet
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            If ProviderID <> 0 Then
                oParamater.Value = ProviderID
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ViewTemplateGallery_MST")
            '   ds.Clear()
            If Not oResultTable Is Nothing Then
                dsTemplates.Tables.Add(oResultTable.Copy)
            End If
            Return dsTemplates

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dsTemplates
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
            If Not IsNothing(oResultTable) Then
                oResultTable.Dispose()
                oResultTable = Nothing  ''slr free oResultTable
            End If
            
            oDB = Nothing
        End Try

    End Function

    Public Function SelectTemplateGallery(ByVal ID As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable = Nothing 'Slr new not needed 
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ScanTemplateGallery_MST")
            If IsNothing(dv) = False Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not oResultTable Is Nothing Then
                dv = oResultTable.Copy().DefaultView
            End If
            Return oResultTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return oResultTable
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            If Not IsNothing(oResultTable) Then
                oResultTable.Dispose()
                oResultTable = Nothing  ''slr free oResultTable
            End If
            oDB = Nothing
        End Try
    End Function
    'Shubhangi
    Public Function GetTemplate(ByVal sTemplateName As String) As Int64

        Dim _Result As Int64 = 0
        Dim oResult As Object
        Dim oDB As New DataBaseLayer

        Try
            Dim strQRY As String = " SELECT nTemplateID FROM TemplateGallery_MST WHERE sTemplateName= '" & sTemplateName.Replace("'", "''") & "'"
            oResult = oDB.GetRecord_Query(strQRY)
            If oResult IsNot Nothing Then
                If oResult.ToString() <> "" Then
                    _Result = Convert.ToInt64(oResult)
                End If
            End If

            If _Result > 0 Then
                SelectTemplateGallery(Convert.ToInt64(_Result))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()  'slr free oDB
            oDB = Nothing
        End Try
        Return _Result
        'End
    End Function
    Public Function GetAllProvider() As DataTable
        Dim oDB As New DataBaseLayer
        Try


            'Dim oParamater As DBParameter

            Dim oResultTable As DataTable = Nothing ''Slr no new needed

            oResultTable = oDB.GetDataTable("gsp_FillProvider_MST")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function

    Public Function GetSelectedProviderID() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing ''Slr new not needed 
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderName"
            ' 20080922 oParamater.Value = gstrDoctorName
            oParamater.Value = gstrPatientProviderName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ScanProviderDetails")

            If Not oResultTable Is Nothing Then

                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''Slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function

    Public Function GetEMField(ByVal ID As Int64) As String
        Dim _result As String = ""
        Dim oDB As New DataBaseLayer
        Try


            Dim strQRY As String = "SELECT sAssociatedEMName FROM AssociatedEMField WHERE nFieldID = " & ID & " AND nFieldType = " & FieldType.Tags.GetHashCode() & ""

            _result = oDB.GetRecord_Query(strQRY)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then  ''Slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
        Return _result
    End Function

    Public Function GetEMAssociatedField(ByVal ID As Int64) As DataTable
        Dim _result As DataTable = Nothing ''Slr new not needed 
        Dim oDB As New DataBaseLayer
        Try


            Dim strQRY As String = "SELECT sAssociatedEMName,sAssociatedEMCategory,sStatus FROM AssociatedEMField WHERE nFieldID = " & ID & " AND nFieldType = " & FieldType.Tags.GetHashCode() & ""

            _result = oDB.GetDataTable_Query(strQRY)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then  ''Slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
        Return _result
    End Function
    Public Function GetAllCategory() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable = Nothing ''Slr new not needed 
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryType"
            oParamater.Value = "Template"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillCategory_MST")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return oResultTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return oResultTable
        Finally
            If Not IsNothing(oDB) Then  ''Slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function

    Public Sub AddNewTemplateGallery(ByRef ID As Long, ByVal TemplateName As String, ByVal CategoryID As Long, ByVal categoryName As String, ByVal ProviderID As Long, ByVal Description As String, Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrMangementOption As ArrayList = Nothing, Optional ByVal _arrDiagnosisOption As ArrayList = Nothing, Optional ByVal TemplateSpecility As String = "", Optional ByVal Bibliographicinfo As String = "", Optional ByVal Bibliographicdev As String = "")   ''Optional ByVal strAssociatedItemName As String = "")
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter


        Try

           
            ''ID change to ByREF added for bugid #74914: 
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryID"
            If CategoryID > 0 Then
                oParamater.Value = CategoryID
            Else
                oParamater.Value = -1
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryName"
            oParamater.Value = categoryName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            If ProviderID > 0 Then
                oParamater.Value = ProviderID
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Description"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(Description)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateSpecility"
            oParamater.Value = TemplateSpecility
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@BibliographicDeveloper"
            oParamater.Value = Bibliographicdev
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Bibliographicinfo"
            oParamater.Value = Bibliographicinfo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@TemplateID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ID = oDB.Add("gsp_InUpTemplateGallery_MST")



            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
                oDB = New DataBaseLayer
            End If
            Dim str As Int64
            TemplateName = TemplateName.Replace("'", "''")
            Dim strQRY As String = "select nTemplateID from TemplateGallery_MST where sTemplateName= '" & TemplateName & "' and sCategoryName='Tags' and nProviderID= " & ProviderID
            'Dim strqry As SqlClient.SqlCommand
            'Condition Added by Mayuri:20100322
            If oDB.GetRecord_Query(strQRY) <> "" Then
                str = Convert.ToInt64(oDB.GetRecord_Query(strQRY))
            End If

            If Not IsNothing(_arrLabs) AndAlso Not IsNothing(_arrOrders) AndAlso Not IsNothing(_arrMangementOption) AndAlso Not IsNothing(_arrDiagnosisOption) Then
                'If _arrLabs.Count > 0 And _arrOrders.Count > 0 And _arrMangementOption.Count > 0 And _arrDiagnosisOption.Count > 0 Then
                Dim strDeleteQRY As String = "DELETE AssociatedEMField where nFieldID= " & str & " and nFieldType= '4'"
                'Dim strqry As SqlClient.SqlCommand
                Convert.ToInt64(oDB.Delete_Query(strDeleteQRY))
                'End If
            End If
            If Not IsNothing(_arrLabs) Then


                For i As Integer = 0 To _arrLabs.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If

            If Not IsNothing(_arrOrders) Then


                For i As Integer = 0 To _arrOrders.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
            If Not IsNothing(_arrDiagnosisOption) Then


                For i As Integer = 0 To _arrDiagnosisOption.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
            If Not IsNothing(_arrMangementOption) Then

                For i As Integer = 0 To _arrMangementOption.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        If Not IsNothing(oDB) Then  ''slr free oDB
            oDB.Dispose()
            oDB = Nothing

        End If
    End Sub
    Public Sub AddNewTemplateGalleryBytes(ByRef ID As Long, ByVal TemplateName As String, ByVal CategoryID As Long, ByVal categoryName As String, ByVal ProviderID As Long, ByVal bBytes As Object, Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrMangementOption As ArrayList = Nothing, Optional ByVal _arrDiagnosisOption As ArrayList = Nothing, Optional ByVal TemplateSpecility As String = "", Optional ByVal Bibliographicinfo As String = "", Optional ByVal Bibliographicdev As String = "", Optional ByVal SnomedCode As String = "", Optional ByVal SnomedDesc As String = "")   ''Optional ByVal strAssociatedItemName As String = "")
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter


        Try


            ''ID change to ByREF added for bugid #74914: 
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryID"
            If CategoryID > 0 Then
                oParamater.Value = CategoryID
            Else
                oParamater.Value = -1
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryName"
            oParamater.Value = categoryName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            If ProviderID > 0 Then
                oParamater.Value = ProviderID
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Description"

            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If


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
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateSpecility"
            oParamater.Value = TemplateSpecility
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@BibliographicDeveloper"
            oParamater.Value = Bibliographicdev
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Bibliographicinfo"
            oParamater.Value = Bibliographicinfo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SnomedCode"
            oParamater.Value = SnomedCode
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SnomedDesc"
            oParamater.Value = SnomedDesc
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@TemplateID"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ID = oDB.Add("gsp_InUpTemplateGallery_MST")



            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
                oDB = New DataBaseLayer
            End If
            Dim str As Int64
            TemplateName = TemplateName.Replace("'", "''")
            Dim strQRY As String = "select nTemplateID from TemplateGallery_MST where sTemplateName= '" & TemplateName & "' and sCategoryName='Tags' and nProviderID= " & ProviderID
            'Dim strqry As SqlClient.SqlCommand
            'Condition Added by Mayuri:20100322
            If oDB.GetRecord_Query(strQRY) <> "" Then
                str = Convert.ToInt64(oDB.GetRecord_Query(strQRY))
            End If

            If Not IsNothing(_arrLabs) AndAlso Not IsNothing(_arrOrders) AndAlso Not IsNothing(_arrMangementOption) AndAlso Not IsNothing(_arrDiagnosisOption) Then
                'If _arrLabs.Count > 0 And _arrOrders.Count > 0 And _arrMangementOption.Count > 0 And _arrDiagnosisOption.Count > 0 Then
                Dim strDeleteQRY As String = "DELETE AssociatedEMField where nFieldID= " & str & " and nFieldType= '4'"
                'Dim strqry As SqlClient.SqlCommand
                Convert.ToInt64(oDB.Delete_Query(strDeleteQRY))
                'End If
            End If
            If Not IsNothing(_arrLabs) Then


                For i As Integer = 0 To _arrLabs.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If

            If Not IsNothing(_arrOrders) Then


                For i As Integer = 0 To _arrOrders.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
            If Not IsNothing(_arrDiagnosisOption) Then


                For i As Integer = 0 To _arrDiagnosisOption.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrDiagnosisOption.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
            If Not IsNothing(_arrMangementOption) Then

                For i As Integer = 0 To _arrMangementOption.Count - 1
                    oDB.DBParametersCol.Clear()
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldID"
                    oParamater.Value = str
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMName"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Description
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nFieldType"
                    oParamater.Value = 4
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociatedEMCategory"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Code
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sStatus"
                    oParamater.Value = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Status
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    oDB.Add("FillEMFields")
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        If Not IsNothing(oDB) Then  ''slr free oDB
            oDB.Dispose()
            oDB = Nothing

        End If
    End Sub
    Public Function CategoryName(ByVal _CategoryID As String) As String
        Dim oDB As New DataBaseLayer
        Try

            Dim sqlQry As String = ""
            Dim result As String
            sqlQry = "select isnull(sDescription,'') as CategoryName from Category_MST where nCategoryID=" & _CategoryID


            Dim _CategoryName As Object = oDB.GetRecord_Query(sqlQry)

            result = Convert.ToString(_CategoryName)


            Return result

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), "CategoryName", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try

    End Function

    Public Sub UpdateTemplateGallery(ByVal TemplateID As Long, ByVal strFileName As String)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Description"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strFileName)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            objword = Nothing

            oDB.Add("gsp_UpdateTemplateGallery_MST")
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Template Modified", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Template Modified", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Sub
    Public Sub UpdateTemplateGalleryBytes(ByVal TemplateID As Long, ByVal bBytes As Object)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Description"
            
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If
            
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            'objword = Nothing

            oDB.Add("gsp_UpdateTemplateGallery_MST")
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Template Modified", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Template Modified", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Sub
    Public Sub DeleteTemplateGallery(ByVal ID As Long, ByVal TemplateName As String)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@id"
            oParamater.Value = ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oDB.Delete("gsp_DeleteTemplateGallery_MST")

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, TemplateName & " Template Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing

            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Template Deleted", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, "Template Deleted", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Sub

    Public Function ImportTemplateName(ByVal strTemplateName As String, ByVal nCategoryID As Long, ByVal nProviderID As Long) As String

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim nTotalTemplates As Int16 = 0
        'Dim oResultTable As New DataTable  'Slr not used 
        Try
            ' oDB = New DataBaseLayer new not needed Slr

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = Replace(strTemplateName, "'", "''")
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryID"
            oParamater.Value = nCategoryID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            oParamater.Value = nProviderID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            nTotalTemplates = CType(oDB.GetDataValue("gsp_CheckTemplateGallery_MST"), Int16)
            If Not IsNothing(oDB) Then ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
            Dim i As Int16 = 0
            Dim strOriginalTemplateName As String
            strOriginalTemplateName = strTemplateName
            While nTotalTemplates <> 0
                i = i + 1
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ID"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                strTemplateName = strOriginalTemplateName & "-" & i
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateName"
                oParamater.Value = Replace(strTemplateName, "'", "''")
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@CategoryID"
                oParamater.Value = nCategoryID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ProviderID"
                oParamater.Value = nProviderID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                nTotalTemplates = CType(oDB.GetDataValue("gsp_CheckTemplateGallery_MST"), Int16)
                If Not IsNothing(oDB) Then ''slr free oDB
                    oDB.Dispose()
                End If
                oDB = Nothing
            End While
            Return strTemplateName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function


    Public Function IsTemplateUsedInPatientEducation(ByVal TemplateID As String) As Boolean

        Dim oDB As New DataBaseLayer
        Try

            Dim sqlQry As String = ""
            Dim result As Int64
            Dim bIsUsed As Boolean
            Dim _RecordCount As Object

            sqlQry = "SELECT COUNT(nICD9MappingID) AS UsedCount  FROM dbo.Education_ICD9 WHERE nTemplateID=" & TemplateID
            _RecordCount = oDB.GetRecord_Query(sqlQry)
            result = Convert.ToInt64(_RecordCount)
            If (result > 0) Then
                bIsUsed = True
            ElseIf bIsUsed = False Then
                sqlQry = "SELECT COUNT(nMedMappingID) AS UsedCount  FROM dbo.Education_Medication WHERE nTemplateID= " & TemplateID
                _RecordCount = oDB.GetRecord_Query(sqlQry)
                result = Convert.ToInt64(_RecordCount)
                If (result > 0) Then
                    bIsUsed = True
                End If
            ElseIf bIsUsed = False Then
                sqlQry = "SELECT COUNT(nLabResultMappingID) AS UsedCount  FROM dbo.Education_LabResults WHERE nTemplateID=" & TemplateID
                _RecordCount = oDB.GetRecord_Query(sqlQry)
                result = Convert.ToInt64(_RecordCount)
                If (result > 0) Then
                    bIsUsed = True
                End If
            ElseIf bIsUsed = False Then
                sqlQry = "SELECT COUNT(nSnomedMappingID) AS UsedCount  FROM dbo.Education_Snomed WHERE nTemplateID=" & TemplateID
                _RecordCount = oDB.GetRecord_Query(sqlQry)
                result = Convert.ToInt64(_RecordCount)
                If (result > 0) Then
                    bIsUsed = True
                End If
            End If


            Return bIsUsed

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), "CategoryName", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try

    End Function

    'sarika 27th sept 07
    Public Function FillAllTemplates() As DataTable
        Dim conn As New SqlClient.SqlConnection(GetConnectionString)
        Dim _strSQL As String = ""
        Dim dt As DataTable = Nothing
        Dim da As SqlClient.SqlDataAdapter = Nothing

        Try
            dt = New DataTable
            _strSQL = "SELECT     ISNULL(TemplateGallery_MST.nTemplateID, '') AS nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, ISNULL(Category_MST.sDescription, '') AS sDescription FROM TemplateGallery_MST LEFT OUTER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID WHERE Category_MST.sDescription Is Not null ORDER BY Category_MST.sDescription, TemplateGallery_MST.sTemplateName "
            da = New SqlClient.SqlDataAdapter(_strSQL, conn)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then  ''slr free Conn,da
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
            End If
            conn = Nothing

            If Not IsNothing(da) Then
                da.Dispose()
            End If
            da = Nothing
        End Try
    End Function
    '--------------------------
    Public Function GetDiagnosis(ByVal PatientID As Int64, ByVal examid As Int64, ByVal mgnVisitID As Int64) As DataTable
        Dim conn As New SqlClient.SqlConnection(GetConnectionString)
        Dim _strSQL As String = ""
        Dim dt As DataTable = Nothing
        Dim da As SqlClient.SqlDataAdapter = Nothing

        Try
            dt = New DataTable
            'nPatientID, nExamID, nVisitID, , sCPTcode, sCPTDescription, sModCode, sModDescription, nUnit
            _strSQL = "SELECT distinct sICD9Code, sICD9Description FROM ExamICD9CPT WHERE nPatientID = " & PatientID & " AND nExamID = " & examid & " AND nVisitID = " & mgnVisitID & ""
            da = New SqlClient.SqlDataAdapter(_strSQL, conn)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then  ''Slr dispose conn,da and free it
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
            End If
            conn = Nothing

            If Not IsNothing(da) Then
                da.Dispose()
            End If
            da = Nothing
        End Try
    End Function
#Region "Liquid Data Functions"
    'Public Function AddDataFieldValue(ByVal nElementId As Int64, ByVal nGroupID As Int64, ByVal strFieldName As String, ByVal strDataType As String, ByVal bIsRequired As Boolean, Optional ByVal ItemList As myList = Nothing, Optional ByVal strFieldCategory As String = "") As Int64
    '    Dim oDB As DataBaseLayer
    '    Dim oParamater As DBParameter
    '    Dim ElementID As Int64 = 0
    '    Try
    '        oDB = New DataBaseLayer

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ElementName"
    '        oParamater.Value = strFieldName
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ElementType"
    '        oParamater.Value = strDataType
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Bit
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@bIsMandatory"

    '        If bIsRequired Then
    '            oParamater.Value = 1
    '        Else
    '            oParamater.Value = 0
    '        End If

    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@GroupID"
    '        oParamater.Value = nGroupID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ColumnID"
    '        oParamater.Value = 0
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@sCategoryName"
    '        If nGroupID <> 0 Then
    '            If strDataType = "Table" Or strDataType = "Group" Then
    '                If IsNothing(ItemList) Then
    '                    oParamater.Value = ""
    '                Else
    '                    oParamater.Value = ItemList.HistoryCategory.ToString
    '                End If
    '            Else
    '                oParamater.Value = ""
    '            End If
    '        Else
    '            oParamater.Value = strFieldCategory
    '        End If
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@sItemName"
    '        If strDataType = "Table" Or strDataType = "Group" Then
    '            If IsNothing(ItemList) Then
    '                oParamater.Value = ""
    '            Else
    '                oParamater.Value = ItemList.HistoryItem.ToString
    '            End If

    '        Else
    '            oParamater.Value = ""
    '        End If
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@MachineID"
    '        oParamater.Value = GetPrefixTransactionID()
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.InputOutput
    '        oParamater.Name = "@ElementID"
    '        oParamater.Value = nElementId
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ElementID = oDB.Add("Insert_DataFields")
    '        Return ElementID
    '    Catch ex As Exception
    '        Return 0
    '    Finally
    '        oDB = Nothing
    '    End Try

    'End Function

    'Problem : 00000163
    'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
    'Change : New parameter nSequenceNo added to below function AddDataFieldValue() and value passed to SP Insert_DataFields.
    Public Function AddDataFieldValue(ByVal nElementId As Int64, ByVal nGroupID As Int64, ByVal strFieldName As String, ByVal strDataType As String, ByVal bIsRequired As Boolean, Optional ByVal ItemList As myList = Nothing, Optional ByVal strFieldCategory As String = "", Optional ByVal nSequenceNo As Int32 = 0) As Int64
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim ElementID As Int64 = 0
        Try
            oDB = New DataBaseLayer


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ElementName"
            If nGroupID <> 0 Then
                If strDataType = "Multiple Selection" OrElse (strDataType = "Boolean" AndAlso IsNothing(ItemList) = False) Then
                    oParamater.Value = ItemList.Value.ToString()
                Else
                    oParamater.Value = strFieldName
                End If
            Else
                oParamater.Value = strFieldName
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ElementType"
            oParamater.Value = strDataType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bIsMandatory"

            If bIsRequired Then
                oParamater.Value = 1
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@GroupID"
            oParamater.Value = nGroupID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ColumnID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sCategoryName"
            If nGroupID <> 0 Then
                If strDataType = "Table" Or strDataType = "Group" Then
                    If IsNothing(ItemList) Then
                        oParamater.Value = ""
                    Else
                        oParamater.Value = ItemList.HistoryCategory.ToString
                    End If
                Else
                    oParamater.Value = ""
                End If
            Else
                oParamater.Value = strFieldCategory
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sItemName"
            If strDataType = "Table" Or strDataType = "Group" Then
                If IsNothing(ItemList) Then
                    oParamater.Value = ""
                Else
                    oParamater.Value = ItemList.HistoryItem.ToString
                End If
            Else
                oParamater.Value = ""
            End If
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
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nControlType"
            If strDataType = "Multiple Selection" Or strDataType = "Table" Or strDataType = "Group" Then
                If IsNothing(ItemList) Then
                    oParamater.Value = 0
                Else
                    oParamater.Value = CType(ItemList.ControlType.GetHashCode(), Integer)
                End If
            Else
                oParamater.Value = 0
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sAssociatedCategory"
            If nGroupID <> 0 Then
                If strDataType = "Table" Or strDataType = "Group" Then
                    If IsNothing(ItemList) Then
                        oParamater.Value = ""
                    Else
                        oParamater.Value = ItemList.AssociatedCategory.ToString
                    End If
                Else
                    oParamater.Value = ""
                End If
            Else
                oParamater.Value = strFieldCategory
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sAssociateditem"
            If strDataType = "Table" Or strDataType = "Group" Then
                If IsNothing(ItemList) Then
                    oParamater.Value = ""
                Else
                    oParamater.Value = ItemList.AssociatedItem.ToString
                End If
            Else
                oParamater.Value = ""
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sAssociatedProperty"
            If strDataType = "Multiple Selection" Or strDataType = "Table" Or strDataType = "Group" Or strDataType = "Boolean" Then
                If IsNothing(ItemList) Then
                    oParamater.Value = ""
                Else
                    oParamater.Value = ItemList.AssociatedProperty.ToString
                End If
            Else
                oParamater.Value = ""
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nSequenceNo"
            oParamater.Value = nSequenceNo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@ElementID"
            oParamater.Value = nElementId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing



            ElementID = oDB.Add("Insert_DataFields")
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

    ''' <summary>
    ''' Get All Primary Liquid Data Fields from Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidData() As DataTable

        Dim oDB As New DataBaseLayer
        ' Dim oParamater As DBParameter
        Dim oResultTable As DataTable = Nothing ''slr New not needed 
        Dim strSQL As String
        Try
            ''Query to get the root level DataField
            'COMMENTED BY SHUBHANGI 20100622
            'strSQL = "select * FROM LiquidData_MST WHERE nGroupId = 0"
            'ADDED BY SHUBHANGI 20100622 ADD ALL LIQUID DATA ASCENDINGLY IT RESOLVE THE PROBLEM THAT IT IS CHANING SEQUENCE OF RECORDS ON SAVE & CLOSE
            strSQL = "select nElementID,sElementName,sElementType,bIsMandatory FROM LiquidData_MST WHERE nGroupId = 0 order by sElementName ASC"
            oResultTable = oDB.GetDataTable_Query(strSQL)

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Liquid Data", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try

    End Function
    ''' <summary>
    ''' Delete the selcted dataField  from datadictionary
    ''' </summary>
    ''' <param name="nElementId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDataField(ByVal nElementId As Int64, Optional ByVal deleteGroupAlso As Boolean = False) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try

            If deleteGroupAlso = True Then
                strSQL = "Delete FROM LiquidData_MST WHERE nElementId = " & nElementId & " or nGroupID= " & nElementId
            Else
                strSQL = "Delete FROM LiquidData_MST WHERE  nGroupID= " & nElementId
            End If

            Return oDB.Delete_Query(strSQL)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If Not IsNothing(oDB) Then ''Slr free oDB it
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function
    ''' <summary>
    ''' To check for the data field existence for the same datatype
    ''' </summary>
    ''' <param name="strFieldName"></param>
    ''' <param name="strDataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDuplicateField(ByVal strFieldName As String, ByVal strDataType As String, ByVal blnModify As Boolean) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim cnt As Int32 = 2
        Try
            'Commented by Shubhangi 20091224 Because We want to avoid duplicate records of same name
            'strSQL = "Select count(*) FROM LiquidData_MST WHERE sElementName = '" & strFieldName.Replace("'", "''") & "' and sElementType='" & strDataType & "'"
            'Shubhangi 20091224 This is modified query for avoiding duplicate records.
            strSQL = "Select count(*) FROM LiquidData_MST WHERE sElementName = '" & strFieldName.Replace("'", "''") & "' AND nGroupId = 0 "
            ''check for DB Null value
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then
                ''Get the no of records present
                cnt = Convert.ToInt32(strResult)
                If cnt = 1 Then
                    '' one record available which is the current one acceptable
                    If blnModify Then
                        Return False
                    ElseIf cnt = 0 Then
                        '' New Record with no duplicate
                        Return False
                    Else
                        ''Records available
                        Return True
                    End If
                End If
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.ToString)
            Return True
        Finally
            If Not IsNothing(oDB) Then ''Slr free oDB it
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get the DataField in detail that need to be modifed/preview for further processing
    ''' </summary>
    ''' <param name="nElementId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataField(ByVal nElementId As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oResult As DataTable  ''Slr new not needed 
        Dim strSQL As String
        Try
            ''Query to retreive the complete
            'Problem : 00000163
            'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
            'Change : Order By nSequenceNo clause added to retrieve data as per sequence maintained by user.

            strSQL = "Select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty FROM LiquidData_MST WHERE nElementId = " & nElementId & " OR nGroupID= " & nElementId & " ORDER BY nSequenceNo"
            oResult = oDB.GetDataTable_Query(strSQL)
            If Not oResult Is Nothing Then
                Return oResult
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then ''Slr free oDB it
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Delete the selcted DataField  from Transaction Tables
    ''' </summary>
    ''' <param name="nElementId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDataFieldValues(ByVal nElementId As Int64, ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal nExamId As Int64) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "Delete FROM LiquidData_DTL WHERE  nPatientId= " & nPatientId & "  and nPrimaryID =" & nExamId    'and nVisitId = " & nVisitId & "
            Return oDB.Delete_Query(strSQL)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If Not IsNothing(oDB) Then ''Slr free oDB it
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function

    Public Sub InsertFieldValue(ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal nExamId As Int64, ByVal nElementId As Int64, ByVal nFieldId As Int64, ByVal strDataType As String, Optional ByVal strDescription As String = "", Optional ByVal strAssociatedTags As String = "")
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientId"
            oParamater.Value = nPatientId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitId"
            oParamater.Value = nVisitId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ExamId"
            oParamater.Value = nExamId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ElementDescription"
            If strDescription <> "" Then
                oParamater.Value = strDescription
            Else
                oParamater.Value = DBNull.Value
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ElementType"
            oParamater.Value = strDataType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FieldId"
            oParamater.Value = nFieldId
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
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ElementID"
            oParamater.Value = nElementId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sAssociatedTags"
            oParamater.Value = strAssociatedTags
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("Insert_DataValues")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.ToString)
        Finally
            If Not IsNothing(oDB) Then ''Slr free oDB it
                oDB.Dispose()
            End If
            oDB = Nothing
        End Try

    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    'Protected Overridable Sub Dispose(disposing As Boolean)
    '    If Not Me.disposedValue Then
    '        If disposing Then
    '            ' TODO: dispose managed state (managed objects).
    '        End If

    '        ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
    '        ' TODO: set large fields to null.
    '    End If
    '    Me.disposedValue = True
    'End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    'Public Sub Dispose() Implements IDisposable.Dispose
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub
#End Region

    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                'If IsNothing(ds) = False Then
                '    ds.Dispose() : ds = Nothing
                'End If
                'If IsNothing(dt) = False Then
                '    dt.Dispose() : dt = Nothing
                'End If
                If IsNothing(dv) = False Then
                    dv.Dispose() : dv = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


End Class
