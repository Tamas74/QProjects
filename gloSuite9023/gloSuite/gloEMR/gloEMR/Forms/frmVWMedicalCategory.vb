Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid

Public Class frmVWMedicalCategory

#Region "Variable declaration"


    Dim UnsentOrdersrowIndex As Integer
    Dim dvData As DataView = Nothing
    Dim strSortColumnName As String = Nothing
    Dim strSortOrder As String = Nothing
    Dim strSortExprn As String = String.Empty
    Dim nMedicalCat As Long = 0
#End Region

#Region "Form Load"

    Private Sub frmVWMedicalCategory_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

        
            If (Not IsNothing(dgMedicalCategoryList.DataSource)) Then
                dgMedicalCategoryList.Clear()
                CType(dgMedicalCategoryList.DataSource, DataView).Dispose()
                dgMedicalCategoryList.DataSource = Nothing
            End If
            If Not dvData Is Nothing Then
                dvData.Dispose()
                dvData = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmVWMedicalCategory_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            ''dgMedicalCategoryList.Cols(5).Style.
            ''style()
            FillGridMedicalCategory()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillGridMedicalCategory(Optional ByVal MedCatId As Long = 0)
        Try
            Dim dt As DataTable = RetrieveMedicalCategoryData()
            If dt.Rows.Count > 0 Then
                tsbtn_Act_Deact_Rule.Visible = True
            Else
                tsbtn_Act_Deact_Rule.Visible = False
            End If
            ''  dgMedicalCategoryList.Cols.
            '  dgMedicalCategoryList.Cols.Count = 6
            dvData = dt.DefaultView
            dgMedicalCategoryList.DataSource = dvData
            dgMedicalCategoryList.Cols(4).Caption = "Category Ranking"
            dgMedicalCategoryList.Cols(5).Caption = "Banner Color"
            '  dgMedicalCategoryList.SelectionMode = SelectionModeEnum.Cell
            'dgMedicalCategoryList.SelectionMode=C1FlexGrid.  
            dgMedicalCategoryList.Cols(5).Selected = False
            '' SetColor(MedCatId)
            FilterRecord(MedCatId)
            dgMedicalCategoryList.Cols(4).Width = 130
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SetColor(Optional ByVal MedicalCatid As Long = 0)
        Dim cstyle As C1.Win.C1FlexGrid.CellStyle
        Try
            If (dgMedicalCategoryList.Styles.Contains("nStyle")) Then
                cstyle = dgMedicalCategoryList.Styles("nStyle")
            Else
                cstyle = dgMedicalCategoryList.Styles.Add("nStyle")
            End If
        Catch ex As Exception
            cstyle = dgMedicalCategoryList.Styles.Add("nStyle")
        End Try
        'cstyle = dgMedicalCategoryList.Styles.Add("nStyle")
        Dim str As String = String.Empty
        Dim lensel As Int32 = 1
        If MedicalCatid <> 0 Then
            For Len As Integer = 1 To dgMedicalCategoryList.Rows.Count - 1
                If Not IsDBNull(dgMedicalCategoryList.Rows(Len)(6)) Then
                    cstyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dgMedicalCategoryList.Rows(Len)(6)))
                    dgMedicalCategoryList.SetData(Len, 5, "")
                    dgMedicalCategoryList.SetCellStyle(Len, 5, cstyle)
                    cstyle = Nothing
                    Dim myString As String = "nStyle" & Len.ToString()
                    Try

                        If (dgMedicalCategoryList.Styles.Contains(myString)) Then
                            cstyle = dgMedicalCategoryList.Styles(myString)
                        Else
                            cstyle = dgMedicalCategoryList.Styles.Add(myString)
                        End If
                    Catch ex As Exception
                        cstyle = dgMedicalCategoryList.Styles.Add(myString)
                    End Try
                    'cstyle = dgMedicalCategoryList.Styles.Add("nStyle" & Len.ToString())
                End If
                If (Convert.ToInt64(dgMedicalCategoryList.Rows(Len)(1)) = MedicalCatid) Then
                    lensel = Len
                End If
            Next
        Else
            For Len As Integer = 1 To dgMedicalCategoryList.Rows.Count - 1
                If Not IsDBNull(dgMedicalCategoryList.Rows(Len)(6)) Then
                    cstyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dgMedicalCategoryList.Rows(Len)(6)))
                    dgMedicalCategoryList.SetData(Len, 5, "")
                    dgMedicalCategoryList.SetCellStyle(Len, 5, cstyle)
                    cstyle = Nothing
                    ' cstyle = dgMedicalCategoryList.Styles.Add("nStyle" & Len.ToString())
                    Dim myString As String = "nStyle" & Len.ToString()
                    Try

                        If (dgMedicalCategoryList.Styles.Contains(myString)) Then
                            cstyle = dgMedicalCategoryList.Styles(myString)
                        Else
                            cstyle = dgMedicalCategoryList.Styles.Add(myString)
                        End If
                    Catch ex As Exception
                        cstyle = dgMedicalCategoryList.Styles.Add(myString)
                    End Try
                End If
            Next
        End If
        If lensel <> -1 And dgMedicalCategoryList.Rows.Count > lensel Then
            dgMedicalCategoryList.SelectionMode = SelectionModeEnum.Cell
            Dim cr As CellRange = dgMedicalCategoryList.GetCellRange(lensel, 2)
            dgMedicalCategoryList.[Select](cr, True)
        End If
    End Sub
    Private Function RetrieveMedicalCategoryData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)
            oDB.Retrive("gsp_GetMedicalCategory", dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FilterRecord(Optional ByVal MedicalCatid As Long = 0)
        Try
            Me.Cursor = Cursors.WaitCursor

            dvData = CType(dgMedicalCategoryList.DataSource, DataView)

            If IsNothing(dvData) Then
                Exit Sub
            Else
                dgMedicalCategoryList.DataSource = dvData
            End If

            Dim dt As DataTable = dvData.Table

            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dvData.RowFilter = " sMedicalCategory Like '%" & strPatientSearchDetails & "%' OR Convert(nImageRanking,'System.String')   Like '%" & strPatientSearchDetails & "%'  OR bIsActive  Like '%" & strPatientSearchDetails & "%'"
                strPatientSearchDetails = Nothing

                Dim _sStatus As String
                If dgMedicalCategoryList.RowSel > 0 Then
                    _sStatus = dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "bIsActive").ToString()
                    If _sStatus.ToUpper() = "ACTIVE" Then
                        tsbtn_Act_Deact_Rule.Text = "De-&activate"
                        tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                        tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
                    Else
                        tsbtn_Act_Deact_Rule.Text = "&Activate"
                        tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                        tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
                    End If
                End If

            End If

            If IsNothing(dt) = False Then
                dt.Dispose() : dt = Nothing
            End If
            If strSortColumnName <> String.Empty And strSortOrder <> String.Empty Then ''added for bugid 80004
                strSortColumnName = strSortColumnName.Replace("[", "")
                dvData.Sort = "[" & strSortColumnName & "] " & strSortOrder
            End If
            strSortColumnName = String.Empty
            SetColor(MedicalCatid)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Control Event"

    Private Sub txtSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearch.TextChanged
        FilterRecord()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
    End Sub

