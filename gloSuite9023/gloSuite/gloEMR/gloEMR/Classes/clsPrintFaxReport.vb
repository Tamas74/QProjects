Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient


Public Class clsPrintFaxReport

    Dim _isInternetAvailable As Boolean = False 'use this variable throughtout the RxForm to add validation to confirm whether there is internet connection available 
    Dim _PatientID As Long
    WithEvents objInternetFax As clsInternetFax

    Private _Owner As Form = Nothing

    Public Property Owner() As Form
        Get
            Return _Owner
        End Get
        Set(ByVal value As Form)
            _Owner = value
        End Set
    End Property
    Public mySFileName As String = Nothing 'added in 6050 for faxchanges in case when efax is off 
    'added IsFax flag in 6050 for faxchanges in case when efax is off 	
    Public Function FaxReport(ByVal oRpt As ReportDocument, Optional IsFax As Boolean = False) As Boolean

        Dim rstream As MemoryStream = Nothing

        Try

            'If mdleFax.IsInternetConnectionAvailable() = False Then
            '    MessageBox.Show("Unable to send the Fax as connection to internet is temporarily lost.", "gloEMR-eFax", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Function
            'End If
            'first chek if internet is available and then only send the eRx, if not then exit from the function
            '''''http://www.developerfusion.com/code/3903/is-an-internet-connection-available/
            _isInternetAvailable = IsInternetConnectionAvailable()
            If _isInternetAvailable = False Then
                MessageBox.Show("Unable to send the Fax as connection to internet is temporarily lost.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                FaxReport = Nothing
                Exit Function
            End If


            rstream = oRpt.ExportToStream(ExportFormatType.PortableDocFormat)

            If IsFax = False Then
                FaxRptUsingEFax(rstream.ToArray())
            Else
                'sarika Temp Directory issue on Terminal Server
                '   Dim strfilename As String = Application.StartupPath & "\" & Path.GetFileNameWithoutExtension(oRpt.FileName) & ".pdf"
                Dim strfilename As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf")
                If File.Exists(strfilename) Then
                    File.Delete(strfilename)
                End If

                Dim oFile As New FileStream(strfilename, FileMode.Create)
                rstream.WriteTo(oFile)

                ' Dim filedata As String = rstream.ToString()
                oFile.Close()


                oFile.Dispose()

                mySFileName = strfilename
            End If
            rstream.Close()
            rstream.Dispose()
            FaxReport = Nothing
        Catch ex As Exception
            FaxReport = Nothing
            Throw ex
        End Try

    End Function
    'added new method for faxing SSRS report(7010_phaseII)
    Public Function FaxReport(ByVal strFile As String, Optional ByVal IsFax As Boolean = False) As Boolean
        Try

            '''''http://www.developerfusion.com/code/3903/is-an-internet-connection-available/
            _isInternetAvailable = IsInternetConnectionAvailable()
            If _isInternetAvailable = False Then
                MessageBox.Show("Unable to send the Fax as connection to internet is temporarily lost.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                FaxReport = Nothing
                Exit Function
            End If


            If IsFax = False Then
                Dim oByte() As Byte = Nothing
                oByte = GenerateBinaryStream(strFile)
                FaxRptUsingEFax(oByte)
                If File.Exists(strFile) Then
                    File.Delete(strFile)
                End If
            Else
                mySFileName = strFile
            End If
            FaxReport = Nothing
        Catch ex As Exception
            FaxReport = Nothing
            Throw ex
        End Try

    End Function

    'Private Sub FaxRptUsingEFax(ByVal filename As String)

    '    Dim objFAX As clsFAX
    '    Dim nFaxID As Int64 = 0

    '    Try


    '        Dim objEFaxSettings As New clsEFaxSettings

    '        objEFaxSettings.FaxID = nFaxID
    '        objEFaxSettings.EFax_UserID = gstrEFaxUserID
    '        objEFaxSettings.EFax_UserPassword = gstrEFaxUserPassword
    '        objEFaxSettings.EFax_Tiff_image_flag = "false"
    '        objEFaxSettings.EFax_Resolution = "high"
    '        objEFaxSettings.EFax_FaxRecipientName = gstrFAXContactPerson
    '        objEFaxSettings.EFax_FaxRecipientNumber = gstrFAXContactPersonFAXNo
    '        objEFaxSettings.EFax_FromName = gstrLoginName
    '        objEFaxSettings.EFax_Faxfilepath = filename
    '        objEFaxSettings.EFax_FaxCoverpagefilepath = ""
    '        objEFaxSettings.EFax_FaxCoverPage = False
    '        objEFaxSettings.EFax_DocumentExtension = "pdf"
    '        objEFaxSettings.EFax_DocumentEncodingType = "base64"
    '        objEFaxSettings.EFax_DocumentContentType = "application/pdf"
    '        objEFaxSettings.EFax_BillingCode = ""

    '        'objInternetFax = New clsInternetFax(objEFaxSettings)
    '        'objInternetFax.efaxdocument()

    '        UpdateLogForFax("---------In FAX Document method-----------")


    '        If multipleRecipients = False Then

    '            UpdateLogForFax("Start objFAX.AddPendingFAX For Single Recipent")
    '            'Add the FAX Details in the Database
    '            objFAX = New clsFAX
    '            Dim oByte As Byte()
    '            oByte = GenerateBinaryStream(filename)
    '            nFaxID = objFAX.AddPendingFAX1(_PatientID, objEFaxSettings.EFax_FaxRecipientName, gstrFAXType, objEFaxSettings.EFax_FaxRecipientNumber, objEFaxSettings.EFax_FromName, "", Now, Convert.ToBase64String(oByte), "pdf", CurrentSendingFAXPriority)
    '            objFAX.Dispose()
    '            objFAX = Nothing
    '            UpdateLogForFax("END objFAX.AddPendingFAX For Single Recipent")
    '            objEFaxSettings.FaxID = nFaxID

    '            'objInternetFax = New clsInternetFax(objEFaxSettings)
    '            'objInternetFax.efaxdocument()
    '        Else

    '            UpdateLogForFax("For Multiple Recipent")
    '            UpdateLogForFax(" gstrFAXContacts.Count= " & gstrFAXContacts.Count.ToString())
    '            If gstrFAXContacts.Count = 0 Then
    '                MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                objEFaxSettings.Dispose()
    '                Exit Sub
    '            End If
    '            Dim oByte As Byte()

    '            If gstrFAXContacts.Count = 0 Then

    '                objFAX = New clsFAX
    '                oByte = GenerateBinaryStream(filename)
    '                nFaxID = objFAX.AddPendingFAX1(_PatientID, gstrFAXContactPerson, gstrFAXType, gstrFAXContactPersonFAXNo, gstrLoginName, "", Now, Convert.ToBase64String(oByte), "pdf", CurrentSendingFAXPriority, "docx")
    '                oByte = Nothing
    '                objFAX.Dispose()
    '                objFAX = Nothing
    '                objEFaxSettings.Dispose()
    '                Exit Sub
    '            End If

    '            Dim mynode As myTreeNode
    '            'mynode = New myTreeNode
    '            mynode = CType(gstrFAXContacts.Item(1), myTreeNode)

    '            objFAX = New clsFAX
    '            '    Dim oByte As Byte()
    '            oByte = GenerateBinaryStream(filename)
    '            nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now, Convert.ToBase64String(oByte), "pdf", CurrentSendingFAXPriority, "docx")
    '            objFAX.Dispose()
    '            objFAX = Nothing
    '            objEFaxSettings.FaxID = nFaxID
    '            objEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
    '            objEFaxSettings.EFax_FaxRecipientName = mynode.Text
    '            mynode = Nothing
    '            'objInternetFax = New clsInternetFax(objEFaxSettings)
    '            'objInternetFax.efaxdocument()
    '            UpdateLogForFax("gstrFAXContacts.Count: " & gstrFAXContacts.Count)

    '            'code added by sarika 10th dec 07

    '            Dim objNewEFaxSettings As clsEFaxSettings

    '            For i As Integer = 2 To gstrFAXContacts.Count
    '                '' First Fax is Sent, Now Therefore gstrFAXContacts Starts From 2 i.e Satr Sending 2nd FAx  
    '                UpdateLogForFax("gstrFAXContacts(i): " & i)
    '                UpdateLogForFax("Start MainMenu.SetFAXPrinterDocumentSettings1")
    '                UpdateLogForFax("Successfully done MainMenu.SetFAXPrinterDocumentSettings1")

    '                'mynode = New myTreeNode
    '                mynode = CType(gstrFAXContacts.Item(i), myTreeNode)
    '                objFAX = New clsFAX
    '                oByte = Nothing
    '                oByte = GenerateBinaryStream(filename)
    '                nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now, Convert.ToBase64String(oByte), "pdf", CurrentSendingFAXPriority)
    '                UpdateLogForFax("Contact Fax Name : " & mynode.Text & " and Contact Fax No. :" & mynode.Tag)
    '                UpdateLogForFax("Fax entry done in FaxPending_mst for the contact :" & mynode.Text)
    '                objFAX.Dispose()
    '                objFAX = Nothing
    '                objNewEFaxSettings = objEFaxSettings
    '                objNewEFaxSettings.FaxID = nFaxID
    '                objNewEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
    '                objNewEFaxSettings.EFax_FaxRecipientName = mynode.Text



    '                'objInternetFax = New clsInternetFax(objNewEFaxSettings)
    '                'objInternetFax.efaxdocument()
    '                objNewEFaxSettings = Nothing

    '                mynode = Nothing
    '            Next
    '        End If
    '        objEFaxSettings.Dispose()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub FaxRptUsingEFax(ByVal oByte() As Byte)

        Dim objFAX As clsFAX
        Dim nFaxID As Int64 = 0
        Dim base64oByte As String = Convert.ToBase64String(oByte)
        Try


            'Dim objEFaxSettings As New clsEFaxSettings

            'objEFaxSettings.FaxID = nFaxID
            'objEFaxSettings.EFax_UserID = gstrEFaxUserID
            'objEFaxSettings.EFax_UserPassword = gstrEFaxUserPassword
            'objEFaxSettings.EFax_Tiff_image_flag = "false"
            'objEFaxSettings.EFax_Resolution = "high"
            'objEFaxSettings.EFax_FaxRecipientName = gstrFAXContactPerson
            'objEFaxSettings.EFax_FaxRecipientNumber = gstrFAXContactPersonFAXNo
            'objEFaxSettings.EFax_FromName = gstrLoginName
            'objEFaxSettings.EFax_Faxfilepath = filename
            'objEFaxSettings.EFax_FaxCoverpagefilepath = ""
            'objEFaxSettings.EFax_FaxCoverPage = False
            'objEFaxSettings.EFax_DocumentExtension = "pdf"
            'objEFaxSettings.EFax_DocumentEncodingType = "base64"
            'objEFaxSettings.EFax_DocumentContentType = "application/pdf"
            'objEFaxSettings.EFax_BillingCode = ""

            'objInternetFax = New clsInternetFax(objEFaxSettings)
            'objInternetFax.efaxdocument()

            UpdateLogForFax("---------In FAX Document method-----------")


            If multipleRecipients = False Then

                UpdateLogForFax("Start objFAX.AddPendingFAX For Single Recipent")
                'Add the FAX Details in the Database
                objFAX = New clsFAX
                'Dim oByte As Byte()
                'oByte = GenerateBinaryStream(filename)
                nFaxID = objFAX.AddPendingFAX1(_PatientID, gstrFAXContactPerson, gstrFAXType, gstrFAXContactPersonFAXNo, gstrLoginName, "", Now, base64oByte, "pdf", CurrentSendingFAXPriority)
                objFAX.Dispose()
                objFAX = Nothing
                UpdateLogForFax("END objFAX.AddPendingFAX For Single Recipent")
                'objEFaxSettings.FaxID = nFaxID

                'objInternetFax = New clsInternetFax(objEFaxSettings)
                'objInternetFax.efaxdocument()
            Else

                UpdateLogForFax("For Multiple Recipent")
                UpdateLogForFax(" gstrFAXContacts.Count= " & gstrFAXContacts.Count.ToString())
                If gstrFAXContacts.Count = 0 Then
                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '   objEFaxSettings.Dispose()
                    Exit Sub
                End If
                'Dim oByte As Byte()

                If gstrFAXContacts.Count = 0 Then

                    objFAX = New clsFAX
                    'oByte = GenerateBinaryStream(filename)
                    nFaxID = objFAX.AddPendingFAX1(_PatientID, gstrFAXContactPerson, gstrFAXType, gstrFAXContactPersonFAXNo, gstrLoginName, "", Now, base64oByte, "pdf", CurrentSendingFAXPriority, "docx")
                    ' oByte = Nothing
                    objFAX.Dispose()
                    objFAX = Nothing
                    '  objEFaxSettings.Dispose()
                    Exit Sub
                End If

                Dim mynode As myTreeNode
                'mynode = New myTreeNode
                mynode = CType(gstrFAXContacts.Item(1), myTreeNode)

                objFAX = New clsFAX
                '    Dim oByte As Byte()
                'oByte = GenerateBinaryStream(filename)
                nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now, base64oByte, "pdf", CurrentSendingFAXPriority, "docx")
                objFAX.Dispose()
                objFAX = Nothing
                'objEFaxSettings.FaxID = nFaxID
                'objEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
                'objEFaxSettings.EFax_FaxRecipientName = mynode.Text
                mynode = Nothing
                'objInternetFax = New clsInternetFax(objEFaxSettings)
                'objInternetFax.efaxdocument()
                UpdateLogForFax("gstrFAXContacts.Count: " & gstrFAXContacts.Count)

                'code added by sarika 10th dec 07

                '   Dim objNewEFaxSettings As clsEFaxSettings

                For i As Integer = 2 To gstrFAXContacts.Count
                    '' First Fax is Sent, Now Therefore gstrFAXContacts Starts From 2 i.e Satr Sending 2nd FAx  
                    UpdateLogForFax("gstrFAXContacts(i): " & i)
                    UpdateLogForFax("Start MainMenu.SetFAXPrinterDocumentSettings1")
                    UpdateLogForFax("Successfully done MainMenu.SetFAXPrinterDocumentSettings1")

                    'mynode = New myTreeNode
                    mynode = CType(gstrFAXContacts.Item(i), myTreeNode)
                    objFAX = New clsFAX
                    '   oByte = Nothing
                    '  oByte = GenerateBinaryStream(filename)
                    nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now, base64oByte, "pdf", CurrentSendingFAXPriority)
                    UpdateLogForFax("Contact Fax Name : " & mynode.Text & " and Contact Fax No. :" & mynode.Tag)
                    UpdateLogForFax("Fax entry done in FaxPending_mst for the contact :" & mynode.Text)
                    objFAX.Dispose()
                    objFAX = Nothing
                    'objNewEFaxSettings = objEFaxSettings
                    'objNewEFaxSettings.FaxID = nFaxID
                    'objNewEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
                    'objNewEFaxSettings.EFax_FaxRecipientName = mynode.Text



                    ''objInternetFax = New clsInternetFax(objNewEFaxSettings)
                    ''objInternetFax.efaxdocument()
                    'objNewEFaxSettings = Nothing

                    mynode = Nothing
                Next
            End If
            'objEFaxSettings.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GenerateBinaryStream(ByVal strfilename As String) As Byte()
        Dim oFile As FileStream
        Dim oReader As BinaryReader
        Dim bytesRead As Byte()

        Try
            ' Dim strfilename As String = Application.StartupPath & "\SampleFax.docx"
            'Const CHUNK_SIZE As Integer = 1024 * 8
            '8K write buffer. 
            If File.Exists(strfilename) Then

                ''Please uncomment the following line of code to read the file, even the file is in use by same or another process 
                ' oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous); 

                ''To read the file only when it is not in use by any process 
                oFile = New FileStream(strfilename, FileMode.Open, FileAccess.Read)

                oReader = New BinaryReader(oFile)

                bytesRead = New Byte(oReader.BaseStream.Length - 1) {}
                oReader.Read(bytesRead, 0, bytesRead.Length)
                oFile.Close()
                oReader.Close()
                oReader.Dispose()
                oFile.Dispose()
                Return bytesRead

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub FaxRptUsingEFaxBackUP(ByVal filename As String)

        Dim objFAX As clsFAX
        Dim nFaxID As Int64 = 0

        Try


            Dim objEFaxSettings As New clsEFaxSettings

            objEFaxSettings.FaxID = nFaxID
            objEFaxSettings.EFax_UserID = gstrEFaxUserID
            objEFaxSettings.EFax_UserPassword = gstrEFaxUserPassword
            objEFaxSettings.EFax_Tiff_image_flag = "false"
            objEFaxSettings.EFax_Resolution = "high"
            objEFaxSettings.EFax_FaxRecipientName = gstrFAXContactPerson
            objEFaxSettings.EFax_FaxRecipientNumber = gstrFAXContactPersonFAXNo
            objEFaxSettings.EFax_FromName = gstrLoginName
            objEFaxSettings.EFax_Faxfilepath = filename
            objEFaxSettings.EFax_FaxCoverpagefilepath = ""
            objEFaxSettings.EFax_FaxCoverPage = False
            objEFaxSettings.EFax_DocumentExtension = "pdf"
            objEFaxSettings.EFax_DocumentEncodingType = "base64"
            objEFaxSettings.EFax_DocumentContentType = "application/pdf"
            objEFaxSettings.EFax_BillingCode = ""

            'objInternetFax = New clsInternetFax(objEFaxSettings)
            'objInternetFax.efaxdocument()

            UpdateLogForFax("---------In FAX Document method-----------")


            If multipleRecipients = False Then

                UpdateLogForFax("Start objFAX.AddPendingFAX For Single Recipent")
                'Add the FAX Details in the Database
                objFAX = New clsFAX
                ' Dim oByte As Byte()
                nFaxID = objFAX.AddPendingFAX1(_PatientID, objEFaxSettings.EFax_FaxRecipientName, gstrFAXType, objEFaxSettings.EFax_FaxRecipientNumber, objEFaxSettings.EFax_FromName, "", Now.Date, "", CurrentSendingFAXPriority)
                objFAX.Dispose()
                objFAX = Nothing
                UpdateLogForFax("END objFAX.AddPendingFAX For Single Recipent")

                objEFaxSettings.FaxID = nFaxID

                objInternetFax = New clsInternetFax(objEFaxSettings)
                objInternetFax.efaxdocument()

            Else

                UpdateLogForFax("For Multiple Recipent")
                UpdateLogForFax(" gstrFAXContacts.Count= " & gstrFAXContacts.Count.ToString())
                If gstrFAXContacts.Count = 0 Then
                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objEFaxSettings.Dispose()
                    Exit Sub
                End If

                Dim mynode As myTreeNode
                ' mynode = New myTreeNode
                mynode = CType(gstrFAXContacts.Item(1), myTreeNode)

                objFAX = New clsFAX
                'Dim oByte As Byte()
                nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now.Date(), "", CurrentSendingFAXPriority)
                objFAX.Dispose()
                objFAX = Nothing
                objEFaxSettings.FaxID = nFaxID
                objEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
                objEFaxSettings.EFax_FaxRecipientName = mynode.Text
                mynode = Nothing
                objInternetFax = New clsInternetFax(objEFaxSettings)
                objInternetFax.efaxdocument()
                UpdateLogForFax("gstrFAXContacts.Count: " & gstrFAXContacts.Count)

                'code added by sarika 10th dec 07

                Dim objNewEFaxSettings As clsEFaxSettings

                For i As Integer = 2 To gstrFAXContacts.Count
                    '' First Fax is Sent, Now Therefore gstrFAXContacts Starts From 2 i.e Satr Sending 2nd FAx  
                    UpdateLogForFax("gstrFAXContacts(i): " & i)
                    UpdateLogForFax("Start MainMenu.SetFAXPrinterDocumentSettings1")
                    UpdateLogForFax("Successfully done MainMenu.SetFAXPrinterDocumentSettings1")

                    'mynode = New myTreeNode
                    mynode = CType(gstrFAXContacts.Item(i), myTreeNode)
                    objFAX = New clsFAX
                    'Dim oByte As Byte()
                    nFaxID = objFAX.AddPendingFAX1(_PatientID, mynode.Text, gstrFAXType, mynode.Tag, gstrLoginName, "", Now.Date(), "", CurrentSendingFAXPriority)
                    UpdateLogForFax("Contact Fax Name : " & mynode.Text & " and Contact Fax No. :" & mynode.Tag)
                    UpdateLogForFax("Fax entry done in FaxPending_mst for the contact :" & mynode.Text)
                    objFAX.Dispose()
                    objFAX = Nothing
                    objNewEFaxSettings = objEFaxSettings
                    objNewEFaxSettings.FaxID = nFaxID
                    objNewEFaxSettings.EFax_FaxRecipientNumber = mynode.Tag
                    objNewEFaxSettings.EFax_FaxRecipientName = mynode.Text

                    objInternetFax = New clsInternetFax(objNewEFaxSettings)
                    objInternetFax.efaxdocument()
                    objNewEFaxSettings = Nothing

                    mynode = Nothing
                Next
            End If
            objEFaxSettings.Dispose()
        Catch ex As Exception
            Throw ex
        End Try





    End Sub


    Private Sub objInternetFax_FaxSent(ByVal TransactionId As String) Handles objInternetFax.FaxSent
        'update the transactionid in FaxPending_Mst table
        If TransactionId > 0 Then


            Dim conn As New SqlConnection(GetConnectionString())
            Dim cmd As New SqlCommand

            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Update FaxPending_Mst set TransactionID='" & TransactionId & "', faxfilebinarydata='" & objInternetFax.EFaxSettings.FaxFileBinaryData.ToString() & "' where nFaxID=" & objInternetFax.EFaxSettings.FaxID & ""

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                conn.Close()
            Catch ex As Exception
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
            End Try
        End If

        MessageBox.Show(TransactionId, "clsPrintFax", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Public Sub New(ByVal PatientID As Long)
        _PatientID = PatientID
    End Sub
End Class
