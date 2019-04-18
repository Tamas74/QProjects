Imports gloGlobal.clsMISC


Public Class frmMstMedicalCategory

#Region "Variable declaration"

    Dim _nMedicalCategoryID As Long = 0
    Dim _sMedicalCategory As String = ""
    Dim _bIsActive As Boolean

    Dim dtColor As DataTable = Nothing
    Dim _rnkCategory As Int16 = -1
    Dim _CatColorId As Int16 = -1
    Dim htCatImages As Hashtable
    Private blnDisableDeactivate As Boolean


#End Region

#Region "Property declaration"

    Public Property DisableDeactivate As Boolean
        Get
            Return _CatColorId
        End Get
        Set(value As Boolean)
            blnDisableDeactivate = value
        End Set
    End Property

    Public Property CatColorId As Int16
        Get
            Return _CatColorId
        End Get
        Set(value As Int16)
            _CatColorId = value
        End Set
    End Property

    Public Property nMedicalCategoryID As Long
        Get
            Return _nMedicalCategoryID
        End Get
        Set(value As Long)
            _nMedicalCategoryID = value
        End Set
    End Property

    Public Property nRnkCategory As Int16
        Get
            Return _rnkCategory
        End Get
        Set(value As Int16)
            _rnkCategory = value
        End Set
    End Property
    Public Property sMedicalCategory As String
        Get
            Return _sMedicalCategory
        End Get
        Set(value As String)
            _sMedicalCategory = value
        End Set
    End Property

    Public Property bIsActive As Long
        Get
            Return _bIsActive
        End Get
        Set(value As Long)
            _bIsActive = value
        End Set
    End Property

#End Region

#Region "Property declaration"

    Private Sub frmMstMedicalCategory_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Not IsNothing(dtColor) Then
            dtColor.Dispose()
            dtColor = Nothing

            cmbcolor.DataSource = Nothing
            cmbcolor.Items.Clear()
        End If

        If Not IsNothing(htCatImages) Then
            htCatImages.Clear()
            htCatImages = Nothing
        End If

    End Sub

    Private Sub frmMstMedicalCategory_Load(sender As Object, e As System.EventArgs) Handles Me.Load



        If nMedicalCategoryID > 0 Then
            txtMedicalCategory.SelectionStart = 10
            txtMedicalCategory.Text = sMedicalCategory
            chkIsActive.Checked = bIsActive

            If blnDisableDeactivate = True Then
                chkIsActive.Enabled = False
                lblDeactivateMedicalCategory.Visible = True
            End If

        End If
        FillMedicalCategoryColor()
        FillHashCatColor()
        If (CatColorId <> -1) Then
            cmbcolor.SelectedValue = CatColorId
        End If
        If (nRnkCategory <> -1) Then
            txtrank.Text = nRnkCategory.ToString()
        End If
        If (CatColorId = -1) Then
            cmbcolor.Text = "Orange"
        End If
        SetBannerColor()

    End Sub

