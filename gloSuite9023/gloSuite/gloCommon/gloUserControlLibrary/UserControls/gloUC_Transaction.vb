Imports gloEMRGeneralLibrary
Public Class gloUC_Transaction


#Region "private variables and events"


    Private _TransactionType As enumTransactionType

    Private Const COL_TEST_RESULT_SELECT = 0
    Private Const COL_TEST_RESULT_NAME = 1
    Private Const COL_TEST_RESULT_CODE = 2
    Private Const COL_ORDERID = 3
    Private Const COL_TESTID = 4
    Private Const COL_TEST_LINENO = 5
    Private Const COL_RESULT_NUMBER = 6
    Private Const COL_RESULT_LINENO = 7
    Private Const COL_RESULT_NAMEID = 8
    Private Const COL_TEST_DIGNOSIS_RESULT_VALUE = 9
    Private Const COL_TEST_DIGNOSISLIST = 10
    Private Const COL_TEST_DIGNOSISBTN = 11
    Private Const COL_TEST_CPT_RESULT_UNIT = 12
    Private Const COL_TEST_CPTLIST = 13
    Private Const COL_TEST_CPTBTN = 14

    Private Const COL_TEST_NOTE = 15
    Private Const COL_TEST_SPECIMEN = 16
    Private Const COL_TEST_COLLECTION = 17
    Private Const COL_TEST_STORAGE = 18
    'Private Const COL_TEST_INSTRUCTON = 20
    'Private Const COL_TEST_PRECUATON = 21
    Private Const COL_RESULT_VALUE_TYPE = 19
    Private Const COL_RESULT_RANGE = 20
    Private Const COL_RESULT_COMMENT = 21
    Private Const COL_ABNORMAL_FLAG = 22  '' Added on 20070602
    Private Const COL_RESULT_TYPE = 23

    Private Const COL_TEST_INSTRUCTION = 24
    Private Const COL_TEST_INSTRUCTIONBTN = 25
    Private Const COL_TEST_PRECAUTION = 26
    Private Const COL_TEST_PRECAUTIONBTN = 27
    Private Const COL_TEST_COMMENTS = 28
    Private Const COL_TEST_COMMENTSBTN = 29

    Private Const COL_TEST_RESULT_DATETIME = 30
    Private Const COL_TEST_RESULT_USERID = 31
    Private Const COL_RECORDTYPE = 32
    Private Const COL_TEST_LOINC = 33

    Private Const COL_ISFINISHED = 34

    Private Const COL_DMSID = 35
    Private Const COL_SCAN = 36
    Private Const COL_VIEW = 37

    '' 20090212 - Added New Constant for Test Name
    Private Const COL_TESTNAME = 38

    Private Const COL_DMSIDCollection = 39
    ''

    Private Const COL_COUNT = 40




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

    ''Event added by Sandip Darade 20090929
    Public Event gUC_ButtonDiagnCPTClicked()



    'Private _ResultTypeList As String = "|" & gloEMRActors.LabActor.enumTestResultReadType.Prilimnary.ToString & "|" & gloEMRActors.LabActor.enumTestResultReadType.Final.ToString & "|" & gloEMRActors.LabActor.enumTestResultReadType.Ammend.ToString
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

    '' SUDHIR 20090626 ''
    Public Event OnSubWindow_Closed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event OnSubWindow_Opened(ByVal sender As Object, ByVal e As EventArgs)

    'sarika 28th sept 07
    Dim strColName As String = ""
    '----------------------------------------------------------

    'sarika Labs Denormalization 20090323
    '    Public arrTestID As ArrayList
    Public arrTestNames As ArrayList
    '---
    ''Sandip Darade 200908221
    ''whether to select diagnosis or CPT 
    Private _DgnCPT As Int16 = 1
    Private _bIsSearch As Boolean

    ''Sandip Darade 20090929
    Dim strDia As String = ""
    Dim strCPT As String = ""
    Dim nPatientID As Int64
    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property

    Private _LabModified As Boolean = False
    Private _IsLoading As Boolean = False
#End Region

