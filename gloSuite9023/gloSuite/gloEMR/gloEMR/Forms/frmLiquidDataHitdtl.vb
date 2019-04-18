Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Reflection
Imports AlphaII.CodeWizard.Objects.EvaluationManagement
Public Class frmLiquidDataHitdtl

#Region "Define Flexgrid column"


    Private _CollLiquidData As CollLiquidData
    Private Col_PatientID As Integer = 0
    Private Col_VisitID As Integer = 1
    Private Col_ExamId As Integer = 2
    Private Col_ElementId As Integer = 3
    Private Col_Helptext As Integer = 4
    Private Col_DataType As Integer = 5
    Private Col_ElementName As Integer = 6
    Private Col_ElementCategory As Integer = 7
    Private Col_HiddenElementCategory As Integer = 8
    Private Col_ElementDetails As Integer = 9
    Private Col_HitCount As Integer = 10
    Private Col_Count As Integer = 11

    Private Col_FieldName As Integer = 0
    Private Col_AssociateFieldName As Integer = 1
    Private Col_FieldCount As Integer = 2

#End Region

#Region "Local varible"



    Public PatientID As Int64
    Public VisitID As Int64
    Public ExamID As Int64
    Public ElementID As Int64
    Public Helptext As Int64
    Public DataType As String
    Dim oDB As DataBaseLayer
    Dim dt As DataTable
    Dim NewRow As Integer = 0

    Dim strTemp As String = ""
    Dim temp As String
    Dim cnt As Integer = 0
    Dim diff As Integer = 0
    Dim Hitcnt As Integer = 0
    Dim Total As Int16 = 0
    Dim GrandTotal As Int16 = 0
    Dim _arrLiquidCategory As New ArrayList
    Dim oLiquidCollection As CollLiquidData

    Dim Historycount As Integer = 0
    Dim ExamCount As Integer = 0
    Dim MDCount As Integer = 0
    Dim HPICount As Integer = 0

    Dim _ExamID As Int64 = 0
    Dim _PatientID As Int64 = 0
    Dim _VisitID As Int64 = 0
    Dim _DOS As DateTime
    Dim _VisitDate As DateTime
    Dim _Ismodified As Boolean = False
    Dim _EMExamType As String
#End Region

#Region "AlphII SDK Varaible"
    Dim _dc As New AlphaII.CodeWizard.Collections.DiagnosisCollection
    Dim oExam As New AlphaII.CodeWizard.Objects.EvaluationManagement.Exams.GeneralMultiSystemExam
    Dim oManagmentOption As New AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions
    Dim oLabs As New AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs
    Dim oXray As New AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology
    Dim oOtherDigTest As New AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests
    Dim oHistory As New AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory
    Dim oMd As New AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtMedicalComplexity
    Dim diagnosis As New AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexDiagnosis
#End Region



    Dim IsHPIBrief As Boolean = False
    Dim IsHPIExtended As Boolean = False
    Dim ocls As New clsLiquiddbLayer
    Dim _dtLabs As New DataTable
    Dim _dtRadiology As New DataTable
    Dim _dtFlowSheet As New DataTable
    'Dim _dtLabsTags As New DataTable
    Dim _dtDrugs As New DataTable
    Dim strAssociateEMField As String
    Public _EmCode As String
    Public _Result As String
    Public _strTagEM As String
    Private bManagementOption As Boolean = False
    Private bXray As Boolean = False
    Private bLabs As Boolean = False
    Private bOtherDiagnostictest As Boolean = False
    Dim strEMTags() As String

    Public _arrLabs As New ArrayList
    Public _arrOrders As New ArrayList
    Public _arrMangementOption As New ArrayList
    Public _arrOtherDiag As New ArrayList
    Public _arrTagsLabs As New ArrayList
    'Dim _arrDiagnosisData As ArrayList
    Dim oDiagnosisData As ArrayList
    Dim oListItem As gloGeneralItem.gloItem
    'constructor commented by dipak as not in use
    'Public Sub New(ByVal oColLiquidData As CollLiquidData)
    '    _CollLiquidData = oColLiquidData

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub
    Public Sub New(ByVal arrLiquidCategory As ArrayList, ByVal ExamID As Int64, ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal DOS As DateTime, ByVal strTagEM As String, ByVal DiagnosisData As ArrayList)
        _arrLiquidCategory = arrLiquidCategory
        _ExamID = ExamID
        _PatientID = PatientID
        _VisitID = VisitID
        _DOS = DOS
        _strTagEM = strTagEM
        '_arrDiagnosisData = arrDiagnosisData
        oDiagnosisData = DiagnosisData
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
#Region "Property Procedures"
    'Public Property arrDiagnosisData()
    '    Get
    '        Return _arrDiagnosisData
    '    End Get
    '    Set(ByVal value)
    '        value = _arrDiagnosisData
    '    End Set
    'End Property
    ''Added by Mayuri:20100615-To save data on diagnosis tab:case No:0003710
    Public Property DiagnosisData()
        Get
            Return oDiagnosisData
        End Get
        Set(ByVal value)
            value = oDiagnosisData
        End Set
    End Property
    Public Property EMExamType()
        Get
            Return _EMExamType
        End Get
        Set(ByVal value)
            value = _EMExamType
        End Set
    End Property
    ''end code Added by Mayuri:20100615
    
