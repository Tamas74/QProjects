Imports System.Reflection
Imports System.Data.SqlClient
Namespace gloStream
    Namespace LabModule
        '*****************CATEGORY**********************
        Namespace Category

       
            Public Class MaintainCategory
                Private _ErrorMessage As String

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function CategoryTypes() As Collection
                    Dim _CategoryTypes As New Collection
                    Try
                        Dim oCategoryType As New gloStream.LabModule.Category.Supporting.Supporting
                        _CategoryTypes.Add(oCategoryType.CategoryType_enum_AsString(Supporting.Supporting.enumCategoryType.Order))

                        oCategoryType = Nothing
                        Return _CategoryTypes
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _CategoryTypes = Nothing
                    End Try
                End Function

                Public Function Categories(ByVal oType As String) As gloStream.LabModule.Category.Supporting.Categories
                    Dim _Categories As New gloStream.LabModule.Category.Supporting.Categories
                    Try
                        If oType.Trim <> "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader

                            Dim _oType As New gloStream.LabModule.Category.Supporting.Supporting
                            Dim _TypeID As Int16 = _oType.CategoryType_enum_AsValue(oType)
                            _oType = Nothing

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '" & _TypeID & "' AND lm_category_Description IS NOT NULL  ORDER BY lm_category_Description")
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    If Not IsDBNull(oDataReader.Item("lm_category_Description") & "") Then
                                        _Categories.Add(oDataReader.Item("lm_category_ID") & "", oDataReader.Item("lm_category_Description") & "")
                                    End If
                                End While
                            End If
                            oDataReader.Close()
                            oDataReader = Nothing

                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Categories
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Categories = Nothing
                    End Try
                End Function

                Public Function Category(ByVal oDescription As String, ByVal oType As String) As gloStream.LabModule.Category.Supporting.Category
                    Dim _Category As New gloStream.LabModule.Category.Supporting.Category
                    Try
                        If Not oDescription.Trim = "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader

                            Dim _oType As New gloStream.LabModule.Category.Supporting.Supporting
                            Dim _TypeID As Int16 = _oType.CategoryType_enum_AsValue(oType)
                            _oType = Nothing

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_Description = '" & oDescription & "' AND lm_category_CategoryType = '" & _TypeID & "' AND lm_category_Description IS NOT NULL ORDER BY lm_category_Description ")
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    _Category.ID = oDataReader.Item("lm_category_ID") & ""
                                    _Category.Description = oDataReader.Item("lm_category_Description") & ""
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Category
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Category = Nothing
                    End Try
                End Function

                Public Function Add(ByVal oDescription As String, ByVal oType As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            Dim _oType As New gloStream.LabModule.Category.Supporting.Supporting
                            Dim _TypeID As Int16 = _oType.CategoryType_enum_AsValue(oType)
                            _oType = Nothing

                            Dim _CategoryID As Long = GetNewCategoryID()

                            If Not _CategoryID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("INSERT INTO LM_Category (lm_category_ID,lm_category_Description,lm_category_CategoryType) VALUES (" & _CategoryID & ",'" & oDescription & "','" & _TypeID & "')")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Category added. ", gloAuditTrail.ActivityOutCome.Success)

                                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "Category added.", gloAuditTrail.ActivityOutCome.Success)
                                    ''Added Rahul P on 20101011
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "Category added.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                    ''
                                
                                End If
                            End If

                        End If
                        Return _Result
                    Catch oError As Exception

                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function Modify(ByVal oCategoryToModify As String, ByVal oDescription As String, ByVal oType As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            '//
                            Dim _CategoryID As Long
                            Dim _oCategory As New gloStream.LabModule.Category.Supporting.Category
                            _oCategory = Category(oCategoryToModify, oType)
                            _CategoryID = _oCategory.ID
                            _oCategory = Nothing

                            Dim _oType As New gloStream.LabModule.Category.Supporting.Supporting
                            Dim _TypeID As Int16 = _oType.CategoryType_enum_AsValue(oType)
                            _oType = Nothing
                            '//

                            If Not _CategoryID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("UPDATE LM_Category SET lm_category_Description = '" & oDescription & "', lm_category_CategoryType = '" & _TypeID & "' WHERE lm_category_ID = " & _CategoryID & "")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Category modified. ", gloAuditTrail.ActivityOutCome.Success)

                                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Category modified.", gloAuditTrail.ActivityOutCome.Success)
                                    ''Added Rahul P on 20101011
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Category modified.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                    ''


                                End If
                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function Delete(ByVal oDescription As String, ByVal oType As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            '//
                            Dim _CategoryID As Long
                            Dim _oCategory As New gloStream.LabModule.Category.Supporting.Category
                            _oCategory = Category(oDescription, oType)
                            _CategoryID = _oCategory.ID
                            _oCategory = Nothing
                            '//

                            If Not _CategoryID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("DELETE FROM LM_Category WHERE lm_category_ID = " & _CategoryID & "")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Category deleted. ", gloAuditTrail.ActivityOutCome.Success)

                                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Category deleted.", gloAuditTrail.ActivityOutCome.Success)
                                    ''Added Rahul P on 20101011
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Category deleted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                    ''

                                End If
                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function IsModify(ByVal oCategory As String) As Boolean
                    Return Nothing
                End Function

                Public Function IsDelete(ByVal oCategory As String) As Boolean
                    Return Nothing
                End Function

                Public Function IsExists(ByVal oDescription As String, ByVal oType As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If Not oDescription.Trim = "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase

                            Dim _oType As New gloStream.LabModule.Category.Supporting.Supporting
                            Dim _TypeID As Int16 = _oType.CategoryType_enum_AsValue(oType)
                            _oType = Nothing

                            oDB.Connect(GetConnectionString)
                            Dim _Count As Integer = 0
                            _Count = Val(oDB.ExecuteQueryScaler("SELECT lm_category_ID FROM LM_Category WHERE lm_category_Description = '" & oDescription & "' AND lm_category_CategoryType = '" & _TypeID & "' AND lm_category_Description IS NOT NULL ") & "")
                            If _Count > 0 Then
                                _Result = True
                            End If
                            _Count = Nothing
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Result = Nothing
                    End Try
                End Function

                Private Function GetNewCategoryID() As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT MAX(lm_category_ID) FROM LM_Category") & "")) + 1
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            '// Supporting Functionality
            Namespace Supporting
                '// Supporting Functions
                Public Class Supporting

                    Public Enum enumCategoryType
                        None = 0
                        Order = 1
                    End Enum

                    Public Function CategoryType_enum_AsString(ByVal oCategoryType As enumCategoryType) As String
                        Select Case oCategoryType
                            Case enumCategoryType.Order
                                Return "Order"
                            Case Else
                                Return "None"
                        End Select
                    End Function

                    Public Function CategoryType_enum_AsValue(ByVal oCategoryType As String) As enumCategoryType
                        Select Case oCategoryType
                            Case "Order"
                                Return enumCategoryType.Order
                            Case Else
                                Return enumCategoryType.None
                        End Select
                    End Function

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                '// Category
                Public Class Category
                    Private _CategoryID As Long
                    Private _CategoryDescription As String

                    Public Property ID() As Long
                        Get
                            Return _CategoryID
                        End Get
                        Set(ByVal Value As Long)
                            _CategoryID = Value
                        End Set
                    End Property

                    Public Property Description() As String
                        Get
                            Return _CategoryDescription
                        End Get
                        Set(ByVal Value As String)
                            _CategoryDescription = Value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                '// Categories Collection
                Public Class Categories
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection
                    Public Sub Dispose()
                        If (IsNothing(mCol) = False) Then
                            mCol.Clear()
                            mCol = Nothing
                        End If
                    End Sub
                    Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.LabModule.Category.Supporting.Category
                        Dim objNewMember As gloStream.LabModule.Category.Supporting.Category
                        objNewMember = New gloStream.LabModule.Category.Supporting.Category
                        objNewMember.ID = oID
                        objNewMember.Description = oDescription
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    End Function

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.LabModule.Category.Supporting.Category
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

        '*****************SPECIMEN**********************
        Namespace Specimen

            
            Public Class MaintainSpecimen
                Private _ErrorMessage As String

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function Specimens() As gloStream.LabModule.Specimen.Supporting.Specimens
                    Dim _Specimens As New gloStream.LabModule.Specimen.Supporting.Specimens
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        Dim oDataReader As SqlClient.SqlDataReader

                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords("SELECT lm_specimen_ID,lm_specimen_Description FROM LM_Specimen WHERE lm_specimen_Description IS NOT NULL")
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _Specimens.Add(oDataReader.Item("lm_specimen_ID") & "", oDataReader.Item("lm_specimen_Description") & "")
                            End While
                        End If
                        oDataReader.Close()
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        Return _Specimens
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Specimens = Nothing
                    End Try
                End Function

                Public Function Specimen(ByVal oDescription As String) As gloStream.LabModule.Specimen.Supporting.Specimen
                    Dim _Specimen As New gloStream.LabModule.Specimen.Supporting.Specimen
                    Try
                        If Not oDescription.Trim = "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords("SELECT lm_specimen_ID,lm_specimen_Description FROM LM_Specimen WHERE lm_specimen_Description = '" & oDescription & "' AND lm_specimen_Description IS NOT NULL")
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    _Specimen.ID = oDataReader.Item("lm_specimen_ID") & ""
                                    _Specimen.Description = oDataReader.Item("lm_specimen_Description") & ""
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Specimen
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Specimen = Nothing
                    End Try
                End Function

                Public Function Add(ByVal oDescription As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            Dim _SpecimenID As Long = GetNewSpecimenID()

                            If Not _SpecimenID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("INSERT INTO LM_Specimen (lm_specimen_ID,lm_specimen_Description) VALUES (" & _SpecimenID & ",'" & oDescription & "')")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                    'Dim objAudit As New clsAudit
                                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oDescription & "' Labs Specimen Added", gstrLoginName, gstrClientMachineName)
                                    'objAudit = Nothing
                                End If
                            End If

                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function Modify(ByVal oSpecimenToModify As String, ByVal oDescription As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            '//
                            Dim _SpecimenID As Long
                            Dim _oSpecimen As New gloStream.LabModule.Specimen.Supporting.Specimen
                            _oSpecimen = Specimen(oSpecimenToModify)
                            _SpecimenID = _oSpecimen.ID
                            _oSpecimen = Nothing

                            If Not _SpecimenID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("UPDATE LM_Specimen SET lm_specimen_Description = '" & oDescription & "' WHERE lm_specimen_ID = " & _SpecimenID & "")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                    
                                End If
                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function Delete(ByVal oDescription As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oDescription.Trim <> "" Then
                            '//
                            Dim _SpecimenID As Long
                            Dim _oSpecimen As New gloStream.LabModule.Specimen.Supporting.Specimen
                            _oSpecimen = Specimen(oDescription)
                            _SpecimenID = _oSpecimen.ID
                            _oSpecimen = Nothing
                            '//

                            If Not _SpecimenID = 0 Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                _Result = oDB.ExecuteNonSQLQuery("DELETE FROM LM_Specimen WHERE lm_specimen_ID = " & _SpecimenID & "")
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If _Result = True Then
                                   
                                End If
                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function IsModify(ByVal oSpecimen As String) As Boolean
                    Return Nothing
                End Function

                Public Function IsDelete(ByVal oSpecimen As String) As Boolean
                    Return Nothing
                End Function

                Public Function IsExists(ByVal oDescription As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If Not oDescription.Trim = "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase

                            oDB.Connect(GetConnectionString)
                            Dim _Count As Integer = 0
                            _Count = Val(oDB.ExecuteQueryScaler("SELECT lm_specimen_ID FROM LM_Specimen WHERE lm_specimen_Description = '" & oDescription & "' AND lm_specimen_Description IS NOT NULL ") & "")
                            If _Count > 0 Then
                                _Result = True
                            End If
                            _Count = Nothing
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Result = Nothing
                    End Try
                End Function

                Private Function GetNewSpecimenID() As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT MAX(lm_specimen_ID) FROM LM_Specimen") & "")) + 1
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            '// Supporting Functionality
            Namespace Supporting
                '// Supporting Functions


                '// Category
                Public Class Specimen
                    Private _SpecimenID As Long
                    Private _SpecimenDescription As String

                    Public Property ID() As Long
                        Get
                            Return _SpecimenID
                        End Get
                        Set(ByVal Value As Long)
                            _SpecimenID = Value
                        End Set
                    End Property

                    Public Property Description() As String
                        Get
                            Return _SpecimenDescription
                        End Get
                        Set(ByVal Value As String)
                            _SpecimenDescription = Value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                '// Categories Collection
                Public Class Specimens
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection
                    Public Sub Dispose()
                        If (IsNothing(mCol) = False) Then
                            mCol.Clear()
                            mCol = Nothing
                        End If
                    End Sub
                    Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.LabModule.Specimen.Supporting.Specimen
                        Dim objNewMember As gloStream.LabModule.Specimen.Supporting.Specimen
                        objNewMember = New gloStream.LabModule.Specimen.Supporting.Specimen
                        objNewMember.ID = oID
                        objNewMember.Description = oDescription
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    End Function

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.LabModule.Specimen.Supporting.Specimen
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

        '*****************TEST**********************
        Namespace Test

            Public Class MaintainTest
                Private _ErrorMessage As String

                Dim objParaFieldID As SqlParameter = Nothing
                Dim objParaAssociatedEM As SqlParameter = Nothing
                Dim objParaFieldType As SqlParameter = Nothing
                Dim objParaAssociatedEMCategory As SqlParameter = Nothing
                Dim objParasStatus As SqlParameter = Nothing

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function Tests(ByVal oCategory As String) As gloStream.LabModule.Test.Supporting.Tests
                    Dim _Tests As New gloStream.LabModule.Test.Supporting.Tests
                    Dim _Test As gloStream.LabModule.Test.Supporting.Test

                    Try
                        If oCategory.Trim <> "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader
                            Dim _strSQL As String

                            _strSQL = "SELECT LM_Test.lm_test_Name, LM_Category.lm_category_Description " _
                            & " FROM LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                            & " WHERE (LM_Category.lm_category_Description = '" & oCategory & "' AND LM_Test.lm_test_Name IS NOT NULL) " _
                            & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_ID, LM_Test.lm_test_TestGroupFlag"

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords(_strSQL)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    ' _Test = New gloStream.LabModule.Test.Supporting.Test
                                    _Test = Test(oDataReader.Item("lm_test_Name") & "", oCategory)
                                    If Not _Test Is Nothing Then
                                        _Tests.Add(_Test)
                                    End If
                                    _Test = Nothing
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Tests
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Test = Nothing
                        _Tests = Nothing
                    End Try
                End Function

                Public Function Test(ByVal oTestName As String, ByVal oCategory As String) As gloStream.LabModule.Test.Supporting.Test
                    Dim _Test As New gloStream.LabModule.Test.Supporting.Test
                    Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                    Try
                        If Not (oTestName.Trim = "" AndAlso oCategory.Trim = "") Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader
                            Dim _strSQL As String

                            _strSQL = "SELECT LM_Test.lm_test_ID, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_CategoryID, " _
                            & " LM_Category.lm_category_Description, LM_Test.lm_test_Template_ID, TemplateGallery_MST.sTemplateName, " _
                            & " LM_Test.lm_test_LabResultID, " _
                            & " LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_LevelNo, " _
                            & " LM_Test.lm_test_Dimension, LM_Test.lm_test_MaleLowerValue, LM_Test.lm_test_MaleHigherValue, " _
                            & " LM_Test.lm_test_FemaleLowerValue, LM_Test.lm_test_FemaleHigherValue, LM_Test.lm_test_IsSpecimenRequired, " _
                            & " LM_Test.lm_test_SpecimenID, LM_Specimen.lm_specimen_Description ,isnull(LM_Test.lm_test_sLonicID,'')as lm_test_sLonicID" _
                            & " FROM LM_Test LEFT OUTER JOIN " _
                            & " TemplateGallery_MST ON LM_Test.lm_test_Template_ID = TemplateGallery_MST.nTemplateID LEFT OUTER JOIN LM_Specimen ON LM_Test.lm_test_SpecimenID = LM_Specimen.lm_specimen_ID LEFT OUTER JOIN " _
                            & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID " _
                            & " WHERE LM_Category.lm_category_Description = '" & oCategory & "' AND LM_Test.lm_test_Name = '" & oTestName & "' AND LM_Test.lm_test_Name IS NOT NULL " _
                            & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_ID, LM_Test.lm_test_TestGroupFlag"

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords(_strSQL)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    With _Test
                                        .ID = CLng(Val(oDataReader.Item("lm_test_ID") & ""))
                                        .Name = Trim(oDataReader.Item("lm_test_Name") & "")
                                        ''Added Rahul for LONIC Code on 20101020
                                        .LoincCode = Trim(oDataReader.Item("lm_test_sLonicID") & "")
                                        ''End
                                        .TestGroupFlagCode = Trim(oDataReader.Item("lm_test_TestGroupFlag") & "")
                                        .TestGroupFlagName = oSupporting.GetTestGroupFlagName(oDataReader.Item("lm_test_TestGroupFlag") & "")
                                        .CategoryID = CLng(Val(oDataReader.Item("lm_test_CategoryID") & ""))
                                        If Not IsDBNull(oDataReader.Item("lm_category_Description")) Then
                                            .CategoryName = Trim(oDataReader.Item("lm_category_Description") & "")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_Template_ID")) Then
                                            .TemplateID = CLng(Val(oDataReader.Item("lm_test_Template_ID") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sTemplateName")) Then
                                            .TemplateName = Trim(oDataReader.Item("sTemplateName") & "")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_LabResultID")) Then
                                            .LabResultID = oDataReader.Item("lm_test_LabResultID")
                                            .LabResultName = GetLabResultName(oDataReader.Item("lm_test_LabResultID"))
                                        End If
                                        .GroupNo = CLng(Val(oDataReader.Item("lm_test_GroupNo") & ""))
                                        If Not IsDBNull(oDataReader.Item("GroupName")) Then
                                            .GroupName = oDataReader.Item("GroupName") & ""
                                        End If
                                        .LevelNo = CInt(Val(oDataReader.Item("lm_test_LevelNo") & ""))
                                        If Not IsDBNull(oDataReader.Item("lm_test_Dimension")) Then
                                            .Dimension = Trim(oDataReader.Item("lm_test_Dimension") & "")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_MaleLowerValue")) Then
                                            .MaleLowerValue = CDbl(Val(oDataReader.Item("lm_test_MaleLowerValue") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_MaleHigherValue")) Then
                                            .MaleHigherValue = CDbl(Val(oDataReader.Item("lm_test_MaleHigherValue") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_FemaleLowerValue")) Then
                                            .FemaleLowerValue = CDbl(Val(oDataReader.Item("lm_test_FemaleLowerValue") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_FemaleHigherValue")) Then
                                            .FemaleHigherValue = CDbl(Val(oDataReader.Item("lm_test_FemaleHigherValue") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_IsSpecimenRequired")) Then
                                            If Val(oDataReader.Item("lm_test_IsSpecimenRequired") & "") > 0 Then
                                                .IsSpecimenRequired = True
                                            Else
                                                .IsSpecimenRequired = False
                                            End If
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_test_SpecimenID")) Then
                                            .SpecimenID = CLng(Val(oDataReader.Item("lm_test_SpecimenID") & ""))
                                        End If
                                        If Not IsDBNull(oDataReader.Item("lm_specimen_Description")) Then
                                            .SpecimenName = Trim(oDataReader.Item("lm_specimen_Description") & "")
                                        End If
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Test
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Test = Nothing
                        oSupporting = Nothing
                    End Try
                End Function
                'ADDED arrOrder & arrOtherDiag BY SHUBHANGI 20100617 
                Public Function Add(ByVal oTest As gloStream.LabModule.Test.Supporting.Test, Optional ByVal arrOrder As ArrayList = Nothing, Optional ByVal arrOtherDiag As ArrayList = Nothing, Optional ByVal arrLabs As ArrayList = Nothing, Optional ByVal arrManagement As ArrayList = Nothing) As Boolean
                    Dim _Result As Boolean = False
                    Dim _strSQL As String = ""
                    Dim _mTestID As Long = 0
                    Dim _mTestName As String = ""
                    Dim _mTestGroupFlagCode As String = "N"
                    Dim _mCategoryID As Long = 0
                    Dim _mTemplateID As Long = 0
                    Dim _mLabResultID As Long = 0
                    Dim _mGroupNo As Long = 0
                    Dim _mLevelNo As Integer = 0
                    Dim _mDimension As String = ""
                    Dim _mMaleLowerValue As Double = 0
                    Dim _mMaleHigherValue As Double = 0
                    Dim _mFemaleLowerValue As Double = 0
                    Dim _mFemaleHigherValue As Double = 0
                    Dim _mIsSpecimanRequired As Integer = 0
                    Dim _mSpecimenID As Long = 0
                    ''Added Rahul for LoincCode on 20101020
                    Dim _sLoincCode As String = ""
                    ''
                    Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                    Dim objCmd As SqlCommand = Nothing

                    


                    Try
                        If Not oTest Is Nothing Then
                            If oTest.CategoryName.Trim <> "" Then
                                '// Values
                                _mTestID = GetNewTestID()
                                _mTestName = oTest.Name.Trim
                                _mTestGroupFlagCode = oSupporting.GetTestGroupFlagCode(oTest.TestGroupFlagName.Trim)
                                _mCategoryID = GetCategoryID(oTest.CategoryName.Trim.Replace("'", "''"))
                                _mTemplateID = GetTemplateID(oTest.TemplateName.Trim.Replace("'", "''"))
                                _mLabResultID = GetLabResultID(oTest.LabResultName.Trim.Replace("'", "''"))
                                ''problem: 00000261
                                ''Reason: As flag is not send, so it return ID of first entry from database which may be group and update that group by test.So group with other test get invisible from grid.
                                ''Description: Pass parameter as "G"-Group to get group no.
                                _mGroupNo = GetTestORGroupID(oTest.GroupName.Trim, oTest.CategoryName.Trim.Replace("'", "''"), "G")
                                _mLevelNo = GetNewLevelNumber(_mGroupNo, _mCategoryID)
                                _mDimension = oTest.Dimension.Trim
                                _mMaleLowerValue = oTest.MaleLowerValue
                                _mMaleHigherValue = oTest.MaleHigherValue
                                _mFemaleLowerValue = oTest.FemaleLowerValue
                                _mFemaleHigherValue = oTest.FemaleHigherValue

                                _sLoincCode = oTest.LoincCode

                                If oTest.IsSpecimenRequired = True Then
                                    _mIsSpecimanRequired = 1
                                Else
                                    _mIsSpecimanRequired = 0
                                End If
                                _mSpecimenID = GetSpecimenID(oTest.SpecimenName.Trim)

                                '31-May-16 Aniket: Resolving Bug #95859: gloEMR -> Edit -> Order Templates -> Unable to save New test; Showing message as "Test not added successfully, please try after some time"
                                '' If _mTestID <> 0 AndAlso _mTestName <> "" AndAlso _mTestGroupFlagCode <> "" AndAlso _mCategoryID <> 0 AndAlso _mGroupNo <> 0 AndAlso _mLevelNo <> 0 Then
                                '' changed if condition for incident  100760
                                If Not (_mTestID = 0 AndAlso _mTestName = "" AndAlso _mTestGroupFlagCode = "" AndAlso _mCategoryID = 0 AndAlso _mGroupNo = 0 AndAlso _mLevelNo = 0) Then

                                    _strSQL = "INSERT INTO LM_Test (lm_test_ID,lm_test_Name,lm_test_TestGroupFlag,lm_test_CategoryID,lm_test_Template_ID,lm_test_GroupNo,lm_test_LevelNo,lm_test_Dimension,lm_test_MaleLowerValue,lm_test_MaleHigherValue,lm_test_FemaleLowerValue,lm_test_FemaleHigherValue,lm_test_IsSpecimenRequired,lm_test_SpecimenID,lm_test_LabResultID,lm_test_sLonicID) " _
                                    & " VALUES (" & _mTestID & ",'" & _mTestName & "','" & _mTestGroupFlagCode & "'," & _mCategoryID & "," & _mTemplateID & "," & _mGroupNo & "," & _mLevelNo & ",'" & _mDimension & "'," & _mMaleLowerValue & "," & _mMaleHigherValue & "," & _mFemaleLowerValue & "," & _mFemaleHigherValue & "," & _mIsSpecimanRequired & "," & _mSpecimenID & "," & _mLabResultID & ",'" & _sLoincCode & "')"

                                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                                    oDB.Connect(GetConnectionString)
                                    _Result = oDB.ExecuteNonSQLQuery(_strSQL)

                                    If arrOrder.Count > 0 Or arrOtherDiag.Count > 0 Or arrLabs.Count > 0 Or arrManagement.Count > 0 Then

                                        For i As Integer = 0 To arrOrder.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            objCmd = New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"

                                            objParaFieldID = Nothing
                                            objParaFieldID = New SqlParameter
                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = Nothing
                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = Nothing
                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = Nothing
                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = Nothing
                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)
                                            objParasStatus = Nothing

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            If objCmd IsNot Nothing Then
                                                objCmd.Parameters.Clear()
                                                objCmd.Dispose()
                                                objCmd = Nothing
                                            End If


                                            objCon.Dispose()
                                            objCon = Nothing
                                        Next
                                        For i As Integer = 0 To arrOtherDiag.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            objCmd = New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"

                                            objParaFieldID = Nothing
                                            objParaFieldID = New SqlParameter
                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = Nothing
                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = Nothing
                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = Nothing
                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = Nothing
                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCon.Dispose()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon = Nothing

                                        Next
                                        For i As Integer = 0 To arrLabs.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            objCmd = New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"

                                            objParaFieldID = Nothing
                                            objParaFieldID = New SqlParameter
                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = Nothing
                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"

                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = Nothing
                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = Nothing
                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = Nothing
                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCon.Dispose()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon = Nothing


                                        Next
                                        For i As Integer = 0 To arrManagement.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            objCmd = New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"

                                            objParaFieldID = Nothing
                                            objParaFieldID = New SqlParameter
                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = Nothing
                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = Nothing
                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = Nothing
                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = Nothing
                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon.Dispose()
                                            objCon = Nothing


                                        Next

                                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "EM Field Associated", gstrLoginName, gstrClientMachineName, 0)


                                    End If



                                    oDB.Disconnect()
                                    oDB.Dispose()
                                    oDB = Nothing

                                    'Dim objAudit As New clsAudit
                                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & _mTestName & "' New Test Addes", gstrLoginName, gstrClientMachineName)
                                    'objAudit = Nothing
                                End If

                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _mTestID = Nothing
                        _mTestName = Nothing
                        _mTestGroupFlagCode = Nothing
                        _mCategoryID = Nothing
                        _mTemplateID = Nothing
                        _mGroupNo = Nothing
                        _mLevelNo = Nothing
                        _mDimension = Nothing
                        _mMaleLowerValue = Nothing
                        _mMaleHigherValue = Nothing
                        _mFemaleLowerValue = Nothing
                        _mFemaleHigherValue = Nothing
                        _mIsSpecimanRequired = Nothing
                        _mSpecimenID = Nothing
                        _sLoincCode = Nothing
                        oSupporting = Nothing

                        If objCmd IsNot Nothing Then
                            objCmd.Parameters.Clear()
                            objCmd.Dispose()
                            objCmd = Nothing
                        End If
                        objParaFieldID = Nothing
                        objParaAssociatedEM = Nothing
                        objParaFieldType = Nothing
                        objParaAssociatedEMCategory = Nothing
                        objParasStatus = Nothing
                    End Try
                End Function
                'Sanjog to not allow same group name or test name in system
                Public Function IsPresent_TestOrGroup(ByVal oTest As gloStream.LabModule.Test.Supporting.Test) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim Cnt As Int16
                    Dim _strSQL As String
                    Try
                        If Not oTest.GroupName = "" Then
                            _strSQL = "SELECT COUNT(ISNULL(lm_test_ID,0)) FROM LM_Test INNER JOIN LM_Category ON LM_Category.lm_category_ID = LM_Test.lm_test_CategoryID WHERE lm_test_Name = '" & oTest.Name & "' AND lm_test_ID <> " & oTest.ID & " AND lm_test_TestGroupFlag ='" & oTest.TestGroupFlagCode & "' AND lm_test_GroupNo IN (SELECT lm_test_ID FROM LM_Test WHERE lm_test_Name = '" & oTest.GroupName & "' AND lm_test_TestGroupFlag ='G') AND LM_Category.lm_category_Description='" & oTest.CategoryName & "' "
                        Else
                            '_strSQL = "SELECT COUNT(ISNULL(lm_test_ID,0)) FROM LM_Test WHERE lm_test_Name = '" & oTest.Name & "' AND lm_test_ID <> " & oTest.ID & " AND lm_test_TestGroupFlag ='" & oTest.TestGroupFlagCode & "' "
                            _strSQL = "SELECT COUNT(ISNULL(LM_Test.lm_test_ID, 0)) AS Expr1 FROM LM_Test INNER JOIN LM_Category ON LM_Category.lm_category_ID = LM_Test.lm_test_CategoryID WHERE (LM_Test.lm_test_Name = '" & oTest.Name & "') AND (LM_Test.lm_test_ID <> " & oTest.ID & ") AND (LM_Test.lm_test_TestGroupFlag = '" & oTest.TestGroupFlagCode & "')	AND (LM_Category.lm_category_Description= '" & oTest.CategoryName & "')"
                        End If

                        oDB.Connect(GetConnectionString)
                        Cnt = oDB.ExecuteQueryScaler(_strSQL)
                        oDB.Disconnect()
                        If Cnt > 0 Then
                            Return True
                        Else
                            Return False
                        End If
                        Return False
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
                        Return Nothing
                    Finally
                        If Not IsNothing(oDB) Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End Try
                End Function
                'Sanjog to not allow same group name or test name in system
                'ADDED arrOrder & arrOtherDiag BY SHUBHANGI 20100617 
                Public Function Modify(ByVal oTestNameToModify As String, ByVal oTest As gloStream.LabModule.Test.Supporting.Test, Optional ByVal arrOrder As ArrayList = Nothing, Optional ByVal arrOtherDiag As ArrayList = Nothing, Optional ByVal arrLabs As ArrayList = Nothing, Optional ByVal arrManagement As ArrayList = Nothing) As Boolean
                    Dim _Result As Boolean = False
                    Dim _strSQL As String = ""
                    Dim _mTestID As Long = 0
                    Dim _mTestName As String = ""
                    Dim _mTestGroupFlagCode As String = "N"
                    Dim _mCategoryID As Long = 0
                    Dim _mTemplateID As Long = 0
                    Dim _mLabResultID As Long = 0
                    Dim _mGroupNo As Long = 0

                    Dim _mLevelNo As Integer = 0
                    Dim _mDimension As String = ""
                    Dim _mMaleLowerValue As Double = 0
                    Dim _mMaleHigherValue As Double = 0
                    Dim _mFemaleLowerValue As Double = 0
                    Dim _mFemaleHigherValue As Double = 0
                    Dim _mIsSpecimanRequired As Integer = 0
                    Dim _mSpecimenID As Long = 0

                    ''Added Rahul on 20101020
                    Dim _sLoincCode As String = ""
                    ''

                    Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting

                    Try
                        If Not oTest Is Nothing Then
                            If oTest.CategoryName.Trim <> "" Then


                                '// Values
                                ''problem: 00000261
                                ''Reason: As flag is not send, so it return ID of first entry from database which may be group and update that group by test.So group with other test get invisible from grid.
                                ''Description: Pass parameter as "T"-Test if test is modify and "G"-Group if group is modify.
                                _mTestID = GetTestORGroupID(oTestNameToModify, oTest.CategoryName.Trim.Replace("'", "''"), oTest.TestGroupFlagCode)
                                _mTestName = oTest.Name.Trim
                                _mTestGroupFlagCode = oSupporting.GetTestGroupFlagCode(oTest.TestGroupFlagName.Trim)
                                _mCategoryID = GetCategoryID(oTest.CategoryName.Trim.Replace("'", "''"))
                                _mTemplateID = GetTemplateID(oTest.TemplateName.Trim.Replace("'", "''"))
                                _mLabResultID = GetLabResultID(oTest.LabResultName.Trim.Replace("'", "''"))
                                ''problem: 00000261
                                ''Reason: As flag is not send, so it return ID of first entry from database which may be group and update that group by test.So group with other test get invisible from grid.
                                ''Description: Pass parameter as "G"-Group to get group no.
                                _mGroupNo = GetTestORGroupID(oTest.GroupName.Trim, oTest.CategoryName.Trim.Replace("'", "''"), "G")
                                _mLevelNo = GetNewLevelNumber(_mGroupNo, _mCategoryID)
                                _mDimension = oTest.Dimension.Trim
                                _mMaleLowerValue = oTest.MaleLowerValue
                                _mMaleHigherValue = oTest.MaleHigherValue
                                _mFemaleLowerValue = oTest.FemaleLowerValue
                                _mFemaleHigherValue = oTest.FemaleHigherValue
                                _sLoincCode = oTest.LoincCode
                                If oTest.IsSpecimenRequired = True Then
                                    _mIsSpecimanRequired = 1
                                Else
                                    _mIsSpecimanRequired = 0
                                End If
                                _mSpecimenID = GetSpecimenID(oTest.SpecimenName.Trim)

                                '' If _mTestID <> 0 AndAlso _mTestName <> "" AndAlso _mTestGroupFlagCode <> "" AndAlso _mCategoryID <> 0 AndAlso _mGroupNo <> 0 AndAlso _mLevelNo <> 0 Then
                                '' changed if condition for incident  100760
                                If Not (_mTestID = 0 AndAlso _mTestName = "" AndAlso _mTestGroupFlagCode = "" AndAlso _mCategoryID = 0 AndAlso _mGroupNo = 0 AndAlso _mLevelNo = 0) Then

                                    '// Insert Record
                                    _strSQL = "UPDATE LM_Test SET lm_test_Name = '" & _mTestName & "', lm_test_TestGroupFlag = '" & _mTestGroupFlagCode & "', lm_test_CategoryID = " & _mCategoryID & ", lm_test_Template_ID = " & _mTemplateID & ", lm_test_GroupNo = " & _mGroupNo & ", " _
                                    & " lm_test_LevelNo = " & _mLevelNo & ", lm_test_Dimension = '" & _mDimension & "', lm_test_MaleLowerValue = " & _mMaleLowerValue & ", lm_test_MaleHigherValue = " & _mMaleHigherValue & ", lm_test_FemaleLowerValue = " & _mFemaleLowerValue & ", lm_test_FemaleHigherValue = " & _mFemaleHigherValue & ", " _
                                    & " lm_test_IsSpecimenRequired = " & _mIsSpecimanRequired & ", lm_test_SpecimenID = " & _mSpecimenID & ", " _
                                    & " lm_test_LabResultID = " & _mLabResultID & ", " _
                                    & " lm_test_sLonicID = '" & _sLoincCode & "' " _
                                    & " WHERE lm_test_ID = " & _mTestID & ""

                                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                                    oDB.Connect(GetConnectionString)
                                    _Result = oDB.ExecuteNonSQLQuery(_strSQL)


                                    If arrOrder.Count > 0 Or arrOtherDiag.Count > 0 Or arrLabs.Count > 0 Or arrManagement.Count > 0 Then

                                        Dim strDeleteQRY As String = "DELETE FROM AssociatedEMField WHERE nFieldID= " & _mTestID & " AND nFieldType=2"
                                        oDB.ExecuteQueryNonQuery(strDeleteQRY)


                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "EM Field Deleted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                        For i As Integer = 0 To arrOrder.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            Dim objCmd As New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"
                                            objParaFieldID = New SqlParameter

                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrOrder.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon.Dispose()
                                            objCon = Nothing
                                        Next

                                        For i As Integer = 0 To arrOtherDiag.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            Dim objCmd As New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"
                                            objParaFieldID = New SqlParameter

                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon.Dispose()
                                            objCon = Nothing
                                        Next

                                        For i As Integer = 0 To arrLabs.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            Dim objCmd As New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"
                                            objParaFieldID = New SqlParameter

                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon.Dispose()
                                            objCon = Nothing
                                        Next

                                        For i As Integer = 0 To arrManagement.Count - 1
                                            Dim objCon As New SqlConnection
                                            objCon.ConnectionString = GetConnectionString()
                                            Dim objCmd As New SqlCommand
                                            objCmd.CommandType = CommandType.StoredProcedure
                                            objCmd.CommandText = "FillEMFields"
                                            objParaFieldID = New SqlParameter

                                            With objParaFieldID
                                                .ParameterName = "@nFieldID"
                                                .Value = _mTestID
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldID)

                                            objParaAssociatedEM = New SqlParameter
                                            With objParaAssociatedEM
                                                .ParameterName = "@sAssociatedEMName"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Description
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEM)

                                            objParaFieldType = New SqlParameter
                                            With objParaFieldType
                                                .ParameterName = "@nFieldType"
                                                .Value = 2
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.BigInt
                                            End With
                                            objCmd.Parameters.Add(objParaFieldType)

                                            objParaAssociatedEMCategory = New SqlParameter
                                            With objParaAssociatedEMCategory
                                                .ParameterName = "@sAssociatedEMCategory"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Code
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParaAssociatedEMCategory)

                                            objParasStatus = New SqlParameter
                                            With objParasStatus
                                                .ParameterName = "@sStatus"
                                                .Value = CType(arrManagement.Item(i), gloGeneralItem.gloItem).Status
                                                .Direction = ParameterDirection.Input
                                                .SqlDbType = SqlDbType.VarChar
                                            End With
                                            objCmd.Parameters.Add(objParasStatus)

                                            objCmd.Connection = objCon
                                            objCon.Open()
                                            objCmd.ExecuteNonQuery()
                                            objCon.Close()
                                            objCmd.Parameters.Clear()
                                            objCmd.Dispose()
                                            objCmd = Nothing
                                            objCon.Dispose()
                                            objCon = Nothing
                                        Next
                                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "EM Field Updated.", gloAuditTrail.ActivityOutCome.Success)
                                        ''Added Rahul P on 20101011
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "EM Field Updated.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                        ''

                                    End If
                                    oDB.Disconnect()
                                    oDB.Dispose()
                                    oDB = Nothing

                                    'Dim objAudit As New clsAudit
                                    'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "'" & _mTestName & "' Test modify with the name " & oTestNameToModify & "'", gstrLoginName, gstrClientMachineName)
                                    'objAudit = Nothing
                                End If
                            End If
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _mTestID = Nothing
                        _mTestName = Nothing
                        _mTestGroupFlagCode = Nothing
                        _mCategoryID = Nothing
                        _mTemplateID = Nothing
                        _mGroupNo = Nothing
                        _mLevelNo = Nothing
                        _mDimension = Nothing
                        _mMaleLowerValue = Nothing
                        _mMaleHigherValue = Nothing
                        _mFemaleLowerValue = Nothing
                        _mFemaleHigherValue = Nothing
                        _mIsSpecimanRequired = Nothing
                        _mSpecimenID = Nothing

                        oSupporting = Nothing

                        objParaFieldID = Nothing
                        objParaAssociatedEM = Nothing
                        objParaFieldType = Nothing
                        objParaAssociatedEMCategory = Nothing
                        objParasStatus = Nothing
                    End Try

                End Function

                Public Function Delete(ByVal oTestName As String, ByVal oCategory As String, ByVal TestID As Int64) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oTestName.Trim <> "" Then
                            Dim _TestID As Long
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            oDB.Connect(GetConnectionString)
                            '_TestID = CLng(Val(oDB.ExecuteQueryScaler("Select LM_Test.lm_test_ID FROM LM_Test INNER JOIN  LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID WHERE LM_Test.lm_test_Name= '" & oTestName & "' AND LM_Category.lm_category_Description = '" & oCategory & "'") & ""))
                            _TestID = TestID
                            _Result = oDB.ExecuteNonSQLQuery("DELETE FROM LM_Test WHERE lm_test_ID = " & _TestID & "")
                            'ADDED BY SHUBHANGI 20100617 COZ WE WANT TO DELETE THIS RECORD FROM ASSOCITATION TABLE ALSO
                            _Result = oDB.ExecuteNonSQLQuery("delete FROM AssociatedEMField WHERE nFieldID = " & _TestID & " AND nFieldType = 2")

                            If _Result = True Then
                                'Dim objAudit As New clsAudit
                                'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & oTestName & "' Test Deleted ", gstrLoginName, gstrClientMachineName)
                                'objAudit = Nothing
                                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Test deleted.", gloAuditTrail.ActivityOutCome.Success)
                                ''Added Rahul P on 20101011
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Test deleted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                ''
                            End If

                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    End Try
                End Function

                Public Function IsModify(ByVal oTestName As String, ByVal oCategory As String) As Boolean
                    Return Nothing
                End Function

                Public Function IsDeleteCategory(ByVal oCategory As String) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)

                    Dim _Result As Boolean = True
                    Dim _ResultCount As Long = 0
                    Dim _strSQL As String
                    Try
                        'If category has any group present, don't allow to delete the category

                        _strSQL = "SELECT count(LM_Test.lm_test_ID) AS GroupCount FROM LM_Test " _
                                    & " LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID  " _
                                    & " WHERE LM_Test.lm_test_TestGroupFlag='G' AND LM_Category.lm_category_Description = '" + oCategory + "' "

                        _ResultCount = Val(oDB.ExecuteQueryScaler(_strSQL) & "")

                        If _ResultCount > 0 Then
                            _Result = False
                            _ErrorMessage = "You can't delete this category because it contains groups inside."
                            IsDeleteCategory = Nothing
                            Exit Function
                        End If

                        '// Check Transected  ' Remark

                        Return _Result

                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

                Public Function IsDelete(ByVal oTestName As String, ByVal oCategory As String, ByVal TestId As Int16) As Boolean
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)

                    Dim _Result As Boolean = True
                    Dim _ResultCount As Long = 0
                    Dim _strSQL As String
                    Try
                        'If Group and have tests then dont allowed
                        If TestId > 0 Then
                            _strSQL = "SELECT COUNT(LM_Test.lm_test_ID) FROM LM_Test INNER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
                                                   & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID " _
                                                   & " WHERE  (LM_Test_1.lm_test_Name = '" & oTestName & "') AND (LM_Category.lm_category_Description = '" & oCategory & "') AND (LM_Test_1.lm_test_ID=" & TestId & ")"

                        Else
                            _strSQL = "SELECT COUNT(LM_Test.lm_test_ID) FROM LM_Test INNER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
                                                   & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID " _
                                                   & " WHERE  (LM_Test_1.lm_test_Name = '" & oTestName & "') AND (LM_Category.lm_category_Description = '" & oCategory & "')"
                        End If

                        _ResultCount = Val(oDB.ExecuteQueryScaler(_strSQL) & "")

                        If _ResultCount > 0 Then
                            _Result = False
                            _ErrorMessage = "You can not delete this group because its contain a tests"
                            IsDelete = Nothing
                            Exit Function
                        End If

                        '// Check Transected  ' Remark

                        Return _Result

                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

                Public Function IsExists(ByVal oTestName As String, ByVal oCategory As String) As Boolean
                    Dim _Result As Boolean = False
                    Try
                        If oTestName.Trim <> "" AndAlso oCategory.Trim <> "" Then
                            Dim oDB As New gloStream.gloDataBase.gloDataBase

                            oDB.Connect(GetConnectionString)
                            Dim _Count As Integer = 0
                            _Count = Val(oDB.ExecuteQueryScaler("Select LM_Test.lm_test_ID FROM LM_Test INNER JOIN  LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID WHERE LM_Test.lm_test_Name= '" & oTestName & "' AND LM_Category.lm_category_Description = '" & oCategory & "'") & "")
                            If _Count > 0 Then
                                _Result = True
                            End If
                            _Count = Nothing
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return _Result
                    Catch oError As Exception
                        _ErrorMessage = oError.Message
                        Return Nothing
                    Finally
                        _Result = Nothing
                    End Try
                End Function

                Private Function GetNewTestID() As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT MAX(lm_test_ID) FROM LM_Test") & "")) + 1
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function
                ''problem: 00000261
                ''Reason: As flag is not send, so it return ID of first entry from database which may be group and update that group by test.So group with other test get invisible from grid.
                ''Description: Parameter added called as oTestORGroupFlag has value "T"-Test and "G"-Group.
                Private Function GetTestORGroupID(ByVal oTestORGroupName As String, ByVal oCategory As String, ByVal oTestORGroupFlag As String) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("Select LM_Test.lm_test_ID FROM LM_Test INNER JOIN  LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID WHERE LM_Test.lm_test_Name= '" & oTestORGroupName & "' AND LM_Category.lm_category_Description = '" & oCategory & "'AND lm_test_TestGroupFlag = '" & oTestORGroupFlag & "'") & ""))
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function

                Private Function GetNewLevelNumber(ByVal GroupNo As Long, ByVal oCategoryNo As Long) As Integer
                    Dim _ID As Long
                    If GroupNo = 0 Then
                        _ID = 1
                    Else
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT lm_test_LevelNo FROM LM_Test WHERE lm_test_GroupNo = " & GroupNo & " AND lm_test_CategoryID = " & oCategoryNo & " ") & "")) + 1
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                    Return _ID
                End Function

                Private Function GetCategoryID(ByVal oCategory As String) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT lm_category_ID FROM LM_Category WHERE lm_category_Description = '" & oCategory.Trim & "'")))
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function

                Private Function GetSpecimenID(ByVal oSpecimen As String) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long
                    oDB.Connect(GetConnectionString)
                    _ID = CLng(Val(oDB.ExecuteQueryScaler("SELECT lm_specimen_ID FROM LM_Specimen WHERE lm_specimen_Description = '" & oSpecimen & "'") & ""))
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _ID
                End Function

                Private Function GetTemplateID(ByVal oTemplate As String) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long = 0
                    Dim _Result As String = ""
                    If oTemplate.Trim <> "" Then
                        oDB.Connect(GetConnectionString)
                        _Result = oDB.ExecuteQueryScaler("SELECT nTemplateID FROM TemplateGallery_MST WHERE sTemplateName = '" & oTemplate & "'") & ""
                        If _Result.Trim <> "" Then
                            _ID = Convert.ToInt64(_Result)
                        End If
                        oDB.Disconnect()
                        oDB = Nothing
                    End If
                    Return _ID
                End Function

                Private Function GetLabResultID(ByVal oLabResult As String) As Long
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _ID As Long = 0
                    Dim _Result As String = ""
                    If oLabResult.Trim <> "" Then
                        oDB.Connect(GetConnectionString)
                        _Result = oDB.ExecuteQueryScaler("SELECT nFlowSheetID FROM LM_LabResult_MST WHERE sFlowSheetName = '" & oLabResult & "'") & ""
                        If _Result.Trim <> "" Then
                            _ID = Convert.ToInt64(_Result)
                        End If
                        oDB.Disconnect()
                        oDB = Nothing
                    End If
                    Return _ID
                End Function

                Private Function GetLabResultName(ByVal oLabResultID As Long) As String
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    Dim _Result As String
                    oDB.Connect(GetConnectionString)
                    _Result = Convert.ToString(oDB.ExecuteQueryScaler("SELECT sFlowSheetName FROM LM_LabResult_MST WHERE nFlowSheetID = " & oLabResultID & "") & "")
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    Return _Result
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            '// Supporting Functionality
            Namespace Supporting
                '// Supporting Functions
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

                    Public Function Groups(ByVal oCategory As String) As Collection
                        Dim _Groups As New Collection
                        Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                        Try
                            If Not (oCategory.Trim = "") Then
                                Dim oDB As New gloStream.gloDataBase.gloDataBase
                                Dim oDataReader As SqlClient.SqlDataReader
                                Dim _strSQL As String

                                _strSQL = "SELECT LM_Test.lm_test_ID, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_CategoryID, " _
                                & " LM_Category.lm_category_Description, LM_Test.lm_test_Template_ID, TemplateGallery_MST.sTemplateName, " _
                                & " LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_LevelNo, " _
                                & " LM_Test.lm_test_Dimension, LM_Test.lm_test_MaleLowerValue, LM_Test.lm_test_MaleHigherValue, " _
                                & " LM_Test.lm_test_FemaleLowerValue, LM_Test.lm_test_FemaleHigherValue, LM_Test.lm_test_IsSpecimenRequired, " _
                                & " LM_Test.lm_test_SpecimenID, LM_Specimen.lm_specimen_Description " _
                                & " FROM LM_Test LEFT OUTER JOIN " _
                                & " TemplateGallery_MST ON LM_Test.lm_test_Template_ID = TemplateGallery_MST.nTemplateID LEFT OUTER JOIN LM_Specimen ON LM_Test.lm_test_SpecimenID = LM_Specimen.lm_specimen_ID LEFT OUTER JOIN " _
                                & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID " _
                                & " WHERE LM_Category.lm_category_Description = '" & oCategory & "' AND LM_Test.lm_test_TestGroupFlag = 'G' AND LM_Test.lm_test_Name IS NOT NULL " _
                                & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_ID, LM_Test.lm_test_TestGroupFlag"

                                oDB.Connect(GetConnectionString)
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        With _Groups
                                            '//.ID = CLng(Val(oDataReader.Item("lm_test_ID") & ""))
                                            .Add(Trim(oDataReader.Item("lm_test_Name") & ""))
                                        End With
                                    End While
                                End If
                                oDataReader.Close()
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                            Return _Groups
                        Catch oError As Exception
                            _ErrorMessage = oError.Message
                            Return Nothing
                        Finally
                            _Groups = Nothing
                            oSupporting = Nothing
                        End Try
                    End Function
                    Public Function GetAssociatedEMField(ByVal TestID As Int64) As String
                        Dim oDB As New gloStream.gloDataBase.gloDataBase

                        Dim str As String = ""
                        Dim strQRY As String = ""

                        Try
                            strQRY = "SELECT sAssociatedEMName FROM AssociatedEMField WHERE nFieldID = " & TestID & " AND nFieldType = '2'"
                            oDB.Connect(GetConnectionString())
                            str = oDB.ExecuteQueryScaler(strQRY)

                            'Audit Trail
                            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Select Record", gstrLoginName, gstrClientMachineName, 0)
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, "Select Record.", gloAuditTrail.ActivityOutCome.Success)
                            ''Added Rahul P on 20101011
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, "Select Record.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''

                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing


                            Return str
                        Catch ex As Exception
                            Return Nothing
                        Finally

                        End Try

                    End Function


                    Public Function Templates() As Collection
                        Dim _Templates As New Collection
                        Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                        Try
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader
                            Dim _strSQL As String

                            ' _strSQL = "SELECT nTemplateID,sTemplateName FROM TemplateGallery_MST ORDER BY sTemplateName"
                            'Code Added by Mayuri:20090924 
                            'In order to fix Bug Id:4051 To fill Templates only thos who are associated with Category Orders
                            _strSQL = "SELECT nTemplateID,sTemplateName FROM TemplateGallery_MST WHERE sCategoryName='Orders' ORDER BY sTemplateName"
                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords(_strSQL)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    With _Templates
                                        '//.ID = CLng(Val(oDataReader.Item("lm_test_ID") & ""))
                                        .Add(Trim(oDataReader.Item("sTemplateName") & ""))
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing

                            Return _Templates
                        Catch oError As Exception
                            _ErrorMessage = oError.Message
                            Return Nothing
                        Finally
                            _Templates = Nothing
                            oSupporting = Nothing
                        End Try
                    End Function

                    Public Function LabResults() As Collection
                        Dim _LabResults As New Collection
                        Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                        Try
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim oDataReader As SqlClient.SqlDataReader
                            Dim _strSQL As String

                            _strSQL = "SELECT DISTINCT nFlowSheetID,sFlowSheetName FROM LM_LabResult_MST ORDER BY sFlowSheetName"

                            oDB.Connect(GetConnectionString)
                            oDataReader = oDB.ReadQueryRecords(_strSQL)
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    With _LabResults
                                        '//.ID = CLng(Val(oDataReader.Item("lm_test_ID") & ""))
                                        .Add(Trim(oDataReader.Item("sFlowSheetName") & ""))
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing

                            Return _LabResults
                        Catch oError As Exception
                            _ErrorMessage = oError.Message
                            Return Nothing
                        Finally
                            _LabResults = Nothing
                            oSupporting = Nothing
                        End Try
                    End Function

                    Public Function GetTestGroupFlagCode(ByVal oTestGroupFlag As String) As String
                        Select Case oTestGroupFlag
                            Case "Test"
                                Return "T"
                            Case "Group"
                                Return "G"
                            Case Else
                                Return "N"
                        End Select
                    End Function

                    Public Function GetTestGroupFlagName(ByVal oTestGroupFlagCode As String) As String
                        Select Case oTestGroupFlagCode
                            Case "T"
                                Return "Test"
                            Case "G"
                                Return "Group"
                            Case Else
                                Return "None"
                        End Select
                    End Function

                    Public Function GetTestGroupFlagNames() As Collection
                        Dim _TestGroupFlagNames As New Collection
                        _TestGroupFlagNames.Add("Test")
                        _TestGroupFlagNames.Add("Group")
                        Return _TestGroupFlagNames
                    End Function

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                '// Test
                Public Class Test
                    Private _TestID As Long = 0
                    Private _TestName As String = ""
                    Private _TestGroupFlagCode As String = "N"
                    Private _TestGroupFlagName As String = "None"
                    Private _CategoryID As Long = 0
                    Private _CategoryName As String = ""
                    Private _TemplateID As Long = 0
                    Private _TemplateName As String = ""
                    Private _LabResultID As Long = 0
                    Private _LabResultName As String = ""
                    Private _GroupNo As Long = 0
                    Private _GroupName As String = ""
                    Private _LevelNo As Integer = 0
                    Private _Dimension As String = ""
                    Private _MaleLowerValue As Double = 0
                    Private _MaleHigherValue As Double = 0
                    Private _FemaleLowerValue As Double = 0
                    Private _FemaleHigherValue As Double = 0
                    Private _IsSpecimanRequired As Boolean = False
                    Private _SpecimenID As Long = 0
                    Private _SpecimenName As String = ""
                    Private _AssociateEM As String
                    Private _AssociateEMCategory As String
                    ''Added Rahul for LOINC Code on 20101020
                    Private _LoincCode As String = ""
                    ''End

                    Public Property ID() As Long
                        Get
                            Return _TestID
                        End Get
                        Set(ByVal Value As Long)
                            _TestID = Value
                        End Set
                    End Property

                    ''Added Rahul for LOINC Code on 20101020
                    Public Property LoincCode() As String
                        Get
                            Return _LoincCode
                        End Get
                        Set(ByVal value As String)
                            _LoincCode = value
                        End Set
                    End Property
                    ''End

                    Public Property Name() As String
                        Get
                            Return _TestName
                        End Get
                        Set(ByVal Value As String)
                            _TestName = Value
                        End Set
                    End Property

                    Public Property TestGroupFlagCode() As String
                        Get
                            Return _TestGroupFlagCode
                        End Get
                        Set(ByVal Value As String)
                            _TestGroupFlagCode = Value
                        End Set
                    End Property

                    Public Property TestGroupFlagName() As String
                        Get
                            Return _TestGroupFlagName
                        End Get
                        Set(ByVal Value As String)
                            _TestGroupFlagName = Value
                        End Set
                    End Property

                    Public Property CategoryID() As Long
                        Get
                            Return _CategoryID
                        End Get
                        Set(ByVal Value As Long)
                            _CategoryID = Value
                        End Set
                    End Property

                    Public Property CategoryName() As String
                        Get
                            Return _CategoryName
                        End Get
                        Set(ByVal Value As String)
                            _CategoryName = Value
                        End Set
                    End Property

                    Public Property LabResultID() As Long
                        Get
                            Return _LabResultID
                        End Get
                        Set(ByVal Value As Long)
                            _LabResultID = Value
                        End Set
                    End Property

                    Public Property LabResultName() As String
                        Get
                            Return _LabResultName
                        End Get
                        Set(ByVal Value As String)
                            _LabResultName = Value
                        End Set
                    End Property

                    Public Property TemplateID() As Long
                        Get
                            Return _TemplateID
                        End Get
                        Set(ByVal Value As Long)
                            _TemplateID = Value
                        End Set
                    End Property

                    Public Property TemplateName() As String
                        Get
                            Return _TemplateName
                        End Get
                        Set(ByVal Value As String)
                            _TemplateName = Value
                        End Set
                    End Property

                    Public Property GroupNo() As Long
                        Get
                            Return _GroupNo
                        End Get
                        Set(ByVal Value As Long)
                            _GroupNo = Value
                        End Set
                    End Property

                    Public Property GroupName() As String
                        Get
                            Return _GroupName
                        End Get
                        Set(ByVal Value As String)
                            _GroupName = Value
                        End Set
                    End Property

                    Public Property LevelNo() As Integer
                        Get
                            Return _LevelNo
                        End Get
                        Set(ByVal Value As Integer)
                            _LevelNo = Value
                        End Set
                    End Property

                    Public Property Dimension() As String
                        Get
                            Return _Dimension
                        End Get
                        Set(ByVal Value As String)
                            _Dimension = Value
                        End Set
                    End Property

                    Public Property MaleLowerValue() As Double
                        Get
                            Return _MaleLowerValue
                        End Get
                        Set(ByVal Value As Double)
                            _MaleLowerValue = Value
                        End Set
                    End Property

                    Public Property MaleHigherValue() As Double
                        Get
                            Return _MaleHigherValue
                        End Get
                        Set(ByVal Value As Double)
                            _MaleHigherValue = Value
                        End Set
                    End Property

                    Public Property FemaleLowerValue() As Double
                        Get
                            Return _FemaleLowerValue
                        End Get
                        Set(ByVal Value As Double)
                            _FemaleLowerValue = Value
                        End Set
                    End Property

                    Public Property FemaleHigherValue() As Double
                        Get
                            Return _FemaleHigherValue
                        End Get
                        Set(ByVal Value As Double)
                            _FemaleHigherValue = Value
                        End Set
                    End Property

                    Public Property IsSpecimenRequired() As Boolean
                        Get
                            Return _IsSpecimanRequired
                        End Get
                        Set(ByVal Value As Boolean)
                            _IsSpecimanRequired = Value
                        End Set
                    End Property

                    Public Property SpecimenID() As Long
                        Get
                            Return _SpecimenID
                        End Get
                        Set(ByVal Value As Long)
                            _SpecimenID = Value
                        End Set
                    End Property

                    Public Property SpecimenName() As String
                        Get
                            Return _SpecimenName
                        End Get
                        Set(ByVal Value As String)
                            _SpecimenName = Value
                        End Set
                    End Property
                    Public Property AssociateEM() As String
                        Get
                            Return _AssociateEM
                        End Get
                        Set(ByVal value As String)
                            _AssociateEM = value
                        End Set
                    End Property

                    Public Property AssociateEMCategory() As String
                        Get
                            Return _AssociateEMCategory
                        End Get
                        Set(ByVal value As String)
                            _AssociateEMCategory = value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                '// Tests Collection
                Public Class Tests
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection
                    Public Sub Dispose()
                        If (IsNothing(mCol) = False) Then
                            mCol.Clear()
                            mCol = Nothing
                        End If
                    End Sub
                    Public Function Add(ByVal oTest As gloStream.LabModule.Test.Supporting.Test) As gloStream.LabModule.Test.Supporting.Test
                        mCol.Add(oTest)
                        Return Nothing
                    End Function

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.LabModule.Test.Supporting.Test
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
End Namespace