Imports System.Data
Imports System.Data.SqlClient
Public Class ClsDBLayer
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        Try
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- New -- " & ex.ToString)
        Catch ex As Exception
            'UpdateLog("ClsDBLayer -- New -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Conn As SqlConnection
    ' Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private CatDataset As System.Data.DataSet
    Private DCatview As DataView
    '  Private Cattable As DataTable
    '  Private CatCommand As System.Data.SqlClient.SqlCommand

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & DCatview.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        DCatview.RowFilter = strexpr
    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = DCatview.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        strexpr = "" & DCatview.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        DCatview.RowFilter = strexpr
    End Sub


    Public Sub FetchData()
        Dim CatCommand As SqlCommand = Nothing
        Try
            CatCommand = New System.Data.SqlClient.SqlCommand("gsp_ViewCategory_Mst", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = CatCommand.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClinicID

            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = CatCommand

            'CatDataset = New System.Data.DataSet
            If (IsNothing(CatDataset) = False) Then
                CatDataset.Dispose()
                CatDataset = Nothing
            End If

            CatDataset = New DataSet
            Adapter.Fill(CatDataset)
            If (IsNothing(DCatview) = False) Then
                DCatview.Dispose()
                DCatview = Nothing
            End If
            If (IsNothing(CatDataset) = False) Then
                If (CatDataset.Tables.Count > 0) Then
                    Dim Cattable As DataTable = CatDataset.Tables(0).Copy()

                    DCatview = New DataView(Cattable)
                    Cattable.Dispose()
                    Cattable = Nothing
                End If
                
            End If
            Adapter.Dispose()
            Adapter = Nothing
            Conn.Close()

            objParam = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, "ClsDBLayer -- FetchData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- FetchData -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, "ClsDBLayer -- FetchData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- FetchData -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
        End Try
        'Return CatDataset
        'Return CatDataset
    End Sub
    '' solving case- GLO2010-0004659
#Region "For the Field Manufacturer"
    Public Sub AddData(ByVal Code As String, ByVal categoryDescription As String)
        Dim objCmd As SqlCommand = Nothing
        Dim _Query As String = ""
        Try
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                _Query = "Insert into Im_Manufacturers(sCodeType, sManufacturerCode, sManufacturer) Values ('Manufacturer', '" & Code & "','" & categoryDescription & "')"
                objCmd = New SqlCommand(_Query, Conn)
                If Not IsNothing(objCmd) Then
                    objCmd.ExecuteNonQuery()
                End If
            End If
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try



    End Sub

    ''20100928 Dhruv :: Add + Update and Delete in (Im_Manufacturers) table called in View - Immunization 
    Public Sub DeleteData(ByVal Code As String, ByVal categoryDescription As String)
        Dim objCmd As SqlCommand = Nothing
        Dim _Query As String = ""
        Try
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                _Query = "Delete Im_Manufacturers Where sManufacturer = '" & categoryDescription & "' and sCodeType = 'Manufacturer' and sManufacturerCode = '" & Code & "'"
                objCmd = New SqlCommand(_Query, Conn)
                If Not IsNothing(objCmd) Then
                    objCmd.ExecuteNonQuery()
                End If
            End If
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try



    End Sub
    Public Sub UpDateData(ByVal Code As String, ByVal categoryDescription As String, ByVal PreviousCode As String, ByVal PreviousCodeDescription As String)
        Dim objCmd As SqlCommand = Nothing
        Dim _Query As String = ""
        Dim _isReturnObject As Boolean = False
        Try

            If Not IsNothing(Conn) Then
                _isReturnObject = CheckCount(PreviousCode, PreviousCodeDescription)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                If _isReturnObject = True Then
                    _Query = "Update  Im_Manufacturers Set sManufacturerCode = '" & Code & "' ,sManufacturer = '" & categoryDescription & "' Where sManufacturer = '" & PreviousCodeDescription & "' and sCodeType = 'Manufacturer' and sManufacturerCode = '" & PreviousCode & "'"
                    objCmd = New SqlCommand(_Query, Conn)
                    If Not IsNothing(objCmd) Then
                        objCmd.ExecuteNonQuery()
                    End If
                Else
                    _Query = "INSERT INTO Im_Manufacturers(sCodeType,sManufacturerCode,sManufacturer) VALUES('Manufacturer','" & Code & "','" & categoryDescription & "')"
                    objCmd = New SqlCommand(_Query, Conn)
                    If Not IsNothing(objCmd) Then
                        objCmd.ExecuteNonQuery()
                    End If
                End If
            End If
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try



    End Sub
    Public Function CheckCount(ByVal PreviousCode As String, ByVal PreviousCodeDescription As String) As Boolean
        Dim objCmd As SqlCommand = Nothing
        Dim oResult As Object
        Dim _Query As String = ""
        Dim _isReturn As Boolean = False
        Try
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                _Query = "SELECT COUNT(*) FROM Im_Manufacturers WHERE sManufacturer = '" & PreviousCodeDescription & "' and sCodeType = 'Manufacturer' and sManufacturerCode = '" & PreviousCode & "'"
                objCmd = New SqlCommand(_Query, Conn)
                If Not IsNothing(objCmd) Then
                    oResult = objCmd.ExecuteScalar()
                    If CType(oResult, Int16) > 0 Then
                        _isReturn = True
                    Else
                        _isReturn = False
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return _isReturn
    End Function
    ''End Dhruv--
#End Region
    '' end

    Public Sub AddData(ByVal Code As String, ByVal categoryDescription As String, ByVal categoryType As String, Optional ByVal HistoryType As String = "", Optional ByVal isFavorite As Boolean = False, Optional ByVal ParentCategoryID As Long = 0, Optional ByVal ParentCode As String = "")
        Dim CatCommand As SqlCommand = Nothing
        Try

            CatCommand = New System.Data.SqlClient.SqlCommand("InsertCategory", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = CatCommand.Parameters.Add("@sCode", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Code

            objParam = CatCommand.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryDescription

            objParam = CatCommand.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryType
            ''nClinicID,bIsBlocked added by Sandip Darade  
            'nClinicID =@nClinicID,
            'bIsBlocked =@bIsBlocked
            objParam = CatCommand.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClinicID

            objParam = CatCommand.Parameters.Add("@bIsBlocked", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = False


            objParam = CatCommand.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryType

            objParam = CatCommand.Parameters.Add("@bIsFavorite", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = isFavorite

            objParam = CatCommand.Parameters.Add("@nParentCategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ParentCategoryID

            objParam = CatCommand.Parameters.Add("@sParentCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ParentCode

            Conn.Open()
            CatCommand.ExecuteNonQuery()
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Category '" & Str2 & "' Added", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing

            '' SUDHIR 20090511 '' ADD DATADICTIONARY ''
            If categoryType = "History" Then
                Dim oDictionary As New clsDataDictionary
                oDictionary.AddDataDictionary("History.sHistoryItem+History.sComments|" & categoryDescription, "History", categoryDescription, "History")
                oDictionary = Nothing
            ElseIf categoryType = "ROS" Then
                Dim oDictionary As New clsDataDictionary
                oDictionary.AddDataDictionary("ROS.sROSItem+ROS.sComments|" & categoryDescription, "ROS", categoryDescription, "Review of Systems")
                oDictionary = Nothing
            End If
            '' END DATADICTIONARY ''

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Category Added", gstrLoginName, gstrClientMachineName)
            objParam = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "ClsDBLayer -- AddData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- AddData -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "ClsDBLayer -- AddData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- AddData -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
        End Try
    End Sub

    Public Sub UpdateData(ByVal Code As String, ByVal categoryDescription As String, ByVal categoryType As String, ByVal id As Int64, Optional ByVal HistoryType As String = "", Optional ByVal isFavorite As Boolean = False, Optional ByVal ParentCategoryID As Long = 0, Optional ByVal ParentCode As String = "")

        Dim cmd As SqlCommand = Nothing
        Dim CatCommand As SqlCommand = Nothing
        Try

            '' SUDHIR 20090511 '' HISTORY DATADICTIONARY ''
            '' WHILE MODIFY. IF CURRENT CATEGORY IS NOT IN HISTORY TRANSACTION. THEN DELETE IT.
            cmd = New SqlCommand("SELECT sCode,sDescription, sCategoryType FROM Category_MST WHERE nCategoryID = " & id & "", Conn)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtCategory As New DataTable
            Dim oDictionary As New clsDataDictionary
            adp.Fill(dtCategory)

            If IsNothing(dtCategory) = False Then
                If dtCategory.Rows.Count > 0 Then
                    If dtCategory.Rows(0)("sCategoryType") = "History" Then
                        Dim strDictionary As String = "History.sHistoryItem+History.sComments|" & dtCategory.Rows(0)("sDescription")
                        If IsCategoryUsedInHistory(dtCategory.Rows(0)("sDescription")) = False Then
                            oDictionary.DeleteDataDictionary(strDictionary)
                        End If
                    ElseIf dtCategory.Rows(0)("sCategoryType") = "ROS" Then
                        Dim strDictionary As String = "ROS.sROSItem+ROS.sComments|" & dtCategory.Rows(0)("sDescription")
                        If IsCategoryUsedInROS(dtCategory.Rows(0)("sDescription")) = False Then
                            oDictionary.DeleteDataDictionary(strDictionary)
                        End If
                    End If
                End If
                dtCategory.Dispose()
                dtCategory = Nothing
            End If
            adp.Dispose()
            adp = Nothing
            ''END SUDHIR ''


            CatCommand = New System.Data.SqlClient.SqlCommand("UpdateCategory", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = CatCommand.Parameters.Add("@sCode", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Code

            objParam = CatCommand.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryDescription

            objParam = CatCommand.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryType

            objParam = CatCommand.Parameters.Add("@id", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            ''nClinicID,bIsBlocked added by Sandip Darade  
            'nClinicID =@nClinicID,
            'bIsBlocked =@bIsBlocked
            objParam = CatCommand.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClinicID

            objParam = CatCommand.Parameters.Add("@bIsBlocked", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = False

            objParam = CatCommand.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryType

            objParam = CatCommand.Parameters.Add("@bIsFavorite", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = isFavorite

            objParam = CatCommand.Parameters.Add("@nParentCategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ParentCategoryID

            objParam = CatCommand.Parameters.Add("@sParentCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ParentCode

            Conn.Open()
            CatCommand.ExecuteNonQuery()

            '' ADD DATADICTIONARY ''
            If categoryType = "History" Then
                oDictionary.AddDataDictionary("History.sHistoryItem+History.sComments|" & categoryDescription, "History", categoryDescription, "History")
            ElseIf categoryType = "ROS" Then
                oDictionary.AddDataDictionary("ROS.sROSItem+ROS.sComments|" & categoryDescription, "ROS", categoryDescription, "Review of Systems")
            End If

            oDictionary = Nothing
            objParam = Nothing
            '' '' 


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Modify, "Category Modified", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Modify, "Category Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Category Modified", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Modify, "ClsDBLayer -- UpdateData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- UpdateData -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Modify, "ClsDBLayer -- UpdateData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- UpdateData -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
        End Try

    End Sub

    Public Sub DeleteData(ByVal id As Long)
        Dim cmd As SqlCommand = Nothing
        Dim CatCommand As SqlCommand = Nothing

        Try
            '' SUDHIR 20090511 '' HISTORY DATADICTIONARY ''
            cmd = New SqlCommand("SELECT sDescription, sCategoryType FROM Category_MST WHERE nCategoryID = " & id & "", Conn)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtCategory As New DataTable
            adp.Fill(dtCategory)
            If IsNothing(dtCategory) = False Then
                If dtCategory.Rows.Count > 0 Then
                    If dtCategory.Rows(0)("sCategoryType") = "History" Then
                        Dim strDictionary As String = "History.sHistoryItem+History.sComments|" & dtCategory.Rows(0)("sDescription")
                        If IsCategoryUsedInHistory(dtCategory.Rows(0)("sDescription")) = False Then
                            Dim oDictionary As New clsDataDictionary
                            oDictionary.DeleteDataDictionary(strDictionary)
                            oDictionary = Nothing
                        End If
                    ElseIf dtCategory.Rows(0)("sCategoryType") = "ROS" Then
                        Dim strDictionary As String = "ROS.sROSItem+ROS.sComments|" & dtCategory.Rows(0)("sDescription")
                        If IsCategoryUsedInROS(dtCategory.Rows(0)("sDescription")) = False Then
                            Dim oDictionary As New clsDataDictionary
                            oDictionary.DeleteDataDictionary(strDictionary)
                            oDictionary = Nothing
                        End If
                    End If
                End If
                dtCategory.Dispose()
                dtCategory = Nothing
            End If
            adp.Dispose()
            adp = Nothing
            ''END SUDHIR ''

            CatCommand = New System.Data.SqlClient.SqlCommand("DeleteCategory", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = CatCommand.Parameters.Add("@id", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id
            Conn.Open()
            CatCommand.ExecuteNonQuery()
            objParam = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, "Category Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, "Category Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Category Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, "ClsDBLayer -- DeleteData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- DeleteData -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, "ClsDBLayer -- DeleteData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- DeleteData -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
        End Try
    End Sub

    Public Function IsCategoryUsedInHistory(ByVal categoryName As String) As Boolean
        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand("SELECT COUNT(*) sHistoryCategory FROM History WHERE sHistoryCategory = '" & categoryName.Replace("'", "''") & "'", con)
            Dim oResult As Object
            con.Open()
            oResult = cmd.ExecuteScalar
            con.Close()
            con.Dispose()
            con = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If CType(oResult, Int16) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function


    Public Function IsCPTCategoryInUse(CategoryId As Long, CategoryDescription As String) As [Boolean]
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _isCPTCategoryInUse As [Boolean] = False
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nCategoryID", CategoryId, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@sCategoryDesc", CategoryDescription, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Retrive("Check_For_IsCPTCategoryInUse", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                If Convert.ToInt64(_dt.Rows(0)("CPTCategoryCount")) > 0 Then
                    _isCPTCategoryInUse = True
                End If

            End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If

        End Try

        Return _isCPTCategoryInUse
    End Function

    Public Function UpdateCPTCategoryInUse(CategoryId As Long) As Int64
        ', string CategoryDescription
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _intresult As Object = 0
        Dim _result As Int64 = 0
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nCategoryID", CategoryId, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@nRowupdated", _result, ParameterDirection.Output, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Execute("UpdtInUseCPTMstCatWithCatMst", oParameters, _intresult)

            If _intresult IsNot Nothing Then
                If _intresult.ToString().Trim() <> "" Then
                    If Convert.ToInt64(_intresult) > 0 Then
                        _result = Convert.ToInt64(_intresult.ToString())
                    End If
                End If

            End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return _result
    End Function


    Public Function IsCategoryUsedInROS(ByVal categoryName As String) As Boolean
        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand("SELECT COUNT(*) sROSCategory FROM ROS WHERE sROSCategory = '" & categoryName.Replace("'", "''") & "'", con)
            Dim oResult As Object
            con.Open()
            oResult = cmd.ExecuteScalar
            con.Close()
            con.Dispose()
            con = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If CType(oResult, Int16) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    Public Function FetchDataForUpdate(ByVal id As Int64) As ArrayList
        Dim arrlist As New ArrayList
        Dim CatCommand As SqlCommand = Nothing
        Try
            'Dim sqlcmd As String
            'sqlcmd = "select 'Description'=Sdescription,'Category'=scategorytype from category_mst where nCategoryID =" & id & ""
            CatCommand = New System.Data.SqlClient.SqlCommand("gsp_scanCategory_Mst", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = CatCommand.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            ''nClinicID,bIsBlocked added by Sandip Darade  
            'nClinicID =@nClinicID,
            'bIsBlocked =@bIsBlocked
            objParam = CatCommand.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClinicID

            objParam = CatCommand.Parameters.Add("@bIsBlocked", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = False

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = CatCommand.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(dreader("nCategoryID"))
                arrlist.Add(dreader("sDescription"))
                arrlist.Add(dreader("sCategoryType"))
                arrlist.Add(dreader("sCode"))
                arrlist.Add(dreader("HistoryType"))
                arrlist.Add(dreader("Favorites"))
                arrlist.Add(dreader("IsSystemDefined"))
                arrlist.Add(dreader("nParentCategoryID"))
            Loop
            dreader.Close()
            dreader = Nothing
            Conn.Close()
            objParam = Nothing
            Return arrlist
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- FetchDataForUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return arrlist
            'UpdateLog("ClsDBLayer -- FetchDataForUpdate -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- FetchDataForUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- FetchDataForUpdate -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return arrlist
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
        End Try
        'Return CatDataset
        'Return CatDataset
    End Function

    Public ReadOnly Property DsDataSet() As DataSet
        Get
            'DCatview = CatDataset.Tables("Category_Mst").DefaultView
            Return CatDataset
            'Return CatDataset
        End Get

    End Property
    Public ReadOnly Property DsDataview() As DataView
        Get
            'DCatview = CatDataset.Tables("Category_Mst").DefaultView
            Return DCatview
            'Return CatDataset
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        DCatview.Sort = "[" & strsort & "]" & strSortOrder
    End Sub
    Public Function ValidateDescription(ByVal str1 As String, ByVal str2 As String, ByVal id As Int64, ByVal sCode As String) As Boolean
        Dim objParam As SqlParameter = Nothing
        Dim CatCommand As SqlCommand = Nothing
        Try
            CatCommand = New System.Data.SqlClient.SqlCommand("gsp_checkcategory_Mst", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure

            objParam = CatCommand.Parameters.Add("@scategoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = CatCommand.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = CatCommand.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = CatCommand.Parameters.Add("@sCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sCode

            Dim dreader As SqlDataReader

            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = CatCommand.ExecuteReader

            Dim i As Int64
            Do While dreader.Read
                i = CType(dreader.Item(0), System.Int64)
                If i > 0 Then
                    dreader.Close()

                    Conn.Close()
                    Return False
                Else
                    dreader.Close()

                    Conn.Close()
                    Return True
                End If
            Loop
            dreader.Close()
            dreader = Nothing
            Return False
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- ValidateDescription -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- ValidateDescription -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- ValidateDescription -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- ValidateDescription -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If

            objParam = Nothing
        End Try

    End Function
    Public Function ValidateCode(ByVal str1 As String, ByVal str2 As String, ByVal id As Int64, ByVal sCode As String, Optional ByVal _Type As String = "") As Boolean
        Dim objParam As SqlParameter
        Dim CatCommand As SqlCommand = Nothing
        Try
            CatCommand = New System.Data.SqlClient.SqlCommand("gsp_checkCategoryImmunization_Mst", Conn)
            CatCommand.CommandType = CommandType.StoredProcedure

            objParam = CatCommand.Parameters.Add("@scategoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = CatCommand.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = CatCommand.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = CatCommand.Parameters.Add("@sCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sCode

            objParam = CatCommand.Parameters.Add("@Type", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _Type

            Dim dreader As SqlDataReader

            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = CatCommand.ExecuteReader

            Dim i As Int64
            Do While dreader.Read
                i = CType(dreader.Item(0), System.Int64)
                If i > 0 Then
                    dreader.Close()
                    Conn.Close()
                    Return False
                Else
                    dreader.Close()
                    Conn.Close()
                    Return True
                End If
            Loop
            dreader.Close()
            dreader = Nothing
            Return False
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- ValidateDescription -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDBLayer -- ValidateDescription -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Query, "ClsDBLayer -- ValidateDescription -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsDBLayer -- ValidateDescription -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If CatCommand IsNot Nothing Then
                CatCommand.Parameters.Clear()
                CatCommand.Dispose()
                CatCommand = Nothing
            End If
            objParam = Nothing
        End Try

    End Function
    Public Function ValidateCodewithDescription(ByVal Code As String, ByVal Description As String)
        Return Nothing
    End Function
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
            Return strsplittext
        End Try
    End Function


    Public Function GetStandardTypes() As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            Cmd = New SqlCommand("History_GetStandardTypes", Conn)
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@FlgHistoryMst", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd


            adpt.Fill(ds)
            Conn.Close()
            objParam = Nothing
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(adpt) = False Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function



    Public Function GetCategoryTypes() As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            Cmd = New SqlCommand("gsp_GetCategoryType", Conn)
            '  Dim objParam As SqlParameter
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd


            adpt.Fill(ds)
            Conn.Close()
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(adpt) = False Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

    Public Function GetRaceEthnicityParentList(ByVal sCategoryDesc As String) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            Cmd = New SqlCommand("gsp_GetRaceList", Conn)
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@sCategoryDesc", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sCategoryDesc
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd


            adpt.Fill(ds)
            Conn.Close()
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(adpt) = False Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

    Public Function IsPublicityCodeUsedonIM(ByVal PreviousCode As String) As Boolean

        Dim con As New SqlClient.SqlConnection(GetConnectionString)
        Dim cmd As New SqlClient.SqlCommand()
        Dim oResult As Object
        Dim _queryString As String = ""

        Try
            cmd.Connection = con

            _queryString = "select COUNT(*) from IM_Trn_Dtl where UPPER(ISNULL(sPublicityCode,'')) = '" + PreviousCode.ToUpper() + "' "

            cmd.CommandText = _queryString

            con.Open()
            oResult = cmd.ExecuteScalar
            con.Close()
            If CType(oResult, Int32) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return True
        Finally
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            con.Close()
            con.Dispose()
            con = Nothing

            oResult = Nothing

        End Try
    End Function

    Public Function GetPortalAccessID(ByVal PatientID As Long) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtPortalData As DataTable = Nothing
        Dim nPatientPortalAccessID As Int64 = 0
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("gsp_GetPortalAccessID", oParameters, dtPortalData)
            If dtPortalData IsNot Nothing AndAlso dtPortalData.Rows.Count > 0 Then
                nPatientPortalAccessID = Convert.ToInt64(dtPortalData.Rows(0)("nPatientPortalAccessID"))
            End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If dtPortalData IsNot Nothing Then
                dtPortalData.Dispose()
                dtPortalData = Nothing
            End If

        End Try

        Return nPatientPortalAccessID
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                'If CatCommand IsNot Nothing Then
                '    CatCommand.Parameters.Clear()
                '    CatCommand.Dispose()
                '    CatCommand = Nothing
                'End If


                ''slr free dv
                If Not IsNothing(DCatview) Then
                    DCatview.Dispose()
                    DCatview = Nothing
                End If
                If Not IsNothing(CatDataset) Then
                    CatDataset.Dispose()
                    CatDataset = Nothing
                End If

                'slr free Con
                If Not IsNothing(Conn) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If


            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

