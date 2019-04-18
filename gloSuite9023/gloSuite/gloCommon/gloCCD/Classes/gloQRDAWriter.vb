Imports gloCCDSchema
Imports System.Windows.Forms
Partial Public Class gloCDAWriter
    Implements IDisposable
#Region "QRDAIII Constants"

    Dim _measurementStartDate As String
    Dim _measurementEndDate As String
    Dim _dtMeasures As DataTable
    Dim _dtCodes As DataTable
    Dim _dtStratum1 As DataTable
    Dim _dtStratum2 As DataTable
    Private Const Col_Measure_IPP1 As Integer = 0
    Private Const Col_Measure_Denom1 As Integer = 1
    Private Const Col_Measure_Num1 As Integer = 2
    Private Const Col_Measure_Num2 As Integer = 3
    Private Const Col_Measure_DenomExcl As Integer = 4
    Private Const Col_Measure_DenomException As Integer = 5
    Private Const Col_Measure_No As Integer = 6
    Private Const Col_Measure_Title As Integer = 7
    Private Const Col_Measure_NeutralID As Integer = 8
    Private Const Col_Measure_VersionNo As Integer = 9
    Private Const Col_Measure_CMSID As Integer = 10 ''MAT ''emesureid
    Private Const Col_Measure_VersionSpecID As Integer = 11
    Private Const Col_Measure_InitialPatientPopulation1ID As Integer = 12
    Private Const Col_Measure_IPP1Denominator1ID As Integer = 13
    Private Const Col_Measure_IPP1Numerator1ID As Integer = 14
    Private Const Col_Measure_IPP1Numerator2ID As Integer = 15
    Private Const Col_Measure_IPP1DenoExclusionID As Integer = 16
    Private Const Col_Measure_IPP1DenoExceptionID As Integer = 17
    Private Const Col_Measure_IPP1PerformaceRate As Integer = 18
    Private Const Col_Measure_IPP1ReportingRate As Integer = 19



    Private Const Col_Codes_Description As Integer = 0
    Private Const Col_Codes_Count As Integer = 1
    Private Const Col_Codes_DataCode As Integer = 2
    Private Const Col_Codes_TableType As Integer = 3
    Private Const Col_Codes_DataType As Integer = 4
    Private Const Col_Codes_MeasureNo As Integer = 5
    Private Const _Version2019 As String = "2017-08-01"

#End Region

#Region "QRDAIII Property Procedures"

    Public Property MeasurementStartDate() As String
        Get
            Return _measurementStartDate
        End Get
        Set(ByVal value As String)
            _measurementStartDate = value
        End Set
    End Property
    Public Property MeasurementEndDate() As String
        Get
            Return _measurementEndDate
        End Get
        Set(ByVal value As String)
            _measurementEndDate = value
        End Set
    End Property
    Public Property dtMeasures() As DataTable
        Get
            Return _dtMeasures
        End Get
        Set(ByVal value As DataTable)
            _dtMeasures = value
        End Set
    End Property
    Public Property dtCodes() As DataTable
        Get
            Return _dtCodes
        End Get
        Set(ByVal value As DataTable)
            _dtCodes = value
        End Set
    End Property
    Public Property dtStratum1() As DataTable
        Get
            Return _dtStratum1
        End Get
        Set(ByVal value As DataTable)
            _dtStratum1 = value
        End Set
    End Property
    Public Property dtStratum2() As DataTable
        Get
            Return _dtStratum2
        End Get
        Set(ByVal value As DataTable)
            _dtStratum2 = value
        End Set
    End Property
#End Region
#Region "QRDAI Constants"
    Private _DateExtension22 As String = "2014-06-09"
    Private _DateExtension24 As String = "2014-12-01"
    Private _DateExtension2015 As String = "2015-08-01"
    Private _DateExtension2016 As String = "2016-02-01"
    Public Shared _IsFromoldVersion As Boolean = False
    Private _DateExtension2016_1 As String = "2016-08-01"
    'Private _DateExtension2016_1 As String = Nothing
    Private _dtQRDA1Data As New DataTable

    ' Private _dtQRDA1EntryData As New DataTable
    Dim _msg As String = ""
    Dim _error As String = "Error while writing QRDA1 "

    Private Const Col_QRDA1_nPatientID As Integer = 0
    Private Const Col_QRDA1_TransactionID As Integer = 1
    Private Const Col_QRDA1_Category As Integer = 2

    Private Const Col_QRDA1_TransactionDate As Integer = 3
    Private Const Col_QRDA1_SDTCValueSet As Integer = 4
    Private Const Col_QRDA1_CodeDescription As Integer = 5
    Private Const Col_QRDA1_CodeValue As Integer = 6
    Private Const Col_QRDA1_ICD9 As Integer = 7
    Private Const Col_QRDA1_ConceptID As Integer = 8

    Private Const Col_QRDA1_RxNorm As Integer = 9
    Private Const Col_QRDA1_LOINC As Integer = 10
    Private Const Col_QRDA1_CVX As Integer = 11
    Private Const Col_QRDA1_BPSittingMax As Integer = 12

    Private Const Col_QRDA1_BPSittingMin As Integer = 13
    Private Const Col_QRDA1_ICD10 As Integer = 14
    Private Const Col_QRDA1_ReasonConcept As Integer = 17

#End Region
#Region "QRDAI Procedures"
    Public Property dtQRDA1Data() As DataTable
        Get
            Return _dtQRDA1Data
        End Get
        Set(ByVal value As DataTable)
            _dtQRDA1Data = value
        End Set
    End Property
    
    Public Property SelectedQRDAIFilePath() As String
        Get
            Return _SelectedQRDAIFilePath
        End Get
        Set(ByVal value As String)
            _SelectedQRDAIFilePath = value
        End Set
    End Property

