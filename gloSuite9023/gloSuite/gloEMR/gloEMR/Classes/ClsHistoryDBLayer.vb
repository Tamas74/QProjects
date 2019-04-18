Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHistoryDBLayer
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Private Conn As SqlConnection
    ' Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private Ds As System.Data.DataSet
    Private Dv As DataView
    ' Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Public Sub FetchData(ByVal CategoryId As Long)
        Try
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Dim Cmd As SqlCommand = New SqlCommand("gsp_ViewHistory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryId

            Adapter.SelectCommand = Cmd
            If (IsNothing(Ds) = False) Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            'Tb = Ds.Tables(0)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(Ds.Tables(0))
            Adapter.Dispose()
            Adapter = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            objParam = Nothing
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Conn.Close()
        End Try
        'Return Ds
        'Return Ds
    End Sub
    ''nICDRevision parameter added for ICD10
    '' LoincCode and loincdescr added 
    Public Function AddData(ByVal str1 As String, ByVal str2 As String, ByVal CategoryID As Long, ByVal ConceptID As String, ByVal DescriptionID As String, ByVal SnomedID As String, ByVal SnomedDescription As String, ByVal sICD9 As String, ByVal sNdcCode As String, ByVal sRxNormCode As String, ByVal sSnoDefination As String, ByVal sSmokingStatus As String, ByVal CPT As String, ByVal HistoryType As String, Optional ByVal nICDRevision As Integer = 9, Optional ByVal dtHistdetail As DataTable = Nothing, Optional ByVal LoincCode As String = "", Optional ByVal LoincDescr As String = "", Optional ByVal refusalcode As String = "", Optional ByVal refusalcodeDescr As String = "", Optional ByVal CVXCode As String = "", Optional ByVal CVXDesc As String = "") As Long
        Dim objParam As SqlParameter
        Try

            'Dim i As Int16
            'For i = 1 To 100
            'str1 = "Social History " & i
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpHistory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            ''objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.bigInt)
            ''objParam.Direction = ParameterDirection.Input
            ''objParam.Value = 0

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID

            objParam = Cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ConceptID

            objParam = Cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = DescriptionID

            objParam = Cmd.Parameters.Add("@sSnomedID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SnomedID

            objParam = Cmd.Parameters.Add("@sSnomedDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SnomedDescription

            objParam = Cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sICD9

            objParam = Cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sNdcCode

            objParam = Cmd.Parameters.Add("@sRxNormCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sRxNormCode


            objParam = Cmd.Parameters.Add("@sSnomedDefination", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sSnoDefination

            objParam = Cmd.Parameters.Add("@sSmokingStatus", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sSmokingStatus


            objParam = Cmd.Parameters.Add("@CPT", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPT

            objParam = Cmd.Parameters.Add("@HistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryType

            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.InputOutput
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            ''nICDRevision parameter added for ICD10
            objParam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nICDRevision

            'objParam = Cmd.Parameters.Add("@CPT", SqlDbType.VarChar)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CPT
            objParam = Cmd.Parameters.Add("@nOBHistoryFlag", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            If (Not IsNothing(dtHistdetail)) Then
                objParam.Value = 1
            Else
                objParam.Value = 0
            End If
            objParam = Cmd.Parameters.Add("@dtHistdtl", SqlDbType.Structured)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = dtHistdetail

            objParam = Cmd.Parameters.Add("@sLoincCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoincCode

            objParam = Cmd.Parameters.Add("@sLoincDescr", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoincDescr



            objParam = Cmd.Parameters.Add("@refusalcode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = refusalcode

            objParam = Cmd.Parameters.Add("@refusalcodeDescr", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = refusalcodeDescr

            'objParam = Cmd.Parameters.Add("@sCVXCode", SqlDbType.VarChar)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CVXCode
            'objParam = Cmd.Parameters.Add("@sCVXDesc", SqlDbType.VarChar)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CVXDesc



            Conn.Open()
            Cmd.ExecuteNonQuery()
            Dim myObject = Cmd.Parameters("@nHistoryId").Value
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, " History - " + str1 + "  Added", gloAuditTrail.ActivityOutCome.Success)
            Return myObject

            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.enmActivityType.Add, " History Added", gstrLoginName, gstrClientMachineName)
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, " History Added", gloAuditTrail.ActivityOutCome.Success)

            Conn.Close()
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
            objParam = Nothing
        End Try
        'Next
    End Function
    Public Sub UpdateData(ByVal str1 As String, ByVal str2 As String, ByVal HistoryId As Long, ByVal ConceptID As String, ByVal DescriptionID As String, ByVal SnomedID As String, ByVal SnomedDescription As String, ByVal CategoryID As Int64, ByVal sICD9 As String, ByVal sNdcCode As String, ByVal sRxNormCode As String, ByVal sSnoDefination As String, ByVal sSmokingStatus As String, ByVal CPT As String, ByVal HistoryType As String, Optional ByVal nICDRevision As Integer = 9, Optional ByVal dtHistdetail As DataTable = Nothing, Optional ByVal LoincCode As String = "", Optional ByVal LoincDescr As String = "", Optional ByVal refusalcode As String = "", Optional ByVal refusalcodeDescr As String = "")
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpHistory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            ''objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.bigInt)
            ''objParam.Direction = ParameterDirection.Input
            ''objParam.Value = HistoryId

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryId

            objParam = Cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ConceptID

            objParam = Cmd.Parameters.Add("@ncategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID

            objParam = Cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = DescriptionID

            objParam = Cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sICD9

            objParam = Cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sNdcCode

            objParam = Cmd.Parameters.Add("@sRxNormCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sRxNormCode

            objParam = Cmd.Parameters.Add("@sSnomedDefination", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sSnoDefination

            objParam = Cmd.Parameters.Add("@sSnomedID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SnomedID

            objParam = Cmd.Parameters.Add("@sSnomedDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SnomedDescription

            objParam = Cmd.Parameters.Add("@sSmokingStatus", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sSmokingStatus

            objParam = Cmd.Parameters.Add("@CPT", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPT

            objParam = Cmd.Parameters.Add("@HistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryType

            ''nICDRevision parameter added for ICD10
            objParam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nICDRevision

            objParam = Cmd.Parameters.Add("@nOBHistoryFlag", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            If (Not IsNothing(dtHistdetail)) Then
                objParam.Value = 1
            Else
                objParam.Value = 0
            End If
            objParam = Cmd.Parameters.Add("@dtHistdtl", SqlDbType.Structured)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = dtHistdetail

            objParam = Cmd.Parameters.Add("@sLoincCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoincCode

            objParam = Cmd.Parameters.Add("@sLoincDescr", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoincDescr

            objParam = Cmd.Parameters.Add("@refusalcode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = refusalcode

            objParam = Cmd.Parameters.Add("@refusalcodeDescr", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = refusalcodeDescr


            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "History Modified", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "History - " + str1 + "  Modified", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            Conn.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objParam = Nothing
        End Try
    End Sub
    Public Sub DeleteData(ByVal Historyid As Long, ByVal sDecription As String)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeleteHistory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Historyid
            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()

            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "History - " + sDecription + "  Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            Conn.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "History Delete " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function FetchDataForUpdate(ByVal id As Long, ByVal _CategoryID As Long) As ArrayList
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanHistory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _CategoryID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()

                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))
                arrlist.Add(dreader.Item(2))
                arrlist.Add(dreader.Item(3))
                arrlist.Add(dreader.Item(4))
                arrlist.Add(dreader.Item(5))
                arrlist.Add(dreader.Item(6))
                arrlist.Add(dreader.Item(7))
                arrlist.Add(dreader.Item(8))
                arrlist.Add(dreader.Item(9))
                arrlist.Add(dreader.Item(10))
                arrlist.Add(dreader.Item(11))
                arrlist.Add(dreader.Item(12))
                arrlist.Add(dreader.Item("LoincCode"))
                arrlist.Add(dreader.Item("sLoincDescr"))
                arrlist.Add(dreader.Item("srefusalcode"))
                arrlist.Add(dreader.Item("srefusalDescr"))
                'arrlist.Add(dreader.Item("CVXCode"))
                'arrlist.Add(dreader.Item("CVXDEsc"))
            Loop
            dreader.Close()
            dreader = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Conn.Close()
            objParam = Nothing
            Return arrlist

        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

        'Return Ds
        'Return Ds
    End Function
    Public ReadOnly Property DsDataSet() As DataSet
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Ds
            'Return Ds
        End Get
    End Property
    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'Dv.Sort = strsort
        Dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub

    Public Function FillControls() As DataTable
        Dim ds As DataSet = Nothing
        Try
            Dim adpt As New SqlDataAdapter


            Dim Cmd As SqlCommand = New SqlCommand("gsp_FillCategory_Mst", Conn)
            Dim objParam As SqlParameter

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "History"

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1
            ds = New DataSet
            adpt.Fill(Ds)
            adpt.Dispose()
            adpt = Nothing

            Conn.Close()
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return Ds.Tables(0).Copy()
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
            If (IsNothing(ds) = False) Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try

        'Dim dreader As SqlDataReader
        'Conn.Open()
        'dreader = Cmd.ExecuteReader()

        'Do While dreader.Read
        '    Dim i As Integer
        '    i = dreader("nSpecialtyID")

        'Loop
    End Function
    Public Function ValidateDescription(ByVal categoryid As System.Int64, ByVal str1 As String, ByVal id As Int64, ByVal ConceptId As String) As Boolean
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_checkHistory_Mst", Conn)
        Try

            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ncategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryid

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ConceptId

            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dreader As SqlDataReader
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = Cmd.ExecuteReader

            Dim i As Int64
            Do While dreader.Read
                i = CType(dreader.Item(0), System.Int64)
                If i > 0 Then
                    Conn.Close()
                    dreader.Close()
                    Return False
                Else
                    Conn.Close()
                    dreader.Close()
                    Return True
                End If
            Loop

            objParam = Nothing
            Return Nothing
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
            Cmd.Parameters.Clear()
            Cmd.Dispose()

        End Try

    End Function
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = Dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
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
            objParam.Value = 0

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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                If (IsNothing(Ds) = False) Then
                    Ds.Dispose()
                    Ds = Nothing
                End If

                If (IsNothing(Dv) = False) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                If (IsNothing(Conn) = False) Then
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



