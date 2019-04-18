Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports System.Data.SqlClient

Module mdlGeneral
    Public gblngloTreeViewLoading As Boolean = False

    Public gblnSMDBSetting As Boolean
    Public gstrSMDBConnstr As String
    Public gstrSMDBServerName As String
    Public gstrSMDBDatabaseName As String
    Public gstrSMDBUserID As String
    Public gstrSMDBPassWord As String
    Public gblnSMDBAuthen As Boolean
   
    Public Function GetHybridConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If

        Return strConnectionString
    End Function
    Public Sub SetListBoxToolTip(ByVal LstBox As ListBox, ByVal C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal Position As Point)
        ' by Ujwala Atre to give tooltip to Listbox
        Dim MousePositionInClientCoords As Point = LstBox.PointToClient(Position)
        Dim indexUnderTheMouse As Integer = LstBox.IndexFromPoint(MousePositionInClientCoords)
        If indexUnderTheMouse > -1 Then
            Dim s As String = LstBox.Items(indexUnderTheMouse).ToString
            Dim g As Graphics = LstBox.CreateGraphics
            If g.MeasureString(s, LstBox.Font).Width > LstBox.ClientRectangle.Width Then
                C1SuperTooltip1.SetToolTip(LstBox, s)
            Else
                C1SuperTooltip1.SetToolTip(LstBox, "")
            End If
            g.Dispose()
        End If
        'by Ujwala Atre to give tooltip to Listbox
    End Sub
    'Public Function FormatPhoneandSSN(ByVal strTemp As String, ByVal FlgSSN As Int16)
    '    Try
    '        If FlgSSN = 0 Then
    '            If strTemp <> "" Then
    '                strTemp = "(" & strTemp.Substring(0, 3) & ")" & strTemp.Substring(3, 3) & "-" & strTemp.Substring(6, 4)
    '            Else
    '                strTemp = ""
    '            End If

    '        ElseIf FlgSSN = 1 Then
    '            If strTemp <> "" Then
    '                strTemp = strTemp.Substring(0, 3) & "-" & strTemp.Substring(3, 2) & "-" & strTemp.Substring(5, 4)
    '            Else
    '                strTemp = ""
    '            End If
    '        End If

    '        Return strTemp
    '    Catch ex As Exception
    '    Finally

    '    End Try
    '    Return strTemp
    'End Function
    'Public Function FormatPhoneandSSN(ByVal strTemp As String, ByVal FlgSSN As Int16)
    '    Try
    '        Dim _strRegex As String = ""
    '        _strRegex = "%[^0-9]%"
    '        If strTemp <> "" Then
    '            For Each c As Char In strTemp
    '                If Regex.IsMatch(c.ToString(), _strRegex) = True Then
    '                    strTemp = strTemp.Replace(c.ToString(), "")
    '                End If
    '            Next
    '            If FlgSSN = 0 Then
    '                strTemp = "(" & strTemp.Substring(0, 3) & ")" & strTemp.Substring(3, 3) & "-" & strTemp.Substring(6, 4)
    '            ElseIf FlgSSN = 1 Then
    '                strTemp = strTemp.Substring(0, 3) & "-" & strTemp.Substring(3, 2) & "-" & strTemp.Substring(5, 4)
    '            End If
    '        Else
    '            strTemp = ""
    '        End If
    '        Return strTemp
    '    Catch ex As Exception
    '    Finally
    '    End Try
    '    Return strTemp
    'End Function
    Public Function GetDefaultPatPhDetails(ByVal nPatientId As Long) As DataTable
        Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer()

        Dim PhDetails As DataTable = Nothing
        Try
            'Dim strquery As String = "select isnull(nPharmacyID,0) from Patient where npatientid = " & nPatientId & " "
            Dim strquery As String
            strquery = " select isnull(nContactId,0) as PharmacyId,ISNULL(sName,'') AS PharmacyName,ISNULL( sAddressLine1,'') AS AddressLine1 ," _
                        & " ISNULL(sAddressLine2,'') AS AddressLine2 ,ISNULL(sCity,'')AS City,ISNULL(sState,'')AS State,ISNULL(nContactId,0) AS ContactId," _
                        & " ISNULL(sZIP,'')AS Zip,ISNULL(sPhone,'')AS Phone,ISNULL(sFax,'')AS Fax,ISNULL(sNCPDPID,'')AS NCPDPID ,ISNULL(sEmail,'')AS Email," _
                        & " ISNULL(sServiceLevel,'')AS ServiceLevel" _
                        & " from Patient_DTL where nContactFlag =1 and nPatientID = " & nPatientId & " AND ISNULL(nContactStatus,0) = 1"
            PhDetails = ogloEMRDatabase.GetDataTable_Query(strquery)

            Return PhDetails

        Catch ex As Exception
            Return PhDetails
        Finally
            'If Not IsNothing(PhDetails) Then
            '    PhDetails.Dispose()
            '    PhDetails = Nothing
            'End If
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If
        End Try
    End Function
   

    
    Public Function Get_DocumentDetails(ByVal nPatientID As Long, ByVal nOrderID As Long, ByVal nTestID As Long, Optional ByVal DMSIDCollection As String = "") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString
        Dim objCmd As New SqlCommand
        Dim dsData As DataSet = Nothing
        Dim myTable As DataTable = Nothing
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "Lab_Get_Test_Attachment"
            objCmd.Parameters.Clear()

            Dim objParaPatient As New SqlParameter
            With objParaPatient
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatient)
            objParaPatient = Nothing

            Dim objParaOrder As New SqlParameter
            With objParaOrder
                .ParameterName = "@OrderID"
                .Value = nOrderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaOrder)
            objParaOrder = Nothing

            Dim objParaTest As New SqlParameter
            With objParaTest
                .ParameterName = "@TestID"
                .Value = nTestID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaTest)
            objParaTest = Nothing

            Dim objParaDMSID As New SqlParameter
            With objParaDMSID
                .ParameterName = "@DMSIDCollection"
                .Value = DMSIDCollection
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDMSID)
            objParaDMSID = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            dsData = New DataSet
            objDA.Fill(dsData)
            myTable = dsData.Tables(0).Copy()
            objCon.Close()

            objDA.Dispose()
            objDA = Nothing


            If Not IsNothing(objParaPatient) Then
                objParaPatient = Nothing
            End If

            If Not IsNothing(objParaOrder) Then
                objParaOrder = Nothing
            End If

            If Not IsNothing(objParaTest) Then
                objParaTest = Nothing
            End If

            If Not IsNothing(objParaDMSID) Then
                objParaDMSID = Nothing
            End If

        Catch ex As Exception

        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(dsData) Then
                dsData.Dispose()
                dsData = Nothing
            End If
            objCon.Dispose()
            objCon = Nothing
        End Try


        Return myTable

    End Function

    Public Function Get_CQMConceptIDetails(ByVal nPatientID As Long, ByVal nOrderID As Long, ByVal nTestID As Long) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString
        Dim objCmd As New SqlCommand
        Dim dsData As DataSet = Nothing
        Dim myTable As DataTable = Nothing
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetCQMCategories"
            objCmd.Parameters.Clear()

            Dim objParaPatient As New SqlParameter
            With objParaPatient
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatient)
            objParaPatient = Nothing

            Dim objParaOrder As New SqlParameter
            With objParaOrder
                .ParameterName = "@OrderID"
                .Value = nOrderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaOrder)
            objParaOrder = Nothing

            Dim objParaTest As New SqlParameter
            With objParaTest
                .ParameterName = "@TestID"
                .Value = nTestID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaTest)
            objParaTest = Nothing



            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            dsData = New DataSet
            objDA.Fill(dsData)
            myTable = dsData.Tables(0).Copy()
            objCon.Close()

            objDA.Dispose()
            objDA = Nothing


            If Not IsNothing(objParaPatient) Then
                objParaPatient = Nothing
            End If

            If Not IsNothing(objParaOrder) Then
                objParaOrder = Nothing
            End If

            If Not IsNothing(objParaTest) Then
                objParaTest = Nothing
            End If


        Catch ex As Exception

        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(dsData) Then
                dsData.Dispose()
                dsData = Nothing
            End If
            objCon.Dispose()
            objCon = Nothing
        End Try


        Return myTable

    End Function


  

End Module