#End Region
#Region "QRDAIII Public Method"
    Public Function GenerateQRDAIII(ByVal _FinalCDAFilePath As String, ByVal PatientLastName As String) As String
        Dim strfilepath As String = ""
        Dim QRDAIIIDoc As New POCD_MT000040UV02ClinicalDocument()
        oCodingSystemMaster = New CodeSystemMaster
        oTemplateIDMaster = New TemplateIDMaster

        Try

            '    Set Clinic Information - End 
            If _FinalCDAFilePath <> "" Then
                strfilepath = _FinalCDAFilePath
            Else
                strfilepath = GenerateFileName(PatientLastName)
            End If

            'Header
            QRDAIIIDoc = GetCDAInitialization()

            'recordTarget
            QRDAIIIDoc.recordTarget = New POCD_MT000040UV02RecordTarget(0) {}
            QRDAIIIDoc.recordTarget(0) = GetCDARecordTarget()

            'author
            QRDAIIIDoc.author = New POCD_MT000040UV02Author(0) {}
            QRDAIIIDoc.author(0) = GetCDAAuthor()

            'custodian
            QRDAIIIDoc.custodian = New POCD_MT000040UV02Custodian
            QRDAIIIDoc.custodian = GetCDACustodian()

            'informationRecipient
            QRDAIIIDoc.informationRecipient = New POCD_MT000040UV02Participant(0) {}
            QRDAIIIDoc.informationRecipient(0) = GetQRDAIIIinforecepient()

            ''legalAuthenticator
            QRDAIIIDoc.legalAuthenticator = New POCD_MT000040UV02LegalAuthenticator(0) {}
            QRDAIIIDoc.legalAuthenticator(0) = GetCDALegalAuthenticator()

            'documentationOf
            QRDAIIIDoc.documentationOf = New POCD_MT000040UV02DocumentationOf(0) {}
            QRDAIIIDoc.documentationOf(0) = GetCDAdocumentationOf()

            'QRDAIII Body
            QRDAIIIDoc.component = New POCD_MT000040UV02Component2
            QRDAIIIDoc.component = GetCDAComponent()








            Try
                gloSerialization.SetQRDADocument(strfilepath, QRDAIIIDoc)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
                ex = Nothing
                Return ""
            End Try
            If _msgString <> "" Then
                MessageBox.Show(_msgString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return ""
        Finally
            If Not IsNothing(QRDAIIIDoc.recordTarget(0)) Then
                QRDAIIIDoc.recordTarget(0) = Nothing
            End If
            If Not IsNothing(QRDAIIIDoc.author(0)) Then
                QRDAIIIDoc.author(0) = Nothing
            End If
            If Not IsNothing(QRDAIIIDoc.custodian) Then
                QRDAIIIDoc.custodian = Nothing
            End If
            If Not IsNothing(QRDAIIIDoc.component) Then
                QRDAIIIDoc.component = Nothing
            End If
            If Not IsNothing(QRDAIIIDoc) Then
                QRDAIIIDoc = Nothing
            End If
            If Not IsNothing(oCodingSystemMaster) Then
                oCodingSystemMaster.Dispose()
                oCodingSystemMaster = Nothing
            End If
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
                oTemplateIDMaster = Nothing
            End If

        End Try

        Return strfilepath
    End Function


#End Region
#Region "QRDAIII Private Methods"

    Private Function GetQRDAIIIReportingParamsComponent() As POCD_MT000040UV02Component3
        Dim _ReportingParams As POCD_MT000040UV02Component3 = Nothing
        Dim section As POCD_MT000040UV02Section = Nothing
        Dim list As StrucDocList = Nothing
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim encounter As POCD_MT000040UV02Encounter = Nothing
        Dim ob As POCD_MT000040UV02Observation = Nothing
        'Dim strDate As String = ""
        Try

            '   If Not IsNothing(mPatient.PatientProblems) AndAlso mPatient.PatientProblems.Count > 0 Then
            _ReportingParams = New POCD_MT000040UV02Component3
            _ReportingParams.typeCode = Nothing
            _ReportingParams.typeCodeSpecified = False
            DirectCast(_ReportingParams, POCD_MT000040UV02Component3).section = New POCD_MT000040UV02Section()
            section = DirectCast(_ReportingParams, POCD_MT000040UV02Component3).section
            'TempMayuri
            ''section.ID = Nothing
            section.classCode = Nothing
            section.classCodeSpecified = False
            section.moodCode = Nothing
            section.moodCodeSpecified = False
            If _nQRDAFileType = CDAFileTypeEnum.QRDA3 Then
                section.templateId = New II(1) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.17.2.1"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = Nothing
                ''commented for qrda3
                'section.templateId(1) = New II()
                'section.templateId(1).root = "2.16.840.1.113883.10.20.27.2.2"
                ''oTemplateIDMaster.GetSectionID("Problem")
                ''"2.16.840.1.113883.10.20.22.2.5.1"
                'section.templateId(1).assigningAuthorityName = Nothing
                'section.templateId(1).extension = Nothing

                ''As per Qualitynet standard
                'section.templateId(2) = New II()
                'section.templateId(2).root = "2.16.840.1.113883.10.20.27.2.6"
                ''oTemplateIDMaster.GetSectionID("Problem")
                ''"2.16.840.1.113883.10.20.22.2.5.1"
                'section.templateId(2).assigningAuthorityName = Nothing
                'section.templateId(2).extension = Nothing
                ''commented for qrda3 end 
            ElseIf _nQRDAFileType = CDAFileTypeEnum.QRDA1 Then
                section.templateId = New II(3) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.17.2.1"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = Nothing

                section.templateId(1) = New II()
                section.templateId(1).root = "2.16.840.1.113883.10.20.17.2.1"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(1).assigningAuthorityName = Nothing
                section.templateId(1).extension = "2015-07-01"

                section.templateId(2) = New II()
                section.templateId(2).root = "2.16.840.1.113883.10.20.27.2.2"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(2).assigningAuthorityName = Nothing
                section.templateId(2).extension = Nothing

                'As per Qualitynet standard
                section.templateId(3) = New II()
                section.templateId(3).root = "2.16.840.1.113883.10.20.27.2.6"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(3).assigningAuthorityName = Nothing
                section.templateId(3).extension = Nothing

            End If




            section.code = New CE()
            ' section.code.nullFlavor = "NA"
            section.code.code = "55187-9"
            'oCodingSystemMaster.GetbyDescription(CodeSystem.LOINC, "Problem List").Code
            '"11450-4"

            section.code.codeSystem = CodeSystem.LOINC
            section.code.codeSystemName = Nothing
            section.code.codeSystemVersion = Nothing
            section.code.displayName = Nothing
            'nothing
            section.title = New ST()
            section.title.Text = New String() {"Reporting Parameters"}
            'section.title.representation = New BinaryDataEncoding
            ' section.title.representation = Nothing
            section.title.mediaType = Nothing
            section.title.language = Nothing
            section.text = New StrucDocText()
            section.text.mediaType = Nothing
            section.text.ID = Nothing
            section.text.language = Nothing
            section.title.representation = Nothing
            section.text.Items = New Object(0) {}
            section.text.Items(0) = New StrucDocList
            list = DirectCast(section.text.Items(0), StrucDocList)
            list.ID = Nothing
            list.language = Nothing
            list.item = New StrucDocItem(0) {}

            list.item(0) = New StrucDocItem
            list.item(0).ID = Nothing
            list.item(0).language = Nothing
            list.item(0).caption = New StrucDocCaption
            list.item(0).caption.ID = Nothing
            list.item(0).caption.language = Nothing
            list.item(0).caption.Text = New String() {"Reporting Period:" & _measurementStartDate & " - " & _measurementEndDate}

            'list.item(1) = New StrucDocItem
            'list.item(1).caption = New StrucDocCaption
            'list.item(1).caption.ID = Nothing
            'list.item(1).caption.language = Nothing
            'list.item(1).ID = Nothing
            'list.item(1).language = Nothing
            'list.item(1).caption.Text = New String() {"First Encounter: " & _measurementStartDate}
            'list.item(2) = New StrucDocItem
            'list.item(2).ID = Nothing
            'list.item(2).language = Nothing
            'list.item(2).caption = New StrucDocCaption
            'list.item(2).caption.ID = Nothing
            'list.item(2).caption.language = Nothing
            'list.item(2).caption.Text = New String() {"Last Encounter: " & _measurementEndDate}



            section.entry = New POCD_MT000040UV02Entry(0) {}

            section.entry(0) = New POCD_MT000040UV02Entry()
            section.entry(0).typeCode = x_ActRelationshipEntry.DRIV
            section.entry(0).Item = New POCD_MT000040UV02Act()
            act = DirectCast(section.entry(0).Item, POCD_MT000040UV02Act)
            act.classCode = x_ActClassDocumentEntryAct.ACT
            act.classCodeSpecified = True
            act.moodCode = x_DocumentActMood.EVN
            act.moodCodeSpecified = True
            act.id = New II(0) {}
            act.id(0) = New II()
            act.id(0).root = Guid.NewGuid().ToString()
            act.id(0).extension = Nothing
            act.id(0).assigningAuthorityName = Nothing
            If _nQRDAFileType = CDAFileTypeEnum.QRDA3 Then
                act.templateId = New II(2) {}
                act.templateId(0) = New II()
                act.templateId(0).root = "2.16.840.1.113883.10.20.17.3.8"

                act.templateId(0).extension = Nothing
                act.templateId(0).assigningAuthorityName = Nothing
                act.templateId(2) = New II()
                act.templateId(2).root = "2.16.840.1.113883.10.20.27.3.23"

                act.templateId(2).extension = "2016-11-01"
                act.templateId(2).assigningAuthorityName = Nothing
            ElseIf _nQRDAFileType = CDAFileTypeEnum.QRDA1 Then
                act.templateId = New II(3) {}
                act.templateId(0) = New II()
                act.templateId(0).root = "2.16.840.1.113883.10.20.17.3.8"

                act.templateId(0).extension = Nothing
                act.templateId(0).assigningAuthorityName = Nothing

                act.templateId(1) = New II()
                act.templateId(1).root = "2.16.840.1.113883.10.20.17.3.8"

                act.templateId(1).extension = "2015-07-01"
                act.templateId(1).assigningAuthorityName = Nothing
                act.templateId(2) = New II()
                act.templateId(2).root = "2.16.840.1.113883.10.20.27.3.23"

                act.templateId(2).extension = Nothing
                act.templateId(2).assigningAuthorityName = Nothing
            End If



            'As per Qualitynet

            act.code = New CD()

            act.code.code = "252116004"
            act.code.codeSystem = "2.16.840.1.113883.6.96"
            act.code.codeSystemName = Nothing
            act.code.codeSystemVersion = Nothing
            act.code.displayName = "Observation Parameters"
            act.effectiveTime = New IVL_TS()
            ''As per QualityNet after first validation on CMS
            act.effectiveTime.operator = Nothing
            act.effectiveTime.value = Nothing
            act.effectiveTime.ItemsElementName = New ItemsChoiceType2(1) {}
            act.effectiveTime.ItemsElementName(0) = ItemsChoiceType2.low
            act.effectiveTime.ItemsElementName(1) = ItemsChoiceType2.high
            act.effectiveTime.Items = New QTY(1) {}
            act.effectiveTime.Items(0) = New IVXB_TS()
            If Not IsNothing(_measurementStartDate) AndAlso _measurementStartDate <> "" Then
                DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Format(CType(_measurementStartDate, Date), "yyyyMMdd")
            Else
                DirectCast(act.effectiveTime.Items(0), IVXB_TS).nullFlavor = "UNK"
                DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Nothing
            End If

            act.effectiveTime.Items(1) = New IVXB_TS()
            If Not IsNothing(_measurementEndDate) AndAlso _measurementEndDate <> "" Then
                DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Format(CType(_measurementEndDate, Date), "yyyyMMdd")
            Else
                DirectCast(act.effectiveTime.Items(1), IVXB_TS).nullFlavor = "UNK"
                DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Nothing
            End If




            Return _ReportingParams
        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "QRDAII Reporting Parameters Section"
            Else
                _msgString = _msgString & vbNewLine & "QRDAII Reporting Parameters Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing


        Finally
            If Not IsNothing(section) Then
                section = Nothing
            End If

            If Not IsNothing(act) Then
                act = Nothing
            End If
            If Not IsNothing(ob) Then
                ob = Nothing
            End If
            If Not IsNothing(list) Then
                list = Nothing
            End If
            If Not IsNothing(encounter) Then
                encounter = Nothing
            End If
        End Try
    End Function

    Private Function GetQRDAIIIMeasureComponent() As POCD_MT000040UV02Component3
        Dim _Measure As POCD_MT000040UV02Component3 = Nothing
        Dim section As POCD_MT000040UV02Section = Nothing

        Dim table As StrucDocTable = Nothing
        Dim extDoc As POCD_MT000040UV02ExternalDocument = Nothing
        Dim org As POCD_MT000040UV02Organizer = Nothing

        Dim ob As POCD_MT000040UV02Observation = Nothing
        Dim obEntry As POCD_MT000040UV02Observation = Nothing
        Dim obEntryRelation As POCD_MT000040UV02Observation = Nothing
        Dim obReferenceExt As POCD_MT000040UV02ExternalObservation = Nothing
        Dim obcontVarReferenceExt As POCD_MT000040UV02ExternalObservation = Nothing
        Dim dvCodes As New DataView
        'Dim dvCodes1 As New DataView
        ''
        ' Dim _isRacePresent As Boolean = False

        Dim _moduleList As List(Of POCD_MT000040UV02Component4) = Nothing
        '  Dim _EntryList As New List(Of POCD_MT000040UV02EntryRelationship)

        Dim _component4 As POCD_MT000040UV02Component4() = Nothing
        Dim _PerformanceRate As POCD_MT000040UV02Component4 = Nothing
        Dim _oEntry As POCD_MT000040UV02EntryRelationship = Nothing
        Dim List As StrucDocList = Nothing

        Try
            If Not IsNothing(_dtMeasures) AndAlso _dtMeasures.Rows.Count > 0 Then
                'Dim strMeasure(0) As String


                _Measure = New POCD_MT000040UV02Component3
                DirectCast(_Measure, POCD_MT000040UV02Component3).section = New POCD_MT000040UV02Section()
                section = DirectCast(_Measure, POCD_MT000040UV02Component3).section
                'TempMayuri
                ' 'section.ID = Nothing
                section.classCode = Nothing
                section.classCodeSpecified = False
                section.moodCode = Nothing
                section.moodCodeSpecified = False

                ''As per QualityNet Standard
                section.templateId = New II(2) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.27.2.1"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = "2017-06-01"

                section.templateId(1) = New II()
                section.templateId(1).root = "2.16.840.1.113883.10.20.24.2.2"
                section.templateId(1).assigningAuthorityName = Nothing
                section.templateId(1).extension = Nothing


                section.templateId(2) = New II()
                section.templateId(2).root = "2.16.840.1.113883.10.20.27.2.3"
                section.templateId(2).assigningAuthorityName = Nothing
                section.templateId(2).extension = "2017-07-01"


                section.code = New CE()

                section.code.code = "55186-1"

                section.code.codeSystem = CodeSystem.LOINC
                section.code.codeSystemName = Nothing
                section.code.codeSystemVersion = Nothing
                section.code.displayName = Nothing
                'nothing
                section.title = New ST()
                section.title.Text = New String() {"Measure Section"}
                ''As per QualityNet after first validation on CMS
                section.title.representation = Nothing
                section.title.mediaType = Nothing

                ''
                section.title.language = Nothing
                section.text = New StrucDocText()
                section.text.mediaType = Nothing
                section.text.ID = Nothing
                section.text.language = Nothing
                section.text.Items = New Object(_dtMeasures.Rows.Count * 2) {}
                section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count + 1) {}



                Dim tblcnt As Integer = 0
                For measures As Integer = 0 To _dtMeasures.Rows.Count - 1
                    If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then

                    Else
                        If Convert.ToString(dtMeasures.Rows(measures)(Col_Measure_No)) = "NQF0421" Then
                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation1")) = "0" And Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2")) = "0" Then
                                Continue For
                            End If

                        ElseIf Convert.ToString(dtMeasures.Rows(measures)(Col_Measure_No)) = "NQF0028" Then
                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation1")) = "0" And Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2")) = "0" And Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation3")) = "0" Then
                                Continue For
                            End If
                        Else

                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation1")) = "0" Then
                                Continue For
                            End If
                        End If
                    End If



                    section.text.Items(tblcnt) = New StrucDocTable()
                    table = DirectCast(section.text.Items(tblcnt), StrucDocTable)
                    table.ID = Nothing
                    table.language = Nothing
                    table.border = "1"
                    table.width = "100%"
                    table.thead = New StrucDocThead()
                    table.thead.ID = Nothing
                    table.thead.language = Nothing
                    table.thead.tr = New StrucDocTr(0) {}

                    table.thead.tr(0) = New StrucDocTr()
                    table.thead.tr(0).ID = Nothing
                    table.thead.tr(0).language = Nothing
                    table.thead.tr(0).Items = New Object(5) {}
                    table.thead.tr(0).Items(0) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).Text = New String() {"eMeasure Title"}
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(1) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).Text = New String() {"Version neutral identifier"}
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(2) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(2), StrucDocTh).Text = New String() {"eMeasure Version Number"}
                    DirectCast(table.thead.tr(0).Items(2), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(2), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(3) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(3), StrucDocTh).Text = New String() {"NQF eMeasure Number"}
                    DirectCast(table.thead.tr(0).Items(3), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(3), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(4) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(4), StrucDocTh).Text = New String() {"eMeasure Identifier (MAT)"}
                    DirectCast(table.thead.tr(0).Items(4), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(4), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(5) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(5), StrucDocTh).Text = New String() {"Version specific identifier"}
                    DirectCast(table.thead.tr(0).Items(5), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(5), StrucDocTh).language = Nothing



                    table.tbody = New StrucDocTbody(0) {}
                    table.tbody(0) = New StrucDocTbody()
                    table.tbody(0).ID = Nothing
                    table.tbody(0).language = Nothing
                    table.tbody(0).tr = New StrucDocTr(0) {}
                    '   section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count - 1) {}


                    table.tbody(0).tr(0) = New StrucDocTr()
                    table.tbody(0).tr(0).Items = New Object(5) {}
                    table.tbody(0).tr(0).Items(0) = New StrucDocTd()
                    table.tbody(0).tr(0).ID = Nothing
                    table.tbody(0).tr(0).language = Nothing

                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_Title)}
                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(1) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_NeutralID)}
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(2) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(2), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_VersionNo)}
                    DirectCast(table.tbody(0).tr(0).Items(2), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(2), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(3) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(3), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_No)}
                    DirectCast(table.tbody(0).tr(0).Items(3), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(3), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(4) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(4), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_CMSID)}
                    DirectCast(table.tbody(0).tr(0).Items(4), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(4), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(5) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(5), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)}
                    DirectCast(table.tbody(0).tr(0).Items(5), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(5), StrucDocTd).language = Nothing

                    ''

                    ''

                    section.entry(measures) = New POCD_MT000040UV02Entry()
                    _moduleList = New List(Of POCD_MT000040UV02Component4)

                    section.entry(measures).typeCode = Nothing
                    section.entry(measures).typeCodeSpecified = False
                    section.entry(measures).Item = New POCD_MT000040UV02Organizer()
                    org = DirectCast(section.entry(measures).Item, POCD_MT000040UV02Organizer)
                    org.classCode = x_ActClassDocumentEntryOrganizer.CLUSTER
                    org.moodCode = "EVN"
                    org.moodCodeSpecified = True


                    org.templateId = New II(2) {}
                    org.templateId(0) = New II()
                    org.templateId(0).root = "2.16.840.1.113883.10.20.24.3.98"
                    org.templateId(0).extension = Nothing
                    org.templateId(0).assigningAuthorityName = Nothing

                    org.templateId(1) = New II()
                    org.templateId(1).root = "2.16.840.1.113883.10.20.27.3.1"
                    org.templateId(1).extension = "2016-09-01"
                    org.templateId(1).assigningAuthorityName = Nothing

                    ''As per QualityNet Standard
                    org.templateId(2) = New II()
                    org.templateId(2).root = "2.16.840.1.113883.10.20.27.3.17"
                    org.templateId(2).extension = "2016-11-01"
                    org.templateId(2).assigningAuthorityName = Nothing
                    ''For Cypress latest release nov
                    org.id = New II(0) {}
                    org.id(0) = New II()
                    org.id(0).nullFlavor = "NA"
                    org.id(0).assigningAuthorityName = Nothing
                    org.id(0).extension = Nothing
                    org.id(0).root = Nothing
                    ''end

                    org.statusCode = New CS()
                    org.statusCode.code = "completed"
                    org.statusCode.codeSystem = Nothing
                    org.statusCode.codeSystemName = Nothing
                    org.statusCode.codeSystemVersion = Nothing
                    org.statusCode.displayName = Nothing


                    org.reference = New POCD_MT000040UV02Reference(0) {}
                    org.reference(0) = New POCD_MT000040UV02Reference
                    org.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                    org.reference(0).realmCode = Nothing
                    org.reference(0).Item = New POCD_MT000040UV02ExternalDocument
                    extDoc = DirectCast(org.reference(0).Item, POCD_MT000040UV02ExternalDocument)
                    extDoc.classCode = ActClassDocument.DOC
                    extDoc.moodCode = "EVN"
                    extDoc.moodCodeSpecified = True
                    extDoc.id = New II(0) {}
                    extDoc.id(0) = New II

                    If _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)) <> "" Then
                        extDoc.id(0).root = "2.16.840.1.113883.4.738"
                        extDoc.id(0).extension = _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) ''version specific identifier
                    Else
                        extDoc.id(0).root = Nothing
                        extDoc.id(0).extension = Nothing
                    End If



                    ' extDoc.id(0).extension = Nothing
                    extDoc.id(0).assigningAuthorityName = Nothing
                    extDoc.code = New CD()
                    extDoc.code.code = "57024-2"
                    extDoc.code.codeSystem = CodeSystem.LOINC
                    extDoc.code.codeSystemName = "LOINC"
                    extDoc.code.codeSystemVersion = Nothing
                    extDoc.code.displayName = "Health Quality Measure Document"

                    extDoc.text = New ED()
                    ''As per QualityNet After first validation on CMS
                    extDoc.text.representation = Nothing
                    If _dtMeasures.Rows(measures)(Col_Measure_Title) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_Title)) <> "" Then
                        extDoc.text.Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_Title)}  ''eMeasure Title
                    Else
                        extDoc.text.Text = Nothing
                    End If

                    extDoc.text.language = Nothing
                    extDoc.text.reference = Nothing
                    extDoc.text.mediaType = Nothing
                    ''commented as per QualityNet
                    ''As per QualityNet after validation on CMS
                    'extDoc.setId = New II()
                    'If _dtMeasures.Rows(measures)(Col_Measure_NeutralID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_NeutralID)) <> "" Then
                    '    extDoc.setId.root = _dtMeasures.Rows(measures)(Col_Measure_NeutralID) ''eMeasure version neutral id GUID 
                    'Else
                    '    extDoc.setId.root = Nothing
                    'End If

                    'extDoc.setId.extension = Nothing
                    'extDoc.setId.assigningAuthorityName = Nothing
                    'extDoc.versionNumber = New INT()
                    'If _dtMeasures.Rows(measures)(Col_Measure_VersionNo) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionNo)) <> "" Then
                    '    extDoc.versionNumber.value = _dtMeasures.Rows(measures)(Col_Measure_VersionNo) ''eMeasure Version number
                    'Else
                    '    extDoc.versionNumber.value = Nothing
                    'End If

                    'extDoc.versionNumber.nullFlavor = Nothing

                    If _dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate)) <> "" Then
                        _PerformanceRate = New POCD_MT000040UV02Component4
                        DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                        ob = DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item

                        ob.classCode = ActClassObservation.OBS
                        ob.moodCode = x_ActMoodDocumentObservation.EVN
                        ob.moodCodespecified = True

                        ob.templateId = New II(2) {}
                        ob.templateId(0) = New II
                        ob.templateId(0).root = "2.16.840.1.113883.10.20.27.3.14"
                        ob.templateId(0).extension = "2016-09-01"
                        ob.templateId(0).assigningAuthorityName = Nothing

                        ''As per QualityNet Standard
                        ob.templateId(1) = New II
                        ob.templateId(1).root = "2.16.840.1.113883.10.20.27.3.30"
                        ob.templateId(1).extension = "2016-09-01"
                        ob.templateId(1).assigningAuthorityName = Nothing

                        ob.templateId(2) = New II
                        ob.templateId(2).root = "2.16.840.1.113883.10.20.27.3.25"
                        ob.templateId(2).extension = "2016-11-01"
                        ob.templateId(2).assigningAuthorityName = Nothing


                        ob.code = New CD()
                        ob.code.code = "72510-1"
                        ob.code.codeSystem = CodeSystem.LOINC
                        ob.code.codeSystemName = "LOINC"
                        ob.code.codeSystemVersion = Nothing
                        ob.code.displayName = "Performance Rate (proportion measure)"

                        ob.statusCode = New CS()
                        ob.statusCode.code = "completed"
                        ob.statusCode.codeSystem = Nothing
                        ob.statusCode.codeSystemName = Nothing
                        ob.statusCode.codeSystemVersion = Nothing
                        ob.statusCode.originalText = Nothing
                        ob.statusCode.displayName = Nothing

                        ob.value = New ANY(0) {}
                        ob.value(0) = New REAL()
                        ''Num/(Deno-Deno. Excl.-Deno. Except.)

                        DirectCast(ob.value(0), REAL).value = Math.Round(_dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate), 6, MidpointRounding.AwayFromZero)
                        ' DirectCast(ob.value(0), REAL).value = Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate))

                        ''As per QualityNet Standard

                        For k As Integer = 0 To _dtMeasures.Columns.Count - 1
                            If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then






                                If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator1" Then
                                    If _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)) <> "" Then

                                        ob.reference = New POCD_MT000040UV02Reference(0) {}
                                        ob.reference(0) = New POCD_MT000040UV02Reference
                                        ob.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                                        ob.reference(0).Item = New POCD_MT000040UV02ExternalObservation
                                        obReferenceExt = DirectCast(ob.reference(0).Item, POCD_MT000040UV02ExternalObservation)
                                        obReferenceExt.classCode = ActClassObservation.OBS
                                        obReferenceExt.moodCode = "EVN"
                                        obReferenceExt.moodCodeSpecified = True
                                        obReferenceExt.id = New II(0) {}
                                        obReferenceExt.id(0) = New II()
                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                        obReferenceExt.id(0).extension = Nothing
                                        obReferenceExt.id(0).assigningAuthorityName = Nothing
                                        obReferenceExt.code = New CD()
                                        obReferenceExt.code.code = "NUMER"
                                        obReferenceExt.code.codeSystem = "2.16.840.1.113883.5.4"
                                        obReferenceExt.code.codeSystemName = "ObservationValue"
                                        obReferenceExt.code.codeSystemVersion = Nothing
                                        obReferenceExt.code.displayName = "Numerator"

                                    End If

                                End If






                            End If
                        Next

                        ''

                        If Not IsNothing(_PerformanceRate) Then
                            _moduleList.Add(_PerformanceRate)
                        End If

                    End If
                    If _dtMeasures.Rows(measures)("IPP2Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1ID")) <> "" Then


                        If _dtMeasures.Rows(measures)("IPP2PerformanceRate") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2PerformanceRate")) <> "" Then
                            _PerformanceRate = New POCD_MT000040UV02Component4
                            DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                            ob = DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item

                            ob.classCode = ActClassObservation.OBS
                            ob.moodCode = x_ActMoodDocumentObservation.EVN
                            ob.moodCodespecified = True

                            ob.templateId = New II(2) {}
                            ob.templateId(0) = New II
                            ob.templateId(0).root = "2.16.840.1.113883.10.20.27.3.14"
                            ob.templateId(0).extension = "2016-09-01"
                            ob.templateId(0).assigningAuthorityName = Nothing

                            ''As per QualityNet Standard
                            ob.templateId(1) = New II
                            ob.templateId(1).root = "2.16.840.1.113883.10.20.27.3.30"
                            ob.templateId(1).extension = "2016-09-01"
                            ob.templateId(1).assigningAuthorityName = Nothing

                            ob.templateId(2) = New II
                            ob.templateId(2).root = "2.16.840.1.113883.10.20.27.3.25"
                            ob.templateId(2).extension = "2016-11-01"
                            ob.templateId(2).assigningAuthorityName = Nothing

                            ob.code = New CD()
                            ob.code.code = "72510-1"
                            ob.code.codeSystem = CodeSystem.LOINC
                            ob.code.codeSystemName = "LOINC"
                            ob.code.codeSystemVersion = Nothing
                            ob.code.displayName = "Performance Rate (proportion measure)"

                            ob.statusCode = New CS()
                            ob.statusCode.code = "completed"
                            ob.statusCode.codeSystem = Nothing
                            ob.statusCode.codeSystemName = Nothing
                            ob.statusCode.codeSystemVersion = Nothing
                            ob.statusCode.originalText = Nothing
                            ob.statusCode.displayName = Nothing

                            ob.value = New ANY(0) {}
                            ob.value(0) = New REAL()
                            ''Num/(Deno-Deno. Excl.-Deno. Except.)

                            DirectCast(ob.value(0), REAL).value = Math.Round(_dtMeasures.Rows(measures)("IPP2PerformanceRate"), 6, MidpointRounding.AwayFromZero)
                            ' DirectCast(ob.value(0), REAL).value = Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate))

                            ''As per QualityNet Standard

                            For k As Integer = 0 To _dtMeasures.Columns.Count - 1
                                If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then






                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator1" Then
                                        If _dtMeasures.Rows(measures)("IPP2Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1ID")) <> "" Then

                                            ob.reference = New POCD_MT000040UV02Reference(0) {}
                                            ob.reference(0) = New POCD_MT000040UV02Reference
                                            ob.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                                            ob.reference(0).Item = New POCD_MT000040UV02ExternalObservation
                                            obReferenceExt = DirectCast(ob.reference(0).Item, POCD_MT000040UV02ExternalObservation)
                                            obReferenceExt.classCode = ActClassObservation.OBS
                                            obReferenceExt.moodCode = "EVN"
                                            obReferenceExt.moodCodeSpecified = True
                                            obReferenceExt.id = New II(0) {}
                                            obReferenceExt.id(0) = New II()
                                            If _dtMeasures.Rows(measures)("IPP2Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1ID")) <> "" Then
                                                obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP2Numerator1ID")
                                            Else
                                                obReferenceExt.id(0).root = Nothing
                                            End If
                                            obReferenceExt.id(0).extension = Nothing
                                            obReferenceExt.id(0).assigningAuthorityName = Nothing
                                            obReferenceExt.code = New CD()
                                            obReferenceExt.code.code = "NUMER"
                                            obReferenceExt.code.codeSystem = "2.16.840.1.113883.5.4"
                                            obReferenceExt.code.codeSystemName = "ObservationValue"
                                            obReferenceExt.code.codeSystemVersion = Nothing
                                            obReferenceExt.code.displayName = "Numerator"

                                        End If

                                    End If






                                End If
                            Next

                            ''

                            If Not IsNothing(_PerformanceRate) Then
                                _moduleList.Add(_PerformanceRate)
                            End If

                        End If
                    End If
                    If Convert.ToString(dtMeasures.Rows(measures)(Col_Measure_No)) = "NQF0028" Then
                        If _dtMeasures.Rows(measures)("IPP3Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3Numerator1ID")) <> "" Then


                            If _dtMeasures.Rows(measures)("IPP3PerformanceRate") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3PerformanceRate")) <> "" Then
                                _PerformanceRate = New POCD_MT000040UV02Component4
                                DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                                ob = DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item

                                ob.classCode = ActClassObservation.OBS
                                ob.moodCode = x_ActMoodDocumentObservation.EVN
                                ob.moodCodeSpecified = True

                                ob.templateId = New II(2) {}
                                ob.templateId(0) = New II
                                ob.templateId(0).root = "2.16.840.1.113883.10.20.27.3.14"
                                ob.templateId(0).extension = "2016-09-01"
                                ob.templateId(0).assigningAuthorityName = Nothing

                                ''As per QualityNet Standard
                                ob.templateId(1) = New II
                                ob.templateId(1).root = "2.16.840.1.113883.10.20.27.3.30"
                                ob.templateId(1).extension = "2016-09-01"
                                ob.templateId(1).assigningAuthorityName = Nothing

                                ob.templateId(2) = New II
                                ob.templateId(2).root = "2.16.840.1.113883.10.20.27.3.25"
                                ob.templateId(2).extension = "2016-11-01"
                                ob.templateId(2).assigningAuthorityName = Nothing

                                ob.code = New CD()
                                ob.code.code = "72510-1"
                                ob.code.codeSystem = CodeSystem.LOINC
                                ob.code.codeSystemName = "LOINC"
                                ob.code.codeSystemVersion = Nothing
                                ob.code.displayName = "Performance Rate (proportion measure)"

                                ob.statusCode = New CS()
                                ob.statusCode.code = "completed"
                                ob.statusCode.codeSystem = Nothing
                                ob.statusCode.codeSystemName = Nothing
                                ob.statusCode.codeSystemVersion = Nothing
                                ob.statusCode.originalText = Nothing
                                ob.statusCode.displayName = Nothing

                                ob.value = New ANY(0) {}
                                ob.value(0) = New REAL()
                                ''Num/(Deno-Deno. Excl.-Deno. Except.)

                                DirectCast(ob.value(0), REAL).value = Math.Round(_dtMeasures.Rows(measures)("IPP3PerformanceRate"), 6, MidpointRounding.AwayFromZero)
                                ' DirectCast(ob.value(0), REAL).value = Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1PerformaceRate))

                                ''As per QualityNet Standard

                                For k As Integer = 0 To _dtMeasures.Columns.Count - 1
                                    If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then






                                        If Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Numerator1" Then
                                            If _dtMeasures.Rows(measures)("IPP3Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3Numerator1ID")) <> "" Then

                                                ob.reference = New POCD_MT000040UV02Reference(0) {}
                                                ob.reference(0) = New POCD_MT000040UV02Reference
                                                ob.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                                                ob.reference(0).Item = New POCD_MT000040UV02ExternalObservation
                                                obReferenceExt = DirectCast(ob.reference(0).Item, POCD_MT000040UV02ExternalObservation)
                                                obReferenceExt.classCode = ActClassObservation.OBS
                                                obReferenceExt.moodCode = "EVN"
                                                obReferenceExt.moodCodeSpecified = True
                                                obReferenceExt.id = New II(0) {}
                                                obReferenceExt.id(0) = New II()
                                                If _dtMeasures.Rows(measures)("IPP3Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3Numerator1ID")) <> "" Then
                                                    obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP3Numerator1ID")
                                                Else
                                                    obReferenceExt.id(0).root = Nothing
                                                End If
                                                obReferenceExt.id(0).extension = Nothing
                                                obReferenceExt.id(0).assigningAuthorityName = Nothing
                                                obReferenceExt.code = New CD()
                                                obReferenceExt.code.code = "NUMER"
                                                obReferenceExt.code.codeSystem = "2.16.840.1.113883.5.4"
                                                obReferenceExt.code.codeSystemName = "ObservationValue"
                                                obReferenceExt.code.codeSystemVersion = Nothing
                                                obReferenceExt.code.displayName = "Numerator"

                                            End If

                                        End If






                                    End If
                                Next

                                ''

                                If Not IsNothing(_PerformanceRate) Then
                                    _moduleList.Add(_PerformanceRate)
                                End If

                            End If
                        End If
                    End If
                    ''As per QualityNet
                    'If _dtMeasures.Rows(measures)(Col_Measure_IPP1ReportingRate) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1ReportingRate)) <> "" Then
                    '    ''Component  Reporting Rate 
                    '    _PerformanceRate = New POCD_MT000040UV02Component4
                    '    DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                    '    ob = DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item



                    '    ob.classCode = ActClassObservation.OBS
                    '    ob.moodCode = x_ActMoodDocumentObservation.EVN

                    '    ob.templateId = New II(0) {}
                    '    ob.templateId(0) = New II
                    '    ob.templateId(0).root = "2.16.840.1.113883.10.20.27.3.15"
                    '    ob.templateId(0).extension = Nothing
                    '    ob.templateId(0).assigningAuthorityName = Nothing

                    '    ob.code = New CD()
                    '    ob.code.code = "72509-3"
                    '    ob.code.codeSystem = CodeSystem.LOINC
                    '    ob.code.codeSystemName = "LOINC"
                    '    ob.code.displayName = "Reporting Rate (proportion measure)"
                    '    ob.code.codeSystemVersion = Nothing

                    '    ob.statusCode = New CS()
                    '    ob.statusCode.code = "completed"
                    '    ob.statusCode.codeSystem = Nothing
                    '    ob.statusCode.codeSystemName = Nothing
                    '    ob.statusCode.codeSystemVersion = Nothing
                    '    ob.statusCode.originalText = Nothing
                    '    ob.statusCode.displayName = Nothing

                    '    ob.value = New ANY(0) {}
                    '    ob.value(0) = New REAL()
                    '    ''(Num+Deno.Excl.+Deno.Except.)/Denom.

                    '    DirectCast(ob.value(0), REAL).value = _dtMeasures.Rows(measures)(Col_Measure_IPP1ReportingRate) ''eMeasure Version number

                    '    If Not IsNothing(_PerformanceRate) Then
                    '        _moduleList.Add(_PerformanceRate)
                    '    End If
                    'End If

                    List = New StrucDocList
                    tblcnt = tblcnt + 1
                    section.text.Items(tblcnt) = New StrucDocList
                    List = DirectCast(section.text.Items(tblcnt), StrucDocList)
                    List.ID = Nothing
                    List.language = Nothing
                    List.item = New StrucDocItem(_dtMeasures.Columns.Count - 1) {}
                    For k As Integer = 0 To _dtMeasures.Columns.Count - 1
                        If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then


                            Select Case Convert.ToString(_dtMeasures.Columns(k))

                                Case "InitialPatientPopulation1", "InitialPatientPopulation2", "IPP1Denominator1", "IPP1Numerator1", "IPP1Numerator2", "IPP1DenoExclusion", "IPP1DenoException", "IPP2Denominator1", "IPP2Numerator1", "IPP2DenoExclusion1", "IPP2DenoException1", "InitialPatientPopulation3", "IPP3Denominator1", "IPP3Numerator1", "IPP3DenoExclusion1", "IPP3DenoException1"
                                    If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then
                                    Else
                                        If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation2" Then
                                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2")) = 0 Then
                                                Exit For
                                            End If
                                        End If
                                        If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation1" Then
                                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation1")) = 0 Then
                                                Exit For
                                            End If
                                        End If
                                        If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation3" Then
                                            If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation3")) = 0 Then
                                                Exit For
                                            End If
                                        End If

                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation2" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation3" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation3ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Denominator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1Denominator1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)) = "" Then
                                            Exit Select
                                        End If
                                    End If

                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator2" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator2ID)) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoExclusion" Then

                                        If Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExclusionID)) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoException" Then


                                        If Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExceptionID)) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    ''Added by Mayuri
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Denominator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2Denominator1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoExclusion1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoExclusion1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoException1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoException1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If

                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Denominator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP3Denominator1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Numerator1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP3Numerator1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoExclusion1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP3DenoExclusion1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoException1" Then
                                        If Convert.ToString(_dtMeasures.Rows(measures)("IPP3DenoException1ID")) = "" Then
                                            Exit Select
                                        End If
                                    End If

                                    'If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation2" Then
                                    '    If Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2ID")) = "" Then
                                    '        Exit Select
                                    '    End If
                                    'End If
                                    ''End

                                    ' End If
                                    ''
                                    'If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then
                                    'Else
                                    '    If Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Denominator1" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1Denominator1")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '        'ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator1" Then
                                    '        '    If Convert.ToString(_dtMeasures.Rows(measures)("IPP1Numerator1")) = "0" Then
                                    '        '        Continue For
                                    '        '    End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator2" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1Numerator2")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoExclusion" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1DenoExclusion")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoException" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1DenoException")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1NumExclusion" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP1NumExclusion")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '        ''Added by Mayuri
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Denominator1" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2Denominator1")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '        'ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator1" Then
                                    '        '    If Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1")) = "0" Then
                                    '        '        Continue For
                                    '        '    End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator2" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator2")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoExclusion1" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoExclusion1")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoException1" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoException1")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2NumExclusion1" Then
                                    '        If Convert.ToString(_dtMeasures.Rows(measures)("IPP2NumExclusion1")) = "0" Then
                                    '            Continue For
                                    '        End If
                                    '    End If

                                    'End If


                                    ''
                                    _PerformanceRate = New POCD_MT000040UV02Component4
                                    DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                                    ob = DirectCast(_PerformanceRate, POCD_MT000040UV02Component4).Item


                                    ob.classCode = ActClassObservation.OBS
                                    ob.moodCode = x_ActMoodDocumentObservation.EVN
                                    ob.moodCodeSpecified = True

                                    ob.templateId = New II(1) {}
                                    ob.templateId(0) = New II
                                    ob.templateId(0).root = "2.16.840.1.113883.10.20.27.3.5"
                                    ob.templateId(0).extension = "2016-09-01"
                                    ob.templateId(0).assigningAuthorityName = Nothing
                                    ''As per QualityNet standard
                                    ob.templateId(1) = New II
                                    ob.templateId(1).root = "2.16.840.1.113883.10.20.27.3.16"
                                    ob.templateId(1).extension = "2016-11-01"
                                    ob.templateId(1).assigningAuthorityName = Nothing

                                    ob.code = New CD()
                                    ob.code.code = "ASSERTION"
                                    ob.code.codeSystem = "2.16.840.1.113883.5.4"
                                    ob.code.codeSystemName = "ActCode"
                                    ob.code.displayName = "Assertion"
                                    ob.code.codeSystemVersion = Nothing

                                    ob.statusCode = New CS()
                                    ob.statusCode.code = "completed"
                                    ob.statusCode.codeSystem = Nothing
                                    ob.statusCode.codeSystemName = Nothing
                                    ob.statusCode.codeSystemVersion = Nothing
                                    ob.statusCode.originalText = Nothing
                                    ob.statusCode.displayName = Nothing
                                    ''IF Statement
                                    ''

                                    '''''''''''
                                    'List.item(k) = New StrucDocItem
                                    'List.item(k).ID = Nothing
                                    'List.item(k).language = Nothing
                                    'List.item(k).caption = New StrucDocCaption
                                    'List.item(k).caption.ID = Nothing
                                    'List.item(k).caption.language = Nothing




                                    If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation1" Or Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation2" Or Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation3" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()
                                        DirectCast(ob.value(0), CD).displayName = "initial population"
                                        DirectCast(ob.value(0), CD).code = "IPOP"
                                        ' DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.1063"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"
                                        DirectCast(ob.value(0), CD).codeSystemName = "ActCode"

                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing


                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Initial Patient Population: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing


                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Denominator1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Denominator1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Denominator1" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Denominator"
                                        DirectCast(ob.value(0), CD).code = "DENOM"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"
                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        '   DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing

                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Denominator: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing



                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Numerator1" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Numerator"
                                        DirectCast(ob.value(0), CD).code = "NUMER"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"

                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        '   DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing

                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Numerator1: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing


                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator2" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Numerator"
                                        DirectCast(ob.value(0), CD).code = "NUMER"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"

                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        ' DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing

                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Numerator2: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing

                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoExclusion" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoExclusion1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoExclusion1" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Denominator Exclusions"
                                        DirectCast(ob.value(0), CD).code = "DENEX"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"

                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        '   DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing

                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Denominator Exclusions: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing

                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoException" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoException1" Or Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoException1" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Denominator Exceptions"
                                        DirectCast(ob.value(0), CD).code = "DENEXCEP"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"

                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        '  DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing
                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Denominator Exceptions: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing

                                        'List.item(k).Items(2) = New StrucDocList
                                        'DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                        'DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing

                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1NumExclusion" Then
                                        ob.value = New ANY(0) {}
                                        ob.value(0) = New CD()

                                        DirectCast(ob.value(0), CD).displayName = "Numerator Exclusions"
                                        DirectCast(ob.value(0), CD).code = "NUMEX"
                                        DirectCast(ob.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"

                                        DirectCast(ob.value(0), CD).codeSystemName = "ObservationValue"
                                        '  DirectCast(ob.value(0), CD).codeSystem = Nothing
                                        DirectCast(ob.value(0), CD).codeSystemVersion = Nothing

                                        List.item(k) = New StrucDocItem
                                        List.item(k).ID = Nothing
                                        List.item(k).language = Nothing
                                        List.item(k).Items = New Object(2) {}
                                        List.item(k).Items(0) = New StrucDocContent
                                        DirectCast(List.item(k).Items(0), StrucDocContent).Text = New String() {"Numerator Exclusions: "}
                                        DirectCast(List.item(k).Items(0), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).language = Nothing
                                        DirectCast(List.item(k).Items(0), StrucDocContent).styleCode = "Bold"

                                        List.item(k).Items(1) = New StrucDocContent
                                        DirectCast(List.item(k).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)(k))}
                                        DirectCast(List.item(k).Items(1), StrucDocContent).ID = Nothing
                                        DirectCast(List.item(k).Items(1), StrucDocContent).language = Nothing


                                    End If

                                    If Not IsNothing(_dtCodes) AndAlso _dtCodes.Rows.Count > 0 Then

                                        dvCodes = _dtCodes.DefaultView
                                        dvCodes.RowFilter = "MeasureNo = '" & _dtMeasures.Rows(measures)(Col_Measure_No) & "' and TableType ='" & Convert.ToString(_dtMeasures.Columns(k)) & "'"
                                        If dvCodes.Count > 0 Then
                                            List.item(k).Items(2) = New StrucDocList
                                            DirectCast(List.item(k).Items(2), StrucDocList).ID = Nothing
                                            DirectCast(List.item(k).Items(2), StrucDocList).language = Nothing
                                            DirectCast(List.item(k).Items(2), StrucDocList).item = New StrucDocItem(dvCodes.Count - 1) {}

                                        End If

                                        If Not IsNothing(dvCodes) AndAlso dvCodes.Count <= 0 Then
                                            ob.entryRelationship = New POCD_MT000040UV02EntryRelationship(5 + 2) {}
                                        Else
                                            ob.entryRelationship = New POCD_MT000040UV02EntryRelationship(dvCodes.Count + 2) {}
                                        End If

                                        ob.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship()
                                        ob.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
                                        ob.entryRelationship(0).inversionInd = True
                                        ob.entryRelationship(0).inversionIndSpecified = True
                                        '' ob.entryRelationship(0).contextConductionInd = False
                                        ob.entryRelationship(0).Item = New POCD_MT000040UV02Observation()
                                        obEntry = DirectCast(ob.entryRelationship(0).Item, POCD_MT000040UV02Observation)

                                        obEntry.classCode = ActClassObservation.OBS
                                        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                        obEntry.moodCodeSpecified = True

                                        obEntry.templateId = New II(1) {}
                                        obEntry.templateId(0) = New II
                                        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
                                        obEntry.templateId(0).extension = Nothing
                                        obEntry.templateId(0).assigningAuthorityName = Nothing
                                        ' ''As per Qualitynet standard
                                        obEntry.templateId(1) = New II
                                        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.24"
                                        obEntry.templateId(1).extension = Nothing
                                        obEntry.templateId(1).assigningAuthorityName = Nothing

                                        obEntry.code = New CD()
                                        obEntry.code.code = "MSRAGG"
                                        obEntry.code.codeSystem = "2.16.840.1.113883.5.4"
                                        obEntry.code.codeSystemName = "ActCode"
                                        obEntry.code.displayName = "rate aggregation"
                                        obEntry.code.codeSystemVersion = Nothing
                                        ''As per QualityNet Standard
                                        obEntry.statusCode = New CS()
                                        obEntry.statusCode.code = "completed"
                                        obEntry.statusCode.codeSystem = Nothing
                                        obEntry.statusCode.codeSystemName = Nothing
                                        obEntry.statusCode.codeSystemVersion = Nothing
                                        obEntry.statusCode.originalText = Nothing
                                        obEntry.statusCode.displayName = Nothing

                                        obEntry.value = New ANY(0) {}
                                        obEntry.value(0) = New INT
                                        If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then
                                            DirectCast(obEntry.value(0), INT).value = _dtMeasures.Rows(measures)(k)
                                        Else
                                            DirectCast(obEntry.value(0), INT).value = Nothing
                                        End If


                                        obEntry.methodCode = New CE(0) {}
                                        obEntry.methodCode(0) = New CE()
                                        obEntry.methodCode(0).code = "COUNT"
                                        obEntry.methodCode(0).displayName = "Count"
                                        obEntry.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                                        obEntry.methodCode(0).codeSystemName = "ObservationMethod"
                                        obEntry.methodCode(0).codeSystemVersion = Nothing

                                        Dim _entryrelationcnt As Integer = 0
                                        If _dtMeasures.Rows(measures)("ReportingStratum1") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("ReportingStratum1")) <> "" Then



                                            ''Stratum1
                                            For i As Integer = 1 To 2
                                                ob.entryRelationship(i) = New POCD_MT000040UV02EntryRelationship()
                                                ob.entryRelationship(i).typeCode = ActRelationshipType.COMP
                                                ob.entryRelationship(i).Item = New POCD_MT000040UV02Observation()


                                                obEntry = DirectCast(ob.entryRelationship(i).Item, POCD_MT000040UV02Observation)
                                                obEntry.classCode = ActClassObservation.OBS
                                                obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                                obEntry.moodCodeSpecified = True
                                                obEntry.templateId = New II(1) {}
                                                obEntry.templateId(0) = New II()
                                                obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.4"
                                                obEntry.templateId(0).extension = Nothing
                                                obEntry.templateId(0).assigningAuthorityName = Nothing
                                                ''As per QualityNet Standard
                                                obEntry.templateId(1) = New II()
                                                obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.20"
                                                obEntry.templateId(1).extension = Nothing
                                                obEntry.templateId(1).assigningAuthorityName = Nothing

                                                obEntry.code = New CD()
                                                obEntry.code.code = "ASSERTION"
                                                obEntry.code.codeSystem = "2.16.840.1.113883.5.4"
                                                obEntry.code.displayName = "Assertion"
                                                obEntry.code.codeSystemName = "ActCode"
                                                obEntry.code.codeSystemVersion = Nothing


                                                obEntry.statusCode = New CS()
                                                obEntry.statusCode.code = "completed"
                                                obEntry.statusCode.codeSystem = Nothing
                                                obEntry.statusCode.codeSystemName = Nothing
                                                obEntry.statusCode.codeSystemVersion = Nothing
                                                obEntry.statusCode.displayName = Nothing


                                                obEntry.value = New CD(0) {}
                                                obEntry.value(0) = New CD
                                                DirectCast(obEntry.value(0), CD).nullFlavor = "OTH"

                                                DirectCast(obEntry.value(0), CD).originalText = New ED()
                                                DirectCast(obEntry.value(0), CD).originalText.Text = New String() {"Stratum"}
                                                DirectCast(obEntry.value(0), CD).originalText.language = Nothing
                                                DirectCast(obEntry.value(0), CD).code = Nothing
                                                DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                                                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                                                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                                                DirectCast(obEntry.value(0), CD).displayName = Nothing

                                                obEntry.methodCode = New CE(0) {}
                                                obEntry.methodCode(0) = New CE()
                                                obEntry.methodCode(0).code = "MEDIAN"
                                                obEntry.methodCode(0).displayName = "Median"
                                                obEntry.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                                                obEntry.methodCode(0).codeSystemName = "ObservationMethod"
                                                obEntry.methodCode(0).codeSystemVersion = Nothing
                                                obEntry.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                                                obEntry.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                                                obEntry.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
                                                obEntry.entryRelationship(0).inversionInd = True
                                                obEntry.entryRelationship(0).inversionIndSpecified = True
                                                '' obEntry.entryRelationship(0).contextConductionInd = False


                                                obEntry.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                                                obEntryRelation = DirectCast(obEntry.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                                obEntryRelation.classCode = ActClassObservation.OBS
                                                obEntryRelation.moodCode = x_ActMoodDocumentObservation.EVN
                                                obEntryRelation.moodCodeSpecified = True
                                                obEntryRelation.templateId = New II(1) {}
                                                obEntryRelation.templateId(0) = New II()
                                                obEntryRelation.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
                                                obEntryRelation.templateId(0).extension = Nothing
                                                obEntryRelation.templateId(0).assigningAuthorityName = Nothing

                                                ''As per QualityNet Standard
                                                obEntryRelation.templateId(1) = New II()
                                                obEntryRelation.templateId(1).root = "2.16.840.1.113883.10.20.27.3.24"
                                                obEntryRelation.templateId(1).extension = Nothing
                                                obEntryRelation.templateId(1).assigningAuthorityName = Nothing

                                                obEntryRelation.code = New CD()
                                                obEntryRelation.code.code = "MSRAGG"
                                                obEntryRelation.code.displayName = "rate aggregation"
                                                obEntryRelation.code.codeSystem = "2.16.840.1.113883.5.4"
                                                obEntryRelation.code.codeSystemName = "ActCode"
                                                obEntryRelation.code.codeSystemVersion = Nothing
                                                ''As per QualityNet Standard
                                                obEntryRelation.statusCode = New CS()
                                                obEntryRelation.statusCode.code = "completed"
                                                obEntryRelation.statusCode.codeSystem = Nothing
                                                obEntryRelation.statusCode.codeSystemName = Nothing
                                                obEntryRelation.statusCode.codeSystemVersion = Nothing
                                                obEntryRelation.statusCode.displayName = Nothing

                                                obEntryRelation.value = New ANY(0) {}
                                                obEntryRelation.value(0) = New INT

                                                If dtStratum1.Rows.Count > 0 And dtStratum2.Rows.Count > 0 Then
                                                    If i = 1 Then
                                                        For l As Integer = 0 To dtStratum1.Columns.Count - 1
                                                            If Convert.ToString(dtStratum1.Columns(l)) = Convert.ToString(_dtMeasures.Columns(k)) Then
                                                                If dtStratum1.Rows(0)(l) IsNot Nothing AndAlso Convert.ToString(dtStratum1.Rows(0)(l)) <> "" Then
                                                                    DirectCast(obEntryRelation.value(0), INT).value = dtStratum1.Rows(0)(l)
                                                                    Exit For
                                                                End If

                                                            End If
                                                        Next
                                                    Else
                                                        For l As Integer = 0 To dtStratum2.Columns.Count - 1
                                                            If Convert.ToString(dtStratum2.Columns(l)) = Convert.ToString(_dtMeasures.Columns(k)) Then
                                                                If dtStratum2.Rows(0)(l) IsNot Nothing AndAlso Convert.ToString(dtStratum2.Rows(0)(l)) <> "" Then
                                                                    DirectCast(obEntryRelation.value(0), INT).value = dtStratum2.Rows(0)(l)
                                                                    Exit For
                                                                End If

                                                            End If
                                                        Next
                                                    End If



                                                End If


                                                obEntryRelation.methodCode = New CE(0) {}
                                                obEntryRelation.methodCode(0) = New CE()
                                                obEntryRelation.methodCode(0).code = "COUNT"
                                                obEntryRelation.methodCode(0).displayName = "Count"
                                                obEntryRelation.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                                                obEntryRelation.methodCode(0).codeSystemName = "ObservationMethod"
                                                obEntryRelation.methodCode(0).codeSystemVersion = Nothing

                                                ''reference
                                                obEntry.reference = New POCD_MT000040UV02Reference(0) {}
                                                obEntry.reference(0) = New POCD_MT000040UV02Reference
                                                obEntry.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                                                obEntry.reference(0).Item = New POCD_MT000040UV02ExternalObservation
                                                obReferenceExt = DirectCast(obEntry.reference(0).Item, POCD_MT000040UV02ExternalObservation)
                                                obReferenceExt.classCode = ActClassObservation.OBS
                                                obReferenceExt.moodCode = "EVN"
                                                obReferenceExt.moodCodeSpecified = True
                                                obReferenceExt.id = New II(0) {}
                                                obReferenceExt.id(0) = New II()
                                                If i = 1 Then
                                                    If _dtMeasures.Rows(measures)("ReportingStratum1") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("ReportingStratum1")) <> "" Then
                                                        obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("ReportingStratum1")
                                                    Else
                                                        obReferenceExt.id(0).root = Nothing
                                                    End If

                                                Else
                                                    If _dtMeasures.Rows(measures)("ReportingStratum2") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("ReportingStratum2")) <> "" Then
                                                        obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("ReportingStratum2")
                                                    Else
                                                        obReferenceExt.id(0).root = Nothing
                                                    End If
                                                End If
                                                obReferenceExt.id(0).extension = Nothing
                                                obReferenceExt.id(0).assigningAuthorityName = Nothing
                                                'If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation1" Then
                                                '    If _dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID)) <> "" Then
                                                '        obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID)
                                                '    Else
                                                '        obReferenceExt.id(0).root = Nothing
                                                '    End If
                                                'End If
                                                _entryrelationcnt = i
                                            Next
                                            _entryrelationcnt = _entryrelationcnt + 1
                                        Else
                                            _entryrelationcnt = 1
                                        End If



                                        If Not IsNothing(dvCodes) AndAlso dvCodes.Count > 0 Then

                                            For _cnt As Integer = 0 To dvCodes.Count - 1
                                                If dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "Gender" Then


                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt) = New StrucDocItem

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)}

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).ID = Nothing
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).language = Nothing

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items = New Object(1) {}

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0) = New StrucDocContent

                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) & ": "}

                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).ID = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).language = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).styleCode = "Bold"



                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                                    ''ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)
                                                    GetGender(obEntry, dvCodes, _cnt)

                                                    'obEntry.classCode = ActClassObservation.OBS
                                                    'obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                                    'obEntry.moodCodespecified = True
                                                    ' ''As per QualiNet Standard
                                                    ''obEntry.id = New II(0) {}
                                                    ''obEntry.id(0) = New II()
                                                    ''obEntry.id(0).nullFlavor = "NA"
                                                    ''obEntry.id(0).root = Nothing
                                                    ''obEntry.id(0).extension = Nothing
                                                    ''obEntry.id(0).assigningAuthorityName = Nothing

                                                    'obEntry.templateId = New II(1) {}
                                                    'obEntry.templateId(0) = New II
                                                    'obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.6"
                                                    'obEntry.templateId(0).extension = Nothing
                                                    'obEntry.templateId(0).assigningAuthorityName = Nothing

                                                    'obEntry.templateId(1) = New II
                                                    'obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.21"
                                                    'obEntry.templateId(1).extension = Nothing
                                                    'obEntry.templateId(1).assigningAuthorityName = Nothing

                                                    'obEntry.code = New CD()
                                                    'obEntry.code.code = "184100006"
                                                    'obEntry.code.codeSystem = CodeSystem.SNOMED_CT
                                                    'obEntry.code.codeSystemName = "SNOMED_CT"
                                                    'obEntry.code.codeSystemVersion = Nothing
                                                    'obEntry.code.displayName = "patient sex"

                                                    'obEntry.statusCode = New CS()
                                                    'obEntry.statusCode.code = "completed"
                                                    'obEntry.statusCode.codeSystem = Nothing
                                                    'obEntry.statusCode.codeSystemName = Nothing
                                                    'obEntry.statusCode.codeSystemVersion = Nothing
                                                    'obEntry.statusCode.displayName = Nothing


                                                    'obEntry.value = New CD(0) {}
                                                    'obEntry.value(0) = New CD
                                                    'If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                                                    '    DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)).Code
                                                    '    DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)).Description
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)).CodingSystemName

                                                    'Else
                                                    '    DirectCast(obEntry.value(0), CD).code = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).displayName = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = Nothing

                                                    'End If


                                                    'DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.AdministrativeGender

                                                    'DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing




                                                ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "Ethnicity" Then
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt) = New StrucDocItem
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)}
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).ID = Nothing
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).language = Nothing

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items = New Object(1) {}

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0) = New StrucDocContent
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) & ": "}
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).ID = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).language = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).styleCode = "Bold"


                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                                    ''   ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)
                                                    GetEthnicity(obEntry, dvCodes, _cnt)

                                                    'obEntry.classCode = ActClassObservation.OBS
                                                    'obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                                    'obEntry.moodCodespecified = True

                                                    ''obEntry.id = New II(0) {}
                                                    ''obEntry.id(0) = New II()
                                                    ''obEntry.id(0).nullFlavor = "NA"
                                                    ''obEntry.id(0).root = Nothing
                                                    ''obEntry.id(0).extension = Nothing
                                                    ''obEntry.id(0).assigningAuthorityName = Nothing

                                                    'obEntry.templateId = New II(1) {}
                                                    'obEntry.templateId(0) = New II
                                                    'obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.7"
                                                    'obEntry.templateId(0).extension = Nothing
                                                    'obEntry.templateId(0).assigningAuthorityName = Nothing
                                                    ' ''As per Qualitynet Standard
                                                    'obEntry.templateId(1) = New II
                                                    'obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.22"
                                                    'obEntry.templateId(1).extension = Nothing
                                                    'obEntry.templateId(1).assigningAuthorityName = Nothing

                                                    'obEntry.code = New CD()
                                                    'obEntry.code.code = "364699009"
                                                    'obEntry.code.codeSystem = CodeSystem.SNOMED_CT
                                                    'obEntry.code.codeSystemName = "SNOMED_CT"
                                                    'obEntry.code.codeSystemVersion = Nothing
                                                    'obEntry.code.displayName = "Ethnic Group"

                                                    'obEntry.statusCode = New CS()
                                                    'obEntry.statusCode.code = "completed"
                                                    'obEntry.statusCode.codeSystem = Nothing
                                                    'obEntry.statusCode.codeSystemName = Nothing
                                                    'obEntry.statusCode.codeSystemVersion = Nothing
                                                    'obEntry.statusCode.displayName = Nothing


                                                    'obEntry.value = New CD(0) {}
                                                    'obEntry.value(0) = New CD
                                                    ''DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Code
                                                    ''DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                                                    ''DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).CodingSystemName
                                                    ''DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                                                    ''DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Description
                                                    'If dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode)) <> "" Then
                                                    '    DirectCast(obEntry.value(0), CD).code = Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode))
                                                    '    DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = "OMB Standards for Race and Ethnicity"
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                                                    '    If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                                                    '        DirectCast(obEntry.value(0), CD).displayName = Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))
                                                    '    Else
                                                    '        DirectCast(obEntry.value(0), CD).displayName = Nothing
                                                    '    End If
                                                    'Else
                                                    '    DirectCast(obEntry.value(0), CD).nullFlavor = "NA"
                                                    '    DirectCast(obEntry.value(0), CD).code = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

                                                    '    DirectCast(obEntry.value(0), CD).displayName = Nothing

                                                    'End If


                                                ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "Race" Then
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt) = New StrucDocItem
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)}
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).ID = Nothing
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).language = Nothing

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items = New Object(1) {}





                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0) = New StrucDocContent
                                                    '   If dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode)) <> "" Then
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) & ": "}
                                                    'Else
                                                    '    DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Other Race" & ": "}
                                                    'End If

                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).ID = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).language = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).styleCode = "Bold"




                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    '' ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)

                                                    GetRace(obEntry, dvCodes, _cnt)
                                                    'obEntry.classCode = ActClassObservation.OBS
                                                    'obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                                    'obEntry.moodCodespecified = True

                                                    ''obEntry.id = New II(0) {}
                                                    ''obEntry.id(0) = New II()
                                                    ''obEntry.id(0).nullFlavor = "NA"
                                                    ''obEntry.id(0).root = Nothing
                                                    ''obEntry.id(0).extension = Nothing
                                                    ''obEntry.id(0).assigningAuthorityName = Nothing

                                                    'obEntry.templateId = New II(1) {}
                                                    'obEntry.templateId(0) = New II
                                                    'obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.8"
                                                    'obEntry.templateId(0).extension = Nothing
                                                    'obEntry.templateId(0).assigningAuthorityName = Nothing
                                                    ''As per QualityNet Standard
                                                    'obEntry.templateId(1) = New II
                                                    'obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.19"
                                                    'obEntry.templateId(1).extension = Nothing
                                                    'obEntry.templateId(1).assigningAuthorityName = Nothing

                                                    'obEntry.code = New CD()
                                                    'obEntry.code.code = "103579009"
                                                    'obEntry.code.codeSystem = CodeSystem.SNOMED_CT
                                                    'obEntry.code.codeSystemName = "SNOMED_CT"
                                                    'obEntry.code.codeSystemVersion = Nothing
                                                    'obEntry.code.displayName = Nothing

                                                    'obEntry.statusCode = New CS()
                                                    'obEntry.statusCode.code = "completed"
                                                    'obEntry.statusCode.codeSystem = Nothing
                                                    'obEntry.statusCode.codeSystemName = Nothing
                                                    'obEntry.statusCode.codeSystemVersion = Nothing
                                                    'obEntry.statusCode.displayName = Nothing


                                                    'obEntry.value = New CD(0) {}
                                                    'obEntry.value(0) = New CD
                                                    ''DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Code
                                                    ''DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                                                    ''DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).CodingSystemName
                                                    ''DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                                                    ''DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Description
                                                    'If dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode)) <> "" Then
                                                    '    DirectCast(obEntry.value(0), CD).code = Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataCode))
                                                    '    DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = "OMB Standards for Race and Ethnicity"
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                                                    '    If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                                                    '        DirectCast(obEntry.value(0), CD).displayName = Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))
                                                    '    Else
                                                    '        DirectCast(obEntry.value(0), CD).displayName = Nothing
                                                    '    End If
                                                    'Else
                                                    '    DirectCast(obEntry.value(0), CD).nullFlavor = "NA"
                                                    '    DirectCast(obEntry.value(0), CD).code = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                                                    '    DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

                                                    '    DirectCast(obEntry.value(0), CD).displayName = Nothing

                                                    'End If









                                                ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "ZipCode" Then
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt) = New StrucDocItem
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)}
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).ID = Nothing
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).language = Nothing

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items = New Object(1) {}

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0) = New StrucDocContent
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"ZipCode" & ": "}
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).ID = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).language = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).styleCode = "Bold"

                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    ''  ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)
                                                    GetZipCode(obEntry, dvCodes, _cnt)

                                                    'obEntry.classCode = ActClassObservation.OBS
                                                    'obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                                    'obEntry.moodCodespecified = True

                                                    'obEntry.id = New II(0) {}
                                                    'obEntry.id(0) = New II()
                                                    'obEntry.id(0).nullFlavor = "NA"
                                                    'obEntry.id(0).root = Nothing
                                                    'obEntry.id(0).extension = Nothing
                                                    'obEntry.id(0).assigningAuthorityName = Nothing

                                                    'obEntry.templateId = New II(0) {}
                                                    'obEntry.templateId(0) = New II
                                                    'obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.10"
                                                    'obEntry.templateId(0).extension = Nothing
                                                    'obEntry.templateId(0).assigningAuthorityName = Nothing

                                                    'obEntry.code = New CD()
                                                    'obEntry.code.code = "184102003"
                                                    'obEntry.code.codeSystem = CodeSystem.SNOMED_CT
                                                    'obEntry.code.codeSystemName = "SNOMED_CT"
                                                    'obEntry.code.codeSystemVersion = Nothing
                                                    'obEntry.code.displayName = Nothing

                                                    'obEntry.statusCode = New CS()
                                                    'obEntry.statusCode.code = "completed"
                                                    'obEntry.statusCode.codeSystem = Nothing
                                                    'obEntry.statusCode.codeSystemName = Nothing
                                                    'obEntry.statusCode.codeSystemVersion = Nothing
                                                    'obEntry.statusCode.displayName = Nothing

                                                    'obEntry.value = New ANY(0) {}
                                                    'obEntry.value(0) = New ST()
                                                    'If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                                                    '    DirectCast(obEntry.value(0), ST).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)}
                                                    'Else
                                                    '    DirectCast(obEntry.value(0), ST).Text = Nothing
                                                    'End If

                                                    'DirectCast(obEntry.value(0), ST).language = Nothing

                                                ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "Payer" Then

                                                    ''
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt) = New StrucDocItem
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Text = New String() {dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)}
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).ID = Nothing
                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).language = Nothing

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items = New Object(1) {}

                                                    DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0) = New StrucDocContent

                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).ID = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).language = Nothing
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).styleCode = "Bold"

                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCodeSpecified = True
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                                    '' ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)

                                                    GetPayer(obEntry, dvCodes, _cnt)
                                                    DirectCast(DirectCast(List.item(k).Items(2), StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer: "}

                                                    ''reference range
                                                ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) = "Stratum1" Then



                                                    ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).typeCodeSpecified = True
                                                    ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()


                                                    obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)
                                                    GetStratrum1(obEntry, dvCodes, _cnt)


                                                    ''reference


                                                End If
                                                If dvCodes.ToTable.Rows(_cnt)(Col_Codes_DataType) <> "ContinousVarMeasure" Then



                                                    GetEntryRelation(obEntry, dvCodes, _cnt)

                                                End If

                                            Next
                                        Else ''Numerator 0

                                            Dim _cnt As Integer = 0
                                            ''Gender
                                            ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                            ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                            ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()

                                            obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)

                                            GetGender(obEntry, dvCodes, _cnt)
                                            _entryrelationcnt = _entryrelationcnt + 1

                                            ''race
                                            ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                            ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                            ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()

                                            obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)



                                            GetEthnicity(obEntry, dvCodes, _cnt)


                                            _entryrelationcnt = _entryrelationcnt + 1

                                            ''Ethnicity

                                            ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                            '' ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                            ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                            ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                            obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)
                                            GetRace(obEntry, dvCodes, _cnt)

                                            _entryrelationcnt = _entryrelationcnt + 1



                                            ob.entryRelationship(_cnt + _entryrelationcnt) = New POCD_MT000040UV02EntryRelationship()
                                            ob.entryRelationship(_cnt + _entryrelationcnt).typeCode = ActRelationshipType.COMP
                                            ob.entryRelationship(_cnt + _entryrelationcnt).Item = New POCD_MT000040UV02Observation()
                                            '' ob.entryRelationship(_cnt + 1).contextConductionInd = False
                                            obEntry = DirectCast(ob.entryRelationship(_cnt + _entryrelationcnt).Item, POCD_MT000040UV02Observation)

                                            GetPayer(obEntry, dvCodes, _cnt)
                                        End If

                                    Else
                                        ob.entryRelationship = New POCD_MT000040UV02EntryRelationship(dvCodes.Count) {}
                                        ob.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship()
                                        ob.entryRelationship(0).typeCode = ActRelationshipType.SUBJ

                                        ob.entryRelationship(0).inversionInd = True
                                        ob.entryRelationship(0).inversionIndSpecified = True
                                        ''   ob.entryRelationship(0).contextConductionInd = False

                                        ob.entryRelationship(0).Item = New POCD_MT000040UV02Observation()
                                        obEntry = DirectCast(ob.entryRelationship(0).Item, POCD_MT000040UV02Observation)

                                        obEntry.classCode = ActClassObservation.OBS
                                        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
                                        obEntry.moodCodeSpecified = True
                                        obEntry.templateId = New II(1) {}
                                        obEntry.templateId(0) = New II
                                        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
                                        obEntry.templateId(0).extension = Nothing
                                        obEntry.templateId(0).assigningAuthorityName = Nothing

                                        obEntry.templateId(1) = New II
                                        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.24"
                                        obEntry.templateId(1).extension = Nothing
                                        obEntry.templateId(1).assigningAuthorityName = Nothing

                                        obEntry.code = New CD()
                                        obEntry.code.code = "MSRAGG"
                                        obEntry.code.codeSystem = "2.16.840.1.113883.5.4"
                                        obEntry.code.codeSystemName = "ActCode"
                                        obEntry.code.displayName = "rate aggregation"
                                        obEntry.code.codeSystemVersion = Nothing
                                        ''As per QualityNet Standard
                                        obEntry.statusCode = New CS()
                                        obEntry.statusCode.code = "completed"
                                        obEntry.statusCode.codeSystem = Nothing
                                        obEntry.statusCode.codeSystemName = Nothing
                                        obEntry.statusCode.codeSystemVersion = Nothing
                                        obEntry.statusCode.displayName = Nothing

                                        obEntry.value = New ANY(0) {}
                                        obEntry.value(0) = New INT
                                        If _dtMeasures.Rows(measures)(k) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(k)) <> "" Then
                                            DirectCast(obEntry.value(0), INT).value = _dtMeasures.Rows(measures)(k)
                                        Else
                                            DirectCast(obEntry.value(0), INT).value = Nothing
                                        End If


                                        obEntry.methodCode = New CE(0) {}
                                        obEntry.methodCode(0) = New CE()
                                        obEntry.methodCode(0).code = "COUNT"
                                        obEntry.methodCode(0).displayName = "Count"
                                        obEntry.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                                        obEntry.methodCode(0).codeSystemName = "ObservationMethod"
                                        obEntry.methodCode(0).codeSystemVersion = Nothing
                                    End If
                                    ob.reference = New POCD_MT000040UV02Reference(0) {}
                                    ob.reference(0) = New POCD_MT000040UV02Reference
                                    ob.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                                    ob.reference(0).Item = New POCD_MT000040UV02ExternalObservation
                                    obReferenceExt = DirectCast(ob.reference(0).Item, POCD_MT000040UV02ExternalObservation)
                                    obReferenceExt.classCode = ActClassObservation.OBS
                                    obReferenceExt.moodCode = "EVN"
                                    obReferenceExt.moodCodeSpecified = True
                                    obReferenceExt.id = New II(0) {}
                                    obReferenceExt.id(0) = New II()
                                    If Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation1" Then
                                        If _dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_InitialPatientPopulation1ID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation2" Then
                                        If _dtMeasures.Rows(measures)("InitialPatientPopulation2ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation2ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("InitialPatientPopulation2ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "InitialPatientPopulation3" Then
                                        If _dtMeasures.Rows(measures)("InitialPatientPopulation3ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("InitialPatientPopulation3ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("InitialPatientPopulation3ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Denominator1" Then

                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1Denominator1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Denominator1ID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1Denominator1ID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Denominator1" Then

                                        If _dtMeasures.Rows(measures)("IPP2Denominator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2Denominator1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP2Denominator1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If

                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Denominator1" Then

                                        If _dtMeasures.Rows(measures)("IPP3Denominator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3Denominator1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP3Denominator1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator1" Then
                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator1ID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2Numerator1" Then
                                        If _dtMeasures.Rows(measures)("IPP2Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2Numerator1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP2Numerator1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP3Numerator1" Then
                                        If _dtMeasures.Rows(measures)("IPP3Numerator1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3Numerator1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP3Numerator1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator2" Then
                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator2ID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator2ID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1Numerator2ID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoExclusion" Then
                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExclusionID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExclusionID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExclusionID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoExclusion1" Then
                                        If _dtMeasures.Rows(measures)("IPP2DenoExclusion1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoExclusion1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP2DenoExclusion1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoExclusion1" Then
                                        If _dtMeasures.Rows(measures)("IPP3DenoExclusion1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3DenoExclusion1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP3DenoExclusion1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1DenoException" Then
                                        If _dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExceptionID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExceptionID)) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_IPP1DenoExceptionID)
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP2DenoException1" Then
                                        If _dtMeasures.Rows(measures)("IPP2DenoException1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP2DenoException1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP2DenoException1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP3DenoException1" Then
                                        If _dtMeasures.Rows(measures)("IPP3DenoException1ID") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("IPP3DenoException1ID")) <> "" Then
                                            obReferenceExt.id(0).root = _dtMeasures.Rows(measures)("IPP3DenoException1ID")
                                        Else
                                            obReferenceExt.id(0).root = Nothing
                                        End If
                                    ElseIf Convert.ToString(_dtMeasures.Columns(k)) = "IPP1Numerator Exclusion" Then
                                        ' obReferenceExt.id(0).root = ""
                                    Else
                                        obReferenceExt.id(0).root = Nothing
                                    End If


                                    ''

                                    obReferenceExt.id(0).extension = Nothing
                                    obReferenceExt.id(0).assigningAuthorityName = Nothing
                                    '''''''''''
                                    If Not IsNothing(_PerformanceRate) Then
                                        _moduleList.Add(_PerformanceRate)
                                    End If

                            End Select
                        End If




                    Next
                    tblcnt = tblcnt + 1
                    _component4 = New POCD_MT000040UV02Component4(_moduleList.Count) {}
                    _moduleList.CopyTo(_component4)
                    org.component = _component4
                Next

                section.entry(dtMeasures.Rows.Count) = New POCD_MT000040UV02Entry()



                Dim act As POCD_MT000040UV02Act = Nothing

                section.entry(dtMeasures.Rows.Count).typeCode = x_ActRelationshipEntry.DRIV
                section.entry(dtMeasures.Rows.Count).Item = New POCD_MT000040UV02Act()
                act = DirectCast(section.entry(dtMeasures.Rows.Count).Item, POCD_MT000040UV02Act)
                act.classCode = x_ActClassDocumentEntryAct.ACT
                act.classCodeSpecified = True
                act.moodCode = x_DocumentActMood.EVN
                act.moodCodeSpecified = True
                act.id = New II(0) {}
                act.id(0) = New II()
                act.id(0).root = Guid.NewGuid().ToString()
                act.id(0).extension = Nothing
                act.id(0).assigningAuthorityName = Nothing
                If _nQRDAFileType = CDAFileTypeEnum.QRDA3 Then
                    act.templateId = New II(2) {}
                    act.templateId(0) = New II()
                    act.templateId(0).root = "2.16.840.1.113883.10.20.17.3.8"

                    act.templateId(0).extension = Nothing
                    act.templateId(0).assigningAuthorityName = Nothing
                    act.templateId(2) = New II()
                    act.templateId(2).root = "2.16.840.1.113883.10.20.27.3.23"

                    act.templateId(2).extension = "2016-11-01"
                    act.templateId(2).assigningAuthorityName = Nothing
                ElseIf _nQRDAFileType = CDAFileTypeEnum.QRDA1 Then
                    act.templateId = New II(3) {}
                    act.templateId(0) = New II()
                    act.templateId(0).root = "2.16.840.1.113883.10.20.17.3.8"

                    act.templateId(0).extension = Nothing
                    act.templateId(0).assigningAuthorityName = Nothing

                    act.templateId(1) = New II()
                    act.templateId(1).root = "2.16.840.1.113883.10.20.17.3.8"

                    act.templateId(1).extension = "2015-07-01"
                    act.templateId(1).assigningAuthorityName = Nothing
                    act.templateId(2) = New II()
                    act.templateId(2).root = "2.16.840.1.113883.10.20.27.3.23"

                    act.templateId(2).extension = Nothing
                    act.templateId(2).assigningAuthorityName = Nothing
                End If



                'As per Qualitynet

                act.code = New CD()

                act.code.code = "252116004"
                act.code.codeSystem = "2.16.840.1.113883.6.96"
                act.code.codeSystemName = Nothing
                act.code.codeSystemVersion = Nothing
                act.code.displayName = "Observation Parameters"
                act.effectiveTime = New IVL_TS()
                ''As per QualityNet after first validation on CMS
                act.effectiveTime.operator = Nothing
                act.effectiveTime.value = Nothing
                act.effectiveTime.ItemsElementName = New ItemsChoiceType2(1) {}
                act.effectiveTime.ItemsElementName(0) = ItemsChoiceType2.low
                act.effectiveTime.ItemsElementName(1) = ItemsChoiceType2.high
                act.effectiveTime.Items = New QTY(1) {}
                act.effectiveTime.Items(0) = New IVXB_TS()
                If Not IsNothing(_measurementStartDate) AndAlso _measurementStartDate <> "" Then
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Format(CType(_measurementStartDate, Date), "yyyyMMdd")
                Else
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).nullFlavor = "UNK"
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Nothing
                End If

                act.effectiveTime.Items(1) = New IVXB_TS()
                If Not IsNothing(_measurementEndDate) AndAlso _measurementEndDate <> "" Then
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Format(CType(_measurementEndDate, Date), "yyyyMMdd")
                Else
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).nullFlavor = "UNK"
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Nothing
                End If




                ''

            End If
            Return _Measure
        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "QRDAIII Measures Section"
            Else
                _msgString = _msgString & vbNewLine & "QRDAIII Measures Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing


        Finally
            If Not IsNothing(section) Then
                section = Nothing
            End If


            If Not IsNothing(ob) Then
                ob = Nothing
            End If
            If Not IsNothing(table) Then
                table = Nothing
            End If
            If Not IsNothing(extDoc) Then
                extDoc = Nothing
            End If
            If Not IsNothing(org) Then
                org = Nothing
            End If
            If Not IsNothing(obEntry) Then
                obEntry = Nothing
            End If
            If Not IsNothing(obEntryRelation) Then
                obEntryRelation = Nothing
            End If
            If Not IsNothing(obReferenceExt) Then
                obReferenceExt = Nothing
            End If
            If Not IsNothing(obcontVarReferenceExt) Then
                obcontVarReferenceExt = Nothing
            End If
            'If Not IsNothing(dvCodes) Then
            '    dvCodes.Dispose()
            '    dvCodes = Nothing
            'End If
            If Not IsNothing(_moduleList) Then

                _moduleList = Nothing
            End If

            If Not IsNothing(_component4) Then

                _component4 = Nothing
            End If
            If Not IsNothing(_PerformanceRate) Then

                _PerformanceRate = Nothing
            End If
            If Not IsNothing(_oEntry) Then

                _oEntry = Nothing
            End If
        End Try
    End Function

    Private Function GetQRDAIIIACIMeasureComponent() As POCD_MT000040UV02Component3
        Dim _Measure As POCD_MT000040UV02Component3 = Nothing
        Dim section As POCD_MT000040UV02Section = Nothing

        Dim table As StrucDocTable = Nothing
        Dim extDoc As POCD_MT000040UV02ExternalDocument = Nothing
        Dim org As POCD_MT000040UV02Organizer = Nothing

       

        Dim _moduleList As List(Of POCD_MT000040UV02Component4) = Nothing
        
        Dim _component4 As POCD_MT000040UV02Component4() = Nothing
        Dim _PerformanceRate As POCD_MT000040UV02Component4 = Nothing
        Dim _oEntry As POCD_MT000040UV02EntryRelationship = Nothing
        Dim List As StrucDocList = Nothing

        Try
            If Not IsNothing(_dtMeasures) AndAlso _dtMeasures.Rows.Count > 0 Then
                'Dim strMeasure(0) As String


                _Measure = New POCD_MT000040UV02Component3
                DirectCast(_Measure, POCD_MT000040UV02Component3).section = New POCD_MT000040UV02Section()
                section = DirectCast(_Measure, POCD_MT000040UV02Component3).section
                'TempMayuri
                ' 'section.ID = Nothing
                section.classCode = Nothing
                section.classCodeSpecified = False
                section.moodCode = Nothing
                section.moodCodeSpecified = False

                ''As per QualityNet Standard
                section.templateId = New II(2) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.27.2.5"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = "2017-06-01"

                section.templateId(1) = New II()
                section.templateId(1).root = "2.16.840.1.113883.10.20.24.2.2"
                section.templateId(1).assigningAuthorityName = Nothing
                section.templateId(1).extension = Nothing


                section.code = New CE()

                section.code.code = "55186-1"

                section.code.codeSystem = CodeSystem.LOINC
                section.code.codeSystemName = Nothing
                section.code.codeSystemVersion = Nothing
                section.code.displayName = Nothing
                'nothing
                section.title = New ST()
                section.title.Text = New String() {"Measure Section"}
                ''As per QualityNet after first validation on CMS
                section.title.representation = Nothing
                section.title.mediaType = Nothing

                ''
                section.title.language = Nothing
                section.text = New StrucDocText()
                section.text.mediaType = Nothing
                section.text.ID = Nothing
                section.text.language = Nothing
                section.text.Items = New Object(_dtMeasures.Rows.Count * 2) {}
                section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count + 1) {}



                Dim tblcnt As Integer = 0
                For measures As Integer = 0 To _dtMeasures.Rows.Count - 1
                   


                    ''''''
                    section.text.Items(tblcnt) = New StrucDocTable()
                    table = DirectCast(section.text.Items(tblcnt), StrucDocTable)
                    table.ID = Nothing
                    table.language = Nothing
                    table.border = "1"
                    table.width = "100%"
                    table.thead = New StrucDocThead()
                    table.thead.ID = Nothing
                    table.thead.language = Nothing
                    table.thead.tr = New StrucDocTr(0) {}

                    table.thead.tr(0) = New StrucDocTr()
                    table.thead.tr(0).ID = Nothing
                    table.thead.tr(0).language = Nothing
                    table.thead.tr(0).Items = New Object(5) {}
                    table.thead.tr(0).Items(0) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).Text = New String() {"ACI Measure Title"}
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(0), StrucDocTh).language = Nothing
                    table.thead.tr(0).Items(1) = New StrucDocTh()
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).Text = New String() {"Measure Identifier"}
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).ID = Nothing
                    DirectCast(table.thead.tr(0).Items(1), StrucDocTh).language = Nothing
                   


                    table.tbody = New StrucDocTbody(0) {}
                    table.tbody(0) = New StrucDocTbody()
                    table.tbody(0).ID = Nothing
                    table.tbody(0).language = Nothing
                    table.tbody(0).tr = New StrucDocTr(0) {}
                    '   section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count - 1) {}


                    table.tbody(0).tr(0) = New StrucDocTr()
                    table.tbody(0).tr(0).Items = New Object(5) {}
                    table.tbody(0).tr(0).Items(0) = New StrucDocTd()
                    table.tbody(0).tr(0).ID = Nothing
                    table.tbody(0).tr(0).language = Nothing

                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)("MeasureName")}
                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(0), StrucDocTd).language = Nothing
                    table.tbody(0).tr(0).Items(1) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)("MeasureID")}
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(0).Items(1), StrucDocTd).language = Nothing



                    List = New StrucDocList
                    tblcnt = tblcnt + 1
                    section.text.Items(tblcnt) = New StrucDocList
                    List = DirectCast(section.text.Items(tblcnt), StrucDocList)
                    List.ID = Nothing
                    List.language = Nothing
                    List.item = New StrucDocItem(_dtMeasures.Rows.Count * 2) {}



                    List.item(tblcnt) = New StrucDocItem
                    List.item(tblcnt).ID = Nothing
                    List.item(tblcnt).language = Nothing
                    If _dtMeasures.Rows(measures)("MeasureName") = "Security Risk Analysis" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Immunization Registry Reporting" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Syndromic Surveillance Reporting" Then
                        List.item(tblcnt).Items = New Object(2) {}
                        List.item(tblcnt).Items(0) = New StrucDocContent
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).Text = New String() {"Measure Answer (Yes/No): "}
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).language = Nothing
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).styleCode = "Bold"

                        List.item(tblcnt).Items(1) = New StrucDocContent
                        If Convert.ToString(_dtMeasures.Rows(measures)("Numerator")) = "1" Then
                            DirectCast(List.item(tblcnt).Items(1), StrucDocContent).Text = New String() {"Yes"}
                        Else
                            DirectCast(List.item(tblcnt).Items(1), StrucDocContent).Text = New String() {"No"}
                        End If

                        DirectCast(List.item(tblcnt).Items(1), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(1), StrucDocContent).language = Nothing
                    Else


                        List.item(tblcnt).Items = New Object(4) {}
                        List.item(tblcnt).Items(0) = New StrucDocContent
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).Text = New String() {"Denominator: "}
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).language = Nothing
                        DirectCast(List.item(tblcnt).Items(0), StrucDocContent).styleCode = "Bold"

                        List.item(tblcnt).Items(1) = New StrucDocContent
                        DirectCast(List.item(tblcnt).Items(1), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)("Denominator"))}
                        DirectCast(List.item(tblcnt).Items(1), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(1), StrucDocContent).language = Nothing

                        List.item(tblcnt).Items(2) = New StrucDocContent
                        DirectCast(List.item(tblcnt).Items(2), StrucDocContent).Text = New String() {"Numerator: "}
                        DirectCast(List.item(tblcnt).Items(2), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(2), StrucDocContent).language = Nothing
                        DirectCast(List.item(tblcnt).Items(2), StrucDocContent).styleCode = "Bold"

                        List.item(tblcnt).Items(3) = New StrucDocContent
                        DirectCast(List.item(tblcnt).Items(3), StrucDocContent).Text = New String() {Convert.ToString(_dtMeasures.Rows(measures)("Numerator"))}
                        DirectCast(List.item(tblcnt).Items(3), StrucDocContent).ID = Nothing
                        DirectCast(List.item(tblcnt).Items(3), StrucDocContent).language = Nothing
                    End If
                    tblcnt = tblcnt + 1
                    ''''''


                    section.entry(measures) = New POCD_MT000040UV02Entry()
                    _moduleList = New List(Of POCD_MT000040UV02Component4)

                    section.entry(measures).typeCode = Nothing
                    section.entry(measures).typeCodeSpecified = False
                    section.entry(measures).Item = New POCD_MT000040UV02Organizer()
                    org = DirectCast(section.entry(measures).Item, POCD_MT000040UV02Organizer)
                    org.classCode = x_ActClassDocumentEntryOrganizer.CLUSTER
                    org.moodCode = "EVN"
                    org.moodCodeSpecified = True


                    org.templateId = New II(2) {}
                    org.templateId(0) = New II()
                    org.templateId(0).root = "2.16.840.1.113883.10.20.24.3.98"
                    org.templateId(0).extension = Nothing
                    org.templateId(0).assigningAuthorityName = Nothing
                    If _dtMeasures.Rows(measures)("MeasureName") = "Security Risk Analysis" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Immunization Registry Reporting" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Syndromic Surveillance Reporting" Then
                        org.templateId(1) = New II()
                        org.templateId(1).root = "2.16.840.1.113883.10.20.27.3.29"
                        org.templateId(1).extension = "2016-09-01"
                        org.templateId(1).assigningAuthorityName = Nothing
                    Else
                        org.templateId(1) = New II()
                        org.templateId(1).root = "2.16.840.1.113883.10.20.27.3.28"
                        org.templateId(1).extension = "2017-06-01"
                        org.templateId(1).assigningAuthorityName = Nothing
                    End If


                   
                    org.id = New II(0) {}
                    org.id(0) = New II()
                    org.id(0).nullFlavor = "NA"
                    org.id(0).assigningAuthorityName = Nothing
                    org.id(0).extension = Nothing
                    org.id(0).root = Nothing
                    ''end

                    org.statusCode = New CS()
                    org.statusCode.code = "completed"
                    org.statusCode.codeSystem = Nothing
                    org.statusCode.codeSystemName = Nothing
                    org.statusCode.codeSystemVersion = Nothing
                    org.statusCode.displayName = Nothing


                    org.reference = New POCD_MT000040UV02Reference(0) {}
                    org.reference(0) = New POCD_MT000040UV02Reference
                    org.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                    org.reference(0).realmCode = Nothing
                    org.reference(0).Item = New POCD_MT000040UV02ExternalDocument
                    extDoc = DirectCast(org.reference(0).Item, POCD_MT000040UV02ExternalDocument)
                    extDoc.classCode = ActClassDocument.DOC
                    extDoc.moodCode = "EVN"
                    extDoc.moodCodeSpecified = True
                    extDoc.id = New II(0) {}
                    extDoc.id(0) = New II

                    If _dtMeasures.Rows(measures)("MeasureName") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("MeasureName")) <> "" Then
                        extDoc.id(0).root = "2.16.840.1.113883.3.7031"
                        extDoc.id(0).extension = _dtMeasures.Rows(measures)("MeasureID")
                        extDoc.text = New ED()
                        extDoc.text.Text = New String() {_dtMeasures.Rows(measures)("MeasureName")}
                        extDoc.text.language = Nothing
                        extDoc.text.mediaType = Nothing
                    Else
                        extDoc.id(0).root = "2.16.840.1.113883.3.7031"
                        extDoc.id(0).extension = Nothing
                        extDoc.text = New ED()
                        extDoc.text.Text = Nothing
                        extDoc.text.language = Nothing
                        extDoc.text.mediaType = Nothing

                    End If

                    If _dtMeasures.Rows(measures)("MeasureName") = "Security Risk Analysis" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Syndromic Surveillance Reporting" OrElse _dtMeasures.Rows(measures)("MeasureName") = "Immunization Registry Reporting" Then
                        Dim _ComponentType As POCD_MT000040UV02Component4
                        _ComponentType = New POCD_MT000040UV02Component4
                        Dim obComponentType As New POCD_MT000040UV02Observation
                        DirectCast(_ComponentType, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                        obComponentType = DirectCast(_ComponentType, POCD_MT000040UV02Component4).Item
                        obComponentType.classCode = ActClassObservation.OBS
                        obComponentType.moodCode = x_ActMoodDocumentObservation.EVN


                        ''
                        obComponentType.templateId = New II(2) {}
                        obComponentType.templateId(0) = New II()
                        obComponentType.templateId(0).root = "2.16.840.1.113883.10.20.27.3.27"
                        obComponentType.templateId(0).assigningAuthorityName = Nothing
                        obComponentType.templateId(0).extension = "2016-09-01"
                        obComponentType.code = New CD()
                        obComponentType.code.code = "ASSERTION"
                        obComponentType.code.codeSystem = "2.16.840.1.113883.5.4"
                        obComponentType.code.codeSystemName = "ActCode"
                        obComponentType.code.displayName = "Assertion"

                        obComponentType.statusCode = New CS()
                        obComponentType.statusCode.code = "completed"
                        'obNumeratorType.value = New CD(0) {}
                        obComponentType.value = New CD(0) {}
                        obComponentType.value(0) = New CD
                        If _dtMeasures.Rows(measures)("Numerator") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("Numerator")) = "1" Then
                            DirectCast(obComponentType.value(0), CD).code = "Y"
                            DirectCast(obComponentType.value(0), CD).codeSystem = "2.16.840.1.113883.12.136"
                            DirectCast(obComponentType.value(0), CD).codeSystemName = "Yes/no indicator (HL7 Table 0136)"
                            DirectCast(obComponentType.value(0), CD).codeSystemVersion = Nothing
                            DirectCast(obComponentType.value(0), CD).displayName = "Yes"
                        Else
                            DirectCast(obComponentType.value(0), CD).code = "N"
                            DirectCast(obComponentType.value(0), CD).codeSystem = "2.16.840.1.113883.12.136"
                            DirectCast(obComponentType.value(0), CD).codeSystemName = "Yes/no indicator (HL7 Table 0136)"
                            DirectCast(obComponentType.value(0), CD).codeSystemVersion = Nothing
                            DirectCast(obComponentType.value(0), CD).displayName = "No"
                        End If



                        If Not IsNothing(_ComponentType) Then
                            _moduleList.Add(_ComponentType)
                        End If
                    Else

                        Dim _NumeratorType As POCD_MT000040UV02Component4
                        _NumeratorType = New POCD_MT000040UV02Component4
                        Dim obNumeratorType As New POCD_MT000040UV02Observation
                        DirectCast(_NumeratorType, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                        obNumeratorType = DirectCast(_NumeratorType, POCD_MT000040UV02Component4).Item
                        obNumeratorType.classCode = ActClassObservation.OBS
                        obNumeratorType.moodCode = x_ActMoodDocumentObservation.EVN


                        ''
                        obNumeratorType.templateId = New II(2) {}
                        obNumeratorType.templateId(0) = New II()
                        obNumeratorType.templateId(0).root = "2.16.840.1.113883.10.20.27.3.31"
                        obNumeratorType.templateId(0).assigningAuthorityName = Nothing
                        obNumeratorType.templateId(0).extension = "2016-09-01"
                        obNumeratorType.code = New CD()
                        obNumeratorType.code.code = "ASSERTION"
                        obNumeratorType.code.codeSystem = "2.16.840.1.113883.5.4"
                        obNumeratorType.code.codeSystemName = "ActCode"
                        obNumeratorType.code.displayName = "Assertion"

                        obNumeratorType.statusCode = New CS()
                        obNumeratorType.statusCode.code = "completed"
                        'obNumeratorType.value = New CD(0) {}
                        obNumeratorType.value = New CD(0) {}
                        obNumeratorType.value(0) = New CD
                        DirectCast(obNumeratorType.value(0), CD).code = "NUMER"
                        DirectCast(obNumeratorType.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"
                        DirectCast(obNumeratorType.value(0), CD).codeSystemName = "ActCode"
                        DirectCast(obNumeratorType.value(0), CD).codeSystemVersion = Nothing

                        ' obNumeratorType.entryRelationship = New POCD_MT000040UV02EntryRelationship() {}
                        obNumeratorType.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                        obNumeratorType.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                        obNumeratorType.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
                        obNumeratorType.entryRelationship(0).inversionInd = True
                        obNumeratorType.entryRelationship(0).inversionIndSpecified = True

                        Dim obNumeratorTypeRelation As New POCD_MT000040UV02Observation
                        obNumeratorType.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                        obNumeratorTypeRelation = DirectCast(obNumeratorType.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                        obNumeratorTypeRelation.classCode = ActClassObservation.OBS
                        obNumeratorTypeRelation.moodCode = x_ActMoodDocumentObservation.EVN
                        obNumeratorTypeRelation.moodCodeSpecified = True
                        obNumeratorTypeRelation.templateId = New II(1) {}
                        obNumeratorTypeRelation.templateId(0) = New II()
                        obNumeratorTypeRelation.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
                        obNumeratorTypeRelation.templateId(0).extension = Nothing
                        obNumeratorTypeRelation.templateId(0).assigningAuthorityName = Nothing



                        obNumeratorTypeRelation.code = New CD()
                        obNumeratorTypeRelation.code.code = "MSRAGG"
                        obNumeratorTypeRelation.code.displayName = "rate aggregation"
                        obNumeratorTypeRelation.code.codeSystem = "2.16.840.1.113883.5.4"
                        obNumeratorTypeRelation.code.codeSystemName = "ActCode"
                        obNumeratorTypeRelation.code.codeSystemVersion = Nothing
                        ''As per QualityNet Standard
                        obNumeratorTypeRelation.statusCode = New CS()
                        obNumeratorTypeRelation.statusCode.code = "completed"
                        obNumeratorTypeRelation.statusCode.codeSystem = Nothing
                        obNumeratorTypeRelation.statusCode.codeSystemName = Nothing
                        obNumeratorTypeRelation.statusCode.codeSystemVersion = Nothing
                        obNumeratorTypeRelation.statusCode.displayName = Nothing

                        obNumeratorTypeRelation.value = New ANY(0) {}
                        obNumeratorTypeRelation.value(0) = New INT
                        ' If Not IsNothing(_dtMeasures.Rows(measures)("Numerator")) AndAlso dvCodes.Count > 0 Then
                        If _dtMeasures.Rows(measures)("Numerator") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("Numerator")) <> "" Then
                            DirectCast(obNumeratorTypeRelation.value(0), INT).value = _dtMeasures.Rows(measures)("Numerator")
                            'dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)
                        Else
                            DirectCast(obNumeratorTypeRelation.value(0), INT).value = "0"
                        End If

                       

                        obNumeratorTypeRelation.methodCode = New CE(0) {}
                        obNumeratorTypeRelation.methodCode(0) = New CE()
                        obNumeratorTypeRelation.methodCode(0).code = "COUNT"
                        obNumeratorTypeRelation.methodCode(0).displayName = "Count"
                        obNumeratorTypeRelation.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                        obNumeratorTypeRelation.methodCode(0).codeSystemName = "ObservationMethod"
                        obNumeratorTypeRelation.methodCode(0).codeSystemVersion = Nothing
                        ''
                        If Not IsNothing(_NumeratorType) Then
                            _moduleList.Add(_NumeratorType)
                        End If

                        ''Denominator
                        Dim _DenominatorType As POCD_MT000040UV02Component4
                        _DenominatorType = New POCD_MT000040UV02Component4
                        Dim obDenominatorType As New POCD_MT000040UV02Observation
                        DirectCast(_DenominatorType, POCD_MT000040UV02Component4).Item = New POCD_MT000040UV02Observation()
                        obDenominatorType = DirectCast(_DenominatorType, POCD_MT000040UV02Component4).Item
                        obDenominatorType.classCode = ActClassObservation.OBS
                        obDenominatorType.moodCode = x_ActMoodDocumentObservation.EVN


                        ''
                        obDenominatorType.templateId = New II(2) {}
                        obDenominatorType.templateId(0) = New II()
                        obDenominatorType.templateId(0).root = "2.16.840.1.113883.10.20.27.3.32"
                        obDenominatorType.templateId(0).assigningAuthorityName = Nothing
                        obDenominatorType.templateId(0).extension = "2016-09-01"
                        obDenominatorType.code = New CD()
                        obDenominatorType.code.code = "ASSERTION"
                        obDenominatorType.code.codeSystem = "2.16.840.1.113883.5.4"
                        obDenominatorType.code.codeSystemName = "ActCode"
                        obDenominatorType.code.displayName = "Assertion"

                        obDenominatorType.statusCode = New CS()
                        obDenominatorType.statusCode.code = "completed"
                        'obNumeratorType.value = New CD(0) {}
                        obDenominatorType.value = New CD(0) {}
                        obDenominatorType.value(0) = New CD
                        DirectCast(obDenominatorType.value(0), CD).code = "DENOM"
                        DirectCast(obDenominatorType.value(0), CD).codeSystem = "2.16.840.1.113883.5.4"
                        DirectCast(obDenominatorType.value(0), CD).codeSystemName = "ActCode"
                        DirectCast(obDenominatorType.value(0), CD).codeSystemVersion = Nothing

                        ' obNumeratorType.entryRelationship = New POCD_MT000040UV02EntryRelationship() {}
                        obDenominatorType.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                        obDenominatorType.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                        obDenominatorType.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
                        obDenominatorType.entryRelationship(0).inversionInd = True
                        obDenominatorType.entryRelationship(0).inversionIndSpecified = True

                        Dim obDenominatorTypeRelation As New POCD_MT000040UV02Observation
                        obDenominatorType.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                        obDenominatorTypeRelation = DirectCast(obDenominatorType.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                        obDenominatorTypeRelation.classCode = ActClassObservation.OBS
                        obDenominatorTypeRelation.moodCode = x_ActMoodDocumentObservation.EVN

                        obDenominatorTypeRelation.moodCodeSpecified = True
                        obDenominatorTypeRelation.templateId = New II(1) {}
                        obDenominatorTypeRelation.templateId(0) = New II()
                        obDenominatorTypeRelation.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
                        obDenominatorTypeRelation.templateId(0).extension = Nothing
                        obDenominatorTypeRelation.templateId(0).assigningAuthorityName = Nothing



                        obDenominatorTypeRelation.code = New CD()
                        obDenominatorTypeRelation.code.code = "MSRAGG"
                        obDenominatorTypeRelation.code.displayName = "rate aggregation"
                        obDenominatorTypeRelation.code.codeSystem = "2.16.840.1.113883.5.4"
                        obDenominatorTypeRelation.code.codeSystemName = "ActCode"
                        obDenominatorTypeRelation.code.codeSystemVersion = Nothing
                        ''As per QualityNet Standard
                        obDenominatorTypeRelation.statusCode = New CS()
                        obDenominatorTypeRelation.statusCode.code = "completed"
                        obDenominatorTypeRelation.statusCode.codeSystem = Nothing
                        obDenominatorTypeRelation.statusCode.codeSystemName = Nothing
                        obDenominatorTypeRelation.statusCode.codeSystemVersion = Nothing
                        obDenominatorTypeRelation.statusCode.displayName = Nothing

                        obDenominatorTypeRelation.value = New ANY(0) {}
                        obDenominatorTypeRelation.value(0) = New INT
                        ' If Not IsNothing(_dtMeasures.Rows(measures)("Numerator")) AndAlso dvCodes.Count > 0 Then
                        If _dtMeasures.Rows(measures)("Numerator") IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)("Denominator")) <> "" Then
                            DirectCast(obDenominatorTypeRelation.value(0), INT).value = _dtMeasures.Rows(measures)("Denominator")
                            'dvCodes.ToTable.Rows(_cnt)(Col_Codes_Count)
                        Else
                            DirectCast(obDenominatorTypeRelation.value(0), INT).value = "0"
                        End If

                        obDenominatorTypeRelation.methodCode = New CE(0) {}
                        obDenominatorTypeRelation.methodCode(0) = New CE()
                        obDenominatorTypeRelation.methodCode(0).code = "COUNT"
                        obDenominatorTypeRelation.methodCode(0).displayName = "Count"
                        obDenominatorTypeRelation.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
                        obDenominatorTypeRelation.methodCode(0).codeSystemName = "ObservationMethod"
                        obDenominatorTypeRelation.methodCode(0).codeSystemVersion = Nothing
                        ''
                        If Not IsNothing(_DenominatorType) Then
                            _moduleList.Add(_DenominatorType)
                        End If
                    End If

                    _component4 = New POCD_MT000040UV02Component4(_moduleList.Count) {}
                    _moduleList.CopyTo(_component4)
                    org.component = _component4
                Next


                section.entry(dtMeasures.Rows.Count) = New POCD_MT000040UV02Entry()



                Dim act As POCD_MT000040UV02Act = Nothing

                section.entry(dtMeasures.Rows.Count).typeCode = x_ActRelationshipEntry.DRIV
                section.entry(dtMeasures.Rows.Count).Item = New POCD_MT000040UV02Act()
                act = DirectCast(section.entry(dtMeasures.Rows.Count).Item, POCD_MT000040UV02Act)
                act.classCode = x_ActClassDocumentEntryAct.ACT
                act.classCodeSpecified = True
                act.moodCode = x_DocumentActMood.EVN
                act.moodCodeSpecified = True
                act.id = New II(0) {}
                act.id(0) = New II()
                act.id(0).root = Guid.NewGuid().ToString()
                act.id(0).extension = Nothing
                act.id(0).assigningAuthorityName = Nothing
                If _nQRDAFileType = CDAFileTypeEnum.QRDA3 Then
                    act.templateId = New II(2) {}
                    act.templateId(0) = New II()
                    act.templateId(0).root = "2.16.840.1.113883.10.20.17.3.8"

                    act.templateId(0).extension = Nothing
                    act.templateId(0).assigningAuthorityName = Nothing
                    act.templateId(2) = New II()
                    act.templateId(2).root = "2.16.840.1.113883.10.20.27.3.23"

                    act.templateId(2).extension = "2016-11-01"
                    act.templateId(2).assigningAuthorityName = Nothing

                End If



                'As per Qualitynet

                act.code = New CD()

                act.code.code = "252116004"
                act.code.codeSystem = "2.16.840.1.113883.6.96"
                act.code.codeSystemName = Nothing
                act.code.codeSystemVersion = Nothing
                act.code.displayName = "Observation Parameters"
                act.effectiveTime = New IVL_TS()
                ''As per QualityNet after first validation on CMS
                act.effectiveTime.operator = Nothing
                act.effectiveTime.value = Nothing
                act.effectiveTime.ItemsElementName = New ItemsChoiceType2(1) {}
                act.effectiveTime.ItemsElementName(0) = ItemsChoiceType2.low
                act.effectiveTime.ItemsElementName(1) = ItemsChoiceType2.high
                act.effectiveTime.Items = New QTY(1) {}
                act.effectiveTime.Items(0) = New IVXB_TS()
                If Not IsNothing(_measurementStartDate) AndAlso _measurementStartDate <> "" Then
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Format(CType(_measurementStartDate, Date), "yyyyMMdd")
                Else
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).nullFlavor = "UNK"
                    DirectCast(act.effectiveTime.Items(0), IVXB_TS).value = Nothing
                End If

                act.effectiveTime.Items(1) = New IVXB_TS()
                If Not IsNothing(_measurementEndDate) AndAlso _measurementEndDate <> "" Then
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Format(CType(_measurementEndDate, Date), "yyyyMMdd")
                Else
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).nullFlavor = "UNK"
                    DirectCast(act.effectiveTime.Items(1), IVXB_TS).value = Nothing
                End If





                ''

            End If
            Return _Measure
        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "QRDAIII Measures Section"
            Else
                _msgString = _msgString & vbNewLine & "QRDAIII Measures Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing


        Finally
            If Not IsNothing(section) Then
                section = Nothing
            End If


            'If Not IsNothing(ob) Then
            '    ob = Nothing
            'End If
            If Not IsNothing(table) Then
                table = Nothing
            End If
            If Not IsNothing(extDoc) Then
                extDoc = Nothing
            End If
            If Not IsNothing(org) Then
                org = Nothing
            End If
            'If Not IsNothing(obEntry) Then
            '    obEntry = Nothing
            'End If
            'If Not IsNothing(obEntryRelation) Then
            '    obEntryRelation = Nothing
            'End If
            'If Not IsNothing(obReferenceExt) Then
            '    obReferenceExt = Nothing
            'End If
            'If Not IsNothing(obcontVarReferenceExt) Then
            '    obcontVarReferenceExt = Nothing
            'End If
           
            If Not IsNothing(_moduleList) Then

                _moduleList = Nothing
            End If

            If Not IsNothing(_component4) Then

                _component4 = Nothing
            End If
            If Not IsNothing(_PerformanceRate) Then

                _PerformanceRate = Nothing
            End If
            If Not IsNothing(_oEntry) Then

                _oEntry = Nothing
            End If
        End Try
    End Function

    Private Sub GetGender(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, ByVal _cnt As Integer)


        Dim obEntryRelation As POCD_MT000040UV02Observation = Nothing
        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True
        ''As per QualiNet Standard
        'obEntry.id = New II(0) {}
        'obEntry.id(0) = New II()
        'obEntry.id(0).nullFlavor = "NA"
        'obEntry.id(0).root = Nothing
        'obEntry.id(0).extension = Nothing
        'obEntry.id(0).assigningAuthorityName = Nothing

        obEntry.templateId = New II(1) {}
        obEntry.templateId(0) = New II
        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.6"
        obEntry.templateId(0).extension = "2016-09-01"
        obEntry.templateId(0).assigningAuthorityName = Nothing

        obEntry.templateId(1) = New II
        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.21"
        obEntry.templateId(1).extension = "2016-11-01"
        obEntry.templateId(1).assigningAuthorityName = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "76689-9"
        obEntry.code.codeSystem = CodeSystem.LOINC
        obEntry.code.codeSystemName = "LOINC"
        obEntry.code.codeSystemVersion = Nothing
        obEntry.code.displayName = "patient sex"

        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing


        obEntry.value = New CD(0) {}
        obEntry.value(0) = New CD
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)).Code
                DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)).Description
                DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.AdministrativeGender, dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)).CodingSystemName

            Else
                DirectCast(obEntry.value(0), CD).code = Nothing
                DirectCast(obEntry.value(0), CD).displayName = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing

            End If


            DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.AdministrativeGender

            DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
        Else
            obEntry.value = New CD(0) {}
            obEntry.value(0) = New CD
            DirectCast(obEntry.value(0), CD).nullFlavor = "UNK"
            DirectCast(obEntry.value(0), CD).code = Nothing
            DirectCast(obEntry.value(0), CD).displayName = Nothing
            DirectCast(obEntry.value(0), CD).codeSystemName = Nothing

            DirectCast(obEntry.value(0), CD).codeSystem = Nothing

            DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

            GetEntryRelation(obEntry, dvcodes, _cnt)
        End If


    End Sub
    Private Sub GetEntryRelation(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, ByVal _cnt As Integer)
        Dim obEntryRelation As POCD_MT000040UV02Observation = Nothing
        obEntry.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
        obEntry.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
        obEntry.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
        obEntry.entryRelationship(0).inversionInd = True
        obEntry.entryRelationship(0).inversionIndSpecified = True
        '' obEntry.entryRelationship(0).contextConductionInd = False


        obEntry.entryRelationship(0).Item = New POCD_MT000040UV02Observation
        obEntryRelation = DirectCast(obEntry.entryRelationship(0).Item, POCD_MT000040UV02Observation)
        obEntryRelation.classCode = ActClassObservation.OBS
        obEntryRelation.moodCode = x_ActMoodDocumentObservation.EVN
        obEntryRelation.moodCodeSpecified = True
        obEntryRelation.templateId = New II(1) {}
        obEntryRelation.templateId(0) = New II()
        obEntryRelation.templateId(0).root = "2.16.840.1.113883.10.20.27.3.3"
        obEntryRelation.templateId(0).extension = Nothing
        obEntryRelation.templateId(0).assigningAuthorityName = Nothing

        ''As per QualityNet Standard
        obEntryRelation.templateId(1) = New II()
        obEntryRelation.templateId(1).root = "2.16.840.1.113883.10.20.27.3.24"
        obEntryRelation.templateId(1).extension = Nothing
        obEntryRelation.templateId(1).assigningAuthorityName = Nothing

        obEntryRelation.code = New CD()
        obEntryRelation.code.code = "MSRAGG"
        obEntryRelation.code.displayName = "rate aggregation"
        obEntryRelation.code.codeSystem = "2.16.840.1.113883.5.4"
        obEntryRelation.code.codeSystemName = "ActCode"
        obEntryRelation.code.codeSystemVersion = Nothing
        ''As per QualityNet Standard
        obEntryRelation.statusCode = New CS()
        obEntryRelation.statusCode.code = "completed"
        obEntryRelation.statusCode.codeSystem = Nothing
        obEntryRelation.statusCode.codeSystemName = Nothing
        obEntryRelation.statusCode.codeSystemVersion = Nothing
        obEntryRelation.statusCode.displayName = Nothing

        obEntryRelation.value = New ANY(0) {}
        obEntryRelation.value(0) = New INT
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Count) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Count)) <> "" Then
                DirectCast(obEntryRelation.value(0), INT).value = dvcodes.ToTable.Rows(_cnt)(Col_Codes_Count)
            Else
                DirectCast(obEntryRelation.value(0), INT).value = "0"
            End If

        Else
            DirectCast(obEntryRelation.value(0), INT).value = "0"
        End If





        obEntryRelation.methodCode = New CE(0) {}
        obEntryRelation.methodCode(0) = New CE()
        obEntryRelation.methodCode(0).code = "COUNT"
        obEntryRelation.methodCode(0).displayName = "Count"
        obEntryRelation.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
        obEntryRelation.methodCode(0).codeSystemName = "ObservationMethod"
        obEntryRelation.methodCode(0).codeSystemVersion = Nothing
    End Sub
    Private Sub GetEthnicity(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, _cnt As Integer)



        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True
        'obEntry.id = New II(0) {}
        'obEntry.id(0) = New II()
        'obEntry.id(0).nullFlavor = "NA"
        'obEntry.id(0).root = Nothing
        'obEntry.id(0).extension = Nothing
        'obEntry.id(0).assigningAuthorityName = Nothing

        obEntry.templateId = New II(1) {}
        obEntry.templateId(0) = New II
        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.7"
        obEntry.templateId(0).extension = "2016-09-01"
        obEntry.templateId(0).assigningAuthorityName = Nothing
        ''As per Qualitynet Standard
        obEntry.templateId(1) = New II
        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.22"
        obEntry.templateId(1).extension = "2016-11-01"
        obEntry.templateId(1).assigningAuthorityName = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "69490-1"
        obEntry.code.codeSystem = CodeSystem.LOINC
        obEntry.code.codeSystemName = "LOINC"
        obEntry.code.codeSystemVersion = Nothing
        obEntry.code.displayName = "Ethnic"

        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing


        obEntry.value = New CD(0) {}
        obEntry.value(0) = New CD
        'DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Code
        'DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
        'DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).CodingSystemName
        'DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
        'DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Description
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode)) <> "" Then
                DirectCast(obEntry.value(0), CD).code = Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode))
                DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                DirectCast(obEntry.value(0), CD).codeSystemName = "OMB Standards for Race and Ethnicity"
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                    DirectCast(obEntry.value(0), CD).displayName = Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description))
                Else
                    DirectCast(obEntry.value(0), CD).displayName = Nothing
                End If
            Else
                DirectCast(obEntry.value(0), CD).nullFlavor = "NA"
                DirectCast(obEntry.value(0), CD).code = Nothing
                DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

                DirectCast(obEntry.value(0), CD).displayName = Nothing

            End If
        Else
            DirectCast(obEntry.value(0), CD).nullFlavor = "UNK"
            DirectCast(obEntry.value(0), CD).code = Nothing
            DirectCast(obEntry.value(0), CD).codeSystem = Nothing
            DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
            DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

            DirectCast(obEntry.value(0), CD).displayName = Nothing


            GetEntryRelation(obEntry, dvcodes, _cnt)
        End If


    End Sub
    Private Sub GetRace(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, _cnt As Integer)



        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True

        'obEntry.id = New II(0) {}
        'obEntry.id(0) = New II()
        'obEntry.id(0).nullFlavor = "NA"
        'obEntry.id(0).root = Nothing
        'obEntry.id(0).extension = Nothing
        'obEntry.id(0).assigningAuthorityName = Nothing

        obEntry.templateId = New II(1) {}
        obEntry.templateId(0) = New II
        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.8"
        obEntry.templateId(0).extension = "2016-09-01"
        obEntry.templateId(0).assigningAuthorityName = Nothing
        'As per QualityNet Standard
        obEntry.templateId(1) = New II
        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.19"
        obEntry.templateId(1).extension = "2016-11-01"
        obEntry.templateId(1).assigningAuthorityName = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "72826-1"
        obEntry.code.codeSystem = CodeSystem.LOINC
        obEntry.code.codeSystemName = "LOINC"
        obEntry.code.codeSystemVersion = Nothing
        obEntry.code.displayName = Nothing

        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing


        obEntry.value = New CD(0) {}
        obEntry.value(0) = New CD
        'DirectCast(obEntry.value(0), CD).code = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Code
        'DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
        'DirectCast(obEntry.value(0), CD).codeSystemName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).CodingSystemName
        'DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
        'DirectCast(obEntry.value(0), CD).displayName = oCodingSystemMaster.GetbyDescription(CodeSystem.RaceAndEthnicity, Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description))).Description
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode)) <> "" Then
                DirectCast(obEntry.value(0), CD).code = Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_DataCode))
                DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.RaceAndEthnicity
                DirectCast(obEntry.value(0), CD).codeSystemName = "OMB Standards for Race and Ethnicity"
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
                If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                    DirectCast(obEntry.value(0), CD).displayName = Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description))
                Else
                    DirectCast(obEntry.value(0), CD).displayName = Nothing
                End If
            Else
                DirectCast(obEntry.value(0), CD).nullFlavor = "NA"
                DirectCast(obEntry.value(0), CD).code = Nothing
                DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

                DirectCast(obEntry.value(0), CD).displayName = Nothing

            End If
        Else


            DirectCast(obEntry.value(0), CD).nullFlavor = "UNK"
            DirectCast(obEntry.value(0), CD).code = Nothing
            DirectCast(obEntry.value(0), CD).codeSystem = Nothing
            DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
            DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

            DirectCast(obEntry.value(0), CD).displayName = Nothing
            GetEntryRelation(obEntry, dvcodes, _cnt)
        End If



    End Sub
    Private Sub GetZipCode(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, _cnt As Integer)

        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True
        obEntry.id = New II(0) {}
        obEntry.id(0) = New II()
        obEntry.id(0).nullFlavor = "NA"
        obEntry.id(0).root = Nothing
        obEntry.id(0).extension = Nothing
        obEntry.id(0).assigningAuthorityName = Nothing

        obEntry.templateId = New II(0) {}
        obEntry.templateId(0) = New II

        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.10"
        obEntry.templateId(0).extension = Nothing
        obEntry.templateId(0).assigningAuthorityName = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "45401-7"
        ' obEntry.code.code = "184102003"
        obEntry.code.codeSystem = CodeSystem.LOINC
        obEntry.code.codeSystemName = "LOINC"
        obEntry.code.codeSystemVersion = Nothing
        obEntry.code.displayName = Nothing

        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing

        obEntry.value = New ANY(0) {}
        obEntry.value(0) = New ST()
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                DirectCast(obEntry.value(0), ST).Text = New String() {dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)}
            Else
                DirectCast(obEntry.value(0), ST).Text = Nothing
            End If

            DirectCast(obEntry.value(0), ST).language = Nothing
        Else

        End If


    End Sub

    Private Sub GetPayer(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, _cnt As Integer)

        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True
        obEntry.templateId = New II(2) {}
        obEntry.templateId(0) = New II

        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.9"
        obEntry.templateId(0).extension = "2016-02-01"
        obEntry.templateId(0).assigningAuthorityName = Nothing

        obEntry.templateId(1) = New II
        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.24.3.55"
        obEntry.templateId(1).extension = Nothing
        obEntry.templateId(1).assigningAuthorityName = Nothing


        'As per QaulityNet Standard
        obEntry.templateId(2) = New II
        obEntry.templateId(2).root = "2.16.840.1.113883.10.20.27.3.18"
        obEntry.templateId(2).extension = "2016-11-01"
        obEntry.templateId(2).assigningAuthorityName = Nothing

        obEntry.id = New II(0) {}
        obEntry.id(0) = New II()
        obEntry.id(0).nullFlavor = "NA"
        obEntry.id(0).assigningAuthorityName = Nothing
        obEntry.id(0).extension = Nothing
        obEntry.id(0).root = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "48768-6"
        obEntry.code.codeSystem = CodeSystem.LOINC
        obEntry.code.codeSystemName = "LOINC"
        obEntry.code.codeSystemVersion = Nothing
        obEntry.code.displayName = "Payment source"

        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing
        ''Start EffectiveTime For Cypress Nov release validation
        obEntry.effectiveTime = New IVL_TS()
        ''As per QualityNet after first validation on CMS
        obEntry.effectiveTime.operator = Nothing
        obEntry.effectiveTime.value = Nothing
        DirectCast(obEntry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(0) {}
        DirectCast(obEntry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low

        DirectCast(obEntry.effectiveTime, IVL_TS).Items = New QTY(0) {}
        DirectCast(obEntry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()

        DirectCast(DirectCast(obEntry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
        DirectCast(DirectCast(obEntry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
        ''End EffectiveTime


        obEntry.value = New CD(0) {}
        obEntry.value(0) = New CD


        ''
        If Not IsNothing(dvcodes) AndAlso dvcodes.Count > 0 Then
            If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then
                If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                    If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MA" Or dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MB" Then
                        DirectCast(obEntry.value(0), CD).displayName = "Medicare"
                        DirectCast(obEntry.value(0), CD).code = "1"
                        '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicare: "}
                    ElseIf dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MC" Then
                        DirectCast(obEntry.value(0), CD).displayName = "Medicaid"
                        DirectCast(obEntry.value(0), CD).code = "2"
                        '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicaid: "}
                    ElseIf dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "OT" Then
                        DirectCast(obEntry.value(0), CD).displayName = "Other"
                        DirectCast(obEntry.value(0), CD).code = "349"
                        '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}

                    Else
                        DirectCast(obEntry.value(0), CD).displayName = "Other"
                        DirectCast(obEntry.value(0), CD).code = "349"
                        ' DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}
                    End If
                Else
                    DirectCast(obEntry.value(0), CD).displayName = "Other"
                    DirectCast(obEntry.value(0), CD).code = "349"
                    ' DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}

                End If
                DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.PaymentTopology
                DirectCast(obEntry.value(0), CD).codeSystemName = "Source of Payment Typology"
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
            Else
                DirectCast(obEntry.value(0), CD).nullFlavor = "OTH"
                DirectCast(obEntry.value(0), CD).translation = New CD(0) {}
                DirectCast(obEntry.value(0), CD).translation(0) = New CD()
                If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
                    If dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "" Or dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MB" Then

                        DirectCast(obEntry.value(0), CD).translation(0).code = "A"
                        DirectCast(obEntry.value(0), CD).translation(0).displayName = "Medicare"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing
                        'DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicare: "}
                    ElseIf dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MC" Then

                        DirectCast(obEntry.value(0), CD).translation(0).code = "B"
                        DirectCast(obEntry.value(0), CD).translation(0).displayName = "Medicaid"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing
                        'DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicaid: "}
                    ElseIf dvcodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "OT" Then

                        DirectCast(obEntry.value(0), CD).translation(0).code = "D"
                        DirectCast(obEntry.value(0), CD).translation(0).displayName = "Other"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing
                        '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}
                    Else

                        DirectCast(obEntry.value(0), CD).translation(0).code = "D"
                        DirectCast(obEntry.value(0), CD).translation(0).displayName = "Other"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                        DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing
                        '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}
                    End If
                Else
                    DirectCast(obEntry.value(0), CD).translation(0).code = "D"
                    DirectCast(obEntry.value(0), CD).translation(0).displayName = "Other"
                    DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                    DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                    DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing
                    '  DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}


                End If

                DirectCast(obEntry.value(0), CD).displayName = Nothing
                DirectCast(obEntry.value(0), CD).code = Nothing
                DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

            End If

        Else
            If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then

                DirectCast(obEntry.value(0), CD).displayName = "Other"
                DirectCast(obEntry.value(0), CD).code = "349"
                ' DirectCast(DirectCast(List.item(k).Items(2), gloCCDSchema.StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}


                DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.PaymentTopology
                DirectCast(obEntry.value(0), CD).codeSystemName = "Source of Payment Typology"
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing
            Else
                DirectCast(obEntry.value(0), CD).nullFlavor = "OTH"
                DirectCast(obEntry.value(0), CD).translation = New CD(0) {}
                DirectCast(obEntry.value(0), CD).translation(0) = New CD()

                DirectCast(obEntry.value(0), CD).translation(0).code = "D"
                DirectCast(obEntry.value(0), CD).translation(0).displayName = "Other"
                DirectCast(obEntry.value(0), CD).translation(0).codeSystem = "2.16.840.1.113883.3.249.12"
                DirectCast(obEntry.value(0), CD).translation(0).codeSystemName = "CMS Clinical Codes"
                DirectCast(obEntry.value(0), CD).translation(0).codeSystemVersion = Nothing



                DirectCast(obEntry.value(0), CD).displayName = Nothing
                DirectCast(obEntry.value(0), CD).code = Nothing
                DirectCast(obEntry.value(0), CD).codeSystem = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemName = Nothing
                DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing

            End If
            GetEntryRelation(obEntry, dvcodes, _cnt)
        End If

    End Sub
    Private Sub GetStratrum1(ByVal obEntry As POCD_MT000040UV02Observation, ByVal dvcodes As DataView, _cnt As Integer)


        obEntry.classCode = ActClassObservation.OBS
        obEntry.moodCode = x_ActMoodDocumentObservation.EVN
        obEntry.moodCodeSpecified = True
        obEntry.templateId = New II(1) {}
        obEntry.templateId(0) = New II()
        obEntry.templateId(0).root = "2.16.840.1.113883.10.20.27.3.4"
        obEntry.templateId(0).extension = Nothing
        obEntry.templateId(0).assigningAuthorityName = Nothing
        ''As per QualityNet Standard
        obEntry.templateId(1) = New II()
        obEntry.templateId(1).root = "2.16.840.1.113883.10.20.27.3.20"
        obEntry.templateId(1).extension = Nothing
        obEntry.templateId(1).assigningAuthorityName = Nothing

        obEntry.code = New CD()
        obEntry.code.code = "ASSERTION"
        obEntry.code.codeSystem = "2.16.840.1.113883.5.4"
        obEntry.code.displayName = "Assertion"
        obEntry.code.codeSystemName = "ActCode"
        obEntry.code.codeSystemVersion = Nothing


        obEntry.statusCode = New CS()
        obEntry.statusCode.code = "completed"
        obEntry.statusCode.codeSystem = Nothing
        obEntry.statusCode.codeSystemName = Nothing
        obEntry.statusCode.codeSystemVersion = Nothing
        obEntry.statusCode.displayName = Nothing


        obEntry.value = New CD(0) {}
        obEntry.value(0) = New CD
        DirectCast(obEntry.value(0), CD).nullFlavor = "OTH"

        DirectCast(obEntry.value(0), CD).originalText = New ED()
        DirectCast(obEntry.value(0), CD).originalText.Text = New String() {"Stratum"}


        obEntry.methodCode = New CE(0) {}
        obEntry.methodCode(0) = New CE()
        obEntry.methodCode(0).code = "MEDIAN"
        obEntry.methodCode(0).displayName = "Median"
        obEntry.methodCode(0).codeSystem = "2.16.840.1.113883.5.84"
        obEntry.methodCode(0).codeSystemName = "ObservationMethod"



    End Sub
#End Region
#Region "QRDAI Public Method"
    Public Function GenerateQRDAI(ByVal PatientLastName As String, ByVal PatientFirstName As String) As String
        Dim strfilepath As String = ""
        Dim QRDAIDoc As New POCD_MT000040UV02ClinicalDocument()
        oCodingSystemMaster = New CodeSystemMaster
        oTemplateIDMaster = New TemplateIDMaster

        Try

            '    Set Clinic Information - End 
            'If _FinalCDAFilePath <> "" Then
            '    strfilepath = _FinalCDAFilePath
            'Else
            strfilepath = GenerateFileName(PatientLastName, PatientFirstName)
            '  End If

            'Header
            QRDAIDoc = GetCDAInitialization()

            'recordTarget
            QRDAIDoc.recordTarget = New POCD_MT000040UV02RecordTarget(0) {}
            QRDAIDoc.recordTarget(0) = GetCDARecordTarget()

            'author
            QRDAIDoc.author = New POCD_MT000040UV02Author(0) {}
            QRDAIDoc.author(0) = GetCDAAuthor()

            'custodian
            QRDAIDoc.custodian = New POCD_MT000040UV02Custodian
            QRDAIDoc.custodian = GetCDACustodian()

            QRDAIDoc.informationRecipient = New POCD_MT000040UV02Participant(0) {}
            QRDAIDoc.informationRecipient(0) = GetQRDAIIIinforecepient()

            'documentationOf
            QRDAIDoc.documentationOf = New POCD_MT000040UV02DocumentationOf(0) {}
            QRDAIDoc.documentationOf(0) = GetCDAdocumentationOf()

            'QRDAIII Body
            QRDAIDoc.component = New POCD_MT000040UV02Component2
            QRDAIDoc.component = GetCDAComponent()

            ''legalAuthenticator
            QRDAIDoc.legalAuthenticator = New POCD_MT000040UV02LegalAuthenticator(0) {}
            QRDAIDoc.legalAuthenticator(0) = GetCDALegalAuthenticator()


            Try
                gloSerialization.SetQRDADocument(strfilepath, QRDAIDoc)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
                ex = Nothing
                Return ""
            End Try
            If _msgString <> "" Then
                MessageBox.Show(_msgString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            If Not IsNothing(QRDAIDoc.recordTarget(0)) Then
                QRDAIDoc.recordTarget(0) = Nothing
            End If
            If Not IsNothing(QRDAIDoc.author(0)) Then
                QRDAIDoc.author(0) = Nothing
            End If
            If Not IsNothing(QRDAIDoc.custodian) Then
                QRDAIDoc.custodian = Nothing
            End If
            If Not IsNothing(QRDAIDoc.component) Then
                QRDAIDoc.component = Nothing
            End If
            If Not IsNothing(QRDAIDoc) Then
                QRDAIDoc = Nothing
            End If
            If Not IsNothing(oCodingSystemMaster) Then
                oCodingSystemMaster.Dispose()
                oCodingSystemMaster = Nothing
            End If
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
                oTemplateIDMaster = Nothing
            End If

        End Try

        Return strfilepath
    End Function
#End Region

#Region "QRDAI Private Methods"
    Private Function GetQRDAIMeasureComponent() As POCD_MT000040UV02Component3
        Dim _Measure As POCD_MT000040UV02Component3 = Nothing
        Dim section As POCD_MT000040UV02Section
        Dim table As StrucDocTable
        Dim extDoc As POCD_MT000040UV02ExternalDocument
        Dim org As POCD_MT000040UV02Organizer

        'Dim ob As POCD_MT000040UV02Observation = Nothing

        Dim _moduleList As List(Of POCD_MT000040UV02Component4) = Nothing
        Dim _component4 As POCD_MT000040UV02Component4()
        'Dim _PerformanceRate As POCD_MT000040UV02Component4 = Nothing
        'Dim _oEntry As POCD_MT000040UV02EntryRelationship = Nothing

        Try
            If Not IsNothing(_dtMeasures) AndAlso _dtMeasures.Rows.Count > 0 Then
                'Dim strMeasure(0) As String


                _Measure = New POCD_MT000040UV02Component3
                DirectCast(_Measure, POCD_MT000040UV02Component3).section = New POCD_MT000040UV02Section()
                section = DirectCast(_Measure, POCD_MT000040UV02Component3).section
                ''section.ID = Nothing
                section.classCode = Nothing
                section.classCodeSpecified = False
                section.moodCode = Nothing
                section.moodCodeSpecified = False
                section.templateId = New II(1) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.24.2.2"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = Nothing

                section.templateId(1) = New II()
                section.templateId(1).root = "2.16.840.1.113883.10.20.24.2.3"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(1).assigningAuthorityName = Nothing
                section.templateId(1).extension = Nothing

                section.code = New CE()
                ' section.code.nullFlavor = "NA"
                section.code.code = "55186-1"
                'oCodingSystemMaster.GetbyDescription(CodeSystem.LOINC, "Problem List").Code
                '"11450-4"

                section.code.codeSystem = CodeSystem.LOINC
                section.code.codeSystemName = Nothing
                section.code.codeSystemVersion = Nothing
                section.code.displayName = Nothing
                'nothing
                section.title = New ST()
                section.title.Text = New String() {"Measure Section"}
                section.title.representation = Nothing
                section.title.mediaType = Nothing

                ''
                section.title.language = Nothing
                section.text = New StrucDocText()
                section.text.mediaType = Nothing
                section.text.ID = Nothing
                section.text.language = Nothing
                section.text.Items = New Object(0) {}
                section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count) {}



                Dim tblcnt As Integer = 0
                section.text.Items(0) = New StrucDocTable()
                table = DirectCast(section.text.Items(0), StrucDocTable)
                table.ID = Nothing
                table.language = Nothing
                table.border = "1"
                table.width = "100%"
                table.thead = New StrucDocThead()
                table.thead.ID = Nothing
                table.thead.language = Nothing
                table.thead.tr = New StrucDocTr(0) {}

                table.thead.tr(0) = New StrucDocTr()
                table.thead.tr(0).ID = Nothing
                table.thead.tr(0).language = Nothing
                table.thead.tr(0).Items = New Object(5) {}
                table.thead.tr(0).Items(0) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(0), StrucDocTh).Text = New String() {"eMeasure Title"}
                DirectCast(table.thead.tr(0).Items(0), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(0), StrucDocTh).language = Nothing
                table.thead.tr(0).Items(1) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(1), StrucDocTh).Text = New String() {"Version neutral identifier"}
                DirectCast(table.thead.tr(0).Items(1), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(1), StrucDocTh).language = Nothing
                table.thead.tr(0).Items(2) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(2), StrucDocTh).Text = New String() {"eMeasure Version Number"}
                DirectCast(table.thead.tr(0).Items(2), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(2), StrucDocTh).language = Nothing
                table.thead.tr(0).Items(3) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(3), StrucDocTh).Text = New String() {"NQF eMeasure Number"}
                DirectCast(table.thead.tr(0).Items(3), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(3), StrucDocTh).language = Nothing
                table.thead.tr(0).Items(4) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(4), StrucDocTh).Text = New String() {"eMeasure Identifier (MAT)"}
                DirectCast(table.thead.tr(0).Items(4), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(4), StrucDocTh).language = Nothing
                table.thead.tr(0).Items(5) = New StrucDocTh()
                DirectCast(table.thead.tr(0).Items(5), StrucDocTh).Text = New String() {"Version specific identifier"}
                DirectCast(table.thead.tr(0).Items(5), StrucDocTh).ID = Nothing
                DirectCast(table.thead.tr(0).Items(5), StrucDocTh).language = Nothing

                table.tbody = New StrucDocTbody(0) {}
                table.tbody(0) = New StrucDocTbody()
                table.tbody(0).ID = Nothing
                table.tbody(0).language = Nothing
                table.tbody(0).tr = New StrucDocTr(_dtMeasures.Rows.Count - 1) {}
                section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count - 1) {}

                For measures As Integer = 0 To _dtMeasures.Rows.Count - 1


                    'table.tbody = New StrucDocTbody(0) {}
                    'table.tbody(0) = New StrucDocTbody()
                    'table.tbody(0).ID = Nothing
                    'table.tbody(0).language = Nothing
                    'table.tbody(0).tr = New StrucDocTr(0) {}
                    '   section.entry = New POCD_MT000040UV02Entry(_dtMeasures.Rows.Count - 1) {}


                    table.tbody(0).tr(measures) = New StrucDocTr()
                    table.tbody(0).tr(measures).Items = New Object(5) {}
                    table.tbody(0).tr(measures).Items(0) = New StrucDocTd()
                    table.tbody(0).tr(measures).ID = Nothing
                    table.tbody(0).tr(measures).language = Nothing

                    DirectCast(table.tbody(0).tr(measures).Items(0), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_Title)}
                    DirectCast(table.tbody(0).tr(measures).Items(0), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(0), StrucDocTd).language = Nothing
                    table.tbody(0).tr(measures).Items(1) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(measures).Items(1), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_NeutralID)}
                    DirectCast(table.tbody(0).tr(measures).Items(1), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(1), StrucDocTd).language = Nothing
                    table.tbody(0).tr(measures).Items(2) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(measures).Items(2), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_VersionNo)}
                    DirectCast(table.tbody(0).tr(measures).Items(2), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(2), StrucDocTd).language = Nothing
                    table.tbody(0).tr(measures).Items(3) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(measures).Items(3), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_No)}
                    DirectCast(table.tbody(0).tr(measures).Items(3), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(3), StrucDocTd).language = Nothing
                    table.tbody(0).tr(measures).Items(4) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(measures).Items(4), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_CMSID)}
                    DirectCast(table.tbody(0).tr(measures).Items(4), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(4), StrucDocTd).language = Nothing
                    table.tbody(0).tr(measures).Items(5) = New StrucDocTd()
                    DirectCast(table.tbody(0).tr(measures).Items(5), StrucDocTd).Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)}
                    DirectCast(table.tbody(0).tr(measures).Items(5), StrucDocTd).ID = Nothing
                    DirectCast(table.tbody(0).tr(measures).Items(5), StrucDocTd).language = Nothing

                    ''

                    ''
                    section.entry(measures) = New POCD_MT000040UV02Entry()
                    _moduleList = New List(Of POCD_MT000040UV02Component4)

                    section.entry(measures).typeCode = Nothing
                    section.entry(measures).typeCodeSpecified = False
                    section.entry(measures).Item = New POCD_MT000040UV02Organizer()
                    org = DirectCast(section.entry(measures).Item, POCD_MT000040UV02Organizer)
                    org.classCode = x_ActClassDocumentEntryOrganizer.CLUSTER
                    org.moodCode = "EVN"
                    org.moodCodeSpecified = True

                    org.templateId = New II(1) {}
                    org.templateId(0) = New II()
                    org.templateId(0).root = "2.16.840.1.113883.10.20.24.3.98"
                    'oTemplateIDMaster.GetEntryID("Problem Concern Act (Condition)")
                    '"2.16.840.1.113883.10.20.22.4.3"
                    org.templateId(0).extension = Nothing
                    org.templateId(0).assigningAuthorityName = Nothing
                    org.templateId(1) = New II()
                    org.templateId(1).root = "2.16.840.1.113883.10.20.24.3.97"
                    'oTemplateIDMaster.GetEntryID("Problem Concern Act (Condition)")
                    '"2.16.840.1.113883.10.20.22.4.3"
                    org.templateId(1).extension = Nothing
                    org.templateId(1).assigningAuthorityName = Nothing

                    ''
                    ' If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then


                    org.id = New II(0) {}
                    org.id(0) = New II
                    If _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)) <> "" Then

                        org.id(0).extension = _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) ''_dtMeasures.Rows(measures)(Col_Measure_NeutralID)  ''eMeasure version neutral id GUID 
                    Else

                        org.id(0).extension = Nothing
                    End If

                    org.id(0).root = "1.3.6.1.4.1.115"
                    org.id(0).assigningAuthorityName = Nothing
                    '   End If
                    ''
                    org.statusCode = New CS()
                    org.statusCode.code = "completed"
                    org.statusCode.codeSystem = Nothing
                    org.statusCode.codeSystemName = Nothing
                    org.statusCode.codeSystemVersion = Nothing
                    org.statusCode.displayName = Nothing


                    org.reference = New POCD_MT000040UV02Reference(0) {}
                    org.reference(0) = New POCD_MT000040UV02Reference
                    org.reference(0).typeCode = x_ActRelationshipExternalReference.REFR
                    org.reference(0).realmCode = Nothing
                    org.reference(0).Item = New POCD_MT000040UV02ExternalDocument
                    extDoc = DirectCast(org.reference(0).Item, POCD_MT000040UV02ExternalDocument)
                    extDoc.classCode = ActClassDocument.DOC
                    '' extDoc.classCodeSpecified = True
                    extDoc.moodCode = "EVN"

                    extDoc.moodCodeSpecified = True
                    extDoc.id = New II(0) {}
                    extDoc.id(0) = New II
                    '  If gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting Then
                    If _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)) <> "" Then
                        extDoc.id(0).root = "2.16.840.1.113883.4.738"
                        extDoc.id(0).extension = _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) ''_dtMeasures.Rows(measures)(Col_Measure_NeutralID)  ''eMeasure version neutral id GUID 
                    Else
                        extDoc.id(0).root = Nothing
                        extDoc.id(0).extension = Nothing
                    End If

                    'Else
                    'If _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)) <> "" Then
                    '    extDoc.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) ''version specific identifier
                    '    extDoc.id(0).extension = Nothing
                    'Else
                    '    extDoc.id(0).root = Nothing
                    '    extDoc.id(0).extension = Nothing
                    'End If

                    'End If
                    'If _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionSpecID)) <> "" Then
                    '    extDoc.id(0).root = _dtMeasures.Rows(measures)(Col_Measure_VersionSpecID) ''version specific identifier
                    'Else
                    '    extDoc.id(0).root = Nothing
                    'End If


                    '  extDoc.id(0).extension = Nothing
                    extDoc.id(0).assigningAuthorityName = Nothing

                    extDoc.text = New ED()
                    If _dtMeasures.Rows(measures)(Col_Measure_Title) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_Title)) <> "" Then
                        extDoc.text.Text = New String() {_dtMeasures.Rows(measures)(Col_Measure_Title)}  ''eMeasure Title
                    Else
                        extDoc.text.Text = Nothing
                    End If

                    extDoc.text.language = Nothing
                    extDoc.text.reference = Nothing
                    extDoc.text.mediaType = Nothing

                    extDoc.setId = New II()
                    If _dtMeasures.Rows(measures)(Col_Measure_NeutralID) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_NeutralID)) <> "" Then
                        extDoc.setId.root = _dtMeasures.Rows(measures)(Col_Measure_NeutralID) ''eMeasure version neutral id GUID 
                    Else
                        extDoc.setId.root = Nothing
                    End If
                    '


                    ''
                    extDoc.setId.extension = Nothing
                    extDoc.setId.assigningAuthorityName = Nothing
                    extDoc.versionNumber = New INT()
                    If _dtMeasures.Rows(measures)(Col_Measure_VersionNo) IsNot Nothing AndAlso Convert.ToString(_dtMeasures.Rows(measures)(Col_Measure_VersionNo)) <> "" Then
                        ''TestMayuri
                        extDoc.versionNumber.value = _dtMeasures.Rows(measures)(Col_Measure_VersionNo) ''eMeasure Version number
                    Else
                        extDoc.versionNumber.value = Nothing
                    End If

                    extDoc.versionNumber.nullFlavor = Nothing
                    tblcnt = tblcnt + 1
                    _component4 = New POCD_MT000040UV02Component4(_moduleList.Count) {}
                    _moduleList.CopyTo(_component4)
                    org.component = _component4
                Next







                ''

            End If
            Return _Measure
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        End Try

    End Function
    Private Function GetQRDAIPatientDataComponent() As POCD_MT000040UV02Component3

        Dim _PatientComponent As POCD_MT000040UV02Component3 = Nothing
        Dim section As POCD_MT000040UV02Section
        Dim _moduleList As List(Of POCD_MT000040UV02Entry) = Nothing
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim _EntrySection As POCD_MT000040UV02Entry()
        ' Dim Encounter As POCD_MT000040UV02Encounter
        ' Dim SubstanceAdmin As POCD_MT000040UV02SubstanceAdministration
        ' Dim ManMaterial As POCD_MT000040UV02Material

        ' Dim obs As POCD_MT000040UV02Observation
        ' Dim obsentry As POCD_MT000040UV02Observation

        Dim _TransactionID As Int64 = 0
        Dim _dvData As New DataView
        Dim dtDistinct As New DataTable
        Try
            If Not IsNothing(_dtQRDA1Data) AndAlso _dtQRDA1Data.Rows.Count > 0 Then

                dtDistinct = dtQRDA1Data.Copy().DefaultView.ToTable(True, "nPatientID", "TransactionID", "Category", "TransactionDate", "SDTCValueSet", "CodeDescription", "CodeValue", "sICD9Code", "sConceptID", "RXCUI", "LOINC", "CVX", "BPSystolic", "BPDiastolic", "sICD10Code", "sReasonCode", "sReasonValueSet", "sReasonConceptID", "sReasonICD9", "sReasonICD10", "sReasonLOINC", "BPBMI", "sTranID2", "sFrequency", "sAmount", "dtDischargeDate", "sunit")

                _PatientComponent = New POCD_MT000040UV02Component3
                DirectCast(_PatientComponent, POCD_MT000040UV02Component3).section = New POCD_MT000040UV02Section()
                section = DirectCast(_PatientComponent, POCD_MT000040UV02Component3).section
                'section.ID = Nothing
                section.classCode = Nothing
                section.classCodeSpecified = False
                section.moodCode = Nothing
                section.moodCodeSpecified = False
                section.templateId = New II(2) {}
                section.templateId(0) = New II()
                section.templateId(0).root = "2.16.840.1.113883.10.20.17.2.4"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(0).assigningAuthorityName = Nothing
                section.templateId(0).extension = Nothing

                section.templateId(1) = New II()
                section.templateId(1).root = "2.16.840.1.113883.10.20.24.2.1"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(1).assigningAuthorityName = Nothing
                section.templateId(1).extension = "2015-07-01"



                section.templateId(2) = New II()
                section.templateId(2).root = "2.16.840.1.113883.10.20.24.2.1"
                'oTemplateIDMaster.GetSectionID("Problem")
                '"2.16.840.1.113883.10.20.22.2.5.1"
                section.templateId(2).assigningAuthorityName = Nothing
                section.templateId(2).extension = _DateExtension24


                section.code = New CE()
                ' section.code.nullFlavor = "NA"
                section.code.code = "55188-7"
                'oCodingSystemMaster.GetbyDescription(CodeSystem.LOINC, "Problem List").Code
                '"11450-4"

                section.code.codeSystem = CodeSystem.LOINC
                section.code.codeSystemName = Nothing
                section.code.codeSystemVersion = Nothing
                section.code.displayName = Nothing

                section.title = New ST()
                section.title.Text = New String() {"Patient Data"}
                section.title.language = Nothing
                section.title.mediaType = Nothing
                section.title.representation = Nothing
                section.text = New StrucDocText
                section.text.mediaType = Nothing
                section.text.ID = Nothing
                section.text.language = Nothing
                ' section.text = Nothing
                _moduleList = New List(Of POCD_MT000040UV02Entry)
                ' section.entry = New POCD_MT000040UV02Entry(_dtQRDA1Data.Rows.Count - 1) {}

                Dim TransactionID As String = ""
                If Not IsNothing(dtDistinct) AndAlso dtDistinct.Rows.Count > 0 Then

                    Dim _valueset As String = ""
                    Dim _codedescription As String = ""
                    Dim _TransactionDate As String = ""

                    For patientComponent As Integer = 0 To dtDistinct.Rows.Count - 1




                        'Dim _loginID As Int64 = 1
                        'dvQRDA1Data = dtQRDA1Data.Copy().DefaultView
                        'dvQRDA1Data.RowFilter = "TransactionID ='" & dtDistinct.Rows(patientComponent)("TransactionID") & "'"
                        Select Case Convert.ToString(dtDistinct.Rows(patientComponent)("Category"))

                            Case "Payer"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetPayerEntry(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Encounter Performed"
                                If Convert.ToString(dtDistinct.Rows(patientComponent)("TransactionID")) <> TransactionID Then
                                    TransactionID = Convert.ToString(dtDistinct.Rows(patientComponent)("TransactionID"))
                                    _entry = New POCD_MT000040UV02Entry
                                    _entry = GetEncounter(dtDistinct, patientComponent)
                                    If Not IsNothing(_moduleList) Then
                                        _moduleList.Add(_entry)
                                    End If
                                End If


                            Case "Medication Order", "Medication Active"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetSubstanceAdmin(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Medication Administered" '', "Medication Administered not done"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetMedicationAdministered(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                                'Case "Medication" ''"Medication Allergy", "Medication Intolerance"
                                '    If Convert.ToString(dtDistinct.Rows(patientComponent)("sReasonConceptID")) = "UNK" Then
                                '        _entry = New POCD_MT000040UV02Entry
                                '        _entry = GetMedicationAllergy(dtDistinct, patientComponent, True)
                                '        If Not IsNothing(_moduleList) Then
                                '            _moduleList.Add(_entry)
                                '        End If
                                '    ElseIf Convert.ToString(dtDistinct.Rows(patientComponent)("sTranID2") = "0") Then
                                '        _entry = New POCD_MT000040UV02Entry
                                '        _entry = GetMedicationAdministered(dtDistinct, patientComponent)
                                '        If Not IsNothing(_moduleList) Then
                                '            _moduleList.Add(_entry)
                                '        End If

                                '    Else
                                '        '_entry = New POCD_MT000040UV02Entry
                                '        '_entry = GetMedicationAllergy(dtDistinct, patientComponent, False)
                                '        'If Not IsNothing(_moduleList) Then
                                '        '    _moduleList.Add(_entry)
                                '        'End If

                                '    End If
                            Case "Medication Allergy"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetMedicationAllergy(dtDistinct, patientComponent, True)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Medication Intolerance"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetMedicationAllergy(dtDistinct, patientComponent, False)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If

                            Case "Diagnosis Active", "Diagnosis Inactive", "Diagnosis Resolved"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetProblemAct(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Tobacco Use"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetTobaccoObservation(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Diagnostic Study Performed", "Physical Exam Performed", "Functional Status Performed"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetDiagnosticStudyPerformed(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                                'Case "Laboratory Test Result", "Diagnostic Study Result","Laboratory Test Performed"
                            Case "Laboratory Test Performed"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetResultObservation(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Laboratory Test Order", "Diagnostic Study Order"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetOrderObservation(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Intervention Performed", "Intervention Order"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetInterventionPerformed(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Procedure Intolerance"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetProcedureIntolerance(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Procedure" '', "Procedure Performed not done"
                                If Convert.ToString(dtDistinct.Rows(patientComponent)("sReasonConceptID")) = "UNK" Then
                                    _entry = New POCD_MT000040UV02Entry
                                    _entry = GetProcedureIntolerance(dtDistinct, patientComponent)
                                    If Not IsNothing(_moduleList) Then
                                        _moduleList.Add(_entry)
                                    End If
                                Else
                                    _entry = New POCD_MT000040UV02Entry
                                    _entry = GetProcedurePerformed(dtDistinct, patientComponent)
                                    If Not IsNothing(_moduleList) Then
                                        _moduleList.Add(_entry)
                                    End If
                                End If
                            Case "Procedure Performed", "Procedure Order"
                                
                                If _valueset <> "" Then
                                    If _valueset = "2.16.840.1.113883.3.464.1003.198.12.1020" Then
                                        If _TransactionDate = Convert.ToString(dtDistinct.Rows(patientComponent)("TransactionDate")) AndAlso _valueset = Convert.ToString(dtDistinct.Rows(patientComponent)("SDTCValueSet")) AndAlso _codedescription = Convert.ToString(dtDistinct.Rows(patientComponent)("CodeDescription")) Then
                                            Continue For
                                        End If
                                    End If
                                    
                                End If

                                _valueset = Convert.ToString(dtDistinct.Rows(patientComponent)("SDTCValueSet"))
                                _codedescription = Convert.ToString(dtDistinct.Rows(patientComponent)("CodeDescription"))
                                _TransactionDate = Convert.ToString(dtDistinct.Rows(patientComponent)("TransactionDate"))
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetProcedurePerformed(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If

                            Case "Medical or Other reason not done"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetMedicalNotReason(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Risk Category Assessment not done", "Risk Category Assessment"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetRiskCatAssessment(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                            Case "Assessment Performed"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetAssessmentPeroformed(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If

                            Case "Communication from Patient to Provider", "Communication from Provider to Patient", "Communication from Provider to Provider"
                                _entry = New POCD_MT000040UV02Entry
                                _entry = GetCommunicationact(dtDistinct, patientComponent)
                                If Not IsNothing(_moduleList) Then
                                    _moduleList.Add(_entry)
                                End If
                        End Select



                    Next
                End If
                _EntrySection = New POCD_MT000040UV02Entry(_moduleList.Count) {}
                _moduleList.CopyTo(_EntrySection)
                section.entry = _EntrySection
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        Return _PatientComponent
    End Function
    Private Function GetProcedurePerformedNotObservation(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Observation
        '  Dim _entryrelation As POCD_MT000040UV02EntryRelationship = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        '  Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Try

        
            '_entryrelation = New POCD_MT000040UV02EntryRelationship
            '_entryrelation.typeCode = "RSON"
            'DirectCast(_entryrelation, POCD_MT000040UV02EntryRelationship).Item = New POCD_MT000040UV02Observation()
            'obs = DirectCast(_entryrelation, POCD_MT000040UV02EntryRelationship).Item
            obs = New POCD_MT000040UV02Observation
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(0) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"

            obs.templateId(0).extension = _DateExtension24
            obs.templateId(0).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            obs.code.code = "77301-0"
            obs.code.displayName = "reason"
            obs.code.codeSystem = CodeSystem.LOINC
            obs.code.codeSystemName = "LOINC"
            obs.code.codeSystemVersion = Nothing



            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            ''
            'obs.value = New ANY(1) {}
            'obs.value(0) = New CD()

            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
            '    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
            '    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
            '        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        DirectCast(obs.value(0), CD).valueSet = Nothing
            '    End If
            '    DirectCast(obs.value(0), CD).valueSetVersion = Nothing
            'Else
            '    DirectCast(obs.value(0), CD).code = Nothing
            'End If
            'DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            'DirectCast(obs.value(0), CD).codeSystemName = Nothing
            'DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            'DirectCast(obs.value(0), CD).displayName = Nothing

            obs.value = New ANY(0) {}
            obs.value(0) = New CD()

            If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
                DirectCast(obs.value(0), CD).valueSetVersion = Nothing
            Else
                DirectCast(obs.value(0), CD).code = Nothing
                If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
            End If
            DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(obs.value(0), CD).codeSystemName = Nothing
            DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).displayName = Nothing




        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            'If Not IsNothing(obs) Then
            '    obs = Nothing
            'End If
            'If Not IsNothing(obs) Then
            '    obs = Nothing
            'End If
        End Try
        Return obs

    End Function
    Private Function GetMedicalNotReason(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer)
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim Proc As POCD_MT000040UV02Procedure = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        'Dim playEntity As POCD_MT000040UV02PlayingEntity = Nothing
        Dim dv As New DataView
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Procedure()
            Proc = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            Proc.classCode = ActClassProcedure.PROC
            Proc.moodCode = x_DocumentProcedureMood.EVN
            Proc.moodCodespecified = True

            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()

                Dim _ValusetOID As String = String.Empty
                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    Proc.negationIndSpecified = True
                    Proc.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If

            Proc.templateId = New II(1) {}
            Proc.templateId(0) = New II()
            Proc.templateId(0).root = "2.16.840.1.113883.10.20.22.4.14"

            Proc.templateId(0).extension = _DateExtension22
            Proc.templateId(0).assigningAuthorityName = Nothing

            Proc.templateId(1) = New II()

            Proc.templateId(1).root = "2.16.840.1.113883.10.20.24.3.64"
            If ReportingYear = "2019" Then
                Proc.templateId(1).extension = _Version2019
            Else
                Proc.templateId(1).extension = _DateExtension2016
            End If



            Proc.templateId(1).assigningAuthorityName = Nothing

            Proc.id = New II(0) {}
            Proc.id(0) = New II()
            Proc.id(0).root = Guid.NewGuid.ToString()
            Proc.id(0).extension = Nothing
            Proc.id(0).assigningAuthorityName = Nothing

            Proc.code = New CD()

            Dim _code As Integer = 0
            dv = dtQRDA1Data.Copy().DefaultView

            dv.RowFilter = "TransactionID = '" & _dtDistinct.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtDistinct.Rows(patientComponent)("Category") & "' "
            Proc.code = New CD()
            DirectCast(Proc.code, CD).translation = New CD(dv.Count) {}


            If Not IsNothing(dv) AndAlso dv.Count > 0 Then
                'For i As Integer = 0 To dv.Count - 1

                If dv.ToTable.Rows(0)("CodeType") = "SnoMed" Then
                    Proc.code = New CD()
                    If dv.ToTable.Rows(0)("CodeValue") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("CodeValue")) <> "" Then

                        Proc.code.code = dv.ToTable.Rows(0)("CodeValue")
                        ''
                        If dv.ToTable.Rows(0)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("SDTCValueSet")) <> "" Then
                            Proc.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                        Else
                            Proc.code.valueSet = Nothing
                        End If
                        Proc.code.valueSetVersion = Nothing
                    Else
                        Proc.code.code = Nothing
                    End If
                    Proc.code.codeSystem = CodeSystem.SNOMED_CT
                    Proc.code.codeSystemName = "SNOMED_CT"
                    Proc.code.codeSystemVersion = Nothing
                    Proc.code.displayName = Nothing



                End If
            End If



            Proc.code.originalText = New ED()
            Proc.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Proc.code.originalText.language = Nothing

            Proc.statusCode = New CS()
            Proc.statusCode.code = "completed"
            Proc.statusCode.codeSystem = Nothing
            Proc.statusCode.codeSystemName = Nothing
            Proc.statusCode.codeSystemVersion = Nothing
            Proc.statusCode.displayName = Nothing


            Proc.effectiveTime = New IVL_TS()

            Proc.effectiveTime.value = Nothing
            DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(Proc.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(Proc.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(Proc.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If




            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                Proc.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                Proc.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                Proc.entryRelationship(0).typeCode = ActRelationshipType.RSON
                Proc.entryRelationship(0).Item = New POCD_MT000040UV02Procedure
                Proc.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
            End If


        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(Proc) Then
                Proc = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry
    End Function
    Private Function GetEncounter(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _Entry As POCD_MT000040UV02Entry = Nothing
        Dim EncounterAct As POCD_MT000040UV02Act = Nothing
        Dim Encounter As POCD_MT000040UV02Encounter = Nothing
        Dim dv As New DataView
        Try


            _Entry = New POCD_MT000040UV02Entry
            _Entry.typeCode = Nothing
            _Entry.typeCodeSpecified = False
            DirectCast(_Entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Act()
            EncounterAct = DirectCast(_Entry, POCD_MT000040UV02Entry).Item
            EncounterAct.classCode = x_ActClassDocumentEntryAct.ACT
            EncounterAct.moodCode = x_DocumentActMood.EVN
            EncounterAct.moodCodeSpecified = True
            EncounterAct.templateId = New II(0) {}
            EncounterAct.templateId(0) = New II()
            EncounterAct.templateId(0).root = "2.16.840.1.113883.10.20.24.3.133"
            If ReportingYear = "2019" Then
                EncounterAct.templateId(0).extension = _Version2019
            Else
                EncounterAct.templateId(0).extension = Nothing
            End If


            EncounterAct.templateId(0).assigningAuthorityName = Nothing
            EncounterAct.id = New II(0) {}
            EncounterAct.id(0) = New II()
            EncounterAct.id(0).root = Guid.NewGuid.ToString()
            EncounterAct.id(0).extension = Nothing
            EncounterAct.id(0).assigningAuthorityName = Nothing
            EncounterAct.code = New CD()
            EncounterAct.code.code = "ENC"
            EncounterAct.code.codeSystem = "2.16.840.1.113883.5.6"
            EncounterAct.code.displayName = "Encounter"
            EncounterAct.code.codeSystemName = "ActClass"
            EncounterAct.code.codeSystemVersion = Nothing
            EncounterAct.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            EncounterAct.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship()
            EncounterAct.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
            EncounterAct.entryRelationship(0).Item = New POCD_MT000040UV02Encounter
            Encounter = DirectCast(EncounterAct.entryRelationship(0).Item, POCD_MT000040UV02Encounter)
            Encounter.classCode = ActClass.ENC
            Encounter.moodCode = x_DocumentEncounterMood.EVN
            Encounter.moodCodeSpecified = True

            Encounter.templateId = New II(1) {}
            Encounter.templateId(0) = New II()
            Encounter.templateId(0).root = "2.16.840.1.113883.10.20.22.4.49"

            Encounter.templateId(0).extension = _DateExtension2015
            Encounter.templateId(0).assigningAuthorityName = Nothing
            Encounter.templateId(1) = New II()
            Encounter.templateId(1).root = "2.16.840.1.113883.10.20.24.3.23"
            If ReportingYear = "2019" Then
                Encounter.templateId(1).extension = _Version2019
            Else
                Encounter.templateId(1).extension = _DateExtension2016
            End If


            Encounter.templateId(1).assigningAuthorityName = Nothing

            Encounter.id = New II(0) {}
            Encounter.id(0) = New II()
            Encounter.id(0).root = "1.3.6.1.4.1.115"
            Encounter.id(0).extension = Guid.NewGuid.ToString()
            Encounter.id(0).assigningAuthorityName = Nothing

            Encounter.code = New CD()
            Dim _code As Integer = 0
            dv = dtQRDA1Data.Copy().DefaultView
            dv.RowFilter = "TransactionID = '" & _dtDistinct.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtDistinct.Rows(patientComponent)("Category") & "' "
            Encounter.code = New CD()
            DirectCast(Encounter.code, CD).translation = New CD(dv.Count) {}


            If Not IsNothing(dv) AndAlso dv.Count > 0 Then
                For i As Integer = 0 To dv.Count - 1

                    '  If dv.ToTable.Rows(i)("CodeType") = "CPT" Or dv.ToTable.Rows(i)("CodeType") = "CPT/Snomed" Or dv.ToTable.Rows(i)("CodeType") = "SnoMed" Then
                    If _code = 0 Then
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then
                            dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)
                            Encounter.code.code = dv.ToTable.Rows(i)("sCPTCode")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Encounter.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Encounter.code.valueSet = Nothing
                            End If
                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                Encounter.code.codeSystem = CodeSystem.CPT
                            Else
                                Encounter.code.codeSystem = CodeSystem.HCPCS

                            End If


                            Encounter.code.codeSystemName = Nothing
                            Encounter.code.codeSystemVersion = Nothing
                            Encounter.code.displayName = Nothing
                            _code = _code + 1
                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                                DirectCast(Encounter.code, CD).translation(i) = New CD()
                                DirectCast(Encounter.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(Encounter.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(Encounter.code, CD).translation(i).valueSet = Nothing
                                '  End If
                                DirectCast(Encounter.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(Encounter.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Encounter.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Encounter.code, CD).translation(i).displayName = Nothing
                                ''_code = _code + 1
                            End If

                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                            Encounter.code.code = dv.ToTable.Rows(i)("sConceptID")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Encounter.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Encounter.code.valueSet = Nothing
                            End If

                            Encounter.code.codeSystem = CodeSystem.SNOMED_CT

                            Encounter.code.codeSystemName = Nothing
                            Encounter.code.codeSystemVersion = Nothing
                            Encounter.code.displayName = Nothing
                            _code = _code + 1
                        ElseIf dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then

                            Encounter.code.code = dv.ToTable.Rows(i)("sICD10Code")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Encounter.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Encounter.code.valueSet = Nothing
                            End If

                            Encounter.code.codeSystem = CodeSystem.ICD10

                            Encounter.code.codeSystemName = Nothing
                            Encounter.code.codeSystemVersion = Nothing
                            Encounter.code.displayName = Nothing
                            _code = _code + 1

                        Else
                            Encounter.code.code = Nothing
                            Encounter.code.codeSystem = CodeSystem.CPT
                            Encounter.code.codeSystemName = Nothing
                            Encounter.code.codeSystemVersion = Nothing
                            Encounter.code.displayName = Nothing
                        End If
                    Else
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then
                            DirectCast(Encounter.code, CD).translation(i) = New CD()
                            DirectCast(Encounter.code, CD).translation(i).code = dv.ToTable.Rows(i)("sCPTCode")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                DirectCast(Encounter.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                DirectCast(Encounter.code, CD).translation(i).valueSet = Nothing
                            End If
                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                DirectCast(Encounter.code, CD).translation(i).codeSystem = CodeSystem.CPT
                            Else
                                DirectCast(Encounter.code, CD).translation(i).codeSystem = CodeSystem.HCPCS

                            End If



                            DirectCast(Encounter.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Encounter.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Encounter.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                                DirectCast(Encounter.code, CD).translation(i) = New CD()
                                DirectCast(Encounter.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(Encounter.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(Encounter.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(Encounter.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(Encounter.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Encounter.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Encounter.code, CD).translation(i).displayName = Nothing
                            End If
                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                            DirectCast(Encounter.code, CD).translation(i) = New CD()
                            DirectCast(Encounter.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(Encounter.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else


                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                DirectCast(Encounter.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                DirectCast(Encounter.code, CD).translation(i).valueSet = Nothing
                            End If
                            '  End If
                            DirectCast(Encounter.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                            DirectCast(Encounter.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Encounter.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Encounter.code, CD).translation(i).displayName = Nothing

                    End If
                    End If

                    ' End If

                Next


            End If
            ''
            Encounter.code.originalText = New ED()

            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                Encounter.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                Encounter.code.originalText.Text = Nothing
            End If
            Encounter.code.originalText.language = Nothing
            Encounter.text = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                Encounter.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                Encounter.text.Text = Nothing
            End If
            Encounter.text.language = Nothing
            Encounter.statusCode = New CS()
            Encounter.statusCode.code = "completed"
            Encounter.statusCode.codeSystem = Nothing
            Encounter.statusCode.codeSystemName = Nothing
            Encounter.statusCode.codeSystemVersion = Nothing
            Encounter.statusCode.displayName = Nothing


            Encounter.effectiveTime = New IVL_TS()
            Encounter.effectiveTime.value = Nothing
            Encounter.effectiveTime.ItemsElementName = New ItemsChoiceType2(1) {}
            Encounter.effectiveTime.ItemsElementName(0) = ItemsChoiceType2.low
            Encounter.effectiveTime.Items = New QTY(1) {}
            Encounter.effectiveTime.Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(Encounter.effectiveTime.Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(Encounter.effectiveTime.Items(0), IVXB_TS).nullFlavor = "UNK"
                DirectCast(Encounter.effectiveTime.Items(0), IVXB_TS).value = Nothing
            End If

            Encounter.effectiveTime.ItemsElementName(1) = ItemsChoiceType2.high
            ' Encounter.effectiveTime.Items = New QTY(1) {}
            Encounter.effectiveTime.Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)("dtDischargeDate")) <> "" Then
                DirectCast(Encounter.effectiveTime.Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)("dtDischargeDate"), "yyyyMMddHHmmss")
                'check if discharge code is present then discharge time must be present in CDA else QRDA1 fails.
            ElseIf _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(Encounter.effectiveTime.Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(Encounter.effectiveTime.Items(1), IVXB_TS).nullFlavor = "UNK"
                DirectCast(Encounter.effectiveTime.Items(1), IVXB_TS).value = Nothing
            End If
            ''
            If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Encounter.dischargeDispositionCode = New CE()

                If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    Encounter.dischargeDispositionCode.code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                    Encounter.dischargeDispositionCode.valueSet = Nothing
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    '    DirectCast(obs.value(0), CD).valueSet = Nothing
                    'End If
                    Encounter.dischargeDispositionCode.valueSetVersion = Nothing
                    Encounter.dischargeDispositionCode.codeSystem = CodeSystem.SNOMED_CT
                Else
                    Encounter.dischargeDispositionCode.code = Nothing
                    Encounter.dischargeDispositionCode.nullFlavor = "NA"
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    Encounter.dischargeDispositionCode.valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    Encounter.dischargeDispositionCode.valueSet = Nothing
                    Encounter.dischargeDispositionCode.codeSystem = Nothing
                    'End If
                End If

                Encounter.dischargeDispositionCode.codeSystemName = Nothing
                Encounter.dischargeDispositionCode.codeSystemVersion = Nothing
                Encounter.dischargeDispositionCode.displayName = Nothing

            End If
            ''
            'For 2019 Reason is removed from encounter performed act
            If ReportingYear <> "2019" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                        Encounter.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                        Encounter.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                        Encounter.entryRelationship(0).typeCode = ActRelationshipType.RSON
                        Encounter.entryRelationship(0).Item = New POCD_MT000040UV02Procedure


                        Encounter.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
                    End If
                End If
            End If

  
            
        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(Encounter) Then
                Encounter = Nothing
            End If

        End Try
        Return _Entry

    End Function
    Private Function GetPayerEntry(ByVal _dtInsurance As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _Entry As POCD_MT000040UV02Entry = Nothing
        Dim Observation As POCD_MT000040UV02Observation = Nothing
        Dim dv As New DataView
        Try


            _Entry = New POCD_MT000040UV02Entry
            DirectCast(_Entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            Observation = DirectCast(_Entry, POCD_MT000040UV02Entry).Item


            Observation.classCode = ActClassObservation.OBS
            Observation.classCodeSpecified = True
            Observation.moodCode = x_ActMoodDocumentObservation.EVN
            Observation.moodCodespecified = True
            Observation.templateId = New II(0) {}
            Observation.templateId(0) = New II()
            Observation.templateId(0).root = "2.16.840.1.113883.10.20.24.3.55"
            Observation.templateId(0).extension = Nothing
            Observation.templateId(0).assigningAuthorityName = Nothing

            Observation.id = New II(0) {}
            Observation.id(0) = New II()
            Observation.id(0).root = Guid.NewGuid.ToString()
            Observation.id(0).extension = Nothing
            Observation.id(0).assigningAuthorityName = Nothing


            Observation.code = New CD()
            Observation.code.code = "48768-6"
            Observation.code.codeSystem = CodeSystem.LOINC
            Observation.code.codeSystemName = "LOINC"
            Observation.code.codeSystemVersion = Nothing
            Observation.code.displayName = "Payment source"

            Observation.statusCode = New CS()
            Observation.statusCode.code = "completed"
            Observation.statusCode.codeSystem = Nothing
            Observation.statusCode.codeSystemName = Nothing
            Observation.statusCode.codeSystemVersion = Nothing
            Observation.statusCode.displayName = Nothing

            Observation.effectiveTime = New IVL_TS()
            ''As per QualityNet after first validation on CMS
            Observation.effectiveTime.operator = Nothing
            Observation.effectiveTime.value = Nothing
            DirectCast(Observation.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(0) {}
            DirectCast(Observation.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low

            DirectCast(Observation.effectiveTime, IVL_TS).Items = New QTY(0) {}
            DirectCast(Observation.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()

            DirectCast(DirectCast(Observation.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
            DirectCast(DirectCast(Observation.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            ''End EffectiveTime

            dv = dtQRDA1Data.Copy().DefaultView
            dv.RowFilter = "TransactionID = '" & _dtInsurance.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtInsurance.Rows(patientComponent)("Category") & "' "

            If Not IsNothing(dv) AndAlso dv.Count > 0 Then
                Observation.value = New CD(0) {}
                Observation.value(0) = New CD

                If dv.ToTable.Rows(0)("CodeValue") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("CodeValue")) <> "" Then
                    If dv.ToTable.Rows(0)("CodeValue") = "MA" Or dv.ToTable.Rows(0)("CodeValue") = "MB" Then
                        DirectCast(Observation.value(0), CD).displayName = "Medicare"
                        DirectCast(Observation.value(0), CD).code = "1"

                    ElseIf dv.ToTable.Rows(0)("CodeValue") = "MC" Then
                        DirectCast(Observation.value(0), CD).displayName = "Medicaid"
                        DirectCast(Observation.value(0), CD).code = "2"

                    ElseIf dv.ToTable.Rows(0)("CodeValue") = "OT" Then
                        DirectCast(Observation.value(0), CD).displayName = "Other"
                        DirectCast(Observation.value(0), CD).code = "349"


                    Else
                        DirectCast(Observation.value(0), CD).displayName = "Other"
                        DirectCast(Observation.value(0), CD).code = "349"

                    End If
                Else
                    DirectCast(Observation.value(0), CD).displayName = "Other"
                    DirectCast(Observation.value(0), CD).code = "349"


                End If
                ''temp
                DirectCast(Observation.value(0), CD).valueSet = Convert.ToString(dv.ToTable.Rows(0)("SDTCValueSet"))
                DirectCast(Observation.value(0), CD).codeSystem = CodeSystem.PaymentTopology
                DirectCast(Observation.value(0), CD).codeSystemName = "Source of Payment Typology"
                DirectCast(Observation.value(0), CD).codeSystemVersion = Nothing

            End If

            'Observation.value = New CD(0) {}
            'Observation.value(0) = New CD

            'If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) IsNot Nothing AndAlso Convert.ToString(dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description)) <> "" Then
            '    If dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MA" Or dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MB" Then
            '        DirectCast(obEntry.value(0), CD).displayName = "Medicare"
            '        DirectCast(obEntry.value(0), CD).code = "1"
            '        DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicare: "}
            '    ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "MC" Then
            '        DirectCast(obEntry.value(0), CD).displayName = "Medicaid"
            '        DirectCast(obEntry.value(0), CD).code = "2"
            '        DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Medicaid: "}
            '    ElseIf dvCodes.ToTable.Rows(_cnt)(Col_Codes_Description) = "OT" Then
            '        DirectCast(obEntry.value(0), CD).displayName = "Other"
            '        DirectCast(obEntry.value(0), CD).code = "349"
            '        DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}

            '    Else
            '        DirectCast(obEntry.value(0), CD).displayName = "Other"
            '        DirectCast(obEntry.value(0), CD).code = "349"
            '        DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}
            '    End If
            'Else
            '    DirectCast(obEntry.value(0), CD).displayName = "Other"
            '    DirectCast(obEntry.value(0), CD).code = "349"
            '    DirectCast(DirectCast(List.item(k).Items(2),StrucDocList).item(_cnt).Items(0), StrucDocContent).Text = New String() {"Payer - Other: "}

            'End If
            'DirectCast(obEntry.value(0), CD).codeSystem = CodeSystem.PaymentTopology
            'DirectCast(obEntry.value(0), CD).codeSystemName = "Source of Payment Typology"
            'DirectCast(obEntry.value(0), CD).codeSystemVersion = Nothing





        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(Observation) Then
                Observation = Nothing
            End If

        End Try
        Return _Entry

    End Function
    Private Function GetSubstanceAdmin(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim SubstanceAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing
        Dim ManMaterial As POCD_MT000040UV02Material = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02SubstanceAdministration()
            SubstanceAdmin = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    Dim _cls As New gloPatientRegDBLayer()

                    Dim _ValusetOID As String = String.Empty
                    _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                    If _ValusetOID = "" Then
                        SubstanceAdmin.negationIndSpecified = True
                        SubstanceAdmin.negationInd = True
                    End If

                    _cls.Dispose()
                    _cls = Nothing

                End If
            End If

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Then
                SubstanceAdmin.classCode = ActClass.SBADM
                SubstanceAdmin.moodCode = x_DocumentSubstanceMood.RQO
                SubstanceAdmin.moodCodespecified = True


                SubstanceAdmin.templateId = New II(1) {}
                SubstanceAdmin.templateId(0) = New II()
                SubstanceAdmin.templateId(0).root = "2.16.840.1.113883.10.20.22.4.42"

                SubstanceAdmin.templateId(0).extension = _DateExtension22
                SubstanceAdmin.templateId(0).assigningAuthorityName = Nothing
                SubstanceAdmin.templateId(1) = New II()
                SubstanceAdmin.templateId(1).root = "2.16.840.1.113883.10.20.24.3.47"

                SubstanceAdmin.templateId(1).extension = _DateExtension2016
                SubstanceAdmin.templateId(1).assigningAuthorityName = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                SubstanceAdmin.classCode = ActClass.SBADM
                SubstanceAdmin.moodCode = x_DocumentSubstanceMood.EVN
                SubstanceAdmin.moodCodeSpecified = True


                SubstanceAdmin.templateId = New II(1) {}
                SubstanceAdmin.templateId(0) = New II()
                SubstanceAdmin.templateId(0).root = "2.16.840.1.113883.10.20.22.4.16"

                SubstanceAdmin.templateId(0).extension = _DateExtension22
                SubstanceAdmin.templateId(0).assigningAuthorityName = Nothing
                SubstanceAdmin.templateId(1) = New II()
                SubstanceAdmin.templateId(1).root = "2.16.840.1.113883.10.20.24.3.41"

                SubstanceAdmin.templateId(1).extension = _DateExtension2016
                SubstanceAdmin.templateId(1).assigningAuthorityName = Nothing
            Else

                SubstanceAdmin.classCode = ActClass.SBADM
                SubstanceAdmin.moodCode = x_DocumentSubstanceMood.EVN
                SubstanceAdmin.moodCodeSpecified = True


                SubstanceAdmin.templateId = New II(0) {}
                SubstanceAdmin.templateId(0) = New II()
                SubstanceAdmin.templateId(0).root = "2.16.840.1.113883.10.20.22.4.52"

                SubstanceAdmin.templateId(0).extension = _DateExtension22
                SubstanceAdmin.templateId(0).assigningAuthorityName = Nothing
                'SubstanceAdmin.templateId(1) = New II()
                'SubstanceAdmin.templateId(1).root = "2.16.840.1.113883.10.20.24.3.41"

                'SubstanceAdmin.templateId(1).extension = _DateExtension2016
                'SubstanceAdmin.templateId(1).assigningAuthorityName = Nothing
            End If


            SubstanceAdmin.id = New II(0) {}
            SubstanceAdmin.id(0) = New II()
            SubstanceAdmin.id(0).root = Guid.NewGuid.ToString()
            SubstanceAdmin.id(0).extension = Nothing
            SubstanceAdmin.id(0).assigningAuthorityName = Nothing
            SubstanceAdmin.text = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                SubstanceAdmin.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                SubstanceAdmin.text.Text = Nothing
            End If
            SubstanceAdmin.text.language = Nothing
            SubstanceAdmin.statusCode = New CS()
            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Then
            '    SubstanceAdmin.statusCode.code = "new"
            'Else
            SubstanceAdmin.statusCode.code = "active"
            ' End If

            SubstanceAdmin.statusCode.codeSystem = Nothing
            SubstanceAdmin.statusCode.codeSystemName = Nothing
            SubstanceAdmin.statusCode.codeSystemVersion = Nothing
            SubstanceAdmin.statusCode.displayName = Nothing

            SubstanceAdmin.effectiveTime = New SXCM_TS(1) {}
            '  SubstanceAdmin.effectiveTime = New IVL_TS(1) {}
            SubstanceAdmin.effectiveTime(0) = New IVL_TS()
            SubstanceAdmin.effectiveTime(0).value = Nothing
            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items = New QTY(1) {}
            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1) = New IVXB_TS()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("dtDischargeDate")) <> "" Then
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)("dtDischargeDate"), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
                End If
            Else
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(SubstanceAdmin.effectiveTime(0), IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
                End If
            End If




            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                If Not IsNothing(_dtDistinct.Rows(patientComponent)("sFrequency")) Then
                    If Convert.ToString(_dtDistinct.Rows(patientComponent)("sFrequency")) <> "" Then


                        Dim objCDADataExtraction = New gloCDADataExtraction()
                        Dim dtdetails As DataTable = objCDADataExtraction.GetFrequencyDetails(_dtDistinct.Rows(patientComponent)("sFrequency"))
                        If Not IsNothing(dtdetails) Then
                            If dtdetails.Rows.Count > 0 Then
                                If dtdetails.Rows(0)("sXsiType") IsNot Nothing AndAlso dtdetails.Rows(0)("sXsiType") <> "" Then
                                    If dtdetails.Rows(0)("sXsiType") = "PIVL_TS" Then
                                        SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                                        SubstanceAdmin.effectiveTime(1).value = Nothing
                                        SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).institutionSpecified1 = dtdetails.Rows(0)("binstitutionSpecified")

                                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()

                                        If dtdetails.Rows(0)("sValue") IsNot Nothing AndAlso dtdetails.Rows(0)("sValue") <> "" Then
                                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = dtdetails.Rows(0)("sValue")
                                        Else
                                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing

                                        End If
                                        If dtdetails.Rows(0)("sUnit") IsNot Nothing AndAlso dtdetails.Rows(0)("sUnit") <> "" Then
                                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.unit = dtdetails.Rows(0)("sUnit")
                                        Else
                                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.unit = Nothing
                                        End If
                                    Else
                                        SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                                        SubstanceAdmin.effectiveTime(1).value = Nothing
                                        SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing

                                    End If
                                Else
                                    SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                                    SubstanceAdmin.effectiveTime(1).value = Nothing
                                    SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing
                                End If
                            Else
                                SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                                SubstanceAdmin.effectiveTime(1).value = Nothing
                                SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                                DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                                DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                                DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing
                            End If
                        Else
                            SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                            SubstanceAdmin.effectiveTime(1).value = Nothing
                            SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                            DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing
                        End If
                        If Not IsNothing(dtdetails) Then
                            dtdetails.Dispose()
                            dtdetails = Nothing
                        End If
                    Else
                        SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                        SubstanceAdmin.effectiveTime(1).value = Nothing
                        SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                        DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing
                    End If

                Else
                    SubstanceAdmin.effectiveTime(1) = New PIVL_TS()
                    SubstanceAdmin.effectiveTime(1).value = Nothing
                    SubstanceAdmin.effectiveTime(1).operator = SetOperator.A
                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period = New PQ()
                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.nullFlavor = "NI"
                    DirectCast(SubstanceAdmin.effectiveTime(1), PIVL_TS).period.value = Nothing
                End If
            End If

            ''
            If Not IsNothing(_dtDistinct.Rows(patientComponent)("sAmount")) Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sAmount")) <> "" Then
                    SubstanceAdmin.doseQuantity = New IVL_PQ()
                    'SubstanceAdmin.doseQuantity.unit = Nothing
                    If Not IsNothing(_dtDistinct.Rows(patientComponent)("sAmount")) Then
                        If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                            Dim Quantity As String() = (_dtDistinct.Rows(patientComponent)("sAmount")).Split(" ")
                            If Quantity.Length > 0 Then
                                SubstanceAdmin.doseQuantity.value = If(Quantity(0) IsNot Nothing AndAlso Quantity(0) <> "", Quantity(0), Nothing)
                                SubstanceAdmin.doseQuantity.unit = If(Quantity(1) IsNot Nothing AndAlso Quantity(1) <> "", Quantity(1), Nothing)
                            Else
                                SubstanceAdmin.doseQuantity.value = Nothing
                            End If
                        Else
                            SubstanceAdmin.doseQuantity.value = _dtDistinct.Rows(patientComponent)("sAmount")
                            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sUnit")) = "" Then
                                SubstanceAdmin.doseQuantity.unit = Nothing
                            Else
                                SubstanceAdmin.doseQuantity.unit = _dtDistinct.Rows(patientComponent)("sUnit")
                            End If


                        End If
                    Else
                        SubstanceAdmin.doseQuantity.value = Nothing
                    End If
                Else
                    SubstanceAdmin.doseQuantity = New IVL_PQ()
                    SubstanceAdmin.doseQuantity.nullFlavor = "UNK"
                    SubstanceAdmin.doseQuantity.value = Nothing
                    SubstanceAdmin.doseQuantity.unit = Nothing
                End If
            Else
                SubstanceAdmin.doseQuantity = New IVL_PQ()
                SubstanceAdmin.doseQuantity.nullFlavor = "UNK"
                SubstanceAdmin.doseQuantity.value = Nothing
                SubstanceAdmin.doseQuantity.unit = Nothing
            End If



            SubstanceAdmin.consumable = New POCD_MT000040UV02Consumable()
            SubstanceAdmin.consumable.typeCode = Nothing
            SubstanceAdmin.consumable.typeCodeSpecified = False
            SubstanceAdmin.consumable.manufacturedProduct = New POCD_MT000040UV02ManufacturedProduct
            SubstanceAdmin.consumable.manufacturedProduct.classCodeSpecified = True
            SubstanceAdmin.consumable.manufacturedProduct.classCode = RoleClass.MANU
            SubstanceAdmin.consumable.manufacturedProduct.templateId = New II(0) {}
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0) = New II()
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).root = "2.16.840.1.113883.10.20.22.4.23"
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).extension = _DateExtension22
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).assigningAuthorityName = Nothing
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).nullFlavor = Nothing

            Else
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0) = New II()
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).root = "2.16.840.1.113883.10.20.22.4.54"
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).extension = _DateExtension22
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).assigningAuthorityName = Nothing
                SubstanceAdmin.consumable.manufacturedProduct.templateId(0).nullFlavor = Nothing

            End If


            
            SubstanceAdmin.consumable.manufacturedProduct.id = New II(0) {}
            SubstanceAdmin.consumable.manufacturedProduct.id(0) = New II()
            SubstanceAdmin.consumable.manufacturedProduct.id(0).root = Guid.NewGuid.ToString()
            SubstanceAdmin.consumable.manufacturedProduct.id(0).extension = Nothing
            SubstanceAdmin.consumable.manufacturedProduct.id(0).assigningAuthorityName = Nothing
            SubstanceAdmin.consumable.manufacturedProduct.id(0).nullFlavor = Nothing


            SubstanceAdmin.consumable.manufacturedProduct.Item = New POCD_MT000040UV02Material()
            ManMaterial = DirectCast(SubstanceAdmin.consumable.manufacturedProduct.Item, POCD_MT000040UV02Material)
            ManMaterial.code = New CE()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Active" Then
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_RxNorm) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_RxNorm)) <> "" Then
                    ManMaterial.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_RxNorm)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        ManMaterial.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        ManMaterial.code.valueSet = Nothing
                    End If
                    ManMaterial.code.valueSetVersion = Nothing
                    ManMaterial.code.codeSystem = CodeSystem.RxNorm
                Else
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        ManMaterial.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        ManMaterial.code.valueSet = Nothing
                    End If
                    ManMaterial.code.code = Nothing
                    ManMaterial.code.nullFlavor = "NA"
                    ManMaterial.code.codeSystemVersion = Nothing
                    ManMaterial.code.codeSystem = Nothing
                    ManMaterial.code.codeSystemName = Nothing
                End If
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Administered" Or Convert.ToString(_dtDistinct.Rows(patientComponent)("sTranID2") = "0") Then
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX)) <> "" Then
                    ManMaterial.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        ManMaterial.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        ManMaterial.code.valueSet = Nothing
                    End If
                    ManMaterial.code.valueSetVersion = Nothing
                    ManMaterial.code.codeSystem = CodeSystem.CVXCode
                    ManMaterial.lotNumberText = New ST()
                    ManMaterial.lotNumberText.Text = New String() {1}
                    '' ManMaterial.lotNumberText.nullFlavor = "NA"
                Else
                    ManMaterial.code.nullFlavor = "NA"
                    ManMaterial.code.code = Nothing
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        ManMaterial.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        ManMaterial.code.valueSet = Nothing
                    End If
                    ManMaterial.lotNumberText = New ST()
                    ManMaterial.lotNumberText.Text = New String() {1}
                    ManMaterial.code.codeSystem = Nothing
                    ManMaterial.code.codeSystemName = Nothing
                    ManMaterial.code.codeSystemVersion = Nothing
                End If
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Administered not done" Then
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                    ManMaterial.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        ManMaterial.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        ManMaterial.code.valueSet = Nothing
                    End If
                    ManMaterial.code.valueSetVersion = Nothing
                Else
                    ManMaterial.code.code = Nothing
                End If
                ManMaterial.code.codeSystem = CodeSystem.SNOMED_CT

            End If

            'Medication Order", "Medication Active


            ManMaterial.code.codeSystemName = Nothing
            ManMaterial.code.codeSystemVersion = Nothing
            ManMaterial.code.displayName = Nothing
            ManMaterial.code.originalText = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                ManMaterial.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                ManMaterial.code.originalText.Text = Nothing
            End If
            ManMaterial.code.originalText.language = Nothing
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Order" Then
                SubstanceAdmin.author = New POCD_MT000040UV02Author(0) {}

                SubstanceAdmin.author(0) = New POCD_MT000040UV02Author
                SubstanceAdmin.author(0).contextControlCode = Nothing
                SubstanceAdmin.author(0).typeCode = Nothing
                SubstanceAdmin.author(0).typeCodeSpecified = False
                SubstanceAdmin.author(0).templateId = New II(1) {}
                SubstanceAdmin.author(0).templateId(0) = New II()
                SubstanceAdmin.author(0).templateId(0).root = "2.16.840.1.113883.10.20.22.4.119"
                SubstanceAdmin.author(0).templateId(0).extension = Nothing
                SubstanceAdmin.author(0).templateId(0).assigningAuthorityName = Nothing
                SubstanceAdmin.author(0).time = New TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    SubstanceAdmin.author(0).time.value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    SubstanceAdmin.author(0).time.value = Nothing
                    SubstanceAdmin.author(0).time.nullFlavor = Nothing
                End If
                SubstanceAdmin.author(0).assignedAuthor = New POCD_MT000040UV02AssignedAuthor
                SubstanceAdmin.author(0).assignedAuthor.id = New II(0) {}

                SubstanceAdmin.author(0).assignedAuthor.id(0) = New II()
                SubstanceAdmin.author(0).assignedAuthor.classCode = Nothing
                SubstanceAdmin.author(0).assignedAuthor.classCodeSpecified = False
                '  SubstanceAdmin.author(0).assignedAuthor.id(0).nullFlavor = "NA"
                SubstanceAdmin.author(0).assignedAuthor.id(0).root = Guid.NewGuid.ToString()
                SubstanceAdmin.author(0).assignedAuthor.id(0).extension = Nothing
                SubstanceAdmin.author(0).assignedAuthor.id(0).assigningAuthorityName = Nothing


                ''
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                    SubstanceAdmin.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                    SubstanceAdmin.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                    SubstanceAdmin.entryRelationship(0).typeCode = ActRelationshipType.RSON
                    SubstanceAdmin.entryRelationship(0).Item = New POCD_MT000040UV02Procedure


                    SubstanceAdmin.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
                End If
                ''
            End If




        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(SubstanceAdmin) Then
                SubstanceAdmin = Nothing
            End If
            If Not IsNothing(ManMaterial) Then
                ManMaterial = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetMedicationAdministered(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim _SubstanceAdminEntry As POCD_MT000040UV02Entry = Nothing
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry

            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Act()
            act = DirectCast(_entry, POCD_MT000040UV02Entry).Item


            act.classCode = x_ActClassDocumentEntryAct.ACT
            act.moodCode = x_DocumentActMood.EVN
            act.moodCodeSpecified = True
            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()

                Dim _ValusetOID As String = String.Empty
                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    act.negationIndSpecified = True
                    act.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If


            act.templateId = New II(0) {}
            act.templateId(0) = New II()
            act.templateId(0).root = "2.16.840.1.113883.10.20.24.3.140"

            ''   act.templateId(0).extension = _DateExtension2016
            act.templateId(0).assigningAuthorityName = Nothing

            act.id = New II(0) {}
            act.id(0) = New II()
            act.id(0).root = "1.3.6.1.4.1.115"
            act.id(0).extension = Guid.NewGuid.ToString()
            act.id(0).assigningAuthorityName = Nothing

            act.code = New CD()
            act.code.code = "416118004"
            act.code.displayName = "Administration"
            act.code.codeSystem = CodeSystem.SNOMED_CT
            act.code.codeSystemName = "SNOMED_CT"
            act.code.codeSystemVersion = Nothing

            act.statusCode = New CS()
            act.statusCode.code = "completed"
            act.statusCode.codeSystem = Nothing
            act.statusCode.codeSystemName = Nothing
            act.statusCode.codeSystemVersion = Nothing
            act.statusCode.displayName = Nothing

            act.effectiveTime = New IVL_TS()
            act.effectiveTime.value = Nothing
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(act.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(act.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(act.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            act.entryRelationship = New POCD_MT000040UV02EntryRelationship(1) {}
            act.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            act.entryRelationship(0).typeCode = ActRelationshipType.COMP


            _SubstanceAdminEntry = New POCD_MT000040UV02Entry
            _SubstanceAdminEntry = GetSubstanceAdmin(_dtDistinct, patientComponent)

            act.entryRelationship(0).Item = New POCD_MT000040UV02SubstanceAdministration()

            act.entryRelationship(0).Item = _SubstanceAdminEntry.Item


            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                ' act.entryRelationship = New POCD_MT000040UV02EntryRelationship(1) {}
                act.entryRelationship(1) = New POCD_MT000040UV02EntryRelationship
                act.entryRelationship(1).typeCode = ActRelationshipType.RSON
                act.entryRelationship(1).Item = New POCD_MT000040UV02Procedure


                act.entryRelationship(1).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
            End If

            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Medication Administered not done" Then
            '    ''Reason of refusal
            '    act.entryRelationship(1) = New POCD_MT000040UV02EntryRelationship
            '    act.entryRelationship(1).typeCode = ActRelationshipType.COMP
            '    act.entryRelationship(1).Item = New POCD_MT000040UV02Observation
            '    obs = DirectCast(act.entryRelationship(1).Item, POCD_MT000040UV02Observation)

            '    obs.classCode = ActClassObservation.OBS
            '    obs.moodCode = x_ActMoodDocumentObservation.EVN
            '    obs.templateId = New II(0) {}
            '    obs.templateId(0) = New II()
            '    obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
            '    'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            '    obs.templateId(0).extension = _DateExtension24
            '    obs.templateId(0).assigningAuthorityName = Nothing


            '    obs.id = New II(0) {}
            '    obs.id(0) = New II()
            '    obs.id(0).root = Guid.NewGuid.ToString()
            '    obs.id(0).extension = Nothing
            '    obs.id(0).assigningAuthorityName = Nothing


            '    obs.code = New CD()
            '    obs.code.code = "77301-0"
            '    obs.code.codeSystem = CodeSystem.LOINC
            '    obs.code.codeSystemName = "LOINC"
            '    obs.code.displayName = "reason"
            '    obs.code.codeSystemVersion = Nothing

            '    obs.statusCode = New CS()
            '    obs.statusCode.code = "completed"
            '    obs.statusCode.codeSystem = Nothing
            '    obs.statusCode.codeSystemName = Nothing
            '    obs.statusCode.codeSystemVersion = Nothing
            '    obs.statusCode.displayName = Nothing


            '    obs.effectiveTime = New IVL_TS()
            '    obs.effectiveTime.value = Nothing
            '    obs.effectiveTime.nullFlavor = "NI"

            '    obs.value = New ANY(0) {}
            '    obs.value(0) = New CD()

            '    DirectCast(obs.value(0), CD).nullFlavor = "OTH"
            '    DirectCast(obs.value(0), CD).code = Nothing
            '    DirectCast(obs.value(0), CD).codeSystem = Nothing
            '    DirectCast(obs.value(0), CD).codeSystemName = Nothing
            '    DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            '    DirectCast(obs.value(0), CD).displayName = Nothing

            'End If



            ''


        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally


        End Try
        Return _entry

    End Function

    Private Function GetProblemObservation(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.4"

            obs.templateId(0).extension = Nothing
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.11"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then

                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.13"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.14"
            End If


            obs.templateId(1).extension = Nothing
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            obs.code.code = "282291009"
            obs.code.displayName = "diagnosis"
            obs.code.codeSystem = CodeSystem.SNOMED_CT
            obs.code.codeSystemName = "SNOMED_CT"
            obs.code.codeSystemVersion = Nothing

            obs.text = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                obs.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                obs.text.Text = Nothing
            End If
            obs.text.language = Nothing
            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            ''
            obs.value = New ANY(1) {}
            obs.value(0) = New CD()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
                DirectCast(obs.value(0), CD).valueSetVersion = Nothing
            Else
                DirectCast(obs.value(0), CD).code = Nothing
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
            End If
            DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(obs.value(0), CD).codeSystemName = Nothing
            DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).displayName = Nothing


            DirectCast(obs.value(0), CD).originalText = New ED()

            ''
            DirectCast(obs.value(0), CD).translation = New CD(1) {}
            DirectCast(obs.value(0), CD).translation(0) = New CD()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)) <> "" Then
                DirectCast(obs.value(0), CD).translation(0).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)
                'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                '    DirectCast(obs.value(0), CD).translation(0).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                'Else
                DirectCast(obs.value(0), CD).translation(0).valueSet = Nothing
                '  End If
                DirectCast(obs.value(0), CD).translation(0).valueSetVersion = Nothing
            Else
                DirectCast(obs.value(0), CD).translation(0).code = Nothing
            End If
            DirectCast(obs.value(0), CD).translation(0).codeSystem = CodeSystem.ICD9
            DirectCast(obs.value(0), CD).translation(0).codeSystemName = Nothing
            DirectCast(obs.value(0), CD).translation(0).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).translation(0).displayName = Nothing

            DirectCast(obs.value(0), CD).translation(1) = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)) <> "" Then
                DirectCast(obs.value(0), CD).translation(1).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    DirectCast(obs.value(0), CD).translation(1).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    DirectCast(obs.value(0), CD).translation(1).valueSet = Nothing
                End If
                DirectCast(obs.value(0), CD).translation(1).valueSetVersion = Nothing
            Else
                DirectCast(obs.value(0), CD).translation(1).code = Nothing
            End If
            DirectCast(obs.value(0), CD).translation(1).codeSystem = CodeSystem.ICD10
            DirectCast(obs.value(0), CD).translation(1).codeSystemName = Nothing
            DirectCast(obs.value(0), CD).translation(1).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).translation(1).displayName = Nothing
            ''
            DirectCast(obs.value(0), CD).originalText = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                DirectCast(obs.value(0), CD).originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                DirectCast(obs.value(0), CD).originalText.Text = Nothing
            End If
            DirectCast(obs.value(0), CD).originalText.language = Nothing
            obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            obs.entryRelationship(0).typeCode = ActRelationshipType.REFR
            obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)
            obsentry.classCode = ActClassObservation.OBS
            obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            obsentry.moodCodeSpecified = True
            obsentry.templateId = New II(1) {}
            obsentry.templateId(0) = New II()
            obsentry.templateId(0).root = "2.16.840.1.113883.10.20.22.4.6"
            obsentry.templateId(0).assigningAuthorityName = Nothing
            obsentry.templateId(0).extension = Nothing
            obsentry.templateId(0).nullFlavor = Nothing
            obsentry.templateId(1) = New II()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                obsentry.templateId(1).root = "2.16.840.1.113883.10.20.24.3.94"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then

                obsentry.templateId(1).root = "2.16.840.1.113883.10.20.24.3.95"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then

                obsentry.templateId(1).root = "2.16.840.1.113883.10.20.24.3.96"
            End If


            obsentry.templateId(1).assigningAuthorityName = Nothing
            obsentry.templateId(1).extension = Nothing
            obsentry.templateId(1).nullFlavor = Nothing

            obsentry.id() = New II(0) {}
            obsentry.id(0) = New II()
            obsentry.id(0).root = Guid.NewGuid.ToString()
            obsentry.id(0).extension = Nothing
            obsentry.id(0).assigningAuthorityName = Nothing
            obsentry.id(0).nullFlavor = Nothing


            obsentry.code = New CD()

            obsentry.code.code = "33999-4"

            obsentry.code.codeSystem = CodeSystem.LOINC
            obsentry.code.codeSystemName = "LOINC"
            obsentry.code.displayName = "status"
            obsentry.code.codeSystemVersion = Nothing

            obsentry.statusCode = New CS()
            obsentry.statusCode = New CS()
            obsentry.statusCode.code = "completed"
            obsentry.statusCode.codeSystem = Nothing
            obsentry.statusCode.codeSystemName = Nothing
            obsentry.statusCode.codeSystemVersion = Nothing
            obsentry.statusCode.displayName = Nothing


            obsentry.value = New ANY(0) {}
            obsentry.value(0) = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                DirectCast(obsentry.value(0), CD).code = "55561003"
                DirectCast(obsentry.value(0), CD).displayName = "Active"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then

                DirectCast(obsentry.value(0), CD).code = "73425007"
                DirectCast(obsentry.value(0), CD).displayName = "inactive"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then

                DirectCast(obsentry.value(0), CD).code = "413322009"
                DirectCast(obsentry.value(0), CD).displayName = "resolved"
            End If


            DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
            DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing

        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry

    End Function

    Private Function GetProblemAct(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim actentryrelation As POCD_MT000040UV02EntryRelationship = Nothing
        Try
         
                _entry = New POCD_MT000040UV02Entry
                DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Act()
                act = DirectCast(_entry, POCD_MT000040UV02Entry).Item
                act.classCode = x_ActClassDocumentEntryAct.ACT
                act.moodCode = x_DocumentActMood.EVN
                act.moodCodeSpecified = True
                act.templateId = New II(1) {}
                act.templateId(0) = New II()
                act.templateId(0).root = "2.16.840.1.113883.10.20.22.4.3"

                act.templateId(0).extension = _DateExtension2015
                act.templateId(0).assigningAuthorityName = Nothing

                act.templateId(1) = New II()
                'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                '    act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.121"
                'ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then
                '    act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.123"
                'ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then
                '    act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.125"



                'End If

                act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.137"
            If (_IsFromoldVersion = True) Then
                act.templateId(1).extension = Nothing ''for reporting year 2017 or prior dateextension is not require 
            Else
                act.templateId(1).extension = _DateExtension2016_1  ''for reporting year 2018  dateextension  require 
            End If
            If ReportingYear = "2019" Then
                act.templateId(1).extension = _Version2019
            End If

                act.templateId(1).assigningAuthorityName = Nothing

                act.id = New II(0) {}
                act.id(0) = New II()
                act.id(0).root = Guid.NewGuid.ToString()
                act.id(0).extension = Nothing
                act.id(0).assigningAuthorityName = Nothing

                act.code = New CD()
                act.code.code = "CONC"
                act.code.displayName = "diagnosis"
                act.code.codeSystem = "2.16.840.1.113883.5.6"
                act.code.codeSystemName = "Concern"
                act.code.codeSystemVersion = Nothing


                act.statusCode = New CS()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                    act.statusCode.code = "active"
                ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then
                    act.statusCode.code = "suspended"
                ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then
                    act.statusCode.code = "completed"
                End If
                '   act.statusCode.code = "active"
                act.statusCode.codeSystem = Nothing
                act.statusCode.codeSystemName = Nothing
                act.statusCode.codeSystemVersion = Nothing
                act.statusCode.displayName = Nothing


                act.effectiveTime = New IVL_TS()

                act.effectiveTime.value = Nothing
                DirectCast(act.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
                DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
                DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
                DirectCast(act.effectiveTime, IVL_TS).Items = New QTY(1) {}
                DirectCast(act.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = "UNK"
                End If

                DirectCast(act.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("dtDischargeDate")) <> "" Then
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)("dtDischargeDate"), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = "UNK"
                End If

                'DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                'DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing




                act.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}

                act.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship()

                actentryrelation = act.entryRelationship(0)
                actentryrelation.typeCode = ActRelationshipType.SUBJ
                actentryrelation.Item = New POCD_MT000040UV02Observation()

                obs = actentryrelation.Item

                ' DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
                ' obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
                ''
                ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
                obs.classCode = ActClassObservation.OBS
                obs.moodCode = x_ActMoodDocumentObservation.EVN
                obs.moodCodeSpecified = True



                obs.templateId = New II(1) {}
                obs.templateId(0) = New II()
                obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.4"

                obs.templateId(0).extension = _DateExtension2015
                obs.templateId(0).assigningAuthorityName = Nothing
                obs.templateId(1) = New II()
                'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Active" Then
                '    obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.11"
                'ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Inactive" Then

                '    obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.13"
                'ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnosis Resolved" Then
                '    obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.14"
                'End If


            obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.135"
            If ReportingYear = "2019" Then
                obs.templateId(1).extension = _Version2019
            Else
                obs.templateId(1).extension = Nothing
            End If


                obs.templateId(1).assigningAuthorityName = Nothing

                obs.id = New II(0) {}
                obs.id(0) = New II()
                obs.id(0).root = Guid.NewGuid.ToString()
                obs.id(0).extension = Nothing
                obs.id(0).assigningAuthorityName = Nothing

                obs.code = New CD()
                obs.code.code = "29308-4"
                obs.code.displayName = "diagnosis"
                obs.code.codeSystem = CodeSystem.LOINC
                obs.code.codeSystemName = "LOINC"
                obs.code.codeSystemVersion = Nothing

                obs.code.translation = New CD(0) {}
                obs.code.translation(0) = New CD()
                obs.code.translation(0).code = "282291009"
                obs.code.translation(0).codeSystem = "2.16.840.1.113883.6.96"
                obs.code.translation(0).codeSystemName = Nothing
                obs.code.translation(0).codeSystemVersion = Nothing
                obs.code.translation(0).displayName = Nothing

                obs.text = New ED()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                    obs.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
                Else
                    obs.text.Text = Nothing
                End If
                obs.text.language = Nothing
                obs.statusCode = New CS()
                obs.statusCode.code = "completed"
                obs.statusCode.codeSystem = Nothing
                obs.statusCode.codeSystemName = Nothing
                obs.statusCode.codeSystemVersion = Nothing
                obs.statusCode.displayName = Nothing


                obs.effectiveTime = New IVL_TS()

                obs.effectiveTime.value = Nothing
                DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
                DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
                DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
                DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
                DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
                End If

                DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("dtDischargeDate")) <> "" Then
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)("dtDischargeDate"), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = "UNK"

                End If

                'DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                'DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

                ''
                obs.value = New ANY(1) {}
                obs.value(0) = New CD()

                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                    DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                    If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                        'DirectCast(obs.value(0), CD).qualifier = New CR(0) {}
                        'DirectCast(obs.value(0), CD).qualifier(0) = New CR()
                        'DirectCast(obs.value(0), CD).qualifier(0).name = New CV()
                        'DirectCast(obs.value(0), CD).qualifier(0).name.code = "182353008"
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystem = CodeSystem.SNOMED_CT
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystemName = "SNOMED_CT"
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystemVersion = Nothing
                        'DirectCast(obs.value(0), CD).qualifier(0).name.displayName = Nothing

                        'DirectCast(obs.value(0), CD).qualifier(0).value = New CD()
                        'DirectCast(obs.value(0), CD).qualifier(0).value.code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                        'DirectCast(obs.value(0), CD).qualifier(0).value.displayName = Nothing
                        'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        '    DirectCast(obs.value(0), CD).qualifier(0).value.valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        'Else
                        '    DirectCast(obs.value(0), CD).qualifier(0).value.valueSet = Nothing
                        'End If
                        'DirectCast(obs.value(0), CD).qualifier(0).value.valueSetVersion = Nothing

                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystem = CodeSystem.SNOMED_CT
                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystemName = "SNOMED_CT"
                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystemVersion = Nothing
                        '  If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                        obs.targetSiteCode = New CD(0) {}
                        obs.targetSiteCode(0) = New CD()

                        obs.targetSiteCode(0).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                        obs.targetSiteCode(0).displayName = Nothing

                        If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                            obs.targetSiteCode(0).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        Else
                            obs.targetSiteCode(0).valueSet = Nothing
                        End If
                        obs.targetSiteCode(0).valueSetVersion = Nothing
                        obs.targetSiteCode(0).codeSystemVersion = Nothing
                        obs.targetSiteCode(0).codeSystem = CodeSystem.SNOMED_CT
                        obs.targetSiteCode(0).codeSystemName = "SNOMED_CT"
                    End If
                ElseIf _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then

                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)) <> "" Then
                        DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)
                        If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                            DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                        Else
                            DirectCast(obs.value(0), CD).valueSet = Nothing
                        End If
                        DirectCast(obs.value(0), CD).codeSystem = CodeSystem.ICD9

                    ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)) <> "" Then
                        DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)
                        If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                            DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                        Else
                            DirectCast(obs.value(0), CD).valueSet = Nothing
                        End If
                        DirectCast(obs.value(0), CD).codeSystem = CodeSystem.ICD10

                    End If
                    If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                        obs.targetSiteCode = New CD(0) {}
                        obs.targetSiteCode(0) = New CD()

                        obs.targetSiteCode(0).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                        obs.targetSiteCode(0).displayName = Nothing

                        If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                            obs.targetSiteCode(0).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        Else
                            obs.targetSiteCode(0).valueSet = Nothing
                        End If
                        obs.targetSiteCode(0).valueSetVersion = Nothing
                        obs.targetSiteCode(0).codeSystemVersion = Nothing
                        obs.targetSiteCode(0).codeSystem = CodeSystem.SNOMED_CT
                        obs.targetSiteCode(0).codeSystemName = "SNOMED_CT"

                        'DirectCast(obs.value(0), CD).qualifier = New CR(0) {}
                        'DirectCast(obs.value(0), CD).qualifier(0) = New CR()
                        'DirectCast(obs.value(0), CD).qualifier(0).name = New CV()
                        'DirectCast(obs.value(0), CD).qualifier(0).name.code = "182353008"
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystem = CodeSystem.SNOMED_CT
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystemName = "SNOMED_CT"
                        'DirectCast(obs.value(0), CD).qualifier(0).name.codeSystemVersion = Nothing
                        'DirectCast(obs.value(0), CD).qualifier(0).name.displayName = Nothing

                        'DirectCast(obs.value(0), CD).qualifier(0).value = New CD()
                        'DirectCast(obs.value(0), CD).qualifier(0).value.code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                        'DirectCast(obs.value(0), CD).qualifier(0).value.displayName = Nothing
                        'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        '    DirectCast(obs.value(0), CD).qualifier(0).value.valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        'Else
                        '    DirectCast(obs.value(0), CD).qualifier(0).value.valueSet = Nothing
                        'End If
                        'DirectCast(obs.value(0), CD).qualifier(0).value.valueSetVersion = Nothing

                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystem = CodeSystem.SNOMED_CT
                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystemName = "SNOMED_CT"
                        'DirectCast(obs.value(0), CD).qualifier(0).value.codeSystemVersion = Nothing

                    End If

                Else
                    DirectCast(obs.value(0), CD).code = Nothing
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                    DirectCast(obs.value(0), CD).nullFlavor = "OTH"
                    DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                End If

                DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing

                DirectCast(obs.value(0), CD).originalText = New ED()

                ''
                DirectCast(obs.value(0), CD).translation = New CD(1) {}
                DirectCast(obs.value(0), CD).translation(0) = New CD()

                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)) <> "" Then
                    DirectCast(obs.value(0), CD).translation(0).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD9)
                    'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    '    DirectCast(obs.value(0), CD).translation(0).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    'Else
                    DirectCast(obs.value(0), CD).translation(0).valueSet = Nothing
                    '  End If
                    DirectCast(obs.value(0), CD).translation(0).valueSetVersion = Nothing

                Else
                    DirectCast(obs.value(0), CD).translation(0).code = Nothing
                End If
                DirectCast(obs.value(0), CD).translation(0).codeSystem = CodeSystem.ICD9
                DirectCast(obs.value(0), CD).translation(0).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).translation(0).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).translation(0).displayName = Nothing

                DirectCast(obs.value(0), CD).translation(1) = New CD()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)) <> "" Then
                    DirectCast(obs.value(0), CD).translation(1).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ICD10)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        DirectCast(obs.value(0), CD).translation(1).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        DirectCast(obs.value(0), CD).translation(1).valueSet = Nothing
                    End If
                    DirectCast(obs.value(0), CD).translation(1).valueSetVersion = Nothing

                Else
                    DirectCast(obs.value(0), CD).translation(1).code = Nothing
                End If
                DirectCast(obs.value(0), CD).translation(1).codeSystem = CodeSystem.ICD10
                DirectCast(obs.value(0), CD).translation(1).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).translation(1).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).translation(1).displayName = Nothing
                ''
                DirectCast(obs.value(0), CD).originalText = New ED()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                    DirectCast(obs.value(0), CD).originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
                Else
                    DirectCast(obs.value(0), CD).originalText.Text = Nothing
                End If
                DirectCast(obs.value(0), CD).originalText.language = Nothing

        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetTobaccoObservation(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(0) {}
            obs.templateId(0) = New II()
            ''obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.85"
            obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.103"

            obs.templateId(0).extension = _DateExtension2016
            obs.templateId(0).assigningAuthorityName = Nothing


            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            obs.code.code = "ASSERTION"
            obs.code.displayName = "Assertion"
            obs.code.codeSystem = "2.16.840.1.113883.5.4"
            obs.code.codeSystemName = "ActCode"
            obs.code.codeSystemVersion = Nothing



            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            ''
            obs.value = New ANY(1) {}
            obs.value(0) = New CD()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
                DirectCast(obs.value(0), CD).valueSetVersion = Nothing
            Else
                DirectCast(obs.value(0), CD).code = Nothing
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                End If
                DirectCast(obs.value(0), CD).nullFlavor = "OTH"
            End If
            DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(obs.value(0), CD).codeSystemName = Nothing
            DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).displayName = Nothing


            DirectCast(obs.value(0), CD).originalText = New ED()


            DirectCast(obs.value(0), CD).originalText = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                DirectCast(obs.value(0), CD).originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                DirectCast(obs.value(0), CD).originalText.Text = Nothing
            End If
            DirectCast(obs.value(0), CD).originalText.language = Nothing


        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetDiagnosticStudyPerformed(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Finding" Then
            '    obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.2"
            'Else
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Functional Status Performed" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Performed" Then
                'obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.67"
                obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.13"
            Else

                obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.13"
            End If


            obs.templateId(0).extension = _DateExtension22
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnostic Study Performed" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.18"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Performed" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.59"
                'ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Finding Finding" Then
                '    obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.57"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Functional Status Performed" Then
                'obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.28"
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.26"
            End If
            If ReportingYear = "2019" Then
                obs.templateId(1).extension = _Version2019
            Else
                obs.templateId(1).extension = _DateExtension2016
            End If


            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = "1.3.6.1.4.1.115"
            obs.id(0).extension = Guid.NewGuid.ToString()
            'obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Performed" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    Dim _cls As New gloPatientRegDBLayer()

                    Dim _ValusetOID As String = String.Empty
                    _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                    If _ValusetOID = "" Then
                        obs.negationIndSpecified = True
                        obs.negationInd = True
                    End If

                    _cls.Dispose()
                    _cls = Nothing

                End If

                obs.code = New CD()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then
                    obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        obs.code.valueSet = Nothing
                    End If
                    obs.code.valueSetVersion = Nothing
                    obs.code.codeSystem = CodeSystem.LOINC
                    obs.code.codeSystemName = "LOINC"
                ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                    obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        obs.code.valueSet = Nothing
                    End If
                    obs.code.valueSetVersion = Nothing
                    obs.code.codeSystem = CodeSystem.SNOMED_CT
                    obs.code.codeSystemName = "SNOMED_CT"
                Else
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        obs.code.valueSet = Nothing
                    End If
                    obs.code.nullFlavor = "NA"
                    obs.code.code = Nothing
                    obs.code.codeSystem = Nothing
                    obs.code.codeSystemName = Nothing
                End If

                obs.code.displayName = Nothing

                obs.code.codeSystemVersion = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Functional Status Performed" Or _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnostic Study Performed" Then
                ''cms 125-Diagnostic study HCPCS code 
                Dim dv As New DataView
                dv = dtQRDA1Data.Copy().DefaultView

                dv.RowFilter = "TransactionID = '" & _dtDistinct.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtDistinct.Rows(patientComponent)("Category") & "' "

                obs.code = New CD()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then
                    obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        obs.code.valueSet = Nothing
                    End If
                    obs.code.valueSetVersion = Nothing
                    obs.code.codeSystem = CodeSystem.LOINC
                    obs.code.codeSystemName = "LOINC"

                ElseIf Not IsNothing(dv) AndAlso dv.Count > 0 Then
                    If dv.ToTable.Rows(0)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("sCPTCode")) <> "" Then
                        obs.code.code = dv.ToTable.Rows(0)("sCPTCode")
                        If dv.ToTable.Rows(0)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("SDTCValueSet")) <> "" Then
                            obs.code.valueSet = dv.ToTable.Rows(0)("SDTCValueSet")
                        Else
                            obs.code.valueSet = Nothing
                        End If

                        obs.code.valueSetVersion = Nothing
                        If IsNumeric(dv.ToTable.Rows(0)("sCPTCode").ToString().Substring(0, 1)) Then
                            obs.code.codeSystem = CodeSystem.CPT
                            obs.code.codeSystemName = "CPT"
                        Else
                            obs.code.codeSystem = CodeSystem.HCPCS
                            obs.code.codeSystemName = "HCPCS"

                        End If


                    End If



                Else



                    obs.code.code = Nothing
                    obs.code.valueSetVersion = Nothing
                    obs.code.codeSystem = CodeSystem.LOINC
                    obs.code.codeSystemName = "LOINC"
                End If
                If Not IsNothing(dv) Then
                    dv.Dispose()
                    dv = Nothing
                End If
                obs.code.displayName = Nothing

                obs.code.codeSystemVersion = Nothing
            Else
                obs.code = New CD()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                    obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                        obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                    Else
                        obs.code.valueSet = Nothing
                    End If
                    obs.code.valueSetVersion = Nothing
                Else
                    obs.code.code = Nothing
                End If

                obs.code.displayName = Nothing
                obs.code.codeSystem = CodeSystem.SNOMED_CT
                obs.code.codeSystemName = "SNOMED_CT"
                obs.code.codeSystemVersion = Nothing
                obs.text = New ED()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                    obs.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
                Else
                    obs.text.Text = Nothing
                End If
                obs.text.language = Nothing
            End If

            ''

            ''

            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else

                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If

            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Physical Exam Performed" Then
                obs.value = New ANY(0) {}
                obs.value(0) = New CD()
                If Not IsNothing(_dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMax)) AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMax)) <> "" Then
                    obs.value = New ANY(0) {}
                    obs.value(0) = New PQ()
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMax)
                    DirectCast(obs.value(0), PQ).unit = "mmHg"
                ElseIf Not IsNothing(_dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMin)) AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMin)) <> "" Then
                    obs.value = New ANY(0) {}
                    obs.value(0) = New PQ()
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)(Col_QRDA1_BPSittingMin)
                    DirectCast(obs.value(0), PQ).unit = "mmHg"
                ElseIf Not IsNothing(_dtDistinct.Rows(patientComponent)("BPBMI")) AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("BPBMI")) <> "" Then
                    obs.value = New ANY(0) {}
                    obs.value(0) = New PQ()
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)("BPBMI")
                    DirectCast(obs.value(0), PQ).unit = "Kg/m2"
                ElseIf Not IsNothing(_dtDistinct.Rows(patientComponent)("CodeValue")) AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeValue")) <> "" Then
                    'obs.value = New ANY(0) {}
                    'obs.value(0) = New CD()
                    'obs.value(0).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")

                    obs.value = New ANY(1) {}
                    obs.value(0) = New CD()

                    If _dtDistinct.Rows(patientComponent)("CodeValue") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeValue")) <> "" Then
                        DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("CodeValue")
                        If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                            DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        Else
                            DirectCast(obs.value(0), CD).valueSet = Nothing
                        End If
                        DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                        DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                    Else
                        DirectCast(obs.value(0), CD).code = Nothing
                        obs.value(0).nullFlavor = "UNK"
                        'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        '    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                        'Else
                        '    DirectCast(obs.value(0), CD).valueSet = Nothing
                        'End If
                    End If

                    DirectCast(obs.value(0), CD).codeSystemName = Nothing
                    DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                    DirectCast(obs.value(0), CD).displayName = Nothing



                    '  DirectCast(obs.value(0), PQ).unit = "Kg/m2"
                Else
                    DirectCast(obs.value(0), CD).nullFlavor = "UNK"
                    DirectCast(obs.value(0), CD).code = Nothing
                    DirectCast(obs.value(0), CD).codeSystem = Nothing
                    DirectCast(obs.value(0), CD).codeSystemName = Nothing
                    DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                    DirectCast(obs.value(0), CD).displayName = Nothing
                End If

                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
                    obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                    obs.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
                End If

            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Functional Status Performed" Then
                obs.value = New ANY(1) {}
                obs.value(0) = New PQ()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)) <> "" Then
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)
                Else
                    DirectCast(obs.value(0), PQ).value = Nothing
                End If

                DirectCast(obs.value(0), PQ).unit = "%"

            Else
                obs.value = New ANY(1) {}
                obs.value(0) = New CD()
                DirectCast(obs.value(0), CD).nullFlavor = "NA"
                DirectCast(obs.value(0), CD).code = Nothing
                DirectCast(obs.value(0), CD).codeSystem = Nothing
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing

            End If

        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetResultObservation(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing

        Try

            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''

            '  obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS

            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True





            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()

            obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.2"



            obs.templateId(0).extension = _DateExtension2015
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnostic Study Result" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.20"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Laboratory Test Performed" Then 'Laboratory Test Performed
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.38"

            Else

                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.40"
            End If
            obs.templateId(1).extension = _DateExtension2016
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = "1.3.6.1.4.1.115"
            obs.id(0).extension = Guid.NewGuid.ToString()
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then
                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing
            Else
                obs.code.code = Nothing
            End If



            obs.code.codeSystem = CodeSystem.LOINC
            obs.code.codeSystemName = "LOINC"
            obs.code.codeSystemVersion = Nothing
            obs.code.displayName = Nothing
            obs.code.originalText = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                obs.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                obs.code.originalText.Text = Nothing
            End If
            obs.code.originalText.language = Nothing



            obs.statusCode = New CS()

            obs.statusCode.code = "completed"


            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            obs.value = New ANY(1) {}
            obs.value(0) = New CD()
            ' obs.value = New ANY(1) {}
            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then


                If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    '    DirectCast(obs.value(0), CD).valueSet = Nothing
                    'End If
                    'commenting valueset to satisfy the file in validation tool 
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                    DirectCast(obs.value(0), CD).valueSetVersion = Nothing

                Else
                    DirectCast(obs.value(0), CD).code = Nothing
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    DirectCast(obs.value(0), CD).valueSet = Nothing
                    DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                    'End If
                End If
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing


                DirectCast(obs.value(0), CD).originalText = New ED()
                DirectCast(obs.value(0), CD).originalText.language = Nothing
                ''
                DirectCast(obs.value(0), CD).translation = New CD(1) {}
                DirectCast(obs.value(0), CD).translation(0) = New CD()

                If _dtDistinct.Rows(patientComponent)("sReasonICD9") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonICD9")) <> "" Then
                    DirectCast(obs.value(0), CD).translation(0).code = _dtDistinct.Rows(patientComponent)("sReasonICD9")
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    DirectCast(obs.value(0), CD).translation(0).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    DirectCast(obs.value(0), CD).translation(0).valueSet = Nothing
                    'End If
                    ' DirectCast(obs.value(0), CD).translation(0).valueSet = Nothing
                    DirectCast(obs.value(0), CD).translation(0).valueSetVersion = Nothing
                Else
                    DirectCast(obs.value(0), CD).translation(0).code = Nothing
                    DirectCast(obs.value(0), CD).translation(0).valueSet = Nothing
                    DirectCast(obs.value(0), CD).translation(0).valueSetVersion = Nothing
                End If
                DirectCast(obs.value(0), CD).translation(0).codeSystem = CodeSystem.ICD9
                DirectCast(obs.value(0), CD).translation(0).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).translation(0).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).translation(0).displayName = Nothing

                DirectCast(obs.value(0), CD).translation(1) = New CD()
                If _dtDistinct.Rows(patientComponent)("sReasonICD10") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonICD10")) <> "" Then
                    DirectCast(obs.value(0), CD).translation(1).code = _dtDistinct.Rows(patientComponent)("sReasonICD10")
                    'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                    '    DirectCast(obs.value(0), CD).translation(1).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    'Else
                    DirectCast(obs.value(0), CD).translation(1).valueSet = Nothing
                    'End If
                    DirectCast(obs.value(0), CD).translation(1).valueSetVersion = Nothing
                Else
                    DirectCast(obs.value(0), CD).translation(1).valueSet = Nothing
                    DirectCast(obs.value(0), CD).translation(1).valueSetVersion = Nothing
                    DirectCast(obs.value(0), CD).translation(1).code = Nothing
                End If
                DirectCast(obs.value(0), CD).translation(1).codeSystem = CodeSystem.ICD10
                DirectCast(obs.value(0), CD).translation(1).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).translation(1).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).translation(1).displayName = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)("sReasonLOINC") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonLOINC")) <> "" Then
                DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonLOINC")
                'If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                '    DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                'Else
                '    DirectCast(obs.value(0), CD).valueSet = Nothing
                'End If
                DirectCast(obs.value(0), CD).valueSet = Nothing
                DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.LOINC
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing
            Else
                obs.value = New ANY(1) {}
                obs.value(0) = New PQ()
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)) <> "" Then
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)
                Else
                    DirectCast(obs.value(0), PQ).value = Nothing
                End If

                DirectCast(obs.value(0), PQ).unit = "%"

            End If



            'obs.value(0) = New ST()
            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)) <> "" Then
            '    DirectCast(obs.value(0), ST).Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeValue)}
            'Else
            '    DirectCast(obs.value(0), ST).Text = Nothing
            'End If
            'DirectCast(obs.value(0), ST).language = Nothing
            'DirectCast(obs.value(0), ST).mediaType = Nothing
            'DirectCast(obs.value(0), ST).nullFlavor = Nothing
        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
        End Try
        Return _entry

    End Function

    Private Function GetOrderObservation(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing

        Try

            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()

                Dim _ValusetOID As String = String.Empty
                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    obs.negationIndSpecified = True
                    obs.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If

            '  obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS

            obs.moodCode = x_ActMoodDocumentObservation.RQO
            obs.moodCodespecified = True



            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()

            obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.44"



            obs.templateId(0).extension = _DateExtension22
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()


            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Diagnostic Study Order" Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.17"

            Else
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.37"
            End If



            obs.templateId(1).extension = _DateExtension2016
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = "1.3.6.1.4.1.115"
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then
                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing
                obs.code.codeSystem = CodeSystem.LOINC
                obs.code.codeSystemName = "LOINC"
            Else
                obs.code.code = Nothing
                obs.code.nullFlavor = "NA"
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.codeSystem = Nothing
                obs.code.codeSystemName = Nothing
            End If



            obs.code.codeSystem = CodeSystem.LOINC
            obs.code.codeSystemName = "LOINC"
            obs.code.codeSystemVersion = Nothing
            obs.code.displayName = Nothing
            obs.code.originalText = New ED()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                obs.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                obs.code.originalText.Text = Nothing
            End If
            obs.code.originalText.language = Nothing



            obs.statusCode = New CS()

            'obs.statusCode.code = "new"
            obs.statusCode.code = "active"


            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing

            obs.author = New POCD_MT000040UV02Author(0) {}
            obs.author(0) = New POCD_MT000040UV02Author
            obs.author(0).templateId = New II(0) {}
            obs.author(0).templateId(0) = New II()
            obs.author(0).templateId(0).root = "2.16.840.1.113883.10.20.22.4.119"
            obs.author(0).templateId(0).extension = Nothing
            obs.author(0).templateId(0).assigningAuthorityName = Nothing
            obs.author(0).time = New TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                obs.author(0).time.value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                obs.author(0).time.value = Nothing
                obs.author(0).time.nullFlavor = Nothing
            End If
            obs.author(0).assignedAuthor = New POCD_MT000040UV02AssignedAuthor
            obs.author(0).assignedAuthor.id = New II(0) {}

            obs.author(0).assignedAuthor.id(0) = New II()
            obs.author(0).assignedAuthor.classCode = Nothing
            obs.author(0).assignedAuthor.classCodeSpecified = False
            ' obs.author(0).assignedAuthor.id(0).nullFlavor = "NA"
            obs.author(0).assignedAuthor.id(0).root = Guid.NewGuid.ToString()
            obs.author(0).assignedAuthor.id(0).extension = Nothing
            obs.author(0).assignedAuthor.id(0).assigningAuthorityName = Nothing

            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
                obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                obs.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
            End If

        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetMedicationAllergy(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer, ByVal isAllergy As Boolean) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Dim playEntity As POCD_MT000040UV02PlayingEntity = Nothing
        Dim observation As POCD_MT000040UV02Observation = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.7"

            obs.templateId(0).extension = _DateExtension22
            obs.templateId(0).assigningAuthorityName = Nothing

            obs.templateId(1) = New II()
            If isAllergy Then
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.44"
            Else
                obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.46"
            End If


            obs.templateId(1).extension = _DateExtension2016
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            obs.code.code = "ASSERTION"
            obs.code.displayName = "Assertion"
            obs.code.codeSystem = "2.16.840.1.113883.5.4"
            obs.code.codeSystemName = "ActCode"
            obs.code.codeSystemVersion = Nothing

            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            ''
            obs.value = New ANY(1) {}
            obs.value(0) = New CD()

            If isAllergy Then
                DirectCast(obs.value(0), CD).code = "416098002"
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obs.value(0), CD).codeSystemName = "SNOMED_CT"
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = "Drug allergy"
            Else
                DirectCast(obs.value(0), CD).code = "59037007"
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obs.value(0), CD).codeSystemName = "SNOMED_CT"
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = "Drug intolerance"
            End If


            obs.participant = New POCD_MT000040UV02Participant3(0) {}
            obs.participant(0) = New POCD_MT000040UV02Participant3
            obs.participant(0).typeCode = ParticipationType.CSM
            obs.participant(0).participantRole = New POCD_MT000040UV02ParticipantRole
            obs.participant(0).participantRole.classCode = RoleClassRoot.MANU
            obs.participant(0).participantRole.addr = Nothing
            obs.participant(0).participantRole.code = Nothing
            obs.participant(0).participantRole.id = Nothing
            obs.participant(0).participantRole.telecom = Nothing
            obs.participant(0).participantRole.typeId = Nothing

            obs.participant(0).participantRole.Item = New POCD_MT000040UV02PlayingEntity()
            playEntity = DirectCast(obs.participant(0).participantRole.Item, POCD_MT000040UV02PlayingEntity)
            playEntity.classCode = EntityClassRoot.MMAT
            playEntity.code = New CE()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX)) <> "" Then
                playEntity.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_CVX)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    playEntity.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    playEntity.code.valueSet = Nothing
                End If
                playEntity.code.valueSetVersion = Nothing
            Else
                playEntity.code.code = Nothing
            End If



            playEntity.code.displayName = Nothing
            playEntity.code.codeSystem = CodeSystem.CVXCode
            playEntity.code.codeSystemName = Nothing
            playEntity.code.codeSystemVersion = Nothing

            ''Severity observation
            obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship()
            obs.entryRelationship(0).typeCode = ActRelationshipType.SUBJ
            obs.entryRelationship(0).inversionInd = True
            obs.entryRelationship(0).inversionIndSpecified = Boolean.TrueString
            obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation()
            observation = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)
            observation.classCode = ActClassObservation.OBS
            observation.moodCode = x_ActMoodDocumentObservation.EVN
            observation.moodCodespecified = True
            observation.templateId = New II(0) {}
            observation.templateId(0) = New II()
            observation.templateId(0).root = "2.16.840.1.113883.10.20.22.4.8"
            observation.templateId(0).assigningAuthorityName = Nothing
            observation.templateId(0).extension = _DateExtension22

            observation.code = New CD()
            observation.code.code = "SEV"
            observation.code.codeSystem = "2.16.840.1.113883.5.4"
            observation.code.codeSystemName = "ActCode"
            observation.code.codeSystemVersion = Nothing
            observation.code.displayName = "Severity Observation"

            observation.text = New ED()
            observation.text.mediaType = Nothing
            observation.text.language = Nothing
            observation.text.reference = New TEL()
            observation.text.reference.value = "severity"

            observation.statusCode = New CS()
            observation.statusCode.code = "completed"
            observation.statusCode.codeSystem = Nothing
            observation.statusCode.codeSystemName = Nothing
            observation.statusCode.codeSystemVersion = Nothing
            observation.statusCode.displayName = Nothing

            observation.value = New ANY(0) {}
            observation.value(0) = New CD()
            DirectCast(observation.value(0), CD).code = "24484000"
            DirectCast(observation.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(observation.value(0), CD).codeSystemName = "SNOMED_CT"
            DirectCast(observation.value(0), CD).codeSystemVersion = Nothing
            DirectCast(observation.value(0), CD).displayName = "severe"

        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetProcedureIntolerance(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer)
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Dim playEntity As POCD_MT000040UV02PlayingEntity = Nothing
        Dim _procPerformed As POCD_MT000040UV02Entry = Nothing
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True



            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.62"
            ' obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.30"

            obs.templateId(0).extension = _DateExtension2016
            obs.templateId(0).assigningAuthorityName = Nothing

            obs.templateId(1) = New II()

            obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.104"


            obs.templateId(1).extension = _DateExtension2016
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            obs.code.code = "ASSERTION"
            obs.code.displayName = "Assertion"
            obs.code.codeSystem = "2.16.840.1.113883.5.4"
            obs.code.codeSystemName = "ActCode"
            obs.code.codeSystemVersion = Nothing

            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If


            ''
            obs.value = New ANY(0) {}
            obs.value(0) = New CD()

            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
            '    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
            '    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
            '        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        DirectCast(obs.value(0), CD).valueSet = Nothing
            '    End If
            '    DirectCast(obs.value(0), CD).valueSetVersion = Nothing
            'Else
            '    DirectCast(obs.value(0), CD).code = Nothing
            'End If
            DirectCast(obs.value(0), CD).code = "102460003"
            DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            DirectCast(obs.value(0), CD).codeSystemName = "SNOMED_CT"
            DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
            DirectCast(obs.value(0), CD).displayName = "Decreased tolerance"


            _procPerformed = New POCD_MT000040UV02Entry
            _procPerformed = GetProcedurePerformed(_dtDistinct, patientComponent)


            obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            obs.entryRelationship(0).typeCode = ActRelationshipType.CAUS
            obs.entryRelationship(0).inversionIndSpecified = True
            obs.entryRelationship(0).inversionInd = True
            obs.entryRelationship(0).Item = New POCD_MT000040UV02Procedure



            obs.entryRelationship(0).Item = _procPerformed.Item




        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry
    End Function
    Private Function GetProcedurePerformed(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer)
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim Proc As POCD_MT000040UV02Procedure = Nothing
        Dim obsentry As POCD_MT000040UV02Observation = Nothing
        Dim playEntity As POCD_MT000040UV02PlayingEntity = Nothing
        Dim dv As New DataView
        Try


            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Procedure()
            Proc = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''
            ' obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            Proc.classCode = ActClassProcedure.PROC

            'Dim _valueset As String = ""
            'Dim _codedescription As String = ""
            'If _valueset <> "" Then
            '    If _valueset = Convert.ToString(_dtDistinct.Rows(patientComponent)("SDTCValueSet")) AndAlso _codedescription = Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeDescription")) Then
            '        Exit Function
            '    End If
            'End If
           
            '_valueset = Convert.ToString(_dtDistinct.Rows(patientComponent)("SDTCValueSet"))
            '_codedescription = Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeDescription"))

            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()

                Dim _ValusetOID As String = String.Empty
                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    Proc.negationIndSpecified = True
                    Proc.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Procedure Order" Then
                Proc.moodCode = x_DocumentProcedureMood.RQO
                Proc.moodCodespecified = True
                Proc.templateId = New II(1) {}
                Proc.templateId(0) = New II()
                Proc.templateId(0).root = "2.16.840.1.113883.10.20.22.4.41"

                Proc.templateId(0).extension = _DateExtension22
                Proc.templateId(0).assigningAuthorityName = Nothing

                Proc.templateId(1) = New II()

                Proc.templateId(1).root = "2.16.840.1.113883.10.20.24.3.63"


                Proc.templateId(1).extension = _DateExtension2016
                Proc.templateId(1).assigningAuthorityName = Nothing
            Else
                Proc.moodCode = x_DocumentProcedureMood.EVN
                Proc.moodCodespecified = True
                Proc.templateId = New II(1) {}
                Proc.templateId(0) = New II()
                Proc.templateId(0).root = "2.16.840.1.113883.10.20.22.4.14"

                Proc.templateId(0).extension = _DateExtension22
                Proc.templateId(0).assigningAuthorityName = Nothing

                Proc.templateId(1) = New II()

                Proc.templateId(1).root = "2.16.840.1.113883.10.20.24.3.64"
                If ReportingYear = "2019" Then
                    Proc.templateId(1).extension = _Version2019
                Else
                    Proc.templateId(1).extension = _DateExtension2016
                End If



                Proc.templateId(1).assigningAuthorityName = Nothing
            End If


            Proc.id = New II(0) {}
            Proc.id(0) = New II()
            Proc.id(0).root = Guid.NewGuid.ToString()
            Proc.id(0).extension = Nothing
            Proc.id(0).assigningAuthorityName = Nothing

            Proc.code = New CD()

            Dim _code As Integer = 0
            dv = dtQRDA1Data.Copy().DefaultView

            dv.RowFilter = "TransactionID = '" & _dtDistinct.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtDistinct.Rows(patientComponent)("Category") & "' "
            Proc.code = New CD()
            DirectCast(Proc.code, CD).translation = New CD(dv.Count) {}


            If Not IsNothing(dv) AndAlso dv.Count > 0 Then
                For i As Integer = 0 To dv.Count - 1

                    '  If dv.ToTable.Rows(i)("CodeType") = "CPT" Or dv.ToTable.Rows(i)("CodeType") = "CPT/Snomed" Or dv.ToTable.Rows(i)("CodeType") = "SnoMed" Then



                    If _code = 0 Then
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then

                            Proc.code.code = dv.ToTable.Rows(i)("sCPTCode")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Proc.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Proc.code.valueSet = Nothing
                            End If


                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                Proc.code.codeSystem = CodeSystem.CPT
                            Else
                                Proc.code.codeSystem = CodeSystem.HCPCS

                            End If


                            Proc.code.codeSystemName = Nothing
                            Proc.code.codeSystemVersion = Nothing
                            Proc.code.displayName = Nothing
                            _code = _code + 1
                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                                DirectCast(Proc.code, CD).translation(i) = New CD()
                                DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Proc.code, CD).translation(i).displayName = Nothing
                                ''_code = _code + 1
                            End If

                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                            Proc.code.code = dv.ToTable.Rows(i)("sConceptID")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Proc.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Proc.code.valueSet = Nothing
                            End If

                            Proc.code.codeSystem = CodeSystem.SNOMED_CT

                            Proc.code.codeSystemName = Nothing
                            Proc.code.codeSystemVersion = Nothing
                            Proc.code.displayName = Nothing
                            _code = _code + 1

                        ElseIf dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then

                            Proc.code.code = dv.ToTable.Rows(i)("sICD9Code")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Proc.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Proc.code.valueSet = Nothing
                            End If

                            Proc.code.codeSystem = CodeSystem.ICD9

                            Proc.code.codeSystemName = Nothing
                            Proc.code.codeSystemVersion = Nothing
                            Proc.code.displayName = Nothing
                            _code = _code + 1
                        ElseIf dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then

                            Proc.code.code = dv.ToTable.Rows(i)("sICD10Code")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Proc.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Proc.code.valueSet = Nothing
                            End If

                            Proc.code.codeSystem = CodeSystem.ICD10

                            Proc.code.codeSystemName = Nothing
                            Proc.code.codeSystemVersion = Nothing
                            Proc.code.displayName = Nothing
                            _code = _code + 1
                        Else
                            Proc.code.nullFlavor = "NA"
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                Proc.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                Proc.code.valueSet = Nothing
                            End If
                            Proc.code.code = Nothing
                            Proc.code.codeSystem = Nothing
                            Proc.code.codeSystemName = Nothing
                            Proc.code.codeSystemVersion = Nothing
                            Proc.code.displayName = Nothing
                        End If
                    Else
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then
                            DirectCast(Proc.code, CD).translation(i) = New CD()
                            DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sCPTCode")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                            End If
                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.CPT
                            Else

                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.HCPCS

                            End If

                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                                DirectCast(Proc.code, CD).translation(i) = New CD()
                                DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                Else
                                    DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                                End If
                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Proc.code, CD).translation(i).displayName = Nothing
                            End If
                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then
                                DirectCast(Proc.code, CD).translation(i) = New CD()
                                DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD9Code")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.ICD9
                                DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Proc.code, CD).translation(i).displayName = Nothing
                            End If
                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then
                                DirectCast(Proc.code, CD).translation(i) = New CD()
                                DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD10Code")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.ICD10
                                DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(Proc.code, CD).translation(i).displayName = Nothing
                            End If

                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                            DirectCast(Proc.code, CD).translation(i) = New CD()
                            DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                            End If
                            DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                        ElseIf dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then
                            DirectCast(Proc.code, CD).translation(i) = New CD()
                            DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD9Code")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                            '  End If
                            DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.ICD9
                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                        ElseIf dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then
                            DirectCast(Proc.code, CD).translation(i) = New CD()
                            DirectCast(Proc.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD10Code")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(Proc.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(Proc.code, CD).translation(i).valueSet = Nothing
                            'End If
                            DirectCast(Proc.code, CD).translation(i).codeSystem = CodeSystem.ICD10
                            DirectCast(Proc.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(Proc.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(Proc.code, CD).translation(i).displayName = Nothing

                    End If
                    End If

                    ' End If

                Next


            End If




            Proc.code.originalText = New ED()
            Proc.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Proc.code.originalText.language = Nothing
            ''



            'DirectCast(Proc.code, CD).translation(1) = New CD()

            'If _dtQRDA1Data.Rows(patientComponent)(Col_QRDA1_CVX) IsNot Nothing AndAlso Convert.ToString(_dtQRDA1Data.Rows(patientComponent)(Col_QRDA1_CVX)) <> "" Then
            '    DirectCast(Proc.code, CD).translation(1).code = _dtQRDA1Data.Rows(patientComponent)(Col_QRDA1_CVX)
            'Else
            '    DirectCast(Proc.code, CD).translation(1).code = Nothing
            'End If
            'DirectCast(Proc.code, CD).translation(1).codeSystem = CodeSystem.CVXCode
            'DirectCast(Proc.code, CD).translation(1).codeSystemName = Nothing
            'DirectCast(Proc.code, CD).translation(1).codeSystemVersion = Nothing
            'DirectCast(Proc.code, CD).translation(1).displayName = Nothing
            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Procedure Order" Then
                Proc.statusCode = New CS()
                Proc.statusCode.code = "active"
                Proc.statusCode.codeSystem = Nothing
                Proc.statusCode.codeSystemName = Nothing
                Proc.statusCode.codeSystemVersion = Nothing
                Proc.statusCode.displayName = Nothing
            Else
                Proc.statusCode = New CS()
                Proc.statusCode.code = "completed"
                Proc.statusCode.codeSystem = Nothing
                Proc.statusCode.codeSystemName = Nothing
                Proc.statusCode.codeSystemVersion = Nothing
                Proc.statusCode.displayName = Nothing
            End If


            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Procedure Order" Then
                Proc.author = New POCD_MT000040UV02Author(0) {}
                Proc.author(0) = New POCD_MT000040UV02Author
                Proc.author(0).typeCode = Nothing
                Proc.author(0).typeCodeSpecified = False
                Proc.author(0).contextControlCode = Nothing
                Proc.author(0).templateId = New II(1) {}
                Proc.author(0).templateId(0) = New II()
                Proc.author(0).templateId(0).root = "2.16.840.1.113883.10.20.22.4.119"
                Proc.author(0).templateId(0).extension = Nothing
                Proc.author(0).templateId(0).assigningAuthorityName = Nothing
                Proc.author(0).time = New TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    Proc.author(0).time.value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    Proc.author(0).time.value = Nothing
                    Proc.author(0).time.nullFlavor = Nothing
                End If
                Proc.author(0).assignedAuthor = New POCD_MT000040UV02AssignedAuthor
                Proc.author(0).assignedAuthor.id = New II(0) {}

                Proc.author(0).assignedAuthor.id(0) = New II()
                ' Proc.author(0).assignedAuthor.id(0).nullFlavor = "NA"
                Proc.author(0).assignedAuthor.classCode = Nothing
                Proc.author(0).assignedAuthor.classCodeSpecified = False
                Proc.author(0).assignedAuthor.id(0).root = Guid.NewGuid.ToString()
                Proc.author(0).assignedAuthor.id(0).extension = Nothing
                Proc.author(0).assignedAuthor.id(0).assigningAuthorityName = Nothing
            Else
                Proc.effectiveTime = New IVL_TS()

                Proc.effectiveTime.value = Nothing
                DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
                DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
                DirectCast(Proc.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
                DirectCast(Proc.effectiveTime, IVL_TS).Items = New QTY(1) {}
                DirectCast(Proc.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
                End If

                DirectCast(Proc.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                    DirectCast(DirectCast(Proc.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
                End If

            End If

            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                Proc.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                Proc.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                Proc.entryRelationship(0).typeCode = ActRelationshipType.RSON
                Proc.entryRelationship(0).Item = New POCD_MT000040UV02Observation



                Proc.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)


                ''  Dim Procobs As POCD_MT000040UV02Observation = Nothing
                'Proc.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                'Proc.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                ' ''Reason of refusal

                'Proc.entryRelationship(0).typeCode = ActRelationshipType.RSON
                'Proc.entryRelationship(0).Item = New POCD_MT000040UV02Observation
                'Dim ob1 As POCD_MT000040UV02Observation
                'obsentry = DirectCast(Proc.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                'GetProcedurePerformedNotObservation(_dtDistinct, patientComponent, ob1)
                'Proc.entryRelationship(0).Item = ob1
                'obsentry.classCode = ActClassObservation.OBS
                'obsentry.moodCode = x_ActMoodDocumentObservation.EVN
                'obsentry.templateId = New II(0) {}
                'obsentry.templateId(0) = New II()
                'obsentry.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
                ''obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
                'obsentry.templateId(0).extension = _DateExtension24
                'obsentry.templateId(0).assigningAuthorityName = Nothing


                'obsentry.id = New II(0) {}
                'obsentry.id(0) = New II()
                'obsentry.id(0).root = Guid.NewGuid.ToString()
                'obsentry.id(0).extension = Nothing
                'obsentry.id(0).assigningAuthorityName = Nothing


                'obsentry.code = New CD()
                'obsentry.code.code = "77301-0"
                'obsentry.code.codeSystem = CodeSystem.LOINC
                'obsentry.code.codeSystemName = "LOINC"
                'obsentry.code.displayName = "reason"
                'obsentry.code.codeSystemVersion = Nothing

                'obsentry.statusCode = New CS()
                'obsentry.statusCode.code = "completed"
                'obsentry.statusCode.codeSystem = Nothing
                'obsentry.statusCode.codeSystemName = Nothing
                'obsentry.statusCode.codeSystemVersion = Nothing
                'obsentry.statusCode.displayName = Nothing


                'obsentry.effectiveTime = New IVL_TS()

                'obsentry.effectiveTime.value = Nothing
                'DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
                'DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
                'DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
                'DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
                'DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
                'If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
                'Else
                '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
                'End If

                'DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()

                'DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                'DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

                'obsentry.value = New ANY(0) {}
                'obsentry.value(0) = New CD()

                'If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                '    DirectCast(obsentry.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                '    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                '        DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                '    Else
                '        DirectCast(obsentry.value(0), CD).valueSet = Nothing
                '    End If
                '    DirectCast(obsentry.value(0), CD).valueSetVersion = Nothing
                'Else
                '    DirectCast(obsentry.value(0), CD).code = Nothing
                '    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                '        DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                '    Else
                '        DirectCast(obsentry.value(0), CD).valueSet = Nothing
                '    End If
                'End If
                'DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                'DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
                'DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
                'DirectCast(obsentry.value(0), CD).displayName = Nothing

                'If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then

                '    DirectCast(obsentry.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")

                '    DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                '    DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
                '    DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
                '    DirectCast(obsentry.value(0), CD).displayName = Nothing
                '    DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                'Else
                '    DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
                '    DirectCast(obsentry.value(0), CD).code = Nothing
                '    DirectCast(obsentry.value(0), CD).codeSystem = Nothing
                '    DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
                '    DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
                '    DirectCast(obsentry.value(0), CD).displayName = Nothing
                'End If


            End If
        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(Proc) Then
                Proc = Nothing
            End If
            If Not IsNothing(obsentry) Then
                obsentry = Nothing
            End If
        End Try
        Return _entry
    End Function
    Private Function GetInterventionPerformed(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim dv As New DataView
        Try

            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Act()
            act = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''

            '  obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            ''Added temporary for finding of hypertension-cms22 hypertension in results intervention performed:hypertension then no need to include negative reson but need to include reason section


            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()

                Dim _ValusetOID As String = String.Empty
                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    act.negationIndSpecified = True
                    act.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If




            act.templateId = New II(1) {}
            act.templateId(0) = New II()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Intervention Order" Then
                act.classCode = x_ActClassDocumentEntryAct.ACT
                act.moodCode = x_DocumentActMood.RQO
                act.moodCodeSpecified = True

                act.templateId(0).root = "2.16.840.1.113883.10.20.22.4.39"
            Else
                act.classCode = x_ActClassDocumentEntryAct.ACT
                act.moodCode = x_DocumentActMood.EVN
                act.moodCodeSpecified = True

                act.templateId(0).root = "2.16.840.1.113883.10.20.22.4.12"
            End If


            act.templateId(0).extension = _DateExtension22
            act.templateId(0).assigningAuthorityName = Nothing
            act.templateId(1) = New II()

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Intervention Order" Then
                act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.31"
            Else
                act.templateId(1).root = "2.16.840.1.113883.10.20.24.3.32"
            End If
            If ReportingYear = "2019" Then
                act.templateId(1).extension = _Version2019
            Else
                act.templateId(1).extension = _DateExtension2016
            End If


            act.templateId(1).assigningAuthorityName = Nothing

            act.id = New II(0) {}
            act.id(0) = New II()
            act.id(0).root = "1.3.6.1.4.1.115"
            act.id(0).extension = Guid.NewGuid.ToString()
            act.id(0).assigningAuthorityName = Nothing

            'act.code = New CD()
            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
            '    act.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
            '    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
            '        act.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        act.code.valueSet = Nothing
            '    End If
            '    act.code.valueSetVersion = Nothing
            'Else
            '    act.code.code = Nothing
            'End If


            'act.code.codeSystem = CodeSystem.SNOMED_CT
            'act.code.codeSystemName = "SNOMED_CT"
            'act.code.codeSystemVersion = Nothing
            'act.code.displayName = Nothing

            Dim _code As Integer = 0
            dv = dtQRDA1Data.Copy().DefaultView
            dv.RowFilter = "TransactionID = '" & _dtDistinct.Rows(patientComponent)("TransactionID") & "' and Category='" & _dtDistinct.Rows(patientComponent)("Category") & "' "
            act.code = New CD()
            DirectCast(act.code, CD).translation = New CD(dv.Count) {}


            If Not IsNothing(dv) AndAlso dv.Count > 0 Then
                For i As Integer = 0 To dv.Count - 1

                    '  If dv.ToTable.Rows(i)("CodeType") = "CPT" Or dv.ToTable.Rows(i)("CodeType") = "CPT/Snomed" Or dv.ToTable.Rows(i)("CodeType") = "SnoMed" Then
                    If _code = 0 Then
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then
                            dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)
                            act.code.code = dv.ToTable.Rows(i)("sCPTCode")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                act.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                act.code.valueSet = Nothing
                            End If
                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                act.code.codeSystem = CodeSystem.CPT
                            Else
                                act.code.codeSystem = CodeSystem.HCPCS

                            End If


                            act.code.codeSystemName = Nothing
                            act.code.codeSystemVersion = Nothing
                            act.code.displayName = Nothing
                            _code = _code + 1
                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                                DirectCast(act.code, CD).translation(i) = New CD()
                                DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(act.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(act.code, CD).translation(i).displayName = Nothing
                                ''_code = _code + 1
                            End If

                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then

                            act.code.code = dv.ToTable.Rows(i)("sConceptID")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                act.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                act.code.valueSet = Nothing
                            End If

                            act.code.codeSystem = CodeSystem.SNOMED_CT

                            act.code.codeSystemName = Nothing
                            act.code.codeSystemVersion = Nothing
                            act.code.displayName = Nothing
                            _code = _code + 1
                            ''CMS 22 intervention order ICD changes
                        ElseIf dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then

                            act.code.code = dv.ToTable.Rows(i)("sICD9Code")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                act.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                act.code.valueSet = Nothing
                            End If

                            act.code.codeSystem = CodeSystem.ICD9

                            act.code.codeSystemName = Nothing
                            act.code.codeSystemVersion = Nothing
                            act.code.displayName = Nothing
                            _code = _code + 1
                        ElseIf dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then

                            act.code.code = dv.ToTable.Rows(i)("sICD10Code")
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                act.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                act.code.valueSet = Nothing
                            End If

                            act.code.codeSystem = CodeSystem.ICD10

                            act.code.codeSystemName = Nothing
                            act.code.codeSystemVersion = Nothing
                            act.code.displayName = Nothing
                            _code = _code + 1

                        Else
                            act.code.nullFlavor = "NA"
                            If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                act.code.valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            Else
                                act.code.valueSet = Nothing
                            End If
                            act.code.code = Nothing
                            act.code.codeSystem = Nothing
                            act.code.codeSystemName = Nothing
                            act.code.codeSystemVersion = Nothing
                            act.code.displayName = Nothing
                        End If
                    Else
                        If dv.ToTable.Rows(i)("sCPTCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sCPTCode")) <> "" Then
                            DirectCast(act.code, CD).translation(i) = New CD()
                            DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sCPTCode")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(act.code, CD).translation(i).valueSet = Nothing
                            '  End If
                            If IsNumeric(dv.ToTable.Rows(i)("sCPTCode").ToString().Substring(0, 1)) Then
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.CPT
                            Else
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.HCPCS

                            End If



                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                                DirectCast(act.code, CD).translation(i) = New CD()
                                DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(act.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                                DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(act.code, CD).translation(i).displayName = Nothing
                            End If

                            If dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then
                                DirectCast(act.code, CD).translation(i) = New CD()
                                DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD9Code")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(act.code, CD).translation(i).valueSet = Nothing
                                ' End If
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.ICD9
                                DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(act.code, CD).translation(i).displayName = Nothing
                            End If
                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing

                            If dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then
                                DirectCast(act.code, CD).translation(i) = New CD()
                                DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD10Code")
                                'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                                '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                                'Else
                                DirectCast(act.code, CD).translation(i).valueSet = Nothing
                                '  End If
                                DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.ICD10
                                DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                                DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                                DirectCast(act.code, CD).translation(i).displayName = Nothing
                            End If
                        ElseIf dv.ToTable.Rows(i)("sConceptID") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sConceptID")) <> "" Then
                            DirectCast(act.code, CD).translation(i) = New CD()
                            DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sConceptID")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(act.code, CD).translation(i).valueSet = Nothing
                            '  End If
                            DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.SNOMED_CT
                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing
                        ElseIf dv.ToTable.Rows(i)("sICD9Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD9Code")) <> "" Then
                            DirectCast(act.code, CD).translation(i) = New CD()
                            DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD9Code")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(act.code, CD).translation(i).valueSet = Nothing
                            ' End If
                            DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.ICD9
                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing

                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing

                        ElseIf dv.ToTable.Rows(i)("sICD10Code") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("sICD10Code")) <> "" Then
                            DirectCast(act.code, CD).translation(i) = New CD()
                            DirectCast(act.code, CD).translation(i).code = dv.ToTable.Rows(i)("sICD10Code")
                            'If dv.ToTable.Rows(i)("SDTCValueSet") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(i)("SDTCValueSet")) <> "" Then
                            '    DirectCast(act.code, CD).translation(i).valueSet = dv.ToTable.Rows(i)("SDTCValueSet")
                            'Else
                            DirectCast(act.code, CD).translation(i).valueSet = Nothing
                            '  End If
                            DirectCast(act.code, CD).translation(i).codeSystem = CodeSystem.ICD10
                            DirectCast(act.code, CD).translation(i).codeSystemName = Nothing
                            DirectCast(act.code, CD).translation(i).codeSystemVersion = Nothing
                            DirectCast(act.code, CD).translation(i).displayName = Nothing

                    End If

                    End If

                    ' End If

                Next


            End If
            ''


            'DirectCast(act.code, CD).translation = New CD(0) {}
            'DirectCast(act.code, CD).translation(0) = New CD()

            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
            '    DirectCast(act.code, CD).translation(0).code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
            '    If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
            '        DirectCast(act.code, CD).translation(0).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        DirectCast(act.code, CD).translation(0).valueSet = Nothing
            '    End If
            '    DirectCast(act.code, CD).translation(0).valueSetVersion = Nothing
            'Else
            '    DirectCast(act.code, CD).translation(0).code = Nothing
            'End If
            'DirectCast(act.code, CD).translation(0).codeSystem = CodeSystem.SNOMED_CT
            'DirectCast(act.code, CD).translation(0).codeSystemName = "SNOMED_CT"
            'DirectCast(act.code, CD).translation(0).codeSystemVersion = Nothing
            'DirectCast(act.code, CD).translation(0).displayName = Nothing


            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Intervention Order" Then
                act.statusCode = New CS()
                act.statusCode.code = "active"
                act.statusCode.codeSystem = Nothing
                act.statusCode.codeSystemName = Nothing
                act.statusCode.codeSystemVersion = Nothing
                act.statusCode.displayName = Nothing


            Else
                act.statusCode = New CS()
                act.statusCode.code = "completed"
                act.statusCode.codeSystem = Nothing
                act.statusCode.codeSystemName = Nothing
                act.statusCode.codeSystemVersion = Nothing
                act.statusCode.displayName = Nothing


            End If

            act.effectiveTime = New IVL_TS()

            act.effectiveTime.value = Nothing
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            'DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(act.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(act.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            'DirectCast(act.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            'If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
            '    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            'Else
            '    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
            '    DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            'End If

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Intervention Order" Then
                act.author = New POCD_MT000040UV02Author(0) {}
                act.author(0) = New POCD_MT000040UV02Author
                act.author(0).templateId = New II(1) {}
                act.author(0).templateId(0) = New II()
                act.author(0).templateId(0).root = "2.16.840.1.113883.10.20.22.4.119"
                act.author(0).templateId(0).extension = Nothing
                act.author(0).templateId(0).assigningAuthorityName = Nothing
                act.author(0).time = New TS()
                If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                    act.author(0).time.value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
                Else
                    act.author(0).time.value = Nothing
                    act.author(0).time.nullFlavor = Nothing
                End If
                act.author(0).assignedAuthor = New POCD_MT000040UV02AssignedAuthor
                act.author(0).assignedAuthor.id = New II(0) {}

                act.author(0).assignedAuthor.id(0) = New II()
                ' act.author(0).assignedAuthor.id(0).nullFlavor = "NA"
                act.author(0).assignedAuthor.classCode = Nothing
                act.author(0).assignedAuthor.classCodeSpecified = False
                act.author(0).assignedAuthor.id(0).root = Guid.NewGuid.ToString()
                act.author(0).assignedAuthor.id(0).extension = Nothing
                act.author(0).assignedAuthor.id(0).assigningAuthorityName = Nothing

            End If
            '    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") = "" Then
            ' Exit Function
            '  End If
            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                act.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                act.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                act.entryRelationship(0).typeCode = ActRelationshipType.RSON
                act.entryRelationship(0).Item = New POCD_MT000040UV02Procedure

                act.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
            End If


        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(act) Then
                act = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetRiskCatAssessment(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim _ValusetOID As String = String.Empty
        Try

            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''

            '  obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodespecified = True


            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()


                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    obs.negationIndSpecified = True
                    obs.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If

            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.22.4.69"

            obs.templateId(0).extension = Nothing
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()
            obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.69"

            obs.templateId(1).extension = _DateExtension2016
            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing

                obs.code.codeSystem = CodeSystem.SNOMED_CT
                obs.code.codeSystemName = "SNOMED_CT"
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then

                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) '' "2.16.840.1.113883.3.526.3.1278" 
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing

                obs.code.codeSystem = CodeSystem.LOINC
                obs.code.codeSystemName = "LOINC"
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
            Else
                obs.code.nullFlavor = "NA"
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
                obs.code.codeSystemName = Nothing
                obs.code.codeSystem = Nothing
                obs.code.code = Nothing
            End If




            obs.code.originalText = New ED()

            ''

            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                obs.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                obs.code.originalText.Text = Nothing
            End If
            obs.code.originalText.language = Nothing



            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If




            'Risk Category Assessment: Adolescent Depression Screening	428171000124102.00	73831-0
            If _ValusetOID <> "" Then

                obs.value = New ANY(0) {}
                obs.value(0) = New CD()

                If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                    DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                Else
                    DirectCast(obs.value(0), CD).code = Nothing
                    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                End If
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing
            Else
                obs.value = New ANY(1) {}
                obs.value(0) = New CD()
                obs.value(0).nullFlavor = "UNK"
                DirectCast(obs.value(0), CD).code = Nothing
                DirectCast(obs.value(0), CD).codeSystem = Nothing
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing
            End If

            If _ValusetOID = "" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
                    obs.entryRelationship(0).Item = New POCD_MT000040UV02Procedure


                    obs.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
                End If
            End If



            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Risk Category Assessment not done" Then

            '    Dim obsentry As POCD_MT000040UV02Observation = Nothing
            '    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            '    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            '    ''Reason of refusal

            '    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
            '    obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            '    obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)

            '    obsentry.classCode = ActClassObservation.OBS
            '    obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            '    obsentry.templateId = New II(0) {}
            '    obsentry.templateId(0) = New II()
            '    obsentry.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
            '    'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            '    obsentry.templateId(0).extension = _DateExtension24
            '    obsentry.templateId(0).assigningAuthorityName = Nothing


            '    obsentry.id = New II(0) {}
            '    obsentry.id(0) = New II()
            '    obsentry.id(0).root = Guid.NewGuid.ToString()
            '    obsentry.id(0).extension = Nothing
            '    obsentry.id(0).assigningAuthorityName = Nothing


            '    obsentry.code = New CD()
            '    obsentry.code.code = "77301-0"
            '    obsentry.code.codeSystem = CodeSystem.LOINC
            '    obsentry.code.codeSystemName = "LOINC"
            '    obsentry.code.displayName = "reason"
            '    obsentry.code.codeSystemVersion = Nothing

            '    obsentry.statusCode = New CS()
            '    obsentry.statusCode.code = "completed"
            '    obsentry.statusCode.codeSystem = Nothing
            '    obsentry.statusCode.codeSystemName = Nothing
            '    obsentry.statusCode.codeSystemVersion = Nothing
            '    obsentry.statusCode.displayName = Nothing


            '    obsentry.effectiveTime = New IVL_TS()

            '    obsentry.effectiveTime.value = Nothing
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            '    If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            '    Else
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            '    End If

            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()

            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            '    obsentry.value = New ANY(0) {}
            '    obsentry.value(0) = New CD()
            '    If _dtDistinct.Rows(patientComponent)("sReasonCode") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonCode")) <> "" Then

            '        DirectCast(obsentry.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonCode")

            '        DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            '        DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '        DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
            '        DirectCast(obsentry.value(0), CD).code = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystem = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '    End If


            'End If
            'Dim dv As DataView
            'dv = _dtDistinct.DefaultView
            'dv.RowFilter = "TransactionDate = '" & _dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate) & "' and sReasonCode <> '' and Category= 'Risk Category Assessment not done'"
            '' and sReasonCode <> '' and _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = " & "' Risk Category Assessment not done'"" "

            'If dv.Count > 0 Then


            '    '   If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Risk Category Assessment not done" Then
            '    Dim obsentry As POCD_MT000040UV02Observation = Nothing
            '    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            '    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            '    ''Reason of refusal

            '    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
            '    obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            '    obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)

            '    obsentry.classCode = ActClassObservation.OBS
            '    obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            '    obsentry.templateId = New II(0) {}
            '    obsentry.templateId(0) = New II()
            '    obsentry.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
            '    'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            '    obsentry.templateId(0).extension = _DateExtension24
            '    obsentry.templateId(0).assigningAuthorityName = Nothing


            '    obsentry.id = New II(0) {}
            '    obsentry.id(0) = New II()
            '    obsentry.id(0).root = Guid.NewGuid.ToString()
            '    obsentry.id(0).extension = Nothing
            '    obsentry.id(0).assigningAuthorityName = Nothing


            '    obsentry.code = New CD()
            '    obsentry.code.code = "77301-0"
            '    obsentry.code.codeSystem = CodeSystem.LOINC
            '    obsentry.code.codeSystemName = "LOINC"
            '    obsentry.code.displayName = "reason"
            '    obsentry.code.codeSystemVersion = Nothing

            '    obsentry.statusCode = New CS()
            '    obsentry.statusCode.code = "completed"
            '    obsentry.statusCode.codeSystem = Nothing
            '    obsentry.statusCode.codeSystemName = Nothing
            '    obsentry.statusCode.codeSystemVersion = Nothing
            '    obsentry.statusCode.displayName = Nothing


            '    obsentry.effectiveTime = New IVL_TS()

            '    obsentry.effectiveTime.value = Nothing
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            '    If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            '    Else
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            '    End If

            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()

            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            '    obsentry.value = New ANY(0) {}
            '    obsentry.value(0) = New CD()
            '    If dv.ToTable.Rows(0)("sReasonCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("sReasonCode")) <> "" Then
            '        obs.negationIndSpecified = True
            '        obs.negationInd = True
            '        DirectCast(obsentry.value(0), CD).code = dv.ToTable.Rows(0)("sReasonCode")
            '        DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            '        DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '        DirectCast(obsentry.value(0), CD).valueSet = dv.ToTable.Rows(0)("sReasonValueSet")
            '    Else
            '        DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
            '        DirectCast(obsentry.value(0), CD).code = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystem = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '    End If
            'End If

            '  End If



        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
        End Try
        Return _entry

    End Function

    Private Function GetAssessmentPeroformed(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim _ValusetOID As String = String.Empty
        Try

            _entry = New POCD_MT000040UV02Entry
            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Observation()
            obs = DirectCast(_entry, POCD_MT000040UV02Entry).Item
            ''

            '  obs = DirectCast(section.entry(patientComponent).Item, POCD_MT000040UV02Observation)
            obs.classCode = ActClassObservation.OBS
            obs.moodCode = x_ActMoodDocumentObservation.EVN
            obs.moodCodeSpecified = True


            If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                Dim _cls As New gloPatientRegDBLayer()


                _ValusetOID = _cls.GetAttributeList(Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")))

                If _ValusetOID = "" Then
                    obs.negationIndSpecified = True
                    obs.negationInd = True
                End If

                _cls.Dispose()
                _cls = Nothing

            End If

            obs.templateId = New II(1) {}
            obs.templateId(0) = New II()
            obs.templateId(0).root = "2.16.840.1.113883.10.20.24.3.144"

            obs.templateId(0).extension = Nothing
            obs.templateId(0).assigningAuthorityName = Nothing
            obs.templateId(1) = New II()
            obs.templateId(1).root = "2.16.840.1.113883.10.20.24.3.144"

            If (_IsFromoldVersion = True) Then
                obs.templateId(1).extension = Nothing ''for reporting year 2017 or prior dateextension is not require 
            Else
                obs.templateId(1).extension = _DateExtension2016_1  ''for reporting year 2018  dateextension  require 
            End If

            obs.templateId(1).assigningAuthorityName = Nothing

            obs.id = New II(0) {}
            obs.id(0) = New II()
            obs.id(0).root = Guid.NewGuid.ToString()
            obs.id(0).extension = Nothing
            obs.id(0).assigningAuthorityName = Nothing

            obs.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing

                obs.code.codeSystem = CodeSystem.SNOMED_CT
                obs.code.codeSystemName = "SNOMED_CT"
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then

                obs.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) '' "2.16.840.1.113883.3.526.3.1278" 
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing

                obs.code.codeSystem = CodeSystem.LOINC
                obs.code.codeSystemName = "LOINC"
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
            Else
                obs.code.nullFlavor = "NA"
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obs.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obs.code.valueSet = Nothing
                End If
                obs.code.valueSetVersion = Nothing
                obs.code.codeSystemVersion = Nothing
                obs.code.displayName = Nothing
                obs.code.codeSystemName = Nothing
                obs.code.codeSystem = Nothing
                obs.code.code = Nothing
            End If




            obs.code.originalText = New ED()

            ''

            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                obs.code.originalText.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                obs.code.originalText.Text = Nothing
            End If
            obs.code.originalText.language = Nothing



            obs.statusCode = New CS()
            obs.statusCode.code = "completed"
            obs.statusCode.codeSystem = Nothing
            obs.statusCode.codeSystemName = Nothing
            obs.statusCode.codeSystemVersion = Nothing
            obs.statusCode.displayName = Nothing


            obs.effectiveTime = New IVL_TS()

            obs.effectiveTime.value = Nothing
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obs.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obs.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obs.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obs.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obs.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If




            'Risk Category Assessment: Adolescent Depression Screening	428171000124102.00	73831-0
            If _ValusetOID <> "" Then

                obs.value = New ANY(0) {}
                obs.value(0) = New CD()

                If _dtDistinct.Rows(patientComponent)("sReasonConceptID") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" Then
                    DirectCast(obs.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonConceptID")
                    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                    DirectCast(obs.value(0), CD).valueSetVersion = Nothing
                Else
                    DirectCast(obs.value(0), CD).code = Nothing
                    If _dtDistinct.Rows(patientComponent)("sReasonValueSet") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonValueSet")) <> "" Then
                        DirectCast(obs.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)("sReasonValueSet")
                    Else
                        DirectCast(obs.value(0), CD).valueSet = Nothing
                    End If
                End If
                DirectCast(obs.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing
            ElseIf Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeValue")) <> "" Then
                obs.value = New ANY(1) {}
                obs.value(0) = New PQ()
                If _dtDistinct.Rows(patientComponent)("CodeValue") <> "" Then
                    DirectCast(obs.value(0), PQ).value = _dtDistinct.Rows(patientComponent)("CodeValue")
                    DirectCast(obs.value(0), PQ).unit = "%"
                Else
                    DirectCast(obs.value(0), PQ).nullFlavor = "UNK"
                    DirectCast(obs.value(0), PQ).nullFlavorSpecified = True
                End If
            Else

                obs.value = New ANY(1) {}
                obs.value(0) = New CD()
                obs.value(0).nullFlavor = "UNK"
                DirectCast(obs.value(0), CD).code = Nothing
                DirectCast(obs.value(0), CD).codeSystem = Nothing
                DirectCast(obs.value(0), CD).codeSystemName = Nothing
                DirectCast(obs.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obs.value(0), CD).displayName = Nothing
            End If


            obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            obs.entryRelationship(0).typeCode = ActRelationshipType.REFR
            obs.entryRelationship(0).Item = New POCD_MT000040UV02Procedure
            obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            Dim obsentry As New POCD_MT000040UV02Observation
            obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)

            obsentry.classCode = ActClassObservation.OBS
            obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            obsentry.templateId = New II(1) {}
            obsentry.templateId(0) = New II()
            obsentry.templateId(0).root = "2.16.840.1.113883.10.20.22.4.2"
            'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            obsentry.templateId(0).extension = _DateExtension2015
            obsentry.templateId(0).assigningAuthorityName = Nothing


            obsentry.templateId(1) = New II()
            obsentry.templateId(1).root = "2.16.840.1.113883.10.20.24.3.87"
            'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            obsentry.templateId(1).extension = _DateExtension2016
            obsentry.templateId(1).assigningAuthorityName = Nothing



            obsentry.id = New II(0) {}
            obsentry.id(0) = New II()
            obsentry.id(0).root = Guid.NewGuid.ToString()
            obsentry.id(0).extension = Nothing
            obsentry.id(0).assigningAuthorityName = Nothing
            obsentry.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                obsentry.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obsentry.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obsentry.code.valueSet = Nothing
                End If
                obsentry.code.valueSetVersion = Nothing

                obsentry.code.codeSystem = CodeSystem.SNOMED_CT
                obsentry.code.codeSystemName = "SNOMED_CT"
                obsentry.code.codeSystemVersion = Nothing
                obsentry.code.displayName = Nothing
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)) <> "" Then

                obsentry.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_LOINC)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obsentry.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) '' "2.16.840.1.113883.3.526.3.1278" 
                Else
                    obsentry.code.valueSet = Nothing
                End If
                obsentry.code.valueSetVersion = Nothing

                obsentry.code.codeSystem = CodeSystem.LOINC
                obsentry.code.codeSystemName = "LOINC"
                obsentry.code.codeSystemVersion = Nothing
                obsentry.code.displayName = Nothing
            Else
                obsentry.code.nullFlavor = "NA"
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    obsentry.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    obsentry.code.valueSet = Nothing
                End If
                obsentry.code.valueSetVersion = Nothing
                obsentry.code.codeSystemVersion = Nothing
                obsentry.code.displayName = Nothing
                obsentry.code.codeSystemName = Nothing
                obsentry.code.codeSystem = Nothing
                obsentry.code.code = Nothing
            End If
            

            obsentry.statusCode = New CS()
            obsentry.statusCode.code = "completed"
            obsentry.statusCode.codeSystem = Nothing
            obsentry.statusCode.codeSystemName = Nothing
            obsentry.statusCode.codeSystemVersion = Nothing
            obsentry.statusCode.displayName = Nothing


            obsentry.effectiveTime = New IVL_TS()

            obsentry.effectiveTime.value = Nothing
            DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            Else
                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            Else

                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing
            End If

           
            If _dtDistinct.Rows(patientComponent)("sReasonCode") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonCode")) <> "" Then
                obsentry.value = New ANY(0) {}
                obsentry.value(0) = New CD()
                DirectCast(obsentry.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonCode")

                DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
                DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
                DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obsentry.value(0), CD).displayName = Nothing
                DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            ElseIf Convert.ToString(_dtDistinct.Rows(patientComponent)("CodeValue")) <> "" Then
                obsentry.value = New ANY(0) {}
                obsentry.value(0) = New PQ()
                'obsentry.value = New ANY(1) {}
                'obsentry.value(0) = New PQ()
                If _dtDistinct.Rows(patientComponent)("CodeValue") <> "" Then
                    DirectCast(obsentry.value(0), PQ).value = _dtDistinct.Rows(patientComponent)("CodeValue")
                    DirectCast(obsentry.value(0), PQ).unit = "%"
                Else
                    DirectCast(obsentry.value(0), PQ).nullFlavor = "UNK"
                    DirectCast(obsentry.value(0), PQ).nullFlavorSpecified = True
                End If
            Else
                obsentry.value = New ANY(0) {}
                obsentry.value(0) = New CD()
                DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
                DirectCast(obsentry.value(0), CD).code = Nothing
                DirectCast(obsentry.value(0), CD).codeSystem = Nothing
                DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
                DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
                DirectCast(obsentry.value(0), CD).displayName = Nothing
            End If



            If _ValusetOID = "" Then
                If Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "" AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonConceptID")) <> "UNK" Then
                    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
                    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
                    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
                    obs.entryRelationship(0).Item = New POCD_MT000040UV02Procedure


                    obs.entryRelationship(0).Item = GetProcedurePerformedNotObservation(_dtDistinct, patientComponent)
                End If
            End If



            'If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Risk Category Assessment not done" Then

            '    Dim obsentry As POCD_MT000040UV02Observation = Nothing
            '    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            '    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            '    ''Reason of refusal

            '    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
            '    obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            '    obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)

            '    obsentry.classCode = ActClassObservation.OBS
            '    obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            '    obsentry.templateId = New II(0) {}
            '    obsentry.templateId(0) = New II()
            '    obsentry.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
            '    'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            '    obsentry.templateId(0).extension = _DateExtension24
            '    obsentry.templateId(0).assigningAuthorityName = Nothing


            '    obsentry.id = New II(0) {}
            '    obsentry.id(0) = New II()
            '    obsentry.id(0).root = Guid.NewGuid.ToString()
            '    obsentry.id(0).extension = Nothing
            '    obsentry.id(0).assigningAuthorityName = Nothing


            '    obsentry.code = New CD()
            '    obsentry.code.code = "77301-0"
            '    obsentry.code.codeSystem = CodeSystem.LOINC
            '    obsentry.code.codeSystemName = "LOINC"
            '    obsentry.code.displayName = "reason"
            '    obsentry.code.codeSystemVersion = Nothing

            '    obsentry.statusCode = New CS()
            '    obsentry.statusCode.code = "completed"
            '    obsentry.statusCode.codeSystem = Nothing
            '    obsentry.statusCode.codeSystemName = Nothing
            '    obsentry.statusCode.codeSystemVersion = Nothing
            '    obsentry.statusCode.displayName = Nothing


            '    obsentry.effectiveTime = New IVL_TS()

            '    obsentry.effectiveTime.value = Nothing
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            '    If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            '    Else
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            '    End If

            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()

            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            '    obsentry.value = New ANY(0) {}
            '    obsentry.value(0) = New CD()
            '    If _dtDistinct.Rows(patientComponent)("sReasonCode") IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)("sReasonCode")) <> "" Then

            '        DirectCast(obsentry.value(0), CD).code = _dtDistinct.Rows(patientComponent)("sReasonCode")

            '        DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            '        DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '        DirectCast(obsentry.value(0), CD).valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
            '    Else
            '        DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
            '        DirectCast(obsentry.value(0), CD).code = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystem = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '    End If


            'End If
            'Dim dv As DataView
            'dv = _dtDistinct.DefaultView
            'dv.RowFilter = "TransactionDate = '" & _dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate) & "' and sReasonCode <> '' and Category= 'Risk Category Assessment not done'"
            '' and sReasonCode <> '' and _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = " & "' Risk Category Assessment not done'"" "

            'If dv.Count > 0 Then


            '    '   If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Risk Category Assessment not done" Then
            '    Dim obsentry As POCD_MT000040UV02Observation = Nothing
            '    obs.entryRelationship = New POCD_MT000040UV02EntryRelationship(0) {}
            '    obs.entryRelationship(0) = New POCD_MT000040UV02EntryRelationship
            '    ''Reason of refusal

            '    obs.entryRelationship(0).typeCode = ActRelationshipType.RSON
            '    obs.entryRelationship(0).Item = New POCD_MT000040UV02Observation
            '    obsentry = DirectCast(obs.entryRelationship(0).Item, POCD_MT000040UV02Observation)

            '    obsentry.classCode = ActClassObservation.OBS
            '    obsentry.moodCode = x_ActMoodDocumentObservation.EVN
            '    obsentry.templateId = New II(0) {}
            '    obsentry.templateId(0) = New II()
            '    obsentry.templateId(0).root = "2.16.840.1.113883.10.20.24.3.88"
            '    'obs.templateId(0).extension = "2.16.840.1.113883.3.117.1.7.1.93"
            '    obsentry.templateId(0).extension = _DateExtension24
            '    obsentry.templateId(0).assigningAuthorityName = Nothing


            '    obsentry.id = New II(0) {}
            '    obsentry.id(0) = New II()
            '    obsentry.id(0).root = Guid.NewGuid.ToString()
            '    obsentry.id(0).extension = Nothing
            '    obsentry.id(0).assigningAuthorityName = Nothing


            '    obsentry.code = New CD()
            '    obsentry.code.code = "77301-0"
            '    obsentry.code.codeSystem = CodeSystem.LOINC
            '    obsentry.code.codeSystemName = "LOINC"
            '    obsentry.code.displayName = "reason"
            '    obsentry.code.codeSystemVersion = Nothing

            '    obsentry.statusCode = New CS()
            '    obsentry.statusCode.code = "completed"
            '    obsentry.statusCode.codeSystem = Nothing
            '    obsentry.statusCode.codeSystemName = Nothing
            '    obsentry.statusCode.codeSystemVersion = Nothing
            '    obsentry.statusCode.displayName = Nothing


            '    obsentry.effectiveTime = New IVL_TS()

            '    obsentry.effectiveTime.value = Nothing
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            '    DirectCast(obsentry.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items = New QTY(1) {}
            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            '    If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddhhmmss")
            '    Else
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
            '        DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            '    End If

            '    DirectCast(obsentry.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()

            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
            '    DirectCast(DirectCast(obsentry.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            '    obsentry.value = New ANY(0) {}
            '    obsentry.value(0) = New CD()
            '    If dv.ToTable.Rows(0)("sReasonCode") IsNot Nothing AndAlso Convert.ToString(dv.ToTable.Rows(0)("sReasonCode")) <> "" Then
            '        obs.negationIndSpecified = True
            '        obs.negationInd = True
            '        DirectCast(obsentry.value(0), CD).code = dv.ToTable.Rows(0)("sReasonCode")
            '        DirectCast(obsentry.value(0), CD).codeSystem = CodeSystem.SNOMED_CT
            '        DirectCast(obsentry.value(0), CD).codeSystemName = "SNOMED_CT"
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '        DirectCast(obsentry.value(0), CD).valueSet = dv.ToTable.Rows(0)("sReasonValueSet")
            '    Else
            '        DirectCast(obsentry.value(0), CD).nullFlavor = "OTH"
            '        DirectCast(obsentry.value(0), CD).code = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystem = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemName = Nothing
            '        DirectCast(obsentry.value(0), CD).codeSystemVersion = Nothing
            '        DirectCast(obsentry.value(0), CD).displayName = Nothing
            '    End If
            'End If

            '  End If



        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(obs) Then
                obs = Nothing
            End If
        End Try
        Return _entry

    End Function
    Private Function GetCommunicationact(ByVal _dtDistinct As DataTable, ByVal patientComponent As Integer) As POCD_MT000040UV02Entry
        Dim _entry As POCD_MT000040UV02Entry = Nothing
        Dim act As POCD_MT000040UV02Act = Nothing

        Try


            _entry = New POCD_MT000040UV02Entry

            DirectCast(_entry, POCD_MT000040UV02Entry).Item = New POCD_MT000040UV02Act()
            act = DirectCast(_entry, POCD_MT000040UV02Entry).Item


            act.classCode = x_ActClassDocumentEntryAct.ACT
            act.moodCode = x_DocumentActMood.EVN
            act.moodCodeSpecified = True



            act.templateId = New II(0) {}
            act.templateId(0) = New II()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Patient to Provider" Then
                act.templateId(0).root = "2.16.840.1.113883.10.20.24.3.2"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Provider to Patient" Then
                act.templateId(0).root = "2.16.840.1.113883.10.20.24.3.3"
            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Provider to Provider" Then
                act.templateId(0).root = "2.16.840.1.113883.10.20.24.3.4"
            End If


            act.templateId(0).extension = _DateExtension2016
            act.templateId(0).assigningAuthorityName = Nothing

            act.id = New II(0) {}
            act.id(0) = New II()
            act.id(0).root = Guid.NewGuid.ToString()
            act.id(0).extension = Nothing
            act.id(0).assigningAuthorityName = Nothing

            act.code = New CD()
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)) <> "" Then
                act.code.code = _dtDistinct.Rows(patientComponent)(Col_QRDA1_ConceptID)
                If _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)) <> "" Then
                    act.code.valueSet = _dtDistinct.Rows(patientComponent)(Col_QRDA1_SDTCValueSet)
                Else
                    act.code.valueSet = Nothing
                End If
                act.code.valueSetVersion = Nothing

            Else
                act.code.code = Nothing
            End If
            act.code.displayName = Nothing
            act.code.codeSystem = CodeSystem.SNOMED_CT
            act.code.codeSystemName = "SNOMED_CT"
            act.code.codeSystemVersion = Nothing

            act.text = New ED()

            ''
            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription) IsNot Nothing AndAlso Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)) <> "" Then
                act.text.Text = New String() {_dtDistinct.Rows(patientComponent)(Col_QRDA1_CodeDescription)}
            Else
                act.text.Text = Nothing
            End If
            act.text.language = Nothing
            act.text.mediaType = Nothing

            act.statusCode = New CS()
            act.statusCode.code = "completed"
            act.statusCode.codeSystem = Nothing
            act.statusCode.codeSystemName = Nothing
            act.statusCode.codeSystemVersion = Nothing
            act.statusCode.displayName = Nothing

            act.effectiveTime = New IVL_TS()
            act.effectiveTime.value = Nothing
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName = New ItemsChoiceType2(1) {}
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(0) = ItemsChoiceType2.low
            DirectCast(act.effectiveTime, IVL_TS).ItemsElementName(1) = ItemsChoiceType2.high
            DirectCast(act.effectiveTime, IVL_TS).Items = New QTY(1) {}
            DirectCast(act.effectiveTime, IVL_TS).Items(0) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).value = Nothing
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(0), IVXB_TS).nullFlavor = Nothing
            End If

            DirectCast(act.effectiveTime, IVL_TS).Items(1) = New IVXB_TS()
            If Convert.ToString(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate)) <> "" Then
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Format(_dtDistinct.Rows(patientComponent)(Col_QRDA1_TransactionDate), "yyyyMMddHHmmss")
            Else
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).value = Nothing
                DirectCast(DirectCast(act.effectiveTime, IVL_TS).Items(1), IVXB_TS).nullFlavor = Nothing

            End If

            If _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Patient to Provider" Then
                act.participant = New POCD_MT000040UV02Participant3(1) {}
                act.participant(0) = New POCD_MT000040UV02Participant3
                act.participant(0).typeCode = ParticipationType.AUT
                act.participant(0).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(0).participantRole.classCode = RoleClassRoot.PAT
                act.participant(0).participantRole.addr = Nothing
                act.participant(0).participantRole.code = New CE()
                act.participant(0).participantRole.code.code = "116154003"
                act.participant(0).participantRole.code.displayName = "Patient"

                act.participant(1) = New POCD_MT000040UV02Participant3
                act.participant(1).typeCode = ParticipationType.IRCP
                act.participant(1).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(1).participantRole.classCode = RoleClassRoot.ASSIGNED
                act.participant(1).participantRole.addr = Nothing
                act.participant(1).participantRole.code = New CE()
                act.participant(1).participantRole.code.code = "158965000"
                act.participant(1).participantRole.code.displayName = "Medical Practitioner"

            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Provider to Patient" Then
                act.participant = New POCD_MT000040UV02Participant3(1) {}
                act.participant(0) = New POCD_MT000040UV02Participant3
                act.participant(0).typeCode = ParticipationType.AUT
                act.participant(0).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(0).participantRole.classCode = RoleClassRoot.ASSIGNED
                act.participant(0).participantRole.addr = Nothing
                act.participant(0).participantRole.code = New CE()
                act.participant(0).participantRole.code.code = "158965000"
                act.participant(0).participantRole.code.displayName = "Medical Practitioner"


                act.participant(1) = New POCD_MT000040UV02Participant3
                act.participant(1).typeCode = ParticipationType.IRCP
                act.participant(1).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(1).participantRole.classCode = RoleClassRoot.PAT
                act.participant(1).participantRole.addr = Nothing
                act.participant(1).participantRole.code = New CE()
                act.participant(1).participantRole.code.code = "116154003"
                act.participant(1).participantRole.code.displayName = "Patient"

            ElseIf _dtDistinct.Rows(patientComponent)(Col_QRDA1_Category) = "Communication from Provider to Provider" Then
                act.participant = New POCD_MT000040UV02Participant3(1) {}
                act.participant(0) = New POCD_MT000040UV02Participant3
                act.participant(0).typeCode = ParticipationType.AUT
                act.participant(0).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(0).participantRole.classCode = RoleClassRoot.ASSIGNED
                act.participant(0).participantRole.addr = Nothing
                act.participant(0).participantRole.code = New CE()
                act.participant(0).participantRole.code.code = "158965000"
                act.participant(0).participantRole.code.displayName = "Medical Practitioner"

                act.participant(1) = New POCD_MT000040UV02Participant3
                act.participant(1).typeCode = ParticipationType.IRCP
                act.participant(1).participantRole = New POCD_MT000040UV02ParticipantRole
                act.participant(1).participantRole.classCode = RoleClassRoot.ASSIGNED
                act.participant(1).participantRole.addr = Nothing
                act.participant(1).participantRole.code = New CE()
                act.participant(1).participantRole.code.code = "158965000"
                act.participant(1).participantRole.code.displayName = "Medical Practitioner"

            End If

            act.participant(0).participantRole.code.codeSystem = CodeSystem.SNOMED_CT
            act.participant(0).participantRole.code.codeSystemName = "SNOMED_CT"
            act.participant(0).participantRole.code.codeSystemVersion = Nothing
            act.participant(0).participantRole.code.displayName = "Patient"
            act.participant(0).participantRole.id = Nothing
            act.participant(0).participantRole.telecom = Nothing
            act.participant(0).participantRole.typeId = Nothing

            act.participant(1).participantRole.code.codeSystem = CodeSystem.SNOMED_CT
            act.participant(1).participantRole.code.codeSystemName = "SNOMED_CT"
            act.participant(1).participantRole.code.codeSystemVersion = Nothing

            act.participant(1).participantRole.id = Nothing
            act.participant(1).participantRole.telecom = Nothing
            act.participant(1).participantRole.typeId = Nothing



        Catch ex As Exception
            If _msg = "" Then

                _msg = vbNewLine & _error & vbNewLine & "Patient Data Section"
            Else
                _msg = _msgString & vbNewLine & "Patient Data Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally


        End Try
        Return _entry

    End Function
#End Region
End Class
