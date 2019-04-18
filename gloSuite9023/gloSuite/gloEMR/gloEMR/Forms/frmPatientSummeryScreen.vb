Imports gloUserControlLibrary  'Shubhangi
Public Class frmPatientSummeryScreen
    Implements IPatientContext
    Private m_PatientID As Int64
    Dim oclsPatientSummry As clsPatientSummery
    Dim dt As DataTable
    Dim dvPatient As DataView
    Dim Phone_AS As String
    Dim DOB_AS As Date
    Dim ISDOB_AS As Boolean
    Dim IsSummeryModified As Boolean = False
    Dim lst As myList
    Dim ArrExam As ArrayList
    Dim ArrRadiology As ArrayList
    Dim ArrLabs As ArrayList
    Dim ArrScanDoc As ArrayList
    Dim dtExam As DataTable
    Dim dtRadiology As DataTable
    Dim dtLabs As DataTable
    Dim dtScanDoc As DataTable
    Dim arrDocuments As ArrayList
    '20090822 commented by Mayuri
    'Added Mobile and Employers Phone in Advanced Search
    Dim Mobile_AS As String
    Dim EmpPhone_AS As String
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip
    Private WithEvents oPatientListControl As gloPatient.PatientListControl
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    'Dim PatientName As String
    ''code added to avoid flickering -pradeep(03062011)
    ''http://www.ms-windows.info/Help/flicker-free-painting-11696.aspx -link referred
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub frmPatientSummeryScreen_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPatientSummeryScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        pnlMain.Padding = New Padding(0, 3, 0, 0)

        Try
            If m_PatientID = 0 Then
                MessageBox.Show("Please Select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Call Get_PatientDetails()
            ' m_PatientID = _PatientID
            Dim strPatientname As String = strPatientFirstName & " " & strPatientLastName ''& " " & gstrPatientMiddleName
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Commented by SHUBHANGI 
            'txtpatientName.Text = strPatientname
            'txtpatientName.Tag = m_PatientID
            '''' Fill Associate Node

            'SHUBHANGI 20090911 Use Patient Strip control
            Call Set_PatientDetailStrip(m_PatientID)

            Filltrv()
            ''''Fill Associate Patient Related inoformation
            FillAssocation()
            ''''Fill Exam with treeview for Patient
            FillExam()
            trvPatientAssoication.Select()
            trvPatientAssoication.Parent.Focus()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientMiddleName = Convert.ToString(dtPatient.Rows(0)("sMiddleName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
    End Sub
    'Shubhangi 20090911
    Private Sub Set_PatientDetailStrip(ByVal m_PatientID As Int64)
        ' '' Add Patient Details Control
        If Not IsNothing(gloUC_PatientStrip1) Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUC_PatientStrip
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.PatientSummary)
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        pnlToolStrip.SendToBack()
        pnlMain.BringToFront()
        pnlMain.Padding = New Padding(0, 3, 0, 0)

    End Sub

    Public Sub Filltrv()

        Dim PatientName As New myTreeNode

        ''PatientName.Nodes.Add(-1, txtpatientName.Text)
        PatientName.Text = gloUC_PatientStrip1.PatientName     'For taking Selected Patient Name
        PatientName.Key = -1
        PatientName.ImageIndex = 0
        PatientName.SelectedImageIndex = 0
        trvPatientAssoication.Nodes.Add(PatientName)
        Dim AssociatiType As myTreeNode

        AssociatiType = New myTreeNode
        AssociatiType.Key = 1
        AssociatiType.Text = "Exams"
        AssociatiType.ImageIndex = 1
        AssociatiType.SelectedImageIndex = 1
        PatientName.Nodes.Add(AssociatiType)

        AssociatiType = New myTreeNode
        AssociatiType.Key = 2
        AssociatiType.ImageIndex = 2
        AssociatiType.SelectedImageIndex = 2
        AssociatiType.Text = "Order Templates"
        PatientName.Nodes.Add(AssociatiType)

        AssociatiType = New myTreeNode
        AssociatiType.Key = 3
        AssociatiType.Text = "Orders and Results"
        AssociatiType.ImageIndex = 3
        AssociatiType.SelectedImageIndex = 3
        PatientName.Nodes.Add(AssociatiType)

        AssociatiType = New myTreeNode
        AssociatiType.Key = 4
        AssociatiType.ImageIndex = 4
        AssociatiType.SelectedImageIndex = 4
        AssociatiType.Text = "Scanned Documents"
        PatientName.Nodes.Add(AssociatiType)
        trvPatientAssoication.ExpandAll()


    End Sub

    Public Sub FillAssocation()
        Try
            oclsPatientSummry = New clsPatientSummery
            ''''Get Patient Association Information
            dt = New DataTable
            dt = oclsPatientSummry.GetPatientAssociation(m_PatientID)

            ''''Get Patient Exam
            dtExam = New DataTable
            dtExam = oclsPatientSummry.GetExamforPatient(m_PatientID)
            ''''Get Patient Orders
            dtRadiology = New DataTable
            dtRadiology = oclsPatientSummry.GetRadiologyOrderforPatient(m_PatientID)
            ''''Get Patient Labs Order
            dtLabs = New DataTable
            dtLabs = oclsPatientSummry.GetLabOrderforPatient(m_PatientID)
            ''''Get Patient Scan Document
            dtScanDoc = New DataTable
            'Added by Mayuri:20091027
            'To fix issue:#4532
            dtScanDoc = oclsPatientSummry.GetScanDocumentforPatient(m_PatientID)
            'End Code Added by Mayuri:20091027-"shows messages box-" Can not find column[Document]"

            arrDocuments = New ArrayList
            arrDocuments = GetPatientDocumentsArray()

            Dim NewNode As myTreeNode = Nothing
            Dim OrderNode As myTreeNode = Nothing
            Dim ResultNode As myTreeNode = Nothing
            If Not IsNothing(dt) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("nAssociateType") = PatientAssociatType.Exam Then ''''Fill Exam Node
                        For j As Integer = 0 To dtExam.Rows.Count - 1
                            If dt.Rows(i)("nAssociateID") = dtExam.Rows(j)("nExamID") Then
                                For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                                    If mynode.Text = "Exams" Then
                                        Dim IsFinish As String = ""
                                        Dim strProviderName As String = ""

                                        If dtExam.Rows(j)("bIsFinished") = True Then
                                            IsFinish = "Yes"
                                        Else
                                            IsFinish = "No"
                                        End If
                                        If (dtExam.Rows(j)("MiddleName") <> "") Then
                                            strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("MiddleName") & " " & dtExam.Rows(j)("LastName")
                                        Else
                                            strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("LastName")
                                        End If
                                        NewNode = New myTreeNode
                                        NewNode.Text = dtExam.Rows(j)("sExamName") & " - " & dtExam.Rows(j)("dtDOS") & " - " & IsFinish & " - " & strProviderName
                                        NewNode.Key = dtExam.Rows(j)("nExamID")
                                        NewNode.ImageIndex = 5
                                        NewNode.SelectedImageIndex = 5
                                        mynode.Nodes.Add(NewNode)
                                    End If
                                Next '''' Exam DataTable dtExam
                            End If
                        Next ''''Associate dataTable dt
                    ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.Radiology Then '''' Fill Orders Node
                        For j As Integer = 0 To dtRadiology.Rows.Count - 1
                            If dt.Rows(i)("nAssociateID") = dtRadiology.Rows(j)("lm_Order_ID") Then
                                For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                                    If mynode.Text = "Order Templates" Then
                                        Dim IsFinish As String = ""
                                        Dim strProviderName As String = ""
                                        If dtRadiology.Rows(j)("lm_IsFinished") = True Then
                                            IsFinish = "Yes"
                                        Else
                                            IsFinish = "No"
                                        End If
                                        If (dtRadiology.Rows(j)("MiddleName") <> "") Then
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("MiddleName") & " " & dtRadiology.Rows(j)("LastName")
                                        Else
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("LastName")
                                        End If
                                        NewNode = New myTreeNode
                                        NewNode.Text = dtRadiology.Rows(j)("lm_test_Name") & " - " & Format(dtRadiology.Rows(j)("lm_OrderDate"), "MM/dd/yyyy") & " - " & IsFinish & " - " & strProviderName  ''lm_OrderDate
                                        NewNode.Key = dtRadiology.Rows(j)("lm_Order_ID")
                                        NewNode.ImageIndex = 5
                                        NewNode.SelectedImageIndex = 5
                                        mynode.Nodes.Add(NewNode)
                                    End If
                                Next '''' Orders DataTable dtRadiology
                            End If
                        Next ''''Associate dataTable dt
                    ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.Labs Then '''' Fill Lab Node
                        Dim IsFirstNode As Boolean = True
                        For j As Integer = 0 To dtLabs.Rows.Count - 1

                            If dt.Rows(i)("nAssociateID") = dtLabs.Rows(j)("labom_OrderID") Then
                                For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                                    If mynode.Text = "Orders and Results" Then
                                        'NewNode = New myTreeNode
                                        'NewNode.Text = dtLabs.Rows(j)("labtm_Name")
                                        'NewNode.Key = dtLabs.Rows(j)("labom_OrderID")
                                        'NewNode.ImageIndex = 5
                                        'NewNode.SelectedImageIndex = 5
                                        'mynode.Nodes.Add(NewNode)
                                        Dim strProviderName As String = ""

                                        If IsFirstNode = True Then '''' If first Node then add Order No Row
                                            IsFirstNode = False
                                            OrderNode = New myTreeNode
                                            If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                            Else
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                            End If

                                            OrderNode.Text = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy") & " - " & strProviderName ''labom_TransactionDate       ''dtLabs.Rows(j)("labtm_Name")
                                            OrderNode.Key = dtLabs.Rows(j)("labom_OrderID")
                                            OrderNode.Tag = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID")
                                            OrderNode.ImageIndex = 6
                                            OrderNode.SelectedImageIndex = 6
                                            mynode.Nodes.Add(OrderNode)
                                            '''' Add Lab Node
                                            ResultNode = New myTreeNode
                                            ResultNode.Text = dtLabs.Rows(j)("labtm_Name")
                                            ResultNode.Tag = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID")
                                            ResultNode.Key = 0
                                            ResultNode.ImageIndex = 5
                                            ResultNode.SelectedImageIndex = 5
                                            OrderNode.Nodes.Add(ResultNode)
                                        Else

                                            If ResultNode.Parent.Tag <> dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") Then  '''' If new node is Order No then    
                                                If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                                    strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                                Else
                                                    strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                                End If

                                                OrderNode = New myTreeNode
                                                OrderNode.Text = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy") & " - " & strProviderName      ''dtLabs.Rows(j)("labtm_Name")
                                                OrderNode.Key = dtLabs.Rows(j)("labom_OrderID")
                                                OrderNode.Tag = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID")
                                                OrderNode.ImageIndex = 6
                                                OrderNode.SelectedImageIndex = 6
                                                mynode.Nodes.Add(OrderNode)
                                                ResultNode = New myTreeNode
                                                ResultNode.Text = dtLabs.Rows(j)("labtm_Name")
                                                ResultNode.Tag = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID")
                                                ResultNode.Key = 0
                                                ResultNode.ImageIndex = 5
                                                ResultNode.SelectedImageIndex = 5
                                                OrderNode.Nodes.Add(ResultNode)
                                            Else  '''' If with in same Order Number
                                                ResultNode = New myTreeNode
                                                ResultNode.Text = dtLabs.Rows(j)("labtm_Name")
                                                ResultNode.Tag = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID")
                                                ResultNode.Key = 0
                                                ResultNode.ImageIndex = 5
                                                ResultNode.SelectedImageIndex = 5
                                                OrderNode.Nodes.Add(ResultNode)
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.ScanDocument Then  '''' Fill Scan Document
                        '' COMMENT BY SUDHIR 20090717 ''
                        'For j As Integer = 0 To dtScanDoc.Rows.Count - 1
                        '    If dt.Rows(i)("nAssociateID") = dtScanDoc.Rows(j)("DocumentID") Then
                        '        For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                        '            If mynode.Text = "Scanned Documents" Then
                        '                NewNode = New myTreeNode
                        '                NewNode.Text = dtScanDoc.Rows(j)("DocumentName") & " - " & Format(dtScanDoc.Rows(j)("ModifyDateTime"), "MM/dd/yyyy")
                        '                NewNode.Key = dtScanDoc.Rows(j)("DocumentID")
                        '                NewNode.ImageIndex = 5
                        '                NewNode.SelectedImageIndex = 5
                        '                mynode.Nodes.Add(NewNode)
                        '            End If
                        '        Next '''' Associate dataTable dt
                        '    End If
                        'Next '''' Scan document datatable dtScanDOC


                        For j As Integer = 0 To arrDocuments.Count - 1
                            If dt.Rows(i)("nAssociateID") = CType(arrDocuments(j), myList).ID Then
                                For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                                    If mynode.Text = "Scanned Documents" Then
                                        NewNode = New myTreeNode
                                        NewNode.Text = CType(arrDocuments(j), myList).Value
                                        NewNode.Key = CType(arrDocuments(j), myList).ID
                                        NewNode.ImageIndex = 5
                                        NewNode.SelectedImageIndex = 5
                                        mynode.Nodes.Add(NewNode)
                                    End If
                                Next
                            End If
                        Next
                        '' END SUDHIR ''
                    End If

                Next
            End If
            trvPatientAssoication.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'For j As Integer = 0 To dtLabs.Rows.Count - 1
    '                       If dt.Rows(i)("nAssociateID") = dtLabs.Rows(j)("labom_OrderID") Then
    '                           For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
    '                               If mynode.Text = "Orders and Results" Then
    '                                   NewNode = New myTreeNode
    '                                   NewNode.Text = dtLabs.Rows(j)("labtm_Name")
    '                                   NewNode.Key = dtLabs.Rows(j)("labom_OrderID")
    '                                   NewNode.ImageIndex = 5
    '                                   NewNode.SelectedImageIndex = 5
    '                                   mynode.Nodes.Add(NewNode)
    '                               End If
    '                           Next
    '                       End If
    '                   Next

    Private Sub btnExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExam.Click
        
        pnlbtnExam.Dock = DockStyle.Top
        pnlbtnRadiology.Dock = DockStyle.Bottom
        pnlbtnLabs.Dock = DockStyle.Bottom
        pnlbtnScanDoc.Dock = DockStyle.Bottom
        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnExam.BackgroundImageLayout = ImageLayout.Stretch

        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

        trvAssociatType.Nodes.Clear()
        txtsearch.Select()
        FillExam()
        If gblnResetSearchTextBox = True Then
            txtsearch.ResetText()
        Else
            txtsearch_TextChanged(sender, e)
        End If
    End Sub

    Public Sub FillExam()
        Try
            oclsPatientSummry = New clsPatientSummery
            dt = New DataTable
            dt = oclsPatientSummry.GetExamforPatient(m_PatientID)
            InsertExam(dt)
            'Dim selectedNode As New myTreeNode

            'If Not IsNothing(dt) Then
            '    For Each mynode As myTreeNode In trvPatientAssoication.Nodes
            '        If mynode.Text = "Exam" Then
            '            For i As Integer = 0 To dt.Rows.Count - 1
            '                NewNode = New myTreeNode
            '                NewNode.Text = dt.Rows(i)("sExamName")
            '                NewNode.Key = dt.Rows(i)("nExamID")
            '                mynode.Nodes.Add(NewNode)
            '            Next
            '        End If
            '    Next
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub InsertExam(ByVal dt As DataTable)
        If Not IsNothing(dt) Then
            If dt.Rows.Count >= 0 Then
                trvAssociatType.Nodes.Clear()
            End If

            Dim ExamNode As myTreeNode
            Dim rootNode As New myTreeNode
            rootNode.Text = "Exams"
            rootNode.Key = -1
            rootNode.ImageIndex = 1
            rootNode.SelectedImageIndex = 1
            trvAssociatType.Nodes.Add(rootNode)
            For i As Integer = 0 To dt.Rows.Count - 1
                ExamNode = New myTreeNode
                ''bIsFinished
                Dim IsFinish As String = ""
                Dim strProviderName As String = ""

                If dt.Rows(i)("bIsFinished") = True Then
                    IsFinish = "Yes"
                Else
                    IsFinish = "No"
                End If
                If (dt.Rows(i)("MiddleName") <> "") Then
                    strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                Else
                    strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")
                End If
                ExamNode.Text = dt.Rows(i)("sExamName") & " - " & dt.Rows(i)("dtDOS") & " - " & IsFinish & " - " & strProviderName
                ExamNode.Key = dt.Rows(i)("nExamID")
                ExamNode.ImageIndex = 5
                ExamNode.SelectedImageIndex = 5
                rootNode.Nodes.Add(ExamNode)
            Next
        End If
        trvAssociatType.ExpandAll()
    End Sub

    Private Sub btnRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.Click
       
        pnlbtnRadiology.Dock = DockStyle.Top
        pnlbtnExam.Dock = DockStyle.Bottom
        pnlbtnLabs.Dock = DockStyle.Bottom
        pnlbtnScanDoc.Dock = DockStyle.Bottom

        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnExam.BackgroundImageLayout = ImageLayout.Stretch

        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

        trvAssociatType.Nodes.Clear()
        txtsearch.Select()
        FillRadiology()
        If gblnResetSearchTextBox = True Then
            txtsearch.ResetText()
        Else
            txtsearch_TextChanged(sender, e)
        End If

    End Sub

    Public Sub FillRadiology()
        Try
            dt = New DataTable
            oclsPatientSummry = New clsPatientSummery

            dt = oclsPatientSummry.GetRadiologyOrderforPatient(m_PatientID)
            InsertRadiology(dt)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'If Not IsNothing(dt) Then
        '    For Each mynode As myTreeNode In trvPatientAssoication.Nodes
        '        If mynode.Text = "Orders and Results" Then
        '            For i As Integer = 0 To dt.Rows.Count - 1
        '                NewNode = New myTreeNode
        '                NewNode.Text = dt.Rows(i)("lm_test_Name")
        '                NewNode.Key = dt.Rows(i)("lm_Order_ID")
        '                mynode.Nodes.Add(NewNode)
        '            Next
        '        End If
        '    Next
        'End If
    End Sub

    Public Sub InsertRadiology(ByVal dt As DataTable)
        If Not IsNothing(dt) Then
            If dt.Rows.Count >= 0 Then
                trvAssociatType.Nodes.Clear()
            End If
        End If


        Dim rootNode As New myTreeNode
        Dim radioNode As myTreeNode
        rootNode.Text = "Order Templates"
        rootNode.Key = -1
        rootNode.ImageIndex = 2
        rootNode.SelectedImageIndex = 2
        trvAssociatType.Nodes.Add(rootNode)
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
                If (dt.Rows(i)("MiddleName") <> "") Then
                    strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                Else
                    strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")
                End If
                radioNode = New myTreeNode
                radioNode.Text = dt.Rows(i)("lm_test_Name") & " - " & Format(dt.Rows(i)("lm_OrderDate"), "MM/dd/yyyy") & " - " & IsFinish & " - " & strProviderName
                radioNode.Key = dt.Rows(i)("lm_Order_ID")
                radioNode.ImageIndex = 5
                radioNode.SelectedImageIndex = 5
                rootNode.Nodes.Add(radioNode)
            Next
        End If
        trvAssociatType.ExpandAll()
    End Sub

    Private Sub btnLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        
        pnlbtnLabs.Dock = DockStyle.Top
        pnlbtnExam.Dock = DockStyle.Bottom
        pnlbtnRadiology.Dock = DockStyle.Bottom
        pnlbtnScanDoc.Dock = DockStyle.Bottom

        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnExam.BackgroundImageLayout = ImageLayout.Stretch

        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch
        trvAssociatType.Nodes.Clear()
        txtsearch.Select()
        FillLabs()
        If gblnResetSearchTextBox = True Then
            txtsearch.ResetText()
        Else
            txtsearch_TextChanged(sender, e)
        End If
    End Sub

    Public Sub FillLabs()
        Try
            dt = New DataTable
            oclsPatientSummry = New clsPatientSummery
            dt = oclsPatientSummry.GetLabOrderforPatient(m_PatientID)
            InsertLab(dt)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'If Not IsNothing(dt) Then
        '    For Each mynode As myTreeNode In trvPatientAssoication.Nodes
        '        If mynode.Text = "Orders and Results" Then
        '            For i As Integer = 0 To dt.Rows.Count - 1
        '                NewNode = New myTreeNode
        '                NewNode.Text = dt.Rows(i)("labtm_Name")
        '                NewNode.Key = dt.Rows(i)("labom_OrderID")
        '                mynode.Nodes.Add(NewNode)
        '            Next
        '        End If
        '    Next
        'End If
    End Sub

    Public Sub InsertLab(ByVal dt As DataTable)

        If Not IsNothing(dt) Then
            If dt.Rows.Count >= 0 Then
                trvAssociatType.Nodes.Clear()
            End If
            Dim rootNode As New myTreeNode
            Dim OrderNode As myTreeNode = Nothing
            Dim ResultNode As myTreeNode = Nothing
            rootNode.Text = "Orders and Results"
            rootNode.Key = -1
            rootNode.ImageIndex = 3
            rootNode.SelectedImageIndex = 3
            ''rootNode.Nodes.Add(-1, "Orders and Results")
            trvAssociatType.Nodes.Add(rootNode)
            Dim strProviderName As String
            For i As Integer = 0 To dt.Rows.Count - 1
                ''labom_OrderNoPrefix
                ''labom_OrderNoID
                If i = 0 Then
                    If (dt.Rows(i)("MiddleName") <> "") Then
                        strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")
                    Else
                        strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")
                    End If
                    OrderNode = New myTreeNode
                    OrderNode.Text = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") & " - " & Format(dt.Rows(i)("labom_TransactionDate"), "MM/dd/yyyy") & " - " & strProviderName        ''dt.Rows(i)("labtm_Name")
                    OrderNode.Key = dt.Rows(i)("labom_OrderID")
                    OrderNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                    OrderNode.ImageIndex = 6
                    OrderNode.SelectedImageIndex = 6
                    rootNode.Nodes.Add(OrderNode)
                    ResultNode = New myTreeNode
                    ResultNode.Text = dt.Rows(i)("labtm_Name")
                    ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                    ResultNode.Key = 0
                    ResultNode.ImageIndex = 5
                    ResultNode.SelectedImageIndex = 5
                    OrderNode.Nodes.Add(ResultNode)
                Else

                    If ResultNode.Parent.Tag <> dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") Then
                        If (dt.Rows(i)("MiddleName") <> "") Then
                            strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("MiddleName") & " " & dt.Rows(i)("LastName")  ''dt.Rows(i)("labtm_Name")
                        Else
                            strProviderName = dt.Rows(i)("FirstName") & " " & dt.Rows(i)("LastName")  ''dt.Rows(i)("labtm_Name")
                        End If
                        OrderNode = New myTreeNode
                        OrderNode.Text = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID") & " - " & Format(dt.Rows(i)("labom_TransactionDate"), "MM/dd/yyyy") & " - " & strProviderName        ''dt.Rows(i)("labtm_Name")
                        OrderNode.Key = dt.Rows(i)("labom_OrderID")
                        OrderNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                        OrderNode.ImageIndex = 6
                        OrderNode.SelectedImageIndex = 6
                        rootNode.Nodes.Add(OrderNode)
                        ResultNode = New myTreeNode
                        ResultNode.Text = dt.Rows(i)("labtm_Name")
                        ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                        ResultNode.Key = 0
                        ResultNode.ImageIndex = 5
                        ResultNode.SelectedImageIndex = 5
                        OrderNode.Nodes.Add(ResultNode)
                    Else
                        ResultNode = New myTreeNode
                        ResultNode.Text = dt.Rows(i)("labtm_Name")
                        ResultNode.Tag = dt.Rows(i)("labom_OrderNoPrefix") & "-" & dt.Rows(i)("labom_OrderNoID")
                        ResultNode.Key = 0
                        ResultNode.ImageIndex = 5
                        ResultNode.SelectedImageIndex = 5
                        OrderNode.Nodes.Add(ResultNode)
                    End If
                    End If
            Next
            trvAssociatType.ExpandAll()
        End If
    End Sub
    'Public Sub FillLabs()
    '    dt = New DataTable
    '    oclsPatientSummry = New clsPatientSummery
    '    dt = oclsPatientSummry.GetLabOrderforPatient(m_PatientID)
    '    trvAssociatType.Nodes.Clear()

    '    If Not IsNothing(dt) Then
    '        Dim rootNode As New myTreeNode
    '        Dim LabNode As myTreeNode
    '        rootNode.Text = "Orders and Results"
    '        rootNode.Key = -1
    '        rootNode.ImageIndex = 3
    '        rootNode.SelectedImageIndex = 3
    '        ''rootNode.Nodes.Add(-1, "Orders and Results")
    '        trvAssociatType.Nodes.Add(rootNode)
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            LabNode = New myTreeNode
    '            LabNode.Text = dt.Rows(i)("labtm_Name")
    '            LabNode.Key = dt.Rows(i)("labom_OrderID")
    '            LabNode.ImageIndex = 5
    '            LabNode.SelectedImageIndex = 5
    '            rootNode.Nodes.Add(LabNode)

    '        Next
    '    End If
    '    trvAssociatType.ExpandAll()
    '    'If Not IsNothing(dt) Then
    '    '    For Each mynode As myTreeNode In trvPatientAssoication.Nodes
    '    '        If mynode.Text = "Orders and Results" Then
    '    '            For i As Integer = 0 To dt.Rows.Count - 1
    '    '                NewNode = New myTreeNode
    '    '                NewNode.Text = dt.Rows(i)("labtm_Name")
    '    '                NewNode.Key = dt.Rows(i)("labom_OrderID")
    '    '                mynode.Nodes.Add(NewNode)
    '    '            Next
    '    '        End If
    '    '    Next
    '    'End If
    'End Sub
    Private Sub btnScanDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanDoc.Click
       
        pnlbtnScanDoc.Dock = DockStyle.Top
        pnlbtnExam.Dock = DockStyle.Bottom
        pnlbtnRadiology.Dock = DockStyle.Bottom
        pnlbtnLabs.Dock = DockStyle.Bottom

        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnExam.BackgroundImageLayout = ImageLayout.Stretch

        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch


        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

        trvAssociatType.Nodes.Clear()
        txtsearch.Select()
        FillScanDocument()
        If gblnResetSearchTextBox = True Then
            txtsearch.ResetText()
        Else
            txtsearch_TextChanged(sender, e)
        End If
    End Sub

    Public Sub FillScanDocument()
        Try
            'dt = New DataTable
            'oclsPatientSummry = New clsPatientSummery
            'dt = oclsPatientSummry.GetScanDocumentforPatient(m_PatientID)
            InsertScanDoc()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'If Not IsNothing(dt) Then
        '    For Each mynode As myTreeNode In trvPatientAssoication.Nodes
        '        If mynode.Text = "Orders and Results" Then
        '            For i As Integer = 0 To dt.Rows.Count - 1
        '                NewNode = New myTreeNode
        '                NewNode.Text = dt.Rows(i)("DocumentName")
        '                NewNode.Key = dt.Rows(i)("DocumentID")
        '                mynode.Nodes.Add(NewNode)
        '            Next
        '        End If
        '    Next
        'End If
    End Sub

    Public Sub InsertScanDoc()


        Dim oCategories As gloEDocumentV3.Common.Categories
        Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
        Dim oDocuments As gloEDocumentV3.Document.BaseDocuments

        oCategories = oList.GetCategories(gClinicID)
        If Not oCategories Is Nothing Then
            trvAssociatType.Nodes.Clear()
            Dim rootNode As New myTreeNode
            rootNode.Text = "Scanned Documents"
            rootNode.Key = -1
            rootNode.ImageIndex = 4
            rootNode.SelectedImageIndex = 4
            Dim ScanDocNode As myTreeNode
            ''rootNode.Nodes.Add(-1, "Scan Document")
            trvAssociatType.Nodes.Add(rootNode)
            For i As Int16 = 0 To oCategories.Count - 1
                'oDocuments = New gloEDocumentV3.Document.BaseDocuments()
                oDocuments = oList.GetBaseDocuments(m_PatientID, oCategories(i).CategoryName, gClinicID)
                If Not oDocuments Is Nothing Then
                    For k As Int16 = 0 To oDocuments.Count - 1
                        ScanDocNode = New myTreeNode
                        ScanDocNode.Text = oDocuments(k).DocumentName & " - " & Format(oDocuments(k).CreatedDateTime, "MM/dd/yyyy")    ''dt.Rows(i)("DocumentName") & " - " & Format(dt.Rows(i)("ModifyDateTime"), "MM/dd/yyyy")
                        ScanDocNode.Key = oDocuments(k).EDocumentID ''dt.Rows(i)("DocumentID")
                        ScanDocNode.ImageIndex = 5
                        ScanDocNode.SelectedImageIndex = 5
                        rootNode.Nodes.Add(ScanDocNode)
                    Next
                End If
                oDocuments.Dispose()
            Next
            oCategories.Dispose()
            oCategories = Nothing
        End If
        oList.Dispose()
        oList = Nothing
        trvAssociatType.ExpandAll()
    End Sub

    Public Function GetPatientDocumentsArray() As ArrayList
        Dim oCategories As gloEDocumentV3.Common.Categories
        Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
        Dim oDocuments As gloEDocumentV3.Document.BaseDocuments
        Dim arrDocs As New ArrayList
        Dim oMyList As myList

        Try
            oCategories = oList.GetCategories(gClinicID)
            If Not oCategories Is Nothing Then
                For i As Int16 = 0 To oCategories.Count - 1
                    ' oDocuments = New gloEDocumentV3.Document.BaseDocuments()
                    oDocuments = oList.GetBaseDocuments(m_PatientID, oCategories(i).CategoryName, gClinicID)
                    If Not oDocuments Is Nothing Then
                        For k As Int16 = 0 To oDocuments.Count - 1
                            oMyList = New myList
                            oMyList.Value = oDocuments(k).DocumentName & " - " & Format(oDocuments(k).CreatedDateTime, "MM/dd/yyyy")
                            oMyList.ID = oDocuments(k).EDocumentID
                            arrDocs.Add(oMyList)
                            oMyList = Nothing
                        Next
                    End If
                    oDocuments.Dispose()
                Next
                oCategories.Dispose()
                oCategories = Nothing
            End If
            oList.Dispose()
            oList = Nothing
            trvAssociatType.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
        Return arrDocs
    End Function

    Private Sub trvAssociatType_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvAssociatType.NodeMouseDoubleClick
        Try
            Dim selectedNode As myTreeNode
            selectedNode = trvAssociatType.SelectedNode
            ''Sanjog-Added on 11012011 to handle unselected treenode
            If Not IsNothing(selectedNode) Then
                If selectedNode.Key = -1 Then
                    trvAssociatType.ExpandAll()
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            ''Sanjog-Added on 11012011 to handle unselected treenode
            trvAssociatType.Nodes.Item(0).ExpandAll()
            Dim NewNode As myTreeNode
            For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                If mynode.Text = selectedNode.Parent.Text Then
                    If mynode.Nodes.Count <> 0 Then
                        Dim IsNodePresent As Boolean = False
                        For Each childNode As myTreeNode In mynode.Nodes
                            If childNode.Key <> 0 Then
                                If childNode.Key <> selectedNode.Key Then
                                    IsNodePresent = False
                                Else
                                    IsNodePresent = True
                                    Exit For
                                End If
                            End If

                        Next
                        If IsNodePresent = False Then
                            NewNode = New myTreeNode
                            NewNode = selectedNode.Clone
                            NewNode.Text = selectedNode.Text
                            NewNode.Key = selectedNode.Key
                            mynode.Nodes.Add(NewNode)
                            IsSummeryModified = True
                        End If
                    Else
                        NewNode = New myTreeNode
                        NewNode = selectedNode.Clone
                        NewNode.Text = selectedNode.Text
                        NewNode.Key = selectedNode.Key
                        mynode.Nodes.Add(NewNode)
                        IsSummeryModified = True
                    End If

                End If
            Next
            'Shubhangi 20091208
            'Check the setting Reset search text box after assiging category
            If gblnResetSearchTextBox = True Then
                txtsearch.ResetText()
            End If
            trvPatientAssoication.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    

    Private Sub GloPatientDataGrid_Cancel_Click() Handles GloPatientDataGrid.Cancel_Click
        Try
            'Me.Close()
            pnlPatientList.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            pnlPatientList.Visible = False
        End Try
    End Sub

    Private Sub GloPatientDataGrid_OK_Click() Handles GloPatientDataGrid.OK_Click
        Try
            'txtpatientName.Text = GloPatientDataGrid.FirstName & " " & GloPatientDataGrid.LastName

            txtpatientName.Tag = GloPatientDataGrid.PatientID

            'SHUBHANGI 20090912
            'for calling Patient Strip after changing Patient
            Set_PatientDetailStrip(GloPatientDataGrid.PatientID)     'Pass the parameter as ID of seleted patient from control GloPatient Data Grid

            'Panel4.Visible = False
            If GloPatientDataGrid.PatientID = 0 Then
                MessageBox.Show("Please select patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If GloPatientDataGrid.PatientID <> m_PatientID Then
                If IsSummeryModified = True Then
                    If MessageBox.Show("Patient Summary for Patient is Changed, Do you want to Save?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                        SaveAssociation()
                        trvPatientAssoication.Nodes.Clear()
                        Filltrv()
                        trvAssociatType.Nodes.Clear()
                        m_PatientID = GloPatientDataGrid.PatientID

                        pnlbtnExam.Dock = DockStyle.Top
                        pnlbtnRadiology.Dock = DockStyle.Bottom
                        pnlbtnLabs.Dock = DockStyle.Bottom
                        pnlbtnScanDoc.Dock = DockStyle.Bottom

                        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                        btnExam.BackgroundImageLayout = ImageLayout.Stretch

                        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                        trvAssociatType.Nodes.Clear()
                        txtsearch.Select()
                        FillExam()
                        FillAssocation()
                    Else
                        trvPatientAssoication.Nodes.Clear()

                        Filltrv()
                        trvAssociatType.Nodes.Clear()
                        m_PatientID = GloPatientDataGrid.PatientID

                        pnlbtnExam.Dock = DockStyle.Top
                        pnlbtnRadiology.Dock = DockStyle.Bottom
                        pnlbtnLabs.Dock = DockStyle.Bottom
                        pnlbtnScanDoc.Dock = DockStyle.Bottom

                        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                        btnExam.BackgroundImageLayout = ImageLayout.Stretch

                        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                        trvAssociatType.Nodes.Clear()
                        txtsearch.Select()
                        FillExam()
                        FillAssocation()
                    End If
                Else
                    trvPatientAssoication.Nodes.Clear()
                    Filltrv()
                    trvAssociatType.Nodes.Clear()
                    m_PatientID = GloPatientDataGrid.PatientID

                    pnlbtnExam.Dock = DockStyle.Top
                    pnlbtnRadiology.Dock = DockStyle.Bottom
                    pnlbtnLabs.Dock = DockStyle.Bottom
                    pnlbtnScanDoc.Dock = DockStyle.Bottom

                    btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                    btnExam.BackgroundImageLayout = ImageLayout.Stretch

                    btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                    btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                    btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                    trvAssociatType.Nodes.Clear()
                    txtsearch.Select()
                    FillExam()
                    FillAssocation()
                End If
                IsSummeryModified = False
                'Set_PatientDetailStrip(m_PatientID)
            End If

            'Me.Close()
            pnlPatientList.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            pnlPatientList.Visible = False
        End Try
    End Sub

    Private Sub GloPatientDataGrid_PicAdv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloPatientDataGrid.PicAdv_Click
        Try
            Dim RowFilter As String = ""
            'Dim DVMain As DataView

            Dim frm As New frmAdvancedSearch
            With frm
                .Phone = Phone_AS
                .ISDOB = ISDOB_AS
                .DOB = DOB_AS
                '20090822 commented by Mayuri
                .Mobile = Mobile_AS
                .EmpPhone = EmpPhone_AS

                Select Case Trim(GloPatientDataGrid.lblSearchCriteria.Text)
                    Case "Patient ID"
                        .Code = GloPatientDataGrid.txtSearchPatient.Text.Trim
                    Case "First Name"
                        .FName = GloPatientDataGrid.txtSearchPatient.Text.Trim
                    Case "Last Name"
                        .LName = GloPatientDataGrid.txtSearchPatient.Text.Trim
                    Case "SSN No"
                        .SSN = GloPatientDataGrid.txtSearchPatient.Text.Trim
                End Select
                ''''
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                '' Set Values For Update
                Phone_AS = .Phone
                ISDOB_AS = .ISDOB
                DOB_AS = .DOB
                Mobile_AS = .Mobile
                EmpPhone_AS = .EmpPhone

                '''''''''
                'If .ISDOB = True Then
                '    ISDOB = True
                '    DOB = .DOB()
                'End If
                'Phone = .Phone

                dvPatient = CType(GloPatientDataGrid.dgPatient.DataSource, DataView)
                If (IsNothing(dvPatient) = False) Then


                    If .Code <> "" Then ''PatientCode
                        RowFilter = GetRowFilter(RowFilter)
                        If .Code.StartsWith("%") = True Or .Code.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '%" & .Code & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '" & .Code & "%'"
                        End If
                    End If


                    If .FName <> "" Then ''PatientFirstName
                        RowFilter = GetRowFilter(RowFilter)
                        If .FName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strFirstName = Mid(.FName, 1, .FName.IndexOf(","))
                            strLastName = Mid(.FName, .FName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .FName.StartsWith("%") = True Or .FName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '%" & .FName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & .FName & "%'"
                            End If
                        End If
                    End If

                    If .LName <> "" Then ''PatientLastName
                        RowFilter = GetRowFilter(RowFilter)
                        If .LName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.LName, 1, .LName.IndexOf(","))
                            strFirstName = Mid(.LName, .LName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .LName.StartsWith("%") = True Or .LName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '%" & .LName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & .LName & "%'"
                            End If
                        End If
                    End If

                    If .SSN <> "" And IsNumeric(.SSN) = True Then ''SSNNo  ''PatientDOB '' Phone
                        RowFilter = GetRowFilter(RowFilter)
                        RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & "=" & .SSN
                        'Else
                        '    RowFilter = GetRowFilter(RowFilter)
                        '    RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & " Like '%'"
                    End If

                    If .Phone <> "" Then ''PatientDOB '' Phone
                        RowFilter = GetRowFilter(RowFilter)
                        If .Phone.StartsWith("%") = True Or .Phone.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '%" & .Phone & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '" & .Phone & "%'"
                        End If
                    End If

                    ''20090822 commented by Mayuri
                    If .Mobile <> "" Then  '' Mobile
                        RowFilter = GetRowFilter(RowFilter)
                        If .Mobile.StartsWith("%") = True Or .Mobile.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMobile").ColumnName & " Like '%" & .Mobile & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMobile").ColumnName & " Like '" & .Mobile & "%'"
                        End If
                    End If
                    If .EmpPhone <> "" Then  '' Employer's Phone
                        RowFilter = GetRowFilter(RowFilter)
                        If .EmpPhone.StartsWith("%") = True Or .EmpPhone.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sWorkPhone").ColumnName & " Like '%" & .EmpPhone & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sWorkPhone").ColumnName & " Like '" & .EmpPhone & "%'"
                        End If
                    End If



                    If .ISDOB = True And IsDate(.DOB) = True Then
                        RowFilter = GetRowFilter(RowFilter)
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientDOB").ColumnName & "= '" & .DOB & "'"
                    End If


                    '''' For Search on Gardian's Info
                    '''' 20070128
                    If .IsGuardianinfo = True Then
                        ''sMother_fName,  sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
                        ''''
                        If .MotherFirstName <> "" Then  ''MotherFirstName
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherFirstName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.MotherFirstName, 1, .MotherFirstName.IndexOf(","))
                                strFirstName = Mid(.MotherFirstName, .MotherFirstName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .MotherFirstName.StartsWith("%") = True Or .MotherFirstName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '%" & .MotherFirstName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & .MotherFirstName & "%'"
                                End If
                            End If
                        End If
                        ''''
                        If .MotherLastName <> "" Then   ''MotherLastName
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherLastName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.MotherLastName, 1, .MotherLastName.IndexOf(","))
                                strFirstName = Mid(.MotherLastName, .MotherLastName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .MotherLastName.StartsWith("%") = True Or .MotherLastName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '%" & .MotherLastName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & .MotherLastName & "%'"
                                End If
                            End If
                        End If

                        If .MotherCellNo <> "" Then  ''MotherCellNo '' sMother_Mobile
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherCellNo.StartsWith("%") = True Or .MotherCellNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '%" & .MotherCellNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '" & .MotherCellNo & "%'"
                            End If
                        End If

                        If .MotherPhoneNo <> "" Then  ''MotherPhoneNo '' sMother_Phone
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherPhoneNo.StartsWith("%") = True Or .MotherPhoneNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '%" & .MotherPhoneNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '" & .MotherPhoneNo & "%'"
                            End If
                        End If
                        '''''

                        '''''
                        If .FatherFirstName <> "" Then   '' FatherFirstName , sFather_fName
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherFirstName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.FatherFirstName, 1, .FatherFirstName.IndexOf(","))
                                strFirstName = Mid(.FatherFirstName, .FatherFirstName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .FatherFirstName.StartsWith("%") = True Or .FatherFirstName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '%" & .FatherFirstName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & .FatherFirstName & "%'"
                                End If
                            End If
                        End If
                        ''''
                        If .FatherLastName <> "" Then    ''FatherLastName, sFather_lName
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherLastName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.FatherLastName, 1, .FatherLastName.IndexOf(","))
                                strFirstName = Mid(.FatherLastName, .FatherLastName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .FatherLastName.StartsWith("%") = True Or .FatherLastName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '%" & .FatherLastName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & .FatherLastName & "%'"
                                End If
                            End If
                        End If

                        If .FatherCellNo <> "" Then   ''FatherCellNo ''  sFather_Mobile
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherCellNo.StartsWith("%") = True Or .FatherCellNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '%" & .FatherCellNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '" & .FatherCellNo & "%'"
                            End If
                        End If

                        If .FatherPhoneNo <> "" Then   ''FatherPhoneNo '' sFather_Phone
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherPhoneNo.StartsWith("%") = True Or .FatherPhoneNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '%" & .FatherPhoneNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '" & .FatherPhoneNo & "%'"
                            End If
                        End If

                    End If
                End If
            End With
            frm.Dispose()
            frm = Nothing
            Me.Cursor = Cursors.WaitCursor
            'Dim dvPatient As DataView

            'dgPatient.DataSource = dvPatient

            If RowFilter <> "" Then
                If (IsNothing(dvPatient) = False) Then
                    dvPatient.RowFilter = RowFilter
                End If

            End If



            Me.Cursor = Cursors.Default

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetRowFilter(ByVal RowFilter As String) As String
        If RowFilter = "" Then
            Return RowFilter
        Else
            Return RowFilter & " AND "
        End If
    End Function

    Private Sub tlsPatientSummary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientSummary.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveAssociation()
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Case "Refresh"
                    Refresh()
                Case "Close"
                    If IsSummeryModified = True Then
                        If MessageBox.Show("Patient Summary for Patient is Changed, Do you want to Save?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                            SaveAssociation()
                            '  Me.Close()
                            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                        Else
                            ' Me.Close()
                            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                        End If
                    Else
                        ' Me.Close()
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    End If
                Case "ChangePatient"
                    pnlPatientList.Visible = True
                    If IsNothing(oPatientListControl) = False Then
                        If pnlPatientList.Contains(oPatientListControl) = True Then
                            pnlPatientList.Controls.Remove(oPatientListControl)
                        End If
                        'If IsNothing(oPatientListControl.pnlToolstrip) = False Then
                        '    oPatientListControl.pnlToolstrip.Visible = True
                        'End If
                        Try
                            RemoveHandler oPatientListControl.Grid_MouseDown, AddressOf oPatientListControl_Grid_MouseDown
                        Catch ex As Exception

                        End Try
                        oPatientListControl.Dispose()
                        oPatientListControl = Nothing
                    End If

                   

                    'Declare Variable for connection string
                    oPatientListControl = New gloPatient.PatientListControl()
                    oPatientListControl.Dock = DockStyle.Fill
                    oPatientListControl.ClinicID = 1
                    oPatientListControl.DatabaseConnection = GetConnectionString()
                    'oPatientListControl.SelectedPatientID = gnPatientID
                    oPatientListControl.FillPatients()
                    ''End code Added by Mayuri:20100212

                    'Dim intControlHeight As Integer
                    'intControlHeight = Me.Height - pnl_tlsp_Top.Height
                    'pnlPatientListView.Height = intControlHeight / 2
                    'Add octlPatientList into pnlPatientListView
                    oPatientListControl.ShowOKCancel(True)
                    pnlPatientList.Controls.Add(oPatientListControl)
                    pnlPatientList.Dock = DockStyle.Top
                    AddHandler oPatientListControl.Grid_MouseDown, AddressOf oPatientListControl_Grid_MouseDown

                    
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Save Associated Exam,Orders,Labs and Scan Document 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Sub SaveAssociation()
        Try
            If trvPatientAssoication.Nodes.Count > 0 Then
                oclsPatientSummry = New clsPatientSummery
                ArrExam = New ArrayList
                ArrLabs = New ArrayList
                ArrRadiology = New ArrayList
                ArrScanDoc = New ArrayList
                For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                    If mynode.Text = "Exams" Then
                        For Each mychildNode As myTreeNode In mynode.Nodes
                            lst = New myList
                            lst.ParameterName = mychildNode.Text
                            lst.ID = mychildNode.Key
                            ArrExam.Add(lst)
                        Next
                    ElseIf mynode.Text = "Order Templates" Then
                        For Each mychildNode As myTreeNode In mynode.Nodes
                            lst = New myList
                            lst.ParameterName = mychildNode.Text
                            lst.ID = mychildNode.Key
                            ArrRadiology.Add(lst)
                        Next
                    ElseIf mynode.Text = "Orders and Results" Then
                        For Each mychildNode As myTreeNode In mynode.Nodes
                            lst = New myList
                            lst.ParameterName = mychildNode.Text
                            lst.ID = mychildNode.Key
                            ArrLabs.Add(lst)
                        Next
                    ElseIf mynode.Text = "Scanned Documents" Then
                        For Each mychildNode As myTreeNode In mynode.Nodes
                            lst = New myList
                            lst.ParameterName = mychildNode.Text
                            lst.ID = mychildNode.Key
                            ArrScanDoc.Add(lst)
                        Next
                    End If
                Next
                oclsPatientSummry.InsertAssociation(m_PatientID, ArrExam, ArrRadiology, ArrLabs, ArrScanDoc)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Refresh()
        trvPatientAssoication.Nodes.Clear()
        trvAssociatType.Nodes.Clear()

        pnlbtnExam.Dock = DockStyle.Top
        pnlbtnRadiology.Dock = DockStyle.Bottom
        pnlbtnLabs.Dock = DockStyle.Bottom
        pnlbtnScanDoc.Dock = DockStyle.Bottom

        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnExam.BackgroundImageLayout = ImageLayout.Stretch
        btnExam.BackColor = Color.FromArgb(255, 197, 108)

        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        btnRadiology.BackColor = Color.FromArgb(181, 216, 242)

        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        btnLabs.BackColor = Color.FromArgb(181, 216, 242)

        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch
        btnScanDoc.BackColor = Color.FromArgb(181, 216, 242)

        IsSummeryModified = False
        Filltrv()
        FillAssocation()
        FillExam()
    End Sub

    Private Sub trvPatientAssoication_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPatientAssoication.MouseClick

    End Sub

    Private Sub trvPatientAssoication_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPatientAssoication.MouseDown
        Try
            trvPatientAssoication.Select()
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Try
                '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                '        trvPatientAssoication.ContextMenuStrip.Dispose()
                '        trvPatientAssoication.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvPatientAssoication.ContextMenuStrip = Nothing

                Dim selectedNode As myTreeNode
                selectedNode = trvPatientAssoication.GetNodeAt(e.X, e.Y)
                If IsNothing(selectedNode) Then
                    Exit Sub
                End If
                trvPatientAssoication.SelectedNode = selectedNode

                ''trvPatientAssoication.Select()
                'Dim selectedNode As New myTreeNode
                'selectedNode = trvPatientAssoication.SelectedNode
                If selectedNode.Key = -1 Or selectedNode.Key = 1 Or selectedNode.Key = 2 Or selectedNode.Key = 3 Or selectedNode.Key = 4 Then
                    'Try
                    '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                    '        trvPatientAssoication.ContextMenuStrip.Dispose()
                    '        trvPatientAssoication.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvPatientAssoication.ContextMenuStrip = Nothing
                    Exit Sub
                Else
                    If selectedNode.Parent.Text = "Exams" Then
                        mnuDelete.Text = "Delete Exam"
                        'Try
                        '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                        '        trvPatientAssoication.ContextMenuStrip.Dispose()
                        '        trvPatientAssoication.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPatientAssoication.ContextMenuStrip = cntPatientAssociation
                    ElseIf selectedNode.Parent.Text = "Order Templates" Then
                        mnuDelete.Text = "Delete Orders"
                        'Try
                        '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                        '        trvPatientAssoication.ContextMenuStrip.Dispose()
                        '        trvPatientAssoication.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPatientAssoication.ContextMenuStrip = cntPatientAssociation
                    ElseIf selectedNode.Parent.Text = "Orders and Results" Then
                        mnuDelete.Text = "Delete Lab"
                        'Try
                        '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                        '        trvPatientAssoication.ContextMenuStrip.Dispose()
                        '        trvPatientAssoication.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPatientAssoication.ContextMenuStrip = cntPatientAssociation
                    ElseIf selectedNode.Parent.Text = "Scanned Documents" Then
                        mnuDelete.Text = "Delete Scanned Doucment"
                        'Try
                        '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                        '        trvPatientAssoication.ContextMenuStrip.Dispose()
                        '        trvPatientAssoication.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPatientAssoication.ContextMenuStrip = cntPatientAssociation
                    Else
                        'Try
                        '    If (IsNothing(trvPatientAssoication.ContextMenuStrip) = False) Then
                        '        trvPatientAssoication.ContextMenuStrip.Dispose()
                        '        trvPatientAssoication.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPatientAssoication.ContextMenuStrip = Nothing
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        trvPatientAssoication.SelectedNode.Remove()
        IsSummeryModified = True
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Dim strSearchDetails As String
        If Trim(txtsearch.Text) <> "" Then
            strSearchDetails = Replace(txtsearch.Text, "'", "''")
            strSearchDetails = Replace(strSearchDetails, "[", "") & ""
            strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
        Else
            strSearchDetails = ""
        End If

        If pnlbtnExam.Dock = DockStyle.Top Then
            AddSearchAssociates(Trim(strSearchDetails), dtExam)
            Exit Sub
        ElseIf pnlbtnRadiology.Dock = DockStyle.Top Then
            AddSearchAssociates(Trim(strSearchDetails), dtRadiology)
            Exit Sub
        ElseIf pnlbtnLabs.Dock = DockStyle.Top Then
            AddSearchAssociates(Trim(strSearchDetails), dtLabs)
            Exit Sub
        ElseIf pnlbtnScanDoc.Dock = DockStyle.Top Then
            AddSearchAssociates(Trim(strSearchDetails), dtScanDoc)
            Exit Sub
        End If

        'If btnTags.Dock = DockStyle.Top Then
        '    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 3)
        '    Exit Sub
        'ElseIf btnPatientEducation.Dock = DockStyle.Top Then
        '    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 4)
        '    Exit Sub
        'End If


        ''If Trim(txtsearchAssociates.Text) <> "" Then
        'If btnDrugs.Dock = DockStyle.Top Then
        '    If Len(Trim(strSearchDetails)) <= 1 Then
        '        If txtsearchAssociates.Tag <> Trim(strSearchDetails) Then

        '            PopulateAssociates(0, Trim(strSearchDetails))
        '            txtsearchAssociates.Tag = Trim(strSearchDetails)
        '        End If
        '    End If
        'End If
        'Dim mychildnode As myTreeNode
        ''child node collection
        'For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
        '    'compare selected node text and entered text
        '    Dim str As String
        '    str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(strSearchDetails))))
        '    If str = UCase(Trim(strSearchDetails)) Then
        '        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
        '        trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
        '        '*************
        '        trAssociates.SelectedNode = mychildnode
        '        txtsearchAssociates.Focus()
        '        Exit Sub
        '    End If
        'Next

        ' '' 20070922 - Mahesh - InString Searching 
        ''child node collection
        'For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
        '    'compare selected node text and entered text
        '    Dim str As String

        '    If InStr(UCase(Trim(mychildnode.Tag)), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
        '        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
        '        trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
        '        '*************
        '        trAssociates.SelectedNode = mychildnode
        '        txtsearchAssociates.Focus()
        '        Exit Sub
        '    End If
        'Next
        'End If
        'End If
    End Sub

    Public Sub AddSearchAssociates(ByVal strSearch As String, ByVal dt As DataTable)

        Try
            'Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            If pnlbtnExam.Dock = DockStyle.Top Then
                dv.RowFilter = "sExamName Like '%" & strSearch & "%'"
                tdt = New DataTable
                tdt = dv.ToTable
                InsertExam(tdt)
            ElseIf pnlbtnRadiology.Dock = DockStyle.Top Then
                dv.RowFilter = "lm_test_Name Like '%" & strSearch & "%'"
                tdt = New DataTable
                If tdt.Rows.Count > 0 Then
                    tdt = dv.ToTable
                Else
                    Exit Sub
                End If
                InsertRadiology(tdt)
                ElseIf pnlbtnLabs.Dock = DockStyle.Top Then
                    dv.RowFilter = "labtm_Name Like '%" & strSearch & "%'"
                    tdt = New DataTable
                    tdt = dv.ToTable
                    InsertLab(tdt)
                ElseIf pnlbtnScanDoc.Dock = DockStyle.Top Then
                    dv.RowFilter = "DocumentName Like '%" & strSearch & "%'"
                    tdt = New DataTable
                    tdt = dv.ToTable
                    ''InsertScanDoc() ''(tdt)
                    SearchScanDoc(strSearch)
                End If

                'tdt = New DataTable
                'tdt = dv.ToTable

                ''add the nodes to treenode
                'trAssociates.BeginUpdate()
                'trAssociates.Nodes(0).Nodes.Clear()
                'trAssociates.Visible = False

                'For i = 0 To tdt.Rows.Count - 1
                '    Dim mychildnode As myTreeNode
                '    If type = 1 Then
                '        'ICD9
                '        trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(2), String)))

                '    ElseIf type = 2 Then
                '        'Drugs
                '        trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1) & "-" & tdt.Rows(i)(2), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                '    Else
                '        '3 and 4
                '        'tags and PE
                '        trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                '    End If
                '    'rootnode.Nodes.Add(dt.Rows(i)(1))
                'Next
                'trAssociates.Visible = True
                'trAssociates.ExpandAll()
                'trAssociates.Select()
                'trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
                'txtsearchAssociates.Focus()

        Catch ex As Exception
            'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            'objex.ErrorMessage = ""
            'Throw objex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Public Sub SearchScanDoc(ByVal Search As String)
        If Trim(Search) <> "" Then
            If trvAssociatType.Nodes.Item(0).GetNodeCount(False) > 0 Then
                ''Dim mychildnode As myTreeNode
                'child node collection
                For Each mychildnode As myTreeNode In trvAssociatType.Nodes.Item(0).Nodes
                    'search against Description
                    'Dim strcode As String
                    If UCase(Mid(mychildnode.Text, 1, Len(Trim(Search)))) = UCase(Trim(Search)) Then
                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                        ''trvAssociatType.SelectedNode = trvAssociatType.SelectedNode.LastNode
                        '*************
                        'select matching node
                        trvAssociatType.SelectedNode = mychildnode
                        txtsearch.Focus()
                        Exit Sub
                    End If

                Next
            End If
        End If
    End Sub

    Private Sub trvPatientAssoication_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvPatientAssoication.NodeMouseDoubleClick
        trvPatientAssoication.ExpandAll()
    End Sub

    ''Button mouse Hover and Leave image............Ojeswini03122009
    Private Sub btnScanDoc_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScanDoc.MouseHover
        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnScanDoc_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScanDoc.MouseLeave
        If pnlbtnScanDoc.Dock = DockStyle.Top Then
            btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnRadiology_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseHover
        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRadiology_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseLeave
        If pnlbtnRadiology.Dock = DockStyle.Top Then
            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnLabs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseHover
        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLabs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseLeave
        If pnlbtnLabs.Dock = DockStyle.Top Then
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnExam_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExam.MouseHover
        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnExam.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnExam_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExam.MouseLeave
        If pnlbtnExam.Dock = DockStyle.Top Then
            btnExam.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnExam.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnExam.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnExam.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnchangepatient_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangepatient.MouseHover
        btnchangepatient.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnchangepatient.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnchangepatient_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangepatient.MouseLeave
        btnchangepatient.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnchangepatient.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub GloPatientDataGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GloPatientDataGrid.Load

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091001
        'USE CLEAR BUTTON TO CLEAR SEARCH TEXT BOX
        txtsearch.ResetText()
        txtsearch.Focus()
        trvAssociatType.SelectedNode = trvAssociatType.Nodes(0)
        ' trvAssociatType.Select()
    End Sub

    Private Sub oPatientListControl_Grid_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles oPatientListControl.Grid_DoubleClick
        Try
            'txtpatientName.Text = GloPatientDataGrid.FirstName & " " & GloPatientDataGrid.LastName
            CType(Me.ParentForm, MainMenu).SetGnPatientID = oPatientListControl.SelectedPatientID
            txtpatientName.Tag = oPatientListControl.SelectedPatientID

            'SHUBHANGI 20090912
            'for calling Patient Strip after changing Patient
            Set_PatientDetailStrip(oPatientListControl.SelectedPatientID)     'Pass the parameter as ID of seleted patient from control GloPatient Data Grid
            Try
                Me.Text = "Patient Summary"
                gloPatient.gloPatient.GetWindowTitle(Me, oPatientListControl.SelectedPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Panel4.Visible = False
            If oPatientListControl.SelectedPatientID = 0 Then
                MessageBox.Show("Please select patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If oPatientListControl.SelectedPatientID <> m_PatientID Then
                If IsSummeryModified = True Then
                    If MessageBox.Show("Patient summary for patient is changed, do you want to save?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                        SaveAssociation()
                        trvPatientAssoication.Nodes.Clear()
                        Filltrv()
                        trvAssociatType.Nodes.Clear()
                        m_PatientID = oPatientListControl.SelectedPatientID

                        pnlbtnExam.Dock = DockStyle.Top
                        pnlbtnRadiology.Dock = DockStyle.Bottom
                        pnlbtnLabs.Dock = DockStyle.Bottom
                        pnlbtnScanDoc.Dock = DockStyle.Bottom

                        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                        btnExam.BackgroundImageLayout = ImageLayout.Stretch

                        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                        trvAssociatType.Nodes.Clear()
                        txtsearch.Select()
                        FillExam()
                        FillAssocation()
                    Else
                        trvPatientAssoication.Nodes.Clear()

                        Filltrv()
                        trvAssociatType.Nodes.Clear()
                        m_PatientID = oPatientListControl.SelectedPatientID

                        pnlbtnExam.Dock = DockStyle.Top
                        pnlbtnRadiology.Dock = DockStyle.Bottom
                        pnlbtnLabs.Dock = DockStyle.Bottom
                        pnlbtnScanDoc.Dock = DockStyle.Bottom

                        btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                        btnExam.BackgroundImageLayout = ImageLayout.Stretch

                        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                        btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                        trvAssociatType.Nodes.Clear()
                        txtsearch.Select()
                        FillExam()
                        FillAssocation()
                    End If
                Else
                    trvPatientAssoication.Nodes.Clear()
                    Filltrv()
                    trvAssociatType.Nodes.Clear()
                    m_PatientID = oPatientListControl.SelectedPatientID

                    pnlbtnExam.Dock = DockStyle.Top
                    pnlbtnRadiology.Dock = DockStyle.Bottom
                    pnlbtnLabs.Dock = DockStyle.Bottom
                    pnlbtnScanDoc.Dock = DockStyle.Bottom

                    btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
                    btnExam.BackgroundImageLayout = ImageLayout.Stretch

                    btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

                    btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnLabs.BackgroundImageLayout = ImageLayout.Stretch

                    btnScanDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnScanDoc.BackgroundImageLayout = ImageLayout.Stretch

                    trvAssociatType.Nodes.Clear()
                    txtsearch.Select()
                    FillExam()
                    FillAssocation()
                End If
                IsSummeryModified = False
                'Set_PatientDetailStrip(m_PatientID)
            End If

            'Me.Close()
            pnlPatientList.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            pnlPatientList.Visible = False
        End Try
    End Sub

    Private Sub oPatientListControl_Grid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) ''Handles oPatientListControl.Grid_MouseDown

    End Sub

    Private Sub oPatientListControl_GridRowSelect_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles oPatientListControl.GridRowSelect_Click
       
    End Sub

    Private Sub oPatientListControl_ItemClosedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oPatientListControl.ItemClosedClick
        pnlPatientList.Visible = False
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        
        ' Add any initialization after the InitializeComponent() call.
        m_PatientID = PatientID
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class