Public Class frmSrchQrdaPatient

    Public dtpatID As DataTable
    Public _databaseConnectionString As String
    Dim _dv As DataView
    Dim dtpatdata As DataTable
    Dim ColCount As Integer = 8
    Dim Col_Selected As Integer = 0
    Dim Col_PatID As Integer = 1
    Dim Col_PatCode As Integer = 2
    Dim Col_Firstname As Integer = 3
    Dim Col_Middlename As Integer = 4
    Dim Col_Lastname As Integer = 5
    Dim Col_Gender As Integer = 6
    Dim Col_DOB As Integer = 7
    Public _Saveflag = False
    Private Sub frmSrchQrdaPatient_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        clsCQMMeasure._databaseConnectionString = _databaseConnectionString
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oParameters.Add("@tvppat", dtpatID, ParameterDirection.Input, SqlDbType.Structured)

        dtpatdata = clsCQMMeasure.GetdataWithParam(oParameters, "gsp_getPatientbyTVP")
        _dv = dtpatdata.DefaultView
        gloC1FlexStyle.Style(c1PatDtl, True)
        c1PatDtl.Rows.Count = 1
        'c1PatDtl.Cols.Count = 8
        SetGridStyle()
        dtpatdata.TableName = "PatientDTL"
        c1PatDtl.DataSource = dtpatdata
        RemoveHandler chkselect.CheckedChanged, AddressOf chkselect_CheckedChanged
        chkselect.Checked = True
        AddHandler chkselect.CheckedChanged, AddressOf chkselect_CheckedChanged
    End Sub
    Private Sub SetGridStyle()
        'With c1PatDtl
        Try

     

        c1PatDtl.Cols.Count = ColCount
        '.Redraw = False
        Dim _TotalWidth As Single = Me.Width - 5
        c1PatDtl.Cols(Col_Selected).Width = _TotalWidth * 0.12
        c1PatDtl.Cols(Col_Selected).AllowEditing = True
        c1PatDtl.Cols(Col_Selected).Visible = True
        c1PatDtl.Cols(Col_Selected).DataType = GetType(Boolean)
        c1PatDtl.SetData(0, Col_Selected, "Selected")
        '  c1PatDtl.Cols(Col_Selected).TextAlign = TextAlignEnum.LeftCenter
        c1PatDtl.Cols(Col_PatID).Width = 0
        c1PatDtl.Cols(Col_PatID).AllowEditing = False
        c1PatDtl.Cols(Col_PatID).Visible = False
        c1PatDtl.SetData(0, Col_PatID, "PatientID")

            c1PatDtl.Cols(Col_PatCode).Width = _TotalWidth * 0.125
        c1PatDtl.Cols(Col_PatCode).AllowEditing = False
        c1PatDtl.Cols(Col_PatCode).Visible = True
        c1PatDtl.SetData(0, Col_PatCode, "Patient Code")

       
            c1PatDtl.Cols(Col_Firstname).Width = _TotalWidth * 0.125
        c1PatDtl.Cols(Col_Firstname).AllowEditing = False
        c1PatDtl.Cols(Col_Firstname).Visible = True
        c1PatDtl.SetData(0, Col_Firstname, "First Name")


            c1PatDtl.Cols(Col_Middlename).Width = _TotalWidth * 0.125
        c1PatDtl.Cols(Col_Middlename).AllowEditing = False
        c1PatDtl.Cols(Col_Middlename).Visible = True
        c1PatDtl.SetData(0, Col_Middlename, "Middle Name")

        c1PatDtl.Cols(Col_Lastname).Width = _TotalWidth * 0.12
        c1PatDtl.Cols(Col_Lastname).AllowEditing = False
        c1PatDtl.Cols(Col_Lastname).Visible = True
        c1PatDtl.SetData(0, Col_Lastname, "Last Name")


        c1PatDtl.Cols(Col_Gender).Width = _TotalWidth * 0.12
        c1PatDtl.Cols(Col_Gender).AllowEditing = False
        c1PatDtl.Cols(Col_Gender).Visible = True
        c1PatDtl.SetData(0, Col_Gender, "Gender")


        c1PatDtl.Cols(Col_DOB).Width = _TotalWidth * 0.12
        c1PatDtl.Cols(Col_DOB).AllowEditing = False
        c1PatDtl.Cols(Col_DOB).Visible = True
        c1PatDtl.SetData(0, Col_DOB, "DOB")


        With c1PatDtl
            .Redraw = False
            For Each dr As DataRow In dtpatdata.Rows
                .Rows.Add()
                .SetData(.Rows.Count - 1, Col_Selected, dr("Selected"))
                .SetData(.Rows.Count - 1, Col_PatID, dr("PatientID"))
                .SetData(.Rows.Count - 1, Col_PatCode, dr("PatientCode"))
                .SetData(.Rows.Count - 1, Col_Firstname, dr("PatientFirstName"))
                .SetData(.Rows.Count - 1, Col_Middlename, dr("PatientMiddleName"))
                .SetData(.Rows.Count - 1, Col_Lastname, dr("PatientLastName"))
                .SetData(.Rows.Count - 1, Col_Gender, dr("Gender"))
                .SetData(.Rows.Count - 1, Col_DOB, dr("PatientDOB"))
            Next

            End With
        Catch ex As Exception
        Finally
            c1PatDtl.Redraw = True
        End Try
    End Sub

    Private Sub c1PatDtl_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub c1PatDtl_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1PatDtl.AfterEdit
        Try

        
        If (e.Col = 0) AndAlso e.Row > 0 Then
            Dim patid As Long = c1PatDtl.GetData(e.Row, Col_PatID)
            Dim flag As Boolean = Convert.ToBoolean(c1PatDtl.GetData(e.Row, Col_Selected))
            UpdateDT(patid, flag)
                If (flag = False) Then
                    RemoveHandler chkselect.CheckedChanged, AddressOf chkselect_CheckedChanged
                    chkselect.Checked = False
                    AddHandler chkselect.CheckedChanged, AddressOf chkselect_CheckedChanged
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateDT(ByVal PatientID As Long, ByVal flag As Boolean)
        If Not IsNothing(dtpatdata) Then
            Dim dr As DataRow() = dtpatdata.Select("PatientID=" & PatientID.ToString() & "")
            If (dr.Length > 0) Then
                dr(0)("Selected") = flag
            End If

            dtpatdata.AcceptChanges()
        End If

    End Sub

    Private Sub updateAlldt(ByVal flag As Boolean)
        For Each dr As DataRow In dtpatdata.Rows
            dr("Selected") = flag
        Next
        dtpatdata.AcceptChanges()
    End Sub

    Private Sub chkselect_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkselect.CheckedChanged
        c1PatDtl.Redraw = False
        Try
            If (_dv.RowFilter = "") Then

                If (chkselect.Checked = True) Then
                    For Len As Integer = 1 To c1PatDtl.Rows.Count - 1

                        c1PatDtl.SetData(Len, Col_Selected, True)
                    Next

                Else
                    For Len As Integer = 1 To c1PatDtl.Rows.Count - 1

                        c1PatDtl.SetData(Len, Col_Selected, False)
                    Next
                End If
                updateAlldt(chkselect.Checked)
            Else
                If (chkselect.Checked = True) Then
                    For Len As Integer = 1 To c1PatDtl.Rows.Count - 1

                        c1PatDtl.SetData(Len, Col_Selected, True)
                        UpdateDT(c1PatDtl.GetData(Len, Col_PatID), True)
                    Next

                Else
                    For Len As Integer = 1 To c1PatDtl.Rows.Count - 1

                        c1PatDtl.SetData(Len, Col_Selected, False)
                        UpdateDT(c1PatDtl.GetData(Len, Col_PatID), False)
                    Next
                End If
            End If
 Catch ex As Exception
        Finally

            c1PatDtl.Redraw = True
        End Try

    End Sub
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            strSpecialChar = Replace(strSpecialChar, "'", "['']") & ""
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        If (txtSearch.Text.Trim() = "") Then
            _dv.RowFilter = ""
            SetData(dtpatdata)
        Else
            _dv = dtpatdata.DefaultView
            Dim strfilterdata As String = ReplaceSpecialCharacters(txtSearch.Text.Trim())
            Dim strfilter As String = "PatientCode like '%" & strfilterdata & "%' OR "
            strfilter = strfilter & "PatientFirstName like '%" & strfilterdata & "%' OR "
            strfilter = strfilter & "PatientMiddleName like '%" & strfilterdata & "%' OR "
            strfilter = strfilter & "PatientLastName like '%" & strfilterdata & "%' OR "
            strfilter = strfilter & "Gender like '%" & strfilterdata & "%'"
            _dv.RowFilter = strfilter
            SetData(_dv.ToTable())
        End If
       
    End Sub
    Private Sub SetData(ByVal dtdata As DataTable)
        Try

        
        c1PatDtl.Rows.Count = 1

        With c1PatDtl
            .Redraw = False
            For Each dr As DataRow In dtdata.Rows
                .Rows.Add()
                .SetData(.Rows.Count - 1, Col_Selected, dr("Selected"))
                .SetData(.Rows.Count - 1, Col_PatID, dr("PatientID"))
                .SetData(.Rows.Count - 1, Col_PatCode, dr("PatientCode"))
                .SetData(.Rows.Count - 1, Col_Firstname, dr("PatientFirstName"))
                .SetData(.Rows.Count - 1, Col_Middlename, dr("PatientMiddleName"))
                .SetData(.Rows.Count - 1, Col_Lastname, dr("PatientLastName"))
                .SetData(.Rows.Count - 1, Col_Gender, dr("Gender"))
                .SetData(.Rows.Count - 1, Col_DOB, dr("PatientDOB"))
            Next

        End With
        Catch ex As Exception
        Finally
            c1PatDtl.Redraw = True
        End Try
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnSave_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub tblSave_Click(sender As System.Object, e As System.EventArgs) Handles tblSave.Click
        _Saveflag = True
        Try
            dtpatID.Rows.Clear()
            Dim drr As DataRow() = dtpatdata.Select("Selected=1")
            For Each dr As DataRow In drr
                dtpatID.Rows.Add(dr("PatientID"))
            Next
        Catch ex As Exception

        End Try
        Me.Close()
    End Sub
End Class