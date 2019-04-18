Imports gloEMRGeneralLibrary
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.Globalization
Imports System.Linq

''


Public Class gloUC_TransactionHistory

    Public Event btnCloseRefillClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnShowGraphClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event On_Flex_DoubleClick(ByVal PatientID As Int64, ByVal OrderID As Int64, ByVal VisitID As Int64, ByVal TransactionDate As DateTime)

   

    Public Delegate Sub GenerateCDAHandler(OrderId As Int64, RowID As Integer)
    Public Event EvntGenerateCDAHandler As GenerateCDAHandler
    Public Delegate Sub ViewOrderHandler(menuitem As String, OrderId As Int64)
    Public Event EvntViewOrderHandler As ViewOrderHandler  ''added to open order from dashboard on right clicking order result grid
    ''Added on 20100119-Raise event after changing criteria
    Public Event On_SearchCriteria_Changed()
    Dim strDia As String = ""
    Private _DgnCPTRunTimeComboList As Int16 = 0
    Private _AbnormalFlag_COL As gloEMRActors.LabActor.ItemDetails
    Private _AbnormalFlagList As String = " "
    Private _ResultTypeList As String = " "
    Private _ObservationStatus_COL As gloEMRActors.LabActor.ItemDetails

    Private _TransactionType As enumTransactionType
    Private _cmbTestNameList As Int16 = 0

    Private Const COL_SELECT = 0
    Private Const COL_DATE = 1
    'Mitesh
    Private Const COL_REPORTED_DATE = 2
    '---
    Private Const COL_ORDERID = 3
    Private Const COL_TESTID = 4
    Private Const COL_RESULTNUMBER = 5
    Private Const COL_RESULTLINENO = 6
    Private Const COL_RESULTNAMEID = 7
    Private Const COL_NAME = 8
    Private Const COL_Lab_InfoButton = 9
    Private Const COL_VALUE = 10
    Private Const COL_UNIT = 11
    Private Const COL_RANGE = 12
    Private Const COL_PATIENT_RANGE = 13
    Private Const COL_FLAG = 14
    Private Const COL_RESULTTYPE = 15
    Private Const COL_ISFINISHED = 16
    Private Const COL_RECORDTYPE = 17
    Private Const COL_VISITID = 18
    Private Const COL_ReceiveDate = 19 '20090217
    Private Const COL_ResultLoincCode = 20
    Private Const COL_TESTCPT = 21
    Private Const COL_TESTNAME = 22

    ''Added by Abhijeet on 20100917 for CCHIT certification requirement    
    Private Const COL_TESTSTATUS = 23
    Private Const COL_SPECIMENSOURCE = 24
    Private Const COL_TESTTYPE = 25
    Private Const COL_SPECIMENCONDITIONDISP = 26
    Private Const COL_LABFACILITYNAME = 27
    Private Const COL_LABFACILITYSTREETADDRESS = 28
    Private Const COL_LABFACILTYCITY = 29
    Private Const COL_LABFACILITYSTATE = 30
    Private Const COL_LABFACILITYZIPCODE = 31
    Private Const COL_RESULT_COMMENT = 32

    Private Const COL_SourceCombo = 33

    ''End of changes Added by Abhijeet on 20100917 for CCHIT certification requirement

    'sarika Labs Denormalization 20090321
    'Private Const COL_COUNT = 18

    ''by Abhijeet on 20100917 .change the total column count to 30
    '' Private Const COL_COUNT = 19
    Private Const COL_COUNT = 34
    ''End of changes by Abhijeet on 20100917 for total column count change

    '---
    Public _OrderID As Int64 = 0

    Private _CurrentRow As Int16 = 1
    Private _CurrentColumn As Int16 = 1
    Private _CurrentOrderID As Long
    Private _CurrentVisitID As Long
    Private _PatientID As Long = 0
    Private _ToDate As Date

    Private _blnFromSplitScreen As Boolean


    Private _nLabTestId As Int64
    Private IsFilterForDate As Boolean = False
    Private IsFilterForTest As Boolean = False
    Private IsFilterForResult As Boolean = False
    Private strSearchString As String = ""
    Public ShowReceivedate As Boolean = False
    'Sanjog
    Private _ClearCombo As Boolean = False

    'Sanjog
    Dim _isTestsLoaded As Boolean = False
    Dim nrowcount As Integer = 35

    Dim _isscroll As Boolean = False
    Dim dsresults As New DataSet
    Dim dsTests As New DataSet

    Public Event gUC_ViewDocument(ByVal TestID As Int64, ByVal DocumentID As Int64)
    Public Event OpenCDA(ByVal OrderID As Int64)
    Dim _nUserId As Int64 = 0
    Dim clsinfobutton_Lab As New gloEMRGeneralLibrary.clsInfobutton
    Dim isEducationMaterialEnables As Boolean = False
    Dim isAdvancedReference As Boolean = False
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Public Event gUC_InfoButtonDocumentClickedDB(ByVal tCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String)
    Public Event gUC_InfoButtonClickedDB(ByVal lcode As String)
    Public IsLabDB As Boolean = False

    Private _MergeOrderID As Int64 = 0
    Private _curOrderID As Int64 = 0

    Dim dtSourceTestName As DataTable
    Dim dtDestinationTestName As DataTable
    Dim isValid As Integer = 0
    Public Sub ClearGlobalVariables()
        If (IsNothing(dsresults) = False) Then
            dsresults.Dispose()
            dsresults = Nothing

        End If
        If (IsNothing(dsTests) = False) Then
            dsTests.Dispose()
            dsTests = Nothing

        End If
        If (IsNothing(clsinfobutton_Lab) = False) Then
            'clsinfobutton_Lab.Dispose()
            clsinfobutton_Lab = Nothing

        End If

        If (IsNothing(dtSourceTestName) = False) Then
            dtSourceTestName.Dispose()
            dtSourceTestName = Nothing

        End If
        If (IsNothing(dtDestinationTestName) = False) Then
            dtDestinationTestName.Dispose()
            dtDestinationTestName = Nothing

        End If
        Try
            If (IsNothing(_AbnormalFlag_COL) = False) Then
                _AbnormalFlag_COL.Dispose()
                _AbnormalFlag_COL = Nothing
            End If
        Catch ex As Exception

        End Try

        Try
            If (IsNothing(_ObservationStatus_COL) = False) Then
                _ObservationStatus_COL.Dispose()
                _ObservationStatus_COL = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Property MergeOrderID() As Int64
        Get
            Return _MergeOrderID
        End Get
        Set(ByVal value As Int64)
            _MergeOrderID = value
        End Set
    End Property

    Public Property CurOrderID() As Int64
        Get
            Return _curOrderID
        End Get
        Set(ByVal value As Int64)
            _curOrderID = value
        End Set
    End Property

    Private _forMerge As Boolean = False
    Public Property ForMerging() As Boolean
        Get
            Return _forMerge
        End Get
        Set(ByVal value As Boolean)
            _forMerge = value
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property
    Public Sub DesignTestGrid(Optional ByVal IsResults As Boolean = False)
        SetGridStyle(_Flex)
        With _Flex
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            Dim _TotalWidth As Single = .Width - 5
            '' .Cols(COL_DATE).DataType = GetType(Date)
            .SetData(0, COL_SELECT, "Select") 'col added by sagaarK forglo UC_TransactionHistory.vb user control
            '.SetData(0, COL_DATE, "Date") 'col added by sagaarK forglo UC_TransactionHistory.vb user control
            .SetData(0, COL_DATE, "Collected")
            .SetData(0, COL_REPORTED_DATE, "Reported")
            .SetData(0, COL_ORDERID, "Order ID")
            .SetData(0, COL_TESTID, "Test ID")
            .SetData(0, COL_RESULTNUMBER, "Result No")
            .SetData(0, COL_RESULTLINENO, "Result Line No")
            .SetData(0, COL_RESULTNAMEID, "Result ID")
            .SetData(0, COL_NAME, "Name")
            .SetData(0, COL_VALUE, "Value")
            .SetData(0, COL_UNIT, "Unit")
            .SetData(0, COL_RANGE, "Range")
            .SetData(0, COL_PATIENT_RANGE, "Patient Specific Range")
            .SetData(0, COL_FLAG, "Flag")
            .SetData(0, COL_RESULTTYPE, "Result Type")
            .SetData(0, COL_ISFINISHED, "Is Finished")
            .SetData(0, COL_RECORDTYPE, "Record Type")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_ReceiveDate, "Receive Date")
            .SetData(0, COL_ResultLoincCode, "LOINC Code")
            .SetData(0, COL_TESTCPT, "CPT")
            .SetData(0, COL_TESTNAME, "Test Name")

            ''Added by Abhijeet on 20101026
            .SetData(0, COL_TESTSTATUS, "Status")
            .SetData(0, COL_TESTTYPE, "Test Type")
            .SetData(0, COL_SPECIMENSOURCE, "Specimen Source")
            .SetData(0, COL_SPECIMENCONDITIONDISP, "Specimen Condition")
            .SetData(0, COL_LABFACILITYNAME, "Facilty Name")
            .SetData(0, COL_LABFACILITYSTREETADDRESS, "Facilty Address")
            .SetData(0, COL_LABFACILTYCITY, "Facilty City")
            .SetData(0, COL_LABFACILITYSTATE, "Facilty State")
            .SetData(0, COL_LABFACILITYZIPCODE, "Facilty Zip Code")
            .SetData(0, COL_RESULT_COMMENT, "Comment")
            .SetData(0, COL_Lab_InfoButton, "")
            ''End of changes Added by Abhijeet on 20101026

            .Rows(0).Height = 22

            .Tree.Column = COL_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15
            '.AllowEditing = False


            '.Cols(COL_ORDERID).Width = _TotalWidth * 0.5 ' 100 'col hide by sagaarK forglo UC_TransactionHistory.vb user control
            '.Cols(COL_SELECT).Width = _TotalWidth * 0.5 '100 'col added by sagaarK forglo UC_TransactionHistory.vb user control
            '.Cols(COL_NAME).Width = _TotalWidth * 0.6 '150
            '.Cols(COL_VALUE).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_UNIT).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_RANGE).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_FLAG).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_RESULTTYPE).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_ISFINISHED).Width = _TotalWidth * 0.5 '100
            '.Cols(COL_RECORDTYPE).Width = _TotalWidth * 0.5 ' 100

            ''Infobutton
            .Cols(COL_Lab_InfoButton).AllowResizing = False
            .SetData(0, COL_Lab_InfoButton, "")
            .Cols(COL_Lab_InfoButton).Width = 22
            If isEducationMaterialEnables And IsLabDB = True Then
                .Cols(COL_Lab_InfoButton).Visible = True
            Else
                .Cols(COL_Lab_InfoButton).Visible = False
            End If

            .Cols(COL_Lab_InfoButton).AllowEditing = False


            .Cols(COL_PATIENT_RANGE).Width = _TotalWidth * 0.13
            .Cols(COL_PATIENT_RANGE).AllowEditing = False
            .Cols(COL_SELECT).Width = _TotalWidth * 0.05 ' 50

            If _blnFromSplitScreen Then
                .Cols(COL_SELECT).AllowEditing = False
            Else
                .Cols(COL_SELECT).AllowEditing = True
            End If


            .Cols(COL_DATE).Width = _TotalWidth * 0.15
            .Cols(COL_DATE).AllowEditing = False

            .Cols(COL_REPORTED_DATE).Width = _TotalWidth * 0.15
            .Cols(COL_REPORTED_DATE).AllowEditing = False

            .Cols(COL_ORDERID).Width = 0
            .Cols(COL_TESTID).Width = 0
            .Cols(COL_RESULTNUMBER).Width = 0
            .Cols(COL_RESULTLINENO).Width = 0
            .Cols(COL_RESULTNAMEID).Width = 0
            .Cols(COL_NAME).Width = _TotalWidth * 0.23 ' 200
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_VALUE).Width = _TotalWidth * 0.09 '100
            .Cols(COL_VALUE).AllowEditing = False
            .Cols(COL_UNIT).Width = _TotalWidth * 0.06 '100
            .Cols(COL_UNIT).AllowEditing = False
            .Cols(COL_RANGE).Width = _TotalWidth * 0.15 '100
            .Cols(COL_RANGE).AllowEditing = False
            .Cols(COL_FLAG).Width = _TotalWidth * 0.16 '100
            .Cols(COL_FLAG).AllowEditing = False
            .Cols(COL_RESULTTYPE).Width = _TotalWidth * 0.1
            .Cols(COL_RESULTTYPE).AllowEditing = False
            .Cols(COL_ISFINISHED).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_VISITID).Width = 0
            .Cols(COL_ReceiveDate).Width = _TotalWidth * 0.15 '20090217
            .Cols(COL_ReceiveDate).AllowEditing = False
            .Cols(COL_ResultLoincCode).Width = _TotalWidth * 0.08 '20090217
            .Cols(COL_ResultLoincCode).AllowEditing = False
            If IsResults Then
                .Cols(COL_TESTCPT).Width = 0
            Else
                .Cols(COL_TESTCPT).Width = _TotalWidth * 0.08 '20090217
            End If

            .Cols(COL_TESTCPT).AllowEditing = False
            .Cols(COL_TESTNAME).AllowEditing = False
            .Cols(COL_TESTNAME).Width = 0

            .Cols(COL_SELECT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            ''Added by Abhijeet on 20101026
            .Cols(COL_TESTSTATUS).Width = 120
            .Cols(COL_TESTTYPE).Width = 120
            .Cols(COL_SPECIMENSOURCE).Width = 150
            .Cols(COL_SPECIMENCONDITIONDISP).Width = 0
            .Cols(COL_LABFACILITYNAME).Width = 0
            .Cols(COL_LABFACILITYSTREETADDRESS).Width = 0
            .Cols(COL_LABFACILTYCITY).Width = 0
            .Cols(COL_LABFACILITYSTATE).Width = 0
            .Cols(COL_LABFACILITYZIPCODE).Width = 0
            .Cols(COL_RESULT_COMMENT).Width = 0

            .Cols(COL_RESULT_COMMENT).AllowEditing = False
            .Cols(COL_TESTSTATUS).AllowEditing = False
            .Cols(COL_TESTTYPE).AllowEditing = False
            .Cols(COL_SPECIMENSOURCE).AllowEditing = False
            .Cols(COL_SPECIMENCONDITIONDISP).AllowEditing = False
            .Cols(COL_LABFACILITYNAME).AllowEditing = False
            .Cols(COL_LABFACILITYSTREETADDRESS).AllowEditing = False
            .Cols(COL_LABFACILTYCITY).AllowEditing = False
            .Cols(COL_LABFACILITYSTATE).AllowEditing = False
            .Cols(COL_LABFACILITYZIPCODE).AllowEditing = False
            .Cols(COL_TESTTYPE).DataType = GetType(String)
            ''End of changes Added by Abhijeet on 20101026

            If ShowReceivedate = True Then
                .Cols(COL_ReceiveDate).Visible = True
            Else
                .Cols(COL_ReceiveDate).Visible = False
            End If

            'Resolved bug:47736
            If cmbCriteria.Text <> "Result" Then
                .Cols(COL_SELECT).Visible = False
            Else
                .Cols(COL_SELECT).Visible = True
            End If

            .Cols(COL_SourceCombo).Width = 0

        End With
        _Flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw 'added by manoj on 201228 for triggering OwnerDraw Event
    End Sub

    Public Sub DesignTestGridSource(ByVal _ordID As Int64, Optional ByVal IsResults As Boolean = False)
        SetGridStyle(_Flex)
        With _Flex
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            Dim _TotalWidth As Single = .Width - 5

            .SetData(0, COL_ORDERID, "Order ID")
            .SetData(0, COL_TESTID, "Test ID")
            .SetData(0, COL_RESULTNUMBER, "Result No")
            .SetData(0, COL_RESULTLINENO, "Result Line No")
            .SetData(0, COL_RESULTNAMEID, "Result ID")
            .SetData(0, COL_NAME, "Name")


            .Rows(0).Height = 22

            .Tree.Column = COL_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15



            If _blnFromSplitScreen Then
                .Cols(COL_SELECT).AllowEditing = False
            Else
                .Cols(COL_SELECT).AllowEditing = True
            End If

            .Cols(COL_SELECT).Width = 0
            .Cols(COL_DATE).Width = 0
            .Cols(COL_REPORTED_DATE).Width = 0

            .Cols(COL_ORDERID).Width = 0
            .Cols(COL_PATIENT_RANGE).Width = 0
            .Cols(COL_Lab_InfoButton).Width = 0
            .Cols(COL_TESTID).Width = 0
            .Cols(COL_RESULTNUMBER).Width = 0
            .Cols(COL_RESULTLINENO).Width = 0
            .Cols(COL_RESULTNAMEID).Width = 0
            .Cols(COL_NAME).Width = _TotalWidth * 0.5
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_VALUE).Width = 0
            .Cols(COL_VALUE).AllowEditing = False
            .Cols(COL_UNIT).Width = 0

            .Cols(COL_RANGE).Width = 0

            .Cols(COL_FLAG).Width = 0

            .Cols(COL_RESULTTYPE).Width = 0

            .Cols(COL_ISFINISHED).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_VISITID).Width = 0
            .Cols(COL_ReceiveDate).Width = 0

            .Cols(COL_ResultLoincCode).Width = 0


            .Cols(COL_TESTCPT).Width = 0

            .Cols(COL_TESTNAME).Width = 0



            ''Added by Abhijeet on 20101026
            .Cols(COL_TESTSTATUS).Width = 0
            .Cols(COL_TESTTYPE).Width = 0
            .Cols(COL_SPECIMENSOURCE).Width = 0
            .Cols(COL_SPECIMENCONDITIONDISP).Width = 0
            .Cols(COL_LABFACILITYNAME).Width = 0
            .Cols(COL_LABFACILITYSTREETADDRESS).Width = 0
            .Cols(COL_LABFACILTYCITY).Width = 0
            .Cols(COL_LABFACILITYSTATE).Width = 0
            .Cols(COL_LABFACILITYZIPCODE).Width = 0
            .Cols(COL_RESULT_COMMENT).Width = 0

            If (_ordID > 0) Then

                .SetData(0, COL_SourceCombo, "Merge Into")
                .Cols(COL_SourceCombo).Width = 300
                .Cols(COL_SourceCombo).AllowEditing = True
                .Cols(COL_SourceCombo).DataType = GetType(Object)
            Else
                .Cols(COL_SourceCombo).Width = 0
            End If



            'Resolved bug:47736
            If cmbCriteria.Text <> "Result" Then
                .Cols(COL_SELECT).Visible = False
            Else
                .Cols(COL_SELECT).Visible = True
            End If

            .ExtendLastCol = False

        End With
        _Flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw 'added by manoj on 201228 for triggering OwnerDraw Event
    End Sub

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
    ''' <summary>
    ''' property added for Hide/Show 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Added by dipak 20100324</remarks>
    Public Property HideCloseButton()
        Get
            Return btnCloseRefill.Visible
        End Get
        Set(ByVal value)
            btnCloseRefill.Visible = value
        End Set
    End Property

    Private Function GetTestName(ByVal TestID As Int64) As String
        Dim _Result As String = ""
        Dim _gloEMRDBID As New gloEMRDatabase.DataBaseLayer
        _Result = _gloEMRDBID.GetRecord_Query("SELECT labtm_Name FROM Lab_Test_Mst WHERE labtm_ID = " & TestID & " ").Trim
        'SLR: Free and then
        If Not IsNothing(_gloEMRDBID) Then
            _gloEMRDBID.Dispose()
        End If
        _gloEMRDBID = Nothing
        Return _Result
    End Function

    Private Function GetTestCode(ByVal TestID As Int64) As String
        Dim _Result As String = ""
        Dim _gloEMRDBID As New gloEMRDatabase.DataBaseLayer
        _Result = _gloEMRDBID.GetRecord_Query("SELECT labtm_Code FROM Lab_Test_Mst WHERE labtm_ID = " & TestID & " ").Trim
        'SLR: Free and then
        If Not IsNothing(_gloEMRDBID) Then
            _gloEMRDBID.Dispose()
        End If
        _gloEMRDBID = Nothing
        Return _Result
    End Function

    Private Function GetOrderDetail(ByVal OrderID As Int64) As String
        Dim _Result As String = ""
        Dim _gloEMRDBID As New gloEMRDatabase.DataBaseLayer
        _Result = _gloEMRDBID.GetRecord_Query("select  CONVERT(CHAR(11), labom_OrderDate, 101)  + LTRIM(RIGHT(CONVERT(CHAR(20), labom_OrderDate, 22), 11))+  ' - '+ labom_OrderNoPrefix + ' '+ Convert(varchar,labom_OrderNoId) from Lab_Order_MST WHERE labOM_OrderID = " & OrderID & " ").Trim
        'SLR: FRee and then
        If Not IsNothing(_gloEMRDBID) Then
            _gloEMRDBID.Dispose()
        End If
        _gloEMRDBID = Nothing
        Return _Result
    End Function


    Public Sub LoadPreviousLabs(ByVal PatientID As Long, ByVal ToDate As Date, Optional ByVal blnFromSplitScreen As Boolean = False)
        _PatientID = PatientID
        _ToDate = ToDate
        _blnFromSplitScreen = blnFromSplitScreen
    End Sub

