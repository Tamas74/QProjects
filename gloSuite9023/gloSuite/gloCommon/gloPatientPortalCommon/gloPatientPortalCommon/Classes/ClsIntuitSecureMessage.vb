Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
'Imports gloSettings

Public Class ClsIntuitSecureMessage
    Implements System.IDisposable
    Dim dbConnectionstring As String = ""
    Dim _PatientID As Int64
    Dim _CommDetailID As Int64
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _machineID As Int64
    Dim _machineName As String = ""
    Dim gstrMessageBoxCaption As String = ""
    Dim _PatientAge As String
    Dim _loginUserName As String

#Region "IDisposable Support"
    Private disposed As Boolean ' To detect redundant calls 
    ' This code added by Visual Basic to correctly implement the disposable pattern.     
    Public Sub Dispose() Implements IDisposable.Dispose


        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.    
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)


        If Not Me.disposed Then
            If disposing Then

                'GC.Collect()
            End If
        End If

        Me.disposed = True
    End Sub
    Protected Overrides Sub Finalize()         ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.         Dispose(False)         MyBase.Finalize()     
        Dispose(False)
        MyBase.Finalize()

    End Sub
#End Region
    Public Function ReadMessageFunctionality(ByVal _FlagType As Integer) As DataTable
        Dim _sqlconn As SqlConnection = New SqlConnection(dbConnectionstring)
        Dim _sqlcmd As New SqlCommand
        Dim _sqlda As New SqlDataAdapter
        Dim dt As New DataTable
        Dim _sqlparam As SqlParameter
        Try

            _sqlcmd = New SqlCommand("Intuit_ReadSecureMessage", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)
            ''
            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = _PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            _sqlparam.Value = _CommDetailID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@FlagType", SqlDbType.Int)
            _sqlparam.Value = _FlagType
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing
            _sqlda.Fill(dt)

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Return Nothing
        Finally
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If IsNothing(_sqlda) = False Then
                _sqlda.Dispose()
                _sqlda = Nothing
            End If
            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If

            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose()
                _sqlconn = Nothing
            End If

        End Try

    End Function
    Public Function DeleteMessageFunctionality(ByVal _FlagType As Integer) As Boolean
        Dim _sqlconn As SqlConnection = New SqlConnection(dbConnectionstring)
        Dim _sqlcmd As New SqlCommand
        Dim _sqlda As New SqlDataAdapter

        Dim _sqlparam As SqlParameter
        Try

            _sqlcmd = New SqlCommand("Intuit_ReadSecureMessage", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)
            ''
            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = _PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            _sqlparam.Value = _CommDetailID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@FlagType", SqlDbType.Int)
            _sqlparam.Value = _FlagType
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing
            _sqlconn.Open()
            _sqlcmd.ExecuteNonQuery()
            _sqlconn.Close()



        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Return Nothing
        Finally
            If IsNothing(_sqlda) = False Then
                _sqlda.Dispose()
                _sqlda = Nothing
            End If
            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If
            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose()
                _sqlconn = Nothing
            End If

        End Try
        Return True
    End Function
    Private Function UpdateMessageFunctionality(ByVal _FlagType As Integer) As Boolean
        Dim _sqlconn As SqlConnection = New SqlConnection(dbConnectionstring)
        Dim _sqlcmd As New SqlCommand
        Dim _sqlda As New SqlDataAdapter

        Dim _sqlparam As SqlParameter
        Try

            _sqlcmd = New SqlCommand("Intuit_ReadSecureMessage", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)
            ''
            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = _PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            _sqlparam.Value = _CommDetailID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@FlagType", SqlDbType.Int)
            _sqlparam.Value = _FlagType
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing
            _sqlconn.Open()
            _sqlcmd.ExecuteNonQuery()
            _sqlconn.Close()


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Return Nothing
        Finally
            If IsNothing(_sqlda) = False Then
                _sqlda.Dispose()
                _sqlda = Nothing
            End If
            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If
            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose()
                _sqlconn = Nothing
            End If
        End Try
        Return True
    End Function
    Public Sub New(ByVal Connectionstring As String, ByVal Patientid As Int64, ByVal CommDetailID As Int64)
        dbConnectionstring = Connectionstring
        _PatientID = Patientid
        _CommDetailID = CommDetailID
    End Sub

    Public Sub New(ByVal Connectionstring As String, ByVal Patientid As Int64, ByVal machineID As Int64, ByVal machineName As String, ByVal loginUserName As String)
        dbConnectionstring = Connectionstring
        _PatientID = Patientid
        _machineID = machineID
        _machineName = machineName
        _loginUserName = loginUserName

        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                gstrMessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            End If
        End If

    End Sub

    '07052012 New Fucntion Added For Checking User Rights
    Public Function RetrieveUserRights(ByVal loginName As String, ByVal conStr As String) As Boolean
        Dim objCon As New SqlConnection
        Dim UserRights As Boolean = False
        objCon.ConnectionString = conStr
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim objParaUserName As New SqlParameter
        Dim objParaApplicationType As SqlParameter = Nothing
        Try

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveUserRights"


            'Sql parameter added by dipak 20091029 for indicate store procedure executed from EMR or PM
            objParaApplicationType = New SqlParameter
            With objParaUserName
                .ParameterName = "@UserName"
                .Value = loginName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUserName)
            'code added by dipak 20091029 to pass Sql parameter attributes value
            With objParaApplicationType
                .ParameterName = "@ApplicationType"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaApplicationType)


            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()

            Dim arrLst As New ArrayList
            While objSQLDataReader.Read
                If IsNothing(objSQLDataReader.Item(0)) = False Then
                    arrLst.Add(Trim(objSQLDataReader.Item(0)))
                End If
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing
            objCon.Dispose()
            objCon = Nothing

            '06122012-Added For Checking user rights For Intuit Communication
            ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal".
            ''change name of right from "Intuit" to "Patient Portal"
            If arrLst.Contains("Patient Portal") = True Then
                UserRights = True
            Else
                UserRights = False
            End If
        Catch ex As Exception
        Finally
            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If IsNothing(objParaUserName) = False Then
                objParaUserName = Nothing
            End If
            If IsNothing(objParaApplicationType) = False Then
                objParaApplicationType = Nothing
            End If
        End Try

       
        Return UserRights

    End Function


    Public Function SendRegistrationProcess(ByVal PatID As Long) As Int16

        Dim Con As SqlConnection = Nothing
        Dim _result As Int16 = 0
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try

            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("IntuitInsertProcessRegistration", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = New SqlParameter

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatID

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _machineID

            sqlParam = cmd.Parameters.Add("@MachineName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _machineName

            Con.Open()
            _result = Convert.ToInt16(cmd.ExecuteScalar())
            Con.Close()



        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try

        Return _result

    End Function

    Public Function CheckValidUser(Optional ShowMessage As Boolean = True) As Int16
        Dim _count As Int16 = 0
        Dim dt As DataTable = Nothing
        Dim dtPractice As DataTable = Nothing
        'Made change for memberID as int32 previous it was int16
        Dim _memBerID As Int64 = 0
        Dim _emailID As String = String.Empty
        Dim _dob As DateTime
        Dim _gender As String = String.Empty
        Dim _zip As String = String.Empty
        Dim _patientCode As String = String.Empty
        Dim _countStaff As Int16 = 0
        Dim _firstName As String = ""
        Dim _middleName As String = ""
        Dim _lastName As String = ""
        Dim _fullName As String = ""
        Dim _fullNameWithAgeandGender As String = ""
        Dim _missingList As String = ""
        Dim _result As Int16 = 0

        Try

            '07052012 Checking User Rights
            If RetrieveUserRights(_loginUserName, dbConnectionstring) = False Then
                If ShowMessage Then
                    MessageBox.Show("This user does not have the rights to send a new secure message. Please contact your system administrator for the same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return 0
            End If

            dtPractice = ToCheckPracticeIDAvailableOrNot()
            If (dtPractice IsNot Nothing) Then
                If dtPractice.Rows.Count = 0 Then
                    If ShowMessage Then
                        MessageBox.Show("There is no Practice ID added in the system which is needed to send a mail. This can be added from the Patient Portal service configuration settings or gloEMR admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Return 0
                End If
            Else
                If ShowMessage Then
                    MessageBox.Show("There is no Practice ID added in the system which is needed to send a mail. This can be added from the Patient Portal service configuration settings or gloEMR admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return 0
            End If

            dt = ToCheckPatientRegisterOrNot(_PatientID)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                _memBerID = Convert.ToInt64(dt.Rows(0)("MemberID"))
                _emailID = Convert.ToString(dt.Rows(0)("Email"))

                If IsDBNull(dt.Rows(0)("dtDob")) Then
                    If Convert.ToString(dt.Rows(0)("dtDob")) Then
                        _dob = Convert.ToDateTime(dt.Rows(0)("dtDob"))
                    End If
                End If

                _gender = Convert.ToString(dt.Rows(0)("Gender"))
                _zip = Convert.ToString(dt.Rows(0)("Zip"))
                _patientCode = Convert.ToString(dt.Rows(0)("Code"))
                _countStaff = Convert.ToInt16(dt.Rows(0)("CountStaff"))
                _firstName = Convert.ToString(dt.Rows(0)("FirstName"))
                _middleName = Convert.ToString(dt.Rows(0)("MiddleName"))
                _lastName = Convert.ToString(dt.Rows(0)("LastName"))
                _fullName = Convert.ToString(dt.Rows(0)("PatientName"))


            End If

            If _countStaff = 0 Then
                'No Staff in table
                If ShowMessage Then
                    MessageBox.Show("There are no Staff ID's added in the system which are needed to send a mail. Go to gloEMR Admin Settings/Interface settings to add Staff ID's.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return 0
            End If

            If _memBerID > 0 Then
                'Member is Registered'
                _count = 1
                Return _count
            End If

            'Gender Not Selected as other or blank
            If (_gender <> "") Then
                If _gender.ToUpper() = "OTHER" Then
                    _gender = ""
                End If
            End If

            'Get FullName appended DOB and GENDER

            If _gender <> "" Then
                _fullNameWithAgeandGender = _fullName & "( " & _PatientAge & ", " & _gender & " )"
            Else
                _fullNameWithAgeandGender = _fullName & "( " & _PatientAge & " )"

            End If


            'Send Registration
            If _memBerID = 0 And _emailID <> "" And _zip <> "" And _gender <> "" Then
                If ShowMessage Then
                    If MessageBox.Show("Only registered patients may receive messages on the portal. Patient " + _fullNameWithAgeandGender + " is not currently registered on the practice portal. If you proceed, a request will be sent to the patient to register and the patient will receive your message after their registration has been accepted. Would you like to proceed? " + System.Environment.NewLine + " " + System.Environment.NewLine + "Note: If the patient has already registered and you are getting this message, please check that there is no pending unaccepted task for the patient’s registration. ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                        _result = SendRegistrationProcess(_PatientID)
                        If (_result > 0) Then
                            _count = 2
                        Else
                            _count = 0
                        End If

                    Else
                        _count = 0
                    End If

                Else
                    _count = 0
                End If
            End If


            'Email, Zip, Gender Needed
            If _memBerID = 0 And (_emailID = "" Or _zip = "" Or _gender = "") Then

                'Get Missing List

                If _emailID = "" And _zip = "" And _gender = "" Then
                    _missingList = "Email-ID, Zip Code and Gender is not set or Gender is set to type Other"

                ElseIf _emailID <> "" And _zip <> "" And _gender = "" Then
                    _missingList = "Gender is not selected or set to type Other"

                ElseIf _emailID <> "" And _zip = "" And _gender <> "" Then
                    _missingList = "Zip Code is missing"

                ElseIf _emailID <> "" And _zip = "" And _gender = "" Then
                    _missingList = "Zip Code and Gender is not set or Gender is set to type Other"

                ElseIf _emailID = "" And _zip <> "" And _gender <> "" Then
                    _missingList = "Email-ID is missing"

                ElseIf _emailID = "" And _zip <> "" And _gender = "" Then
                    _missingList = "Email-ID and Gender is not set or Gender is set to type Other"

                ElseIf _emailID = "" And _zip = "" And _gender <> "" Then
                    _missingList = "Email-ID and Zip Code is not set"

                End If

                If ShowMessage Then
                    MessageBox.Show("Only patients registered on the practice portal may receive messages. Patient " + _fullNameWithAgeandGender + " may not be registered at this time as the following information has not been entered for this patient:  " + System.Environment.NewLine + " " + System.Environment.NewLine + _missingList + " " + System.Environment.NewLine + " " + System.Environment.NewLine + "Please go to Patient Registration and enter this information in order to enable secure messaging to this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                _count = 0

            End If


            Return _count


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0

        Finally
            If Not IsNothing(dtPractice) Then
                dtPractice.Dispose()
                dtPractice = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            _emailID = Nothing
            _gender = Nothing
            _zip = Nothing
            _patientCode = Nothing
            _firstName = Nothing
            _middleName = Nothing
            _lastName = Nothing
            _fullName = Nothing
            _fullNameWithAgeandGender = Nothing
            _missingList = Nothing

        End Try


    End Function

    

    Public Function ToCheckPatientRegisterOrNot(ByVal PatID As Long) As DataTable
        Dim Con As SqlConnection = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dataset As DataSet = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("IntuitCheckPatientRegisterOrNot", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = New SqlParameter


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatID

            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                dt = _dataset.Tables(0).Copy()
            End If

          Return dt



        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If Not IsNothing(_dataset) Then
                _dataset.Dispose()
                _dataset = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try


    End Function

    Public Function ToCheckPracticeIDAvailableOrNot() As DataTable
        Dim Con As SqlConnection = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dataset As DataSet = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("gsp_GetIntuitFusionSettings", Con)
            cmd.CommandType = CommandType.StoredProcedure

            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                dt = _dataset.Tables(0).Copy()
            End If

            Return dt



        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If Not IsNothing(_dataset) Then
                _dataset.Dispose()
                _dataset = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try


    End Function

    Public Property PatientAge() As String
        Get
            Return _PatientAge
        End Get
        Set(ByVal value As String)
            _PatientAge = value
        End Set
    End Property


    Public Function IntuitCheckIsReplied(ByVal CommDetailID As Long) As Int16

        Dim Con As SqlConnection = Nothing
        Dim _intFlag As Int16 = 0
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try

            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("IntuitCheckIsReplied", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = New SqlParameter

            sqlParam = cmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CommDetailID

            Con.Open()
            _intFlag = Convert.ToInt16(cmd.ExecuteScalar())

            Con.Close()



        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try

        Return _intFlag

    End Function

End Class
