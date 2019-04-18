Public Class frmDisclosureSet

    Dim oDisclosure As clsDisclosureMgmt
    Dim dt As DataTable
    Private m_DisclosureID As Int64
    Private m_PatientID As Int64
    Public arrlist As ArrayList
    Public lst As myList
    Private IsFormLoading As Boolean = True
    Dim _arrExamlist As ArrayList


    Private Sub frmDisclosureSet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IsFormLoading = True
        FillDisclosureSet()
        trvAssociation.Nodes.Clear()
        trvAssociation.CheckBoxes = True
        oDisclosure = New clsDisclosureMgmt

        dtpFromDate.Value = Now
        dtpToDate.Value = Now

        Dim nAssociatSetID As Int64 = oDisclosure.GetAssociatedSet(m_DisclosureID, m_PatientID)
        If nAssociatSetID <> 0 Then
            cmbDisclosureSet.SelectedValue = nAssociatSetID
        End If
        FillParentNode()
        FillAssociateSet()
        IsFormLoading = False
        'AddHandler cmbDisclosureSet.SelectedIndexChanged, AddressOf cmbDisclosureSet_SelectedIndexChanged
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub FillDisclosureSet()
        ' dt = New DataTable
        If (IsNothing(oDisclosure) = False) Then
            oDisclosure.Dispose()
            oDisclosure = Nothing

        End If
        oDisclosure = New clsDisclosureMgmt
        dt = oDisclosure.GetDisclosureSet()
        If Not IsNothing(dt) Then
            cmbDisclosureSet.DataSource = dt
            cmbDisclosureSet.DisplayMember = dt.Columns(1).ColumnName
            cmbDisclosureSet.ValueMember = dt.Columns(0).ColumnName
            cmbDisclosureSet.SelectedIndex = 0
        End If
        'FillAssociateSet()
    End Sub
    Public Sub FillAssociateSet()
        dt = New DataTable
        oDisclosure = New clsDisclosureMgmt
        Try
            dt = oDisclosure.GetAssociation(m_DisclosureID, m_PatientID, cmbDisclosureSet.SelectedValue, cmbDisclosureSet.Text)
            If Not IsNothing(dt) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    For Each treenode As myTreeNode In trvAssociation.Nodes

                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.PatientDemographics) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = mychildNode.Key Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If

                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.History) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                For Each ItemNode As myTreeNode In mychildNode.Nodes
                                    If Convert.ToInt64(dt.Rows(i)("nAssociateID")) = Convert.ToInt64(ItemNode.Key) Then
                                        ItemNode.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                            Continue For
                        End If

                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Medication) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToInt64(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.PatientExam) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToInt64(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Labs) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToInt64(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Orders) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToInt64(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.DMS) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToDecimal(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If
                        ''SUDHIR 20090219
                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Consent) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToDecimal(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If

                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Flowsheet) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToDecimal(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If

                        If dt.Rows(i)("sAssociateType") = DisclosureSetNames(DisclosureSet.Messages) Then
                            For Each mychildNode As myTreeNode In treenode.Nodes
                                If dt.Rows(i)("nAssociateID") = Convert.ToDecimal(mychildNode.Key) Then
                                    mychildNode.Checked = True
                                    Exit For
                                End If
                            Next
                            Continue For
                        End If

                        '' END SUDHIR 
                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub cmbDisclosureSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'FillAssoicateData()
    'End Sub
    Private Sub cmbDisclosureSet_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDisclosureSet.SelectionChangeCommitted
        trvAssociation.Nodes.Clear()
        trvAssociation.CheckBoxes = True
        FillParentNode()
        FillAssociateSet()
    End Sub
    Public Sub FillParentNode()
        oDisclosure = New clsDisclosureMgmt
        Dim DisclosureAssociation As String
        DisclosureAssociation = oDisclosure.GetDisclosureAssociation(cmbDisclosureSet.SelectedValue)
        Dim arrAssociation() As String = Split(DisclosureAssociation, ", ")
        Dim _mytreeNode As myTreeNode

        For i As Integer = 0 To arrAssociation.Length - 1
            If arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.PatientDemographics) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.PatientDemographics)
                _mytreeNode.ImageIndex = 0
                _mytreeNode.SelectedImageIndex = 0
                trvAssociation.Nodes.Add(_mytreeNode)
                'FillMedication(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.History) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.History)
                trvAssociation.Nodes.Add(_mytreeNode)
                _mytreeNode.ImageIndex = 1
                _mytreeNode.SelectedImageIndex = 1
                FillHistoy(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Medication) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Medication)
                _mytreeNode.ImageIndex = 2
                _mytreeNode.SelectedImageIndex = 2
                trvAssociation.Nodes.Add(_mytreeNode)
                FillMedication(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.PatientExam) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.PatientExam)
                trvAssociation.Nodes.Add(_mytreeNode)
                _mytreeNode.ImageIndex = 3
                _mytreeNode.SelectedImageIndex = 3
                FillNotes(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Labs) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Labs)
                _mytreeNode.ImageIndex = 4
                _mytreeNode.SelectedImageIndex = 4
                trvAssociation.Nodes.Add(_mytreeNode)
                FillLabs(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Orders) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Orders)
                _mytreeNode.ImageIndex = 5
                _mytreeNode.SelectedImageIndex = 5
                trvAssociation.Nodes.Add(_mytreeNode)
                FillRadiologyOrders(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.DMS) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.DMS)
                _mytreeNode.ImageIndex = 6
                _mytreeNode.SelectedImageIndex = 6
                trvAssociation.Nodes.Add(_mytreeNode)
                FillDMS(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Consent) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Consent)
                _mytreeNode.ImageIndex = 9
                _mytreeNode.SelectedImageIndex = 9
                trvAssociation.Nodes.Add(_mytreeNode)
                FillConcent(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Flowsheet) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Flowsheet)
                _mytreeNode.ImageIndex = 10
                _mytreeNode.SelectedImageIndex = 10
                trvAssociation.Nodes.Add(_mytreeNode)
                FillFlowSheet(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Immunization) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Immunization)
                _mytreeNode.ImageIndex = 11
                _mytreeNode.SelectedImageIndex = 11
                trvAssociation.Nodes.Add(_mytreeNode)
                FillImmunization(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Messages) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Messages)
                _mytreeNode.ImageIndex = 12
                _mytreeNode.SelectedImageIndex = 12
                trvAssociation.Nodes.Add(_mytreeNode)
                FillMessages(_mytreeNode)
                _mytreeNode = Nothing
            ElseIf arrAssociation.GetValue(i) = DisclosureSetNames(DisclosureSet.Vitals) Then
                _mytreeNode = New myTreeNode
                _mytreeNode.Text = DisclosureSetNames(DisclosureSet.Vitals)
                _mytreeNode.ImageIndex = 13
                _mytreeNode.SelectedImageIndex = 13
                trvAssociation.Nodes.Add(_mytreeNode)
                FillVitals(_mytreeNode)
                _mytreeNode = Nothing
            End If
        Next
        ''commented Sandip Darade 20090310
        'trvAssociation.ExpandAll()
    End Sub

    Public Sub FillHistoy(ByVal ItemNode As myTreeNode)
        ' dt = New DataTable
        Dim HistoryNode As myTreeNode
        Dim CategoryNode As myTreeNode

        dt = oDisclosure.GetHistory(m_PatientID)
        Dim IsCategoryPresent As Boolean = False
        For i As Integer = 0 To dt.Rows.Count - 1
            For Each trvNode As myTreeNode In ItemNode.Nodes
                If trvNode.Text = dt.Rows(i)("sHistorycategory") Then
                    IsCategoryPresent = True
                    HistoryNode = New myTreeNode

                    ''Commented Sandip Darade 20090310
                    ''HistoryNode.Text = dt.Rows(i)("sHistoryItem") & " - " & dt.Rows(i)("sComments")

                    ''Sandip Darade 20090310
                    HistoryNode.Text = dt.Rows(i)("sHistoryItem")

                    ''Add comments to node text  if there any 
                    If (dt.Rows(i)("sComments") <> Nothing) Then
                        HistoryNode.Text += " - " & dt.Rows(i)("sComments")
                    End If

                    ''Add Visit date to node text
                    If (dt.Rows(i)("nVisitID") <> Nothing) Then
                        Dim dtVisitdate As DateTime = GetVisitdate(dt.Rows(i)("nVisitID")) ''retrieve visit date passing visitID
                        HistoryNode.Text += " - " & dtVisitdate.ToShortDateString()
                    End If

                    HistoryNode.Key = dt.Rows(i)("nHistoryID")
                    HistoryNode.Tag = ""
                    HistoryNode.ImageIndex = 7
                    HistoryNode.SelectedImageIndex = 7
                    trvNode.Nodes.Add(HistoryNode)
                    Exit For
                Else
                    IsCategoryPresent = False
                End If
            Next
            If IsCategoryPresent = False Then
                CategoryNode = New myTreeNode
                CategoryNode.Text = dt.Rows(i)("sHistorycategory")
                CategoryNode.Key = dt.Rows(i)("nHistoryID")
                CategoryNode.Tag = "C"
                CategoryNode.ImageIndex = 7
                CategoryNode.SelectedImageIndex = 7
                ItemNode.Nodes.Add(CategoryNode)
                HistoryNode = New myTreeNode
                ''Commented Sandip Darade 20090310
                'HistoryNode.Text = dt.Rows(i)("sHistoryItem") & " - " & dt.Rows(i)("sComments")

                ''Sandip Darade 20090310
                HistoryNode.Text = dt.Rows(i)("sHistoryItem")

                ''Add comments to node text  if there any 
                If (dt.Rows(i)("sComments") <> Nothing) Then
                    HistoryNode.Text += " - " & dt.Rows(i)("sComments")
                End If

                ''Add Visit date to node text
                If (dt.Rows(i)("nVisitID") <> Nothing) Then
                    Dim dtVisitdate As DateTime = GetVisitdate(dt.Rows(i)("nVisitID"))
                    HistoryNode.Text += " - " & dtVisitdate.ToShortDateString()
                End If

                HistoryNode.Key = dt.Rows(i)("nHistoryID")
                HistoryNode.Tag = ""
                HistoryNode.ImageIndex = 7
                HistoryNode.SelectedImageIndex = 7
                CategoryNode.Nodes.Add(HistoryNode)
            End If

        Next

    End Sub

    Public Sub FillMedication(ByVal MediNode As myTreeNode)
        ' dt = New DataTable
        dt = oDisclosure.GetMedicationforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        Dim MedicationNode As myTreeNode
        If Not IsNothing(dt) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                MedicationNode = New myTreeNode
                MedicationNode.Text = dt.Rows(i)("sMedication") & " - " & Format(dt.Rows(i)("dtMedicationDate"), "MM/dd/yyyy")
                MedicationNode.Key = dt.Rows(i)("nMedicationID")
                MedicationNode.ImageIndex = 7
                MedicationNode.SelectedImageIndex = 7
                MediNode.Nodes.Add(MedicationNode)
                MedicationNode = Nothing
            Next
        End If
    End Sub

    Public Sub FillLabs(ByVal LabNode As myTreeNode)


        dt = oDisclosure.GetLabOrderforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)

        Dim OrderNode As myTreeNode = Nothing
        Dim ResultNode As myTreeNode = Nothing
        For i As Integer = 0 To dt.Rows.Count - 1
            ''labom_OrderNoPrefix
            ''labom_OrderNoID
            'OrderNode = New myTreeNode
            If i = 0 Then
                'strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                OrderNode = New myTreeNode
                OrderNode.Text = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") & " - " & dt.Rows(i)("labom_TransactionDate") '& " - " & strProviderName        ''dt.Rows(i)("labtm_Name")
                OrderNode.Key = dt.Rows(i)("labom_OrderID")
                OrderNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                OrderNode.ImageIndex = 8
                OrderNode.SelectedImageIndex = 8
                LabNode.Nodes.Add(OrderNode)
                ResultNode = New myTreeNode
                ResultNode.Text = dt.Rows(i)("labtm_Name")
                ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                ResultNode.Key = 0
                ResultNode.ImageIndex = 7
                ResultNode.SelectedImageIndex = 7
                OrderNode.Nodes.Add(ResultNode)
            Else

                If ResultNode.Parent.Tag <> dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") Then
                    'strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")  ''dt.Rows(i)("labtm_Name")
                    OrderNode = New myTreeNode
                    OrderNode.Text = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") & " - " & dt.Rows(i)("labom_TransactionDate") '& " - " & strProviderName        ''dt.Rows(i)("labtm_Name")
                    OrderNode.Key = dt.Rows(i)("labom_OrderID")
                    OrderNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                    OrderNode.ImageIndex = 8
                    OrderNode.SelectedImageIndex = 8
                    LabNode.Nodes.Add(OrderNode)
                    ResultNode = New myTreeNode
                    ResultNode.Text = dt.Rows(i)("labtm_Name")
                    ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                    ResultNode.Key = 0
                    ResultNode.ImageIndex = 7
                    ResultNode.SelectedImageIndex = 7
                    OrderNode.Nodes.Add(ResultNode)
                Else
                    ResultNode = New myTreeNode
                    ResultNode.Text = dt.Rows(i)("labtm_Name")
                    ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                    ResultNode.Key = 0
                    ResultNode.ImageIndex = 7
                    ResultNode.SelectedImageIndex = 7
                    OrderNode.Nodes.Add(ResultNode)
                End If
            End If
        Next
    End Sub

    Public Sub FillRadiologyOrders(ByVal RdoNode As myTreeNode)

        dt = oDisclosure.GetRadiologyOrderforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)

        Dim radioNode As myTreeNode

        If Not IsNothing(dt) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ''lm_IsFinished()
                Dim IsFinish As String = ""
                Dim strProviderName As String = ""
                If dt.Rows(i)("lm_IsFinished") = True Then
                    IsFinish = "Yes"
                Else
                    IsFinish = "No"
                End If
                strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                radioNode = New myTreeNode
                radioNode.Text = dt.Rows(i)("lm_test_Name") & " - " & dt.Rows(i)("lm_OrderDate") & " - " & IsFinish & " - " & strProviderName
                radioNode.Key = dt.Rows(i)("lm_Order_ID")
                radioNode.ImageIndex = 7
                radioNode.SelectedImageIndex = 7
                RdoNode.Nodes.Add(radioNode)
            Next
        End If
    End Sub

    Public Sub FillDMS(ByVal LabNode As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetScanDocumentforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        If Not IsNothing(dt) Then

            Dim ScanDocNode As myTreeNode
            ''rootNode.Nodes.Add(-1, "Scan Document")

            For i As Integer = 0 To dt.Rows.Count - 1
                ScanDocNode = New myTreeNode
                ScanDocNode.Text = dt.Rows(i)("DocumentName") & " - " & Format(dt.Rows(i)("ModifiedDateTime"), "MM/dd/yyyy")
                ScanDocNode.Key = dt.Rows(i)("eDocumentID")
                ScanDocNode.ImageIndex = 7
                ScanDocNode.SelectedImageIndex = 7
                LabNode.Nodes.Add(ScanDocNode)
            Next
        End If
    End Sub

    ''SUDHIR 20090219 ''
    Public Sub FillConcent(ByVal ConcentParent As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetConcentforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        If Not IsNothing(dt) Then

            Dim ConcentNode As myTreeNode
            For i As Integer = 0 To dt.Rows.Count - 1
                ConcentNode = New myTreeNode
                ConcentNode.Text = dt.Rows(i)("sTemplateName") & " - " & Format(dt.Rows(i)("dtConsentdate"), "MM/dd/yyyy")
                ConcentNode.Key = dt.Rows(i)("nConsentId")
                ConcentNode.ImageIndex = 7
                ConcentNode.SelectedImageIndex = 7
                ConcentParent.Nodes.Add(ConcentNode)
            Next
        End If
    End Sub

    Public Sub FillFlowSheet(ByVal FlowSheetParent As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetFlowSheetforPatient(m_PatientID)
        If Not IsNothing(dt) Then

            Dim FlowSheetNode As myTreeNode
            For i As Integer = 0 To dt.Rows.Count - 1
                FlowSheetNode = New myTreeNode
                FlowSheetNode.Text = dt.Rows(i)("sFlowSheetName").ToString
                FlowSheetNode.Key = dt.Rows(i)("nFlowSheetRecordID")
                FlowSheetNode.ImageIndex = 7
                FlowSheetNode.SelectedImageIndex = 7
                FlowSheetParent.Nodes.Add(FlowSheetNode)
            Next
        End If
    End Sub

    Public Sub FillImmunization(ByVal ImmunizationParent As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetImmunizationforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        If Not IsNothing(dt) Then

            Dim ImmunizationNode As myTreeNode
            For i As Integer = 0 To dt.Rows.Count - 1
                ImmunizationNode = New myTreeNode
                If Not IsDBNull(dt.Rows(i)("im_trn_Date")) Then
                    ImmunizationNode.Text = dt.Rows(i)("im_item_name") & " - " & Format(dt.Rows(i)("im_trn_Date"), "MM/dd/yyyy")
                Else
                    ImmunizationNode.Text = dt.Rows(i)("im_item_name")
                End If
                ImmunizationNode.Key = dt.Rows(i)("im_trn_mst_Id")
                ImmunizationNode.ImageIndex = 7
                ImmunizationNode.SelectedImageIndex = 7
                ImmunizationParent.Nodes.Add(ImmunizationNode)
            Next
        End If
    End Sub

    Public Sub FillMessages(ByVal MessagesParent As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetMessagesforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        If Not IsNothing(dt) Then

            Dim ImmunizationNode As myTreeNode
            For i As Integer = 0 To dt.Rows.Count - 1
                ImmunizationNode = New myTreeNode
                ImmunizationNode.Text = dt.Rows(i)("sTemplateName") & " - " & Format(dt.Rows(i)("dtMsgDate"), "MM/dd/yyyy")
                ImmunizationNode.Key = dt.Rows(i)("nMessageID")
                ImmunizationNode.ImageIndex = 7
                ImmunizationNode.SelectedImageIndex = 7
                MessagesParent.Nodes.Add(ImmunizationNode)
            Next
        End If
    End Sub

    Public Sub FillVitals(ByVal VitalParent As myTreeNode)
        'Dim m_PatientID As Int64
        dt = oDisclosure.GetVitalsforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then


                Dim VitalNode As myTreeNode
                ''HEIGHT
                VitalNode = New myTreeNode
                If CType(dt.Rows(0)("dHeightinCm"), Double) <> 0 Then
                    VitalNode.Text = "Height - " & dt.Rows(0)("dHeightinCm").ToString & " cm" & " - " & dt.Rows(0)("dtVitalDate")
                Else
                    VitalNode.Text = "Height"
                End If
                VitalNode.Tag = "Height"
                VitalNode.Key = CType(dt.Rows(0)("nVitalID"), Int64)
                VitalNode.ImageIndex = 7
                VitalNode.SelectedImageIndex = 7
                VitalParent.Nodes.Add(VitalNode)

                ''WEIGHT
                VitalNode = New myTreeNode
                If CType(dt.Rows(0)("dWeightinlbs"), Double) <> 0 Then
                    VitalNode.Text = "Weight - " & dt.Rows(0)("dWeightinlbs").ToString & " lbs" & " - " & dt.Rows(0)("dtVitalDate")
                Else
                    VitalNode.Text = "Weight"
                End If
                VitalNode.Tag = "Weight"
                VitalNode.Key = CType(dt.Rows(0)("nVitalID"), Int64)
                VitalNode.ImageIndex = 7
                VitalNode.SelectedImageIndex = 7
                VitalParent.Nodes.Add(VitalNode)

                ''TEMPERATURE IN F
                VitalNode = New myTreeNode
                If CType(dt.Rows(0)("dTemperature"), Double) <> 0 Then
                    VitalNode.Text = "Temperature - " & dt.Rows(0)("dTemperature").ToString & " F" & " - " & dt.Rows(0)("dtVitalDate")
                Else
                    VitalNode.Text = "Temperature"
                End If
                VitalNode.Tag = "Temperature"
                VitalNode.Key = CType(dt.Rows(0)("nVitalID"), Int64)
                VitalNode.ImageIndex = 7
                VitalNode.SelectedImageIndex = 7
                VitalParent.Nodes.Add(VitalNode)

                ''BP SITTING MAX/MIN
                VitalNode = New myTreeNode
                If CType(dt.Rows(0)("dBloodPressureSittingMin"), Double) <> 0 And CType(dt.Rows(0)("dBloodPressureSittingMax"), Double) <> 0 Then
                    VitalNode.Text = "BP Sitting Min/Max - " & dt.Rows(0)("dBloodPressureSittingMin").ToString & " / " & dt.Rows(0)("dBloodPressureSittingMax").ToString & " - " & dt.Rows(0)("dtVitalDate")
                Else
                    VitalNode.Text = "BP Sitting Min/Max"
                End If
                VitalNode.Tag = "BP Sitting Min/Max"
                VitalNode.Key = CType(dt.Rows(0)("nVitalID"), Int64)
                VitalNode.ImageIndex = 7
                VitalNode.SelectedImageIndex = 7
                VitalParent.Nodes.Add(VitalNode)

                ''BP STANDING MAX/MIN
                VitalNode = New myTreeNode
                If CType(dt.Rows(0)("dBloodPressureStandingMin"), Double) <> 0 And CType(dt.Rows(0)("dBloodPressureStandingMax"), Double) <> 0 Then
                    VitalNode.Text = "BP Standing Min/Max - " & dt.Rows(0)("dBloodPressureStandingMin").ToString & " / " & dt.Rows(0)("dBloodPressureStandingMax").ToString & " - " & dt.Rows(0)("dtVitalDate")
                Else
                    VitalNode.Text = "BP Standing Min/Max"
                End If
                VitalNode.Tag = "BP Standing Min/Max"
                VitalNode.Key = CType(dt.Rows(0)("nVitalID"), Int64)
                VitalNode.ImageIndex = 7
                VitalNode.SelectedImageIndex = 7
                VitalParent.Nodes.Add(VitalNode)
            End If
        End If
    End Sub

    ''END SUDHIR

    Public Sub FillNotes(ByVal NoteNode As myTreeNode)
        dt = oDisclosure.GetNotesforPatient(m_PatientID, dtpFromDate.Value, dtpToDate.Value)
        Dim NameNode As myTreeNode
        If Not IsNothing(dt) Then

            For i As Integer = 0 To dt.Rows.Count - 1
                NameNode = New myTreeNode
                ''bIsFinished
                Dim IsFinish As String = ""
                Dim strProviderName As String = ""

                If dt.Rows(i)("bIsFinished") = True Then
                    IsFinish = "Yes"
                Else
                    IsFinish = "No"
                End If
                strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                NameNode.Text = dt.Rows(i)("sExamName") & " - " & dt.Rows(i)("dtDOS") & " - " & IsFinish  '& " - " & strProviderName
                NameNode.Key = dt.Rows(i)("nExamID")

                NameNode.ImageIndex = 7
                NameNode.SelectedImageIndex = 7
                NoteNode.Nodes.Add(NameNode)
            Next
        End If

    End Sub

    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub
    Public Sub New(ByVal _DisclosureId As Int64, ByVal _PatientID As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        m_DisclosureID = _DisclosureId
        m_PatientID = _PatientID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub tlsDisclosureSet_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDisclosureSet.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveAssociation()
            Case "Close"
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "Print"
                Print()
        End Select
    End Sub
    Public Sub SaveAssociation(Optional ByVal bPrint As Boolean = False)
        arrlist = New ArrayList
        For Each treenode As myTreeNode In trvAssociation.Nodes
            If treenode.Text = DisclosureSetNames(DisclosureSet.History) Then
                For Each HistoryNode As myTreeNode In treenode.Nodes
                    For Each ItemNode As myTreeNode In HistoryNode.Nodes
                        If ItemNode.Tag = "" And ItemNode.Checked = True Then
                            lst = New myList
                            lst.DisclosureAssociationID = ItemNode.Key
                            lst.DisclosureType = DisclosureSetNames(DisclosureSet.History)
                            arrlist.Add(lst)
                        End If
                    Next

                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.Medication) Then
                For Each MediNode As myTreeNode In treenode.Nodes
                    If MediNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = MediNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Medication)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.PatientExam) Then
                For Each NoteNode As myTreeNode In treenode.Nodes
                    If NoteNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = NoteNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.PatientExam)
                        arrlist.Add(lst)
                    End If
                Next
            End If
            If treenode.Text = DisclosureSetNames(DisclosureSet.Labs) Then
                For Each OrderNode As myTreeNode In treenode.Nodes
                    If OrderNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = OrderNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Labs)
                        arrlist.Add(lst)
                    End If
                Next
            End If
            If treenode.Text = DisclosureSetNames(DisclosureSet.Orders) Then
                For Each RadioNode As myTreeNode In treenode.Nodes
                    If RadioNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = RadioNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Orders)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.DMS) Then
                For Each DMSNode As myTreeNode In treenode.Nodes
                    If DMSNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = DMSNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.DMS)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            ''SUDHIR 20090219
            If treenode.Text = DisclosureSetNames(DisclosureSet.Consent) Then
                For Each ConcentNode As myTreeNode In treenode.Nodes
                    If ConcentNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = ConcentNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Consent)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.Flowsheet) Then
                For Each FlowSheetNode As myTreeNode In treenode.Nodes
                    If FlowSheetNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = FlowSheetNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Flowsheet)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.Immunization) Then
                For Each ImmunizationNode As myTreeNode In treenode.Nodes
                    If ImmunizationNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = ImmunizationNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Immunization)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.Messages) Then
                For Each MessagesNode As myTreeNode In treenode.Nodes
                    If MessagesNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = MessagesNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Messages)
                        arrlist.Add(lst)
                    End If
                Next
            End If

            If treenode.Text = DisclosureSetNames(DisclosureSet.Vitals) Then
                For Each VitalsNode As myTreeNode In treenode.Nodes
                    If VitalsNode.Checked = True Then
                        lst = New myList
                        lst.DisclosureAssociationID = VitalsNode.Key
                        lst.DisclosureType = DisclosureSetNames(DisclosureSet.Vitals)
                        lst.Value = VitalsNode.Tag
                        arrlist.Add(lst)
                    End If
                Next
            End If

            ''END SUDHIR
        Next

        oDisclosure = New clsDisclosureMgmt
        If arrlist.Count > 0 Then
            oDisclosure.SaveDislosureSetDetails(m_DisclosureID, m_PatientID, cmbDisclosureSet.SelectedValue, cmbDisclosureSet.Text, arrlist)
        End If

        If bPrint = False Then
            '  Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End If

    End Sub

    Private Sub Print()
        SaveAssociation(True)
        Dim oRPTViewer As New frmPatient_RptViewer(True, m_PatientID)
        
        For Each TreeNode As myTreeNode In trvAssociation.Nodes
            If TreeNode.Checked = True Then
                Select Case TreeNode.Text
                    Case DisclosureSetNames(DisclosureSet.PatientDemographics)
                        oRPTViewer.PatientDemographic = True
                    Case DisclosureSetNames(DisclosureSet.History)
                        oRPTViewer.History = True
                        'Case DisclosureSetNames(DisclosureSet.PatientExam)
                        '    oRPTViewer.Exam = True
                    Case DisclosureSetNames(DisclosureSet.Messages)
                        oRPTViewer.Message = True
                    Case DisclosureSetNames(DisclosureSet.Vitals)
                        oRPTViewer.Vitals = True
                        ''Sandip Darade 20090310
                    Case DisclosureSetNames(DisclosureSet.PatientExam)
                        oRPTViewer.Exam = True
                        oRPTViewer.PatientExam = True
                        'oRPTViewer.Examlist = EnlistExams() ''Patient exams
                    Case DisclosureSetNames(DisclosureSet.Medication)
                        oRPTViewer.Medication = True
                    Case DisclosureSetNames(DisclosureSet.Labs)
                        oRPTViewer.Labs = True
                    Case DisclosureSetNames(DisclosureSet.Orders)
                        oRPTViewer.Orders = True
                    Case DisclosureSetNames(DisclosureSet.DMS)
                        oRPTViewer.DMS = True
                    Case DisclosureSetNames(DisclosureSet.Consent)
                        oRPTViewer.PatientConsent = True
                    Case DisclosureSetNames(DisclosureSet.Flowsheet)
                        oRPTViewer.Flowsheet = True
                    Case DisclosureSetNames(DisclosureSet.Immunization)
                        oRPTViewer.Immunization = True
                        '''''
                    Case Else
                End Select
            End If
        Next
        oRPTViewer.FromDate = dtpFromDate.Value
        oRPTViewer.ToDate = dtpToDate.Value
        oRPTViewer.ShowDialog(IIf(IsNothing(oRPTViewer.Parent), Me, oRPTViewer.Parent))
        oRPTViewer.Dispose()
        oRPTViewer = Nothing
    End Sub

    Private Sub trvAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAssociation.AfterCheck
        If e.Node.Checked = False Then
            For Each treenode As myTreeNode In e.Node.Nodes
                treenode.Checked = False
            Next
        End If
        If e.Node.Checked = True Then
            For Each treenode As myTreeNode In e.Node.Nodes
                treenode.Checked = True
            Next
        End If
    End Sub

    Private Sub trvAssociation_BeforeCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvAssociation.BeforeCheck
        If e.Node.Checked = False Then
            For Each treenode As myTreeNode In e.Node.Nodes
                treenode.Checked = True
            Next
        End If
        If e.Node.Checked = True Then
            For Each treenode As myTreeNode In e.Node.Nodes
                treenode.Checked = False
            Next
        End If
    End Sub
   
    Private Sub dtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
        If IsFormLoading = False Then
            trvAssociation.Nodes.Clear()
            trvAssociation.CheckBoxes = True
            FillParentNode()
            FillAssociateSet()
        End If
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        If IsFormLoading = False Then
            trvAssociation.Nodes.Clear()
            trvAssociation.CheckBoxes = True
            FillParentNode()
            FillAssociateSet()
        End If
    End Sub
    ''Sandip Darade 20090310
    'Private Function EnlistExams()
    '    _arrExamlist = New ArrayList
    '    Try
    '        For Each treenode As myTreeNode In trvAssociation.Nodes
    '            If treenode.Text = DisclosureSetNames(DisclosureSet.PatientExam) Then
    '                For Each NoteNode As myTreeNode In treenode.Nodes
    '                    If NoteNode.Checked = True Then
    '                        _arrExamlist.Add(Convert.ToInt64(NoteNode.Key)) ''Exam ID 
    '                    End If
    '                Next
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString())
    '    End Try
    '    Return _arrExamlist
    'End Function
End Class