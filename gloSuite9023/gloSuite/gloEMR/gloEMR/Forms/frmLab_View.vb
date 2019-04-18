Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_View
    Dim MasterCriteria_Test As String = "Test"
    Dim MasterCriteria_Group As String = "Group"
    Dim MasterCriteria_Specimen As String = "Specimen"
    Dim MasterCriteria_CollectionContainer As String = "Collection Container"
    Dim MasterCriteria_StorageTemperature As String = "Storage Temperature"
    Dim MasterCriteria_LoincMst As String = "LOINC Order Code"

    ''Added for Orders & Resultset on 20140212
    Dim MasterCriteria_InternalNotes As String = "Acknowledgement-Internal Comments"
    Dim MasterCriteria_PatientNotes As String = "Acknowledgement-Patient Notes"
    ''End

    '//Order Related Masters
    Dim MasterCriteria_PreferredLab As String = "Preferred/Performing Lab"
    Dim MasterCriteria_ReferredBY As String = "Referred By"
    Dim MasterCriteria_SampledBy As String = "Sampled BY"
    Private Const COL_ID = 0
    Private Const COL_CODE = 1
    Private Const COL_NAME = 2
    Private Const COL_OrderType = 3
    Private Const COL_StructuredLabResults = 4
    Private Const COL_OBTranofCare = 5
    Private COL_COUNT = 3
    Dim oTests As LabActor.Tests = Nothing
    Private TestID As Long = 0  ''added for selecting particular record

    '07-Oct-14 Aniket Resolving Bug #74749: gloEMR:Orders & Results Setup-Highlight first record after refresh
    Private blnRefreshClicked As Boolean
    Private C1Formulary_DataTable As DataTable

    Private Sub frmLab_View_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If IsNothing(C1Formulary_DataTable) = False Then
            C1Formulary_DataTable.Dispose()
            C1Formulary_DataTable = Nothing
        End If


        If IsNothing(oTests) = False Then
            oTests.Dispose()
            oTests = Nothing
        End If


    End Sub

    Private Sub frmLab_TestView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  lblSearch.Text = "Search On Code"
        Try

            gloC1FlexStyle.Style(c1TestLibrary)
            With cmbCriteria
                .Items.Clear()
                .Items.Add(MasterCriteria_Test)
                .Items.Add(MasterCriteria_Group)
                .Items.Add(MasterCriteria_Specimen)
                .Items.Add(MasterCriteria_CollectionContainer)
                .Items.Add(MasterCriteria_StorageTemperature)
                .Items.Add(MasterCriteria_PreferredLab)
                .Items.Add(MasterCriteria_LoincMst)

                ''Added for Orders & Resultset on 20140212
                .Items.Add(MasterCriteria_InternalNotes)
                .Items.Add(MasterCriteria_PatientNotes)
                ''End

                'Line commented by dipak 20090905 
                'Items no Longer Used
                '.Items.Add(MasterCriteria_ReferredBY)
                '.Items.Add(MasterCriteria_SampledBy)
            End With
            Try
                RemoveHandler cmbordtype.SelectedIndexChanged, AddressOf cmbordtype_SelectedIndexChanged
                RemoveHandler cmbstruct.SelectedIndexChanged, AddressOf cmbstruct_SelectedIndexChanged
                RemoveHandler cmbtransition.SelectedIndexChanged, AddressOf cmbtransition_SelectedIndexChanged
            Catch ex As Exception
                ex = Nothing
            End Try
          

            cmbordtype.Items.Clear()

            cmbordtype.Items.Add("All")
            cmbordtype.Items.Add("Lab")
            cmbordtype.Items.Add("Radiology/Imaging")
            cmbordtype.Items.Add("Other")
            cmbordtype.Items.Add("Referral")
            'cmbCriteria.SelectedIndex = 0
            cmbordtype.SelectedItem = "All"
            cmbstruct.SelectedItem = "All"
            cmbtransition.SelectedItem = "All"
            'Dim _colName As String = ""
            '_colName = c1TestLibrary.Cols(0).Caption
            chkenbdate.Checked = False
            dtfromdate.Enabled = False
            dttodate.Enabled = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub SetGridStyle()
        With c1TestLibrary
           
            .Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Styles.Alternate.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
           
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        End With
    End Sub

    Private Sub Fill_List(ByVal Criteria As String)
        Try

            With c1TestLibrary
                '.Clear()
                SetGridStyle()
                c1TestLibrary.Visible = False

                c1TestLibrary.DataSource = Nothing
                COL_COUNT = 3
                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        .Cols.Count = 2

                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        .Cols.Count = 4
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        .Cols.Count = 2
                    Case MasterCriteria_Test
                        .Cols.Count = 6
                        COL_COUNT = 6
                    Case Else
                        COL_COUNT = 3
                        .Cols.Count = COL_COUNT
                End Select

                .Rows.Count = 1
                .Rows.Fixed = 1
                .Clear(C1.Win.C1FlexGrid.ClearFlags.All)
                ts_btnNormal.Visible = False
                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        .SetData(0, 0, "ID")
                        .SetData(0, 1, "Contact Name")
                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        .SetData(0, 0, "ID")
                        .SetData(0, 1, "First Name")
                        .SetData(0, 2, "Middle Name")
                        .SetData(0, 3, "Last Name")
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        .SetData(0, 0, "ID")
                        .SetData(0, 1, "Notes")
                    Case MasterCriteria_Test
                        .Cols(COL_ID).Width = 0
                        .Cols(COL_NAME).Width = 100
                        .Cols(COL_OrderType).Width = 80
                        .Cols(COL_StructuredLabResults).Width = 120
                        .SetData(0, COL_ID, "ID")
                        .SetData(0, COL_CODE, "Code")
                        .SetData(0, COL_NAME, "Name")
                        .SetData(0, COL_OrderType, "Order Type")
                        .SetData(0, COL_StructuredLabResults, "Structured Lab Results")
                        .SetData(0, COL_OBTranofCare, "Outbound Transition of Care")
                    Case Else
                        .SetData(0, COL_ID, "ID")
                        .SetData(0, COL_CODE, "Code")
                        .SetData(0, COL_NAME, "Name")
                End Select

            End With

            If Not Criteria.Trim = "" Then
                Select Case Criteria
                    Case MasterCriteria_Test
                        Dim blnrowselected As Boolean = False
                        Dim oLabTest As New gloEMRLabTest
                        If Not IsNothing(oTests) Then
                            oTests.Dispose()
                            oTests = Nothing
                        End If
                        ' oTests = New LabActor.Tests()
                        oTests = oLabTest.GetTestsNew(False)
                        If Not oTests Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 6
                                Dim strordertype = cmbordtype.Text.Trim()
                                'If (cmbordtype.Text = "Lab") Then ''8020 order prd changes
                                '    strordertype = ""
                                'End If
                                Dim strstructtype = cmbstruct.Text.Trim()
                                'If (cmbstruct.Text = "No") Then  ''8020 order prd changes
                                '    strstructtype = ""
                                'End If

                                Dim blnobtransition As Boolean = False
                                If (cmbtransition.Text = "Yes") Then
                                    blnobtransition = True
                                Else
                                    blnobtransition = False
                                End If

                                If chkenbdate.Checked = False Then
                                    For Each testResult As LabActor.Test In oTests
                                        ''If testResult.MUReportingCategory = strCategory Or strCategory = "All" Then
                                        ''condition added for order PRd 8020 
                                        ''if MUReportingCategory is blank the show it is lab change for Order PRD
                                        If ((testResult.IsStructuredLabTest = strstructtype Or cmbstruct.Text.Trim() = "All") Or (testResult.IsStructuredLabTest = "" And cmbstruct.Text.Trim() = "No")) And (testResult.MUReportingCategory = strordertype Or cmbordtype.Text.Trim() = "All" Or (testResult.MUReportingCategory = "" And cmbordtype.Text = "Lab")) And (testResult.bOutboundTransistion = blnobtransition Or cmbtransition.Text.Trim() = "All") Then

                                            '25-Sep-14 Aniket: Do not reset filter on saving a test
                                            If (txtListSearch.Text = "") Or (testResult.Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or testResult.Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then


                                                .Rows.Add()
                                                .SetData(.Rows.Count - 1, COL_ID, testResult.TestID)
                                                .SetData(.Rows.Count - 1, COL_CODE, testResult.Code)
                                                .SetData(.Rows.Count - 1, COL_NAME, testResult.Name)
                                                .SetData(.Rows.Count - 1, COL_OrderType, testResult.MUReportingCategory)
                                                .SetData(.Rows.Count - 1, COL_StructuredLabResults, testResult.IsStructuredLabTest)
                                                .SetData(.Rows.Count - 1, COL_OBTranofCare, testResult.bOutboundTransistion.ToString().Replace("True", "Yes").Replace("False", "No"))

                                                '07-Oct-14 Aniket Resolving Bug #74749: gloEMR:Orders & Results Setup-Highlight first record after refresh
                                                If (TestID = testResult.TestID) And blnRefreshClicked = False Then
                                                    c1TestLibrary.Select(.Rows.Count - 1, 0)
                                                    blnrowselected = True
                                                End If
                                            End If

                                        End If
                                    Next

                                Else
                                    For Each testResult As LabActor.Test In oTests
                                        ''If testResult.MUReportingCategory = strCategory Or strCategory = "All" Then
                                        ''condition added for order PRd 8020 
                                        ''if MUReportingCategory is blank the show it is lab change for Order PRD
                                        If (testResult.IsStructuredLabTest = strstructtype Or cmbstruct.Text.Trim() = "All") And (testResult.MUReportingCategory = strordertype Or cmbordtype.Text.Trim() = "All" Or (testResult.MUReportingCategory = "" And cmbordtype.Text = "Lab")) And (testResult.bOutboundTransistion = blnobtransition Or cmbtransition.Text.Trim() = "All") And (testResult.dtUpdatedDate >= dtfromdate.Value.ToShortDateString() And testResult.dtUpdatedDate <= dttodate.Value.ToShortDateString()) Then
                                            '25-Sep-14 Aniket: Do not reset filter on saving a test
                                            If (txtListSearch.Text = "") Or (testResult.Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or testResult.Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then


                                                .Rows.Add()
                                                .SetData(.Rows.Count - 1, COL_ID, testResult.TestID)
                                                .SetData(.Rows.Count - 1, COL_CODE, testResult.Code)
                                                .SetData(.Rows.Count - 1, COL_NAME, testResult.Name)
                                                .SetData(.Rows.Count - 1, COL_OrderType, testResult.MUReportingCategory)
                                                .SetData(.Rows.Count - 1, COL_StructuredLabResults, testResult.IsStructuredLabTest)
                                                .SetData(.Rows.Count - 1, COL_OBTranofCare, testResult.bOutboundTransistion.ToString().Replace("True", "Yes").Replace("False", "No"))

                                                If (TestID = testResult.TestID) Then
                                                    c1TestLibrary.Select(.Rows.Count - 1, 0)
                                                    blnrowselected = True
                                                End If
                                            End If

                                        End If
                                    Next

                                End If
                                If blnrowselected = False And TestID <> 0 Then
                                    If c1TestLibrary.Rows.Count >= 2 Then  ''changes made for bugid 68337
                                        c1TestLibrary.Select(1, 0)
                                    End If
                                End If

                            End With

                        End If
                        Try
                            RemoveHandler cmbordtype.SelectedIndexChanged, AddressOf cmbordtype_SelectedIndexChanged
                            RemoveHandler cmbstruct.SelectedIndexChanged, AddressOf cmbstruct_SelectedIndexChanged
                            RemoveHandler cmbtransition.SelectedIndexChanged, AddressOf cmbtransition_SelectedIndexChanged
                        Catch ex As Exception
                            ex = Nothing
                        End Try
                   

                        AddHandler cmbordtype.SelectedIndexChanged, AddressOf cmbordtype_SelectedIndexChanged
                        AddHandler cmbstruct.SelectedIndexChanged, AddressOf cmbstruct_SelectedIndexChanged
                        AddHandler cmbtransition.SelectedIndexChanged, AddressOf cmbtransition_SelectedIndexChanged

                        oLabTest.Dispose()
                        oLabTest = Nothing



                    Case MasterCriteria_Group
                        Dim oGroups As LabActor.LabGroups
                        Dim oLabGroup As New gloEMRLabGroup
                        oGroups = oLabGroup.GetGroups
                        If Not oGroups Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 3
                                For i As Int16 = 0 To oGroups.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oGroups.Item(i).LabGroupCode.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oGroups.Item(i).LabGroupName.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, COL_ID, oGroups.Item(i).LabGroupID)
                                        .SetData(.Rows.Count - 1, COL_CODE, oGroups.Item(i).LabGroupCode)
                                        .SetData(.Rows.Count - 1, COL_NAME, oGroups.Item(i).LabGroupName)
                                    End If
                                Next
                            End With
                            oGroups.Dispose()
                            oGroups = Nothing
                        End If
                       
                        oLabGroup.Dispose()
                        oLabGroup = Nothing
                    Case MasterCriteria_Specimen
                        
                        Dim oLabCSSTs As LabActor.LabCSSTs
                        Dim ogloEMRLabCSST As New gloEMRLabCSST
                        oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Specimen)
                        If Not oLabCSSTs Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 3
                                For i As Int16 = 0 To oLabCSSTs.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oLabCSSTs.Item(i).LabCSST_Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oLabCSSTs.Item(i).LabCSST_Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, COL_ID, oLabCSSTs.Item(i).LabCSST_ID)
                                        .SetData(.Rows.Count - 1, COL_CODE, oLabCSSTs.Item(i).LabCSST_Code)
                                        .SetData(.Rows.Count - 1, COL_NAME, oLabCSSTs.Item(i).LabCSST_Name)
                                    End If
                                Next
                            End With
                            oLabCSSTs.Dispose()
                            oLabCSSTs = Nothing
                        End If
                      
                        ogloEMRLabCSST.Dispose()
                        ogloEMRLabCSST = Nothing

                    Case MasterCriteria_CollectionContainer
                        Dim oLabCSSTs As LabActor.LabCSSTs
                        Dim ogloEMRLabCSST As New gloEMRLabCSST

                        oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Collection)

                        If Not oLabCSSTs Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 3

                                For i As Int16 = 0 To oLabCSSTs.Count - 1

                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oLabCSSTs.Item(i).LabCSST_Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oLabCSSTs.Item(i).LabCSST_Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, COL_ID, oLabCSSTs.Item(i).LabCSST_ID)
                                        .SetData(.Rows.Count - 1, COL_CODE, oLabCSSTs.Item(i).LabCSST_Code)
                                        .SetData(.Rows.Count - 1, COL_NAME, oLabCSSTs.Item(i).LabCSST_Name)
                                    End If
                                    
                                Next

                            End With
                            oLabCSSTs.Dispose()
                            oLabCSSTs = Nothing
                        End If
                     
                        ogloEMRLabCSST.Dispose()
                        ogloEMRLabCSST = Nothing

                    Case MasterCriteria_StorageTemperature

                        Dim oLabCSSTs As LabActor.LabCSSTs
                        Dim ogloEMRLabCSST As New gloEMRLabCSST

                        oLabCSSTs = ogloEMRLabCSST.GetLabCSSTs(gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.StorageTemperature)

                        If Not oLabCSSTs Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 3

                                For i As Int16 = 0 To oLabCSSTs.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oLabCSSTs.Item(i).LabCSST_Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oLabCSSTs.Item(i).LabCSST_Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, COL_ID, oLabCSSTs.Item(i).LabCSST_ID)
                                        .SetData(.Rows.Count - 1, COL_CODE, oLabCSSTs.Item(i).LabCSST_Code)
                                        .SetData(.Rows.Count - 1, COL_NAME, oLabCSSTs.Item(i).LabCSST_Name)
                                    End If
                                Next

                            End With
                            oLabCSSTs.Dispose()
                            oLabCSSTs = Nothing
                        End If

                      
                        ogloEMRLabCSST.Dispose()
                        ogloEMRLabCSST = Nothing

                    Case MasterCriteria_PreferredLab

                        Dim oContactInfos As LabActor.LabContactInformations
                        Dim oLabContactInfo As New gloEMRLabContactInfo
                        oContactInfos = oLabContactInfo.GetContactInformations(LabActor.enumContactType.PreferredLab)

                        If Not oContactInfos Is Nothing Then
                            With c1TestLibrary
                                For i As Int16 = 0 To oContactInfos.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oContactInfos.Item(i).ContactName.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, 0, oContactInfos.Item(i).ContactID)
                                        .SetData(.Rows.Count - 1, 1, oContactInfos.Item(i).ContactName)
                                    End If
                                Next
                            End With
                            oContactInfos.Dispose()
                            oContactInfos = Nothing
                        End If


                       
                        oLabContactInfo.Dispose()
                        oLabContactInfo = Nothing

                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        Dim oContactInfos As LabActor.LabContactInformations = Nothing
                        Dim oLabContactInfo As New gloEMRLabContactInfo

                        If Criteria = MasterCriteria_ReferredBY Then
                            oContactInfos = oLabContactInfo.GetContactInformations(LabActor.enumContactType.ReferredBy)
                        ElseIf Criteria = MasterCriteria_SampledBy Then
                            oContactInfos = oLabContactInfo.GetContactInformations(LabActor.enumContactType.SampledBy)
                        End If

                        If Not oContactInfos Is Nothing Then
                            With c1TestLibrary
                                For i As Int16 = 0 To oContactInfos.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oContactInfos.Item(i).FirstName.ToUpper.Contains(txtListSearch.Text.ToUpper) Or (oContactInfos.Item(i).MiddleName.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oContactInfos.Item(i).LastName.ToUpper.Contains(txtListSearch.Text.ToUpper))) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, 0, oContactInfos.Item(i).ContactID)
                                        .SetData(.Rows.Count - 1, 1, oContactInfos.Item(i).FirstName)
                                        .SetData(.Rows.Count - 1, 2, oContactInfos.Item(i).MiddleName)
                                        .SetData(.Rows.Count - 1, 3, oContactInfos.Item(i).LastName)
                                    End If
                                Next
                            End With
                            oContactInfos.Dispose()
                            oContactInfos = Nothing
                        End If
                       
                        oLabContactInfo.Dispose()
                        oLabContactInfo = Nothing
                        ''Added by Mayuri:201305300-Added Loinc Order Code Master-Order PRD changes
                    Case MasterCriteria_LoincMst

                        Dim oLabLoincMsts As LabActor.LabLoincMsts
                        Dim ogloEMRLabLoincMst As New gloEMRLabLoincMst
                        oLabLoincMsts = ogloEMRLabLoincMst.GetLabLoincMst()

                        If Not oLabLoincMsts Is Nothing Then
                            With c1TestLibrary
                                .Cols.Count = 3
                                For i As Int16 = 0 To oLabLoincMsts.Count - 1
                                    '25-Sep-14 Aniket: Do not reset filter on saving a test
                                    If (txtListSearch.Text = "") Or (oLabLoincMsts.Item(i).LabLoinc_Code.ToUpper.Contains(txtListSearch.Text.ToUpper) Or oLabLoincMsts.Item(i).LabLoinc_Name.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, COL_ID, oLabLoincMsts.Item(i).LabLoinc_ID)
                                        .SetData(.Rows.Count - 1, COL_CODE, oLabLoincMsts.Item(i).LabLoinc_Code)
                                        .SetData(.Rows.Count - 1, COL_NAME, oLabLoincMsts.Item(i).LabLoinc_Name)
                                    End If
                                Next
                            End With
                            oLabLoincMsts.Dispose()
                            oLabLoincMsts = Nothing
                        End If

                       
                        ogloEMRLabLoincMst.Dispose()
                        ogloEMRLabLoincMst = Nothing

                    Case MasterCriteria_InternalNotes
                        Dim oLabAckNotes As New LabAckNotes
                        Try
                            Dim dtNotes As DataTable = oLabAckNotes.Get_AckNotes(0, 0)
                            If Not dtNotes Is Nothing AndAlso dtNotes.Rows.Count > 0 Then
                                ts_btnNormal.Visible = True
                                With c1TestLibrary
                                    For i As Int32 = 0 To dtNotes.Rows.Count - 1
                                        '25-Sep-14 Aniket: Do not reset filter on saving a test
                                        If (txtListSearch.Text = "") Or (dtNotes.Rows(i)("labAckNotes").ToString.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                            .Rows.Add()
                                            .SetData(.Rows.Count - 1, 0, dtNotes.Rows(i)("labAckNotes_ID"))
                                            .SetData(.Rows.Count - 1, 1, dtNotes.Rows(i)("labAckNotes"))
                                        End If

                                    Next
                                End With
                            End If
                            If Not dtNotes Is Nothing Then
                                dtNotes.Dispose()
                                dtNotes = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            oLabAckNotes.Dispose()
                            oLabAckNotes = Nothing
                        End Try
                    Case MasterCriteria_PatientNotes
                        Dim oLabAckNotes As New LabAckNotes
                        Try
                            Dim dtNotes As DataTable = oLabAckNotes.Get_AckNotes(0, 1)
                            If Not dtNotes Is Nothing AndAlso dtNotes.Rows.Count > 0 Then
                                ts_btnNormal.Visible = True
                                With c1TestLibrary
                                    For i As Int32 = 0 To dtNotes.Rows.Count - 1
                                        '25-Sep-14 Aniket: Do not reset filter on saving a test
                                        If (txtListSearch.Text = "") Or (dtNotes.Rows(i)("labAckNotes").ToString.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                            .Rows.Add()
                                            .SetData(.Rows.Count - 1, 0, dtNotes.Rows(i)("labAckNotes_ID"))
                                            .SetData(.Rows.Count - 1, 1, dtNotes.Rows(i)("labAckNotes"))
                                        End If
                                    Next
                                End With
                            End If
                            If Not dtNotes Is Nothing Then
                                dtNotes.Dispose()
                                dtNotes = Nothing
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            oLabAckNotes.Dispose()
                            oLabAckNotes = Nothing
                        End Try
                End Select
            End If

            With c1TestLibrary
                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If
                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        If .Cols.Count = 4 Then
                            Dim _Width As Single = (.Width - 20) / 6
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 2
                            .Cols(2).Width = _Width * 2
                            .Cols(3).Width = _Width * 2
                        End If
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If
                    Case MasterCriteria_Test
                        Dim _Width As Single = (c1TestLibrary.Width)
                        .Cols(COL_ID).Width = 0
                        '.Cols(COL_CODE).Width = _Width * 0.15
                        '.Cols(COL_NAME).Width = _Width * 0.25
                        '.Cols(COL_OrderType).Width = _Width * 0.2
                        '.Cols(COL_StructuredLabResults).Width = _Width * 0.2
                        '.Cols(COL_OBTranofCare).Width = _Width * 0.2
                        .Cols(COL_CODE).Width = _Width * 0.25
                        .Cols(COL_NAME).Width = _Width * 0.35
                        .Cols(COL_OrderType).Width = _Width * 0.1
                        .Cols(COL_StructuredLabResults).Width = _Width * 0.12
                        .Cols(COL_OBTranofCare).Width = _Width * 0.16

                    Case Else
                        If .Cols.Count = COL_COUNT Then
                            Dim _Width As Single = (.Width - 20) / 5
                            .Cols(COL_ID).Width = 0
                            '.Cols(COL_CODE).Width = _Width * 1.5
                            .Cols(COL_CODE).Width = _Width * 1.0
                            '.Cols(COL_NAME).Width = _Width * 3.5
                            .Cols(COL_NAME).Width = _Width * 3.0
                        End If
                End Select
            End With


            c1TestLibrary.ColSel = 1


        Catch ex As Exception
            Throw ex
        Finally

            c1TestLibrary.Visible = True

        End Try

    End Sub

    Private Sub Redesign_Fill_List(ByVal Criteria As String)
        Try
            SetGridStyle()
            COL_COUNT = 3
            With c1TestLibrary
                .Cols.Fixed = 0

                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If
                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        If .Cols.Count = 4 Then
                            Dim _Width As Single = (.Width - 20) / 6
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 2
                            .Cols(2).Width = _Width * 2
                            .Cols(3).Width = _Width * 2
                        End If
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If

                    Case MasterCriteria_Test  '' added to solve bugid 67964
                        .Cols.Count = 6
                        COL_COUNT = 6

                        Dim _Width As Single = (c1TestLibrary.Width)
                        '.Cols(COL_ID).Width = 0
                        '.Cols(COL_CODE).Width = _Width * 0.15
                        '.Cols(COL_NAME).Width = _Width * 0.25
                        '.Cols(COL_OrderType).Width = _Width * 0.2
                        '.Cols(COL_StructuredLabResults).Width = _Width * 0.2
                        '.Cols(COL_OBTranofCare).Width = _Width * 0.2
                        .Cols(COL_ID).Width = 0
                        .Cols(COL_CODE).Width = _Width * 0.25
                        .Cols(COL_NAME).Width = _Width * 0.35
                        .Cols(COL_OrderType).Width = _Width * 0.1
                        .Cols(COL_StructuredLabResults).Width = _Width * 0.12
                        .Cols(COL_OBTranofCare).Width = _Width * 0.16
                    Case Else
                        If .Cols.Count = COL_COUNT Then
                            Dim _Width As Single = (.Width - 20) / 5
                            .Cols(COL_ID).Width = 0
                            '.Cols(COL_CODE).Width = _Width * 1.5
                            .Cols(COL_CODE).Width = _Width * 1.0
                            '.Cols(COL_NAME).Width = _Width * 3.5
                            .Cols(COL_NAME).Width = _Width * 3.0
                        End If
                End Select
            End With

            'sarika 28th may 07
            c1TestLibrary.ColSel = 1

        Catch ex As Exception
            Throw ex
        Finally

            c1TestLibrary.Visible = True

        End Try

    End Sub
    Private Sub frmLab_TestView_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim Criteria As String = ""
        If Not cmbCriteria.SelectedItem Is Nothing Then
            Criteria = cmbCriteria.SelectedItem
        End If


        Try


            With c1TestLibrary
                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If
                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        If .Cols.Count = 4 Then
                            Dim _Width As Single = (.Width - 20) / 6
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 2
                            .Cols(2).Width = _Width * 2
                            .Cols(3).Width = _Width * 2
                        End If
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        If .Cols.Count = 2 Then
                            Dim _Width As Single = (.Width - 20) / 1
                            .Cols(0).Width = 0
                            .Cols(1).Width = _Width * 1
                        End If
                    Case Else
                        If .Cols.Count = COL_COUNT Then
                            Dim _Width As Single = (.Width - 20) / 5
                            .Cols(COL_ID).Width = 0
                            .Cols(COL_CODE).Width = _Width * 1.5
                            .Cols(COL_NAME).Width = _Width * 3.5
                        End If
                End Select
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

   

    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        Try


            If Not cmbCriteria.SelectedItem Is Nothing Then
                If Not IsNothing(oTests) Then
                    oTests.Dispose()
                    oTests = Nothing
                End If
                Fill_List(cmbCriteria.SelectedItem)
                If cmbCriteria.SelectedItem = "Test" Then
                    cmbstruct.Visible = True
                    cmbordtype.Visible = True
                    cmbtransition.Visible = True
                    lblordtype.Visible = True
                    lblstruct.Visible = True
                    chkenbdate.Visible = True
                    dtfromdate.Visible = True
                    dttodate.Visible = True
                    lblfrdate.Visible = True
                    lbltodate.Visible = True
                    'Panel4.Height = 86
                    Panel2.Height = 104
                    GroupBox1.Visible = True
                Else
                    ' Panel4.Height = 50
                    Panel2.Height = 45
                    cmbstruct.Visible = False
                    cmbordtype.Visible = False
                    cmbtransition.Visible = False
                    lblordtype.Visible = False
                    lblstruct.Visible = False
                    chkenbdate.Visible = False
                    dtfromdate.Visible = False
                    dttodate.Visible = False
                    lblfrdate.Visible = False
                    lbltodate.Visible = False
                    GroupBox1.Visible = False
                End If
            End If
            c1TestLibrary.ColSel = 1

            ''commented on 20100105
            ''If c1TestLibrary.Cols(c1TestLibrary.ColSel).Caption = "ID" Then
            ''    lblSearch.Text = "Search on Code "
            ''    lblSearch.Padding = New Padding(2, 4, 2, 2)
            ''    lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            ''Else
            ''    lblSearch.Text = "Search on " & c1TestLibrary.Cols(c1TestLibrary.ColSel).Caption & ""
            ''End If

            txtListSearch.Text = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub txtListSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtListSearch.TextChanged

        

        Dim dvlab As DataView = Nothing

        ''dvlab = CType(c1TestLibrary.DataSource(), DataView)
        Dim oCol As DataColumn

        Try

            If txtListSearch.Text.Trim <> "" Then

                If Not cmbCriteria.SelectedItem Is Nothing Then
                    Fill_List(cmbCriteria.SelectedItem)
                End If

                If IsNothing(C1Formulary_DataTable) = False Then
                    C1Formulary_DataTable.Dispose()
                    C1Formulary_DataTable = Nothing
                End If

                C1Formulary_DataTable = New DataTable
                If c1TestLibrary.Cols.Count > 0 Then
                    oCol = New DataColumn
                    For i As Integer = 0 To c1TestLibrary.Cols.Count - 1
                        oCol.Caption = c1TestLibrary.GetData(0, i)
                        oCol.ColumnName = c1TestLibrary.GetData(0, i)
                        C1Formulary_DataTable.Columns.Add(c1TestLibrary.GetData(0, i))
                    Next

                End If

                Dim oRow As DataRow
                If c1TestLibrary.Rows.Count > 1 Then

                    For iRow As Integer = 1 To c1TestLibrary.Rows.Count - 1

                        oRow = C1Formulary_DataTable.NewRow

                        For iCol As Integer = 0 To c1TestLibrary.Cols.Count - 1
                            oRow(iCol) = c1TestLibrary.GetData(iRow, iCol)
                        Next
                        C1Formulary_DataTable.Rows.Add(oRow)
                    Next
                    dvlab = C1Formulary_DataTable.DefaultView
                End If

                If IsNothing(dvlab) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                'c1TestLibrary.Clear()
                c1TestLibrary.DataSource = Nothing
                c1TestLibrary.Clear(C1.Win.C1FlexGrid.ClearFlags.All)

                c1TestLibrary.DataSource = dvlab

                Dim strLab As String
                If Trim(txtListSearch.Text) <> "" Then
                    strLab = Replace(txtListSearch.Text, "'", "''")

                    ''If search string starts with '*' char then repalce all '*' chars except the one at start  
                    If (strLab.StartsWith("*") = True) Then

                        strLab = Replace(strLab, "*", "") & ""
                        strLab = "*" + strLab
                    Else
                        strLab = Replace(strLab, "*", "") & ""
                    End If
                    '''' for special characters in search.
                    strLab = Replace(strLab, "[", "") & ""
                    strLab = mdlGeneral.ReplaceSpecialCharacters(strLab)

                Else
                    strLab = ""
                End If

                Dim Criteria As String = cmbCriteria.SelectedItem

                Select Case Criteria
                    Case MasterCriteria_PreferredLab
                        ' c1TestLibrary.Cols.Count = 2
                        'Apply General Search 
                        dvlab.RowFilter = "[" & dvlab.Table.Columns(1).ColumnName & "]" & " Like '" & strLab & "%'" '' OR " _
                        ''& dvlab.Table.Columns(2).ColumnName & " Like '" & strLab & "%'"""

                    Case MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        'c1TestLibrary.Cols.Count = 4
                        'Apply General Search 
                        dvlab.RowFilter = dvlab.Table.Columns(1).ColumnName & " Like '%" & strLab & "%' OR " _
                                                          & dvlab.Table.Columns(2).ColumnName & " Like '%" & strLab & "%' OR " _
                                                          & dvlab.Table.Columns(3).ColumnName & " Like '%" & strLab & "%' OR " _
                                                          & dvlab.Table.Columns(4).ColumnName & " Like '%" & strLab & "%'"
                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                        dvlab.RowFilter = "[" & dvlab.Table.Columns(1).ColumnName & "]" & " Like '" & strLab & "%'"
                    Case Else
                        'c1TestLibrary.Cols.Count = COL_COUNT  ''3
                        'Apply General Search 
                        dvlab.RowFilter = dvlab.Table.Columns(1).ColumnName & " Like '%" & strLab & "%' OR " _
                                                          & dvlab.Table.Columns(2).ColumnName & " Like '%" & strLab & "%' " ''OR " _
                        ''& dvlab.Table.Columns(2).ColumnName & " Like '" & strLab & "%' "
                End Select

                'Set focus on first line from the filtered line.
                If dvlab.Count > 0 Then
                    c1TestLibrary.RowSel = 1
                End If

                If Not cmbCriteria.SelectedItem Is Nothing Then
                    Redesign_Fill_List(cmbCriteria.SelectedItem)
                End If

            Else
                ''If blank search text box
                If Not cmbCriteria.SelectedItem Is Nothing Then
                    Fill_List(cmbCriteria.SelectedItem)
                End If
            End If

            c1TestLibrary.ColSel = 1

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, "Lab", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        Try


            If Not cmbCriteria.SelectedItem Is Nothing Then
                Select Case cmbCriteria.SelectedItem
                    Case MasterCriteria_Test
                        Dim oTestMaster As New frmLab_TestMaster
                        oTestMaster.nEditID = 0
                        oTestMaster.sEditCode = ""
                        oTestMaster.sEditName = ""
                        oTestMaster.blnIsModify = True
                        oTestMaster.ShowDialog(IIf(IsNothing(oTestMaster.Parent), Me, oTestMaster.Parent))
                        TestID = oTestMaster._TestID
                        oTestMaster.Dispose()
                        oTestMaster = Nothing
                    Case MasterCriteria_Group
                        Dim oGroupMaster As New frmLab_GroupMaster
                        oGroupMaster.nEditID = 0
                        oGroupMaster.sEditCode = ""
                        oGroupMaster.sEditName = ""
                        oGroupMaster.blnIsModify = True
                        oGroupMaster.ShowDialog(IIf(IsNothing(oGroupMaster.Parent), Me, oGroupMaster.Parent))
                        oGroupMaster.Dispose()
                        oGroupMaster = Nothing
                    Case MasterCriteria_Specimen
                        Dim oSpecimenMaster As New frmLab_SpecimenMaster
                        oSpecimenMaster.nEditID = 0
                        oSpecimenMaster.sEditCode = ""
                        oSpecimenMaster.sEditName = ""
                        oSpecimenMaster.blnIsModify = True
                        oSpecimenMaster.enumtype = gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Specimen                      'for Specimen type
                        oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                        oSpecimenMaster.Dispose()
                        oSpecimenMaster = Nothing
                    Case MasterCriteria_CollectionContainer
                        ''\\ Comented For Lab-DeNormalization
                        ''Dim oCollectionContainer As New frmLab_CollectionMaster
                        ''oCollectionContainer.nEditID = 0
                        ''oCollectionContainer.sEditCode = ""
                        ''oCollectionContainer.sEditName = ""
                        ''oCollectionContainer.blnIsModify = True
                        ''oCollectionContainer.ShowDialog(Me)
                        ''oCollectionContainer = Nothing
                        Dim oSpecimenMaster As New frmLab_SpecimenMaster
                        oSpecimenMaster.nEditID = 0
                        oSpecimenMaster.sEditCode = ""
                        oSpecimenMaster.sEditName = ""
                        oSpecimenMaster.blnIsModify = True
                        oSpecimenMaster.enumtype = 2 'for Collection type
                        oSpecimenMaster.Text = "Collection Container Master"
                        oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                        oSpecimenMaster.Dispose()
                        oSpecimenMaster = Nothing
                    Case MasterCriteria_StorageTemperature
                        ''Dim oStorageTempMaster As New frmLab_StorageTemperature
                        ''oStorageTempMaster.nEditID = 0
                        ''oStorageTempMaster.sEditCode = ""
                        ''oStorageTempMaster.sEditName = ""
                        ''oStorageTempMaster.blnIsModify = True
                        ''oStorageTempMaster.ShowDialog(Me)
                        ''oStorageTempMaster = Nothing
                        Dim oSpecimenMaster As New frmLab_SpecimenMaster
                        oSpecimenMaster.nEditID = 0
                        oSpecimenMaster.sEditCode = ""
                        oSpecimenMaster.sEditName = ""
                        oSpecimenMaster.blnIsModify = True
                        oSpecimenMaster.enumtype = gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.StorageTemperature   'for Storage Temprature  type
                        oSpecimenMaster.Text = "Storage Temperature Master"
                        oSpecimenMaster.lblSpecimen.Text = "Temperature :"

                        oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                        oSpecimenMaster.Dispose()
                        oSpecimenMaster = Nothing
                    Case MasterCriteria_PreferredLab, MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                        Dim oContactInformation As New frmLab_ContactInformation
                        oContactInformation.nEditID = 0
                        oContactInformation.sEditContactName = ""
                        oContactInformation.sEditFirstName = ""
                        oContactInformation.sEditMiddleName = ""
                        oContactInformation.sEditLastName = ""
                        oContactInformation.blnIsModify = True
                        If cmbCriteria.SelectedItem = MasterCriteria_PreferredLab Then
                            oContactInformation.blnContactType = LabActor.enumContactType.PreferredLab
                        ElseIf cmbCriteria.SelectedItem = MasterCriteria_ReferredBY Then
                            oContactInformation.blnContactType = LabActor.enumContactType.ReferredBy
                        ElseIf cmbCriteria.SelectedItem = MasterCriteria_SampledBy Then
                            oContactInformation.blnContactType = LabActor.enumContactType.SampledBy
                        End If
                        oContactInformation.ShowDialog(IIf(IsNothing(oContactInformation.Parent), Me, oContactInformation.Parent))
                        oContactInformation.Dispose()
                        oContactInformation = Nothing
                    Case MasterCriteria_LoincMst
                        Dim oLoincMst As New frmLab_LOINCOrderCodeMst
                        oLoincMst.nEditID = 0
                        oLoincMst.sEditCode = ""
                        oLoincMst.sEditName = ""
                        oLoincMst.blnIsModify = True
                        oLoincMst.Text = "LOINC Order Code Master"
                        oLoincMst.ShowDialog(IIf(IsNothing(oLoincMst.Parent), Me, oLoincMst.Parent))
                        oLoincMst.Dispose()
                        oLoincMst = Nothing

                    Case MasterCriteria_InternalNotes
                        Dim oLabNotesMst As New frmLab_Notes
                        oLabNotesMst.AckNoteType = "InternalNotes"
                        oLabNotesMst.ShowDialog(IIf(IsNothing(oLabNotesMst.Parent), Me, oLabNotesMst.Parent))
                        oLabNotesMst.Dispose()
                        oLabNotesMst = Nothing

                    Case MasterCriteria_PatientNotes
                        Dim oLabNotesMst As New frmLab_Notes
                        oLabNotesMst.AckNoteType = "PatientNotes"
                        oLabNotesMst.ShowDialog(IIf(IsNothing(oLabNotesMst.Parent), Me, oLabNotesMst.Parent))
                        oLabNotesMst.Dispose()
                        oLabNotesMst = Nothing
                End Select

                RefreshCategory()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


        End Try
    End Sub
    Private Sub UpdateCategory()

        Dim _ID As Long = 0
        Dim _Code As String = ""
        Dim _Name As String = ""
        'Dim enumtype As Int16
        'sarika 28th may 07
        Dim _ContactName As String = ""
        Dim _FirstName As String = ""
        Dim _MiddleName As String = ""
        Dim _LastName As String = ""


        Try


            If Not cmbCriteria.SelectedItem Is Nothing Then
                If c1TestLibrary.Rows.Count > 1 Then
                    If c1TestLibrary.Row > 0 Then
                        _ID = c1TestLibrary.GetData(c1TestLibrary.Row, COL_ID)
                        If c1TestLibrary.Cols.Count = COL_COUNT Then
                            _Code = c1TestLibrary.GetData(c1TestLibrary.Row, COL_CODE) & ""
                            _Name = c1TestLibrary.GetData(c1TestLibrary.Row, COL_NAME) & ""
                        End If
                        If _ID > 0 Then
                            Select Case cmbCriteria.SelectedItem
                                Case MasterCriteria_Test
                                    Dim oTestMaster As New frmLab_TestMaster
                                    oTestMaster.nEditID = _ID
                                    oTestMaster.sEditCode = _Code
                                    oTestMaster.sEditName = _Name
                                    oTestMaster.blnIsModify = False
                                    TestID = _ID
                                    oTestMaster.ShowDialog(IIf(IsNothing(oTestMaster.Parent), Me, oTestMaster.Parent))
                                    oTestMaster.Dispose()
                                    oTestMaster = Nothing
                                Case MasterCriteria_Group
                                    Dim oGroupMaster As New frmLab_GroupMaster
                                    oGroupMaster.nEditID = _ID
                                    oGroupMaster.sEditCode = _Code
                                    oGroupMaster.sEditName = _Name
                                    oGroupMaster.blnIsModify = False
                                    oGroupMaster.ShowDialog(IIf(IsNothing(oGroupMaster.Parent), Me, oGroupMaster.Parent))
                                    oGroupMaster.Dispose()
                                    oGroupMaster = Nothing
                                Case MasterCriteria_Specimen
                                    Dim oSpecimenMaster As New frmLab_SpecimenMaster
                                    oSpecimenMaster.nEditID = _ID
                                    oSpecimenMaster.sEditCode = _Code
                                    oSpecimenMaster.sEditName = _Name
                                    oSpecimenMaster.enumtype = gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Specimen    ' for Specimen
                                    oSpecimenMaster.blnIsModify = False
                                    oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                                    oSpecimenMaster.Dispose()
                                    oSpecimenMaster = Nothing

                                Case MasterCriteria_CollectionContainer
                                    Dim oSpecimenMaster As New frmLab_SpecimenMaster
                                    oSpecimenMaster.nEditID = _ID
                                    oSpecimenMaster.sEditCode = _Code
                                    oSpecimenMaster.sEditName = _Name
                                    oSpecimenMaster.enumtype = 2  ' for CollectionContainer
                                    oSpecimenMaster.blnIsModify = False
                                    oSpecimenMaster.Text = "Collection Container Master"
                                    oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                                    oSpecimenMaster.Dispose()
                                    oSpecimenMaster = Nothing

                                Case MasterCriteria_StorageTemperature
                                    Dim oSpecimenMaster As New frmLab_SpecimenMaster
                                    oSpecimenMaster.nEditID = _ID
                                    oSpecimenMaster.sEditCode = _Code
                                    oSpecimenMaster.sEditName = _Name
                                    oSpecimenMaster.enumtype = 3  ' for StorageTemperature
                                    oSpecimenMaster.blnIsModify = False
                                    oSpecimenMaster.Text = "Storage Temperature Master"
                                    oSpecimenMaster.lblSpecimen.Text = "Temperature :"
                                    oSpecimenMaster.ShowDialog(IIf(IsNothing(oSpecimenMaster.Parent), Me, oSpecimenMaster.Parent))
                                    oSpecimenMaster.Dispose()
                                    oSpecimenMaster = Nothing
                                Case MasterCriteria_PreferredLab, MasterCriteria_ReferredBY, MasterCriteria_SampledBy
                                    Dim oContactInfo As New frmLab_ContactInformation
                                    oContactInfo.nEditID = _ID
                                    oContactInfo.blnIsModify = False
                                    If cmbCriteria.SelectedItem = MasterCriteria_PreferredLab Then
                                        oContactInfo.blnContactType = LabActor.enumContactType.PreferredLab
                                        oContactInfo.sEditContactName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""
                                        'sarika 28th may 07
                                        _ContactName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""

                                        _FirstName = ""
                                        _MiddleName = ""
                                        _LastName = ""
                                        '-------------
                                        oContactInfo.sEditFirstName = ""
                                        oContactInfo.sEditMiddleName = ""
                                        oContactInfo.sEditLastName = ""
                                    ElseIf cmbCriteria.SelectedItem = MasterCriteria_ReferredBY Then
                                        oContactInfo.blnContactType = LabActor.enumContactType.ReferredBy
                                        oContactInfo.sEditContactName = ""
                                        oContactInfo.sEditFirstName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""
                                        oContactInfo.sEditMiddleName = c1TestLibrary.GetData(c1TestLibrary.Row, 2) & ""
                                        oContactInfo.sEditLastName = c1TestLibrary.GetData(c1TestLibrary.Row, 3) & ""

                                        'sarika 28th may 07
                                        _ContactName = ""

                                        _FirstName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""
                                        _MiddleName = c1TestLibrary.GetData(c1TestLibrary.Row, 2) & ""
                                        _LastName = c1TestLibrary.GetData(c1TestLibrary.Row, 3) & ""
                                        '-------------
                                    ElseIf cmbCriteria.SelectedItem = MasterCriteria_SampledBy Then
                                        oContactInfo.blnContactType = LabActor.enumContactType.SampledBy
                                        oContactInfo.sEditContactName = ""
                                        oContactInfo.sEditFirstName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""
                                        oContactInfo.sEditMiddleName = c1TestLibrary.GetData(c1TestLibrary.Row, 2) & ""
                                        oContactInfo.sEditLastName = c1TestLibrary.GetData(c1TestLibrary.Row, 3) & ""

                                        'sarika 28th may 07
                                        _ContactName = ""

                                        _FirstName = c1TestLibrary.GetData(c1TestLibrary.Row, 1) & ""
                                        _MiddleName = c1TestLibrary.GetData(c1TestLibrary.Row, 2) & ""
                                        _LastName = c1TestLibrary.GetData(c1TestLibrary.Row, 3) & ""
                                        '-------------
                                    End If
                                    oContactInfo.ShowDialog(IIf(IsNothing(oContactInfo.Parent), Me, oContactInfo.Parent))
                                    oContactInfo.Dispose()
                                    oContactInfo = Nothing
                                Case MasterCriteria_LoincMst

                                    Dim oLoincMst As New frmLab_LOINCOrderCodeMst
                                    oLoincMst.nEditID = _ID
                                    oLoincMst.sEditCode = _Code
                                    oLoincMst.sEditName = _Name

                                    oLoincMst.blnIsModify = False
                                    oLoincMst.Text = "LOINC Order Code Master"
                                    oLoincMst.ShowDialog(IIf(IsNothing(oLoincMst.Parent), Me, oLoincMst.Parent))
                                    oLoincMst.Dispose()
                                    oLoincMst = Nothing

                                Case MasterCriteria_InternalNotes
                                    Dim oLabNotesMst As New frmLab_Notes
                                    oLabNotesMst.AckNoteType = "InternalNotes"
                                    oLabNotesMst._blnModify = True
                                    oLabNotesMst._labAckNotesID = c1TestLibrary.GetData(c1TestLibrary.Row, 0)
                                    oLabNotesMst._Notes = c1TestLibrary.GetData(c1TestLibrary.Row, 1)
                                    oLabNotesMst.ShowDialog(IIf(IsNothing(oLabNotesMst.Parent), Me, oLabNotesMst.Parent))
                                    oLabNotesMst.Dispose()
                                    oLabNotesMst = Nothing

                                Case MasterCriteria_PatientNotes
                                    Dim oLabNotesMst As New frmLab_Notes
                                    oLabNotesMst.AckNoteType = "PatientNotes"
                                    oLabNotesMst._blnModify = True
                                    oLabNotesMst._labAckNotesID = c1TestLibrary.GetData(c1TestLibrary.Row, 0)
                                    oLabNotesMst._Notes = c1TestLibrary.GetData(c1TestLibrary.Row, 1)
                                    oLabNotesMst.ShowDialog(IIf(IsNothing(oLabNotesMst.Parent), Me, oLabNotesMst.Parent))
                                    oLabNotesMst.Dispose()
                                    oLabNotesMst = Nothing
                            End Select

                            RefreshCategory()

                        End If
                    Else
                        MessageBox.Show("Select the record to be modified", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("There are no records to modify", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


        End Try

    End Sub
    Private Sub DeleteCategory()
        Try


            If Not cmbCriteria.SelectedItem Is Nothing Then
                Dim _ID As Long = 0
                If c1TestLibrary.Rows.Count > 1 Then
                    If c1TestLibrary.Row > 0 Then
                        _ID = c1TestLibrary.GetData(c1TestLibrary.Row, COL_ID)
                        If _ID > 0 Then
                            If MessageBox.Show("Are you sure, you want to delete the record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                                Select Case cmbCriteria.SelectedItem
                                    Case MasterCriteria_Test

                                        Dim _gloEMRLabTest As New gloEMRLabTest

                                        '29-Jan-13 Aniket: Resolving Bug #62647
                                        If _gloEMRLabTest.IsTestUsed(_ID) = False Then
                                            _gloEMRLabTest.Delete(_ID)

                                        Else
                                            MessageBox.Show("This Test cannot be deleted as it is used further.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        End If
                                        _gloEMRLabTest.Dispose()
                                        _gloEMRLabTest = Nothing

                                    Case MasterCriteria_Group
                                        Dim _gloEMRLabGroup As New gloEMRLabGroup
                                        _gloEMRLabGroup.Delete(_ID)
                                        _gloEMRLabGroup.Dispose()
                                        _gloEMRLabGroup = Nothing
                                    Case MasterCriteria_Specimen
                                        Dim ogloEMRLabCSST As New gloEMRLabCSST  'Dim _gloEMRLabSpecimen As New gloEMRLabSpecimen
                                        ogloEMRLabCSST.Delete(_ID, gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Specimen)
                                        ogloEMRLabCSST.Dispose()
                                        ogloEMRLabCSST = Nothing
                                    Case MasterCriteria_CollectionContainer
                                        ''Dim _gloEMRLabCollection As New gloEMRLabCollectionContainer
                                        ''_gloEMRLabCollection.Delete(_ID)
                                        ''_gloEMRLabCollection = Nothing
                                        Dim ogloEMRLabCSST As New gloEMRLabCSST
                                        ogloEMRLabCSST.Delete(_ID, gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.Collection)
                                        ogloEMRLabCSST.Dispose()
                                        ogloEMRLabCSST = Nothing
                                    Case MasterCriteria_StorageTemperature
                                        'Dim _gloEMRLabStorageTemp As New gloEMRLabStorageTemperature
                                        '_gloEMRLabStorageTemp.Delete(_ID)
                                        '_gloEMRLabStorageTemp = Nothing
                                        Dim ogloEMRLabCSST As New gloEMRLabCSST  'Dim _gloEMRLabSpecimen As New gloEMRLabSpecimen
                                        ogloEMRLabCSST.Delete(_ID, gloEMRGeneralLibrary.gloEMRActors.LabActor.enumLabCCSTType.StorageTemperature)
                                        ogloEMRLabCSST.Dispose()
                                        ogloEMRLabCSST = Nothing
                                    Case MasterCriteria_PreferredLab
                                        Dim _gloEMRLabContactInfo As New gloEMRLabContactInfo
                                        _gloEMRLabContactInfo.Delete(_ID, LabActor.enumContactType.PreferredLab)
                                        _gloEMRLabContactInfo.Dispose()
                                        _gloEMRLabContactInfo = Nothing
                                    Case MasterCriteria_ReferredBY
                                        Dim _gloEMRLabContactInfo As New gloEMRLabContactInfo
                                        _gloEMRLabContactInfo.Delete(_ID, LabActor.enumContactType.ReferredBy)
                                        _gloEMRLabContactInfo.Dispose()
                                        _gloEMRLabContactInfo = Nothing
                                    Case MasterCriteria_SampledBy
                                        Dim _gloEMRLabContactInfo As New gloEMRLabContactInfo
                                        _gloEMRLabContactInfo.Delete(_ID, LabActor.enumContactType.SampledBy)
                                        _gloEMRLabContactInfo.Dispose()
                                        _gloEMRLabContactInfo = Nothing
                                    Case MasterCriteria_LoincMst
                                        Dim ogloEMRLabLoincMst As New gloEMRLabLoincMst
                                        ogloEMRLabLoincMst.DeleteLOINCCode(_ID)
                                        ogloEMRLabLoincMst.Dispose()
                                        ogloEMRLabLoincMst = Nothing
                                    Case MasterCriteria_InternalNotes, MasterCriteria_PatientNotes
                                        Dim oLabAckNotes As New LabAckNotes
                                        oLabAckNotes.Delete(_ID)
                                        oLabAckNotes.Dispose()
                                        oLabAckNotes = Nothing
                                End Select
                                RefreshCategory()
                            End If
                        Else
                            MessageBox.Show("There are no records to delete", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                        End If
                    Else
                        MessageBox.Show("Select the row to be deleted", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                    End If
                Else
                    MessageBox.Show("There are no records to delete", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


        End Try


    End Sub

    Private Sub RefreshCategory()

        Try


            txtListSearch.Focus()

            If Not cmbCriteria.SelectedItem Is Nothing Then
                Fill_List(cmbCriteria.SelectedItem)
            Else
                Fill_List("")
            End If



            '25-Sep-14 Aniket: Do not reset filter on saving a test
            'txtListSearch.Text = ""

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub
    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try


    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "New"
                Call AddCategory()
            Case "Modify"
                Call UpdateCategory()
            Case "Delete"
                Call DeleteCategory()

            Case "Refresh"

                '07-Oct-14 Aniket Resolving Bug #74749: gloEMR:Orders & Results Setup-Highlight first record after refresh
                blnRefreshClicked = True
                Call RefreshCategory()
                '07-Oct-14 Aniket Resolving Bug #74749: gloEMR:Orders & Results Setup-Highlight first record after refresh
                blnRefreshClicked = False

            Case "Close"
                Call FormClose()
            Case "Normal Note"
                Dim omakeNormalnote As New frmLabMakeNormalLotes
                Try
                    omakeNormalnote.ShowDialog(IIf(IsNothing(omakeNormalnote.Parent), Me, omakeNormalnote.Parent))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    omakeNormalnote.Dispose()
                    omakeNormalnote = Nothing
                End Try
        End Select
    End Sub

    Private Sub c1TestLibrary_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1TestLibrary.DoubleClick
        UpdateCategory()
    End Sub


    Private Sub c1TestLibrary_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1TestLibrary.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use to clear search text box
        txtListSearch.ResetText()
        txtListSearch.Focus()
    End Sub
    'Shubhangi 20091203
    'Add this event & call the cmbCriteria.SelectedIndex Event here Coz for the solution of grid resizing.
    Private Sub frmLab_View_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If (cmbCriteria.Items.Count > 0) Then
            cmbCriteria.SelectedIndex = 0
        End If


    End Sub

    ''added on 20100105
    Private Sub txtListSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtListSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1TestLibrary.Rows.Count >= 0 Then
                    c1TestLibrary.Select(0, 0)
                    'c1TestLibrary.CurrentRowIndex = 0
                End If
            End If

            ''commented to allow '-','%'
            'mdlGeneral.ValidateText(txtSearch.Text, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbstruct_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbstruct.SelectedIndexChanged
        FilterTestData()

    End Sub

    Private Sub chkoutboundtran_CheckedChanged(sender As Object, e As System.EventArgs)
        FilterTestData()

    End Sub

    Private Sub cmbordtype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbordtype.SelectedIndexChanged
        FilterTestData()

    End Sub

    Private Sub chkenbdate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkenbdate.CheckedChanged
        If (chkenbdate.Checked = True) Then
            dtfromdate.Enabled = True
            dttodate.Enabled = True
        Else
            dtfromdate.Enabled = False
            dttodate.Enabled = False

        End If
        FilterTestData()
    End Sub

    Private Sub dttodate_ValueChanged(sender As Object, e As System.EventArgs) Handles dttodate.ValueChanged
        dttodate.MinDate = dtfromdate.Value
        FilterTestData()
    End Sub

    Private Sub dtfromdate_ValueChanged(sender As Object, e As System.EventArgs) Handles dtfromdate.ValueChanged
        dttodate.MinDate = dtfromdate.Value
        FilterTestData()
    End Sub
    Private Sub FilterTestData()
        Try
            Dim blnrowselected As Boolean = False

            With c1TestLibrary
                '.Clear()
                SetGridStyle()
                c1TestLibrary.Visible = False
                c1TestLibrary.DataSource = Nothing
                .Cols.Count = 6
                COL_COUNT = 6
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Clear(C1.Win.C1FlexGrid.ClearFlags.All)
                .Cols(COL_ID).Width = 0
                .Cols(COL_NAME).Width = 100
                .Cols(COL_OrderType).Width = 80

                .Cols(COL_StructuredLabResults).Width = 120
                .SetData(0, COL_ID, "ID")
                .SetData(0, COL_CODE, "Code")
                .SetData(0, COL_NAME, "Name")
                .SetData(0, COL_OrderType, "Order Type")
                .SetData(0, COL_StructuredLabResults, "Structured Lab Results")
                .SetData(0, COL_OBTranofCare, "Outbound Transition of Care")
                Dim strordertype = cmbordtype.Text.Trim()
                'If (cmbordtype.Text = "Lab") Then  ''code commented 8020 order PRD changes
                '    strordertype = ""
                'End If
                Dim strstructtype = cmbstruct.Text.Trim()
                'If (cmbstruct.Text = "No") Then   ''code commented 8020 order PRD changes
                '    strstructtype = ""
                'End If
                Dim blnobtransition As Boolean = False
                If (cmbtransition.Text = "Yes") Then
                    blnobtransition = True
                Else
                    blnobtransition = False
                End If

                If chkenbdate.Checked = False Then
                    For Each testResult As LabActor.Test In oTests
                        ''If testResult.MUReportingCategory = strCategory Or strCategory = "All" Then
                        ''condition added for order PRd 8020 
                        ''if MUReportingCategory is blank the show it is lab change for Order PRD
                        If ((testResult.IsStructuredLabTest = strstructtype Or cmbstruct.Text.Trim() = "All") Or (testResult.IsStructuredLabTest = "" And cmbstruct.Text.Trim() = "No")) And (testResult.MUReportingCategory = strordertype Or cmbordtype.Text.Trim() = "All" Or (testResult.MUReportingCategory = "" And cmbordtype.Text = "Lab")) And (testResult.bOutboundTransistion = blnobtransition Or cmbtransition.Text.Trim() = "All") Then
                            .Rows.Add()
                            .SetData(.Rows.Count - 1, COL_ID, testResult.TestID)
                            .SetData(.Rows.Count - 1, COL_CODE, testResult.Code)
                            .SetData(.Rows.Count - 1, COL_NAME, testResult.Name)
                            .SetData(.Rows.Count - 1, COL_OrderType, testResult.MUReportingCategory)
                            .SetData(.Rows.Count - 1, COL_StructuredLabResults, testResult.IsStructuredLabTest)
                            .SetData(.Rows.Count - 1, COL_OBTranofCare, testResult.bOutboundTransistion.ToString().Replace("True", "Yes").Replace("False", "No"))
                            If (TestID = testResult.TestID) Then
                                c1TestLibrary.Select(.Rows.Count - 1, 0)
                                blnrowselected = True
                            End If
                        End If
                    Next
                Else
                    For Each testResult As LabActor.Test In oTests
                        ''If testResult.MUReportingCategory = strCategory Or strCategory = "All" Then
                        ''condition added for order PRd 8020 
                        ''if MUReportingCategory is blank the show it is lab change for Order PRD
                        If (testResult.IsStructuredLabTest = strstructtype Or cmbstruct.Text.Trim() = "All") And (testResult.MUReportingCategory = strordertype Or cmbordtype.Text.Trim() = "All" Or (testResult.MUReportingCategory = "" And cmbordtype.Text = "Lab")) And (testResult.bOutboundTransistion = blnobtransition Or cmbtransition.Text.Trim() = "All") And (testResult.dtUpdatedDate >= dtfromdate.Value.ToShortDateString() And testResult.dtUpdatedDate <= dttodate.Value.ToShortDateString()) Then
                            .Rows.Add()
                            .SetData(.Rows.Count - 1, COL_ID, testResult.TestID)
                            .SetData(.Rows.Count - 1, COL_CODE, testResult.Code)
                            .SetData(.Rows.Count - 1, COL_NAME, testResult.Name)
                            .SetData(.Rows.Count - 1, COL_OrderType, testResult.MUReportingCategory)
                            .SetData(.Rows.Count - 1, COL_StructuredLabResults, testResult.IsStructuredLabTest)
                            .SetData(.Rows.Count - 1, COL_OBTranofCare, testResult.bOutboundTransistion.ToString().Replace("True", "Yes").Replace("False", "No"))
                            If (TestID = testResult.TestID) Then
                                c1TestLibrary.Select(.Rows.Count - 1, 0)
                                blnrowselected = True
                            End If
                        End If
                    Next

                End If
                If blnrowselected = False And TestID <> 0 Then
                    If c1TestLibrary.Rows.Count >= 2 Then
                        c1TestLibrary.Select(1, 0)  ''changes made for bugid 68337
                    End If
                End If
                Dim _Width As Single = (c1TestLibrary.Width)
                .Cols(COL_ID).Width = 0
                .Cols(COL_CODE).Width = _Width * 0.25
                .Cols(COL_NAME).Width = _Width * 0.35
                .Cols(COL_OrderType).Width = _Width * 0.1
                .Cols(COL_StructuredLabResults).Width = _Width * 0.12  ''0.2
                .Cols(COL_OBTranofCare).Width = _Width * 0.16

            End With
            '  c1TestLibrary.ColSel = 1
            If (txtListSearch.Text.Trim() <> "") Then
                Dim evt As EventArgs = Nothing
                txtListSearch_TextChanged(txtListSearch, evt)
            End If

        Catch ex As Exception
            ex = Nothing
        Finally
            c1TestLibrary.Visible = True
        End Try
    End Sub

    Private Sub cmbtransition_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbtransition.SelectedIndexChanged
        FilterTestData()
    End Sub

   

    
End Class