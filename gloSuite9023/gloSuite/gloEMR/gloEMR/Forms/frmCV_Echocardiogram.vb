Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloUserControlLibrary
Imports System.Text.RegularExpressions
Public Class frmCV_Echocardiogram

    Private WithEvents dgCustomGrid As CustomTask

    Dim dt As DataTable = Nothing
    Dim oCPT As DataTable = Nothing
    Private Col_Count As Integer = 3
    Private Col_Check As Integer = 2
    Private Col_Name As Integer = 0
    Private strlst As String = ""
    Private _nPatientID As Int64 = 0
    Private _nVisitId As Int64 = 0
    Private _dtprocdate As Date = Nothing

    Dim objCath As New Cls_CardioVasculars


    Private _nTransaction As Integer '1-Add,2-Modify
    Public Property nTransaction() As Integer
        Get
            Return _nTransaction
        End Get
        Set(ByVal value As Integer)
            _nTransaction = value
        End Set
    End Property


    'Private _nPatientID As Int64
    'Private _nExamId As Int64
    'Private _nVisitId As Int64
    'Private _dtprocdate As Date

    Private Sub tsCatheterization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsCatheterization.ItemClicked
        Dim sErrorMsg As String
        Select Case e.ClickedItem.Tag
            Case "Save"

                sErrorMsg = ValidateForm()
                If sErrorMsg = "" Then
                    Try
                        Dim flagnodata As Integer = 0

                        If (_nPatientID = 0) OrElse (_nVisitId = 0) OrElse (IsNothing(_dtprocdate)) Then
                            _nPatientID = _nPatientID
                            Try
                                _nVisitId = GenerateVisitID(dtdop.Value.Date, _nPatientID)
                            Catch ex As Exception

                            End Try

                            _dtprocdate = dtdop.Value
                        End If

                        Deleterecord(_nPatientID, _nVisitId, _dtprocdate)

                        Dim lstcls_echo As New Cls_Echocardiograms
                        Dim maxcnt As Integer = 0
                        maxcnt = lstCPTcode.Items.Count
                        If (maxcnt < lstProcName.Items.Count) Then
                            maxcnt = lstProcName.Items.Count
                        End If
                        If (maxcnt < lstIDPhy.Items.Count) Then
                            maxcnt = lstIDPhy.Items.Count
                        End If
                        If maxcnt = 0 Then
                            maxcnt = 1
                            flagnodata = 1
                        End If
                        For cnt As Integer = 0 To maxcnt - 1


                            Dim objcls_echo As New Cls_Echocardiogram
                            objcls_echo.aortic = txt_arotic.Text
                            objcls_echo.avarea = txtav_area.Text

                            objcls_echo.dtprocdate = _dtprocdate
                            Try

                                If flagnodata = 0 Then
                                    objcls_echo.IDofinterpatphys = lstIDPhy.Items(cnt).ToString()
                                Else
                                    objcls_echo.IDofinterpatphys = ""
                                End If


                            Catch ex As Exception
                                objcls_echo.IDofinterpatphys = ""
                            End Try

                            Try
                                If flagnodata = 0 Then
                                    objcls_echo.scptcode = lstCPTcode.Items(cnt).ToString()
                                Else
                                    objcls_echo.scptcode = ""
                                End If

                            Catch ex As Exception
                                objcls_echo.scptcode = ""
                            End Try

                            objcls_echo.laarea = txt_laarea.Text
                            objcls_echo.lvedd = txt_lvedd.Text
                            objcls_echo.lvesdvc = txt_lvesd.Text
                            objcls_echo.LVDiastolic = txt_lvdiastvol.Text
                            objcls_echo.lvmass = txt_lvmass.Text
                            objcls_echo.lvpostwallthik = txt_lvthick.Text
                            objcls_echo.lvsystvol = txt_lvsystvol.Text
                            objcls_echo.mitral = txt_mitral.Text
                            objcls_echo.mvarea = txt_mvarea.Text
                            objcls_echo.Narrativesummary = txt_Nativesum.Text
                            objcls_echo.nPatientID = _nPatientID

                            objcls_echo.septalthik = txt_septhik.Text


                            Try

                                If flagnodata = 0 Then
                                    objcls_echo.sprocedures = lstProcName.Items(cnt).ToString()
                                Else
                                    objcls_echo.sprocedures = ""
                                End If


                            Catch ex As Exception
                                objcls_echo.sprocedures = ""
                            End Try


                            objcls_echo.VisitID = _nVisitId
                            objcls_echo.ExamID = 0
                            lstcls_echo.Add(objcls_echo)
                            objcls_echo.Dispose()
                            objcls_echo = Nothing
                        Next
                        Dim objcls As New clscv_echocardiogram


                        objcls.insert_echocardiogram(lstcls_echo, nTransaction)
                        objcls = Nothing

                    Catch ex As Exception
                    Finally
                        _nPatientID = 0
                        _nVisitId = 0
                        _dtprocdate = Nothing
                        ' Dim objviewecho As New frmCV_VWEChocardiogram()
                        'objviewecho.fillgriddata()
                        Me.Close()

                    End Try
                Else
                    MessageBox.Show(sErrorMsg, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "Close"
                Me.Close()
        End Select

    End Sub

    Private Sub Deleterecord(ByVal patientId As Int64, ByVal visitid As Int64, ByVal patdate As Date)
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""
        ' DataView dv = dt.DefaultView;

        Try

        

            _strSQL = "delete    FROM CV_Echocardiogram WHERE  nPatientID = " & patientId & " and nVisitID= " & visitid & " and  convert(varchar,dtproceduredate,101)= '" & Format(patdate, "MM/dd/yyyy") & "'"
            Dim b As Boolean = oDB.Delete_Query(_strSQL)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btncptbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncptbr.Click
        strlst = "CPT"
        LoadUserGrid()

        ''''''''
        SetCheckValues(lstCPTcode)
        ''''''''
        LockControls(Me, pnlcustomTask)
        ''''''''
    End Sub

    Private Sub SetCheckValues(ByVal LstBx As ListBox)
        Dim k As Integer
        If strlst = "CPT" Then
            Try

          
                For k = 0 To LstBx.Items.Count - 1
                    For i As Int32 = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                        If dgCustomGrid.GetItem(i, 1).ToString.Substring(0, dgCustomGrid.GetItem(i, 1).ToString.IndexOf("-")) = LstBx.Items(k).ToString.Substring(0, LstBx.Items(k).ToString().IndexOf("-")) Then
                            dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Exit For
                        End If
                    Next
                Next
            Catch ex As Exception

            End Try
        Else

            For k = 0 To LstBx.Items.Count - 1
                For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1

                    If dgCustomGrid.GetItem(i, 1).ToString.Trim = LstBx.Items(k).ToString.Trim Then
                        dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                        Exit For
                    End If
                Next
            Next
        End If

    End Sub
    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                pnlcustomTask.Width = 600
                pnlcustomTask.Height = 220
                dgCustomGrid.Width = pnlcustomTask.Width
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.txtsearch.Width = 150

                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        pnlcustomTask.Controls.Add(dgCustomGrid)
        pnlcustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64

        ''''''''''''''''''''''
        ''dgCustomGrid.lblCaption.Visible = True
        ''''''''''''''''''''''

        ' If strLst = "drugs" Then
      
        ' ElseIf strLst = "CPT" Then
        'y = LstTreatment.Bottom + 50
        'x = lblTreatment.Left - 10
        'ElseIf strLst = "diag" Then
        'y = LstDiagnosis.Bottom + 50
        'x = lblDiagnosis.Left
        ' End If

        If strlst = "UserPhy" Then
            y = lstIDPhy.Bottom + 130
            x = 360
            'dgCustomGrid.lblCaption.Text = " User/Physician List "
        End If
        If strlst = "CPT" Then
            y = lstCPTcode.Bottom + 130
            x = btncptbr.Left
            'dgCustomGrid.lblCaption.Text = " CPT List "
        End If
        If strlst = "Pname" Then
            y = lstProcName.Bottom + 130
            ' x = lblproc.Left
            x = 250
            'dgCustomGrid.lblCaption.Text = " Procedure List "
        End If

        pnlcustomTask.Location = New Point(x, y)
        pnlcustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlcustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub
    'Remove customGrid control to form 
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlcustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub BindUserGrid()
        Try

            'If strLst = "drugs" Then
            '    dt = FillDrugs()
            'ElseIf strLst = "CPT" Then
            If strlst = "UserPhy" Then
                dt = PopulateUserDt()                
            End If
            If strlst = "CPT" Then
                dt = FillCPT()               
            End If
            If strlst = "Pname" Then
                dt = getProcedure()                
            End If
            'ElseIf strLst = "diag" Then
            'dt = FillDiagnosis()
            'End If
            pnlcustomTask.Width = dgCustomGrid.Width

            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            'dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            dgCustomGrid.C1Task.ExtendLastCol = True
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            ' dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            'dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.5
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function IsNumber(ByVal strNumber As [String]) As Boolean
        Try
            Dim objNotNumberPattern As New Regex("[^0-9.-]")
            Dim objTwoDotPattern As New Regex("[0-9]*[.][0-9]*[.][0-9]*")
            Dim objTwoMinusPattern As New Regex("[0-9]*[-][0-9]*[-][0-9]*")
            Dim strValidRealPattern As [String] = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$"
            Dim strValidIntegerPattern As [String] = "^([-]|[0-9])[0-9]*$"
            Dim objNumberPattern As New Regex("(" & strValidRealPattern & ")|(" & strValidIntegerPattern & ")")
            Return Not objNotNumberPattern.IsMatch(strNumber) AndAlso Not objTwoDotPattern.IsMatch(strNumber) AndAlso Not objTwoMinusPattern.IsMatch(strNumber) AndAlso objNumberPattern.IsMatch(strNumber)

            'objNotNumberPattern = Nothing
            'objTwoDotPattern = Nothing
            'objTwoMinusPattern = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Public Function IsPositiveNumber(ByVal strNumber As [String]) As Boolean
        Try
            Dim objNotPositivePattern As New Regex("[^0-9.]")
            Dim objPositivePattern As New Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$")
            Dim objTwoDotPattern As New Regex("[0-9]*[.][0-9]*[.][0-9]*")
            Return Not objNotPositivePattern.IsMatch(strNumber) AndAlso objPositivePattern.IsMatch(strNumber) AndAlso Not objTwoDotPattern.IsMatch(strNumber)

            'objNotPositivePattern = Nothing
            'objPositivePattern = Nothing
            'objTwoDotPattern = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Count
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "Name")
            .Cols(Col_Name).Width = _TotalWidth * 0.45
            ' .Cols(Col_DrugName).AllowEditing = False

            ' .SetData(0, Col_Dosage, "Dosage")
            ' .Cols(Col_Dosage).Width = _TotalWidth * 0.45
            ' .Cols(Col_Dosage).AllowEditing = False
            ''move the last column to select column
            '.Cols.Move(.Cols.Count - 1, 0)
            'dgCustomGrid.C1Task.SetCellCheck(3, 0, CheckEnum.Checked)

        End With

    End Sub


    Public Function FillCPT() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try
            ''    _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            ''_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            _strSQL = "select Distinct (isNull(rtrim(sCPTCode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where  sCPTCode <>'' AND sDescription<>''"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function


    Public Function getProcedure()
       



        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try
            ''    _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            ''_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            _strSQL = "select isnull( sDescription,'') as [Procedure Name] from Category_MST where sCategoryType='Procedures'"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try



    End Function


    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
        ''''''''
        LockControls(Me, pnlcustomTask, True)
        ''''''''
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            ''''''''
            LockControls(Me, pnlcustomTask, True)
            ''''''''

            If strlst = "UserPhy" Then
                lstIDPhy.Items.Clear()
            End If
            If strlst = "CPT" Then
                lstCPTcode.Items.Clear()
            End If
            If strlst = "Pname" Then
                lstProcName.Items.Clear()

            End If



            Dim i As Int32
            '''''''''''''''''''''
            dgCustomGrid.txtsearch.Text = ""
            '''''''''''''''''''''
            For i = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    'If strLst = "drugs" Then
                    '    LstMedication.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    'ElseIf strLst = "CPT" Then
                    '    LstTreatment.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    'ElseIf strLst = "diag" Then
                    '    LstDiagnosis.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    'End If


                    If strlst = "UserPhy" Then
                        lstIDPhy.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    End If
                    If strlst = "CPT" Then
                        lstCPTcode.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)

                    End If
                    If strlst = "Pname" Then
                        lstProcName.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)

                    End If

                   
                End If
            Next

            ''  LstDiagnosis.Enabled = True
            pnlcustomTask.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub


    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            'CType(, DataView)
            'CType(dgCustomGrid.datasource(dt.DefaultView), DataView)
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGrid.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGrid.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(0).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            'OR " _
            '                                                 & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
            '                                                & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "



            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btncptcls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncptcls.Click
        ' While (lstCPTcode.SelectedItems.Count > 0)
        If lstCPTcode.SelectedItems.Count > 0 Then
            lstCPTcode.Items.Remove(lstCPTcode.SelectedItems(0))
        End If
    End Sub

    Private Sub btncptclr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncptclr.Click
        lstCPTcode.Items.Clear()
    End Sub
    Public Function PopulateUserDt() As DataTable
       

        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try
            ''    _strSQL = "select Distinct sICD9Code ,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            ''_strSQL = "select Distinct (isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"
            _strSQL = "Select sLoginName +'-'  + isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as [Login Name]  from User_MST"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try


    End Function
    Private Sub pnlSelectFromList_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlSelectFromList.Paint

    End Sub

    Private Sub btnpnamebr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpnamebr.Click
        strlst = "Pname"
        LoadUserGrid()
        SetCheckValues(lstProcName)
        ''''''''
        LockControls(Me, pnlcustomTask)
        ''''''''
    End Sub

    Private Sub btnpnamecls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpnamecls.Click



        If lstProcName.SelectedItems.Count > 0 Then
            lstProcName.Items.Remove(lstProcName.SelectedItems(0))

        End If


    End Sub

    Private Sub btnpnameclr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpnameclr.Click
        lstProcName.Items.Clear()
    End Sub

    Private Sub btnidbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnidbr.Click
        strlst = "UserPhy"
        LoadUserGrid()
        SetCheckValues(lstIDPhy)
        ''''''''
        LockControls(Me, pnlcustomTask)
        ''''''''
    End Sub

    Private Sub btnidcls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnidcls.Click

        If lstIDPhy.SelectedItems.Count > 0 Then
            lstIDPhy.Items.Remove(lstIDPhy.SelectedItems(0))
        End If


    End Sub

    Private Sub bntidclr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntidclr.Click


        lstIDPhy.Items.Clear()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        '_nPatientID = gnPatientID
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub




    Public Sub New(ByVal nPatientID As Int64, ByVal nVisitId As Int64, ByVal dtprocdate As Date)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _nPatientID = nPatientID
        _nVisitId = nVisitId
        _dtprocdate = dtprocdate
        filldata(nPatientID, nVisitId, dtprocdate)
        dtdop.Enabled = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub filldata(ByVal nPatientID As Int64, ByVal nVisitId As Int64, ByVal dtprocdate As Date)
        Dim oDB As New DataBaseLayer
        Dim _dt As DataTable
        Try

            Dim _strSQL As String = ""

            '  _strSQL = "Select *  from cv_Echocardiogram where npatientid=" & nPatientID & "  and nVisitID=" & nVisitId & " and convert(varchar,dtproceduredate,101)='" & Format(dtprocdate, "MM/dd/yyyy") & "'"   'and nExamId="& nExamId &"

            _strSQL = "select isnull(nEchocardiogramID,0)as nEchocardiogramID,isnull(sAortic,'')as sAortic,isnull(sLAArea,'')as 'sLAArea',isnull(sLVDiastvol,'')as sLVDiastvol,isnull(sLvedd,'')as sLvedd,isnull(sLVesd,'')as sLVesd,isnull(sLVMass,'')as sLVMass,isnull(sLVsystvol,'')as sLVsystvol,isnull(sLVpostwallthick,'')as sLVpostwallthick, isnull(sSeptalthick,'')as sSeptalthick,isnull(sMitral,'')as sMitral,isnull(smvarea,'')as smvarea,isnull(sAVArea,'')as sAVArea,isnull(sIDofintrepertingphys,'')as sIDofintrepertingphys,isnull(sCPTCode,'')as sCPTCode,isnull(sNarrativeSummary,'')as sNarrativeSummary,isnull(sProcedures,'')as sProcedures,isnull(nPatientId,0)as nPatientId,isnull(nvisitId,0)as nvisitId,isnull(nExamId,0)as nExamId,isnull(convert(varchar,dtproceduredate,101),'') as dtproceduredate   FROM CV_Echocardiogram where npatientid=" & nPatientID & "  and nVisitID=" & nVisitId & " and convert(varchar,dtproceduredate,101)='" & Format(dtprocdate, "MM/dd/yyyy") & "'"   'and nExamId="& nExamId &"

            _dt = oDB.GetDataTable_Query(_strSQL)
            Dim _str As String = ""
            If Not _dt Is Nothing Then
                For rec_cnt As Integer = 0 To _dt.Rows.Count - 1
                    If rec_cnt = 0 Then
                        dtdop.Value = dtprocdate
                        txt_arotic.Text = _dt.Rows(rec_cnt)("sAortic").ToString()
                        txt_laarea.Text = _dt.Rows(rec_cnt)("sLAArea").ToString()
                        txt_lvdiastvol.Text = _dt.Rows(rec_cnt)("sLVDiastvol").ToString()
                        txt_lvedd.Text = _dt.Rows(rec_cnt)("sLvedd").ToString()
                        txt_lvesd.Text = _dt.Rows(rec_cnt)("sLVesd").ToString()
                        txt_lvmass.Text = _dt.Rows(rec_cnt)("sLVMass").ToString()
                        txt_lvsystvol.Text = _dt.Rows(rec_cnt)("sLVsystvol").ToString()
                        txt_lvthick.Text = _dt.Rows(rec_cnt)("sLVpostwallthick").ToString()
                        txt_septhik.Text = _dt.Rows(rec_cnt)("sSeptalthick").ToString()
                        txt_mitral.Text = _dt.Rows(rec_cnt)("sMitral").ToString()
                        txt_mvarea.Text = _dt.Rows(rec_cnt)("smvarea").ToString()
                        txt_Nativesum.Text = _dt.Rows(rec_cnt)("sNarrativeSummary").ToString()
                        txtav_area.Text = _dt.Rows(rec_cnt)("sAVArea").ToString()
                        If _dt.Rows(rec_cnt)("sCPTCode").ToString() <> "" Then
                            lstCPTcode.Items.Add(_dt.Rows(rec_cnt)("sCPTCode"))
                        End If
                        If _dt.Rows(rec_cnt)("sIDofintrepertingphys").ToString() <> "" Then
                            lstIDPhy.Items.Add(_dt.Rows(rec_cnt)("sIDofintrepertingphys"))
                        End If
                        If _dt.Rows(rec_cnt)("sProcedures").ToString() <> "" Then
                            lstProcName.Items.Add(_dt.Rows(rec_cnt)("sProcedures"))
                        End If




                    Else

                        If _dt.Rows(rec_cnt)("sCPTCode").ToString() <> "" Then
                            lstCPTcode.Items.Add(_dt.Rows(rec_cnt)("sCPTCode"))
                        End If
                        If _dt.Rows(rec_cnt)("sIDofintrepertingphys").ToString() <> "" Then
                            lstIDPhy.Items.Add(_dt.Rows(rec_cnt)("sIDofintrepertingphys"))
                        End If
                        If _dt.Rows(rec_cnt)("sProcedures").ToString() <> "" Then
                            lstProcName.Items.Add(_dt.Rows(rec_cnt)("sProcedures"))
                        End If
                    End If



                Next




            End If

        Catch ex As Exception
            '    Return Nothing

            ' oDB = Nothing
        Finally
            oDB = Nothing
            _dt = Nothing
        End Try


    End Sub
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub frmCV_Echocardiogram_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(gloUC_PatientStrip1) = False) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmCV_Echocardiogram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        pnlToolStrip.SendToBack()
        gloUC_PatientStrip1.ShowDetail(_nPatientID, gloUC_PatientStrip.enumFormName.None)
        Me.Controls.Add(gloUC_PatientStrip1)
        lstCPTcode.Focus()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