#End Region

#Region "Button Click Events"

    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try

            Select Case e.ClickedItem.Tag

                Case "Add"
                    AddMedicalCategory()

                Case "Modify"
                    ModifyMedicalCategory()

                Case "Delete"
                    DeleteMedicalCategory()

                Case "ACTIVATE"
                    ChangeMedicalCategoryStatus()

                Case "DEACTIVATE"
                    ChangeMedicalCategoryStatus()

                Case "Close"
                    FormClose()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddMedicalCategory()
        Try

            dvData = CType(dgMedicalCategoryList.DataSource, DataView)
            If (IsNothing(dvData) = False) Then


                strSortExprn = dvData.Sort

                Dim arrcolumnsort As String() = Split(strSortExprn, "]")
                If arrcolumnsort.Length > 1 Then
                    strSortColumnName = arrcolumnsort.GetValue(0)

                End If
            End If

            Dim oAddMedCategory As New frmMstMedicalCategory

            oAddMedCategory.ShowDialog(IIf(IsNothing(oAddMedCategory.Parent), Me, oAddMedCategory.Parent))
            oAddMedCategory.Dispose()
            oAddMedCategory = Nothing
            FillGridMedicalCategory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ModifyMedicalCategory()
        Try
            If dgMedicalCategoryList.RowSel > 0 Then
                dvData = CType(dgMedicalCategoryList.DataSource, DataView)
                If (IsNothing(dvData) = False) Then


                    strSortExprn = dvData.Sort

                    Dim arrcolumnsort As String() = Split(strSortExprn, "]")
                    If arrcolumnsort.Length > 1 Then
                        strSortColumnName = arrcolumnsort.GetValue(0)

                    End If
                End If
                Dim oAddMedCategory As New frmMstMedicalCategory

                Dim nMedicalCategoryID As Long = Convert.ToInt64(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nMedicalCategoryID"))
                Dim sMedicalCategory As String = Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "sMedicalCategory"))

                Dim bIsActive As Boolean

                If CheckIfOBMedicalCategory(nMedicalCategoryID) = True Then
                    oAddMedCategory.DisableDeactivate = True
                End If

                If Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "bIsActive")) = "Active" Then
                    bIsActive = 1
                Else
                    bIsActive = 0
                End If

                oAddMedCategory.nMedicalCategoryID = nMedicalCategoryID
                oAddMedCategory.sMedicalCategory = sMedicalCategory
                oAddMedCategory.bIsActive = bIsActive
                Dim _ImgRank As String = ""
                _ImgRank = Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nImageRanking"))
                If _ImgRank <> "" Then
                    oAddMedCategory.nRnkCategory = Convert.ToInt16(_ImgRank)
                End If
                'If Not IsDBNull(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nImageRanking")) Then
                '    oAddMedCategory.nRnkCategory = Convert.ToInt16(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nImageRanking"))
                'End If
                Dim _CatImgRank As String = ""
                _CatImgRank = Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, ("nCategoryImage")))
                If _CatImgRank <> "" Then
                    oAddMedCategory.CatColorId = Convert.ToInt16(_CatImgRank)
                End If
                ''End If
                oAddMedCategory.ShowDialog(IIf(IsNothing(oAddMedCategory.Parent), Me, oAddMedCategory.Parent))
                oAddMedCategory.Dispose()
                oAddMedCategory = Nothing
                FillGridMedicalCategory(nMedicalCategoryID)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DeleteMedicalCategory()
        If dgMedicalCategoryList.RowSel > 0 Then

            Dim nMedicalCategoryID As Long = Convert.ToInt64(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nMedicalCategoryID"))
            Dim nCount As Integer
            nCount = GetAssociatedMedCat_DML(nMedicalCategoryID)

            If nCount > 0 Then
                MessageBox.Show("Medical Category cannot be deleted as it is already associated with patient(s).", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Not MessageBox.Show("Are you sure you want to delete the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Exit Sub
            End If

            MedicalCategory_DML(nMedicalCategoryID, "", 0, "Delete")
            FillGridMedicalCategory()
        End If
    End Sub

    Private Function GetAssociatedMedCat_DML(nMedicalCategoryID As Long) As Integer
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim nCount As Object
        Try
            oDB = New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
            oParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)


            oParameters.Add("@nMedicalCategoryID", nMedicalCategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            nCount = oDB.ExecuteScalar("gsp_GetAssociatedMedCat_DML", oParameters)

            Return CInt(nCount)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect() : oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose() : oParameters = Nothing
            End If
        End Try
    End Function


    Private Sub ChangeMedicalCategoryStatus()

        Dim nMedicalCategoryID As Long
        Dim sMedicalCategory As String

        '27-May-15 Aniket: Resolving Bug #83641: EMR: Medical category- exception on search
        If dgMedicalCategoryList.RowSel > 0 Then


            nMedicalCategoryID = Convert.ToInt64(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nMedicalCategoryID"))
            sMedicalCategory = Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "sMedicalCategory"))

            Dim _sStatus As String

            If dgMedicalCategoryList.RowSel > 0 Then
                If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                    If Not MessageBox.Show("Are you sure you want to activate the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                If tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then
                    If CheckIfOBMedicalCategory(nMedicalCategoryID) = False Then
                        If Not MessageBox.Show("Are you sure you want to de-activate the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("This Category cannot be deactivated as it is set as an 'OB Medical Category' in gloEMR Admin." & vbCrLf & vbCrLf & "To deactivate it first remove it as an OB Medical Category from gloEMR Admin->Settings->OB Specialty.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End If


                Dim nImageRank As Int32 = -1
                If Not IsDBNull(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nImageRanking")) Then
                    nImageRank = dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nImageRanking")
                End If
                Dim nCategoryImage As Int32 = -1
                If Not IsDBNull(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nCategoryImage")) Then
                    nCategoryImage = dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nCategoryImage")
                End If

                Dim bIsActive As Boolean

                If Convert.ToString(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "bIsActive")) = "Active" Then
                    bIsActive = 0
                    dgMedicalCategoryList.SetData(dgMedicalCategoryList.RowSel, "bIsActive", "Inactive")
                Else
                    bIsActive = 1
                    dgMedicalCategoryList.SetData(dgMedicalCategoryList.RowSel, "bIsActive", "Active")
                End If


                MedicalCategory_DML(nMedicalCategoryID, sMedicalCategory, bIsActive, "Update", nCategoryImage, nImageRank)
                sMedicalCategory = Nothing




                If dgMedicalCategoryList.RowSel > 0 Then
                    _sStatus = dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "bIsActive").ToString()
                    If _sStatus.ToUpper() = "ACTIVE" Then
                        tsbtn_Act_Deact_Rule.Text = "De-&activate"
                        tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                        tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
                    Else
                        tsbtn_Act_Deact_Rule.Text = "&Activate"
                        tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                        tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
                    End If
                End If

            End If

        End If
    End Sub

    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CheckIfOBMedicalCategory(nMedicalCategoryID As Long) As Boolean

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim bytCount As Byte

        Try

            oDB = New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
            oParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)


            oParameters.Add("@nMedicalCategoryID", nMedicalCategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            bytCount = oDB.ExecuteScalar("gsp_CheckIfOBMedicalCategory", oParameters)

            If bytCount = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True

        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect() : oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oParameters) Then
                oParameters.Dispose() : oParameters = Nothing
            End If
        End Try

    End Function

    Private Sub MedicalCategory_DML(nMedicalCategoryID As Long, sMedicalCategory As String, bIsActive As Boolean, Flag As String, Optional ByVal CategoryImage As Int32 = -1, Optional ByVal ImageRanking As Int32 = -1)
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
            oParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)


            oParameters.Add("@nMedicalCategoryID", nMedicalCategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@sMedicalCategory", sMedicalCategory, ParameterDirection.Input, SqlDbType.Text)
            oParameters.Add("@bIsActive", bIsActive, ParameterDirection.Input, SqlDbType.Bit)
            oParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Text)
            If (CategoryImage <> -1) Then
                oParameters.Add("@nCategoryImage", CategoryImage, ParameterDirection.Input, SqlDbType.SmallInt)
            End If
            If (ImageRanking <> -1) Then
                oParameters.Add("@nImageRanking", ImageRanking, ParameterDirection.Input, SqlDbType.SmallInt)
            End If
            oDB.Execute("gsp_GetMedicalCategory_DML", oParameters)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect() : oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose() : oParameters = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Grid Events"

    Private Sub dgMedicalCategoryList_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles dgMedicalCategoryList.AfterSort
        Try
           
          
            If (e.Col = 5) Then
                Dim strord As String = ""
                If (e.Order = 1) Then
                    strord = "Asc"
                Else
                    strord = "Desc"
                End If
                dvData = CType(dgMedicalCategoryList.DataSource, DataView)

                If IsNothing(dvData) Then
                    Exit Sub
                Else
                    dgMedicalCategoryList.DataSource = dvData
                    dvData.Sort = "BackColorValue " & strord
                End If

            End If
            If (e.Order = 1) Then
                strSortOrder = "Asc"
            Else
                strSortOrder = "Desc"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetColor(nMedicalCat)
    End Sub

    Private Sub dgMedicalCategoryList_Click(sender As Object, e As System.EventArgs) Handles dgMedicalCategoryList.Click
        Try
            If dgMedicalCategoryList.Rows.Count > 1 Then
                ' Dim cm As CurrencyManager = DirectCast(BindingContext(Me.dgMedicalCategoryList.DataSource), CurrencyManager)
                'Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                ''     UnsentOrdersrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                nMedicalCat = Convert.ToInt64(dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "nMedicalCategoryID"))

                '  cm = Nothing
                ' dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgMedicalCategoryList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgMedicalCategoryList.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = dgMedicalCategoryList.HitTest(e.X, e.Y)

            If htInfo.Type = DataGrid.HitTestType.Cell Then
                ModifyMedicalCategory()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub dgMedicalCategoryList_RowColChange(sender As System.Object, e As System.EventArgs) Handles dgMedicalCategoryList.RowColChange
        Dim _sStatus As String
        If dgMedicalCategoryList.RowSel > 0 Then
            _sStatus = dgMedicalCategoryList.GetData(dgMedicalCategoryList.RowSel, "bIsActive").ToString()
            If _sStatus.ToUpper() = "ACTIVE" Then
                tsbtn_Act_Deact_Rule.Text = "De-&activate"
                tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
            Else
                tsbtn_Act_Deact_Rule.Text = "&Activate"
                tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
            End If
        End If
    End Sub

    Private Sub dgMedicalCategoryList_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgMedicalCategoryList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

#End Region

    
End Class