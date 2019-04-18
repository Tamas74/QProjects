Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.Glogeneral

Public Class gloRxHistoryUserCtrl

    Private WithEvents GloRxToolBarUserCtrl1 As gloRxToolbarUserCtrl
    Public Event Recordlock(ByVal blnRecordLock As Boolean)
    Private intMode As Int16
    Private blnIsRefill As Boolean
    Private tempdate As DateTime
    Private tempVisitId As Long
    Public Event PrescriptionLoaded()
    Public Event PrescriptionUnLoaded() 'this fuction is called when user dont want to switch to edit mode
    Private _PatientID As Long
    Public Sub New(ByRef objRxBuisnessLayer As RxBusinesslayer, ByVal PatientID As Long)
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        _RxBuisnessLayer = objRxBuisnessLayer
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private _RxBuisnessLayer As RxBusinesslayer

    Public Property RxBuisnessLayerObject() As RxBusinesslayer
        Get
            Return _RxBuisnessLayer
        End Get
        Set(ByVal value As RxBusinesslayer)
            _RxBuisnessLayer = value
        End Set
    End Property


    Private Sub gloRxHistoryUserCtrl_trvAfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.trvAfterSelect
        Try

            Dim objcol As Prescriptions = Nothing
            If e.Action <> TreeViewAction.Unknown Then
                Dim mynode As myTreeNode
                mynode = CType(e.Node, myTreeNode)
                If mynode.Text <> trvHistory.Nodes.Item(0).Text Then
                    If CType(mynode.Parent, myTreeNode) Is trvHistory.Nodes.Item(0) Then
                        If mynode.Text.Contains("Current") Then
                            objcol = _RxBuisnessLayer.FillPrescription("C")
                        ElseIf mynode.Text.Contains("Yesterday") Then
                            objcol = _RxBuisnessLayer.FillPrescription("Y")
                        ElseIf mynode.Text.Contains("Last Week") Then
                            objcol = _RxBuisnessLayer.FillPrescription("W")
                        ElseIf mynode.Text.Contains("Last Month") Then
                            objcol = _RxBuisnessLayer.FillPrescription("M")
                        ElseIf mynode.Text.Contains("Older") Then
                            objcol = _RxBuisnessLayer.FillPrescription("O")
                        Else
                            'objcol = New Prescriptions()
                        End If
                        mynode.Nodes.Clear()
                        If (IsNothing(objcol) = False) Then


                            Dim i As Integer
                            For i = 0 To objcol.Count - 1
                                Dim PrescriptionNode As myTreeNode

                                PrescriptionNode = New myTreeNode("Prescription for " & CType(objcol.Item(i).Prescriptiondate, String), objcol.Item(i).VisitID, objcol.Item(i).Prescriptiondate)

                                mynode.Nodes.Add(PrescriptionNode)

                                Dim selnode As String
                                selnode = mynode.Nodes.Item(i).Parent.Text

                                If selnode = "Current" Or selnode = "Yesterday" Then
                                    'mynode.Parent.Nodes.Clear()
                                    'Within threshold limits
                                    Dim _ServerTime As DateTime
                                    _ServerTime = _RxBuisnessLayer.getServerTime()
                                    If DateDiff(DateInterval.Minute, PrescriptionNode.Tag, _ServerTime) <= clsgeneral.gnThresholdSetting Then
                                        'PrescriptionNode.ImageIndex = 0
                                        'PrescriptionNode.SelectedImageIndex = 0
                                        PrescriptionNode.ImageIndex = 12
                                        PrescriptionNode.SelectedImageIndex = 12
                                    Else
                                        'outside threshold limits
                                        'PrescriptionNode.ImageIndex = 2
                                        'PrescriptionNode.SelectedImageIndex = 2
                                        PrescriptionNode.ImageIndex = 12
                                        PrescriptionNode.SelectedImageIndex = 12
                                    End If
                                Else
                                    'PrescriptionNode.ImageIndex = 2
                                    'PrescriptionNode.SelectedImageIndex = 2
                                    PrescriptionNode.ImageIndex = 12
                                    PrescriptionNode.SelectedImageIndex = 12
                                End If
                                '**************************code commented from 20/02/2006


                                SetPrescriptionHistoryNodeColor(PrescriptionNode)
                            Next
                            objcol.Dispose()
                            objcol = Nothing
                        End If
                        mynode.ExpandAll()
                        'trPrescriptionHistory.SelectedNode = trPrescriptionHistory.Nodes.Item(0)
                    Else
                        'if selected node is prescription transaction node then
                        If Not IsNothing(CType(mynode, myTreeNode).Tag) Then
                            mynode.Nodes.Clear()
                            'Dim objcol As Prescriptions
                            objcol = _RxBuisnessLayer.FetchPrescriptionforView(CType(mynode, myTreeNode).Key, CType(mynode, myTreeNode).Tag)
                            If Not IsNothing(objcol) Then
                                Dim j As Integer
                                For j = 0 To objcol.Count - 1
                                    Dim mydrugnode As myTreeNode
                                    mydrugnode = New myTreeNode(CType(objcol.Item(j).Medication, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    'Add DrugID
                                    mydrugnode.NodeName = objcol.Item(j).DrugID
                                    mynode.Nodes.Add(mydrugnode)

                                    'mynode.Nodes.Item(i).Nodes.Item(j).Nodes.Add(New myTreeNode("Dosage -" & CType(dt.Rows(j)(2), String), CType(dt.Rows(j)(0), Int64)))
                                    'mynode.Nodes.Item(i).Nodes.Item(j).Nodes.Add(New myTreeNode("Route -" & CType(dt.Rows(j)(3), String), CType(dt.Rows(j)(0), Int64)))
                                    'mynode.Nodes.Item(i).Nodes.Item(j).Nodes.Add(New myTreeNode("Frequency -" & CType(dt.Rows(j)(4), String), CType(dt.Rows(j)(0), Int64)))
                                    'mynode.Nodes.Item(i).Nodes.Item(j).Nodes.Add(New myTreeNode("Duration -" & CType(dt.Rows(j)(5), String), CType(dt.Rows(j)(0), Int64)))
                                    'dt.Rows(j).IsNull(6)
                                    If (CType(objcol.Item(j).Enddate, String) <> "") Then     'Enddate present
                                        'mynode.Nodes.Item(i).Nodes.Item(j).ImageIndex = 1
                                        'mynode.Nodes.Item(i).Nodes.Item(j).SelectedImageIndex = 1
                                        mynode.Nodes.Item(j).ImageIndex = 12
                                        mynode.Nodes.Item(j).SelectedImageIndex = 12
                                    Else 'EndDate Not Present
                                        'mynode.Nodes.Item(i).Nodes.Item(j).ImageIndex = 3
                                        'mynode.Nodes.Item(i).Nodes.Item(j).SelectedImageIndex = 3
                                        mynode.Nodes.Item(j).ImageIndex = 13
                                        mynode.Nodes.Item(j).SelectedImageIndex = 13
                                    End If

                                    Dim medicationitemnode As myTreeNode
                                    medicationitemnode = New myTreeNode("Dosage -" & CType(objcol.Item(j).Medication, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Route -" & CType(objcol.Item(j).Route, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Frequency -" & CType(objcol.Item(j).Frequency, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)


                                    medicationitemnode = New myTreeNode("Duration -" & CType(objcol.Item(j).Duration, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0
                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)

                                    medicationitemnode = New myTreeNode("Dispense -" & CType(objcol.Item(j).Amount, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0

                                    medicationitemnode = New myTreeNode("Refills -" & CType(objcol.Item(j).Refills, String), CType(objcol.Item(j).PrescriptionID, Long))
                                    medicationitemnode.ImageIndex = 0
                                    medicationitemnode.SelectedImageIndex = 0

                                    mynode.Nodes.Item(j).Nodes.Add(medicationitemnode)
                                Next
                                objcol.Dispose()
                                objcol = Nothing


                            End If
                            mynode.ExpandAll()
                            SetPrescriptionHistoryNodeColor(mynode)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetNodeCount(ByVal NodeText As String) As Integer
        Dim _nCount As Integer = 0

        Dim objcol As Prescriptions = Nothing
        Try
            If NodeText = "Current" Then
                objcol = _RxBuisnessLayer.FillPrescription("C")
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
                objcol = _RxBuisnessLayer.FillPrescription("Y")
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
                objcol = _RxBuisnessLayer.FillPrescription("W")
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
                objcol = _RxBuisnessLayer.FillPrescription("M")
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
                objcol = _RxBuisnessLayer.FillPrescription("O")
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
            objcol = Nothing
            Return _nCount
        Finally
            If (IsNothing(objcol) = False) Then
                objcol.Dispose()
                objcol = Nothing
            End If
         
        End Try
    End Function

    Private Sub SetPrescriptionHistoryNodeColor(ByRef PrescriptionNode As myTreeNode)
        Select Case PrescriptionNode.Parent.Text
            Case "Current"
                PrescriptionNode.ForeColor = Color.Blue
                Dim PrescriptionItemNode As myTreeNode
                For Each PrescriptionItemNode In PrescriptionNode.Nodes
                    PrescriptionItemNode.ForeColor = Color.Blue
                    Dim SigNode As myTreeNode
                    For Each SigNode In PrescriptionItemNode.Nodes
                        SigNode.ForeColor = Color.Blue
                    Next
                Next
            Case "Yesterday"
                PrescriptionNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                Dim PrescriptionItemNode As myTreeNode
                For Each PrescriptionItemNode In PrescriptionNode.Nodes
                    PrescriptionItemNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                    Dim SigNode As myTreeNode
                    For Each SigNode In PrescriptionItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                    Next
                Next
            Case "Last Week"
                PrescriptionNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                Dim PrescriptionItemNode As myTreeNode
                For Each PrescriptionItemNode In PrescriptionNode.Nodes
                    PrescriptionItemNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                    Dim SigNode As myTreeNode
                    For Each SigNode In PrescriptionItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                    Next
                Next
            Case "Last Month"
                PrescriptionNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                Dim PrescriptionItemNode As myTreeNode
                For Each PrescriptionItemNode In PrescriptionNode.Nodes
                    PrescriptionItemNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                    Dim SigNode As myTreeNode
                    For Each SigNode In PrescriptionItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                    Next
                Next
            Case "Older"
                PrescriptionNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                Dim PrescriptionItemNode As myTreeNode
                For Each PrescriptionItemNode In PrescriptionNode.Nodes
                    PrescriptionItemNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                    Dim SigNode As myTreeNode
                    For Each SigNode In PrescriptionItemNode.Nodes
                        SigNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                    Next
                Next
        End Select
    End Sub

    Private Function ConcatentateDrugDetails(ByVal objPrescription As Prescription) As String
        If Not IsNothing(objPrescription) Then
            Return objPrescription.Medication & "-" & If(IsNothing(objPrescription.Dosage), " ", " Dosage " & objPrescription.Dosage) & If(IsNothing(objPrescription.Route), " ", " Route - " & objPrescription.Route) & If(IsNothing(objPrescription.Frequency), " ", " Frequency - " & objPrescription.Frequency) & If(IsNothing(objPrescription.Duration), " ", " Duration - " & objPrescription.Duration) & If(IsNothing(objPrescription.Amount), " ", " Dispense - " & objPrescription.Amount) & If(IsNothing(objPrescription.Refills), " ", " Refills - " & objPrescription.Refills)
        Else
            Return ""
        End If
    End Function

    Protected Sub SetControlStyles()
        Try
            With txtHistorySearch
                .BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                .Font = gloGlobal.clsgloFont.gFontVerdana_Regular 'New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular)
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Initializing Control"
            Throw objex
        End Try
    End Sub

    Private Sub txtHistorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHistorySearch.KeyPress

        Try
            If (e.KeyChar = ChrW(13)) Then
                trvHistory.Select()
            Else
                trvHistory.SelectedNode = trvHistory.Nodes.Item(0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


    End Sub

    Private Sub txtHistorySearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHistorySearch.TextChanged
        Try
            If Trim(txtHistorySearch.Text) <> "" Then
                Dim mynode As myTreeNode
                For Each mynode In trvHistory.Nodes.Item(0).Nodes

                    Dim mychildnode As myTreeNode
                    For Each mychildnode In mynode.Nodes
                        Dim str As String
                        str = mychildnode.Text

                        If str.ToUpper.Contains(txtHistorySearch.Text.ToUpper) Then
                            mynode.Parent.ExpandAll()
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
                    'mychildnode is prescriptiondate annd prescription
                    For Each mychildnode In mynode.Nodes
                        Dim mydrugnode As myTreeNode
                        For Each mydrugnode In mychildnode.Nodes
                            If Trim(mydrugnode.Text) <> "" Then
                                Dim str As String
                                str = mydrugnode.Text
                                If str.ToUpper.Contains(txtHistorySearch.Text.ToUpper) Then
                                    mynode.Parent.ExpandAll()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefreshPrescriptionHistory()
        Dim _NodeCount As Integer = 0
        Try

            If _RxBuisnessLayer.CheckRecordCount("C") Then
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
            If _RxBuisnessLayer.CheckRecordCount("Y") Then
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
            If _RxBuisnessLayer.CheckRecordCount("W") Then
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
            If _RxBuisnessLayer.CheckRecordCount("M") Then
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
            If _RxBuisnessLayer.CheckRecordCount("O") Then
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

End Class
