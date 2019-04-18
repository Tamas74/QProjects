Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRLab
Imports System.Reflection
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class frmLab_TestMaster

    Private COL_NO As Int16 = 0
    Private COL_RESULTNAME As Int16 = 1
    Private COL_VALUETYPE As Int16 = 2
    Private COL_UNIT As Int16 = 3
    Private COL_DEFAULTVALUE As Int16 = 4
    Private COL_REFRANGE As Int16 = 5
    Private COL_COMMENTS As Int16 = 6
    Private COL_INSTRUCTION As Int16 = 7
    Private COL_BOUNDID As Int16 = 8
    Private COL_MALELB As Int16 = 9
    Private COL_MALEUB As Int16 = 10
    Private COL_FEMALELB As Int16 = 11
    Private COL_FEMALEUB As Int16 = 12
    Private COL_AlternateResultCode As Int16 = 13 'added by sagaarK on 10012008 for showing the LoinCID
    Private COL_LOINCID As Int16 = 14 'added by sagaarK on 10012008 for showing the LoinCID

    Private COL_COUNT As Int16 = 15

    Private COL_PreferedLabNo As Int16 = 0
    Private COL_PreferedLabTestNo As Int16 = 1
    Private COL_PreferedLabTestCINo As Int16 = 2
    Private COL_PreferedLabName As Int16 = 3
    Private COL_PreferedLabComments As Int16 = 4
    Private COL_PCount As Int16 = 5
    Private sPreferedLabName As String = " "

    Private sResultType_Text As String = "Text"
    Private sResultType_Numeric As String = "Numeric"
    Public _TestID As Long = 0  ''added for selecting particular recode for order $ resultsetup
    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Public blnIsModify As Boolean = False
    Dim _arrLabs As New ArrayList
    Dim _arrManagment As New ArrayList
    Dim _arrOrders As New ArrayList
    Dim _arrOtherDiag As New ArrayList
    Dim dtEMField As DataTable
    Dim _isLoadGridCvxControl As Boolean = False
    Private oLOINCOrderControl As gloUserControlLibrary.gloUC_GridList
    Private oCPTControl As gloUserControlLibrary.gloUC_GridList
    Public Event GridListLoaded()
    Public Event GridListClosed()
    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer
    'Dim LoincItem As String = ""
    Private ToolTip11 As New System.Windows.Forms.ToolTip
    Private blnLOINCSelectorClicked As Boolean
    Private blnCPTSelectorClicked As Boolean
    Private oLabCSSTs As LabActor.LabCSSTs

    Private Sub frmLab_TestMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        ToolTip11.Dispose()
        ToolTip11 = Nothing

        cmbSpecimen.DataSource = Nothing
        cmbSpecimen.Items.Clear()

        cmbCollContainer.DataSource = Nothing
        cmbCollContainer.Items.Clear()

        cmbStorageTemp.DataSource = Nothing
        cmbStorageTemp.Items.Clear()

        If IsNothing(oLabCSSTs) = False Then
            oLabCSSTs.Dispose()
            oLabCSSTs = Nothing
        End If


    End Sub

    Private Sub frmLab_TestMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            gloC1FlexStyle.Style(C1LabResult)
            DesignResultGrid()
            gloC1FlexStyle.Style(C1PreferedLab)
            DesignPreferedLabGrid()


            Dim i As Int16
            Dim k As Int16

            'Specimen
            cmbSpecimen.Items.Clear()



            Dim ogloEMRLabCSST As New gloEMRLabCSST

            'If IsNothing(oLabCSSTs) = False Then
            '    oLabCSSTs.Dispose()
            '    oLabCSSTs = Nothing
            'End If


            oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Specimen)
            If (IsNothing(oLabCSSTs) = False) Then


                If oLabCSSTs.Count > 0 Then
                    Dim olabCSST As gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST
                    olabCSST = New gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST
                    olabCSST.LabCSST_ID = 0
                    olabCSST.LabCSST_Code = ""
                    olabCSST.LabCSST_Name = ""
                    olabCSST.LabCSST_Type = 0
                    olabCSST.nClinicID = 0
                    oLabCSSTs.Add(olabCSST)
                    cmbSpecimen.DataSource = oLabCSSTs
                    cmbSpecimen.DisplayMember = "labCSST_Name"  'add only column name, not field value
                    cmbSpecimen.ValueMember = "LabCSST_ID"
                    If cmbSpecimen.Items.Count > 0 Then
                        cmbSpecimen.SelectedIndex = cmbSpecimen.Items.Count - 1
                    Else
                        cmbSpecimen.Text = ""
                    End If
                Else
                    cmbSpecimen.Items.Add("")
                    cmbSpecimen.SelectedItem = ""
                End If

                'oLabCSSTs = Nothing
            End If
            ogloEMRLabCSST.Dispose()
            ogloEMRLabCSST = Nothing



            cmbCollContainer.Items.Clear()
            'oLabCSSTs = New LabActor.LabCSSTs
            ogloEMRLabCSST = New gloEMRLabCSST

            'If IsNothing(oLabCSSTs) = False Then
            '    oLabCSSTs.Dispose()
            '    oLabCSSTs = Nothing
            'End If

            oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Collection)
            If (IsNothing(oLabCSSTs) = False) Then
                If oLabCSSTs.Count > 0 Then
                    cmbCollContainer.DataSource = oLabCSSTs
                    cmbCollContainer.DisplayMember = "labCSST_Name"  'add only column name, not field value
                    cmbCollContainer.ValueMember = "LabCSST_ID"
                End If
            End If

            'oLabCSSTs = Nothing
            ogloEMRLabCSST.Dispose()
            ogloEMRLabCSST = Nothing


            cmbStorageTemp.Items.Clear()
            'oLabCSSTs = New LabActor.LabCSSTs
            ogloEMRLabCSST = New gloEMRLabCSST

            'If IsNothing(oLabCSSTs) = False Then
            '    oLabCSSTs.Dispose()
            '    oLabCSSTs = Nothing
            'End If

            oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.StorageTemperature)
            If (IsNothing(oLabCSSTs) = False) Then


                If oLabCSSTs.Count > 0 Then
                    Dim olabCSST As gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST
                    olabCSST = New gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST
                    olabCSST.LabCSST_ID = 0
                    olabCSST.LabCSST_Code = ""
                    olabCSST.LabCSST_Name = ""
                    olabCSST.LabCSST_Type = 0
                    olabCSST.nClinicID = 0
                    oLabCSSTs.Add(olabCSST)

                    cmbStorageTemp.DataSource = oLabCSSTs
                    cmbStorageTemp.DisplayMember = "labCSST_Name"  'add only column name, not field value
                    cmbStorageTemp.ValueMember = "LabCSST_ID"
                    If cmbStorageTemp.Items.Count > 0 Then
                        cmbStorageTemp.SelectedIndex = cmbStorageTemp.Items.Count - 1
                    Else
                        cmbStorageTemp.Text = ""
                    End If
                Else
                    cmbStorageTemp.Items.Add("")
                    cmbStorageTemp.SelectedItem = ""
                End If
            End If
            'oLabCSSTs = Nothing
            ogloEMRLabCSST.Dispose()
            ogloEMRLabCSST = Nothing



            ''Template Names
            'oLabCSSTs = New LabActor.LabCSSTs
            ogloEMRLabCSST = New gloEMRLabCSST
            Dim dtTemplate As DataTable
            dtTemplate = ogloEMRLabCSST.GetTemplateNames()
            If Not IsNothing(dtTemplate) Then

                If dtTemplate.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dtTemplate
                    cmbTemplate.DisplayMember = "sTemplateName"
                    cmbTemplate.ValueMember = "nTemplateID"
                End If
            End If
            'oLabCSSTs = Nothing
            ogloEMRLabCSST.Dispose()
            ogloEMRLabCSST = Nothing
            cmbTemplate.SelectedValue = 0

            cmbMUReportingCat.Items.Clear()
            cmbMUReportingCat.Items.Add("")
            cmbMUReportingCat.Items.Add("Lab")
            cmbMUReportingCat.Items.Add("Radiology/Imaging")
            cmbMUReportingCat.Items.Add("Other")
            cmbMUReportingCat.Items.Add("Referral")

            'Value Type
            cmbValueType.Items.Clear()
            cmbValueType.Items.Add(sResultType_Text)
            cmbValueType.Items.Add(sResultType_Numeric)

            If nEditID > 0 Then
                'Modify
                RemoveHandler cmbMUReportingCat.SelectedIndexChanged, AddressOf cmbMUReportingCat_SelectedIndexChanged
                _isLoadGridCvxControl = True
                Dim _gloEMRTest As New gloEMRLabTest
                Dim oLabTest As LabActor.Test
                'Dim strAssociatedfield As String
                oLabTest = _gloEMRTest.GetTest(nEditID)

                txtCode.Text = oLabTest.Code
                txtName.Text = oLabTest.Name
                txt_testLoinicCode.Text = oLabTest.LOINCLongName
                txtCPTCode.Text = oLabTest.sCPTDescription
                chkOrderable.Checked = oLabTest.Ordarable
                _isLoadGridCvxControl = False

                cmbSpecimen.Text = oLabTest.SpecimenName


                cmbCollContainer.Text = oLabTest.CollectionName



                cmbStorageTemp.Text = oLabTest.StorageName

                If oLabTest.nTemplateID <> 0 Then
                    ogloEMRLabCSST = New gloEMRLabCSST
                    cmbTemplate.Text = ogloEMRLabCSST.GetTemplateName(oLabTest.nTemplateID)
                    For j As Int16 = 0 To cmbTemplate.Items.Count - 1
                        If DirectCast(cmbTemplate.Items(j), System.Data.DataRowView).Row.ItemArray(1) = cmbTemplate.Text Then
                            cmbTemplate.SelectedIndex = j
                            Exit For
                        End If
                    Next
                    ogloEMRLabCSST.Dispose()
                    ogloEMRLabCSST = Nothing
                End If


                If oLabTest.MUReportingCategory <> "" Then
                    ' ogloEMRLabCSST = New gloEMRLabCSST
                    cmbMUReportingCat.Text = oLabTest.MUReportingCategory

                    For j As Int16 = 0 To cmbMUReportingCat.Items.Count - 1
                        If cmbMUReportingCat.Items(j) = cmbMUReportingCat.Text Then
                            cmbMUReportingCat.SelectedIndex = j
                            Exit For
                        End If
                    Next
                    'ogloEMRLabCSST.Dispose()
                    'ogloEMRLabCSST = Nothing
                End If

                cmbStructuredLabResults.Text = oLabTest.IsStructuredLabTest

                chkOutboundTransistion.Checked = oLabTest.bOutboundTransistion

                If oLabTest.ResultType = LabActor.enumTestResultType.SingleResult Then
                    optSingle.Checked = True
                ElseIf oLabTest.ResultType = LabActor.enumTestResultType.ProfileResult Then
                    optProfile.Checked = True
                End If

                txtInstruction.Text = oLabTest.Instruction
                txtPrecaution.Text = oLabTest.Precaution

                cmbDept.Text = "" 'oLabTest.DepartmentCategoryID
                For i = 0 To cmbDept.Items.Count - 1
                    If cmbDept.Items(i) = cmbDept.Text Then
                        cmbDept.SelectedIndex = i
                        Exit For
                    End If
                Next

                cmbTestHead.Text = "" 'oLabTest.TestHeadID
                For i = 0 To cmbTestHead.Items.Count - 1
                    If cmbTestHead.Items(i) = cmbTestHead.Text Then
                        cmbTestHead.SelectedIndex = i
                        Exit For
                    End If
                Next

                With oLabTest
                    If oLabTest.ResultType = LabActor.enumTestResultType.SingleResult Then
                        If .Results.Count > 0 Then
                            txtResultName.Text = .Results.Item(0).ResultName
                            If .Results.Item(0).ValueType = LabActor.enumTestResultValueType.Text Then
                                cmbValueType.Text = sResultType_Text
                            ElseIf .Results.Item(0).ValueType = LabActor.enumTestResultValueType.Numeric Then
                                cmbValueType.Text = sResultType_Numeric
                            End If
                            For i = 0 To cmbValueType.Items.Count - 1
                                If cmbValueType.Items(i) = cmbValueType.Text Then
                                    cmbValueType.SelectedIndex = i
                                    Exit For
                                End If
                            Next

                            txtRefRange.Text = .Results.Item(0).ReferenceRange
                            txtComment.Text = .Results.Item(0).Comments
                            txtLoincId.Text = .Results.Item(0).LoincID
                            txtAlternateResultCode.Text = .Results.Item(0).AlternateResultCode

                        End If
                    ElseIf oLabTest.ResultType = LabActor.enumTestResultType.ProfileResult Then
                        If .Results.Count > 0 Then
                            DesignResultGrid(.Results.Count, oLabTest.Results)

                            For i = 0 To oLabTest.Results.Count - 1
                                C1LabResult.SetData(i + 1, COL_RESULTNAME, .Results.Item(i).ResultName)
                                If .Results.Item(i).ValueType = LabActor.enumTestResultValueType.Text Then
                                    C1LabResult.SetData(i + 1, COL_VALUETYPE, sResultType_Text)
                                ElseIf .Results.Item(i).ValueType = LabActor.enumTestResultValueType.Numeric Then
                                    C1LabResult.SetData(i + 1, COL_VALUETYPE, sResultType_Numeric)
                                End If
                                C1LabResult.SetData(i + 1, COL_REFRANGE, .Results.Item(i).ReferenceRange)
                                C1LabResult.SetData(i + 1, COL_COMMENTS, .Results.Item(i).Comments)
                                C1LabResult.SetData(i + 1, COL_LOINCID, .Results.Item(i).LoincID)
                                C1LabResult.SetData(i + 1, COL_AlternateResultCode, .Results.Item(i).AlternateResultCode)
                            Next
                        End If
                    End If
                End With

                Dim dtPreferedLab As DataTable = Nothing
                dtPreferedLab = GetPreferedLab(nEditID)
                With C1PreferedLab
                    If Not IsNothing(dtPreferedLab) Then
                        If dtPreferedLab.Rows.Count > 0 Then
                            DesignPreferedLabGrid(dtPreferedLab.Rows.Count, dtPreferedLab)
                            For k = 0 To dtPreferedLab.Rows.Count - 1
                                C1PreferedLab.SetData(k + 1, COL_PreferedLabName, dtPreferedLab.Rows(k)("labci_ContactName"))
                                C1PreferedLab.SetData(k + 1, COL_PreferedLabComments, dtPreferedLab.Rows(k)("sComments"))
                            Next
                        End If
                    End If
                    .Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_PreferedLabName)
                End With


                'strAssociatedfield = _gloEMRTest.GetAssociatedEMField(nEditID)
                dtEMField = _gloEMRTest.GetEMAssociatedField(nEditID)
                FillEMArraylist(dtEMField)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "EM field  viewed.  ", gloAuditTrail.ActivityOutCome.Success)
                If (IsNothing(_gloEMRTest) = False) Then
                    _gloEMRTest.Dispose()
                    _gloEMRTest = Nothing
                End If
                If (IsNothing(oLabTest) = False) Then

                    oLabTest = Nothing
                End If

                AddHandler cmbMUReportingCat.SelectedIndexChanged, AddressOf cmbMUReportingCat_SelectedIndexChanged

            End If

            ToolTip11.SetToolTip(Me.btnBrowse, " Associate E&M Fields ")


            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "EM field  viewed.", gloAuditTrail.ActivityOutCome.Success)

            'txtCode.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Private Function FillPreferedLabs() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "SELECT Lab_ContactInfo.labci_ContactName FROM Lab_ContactInfo WHERE labci_Type = 1 AND labci_Id IS NOT NULL"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(Connection) = False) Then
                If Connection.State = ConnectionState.Open Then
                    Connection.Close()
                End If
                Connection.Dispose()
                Connection = Nothing
            End If
        End Try
    End Function
    Private Function GetPreferedLab(ByVal LabTestID As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim dt As DataTable = Nothing
        Try
            Dim strQRY As String = "SELECT  Lab_ContactInfo.labci_ContactName , Lab_Test_Mst_PreferredLab.sComments FROM Lab_Test_Mst_PreferredLab INNER JOIN Lab_ContactInfo ON Lab_Test_Mst_PreferredLab.labci_Id = Lab_ContactInfo.labci_Id AND Lab_Test_Mst_PreferredLab.labtm_ID =" & LabTestID
            dt = oDB.GetDataTable_Query(strQRY)
            Return dt
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function
    Private Function GetPreferedLabID(ByVal PreferedLabName As String) As Int64
        Dim oDB As New DataBaseLayer
        Dim LabID As Int64 = Nothing
        Try
            Dim strQRY As String = "SELECT  Lab_ContactInfo.labci_Id  FROM Lab_ContactInfo where Lab_ContactInfo.labci_ContactName ='" & PreferedLabName.Replace("'", "''") & "'"
            LabID = oDB.GetRecord_Query(strQRY)
            Return LabID
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function
    Public Sub FillEMArraylist(ByVal _dt As DataTable)
        'Dim Emylist As myList
        Dim oListItem As gloGeneralItem.gloItem
        If Not IsNothing(_dt) Then
            For i As Integer = 0 To _dt.Rows.Count - 1
                ''Labs


                If _dt.Rows(i)("sAssociatedEMName") = "IncisionalBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IncisionalBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "SuperficialBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "SuperficialBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "TypeCrossmatchRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TypeCrossmatchRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PTRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PTRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ABGsRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ABGsRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CardiacEnzymesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CardiacEnzymesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChemicalProfileRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChemicalProfileRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DrugScreenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DrugScreenRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ElectrolytesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ElectrolytesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "BunCreatinineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "BunCreatinineRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AmylaseRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AmylaseRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PregnancyTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PregnancyTestRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "FluStrepMonoRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "FluStrepMonoRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CbcUaRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CbcUaRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussionWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussionWPerformingPhys"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "OtherLabsCount" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherLabsCount"
                    oListItem.Code = strLabs.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If


                ''orders

                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesWRiskRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MRIRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MRIRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CATScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CATScanRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVPRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVPRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "GIGallbladderRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "GIGallbladderRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TLSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TLSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscographyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscographyRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiagUltrasoundRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiagUltrasoundRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HipPelvisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HipPelvisRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "AbdomenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AbdomenRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ExtremitiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ExtremitiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ChestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChestRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherXRaysCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherXRaysCount"
                    oListItem.Code = strOrders.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If

                ''other diagnosis

                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeWRiskRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CuldocentesesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CuldocentesesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ThoracentesisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ThoracentesisRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "LumbarPunctureRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "LumbarPunctureRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "NuclearScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearScanRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PulmonaryStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PulmonaryStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DopplerFlowStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DopplerFlowStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VectorcardiogramRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VectorcardiogramRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "EegEmgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EegEmgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TreadmillStressTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TreadmillStressTestRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "HolterMonitorRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HolterMonitorRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EkgEcgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EkgEcgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If


                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherDiagnosticStudiesCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherDiagnosticStudiesCount"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If


                ''management option


                If _dt.Rows(i)("sAssociatedEMName") = "DiscussCaseWHealthProvider" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussCaseWHealthProvider"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ReviewMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ReviewMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionObtainMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionObtainMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionNotResuscitate" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionNotResuscitate"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MajorEmergencySurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorEmergencySurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ClosedFx" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ClosedFx"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PhysicalTherapy" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PhysicalTherapy"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "NuclearMedicine" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearMedicine"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "RespiratoryTreatments" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "RespiratoryTreatments"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If


                If _dt.Rows(i)("sAssociatedEMName") = "Telemetry" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "Telemetry"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "HighRiskMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HighRiskMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMedsWAdditives" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMedsWAdditives"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PrescripIMMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PrescripIMMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OverCounterMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OverCounterMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ConfWPatientFamilyMinutes" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ConfWPatientFamilyMinutes"
                    oListItem.Code = strMangementOption.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If
            Next
        End If
    End Sub

    Private Sub FillEMLabsField()
        Try


            cmbAssociatedEM.Items.Clear()
            Dim strprop As String
            Dim pType As Type
            cmbAssociatedEM.Items.Add("Select EM field")
            cmbAssociatedEM.SelectedIndex = 0
            'Declare a variable for PropertyInfo
            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType
                'Add Items in combo box


                If pType.Name = "Boolean" Then
                    If strprop <> "IndependentVisualTest" And strprop <> "DiscussionWPerformingPhys" Then
                        If strprop.EndsWith("Urgent") <> True Then
                            cmbAssociatedEM.Items.Add(strprop)
                        End If
                    End If
                End If
            Next
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Lab master opened.  ", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Lab master opened.  ", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)

        End Try
    End Sub
    Private Sub SaveTestMaster()

        C1LabResult.Select()

        Dim oLab As New gloEMRLabTest
        Dim oLabResult As LabActor.TestResult
        Dim oPreferedLabs As New LabActor.PreferedLabResult


        If txtCode.Text.Trim = "" Then
            MessageBox.Show("Please enter Test Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCode.Select()
            Exit Sub
        End If
        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter Test Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Select()
            Exit Sub
        End If
        If txtCode.Text.Trim().Length >= 60 Then  ''changes made for bugid 68344
            MessageBox.Show("Please enter Shorter Test Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCode.Select()
            Exit Sub
        End If
        If txtName.Text.Trim().Length >= 255 Then  ''changes made for bugid 68344 
            MessageBox.Show("Please enter Shorter Test Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Select()
            Exit Sub
        End If


        If blnIsModify = True Then


            If oLab.IsCodeExists(txtCode.Text.Trim) = True And oLab.IsExists(txtName.Text.Trim) = True Then
                MessageBox.Show("Duplicate Test Code and Name. Please enter another Test Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Select()
                Exit Sub
            End If
            If oLab.IsCodeExists(txtCode.Text.Trim) = True Then
                MessageBox.Show("Duplicate Test Code. Please enter another Test Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Select()
                Exit Sub
            End If
            If oLab.IsExists(txtName.Text) = True Then
                MessageBox.Show("Duplicate Test Name. Please enter another Test Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtName.Select()
                Exit Sub
            End If
        Else
            If UCase(txtName.Text.Trim) <> UCase(sEditName.Trim) And UCase(txtCode.Text.Trim) <> UCase(sEditCode.Trim) Then
                If oLab.IsExists(txtName.Text.Trim) = True And oLab.IsCodeExists(txtCode.Text.Trim) = True Then
                    MessageBox.Show("Duplicate Test Code and Name. Please enter another Test Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Select()
                    Exit Sub
                End If
            End If
            If UCase(txtName.Text.Trim) <> UCase(sEditName.Trim) Then
                If oLab.IsExists(txtName.Text.Trim) = True Then
                    MessageBox.Show("Duplicate Test Name. Please enter another Test Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtName.Select()
                    Exit Sub
                End If
            End If
            If UCase(txtCode.Text.Trim) <> UCase(sEditCode.Trim) Then
                If oLab.IsCodeExists(txtCode.Text.Trim) = True Then
                    MessageBox.Show("Duplicate Test Code. Please enter another Test Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Select()
                    Exit Sub
                End If
            End If
        End If

        'End of Test Validations

        Try
            Dim dlg As DialogResult = Nothing
            If cmbMUReportingCat.Text = "Referral" And chkOutboundTransistion.Checked = False Then ''condition added to solve bugid 67451,67448
                dlg = MessageBox.Show("Referral Orders are often tracked as Meaningful Use Outbound Transitions of Care." & vbNewLine & vbNewLine & " Do you want this Referral Test to be tracked as Outbound Transitions of Care?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)




            End If
            If Not IsNothing(dlg) Then
                If dlg = Windows.Forms.DialogResult.Yes Then
                    chkOutboundTransistion.Checked = True

                End If
                If dlg = Windows.Forms.DialogResult.No Then
                    chkOutboundTransistion.Checked = False

                End If
                If dlg = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End If


            'Do the Result grid validations
            '   Dim oLabCSSTs As New LabActor.LabCSSTs
            With oLab.LabActor
                If blnIsModify = True Then
                    .TestID = 0
                Else
                    .TestID = nEditID
                End If

                .Code = txtCode.Text.Trim
                .Name = txtName.Text.Trim
                ' sCode = txtCode.Text.Trim
                'sName = txtName.Text.Trim
                .Ordarable = chkOrderable.Checked
                .Specimen = cmbSpecimen.Text
                If cmbAssociatedEM.SelectedItem <> "Select EM field" Then
                    .AssociatedEMField = cmbAssociatedEM.Text
                Else
                    .AssociatedEMField = ""
                End If

                If (Not cmbCollContainer.SelectedItem Is Nothing) AndAlso (cmbCollContainer.SelectedItem.ToString().Trim() <> "") Then  ''andalso condition added for checking empty string
                    '.Collection = cmbCollContainer.Text
                    .Collection = CType(cmbCollContainer.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_ID     'ID
                    .CollectionName = CType(cmbCollContainer.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_Name  'Name
                Else
                    .Collection = "0"
                    .CollectionName = ""
                End If
                If (Not cmbStorageTemp.SelectedItem Is Nothing) AndAlso (cmbStorageTemp.SelectedItem.ToString().Trim() <> "") Then  ''andalso condition added for checking empty string
                    '.Storage = cmbStorageTemp.Text
                    .Storage = CType(cmbStorageTemp.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_ID 'cmbStorageTemp.ValueMember  'ID
                    .StorageName = CType(cmbStorageTemp.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_Name 'cmbStorageTemp.DisplayMember  'name
                Else
                    .Storage = "0"
                    .StorageName = ""
                End If
                If Not cmbSpecimen.SelectedItem Is Nothing AndAlso (cmbSpecimen.SelectedItem.ToString().Trim() <> "") Then  ''andalso condition added for checking empty string
                    '.Specimen = cmbSpecimen.Text 'cmbSpecimen.Items.Item(0)
                    .Specimen = CType(cmbSpecimen.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_ID 'cmbSpecimen.ValueMember 'ID
                    .SpecimenName = CType(cmbSpecimen.SelectedItem, gloEMRGeneralLibrary.gloEMRActors.LabActor.LabCSST).LabCSST_Name  'cmbSpecimen.DisplayMember 'Name
                Else
                    .Specimen = "0"
                    .SpecimenName = ""
                End If
                .Instruction = txtInstruction.Text.Trim
                .Precaution = txtPrecaution.Text.Trim

                'If (txt_testLoinicCode.Text.Trim() = "") Then
                'txt_testLoinicCode.Text = LoincItem
                ' End If
                If txt_testLoinicCode.Text <> "" Or txtCPTCode.Text <> "" Then
                    Dim ogloEMRLabCSST As gloEMRLabCSST = New gloEMRLabCSST
                    Dim dsData As DataSet
                    dsData = ogloEMRLabCSST.SeperateCodeAnddescription(txt_testLoinicCode.Text, txtCPTCode.Text)
                    If Not IsNothing(dsData) Then
                        If dsData.Tables(0).Rows.Count > 0 Then
                            .LOINCId = dsData.Tables(0).Rows(0)(0)
                        End If
                        If dsData.Tables(1).Rows.Count > 0 Then
                            .LOINCLongName = dsData.Tables(1).Rows(0)(0)
                        End If
                        If dsData.Tables(2).Rows.Count > 0 Then
                            .sCPTCode = dsData.Tables(2).Rows(0)(0)
                        End If
                        If dsData.Tables(3).Rows.Count > 0 Then
                            .sCPTDescription = dsData.Tables(3).Rows(0)(0)
                        End If
                    End If
                    If Not IsNothing(ogloEMRLabCSST) Then
                        ogloEMRLabCSST.Dispose()
                        ogloEMRLabCSST = Nothing
                    End If
                    If Not IsNothing(dsData) Then
                        dsData.Dispose()
                        dsData = Nothing
                    End If
                End If




                If Not cmbTemplate.SelectedItem Is Nothing Then
                    .nTemplateID = cmbTemplate.SelectedValue
                Else
                    .nTemplateID = 0
                End If

                If Not cmbMUReportingCat.SelectedItem Is Nothing Then
                    .MUReportingCategory = cmbMUReportingCat.Text

                Else
                    .MUReportingCategory = ""
                End If

                'If Not cmbStructuredLabResults.SelectedItem Is Nothing Then

                .IsStructuredLabTest = cmbStructuredLabResults.Text
                'Else
                '.MUReportingCategory = ""
                'End If
                .bOutboundTransistion = chkOutboundTransistion.Checked


                .DepartmentCategoryID = 0
                .TestHeadID = 0
                If optSingle.Checked = True Then
                    .ResultType = LabActor.enumTestResultType.SingleResult
                Else
                    .ResultType = LabActor.enumTestResultType.ProfileResult
                End If

                Dim _ResultName As String = ""
                Dim _ValueType As Int16 = 0
                '----------Prefered Labs----------'
                C1PreferedLab.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

                C1PreferedLab.Select()

                With .PreferedResults
                    For i As Int16 = 1 To C1PreferedLab.Rows.Count - 1
                        Dim StrPreferedLabName As String = ""
                        Dim StrComment As String = String.Empty
                        Dim _PreferedLabCIID As Int64 = 0
                        If Not IsNothing(C1PreferedLab.GetData(i, COL_PreferedLabName)) OrElse (C1PreferedLab.GetData(i, COL_PreferedLabName) = "") Then
                            StrPreferedLabName = C1PreferedLab.GetData(i, COL_PreferedLabName)
                            StrComment = C1PreferedLab.GetData(i, COL_PreferedLabComments)
                            If (StrPreferedLabName = Nothing AndAlso StrComment <> Nothing) Then
                                MessageBox.Show("Preferred lab cannot be blank. Please select preferred lab.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                            If Not IsNothing(StrPreferedLabName) AndAlso StrPreferedLabName <> " " Then
                                _PreferedLabCIID = GetPreferedLabID(StrPreferedLabName)
                            End If
                        End If
                        If IsNothing(C1PreferedLab.GetData(i, COL_PreferedLabComments)) Then
                            StrComment = String.Empty
                        Else
                            StrComment = C1PreferedLab.GetData(i, COL_PreferedLabComments)
                        End If
                        oPreferedLabs = New LabActor.PreferedLabResult
                        With oPreferedLabs
                            .TestMstPreferredLabID = 0
                            .TestID = _TestID
                            .TLabCI_Id = _PreferedLabCIID
                            .sComments = StrComment
                        End With
                        .Add(oPreferedLabs)
                        oPreferedLabs = Nothing

                    Next
                End With
                '----------Prefered Labs----------'

                If optProfile.Checked = True Then

                    '20090819 Not allowed to change the cell position
                    C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
                    With .Results
                        For i As Int16 = 1 To C1LabResult.Rows.Count - 1

                            _ResultName = "" : _ValueType = 0

                            _ResultName = C1LabResult.GetData(i, COL_RESULTNAME) & ""
                            If C1LabResult.GetData(i, COL_VALUETYPE) & "" = sResultType_Text Then
                                _ValueType = 1
                            ElseIf C1LabResult.GetData(i, COL_VALUETYPE) & "" = sResultType_Numeric Then
                                _ValueType = 2
                            End If

                            If _ResultName <> "" And _ValueType <> 0 Then
                                oLabResult = New LabActor.TestResult
                                With oLabResult
                                    .TestID = 0
                                    .ResultID = i
                                    .ResultName = _ResultName
                                    .ValueType = _ValueType
                                    .Unit = C1LabResult.GetData(i, COL_UNIT) & ""
                                    .DefaultValue = C1LabResult.GetData(i, COL_DEFAULTVALUE) & ""
                                    .ReferenceRange = C1LabResult.GetData(i, COL_REFRANGE) & ""
                                    .Comments = C1LabResult.GetData(i, COL_COMMENTS) & ""
                                    .Instruction = C1LabResult.GetData(i, COL_INSTRUCTION) & ""
                                    .BoundID = 0
                                    .BoundMaleLower = C1LabResult.GetData(i, COL_MALELB) & ""
                                    .BoundMaleUpper = C1LabResult.GetData(i, COL_MALEUB) & ""
                                    .BoundFemaleLower = C1LabResult.GetData(i, COL_FEMALELB) & ""
                                    .BoundFemaleUpper = C1LabResult.GetData(i, COL_FEMALEUB) & ""
                                    .LoincID = C1LabResult.GetData(i, COL_LOINCID) & "" 'added by sagaarK on 10 jan 08 
                                    .AlternateResultCode = C1LabResult.GetData(i, COL_AlternateResultCode) & "" 'added by sagaarK on 10 jan 08 
                                End With
                                .Add(oLabResult)
                                oLabResult = Nothing
                            End If

                        Next
                    End With



                Else
                    'optsingle
                    With .Results
                        _ResultName = "" : _ValueType = 0

                        _ResultName = txtResultName.Text
                        If Not cmbValueType.SelectedItem Is Nothing Then
                            If cmbValueType.SelectedItem & "" = sResultType_Text Then
                                _ValueType = 1
                            ElseIf cmbValueType.SelectedItem & "" = sResultType_Numeric Then
                                _ValueType = 2
                            End If
                        End If
                        oLabResult = New LabActor.TestResult
                        With oLabResult
                            .TestID = 0
                            .ResultID = 1
                            .ResultName = _ResultName
                            .ValueType = _ValueType
                            .ReferenceRange = txtRefRange.Text
                            .Comments = txtComment.Text
                            .LoincID = txtLoincId.Text 'added by sagaarK on 10 jan 08 
                            .AlternateResultCode = txtAlternateResultCode.Text
                        End With
                        .Add(oLabResult)
                        oLabResult = Nothing
                    End With
                End If
            End With


            If blnIsModify = True Then
                ''_arrLabs parameter added by Mayuri:20100617-To Associate multiple EM fields from Labs-Case No:0003710
                oLab.Add(_arrLabs, _arrOrders, _arrOtherDiag, _arrManagment, _TestID)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "EM field  associated.  ", gloAuditTrail.ActivityOutCome.Success)
            Else
                ''_arrLabs parameter added by Mayuri:20100617-To Associate multiple EM fields from Labs-Case No:0003710
                oLab.Modify(nEditID, _arrLabs, _arrOrders, _arrOtherDiag, _arrManagment)
                '' update association records as per update flag ("Orderset")

                '' ------------
                ''GLO2010-0004476 : Updates to Labs/Orders not reflected in Smart Orders already configured
                oLab.UpdateAssociatedSmartOrders(oLab.LabActor.Name.Trim(), nEditID)
                oLab.UpdateAssociatedSmartDiagnosis(oLab.LabActor.Name.Trim(), nEditID, oLab.LabActor.Code)
                '' UpdateAssociatedSmartTreatment not available due to in assocation table on id is considered
                '' ------------

                If cmbAssociatedEM.SelectedItem = "Select EM field" Then
                    'Audit Trail
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Delete, "EM field  deleted.  ", gloAuditTrail.ActivityOutCome.Success)
                Else
                    'Audit Trail
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "EM field  modified.  ", gloAuditTrail.ActivityOutCome.Success)
                End If


            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "EM field  added", gloAuditTrail.ActivityOutCome.Success)
            If blnIsModify = True Then
                txtName.Text = ""
                txtCode.Text = ""
                txtInstruction.Text = ""
                txtPrecaution.Text = ""
                txtResultName.Text = "None"
                txtRefRange.Text = ""
                txtComment.Text = ""
                txtLoincId.Text = "" 'added by sagaarK on 10 jan 08 

                'sarika 6th june 07
                DesignResultGrid(50, Nothing)

                txtCode.Select()
                ' Else
                ' Me.Close()
            End If

            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(oLab) = False) Then
                oLab.Dispose()
                oLab = Nothing
            End If

            '  oLabResult = Nothing
        End Try
    End Sub

    Private Sub DesignResultGrid(Optional ByVal rowcount As Integer = 1, Optional ByVal Results As gloEMRGeneralLibrary.gloEMRActors.LabActor.TestResults = Nothing)


        With C1LabResult
            .Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 1

            .Cols(COL_NO).Visible = True
            .Cols(COL_RESULTNAME).Visible = True
            .Cols(COL_VALUETYPE).Visible = True
            .Cols(COL_UNIT).Visible = False
            .Cols(COL_DEFAULTVALUE).Visible = False
            .Cols(COL_REFRANGE).Visible = True
            .Cols(COL_COMMENTS).Visible = True
            .Cols(COL_INSTRUCTION).Visible = False
            .Cols(COL_BOUNDID).Visible = False
            .Cols(COL_MALELB).Visible = False
            .Cols(COL_MALEUB).Visible = False
            .Cols(COL_FEMALELB).Visible = False
            .Cols(COL_FEMALEUB).Visible = False
            .Cols(COL_LOINCID).Visible = True 'added by sagaarK on 10012008
            .Cols(COL_AlternateResultCode).Visible = True 'added by sagaarK on 10012008

            .Cols(COL_NO).AllowEditing = False
            .Cols(COL_RESULTNAME).AllowEditing = True
            .Cols(COL_VALUETYPE).AllowEditing = True
            .Cols(COL_UNIT).AllowEditing = False
            .Cols(COL_DEFAULTVALUE).AllowEditing = False
            .Cols(COL_REFRANGE).AllowEditing = True
            .Cols(COL_COMMENTS).AllowEditing = True
            .Cols(COL_INSTRUCTION).AllowEditing = False
            .Cols(COL_BOUNDID).AllowEditing = False
            .Cols(COL_MALELB).AllowEditing = False
            .Cols(COL_MALEUB).AllowEditing = False
            .Cols(COL_FEMALELB).AllowEditing = False
            .Cols(COL_FEMALEUB).AllowEditing = False
            .Cols(COL_LOINCID).AllowEditing = True 'added by sagaarK on 10012008
            .Cols(COL_AlternateResultCode).AllowEditing = True 'added by sagaarK on 10012008

            ''.Cols(COL_NO).Width = 50
            .Cols(COL_NO).Width = 0         ''Dhruv -> Hidding column [No] 
            .Cols(COL_RESULTNAME).Width = 140
            .Cols(COL_VALUETYPE).Width = 80
            .Cols(COL_UNIT).Width = 0
            .Cols(COL_DEFAULTVALUE).Width = 0
            .Cols(COL_REFRANGE).Width = 100
            .Cols(COL_COMMENTS).Width = 150
            .Cols(COL_INSTRUCTION).Width = 0
            .Cols(COL_BOUNDID).Width = 0
            .Cols(COL_MALELB).Width = 0
            .Cols(COL_MALEUB).Width = 0
            .Cols(COL_FEMALELB).Width = 0
            .Cols(COL_FEMALEUB).Width = 0
            .Cols(COL_LOINCID).Width = 125 'added by sagaarK on 10012008
            .Cols(COL_AlternateResultCode).Width = 100 'added by sagaarK on 10012008

            .Cols(COL_NO).DataType = GetType(Int16)
            .Cols(COL_RESULTNAME).DataType = GetType(String)
            .Cols(COL_REFRANGE).DataType = GetType(String)
            .Cols(COL_COMMENTS).DataType = GetType(String)
            .Cols(COL_VALUETYPE).DataType = GetType(String)
            .Cols(COL_VALUETYPE).ComboList = sResultType_Text & "|" & sResultType_Numeric
            .Cols(COL_LOINCID).DataType = GetType(String) 'added by sagaarK on 10012008
            .Cols(COL_AlternateResultCode).DataType = GetType(String) 'added by sagaarK on 10012008

            .SetData(0, COL_NO, "No.")
            .SetData(0, COL_RESULTNAME, "Result Name")
            .SetData(0, COL_VALUETYPE, "Value Type")
            .SetData(0, COL_REFRANGE, "Ref. Range")
            .SetData(0, COL_COMMENTS, "Comments")
            .SetData(0, COL_LOINCID, "Lab Test Code") 'added by sagaarK on 10012008
            .SetData(0, COL_AlternateResultCode, "LOINC Code") 'added by sagaarK on 10012008

            .Rows(.Rows.Count - 1).Height = 22

            ''Dhruv-> As there is the 1 row as fixed and then adding the second row, the value of the second row should start with 1
            'For i As Int16 = 1 To rowcount ''Commented -> Dhruv Initial code
            For i As Int16 = 2 To rowcount
                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_NO, i)
                .Rows(.Rows.Count - 1).Height = 22
            Next

            'sarika 6th june 07
            If Not IsNothing(Results) Then


                For i As Integer = 0 To Results.Count - 1
                    C1LabResult.SetData(i + 1, COL_RESULTNAME, Results.Item(i).ResultName)
                    If Results.Item(i).ValueType = LabActor.enumTestResultValueType.Text Then
                        C1LabResult.SetData(i + 1, COL_VALUETYPE, sResultType_Text)
                    ElseIf Results.Item(i).ValueType = LabActor.enumTestResultValueType.Numeric Then
                        C1LabResult.SetData(i + 1, COL_VALUETYPE, sResultType_Numeric)
                    End If
                    C1LabResult.SetData(i + 1, COL_REFRANGE, Results.Item(i).ReferenceRange)
                    C1LabResult.SetData(i + 1, COL_COMMENTS, Results.Item(i).Comments)
                    C1LabResult.SetData(i + 1, COL_LOINCID, Results.Item(i).LoincID) 'added by sagaarK on 10012008
                    C1LabResult.SetData(i + 1, COL_AlternateResultCode, Results.Item(i).AlternateResultCode) 'added by sagaarK on 10012008
                Next
            End If

        End With
    End Sub

    Private Sub DesignPreferedLabGrid(Optional ByVal rowcount As Integer = 1, Optional ByVal dtPreferedLab As DataTable = Nothing)


        With C1PreferedLab
            .Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_PCount
            .Cols.Fixed = 1

            .Cols(COL_PreferedLabNo).Visible = False
            .Cols(COL_PreferedLabTestNo).Visible = False
            .Cols(COL_PreferedLabTestCINo).Visible = False
            .Cols(COL_PreferedLabName).Visible = True
            .Cols(COL_PreferedLabComments).Visible = True


            .Cols(COL_PreferedLabNo).AllowEditing = False
            .Cols(COL_PreferedLabTestNo).AllowEditing = False
            .Cols(COL_PreferedLabTestCINo).AllowEditing = False
            .Cols(COL_PreferedLabName).AllowEditing = True
            .Cols(COL_PreferedLabComments).AllowEditing = True



            .Cols(COL_PreferedLabNo).Width = 0
            .Cols(COL_PreferedLabTestNo).Width = 0
            .Cols(COL_PreferedLabTestCINo).Width = 0
            .Cols(COL_PreferedLabName).Width = 250
            .Cols(COL_PreferedLabComments).Width = 440



            Dim dt As DataTable = Nothing
            dt = FillPreferedLabs()
            If Not IsNothing(dt) Then
                If sPreferedLabName = " " Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        sPreferedLabName = sPreferedLabName & "|" & dt.Rows(i)("labci_ContactName")
                    Next
                End If
            End If

            .Cols(COL_PreferedLabNo).DataType = GetType(Int64)
            .Cols(COL_PreferedLabTestNo).DataType = GetType(Int64)
            .Cols(COL_PreferedLabTestCINo).DataType = GetType(Int64)
            .Cols(COL_PreferedLabName).DataType = GetType(String)

            .Cols(COL_PreferedLabName).Style.ComboList = sPreferedLabName


            .Cols(COL_PreferedLabComments).DataType = GetType(String)

            .SetData(0, COL_PreferedLabNo, "Lab No")
            .SetData(0, COL_PreferedLabTestNo, "Lab Test No")
            .SetData(0, COL_PreferedLabTestCINo, "Lab Test CI No")
            .SetData(0, COL_PreferedLabName, "Lab Name")
            .SetData(0, COL_PreferedLabComments, "Comments")


            .Rows(.Rows.Count - 1).Height = 22

            For i As Int16 = 2 To rowcount
                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_PreferedLabNo, i)
                .Rows(.Rows.Count - 1).Height = 22
            Next

            If Not IsNothing(dtPreferedLab) Then
                If nEditID > 0 Then
                    .Rows.Add()
                End If
            End If

            If Not IsNothing(dtPreferedLab) Then
                For i As Integer = 0 To dtPreferedLab.Rows.Count - 1
                    C1PreferedLab.SetData(i + 1, COL_PreferedLabName, dtPreferedLab.Rows(i)("labci_ContactName"))
                    C1PreferedLab.SetData(i + 1, COL_PreferedLabComments, dtPreferedLab.Rows(i)("sComments"))
                Next
            End If
        End With
    End Sub

    Private Sub optSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSingle.CheckedChanged
        If optSingle.Checked = True Then
            optSingle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSingle.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            gbSingle.Visible = True
            gbProfile.Visible = False
        End If
    End Sub

    Private Sub optProfile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optProfile.CheckedChanged

        If optProfile.Checked = True Then
            '20090817
            'We have to fixed problem of dragging header in c1LabResult
            C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            'DesignResultGrid()
            optProfile.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optProfile.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            'DesignResultGrid(100)
            gbSingle.Visible = False
            gbProfile.Visible = True

        End If
        'If blnIsModify = False Then
        '    'Modify

        '    DesignResultGrid()

        'Else
        '    DesignResultGrid(100)
        'End If
    End Sub




    Private Sub C1LabResult_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1LabResult.AfterEdit
        If e.Row > 0 Then
            If C1LabResult.GetData(e.Row - 1, COL_RESULTNAME) & "" = "" Or C1LabResult.GetData(e.Row - 1, COL_VALUETYPE) & "" = "" Then
                C1LabResult.SetData(e.Row, e.Col, Nothing)
            End If
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub tlsp_TestMaster_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_TestMaster.ItemClicked, miniToolStrip.ItemClicked, ToolStrip1.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    oCPTControl_InternalGridLostFocus(Nothing, Nothing)
                    oLOINCOrderControl_InternalGridLostFocus(Nothing, Nothing)
                    SaveTestMaster()

                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub

    Private Sub cmbAssociatedEM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssociatedEM.SelectedIndexChanged

    End Sub

    Private Sub C1LabResult_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1LabResult.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub g(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1LabResult.Enter

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            Dim ofrm As New frmEMTagAssociation(_arrLabs, _arrOrders, _arrOtherDiag, _arrManagment, "Labs")
            With ofrm
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                _arrLabs = .arrLabs
                _arrManagment = .arrManagment
                _arrOrders = .arrOrders
                _arrOtherDiag = .arrOtherDiag
            End With
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception

        End Try
    End Sub




#Region "Grid List Control"

    Private Sub txt_testLoinicCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_testLoinicCode.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlLOINCControl.Visible Then
                    If oLOINCOrderControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oLOINCOrderControl.GetCurrentSelectedItem()
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlLOINCControl.Visible Then
                    If oLOINCOrderControl IsNot Nothing Then
                        oLOINCOrderControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlLOINCControl.Visible Then
                    If oLOINCOrderControl IsNot Nothing Then
                        oLOINCOrderControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txt_testLoinicCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_testLoinicCode.LostFocus
        If pnlLOINCControl.Visible Then
            If oLOINCOrderControl IsNot Nothing Then
                If oLOINCOrderControl.Focus() = False Then
                    If oLOINCOrderControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("LOINCCode")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txt_testLoinicCode_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_testLoinicCode.MouseHover
        ToolTip1.SetToolTip(txt_testLoinicCode, txt_testLoinicCode.Text)
    End Sub


    Private Sub txt_testLoinicCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_testLoinicCode.TextChanged
        Try

            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txt_testLoinicCode.Text
                If (_strSearchString.Trim() <> "") Then
                    If IsNothing(oLOINCOrderControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LOINCCode, "LOINCCode", "")
                    Else
                        If oLOINCOrderControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LOINCCode, "LOINCCode", "")
                        End If
                    End If

                    If Not IsNothing(oLOINCOrderControl) Then
                        oLOINCOrderControl.FillControl(_strSearchString)
                        '  LoincItem = oLOINCOrderControl.SelectedContent
                    End If
                Else
                    ' cmbCPT.Items.Clear()
                End If
            End If

            If IsNothing(oLOINCOrderControl) = False Then
                If oLOINCOrderControl.C1GridList.Focused Then
                    SetCursorPos(txt_testLoinicCode.Left + Me.Left + 270, txt_testLoinicCode.Top + Me.Top + 70)
                End If
                RemoveHandler oLOINCOrderControl.InternalGridLostFocus, AddressOf oLOINCOrderControl_InternalGridLostFocus
                txt_testLoinicCode.Focus()
                txt_testLoinicCode.SelectionStart = Len(txt_testLoinicCode.Text)
                AddHandler oLOINCOrderControl.InternalGridLostFocus, AddressOf oLOINCOrderControl_InternalGridLostFocus
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenProcedureControl(ByVal ControlType As gloUserControlLibrary.gloGridListControlType, ByVal ControlHeader As String, ByVal SearchText As String)

        Try
            If ControlType = gloUserControlLibrary.gloGridListControlType.LOINCCode Then
                If oLOINCOrderControl IsNot Nothing Then
                    CloseProcedureControl("LOINCCode")
                End If
                oLOINCOrderControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.LOINCCode, True, 100)
                oLOINCOrderControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oLOINCOrderControl.ItemSelected, AddressOf oLOINCOrderControl_ItemSelected
                AddHandler oLOINCOrderControl.InternalGridLostFocus, AddressOf oLOINCOrderControl_InternalGridLostFocus
                AddHandler oLOINCOrderControl.C1GridList.MouseMove, AddressOf oLOINCOrderControl_MouseMove
                oLOINCOrderControl.LOINCOrCPTName = txt_testLoinicCode.Text.Trim
                oLOINCOrderControl.ShowHeader = False
                oLOINCOrderControl.ControlHeader = ControlHeader
                pnlLOINCControl.Controls.Add(oLOINCOrderControl)
                oLOINCOrderControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oLOINCOrderControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oLOINCOrderControl.Margin = New Padding(150, 100, 100, 150)
                oLOINCOrderControl.Show()
                pnlLOINCControl.Visible = True
                RaiseEvent GridListLoaded()
                pnlLOINCControl.BringToFront()
            ElseIf ControlType = gloUserControlLibrary.gloGridListControlType.LabsCPT Then
                If oCPTControl IsNot Nothing Then
                    CloseProcedureControl("LabsCPT")
                End If
                oCPTControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.LabsCPT, True, 100)
                oCPTControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oCPTControl.ItemSelected, AddressOf oCPTControl_ItemSelected
                AddHandler oCPTControl.InternalGridLostFocus, AddressOf oCPTControl_InternalGridLostFocus
                AddHandler oCPTControl.C1GridList.MouseMove, AddressOf oCPTControl_MouseMove
                oCPTControl.LOINCOrCPTName = txtCPTCode.Text.Trim
                oCPTControl.ShowHeader = False
                oCPTControl.ControlHeader = ControlHeader
                pnlCPTCode.Controls.Add(oCPTControl)
                oCPTControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oCPTControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oCPTControl.Show()
                pnlCPTCode.Visible = True
                RaiseEvent GridListLoaded()
                pnlCPTCode.BringToFront()


            End If

        Catch ex As Exception
            '  gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
        Finally
        End Try
    End Sub

    Private Sub CloseProcedureControl(ByVal _controlName As String)

        Try

            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            If _controlName = "LOINCCode" And blnLOINCSelectorClicked = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlLOINCControl.Controls.Count - 1 To 0 Step -1
                    pnlLOINCControl.Controls.RemoveAt(i)
                Next
                If oLOINCOrderControl IsNot Nothing Then
                    RemoveHandler oLOINCOrderControl.ItemSelected, AddressOf oLOINCOrderControl_ItemSelected
                    RemoveHandler oLOINCOrderControl.InternalGridLostFocus, AddressOf oLOINCOrderControl_InternalGridLostFocus
                    RemoveHandler oLOINCOrderControl.C1GridList.MouseMove, AddressOf oLOINCOrderControl_MouseMove

                    oLOINCOrderControl.Dispose()
                    oLOINCOrderControl = Nothing
                End If
                pnlLOINCControl.Visible = False
                RaiseEvent GridListClosed()
                pnlLOINCControl.SendToBack()
                '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            ElseIf _controlName = "LabsCPT" And blnCPTSelectorClicked = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlCPTCode.Controls.Count - 1 To 0 Step -1
                    pnlCPTCode.Controls.RemoveAt(i)
                Next
                If oCPTControl IsNot Nothing Then
                    RemoveHandler oCPTControl.ItemSelected, AddressOf oCPTControl_ItemSelected
                    RemoveHandler oCPTControl.InternalGridLostFocus, AddressOf oCPTControl_InternalGridLostFocus
                    RemoveHandler oCPTControl.C1GridList.MouseMove, AddressOf oCPTControl_MouseMove

                    oCPTControl.Dispose()
                    oCPTControl = Nothing
                End If
                pnlCPTCode.Visible = False
                RaiseEvent GridListClosed()
                pnlCPTCode.SendToBack()
            End If


        Catch ex As Exception
            ''   gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
        Finally

        End Try

    End Sub
    Private Sub oLOINCOrderControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oLOINCOrderControl.C1GridList.Select()
    End Sub
    Private Sub oLOINCOrderControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oLOINCOrderControl.SelectedItems IsNot Nothing Then
                If oLOINCOrderControl.SelectedItems.Count > 0 Then
                    oProcedure = oLOINCOrderControl.SelectedItems(0)
                    Select Case oLOINCOrderControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.LOINCCode
                            txt_testLoinicCode.Text = oProcedure.Description
                    End Select
                    CloseProcedureControl("LOINCCode")
                Else
                End If
            End If


            txt_testLoinicCode.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oLOINCOrderControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 1")
        If pnlLOINCControl.Visible Then
            'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 2")
            If oLOINCOrderControl IsNot Nothing Then
                'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 3")
                If oLOINCOrderControl.IsLabsRecordExist(txt_testLoinicCode.Text.Trim) = False Then
                    'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 4")
                    txt_testLoinicCode.Text = ""
                    CloseProcedureControl("LOINCCode")
                    'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 5")
                Else
                    CloseProcedureControl("LOINCCode")
                    'Debug.WriteLine("oLOINCOrderControl_InternalGridLostFocus Called 6")
                End If
            End If
        End If
    End Sub


    Private Sub btnLOINCCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOINCCode.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnLOINCSelectorClicked = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LOINCCode, "LOINCCode", "")
            oLOINCOrderControl.FillControl("")
            _isLoadGridCvxControl = False
            txt_testLoinicCode.Select()
            '  LoincItem = oLOINCOrderControl.SelectedContent
            blnLOINCSelectorClicked = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCPTCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCPTCode.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlCPTCode.Visible Then
                    If oCPTControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oCPTControl.GetCurrentSelectedItem()
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlCPTCode.Visible Then
                    If oCPTControl IsNot Nothing Then
                        oCPTControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlCPTCode.Visible Then
                    If oCPTControl IsNot Nothing Then
                        oCPTControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtCPTCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.LostFocus
        If pnlCPTCode.Visible Then
            If oCPTControl IsNot Nothing Then
                If oCPTControl.Focus() = False Then
                    If oCPTControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("LabsCPT")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCPTCode_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.MouseHover
        ToolTip1.SetToolTip(txtCPTCode, txtCPTCode.Text)
    End Sub

    Private Sub txtCPTCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPTCode.TextChanged
        Try

            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtCPTCode.Text
                If (_strSearchString.Trim() <> "") Then
                    If IsNothing(oCPTControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LabsCPT, "CPT", "")
                    Else
                        If oCPTControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LabsCPT, "CPT", "")
                        End If
                    End If

                    If Not IsNothing(oCPTControl) Then
                        oCPTControl.FillControl(_strSearchString)
                    End If
                Else
                    ' cmbCPT.Items.Clear()
                End If
            End If

            If IsNothing(oCPTControl) = False Then
                If oCPTControl.C1GridList.Focused Then
                    SetCursorPos(txtCPTCode.Left + Me.Left + 270, txtCPTCode.Top + Me.Top + 70)
                End If
                RemoveHandler oCPTControl.InternalGridLostFocus, AddressOf oCPTControl_InternalGridLostFocus
                txtCPTCode.Focus()
                txtCPTCode.SelectionStart = Len(txtCPTCode.Text)
                AddHandler oCPTControl.InternalGridLostFocus, AddressOf oCPTControl_InternalGridLostFocus
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oCPTControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlCPTCode.Visible Then
            If oCPTControl IsNot Nothing Then
                If oCPTControl.IsLabsRecordExist(txtCPTCode.Text.Trim) = False Then
                    txtCPTCode.Text = ""
                    CloseProcedureControl("LabsCPT")
                Else
                    CloseProcedureControl("LabsCPT")
                End If
            End If
        End If
    End Sub
    Private Sub oCPTControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oCPTControl.C1GridList.Select()
    End Sub
    Private Sub oCPTControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oCPTControl.SelectedItems IsNot Nothing Then
                If oCPTControl.SelectedItems.Count > 0 Then
                    oProcedure = oCPTControl.SelectedItems(0)
                    Select Case oCPTControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.LabsCPT
                            txtCPTCode.Text = oProcedure.Description
                    End Select
                    CloseProcedureControl("LabsCPT")
                Else
                End If
            End If


            txtCPTCode.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnCPTSelectorClicked = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.LabsCPT, "CPT", "")
            oCPTControl.FillControl("")
            _isLoadGridCvxControl = False
            txtCPTCode.Select()
            blnCPTSelectorClicked = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region



    Private Sub BtnAddLOINCCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddLOINCCode.Click
        Dim oLoincMst As New frmLab_LOINCOrderCodeMst
        oLoincMst.nEditID = 0
        oLoincMst.sEditCode = ""
        oLoincMst.sEditName = ""
        oLoincMst.blnIsModify = True
        oLoincMst.Text = "LOINC Order Code Master"
        oLoincMst.Location = New Point(150, 152)
        oLoincMst.ShowDialog(IIf(IsNothing(oLoincMst.Parent), Me, oLoincMst.Parent))
        oLoincMst.Dispose()
        oLoincMst = Nothing
    End Sub

    Private Sub btnCPT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.LostFocus
        If pnlCPTCode.Visible Then
            If oCPTControl IsNot Nothing Then
                If oCPTControl.Focus() = False Then
                    If oCPTControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("LabsCPT")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnLOINCCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLOINCCode.LostFocus
        If pnlLOINCControl.Visible Then
            If oLOINCOrderControl IsNot Nothing Then
                If oLOINCOrderControl.Focus() = False Then
                    If oLOINCOrderControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("LOINCCode")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cmbMUReportingCat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMUReportingCat.SelectedIndexChanged
        If (cmbMUReportingCat.Text = "Referral") Then
            chkOutboundTransistion.Checked = True
        End If
    End Sub

    Private Sub C1PreferedLab_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1PreferedLab.AfterEdit
        C1PreferedLab.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        If e.Row > 0 Then

            For i As Integer = 1 To C1PreferedLab.Rows.Count - 1
                If (i - 1 <> e.Row) Then
                    If Not IsNothing(C1PreferedLab.GetData(i - 1, COL_PreferedLabName)) Then
                        If (C1PreferedLab.GetData(i - 1, COL_PreferedLabName) <> " " And C1PreferedLab.GetData(e.Row, COL_PreferedLabName) <> " ") Then
                            If C1PreferedLab.GetData(i - 1, COL_PreferedLabName) = C1PreferedLab.GetData(e.Row, COL_PreferedLabName) Then
                                MessageBox.Show("Duplicate Labs not allowed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                C1PreferedLab.SetData(e.Row, e.Col, Nothing)
                            End If
                        End If
                    End If
                End If
            Next
        End If
        With C1PreferedLab
            If e.Row > 0 Then
                If TypeOf .Editor Is TextBox Then
                    Dim tb As TextBox = .Editor
                    tb.MaxLength = 500
                    If IsNothing(tb) Then
                        If Not IsNothing(C1PreferedLab.GetData(e.Row, COL_PreferedLabComments)) AndAlso C1PreferedLab.GetData(e.Row, COL_PreferedLabComments) & "" <> "" Then
                            tb = C1PreferedLab.GetData(e.Row, COL_PreferedLabComments)
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub C1PreferedLab_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles C1PreferedLab.KeyDown
        If C1PreferedLab.Row > 0 Then
            If e.KeyCode = 46 Then
                C1PreferedLab.RemoveItem(C1PreferedLab.Row)
            Else
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub C1PreferedLab_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1PreferedLab.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1PreferedLab_SelChange(sender As Object, e As System.EventArgs) Handles C1PreferedLab.SelChange

    End Sub

    Private Sub C1PreferedLab_SetupEditor(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1PreferedLab.SetupEditor
        With C1PreferedLab
            If e.Row > 0 Then
                'C1PreferedLab.GetCellRange(e.Row, e.Col)

                If TypeOf .Editor Is TextBox Then
                    Dim tb As TextBox = .Editor
                    tb.MaxLength = 500
                    If IsNothing(tb) Then
                        If Not IsNothing(C1PreferedLab.GetData(e.Row, COL_PreferedLabComments)) AndAlso C1PreferedLab.GetData(e.Row, COL_PreferedLabComments) & "" <> "" Then
                            tb = C1PreferedLab.GetData(e.Row, COL_PreferedLabComments)
                        End If
                    End If

                End If
            End If

        End With
    End Sub

    Private Sub btnPreferredLabDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnPreferredLabDelete.Click
        If C1PreferedLab.Row > 0 Then
            C1PreferedLab.RemoveItem(C1PreferedLab.Row)
        End If
    End Sub
End Class
