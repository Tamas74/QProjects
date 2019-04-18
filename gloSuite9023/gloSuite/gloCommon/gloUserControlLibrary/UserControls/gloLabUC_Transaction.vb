Imports System.Configuration

Imports gloEMRGeneralLibrary
Imports System.Text
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase


Public Class gloLabUC_Transaction

#Region "Previous Column Width"
    ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
    'used when user tries to minimize width column contains dropdown list in before and after column resize event
    ''Start
    Dim prevWidth_COL_TEST_CPT_RESULT_UNIT As Int16
    Dim prevWidth_COL_TEST_DIGNOSIS_RESULT_VALUE As Int16
    Dim prevWidth_COL_TEST_STATUS As Int16
    ''End
#End Region

#Region "private variables and events"
    Dim items() As String
    Dim dropdown As New gloUserControlLibrary.CheckedListBoxDroDown(items, False)
    Dim _isfinal As Boolean = False
    Dim _isnormal As Boolean = False
    Dim _isAbnormal As Boolean = False
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Dim _ConnectionString As String = String.Empty

    Dim _nUserId As Int64 = 0
    Dim _IsColumnWidthSet As Boolean = False

    Dim usePreferredLabforIndividualTest As Boolean = False

    Private _TransactionType As enumTransactionType

    Private Const COL_TEST_RESULT_SELECT = 0
    Private Const COL_TEST_RESULT_NAME = 1


    Private Const COL_LAB_INFOBUTTON = 2
    Private Const COL_TEST_RESULT_CODE = 3
    Private Const COL_ORDERID = 4
    Private Const COL_TESTID = 5
    Private Const COL_TEST_LINENO = 6
    Private Const COL_RESULT_NUMBER = 7
    Private Const COL_RESULT_LINENO = 8
    Private Const COL_RESULT_NAMEID = 9
    Private Const COL_TEST_DIGNOSIS_RESULT_VALUE = 10
    Private Const COL_TEST_DIGNOSISLIST = 11
    Private Const COL_TEST_DIGNOSISBTN = 12
    Private Const COL_TEST_CPT_RESULT_UNIT = 13
    Private Const COL_TEST_CPTLIST = 14
    Private Const COL_TEST_CPTBTN = 15

    Private Const COL_TEST_NOTE = 16
    Private Const COL_TEST_SPECIMEN = 17
    Private Const COL_TEST_COLLECTION = 18
    Private Const COL_TEST_STORAGE = 19
    Private Const COL_RESULT_VALUE_TYPE = 20
    Private Const COL_RESULT_RANGE = 21
    Private Const COL_LAB_SPEC_RANGE = 22 ''instrcution
    Private Const COL_RESULT_COMMENT = 23
    Private Const COL_TEST_INSTRUCTIONBTN = 24

    Private Const COL_ABNORMAL_FLAG = 25  '' Added on 20070602 ''precauction
    Private Const COL_TEST_PRECAUTIONBTN = 26
    Private Const COL_RESULT_TYPE = 27 ''comments
    Private Const COL_TEST_COMMENTSBTN = 28

    Private Const COL_SCHEDULED_DATETIME = 29
    Private Const COL_TEST_RESULT_OR_TRANSFER_DATETIME = 30
    Private Const COL_REPORTED_DATETIME = 31
    Private Const COL_TEST_RESULT_USERID = 32
    Private Const COL_RECORDTYPE = 33
    Private Const COL_LAB_TEST_CODE = 34
    Private Const COL_TEST_LOINC = 35
    Private Const COL_TEST_CPT = 36

    Private Const COL_ISFINISHED = 37

    Private Const COL_DMSID = 38
    Private Const COL_SCAN = 39
    Private Const COL_VIEW = 40

    Private Const COL_TESTNAME = 41

    Private Const COL_SPECIMEN_RECEIVED_DATETIME = 42
    Private Const COL_RESULT_TRANSFER_DATETIME = 43

    Private Const COL_ALTERNATE_TEST_NAME = 44
    Private Const COL_ALTERNATE_TEST_CODE = 45
    Private Const COL_ALTERNATE_RESULT_NAME = 46
    Private Const COL_ALTERNATE_RESULT_CODE = 47
    Private Const COL_PRODUCER_IDENTIFIER = 48

    Private Const COL_DICOMID = 49

    Private Const COL_TEST_STATUS = 50
    Private Const COL_SPECIMEN_SOURCE = 51
    Private Const COL_SPECIMEN_CONDITION_DISP = 52
    Private Const COL_LAB_FACILITY_NAME = 53
    Private Const COL_LAB_FACILITY_STREET_ADDRESS = 54
    Private Const COL_LAB_FACILITY_CITY = 55
    Private Const COL_LAB_FACILITY_STATE = 56
    Private Const COL_LAB_FACILITY_ZIP_CODE = 57
    Private Const COL_LAB_TEST_TYPE = 58

    Private Const COL_LAB_ORIGINAL_ABNORMAL_FLAG = 59

    Private Const COL_SPECIMENCOLLECTIONDATE = 60

    Private Const COL_TEST_RESULT_DATETIME = 61
    Private Const COL_TEST_WORDTEMPLATE = 62

    Private Const COL_DMSIDCollection = 63

    Private Const COL_TestResultDateTimeUTC = 64
    Private Const COL_SpecimenReceivedDateTimeUTC = 65
    Private Const COL_ResultTransferDateTimeUTC = 66
    Private Const COL_LabFacilityIdentifierTypeCode = 67
    Private Const COL_LabFacilityOrganizationIdentifier = 68
    Private Const COL_LabFacilityCountry = 69
    Private Const COL_LabFacilityCountyOrParishCode = 70
    Private Const COL_ResultCode = 71
    Private Const COL_ResultCodeType = 72
    Private Const COL_LabFacilityMedicalDirectorIDNumber = 73
    Private Const COL_LabFacilityMedicalDirectorLastName = 74
    Private Const COL_LabFacilityMedicalDirectorMiddleName = 75
    Private Const COL_LabFacilityMedicalDirectorSuffix = 76
    Private Const COL_LabFacilityMedicalDirectorPrefix = 77
    Private Const COL_LabFacilityMedicalDirectorFirstName = 78
    Private Const COL_ResultParentChildFlag = 79
    Private Const COL_ResultDateTimeUTC = 80
    Private Const COL_TestSpecimenCollectionDateTimeUTC = 81

    Private Const COL_SPECIMEN_TYPE_IDENTIFIER = 82
    Private Const COL_SPECIMEN_TYPE = 83
    Private Const COL_SPECIMEN_TYPE_CODING_SYSTEM = 84
    Private Const COL_SPECIMEN_COLLECTION_START_DATE_TIME = 85
    Private Const COL_SPECIMEN_REJECT_REASON = 86
    Private Const COL_SPECIMEN_CONDITION = 87
    Private Const COL_SPECIMEN_ACTION_CODE = 88
    Private Const COL_TEST_SCHEDULED_END_DATE_TIME = 89
    Private Const COL_DATE_TIME_UTC = 90
    Private Const COL_TEST_SCHEDULED_DATE_TIME_UTC = 91
    Private Const COL_TEST_SCHEDULED_END_DATE_TIME_UTC = 92
    Private Const COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC = 93

    Private Const COL_ConceptID = 94
    Private Const COL_ICD9 = 95
    Private Const COL_ICD10 = 96
    Private Const COL_LOINC = 97

    Private Const COL_PREFERREDLABID = 98
    Private Const COL_PREFERREDLAB = 99

    Private Const COL_COUNT = 100

    Private Const COL_DT_ID = 0
    Private Const COL_DT_SELECT = 1
    Private Const COL_DT_CODE = 2
    Private Const COL_DT_DESCRIPTON = 3
    Private Const COL_DT_COUNT = 4


    Private _CurrentRow As Int16 = 1
    Private _CurrentColumn As Int16 = 1

    Private _CurrentTestSelectedID As Int64 = 0 ' It used to set result against that test so take test id
    Private _CurrentTestResultSelectedCounterID As Int64 = 0 ' It used to set result against that test, it retrive result counter id like its Primary - 1, primary - 2, finalize - 3
    Private _CurrentTestResultIsModify As Boolean = False
    Private _CurrentTestLineNo As Int16 = 0

    Private _PreviousTestID As Int64 = 0 ' it use to check is test changes then fire testselected event
    Private _CurrentOrderID As Int64 = 0 ' if new then 0 else modified order id

    Private _DgnCPTRunTimeComboList As Int16 = 0

    Public Event gUC_TestSelected(ByVal TestID As Int64, ByVal Specimen As String, ByVal CollectionContainer As String, ByVal StorageTemperature As String, ByVal LOINCCode As String, ByVal Instructionas As String, ByVal Precuation As String, ByVal Comments As String)

    Public Event gUC_ScanDocument(ByVal TestID As Int64)

    Public Event gUC_ViewDocument(ByVal TestID As Int64, ByVal DocumentID As Int64)

    Public Event gUC_ViewURLDocument(ByVal TestID As Int64, ByVal DocumentID As Int64)




    Public Event gUC_AddFormHandlerClick()

    Public Event gUC_ViewDicomDocument(ByVal nPatientId As Int64, ByVal DocumentID As Int64)

    Public Event gUC_ButtonDiagnCPTClicked()

    Public Event gUC_ButtonTemplatesClicked(ByVal nTestId As Int64, ByVal nTemplateId As Object, ByVal refreshtemplate As Boolean, isfinished As Integer) ''refreshtemplate added to load liquidlink data for firsttime ,not while modifying

    ''Added by Mayuri:20140313-Added event Ok to fill task users list.
    Public Event gUC_OkButtonClicked(ByVal nSendTaskType As Int16, ByVal nResultType As Int16)
    ''
    Private _ResultTypeList As String = " "
    Private _AbnormalFlagList As String = " "

    Private _AbnormalFlag_COL As gloEMRActors.LabActor.ItemDetails
    Private _ObservationStatus_COL As gloEMRActors.LabActor.ItemDetails

    Private _strLabTestName As String
    Private _strLabResultName As String
    Private _nLabTestId As Int64
    Private _nLabResultId As Int64
    Private _dtSelectedFromDt As DateTime
    Private _dtSelectedToDt As DateTime

    Public Event ShowGraph(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ShowGraph_crieteria(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event OnSubWindow_Closed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event OnSubWindow_Opened(ByVal sender As Object, ByVal e As EventArgs)

    Dim strColName As String = ""

    Public arrTestNames As ArrayList

    ''Sandip Darade 200908221 whether to select diagnosis or CPT 
    Private _DgnCPT As Int16 = 1
    Private _bIsSearch As Boolean

    Dim strDia As String = ""
    Dim strCPT As String = ""
    Dim nPatientID As Int64
    Dim nProviderID As Int64

    'Madan added on ---20100317--- checks the type of the order....
    Dim strOrderType As String = ""
    'Madan-- addeon on--20100413-- for closing event..

    Private _LabModified As Boolean = False
    Private _IsLoading As Boolean = False

    Private _IsLoadLastTransaction As Boolean = False


    Private _IsOrderLocked = False
    Private _IsCQMConceptDisp = False

    Private _ParentControl As String = ""
    Private templateId As Long
    Public Event LockOrder(ByVal OrderID As Long)
    Dim ht As C1.Win.C1FlexGrid.HitTestInfo = Nothing ''added by manoj on 20121127

    ''Event Added for Infobutton
    Public Event gUC_InfoButtonClicked(ByVal lCode As String)
    Public Event gUC_InfoButtonDocumentClicked(ByVal tCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String)
    Public Event gUC_InfoButtonClickedDB(ByVal lcode As String)
    Public Event gUC_InfoButtonDocumentClickedDB(ByVal tCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String)

    ''Class variable FOr infobutton

    Dim isEducationMaterialEnables As Boolean = False
    Dim isAdvancedReference As Boolean = False
    Dim nDocList As ArrayList
   


    Dim nURLDocList As ArrayList

    Public dtURLDoc As DataTable

    Dim WithEvents frmLabURL As frmLabURLdocument

    Dim oContxMenu As ContextMenuStrip
    Dim flexrow As Integer = 0


#End Region

#Region "Properties and Enums"
    
    Public Property PatientID() As Int64 ''This property added by Abhijeet on 20100625
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property
    Public Property ProviderID() As Int64 ''This property added by Abhijeet on 20100625
        Get
            Return nProviderID
        End Get
        Set(ByVal value As Int64)
            nProviderID = value
        End Set
    End Property
    Public Property TransactionType() As enumTransactionType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As enumTransactionType)
            _TransactionType = value
        End Set
    End Property

    Public Property LabTestName() As String
        Get
            Return _strLabTestName
        End Get
        Set(ByVal value As String)
            _strLabTestName = value
        End Set
    End Property

    Public Property LabResultName() As String
        Get
            Return _strLabResultName
        End Get
        Set(ByVal value As String)
            _strLabTestName = value
        End Set
    End Property

    Public Property LabTestId() As Int64
        Get
            Return _nLabTestId
        End Get
        Set(ByVal value As Int64)
            _nLabTestId = value
        End Set
    End Property

    Public Property LabResultId() As Int64
        Get
            Return _nLabResultId
        End Get
        Set(ByVal value As Int64)
            _nLabResultId = value
        End Set
    End Property

    ' return the selected transaction date
    Public Property dtSelectedToDt() As DateTime
        Get
            Return _dtSelectedToDt
        End Get
        Set(ByVal value As DateTime)
            _dtSelectedToDt = value
        End Set
    End Property

    Public Property dtSelectedFromDt() As DateTime
        Get
            Return _dtSelectedFromDt
        End Get
        Set(ByVal value As DateTime)
            _dtSelectedFromDt = value
        End Set
    End Property
    'Madan-- Added on 20100413
    Public Property LabModified() As Boolean
        Get
            Return _LabModified
        End Get
        Set(ByVal value As Boolean)
            _LabModified = value
        End Set
    End Property
    'Madan-- Added on 20100517
    Public Property IsLoadLastTransaction() As Boolean
        Get
            Return _IsLoadLastTransaction
        End Get
        Set(ByVal value As Boolean)
            _IsLoadLastTransaction = value
        End Set
    End Property
    Public Property IsOrderLocked() As Boolean
        Get
            Return _IsOrderLocked
        End Get
        Set(ByVal value As Boolean)
            _IsOrderLocked = value
        End Set
    End Property
    Public Property IsCQMConceptDisplay() As Boolean
        Get
            Return _IsCQMConceptDisp
        End Get
        Set(ByVal value As Boolean)
            _IsCQMConceptDisp = value
        End Set
    End Property
    Public Property ParentControl() As String
        Get
            Return _ParentControl
        End Get
        Set(ByVal value As String)
            _ParentControl = value
        End Set
    End Property

    Public Enum enumTransactionType
        None = 0
        LabOrder = 1
        LabResult = 2
        LabExternalResult = 3
    End Enum

    Public Enum enumRecordType
        None = 0
        Test = 1
        TestHeader = 2
        Result = 3
        ResultHeader = 4
    End Enum
#End Region

    Private Sub DesignTestGrid()

        SetGridStyle(_Flex)

        With _Flex
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0

            'GLO2011-0015782 One client workstation does not have diagnosis icon under orders menu
            'after resize the button present in column is not show.
            ''Start''
            .Cols(COL_TEST_DIGNOSISBTN).AllowResizing = False
            .Cols(COL_TEST_CPTBTN).AllowResizing = False
            .Cols(COL_TEST_INSTRUCTIONBTN).AllowResizing = False
            .Cols(COL_TEST_PRECAUTIONBTN).AllowResizing = False
            .Cols(COL_TEST_COMMENTSBTN).AllowResizing = False
            .Cols(COL_TEST_COMMENTSBTN).AllowEditing = True

            .Cols(COL_TEST_DIGNOSISBTN).AllowSorting = False
            .Cols(COL_TEST_CPTBTN).AllowSorting = False
            .Cols(COL_TEST_INSTRUCTIONBTN).AllowSorting = False
            .Cols(COL_TEST_PRECAUTIONBTN).AllowSorting = False
            .Cols(COL_TEST_COMMENTSBTN).AllowSorting = False
            .Cols(COL_TEST_STATUS).AllowSorting = False
            .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).AllowSorting = False
            .Cols(COL_TEST_CPT_RESULT_UNIT).AllowSorting = False
            ''End''

            ''Infobutton
            .Cols(COL_LAB_INFOBUTTON).AllowResizing = False
            .SetData(0, COL_LAB_INFOBUTTON, "")
            .Cols(COL_LAB_INFOBUTTON).Width = 22
            If isEducationMaterialEnables Then
                .Cols(COL_LAB_INFOBUTTON).Visible = True
            Else
                .Cols(COL_LAB_INFOBUTTON).Visible = False
            End If

            .Cols(COL_LAB_INFOBUTTON).AllowEditing = False ''added for bugid 80184
            ''

            .SetData(0, COL_TEST_RESULT_SELECT, "Select")
            .SetData(0, COL_TEST_RESULT_NAME, "Test")
            .SetData(0, COL_TEST_RESULT_CODE, "Code")
            .SetData(0, COL_ORDERID, "Order ID")
            .SetData(0, COL_TESTID, "Test ID")
            .SetData(0, COL_TEST_LINENO, "Test Line No")
            .SetData(0, COL_RESULT_NUMBER, "Result Number")
            .SetData(0, COL_RESULT_LINENO, "Result Line No")
            .SetData(0, COL_RESULT_NAMEID, "Result Name ID")
            .SetData(0, COL_TEST_DIGNOSIS_RESULT_VALUE, "Diagnosis")
            .SetData(0, COL_TEST_DIGNOSISLIST, "Diagnosis List")
            .SetData(0, COL_TEST_DIGNOSISBTN, "...")
            .SetData(0, COL_TEST_CPT_RESULT_UNIT, "Treatments")
            .SetData(0, COL_TEST_CPTLIST, "Treatment List")
            .SetData(0, COL_TEST_CPTBTN, "...")
            .SetData(0, COL_TEST_NOTE, "Note")
            .SetData(0, COL_TEST_SPECIMEN, "Specimen")
            .SetData(0, COL_TEST_COLLECTION, "Collection")
            .SetData(0, COL_TEST_STORAGE, "Storage")
            .SetData(0, COL_LAB_SPEC_RANGE, "Instruction")
            .SetData(0, COL_TEST_INSTRUCTIONBTN, "...")
            .SetData(0, COL_ABNORMAL_FLAG, "Precaution")
            .SetData(0, COL_TEST_PRECAUTIONBTN, "...")
            .SetData(0, COL_RESULT_TYPE, "Comments")
            .SetData(0, COL_TEST_COMMENTSBTN, "...")
            .SetData(0, COL_RESULT_VALUE_TYPE, "Value Type")
            .SetData(0, COL_RESULT_RANGE, "Templates")

            .SetData(0, COL_RESULT_COMMENT, "Comment")
            ' .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")
            .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")


            .Cols(COL_SCHEDULED_DATETIME).DataType = GetType(DateTime)
            .SetData(0, COL_SCHEDULED_DATETIME, "Scheduled")

            'Sanjog
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).DataType = GetType(DateTime)
            .SetData(0, COL_TEST_RESULT_OR_TRANSFER_DATETIME, "Collected")
            'Sanjog

            .Cols(COL_REPORTED_DATETIME).DataType = GetType(DateTime)
            .SetData(0, COL_REPORTED_DATETIME, "Reported")

            .SetData(0, COL_TEST_RESULT_USERID, "R. UserID")
            .SetData(0, COL_RECORDTYPE, "Rcd. Type")
            ''Added column heading by Abhijeet on 20101116
            ''.SetData(0, COL_TEST_LOINC, "")
            .SetData(0, COL_TEST_LOINC, "Lab Test Code")
            .SetData(0, COL_LAB_TEST_CODE, "LOINC Code")
            .SetData(0, COL_TEST_CPT, "CPT")
            '' End of changes by Abhijeet on 20101116 for column heading

            .SetData(0, COL_ISFINISHED, "Finished")
            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            .SetData(0, COL_PRODUCER_IDENTIFIER, "PRODUCER_IDENTIFIER")
            .SetData(0, COL_ALTERNATE_RESULT_CODE, "ALTERNATIVE_RESULT_CODE")
            .SetData(0, COL_ALTERNATE_RESULT_NAME, "ALTERNATIVE_RESULT_NAME")
            .SetData(0, COL_ALTERNATE_TEST_CODE, "ALTERNATIVE_TEST_CODE")
            .SetData(0, COL_ALTERNATE_TEST_NAME, "ALTERNATIVE_TEST_NAME")
            .SetData(0, COL_SPECIMEN_RECEIVED_DATETIME, "SPECIMEN_RECEIVED_DATETIME")
            .SetData(0, COL_RESULT_TRANSFER_DATETIME, "RESULT_TRANSFER_DATETIME")
            'End Madan..

            ''Added by Abhijeet on 20100917
            .SetData(0, COL_TEST_STATUS, "Status")
            .Cols(COL_TEST_STATUS).ComboList = "Ordered|Resulted|Inactive|Erroneous|Completed|Discontinued|Some, but not all, results available|Correction to results|Final results|No results available; specimen received, procedure incomplete|Preliminary|No results available; procedure scheduled, but not done|No results available, order cancelled"
            .SetData(0, COL_SPECIMEN_SOURCE, "Specimen Source")
            .SetData(0, COL_SPECIMEN_CONDITION_DISP, "Condition of Specimen")
            .SetData(0, COL_LAB_TEST_TYPE, "Test Type")

            .SetData(0, COL_LAB_ORIGINAL_ABNORMAL_FLAG, "")
            ''End of changes by Abhijeet on 20101026

            .SetData(0, COL_DMSID, "DMS ID")
            .SetData(0, COL_SCAN, "Scan")
            .SetData(0, COL_VIEW, "View")
            .SetData(0, COL_DICOMID, "DICOM ID")

            .SetData(0, COL_DMSIDCollection, "DMSID Collection")

            .SetData(0, COL_TestResultDateTimeUTC, "TestResultDateTimeUTC")
            .SetData(0, COL_SpecimenReceivedDateTimeUTC, "SpecimenReceivedDateTimeUTC")
            .SetData(0, COL_ResultTransferDateTimeUTC, "ResultTransferDateTimeUTC")
            .SetData(0, COL_LabFacilityIdentifierTypeCode, "LabFacilityIdentifierTypeCode")
            .SetData(0, COL_LabFacilityOrganizationIdentifier, "LabFacilityOrganizationIdentifier")
            .SetData(0, COL_LabFacilityCountry, "LabFacilityCountry")
            .SetData(0, COL_LabFacilityCountyOrParishCode, "LabFacilityCountyOrParishCode")
            .SetData(0, COL_ResultCode, "ResultCode")
            .SetData(0, COL_ResultCodeType, "ResultCodeType")
            .SetData(0, COL_LabFacilityMedicalDirectorIDNumber, "LabFacilityMedicalDirectorIDNumber")
            .SetData(0, COL_LabFacilityMedicalDirectorLastName, "LabFacilityMedicalDirectorLastName")
            .SetData(0, COL_LabFacilityMedicalDirectorMiddleName, "LabFacilityMedicalDirectorMiddleName")
            .SetData(0, COL_LabFacilityMedicalDirectorSuffix, "LabFacilityMedicalDirectorSuffix")
            .SetData(0, COL_LabFacilityMedicalDirectorPrefix, "LabFacilityMedicalDirectorPrefix")
            .SetData(0, COL_LabFacilityMedicalDirectorFirstName, "LabFacilityMedicalDirectorFirstName")
            .SetData(0, COL_ResultParentChildFlag, "ResultParentChildFlag")
            .SetData(0, COL_ResultDateTimeUTC, "ResultDateTimeUTC")
            .SetData(0, COL_TestSpecimenCollectionDateTimeUTC, "TestSpecimenCollectionDateTimeUTC")

            .SetData(0, COL_SPECIMEN_TYPE_IDENTIFIER, "Specimen Type Identifier")
            .SetData(0, COL_SPECIMEN_TYPE, "Specimen Type")
            .SetData(0, COL_SPECIMEN_TYPE_CODING_SYSTEM, "Specimen Type Coding System")
            .SetData(0, COL_SPECIMEN_COLLECTION_START_DATE_TIME, "Specimen Collection Date Time")
            .SetData(0, COL_SPECIMEN_REJECT_REASON, "Specimen Reject Reason")
            .SetData(0, COL_SPECIMEN_CONDITION, "Specimen Condition")
            .SetData(0, COL_SPECIMEN_ACTION_CODE, "Specimen Action Code")
            .SetData(0, COL_TEST_SCHEDULED_END_DATE_TIME, "Test Scheduled End Date Time")
            .SetData(0, COL_DATE_TIME_UTC, "Date Time UTC")
            .SetData(0, COL_TEST_SCHEDULED_DATE_TIME_UTC, "Test Scheduled Date Time UTC")
            .SetData(0, COL_TEST_SCHEDULED_END_DATE_TIME_UTC, "Test Scheduled End Date Time UTC")
            .SetData(0, COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC, "Specimen Collection Start Date Time UTC")

            .SetData(0, COL_ConceptID, "ConceptID")
            .SetData(0, COL_ICD9, "ICD9")
            .SetData(0, COL_ICD10, "ICD10")
            .SetData(0, COL_LOINC, "LOINC")
            .SetData(0, COL_PREFERREDLABID, "Preforming Lab ID")
            .SetData(0, COL_PREFERREDLAB, "Preforming Lab")
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Rows(0).Height = 22

            .Tree.Column = COL_TEST_RESULT_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15
            '.Cols(0).AllowEditing = False
            '.Cols(1).AllowEditing = False
            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False
            .Cols(COL_TEST_DIGNOSISLIST).DataType = GetType(Object)
            .Cols(COL_TEST_CPTLIST).DataType = GetType(Object)
            .Cols(COL_TEST_WORDTEMPLATE).DataType = GetType(Object)

            .Cols(COL_TEST_RESULT_CODE).Visible = False
            .Cols(COL_ORDERID).Visible = False
            .Cols(COL_TESTID).Visible = False
            .Cols(COL_TEST_LINENO).Visible = False
            .Cols(COL_RESULT_NUMBER).Visible = False
            .Cols(COL_RESULT_LINENO).Visible = False
            .Cols(COL_RESULT_NAMEID).Visible = False
            .Cols(COL_TEST_CPTLIST).Visible = False
            .Cols(COL_TEST_NOTE).Visible = False
            .Cols(COL_TEST_SPECIMEN).Visible = False
            .Cols(COL_TEST_COLLECTION).Visible = False
            .Cols(COL_TEST_STORAGE).Visible = False
            .Cols(COL_RESULT_VALUE_TYPE).Visible = False
            .Cols(COL_RESULT_COMMENT).Visible = False
            .Cols(COL_TEST_RESULT_USERID).Visible = False
            .Cols(COL_RECORDTYPE).Visible = False
            .Cols(COL_ISFINISHED).Visible = True
            .Cols(COL_DMSID).Visible = False
            .Cols(COL_SCAN).Visible = False
            .Cols(COL_VIEW).Visible = False
            .Cols(COL_TESTNAME).Visible = False

            .Cols(COL_DMSIDCollection).Visible = False

            .Cols(COL_PRODUCER_IDENTIFIER).Visible = False
            .Cols(COL_ALTERNATE_RESULT_CODE).Visible = False
            .Cols(COL_ALTERNATE_RESULT_NAME).Visible = False
            .Cols(COL_ALTERNATE_TEST_CODE).Visible = False
            .Cols(COL_ALTERNATE_TEST_NAME).Visible = False
            .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Visible = False
            .Cols(COL_RESULT_TRANSFER_DATETIME).Visible = False
            'End Madan.
            'Madan--20100417-- added
            .Cols(COL_TEST_RESULT_SELECT).Visible = False
            .Cols(COL_SPECIMENCOLLECTIONDATE).Visible = False
            'End
            .Cols(COL_DICOMID).Visible = False

            .Cols(COL_LAB_SPEC_RANGE).Visible = True
            .Cols(COL_SCHEDULED_DATETIME).Visible = True
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Visible = True

            .Cols(COL_REPORTED_DATETIME).Visible = True

            .Cols(COL_LAB_SPEC_RANGE).Width = 150
            ''Added by Abhijeet on 20101026
            .Cols(COL_LAB_TEST_TYPE).Width = 150
            .Cols(COL_LAB_TEST_TYPE).Visible = True
            .Cols(COL_LAB_TEST_TYPE).AllowEditing = False
            .Cols(COL_LAB_TEST_TYPE).DataType = GetType(String)
            .Cols(COL_TEST_STATUS).Width = 150
            .Cols(COL_SPECIMEN_SOURCE).Width = 150
            .Cols(COL_SPECIMEN_CONDITION_DISP).Width = 0
            .Cols(COL_TEST_STATUS).Visible = True
            .Cols(COL_SPECIMEN_SOURCE).Visible = True
            .Cols(COL_SPECIMEN_CONDITION_DISP).Visible = False

            .Cols(COL_LAB_FACILITY_NAME).Width = 0
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Width = 0
            .Cols(COL_LAB_FACILITY_CITY).Width = 0
            .Cols(COL_LAB_FACILITY_STATE).Width = 0
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Width = 0

            .Cols(COL_LAB_FACILITY_NAME).Visible = False
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Visible = False
            .Cols(COL_LAB_FACILITY_CITY).Visible = False
            .Cols(COL_LAB_FACILITY_STATE).Visible = False
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Visible = False
            .Cols(COL_TEST_WORDTEMPLATE).Width = 0
            .Cols(COL_TEST_WORDTEMPLATE).Visible = False

            .Cols(COL_LAB_ORIGINAL_ABNORMAL_FLAG).Width = 0
            .Cols(COL_LAB_ORIGINAL_ABNORMAL_FLAG).Visible = False
            ''End of changes By Abhijeet on 20101026

            .Cols(COL_RESULT_RANGE).AllowEditing = True
            .Cols(COL_RESULT_TYPE).AllowEditing = False
            .Cols(COL_ABNORMAL_FLAG).AllowEditing = False
            .Cols(COL_ISFINISHED).AllowEditing = False
            .Cols(COL_TEST_RESULT_NAME).AllowEditing = False '' SUDHIR 20090717 '' RC1 BUG ''
            .Cols(COL_TEST_RESULT_DATETIME).AllowEditing = False '' SUDHIR 20090720 '' RC1 BUG ''

            .Cols(COL_SCHEDULED_DATETIME).AllowEditing = True
            'Sanjog
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = True
            'Sanjog

            .Cols(COL_REPORTED_DATETIME).AllowEditing = True

            ''Added by Abhijeet on 20100919 for CCHIT certification requirement 
            .Cols(COL_TEST_LOINC).AllowEditing = False
            .Cols(COL_LAB_TEST_CODE).AllowEditing = False
            ''End of changes Added by Abhijeet on 20100919 for CCHIT certification requirement 
            .Cols(COL_SCHEDULED_DATETIME).Format = "MM/dd/yyyy hh:mm:ss tt"

            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Format = "MM/dd/yyyy hh:mm:ss tt"    ' "MM/dd/yyyy HH:mm:ss"
            .Cols(COL_REPORTED_DATETIME).Format = "MM/dd/yyyy hh:mm:ss tt"
            Dim dtp As DateTimePicker = New DateTimePicker
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
            .Cols(COL_SCHEDULED_DATETIME).Editor = dtp
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Editor = dtp
            .Cols(COL_REPORTED_DATETIME).Editor = dtp
            .Cols(COL_TEST_CPT).Width = 100
            .Cols(COL_TEST_CPT).AllowEditing = False

            .Cols(COL_TestResultDateTimeUTC).Visible = False
            .Cols(COL_SpecimenReceivedDateTimeUTC).Visible = False
            .Cols(COL_ResultTransferDateTimeUTC).Visible = False
            .Cols(COL_LabFacilityIdentifierTypeCode).Visible = False
            .Cols(COL_LabFacilityOrganizationIdentifier).Visible = False
            .Cols(COL_LabFacilityCountry).Visible = False
            .Cols(COL_LabFacilityCountyOrParishCode).Visible = False
            .Cols(COL_ResultCode).Visible = False
            .Cols(COL_ResultCodeType).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorLastName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Visible = False
            .Cols(COL_ResultParentChildFlag).Visible = False
            .Cols(COL_ResultDateTimeUTC).Visible = False
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Visible = False

            .Cols(COL_ConceptID).Visible = False
            .Cols(COL_ICD9).Visible = False
            .Cols(COL_ICD10).Visible = False
            .Cols(COL_LOINC).Visible = False

            .Cols(COL_PREFERREDLABID).Visible = False
            .Cols(COL_PREFERREDLAB).Visible = usePreferredLabforIndividualTest
            .Cols(COL_ISFINISHED).Visible = True
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Cols(COL_TestResultDateTimeUTC).Width = 0
            .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
            .Cols(COL_ResultTransferDateTimeUTC).Width = 0
            .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
            .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
            .Cols(COL_LabFacilityCountry).Width = 0
            .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
            .Cols(COL_ResultCode).Width = 0
            .Cols(COL_ResultCodeType).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
            .Cols(COL_ResultParentChildFlag).Width = 0
            .Cols(COL_ResultDateTimeUTC).Width = 0
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

            .Cols(COL_ConceptID).Width = 0
            .Cols(COL_ICD9).Width = 0
            .Cols(COL_ICD10).Width = 0
            .Cols(COL_LOINC).Width = 0
            .Cols(COL_PREFERREDLABID).Width = 0
            If usePreferredLabforIndividualTest = False Then
                .Cols(COL_PREFERREDLAB).Width = 0
            End If
            .Cols(COL_PREFERREDLAB).AllowEditing = False

            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Visible = False
            .Cols(COL_SPECIMEN_TYPE).Visible = False
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Visible = False
            .Cols(COL_SPECIMEN_REJECT_REASON).Visible = False
            .Cols(COL_SPECIMEN_CONDITION).Visible = False
            .Cols(COL_SPECIMEN_ACTION_CODE).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Visible = False
            .Cols(COL_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Visible = False

            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Width = 0
            .Cols(COL_SPECIMEN_TYPE).Width = 0
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Width = 0
            .Cols(COL_SPECIMEN_REJECT_REASON).Width = 0
            .Cols(COL_SPECIMEN_CONDITION).Width = 0
            .Cols(COL_SPECIMEN_ACTION_CODE).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Width = 0
            .Cols(COL_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Width = 0
        End With

        If RetriveAppSettings() Then

            Dim objSettings As New gloSettings.GeneralSettings(_ConnectionString)

            Try
                If objSettings.LoadGridColumnWidth(_Flex, gloSettings.ModuleOfGridColumn.LabResultGrid, _nUserId) Then
                    _IsColumnWidthSet = True
                Else
                    _IsColumnWidthSet = False
                End If

            Catch ex As Exception
                _IsColumnWidthSet = False
            Finally

                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                End If
            End Try
        End If

        If _IsColumnWidthSet = False Then
            With _Flex
                ''Infobutton
                .Cols(COL_LAB_INFOBUTTON).Width = 22

                .Cols(COL_TEST_RESULT_SELECT).Width = 50
                .Cols(COL_TEST_RESULT_NAME).Width = 150
                .Cols(COL_TEST_RESULT_CODE).Width = 0
                .Cols(COL_ORDERID).Width = 0
                .Cols(COL_TESTID).Width = 0
                .Cols(COL_TEST_LINENO).Width = 0
                .Cols(COL_RESULT_NUMBER).Width = 0
                .Cols(COL_RESULT_LINENO).Width = 0
                .Cols(COL_RESULT_NAMEID).Width = 0
                .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width = 100
                .Cols(COL_TEST_DIGNOSISLIST).Width = 0
                .Cols(COL_TEST_DIGNOSISBTN).Width = 20
                .Cols(COL_TEST_CPT_RESULT_UNIT).Width = 100
                .Cols(COL_TEST_CPTLIST).Width = 0
                .Cols(COL_TEST_CPTBTN).Width = 20
                .Cols(COL_LAB_SPEC_RANGE).Width = 100
                .Cols(COL_TEST_INSTRUCTIONBTN).Width = 20
                .Cols(COL_ABNORMAL_FLAG).Width = 100
                .Cols(COL_TEST_PRECAUTIONBTN).Width = 20
                .Cols(COL_RESULT_TYPE).Width = 100
                .Cols(COL_TEST_COMMENTSBTN).Width = 20
                .Cols(COL_TEST_NOTE).Width = 0
                .Cols(COL_TEST_SPECIMEN).Width = 0
                .Cols(COL_TEST_COLLECTION).Width = 0
                .Cols(COL_TEST_STORAGE).Width = 0
                '.Cols(COL_TEST_INSTRUCTON).Width = 0
                '.Cols(COL_TEST_PRECUATON).Width = 0
                .Cols(COL_RESULT_VALUE_TYPE).Width = 0
                .Cols(COL_RESULT_RANGE).Width = 75
                .Cols(COL_RESULT_TYPE).Width = 150
                .Cols(COL_ABNORMAL_FLAG).Width = 150
                .Cols(COL_RESULT_COMMENT).Width = 0
                'Sanjog
                .Cols(COL_TEST_RESULT_DATETIME).Width = 0
                .Cols(COL_SCHEDULED_DATETIME).Width = 100
                .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Width = 100

                .Cols(COL_REPORTED_DATETIME).Width = 100

                'Sanjog
                .Cols(COL_TEST_RESULT_USERID).Width = 0
                .Cols(COL_RECORDTYPE).Width = 0
                .Cols(COL_TEST_LOINC).Width = 100
                .Cols(COL_LAB_TEST_CODE).Width = 100
                .Cols(COL_ISFINISHED).Width = 100
                ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                'Added by madan-- on 20100409...
                .Cols(COL_PRODUCER_IDENTIFIER).Width = 0
                .Cols(COL_ALTERNATE_RESULT_CODE).Width = 0
                .Cols(COL_ALTERNATE_RESULT_NAME).Width = 0
                .Cols(COL_ALTERNATE_TEST_CODE).Width = 0
                .Cols(COL_ALTERNATE_TEST_NAME).Width = 0
                .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Width = 0
                .Cols(COL_RESULT_TRANSFER_DATETIME).Width = 0
                .Cols(COL_DMSID).Width = 0
                .Cols(COL_SCAN).Width = 0
                .Cols(COL_VIEW).Width = 0
                .Cols(COL_TESTNAME).Width = 0 ''Hide by Sudhir - 20090227
                .Cols(COL_DICOMID).Width = 0 ''Added by madan -2010007
                .Cols(COL_TEST_CPT).Width = 100
                .Cols(COL_DMSIDCollection).Width = 0

                .Cols(COL_TestResultDateTimeUTC).Width = 0
                .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
                .Cols(COL_ResultTransferDateTimeUTC).Width = 0
                .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
                .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
                .Cols(COL_LabFacilityCountry).Width = 0
                .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
                .Cols(COL_ResultCode).Width = 0
                .Cols(COL_ResultCodeType).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
                .Cols(COL_ResultParentChildFlag).Width = 0
                .Cols(COL_ResultDateTimeUTC).Width = 0
                .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

                .Cols(COL_ConceptID).Width = 0
                .Cols(COL_ICD9).Width = 0
                .Cols(COL_ICD10).Width = 0
                .Cols(COL_LOINC).Width = 0
                .Cols(COL_PREFERREDLABID).Width = 0
                If usePreferredLabforIndividualTest = False Then
                    .Cols(COL_PREFERREDLAB).Width = 0
                End If
                .Cols(COL_PREFERREDLAB).AllowEditing = False
                .Cols(COL_TestResultDateTimeUTC).Width = 0
                .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
                .Cols(COL_ResultTransferDateTimeUTC).Width = 0
                .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
                .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
                .Cols(COL_LabFacilityCountry).Width = 0
                .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
                .Cols(COL_ResultCode).Width = 0
                .Cols(COL_ResultCodeType).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
                .Cols(COL_ResultParentChildFlag).Width = 0
                .Cols(COL_ResultDateTimeUTC).Width = 0
                .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

                .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Width = 0
                .Cols(COL_SPECIMEN_TYPE).Width = 0
                .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Width = 0
                .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Width = 0
                .Cols(COL_SPECIMEN_REJECT_REASON).Width = 0
                .Cols(COL_SPECIMEN_CONDITION).Width = 0
                .Cols(COL_SPECIMEN_ACTION_CODE).Width = 0
                .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Width = 0
                .Cols(COL_DATE_TIME_UTC).Width = 0
                .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Width = 0
                .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Width = 0
                .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Width = 0

            End With
        End If
        ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
        'used when user tries to minimize width column contains dropdown list in before and after column resize event
        ''Start
        prevWidth_COL_TEST_STATUS = _Flex.Cols(COL_TEST_STATUS).Width
        prevWidth_COL_TEST_CPT_RESULT_UNIT = _Flex.Cols(COL_TEST_CPT_RESULT_UNIT).Width
        prevWidth_COL_TEST_DIGNOSIS_RESULT_VALUE = _Flex.Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width
        ''End
        _Flex.Cols(COL_ISFINISHED).Width = 100
        _Flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw 'added by manoj on 20121127 for triggering Cell OwnerDraw Event
    End Sub


    'Private Sub _Flex_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles _Flex.KeyUp
    '    If e.KeyCode = Keys.Delete Then
    '        If _Flex.Col = COL_TEST_RESULT_OR_TRANSFER_DATETIME Then
    '            _Flex.SetData(_Flex.RowSel, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Nothing)
    '        End If
    '    End If

    'End Sub

    Private Sub DesignEmdeonTestGrid()
        SetGridStyle(_Flex)
        With _Flex

            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0

            .SetData(0, COL_TEST_RESULT_SELECT, "Select")
            .SetData(0, COL_TEST_RESULT_NAME, "Test")
            .SetData(0, COL_TEST_RESULT_CODE, "Code")
            .SetData(0, COL_ORDERID, "Order ID")
            .SetData(0, COL_TESTID, "Test ID")
            .SetData(0, COL_TEST_LINENO, "Test Line No")
            .SetData(0, COL_RESULT_NUMBER, "Result Number")
            .SetData(0, COL_RESULT_LINENO, "Result Line No")
            .SetData(0, COL_RESULT_NAMEID, "Result Name ID")
            .SetData(0, COL_TEST_DIGNOSIS_RESULT_VALUE, "Diagnosis")
            .SetData(0, COL_TEST_DIGNOSISLIST, "Diagnosis List")
            .SetData(0, COL_TEST_DIGNOSISBTN, "...")
            .SetData(0, COL_TEST_CPT_RESULT_UNIT, "Treatments")
            .SetData(0, COL_TEST_CPTLIST, "Treatment List")
            .SetData(0, COL_TEST_CPTBTN, "...")
            .SetData(0, COL_TEST_NOTE, "Note")
            .SetData(0, COL_TEST_SPECIMEN, "Specimen")
            .SetData(0, COL_TEST_COLLECTION, "Collection")
            .SetData(0, COL_TEST_STORAGE, "Storage")
            .SetData(0, COL_LAB_SPEC_RANGE, "Instruction")
            .SetData(0, COL_TEST_INSTRUCTIONBTN, "...")
            .SetData(0, COL_ABNORMAL_FLAG, "Precaution")
            .SetData(0, COL_TEST_PRECAUTIONBTN, "...")
            .SetData(0, COL_RESULT_TYPE, "Comments")
            .SetData(0, COL_TEST_COMMENTSBTN, "...")
            .SetData(0, COL_RESULT_VALUE_TYPE, "Value Type")
            .SetData(0, COL_RESULT_RANGE, "")
            ' .SetData(0, COL_RESULT_TYPE, "")
            .SetData(0, COL_RESULT_COMMENT, "Comment")
            ' .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")
            .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")
            'Sanjog
            .SetData(0, COL_TEST_RESULT_OR_TRANSFER_DATETIME, "Collected")
            'Sanjog
            'Mitesh
            .SetData(0, COL_REPORTED_DATETIME, "Reported")
            'Mitesh


            .SetData(0, COL_TEST_RESULT_USERID, "R. UserID")
            .SetData(0, COL_RECORDTYPE, "Rcd. Type")
            ''Added column heading by Abhijeet on 20101116
            ''.SetData(0, COL_TEST_LOINC, "")
            .SetData(0, COL_TEST_LOINC, "Lab Test Code")
            .SetData(0, COL_LAB_TEST_CODE, "LOINC Code")
            .SetData(0, COL_TEST_CPT, "CPT")
            '' End of changes by Abhijeet on 20101116 for column heading
            .SetData(0, COL_ISFINISHED, "Finished")
            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            .SetData(0, COL_PRODUCER_IDENTIFIER, "PRODUCER_IDENTIFIER")
            .SetData(0, COL_ALTERNATE_RESULT_CODE, "ALTERNATE_RESULT_CODE")
            .SetData(0, COL_ALTERNATE_RESULT_NAME, "ALTERNATE_RESULT_NAME")
            .SetData(0, COL_ALTERNATE_TEST_CODE, "ALTERNATIVE_TEST_CODE")
            .SetData(0, COL_ALTERNATE_TEST_NAME, "ALTERNATIVE_TEST_NAME")
            .SetData(0, COL_SPECIMEN_RECEIVED_DATETIME, "SPECIMEN_RECEIVED_DATETIME")
            .SetData(0, COL_RESULT_TRANSFER_DATETIME, "RESULT_TRANSFER_DATETIME")
            ''Added by Abhijeet on 20101026
            .SetData(0, COL_TEST_STATUS, "Status")
            .Cols(COL_TEST_STATUS).ComboList = "Ordered|Resulted|Inactive|Erroneous|Completed|Discontinued|Some, but not all, results available|Correction to results|Final results|No results available; specimen received, procedure incomplete|Preliminary|No results available; procedure scheduled, but not done|No results available, order cancelled"
            .SetData(0, COL_SPECIMEN_SOURCE, "Specimen Source")
            .SetData(0, COL_SPECIMEN_CONDITION_DISP, "Condition of Specimen")
            .SetData(0, COL_LAB_TEST_TYPE, "Test Type")

            .SetData(0, COL_LAB_ORIGINAL_ABNORMAL_FLAG, "")
            ''End of changes by Abhijeet on 20101026


            .SetData(0, COL_DMSID, "DMS ID")
            .SetData(0, COL_SCAN, "Scan")
            .SetData(0, COL_VIEW, "View")
            .SetData(0, COL_DICOMID, "DICOM ID")

            .SetData(0, COL_DMSIDCollection, "DMSID Collection")

            .SetData(0, COL_SPECIMEN_TYPE_IDENTIFIER, "Specimen Type Identifier")
            .SetData(0, COL_SPECIMEN_TYPE, "Specimen Type")
            .SetData(0, COL_SPECIMEN_TYPE_CODING_SYSTEM, "Specimen Type Coding System")
            .SetData(0, COL_SPECIMEN_COLLECTION_START_DATE_TIME, "Specimen Collection Date Time")
            .SetData(0, COL_SPECIMEN_REJECT_REASON, "Specimen Reject Reason")
            .SetData(0, COL_SPECIMEN_CONDITION, "Specimen Condition")
            .SetData(0, COL_SPECIMEN_ACTION_CODE, "Specimen Action Code")
            .SetData(0, COL_TEST_SCHEDULED_END_DATE_TIME, "Test Scheduled End Date Time")
            .SetData(0, COL_DATE_TIME_UTC, "Date Time UTC")
            .SetData(0, COL_TEST_SCHEDULED_DATE_TIME_UTC, "Test Scheduled Date Time UTC")
            .SetData(0, COL_TEST_SCHEDULED_END_DATE_TIME_UTC, "Test Scheduled End Date Time UTC")
            .SetData(0, COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC, "Specimen Collection Start Date Time UTC")

            .Rows(0).Height = 22

            .Tree.Column = COL_TEST_RESULT_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15
            '.Cols(0).AllowEditing = False
            '.Cols(1).AllowEditing = False
            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False
            .Cols(COL_TEST_DIGNOSISLIST).DataType = GetType(Object)
            .Cols(COL_TEST_CPTLIST).DataType = GetType(Object)
            .Cols(COL_TEST_WORDTEMPLATE).DataType = GetType(Object)
            'Madan--20100417-- added
            .Cols(COL_TEST_RESULT_SELECT).Visible = False
            'End
            .Cols(COL_DICOMID).Visible = False
            .Cols(COL_RESULT_RANGE).AllowEditing = True
            .Cols(COL_RESULT_TYPE).AllowEditing = False
            .Cols(COL_ABNORMAL_FLAG).AllowEditing = False
            .Cols(COL_TEST_RESULT_NAME).AllowEditing = False '' SUDHIR 20090717 '' RC1 BUG ''
            .Cols(COL_TEST_RESULT_DATETIME).AllowEditing = False '' SUDHIR 20090720 '' RC1 BUG ''
            'visible... change..//Changed for emdeon orders... by Madan
            'Sanjog
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = False
            'Sanjog

            .Cols(COL_ALTERNATE_RESULT_CODE).Visible = False
            .Cols(COL_ALTERNATE_RESULT_NAME).Visible = False
            .Cols(COL_ALTERNATE_TEST_CODE).Visible = False
            .Cols(COL_ALTERNATE_TEST_NAME).Visible = False
            .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Visible = False
            .Cols(COL_RESULT_TRANSFER_DATETIME).Visible = False
            '.Cols(COL_TEST_CPTBTN).Visible = False
            '.Cols(COL_TEST_PRECAUTIONBTN).Visible = False
            .Cols(COL_TEST_DIGNOSISBTN).Visible = False
            .Cols(COL_TEST_CPTBTN).Visible = False
            '.Cols(COL_TEST_INSTRUCTIONBTN).Visible = False
            '.Cols(COL_TEST_COMMENTSBTN).Visible = False
            '.Cols(COL_TEST_DIGNOSISLIST).Visible = False
            '.Cols(COL_TEST_CPTLIST).Visible = False

            .Cols(COL_TEST_RESULT_CODE).Visible = False
            .Cols(COL_ORDERID).Visible = False
            .Cols(COL_TESTID).Visible = False
            .Cols(COL_TEST_LINENO).Visible = False
            .Cols(COL_RESULT_NUMBER).Visible = False
            .Cols(COL_RESULT_LINENO).Visible = False
            .Cols(COL_RESULT_NAMEID).Visible = False
            .Cols(COL_TEST_DIGNOSISLIST).Visible = False
            .Cols(COL_TEST_NOTE).Visible = False
            .Cols(COL_TEST_SPECIMEN).Visible = False
            .Cols(COL_TEST_COLLECTION).Visible = False
            .Cols(COL_TEST_STORAGE).Visible = False

            .Cols(COL_RESULT_VALUE_TYPE).Visible = False
            .Cols(COL_RESULT_COMMENT).Visible = False
            .Cols(COL_TEST_RESULT_USERID).Visible = False
            .Cols(COL_RECORDTYPE).Visible = False
            .Cols(COL_ISFINISHED).Visible = True
            .Cols(COL_PRODUCER_IDENTIFIER).Visible = False
            .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Visible = False
            .Cols(COL_RESULT_TRANSFER_DATETIME).Visible = False
            .Cols(COL_DMSID).Visible = False
            .Cols(COL_SCAN).Visible = False
            .Cols(COL_VIEW).Visible = False
            .Cols(COL_TESTNAME).Visible = False ''Hide by Sudhir - 20090227
            .Cols(COL_DICOMID).Visible = False '

            .Cols(COL_DMSIDCollection).Visible = False

            .Cols(COL_LAB_SPEC_RANGE).Visible = True
            .Cols(COL_LAB_SPEC_RANGE).Width = 100
            ''Added by Abhijeet on 20101026
            .Cols(COL_TEST_STATUS).Width = 150
            .Cols(COL_SPECIMEN_SOURCE).Width = 150
            .Cols(COL_SPECIMEN_CONDITION_DISP).Width = 0
            .Cols(COL_LAB_TEST_TYPE).Width = 150
            .Cols(COL_LAB_TEST_TYPE).DataType = GetType(String)

            .Cols(COL_LAB_ORIGINAL_ABNORMAL_FLAG).Width = 0
            .Cols(COL_LAB_ORIGINAL_ABNORMAL_FLAG).Visible = False
            ''End of changes By Abhijeet on 20101026

            ''Added by Abhijeet on 20101026
            .Cols(COL_TEST_STATUS).Visible = True
            .Cols(COL_SPECIMEN_SOURCE).Visible = True
            .Cols(COL_SPECIMEN_CONDITION_DISP).Visible = False
            .Cols(COL_LAB_TEST_TYPE).Visible = True
            .Cols(COL_LAB_TEST_TYPE).AllowEditing = False

            .Cols(COL_LAB_FACILITY_NAME).Width = 0
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Width = 0
            .Cols(COL_LAB_FACILITY_CITY).Width = 0
            .Cols(COL_LAB_FACILITY_STATE).Width = 0
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Width = 0

            .Cols(COL_LAB_FACILITY_NAME).Visible = False
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Visible = False
            .Cols(COL_LAB_FACILITY_CITY).Visible = False
            .Cols(COL_LAB_FACILITY_STATE).Visible = False
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Visible = False
            .Cols(COL_TEST_WORDTEMPLATE).Visible = False
            .Cols(COL_TEST_WORDTEMPLATE).Width = 0
            ''End of changes By Abhijeet on 20101026
            .Cols(COL_TEST_CPT).Width = 100
            .Cols(COL_TEST_CPT).AllowEditing = False
            ''Added by Abhijeet on 20101026
            .Cols(COL_TEST_LOINC).AllowEditing = False
            .Cols(COL_LAB_TEST_CODE).AllowEditing = False
            ''End of changes Added by Abhijeet on 20101026
            .Cols(COL_REPORTED_DATETIME).Visible = True
            ''Added
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = True
            .Cols(COL_REPORTED_DATETIME).AllowEditing = True

            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Format = "MM/dd/yyyy hh:mm:ss tt"    ' "MM/dd/yyyy HH:mm:ss"
            .Cols(COL_REPORTED_DATETIME).Format = "MM/dd/yyyy hh:mm:ss tt"
            Dim dtp As DateTimePicker = New DateTimePicker
            dtp.Format = DateTimePickerFormat.Custom
            dtp.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Editor = dtp
            .Cols(COL_REPORTED_DATETIME).Editor = dtp

            .SetData(0, COL_TestResultDateTimeUTC, "TestResultDateTimeUTC")
            .SetData(0, COL_SpecimenReceivedDateTimeUTC, "SpecimenReceivedDateTimeUTC")
            .SetData(0, COL_ResultTransferDateTimeUTC, "ResultTransferDateTimeUTC")
            .SetData(0, COL_LabFacilityIdentifierTypeCode, "LabFacilityIdentifierTypeCode")
            .SetData(0, COL_LabFacilityOrganizationIdentifier, "LabFacilityOrganizationIdentifier")
            .SetData(0, COL_LabFacilityCountry, "LabFacilityCountry")
            .SetData(0, COL_LabFacilityCountyOrParishCode, "LabFacilityCountyOrParishCode")
            .SetData(0, COL_ResultCode, "ResultCode")
            .SetData(0, COL_ResultCodeType, "ResultCodeType")
            .SetData(0, COL_LabFacilityMedicalDirectorIDNumber, "LabFacilityMedicalDirectorIDNumber")
            .SetData(0, COL_LabFacilityMedicalDirectorLastName, "LabFacilityMedicalDirectorLastName")
            .SetData(0, COL_LabFacilityMedicalDirectorMiddleName, "LabFacilityMedicalDirectorMiddleName")
            .SetData(0, COL_LabFacilityMedicalDirectorSuffix, "LabFacilityMedicalDirectorSuffix")
            .SetData(0, COL_LabFacilityMedicalDirectorPrefix, "LabFacilityMedicalDirectorPrefix")
            .SetData(0, COL_LabFacilityMedicalDirectorFirstName, "LabFacilityMedicalDirectorFirstName")
            .SetData(0, COL_ResultParentChildFlag, "ResultParentChildFlag")
            .SetData(0, COL_ResultDateTimeUTC, "ResultDateTimeUTC")
            .SetData(0, COL_TestSpecimenCollectionDateTimeUTC, "TestSpecimenCollectionDateTimeUTC")

            .SetData(0, COL_ConceptID, "ConceptID")
            .SetData(0, COL_ICD9, "ICD9")
            .SetData(0, COL_ICD10, "ICD10")
            .SetData(0, COL_LOINC, "LOINC")
            .SetData(0, COL_PREFERREDLABID, "Performing Lab ID")
            .SetData(0, COL_PREFERREDLAB, "Performing Lab")

            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Cols(COL_TestResultDateTimeUTC).Visible = False
            .Cols(COL_SpecimenReceivedDateTimeUTC).Visible = False
            .Cols(COL_ResultTransferDateTimeUTC).Visible = False
            .Cols(COL_LabFacilityIdentifierTypeCode).Visible = False
            .Cols(COL_LabFacilityOrganizationIdentifier).Visible = False
            .Cols(COL_LabFacilityCountry).Visible = False
            .Cols(COL_LabFacilityCountyOrParishCode).Visible = False
            .Cols(COL_ResultCode).Visible = False
            .Cols(COL_ResultCodeType).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorLastName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Visible = False
            .Cols(COL_ResultParentChildFlag).Visible = False
            .Cols(COL_ResultDateTimeUTC).Visible = False
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Visible = False

            .Cols(COL_ConceptID).Visible = False
            .Cols(COL_ICD9).Visible = False
            .Cols(COL_ICD10).Visible = False
            .Cols(COL_LOINC).Visible = False
            .Cols(COL_PREFERREDLABID).Visible = False
            .Cols(COL_PREFERREDLAB).Visible = usePreferredLabforIndividualTest
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Cols(COL_TestResultDateTimeUTC).Width = 0
            .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
            .Cols(COL_ResultTransferDateTimeUTC).Width = 0
            .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
            .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
            .Cols(COL_LabFacilityCountry).Width = 0
            .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
            .Cols(COL_ResultCode).Width = 0
            .Cols(COL_ResultCodeType).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
            .Cols(COL_ResultParentChildFlag).Width = 0
            .Cols(COL_ResultDateTimeUTC).Width = 0
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

            .Cols(COL_ConceptID).Width = 0
            .Cols(COL_ICD9).Width = 0
            .Cols(COL_ICD10).Width = 0
            .Cols(COL_LOINC).Width = 0
            .Cols(COL_PREFERREDLABID).Width = 0
            If usePreferredLabforIndividualTest = False Then
                .Cols(COL_PREFERREDLAB).Width = 0
            End If
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Visible = False
            .Cols(COL_SPECIMEN_TYPE).Visible = False
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Visible = False
            .Cols(COL_SPECIMEN_REJECT_REASON).Visible = False
            .Cols(COL_SPECIMEN_CONDITION).Visible = False
            .Cols(COL_SPECIMEN_ACTION_CODE).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Visible = False
            .Cols(COL_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Visible = False

            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Width = 0
            .Cols(COL_SPECIMEN_TYPE).Width = 0
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Width = 0
            .Cols(COL_SPECIMEN_REJECT_REASON).Width = 0
            .Cols(COL_SPECIMEN_CONDITION).Width = 0
            .Cols(COL_SPECIMEN_ACTION_CODE).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Width = 0
            .Cols(COL_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Width = 0
        End With

        If RetriveAppSettings() Then

            Dim objSettings As New gloSettings.GeneralSettings(_ConnectionString)

            Try
                If objSettings.LoadGridColumnWidth(_Flex, gloSettings.ModuleOfGridColumn.LabResultGrid, _nUserId) Then
                    _IsColumnWidthSet = True
                Else
                    _IsColumnWidthSet = False
                End If

            Catch ex As Exception
                _IsColumnWidthSet = False
            Finally

                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                End If
            End Try
        End If

        With _Flex

            If _IsColumnWidthSet = False Then

                .Cols(COL_TEST_RESULT_SELECT).Width = 50
                .Cols(COL_TEST_RESULT_NAME).Width = 150
                .Cols(COL_TEST_RESULT_CODE).Width = 0
                .Cols(COL_ORDERID).Width = 0
                .Cols(COL_TESTID).Width = 0
                .Cols(COL_TEST_LINENO).Width = 0
                .Cols(COL_RESULT_NUMBER).Width = 0
                .Cols(COL_RESULT_LINENO).Width = 0
                .Cols(COL_RESULT_NAMEID).Width = 0
                .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width = 100
                .Cols(COL_TEST_DIGNOSISLIST).Width = 0
                .Cols(COL_TEST_DIGNOSISBTN).Width = 20
                .Cols(COL_TEST_CPT_RESULT_UNIT).Width = 100
                .Cols(COL_TEST_CPTLIST).Width = 0
                .Cols(COL_TEST_CPTBTN).Width = 20
                .Cols(COL_LAB_SPEC_RANGE).Width = 100
                .Cols(COL_TEST_INSTRUCTIONBTN).Width = 20
                .Cols(COL_ABNORMAL_FLAG).Width = 100
                .Cols(COL_TEST_PRECAUTIONBTN).Width = 20
                .Cols(COL_RESULT_TYPE).Width = 100
                .Cols(COL_TEST_COMMENTSBTN).Width = 20
                .Cols(COL_TEST_NOTE).Width = 0
                .Cols(COL_TEST_SPECIMEN).Width = 0
                .Cols(COL_TEST_COLLECTION).Width = 0
                .Cols(COL_TEST_STORAGE).Width = 0
                '.Cols(COL_TEST_INSTRUCTON).Width = 0
                '.Cols(COL_TEST_PRECUATON).Width = 0
                .Cols(COL_RESULT_VALUE_TYPE).Width = 0
                .Cols(COL_RESULT_RANGE).Width = 75
                .Cols(COL_RESULT_TYPE).Width = 150
                .Cols(COL_ABNORMAL_FLAG).Width = 150
                .Cols(COL_RESULT_COMMENT).Width = 0
                'Sanjog
                .Cols(COL_TEST_RESULT_DATETIME).Width = 0
                .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Width = 100
                'Sanjog
                .Cols(COL_TEST_RESULT_USERID).Width = 0
                .Cols(COL_RECORDTYPE).Width = 0
                .Cols(COL_TEST_LOINC).Width = 100
                .Cols(COL_LAB_TEST_CODE).Width = 100
                .Cols(COL_ISFINISHED).Width = 100
                .Cols(COL_ALTERNATE_RESULT_CODE).Width = 0
                .Cols(COL_ALTERNATE_RESULT_NAME).Width = 0
                .Cols(COL_PRODUCER_IDENTIFIER).Width = 0
                .Cols(COL_ALTERNATE_TEST_CODE).Width = 0
                .Cols(COL_ALTERNATE_TEST_NAME).Width = 0
                .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Width = 0
                'Sanjog
                .Cols(COL_RESULT_TRANSFER_DATETIME).Width = 100
                'Sanjog
                .Cols(COL_DMSID).Width = 0
                .Cols(COL_SCAN).Width = 0
                .Cols(COL_VIEW).Width = 0
                .Cols(COL_TESTNAME).Width = 0
                .Cols(COL_DICOMID).Width = 0
                .Cols(COL_TEST_CPT).Width = 100
                .Cols(COL_DMSIDCollection).Width = 0
                .Cols(COL_TestResultDateTimeUTC).Width = 0
                .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
                .Cols(COL_ResultTransferDateTimeUTC).Width = 0

                .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
                .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
                .Cols(COL_LabFacilityCountry).Width = 0
                .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
                .Cols(COL_ResultCode).Width = 0
                .Cols(COL_ResultCodeType).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
                .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
                .Cols(COL_ResultParentChildFlag).Width = 0
                .Cols(COL_ResultDateTimeUTC).Width = 0
                .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

                .Cols(COL_ConceptID).Width = 0
                .Cols(COL_ICD9).Width = 0
                .Cols(COL_ICD10).Width = 0
                .Cols(COL_LOINC).Width = 0
                .Cols(COL_PREFERREDLABID).Width = 0
                If usePreferredLabforIndividualTest = False Then
                    .Cols(COL_PREFERREDLAB).Width = 0
                End If
                .Cols(COL_PREFERREDLAB).AllowEditing = False
                .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Width = 0
                .Cols(COL_SPECIMEN_TYPE).Width = 0
                .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Width = 0
                .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Width = 0
                .Cols(COL_SPECIMEN_REJECT_REASON).Width = 0
                .Cols(COL_SPECIMEN_CONDITION).Width = 0
                .Cols(COL_SPECIMEN_ACTION_CODE).Width = 0
                .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Width = 0
                .Cols(COL_DATE_TIME_UTC).Width = 0
                .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Width = 0
                .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Width = 0
                .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Width = 0
            End If
        End With
        _Flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw 'added by manoj on 20121127 for trriger Grid OwnerDraw Event

    End Sub
    Private Sub DesignDignosisCPTGrid()
        SetGridStyle(c1DignosisCPTs)

        With c1DignosisCPTs
            .BeginUpdate()
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_DT_COUNT
            .Cols.Fixed = 0

            .SetData(0, COL_DT_ID, "ID")
            .SetData(0, COL_DT_SELECT, "Select")
            .SetData(0, COL_DT_CODE, "Code")
            .SetData(0, COL_DT_DESCRIPTON, "Description")
            Dim _Width As Single
            If .Width = 451 Then
                _Width = (((.Width * 2) + 80) - 20) / 5
            Else
                _Width = (.Width - 20) / 5
            End If

            .Cols(COL_DT_ID).Width = 0
            .Cols(COL_DT_SELECT).Width = _Width * 0.5
            .Cols(COL_DT_CODE).Width = _Width * 1
            .Cols(COL_DT_DESCRIPTON).Width = _Width * 3.5

            .Cols(COL_DT_ID).DataType = GetType(Int64)
            .Cols(COL_DT_SELECT).DataType = GetType(Boolean)
            .Cols(COL_DT_CODE).DataType = GetType(String)
            .Cols(COL_DT_DESCRIPTON).DataType = GetType(String)

            .Cols(0).AllowEditing = True
            .Cols(COL_DT_ID).AllowEditing = False
            .Cols(COL_DT_CODE).AllowEditing = False
            .Cols(COL_DT_DESCRIPTON).AllowEditing = False

            .EndUpdate()
        End With
        ' lblDgnCPTSearch.Text = "Search on Code"
    End Sub


    Private Sub DesignResultGrid()
        SetGridStyle(c1Results)

        With c1Results
            .BeginUpdate()
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT


            ''Infobutton
            .SetData(0, COL_LAB_INFOBUTTON, "")
            .Cols(COL_LAB_INFOBUTTON).Width = 22
            If isEducationMaterialEnables Then
                .Cols(COL_LAB_INFOBUTTON).Visible = True
            Else
                .Cols(COL_LAB_INFOBUTTON).Visible = False
            End If
            .Cols(COL_LAB_INFOBUTTON).AllowResizing = False
            .Cols(COL_LAB_INFOBUTTON).AllowEditing = True
            ''

            .SetData(0, COL_TEST_RESULT_SELECT, "Select")
            .SetData(0, COL_TEST_RESULT_NAME, "Name")
            .SetData(0, COL_TEST_RESULT_CODE, "Code")
            .SetData(0, COL_ORDERID, "Order ID")
            .SetData(0, COL_TESTID, "Test ID")
            .SetData(0, COL_TEST_LINENO, "Test Line No")
            .SetData(0, COL_RESULT_NUMBER, "Result Number")
            .SetData(0, COL_RESULT_LINENO, "Result Line No")
            .SetData(0, COL_RESULT_NAMEID, "Result Name ID")
            .SetData(0, COL_TEST_DIGNOSIS_RESULT_VALUE, "Value")
            .SetData(0, COL_TEST_DIGNOSISLIST, "Diagnosis List")
            .SetData(0, COL_TEST_DIGNOSISBTN, "DBtn")
            .SetData(0, COL_TEST_CPT_RESULT_UNIT, "Unit")
            .SetData(0, COL_TEST_CPTLIST, "Treatment List")
            .SetData(0, COL_TEST_CPTBTN, "TBtn")
            .SetData(0, COL_TEST_NOTE, "Note")
            .SetData(0, COL_TEST_SPECIMEN, "Specimen")
            .SetData(0, COL_TEST_COLLECTION, "Collection")
            .SetData(0, COL_TEST_STORAGE, "Storage")
            .SetData(0, COL_TEST_LOINC, "Lab Test Code")
            .SetData(0, COL_LAB_TEST_CODE, "LOINC Code")
            ' .SetData(0, COL_TEST_CPT, "CPT")
            .SetData(0, COL_LAB_SPEC_RANGE, "Instruction")
            .SetData(0, COL_ABNORMAL_FLAG, "Precuation")
            .SetData(0, COL_RESULT_TYPE, "Comments")
            .SetData(0, COL_RESULT_VALUE_TYPE, "Value Type")
            .SetData(0, COL_RESULT_RANGE, "Range")
            .SetData(0, COL_LAB_SPEC_RANGE, "Patient Range")
            .SetData(0, COL_ABNORMAL_FLAG, "Flag")
            .SetData(0, COL_RESULT_TYPE, "Result Type")
            .SetData(0, COL_RESULT_COMMENT, "R. Comment")
            .SetData(0, COL_TEST_RESULT_DATETIME, "R. DateTime")
            'Sanjog
            ' .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")
            .SetData(0, COL_TEST_RESULT_DATETIME, "Collected")
            'Sanjog
            'Mitesh
            '.SetData(0, COL_REPORTED_DATETIME, "Reported")
            'Mitesh
            .SetData(0, COL_TEST_RESULT_USERID, "R. UserID")
            .SetData(0, COL_RECORDTYPE, "Rcd. Type")
            .SetData(0, COL_ISFINISHED, "Finished")
            ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            .SetData(0, COL_ALTERNATE_RESULT_CODE, "Alternative result code")
            .SetData(0, COL_ALTERNATE_RESULT_NAME, "Alternative result Name")
            .SetData(0, COL_SPECIMEN_RECEIVED_DATETIME, "S.DateTime")
            .SetData(0, COL_RESULT_TRANSFER_DATETIME, "S.DateTime")
            .SetData(0, COL_ALTERNATE_TEST_CODE, "Alternative test code")
            .SetData(0, COL_ALTERNATE_TEST_NAME, "Alternative test name")
            'End Madan

            .SetData(0, COL_SPECIMENCOLLECTIONDATE, "Specimen Collection Date")

            ''Added by Abhijeet on 20101026
            .SetData(0, COL_LAB_FACILITY_NAME, "Lab Facility Name")
            .SetData(0, COL_LAB_FACILITY_STREET_ADDRESS, "Lab Facility Address")
            .SetData(0, COL_LAB_FACILITY_CITY, "Lab Facility City")
            .SetData(0, COL_LAB_FACILITY_STATE, "Lab Facility State")
            .SetData(0, COL_LAB_FACILITY_ZIP_CODE, "Lab Facility Zip Code")
            ''End of changes by Abhijeet on 20101026


            .SetData(0, COL_DMSID, "DMS ID")
            .SetData(0, COL_SCAN, "Scan")
            .SetData(0, COL_VIEW, "View")

            .SetData(0, COL_DMSIDCollection, "DMSID Collection")

            .Rows(0).Height = 22

            .Cols(COL_RESULT_TYPE).DataType = GetType(String)
            .Cols(COL_RESULT_TYPE).ComboList = _ResultTypeList

            '' 20070602 Added New Column 
            .Cols(COL_ABNORMAL_FLAG).DataType = GetType(String)
            .Cols(COL_ABNORMAL_FLAG).ComboList = _AbnormalFlagList
            '' 

            .Tree.Column = COL_TEST_RESULT_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15
            '.Cols(0).AllowEditing = False
            '.Cols(1).AllowEditing = False
            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False

            .Cols(COL_TEST_RESULT_SELECT).Visible = False
            .Cols(COL_TEST_RESULT_NAME).Visible = True
            .Cols(COL_LAB_INFOBUTTON).Visible = False
            .Cols(COL_TEST_RESULT_CODE).Visible = False
            .Cols(COL_ORDERID).Visible = False
            .Cols(COL_TESTID).Visible = False
            .Cols(COL_TEST_LINENO).Visible = False
            .Cols(COL_RESULT_NUMBER).Visible = False
            .Cols(COL_RESULT_LINENO).Visible = False
            .Cols(COL_RESULT_NAMEID).Visible = False
            .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Visible = True
            .Cols(COL_TEST_DIGNOSISLIST).Visible = False
            .Cols(COL_TEST_DIGNOSISBTN).Visible = False
            .Cols(COL_TEST_CPT_RESULT_UNIT).Visible = True
            .Cols(COL_TEST_CPTLIST).Visible = False
            .Cols(COL_TEST_CPTBTN).Visible = False
            .Cols(COL_TEST_NOTE).Visible = False
            .Cols(COL_TEST_SPECIMEN).Visible = False
            .Cols(COL_TEST_COLLECTION).Visible = False
            .Cols(COL_TEST_STORAGE).Visible = False
            .Cols(COL_RESULT_VALUE_TYPE).Visible = False
            .Cols(COL_RESULT_RANGE).Visible = True
            .Cols(COL_LAB_SPEC_RANGE).Visible = True
            .Cols(COL_RESULT_COMMENT).Visible = False
            .Cols(COL_TEST_INSTRUCTIONBTN).Visible = False
            .Cols(COL_ABNORMAL_FLAG).Visible = True
            .Cols(COL_TEST_PRECAUTIONBTN).Visible = False
            .Cols(COL_RESULT_TYPE).Visible = True
            .Cols(COL_TEST_COMMENTSBTN).Visible = False
            .Cols(COL_SCHEDULED_DATETIME).Visible = False
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Visible = False
            .Cols(COL_REPORTED_DATETIME).Visible = False
            .Cols(COL_TEST_RESULT_USERID).Visible = False
            .Cols(COL_RECORDTYPE).Visible = False
            ' .Cols(COL_ISFINISHED).Visible = True
            .Cols(COL_TEST_LOINC).Visible = True
            .Cols(COL_LAB_TEST_CODE).Visible = True
            .Cols(COL_TEST_CPT).Visible = False
            .Cols(COL_ISFINISHED).Visible = True
            .Cols(COL_DMSID).Visible = False
            .Cols(COL_SCAN).Visible = False
            .Cols(COL_VIEW).Visible = False
            .Cols(COL_TESTNAME).Visible = False
            .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Visible = False
            .Cols(COL_RESULT_TRANSFER_DATETIME).Visible = False
            .Cols(COL_ALTERNATE_TEST_NAME).Visible = False
            .Cols(COL_ALTERNATE_TEST_CODE).Visible = False
            .Cols(COL_ALTERNATE_RESULT_NAME).Visible = False
            .Cols(COL_ALTERNATE_RESULT_CODE).Visible = False
            .Cols(COL_PRODUCER_IDENTIFIER).Visible = False
            .Cols(COL_DICOMID).Visible = False
            .Cols(COL_TEST_STATUS).Visible = False
            .Cols(COL_SPECIMEN_SOURCE).Visible = False
            .Cols(COL_SPECIMEN_CONDITION_DISP).Visible = False
            .Cols(COL_LAB_FACILITY_NAME).Visible = False
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Visible = False
            .Cols(COL_LAB_FACILITY_CITY).Visible = False
            .Cols(COL_LAB_FACILITY_STATE).Visible = False
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Visible = False
            .Cols(COL_LAB_TEST_TYPE).Visible = False
            .Cols(COL_LAB_ORIGINAL_ABNORMAL_FLAG).Visible = False
            .Cols(COL_SPECIMENCOLLECTIONDATE).Visible = False
            .Cols(COL_TEST_RESULT_DATETIME).Visible = False
            .Cols(COL_TEST_WORDTEMPLATE).Visible = False
            .Cols(COL_DMSIDCollection).Visible = False
            .Cols(COL_TestResultDateTimeUTC).Visible = False
            .Cols(COL_SpecimenReceivedDateTimeUTC).Visible = False
            .Cols(COL_ResultTransferDateTimeUTC).Visible = False
            .Cols(COL_LabFacilityIdentifierTypeCode).Visible = False
            .Cols(COL_LabFacilityOrganizationIdentifier).Visible = False
            .Cols(COL_LabFacilityCountry).Visible = False
            .Cols(COL_LabFacilityCountyOrParishCode).Visible = False
            .Cols(COL_ResultCode).Visible = False
            .Cols(COL_ResultCodeType).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorLastName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Visible = False
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Visible = False
            .Cols(COL_ResultParentChildFlag).Visible = False
            .Cols(COL_ResultDateTimeUTC).Visible = False
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Visible = False
            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Visible = False
            .Cols(COL_SPECIMEN_TYPE).Visible = False
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Visible = False
            .Cols(COL_SPECIMEN_REJECT_REASON).Visible = False
            .Cols(COL_SPECIMEN_CONDITION).Visible = False
            .Cols(COL_SPECIMEN_ACTION_CODE).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Visible = False
            .Cols(COL_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Visible = False
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Visible = False
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Visible = False

            .Cols(COL_ConceptID).Visible = False
            .Cols(COL_ICD9).Visible = False
            .Cols(COL_ICD10).Visible = False
            .Cols(COL_LOINC).Visible = False
            .Cols(COL_PREFERREDLABID).Visible = False
            .Cols(COL_PREFERREDLAB).Visible = usePreferredLabforIndividualTest
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            Dim _Width As Single
            If .Width = 980 Then
                _Width = ((.Width / 2) - 25) / 7
            Else
                _Width = (.Width - 25) / 7
            End If
            'SANJOG
            .Cols(COL_SPECIMENCOLLECTIONDATE).Width = 0
            .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).Width = 0
            .Cols(COL_TEST_RESULT_DATETIME).Width = 0
            'Sanjog
            .Cols(COL_LAB_SPEC_RANGE).Width = 0
            .Cols(COL_TEST_RESULT_SELECT).Width = 0
            .Cols(COL_TEST_RESULT_NAME).Width = _Width * 1.5
            .Cols(COL_TEST_RESULT_CODE).Width = 0
            .Cols(COL_ORDERID).Width = 0
            .Cols(COL_TESTID).Width = 0
            .Cols(COL_TESTNAME).Width = 0
            .Cols(COL_TEST_LINENO).Width = 0
            .Cols(COL_RESULT_NUMBER).Width = 0
            .Cols(COL_RESULT_LINENO).Width = 0
            .Cols(COL_RESULT_NAMEID).Width = 0
            .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width = _Width * 0.8
            .Cols(COL_TEST_DIGNOSISLIST).Width = 0
            .Cols(COL_TEST_DIGNOSISBTN).Width = 0
            .Cols(COL_TEST_CPT_RESULT_UNIT).Width = _Width * 0.8
            .Cols(COL_TEST_CPTLIST).Width = 0
            .Cols(COL_TEST_CPTBTN).Width = 0
            .Cols(COL_TEST_NOTE).Width = 0
            .Cols(COL_TEST_SPECIMEN).Width = 0
            .Cols(COL_TEST_COLLECTION).Width = 0
            .Cols(COL_TEST_STORAGE).Width = 0
            .Cols(COL_TEST_LOINC).Width = _Width * 0.8
            .Cols(COL_LAB_TEST_CODE).Width = _Width * 0.8
            .Cols(COL_LAB_SPEC_RANGE).Width = 0
            .Cols(COL_TEST_INSTRUCTIONBTN).Width = 0
            .Cols(COL_ABNORMAL_FLAG).Width = 0
            .Cols(COL_TEST_PRECAUTIONBTN).Width = 0
            .Cols(COL_RESULT_TYPE).Width = 0
            .Cols(COL_TEST_COMMENTSBTN).Width = 0
            .Cols(COL_RESULT_VALUE_TYPE).Width = 0
            .Cols(COL_RESULT_RANGE).Width = _Width * 0.8
            .Cols(COL_RESULT_TYPE).Width = _Width * 1.2
            .Cols(COL_ABNORMAL_FLAG).Width = _Width * 1.2
            .Cols(COL_RESULT_COMMENT).Width = 0
            .Cols(COL_TEST_RESULT_DATETIME).Width = 0
            .Cols(COL_TEST_RESULT_USERID).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_ISFINISHED).Width = 100
            ' Added below Four fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            .Cols(COL_SPECIMEN_RECEIVED_DATETIME).Width = 0
            .Cols(COL_RESULT_TRANSFER_DATETIME).Width = 0
            .Cols(COL_ALTERNATE_TEST_CODE).Width = 0
            .Cols(COL_ALTERNATE_TEST_NAME).Width = 0
            .Cols(COL_ALTERNATE_RESULT_CODE).Width = 0
            .Cols(COL_ALTERNATE_RESULT_NAME).Width = 0
            'End madan
            .Cols(COL_DMSID).Width = 0
            .Cols(COL_SCAN).Width = 0
            .Cols(COL_VIEW).Width = 0
            .Cols(COL_DICOMID).Width = 0

            .Cols(COL_DMSIDCollection).Width = 0

            ''Added by Abhijeet on 20101026
            .Cols(COL_LAB_FACILITY_NAME).Width = 0
            .Cols(COL_LAB_FACILITY_STREET_ADDRESS).Width = 0
            .Cols(COL_LAB_FACILITY_CITY).Width = 0
            .Cols(COL_LAB_FACILITY_STATE).Width = 0
            .Cols(COL_LAB_FACILITY_ZIP_CODE).Width = 0
            .Cols(COL_TEST_WORDTEMPLATE).Width = 0
            ' .Cols(COL_TEST_CPT).Width = 100

            ''End of changes by Abhijeet on 20101026

            .SetData(0, COL_TestResultDateTimeUTC, "TestResultDateTimeUTC")
            .SetData(0, COL_SpecimenReceivedDateTimeUTC, "SpecimenReceivedDateTimeUTC")
            .SetData(0, COL_ResultTransferDateTimeUTC, "ResultTransferDateTimeUTC")
            .SetData(0, COL_LabFacilityIdentifierTypeCode, "LabFacilityIdentifierTypeCode")
            .SetData(0, COL_LabFacilityOrganizationIdentifier, "LabFacilityOrganizationIdentifier")
            .SetData(0, COL_LabFacilityCountry, "LabFacilityCountry")
            .SetData(0, COL_LabFacilityCountyOrParishCode, "LabFacilityCountyOrParishCode")
            .SetData(0, COL_ResultCode, "ResultCode")
            .SetData(0, COL_ResultCodeType, "ResultCodeType")
            .SetData(0, COL_LabFacilityMedicalDirectorIDNumber, "LabFacilityMedicalDirectorIDNumber")
            .SetData(0, COL_LabFacilityMedicalDirectorLastName, "LabFacilityMedicalDirectorLastName")
            .SetData(0, COL_LabFacilityMedicalDirectorMiddleName, "LabFacilityMedicalDirectorMiddleName")
            .SetData(0, COL_LabFacilityMedicalDirectorSuffix, "LabFacilityMedicalDirectorSuffix")
            .SetData(0, COL_LabFacilityMedicalDirectorPrefix, "LabFacilityMedicalDirectorPrefix")
            .SetData(0, COL_LabFacilityMedicalDirectorFirstName, "LabFacilityMedicalDirectorFirstName")
            .SetData(0, COL_ResultParentChildFlag, "ResultParentChildFlag")
            .SetData(0, COL_ResultDateTimeUTC, "ResultDateTimeUTC")
            .SetData(0, COL_TestSpecimenCollectionDateTimeUTC, "TestSpecimenCollectionDateTimeUTC")

            .SetData(0, COL_ConceptID, "ConceptID")
            .SetData(0, COL_ICD9, "ICD9")
            .SetData(0, COL_ICD10, "ICD10")
            .SetData(0, COL_LOINC, "LOINC")
            .SetData(0, COL_PREFERREDLABID, "Performing Lab ID")
            .SetData(0, COL_PREFERREDLAB, "Performing Lab")
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            .Cols(COL_TestResultDateTimeUTC).Width = 0
            .Cols(COL_SpecimenReceivedDateTimeUTC).Width = 0
            .Cols(COL_ResultTransferDateTimeUTC).Width = 0
            .Cols(COL_LabFacilityIdentifierTypeCode).Width = 0
            .Cols(COL_LabFacilityOrganizationIdentifier).Width = 0
            .Cols(COL_LabFacilityCountry).Width = 0
            .Cols(COL_LabFacilityCountyOrParishCode).Width = 0
            .Cols(COL_ResultCode).Width = 0
            .Cols(COL_ResultCodeType).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorIDNumber).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorLastName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorMiddleName).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorSuffix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorPrefix).Width = 0
            .Cols(COL_LabFacilityMedicalDirectorFirstName).Width = 0
            .Cols(COL_ResultParentChildFlag).Width = 0
            .Cols(COL_ResultDateTimeUTC).Width = 0
            .Cols(COL_TestSpecimenCollectionDateTimeUTC).Width = 0

            .Cols(COL_SPECIMEN_TYPE_IDENTIFIER).Width = 0
            .Cols(COL_SPECIMEN_TYPE).Width = 0
            .Cols(COL_SPECIMEN_TYPE_CODING_SYSTEM).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME).Width = 0
            .Cols(COL_SPECIMEN_REJECT_REASON).Width = 0
            .Cols(COL_SPECIMEN_CONDITION).Width = 0
            .Cols(COL_SPECIMEN_ACTION_CODE).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME).Width = 0
            .Cols(COL_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_DATE_TIME_UTC).Width = 0
            .Cols(COL_TEST_SCHEDULED_END_DATE_TIME_UTC).Width = 0
            .Cols(COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC).Width = 0

            .Cols(COL_ConceptID).Width = 0
            .Cols(COL_ICD9).Width = 0
            .Cols(COL_ICD10).Width = 0
            .Cols(COL_LOINC).Width = 0
            .Cols(COL_PREFERREDLABID).Width = 0
            If usePreferredLabforIndividualTest = False Then
                .Cols(COL_PREFERREDLAB).Width = 0
            End If
            .Cols(COL_PREFERREDLAB).AllowEditing = False
            ''Added by ABhijeet on 20101117
            .ExtendLastCol = True
            ''End of changes by ABhijeet on 20101117
            .EndUpdate()
        End With
    End Sub

    Private Function GetTestName(ByVal TestID As Int64) As String
        Dim _Result As String = ""
        Dim _gloEMRDBID As New gloEMRDatabase.DataBaseLayer
        _Result = _gloEMRDBID.GetRecord_Query("SELECT labtm_Name FROM Lab_Test_Mst WHERE labtm_ID = " & TestID & " ").Trim
        _gloEMRDBID.Dispose()
        _gloEMRDBID = Nothing
        Return _Result
    End Function

    Private Function GetTestCode(ByVal TestID As Int64) As String
        Dim _Result As String = ""
        Dim _gloEMRDBID As New gloEMRDatabase.DataBaseLayer
        _Result = _gloEMRDBID.GetRecord_Query("SELECT labtm_Code FROM Lab_Test_Mst WHERE labtm_ID = " & TestID & " ").Trim
        _gloEMRDBID.Dispose()
        _gloEMRDBID = Nothing
        Return _Result
    End Function
    Private Function GetEmdeonOrderStatus(_OrderID As Int64) As Boolean
        Dim _isDisable As Boolean = False
        Dim _OrderReferanceId As String = String.Empty
        Dim strQuery As String = String.Empty
        Dim dtResult As DataTable = Nothing
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_ConnectionString)
        Try
            objDbLayer.Connect(False)
            strQuery = "select labom_dgloLabOrderID,labom_gloLabOrderStatus from lab_order_Mst where labom_OrderID=" & _OrderID

            objDbLayer.Retrive_Query(strQuery, dtResult)
            If dtResult IsNot Nothing AndAlso dtResult.Rows.Count > 0 Then
                _OrderReferanceId = Convert.ToString(dtResult.Rows(0)("labom_dgloLabOrderID").ToString())
            Else
                _OrderReferanceId = String.Empty
            End If
            If Convert.ToString(_OrderReferanceId) <> "" Then
                _isDisable = True
            End If
            objDbLayer.Disconnect()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If objDbLayer IsNot Nothing Then
                objDbLayer.Dispose()
            End If
            If dtResult IsNot Nothing Then
                dtResult.Dispose()
                ' added for memory management  
                dtResult = Nothing
            End If
            strQuery = String.Empty

        End Try
        Return _isDisable
    End Function

    Public Function SetData(ByVal oOrder As gloEMRActors.LabActor.LabOrder, ByVal _strOrder As String) As Boolean
        If oOrder Is Nothing Then
            SetData = Nothing
            Exit Function
        End If

        _Flex.BeginUpdate()
        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        strOrderType = _strOrder.ToLower() 'assigning value to variable--- 

        _CurrentOrderID = oOrder.OrderID
        nPatientID = oOrder.PatientID
        'First Clear All the Grids
        Select Case strOrderType.ToLower()
            Case ("emr")
                ClearTest()
                _Flex.Cols(COL_SCHEDULED_DATETIME).AllowEditing = True
                _Flex.Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = True
                _Flex.Cols(COL_REPORTED_DATETIME).AllowEditing = True
                Exit Select
            Case ("emdeon")
                ClearTestEmdeon()
                _Flex.Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = False
                _Flex.Cols(COL_REPORTED_DATETIME).AllowEditing = False

                Exit Select
            Case Else
                Exit Select
        End Select

        'Madan---Added for saving orders in view orders form... 20100413
        If (IsNothing(dtURLDoc) = False) Then
            dtURLDoc.Dispose()
            dtURLDoc = Nothing
        End If
        dtURLDoc = Lab_GetURLDocument(_CurrentOrderID, 0)

        _IsLoading = True
        Dim items(oOrder.DiagnosisCount) As String
        Dim _itemcount As Int32 = 0

        With oOrder.OrderTests
            For nTest As Int16 = 0 To .Count - 1
                'Problem #939 Added Code to Populate particaular Dx and CPT against each test
                Dim _FillDXDesc As String = ""
                Dim _FillTreatment As String = ""

                'Labs Denormalization 20090321
                '_FillTestName = GetTestName(.Item(nTest).TestID)
                _FillTestName = .Item(nTest).TestName
                '---
                _FillTestCode = .Item(nTest).TestCode
                ' _FillTestCode = GetTestCode(.Item(nTest).TestID)
               
                If IsTestExists(.Item(nTest).OrderID, .Item(nTest).TestID, _FillTestName) = False Then
                    With .Item(nTest)
                        '//---Add Test---Start---//
                        _Flex.Rows.Add()

                        _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail
                        _FillTestLineNo = .TestLineNo
                        Dim intDMSId As Int64 = .DMSID
                        With _Flex.Rows(_Flex.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 22
                            .IsNode = True
                            .Style = _Flex.Styles("CS_Record")
                            .Node.Level = 0
                            If intDMSId = 0 Then

                                Dim dvURLDoc As DataView = New DataView(dtURLDoc)
                                Dim dtdvURLDoc As DataTable = Nothing

                                If Not IsNothing(dtURLDoc) Then
                                    dvURLDoc.RowFilter = "labotrda_TestID =" & Convert.ToInt64(oOrder.OrderTests.Item(nTest).TestID)
                                    dtdvURLDoc = dvURLDoc.ToTable
                                End If

                                If (IsNothing(dtdvURLDoc) = False) Then
                                    If dtdvURLDoc.Rows.Count > 0 Then
                                        .Node.Image = ImgAttachment.Image
                                    Else
                                        .Node.Image = ImgTest.Image
                                    End If

                                    dtdvURLDoc.Dispose()
                                    dtdvURLDoc = Nothing
                                Else
                                    .Node.Image = ImgTest.Image
                                End If
                                dvURLDoc.Dispose()
                                dvURLDoc = Nothing

                            Else
                                .Node.Image = ImgAttachment.Image
                            End If

                            .Node.Data = _FillTestName
                        End With

                        ''Infobutton
                        Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                        _Flex.SetCellImage(_Flex.Rows.Count - 1, COL_LAB_INFOBUTTON, imgFlag)
                        'If ParentControl = "" Then
                        '    _Flex.Cols(COL_LAB_INFOBUTTON).Visible = False
                        'End If
                        ''
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_CheckBox"))
                        '_Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                        ' _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_CPTBTN, _Flex.Styles("CS_ComboList"))

                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_COMMENTSBTN, _Flex.Styles("CS_ComboList"))
                        If ParentControl = "Record Result" Then
                            _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_RESULT_RANGE, _Flex.Styles("CS_ComboList"))
                        End If


                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_SELECT, True)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_CODE, _FillTestCode)
                        '' 20090212 Mahesh TO Add a Test NAme in The FlexGrid Column
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_NAME, _FillTestName)
                        ''
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, .OrderID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .TestID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTNAME, .TestName)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_LINENO, .TestLineNo)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_DMSID, .DMSID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_DICOMID, .DicomID)

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_DMSIDCollection, .DMSIDCollection)


                        ''Added by Abhijeet on 20101026
                        If IsDBNull(.TestStatus) OrElse (.TestStatus.ToString() = "") Then
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_STATUS, "Ordered")
                        Else
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_STATUS, .TestStatus)
                        End If
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_WORDTEMPLATE, .TestTemplate)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_CPT, .CPT)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_SOURCE, .SpecimenSource)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_CONDITION_DISP, .SpecimenConditionDisp)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_LAB_TEST_TYPE, .TestType)
                        If .Is_Finished = True Then
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_ISFINISHED, "Yes")
                        Else
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_ISFINISHED, "No")
                        End If
                        ''End of Changes by Abhijeet on 20101026

                        '/-Fill Diagnosis & CPTs-Start/
                        Dim _ComboList As String = ""
                        Dim _Code As String = "", _Description As String = ""
                        _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1


                        ''Changed by Mayuri: added multiple selection dropdown control-20160825
                        If Not .Diagonosis Is Nothing Then
                            Try
                                For d As Int16 = 0 To .Diagonosis.Count - 1
                                    _Code = .Diagonosis.Item(d).Code
                                    _Description = .Diagonosis.Item(d).Description
                                    If .Diagonosis.Item(d).ID = 1 Then
                                        _FillDXDesc = _FillDXDesc & _Code & "-" & _Description & "|"
                                    End If
                                    Dim _isExists As Boolean = False
                                    For Each _item As String In items
                                        If Not IsNothing(_item) Then
                                            If Convert.ToString(_item) <> "" Then
                                                If (_Code & "-" & _Description) = _item Then
                                                    _isExists = True
                                                    Exit For
                                                End If
                                            End If
                                        End If
                                    Next
                                    If _isExists = False Then

                                        items(_itemcount) = _Code & "-" & _Description
                                        _itemcount = _itemcount + 1
                                    End If
                                Next
                                If (_FillDXDesc <> "") Then
                                    _FillDXDesc = _FillDXDesc.Remove(_FillDXDesc.LastIndexOf("|"), 1)
                                End If
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                            End Try
                            Try

                                If Not IsNothing(c1DropDownControl1) Then
                                    If Not IsNothing(c1DropDownControl1.DropDownForm) Then
                                        c1DropDownControl1.DropDownForm.Close()
                                    End If
                                    c1DropDownControl1.Dispose()
                                    c1DropDownControl1 = Nothing
                                End If
                                If Not IsNothing(dropdown) Then
                                    dropdown.Dispose()
                                    dropdown = Nothing
                                End If
                                Dim _isdisable As Boolean = False
                                _isdisable = GetEmdeonOrderStatus(_CurrentOrderID)
                                c1DropDownControl1 = New C1.Win.C1Input.C1DropDownControl
                                dropdown = New gloUserControlLibrary.CheckedListBoxDroDown(items, _isdisable)
                                c1DropDownControl1.DropDownForm = dropdown
                                _Flex.Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Editor = c1DropDownControl1
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_DIGNOSISLIST, .Diagonosis)
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _FillDXDesc)

                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                            End Try

                        End If

                        _ComboList = "" : _Code = "" : _Description = ""
                        _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1

                        If Not .Treatments Is Nothing Then
                            For t As Int16 = 0 To .Treatments.Count - 1
                                _Code = .Treatments.Item(t).Code
                                _Description = .Treatments.Item(t).Description
                                _ComboList = _ComboList & "|" & _Code & "-" & _Description & "|"
                                'Problem #939 Added Code to Populate particaular Dx and CPT against each test
                                If .Treatments.Item(t).ID = 1 Then
                                    _FillTreatment = _Code & "-" & _Description
                                End If

                            Next

                            Dim csDgnCPTList As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
                            Try
                                If (_Flex.Styles.Contains("CS_DigCPT" & _DgnCPTRunTimeComboList)) Then
                                    csDgnCPTList = _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList)
                                Else
                                    csDgnCPTList = _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
                                    With csDgnCPTList
                                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                                        .ForeColor = Color.Black
                                        .BackColor = Color.GhostWhite
                                        .DataType = Type.GetType("System.String")
                                        .ComboList = _ComboList
                                    End With
                                End If
                            Catch ex As Exception
                                csDgnCPTList = _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
                                With csDgnCPTList
                                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                                    .ForeColor = Color.Black
                                    .BackColor = Color.GhostWhite
                                    .DataType = Type.GetType("System.String")
                                    .ComboList = _ComboList
                                End With
                            End Try


                            _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList))
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_CPTLIST, .Treatments)
                            'Problem #939 Added Code to Populate particaular Dx and CPT against each test
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _FillTreatment)
                        End If
                        'Fill_Diagnosis_CPT()
                        '/-Fill Diagnosis & CPTs-Finish/
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_NUMBER, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_LINENO, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_NAMEID, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_NOTE, .Note)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_SPECIMEN, .Specimen)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_COLLECTION, .Collection)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_STORAGE, .Storage)
                        'If .LOINCCode <> 0 Then ''Sudhir 20090227
                        '_Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_LOINC, .LOINCCode)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_LAB_TEST_CODE, .LOINCCode)
                        'End If
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_LAB_SPEC_RANGE, .Instruction)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_ABNORMAL_FLAG, .Precaution)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_TYPE, .Comments)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_RANGE, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_VALUE_TYPE, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_COMMENT, "")
                        ' _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_TYPE, "")
                        If IsDBNull(.TestDateTime) = False Then
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_DATETIME, .TestDateTime)
                        End If

                        If IsDBNull(.ScheduleDateTime) = False Then
                            If .ScheduleDateTime = "12:00:00 AM" Then
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SCHEDULED_DATETIME, Nothing)
                            Else
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SCHEDULED_DATETIME, .ScheduleDateTime)
                            End If

                        Else
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_SCHEDULED_DATETIME, Nothing)
                        End If

                        'sANJOG
                        If IsDBNull(.TestSpecimenCollectionDateTime) = False Then
                            If .TestSpecimenCollectionDateTime = "12:00:00 AM" Then
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Nothing)
                            Else
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_OR_TRANSFER_DATETIME, .TestSpecimenCollectionDateTime)
                            End If

                        Else
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_OR_TRANSFER_DATETIME, "")
                        End If
                        ''sANJOG
                        'Mitesh
                        If IsNothing(.ReportedDateTime) = False Then
                            If .ReportedDateTime = "12:00:00 AM" Then
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATETIME, Nothing)
                            Else
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATETIME, .ReportedDateTime)
                            End If

                        Else
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATETIME, Nothing)
                        End If
                        '--
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_USERID, .UserID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_TYPE_IDENTIFIER, .SpecimenTypeIdentifier)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_TYPE, .SpecimenTypeText)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_TYPE_CODING_SYSTEM, .SpecimenTypeCodingSystem)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_COLLECTION_START_DATE_TIME, .SpecimenCollectionStartDateTime)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_REJECT_REASON, .SpecimenRejectReason)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_CONDITION, .SpecimenCondition)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_ACTION_CODE, .SpecimenActionCode)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_SCHEDULED_END_DATE_TIME, .TestScheduledEndDateTime)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE_TIME_UTC, .labotd_DateTimeUTC)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_SCHEDULED_DATE_TIME_UTC, .labotd_TestScheduledDateTimeUTC)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_SCHEDULED_END_DATE_TIME_UTC, .labotd_TestScheduledEndDateTimeUTC)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC, .labotd_SpecimenCollectionStartDateTimeUTC)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_PREFERREDLABID, .TestPreferredLabID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_PREFERREDLAB, .TestPreferredLab)

                        '//---Add Test---Finish---//

                        '// If _TransactionType = enumTransactionType.LabResult Then

                        Dim nStyleRow As Int16 = 0
                        Dim nNodeRow As Int16 = 0
                        Dim i As Int16 = 0
                        Dim TransferDate As String = ""
                        For nResults As Int16 = 0 To .OrderTestResults.Count - 1
                            With .OrderTestResults.Item(nResults)
                                '//---Add Test Result Header---Start---//
                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .TestResultName, .TestID & .TestResultNumber, ImgResultHeader_Flask.Image)
                                nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                _FillTestResultsNodeIndex = nStyleRow
                                '---------------------------------------------------------
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_NAME, .TestResultName)
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_CODE, "Code")
                                _Flex.SetData(nStyleRow, COL_ORDERID, .OrderID)
                                _Flex.SetData(nStyleRow, COL_TESTID, .TestID)
                                _Flex.SetData(nStyleRow, COL_TESTNAME, .TestName)
                                _Flex.SetData(nStyleRow, COL_TEST_LINENO, _FillTestLineNo)
                                _Flex.SetData(nStyleRow, COL_RESULT_NUMBER, .TestResultNumber)
                                _Flex.SetData(nStyleRow, COL_RESULT_LINENO, 0)
                                _Flex.SetData(nStyleRow, COL_RESULT_NAMEID, 0)
                                _Flex.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "Value")
                                _Flex.SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                                _Flex.SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                                _Flex.SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, "Unit")
                                _Flex.SetData(nStyleRow, COL_TEST_CPTLIST, "")
                                _Flex.SetData(nStyleRow, COL_TEST_CPTBTN, "")
                                _Flex.SetData(nStyleRow, COL_TEST_NOTE, "Note")
                                _Flex.SetData(nStyleRow, COL_TEST_SPECIMEN, "Specimen")
                                _Flex.SetData(nStyleRow, COL_TEST_COLLECTION, "Collection")
                                _Flex.SetData(nStyleRow, COL_TEST_STORAGE, "Storage")

                                ''Added by ABhijeet on 20101116
                                ''_Flex.SetData(nStyleRow, COL_TEST_LOINC, "LOINC Code")
                                _Flex.SetData(nStyleRow, COL_TEST_LOINC, "")

                                ''End of changes by ABhijeet on 20101116

                                _Flex.SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                _Flex.SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "")
                                _Flex.SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                _Flex.SetData(nStyleRow, COL_RESULT_RANGE, "Normal Range")
                                _Flex.SetData(nStyleRow, COL_LAB_SPEC_RANGE, "Patient Specific Range")
                                _Flex.SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "Value Type")
                                _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, "Flag")
                                _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "Result Type")
                                _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, "R. Comment")
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_DATETIME, .TestResultDateTime)

                                '_Flex.SetData(nStyleRow, COL_TEST_RESULT_DATETIME, .ResultTransferDateTime)
                                'Sanjog
                                TransferDate = ""
                                TransferDate = .ResultTransferDateTime
                                '_Flex.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, .ResultTransferDateTime)
                                'Sanjog
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_USERID, "R. UserID")
                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                _Flex.SetData(nStyleRow, COL_ISFINISHED, .IsFinished)
                                _Flex.SetData(nStyleRow, COL_DMSID, .DMSID)

                                _Flex.SetData(nStyleRow, COL_DMSIDCollection, .DMSIDCollection)

                                'Madan--Added on--20100409
                                _Flex.SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, .SpecimenReceivedDateTime)
                                _Flex.SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, .ResultTransferDateTime)
                                _Flex.SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, .AlternateTestCode)
                                _Flex.SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, .AlternateTestName)

                                'END Madan
                                _Flex.SetData(nStyleRow, COL_TestResultDateTimeUTC, .TestResultDateTimeUTC)
                                _Flex.SetData(nStyleRow, COL_SpecimenReceivedDateTimeUTC, .SpecimenReceivedDateTimeUTC)
                                _Flex.SetData(nStyleRow, COL_ResultTransferDateTimeUTC, .ResultTransferDateTimeUTC)


                                '---------------------------------------------------------

                                With _Flex.Rows(nStyleRow)
                                    .ImageAndText = False
                                    .Height = 22
                                    .Style = _Flex.Styles("CS_Header")
                                    .AllowEditing = False
                                End With
                                _Flex.SetCellStyle(nStyleRow, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_Record"))



                                '//---Add Test Result Header---Finish---//

                                For nResult As Int16 = 0 To .TestResultDetails.Count - 1

                                    With .TestResultDetails.Item(nResult)

                                        '//---Add Test Result Detail---Start---//
                                        '_Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult.Image)

                                        If IsNothing(.ResultComment) = False Then
                                            If .ResultComment.Trim <> "" Then
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult_Comment.Image)
                                            Else
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult_Flask.Image)
                                            End If
                                        Else
                                            _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult_Flask.Image)
                                        End If

                                        nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        '---------------------------------------------------------
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_NAME, .ResultName)
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                                        _Flex.SetData(nStyleRow, COL_ORDERID, .OrderID)
                                        _Flex.SetData(nStyleRow, COL_TESTID, .TestID)
                                        'Labs Denormalization 20090321
                                        _Flex.SetData(nStyleRow, COL_TESTNAME, .TestName)
                                        '---
                                        '_Flex.SetData(nStyleRow, COL_TESTID, .TestName) ''Commented By Sudhir 20090227 ''Since It is OverWriting TestID 
                                        _Flex.SetData(nStyleRow, COL_TEST_LINENO, _FillTestLineNo)
                                        _Flex.SetData(nStyleRow, COL_RESULT_NUMBER, .TestResultNumber)
                                        _Flex.SetData(nStyleRow, COL_RESULT_LINENO, .ResultLineNo)
                                        _Flex.SetData(nStyleRow, COL_RESULT_NAMEID, .ResultNameID)
                                        '_Flex.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, .ResultValue) commented by manoj on 20121127
                                        'start of added by manoj on 20121127 for making result value as hyperlink
                                        If gloGlobal.gloLabGenral.IsResultisHyperLink(.ResultValue) Then
                                            _Flex.Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Item(nStyleRow) = New Hyperlink(.ResultValue.ToString())
                                        Else
                                            _Flex.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, .ResultValue)
                                        End If
                                        'end of added by mnaoj on 20121127 for making result value as hyperlink
                                        _Flex.SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, .ResultUnit)
                                        _Flex.SetData(nStyleRow, COL_TEST_CPTLIST, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_CPTBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_NOTE, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_COLLECTION, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_STORAGE, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_LOINC, .ResultLOINCID)
                                        _Flex.SetData(nStyleRow, COL_LAB_TEST_CODE, .AlternateResultCode)
                                        _Flex.SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                        _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                        _Flex.SetData(nStyleRow, COL_RESULT_RANGE, .ResultRange)
                                        _Flex.SetData(nStyleRow, COL_RESULT_VALUE_TYPE, Nothing) ' Remark its remaing
                                        _Flex.SetData(nStyleRow, COL_LAB_SPEC_RANGE, .PatientSpecificRange)
                                        _Flex.SetData(nStyleRow, COL_ISFINISHED, .IsFinished)
                                        'Sanjog
                                        If .TestSpecimenCollectionDate = "1/1/1900" Then
                                            .TestSpecimenCollectionDate = Nothing
                                        End If
                                        _Flex.SetData(nStyleRow, COL_SPECIMENCOLLECTIONDATE, .TestSpecimenCollectionDate)
                                        'Sanjog
                                        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, _ObservationStatus_COL.GetDescription(.ResultTypeCode))

                                        ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                        ''_Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(.AbnormalFlagCode))

                                        '_Flex.SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, .AbnormalFlagCode)
                                        'Dim strFlagList As String()
                                        'Dim strFlagInDB As String = .AbnormalFlagCode
                                        'strFlagList = strFlagInDB.Split(",")
                                        'If strFlagList.Length > 1 Then
                                        '    If glogeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                        '        _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                        '    Else
                                        '        _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                        '    End If
                                        'Else
                                        '    _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(.AbnormalFlagCode))
                                        'End If

                                        _Flex.SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, .AbnormalFlagCode)
                                        Dim strFlagList As String()
                                        Dim strFlagInDB As String = .AbnormalFlagCode
                                        strFlagList = strFlagInDB.Split(",")
                                        If strFlagList.Length > 1 Then '' Found two flags
                                            '' Bind to Invisible column value of Naomral range flag

                                            '' User Setting Turn ON and Off with "Close" instead  "Save and Close" this scenario will hadle to show flag
                                            If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, strFlagList(0))
                                                _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                            End If
                                        Else ' Only one flag which not realted lab specific range .
                                            _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(.AbnormalFlagCode))
                                        End If


                                        ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                        'Select Case .ResultType
                                        '    Case gloEMRActors.LabActor.enumTestResultReadType.None
                                        '        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "None")
                                        '    Case gloEMRActors.LabActor.enumTestResultReadType.Prilimnary
                                        '        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "Prilimnary")
                                        '    Case gloEMRActors.LabActor.enumTestResultReadType.Final
                                        '        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "Final")
                                        '    Case gloEMRActors.LabActor.enumTestResultReadType.Ammend
                                        '        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "Ammend")
                                        'End Select
                                        _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, .ResultComment)
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_DATETIME, .ResultDateTime)
                                        'SaNJOG

                                        'Commented Only Show on Test Row 
                                        'If TransferDate = "12:00:00 AM" Then
                                        '    _Flex.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, "")
                                        'Else
                                        '    _Flex.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, TransferDate)
                                        'End If

                                        'SAnjog
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_USERID, .UserID) '// Remaining
                                        _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                                        _Flex.SetData(nStyleRow, COL_ISFINISHED, .IsFinished)
                                        ''Madan--Added on--20100409
                                        _Flex.SetData(nStyleRow, COL_ALTERNATE_RESULT_CODE, .AlternateResultCode)
                                        _Flex.SetData(nStyleRow, COL_ALTERNATE_RESULT_NAME, .AlternateResultName)
                                        _Flex.SetData(nStyleRow, COL_PRODUCER_IDENTIFIER, .ProducerIdentifier)
                                        ''END Madan
                                        '---------------------------------------------------------

                                        ''Added by Abhijeet on 20101026 for CCHIT certification requirement fields
                                        _Flex.SetData(nStyleRow, COL_LAB_FACILITY_NAME, .LabFacilityName)
                                        _Flex.SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, .LabFacilityStreetAddress)
                                        _Flex.SetData(nStyleRow, COL_LAB_FACILITY_CITY, .LabFacilityCity)
                                        _Flex.SetData(nStyleRow, COL_LAB_FACILITY_STATE, .LabFacilityState)
                                        _Flex.SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, .LabFacilityZipCode)
                                        ''End of changes by Abhijeet on 20101026 for CCHit certification requirement fields

                                        _Flex.SetData(nStyleRow, COL_LabFacilityIdentifierTypeCode, .LabFacilityIdentifierTypeCode)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityOrganizationIdentifier, .LabFacilityOrganizationIdentifier)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityCountry, .LabFacilityCountry)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityCountyOrParishCode, .LabFacilityCountyOrParishCode)
                                        _Flex.SetData(nStyleRow, COL_ResultCode, .ResultCode)
                                        _Flex.SetData(nStyleRow, COL_ResultCodeType, .ResultCodeType)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorIDNumber, .LabFacilityMedicalDirectorIDNumber)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorLastName, .LabFacilityMedicalDirectorLastName)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorMiddleName, .LabFacilityMedicalDirectorMiddleName)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorSuffix, .LabFacilityMedicalDirectorSuffix)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorPrefix, .LabFacilityMedicalDirectorPrefix)
                                        _Flex.SetData(nStyleRow, COL_LabFacilityMedicalDirectorFirstName, .LabFacilityMedicalDirectorFirstName)
                                        _Flex.SetData(nStyleRow, COL_ResultParentChildFlag, .ResultParentChildFlag)
                                        _Flex.SetData(nStyleRow, COL_ResultDateTimeUTC, .ResultDateTimeUTC)
                                        _Flex.SetData(nStyleRow, COL_TestSpecimenCollectionDateTimeUTC, .TestSpecimenCollectionDateTimeUTC)

                                        _Flex.SetData(nStyleRow, COL_ConceptID, .LabResultConceptID)
                                        _Flex.SetData(nStyleRow, COL_ICD9, .LabResultICD9)
                                        _Flex.SetData(nStyleRow, COL_ICD10, .LabResultICD10)
                                        _Flex.SetData(nStyleRow, COL_LOINC, .LabResultLOINC)

                                        With _Flex.Rows(nStyleRow)
                                            .ImageAndText = False
                                            .Height = 22
                                            'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                            'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                            'If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG))) Then
                                            If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG))) Then
                                                'Noraml and nothing
                                                .Style = _Flex.Styles("CS_Record")
                                            Else
                                                .Style = _Flex.Styles("CS_NotNormal")
                                                ' '' Not Normal 
                                            End If

                                            .AllowEditing = False
                                        End With
                                        ''Infobutton
                                        Dim imgFlag1 As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                                        _Flex.SetCellImage(nStyleRow, COL_LAB_INFOBUTTON, imgFlag1)

                                        '//---Add Test Result Detail---Finish---//

                                    End With 'With .TestResultDetails.Item(nResult)

                                Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1

                            End With ' With .OrderTestResults.Item(nResults)

                        Next ' For nResults As Int16 = 0 To .OrderTestResults.Count - 1

                        '// End If ' If _TransactionType = enumTransactionType.LabResult Then

                    End With ' With .Item(nTest)
                End If ' If IsTestExists(OrderID, TestID, TestName) = False Then
            Next ' For nTest As Int16 = 0 To .Count - 1
        End With


        If _Flex.Rows.Count >= 2 Then
            _Flex.Row = 1
            _Flex.Select(1, COL_TEST_RESULT_SELECT)
        End If
        _Flex.EndUpdate()
        _IsLoading = False
        Return True
    End Function

    Public Function GetData() As gloEMRActors.LabActor.OrderTests
        Dim oOrderTests As New gloEMRActors.LabActor.OrderTests
        Dim oCellRange As C1.Win.C1FlexGrid.CellRange

        If _Flex.Rows.Count > 1 Then
            '---Fill Tests Start---
            Dim oOrderTest As gloEMRActors.LabActor.OrderTest
            'sarika Labs Denormalization 20090323
            '  arrTestID = New ArrayList
            arrTestNames = New ArrayList
            '----
            _Flex.FinishEditing()
            For nTest As Int16 = 1 To _Flex.Rows.Count - 1
                If Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                    oOrderTest = New gloEMRActors.LabActor.OrderTest
                    With oOrderTest

                        Dim dtcqm As DataTable = Get_CQMConceptIDetails(nPatientID, _CurrentOrderID, Convert.ToInt64(_Flex.GetData(nTest, COL_TESTID)))
                        If dtcqm.Rows.Count > 0 Then
                            .ValueSetID = dtcqm.Rows(0)("sValueSetOID")
                            .ValueSetName = dtcqm.Rows(0)("sValueSetName")
                        End If
                        If Not IsNothing(dtcqm) Then
                            dtcqm.Dispose()
                            dtcqm = Nothing
                        End If
                        Dim dtconceptID As DataTable = Get_CQMConceptIDetails(nPatientID, _CurrentOrderID, Convert.ToInt64(_Flex.GetData(nTest, COL_TESTID)))
                        If dtconceptID.Rows.Count > 0 Then
                            .ConceptID = dtconceptID.Rows(0)("sConceptID")
                            .ConceptIDDescription = dtconceptID.Rows(0)("sDescriptionID")
                        End If
                        If Not IsNothing(dtconceptID) Then
                            dtconceptID.Dispose()
                            dtconceptID = Nothing
                        End If
                        .OrderID = _Flex.GetData(nTest, COL_ORDERID)
                        .TestID = _Flex.GetData(nTest, COL_TESTID)
                        .TestName = _Flex.GetData(nTest, COL_TESTNAME)
                        If Convert.ToString(_Flex.GetData(nTest, COL_ISFINISHED)) = "Yes" Then
                            .Is_Finished = True
                        Else
                            .Is_Finished = False
                        End If

                        If _Flex.GetCellCheck(nTest, COL_TEST_RESULT_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            'sarika Labs Denormalization 20090323
                            '                            arrTestID.Add(_Flex.GetData(nTest, COL_TESTID))
                            arrTestNames.Add(_Flex.GetData(nTest, COL_TESTNAME))
                            '---
                        End If
                        .TestLineNo = _Flex.GetData(nTest, COL_TEST_LINENO)
                        Try

                            ''Added by Mayuri : to update diagnosis type as 1 or 0

                            ''Add if selected diagnosis is not present in collection
                            If Not _Flex.GetData(nTest, COL_TEST_DIGNOSIS_RESULT_VALUE) Is Nothing Then
                                Dim oDgnColl As gloEMRActors.LabActor.ItemDetails = CType(_Flex.GetData(nTest, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
                                Dim sDxCode As String = _Flex.GetData(nTest, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString
                                Dim strtoadd As String = ""
                                Dim _isExisits As Boolean = False
                                Dim dt As DataTable = Nothing
                                Dim cls As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder()
                                If sDxCode <> "" Then
                                    Dim DxCode() As String = Nothing
                                    DxCode = sDxCode.Split("|")
                                    If Not IsNothing(DxCode) Then
                                        If DxCode.Length >= 1 Then
                                            For Each Str As String In DxCode
                                                _isExisits = False
                                                strtoadd = Str
                                                For k As Integer = 0 To oDgnColl.Count - 1

                                                    If oDgnColl.Item(k).Code = Str.Substring(0, Str.IndexOf("-")).Trim Then
                                                        _isExisits = True
                                                        Exit For
                                                    End If
                                                Next
                                                ''if diagnosis is not present in collection then add that into collection
                                                If _isExisits = False Then
                                                    If strtoadd <> "" Then
                                                        Dim _Dignos As New gloEMRActors.LabActor.ItemDetail ''for Diagnosis
                                                        With _Dignos
                                                            dt = cls.GetSelectedICDDetails(_CurrentOrderID, oOrderTest.TestID, strtoadd.Substring(0, strtoadd.IndexOf("-")).Trim)
                                                            If Not IsNothing(dt) Then
                                                                If dt.Rows.Count > 0 Then
                                                                    .Code = dt.Rows(0)("labodtl_Code")
                                                                    .Description = dt.Rows(0)("labodtl_Description")
                                                                    .IcdRevision = dt.Rows(0)("nICDRevision")
                                                                    .ID = 1
                                                                    oDgnColl.Add(_Dignos)
                                                                    _Dignos = Nothing
                                                                End If
                                                            End If
                                                        End With
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                End If
                                If Not IsNothing(dt) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                                If Not IsNothing(cls) Then
                                    cls.Dispose()
                                    cls = Nothing
                                End If
                                For m As Integer = 0 To oDgnColl.Count - 1
                                    If sDxCode.Contains(oDgnColl.Item(m).Code) Then
                                        oDgnColl.Item(m).ID = 1
                                        oOrderTest.Diagonosis.Add(oDgnColl.Item(m))
                                    Else
                                        oDgnColl.Item(m).ID = 0
                                        oOrderTest.Diagonosis.Add(oDgnColl.Item(m))
                                    End If
                                Next
                            End If

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                        End Try

                        If Not _Flex.GetData(nTest, COL_TEST_CPTLIST) Is Nothing Then
                            Dim oCPTColl As gloEMRActors.LabActor.ItemDetails = CType(_Flex.GetData(nTest, COL_TEST_CPTLIST), gloEMRActors.LabActor.ItemDetails)
                            For i As Int16 = 0 To oCPTColl.Count - 1
                                'Problem #939 Added Code to Populate particaular Dx and CPT against each test
                                If Not _Flex.GetData(nTest, COL_TEST_CPT_RESULT_UNIT) Is Nothing Then
                                    Dim sTreatmentCode As String = _Flex.GetData(nTest, COL_TEST_CPT_RESULT_UNIT).ToString
                                    If sTreatmentCode <> "" Then
                                        If oCPTColl.Item(i).Code = sTreatmentCode.Substring(0, sTreatmentCode.IndexOf("-")) Then
                                            oCPTColl.Item(i).ID = 1
                                        Else
                                            oCPTColl.Item(i).ID = 0
                                        End If
                                    End If
                                Else
                                    oCPTColl.Item(i).ID = 0
                                End If

                                .Treatments.Add(oCPTColl.Item(i))
                            Next
                            oCPTColl = Nothing
                        End If

                        .Note = _Flex.GetData(nTest, COL_TEST_NOTE)
                        .SpecimenName = _Flex.GetData(nTest, COL_TEST_SPECIMEN)

                        .CollectionName = _Flex.GetData(nTest, COL_TEST_COLLECTION)
                        .StorageName = _Flex.GetData(nTest, COL_TEST_STORAGE)
                        ''Commented Sandip Darade 20090403
                        'If Val(_Flex.GetData(nTest, COL_TEST_LOINC) & "") > 0 Then
                        '    .LOINCCode = _Flex.GetData(nTest, COL_TEST_LOINC) & ""
                        'Else
                        '    .LOINCCode = 0
                        'End If

                        ''Sandip Darade 20090403
                        ''Loiniccode as string 
                        '.LOINCCode = _Flex.GetData(nTest, COL_TEST_LOINC)
                        .LOINCCode = _Flex.GetData(nTest, COL_LAB_TEST_CODE)
                        .CPT = _Flex.GetData(nTest, COL_TEST_CPT)
                        .Instruction = _Flex.GetData(nTest, COL_LAB_SPEC_RANGE)
                        .Precaution = _Flex.GetData(nTest, COL_ABNORMAL_FLAG)
                        .Comments = _Flex.GetData(nTest, COL_RESULT_TYPE)
                        .TestDateTime = _Flex.GetData(nTest, COL_TEST_RESULT_DATETIME)
                        .ScheduleDateTime = _Flex.GetData(nTest, COL_SCHEDULED_DATETIME)




                        'Mitesh
                        .TestSpecimenCollectionDateTime = _Flex.GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME)
                        .ReportedDateTime = _Flex.GetData(nTest, COL_REPORTED_DATETIME)
                        '--------x---
                        .UserID = _Flex.GetData(nTest, COL_TEST_RESULT_USERID)
                        .DMSID = _Flex.GetData(nTest, COL_DMSID)
                        .DMSID = 0
                        If Not _Flex.GetData(nTest, COL_DMSID) Is Nothing Then
                            If Not _Flex.GetData(nTest, COL_DMSID).ToString() = "" Then
                                .DMSID = Convert.ToInt64(_Flex.GetData(nTest, COL_DMSID).ToString())
                            End If
                        End If

                        'DMSIDCollection
                        If Not _Flex.GetData(nTest, COL_DMSIDCollection) Is Nothing Then
                            If Not _Flex.GetData(nTest, COL_DMSIDCollection).ToString() = "" Then
                                .DMSIDCollection = _Flex.GetData(nTest, COL_DMSIDCollection).ToString()
                            End If
                        End If

                        If IsNothing(dtURLDoc) = True Then
                            dtURLDoc = Lab_GetURLDocument(_CurrentOrderID, 0)
                        End If

                        If IsNothing(dtURLDoc) = False Then
                            .LabURLDocument = dtURLDoc
                        End If


                        'ADded by madan.. on 2010007
                        If Not _Flex.GetData(nTest, COL_DICOMID) Is Nothing Then
                            If Not _Flex.GetData(nTest, COL_DICOMID).ToString() = "" Then
                                .DicomID = Convert.ToInt64(_Flex.GetData(nTest, COL_DICOMID).ToString())
                            End If
                        End If
                        'end by madan.. on 2010007

                        ''Added by Abhijeet on 20101026
                        .TestStatus = _Flex.GetData(nTest, COL_TEST_STATUS)
                        Try

                            If Convert.ToString(_Flex.GetData(nTest, COL_TEST_WORDTEMPLATE)) <> "" Then
                                .TestTemplate = _Flex.GetData(nTest, COL_TEST_WORDTEMPLATE) ' Encoding.ASCII.GetBytes(Convert.ToString(_Flex.GetData(nTest, COL_TEST_WORDTEMPLATE)))
                            Else
                                .TestTemplate = Nothing
                            End If

                        Catch ex As Exception

                        End Try
                        .SpecimenSource = _Flex.GetData(nTest, COL_SPECIMEN_SOURCE)
                        .SpecimenConditionDisp = _Flex.GetData(nTest, COL_SPECIMEN_CONDITION_DISP)
                        .TestType = _Flex.GetData(nTest, COL_LAB_TEST_TYPE)
                        ''End of changes by Abhijeet on 20101026

                        .SpecimenTypeIdentifier = _Flex.GetData(nTest, COL_SPECIMEN_TYPE_IDENTIFIER)
                        .SpecimenTypeText = _Flex.GetData(nTest, COL_SPECIMEN_TYPE)
                        .SpecimenTypeCodingSystem = _Flex.GetData(nTest, COL_SPECIMEN_TYPE_CODING_SYSTEM)
                        .SpecimenCollectionStartDateTime = _Flex.GetData(nTest, COL_SPECIMEN_COLLECTION_START_DATE_TIME)
                        .SpecimenRejectReason = _Flex.GetData(nTest, COL_SPECIMEN_REJECT_REASON)
                        .SpecimenCondition = _Flex.GetData(nTest, COL_SPECIMEN_CONDITION)
                        .SpecimenActionCode = _Flex.GetData(nTest, COL_SPECIMEN_ACTION_CODE)
                        .TestScheduledEndDateTime = _Flex.GetData(nTest, COL_TEST_SCHEDULED_END_DATE_TIME)
                        .labotd_DateTimeUTC = _Flex.GetData(nTest, COL_DATE_TIME_UTC)
                        .labotd_TestScheduledDateTimeUTC = _Flex.GetData(nTest, COL_TEST_SCHEDULED_DATE_TIME_UTC)
                        .labotd_TestScheduledEndDateTimeUTC = _Flex.GetData(nTest, COL_TEST_SCHEDULED_END_DATE_TIME_UTC)
                        .labotd_SpecimenCollectionStartDateTimeUTC = _Flex.GetData(nTest, COL_SPECIMEN_COLLECTION_START_DATE_TIME_UTC)
                        .TestPreferredLabID = _Flex.GetData(nTest, COL_PREFERREDLABID)
                        .TestPreferredLab = _Flex.GetData(nTest, COL_PREFERREDLAB)


                        '---Fill Test Result Start---
                        oCellRange = _Flex.Rows(nTest).Node.GetCellRange
                        Dim _ResultStart As Int16 = oCellRange.r1 + 1
                        Dim _ResultEnd As Int16 = oCellRange.r2
                        oCellRange = Nothing

                        Dim oTestResult As gloEMRActors.LabActor.OrderTestResult
                        For nResult As Int16 = _ResultStart To _ResultEnd
                            If Val(_Flex.GetData(nResult, COL_RECORDTYPE) & "") = enumRecordType.ResultHeader Then
                                oTestResult = New gloEMRActors.LabActor.OrderTestResult
                                With oTestResult
                                    .OrderID = _Flex.GetData(nResult, COL_ORDERID)
                                    .TestID = _Flex.GetData(nResult, COL_TESTID)
                                    .TestName = _Flex.GetData(nResult, COL_TESTNAME)
                                    .TestResultNumber = _Flex.GetData(nResult, COL_RESULT_NUMBER)
                                    .TestResultName = _Flex.GetData(nResult, COL_TEST_RESULT_NAME)
                                    .TestResultDateTime = _Flex.GetData(nResult, COL_TEST_RESULT_DATETIME)
                                    .IsFinished = _Flex.GetData(nResult, COL_ISFINISHED)
                                    ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                                    'Added by madan-- on 20100409...
                                    .SpecimenReceivedDateTime = _Flex.GetData(nResult, COL_SPECIMEN_RECEIVED_DATETIME)
                                    If _Flex.GetData(nResult, COL_RESULT_TRANSFER_DATETIME).ToString() = "1/1/0001 12:00:00 AM" OrElse _Flex.GetData(nResult, COL_RESULT_TRANSFER_DATETIME).ToString() = "" Or _Flex.GetData(nResult, COL_RESULT_TRANSFER_DATETIME).ToString() = "12:00:00 AM" Then
                                        .ResultTransferDateTime = Nothing
                                    Else
                                        .ResultTransferDateTime = _Flex.GetData(nResult, COL_RESULT_TRANSFER_DATETIME)
                                    End If

                                    If IsNothing(_Flex.GetData(nTest, COL_REPORTED_DATETIME)) = False Then
                                        If _Flex.GetData(nTest, COL_REPORTED_DATETIME).ToString() = "1/1/0001 12:00:00 AM" OrElse _Flex.GetData(nTest, COL_REPORTED_DATETIME).ToString() = "" Or _Flex.GetData(nTest, COL_REPORTED_DATETIME).ToString() = "12:00:00 AM" Then
                                            .ReportedDateTime = Nothing
                                        Else
                                            .ReportedDateTime = _Flex.GetData(nTest, COL_REPORTED_DATETIME)
                                        End If
                                    Else
                                        .ReportedDateTime = Nothing
                                    End If
                                    .AlternateTestCode = _Flex.GetData(nResult, COL_ALTERNATE_TEST_CODE)
                                    .AlternateTestName = _Flex.GetData(nResult, COL_ALTERNATE_TEST_NAME)
                                    'End Madan

                                    .DMSID = 0
                                    If Not _Flex.GetData(nResult, COL_DMSID) Is Nothing Then
                                        If Not _Flex.GetData(nResult, COL_DMSID).ToString() = "" Then
                                            .DMSID = Convert.ToInt64(_Flex.GetData(nResult, COL_DMSID).ToString())
                                        End If
                                    End If

                                    'DMSIDCollection
                                    If Not _Flex.GetData(nResult, COL_DMSIDCollection) Is Nothing Then
                                        If Not _Flex.GetData(nResult, COL_DMSIDCollection).ToString() = "" Then
                                            .DMSIDCollection = Convert.ToInt64(_Flex.GetData(nResult, COL_DMSIDCollection).ToString())
                                        End If
                                    End If

                                    If Not _Flex.GetData(nResult, COL_TestResultDateTimeUTC) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResult, COL_TestResultDateTimeUTC)) Then
                                        .TestResultDateTimeUTC = _Flex.GetData(nResult, COL_TestResultDateTimeUTC)
                                    End If
                                    If Not _Flex.GetData(nResult, COL_SpecimenReceivedDateTimeUTC) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResult, COL_SpecimenReceivedDateTimeUTC)) Then
                                        .SpecimenReceivedDateTimeUTC = _Flex.GetData(nResult, COL_SpecimenReceivedDateTimeUTC)
                                    End If
                                    If Not _Flex.GetData(nResult, COL_ResultTransferDateTimeUTC) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResult, COL_ResultTransferDateTimeUTC)) Then
                                        .ResultTransferDateTimeUTC = _Flex.GetData(nResult, COL_ResultTransferDateTimeUTC)
                                    End If

                                    '---Fill Test Result Detail Start---
                                    oCellRange = _Flex.Rows(nResult).Node.GetCellRange
                                    Dim _ResultDetailStart As Int16 = oCellRange.r1 + 1
                                    Dim _ResultDetailEnd As Int16 = oCellRange.r2
                                    oCellRange = Nothing

                                    Dim oTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
                                    'Dim oItemDetails As New gloEMRActors.LabActor.ItemDetails
                                    For nResultDetail As Int16 = _ResultDetailStart To _ResultDetailEnd
                                        If Val(_Flex.GetData(nResultDetail, COL_RECORDTYPE) & "") = enumRecordType.Result Then
                                            oTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail
                                            With oTestResultDetail
                                                .OrderID = _Flex.GetData(nResultDetail, COL_ORDERID)
                                                .TestID = _Flex.GetData(nResultDetail, COL_TESTID)
                                                .TestName = _Flex.GetData(nResult, COL_TESTNAME) ' _Flex.GetData(nResultDetail, COL_TESTNAME)
                                                .TestResultNumber = _Flex.GetData(nResultDetail, COL_RESULT_NUMBER)
                                                .ResultLineNo = _Flex.GetData(nResultDetail, COL_RESULT_LINENO)
                                                .ResultNameID = _Flex.GetData(nResultDetail, COL_RESULT_NAMEID)
                                                .ResultName = _Flex.GetData(nResultDetail, COL_TEST_RESULT_NAME) & ""
                                                '.ResultValue = _Flex.GetData(nResultDetail, COL_TEST_DIGNOSIS_RESULT_VALUE) & "" commented by manoj on 20121727
                                                'start of added by manoj on 20121127 for making result value as hyperlink
                                                If TypeOf (_Flex.GetData(nResultDetail, COL_TEST_DIGNOSIS_RESULT_VALUE)) Is Hyperlink Then
                                                    .ResultValue = _Flex.GetData(nResultDetail, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString()
                                                Else
                                                    .ResultValue = _Flex.GetData(nResultDetail, COL_TEST_DIGNOSIS_RESULT_VALUE) & ""
                                                End If
                                                'end of added by manoj on 20121127 for making result value as hyperlink
                                                .ResultUnit = _Flex.GetData(nResultDetail, COL_TEST_CPT_RESULT_UNIT) & ""
                                                .ResultRange = _Flex.GetData(nResultDetail, COL_RESULT_RANGE) & ""

                                                .ResultTypeCode = _ObservationStatus_COL.GetCode(_Flex.GetData(nResultDetail, COL_RESULT_TYPE) & "")
                                                .ResultTypeDesc = _Flex.GetData(nResultDetail, COL_RESULT_TYPE)

                                                'COL_LAB_ORIGINAL_ABNORMAL_FLAG
                                                ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                                ''.AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG) & "")

                                                'If glogeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                '    'Dim strFlag As String
                                                '    'Dim strFlagList As String()
                                                '    'Dim strFlagInDB As String = _Flex.GetData(nResultDetail, COL_LAB_ORIGINAL_ABNORMAL_FLAG)
                                                '    'strFlagList = strFlagInDB.Split(",")

                                                '    'If strFlagList.Length > 1 Then
                                                '    '    strFlag = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)) & "," & strFlagList(1)
                                                '    'Else
                                                '    '    strFlag = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)) & "," & ""
                                                '    'End If
                                                '    '.AbnormalFlagCode = strFlag & ""

                                                '    .AbnormalFlagCode = _Flex.GetData(nResultDetail, COL_LAB_ORIGINAL_ABNORMAL_FLAG) & ""
                                                'Else
                                                '    .AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG) & "")
                                                'End If


                                                Dim strFlagList As String()
                                                Dim strFlagInDB As String = _Flex.GetData(nResultDetail, COL_LAB_ORIGINAL_ABNORMAL_FLAG)

                                                If Not IsNothing(strFlagInDB) Then
                                                    strFlagList = strFlagInDB.Split(",")
                                                    If strFlagList.Length > 1 Then ''Specific range flag binded to column 
                                                        .AbnormalFlagCode = strFlagList(0) & ""
                                                    Else ''This column has no specific range. Take the Normal Specific range value to save back to database 
                                                        .AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)) & ""
                                                    End If
                                                Else
                                                    .AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)) & ""
                                                End If


                                                'If glogeneral.clsgeneral.gstrSpecificResultRange = "1" Then                                                    
                                                '    .AbnormalFlagCode = _Flex.GetData(nResultDetail, COL_LAB_ORIGINAL_ABNORMAL_FLAG) & ""
                                                'Else
                                                '    .AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG) & "")
                                                'End If

                                                ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                                .AbnormalFlagDesc = _Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)
                                                'Select Case _Flex.GetData(nResultDetail, COL_RESULT_TYPE) & ""
                                                '    Case "None"
                                                '        .ResultType = gloEMRActors.LabActor.enumTestResultReadType.None
                                                '    Case "Prilimnary"
                                                '        .ResultType = gloEMRActors.LabActor.enumTestResultReadType.Prilimnary
                                                '    Case "Final"
                                                '        .ResultType = gloEMRActors.LabActor.enumTestResultReadType.Final
                                                '    Case "Ammend"
                                                '        .ResultType = gloEMRActors.LabActor.enumTestResultReadType.Ammend
                                                'End Select
                                                ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                                'Added by madan-- on 20100409...
                                                .AlternateResultCode = _Flex.GetData(nResultDetail, COL_ALTERNATE_RESULT_CODE) & ""
                                                .AlternateResultName = _Flex.GetData(nResultDetail, COL_ALTERNATE_RESULT_NAME) & ""
                                                .ProducerIdentifier = _Flex.GetData(nResultDetail, COL_PRODUCER_IDENTIFIER) & ""
                                                'End MAdan
                                                .ResultComment = _Flex.GetData(nResultDetail, COL_RESULT_COMMENT) & ""
                                                .ResultWord = Nothing
                                                .ResultDMSID = 0
                                                .UserID = 0
                                                .ResultLOINCID = _Flex.GetData(nResultDetail, COL_TEST_LOINC) & ""
                                                .AlternateResultCode = _Flex.GetData(nResultDetail, COL_LAB_TEST_CODE) & ""
                                                .ResultDateTime = _Flex.GetData(nResultDetail, COL_TEST_RESULT_DATETIME)
                                                .IsFinished = _Flex.GetData(nResultDetail, COL_ISFINISHED)

                                                ''Added by Abhijeet on 20101026
                                                .LabFacilityName = _Flex.GetData(nResultDetail, COL_LAB_FACILITY_NAME)
                                                .LabFacilityStreetAddress = _Flex.GetData(nResultDetail, COL_LAB_FACILITY_STREET_ADDRESS)
                                                .LabFacilityCity = _Flex.GetData(nResultDetail, COL_LAB_FACILITY_CITY)
                                                .LabFacilityState = _Flex.GetData(nResultDetail, COL_LAB_FACILITY_STATE)
                                                .LabFacilityZipCode = _Flex.GetData(nResultDetail, COL_LAB_FACILITY_ZIP_CODE)
                                                ''End of changes by Abhijeet on 20101026
                                                .PatientSpecificRange = _Flex.GetData(nResultDetail, COL_LAB_SPEC_RANGE)
                                                'Sanjog
                                                'Dim str As String = _Flex.GetData(nResultDetail, COL_SPECIMENCOLLECTIONDATE)
                                                .TestSpecimenCollectionDate = _Flex.GetData(nResultDetail, COL_SPECIMENCOLLECTIONDATE)
                                                'Sanjog


                                                'Mitesh
                                                If IsNothing(_Flex.GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME)) = False Then
                                                    .TestSpecimenCollectionDateTime = _Flex.GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME)
                                                Else
                                                    .TestSpecimenCollectionDateTime = Nothing
                                                End If

                                                .LabFacilityIdentifierTypeCode = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityIdentifierTypeCode))
                                                .LabFacilityOrganizationIdentifier = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityOrganizationIdentifier))
                                                .LabFacilityCountry = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityCountry))
                                                .LabFacilityCountyOrParishCode = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityCountyOrParishCode))
                                                .ResultCode = Convert.ToString(_Flex.GetData(nResultDetail, COL_ResultCode))
                                                .ResultCodeType = Convert.ToString(_Flex.GetData(nResultDetail, COL_ResultCodeType))
                                                .LabFacilityMedicalDirectorIDNumber = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorIDNumber))
                                                .LabFacilityMedicalDirectorLastName = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorLastName))
                                                .LabFacilityMedicalDirectorMiddleName = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorMiddleName))
                                                .LabFacilityMedicalDirectorSuffix = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorSuffix))
                                                .LabFacilityMedicalDirectorPrefix = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorPrefix))
                                                .LabFacilityMedicalDirectorFirstName = Convert.ToString(_Flex.GetData(nResultDetail, COL_LabFacilityMedicalDirectorFirstName))
                                                If Not _Flex.GetData(nResultDetail, COL_ResultParentChildFlag) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResultDetail, COL_ResultParentChildFlag)) Then
                                                    .ResultParentChildFlag = Convert.ToInt64(_Flex.GetData(nResultDetail, COL_ResultParentChildFlag))
                                                End If
                                                If Not _Flex.GetData(nResultDetail, COL_ResultDateTimeUTC) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResultDetail, COL_ResultDateTimeUTC)) Then
                                                    .ResultDateTimeUTC = Convert.ToInt32(_Flex.GetData(nResultDetail, COL_ResultDateTimeUTC))
                                                End If
                                                If Not _Flex.GetData(nResultDetail, COL_TestSpecimenCollectionDateTimeUTC) Is Nothing AndAlso IsNumeric(_Flex.GetData(nResultDetail, COL_TestSpecimenCollectionDateTimeUTC)) Then
                                                    .TestSpecimenCollectionDateTimeUTC = Convert.ToInt32(_Flex.GetData(nResultDetail, COL_TestSpecimenCollectionDateTimeUTC))
                                                End If

                                                .LabResultConceptID = Convert.ToString(_Flex.GetData(nResultDetail, COL_ConceptID))
                                                .LabResultICD9 = Convert.ToString(_Flex.GetData(nResultDetail, COL_ICD9))
                                                .LabResultICD10 = Convert.ToString(_Flex.GetData(nResultDetail, COL_ICD10))
                                                .LabResultLOINC = Convert.ToString(_Flex.GetData(nResultDetail, COL_LOINC))
                                                '--------x---

                                            End With
                                            .TestResultDetails.Add(oTestResultDetail)
                                            oTestResultDetail = Nothing
                                        End If
                                    Next ' For nResultDetail As Int16 = _ResultDetailStart To _ResultDetailEnd
                                    oTestResultDetail = Nothing
                                    '---Fill Test Result Detail Start---

                                End With
                                .OrderTestResults.Add(oTestResult)
                                oTestResult = Nothing
                            End If
                        Next 'For nresult As Int16 = _ResultStart To _ResultEnd
                        oTestResult = Nothing

                        'Private _OrderTestResults As OrderTestResults
                        '---Fill Test Result Finish---
                    End With
                    oOrderTests.Add(oOrderTest)
                    oOrderTest = Nothing
                End If
            Next

            '---Fill Tests Finish---           

            Return oOrderTests
        Else
            Return Nothing
        End If

    End Function





    Public Function AddTest(ByVal OrderID As Int64, ByVal TestID As Int64, ByVal TestName As String, ByVal UserID As Int64, Optional ByVal TestCode As String = "") As Boolean

        If IsTestExists(OrderID, TestID, TestName) = True Then
            AddTest = Nothing
            Exit Function
        End If
        'Resolved Bug #58664
        If OrderID > 0 Then
            _CurrentOrderID = OrderID
        End If
        '----

        With _Flex
            Try
                .Rows.Add()
                'Bug #81526: 00000893 :Unable to select from dx drop down unless select the grey box first 
                _CurrentRow = .Rows.Count - 1
                With .Rows(.Rows.Count - 1)
                    .ImageAndText = True
                    .Height = 22
                    .IsNode = True
                    .Style = _Flex.Styles("CS_Record")
                    .Node.Level = 0
                    .Node.Image = ImgTest.Image
                    .Node.Data = TestName
                End With
                ''Infobutton
                Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                _Flex.SetCellImage(.Rows.Count - 1, COL_LAB_INFOBUTTON, imgFlag)

                .SetCellStyle(.Rows.Count - 1, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_CheckBox"))
                '.SetCellStyle(.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                '.SetCellStyle(.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_CPTBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_COMMENTSBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_RESULT_RANGE, _Flex.Styles("CS_ComboList"))
                Dim _DB As New gloEMRDatabase.DataBaseLayer
                Dim _strSQL As String = ""
                Dim oDataTable As DataTable = Nothing


                _strSQL = "SELECT     Lab_Test_Mst.labtm_Code, Lab_Test_Mst.labtm_Name, isnull(Lab_CSST_MST.labCSST_Name,'') AS Specimen, " +
                          "Lab_Test_Mst.labtm_Instruction, Lab_Test_Mst.labtm_Precuation, Lab_Test_Mst.labtm_LOINCId,CASE WHEN Lab_Test_Mst.labtm_IsFinished = 1 THEN 'Yes' ELSE 'No' END AS labtm_IsFinished, isnull(Lab_CSST_MST_1.labCSST_Name,'') AS Collection, " +
                          "isnull(Lab_CSST_MST_2.labCSST_Name,'') AS StorageTemperature, isnull(Lab_CSST_MST.labCSST_ID,0) AS SpecimenID, isnull(Lab_CSST_MST_1.labCSST_ID,0) AS CollectionID, " +
                          "isnull(Lab_CSST_MST_2.labCSST_ID,0) AS StorageTemperatureID,Lab_Test_Mst.nTemplateID,isnull(Lab_Test_Mst.sCPTCode,'') as sCPTCode  FROM Lab_CSST_MST AS Lab_CSST_MST_2 RIGHT OUTER JOIN  " +
                          "Lab_Test_Mst ON Lab_CSST_MST_2.labCSST_ID = Lab_Test_Mst.labtm_StorageID LEFT OUTER JOIN Lab_CSST_MST AS Lab_CSST_MST_1 ON  " +
                          "Lab_Test_Mst.labtm_CollectionID = Lab_CSST_MST_1.labCSST_ID LEFT OUTER JOIN Lab_CSST_MST ON Lab_Test_Mst.labtm_SpecimenID = Lab_CSST_MST.labCSST_ID  " +
                          "WHERE (Lab_Test_Mst.labtm_ID = " & TestID & ") "

                oDataTable = _DB.GetDataTable_Query(_strSQL)
                If Not oDataTable Is Nothing Then
                    If oDataTable.Rows.Count > 0 Then
                        'Bug #81526: 00000893 :Unable to select from dx drop down unless select the grey box first 
                        Fill_Diagnosis_CPT()
                        .Cols(COL_SCHEDULED_DATETIME).AllowEditing = True
                        'Mitesh
                        .Cols(COL_TEST_RESULT_OR_TRANSFER_DATETIME).AllowEditing = True
                        .Cols(COL_REPORTED_DATETIME).AllowEditing = True
                        '--x-----
                        For i As Int16 = 0 To oDataTable.Rows.Count - 1
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_SELECT, True)

                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Code")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_RESULT_CODE, oDataTable.Rows(i).Item("labtm_Code") & "")
                            End If

                            .SetData(.Rows.Count - 1, COL_ORDERID, OrderID)
                            .SetData(.Rows.Count - 1, COL_TESTID, TestID)
                            .SetData(.Rows.Count - 1, COL_TESTNAME, TestName)

                            ''By Abhijeet on 20101026 
                            .SetData(.Rows.Count - 1, COL_TEST_STATUS, "Ordered")
                            .SetData(.Rows.Count - 1, COL_SPECIMEN_SOURCE, "")
                            .SetData(.Rows.Count - 1, COL_SPECIMEN_CONDITION_DISP, "")
                            .SetData(.Rows.Count - 1, COL_LAB_TEST_TYPE, "")
                            ''End of changes by Abhijeet on 20101026


                            Dim _TestLN As Int16 = 0
                            For l As Int16 = .Rows.Count - 1 To 1 Step -1
                                If .GetData(l, COL_RECORDTYPE) = enumRecordType.Test Then
                                    _TestLN = Val(.GetData(l, COL_TEST_LINENO))
                                    Exit For
                                End If
                            Next
                            _TestLN = _TestLN + 1
                            .SetData(.Rows.Count - 1, COL_TEST_LINENO, _TestLN)


                            .SetData(.Rows.Count - 1, COL_RESULT_NUMBER, 0)
                            .SetData(.Rows.Count - 1, COL_RESULT_LINENO, 0)
                            .SetData(.Rows.Count - 1, COL_RESULT_NAMEID, 0)
                            .SetData(.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, "")
                            .SetData(.Rows.Count - 1, COL_TEST_DIGNOSISBTN, "")
                            .SetData(.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, "")
                            .SetData(.Rows.Count - 1, COL_TEST_CPTBTN, "")
                            .SetData(.Rows.Count - 1, COL_LAB_SPEC_RANGE, "")
                            .SetData(.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, "")
                            .SetData(.Rows.Count - 1, COL_ABNORMAL_FLAG, "")
                            .SetData(.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_TYPE, "")
                            .SetData(.Rows.Count - 1, COL_TEST_COMMENTSBTN, "")
                            .SetData(.Rows.Count - 1, COL_TEST_NOTE, "")
                            .SetData(.Rows.Count - 1, COL_ISFINISHED, "")
                            If Not IsDBNull(oDataTable.Rows(i).Item("Specimen")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_SPECIMEN, oDataTable.Rows(i).Item("Specimen") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("Collection")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_COLLECTION, oDataTable.Rows(i).Item("Collection") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("StorageTemperature")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_STORAGE, oDataTable.Rows(i).Item("StorageTemperature") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_LOINCId")) Then
                                .SetData(.Rows.Count - 1, COL_LAB_TEST_CODE, oDataTable.Rows(i).Item("labtm_LOINCId") & "")
                            End If
                            .SetData(.Rows.Count - 1, COL_TEST_LOINC, "")
                            If Not IsDBNull(oDataTable.Rows(i).Item("sCPTCode")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_CPT, oDataTable.Rows(i).Item("sCPTCode") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Instruction")) Then
                                .SetData(.Rows.Count - 1, COL_LAB_SPEC_RANGE, oDataTable.Rows(i).Item("labtm_Instruction") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Precuation")) Then
                                .SetData(.Rows.Count - 1, COL_ABNORMAL_FLAG, oDataTable.Rows(i).Item("labtm_Precuation") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_IsFinished")) Then
                                .SetData(.Rows.Count - 1, COL_ISFINISHED, oDataTable.Rows(i).Item("labtm_IsFinished") & "")
                            End If
                            .SetData(.Rows.Count - 1, COL_RESULT_RANGE, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_VALUE_TYPE, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_COMMENT, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_TYPE, "")
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_DATETIME, Date.Now)
                            .SetData(.Rows.Count - 1, COL_SCHEDULED_DATETIME, Nothing)

                            'Sanjog
                            '.SetData(.Rows.Count - 1, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Nothing)
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_OR_TRANSFER_DATETIME, DateTime.Now)
                            'Sanjog

                            'Mitesh
                            .SetData(.Rows.Count - 1, COL_REPORTED_DATETIME, DateTime.Now)
                            '---x--

                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_USERID, UserID)
                            .SetData(.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
                            '.SetData(.Rows.Count - 1, COL_ISFINISHED, "")
                            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                            'Added by madan-- on 20100409...
                            .SetData(.Rows.Count - 1, COL_ALTERNATE_RESULT_CODE, "")
                            .SetData(.Rows.Count - 1, COL_ALTERNATE_RESULT_NAME, "")
                            'End

                            'If Convert.ToString(oDataTable.Rows(i).Item("nTemplateID")) = "" Then
                            '    templateId = 0
                            'Else
                            '    templateId = Convert.ToInt64(oDataTable.Rows(i).Item("nTemplateID"))
                            'End If
                        Next
                    End If
                    oDataTable.Dispose()
                    oDataTable = Nothing
                End If
                _DB.Dispose()
                _DB = Nothing

                If _Flex.Rows.Count > 1 Then
                    _Flex.Row = _Flex.Rows.Count - 1
                    _Flex.Select(_Flex.Rows.Count - 1, COL_TEST_RESULT_SELECT)
                End If


                'End If
            Catch objError As Exception
                'MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            Finally

            End Try

        End With
        Return Nothing
    End Function

    Public Function ClearTest() As Boolean
        _IsLoading = True
        DesignTestGrid()
        DesignDignosisCPTGrid()
        DesignResultGrid()
        _IsLoading = False

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False
        Return True
    End Function
    Public Function ClearTestEmdeon() As Boolean
        _IsLoading = True
        DesignEmdeonTestGrid()
        DesignDignosisCPTGrid()
        DesignResultGrid()
        _IsLoading = False

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False
        Return True
    End Function

    Public Function IsTestExists(ByVal OrderID As Int64, ByVal TestID As Int64, ByVal TestName As String) As Boolean
        Dim _Result As Boolean = False
        With _Flex
            For i As Integer = 1 To .Rows.Count - 1
                If .GetData(i, COL_RECORDTYPE) = enumRecordType.Test Then
                    If .GetData(i, COL_TEST_RESULT_NAME) = TestName AndAlso .GetData(i, COL_TESTID) = TestID Then
                        _Result = True
                        Exit For
                    End If
                End If
            Next
        End With

        Return _Result
    End Function

#Region "Commented by Pramod for DMSV2"
    'Public Function AddScanDocument(ByVal TestID As Int64, ByVal DocumentID As Int64) As Boolean
    '    Try
    '        With _Flex
    '            For i As Int16 = 1 To .Rows.Count - 1
    '                If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
    '                    Dim _tmpTestID As Int64 = 0
    '                    If Val(.GetData(i, COL_TESTID)) > 0 Then
    '                        _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
    '                    End If
    '                    If _tmpTestID = TestID Then
    '                        .SetData(i, COL_DMSID, DocumentID)
    '                        Exit For
    '                    End If
    '                End If
    '            Next
    '        End With

    '    Catch ex As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function
#End Region

    'Public Function AddScanDocument(ByVal TestID As Int64, ByVal DocumentID As Int64) As Boolean
    Public Function AddScanDocument(ByVal TestID As Int64, ByVal DocumentID As ArrayList) As Boolean
        Try
            With _Flex
                For i As Int16 = 1 To .Rows.Count - 1
                    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
                        Dim _tmpTestID As Int64 = 0
                        If Val(.GetData(i, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
                        End If
                        If _tmpTestID = TestID Then
                            If DocumentID.Count > 0 Then
                                .SetData(i, COL_DMSID, DocumentID(0))
                                .Rows(i).Node.Image = ImgAttachment.Image
                                Dim TmpID As String = String.Empty
                                For j As Int16 = 0 To DocumentID.Count - 1
                                    If j <> 0 Then
                                        TmpID = Convert.ToString(.GetData(i, COL_DMSIDCollection)) & "," & DocumentID(j)
                                        .SetData(i, COL_DMSIDCollection, TmpID)
                                    Else
                                        If Convert.ToString(.GetData(i, COL_DMSIDCollection)) = "" Then
                                            .SetData(i, COL_DMSIDCollection, DocumentID(j))
                                        Else
                                            TmpID = Convert.ToString(.GetData(i, COL_DMSIDCollection)) & "," & DocumentID(j)
                                            .SetData(i, COL_DMSIDCollection, TmpID)
                                        End If

                                    End If
                                Next
                                ''Added by Mayuri:20140320-To get lab user tasks 
                                '   GetLabTaskUsers() commented for incident 43035
                                ''
                                Exit For
                            End If
                        End If
                    End If
                Next
            End With



        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
#Region "Added by Mayuri:20140320-To get lab user tasks"
    Private Sub GetLabTaskUsers()
        Dim _ForEachNormalResult As Boolean = False
        Dim _ForEachAbNormalResult As Boolean = False
        Dim _OrderCompletionNormalResult As Boolean = False
        Dim _OrderCompletionAbNormalResult As Boolean = False
        Dim dtSetting As DataTable = Nothing
        Try

            dtSetting = GetLabTaskSettings(nProviderID)
            If Not dtSetting Is Nothing Then
                If dtSetting.Rows.Count > 0 Then
                    If dtSetting.Rows(0)(0) = 1 Then
                        _ForEachNormalResult = True
                    ElseIf dtSetting.Rows(0)(0) = 2 Then
                        _OrderCompletionNormalResult = True
                    End If
                    If dtSetting.Rows(0)(1) = 1 Then
                        _ForEachAbNormalResult = True
                    ElseIf dtSetting.Rows(0)(1) = 2 Then
                        _OrderCompletionAbNormalResult = True
                    End If
                End If
            End If
            CheckForFinalResults()
            CheckDataForFinalnormalResults()

            If _isnormal = True And _isAbnormal = False And _isfinal = True Then
                If _ForEachNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(1, 1)  ''1 for send tasktype and 1 for normal result
                ElseIf _OrderCompletionNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(2, 1) '' 2 for send task type upon order completion and 1 for normal result
                Else
                    RaiseEvent gUC_OkButtonClicked(1, 1) ''1 for each result and 1 for normal
                End If
            ElseIf _isnormal = True And _isAbnormal = False And _isfinal = False Then
                If _ForEachNormalResult = False And _OrderCompletionNormalResult = False Then ''if no user set in admin
                    RaiseEvent gUC_OkButtonClicked(1, 1)
                ElseIf _ForEachNormalResult = True Then ''if upon order completetion set and not all results final
                    RaiseEvent gUC_OkButtonClicked(1, 1)
                End If
            ElseIf _isnormal = False And _isAbnormal = True And _isfinal = True Then
                If _ForEachAbNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                ElseIf _OrderCompletionAbNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(2, 2)
                Else
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                End If
            ElseIf _isnormal = False And _isAbnormal = True And _isfinal = False Then
                If _ForEachAbNormalResult = False And _OrderCompletionAbNormalResult = False Then ''if no user set in admin
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                ElseIf _ForEachAbNormalResult = True Then ''if upon order completion set and not all results final
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                End If
            ElseIf _isnormal = True And _isAbnormal = True And _isfinal = True Then

                If _ForEachAbNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                ElseIf _OrderCompletionAbNormalResult = True Then
                    RaiseEvent gUC_OkButtonClicked(2, 2)
                Else
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                End If

            ElseIf _isnormal = True And _isAbnormal = True And _isfinal = False Then

                If _ForEachAbNormalResult = False And _OrderCompletionAbNormalResult = False Then ''if no user set in admin
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                ElseIf _ForEachAbNormalResult = True Then ''if upon order completion set and not all results final
                    RaiseEvent gUC_OkButtonClicked(1, 2)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtSetting) Then
                dtSetting.Dispose()
                dtSetting = Nothing
            End If
        End Try
    End Sub
    Private Sub CheckDataForFinalnormalResults()
        Dim oCellRange As C1.Win.C1FlexGrid.CellRange

        _isnormal = False
        _isAbnormal = False
        If _Flex.Rows.Count > 1 Then
            For nTest As Int16 = 1 To _Flex.Rows.Count - 1
                If Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                    oCellRange = _Flex.Rows(nTest).Node.GetCellRange
                    Dim _ResultStart As Int16 = oCellRange.r1 + 1
                    Dim _ResultEnd As Int16 = oCellRange.r2
                    oCellRange = Nothing
                    If _ResultStart < _ResultEnd Then
                        For nResult As Int16 = _ResultEnd To _ResultStart Step -1
                            If Val(_Flex.GetData(nResult, COL_RECORDTYPE) & "") = enumRecordType.ResultHeader Then
                                oCellRange = _Flex.Rows(nResult).Node.GetCellRange
                                Dim _ResultDetailStart As Int16 = oCellRange.r1 + 1
                                Dim _ResultDetailEnd As Int16 = oCellRange.r2
                                oCellRange = Nothing
                                For nResultDetail As Int16 = _ResultDetailStart To _ResultDetailEnd
                                    If Val(_Flex.GetData(nResultDetail, COL_RECORDTYPE) & "") = enumRecordType.Result Then
                                        ' If _isfinal = True Then


                                        'End If
                                        ' If _isnormal = True Then
                                        If (Convert.ToString(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)).Trim = "Normal") OrElse (Convert.ToString(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG)).Trim = "") Then
                                            _isnormal = True
                                        Else
                                            _isAbnormal = True
                                        End If
                                        'End If

                                    End If
                                Next
                                Exit For
                            End If

                        Next

                    Else
                        'If _isfinal = True Then
                        '    _isfinal = False
                        'End If
                    End If
                End If
            Next

        End If
    End Sub

    Private Sub CheckForFinalResults()
        Dim oCellRange As C1.Win.C1FlexGrid.CellRange
        _isfinal = False

        If _Flex.Rows.Count > 1 Then
            For nTest As Int16 = 1 To _Flex.Rows.Count - 1
                If Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                    oCellRange = _Flex.Rows(nTest).Node.GetCellRange
                    Dim _ResultStart As Int16 = oCellRange.r1 + 1
                    Dim _ResultEnd As Int16 = oCellRange.r2
                    oCellRange = Nothing
                    If _ResultStart < _ResultEnd Then

                        For nResult As Int16 = _ResultEnd To _ResultStart Step -1
                            If Val(_Flex.GetData(nResult, COL_RECORDTYPE) & "") = enumRecordType.ResultHeader Then
                                oCellRange = _Flex.Rows(nResult).Node.GetCellRange
                                Dim _ResultDetailStart As Int16 = oCellRange.r1 + 1
                                Dim _ResultDetailEnd As Int16 = oCellRange.r2
                                oCellRange = Nothing
                                For nResultDetail As Int16 = _ResultDetailStart To _ResultDetailEnd
                                    If Val(_Flex.GetData(nResultDetail, COL_RECORDTYPE) & "") = enumRecordType.Result Then

                                        If (Convert.ToString(_Flex.GetData(nResultDetail, COL_RESULT_TYPE)).Trim = "Final results") OrElse (Convert.ToString(_Flex.GetData(nResultDetail, COL_RESULT_TYPE)).Trim = "") Then
                                            _isfinal = True
                                        Else
                                            _isfinal = False
                                            Exit Sub
                                        End If



                                    End If

                                Next
                                Exit For
                            End If
                        Next
                    Else
                        If _isfinal = True Then
                            _isfinal = False
                        End If
                    End If
                End If
            Next

        End If
    End Sub

    Public Function GetLabTaskSettings(ByVal nProviderID As Long) As DataTable


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim dt As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nProviderID"
            oParamater.Value = nProviderID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            dt = oDB.GetDataTable("GetLabUserTaskSetting")
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

        End Try
    End Function
#End Region

    Public Function AddPrecuation(ByVal TestID As Int64, ByVal sData As String) As Boolean
        Try
            With _Flex
                For i As Int16 = 1 To .Rows.Count - 1
                    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
                        Dim _tmpTestID As Int64 = 0
                        If Val(.GetData(i, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
                        End If
                        If _tmpTestID = TestID Then
                            .SetData(i, COL_ABNORMAL_FLAG, sData)
                            Exit For
                        End If
                    End If
                Next
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Public Function AddInstruction(ByVal TestID As Int64, ByVal sData As String) As Boolean
        Try
            With _Flex
                For i As Int16 = 1 To .Rows.Count - 1
                    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
                        Dim _tmpTestID As Int64 = 0
                        If Val(.GetData(i, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
                        End If
                        If _tmpTestID = TestID Then
                            .SetData(i, COL_LAB_SPEC_RANGE, sData)
                            Exit For
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Public Function AddComments(ByVal TestID As Int64, ByVal sData As String) As Boolean
        '' 2070604 
        Try
            With _Flex
                For i As Int16 = 1 To .Rows.Count - 1
                    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
                        Dim _tmpTestID As Int64 = 0
                        If Val(.GetData(i, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
                        End If
                        If _tmpTestID = TestID Then
                            .SetData(i, COL_RESULT_TYPE, sData)
                            Exit For
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Public Sub New()

        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
   





    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Shared underLineFont As Font = Nothing
    Private Sub SetGridStyle(ByVal oFlex As C1.Win.C1FlexGrid.C1FlexGrid)
        With oFlex
            .ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            .Cols.Count = 0
            .Cols.Fixed = 0
            .Rows.Count = 1
            .Rows.Fixed = 1

            .Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.FromArgb(240, 247, 255)


            .Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

            .Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

            .Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
            .Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

            .Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.Focus.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Focus.ForeColor = Color.Black


            Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Header")
            Try
                If (.Styles.Contains("CS_Header")) Then
                    csHeader = .Styles("CS_Header")
                Else
                    csHeader = .Styles.Add("CS_Header")
                    With csHeader
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                        .ForeColor = Color.Black
                        .BackColor = Color.FromArgb(192, 203, 233)
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csHeader = .Styles.Add("CS_Header")
                With csHeader
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                    .ForeColor = Color.Black
                    .BackColor = Color.FromArgb(192, 203, 233)
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.String")
                End With
            End Try


            Dim csRecord As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Record")

            Try
                If (.Styles.Contains("CS_Record")) Then
                    csRecord = .Styles("CS_Record")
                Else
                    csRecord = .Styles.Add("CS_Record")
                    With csRecord
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csRecord = .Styles.Add("CS_Record")
                With csRecord
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.String")
                End With
            End Try


            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_ComboList")
            Try
                If (.Styles.Contains("CS_ComboList")) Then
                    csComboList = .Styles("CS_ComboList")
                Else
                    csComboList = .Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = .Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.String")
                    .ComboList = "..."
                End With
            End Try


            Dim csCheckBox As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_CheckBox")
            Try
                If (.Styles.Contains("CS_CheckBox")) Then
                    csCheckBox = .Styles("CS_CheckBox")
                Else
                    csCheckBox = .Styles.Add("CS_CheckBox")
                    With csCheckBox
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csCheckBox = .Styles.Add("CS_CheckBox")
                With csCheckBox
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.Boolean")
                End With
            End Try


            Dim csNotNormal As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_NotNormal")
            Try
                If (.Styles.Contains("CS_NotNormal")) Then
                    csNotNormal = .Styles("CS_NotNormal")
                Else
                    csNotNormal = .Styles.Add("CS_NotNormal")
                    With csNotNormal
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Red
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        '.DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csNotNormal = .Styles.Add("CS_NotNormal")
                With csNotNormal
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Red
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    '.DataType = Type.GetType("System.Boolean")
                End With
            End Try


        End With

        'start of added by manoj on 20121127 for making result value as hyperlink
        Dim csNewLink As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("NewLink")
        Try
            If (_Flex.Styles.Contains("NewLink")) Then
                csNewLink = _Flex.Styles("NewLink")
            Else
                csNewLink = _Flex.Styles.Add("NewLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(_Flex.Font, FontStyle.Underline)
                End If
                csNewLink.Font = underLineFont
                csNewLink.ForeColor = Color.Blue
            End If
        Catch ex As Exception
            csNewLink = _Flex.Styles.Add("NewLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(_Flex.Font, FontStyle.Underline)
            End If
            csNewLink.Font = underLineFont
            csNewLink.ForeColor = Color.Blue
        End Try

        'csNewLink.BackColor = Color.GhostWhite

        Dim csOldLink As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("OldLink")
        Try
            If (_Flex.Styles.Contains("OldLink")) Then
                csOldLink = _Flex.Styles("OldLink")
            Else
                csOldLink = _Flex.Styles.Add("OldLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(_Flex.Font, FontStyle.Underline)
                End If
                csOldLink.Font = underLineFont
                csOldLink.ForeColor = Color.Purple
            End If
        Catch ex As Exception
            csOldLink = _Flex.Styles.Add("OldLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(_Flex.Font, FontStyle.Underline)
            End If
            csOldLink.Font = underLineFont
            csOldLink.ForeColor = Color.Purple
        End Try

        '.BackColor = Color.GhostWhite
        'end of added by manoj on 20121127 for making result value as hyperlink

    End Sub

    Private Sub gloLabUC_Transaction_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        If Not IsNothing(c1DropDownControl1) Then
            c1DropDownControl1.CloseDropDown()
            c1DropDownControl1.CloseDropDown(True)
            If Not IsNothing(c1DropDownControl1.DropDownForm) Then
                c1DropDownControl1.DropDownForm.Close()
            End If
        End If
        If Not IsNothing(dropdown) Then
            dropdown.Dispose()
            dropdown = Nothing
        End If
    End Sub

    Private Sub gloUC_Transaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      

        gloC1FlexStyle.Style(c1DignosisCPTs)
        gloC1FlexStyle.Style(c1Results)
        _nUserId = Convert.ToInt64(appSettings("UserID"))
        Dim arrUserRights As ArrayList
        Dim clsinfobutton_Lab As New gloEMRGeneralLibrary.clsInfobutton
        arrUserRights = clsinfobutton_Lab.GetUserRightsForEducationMaterial(_nUserId)
        clsinfobutton_Lab = Nothing
        If Not IsNothing(arrUserRights) Then
            isEducationMaterialEnables = arrUserRights(0)
            isAdvancedReference = arrUserRights(1)
        End If


        'Madan- added on 2010413
        _IsLoading = True
        DesignTestGrid()
        DesignDignosisCPTGrid()
        DesignResultGrid()

        Fill_AbnormalFlag()
        Fill_ResultType()

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False

        'Read Setting to Enable Performing Lab Menu
        Dim objSettings As New gloSettings.GeneralSettings(_ConnectionString)
        Dim dtSettings As DataTable = objSettings.GetSetting("Enable Performing Lab Menu")
        If Not IsNothing(dtSettings) Then
            If dtSettings.Rows.Count > 0 Then
                usePreferredLabforIndividualTest = Convert.ToByte(dtSettings.Rows(0)("sSettingsValue"))
            End If
        End If
        If Not IsNothing(objSettings) Then
            objSettings.Dispose()
            objSettings = Nothing
        End If
        If Not IsNothing(dtSettings) Then
            dtSettings.Dispose()
            dtSettings = Nothing
        End If
       
        If strColName = "" Then
            ' lblDgnCPTSearch.Text = "Search on Code"
        End If
        _IsLoading = False

    End Sub

    Private Sub Fill_AbnormalFlag()
        _AbnormalFlag_COL = New gloEMRActors.LabActor.ItemDetails
        Dim AbnormalFlag As gloEMRActors.LabActor.ItemDetail

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "<"
            .Description = "Below absolute low-off instrument scale"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)

        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = ">"
            .Description = "Above absolute high-off instrument scale"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "A"
            .Description = "Abnormal"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "AA"
            .Description = "Very abnormal"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "B"
            .Description = "Better"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "D"
            .Description = "Significant change down"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "H"
            .Description = "Above high normal"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "HH"
            .Description = "Above upper panic limits"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "I"
            .Description = "Intermediate*"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "L"
            .Description = "Below low normal"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "LL"
            .Description = "Below lower panic limits"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "MS"
            .Description = "Moderately susceptible*"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "N"
            .Description = "Normal"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "null"
            .Description = "No range defined"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "R"
            .Description = "Resistant*"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "S"
            .Description = "Susceptible*"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "U"
            .Description = "Significant change up"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "VS"
            .Description = "Very susceptible*"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

        AbnormalFlag = New gloEMRActors.LabActor.ItemDetail
        With AbnormalFlag
            .Code = "W"
            .Description = "Worse"
            _AbnormalFlagList = _AbnormalFlagList & "|" & .Description
        End With
        _AbnormalFlag_COL.Add(AbnormalFlag)
        AbnormalFlag = Nothing

    End Sub

    Private Sub Fill_ResultType()
        ' '' Observation Status is Same as Result Type  [HL7-A4]
        _ObservationStatus_COL = New gloEMRActors.LabActor.ItemDetails
        Dim ObservationStatus As gloEMRActors.LabActor.ItemDetail

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "C"
            .Description = "Record coming over is a correction and thus replaces a final result"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "D"
            .Description = "Deletes the OBX record"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "F"
            .Description = "Final results"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "I"
            .Description = "Specimen in lab; results pending"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "N"
            .Description = "Not asked"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "O"
            .Description = "Order detail description only (no result)"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "P"
            .Description = "Preliminary results"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "R"
            .Description = "Results entered -- not verified"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "U"
            .Description = "Results status change to final without retransmitting results already sent as 'preliminary'"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "W"
            .Description = "Post original as wrong"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "S"
            .Description = "Partial Results"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing

        ObservationStatus = New gloEMRActors.LabActor.ItemDetail
        With ObservationStatus
            .Code = "X"
            .Description = "Results cannot be obtained for this observation"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing
    End Sub

    Private Sub _Flex_AfterResizeColumn(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterResizeColumn
        If RetriveAppSettings() Then
            Dim objSettings As New gloSettings.GeneralSettings(_ConnectionString)
            Try
                ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
                'When user tries to minimize width column contains dropdown listbox.
                ''Start
                With _Flex
                    If e.Col = COL_TEST_DIGNOSIS_RESULT_VALUE Or e.Col = COL_TEST_CPT_RESULT_UNIT Or e.Col = COL_TEST_STATUS Then
                        If .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width > prevWidth_COL_TEST_DIGNOSIS_RESULT_VALUE Then
                            .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width = .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width
                        Else
                            .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width = prevWidth_COL_TEST_DIGNOSIS_RESULT_VALUE
                        End If

                        If .Cols(COL_TEST_CPT_RESULT_UNIT).Width > prevWidth_COL_TEST_CPT_RESULT_UNIT Then
                            .Cols(COL_TEST_CPT_RESULT_UNIT).Width = .Cols(COL_TEST_CPT_RESULT_UNIT).Width
                        Else
                            .Cols(COL_TEST_CPT_RESULT_UNIT).Width = prevWidth_COL_TEST_CPT_RESULT_UNIT
                        End If

                        If .Cols(COL_TEST_STATUS).Width > prevWidth_COL_TEST_STATUS Then
                            .Cols(COL_TEST_STATUS).Width = .Cols(COL_TEST_STATUS).Width
                        Else
                            .Cols(COL_TEST_STATUS).Width = prevWidth_COL_TEST_STATUS
                        End If
                    End If
                End With
                ''End
                objSettings.SaveGridColumnWidth(_Flex, gloSettings.ModuleOfGridColumn.LabResultGrid, _nUserId)
            Catch ex As Exception

            Finally

                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                End If
            End Try
        End If
    End Sub

    Private Sub _Flex_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _Flex.AfterSelChange
        txtTestResultComment.Visible = False
        Splitter1.Visible = False
        txtTestResultComment.Text = ""
        If _Flex.Rows.Count >= 2 AndAlso _Flex.Cols.Count = COL_COUNT Then
            If _Flex.Row > 0 Then
                If Val(_Flex.GetData(_Flex.Row, COL_RECORDTYPE)) = enumRecordType.Test Then
                    Dim _tmpTestID As Int64 = 0
                    Dim _tmpDataRow As Int16 = _Flex.Row

                    If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                        _tmpTestID = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID) & "")
                    End If
                    'If _PreviousTestID <> _tmpTestID Then
                    RaiseEvent gUC_TestSelected(_tmpTestID, _Flex.GetData(_tmpDataRow, COL_TEST_SPECIMEN) & "", _Flex.GetData(_tmpDataRow, COL_TEST_COLLECTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_STORAGE) & "", _Flex.GetData(_tmpDataRow, COL_TEST_LOINC) & "", _Flex.GetData(_tmpDataRow, COL_LAB_SPEC_RANGE) & "", _Flex.GetData(_tmpDataRow, COL_ABNORMAL_FLAG) & "", _Flex.GetData(_tmpDataRow, COL_RESULT_TYPE) & "")
                    'End If
                End If
                If IsNothing(_Flex.GetData(_Flex.Row, COL_RESULT_COMMENT)) = False Then
                    If _Flex.GetData(_Flex.Row, COL_RESULT_COMMENT).ToString() <> "" And Val(_Flex.GetData(_Flex.Row, COL_RECORDTYPE)) = enumRecordType.Result Then
                        txtTestResultComment.Visible = True
                        Splitter1.Visible = True
                        txtTestResultComment.Text = _Flex.GetData(_Flex.Row, COL_RESULT_COMMENT).ToString()
                        'txtTestResultComment.BringToFront()
                        Splitter1.BringToFront()

                        '' _Flex.Select(_Flex.Row, _Flex.Col, True)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub _Flex_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellButtonClick
        Try
            With _Flex


                If e.Row > 0 And (e.Col = COL_RESULT_RANGE Or e.Col = COL_TEST_DIGNOSISBTN Or e.Col = COL_TEST_CPTBTN Or (e.Col = COL_TEST_INSTRUCTIONBTN Or e.Col = COL_TEST_PRECAUTIONBTN Or e.Col = COL_TEST_COMMENTSBTN)) Then
                    If Val(.GetData(e.Row, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        Try
                            _CurrentRow = e.Row
                            _CurrentColumn = e.Col

                            If _CurrentOrderID > 0 Then
                                RaiseEvent LockOrder(_CurrentOrderID)
                            End If

                            If _IsOrderLocked Then
                                Return
                            End If

                            'pnlDiagosisCPT.Visible = False
                            '_Flex.Visible = False
                            'pnlResults.Visible = False
                            'pnlInstruction.Visible = False

                            ''If _CurrentColumn = COL_TEST_DIGNOSISBTN Then
                            ''    '' Fill Dx
                            ''    Fill_DignosisCPT(e.Col)
                            ''    ''Sandip Darade 20090821
                            ''    _DgnCPT = e.Col
                            ''    pnlDiagosisCPT.Visible = True
                            ''    If Not .GetData(e.Row, COL_TEST_DIGNOSISLIST) Is Nothing Then
                            ''        Dim oDgnColl As New gloEMRActors.LabActor.ItemDetails
                            ''        oDgnColl = CType(.GetData(e.Row, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
                            ''        For i As Int16 = 0 To oDgnColl.Count - 1
                            ''            For j As Int16 = 1 To c1DignosisCPTs.Rows.Count - 1
                            ''                If c1DignosisCPTs.GetData(j, COL_DT_CODE) = oDgnColl.Item(i).Code And c1DignosisCPTs.GetData(j, COL_DT_DESCRIPTON) = oDgnColl.Item(i).Description Then
                            ''                    c1DignosisCPTs.SetData(j, COL_DT_SELECT, True)
                            ''                    Exit For
                            ''                End If
                            ''            Next
                            ''        Next
                            ''        oDgnColl = Nothing
                            ''    End If
                            ''ElseIf _CurrentColumn = COL_TEST_CPTBTN Then
                            ''    '' Fill CPT
                            ''    Fill_DignosisCPT(e.Col)
                            ''    ''Sandip Darade 20090821
                            ''    _DgnCPT = e.Col
                            ''    pnlDiagosisCPT.Visible = True
                            ''    If Not .GetData(e.Row, COL_TEST_CPTLIST) Is Nothing Then
                            ''        Dim oCPTColl As New gloEMRActors.LabActor.ItemDetails
                            ''        oCPTColl = CType(.GetData(e.Row, COL_TEST_CPTLIST), gloEMRActors.LabActor.ItemDetails)
                            ''        For i As Int16 = 0 To oCPTColl.Count - 1
                            ''            For j As Int16 = 1 To c1DignosisCPTs.Rows.Count - 1
                            ''                If c1DignosisCPTs.GetData(j, COL_DT_CODE) = oCPTColl.Item(i).Code And c1DignosisCPTs.GetData(j, COL_DT_DESCRIPTON) = oCPTColl.Item(i).Description Then
                            ''                    c1DignosisCPTs.SetData(j, COL_DT_SELECT, True)
                            ''                    Exit For
                            ''                End If
                            ''            Next
                            ''        Next
                            ''        oCPTColl = Nothing
                            ''    End If

                            ''Above code commented by Sandip Darade 20090929 adding code below  
                            ''to add CPT and Diagnosis using diagnosis screen as we do in Orders  
                            If _CurrentColumn = COL_TEST_DIGNOSISBTN Or _CurrentColumn = COL_TEST_CPTBTN Then
                                RaiseEvent gUC_ButtonDiagnCPTClicked() ''event will be raised on frmLab_RequestOrder
                                _Flex.Visible = True
                                pnlDiagosisCPT.Visible = False
                                pnlResults.Visible = False
                                Fill_Diagnosis_CPT()
                            ElseIf _CurrentColumn = COL_TEST_INSTRUCTIONBTN Then
                                ' '' 
                                '  Me.Controls.Add(pnlInstruction)
                                pnlInstruction.Visible = True
                                pnlInstruction.Dock = DockStyle.Fill
                                pnlInstruction.BringToFront()
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_LAB_SPEC_RANGE)
                                lblTestDetails_Header.Text = "      Instructions for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)
                            ElseIf _CurrentColumn = COL_TEST_PRECAUTIONBTN Then
                                ' ''
                                pnlInstruction.Visible = True
                                pnlInstruction.Dock = DockStyle.Fill
                                pnlInstruction.BringToFront()
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_ABNORMAL_FLAG)
                                lblTestDetails_Header.Text = "      Precaution for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)

                            ElseIf _CurrentColumn = COL_TEST_COMMENTSBTN Then                          ' ''
                                pnlInstruction.Visible = True
                                pnlInstruction.Dock = DockStyle.Fill
                                pnlInstruction.BringToFront()
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_RESULT_TYPE)
                                lblTestDetails_Header.Text = "      Comments for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)
                            ElseIf _CurrentColumn = COL_RESULT_RANGE Then

                                Dim _DB As New gloEMRDatabase.DataBaseLayer
                                Dim _strSQL As String = ""
                                Dim oDataTable As DataTable = Nothing
                                Dim sTemplate As Object = Nothing

                                Try
                                    sTemplate = .GetData(_CurrentRow, COL_TEST_WORDTEMPLATE)
                                    If Not IsNothing(sTemplate) Then
                                        Dim _isfinished As Integer = 0
                                        If (Convert.ToString(_Flex.GetData(_CurrentRow, COL_ISFINISHED)).Contains("Yes")) Then
                                            _isfinished = 1
                                        End If
                                        RaiseEvent gUC_ButtonTemplatesClicked(Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID)), sTemplate, False, _isfinished)
                                    Else
                                        _strSQL = "select sDescription from TemplateGallery_MST where nTemplateID =(SELECT Lab_Test_Mst.nTemplateID  FROM Lab_Test_Mst WHERE (Lab_Test_Mst.labtm_ID = " & Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID)) & "))"
                                        oDataTable = _DB.GetDataTable_Query(_strSQL)

                                        If Not oDataTable Is Nothing Then
                                            If oDataTable.Rows.Count > 0 Then
                                                If Convert.ToString(oDataTable.Rows(0).Item("sDescription")) = "" Then
                                                    sTemplate = Nothing
                                                Else
                                                    sTemplate = oDataTable.Rows(0).Item("sDescription")
                                                End If
                                            End If
                                        End If
                                        Dim _isfinished As Integer = 0
                                        If (Convert.ToString(.GetData(_CurrentRow, COL_ISFINISHED)).Contains("True")) Then
                                            _isfinished = 1
                                        End If
                                        RaiseEvent gUC_ButtonTemplatesClicked(Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID)), sTemplate, True, _isfinished)
                                    End If

                                Catch ex As Exception
                                Finally
                                    If Not IsNothing(_DB) Then
                                        _DB.Dispose()
                                        _DB = Nothing
                                    End If

                                    _strSQL = Nothing
                                    If Not IsNothing(oDataTable) Then
                                        oDataTable.Dispose()
                                        oDataTable = Nothing
                                    End If
                                End Try

                            End If


                        Catch ex As Exception
                            Me.Cursor = Cursors.Default
                            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "Patient Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub







    Private Sub Fill_DignosisCPT(ByVal COL_DgnCPT As Int16)
        Try
            Dim _DB As New gloEMRDatabase.DataBaseLayer
            Dim _strSQL As String = ""
            'If COL_DgnCPT = COL_TEST_DIGNOSISBTN Then
            '    _strSQL = "SELECT nICD9ID AS DTID ,sICD9Code AS DTCODE ,sDescription AS DTDESCRIPTON FROM ICD9 ORDER BY sICD9Code,sDescription"
            'ElseIf COL_DgnCPT = COL_TEST_CPTBTN Then
            '    _strSQL = "SELECT nCPTID AS DTID ,sCPTCode AS DTCODE ,sDescription AS DTDESCRIPTON  FROM CPT_MST ORDER BY sCPTCode,sDescription"
            'End If

            ''Above code commented by Sandip Darade  20090821
            ''to apply a filter before the data to be pulled 
            Dim strSearch As String = txtDgnCPTSearch.Text.Replace("'", "''")

            If COL_DgnCPT = COL_TEST_DIGNOSISBTN Then
                Dim RowFilter As String = "sICD9Code Like '%" & strSearch & "%' OR sDescription Like '%" & strSearch & "%' "

                _strSQL = "SELECT nICD9ID AS DTID ,sICD9Code AS DTCODE ,sDescription AS DTDESCRIPTON FROM ICD9  WHERE " & RowFilter & " ORDER BY sICD9Code,sDescription"

            ElseIf COL_DgnCPT = COL_TEST_CPTBTN Then

                Dim RowFilter As String = "sCPTCode Like '%" & strSearch & "%' OR sDescription Like '%" & strSearch & "%' "

                _strSQL = "SELECT nCPTID AS DTID ,sCPTCode AS DTCODE ,sDescription AS DTDESCRIPTON  FROM CPT_MST where " & RowFilter & " ORDER BY sCPTCode,sDescription"
            End If


            Dim oDataTable As DataTable = Nothing

            DesignDignosisCPTGrid()

            oDataTable = _DB.GetDataTable_Query(_strSQL)
            If Not oDataTable Is Nothing Then
                If oDataTable.Rows.Count > 0 Then
                    For i As Int32 = 0 To oDataTable.Rows.Count - 1
                        c1DignosisCPTs.Rows.Add()
                        c1DignosisCPTs.SetData(c1DignosisCPTs.Rows.Count - 1, COL_DT_ID, oDataTable.Rows(i).Item("DTID"))
                        c1DignosisCPTs.SetData(c1DignosisCPTs.Rows.Count - 1, COL_DT_CODE, oDataTable.Rows(i).Item("DTCODE"))
                        c1DignosisCPTs.SetData(c1DignosisCPTs.Rows.Count - 1, COL_DT_DESCRIPTON, oDataTable.Rows(i).Item("DTDESCRIPTON"))
                    Next
                End If
                oDataTable.Dispose()
                oDataTable = Nothing
            End If

            If (_bIsSearch = True) Then

                With _Flex
                    If COL_DgnCPT = COL_TEST_DIGNOSISBTN Then
                        If Not .GetData(.RowSel, COL_TEST_DIGNOSISLIST) Is Nothing Then
                            Dim oDgnColl As gloEMRActors.LabActor.ItemDetails = CType(.GetData(.RowSel, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
                            For i As Int16 = 0 To oDgnColl.Count - 1
                                For j As Int16 = 1 To c1DignosisCPTs.Rows.Count - 1
                                    If c1DignosisCPTs.GetData(j, COL_DT_CODE) = oDgnColl.Item(i).Code And c1DignosisCPTs.GetData(j, COL_DT_DESCRIPTON) = oDgnColl.Item(i).Description Then
                                        c1DignosisCPTs.SetData(j, COL_DT_SELECT, True)
                                        Exit For
                                    End If
                                Next
                            Next
                            oDgnColl = Nothing
                        ElseIf COL_DgnCPT = COL_TEST_CPTBTN Then

                            If Not .GetData(.RowSel, COL_TEST_CPTLIST) Is Nothing Then
                                Dim oCPTColl As gloEMRActors.LabActor.ItemDetails = CType(.GetData(.RowSel, COL_TEST_CPTLIST), gloEMRActors.LabActor.ItemDetails)
                                For i As Int16 = 0 To oCPTColl.Count - 1
                                    For j As Int16 = 1 To c1DignosisCPTs.Rows.Count - 1
                                        If c1DignosisCPTs.GetData(j, COL_DT_CODE) = oCPTColl.Item(i).Code And c1DignosisCPTs.GetData(j, COL_DT_DESCRIPTON) = oCPTColl.Item(i).Description Then
                                            c1DignosisCPTs.SetData(j, COL_DT_SELECT, True)
                                            Exit For
                                        End If
                                    Next
                                Next
                                oCPTColl = Nothing
                            End If
                        End If
                    End If
                End With

            End If
            oDataTable = Nothing
            _DB.Dispose()
            _DB = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnResultClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False

    End Sub

    Private Sub btnDigCPTClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsp_btnCancel.Click
        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False
    End Sub

    Private Sub btnDigCPTOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsp_btnOK.Click
        'Fill Code Remark
        Dim _ComboList As String = ""
        Dim _Code As String = "", _Description As String = ""

        Dim _Dignosis As New gloEMRActors.LabActor.ItemDetails
        Dim _Dignos As gloEMRActors.LabActor.ItemDetail

        For i As Int16 = 1 To c1DignosisCPTs.Rows.Count - 1
            If c1DignosisCPTs.GetData(i, COL_DT_SELECT) = True Then
                _Code = Trim(c1DignosisCPTs.GetData(i, COL_DT_CODE) & "")
                _Description = Trim(c1DignosisCPTs.GetData(i, COL_DT_DESCRIPTON) & "")
                _Dignos = New gloEMRActors.LabActor.ItemDetail
                With _Dignos
                    .Code = _Code
                    .Description = _Description
                End With
                _ComboList = _ComboList & "|" & _Code & "-" & _Description & "|"
                _Dignosis.Add(_Dignos)
                _Dignos = Nothing
            End If
        Next


        'Previous Dignosis Code
        _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1

        Dim csDgnCPTList As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
        Try
            If (_Flex.Styles.Contains("CS_DigCPT" & _DgnCPTRunTimeComboList)) Then
                csDgnCPTList = _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList)
            Else
                csDgnCPTList = _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
                With csDgnCPTList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    .DataType = Type.GetType("System.String")
                    .ComboList = _ComboList
                End With
            End If
        Catch ex As Exception
            csDgnCPTList = _Flex.Styles.Add("CS_DigCPT" & _DgnCPTRunTimeComboList)
            With csDgnCPTList
                .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                .ForeColor = Color.Black
                .BackColor = Color.GhostWhite
                .DataType = Type.GetType("System.String")
                .ComboList = _ComboList
            End With
        End Try


        If _CurrentColumn = COL_TEST_DIGNOSISBTN Then
            _Flex.SetCellStyle(_CurrentRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList))
            _Flex.SetData(_CurrentRow, COL_TEST_DIGNOSISLIST, _Dignosis)
        ElseIf _CurrentColumn = COL_TEST_CPTBTN Then
            _Flex.SetCellStyle(_CurrentRow, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList))
            _Flex.SetData(_CurrentRow, COL_TEST_CPTLIST, _Dignosis)
        End If

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False
    End Sub

    Private Sub btnResultOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim nTest As Int16 = 0
        Dim nStyleRow As Int16 = 0
        Dim nNodeRow As Int16 = 0
        Dim i As Int16 = 0

        Try
            c1Results.FinishEditing()
            Dim _blnResFnd As Boolean = False

            'Check Result Entered or not
            If c1Results.Rows.Count > 0 Then
                For i = 1 To c1Results.Rows.Count - 1
                    If Convert.ToString(c1Results.GetData(i, COL_TEST_RESULT_NAME)).Trim() & "" <> "" Then  ''trim condition added for bugid 67226
                        If Convert.ToString(c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE)).Trim() & "" <> "" And Convert.ToString(c1Results.GetData(i, COL_RESULT_TYPE)).Trim() & "" <> "" Then
                            _blnResFnd = True
                            Exit For
                        End If
                    End If
                Next
            End If

            If _blnResFnd = False Then
                MessageBox.Show("You are not entering any result, please enter result to continue", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Check Result Validation
            If _blnResFnd = True Then
                For i = 1 To c1Results.Rows.Count - 1
                    If c1Results.GetData(i, COL_TEST_RESULT_NAME) & "" <> "" Then
                        If c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE) & "" <> "" Then
                            If c1Results.GetData(i, COL_RESULT_TYPE) & "" = "" Then
                                MessageBox.Show("Please select result type for " & c1Results.GetData(i, COL_TEST_RESULT_NAME), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If


            With _Flex
                If .Rows.Count > 0 Then
                    If _CurrentTestResultIsModify = False Then
                        For nTest = 1 To .Rows.Count - 1
                            Dim _GrdTestID As Int64 = 0
                            Dim _sGrdTestName As String = ""
                            If Val(.GetData(nTest, COL_TESTID) & "") > 0 Then
                                _GrdTestID = Convert.ToInt64(.GetData(nTest, COL_TESTID))
                                _sGrdTestName = .GetData(nTest, COL_TESTNAME)
                            End If
                            'Mitesh
                            If _GrdTestID = _CurrentTestSelectedID AndAlso Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                                If IsNothing(.GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME)) OrElse .GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME) & "" = "" Then
                                    .SetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Date.Now)
                                End If
                                If IsNothing(.GetData(nTest, COL_REPORTED_DATETIME)) OrElse .GetData(nTest, COL_REPORTED_DATETIME) & "" = "" Then
                                    .SetData(nTest, COL_REPORTED_DATETIME, Date.Now)
                                End If
                            End If

                            '----x---
                            If _GrdTestID = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.Test Then
                                nStyleRow = nTest ' Set Test Node

                                .Rows(nTest).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Result-" & _CurrentTestResultSelectedCounterID, _CurrentTestSelectedID & _CurrentTestResultSelectedCounterID, ImgResultHeader.Image)

                                nStyleRow = .Rows(nTest).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                nNodeRow = nStyleRow

                                '---------------------------------------------------------
                                .SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")
                                .SetData(nStyleRow, COL_TEST_RESULT_NAME, "Result-" & _CurrentTestResultSelectedCounterID)
                                .SetData(nStyleRow, COL_TEST_RESULT_CODE, "Code")
                                .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                .SetData(nStyleRow, COL_TESTID, _CurrentTestSelectedID)
                                .SetData(nStyleRow, COL_TEST_LINENO, _CurrentTestLineNo)
                                .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                                .SetData(nStyleRow, COL_RESULT_LINENO, 0)
                                .SetData(nStyleRow, COL_RESULT_NAMEID, 0)
                                .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "Value")
                                .SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                                .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                                .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, "Unit")
                                .SetData(nStyleRow, COL_TEST_CPTLIST, "")
                                .SetData(nStyleRow, COL_TEST_CPTBTN, "")
                                .SetData(nStyleRow, COL_TEST_NOTE, "Note")
                                .SetData(nStyleRow, COL_TEST_SPECIMEN, "Specimen")
                                .SetData(nStyleRow, COL_TEST_COLLECTION, "Collection")
                                .SetData(nStyleRow, COL_TEST_STORAGE, "Storage")
                                ''Remove column Header by Abhijeet on 20101116
                                ''.SetData(nStyleRow, COL_TEST_LOINC, "LOINC Code")
                                .SetData(nStyleRow, COL_TEST_LOINC, "")
                                ''End of changes to Remove column Header by Abhijeet on 20101116
                                .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                .SetData(nStyleRow, COL_RESULT_TYPE, "")
                                .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                .SetData(nStyleRow, COL_RESULT_RANGE, "Range")
                                .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "Patient Specific Range")
                                .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "Value Type")
                                .SetData(nStyleRow, COL_ABNORMAL_FLAG, "Flag")
                                .SetData(nStyleRow, COL_RESULT_TYPE, "Result Type")
                                .SetData(nStyleRow, COL_RESULT_COMMENT, "R. Comment")
                                .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                                'Sanjog
                                '.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Date.Now)
                                'Sanjog




                                .SetData(nStyleRow, COL_TEST_RESULT_USERID, "R. UserID")
                                .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                .SetData(nStyleRow, COL_ISFINISHED, 0)
                                ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                                'Added by madan-- on 20100409...
                                .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                                .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, "")
                                .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                                .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                                .SetData(nStyleRow, COL_ALTERNATE_RESULT_CODE, "")
                                .SetData(nStyleRow, COL_ALTERNATE_RESULT_NAME, "")

                                .SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, "")

                                'End Madan.
                                ''Added by Abhijeet on 20101026
                                .SetData(nStyleRow, COL_TEST_STATUS, "")
                                .SetData(nStyleRow, COL_SPECIMEN_SOURCE, "")
                                .SetData(nStyleRow, COL_SPECIMEN_CONDITION_DISP, "")
                                .SetData(nStyleRow, COL_LAB_TEST_TYPE, "")
                                ''End of change Added by Abhijeet on 20101026


                                'Labs Denormalization 20090321
                                .SetData(nStyleRow, COL_TESTNAME, .GetData(nTest, COL_TESTNAME))
                                '---

                                '---------------------------------------------------------

                                With .Rows(nStyleRow)
                                    .ImageAndText = True
                                    .Height = 22
                                    .Style = _Flex.Styles("CS_Header")
                                    .AllowEditing = False
                                End With

                                .SetCellStyle(nStyleRow, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_Record"))


                                Exit For
                            End If
                        Next
                    Else
                        For nTest = 1 To .Rows.Count - 1
                            'Sanjog -Commented and added on 2011 March 9 to update the actual result which we are open for modification --Start
                            ''If .GetData(nTest, COL_TESTID) & "" = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.ResultHeader AndAlso .GetData(nTest, COL_RESULT_NUMBER) & "" = _CurrentTestResultSelectedCounterID Then
                            If .GetData(nTest, COL_TESTID) = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.ResultHeader AndAlso .GetData(nTest, COL_RESULT_NUMBER) & "" = _CurrentTestResultSelectedCounterID Then
                                'Sanjog -added on 2011 March 9 to update the actual result which we are open for modification  --End
                                nStyleRow = nTest ' Set Test Result Node
                                nNodeRow = nStyleRow

                                Dim oRange As C1.Win.C1FlexGrid.CellRange
                                oRange = .Rows(nTest).Node.GetCellRange
                                Dim nTestStart As Int16 = oRange.TopRow
                                Dim nTestEnd As Int16 = oRange.BottomRow
                                Dim nTestCount As Int16 = nTestEnd - nTestStart
                                If nTestStart <> nTestEnd Then
                                    oRange.r1 = nTestStart + 1
                                    .Rows.RemoveRange(oRange.r1, nTestCount)
                                End If
                                oRange = Nothing

                                Exit For
                            End If
                        Next
                    End If
                    'Dim tempSendTask As Int16 = 1
                    'Dim _SendTaskType As String = 1
                    'Dim tempNormalResult As Int16 = 0
                    'Dim tempAbNormalResult As Int16 = 0
                    'Fill Results
                    For i = 1 To c1Results.Rows.Count - 1
                        If c1Results.GetData(i, COL_TEST_RESULT_NAME) <> "" AndAlso c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE) <> "" Then
                            '.Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, c1Results.GetData(i, COL_TEST_RESULT_NAME), _CurrentTestResultSelectedCounterID & i, ImgResult.Image)

                            If IsNothing(c1Results.GetData(i, COL_RESULT_COMMENT)) = False Then
                                If c1Results.GetData(i, COL_RESULT_COMMENT).ToString() <> "" Then
                                    '' If Test Result have Comment
                                    .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, c1Results.GetData(i, COL_TEST_RESULT_NAME), _CurrentTestResultSelectedCounterID & i, ImgResult_Comment.Image)
                                Else
                                    '' If Test Result don't have Comment
                                    .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, c1Results.GetData(i, COL_TEST_RESULT_NAME), _CurrentTestResultSelectedCounterID & i, ImgResult.Image)
                                End If
                            Else
                                '' If Test Result don't have Comment
                                .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, c1Results.GetData(i, COL_TEST_RESULT_NAME), _CurrentTestResultSelectedCounterID & i, ImgResult.Image)
                            End If

                            nStyleRow = .Rows(nNodeRow).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index

                            '---------------------------------------------------------
                            .SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")
                            .SetData(nStyleRow, COL_TEST_RESULT_NAME, c1Results.GetData(i, COL_TEST_RESULT_NAME))
                            .SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                            .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                            .SetData(nStyleRow, COL_TESTID, _CurrentTestSelectedID)
                            .SetData(nStyleRow, COL_TEST_LINENO, _CurrentTestLineNo)
                            .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                            .SetData(nStyleRow, COL_RESULT_LINENO, i)
                            .SetData(nStyleRow, COL_RESULT_NAMEID, c1Results.GetData(i, COL_RESULT_NAMEID))
                            '.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE)) commented by manoj jadhav on 20121127
                            'start of added by manoj on 20121127 for making result value as hyperlink
                            If gloGlobal.gloLabGenral.IsResultisHyperLink(c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE)) Then
                                .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Item(nStyleRow) = New Hyperlink(c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString())
                            Else
                                .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE))
                            End If
                            'start of added by manoj on 20121127 for making result value as hyperlink
                            .SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                            .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                            .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, c1Results.GetData(i, COL_TEST_CPT_RESULT_UNIT))
                            .SetData(nStyleRow, COL_TEST_CPTLIST, "")
                            .SetData(nStyleRow, COL_TEST_CPTBTN, "")
                            .SetData(nStyleRow, COL_TEST_NOTE, "")
                            .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                            .SetData(nStyleRow, COL_TEST_COLLECTION, "")
                            .SetData(nStyleRow, COL_TEST_STORAGE, "")
                            .SetData(nStyleRow, COL_TEST_LOINC, c1Results.GetData(i, COL_TEST_LOINC))
                            .SetData(nStyleRow, COL_LAB_TEST_CODE, c1Results.GetData(i, COL_LAB_TEST_CODE))
                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                            .SetData(nStyleRow, COL_RESULT_TYPE, "")
                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                            .SetData(nStyleRow, COL_RESULT_RANGE, c1Results.GetData(i, COL_RESULT_RANGE))
                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, c1Results.GetData(i, COL_RESULT_VALUE_TYPE))
                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, c1Results.GetData(i, COL_ABNORMAL_FLAG))
                            .SetData(nStyleRow, COL_RESULT_TYPE, c1Results.GetData(i, COL_RESULT_TYPE))
                            .SetData(nStyleRow, COL_RESULT_COMMENT, c1Results.GetData(i, COL_RESULT_COMMENT))
                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                            'Sanjog
                            .SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, c1Results.GetData(i, COL_TEST_RESULT_OR_TRANSFER_DATETIME))
                            'Sanjog
                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, c1Results.GetData(i, COL_TEST_RESULT_USERID)) '// Remaining
                            .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                            .SetData(nStyleRow, COL_ISFINISHED, 0)
                            ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                            'Added by madan-- on 20100409...
                            .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                            .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, c1Results.GetData(i, COL_RESULT_TRANSFER_DATETIME))
                            .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                            .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                            .SetData(nStyleRow, COL_ALTERNATE_RESULT_CODE, "")
                            .SetData(nStyleRow, COL_ALTERNATE_RESULT_NAME, "")
                            'End MAdan

                            ''Added by Abhijeet on 20101026
                            .SetData(nStyleRow, COL_LAB_FACILITY_NAME, "")
                            .SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, "")
                            .SetData(nStyleRow, COL_LAB_FACILITY_CITY, "")
                            .SetData(nStyleRow, COL_LAB_FACILITY_STATE, "")
                            .SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, "")
                            .SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, c1Results.GetData(i, COL_LAB_ORIGINAL_ABNORMAL_FLAG))
                            ''End of change Added by Abhijeet on 20101026
                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, c1Results.GetData(i, COL_LAB_SPEC_RANGE))
                            'Labs Denormalization 20090321
                            'Sanjog
                            .SetData(nStyleRow, COL_SPECIMENCOLLECTIONDATE, c1Results.GetData(i, COL_SPECIMENCOLLECTIONDATE))
                            'Sanjog
                            .SetData(nStyleRow, COL_TESTNAME, c1Results.GetData(i, COL_TESTNAME))
                            '------
                            '---------------------------------------------------------
                            With .Rows(nStyleRow)
                                .ImageAndText = True
                                .Height = 22
                                'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                'If _AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG))) Then
                                If _AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG))) Then
                                    'Normal and nothing
                                    .Style = _Flex.Styles("CS_Record")
                                Else
                                    ' '' Not Normal
                                    .Style = _Flex.Styles("CS_NotNormal")

                                End If
                            End With
                            .SetCellStyle(nStyleRow, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                            .Rows(nStyleRow).AllowEditing = False
                        End If

                        'Infobutton
                        Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                        _Flex.SetCellImage(nStyleRow, COL_LAB_INFOBUTTON, imgFlag)


                    Next ' For i = 1 To oCategorisedDocuments.Count

                    _Flex.Visible = True
                    pnlDiagosisCPT.Visible = False
                    pnlResults.Visible = False
                    ''Added by Mayuri:20140313-Added event Ok to fill task users list.
                    GetLabTaskUsers()

                End If
            End With




        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub _Flex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            With _Flex
                'Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                ''Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
                '_Flex.Select(_tmpDataRow, True)
                If .Row > 0 Then
                    If .GetData(.Row, COL_RECORDTYPE) = enumRecordType.Result Then
                        If .Col = COL_TEST_DIGNOSIS_RESULT_VALUE And Convert.ToString(_Flex.GetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE)).Length > 50 Then
                            'start of code added by manoj on 20121205 for making electronic result readonly
                            If Convert.ToString(_Flex.GetData(.Row, COL_RESULT_NAMEID)).Trim() = "0" Then
                                txtInstruction.ReadOnly = False
                            Else
                                If TypeOf (_Flex.GetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE)) Is Hyperlink Then
                                    Exit Try
                                End If
                                txtInstruction.ReadOnly = True
                            End If
                            'end of code added by manoj on 20121205 for making electronic result readonly
                            _Flex.Visible = False

                            pnlInstruction.Visible = True
                            pnlInstruction.Dock = DockStyle.Fill
                            pnlInstruction.BringToFront()
                            txtInstruction.Text = _Flex.GetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString()
                            lblTestDetails_Header.Text = "Results"
                        End If

                        'Added by manoj on 20140731 V8022 PRD:View Lab Order Comments for Electronic Lab orders
                        Try
                            If Not String.IsNullOrEmpty(Convert.ToString(_Flex.GetData(_Flex.Row, COL_RESULT_COMMENT))) Then
                                txtTestResultComment.Visible = True
                                Splitter1.Visible = True
                                txtTestResultComment.Text = _Flex.GetData(_Flex.Row, COL_RESULT_COMMENT).ToString()
                                Splitter1.BringToFront()
                            End If
                        Catch
                        End Try
                    End If
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function calculateAge(ByVal birthDate As Date) As Integer
        Dim mySpan As Long = CLng(Date.Now.Subtract(birthDate).TotalDays)
        Dim nowYear As Int16 = Date.Now.Year
        Dim nowMonth As Int16 = Date.Now.Month
        Dim birthYear As Int16 = birthDate.Year
        Dim birthMonth As Int16 = birthDate.Month
        Dim yearCount As Int16
        For yearCount = Date.Now.Year To birthYear Step -1
            If yearCount Mod 4 = 0 Then
                Select Case True
                    Case yearCount = nowYear And nowMonth < 3
                        'This is a leap year but we're not past Feb yet
                        'Do nothing
                    Case yearCount = birthYear And birthMonth > 2
                        'They were born after Feb in a leap year
                        'Do nothing
                    Case Else
                        'This was a full leap year, subtract a day to make it 365
                        mySpan -= 1
                End Select
            End If
        Next

        Dim myYears As Int16 = mySpan / 365
        Dim myDays As Int16 = mySpan - (myYears * 365)
        If myDays < 0 Then
            myYears -= 1
            myDays = 365 + myDays ' myDays is negative so this will subtract
        End If
        Return myYears
    End Function
    'Private Function Get_DocumentDetails(ByVal nPatientID As Long, ByVal nOrderID As Long, ByVal nTestID As Long) As DataTable
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString
    '    Dim objCmd As New SqlCommand

    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "Lab_Get_Test_Attachment"
    '    objCmd.Parameters.Clear()

    '    Dim objParaPatient As New SqlParameter
    '    With objParaPatient
    '        .ParameterName = "@PatientID"
    '        .Value = nPatientID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaPatient)
    '    objParaPatient = Nothing

    '    Dim objParaOrder As New SqlParameter
    '    With objParaOrder
    '        .ParameterName = "@OrderID"
    '        .Value = nOrderID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaOrder)
    '    objParaOrder = Nothing

    '    Dim objParaTest As New SqlParameter
    '    With objParaTest
    '        .ParameterName = "@TestID"
    '        .Value = nTestID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaTest)
    '    objParaTest = Nothing


    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)
    '    Dim dsData As New DataSet
    '    objDA.Fill(dsData)
    '    objCon.Close()
    '    objCon = Nothing
    '    Return dsData.Tables(0)

    'End Function
    Private Sub _Flex_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            Try
                If (IsNothing(_Flex.ContextMenu) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenu)
                    If (IsNothing(_Flex.ContextMenu.MenuItems) = False) Then
                        _Flex.ContextMenu.MenuItems.Clear()
                    End If
                    _Flex.ContextMenu.Dispose()
                    _Flex.ContextMenu = Nothing
                End If
            Catch ex As Exception

            End Try
            _Flex.ContextMenu = Nothing


            'Edited by madan on 20100617
            'If _IsLoadLastTransaction = False Then
            If e.Button = Windows.Forms.MouseButtons.Right Then

                'Madan added for checking order is locked... and event will be fired in vieworder form in Emdoeninterface project.
                'If _CurrentOrderID > 0 Then
                '    RaiseEvent LockOrder(_CurrentOrderID)
                'End If

                If _IsOrderLocked Then
                    Return
                End If

                Try
                    If (IsNothing(oContxMenu) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(oContxMenu)
                        If (IsNothing(oContxMenu.Items) = False) Then
                            oContxMenu.Items.Clear()
                        End If
                        oContxMenu.Dispose()
                        oContxMenu = Nothing
                    End If
                Catch

                End Try

                oContxMenu = New ContextMenuStrip
                '  Dim i As Integer = 0
                Dim _tmpTestResultID As Int64 = 0
                Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                'Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
                _Flex.Select(_tmpDataRow, True)
                If _tmpDataRow > 0 Then
                    flexrow = 0
                    flexrow = _tmpDataRow
                    If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test Then
                        'Test - Add Result
                        If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                            '_tmpTestResultID = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID) & "")
                            oContxMenu.Items.Clear()

                            Dim omnuNode As C1.Win.C1FlexGrid.Node
                            Dim _BlockRemove As Boolean = False
                            Dim _mnuStart As Integer = 0, _mnuFinish As Integer = 0
                            Dim _mnuRange As C1.Win.C1FlexGrid.CellRange

                            omnuNode = _Flex.Rows(_tmpDataRow).Node
                            _mnuRange = omnuNode.GetCellRange

                            _mnuStart = _mnuRange.TopRow : _mnuFinish = _mnuRange.BottomRow
                            If _mnuStart <> _mnuFinish Then
                                For _mnuResCnt As Integer = _mnuStart To _mnuFinish
                                    If Val(_Flex.GetData(_mnuResCnt, COL_RECORDTYPE)) = enumRecordType.ResultHeader Then
                                        If Val(_Flex.GetData(_mnuResCnt, COL_ISFINISHED)) = 1 Then
                                            _BlockRemove = True
                                            Exit For
                                        End If
                                    End If
                                Next
                            End If

                            'Menu Object
                            Dim oMnuItem As ToolStripMenuItem
                            Dim oMnuItemDMS As ToolStripMenuItem
                            Dim oMnuSubItem As ToolStripMenuItem

                            oMnuItem = New ToolStripMenuItem
                            oMnuItem.Text = "Add Result" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftR : oMnuItem.ShowShortcutKeys = False
                            oMnuItem.Image = ImgContextMenu.Images("Add Result.ico")
                            oContxMenu.Items.Add(oMnuItem)
                            AddHandler oMnuItem.Click, AddressOf Set_Menu_AddResult
                            oMnuItem = Nothing

                            oContxMenu.Items.Add(New ToolStripSeparator)

                            oMnuItem = New ToolStripMenuItem
                            oMnuItem.Text = "Add Result Document" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                            oMnuItem.Image = ImgContextMenu.Images("Add Result Document.ico")
                            oContxMenu.Items.Add(oMnuItem)
                            AddHandler oMnuItem.Click, AddressOf Set_Menu_ScanDocument
                            oMnuItem = Nothing


                            If Val(_Flex.GetData(_tmpDataRow, COL_DMSID)) > 0 Then

                                Dim dtDoc As DataTable = Get_DocumentDetails(nPatientID, _CurrentOrderID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)), Convert.ToString(_Flex.GetData(_tmpDataRow, COL_DMSIDCollection)))
                                Dim i As Integer = 0
                                If Not IsNothing(dtDoc) Then

                                    If dtDoc.Rows.Count > 0 Then

                                        oMnuItem = New ToolStripMenuItem
                                        oMnuItem.Text = "View Result Document" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftV : oMnuItem.ShowShortcutKeys = False
                                        oMnuItem.Image = ImgContextMenu.Images("View Result Document.ico")
                                        oContxMenu.Items.Add(oMnuItem)
                                        'AddHandler oMnuItem.Click, AddressOf Set_Menu_ViewDocument

                                        oMnuItemDMS = New ToolStripMenuItem
                                        oMnuItemDMS.Text = "Remove Result Document" : oMnuItemDMS.ShortcutKeys = Shortcut.CtrlShiftR : oMnuItemDMS.ShowShortcutKeys = False
                                        oMnuItemDMS.Image = ImgContextMenu.Images("Remove Result Document.ico")
                                        oContxMenu.Items.Add(oMnuItemDMS)

                                        If Not IsNothing(nDocList) Then
                                            nDocList.Clear()
                                        End If
                                        nDocList = New ArrayList
                                        For i = 0 To dtDoc.Rows.Count - 1
                                            oMnuSubItem = New ToolStripMenuItem
                                            oMnuSubItem.Text = dtDoc.Rows(i)("DocumentName")
                                            oMnuSubItem.Tag = dtDoc.Rows(i)("DocumentID")
                                            oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                            oMnuItem.DropDownItems.Add(oMnuSubItem)
                                            AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewDocument
                                            oMnuSubItem = Nothing

                                            'Remove Document
                                            oMnuSubItem = New ToolStripMenuItem
                                            oMnuSubItem.Text = dtDoc.Rows(i)("DocumentName")
                                            oMnuSubItem.Tag = dtDoc.Rows(i)("DocumentID")
                                            oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                            oMnuItemDMS.DropDownItems.Add(oMnuSubItem)
                                            AddHandler oMnuSubItem.Click, AddressOf Set_Menu_RemoveDocument
                                            oMnuSubItem = Nothing
                                            nDocList.Add(dtDoc.Rows(i)("DocumentID"))
                                        Next
                                    End If
                                End If

                                oMnuItemDMS = Nothing
                                oMnuItem = Nothing
                                If Not IsNothing(dtDoc) Then
                                    dtDoc.Dispose()
                                    dtDoc = Nothing
                                End If
                            End If

                            oContxMenu.Items.Add(New ToolStripSeparator)

                            'URL Document
                            oMnuItem = New ToolStripMenuItem
                            oMnuItem.Text = "Add Result URL Document" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                            oMnuItem.Image = ImgContextMenu.Images("Add Result URL Doc.ico")
                            oContxMenu.Items.Add(oMnuItem)
                            AddHandler oMnuItem.Click, AddressOf Set_Menu_URLDocument
                            oMnuItem = Nothing


                            If IsNothing(dtURLDoc) = True Then
                                dtURLDoc = Lab_GetURLDocument(_CurrentOrderID, 0)
                            End If

                            Dim dvURLDoc As DataView = New DataView(dtURLDoc)
                            Dim dtdvURLDoc As DataTable = Nothing

                            If Not IsNothing(dtURLDoc) Then
                                dvURLDoc.RowFilter = "labotrda_TestID =" & Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID))
                                dtdvURLDoc = dvURLDoc.ToTable
                            End If

                            If Not IsNothing(dtdvURLDoc) > 0 Then
                                Dim i As Integer = 0
                                If dtdvURLDoc.Rows.Count > 0 Then

                                    Dim oMnuItemNewURLdoc As New ToolStripMenuItem
                                    oMnuItemNewURLdoc = New ToolStripMenuItem
                                    oMnuItemNewURLdoc.Text = "View Result URL Document"
                                    oMnuItemNewURLdoc.ShortcutKeys = Shortcut.CtrlShiftV
                                    oMnuItemNewURLdoc.ShowShortcutKeys = False
                                    oMnuItemNewURLdoc.Image = ImgContextMenu.Images("View Result URL Doc.ico")
                                    oContxMenu.Items.Add(oMnuItemNewURLdoc)

                                    Dim oMnuItemEditURLdoc As New ToolStripMenuItem
                                    oMnuItemEditURLdoc.Text = "Edit Result URL Document"
                                    oMnuItemEditURLdoc.ShortcutKeys = Shortcut.CtrlShiftR
                                    oMnuItemEditURLdoc.ShowShortcutKeys = False
                                    oMnuItemEditURLdoc.Image = ImgContextMenu.Images("Edit View URL Doc.ico")
                                    oContxMenu.Items.Add(oMnuItemEditURLdoc)

                                    Dim oMnuItemRemoveURLdoc As New ToolStripMenuItem
                                    oMnuItemRemoveURLdoc = New ToolStripMenuItem
                                    oMnuItemRemoveURLdoc.Text = "Remove Result URL Document"
                                    oMnuItemRemoveURLdoc.ShortcutKeys = Shortcut.CtrlShiftR
                                    oMnuItemRemoveURLdoc.ShowShortcutKeys = False
                                    oMnuItemRemoveURLdoc.Image = ImgContextMenu.Images("Remove Result URL Doc.ico")
                                    oContxMenu.Items.Add(oMnuItemRemoveURLdoc)

                                    If Not IsNothing(nURLDocList) Then
                                        nURLDocList.Clear()
                                    End If
                                    nURLDocList = New ArrayList
                                    For i = 0 To dtdvURLDoc.Rows.Count - 1
                                        'New URl document
                                        oMnuSubItem = New ToolStripMenuItem
                                        oMnuSubItem.Text = dtdvURLDoc.Rows(i)("URLDisplayName")
                                        oMnuSubItem.Tag = dtdvURLDoc.Rows(i)("URLName") + "||" + Convert.ToString(dtdvURLDoc.Rows(i)("labotrda_TestID"))
                                        oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                        oMnuItemNewURLdoc.DropDownItems.Add(oMnuSubItem)
                                        AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument
                                        oMnuSubItem = Nothing

                                        'Edit URL document
                                        oMnuSubItem = New ToolStripMenuItem
                                        oMnuSubItem.Text = dtdvURLDoc.Rows(i)("URLDisplayName")
                                        oMnuSubItem.Tag = dtdvURLDoc.Rows(i)("URLName") + "||" + Convert.ToString(dtdvURLDoc.Rows(i)("labotrda_TestID")) + "||" + i.ToString()
                                        oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                        oMnuItemEditURLdoc.DropDownItems.Add(oMnuSubItem)
                                        AddHandler oMnuSubItem.Click, AddressOf Set_Menu_EditURLDocument
                                        oMnuSubItem = Nothing
                                        nURLDocList.Add(dtdvURLDoc.Rows(i)("URLID"))

                                        'Remove URL document
                                        oMnuSubItem = New ToolStripMenuItem
                                        oMnuSubItem.Text = dtdvURLDoc.Rows(i)("URLDisplayName")
                                        oMnuSubItem.Tag = dtdvURLDoc.Rows(i)("URLName") + "||" + Convert.ToString(dtdvURLDoc.Rows(i)("labotrda_TestID"))
                                        oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                        oMnuItemRemoveURLdoc.DropDownItems.Add(oMnuSubItem)
                                        AddHandler oMnuSubItem.Click, AddressOf Set_Menu_RemoveURLDocument
                                        oMnuSubItem = Nothing
                                        nURLDocList.Add(dtdvURLDoc.Rows(i)("URLID"))
                                    Next
                                End If
                                dtdvURLDoc.Dispose()
                                dtdvURLDoc = Nothing
                            End If

                            If strOrderType <> "emdeon" And _IsLoadLastTransaction = False Then
                                If _BlockRemove = False Then
                                    oContxMenu.Items.Add(New ToolStripSeparator)
                                    oMnuItem = New ToolStripMenuItem
                                    oMnuItem.Text = "Remove Test" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftT : oMnuItem.ShowShortcutKeys = False
                                    oMnuItem.Image = ImgContextMenu.Images("Remove Test.ico")
                                    oContxMenu.Items.Add(oMnuItem)
                                    AddHandler oMnuItem.Click, AddressOf Set_Menu_DeleteTest
                                    oMnuItem = Nothing
                                End If
                            End If

                            
                         


                            'CQM
                            'Try

                            If IsCQMConceptDisplay = False Then
                                Dim i As Integer = 0
                                oContxMenu.Items.Add(New ToolStripSeparator)

                                oMnuItem = New ToolStripMenuItem
                                oMnuItem.Text = "CQM Categories" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                                oMnuItem.Image = ImgContextMenu.Images("CQMCategories.ico")
                                oContxMenu.Items.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_CodificationForResult
                                oMnuItem = Nothing


                                Dim dtcqm As DataTable = Get_CQMConceptIDetails(nPatientID, _CurrentOrderID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)))

                                If Not IsNothing(dtcqm) Then

                                    If dtcqm.Rows.Count > 0 Then


                                        If dtcqm.Rows(i)("sValueSetOID") <> "" AndAlso dtcqm.Rows(i)("sValueSetName") <> "" Then

                                            oMnuItem = New ToolStripMenuItem
                                            oMnuItem.Text = "View CQM Categories" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftV : oMnuItem.ShowShortcutKeys = False
                                            oMnuItem.Image = ImgContextMenu.Images("ViewCQMCategories.ico")
                                            oContxMenu.Items.Add(oMnuItem)

                                            oMnuItemDMS = New ToolStripMenuItem
                                            oMnuItemDMS.Text = "Remove CQM Categories" : oMnuItemDMS.ShortcutKeys = Shortcut.CtrlShiftR : oMnuItemDMS.ShowShortcutKeys = False
                                            oMnuItemDMS.Image = ImgContextMenu.Images("RemoveCQMCategories.ico")
                                            oContxMenu.Items.Add(oMnuItemDMS)



                                            oMnuSubItem = New ToolStripMenuItem
                                            oMnuSubItem.Text = dtcqm.Rows(i)("sValueSetName")
                                            oMnuSubItem.Tag = dtcqm.Rows(i)("sValueSetName")
                                            oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                            oMnuItem.DropDownItems.Add(oMnuSubItem)
                                            oMnuSubItem = Nothing

                                            'Remove Document
                                            oMnuSubItem = New ToolStripMenuItem
                                            oMnuSubItem.Text = dtcqm.Rows(i)("sValueSetName")
                                            oMnuSubItem.Tag = dtcqm.Rows(i)("sValueSetName")
                                            oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                            oMnuItemDMS.DropDownItems.Add(oMnuSubItem)
                                            AddHandler oMnuSubItem.Click, AddressOf Remove_CQMCategories
                                            oMnuSubItem = Nothing


                                        End If



                                        oMnuItemDMS = Nothing
                                        oMnuItem = Nothing

                                        '----------------------------------------------
                                        oContxMenu.Items.Add(New ToolStripSeparator)
                                        'ConceptID

                                        oMnuItem = New ToolStripMenuItem
                                        oMnuItem.Text = "ConceptID" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                                        oMnuItem.Image = ImgContextMenu.Images("ConceptID.ico")
                                        oContxMenu.Items.Add(oMnuItem)
                                        AddHandler oMnuItem.Click, AddressOf CodificationForConceptId
                                        oMnuItem = Nothing



                                        ' Dim dtconceptID As DataTable = Get_CQMConceptIDetails(nPatientID, _CurrentOrderID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)))

                                        If Not IsNothing(dtcqm) Then

                                            If dtcqm.Rows.Count > 0 Then


                                                If dtcqm.Rows(i)("sConceptID") <> "" Then


                                                    oMnuItem = New ToolStripMenuItem
                                                    oMnuItem.Text = "View ConceptID" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftV : oMnuItem.ShowShortcutKeys = False
                                                    oMnuItem.Image = ImgContextMenu.Images("ViewConceptID.ico")
                                                    oContxMenu.Items.Add(oMnuItem)

                                                  

                                                    oMnuItemDMS = New ToolStripMenuItem
                                                    oMnuItemDMS.Text = "Remove ConceptID" : oMnuItemDMS.ShortcutKeys = Shortcut.CtrlShiftR : oMnuItemDMS.ShowShortcutKeys = False
                                                    oMnuItemDMS.Image = ImgContextMenu.Images("RemoveConceptID.ico")
                                                    oContxMenu.Items.Add(oMnuItemDMS)

                                                  

                                                    oMnuSubItem = New ToolStripMenuItem
                                                    oMnuSubItem.Text = dtcqm.Rows(i)("sConceptID")
                                                    'oMnuSubItem.Tag = dtcqm.Rows(i)("sValueSetName")
                                                    oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                                    oMnuItem.DropDownItems.Add(oMnuSubItem)
                                                    oMnuSubItem = Nothing

                                                    'Remove Document
                                                    oMnuSubItem = New ToolStripMenuItem
                                                    oMnuSubItem.Text = dtcqm.Rows(i)("sConceptID")
                                                    'oMnuSubItem.Tag = dtcqm.Rows(i)("sValueSetName")
                                                    oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")
                                                    oMnuItemDMS.DropDownItems.Add(oMnuSubItem)
                                                    AddHandler oMnuSubItem.Click, AddressOf Remove_ConceptID
                                                    oMnuSubItem = Nothing



                                                End If


                                            End If
                                            oMnuItemDMS = Nothing
                                            oMnuItem = Nothing

                                        End If
                                    End If
                                End If

                                If Not IsNothing(dtcqm) Then
                                    dtcqm.Dispose()
                                    dtcqm = Nothing
                                End If
                            End If
                            'End If
                            '--------------------------------------------------------------
                            '//Scan Document Functionality - Finish //

                            '/*** Added for Dicom Image Viewing--Added by madan,... ///
                            If Val(_Flex.GetData(_tmpDataRow, COL_DICOMID)) > 0 Then
                                oMnuItem = New ToolStripMenuItem
                                oMnuItem.Text = "View DICOM File" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftV : oMnuItem.ShowShortcutKeys = False
                                'Sandip : 20140717 1229
                                'Bug #71084: GUI - Orders & Results - Orders tab - DICOM Import from HL7 service , application is not displaying DICOM icon on View DICOM.
                                'Changes Done: 
                                '1: Added the Image in context menu at design time;
                                '2: Changed the Case of letters in word Dicom to DICOM;
                                oMnuItem.Image = ImgContextMenu.Images("View DICOM File.ico")
                                'Bug #71084: GUI - Orders & Results - Orders tab - DICOM Import from HL7 service , application is not displaying DICOM icon on View DICOM.
                                '**Sandip : 20140717 1229
                                oContxMenu.Items.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_ViewDicomDocument
                                oMnuItem = Nothing
                            End If

                            ''End of madan.


                            If usePreferredLabforIndividualTest = True Then
                                'Performing Lab
                                oContxMenu.Items.Add(New ToolStripSeparator)
                                oMnuItem = New ToolStripMenuItem
                                oMnuItem.Text = "Performing Lab" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                                oMnuItem.Image = ImgContextMenu.Images("Lab.ico")
                                oContxMenu.Items.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_PrefferedLab
                                oMnuItem = Nothing
                            End If
                           

                            Try
                                If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                                    If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                                        _Flex.ContextMenuStrip.Items.Clear()
                                    End If
                                    _Flex.ContextMenuStrip.Dispose()
                                    _Flex.ContextMenuStrip = Nothing
                                End If
                            Catch

                            End Try

                            '20-May-15 Aniket: Resolving Bug #81110 ( Modified): Orders & results : Application opens orders tab when user try to enter results in Order entery screen. [Menu Focus]
                            ChangeZOrder()
                            _Flex.ContextMenuStrip = oContxMenu
                            ChangeZOrder()


                            _CurrentRow = _tmpDataRow
                            omnuNode = Nothing
                            _mnuStart = Nothing
                            _mnuFinish = Nothing
                            _mnuRange = Nothing
                            _BlockRemove = Nothing
                            dvURLDoc.Dispose()
                            dvURLDoc = Nothing
                        End If
                        'End If
                        'ElseIf _TransactionType = enumTransactionType.LabResult Then
                        'If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test Then
                        ' ''Test - Add Result
                        'If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                        '    '_tmpTestResultID = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID) & "")
                        '    oContxMenu.MenuItems.Clear()

                        '    'Menu Object
                        '    Dim oMnuItem As MenuItem
                        '    oMnuItem = New MenuItem
                        '    oMnuItem.Text = "Add Result" : oMnuItem.Shortcut = Shortcut.CtrlShiftR : oMnuItem.ShowShortcut = False
                        '    oContxMenu.MenuItems.Add(oMnuItem)
                        '    AddHandler oMnuItem.Click, AddressOf Set_Menu_AddResult
                        '    oMnuItem = Nothing

                        '    _Flex.ContextMenu = oContxMenu

                        '    _CurrentRow = _tmpDataRow
                        'End If
                    ElseIf Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.ResultHeader Then
                        'Result - Modify Result
                        If Val(_Flex.GetData(_tmpDataRow, COL_RESULT_NUMBER)) > 0 Then
                            If Val(_Flex.GetData(_tmpDataRow, COL_ISFINISHED)) = 0 Then
                                '_tmpTestResultID = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID) & "")
                                oContxMenu.Items.Clear()
                                'Menu Object
                                Dim oMnuItem As ToolStripMenuItem
                                oMnuItem = New ToolStripMenuItem
                                oMnuItem.Text = "Modify Result" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftM : oMnuItem.ShowShortcutKeys = False
                                oContxMenu.Items.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_ModifyResult
                                oMnuItem = Nothing
                                ''Added by madan on 20100901-- for previous histroy functionality.
                                If _IsLoadLastTransaction = False Then
                                    'Menu Object
                                    oMnuItem = New ToolStripMenuItem
                                    oMnuItem.Text = "Delete Result" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftD : oMnuItem.ShowShortcutKeys = False
                                    oContxMenu.Items.Add(oMnuItem)
                                    AddHandler oMnuItem.Click, AddressOf Set_Menu_DeleteResult
                                    oMnuItem = Nothing
                                End If
                                Try
                                    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                                        gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                                        If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                                            _Flex.ContextMenuStrip.Items.Clear()
                                        End If
                                        _Flex.ContextMenuStrip.Dispose()
                                        _Flex.ContextMenuStrip = Nothing
                                    End If
                                Catch

                                End Try

                                '20-May-15 Aniket: Resolving Bug #81110 ( Modified): Orders & results : Application opens orders tab when user try to enter results in Order entery screen. [Menu Focus]
                                ChangeZOrder()
                                _Flex.ContextMenuStrip = oContxMenu
                                ChangeZOrder()
                                _CurrentRow = _tmpDataRow
                            End If
                        End If
                    ElseIf Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Result Then

                        If Val(_Flex.GetData(_tmpDataRow, COL_RESULT_NUMBER)) > 0 Then
                            oContxMenu.Items.Clear()
                            'Menu Object
                            Dim oMnuItem As ToolStripMenuItem
                            oMnuItem = New ToolStripMenuItem
                            If _Flex.GetData(_tmpDataRow, COL_RESULT_COMMENT) <> "" Then
                                oMnuItem.Text = "Modify Comment" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftC : oMnuItem.ShowShortcutKeys = False
                            Else
                                oMnuItem.Text = "Add Comment" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftA : oMnuItem.ShowShortcutKeys = False
                            End If
                            oContxMenu.Items.Add(oMnuItem)
                            AddHandler oMnuItem.Click, AddressOf Set_Menu_Comment
                            oMnuItem = Nothing
                            Try
                                If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                                    If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                                        _Flex.ContextMenuStrip.Items.Clear()
                                    End If
                                    _Flex.ContextMenuStrip.Dispose()
                                    _Flex.ContextMenuStrip = Nothing
                                End If
                            Catch

                            End Try

                            '20-May-15 Aniket: Resolving Bug #81110 ( Modified): Orders & results : Application opens orders tab when user try to enter results in Order entery screen. [Menu Focus]
                            ChangeZOrder()
                            _Flex.ContextMenuStrip = oContxMenu
                            ChangeZOrder()
                            _CurrentRow = _tmpDataRow
                        End If

                        '''' Commented by Pramod For CCHIT 2007 Start
                        'ElseIf Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Result Then ' code updates on 2007/07/10 By - Bipin
                        '    If Val(_Flex.GetData(_tmpDataRow, COL_RESULT_NUMBER)) > 0 Then
                        '        oContxMenu.MenuItems.Clear()
                        '        'Menu Object
                        '        Dim oMnuItem As MenuItem
                        '        oMnuItem = New MenuItem
                        '        oMnuItem.Text = "View Graph" : oMnuItem.Shortcut = Shortcut.CtrlShiftG : oMnuItem.ShowShortcut = False
                        '        oContxMenu.MenuItems.Add(oMnuItem)
                        '        AddHandler oMnuItem.Click, AddressOf View_Graphs
                        '        oMnuItem = Nothing
                        '        'Menu Object
                        '        oMnuItem = New MenuItem
                        '        oMnuItem.Text = "View Graph With Selection Criteria" : oMnuItem.Shortcut = Shortcut.CtrlShiftD : oMnuItem.ShowShortcut = False
                        '        oContxMenu.MenuItems.Add(oMnuItem)
                        '        AddHandler oMnuItem.Click, AddressOf View_Graphs_criteria
                        '        oMnuItem = Nothing
                        '        _Flex.ContextMenu = oContxMenu
                        '        _CurrentRow = _tmpDataRow
                        '    End If
                        '''' Commented by Pramod For CCHIT 2007 End
                    End If
                    '' End If  '' ' If _TransactionType = enumTransactionType.LabOrder Then
                End If


            Else

                'start code added by manoj on 10121127
                Try
                    If Cursor Is Cursors.Hand Then
                        ht = _Flex.HitTest(e.X, e.Y)
                        If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf _Flex(ht.Row, ht.Column) Is Hyperlink Then
                            _Flex(ht.Row, ht.Column).activate()
                        End If
                    End If

                Catch ex As Exception
                    ex = Nothing
                Finally
                    ht = Nothing
                End Try
                'end of  code added by manoj on 10121127


                If e.Button = Windows.Forms.MouseButtons.Left Then
                    Dim _tmpDataColumn As Int16 = _Flex.HitTest(e.X, e.Y).Column

                    If _tmpDataColumn = COL_LAB_INFOBUTTON Then


                        If _CurrentOrderID > 0 Then
                            RaiseEvent LockOrder(_CurrentOrderID)
                        End If

                        If _IsOrderLocked Then
                            Return
                        End If

                        Try
                            If (IsNothing(oContxMenu) = False) Then
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(oContxMenu)
                                If (IsNothing(oContxMenu.Items) = False) Then
                                    oContxMenu.Items.Clear()
                                End If
                                oContxMenu.Dispose()
                                oContxMenu = Nothing
                            End If
                        Catch

                        End Try

                        oContxMenu = New ContextMenuStrip
                        Dim _tmpTestResultID As Int64 = 0
                        Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                        _Flex.Select(_tmpDataRow, True)

                        ''Infobutton
                        Dim dtEdu As DataTable
                        Dim clsinfobutton_Lab As New gloEMRGeneralLibrary.clsInfobutton
                        Dim dtPatient As DataTable = clsinfobutton_Lab.GetPatientInfo(PatientID)

                        Dim strPatientAge As Integer
                        Dim strPatientGender As String = ""
                        If (IsNothing(dtPatient) = False) Then


                            If dtPatient.Rows.Count > 0 Then
                                strPatientAge = calculateAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                                strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                            End If
                            dtPatient.Dispose()
                            dtPatient = Nothing
                        End If

                        dtEdu = clsinfobutton_Lab.GetEducationMaterial(Convert.ToString(_Flex.GetData(_tmpDataRow, COL_LAB_TEST_CODE)), "2.16.840.1.113883.6.1", strPatientAge, strPatientGender)
                        clsinfobutton_Lab = Nothing
                        If Not IsNothing(dtEdu) Then
                            If dtEdu.Rows.Count > 0 Then
                                If _tmpDataRow > 0 Then
                                    If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test Then
                                        If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                                            'Menu Object
                                            Dim oMnuItem As ToolStripMenuItem
                                            Dim oMnuSubItem As ToolStripMenuItem
                                            ''Added For Infobutton  implementation -28/08/2013
                                            'If ParentControl <> "" Then
                                            If isEducationMaterialEnables Then
                                                If Not IsNothing(dtEdu) Then
                                                    If dtEdu.Rows.Count > 0 Then

                                                        oMnuItem = New ToolStripMenuItem
                                                        oMnuItem.Text = "Infobutton"
                                                        oMnuItem.Tag = "Infobutton"
                                                        oMnuItem.Image = My.Resources.infobutton
                                                        AddHandler oMnuItem.Click, AddressOf OpenOnlineDocument
                                                        oContxMenu.Items.Add(oMnuItem)
                                                        oMnuItem = Nothing

                                                        oMnuItem = New ToolStripMenuItem
                                                        oMnuItem.Image = My.Resources.Patient_reference_material_img
                                                        oMnuItem.Text = "Patient Reference Material" ': oMnuItem.Shortcut = Shortcut.CtrlShiftE : oMnuItem.ShowShortcut = False
                                                        For i As Integer = 0 To dtEdu.Rows.Count - 1
                                                            If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 1 Then
                                                                If Convert.ToDouble(dtEdu.Rows(i)("nValueOne")) = 0 And Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) = 0 And Convert.ToDouble(dtEdu.Rows(i)("nOperator")) = 0 Then
                                                                    oMnuSubItem = New ToolStripMenuItem
                                                                    oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                    oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Patient Reference Material"
                                                                    oMnuItem.DropDownItems.Add(oMnuSubItem)
                                                                    AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                    oMnuSubItem = Nothing
                                                                End If
                                                            End If
                                                        Next
                                                        If oMnuItem.DropDownItems.Count > 0 Then
                                                            oContxMenu.Items.Add(oMnuItem)
                                                        End If
                                                        oMnuItem = Nothing

                                                        oMnuItem = New ToolStripMenuItem
                                                        oMnuItem.Image = My.Resources.Provider_reference_material_img
                                                        oMnuItem.Text = "Provider Reference Material" ': oMnuItem.Shortcut = Shortcut.CtrlShiftM : oMnuItem.ShowShortcut = False
                                                        For i As Integer = 0 To dtEdu.Rows.Count - 1
                                                            If Convert.ToDouble(dtEdu.Rows(i)("nValueOne")) = 0 And Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) = 0 And Convert.ToDouble(dtEdu.Rows(i)("nOperator")) = 0 Then
                                                                If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 2 Then
                                                                    If Convert.ToBoolean(dtEdu.Rows(i)("bIsAdvancedProviderReference")) Then
                                                                        If isAdvancedReference = True Then
                                                                            oMnuSubItem = New ToolStripMenuItem
                                                                            oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                            oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                                            oMnuItem.DropDownItems.Add(oMnuSubItem)
                                                                            AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                            oMnuSubItem = Nothing
                                                                        End If
                                                                    Else
                                                                        oMnuSubItem = New ToolStripMenuItem
                                                                        oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                        oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                                        oMnuItem.DropDownItems.Add(oMnuSubItem)
                                                                        AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                        oMnuSubItem = Nothing
                                                                    End If
                                                                End If
                                                            End If
                                                        Next
                                                        If oMnuItem.DropDownItems.Count > 0 Then
                                                            oContxMenu.Items.Add(oMnuItem)
                                                        End If
                                                        oMnuItem = Nothing

                                                    End If
                                                End If
                                            End If
                                            ' End If
                                            Try
                                                If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                                                    If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                                                        _Flex.ContextMenuStrip.Items.Clear()
                                                    End If
                                                    _Flex.ContextMenuStrip.Dispose()
                                                    _Flex.ContextMenuStrip = Nothing
                                                End If
                                            Catch

                                            End Try

                                            '20-May-15 Aniket: Resolving Bug #81110 ( Modified): Orders & results : Application opens orders tab when user try to enter results in Order entery screen. [Menu Focus]
                                            ChangeZOrder()
                                            _Flex.ContextMenuStrip = oContxMenu
                                            ChangeZOrder()

                                            _CurrentRow = _tmpDataRow
                                        End If
                                    ElseIf Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Result Then
                                        If Val(_Flex.GetData(_tmpDataRow, COL_RESULT_NUMBER)) > 0 Then
                                            oContxMenu.Items.Clear()
                                            'Menu Object
                                            Dim oMnuItem As ToolStripMenuItem
                                            Dim oMnuItem_Patient As ToolStripMenuItem
                                            Dim oMnuItem_Provider As ToolStripMenuItem
                                            Dim oMnuSubItem As ToolStripMenuItem
                                            ''Infobutton
                                            'If ParentControl <> "" Then
                                            If isEducationMaterialEnables Then
                                                If Not IsNothing(dtEdu) Then
                                                    If dtEdu.Rows.Count > 0 Then
                                                        Dim bAddMenu As Boolean = False
                                                        oMnuItem_Patient = New ToolStripMenuItem
                                                        oMnuItem_Provider = New ToolStripMenuItem

                                                        oMnuItem_Patient.Text = "Patient Reference Material" ': oMnuItem_Patient.Shortcut = Shortcut.CtrlShiftE : oMnuItem_Patient.ShowShortcut = False
                                                        oMnuItem_Patient.Image = My.Resources.Patient_reference_material_img
                                                        oMnuItem_Provider.Text = "Provider Reference Material" ': oMnuItem_Provider.Shortcut = Shortcut.CtrlShiftM : oMnuItem_Provider.ShowShortcut = False
                                                        oMnuItem_Provider.Image = My.Resources.Provider_reference_material_img

                                                        oMnuItem = New ToolStripMenuItem
                                                        oMnuItem.Text = "Infobutton"
                                                        oMnuItem.Tag = "Infobutton"
                                                        oMnuItem.Image = My.Resources.infobutton
                                                        AddHandler oMnuItem.Click, AddressOf OpenOnlineDocument
                                                        oContxMenu.Items.Add(oMnuItem)
                                                        oMnuItem = Nothing

                                                        For i As Integer = 0 To dtEdu.Rows.Count - 1
                                                            If Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 1 Then '"Between"
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) >= Convert.ToDouble(dtEdu.Rows(i)("nValueOne")) And Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 2 Then ' "Equal"
                                                                If Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) = Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 3 Then '"Greater than"
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) > Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 4 Then ' "Less than"
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 0 Then ' "No Criteria"
                                                                bAddMenu = True
                                                            End If



                                                            If bAddMenu Then
                                                                If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 1 Then
                                                                    oMnuSubItem = New ToolStripMenuItem
                                                                    oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                    oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Patient Reference Material"
                                                                    oMnuItem_Patient.DropDownItems.Add(oMnuSubItem)
                                                                    AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                    oMnuSubItem = Nothing
                                                                ElseIf Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 2 Then
                                                                    If Convert.ToBoolean(dtEdu.Rows(i)("bIsAdvancedProviderReference")) Then
                                                                        If isAdvancedReference = True Then
                                                                            oMnuSubItem = New ToolStripMenuItem
                                                                            oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                            oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                                            oMnuItem_Provider.DropDownItems.Add(oMnuSubItem)
                                                                            AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                            oMnuSubItem = Nothing
                                                                        End If
                                                                    Else
                                                                        oMnuSubItem = New ToolStripMenuItem
                                                                        oMnuSubItem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                                                        oMnuSubItem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                                        oMnuItem_Provider.DropDownItems.Add(oMnuSubItem)
                                                                        AddHandler oMnuSubItem.Click, AddressOf OpenInfoDocument
                                                                        oMnuSubItem = Nothing
                                                                    End If
                                                                End If
                                                            End If
                                                            bAddMenu = False
                                                        Next
                                                        If oMnuItem_Patient.DropDownItems.Count > 0 Then
                                                            oContxMenu.Items.Add(oMnuItem_Patient)
                                                        End If

                                                        If oMnuItem_Provider.DropDownItems.Count > 0 Then
                                                            oContxMenu.Items.Add(oMnuItem_Provider)
                                                        End If

                                                        If oMnuItem_Patient.DropDownItems.Count <= 0 And oMnuItem_Provider.DropDownItems.Count <= 0 Then
                                                            oContxMenu.Items.Clear()
                                                            'Open Online Document
                                                            OpenOnlineDocument()
                                                        End If

                                                        oMnuItem_Patient = Nothing
                                                        oMnuItem_Provider = Nothing

                                                    End If
                                                End If
                                            End If
                                            'End If
                                            Try
                                                If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                                                    If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                                                        _Flex.ContextMenuStrip.Items.Clear()
                                                    End If
                                                    _Flex.ContextMenuStrip.Dispose()
                                                    _Flex.ContextMenuStrip = Nothing
                                                End If
                                            Catch

                                            End Try

                                            '20-May-15 Aniket: Resolving Bug #81110 ( Modified): Orders & results : Application opens orders tab when user try to enter results in Order entery screen. [Menu Focus]
                                            ChangeZOrder()
                                            _Flex.ContextMenuStrip = oContxMenu
                                            ChangeZOrder()
                                            _CurrentRow = _tmpDataRow
                                        End If
                                    End If
                                End If
                                _Flex.ContextMenuStrip.Visible = True
                                _Flex.ContextMenuStrip.Show(CType(sender, Control), e.Location)

                            Else
                                'Open Online Document
                                OpenOnlineDocument()
                            End If
                            dtEdu.Dispose()
                            dtEdu = Nothing
                        Else
                            'Open Online Document
                            OpenOnlineDocument()
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            ' Dim oContxMenu As New ContextMenuStrip
        End Try

    End Sub

    Private Sub ChangeZOrder()
        Try
            If (IsNothing(Me.ParentForm) = False) Then
                Me.ParentForm.BringToFront()
                If (IsNothing(Me.ParentForm.Owner) = False) Then
                    Me.ParentForm.Owner.SendToBack()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function Lab_GetURLDocument(ByVal OrderID As Int64, ByVal TestID As Int64) As DataTable
        Dim con As SqlConnection
        Dim SqlCmd As SqlCommand
        Dim objParam As SqlParameter
        Dim dtURLdocument As New DataTable
        Try

            con = New SqlConnection(_ConnectionString)
            SqlCmd = New SqlCommand("Lab_GetURLDocument", con)
            SqlCmd.CommandType = CommandType.StoredProcedure

            objParam = New SqlParameter
            objParam = SqlCmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OrderID
            objParam = Nothing

            objParam = New SqlParameter
            objParam = SqlCmd.Parameters.Add("@TestID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TestID
            objParam = Nothing

            con.Open()
            Dim objDA As New SqlDataAdapter(SqlCmd)
            objDA.Fill(dtURLdocument)
            objDA.Dispose()
            objDA = Nothing
            con.Close()

            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If

            If Not IsNothing(SqlCmd) Then
                SqlCmd.Parameters.Clear()
                SqlCmd.Dispose()
                SqlCmd = Nothing
            End If

            Return dtURLdocument

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Return Nothing
        Finally
            'If Not IsNothing(dtURLdocument) Then
            '    dtURLdocument.Dispose()
            '    dtURLdocument = Nothing
            'End If
        End Try

    End Function

    Private Sub View_Graphs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Testnode As C1.Win.C1FlexGrid.Node

        With _Flex
            Testnode = .Rows(.Row).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root)
        End With

        _strLabResultName = ""
        _strLabTestName = ""
        _nLabTestId = 0
        _nLabResultId = 0

        _strLabResultName = _Flex.GetData(_Flex.Row, COL_TEST_RESULT_NAME)
        '_strLabTestName = _Flex.GetData(nTestRow, COL_TEST_RESULT_NAME)
        '_strLabTestName = _Flex(Testnode.Row.Item(COL_TEST_RESULT_NAME), COL_TEST_RESULT_NAME)
        _strLabTestName = Testnode.Row.Item(COL_TEST_RESULT_NAME)

        _nLabTestId = _Flex.GetData(Testnode.Row.Index, COL_TESTID)
        _nLabResultId = _Flex.GetData(_Flex.Row, COL_RESULT_NAMEID)
        _dtSelectedToDt = _Flex.GetData(_Flex.Row, COL_TEST_RESULT_DATETIME)

        'Dim nFirstResultRow As Integer = 0
        'nFirstResultRow = Testnode.Row.Index + 2
        '_dtSelectedToDt = _Flex.GetData(nFirstResultRow, COL_TEST_RESULT_DATETIME)

        RaiseEvent ShowGraph(sender, e)
    End Sub

    Private Sub View_Graphs_criteria(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Testnode As C1.Win.C1FlexGrid.Node

        With _Flex
            Testnode = .Rows(.Row).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root)
        End With

        _strLabResultName = ""
        _strLabTestName = ""
        _nLabTestId = 0
        _nLabResultId = 0

        _strLabResultName = _Flex.GetData(_Flex.Row, COL_TEST_RESULT_NAME)
        '_strLabTestName = _Flex.GetData(nTestRow, COL_TEST_RESULT_NAME)
        '_strLabTestName = _Flex(Testnode.Row.Item(COL_TEST_RESULT_NAME), COL_TEST_RESULT_NAME)
        _strLabTestName = Testnode.Row.Item(COL_TEST_RESULT_NAME)

        _nLabTestId = _Flex.GetData(Testnode.Row.Index, COL_TESTID)
        _nLabResultId = _Flex.GetData(_Flex.Row, COL_RESULT_NAMEID)
        _dtSelectedToDt = _Flex.GetData(_Flex.Row, COL_TEST_RESULT_DATETIME)

        'Dim nFirstResultRow As Integer = 0
        'nFirstResultRow = Testnode.Row.Index + 2
        '_dtSelectedToDt = _Flex.GetData(nFirstResultRow, COL_TEST_RESULT_DATETIME)

        RaiseEvent ShowGraph_crieteria(sender, e)
    End Sub

    ''Infobutton
    Private Sub OpenInfoDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim oCurrentMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim tag() As String = oCurrentMenu.Tag.ToString().Split("-")
            Dim templateName As String = oCurrentMenu.Text
            Dim nTempId As Int64 = CType(tag(0), Int64)
            Dim OpenFor As String = tag(1).ToString()
            OpenFor = OpenFor + "-" + templateName
            If _ParentControl = "" Then
                RaiseEvent gUC_InfoButtonDocumentClickedDB(nTempId, OpenFor, templateName, tag(1))
            Else
                RaiseEvent gUC_InfoButtonDocumentClicked(nTempId, OpenFor, templateName, tag(1))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub OpenOnlineDocument()
        Try
            ''Infobutton
            If Cursor Is Cursors.Default Then

                'If _tmpDataCol = COL_LAB_INFOBUTTON Then
                ''Infobutton
                If Val(_Flex.GetData(_Flex.RowSel, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                    ' Dim TestLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_TEST_LOINC))
                    Dim TestLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_LAB_TEST_CODE))
                    If TestLCode = "" Then
                        MessageBox.Show("LOINC code is not available for selected test", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        If _ParentControl = "" Then
                            RaiseEvent gUC_InfoButtonClickedDB(TestLCode)
                        Else
                            RaiseEvent gUC_InfoButtonClicked(TestLCode)
                        End If
                    End If
                ElseIf Val(_Flex.GetData(_Flex.RowSel, COL_RECORDTYPE) & "") = Val(enumRecordType.Result) Then
                    'Dim TestLCode As String = ""
                    'For i As Integer = _tmpDataRow To 0 Step -1
                    '    If Val(_Flex.GetData(i, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                    '        TestLCode = Convert.ToString(_Flex.GetData(_CurrentRow, COL_TEST_LOINC))
                    '        Exit For
                    '    End If
                    'Next
                    '  Dim ResultLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_TEST_LOINC))
                    Dim ResultLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_LAB_TEST_CODE))
                    'Dim ResultValue = Convert.ToString(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))
                    'Dim ResultValueUnit = Convert.ToString(_Flex.GetData(_tmpDataRow, COL_TEST_CPT_RESULT_UNIT))
                    If ResultLCode = "" Then
                        MessageBox.Show("LOINC code is not available for selected result", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        If _ParentControl = "" Then
                            RaiseEvent gUC_InfoButtonClickedDB(ResultLCode)
                        Else
                            RaiseEvent gUC_InfoButtonClicked(ResultLCode)
                        End If
                    End If
                End If
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub EnableSearchClear()

        Dim ob As Object

        Try

            ob = Me.ParentForm
            If Not IsNothing(ob) Then
                If (ob.name = "frmViewNormalLab") Then

                    If Not IsNothing(ob) AndAlso Not IsNothing(ob.pnlOrder) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1")) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail")) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnClearPL) Then
                        ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnClearPL.Enabled = True
                        ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnSearchPL.Enabled = True
                    End If

                ElseIf (ob.name = "frmViewgloLab") Then

                    If Not IsNothing(ob) AndAlso Not IsNothing(ob.pnlmain) AndAlso Not IsNothing(ob.pnlmain.Controls("gloUCLab_OrderDetail")) AndAlso Not IsNothing(ob.pnlmain.Controls("gloUCLab_OrderDetail").btnClearPL) Then
                        ob.pnlmain.Controls("gloUCLab_OrderDetail").btnClearPL.Enabled = True
                        ob.pnlmain.Controls("gloUCLab_OrderDetail").btnSearchPL.Enabled = True

                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DisableSearchClear()

        Dim ob As Object

        Try

            ob = Me.ParentForm
            If Not IsNothing(ob) Then
                If (ob.name = "frmViewNormalLab") Then

                    If Not IsNothing(ob) AndAlso Not IsNothing(ob.pnlOrder) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1")) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail")) AndAlso Not IsNothing(ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnClearPL) Then
                        ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnClearPL.Enabled = False
                        ob.pnlOrder.Controls("panel1").Controls("gloUCLab_OrderDetail").btnSearchPL.Enabled = False
                    End If

                ElseIf (ob.name = "frmViewgloLab") Then

                    If Not IsNothing(ob) AndAlso Not IsNothing(ob.pnlmain) AndAlso Not IsNothing(ob.pnlmain.Controls("gloUCLab_OrderDetail")) AndAlso Not IsNothing(ob.pnlmain.Controls("gloUCLab_OrderDetail").btnClearPL) Then
                        ob.pnlmain.Controls("gloUCLab_OrderDetail").btnClearPL.Enabled = False
                        ob.pnlmain.Controls("gloUCLab_OrderDetail").btnSearchPL.Enabled = False

                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Remove_CQMCategories(ByVal sender As Object, ByVal e As System.EventArgs)


        Try
            Dim nStyleRow As Int16 = 0
            If MessageBox.Show("Are you sure you want to remove CQM Categories", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                With _Flex
                    If _CurrentRow > 0 Then
                        If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                            _CurrentRow = _CurrentRow
                            Dim _tmpTestID As Int64 = 0

                            If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then

                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")


                                Update_CQMCategories(_tmpTestID, "", "", 1, "", "")


                            End If

                        End If
                    End If

                End With
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Remove_ConceptID(ByVal sender As Object, ByVal e As System.EventArgs)


        Try
            Dim nStyleRow As Int16 = 0
            If MessageBox.Show("Are you sure you want to remove ConceptID", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                With _Flex
                    If _CurrentRow > 0 Then
                        If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                            _CurrentRow = _CurrentRow
                            Dim _tmpTestID As Int64 = 0

                            If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then

                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")



                                Update_CQMCategories(_tmpTestID, "", "", 2, "", "")



                            End If

                        End If
                    End If

                End With
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub Set_Menu_Comment(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'Date : 10/29/2013
            If _CurrentOrderID > 0 Then
                RaiseEvent LockOrder(_CurrentOrderID)
            End If
            _LabModified = True

            txtTestResultComment.Visible = False
            Splitter1.Visible = False
            pnlInstruction.Visible = True
            pnlInstruction.Dock = DockStyle.Fill
            pnlInstruction.BringToFront()
            txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_RESULT_COMMENT)
            lblTestDetails_Header.Text = "      Comments for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Set_Menu_AddResult(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow

                        DesignResultGrid()
                        c1Results.Cols(COL_LAB_INFOBUTTON).Width = 0
                        '//--//


                        With c1Results
                            Try
                                _CurrentTestResultIsModify = False
                                '//--TEST NODE--//
                                Dim _tmpTestID As Int64

                                _tmpTestID = _Flex.GetData(_CurrentRow, COL_TESTID)
                                _CurrentTestSelectedID = _Flex.GetData(_CurrentRow, COL_TESTID)
                                _CurrentTestLineNo = _Flex.GetData(_CurrentRow, COL_TEST_LINENO)

                                _CurrentTestResultSelectedCounterID = 0
                                Dim oRange As C1.Win.C1FlexGrid.CellRange
                                Dim nStart As Int16 = 0
                                Dim nEnd As Int16 = 0

                                oRange = _Flex.Rows(_CurrentRow).Node.GetCellRange
                                nStart = oRange.TopRow : nEnd = oRange.BottomRow
                                For l As Int16 = nEnd To nStart Step -1
                                    If _Flex.GetData(l, COL_RECORDTYPE) = enumRecordType.ResultHeader Then
                                        _CurrentTestResultSelectedCounterID = Val(_Flex.GetData(l, COL_RESULT_NUMBER))
                                        Exit For
                                    End If
                                Next
                                _CurrentTestResultSelectedCounterID = _CurrentTestResultSelectedCounterID + 1

                                oRange = Nothing


                                'Date : 10/29/2013
                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                '//--TEST NODE--//

                                Dim _DB As New gloEMRDatabase.DataBaseLayer
                                Dim _strSQL As String = ""
                                Dim oDataTable As DataTable = Nothing

                                _strSQL = "SELECT Lab_Test_ResultDtl.labtrd_ResultID, Lab_Test_ResultDtl.labtrd_ResultName, " _
                                & " Lab_Test_ResultDtl.labtrd_ValueType, Lab_Test_ResultDtl.labtrd_Unit, Lab_Test_ResultDtl.labtrd_DefaultValue, " _
                                & " Lab_Test_ResultDtl.labtrd_RefRange, Lab_Test_ResultDtl.labtrd_LOINCId , Lab_Test_Mst.labtm_Name , ISNULL(Lab_Test_ResultDtl.labtrd_AlternateResultCode,'') as labtrd_AlternateResultCode " _
                                & " FROM Lab_Test_Mst INNER JOIN Lab_Test_ResultDtl ON Lab_Test_Mst.labtm_ID = Lab_Test_ResultDtl.labtrd_TestID " _
                                & " WHERE Lab_Test_Mst.labtm_ID = " & _tmpTestID & " AND Lab_Test_ResultDtl.labtrd_ResultName IS NOT NULL AND Lab_Test_ResultDtl.labtrd_ResultID IS NOT NULL ORDER BY Lab_Test_ResultDtl.labtrd_ResultID"

                                Dim nStyleRow As Int16 = .Rows.Count - 1 ' Set Test Node

                                oDataTable = _DB.GetDataTable_Query(_strSQL)
                                If Not oDataTable Is Nothing Then
                                    If oDataTable.Rows.Count > 0 Then
                                        For i As Int16 = 0 To oDataTable.Rows.Count - 1
                                            .Rows.Add()
                                            nStyleRow = nStyleRow + 1

                                            .SetData(nStyleRow, COL_TEST_RESULT_NAME, oDataTable.Rows(i).Item("labtrd_ResultName") & "")
                                            .SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                                            .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            .SetData(nStyleRow, COL_TESTID, _tmpTestID)
                                            .SetData(nStyleRow, COL_TESTNAME, oDataTable.Rows(i).Item("labtm_Name") & "")
                                            .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                                            .SetData(nStyleRow, COL_TEST_LINENO, _Flex.GetData(_CurrentRow, COL_TEST_LINENO))
                                            .SetData(nStyleRow, COL_RESULT_LINENO, i + 1) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_NAMEID, oDataTable.Rows(i).Item("labtrd_ResultID")) '//Result//
                                            .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "")
                                            .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, Nothing)
                                            .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, oDataTable.Rows(i).Item("labtrd_Unit"))
                                            .SetData(nStyleRow, COL_TEST_CPTBTN, Nothing)
                                            .SetData(nStyleRow, COL_TEST_NOTE, "")
                                            .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                                            .SetData(nStyleRow, COL_TEST_STORAGE, "")
                                            .SetData(nStyleRow, COL_TEST_LOINC, oDataTable.Rows(i).Item("labtrd_LOINCId"))
                                            .SetData(nStyleRow, COL_LAB_TEST_CODE, oDataTable.Rows(i).Item("labtrd_AlternateResultCode"))
                                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                            .SetData(nStyleRow, COL_RESULT_TYPE, "")
                                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, oDataTable.Rows(i).Item("labtrd_ValueType")) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_RANGE, oDataTable.Rows(i).Item("labtrd_RefRange")) '//Result//
                                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_TYPE, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_COMMENT, "") '//Result//
                                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                                            'Mitesh
                                            '  .SetData(nStyleRow, COL_REPORTED_DATETIME, Date.Now)
                                            'Mitesh

                                            'Sanjog
                                            .SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Nothing)
                                            'Sanjog
                                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, _Flex.GetData(_CurrentRow, COL_TEST_RESULT_USERID))
                                            .SetData(nStyleRow, COL_RECORDTYPE, CType(enumRecordType.Result, Integer))
                                            .SetData(nStyleRow, COL_ISFINISHED, 0)
                                            ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                                            'Added by madan-- on 20100409...
                                            .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                                            .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, "")
                                            .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                                            .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                                            'End madan

                                            ''Added by Abhijeet on 20101026
                                            .SetData(nStyleRow, COL_LAB_FACILITY_NAME, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_CITY, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_STATE, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, "")
                                            ''End of changes Added by Abhijeet on 20101026

                                            'Added by madan-- on 20100504
                                            _LabModified = True
                                            'end madan
                                            With .Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                .Style = _Flex.Styles("CS_Record")

                                            End With

                                        Next
                                    End If
                                    oDataTable.Dispose()
                                    oDataTable = Nothing
                                End If
                                _DB.Dispose()
                                _DB = Nothing
                                oDataTable = Nothing

                                .Rows.Count = .Rows.Count + 21

                            Catch objError As Exception
                                'MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
                            Finally

                            End Try

                        End With
                        '//--//

                        _Flex.Visible = False
                        pnlDiagosisCPT.Visible = False
                        pnlResults.Visible = True
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Menu_DeleteTest(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim nStyleRow As Int16 = 0
            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow
                        If MessageBox.Show("Are you sure you want to remove this test?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            'Date : 10/29/2013
                            If _CurrentOrderID > 0 Then
                                RaiseEvent LockOrder(_CurrentOrderID)
                            End If
                            _LabModified = True
                            Dim strTestID As String = _Flex.GetData(_CurrentRow, COL_TESTID)
                            ''dtURLDoc "labotrda_TestID"
                            If Not IsNothing(dtURLDoc) Then
                                Dim drdtURLDoc As DataRow() = dtURLDoc.Select("labotrda_TestID='" + strTestID + "'")

                                For len As Integer = 0 To drdtURLDoc.Length - 1
                                    dtURLDoc.Rows.Remove(drdtURLDoc(len))
                                Next
                            End If

                            Dim oRange As C1.Win.C1FlexGrid.CellRange
                            oRange = .Rows(_CurrentRow).Node.GetCellRange
                            Dim nTestStart As Int16 = oRange.TopRow
                            Dim nTestEnd As Int16 = oRange.BottomRow
                            .Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1)
                            oRange = Nothing
                            'Madan Added on 20100504--- for keeping track of TestAdd and Delete
                            _LabModified = True
                            'End Madan
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oRefusalListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmRefusalList.Close()
    End Sub


    Dim sICDType As String = ""



    Private Sub oRefusalListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oRefusalListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oRefusalListControl.SelectedItems.Count - 1
                    Dim testId As Int64 = 0

                    testId = Convert.ToInt64(_Flex.GetData(flexrow, COL_TESTID))


                    Update_CQMCategories(testId, Convert.ToString(oRefusalListControl.SelectedItems(i).Code), Convert.ToString(oRefusalListControl.SelectedItems(i).Description), 1, "", "")


                Next
                ofrmRefusalList.Close()
            Else

                ofrmRefusalList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        Try

            If oListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                    Dim testId As Int64 = 0
                    testId = Convert.ToInt64(_Flex.GetData(flexrow, COL_TESTID))

                    Update_CQMCategories(testId, "", "", 2, Convert.ToString(oListControl.SelectedItems(i).Code), Convert.ToString(oListControl.SelectedItems(i).Description))

                Next

                ofrmCodeList.Close()
            Else

                ofrmCodeList.Close()
            End If


        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As System.EventArgs)

        ofrmCodeList.Close()

    End Sub


    Private oRefusalListControl As gloListControl.gloListControl
    Private ofrmRefusalList As frmViewListControl



    Private Sub Set_Menu_CodificationForResult(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ofrmRefusalList = New frmViewListControl
            oRefusalListControl = New gloListControl.gloListControl(_ConnectionString, gloListControl.gloListControlType.CQMCategoriesValueSet, False, Me.Width)
            oRefusalListControl.ControlHeader = "CQM Categories"
            'set the property true for refused code you want 
            oRefusalListControl.bShowNotTakenCodes = False
            oRefusalListControl.bShowAttributeCodes = True

            AddHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            AddHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            ofrmRefusalList.Controls.Add(oRefusalListControl)
            oRefusalListControl.Dock = DockStyle.Fill
            oRefusalListControl.BringToFront()

            oRefusalListControl.ShowHeaderPanel(False)

            ofrmRefusalList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmRefusalList.Text = "CQM Categories"
            ofrmRefusalList.ShowDialog(IIf(IsNothing(CType(ofrmRefusalList, Control).Parent), Me, CType(ofrmRefusalList, Control).Parent))


            If (IsNothing(oRefusalListControl) = False) Then
                ofrmRefusalList.Controls.Remove(oRefusalListControl)
                RemoveHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
                RemoveHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
                oRefusalListControl.Dispose()
                oRefusalListControl = Nothing
            End If

            If IsNothing(ofrmRefusalList) = False Then
                ofrmRefusalList.Dispose()
                ofrmRefusalList = Nothing
            End If

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private oListControl As gloListControl.gloListControl
    Private ofrmCodeList As frmViewListControl


    Private Sub CodificationForConceptId(ByVal sender As Object, ByVal e As System.EventArgs)


        Try

            ofrmCodeList = New frmViewListControl()
            oListControl = New gloListControl.gloListControl(gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.ConceptID, False, Me.Width)
            oListControl.ControlHeader = "Concept ID"
            oListControl.bShowNotTakenCodes = False
            oListControl.bShowAttributeCodes = True


            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            ofrmCodeList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
            oListControl.ShowHeaderPanel(False)

            ofrmCodeList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCodeList.Text = "Concept ID"

            ofrmCodeList.ShowDialog(IIf(IsNothing(CType(ofrmCodeList, Control).Parent), Me, CType(ofrmCodeList, Control).Parent))

            If (IsNothing(oListControl) = False) Then
                ofrmCodeList.Controls.Remove(oListControl)

                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            End If


            If IsNothing(ofrmCodeList) = False Then
                ofrmCodeList.Dispose()
                ofrmCodeList = Nothing
            End If


        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
   



    Public Sub Set_All_DeleteTest(ByVal strTestID As String)

        With _Flex
            Dim rowid As Integer
            For i As Int16 = 0 To .Rows.Count
                If _Flex.GetData(i, COL_TESTID) = strTestID Then
                    rowid = _Flex.FindRow(strTestID, i, COL_TESTID, False, False, True)
                    .Row = rowid
                    Exit For
                End If
            Next

            Dim oRange As C1.Win.C1FlexGrid.CellRange
            oRange = .Rows(rowid).Node.GetCellRange
            Dim nTestStart As Int16 = oRange.TopRow
            Dim nTestEnd As Int16 = oRange.BottomRow
            .Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1)
            oRange = Nothing
            _LabModified = True
        End With
    End Sub



    Private Sub Set_Menu_ScanDocument(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim nStyleRow As Int16 = 0

            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow
                        Dim _tmpTestID As Int64 = 0

                        If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")
                            RaiseEvent gUC_ScanDocument(_tmpTestID)
                            If (_Flex.GetData(_CurrentRow, COL_DMSID) & "") <> "0" And (_Flex.GetData(_CurrentRow, COL_DMSID) & "") <> "" Then


                                'Date : 10/29/2013
                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                Dim oRange As C1.Win.C1FlexGrid.CellRange
                                Dim nStart As Int16 = 0
                                Dim nEnd As Int16 = 0
                                Dim _CurrentResultNo As Int16 = 0

                                oRange = _Flex.Rows(_CurrentRow).Node.GetCellRange
                                nStart = oRange.TopRow : nEnd = oRange.BottomRow
                                For l As Int16 = nEnd To nStart Step -1
                                    If _Flex.GetData(l, COL_RECORDTYPE) = enumRecordType.ResultHeader Then
                                        _CurrentResultNo = Val(_Flex.GetData(l, COL_RESULT_NUMBER))
                                        Exit For
                                    End If
                                Next
                                oRange = Nothing
                                If _CurrentResultNo = 0 Then
                                    AddDefaultDMSDocumentResult()
                                    _CurrentResultNo = 0
                                End If


                            End If


                        End If

                    End If
                End If
            End With
            GetLabTaskUsers() ''added for incident 43035
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Set_Menu_ViewDocument(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim nStyleRow As Int16 = 0

            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow
                        Dim _tmpTestID As Int64 = 0

                        If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")
                            ' RaiseEvent gUC_ViewDocument(_tmpTestID, Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_DMSID)))
                            RaiseEvent gUC_ViewDocument(_tmpTestID, Convert.ToInt64(CType(sender, ToolStripMenuItem).Tag))

                        End If
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub




    Private Sub Set_Menu_RemoveDocument(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim nStyleRow As Int16 = 0
            If MessageBox.Show("Are you sure you want to remove this Image Result?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                With _Flex
                    If _CurrentRow > 0 Then
                        If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                            _CurrentRow = _CurrentRow
                            Dim _tmpTestID As Int64 = 0
                            Dim _tmpDocIDs() As String
                            If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then
                                'Date : 10/29/2013
                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")
                                'RaiseEvent gUC_ViewDocument(_tmpTestID, Convert.ToInt64(CType(sender, MenuItem).Tag))
                                _tmpDocIDs = Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSIDCollection) & "").Split(",")
                                _Flex.SetData(_CurrentRow, COL_DMSIDCollection, "")
                                For l As Int16 = 0 To _tmpDocIDs.Length - 1
                                    If _tmpDocIDs(l).ToString() <> Convert.ToString(CType(sender, ToolStripMenuItem).Tag) Then

                                        If Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSIDCollection) & "") <> "" Then
                                            Dim sDocIds As String = Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSIDCollection) & "") & "," & _tmpDocIDs(l).ToString()
                                            _Flex.SetData(_CurrentRow, COL_DMSIDCollection, sDocIds)
                                        Else
                                            _Flex.SetData(_CurrentRow, COL_DMSIDCollection, _tmpDocIDs(l).ToString())
                                        End If


                                    End If
                                Next

                                _Flex.SetData(_CurrentRow, COL_DMSID, 0)
                                For i As Int16 = 0 To nDocList.Count - 1
                                    If nDocList(i).ToString() <> Convert.ToString(CType(sender, ToolStripMenuItem).Tag) Then
                                        _Flex.SetData(_CurrentRow, COL_DMSID, Convert.ToInt64(nDocList(i)))
                                        Exit For
                                    End If
                                Next
                                'If Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSID)).Trim = "0" Then
                                '    _Flex.Rows(_CurrentRow).Node.Image = ImgTest.Image
                                'End If

                                DelUp_LabDMS_Document(_CurrentOrderID, _tmpTestID, Convert.ToInt64(CType(sender, ToolStripMenuItem).Tag))

                                GetAvailableDMSandURLAttachments()

                            End If
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub GetAvailableDMSandURLAttachments()

        If IsNothing(dtURLDoc) = False Then
            Dim dvURLDoc As DataView = New DataView(dtURLDoc)
            Dim dtdvURLDoc As DataTable = Nothing

            If Not IsNothing(dtURLDoc) Then
                'dtURLDoc = Lab_GetURLDocument(_CurrentOrderID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)))
                dvURLDoc.RowFilter = "labotrda_TestID =" & Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID))
                dtdvURLDoc = dvURLDoc.ToTable
                If dtdvURLDoc.Rows.Count = 0 AndAlso (Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSID)).Trim = "0" OrElse Convert.ToString(_Flex.GetData(_CurrentRow, COL_DMSID)).Trim = "") Then
                    _Flex.Rows(_CurrentRow).Node.Image = ImgTest.Image
                End If
                dtdvURLDoc.Dispose()
                dtdvURLDoc = Nothing
            End If



            dvURLDoc.Dispose()
            dvURLDoc = Nothing

        End If

    End Sub

    Private Sub Set_Menu_URLDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not IsNothing(frmLabURL) Then
                frmLabURL.Close()
                frmLabURL.Dispose()
                frmLabURL = Nothing
            End If
            frmLabURL = New frmLabURLdocument
            frmLabURL.ShowDialog(IIf(IsNothing(frmLabURL.Parent), Me, frmLabURL.Parent))
            frmLabURL.Dispose()
            frmLabURL = Nothing
            GetLabTaskUsers() ''added for incident 43035
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim ofrmList As frmViewListControl
    Dim oListPreferredLab As gloListControl.gloListControl
    Private _PreferredLab As String
    Private _PreferredLabID As Int64
    Dim _OrderLabType As String = ""

    Public Property PreferredLab() As String
        Get
            Return _PreferredLab
        End Get
        Set(ByVal value As String)
            _PreferredLab = value
        End Set
    End Property

    Public Property PreferredLabID() As Int64
        Get
            Return _PreferredLabID
        End Get
        Set(ByVal value As Int64)
            _PreferredLabID = value
        End Set
    End Property

    Private Sub Set_Menu_PrefferedLab(ByVal sender As Object, ByVal e As System.EventArgs)
      

        Try

            ofrmList = New frmViewListControl
            ofrmList.Text = "Performing Labs"
            oListPreferredLab = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.PreferredLab, False, ofrmList.Width, "gloLabUC_Transaction")
            oListPreferredLab.ControlHeader = "Performing Labs"


            AddHandler oListPreferredLab.ItemSelectedClick, AddressOf oListPreferredLab_ItemSelectedClick
            AddHandler oListPreferredLab.ItemClosedClick, AddressOf oListPreferredLab_ItemClosedClick
            AddHandler oListPreferredLab.AddFormHandlerClick, AddressOf oListControl_AddFormHandlerClick

            ofrmList.Controls.Add(oListPreferredLab)
            oListPreferredLab.Dock = DockStyle.Fill
            oListPreferredLab.BringToFront()
            oListPreferredLab.OpenControl()
            oListPreferredLab.ShowHeaderPanel(False)
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If Not IsNothing(oListPreferredLab.SelectedItems) Then
                If oListPreferredLab.SelectedItems.Count > 0 Then
                    Dim objfrmViewNormalLab As Object
                    objfrmViewNormalLab = Me.ParentForm
                    LoadTests(objfrmViewNormalLab, oListPreferredLab.SelectedItems(0).ID)
                    _PreferredLabID = oListPreferredLab.SelectedItems(0).ID
                End If
            End If

            If IsNothing(ofrmList) = False Then

                RemoveHandler oListPreferredLab.ItemSelectedClick, AddressOf oListPreferredLab_ItemSelectedClick
                RemoveHandler oListPreferredLab.ItemClosedClick, AddressOf oListPreferredLab_ItemClosedClick
                RemoveHandler oListPreferredLab.AddFormHandlerClick, AddressOf oListControl_AddFormHandlerClick
                ofrmList.Controls.Remove(oListPreferredLab)
                oListPreferredLab.Dispose()
                oListPreferredLab = Nothing
                ofrmList.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListPreferredLab_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If oListPreferredLab.SelectedItems.Count > 0 Then
                _Flex.SetData(_Flex.RowSel, COL_PREFERREDLABID, oListPreferredLab.SelectedItems(0).ID)
                _Flex.SetData(_Flex.RowSel, COL_PREFERREDLAB, oListPreferredLab.SelectedItems(0).Description)
            End If
            ofrmList.Close()
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListPreferredLab_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub oListControl_AddFormHandlerClick(sender As Object, e As EventArgs)
        RaiseEvent gUC_AddFormHandlerClick()
    End Sub

    Private Sub LoadTests(objViewNormalLab As Object, Optional PreferredID As Int64 = 0)

        '28-Jul-16 Aniket: Resolving Bug #98908: gloEMR:Lab Order:Application unable to display all lab test .
        If PreferredID = 0 Then
            PreferredLabID = 0
        End If

        objViewNormalLab = Me.ParentForm

        If (objViewNormalLab.Name = "frmViewNormalLab") Then
            objViewNormalLab.IsPreferredLabCleared = True

            If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillTests_NEW()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillRadiologyImagingTests()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillOthers()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
                objViewNormalLab.FillGroups_NEW(PreferredID)
                'objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillRefTests()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals, PreferredID)

            End If
            'End If
        End If

    End Sub

    Private Sub LabAddURlDocument(ByVal URlDisplayName As String, ByVal URLName As String, ByVal Rowno As Integer) Handles frmLabURL.AddURLEvent
        Try
            Dim newURLDoc As DataRow = Nothing
            If IsNothing(dtURLDoc) = False Then
                If (Rowno = -1) Then
                    newURLDoc = dtURLDoc.NewRow()
                Else
                    dtURLDoc.AcceptChanges()
                    newURLDoc = dtURLDoc.Rows(Rowno)
                End If

                If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then
                    newURLDoc("labotrda_TestID") = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")
                End If

                newURLDoc("URLDisplayName") = URlDisplayName
                newURLDoc("URLName") = URLName.ToString().Replace(" ", "%20")

                Dim drURLDoc As DataRow() = dtURLDoc.Select("labotrda_TestID='" + _Flex.GetData(_CurrentRow, COL_TESTID) + "' and URLDisplayName='" + URlDisplayName + "'")
                If Rowno = -1 Then ''while adding new  records
                    If ((drURLDoc.Length = 0)) Then

                        dtURLDoc.Rows.Add(newURLDoc)
                        _LabModified = True
                    Else
                        MessageBox.Show("Display name already exist.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        Exit Sub
                    End If
                    drURLDoc = Nothing
                End If

                If Rowno >= 0 Then ''while modifying existing records
                    If ((drURLDoc.Length = 1)) Then


                        dtURLDoc.AcceptChanges()
                        _LabModified = True
                    Else
                        MessageBox.Show("Display name already exist.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        dtURLDoc.RejectChanges()
                        Exit Sub
                    End If
                    drURLDoc = Nothing
                    newURLDoc = Nothing
                End If

                If dtURLDoc.Rows.Count > 0 Then
                    _Flex.Rows(_CurrentRow).Node.Image = ImgAttachment.Image
                    ''Added by Mayuri:20140320-To get lab user tasks 
                    ''GetLabTaskUsers()  commented for incident 43035
                End If

            End If
            frmLabURL.Close()

            Dim oRange As C1.Win.C1FlexGrid.CellRange
            Dim nStart As Int16 = 0
            Dim nEnd As Int16 = 0
            Dim _CurrentResultNo As Int32 = 0
            oRange = _Flex.Rows(_CurrentRow).Node.GetCellRange
            nStart = oRange.TopRow : nEnd = oRange.BottomRow
            For l As Int16 = nEnd To nStart Step -1
                If _Flex.GetData(l, COL_RECORDTYPE) = enumRecordType.ResultHeader Then
                    _CurrentResultNo = Val(_Flex.GetData(l, COL_RESULT_NUMBER))
                    Exit For
                End If
            Next
            oRange = Nothing
            If _CurrentResultNo = 0 Then
                _Flex.Rows(_CurrentRow).Node.Image = ImgAttachment.Image
                AddDefaultDMSDocumentResult()
                _CurrentResultNo = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Menu_ViewURLDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim strurl As String()
            Dim mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            If (Not IsNothing(mnu.Tag)) Then
                strurl = mnu.Tag.ToString().Split("||")
                If (strurl.Length > 0) Then
                    gloGlobal.gloLabGenral.RunInSystemDefaultBrowser(strurl(0))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Menu_EditURLDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strsplitURL As String() = Nothing
        Try
            Dim mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            If (Not IsNothing(mnu.Tag)) Then
                strsplitURL = mnu.Tag.ToString().Split("||")
            End If

            If Not IsNothing(frmLabURL) Then
                frmLabURL.Close()
                frmLabURL.Dispose()
                frmLabURL = Nothing
            End If
            frmLabURL = New frmLabURLdocument
            frmLabURL._blnOpenforModify = True
            frmLabURL.UrlDisplayName = mnu.Text
            frmLabURL.UrlName = strsplitURL(0).Replace("%20", " ")
            frmLabURL.RowNo = Convert.ToInt32(strsplitURL(4))
            frmLabURL.ShowDialog(IIf(IsNothing(frmLabURL.Parent), Me, frmLabURL.Parent))
            frmLabURL.Dispose()
            frmLabURL = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Menu_RemoveURLDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If MessageBox.Show("Are you sure you want to remove the URL Document?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim splstr As String() = Nothing
                Dim mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
                If (Not IsNothing(mnu.Tag)) Then
                    splstr = mnu.Tag.ToString().Split("||")
                End If
                If (splstr.Length > 1) Then
                    If (Not IsNothing(mnu.Text)) Then
                        Dim dr As DataRow() = dtURLDoc.Select("URLDisplayName='" + mnu.Text + "'  and labotrda_TestID='" + splstr(2) + "' ")
                        If (dr.Length > 0) Then
                            dtURLDoc.Rows.Remove(dr(0))
                            _LabModified = True
                        End If
                    End If
                End If

                GetAvailableDMSandURLAttachments()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function DelUp_LabDMS_Document(ByVal OrderID As Int64, ByVal TestID As Int64, ByVal DocumentID As Int64) As Boolean
        Dim con As SqlConnection = Nothing
        Dim SqlCmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing

        Try

            con = New SqlConnection(_ConnectionString)
            SqlCmd = New SqlCommand("Lab_DEL_Test_Order_Attach", con)
            SqlCmd.CommandType = CommandType.StoredProcedure

            objParam = SqlCmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OrderID

            objParam = SqlCmd.Parameters.Add("@TestID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TestID

            objParam = SqlCmd.Parameters.Add("@DocumentID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = DocumentID

            con.Open()
            SqlCmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
            con = Nothing

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Return False
        Finally

            If Not IsNothing(SqlCmd) Then
                SqlCmd.Parameters.Clear()
                SqlCmd.Dispose()
                SqlCmd = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If


            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
    Private Function Update_CQMCategories(ByVal TestID As Long, ByVal sValueSetOID As String, ByVal sValueSetName As String, ByVal smode As Int16, ByVal sConceptID As String, ByVal sDescriptionID As String) As Boolean
        Dim con As SqlConnection = Nothing
        Dim SqlCmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing

        Try



            con = New SqlConnection(_ConnectionString)
            SqlCmd = New SqlCommand("gsp_UpdateCQM_NotDoneCategories", con)
            SqlCmd.CommandType = CommandType.StoredProcedure

            objParam = SqlCmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _CurrentOrderID


            objParam = SqlCmd.Parameters.Add("@Patientid", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nPatientID

            objParam = SqlCmd.Parameters.Add("@TestID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TestID

            objParam = SqlCmd.Parameters.Add("@sValueSetOID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sValueSetOID


            objParam = SqlCmd.Parameters.Add("@sValueSetName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sValueSetName


            objParam = SqlCmd.Parameters.Add("@Mode", SqlDbType.TinyInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = smode


            objParam = SqlCmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sConceptID


            objParam = SqlCmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sDescriptionID


            con.Open()
            SqlCmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
            con = Nothing

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Return False
        Finally

            If Not IsNothing(SqlCmd) Then
                SqlCmd.Parameters.Clear()
                SqlCmd.Dispose()
                SqlCmd = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If


            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
    'Private Function Update_ConceptID(ByVal TestID As Long, ByVal sConceptID As String, ByVal sDescriptionID As String) As Boolean
    '    Dim con As SqlConnection = Nothing
    '    Dim SqlCmd As SqlCommand = Nothing
    '    Dim objParam As SqlParameter = Nothing

    '    Try
    '        SqlCmd.Parameters.AddWithValue("@Mode", 2)
    '        con = New SqlConnection(_ConnectionString)
    '        SqlCmd = New SqlCommand("gsp_UpdateCQM_NotDoneCategories", con)
    '        SqlCmd.CommandType = CommandType.StoredProcedure

    '        objParam = SqlCmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = _CurrentOrderID


    '        objParam = SqlCmd.Parameters.Add("@Patientid", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = nPatientID

    '        objParam = SqlCmd.Parameters.Add("@TestID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = TestID

    '        objParam = SqlCmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = sConceptID

    '        objParam = SqlCmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = sDescriptionID



    '        con.Open()
    '        SqlCmd.ExecuteNonQuery()
    '        con.Close()
    '        con.Dispose()
    '        con = Nothing

    '        Return True
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
    '        Return False
    '    Finally

    '        If Not IsNothing(SqlCmd) Then
    '            SqlCmd.Parameters.Clear()
    '            SqlCmd.Dispose()
    '            SqlCmd = Nothing
    '        End If

    '        If Not IsNothing(objParam) Then
    '            objParam = Nothing
    '        End If


    '        If Not IsNothing(con) Then
    '            If con.State = ConnectionState.Open Then
    '                con.Close()
    '            End If
    '            con.Dispose()
    '            con = Nothing
    '        End If
    '    End Try
    'End Function


    Private Sub AddDefaultDMSDocumentResult()
        Dim nTest As Int16 = 0
        Dim nStyleRow As Int16 = 0
        Dim nNodeRow As Int16 = 0
        Dim i As Int16 = 0

        Try
            _CurrentTestSelectedID = _Flex.GetData(_CurrentRow, COL_TESTID)
            _CurrentTestLineNo = _Flex.GetData(_CurrentRow, COL_TEST_LINENO)
            _CurrentTestResultSelectedCounterID = 1
            With _Flex
                If .Rows.Count > 0 Then
                    'If _CurrentTestResultIsModify = False Then
                    For nTest = 1 To .Rows.Count - 1
                        Dim _GrdTestID As Int64 = 0
                        Dim _sGrdTestName As String = ""
                        If Val(.GetData(nTest, COL_TESTID) & "") > 0 Then
                            _GrdTestID = Convert.ToInt64(.GetData(nTest, COL_TESTID))
                            _sGrdTestName = .GetData(nTest, COL_TESTNAME)
                        End If
                        'Mitesh
                        If _GrdTestID = _CurrentTestSelectedID AndAlso Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                            If IsNothing(.GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME)) OrElse .GetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME) & "" = "" Then
                                .SetData(nTest, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Date.Now)
                            End If
                            If IsNothing(.GetData(nTest, COL_REPORTED_DATETIME)) OrElse .GetData(nTest, COL_REPORTED_DATETIME) & "" = "" Then
                                .SetData(nTest, COL_REPORTED_DATETIME, Date.Now)
                            End If
                        End If

                        '----x---
                        If _GrdTestID = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.Test Then
                            nStyleRow = nTest ' Set Test Node

                            .Rows(nTest).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Result-" & _CurrentTestResultSelectedCounterID, _CurrentTestSelectedID & _CurrentTestResultSelectedCounterID, ImgResultHeader.Image)

                            nStyleRow = .Rows(nTest).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            nNodeRow = nStyleRow

                            '---------------------------------------------------------
                            .SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")
                            .SetData(nStyleRow, COL_TEST_RESULT_NAME, "Result-" & _CurrentTestResultSelectedCounterID)
                            .SetData(nStyleRow, COL_TEST_RESULT_CODE, "Code")
                            .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                            .SetData(nStyleRow, COL_TESTID, _CurrentTestSelectedID)
                            .SetData(nStyleRow, COL_TEST_LINENO, _CurrentTestLineNo)
                            .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                            .SetData(nStyleRow, COL_RESULT_LINENO, 0)
                            .SetData(nStyleRow, COL_RESULT_NAMEID, 0)
                            .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "Value")
                            .SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                            .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                            .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, "Unit")
                            .SetData(nStyleRow, COL_TEST_CPTLIST, "")
                            .SetData(nStyleRow, COL_TEST_CPTBTN, "")
                            .SetData(nStyleRow, COL_TEST_NOTE, "Note")
                            .SetData(nStyleRow, COL_TEST_SPECIMEN, "Specimen")
                            .SetData(nStyleRow, COL_TEST_COLLECTION, "Collection")
                            .SetData(nStyleRow, COL_TEST_STORAGE, "Storage")
                            ''Remove column Header by Abhijeet on 20101116
                            ''.SetData(nStyleRow, COL_TEST_LOINC, "LOINC Code")
                            .SetData(nStyleRow, COL_TEST_LOINC, "")
                            ''End of changes to Remove column Header by Abhijeet on 20101116
                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                            .SetData(nStyleRow, COL_RESULT_TYPE, "")
                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                            .SetData(nStyleRow, COL_RESULT_RANGE, "Range")
                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "Patient Specific Range")
                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "Value Type")
                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, "Flag")
                            .SetData(nStyleRow, COL_RESULT_TYPE, "Result Type")
                            .SetData(nStyleRow, COL_RESULT_COMMENT, "R. Comment")
                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                            'Sanjog
                            '.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Date.Now)
                            'Sanjog




                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, "R. UserID")
                            .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                            .SetData(nStyleRow, COL_ISFINISHED, 0)
                            ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                            'Added by madan-- on 20100409...
                            .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                            .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, "")
                            .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                            .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                            .SetData(nStyleRow, COL_ALTERNATE_RESULT_CODE, "")
                            .SetData(nStyleRow, COL_ALTERNATE_RESULT_NAME, "")

                            .SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, "")

                            'End Madan.
                            ''Added by Abhijeet on 20101026
                            .SetData(nStyleRow, COL_TEST_STATUS, "")
                            .SetData(nStyleRow, COL_SPECIMEN_SOURCE, "")
                            .SetData(nStyleRow, COL_SPECIMEN_CONDITION_DISP, "")
                            .SetData(nStyleRow, COL_LAB_TEST_TYPE, "")
                            ''End of change Added by Abhijeet on 20101026


                            'Labs Denormalization 20090321
                            .SetData(nStyleRow, COL_TESTNAME, .GetData(nTest, COL_TESTNAME))
                            '---

                            '---------------------------------------------------------

                            With .Rows(nStyleRow)
                                .ImageAndText = True
                                .Height = 22
                                .Style = _Flex.Styles("CS_Header")
                                .AllowEditing = False
                            End With

                            .SetCellStyle(nStyleRow, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_Record"))


                            Exit For
                        End If
                    Next
                    'Else
                    '    For nTest = 1 To .Rows.Count - 1
                    '        'Sanjog -Commented and added on 2011 March 9 to update the actual result which we are open for modification --Start
                    '        ''If .GetData(nTest, COL_TESTID) & "" = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.ResultHeader AndAlso .GetData(nTest, COL_RESULT_NUMBER) & "" = _CurrentTestResultSelectedCounterID Then
                    '        If .GetData(nTest, COL_TESTID) = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.ResultHeader AndAlso .GetData(nTest, COL_RESULT_NUMBER) & "" = _CurrentTestResultSelectedCounterID Then
                    '            'Sanjog -added on 2011 March 9 to update the actual result which we are open for modification  --End
                    '            nStyleRow = nTest ' Set Test Result Node
                    '            nNodeRow = nStyleRow

                    '            Dim oRange As New C1.Win.C1FlexGrid.CellRange
                    '            oRange = .Rows(nTest).Node.GetCellRange
                    '            Dim nTestStart As Int16 = oRange.TopRow
                    '            Dim nTestEnd As Int16 = oRange.BottomRow
                    '            Dim nTestCount As Int16 = nTestEnd - nTestStart
                    '            If nTestStart <> nTestEnd Then
                    '                oRange.r1 = nTestStart + 1
                    '                .Rows.RemoveRange(oRange.r1, nTestCount)
                    '            End If
                    '            oRange = Nothing

                    '            Exit For
                    '        End If
                    '    Next
                    'End If

                    ''Fill Results
                    'For i = 1 To c1Results.Rows.Count - 1
                    '' If c1Results.GetData(i, COL_TEST_RESULT_NAME) <> "" AndAlso c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE) <> "" Then

                    'If IsNothing(c1Results.GetData(i, COL_RESULT_COMMENT)) = False Then
                    '    If c1Results.GetData(i, COL_RESULT_COMMENT).ToString() <> "" Then
                    '        '' If Test Result have Comment
                    '        .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Document attached", _CurrentTestResultSelectedCounterID & i, ImgResult_Comment.Image)
                    '    Else
                    '        '' If Test Result don't have Comment
                    '        .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Document attached", _CurrentTestResultSelectedCounterID & i, ImgResult.Image)
                    '    End If
                    'Else
                    '' If Test Result don't have Comment
                    .Rows(nNodeRow).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, Convert.ToString(_Flex.GetData(_CurrentRow, COL_TESTNAME)), _CurrentTestResultSelectedCounterID & 1, ImgResult.Image)
                    'End If

                    nStyleRow = .Rows(nNodeRow).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index

                    Dim _DB As New gloEMRDatabase.DataBaseLayer
                    Dim _strSQL As String = ""
                    Dim oDataTable As DataTable = Nothing

                    _strSQL = "SELECT Lab_Test_ResultDtl.labtrd_ResultID, Lab_Test_ResultDtl.labtrd_ResultName " _
                    & " FROM Lab_Test_Mst INNER JOIN Lab_Test_ResultDtl ON Lab_Test_Mst.labtm_ID = Lab_Test_ResultDtl.labtrd_TestID " _
                    & " WHERE Lab_Test_Mst.labtm_ID = " & _CurrentTestSelectedID & " AND Lab_Test_ResultDtl.labtrd_ResultName IS NOT NULL AND Lab_Test_ResultDtl.labtrd_ResultID IS NOT NULL ORDER BY Lab_Test_ResultDtl.labtrd_ResultID"



                    oDataTable = _DB.GetDataTable_Query(_strSQL)


                    '---------------------------------------------------------
                    .SetData(nStyleRow, COL_TEST_RESULT_SELECT, "")

                    Dim IsResult As Boolean = False
                    If Not oDataTable Is Nothing Then
                        If oDataTable.Rows.Count > 0 Then

                            .SetData(nStyleRow, COL_TEST_RESULT_NAME, oDataTable.Rows(0).Item("labtrd_ResultName") & "")
                            IsResult = True
                        End If
                        oDataTable.Dispose()
                        oDataTable = Nothing
                    End If
                    If IsResult = False Then
                        .SetData(nStyleRow, COL_TEST_RESULT_NAME, Convert.ToString(_Flex.GetData(_CurrentRow, COL_TESTNAME)))
                    End If

                    .SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                    .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                    .SetData(nStyleRow, COL_TESTID, _CurrentTestSelectedID)
                    .SetData(nStyleRow, COL_TEST_LINENO, _CurrentTestLineNo)
                    .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                    .SetData(nStyleRow, COL_RESULT_LINENO, 1)
                    ' .SetData(nStyleRow, COL_RESULT_NAMEID, c1Results.GetData(i, COL_RESULT_NAMEID))

                    'If gloGlobal.gloLabGenral.IsResultisHyperLink(c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE)) Then
                    '    .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Item(nStyleRow) = New Hyperlink(c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString())
                    'Else
                    .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "Document attached")
                    'End If
                    'start of added by manoj on 20121127 for making result value as hyperlink
                    .SetData(nStyleRow, COL_TEST_DIGNOSISLIST, "")
                    .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, "")
                    ' .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, c1Results.GetData(i, COL_TEST_CPT_RESULT_UNIT))
                    .SetData(nStyleRow, COL_TEST_CPTLIST, "")
                    .SetData(nStyleRow, COL_TEST_CPTBTN, "")
                    .SetData(nStyleRow, COL_TEST_NOTE, "")
                    .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                    .SetData(nStyleRow, COL_TEST_COLLECTION, "")
                    .SetData(nStyleRow, COL_TEST_STORAGE, "")
                    '.SetData(nStyleRow, COL_TEST_LOINC, c1Results.GetData(i, COL_TEST_LOINC))
                    .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                    .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                    .SetData(nStyleRow, COL_RESULT_TYPE, "")
                    .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                    .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                    .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                    '.SetData(nStyleRow, COL_RESULT_RANGE, c1Results.GetData(i, COL_RESULT_RANGE))
                    '.SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "")  'c1Results.GetData(i, COL_RESULT_VALUE_TYPE)
                    '.SetData(nStyleRow, COL_ABNORMAL_FLAG, c1Results.GetData(i, COL_ABNORMAL_FLAG))
                    .SetData(nStyleRow, COL_RESULT_TYPE, "Final results")
                    '.SetData(nStyleRow, COL_RESULT_COMMENT, c1Results.GetData(i, COL_RESULT_COMMENT))
                    .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                    ''Sanjog
                    '.SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, c1Results.GetData(i, COL_TEST_RESULT_OR_TRANSFER_DATETIME))
                    ''Sanjog
                    ' .SetData(nStyleRow, COL_TEST_RESULT_USERID, c1Results.GetData(i, COL_TEST_RESULT_USERID)) '// Remaining
                    .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                    .SetData(nStyleRow, COL_ISFINISHED, 0)
                    ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                    'Added by madan-- on 20100409...
                    .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                    '.SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, c1Results.GetData(i, COL_RESULT_TRANSFER_DATETIME))
                    .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                    .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                    .SetData(nStyleRow, COL_ALTERNATE_RESULT_CODE, "")
                    .SetData(nStyleRow, COL_ALTERNATE_RESULT_NAME, "")
                    'End MAdan

                    ''Added by Abhijeet on 20101026
                    .SetData(nStyleRow, COL_LAB_FACILITY_NAME, "")
                    .SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, "")
                    .SetData(nStyleRow, COL_LAB_FACILITY_CITY, "")
                    .SetData(nStyleRow, COL_LAB_FACILITY_STATE, "")
                    .SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, "")

                    '.SetData(nStyleRow, COL_LAB_SPEC_RANGE, c1Results.GetData(i, COL_LAB_SPEC_RANGE))

                    ' .SetData(nStyleRow, COL_SPECIMENCOLLECTIONDATE, c1Results.GetData(i, COL_SPECIMENCOLLECTIONDATE))
                    'Sanjog
                    '.SetData(nStyleRow, COL_TESTNAME, c1Results.GetData(i, COL_TESTNAME))
                    '------
                    '---------------------------------------------------------


                    With .Rows(nStyleRow)
                        .ImageAndText = True
                        .Height = 22
                        ''GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                        ''Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                        ''If _AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG))) Then
                        'If _AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG)) = "N" Or String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG))) Then
                        '    'Normal and nothing
                        '    .Style = _Flex.Styles("CS_Record")
                        'Else
                        '    ' '' Not Normal
                        '    .Style = _Flex.Styles("CS_NotNormal")

                        'End If


                    End With


                    .SetCellStyle(nStyleRow, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                    .Rows(nStyleRow).AllowEditing = False
                    ''  End If

                    'Infobutton
                    Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                    _Flex.SetCellImage(nStyleRow, COL_LAB_INFOBUTTON, imgFlag)


                    ' Next ' For i = 1 To oCategorisedDocuments.Count

                    _Flex.Visible = True
                    pnlDiagosisCPT.Visible = False
                    pnlResults.Visible = False

                    _DB.Dispose()
                    _DB = Nothing

                End If
            End With



        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''Added by madan for dicom

    Private Sub Set_Menu_ViewDicomDocument(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim nStyleRow As Int16 = 0

            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow
                        Dim _tmpDicomID As Int64 = 0

                        If Val(_Flex.GetData(_CurrentRow, COL_DICOMID)) > 0 Then
                            _tmpDicomID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_DICOMID) & "")
                            RaiseEvent gUC_ViewDicomDocument(nPatientID, _tmpDicomID)
                        End If
                        _tmpDicomID = 0
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''End Madan.

#Region "Commented BY Pramod for DMSV2"
    'Private Sub Set_Menu_ViewDocument(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Try
    '        Dim nStyleRow As Int16 = 0

    '        With _Flex
    '            If _CurrentRow > 0 Then
    '                If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
    '                    _CurrentRow = _CurrentRow
    '                    Dim _tmpTestID As Int64 = 0

    '                    If Val(_Flex.GetData(_CurrentRow, COL_TESTID)) > 0 Then
    '                        _tmpTestID = Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_TESTID) & "")
    '                        RaiseEvent gUC_ViewDocument(_tmpTestID, Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_DMSID)))
    '                    End If
    '                End If
    '            End If
    '        End With

    '    Catch ex As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
#End Region

    Private Sub Set_Menu_ModifyResult(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim nStyleRow As Int16 = 0

            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.ResultHeader) Then
                        ' '' Chnages Made By Mahesh 
                        _CurrentRow = _CurrentRow

                        DesignResultGrid()

                        '//--//
                        With c1Results
                            Try
                                _CurrentTestResultIsModify = True
                                '//--TEST NODE--//
                                Dim _tmpTestID As Int64
                                Dim _tmpTestName As String = ""

                                _tmpTestID = _Flex.GetData(_CurrentRow, COL_TESTID)
                                'Labs Denormalization
                                _tmpTestName = _Flex.GetData(_CurrentRow, COL_TESTNAME)
                                '-----

                                _CurrentTestSelectedID = _Flex.GetData(_CurrentRow, COL_TESTID)
                                _CurrentTestLineNo = _Flex.GetData(_CurrentRow, COL_TEST_LINENO)
                                _CurrentTestResultSelectedCounterID = _Flex.GetData(_CurrentRow, COL_RESULT_NUMBER)

                                '''''''''''''''''''''
                                Dim _DB As New gloEMRDatabase.DataBaseLayer
                                Dim _strSQL As String = ""
                                Dim oDataTable As DataTable = Nothing

                                _strSQL = "SELECT Lab_Test_ResultDtl.labtrd_ResultID, Lab_Test_ResultDtl.labtrd_ResultName, " _
                                & " Lab_Test_ResultDtl.labtrd_ValueType, Lab_Test_ResultDtl.labtrd_Unit, Lab_Test_ResultDtl.labtrd_DefaultValue, " _
                                & " Lab_Test_ResultDtl.labtrd_RefRange, Lab_Test_ResultDtl.labtrd_LOINCId , Lab_Test_Mst.labtm_Name " _
                                & " FROM Lab_Test_Mst INNER JOIN Lab_Test_ResultDtl ON Lab_Test_Mst.labtm_ID = Lab_Test_ResultDtl.labtrd_TestID " _
                                & " WHERE Lab_Test_Mst.labtm_ID = " & _tmpTestID & " AND Lab_Test_ResultDtl.labtrd_ResultName IS NOT NULL AND Lab_Test_ResultDtl.labtrd_ResultID IS NOT NULL ORDER BY Lab_Test_ResultDtl.labtrd_ResultID"



                                oDataTable = _DB.GetDataTable_Query(_strSQL)
                                ''''''''''''''''''''
                                'Date : 10/29/2013
                                If _CurrentOrderID > 0 Then
                                    RaiseEvent LockOrder(_CurrentOrderID)
                                End If
                                _LabModified = True

                                Dim oRange As C1.Win.C1FlexGrid.CellRange
                                Dim nStart As Int16 = 0
                                Dim nEnd As Int16 = 0

                                oRange = _Flex.Rows(_CurrentRow).Node.GetCellRange
                                nStart = oRange.TopRow + 1 : nEnd = oRange.BottomRow
                                For l As Int16 = nStart To nEnd
                                    ' If _Flex.GetData(l, COL_RECORDTYPE) = enumRecordType.Result Then
                                    .Rows.Add()
                                    nStyleRow = nStyleRow + 1

                                    .SetData(nStyleRow, COL_TEST_RESULT_NAME, _Flex.GetData(l, COL_TEST_RESULT_NAME) & "")
                                    .SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                                    .SetData(nStyleRow, COL_ORDERID, _Flex.GetData(l, COL_ORDERID))
                                    .SetData(nStyleRow, COL_TESTID, _tmpTestID)
                                    .SetData(nStyleRow, COL_RESULT_NUMBER, _Flex.GetData(l, COL_RESULT_NUMBER))
                                    .SetData(nStyleRow, COL_TEST_LINENO, _Flex.GetData(l, COL_TEST_LINENO))
                                    .SetData(nStyleRow, COL_RESULT_LINENO, _Flex.GetData(l, COL_RESULT_LINENO)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_NAMEID, _Flex.GetData(l, COL_RESULT_NAMEID)) '//Result//
                                    '.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.GetData(l, COL_TEST_DIGNOSIS_RESULT_VALUE)) 'commented  by manoj on 20121127
                                    'start code added by manoj on 20121127 for making result value as hyperlink
                                    If TypeOf (_Flex.GetData(l, COL_TEST_DIGNOSIS_RESULT_VALUE)) Is Hyperlink Then
                                        .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.GetData(l, COL_TEST_DIGNOSIS_RESULT_VALUE).ToString())
                                    Else
                                        .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.GetData(l, COL_TEST_DIGNOSIS_RESULT_VALUE))
                                    End If
                                    'end of code added by manoj on 20121127 for making result value as hyperlink
                                    .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, _Flex.GetData(l, COL_TEST_CPT_RESULT_UNIT))
                                    .SetData(nStyleRow, COL_TEST_CPTBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_NOTE, "")
                                    .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                                    .SetData(nStyleRow, COL_TEST_STORAGE, "")
                                    .SetData(nStyleRow, COL_TEST_LOINC, _Flex.GetData(l, COL_TEST_LOINC))
                                    .SetData(nStyleRow, COL_LAB_TEST_CODE, _Flex.GetData(l, COL_LAB_TEST_CODE))
                                    .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                    .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                    .SetData(nStyleRow, COL_RESULT_TYPE, "")
                                    .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_COMMENTSBTN, Nothing)
                                    .SetData(nStyleRow, COL_RESULT_RANGE, _Flex.GetData(l, COL_RESULT_RANGE)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, _Flex.GetData(l, COL_RESULT_VALUE_TYPE)) '//Result//
                                    .SetData(nStyleRow, COL_ABNORMAL_FLAG, _Flex.GetData(l, COL_ABNORMAL_FLAG)) '//Result//

                                    ''Added by Abhijeet on 20101122
                                    .SetData(nStyleRow, COL_LAB_ORIGINAL_ABNORMAL_FLAG, _Flex.GetData(l, COL_LAB_ORIGINAL_ABNORMAL_FLAG))
                                    ''End of changes by Abhijeet on 20101122

                                    .SetData(nStyleRow, COL_RESULT_TYPE, _Flex.GetData(l, COL_RESULT_TYPE)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_COMMENT, _Flex.GetData(l, COL_RESULT_COMMENT)) '//Result//
                                    .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, _Flex.GetData(l, COL_TEST_RESULT_DATETIME))
                                    'Sanjog
                                    .SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, _Flex.GetData(l, COL_TEST_RESULT_OR_TRANSFER_DATETIME))
                                    'Sanjog
                                    .SetData(nStyleRow, COL_TEST_RESULT_USERID, _Flex.GetData(l, COL_TEST_RESULT_USERID))
                                    .SetData(nStyleRow, COL_RECORDTYPE, _Flex.GetData(l, COL_RECORDTYPE))
                                    .SetData(nStyleRow, COL_ISFINISHED, 0)
                                    ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                                    'Added by madan-- on 20100409...
                                    .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                                    .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, _Flex.GetData(l, COL_RESULT_TRANSFER_DATETIME))
                                    .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                                    .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                                    'end

                                    ''Added by Abhijeet on 20101026
                                    .SetData(nStyleRow, COL_LAB_FACILITY_NAME, "")
                                    .SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, "")
                                    .SetData(nStyleRow, COL_LAB_FACILITY_CITY, "")
                                    .SetData(nStyleRow, COL_LAB_FACILITY_STATE, "")
                                    .SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, "")
                                    ''End of changes By Abhijeet on 20101026
                                    'Added by madan on 20101122
                                    .SetData(nStyleRow, COL_LAB_SPEC_RANGE, _Flex.GetData(1, COL_LAB_SPEC_RANGE))
                                    'End madan changes.
                                    'Sanjog
                                    ' Dim str As String = .SetData(nStyleRow, COL_SPECIMENCOLLECTIONDATE, _Flex.GetData(1, COL_SPECIMENCOLLECTIONDATE))
                                    .SetData(nStyleRow, COL_SPECIMENCOLLECTIONDATE, _Flex.GetData(1, COL_SPECIMENCOLLECTIONDATE))
                                    'Sanjog

                                    'Labs Denormalization
                                    .SetData(nStyleRow, COL_TESTNAME, _tmpTestName)
                                    '----
                                    'Madan-- added on 20100504
                                    _LabModified = True
                                    'End madan.

                                    Dim MyRow As DataRow() = oDataTable.Select("labtrd_ResultName='" + _Flex.GetData(l, COL_TEST_RESULT_NAME).ToString() + "'")
                                    If MyRow.Length > 0 Then
                                        oDataTable.Rows.Remove(MyRow(0))
                                    End If

                                    With .Rows(nStyleRow)
                                        .ImageAndText = False
                                        .Height = 22
                                        .Style = _Flex.Styles("CS_Record")
                                    End With
                                    'End If
                                Next

                                ''''''''
                                If Not oDataTable Is Nothing Then
                                    If oDataTable.Rows.Count > 0 Then
                                        For i As Int16 = 0 To oDataTable.Rows.Count - 1
                                            .Rows.Add()
                                            nStyleRow = nStyleRow + 1

                                            .SetData(nStyleRow, COL_TEST_RESULT_NAME, oDataTable.Rows(i).Item("labtrd_ResultName") & "")
                                            .SetData(nStyleRow, COL_TEST_RESULT_CODE, "")
                                            .SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            .SetData(nStyleRow, COL_TESTID, _tmpTestID)
                                            .SetData(nStyleRow, COL_TESTNAME, oDataTable.Rows(i).Item("labtm_Name") & "")
                                            .SetData(nStyleRow, COL_RESULT_NUMBER, _CurrentTestResultSelectedCounterID)
                                            .SetData(nStyleRow, COL_TEST_LINENO, _Flex.GetData(_CurrentRow, COL_TEST_LINENO))
                                            .SetData(nStyleRow, COL_RESULT_LINENO, i + 1) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_NAMEID, oDataTable.Rows(i).Item("labtrd_ResultID")) '//Result//
                                            .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, "")
                                            .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, Nothing)
                                            .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, oDataTable.Rows(i).Item("labtrd_Unit"))
                                            .SetData(nStyleRow, COL_TEST_CPTBTN, Nothing)
                                            .SetData(nStyleRow, COL_TEST_NOTE, "")
                                            .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                                            .SetData(nStyleRow, COL_TEST_STORAGE, "")
                                            .SetData(nStyleRow, COL_TEST_LOINC, oDataTable.Rows(i).Item("labtrd_LOINCId"))
                                            .SetData(nStyleRow, COL_LAB_SPEC_RANGE, "")
                                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, "")
                                            .SetData(nStyleRow, COL_RESULT_TYPE, "")
                                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, oDataTable.Rows(i).Item("labtrd_ValueType")) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_RANGE, oDataTable.Rows(i).Item("labtrd_RefRange")) '//Result//
                                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_TYPE, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_COMMENT, "") '//Result//
                                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                                            'Mitesh
                                            '  .SetData(nStyleRow, COL_REPORTED_DATETIME, Date.Now)
                                            'Mitesh

                                            'Sanjog
                                            .SetData(nStyleRow, COL_TEST_RESULT_OR_TRANSFER_DATETIME, Nothing)
                                            'Sanjog
                                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, _Flex.GetData(_CurrentRow, COL_TEST_RESULT_USERID))
                                            .SetData(nStyleRow, COL_RECORDTYPE, CType(enumRecordType.Result, Integer))
                                            .SetData(nStyleRow, COL_ISFINISHED, 0)
                                            ' Added below four fileds as per qwest certification and same as updateing while save and close in view order form.
                                            'Added by madan-- on 20100409...
                                            .SetData(nStyleRow, COL_SPECIMEN_RECEIVED_DATETIME, Date.Now)
                                            .SetData(nStyleRow, COL_RESULT_TRANSFER_DATETIME, "")
                                            .SetData(nStyleRow, COL_ALTERNATE_TEST_CODE, "")
                                            .SetData(nStyleRow, COL_ALTERNATE_TEST_NAME, "")
                                            'End madan

                                            ''Added by Abhijeet on 20101026
                                            .SetData(nStyleRow, COL_LAB_FACILITY_NAME, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_STREET_ADDRESS, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_CITY, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_STATE, "")
                                            .SetData(nStyleRow, COL_LAB_FACILITY_ZIP_CODE, "")
                                            ''End of changes Added by Abhijeet on 20101026
                                            'Added by madan-- on 20100504
                                            _LabModified = True
                                            'end madan
                                            With .Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                .Style = _Flex.Styles("CS_Record")

                                            End With

                                        Next
                                    End If
                                    oDataTable.Dispose()
                                    oDataTable = Nothing
                                End If
                                ''''''''
                                _DB.Dispose()
                                _DB = Nothing

                                oRange = Nothing

                                .Rows.Count = .Rows.Count + 21

                            Catch objError As Exception
                                'MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
                            Finally

                            End Try

                        End With
                        '//--//

                        _Flex.Visible = False
                        pnlDiagosisCPT.Visible = False
                        pnlResults.Visible = True
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Menu_DeleteResult(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim nStyleRow As Int16 = 0

            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.ResultHeader) Then
                        _CurrentRow = _CurrentRow
                        If MessageBox.Show("Are you sure you want to delete this result?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            'Date : 10/29/2013
                            If _CurrentOrderID > 0 Then
                                RaiseEvent LockOrder(_CurrentOrderID)
                            End If
                            _LabModified = True

                            Dim oRange As C1.Win.C1FlexGrid.CellRange
                            oRange = .Rows(_CurrentRow).Node.GetCellRange
                            Dim nTestStart As Int16 = oRange.TopRow
                            Dim nTestEnd As Int16 = oRange.BottomRow
                            .Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1)
                            oRange = Nothing
                            'Madan added on 20100504
                            _LabModified = True
                            'end madan
                        End If
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1Results_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Results.AfterEdit
        If e.Row > 0 Then
            If c1Results.GetData(e.Row, COL_RESULT_TYPE) = "" Then
                c1Results.SetData(e.Row, COL_RESULT_TYPE, "Final results") '' gloEMRActors.LabActor.enumTestResultReadType.Final.ToString)
            End If
        End If

        If e.Row > 0 Then
            If c1Results.GetData(e.Row, COL_TEST_RESULT_NAME) & "" = "" Then
                c1Results.SetData(e.Row, e.Col, Nothing)
            ElseIf c1Results.GetData(e.Row - 1, COL_TEST_RESULT_NAME) & "" = "" And c1Results.GetData(e.Row - 1, COL_TEST_DIGNOSIS_RESULT_VALUE) & "" = "" Then
                c1Results.SetData(e.Row, e.Col, Nothing)
            End If
        End If
    End Sub

    Private Sub btnInstruction_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnOk.Click
        Try
            With _Flex
                If .Col = COL_TEST_INSTRUCTIONBTN Then
                    .SetData(.Row, COL_LAB_SPEC_RANGE, txtInstruction.Text.Trim)
                ElseIf .Col = COL_TEST_PRECAUTIONBTN Then
                    .SetData(.Row, COL_ABNORMAL_FLAG, txtInstruction.Text.Trim)
                ElseIf .Col = COL_TEST_COMMENTSBTN Then
                    .SetData(.Row, COL_RESULT_TYPE, txtInstruction.Text.Trim)
                End If

                If Val(_Flex.GetData(.Row, COL_RECORDTYPE)) = enumRecordType.Result Then
                    .SetData(.Row, COL_RESULT_COMMENT, txtInstruction.Text.Trim)
                End If


                If .GetData(.Row, COL_RECORDTYPE) = enumRecordType.Result And .Col = COL_TEST_DIGNOSIS_RESULT_VALUE Then
                    '        .SetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE, txtInstruction.Text) commented by manoj on 20121130 
                    'start of code added by manoj on 20121130 for making result value as hyper link
                    If gloGlobal.gloLabGenral.IsResultisHyperLink(txtInstruction.Text.Trim()) Then
                        .Item(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE) = New Hyperlink(txtInstruction.Text.Trim())
                    Else
                        .SetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE, txtInstruction.Text)
                    End If
                    'end of code added by manoj on 20121130 for making result value as hyper link
                End If


                pnlDiagosisCPT.Visible = False
                _Flex.Visible = True
                pnlResults.Visible = False
                pnlInstruction.Visible = False
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInstruction_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Try

            pnlInstruction.Visible = False
            _Flex.Visible = True

        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtDgnCPTSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDgnCPTSearch.TextChanged
        'sarika 28th sept 07
        Try


            ''Sandip Darade 20090821
            ''Code commented by Sandip darade 20090821 
            ''to implement instring search 
            _bIsSearch = True
            Fill_DignosisCPT(_DgnCPT)

            'Dim strSearch As String
            'With txtDgnCPTSearch
            '    If Trim(.Text) <> "" Then
            '        strSearch = Replace(.Text, "'", "''")
            '    Else
            '        strSearch = ""
            '    End If
            'End With

            'If strColName = "Code" Then
            '    With c1DignosisCPTs
            '        .Row = .FindRow(strSearch, 1, COL_DT_CODE, False, False, True)
            '        If .Row > 0 Then
            '            Exit Sub
            '        End If

            '        '' 20070921 - Mahesh - InString Search 
            '        Dim strNode As String = ""
            '        For i As Int16 = 1 To .Rows.Count - 1
            '            strNode = ""
            '            strNode = UCase(.GetData(i, COL_DT_CODE).ToString.Trim)
            '            If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
            '                .Row = i
            '                Exit Sub
            '            End If
            '        Next
            '        '' ---
            '    End With
            'ElseIf strColName = "Description" Then
            '    With c1DignosisCPTs
            '        .Row = .FindRow(strSearch, 1, COL_DT_DESCRIPTON, False, False, True)
            '        If .Row > 0 Then
            '            Exit Sub
            '        End If

            '        '' 20070921 - Mahesh - InString Search 
            '        Dim strNode As String = ""
            '        For i As Int16 = 1 To .Rows.Count - 1
            '            strNode = ""
            '            strNode = UCase(.GetData(i, COL_DT_DESCRIPTON).ToString.Trim)
            '            If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
            '                .Row = i
            '                Exit Sub
            '            End If
            '        Next
            '        '' ---
            '    End With
            'Else
            '    'select
            '    With c1DignosisCPTs
            '        .Row = .FindRow(strSearch, 1, COL_DT_CODE, False, False, True)
            '        If .Row > 0 Then
            '            Exit Sub
            '        End If

            '        '' 20070921 - Mahesh - InString Search 
            '        Dim strNode As String = ""
            '        For i As Int16 = 1 To .Rows.Count - 1
            '            strNode = ""
            '            strNode = UCase(.GetData(i, COL_DT_CODE).ToString.Trim)
            '            If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
            '                .Row = i
            '                Exit Sub
            '            End If
            '        Next
            '        '' ---
            '    End With
            'End If


            '----------------------------------------------------------------------------------------
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Lab Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub c1DignosisCPTs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DignosisCPTs.MouseDown
        'sarika 28th sept 07
        Try
            If c1DignosisCPTs.ColSel > 1 Then
                strColName = c1DignosisCPTs.Cols(c1DignosisCPTs.ColSel).Caption
                ' lblDgnCPTSearch.Text = "Search on " & strColName
                lbl_SearchColumn.Text = strColName
            End If
            If strColName = "" Then
                'lblDgnCPTSearch.Text = "Search on Code"
                lbl_SearchColumn.Text = "Code"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Lab Transaction Control", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '-------------------------------------------------
    End Sub


    Private Sub lblDgnCPTSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub c1DignosisCPTs_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DignosisCPTs.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub c1Results_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Results.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        'start of codde added by manoj on 20121127 
        Try
            ht = c1Results.HitTest(e.X, e.Y)
            If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf c1Results(ht.Row, ht.Column) Is Hyperlink Then
                Cursor = Cursors.Hand
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
        'end of codde added by manoj on 20121127 
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        'start of codde added by manoj on 20121127 
        Try
            ht = _Flex.HitTest(e.X, e.Y)
            If (ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell) AndAlso TypeOf _Flex(ht.Row, ht.Column) Is Hyperlink Then
                Cursor = Cursors.Hand
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
        'end of codde added by manoj on 20121127 
    End Sub

    '' SUDHIR 20090626 '' TO SHOW/HIDE MAIN TOOL STRIP ''
    Private Sub pnlDiagosisCPT_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlDiagosisCPT.VisibleChanged
        If pnlDiagosisCPT.Visible Then
            RaiseEvent OnSubWindow_Opened(sender, e)
        Else
            RaiseEvent OnSubWindow_Closed(sender, e)
        End If
    End Sub

    Private Sub pnlInstruction_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlInstruction.VisibleChanged
        If pnlInstruction.Visible Then
            DisableSearchClear()
            RaiseEvent OnSubWindow_Opened(sender, e)
        Else
            EnableSearchClear()
            RaiseEvent OnSubWindow_Closed(sender, e)
        End If
    End Sub

    Private Sub pnlResults_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlResults.VisibleChanged
        If pnlResults.Visible Then
            DisableSearchClear()
            RaiseEvent OnSubWindow_Opened(sender, e)
        Else
            EnableSearchClear()
            RaiseEvent OnSubWindow_Closed(sender, e)
        End If
    End Sub
    '' END SUDHIR ''

    ''Sandip Darade 20090929
    ''fill CPTs and Dignosis in the respective combolists pulling from table ExamICD9CPT  
    'Bug #81526: 00000893 :Unable to select from dx drop down unless select the grey box first 
    Public Sub Fill_Diagnosis_CPT()
        Try
            Dim _DB As New gloEMRDatabase.DataBaseLayer
            Dim _strSQL As String = ""
            strDia = ""
            strCPT = ""
            ''nIcdRevision added for icd10 functionality 
            _strSQL = " SELECT  DISTINCT ISNull(sICD9Code,'') as sICD9Code, isnull(sICD9Description,'') as sICD9Description ,ISNULL(nIcdRevision,9) as nIcdRevision  " _
                       & " FROM ExamICD9CPT  WHERE   nPatientID = " & nPatientID & " UNION   SELECT Distinct  ISNULL(Lab_Order_TestDtl_DiagCPT.labodtl_Code ,'') AS labodtl_Code , " _
                       & " ISNULL(Lab_Order_TestDtl_DiagCPT.labodtl_Description,'') as labodtl_Description  , ISNULL(Lab_Order_TestDtl_DiagCPT.nICDRevision,9) as nICDRevision FROM    Lab_Order_MST " _
                       & " INNER JOIN Lab_Order_TestDtl ON dbo.Lab_Order_MST.labom_OrderID=Lab_Order_TestDtl.labotd_OrderID  LEFT OUTER JOIN Lab_Order_TestDtl_DiagCPT ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl_DiagCPT.labodtl_OrderID  " _
                       & " AND   Lab_Order_TestDtl_DiagCPT.labodtl_TestID =Lab_Order_TestDtl.labotd_TestID  WHERE labodtl_Type=1 and Lab_Order_MST.labom_patientid =" & nPatientID
            Dim dt As DataTable = Nothing
            Dim i As Integer
            '' Fill Diagnosis Of the Patient 
            dt = _DB.GetDataTable_Query(_strSQL)

            _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1
            Dim _ComboList As String = ""
            Dim _Code As String = "", _Description As String = ""
            Dim _Dignosis As New gloEMRActors.LabActor.ItemDetails ''for Diagnosis 
            Dim _Dignos As gloEMRActors.LabActor.ItemDetail ''for Diagnosis
            Dim _Dignosis1 As New gloEMRActors.LabActor.ItemDetails ''for CPT 
            Dim _Dignos1 As gloEMRActors.LabActor.ItemDetail ''for CPT 

            ''Diagnosis 
            ''nIcdRevision added for icd10 functionality 
            Dim items(dt.Rows.Count - 1) As String

            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    If (Convert.ToString(dt.Rows(i)("sICD9Code")) <> "") Then

                        ''                          ICD9Code                        ICD9Description
                        strDia = strDia & "|" & dt.Rows(i)("sICD9Code") & "-" & dt.Rows(i)("sICD9Description")
                        _Dignos = New gloEMRActors.LabActor.ItemDetail
                        With _Dignos
                            .Code = dt.Rows(i)("sICD9Code")
                            .Description = dt.Rows(i)("sICD9Description")
                            .IcdRevision = dt.Rows(i)("nIcdRevision")
                            items(i) = .Code & "-" & .Description
                            ' _FlexDxCode = _FlexDxCode & .Code & "-" & .Description & "|"
                        End With
                        _Dignosis.Add(_Dignos)
                        _Dignos = Nothing
                    End If
                Next
                dt.Dispose()
                dt = Nothing
            End If
            'If (_FlexDxCode <> "") Then
            '    _FlexDxCode = _FlexDxCode.Remove(_FlexDxCode.LastIndexOf("|"), 1)
            'End If
            ''CPT
            ''nIcdRevision added for icd10 functionality 
            _strSQL = " SELECT  DISTINCT ISNull(sCPTCode,'') as sCPTCode, isnull(sCPTDescription,'') as sCPTDescription ,ISNULL(nIcdRevision,9) as nIcdRevision " _
                       & " FROM ExamICD9CPT  WHERE   nPatientID = " & nPatientID & "  "

            dt = Nothing
            '' Fill CPTs Of the Patient 
            dt = _DB.GetDataTable_Query(_strSQL)
            ''nIcdRevision added for icd10 functionality 

            If IsNothing(dt) = False Then

                For i = 0 To dt.Rows.Count - 1
                    If (Convert.ToString(dt.Rows(i)("sCPTCode")) <> "") Then
                        ''                          CPTCode                        CPTDescription
                        strCPT = strCPT & "|" & dt.Rows(i)("sCPTCode") & "-" & dt.Rows(i)("sCPTDescription")
                        _Dignos1 = New gloEMRActors.LabActor.ItemDetail
                        With _Dignos1
                            .Code = dt.Rows(i)("sCPTCode")
                            .Description = dt.Rows(i)("sCPTDescription")
                            .IcdRevision = dt.Rows(i)("nIcdRevision")

                        End With
                        _Dignosis1.Add(_Dignos1)
                        _Dignos1 = Nothing

                    End If
                Next
                dt.Dispose()
                dt = Nothing
            End If

            ''Fill combolist for Diagnosis and CPT 


            If Not IsNothing(c1DropDownControl1) Then
                If Not IsNothing(c1DropDownControl1.DropDownForm) Then
                    c1DropDownControl1.DropDownForm.Close()
                End If
                c1DropDownControl1.Dispose()
                c1DropDownControl1 = Nothing
            End If
            If Not IsNothing(dropdown) Then
                dropdown.Dispose()
                dropdown = Nothing

            End If
            c1DropDownControl1 = New C1.Win.C1Input.C1DropDownControl
            Dim _isdisable As Boolean = False
            _isdisable = GetEmdeonOrderStatus(_CurrentOrderID)
            dropdown = New gloUserControlLibrary.CheckedListBoxDroDown(items, _isdisable)
            c1DropDownControl1.DropDownForm = dropdown
            _Flex.Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Editor = c1DropDownControl1
            If _Flex.Rows.Count > 1 Then
                _Flex.SetData(_CurrentRow, COL_TEST_DIGNOSISLIST, _Dignosis)
            End If

            Dim csCPTList As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("CS_CPT" & _DgnCPTRunTimeComboList)
            Try
                If (_Flex.Styles.Contains("CS_CPT" & _DgnCPTRunTimeComboList)) Then
                    csCPTList = _Flex.Styles("CS_CPT" & _DgnCPTRunTimeComboList)
                Else
                    csCPTList = _Flex.Styles.Add("CS_CPT" & _DgnCPTRunTimeComboList)
                    With csCPTList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        .DataType = Type.GetType("System.String")
                        .ComboList = strCPT
                    End With
                End If
            Catch ex As Exception
                csCPTList = _Flex.Styles.Add("CS_CPT" & _DgnCPTRunTimeComboList)
                With csCPTList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    .DataType = Type.GetType("System.String")
                    .ComboList = strCPT
                End With
            End Try
            If _Flex.Rows.Count > 1 Then
                _Flex.SetCellStyle(_CurrentRow, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_CPT" & _DgnCPTRunTimeComboList))
                _Flex.SetData(_CurrentRow, COL_TEST_CPTLIST, _Dignosis1)
            End If

            ' _Flex.SetData(_CurrentRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _FlexDxCode)
            ' If _isCellButtonClick Then


            'Dim _strflex As String = ""
            'Dim oDgnColl As gloEMRActors.LabActor.ItemDetails = CType(_Flex.GetData(_CurrentRow, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
            'Dim sDxCode As String = Convert.ToString(_Flex.GetData(_CurrentRow, COL_TEST_DIGNOSIS_RESULT_VALUE))

            'If sDxCode <> "" Then
            '    Dim DxCode() As String = Nothing
            '    DxCode = sDxCode.Split("|")

            '    If Not IsNothing(DxCode) Then
            '        If DxCode.Length >= 1 Then
            '            For Each Str As String In DxCode
            '                For k As Integer = 0 To oDgnColl.Count - 1

            '                    If oDgnColl.Item(k).Code = Str.Substring(0, Str.IndexOf("-")).Trim Then
            '                        _strflex = _strflex & oDgnColl.Item(k).Code & "-" & oDgnColl.Item(k).Description & "|"
            '                        ' Exit For
            '                    End If
            '                Next
            '            Next

            '        End If
            '    End If
            'End If
            ''   For i = 1 To _Flex.Rows.Count - 1
            ''    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
            '_Flex.SetData(_CurrentRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _strflex)
            'End If


            '  Next
            ' End If
            _DB.Dispose()
            _DB = Nothing

        Catch ex As Exception

        End Try
    End Sub

    'Madan-- added for closing event..
    Private Sub _Flex_CellChanged(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellChanged


        If _IsLoading = False Then
            _LabModified = True
            If _CurrentOrderID > 0 Then
                RaiseEvent LockOrder(_CurrentOrderID)
            End If

        End If

    End Sub

    'Madan --- added this method on 20100617
    Public Sub ChangeFlexToReadOnly(ByVal IsToBeChanged As Boolean)

        'If IsToBeChanged Then
        '    _Flex.AllowEditing = False
        'Else
        '    _Flex.AllowEditing = True
        'End If

    End Sub

    Private Function RetriveAppSettings() As Boolean
        Dim blnResult As Boolean = False
        Try

            If appSettings IsNot Nothing Then
                _ConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                _nUserId = Convert.ToInt64(appSettings("UserID"))
            End If


            If _ConnectionString.Trim().Length > 0 And _nUserId > 0 Then
                blnResult = True
            End If

        Catch ex As Exception
            blnResult = False
        End Try
        Return blnResult
    End Function
    ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
    'called when user tries to minimize width column contains dropdown listbox. 
    ''Start
    Private Sub _Flex_BeforeResizeColumn(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.BeforeResizeColumn
        Try
            With _Flex
                If e.Col = COL_TEST_DIGNOSIS_RESULT_VALUE Or e.Col = COL_TEST_CPT_RESULT_UNIT Or e.Col = COL_TEST_STATUS Then
                    If .Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).Width < prevWidth_COL_TEST_DIGNOSIS_RESULT_VALUE Then
                        e.Cancel = True
                    End If

                    If .Cols(COL_TEST_CPT_RESULT_UNIT).Width < prevWidth_COL_TEST_CPT_RESULT_UNIT Then
                        e.Cancel = True
                    End If

                    If .Cols(COL_TEST_STATUS).Width < prevWidth_COL_TEST_STATUS Then
                        e.Cancel = True
                    End If
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.RetrivedAllLabOrders, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    ''End
    Public Sub EndEditTestGrid()
        _Flex.FinishEditing()
    End Sub

    'added by manoj on 20121127 for drawing result as hyper link
    Private Sub _flex_OwnerDrawCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles _Flex.OwnerDrawCell
        Try
            If Not _Flex(e.Row, e.Col) Is Nothing AndAlso TypeOf _Flex(e.Row, e.Col) Is Hyperlink Then
                If _Flex(e.Row, e.Col).Visited() Then
                    e.Style = _Flex.Styles("OldLink")
                Else
                    e.Style = _Flex.Styles("NewLink")
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj on 20121127 for drawing result as hyper link
    Private Sub c1Results_OwnerDrawCell(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles c1Results.OwnerDrawCell
        Try
            If Not c1Results(e.Row, e.Col) Is Nothing AndAlso TypeOf c1Results(e.Row, e.Col) Is Hyperlink Then
                If c1Results(e.Row, e.Col).Visited Then
                    e.Style = c1Results.Styles("OldLink")
                Else
                    e.Style = c1Results.Styles("NewLink")
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj on 20121127 for drawing result as hyper link
    Private Sub c1Results_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Results.MouseDown
        Try
            If Cursor Is Cursors.Hand Then
                ht = c1Results.HitTest(e.X, e.Y)
                If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                    If TypeOf c1Results(ht.Row, ht.Column) Is Hyperlink Then
                        c1Results(ht.Row, ht.Column).Activate()
                    End If
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
    End Sub

    'added by manoj on 20121127 for drawing result as hyper link
    Private Sub txtTestResultComment_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles txtTestResultComment.LinkClicked
        Try
            If Not String.IsNullOrEmpty(e.LinkText.Trim()) Then
                gloGlobal.gloLabGenral.OpenLinkInBrowser(e.LinkText.Trim())
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Public Sub SetWordTemplate(ByVal tempByte As Object)
        If _Flex.Row > 0 Then
            _Flex.SetData(_Flex.Row, COL_TEST_WORDTEMPLATE, tempByte)
        End If

    End Sub
    Public Sub SetTemplateIsFinished(ByVal IsFinished As Boolean, ByVal curTest As Long)
        If _Flex.Row > 0 AndAlso _Flex.GetData(_Flex.Row, COL_TESTID) = curTest Then
            '_Flex.SetData(_Flex.Row, COL_ISFINISHED, IsFinished)
            If IsFinished = True Then
                _Flex.SetData(_Flex.Row, COL_ISFINISHED, "Yes")
            Else
                _Flex.SetData(_Flex.Row, COL_ISFINISHED, "No")
            End If
        End If
    End Sub
    Public Function GetTemplateIsFinished(ByVal curTest As Long) As Boolean
        If _Flex.Row > 0 AndAlso _Flex.GetData(_Flex.Row, COL_TESTID) = curTest Then
            If Convert.ToString(_Flex.GetData(_Flex.Row, COL_ISFINISHED)) = "Yes" Then
                Return True
            Else
                Return False
            End If
            'Return _Flex.GetData(_Flex.Row, COL_ISFINISHED)
        End If

    End Function
    Public Sub ShowLabOrderComments(ByVal _sOrdercomments As String) 'Added by manoj on 20140407 PRD:View Lab Order Comments for Electronic Lab orders
        Try
            txtTestResultComment.Visible = True
            Splitter1.Visible = True
            txtTestResultComment.Text = _sOrdercomments
            Splitter1.BringToFront()
        Catch
        Finally
            _sOrdercomments = String.Empty
        End Try

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtDgnCPTSearch.ResetText()
        txtDgnCPTSearch.Focus()
    End Sub

    Private Sub c1DropDownControl1_Leave(sender As System.Object, e As System.EventArgs) Handles c1DropDownControl1.Leave
        'c1DropDownControl1.CloseDropDown()
        'c1DropDownControl1.CloseDropDown(True)

        'If Not IsNothing(c1DropDownControl1.DropDownForm) Then
        '    c1DropDownControl1.DropDownForm.Close()
        'End If
    End Sub
End Class
