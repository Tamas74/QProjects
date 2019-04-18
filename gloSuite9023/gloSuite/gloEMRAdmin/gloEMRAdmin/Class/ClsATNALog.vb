Imports System.Xml
Imports System.Data.SqlClient
Imports System.Net

Public Class ClsATNALog
    Public Function GenerateFileName() As String
        Dim strfilename As String = ""
        strfilename = "ATNA_"
        Dim dtdate As DateTime = Date.UtcNow
        Dim strtemp As String = strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
       
        strfilename = gloSettings.FolderSettings.AppTempFolderPath & strtemp & ".xml"

        Return strfilename
    End Function
    Public Function CreateATNALogXMLFILE(ByVal dt As DataTable) As String
        Dim strfilepath As String = ""
        Dim localMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.LocalMachineDetails()
        Try
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

                strfilepath = GenerateFileName()

                Dim xmlwriter As XmlTextWriter = Nothing
                If System.IO.File.Exists(strfilepath) Then
                    System.IO.File.Delete(strfilepath)
                End If
                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
                xmlwriter.Formatting = Formatting.Indented

                xmlwriter.WriteStartDocument()
                'If Not System.IO.File.Exists(Application.StartupPath & "/arr-details-atna.xsl") Then
                '    System.IO.File.Copy(Application.StartupPath & "/arr-details-atna.xsl", gloSettings.FolderSettings.AppTempFolderPath & "\arr-details-atna.xsl", True)
                'End If

                Dim xslpath As String = "file:///" + System.IO.Path.Combine(Application.StartupPath, "arr-details-atna.xsl")
                xslpath = Replace(xslpath, "\", "/")
                Dim xsdPath As String = "file:///" + System.IO.Path.Combine(Application.StartupPath, "healthcare-security-audit.xsd")
                xsdPath = Replace(xsdPath, "\", "/")
                'Dim _myStyle As String = "type='text/xsl' href='file://D:/arr-details-atna.xsl'"
                Dim _myStyle As String = "type='text/xsl' href= '" & xslpath & "'"

                xmlwriter.WriteProcessingInstruction("xml-stylesheet", _myStyle)
                xmlwriter.WriteStartElement("AuditMessage")

                '  xmlwriter.WriteAttributeString("xsi:SchemaLocation", "http://www.glostream.com/css/XSLT/ATNA.xsd")
                xmlwriter.WriteAttributeString("xsi:SchemaLocation", xsdPath)

                ' xmlwriter.WriteAttributeString("xmlns:sdtc", "urn:hl7-org:sdtc")
                xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                'xmlwriter.WriteAttributeString("xmlns", "urn:hl7-org:v3")

                xmlwriter.WriteStartElement("EventIdentification")
                xmlwriter.WriteAttributeString("EventActionCode", "R")
                Dim _Date As String
                If Not IsNothing(dt.Rows(0)("dtActivityDateTime")) Then
                    _Date = Format(CDate(dt.Rows(0)("dtActivityDateTime")), "yyyy-MM-ddTHH:mm:ss")
                Else
                    _Date = Format(CDate(Now), "yyyy-MM-ddTHH:mm:ss")
                End If

                xmlwriter.WriteAttributeString("EventDateTime", _Date)
                xmlwriter.WriteAttributeString("EventOutcomeIndicator", "0")

                xmlwriter.WriteStartElement("EventID")
                xmlwriter.WriteAttributeString("code", "110110")
                xmlwriter.WriteAttributeString("displayName", "Patient Record")
                xmlwriter.WriteAttributeString("codeSystemName", "DCM")
                xmlwriter.WriteEndElement() 'End EventID Element

                xmlwriter.WriteStartElement("EventTypeCode")
                xmlwriter.WriteAttributeString("code", "ITI-42")
                xmlwriter.WriteAttributeString("codeSystemName", "IHE Transactions")
                xmlwriter.WriteAttributeString("displayName", "Register Document Set-b")
                xmlwriter.WriteEndElement() 'End EventTypeCode Element

                xmlwriter.WriteEndElement() 'End EventIdentification Element

                Dim _userID As Long
                If Not IsNothing(dt.Rows(0)("UserID")) Then
                    _userID = dt.Rows(0)("UserID")
                Else
                    _userID = 1
                End If
                xmlwriter.WriteStartElement("ActiveParticipant")
                xmlwriter.WriteAttributeString("UserID", _userID)
                xmlwriter.WriteAttributeString("UserIsRequestor", "True")

                xmlwriter.WriteStartElement("RoleIDCode")
                xmlwriter.WriteAttributeString("code", "110153")
                xmlwriter.WriteAttributeString("displayName", "Source")
                xmlwriter.WriteAttributeString("codeSystemName", "DCM")
                xmlwriter.WriteEndElement() 'End RoleIDCode Element

                xmlwriter.WriteEndElement() 'End ActiveParticipant Element

                xmlwriter.WriteStartElement("ActiveParticipant")
                xmlwriter.WriteAttributeString("UserID", _userID)
                xmlwriter.WriteAttributeString("UserIsRequestor", "False")

                xmlwriter.WriteStartElement("RoleIDCode")
                xmlwriter.WriteAttributeString("code", "110152")
                xmlwriter.WriteAttributeString("displayName", "Destination")
                xmlwriter.WriteAttributeString("codeSystemName", "DCM")
                xmlwriter.WriteEndElement() 'End RoleIDCode Element

                xmlwriter.WriteEndElement() 'End ActiveParticipant Element

                Dim ipEntry As IPHostEntry = Dns.GetHostByName(localMachine.MachineName)
                Dim IpAddr As IPAddress() = ipEntry.AddressList

                xmlwriter.WriteStartElement("AuditSourceIdentification")
                xmlwriter.WriteAttributeString("AuditSourceID", IpAddr(0).ToString())
                xmlwriter.WriteEndElement() 'End AuditSourceIdentification Element

                xmlwriter.WriteStartElement("ParticipantObjectIdentification")
                xmlwriter.WriteAttributeString("ParticipantObjectID", "1")
                xmlwriter.WriteAttributeString("ParticipantObjectName", "Admin")
                xmlwriter.WriteAttributeString("ParticipantObjectTypeCode", "2")
                xmlwriter.WriteAttributeString("ParticipantObjectTypeCodeRole", "20")

                xmlwriter.WriteStartElement("ParticipantObjectIDTypeCode")
                xmlwriter.WriteAttributeString("code", "2")
                xmlwriter.WriteEndElement() 'End ParticipantObjectIDTypeCode Element

                xmlwriter.WriteEndElement() 'End ParticipantObjectIdentification Element

                xmlwriter.WriteEndElement() 'End AuditMessage Element

                xmlwriter.WriteEndDocument()
                xmlwriter.Close()

            End If
            Return strfilepath
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return ""
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
            End If
        End Try
    End Function
    'Code Start-Added by kanchan on 20101115 for CCHIT
    Public Function GetDataTable(ByVal Sqlquery As String) As DataTable
        Dim sqlcon As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As New DataTable
        Dim sqldata As SqlDataAdapter
        Try
            sqlcon.ConnectionString = mdlGeneral.GetConnectionString()
            sqlcon.Open()
            cmd.Connection = sqlcon
            cmd.CommandText = Sqlquery
            sqldata = New SqlDataAdapter(cmd)
            sqldata.Fill(dt)

        Catch ex As Exception
            Return Nothing
        Finally
            sqlcon.Close()
        End Try
        Return dt
    End Function
End Class
