Imports C1.Win.C1FlexGrid

Public Class frmBusinessCenterRulesSetup

    Dim _ClinicID As Int64 = 1



    ''Column constants for billing grid
    Dim COL_Sort As Integer = 0
    Dim COL_RuleId As Integer = 1
    Dim COL_PRI As Integer = 2
    Dim COL_BillProvider As Integer = 3
    Dim COL_Facility As Integer = 4
    Dim COL_BusCenter As Integer = 5
  
    Dim _messageBoxCaption As String = gloGlobal.gloPMGlobal.MessageBoxCaption


    Dim dtProvider As New DataTable()
    Dim dtFAcility As New DataTable()
    Dim dtBusinessCenter As New DataTable()

    Private Sub frmBusinessCenterRulesSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DesignGrid()
        SetAllBusinessCenterRules()
    End Sub

    Private Sub DesignGrid()
        Try
            Dim dtfacility As DataTable = GetFacilities()

            Dim _strFacility As String

            If Not dtfacility Is Nothing Then
                If dtfacility.Rows.Count > 0 Then
                    For i As Integer = 0 To dtfacility.Rows.Count - 1

                        _strFacility = _strFacility & "|" & dtfacility.Rows(i)("sfacilityname").ToString()

                    Next
                End If
            End If


            Dim dtProviders As DataTable = GetProvidersForProviderSetting()

            Dim _strProviders As String
            If Not dtProviders Is Nothing Then
                If dtProviders.Rows.Count > 0 Then
                    For i As Integer = 0 To dtProviders.Rows.Count - 1

                        _strProviders = _strProviders & "|" & dtProviders.Rows(i)("ProviderName").ToString()

                    Next
                End If
            End If



            Dim dtBusCenter As DataTable = GetBusinessCenter()

            Dim _strBusCenter As String
            If Not dtBusCenter Is Nothing Then
                If dtBusCenter.Rows.Count > 0 Then
                    For i As Integer = 0 To dtBusCenter.Rows.Count - 1

                        _strBusCenter = _strBusCenter & "|" & dtBusCenter.Rows(i)("sBusinessCenter").ToString()

                    Next
                End If
            End If


            c1BusinessCenter.Cols(COL_BillProvider).ComboList = " |" & _strProviders
            c1BusinessCenter.Cols(COL_BusCenter).ComboList = " |" & _strBusCenter
            c1BusinessCenter.Cols(COL_Facility).ComboList = " |" & _strFacility

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Function GetFacilities() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = ""
            _sqlQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName " _
                    & " FROM   BL_Facility_MST WHERE bIsBlocked = '" & False & "' AND nClinicID = " & _ClinicID & " "


            odb.Connect(gloGlobal.gloPMGlobal.DatabaseConnectionString())
            dtFAcility = odb.ReadQueryData(_sqlQuery)
            Return dtFAcility

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function



    Private Function ValiDate() As Boolean

        c1BusinessCenter.FinishEditing()

        Dim Result As Boolean = True

        Try

            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1
                If ((c1BusinessCenter.GetData(i, COL_PRI) Is Nothing) Or (Convert.ToString(c1BusinessCenter.GetData(i, COL_PRI)).Trim() = "") Or (Convert.ToString(c1BusinessCenter.GetData(i, COL_PRI)) = "0")) Then

                    MessageBox.Show("Priority is not found. Please assign priority.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1BusinessCenter.[Select](i, COL_PRI)
                    c1BusinessCenter.Focus()
                    Result = False
                    Return Result

                End If
            Next


            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1
                If ((c1BusinessCenter.GetData(i, COL_BillProvider) Is Nothing) Or (Convert.ToString(c1BusinessCenter.GetData(i, COL_BillProvider)).Trim() = "")) And ((c1BusinessCenter.GetData(i, COL_Facility) Is Nothing) Or (Convert.ToString(c1BusinessCenter.GetData(i, COL_Facility)).Trim() = "")) Then

                    MessageBox.Show("Select provider or facility.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1BusinessCenter.[Select](i, COL_PRI)
                    c1BusinessCenter.Focus()
                    Result = False
                    Return Result

                End If
            Next


            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1
                If (c1BusinessCenter.GetData(i, COL_BusCenter) Is Nothing) Or (Convert.ToString(c1BusinessCenter.GetData(i, COL_BusCenter)).Trim() = "") Then

                    MessageBox.Show("Business Center is not found. Please assign Business Center.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1BusinessCenter.[Select](i, COL_PRI)
                    c1BusinessCenter.Focus()
                    Result = False

                    Return Result

                End If

            Next





            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1
                Dim _Party As Int16 = 0
                If c1BusinessCenter.GetData(i, COL_PRI) IsNot Nothing Then
                    If Int16.TryParse(c1BusinessCenter.GetData(i, COL_PRI).ToString(), _Party) = True Then
                        If _Party > 0 Then

                            For j As Integer = i + 1 To c1BusinessCenter.Rows.Count - 1
                                Dim _NextParty As Int16 = 0
                                If Int16.TryParse(c1BusinessCenter.GetData(j, COL_PRI).ToString(), _NextParty) = True Then
                                    If _Party = _NextParty Then
                                        MessageBox.Show("Same priority is found for multiple rules. Please assign unique priority.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        c1BusinessCenter.[Select](i, COL_PRI)
                                        c1BusinessCenter.Focus()
                                        Result = False
                                        Return Result
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Next



            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1
                Dim _Party As Int16 = 0
                If c1BusinessCenter.GetData(i, COL_PRI) IsNot Nothing AndAlso c1BusinessCenter.GetData(i, COL_PRI).ToString().Length > 0 Then
                    If Int16.TryParse(c1BusinessCenter.GetData(i, COL_PRI).ToString(), _Party) = True Then
                        If _Party <> i Then
                            MessageBox.Show("Priority is out of order. Please specify priority in sequence. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            c1BusinessCenter.[Select](i, COL_PRI)
                            c1BusinessCenter.Focus()
                            Result = False
                            Return Result
                        End If
                    End If
                Else
                    Exit For
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try

        Return Result


    End Function





    Public Function GetProvidersForProviderSetting() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString())
        Dim _strSQL As [String] = ""

        Try
            If Me._ClinicID = 0 Then
                _strSQL = "SELECT nProviderID , dbo.GET_NAME(sFirstName,sMiddleName,sLastName)ProviderName  " _
                          & " FROM Provider_MST LEFT JOIN ProviderType_MST ON Provider_MST.nProviderType = ProviderType_MST.nProviderTypeID FROM Provider_MST ORDER BY ProviderName"
            Else
                _strSQL = "SELECT nProviderID , dbo.GET_NAME(sFirstName,sMiddleName,sLastName)ProviderName  " _
                          & " FROM Provider_MST LEFT JOIN ProviderType_MST ON Provider_MST.nProviderType = ProviderType_MST.nProviderTypeID WHERE nClinicID = " & Me._ClinicID & " ORDER BY ProviderName"
            End If

            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtProvider)
            oDB.Disconnect()
            If dtProvider IsNot Nothing AndAlso dtProvider.Rows.Count > 0 Then
                Return dtProvider
            Else
                Return Nothing
            End If
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            MessageBox.Show(dbex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function



    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Dim res As DialogResult = MessageBox.Show("Do you want to save changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If res = DialogResult.No Then
            Me.Close()
        ElseIf res = DialogResult.Yes Then
            SaveRules()
        End If
    End Sub

    Private Function GetBusinessCenter() As DataTable
        Try

            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = ""
            _sqlQuery = "SELECT [nBusinessCenterID]      ,[sBusinessCenterCode]+'-'+[sDescription] As sBusinessCenter" _
                    & " FROM  BL_BusinessCenterCodes "


            odb.Connect(gloGlobal.gloPMGlobal.DatabaseConnectionString())
            dtBusinessCenter = odb.ReadQueryData(_sqlQuery)
            Return dtBusinessCenter

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub ts_btnAddLine_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnAddLine.Click
        Try
            c1BusinessCenter.Rows.Add()
            c1BusinessCenter.SetData(c1BusinessCenter.Rows.Count - 1, COL_Sort, 999)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub tsb_btnRemoveLine_Click(sender As System.Object, e As System.EventArgs) Handles tsb_btnRemoveLine.Click
        Try
            If c1BusinessCenter IsNot Nothing AndAlso c1BusinessCenter.Rows.Count > 1 Then
                Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If res = DialogResult.Yes Then
                    Dim i As Integer

                    Dim rowIndex As Integer = c1BusinessCenter.RowSel
                    c1BusinessCenter.Rows.Remove(rowIndex)
                    ReOrderRules()
                    ReAssignRules()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub c1BusinessCenter_KeyPressEdit(sender As System.Object, e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles c1BusinessCenter.KeyPressEdit
        Try
            If (c1BusinessCenter.ColSel = COL_PRI) Then
                If (Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar)) Or (e.KeyChar = "") Then
                    e.Handled = True
                Else
                    e.Handled = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub c1BusinessCenter_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1BusinessCenter.MouseDown
        Try
            Dim tempRow As Integer = 0
            tempRow = c1BusinessCenter.HitTest(e.X, e.Y).Row
            If tempRow <> -1 And tempRow <> 0 Then
                c1BusinessCenter.Row = tempRow
                c1BusinessCenter.ContextMenuStrip = cmnu_Row
            Else
                c1BusinessCenter.ContextMenuStrip = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub


    Private Sub mnuItem_Add_Above_Click(sender As System.Object, e As System.EventArgs) Handles mnuItem_Add_Above.Click
        Try
            Dim rowIndex As Integer
            If c1BusinessCenter IsNot Nothing AndAlso c1BusinessCenter.Rows.Count > 1 Then
                rowIndex = c1BusinessCenter.RowSel
            End If
            c1BusinessCenter.Rows.Insert(rowIndex)
            If (Convert.ToString(c1BusinessCenter.GetData(rowIndex - 1, COL_PRI)).Trim() = "") Then
                c1BusinessCenter.SetData(rowIndex, COL_Sort, 999)
            Else
                c1BusinessCenter.SetData(rowIndex, COL_Sort, rowIndex)
                c1BusinessCenter.SetData(rowIndex, COL_PRI, rowIndex)
                ReOrderRules()
                ReAssignRules()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub mnuItem_Add_Below_Click(sender As System.Object, e As System.EventArgs) Handles mnuItem_Add_Below.Click
        Try
            Dim rowIndex As Integer
            If c1BusinessCenter IsNot Nothing AndAlso c1BusinessCenter.Rows.Count > 1 Then
                rowIndex = c1BusinessCenter.RowSel
            End If
            c1BusinessCenter.Rows.Insert(rowIndex + 1)
           
            If (Convert.ToString(c1BusinessCenter.GetData(rowIndex, COL_PRI)).Trim() = "") Then
                c1BusinessCenter.SetData(rowIndex + 1, COL_Sort, 999)
            Else
                c1BusinessCenter.SetData(rowIndex + 1, COL_Sort, rowIndex + 1)
                c1BusinessCenter.SetData(rowIndex + 1, COL_PRI, rowIndex + 1)
                ReOrderRules()
                ReAssignRules()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub mnuItem_Delete_Click(sender As System.Object, e As System.EventArgs) Handles mnuItem_Delete.Click
        Try
            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = DialogResult.Yes Then
                Dim i As Integer
                If c1BusinessCenter IsNot Nothing AndAlso c1BusinessCenter.Rows.Count > 1 Then
                    Dim rowIndex As Integer = c1BusinessCenter.RowSel
                    c1BusinessCenter.Rows.Remove(rowIndex)
                    ReOrderRules()
                    ReAssignRules()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ReAssignRules()
        Dim i As Integer
        For i = 1 To c1BusinessCenter.Rows.Count - 1
            If (c1BusinessCenter.GetData(i, COL_Sort) <> 999) Then
                c1BusinessCenter.SetData(i, COL_PRI, i)
                c1BusinessCenter.SetData(i, COL_Sort, i)
            End If
        Next
    End Sub


    Private Sub ReOrderRules()

        c1BusinessCenter.Sort(SortFlags.Ascending, COL_Sort)
        'Dim j As Integer = c1BusinessCenter.RowSel + 1
        'Dim i As Integer
        'For i = j To c1BusinessCenter.Rows.Count - 1
        '    If (c1BusinessCenter.GetData(i, COL_Sort) <> 999) Then
        '        c1BusinessCenter.SetData(i, COL_PRI, i)
        '        c1BusinessCenter.SetData(i, COL_Sort, i)
        '    End If
        'Next
    End Sub


    Private Sub tsb_Saveclose_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Saveclose.Click
        SaveRules()
    End Sub

    Private Sub SaveRules()
        Dim dt As New DataTable()

        Try
            If (ValiDate()) Then
                dt = GetdataTOsave()
                If (ValidateData(dt)) Then
                    Dim objBusinessCenter As New ClsBusinessCenter()
                    objBusinessCenter.SaveBusinessCenterRules(dt)
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub


    Private Function ValidateData(ByVal Dt As DataTable) As Boolean

        Dim Result As Boolean = True
        Dim DtData As New DataTable()

        Try
            DtData = Dt.Copy
            DtData.Columns.Remove("RuleID")
            DtData.Columns.Remove("Priority")
            DtData.Columns.Remove("BusinessCenterID")


            Dim distinctData As DataTable = DtData.DefaultView.ToTable(True)
            If (distinctData.Rows.Count <> DtData.Rows.Count) Then
                MessageBox.Show("Duplicate records. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Result = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

        Return Result

    End Function


    Private Sub c1BusinessCenter_CellChanged(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1BusinessCenter.CellChanged
        If e.Col = COL_PRI Then
            If c1BusinessCenter.GetData(e.Row, COL_PRI).ToString.Trim() = "" Then
                c1BusinessCenter.SetData(e.Row, COL_Sort, 999)
            Else
                c1BusinessCenter.SetData(e.Row, COL_Sort, c1BusinessCenter.GetData(e.Row, COL_PRI))
            End If
            ReOrderRules()
        End If
    End Sub

    Private Sub c1BusinessCenter_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1BusinessCenter.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Function GetdataTOsave() As DataTable

        Dim dtgrid As DataTable = New DataTable()

        Try

            dtgrid.Columns.Add("RuleID")
            dtgrid.Columns.Add("Priority")
            dtgrid.Columns.Add("ProviderID")
            dtgrid.Columns.Add("FacilityID")
            dtgrid.Columns.Add("BusinessCenterID")

            Dim _RuleId As String = ""
            Dim _ProviderID As String = ""
            Dim _FacilityID As String = ""
            Dim _BusinessCenterID As String = ""
            Dim _Priority As String = ""


            For i As Integer = 1 To c1BusinessCenter.Rows.Count - 1

                _RuleId = ""
                _ProviderID = ""
                _FacilityID = ""
                _BusinessCenterID = ""
                _Priority = ""

                _RuleId = c1BusinessCenter.GetData(i, COL_RuleId)

                _Priority = c1BusinessCenter.GetData(i, COL_PRI)


                If Not IsNothing(c1BusinessCenter.GetData(i, COL_BillProvider)) Then
                    If (c1BusinessCenter.GetData(i, COL_BillProvider).ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProvider.[Select]("ProviderName = '" & Convert.ToString(c1BusinessCenter.GetData(i, COL_BillProvider)).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _ProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _ProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(c1BusinessCenter.GetData(i, COL_Facility)) Then
                    If (c1BusinessCenter.GetData(i, COL_Facility).ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtFAcility.[Select]("sFacilityName = '" & Convert.ToString(c1BusinessCenter.GetData(i, COL_Facility)).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _FacilityID = Convert.ToInt64(_dr(0).Item("nFacilityID"))
                        Else
                            _FacilityID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(c1BusinessCenter.GetData(i, COL_BusCenter)) Then
                    If (c1BusinessCenter.GetData(i, COL_BusCenter).ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtBusinessCenter.[Select]("sBusinessCenter = '" & Convert.ToString(c1BusinessCenter.GetData(i, COL_BusCenter)).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _BusinessCenterID = Convert.ToInt64(_dr(0).Item("nBusinessCenterID"))
                        Else
                            _BusinessCenterID = Nothing

                        End If

                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("RuleID") = _RuleId
                _drNew("Priority") = _Priority
                _drNew("ProviderID") = _ProviderID
                _drNew("FacilityID") = _FacilityID
                _drNew("BusinessCenterID") = _BusinessCenterID

                dtgrid.Rows.Add(_drNew)

            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

        Return dtgrid

    End Function

    Private Sub SetAllBusinessCenterRules()
        Dim dtRules As New DataTable()

        Try

            Dim objBusinessCenter As New ClsBusinessCenter()
            dtRules = objBusinessCenter.GetBusinessCenterRules()

            If dtRules IsNot Nothing AndAlso dtRules.Rows.Count > 0 Then

                For i As Integer = 0 To dtRules.Rows.Count - 1

                    c1BusinessCenter.Rows.Insert(i + 1)
                    c1BusinessCenter.SetData(i + 1, COL_PRI, dtRules.Rows(i)("npriority").ToString())
                    c1BusinessCenter.SetData(i + 1, COL_Sort, dtRules.Rows(i)("npriority").ToString())
                    c1BusinessCenter.SetData(i + 1, COL_RuleId, dtRules.Rows(i)("nruleid").ToString())
                    c1BusinessCenter.SetData(i + 1, COL_BillProvider, dtRules.Rows(i)("ProviderName").ToString())
                    c1BusinessCenter.SetData(i + 1, COL_Facility, dtRules.Rows(i)("sFacilityName").ToString())
                    c1BusinessCenter.SetData(i + 1, COL_BusCenter, dtRules.Rows(i)("sBusinessCenter").ToString())
                    c1BusinessCenter.Refresh()

                Next

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub mnuBilling_AddLine_Click(sender As System.Object, e As System.EventArgs) Handles mnuBilling_AddLine.Click
        ts_btnAddLine.PerformClick()
    End Sub

    Private Sub mnuBilling_RemoveLine_Click(sender As System.Object, e As System.EventArgs) Handles mnuBilling_RemoveLine.Click
        tsb_btnRemoveLine.PerformClick()
    End Sub

    Private Sub mnuBilling_Save_Click(sender As System.Object, e As System.EventArgs) Handles mnuBilling_Save.Click
        tsb_Saveclose.PerformClick()
    End Sub

    Private Sub mnuBilling_Close_Click(sender As System.Object, e As System.EventArgs) Handles mnuBilling_Close.Click
        ts_btnClose.PerformClick()
    End Sub

    Private Sub c1BusinessCenter_SetupEditor(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1BusinessCenter.SetupEditor
        If (e.Col = COL_PRI) Then
            CType(c1BusinessCenter.Editor, TextBox).MaxLength = 3
        End If
    End Sub

    Private Sub c1BusinessCenter_StartEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1BusinessCenter.StartEdit
        If (e.Col = COL_PRI) Then
            c1BusinessCenter.Editor = CType(c1BusinessCenter.Editor, TextBox)
        End If
    End Sub

   
    Private Sub c1BusinessCenter_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles c1BusinessCenter.KeyUp
        If (c1BusinessCenter.ColSel = COL_PRI) Then
            If (e.KeyCode = Keys.Delete) Then
                c1BusinessCenter.SetData(c1BusinessCenter.RowSel, COL_PRI, "")
                ReOrderRules()
                ReAssignRules()
            End If
        End If
    End Sub
End Class
