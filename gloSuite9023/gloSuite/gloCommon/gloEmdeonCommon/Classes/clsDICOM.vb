Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class clsDICOM

#Region "variable declarations"
    Public Enum DICOMDescType
        Acknowledge = 1
        Review = 2
        Notes = 3
    End Enum
#End Region

    'Public Function GetDICOMDetails(ByVal nPatientID As Long) As DataTable
    '    Dim dtDICOM As New DataTable
    '    Dim conn As New SqlConnection
    '    Dim cmd As New SqlCommand
    '    Dim strSQL As String = ""
    '    Dim da As SqlDataAdapter

    '    Try
    '        conn.ConnectionString = GetConnectionString()

    '        '  strSQL = "SELECT DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription FROM DICOMdetails where nPatientID = " & nPatientID & ""
    '        'if this form opened from view->dicom then
    '        strSQL = "SELECT  DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, dtImportDateTime FROM DICOMdetails where nPatientID = " & nPatientID & ""

    '        'else if this form opened from lm_orders form then show dicom files of tht patient specific to test
    '        strSQL = "SELECT  DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, dtImportDateTime FROM DICOMdetails where nPatientID = " & nPatientID & " " _
    '        & " and DICOMID in (  SELECT ISNULL(lm_DICOMID,'') AS DICOMIDs  FROM dbo.LM_Orders WHERE lm_Patient_ID = "&nPatientID"   AND lm_test_ID = "& &")"
    '        ''
    '        'and  DicomID in ''

    '        cmd.Connection = conn
    '        cmd.CommandText = strSQL
    '        cmd.CommandType = CommandType.Text
    '        da = New SqlDataAdapter(cmd)

    '        da.Fill(dtDICOM)

    '        da = Nothing
    '        cmd = Nothing
    '    Catch sqlex As SqlException
    '        MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Not IsNothing(conn) Then
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '            End If

    '            conn.Dispose()
    '            conn = Nothing
    '        End If
    '    End Try

    '    Return dtDICOM
    'End Function
    ''Sandip Darade 20090316 
    Public Function GetDICOMDetails(ByVal nPatientID As Long, Optional ByVal TestID As Int64 = Nothing, Optional ByVal OrderID As Int64 = Nothing, Optional ByVal IsTest As Boolean = False, Optional ByVal sDicomIDs As String = Nothing) As DataTable
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim da As SqlDataAdapter

        Try
            conn.ConnectionString = GetConnectionString()

            '  strSQL = "SELECT DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription FROM DICOMdetails where nPatientID = " & nPatientID & ""
            'if this form opened from view->dicom then
            strSQL = "SELECT  DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, dtImportDateTime FROM DICOMdetails where nPatientID = " & nPatientID & ""
            '' to get DICOM files for a  test 
            If (IsTest = True) Then
                ''Get dicomid files for test 
                'strSQL = "SELECT ISNULL(lm_DICOMID,'') AS DICOMIDs  FROM LM_Orders WHERE lm_Patient_ID = " & nPatientID & "   AND lm_test_ID = " & TestID & " AND lm_Order_ID = " & OrderID & " "
                'cmd.Connection = conn
                'cmd.CommandText = strSQL
                'cmd.CommandType = CommandType.Text
                'da = New SqlDataAdapter(cmd)
                'da.Fill(dtDICOM)
                'da = Nothing
                'cmd = Nothing
                'If dtDICOM.Rows.Count > 0 Then
                '    ' if this form opened from lm_orders form then show dicom files of tht patient specific to test
                '    strSQL = "SELECT  DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, dtImportDateTime FROM DICOMdetails where nPatientID = " & nPatientID & " " _
                '    & " and DICOMID in (" & Convert.ToString(dtDICOM.Rows(0)(0)) & " )"
                '    ''
                '    'and  DicomID in ''
                'End If
                strSQL = "SELECT  DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, dtImportDateTime FROM DICOMdetails where nPatientID = " & nPatientID & " " _
                  & " and DICOMID in (" & sDicomIDs & " )"
            End If
            'cmd = New SqlCommand
            'dtDICOM = New DataTable
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)

            da.Fill(dtDICOM)

            da = Nothing
            cmd = Nothing
        Catch sqlex As SqlException
            MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return dtDICOM
    End Function

    Public Function GetDICOMFileName(ByVal DICOMID As Long) As String
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim strFileName As String = ""

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT  isnull(DICOMFileName,'') as DICOMFileName  FROM DICOMdetails where DICOMID = " & DICOMID & ""
            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            strFileName = cmd.ExecuteScalar()

            cmd = Nothing
        Catch sqlex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, sqlex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return strFileName
    End Function

    Public Function GetUsers() As ArrayList

        Dim _Results As System.Collections.ArrayList = New ArrayList()
        Dim conn As New SqlConnection
        Dim da As New SqlDataAdapter
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""

        Dim oDataTable As New DataTable
        Try
            conn.ConnectionString = GetConnectionString()
            _strSQL = "SELECT nUserID,sLoginName ,ISNULL(sFirstName,'')+Space(1)+ ISNULL(sMiddleName,'') +Space(1) + ISNULL(sLastName,'') AS UserName FROM User_MST"

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)

            da.Fill(oDataTable)

            If oDataTable IsNot Nothing Then
                If oDataTable.Rows.Count > 0 Then
                    For i As Integer = 0 To oDataTable.Rows.Count - 1
                        _Results.Add(oDataTable.Rows(i)("sLoginName").ToString())
                    Next
                End If
            End If


        Catch ex As Exception
            Throw ex
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try



        Return _Results
    End Function

    Public Function InsertDicomFile(ByVal MachineID As Long, ByVal DicomID As Long, ByVal nPatientID As Long, ByVal DICOMFileName As String, ByVal DICOMFileExtn As String, ByVal DICOMDocName As String, ByVal nUserID As Long, ByRef RetDICOMID As Long) As Boolean

        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim _Result As Boolean = True

        Dim objParameter As SqlParameter = Nothing

        Try
            _strSQL = "dbo.DCM_INUPDicomDetails" '"insert into DicomDetails(DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription) values()"

            conn.ConnectionString = GetConnectionString()
            cmd.Connection = conn

            cmd.CommandText = _strSQL
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = conn

            objParameter = New SqlParameter
            objParameter.ParameterName = "@MachineID"
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.SqlValue = MachineID
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing


            objParameter = New SqlParameter
            objParameter.ParameterName = "@DicomID"
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.Direction = ParameterDirection.InputOutput
            objParameter.SqlValue = DicomID
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@nPatientID"
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlValue = nPatientID
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing


            objParameter = New SqlParameter
            objParameter.ParameterName = "@DICOMFileName"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlValue = DICOMFileName
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@DICOMFileExtn"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlValue = DICOMFileExtn
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing


            objParameter = New SqlParameter
            objParameter.ParameterName = "@DICOMDocName"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlValue = DICOMDocName
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing


            objParameter = New SqlParameter
            objParameter.ParameterName = "@nUserID"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlValue = nUserID
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            conn.Open()
            cmd.ExecuteNonQuery()
            RetDICOMID = cmd.Parameters("@DicomID").Value

            conn.Close()

        Catch ex As Exception
            _Result = False
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex

        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objParameter) Then
                objParameter = Nothing
            End If

        End Try

        Return _Result
    End Function

    Public Function FillAcknowledgements(ByVal strSQL As String) As String
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        '  Dim strSQL As String = ""
        Dim strAcknowledgement As String = ""

        Try

            '   strSQL = "select isnull(AckRvwDescription) as AckRvwDescription from DicomDetails where IsAckReview = 1 and DICOMID=" & DICOMID & ""
            conn.ConnectionString = GetConnectionString()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            conn.Open()

            strAcknowledgement = cmd.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return strAcknowledgement
    End Function

    Public Sub UpdateNotes(ByVal DicomId As Int64, ByVal NotesDescription As String, ByVal flag As Integer)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim _result As Boolean = False
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            If flag = 0 Then
                'ack
                objCmd.CommandText = "UPDATE DicomDetails SET AckRvwDescription='" & NotesDescription & "', IsAckReview = 0 WHERE DicomID = " & DicomId & "  "

            Else
                'review
                objCmd.CommandText = "UPDATE DicomDetails SET AckRvwDescription='" & NotesDescription & "',IsAckReview = 1 WHERE DicomID = " & DicomId & "  "

            End If
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                _result = True
            End If
        Catch sqlex As SqlException
            Throw sqlex
        Catch ex As Exception
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        '  Return _result
    End Sub

    Public Function InsertUpdateDICOMAckRvwNotes(ByVal MachineID As Long, ByVal DICOMDetID As Long, ByVal DICOMID As Long, ByVal nType As DICOMDescType, ByVal Description As String, ByVal sUserName As String, ByRef RetDICOMDetID As Long)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objParameter As SqlParameter = Nothing
        Dim _result As Boolean = False

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "DCM_INUPDicomAckRvwNotes"

            objParameter = New SqlParameter
            objParameter.ParameterName = "@MachineID"
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = MachineID
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@DICOMDetID"
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.Direction = ParameterDirection.InputOutput
            objParameter.Value = DICOMDetID
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@DICOMID"
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = DICOMID
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@nType"
            objParameter.SqlDbType = SqlDbType.Int
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = nType
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing


            objParameter = New SqlParameter
            objParameter.ParameterName = "@Description"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = Description
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@sUserName"
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = sUserName
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objCon.Open()
            objCmd.ExecuteNonQuery()

            RetDICOMDetID = objCmd.Parameters("@DICOMDetID").Value

            _result = True

        Catch sqlex As SqlException
            _result = False
            Throw sqlex
        Catch ex As Exception
            _result = False
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If Not IsNothing(objParameter) Then
                objParameter = Nothing
            End If


            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

        Return _result
    End Function

    ''Get notes for the patient
    Public Function GetDICOMDescription(ByVal strSelect As String) As String
        '  Dim strSelect As String = ""
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strNotes As String = ""

        Try
            conn.ConnectionString = GetConnectionString()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSelect

            conn.Open()
            strNotes = cmd.ExecuteScalar()

        Catch ex As Exception
            'MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return strNotes
    End Function

    Private Function UpdateNotes(ByVal DicomId As Int64, ByVal NotesDescription As String)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim _result As Boolean = False

        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "UPDATE DicomDetails SET NotesDescription='" & NotesDescription & "' WHERE DicomID = " & DicomId & "  "
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                _result = True
            End If
        Catch sqlex As SqlException
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            Throw sqlex
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetAckID(ByVal DICOMID As Long, ByVal flag As Integer) As Long
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim AckID As Long = 0

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT  DICOMDetID  FROM  DICOM_AckRvwNotes  where ntype= " & flag & " and DICOMID = " & DICOMID & ""
            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            AckID = cmd.ExecuteScalar()

            cmd = Nothing
        Catch sqlex As SqlException
            MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw sqlex
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return AckID
    End Function

    Public Function GetNotesID(ByVal DICOMID As Long) As Long
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim AckID As Long = 0

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT  DICOMDetID  FROM  DICOM_AckRvwNotes  where ntype=3 and DICOMID = " & DICOMID & ""
            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            AckID = cmd.ExecuteScalar()

            cmd = Nothing
        Catch sqlex As SqlException
            Throw sqlex
            MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return AckID
    End Function

    Public Function GetReviewID(ByVal DICOMID As Long) As Long
        Dim dtDICOM As New DataTable
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim AckID As Long = 0

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT  DICOMDetID  FROM  DICOM_AckRvwNotes  where ntype=2 and DICOMID = " & DICOMID & ""
            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            AckID = cmd.ExecuteScalar()

            cmd = Nothing
        Catch sqlex As SqlException
            Throw sqlex
            MessageBox.Show(sqlex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return AckID
    End Function

    Public Function GetLoginName(ByVal nuserid As Long) As String


        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim strLoginName As String = ""

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT isnull(sLoginName,'') as sLoginName  FROM  user_mst  where nuserid = " & nuserid & ""
            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            strLoginName = cmd.ExecuteScalar()

            cmd = Nothing
        Catch sqlex As SqlException
            Throw sqlex
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try


        Return strLoginName
    End Function

    Public Function getFileExtn(ByVal _DICOMID As Int64) As String
        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = ""
        Dim strLoginName As String = ""

        Try
            conn.ConnectionString = GetConnectionString()

            strSQL = "SELECT  DICOMFileExtn FROM DICOMdetails where  DicomID = " & _DICOMID

            'DicomID, nPatientID, DICOMFileName, DICOMFileExtn, IsAckReview, AckRvwDescription, NotesDescription
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            strLoginName = cmd.ExecuteScalar()

            cmd = Nothing
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If

                conn.Dispose()
                conn = Nothing
            End If
        End Try


        Return strLoginName
    End Function
    ''Sandip Darade 20090320
    ''Delete DICOM Files for selected patient
    Public Function DeleteDICOMFile(ByVal DicomId As Int64, ByVal nPatientId As Int64)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim _result As Boolean = False

        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE  DicomDetails WHERE DicomID = " & DicomId & " AND nPatientID = " & nPatientId & "   "
            ''AND dtImportDateTime =
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                _result = True
            End If
        Catch ex As SqlException
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            Throw ex
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return _result
    End Function

End Class