#End Region

    Private Sub frmLiquidDataHitdtl_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            SaveDiagnosisData(_dc)
            'Dim Result As Integer

            ''If IsModified() = True Then
            'Result = MsgBox("Do you want to save the changes?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
            'If Result = MsgBoxResult.Yes Then
            '    SaveDiagnosisData()
            '    'Me.Close()
            'ElseIf Result = MsgBoxResult.No Then
            'ElseIf Result = MsgBoxResult.Cancel Then
            '    e.Cancel = True
            '    Exit Sub

            'End If
            ''End If
        Catch ex As Exception

        End Try
    End Sub
   

    Private Sub frmLiquidDataHitdtl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            gloC1FlexStyle.Style(C1Details)
            gloC1FlexStyle.Style(C1FlexGrid1)
            gloC1FlexStyle.Style(C1FlexGrid2)
            gloC1FlexStyle.Style(C1FlexGrid3)
            gloC1FlexStyle.Style(C1FlexGrid4)
            gloC1FlexStyle.Style(C1FlexGrid5)
            gloC1FlexStyle.Style(C1FlexGrid6)
            gloC1FlexStyle.Style(C1HPI)
            gloC1FlexStyle.Style(C1Labs)
            gloC1FlexStyle.Style(C1MedicalCondition)
            gloC1FlexStyle.Style(C1PhysicalExamination)
            gloC1FlexStyle.Style(C1Radiology)
            gloC1FlexStyle.Style(C1ROS)

            ''''DesignGrid
            DesignGrid()

            ''''Fill Data from Exam
            For i As Integer = 0 To _arrLiquidCategory.Count - 1
                If i = _arrLiquidCategory.Count - 1 Then
                    _dc = CType(_arrLiquidCategory.Item(i), AlphaII.CodeWizard.Collections.DiagnosisCollection)
                    FilldignosisColl(_dc)
                Else
                    ' oLiquidCollection = New CollLiquidData
                    oLiquidCollection = CType(_arrLiquidCategory.Item(i), CollLiquidData)
                    If oLiquidCollection.Count > 0 Then
                        If CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.General Then
                            'FillGeneralData(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Hitory Then
                            FillHistoryData(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Physical_Examination Then
                            FillPhysicalExam(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Medical_Decision_Making Then
                            FillMedicalCondition(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.HPI Then
                            FillHPI(oLiquidCollection)
                            FillROSnHPI(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Management_option Then
                            FillMangement_Option(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Labs Then
                            FillLabs(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.X_Ray_Radiology Then
                            FillX_Ray_Radiology(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.Other_Diagonsis_Tests Then
                            FillOther_Diagonsis_Tests(oLiquidCollection)
                        ElseIf CType(oLiquidCollection.Item(0).Category, CategoryType) = CategoryType.ROS Then
                            FillROS(oLiquidCollection)
                            FillROSnHPI(oLiquidCollection)
                        ElseIf oLiquidCollection.Item(0).Category = CategoryType.DB_History Then
                            FillHistoryData(oLiquidCollection)
                        End If
                    End If
                End If
            Next

            ''''Fill Data from Database against Patient
            _dtLabs = ocls.GetPatientLabsOrder(_PatientID, _DOS, FieldType.Labs.GetHashCode())
            _dtRadiology = ocls.GetPatientRadiologyOrder(_PatientID, _DOS, FieldType.Radiology.GetHashCode())
            _dtFlowSheet = ocls.GetPatientFlowSheet(_PatientID, _VisitID)
            '_dtLabsTags = ocls.GetPatientLabsTagsOrder(_PatientID, _DOS)

            ''''Labs

            'Dim Emylist As myList
            '_arrLabs = New ArrayList
            If Not IsNothing(_dtLabs) Then
                SetColumnstdData(C1Labs)
                For i As Integer = 0 To _dtLabs.Rows.Count - 1
                    If _dtLabs.Rows(i)("sAssociatedEMName") <> "" Then
                        With C1Labs
                            .Rows.Add()
                            .SetData(.Rows.Count - 1, Col_FieldName, _dtLabs.Rows(i)("labtm_Name"))
                            .SetData(.Rows.Count - 1, Col_AssociateFieldName, _dtLabs.Rows(i)("sAssociatedEMName"))
                            ''Code modified by Mayuri:20100617-Used generalitem instead of mylist for Lab purpose only
                            'Emylist = New myList
                            'Emylist.AssociatedProperty = _dtLabs.Rows(i)("sAssociatedEMName")
                            'Emylist.AssociatedCategory = strLabs.ToString()
                            'Emylist.AssociatedItem = "True"
                            '_arrLabs.Add(Emylist)
                            'If _dtLabs.Rows(i)("sAssociatedEMCategory") = strLabs Then
                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = _dtLabs.Rows(i)("sAssociatedEMName")
                            oListItem.Code = strLabs.ToString()
                            oListItem.Status = _dtLabs.Rows(i)("sStatus")
                            'EGeneralItems.Add(oListItem)
                            _arrLabs.Add(oListItem)
                            oListItem.Dispose()

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = _dtLabs.Rows(i)("sAssociatedEMName")
                            oListItem.Code = strOrders.ToString()
                            oListItem.Status = _dtLabs.Rows(i)("sStatus")
                            'EGeneralItems.Add(oListItem)
                            _arrOrders.Add(oListItem)
                            oListItem.Dispose()

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = _dtLabs.Rows(i)("sAssociatedEMName")
                            oListItem.Code = strOtherDiagnosis.ToString()
                            oListItem.Status = _dtLabs.Rows(i)("sStatus")
                            'EGeneralItems.Add(oListItem)
                            _arrOtherDiag.Add(oListItem)
                            oListItem.Dispose()
                            'ElseIf _dtLabs.Rows(i)("sAssociatedEMCategory") = strMangementOption Then

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = _dtLabs.Rows(i)("sAssociatedEMName")
                            oListItem.Code = strMangementOption.ToString()
                            oListItem.Status = _dtLabs.Rows(i)("sStatus")
                            'EGeneralItems.Add(oListItem)
                            _arrMangementOption.Add(oListItem)
                            oListItem.Dispose()
                            ' End If
                            'oLabs.SetProperty(_dtLabs.Rows(i)("sAssociatedEMName"), "True")
                        End With
                    End If
                Next
            End If

            'FillEMLabs()
            ''''Radiology
            If Not IsNothing(_dtRadiology) Then
                'Dim strAssField() As String
                SetColumnstdData(C1Radiology)
                For i As Integer = 0 To _dtRadiology.Rows.Count - 1
                    If _dtRadiology.Rows(i)("sAssociatedEMName") <> "" Then
                        With C1Radiology
                            'strAssField = Split(_dtRadiology.Rows(i)("sAssociatedEMName"), "-")
                            'If strAssField.Length = 2 Then
                            If _dtRadiology.Rows(i)("sAssociatedEMCategory") = strOrders Then
                                With C1Radiology
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_FieldName, _dtRadiology.Rows(i)("lm_test_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateFieldName, _dtRadiology.Rows(i)("sAssociatedEMName"))
                                    'Emylist = New myList
                                    'Emylist.AssociatedProperty = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    'Emylist.AssociatedCategory = strOrders.ToString()
                                    'Emylist.AssociatedItem = "True"
                                    '_arrOrders.Add(Emylist)

                                    oListItem = New gloGeneralItem.gloItem
                                    oListItem.Description = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    oListItem.Code = strOrders.ToString()
                                    oListItem.Status = _dtRadiology.Rows(i)("sStatus")
                                    'EGeneralItems.Add(oListItem)
                                    _arrOrders.Add(oListItem)
                                    oListItem.Dispose()
                                    'oXray.SetProperty(strAssField.GetValue(0), "True")
                                End With
                            ElseIf _dtRadiology.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis Then 'strAssField.GetValue(1) = CategoryType.Other_Diagonsis_Tests.GetHashCode() Then
                                With C1Radiology
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_FieldName, _dtRadiology.Rows(i)("lm_test_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateFieldName, _dtRadiology.Rows(i)("sAssociatedEMName"))
                                    'Emylist = New myList
                                    'Emylist.AssociatedProperty = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                                    'Emylist.AssociatedItem = "True"
                                    '_arrOtherDiag.Add(Emylist)

                                    oListItem = New gloGeneralItem.gloItem
                                    oListItem.Description = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    oListItem.Code = strOtherDiagnosis.ToString()
                                    oListItem.Status = _dtRadiology.Rows(i)("sStatus")
                                    'EGeneralItems.Add(oListItem)
                                    _arrOtherDiag.Add(oListItem)
                                    oListItem.Dispose()
                                    'oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
                                End With

                            ElseIf _dtRadiology.Rows(i)("sAssociatedEMCategory") = strLabs Then 'strAssField.GetValue(1) = CategoryType.Other_Diagonsis_Tests.GetHashCode() Then
                                With C1Radiology
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_FieldName, _dtRadiology.Rows(i)("lm_test_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateFieldName, _dtRadiology.Rows(i)("sAssociatedEMName"))
                                    'Emylist = New myList
                                    'Emylist.AssociatedProperty = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                                    'Emylist.AssociatedItem = "True"
                                    '_arrOtherDiag.Add(Emylist)

                                    oListItem = New gloGeneralItem.gloItem
                                    oListItem.Description = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    oListItem.Code = strLabs.ToString()
                                    oListItem.Status = _dtRadiology.Rows(i)("sStatus")
                                    'EGeneralItems.Add(oListItem)
                                    _arrLabs.Add(oListItem)
                                    oListItem.Dispose()
                                    'oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
                                End With
                            ElseIf _dtRadiology.Rows(i)("sAssociatedEMCategory") = strMangementOption Then 'strAssField.GetValue(1) = CategoryType.Other_Diagonsis_Tests.GetHashCode() Then
                                With C1Radiology
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_FieldName, _dtRadiology.Rows(i)("lm_test_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateFieldName, _dtRadiology.Rows(i)("sAssociatedEMName"))
                                    'Emylist = New myList
                                    'Emylist.AssociatedProperty = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                                    'Emylist.AssociatedItem = "True"
                                    '_arrOtherDiag.Add(Emylist)

                                    oListItem = New gloGeneralItem.gloItem
                                    oListItem.Description = _dtRadiology.Rows(i)("sAssociatedEMName")
                                    oListItem.Code = strMangementOption.ToString()
                                    oListItem.Status = _dtRadiology.Rows(i)("sStatus")
                                    'EGeneralItems.Add(oListItem)
                                    _arrMangementOption.Add(oListItem)
                                    oListItem.Dispose()
                                    'oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
                                End With
                            End If
                            'End If
                        End With
                    End If
                Next
            End If

            ''''Flowsheet
            If Not IsNothing(_dtFlowSheet) Then
                'SetColumnstdData(C1Radiology)
                'SetColumnstdData(C1Labs)
                'Dim strAssField() As String
                For i As Integer = 0 To _dtFlowSheet.Rows.Count - 1

                    If _dtFlowSheet.Rows(i)("sAssociatedEMName") <> "" Then
                        'strAssField = Split(_dtFlowSheet.Rows(i)("sAssociatedEMName"), "-")
                        'If strAssField.Length = 2 Then
                        If _dtFlowSheet.Rows(i)("sAssociatedEMCategory") = strLabs Then
                            With C1Labs
                                ''''Code modified by Mayuri:20100617-Used generalitem instead of mylist for Lab purpose only
                                oListItem = New gloGeneralItem.gloItem
                                oListItem.Description = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                oListItem.Code = strLabs.ToString()
                                oListItem.Status = _dtFlowSheet.Rows(i)("sStatus")
                                'EGeneralItems.Add(oListItem)
                                _arrLabs.Add(oListItem)
                                oListItem.Dispose()
                                ''
                                'Emylist = New myList
                                'Emylist.AssociatedProperty = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                'Emylist.AssociatedCategory = strLabs.ToString()
                                'Emylist.AssociatedItem = "True"
                                '_arrLabs.Add(Emylist)
                                'oLabs.SetProperty(strAssField.GetValue(0), "True")
                            End With
                        ElseIf _dtFlowSheet.Rows(i)("sAssociatedEMCategory") = strOrders Then
                            With C1Radiology
                                'Emylist = New myList
                                'Emylist.AssociatedProperty = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                'Emylist.AssociatedCategory = strOrders.ToString()
                                'Emylist.AssociatedItem = _dtFlowSheet.Rows(i)("sStatus")
                                '_arrOrders.Add(Emylist)

                                oListItem = New gloGeneralItem.gloItem
                                oListItem.Description = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                oListItem.Code = strOrders.ToString()
                                oListItem.Status = _dtFlowSheet.Rows(i)("sStatus")
                                'EGeneralItems.Add(oListItem)
                                _arrOrders.Add(oListItem)
                                oListItem.Dispose()
                                ''
                                'oXray.SetProperty(strAssField.GetValue(0), "True")
                            End With
                        ElseIf _dtFlowSheet.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis Then
                            With C1Radiology
                                'Emylist = New myList
                                'Emylist.AssociatedProperty = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                                'Emylist.AssociatedItem = _dtFlowSheet.Rows(i)("sStatus")
                                '_arrOtherDiag.Add(Emylist)

                                oListItem = New gloGeneralItem.gloItem
                                oListItem.Description = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                oListItem.Code = strOtherDiagnosis.ToString()
                                oListItem.Status = _dtFlowSheet.Rows(i)("sStatus")
                                'EGeneralItems.Add(oListItem)
                                _arrOtherDiag.Add(oListItem)
                                oListItem.Dispose()
                                'oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
                            End With

                        ElseIf _dtFlowSheet.Rows(i)("sAssociatedEMCategory") = strMangementOption Then
                            With C1Radiology
                                'Emylist = New myList
                                'Emylist.AssociatedProperty = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                                'Emylist.AssociatedItem = _dtFlowSheet.Rows(i)("sStatus")
                                '_arrOtherDiag.Add(Emylist)

                                oListItem = New gloGeneralItem.gloItem
                                oListItem.Description = _dtFlowSheet.Rows(i)("sAssociatedEMName")
                                oListItem.Code = strMangementOption.ToString()
                                oListItem.Status = _dtFlowSheet.Rows(i)("sStatus")
                                'EGeneralItems.Add(oListItem)
                                _arrMangementOption.Add(oListItem)
                                oListItem.Dispose()
                                'oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
                            End With
                        End If
                        'End If
                    End If
                Next
            End If


            _dtDrugs = ocls.GetPatientOTC(_PatientID, _DOS)
            If Not IsNothing(_dtDrugs) Then
                For i As Integer = 0 To _dtDrugs.Rows.Count - 1
                    If _dtDrugs.Rows(0)("RxDrugs") >= 1 Then
                        chkMPerscripmeds.Checked = True
                    End If
                    If _dtDrugs.Rows(0)("OTCDrugs") >= 1 Then
                        chkMOTCmeds.Checked = True
                    End If
                Next
            End If
            'If _strTagEM <> "" Then
            '    strEMTags = Split(_strTagEM, "|")

            '    For i As Integer = 0 To strEMTags.Length - 1
            '        SetFlags(i)
            '    Next
            '    'If strEMTags.Length = 1 Then
            '    '    SetFlags(0)
            '    'ElseIf strEMTags.Length = 2 Then
            '    '    SetFlags(0)
            '    '    SetFlags(1)
            '    'ElseIf strEMTags.Length = 3 Then
            '    '    SetFlags(0)
            '    '    SetFlags(1)
            '    '    SetFlags(2)
            '    'ElseIf strEMTags.Length = 4 Then
            '    '    SetFlags(0)
            '    '    SetFlags(1)
            '    '    SetFlags(2)
            '    '    SetFlags(3)
            '    'End If
            'End If

            'chkMOdiscofcase.Checked = bManagementOption
            'chkRadioperformingPhyscian.Checked = bXray
            'chkLabsperformingPhyscian.Checked = bLabs
            'chkOTDTDiscussperf.Checked = bOtherDiagnostictest

            ''''Temporary Remove the tabs
            'tb_MedicalComplexity.TabPages.Remove(tb_OtherDxTests)
            'tb_MedicalComplexity.TabPages.Remove(tb_Managmentoption)
            'tb_History.TabPages.Remove(tbPageROS)

            If gbOtherPatientType = False Then
                If ocls.IsEstablishedPatient(_PatientID, _DOS) = True Then
                    lblVisitType.Text = "Visit Type : Office of Other Outpatient Services - ESTABLISHED"
                Else
                    lblVisitType.Text = "Visit Type : Office or Other Outpatient Services - NEW"
                End If
                pnlfix.Visible = True
                pnlOthervisittype.Visible = False
            Else
                FillVisitTypeCombo()
                pnlOthervisittype.Visible = True
                pnlfix.Visible = False
            End If

            'Shubhangi 20100105
            'Fill the Combo box of EMExam type & retrive & set the value which is est from the Admin
            FillEMExamType()
            'Set the combo box selected value
            Dim objSettings As New clsSettings
            'COMMENTED BY SHUBHANGI 20110606
            'If CType(objSettings.EMExamType, enumExamControlType) = gEMExamType Then
            '    cmbEMExamType.SelectedIndex = -1
            'Else
            '    cmbEMExamType.Text = gEMExamType.ToString

            'End If
            'End

            ''Added on 20100615 by Mayuri-To save previously selected criteria for diagnosis tab
            Dim j As Int16
            If Not IsNothing(oDiagnosisData) Then
                If (oDiagnosisData.Count > 0) Then
                    cmbcodetype.Text = CType(oDiagnosisData.Item(0), gloEMR.myList).AssociatedCategory.ToString()
                    cmbEMExamType.Text = CType(oDiagnosisData.Item(1), gloEMR.myList).AssociatedCategory.ToString()
                    If CType(oDiagnosisData.Item(10), myList).AssociatedCategory.ToString = "1" Then
                        chkDiagnosis.Checked = True
                    Else
                        chkDiagnosis.Checked = False
                    End If

                    '' Check only for Diagnosis
                    For j = 2 To oDiagnosisData.Count - 2
                        If lblDignosis1.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                'cmbDignosis1.Text = "No Selection"
                                '       'lblDignosis1.Text = "None"
                            Else
                                cmbDignosis1.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis2.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                
                            Else
                                cmbDignosis2.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis3.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                               
                            Else
                                cmbDignosis3.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If


                        If lblDignosis4.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                
                            Else
                                cmbDignosis4.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis5.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                
                            Else
                                cmbDignosis5.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis6.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                
                            Else
                                cmbDignosis6.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis7.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                                
                            Else
                                cmbDignosis7.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                        If lblDignosis8.Text.Trim.ToUpper = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedItem.ToString().Trim.ToUpper Then
                            If CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString() = "" Then
                               
                            Else
                                cmbDignosis8.Text = CType(oDiagnosisData.Item(j), gloEMR.myList).AssociatedCategory.ToString()
                            End If
                        End If

                    Next
                End If
            End If
            ''End code Added on 20100615

            FillEMValuesforTags()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'Shubhangi
    Public Sub FillEMExamType()
        Try
            Dim clist As myList
            Dim arrlist As New List(Of myList)
            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Cardiovascular
            clist.AssociatedCategory = "Cardiovascular"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.EarsNoseThroat
            clist.AssociatedCategory = "Ears Nose Throat"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Eye
            clist.AssociatedCategory = "Eye"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.GeneralMultiSystem
            clist.AssociatedCategory = "General Multiple System"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Genitourinary
            clist.AssociatedCategory = "Genitourinary"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.HemaLymphImmuno
            clist.AssociatedCategory = "HemaLymphImmuno"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Musculoskeletal
            clist.AssociatedCategory = "Musculoskeletal"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Neurological
            clist.AssociatedCategory = "Neurological"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.None
            clist.AssociatedCategory = "None"
            arrlist.Add(clist)

          
            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Pre97Guidelines
            clist.AssociatedCategory = "Pre97Guidelines"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Psychiatric
            clist.AssociatedCategory = "Psychiatric"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Respiratory
            clist.AssociatedCategory = "Respiratory"
            arrlist.Add(clist)

            clist = New myList
            clist.ExamControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.Skin
            clist.AssociatedCategory = "Skin"
            arrlist.Add(clist)

            cmbEMExamType.DataSource = arrlist
            cmbEMExamType.DisplayMember = "AssociatedCategory"
            cmbEMExamType.ValueMember = "ExamControlType"
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'End
    Public Sub FillEMValuesforTags()
        Try
            ''Labs
            Dim strAssociatedProperty As String = ""
            Dim strAssociatedCategory As String = ""
            Dim strItem As String = ""
            For i As Integer = 0 To _arrLabs.Count - 1

                'strAssociatedCategory = CType(_arrLabs.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(_arrLabs.Item(i), myList).AssociatedProperty
                'strItem = CType(_arrLabs.Item(i), myList).AssociatedItem
                strAssociatedCategory = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status

                If strAssociatedProperty = "IncisionalBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLDeepIncisionalBiopsy.Checked = True

                End If
                If strAssociatedProperty = "SuperficialBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then

                    chkLSuperficialbiopsy.Checked = True

                End If
                If strAssociatedProperty = "TypeCrossmatchRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLTypesandCrossmatch.Checked = True

                End If

                If strAssociatedProperty = "PTRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLPT.Checked = True

                End If

                If strAssociatedProperty = "ABGsRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLABGS.Checked = True

                End If
                If strAssociatedProperty = "CardiacEnzymesRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLCardiacenzymes.Checked = True

                End If
                If strAssociatedProperty = "ChemicalProfileRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLChemicalProfile.Checked = True

                End If
                If strAssociatedProperty = "DrugScreenRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLETOH.Checked = True

                End If
                If strAssociatedProperty = "ElectrolytesRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLElectrolytes.Checked = True

                End If

                If strAssociatedProperty = "BunCreatinineRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLBun.Checked = True

                End If
                If strAssociatedProperty = "AmylaseRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLAmylase.Checked = True

                End If

                If strAssociatedProperty = "PregnancyTestRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLPregnancyTest.Checked = True

                End If
                If strAssociatedProperty = "FluStrepMonoRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLFlu.Checked = True

                End If

                If strAssociatedProperty = "CbcUaRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLLCBC.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strLabs.ToString() Then
                    chkLIndependentVisualizationoftest.Checked = True

                End If
                If strAssociatedProperty = "DiscussionWPerformingPhys" And strAssociatedCategory = strLabs.ToString() Then
                    chkLDiscussionwithperformingPhysician.Checked = True

                End If

                If strAssociatedProperty = "OtherLabsCount" And strAssociatedCategory = strLabs.ToString() Then
                    nudLOtherLabs.Value = Convert.ToInt16(strItem)
                End If

            Next




            For i As Integer = 0 To _arrOrders.Count - 1
                'strAssociatedCategory = CType(_arrOrders.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(_arrOrders.Item(i), myList).AssociatedProperty
                'strItem = CType(_arrOrders.Item(i), myList).AssociatedItem

                strAssociatedCategory = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Status

                If strAssociatedProperty = "VascularStudiesWRiskRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXVascularStudieswrisk.Checked = True

                End If
                If strAssociatedProperty = "VascularStudiesRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXVascularStudies.Checked = True

                End If

                If strAssociatedProperty = "MRIRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXMRI.Checked = True

                End If
                If strAssociatedProperty = "CATScanRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXcatScan.Checked = True

                End If
                If strAssociatedProperty = "IVPRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXIVP.Checked = True

                End If

                If strAssociatedProperty = "GIGallbladderRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXGIGallablader.Checked = True

                End If
                If strAssociatedProperty = "TLSpineRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXTLSpire.Checked = True


                End If
                If strAssociatedProperty = "DiscographyRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXDiscographt.Checked = True

                End If
                If strAssociatedProperty = "DiagUltrasoundRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXDiagosticUltrasound.Checked = True

                End If
                If strAssociatedProperty = "CSpineRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXCspine.Checked = True

                End If
                If strAssociatedProperty = "HipPelvisRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXHipPelvis.Checked = True

                End If
                If strAssociatedProperty = "AbdomenRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXAbdomen.Checked = True

                End If
                If strAssociatedProperty = "ExtremitiesRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXExtrimities.Checked = True

                End If
                If strAssociatedProperty = "ChestRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXChest.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strOrders.ToString() Then
                    chkXIndepedent.Checked = True

                End If
                If strAssociatedProperty = "DiscussWPerformingPhys" And strAssociatedCategory = strOrders.ToString() Then
                    chkXperformingPhy.Checked = True

                End If
                If strAssociatedProperty = "OtherXRaysCount" And strAssociatedCategory = strOrders.ToString() Then
                    numupXOther.Value = Convert.ToInt16(strItem)
                End If

            Next

            ''''Other Diagnosis
            For i As Integer = 0 To _arrOtherDiag.Count - 1

                'strAssociatedCategory = CType(_arrOtherDiag.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(_arrOtherDiag.Item(i), myList).AssociatedProperty
                'strItem = CType(_arrOtherDiag.Item(i), myList).AssociatedItem
                strAssociatedCategory = CType(_arrOtherDiag.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(_arrOtherDiag.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(_arrOtherDiag.Item(i), gloGeneralItem.gloItem).Status
                If strAssociatedProperty = "EndoscopeWRiskRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEndoScopewRisk.Checked = True

                End If
                If strAssociatedProperty = "EndoscopeRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEndoscopeworisk.Checked = True

                End If
                If strAssociatedProperty = "CuldocentesesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOCuldcentesis.Checked = True

                End If
                If strAssociatedProperty = "ThoracentesisRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOThoracentesis.Checked = True

                End If
                If strAssociatedProperty = "LumbarPunctureRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOLumbarPunctor.Checked = True

                End If
                If strAssociatedProperty = "NuclearScanRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkONuclearScan.Checked = True

                End If
                If strAssociatedProperty = "PulmonaryStudiesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOPulmonary.Checked = True

                End If
                If strAssociatedProperty = "DopplerFlowStudiesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkODopplerFlowStudies.Checked = True

                End If
                If strAssociatedProperty = "VectorcardiogramRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOVectorCardiogram.Checked = True

                End If
                If strAssociatedProperty = "EegEmgRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEEG.Checked = True

                End If
                If strAssociatedProperty = "TreadmillStressTestRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOTreadmill.Checked = True

                End If
                If strAssociatedProperty = "HolterMonitorRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOHolterMonitor.Checked = True

                End If
                If strAssociatedProperty = "EkgEcgRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEKG.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOIndependentVisualization.Checked = True

                End If
                If strAssociatedProperty = "DiscussWPerformingPhys" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkODiscuswithPerfoming.Checked = True

                End If

                If strAssociatedProperty = "OtherDiagnosticStudiesCount" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    nudDignosisstudies.Value = Convert.ToInt16(strItem)
                End If

            Next



            '''' Managment Option
            For i As Integer = 0 To _arrMangementOption.Count - 1
                'strAssociatedCategory = CType(_arrMangementOption.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(_arrMangementOption.Item(i), myList).AssociatedProperty
                'strItem = CType(_arrMangementOption.Item(i), myList).AssociatedItem

                strAssociatedCategory = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(_arrMangementOption.Item(i), gloGeneralItem.gloItem).Status

                If strAssociatedProperty = "DiscussCaseWHealthProvider" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisionofcase.Checked = True
                End If
                If strAssociatedProperty = "ReviewMedicalRecsOther" And strAssociatedCategory = strMangementOption.ToString() Then
                    ckkMreviewandsummary.Checked = True

                End If
                If strAssociatedProperty = "DecisionObtainMedicalRecsOther" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisiontoobtain.Checked = True

                End If
                If strAssociatedProperty = "DecisionNotResuscitate" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisionnottoresuscitate.Checked = True

                End If
                If strAssociatedProperty = "MajorEmergencySurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajoremergencySurgery.Checked = True

                End If
                If strAssociatedProperty = "MajorSurgeryWRiskFactors" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajorSurgerywrisk.Checked = True

                End If
                If strAssociatedProperty = "MajorSurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajorsurgeryworisk.Checked = True

                End If
                If strAssociatedProperty = "MinorSurgeryWRiskFactors" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMinorSurgeryWrisk.Checked = True

                End If
                If strAssociatedProperty = "MinorSurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMminorsurgeryWOrisk.Checked = True

                End If
                If strAssociatedProperty = "ClosedFx" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMClosefx.Checked = True

                End If
                If strAssociatedProperty = "PhysicalTherapy" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMPhysicalOccupationaltherapy.Checked = True

                End If
                If strAssociatedProperty = "NuclearMedicine" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMNuclearMedicine.Checked = True

                End If
                If strAssociatedProperty = "RespiratoryTreatments" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMRespiratoryTreatment.Checked = True

                End If
                If strAssociatedProperty = "Telemetry" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMTelemetry.Checked = True

                End If
                If strAssociatedProperty = "HighRiskMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMHighRiskmeds.Checked = True

                End If
                If strAssociatedProperty = "IVMedsWAdditives" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMIVmedswadditives.Checked = True

                End If
                If strAssociatedProperty = "IVMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMivmeds.Checked = True

                End If
                If strAssociatedProperty = "PrescripIMMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMPerscripmeds.Checked = True

                End If
                If strAssociatedProperty = "OverCounterMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMOTCmeds.Checked = True

                End If

                If strAssociatedProperty = "ConfWPatientFamilyMinutes" And strAssociatedCategory = strMangementOption.ToString() Then
                    nudMTimeSpent.Value = Convert.ToInt16(strItem)
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#Region "fill EM Labs"

    'Public Sub FillEMLabs()
    '    Try


    '        ''Labs
    '        Dim strAssociatedProperty As String = ""
    '        Dim strAssociatedCategory As String = ""
    '        Dim strItem As String = ""
    '        For i As Integer = 0 To _arrLabs.Count - 1


    '            strAssociatedCategory = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
    '            strAssociatedProperty = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
    '            strItem = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status

    '            If strAssociatedProperty = "IncisionalBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLDeepIncisionalBiopsy.Checked = True

    '            End If
    '            If strAssociatedProperty = "SuperficialBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then

    '                chkLSuperficialbiopsy.Checked = True

    '            End If
    '            If strAssociatedProperty = "TypeCrossmatchRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLTypesandCrossmatch.Checked = True

    '            End If

    '            If strAssociatedProperty = "PTRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLPT.Checked = True

    '            End If

    '            If strAssociatedProperty = "ABGsRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLABGS.Checked = True

    '            End If
    '            If strAssociatedProperty = "CardiacEnzymesRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLCardiacenzymes.Checked = True

    '            End If
    '            If strAssociatedProperty = "ChemicalProfileRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLChemicalProfile.Checked = True

    '            End If
    '            If strAssociatedProperty = "DrugScreenRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLETOH.Checked = True

    '            End If
    '            If strAssociatedProperty = "ElectrolytesRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLElectrolytes.Checked = True

    '            End If

    '            If strAssociatedProperty = "BunCreatinineRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLBun.Checked = True

    '            End If
    '            If strAssociatedProperty = "AmylaseRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLAmylase.Checked = True

    '            End If

    '            If strAssociatedProperty = "PregnancyTestRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLPregnancyTest.Checked = True

    '            End If
    '            If strAssociatedProperty = "FluStrepMonoRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLFlu.Checked = True

    '            End If

    '            If strAssociatedProperty = "CbcUaRoutine" And strAssociatedCategory = strLabs.ToString() Then
    '                chkLLCBC.Checked = True

    '            End If

    '        Next

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#End Region

    Public Sub FillVisitTypeCombo()
        Try
            Dim clist As myList
            Dim arrlist As New List(Of myList)
            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcNew
            clist.AssociatedCategory = "Office or Other Outpatient Services - NEW"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcEstablished
            clist.AssociatedCategory = "Office of Other Outpatient Services - ESTABLISHED"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvc
            clist.AssociatedCategory = "Hospital Observation Services"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvcWAdmissionDischarge
            clist.AssociatedCategory = "Hospital Observation Services w/ Admission and Discharge"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcInitialCare
            clist.AssociatedCategory = "Hospital Inpatient Services - INITIAL CARE"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcSubsequentCare
            clist.AssociatedCategory = "Hospital Inpatient Services - SUBSEQUENT CARE"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.EmergencyDeptSvc
            clist.AssociatedCategory = "Emergency Department Services"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultOfficeOutpatient
            clist.AssociatedCategory = "Consultations: Office of Other Outpatient"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultInitialInpatient
            clist.AssociatedCategory = "Consultations: Inpatient"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilityInitialCompAssesment
            clist.AssociatedCategory = "Nursing Facility: INITIAL Comprehensive Assesment"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilitySubsequentCompAssessment
            clist.AssociatedCategory = "Nursing Facility: SUBSEQUENT"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesNew
            clist.AssociatedCategory = "Home Services - New"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesEstablished
            clist.AssociatedCategory = "Home Services - Established"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesNew
            clist.AssociatedCategory = "Domiciliary, Rest Home, Custodial Care Services - New"
            arrlist.Add(clist)

            clist = New myList
            clist.ControlType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesEstb
            clist.AssociatedCategory = "Domiciliary, Rest Home, Custodial Care Services - Estb"
            arrlist.Add(clist)


            cmbcodetype.DataSource = arrlist
            cmbcodetype.DisplayMember = "AssociatedCategory"
            cmbcodetype.ValueMember = "ControlType"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub SetFlags(ByVal cnt As Integer)
        Try
            If Convert.ToString(strEMTags.GetValue(cnt)).Contains("X-Ray/Radiology") Then
                bXray = True
            ElseIf Convert.ToString(strEMTags.GetValue(cnt)).Contains("Labs") Then
                bLabs = True
            ElseIf Convert.ToString(strEMTags.GetValue(cnt)).Contains("Management Option") Then
                bManagementOption = True
            ElseIf Convert.ToString(strEMTags.GetValue(cnt)).Contains("Other Diagnostic Tests") Then
                bOtherDiagnostictest = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    ''' <summary>
    ''' Fill Management Option data comes from Patient Exam Liquid data
    ''' </summary>
    ''' <param name="oDataCollection"></param>
    ''' <remarks></remarks>
    Public Sub FillMangement_Option(ByVal oDataCollection As CollLiquidData)
        Try
            For i As Integer = 0 To oDataCollection.Count - 1
                Dim arrDetails As New ArrayList
                Dim mydataList As myList
                arrDetails = Nothing
                arrDetails = oDataCollection.Item(i).ArrText_Field
                Dim strAssociatedProperty As String = String.Empty
                mydataList = New myList
                Dim timespent() As String
                If Not IsNothing(arrDetails) Then
                    If Not IsNothing(CType(arrDetails.Item(0), myList).AssociatedProperty) Then
                        strAssociatedProperty = CType(arrDetails.Item(0), myList).AssociatedProperty
                    End If
                    ''strAssociatedProperty	"ConfWPatientFamilyMinutes"	String
                    ''oManagmentOption.ConfWPatientFamilyMinut()
                    If Not IsNothing(strAssociatedProperty) AndAlso strAssociatedProperty <> "" Then
                        If oDataCollection.Item(i).m_datatype = "Boolean" Then
                            If strAssociatedProperty = "DecisionObtainMedicalRecsOthe" Then
                                'chkMODecisointoobtain.Checked = True
                            ElseIf strAssociatedProperty = "ReviewMedicalRecsOther" Then
                                'chkMOreviewofsumm.Checked = True
                            ElseIf strAssociatedProperty = "DiscussCaseWHealthProvider" Then
                                'chkMOdiscofcase.Checked = True
                            End If
                        ElseIf oDataCollection.Item(i).m_datatype = "" Then
                            If strAssociatedProperty = "confWPatientFamilyMinutes" Then
                                If Not IsNothing(CType(arrDetails.Item(0), myList).HistoryItem) Then
                                    timespent = Split(CType(arrDetails.Item(0), myList).HistoryItem, " ")
                                    nudMTimeSpent.Value = Convert.ToDouble(timespent.GetValue(0))
                                End If
                            End If

                        Else
                            If IsNothing(oManagmentOption.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then

                                oManagmentOption.SetProperty(strAssociatedProperty, "True")
                            End If
                        End If
                    End If
                End If

            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    '
    ''' <summary>
    ''' Fill Labs data Comes from Patient Exam Liquid data
    ''' </summary>
    ''' <param name="oDataCollection"></param>
    ''' <remarks></remarks>
    Public Sub FillLabs(ByVal oDataCollection As CollLiquidData)
        Try
            For i As Integer = 0 To oDataCollection.Count - 1
                Dim arrDetails As New ArrayList
                Dim mydataList As myList
                arrDetails = Nothing
                arrDetails = oDataCollection.Item(i).ArrText_Field
                Dim strAssociatedProperty As String

                If Not IsNothing(arrDetails) Then
                    mydataList = New myList
                    strAssociatedProperty = CType(arrDetails.Item(0), myList).AssociatedProperty
                    If Not IsNothing(strAssociatedProperty) AndAlso strAssociatedProperty <> "" Then
                        If oDataCollection.Item(i).m_datatype = "Boolean" Then
                            If strAssociatedProperty = "IndependentVisualTest" Then
                                'chkLabvisualizationoftest.Checked = True
                            ElseIf strAssociatedProperty = "DiscussionWPerformingPhys" Then
                                'chkLabsperformingPhyscian.Checked = True
                            End If
                        ElseIf oDataCollection.Item(i).m_datatype = "" Then
                            If strAssociatedProperty = "OtherLabsCount" Then
                                If Not IsNothing(CType(arrDetails.Item(0), myList).HistoryItem) Then
                                    'timespent = Split(CType(arrDetails.Item(0), myList).HistoryItem, " ")
                                    nudLOtherLabs.Value = Convert.ToDouble(CType(arrDetails.Item(0), myList).HistoryItem)
                                End If
                            End If

                        Else
                            If IsNothing(oLabs.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                oLabs.SetProperty(strAssociatedProperty, "True")
                            End If
                        End If
                    End If
                End If


            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    ''' <summary>
    ''' Fill X-Ray/Radiology data Comes from Patient Exam Liquid data
    ''' </summary>
    ''' <param name="oDataCollection"></param>
    ''' <remarks></remarks>
    Public Sub FillX_Ray_Radiology(ByVal oDataCollection As CollLiquidData)
        Try
            For i As Integer = 0 To oDataCollection.Count - 1
                Dim arrDetails As New ArrayList
                Dim mydataList As myList
                arrDetails = Nothing
                arrDetails = oDataCollection.Item(i).ArrText_Field
                Dim strAssociatedProperty As String
                If Not IsNothing(arrDetails) Then
                    mydataList = New myList
                    strAssociatedProperty = CType(arrDetails.Item(0), myList).AssociatedProperty

                    If Not IsNothing(strAssociatedProperty) AndAlso strAssociatedProperty <> "" Then
                        If oDataCollection.Item(i).m_datatype = "Boolean" Then
                            If strAssociatedProperty = "IndependentVisualTest" Then
                                ' chkRadiovisualizationoftest.Checked = True
                            ElseIf strAssociatedProperty = "DiscussWPerformingPhys" Then
                                ' chkRadioperformingPhyscian.Checked = True
                            End If
                        ElseIf oDataCollection.Item(i).m_datatype = "" Then
                            If strAssociatedProperty = "OtherXRaysCount" Then
                                If Not IsNothing(CType(arrDetails.Item(0), myList).HistoryItem) Then
                                    'timespent = Split(CType(arrDetails.Item(0), myList).HistoryItem, " ")
                                    numupXOther.Value = Convert.ToDouble(CType(arrDetails.Item(0), myList).HistoryItem)
                                End If
                            End If
                        Else
                            If IsNothing(oXray.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                oXray.SetProperty(strAssociatedProperty, "True")
                            End If
                        End If
                    End If
                End If


            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    ''' <summary>
    ''' Fill Other Diagnosis Test data Comes from Patient Exam Liquid data
    ''' </summary>
    ''' <param name="oDataCollection"></param>
    ''' <remarks></remarks>
    Public Sub FillOther_Diagonsis_Tests(ByVal oDataCollection As CollLiquidData)
        Try
            For i As Integer = 0 To oDataCollection.Count - 1
                Dim arrDetails As New ArrayList
                Dim mydataList As myList
                arrDetails = Nothing
                arrDetails = oDataCollection.Item(i).ArrText_Field
                Dim strAssociatedProperty As String
                If Not IsNothing(arrDetails) Then
                    mydataList = New myList
                    strAssociatedProperty = CType(arrDetails.Item(0), myList).AssociatedProperty
                    If Not IsNothing(strAssociatedProperty) AndAlso strAssociatedProperty <> "" Then
                        If oDataCollection.Item(i).m_datatype = "Boolean" Then
                            If strAssociatedProperty = "IndependentVisualTest" Then
                                chkODTindependentvisu.Checked = True
                            ElseIf strAssociatedProperty = "DiscussWPerformingPhys" Then
                                chkOTDTDiscussperf.Checked = True
                            End If
                        ElseIf oDataCollection.Item(i).m_datatype = "" Then
                            If strAssociatedProperty = "speOtherDiagnosticStudiesCount" Then
                                If Not IsNothing(CType(arrDetails.Item(0), myList).HistoryItem) Then
                                    'timespent = Split(CType(arrDetails.Item(0), myList).HistoryItem, " ")
                                    nudDignosisstudies.Value = Convert.ToDouble(CType(arrDetails.Item(0), myList).HistoryItem)
                                End If
                            End If
                        Else
                            If IsNothing(oOtherDigTest.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                oOtherDigTest.SetProperty(strAssociatedProperty, "True")
                            End If
                        End If
                    End If
                End If
                'oOtherDigTest.OtherDiagnosticStudiesCount = numUpdownOtherDiagnosistests.Value

            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    ''' <summary>
    ''' Fill Ros ANd HPI data comes from Patient Exam Liquid data
    ''' </summary>
    ''' <param name="oDataCollection"></param>
    ''' <remarks></remarks>
    Public Sub FillROSnHPI(ByVal oDataCollection As CollLiquidData)
        Try
            For i As Integer = 0 To oDataCollection.Count - 1
                Dim arrDetails As New ArrayList
                Dim mydataList As myList
                arrDetails = Nothing
                arrDetails = oDataCollection.Item(i).ArrText_Field
                Dim strAssociatedProperty As String
                If Not IsNothing(arrDetails) Then
                    mydataList = New myList
                    Dim ty As Type = arrDetails.Item(0).GetType
                    If ty.Name = "myList" Then
                        strAssociatedProperty = CType(arrDetails.Item(0), myList).AssociatedProperty
                        If Not IsNothing(strAssociatedProperty) AndAlso strAssociatedProperty <> "" AndAlso strAssociatedProperty <> "HpiCount" Then
                            If IsNothing(oHistory.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                oHistory.SetProperty(strAssociatedProperty, "True")
                            End If
                        End If
                    End If

                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    ''' <summary>
    ''' Fill Diagnosis for selected diagnosis for patient
    ''' </summary>
    ''' <param name="dc"></param>
    ''' <remarks></remarks>
    Public Sub FilldignosisColl(ByVal dc As AlphaII.CodeWizard.Collections.DiagnosisCollection)
        Try
            If _dc.Count = 1 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
            ElseIf _dc.Count = 2 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
            ElseIf _dc.Count = 3 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
            ElseIf _dc.Count = 4 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
                If Not IsNothing(_dc.Item(3)) Then
                    lblDignosis4.Text = _dc.Item(3).Code
                    cmbDignosis4.Enabled = True
                    FillDiagnosisType(cmbDignosis4)
                End If
            ElseIf _dc.Count = 5 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
                If Not IsNothing(_dc.Item(3)) Then
                    lblDignosis4.Text = _dc.Item(3).Code
                    cmbDignosis4.Enabled = True
                    FillDiagnosisType(cmbDignosis4)
                End If
                If Not IsNothing(_dc.Item(4)) Then
                    lblDignosis5.Text = _dc.Item(4).Code
                    cmbDignosis5.Enabled = True
                    FillDiagnosisType(cmbDignosis5)
                End If
            ElseIf _dc.Count = 6 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
                If Not IsNothing(_dc.Item(3)) Then
                    lblDignosis4.Text = _dc.Item(3).Code
                    cmbDignosis4.Enabled = True
                    FillDiagnosisType(cmbDignosis4)
                End If
                If Not IsNothing(_dc.Item(4)) Then
                    lblDignosis5.Text = _dc.Item(4).Code
                    cmbDignosis5.Enabled = True
                    FillDiagnosisType(cmbDignosis5)
                End If
                If Not IsNothing(_dc.Item(5)) Then
                    ''Mayuri
                    lblDignosis6.Text = _dc.Item(5).Code
                    'lblDignosis6.Text = _dc.Item(6).Code
                    cmbDignosis6.Enabled = True
                    FillDiagnosisType(cmbDignosis6)
                End If
            ElseIf _dc.Count = 7 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
                If Not IsNothing(_dc.Item(3)) Then
                    lblDignosis4.Text = _dc.Item(3).Code
                    cmbDignosis4.Enabled = True
                    FillDiagnosisType(cmbDignosis4)
                End If
                If Not IsNothing(_dc.Item(4)) Then
                    lblDignosis5.Text = _dc.Item(4).Code
                    cmbDignosis5.Enabled = True
                    FillDiagnosisType(cmbDignosis5)
                End If
                If Not IsNothing(_dc.Item(5)) Then
                    ''Modified by Mayuri:to fill diagnosis combo according to selected diagnosis from DxCPT
                    'lblDignosis6.Text = _dc.Item(6).Code
                    lblDignosis6.Text = _dc.Item(5).Code
                    cmbDignosis6.Enabled = True
                    FillDiagnosisType(cmbDignosis6)
                End If
                If Not IsNothing(_dc.Item(6)) Then
                    lblDignosis7.Text = _dc.Item(6).Code
                    cmbDignosis7.Enabled = True
                    FillDiagnosisType(cmbDignosis7)
                End If
            ElseIf _dc.Count = 8 Then
                If Not IsNothing(_dc.Item(0)) Then
                    lblDignosis1.Text = _dc.Item(0).Code
                    cmbDignosis1.Enabled = True
                    FillDiagnosisType(cmbDignosis1)
                End If
                If Not IsNothing(_dc.Item(1)) Then
                    lblDignosis2.Text = _dc.Item(1).Code
                    cmbDignosis2.Enabled = True
                    FillDiagnosisType(cmbDignosis2)
                End If
                If Not IsNothing(_dc.Item(2)) Then
                    lblDignosis3.Text = _dc.Item(2).Code
                    cmbDignosis3.Enabled = True
                    FillDiagnosisType(cmbDignosis3)
                End If
                If Not IsNothing(_dc.Item(3)) Then
                    lblDignosis4.Text = _dc.Item(3).Code
                    cmbDignosis4.Enabled = True
                    FillDiagnosisType(cmbDignosis4)
                End If
                If Not IsNothing(_dc.Item(4)) Then
                    lblDignosis5.Text = _dc.Item(4).Code
                    cmbDignosis5.Enabled = True
                    FillDiagnosisType(cmbDignosis5)
                End If
                If Not IsNothing(_dc.Item(5)) Then
                    ''Mayuri
                    'lblDignosis6.Text = _dc.Item(6).Code
                    lblDignosis6.Text = _dc.Item(5).Code
                    cmbDignosis6.Enabled = True
                    FillDiagnosisType(cmbDignosis6)
                End If
                If Not IsNothing(_dc.Item(6)) Then
                    lblDignosis7.Text = _dc.Item(6).Code
                    cmbDignosis7.Enabled = True
                    FillDiagnosisType(cmbDignosis7)
                End If
                If Not IsNothing(_dc.Item(7)) Then
                    lblDignosis8.Text = _dc.Item(7).Code
                    cmbDignosis8.Enabled = True
                    FillDiagnosisType(cmbDignosis8)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    ''' <summary>
    ''' Fill Diagonsis type in Combobox
    ''' </summary>
    ''' <param name="ocmb"></param>
    ''' <remarks></remarks>
    Public Sub FillDiagnosisType(ByVal ocmb As ComboBox)
        Try
            With ocmb
                Dim oList As myList
                Dim oDiagnosis As New List(Of myList)

                oList = New myList
                oList.AssociatedProperty = "None"
                oList.Value = "No Selection"

                oDiagnosis.Add(oList)

                oList = New myList
                oList.AssociatedProperty = "SelfLimited"
                oList.Value = "Self limited problem"

                oDiagnosis.Add(oList)

                oList = New myList
                oList.AssociatedProperty = "EstablishedSameImproving"
                oList.Value = "Established problem same/improving"

                oDiagnosis.Add(oList)

                oList = New myList
                oList.AssociatedProperty = "EstablishedWorsening"
                oList.Value = "Established problem worsening"

                oDiagnosis.Add(oList)

                oList = New myList
                oList.AssociatedProperty = "NewWithoutAdditionalWorkup"
                oList.Value = "New problem no work up planned"

                oDiagnosis.Add(oList)

                oList = New myList
                oList.AssociatedProperty = "NewWithAdditionalWorkup"
                oList.Value = "New problem additional work up"

                oDiagnosis.Add(oList)


                ocmb.DataSource = oDiagnosis
                ocmb.DisplayMember = "Value"
                ocmb.ValueMember = "AssociatedProperty"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    ''' <summary>
    ''' Design Grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DesignGrid()
        SetColumn(C1Details)
        SetColumn(C1MedicalCondition)
        SetColumn(C1PhysicalExamination)
        SetColumn(C1HPI)
        SetColumn(C1ROS)
    End Sub

    ''' <summary>
    ''' Set Column Width for History,Phyiscal Examination, and Medical complexity
    ''' </summary>
    ''' <param name="oflex"></param>
    ''' <remarks></remarks>
    Public Sub SetColumn(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            With oflex
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = Col_Count
                .AllowEditing = False
                Dim wid As Integer = (Me.Width * 0.3) / 100

                .SetData(0, Col_PatientID, "PatientId")
                .Cols(Col_PatientID).Width = 0
                .Cols(Col_PatientID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_PatientID).AllowEditing = False

                .SetData(0, Col_VisitID, "VisitId")
                .Cols(Col_VisitID).Width = 0
                .Cols(Col_VisitID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_VisitID).AllowEditing = False

                .SetData(0, Col_ExamId, "ExamId")
                .Cols(Col_ExamId).Width = 0
                .Cols(Col_ExamId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_ExamId).AllowEditing = False

                .SetData(0, Col_ElementId, "ElementId")
                .Cols(Col_ElementId).Width = 0
                .Cols(Col_ElementId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_ElementId).AllowEditing = False

                .SetData(0, Col_Helptext, "HelpText")
                .Cols(Col_Helptext).Width = 0
                .Cols(Col_Helptext).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Helptext).AllowEditing = False

                .SetData(0, Col_DataType, "DataType")
                .Cols(Col_DataType).Width = 0
                .Cols(Col_DataType).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_DataType).AllowEditing = False

                .SetData(0, Col_ElementName, "Name")
                .Cols(Col_ElementName).Width = wid * 80
                .Cols(Col_ElementName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_ElementName).AllowEditing = False

                .SetData(0, Col_ElementCategory, "Category")
                .Cols(Col_ElementCategory).Width = wid * 80
                .Cols(Col_ElementCategory).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_ElementCategory).AllowEditing = False

                .SetData(0, Col_HiddenElementCategory, "HiddenCategory")
                .Cols(Col_HiddenElementCategory).Width = 0
                .Cols(Col_HiddenElementCategory).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_HiddenElementCategory).AllowEditing = False

                .SetData(0, Col_ElementDetails, "Details")
                .Cols(Col_ElementDetails).Width = wid * 120
                .Cols(Col_ElementDetails).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_ElementDetails).DataType = GetType(System.String)
                .Cols(Col_ElementDetails).AllowEditing = False

                .SetData(0, Col_HitCount, "Number of Hits")
                .Cols(Col_HitCount).Width = wid * 40
                .Cols(Col_HitCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_HitCount).DataType = GetType(System.String)
                .Cols(Col_HitCount).AllowEditing = False
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    ''' <summary>
    ''' Set column with for X-ray/Radiology,Labs,Managmentoption etc.
    ''' </summary>
    ''' <param name="oflex"></param>
    ''' <remarks></remarks>
    Public Sub SetColumnstdData(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            With oflex
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = Col_FieldCount
                Dim wid As Integer = (Me.Width * 0.3) / 100

                .SetData(0, Col_FieldName, "FieldName")
                .Cols(Col_FieldName).Width = wid * 120
                .Cols(Col_FieldName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .SetData(0, Col_AssociateFieldName, "Associated Field Name")
                .Cols(Col_AssociateFieldName).Width = wid * 120
                .Cols(Col_AssociateFieldName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


#Region "Fill History,PhysciaExam,Medicalcomplixty in flexgrid from Patient Exam note"



    Public Sub FillGeneralData(ByVal oDataCollection As CollLiquidData)
        Try
            With C1Details
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            SetElementHearderStyle(C1Details)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
                            .Rows.Add()
                            NewRow = .Rows.Count - 1

                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    ''.Rows(NewRow
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1Details)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1Details)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = 0 To C1Details.Rows.Count - 1
                                    With C1Details
                                        If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                            Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1Details.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1Details)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1Details)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1Details)

                                SetElementHearderStyle(C1Details)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1Details)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                'Hitcnt = 0
                                                'Hitcnt = Hitcnt + 1
                                                '.SetData(NewRow, Col_HitCount, Hitcnt)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1Details)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1Details.Rows.Count - 1 To 0 Step -1
                            If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                Exit For
                            End If
                            With C1Details
                                If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                    Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                End If
                            End With
                        Next
                        If Total <> 0 Then
                            C1Details.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1Details)
                        End If
                    End If

                Next



                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1Details.Rows.Count - 1
                    With C1Details
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1Details)
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub FillHistoryData(ByVal oDataCollection As CollLiquidData)
        Try
            With C1Details
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then '''' for History Category from db

                            '''' Add Header Row 
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            SetElementHearderStyle(C1Details)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))

                            '''' Add Category Row
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                '''' If Item type is of string then add Item without adding row
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        .SetData(NewRow, Col_HitCount, "8")
                                        'C1Details.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1Details)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "8")
                                        'SetElementTotalStyle(C1Details)
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        .SetData(NewRow, Col_HitCount, "3")
                                        'C1Details.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1Details)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "3")
                                        'SetElementTotalStyle(C1Details)
                                    Else
                                        .SetData(NewRow, Col_HitCount, "1")
                                    End If
                                    ''.Rows(NewRow
                                Else

                                    '''' If datatype is bool or single selection the Add item
                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                        .SetData(NewRow, Col_HitCount, "1")
                                        C1Details.Rows.Add()
                                        NewRow = .Rows.Count - 1
                                        Basicinfo(i, oDataCollection, C1Details)
                                        .SetData(NewRow, Col_ElementDetails, "Total")
                                        .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        .SetData(NewRow, Col_HitCount, "1")
                                        SetElementTotalStyle(C1Details)
                                    Else
                                        '''' If Datatype is of Table or Group the add new row for Item
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1Details)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            '''' If Item is from same category then add item
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        .SetData(NewRow, Col_HitCount, "8")
                                        'C1Details.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1Details)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "8")
                                        'SetElementTotalStyle(C1Details)
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        .SetData(NewRow, Col_HitCount, "3")
                                        'C1Details.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1Details)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "3")
                                        'SetElementTotalStyle(C1Details)
                                    End If
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1Details)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = C1Details.Rows.Count - 1 To 0 Step -1
                                    With C1Details
                                        If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                            Exit For
                                        End If
                                        If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" Then
                                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        Else
                                            If T <> 0 Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If

                                        End If

                                    End With
                                Next
                                'If oDataCollection.Item(i - 1).Title = "Brief" Or oDataCollection.Item(i - 1).Title = "Extended" Then
                                '    Total = 0
                                'End If
                                If Total <> 0 Then
                                    C1Details.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1Details)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1Details)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1Details)

                                SetElementHearderStyle(C1Details)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1Details)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                        If oDataCollection.Item(i).Title.Contains("Extended") Then
                                            .SetData(NewRow, Col_HitCount, "8")
                                            'C1Details.Rows.Add()
                                            'NewRow = .Rows.Count - 1
                                            'Basicinfo(i, oDataCollection, C1Details)
                                            '.SetData(NewRow, Col_ElementDetails, "Total")
                                            '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                            '.SetData(NewRow, Col_HitCount, "8")
                                            'SetElementTotalStyle(C1Details)
                                        ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                            .SetData(NewRow, Col_HitCount, "3")
                                            'C1Details.Rows.Add()
                                            'NewRow = .Rows.Count - 1
                                            'Basicinfo(i, oDataCollection, C1Details)
                                            '.SetData(NewRow, Col_ElementDetails, "Total")
                                            '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                            '.SetData(NewRow, Col_HitCount, "3")
                                            'SetElementTotalStyle(C1Details)
                                        Else
                                            .SetData(NewRow, Col_HitCount, "1")
                                        End If
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1Details))
                                            .SetData(NewRow, Col_HitCount, "1")
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                .SetData(NewRow, Col_HitCount, "1")
                                                'C1Details.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                'Basicinfo(i, oDataCollection, C1Details)
                                                '.SetData(NewRow, Col_ElementDetails, "Total")
                                                '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                                '.SetData(NewRow, Col_HitCount, "1")
                                                'SetElementTotalStyle(C1Details)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1Details)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1Details.Rows.Count - 1 To 0 Step -1
                            If C1Details.Rows.Count > 1 Then
                                If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                    Exit For
                                End If
                                With C1Details
                                    If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
                                        If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                            Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))

                                        End If
                                    Else
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If

                                End With
                            End If
                        Next
                        'If i = 0 Then
                        '    Total = 0
                        'Else
                        '    If oDataCollection.Item(i - 1).Title = "Brief" Or oDataCollection.Item(i - 1).Title = "Extended" Then
                        '        Total = 0
                        '    End If
                        'End If

                        If Total <> 0 Then
                            C1Details.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1Details)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1Details)
                        End If
                    End If

                Next


                For H As Integer = 0 To oDataCollection.Count - 1
                    If oDataCollection.Item(H).HistoryCount <> 0 Then
                        Dim strQry As String = "SELECT nHistoryID, nVisitID, nPatientID, sHistoryCategory, sHistoryItem, sComments, sReaction FROM History WHERE nVisitID = " & oDataCollection.Item(H).mgnVisitID & " AND nPatientID = " & oDataCollection.Item(H).PatientID & ""
                        Dim ODB As New DataBaseLayer
                        dt = New DataTable
                        dt = ODB.GetDataTable_Query(strQry)
                        For HL As Integer = 0 To dt.Rows.Count - 1
                            With C1Details
                                If HL = 0 Then
                                    '''' Add header
                                    .Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(H, oDataCollection, C1Details)
                                    SetElementHearderStyle(C1Details)
                                    .SetData(NewRow, Col_ElementName, "History") ''   dt.Rows(0)("sElementName"))

                                    '''' Add Category
                                    .Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    .SetData(NewRow, Col_ElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                    .SetData(NewRow, Col_HiddenElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                    Hitcnt = 0
                                    Hitcnt = Hitcnt + 1
                                    .SetData(NewRow, Col_HitCount, Hitcnt)
                                    .Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(H, oDataCollection, C1Details)
                                    .SetData(NewRow, Col_HiddenElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                    .SetData(NewRow, Col_ElementDetails, dt.Rows(HL)("sHistoryItem").ToString())
                                Else
                                    If dt.Rows(HL)("sHistoryCategory") <> .GetData(NewRow - 1, Col_HiddenElementCategory) Then
                                        .Rows.Add()
                                        NewRow = .Rows.Count - 1
                                        .SetData(NewRow, Col_ElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                        .SetData(NewRow, Col_HiddenElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        .Rows.Add()
                                        NewRow = .Rows.Count - 1
                                        Basicinfo(H, oDataCollection, C1Details)
                                        .SetData(NewRow, Col_HiddenElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                        .SetData(NewRow, Col_ElementDetails, dt.Rows(HL)("sHistoryItem").ToString())
                                    Else
                                        .Rows.Add()
                                        NewRow = .Rows.Count - 1
                                        Basicinfo(H, oDataCollection, C1Details)
                                        .SetData(NewRow, Col_HiddenElementCategory, dt.Rows(HL)("sHistoryCategory").ToString())
                                        .SetData(NewRow, Col_ElementDetails, dt.Rows(HL)("sHistoryItem").ToString())

                                        Dim c As Integer = 0
                                        For c = NewRow - 1 To 0 Step -1
                                            If .GetData(c, Col_ElementDetails) = "" Then
                                                Hitcnt = Hitcnt + 1
                                                .SetData(c, Col_HitCount, Hitcnt)
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If


                            End With
                            If HL = dt.Rows.Count - 1 Then
                                Total = 0
                                For T As Integer = C1Details.Rows.Count - 1 To 0 Step -1
                                    If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                        Exit For
                                    End If
                                    With C1Details
                                        If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                            Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1Details.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(H, oDataCollection, C1Details)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1Details)
                                End If
                            End If
                        Next
                    End If
                Next

                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1Details.Rows.Count - 1
                    With C1Details
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1Details)
                End If
                Historycount = GrandTotal
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'Public Sub FillPhysicalExam(ByVal oDataCollection As CollLiquidData)
    '    With C1PhysicalExamination
    '        For i As Integer = 0 To oDataCollection.Count - 1
    '            If i = 0 Then
    '                If oDataCollection.Item(i).Title <> "" Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                    SetElementHearderStyle(C1PhysicalExamination)
    '                    .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1

    '                    .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                    .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                    .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                    .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                    .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                    .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    Dim mydataList As myList
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                            ''.Rows(NewRow
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                Hitcnt = 0
    '                                Hitcnt = Hitcnt + 1
    '                                .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                                End If
    '                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                            End If
    '                        End If

    '                    End If
    '                End If
    '            Else
    '                If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    Dim mydataList As myList
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    Dim c As Integer = 0
    '                                    For c = NewRow - 1 To 0 Step -1
    '                                        If .GetData(c, Col_ElementDetails) = "" Then
    '                                            Hitcnt = Hitcnt + 1
    '                                            .SetData(c, Col_HitCount, Hitcnt)
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                Else
    '                                    '.Rows.Add()
    '                                    'NewRow = .Rows.Count - 1

    '                                    'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    Hitcnt = 0
    '                                    Hitcnt = Hitcnt + 1
    '                                    .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                    'End If
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                End If

    '                            End If
    '                        End If

    '                    End If
    '                Else
    '                    If oDataCollection.Item(i).Title <> "" Then
    '                        Total = 0
    '                        For T As Integer = 0 To C1PhysicalExamination.Rows.Count - 1
    '                            With C1PhysicalExamination
    '                                If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                                    Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                                End If
    '                            End With
    '                        Next
    '                        If Total <> 0 Then
    '                            C1PhysicalExamination.Rows.Add()
    '                            NewRow = .Rows.Count - 1
    '                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                            .SetData(NewRow, Col_ElementDetails, "Total")
    '                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                            SetElementTotalStyle(C1PhysicalExamination)
    '                        End If

    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1PhysicalExamination)

    '                        SetElementHearderStyle(C1PhysicalExamination)
    '                        ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
    '                        .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                        .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                        .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                        .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                        .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                        .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                        .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                        Dim arrDetails As New ArrayList
    '                        arrDetails = Nothing
    '                        Dim mydataList As myList
    '                        arrDetails = oDataCollection.Item(i).ArrText_Field
    '                        If Not IsNothing(arrDetails) Then
    '                            mydataList = New myList
    '                            Dim Item As String
    '                            If arrDetails.Item(0).GetType().Name = "String" Then
    '                                Item = arrDetails.Item(0).ToString
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                            Else
    '                                If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
    '                                Else
    '                                    ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                    If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                        'Hitcnt = 0
    '                                        'Hitcnt = Hitcnt + 1
    '                                        '.SetData(NewRow, Col_HitCount, Hitcnt)
    '                                    Else
    '                                        '.Rows.Add()
    '                                        'NewRow = .Rows.Count - 1
    '                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        Hitcnt = 0
    '                                        Hitcnt = Hitcnt + 1
    '                                        .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                        .Rows.Add()
    '                                        NewRow = .Rows.Count - 1
    '                                        Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    End If
    '                                End If
    '                            End If

    '                        End If
    '                    End If
    '                End If
    '            End If
    '            Total = 0

    '            If i = oDataCollection.Count - 1 Then

    '                For T As Integer = C1PhysicalExamination.Rows.Count - 1 To 0 Step -1
    '                    If .GetData(T, Col_HiddenElementCategory) = "Total" Then
    '                        Exit For
    '                    End If
    '                    With C1PhysicalExamination
    '                        If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                            Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                        End If
    '                    End With
    '                Next
    '                If Total <> 0 Then
    '                    C1PhysicalExamination.Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
    '                    .SetData(NewRow, Col_ElementDetails, "Total")
    '                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                    SetElementTotalStyle(C1PhysicalExamination)
    '                End If
    '            End If

    '        Next



    '        Total = 0
    '        GrandTotal = 0
    '        For G As Integer = 0 To C1PhysicalExamination.Rows.Count - 1
    '            With C1PhysicalExamination
    '                If .GetData(G, Col_ElementDetails) = "Total" Then
    '                    GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
    '                End If
    '            End With
    '        Next
    '        If GrandTotal <> 0 Then
    '            .Rows.Add()
    '            NewRow = NewRow + 1
    '            .SetData(NewRow, Col_ElementDetails, "Grand Total")
    '            .SetData(NewRow, Col_HitCount, GrandTotal)
    '            SetElementGrandTotalStyle(C1PhysicalExamination)
    '        End If
    '    End With
    'End Sub

    Public Sub FillPhysicalExam(ByVal oDataCollection As CollLiquidData)
        Try
            With C1PhysicalExamination
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
                            SetElementHearderStyle(C1PhysicalExamination)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
                            .Rows.Add()
                            NewRow = .Rows.Count - 1

                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                    .SetData(NewRow, Col_HitCount, "1")
                                    ''.Rows(NewRow
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        If IsNothing(oExam.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                            oExam.SetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty, "True")
                                        End If


                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                    .SetData(NewRow, Col_HitCount, "1")
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            If IsNothing(oExam.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                                oExam.SetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty, "True")
                                            End If

                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            If IsNothing(oExam.GetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty)) = False Then
                                                oExam.SetProperty(CType(arrDetails.Item(0), myList).AssociatedProperty, "True")
                                            End If

                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = C1PhysicalExamination.Rows.Count - 1 To 0 Step -1
                                    With C1PhysicalExamination
                                        If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                            Exit For
                                        End If
                                        If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
                                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        Else
                                            If T <> 0 Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1PhysicalExamination.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1PhysicalExamination)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1PhysicalExamination)

                                SetElementHearderStyle(C1PhysicalExamination)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1PhysicalExamination))
                                            .SetData(NewRow, Col_HitCount, "1")
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                .SetData(NewRow, Col_HitCount, "1")
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                'Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                                '.SetData(NewRow, Col_ElementDetails, "Total")
                                                '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                                '.SetData(NewRow, Col_HitCount, "1")
                                                'SetElementTotalStyle(C1PhysicalExamination)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1PhysicalExamination)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1PhysicalExamination.Rows.Count - 1 To 0 Step -1
                            If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                Exit For
                            End If
                            With C1PhysicalExamination
                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                Else
                                    If T <> 0 Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                End If

                            End With
                        Next
                        If Total <> 0 Then
                            C1PhysicalExamination.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1PhysicalExamination)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1PhysicalExamination)
                        End If
                    End If

                Next



                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1PhysicalExamination.Rows.Count - 1
                    With C1PhysicalExamination
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1PhysicalExamination)
                End If
                ExamCount = GrandTotal
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Public Sub FillMedicalCondition(ByVal oDataCollection As CollLiquidData)
        Try
            With C1MedicalCondition
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1MedicalCondition)
                            SetElementHearderStyle(C1MedicalCondition)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
                            .Rows.Add()
                            NewRow = .Rows.Count - 1

                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                    .SetData(NewRow, Col_HitCount, "1")
                                    ''.Rows(NewRow
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1MedicalCondition)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1MedicalCondition)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                    .SetData(NewRow, Col_HitCount, "1")
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1MedicalCondition)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = C1MedicalCondition.Rows.Count - 1 To 0 Step -1
                                    With C1MedicalCondition
                                        If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                            Exit For
                                        End If
                                        If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
                                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        Else
                                            If T <> 0 Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1MedicalCondition.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1MedicalCondition)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1MedicalCondition)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1MedicalCondition)

                                SetElementHearderStyle(C1MedicalCondition)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1MedicalCondition)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1MedicalCondition))
                                            .SetData(NewRow, Col_HitCount, "1")
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                .SetData(NewRow, Col_HitCount, "1")
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                'Basicinfo(i, oDataCollection, C1MedicalCondition)
                                                '.SetData(NewRow, Col_ElementDetails, "Total")
                                                '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                                '.SetData(NewRow, Col_HitCount, "1")
                                                'SetElementTotalStyle(C1MedicalCondition)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1MedicalCondition)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1MedicalCondition.Rows.Count - 1 To 0 Step -1
                            If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                Exit For
                            End If
                            With C1MedicalCondition
                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                Else
                                    If T <> 0 Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                End If

                            End With
                        Next
                        If Total <> 0 Then
                            C1MedicalCondition.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1MedicalCondition)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1MedicalCondition)
                        End If
                    End If

                Next



                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1MedicalCondition.Rows.Count - 1
                    With C1MedicalCondition
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1MedicalCondition)
                End If
                MDCount = GrandTotal
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Public Sub FillHPI(ByVal oDataCollection As CollLiquidData)
    '    With C1HPI
    '        For i As Integer = 0 To oDataCollection.Count - 1
    '            If i = 0 Then
    '                If oDataCollection.Item(i).Title <> "" Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    SetElementHearderStyle(C1HPI)
    '                    .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1

    '                    .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                    .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                    .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                    .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                    .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                    .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    Dim mydataList As myList
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                            .SetData(NewRow, Col_HitCount, "1")
    '                            ''.Rows(NewRow
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                Hitcnt = 0
    '                                Hitcnt = Hitcnt + 1
    '                                .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1HPI)
    '                                End If
    '                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                            End If
    '                        End If

    '                    End If
    '                End If
    '            Else
    '                If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    Dim mydataList As myList
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                            .SetData(NewRow, Col_HitCount, "1")
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    Dim c As Integer = 0
    '                                    For c = NewRow - 1 To 0 Step -1
    '                                        If .GetData(c, Col_ElementDetails) = "" Then
    '                                            Hitcnt = Hitcnt + 1
    '                                            .SetData(c, Col_HitCount, Hitcnt)
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                Else
    '                                    '.Rows.Add()
    '                                    'NewRow = .Rows.Count - 1

    '                                    'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    Hitcnt = 0
    '                                    Hitcnt = Hitcnt + 1
    '                                    .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                    'End If
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1HPI)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                End If

    '                            End If
    '                        End If

    '                    End If
    '                Else
    '                    If oDataCollection.Item(i).Title <> "" Then
    '                        Total = 0
    '                        For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
    '                            With C1HPI
    '                                If .GetData(T, Col_HiddenElementCategory) = "Total" Then
    '                                    Exit For
    '                                End If
    '                                If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
    '                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                                    End If
    '                                Else
    '                                    If T <> 0 Then
    '                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                                    End If
    '                                End If
    '                            End With
    '                        Next
    '                        If Total <> 0 Then
    '                            C1HPI.Rows.Add()
    '                            NewRow = .Rows.Count - 1
    '                            Basicinfo(i, oDataCollection, C1HPI)
    '                            .SetData(NewRow, Col_ElementDetails, "Total")
    '                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                            SetElementTotalStyle(C1HPI)
    '                        End If

    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1HPI)

    '                        SetElementHearderStyle(C1HPI)
    '                        ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
    '                        .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1HPI)
    '                        .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                        .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                        .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                        .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                        .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                        .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                        Dim arrDetails As New ArrayList
    '                        arrDetails = Nothing
    '                        Dim mydataList As myList
    '                        arrDetails = oDataCollection.Item(i).ArrText_Field
    '                        If Not IsNothing(arrDetails) Then
    '                            mydataList = New myList
    '                            Dim Item As String
    '                            If arrDetails.Item(0).GetType().Name = "String" Then
    '                                Item = arrDetails.Item(0).ToString
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                    .SetData(NewRow, Col_HitCount, "1")
    '                                Else
    '                                    ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                    If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                        .SetData(NewRow, Col_HitCount, "1")
    '                                        '.Rows.Add()
    '                                        'NewRow = .Rows.Count - 1
    '                                        'Basicinfo(i, oDataCollection, C1HPI)
    '                                        '.SetData(NewRow, Col_ElementDetails, "Total")
    '                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                                        '.SetData(NewRow, Col_HitCount, "1")
    '                                        'SetElementTotalStyle(C1HPI)
    '                                    Else
    '                                        '.Rows.Add()
    '                                        'NewRow = .Rows.Count - 1
    '                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        Hitcnt = 0
    '                                        Hitcnt = Hitcnt + 1
    '                                        .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                        .Rows.Add()
    '                                        NewRow = .Rows.Count - 1
    '                                        Basicinfo(i, oDataCollection, C1HPI)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    End If
    '                                End If
    '                            End If

    '                        End If
    '                    End If
    '                End If
    '            End If
    '            Total = 0

    '            If i = oDataCollection.Count - 1 Then

    '                For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
    '                    If .GetData(T, Col_HiddenElementCategory) = "Total" Then
    '                        Exit For
    '                    End If
    '                    With C1HPI
    '                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
    '                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                            End If
    '                        Else
    '                            If T <> 0 Then
    '                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                            End If
    '                        End If

    '                    End With
    '                Next
    '                If Total <> 0 Then
    '                    C1HPI.Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    .SetData(NewRow, Col_ElementDetails, "Total")
    '                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                    SetElementTotalStyle(C1HPI)
    '                End If
    '            End If

    '        Next



    '        Total = 0
    '        GrandTotal = 0
    '        For G As Integer = 0 To C1HPI.Rows.Count - 1
    '            With C1HPI
    '                If .GetData(G, Col_ElementDetails) = "Total" Then
    '                    GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
    '                End If
    '            End With
    '        Next
    '        If GrandTotal <> 0 Then
    '            .Rows.Add()
    '            NewRow = NewRow + 1
    '            .SetData(NewRow, Col_ElementDetails, "Grand Total")
    '            .SetData(NewRow, Col_HitCount, GrandTotal)
    '            SetElementGrandTotalStyle(C1HPI)
    '        End If
    '    End With
    'End Sub
    Public Sub Basicinfo(ByVal rowno As Integer, ByVal oLiqColl As CollLiquidData, ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        With oflex
            .SetData(NewRow, Col_PatientID, oLiqColl.Item(rowno).PatientID)
            .SetData(NewRow, Col_VisitID, oLiqColl.Item(rowno).mgnVisitID)
            .SetData(NewRow, Col_ExamId, oLiqColl.Item(rowno).examid)
            .SetData(NewRow, Col_ElementId, oLiqColl.Item(rowno).m_elementId)
            .SetData(NewRow, Col_Helptext, oLiqColl.Item(rowno).HelpText)
            .SetData(NewRow, Col_DataType, oLiqColl.Item(rowno).m_datatype)
        End With
    End Sub
    Public Sub SetElementHearderStyle(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            With oflex
                Dim rng As C1.Win.C1FlexGrid.CellRange = .GetCellRange(NewRow, Col_PatientID, NewRow, Col_HitCount)
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                'cStyle = .Styles.Add("Add")
                Try
                    If (.Styles.Contains("Add")) Then
                        cStyle = .Styles("Add")
                    Else
                        cStyle = .Styles.Add("Add")
                        cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                        'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                        cStyle.BackColor = Color.FromArgb(119, 172, 208) '(141, 180, 227)
                        cStyle.ForeColor = Color.White
                        cStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("Add")
                    cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                    'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                    cStyle.BackColor = Color.FromArgb(119, 172, 208) '(141, 180, 227)
                    cStyle.ForeColor = Color.White
                    cStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
                End Try
               
                rng.Style = cStyle
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub SetElementTotalStyle(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            With oflex
                Dim rng As C1.Win.C1FlexGrid.CellRange = .GetCellRange(NewRow, Col_PatientID, NewRow, Col_HitCount)
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                ' cStyle = .Styles.Add("Total")
                Try
                    If (.Styles.Contains("Total")) Then
                        cStyle = .Styles("Total")
                    Else
                        cStyle = .Styles.Add("Total")
                        cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                        'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                        cStyle.ForeColor = Color.Maroon
                        cStyle.BackColor = Color.Bisque                     ''FromArgb(141, 180, 227)
                        ''cStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("Total")
                    cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                    'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                    cStyle.ForeColor = Color.Maroon
                    cStyle.BackColor = Color.Bisque                     ''FromArgb(141, 180, 227)
                    ''cStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
                End Try
               
                rng.Style = cStyle


                Dim _rng As C1.Win.C1FlexGrid.CellRange = .GetCellRange(NewRow, Col_ElementDetails, NewRow, Col_ElementDetails)
                Dim _cStyle As C1.Win.C1FlexGrid.CellStyle
                '_cStyle = .Styles.Add("NewTotal")
                Try
                    If (.Styles.Contains("NewTotal")) Then
                        _cStyle = .Styles("NewTotal")
                    Else
                        _cStyle = .Styles.Add("NewTotal")
                        _cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                        _cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                        _cStyle.ForeColor = Color.Maroon
                        _cStyle.BackColor = Color.Bisque
                    End If
                Catch ex As Exception
                    _cStyle = .Styles.Add("NewTotal")
                    _cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                    _cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    _cStyle.ForeColor = Color.Maroon
                    _cStyle.BackColor = Color.Bisque
                End Try

                
                _rng.Style = _cStyle
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub SetElementGrandTotalStyle(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            With oflex
                Dim rng As C1.Win.C1FlexGrid.CellRange = .GetCellRange(NewRow, Col_PatientID, NewRow, Col_HitCount)
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                'cStyle = .Styles.Add("GrandTotal")
                Try
                    If (.Styles.Contains("GrandTotal")) Then
                        cStyle = .Styles("GrandTotal")
                    Else
                        cStyle = .Styles.Add("GrandTotal")
                        cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                        'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                        cStyle.ForeColor = Color.Maroon
                        cStyle.BackColor = Color.SandyBrown
                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("GrandTotal")
                    cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                    'cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                    cStyle.ForeColor = Color.Maroon
                    cStyle.BackColor = Color.SandyBrown
                End Try

                rng.Style = cStyle

                Dim _rng As C1.Win.C1FlexGrid.CellRange = .GetCellRange(NewRow, Col_ElementDetails, NewRow, Col_ElementDetails)
                Dim _cStyle As C1.Win.C1FlexGrid.CellStyle
                '_cStyle = .Styles.Add("GrandTotal")
                Try
                    If (.Styles.Contains("GrandTotal")) Then
                        _cStyle = .Styles("GrandTotal")
                    Else
                        _cStyle = .Styles.Add("GrandTotal")
                        _cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                        _cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                        _cStyle.ForeColor = Color.Maroon
                        _cStyle.BackColor = Color.SandyBrown
                    End If
                Catch ex As Exception
                    _cStyle = .Styles.Add("GrandTotal")
                    _cStyle.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
                    _cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    _cStyle.ForeColor = Color.Maroon
                    _cStyle.BackColor = Color.SandyBrown
                End Try
              
                _rng.Style = _cStyle
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Function SetRowHeight(ByVal _Item As String, ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            strTemp = ""
            temp = ""
            cnt = 0
            diff = 0
            With oflex
                For l As Integer = 1 To _Item.Length

                    If l = 1 Then
                        strTemp = Mid(_Item, l, 75) '75)
                        temp = strTemp
                    Else    'If l > 0 Then
                        strTemp = Mid(_Item, l, 75)
                        temp = temp & vbNewLine(1) & strTemp

                    End If
                    l = l + 74
                    cnt = cnt + 1
                Next


                If cnt <= 10 Then
                    .Rows(NewRow).Height = (cnt) * 20
                Else
                    If cnt >= 110 Then
                        diff = cnt / 110
                    End If
                    .Rows(NewRow).Height = (cnt - diff) * 15
                End If
                Return temp
            End With
            '.Rows(NewRow).AllowResizing = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Public Sub FillHPI(ByVal oDataCollection As CollLiquidData)
        Try
            With C1HPI
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1HPI)
                            SetElementHearderStyle(C1HPI)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
                            .Rows.Add()
                            NewRow = .Rows.Count - 1

                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    'Item = arrDetails.Item(0).ToString
                                    '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                    '.SetData(NewRow, Col_HitCount, "1")


                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        IsHPIExtended = True
                                        .SetData(NewRow, Col_HitCount, "8")
                                        'C1HPI.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1HPI)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "8")
                                        'SetElementTotalStyle(C1HPI)
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        IsHPIBrief = True
                                        .SetData(NewRow, Col_HitCount, "3")
                                        'C1HPI.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1HPI)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "3")
                                        'SetElementTotalStyle(C1HPI)
                                    Else
                                        .SetData(NewRow, Col_HitCount, "1")
                                    End If
                                    ''.Rows(NewRow
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1HPI)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1HPI)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    'Item = arrDetails.Item(0).ToString
                                    '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                    '.SetData(NewRow, Col_HitCount, "1")
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        IsHPIExtended = True
                                        .SetData(NewRow, Col_HitCount, "8")
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        IsHPIBrief = True
                                        .SetData(NewRow, Col_HitCount, "3")
                                    End If
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1HPI)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
                                    With C1HPI
                                        If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                            Exit For
                                        End If
                                        If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
                                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        Else
                                            If T <> 0 Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1HPI.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1HPI)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1HPI)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1HPI)

                                SetElementHearderStyle(C1HPI)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1HPI)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        'Item = arrDetails.Item(0).ToString
                                        '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                        '.SetData(NewRow, Col_HitCount, "1")
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                        If oDataCollection.Item(i).Title.Contains("Extended") Then
                                            IsHPIExtended = True
                                            .SetData(NewRow, Col_HitCount, "8")

                                        ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                            IsHPIBrief = True
                                            .SetData(NewRow, Col_HitCount, "3")
                                        Else
                                            .SetData(NewRow, Col_HitCount, "1")
                                        End If
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
                                            .SetData(NewRow, Col_HitCount, "1")
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                .SetData(NewRow, Col_HitCount, "1")
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                'Basicinfo(i, oDataCollection, C1HPI)
                                                '.SetData(NewRow, Col_ElementDetails, "Total")
                                                '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                                '.SetData(NewRow, Col_HitCount, "1")
                                                'SetElementTotalStyle(C1HPI)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1HPI)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
                            If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                Exit For
                            End If
                            With C1HPI
                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                Else
                                    If T <> 0 Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                End If

                            End With
                        Next
                        If Total <> 0 Then
                            C1HPI.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1HPI)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1HPI)
                        End If
                    End If

                Next



                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1HPI.Rows.Count - 1
                    With C1HPI
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1HPI)
                End If
                HPICount = GrandTotal
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub FillROS(ByVal oDataCollection As CollLiquidData)
        Try
            With C1ROS
                For i As Integer = 0 To oDataCollection.Count - 1
                    If i = 0 Then
                        If oDataCollection.Item(i).Title <> "" Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1ROS)
                            SetElementHearderStyle(C1ROS)
                            .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
                            .Rows.Add()
                            NewRow = .Rows.Count - 1

                            .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                            .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                            .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                            .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                            .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                            .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            Dim mydataList As myList
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    'Item = arrDetails.Item(0).ToString
                                    '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                    '.SetData(NewRow, Col_HitCount, "1")


                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        IsHPIExtended = True
                                        .SetData(NewRow, Col_HitCount, "8")
                                        'C1ROS.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1ROS)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "8")
                                        'SetElementTotalStyle(C1ROS)
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        IsHPIBrief = True
                                        .SetData(NewRow, Col_HitCount, "3")
                                        'C1ROS.Rows.Add()
                                        'NewRow = .Rows.Count - 1
                                        'Basicinfo(i, oDataCollection, C1ROS)
                                        '.SetData(NewRow, Col_ElementDetails, "Total")
                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                        '.SetData(NewRow, Col_HitCount, "3")
                                        'SetElementTotalStyle(C1ROS)
                                    Else
                                        .SetData(NewRow, Col_HitCount, "1")
                                    End If
                                    ''.Rows(NewRow
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                        Hitcnt = 0
                                        Hitcnt = Hitcnt + 1
                                        .SetData(NewRow, Col_HitCount, Hitcnt)
                                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1ROS)
                                        End If
                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                    End If
                                End If

                            End If
                        End If
                    Else
                        If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1ROS)
                            Dim arrDetails As New ArrayList
                            arrDetails = Nothing
                            Dim mydataList As myList
                            arrDetails = oDataCollection.Item(i).ArrText_Field
                            If Not IsNothing(arrDetails) Then
                                mydataList = New myList
                                Dim Item As String
                                If arrDetails.Item(0).GetType().Name = "String" Then
                                    'Item = arrDetails.Item(0).ToString
                                    '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                    '.SetData(NewRow, Col_HitCount, "1")
                                    Item = arrDetails.Item(0).ToString
                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                    If oDataCollection.Item(i).Title.Contains("Extended") Then
                                        IsHPIExtended = True
                                        .SetData(NewRow, Col_HitCount, "8")
                                    ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                        IsHPIBrief = True
                                        .SetData(NewRow, Col_HitCount, "3")
                                    End If
                                Else

                                    If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                        Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                        .SetData(NewRow, Col_HitCount, "1")
                                    Else
                                        ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                        If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            Dim c As Integer = 0
                                            For c = NewRow - 1 To 0 Step -1
                                                If .GetData(c, Col_ElementDetails) = "" Then
                                                    Hitcnt = Hitcnt + 1
                                                    .SetData(c, Col_HitCount, Hitcnt)
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            '.Rows.Add()
                                            'NewRow = .Rows.Count - 1

                                            'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            Hitcnt = 0
                                            Hitcnt = Hitcnt + 1
                                            .SetData(NewRow, Col_HitCount, Hitcnt)
                                            'End If
                                            .Rows.Add()
                                            NewRow = .Rows.Count - 1
                                            Basicinfo(i, oDataCollection, C1ROS)
                                            .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                            .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                        End If

                                    End If
                                End If

                            End If
                        Else
                            If oDataCollection.Item(i).Title <> "" Then
                                Total = 0
                                For T As Integer = C1ROS.Rows.Count - 1 To 0 Step -1
                                    With C1ROS
                                        If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                            Exit For
                                        End If
                                        If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
                                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        Else
                                            If T <> 0 Then
                                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                            End If
                                        End If
                                    End With
                                Next
                                If Total <> 0 Then
                                    C1ROS.Rows.Add()
                                    NewRow = .Rows.Count - 1
                                    Basicinfo(i, oDataCollection, C1ROS)
                                    .SetData(NewRow, Col_ElementDetails, "Total")
                                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
                                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                                    SetElementTotalStyle(C1ROS)
                                End If

                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1ROS)

                                SetElementHearderStyle(C1ROS)
                                ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
                                .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                Basicinfo(i, oDataCollection, C1ROS)
                                .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
                                .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
                                .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
                                .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
                                .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
                                .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
                                Dim arrDetails As New ArrayList
                                arrDetails = Nothing
                                Dim mydataList As myList
                                arrDetails = oDataCollection.Item(i).ArrText_Field
                                If Not IsNothing(arrDetails) Then
                                    mydataList = New myList
                                    Dim Item As String
                                    If arrDetails.Item(0).GetType().Name = "String" Then
                                        'Item = arrDetails.Item(0).ToString
                                        '.Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        '.SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                        '.SetData(NewRow, Col_HitCount, "1")
                                        Item = arrDetails.Item(0).ToString
                                        .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                        .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                        If oDataCollection.Item(i).Title.Contains("Extended") Then
                                            IsHPIExtended = True
                                            .SetData(NewRow, Col_HitCount, "8")

                                        ElseIf oDataCollection.Item(i).Title.Contains("Brief") Then
                                            IsHPIBrief = True
                                            .SetData(NewRow, Col_HitCount, "3")
                                        Else
                                            .SetData(NewRow, Col_HitCount, "1")
                                        End If
                                    Else
                                        If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
                                            Item = CType(arrDetails.Item(0), myList).HistoryItem()
                                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
                                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1ROS))
                                            .SetData(NewRow, Col_HitCount, "1")
                                        Else
                                            ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
                                            If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                                .SetData(NewRow, Col_HitCount, "1")
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                'Basicinfo(i, oDataCollection, C1ROS)
                                                '.SetData(NewRow, Col_ElementDetails, "Total")
                                                '.SetData(NewRow, Col_HiddenElementCategory, "Total")
                                                '.SetData(NewRow, Col_HitCount, "1")
                                                'SetElementTotalStyle(C1ROS)
                                            Else
                                                '.Rows.Add()
                                                'NewRow = .Rows.Count - 1
                                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                Hitcnt = 0
                                                Hitcnt = Hitcnt + 1
                                                .SetData(NewRow, Col_HitCount, Hitcnt)
                                                .Rows.Add()
                                                NewRow = .Rows.Count - 1
                                                Basicinfo(i, oDataCollection, C1ROS)
                                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
                                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                    Total = 0

                    If i = oDataCollection.Count - 1 Then

                        For T As Integer = C1ROS.Rows.Count - 1 To 0 Step -1
                            If .GetData(T, Col_HiddenElementCategory) = "Total" Then
                                Exit For
                            End If
                            With C1ROS
                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                Else
                                    If T <> 0 Then
                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
                                    End If
                                End If

                            End With
                        Next
                        If Total <> 0 Then
                            C1ROS.Rows.Add()
                            NewRow = .Rows.Count - 1
                            Basicinfo(i, oDataCollection, C1ROS)
                            .SetData(NewRow, Col_ElementDetails, "Total")
                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
                            SetElementTotalStyle(C1ROS)
                        End If
                    End If

                Next



                Total = 0
                GrandTotal = 0
                For G As Integer = 0 To C1ROS.Rows.Count - 1
                    With C1ROS
                        If .GetData(G, Col_ElementDetails) = "Total" Then
                            GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
                        End If
                    End With
                Next
                If GrandTotal <> 0 Then
                    .Rows.Add()
                    NewRow = NewRow + 1
                    .SetData(NewRow, Col_ElementDetails, "Grand Total")
                    .SetData(NewRow, Col_HitCount, GrandTotal)
                    SetElementGrandTotalStyle(C1ROS)
                End If
                HPICount = GrandTotal
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region




    Private Sub tls_Liquiddata_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Liquiddata.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString
                Case "EMCode"
                    Dim emResult As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerateResult = GetEMCode()
                    Dim ofrm As New frmEMResult(emResult)
                    With ofrm
                        Me.TopMost = False
                        .StartPosition = FormStartPosition.CenterScreen
                        For Each myForm As Form In Application.OpenForms
                            If (myForm.TopMost) Then
                                myForm.TopMost = False
                            End If
                        Next
                        .TopMost = True
                        .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        _EmCode = .EMCode
                        _Result = .Result
                        For Each myForm As Form In Application.OpenForms
                            If (myForm.TopMost) Then
                                myForm.TopMost = False
                            End If
                        Next
                        Me.TopMost = True
                        If _Result = "Accept" Then
                            Me.Close()
                        End If

                    End With
                    ofrm.Dispose()

                Case "Close"

                    Me.Close()

            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#Region "Save Diagnosis Data"
    Private Sub SaveDiagnosisData(ByVal _dc As AlphaII.CodeWizard.Collections.DiagnosisCollection)
        Try
            ''Added on 20100615 by Mayuri:to save data on diagnosis tab
            Dim oList As myList
            oDiagnosisData = New ArrayList

            oList = New myList
            oList.AssociatedCategory = cmbcodetype.Text
            oList.AssociatedItem = ""
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbEMExamType.Text
            oList.AssociatedItem = ""
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis1.Text
            oList.AssociatedItem = lblDignosis1.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis2.Text
            oList.AssociatedItem = lblDignosis2.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis3.Text
            oList.AssociatedItem = lblDignosis3.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis4.Text
            oList.AssociatedItem = lblDignosis4.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis5.Text
            oList.AssociatedItem = lblDignosis5.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis6.Text
            oList.AssociatedItem = lblDignosis6.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis7.Text
            oList.AssociatedItem = lblDignosis7.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            oList.AssociatedCategory = cmbDignosis8.Text
            oList.AssociatedItem = lblDignosis8.Text
            oDiagnosisData.Add(oList)

            oList = New myList
            If chkDiagnosis.Checked = True Then
                oList.AssociatedCategory = "1"
            Else
                oList.AssociatedCategory = "0"
            End If
            oDiagnosisData.Add(oList)
            ''End code Added on 20100615

        Catch ex As Exception

        End Try
    End Sub