#End Region
    Private Sub FillHashCatColor()
        htCatImages = New Hashtable()
        ''    HashtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown")
        'If (strcolor.Trim() <> "") Then
        '    For Each di As DictionaryEntry In HashtblMedcatClr
        '        If (di.Key.ToString().Contains(strcolor)) Then
        '            Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("gloUserControlLibrary.Resources", GetType(gloUC_PatientStrip).Assembly)
        '            pnlButton.BackgroundImage = CType(temp.GetObject(Convert.ToString(di.Key), resourceCulture), Image)
        '            pnlPatientDetail.BackgroundImage = CType(temp.GetObject(Convert.ToString(di.Value), resourceCulture), Image)
        '            lbl_BorderBOTTOM.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
        '            lbl_BorderRIGHT.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
        '            lbl_BorderLEFT.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
        '            lbl_BorderTOP.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
        '            label27.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
        '            pnlGlobalPeriod.BackColor = System.Drawing.ColorTranslator.FromHtml(strbottompanelcolr)
        '            temp = Nothing
        '            Exit For
        '        End If
        '    Next

        htCatImages.Add("PatBannerBlue", "Blue")
        htCatImages.Add("PatBannerBrown", "Brown")
        htCatImages.Add("PatBannerGray", "Gray")
        htCatImages.Add("PatBannerGreen", "Green")

        htCatImages.Add("PatBannerOrange", "Orange")
        htCatImages.Add("PatBannerPink", "Pink")
        htCatImages.Add("PatBannerRed", "Red")
        htCatImages.Add("PatBannerViolet", "Violet")
        htCatImages.Add("PatBannerYellow", "Yellow")
        htCatImages.Add("PatBannerDark_Blue", "Dark Blue")

    End Sub
    Private resourceCulture As Global.System.Globalization.CultureInfo
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Friend Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set(value As Global.System.Globalization.CultureInfo)
            resourceCulture = value
        End Set
    End Property
    Private Shared resEMR As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("gloEMR.Resources", GetType(frmMstMedicalCategory).Assembly)
    Private Sub SetBannerColor()
        Dim strcolor As String = cmbcolor.Text.ToString().Trim()

        If (strcolor.Trim() <> "") Then
            For Each di As DictionaryEntry In htCatImages
                If (di.Key.ToString().Contains(strcolor.Replace(" ", "_"))) Then

                    pnlPatBanner.BackgroundImage = CType(resEMR.GetObject(Convert.ToString(di.Key), resourceCulture), Image)

                    'temp = Nothing
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub FillMedicalCategoryColor()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing

        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters

            oDB.Retrive("gsp_GetMedicalCategoryColors", oParam, dtColor)
            oDB.Disconnect()

            cmbcolor.DataSource = dtColor
            cmbcolor.DisplayMember = "ImageColor"
            cmbcolor.ValueMember = "nCategoryImage"


        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            'If Not IsNothing(dtColor) Then
            '    dtColor.Dispose() : dtColor = Nothing
            'End If
        End Try
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveMedicalCategory()
                Case "Close"
                    FormClose()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveMedicalCategory()

        If Trim(txtMedicalCategory.Text) = "" Then
            MessageBox.Show("Please enter Medical Category.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtMedicalCategory.Focus()
            Exit Sub
        End If
        If (txtrank.Text.Trim() <> "") Then
            If Not IsNumeric(txtrank.Text) Then
                MessageBox.Show("Medical Category Rank can be numeric only.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtrank.Text = ""
                txtrank.Focus()
                Exit Sub

            End If
        End If
        Dim dt As DataTable

        If nMedicalCategoryID > 0 Then
            dt = CheckDuplicateMedicalCategory(nMedicalCategoryID, txtMedicalCategory.Text)
        Else
            dt = CheckDuplicateMedicalCategory(0, txtMedicalCategory.Text)
        End If

        If dt.Rows.Count > 0 Then
            MessageBox.Show("Medical Category already exist.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtMedicalCategory.Focus()
            Exit Sub
        End If

        If nMedicalCategoryID > 0 Then

            MedicalCategory_DML(nMedicalCategoryID, txtMedicalCategory.Text, chkIsActive.Checked, "Update")
        Else
            MedicalCategory_DML(0, txtMedicalCategory.Text, chkIsActive.Checked, "Insert")
        End If
        Me.Close()
    End Sub


    Private Function CheckDuplicateMedicalCategory(nMedicalCategoryID As Long, MedicalCategory As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nMedicalCategoryID", nMedicalCategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@sMedicalCategory", MedicalCategory, ParameterDirection.Input, SqlDbType.Text)

            oDB.Retrive("gsp_GetMedicalCategory_DML_Duplicate", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub MedicalCategory_DML(nMedicalCategoryID As Long, sMedicalCategory As String, bIsActive As Boolean, Flag As String)
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
            oParameters.Add("@nCategoryImage", cmbcolor.SelectedValue, ParameterDirection.Input, SqlDbType.SmallInt)
            If (txtrank.Text.Trim() <> "") Then
                oParameters.Add("@nImageRanking", Convert.ToInt16(txtrank.Text), ParameterDirection.Input, SqlDbType.SmallInt)
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

    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtrank_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtrank.KeyPress

        If e.KeyChar < Chr(48) Or e.KeyChar > Chr(57) Or e.KeyChar = "." Then
            If (Asc(e.KeyChar) <> 8) Then
                e.KeyChar = Nothing
            End If
        End If

    End Sub

    Private Sub cmbcolor_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbcolor.SelectedIndexChanged
        Me.Opacity = 1
    End Sub

    Private Sub cmbcolor_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles cmbcolor.SelectionChangeCommitted
        SetBannerColor()
        Me.Opacity = 0.5
    End Sub


    '13-Oct-15 Aniket: Resolving: Bug #90243: glOEMR: Patient List report- application gives an error on generate report
    Private Sub txtMedicalCategory_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMedicalCategory.KeyPress

        If gloGlobal.clsMISC.IsSpecialChar(e.KeyChar) = True Then
            e.Handled = True
        End If

    End Sub

End Class