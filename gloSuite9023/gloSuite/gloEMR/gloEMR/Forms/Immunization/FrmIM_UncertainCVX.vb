'Added New Form by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX
Public Class FrmIM_UncertainCVX

#Region "Grid Colum"
    Private Const COL_Select As Int16 = 0
    Private Const COL_UCVXCode As Int16 = 1
    Private Const COL_UCVXTEST As Int16 = 2
    Private Const COL_PresentationDate As Int16 = 3
    Private Const COL_PublicationDate As Int16 = 4
    Private Const COL_ImmunizationScheduleUsed As Int16 = 5
    Private Const COL_IsValidDose As Int16 = 6
    Private Const COL_Reason As Int16 = 7
    Private Const COL_COUNT As Int16 = 8
#End Region

    Public Structure UncertainCVX
        Public bCheck As Boolean
        Public UCVXCode As String
        Public UCVXText As String
        Public UCVxPresentationDate As Nullable(Of Date)
        Public UCVxPublicationDate As Nullable(Of Date)
        Public UCVxImmunizationScheduleUsed As String
        Public UCVxIsDoseValid As String
        Public UCVxReason As String
    End Structure

    Public _lUnCertainCVX As List(Of UncertainCVX) = Nothing
    Public _UnCertainFormulationCVXSaved As Boolean = False
    Public _isHistory As Int16 = 0

    Private Sub FrmIM_UncertainCVX_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim item As FrmIM_UncertainCVX.UncertainCVX = Nothing
        Try
            DesignGrid()
            For i As Integer = 0 To _lUnCertainCVX.Count - 1
                c1UncertainCVX.Rows.Add()
                item = _lUnCertainCVX(i)
                c1UncertainCVX.Rows(i + 1)(COL_Select) = item.bCheck
                c1UncertainCVX.Rows(i + 1)(COL_UCVXCode) = item.UCVXCode
                c1UncertainCVX.Rows(i + 1)(COL_UCVXTEST) = item.UCVXText
                If IsDate(item.UCVxPresentationDate) Then
                    c1UncertainCVX.Rows(i + 1)(COL_PresentationDate) = item.UCVxPresentationDate
                End If
                If IsDate(item.UCVxPublicationDate) Then
                    c1UncertainCVX.Rows(i + 1)(COL_PublicationDate) = item.UCVxPublicationDate
                End If
                c1UncertainCVX.Rows(i + 1)(COL_ImmunizationScheduleUsed) = item.UCVxImmunizationScheduleUsed
                c1UncertainCVX.Rows(i + 1)(COL_IsValidDose) = item.UCVxIsDoseValid
                c1UncertainCVX.Rows(i + 1)(COL_Reason) = item.UCVxReason
            Next
            ' In case of History
            If _isHistory = 1 Then
                tblbtn_Save.Enabled = False
            ElseIf _isHistory = 0 Then
                c1UncertainCVX.Cols(COL_ImmunizationScheduleUsed).Visible = False
                c1UncertainCVX.Cols(COL_IsValidDose).Visible = False
                c1UncertainCVX.Cols(COL_Reason).Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.DesignGrid() " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignGrid()
        Try

            c1UncertainCVX.DataSource = Nothing
            c1UncertainCVX.Clear()
            c1UncertainCVX.Cols.Count = COL_COUNT
            c1UncertainCVX.Rows.Count = 1
            c1UncertainCVX.Rows.Fixed = 1

            c1UncertainCVX.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            c1UncertainCVX.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, FontStyle.Regular)
            c1UncertainCVX.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            c1UncertainCVX.BackColor = Color.White
            c1UncertainCVX.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            c1UncertainCVX.ScrollBars = ScrollBars.Both
            SetGridStyle(c1UncertainCVX)
            ' set visibility of column
            c1UncertainCVX.Cols(COL_Select).Visible = True
            c1UncertainCVX.Cols(COL_UCVXCode).Visible = True
            c1UncertainCVX.Cols(COL_UCVXTEST).Visible = True
            c1UncertainCVX.Cols(COL_PresentationDate).Visible = True
            c1UncertainCVX.Cols(COL_PublicationDate).Visible = True
            c1UncertainCVX.Cols(COL_ImmunizationScheduleUsed).Visible = True
            c1UncertainCVX.Cols(COL_IsValidDose).Visible = True
            c1UncertainCVX.Cols(COL_Reason).Visible = True
            ' set column type
            c1UncertainCVX.Cols(COL_Select).DataType = GetType(Boolean)
            c1UncertainCVX.Cols(COL_Select).DataType = GetType(Boolean)

            c1UncertainCVX.Cols(COL_PresentationDate).DataType = GetType(Date)
            c1UncertainCVX.Cols(COL_PublicationDate).DataType = GetType(Date)
            c1UncertainCVX.AllowEditing = True
            ' set column editing
            c1UncertainCVX.Cols(COL_Select).AllowEditing = True
            c1UncertainCVX.Cols(COL_UCVXCode).AllowEditing = False
            c1UncertainCVX.Cols(COL_UCVXTEST).AllowEditing = True
            c1UncertainCVX.Cols(COL_PresentationDate).AllowEditing = True
            c1UncertainCVX.Cols(COL_PublicationDate).AllowEditing = True
            c1UncertainCVX.Cols(COL_ImmunizationScheduleUsed).AllowEditing = False
            c1UncertainCVX.Cols(COL_IsValidDose).AllowEditing = False
            c1UncertainCVX.Cols(COL_Reason).AllowEditing = False
            'set Heading
            c1UncertainCVX.SetData(0, COL_Select, "Select")
            c1UncertainCVX.SetData(0, COL_UCVXCode, "CVX Code")
            If _isHistory = 0 Then
                c1UncertainCVX.SetData(0, COL_UCVXTEST, "CVX Description")
            ElseIf _isHistory = 1 Then
                c1UncertainCVX.SetData(0, COL_UCVXTEST, "Vaccine Group")
            End If
            c1UncertainCVX.SetData(0, COL_PresentationDate, "Presented Date")
            c1UncertainCVX.SetData(0, COL_PublicationDate, "Publication Date")
            c1UncertainCVX.SetData(0, COL_ImmunizationScheduleUsed, "Immunization Schedule Used")
            c1UncertainCVX.SetData(0, COL_IsValidDose, "Valid Dose")
            c1UncertainCVX.SetData(0, COL_Reason, "Validity Reason")
            ' set width
            c1UncertainCVX.Cols(COL_Select).Width = 50
            c1UncertainCVX.Cols(COL_UCVXCode).Width = 80
            c1UncertainCVX.Cols(COL_UCVXTEST).Width = 230
            c1UncertainCVX.Cols(COL_PresentationDate).Width = 100
            c1UncertainCVX.Cols(COL_PublicationDate).Width = 100
            c1UncertainCVX.Cols(COL_ImmunizationScheduleUsed).Width = 170
            c1UncertainCVX.Cols(COL_IsValidDose).Width = 80
            c1UncertainCVX.Cols(COL_Reason).Width = 200
            c1UncertainCVX.ExtendLastCol = True
            'Align ment
            c1UncertainCVX.Cols(COL_Select).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            c1UncertainCVX.Cols(COL_UCVXCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_UCVXTEST).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_PresentationDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_PublicationDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_ImmunizationScheduleUsed).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_IsValidDose).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom
            c1UncertainCVX.Cols(COL_Reason).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftBottom

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.DesignGrid() " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub SetGridStyle(oFlex As C1.Win.C1FlexGrid.C1FlexGrid)
        c1UncertainCVX.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        oFlex.Rows.Count = 1
        oFlex.Rows.Fixed = 1

        oFlex.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        oFlex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        oFlex.BackColor = System.Drawing.Color.FromArgb(240, 247, 255)


        oFlex.Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211)
        oFlex.Styles.Fixed.ForeColor = Color.White
        oFlex.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Bold)

        oFlex.Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250)
        ' Color.LightBlue;
        oFlex.Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
        oFlex.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

        oFlex.Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
        oFlex.Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
        oFlex.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

        oFlex.Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
        oFlex.Styles.Highlight.ForeColor = Color.Black

        oFlex.Styles.Focus.BackColor = Color.FromArgb(254, 207, 102)
        oFlex.Styles.Focus.ForeColor = Color.Black


        Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_Header")
        Try
            If (oFlex.Styles.Contains("CS_Header")) Then
                csHeader = oFlex.Styles("CS_Header")
            Else
                csHeader = oFlex.Styles.Add("CS_Header")
                csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold)
                csHeader.ForeColor = Color.Black
                csHeader.BackColor = Color.FromArgb(192, 203, 233)
                'csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csHeader.DataType = Type.[GetType]("System.String")
            End If
        Catch ex As Exception
            csHeader = oFlex.Styles.Add("CS_Header")
            csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Bold)
            csHeader.ForeColor = Color.Black
            csHeader.BackColor = Color.FromArgb(192, 203, 233)
            'csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            csHeader.DataType = Type.[GetType]("System.String")
        End Try




        Dim csRecord As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_Record")
        Try
            If (oFlex.Styles.Contains("CS_Record")) Then
                csRecord = oFlex.Styles("CS_Record")
            Else
                csRecord = oFlex.Styles.Add("CS_Record")
                csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) ' IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular)
                csRecord.ForeColor = Color.Black
                csRecord.BackColor = Color.GhostWhite
                csRecord.DataType = Type.[GetType]("System.String")
            End If
        Catch ex As Exception
            csRecord = oFlex.Styles.Add("CS_Record")
            csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
            csRecord.ForeColor = Color.Black
            csRecord.BackColor = Color.GhostWhite
            csRecord.DataType = Type.[GetType]("System.String")
        End Try

        'csRecord.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack



        Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_ComboList")
        Try
            If (oFlex.Styles.Contains("CS_ComboList")) Then
                csComboList = oFlex.Styles("CS_ComboList")
            Else
                csComboList = oFlex.Styles.Add("CS_ComboList")
                csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csComboList.ForeColor = Color.Black
                csComboList.BackColor = Color.GhostWhite
                'csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csComboList.DataType = Type.[GetType]("System.String")
                csComboList.ComboList = "..."
            End If
        Catch ex As Exception
            csComboList = oFlex.Styles.Add("CS_ComboList")
            csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
            csComboList.ForeColor = Color.Black
            csComboList.BackColor = Color.GhostWhite
            'csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            csComboList.DataType = Type.[GetType]("System.String")
            csComboList.ComboList = "..."
        End Try



        Dim csCheckBox As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_CheckBox")
        Try
            If (oFlex.Styles.Contains("CS_CheckBox")) Then
                csCheckBox = oFlex.Styles("CS_CheckBox")
            Else
                csCheckBox = oFlex.Styles.Add("CS_CheckBox")
                csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) ' IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csCheckBox.ForeColor = Color.Black
                csCheckBox.BackColor = Color.GhostWhite
                'csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csCheckBox.DataType = Type.[GetType]("System.Boolean")
            End If
        Catch ex As Exception
            csCheckBox = oFlex.Styles.Add("CS_CheckBox")
            csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
            csCheckBox.ForeColor = Color.Black
            csCheckBox.BackColor = Color.GhostWhite
            'csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            csCheckBox.DataType = Type.[GetType]("System.Boolean")
        End Try



        Dim csNotNormal As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_NotNormal")
        Try
            If (oFlex.Styles.Contains("CS_NotNormal")) Then
                csNotNormal = oFlex.Styles("CS_NotNormal")
            Else
                csNotNormal = oFlex.Styles.Add("CS_NotNormal")
                csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csNotNormal.ForeColor = Color.Red
                csNotNormal.BackColor = Color.GhostWhite
            End If
        Catch ex As Exception
            csNotNormal = oFlex.Styles.Add("CS_NotNormal")
            csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
            csNotNormal.ForeColor = Color.Red
            csNotNormal.BackColor = Color.GhostWhite
        End Try
     


    End Sub

    Private Sub tblbtn_Save_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Save.Click
        Dim item As FrmIM_UncertainCVX.UncertainCVX = Nothing
        Try
            If _lUnCertainCVX Is Nothing Then
                _lUnCertainCVX = New List(Of UncertainCVX)
            End If
            c1UncertainCVX.FinishEditing()
            _lUnCertainCVX.Clear()
            For i As Integer = 1 To c1UncertainCVX.Rows.Count() - 1
                item = New FrmIM_UncertainCVX.UncertainCVX()
                item.bCheck = c1UncertainCVX.Rows(i)(COL_Select)
                item.UCVXCode = c1UncertainCVX.Rows(i)(COL_UCVXCode)
                item.UCVXText = c1UncertainCVX.Rows(i)(COL_UCVXTEST)
                If IsDate(c1UncertainCVX.Rows(i)(COL_PresentationDate)) Then
                    item.UCVxPresentationDate = c1UncertainCVX.Rows(i)(COL_PresentationDate)
                End If
                If IsDate(c1UncertainCVX.Rows(i)(COL_PublicationDate)) Then
                    item.UCVxPublicationDate = c1UncertainCVX.Rows(i)(COL_PublicationDate)
                End If
                _lUnCertainCVX.Add(item)
            Next
            _UnCertainFormulationCVXSaved = True
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryTests.DesignGrid() " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub

    'Private Sub c1UncertainCVX_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1UncertainCVX.AfterEdit
    '    Try
    '        If e.Col = COL_Select Then
    '            If Not c1UncertainCVX.GetData(e.Row, COL_Select) Then
    '                c1UncertainCVX.Rows(e.Row)(COL_PresentationDate) = Nothing
    '                c1UncertainCVX.Rows(e.Row)(COL_PublicationDate) = Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ex = Nothing
    '    End Try
    'End Sub
End Class