#Region "Sort by date"

    'Private Function GetOrdersByDate(ByVal PatientId As Long, ByVal Todate As Date, ByVal nrowcount As Integer) As DataTable
    '    Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim dt As DataTable
    '    Try

    '        'first get all the orders aginst that orderID
    '        '//// <> Order Master 
    '        dt = New DataTable
    '        With odb
    '            Dim _strSql As String = "SELECT DISTINCT  labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate , ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID FROM Lab_Order_MST " _
    '           & " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
    '           & " WHERE(labom_PatientID = " & PatientId & ")ORDER By labom_TransactionDate desc, labom_OrderNoID"

    '            ' ''Fill Order Master 
    '            dt = .GetDataTable_Query(_strSql)

    '        End With

    '        Return dt
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    End Try


    'End Function
    Private Function GetOrdersByDate(ByVal PatientId As Long, ByVal FromDate As Date, ByVal Todate As Date) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try

            'first get all the orders aginst that orderID
            '//// <> Order Master 
            '  dt = New DataTable ''Slr new not needed 
            With odb



                Dim _strSql As String = "SELECT DISTINCT labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate , ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID FROM select * from Lab_Order_MST " _
               & " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
               & " WHERE(labom_PatientID = " & PatientId & " and convert(datetime,convert(varchar,labom_TransactionDate,101)) between '" & FromDate & "' and '" & Todate & "')ORDER By labom_TransactionDate desc, labom_OrderNoID"

                ' ''Fill Order Master 
                dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try


    End Function

    Private Function GetOrderByDate(ByVal OrderID As Int64) As DataTable


        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try

            'first get all the orders aginst that orderID
            '//// <> Order Master 
            '  dt = New DataTable  ''Slr new not needed 
            With odb
                Dim oPara As New gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                ' ''Fill Order Master 
                dt = .GetDataTable("Lab_GetOrderMaster")

                oPara = Nothing
            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    Private Function GetTestsByDate(ByVal OrderID As Int64) As DataTable
        Dim dt As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '//// <> Fill Order Test Details Object 
            ' dt = New DataTable  'SLR: new is not needed
            With odb
                Dim oPara As New gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                dt = .GetDataTable("Lab_GetOrderTestDtl")
                ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
                ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , 
                '' nUserID, sLoginName, UserName, labotd_Comment
                oPara = Nothing
            End With
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Don't do, instead free odb
            '  dt = Nothing
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing

        End Try
    End Function

    Private Function GetResultsByDate(ByVal OrderID As Int64, ByVal OrdTestName As String) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 
            Dim dtResult As DataTable  ''SLR new not needed 

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrdTestName 'oOrderTest.TestID
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ''Sanjog
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorResult"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                'sanjog

                dtResult = .GetDataTable("Lab_GetOrderTestResult")
                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                'oPara = Nothing
            End With
            Return dtResult
            ' dtResult = Nothing  'SLR: DOn't do 
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FInaly free oDB
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing

        End Try
    End Function

    Private Function GetResultsDetailsByDate(ByVal OrderID As Int64, ByVal OrdTestName As String, ByVal TestResultNumber As Int64) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer

        Try

            '' //// <> Fill Order Test Result Details Object 
            Dim dtResultDTL As DataTable  ''Slr no need for new 
            'Retrive from db
            ' Dim oTestResult As New gloEMRActors.LabActor.OrderTestResult  Slr not used 
            With odb


                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrdTestName 'oOrderTest.TestID
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestResultNumber ' oTestResult.TestResultNumber
                oPara.Name = "@TestResultNumber"
                .DBParametersCol.Add(oPara)

                dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails")
                'labotrd_TestResultNumber, labotrd_ResultLineNo ,labotrd_ResultNameID , labotrd_ResultName,labotrd_ResultValue, labotrd_ResultUnit , labotrd_ResultRange,labotrd_ResultType ,
                'labotrd_ResultComment, labotrd_ResultWord, labotrd_ResultDMSID, labotrd_ResultUserID, labotrd_ResultDateTime()
                oPara = Nothing
            End With

            Return dtResultDTL

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free oDB
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    'copied original setdata  function
    'Public Function SetDataByDate(ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal nrowcount As Integer = 10) As Boolean
    '    'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Load, " Before setsortbydate_new Load", gloAuditTrail.ActivityOutCome.Success)
    '    '' SetDataByDate_New()
    '    'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ModifyLabs, gloAuditTrail.ActivityType.Load, " After setsortbydate_new Load", gloAuditTrail.ActivityOutCome.Success)


    '    'Sanjog- added on 2011 march 1 to handle the nothing
    '    If IsNothing(_ObservationStatus_COL) Then
    '        Fill_ResultType()
    '    End If
    '    If IsNothing(_AbnormalFlag_COL) Then
    '        Fill_AbnormalFlag()
    '    End If
    '    'Sanjog- added on 2011 march 1 to handle the nothing

    '    If _PatientID = 0 Then Exit Function

    '    Dim _FillTestName As String = ""
    '    Dim _FillTestCode As String = ""
    '    Dim _FillTestNodeIndex As Int16 = 0
    '    Dim _FillTestResultsNodeIndex As Int16 = 0
    '    Dim _FillTestLineNo As Int16 = 0

    '    Dim _Orders As New DataTable() 'multiple orders collection 
    '    Dim _Order As New DataTable() 'single order collection
    '    Dim _Tests As New DataTable() 'multiple tests collection
    '    Dim _Test As New DataTable() 'single test collection
    '    Dim _Results As New DataTable() 'multiple results collection
    '    Dim _ResultDetails As New DataTable() 'single result collection
    '    Dim dsResults As New DataSet
    '    Try

    '        If IsFilterForDate Then
    '            _Orders = GetOrdersByDate(_PatientID, FromDate, ToDate)
    '        Else
    '            _Orders = GetOrdersByDate(_PatientID, DateTime.Now.Date, DateTime.Now.Date)
    '        End If
    '        ''''

    '        ''''


    '        For nOrders As Int16 = 0 To _Orders.Rows.Count - 1

    '            'remove the current order from the Orders table
    '            _CurrentOrderID = _Orders.Rows(nOrders)("labom_OrderID")

    '            '' VisitID for Current Order
    '            _CurrentVisitID = _Orders.Rows(nOrders)("labom_VisitID")

    '            'get the current order against that orderId
    '            ''   _Order = GetOrderByDate(_CurrentOrderID)

    '            'remove all the Tests against that order

    '            ''
    '            _isTestsLoaded = False
    '            If _isscroll = False Then


    '               _Tests = GetTestsByDate(_CurrentOrderID)
    '                dsResults = GetResults(_PatientID, FromDate, ToDate)
    '                ' dvResults = dsResults.Tables(0).DefaultView
    '                ''''
    '            Else

    '                ' _Flex.Clear()
    '            End If
    '            DesignTestGrid()
    '            'If IsNothing(dvResults) = False Then
    '            '    If nrowcount > dvResults.Table.Rows.Count Then
    '            '        _isTestsLoaded = True
    '            '        nrowcount = dvResults.Table.Rows.Count
    '            '    End If
    '            '    If nrowcount < dvResults.Table.Rows.Count Then
    '            '        nrowcount = 10
    '            '    End If
    '            'End If

    '            ''
    '            ''''
    '            'loop if there are any multiple Tests against that order and set the data to the flex grid
    '            For nTests As Int16 = 0 To dvResults.Table.Rows.Count - 1
    '                ' _FillTestName = GetTestName(_Tests.Rows(nTests)("labotd_TestID"))

    '                _FillTestName = _Tests.Rows(nTests)("labotd_TestName")
    '                '' _FillTestCode = GetTestCode(_Tests.Rows(nTests)("labotd_TestID"))

    '                'If IsTestExists(.Item(nTest).OrderID, .Item(nTest).TestID, _FillTestName) = False Then
    '                'If IsTestExists(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestID"), _FillTestName) = False Then
    '                With _Tests '.Item(nTest)
    '                    '//---Add Test---Start---//
    '                    _Flex.Rows.Add()

    '                    _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail

    '                    _FillTestLineNo = .Rows(nTests)("labotd_LineNo") ' .TestLineNo

    '                    With _Flex.Rows(_Flex.Rows.Count - 1)
    '                        .ImageAndText = True
    '                        .Height = 22
    '                        .IsNode = True
    '                        .Style = _Flex.Styles("CS_Record")
    '                        .Node.Level = 0
    '                        .Node.Image = ImgTest.Image
    '                        .Node.Data = _FillTestName
    '                    End With

    '                    _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SELECT, _Flex.Styles("CS_CheckBox"))
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_SELECT, False)
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nTests)("labotd_TestID")) '.TestID
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_VISITID, _CurrentVisitID) 'VisitID
    '                    'labs denormalization
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTNAME, .Rows(nTests)("labotd_TestName")) 'VisitID

    '                    ''Added by Abhijeet on 20101026
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, .Rows(nTests)("labotd_TestStatus"))
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, .Rows(nTests)("labotd_SpecimenSource"))
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENCONDITIONDISP, .Rows(nTests)("labotd_SpecimenConditionDisp"))
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, .Rows(nTests)("labotd_TestType"))
    '                    ''End of changes by Abhijeet on 20101026
    '                    _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, .Rows(nTests)("labotd_LOINCCode"))
    '                    '---
    '                    '_Flex.SetData(_Flex.Rows.Count - 1, COL_TEST, _CurrentVisitID) 'VisitID
    '                    '//---Add Test---Finish---//

    '                    '// If _TransactionType = enumTransactionType.LabResult Then

    '                    Dim nStyleRow As Int16 = 0
    '                    Dim nNodeRow As Int16 = 0
    '                    Dim i As Int16 = 0

    '                    'get the results against that orderid and that testID
    '                    _Results = GetResultsByDate(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestName"))

    '                    For nResult As Int16 = 0 To _Results.Rows.Count - 1
    '                        With _Results
    '                            '//---Add Test Result Header---Start---//
    '                            _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(nResult)("labotr_TestResultName"), _Tests.Rows(nTests)("labotd_TestID") & .Rows(nResult)("labotr_TestResultNumber"), ImgResultHeader.Image)
    '                            nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                            _FillTestResultsNodeIndex = nStyleRow
    '                            '---------------------------------------------------------
    '                            _Flex.SetData(nStyleRow, COL_SELECT, "")
    '                            _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
    '                            _Flex.SetData(nStyleRow, COL_TESTID, _Tests.Rows(nTests)("labotd_TestID"))
    '                            _Flex.SetData(nStyleRow, COL_NAME, .Rows(nResult)("labotr_TestResultName"))
    '                            _Flex.SetData(nStyleRow, COL_RESULTNUMBER, .Rows(nResult)("labotr_TestResultNumber"))
    '                            _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
    '                            _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)
    '                            _Flex.SetData(nStyleRow, COL_VALUE, "Value") '"Value"
    '                            _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
    '                            _Flex.SetData(nStyleRow, COL_RANGE, "Range")
    '                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, "ResultType")
    '                            _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
    '                            _Flex.SetData(nStyleRow, COL_ISFINISHED, .Rows(nResult)("labotr_IsFinished"))
    '                            _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
    '                            _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
    '                            'labs denormalization 20090321
    '                            _Flex.SetData(nStyleRow, COL_TESTNAME, .Rows(nResult)("labotr_TestName"))
    '                            '\\Added on 20090217
    '                            '---------------------------------------------------------

    '                            'sarika bug 1300 20090518
    '                            ' chetan commented on 28-oct-2010
    '                            ' _Flex.SetData(nStyleRow, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate 
    '                            ' chetan commented on 28-oct-2010
    '                            '---
    '                            ' chetan added on 28-oct-2010
    '                            _Flex.SetData(nStyleRow, COL_DATE, .Rows(nResult)("labotr_TestResultDateTime")) ' oOrder.TransactionDate
    '                            ' chetan added on 28-oct-2010
    '                            'With _Flex.Rows(nStyleRow)
    '                            '    .ImageAndText = False
    '                            '    .Height = 22
    '                            '    .Style = _Flex.Styles("CS_Header")
    '                            '    .AllowEditing = False
    '                            'End With
    '                            ' _Flex.SetCellStyle(nStyleRow, COL_SELECT, _Flex.Styles("CS_Record"))



    '                            'get the test result details
    '                            _ResultDetails = GetResultsDetailsByDate(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestName"), _Results.Rows(nResult)("labotr_TestResultNumber"))
    '                            '//---Add Test Result Header---Finish---//
    '                            For nResults As Int16 = 0 To _ResultDetails.Rows.Count - 1

    '                                With _ResultDetails

    '                                    '//---Add Test Result Detail---Start---//
    '                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(nResults)("labotrd_ResultName"), .Rows(nResults)("labotrd_TestResultNumber") & nResults, ImgResult.Image)
    '                                    nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                                    '---------------------------------------------------------
    '                                    _Flex.SetData(nStyleRow, COL_SELECT, "")
    '                                    _Flex.SetData(nStyleRow, COL_NAME, .Rows(nResults)("labotrd_ResultName"))
    '                                    _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
    '                                    _Flex.SetData(nStyleRow, COL_TESTID, _Tests.Rows(nTests)("labotd_TestID"))
    '                                    _Flex.SetData(nStyleRow, COL_RESULTNUMBER, .Rows(nResults)("labotrd_TestResultNumber"))
    '                                    _Flex.SetData(nStyleRow, COL_RESULTLINENO, .Rows(nResults)("labotrd_ResultLineNo"))
    '                                    _Flex.SetData(nStyleRow, COL_RESULTNAMEID, .Rows(nResults)("labotrd_ResultNameID"))
    '                                    _Flex.SetData(nStyleRow, COL_VALUE, .Rows(nResults)("labotrd_ResultValue"))
    '                                    _Flex.SetData(nStyleRow, COL_UNIT, .Rows(nResults)("labotrd_ResultUnit"))
    '                                    _Flex.SetData(nStyleRow, COL_RANGE, .Rows(nResults)("labotrd_ResultRange"))
    '                                    _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(.Rows(nResults)("labotrd_ResultType")))

    '                                    ''By Abhijeet on 20101122 for showing result flag description according to settings.
    '                                    ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResults)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
    '                                    Dim strFlagList As String()
    '                                    Dim strFlagInDB As String = .Rows(nResults)("labotrd_AbnormalFlag")
    '                                    strFlagList = strFlagInDB.Split(",")
    '                                    If strFlagList.Length > 1 Then
    '                                        If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
    '                                            _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
    '                                        Else
    '                                            _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
    '                                        End If
    '                                    Else
    '                                        _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResults)("labotrd_AbnormalFlag")))
    '                                    End If
    '                                    ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

    '                                    _Flex.SetData(nStyleRow, COL_ISFINISHED, .Rows(nResults)("labotrd_IsFinished"))
    '                                    _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
    '                                    _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
    '                                    _Flex.SetData(nStyleRow, COL_ResultLoincCode, .Rows(nResults)("labotrd_LOINCID"))
    '                                    _Flex.SetData(nStyleRow, COL_ReceiveDate, .Rows(nResults)("labotrd_ResultDateTime"))
    '                                    _Flex.SetData(nStyleRow, COL_TESTNAME, .Rows(nResults)("labotrd_TestName"))
    '                                    'sarika bug 1300 20090518
    '                                    ' chetan commented on 28-oct-2010
    '                                    ' _Flex.SetData(nStyleRow, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
    '                                    ' chetan commented on 28-oct-2010
    '                                    ''Added by Abhijeet on 20101026
    '                                    _Flex.SetData(nStyleRow, COL_LABFACILITYNAME, .Rows(nResults)("labotrd_LabFacilityName"))
    '                                    _Flex.SetData(nStyleRow, COL_LABFACILITYSTREETADDRESS, .Rows(nResults)("labotrd_LabFacilityStreetAddress"))
    '                                    _Flex.SetData(nStyleRow, COL_LABFACILTYCITY, .Rows(nResults)("labotrd_LabFacilityCity"))
    '                                    _Flex.SetData(nStyleRow, COL_LABFACILITYSTATE, .Rows(nResults)("labotrd_LabFacilityState"))
    '                                    _Flex.SetData(nStyleRow, COL_LABFACILITYZIPCODE, .Rows(nResults)("labotrd_LabFacilityZipCode"))
    '                                    _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, .Rows(nResults)("labotrd_ResultComment"))
    '                                    _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, .Rows(nResults)("labotrd_specificResultRefRange"))
    '                                    ''End of changes Added by Abhijeet on 20101026

    '                                    '---
    '                                    '---------------------------------------------------------

    '                                    '---------------------------------------------------------


    '                                    With _Flex.Rows(nStyleRow)
    '                                        .ImageAndText = False
    '                                        .Height = 22
    '                                        If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
    '                                            'Noraml and nothing
    '                                            .Style = _Flex.Styles("CS_Record")
    '                                        Else
    '                                            .Style = _Flex.Styles("CS_NotNormal")
    '                                            ' '' Not Normal 
    '                                        End If

    '                                        .AllowEditing = False
    '                                    End With

    '                                    '//---Add Test Result Detail---Finish---//

    '                                End With 'With .TestResultDetails.Item(nResult)

    '                            Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1

    '                        End With ' With .OrderTestResults.Item(nResults)

    '                    Next ' For nResults As Int16 = 0 To .OrderTestResults.Count - 1

    '                    '// End If ' If _TransactionType = enumTransactionType.LabResult Then

    '                End With ' With .Item(nTest)
    '                'End If ' If IsTestExists(OrderID, TestID, TestName) = False Then
    '            Next ' For nTest As Int16 = 0 To .Count - 1

    '        Next


    '        If _Flex.Rows.Count >= 2 Then
    '            _Flex.Row = 1
    '            _Flex.Select(1, COL_SELECT)
    '        End If
    '        'If _Flex.Rows.Count <> 1 Then


    '        '    If _Flex.Rows.Count <= 30 And _isTestsLoaded = False Then
    '        '        nrowcount = nrowcount + 1
    '        '        SetDataByDate(FromDate, ToDate, nrowcount)
    '        '    End If
    '        'End If
    '        Return True

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Function

    Public Function SetDataByDate(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Boolean

        If IsNothing(_ObservationStatus_COL) Then
            Fill_ResultType()
        End If
        If IsNothing(_AbnormalFlag_COL) Then
            Fill_AbnormalFlag()
        End If

        If _PatientID = 0 Then
            SetDataByDate = Nothing
            Exit Function

        End If
        
        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        Dim _FillOrderID As Int64 = 0
        'Dim _Orders As New DataTable() 'multiple orders collection 
        'Dim _Order As New DataTable() 'single order collection
        'Dim _Tests As New DataTable() 'multiple tests collection
        'Dim _Test As New DataTable() 'single test collection
        'Dim _Results As New DataTable() 'multiple results collection
        'Dim _ResultDetails As New DataTable() 'single result collection
        Dim dvResults As DataView = Nothing ''Slr new not needed
        Try



            _isTestsLoaded = False


            dsresults = GetResultsByDate(_PatientID, FromDate, ToDate, nrowcount)
            'SLR: else aloocate new dtaview
            If IsNothing(dsresults) = False Then
                dvResults = dsresults.Tables(0).DefaultView
            Else
                dvResults = New DataView
            End If

            DesignTestGrid()

            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then


                    If (nrowcount > dvResults.Count) Then
                        _isTestsLoaded = True
                    End If
                    If (nrowcount < 35) Then
                        nrowcount = 35
                    Else
                        nrowcount = dvResults.Count
                        If (nrowcount < 35) Then
                            nrowcount = 35
                        End If
                    End If
                End If
            End If
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then

                    For nOrders As Int16 = 0 To dvResults.Table.Rows.Count - 1

                        'remove the current order from the Orders table
                        _CurrentOrderID = dvResults.Table.Rows(nOrders)("labom_OrderID")

                        _CurrentVisitID = dvResults.Table.Rows(nOrders)("labom_VisitID")


                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID")) OrElse _FillOrderID <> Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestName"))
                            With dvResults.Table  '.Item(nTest)
                                '//---Add Test---Start---//
                                Dim strfilter As String = ""
                                Dim _resultnumber As Integer
                                _resultnumber = 0
                                strfilter = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(nOrders)("labom_OrderID").ToString() & "' "

                                dvResults.RowFilter = strfilter

                                _Flex.Rows.Add()

                                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail

                                _FillTestLineNo = .Rows(nOrders)("labotd_LineNo") ' .TestLineNo
                                Dim DmsID As Int64 = Convert.ToInt64(.Rows(nOrders)("labotd_DMSID"))
                                With _Flex.Rows(_Flex.Rows.Count - 1)
                                    .ImageAndText = True
                                    .Height = 22
                                    .IsNode = True
                                    .Style = _Flex.Styles("CS_Record")
                                    .Node.Level = 0
                                    If DmsID = 0 Then
                                        .Node.Image = ImgTest.Image
                                    Else
                                        .Node.Image = ImgAttachment.Image
                                    End If
                                    .Node.Data = _FillTestName
                                End With

                                _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SELECT, _Flex.Styles("CS_CheckBox"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SELECT, False)
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nOrders)("labotd_TestID")) '.TestID
                                '_Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                If IsNothing(.Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime"))
                                End If


                                _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_VISITID, _CurrentVisitID) 'VisitID
                                'labs denormalization
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTNAME, .Rows(nOrders)("labotd_TestName")) 'VisitID

                                ''Added by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, .Rows(nOrders)("labotd_TestStatus"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, .Rows(nOrders)("labotd_SpecimenSource"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENCONDITIONDISP, .Rows(nOrders)("labotd_SpecimenConditionDisp"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, .Rows(nOrders)("labotd_TestType"))
                                ''End of changes by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, .Rows(nOrders)("labotd_LOINCCode"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTCPT, .Rows(nOrders)("sCPTCode"))
                                '---

                                'Mitesh
                                If IsNothing(.Rows(nOrders)("labotr_TestResultDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATE, .Rows(nOrders)("labotr_TestResultDateTime"))
                                End If
                                '--

                                Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                                _Flex.SetCellImage(_Flex.Rows.Count - 1, COL_Lab_InfoButton, imgFlag)


                                Dim nStyleRow As Int16 = 0
                                Dim nNodeRow As Int16 = 0
                                Dim i As Int16 = 0

                                Dim _testname As String
                                For i = 0 To dvResults.Count - 1

                                    '   Dim strfilter1 As String
                                    ' strfilter1 = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "'AND labotrd_Resultlineno = '" & dvResults.Table.Rows(nOrders)("labotrd_Resultlineno").ToString() & "'"
                                    ' dvResults.RowFilter = strfilter1
                                    If IsNothing(dvResults(i)("labotr_TestID")) = False Then

                                        If Convert.ToString(dvResults(i)("labotr_TestID")) <> "" Then

                                            If _resultnumber <> dvResults(i)("labotr_testresultnumber") Then
                                                '' And _resultlineno <> dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno") Then
                                                _testname = dvResults(i)("labotr_TestName")
                                                ''  _resultlineno = dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno")
                                                ' If _resultnumber <> dsResultWithDetails.Tables(0).Rows(i)("labotr_testresultnumber") Then
                                                _resultnumber = dvResults(i)("labotr_testresultnumber")
                                                ''_Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsResults.Tables(0).Rows(nOrders)("labotr_TestResultName"), dsResults.Tables(0).Rows(nOrders)("labotd_TestID") & dsResults.Tables(0).Rows(nOrders)("labotr_TestResultNumber"), ImgResultHeader.Image)
                                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotr_TestResultName"), dvResults.Table.Rows(nOrders)("labotd_TestID") & dvResults(i)("labotr_TestResultNumber"), ImgResultHeader_Flask.Image)
                                                nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                _FillTestResultsNodeIndex = nStyleRow
                                                '---------------------------------------------------------
                                                _Flex.SetData(nStyleRow, COL_SELECT, "")
                                                _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                                _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                                _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotr_TestResultName"))
                                                _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotr_TestResultNumber"))
                                                _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
                                                _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)
                                                _Flex.SetData(nStyleRow, COL_VALUE, "Value") '"Value"
                                                _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
                                                _Flex.SetData(nStyleRow, COL_RANGE, "Range")
                                                _Flex.SetData(nStyleRow, COL_RESULTTYPE, "ResultType")
                                                _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
                                                _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotr_IsFinished"))
                                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                                _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                                'labs denormalization 20090321
                                                _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotr_TestName"))

                                                ' _Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotr_TestResultDateTime")) ' oOrder.TransactionDate
                                                'Commented not display on Result line
                                                '_Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotrd_TestSpecimenCollectionDateTime"))
                                                '---
                                            Else
                                                '  i = i + 1
                                                ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            End If
                                            ' End If
                                            ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            '//---Add Test Result Detail---Start---//

                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present
                                            If Not IsNothing(dvResults(i)("labotrd_ResultComment")) Then
                                                If dvResults(i)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Comment.Image)
                                                Else
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                                End If
                                            Else
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                            End If
                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present


                                            '_Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult.Image)
                                            nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            '---------------------------------------------------------
                                            _Flex.SetData(nStyleRow, COL_SELECT, "")
                                            _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotrd_ResultName"))
                                            _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotrd_TestResultNumber"))
                                            _Flex.SetData(nStyleRow, COL_RESULTLINENO, dvResults(i)("labotrd_ResultLineNo"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNAMEID, dvResults(i)("labotrd_ResultNameID"))
                                            '_Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue")) commented by manoj on 20121127
                                            'start of added by manoj on 20121127 for displaying hyperlink in result value
                                            If gloGlobal.gloLabGenral.IsResultisHyperLink(dvResults(i)("labotrd_ResultValue")) Then
                                                _Flex.Cols(COL_VALUE).Item(nStyleRow) = New Hyperlink(dvResults(i)("labotrd_ResultValue"))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue"))
                                            End If
                                            'end of added by manoj on 20121127 for displaying hyperlink in result value
                                            _Flex.SetData(nStyleRow, COL_UNIT, dvResults(i)("labotrd_ResultUnit"))
                                            _Flex.SetData(nStyleRow, COL_RANGE, dvResults(i)("labotrd_ResultRange"))
                                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(dvResults(i)("labotrd_ResultType")))

                                            ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                            ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResults)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
                                            Dim strFlagList As String()
                                            Dim strFlagInDB As String = dvResults(i)("labotrd_AbnormalFlag")
                                            strFlagList = strFlagInDB.Split(",")
                                            If strFlagList.Length > 1 Then
                                                If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                                Else
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                                End If
                                            Else
                                                _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(dvResults(i)("labotrd_AbnormalFlag")))
                                            End If
                                            ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                            _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotrd_IsFinished"))
                                            _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                                            _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                            _Flex.SetData(nStyleRow, COL_ResultLoincCode, dvResults(i)("labotrd_LOINCID"))
                                            _Flex.SetData(nStyleRow, COL_ReceiveDate, dvResults(i)("labotrd_ResultDateTime"))
                                            _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotrd_TestName"))
                                            'sarika bug 1300 20090518
                                            ' chetan commented on 28-oct-2010
                                            ' _Flex.SetData(nStyleRow, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                            ' chetan commented on 28-oct-2010
                                            ''Added by Abhijeet on 20101026
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYNAME, dvResults(i)("labotrd_LabFacilityName"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTREETADDRESS, dvResults(i)("labotrd_LabFacilityStreetAddress"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILTYCITY, dvResults(i)("labotrd_LabFacilityCity"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTATE, dvResults(i)("labotrd_LabFacilityState"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYZIPCODE, dvResults(i)("labotrd_LabFacilityZipCode"))
                                            _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, dvResults(i)("labotrd_ResultComment"))
                                            _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, dvResults(i)("labotrd_specificResultRefRange"))
                                            ''End of changes Added by Abhijeet on 20101026

                                            '---
                                            '---------------------------------------------------------

                                            '---------------------------------------------------------

                                            With _Flex.Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                                'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                                'If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
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
                                            _Flex.SetCellImage(nStyleRow, COL_Lab_InfoButton, imgFlag1)
                                            '//---Add Test Result Detail---Finish---//
                                            ''SLR free imgFlag1  ''giving error therefore commented
                                            'If Not IsNothing(imgFlag1) Then
                                            '    imgFlag1.Dispose()
                                            '    imgFlag1 = Nothing
                                            'End If
                                        End If
                                    End If
                                Next
                            End With 'With .TestResultDetails.Item(nResult)
                        End If
                    Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1


                End If
            End If

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function


#End Region


    Public Function GetResultsByDate(ByVal PatientId As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Rowcount As Integer)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 
            Dim ds As DataSet  ''SLR: new is not needed

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = PatientId
                oPara.Name = "@Patientid"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorResult"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Rowcount
                oPara.Name = "@RowCount"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                If chkDate.Checked Then


                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = FromDate
                    oPara.Name = "@FromDate"
                    .DBParametersCol.Add(oPara)
                    oPara = Nothing

                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = ToDate
                    oPara.Name = "@ToDate"
                    .DBParametersCol.Add(oPara)
                    oPara = Nothing


                End If

                ds = .GetDataSet("Lab_GetResults_Date")
                'ds.Tables(0).TableName = "Tests"
                'ds.Tables(1).TableName = "Results"
                'ds.Tables(2).TableName = "ResultDetails"
                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                'oPara = Nothing
            End With
            Return ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb, dbparameteds
            If Not IsNothing(odb) Then
                odb.Dispose()

            End If
            odb = Nothing
        End Try

    End Function
    Public Function GetOrderDetails(ByVal OrderId As Int64) As String
        Dim OrderDetail As String = ""
        OrderDetail = GetOrderDetail(OrderId)
        Return OrderDetail
    End Function

    Public Function GetResultsByTests(ByVal PatientId As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal TestName As String)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 
            Dim ds As DataSet  ''Slr new not needed 

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = PatientId
                oPara.Name = "@Patientid"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorResult"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestName
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                If chkDate.Checked Then


                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = FromDate
                    oPara.Name = "@FromDate"
                    .DBParametersCol.Add(oPara)
                    oPara = Nothing

                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = ToDate
                    oPara.Name = "@ToDate"
                    .DBParametersCol.Add(oPara)
                    oPara = Nothing


                End If

                ds = .GetDataSet("Lab_GetResultsByTests")
                'ds.Tables(0).TableName = "Tests"
                'ds.Tables(1).TableName = "Results"
                'ds.Tables(2).TableName = "ResultDetails"
                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                'oPara = Nothing
            End With
            Return ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finally free odb, dbpaameters
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try

    End Function
    Public Function GetResultsWithDetails_ByDate(ByVal OrderID As Int64, ByVal TestName As String)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 
            Dim ds As DataSet ''Slr new not needed 

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestName
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Bit
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorResult"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ds = .GetDataSet("GetResultsWithDetails")
                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                'oPara = Nothing
            End With
            Return ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finally free odb, dbpaameters
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try

    End Function
#Region "Sort by Test"

    ''get all tests of conducted against that patient 
    Private Function GetTests(ByVal PatientId As Long, ByVal Todate As Date) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable = Nothing
        Try


            'first get all the Tests aginst that patient
            '//// <> Order Master 
            '   dt = New DataTable
            With odb


                ''Added new fields labotd_TestType,labotd_TestStatus,labotd_SpecimenSource in following query
                Dim _strSql As String = "SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, " _
              & " Lab_Order_TestDtl.labotd_TestType,Lab_Order_TestDtl.labotd_TestStatus,Lab_Order_TestDtl.labotd_SpecimenSource,Lab_Order_TestDtl.labotd_LOINCCode " _
              & " FROM Lab_Order_TestDtl INNER JOIN " _
              & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
              & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
              & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & " Order by Lab_Test_Mst.labtm_Name"

                ' ''Fill Order Master 
                dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    Private Function GetTests(ByVal PatientId As Long, ByVal Fromdate As Date, ByVal ToDate As Date, ByVal strTestName As String) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try


            'first get all the Tests aginst that patient
            '//// <> Order Master 
            ' dt = New DataTable  'SLR: new not needed
            With odb
                'commented by sanjog 
                'Dim _strSql As String
                'If strTestName = "" Then
                '    ''Added new fields labotd_TestType,labotd_TestStatus,labotd_SpecimenSource in following query
                '    _strSql = "SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, " _
                '                  & " Lab_Order_TestDtl.labotd_TestType,Lab_Order_TestDtl.labotd_TestStatus,Lab_Order_TestDtl.labotd_SpecimenSource,Lab_Order_TestDtl.labotd_LOINCCode " _
                '                  & " FROM Lab_Order_TestDtl INNER JOIN " _
                '                  & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                '                  & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
                '                  & " WHERE (Lab_Order_MST.labom_PatientID = " & PatientId & " AND convert(datetime,convert(varchar,labom_TransactionDate,101)) between '" & Fromdate & "' and '" & ToDate & "') order by Lab_Test_Mst.labtm_Name"

                '    '_strSql = "SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, " _
                '    '              & " Lab_Order_TestDtl.labotd_TestType,Lab_Order_TestDtl.labotd_TestStatus,Lab_Order_TestDtl.labotd_SpecimenSource,Lab_Order_TestDtl.labotd_LOINCCode " _
                '    '              & " FROM Lab_Order_TestDtl INNER JOIN " _
                '    '              & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                '    '              & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
                '    '              & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & "  order by Lab_Test_Mst.labtm_Name"

                'Else
                '    ''Added new fields labotd_TestType,labotd_TestStatus,labotd_SpecimenSource in following query
                '    _strSql = "SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, " _
                '                  & " Lab_Order_TestDtl.labotd_TestType,Lab_Order_TestDtl.labotd_TestStatus,Lab_Order_TestDtl.labotd_SpecimenSource,Lab_Order_TestDtl.labotd_LOINCCode " _
                '                  & " FROM Lab_Order_TestDtl INNER JOIN " _
                '                  & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                '                  & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
                '                  & " WHERE (Lab_Order_MST.labom_PatientID = " & PatientId & " and Lab_Test_Mst.labtm_Name = '" & (strTestName).Replace("'", "''") & "' AND convert(datetime,convert(varchar,labom_TransactionDate,101)) between '" & Fromdate & "' and '" & ToDate & "') order by Lab_Test_Mst.labtm_Name"

                '    '_strSql = "SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, " _
                '    '             & " Lab_Order_TestDtl.labotd_TestType,Lab_Order_TestDtl.labotd_TestStatus,Lab_Order_TestDtl.labotd_SpecimenSource,Lab_Order_TestDtl.labotd_LOINCCode " _
                '    '             & " FROM Lab_Order_TestDtl INNER JOIN " _
                '    '             & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                '    '             & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
                '    '             & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & " and Lab_Test_Mst.labtm_Name = '" & (strTestName).Replace("'", "''") & "' order by Lab_Test_Mst.labtm_Name"

                'End If
                Dim oPara As gloEMRDatabase.DBParameter

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = PatientId
                oPara.Name = "@PatientID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Fromdate
                oPara.Name = "@FromDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = ToDate
                oPara.Name = "@ToDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = strTestName
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorCheck"
                .DBParametersCol.Add(oPara)

                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If chkDate.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@DateCheck"
                .DBParametersCol.Add(oPara)

                dt = .GetDataTable("Lab_GetTests")
                oPara = Nothing


                ' ''Fill Order Master 
                'dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRe odb, dbparametrer
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    'get all orders of that Test. therefore pass the testID
    Private Function GetTestOrders(ByVal TestId As Long, Optional ByVal PatientID As Int64 = 0) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try
            ''Patient ID parameter added by Sandip Darade 20090619 to pull orders particular to patient
            '//// <> Order Master 
            ' dt = New DataTable  'SLR: new not needed
            With odb
                If (PatientID = 0) Then
                    Dim _strSql As String = "SELECT Lab_Order_MST.labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate " _
                   & " FROM Lab_Order_MST INNER JOIN " _
                   & " Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID " _
                   & " WHERE Lab_Order_TestDtl.labotd_TestID =" & TestId & ""

                    ' ''Fill Order Master 
                    dt = .GetDataTable_Query(_strSql)
                Else
                    Dim _strSql As String = "SELECT Lab_Order_MST.labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate " _
                                      & " FROM Lab_Order_MST INNER JOIN " _
                                      & " Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID " _
                                      & " WHERE Lab_Order_TestDtl.labotd_TestID =" & TestId & " AND   Lab_Order_MST.labom_PatientID =" & PatientID & "  "

                    ' ''Fill Order Master 
                    dt = .GetDataTable_Query(_strSql)
                End If

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    'get all the TestResultDeatils of that Test against that order. Therefore pass the testid and orderid
    Private Function GetTestResults(ByVal TestId As Long, ByVal OrderNumbersLst As String) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try


            'first get all the Tests aginst that patient
            '//// <> Order Master 
            '  dt = New DataTable  SLR new not needed 
            With odb
                ' Dim _strSql As String = "SELECT Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime, Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
                '& " FROM Lab_Order_Test_Result INNER JOIN " _
                '& " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
                '& " WHERE (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") order by convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)"


                'Dim _strSql As String = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange " _
                '                       & " FROM Lab_Order_Test_Result INNER JOIN  " _
                '                       & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                '                       & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "
                ' ''Fill Order Master 


                Dim _strSql As String
                If ChkPrior.Checked Then
                    _strSql = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_ResultType " _
                                                           & " FROM Lab_Order_Test_Result INNER JOIN  " _
                                                           & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                                                           & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "
                Else
                    _strSql = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_ResultType " _
                                                           & " FROM Lab_Order_Test_Result INNER JOIN  " _
                                                           & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                                                           & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID " _
                                                           & " AND labotr_TestResultNumber in( SELECT MAX(Lab_Order_Test_Result1.labotr_TestResultNumber) FROM  Lab_Order_Test_Result As Lab_Order_Test_Result1 WHERE Lab_Order_Test_Result1.labotr_OrderID=Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_Result1.labotr_TestID =Lab_Order_Test_Result.labotr_TestID) " _
                                                           & "WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "
                End If


                dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    Private Function GetTestResults(ByVal TestId As Long, ByVal OrderNumbersLst As String, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try


            'first get all the Tests aginst that patient
            '//// <> Order Master 
            ' dt = New DataTable  'SLR new not needed 
            With odb
                ' Dim _strSql As String = "SELECT Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime, Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
                '& " FROM Lab_Order_Test_Result INNER JOIN " _
                '& " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
                '& " WHERE (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") order by convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)"


                'Dim _strSql As String = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag ,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange" _
                '                       & " FROM Lab_Order_Test_Result INNER JOIN  " _
                '                       & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                '                       & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") and convert(datetime,convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)) between '" & FromDate & "' and '" & ToDate & "' ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "

                'Dim _strSql As String = ""

                'If ChkPrior.Checked = False Then
                '    _strSql = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag ,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange" _
                '                                           & " FROM Lab_Order_Test_Result INNER JOIN  " _
                '                                           & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                '                                           & " Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " _
                '                                           & "IN ( SELECT MAX(Lab_Order_Test_Result1.labotrd_TestResultNumber) FROM  Lab_Order_Test_ResultDtl AS Lab_Order_Test_Result1 " _
                '                                           & " WHERE(Lab_Order_Test_Result1.labotrd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID)" _
                '                                           & " AND Lab_Order_Test_Result1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID) AND " _
                '                                           & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") and convert(datetime,convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)) between '" & FromDate & "' and '" & ToDate & "' ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "
                'Else
                '    _strSql = " Select DISTINCT CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) , Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag ,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange" _
                '                                           & " FROM Lab_Order_Test_Result INNER JOIN  " _
                '                                           & " Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND " _
                '                                           & " Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID WHERE     (Lab_Order_Test_Result.labotr_TestID = " & TestId & ") AND (Lab_Order_Test_Result.labotr_OrderID IN " & OrderNumbersLst & ") and convert(datetime,convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)) between '" & FromDate & "' and '" & ToDate & "' ORDER BY  CONVERT(varchar, Lab_Order_Test_Result.labotr_TestResultDateTime, 101) "
                'End If


                Dim oPara As gloEMRDatabase.DBParameter

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestId
                oPara.Name = "@TestId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing



                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = FromDate
                oPara.Name = "@FromDate"
                .DBParametersCol.Add(oPara)

                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = ToDate
                oPara.Name = "@ToDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderNumbersLst
                oPara.Name = "@OrderNumbersLst"
                .DBParametersCol.Add(oPara)

                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorCheck"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If chkDate.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@DateCheck"
                .DBParametersCol.Add(oPara)

                dt = .GetDataTable("Lab_GetTestResults")
                oPara = Nothing



                ' ''Fill Order Master 
                'dt = .GetDataTable_Query(_strSql)
            End With
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb, dbparametrer
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    'Private Function SetDataByTests(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Boolean

    '    If _PatientID = 0 Then Exit Function

    '    Dim _FillTestName As String = ""
    '    Dim _FillTestCode As String = ""
    '    Dim _FillTestNodeIndex As Int16 = 0
    '    Dim _FillTestResultsNodeIndex As Int16 = 0
    '    Dim _FillTestLineNo As Int16 = 0

    '    Dim _Orders As New DataTable() 'multiple orders collection 
    '    Dim _Order As New DataTable() 'single order collection
    '    Dim _Tests As New DataTable() 'multiple tests collection
    '    Dim _Test As New DataTable() 'single test collection
    '    Dim _Results As New DataTable() 'multiple results collection
    '    Dim _ResultDetails As New DataTable() 'single result collection



    '    Dim _OrderIdLst As New ArrayList
    '    Try


    '        ''get all tests of conducted against that patient 
    '        '_Tests = GetTests(_PatientID, DateTime.Now.Date)

    '        'For nTests As Int16 = 0 To _Tests.Rows.Count - 1

    '        '    'get all orders of that Test. therefore pass the testID
    '        '    _Orders = GetTestOrders(_Tests.Rows(nTests)("labotd_TestID"))
    '        '    Dim _OrderNumbers As String = ""

    '        '    'loop through all the order and fill the order id against that test in the orderid arraylist
    '        '    'this order id list will be passed to get the results, if there are multiple results against that Test.
    '        '    For _nOrders As Int16 = 0 To _Orders.Rows.Count - 1
    '        '        If (_nOrders = 0) Then
    '        '            _OrderNumbers = "(" & _Orders.Rows(_nOrders)("labom_OrderID").ToString()
    '        '        Else
    '        '            _OrderNumbers = _OrderNumbers & "," & _Orders.Rows(_nOrders)("labom_OrderID").ToString()
    '        '        End If
    '        '    Next
    '        '    _OrderNumbers = _OrderNumbers & ")"

    '        '    _Flex.Rows.Add()

    '        '    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _Tests.Rows(nTests)("labotd_DateTime") & "")
    '        '    _Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, _Tests.Rows(nTests)("labtm_Name") & "")

    '        '    Dim nStyleRow As Int16 = 0
    '        '    'fill the Result details for that  Test by passing the Test id and the string of order numbers because we hv used IN clause in this query
    '        '    _ResultDetails = GetTestResults(_Tests.Rows(nTests)("labotd_TestID"), _OrderNumbers)
    '        '    For _nResults As Int16 = 0 To _ResultDetails.Rows.Count - 1
    '        '        With _ResultDetails



    '        '            'under the TestName the Result name should come so the felx coloum shold be a treeview coloumn
    '        '            '//---Add Test Result Header---Start---//
    '        '            _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(_nResults)("labotrd_ResultName"), _Tests.Rows(nTests)("labotd_TestID"), ImgResultHeader.Image)
    '        '            nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '        '            _FillTestResultsNodeIndex = nStyleRow

    '        '            _Flex.SetData(nStyleRow, COL_DATE, "")
    '        '            _Flex.SetData(nStyleRow, COL_VALUE, _ResultDetails.Rows(_nResults)("labotrd_ResultValue")) '"Value"
    '        '            _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
    '        '            _Flex.SetData(nStyleRow, COL_RANGE, "Range")
    '        '            _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
    '        '            '_Flex.SetData(nStyleRow, COL_ISFINISHED, .Rows(nResult)("labotr_IsFinished"))
    '        '            '_Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
    '        '            '_Flex.SetData(_Flex.Rows.Count - 1, COL_VALUE, _Tests.Rows(nTests)("labotrd_ResultValue") & "")
    '        '            '_Flex.SetData(_Flex.Rows.Count - 1, COL_UNIT, _Tests.Rows(nTests)("labotrd_ResultUnit") & "")
    '        '            '_Flex.SetData(_Flex.Rows.Count - 1, COL_RANGE, _Tests.Rows(nTests)("labotrd_ResultRange") & "")
    '        '        End With
    '        '    Next


    '        'Next


    '        '-------------------------------------------------------


    '        'get all tests of conducted against that patient 
    '        'If IsFilterForTest Then
    '        '    _Tests = GetTests(_PatientID, FromDate, ToDate, strSearchString)
    '        'Else
    '        '    '_Tests = GetTests(_PatientID, DateTime.Now.Date)
    '        '    _Tests = GetTests(_PatientID, FromDate, ToDate, strSearchString)
    '        'End If

    '        _Tests = GetTests(_PatientID, FromDate, ToDate, strSearchString)

    '        If _ClearCombo = True Then
    '            cmbType.Items.Clear()
    '        End If

    '        If IsNothing(_Tests) = False Then
    '            For nTests As Int16 = 0 To _Tests.Rows.Count - 1

    '                'get all orders of that Test. therefore pass the testID
    '                _Orders = GetTestOrders(_Tests.Rows(nTests)("labotd_TestID"), _PatientID)

    '                If IsFilterForTest = False And _ClearCombo = True Then
    '                    If cmbType.Items.Contains(_Tests.Rows(nTests)("labtm_Name")) = False Then
    '                        cmbType.Items.Add(_Tests.Rows(nTests)("labtm_Name"))
    '                    End If
    '                End If

    '                Dim _OrderNumbers As String = ""

    '                'loop through all the order and fill the order id against that test in the orderid arraylist
    '                'this order id list will be passed to get the results, if there are multiple results against that Test.
    '                If IsNothing(_Orders) = False Then
    '                    For _nOrders As Int16 = 0 To _Orders.Rows.Count - 1
    '                        If (_nOrders = 0) Then
    '                            _OrderNumbers = _Orders.Rows(_nOrders)("labom_OrderID").ToString()
    '                        Else
    '                            _OrderNumbers = _OrderNumbers & "," & _Orders.Rows(_nOrders)("labom_OrderID").ToString()
    '                        End If
    '                    Next
    '                    _OrderNumbers = _OrderNumbers

    '                    _Orders = Nothing
    '                End If

    '                'If _ResultDetails.Rows.Count <> 0 And IsFilterForTest = False Then
    '                '//---Add Test---Start---//
    '                _Flex.Rows.Add()

    '                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail


    '                With _Flex.Rows(_Flex.Rows.Count - 1)
    '                    .ImageAndText = True
    '                    .Height = 22
    '                    .IsNode = True
    '                    .Style = _Flex.Styles("CS_Record")
    '                    .Node.Level = 0
    '                    .Node.Image = ImgTest.Image
    '                    .Node.Data = _FillTestName
    '                End With


    '                '_Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _Tests.Rows(nTests)("labotd_DateTime") & "")
    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, _Tests.Rows(nTests)("labtm_Name") & "")
    '                ' End If

    '                ''By ABhijeet on 20101026
    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, _Tests.Rows(nTests)("labotd_TestType") & "")
    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, _Tests.Rows(nTests)("labotd_TestStatus") & "")
    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, _Tests.Rows(nTests)("labotd_SpecimenSource") & "")
    '                ''end of changes By ABhijeet on 20101026
    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, _Tests.Rows(nTests)("labotd_LOINCCode"))
    '                '//---Add Test---Finish---//


    '                Dim nStyleRow As Int16 = 0
    '                Dim nNodeRow As Int16 = 0
    '                Dim i As Int16 = 0

    '                'fill the Result details for that  Test by passing the Test id and the string of order numbers because we hv used IN clause in this query
    '                If IsFilterForTest Then
    '                    _ResultDetails = GetTestResults(_Tests.Rows(nTests)("labotd_TestID"), _OrderNumbers, FromDate, ToDate)
    '                Else
    '                    '_ResultDetails = GetTestResults(_Tests.Rows(nTests)("labotd_TestID"), _OrderNumbers)
    '                    _ResultDetails = GetTestResults(_Tests.Rows(nTests)("labotd_TestID"), _OrderNumbers, FromDate, ToDate)
    '                End If

    '                '// If _TransactionType = enumTransactionType.LabResult Then

    '                If IsNothing(_ResultDetails) = False Then
    '                    'under the TestName the Result name should come so the felx coloum shold be a treeview coloumn
    '                    For nResultDetails As Int16 = 0 To _ResultDetails.Rows.Count - 1
    '                        With _ResultDetails
    '                            '//---Add Test Result Header---Start---//
    '                            _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(nResultDetails)("labotrd_ResultName"), _Tests.Rows(nTests)("labotd_TestID"), ImgResultHeader.Image)
    '                            nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                            _FillTestResultsNodeIndex = nStyleRow
    '                            '---------------------------------------------------------
    '                            _Flex.SetCellStyle(nStyleRow, COL_SELECT, _Flex.Styles("CS_CheckBox"))
    '                            _Flex.SetData(nStyleRow, COL_SELECT, False)
    '                            _Flex.SetData(nStyleRow, COL_DATE, .Rows(nResultDetails)("labotr_TestResultDateTime") & "")
    '                            _Flex.SetData(nStyleRow, COL_NAME, .Rows(nResultDetails)("labotrd_ResultName"))
    '                            _Flex.SetData(nStyleRow, COL_VALUE, .Rows(nResultDetails)("labotrd_ResultValue")) '"Value"
    '                            _Flex.SetData(nStyleRow, COL_UNIT, .Rows(nResultDetails)("labotrd_ResultUnit"))
    '                            _Flex.SetData(nStyleRow, COL_RANGE, .Rows(nResultDetails)("labotrd_ResultRange"))
    '                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(.Rows(nResultDetails)("labotrd_ResultType")))

    '                            ''By Abhijeet on 20101122 for showing result flag description according to settings.
    '                            ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResultDetails)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
    '                            Dim strFlagList As String()
    '                            Dim strFlagInDB As String = .Rows(nResultDetails)("labotrd_AbnormalFlag")
    '                            strFlagList = strFlagInDB.Split(",")
    '                            If strFlagList.Length > 1 Then
    '                                If glogeneral.clsgeneral.gstrSpecificResultRange = "1" Then
    '                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
    '                                Else
    '                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
    '                                End If
    '                            Else
    '                                _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResultDetails)("labotrd_AbnormalFlag")))
    '                            End If
    '                            ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

    '                            _Flex.SetData(nStyleRow, COL_ReceiveDate, .Rows(nResultDetails)("labotr_TestResultDateTime")) '\\Added on 20090217
    '                            _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, .Rows(nResultDetails)("labotrd_specificResultRefRange"))
    '                            '---------------------------------------------------------


    '                            With _Flex.Rows(nStyleRow)
    '                                '_Flex.Cols(COL_SELECT).AllowEditing = True
    '                                .ImageAndText = False
    '                                .Height = 22
    '                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
    '                                    'Noraml and nothing
    '                                    .Style = _Flex.Styles("CS_Record")
    '                                Else
    '                                    .Style = _Flex.Styles("CS_NotNormal")
    '                                    ' '' Not Normal 
    '                                End If

    '                                '.AllowEditing = True
    '                            End With
    '                            'With _Flex.Rows(nStyleRow)
    '                            '    .ImageAndText = False
    '                            '    .Height = 22
    '                            '    .Style = _Flex.Styles("CS_Header")
    '                            '    .AllowEditing = False
    '                            'End With
    '                            '_Flex.SetCellStyle(nStyleRow, COL_SELECT, _Flex.Styles("CS_Record"))


    '                            '//---Add Test Result Header---Finish---//

    '                            '//---Add Test Result Detail---Start---//

    '                            '//---Add Test Result Detail---Finish---//


    '                        End With 'With .TestResultDetails.Item(nResult)

    '                    Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1
    '                End If

    '            Next ' For nResults As Int16 = 0 To .OrderTestResults.Count - 1
    '        End If
    '        '-------------------------------------------------------

    '        If _Flex.Rows.Count >= 2 Then
    '            _Flex.Row = 1
    '            _Flex.Select(1, COL_SELECT)
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function
    Private Function SetDataByTests(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Boolean

        If _PatientID = 0 Then
            SetDataByTests = Nothing
            Exit Function
        End If


        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        Dim _FillOrderID As Int64 = 0
        ''   Dim _Orders As New DataTable() 'multiple orders collection 
        '' Dim _Order As New DataTable() 'single order collection
        '' Dim _Tests As New DataTable() 'multiple tests collection
        ''  Dim _Test As New DataTable() 'single test collection
        ''Dim _Results As New DataTable() 'multiple results collection
        '' Dim _ResultDetails As New DataTable() 'single result collection
        Dim dvTests As DataView = Nothing  ''Slr new not needed 


        Dim _OrderIdLst As New ArrayList
        Try


            dsTests = GetResultsByTests(_PatientID, FromDate, ToDate, strSearchString)

            If IsNothing(dsTests) = False Then
                dvTests = dsTests.Tables(0).DefaultView
            Else
                'SLR: Else allocate new view
                dvTests = New DataView
            End If



            If _ClearCombo = True Then
                cmbType.Items.Clear()
            End If

            If IsNothing(dvTests) = False Then
                For nTests As Int16 = 0 To dvTests.Table.Rows.Count - 1


                    If _FillTestCode <> Convert.ToString(dvTests.Table.Rows(nTests)("labotd_TestID")) OrElse _FillOrderID <> Convert.ToString(dvTests.Table.Rows(nTests)("labom_OrderID")) Then
                        _FillTestCode = Convert.ToString(dvTests.Table.Rows(nTests)("labotd_TestID"))
                        _FillOrderID = Convert.ToString(dvTests.Table.Rows(nTests)("labom_OrderID"))
                        If IsFilterForTest = False And _ClearCombo = True Then
                            If cmbType.Items.Contains(dvTests.Table.Rows(nTests)("labotd_TestName")) = False Then
                                cmbType.Items.Add(dvTests.Table.Rows(nTests)("labotd_TestName"))
                            End If

                        End If


                        Dim strfilter As String = ""
                        Dim _resultnumber As Integer
                        _resultnumber = 0
                        strfilter = "labotd_TestID = '" & dvTests.Table.Rows(nTests)("labotd_TestID").ToString() & "'  and labom_OrderID = '" & dvTests.Table.Rows(nTests)("labom_OrderID").ToString() & "' "

                        dvTests.RowFilter = strfilter

                        _Flex.Rows.Add()

                        _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail

                        Dim DMSID As Int64 = Convert.ToInt64(dvTests.Table.Rows(nTests)("labotd_DMSID"))
                        With _Flex.Rows(_Flex.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 22
                            .IsNode = True
                            .Style = _Flex.Styles("CS_Record")
                            .Node.Level = 0
                            If DMSID > 0 Then
                                .Node.Image = ImgAttachment.Image
                            Else
                                .Node.Image = ImgTest.Image
                            End If

                            .Node.Data = _FillTestName
                        End With




                        _Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, dvTests.Table.Rows(nTests)("labotd_TestName") & "")

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, dvTests.Table.Rows(nTests)("labotd_TestType") & "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, dvTests.Table.Rows(nTests)("labotd_TestStatus") & "")
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, dvTests.Table.Rows(nTests)("labotd_SpecimenSource") & "")

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, dvTests.Table.Rows(nTests)("labotd_LOINCCode"))
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTCPT, dvTests.Table.Rows(nTests)("sCPTCode"))

                        _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, dvTests.Table.Rows(nTests)("labom_OrderID"))
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, dvTests.Table.Rows(nTests)("labotd_TestID"))
                        'Mitesh
                        ' _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_COMMENT, dvTests.Table.Rows(nTests)("labotrd_ResultComment"))

                        If IsNothing(dvTests.Table.Rows(nTests)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, dvTests.Table.Rows(nTests)("labotrd_TestSpecimenCollectionDateTime") & "")
                        End If
                        '---

                        If IsNothing(dvTests.Table.Rows(nTests)("labotr_TestResultDateTime")) = False Then
                            _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATE, dvTests.Table.Rows(nTests)("labotr_TestResultDateTime") & "")
                        End If




                        Dim nStyleRow As Int16 = 0
                        Dim nNodeRow As Int16 = 0
                        Dim i As Int16 = 0



                        If IsNothing(dvTests) = False Then
                            'under the TestName the Result name should come so the felx coloum shold be a treeview coloumn
                            For nResultDetails As Int16 = 0 To dvTests.Count - 1
                                With dvTests
                                    If IsNothing(dvTests(i)("labotr_TestID")) = False Then

                                        If Convert.ToString(dvTests(i)("labotr_TestID")) <> "" Then

                                            '//---Add Test Result Header---Start---//
                                            If Not IsNothing(dvTests(nResultDetails)("labotrd_ResultComment")) Then
                                                If dvTests(nResultDetails)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvTests(nResultDetails)("labotrd_ResultName"), dvTests.Table.Rows(nTests)("labotd_TestID"), ImgResult_Comment.Image)
                                                Else
                                                    _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvTests(nResultDetails)("labotrd_ResultName"), dvTests.Table.Rows(nTests)("labotd_TestID"), ImgResult_Flask.Image)
                                                End If
                                            Else
                                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvTests(nResultDetails)("labotrd_ResultName"), dvTests.Table.Rows(nTests)("labotd_TestID"), ImgResult_Flask.Image)
                                            End If

                                            ' _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvTests(nResultDetails)("labotrd_ResultName"), dvTests.Table.Rows(nTests)("labotd_TestID"), ImgResultHeader.Image)
                                            nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            _FillTestResultsNodeIndex = nStyleRow

                                            'Developer name : Mitesh Patel
                                            'Case No : Incident #00002197 
                                            If Not IsNothing(dvTests(nResultDetails)("labotrd_ResultComment")) Then
                                                If dvTests(nResultDetails)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.SetData(_FillTestResultsNodeIndex, COL_RESULT_COMMENT, dvTests(nResultDetails)("labotrd_ResultComment").ToString())
                                                End If
                                            End If

                                            '---------------------------------------------------------
                                            _Flex.SetCellStyle(nStyleRow, COL_SELECT, _Flex.Styles("CS_CheckBox"))
                                            _Flex.SetData(nStyleRow, COL_SELECT, False)
                                            ' _Flex.SetData(nStyleRow, COL_DATE, dvTests(nResultDetails)("labotr_TestResultDateTime") & "")
                                            'Mitesh
                                            'Only Show on Test line not on Result
                                            'If IsNothing(dvTests(nResultDetails)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                            '    _Flex.SetData(nStyleRow, COL_DATE, dvTests(nResultDetails)("labotrd_TestSpecimenCollectionDateTime") & "")
                                            'End If
                                            '---

                                            _Flex.SetData(nStyleRow, COL_NAME, dvTests(nResultDetails)("labotrd_ResultName"))
                                            _Flex.SetData(nStyleRow, COL_ResultLoincCode, dvTests(nResultDetails)("labotrd_LoincID"))
                                            '_Flex.SetData(nStyleRow, COL_VALUE, dvTests(nResultDetails)("labotrd_ResultValue")) '"Value" 'commented by manoj 
                                            'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                            If gloGlobal.gloLabGenral.IsResultisHyperLink(dvTests(nResultDetails)("labotrd_ResultValue")) Then
                                                _Flex.Cols(COL_VALUE).Item(nStyleRow) = New Hyperlink(dvTests(i)("labotrd_ResultValue"))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_VALUE, dvTests(nResultDetails)("labotrd_ResultValue")) '"Value"
                                            End If
                                            'end of added by manoj on 20121127 for dispalying hyperlinks in result value
                                            _Flex.SetData(nStyleRow, COL_UNIT, dvTests(nResultDetails)("labotrd_ResultUnit"))
                                            _Flex.SetData(nStyleRow, COL_RANGE, dvTests(nResultDetails)("labotrd_ResultRange"))
                                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(dvTests(nResultDetails)("labotrd_ResultType")))

                                            ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                            ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResultDetails)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
                                            Dim strFlagList As String()
                                            Dim strFlagInDB As String = dvTests(nResultDetails)("labotrd_AbnormalFlag")
                                            strFlagList = strFlagInDB.Split(",")
                                            If strFlagList.Length > 1 Then
                                                If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                                Else
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                                End If
                                            Else
                                                _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(dvTests(nResultDetails)("labotrd_AbnormalFlag")))
                                            End If
                                            ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                            _Flex.SetData(nStyleRow, COL_ReceiveDate, dvTests(nResultDetails)("labotr_TestResultDateTime")) '\\Added on 20090217
                                            _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, dvTests(nResultDetails)("labotrd_specificResultRefRange"))
                                            '---------------------------------------------------------


                                            With _Flex.Rows(nStyleRow)
                                                '_Flex.Cols(COL_SELECT).AllowEditing = True
                                                .ImageAndText = False
                                                .Height = 22
                                                'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                                'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                                'If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                    'Noraml and nothing
                                                    .Style = _Flex.Styles("CS_Record")
                                                Else
                                                    .Style = _Flex.Styles("CS_NotNormal")
                                                    ' '' Not Normal 
                                                End If

                                                '.AllowEditing = True
                                            End With
                                        End If

                                    End If

                                End With 'With .TestResultDetails.Item(nResult)

                            Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1
                        End If
                    End If
                Next ' For nResults As Int16 = 0 To .OrderTestResults.Count - 1
            End If
            '-------------------------------------------------------

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
#End Region