#Region "Properties and Enums"



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

    Public ReadOnly Property LabModified() As Boolean
        Get
            Return _LabModified
        End Get
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
            .SetData(0, COL_TEST_INSTRUCTION, "Instruction")
            .SetData(0, COL_TEST_INSTRUCTIONBTN, "...")
            .SetData(0, COL_TEST_PRECAUTION, "Precaution")
            .SetData(0, COL_TEST_PRECAUTIONBTN, "...")
            .SetData(0, COL_TEST_COMMENTS, "Comments")
            .SetData(0, COL_TEST_COMMENTSBTN, "...")
            .SetData(0, COL_RESULT_VALUE_TYPE, "Value Type")
            .SetData(0, COL_RESULT_RANGE, "")
            .SetData(0, COL_RESULT_TYPE, "")
            .SetData(0, COL_RESULT_COMMENT, "Comment")
            .SetData(0, COL_TEST_RESULT_DATETIME, "DateTime")
            .SetData(0, COL_TEST_RESULT_USERID, "R. UserID")
            .SetData(0, COL_RECORDTYPE, "Rcd. Type")
            .SetData(0, COL_TEST_LOINC, "")
            .SetData(0, COL_ISFINISHED, "Finished")


            .SetData(0, COL_DMSID, "DMS ID")

            .SetData(0, COL_DMSIDCollection, "DMSID Collection")

            .SetData(0, COL_SCAN, "Scan")
            .SetData(0, COL_VIEW, "View")

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
            .Cols(COL_TEST_INSTRUCTION).Width = 100
            .Cols(COL_TEST_INSTRUCTIONBTN).Width = 20
            .Cols(COL_TEST_PRECAUTION).Width = 100
            .Cols(COL_TEST_PRECAUTIONBTN).Width = 20
            .Cols(COL_TEST_COMMENTS).Width = 100
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
            .Cols(COL_TEST_RESULT_DATETIME).Width = 100
            .Cols(COL_TEST_RESULT_USERID).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_TEST_LOINC).Width = 100
            .Cols(COL_ISFINISHED).Width = 0

            .Cols(COL_DMSID).Width = 0
            .Cols(COL_DMSIDCollection).Width = 0

            .Cols(COL_SCAN).Width = 0
            .Cols(COL_VIEW).Width = 0
            .Cols(COL_TESTNAME).Width = 0 ''Hide by Sudhir - 20090227

            .Cols(COL_RESULT_RANGE).AllowEditing = False
            .Cols(COL_RESULT_TYPE).AllowEditing = False
            .Cols(COL_ABNORMAL_FLAG).AllowEditing = False
            .Cols(COL_TEST_RESULT_NAME).AllowEditing = False '' SUDHIR 20090717 '' RC1 BUG ''
            .Cols(COL_TEST_RESULT_DATETIME).AllowEditing = False '' SUDHIR 20090720 '' RC1 BUG ''

        
        End With
    End Sub

    Private Sub DesignDignosisCPTGrid()
        SetGridStyle(c1DignosisCPTs)

        With c1DignosisCPTs
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
        End With
        ' lblDgnCPTSearch.Text = "Search on Code"
    End Sub

    Private Sub DesignResultGrid()
        SetGridStyle(c1Results)

        With c1Results
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT



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
            .SetData(0, COL_TEST_LOINC, "LOINC Code")
            .SetData(0, COL_TEST_INSTRUCTION, "Instruction")
            .SetData(0, COL_TEST_PRECAUTION, "Precuation")
            .SetData(0, COL_RESULT_VALUE_TYPE, "Value Type")
            .SetData(0, COL_RESULT_RANGE, "Range")
            .SetData(0, COL_ABNORMAL_FLAG, "Flag")
            .SetData(0, COL_RESULT_TYPE, "Result Type")
            .SetData(0, COL_RESULT_COMMENT, "R. Comment")
            .SetData(0, COL_TEST_RESULT_DATETIME, "R. DateTime")
            .SetData(0, COL_TEST_RESULT_USERID, "R. UserID")
            .SetData(0, COL_RECORDTYPE, "Rcd. Type")
            .SetData(0, COL_ISFINISHED, "Is Finished")

            .SetData(0, COL_DMSID, "DMS ID")

            .SetData(0, COL_DMSIDCollection, " DMSID Collection")

            .SetData(0, COL_SCAN, "Scan")
            .SetData(0, COL_VIEW, "View")

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
            .Cols(COL_TEST_RESULT_CODE).Visible = False
            .Cols(COL_ORDERID).Visible = False
            .Cols(COL_TESTID).Visible = False
            .Cols(COL_TESTNAME).Visible = False
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
            .Cols(COL_TEST_LOINC).Visible = True
            .Cols(COL_TEST_INSTRUCTION).Visible = False
            .Cols(COL_TEST_INSTRUCTIONBTN).Visible = False
            .Cols(COL_TEST_PRECAUTION).Visible = False
            .Cols(COL_TEST_PRECAUTIONBTN).Visible = False
            .Cols(COL_RESULT_VALUE_TYPE).Visible = False
            .Cols(COL_RESULT_RANGE).Visible = True
            .Cols(COL_ABNORMAL_FLAG).Visible = True
            .Cols(COL_RESULT_TYPE).Visible = True
            .Cols(COL_RESULT_COMMENT).Visible = False
            .Cols(COL_TEST_RESULT_DATETIME).Visible = False
            .Cols(COL_TEST_RESULT_USERID).Visible = False
            .Cols(COL_RECORDTYPE).Visible = False
            .Cols(COL_ISFINISHED).Visible = False

            .Cols(COL_DMSID).Visible = False
            .Cols(COL_DMSIDCollection).Visible = False
            .Cols(COL_SCAN).Visible = False
            .Cols(COL_VIEW).Visible = False

            Dim _Width As Single
            If .Width = 980 Then
                _Width = ((.Width / 2) - 25) / 7
            Else
                _Width = (.Width - 25) / 7
            End If

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
            .Cols(COL_TEST_INSTRUCTION).Width = 0
            .Cols(COL_TEST_INSTRUCTIONBTN).Width = 0
            .Cols(COL_TEST_PRECAUTION).Width = 0
            .Cols(COL_TEST_PRECAUTIONBTN).Width = 0
            .Cols(COL_TEST_COMMENTS).Width = 0
            .Cols(COL_TEST_COMMENTSBTN).Width = 0
            .Cols(COL_RESULT_VALUE_TYPE).Width = 0
            .Cols(COL_RESULT_RANGE).Width = _Width * 0.8
            .Cols(COL_RESULT_TYPE).Width = _Width * 1.2
            .Cols(COL_ABNORMAL_FLAG).Width = _Width * 1.2
            .Cols(COL_RESULT_COMMENT).Width = 0
            .Cols(COL_TEST_RESULT_DATETIME).Width = 0
            .Cols(COL_TEST_RESULT_USERID).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_ISFINISHED).Width = 0

            .Cols(COL_DMSID).Width = 0
            .Cols(COL_DMSIDCollection).Width = 0
            .Cols(COL_SCAN).Width = 0
            .Cols(COL_VIEW).Width = 0
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

    Public Function SetData(ByVal oOrder As gloEMRActors.LabActor.LabOrder) As Boolean

        If oOrder Is Nothing Then
            SetData = Nothing
            Exit Function
        End If


        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0

        _CurrentOrderID = oOrder.OrderID
        nPatientID = oOrder.PatientID
        'First Clear All the Grids
        ClearTest()
        _IsLoading = True
        With oOrder.OrderTests
            For nTest As Int16 = 0 To .Count - 1
                'Labs Denormalization 20090321
                '_FillTestName = GetTestName(.Item(nTest).TestID)
                _FillTestName = .Item(nTest).TestName
                '---
                _FillTestCode = GetTestCode(.Item(nTest).TestID)

                If IsTestExists(.Item(nTest).OrderID, .Item(nTest).TestID, _FillTestName) = False Then
                    With .Item(nTest)
                        '//---Add Test---Start---//
                        _Flex.Rows.Add()

                        _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail
                        _FillTestLineNo = .TestLineNo

                        With _Flex.Rows(_Flex.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 22
                            .IsNode = True
                            .Style = _Flex.Styles("CS_Record")
                            .Node.Level = 0
                            .Node.Image = ImgTest.Image
                            .Node.Data = _FillTestName
                        End With

                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_CheckBox"))
                        '_Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                        ' _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_CPTBTN, _Flex.Styles("CS_ComboList"))

                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, _Flex.Styles("CS_ComboList"))
                        _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_COMMENTSBTN, _Flex.Styles("CS_ComboList"))

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

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_DMSIDCollection, .DMSIDCollection)

                        '/-Fill Diagnosis & CPTs-Start/
                        Dim _ComboList As String = ""
                        Dim _Code As String = "", _Description As String = ""
                        _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1

                        If Not .Diagonosis Is Nothing Then
                            For d As Int16 = 0 To .Diagonosis.Count - 1
                                _Code = .Diagonosis.Item(d).Code
                                _Description = .Diagonosis.Item(d).Description
                                _ComboList = _ComboList & "|" & _Code & "-" & _Description & "|"
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
                          

                            _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_DigCPT" & _DgnCPTRunTimeComboList))
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_DIGNOSISLIST, .Diagonosis)
                        End If

                        _ComboList = "" : _Code = "" : _Description = ""
                        _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1

                        If Not .Treatments Is Nothing Then
                            For t As Int16 = 0 To .Treatments.Count - 1
                                _Code = .Treatments.Item(t).Code
                                _Description = .Treatments.Item(t).Description
                                _ComboList = _ComboList & "|" & _Code & "-" & _Description & "|"
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
                        End If
                        Fill_Diagnosis_CPT()
                        '/-Fill Diagnosis & CPTs-Finish/
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_NUMBER, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_LINENO, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_NAMEID, 0)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_NOTE, .Note)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_SPECIMEN, .Specimen)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_COLLECTION, .Collection)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_STORAGE, .Storage)
                        'If .LOINCCode <> 0 Then ''Sudhir 20090227
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_LOINC, .LOINCCode)
                        'End If
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_INSTRUCTION, .Instruction)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_PRECAUTION, .Precaution)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_COMMENTS, .Comments)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_RANGE, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_VALUE_TYPE, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_COMMENT, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_TYPE, "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_DATETIME, .TestDateTime)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TEST_RESULT_USERID, .UserID)
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))

                        '//---Add Test---Finish---//

                        '// If _TransactionType = enumTransactionType.LabResult Then

                        Dim nStyleRow As Int16 = 0
                        Dim nNodeRow As Int16 = 0
                        Dim i As Int16 = 0

                        For nResults As Int16 = 0 To .OrderTestResults.Count - 1
                            With .OrderTestResults.Item(nResults)
                                '//---Add Test Result Header---Start---//
                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .TestResultName, .TestID & .TestResultNumber, ImgResultHeader.Image)
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
                                _Flex.SetData(nStyleRow, COL_TEST_LOINC, "LOINC Code")
                                _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                                _Flex.SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                                _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                _Flex.SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                _Flex.SetData(nStyleRow, COL_TEST_COMMENTS, "")
                                _Flex.SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                _Flex.SetData(nStyleRow, COL_RESULT_RANGE, "Range")
                                _Flex.SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "Value Type")
                                _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, "Flag")
                                _Flex.SetData(nStyleRow, COL_RESULT_TYPE, "Result Type")
                                _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, "R. Comment")
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_DATETIME, .TestResultDateTime)
                                _Flex.SetData(nStyleRow, COL_TEST_RESULT_USERID, "R. UserID")
                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                _Flex.SetData(nStyleRow, COL_ISFINISHED, .IsFinished)
                                _Flex.SetData(nStyleRow, COL_DMSID, .DMSID)
                                _Flex.SetData(nStyleRow, COL_DMSIDCollection, .DMSIDCollection)

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
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult.Image)
                                            End If
                                        Else
                                            _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .ResultName, .TestResultNumber & nResult, ImgResult.Image)
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
                                        _Flex.SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, .ResultValue)
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
                                        _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_COMMENTS, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                        _Flex.SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                        _Flex.SetData(nStyleRow, COL_RESULT_RANGE, .ResultRange)
                                        _Flex.SetData(nStyleRow, COL_RESULT_VALUE_TYPE, Nothing) ' Remark its remaing

                                        _Flex.SetData(nStyleRow, COL_RESULT_TYPE, _ObservationStatus_COL.GetDescription(.ResultTypeCode))
                                        _Flex.SetData(nStyleRow, COL_ABNORMAL_FLAG, _AbnormalFlag_COL.GetDescription(.AbnormalFlagCode))

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
                                        _Flex.SetData(nStyleRow, COL_TEST_RESULT_USERID, .UserID) '// Remaining
                                        _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                                        _Flex.SetData(nStyleRow, COL_ISFINISHED, .IsFinished)
                                        '---------------------------------------------------------

                                        With _Flex.Rows(nStyleRow)
                                            .ImageAndText = False
                                            .Height = 22
                                            If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG)) = "N" OrElse IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG))) Then
                                                'Noraml and nothing
                                                .Style = _Flex.Styles("CS_Record")
                                            Else
                                                .Style = _Flex.Styles("CS_NotNormal")
                                                ' '' Not Normal 
                                            End If

                                            .AllowEditing = False
                                        End With

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

            For nTest As Int16 = 1 To _Flex.Rows.Count - 1
                If Val(_Flex.GetData(nTest, COL_RECORDTYPE) & "") = enumRecordType.Test Then
                    oOrderTest = New gloEMRActors.LabActor.OrderTest
                    With oOrderTest
                        .OrderID = _Flex.GetData(nTest, COL_ORDERID)
                        .TestID = _Flex.GetData(nTest, COL_TESTID)
                        .TestName = _Flex.GetData(nTest, COL_TESTNAME)

                        If _Flex.GetCellCheck(nTest, COL_TEST_RESULT_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            'sarika Labs Denormalization 20090323
                            '                            arrTestID.Add(_Flex.GetData(nTest, COL_TESTID))
                            arrTestNames.Add(_Flex.GetData(nTest, COL_TESTNAME))
                            '---
                        End If
                        .TestLineNo = _Flex.GetData(nTest, COL_TEST_LINENO)

                        If Not _Flex.GetData(nTest, COL_TEST_DIGNOSISLIST) Is Nothing Then
                            Dim oDgnColl As gloEMRActors.LabActor.ItemDetails
                            oDgnColl = CType(_Flex.GetData(nTest, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
                            For i As Int16 = 0 To oDgnColl.Count - 1
                                .Diagonosis.Add(oDgnColl.Item(i))
                            Next
                            oDgnColl = Nothing
                        End If

                        If Not _Flex.GetData(nTest, COL_TEST_CPTLIST) Is Nothing Then
                            Dim oCPTColl As gloEMRActors.LabActor.ItemDetails
                            oCPTColl = CType(_Flex.GetData(nTest, COL_TEST_CPTLIST), gloEMRActors.LabActor.ItemDetails)
                            For i As Int16 = 0 To oCPTColl.Count - 1
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
                        .LOINCCode = _Flex.GetData(nTest, COL_TEST_LOINC)

                        .Instruction = _Flex.GetData(nTest, COL_TEST_INSTRUCTION)
                        .Precaution = _Flex.GetData(nTest, COL_TEST_PRECAUTION)
                        .Comments = _Flex.GetData(nTest, COL_TEST_COMMENTS)
                        .TestDateTime = _Flex.GetData(nTest, COL_TEST_RESULT_DATETIME)
                        .UserID = _Flex.GetData(nTest, COL_TEST_RESULT_USERID)
                        .DMSID = _Flex.GetData(nTest, COL_DMSID)
                        .DMSID = 0
                        If Not _Flex.GetData(nTest, COL_DMSID) Is Nothing Then
                            If Not _Flex.GetData(nTest, COL_DMSID).ToString() = "" Then
                                .DMSID = Convert.ToInt64(_Flex.GetData(nTest, COL_DMSID).ToString())
                            End If
                        End If
                        'DMS Collection IDs
                        If Not _Flex.GetData(nTest, COL_DMSIDCollection) Is Nothing Then
                            If Not _Flex.GetData(nTest, COL_DMSIDCollection).ToString() = "" Then
                                .DMSIDCollection = _Flex.GetData(nTest, COL_DMSIDCollection).ToString()
                            End If
                        End If


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
                                    .DMSID = 0
                                    If Not _Flex.GetData(nResult, COL_DMSID) Is Nothing Then
                                        If Not _Flex.GetData(nResult, COL_DMSID).ToString() = "" Then
                                            .DMSID = Convert.ToInt64(_Flex.GetData(nResult, COL_DMSID).ToString())
                                        End If
                                    End If

                                    'DMS Collection ID
                                    If Not _Flex.GetData(nResult, COL_DMSIDCollection) Is Nothing Then
                                        If Not _Flex.GetData(nResult, COL_DMSIDCollection).ToString() = "" Then
                                            .DMSIDCollection = _Flex.GetData(nResult, COL_DMSIDCollection).ToString()
                                        End If
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
                                                .ResultValue = _Flex.GetData(nResultDetail, COL_TEST_DIGNOSIS_RESULT_VALUE) & ""
                                                .ResultUnit = _Flex.GetData(nResultDetail, COL_TEST_CPT_RESULT_UNIT) & ""
                                                .ResultRange = _Flex.GetData(nResultDetail, COL_RESULT_RANGE) & ""

                                                .ResultTypeCode = _ObservationStatus_COL.GetCode(_Flex.GetData(nResultDetail, COL_RESULT_TYPE) & "")
                                                .ResultTypeDesc = _Flex.GetData(nResultDetail, COL_RESULT_TYPE)

                                                .AbnormalFlagCode = _AbnormalFlag_COL.GetCode(_Flex.GetData(nResultDetail, COL_ABNORMAL_FLAG) & "")
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

                                                .ResultComment = _Flex.GetData(nResultDetail, COL_RESULT_COMMENT) & ""
                                                .ResultWord = Nothing
                                                .ResultDMSID = 0
                                                .UserID = 0
                                                .ResultLOINCID = _Flex.GetData(nResultDetail, COL_TEST_LOINC) & ""
                                                .ResultDateTime = _Flex.GetData(nResultDetail, COL_TEST_RESULT_DATETIME)
                                                .IsFinished = _Flex.GetData(nResultDetail, COL_ISFINISHED)

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

        _CurrentOrderID = OrderID
        With _Flex
            Try
                .Rows.Add()
                With .Rows(.Rows.Count - 1)
                    .ImageAndText = True
                    .Height = 22
                    .IsNode = True
                    .Style = _Flex.Styles("CS_Record")
                    .Node.Level = 0
                    .Node.Image = ImgTest.Image
                    .Node.Data = TestName
                End With

                .SetCellStyle(.Rows.Count - 1, COL_TEST_RESULT_SELECT, _Flex.Styles("CS_CheckBox"))
                '.SetCellStyle(.Rows.Count - 1, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_DIGNOSISBTN, _Flex.Styles("CS_ComboList"))
                '.SetCellStyle(.Rows.Count - 1, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_CPTBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, _Flex.Styles("CS_ComboList"))
                .SetCellStyle(.Rows.Count - 1, COL_TEST_COMMENTSBTN, _Flex.Styles("CS_ComboList"))

                Dim _DB As New gloEMRDatabase.DataBaseLayer
                Dim _strSQL As String = ""
                Dim oDataTable As DataTable


                _strSQL = "SELECT     Lab_Test_Mst.labtm_Code, Lab_Test_Mst.labtm_Name, isnull(Lab_CSST_MST.labCSST_Name,'') AS Specimen, Lab_Test_Mst.labtm_Instruction, Lab_Test_Mst.labtm_Precuation, Lab_Test_Mst.labtm_LOINCId, isnull(Lab_CSST_MST_1.labCSST_Name,'') AS Collection, isnull(Lab_CSST_MST_2.labCSST_Name,'') AS StorageTemperature, isnull(Lab_CSST_MST.labCSST_ID,0) AS SpecimenID, isnull(Lab_CSST_MST_1.labCSST_ID,0) AS CollectionID, isnull(Lab_CSST_MST_2.labCSST_ID,0) AS StorageTemperatureID FROM Lab_CSST_MST AS Lab_CSST_MST_2 RIGHT OUTER JOIN Lab_Test_Mst ON Lab_CSST_MST_2.labCSST_ID = Lab_Test_Mst.labtm_StorageID LEFT OUTER JOIN Lab_CSST_MST AS Lab_CSST_MST_1 ON Lab_Test_Mst.labtm_CollectionID = Lab_CSST_MST_1.labCSST_ID LEFT OUTER JOIN Lab_CSST_MST ON Lab_Test_Mst.labtm_SpecimenID = Lab_CSST_MST.labCSST_ID WHERE (Lab_Test_Mst.labtm_ID = " & TestID & ") "

                oDataTable = _DB.GetDataTable_Query(_strSQL)
                If Not oDataTable Is Nothing Then
                    If oDataTable.Rows.Count > 0 Then
                        For i As Int16 = 0 To oDataTable.Rows.Count - 1
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_SELECT, True)

                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Code")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_RESULT_CODE, oDataTable.Rows(i).Item("labtm_Code") & "")
                            End If

                            .SetData(.Rows.Count - 1, COL_ORDERID, OrderID)
                            .SetData(.Rows.Count - 1, COL_TESTID, TestID)
                            .SetData(.Rows.Count - 1, COL_TESTNAME, TestName)

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
                            .SetData(.Rows.Count - 1, COL_TEST_INSTRUCTION, "")
                            .SetData(.Rows.Count - 1, COL_TEST_INSTRUCTIONBTN, "")
                            .SetData(.Rows.Count - 1, COL_TEST_PRECAUTION, "")
                            .SetData(.Rows.Count - 1, COL_TEST_PRECAUTIONBTN, "")
                            .SetData(.Rows.Count - 1, COL_TEST_COMMENTS, "")
                            .SetData(.Rows.Count - 1, COL_TEST_COMMENTSBTN, "")
                            .SetData(.Rows.Count - 1, COL_TEST_NOTE, "")
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
                                .SetData(.Rows.Count - 1, COL_TEST_LOINC, oDataTable.Rows(i).Item("labtm_LOINCId") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Instruction")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_INSTRUCTION, oDataTable.Rows(i).Item("labtm_Instruction") & "")
                            End If
                            If Not IsDBNull(oDataTable.Rows(i).Item("labtm_Precuation")) Then
                                .SetData(.Rows.Count - 1, COL_TEST_PRECAUTION, oDataTable.Rows(i).Item("labtm_Precuation") & "")
                            End If
                            .SetData(.Rows.Count - 1, COL_RESULT_RANGE, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_VALUE_TYPE, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_COMMENT, "")
                            .SetData(.Rows.Count - 1, COL_RESULT_TYPE, "")
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_DATETIME, Date.Now)
                            .SetData(.Rows.Count - 1, COL_TEST_RESULT_USERID, UserID)
                            .SetData(.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
                            .SetData(.Rows.Count - 1, COL_ISFINISHED, 0)
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

    Public Function AddScanDocument(ByVal TestID As Int64, ByVal DocumentID As Int64) As Boolean
        Try
            With _Flex
                For i As Int16 = 1 To .Rows.Count - 1
                    If Val(_Flex.GetData(i, COL_RECORDTYPE)) = enumRecordType.Test Then
                        Dim _tmpTestID As Int64 = 0
                        If Val(.GetData(i, COL_TESTID)) > 0 Then
                            _tmpTestID = Convert.ToInt64(.GetData(i, COL_TESTID))
                        End If
                        If _tmpTestID = TestID Then
                            .SetData(i, COL_DMSID, DocumentID)
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
                            .SetData(i, COL_TEST_PRECAUTION, sData)
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
                            .SetData(i, COL_TEST_INSTRUCTION, sData)
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
                            .SetData(i, COL_TEST_COMMENTS, sData)
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
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)

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
                        .ForeColor = Color.Maroon
                        .BackColor = Color.FromArgb(192, 203, 233)
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csHeader = .Styles.Add("CS_Header")
                With csHeader
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                    .ForeColor = Color.Maroon
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
    End Sub

    Private Sub gloUC_Transaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _IsLoading = True
        DesignTestGrid()
        DesignDignosisCPTGrid()
        DesignResultGrid()

        Fill_AbnormalFlag()
        Fill_ResultType()

        _Flex.Visible = True
        pnlDiagosisCPT.Visible = False
        pnlResults.Visible = False

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
            .Code = "X"
            .Description = "Results cannot be obtained for this observation"
            _ResultTypeList = _ResultTypeList & "|" & .Description
        End With
        _ObservationStatus_COL.Add(ObservationStatus)
        ObservationStatus = Nothing
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
                    RaiseEvent gUC_TestSelected(_tmpTestID, _Flex.GetData(_tmpDataRow, COL_TEST_SPECIMEN) & "", _Flex.GetData(_tmpDataRow, COL_TEST_COLLECTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_STORAGE) & "", _Flex.GetData(_tmpDataRow, COL_TEST_LOINC) & "", _Flex.GetData(_tmpDataRow, COL_TEST_INSTRUCTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_PRECAUTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_COMMENTS) & "")
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


                If e.Row > 0 And (e.Col = COL_TEST_DIGNOSISBTN Or e.Col = COL_TEST_CPTBTN Or (e.Col = COL_TEST_INSTRUCTIONBTN Or e.Col = COL_TEST_PRECAUTIONBTN Or e.Col = COL_TEST_COMMENTSBTN)) Then
                    If Val(.GetData(e.Row, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        Try
                            _CurrentRow = e.Row
                            _CurrentColumn = e.Col

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
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_TEST_INSTRUCTION)
                                lblTestDetails_Header.Text = "      Instructions for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)
                                lblTestDetails_Header.ForeColor = Color.FromArgb(31, 73, 125)
                            ElseIf _CurrentColumn = COL_TEST_PRECAUTIONBTN Then
                                ' ''
                                pnlInstruction.Visible = True
                                pnlInstruction.Dock = DockStyle.Fill
                                pnlInstruction.BringToFront()
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_TEST_PRECAUTION)
                                lblTestDetails_Header.Text = "      Precaution for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)
                                lblTestDetails_Header.ForeColor = Color.FromArgb(31, 73, 125)
                            ElseIf _CurrentColumn = COL_TEST_COMMENTSBTN Then
                                ' ''
                                pnlInstruction.Visible = True
                                pnlInstruction.Dock = DockStyle.Fill
                                pnlInstruction.BringToFront()
                                txtInstruction.Text = _Flex.GetData(_CurrentRow, COL_TEST_COMMENTS)
                                lblTestDetails_Header.Text = "      Comments for " & _Flex.GetData(_CurrentRow, COL_TEST_RESULT_NAME)
                                lblTestDetails_Header.ForeColor = Color.FromArgb(31, 73, 125)
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


            Dim oDataTable As DataTable

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
            End If

            If (_bIsSearch = True) Then

                With _Flex
                    If COL_DgnCPT = COL_TEST_DIGNOSISBTN Then
                        If Not .GetData(.RowSel, COL_TEST_DIGNOSISLIST) Is Nothing Then
                            Dim oDgnColl As gloEMRActors.LabActor.ItemDetails
                            oDgnColl = CType(.GetData(.RowSel, COL_TEST_DIGNOSISLIST), gloEMRActors.LabActor.ItemDetails)
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
                                Dim oCPTColl As gloEMRActors.LabActor.ItemDetails
                                oCPTColl = CType(.GetData(.RowSel, COL_TEST_CPTLIST), gloEMRActors.LabActor.ItemDetails)
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
            If (IsNothing(oDataTable) = False) Then
                oDataTable.Dispose()
                oDataTable = Nothing
            End If
            If (IsNothing(_DB) = False) Then
                _DB.Dispose()
                _DB = Nothing
            End If

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

            Dim _blnResFnd As Boolean = False

            'Check Result Entered or not
            If c1Results.Rows.Count > 0 Then
                For i = 1 To c1Results.Rows.Count - 1
                    If c1Results.GetData(i, COL_TEST_RESULT_NAME) & "" <> "" Then
                        If c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE) & "" <> "" And c1Results.GetData(i, COL_RESULT_TYPE) & "" <> "" Then
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
                                .SetData(nStyleRow, COL_TEST_LOINC, "LOINC Code")
                                .SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                                .SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                                .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                .SetData(nStyleRow, COL_TEST_COMMENTS, "")
                                .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                .SetData(nStyleRow, COL_RESULT_RANGE, "Range")
                                .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, "Value Type")
                                .SetData(nStyleRow, COL_ABNORMAL_FLAG, "Flag")
                                .SetData(nStyleRow, COL_RESULT_TYPE, "Result Type")
                                .SetData(nStyleRow, COL_RESULT_COMMENT, "R. Comment")
                                .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                                .SetData(nStyleRow, COL_TEST_RESULT_USERID, "R. UserID")
                                .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                .SetData(nStyleRow, COL_ISFINISHED, 0)


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
                            If .GetData(nTest, COL_TESTID) & "" = _CurrentTestSelectedID AndAlso .GetData(nTest, COL_RECORDTYPE) = enumRecordType.ResultHeader AndAlso .GetData(nTest, COL_RESULT_NUMBER) & "" = _CurrentTestResultSelectedCounterID Then
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
                            .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, c1Results.GetData(i, COL_TEST_DIGNOSIS_RESULT_VALUE))
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
                            .SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                            .SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                            .SetData(nStyleRow, COL_TEST_COMMENTS, "")
                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                            .SetData(nStyleRow, COL_RESULT_RANGE, c1Results.GetData(i, COL_RESULT_RANGE))
                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, c1Results.GetData(i, COL_RESULT_VALUE_TYPE))
                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, c1Results.GetData(i, COL_ABNORMAL_FLAG))
                            .SetData(nStyleRow, COL_RESULT_TYPE, c1Results.GetData(i, COL_RESULT_TYPE))
                            .SetData(nStyleRow, COL_RESULT_COMMENT, c1Results.GetData(i, COL_RESULT_COMMENT))
                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, c1Results.GetData(i, COL_TEST_RESULT_USERID)) '// Remaining
                            .SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                            .SetData(nStyleRow, COL_ISFINISHED, 0)

                            'Labs Denormalization 20090321
                            .SetData(nStyleRow, COL_TESTNAME, c1Results.GetData(i, COL_TESTNAME))
                            '------
                            '---------------------------------------------------------


                            With .Rows(nStyleRow)
                                .ImageAndText = True
                                .Height = 22
                                If _AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG)) = "N" OrElse IsNothing(_AbnormalFlag_COL.GetCode(c1Results.GetData(i, COL_ABNORMAL_FLAG))) Then
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
                    Next ' For i = 1 To oCategorisedDocuments.Count

                    _Flex.Visible = True
                    pnlDiagosisCPT.Visible = False
                    pnlResults.Visible = False

                End If
            End With



        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _Flex_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellChanged
        If _IsLoading = False Then
            _LabModified = True
        End If

    End Sub

    Private Sub _Flex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            With _Flex
                'Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                ''Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
                '_Flex.Select(_tmpDataRow, True)
                If .Row > 0 Then
                    If .GetData(.Row, COL_RECORDTYPE) = enumRecordType.Result And .Col = COL_TEST_DIGNOSIS_RESULT_VALUE And CStr(_Flex.GetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE) & "").Length > 50 Then
                        _Flex.Visible = False

                        pnlInstruction.Visible = True
                        pnlInstruction.Dock = DockStyle.Fill
                        pnlInstruction.BringToFront()
                        txtInstruction.Text = _Flex.GetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE)
                        lblTestDetails_Header.Text = "Results"
                    End If
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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


            If e.Button = Windows.Forms.MouseButtons.Right Then



                Dim _tmpTestResultID As Int64 = 0
                Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                'Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
                _Flex.Select(_tmpDataRow, True)

                If _tmpDataRow > 0 Then

                    If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test Then
                        'Test - Add Result
                        If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                            '_tmpTestResultID = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID) & "")
                            Dim oContxMenu As New ContextMenu
                            oContxMenu.MenuItems.Clear()

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
                            Dim oMnuItem As MenuItem

                            If _BlockRemove = False Then
                                oMnuItem = New MenuItem
                                oMnuItem.Text = "Remove Test" : oMnuItem.Shortcut = Shortcut.CtrlShiftT : oMnuItem.ShowShortcut = False
                                oContxMenu.MenuItems.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_DeleteTest
                                oMnuItem = Nothing
                            End If

                            oMnuItem = New MenuItem
                            oMnuItem.Text = "Add Result" : oMnuItem.Shortcut = Shortcut.CtrlShiftR : oMnuItem.ShowShortcut = False
                            oContxMenu.MenuItems.Add(oMnuItem)
                            AddHandler oMnuItem.Click, AddressOf Set_Menu_AddResult
                            oMnuItem = Nothing

                            '//Scan Document Functionality - Start //
                            If Val(_Flex.GetData(_tmpDataRow, COL_DMSID)) > 0 Then
                                oMnuItem = New MenuItem
                                oMnuItem.Text = "View Documents" : oMnuItem.Shortcut = Shortcut.CtrlShiftV : oMnuItem.ShowShortcut = False
                                oContxMenu.MenuItems.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_ViewDocument
                                oMnuItem = Nothing
                            Else
                                oMnuItem = New MenuItem
                                oMnuItem.Text = "Scan Documents" : oMnuItem.Shortcut = Shortcut.CtrlShiftS : oMnuItem.ShowShortcut = False
                                oContxMenu.MenuItems.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_ScanDocument
                                oMnuItem = Nothing
                            End If
                            '//Scan Document Functionality - Finish //
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
                            _Flex.ContextMenu = oContxMenu

                            _CurrentRow = _tmpDataRow

                            omnuNode = Nothing
                            _mnuStart = Nothing
                            _mnuFinish = Nothing
                            _mnuRange = Nothing
                            _BlockRemove = Nothing
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
                                Dim oContxMenu As New ContextMenu
                                oContxMenu.MenuItems.Clear()

                                'Menu Object
                                Dim oMnuItem As MenuItem
                                oMnuItem = New MenuItem
                                oMnuItem.Text = "Modify Result" : oMnuItem.Shortcut = Shortcut.CtrlShiftM : oMnuItem.ShowShortcut = False
                                oContxMenu.MenuItems.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_ModifyResult
                                oMnuItem = Nothing


                                'Menu Object
                                oMnuItem = New MenuItem
                                oMnuItem.Text = "Delete Result" : oMnuItem.Shortcut = Shortcut.CtrlShiftD : oMnuItem.ShowShortcut = False
                                oContxMenu.MenuItems.Add(oMnuItem)
                                AddHandler oMnuItem.Click, AddressOf Set_Menu_DeleteResult
                                oMnuItem = Nothing
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
                                _Flex.ContextMenu = oContxMenu
                                _CurrentRow = _tmpDataRow
                            End If
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
            End If
        Catch ex As Exception

        End Try

    End Sub

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

    Private Sub Set_Menu_AddResult(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With _Flex
                If _CurrentRow > 0 Then
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                        _CurrentRow = _CurrentRow

                        DesignResultGrid()

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

                                '//--TEST NODE--//

                                Dim _DB As New gloEMRDatabase.DataBaseLayer
                                Dim _strSQL As String = ""
                                Dim oDataTable As DataTable

                                _strSQL = "SELECT Lab_Test_ResultDtl.labtrd_ResultID, Lab_Test_ResultDtl.labtrd_ResultName, " _
                                & " Lab_Test_ResultDtl.labtrd_ValueType, Lab_Test_ResultDtl.labtrd_Unit, Lab_Test_ResultDtl.labtrd_DefaultValue, " _
                                & " Lab_Test_ResultDtl.labtrd_RefRange, Lab_Test_ResultDtl.labtrd_LOINCId , Lab_Test_Mst.labtm_Name " _
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
                                            .SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                                            .SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                                            .SetData(nStyleRow, COL_TEST_COMMENTS, "")
                                            .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, "")
                                            .SetData(nStyleRow, COL_TEST_COMMENTSBTN, "")
                                            .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, oDataTable.Rows(i).Item("labtrd_ValueType")) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_RANGE, oDataTable.Rows(i).Item("labtrd_RefRange")) '//Result//
                                            .SetData(nStyleRow, COL_ABNORMAL_FLAG, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_TYPE, Nothing) '//Result//
                                            .SetData(nStyleRow, COL_RESULT_COMMENT, "") '//Result//
                                            .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, Date.Now)
                                            .SetData(nStyleRow, COL_TEST_RESULT_USERID, _Flex.GetData(_CurrentRow, COL_TEST_RESULT_USERID))
                                            .SetData(nStyleRow, COL_RECORDTYPE, CType(enumRecordType.Result, Integer))
                                            .SetData(nStyleRow, COL_ISFINISHED, 0)

                                            With .Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                .Style = _Flex.Styles("CS_Record")

                                            End With

                                        Next
                                    End If
                                End If
                                If (IsNothing(_DB) = False) Then
                                    _DB.Dispose()
                                    _DB = Nothing
                                End If
                                If (IsNothing(oDataTable) = False) Then
                                    oDataTable.Dispose()
                                    oDataTable = Nothing
                                End If
                                

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

                            Dim oRange As C1.Win.C1FlexGrid.CellRange
                            oRange = .Rows(_CurrentRow).Node.GetCellRange
                            Dim nTestStart As Int16 = oRange.TopRow
                            Dim nTestEnd As Int16 = oRange.BottomRow
                            .Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1)
                            oRange = Nothing

                        End If
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
                        End If

                    End If
                End If
            End With

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
                            RaiseEvent gUC_ViewDocument(_tmpTestID, Convert.ToInt64(_Flex.GetData(_CurrentRow, COL_DMSID)))
                        End If
                    End If
                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
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
                                    .SetData(nStyleRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.GetData(l, COL_TEST_DIGNOSIS_RESULT_VALUE))
                                    .SetData(nStyleRow, COL_TEST_DIGNOSISBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_CPT_RESULT_UNIT, _Flex.GetData(l, COL_TEST_CPT_RESULT_UNIT))
                                    .SetData(nStyleRow, COL_TEST_CPTBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_NOTE, "")
                                    .SetData(nStyleRow, COL_TEST_SPECIMEN, "")
                                    .SetData(nStyleRow, COL_TEST_STORAGE, "")
                                    .SetData(nStyleRow, COL_TEST_LOINC, _Flex.GetData(l, COL_TEST_LOINC))
                                    .SetData(nStyleRow, COL_TEST_INSTRUCTION, "")
                                    .SetData(nStyleRow, COL_TEST_PRECAUTION, "")
                                    .SetData(nStyleRow, COL_TEST_COMMENTS, "")
                                    .SetData(nStyleRow, COL_TEST_INSTRUCTIONBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_PRECAUTIONBTN, Nothing)
                                    .SetData(nStyleRow, COL_TEST_COMMENTSBTN, Nothing)
                                    .SetData(nStyleRow, COL_RESULT_RANGE, _Flex.GetData(l, COL_RESULT_RANGE)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_VALUE_TYPE, _Flex.GetData(l, COL_RESULT_VALUE_TYPE)) '//Result//
                                    .SetData(nStyleRow, COL_ABNORMAL_FLAG, _Flex.GetData(l, COL_ABNORMAL_FLAG)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_TYPE, _Flex.GetData(l, COL_RESULT_TYPE)) '//Result//
                                    .SetData(nStyleRow, COL_RESULT_COMMENT, _Flex.GetData(l, COL_RESULT_COMMENT)) '//Result//
                                    .SetData(nStyleRow, COL_TEST_RESULT_DATETIME, _Flex.GetData(l, COL_TEST_RESULT_DATETIME))
                                    .SetData(nStyleRow, COL_TEST_RESULT_USERID, _Flex.GetData(l, COL_TEST_RESULT_USERID))
                                    .SetData(nStyleRow, COL_RECORDTYPE, _Flex.GetData(l, COL_RECORDTYPE))
                                    .SetData(nStyleRow, COL_ISFINISHED, 0)

                                    'Labs Denormalization
                                    .SetData(nStyleRow, COL_TESTNAME, _tmpTestName)
                                    '----

                                    With .Rows(nStyleRow)
                                        .ImageAndText = False
                                        .Height = 22
                                        .Style = _Flex.Styles("CS_Record")
                                    End With
                                    'End If
                                Next
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

                            Dim oRange As C1.Win.C1FlexGrid.CellRange
                            oRange = .Rows(_CurrentRow).Node.GetCellRange
                            Dim nTestStart As Int16 = oRange.TopRow
                            Dim nTestEnd As Int16 = oRange.BottomRow
                            .Rows.RemoveRange(nTestStart, (nTestEnd - nTestStart) + 1)
                            oRange = Nothing

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
                    .SetData(.Row, COL_TEST_INSTRUCTION, txtInstruction.Text.Trim)
                ElseIf .Col = COL_TEST_PRECAUTIONBTN Then
                    .SetData(.Row, COL_TEST_PRECAUTION, txtInstruction.Text.Trim)
                ElseIf .Col = COL_TEST_COMMENTSBTN Then
                    .SetData(.Row, COL_TEST_COMMENTS, txtInstruction.Text.Trim)
                End If

                If .GetData(.Row, COL_RECORDTYPE) = enumRecordType.Result And .Col = COL_TEST_DIGNOSIS_RESULT_VALUE Then
                    .SetData(.Row, COL_TEST_DIGNOSIS_RESULT_VALUE, txtInstruction.Text)
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
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
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
            RaiseEvent OnSubWindow_Opened(sender, e)
        Else
            RaiseEvent OnSubWindow_Closed(sender, e)
        End If
    End Sub

    Private Sub pnlResults_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlResults.VisibleChanged
        If pnlResults.Visible Then
            RaiseEvent OnSubWindow_Opened(sender, e)
        Else
            RaiseEvent OnSubWindow_Closed(sender, e)
        End If
    End Sub
    '' END SUDHIR ''

    ''Sandip Darade 20090929
    ''fill CPTs and Dignosis in the respective combolists pulling from table ExamICD9CPT  
    Private Sub Fill_Diagnosis_CPT()
        Try
            Dim _DB As New gloEMRDatabase.DataBaseLayer
            Dim _strSQL As String = ""
            strDia = ""
            strCPT = ""

            _strSQL = " SELECT  DISTINCT ISNull(sICD9Code,'') as sICD9Code, isnull(sICD9Description,'') as sICD9Description " _
                       & " FROM ExamICD9CPT  WHERE   nPatientID = " & nPatientID & "  "
            Dim dt As DataTable
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
            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    If (Convert.ToString(dt.Rows(i)("sICD9Code")) <> "") Then

                        ''                          ICD9Code                        ICD9Description
                        strDia = strDia & "|" & dt.Rows(i)("sICD9Code") & "-" & dt.Rows(i)("sICD9Description")
                        _Dignos = New gloEMRActors.LabActor.ItemDetail
                        With _Dignos
                            .Code = dt.Rows(i)("sICD9Code")
                            .Description = dt.Rows(i)("sICD9Description")
                        End With
                        _Dignosis.Add(_Dignos)
                        _Dignos = Nothing
                    End If
                Next
                dt.Dispose()
            End If

            ''CPT
            _strSQL = " SELECT  DISTINCT ISNull(sCPTCode,'') as sCPTCode, isnull(sCPTDescription,'') as sCPTDescription  " _
                       & " FROM ExamICD9CPT  WHERE   nPatientID = " & nPatientID & "  "
            dt = Nothing
            '' Fill CPTs Of the Patient 
            dt = _DB.GetDataTable_Query(_strSQL)
            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    If (Convert.ToString(dt.Rows(i)("sCPTCode")) <> "") Then
                        ''                          CPTCode                        CPTDescription
                        strCPT = strCPT & "|" & dt.Rows(i)("sCPTCode") & "-" & dt.Rows(i)("sCPTDescription")
                        _Dignos1 = New gloEMRActors.LabActor.ItemDetail
                        With _Dignos1
                            .Code = dt.Rows(i)("sCPTCode")
                            .Description = dt.Rows(i)("sCPTDescription")
                        End With
                        _Dignosis1.Add(_Dignos1)
                        _Dignos1 = Nothing

                    End If
                Next
                dt.Dispose()
                dt = Nothing
            End If
            Dim j As Int16
            ''Fill combolist for Diagnosis and CPT 
            For j = 1 To _Flex.Rows.Count - 1
                _DgnCPTRunTimeComboList = _DgnCPTRunTimeComboList + 1
                Dim csDignosisList As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("CS_Dignosis" & _DgnCPTRunTimeComboList)
                Try
                    If (_Flex.Styles.Contains("CS_Dignosis" & _DgnCPTRunTimeComboList)) Then
                        csDignosisList = _Flex.Styles("CS_Dignosis" & _DgnCPTRunTimeComboList)
                    Else
                        csDignosisList = _Flex.Styles.Add("CS_Dignosis" & _DgnCPTRunTimeComboList)
                        With csDignosisList
                            .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                            .ForeColor = Color.Black
                            .BackColor = Color.GhostWhite
                            .DataType = Type.GetType("System.String")
                            .ComboList = strDia
                        End With
                    End If
                Catch ex As Exception
                    csDignosisList = _Flex.Styles.Add("CS_Dignosis" & _DgnCPTRunTimeComboList)
                    With csDignosisList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        .DataType = Type.GetType("System.String")
                        .ComboList = strDia
                    End With

                End Try
              
                ''_Flex.SetCellStyle(_CurrentRow, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_Dignosis" & _DgnCPTRunTimeComboList))
                '' _Flex.SetData(_CurrentRow, COL_TEST_DIGNOSISLIST, _Dignosis)
                _Flex.SetCellStyle(j, COL_TEST_DIGNOSIS_RESULT_VALUE, _Flex.Styles("CS_Dignosis" & _DgnCPTRunTimeComboList))
                _Flex.SetData(j, COL_TEST_DIGNOSISLIST, _Dignosis)

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
              
                '' _Flex.SetCellStyle(_CurrentRow, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_CPT" & _DgnCPTRunTimeComboList))
                '' _Flex.SetData(_CurrentRow, COL_TEST_CPTLIST, _Dignosis1)
                _Flex.SetCellStyle(j, COL_TEST_CPT_RESULT_UNIT, _Flex.Styles("CS_CPT" & _DgnCPTRunTimeComboList))
                _Flex.SetData(j, COL_TEST_CPTLIST, _Dignosis1)
            Next
            If (IsNothing(_DB) = False) Then
                _DB.Dispose()
                _DB = Nothing
            End If
           
            '_Flex.Cols(COL_TEST_DIGNOSIS_RESULT_VALUE).AllowEditing = False
            '_Flex.Cols(COL_TEST_CPT_RESULT_UNIT).AllowEditing = False

        Catch ex As Exception

        End Try
    End Sub


End Class
