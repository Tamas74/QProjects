Imports System.Data.SqlClient
Namespace gloGeneral

    Namespace gloAuditTrail

        Public Enum enmActivityType
            Add
            Modify
            Delete
            Login
            Logout
            Other
            'RecordAdded
            'RecordModified
            'Delete
            RecordViewed
            UserBlocked
            UserUnBlocked
            SecurityAdmin
            ResetPassword
            NodeAuthenticationFailure
            Query
            ImportDocument_FromFile ' Import Document from existing File
            ImportDocument_FromScanner ' Import Document from Scanned Images
            NewDocument  'Categorised pdf file in New Document
            MergeDocument 'Categorised pdf file in existing Document (Merge)
            DeleteDocument ''Categorised pdf file send to recycle bin (delete)
            DeletePage ' Delete Pages from PDF Document
            'Fax
            ''FAX Documents --will be included in PHI Export
            MoveDocumentFromCategory 'Move documents from category
            'Print
            ''Print docs --will be included in PHI Export
            '---------------------------------------------------
            'sarika 25th apr 2007
            PHIImport
            PHIExport
            Import
            Export
            ChangePassword
            PatientRecordAdded
            PatientRecordModified
            PatientRecordDeleted
            PatientRecordViewed
            SignatureCreated
            SignatureValidated

            '---------------------------------------------------

        End Enum

        Public Enum enmOutCome
            Success
            Failure
        End Enum

        Public Class gloAuditTrail

            'Public Shared Function CreateLog(ByVal enmLogActivityType As enmActivityType, ByVal strDescription As String, ByVal sUserName As String, Optional ByVal sMachineName As String = "", Optional ByVal nPatientID As Long = 0, Optional ByVal loginAuditFlag As Boolean = False, Optional ByVal enmLogOutcome As enmOutCome = enmOutCome.Success, Optional ByVal SoftwareComponent As String = "gloEMR") As Boolean
            '    Dim objCon As New SqlConnection

            '    '---------------------------------------
            '    'sarika 25th apr 2007
            '    'strDescription &= "(In gloEMR Admin)"
            '    Dim strSoftwareComponent As String
            '    strSoftwareComponent = SoftwareComponent
            '    '--------------------------------------

            '    Try
            '        objCon.ConnectionString = clsgeneral.GetConnectionstring()
            '        Dim objCmd As New SqlCommand
            '        Dim objSQLDataReader As SqlDataReader
            '        Dim _strSQL As String = ""
            '        Dim blnIsAuditTrailEnabledFlag As Boolean = False
            '        ''''By Mahesh - 20070130

            '        '' For Check If The Login User Has Facility to Make Entry in AditTrial
            '        '' If He HAS the Add the Entry in Audit Trial
            '        '' If NOT then Exit From the Function 
            '        ''''''''''''''''''''''''''''''
            '        _strSQL = "select IsAuditTrail from User_MST where sLoginName = '" & sUserName & "'"
            '        objCmd = New SqlCommand(_strSQL, objCon)
            '        If objCon.State = ConnectionState.Closed Then
            '            objCon.Open()
            '        End If

            '        objSQLDataReader = objCmd.ExecuteReader

            '        If Not objSQLDataReader Is Nothing Then
            '            If objSQLDataReader.HasRows = True Then
            '                While objSQLDataReader.Read
            '                    'the value can be NULL besides 0 and 1 , so chk for null value
            '                    If Not IsDBNull(objSQLDataReader.Item("IsAuditTrail")) Then
            '                        'if not nulll then set the value of flag 
            '                        blnIsAuditTrailEnabledFlag = objSQLDataReader.Item("IsAuditTrail")
            '                    Else
            '                        'if the value is null/ 0 then set the flag to false
            '                        blnIsAuditTrailEnabledFlag = False
            '                    End If
            '                End While
            '            End If
            '            objSQLDataReader.Close()
            '        End If

            '        '  objCon.Close()

            '        If blnIsAuditTrailEnabledFlag = False Then
            '            '' If The Login User does not have Facility to Make Entry in AuditTrial then Exit From the Function 
            '            If loginAuditFlag = False Then
            '                objCon.Close()
            '                Return True
            '            End If
            '        End If


            '        '''''''''''''''''''''''''

            '        'Dim objCon As New SqlConnection
            '        'objCon.ConnectionString = GetConnectionString()


            '        objCmd = New SqlCommand
            '        objCmd.CommandType = CommandType.StoredProcedure
            '        objCmd.CommandText = "gsp_InsertAuditTrail"

            '        Dim objParaCategory As New SqlParameter
            '        With objParaCategory
            '            .ParameterName = "@ActivityCategory"
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        Select Case enmLogActivityType
            '            Case enmActivityType.Add
            '                objParaCategory.Value = "Record Added"
            '            Case enmActivityType.Delete
            '                objParaCategory.Value = "Record Deleted"
            '            Case enmActivityType.Modify
            '                objParaCategory.Value = "Record Modified"
            '            Case enmActivityType.Login
            '                objParaCategory.Value = "Login"
            '                'Sarika
            '                '*************************************
            '            Case enmActivityType.Logout
            '                objParaCategory.Value = "Logout"

            '                '*************************************
            '            Case enmActivityType.DeleteDocument
            '                objParaCategory.Value = "Delete Document"
            '            Case enmActivityType.ImportDocument_FromFile
            '                objParaCategory.Value = "Import Document From File"
            '            Case enmActivityType.ImportDocument_FromScanner
            '                objParaCategory.Value = "Import Document From Scanner"
            '            Case enmActivityType.MergeDocument
            '                objParaCategory.Value = "Merge Document"
            '            Case enmActivityType.NewDocument
            '                objParaCategory.Value = "New Document"
            '            Case enmActivityType.DeletePage
            '                objParaCategory.Value = "Delete Pages"
            '            Case enmActivityType.Other
            '                objParaCategory.Value = "Others"

            '                '// Sarika 26 April 2007 - Start //
            '            Case enmActivityType.PHIImport
            '                objParaCategory.Value = "PHI Import"
            '            Case enmActivityType.PHIExport
            '                objParaCategory.Value = "PHI Export"
            '            Case enmActivityType.Import
            '                objParaCategory.Value = "Import"
            '            Case enmActivityType.Export
            '                objParaCategory.Value = "Export"
            '            Case enmActivityType.ChangePassword
            '                objParaCategory.Value = "Change Password"
            '            Case enmActivityType.PatientRecordAdded
            '                objParaCategory.Value = "Patient Record Added"
            '            Case enmActivityType.PatientRecordModified
            '                objParaCategory.Value = "Patient Record Modified"
            '            Case enmActivityType.PatientRecordDeleted
            '                objParaCategory.Value = "Patient Record Deleted"
            '            Case enmActivityType.PatientRecordViewed
            '                objParaCategory.Value = "Patient Record Viewed"
            '            Case enmActivityType.SignatureCreated
            '                objParaCategory.Value = "SignatureCreated"
            '            Case enmActivityType.SignatureValidated
            '                objParaCategory.Value = "SignatureValidated"

            '            Case enmActivityType.MoveDocumentFromCategory
            '                objParaCategory.Value = "Move Document From Category"

            '            Case enmActivityType.SecurityAdmin
            '                objParaCategory.Value = "Security Administration"
            '            Case enmActivityType.NodeAuthenticationFailure
            '                objParaCategory.Value = "Node Authentication Failure"
            '            Case enmActivityType.Query
            '                objParaCategory.Value = "Query"
            '            Case enmActivityType.RecordViewed
            '                objParaCategory.Value = "Record Viewed"


            '                '// Sarika 26 April 2007 - Finish //

            '        End Select
            '        objCmd.Parameters.Add(objParaCategory)

            '        Dim objParaDescription As New SqlParameter
            '        With objParaDescription
            '            .ParameterName = "@Description"
            '            .Value = strDescription
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        objCmd.Parameters.Add(objParaDescription)

            '        Dim objParaPatientID As New SqlParameter
            '        With objParaPatientID
            '            .ParameterName = "@PatientID"
            '            .Value = nPatientID
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.BigInt
            '        End With
            '        objCmd.Parameters.Add(objParaPatientID)

            '        Dim objParaUserID As New SqlParameter
            '        With objParaUserID
            '            .ParameterName = "@UserName"
            '            .Value = sUserName
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        objCmd.Parameters.Add(objParaUserID)

            '        '--------------------------------------
            '        'sarika 25th apr 2007

            '        Dim objParaSoftwareComponent As New SqlParameter
            '        With objParaSoftwareComponent
            '            .ParameterName = "@sSoftwareComponent"
            '            .Value = strSoftwareComponent
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        objCmd.Parameters.Add(objParaSoftwareComponent)

            '        'sarika 26th Apr 2007
            '        Dim objParaOutcome As New SqlParameter
            '        With objParaOutcome
            '            .ParameterName = "@sOutcome"
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        Select Case enmLogOutcome
            '            Case enmOutCome.Failure
            '                objParaOutcome.Value = "Failure"
            '            Case enmOutCome.Success
            '                objParaOutcome.Value = "Success"
            '        End Select
            '        objCmd.Parameters.Add(objParaOutcome)

            '        '--------------------------------------

            '        Dim objParaMachine As New SqlParameter
            '        With objParaMachine
            '            .ParameterName = "@MachineName"
            '            .Value = sMachineName
            '            .Direction = ParameterDirection.Input
            '            .SqlDbType = SqlDbType.VarChar
            '        End With
            '        objCmd.Parameters.Add(objParaMachine)

            '        objCmd.Connection = objCon
            '        '   objCon.Open()
            '        objCmd.ExecuteNonQuery()
            '        objCon.Close()
            '        objCmd = Nothing
            '        objCon = Nothing

            '        Return True

            '    Catch ex As Exception
            '        Return False
            '    Finally
            '        ' objCon.Close()
            '    End Try
            'End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

    End Namespace

    Public Class clsAudit

        Enum enmActivityType
            Add
            Modify
            Delete
            Login
            Logout
            Other
            RecordViewed
            SecurityAdmin
            NodeAuthenticationFailure
            Query
            ImportDocument_FromFile ' Import Document from existing File
            ImportDocument_FromScanner ' Import Document from Scanned Images
            NewDocument  'Categorised pdf file in New Document
            MergeDocument 'Categorised pdf file in existing Document (Merge)
            DeleteDocument ''Categorised pdf file send to recycle bin (delete)
            DeletePage ' Delete Pages from PDF Document
            'Fax
            ''FAX Documents --will be included in PHI Export
            MoveDocumentFromCategory 'Move documents from category
            'Print
            ''Print docs --will be included in PHI Export
            '---------------------------------------------------
            'sarika 25th apr 2007
            PHIImport
            PHIExport
            Import
            Export
            ChangePassword
            PatientRecordAdded
            PatientRecordModified
            PatientRecordDeleted
            PatientRecordViewed
            SignatureCreated
            SignatureValidated

            '---------------------------------------------------
        End Enum
        Enum enmOutCome
            Success
            Failure
        End Enum

        Public Sub New()
            MyBase.new()
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#Region "   Private Variables"

        Dim _nAuditTrailID As Integer
        Dim _dtActivityDate As Date
        Dim _sActivityCategory As String
        Dim _sDescription As String
        Dim _nPatientID As Long
        Dim _nUserID As Int64

#End Region

#Region "   Public Properties"

        Public ReadOnly Property AuditTrailID() As Integer
            Get
                Return _nAuditTrailID
            End Get
        End Property

        Public ReadOnly Property ActivityDate() As Date
            Get
                Return _dtActivityDate
            End Get
        End Property

        Public Property ActivityCategory() As String
            Get
                Return _sActivityCategory
            End Get
            Set(ByVal Value As String)
                _sActivityCategory = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _sDescription
            End Get
            Set(ByVal Value As String)
                _sDescription = Value
            End Set
        End Property

        Public Property PatientID() As Long
            Get
                Return _nPatientID
            End Get
            Set(ByVal Value As Long)
                _nPatientID = Value
            End Set
        End Property

        Public Property UserID() As Int64
            Get
                Return _nUserID
            End Get
            Set(ByVal Value As Int64)
                _nUserID = Value
            End Set
        End Property

#End Region

#Region "   Public Functions"

        'Public Function Fill_AuditCategory() As Collection
        '    Dim clUsers As New Collection
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()
        '    Dim objCmd As New SqlCommand
        '    Dim objSQLDataReader As SqlDataReader
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_FillAuditCategory"
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objSQLDataReader = objCmd.ExecuteReader()
        '    While objSQLDataReader.Read
        '        clUsers.Add(objSQLDataReader.Item(0))
        '    End While
        '    objSQLDataReader.Close()
        '    objCon.Close()
        '    objCon = Nothing
        '    objCmd = Nothing
        '    objSQLDataReader = Nothing
        '    Return clUsers
        'End Function

        'Public Function Fill_Users() As Collection
        '    Dim clUsers As New Collection
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()
        '    Dim objCmd As New SqlCommand
        '    Dim objSQLDataReader As SqlDataReader
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_FillUsers"
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objSQLDataReader = objCmd.ExecuteReader()
        '    While objSQLDataReader.Read
        '        clUsers.Add(objSQLDataReader.Item(0))
        '    End While
        '    objSQLDataReader.Close()
        '    objCon.Close()
        '    objCon = Nothing
        '    objCmd = Nothing
        '    objSQLDataReader = Nothing
        '    Return clUsers
        'End Function

        Public Function CreateLog(ByVal enmLogActivityType As enmActivityType, ByVal strDescription As String, ByVal sUserName As String, Optional ByVal sMachineName As String = "", Optional ByVal nPatientID As Long = 0, Optional ByVal loginAuditFlag As Boolean = False, Optional ByVal enmLogOutcome As enmOutCome = enmOutCome.Success, Optional ByVal SoftwareComponent As String = "gloEMR") As Boolean

            Dim objCon As New SqlConnection

            Dim objCmd As New SqlCommand

            Dim objParaCategory As New SqlParameter
            Dim objParaDescription As New SqlParameter
            Dim objParaPatientID As New SqlParameter
            Dim objParaUserID As New SqlParameter
            Dim objParaSoftwareComponent As New SqlParameter
            Dim objParaOutcome As New SqlParameter
            Dim objParaMachine As New SqlParameter
            '---------------------------------------
            'sarika 25th apr 2007
            'strDescription &= "(In gloEMR Admin)"
            Dim strSoftwareComponent As String
            strSoftwareComponent = SoftwareComponent
            '--------------------------------------

            Try
                objCon.ConnectionString = clsgeneral.GetConnectionstring()

                Dim objSQLDataReader As SqlDataReader
                Dim _strSQL As String = ""
                Dim blnIsAuditTrailEnabledFlag As Boolean = False
                ''''By Mahesh - 20070130

                '' For Check If The Login User Has Facility to Make Entry in AditTrial
                '' If He HAS the Add the Entry in Audit Trial
                '' If NOT then Exit From the Function 
                ''''''''''''''''''''''''''''''
                _strSQL = "select IsAuditTrail from User_MST where sLoginName = '" & sUserName & "'"
                objCmd = New SqlCommand(_strSQL, objCon)
                If objCon.State = ConnectionState.Closed Then
                    objCon.Open()
                End If

                objSQLDataReader = objCmd.ExecuteReader

                If Not objSQLDataReader Is Nothing Then
                    If objSQLDataReader.HasRows = True Then
                        While objSQLDataReader.Read
                            'the value can be NULL besides 0 and 1 , so chk for null value
                            If Not IsDBNull(objSQLDataReader.Item("IsAuditTrail")) Then
                                'if not nulll then set the value of flag 
                                blnIsAuditTrailEnabledFlag = objSQLDataReader.Item("IsAuditTrail")
                            Else
                                'if the value is null/ 0 then set the flag to false
                                blnIsAuditTrailEnabledFlag = False
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()
                    objSQLDataReader.Dispose()
                End If

                '  objCon.Close()

                If blnIsAuditTrailEnabledFlag = False Then
                    '' If The Login User does not have Facility to Make Entry in AuditTrial then Exit From the Function 
                    If loginAuditFlag = False Then
                        objCon.Close()
                        Return True
                    End If
                End If


                '''''''''''''''''''''''''

                'Dim objCon As New SqlConnection
                'objCon.ConnectionString = GetConnectionString()
                If objCmd IsNot Nothing Then
                    objCmd.Parameters.Clear()
                    objCmd.Dispose()
                    objCmd = Nothing
                End If
                objCmd = New SqlCommand
                objCmd.CommandType = CommandType.StoredProcedure
                objCmd.CommandText = "gsp_InsertAuditTrail"

              

                With objParaCategory
                    .ParameterName = "@ActivityCategory"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                Select Case enmLogActivityType
                    Case enmActivityType.Add
                        objParaCategory.Value = "Record Added"
                    Case enmActivityType.Delete
                        objParaCategory.Value = "Record Deleted"
                    Case enmActivityType.Modify
                        objParaCategory.Value = "Record Modified"
                    Case enmActivityType.Login
                        objParaCategory.Value = "Login"
                        'Sarika
                        '*************************************
                    Case enmActivityType.Logout
                        objParaCategory.Value = "Logout"

                        '*************************************
                    Case enmActivityType.DeleteDocument
                        objParaCategory.Value = "Delete Document"
                    Case enmActivityType.ImportDocument_FromFile
                        objParaCategory.Value = "Import Document From File"
                    Case enmActivityType.ImportDocument_FromScanner
                        objParaCategory.Value = "Import Document From Scanner"
                    Case enmActivityType.MergeDocument
                        objParaCategory.Value = "Merge Document"
                    Case enmActivityType.NewDocument
                        objParaCategory.Value = "New Document"
                    Case enmActivityType.DeletePage
                        objParaCategory.Value = "Delete Pages"
                    Case enmActivityType.Other
                        objParaCategory.Value = "Others"

                        '// Sarika 26 April 2007 - Start //
                    Case enmActivityType.PHIImport
                        objParaCategory.Value = "PHI Import"
                    Case enmActivityType.PHIExport
                        objParaCategory.Value = "PHI Export"
                    Case enmActivityType.Import
                        objParaCategory.Value = "Import"
                    Case enmActivityType.Export
                        objParaCategory.Value = "Export"
                    Case enmActivityType.ChangePassword
                        objParaCategory.Value = "Change Password"
                    Case enmActivityType.PatientRecordAdded
                        objParaCategory.Value = "Patient Record Added"
                    Case enmActivityType.PatientRecordModified
                        objParaCategory.Value = "Patient Record Modified"
                    Case enmActivityType.PatientRecordDeleted
                        objParaCategory.Value = "Patient Record Deleted"
                    Case enmActivityType.PatientRecordViewed
                        objParaCategory.Value = "Patient Record Viewed"
                    Case enmActivityType.SignatureCreated
                        objParaCategory.Value = "SignatureCreated"
                    Case enmActivityType.SignatureValidated
                        objParaCategory.Value = "SignatureValidated"

                    Case enmActivityType.MoveDocumentFromCategory
                        objParaCategory.Value = "Move Document From Category"

                    Case enmActivityType.SecurityAdmin
                        objParaCategory.Value = "Security Administration"
                    Case enmActivityType.NodeAuthenticationFailure
                        objParaCategory.Value = "Node Authentication Failure"
                    Case enmActivityType.Query
                        objParaCategory.Value = "Query"
                    Case enmActivityType.RecordViewed
                        objParaCategory.Value = "Record Viewed"

                End Select
                objCmd.Parameters.Add(objParaCategory)


                If Not System.Configuration.ConfigurationManager.AppSettings("BreakTheGlass") Is Nothing Then
                    If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("BreakTheGlass")) Then
                        strDescription = "Emergency Access:" + strDescription
                    End If
               
                End If

                With objParaDescription
                    .ParameterName = "@Description"
                    .Value = strDescription
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaDescription)


                With objParaPatientID
                    .ParameterName = "@PatientID"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaPatientID)



                With objParaOutcome
                    .ParameterName = "@sOutcome"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                Select Case enmLogOutcome
                    Case enmOutCome.Failure
                        objParaOutcome.Value = "Failure"
                    Case enmOutCome.Success
                        objParaOutcome.Value = "Success"
                End Select
                objCmd.Parameters.Add(objParaOutcome)


                objCmd.Connection = objCon

                objCmd.ExecuteNonQuery()
                objCon.Close()


                Return True

            Catch ex As Exception
                Return False
            Finally
                If objCmd IsNot Nothing Then
                    objCmd.Parameters.Clear()
                    objCmd.Dispose()
                    objCmd = Nothing
                End If
                If objCon IsNot Nothing Then
                    objCon.Dispose()
                    objCon = Nothing
                End If
                objParaCategory = Nothing
                objParaDescription = Nothing
                objParaPatientID = Nothing
                objParaUserID = Nothing
                objParaSoftwareComponent = Nothing
                objParaOutcome = Nothing
                objParaMachine = Nothing
            End Try
        End Function

#End Region

    End Class
End Namespace