#Region "Sort by Results"

    'get all the Distinc results against the tests conducted for that patient
    Private Function GetResultsByResult(ByVal PatientId As Long, ByVal Todate As Date) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable = Nothing
        Try

            ' dt = New DataTable SLR new not needed
            With odb
                Dim _strSql As String = "SELECT DISTINCT Lab_Order_Test_ResultDtl.labotrd_ResultName FROM Lab_Order_Test_ResultDtl" _
               & " INNER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID" _
               & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & ""

                ' ''Fill Order Master 
                dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function


    'get the result details against those tests
    Private Function GetResultsDetailsByResult1(ByVal ResultName As String) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable = Nothing
        Try

            ' dt = New DataTable  SLR new not needed 
            With odb
                Dim _strSql As String = "SELECT Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_Result.labotr_TestResultName, Lab_Order_Test_Result.labotr_TestResultDateTime,Lab_Order_Test_ResultDtl.labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, Lab_Order_Test_ResultDtl.labotrd_ResultType, Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_ResultType " _
               & " FROM Lab_Order_Test_Result RIGHT OUTER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID" _
               & " WHERE Lab_Order_Test_ResultDtl.labotrd_ResultName = " & "'" & ResultName & "'" & ""

                ' ''Fill Order Master 
                dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    ' Not in used so commented

    'Private Function SetDataByResults1() As Boolean
    '    If _PatientID = 0 Then Exit Function

    '    Dim _FillTestName As String = ""
    '    Dim _FillTestCode As String = ""
    '    Dim _FillTestNodeIndex As Int16 = 0
    '    Dim _FillTestResultsNodeIndex As Int16 = 0
    '    Dim _FillTestLineNo As Int16 = 0

    '    '   Dim _Orders As New DataTable() 'multiple orders collection    slr not used 
    '    ' Dim _Order As New DataTable() 'single order collection   slr not used 
    '    '   Dim _Tests As New DataTable() 'multiple tests collection  slr not used 
    '   ' Dim _Test As New DataTable() 'single test collection  slr not used 
    '    Dim _Results As DataTable 'multiple results collection
    '    Dim _ResultDetails As DataTable 'single result collection


    '    _Results = GetResultsByResult(_PatientID, DateTime.Now.Date)

    '    'loop through the Tests and get the orders against those tests by passing the individual testId
    '    For nResults As Int16 = 0 To _Results.Rows.Count - 1
    '        _ResultDetails = GetResultsDetailsByResult()

    '        For nResultsDetails As Int16 = 0 To _ResultDetails.Rows.Count - 1
    '            '//---Add Results---Start---//
    '            _Flex.Rows.Add()
    '            _FillTestNodeIndex = _Flex.Rows.Count - 1


    '            With _Flex.Rows(_Flex.Rows.Count - 1)
    '                .ImageAndText = True
    '                .Height = 22
    '                .IsNode = True
    '                .Style = _Flex.Styles("CS_Record")
    '                .Node.Level = 0
    '                .Node.Image = ImgTest.Image
    '                .Node.Data = _FillTestName
    '            End With

    '            _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _ResultDetails.Rows(nResultsDetails)("labotr_TestResultDateTime"))
    '            _Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, _Results.Rows(nResults)("labotrd_ResultName"))

    '            _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, _ResultDetails.Rows(nResultsDetails)("labotr_TestResultDateTime")) ', _Results.Rows(nResultsDetails)("labotrd_ResultValue")) '& .Rows(nResult)("labotr_TestResultNumber"), ImgResultHeader.Image)

    '            '_Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, _Results.Rows(nResultsDetails)("labotrd_ResultName"))


    '        Next

    '    Next




    '    '_Orders = GetOrdersByDate(_PatientID, DateTime.Now.Date)
    '    'For nOrders As Int16 = 0 To _Orders.Rows.Count - 1

    '    '    'remove the current order from the Orders table
    '    '    _CurrentOrderID = _Orders.Rows(nOrders)("labom_OrderID")

    '    '    'get the current order against that orderId
    '    '    _Order = GetOrderByDate(_CurrentOrderID)

    '    '    'remove all the Tests against that order
    '    '    _Tests = GetTestsByDate(_CurrentOrderID)

    '    '    'loop if there are any multiple Tests against that order and set the data to the flex grid
    '    '    For nTests As Int16 = 0 To _Tests.Rows.Count - 1
    '    '        _FillTestName = GetTestName(_Tests.Rows(nTests)("labotd_TestID"))
    '    '        _FillTestCode = GetTestCode(_Tests.Rows(nTests)("labotd_TestID"))

    '    '        'If IsTestExists(.Item(nTest).OrderID, .Item(nTest).TestID, _FillTestName) = False Then
    '    '        If IsTestExists(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestID"), _FillTestName) = False Then
    '    '            With _Tests '.Item(nTest)
    '    '                '//---Add Test---Start---//
    '    '                _Flex.Rows.Add()

    '    '                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail
    '    '                _FillTestLineNo = .Rows(nTests)("labotd_LineNo") ' .TestLineNo

    '    '                With _Flex.Rows(_Flex.Rows.Count - 1)
    '    '                    .ImageAndText = True
    '    '                    .Height = 22
    '    '                    .IsNode = True
    '    '                    .Style = _Flex.Styles("CS_Record")
    '    '                    .Node.Level = 0
    '    '                    .Node.Image = ImgTest.Image
    '    '                    .Node.Data = _FillTestName
    '    '                End With

    '    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
    '    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nTests)("labotd_TestID")) '.TestID
    '    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _Order.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
    '    '                _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
    '    '                '//---Add Test---Finish---//

    '    '                '// If _TransactionType = enumTransactionType.LabResult Then

    '    '                Dim nStyleRow As Int16 = 0
    '    '                Dim nNodeRow As Int16 = 0
    '    '                Dim i As Int16 = 0

    '    '                'get the results against that orderid and that testID
    '    '                _Results = GetResultsByDate(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestID"))

    '    '                For nResult As Int16 = 0 To _Results.Rows.Count - 1
    '    '                    With _Results
    '    '                        '//---Add Test Result Header---Start---//
    '    '                        _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(nResult)("labotr_TestResultName"), _Tests.Rows(nTests)("labotd_TestID") & .Rows(nResult)("labotr_TestResultNumber"), ImgResultHeader.Image)
    '    '                        nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '    '                        _FillTestResultsNodeIndex = nStyleRow
    '    '                        '---------------------------------------------------------
    '    '                        _Flex.SetData(nStyleRow, COL_SELECT, "")
    '    '                        _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
    '    '                        _Flex.SetData(nStyleRow, COL_TESTID, _Tests.Rows(nTests)("labotd_TestID"))
    '    '                        _Flex.SetData(nStyleRow, COL_NAME, .Rows(nResult)("labotr_TestResultName"))
    '    '                        _Flex.SetData(nStyleRow, COL_RESULTNUMBER, .Rows(nResult)("labotr_TestResultNumber"))
    '    '                        _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
    '    '                        _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)
    '    '                        _Flex.SetData(nStyleRow, COL_VALUE, "Value") '"Value"
    '    '                        _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
    '    '                        _Flex.SetData(nStyleRow, COL_RANGE, "Range")
    '    '                        _Flex.SetData(nStyleRow, COL_RESULTTYPE, "ResultType")
    '    '                        _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
    '    '                        _Flex.SetData(nStyleRow, COL_ISFINISHED, .Rows(nResult)("labotr_IsFinished"))
    '    '                        _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
    '    '                        '---------------------------------------------------------

    '    '                        With _Flex.Rows(nStyleRow)
    '    '                            .ImageAndText = False
    '    '                            .Height = 22
    '    '                            .Style = _Flex.Styles("CS_Header")
    '    '                            .AllowEditing = False
    '    '                        End With
    '    '                        _Flex.SetCellStyle(nStyleRow, COL_SELECT, _Flex.Styles("CS_Record"))



    '    '                        'get the test result details
    '    '                        _ResultDetails = GetResultsDetailsByDate(_CurrentOrderID, _Tests.Rows(nTests)("labotd_TestID"), _Results.Rows(nResult)("labotr_TestResultNumber"))
    '    '                        '//---Add Test Result Header---Finish---//
    '    '                        For nResults As Int16 = 0 To _ResultDetails.Rows.Count - 1

    '    '                            With _ResultDetails

    '    '                                '//---Add Test Result Detail---Start---//
    '    '                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, .Rows(nResults)("labotrd_ResultName"), .Rows(nResults)("labotrd_TestResultNumber") & nResults, ImgResult.Image)
    '    '                                nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '    '                                '---------------------------------------------------------
    '    '                                _Flex.SetData(nStyleRow, COL_SELECT, "")
    '    '                                _Flex.SetData(nStyleRow, COL_NAME, .Rows(nResults)("labotrd_ResultName"))
    '    '                                _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
    '    '                                _Flex.SetData(nStyleRow, COL_TESTID, _Tests.Rows(nTests)("labotd_TestID"))
    '    '                                _Flex.SetData(nStyleRow, COL_RESULTNUMBER, .Rows(nResults)("labotrd_TestResultNumber"))
    '    '                                _Flex.SetData(nStyleRow, COL_RESULTLINENO, .Rows(nResults)("labotrd_ResultLineNo"))
    '    '                                _Flex.SetData(nStyleRow, COL_RESULTNAMEID, .Rows(nResults)("labotrd_ResultNameID"))
    '    '                                _Flex.SetData(nStyleRow, COL_VALUE, .Rows(nResults)("labotrd_ResultValue"))
    '    '                                _Flex.SetData(nStyleRow, COL_UNIT, .Rows(nResults)("labotrd_ResultUnit"))
    '    '                                _Flex.SetData(nStyleRow, COL_RANGE, .Rows(nResults)("labotrd_ResultRange"))
    '    '                                '_Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(.ResultTypeCode))
    '    '                                '_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.AbnormalFlagCode))

    '    '                                _Flex.SetData(nStyleRow, COL_ISFINISHED, .Rows(nResults)("labotrd_IsFinished"))
    '    '                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
    '    '                                '---------------------------------------------------------

    '    '                                'With _Flex.Rows(nStyleRow)
    '    '                                '    .ImageAndText = False
    '    '                                '    .Height = 22
    '    '                                '    If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_ABNORMAL_FLAG)) <> "N" Then
    '    '                                '        ' '' Not Normal 
    '    '                                '        .Style = _Flex.Styles("CS_NotNormal")
    '    '                                '    Else
    '    '                                '        .Style = _Flex.Styles("CS_Record")
    '    '                                '    End If

    '    '                                '    .AllowEditing = False
    '    '                                'End With

    '    '                                '//---Add Test Result Detail---Finish---//

    '    '                            End With 'With .TestResultDetails.Item(nResult)

    '    '                        Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1

    '    '                    End With ' With .OrderTestResults.Item(nResults)

    '    '                Next ' For nResults As Int16 = 0 To .OrderTestResults.Count - 1

    '    '                '// End If ' If _TransactionType = enumTransactionType.LabResult Then

    '    '            End With ' With .Item(nTest)
    '    '        End If ' If IsTestExists(OrderID, TestID, TestName) = False Then
    '    '    Next ' For nTest As Int16 = 0 To .Count - 1

    '    'Next


    '    If _Flex.Rows.Count >= 2 Then
    '        _Flex.Row = 1
    '        _Flex.Select(1, COL_SELECT)
    '    End If

    '    Return True
    'End Function

    'Private Function GetResultsDetailsByResult() As DataTable
    '    Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim dt As DataTable
    '    Try

    '        ' dt = New DataTable  'SLR: new not needed
    '        With odb

    '            Dim _strSql As String = ""
    '            If ChkPrior.Checked = False Then
    '                _strSql = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
    '                                & " FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID " _
    '                                & " AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber in( SELECT MAX(Lab_Order_Test_Result1.labotrd_TestResultNumber) FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_Result1 WHERE Lab_Order_Test_Result1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID AND Lab_Order_Test_Result1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID) " _
    '                                & " AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
    '                                & " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") " _
    '                                & " ORDER BY Lab_Order_Test_Result.labotr_TestResultDateTime , labotrd_ResultName"
    '            Else
    '                _strSql = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
    '                                & " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
    '                                & " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
    '                                & " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") " _
    '                                & " ORDER BY Lab_Order_Test_Result.labotr_TestResultDateTime , labotrd_ResultName"
    '            End If
    '            'sanjog
    '            'Dim _strSql As String = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
    '            '& " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
    '            '& " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
    '            '& " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange" _
    '            '& " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
    '            '& " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") " _
    '            '& " ORDER BY Lab_Order_Test_Result.labotr_TestResultDateTime , labotrd_ResultName"

    '            ' ''Fill Order Master 
    '            dt = .GetDataTable_Query(_strSql)

    '        End With
    '        Return dt
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(odb) Then
    '            odb.Dispose()
    '        End If
    '        odb = Nothing
    '    End Try
    'End Function

    Private Function GetResultsDetailsByResult(ByVal FromDate As Date, ByVal ToDate As Date, ByVal strsearchResult As String) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Try

            ' dt = New DataTable  'SLR: new not needed
            With odb

                Dim oPara As New gloEMRDatabase.DBParameter

                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = _PatientID
                oPara.Name = "@PatientID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = FromDate
                oPara.Name = "@FromDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = ToDate
                oPara.Name = "@ToDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = strsearchResult
                oPara.Name = "@TestName"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If ChkPrior.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@PriorCheck"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                If chkDate.Checked Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@DateCheck"
                .DBParametersCol.Add(oPara)


                dt = .GetDataTable("Lab_GetResultsDetailsByResult")

                oPara = Nothing

                'sanjog
                'Dim _strSql As String
                'If strsearchResult = "" Then

                '    If FromDate = DateTime.Now.Date And ToDate = DateTime.Now.Date Then
                '        _strSql = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange " _
                '                                            & " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
                '                                            & " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") " _
                '                                            & " ORDER BY labotrd_ResultName,labotr_TestResultDateTime"
                '    Else
                '        _strSql = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
                '                                            & " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange " _
                '                                            & " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
                '                                            & " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") and (convert(datetime,convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)) between '" & FromDate & "' and '" & ToDate & "') " _
                '                                            & " ORDER BY labotrd_ResultName,labotr_TestResultDateTime"
                '    End If


                'Else
                '    _strSql = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
                '& " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
                '& " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange,Lab_Order_Test_ResultDtl.labotrd_specificResultRefRange as labotrd_specificResultRefRange, " _
                '& " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
                '& " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
                '& " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") and (convert(datetime,convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101)) between '" & FromDate & "' and '" & ToDate & "') and (Lab_Order_Test_ResultDtl.labotrd_ResultName = '" & strsearchResult.Replace("'", "''") & "')" _
                '& " ORDER BY labotrd_ResultName,labotr_TestResultDateTime"


                'End If



                '' ''Fill Order Master 
                'dt = .GetDataTable_Query(_strSql)

            End With

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee odb, dbparametrer
            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    Private Function SetDataByResults(ByVal fromDate As Date, ByVal ToDate As Date) As Boolean
        Dim _ResultDetails As DataTable = Nothing  'single result collection slr new not needed 
        Try

            If _PatientID = 0 Then
                SetDataByResults = Nothing
                Exit Function

            End If



            'If IsFilterForResult Then
            '    _ResultDetails = GetResultsDetailsByResult(fromDate, ToDate, strSearchString)
            'Else
            '    _ResultDetails = GetResultsDetailsByResult(fromDate, ToDate, strSearchString)
            'End If

            _ResultDetails = GetResultsDetailsByResult(fromDate, ToDate, strSearchString)

            'loop through the Tests and get the orders against those tests by passing the individual testId

            If _ClearCombo = True Then
                cmbType.Items.Clear()
            End If

            For nResults As Int16 = 0 To _ResultDetails.Rows.Count - 1
                If IsFilterForResult = False And _ClearCombo = True Then

                    If cmbType.Items.Contains(_ResultDetails.Rows(nResults)("labotrd_ResultName")) = False Then
                        cmbType.Items.Add(_ResultDetails.Rows(nResults)("labotrd_ResultName"))
                    End If

                End If

                _Flex.Rows.Add()
                _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SELECT, _Flex.Styles("CS_CheckBox"))
                _Flex.SetData(_Flex.Rows.Count - 1, COL_SELECT, False)
                '_Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _ResultDetails.Rows(nResults)("ResDate") & "")
                If IsNothing(_ResultDetails.Rows(nResults)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, _ResultDetails.Rows(nResults)("labotrd_TestSpecimenCollectionDateTime") & "")
                End If

                _Flex.SetData(_Flex.Rows.Count - 1, COL_NAME, _ResultDetails.Rows(nResults)("labotrd_ResultName") & "")
                '_Flex.SetData(_Flex.Rows.Count - 1, COL_VALUE, _ResultDetails.Rows(nResults)("labotrd_ResultValue") & "") 'commneted by manoj on 20121127
                'start added by manoj on 20121127 for dispalying hyperlinks in result value
                If gloGlobal.gloLabGenral.IsResultisHyperLink(_ResultDetails.Rows(nResults)("labotrd_ResultValue")) Then
                    _Flex.Cols(COL_VALUE).Item(_Flex.Rows.Count - 1) = New Hyperlink(_ResultDetails.Rows(nResults)("labotrd_ResultValue"))
                Else
                    _Flex.SetData(_Flex.Rows.Count - 1, COL_VALUE, _ResultDetails.Rows(nResults)("labotrd_ResultValue") & "")
                End If
                'end of added by manoj on 20121127 for dispalying hyperlinks in result value
                _Flex.SetData(_Flex.Rows.Count - 1, COL_UNIT, _ResultDetails.Rows(nResults)("labotrd_ResultUnit") & "")
                _Flex.SetData(_Flex.Rows.Count - 1, COL_RANGE, _ResultDetails.Rows(nResults)("labotrd_ResultRange") & "")
                _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(_ResultDetails.Rows(nResults)("labotrd_ResultType")))

                'Mitesh
                If IsNothing(_ResultDetails.Rows(nResults)("ResDate")) = False Then
                    _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATE, _ResultDetails.Rows(nResults)("ResDate"))
                End If
                '----

                'Mitesh
                _Flex.SetData(_Flex.Rows.Count - 1, COL_RESULT_COMMENT, _ResultDetails.Rows(nResults)("labotrd_ResultComment"))
                _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, _ResultDetails.Rows(nResults)("labotrd_LOINCID"))
                '-----xx--

                ''By Abhijeet on 20101122 for showing result flag description according to settings.
                '' _Flex.SetData(_Flex.Rows.Count - 1, COL_FLAG, _AbnormalFlag_COL.GetDescription(_ResultDetails.Rows(nResults)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
                Dim strFlagList As String()
                Dim strFlagInDB As String = _ResultDetails.Rows(nResults)("labotrd_AbnormalFlag")
                strFlagList = strFlagInDB.Split(",")
                If strFlagList.Length > 1 Then
                    If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                    Else
                        _Flex.SetData(_Flex.Rows.Count - 1, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                    End If
                Else
                    _Flex.SetData(_Flex.Rows.Count - 1, COL_FLAG, _AbnormalFlag_COL.GetDescription(_ResultDetails.Rows(nResults)("labotrd_AbnormalFlag")))
                End If
                ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                _Flex.SetData(_Flex.Rows.Count - 1, COL_ReceiveDate, _ResultDetails.Rows(nResults)("ResDate") & "") '\\Added on 20090217 i.e. labotr_TestResultDateTime
                _Flex.SetData(_Flex.Rows.Count - 1, COL_PATIENT_RANGE, _ResultDetails.Rows(nResults)("labotrd_specificResultRefRange") & "") '\\Added on 20090217 i.e. labotr_TestResultDateTime

                '---------------------------------------------------------


                With _Flex.Rows(_Flex.Rows.Count - 1)
                    .ImageAndText = False
                    .Height = 22
                    'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                    'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                    'If _AbnormalFlag_COL.GetCode(_Flex.GetData(_Flex.Rows.Count - 1, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(_Flex.Rows.Count - 1, COL_FLAG))) Then
                    If _AbnormalFlag_COL.GetCode(_Flex.GetData(_Flex.Rows.Count - 1, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(_Flex.Rows.Count - 1, COL_FLAG))) Then
                        'Noraml and nothing
                        .Style = _Flex.Styles("CS_Record")
                    Else
                        .Style = _Flex.Styles("CS_NotNormal")
                        ' '' Not Normal 
                    End If

                    '.AllowEditing = False
                End With

            Next

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: FRee resultdetails
            If Not IsNothing(_ResultDetails) Then
                _ResultDetails.Dispose()
            End If
            _ResultDetails = Nothing
        End Try

    End Function

#End Region




    Private Function IsTestExists(ByVal OrderID As Int64, ByVal TestID As Int64, ByVal TestName As String) As Boolean
        Dim _Result As Boolean = False
        With _Flex
            For i As Integer = 1 To .Rows.Count - 1
                If .GetData(i, COL_RECORDTYPE) = enumRecordType.Test Then
                    If .GetData(i, COL_NAME) = TestName AndAlso .GetData(i, COL_TESTID) = TestID Then
                        _Result = True
                        Exit For
                    End If
                End If
            Next
        End With

        Return _Result
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


            .Styles.Fixed.BackColor = Color.FromArgb(81, 126, 211)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

            .Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Alternate.Border.Color = Color.FromArgb(159, 181, 221)

            .Styles.Normal.BackColor = Color.FromArgb(222, 231, 250)
            .Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Normal.Border.Color = Color.FromArgb(159, 181, 221)

            .Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.ForeColor = Color.Black


            Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Header")
            Try
                If (.Styles.Contains("CS_Header")) Then
                    csHeader = .Styles("CS_Header")
                Else
                    csHeader = .Styles.Add("CS_Header")
                    With csHeader
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .BackColor = Color.FromArgb(86, 126, 211)
                        .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csHeader = .Styles.Add("CS_Header")
                With csHeader
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .BackColor = Color.FromArgb(86, 126, 211)
                    .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
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
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csRecord = .Styles.Add("CS_Record")
                With csRecord
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.FromArgb(31, 73, 125)
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
                        .ForeColor = Color.FromArgb(31, 73, 125)
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
                    .ForeColor = Color.FromArgb(31, 73, 125)
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
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.GhostWhite
                        .ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                        .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csCheckBox = .Styles.Add("CS_CheckBox")
                With csCheckBox
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackColor = Color.GhostWhite
                    .ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                    .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
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

        'start added by manoj on 20121127 for applaying style ot hyperlinks
        Dim csNewLink As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("NewLink")
        Try
            If (_Flex.Styles.Contains("NewLink")) Then
                csNewLink = _Flex.Styles("NewLink")
            Else
                csNewLink = _Flex.Styles.Add("NewLink")
                csNewLink.Font = New Font(_Flex.Font, FontStyle.Underline)
                csNewLink.ForeColor = Color.Blue
            End If
        Catch ex As Exception
            csNewLink = _Flex.Styles.Add("NewLink")
              csNewLink.Font = New Font(_Flex.Font, FontStyle.Underline)
            csNewLink.ForeColor = Color.Blue

        End Try

      
        ' .BackColor = Color.GhostWhite

        Dim csOldLink As C1.Win.C1FlexGrid.CellStyle '= _Flex.Styles.Add("OldLink")
        Try
            If (_Flex.Styles.Contains("OldLink")) Then
                csOldLink = _Flex.Styles("OldLink")
            Else
                csOldLink = _Flex.Styles.Add("OldLink")
                csOldLink.Font = New Font(_Flex.Font, FontStyle.Underline)
                csOldLink.ForeColor = Color.Purple
                '.BackColor = Color.GhostWhite
            End If
        Catch ex As Exception
            csOldLink = _Flex.Styles.Add("OldLink")
            csOldLink.Font = New Font(_Flex.Font, FontStyle.Underline)
            csOldLink.ForeColor = Color.Purple
            '.BackColor = Color.GhostWhite

        End Try
     

        'end of added by manoj on 20121127 for applaying style ot hyperlinks
    End Sub

    Private Sub gloUC_Transaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        gloC1FlexStyle.Style(_Flex)

        Try

            _nUserId = Convert.ToInt64(appSettings("UserID"))
            Dim arrUserRights As ArrayList
            arrUserRights = clsinfobutton_Lab.GetUserRightsForEducationMaterial(_nUserId)

            If Not IsNothing(arrUserRights) Then
                isEducationMaterialEnables = arrUserRights(0)
                isAdvancedReference = arrUserRights(1)
            End If

            ''first fill the criteria combo with the DATE, TEST, RESULT criteria
            If cmbCriteria.Items.Count <= 0 Then
                cmbCriteria.Items.Add("Date")
                cmbCriteria.Items.Add("Test")
                cmbCriteria.Items.Add("Result")
                'cmbCriteria.Items.Add("Order")
                'cmbCriteria.SelectedIndex = 0
            End If

            ''  DesignTestGrid()

            Fill_AbnormalFlag()
            Fill_ResultType()

            'dtpFrom.Text = DateTime.Now.Date
            'dtpToDate.Text = DateTime.Now.Date
            'cmbType.SelectedValue = ""
            'cmbType.Items.Clear()
            'strSearchString = ""

            ''DesignTestGrid()
            'pnlFlexGrid.Visible = True
            'btnShowGraph.Visible = False
            'pnlselectType.Visible = False
            'IsFilterForDate = False
            'cmbCriteria.SelectedIndex = 0
            'SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)

            _Flex.Visible = True
            lblSearchColumnName.Text = "Date"

            If _blnFromSplitScreen Then
                cmbCriteria.Text = "Date"
                btnShowGraph.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Fill_AbnormalFlag()
        'SLR: Free _AbnormalFlag_COL before assigning new memory
        If Not IsNothing(_AbnormalFlag_COL) Then
            _AbnormalFlag_COL.Dispose()
        End If
        _AbnormalFlag_COL = Nothing
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
        'SLR: Free _ObservationStatus_COL before assigning new memory
        If Not IsNothing(_ObservationStatus_COL) Then
            _ObservationStatus_COL.Dispose()

        End If
        _ObservationStatus_COL = Nothing
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
        'Developer:Mitesh Patel
        'Date: 28 Jun 2012
        'Bug ID: 28817

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
    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        Try

            dtpFrom.Text = DateTime.Now.Date
            dtpToDate.Text = DateTime.Now.Date
            cmbType.Text = ""
            'cmbType.Items.Clear()
            strSearchString = ""
            _ClearCombo = True
            Select Case cmbCriteria.SelectedItem.ToString()
                Case "Date"
                    DesignTestGrid()
                    pnlFlexGrid.Visible = True
                    btnShowGraph.Visible = False
                    pnlselectType.Visible = False
                    IsFilterForDate = False
                    ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)

                    SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                    'Case "Order"
                    '    DesignTestGrid()
                    '    pnlFlexGrid.Visible = True
                    '    btnShowGraph.Visible = False
                    '    pnlselectType.Visible = False
                    '    IsFilterForDate = False
                    '    ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)

                    '    SetDataByOrderID(172683877260840034)
                Case "Test"
                    DesignTestGrid()
                    pnlFlexGrid.Visible = True
                    btnShowGraph.Visible = False
                    pnlselectType.Visible = True
                    With lblType
                        .Text = "Select Test :"
                        '.SendToBack()
                        '.BringToFront()
                    End With
                    IsFilterForTest = False
                    SetDataByTests(DateTime.Now.Date, DateTime.Now.Date)
                Case "Result"
                    DesignTestGrid(True)
                    pnlFlexGrid.Visible = True

                    'added by Amit to hide the showgraph button when opened it from split control in 7020 11/29/2012
                    If _blnFromSplitScreen Then
                        btnShowGraph.Visible = False
                    Else
                        btnShowGraph.Visible = True
                    End If

                    pnlselectType.Visible = True
                    With lblType
                        .Text = "Select Result :"
                        '.SendToBack()
                        '.BringToFront()
                    End With
                    IsFilterForResult = False
                    SetDataByResults(dtpFrom.Text, dtpToDate.Text)
            End Select
            _ClearCombo = False
            'If cmbCriteria.SelectedItem.ToString() = "Date" Then

            '    SetDataByDate()
            '    btnShowGraph.Visible = False
            'ElseIf cmbCriteria.SelectedItem.ToString() = "Test" Then
            '    DesignTestGrid()
            '    SetDataByTests()
            '    btnShowGraph.Visible = False
            'Else
            '    DesignTestGrid()
            '    SetDataByResults()
            '    btnShowGraph.Visible = True
            'End If
            ''Added on 20100119-Raise event after changing criteria
            RaiseEvent On_SearchCriteria_Changed()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCloseRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRefill.Click
        Try
            RaiseEvent btnCloseRefillClick(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearchResultGrdText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchResultGrdText.TextChanged
        Try
            Dim str As String = ""
            Dim rowid As Integer
            Select Case lblSearchColumnName.Text
                Case "Date"

                    str = txtSearchResultGrdText.Text
                    With _Flex

                        rowid = .FindRow(str, 1, .Col, False, False, True)
                        .Row = rowid
                    End With

                Case "Name"
                    str = txtSearchResultGrdText.Text
                    With _Flex

                        rowid = .FindRow(str, 1, .Col, False, False, True)
                        .Row = rowid
                    End With


                Case Else 'this is the case in which it will be invoked by default i.e on drug name
                    str = txtSearchResultGrdText.Text
                    With _Flex

                        rowid = .FindRow(str, 1, COL_DATE, False, False, True)
                        .Row = rowid
                    End With

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Developer:Sanjog Dhamke
    'Date: 20 Dec 2011 (6060)
    'Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
    'Reason:to get current selected OrderID

    Private Sub _Flex_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _Flex.AfterRowColChange
        Try
            If (_Flex.RowSel > 0) Then
                _OrderID = Convert.ToInt64(_Flex(_Flex.RowSel, COL_ORDERID))
            Else
                _OrderID = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _Flex_AfterScroll(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _Flex.AfterScroll

        If (e.NewRange.BottomRow = _Flex.Rows.Count - 1) And e.NewRange.BottomRow <> -1 And _isTestsLoaded = False Then
            If Not IsNothing(cmbCriteria.SelectedItem) Then
                If cmbCriteria.SelectedItem.ToString() = "Date" Then
                    _Flex.BeginUpdate()
                    Dim _ScrollPosition As Point
                    _ScrollPosition = _Flex.ScrollPosition
                    nrowcount = nrowcount + nrowcount
                    ''  SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                    _isscroll = True
                    ''SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
                    SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                    ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
                    _Flex.EndUpdate()
                    _isscroll = False
                End If
                '    Else
                '        If _MergeOrderID <> 0 Then
                '            _Flex.BeginUpdate()
                '            Dim _ScrollPosition As Point
                '            _ScrollPosition = _Flex.ScrollPosition
                '            nrowcount = nrowcount + nrowcount
                '            ''  SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                '            _isscroll = True
                '            ''SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
                '            SetDataByOrderIDSource(_MergeOrderID, _curOrderID)
                '            ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
                '            _Flex.EndUpdate()
                '            _isscroll = False
                '        End If
            End If

        End If
    End Sub



    Private Sub _Flex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            Dim i As Integer
            If _Flex.Rows.Count > 1 Then
                txtSearchResultGrdText.Text = ""
                i = _Flex.Col
                If i >= 0 Then
                    lblSearchColumnName.Text = CType((_Flex.GetData(0, i)), String)
                End If
                'lblSearchColumnName.Text = CType((_Flex.GetData(0, i)), String)
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' Retrive selected Result From Grid
    ''' </summary>
    ''' <remarks></remarks>

    Dim dt_SelectedResult As DataTable
    Dim r As DataRow

    Public Function SelectResult() As DataTable
        'SLR: before assigining new value, free exisitgng memeory if any
        If Not IsNothing(dt_SelectedResult) Then
            dt_SelectedResult.Dispose()
        End If
        dt_SelectedResult = Nothing
        dt_SelectedResult = New DataTable
        Dim clmnDate As New DataColumn
        With clmnDate
            .ColumnName = "Date"
        End With
        dt_SelectedResult.Columns.Add(clmnDate)

        Dim clmTestName As New DataColumn
        With clmTestName
            .ColumnName = "Test Name"
        End With
        dt_SelectedResult.Columns.Add(clmTestName)

        Dim clmResultName As New DataColumn
        With clmResultName
            .ColumnName = "ResultName"
        End With
        dt_SelectedResult.Columns.Add(clmResultName)

        Dim clmValue As New DataColumn
        With clmValue
            .ColumnName = "Value"
        End With
        dt_SelectedResult.Columns.Add(clmValue)

        Dim clmRange As New DataColumn
        With clmRange
            .ColumnName = "Range"
        End With
        dt_SelectedResult.Columns.Add(clmRange)
        Dim arrlistResult As New ArrayList
        Dim Result As String = ""

        With _Flex
            For i As Integer = 1 To .Rows.Count - 1
                If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    r = dt_SelectedResult.NewRow()
                    'Dim oNode As C1.Win.C1FlexGrid.Node = .Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)  '' , COL_NAME), C1.Win.C1FlexGrid.Node)
                    r.Item("Date") = .GetData(i, COL_DATE)
                    r.Item("Test Name") = "" 'oNode.Data  ''.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                    r.Item("ResultName") = .GetData(i, COL_NAME)
                    r.Item("Value") = .GetData(i, COL_VALUE)
                    r.Item("Range") = .GetData(i, COL_RANGE)
                    dt_SelectedResult.Rows.Add(r)
                End If
            Next
        End With




        If dt_SelectedResult Is Nothing Then
            Return Nothing
            Exit Function
        Else
            Return dt_SelectedResult
        End If



    End Function


    Private Sub dtpFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFrom.TextChanged
        _ClearCombo = True
        Select Case cmbCriteria.SelectedItem.ToString()
            Case "Date"
                '  DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = False
                pnlselectType.Visible = False
                IsFilterForDate = True
                SetDataByDate(dtpFrom.Text, dtpToDate.Text)
            Case "Test"
                DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = False
                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Test :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForTest = False
                strSearchString = cmbType.Text
                SetDataByTests(dtpFrom.Text, dtpToDate.Text)
            Case "Result"
                DesignTestGrid()
                pnlFlexGrid.Visible = True

                If _blnFromSplitScreen Then
                    btnShowGraph.Visible = False
                Else
                    btnShowGraph.Visible = True
                End If


                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Result :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForResult = False
                strSearchString = cmbType.Text
                SetDataByResults(dtpFrom.Text, dtpToDate.Text)
        End Select
        _ClearCombo = False
        ''Added on 20100119-Raise event after changing criteria
        RaiseEvent On_SearchCriteria_Changed()
    End Sub

    Private Sub dtpToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        _ClearCombo = True
        Select Case cmbCriteria.SelectedItem.ToString()
            Case "Date"
                DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = False
                pnlselectType.Visible = False
                IsFilterForDate = True
                SetDataByDate(dtpFrom.Text, dtpToDate.Text)
            Case "Test"
                DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = False
                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Test :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForTest = False
                strSearchString = cmbType.Text
                SetDataByTests(dtpFrom.Text, dtpToDate.Text)
            Case "Result"
                DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = True
                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Result :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForResult = False
                strSearchString = cmbType.Text
                SetDataByResults(dtpFrom.Text, dtpToDate.Text)
        End Select
        _ClearCombo = False
        ''Added on 20100119-Raise event after changing criteria
        RaiseEvent On_SearchCriteria_Changed()
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged

        Select Case cmbCriteria.SelectedItem.ToString()
            Case "Test"
                DesignTestGrid()
                pnlFlexGrid.Visible = True
                btnShowGraph.Visible = False
                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Test :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForTest = False
                strSearchString = cmbType.Text
                SetDataByTests(dtpFrom.Text, dtpToDate.Text)
            Case "Result"
                DesignTestGrid()
                pnlFlexGrid.Visible = True

                If _blnFromSplitScreen Then
                    btnShowGraph.Visible = False
                Else
                    btnShowGraph.Visible = True
                End If

                pnlselectType.Visible = True
                With lblType
                    .Text = "Select Result :"
                    '.SendToBack()
                    '.BringToFront()
                End With
                IsFilterForResult = False
                strSearchString = cmbType.Text
                SetDataByResults(dtpFrom.Text, dtpToDate.Text)
        End Select
    End Sub

    Private Sub cmbType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.SelectionChangeCommitted

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        dtpFrom.Text = DateTime.Now.Date
        dtpToDate.Text = DateTime.Now.Date
        cmbType.Text = ""
        'cmbType.Items.Clear()
        strSearchString = ""
        cmbCriteria.Text = "Date"
        DesignTestGrid()
        pnlFlexGrid.Visible = True
        btnShowGraph.Visible = False
        pnlselectType.Visible = False
        IsFilterForDate = False
        SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
    End Sub

    Private Sub btnCloseRefill_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseRefill.MouseEnter
        ToolTip1.SetToolTip(btnCloseRefill, "Close")

        'ToolTip1.ToolTipTitle = "Close"
    End Sub

    Private Sub btnCloseRefill_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseRefill.MouseLeave
        ToolTip1.RemoveAll()
        ToolTip1.ToolTipTitle = ""
    End Sub

    Private Sub btnShowGraph_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowGraph.MouseEnter
        ToolTip1.SetToolTip(btnShowGraph, "Show Graph")
    End Sub

    Private Sub btnShowGraph_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowGraph.MouseLeave
        ToolTip1.RemoveAll()
        ToolTip1.ToolTipTitle = ""
    End Sub

    Private Sub btnRefresh_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.MouseEnter
        ToolTip1.SetToolTip(btnRefresh, "Refresh")
    End Sub

    Private Sub btnRefresh_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.MouseLeave
        ToolTip1.RemoveAll()
        ToolTip1.ToolTipTitle = ""
    End Sub

    Private Sub _Flex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.DoubleClick
        Try
            Dim OrderDate As DateTime
            With _Flex
                If (.Row > 0) Then
                    If (_Flex.GetData(_Flex.Row, COL_ORDERID) > 0) Then
                        Dim OrderID As Int64 = .GetData(.Row, COL_ORDERID)
                        Dim VisitID As Int64 = .GetData(.Row, COL_VISITID)
                        ''Added by Mayuri:20101116-instead of sending result date sending order trnasaction date
                        ''because in COL_DATE result date is Not coming at.Row position
                        'Dim OrderDate As DateTime = .GetData(.Row, COL_DATE)
                        If IsNothing(.GetData(.Row, COL_DATE)) = True Then
                            If .GetData(.Row, COL_DATE) = "" Then
                                OrderDate = Nothing
                            Else
                                OrderDate = .GetData(1, COL_DATE)
                            End If
                        Else
                            If .GetData(.Row, COL_DATE) = "" Then
                                OrderDate = Nothing
                            Else
                                OrderDate = .GetData(.Row, COL_DATE)
                            End If

                        End If


                        RaiseEvent On_Flex_DoubleClick(_PatientID, OrderID, VisitID, OrderDate)
                    End If
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub btnShowGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowGraph.Click
        Try
            btnShowGraph.Enabled = False
            RaiseEvent btnShowGraphClick(sender, e)
        Catch ex As Exception
            Throw ex
        Finally
            btnShowGraph.Enabled = True
        End Try
    End Sub


    Private Sub _Flex_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterEdit
        ''written this event code by Abhijeet & Madan on 20100521
        '' to remove error of com Exception and checking value is numeric for plotting graph
        Try
            If Not IsNothing(cmbCriteria.SelectedItem) Then
                If cmbCriteria.SelectedItem.ToString() = "Result" Then
                    With _Flex
                        If (.Row > 0) Then
                            If (_Flex.GetData(.Row, COL_SELECT)) Then
                                If (Not IsNumeric(.GetData(.Row, COL_VALUE))) Then
                                    MessageBox.Show("Selected result value is not numeric, cannot be selected for graph.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    .SetCellCheck(.Row, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If
                            Else
                            End If
                        End If
                    End With
                End If
            End If

            If e.Row > 0 Then
                If e.Col = COL_SourceCombo Then

                    Dim ostyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                    Dim _testID As String = String.Empty
                    Dim _dataMapValue As String = String.Empty
                    Dim _bool As Boolean = False

                    If Not IsNothing(_Flex.GetCellStyle(e.Row, COL_SourceCombo)) Then
                        ostyle = _Flex.GetCellStyle(e.Row, COL_SourceCombo)

                        _testID = _Flex.GetData(e.Row, COL_TESTID).ToString()

                        If ostyle.DataMap IsNot Nothing Then
                            _dataMapValue = _Flex(e.Row, COL_SourceCombo).ToString()
                            If _dataMapValue <> String.Empty Then
                                If Convert.ToInt64(_dataMapValue) > 1 Then
                                    If _testID <> _dataMapValue Then
                                        _bool = checkDuplicate(_dataMapValue)
                                        If _bool = False Then
                                            _Flex.SetData(e.Row, COL_SourceCombo, "0")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub _Flex_AfterSelChange(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _Flex.AfterSelChange
        ''Added code for this event by Abhijeet on 20100917 for showing Result comment at end of control
        ''This changes done for CCHIT certification requirement
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
                    'RaiseEvent gUC_TestSelected(_tmpTestID, _Flex.GetData(_tmpDataRow, COL_TEST_SPECIMEN) & "", _Flex.GetData(_tmpDataRow, COL_TEST_COLLECTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_STORAGE) & "", _Flex.GetData(_tmpDataRow, COL_TEST_LOINC) & "", _Flex.GetData(_tmpDataRow, COL_TEST_INSTRUCTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_PRECAUTION) & "", _Flex.GetData(_tmpDataRow, COL_TEST_COMMENTS) & "")
                    'End If
                End If
                If IsNothing(_Flex.GetData(_Flex.Row, COL_RESULT_COMMENT)) = False Then
                    'Developer name : Mitesh Patel
                    'Case No : Incident #00002197 
                    ' If _Flex.GetData(_Flex.Row, COL_RESULT_COMMENT).ToString() <> "" And Val(_Flex.GetData(_Flex.Row, COL_RECORDTYPE)) = enumRecordType.Result Then
                    If _Flex.GetData(_Flex.Row, COL_RESULT_COMMENT).ToString() <> "" Then
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

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        'start added by manoj on 20121127 for dispalying hyperlinks in result value
        Try
            ht = _Flex.HitTest(e.X, e.Y)
            If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf _Flex(ht.Row, ht.Column) Is Hyperlink Then
                Cursor = Cursors.Hand
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
        'end of added by manoj on 20121127 for dispalying hyperlinks in result value
    End Sub

    Private Sub ChkPrior_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkPrior.CheckedChanged
        dtpFrom_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub chkDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDate.CheckedChanged
        If chkDate.Checked = True Then
            dtpFrom.Enabled = True
            dtpToDate.Enabled = True
        Else
            dtpFrom.Enabled = False
            dtpToDate.Enabled = False
        End If
        dtpFrom_TextChanged(Nothing, Nothing)
    End Sub
    Public Sub ArrangeDesign()
        With _Flex
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            Dim _TotalWidth As Single = .Width - 5
            .Cols(COL_PATIENT_RANGE).Width = _TotalWidth * 0.13
            .Cols(COL_PATIENT_RANGE).AllowEditing = False
            .Cols(COL_SELECT).Width = _TotalWidth * 0.05 ' 50

            If _blnFromSplitScreen Then
                .Cols(COL_SELECT).AllowEditing = False
            Else
                .Cols(COL_SELECT).AllowEditing = True
            End If


            .Cols(COL_DATE).Width = _TotalWidth * 0.15
            .Cols(COL_DATE).AllowEditing = False

            .Cols(COL_REPORTED_DATE).Width = _TotalWidth * 0.15
            .Cols(COL_REPORTED_DATE).AllowEditing = False

            .Cols(COL_ORDERID).Width = 0
            .Cols(COL_TESTID).Width = 0
            .Cols(COL_RESULTNUMBER).Width = 0
            .Cols(COL_RESULTLINENO).Width = 0
            .Cols(COL_RESULTNAMEID).Width = 0
            .Cols(COL_NAME).Width = _TotalWidth * 0.23 ' 200
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_VALUE).Width = _TotalWidth * 0.09 '100
            .Cols(COL_VALUE).AllowEditing = False
            .Cols(COL_UNIT).Width = _TotalWidth * 0.06 '100
            .Cols(COL_UNIT).AllowEditing = False
            .Cols(COL_RANGE).Width = _TotalWidth * 0.15 '100
            .Cols(COL_RANGE).AllowEditing = False
            .Cols(COL_FLAG).Width = _TotalWidth * 0.16 '100
            .Cols(COL_FLAG).AllowEditing = False
            .Cols(COL_RESULTTYPE).Width = _TotalWidth * 0.1
            .Cols(COL_RESULTTYPE).AllowEditing = False
            .Cols(COL_ISFINISHED).Width = 0
            .Cols(COL_RECORDTYPE).Width = 0
            .Cols(COL_VISITID).Width = 0
            .Cols(COL_ReceiveDate).Width = _TotalWidth * 0.15 '20090217
            .Cols(COL_ReceiveDate).AllowEditing = False
            .Cols(COL_ResultLoincCode).Width = _TotalWidth * 0.08 '20090217
            .Cols(COL_ResultLoincCode).AllowEditing = False
            .Cols(COL_TESTNAME).AllowEditing = False
            .Cols(COL_TESTNAME).Width = 0


            .Cols(COL_SELECT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            ''Added by Abhijeet on 20101026
            .Cols(COL_TESTSTATUS).Width = 120
            .Cols(COL_TESTTYPE).Width = 120
            .Cols(COL_SPECIMENSOURCE).Width = 150
            .Cols(COL_SPECIMENCONDITIONDISP).Width = 0
            .Cols(COL_LABFACILITYNAME).Width = 0
            .Cols(COL_LABFACILITYSTREETADDRESS).Width = 0
            .Cols(COL_LABFACILTYCITY).Width = 0
            .Cols(COL_LABFACILITYSTATE).Width = 0
            .Cols(COL_LABFACILITYZIPCODE).Width = 0
            .Cols(COL_RESULT_COMMENT).Width = 0


            ' .Cols(COL_SourceCombo).Width = 0

        End With
    End Sub

    Private Sub gloUC_TransactionHistory_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If (_forMerge = False) Then
            ArrangeDesign()
        End If

    End Sub


    Private Sub _flex_OwnerDrawCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles _Flex.OwnerDrawCell
        Try
            If Not _Flex(e.Row, e.Col) Is Nothing AndAlso TypeOf _Flex(e.Row, e.Col) Is Hyperlink Then
                If _Flex(e.Row, e.Col).Visited Then
                    e.Style = _Flex.Styles("OldLink")
                Else
                    e.Style = _Flex.Styles("NewLink")
                End If
            End If

            If e.Col = COL_SourceCombo Then

                Dim testID As Long = Convert.ToString(_Flex.GetData(e.Row, COL_TESTID))
                Dim ostyle As C1.Win.C1FlexGrid.CellStyle

                If Not IsNothing(_Flex.GetCellStyle(e.Row, COL_SourceCombo)) Then
                    ostyle = _Flex.GetCellStyle(e.Row, COL_SourceCombo)

                    If ostyle.DataMap IsNot Nothing Then
                        Dim Map As MultiColumnDictionary = ostyle.DataMap
                        Dim myEnumerator As IDictionaryEnumerator = Map.GetEnumerator()

                        While myEnumerator.MoveNext()
                            Dim s As Long
                            s = Convert.ToInt64(myEnumerator.Key)
                            If testID = s Then
                                e.Text = myEnumerator.Value
                                _Flex.SetData(e.Row, COL_SourceCombo, testID.ToString())
                                Exit While
                                'ElseIf e.Text = "" Then
                                '    e.Text = "No Match Found"
                                '    _Flex.SetData(e.Row, COL_SourceCombo, "1")
                            End If

                        End While

                    End If


                End If
            End If

        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Dim ht As C1.Win.C1FlexGrid.HitTestInfo = Nothing 'added by manoj on 20121127 for dispalying hyperlinks in result value

    'start added by manoj on 20121127 for dispalying hyperlinks in result value
    Private Sub _Flex_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
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

            _Flex.ContextMenuStrip = Nothing
            If Cursor Is Cursors.Hand Then
                ht = _Flex.HitTest(e.X, e.Y)
                If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf _Flex(ht.Row, ht.Column) Is Hyperlink Then
                    _Flex(_Flex.HitTest(e.X, e.Y).Row, _Flex.HitTest(e.X, e.Y).Column).Activate()
                End If
            End If
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
                '_Flex.Select(_tmpDataRow, True)

                If _tmpDataRow > 0 Then

                    If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test OrElse Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.None Then
                        'Test - Add Result
                        If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                            Dim oContxMenu As New ContextMenuStrip
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
                            Dim nDocList As ArrayList = Nothing

                            ''    If Val(_Flex.GetData(_tmpDataRow, COL_DMSID)) > 0 Then
                            If (Me.ParentForm.Name.ToString() = "MainMenu") Then ''This if condition is to add context menu only if it is called from dashboard
                                oMnuSubItem = New ToolStripMenuItem ''added for bugid 70345
                                oMnuSubItem.Text = "Generate CDA"
                                oMnuSubItem.Image = ImgContextMenu.Images("Generate CDA.ico")
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf OpenCDAClick
                                oMnuSubItem = Nothing
                                oMnuSubItem = New ToolStripMenuItem
                                oMnuSubItem.Text = "View Order"
                                oMnuSubItem.Tag = "View Order"
                                oMnuSubItem.Image = ImgContextMenu.Images("View Order.ico")
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf ViewOrderClick
                                oMnuSubItem = Nothing
                                oMnuSubItem = New ToolStripMenuItem
                                oMnuSubItem.Text = "Modify Order"
                                oMnuSubItem.Tag = "Modify Order"
                                oMnuSubItem.Image = ImgContextMenu.Images("Modify Order.ico")
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf ViewOrderClick
                                oMnuSubItem = Nothing

                            End If

                            Dim dtDoc As DataTable = Get_DocumentDetails(_PatientID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_ORDERID)), Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)), "")



                            Dim i As Integer = 0
                            If Not IsNothing(dtDoc) Then
                                If dtDoc.Rows.Count > 0 Then
                                    If Not IsNothing(nDocList) Then
                                        nDocList.Clear()
                                    End If

                                    oMnuItem = New ToolStripMenuItem
                                    oMnuItem.Text = "View Result Document" : oMnuItem.ShortcutKeys = Shortcut.CtrlShiftV : oMnuItem.ShowShortcutKeys = False
                                    oMnuItem.Image = ImgContextMenu.Images("View Result Document.ico")
                                    oContxMenu.Items.Add(oMnuItem)

                                    nDocList = New ArrayList
                                    For i = 0 To dtDoc.Rows.Count - 1
                                        oMnuSubItem = New ToolStripMenuItem
                                        oMnuSubItem.Text = dtDoc.Rows(i)("DocumentName")
                                        oMnuSubItem.Tag = dtDoc.Rows(i)("DocumentID")
                                        oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")

                                        oMnuItem.DropDownItems.Add(oMnuSubItem)
                                        AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewDocument
                                        oMnuSubItem = Nothing

                                        nDocList.Add(dtDoc.Rows(i)("DocumentID"))

                                    Next

                                End If
                            End If
                            'nDocCount = oMnuItemDMS.MenuItems.Count()

                            oMnuItemDMS = Nothing
                            oMnuItem = Nothing
                            If Not IsNothing(dtDoc) Then
                                dtDoc.Dispose()
                                dtDoc = Nothing
                            End If

                            Dim dtURLDoc As DataTable = Nothing
                            If IsNothing(dtURLDoc) = True Then
                                Dim _ordid As Int64 = Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_ORDERID))
                                dtURLDoc = Lab_GetURLDocument(_ordid, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)))
                                'dtURLDoc = Lab_GetURLDocument(_OrderID, Convert.ToInt64(_Flex.GetData(_tmpDataRow, COL_TESTID)))
                            End If

                            If Not IsNothing(dtURLDoc) Then

                                If (dtURLDoc.Rows.Count > 0) Then
                                    Dim oMnuItemNewURLdoc As New ToolStripMenuItem
                                    oMnuItemNewURLdoc = New ToolStripMenuItem
                                    oMnuItemNewURLdoc.Text = "View Result URL Document"
                                    oMnuItemNewURLdoc.ShortcutKeys = Shortcut.CtrlShiftV
                                    oMnuItemNewURLdoc.ShowShortcutKeys = False
                                    oMnuItemNewURLdoc.Image = ImgContextMenu.Images("View Result URL Doc.ico")

                                    oContxMenu.Items.Add(oMnuItemNewURLdoc)

                                    For i = 0 To dtURLDoc.Rows.Count - 1
                                        'New URl document
                                        oMnuSubItem = New ToolStripMenuItem
                                        oMnuSubItem.Text = dtURLDoc.Rows(i)("URLDisplayName")
                                        oMnuSubItem.Tag = dtURLDoc.Rows(i)("URLName") + "||" + Convert.ToString(dtURLDoc.Rows(i)("labotrda_TestID"))
                                        oMnuSubItem.Image = ImgContextMenu.Images("Bullet06.ico")

                                        oMnuItemNewURLdoc.DropDownItems.Add(oMnuSubItem)
                                        AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument
                                        oMnuSubItem = Nothing
                                    Next i
                                    oContxMenu.Items.Add(oMnuItemNewURLdoc)
                                    ' oMnuItemNewURLdoc.Dispose()
                                    oMnuItemNewURLdoc = Nothing
                                End If
                                dtURLDoc.Dispose()
                                dtURLDoc = Nothing
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
                            _Flex.ContextMenuStrip = oContxMenu
                            If (Me.ParentForm.Name.ToString() = "MainMenu") Then ''This if condition is to add context menu only if it is called from dashboard
                                If cmbCriteria.SelectedItem.ToString() <> "Result" Then
                                    Dim _nRow As Integer = _Flex.HitTest(e.X, e.Y).Row
                                    _Flex.Row = _nRow
                                    If _nRow > 0 Then
                                        If (_Flex.ContextMenuStrip Is Nothing) Then
                                            Dim cmnuOrderDetails As ContextMenuStrip = New ContextMenuStrip
                                            Dim oOrderMenuItem As ToolStripMenuItem = New ToolStripMenuItem
                                            oOrderMenuItem.Text = "Generate CDA"
                                            oOrderMenuItem.Image = ImgContextMenu.Images("Generate CDA.ico")
                                            AddHandler oOrderMenuItem.Click, AddressOf OpenCDAClick
                                            cmnuOrderDetails.Items.Add(oOrderMenuItem)
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
                                            _Flex.ContextMenuStrip = cmnuOrderDetails
                                        End If
                                    End If
                                End If
                            End If


                        End If

                        ''End If
                    Else
                        Try


                            If (Me.ParentForm.Name.ToString() = "MainMenu") Then

                                _Flex.Select(_tmpDataRow, 0)
                                Dim oMnuSubItem As ToolStripMenuItem  ''added for bugid 70345
                                oMnuSubItem = New ToolStripMenuItem
                                oMnuSubItem.Text = "Generate CDA"
                                oMnuSubItem.Image = ImgContextMenu.Images("Generate CDA.ico")
                                Dim oContxMenu As New ContextMenuStrip
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf OpenCDAClick
                                oMnuSubItem = Nothing

                                oMnuSubItem = New ToolStripMenuItem
                                oMnuSubItem.Text = "View Order"
                                oMnuSubItem.Tag = "View Order"
                                oMnuSubItem.Image = ImgContextMenu.Images("View Order.ico")
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf ViewOrderClick
                                oMnuSubItem = Nothing
                                oMnuSubItem = New ToolStripMenuItem
                                oMnuSubItem.Text = "Modify Order"
                                oMnuSubItem.Tag = "Modify Order"
                                oMnuSubItem.Image = ImgContextMenu.Images("Modify Order.ico")
                                oContxMenu.Items.Add(oMnuSubItem)
                                AddHandler oMnuSubItem.Click, AddressOf ViewOrderClick
                                oMnuSubItem = Nothing
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
                                _Flex.ContextMenuStrip = oContxMenu

                            End If
                        Catch ex As Exception

                        End Try
                    End If
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

                    If _tmpDataColumn = COL_Lab_InfoButton Then


                        'If _CurrentOrderID > 0 Then
                        '    RaiseEvent LockOrder(_CurrentOrderID)
                        'End If

                        'If _IsOrderLocked Then
                        '    Return
                        'End If


                        Dim _tmpTestResultID As Int64 = 0
                        Dim _tmpDataRow As Int16 = _Flex.HitTest(e.X, e.Y).Row '_Flex.Row
                        _Flex.Select(_tmpDataRow, True)

                        ''Infobutton
                        Dim dtEdu As DataTable
                        Dim dtPatient As DataTable = clsinfobutton_Lab.GetPatientInfo(_PatientID)
                        Dim strPatientAge As Integer
                        Dim strPatientGender As String = ""
                        If dtPatient.Rows.Count > 0 Then
                            strPatientAge = calculateAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                            strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                        End If
                        dtEdu = clsinfobutton_Lab.GetEducationMaterial(Convert.ToString(_Flex.GetData(_tmpDataRow, COL_ResultLoincCode)), "2.16.840.1.113883.6.1", strPatientAge, strPatientGender)

                        If Not IsNothing(dtEdu) Then
                            If dtEdu.Rows.Count > 0 Then
                                If _tmpDataRow > 0 Then
                                    If Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Test Then
                                        If Val(_Flex.GetData(_tmpDataRow, COL_TESTID)) > 0 Then
                                            'Menu Object
                                            Dim oContxMenu As New ContextMenuStrip
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

                                                        'Dim oContxMenu As New ContextMenuStrip
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
                                            _Flex.ContextMenuStrip = oContxMenu

                                            _CurrentRow = _tmpDataRow
                                        End If
                                    ElseIf Val(_Flex.GetData(_tmpDataRow, COL_RECORDTYPE)) = enumRecordType.Result Then
                                        If Val(_Flex.GetData(_tmpDataRow, COL_RESULTNUMBER)) > 0 Then
                                            Dim oContxMenu As New ContextMenuStrip
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
                                                                ''
                                                                ''If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) >= Convert.ToDouble(dtEdu.Rows(i)("nValueOne")) And Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_VALUE))) >= Convert.ToDouble(dtEdu.Rows(i)("nValueOne")) And Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 2 Then ' "Equal"
                                                                ''If Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) = Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) Then
                                                                If Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) = Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_VALUE))) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 3 Then '"Greater than"
                                                                ''If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) > Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_VALUE))) > Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                    bAddMenu = True
                                                                End If
                                                            ElseIf Convert.ToInt16(dtEdu.Rows(i)("nOperator")) = 4 Then ' "Less than"
                                                                'If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
                                                                If Convert.ToDouble(Val(_Flex.GetData(_tmpDataRow, COL_VALUE))) < Convert.ToDouble(dtEdu.Rows(i)("nValueTwo")) Then
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
                                            _Flex.ContextMenuStrip = oContxMenu
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
                        Else
                            'Open Online Document
                            OpenOnlineDocument()
                        End If
                        'SLR: FRee dtedu, dtpatient
                        If Not IsNothing(dtEdu) Then
                            dtEdu.Dispose()
                        End If
                        dtEdu = Nothing

                        If Not IsNothing(dtPatient) Then
                            dtPatient.Dispose()
                        End If
                        dtPatient = Nothing


                    End If
                End If






            End If




        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
    End Sub
    Private Sub Set_Menu_ViewURLDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim strurl As String()
            Dim mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            If (Not IsNothing(mnu.Tag)) Then
                strurl = mnu.Tag.ToString().Split("||")
                If (strurl.Length > 0) Then
                    gloGlobal.gloLabGenral.RunInSystemDefaultBrowser(strurl(0))  ''made change against bugid 65334:
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Lab_GetURLDocument(ByVal OrdID As Int64, ByVal TestID As Int64) As DataTable

        Dim dtURLdocument As DataTable = Nothing

        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrdID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestID
                oPara.Name = "@TestID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing



                dtURLdocument = .GetDataTable("Lab_GetURLDocument")

            End With
            Return dtURLdocument
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb, dbparameteds
            If Not IsNothing(odb) Then
                odb.Dispose()

            End If
            odb = Nothing


            If Not IsNothing(dtURLdocument) Then
                dtURLdocument.Dispose()
                dtURLdocument = Nothing
            End If

        End Try








    End Function

    Private Sub OpenOnlineDocument()
        Try
            ''Infobutton
            If Cursor Is Cursors.Default Then

                'If _tmpDataCol = COL_LAB_INFOBUTTON Then
                ''Infobutton
                If Val(_Flex.GetData(_Flex.RowSel, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                    Dim TestLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_ResultLoincCode))
                    If TestLCode.Trim() = "" Then
                        MessageBox.Show("LOINC code is not available for selected test", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        'If _ParentControl = "" Then
                        RaiseEvent gUC_InfoButtonClickedDB(TestLCode)
                        'Else


                        '    RaiseEvent gUC_InfoButtonClicked(TestLCode)
                        'End If
                    End If
                ElseIf Val(_Flex.GetData(_Flex.RowSel, COL_RECORDTYPE) & "") = Val(enumRecordType.Result) Then
                    'Dim TestLCode As String = ""
                    'For i As Integer = _tmpDataRow To 0 Step -1
                    '    If Val(_Flex.GetData(i, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) Then
                    '        TestLCode = Convert.ToString(_Flex.GetData(_CurrentRow, COL_TEST_LOINC))
                    '        Exit For
                    '    End If
                    'Next
                    Dim ResultLCode As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_ResultLoincCode))
                    'Dim ResultValue = Convert.ToString(_Flex.GetData(_tmpDataRow, COL_TEST_DIGNOSIS_RESULT_VALUE))
                    'Dim ResultValueUnit = Convert.ToString(_Flex.GetData(_tmpDataRow, COL_TEST_CPT_RESULT_UNIT))
                    If ResultLCode = "" Then
                        MessageBox.Show("LOINC code is not available for selected result", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        'If _ParentControl = "" Then
                        RaiseEvent gUC_InfoButtonClickedDB(ResultLCode)
                        'Else


                        '    RaiseEvent gUC_InfoButtonClicked(ResultLCode)
                        'End If
                    End If
                End If
                'End If
            End If
        Catch ex As Exception

        End Try

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
            'If _ParentControl = "" Then
            RaiseEvent gUC_InfoButtonDocumentClickedDB(nTempId, OpenFor, templateName, tag(1))
            'Else
            'RaiseEvent gUC_InfoButtonDocumentClicked(nTempId, OpenFor, templateName)
            'End If

        Catch ex As Exception

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
    Private Sub ViewOrderClick(ByVal sender As Object, ByVal e As System.EventArgs) ''''added to open order from dashboard on right clicking order result grid
        Try
            With _Flex
                If (_Flex.GetData(_Flex.Row, COL_ORDERID) > 0) Then
                    If _CurrentRow > 0 Then
                        Dim OrderID As Int64 = .GetData(_Flex.Row, COL_ORDERID)

                        RaiseEvent EvntViewOrderHandler((CType(sender, ToolStripMenuItem)).Tag.ToString(), OrderID)
                    Else
                        RaiseEvent EvntViewOrderHandler("", 0)
                    End If

                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub OpenCDAClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            With _Flex
                If (_Flex.GetData(_Flex.Row, COL_ORDERID) > 0) Then
                    If _CurrentRow > 0 Then
                        Dim OrderID As Int64 = .GetData(_Flex.Row, COL_ORDERID)
                        RaiseEvent EvntGenerateCDAHandler(OrderID, _CurrentRow)
                    Else
                        RaiseEvent EvntGenerateCDAHandler(0, 0)
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
                    If Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.Test) OrElse Val(.GetData(_CurrentRow, COL_RECORDTYPE) & "") = Val(enumRecordType.None) Then
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


    'start added by manoj on 20121127 for dispalying hyperlinks in result value
    Private Sub txtTestResultComment_LinkClicked(sender As Object, e As System.Windows.Forms.LinkClickedEventArgs) Handles txtTestResultComment.LinkClicked
        Try
            If Not String.IsNullOrEmpty(e.LinkText.Trim()) Then
                gloGlobal.gloLabGenral.OpenLinkInBrowser(e.LinkText.Trim())
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub pnlSelectCriteria_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlSelectCriteria.MouseDown
        If (DirectCast(e, System.Windows.Forms.MouseEventArgs).Button = Windows.Forms.MouseButtons.Right) Then
            RaiseEvent EvntGenerateCDAHandler(0, 0)
        End If
    End Sub

#Region "Orders"

    Public Sub HidePanel(ByVal sender As Object, ByVal e As System.EventArgs)
        pnlSelectCriteria.Visible = False
        pnlSelectCriteria1.Visible = False
    End Sub

    Public Function SetDataByOrderID(ByVal OrderID As Long) As Boolean
        'RemoveHandler cmbType.SelectedIndexChanged, AddressOf cmbType_SelectedIndexChanged
        'RemoveHandler cmbCriteria.SelectedIndexChanged, AddressOf cmbCriteria_SelectedIndexChanged
        pnlSelectCriteria.Visible = False
        pnlSelectCriteria1.Visible = False
        'dtpFrom.Text = DateTime.Now.Date
        'dtpToDate.Text = DateTime.Now.Date
        'cmbType.Text = ""
        ''cmbType.Items.Clear()
        'strSearchString = ""
        '_ClearCombo = True
        pnlFlexGrid.Visible = True
        'btnShowGraph.Visible = False
        'pnlselectType.Visible = False
        'IsFilterForDate = False

        If IsNothing(_ObservationStatus_COL) Then
            Fill_ResultType()
        End If
        If IsNothing(_AbnormalFlag_COL) Then
            Fill_AbnormalFlag()
        End If

        ' If _PatientID = 0 Then Exit Function

        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        Dim _FillOrderID As Int64 = 0
        'Dim _Orders As New DataTable() 'multiple orders collection 
        'Dim _Order As New DataTable() 'single order collection
        'Dim _Tests As New DataTable() 'multiple tests collection
        'Dim _Test As New DataTable() 'single test collection
        'Dim _Results As New DataTable() 'multiple results collection
        'Dim _ResultDetails As New DataTable() 'single result collection
        Dim dvResults As DataView = Nothing ''Slr new not needed
        Try



            _isTestsLoaded = False


            dsresults = GetResultsByOrder(OrderID, nrowcount)
            'SLR: else aloocate new dtaview
            If IsNothing(dsresults) = False Then
                dvResults = dsresults.Tables(0).DefaultView
            Else
                dvResults = New DataView
            End If

            DesignTestGrid(True)

            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then


                    If (nrowcount > dvResults.Count) Then
                        _isTestsLoaded = True
                    End If
                    If (nrowcount < 35) Then
                        nrowcount = 35
                    Else
                        nrowcount = dvResults.Count
                        If (nrowcount < 35) Then
                            nrowcount = 35
                        End If
                    End If
                End If
            End If
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then

                    For nOrders As Int16 = 0 To dvResults.Table.Rows.Count - 1

                        'remove the current order from the Orders table
                        _CurrentOrderID = dvResults.Table.Rows(nOrders)("labom_OrderID")

                        _CurrentVisitID = dvResults.Table.Rows(nOrders)("labom_VisitID")


                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID")) OrElse _FillOrderID <> Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestName"))
                            With dvResults.Table  '.Item(nTest)
                                '//---Add Test---Start---//
                                Dim strfilter As String = ""
                                Dim _resultnumber As Integer
                                _resultnumber = 0
                                strfilter = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(nOrders)("labom_OrderID").ToString() & "' "

                                dvResults.RowFilter = strfilter

                                _Flex.Rows.Add()

                                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail

                                _FillTestLineNo = .Rows(nOrders)("labotd_LineNo") ' .TestLineNo
                                Dim DmsID As Int64 = Convert.ToInt64(.Rows(nOrders)("labotd_DMSID"))
                                With _Flex.Rows(_Flex.Rows.Count - 1)
                                    .ImageAndText = True
                                    .Height = 22
                                    .IsNode = True
                                    .Style = _Flex.Styles("CS_Record")
                                    .Node.Level = 0
                                    If DmsID = 0 Then
                                        .Node.Image = ImgTest.Image
                                    Else
                                        .Node.Image = ImgAttachment.Image
                                    End If
                                    .Node.Data = _FillTestName
                                End With

                                _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SELECT, _Flex.Styles("CS_CheckBox"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SELECT, False)
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nOrders)("labotd_TestID")) '.TestID
                                '_Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                If IsNothing(.Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime"))
                                End If


                                _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_VISITID, _CurrentVisitID) 'VisitID
                                'labs denormalization
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTNAME, .Rows(nOrders)("labotd_TestName")) 'VisitID




                                ''Added by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, .Rows(nOrders)("labotd_TestStatus"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, .Rows(nOrders)("labotd_SpecimenSource"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENCONDITIONDISP, .Rows(nOrders)("labotd_SpecimenConditionDisp"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, .Rows(nOrders)("labotd_TestType"))
                                ''End of changes by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, .Rows(nOrders)("labotd_LOINCCode"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTCPT, .Rows(nOrders)("sCPTCode"))
                                '---

                                'Mitesh
                                If IsNothing(.Rows(nOrders)("labotr_TestResultDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATE, .Rows(nOrders)("labotr_TestResultDateTime"))
                                End If
                                '--

                                Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                                _Flex.SetCellImage(_Flex.Rows.Count - 1, COL_Lab_InfoButton, imgFlag)


                                Dim nStyleRow As Int16 = 0
                                Dim nNodeRow As Int16 = 0
                                Dim i As Int16 = 0

                                Dim _testname As String
                                For i = 0 To dvResults.Count - 1

                                    '   Dim strfilter1 As String
                                    ' strfilter1 = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "'AND labotrd_Resultlineno = '" & dvResults.Table.Rows(nOrders)("labotrd_Resultlineno").ToString() & "'"
                                    ' dvResults.RowFilter = strfilter1
                                    If IsNothing(dvResults(i)("labotr_TestID")) = False Then

                                        If Convert.ToString(dvResults(i)("labotr_TestID")) <> "" Then

                                            If _resultnumber <> dvResults(i)("labotr_testresultnumber") Then
                                                '' And _resultlineno <> dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno") Then
                                                _testname = dvResults(i)("labotr_TestName")
                                                ''  _resultlineno = dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno")
                                                ' If _resultnumber <> dsResultWithDetails.Tables(0).Rows(i)("labotr_testresultnumber") Then
                                                _resultnumber = dvResults(i)("labotr_testresultnumber")
                                                ''_Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsResults.Tables(0).Rows(nOrders)("labotr_TestResultName"), dsResults.Tables(0).Rows(nOrders)("labotd_TestID") & dsResults.Tables(0).Rows(nOrders)("labotr_TestResultNumber"), ImgResultHeader.Image)
                                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotr_TestResultName"), dvResults.Table.Rows(nOrders)("labotd_TestID") & dvResults(i)("labotr_TestResultNumber"), ImgResultHeader_Flask.Image)
                                                nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                _FillTestResultsNodeIndex = nStyleRow
                                                '---------------------------------------------------------
                                                _Flex.SetData(nStyleRow, COL_SELECT, "")
                                                _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                                _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                                _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotr_TestResultName"))
                                                _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotr_TestResultNumber"))
                                                _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
                                                _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)
                                                _Flex.SetData(nStyleRow, COL_VALUE, "Value") '"Value"
                                                _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
                                                _Flex.SetData(nStyleRow, COL_RANGE, "Range")
                                                _Flex.SetData(nStyleRow, COL_RESULTTYPE, "ResultType")
                                                _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
                                                _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotr_IsFinished"))
                                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                                _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                                'labs denormalization 20090321
                                                _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotr_TestName"))

                                                ' _Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotr_TestResultDateTime")) ' oOrder.TransactionDate
                                                'Commented not display on Result line
                                                '_Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotrd_TestSpecimenCollectionDateTime"))
                                                '---
                                            Else
                                                '  i = i + 1
                                                ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            End If
                                            ' End If
                                            ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            '//---Add Test Result Detail---Start---//

                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present
                                            If Not IsNothing(dvResults(i)("labotrd_ResultComment")) Then
                                                If dvResults(i)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Comment.Image)
                                                Else
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                                End If
                                            Else
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                            End If
                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present


                                            '_Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult.Image)
                                            nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            '---------------------------------------------------------
                                            _Flex.SetData(nStyleRow, COL_SELECT, "")
                                            _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotrd_ResultName"))
                                            _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotrd_TestResultNumber"))
                                            _Flex.SetData(nStyleRow, COL_RESULTLINENO, dvResults(i)("labotrd_ResultLineNo"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNAMEID, dvResults(i)("labotrd_ResultNameID"))
                                            '_Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue")) commented by manoj on 20121127
                                            'start of added by manoj on 20121127 for displaying hyperlink in result value
                                            If gloGlobal.gloLabGenral.IsResultisHyperLink(dvResults(i)("labotrd_ResultValue")) Then
                                                _Flex.Cols(COL_VALUE).Item(nStyleRow) = New Hyperlink(dvResults(i)("labotrd_ResultValue"))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue"))
                                            End If
                                            'end of added by manoj on 20121127 for displaying hyperlink in result value
                                            _Flex.SetData(nStyleRow, COL_UNIT, dvResults(i)("labotrd_ResultUnit"))
                                            _Flex.SetData(nStyleRow, COL_RANGE, dvResults(i)("labotrd_ResultRange"))
                                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(dvResults(i)("labotrd_ResultType")))

                                            ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                            ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResults)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
                                            Dim strFlagList As String()
                                            Dim strFlagInDB As String = dvResults(i)("labotrd_AbnormalFlag")
                                            strFlagList = strFlagInDB.Split(",")
                                            If strFlagList.Length > 1 Then
                                                If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                                Else
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                                End If
                                            Else
                                                _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(dvResults(i)("labotrd_AbnormalFlag")))
                                            End If
                                            ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                            _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotrd_IsFinished"))
                                            _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                                            _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                            _Flex.SetData(nStyleRow, COL_ResultLoincCode, dvResults(i)("labotrd_LOINCID"))
                                            _Flex.SetData(nStyleRow, COL_ReceiveDate, dvResults(i)("labotrd_ResultDateTime"))
                                            _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotrd_TestName"))
                                            'sarika bug 1300 20090518
                                            ' chetan commented on 28-oct-2010
                                            ' _Flex.SetData(nStyleRow, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                            ' chetan commented on 28-oct-2010
                                            ''Added by Abhijeet on 20101026
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYNAME, dvResults(i)("labotrd_LabFacilityName"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTREETADDRESS, dvResults(i)("labotrd_LabFacilityStreetAddress"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILTYCITY, dvResults(i)("labotrd_LabFacilityCity"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTATE, dvResults(i)("labotrd_LabFacilityState"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYZIPCODE, dvResults(i)("labotrd_LabFacilityZipCode"))
                                            _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, dvResults(i)("labotrd_ResultComment"))
                                            _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, dvResults(i)("labotrd_specificResultRefRange"))
                                            ''End of changes Added by Abhijeet on 20101026

                                            '---
                                            '---------------------------------------------------------

                                            '---------------------------------------------------------

                                            With _Flex.Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                                'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                                'If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
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
                                            _Flex.SetCellImage(nStyleRow, COL_Lab_InfoButton, imgFlag1)
                                            '//---Add Test Result Detail---Finish---//
                                            ''SLR free imgFlag1  ''giving error therefore commented
                                            'If Not IsNothing(imgFlag1) Then
                                            '    imgFlag1.Dispose()
                                            '    imgFlag1 = Nothing
                                            'End If
                                        End If
                                    End If
                                Next
                            End With 'With .TestResultDetails.Item(nResult)
                        End If
                    Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1


                End If
            End If

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If
            _ClearCombo = False

            RaiseEvent On_SearchCriteria_Changed()
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function


    Public Function SetDataByOrderIDSource(ByVal OrderID As Long, ByVal SourceOrdID As Long) As Boolean

        pnlSelectCriteria.Visible = False
        pnlSelectCriteria1.Visible = False
        pnlFlexGrid.Visible = True

        If IsNothing(_ObservationStatus_COL) Then
            Fill_ResultType()
        End If
        If IsNothing(_AbnormalFlag_COL) Then
            Fill_AbnormalFlag()
        End If


        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        Dim _FillOrderID As Int64 = 0
        Dim strDia As String = ""
        Dim strNames = String.Empty
        Dim dvResults As DataView = Nothing ''Slr new not needed

        Dim map As MultiColumnDictionary = Nothing
        Dim csNothing As C1.Win.C1FlexGrid.CellStyle = Nothing
        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing

        Try
            _isTestsLoaded = False

            dsresults = GetResultsByOrder(OrderID)

            If IsNothing(dsresults) = False Then
                dvResults = dsresults.Tables(0).DefaultView
            Else
                dvResults = New DataView
            End If

            DesignTestGridSource(SourceOrdID, True)

            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then

                    If SourceOrdID > 0 Then
                        '  dtSourceTestName = Nothing
                        If (IsNothing(dtSourceTestName) = False) Then
                            dtSourceTestName.Dispose()
                            dtSourceTestName = Nothing

                        End If
                        'If (IsNothing(dtDestinationTestName) = False) Then
                        '    dtDestinationTestName.Dispose()
                        '    dtDestinationTestName = Nothing

                        'End If
                        dtSourceTestName = GetTestNameByOrderID(SourceOrdID)

                        If OrderID > 0 Then
                            ' dtDestinationTestName = Nothing
                            'If (IsNothing(dtSourceTestName) = False) Then
                            '    dtSourceTestName.Dispose()
                            '    dtSourceTestName = Nothing

                            'End If
                            If (IsNothing(dtDestinationTestName) = False) Then
                                dtDestinationTestName.Dispose()
                                dtDestinationTestName = Nothing

                            End If
                            dtDestinationTestName = GetTestNameByOrderID(OrderID)
                        End If

                        Dim columnNames() As String
                        columnNames = New String() {"labotd_TestName"}
                        map = New MultiColumnDictionary(dtSourceTestName, "labotd_TestID", columnNames, 0)
                    End If
                End If

                If (nrowcount > dvResults.Count) Then
                    _isTestsLoaded = True
                End If

            End If

            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then
                    For nOrders As Int16 = 0 To dvResults.Table.Rows.Count - 1
                        'remove the current order from the Orders table
                        _CurrentOrderID = dvResults.Table.Rows(nOrders)("labom_OrderID")
                        _CurrentVisitID = dvResults.Table.Rows(nOrders)("labom_VisitID")

                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID")) OrElse _FillOrderID <> Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestName"))

                            With dvResults.Table

                                Dim strfilter As String = ""
                                Dim _resultnumber As Integer
                                _resultnumber = 0
                                strfilter = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(nOrders)("labom_OrderID").ToString() & "' "
                                dvResults.RowFilter = strfilter
                                _Flex.Rows.Add()
                                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail
                                _FillTestLineNo = .Rows(nOrders)("labotd_LineNo") ' .TestLineNo

                                Dim DmsID As Int64 = Convert.ToInt64(.Rows(nOrders)("labotd_DMSID"))
                                With _Flex.Rows(_Flex.Rows.Count - 1)
                                    .ImageAndText = True
                                    .Height = 22
                                    .IsNode = True
                                    .Style = _Flex.Styles("CS_Record")
                                    .Node.Level = 0
                                    If DmsID = 0 Then
                                        .Node.Image = ImgTest.Image
                                    Else
                                        .Node.Image = ImgAttachment.Image
                                    End If
                                    .Node.Data = _FillTestName
                                End With

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                                If SourceOrdID > 0 Then
                                    'csNothing = _Flex.Styles.Add("Combostyle" & _Flex.Rows.Count - 1)
                                    Try
                                        If (_Flex.Styles.Contains("Combostyle" & _Flex.Rows.Count - 1)) Then
                                            csNothing = _Flex.Styles("Combostyle" & _Flex.Rows.Count - 1)
                                        Else
                                            csNothing = _Flex.Styles.Add("Combostyle" & _Flex.Rows.Count - 1)
                                            With csNothing
                                                .DataMap = map
                                                _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SourceCombo, csNothing)
                                            End With
                                        End If
                                    Catch ex As Exception
                                        csNothing = _Flex.Styles.Add("Combostyle" & _Flex.Rows.Count - 1)

                                        With csNothing
                                            .DataMap = map
                                            _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SourceCombo, csNothing)
                                        End With

                                    End Try

                                End If

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''

                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nOrders)("labotd_TestID")) '.TestID

                                Dim nStyleRow As Int16 = 0
                                Dim nNodeRow As Int16 = 0
                                Dim i As Int16 = 0

                                Dim _testname As String

                                For i = 0 To dvResults.Count - 1

                                    If IsNothing(dvResults(i)("labotr_TestID")) = False Then
                                        If Convert.ToString(dvResults(i)("labotr_TestID")) <> "" Then

                                            If _resultnumber <> dvResults(i)("labotr_testresultnumber") Then
                                                _testname = dvResults(i)("labotr_TestName")
                                                _resultnumber = dvResults(i)("labotr_testresultnumber")
                                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotr_TestResultName"), dvResults.Table.Rows(nOrders)("labotd_TestID") & dvResults(i)("labotr_TestResultNumber"), ImgResultHeader_Flask.Image)
                                                nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                _FillTestResultsNodeIndex = nStyleRow
                                                '---------------------------------------------------------
                                                _Flex.SetData(nStyleRow, COL_SELECT, "")
                                                _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                                _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                                _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotr_TestResultName"))

                                                _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotr_TestResultNumber"))
                                                _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
                                                _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)

                                            Else

                                            End If

                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present
                                            If Not IsNothing(dvResults(i)("labotrd_ResultComment")) Then
                                                If dvResults(i)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Comment.Image)
                                                Else
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                                End If
                                            Else
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                            End If

                                            nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            '---------------------------------------------------------

                                            _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotrd_TestResultNumber"))
                                            _Flex.SetData(nStyleRow, COL_RESULTLINENO, dvResults(i)("labotrd_ResultLineNo"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNAMEID, dvResults(i)("labotrd_ResultNameID"))

                                            If gloGlobal.gloLabGenral.IsResultisHyperLink(dvResults(i)("labotrd_ResultValue")) Then
                                                _Flex.Cols(COL_VALUE).Item(nStyleRow) = New Hyperlink(dvResults(i)("labotrd_ResultValue"))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue"))
                                            End If

                                            With _Flex.Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                    'Noraml and nothing
                                                    .Style = _Flex.Styles("CS_Record")
                                                Else
                                                    .Style = _Flex.Styles("CS_NotNormal")
                                                    ' '' Not Normal 
                                                End If

                                                .AllowEditing = False
                                            End With
                                        End If
                                    End If
                                Next
                            End With 'With .TestResultDetails.Item(nResult)
                        End If
                    Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1

                End If
            End If

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If
            _ClearCombo = False

            RaiseEvent On_SearchCriteria_Changed()

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Public Function GetResultsByOrder(ByVal OrderID As Int64, ByVal Rowcount As Integer)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 
            Dim ds As DataSet  ''SLR: new is not needed

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Rowcount
                oPara.Name = "@RowCount"
                .DBParametersCol.Add(oPara)
                oPara = Nothing



                ds = .GetDataSet("Lab_GetResults_ByOrder")
                'ds.Tables(0).TableName = "Tests"
                'ds.Tables(1).TableName = "Results"
                'ds.Tables(2).TableName = "ResultDetails"
                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                'oPara = Nothing
            End With
            Return ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'SLR: Finaly free odb, dbparameteds
            If Not IsNothing(odb) Then
                odb.Dispose()

            End If
            odb = Nothing
        End Try

    End Function

    Public Function GetResultsByOrder(ByVal OrderID As Int64)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try

            Dim ds As DataSet
            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                ds = .GetDataSet("Lab_GetResults_ByOrder_Merge")

            End With
            Return ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(odb) Then
                odb.Dispose()
            End If
            odb = Nothing
        End Try

    End Function

    Public Function GetTestNameByOrderID(ByVal OrderID As Int64)
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try

            Dim dt As DataTable

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
                dt = .GetDataTable("MergeOrders_GetTestsByOrderID")

            End With
            Return dt
            dt = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(odb) Then
                odb.Dispose()

            End If
            odb = Nothing
        End Try

    End Function


#End Region


    Private Sub _Flex_BeforeEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.BeforeEdit

        Try
            'If e.Row > 0 Then
            '    If e.Col = COL_SourceCombo Then

            '        Dim ostyle As C1.Win.C1FlexGrid.CellStyle

            '        If Not IsNothing(_Flex.GetCellStyle(e.Row, COL_SourceCombo)) Then
            '            ostyle = _Flex.GetCellStyle(e.Row, COL_SourceCombo)

            '            If ostyle.DataMap IsNot Nothing Then


            '                '               Dim Map As MultiColumnDictionary = ostyle.DataMap
            '                '                'For i As Integer = 0 To Map.Count - 1
            '                '                'For Each pair As VariantType In Map

            '                '                Dim myEnumerator As IDictionaryEnumerator = Map.GetEnumerator()

            '                '                While myEnumerator.MoveNext()
            '                '                    Dim s As String
            '                '                    s = myEnumerator.Value
            '                '                End While


            '            End If


            '            '            '    strUserList = ostyle.ComboList
            '        End If



            '    Dim _sourceColumn As C1.Win.C1FlexGrid.Column = Nothing

            '    _sourceColumn = _Flex.Cols(e.Col)
            '    If _sourceColumn.DataMap IsNot Nothing Then
            '        Dim dt = TryCast(_Flex.DataSource, DataTable)

            '        For Each dr As DataRow In dt.Rows
            '            Dim key = dr(_sourceColumn.Name)
            '            dr("labotd_TestID") = _sourceColumn.DataMap(key)

            '        Next


            '    End If

            '    'Dim testID As String
            '    'Dim _SourceTestNames As New gloEMRActors.LabActor.ItemDetails
            '    'Dim _SourceTest As gloEMRActors.LabActor.ItemDetail
            '    'Dim ostyle As C1.Win.C1FlexGrid.CellStyle
            '    'Dim strUserList As String = ""

            '    Dim Map As MultiColumnDictionary = DirectCast(_Flex.Cols(COL_SourceCombo).DataMap, C1.Win.C1FlexGrid.MultiColumnDictionary)
            '    'For i As Integer = 0 To Map.Count - 1
            '    'For Each pair As VariantType In Map

            '    Dim myEnumerator As IDictionaryEnumerator = Map.GetEnumerator()

            '    While myEnumerator.MoveNext()
            '        Dim s As String
            '        s = myEnumerator.Value
            '    End While


            ' Next

            ' Next

            'testID = Convert.ToString(_Flex.GetData(e.Row, COL_TESTID))
            ''  _SourceTestNames = CType(_Flex.GetData(e.Row, COL_SourceCombo), gloEMRActors.LabActor.ItemDetails)
            ' ''
            'If Not IsNothing(_Flex.GetCellStyle(e.Row, COL_SourceCombo)) Then
            '    ostyle = _Flex.GetCellStyle(e.Row, COL_SourceCombo)
            '    strUserList = ostyle.ComboList
            'End If

            ' ''
            'If Not IsNothing(_SourceTestNames) Then
            '    For i As Int16 = 0 To _SourceTestNames.Count - 1
            '        _SourceTestNames.Item(i).Description = testID
            '        e.Cancel = True
            '        Exit For
            '    Next
            'End If
            'End If
            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function GetDataTableFromControl() As DataTable

        Dim dt As DataTable = Nothing
        Dim r As DataRow
        Try
            Dim nStyleRow As Int16 = 0
            dt = New DataTable
            Dim clmnDestination As New DataColumn
            With clmnDestination
                .ColumnName = "Manual"
                .DataType = System.Type.GetType("System.Int64")
            End With
            dt.Columns.Add(clmnDestination)

            Dim clmnSource As New DataColumn
            With clmnSource
                .ColumnName = "Electronic"
                .DataType = System.Type.GetType("System.Int64")
            End With
            dt.Columns.Add(clmnSource)

            Dim ostyle As C1.Win.C1FlexGrid.CellStyle
            Dim Result As String = ""
            Dim _sourceTestID As Int64 = 0
            Dim _destinationTestID As Int64 = 0

            With _Flex
                Dim i As Integer = 1

                For i = 1 To .Rows.Count - 1
                    If Not IsNothing(_Flex.GetCellStyle(i, COL_SourceCombo)) Then
                        ostyle = _Flex.GetCellStyle(i, COL_SourceCombo)

                        If ostyle.DataMap IsNot Nothing Then
                            _sourceTestID = Convert.ToInt64(_Flex.GetData(i, COL_TESTID))
                            If _Flex(i, COL_SourceCombo) <> Nothing Then
                                _destinationTestID = Convert.ToInt64(_Flex(i, COL_SourceCombo).ToString())
                            Else
                                _destinationTestID = 0
                            End If
                            r = dt.NewRow()
                            r("Electronic") = _sourceTestID
                            r("Manual") = _destinationTestID
                            dt.Rows.Add(r)
                        End If

                    End If
                Next

            End With

            Return dt


        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function

    'Private Sub checkDuplicate1(dataMapValue As String)

    '    If Not IsNothing(dtDestinationTestName) Then
    '        If dtDestinationTestName.Rows.Count > 0 Then
    '            Dim i As Int16 = 0
    '            For i = 0 To dtDestinationTestName.Rows.Count - 1
    '                If dataMapValue = dtDestinationTestName.Rows(i)("labotd_TestID").ToString() Then
    '                    MessageBox.Show("Selected Test is already being merged into. Same Test cannot be added again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    End If



    'End Sub


    Private Function checkDuplicate(dataMapValue As String) As Boolean

        Dim boolValue As Boolean = True

        If Not IsNothing(dtDestinationTestName) Then
            If dtDestinationTestName.Rows.Count > 0 Then
                Dim i As Int16 = 0
                For i = 0 To dtDestinationTestName.Rows.Count - 1
                    If dataMapValue = dtDestinationTestName.Rows(i)("labotd_TestID").ToString() Then
                        boolValue = False
                        MessageBox.Show("Selected Test is already being merged into. Same Test cannot be added again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If
                Next
            End If
        End If

        Return boolValue

    End Function

#Region "By DataTable"
    Public Function Bind(Data As DataSet) As Boolean
        '
        pnlSelectCriteria.Visible = False
        pnlSelectCriteria1.Visible = False
        pnlFlexGrid.Visible = True

        Dim _FillTestName As String = ""
        Dim _FillTestCode As String = ""
        Dim _FillTestNodeIndex As Int16 = 0
        Dim _FillTestResultsNodeIndex As Int16 = 0
        Dim _FillTestLineNo As Int16 = 0
        Dim _FillOrderID As Int64 = 0
        Dim dvResults As DataView = Nothing ''Slr new not needed
        Try

            dsresults = Data

            _isTestsLoaded = False


            'dsresults = GetResultsByOrder(OrderID, nrowcount)
            'SLR: else aloocate new dtaview
            If IsNothing(dsresults) = False Then
                dvResults = dsresults.Tables(0).DefaultView
            Else
                dvResults = New DataView
            End If

            DesignTestGrid(True)

            'If IsNothing(dvResults) = False Then
            '    If dvResults.Count > 0 Then


            '        If (nrowcount > dvResults.Count) Then
            '            _isTestsLoaded = True
            '        End If
            '        If (nrowcount < 35) Then
            '            nrowcount = 35
            '        Else
            '            nrowcount = dvResults.Count
            '            If (nrowcount < 35) Then
            '                nrowcount = 35
            '            End If
            '        End If
            '    End If
            'End If
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then

                    For nOrders As Int16 = 0 To dvResults.Table.Rows.Count - 1

                        'remove the current order from the Orders table
                        _CurrentOrderID = dvResults.Table.Rows(nOrders)("labom_OrderID")

                        _CurrentVisitID = dvResults.Table.Rows(nOrders)("labom_VisitID")


                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID")) OrElse _FillOrderID <> Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(nOrders)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(nOrders)("labotd_TestName"))
                            With dvResults.Table  '.Item(nTest)
                                '//---Add Test---Start---//
                                Dim strfilter As String = ""
                                Dim _resultnumber As Integer
                                _resultnumber = 0
                                strfilter = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(nOrders)("labom_OrderID").ToString() & "' "

                                dvResults.RowFilter = strfilter

                                _Flex.Rows.Add()

                                _FillTestNodeIndex = _Flex.Rows.Count - 1 ' It use to fill test result header and its result detail

                                _FillTestLineNo = .Rows(nOrders)("labotd_LineNo") ' .TestLineNo
                                Dim DmsID As Int64 = Convert.ToInt64(.Rows(nOrders)("labotd_DMSID"))
                                With _Flex.Rows(_Flex.Rows.Count - 1)
                                    .ImageAndText = True
                                    .Height = 22
                                    .IsNode = True
                                    .Style = _Flex.Styles("CS_Record")
                                    .Node.Level = 0
                                    If DmsID = 0 Then
                                        .Node.Image = ImgTest.Image
                                    Else
                                        .Node.Image = ImgAttachment.Image
                                    End If
                                    .Node.Data = _FillTestName
                                End With

                                _Flex.SetCellStyle(_Flex.Rows.Count - 1, COL_SELECT, _Flex.Styles("CS_CheckBox"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SELECT, False)
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ORDERID, _CurrentOrderID) '.OrderID
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTID, .Rows(nOrders)("labotd_TestID")) '.TestID
                                '_Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                If IsNothing(.Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_DATE, .Rows(nOrders)("labotrd_TestSpecimenCollectionDateTime"))
                                End If


                                _Flex.SetData(_Flex.Rows.Count - 1, COL_RECORDTYPE, CType(enumRecordType.Test, Integer))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_VISITID, _CurrentVisitID) 'VisitID
                                'labs denormalization
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTNAME, .Rows(nOrders)("labotd_TestName")) 'VisitID

                                ''Added by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTSTATUS, .Rows(nOrders)("labotd_TestStatus"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENSOURCE, .Rows(nOrders)("labotd_SpecimenSource"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_SPECIMENCONDITIONDISP, .Rows(nOrders)("labotd_SpecimenConditionDisp"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTTYPE, .Rows(nOrders)("labotd_TestType"))
                                ''End of changes by Abhijeet on 20101026
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_ResultLoincCode, .Rows(nOrders)("labotd_LOINCCode"))
                                _Flex.SetData(_Flex.Rows.Count - 1, COL_TESTCPT, .Rows(nOrders)("sCPTCode"))
                                '---

                                'Mitesh
                                If IsNothing(.Rows(nOrders)("labotr_TestResultDateTime")) = False Then
                                    _Flex.SetData(_Flex.Rows.Count - 1, COL_REPORTED_DATE, .Rows(nOrders)("labotr_TestResultDateTime"))
                                End If
                                '--

                                Dim imgFlag As Image = Global.gloUserControlLibrary.My.Resources.Resources.infobutton
                                _Flex.SetCellImage(_Flex.Rows.Count - 1, COL_Lab_InfoButton, imgFlag)


                                Dim nStyleRow As Int16 = 0
                                Dim nNodeRow As Int16 = 0
                                Dim i As Int16 = 0

                                Dim _testname As String
                                For i = 0 To dvResults.Count - 1

                                    '   Dim strfilter1 As String
                                    ' strfilter1 = "labotd_TestID = '" & dvResults.Table.Rows(nOrders)("labotd_TestID").ToString() & "'AND labotrd_Resultlineno = '" & dvResults.Table.Rows(nOrders)("labotrd_Resultlineno").ToString() & "'"
                                    ' dvResults.RowFilter = strfilter1
                                    If IsNothing(dvResults(i)("labotr_TestID")) = False Then

                                        If Convert.ToString(dvResults(i)("labotr_TestID")) <> "" AndAlso Convert.ToString(dvResults(i)("labotr_TestID")) <> "0" Then

                                            If _resultnumber <> dvResults(i)("labotr_testresultnumber") Then
                                                '' And _resultlineno <> dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno") Then
                                                _testname = dvResults(i)("labotd_TestName")
                                                ''  _resultlineno = dsResultWithDetails.Tables(0).Rows(i)("labotrd_resultlineno")
                                                ' If _resultnumber <> dsResultWithDetails.Tables(0).Rows(i)("labotr_testresultnumber") Then
                                                _resultnumber = dvResults(i)("labotr_testresultnumber")
                                                ''_Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsResults.Tables(0).Rows(nOrders)("labotr_TestResultName"), dsResults.Tables(0).Rows(nOrders)("labotd_TestID") & dsResults.Tables(0).Rows(nOrders)("labotr_TestResultNumber"), ImgResultHeader.Image)
                                                _Flex.Rows(_FillTestNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotr_TestResultName"), dvResults.Table.Rows(nOrders)("labotd_TestID") & dvResults(i)("labotr_TestResultNumber"), ImgResultHeader_Flask.Image)
                                                nStyleRow = _Flex.Rows(_FillTestNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                _FillTestResultsNodeIndex = nStyleRow
                                                '---------------------------------------------------------
                                                _Flex.SetData(nStyleRow, COL_SELECT, "")
                                                _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                                _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                                _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotr_TestResultName"))
                                                _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotr_TestResultNumber"))
                                                _Flex.SetData(nStyleRow, COL_RESULTLINENO, 0)
                                                _Flex.SetData(nStyleRow, COL_RESULTNAMEID, 0)
                                                _Flex.SetData(nStyleRow, COL_VALUE, "Value") '"Value"
                                                _Flex.SetData(nStyleRow, COL_UNIT, "Unit")
                                                _Flex.SetData(nStyleRow, COL_RANGE, "Range")
                                                _Flex.SetData(nStyleRow, COL_RESULTTYPE, "ResultType")
                                                _Flex.SetData(nStyleRow, COL_FLAG, "Flag")
                                                _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotr_IsFinished"))
                                                _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.ResultHeader))
                                                _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                                'labs denormalization 20090321
                                                _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotd_TestName"))

                                                ' _Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotr_TestResultDateTime")) ' oOrder.TransactionDate
                                                'Commented not display on Result line
                                                '_Flex.SetData(nStyleRow, COL_DATE, dvResults(i)("labotrd_TestSpecimenCollectionDateTime"))
                                                '---
                                            Else
                                                '  i = i + 1
                                                ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            End If
                                            ' End If
                                            ' _FillTestResultsNodeIndex = _FillTestResultsNodeIndex + 1
                                            '//---Add Test Result Detail---Start---//

                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present
                                            If Not IsNothing(dvResults(i)("labotrd_ResultComment")) Then
                                                If dvResults(i)("labotrd_ResultComment").ToString() <> "" Then
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Comment.Image)
                                                Else
                                                    _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                                End If
                                            Else
                                                _Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult_Flask.Image)
                                            End If
                                            'Sanjog - Added on 2011 July to Show Different Icon if result Comment is present


                                            '_Flex.Rows(_FillTestResultsNodeIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dvResults(i)("labotrd_ResultName"), dvResults(i)("labotrd_TestResultNumber") & i, ImgResult.Image)
                                            nStyleRow = _Flex.Rows(_FillTestResultsNodeIndex).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            '---------------------------------------------------------
                                            _Flex.SetData(nStyleRow, COL_SELECT, "")
                                            _Flex.SetData(nStyleRow, COL_NAME, dvResults(i)("labotrd_ResultName"))
                                            _Flex.SetData(nStyleRow, COL_ORDERID, _CurrentOrderID)
                                            _Flex.SetData(nStyleRow, COL_TESTID, dvResults.Table.Rows(nOrders)("labotd_TestID"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNUMBER, dvResults(i)("labotrd_TestResultNumber"))
                                            _Flex.SetData(nStyleRow, COL_RESULTLINENO, dvResults(i)("labotrd_ResultLineNo"))
                                            _Flex.SetData(nStyleRow, COL_RESULTNAMEID, dvResults(i)("labotrd_ResultNameID"))
                                            '_Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue")) commented by manoj on 20121127
                                            'start of added by manoj on 20121127 for displaying hyperlink in result value
                                            If gloGlobal.gloLabGenral.IsResultisHyperLink(dvResults(i)("labotrd_ResultValue")) Then
                                                _Flex.Cols(COL_VALUE).Item(nStyleRow) = New Hyperlink(dvResults(i)("labotrd_ResultValue"))
                                            Else
                                                _Flex.SetData(nStyleRow, COL_VALUE, dvResults(i)("labotrd_ResultValue"))
                                            End If
                                            'end of added by manoj on 20121127 for displaying hyperlink in result value
                                            _Flex.SetData(nStyleRow, COL_UNIT, dvResults(i)("labotrd_ResultUnit"))
                                            _Flex.SetData(nStyleRow, COL_RANGE, dvResults(i)("labotrd_ResultRange"))
                                            _Flex.SetData(nStyleRow, COL_RESULTTYPE, _ObservationStatus_COL.GetDescription(dvResults(i)("labotrd_ResultType")))

                                            ''By Abhijeet on 20101122 for showing result flag description according to settings.
                                            ''_Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(.Rows(nResults)("labotrd_AbnormalFlag"))) ''.Rows(nResultDetails)("labotrd_AbnormalFlag")
                                            Dim strFlagList As String()
                                            Dim strFlagInDB As String = dvResults(i)("labotrd_AbnormalFlag")
                                            strFlagList = strFlagInDB.Split(",")
                                            If strFlagList.Length > 1 Then
                                                If gloGeneral.clsgeneral.gstrSpecificResultRange = "1" Then
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(1)))
                                                Else
                                                    _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(strFlagList(0)))
                                                End If
                                            Else
                                                _Flex.SetData(nStyleRow, COL_FLAG, _AbnormalFlag_COL.GetDescription(dvResults(i)("labotrd_AbnormalFlag")))
                                            End If
                                            ''End of changes by Abhijeet on 20101122 for showing result flag description according to settings.

                                            _Flex.SetData(nStyleRow, COL_ISFINISHED, dvResults(i)("labotrd_IsFinished"))
                                            _Flex.SetData(nStyleRow, COL_RECORDTYPE, CInt(enumRecordType.Result))
                                            _Flex.SetData(nStyleRow, COL_VISITID, _CurrentVisitID)
                                            _Flex.SetData(nStyleRow, COL_ResultLoincCode, dvResults(i)("labotrd_LOINCID"))
                                            _Flex.SetData(nStyleRow, COL_ReceiveDate, dvResults(i)("labotrd_ResultDateTime"))
                                            _Flex.SetData(nStyleRow, COL_TESTNAME, dvResults(i)("labotrd_TestName"))
                                            'sarika bug 1300 20090518
                                            ' chetan commented on 28-oct-2010
                                            ' _Flex.SetData(nStyleRow, COL_DATE, _Orders.Rows(nOrders)("labom_TransactionDate")) ' oOrder.TransactionDate
                                            ' chetan commented on 28-oct-2010
                                            ''Added by Abhijeet on 20101026
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYNAME, dvResults(i)("labotrd_LabFacilityName"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTREETADDRESS, dvResults(i)("labotrd_LabFacilityStreetAddress"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILTYCITY, dvResults(i)("labotrd_LabFacilityCity"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYSTATE, dvResults(i)("labotrd_LabFacilityState"))
                                            _Flex.SetData(nStyleRow, COL_LABFACILITYZIPCODE, dvResults(i)("labotrd_LabFacilityZipCode"))
                                            _Flex.SetData(nStyleRow, COL_RESULT_COMMENT, dvResults(i)("labotrd_ResultComment"))
                                            _Flex.SetData(nStyleRow, COL_PATIENT_RANGE, dvResults(i)("labotrd_specificResultRefRange"))
                                            ''End of changes Added by Abhijeet on 20101026

                                            '---
                                            '---------------------------------------------------------

                                            '---------------------------------------------------------

                                            With _Flex.Rows(nStyleRow)
                                                .ImageAndText = False
                                                .Height = 22
                                                'GLO2012-0016123 : Lab results coming in are all showing abnormal, although the values are within the range
                                                'Instead of IsNothing, verified the abnormal flag string for null or empty both to apply proper style
                                                'If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" Or IsNothing(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
                                                If _AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG)) = "N" OrElse String.IsNullOrEmpty(_AbnormalFlag_COL.GetCode(_Flex.GetData(nStyleRow, COL_FLAG))) Then
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
                                            _Flex.SetCellImage(nStyleRow, COL_Lab_InfoButton, imgFlag1)
                                            '//---Add Test Result Detail---Finish---//
                                            ''SLR free imgFlag1  ''giving error therefore commented
                                            'If Not IsNothing(imgFlag1) Then
                                            '    imgFlag1.Dispose()
                                            '    imgFlag1 = Nothing
                                            'End If
                                        End If
                                    End If
                                Next
                            End With 'With .TestResultDetails.Item(nResult)
                        End If
                    Next ' For nResult As Int16 = 0 To .TestResultDetails.Count - 1


                End If
            End If

            If _Flex.Rows.Count >= 2 Then
                _Flex.Row = 1
                _Flex.Select(1, COL_SELECT)
            End If
            _ClearCombo = False

            RaiseEvent On_SearchCriteria_Changed()
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
#End Region

    'Public Sub AfterScroll(sender As Object, e As RangeEventArgs)
    '    If (e.NewRange.BottomRow = _Flex.Rows.Count - 1) And e.NewRange.BottomRow <> -1 And _isTestsLoaded = False Then
    '        If Not IsNothing(cmbCriteria.SelectedItem) Then
    '            If cmbCriteria.SelectedItem.ToString() = "Date" Then
    '                _Flex.BeginUpdate()
    '                Dim _ScrollPosition As Point
    '                _ScrollPosition = _Flex.ScrollPosition
    '                nrowcount = nrowcount + nrowcount
    '                ''  SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
    '                _isscroll = True
    '                ''SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
    '                SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
    '                ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
    '                _Flex.EndUpdate()
    '                _isscroll = False
    '            End If
    '        Else
    '            If _MergeOrderID <> 0 Then
    '                _Flex.BeginUpdate()
    '                Dim _ScrollPosition As Point
    '                _ScrollPosition = _Flex.ScrollPosition
    '                nrowcount = nrowcount + nrowcount
    '                ''  SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
    '                _isscroll = True
    '                ''SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
    '                SetDataByOrderIDSource(_MergeOrderID, _curOrderID)
    '                ' SetDataByDate(DateTime.Now.Date, DateTime.Now.Date, nrowcount)
    '                _Flex.EndUpdate()
    '                _isscroll = False
    '            End If
    '        End If

    '    End If
    'End Sub

   
End Class
