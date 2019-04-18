Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Module mdlFunctions

    'sarika 11th sept 07
    Enum enmErrorOccuredForm
        StartupSettings
        Login
        MainForm
        Client
        Clinic
        DataDictionary
        DoctorType
        Doctor
        Tools
        None
    End Enum

    Enum enmOperation
        Add
        Update
        Delete
        Retrieve
        Others
        None
    End Enum

    Public _sErrorLogFileName As String = ""

    '-------------------------------------------------------

    Public Function IsInternetConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.gloStream.com/")
        ' Setup WebRequest
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            ' Attempt to get response and return True
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            'objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function

    'sarika 21st July 08
    'eDMS Categories 

    'Category List
    'Public Function DMSCategories() As Collection
    '    Dim clCategories As New Collection
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '    Dim objcmd As New SqlCommand
    '    Dim objReader As SqlDataReader
    '    objcmd.Connection = objCon
    '    objcmd.CommandText = "sp_DMS_FillCategories"
    '    objcmd.CommandType = CommandType.StoredProcedure
    '    Dim objParam As New SqlParameter
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.ParameterName = "@GetStatus"
    '    objParam.Value = 2
    '    objcmd.Parameters.Add(objParam)
    '    objCon.Open()
    '    objReader = objcmd.ExecuteReader()
    '    While objReader.Read
    '        clCategories.Add(objReader.GetString(1))
    '    End While
    '    objReader.Close()
    '    objCon.Close()
    '    Return clCategories
    'End Function




    'Category List
    Public Function DMSCategories() As Collection
        Dim clCategories As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objcmd As New SqlCommand
        Dim objReader As SqlDataReader
        objcmd.Connection = objCon
        'objcmd.CommandText = "select  CategoryId, CategoryName, isnull(IsDeleted,0) as IsDeleted , isnull(ClinicID,0) as ClinicID from eDocument_Category where isnull(CategoryName,'') <> '' and IsDeleted = 0 order by CategoryName"
        objcmd.CommandText = "select  CategoryId, CategoryName, isnull(ClinicID,0) as ClinicID from eDocument_Category_V3 where isnull(CategoryName,'') <> '' order by CategoryName"
        objcmd.CommandType = CommandType.Text

        objCon.Open()
        objReader = objcmd.ExecuteReader()

        If Not IsNothing(objReader) Then
            If objReader.HasRows = True Then
                While objReader.Read
                    clCategories.Add(objReader.GetString(1))
                End While
            End If
        End If


        objReader.Close()
        objCon.Close()

        Return clCategories
    End Function

    '----------End of eDMS Categories-----------


    'sarika 11th sept 07
    Public Sub UpdateErrorLog(ByVal strLogMessage As String, Optional ByVal enmForm As enmErrorOccuredForm = enmErrorOccuredForm.None, Optional ByVal enmErrorOperation As enmOperation = enmOperation.None, Optional ByVal blnErrorOccured As Boolean = False)
        Try
            If gblnErrorLogged = True Then
                ' Dim objErrorLog As New gloEMR.gloStreamAdmin.clsErrorLog(Application.StartupPath & "\gloStreamAdminErrorLog.txt")
                UpdateErrorLog1(strLogMessage, enmForm, enmErrorOperation, blnErrorOccured)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub UpdateErrorLog1(ByVal strLogMessage As String, Optional ByVal enmForm As enmErrorOccuredForm = enmErrorOccuredForm.None, Optional ByVal enmErrorOperation As enmOperation = enmOperation.None, Optional ByVal blnErrorOccured As Boolean = False)
        Try
            Dim strFormName As String
            Dim strOperation As String
            Select Case enmForm
                Case enmErrorOccuredForm.Client
                    strFormName = "Client         "
                Case enmErrorOccuredForm.Clinic
                    strFormName = "Clinic         "
                Case enmErrorOccuredForm.DataDictionary
                    strFormName = "Data Dictionary"
                Case enmErrorOccuredForm.Doctor
                    strFormName = "Doctor         "
                Case enmErrorOccuredForm.Login
                    strFormName = "Login          "
                Case enmErrorOccuredForm.MainForm
                    strFormName = "Main Form      "
                Case enmErrorOccuredForm.StartupSettings
                    strFormName = "Startup Settings"
                Case enmErrorOccuredForm.Tools
                    strFormName = "Tools          "
                Case enmErrorOccuredForm.None
                    strFormName = ""
            End Select
            Select Case enmErrorOperation
                Case enmOperation.Add
                    strOperation = "Add     "
                Case enmOperation.Delete
                    strOperation = "Delete  "
                Case enmOperation.Others
                    strOperation = "Others  "
                Case enmOperation.Retrieve
                    strOperation = "Retrieve"
                Case enmOperation.Update
                    strOperation = "Update  "
                Case enmOperation.None
                    strOperation = ""
            End Select
            Dim strMessage As String
            If Trim(strFormName) <> "" Then
                strMessage = strFormName & vbTab
            End If
            If Trim(strOperation) <> "" Then
                strMessage = strMessage & vbTab & strOperation
            End If
            If Trim(strMessage) <> "" Then
                strMessage = strMessage & vbTab & strLogMessage
            Else
                strMessage = strLogMessage
            End If
            Dim objFile As New System.IO.StreamWriter(_sErrorLogFileName, True)
            If blnErrorOccured = True Then
                objFile.WriteLine("----------------------------------------- Error Occured -----------------------------------------")
                objFile.WriteLine(System.DateTime.Now & vbTab & strMessage)
                objFile.WriteLine("-------------------------------------------------------------------------------------------------")
            Else
                objFile.WriteLine(System.DateTime.Now & vbTab & strMessage)
            End If
            objFile.Close()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub

    '-----------------------------

    'Added by rahul patel on 27-09-2010
    'For URL  validation.
    '----------Start if URL validation ----------
    Public Function CheckURLAddress(ByVal strUrlInput As String) As Boolean

        Dim strRegex As String
        ' strRegex = "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))([-a-zA-Z0-9@:%_\+.~#?&//=])+.([-a-zA-Z0-9@:%_\+.~#?&//=])+$"
        strRegex = "^((((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[a-zA-Z0-9_\-]+(?:\.[a-zA-Z0-9_\-]+)*\.[a-zA-Z]{2,4}(?:\/[a-zA-Z0-9_]+)*(?:\/[a-zA-Z0-9_]+\.[a-zA-Z]{2,4}(?:\?[a-zA-Z0-9_]+\=[a-zA-Z0-9_]+)?)?(?:\&[a-zA-Z0-9_]+\=[a-zA-Z0-9_]+)*)$"
        If Regex.IsMatch(strUrlInput, strRegex) = False Then
            Return False
        Else
            Return True
        End If
    End Function
    '------End of URL validation -----------

    'Added by rahul patel on 27-09-2010

    'For Email  validation.
    '----------Start if Email validation ----------
    Public Function CheckEmailAddress(ByVal strEmailIdInput As String) As Boolean

        Dim strRegex As String

        strRegex = "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        If Regex.IsMatch(strEmailIdInput, strRegex) = False Then
            Return False
        Else
            Return True
        End If
    End Function
    '------End of Email validation -----------


End Module