Private Sub Panel2_Paint( ByVal sender As System.Object,  ByVal e As System.Windows.Forms.PaintEventArgs)

End Sub

    Private Sub lstCPTcode_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCPTcode.MouseMove
        objCath.SetListBoxToolTip(lstCPTcode, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstProcName_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstProcName.MouseMove
        objCath.SetListBoxToolTip(lstProcName, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstIDPhy_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstIDPhy.MouseMove
        objCath.SetListBoxToolTip(lstIDPhy, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub txt_lvedd_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_lvedd.Validating
        Try
            If Not IsPositiveNumber(txt_lvedd.Text) AndAlso Not txt_lvedd.Text = Nothing Then
                MessageBox.Show("LVEDD is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_lvedd.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_lvesd_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_lvesd.Validating
        Try
            If Not IsPositiveNumber(txt_lvesd.Text) AndAlso Not txt_lvesd.Text = Nothing Then
                MessageBox.Show("LVESD is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_lvesd.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_lvthick_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_lvthick.Validating
        Try
            If Not IsPositiveNumber(txt_lvthick.Text) AndAlso Not txt_lvthick.Text = Nothing Then
                MessageBox.Show("LV Posterior Wall Thickness is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_lvthick.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_septhik_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_septhik.Validating
        Try
            If Not IsPositiveNumber(txt_septhik.Text) AndAlso Not txt_septhik.Text = Nothing Then
                MessageBox.Show("Septal Thickness is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_septhik.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_arotic_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_arotic.Validating
        Try
            If Not IsPositiveNumber(txt_arotic.Text) AndAlso Not txt_arotic.Text = Nothing Then
                MessageBox.Show("Arotic is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_arotic.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_laarea_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_laarea.Validating
        Try
            If Not IsPositiveNumber(txt_laarea.Text) AndAlso Not txt_laarea.Text = Nothing Then
                MessageBox.Show("LA Area is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_laarea.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtav_area_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtav_area.Validating
        Try
            If Not IsPositiveNumber(txtav_area.Text) AndAlso Not txtav_area.Text = Nothing Then
                MessageBox.Show("AV Area is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtav_area.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_mvarea_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_mvarea.Validating
        Try
            If Not IsPositiveNumber(txt_mvarea.Text) AndAlso Not txt_mvarea.Text = Nothing Then
                MessageBox.Show("MV Area is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_mvarea.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_lvmass_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_lvmass.Validating
        Try
            If Not IsPositiveNumber(txt_lvmass.Text) AndAlso Not txt_lvmass.Text = Nothing Then
                MessageBox.Show("LV Mass is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_lvmass.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ValidateForm() As String
        Dim sReturnMessage As String = ""
        Try
            If Not IsPositiveNumber(txt_lvedd.Text) AndAlso Not txt_lvedd.Text = Nothing Then
                sReturnMessage = "LVEDD is not in correct format. "
                txt_lvedd.Focus()
                Exit Try
            End If


            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_lvesd.Text) AndAlso Not txt_lvesd.Text = Nothing Then
                    sReturnMessage = "LVESD is not in correct format. "
                    txt_lvesd.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_lvthick.Text) AndAlso Not txt_lvthick.Text = Nothing Then
                    sReturnMessage = "LV Posterior Wall Thickness is not in correct format. "
                    txt_lvthick.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_septhik.Text) AndAlso Not txt_septhik.Text = Nothing Then
                    sReturnMessage = "Septal Thickness is not in correct format. "
                    txt_septhik.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_arotic.Text) AndAlso Not txt_arotic.Text = Nothing Then
                    sReturnMessage = "Arotic is not in correct format. "
                    txt_arotic.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_laarea.Text) AndAlso Not txt_laarea.Text = Nothing Then
                    sReturnMessage = "LA Area is not in correct format. "
                    txt_laarea.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtav_area.Text) AndAlso Not txtav_area.Text = Nothing Then
                    sReturnMessage = "AV Area is not in correct format. "
                    txtav_area.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_mvarea.Text) AndAlso Not txt_mvarea.Text = Nothing Then
                    sReturnMessage = "MV Area is not in correct format. "
                    txt_mvarea.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_lvmass.Text) AndAlso Not txt_lvmass.Text = Nothing Then
                    sReturnMessage = "LV Mass is not in correct format. "
                    txt_lvmass.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return sReturnMessage

    End Function

   
End Class