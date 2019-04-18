Imports System.IO
Imports System.Data.SqlClient
Namespace gloStream 'Main NameSpace
    Namespace gloDataCather 'Sub NameSpace

        Public Class DataProcess

        End Class

        Namespace DCMapping 'Sub NameSpace

            Public Class DCMapping 'Class Defination

                'Add Function
                Public Function Add(ByVal oMapDocument As gloStream.gloDataCather.DCMapping.MapDocument) As Boolean

                    Dim _Result As Boolean
                    Dim strSQL As String
                    Dim ID As Integer
                    Dim CheckExist As Integer

                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try
                        oDB.Connect(GetConnectionString)

                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & oMapDocument.DCTemplateName & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))

                        strSQL = "Select count(*) from DCMap_Mst where nTemplateID = " & ID
                        CheckExist = Val(oDB.ExecuteQueryScaler(strSQL))
                        If CheckExist <> 0 Then
                            _Result = False
                            Return _Result
                        Else
                            For i As Integer = 1 To oMapDocument.MapLines.Count
                                strSQL = "Insert Into DCMap_Mst (nMapID, nTemplateID, sBaseField, sgloDBField, sDisplayName) Values (" & i & "," & ID & ",'" & oMapDocument.MapLines(i).ExternalField & "','" & oMapDocument.MapLines(i).gloDBField & "','" & oMapDocument.MapLines(i).VerficationDisplayName & "')"
                                _Result = oDB.ExecuteNonSQLQuery(strSQL)
                            Next
                            Return _Result
                        End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCMapping -- Add -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCMapping -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try

                End Function

                'Modify Function
                Public Function Modify(ByVal oMapDocument As gloStream.gloDataCather.DCMapping.MapDocument) As Boolean
                    Dim _Result As Boolean
                    Dim strSQL As String
                    Dim ID As Integer
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try

                        oDB.Connect(GetConnectionString)

                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & oMapDocument.DCTemplateName & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))
                        strSQL = "Delete from DCMap_Mst"
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)
                        Call Add(oMapDocument)
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCMapping -- Modify -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCMapping -- Modify -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function
                Public Function Delete(ByVal DCTemplate As String) As Boolean
                    Dim _Result As Boolean
                    Dim strSQL As String
                    Dim ID As Integer

                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try
                        oDB.Connect(GetConnectionString)

                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & DCTemplate & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))
                        strSQL = "Delete from DCMap_Mst"
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCMapping -- Delete -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCMapping -- Delete -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function


                Public Function MapDocument(ByVal DCTemplate As String) As gloStream.gloDataCather.DCMapping.MapDocument
                    Dim _MapDocument As New gloStream.gloDataCather.DCMapping.MapDocument
                    Dim _Line As gloStream.gloDataCather.DCMapping.MapLine
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim strSQL As String

                    Try

                        oDB.Connect(GetConnectionString)
                        Dim newID As Int16
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & DCTemplate & "'"
                        newID = Val(oDB.ExecuteQueryScaler(strSQL))

                        Dim DataReader As SqlClient.SqlDataReader
                        strSQL = ""
                        strSQL = "Select sBaseField, sgloDBField, sDisplayName from DCMap_Mst where nTemplateID = " & newID & " order by nMapID"
                        DataReader = oDB.ReadQueryRecords(strSQL)

                        If IsNothing(DataReader) = False Then
                            With _MapDocument
                                .DCTemplateName = DCTemplate
                                While DataReader.Read
                                    _Line = New gloStream.gloDataCather.DCMapping.MapLine
                                    _Line.VerficationDisplayName = DataReader.Item("sDisplayName")
                                    _Line.ExternalField = DataReader.Item("sBaseField")
                                    _Line.gloDBField = DataReader.Item("sgloDBField")
                                    .MapLines.Add(_Line)
                                    _Line = Nothing
                                End While
                            End With
                            DataReader.Close()

                        End If
                        DataReader = Nothing

                        Return _MapDocument



                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCMapping -- MapDocument -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCMapping -- MapDocument -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

            End Class

            Public Class MapDocument
                Private _DCTemplateName As String
                Private _MapLines As MapLines
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _MapLines.Dispose()
                        bAssigned = False
                    End If
                End Sub
                Public Property DCTemplateName() As String
                    Get
                        Return _DCTemplateName
                    End Get
                    Set(ByVal Value As String)
                        _DCTemplateName = Value
                    End Set
                End Property

                Public Property MapLines() As MapLines
                    Get
                        Return _MapLines
                    End Get
                    Set(ByVal Value As MapLines)
                        If (bAssigned) Then
                            _MapLines.Dispose()
                            bAssigned = False
                        End If
                        _MapLines = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _MapLines = New MapLines
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _MapLines = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class MapLine
                Private _ExternalField As String
                Private _gloDBField As String
                Private _VerficationDisplayName As String

                Public Property ExternalField() As String
                    Get
                        Return _ExternalField
                    End Get
                    Set(ByVal Value As String)
                        _ExternalField = Value
                    End Set
                End Property

                Public Property gloDBField() As String
                    Get
                        Return _gloDBField
                    End Get
                    Set(ByVal Value As String)
                        _gloDBField = Value
                    End Set
                End Property

                Public Property VerficationDisplayName() As String
                    Get
                        Return _VerficationDisplayName
                    End Get
                    Set(ByVal Value As String)
                        _VerficationDisplayName = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class MapLines
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oMapLine As MapLine) As MapLine
                    'create a new object
                    Dim objNewMember As MapLine
                    objNewMember = New MapLine

                    objNewMember.ExternalField = oMapLine.ExternalField
                    objNewMember.gloDBField = oMapLine.gloDBField
                    objNewMember.VerficationDisplayName = oMapLine.VerficationDisplayName

                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As MapLine
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

        Namespace DCTemplate
            'Maintain Template Functionality Class
            Public Class DCTemplate
                Private _ErrorMessage As String
                'Handle error occures in class in this property
                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function Add(ByVal oTemplate As Template) As Boolean
                    Dim strSQL As String
                    '  Dim Count As Int16
                    Dim _Result As Boolean = False
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)


                        Dim newID As Int16
                        strSQL = "Select Max(nTemplateID) from DCTemplate_Mst"
                        newID = Val(oDB.ExecuteQueryScaler(strSQL))
                        If newID >= 1 Then
                            newID = newID + 1
                        Else
                            newID = 1
                        End If



                        strSQL = ""

                        strSQL = "Insert into DCTemplate_Mst (nTemplateID, sTemplateName, nTemplateType) Values (" & newID & ",'" & oTemplate.Name & "'," & oTemplate.Type & ")"
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)

                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing

                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCTemplate -- Add -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCTemplate -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Public Function Modify(ByVal oOldTemplate As Template, ByVal oNewTemplate As Template) As Boolean

                    Dim strSQL As String
                    Dim _Result As Boolean = False
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)


                        Dim TempID As Int16
                        strSQL = ""
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & oOldTemplate.Name & "' and nTemplateType = " & oOldTemplate.Type & ""
                        TempID = Val(oDB.ExecuteQueryScaler(strSQL))

                        strSQL = ""
                        strSQL = "Update DCTemplate_Mst Set sTemplateName = '" & oNewTemplate.Name & "', nTemplateType = " & oNewTemplate.Type & " where nTemplateID = " & TempID
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)

                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing

                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCTemplate -- Modify -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCTemplate -- Modify -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Public Function Delete(ByVal oTemplate As Template) As Boolean
                    Dim strSQL As String
                    Dim _Result As Boolean
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)


                        Dim TempID As Int16
                        strSQL = ""
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & oTemplate.Name & "' and nTemplateType = " & oTemplate.Type & ""
                        TempID = Val(oDB.ExecuteQueryScaler(strSQL))

                        strSQL = "Delete from DCTemplate_Mst where nTemplateID = " & TempID
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCTemplate -- Delete -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCTemplate -- Delete -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function


                Public Function IsExist(ByVal oTemplate As Template, Optional ByVal OldTemplateName As String = "") As Boolean
                    Dim strSQL As String
                    Dim _Result As Boolean
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        'oldname <> "" - edit
                        'oldname = "" - new

                        'template name where dbname = templatename
                        'true = duplicate

                        'template name where dbname = templatename , <> oldname

                        strSQL = ""
                        If OldTemplateName = "" Then
                            strSQL = "Select Count(*) from DCTemplate_Mst where sTemplateName = '" & oTemplate.Name & "' and nTemplateType = '" & oTemplate.Type & "' "
                            _Result = oDB.ReadQueryRecord(strSQL)
                           
                        Else
                            strSQL = "Select Count(*) from DCTemplate_Mst where sTemplateName = '" & oTemplate.Name & "'  "
                            _Result = oDB.ReadQueryRecord(strSQL)
                        End If

                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing

                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCTemplate -- IsExist -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCTemplate -- IsExist -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Public Function Templates() As gloStream.gloDataCather.DCTemplate.Templates
                    Dim _Templates As New gloStream.gloDataCather.DCTemplate.Templates
                    Dim _Template As gloStream.gloDataCather.DCTemplate.Template
                    Dim strSQL As String
                    Dim odb As New gloStream.gloDataBase.gloDataBase
                    odb.Connect(GetConnectionString)
                    Dim DataReader As SqlClient.SqlDataReader
                    Try
                        strSQL = "Select sTemplateName, nTemplateType from DCTemplate_Mst"
                        DataReader = odb.ReadQueryRecords(strSQL)

                        If IsNothing(DataReader) = False Then
                            While DataReader.Read
                                With _Templates
                                    _Template = New gloStream.gloDataCather.DCTemplate.Template
                                    _Template.Name = Trim(DataReader.Item("sTemplateName"))
                                    _Template.Type = DataReader.Item("nTemplateType")
                                    .Add(_Template)
                                    _Template = Nothing
                                End With

                            End While
                            DataReader.Close()
                        End If
                        DataReader = Nothing
                        Return _Templates
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- DCTemplate -- Templates -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- DCTemplate -- Templates -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        odb.Disconnect()
                        odb.Dispose()
                        odb = Nothing
                    End Try
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub


                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Template Property Class
            Public Class Template
                Private _Name As String
                Private _Type As gloStream.gloDataCather.Supporting.TemplateType

                Public Property Name() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property Type() As gloStream.gloDataCather.Supporting.TemplateType
                    Get
                        Return _Type
                    End Get
                    Set(ByVal Value As gloStream.gloDataCather.Supporting.TemplateType)
                        _Type = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            'Template Collection
            Public Class Templates
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oTemplate As Template) As Template
                    'create a new object
                    Dim objNewMember As Template
                    objNewMember = New Template

                    objNewMember.Name = oTemplate.Name
                    objNewMember.Type = oTemplate.Type
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Template
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

        Namespace Settings

            Public Class settings

                Public Function Add(ByVal Param As gloStream.gloDataCather.Settings.SettingsParam) As Boolean
                    Dim strSQL As String
                    Dim _Result As Boolean = False
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try
                        oDB.Connect(GetConnectionString)

                        Dim ID As Int16
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & Param.TempName & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))

                        Dim newID As Int16
                        strSQL = "Select Max(nSetID) from DCSetting_Mst"
                        newID = Val(oDB.ExecuteQueryScaler(strSQL))

                        If newID >= 1 Then
                            newID = newID + 1
                        Else
                            newID = 1
                        End If
                        'Dim newID As Int16
                        'strSQL = "Delete from DCSetting_Mst"
                        'oDB.ExecuteQueryScaler(strSQL)
                        'newID = 1

                        strSQL = ""
                        strSQL = "Insert into DCSetting_Mst (nSetID, nTempID, sFilePath, sDelimiter) Values (" & newID & ",'" & ID & "','" & Param.FilePath & "','" & Param.Delimiter & "')"
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- Settings -- Add -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- Settings -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

                Public Function Modify(ByVal Param As gloStream.gloDataCather.Settings.SettingsParam) As Boolean
                    Dim strSQL As String
                    Dim _Result As Boolean = False
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try
                        oDB.Connect(GetConnectionString)

                        Dim ID As Int16
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & Param.TempName & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))


                        strSQL = ""
                        strSQL = "Update DCSetting_Mst set sFilePath ='" & Param.FilePath & "' , sDelimiter ='" & Param.Delimiter & "' where nTempID = " & ID
                        _Result = oDB.ExecuteNonSQLQuery(strSQL)
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- Settings -- Modify -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- Settings -- Modify -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function
                Public Function IsExist(ByVal Param As gloStream.gloDataCather.Settings.SettingsParam) As Boolean
                    Dim strSQL As String
                    Dim _Result As Boolean = False
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Try
                        oDB.Connect(GetConnectionString)

                        Dim ID As Int16
                        strSQL = "Select nTemplateID from DCTemplate_Mst Where sTemplateName = '" & Param.TempName & "'"
                        ID = Val(oDB.ExecuteQueryScaler(strSQL))

                        strSQL = "Select Count(*) from DCSetting_Mst Where nTempID = " & ID

                        _Result = oDB.ReadQueryRecord(strSQL)
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- Settings -- IsExist -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- Settings -- IsExist -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

                Public Function settings() As gloStream.gloDataCather.Settings.SettingsParam
                    Dim _Setting As New gloStream.gloDataCather.Settings.SettingsParam
                    Dim strSQL As String
                    Dim odb As New gloStream.gloDataBase.gloDataBase
                    odb.Connect(GetConnectionString)
                    Dim DataReader As SqlClient.SqlDataReader
                    Try
                        strSQL = "Select sTemplateName from DCTemplate_Mst"
                        DataReader = odb.ReadQueryRecords(strSQL)
                        If IsNothing(DataReader) = False Then
                            While DataReader.Read
                                With _Setting
                                    _Setting.TempName = Trim(DataReader.Item("sTemplateName"))
                                End With
                            End While
                            DataReader.Close()
                        End If



                        strSQL = "Select sFilePath,sDelimiter from DCSetting_Mst"
                        DataReader = odb.ReadQueryRecords(strSQL)

                        If IsNothing(DataReader) = False Then
                            While DataReader.Read
                                With _Setting
                                    _Setting.FilePath = Trim(DataReader.Item("sFilePath"))
                                    _Setting.Delimiter = Trim(DataReader.Item("sDelimiter"))
                                    '_Setting.Provider = DataReader.Item(4)
                                    '_Setting.PCP = DataReader.Item(5)
                                    '_Setting.Pharmacy = DataReader.Item(6)
                                End With
                            End While
                            DataReader.Close()
                        End If
                        DataReader = Nothing
                        Return _Setting
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- Settings -- settings -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- Settings -- settings -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        odb.Disconnect()
                        odb.Dispose()
                        odb = Nothing
                    End Try
                End Function

            End Class

            Public Class DefaultSettings
                Implements System.Collections.IEnumerable
                Private mCol As Collection

                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As SettingsParam
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

            Public Class SettingsParam
                Private _TempName As String
                Private _FilePath As String
                Private _Delimiter As String
                'Private _Provider As String
                'Private _PCP As String
                'Private _Pharmacy As String

                Public Property TempName() As String
                    Get
                        Return _TempName
                    End Get
                    Set(ByVal Value As String)
                        _TempName = Value
                    End Set
                End Property

                Public Property FilePath() As String
                    Get
                        Return _FilePath
                    End Get
                    Set(ByVal Value As String)
                        _FilePath = Value
                    End Set
                End Property

                Public Property Delimiter() As String
                    Get
                        Return _Delimiter
                    End Get
                    Set(ByVal Value As String)
                        _Delimiter = Value
                    End Set
                End Property
                'Public Property Provider() As String
                '    Get
                '        Return _Provider
                '    End Get
                '    Set(ByVal Value As String)
                '        _Provider = Value
                '    End Set
                'End Property

                'Public Property PCP() As String
                '    Get
                '        Return _PCP
                '    End Get
                '    Set(ByVal Value As String)
                '        _PCP = Value
                '    End Set
                'End Property

                'Public Property Pharmacy() As String
                '    Get
                '        Return _Pharmacy
                '    End Get
                '    Set(ByVal Value As String)
                '        _Pharmacy = Value
                '    End Set
                'End Property
                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

        End Namespace

        Namespace Supporting
            Public Enum PatientFieldNames
                ID = 0
                Code = 1
                FirstName = 2
                LastName = 3
                MiddleName = 4
            End Enum

            Public Enum TemplateType
                PatientRegistration = 0
            End Enum

            Public Class Supporting
                Public Function PatientDBFieldName(ByVal FieldName As gloStream.gloDataCather.Supporting.PatientFieldNames) As String
                    Return Nothing
                End Function

                Public Function PatientDisplayFieldName(ByVal FieldName As gloStream.gloDataCather.Supporting.PatientFieldNames) As String
                    Return Nothing
                End Function
                Public Function SeprateData(ByVal File As String, ByVal Delimator As String) As Collection

                    Dim _TempColl As New Collection
                    Dim _Line As String
                    Dim sr2 As String = File
                    _Line = sr2
                    Dim _SplitData As Array

                    'Do
                    '_Line = sr1.ReadLine
                    'If Trim(_Line) <> "" Then
                    '_Lines.Add(_Line)
                    'End If
                    'Loop Until _Line Is Nothing
                    'Return _Lines

                    _SplitData = Split(File, ",")
                    For j As Integer = 0 To UBound(_SplitData)
                        _TempColl.Add(_SplitData(j))
                    Next

                    Return _TempColl
                End Function
            End Class
        End Namespace

        Namespace gloPatient
            Public Class clsManagePatient
                Public Sub New()
                    MyBase.new()
                End Sub
                Private PatientCol As New ArrayList
                Private Dv As DataView
                '  Private Cmd As System.Data.SqlClient.SqlCommand
                Public Sub Dispose()

                    ''slr free dv
                    If Not IsNothing(dv) Then
                        dv.Dispose()
                        dv = Nothing
                    End If
                    'If Not IsNothing(ds) Then
                    '    ds.Dispose()
                    '    ds = Nothing
                    'End If
                    PatientCol.Clear()
                    

                End Sub
                Public Function GetPatientCol(ByVal index As Int16) As clsPatient
                    If index < PatientCol.Count Then
                        Return CType(PatientCol.Item(index), clsPatient)
                    End If
                    Return Nothing
                End Function
                Public Function SetPatientCol(ByVal objPatient As clsPatient, ByVal index As Int16)
                    If index < PatientCol.Count Then
                        PatientCol.Item(index) = objPatient
                    End If
                    Return Nothing
                End Function
                Public Function PopulatePatient(ByVal objPatient As clsPatient, Optional ByVal index As Int16 = -1)
                    'when form loaded first time
                    If index = -1 Then
                        PatientCol.Add(objPatient)
                    Else
                        'Check if item present in collection
                        If index < PatientCol.Count Then
                            'if item present
                            SetPatientCol(objPatient, index)
                        Else
                            'if item not present
                            PatientCol.Add(objPatient)
                        End If
                    End If
                    Return Nothing
                End Function
                Public Sub ClearPatientcol()
                    PatientCol.Clear()
                End Sub
                Public Function GetPatientCount() As Int16
                    Return PatientCol.Count
                End Function
                Public Function FillControls(ByVal FillType As String) As DataTable
                    'Dim oDB As New gloStream.gloDataBase.gloDataBase
                    'Dim oReader As SqlClient.SqlDataReader

                    'oDB.Connect(GetConnectionString)

                    'If FillType = "" Then
                    '    oReader = oDB.ReadRecords("sp_FillProvider_Mst")
                    'Else

                    'End If



                    'oDB.DBParameters.Add("", "", ParameterDirection.Input, SqlDbType.Float)

                    'oReader = oDB.ReadRecords("")



                    'Try
                    '    Dim adpt As New SqlDataAdapter
                    '    Dim dt As New DataTable
                    '    
                    '        Cmd = New SqlCommand("sp_FillContacts_Mst", Conn)
                    '        Cmd.CommandType = CommandType.StoredProcedure

                    '        Dim objParam As SqlParameter
                    '        objParam = Cmd.Parameters.Add("@Type", SqlDbType.Char)
                    '        objParam.Direction = ParameterDirection.Input
                    '        objParam.Value = FillType

                    '        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Char)
                    '        objParam.Direction = ParameterDirection.Input
                    '        objParam.Value = 1
                    '    End If

                    '    adpt.SelectCommand = Cmd
                    '    adpt.Fill(dt)
                    '    Dv = dt.DefaultView
                    '    'Conn.Close()
                    '    Return dt
                    'Catch ex As SqlClient.SqlException
                    '    If 'Conn.State = ConnectionState.Open Then
                    '        'Conn.Close()
                    '    End If
                    '    'MsgBox(ex.Message)
                    'End Try
                    Return Nothing
                End Function
                Public Sub SortDataview(ByVal strsort As String)
                    Dv.Sort = "[" & strsort & "]"
                End Sub
                Public ReadOnly Property DsDataview() As DataView
                    Get
                        'Dv = Ds.Tables("Category_Mst").DefaultView
                        Return Dv
                        'Return Ds
                    End Get

                End Property

                Public Sub SetRowFilter(ByVal txtSearch As String)
                    Dim strexpr As String
                    Dim str As String
                    str = Dv.Sort
                    str = Splittext(str)
                    str = Mid(str, 2)
                    str = Mid(str, 1, Len(str) - 1)
                    'strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '%" & txtSearch & "%'"
                    strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
                    Dv.RowFilter = strexpr
                End Sub
                Private Function Splittext(ByVal strsplittext As String) As String

                    Dim arrstring() As String
                    Try
                        If Trim(strsplittext) <> "" Then

                            arrstring = Split(LTrim(strsplittext), " ")
                            Return arrstring(0)
                        Else
                            Return strsplittext
                        End If
                    Catch ex As Exception
                        Return strsplittext
                    End Try
                End Function
                Public Function SavePatientDetails(ByVal Skip As Boolean) As String
                    Dim oDB As New gloStream.gloDataBase.gloDataBase

                    Try
                        Dim LogEntry As String = ""
                        'Dim i As Integer
                        Dim objPatient As clsPatient
                        Dim TempID As String = ""
                        Dim _Result As String = ""


                        If PatientCol.Count > 0 Then
                            oDB.Connect(GetConnectionString)
                            'For i = 0 To PatientCol.Count - 1
                            oDB.DBParameters.Clear()
                            objPatient = CType(PatientCol.Item(PatientCol.Count - 1), clsPatient)
                            If Skip = False Then
                                oDB.DBParameters.Add("@sPatientCode", objPatient.PatientCode, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sFirstName", objPatient.FirstName & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sMiddleName", objPatient.MiddleName & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sLastName", objPatient.LastName & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@nSSN", objPatient.SSN.Replace("-", "") & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@dtDOB", objPatient.DOB & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sGender", objPatient.Gender & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sMaritalStatus", objPatient.MaritalStatus & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sAddressLine1", objPatient.AddressLine1 & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sAddressLine2", objPatient.AddressLine2 & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sCity", objPatient.City & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sState", objPatient.State & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sZip", objPatient.ZIP & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sPhone", objPatient.Phone & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sMobile", objPatient.Mobile & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sEmail", objPatient.Email & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sFax", objPatient.FAX & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sOccupation", objPatient.Occupation & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sEmploymentStatus", objPatient.EmploymentStatus & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sPlaceofEmployment", objPatient.PlaceofEmployment & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkAddressLine1", objPatient.WorkAddressLine1 & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkAddressLine2", objPatient.WorkAddressLine2 & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkCity", objPatient.WorkCity & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkState", objPatient.WorkState & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkZip", objPatient.WorkZIP & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkPhone", objPatient.WorkPhone & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sWorkFax", objPatient.WorkFAX & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sChiefComplaints", objPatient.ChiefComplaints & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@nPCPID", objPatient.PCPId & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sGuarantor", objPatient.Guarantor & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sSpouseName", objPatient.SpouseName & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sSpousePhone", objPatient.spousePhone & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sRace", objPatient.race & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sPatientStatus", objPatient.patientStatus & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@nProviderId", objPatient.ProviderID & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@nPharmacyID", objPatient.PharmacyID & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sCounty", objPatient.Country & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@dtRegistrationDate", objPatient.RegistrationDate & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@dtInjurydate", objPatient.InjuryDate & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@dtSurgerydate", objPatient.SurgeryDate & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sHandDominance", objPatient.HandDominance & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sLocation", objPatient.Location & "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@MachineID", GetPrefixTransactionID(CType(objPatient.DOB, Date)), ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@nPatientID", 0, ParameterDirection.Output, SqlDbType.BigInt)
                                If oDB.ExecuteNonQueryVal("gsp_DC_InUpPatient", TempID) = True Then
                                    LogEntry = "VALID RECORD|Record of PatientCode: '" & objPatient.PatientCode & "' was Added in Patient Table"
                                End If
                                _Result = TempID.ToString
                                oDB.DBParameters.Clear()
                                oDB.ExecuteNonQuery("gsp_DC_ClearLineNum")
                            End If
                            Dim sFileName As String = Application.StartupPath & "\DataImport.log"
                            If TempID <> "" Then
                                oDB.DBParameters.Add("@sGloId", TempID & "", ParameterDirection.Input, SqlDbType.VarChar)
                                If File.Exists(sFileName) = True Then
                                    Dim stmWrite As StreamWriter
                                    'Dim filErrorLog As File
                                    stmWrite = File.AppendText(sFileName)
                                    stmWrite.WriteLine(LogEntry)
                                    stmWrite.Close()
                                    stmWrite.Dispose()
                                Else
                                    Dim stmAppendWrite As StreamWriter = File.CreateText(sFileName)
                                    stmAppendWrite.WriteLine(LogEntry)
                                    stmAppendWrite.Close()
                                    stmAppendWrite.Dispose()
                                End If
                            Else
                                oDB.DBParameters.Add("@sGloId", TempID & "", ParameterDirection.Input, SqlDbType.Int)
                                If File.Exists(sFileName) = True Then
                                    Dim stmWrite As StreamWriter
                                    'Dim filErrorLog As File
                                    stmWrite = File.AppendText(sFileName)
                                    LogEntry = "INVALID RECORD|Record of PatientCode: '" & objPatient.PatientCode & "' was Added in DCTableMaster Table"
                                    stmWrite.WriteLine(LogEntry)
                                    stmWrite.Close()
                                    stmWrite.Dispose()
                                Else
                                    Dim stmAppendWrite As StreamWriter = File.CreateText(sFileName)
                                    LogEntry = "INVALID RECORD|Record of PatientCode: '" & objPatient.PatientCode & "' was Added in DCTableMaster Table"
                                    stmAppendWrite.WriteLine(LogEntry)
                                    stmAppendWrite.Close()
                                    stmAppendWrite.Dispose()
                                End If

                            End If
                            '---Set Counter = 0

                            oDB.DBParameters.Add("@nLineNum", objPatient.LineCount, ParameterDirection.Input, SqlDbType.Int)
                            oDB.DBParameters.Add("@sData", objPatient.sData & "", ParameterDirection.Input, SqlDbType.Text)
                            oDB.DBParameters.Add("@sCode", objPatient.PatientCode & "", ParameterDirection.Input, SqlDbType.Text)
                            If oDB.ExecuteNonQuery("gsp_DC_InUpDataTable") = True Then
                                ''If File.Exists(Application.StartupPath & "\DataCatcher.log") = True Then
                                ''    Dim stmNewWrite As StreamWriter = New StreamWriter(Application.StartupPath & "\DataCatcher.log")
                                ''    stmNewWrite.WriteLine("INVALID RECORD|Record of PatientCode: '" & objPatient.PatientCode & "' was Added in DCTableMaster Table")
                                ''    stmNewWrite.Close()
                                ''Else
                                ''    Dim stmAppendWrite As StreamWriter = File.CreateText(Application.StartupPath & "\DataCatcher.log")
                                ''    stmAppendWrite.WriteLine("INVALID RECORD|Record of PatientCode: '" & objPatient.PatientCode & "' was Added in DCTableMaster Table")
                                ''    stmAppendWrite.Close()
                                ''End If
                            End If


                            objPatient = Nothing
                            ' Next
                        End If
                        Return _Result
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDataCateher -- gloPatient -- clsManagePatient -- SavePatientDetails -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDataCateher -- gloPatient -- clsManagePatient -- SavePatientDetails -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function
            End Class
            Public Class clsPatient
                Private _sData As String

                Private _PatientCode As String
                Private _FirstName As String
                Private _MiddleName As String
                Private _LastName As String
                Private _SSN As String
                Private _DOB As String
                Private _Gender As String
                Private _MaritalStatus As String
                Private _AddressLine1 As String
                Private _AddressLine2 As String
                Private _City As String
                Private _State As String
                Private _ZIP As String
                Private _Country As String
                Private _Phone As String
                Private _Mobile As String
                Private _Email As String
                Private _FAX As String
                Private _Occupation As String
                Private _EmploymentStatus As String
                Private _PlaceofEmployment As String
                Private _WorkAddressLine1 As String
                Private _WorkAddressLine2 As String
                Private _WorkCity As String
                Private _WorkState As String
                Private _WorkZIP As String
                Private _WorkPhone As String
                Private _WorkFAX As String
                Private _ChiefComplaints As String
                Private _ProviderID As String
                Private _PCPId As String
                Private _Guarantor As String
                Private _PharmacyID As String
                Private _SpouseName As String
                Private _SpousePhone As String
                Private _Race As String
                Private _PatientStatus As String
                Private _Photo As String
                Private _RegistrationDate As String
                Private _InjuryDate As String
                Private _SurgeryDate As String
                Private _HandDominance As String
                Private _LineCount As String
                Private _Location As String
                Private _check1 As Boolean
                Private _check2 As Boolean
                Private _check3 As Boolean
                Private _check4 As Boolean
                Private _PhotoModified As Boolean
                Private _MachineID As Long

                Public Property sData() As String
                    Get
                        Return _sData
                    End Get
                    Set(ByVal Value As String)
                        _sData = Value
                    End Set
                End Property


                Public Property PatientCode() As String
                    Get
                        Return _PatientCode
                    End Get
                    Set(ByVal Value As String)
                        _PatientCode = Value
                    End Set
                End Property

                Public Property FirstName() As String
                    Get
                        Return _FirstName
                    End Get
                    Set(ByVal Value As String)
                        _FirstName = Value
                    End Set
                End Property
                Public Property MiddleName() As String
                    Get
                        Return _MiddleName
                    End Get
                    Set(ByVal Value As String)
                        _MiddleName = Value
                    End Set
                End Property
                Public Property LastName() As String
                    Get
                        Return _LastName
                    End Get
                    Set(ByVal Value As String)
                        _LastName = Value
                    End Set
                End Property
                Public Property SSN() As String
                    Get
                        Return _SSN
                    End Get
                    Set(ByVal Value As String)
                        _SSN = Value
                    End Set
                End Property
                Public Property DOB() As String
                    Get
                        Return _DOB
                    End Get
                    Set(ByVal Value As String)
                        _DOB = Value
                    End Set
                End Property
                Public Property Gender() As String
                    Get
                        Return _Gender
                    End Get
                    Set(ByVal Value As String)
                        _Gender = Value
                    End Set
                End Property
                Public Property MaritalStatus() As String
                    Get
                        Return _MaritalStatus
                    End Get
                    Set(ByVal Value As String)
                        _MaritalStatus = Value
                    End Set
                End Property
                Public Property AddressLine1() As String
                    Get
                        Return _AddressLine1
                    End Get
                    Set(ByVal Value As String)
                        _AddressLine1 = Value
                    End Set
                End Property
                Public Property AddressLine2() As String
                    Get
                        Return _AddressLine2
                    End Get
                    Set(ByVal Value As String)
                        _AddressLine2 = Value
                    End Set
                End Property
                Public Property City() As String
                    Get
                        Return _City
                    End Get
                    Set(ByVal Value As String)
                        _City = Value
                    End Set
                End Property
                Public Property State() As String
                    Get
                        Return _State
                    End Get
                    Set(ByVal Value As String)
                        _State = Value
                    End Set
                End Property
                Public Property ZIP() As String
                    Get
                        Return _ZIP
                    End Get
                    Set(ByVal Value As String)
                        _ZIP = Value
                    End Set
                End Property
                Public Property Country() As String
                    Get
                        Return _Country
                    End Get
                    Set(ByVal Value As String)
                        _Country = Value
                    End Set
                End Property
                Public Property Phone() As String
                    Get
                        Return _Phone
                    End Get
                    Set(ByVal Value As String)
                        _Phone = Value
                    End Set
                End Property
                Public Property Mobile() As String
                    Get
                        Return _Mobile
                    End Get
                    Set(ByVal Value As String)
                        _Mobile = Value
                    End Set
                End Property
                Public Property Email() As String
                    Get
                        Return _Email
                    End Get
                    Set(ByVal Value As String)
                        _Email = Value
                    End Set
                End Property
                Public Property FAX() As String
                    Get
                        Return _FAX
                    End Get
                    Set(ByVal Value As String)
                        _FAX = Value
                    End Set
                End Property

                Public Property Occupation() As String
                    Get
                        Return _Occupation
                    End Get
                    Set(ByVal Value As String)
                        _Occupation = Value
                    End Set
                End Property
                Public Property EmploymentStatus() As String
                    Get
                        Return _EmploymentStatus
                    End Get
                    Set(ByVal Value As String)
                        _EmploymentStatus = Value
                    End Set
                End Property
                Public Property PlaceofEmployment() As String
                    Get
                        Return _PlaceofEmployment
                    End Get
                    Set(ByVal Value As String)
                        _PlaceofEmployment = Value
                    End Set
                End Property

                Public Property WorkAddressLine1() As String
                    Get
                        Return _WorkAddressLine1
                    End Get
                    Set(ByVal Value As String)
                        _WorkAddressLine1 = Value
                    End Set
                End Property
                Public Property WorkAddressLine2() As String
                    Get
                        Return _WorkAddressLine2
                    End Get
                    Set(ByVal Value As String)
                        _WorkAddressLine2 = Value
                    End Set
                End Property
                Public Property WorkCity() As String
                    Get
                        Return _WorkCity
                    End Get
                    Set(ByVal Value As String)
                        _WorkCity = Value
                    End Set
                End Property
                Public Property WorkState() As String
                    Get
                        Return _WorkState
                    End Get
                    Set(ByVal Value As String)
                        _WorkState = Value
                    End Set
                End Property
                Public Property WorkZIP() As String
                    Get
                        Return _WorkZIP
                    End Get
                    Set(ByVal Value As String)
                        _WorkZIP = Value
                    End Set
                End Property
                Public Property WorkPhone() As String
                    Get
                        Return _WorkPhone
                    End Get
                    Set(ByVal Value As String)
                        _WorkPhone = Value
                    End Set
                End Property
                Public Property WorkFAX() As String
                    Get
                        Return _WorkFAX
                    End Get
                    Set(ByVal Value As String)
                        _WorkFAX = Value
                    End Set
                End Property
                Public Property ChiefComplaints() As String
                    Get
                        Return _ChiefComplaints
                    End Get
                    Set(ByVal Value As String)
                        _ChiefComplaints = Value
                    End Set
                End Property
                Public Property ProviderID() As String
                    Get
                        Return _ProviderID
                    End Get
                    Set(ByVal Value As String)
                        _ProviderID = Value
                    End Set
                End Property
                Public Property PCPId() As String
                    Get
                        Return _PCPId
                    End Get
                    Set(ByVal Value As String)
                        _PCPId = Value
                    End Set
                End Property
                Public Property Guarantor() As String
                    Get
                        Return _Guarantor
                    End Get
                    Set(ByVal Value As String)
                        _Guarantor = Value
                    End Set
                End Property
                Public Property PharmacyID() As String
                    Get
                        Return _PharmacyID
                    End Get
                    Set(ByVal Value As String)
                        _PharmacyID = Value
                    End Set
                End Property
                Public Property SpouseName() As String
                    Get
                        Return _SpouseName
                    End Get
                    Set(ByVal Value As String)
                        _SpouseName = Value
                    End Set
                End Property


                Public Property spousePhone() As String
                    Get
                        Return _SpousePhone
                    End Get
                    Set(ByVal Value As String)
                        _SpousePhone = Value
                    End Set
                End Property

                Public Property race() As String
                    Get
                        Return _Race
                    End Get
                    Set(ByVal Value As String)
                        _Race = Value
                    End Set
                End Property
                Public Property patientStatus() As String
                    Get
                        Return _PatientStatus
                    End Get
                    Set(ByVal Value As String)
                        _PatientStatus = Value
                    End Set
                End Property

                Public Property Photo() As String
                    Get
                        Return _Photo
                    End Get
                    Set(ByVal Value As String)
                        _Photo = Value
                    End Set
                End Property
                Public Property RegistrationDate() As String
                    Get
                        Return _RegistrationDate
                    End Get
                    Set(ByVal Value As String)
                        _RegistrationDate = Value
                    End Set
                End Property
                Public Property InjuryDate() As String
                    Get
                        Return _InjuryDate
                    End Get
                    Set(ByVal Value As String)
                        _InjuryDate = Value
                    End Set
                End Property
                Public Property SurgeryDate() As String
                    Get
                        Return _SurgeryDate
                    End Get
                    Set(ByVal Value As String)
                        _SurgeryDate = Value
                    End Set
                End Property
                Public Property HandDominance() As String
                    Get
                        Return _HandDominance
                    End Get
                    Set(ByVal Value As String)
                        _HandDominance = Value
                    End Set
                End Property

                Public Property Location() As String
                    Get
                        Return _Location
                    End Get
                    Set(ByVal Value As String)
                        _Location = Value
                    End Set
                End Property

                Public Property MachineID() As Long
                    Get
                        Return _MachineID
                    End Get
                    Set(ByVal Value As Long)
                        _MachineID = Value
                    End Set
                End Property

                Public Property LineCount() As Int32
                    Get
                        Return _LineCount
                    End Get
                    Set(ByVal Value As Int32)
                        _LineCount = Value
                    End Set
                End Property

            End Class

        End Namespace

    End Namespace

End Namespace
