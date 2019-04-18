
Public Class DateManagement

    Public node As myTreeNode
    Public _dt As DataTable
    Public _flag As Categorization
    Dim i As Integer
    Public mySelectedNode As myTreeNode
    Public Shared NodesCount As Integer
    Public sDateCategory As String = ""

    Public Event trvCategoryDoublclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event trvcatagoryAfterselect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    Public Event trvSCatagaoryMouseNodeClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    Public Event cmnuTasks_click(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CmnuTasks_complete(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Msg_NewMEssages(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Msg_History(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event trvCatagoryMouse_upClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event trvCategoryMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)


    Public Enum Categorization
        None = 0
        Tasks = 1
        Messages = 2
        UnFinishedExams = 3
        History = 4
        PatientROS = 5
        Radiology = 6
        'sarika Date Mgt 20081006
        'opened from unfinished exams form
        UnFinishedExams1 = 7
        '---
    End Enum
    Public Sub New(ByVal dt As DataTable, ByVal flag As Categorization)
        _dt = dt
        _flag = flag


        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal dt As DataTable, ByVal flag As Categorization, ByVal oNode As myTreeNode)
        _dt = dt
        _flag = flag

        mySelectedNode = oNode

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal dt As DataTable, ByVal flag As Categorization, ByVal selDateCategory As String)
        _dt = dt
        _flag = flag

        sDateCategory = selDateCategory

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub DateManagement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowDateCategories(_dt, _flag, sDateCategory)
    End Sub

    Public Sub ShowDateCategories(ByVal _dt As DataTable, ByVal _flag As Categorization, ByVal selDateCategory As String)
        trvCateogory.Nodes.Clear()
        sDateCategory = selDateCategory        
        If _flag = Categorization.Tasks Then
            If _dt.Rows.Count > 0 Then
                For i = 0 To _dt.Rows.Count - 1
                    Dim node As myTreeNode

                    node = New myTreeNode
                    node.Text = Format(_dt.Rows(i)(2), "MM/dd/yyyy hh:mm tt") & " : " & CType(_dt.Rows(i)(1), System.String) & " - " & _dt.Rows(i)("sPatientCode") & "- " & _dt.Rows(i)("sFirstName") & " " & _dt.Rows(i)("sLastName")
                    node.Tag = CType(_dt.Rows(i)(0), System.Int64) 'Store TaskId
                    node.Key = _dt.Rows(i)("TaskType")  '' Task Type TASK/FAX/ORDER

                    node.OrderTime = _dt.Rows(i)("DueDate")  '' TASK Due Date                    
                    node.TemplateResult = _dt.Rows(i)("Status")  '' TASK Status
                    node.NodeName = (_dt.Rows(i)("Priority")) '' Task Priority

                    Dim Selecteddate As Date = _dt.Rows(i)("DueDate")
                    Dim Selectedcategory As String
                    Selectedcategory = GetDateCategoryField(Selecteddate)


                    Dim IsCategoryPresent As Boolean = False

                    Dim newparentnode As New myTreeNode

                    For Each myparentnode As myTreeNode In trvCateogory.Nodes
                        If Selectedcategory = myparentnode.Text Then
                            IsCategoryPresent = True
                            myparentnode.Nodes.Add(node)
                            Exit For
                        End If
                    Next
                    If IsCategoryPresent = False Then
                        newparentnode = New myTreeNode
                        newparentnode.Text = Selectedcategory
                        trvCateogory.Nodes.Add(newparentnode)
                        newparentnode.Nodes.Add(node)
                    End If

                    Dim ImageIndex As Int16 = 1

                    '' // Status of Task
                    Select Case node.TemplateResult.ToString
                        Case "Not Started"
                            ImageIndex = 9
                        Case "In Progress"
                            ImageIndex = 10
                        Case "Waiting on someone else"
                            ImageIndex = 12
                        Case "Deferred"
                            ImageIndex = 11
                    End Select
                    ''// 

                    '' // Priority of Task
                    Select Case node.NodeName.Trim
                        Case "Low"
                            node.ForeColor = System.Drawing.Color.Blue
                        Case "Normal"
                            node.ForeColor = System.Drawing.Color.Magenta
                        Case "High"
                            node.ForeColor = System.Drawing.Color.Maroon
                    End Select
                    ''// 
                    node.ImageIndex = ImageIndex
                    node.SelectedImageIndex = ImageIndex

                    'Change made to solve memory Leak and word crash issue
                    node = Nothing
                    newparentnode = Nothing
                Next

                cmnuTask_Complete.Visible = True
                cmnuTask_Add.Visible = True
                MenuItem1.Visible = False
                MenuItem2.Visible = False


                NodesCount = trvCateogory.Nodes.Count

                Dim IsExpand As Boolean = False
                For i = 0 To trvCateogory.GetNodeCount(False) - 1
                    '' If The Node Contains sub node then
                    If trvCateogory.Nodes(i).GetNodeCount(False) > 0 Then
                        If IsExpand = False Then
                            trvCateogory.Nodes(i).Expand()
                            '' Make Select First Node
                            trvCateogory.SelectedNode = trvCateogory.Nodes(i).Nodes(0)
                            IsExpand = True
                        End If
                        '' To Give the No. of Sub Nodes the Parent Node Contains
                        trvCateogory.Nodes(i).Text = trvCateogory.Nodes(i).Text & " (" & trvCateogory.Nodes(i).GetNodeCount(False) & ")"
                    End If
                Next

                trvCateogory.Show()
                If trvCateogory.Nodes.Count > 0 Then
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    trvCateogory.SelectedNode.Expand()
                End If

                trvCateogory.SelectedNode.EnsureVisible()
                trvCateogory.Focus()
            Else
                cmnuTask_Complete.Visible = False
                cmnuTask_Add.Visible = True
                MenuItem1.Visible = False
                MenuItem2.Visible = False
                trvCateogory.Focus()
            End If
        ElseIf _flag = Categorization.Messages Then

            Try
                If _dt.Rows.Count > 0 Then
                    For i = 0 To _dt.Rows.Count - 1

                        Dim MsgNode As New myTreeNode
                        MsgNode.Tag = CType(_dt.Rows(i)(0), System.Int64)
                        MsgNode.Text = Format(_dt.Rows(i)(1), "MM/dd/yyyy hh:mm tt") & "-" & CType(_dt.Rows(i)(2), System.String) & "-" & CType(_dt.Rows(i)(3), System.String) & "-" & CType(_dt.Rows(i)(4), System.String)
                        MsgNode.OrderTime = (CType(_dt.Rows(i)(1), Date))
                        MsgNode.NodeName = CStr(_dt.Rows(i)(2))
                        MsgNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)

                        MsgNode.ImageIndex = 1
                        MsgNode.SelectedImageIndex = 1
                        Dim Selecteddate As Date = _dt.Rows(i)(1)
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)

                        Dim IsCatagorypresent As Boolean = False

                        Dim newparentnode As myTreeNode

                        For Each myparentnode As myTreeNode In trvCateogory.Nodes
                            If SelectedCatagory = myparentnode.Text Then
                                IsCatagorypresent = True
                                myparentnode.Nodes.Add(MsgNode)
                                Exit For
                            End If
                        Next
                        If IsCatagorypresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0
                            trvCateogory.Nodes.Add(newparentnode)
                            newparentnode.Nodes.Add(MsgNode)
                        End If
                        'Change made to solve memory Leak and word crash issue
                        MsgNode = Nothing
                        newparentnode = Nothing
                    Next



                    Dim IsExpand As Boolean = False
                    For i = 0 To trvCateogory.GetNodeCount(False) - 1
                        '' If The Node Contains sub node then
                        If trvCateogory.Nodes(i).GetNodeCount(False) > 0 Then
                            If IsExpand = False Then
                                trvCateogory.Nodes(i).Expand()
                                '' Make Select First Node
                                trvCateogory.SelectedNode = trvCateogory.Nodes(i).Nodes(0)
                                IsExpand = True
                            End If
                            '' To Give the No. of Sub Nodes the Parent Node Contains
                            trvCateogory.Nodes(i).Text = trvCateogory.Nodes(i).Text & " (" & trvCateogory.Nodes(i).GetNodeCount(False) & ")"
                        End If
                    Next

                    If trvCateogory.Nodes.Count > 0 Then
                        trvCateogory.SelectedNode = trvCateogory.Nodes(0).Nodes(0)
                        trvCateogory.SelectedNode.Expand()
                    End If

                    trvCateogory.Show()
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    NodesCount = trvCateogory.Nodes.Count
                    trvCateogory.SelectedNode.EnsureVisible()
                    trvCateogory.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            cmnuTask_Add.Visible = False
            cmnuTask_Complete.Visible = False
            MenuItem1.Visible = True
            MenuItem2.Visible = True


        ElseIf _flag = Categorization.UnFinishedExams Then
            Try
                If _dt.Rows.Count > 0 Then
                    Dim newparentnode As myTreeNode
                    Dim mynode As myTreeNode
                    For i = 0 To _dt.Rows.Count - 1
                        mynode = New myTreeNode

                        mynode.Key = _dt.Rows(i)("ExamID")                        
                        mynode.Tag = _dt.Rows(i)("DOS") & " - " & _dt.Rows(i)("ExamName")
                        mynode.Text = _dt.Rows(i)("DOS") & " - " & _dt.Rows(i)("ExamName") & " : " & _dt.Rows(i)("PatientCode") & " - " & _dt.Rows(i)("PatientName")  '' & " " & dt.Rows(i)("sLastName")
                        mynode.PatientId = _dt.Rows(i)("PatientID")
                        mynode.NodeName = _dt.Rows(i)("PatientCode")
                        mynode.TemplateResult = _dt.Rows(i)("VisitID")
                        mynode.ImageIndex = 1
                        mynode.SelectedImageIndex = 1

                        Dim Selecteddate As Date = _dt.Rows(i)("DOS")
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)

                        Dim IsCatagorypresent As Boolean = False


                        For Each myparentnode As myTreeNode In trvCateogory.Nodes
                            If SelectedCatagory = myparentnode.Text Then
                                IsCatagorypresent = True
                                myparentnode.Nodes.Add(mynode)
                                Exit For
                            End If
                        Next
                        If IsCatagorypresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0
                            trvCateogory.Nodes.Add(newparentnode)
                            newparentnode.Nodes.Add(mynode)
                        End If
                        'Change made to solve memory Leak and word crash issue
                        mynode = Nothing
                        newparentnode = Nothing
                    Next

                    Dim IsExpand As Boolean = False
                    For n As Integer = 0 To trvCateogory.GetNodeCount(False) - 1
                        '' If The Node Contains sub node then
                        If trvCateogory.Nodes(n).GetNodeCount(False) > 0 Then
                            If IsExpand = False Then
                                trvCateogory.Nodes(n).Expand()
                                '' Make Select First Node
                                trvCateogory.SelectedNode = trvCateogory.Nodes(n).Nodes(0)
                                IsExpand = True
                            End If
                            '' To Give the No. of Sub Nodes the Parent Node Contains
                            trvCateogory.Nodes(n).Text = trvCateogory.Nodes(n).Text & " (" & trvCateogory.Nodes(n).GetNodeCount(False) & ")"
                        End If
                    Next

                    trvCateogory.Show()
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    trvCateogory.SelectedNode.Expand()
                    NodesCount = trvCateogory.Nodes.Count

                    trvCateogory.SelectedNode.EnsureVisible()
                    trvCateogory.Focus()

                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        ElseIf _flag = Categorization.PatientROS Then
            Try
                If _dt.Rows.Count > 0 Then


                    Dim Rootnode As New myTreeNode
                    Rootnode.Text = "PatientRos"
                    trvCateogory.Nodes.Add(Rootnode)

                    Dim newparentnode As myTreeNode
                    Dim mynode As myTreeNode
                    For i = 0 To _dt.Rows.Count - 1
                        mynode = New myTreeNode

                        mynode.Text = (Format(_dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"))
                        mynode.Key = _dt.Rows(i)("nVisitID")
                        mynode.ImageIndex = 1
                        mynode.SelectedImageIndex = 1
                        Dim Selecteddate As Date = _dt.Rows(i)(1)
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)
                        Dim IsCatagorypresent As Boolean = False


                        For Each myparentnode As myTreeNode In trvCateogory.Nodes
                            For Each dateNode As myTreeNode In myparentnode.Nodes
                                If SelectedCatagory = dateNode.Text Then
                                    IsCatagorypresent = True
                                    dateNode.Nodes.Add(mynode)

                                    Exit For
                                End If
                            Next
                        Next
                        If IsCatagorypresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.Key = -1
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0
                            Rootnode.Nodes.Add(newparentnode)
                            newparentnode.Nodes.Add(mynode)
                        End If
                        'Change made to solve memory Leak and word crash issue
                        mynode = Nothing
                        newparentnode = Nothing
                        Rootnode = Nothing
                    Next
                    trvCateogory.Show()
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    NodesCount = trvCateogory.Nodes.Count
                    trvCateogory.SelectedNode.EnsureVisible()
                    trvCateogory.Focus()
                End If
            Catch ex As Exception
            End Try

        ElseIf _flag = Categorization.History Then
            Try
                If _dt.Rows.Count > 0 Then
                    Dim RootNode As New myTreeNode
                    RootNode.Text = "Patient History"
                    trvCateogory.Nodes.Add(RootNode)


                    Dim mynode As myTreeNode
                    For i = 0 To _dt.Rows.Count - 1
                        mynode = New myTreeNode
                        mynode.Text = (Format(_dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"))
                        mynode.Key = _dt.Rows(i)("nVisitID")
                        mynode.ImageIndex = 1
                        mynode.SelectedImageIndex = 1
                        Dim Selecteddate As Date = _dt.Rows(i)("dtVisitDate")
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)


                        Dim IsCategoryPresent As Boolean = False

                        Dim newparentnode As New myTreeNode


                        For Each myparentnode As myTreeNode In trvCateogory.Nodes
                            For Each dateNode As myTreeNode In myparentnode.Nodes
                                If SelectedCatagory = dateNode.Text Then
                                    IsCategoryPresent = True
                                    dateNode.Nodes.Add(mynode)
                                    Exit For
                                End If
                            Next
                        Next
                        If IsCategoryPresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.Key = -1
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0
                            RootNode.Nodes.Add(newparentnode)
                            newparentnode.Nodes.Add(mynode)

                        End If
                        'Change made to solve memory Leak and word crash issue
                        RootNode = Nothing
                        newparentnode = Nothing
                        mynode = Nothing
                    Next
                    trvCateogory.Show()
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    NodesCount = trvCateogory.Nodes.Count
                    trvCateogory.SelectedNode.EnsureVisible()
                    trvCateogory.Focus()
                End If
            Catch ex As Exception
            End Try


        ElseIf _flag = Categorization.Radiology Then
            Try
                If _dt.Rows.Count > 0 Then
                    Dim Rootnode As New myTreeNode
                    Rootnode.Text = "Orders History"

                    Dim newparentnode As myTreeNode
                    Dim mynode As myTreeNode
                    For i = 0 To _dt.Rows.Count - 1
                        mynode = New myTreeNode
                        mynode.Text = (Format(_dt.Rows(i)(5), "MM/dd/yyyy"))
                        mynode.Tag = _dt.Rows(i)(2)
                        mynode.Key = _dt.Rows(i)(2)
                        mynode.ImageIndex = 1
                        mynode.SelectedImageIndex = 1
                        Dim Selecteddate As Date = _dt.Rows(i)(5)
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)

                        Dim IsCatagoryPresent As Boolean = False
                        For Each myparentode As myTreeNode In trvCateogory.Nodes
                            For Each datenode As myTreeNode In myparentode.Nodes
                                If SelectedCatagory = myparentode.Text Then
                                    IsCatagoryPresent = True
                                    datenode.Nodes.Add(mynode)
                                    Exit For
                                End If
                            Next
                        Next
                        If IsCatagoryPresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.Key = -1
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0
                            trvCateogory.Nodes.Add(newparentnode)
                            newparentnode.Nodes.Add(mynode)

                        End If
                        'Change made to solve memory Leak and word crash issue
                        Rootnode = Nothing
                        newparentnode = Nothing
                        mynode = Nothing
                    Next

                    trvCateogory.Show()
                    trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    trvCateogory.SelectedNode.EnsureVisible()
                    trvCateogory.Focus()
                End If
            Catch ex As Exception
            End Try

            'sarika Date Mgt 20081006
        ElseIf _flag = Categorization.UnFinishedExams1 Then
            Try
                If _dt.Rows.Count > 0 Then
                    Dim Rootnode As New myTreeNode
                    Rootnode.Text = "Unfinished Exams"

                    Dim newparentnode As myTreeNode
                    '   Dim mynode As myTreeNode
                    For i = 0 To _dt.Rows.Count - 1                        
                        Dim Selecteddate As Date = _dt.Rows(i)("DOS")
                        Dim SelectedCatagory As String
                        SelectedCatagory = GetDateCategoryField(Selecteddate)

                        Dim IsCatagoryPresent As Boolean = False
                        For Each myparentode As myTreeNode In trvCateogory.Nodes                            
                            If SelectedCatagory = myparentode.Text Then
                                IsCatagoryPresent = True                                
                                _dt.Rows(i)("Category") = SelectedCatagory
                                Exit For
                            End If                            
                        Next
                        If IsCatagoryPresent = False Then
                            newparentnode = New myTreeNode
                            newparentnode.Text = SelectedCatagory
                            newparentnode.Key = -1
                            newparentnode.ImageIndex = 0
                            newparentnode.SelectedImageIndex = 0

                            trvCateogory.Nodes.Add(newparentnode)                            
                            _dt.Rows(i)("Category") = SelectedCatagory
                        End If
                        'Change made to solve memory Leak and word crash issue
                        Rootnode = Nothing
                        newparentnode = Nothing
                    Next

                    Dim newnode As New gloUserControlLibrary.myTreeNode
                    newnode.Text = "Customize"
                    newnode.ImageIndex = 0
                    newnode.SelectedImageIndex = 0
                    If trvCateogory.Nodes.Contains(newnode) = False Then
                        trvCateogory.Nodes.Add(newnode)
                    Else
                        'newnode.Dispose()
                    End If
                    newnode = Nothing 'Change made to solve memory Leak and word crash issue

                    If sDateCategory = "" Then
                        trvCateogory.SelectedNode = trvCateogory.Nodes.Item(0)
                    Else
                        'loop thru all the treenodes
                        For tncnt As Integer = 0 To trvCateogory.Nodes.Count - 1
                            If trvCateogory.Nodes.Item(tncnt).Text = sDateCategory Then
                                trvCateogory.SelectedNode = trvCateogory.Nodes.Item(tncnt)
                                Exit Sub
                            End If
                        Next
                    End If

                    trvCateogory.Show()
                    NodesCount = trvCateogory.Nodes.Count                    
                    trvCateogory.Focus()
                Else
                    Dim newnode As New gloUserControlLibrary.myTreeNode
                    newnode.Text = "Customize"
                    newnode.ImageIndex = 0
                    newnode.SelectedImageIndex = 0
                    If trvCateogory.Nodes.Contains(newnode) = False Then
                        trvCateogory.Nodes.Add(newnode)
                    Else
                        'newnode.Dispose()
                    End If
                    newnode = Nothing 'Change made to solve memory Leak and word crash issue

                    trvCateogory.Show()
                    NodesCount = trvCateogory.Nodes.Count
                    trvCateogory.Focus()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If


    End Sub



    Public Function GetDateCategoryField(ByVal selectedDate As Date) As String

        Dim diff As Integer = Now.Day - selectedDate.Day
       
        '''''''''Beyond Next Month'''''''''''''''''''''''
        If Now.Month <> selectedDate.Month And Now.Month < selectedDate.Month And Now.Year = selectedDate.Year And Now.Month <> (selectedDate.Month) - 1 And Now.Month <> 12 Then            
            Return "Beyond Next Month"
            '''''''''Beyond Next Month'''''''''''''''''''''''
        ElseIf Now.Year < selectedDate.Year And Now.Month <> 12 Then            
            Return "Beyond Next Month"
            '''''''' For current Month , Sunday and Future Task '''''''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then            
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -2 And diff > -8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then            
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then            
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then            
            Return "Later this Month"
            '''''' For Previous Month , Sunday and Past Task and For 31 days Month
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then '''' New Start Sunday            
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff > -4 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then            
            Return "Last Month"

            '''''' For Previous Month , Sunday and Past Task and For 30 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -17 And diff > -24) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -10 And diff > -17) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -3 And diff > -10) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then            
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff > -3 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then  '''' New End Sunday            
            Return "Last Month"

            '''''' For Previous Month , Sunday and Past Task and For 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -23 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -22 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then            
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -1 And diff > -8) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff > -1 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Month"

            '''''' For Previous Month , Sunday and Past Task and For 29 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -23 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -2 And diff > -9) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff > -2 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Month"

            '''''''''''''''''''For Future Task,Next Month,Sunday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 30 And diff > 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 23 And diff > 16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 16 And diff > 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Sunday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 29 And diff > 22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 22 And diff > 15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 15 And diff > 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Sunday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 27 And diff > 20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 20 And diff > 13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 13 And diff > 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Sunday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 28 And diff > 21) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 21 And diff > 14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 14 And diff > 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Month"

            ''''''''For Previous Year, Sunday and Past Task'''''''''''''''''''''''''''''''''''''''''''
            ''''''Add on 06052008 Start
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -29 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -28 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -27 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -26 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = -25 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff > -4 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Month"
            ''''''Add on 06052008 End

            '''''''''''''''''For Future Task and Next Year ,sunday''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 29 And diff > 23) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 23 And diff > 16) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 16 And diff > 9) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff <= 9) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And Now.Year < selectedDate.Year And Now.Month = 12 Then
            Return "Beyond Next Month"


            ''''''''''''''''For Current Month,Sunday and This month  Previous task'''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff >= 7 And diff < 14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff >= 14 And diff < 21) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff >= 21 And diff < 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And (diff >= 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Starting this Month"            
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 6 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 5 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 4 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 3 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 2 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Sunday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Today"


            '''''''''For Future Task for current Month ,saturday'''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -2 And diff > -9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Later this Month"
            
            '''''''''''''For Past Task Previous Month,saturday, 31 days Month'''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then '''' New Start Saturday
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff > -5 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Month"

            ''''''''''''''' for Past Task Previous Month,saturday, 30 days Month
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff > -4 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then '''' New End Saturday
            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,saturday, 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -23 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -2 And diff > -9) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff > -2 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Month"
            ''''''''''''''' for Past Task Previous Month,saturday, 29 days Month
            '''''''Add on 06052008 Start
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And Now.Year = selectedDate.Year And selectedDate.Month <> 12 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -17 And diff > -24) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -10 And diff > -17) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -3 And diff > -10) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff > -3 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 And selectedDate.Month <> 12 Then
            Return "Last Month"

            '''''''Add on 06052008 END

            '''''''''''''''''''For Future Task,Next Month,Saturday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 29 And diff > 22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 22 And diff > 15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 15 And diff > 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Saturday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 28 And diff > 21) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 21 And diff > 14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 14 And diff > 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Saturday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 26 And diff > 19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 19 And diff > 12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 12 And diff > 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Saturday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 27 And diff > 20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 20 And diff > 13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 13 And diff > 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Month"

            '''''''''''''''''For past Task and Previous Year,Saturday '''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''Addd on 06052008 Start
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -29 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -28 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -27 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = -26 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff > -5 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Month"
            '''''Addd on 06052008 END
            '''''''''''''For Future task ,Next year,Saturday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 29 And diff > 22) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 22 And diff > 15) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 15 And diff > 8) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff <= 8) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And Now.Year < selectedDate.Year And Now.Month = 12 Then
            Return "Beyond Next Month"

            '''''''''''''''''''''''' For past Task in current Month'''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff >= 6 And diff < 13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff >= 13 And diff < 20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff >= 20 And diff < 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And (diff >= 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Starting this Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 5 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 4 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 3 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 2 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Saturday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Today"

            ''''''''''''''''' For Future task in current Month, Friday'''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = -2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -3 And diff > -10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -10 And diff > -17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -16 And diff > -24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Later this Month"
           
            '''''''''''''''''''' For past Task in Previous Month, Friday, Previous Month with 31 days ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then ''''New Start Friday
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff > -6 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Month"

            '''''''''''''''''''''' For past Task in previous month, Friday, Prevous Month with 30 days '''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff > -5 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then '''' New End Firday
            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Friday, 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -24 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -17 And diff > -24) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -10 And diff > -17) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -3 And diff > -10) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff > -3 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Friday, 29 days Month
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff > -4 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Month"

            '''''''''''''''''''For Future Task,Next Month,Friday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 28 And diff > 21) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 21 And diff > 14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 14 And diff > 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Friday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 27 And diff > 20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 20 And diff > 13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 13 And diff > 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Friday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 25 And diff > 18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 18 And diff > 11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 11 And diff > 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then
            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Friday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 26 And diff > 19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 19 And diff > 12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 12 And diff > 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then
            Return "Next Month"

            '''''''''''''''''''For Past Task Last year, Friday''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -29 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -28 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = -27 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff > -6 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            Return "Last Month"

            '''''''''''''For Future task ,Next year,Friday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 29 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 28 And diff > 21) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 21 And diff > 14) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 14 And diff > 7) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff <= 7) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then
            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And Now.Year < selectedDate.Year And Now.Month = 12 Then
            Return "Beyond Next Month"

            '''''''''''''For Past Task, Current Month, Friday '''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff >= 5 And diff < 12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff >= 12 And diff < 19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff >= 19 And diff < 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And (diff >= 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Starting this Month"            
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 4 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 3 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 2 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Friday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Today"

            '''''''''''''''''For Future Task,Next Month,Thursday'''''''''''''''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = -2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = -3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff <= -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            Return "Later this Month"
            
            ''''''''''''''''''For Past Task, Previous Month,Thursday, 31 days Month''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then  ''New Start Thursday
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff > -7 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            Return "Last Month"

            '''''''''''''''''''''For Past Task , Previous Month,Thursday,30 days Month''''''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff > -6 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then '''' New End Thursday
            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Thursday, 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -25 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -18 And diff > -25) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -11 And diff > -18) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -4 And diff > -11) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff > -4 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Thursday, 29 days Month
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff > -5 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            Return "Last Month"

            '''''''''''''''''''For Future Task,Next Month,Thursday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 27 And diff > 20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 20 And diff > 13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 13 And diff > 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then
            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Thursday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 26 And diff > 19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 19 And diff > 12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 12 And diff > 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Thursday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 24 And diff > 17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 17 And diff > 10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 10 And diff > 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Thursday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 25 And diff > 18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 18 And diff > 11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 11 And diff > 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Month"


            '''''''''''''''''''For Past Task, Last Year,Thursday''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -29 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            'MessageBox.Show("Tuesday")
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = -28 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff > -7 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Month"


            '''''''''''''For Future task ,Next year,Thursday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 29 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 28 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 27 And diff > 20) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 20 And diff > 13) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 13 And diff > 6) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff <= 6) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And Now.Year < selectedDate.Year And Now.Month = 12 Then

            Return "Beyond Next Month"

            ''''''''''''''''' For Current Month,Past task , Thursday''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff >= 4 And diff < 11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff >= 11 And diff < 18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff >= 18 And diff < 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff >= 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Starting this Month"

            '    Return "Four Weeks Ago"
            'ElseIf Now.DayOfWeek = DayOfWeek.Thursday And (diff >= 32 And diff < 39) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            '    Return "Five Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 3 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 2 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            'MessageBox.Show("Tuesday")
            Return "Tuesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Thursday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Today"


            ''''''''''''''''' For Current Month,Future task , Wednesday''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = -2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = -3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = -4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff <= -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Later this Month"
            'ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -26 And diff > -33) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            '    'MessageBox.Show("Four Weeks Away")
            '    Return "Four Weeks Away"
            'ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -33 And diff > -40) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            '    'MessageBox.Show("Five Weeks Away")
            '    Return "Five Weeks Away"

            ''''''''''' For Last Month, Past Task, Wednesday, 30 Days Month''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then '''' New Start Wednesday

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff > -7 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Last Month"
            ''''''''' For Last Month, past Task Wednesday, 31 Days Month'''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -22 And diff > -29) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff > -8 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then '''' New End Wednesday

            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Wednesday, 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -26 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -19 And diff > -26) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -12 And diff > -19) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -5 And diff > -12) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff > -5 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Wednesday, 29 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff > -6 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Month"

            '''''''''''''''''''For Future Task,Next Month,Wednesday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 26 And diff > 19) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 19 And diff > 12) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 12 And diff > 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Wednesday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 25 And diff > 18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 18 And diff > 11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 11 And diff > 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Wednesday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 23 And diff > 16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 16 And diff > 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 9 And diff > 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Wednesday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 24 And diff > 17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 17 And diff > 10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 10 And diff > 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Month"

            ''''''''''''''''''''''''''For past Task , Previous Year'''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = -29 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -22 And diff > -29) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff > -8 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Month"


            '''''''''''''For Future task ,Next year,Wednesday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 29 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 28 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 27 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 26 And diff > 19) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 19 And diff > 12) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 12 And diff > 5) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff <= 5) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And Now.Year < selectedDate.Year And Now.Month = 12 Then

            Return "Beyond Next Month"

            ''''''''''''''''''''''''' For Past Task, Current Month, Wednesday''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff >= 3 And diff < 10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff >= 10 And diff < 17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff >= 17 And diff < 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff >= 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Starting this Month"

            '    Return "Four Weeks Ago"
            'ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And (diff >= 31 And diff < 38) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            '    Return "Five Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 2 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            'MessageBox.Show("Monday")
            Return "Monday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Wednesday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Today"



            '''''''''''''''''For  Future Task, current Month,Tuesday ''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = -2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Thursday" '"One Day Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = -3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Friday" '"One Two Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = -4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Saturday" '"One Three Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = -5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Sunday" ''"After Four Day"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Later this Month"
            'ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -27 And diff > -34) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            '    'MessageBox.Show("Four Weeks Away")
            '    Return "Four Weeks Away"
            'ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -34 And diff > -41) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then
            '    'MessageBox.Show("Five Weeks Away")
            '    Return "Five Weeks Away"

            '''''''''''''''''For  Past Task,for Previous Month,Tuesday ,30 days Month''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = -29 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then '''' New Start Tuesday

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -22 And diff > -29) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff > -8 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then
            'MessageBox.Show("Four Weeks Ago")
            Return "Last Month"

            '''''''''''''''''For  Past Task,for Previous Month,Tuesday ,31 days Month''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = -30 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -23 And diff > -30) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff > -9 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then '''' New End Tuesday

            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Tuesday, 28 days Month

        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = -27 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Yesterday"

        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -20 And diff > -27) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -13 And diff > -20) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -6 And diff > -13) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff > -6 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Tuesday, 29 days Month'''''''''''''''''''''''''''''''''''''''''''''

        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = -28 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Yesterday"

        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff > -7 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Month"

            '''''''''''''''''''For Future Task,Next Month,Tuesday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 25 And diff > 18) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 18 And diff > 11) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 11 And diff > 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Tuesday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 24 And diff > 17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 17 And diff > 10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 10 And diff > 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Tuesday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 22 And diff > 15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 15 And diff > 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 8 And diff > 1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Tuesday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 23 And diff > 16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 16 And diff > 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 9 And diff > 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Month"

            ''''''''''''''''''''''For Previous Year,Past Task,Tuesday'''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = -30 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -23 And diff > -30) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff > -9 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Month"


            '''''''''''''For Future task ,Next year,Tuesday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 29 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 28 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 27 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 26 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 25 And diff > 18) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 18 And diff > 11) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 11 And diff > 4) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff <= 4) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And Now.Year < selectedDate.Year And Now.Month = 12 Then

            Return "Beyond Next Month"

            ''''''''''''''''''''''For Past Task ,Current Month,Tuesday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff >= 2 And diff < 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff >= 9 And diff < 16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff >= 16 And diff < 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And (diff >= 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Starting this Month"

        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 1 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Yesterday"
        ElseIf Now.DayOfWeek = DayOfWeek.Tuesday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Today"

            '''''''''''''''' for Future Task,Current Month,Monday'''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -4) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -5) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Saturday" ''"After Four Day"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = -6) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Sunday" ''"After Five Day"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Later this Month"

            ''''''''''''''' For past Task, Previous Month,30 Days Month'''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -23 And diff > -30) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff > -9 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 30 Then

            Return "Last Month"


            ''''''''''''''' For past Task, Previous Month,31 Days Month'''''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -24 And diff > -31) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -17 And diff > -24) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -10 And diff > -17) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff > -10 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 31 Then '''' New End Monday

            Return "Last Month"



            ''''''''''''''' for Past Task Previous Month,Monday, 28 days Month
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -21 And diff > -28) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -14 And diff > -21) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -7 And diff > -14) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff > -7 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 28 Then

            Return "Last Month"


            ''''''''''''''' for Past Task Previous Month,Monday, 29 days Month'''''''''''''''''''''''''''''''''''''''''''''

        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -22 And diff > -29) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -15 And diff > -22) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -8 And diff > -15) And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff > -8 And Now.Year = selectedDate.Year And Now.Month = (selectedDate.Month) + 1 And Date.DaysInMonth(selectedDate.Year, selectedDate.Month) = 29 Then

            Return "Last Month"



            '''''''''''''''''''For Future Task,Next Month,Monday,31 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 30) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 24 And diff > 17) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 17 And diff > 10) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 10 And diff > 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 3) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 31 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Monday,30 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 29) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 23 And diff > 16) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 16 And diff > 9) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 9 And diff > 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 2) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 30 Then

            Return "Next Month"

            '''''''''''''''''''For Future Task,Next Month,Monday,28 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 21 And diff > 14) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 14 And diff > 7) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 7 And diff > 0) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 0) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 28 Then

            Return "Next Month"


            '''''''''''''''''''For Future Task,Next Month,Monday,29 Days Month'''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 28) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 27) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 26) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 25) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 24) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff = 23) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 22 And diff > 15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 15 And diff > 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 8 And diff > 1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 1) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month - 1 And Date.DaysInMonth(Now.Year, Now.Month) = 29 Then

            Return "Next Month"

            '''''''''''''''''''For Past Task,Past year,Monday''''''''''''''''''''''''''''''''

        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -23 And diff > -31) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -16 And diff > -23) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= -9 And diff > -16) And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff > -9 And Now.Year = selectedDate.Year + 1 And selectedDate.Month = 12 And Now.Month = 1 Then

            Return "Last Month"


            '''''''''''''For Future task ,Next year,Monday''''''''''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 30 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Tomorrow"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 29 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Wednesday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 28 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Thursday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 27 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Friday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 26 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Saturday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 25 And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Sunday"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 24 And diff > 17) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 17 And diff > 10) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Two Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 10 And diff > 3) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Three Weeks Away"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff <= 3) And Now.Year = selectedDate.Year - 1 And Now.Month = 12 And selectedDate.Month = 1 Then

            Return "Next Month"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And Now.Year < selectedDate.Year And Now.Month = 12 Then

            Return "Beyond Next Month"

            '''''''''''''''''''''For Past Task Current Month,Monday''''''''''''''''''''''''''''''
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff >= 1 And diff < 8) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Last Week"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff >= 8 And diff < 15) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Two Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff >= 15 And diff < 22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Three Weeks Ago"
        ElseIf Now.DayOfWeek = DayOfWeek.Monday And (diff >= 22) And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Starting this Month"

        ElseIf Now.DayOfWeek = DayOfWeek.Monday And diff = 0 And Now.Year = selectedDate.Year And Now.Month = selectedDate.Month Then

            Return "Today"
        Else

            Return "Older"
        End If
    End Function


    Private Sub trvCateogory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvCateogory.Click

    End Sub

    Private Sub trvCateogory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvCateogory.DoubleClick
        ' mySelectedNode = New myTreeNode
        mySelectedNode = trvCateogory.SelectedNode
        RaiseEvent trvCategoryDoublclick(sender, e)
        '   trvCateogory.ExpandAll()
    End Sub

    Private Sub trvCateogory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCateogory.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If _flag = Categorization.Tasks Then
                If trvCateogory.Nodes.Count = 0 Then
                    'Try
                    '    If (IsNothing(trvCateogory.ContextMenu) = False) Then
                    '        trvCateogory.ContextMenu.Dispose()
                    '        trvCateogory.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCateogory.ContextMenu = cmnuTasks
                    cmnuTask_Delete.Visible = False
                    cmnuTask_Complete.Visible = False
                    cmnuTask_Add.Visible = True
                    MenuItem1.Visible = False
                    MenuItem2.Visible = False
                End If
            ElseIf _flag = Categorization.Messages Then
                If trvCateogory.Nodes.Count = 0 Then
                    'Try
                    '    If (IsNothing(trvCateogory.ContextMenu) = False) Then
                    '        trvCateogory.ContextMenu.Dispose()
                    '        trvCateogory.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCateogory.ContextMenu = cmnuTasks
                    cmnuTask_Delete.Visible = False
                    cmnuTask_Complete.Visible = False
                    cmnuTask_Add.Visible = False
                    MenuItem1.Visible = True
                    MenuItem2.Visible = False
                End If
            Else
                If trvCateogory.Nodes.Count = 0 Then
                    'Try
                    '    If (IsNothing(trvCateogory.ContextMenu) = False) Then
                    '        trvCateogory.ContextMenu.Dispose()
                    '        trvCateogory.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCateogory.ContextMenu = Nothing
                End If
            End If
            '    mySelectedNode = New myTreeNode
            mySelectedNode = CType(trvCateogory.SelectedNode, myTreeNode)
            RaiseEvent trvCategoryMouseDown(sender, e)

        End If
    End Sub


    Private Sub trvCateogory_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvCateogory.NodeMouseClick

        If _flag = Categorization.Messages Or _flag = Categorization.Tasks Then

            If e.Button = Windows.Forms.MouseButtons.Right Then
                '     mySelectedNode = New myTreeNode()

                mySelectedNode = trvCateogory.GetNodeAt(e.X, e.Y) '' . SelectedNode
                trvCateogory.SelectedNode = mySelectedNode
                If IsNothing(mySelectedNode) = False Then

                    RaiseEvent trvSCatagaoryMouseNodeClick(sender, e)
                End If

            End If
        End If

        If e.Button = Windows.Forms.MouseButtons.Left Then
            '  mySelectedNode = New myTreeNode()
            mySelectedNode = trvCateogory.GetNodeAt(e.X, e.Y) '' . SelectedNode
            trvCateogory.SelectedNode = mySelectedNode
            If IsNothing(mySelectedNode) = False Then
                RaiseEvent trvSCatagaoryMouseNodeClick(sender, e)
            End If

        End If

    End Sub

    Private Sub cmnuTask_Complete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuTask_Complete.Click


        RaiseEvent CmnuTasks_complete(sender, e)
    End Sub

    Private Sub cmnuTask_Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuTask_Add.Click
        RaiseEvent cmnuTasks_click(sender, e)
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        ''context menu item click fr msgs
        RaiseEvent Msg_NewMEssages(sender, e)
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        RaiseEvent Msg_History(sender, e)
    End Sub
End Class
