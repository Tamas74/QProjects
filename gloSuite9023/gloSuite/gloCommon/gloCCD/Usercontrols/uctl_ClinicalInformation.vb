Imports System.IO
Imports System.Data.SqlClient
Imports gloSettings
Imports gloGlobal

Public Class uctl_ClinicalInformation
    Private mPatient As Patient
    Private mEffectiveTime As String = ""
    'Private NewDocumentName As String = ""
    Private mFileType As String = ""
    Private CCDData As String = ""

    Public Sub SetCCDData(ByVal strCCDFilePath As String)
        rtbClinicalInformation.LoadFile(strCCDFilePath)
    End Sub
    Public Sub New(ByVal oPatient As Patient, ByVal strEffectiveTime As String, ByVal sDocType As String)
        mPatient = oPatient
        mEffectiveTime = strEffectiveTime
        mFileType = sDocType
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' BuildClinicalInformationResult()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()


    End Sub
    Public Function SaveCCD(ByVal sCCDXMLFilePath As String, ByVal nImportUser As Long, ByVal sSource As String, ByVal dtCreatedDate As String, ByVal _strNonXML As String) As Int64
        Dim _CCDID As Int64
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim strFilename As String = Nothing
        Dim arrByte As Byte() = Nothing
        Dim XMLarrByte As Byte() = Nothing
        Dim _fileHashValue As String = ""
        Dim _fileHashAlgorithmType As String = ""

        Try
            If _strNonXML <> "" Then
                Return Nothing
            End If
            strFilename = CCDFile(gloSettings.FolderSettings.AppTempFolderPath, ".rtf")
            If File.Exists(strFilename) Then
                File.Delete(strFilename)
            End If

            rtbClinicalInformation.SaveFile(strFilename)
            arrByte = ConvertFiletoBinary(strFilename)
            XMLarrByte = ConvertFiletoBinary(sCCDXMLFilePath)

            cmd = New SqlCommand("CCD_InsertFile", conn)
            cmd.CommandType = CommandType.StoredProcedure

            'Code Start-Added by kanchan on 20100608 for CCD unique id
            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0
            'Code Start-Added by kanchan on 20100608 for CCD unique id

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mPatient.PatientName.ID

            sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = mPatient.PatientName.FirstName
            sqlParam.Value = mPatient.PatientDemographics.DemographicsDetail.PatientFirstName

            sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = mPatient.PatientName.LastName
            sqlParam.Value = mPatient.PatientDemographics.DemographicsDetail.PatientLastName

            sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            'Changes done by kanchan on 20100616,insert current datetime in this field not document datetime
            'sqlParam.Value = mEffectiveTime
            sqlParam.Value = DateTime.Now

            sqlParam = cmd.Parameters.Add("@iData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = arrByte

            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Path.GetFileName(strFilename) ' NewDocumentName

            'Code Start-Added by kanchan on 20100603 for CCD changes
            If mPatient.PatientDemographics.DemographicsDetail.PatientDOB <> "12:00:00 AM" Then
                sqlParam = cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = mPatient.DateofBirth
                sqlParam.Value = mPatient.PatientDemographics.DemographicsDetail.PatientDOB
            End If


            sqlParam = cmd.Parameters.Add("@iXMLData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = XMLarrByte
            'Code End-Added by kanchan on 20100603 for CCD changes

            'Code Start-Added by kanchan on 20100611 for CCR/CCD changes
            sqlParam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mFileType

            'Code Start-Added by kanchan on 20100616 for Export CCD View option
            sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            'Code Start-Added by kanchan on 20101028 for generate hash value

            _fileHashValue = gloSecurity.gloDataHashing.GetSHA1Hash(sCCDXMLFilePath, _fileHashAlgorithmType)

            sqlParam = cmd.Parameters.Add("@sHashValue", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashValue

            sqlParam = cmd.Parameters.Add("@sHashAlgoType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashAlgorithmType
            'Code End-Added by kanchan on 20101028 for generate hash value


            sqlParam = cmd.Parameters.Add("@nImportUserId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nImportUser


            sqlParam = cmd.Parameters.Add("@nStatus", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CCDFileStatus.Imported


            sqlParam = cmd.Parameters.Add("@sSource", SqlDbType.VarChar, 150)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = mPatient.PatientName.LastName
            sqlParam.Value = sSource


            sqlParam = cmd.Parameters.Add("@dtCreatedDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = mPatient.PatientName.LastName
            sqlParam.Value = Convert.ToDateTime(dtCreatedDate.ToString())


            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()
            _CCDID = DirectCast(cmd.Parameters(0).Value, Int64)
            Return _CCDID

        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If

            strFilename = Nothing
            arrByte = Nothing
            XMLarrByte = Nothing
            _fileHashValue = Nothing
            _fileHashAlgorithmType = Nothing

        End Try
    End Function
    
    Private Function ConvertFiletoBinary(ByVal strFileName As String) As Byte()
        'If File.Exists(strFileName) Then
        '    Dim ofile As FileStream
        '    ofile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

        '    Dim Br As BinaryReader
        '    Br = New BinaryReader(ofile)
        '    Dim bytesRead As Byte() = Br.ReadBytes(ofile.Length)
        '    Return bytesRead
        'Else
        '    Return Nothing
        'End If
        If File.Exists(strFileName) Then
            Dim oFile As FileStream = Nothing
            Dim oReader As BinaryReader = Nothing
            Try
                ''Please uncomment the following line of code to read the file, even the file is in use by same or another process
                'oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                ''To read the file only when it is not in use by any process
                oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

                oReader = New BinaryReader(oFile)
                Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                Return bytesRead

            Catch ex As IOException
                Throw New gloCCDException(ex.tostring)
            Catch ex As Exception
                Throw New gloCCDException(ex.ToString)
            Finally
                If Not IsNothing(oFile) Then
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                End If
                If Not IsNothing(oReader) Then
                    oReader.Close()
                    oReader.Dispose()
                    oReader = Nothing
                End If

            End Try

        Else
            Return Nothing
        End If
    End Function
    Private ReadOnly Property CCDFile(ByVal _path As String, ByVal _extension As String) As String
        Get
            'NewDocumentName = ""
            '' Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
            'Dim _NewDocumentName As String = ""
            '' Dim _Extension As String = _extension
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _extension
            ''While File.Exists(_path & "\" & _NewDocumentName) = True
            'While File.Exists(_path & _NewDocumentName) = True And i < Integer.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _extension
            'End While
            '' Return _path & "\" & _NewDocumentName
            'NewDocumentName = _NewDocumentName
            'Return _path & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(_path, _extension, "MMddyyyyHHmmssffff")
        End Get
    End Property
    Private Sub BuildClinicalInformationResult() ' As String
        Dim strResult As New System.Text.StringBuilder
        Try
            'rtbClinicalInformation.ForeColor = Drawing.Color.Blue

            If Not IsNothing(mPatient.PatientName.Prefix) Then
                If mPatient.PatientName.Prefix <> "" Then
                    strResult.Append(mPatient.PatientName.Prefix & " ")
                End If
            End If

            strResult.Append(mPatient.PatientName.FirstName & " " & mPatient.PatientName.LastName)

            If Not IsNothing(mPatient.PatientName.Suffix) Then
                If mPatient.PatientName.Suffix <> "" Then
                    strResult.Append(" " & mPatient.PatientName.Suffix)
                End If
            End If

            rtbClinicalInformation.Text = "  Clinical Information for " & strResult.ToString

            strResult.Remove(0, strResult.Length)

            strResult.Append(vbCrLf & vbCrLf & "  Document TimeStamp: ")
            If Not IsNothing(mEffectiveTime) Then
                If mEffectiveTime <> "" Then
                    strResult.Append(mEffectiveTime)
                End If
            End If

            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)

            'rtbClinicalInformation.ForeColor = Drawing.Color.Chocolate
            strResult.Append(vbCrLf & vbCrLf & "  Person Identifier: ")
            If Not IsNothing(mPatient.PatientName.Code) Then
                If mPatient.PatientName.Code <> "" Then
                    strResult.Append(mPatient.PatientName.Code)
                End If
            End If

            strResult.Append(vbCrLf & vbTab & vbTab & "  Gender: ")
            If Not IsNothing(mPatient.Gender) Then
                If mPatient.Gender <> "" Then
                    Select Case mPatient.Gender
                        Case "F"
                        Case "Female"
                            mPatient.Gender = "Female"
                        Case "M"
                        Case "Male"
                            mPatient.Gender = "Male"
                        Case Else
                            mPatient.Gender = "Other"
                    End Select
                    strResult.Append(mPatient.Gender)
                End If
            End If

            strResult.Append(vbTab & vbTab & vbTab & vbTab & "  Date of Birth: ")
            If Not IsNothing(mPatient.DateofBirth) Then
                If mPatient.DateofBirth > "" Then
                    strResult.Append(mPatient.DateofBirth)
                End If
            End If
            strResult.Append(vbCrLf & vbTab & vbTab & "  Religion: ")
            If Not IsNothing(mPatient.ReligiousAffiliationCode) Then
                If mPatient.ReligiousAffiliationCode <> "" Then
                    strResult.Append(mPatient.ReligiousAffiliationCode)
                End If
            End If

            strResult.Append(vbTab & vbTab & vbTab & "  MaritalStatus: ")
            If Not IsNothing(mPatient.MaritalStatus) Then
                If mPatient.MaritalStatus <> "" Then
                    strResult.Append(mPatient.MaritalStatus)
                End If
            End If
            strResult.Append(vbCrLf & vbTab & vbTab & "  Ethnicity: ")
            If Not IsNothing(mPatient.ethnicGroupCode) Then
                If mPatient.ethnicGroupCode <> "" Then
                    strResult.Append(mPatient.ethnicGroupCode)
                End If
            End If

            strResult.Append(vbTab & "  Race: ")

            If Not IsNothing(mPatient.RaceCode) Then
                If mPatient.RaceCode <> "" Then
                    strResult.Append(mPatient.RaceCode)
                End If
            End If


            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)


            strResult.Append(vbCrLf & vbCrLf & "  Address")

            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Street) Then
                If mPatient.PatientName.PersonContactAddress.Street <> "" Then
                    strResult.Append(vbCrLf & vbTab & vbTab & mPatient.PatientName.PersonContactAddress.Street)
                End If
            End If
            'strResult.Append(vbCrLf & vbCrLf & vbTab & vbTab & "  City: ")
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.City) Then
                If mPatient.PatientName.PersonContactAddress.City <> "" Then
                    strResult.Append(vbCrLf & vbTab & vbTab & mPatient.PatientName.PersonContactAddress.City)
                End If
            End If
            'strResult.Append(vbTab & vbTab & "  State: ")
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.State) Then
                If mPatient.PatientName.PersonContactAddress.State <> "" Then
                    strResult.Append(vbCrLf & vbTab & vbTab & mPatient.PatientName.PersonContactAddress.State)
                End If
            End If
            'strResult.Append(vbCrLf & vbCrLf & "  Zip: ")
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Zip) Then
                If mPatient.PatientName.PersonContactAddress.Zip <> "" Then
                    strResult.Append(vbCrLf & vbTab & vbTab & mPatient.PatientName.PersonContactAddress.Zip)
                End If
            End If

            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)

            strResult.Append(vbCrLf & vbCrLf & "  Contact")
            strResult.Append(vbCrLf & vbTab & vbTab & "  Email: ")
            If Not IsNothing(mPatient.PatientName.PersonContactPhone.Email) Then
                If mPatient.PatientName.PersonContactPhone.Email <> "" Then
                    mPatient.PatientName.PersonContactPhone.Email = Replace(mPatient.PatientName.PersonContactPhone.Email, "mailto:", "")
                    strResult.Append(mPatient.PatientName.PersonContactPhone.Email)
                End If
            End If

            strResult.Append(vbCrLf & vbTab & vbTab & "  Home Phone: ")
            If Not IsNothing(mPatient.PatientName.PersonContactPhone.Phone) Then
                If mPatient.PatientName.PersonContactPhone.Phone <> "" Then
                    mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, "tel:", "")
                    strResult.Append(mPatient.PatientName.PersonContactPhone.Phone)
                End If
            End If
            strResult.Append(vbCrLf & vbTab & vbTab & "  Mobile Phone: ")
            If Not IsNothing(mPatient.PatientName.PersonContactPhone.Mobile) Then
                If mPatient.PatientName.PersonContactPhone.Mobile <> "" Then
                    mPatient.PatientName.PersonContactPhone.Mobile = Replace(mPatient.PatientName.PersonContactPhone.Mobile, "tel:", "")
                    strResult.Append(mPatient.PatientName.PersonContactPhone.Mobile)
                End If
            End If
            strResult.Append(vbCrLf & vbTab & vbTab & "  Work Phone: ")
            If Not IsNothing(mPatient.PatientName.PersonContactPhone.WorkPhone) Then
                If mPatient.PatientName.PersonContactPhone.WorkPhone <> "" Then
                    mPatient.PatientName.PersonContactPhone.WorkPhone = Replace(mPatient.PatientName.PersonContactPhone.WorkPhone, "tel:", "")
                    strResult.Append(mPatient.PatientName.PersonContactPhone.WorkPhone)
                End If
            End If

            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)
            Dim icnt As Int32 = 0

            If Not IsNothing(mPatient.PatientLanguages) Then
                If mPatient.PatientLanguages.Count > 0 Then
                    strResult.Append(vbCrLf & vbCrLf & "  Language ")
                    For Each oLanguage As Language In mPatient.PatientLanguages
                        icnt = icnt + 1
                        'strResult.Append(vbCrLf & vbCrLf & "  Language: ")
                        If Not IsNothing(oLanguage.Language) Then
                            If oLanguage.Language <> "" Then

                                strResult.Append(vbCrLf & vbTab & vbTab & icnt.ToString & ". " & oLanguage.Language)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Country: ")
                        If Not IsNothing(oLanguage.Country) Then
                            If oLanguage.Country <> "" Then
                                strResult.Append(oLanguage.Country)
                            End If
                        End If


                        strResult.Append(vbCrLf & vbTab & vbTab & "  Mode: ")
                        If Not IsNothing(oLanguage.Mode) Then
                            If oLanguage.Mode <> "" Then
                                strResult.Append(oLanguage.Mode)
                            End If
                        End If

                        strResult.Append(vbCrLf & vbTab & vbTab & "  Preferred: ")
                        If Not IsNothing(oLanguage.Preferred) Then
                            If oLanguage.Preferred <> "" Then
                                strResult.Append(oLanguage.Preferred)
                            End If
                        End If
                    Next

                End If
            End If
            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)

            icnt = 0

            If Not IsNothing(mPatient.PatientAllergies) Then
                If mPatient.PatientAllergies.Count > 0 Then
                    strResult.Append(vbCrLf & vbCrLf & "  Allergies ")
                    For Each oAllergies As Allergies In mPatient.PatientAllergies
                        'strResult.Append("  Product Name: ")
                        icnt = icnt + 1
                        If Not IsNothing(oAllergies.ProductName) Then
                            If oAllergies.ProductName <> "" Then
                                strResult.Append(vbCrLf & vbTab & vbTab & icnt.ToString & ". " & oAllergies.ProductName)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  ProductCode: ")
                        If Not IsNothing(oAllergies.ProductCode) Then
                            If oAllergies.ProductCode <> "" Then
                                strResult.Append(oAllergies.ProductCode)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Allergy Type: ")
                        If Not IsNothing(oAllergies.AllergyType) Then
                            If oAllergies.AllergyType <> "" Then
                                strResult.Append(oAllergies.AllergyType)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Start Time: ")
                        If Not IsNothing(oAllergies.EffectiveStartTime) Then
                            If oAllergies.EffectiveStartTime <> "" Then
                                strResult.Append(oAllergies.EffectiveStartTime)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  End Time: ")
                        If Not IsNothing(oAllergies.EffectiveEndTime) Then
                            If oAllergies.EffectiveEndTime <> "" Then
                                strResult.Append(oAllergies.EffectiveEndTime)
                            End If
                        End If
                    Next
                Else
                    strResult.Append(vbCrLf & vbCrLf)
                    strResult.Append("Allergies not found for the patient")
                End If
            Else
                strResult.Append(vbCrLf & vbCrLf)
                strResult.Append("Allergies not found for the patient")
            End If
            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)
            icnt = 0

            If Not IsNothing(mPatient.PatientMedications) Then
                If mPatient.PatientMedications.Count > 0 Then
                    strResult.Append(vbCrLf & vbCrLf & "  Medication ")
                    For Each oMedication As Medication In mPatient.PatientMedications
                        icnt = icnt + 1
                        'strResult.Append(vbTab & vbTab & "  Product Name: ")
                        If Not IsNothing(oMedication.DrugName) Then
                            If oMedication.DrugName <> "" Then
                                strResult.Append(vbCrLf & vbTab & vbTab & icnt.ToString & ". " & oMedication.DrugName)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Product Code: ")
                        If Not IsNothing(oMedication.ProdCode) Then
                            If oMedication.ProdCode <> "" Then
                                strResult.Append(oMedication.ProdCode)
                            End If
                        End If

                        strResult.Append(vbCrLf & vbTab & vbTab & "  CodeSystem: ")
                        If Not IsNothing(oMedication.CodingSystem) Then
                            If oMedication.CodingSystem <> "" Then
                                strResult.Append(oMedication.CodingSystem)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  FreeTextBrandName: ")
                        If Not IsNothing(oMedication.FreeTextBrandName) Then
                            If oMedication.FreeTextBrandName <> "" Then
                                strResult.Append(oMedication.FreeTextBrandName)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  MedicationType: ")
                        If Not IsNothing(oMedication.MedicationType) Then
                            If oMedication.MedicationType <> "" Then
                                strResult.Append(oMedication.MedicationType)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Status: ")
                        If Not IsNothing(oMedication.Status) Then
                            If oMedication.Status <> "" Then
                                strResult.Append(oMedication.Status)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Quantity: ")
                        If Not IsNothing(oMedication.DrugStrength) Then
                            If oMedication.DrugStrength <> "" Then
                                strResult.Append(oMedication.DrugStrength)
                            End If
                        End If
                        strResult.Append(vbCrLf & vbTab & vbTab & "  Unit: ")
                        If Not IsNothing(oMedication.StrengthUnits) Then
                            If oMedication.StrengthUnits <> "" Then
                                strResult.Append(oMedication.StrengthUnits)
                            End If
                        End If
                    Next
                Else
                    strResult.Append(vbCrLf & vbCrLf)
                    strResult.Append("Medication not found for the patient")
                End If
            Else
                strResult.Append(vbCrLf & vbCrLf)
                strResult.Append("Medication not found for the patient")
            End If

            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            strResult.Remove(0, strResult.Length)
            icnt = 0
            If Not IsNothing(mPatient.Author) Then

                strResult.Append(vbCrLf & vbCrLf & "  Patient Information Source ")
                strResult.Append(vbCrLf & vbTab & vbTab & "  Author Name: ")
                If Not IsNothing(mPatient.Author.PersonName.Prefix) Then
                    If mPatient.Author.PersonName.Prefix <> "" Then
                        strResult.Append(mPatient.Author.PersonName.Prefix & " ")
                    End If
                End If
                strResult.Append(mPatient.Author.PersonName.FirstName & " " & mPatient.Author.PersonName.LastName)

                If Not IsNothing(mPatient.Author.PersonName.Suffix) Then
                    If mPatient.Author.PersonName.Suffix <> "" Then
                        strResult.Append(mPatient.Author.PersonName.Suffix & " ")
                    End If
                End If
                strResult.Append(vbCrLf & vbTab & vbTab & "  Organization Name: ")
                If Not IsNothing(mPatient.Author.Organization) Then
                    If mPatient.Author.Organization <> "" Then
                        strResult.Append(mPatient.Author.Organization)
                    End If
                End If
                strResult.Append(vbCrLf & vbTab & vbTab & "  Document ID: ")
                If Not IsNothing(mPatient.Author.DocumentId) Then
                    If mPatient.Author.DocumentId <> "" Then
                        strResult.Append(mPatient.Author.DocumentId)
                    End If
                End If
            End If
            rtbClinicalInformation.Text = rtbClinicalInformation.Text & strResult.ToString
            CCDData = strResult.ToString
            strResult.Remove(0, strResult.Length)
            'Return strResult.ToString

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        End Try
    End Sub




    'Private Function BuildClinicalInformationResult() As String
    '    Dim strResult As New System.Text.StringBuilder
    '    Try
    '        rtbClinicalInformation.ForeColor = Drawing.Color.Blue
    '        rtbClinicalInformation.Text = "Clinical Information for "
    '        If Not IsNothing(mPatient.PatientName.Prefix) Then
    '            If mPatient.PatientName.Prefix <> "" Then
    '                strResult.Append(mPatient.PatientName.Prefix & " ")
    '            End If
    '        End If
    '        If Not IsNothing(mEffectiveTime) Then
    '            If mEffectiveTime <> "" Then
    '                strResult.Append(vbCrLf & vbCrLf & "  Document TimeStamp:")
    '                strResult.Append(mEffectiveTime)
    '            End If
    '        End If

    '        strResult.Append("  Patient Details")
    '        strResult.Append(vbCrLf & vbCrLf & "  Person Identifier:")
    '        If Not IsNothing(mPatient.PatientName.Code) Then
    '            If mPatient.PatientName.Code <> "" Then
    '                strResult.Append(mPatient.PatientName.Code & " ")
    '            End If
    '        End If

    '        strResult.Append(vbCrLf & vbCrLf & "  Person Name:")


    '        strResult.Append(mPatient.PatientName.FirstName & " " & mPatient.PatientName.LastName)

    '        If Not IsNothing(mPatient.PatientName.Suffix) Then
    '            If mPatient.PatientName.Suffix <> "" Then
    '                strResult.Append(" " & mPatient.PatientName.Suffix)
    '            End If
    '        End If

    '        strResult.Append(vbCrLf & vbCrLf & "   Gender: ")
    '        If Not IsNothing(mPatient.Gender) Then
    '            If mPatient.Gender <> "" Then
    '                Select Case mPatient.Gender
    '                    Case "F"
    '                        mPatient.Gender = "Female"
    '                    Case "M"
    '                        mPatient.Gender = "Male"
    '                    Case Else
    '                        mPatient.Gender = "Other"
    '                End Select
    '                strResult.Append(mPatient.Gender)
    '            End If
    '        End If
    '        strResult.Append(vbTab & vbTab & "   Date of Birth: ")
    '        If Not IsNothing(mPatient.DateofBirth) Then
    '            If mPatient.DateofBirth > "" Then
    '                strResult.Append(mPatient.DateofBirth)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  MaritalStatus: ")
    '        If Not IsNothing(mPatient.MaritalStatus) Then
    '            If mPatient.MaritalStatus <> "" Then
    '                strResult.Append(mPatient.MaritalStatus)
    '            End If
    '        End If
    '        strResult.Append(vbTab & vbTab & "  Religion: ")
    '        If Not IsNothing(mPatient.ReligiousAffiliationCode) Then
    '            If mPatient.ReligiousAffiliationCode <> "" Then
    '                strResult.Append(mPatient.ReligiousAffiliationCode)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  Race: ")

    '        If Not IsNothing(mPatient.RaceCode) Then
    '            If mPatient.RaceCode <> "" Then
    '                strResult.Append(mPatient.RaceCode)
    '            End If
    '        End If
    '        strResult.Append(vbTab & vbTab & "  Ethnicity: ")
    '        If Not IsNothing(mPatient.ethnicGroupCode) Then
    '            If mPatient.ethnicGroupCode <> "" Then
    '                strResult.Append(mPatient.ethnicGroupCode)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  AddressLine: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactAddress.Street) Then
    '            If mPatient.PatientName.PersonContactAddress.Street <> "" Then
    '                strResult.Append(mPatient.PatientName.PersonContactAddress.Street)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  City: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactAddress.City) Then
    '            If mPatient.PatientName.PersonContactAddress.City <> "" Then
    '                strResult.Append(mPatient.PatientName.PersonContactAddress.City)
    '            End If
    '        End If
    '        strResult.Append(vbTab & vbTab & "  State: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactAddress.State) Then
    '            If mPatient.PatientName.PersonContactAddress.State <> "" Then
    '                strResult.Append(mPatient.PatientName.PersonContactAddress.State)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  Zip: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactAddress.Zip) Then
    '            If mPatient.PatientName.PersonContactAddress.Zip <> "" Then
    '                strResult.Append(mPatient.PatientName.PersonContactAddress.Zip)
    '            End If
    '        End If

    '        strResult.Append(vbTab & vbTab & "  Email: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactPhone.Email) Then
    '            If mPatient.PatientName.PersonContactPhone.Email <> "" Then

    '                mPatient.PatientName.PersonContactPhone.Email = Replace(mPatient.PatientName.PersonContactPhone.Email, "mailto:", "")
    '                strResult.Append(mPatient.PatientName.PersonContactPhone.Email)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  Home Phone: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactPhone.Phone) Then
    '            If mPatient.PatientName.PersonContactPhone.Phone <> "" Then
    '                mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, "tel:", "")
    '                strResult.Append(mPatient.PatientName.PersonContactPhone.Phone)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  Mobile Phone: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactPhone.Mobile) Then
    '            If mPatient.PatientName.PersonContactPhone.Mobile <> "" Then
    '                mPatient.PatientName.PersonContactPhone.Mobile = Replace(mPatient.PatientName.PersonContactPhone.Mobile, "tel:", "")
    '                strResult.Append(mPatient.PatientName.PersonContactPhone.Mobile)
    '            End If
    '        End If
    '        strResult.Append(vbCrLf & vbCrLf & "  Work Phone: ")
    '        If Not IsNothing(mPatient.PatientName.PersonContactPhone.WorkPhone) Then
    '            If mPatient.PatientName.PersonContactPhone.WorkPhone <> "" Then
    '                mPatient.PatientName.PersonContactPhone.WorkPhone = Replace(mPatient.PatientName.PersonContactPhone.WorkPhone, "tel:", "")
    '                strResult.Append(mPatient.PatientName.PersonContactPhone.WorkPhone)
    '            End If
    '        End If


    '        If Not IsNothing(mPatient.PatientLanguages) Then
    '            If mPatient.PatientLanguages.Count > 0 Then
    '                strResult.Append(vbCrLf & vbCrLf & "  Language Details: ")
    '                For Each oLanguage As Language In mPatient.PatientLanguages
    '                    strResult.Append(vbCrLf & vbCrLf & "  Language: ")
    '                    If Not IsNothing(oLanguage.Language) Then
    '                        If oLanguage.Language <> "" Then

    '                            strResult.Append(oLanguage.Language)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  Country: ")
    '                    If Not IsNothing(oLanguage.Country) Then
    '                        If oLanguage.Country <> "" Then
    '                            strResult.Append(oLanguage.Country)
    '                        End If
    '                    End If


    '                    strResult.Append(vbCrLf & "  Mode: ")
    '                    If Not IsNothing(oLanguage.Mode) Then
    '                        If oLanguage.Mode <> "" Then
    '                            strResult.Append(oLanguage.Mode)
    '                        End If
    '                    End If

    '                    strResult.Append(vbTab & vbTab & "  Preferred: ")
    '                    If Not IsNothing(oLanguage.Preferred) Then
    '                        If oLanguage.Preferred <> "" Then
    '                            strResult.Append(oLanguage.Preferred)
    '                        End If
    '                    End If
    '                Next

    '            End If
    '        End If

    '        If Not IsNothing(mPatient.PatientAllergies) Then
    '            If mPatient.PatientAllergies.Count > 0 Then
    '                strResult.Append(vbCrLf & vbCrLf & "  Allergies Details: ")
    '                For Each oAllergies As Allergies In mPatient.PatientAllergies
    '                    strResult.Append(vbCrLf & "  Product Name: ")
    '                    If Not IsNothing(oAllergies.ProductName) Then
    '                        If oAllergies.ProductName <> "" Then
    '                            strResult.Append(oAllergies.ProductName)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  ProductCode: ")
    '                    If Not IsNothing(oAllergies.ProductCode) Then
    '                        If oAllergies.ProductCode <> "" Then

    '                            strResult.Append(oAllergies.ProductCode)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  Allergy Type: ")
    '                    If Not IsNothing(oAllergies.AllergyType) Then
    '                        If oAllergies.AllergyType <> "" Then
    '                            strResult.Append(oAllergies.AllergyType)
    '                        End If
    '                    End If
    '                    strResult.Append(vbCrLf & "  Start Time: ")
    '                    If Not IsNothing(oAllergies.EffectiveStartTime) Then
    '                        If oAllergies.EffectiveStartTime <> "" Then
    '                            strResult.Append(oAllergies.EffectiveStartTime)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  End Time: ")
    '                    If Not IsNothing(oAllergies.EffectiveEndTime) Then
    '                        If oAllergies.EffectiveEndTime <> "" Then
    '                            strResult.Append(oAllergies.EffectiveEndTime)
    '                        End If
    '                    End If
    '                Next

    '            End If
    '        End If

    '        If Not IsNothing(mPatient.PatientMedications) Then
    '            If mPatient.PatientMedications.Count > 0 Then
    '                strResult.Append(vbCrLf & vbCrLf & "  Medication Details: ")
    '                For Each oMedication As Medication In mPatient.PatientMedications
    '                    strResult.Append(vbCrLf & "  Product Code: ")
    '                    If Not IsNothing(oMedication.ProdCode) Then
    '                        If oMedication.ProdCode <> "" Then
    '                            strResult.Append(oMedication.ProdCode)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  Product Name: ")
    '                    If Not IsNothing(oMedication.DrugName) Then
    '                        If oMedication.DrugName <> "" Then
    '                            strResult.Append(oMedication.DrugName)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  CodeSystem: ")
    '                    If Not IsNothing(oMedication.CodingSystem) Then
    '                        If oMedication.CodingSystem <> "" Then
    '                            strResult.Append(oMedication.CodingSystem)
    '                        End If
    '                    End If
    '                    strResult.Append(vbCrLf & "  FreeTextBrandName: ")
    '                    If Not IsNothing(oMedication.FreeTextBrandName) Then
    '                        If oMedication.FreeTextBrandName <> "" Then
    '                            strResult.Append(oMedication.FreeTextBrandName)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  MedicationType: ")
    '                    If Not IsNothing(oMedication.MedicationType) Then
    '                        If oMedication.MedicationType <> "" Then
    '                            strResult.Append(oMedication.MedicationType)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  Status: ")
    '                    If Not IsNothing(oMedication.Status) Then
    '                        If oMedication.Status <> "" Then
    '                            strResult.Append(oMedication.Status)
    '                        End If
    '                    End If
    '                    strResult.Append(vbCrLf & "  Quantity: ")
    '                    If Not IsNothing(oMedication.DrugStrength) Then
    '                        If oMedication.DrugStrength <> "" Then
    '                            strResult.Append(oMedication.DrugStrength)
    '                        End If
    '                    End If
    '                    strResult.Append(vbTab & vbTab & "  Unit: ")
    '                    If Not IsNothing(oMedication.StrengthUnits) Then
    '                        If oMedication.StrengthUnits <> "" Then
    '                            strResult.Append(oMedication.StrengthUnits)
    '                        End If
    '                    End If


    '                Next
    '            End If
    '        End If

    '        If Not IsNothing(mPatient.Author) Then

    '            strResult.Append(vbCrLf & vbCrLf & "  Patient Information Source: ")
    '            strResult.Append(vbCrLf & "  Author Name: ")
    '            If Not IsNothing(mPatient.Author.PersonName.Prefix) Then
    '                If mPatient.Author.PersonName.Prefix <> "" Then
    '                    strResult.Append(mPatient.Author.PersonName.Prefix & " ")
    '                End If
    '            End If
    '            strResult.Append(mPatient.Author.PersonName.FirstName & " " & mPatient.Author.PersonName.LastName)

    '            If Not IsNothing(mPatient.Author.PersonName.Suffix) Then
    '                If mPatient.Author.PersonName.Suffix <> "" Then
    '                    strResult.Append(mPatient.Author.PersonName.Suffix & " ")
    '                End If
    '            End If
    '            strResult.Append(vbCrLf & "  Organization Name: ")
    '            If Not IsNothing(mPatient.Author.Organization) Then
    '                If mPatient.Author.Organization <> "" Then
    '                    strResult.Append(mPatient.Author.Organization)
    '                End If
    '            End If
    '            strResult.Append(vbTab & vbTab & "  Document ID: ")
    '            If Not IsNothing(mPatient.Author.DocumentId) Then
    '                If mPatient.Author.DocumentId <> "" Then
    '                    strResult.Append(mPatient.Author.DocumentId)
    '                End If
    '            End If
    '        End If
    '        Return strResult.ToString

    '    Catch ex As gloCCDException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw New gloCCDException(ex.ToString)
    '    End Try
    'End Function

End Class
