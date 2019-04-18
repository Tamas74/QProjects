'Imports gloEMR.gloFaxService
Imports System.Xml
Imports System.Text
Imports System.IO
Imports System.Windows.Forms


Public Class clsInternetFax



#Region "Event Declarations"
    Public Event FaxSent(ByVal TransactionId As String)
#End Region

#Region "Variables"
    Private _ErrorFlag As Boolean
    Private _ReturnMessage As String = ""
    Public _TransactionID As String = ""
#End Region

#Region "EFax Settings Variables"
    Private _EFaxSettings As New clsEFaxSettings

    Public Property EFaxSettings() As clsEFaxSettings
        Get
            Return _EFaxSettings
        End Get
        Set(ByVal value As clsEFaxSettings)
            _EFaxSettings = value
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New(ByVal eFaxSettings As clsEFaxSettings)
        _EFaxSettings = eFaxSettings
        Me.EFaxSettings = eFaxSettings
    End Sub
#End Region

#Region "Methods and Functions for Internet Fax"

    Public Function efaxdocument() As Boolean

        UpdateLogForFax("In efaxdocument : " & Now)

        Try
            If GenerateXML() Then
                UpdateLogForFax("Loading XmlDocument : ")
                Dim odoc As XmlDocument = New XmlDocument
                odoc.Load(gstrgloEMRStartupPath & "\SampleXML.xml")
                UpdateLogForFax("Loading XmlDocument Finished: ")
                ' Dim oResponse As SendResponse

                'MessageBox.Show(odoc.InnerXml)
                '   oResponse = osamplesendfax.SendSingleFax(odoc.InnerXml)
                UpdateLogForFax("SendSingleFaxAsync Started: ")
                '  osamplesendfax.SendSingleFaxAsync(odoc.InnerXml)
                UpdateLogForFax("SendSingleFaxAsync Finished: ")
                'MessageBox.Show(oResponse.Header.ReturnMessage)
                'MessageBox.Show(oResponse.TransactionID)

                ' Return oResponse.TransactionID
                ' Return _TransactionID
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        UpdateLogForFax("End of efaxdocument : " & Now)
        Return Nothing
    End Function

#End Region

