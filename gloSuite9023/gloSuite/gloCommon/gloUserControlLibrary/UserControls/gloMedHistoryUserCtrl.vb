Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMRGeneralLibrary.Glogeneral


Public Class gloMedHistoryUserCtrl

    Public Event cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event MedicationLoaded()

    Public Event Recordlock(ByVal blnRecordLock As Boolean)

    Dim tempVisitId As Long
    Dim tempdate As DateTime
    Private strnodetype As String = ""
    Private VisitID As Long
    Dim _PatuientID As Long

    Public Sub New(ByRef objMedBuisnessLayer As MedicationBusinessLayer, ByVal PatientID As Long)
        MyBase.New()
        InitializeComponent()
        ''AddMenu() Commented from 6040, as per Drew email Sub: : Rx-meds left panel dated 01 August 2011
        _MedBuisnessLayer = objMedBuisnessLayer
        _PatuientID = PatientID

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private _MedBuisnessLayer As MedicationBusinessLayer

    Public Property objMedBuisnessLayer() As MedicationBusinessLayer
        Get
            Return _MedBuisnessLayer
        End Get
        Set(ByVal value As MedicationBusinessLayer)
            _MedBuisnessLayer = value
        End Set
    End Property

    Public Sub AddMenu()
        Dim tlstripitem As ToolStripMenuItem

        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Modify Medication History"
        tlstripitem.Tag = 1
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = imgHistory.Images(14)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf cntListmenuStrip_Click
        'tlstripitem.Dispose()
        tlstripitem = Nothing

        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Delete Medication History"
        tlstripitem.Tag = 2
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = imgHistory.Images(15)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf cntListmenuStrip_Click
        'tlstripitem.Dispose()
        tlstripitem = Nothing

    End Sub

    Public Sub cntListmenuStrip_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim mynode As myTreeNode = Nothing
        Try
            Select Case CType(sender, ToolStripMenuItem).Tag

                Case 1 'Modify Medication History
                    Try
                        mynode = CType(trvHistory.SelectedNode, myTreeNode)
                        If Not IsNothing(mynode) Then
                            AddNodeforEdit(mynode)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                Case 2 'Delete Medication History

                    Try
                        mynode = CType(trvHistory.SelectedNode, myTreeNode)
                        If Not IsNothing(trvHistory.SelectedNode) Then
                            If Not IsNothing(trvHistory.SelectedNode.Tag) Then
                                Dim count As Integer

                                count = _MedBuisnessLayer.GetMedicationPrescriptionCount(mynode.Key, clsgeneral.gnThresholdSetting)
                                If count > 0 Then
                                    MessageBox.Show("The medication cannot be deleted as it has not crossed the threshold limit.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                If MessageBox.Show("Are you sure you want to delete this medication?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                    trvHistory.SelectedNode.Remove()

                                    'If _MedBuisnessLayer.DeleteMedication(mynode.Key, mynode.Tag) Then

                                    '    RefreshMedicationHistory()

                                    'End If
                                End If
                            End If
                        End If
                    Catch ex As SqlClient.SqlException
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(mynode) Then
                mynode = Nothing
            End If
        End Try
    End Sub

    Private Sub AddNodeforEdit(ByVal mynode As myTreeNode)

        If mynode.Text <> trvHistory.Nodes.Item(0).Text Then  'not root node
            If Not mynode.Parent Is trvHistory.Nodes.Item(0) Then 'not current/yesterday node
                If Not IsNothing(mynode.Tag) Then

                    If MessageBox.Show("Do you want to switch to edit mode", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        ''commented in 6050 as we added new form level locking
                        'If clsgeneral.gblnRecordLocking = True Then
                        '    Dim mydt As New gloEMRGeneralLibrary.gloEMRActors.generalList
                        '    mydt = clsgeneral.Scan_n_Lock_Transaction(clsgeneral.TrnType.Medication, _PatuientID, CType(mynode, myTreeNode).Key, Now)
                        '    If mydt.Description <> globalSecurity.gstrClientMachineName Then
                        '        If MessageBox.Show("This Medication is being modified by " & mydt.Code & " on " & mydt.Description & ". You can not modify it. Do you want to open it?", clsgeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '            '' Open For view 
                        '            clsgeneral._blnRecordLock_Med = True
                        '        Else
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        If clsgeneral._blnRecordLock_Med = True Then
                        '        Else
                        '            If _MedBuisnessLayer.CurrentVisitDate = "#12:00:00 AM#" Then
                        '                _MedBuisnessLayer.CurrentVisitDate = Now
                        '            End If

                        '            Call clsgeneral.UnLock_Transaction(clsgeneral.TrnType.Medication, _PatuientID, _MedBuisnessLayer.CurrentVisitID, _MedBuisnessLayer.CurrentVisitDate)
                        '        End If
                        '        clsgeneral._blnRecordLock_Med = False
                        '    End If
                        '    If Not IsNothing(mydt) Then
                        '        mydt = Nothing
                        '    End If

                        '    RaiseEvent Recordlock(clsgeneral._blnRecordLock_Med)
                        'End If

                        _MedBuisnessLayer.MedicationCol.Clear()
                        _MedBuisnessLayer.CurrentVisitID = CType(mynode, myTreeNode).Key

                        _MedBuisnessLayer.FetchMedicationforUpdate(mynode.Tag)

                        If _MedBuisnessLayer.MedicationCol.Count > 0 Then
                            RaiseEvent MedicationLoaded()

                            gloEMRGeneralLibrary.Glogeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gloMedHistoryUserCtrl_trvAfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.trvAfterSelect

        Try
            Dim objcol As Medications = Nothing
            If e.Action <> TreeViewAction.Unknown Then
                Dim mynode As myTreeNode
                mynode = CType(e.Node, myTreeNode)

                'check if selected node is rootnode
                If Not mynode Is trvHistory.Nodes.Item(0) Then
                    'Check if selected node is one of the subnodes(ex current,yesterday,last week,last month etc)
                    If CType(mynode.Parent, myTreeNode) Is trvHistory.Nodes.Item(0) Then
                        If mynode.Text.Contains("Current") Then
                            objcol = _MedBuisnessLayer.FillMedication("C")
                        ElseIf mynode.Text.Contains("Yesterday") Then
                            objcol = _MedBuisnessLayer.FillMedication("Y")
                        ElseIf mynode.Text.Contains("Last Week") Then
                            objcol = _MedBuisnessLayer.FillMedication("W")
                        ElseIf mynode.Text.Contains("Last Month") Then
                            objcol = _MedBuisnessLayer.FillMedication("M")
                        ElseIf mynode.Text.Contains("Older") Then
                            objcol = _MedBuisnessLayer.FillMedication("O")
                        End If
                        mynode.Nodes.Clear()
                        If (IsNothing(objcol) = False) Then


                            Dim i As Integer
                            For i = 0 To objcol.Count - 1
                                Dim MedicationNode As myTreeNode
                                MedicationNode = New myTreeNode("Medication for " & CType(objcol.Item(i).Medicationdate, String), objcol.Item(i).VisitID, CType(objcol.Item(i).Medicationdate, DateTime))

                                mynode.Nodes.Add(MedicationNode)
                                Dim selnode As String
                                selnode = mynode.Nodes.Item(i).Parent.Text

                                If selnode.Contains("Current") Or selnode.Contains("Yesterday") Then
                                    MedicationNode.ImageIndex = 12
                                    MedicationNode.SelectedImageIndex = 12
                                End If

                                SetMedicationHistoryNodeColor(MedicationNode)
                            Next
                            objcol.Dispose()
                            objcol = Nothing
                        End If
                        mynode.ExpandAll()
                    Else
                        Dim thisNode As myTreeNode = CType(mynode, myTreeNode)
                        If Not IsNothing(thisNode.Tag) Then

                            objcol = _MedBuisnessLayer.FetchMedicationforView(thisNode.Key, thisNode.Tag)
                            If Not IsNothing(objcol) Then
                                mynode.Nodes.Clear()
                                Dim j As Integer
                                For j = 0 To objcol.Count - 1
                                    mynode.Nodes.Add(New myTreeNode(CType(objcol.Item(j).Medication, String), CType(objcol.Item(j).MedicationID, Long)))
                                    If (CType(objcol.Item(j).Enddate, String) <> "") Then
                                        mynode.Nodes.Item(j).ImageIndex = 1
                                        mynode.Nodes.Item(j).SelectedImageIndex = 1
                                    Else 'EndDate Not Present
                                        mynode.Nodes.Item(j).ImageIndex = 1
                                        mynode.Nodes.Item(j).SelectedImageIndex = 1
                                    End If

                                    Dim medicationitemnode As myTreeNode
                                    medicationitemnode = New myTreeNode("Dosage -" & CType(objcol.Item(j).Dosage, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Route -" & CType(objcol.Item(j).Route, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Frequency -" & CType(objcol.Item(j).Frequency, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)


                                    medicationitemnode = New myTreeNode("Duration -" & CType(objcol.Item(j).Duration, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Dispense -" & CType(objcol.Item(j).Amount, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Status -" & CType(objcol.Item(j).Status, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Reason -" & CType(objcol.Item(j).Reason, String), CType(objcol.Item(j).MedicationID, Long))
                                    medicationitemnode.ImageIndex = 13
                                    medicationitemnode.SelectedImageIndex = 13
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                Next
                                objcol.Dispose()
                                objcol = Nothing
                            End If
                            mynode.ExpandAll()
                            SetMedicationHistoryNodeColor(mynode)

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetMedicationHistoryNodeColor(ByRef MedicationNode As myTreeNode)
        Select Case MedicationNode.Parent.Text
            Case "Current"
                MedicationNode.ForeColor = Color.Blue
                Dim MedicationItemNode As myTreeNode
                For Each MedicationItemNode In MedicationNode.Nodes
                    MedicationItemNode.ForeColor = Color.Blue
                    Dim SigNode As myTreeNode
                    For Each SigNode In MedicationItemNode.Nodes
                        SigNode.ForeColor = Color.Blue
                    Next
                Next
            Case "Yesterday"
                MedicationNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                Dim MedicationItemNode As myTreeNode
                For Each MedicationItemNode In MedicationNode.Nodes
                    MedicationItemNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                    Dim SigNode As myTreeNode
                    For Each SigNode In MedicationItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                    Next
                Next
            Case "Last Week"
                MedicationNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                Dim MedicationItemNode As myTreeNode
                For Each MedicationItemNode In MedicationNode.Nodes
                    MedicationItemNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                    Dim SigNode As myTreeNode
                    For Each SigNode In MedicationItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                    Next
                Next
            Case "Last Month"
                MedicationNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                Dim MedicationItemNode As myTreeNode
                For Each MedicationItemNode In MedicationNode.Nodes
                    MedicationItemNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                    Dim SigNode As myTreeNode
                    For Each SigNode In MedicationItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                    Next
                Next
            Case "Older"
                MedicationNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                Dim MedicationItemNode As myTreeNode
                For Each MedicationItemNode In MedicationNode.Nodes
                    MedicationItemNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                    Dim SigNode As myTreeNode
                    For Each SigNode In MedicationItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)

                    Next
                Next
        End Select
    End Sub


    Private Sub txtHistorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHistorySearch.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvHistory.Select()
        Else
            trvHistory.SelectedNode = trvHistory.Nodes.Item(0)
        End If

    End Sub

    Private Sub txtHistorySearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHistorySearch.TextChanged

        Try
            If Trim(txtHistorySearch.Text) <> "" Then
                Dim mynode As myTreeNode
                For Each mynode In trvHistory.Nodes.Item(0).Nodes

                    Dim mychildnode As myTreeNode
                    For Each mychildnode In mynode.Nodes

                        Dim str As String
                        str = mychildnode.Text
                        If str.ToUpper.Contains(txtHistorySearch.Text.ToUpper) Then
                            mychildnode.Parent.ExpandAll()
                            trvHistory.SelectedNode = mychildnode
                            txtHistorySearch.Focus()
                            Exit Sub
                        End If
                    Next
                Next
            End If
            If Trim(txtHistorySearch.Text) <> "" Then
                Dim mynode As myTreeNode
                For Each mynode In trvHistory.Nodes.Item(0).Nodes

                    Dim mychildnode As myTreeNode
                    For Each mychildnode In mynode.Nodes
                        Dim mydrugnode As myTreeNode
                        For Each mydrugnode In mychildnode.Nodes
                            If Trim(mydrugnode.Text) <> "" Then

                                Dim str As String
                                str = mydrugnode.Text
                                If str.ToUpper.Contains(txtHistorySearch.Text.ToUpper) Then
                                    mydrugnode.Parent.ExpandAll()
                                    trvHistory.SelectedNode = mydrugnode
                                    txtHistorySearch.Focus()
                                    Exit Sub
                                End If
                                'End If
                            End If
                        Next
                    Next
                Next
            End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' Commented from 6040, as per Drew email Sub: : Rx-meds left panel dated 01 August 2011
    'Private Sub gloMedHistoryUserCtrl_trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.trvDoubleClick
    '    Try
    '        Dim mynode As myTreeNode
    '        mynode = CType(trvHistory.SelectedNode, myTreeNode)
    '        If Not IsNothing(mynode) Then
    '            AddNodeforEdit(mynode)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub




    'function to enable and disable the icons in the history tree view.
    'If data present the the icon is enabled else disabled
    Public Sub RefreshMedicationHistory()
        Dim _NodeCount As Integer = 0
        Try

            If _MedBuisnessLayer.CheckRecordCount("C") Then
                _NodeCount = GetNodeCount("Current")
                trvHistory.Nodes.Item(0).Nodes.Item(0).Text = "Current"
                trvHistory.Nodes.Item(0).Nodes.Item(0).Text = trvHistory.Nodes.Item(0).Nodes.Item(0).Text & " " & "(" & _NodeCount.ToString & ")"
                trvHistory.Nodes.Item(0).Nodes.Item(0).ImageIndex = 2
                trvHistory.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 2
            Else
                trvHistory.Nodes.Item(0).Nodes.Item(0).Text = "Current"
                trvHistory.Nodes.Item(0).Nodes.Item(0).ImageIndex = 3
                trvHistory.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 3
            End If
            If _MedBuisnessLayer.CheckRecordCount("Y") Then
                _NodeCount = GetNodeCount("Yesterday")
                trvHistory.Nodes.Item(0).Nodes.Item(1).Text = "Yesterday"
                trvHistory.Nodes.Item(0).Nodes.Item(1).Text = trvHistory.Nodes.Item(0).Nodes.Item(1).Text & " " & "(" & _NodeCount.ToString & ")"
                trvHistory.Nodes.Item(0).Nodes.Item(1).ImageIndex = 4
                trvHistory.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 4
            Else
                trvHistory.Nodes.Item(0).Nodes.Item(1).Text = "Yesterday"
                trvHistory.Nodes.Item(0).Nodes.Item(1).ImageIndex = 5
                trvHistory.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 5
            End If
            If _MedBuisnessLayer.CheckRecordCount("W") Then
                _NodeCount = GetNodeCount("Last Week")
                trvHistory.Nodes.Item(0).Nodes.Item(2).Text = "Last Week"
                trvHistory.Nodes.Item(0).Nodes.Item(2).Text = trvHistory.Nodes.Item(0).Nodes.Item(2).Text & " " & "(" & _NodeCount.ToString & ")"
                trvHistory.Nodes.Item(0).Nodes.Item(2).ImageIndex = 6
                trvHistory.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 6
            Else
                trvHistory.Nodes.Item(0).Nodes.Item(2).Text = "Last Week"
                trvHistory.Nodes.Item(0).Nodes.Item(2).ImageIndex = 7
                trvHistory.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 7
            End If
            If _MedBuisnessLayer.CheckRecordCount("M") Then
                _NodeCount = GetNodeCount("Last Month")
                trvHistory.Nodes.Item(0).Nodes.Item(3).Text = "Last Month"
                trvHistory.Nodes.Item(0).Nodes.Item(3).Text = trvHistory.Nodes.Item(0).Nodes.Item(3).Text & " " & "(" & _NodeCount.ToString & ")"
                trvHistory.Nodes.Item(0).Nodes.Item(3).ImageIndex = 8
                trvHistory.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 8
            Else
                trvHistory.Nodes.Item(0).Nodes.Item(3).Text = "Last Month"
                trvHistory.Nodes.Item(0).Nodes.Item(3).ImageIndex = 9
                trvHistory.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 9
            End If
            If _MedBuisnessLayer.CheckRecordCount("O") Then
                _NodeCount = GetNodeCount("Older")
                trvHistory.Nodes.Item(0).Nodes.Item(4).Text = "Older"
                trvHistory.Nodes.Item(0).Nodes.Item(4).Text = trvHistory.Nodes.Item(0).Nodes.Item(4).Text & " " & "(" & _NodeCount.ToString & ")"
                trvHistory.Nodes.Item(0).Nodes.Item(4).ImageIndex = 10
                trvHistory.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 10
            Else
                trvHistory.Nodes.Item(0).Nodes.Item(4).Text = "Older"
                trvHistory.Nodes.Item(0).Nodes.Item(4).ImageIndex = 11
                trvHistory.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 11
            End If
            MyBase.RefreshHistoryData()

        Catch ex As Exception

        End Try
    End Sub

    Public Function GetNodeCount(ByVal NodeText As String) As Integer
        Dim _nCount As Integer = 0
        'Dim _RxDBLayer As New RxDBLayer
        Dim objcol As Medications = Nothing
        Try


            'Dim objcol As Prescriptions
            If NodeText = "Current" Then
                objcol = _MedBuisnessLayer.FillMedication("C")
                If Not IsNothing(objcol) Then
                    If objcol.Count > 0 Then
                        _nCount = objcol.Count
                        Return _nCount
                    Else
                        Return _nCount
                    End If

                Else
                    Return _nCount
                End If

            ElseIf NodeText = "Yesterday" Then
                objcol = _MedBuisnessLayer.FillMedication("Y")
                If Not IsNothing(objcol) Then
                    If objcol.Count > 0 Then
                        _nCount = objcol.Count
                        Return _nCount
                    Else
                        Return _nCount
                    End If

                Else
                    Return _nCount
                End If
            ElseIf NodeText = "Last Week" Then
                objcol = _MedBuisnessLayer.FillMedication("W")
                If Not IsNothing(objcol) Then
                    If objcol.Count > 0 Then
                        _nCount = objcol.Count
                        Return _nCount
                    Else
                        Return _nCount
                    End If

                Else
                    Return _nCount
                End If
            ElseIf NodeText = "Last Month" Then
                objcol = _MedBuisnessLayer.FillMedication("M")
                If Not IsNothing(objcol) Then
                    If objcol.Count > 0 Then
                        _nCount = objcol.Count
                        Return _nCount
                    Else
                        Return _nCount
                    End If

                Else
                    Return _nCount
                End If
            ElseIf NodeText = "Older" Then
                objcol = _MedBuisnessLayer.FillMedication("O")
                If Not IsNothing(objcol) Then
                    If objcol.Count > 0 Then
                        _nCount = objcol.Count
                        Return _nCount
                    Else
                        Return _nCount
                    End If

                Else
                    Return _nCount
                End If
            End If
            Return _nCount
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            objcol = Nothing
            Return _nCount
        Finally
            If (IsNothing(objcol) = False) Then
                objcol.Dispose()
                objcol = Nothing
            End If
            'objcol = Nothing
        End Try
    End Function

    ''Commented from 6040, as per Drew email Sub: : Rx-meds left panel dated 01 August 2011
    'Private Sub gloMedHistoryUserCtrl_trvMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.trvMouseDown
    '    Try
    '        If e.Button = Windows.Forms.MouseButtons.Right Then

    '            Dim trvnode As myTreeNode
    '            trvnode = CType(trvHistory.GetNodeAt(e.X, e.Y), myTreeNode)
    '            If IsNothing(trvnode) = False Then
    '                trvHistory.SelectedNode = trvnode
    '                Dim mynode As myTreeNode
    '                mynode = trvHistory.SelectedNode
    '                If Not IsNothing(mynode) Then
    '                    If trvHistory.Nodes.Item(0).Text = mynode.Text Then
    '                        trvHistory.ContextMenuStrip = Nothing
    '                    ElseIf mynode.Parent Is CType(trvHistory.Nodes.Item(0), myTreeNode) Then
    '                        trvHistory.ContextMenuStrip = cntListmenuStrip
    '                        cntListmenuStrip.Items(0).Visible = False 'this is for Edit Prescription menu
    '                        cntListmenuStrip.Items(1).Visible = False 'this is for Delete Prescription menu
    '                    ElseIf Not IsNothing(mynode.Tag) Then
    '                        Dim selnode As String
    '                        selnode = mynode.Parent.Text
    '                        trvHistory.ContextMenuStrip = cntListmenuStrip

    '                        cntListmenuStrip.Items(0).Visible = True 'this is for Edit Prescription menu
    '                        cntListmenuStrip.Items(1).Visible = True 'this is for Delete Prescription History menu
    '                    Else
    '                        cntListmenuStrip.Items(0).Visible = False 'this is for Edit Prescription menu
    '                        cntListmenuStrip.Items(1).Visible = False 'this is for Delete Prescription History menu
    '                    End If

    '                Else
    '                    trvHistory.ContextMenu = Nothing
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub
    'C

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class