#End Region




#Region "Generate EM Code"



    Public Function GetEMCode() As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerateResult
        Try
            Dim oGenrate As New AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerateResult
            Dim ocoding As New AlphaII.CodeWizard.Coding
            Dim oEmGentrator As New AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerate
            Dim ocls As New clsLiquiddbLayer

            GetDiagnosis()

            GetHistory()

            GetMedicalComplxity()


            oEmGentrator.History = oHistory
            oEmGentrator.MedicalComplexity = oMd
            oEmGentrator.Exam.GeneralMultiSystem = oExam
            oEmGentrator.MedicalComplexity.Diagnosis = diagnosis

            '''' Set Code type
            ' If pnlfix.Visible = True Then
            'Shubhangi 20091224
            'Check cmbcodetype combo box is visible or not
            'Check the global variable Patient Type which we set from admin
            If gbOtherPatientType = False Then
                ' If cmbcodetype.Visible = False Then
                If ocls.IsEstablishedPatient(_PatientID, _DOS) = True Then
                    oEmGentrator.CodeType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcEstablished
                Else
                    oEmGentrator.CodeType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcNew
                End If
            Else
                oEmGentrator.CodeType = cmbcodetype.SelectedValue
            End If

            'COMMNTED BY SHUBHANGI 20110606
            'Shubhangi 20100105
            'Check the selected value of combobox EMExam type or set the Change vlue of EMExam type
            'If gEMExamType = EvalMgmtExamType.None Or CType(cmbEMExamType.SelectedValue, enumExamControlType) = enumExamControlType.None Then
            '    oEmGentrator.Exam.ExamType = AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType.None
            'Else
            '    ''enumExamControlType
            '    oEmGentrator.Exam.ExamType = CType(cmbEMExamType.SelectedValue, enumExamControlType)
            'End If
            ''Added by Mayuri:20100615-to save EMExamType field in PatientExams table
            _EMExamType = cmbEMExamType.Text
            Return ocoding.GenerateEvalMgmt(oEmGentrator)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function


    Public Sub GetDiagnosis()
        Try
            If cmbDignosis1.Enabled = True Then
                diagnosis.Diagnosis1 = lblDignosis1.Text
                If cmbDignosis1.Text <> "No Selection" Then
                    diagnosis.Diagnosis1ProbType = GetEnumType(CType(cmbDignosis1.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis1ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis2.Enabled = True Then
                diagnosis.Diagnosis2 = lblDignosis2.Text
                If cmbDignosis2.Text <> "No Selection" Then
                    diagnosis.Diagnosis2ProbType = GetEnumType(CType(cmbDignosis2.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis2ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis3.Enabled = True Then
                diagnosis.Diagnosis3 = lblDignosis3.Text
                If cmbDignosis3.Text <> "No Selection" Then
                    diagnosis.Diagnosis3ProbType = GetEnumType(CType(cmbDignosis3.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis3ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If

            End If
            If cmbDignosis4.Enabled = True Then
                diagnosis.Diagnosis4 = lblDignosis4.Text
                If cmbDignosis4.Text <> "No Selection" Then
                    diagnosis.Diagnosis4ProbType = GetEnumType(CType(cmbDignosis4.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis4ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis5.Enabled = True Then
                diagnosis.Diagnosis5 = lblDignosis5.Text
                If cmbDignosis5.Text <> "No Selection" Then
                    diagnosis.Diagnosis5ProbType = GetEnumType(CType(cmbDignosis5.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis5ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis6.Enabled = True Then
                diagnosis.Diagnosis6 = lblDignosis6.Text
                If cmbDignosis6.Text <> "No Selection" Then
                    diagnosis.Diagnosis6ProbType = GetEnumType(CType(cmbDignosis6.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis6ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis7.Enabled = True Then
                diagnosis.Diagnosis7 = lblDignosis7.Text
                If cmbDignosis7.Text <> "No Selection" Then
                    diagnosis.Diagnosis7ProbType = GetEnumType(CType(cmbDignosis7.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis7ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If
            If cmbDignosis8.Enabled = True Then
                diagnosis.Diagnosis8 = lblDignosis8.Text
                If cmbDignosis8.Text <> "No Selection" Then
                    diagnosis.Diagnosis8ProbType = GetEnumType(CType(cmbDignosis8.SelectedItem, myList).AssociatedProperty)
                Else
                    diagnosis.Diagnosis8ProbType = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.None
                End If
            End If

            diagnosis.IllnessSevereSideEffectTreatment = chkDiagnosis.Checked
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub GetHistory()
        Try
            'Set HPI,ROS,PHPS
            If IsHPIExtended = True Then
                'oHistory.HpiContext = True
                'oHistory.HpiDuration = True
                'oHistory.HpiLocation = True
                'oHistory.HpiModifyingFactors = True
                'oHistory.HpiQuality = True
                'oHistory.HpiSeverity = True
                'oHistory.HpiSignsSymptoms = True
                'oHistory.HpiTiming = True
                oHistory.HpiCount = 8
            End If
            If IsHPIBrief = True Then
                'oHistory.HpiLocation = True
                'oHistory.HpiContext = True
                'oHistory.HpiDuration = True
                oHistory.HpiCount = 3
            End If
            If IsHPIExtended = False And IsHPIBrief = False Then
                oHistory.HpiCount = 0
            End If
            'oHistory.RosCount = Historycount
            oHistory.FamilyHistory = ocls.IsHistoryPresent(_PatientID, _VisitID, "Family History")
            oHistory.SocialHistory = ocls.IsHistoryPresent(_PatientID, _VisitID, "Social History")
            oHistory.PersonalHistory = ocls.IsHistoryPresent(_PatientID, _VisitID, "Past Medical History")

            'Shubhangi 20090305'
            'Check For Flag

            If gstrChiefComplaintType = "ProblemList" Then
                oHistory.ChiefComplaintDoc = ocls.IsChiefComplentPresentProblemList(_PatientID, _VisitID, _DOS)
                'ocls.IsChiefComplentPresentProblemList()

            ElseIf gstrChiefComplaintType = "ChiefComplaint" Then
                oHistory.ChiefComplaintDoc = ocls.IsChiefComplentPresent(_PatientID, _VisitID, _ExamID, _VisitDate)
            End If
            'End Shubhangi'
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub GetMedicalComplxity()
        Try
            ''''Set Medical COmplexity

            GetLabs()
            GetRadiology()
            GetManagementOption()
            GetOtherDiagnosistest()

            oMd.ManagementOptions = oManagmentOption
            oMd.Labs = oLabs
            oMd.XrayRadiology = oXray
            oMd.OtherDiagnosticTests = oOtherDigTest
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Function GetEnumType(ByVal str As String) As AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType
        Try
            If str = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.SelfLimited.ToString() Then
                Return AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.SelfLimited
            ElseIf str = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.EstablishedSameImproving.ToString() Then
                Return AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.EstablishedSameImproving
            ElseIf str = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.EstablishedWorsening.ToString() Then
                Return AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.EstablishedWorsening
            ElseIf str = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.NewWithAdditionalWorkup.ToString() Then
                Return AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.NewWithAdditionalWorkup
            ElseIf str = AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.NewWithoutAdditionalWorkup.ToString() Then
                Return AlphaII.CodeWizard.Objects.EvaluationManagement.PresentingProblemType.NewWithoutAdditionalWorkup
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Public Sub GetLabs()
        Try
            'If Not IsNothing(_dtLabs) Then
            '    If _dtLabs.Rows.Count > 0 Then
            '        For i As Integer = 0 To _dtLabs.Rows.Count - 1
            '            strAssociateEMField = _dtLabs.Rows(i)("sAssociatedEMName")
            '            If Not IsNothing(strAssociateEMField) And strAssociateEMField <> "" Then
            '                If IsNothing(oLabs.GetProperty(strAssociateEMField)) = False Then
            '                    oLabs.SetProperty(strAssociateEMField, "True")
            '                End If
            '            End If
            '        Next
            '    End If
            'End If
            'oLabs.OtherLabsCount = numUPdownLabs.Value
            'oLabs.DiscussionWPerformingPhys = chkLabsperformingPhyscian.Checked
            'oLabs.IndependentVisualTest = chkLabvisualizationoftest.Checked
            setSelectedLabs()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub setSelectedLabs()
        Try
            If chkLDeepIncisionalBiopsy.Checked = True Then
                oLabs.SetProperty("IncisionalBiopsyRoutine", "True")
            End If
            If chkLSuperficialbiopsy.Checked = True Then
                oLabs.SetProperty("SuperficialBiopsyRoutine", "True")
            End If
            If chkLTypesandCrossmatch.Checked = True Then

                oLabs.SetProperty("TypeCrossmatchRoutine", "True")
            End If
            If chkLPT.Checked = True Then

                oLabs.SetProperty("PTRoutine", "True")
            End If
            If chkLABGS.Checked = True Then
                oLabs.SetProperty("ABGsRoutine", "True")
            End If
            If chkLCardiacenzymes.Checked = True Then
                oLabs.SetProperty("CardiacEnzymesRoutine", "True")
            End If
            If chkLChemicalProfile.Checked = True Then
                oLabs.SetProperty("ChemicalProfileRoutine", "True")
            End If
            If chkLETOH.Checked = True Then
                oLabs.SetProperty("DrugScreenRoutine", "True")
            End If
            If chkLElectrolytes.Checked = True Then
                oLabs.SetProperty("ElectrolytesRoutine", "True")
            End If
            If chkLBun.Checked = True Then
                oLabs.SetProperty("BunCreatinineRoutine", "True")
            End If
            If chkLAmylase.Checked = True Then
                oLabs.SetProperty("AmylaseRoutine", "True")
            End If
            If chkLPregnancyTest.Checked = True Then
                oLabs.SetProperty("PregnancyTestRoutine", "True")
            End If
            If chkLFlu.Checked = True Then
                oLabs.SetProperty("FluStrepMonoRoutine", "True")
            End If
            If chkLLCBC.Checked = True Then
                oLabs.SetProperty("CbcUaRoutine", "True")
            End If
            If chkLIndependentVisualizationoftest.Checked = True Then
                oLabs.SetProperty("IndependentVisualTest", "True")
            End If
            If chkLDiscussionwithperformingPhysician.Checked = True Then
                oLabs.SetProperty("DiscussionWPerformingPhys", "True")
            End If

            If nudLOtherLabs.Value <> 0 Then
                oLabs.SetProperty("OtherLabsCount", Convert.ToInt16(nudLOtherLabs.Value))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub GetRadiology()
        'If Not IsNothing(_dtRadiology) Then
        '    Dim strAssField() As String
        '    If _dtRadiology.Rows.Count > 0 Then
        '        For i As Integer = 0 To _dtRadiology.Rows.Count - 1
        '            strAssField = Split(_dtRadiology.Rows(i)("sAssociatedEMName"), "-")
        '            If strAssField.Length = 2 Then
        '                If strAssField.GetValue(1) = CategoryType.X_Ray_Radiology.GetHashCode() Then

        '                    oXray.SetProperty(strAssField.GetValue(0), "True")

        '                ElseIf strAssField.GetValue(1) = CategoryType.Other_Diagonsis_Tests.GetHashCode() Then
        '                    oOtherDigTest.SetProperty(strAssField.GetValue(0), "True")
        '                End If
        '            End If


        '        Next
        '    End If
        'End If
        'oXray.OtherXRaysCount = numUPdownLabs.Value
        'oXray.DiscussWPerformingPhys = chkRadioperformingPhyscian.Checked
        'oXray.IndependentVisualTest = chkRadiovisualizationoftest.Checked
        setSelectedOrders()

    End Sub
    Public Sub setSelectedOrders()
        Try
            If chkXVascularStudieswrisk.Checked = True Then
                oXray.SetProperty("VascularStudiesWRiskRoutine", "True")
            End If
            If chkXVascularStudies.Checked = True Then
                oXray.SetProperty("VascularStudiesRoutine", "True")
            End If
            If chkXMRI.Checked = True Then
                oXray.SetProperty("MRIRoutine", "True")
            End If
            If chkXcatScan.Checked = True Then
                oXray.SetProperty("CATScanRoutine", "True")
            End If
            If chkXIVP.Checked = True Then
                oXray.SetProperty("IVPRoutine", "True")
            End If
            If chkXGIGallablader.Checked = True Then
                oXray.SetProperty("GIGallbladderRoutine", "True")
            End If
            If chkXTLSpire.Checked = True Then
                oXray.SetProperty("TLSpineRoutine", "True")
            End If
            If chkXDiscographt.Checked = True Then
                oXray.SetProperty("DiscographyRoutine", "True")
            End If
            If chkXDiagosticUltrasound.Checked = True Then
                oXray.SetProperty("DiagUltrasoundRoutine", "True")
            End If
            If chkXCspine.Checked = True Then
                oXray.SetProperty("CSpineRoutine", "True")
            End If
            If chkXHipPelvis.Checked = True Then
                oXray.SetProperty("HipPelvisRoutine", "True")
            End If
            If chkXAbdomen.Checked = True Then
                oXray.SetProperty("AbdomenRoutine", "True")
            End If
            If chkXExtrimities.Checked = True Then
                oXray.SetProperty("ExtremitiesRoutine", "True")
            End If
            If chkXChest.Checked = True Then
                oXray.SetProperty("ChestRoutine", "True")
            End If
            If chkXIndepedent.Checked = True Then
                oXray.SetProperty("IndependentVisualTest", "True")
            End If
            If chkXperformingPhy.Checked = True Then
                oXray.SetProperty("DiscussWPerformingPhys", "True")
            End If

            If numupXOther.Value <> 0 Then
                oXray.SetProperty("OtherXRaysCount", Convert.ToInt16(numupXOther.Value))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub GetOtherDiagnosistest()
        'oOtherDigTest.OtherDiagnosticStudiesCount = numUpdownOtherDiagnosistests.Value
        'oOtherDigTest.IndependentVisualTest = chkODTindependentvisu.Checked
        'oOtherDigTest.DiscussWPerformingPhys = chkOTDTDiscussperf.Checked
        setSelectedOtherDiagnosisTest()
    End Sub

    Public Sub setSelectedOtherDiagnosisTest()
        Try
            If chkOEndoScopewRisk.Checked = True Then
                oOtherDigTest.SetProperty("EndoscopeWRiskRoutine", "True")
            End If
            If chkOEndoscopeworisk.Checked = True Then
                oOtherDigTest.SetProperty("EndoscopeRoutine", "True")
            End If
            If chkOCuldcentesis.Checked = True Then
                oOtherDigTest.SetProperty("CuldocentesesRoutine", "True")
            End If
            If chkOThoracentesis.Checked = True Then
                oOtherDigTest.SetProperty("ThoracentesisRoutine", "True")
            End If
            If chkOLumbarPunctor.Checked = True Then
                oOtherDigTest.SetProperty("LumbarPunctureRoutine", "True")
            End If
            If chkONuclearScan.Checked = True Then
                oOtherDigTest.SetProperty("NuclearScanRoutine", "True")
            End If
            If chkOPulmonary.Checked = True Then
                oOtherDigTest.SetProperty("PulmonaryStudiesRoutine", "True")
            End If
            If chkODopplerFlowStudies.Checked = True Then
                oOtherDigTest.SetProperty("DopplerFlowStudiesRoutine", "True")
            End If
            If chkOVectorCardiogram.Checked = True Then
                oOtherDigTest.SetProperty("VectorcardiogramRoutine", "True")
            End If
            If chkOEEG.Checked = True Then
                oOtherDigTest.SetProperty("EegEmgRoutine", "True")
            End If
            If chkOTreadmill.Checked = True Then
                oOtherDigTest.SetProperty("TreadmillStressTestRoutine", "True")
            End If
            If chkOHolterMonitor.Checked = True Then
                oOtherDigTest.SetProperty("HolterMonitorRoutine", "True")
            End If
            If chkOEKG.Checked = True Then
                oOtherDigTest.SetProperty("EkgEcgRoutine", "True")
            End If
            If chkOIndependentVisualization.Checked = True Then
                oOtherDigTest.SetProperty("IndependentVisualTest", "True")
            End If
            If chkODiscuswithPerfoming.Checked = True Then
                oOtherDigTest.SetProperty("DiscussWPerformingPhys", "True")
            End If

            If nudDignosisstudies.Value <> 0 Then
                oOtherDigTest.SetProperty("OtherDiagnosticStudiesCount", Convert.ToInt16(nudDignosisstudies.Value))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub GetManagementOption()
        Try
            Dim _dt As New DataTable
            'oManagmentOption.DecisionObtainMedicalRecsOther = chkMODecisointoobtain.Checked
            'oManagmentOption.ReviewMedicalRecsOther = chkMOreviewofsumm.Checked
            'oManagmentOption.DiscussCaseWHealthProvider = chkMOdiscofcase.Checked
            'oManagmentOption.ConfWPatientFamilyMinutes = numUPdownTimeSpan.Value
            Dim ocls As New clsLiquiddbLayer
            _dt = ocls.GetPatientOTC(_PatientID, _DOS)
            If _dt.Rows(0)("OTCDrugs") > 0 Then
                oMd.ManagementOptions.OverCounterMeds = True
            End If
            If _dt.Rows(0)("RxDrugs") > 0 Then
                oMd.ManagementOptions.PrescripIMMeds = True
            End If
            setSelectedManagementOption()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub setSelectedManagementOption()
        Try
            If chkMDecisionofcase.Checked = True Then
                oManagmentOption.SetProperty("DiscussCaseWHealthProvider", "True")
            End If
            If ckkMreviewandsummary.Checked = True Then
                oManagmentOption.SetProperty("ReviewMedicalRecsOther", "True")
            End If
            If chkMDecisiontoobtain.Checked = True Then
                oManagmentOption.SetProperty("DecisionObtainMedicalRecsOther", "True")
            End If
            If chkMDecisionnottoresuscitate.Checked = True Then
                oManagmentOption.SetProperty("DecisionNotResuscitate", "True")
            End If
            If chkMMajoremergencySurgery.Checked = True Then
                oManagmentOption.SetProperty("MajorEmergencySurgery", "True")
            End If
            If chkMMajorSurgerywrisk.Checked = True Then
                oManagmentOption.SetProperty("MajorSurgeryWRiskFactors", "True")
            End If
            If chkMMajorsurgeryworisk.Checked = True Then
                oManagmentOption.SetProperty("MajorSurgery", "True")
            End If
            If chkMMinorSurgeryWrisk.Checked = True Then
                oManagmentOption.SetProperty("MinorSurgeryWRiskFactors", "True")
            End If
            If chkMminorsurgeryWOrisk.Checked = True Then
                oManagmentOption.SetProperty("MinorSurgery", "True")
            End If
            If chkMClosefx.Checked = True Then
                oManagmentOption.SetProperty("ClosedFx", "True")
            End If
            If chkMPhysicalOccupationaltherapy.Checked = True Then
                oManagmentOption.SetProperty("PhysicalTherapy", "True")
            End If
            If chkMNuclearMedicine.Checked = True Then
                oManagmentOption.SetProperty("NuclearMedicine", "True")
            End If
            If chkMRespiratoryTreatment.Checked = True Then
                oManagmentOption.SetProperty("RespiratoryTreatments", "True")
            End If
            If chkMTelemetry.Checked = True Then
                oManagmentOption.SetProperty("Telemetry", "True")
            End If
            If chkMHighRiskmeds.Checked = True Then
                oManagmentOption.SetProperty("HighRiskMeds", "True")
            End If
            If chkMIVmedswadditives.Checked = True Then
                oManagmentOption.SetProperty("IVMedsWAdditives", "True")
            End If
            If chkMivmeds.Checked = True Then
                oManagmentOption.SetProperty("IVMeds", "True")
            End If
            If chkMPerscripmeds.Checked = True Then
                oManagmentOption.SetProperty("PrescripIMMeds", "True")
            End If
            If chkMOTCmeds.Checked = True Then
                oManagmentOption.SetProperty("OverCounterMeds", "True")
            End If

            'If nudMTimeSpent.Value <> 0 Then
            oManagmentOption.SetProperty("ConfWPatientFamilyMinutes", Convert.ToInt16(nudMTimeSpent.Value))
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.GenerateCode, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    '''' Old HPI Function 

    'Public Sub FillHPI(ByVal oDataCollection As CollLiquidData)
    '    With C1HPI
    '        For i As Integer = 0 To oDataCollection.Count - 1
    '            If i = 0 Then
    '                If oDataCollection.Item(i).Title <> "" Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    SetElementHearderStyle(C1HPI)
    '                    .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title) ''   dt.Rows(0)("sElementName"))
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1

    '                    .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                    .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                    .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                    .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                    .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                    .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    Dim mydataList As myList
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                            .SetData(NewRow, Col_HitCount, "1")
    '                            ''.Rows(NewRow
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                Hitcnt = 0
    '                                Hitcnt = Hitcnt + 1
    '                                .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                If oDataCollection.Item(i).m_datatype <> "Multiple Selection" Then ''dt.Rows(0)("sElementType") <> "Multiple Selection" Then
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1HPI)
    '                                End If
    '                                .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                            End If
    '                        End If

    '                    End If
    '                End If
    '            Else
    '                If .GetData(NewRow - 1, Col_ElementId) = oDataCollection.Item(i).m_elementId Then
    '                    .Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    Dim arrDetails As New ArrayList
    '                    arrDetails = Nothing
    '                    Dim mydataList As myList
    '                    arrDetails = oDataCollection.Item(i).ArrText_Field
    '                    If Not IsNothing(arrDetails) Then
    '                        mydataList = New myList
    '                        Dim Item As String
    '                        If arrDetails.Item(0).GetType().Name = "String" Then
    '                            Item = arrDetails.Item(0).ToString
    '                            .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                            .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                            .SetData(NewRow, Col_HitCount, "1")
    '                        Else

    '                            If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    Dim c As Integer = 0
    '                                    For c = NewRow - 1 To 0 Step -1
    '                                        If .GetData(c, Col_ElementDetails) = "" Then
    '                                            Hitcnt = Hitcnt + 1
    '                                            .SetData(c, Col_HitCount, Hitcnt)
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                Else
    '                                    '.Rows.Add()
    '                                    'NewRow = .Rows.Count - 1

    '                                    'If dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    Hitcnt = 0
    '                                    Hitcnt = Hitcnt + 1
    '                                    .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                    'End If
    '                                    .Rows.Add()
    '                                    NewRow = .Rows.Count - 1
    '                                    Basicinfo(i, oDataCollection, C1HPI)
    '                                    .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                    .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                End If

    '                            End If
    '                        End If

    '                    End If
    '                Else
    '                    If oDataCollection.Item(i).Title <> "" Then
    '                        Total = 0
    '                        For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
    '                            With C1HPI
    '                                If .GetData(T, Col_HiddenElementCategory) = "Total" Then
    '                                    Exit For
    '                                End If
    '                                If oDataCollection.Item(i - 1).m_datatype <> "Multiple Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" And oDataCollection.Item(i - 1).m_datatype <> "Text" And oDataCollection.Item(i - 1).m_datatype <> "Single Selection" And oDataCollection.Item(i - 1).m_datatype <> "Boolean" Then
    '                                    If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                                    End If
    '                                Else
    '                                    If T <> 0 Then
    '                                        Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                                    End If
    '                                End If
    '                            End With
    '                        Next
    '                        If Total <> 0 Then
    '                            C1HPI.Rows.Add()
    '                            NewRow = .Rows.Count - 1
    '                            Basicinfo(i, oDataCollection, C1HPI)
    '                            .SetData(NewRow, Col_ElementDetails, "Total")
    '                            .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                            .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                            SetElementTotalStyle(C1HPI)
    '                        End If

    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1HPI)

    '                        SetElementHearderStyle(C1HPI)
    '                        ''.SetData(NewRow, Col_ElementName, dt.Rows(0)("sElementName"))
    '                        .SetData(NewRow, Col_ElementName, oDataCollection.Item(i).Title)
    '                        .Rows.Add()
    '                        NewRow = .Rows.Count - 1
    '                        Basicinfo(i, oDataCollection, C1HPI)
    '                        .SetData(NewRow, Col_PatientID, oDataCollection.Item(i).PatientID)
    '                        .SetData(NewRow, Col_VisitID, oDataCollection.Item(i).mgnVisitID)
    '                        .SetData(NewRow, Col_ExamId, oDataCollection.Item(i).examid)
    '                        .SetData(NewRow, Col_ElementId, oDataCollection.Item(i).m_elementId)
    '                        .SetData(NewRow, Col_Helptext, oDataCollection.Item(i).HelpText)
    '                        .SetData(NewRow, Col_DataType, oDataCollection.Item(i).m_datatype)
    '                        Dim arrDetails As New ArrayList
    '                        arrDetails = Nothing
    '                        Dim mydataList As myList
    '                        arrDetails = oDataCollection.Item(i).ArrText_Field
    '                        If Not IsNothing(arrDetails) Then
    '                            mydataList = New myList
    '                            Dim Item As String
    '                            If arrDetails.Item(0).GetType().Name = "String" Then
    '                                Item = arrDetails.Item(0).ToString
    '                                .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                .SetData(NewRow, Col_HitCount, "1")
    '                            Else
    '                                If CType(arrDetails.Item(0), myList).HistoryCategory = "" And oDataCollection.Item(i).m_datatype = "Multiple Selection" Then ''dt.Rows(0)("sElementType") = "Multiple Selection" Then
    '                                    Item = CType(arrDetails.Item(0), myList).HistoryItem()
    '                                    .Rows(NewRow).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop
    '                                    .SetData(NewRow, Col_ElementDetails, SetRowHeight(Item, C1HPI))
    '                                    .SetData(NewRow, Col_HitCount, "1")
    '                                Else
    '                                    ''Item = CType(arrDetails.Item(0), myList).HistoryCategory & "-" & CType(arrDetails.Item(0), myList).HistoryItem
    '                                    If .GetData(NewRow - 1, Col_HiddenElementCategory) = CType(arrDetails.Item(0), myList).HistoryCategory Then
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                        .SetData(NewRow, Col_HitCount, "1")
    '                                        '.Rows.Add()
    '                                        'NewRow = .Rows.Count - 1
    '                                        'Basicinfo(i, oDataCollection, C1HPI)
    '                                        '.SetData(NewRow, Col_ElementDetails, "Total")
    '                                        '.SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                                        '.SetData(NewRow, Col_HitCount, "1")
    '                                        'SetElementTotalStyle(C1HPI)
    '                                    Else
    '                                        '.Rows.Add()
    '                                        'NewRow = .Rows.Count - 1
    '                                        .SetData(NewRow, Col_ElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        Hitcnt = 0
    '                                        Hitcnt = Hitcnt + 1
    '                                        .SetData(NewRow, Col_HitCount, Hitcnt)
    '                                        .Rows.Add()
    '                                        NewRow = .Rows.Count - 1
    '                                        Basicinfo(i, oDataCollection, C1HPI)
    '                                        .SetData(NewRow, Col_HiddenElementCategory, CType(arrDetails.Item(0), myList).HistoryCategory)
    '                                        .SetData(NewRow, Col_ElementDetails, CType(arrDetails.Item(0), myList).HistoryItem)
    '                                    End If
    '                                End If
    '                            End If

    '                        End If
    '                    End If
    '                End If
    '            End If
    '            Total = 0

    '            If i = oDataCollection.Count - 1 Then

    '                For T As Integer = C1HPI.Rows.Count - 1 To 0 Step -1
    '                    If .GetData(T, Col_HiddenElementCategory) = "Total" Then
    '                        Exit For
    '                    End If
    '                    With C1HPI
    '                        If oDataCollection.Item(i).m_datatype <> "Multiple Selection" And oDataCollection.Item(i).m_datatype <> "Text" And oDataCollection.Item(i).m_datatype <> "Boolean" And oDataCollection.Item(i).m_datatype <> "Single Selection" Then
    '                            If .GetData(T, Col_ElementCategory) <> "" And .GetData(T, Col_ElementName) = "" Then ''And .GetData(NewRow, Col_ElementId) = .GetData(T, Col_ElementId) Then
    '                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                            End If
    '                        Else
    '                            If T <> 0 Then
    '                                Total = Total + Convert.ToInt16(.GetData(T, Col_HitCount))
    '                            End If
    '                        End If

    '                    End With
    '                Next
    '                If Total <> 0 Then
    '                    C1HPI.Rows.Add()
    '                    NewRow = .Rows.Count - 1
    '                    Basicinfo(i, oDataCollection, C1HPI)
    '                    .SetData(NewRow, Col_ElementDetails, "Total")
    '                    .SetData(NewRow, Col_HiddenElementCategory, "Total")
    '                    .SetData(NewRow, Col_HitCount, Convert.ToString(Total))
    '                    SetElementTotalStyle(C1HPI)
    '                End If
    '            End If

    '        Next



    '        Total = 0
    '        GrandTotal = 0
    '        For G As Integer = 0 To C1HPI.Rows.Count - 1
    '            With C1HPI
    '                If .GetData(G, Col_ElementDetails) = "Total" Then
    '                    GrandTotal = GrandTotal + Convert.ToInt16(.GetData(G, Col_HitCount))
    '                End If
    '            End With
    '        Next
    '        If GrandTotal <> 0 Then
    '            .Rows.Add()
    '            NewRow = NewRow + 1
    '            .SetData(NewRow, Col_ElementDetails, "Grand Total")
    '            .SetData(NewRow, Col_HitCount, GrandTotal)
    '            SetElementGrandTotalStyle(C1HPI)
    '        End If
    '    End With
    'End Sub

#End Region




    Private Sub C1HPI_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1HPI.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1ROS_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ROS.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1Details_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Details.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1PhysicalExamination_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PhysicalExamination.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1Labs_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Labs.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1Radiology_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Radiology.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1MedicalCondition_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1MedicalCondition.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub




End Class