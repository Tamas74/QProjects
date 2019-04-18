
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports C1.Win.C1FlexGrid


Public Class frmCV_StressTest

    'Declare each column in table CV_StressTest
    Dim COL_StressID As Integer = 0
    Dim COL_PatientID As Integer = 1
    Dim COL_ExamID As Integer = 2
    Dim COL_VisitID As Integer = 3
    Dim COL_ClinicID As Integer = 4
    Dim COL_DateofStudy As Integer = 5
    Dim COL_Result As Integer = 6
    Dim COL_DateofStudyInvisible As Integer = 7
    Dim COL_Parent As Integer = 8
    Dim COL_IDENTITY As Integer = 9
    
    Dim COLUMN_COUNT As Integer = 10

    Dim cStyleResult As C1.Win.C1FlexGrid.CellStyle

    Public blnIsNew As Boolean
    Dim struser As String

    Dim objclsDbLayer As New ClsCVStressDbLayer
    Public strSearch As String = String.Empty
    Private blnIsLoaded As Boolean = False
    Private mPatientID As Int64
    Private mVisitID As Int64
    Private mExamId As Int64
    Private mClinicID As Int64
    Private mDateofStudy As Date
    Private WithEvents gloUC_PatientStrip1 As New gloUserControlLibrary.gloUC_PatientStrip

    Public Sub New(ByVal PatientId As Int64, ByVal DateOfStudy As Date, Optional ByVal ExamID As Int64 = 0, Optional ByVal ClinicID As Int64 = 1)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mPatientID = PatientId
        mVisitID = 0
        mDateofStudy = DateOfStudy
        blnIsLoaded = False
        dtpDateofStudy.Value = mDateofStudy
        blnIsLoaded = True
        mExamId = ExamID
        mClinicID = ClinicID
    End Sub

    Public Sub New(ByVal PatientId As Int64, ByVal VisitID As Int64, ByVal DateOfStudy As Date, Optional ByVal ExamID As Int64 = 0, Optional ByVal ClinicID As Int64 = 1)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mPatientID = PatientId
        mVisitID = VisitID
        mDateofStudy = DateOfStudy
        blnIsLoaded = False
        dtpDateofStudy.Value = mDateofStudy
        blnIsLoaded = True
        mExamId = ExamID
        mClinicID = ClinicID
    End Sub

    Private Sub frmCV_StressTest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_StressTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(C1CV_StressTest)

        Try
            txtSearch.Focus()
            gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
            Me.Controls.Add(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dock = DockStyle.Top
            gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
            gloUC_PatientStrip1.BringToFront()
            pnlPatientSearchHeader.BringToFront()
            pnlRight.BringToFront()
            Splitter1.BringToFront()
            pnlC1CV_StressTest.BringToFront()
            gloUC_PatientStrip1.ShowDetail(mPatientID, gloUC_PatientStrip.enumFormName.None)

            'declare datatables
            Dim dtPopulateCpt As DataTable
            Dim dtPopulateStressTest As DataTable
            'Dim dtPopulateStressUser As DataTable

            'Show data into tree view
            'call PopulateCptDt function to fill datatable
            dtPopulateCpt = objclsDbLayer.PopulateCptDt()

            If Not IsNothing(dtPopulateCpt) Then
                If dtPopulateCpt.Rows.Count > 0 Then
                    'call populateCptList function to populate tree view control
                    PopulateCptList(dtPopulateCpt, strSearch)
                End If
            End If


            'call PopulateStressTestDt function to fill datatable
            dtPopulateStressTest = objclsDbLayer.PopulateStressTestDt(Convert.ToDateTime("2/20/2009 12:00:00 AM"), 397693578328223001, 39862935813331301)
            C1CV_StressTest.Clear(ClearFlags.All)
            'call setGridstyle function to set the style of C1CV_StressTest
            SetGridStyle()
            If blnIsNew = False Then
                'Check if form is loaded in edit mode
                FillStressTest()
            Else
                'if form is loaded in new mode then check if visit already exists
                If mVisitID = 0 Then
                    'if visit does not exists then load the form in new mode
                    FillStressTestForAdd()
                Else
                    'if visit exists for the given date then pull the data for that date
                    FillStressTest()
                End If
            End If
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, "View Stress Test", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, "View Stress Test", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
       
    End Sub

    Public Function PopulateUserList(ByVal dt As DataTable, ByVal strQuerySearch As String)

        'Handle Try-Catch block
        Try
            Dim dv As New DataView(dt)
            dv.RowFilter = "sLoginName Like '%" & strQuerySearch & "%'"

            Dim tdt As New DataTable
            tdt = dv.ToTable

            trUsersList.BeginUpdate()
            trUsersList.Visible = False
            Dim icnt As Int32 = 0
            
            ' Add Tree viewcontrol Acc. to Users in UserList
            trUsersList.Nodes.Clear()
            If Not IsNothing(tdt) Then
                trUsersList.Nodes.Clear()
                If tdt.Rows.Count > 0 Then
                    'Add root Node
                    trUsersList.Nodes.Clear()
                    trUsersList.Nodes.Add("User Name")
                    Dim myNode As TreeNode
                    myNode = trUsersList.Nodes.Item(0)
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2

                    'clear subnodes
                    trUsersList.Nodes(0).Nodes.Clear()

                    '  If tdt.Rows.Count > 0 Then
                    'Add sub nodes 
                    For icnt = 0 To tdt.Rows.Count - 1
                        Dim mychildnode As TreeNode
                        mychildnode = New TreeNode(CType(tdt.Rows.Item(icnt)(0), String) & "-" & CType(tdt.Rows.Item(icnt)(1), String))
                        mychildnode.Tag = CType(tdt.Rows.Item(icnt)(0), String)
                        mychildnode.ImageIndex = 1
                        mychildnode.SelectedImageIndex = 1
                        myNode.Nodes.Add(mychildnode)
                    Next


                    trUsersList.Visible = True
                    trUsersList.ExpandAll()
                    trUsersList.Select()
                    trUsersList.SelectedNode = trUsersList.Nodes.Item(0)
                    txtSearch.Focus()
                    ' End If

                End If
                txtSearch.Focus()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trUsersList.EndUpdate()
        End Try
        Return True
    End Function

    Public Function PopulateCptList(ByVal dt As DataTable, ByVal strQuerySearch As String)

        Try

            'search text
            Dim dv As New DataView(dt)
            dv.RowFilter = "CPTDesc Like '%" & strQuerySearch & "%'"
            'search text complete

            Dim tdt As New DataTable
            tdt = dv.ToTable
            trCPTList.BeginUpdate()
            trCPTList.Visible = False

            Dim iCnt As Integer = 0
            If Not IsNothing(tdt) Then
                If tdt.Rows.Count > 0 Then
                    'Add root node
                    trCPTList.Nodes.Clear()
                    trCPTList.Nodes.Add("CPT List")
                    Dim myNode As TreeNode
                    myNode = trCPTList.Nodes.Item(0)
                    myNode.ImageIndex = 0
                    myNode.SelectedImageIndex = 0
                    'clear subnodes
                    trCPTList.Nodes(0).Nodes.Clear()
                    ' If tdt.Rows.Count > 0 Then

                    'Add Sud nodes
                    For iCnt = 0 To tdt.Rows.Count - 1
                        Dim myChildNode As TreeNode
                        myChildNode = New TreeNode(CType(tdt.Rows.Item(iCnt)(2), String))
                        myChildNode.ImageIndex = 1
                        myChildNode.SelectedImageIndex = 1
                        myNode.Nodes.Add(myChildNode)
                    Next
                    'End If

                    ' End If
                    trCPTList.Visible = True
                    trCPTList.ExpandAll()
                    trCPTList.Select()
                    trCPTList.SelectedNode = trCPTList.Nodes.Item(0)

                End If
            End If
            txtSearch.Focus()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trCPTList.EndUpdate()
        End Try
        Return True
    End Function

    'To give btn Hover and Leave iamges.>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Ojeswini03022009<<<<<<<<<<<<<<<<<<<<<<<
    Private Sub btnUserList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserList.Click
        Dim dt As DataTable
        pnltrCPTList.Visible = False
        pnltrUsersList.Visible = True
        pnlbtnUserList.Dock = DockStyle.Top
        btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnUserList.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnUserList.Tag = "Selected"

        pnlSearch.BringToFront()
        pnlTreeview.BringToFront()

        pnlbtnCPT.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnCPT.Tag = "UnSelected"

        pnlbtnType.Dock = DockStyle.Bottom
        btnType.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnType.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnType.Tag = "UnSelected"

        'call PopulateUserDt function to fill datatable
        dt = objclsDbLayer.PopulateUserDt()
        'call populateUserList function to populate tree view control
        PopulateUserList(dt, strSearch)
        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtSearch.ResetText()
        Else
            txtSearch_TextChanged(sender, e)
        End If

        'txtSearch.Text = ""
        txtSearch.Focus()

    End Sub

    Private Sub btnUserList_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserList.MouseHover
        btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnUserList.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnUserList_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserList.MouseLeave
        'If pnlbtnUserList.Tag.ToString().ToUpper() = "SELECTED" Then
        If pnlbtnUserList.Dock = DockStyle.Top Then
            btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnUserList.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnUserList.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        pnltrCPTList.Show()
        pnltrUsersList.Hide()
        pnlbtnCPT.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnCPT.Tag = "Selected"

        pnlSearch.BringToFront()
        pnlTreeview.BringToFront()


        pnlbtnUserList.Dock = DockStyle.Bottom
        btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnUserList.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnUserList.Tag = "UnSelected"

        pnlbtnType.Dock = DockStyle.Bottom
        btnType.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnType.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnType.Tag = "UnSelected"

        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtSearch.ResetText()
        Else
            txtSearch_TextChanged(sender, e)
        End If
        '  txtSearch.Text = ""
        txtSearch.Focus()


    End Sub

    Private Sub btnCPT_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseHover
        
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseLeave
        'If pnlbtnCPT.Tag.ToString().ToUpper() = "SELECTED" Then
        If pnlbtnCPT.Dock = DockStyle.Top Then
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Dim dt As DataTable
        pnltrCPTList.Visible = False
        pnltrUsersList.Visible = True
        pnlbtnType.Dock = DockStyle.Top
        btnType.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnType.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnType.Tag = "Selected"

        pnlSearch.BringToFront()
        pnlTreeview.BringToFront()

        pnlbtnCPT.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnCPT.Tag = "UnSelected"

        pnlbtnUserList.Dock = DockStyle.Bottom
        btnUserList.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnUserList.BackgroundImageLayout = ImageLayout.Stretch
        pnlbtnUserList.Tag = "UnSelected"

       
        'call PopulateUserDt function to fill datatable
        dt = objclsDbLayer.PopulateTypeDt()
     
        'call populateUserList function to populate tree view control
        trUsersList.Nodes.Clear()
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                PopulateTypeList(dt, strSearch)
            End If
        End If
        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtSearch.ResetText()
        Else
            txtSearch_TextChanged(sender, e)
        End If



        'txtSearch.Text = ""
        txtSearch.Focus()
    End Sub

    Private Sub btnType_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.MouseHover
        btnType.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnType.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnType_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.MouseLeave
        'If pnlbtnType.Tag.ToString().ToUpper() = "SELECTED" Then
        If pnlbtnType.Dock = DockStyle.Top Then
            btnType.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnType.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnType.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnType.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub
    '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Ojeswini03022009<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub SetGridStyle()
        'Declare a variable
        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        Dim struser As String
        With C1CV_StressTest
            Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (.Width - 20) / 3

            .Cols.Count = COLUMN_COUNT  '' COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = True
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_StressID).Width = .Width * 0
            .Cols(COL_StressID).AllowEditing = False
            .SetData(0, COL_StressID, "StressID")
            .Cols(COL_StressID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False
            .SetData(0, COL_VisitID, "ExamID")
            .Cols(COL_ExamID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .SetData(0, COL_VisitID, "VisitID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_ClinicID).Width = .Width * 0
            .Cols(COL_ClinicID).AllowEditing = False
            .SetData(0, COL_VisitID, "ClinicID")
            .Cols(COL_ClinicID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_DateofStudy).Width = _TotalWidth * 1
            .SetData(0, COL_DateofStudy, "Stress Test") '"Date"
            .Cols(COL_DateofStudy).DataType = GetType(String)
            .Cols(COL_DateofStudy).AllowEditing = False

            .Cols(COL_Result).Width = _TotalWidth * 1
            .SetData(0, COL_Result, "Result")
            .Cols(COL_Result).DataType = GetType(String)
            .Cols(COL_Result).AllowEditing = False ''True bug no:5685 change it to false

            Dim dtResults As DataTable
            dtResults = New DataTable()
            dtResults = GetResults()

            Dim strComboString As New System.Text.StringBuilder
            'Dim blnIsNormal As Boolean
            'Dim blnIsAbnormal As Boolean
            'Dim blnIsIntermediate As Boolean
            'If dtResults.Rows.Count > 0 Then
            '    For icnt As Int32 = 0 To dtResults.Rows.Count - 1
            '        If dtResults.Rows(icnt)(0).ToString = "Normal" Then
            '            blnIsNormal = True
            '        ElseIf dtResults.Rows(icnt)(0).ToString = "Abnormal" Then
            '            blnIsAbnormal = True
            '        ElseIf dtResults.Rows(icnt)(0).ToString = "Intermediate" Then
            '            blnIsIntermediate = True
            '        End If
            '        strComboString = strComboString & "|" & dtResults.Rows(icnt)(0).ToString
            '    Next
            '    If blnIsNormal = False Then
            '        strComboString = strComboString & "|" & "Normal"
            '    End If
            '    If blnIsAbnormal = False Then
            '        strComboString = strComboString & "|" & "Abnormal"
            '    End If
            '    If blnIsIntermediate = False Then
            '        strComboString = strComboString & "|" & "Intermediate"
            '    End If
            'Else

            '    strComboString = "Normal|Abnormal|Intermediate"
            'End If
            'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
            For icnt As Int32 = 0 To dtResults.Rows.Count - 1
                If icnt = 0 Then
                    strComboString.Append(dtResults.Rows.Item(icnt)(0))
                Else
                    strComboString.Append("|")
                    strComboString.Append(dtResults.Rows.Item(icnt)(0))
                End If

            Next
            cStyleResult = C1CV_StressTest.Styles.Add("Results")
            cStyleResult.ComboList = strComboString.ToString
            strComboString.Remove(0, strComboString.Length)

            .Cols(COL_DateofStudyInvisible).Width = 0
            .SetData(0, COL_DateofStudyInvisible, "Date")
            .Cols(COL_DateofStudyInvisible).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisible).AllowEditing = False

            .Cols(COL_Parent).Width = 0
            .SetData(0, COL_Parent, "ParentNode")

            .Cols(COL_IDENTITY).Width = 0
            .SetData(0, COL_IDENTITY, "IDENTITY")

            'Dim dt1 As DataTable
            'dt1 = fillusercombo()
            'Dim strUserName As New System.Text.StringBuilder
            'For j As Int32 = 0 To dt1.Rows.Count - 1
            '    If j > 0 Then
            '        strUserName.Append("|")
            '    End If
            '    strUserName.Append(dt1.Rows(j)("sLoginName"))
            'Next
        End With
    End Sub

    Private Function GetResults() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Dim ds As New DataSet
        Dim dt As DataTable
        Try
            Connection.Open()
            Dim CommandString As String = "select Distinct sDescription from category_mst where scategorytype = 'Cardio Test Result'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
        End Try
    End Function

    Private Sub FillStressTestForAdd()
        Try

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1CV_StressTest
                .Tree.Column = COL_DateofStudy
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 15
            End With
            Dim dtStudyDate As DataTable
            Dim dtCPT As DataTable
            Dim dtUsers As DataTable


            Dim nDOS As Int16
            Dim nCPT As Int16
            Dim nUser As Int16


            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer


            strselecrICD9Qry = "SELECT Distinct nStressID,nPatientID,nExamID,nVisitID,nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE  nPatientID = " & mPatientID & "  and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & mDateofStudy & "'"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtStudyDate = oDB.ReadQueryDataTable(strselecrICD9Qry)
            oDB.Disconnect()

            With dtStudyDate
                If Not IsNothing(dtStudyDate) Then
                    If dtStudyDate.Rows.Count = 0 Then


                        Dim StressID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        Dim DateofStudy As Date

                        Dim count As Integer = nDOS + 1

                        C1CV_StressTest.Rows.Add()
                        _Row = C1CV_StressTest.Rows.Count - 1
                        'set the properties for newly added row
                        With C1CV_StressTest.Rows(_Row)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 0

                            'sarika bug 1613
                            '.Node.Data = mDateofStudy  '' 02/20/2009
                            .Node.Data = mDateofStudy.ToShortDateString  '' 02/20/2009
                            '---
                            .Node.Image = imgTabs.Images(2) 'Global.gloEMR.My.Resources.Resources.ICD_09
                        End With
                        nextICD = _Row

                        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                        'cStyle = C1CV_StressTest.Styles.Add("CS_Roles" & nDOS) ''style new for every row
                        'cStyle.Editor.Enabled = True
                        'Dim rgStyle As C1.Win.C1FlexGrid.CellRange = C1CV_StressTest.GetCellRange(nDOS + 1, COL_Result)
                        'Dim rgTestType As C1.Win.C1FlexGrid.CellRange = C1CV_StressTest.GetCellRange(nDOS + 1, COL_TestType)

                        With C1CV_StressTest
                            '.SetData(_Row, COL_DateofStudy, _Row)
                            '.SetData(_Row, COL_StressID, 0)
                            .SetData(_Row, COL_PatientID, mPatientID)
                            .SetData(_Row, COL_ExamID, mExamId)
                            .SetData(_Row, COL_VisitID, mVisitID)
                            .SetData(_Row, COL_ClinicID, mClinicID)
                            .SetData(_Row, COL_DateofStudyInvisible, mDateofStudy)
                            .SetData(_Row, COL_Parent, "Date")
                            '.SetCellStyle(_Row, COL_Result, cStyle)
                            '.SetCellStyle(_Row, COL_TestType, cStyle)

                        End With

                        C1CV_StressTest.Rows.Add()
                        _Row = C1CV_StressTest.Rows.Count - 1
                        With C1CV_StressTest.Rows(_Row)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = "CPT" '' "CPT"
                            .Node.Image = imgTabs.Images(0) 'Global.gloEMR.My.Resources.Resources.cpt
                        End With


                        C1CV_StressTest.Rows.Add()
                        _Row = C1CV_StressTest.Rows.Count - 1
                        With C1CV_StressTest.Rows(_Row)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = "Users" '' "CPT"
                            .Node.Image = imgTabs.Images(4) 'Global.gloEMR.My.Resources.Resources.Modify1
                        End With
                        '' If CStr(dtStudyDate.Rows(nDOS)("sICD9Code")).Trim <> "" Then
                    End If
                End If  '' If IsNothing(dtStudyDate) = False Then
            End With '' With dtStudyDate


            dtStudyDate = Nothing
            dtCPT = Nothing
            dtUsers = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillStressTest()
        Try

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1CV_StressTest
                .Tree.Column = COL_DateofStudy
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 15
            End With
            Dim dtStudyDate As DataTable
            Dim dtCPT As DataTable
            Dim dtUsers As DataTable
            Dim dtTests As DataTable

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            Dim nCPT As Int16
            Dim nUser As Int16


            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer


            strselecrICD9Qry = "SELECT Distinct nPatientID,nExamID,nVisitID,nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE  nPatientID = " & mPatientID & "  and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & mDateofStudy & "'"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtStudyDate = oDB.ReadQueryDataTable(strselecrICD9Qry)
            oDB.Disconnect()

            With dtStudyDate
                If IsNothing(dtStudyDate) = False Then
                    If dtStudyDate.Rows.Count > 0 Then


                        For nDOS = 0 To .Rows.Count - 1
                            Dim StressID As Int64 = 0
                            Dim PatientID As Int64 = 0
                            Dim VisitID As Int64 = 0
                            Dim ExamID As Int64 = 0
                            Dim ClinicID As Int64 = 0
                            Dim DateofStudy As Date
                            Dim TestType As String = ""

                            Dim Result As String = ""

                            Dim count As Integer = nDOS + 1
                            If CStr(dtStudyDate.Rows(nDOS)("DateofStudy")).Trim <> "" Then

                                C1CV_StressTest.Rows.Add()
                                _Row = C1CV_StressTest.Rows.Count - 1
                                'set the properties for newly added row
                                With C1CV_StressTest.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    'sarika bug 1613
                                    '.Node.Data = dtStudyDate.Rows(nDOS)("DateofStudy") '' 02/20/2009
                                    .Node.Data = Convert.ToDateTime(dtStudyDate.Rows(nDOS)("DateofStudy")).ToShortDateString  '' 02/20/2009
                                    '--
                                    .Node.Image = imgTabs.Images(2) 'Global.gloEMR.My.Resources.Resources.ICD_09
                                End With
                                nextICD = _Row

                                With C1CV_StressTest
                                    '.SetData(_Row, COL_DateofStudy, _Row)
                                    '.SetData(_Row, COL_StressID, dtStudyDate.Rows(nDOS)("nStressID"))
                                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(nDOS)("nPatientID"))
                                    .SetData(_Row, COL_ExamID, dtStudyDate.Rows(nDOS)("nExamID"))
                                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(nDOS)("nVisitID"))
                                    .SetData(_Row, COL_ClinicID, dtStudyDate.Rows(nDOS)("nClinicID"))
                                    'sarika 1613
                                    '.SetData(_Row, COL_DateofStudyInvisible,dtStudyDate.Rows(nDOS)("DateofStudy"))
                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(nDOS)("DateofStudy")).ToShortDateString)
                                    '--
                                    .SetData(_Row, COL_Parent, "Date")
                                    .SetData(_Row, COL_Result, Result)

                                    'StressID = dtStudyDate.Rows(nDOS)("nStressID")
                                    PatientID = dtStudyDate.Rows(nDOS)("nPatientID")
                                    VisitID = dtStudyDate.Rows(nDOS)("nVisitID")
                                    ExamID = dtStudyDate.Rows(nDOS)("nExamID")
                                    ClinicID = dtStudyDate.Rows(nDOS)("nClinicID")
                                    'sarika bug 1613
                                    'DateofStudy = dtStudyDate.Rows(nDOS)("DateofStudy")
                                    DateofStudy = Convert.ToDateTime(dtStudyDate.Rows(nDOS)("DateofStudy")).ToShortDateString
                                    '---
                                End With

                                Dim dtDateofStudy As Date = dtStudyDate.Rows(nDOS)("DateofStudy")
                                'Query for selecting CPT for current exam
                                strselectCPTQry = "SELECT DISTINCT isnull(sCPT,'') as sCPTcode FROM CV_StressTest WHERE  nPatientID = " & mPatientID & " and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & mDateofStudy & "'"
                                oDB.Connect(GetConnectionString)
                                dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
                                oDB.Disconnect()

                                'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sCPTcode"))

                                With dtCPT
                                    If IsNothing(dtCPT) = False Then
                                        If dtCPT.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "CPT" '' "CPT"
                                                .Node.Image = imgTabs.Images(0) 'Global.gloEMR.My.Resources.Resources.cpt
                                            End With
                                        End If
                                        For nCPT = 0 To .Rows.Count - 1
                                            Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                            If strCurrentCPT.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                'set the properties for newly added row
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = strCurrentCPT
                                                    .Node.Image = imgTabs.Images(1) 'Global.gloEMR.My.Resources.Resources.CPT1
                                                End With


                                                With C1CV_StressTest
                                                    ' .SetData(_Row, COL_StressID, StressID)
                                                    .SetData(_Row, COL_PatientID, PatientID)
                                                    .SetData(_Row, COL_ExamID, ExamID)
                                                    .SetData(_Row, COL_VisitID, VisitID)
                                                    .SetData(_Row, COL_ClinicID, ClinicID)
                                                    'sarika bug 1613
                                                    '.SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
                                                    .SetData(_Row, COL_DateofStudyInvisible, DateofStudy.ToShortDateString)
                                                    '--
                                                    .SetData(_Row, COL_Parent, "CPT")
                                                End With
                                                strselectMODQry = "SELECT Distinct isnull(sTesttype,'') as TestType,isnull(sResult,'') as Result from CV_StressTest WHERE  nPatientID = " & mPatientID & " and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & mDateofStudy & "' and sCPT='" & strCurrentCPT & "' and sTesttype is not null and sTesttype  <> ''"

                                                oDB.Connect(GetConnectionString)
                                                dtTests = oDB.ReadQueryDataTable(strselectMODQry)
                                                oDB.Disconnect()

                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 3
                                                    .Node.Data = "TestType" '' 
                                                    .Node.Image = imgTabs.Images(5) 'Global.gloEMR.My.Resources.Resources.p
                                                End With
                                                '' To Identify for which CPT we are addding the Test Type
                                                C1CV_StressTest.SetData(_Row, COL_IDENTITY, "TestType" & strCurrentCPT)

                                                With dtTests
                                                    If IsNothing(dtTests) = False Then
                                                        For nUser = 0 To dtTests.Rows.Count - 1
                                                            Dim strTest As String = dtTests.Rows(nUser)("TestType")
                                                            C1CV_StressTest.Rows.Add()
                                                            _Row = C1CV_StressTest.Rows.Count - 1
                                                            'set the properties for newly added row
                                                            With C1CV_StressTest.Rows(_Row)
                                                                .AllowEditing = True
                                                                .ImageAndText = True
                                                                .Height = 24
                                                                .IsNode = True
                                                                .Node.Level = 4
                                                                .Node.Data = strTest
                                                                .Node.Image = imgTabs.Images(1) 'Global.gloEMR.My.Resources.Resources.Modify1
                                                            End With

                                                            With C1CV_StressTest
                                                                '.SetData(_Row, COL_StressID, StressID)
                                                                .SetData(_Row, COL_PatientID, PatientID)
                                                                .SetData(_Row, COL_ExamID, ExamID)
                                                                .SetData(_Row, COL_VisitID, VisitID)
                                                                .SetData(_Row, COL_ClinicID, ClinicID)
                                                                'sarika bug 1613
                                                                '                                                                .SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
                                                                .SetData(_Row, COL_DateofStudyInvisible, DateofStudy.ToShortDateString)
                                                                '--
                                                                .SetData(_Row, COL_DateofStudy, dtTests.Rows(nUser)("TestType"))
                                                                .SetData(_Row, COL_Result, dtTests.Rows(nUser)("Result"))
                                                                .SetCellStyle(_Row, COL_Result, cStyleResult)
                                                                .SetData(_Row, COL_Parent, "TestType")
                                                            End With

                                                        Next
                                                    End If
                                                End With
                                            End If
                                        Next '' For nCPT = 0 To .Rows.Count - 1
                                    End If
                                End With '' With dtCPT
                                'Query for selecting Modifier for current exam 
                                strselectMODQry = "SELECT Distinct isnull(sUserName,'') as UserName from CV_StressTest WHERE  nPatientID = " & mPatientID & " and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & mDateofStudy & "'"

                                oDB.Connect(GetConnectionString)
                                dtUsers = oDB.ReadQueryDataTable(strselectMODQry)
                                oDB.Disconnect()

                                With dtUsers
                                    If IsNothing(dtUsers) = False Then
                                        If dtUsers.Rows.Count > 0 Then
                                            Dim strUsers As String = dtUsers.Rows(0)("UserName")
                                            Dim arrUsers() As String = Split(strUsers, "|")
                                            If arrUsers.Length > 0 Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 1
                                                    .Node.Data = "Users" '' "CPT"
                                                    .Node.Image = imgTabs.Images(4) 'Global.gloEMR.My.Resources.Resources.p
                                                End With

                                                For nUser = 0 To arrUsers.Length - 1
                                                    If arrUsers(0) <> "" Then
                                                        C1CV_StressTest.Rows.Add()
                                                        _Row = C1CV_StressTest.Rows.Count - 1
                                                        'set the properties for newly added row
                                                        With C1CV_StressTest.Rows(_Row)
                                                            .AllowEditing = False
                                                            .ImageAndText = True
                                                            .Height = 24
                                                            .IsNode = True
                                                            .Node.Level = 2
                                                            .Node.Data = arrUsers(nUser)
                                                            .Node.Image = imgTabs.Images(1) 'Global.gloEMR.My.Resources.Resources.Modify1
                                                        End With
                                                        'Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

                                                        With C1CV_StressTest
                                                            '.SetData(_Row, COL_StressID, StressID)
                                                            .SetData(_Row, COL_PatientID, PatientID)
                                                            .SetData(_Row, COL_ExamID, ExamID)
                                                            .SetData(_Row, COL_VisitID, VisitID)
                                                            .SetData(_Row, COL_ClinicID, ClinicID)
                                                            'sarika bug 1613
                                                            '.SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
                                                            .SetData(_Row, COL_DateofStudyInvisible, DateofStudy.ToShortDateString)
                                                            '---
                                                            .SetData(_Row, COL_Parent, "Users")
                                                        End With
                                                    End If

                                                Next
                                            End If
                                        End If
                                    End If
                                End With '' With dtUsers
                            End If  '' If CStr(dtStudyDate.Rows(nDOS)("sICD9Code")).Trim <> "" Then
                        Next ''For nDOS = 0 To .Rows.Count - 1
                    Else
                        FillStressTestForAdd()
                    End If

                End If  '' If IsNothing(dtStudyDate) = False Then
            End With '' With dtStudyDate


            dtStudyDate = Nothing
            dtCPT = Nothing
            dtUsers = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trCPTList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trCPTList.DoubleClick
        Try
            Dim CPTCatNode As Node = GetC1UniqueNode("CPT", C1CV_StressTest)
            Dim CPTNode As Node = GetC1UniqueNode(trCPTList.SelectedNode.Text.Trim, C1CV_StressTest)

            If IsNothing(CPTNode) = True Then
                CPTCatNode.AddNode(NodeTypeEnum.LastChild, trCPTList.SelectedNode.Text.Trim)
                CPTNode = CPTCatNode.GetNode(NodeTypeEnum.LastChild)
                CPTNode.Level = 2
                'CPTNode.Image = Global.gloEMR.My.Resources.Resources.CPT1
                CPTNode.Image = imgTabs.Images(1) 'CType(Global.gloEMR.My.Resources.Resources.Bullet_PNG, Image)
                C1CV_StressTest.SetData(CPTNode.Row.Index, COL_Parent, "CPT")
                'C1CV_StressTest.Rows.Add()
                'Dim mRow As Int32 = C1CV_StressTest.Rows.Count - 1
                CPTNode.AddNode(NodeTypeEnum.LastChild, "TestType")
                'Shubhangi 20091209
                'Check the setting Reset search text box after assiging category
                If gblnResetSearchTextBox = True Then
                    txtSearch.ResetText()
                End If

                Dim mNode As Node = CPTNode.GetNode(NodeTypeEnum.LastChild)
                mNode.Image = imgTabs.Images(5)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetC1UniqueNode(ByVal FindData As String, ByVal _C1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = _C1FlexGrid.FindRow(FindData, 1, COL_DateofStudy, False, True, True)
        If _FindRow > 0 Then
            _Node = _C1FlexGrid.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

    Private Function GetC1UniqueNode(ByVal FindData As String, ByVal ParentNode As C1.Win.C1FlexGrid.Node, ByVal COLNO As Int16, ByVal FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid) As C1.Win.C1FlexGrid.Node
        Dim oNode As C1.Win.C1FlexGrid.Node = Nothing
        Dim nfirstRow, nlastRow As Int16

        If ParentNode.Children > 0 Then
            nfirstRow = ParentNode.GetNode(NodeTypeEnum.FirstChild).Row.Index
            nlastRow = ParentNode.GetNode(NodeTypeEnum.LastChild).Row.Index
        End If

        For i As Int16 = nfirstRow To nlastRow
            If (FindData = FlexGrid.GetData(i, COLNO)) Then
                oNode = FlexGrid.Rows(i).Node
            End If
        Next
        Return oNode
    End Function

    Private Sub trUsersList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trUsersList.DoubleClick
        Try
            If pnlbtnUserList.Dock = DockStyle.Top Then
                Dim UserCatNode As Node = GetC1UniqueNode("Users", C1CV_StressTest)
                Dim UserNode As Node = GetC1UniqueNode(trUsersList.SelectedNode.Text.Trim, C1CV_StressTest)

                If IsNothing(UserNode) = True Then
                    ''UserCatNode.AddNode(NodeTypeEnum.LastChild, trUsersList.SelectedNode.Tag.Trim)
                    UserCatNode.AddNode(NodeTypeEnum.LastChild, trUsersList.SelectedNode.Text.Trim)
                    UserNode = UserCatNode.GetNode(NodeTypeEnum.LastChild)
                    UserNode.Level = 2
                    C1CV_StressTest.SetData(UserNode.Row.Index, COL_Parent, "Users")
                    'UserNode.Image = Global.gloEMR.My.Resources.Resources.Modify1
                    UserNode.Image = imgTabs.Images(1) 'CType(Global.gloEMR.My.Resources.Resources.Bullet_PNG, Image)
                End If
            ElseIf pnlbtnType.Dock = DockStyle.Top Then
                'Dim _FindRow As Integer = C1CV_StressTest.FindRow(trUsersList.SelectedNode.Text, 1, COL_DateofStudy, False, True, True)

                If C1CV_StressTest.Row <= 1 Then
                    Exit Sub
                End If

                Dim TestTypeNode As Node = C1CV_StressTest.Rows(C1CV_StressTest.Row).Node
                If IsNothing(TestTypeNode.GetNode(NodeTypeEnum.Parent)) = False Then
                Else
                    TestTypeNode = GetC1UniqueNode("TestType", C1CV_StressTest)
                End If

                If IsNothing(TestTypeNode) = False Then
                    If (TestTypeNode.Data = "TestType" Or TestTypeNode.GetNode(NodeTypeEnum.Parent).Data = "TestType") Then
                        If (TestTypeNode.Data = "TestType") Then
                        Else
                            TestTypeNode = TestTypeNode.GetNode(NodeTypeEnum.Parent)
                        End If
                        Dim TestNode As Node = GetC1UniqueNode(trUsersList.SelectedNode.Text.Trim, TestTypeNode, COL_DateofStudy, C1CV_StressTest)

                        If IsNothing(TestNode) = True Then
                            TestTypeNode.AddNode(NodeTypeEnum.LastChild, trUsersList.SelectedNode.Text.Trim)
                            TestNode = TestTypeNode.GetNode(NodeTypeEnum.LastChild)
                            TestNode.Level = 4
                            C1CV_StressTest.SetData(TestNode.Row.Index, COL_Parent, "TestType")
                            Dim mRow As Int32 = C1CV_StressTest.Rows.Count - 1
                            C1CV_StressTest.SetCellStyle(TestNode.Row.Index, COL_Result, cStyleResult)
                            TestNode.Image = imgTabs.Images(1) 'CType(Global.gloEMR.My.Resources.Resources.Bullet_PNG, Image)
                        End If
                    End If
                End If
            End If
            'Shubhangi 20091209
            'Check the setting Reset search text box after assiging category
            If gblnResetSearchTextBox = True Then
                txtSearch.ResetText()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub tls_StressTest_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_StressTest.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Save"
                    SaveStressTest()
                    Me.Close()
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function SaveStressTest()

        Try
            Dim objstresstest As clsstresstest
            Dim Arrlist As New ArrayList
            Dim objEjectionFractionDBLayer As New ClsEjectionFractionDBLayer
            With C1CV_StressTest
                Dim strUsers As New System.Text.StringBuilder
                .Select(0, 0, False)
                'Dim CPTNode As Node
                Dim UserCatNode As Node = GetC1UniqueNode("Users", C1CV_StressTest)
                If Not IsNothing(UserCatNode) Then
                    If UserCatNode.Children > 0 Then
                        Dim nFirstUser As Int16 = UserCatNode.Row.Index + 1
                        Dim nLastUser As Int16 = UserCatNode.GetNode(NodeTypeEnum.LastChild).Row.Index


                        For i As Int16 = nFirstUser To nLastUser
                            If i > nFirstUser Then
                                strUsers.Append("|")
                            End If
                            strUsers.Append(.GetData(i, COL_DateofStudy))
                        Next
                    End If
                  
                End If
             

                Dim CPTCatNode As Node = GetC1UniqueNode("CPT", C1CV_StressTest)
                If Not IsNothing(CPTCatNode) Then
                    If CPTCatNode.Children > 0 Then
                        Dim nFirstCPT As Int16 = CPTCatNode.Row.Index + 1
                        Dim nLastCPT As Int16 = CPTCatNode.GetNode(NodeTypeEnum.LastChild).Row.Index

                        If mVisitID = 0 Then
                            mVisitID = GenerateVisitID(dtpDateofStudy.Value.Date, mPatientID)
                        End If
                        mDateofStudy = dtpDateofStudy.Value

                        Dim Result As String = .GetData(1, COL_Result)
                        For i As Int16 = nFirstCPT To nLastCPT + 1
                            Dim sProcedure As String
                            If .GetData(i, COL_Parent) = "CPT" Then

                                sProcedure = ""

                                objstresstest = New clsstresstest
                                objstresstest.ClinicID = 1
                                objstresstest.PatientID = mPatientID
                                objstresstest.Examid = mExamId
                                objstresstest.VisitID = mVisitID
                                objstresstest.DateofStudy = mDateofStudy
                                ' objstresstest.TestType = TestType
                                'objstresstest.Result = Result
                                objstresstest.UserName = strUsers.ToString
                                objstresstest.procedure = .GetData(i, COL_DateofStudy)
                                sProcedure = objstresstest.procedure
                                Arrlist.Add(objstresstest)
                            End If
                            If .GetData(i, COL_DateofStudy) = "TestType" Then
                                Dim mNode As Node = .Rows(i).Node

                                Dim TypeCatNode As Node = C1CV_StressTest.Rows(i).Node
                                If Not IsNothing(TypeCatNode) Then
                                    If TypeCatNode.Children > 0 Then
                                        Dim nFirstType As Int16 = TypeCatNode.Row.Index + 1
                                        Dim nLastType As Int16 = TypeCatNode.GetNode(NodeTypeEnum.LastChild).Row.Index
                                        For j As Int16 = nFirstType To nLastType
                                            If j > nFirstType Then
                                                objstresstest = New clsstresstest
                                                objstresstest.ClinicID = 1
                                                objstresstest.PatientID = mPatientID
                                                objstresstest.Examid = mExamId
                                                objstresstest.VisitID = mVisitID
                                                objstresstest.DateofStudy = mDateofStudy
                                                objstresstest.TestType = .GetData(j, COL_DateofStudy)
                                                objstresstest.Result = .GetData(j, COL_Result)
                                                objstresstest.UserName = strUsers.ToString
                                                objstresstest.procedure = sProcedure
                                                Arrlist.Add(objstresstest)
                                            Else
                                                objstresstest.TestType = .GetData(j, COL_DateofStudy)
                                                objstresstest.Result = .GetData(j, COL_Result)
                                            End If
                                        Next
                                    End If
                                    
                                End If
                               
                            End If
                        Next
                        objEjectionFractionDBLayer.SaveStressTest(Arrlist)
                    End If
                    
                End If
               
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Function

    Private Sub mnuRemoveCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveCPT.Click
        Try
            Dim intSlectedrow As Integer = C1CV_StressTest.Row


            '' COMMENT BY SUDHIR 20090528 '' OBJECT REFERENCE ERROR IN CODE ''
            'Dim nTesttypeIndex As Int32 = C1CV_StressTest.Rows.Item(intSlectedrow).Node.GetNode(NodeTypeEnum.FirstChild).Row.Index
            'Dim nFirstChild As Int32 = C1CV_StressTest.Rows.Item(nTesttypeIndex).Node.GetNode(NodeTypeEnum.FirstChild).Row.Index
            'Dim nLastChild As Int32 = C1CV_StressTest.Rows.Item(nTesttypeIndex).Node.GetNode(NodeTypeEnum.LastChild).Row.Index

            'For i As Int32 = nLastChild To nFirstChild Step -1
            '    C1CV_StressTest.Rows.Remove(i)
            'Next
            'C1CV_StressTest.Rows.Remove(nTesttypeIndex)
            'C1CV_StressTest.Rows.Remove(intSlectedrow)

            C1CV_StressTest.Rows.Item(intSlectedrow).Node.RemoveNode() '' BUG FIXED ''


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub

    Private Sub C1CV_StressTest_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_StressTest.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            With C1CV_StressTest
                Dim r As Integer = .HitTest(e.X, e.Y).Row
                If r > 0 Then
                    .Select(r, True)

                    If .GetData(r, COL_Parent) = "" Then
                        C1CV_StressTest.ContextMenuStrip = Nothing
                    Else
                        C1CV_StressTest.ContextMenuStrip = cntRemoveStressTest
                        Dim intSlectedrow As Integer = C1CV_StressTest.Row
                        If .GetData(r, COL_Parent) = "CPT" Then
                            mnuRemoveCPT.Visible = True
                            mnuRemoveUser.Visible = False
                            mnuRemoveTest.Visible = False
                        ElseIf .GetData(r, COL_Parent) = "Users" Then
                            mnuRemoveCPT.Visible = False
                            mnuRemoveUser.Visible = True
                            mnuRemoveTest.Visible = False
                        ElseIf .GetData(r, COL_Parent) = "TestType" Then
                            mnuRemoveCPT.Visible = False
                            mnuRemoveUser.Visible = False
                            mnuRemoveTest.Visible = True
                        Else
                            mnuRemoveCPT.Visible = False
                            mnuRemoveUser.Visible = False
                            mnuRemoveTest.Visible = False
                        End If
                    End If
                Else
                    C1CV_StressTest.ContextMenuStrip = Nothing
                End If
            End With

        End If
    End Sub

    '\\ added by suraj 20090226 - for allowed '-' char in searchbox 
    Public Function ValidateTextSearch(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            If pnlbtnCPT.Dock = DockStyle.Top Then
                trCPTList.Select()
            ElseIf pnlbtnUserList.Dock = DockStyle.Top Then
                trUsersList.Select()
            ElseIf pnlbtnType.Dock = DockStyle.Top Then
                trUsersList.Select()
            End If
        Else
            If pnlbtnCPT.Dock = DockStyle.Top Then
                If trCPTList.Nodes.Count > 0 Then
                    trCPTList.SelectedNode = trCPTList.Nodes.Item(0)
                End If
            ElseIf pnlbtnUserList.Dock = DockStyle.Top Then
                ''Dhruv 20091214 
                ''If there is no nodes present into the tree then check it
                If trUsersList.Nodes.Count > 0 Then
                    trUsersList.SelectedNode = trUsersList.Nodes.Item(0)
                End If
                ''Dhruv 20091214 
                ''If there is no nodes present into the tree then check it
            ElseIf pnlbtnType.Dock = DockStyle.Top Then
                If trUsersList.Nodes.Count > 0 Then
                    trUsersList.SelectedNode = trUsersList.Nodes.Item(0)
                End If
            End If
            End If
            '\\  search with allowed char '-'
            If pnlbtnCPT.Dock = DockStyle.Top Then
                ValidateTextSearch(txtSearch.Text, e)
            Else
                mdlGeneral.ValidateText(txtSearch.Text, e)
            End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try

            Dim strSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strSearchDetails = Replace(txtSearch.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If

            Dim dt As DataTable
            Dim dtPopulateCpt As DataTable

            '  If Len(Trim(txtSearch.Text)) > 0 Then
            'If txtSearch.Tag <> Trim(txtSearch.Text) Then
            If pnlbtnCPT.Dock = DockStyle.Top Then
                'FillAssociates(Associates.CPT, Trim(txtSearch.Text))
                dtPopulateCpt = objclsDbLayer.PopulateCptDt()
                PopulateCptList(dtPopulateCpt, strSearchDetails)
                txtSearch.Tag = Trim(txtSearch.Text)
            ElseIf pnlbtnUserList.Dock = DockStyle.Top Then
                'FillAssociates(Associates.Procedure, Trim(txtSearch.Text))
                dt = objclsDbLayer.PopulateUserDt()
                PopulateUserList(dt, strSearchDetails)
                txtSearch.Tag = Trim(txtSearch.Text)
            ElseIf pnlbtnType.Dock = DockStyle.Top Then
                'FillAssociates(Associates.Procedure, Trim(txtSearch.Text))
                dt = objclsDbLayer.PopulateTypeDt()
                PopulateTypeList(dt, strSearchDetails)
                txtSearch.Tag = Trim(txtSearch.Text)

            End If
            'End If
            '  End If

            If pnlbtnCPT.Dock = DockStyle.Top Then
                Dim mychildnode As TreeNode
                'Shubhangi 20091007
                'If there is no record in tree view

                If trCPTList.Nodes.Count > 0 Then

                    'child node collection
                    For Each mychildnode In trCPTList.Nodes.Item(0).Nodes

                        'compare selected node text and entered text
                        Dim str As String
                        str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(txtSearch.Text))))
                        If str = UCase(Trim(txtSearch.Text)) Then
                            trCPTList.SelectedNode = trCPTList.SelectedNode.LastNode
                            trCPTList.SelectedNode = mychildnode
                            trCPTList.Focus()
                            txtSearch.Focus()
                            Exit Sub
                        End If
                    Next

                End If

                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched CPT having substring", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched CPT having substring", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched CPT having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ElseIf pnlbtnUserList.Dock = DockStyle.Top Then
                Dim mychildnode As TreeNode
                'Shubhangi 20091007
                'If there is no record in tree view
                If trUsersList.Nodes.Count > 0 Then

                    'child node collection
                    For Each mychildnode In trUsersList.Nodes.Item(0).Nodes
                        'compare selected node text and entered text
                        Dim str As String
                        str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(txtSearch.Text))))
                        If str = UCase(Trim(txtSearch.Text)) Then
                            trUsersList.SelectedNode = trUsersList.SelectedNode.LastNode
                            trUsersList.SelectedNode = mychildnode
                            trUsersList.Focus()
                            txtSearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched user having substring", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched user having substring", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched user having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ElseIf pnlbtnType.Dock = DockStyle.Top Then

                Dim mychildnode As TreeNode
                'Shubhangi 20091007
                'If there is no record in tree view
                If trUsersList.Nodes.Count > 0 Then
                    'child node collection
                    For Each mychildnode In trUsersList.Nodes.Item(0).Nodes
                        'compare selected node text and entered text
                        Dim str As String
                        str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(txtSearch.Text))))
                        If str = UCase(Trim(txtSearch.Text)) Then
                            trUsersList.SelectedNode = trUsersList.SelectedNode.LastNode
                            trUsersList.SelectedNode = mychildnode
                            trUsersList.Focus()
                            txtSearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched Type having substring", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, "Searched Type having substring", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub dtpDateofStudy_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDateofStudy.ValueChanged
        Try
            If blnIsLoaded = True Then
                mDateofStudy = dtpDateofStudy.Value.Date
                'When date is changed check if there is a visitid for that date
                mVisitID = GetVisitID(dtpDateofStudy.Value.Date, mPatientID)
                ' if visit exists for that date then
                If mVisitID <> 0 Then
                    'Further check if there is stresstest for that date
                    Dim strquery As String = "select count(*) from cv_stresstest where nvisitid=" & mVisitID & ""
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)
                    Dim objval As Object = oDB.ExecuteQueryScaler(strquery)
                    If Not IsNothing(objval) Then
                        If CType(objval, Int32) > 0 Then
                            C1CV_StressTest.Clear(ClearFlags.All)
                            SetGridStyle()
                            FillStressTest()
                        Else
                            C1CV_StressTest.SetData(1, COL_DateofStudy, dtpDateofStudy.Value.Date)
                        End If
                    Else
                        C1CV_StressTest.SetData(1, COL_DateofStudy, dtpDateofStudy.Value.Date)
                    End If
                Else
                    C1CV_StressTest.SetData(1, COL_DateofStudy, dtpDateofStudy.Value.Date)
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Function PopulateTypeList(ByVal dt As DataTable, ByVal strQuerySearch As String)
        '\\NOTE=> here trUserlist treeview is used for showing Type List
        'Handle Try-Catch block
        Try
            Dim dv As New DataView(dt)
            dv.RowFilter = "sDescription Like '%" & strQuerySearch & "%'"

            Dim tdt As New DataTable
            tdt = dv.ToTable

            trUsersList.BeginUpdate()
            trUsersList.Visible = False
            Dim icnt As Int32 = 0
            ' Add Tree viewcontrol Acc. to Users in UserList
            'If Not IsNothing(tdt) Then
            If tdt.Rows.Count > 0 Then
                'Add root Node
                trUsersList.Nodes.Clear()
                trUsersList.Nodes.Add("Test Type")
                Dim myNode As TreeNode
                myNode = trUsersList.Nodes.Item(0)
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                'clear subnodes
                trUsersList.Nodes(0).Nodes.Clear()
                '  If tdt.Rows.Count > 0 Then
                'Add sub nodes 
                For icnt = 0 To tdt.Rows.Count - 1
                    Dim mychildnode As TreeNode
                    mychildnode = New TreeNode(CType(tdt.Rows.Item(icnt)(0), String))
                    mychildnode.ImageIndex = 1
                    mychildnode.SelectedImageIndex = 1
                    myNode.Nodes.Add(mychildnode)
                Next

                trUsersList.Visible = True
                trUsersList.ExpandAll()
                trUsersList.Select()
                trUsersList.SelectedNode = trUsersList.Nodes.Item(0)
                'End If
                'End If
            End If
            txtSearch.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trUsersList.EndUpdate()
        End Try
        Return True
    End Function

    Private Sub mnuRemoveTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveTest.Click, mnuRemoveUser.Click
        Try
            Dim intSlectedrow As Integer = C1CV_StressTest.Row
            C1CV_StressTest.Rows.Remove(intSlectedrow)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub

    Private Sub C1CV_StressTest_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_StressTest.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnCategoryClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091006
        'USE CLEAR BUTTON TO CLEAR SEARCH TEXT BOX
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
End Class