#Region "Methods for File Conversions"
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
                oFile.Dispose()
                oReader.Dispose()
                oFile = Nothing
                oReader = Nothing
                Return bytesRead

            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try
    End Function

    Private Sub GenerateDatafromBinaryStream(ByVal ZipContent As Byte(), ByVal sfilename As String)
        Try
            Dim strfilename As String = gstrgloEMRStartupPath & "\" & sfilename
            '    Dim Stream As New MemoryStream(ZipContent)
            If (IsNothing(ZipContent) = False) Then
                Dim oFile As New FileStream(strfilename, System.IO.FileMode.Create)
                oFile.Write(ZipContent, 0, ZipContent.Length)
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
            End If


            'Stream.WriteTo(oFile)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Function efaxdocumentBackUp() As Boolean

        UpdateLogForFax("In efaxdocument : " & Now)

        Try
            If GenerateXML() Then
                UpdateLogForFax("Loading XmlDocument : " & Now)
                Dim odoc As XmlDocument = New XmlDocument
                odoc.Load(gstrgloEMRStartupPath & "\SampleXML.xml")
                UpdateLogForFax("Loading XmlDocument : " & Now)
                ' Dim oResponse As SendResponse

                'MessageBox.Show(odoc.InnerXml)
                '   oResponse = osamplesendfax.SendSingleFax(odoc.InnerXml)
                '  osamplesendfax.SendSingleFaxAsync(odoc.InnerXml)
                'MessageBox.Show(oResponse.Header.ReturnMessage)
                'MessageBox.Show(oResponse.TransactionID)

                ' Return oResponse.TransactionID
                ' Return _TransactionID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        UpdateLogForFax("End of efaxdocument : " & Now)
        Return Nothing
    End Function

    Public Function GenerateXML() As Boolean
        'UpdateLogForFax("In GenerateXML")
        Dim strfilepath As String = gstrgloEMRStartupPath & "\SampleXML.xml"

        Dim xmlwriter As XmlTextWriter = Nothing
        Try
            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If
            xmlwriter = New XmlTextWriter(strfilepath, Nothing)
            xmlwriter.WriteStartDocument()
            xmlwriter.WriteStartElement("single_fax_info")

            xmlwriter.WriteAttributeString("xmlns", "http://www.protus.com")
            xmlwriter.WriteAttributeString("xmlns:xsi", "xsi=http://www.w3.org/2001/XMLSchema-instance")

            xmlwriter.WriteElementString("SchemaVersion", "1.2")

            xmlwriter.WriteStartElement("login_key")

            ' xmlwriter.WriteElementString("user_id", user_id)
            xmlwriter.WriteElementString("user_id", _EFaxSettings.EFax_UserID)
            'xmlwriter.WriteElementString("user_password", "glostream11")
            xmlwriter.WriteElementString("user_password", _EFaxSettings.EFax_UserPassword)
            xmlwriter.WriteEndElement()

            xmlwriter.WriteStartElement("single_fax_options")
            xmlwriter.WriteElementString("billing_code", _EFaxSettings.EFax_BillingCode)
            xmlwriter.WriteElementString("from_name", _EFaxSettings.EFax_FromName)
            xmlwriter.WriteElementString("tiff_image_flag", _EFaxSettings.EFax_Tiff_image_flag)
            xmlwriter.WriteElementString("resolution", _EFaxSettings.EFax_Resolution)
            xmlwriter.WriteEndElement()

            xmlwriter.WriteStartElement("fax_recipient")
            'xmlwriter.WriteElementString("fax_recipient_number", "18668828796")
            xmlwriter.WriteElementString("fax_recipient_number", _EFaxSettings.EFax_FaxRecipientNumber)
            xmlwriter.WriteElementString("fax_recipient_name", _EFaxSettings.EFax_FaxRecipientName)
            xmlwriter.WriteEndElement()
            xmlwriter.WriteStartElement("document_list")

            '  ----------------------------------------------------------------------------------------------
            '' For Adding FaxCoverPage to a fax file if it exists
            ' ----------------------------------------------------------------------------------------------
            If _EFaxSettings.EFax_FaxCoverPage = True Then
                If _EFaxSettings.EFax_FaxCoverpagefilepath <> "" Then
                    xmlwriter.WriteStartElement("document")
                    xmlwriter.WriteAttributeString("document_content_type", "application/msword")
                    xmlwriter.WriteAttributeString("document_encoding_type", "base64")
                    xmlwriter.WriteAttributeString("document_extension", "docx")
                    '--------------------------------------------------------
                    'here u can write the fax cover page
                    '    xmlwriter.WriteString("Hi myFax this is sample Fax")
                    UpdateLogForFax("Generating BinaryStream")
                    Dim oByteFaxCoverPage As Byte() = GenerateBinaryStream(_EFaxSettings.EFax_FaxCoverpagefilepath)
                    UpdateLogForFax("Generating BinaryStream finished")
                    UpdateLogForFax("Generating Base64String")
                    Dim strFaxCoverPageData As String = Convert.ToBase64String(oByteFaxCoverPage)
                    UpdateLogForFax("Generating Base64String finished")
                    xmlwriter.WriteString(strFaxCoverPageData)
                    _EFaxSettings.FaxCoverPageBinaryData = oByteFaxCoverPage

                    '--------------------------------------------------------
                    xmlwriter.WriteEndElement() 'End Document
                    ' ----------------------------------------------------------------------------------------------
                    ' ----------------------------------------------------------------------------------------------
                End If
            End If
            '  ----------------------------------------------------------------------------------------------
            '' For Adding FaxCoverPage to a fax file if it exists
            ' ----------------------------------------------------------------------------------------------

            xmlwriter.WriteStartElement("document")
            '    xmlwriter.WriteAttributeString("document_content_type", "application/msword")
            xmlwriter.WriteAttributeString("document_content_type", _EFaxSettings.EFax_DocumentContentType)
            xmlwriter.WriteAttributeString("document_encoding_type", _EFaxSettings.EFax_DocumentEncodingType)
            '   xmlwriter.WriteAttributeString("document_extension", "docx")
            xmlwriter.WriteAttributeString("document_extension", _EFaxSettings.EFax_DocumentExtension)

            'Dim oByte As Byte() = GenerateBinaryStream(strfilepath1)
            UpdateLogForFax("Generating BinaryStream")
            Dim oByte As Byte() = GenerateBinaryStream(_EFaxSettings.EFax_Faxfilepath)
            UpdateLogForFax("Generating BinaryStream finished")
            UpdateLogForFax("Generating Base64String")
            Dim strdata As String = Convert.ToBase64String(oByte)
            UpdateLogForFax("Generating Base64String finished")
            xmlwriter.WriteString(strdata)
            _EFaxSettings.FaxFileBinaryData = oByte

            xmlwriter.WriteEndElement() 'End Document
            xmlwriter.WriteEndElement() 'End document_list
            xmlwriter.WriteEndElement() 'End single_fax_info
            xmlwriter.WriteEndDocument() 'End Document
            xmlwriter.Close()            'Close xml file

            UpdateLogForFax("Generating Base64String finished")
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
            'Throw ex
        End Try
    End Function
#End Region



End Class
