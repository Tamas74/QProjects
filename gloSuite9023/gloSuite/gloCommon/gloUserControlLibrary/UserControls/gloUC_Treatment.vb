Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary
Imports gloSnoMed

#Region " Public ENUMs "
Public Enum enumIC9Count
    Show_8_ICD9
    Show_4_ICD9
End Enum

Public Enum enumModifierCount
    Show_4_Modifier
    Show_2_MOdifier
End Enum

#End Region

Public Class gloUC_Treatment

    Public Event ICD9_Inserted(ByVal oICD9 As Object)
    Public Event ICD9_Removed(ByVal oICD9s As Object)
    Public Event GridListLoaded()
    Public Event GridListClosed()
    Public Event MouseDoubleClick()
    Public Shadows Event MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)


#Region " Private Variables "
    Private _DatabaseConnectionString As String
    Private Const _MessageBoxCaption = "gloEMR"
    Private _ICD9count As enumIC9Count
    Private _ModifierCount As enumModifierCount
    Private _DOS As DateTime
    Private _DisableGrid As Boolean = False
    Private _SelectedDiagnosis As String
    Private _RowSel As Integer
    Private _ColSel As Integer
    Private _TreatmentModified As Boolean = False
    Private _ICDRevision As Integer

    Private _ItemInserted As Boolean = False
    Private _LastCode As String = ""
    Private _LastDesc As String = ""
    Private _TreatmentFilling As Boolean = False

    Private oToolTip As New ToolTip
#End Region

#Region " Public Properties "

    Public Property IsExamPTBillingEnabled() As Boolean

    Public Property DatabaseConnectionString() As String
        Get
            Return _DatabaseConnectionString
        End Get
        Set(ByVal value As String)
            _DatabaseConnectionString = value
        End Set
    End Property

    Public Property ICD9Count() As enumIC9Count
        Get
            Return _ICD9count
        End Get
        Set(ByVal value As enumIC9Count)
            _ICD9count = value
        End Set
    End Property

    Public Property ModifierCount() As enumModifierCount
        Get
            Return _ModifierCount
        End Get
        Set(ByVal value As enumModifierCount)
            _ModifierCount = value
        End Set
    End Property

    Public Property DOS() As DateTime
        Get
            Return _DOS
        End Get
        Set(ByVal value As DateTime)
            _DOS = value
        End Set
    End Property

    Public Property DisableGrid() As Boolean
        Get
            Return _DisableGrid
        End Get
        Set(ByVal value As Boolean)
            _DisableGrid = value
            C1Treatment.AllowEditing = value
        End Set
    End Property

    Public ReadOnly Property SelectedDiagnosis() As String
        Get
            Try
                If C1Treatment.RowSel > 0 And C1Treatment.ColSel >= COL_DX1_CODE And C1Treatment.ColSel <= COL_DX8_CODE Then
                    Return C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel) & " " & C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel + 1)
                Else
                    Return ""
                End If
            Catch
                Return ""
            End Try
        End Get
    End Property

    Public Property RowSel() As Integer
        Get
            Return C1Treatment.RowSel
        End Get
        Set(ByVal value As Integer)
            C1Treatment.RowSel = _RowSel
        End Set
    End Property

    Public Property ColSel() As Integer
        Get
            Return C1Treatment.ColSel
        End Get
        Set(ByVal value As Integer)
            C1Treatment.ColSel = _ColSel
        End Set
    End Property

    Public Property TreatmentModified() As Boolean
        Get
            Return _TreatmentModified
        End Get
        Set(ByVal value As Boolean)
            _TreatmentModified = value
        End Set
    End Property


    Public Property ICDRevision() As Integer
        Get
            Return _ICDRevision
        End Get
        Set(ByVal value As Integer)
            _ICDRevision = value
        End Set
    End Property

#End Region

#Region " C1 Constants "
    Private Const COL_LINE_NO = 0
    Private Const COL_DOS = 1
    Private Const COL_CPT_ID = 2
    Private Const COL_CPT_CODE = 3
    Private Const COL_CPT_DESC = 4
    Private Const COL_DX1_ID = 5
    Private Const COL_DX1_CODE = 6
    Private Const COL_DX1_DESC = 7
    Private Const COL_DX2_ID = 8
    Private Const COL_DX2_CODE = 9
    Private Const COL_DX2_DESC = 10
    Private Const COL_DX3_ID = 11
    Private Const COL_DX3_CODE = 12
    Private Const COL_DX3_DESC = 13
    Private Const COL_DX4_ID = 14
    Private Const COL_DX4_CODE = 15
    Private Const COL_DX4_DESC = 16
    Private Const COL_DX5_ID = 17
    Private Const COL_DX5_CODE = 18
    Private Const COL_DX5_DESC = 19
    Private Const COL_DX6_ID = 20
    Private Const COL_DX6_CODE = 21
    Private Const COL_DX6_DESC = 22
    Private Const COL_DX7_ID = 23
    Private Const COL_DX7_CODE = 24
    Private Const COL_DX7_DESC = 25
    Private Const COL_DX8_ID = 26
    Private Const COL_DX8_CODE = 27
    Private Const COL_DX8_DESC = 28
    Private Const COL_MOD1_ID = 29
    Private Const COL_MOD1_CODE = 30
    Private Const COL_MOD1_DESC = 31
    Private Const COL_MOD2_ID = 32
    Private Const COL_MOD2_CODE = 33
    Private Const COL_MOD2_DESC = 34
    Private Const COL_MOD3_ID = 35
    Private Const COL_MOD3_CODE = 36
    Private Const COL_MOD3_DESC = 37
    Private Const COL_MOD4_ID = 38
    Private Const COL_MOD4_CODE = 39
    Private Const COL_MOD4_DESC = 40

    Private Const COL_PT_TIMEDTHERAPY = 41
    Private Const COL_PT_UNTIMEDTHERAPY = 42


    Private Const COL_UNIT = 43

    Private Const COL_DX1_SNOMEDCODE = 44
    Private Const COL_DX1_SNOMEDDESC = 45
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX1_SnoMedOneToOne = 46

    Private Const COL_DX2_SNOMEDCODE = 47
    Private Const COL_DX2_SNOMEDDESC = 48
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX2_SnoMedOneToOne = 49

    Private Const COL_DX3_SNOMEDCODE = 50
    Private Const COL_DX3_SNOMEDDESC = 51
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX3_SnoMedOneToOne = 52

    Private Const COL_DX4_SNOMEDCODE = 53
    Private Const COL_DX4_SNOMEDDESC = 54
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX4_SnoMedOneToOne = 55

    Private Const COL_DX5_SNOMEDCODE = 56
    Private Const COL_DX5_SNOMEDDESC = 57
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX5_SnoMedOneToOne = 58

    Private Const COL_DX6_SNOMEDCODE = 59
    Private Const COL_DX6_SNOMEDDESC = 60
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX6_SnoMedOneToOne = 61

    Private Const COL_DX7_SNOMEDCODE = 62
    Private Const COL_DX7_SNOMEDDESC = 63
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX7_SnoMedOneToOne = 64

    Private Const COL_DX8_SNOMEDCODE = 65
    Private Const COL_DX8_SNOMEDDESC = 66
    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
    Private Const COL_DX8_SnoMedOneToOne = 67

    Private Const Col_PT_ReasonConceptID = 68
    Private Const Col_PT_ReasonConceptDesc = 69

    Private Const COL_COUNT = 70

#End Region

#Region " Control Load Event "

    Private oGridListControl As gloUC_GridList

    Private Sub gloUC_Treatment_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If oToolTip IsNot Nothing Then
            oToolTip.RemoveAll()
            oToolTip.Dispose()
            oToolTip = Nothing
        End If
    End Sub

    Private Sub gloUC_Treatment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DesignTreatmentGrid()

    End Sub
#End Region

#Region " C1 Events "

    Private Sub C1Treatment_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Treatment.AfterEdit
        Dim _unit As Decimal = FormatNumber(Convert.ToDecimal(C1Treatment.GetData(C1Treatment.RowSel, COL_UNIT)))
        C1Treatment.SetData(C1Treatment.RowSel, COL_UNIT, 0)
        C1Treatment.SetData(C1Treatment.RowSel, COL_UNIT, _unit)
    End Sub

    Private Sub c1Treatment_BeforeRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1Treatment.BeforeRowColChange
        Try
            If _ItemInserted = False Then
                If oGridListControl IsNot Nothing Then
                    If _LastCode = "" Then
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, "")
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex + 1, "")
                    ElseIf _LastCode <> e.OldRange.Data Then
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, _LastCode)
                        If oGridListControl.ParentColIndex <> COL_UNIT Then
                            C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex + 1, _LastDesc)
                        End If
                        _LastCode = ""
                        _LastDesc = ""
                    End If
                    CloseInternalControl()
                End If

                If e.OldRange.c1 = COL_UNIT And e.OldRange.Data = "0" Then
                    C1Treatment.SetData(e.OldRange.r1, COL_UNIT, "1")
                ElseIf e.OldRange.c1 = COL_UNIT And e.OldRange.Data.ToString <> "" Then
                    C1Treatment.SetData(C1Treatment.RowSel, COL_UNIT, Math.Abs(C1Treatment.GetData(C1Treatment.RowSel, COL_UNIT)).ToString)
                End If
            End If



        Catch ex As Exception
        End Try
    End Sub

    Private Sub c1Treatment_ChangeEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Treatment.ChangeEdit
        Dim _strSearchString As String = ""
        Try

            _strSearchString = C1Treatment.Editor.Text

            If oGridListControl IsNot Nothing Then

                If C1Treatment.Col = COL_CPT_CODE OrElse C1Treatment.Col = COL_DX1_CODE OrElse C1Treatment.Col = COL_DX2_CODE OrElse C1Treatment.Col = COL_DX3_CODE OrElse C1Treatment.Col = COL_DX4_CODE OrElse C1Treatment.Col = COL_DX5_CODE OrElse C1Treatment.Col = COL_DX6_CODE OrElse C1Treatment.Col = COL_DX7_CODE OrElse C1Treatment.Col = COL_DX8_CODE Then
                    Dim _cptCode As String = ""

                    If C1Treatment IsNot Nothing AndAlso C1Treatment.Rows.Count > 0 Then
                        _cptCode = Convert.ToString(C1Treatment.GetData(C1Treatment.RowSel, COL_CPT_CODE))
                        oGridListControl.SelectedCPTCode = _cptCode
                    End If

                    oGridListControl.FillControl(_strSearchString)
                Else
                    oGridListControl.AdvanceSearch(_strSearchString)
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("ERROR : " & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

        End Try


    End Sub

    Private Sub c1Treatment_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Treatment.StartEdit

        Try
            If oGridListControl Is Nothing Then
                _LastCode = Convert.ToString(C1Treatment.GetData(e.Row, e.Col)) ''Convert.ToString added for bugid 92671

                If e.Col = COL_UNIT Then
                    _LastDesc = ""
                Else
                    _LastDesc = Convert.ToString(C1Treatment.GetData(e.Row, e.Col + 1))  ''Convert.ToString added for bugid 92671
                End If
            End If

            C1Treatment.Enabled = False
            Select Case e.Col
                Case COL_CPT_CODE
                    OpenInternalControl(gloGridListControlType.CPT, "CPT", False, e.Row, e.Col, "")
                Case COL_DX1_CODE, COL_DX2_CODE, COL_DX3_CODE, COL_DX4_CODE, COL_DX5_CODE, COL_DX6_CODE, COL_DX7_CODE, COL_DX8_CODE
                    RaiseEvent MouseDoubleClick()
                    If ICDRevision = 10 Then
                        OpenInternalControl(gloGridListControlType.ICD10, "ICD10", False, e.Row, e.Col, "")
                    ElseIf ICDRevision = 9 Then
                        OpenInternalControl(gloGridListControlType.ICD9, "ICD9", False, e.Row, e.Col, "")
                    End If

                Case COL_MOD1_CODE, COL_MOD2_CODE, COL_MOD3_CODE, COL_MOD4_CODE
                    OpenInternalControl(gloGridListControlType.Modifier, "Modifier", False, e.Row, e.Col, "")
            End Select
            C1Treatment.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            C1Treatment.Enabled = True
        End Try
    End Sub

    Private Sub c1Treatment_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles C1Treatment.KeyUp
        Dim _isdeleted As Boolean = True
        _TreatmentModified = True
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlInternalControl.Visible Then
                    If oGridListControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oGridListControl.GetCurrentSelectedItem()
                        If _IsItemSelected Then

                            'If Item is Selected Move to nextcell
                            'MoveNext();

                            '********* Code Commented shifted to oGridListControl_ItemSelected
                        End If
                    End If
                End If

                'If C1Treatment.ColSel = COL_UNIT And C1Treatment.RowSel = C1Treatment.Rows.Count - 1 Then
                '    AddLine()
                'End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                If pnlInternalControl.Visible Then
                    If oGridListControl IsNot Nothing Then
                        oGridListControl.Focus()
                    End If
                End If
            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                If pnlInternalControl.Visible Then
                    If oGridListControl IsNot Nothing Then
                        CloseInternalControl()
                    End If
                End If
                ''Commented As per Discussion on 20150812-To resolve issue #86812-gloEMR>New Exam>DxCPT>After pressing Esc+Right arrow key button ,It is taking Previously inserted CPT Value
                'C1Treatment.SetData(C1Treatment.Row, C1Treatment.Col, _LastCode)
                'If C1Treatment.Col <> COL_UNIT Then
                '    C1Treatment.SetData(C1Treatment.Row, C1Treatment.Col + 1, _LastDesc)
                'End If
            ElseIf e.KeyCode = Keys.Delete Then
                e.SuppressKeyPress = True
                If C1Treatment.ColSel = COL_UNIT Then
                    C1Treatment.SetData(C1Treatment.RowSel, C1Treatment.ColSel, 1)
                ElseIf C1Treatment.Col = COL_PT_TIMEDTHERAPY Then
                    C1Treatment.SetData(C1Treatment.Row, COL_PT_TIMEDTHERAPY, Nothing)
                ElseIf C1Treatment.Col = COL_PT_UNTIMEDTHERAPY Then
                    C1Treatment.SetData(C1Treatment.Row, COL_PT_UNTIMEDTHERAPY, Nothing)
                ElseIf C1Treatment.ColSel <> COL_DOS Then
                    C1Treatment.SetData(C1Treatment.RowSel, C1Treatment.ColSel, "") '' CODE ''
                    C1Treatment.SetData(C1Treatment.RowSel, C1Treatment.ColSel + 1, "") '' DESCRIPTION ''
                    C1Treatment.SetData(C1Treatment.RowSel, C1Treatment.ColSel - 1, "") '' ID ''

                    Dim _ICD9CodeToRemove As String = C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel)
                    Dim _ICD9DescToRemove As String = C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel + 1)
                    RemoveICD9(_ICD9CodeToRemove, _ICD9DescToRemove) '' This will remove ICD9 from unique grid ''
                End If
                RearrangeAfterDelete(C1Treatment.RowSel, C1Treatment.ColSel)
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub C1Treatment_KeyDownEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles C1Treatment.KeyDownEdit
        If e.KeyData = Keys.Down And pnlInternalControl.Visible Then
            e.Handled = True
            If oGridListControl IsNot Nothing Then
                oGridListControl.Focus()
            End If
        End If

        If e.KeyData = Keys.Enter Then
            If C1Treatment.ColSel = COL_UNIT Then
                If C1Treatment.RowSel = C1Treatment.Rows.Count - 1 Then
                    AddLine()
                Else
                    C1Treatment.Select(C1Treatment.RowSel + 1, COL_CPT_CODE)
                End If
            ElseIf C1Treatment.ColSel = COL_PT_TIMEDTHERAPY Or C1Treatment.ColSel = COL_PT_UNTIMEDTHERAPY Then
                MoveNext(e.Col)
            End If
        End If
    End Sub

    Private Sub C1Treatment_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Treatment.MouseDown
        Try
            Dim oHit As C1.Win.C1FlexGrid.HitTestInfo
            oHit = C1Treatment.HitTest(e.X, e.Y)
            Dim _Col As Integer = oHit.Column
            If _Col = 0 Then _Col = COL_CPT_CODE
            If oHit.Row > 0 Then
                C1Treatment.Select(oHit.Row, _Col)
            Else
                If C1Treatment.Row < 0 And C1Treatment.Rows.Count > 1 Then C1Treatment.Select(C1Treatment.Rows.Count - 1, _Col)
            End If
            RaiseEvent MouseDown(sender, e)
        Catch
        End Try
    End Sub

    Private Sub C1Treatment_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Treatment.MouseLeave
        Try
            oToolTip.RemoveAll()
        Catch
        End Try
    End Sub

    Private Sub C1Treatment_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1Treatment.KeyPressEdit
        If e.Col = COL_UNIT Or e.Col = COL_PT_TIMEDTHERAPY Or e.Col = COL_PT_UNTIMEDTHERAPY Then
            If (Char.IsDigit(e.KeyChar) = False) Then
                If (e.KeyChar.ToString() = "-") Then
                    e.Handled = True
                End If
            End If
            If e.Col = COL_PT_TIMEDTHERAPY Or e.Col = COL_PT_UNTIMEDTHERAPY Then
                If (Char.IsDigit(e.KeyChar) = False) Then
                    If (e.KeyChar.ToString() = ".") Then
                        e.Handled = True
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region " Public Methods "
    Public Sub AddLine()

        '' IF PREVIOUS LINE DOES NOT HAVE ANY CPT '' THEN DON'T ADD LINE ''
        If C1Treatment.Rows.Count > 1 Then
            If C1Treatment.GetData(C1Treatment.Rows.Count - 1, COL_CPT_CODE) = "" Then
                C1Treatment.Focus()
                C1Treatment.Select(C1Treatment.Rows.Count - 1, COL_CPT_CODE)
                Exit Sub
            End If
        End If

        C1Treatment.Rows.Add()
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_LINE_NO, Convert.ToString(C1Treatment.Rows.Count - 1))
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_DOS, _DOS.ToString)
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_UNIT, "1")

        'Add the previous Line Modifiers to newly added line

        If C1Treatment.Rows.Count > 2 Then
            Dim _LastLineNumber As Integer = C1Treatment.Rows.Count - 1
            Dim _SecondLastLineNumber As Integer = _LastLineNumber - 1


            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX1_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX1_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX1_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX1_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX1_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX1_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX1_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX2_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX2_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX2_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX2_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX2_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX2_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX2_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX3_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX3_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX3_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX3_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX3_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX3_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX3_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX4_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX4_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX4_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX4_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX4_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX4_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX4_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX5_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX5_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX5_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX5_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX5_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX5_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX5_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX6_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX6_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX6_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX6_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX6_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX6_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX6_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX7_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX7_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX7_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX7_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX7_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX7_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX7_SnoMedOneToOne))
            End If

            If Convert.ToString(C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_CODE)) <> "" Then
                C1Treatment.SetData(_LastLineNumber, COL_DX8_ID, 0)
                C1Treatment.SetData(_LastLineNumber, COL_DX8_CODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_CODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX8_DESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_DESC))
                C1Treatment.SetData(_LastLineNumber, COL_DX8_SNOMEDCODE, C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_SNOMEDCODE))
                C1Treatment.SetData(_LastLineNumber, COL_DX8_SNOMEDDESC, C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_SNOMEDDESC))
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                C1Treatment.SetData(_LastLineNumber, COL_DX8_SnoMedOneToOne, C1Treatment.GetData(_SecondLastLineNumber, COL_DX8_SnoMedOneToOne))
            End If

            '' SELECT CPT COLUMN ''
            C1Treatment.Focus()
            C1Treatment.Select(_LastLineNumber, COL_CPT_CODE)
            _TreatmentModified = True
        Else
            C1Treatment.Focus()
            C1Treatment.Select(C1Treatment.Rows.Count - 1, COL_CPT_CODE)
        End If

    End Sub
    Public Sub FillPQRS_Codes(ByVal dtpqrs As DataTable)
        Try

            If Not IsNothing(dtpqrs) Then
                'For Each dr As DataRow In dtpqrs.Rows
                Dim rowcnt As Integer = 0
                Dim strbuild As New System.Text.StringBuilder()
                '' AddLine()
                Dim strcptcode As String = ""
                For rowcnt = 0 To C1Treatment.Rows.Count - 1
                    strcptcode = Convert.ToString(C1Treatment.GetData(rowcnt, COL_CPT_CODE))
                    If (strcptcode.Trim().Length > 0) Then
                        strbuild.Append("'" & strcptcode & "',")
                    End If
                Next rowcnt

                'Next
                Dim strcodes As String = ""
                If strbuild.ToString().Length > 1 Then
                    strcodes = strbuild.ToString().Substring(0, strbuild.Length - 1)

                    Dim drr() As DataRow = dtpqrs.Select("sCPTCode not in(" & strcodes & ")")
                    If (drr.Length > 0) Then
                        For Len As Integer = 0 To drr.Length - 1
                            AddLine()
                            If (C1Treatment.Rows.Count = 1) Then
                                AddRow()
                            End If
                            C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_CPT_CODE, Convert.ToString(drr(Len)(0)))
                        Next
                    End If
                End If
                dtpqrs.Dispose()
                dtpqrs = Nothing
                strbuild = Nothing
            End If
         

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub RemoveLine()
        If C1Treatment.RowSel >= 0 Then

            If MessageBox.Show("Are you sure you want to remove selected line?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                C1Treatment.Focus()
                C1Treatment.Select(C1Treatment.RowSel, COL_CPT_CODE)
                Exit Sub
            End If

            '' TO REMOVE ICD9 FROM UNIQUE GRID ''
            Dim arrICD9s As New ArrayList
            Dim _Row As Integer = C1Treatment.RowSel
            Dim oICD9 As gloGeneralItem.gloItem

            If C1Treatment.GetData(_Row, COL_DX1_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX1_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX1_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If

            If C1Treatment.GetData(_Row, COL_DX2_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX2_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX2_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX3_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX3_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX3_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX4_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX4_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX4_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX5_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX5_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX5_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX6_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX6_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX6_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX7_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX7_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX7_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If


            If C1Treatment.GetData(_Row, COL_DX8_CODE) <> "" Then
                oICD9 = New gloGeneralItem.gloItem
                oICD9.Code = C1Treatment.GetData(_Row, COL_DX8_CODE)
                oICD9.Description = C1Treatment.GetData(_Row, COL_DX8_DESC)
                arrICD9s.Add(oICD9)
                oICD9.Dispose()
            End If

            C1Treatment.Rows.Remove(_Row)

            For i As Integer = 0 To arrICD9s.Count - 1
                oICD9 = CType(arrICD9s(i), gloGeneralItem.gloItem)
                RemoveICD9(oICD9.Code, oICD9.Description)
            Next

            For iRow As Integer = 1 To C1Treatment.Rows.Count - 1
                C1Treatment.SetData(iRow, COL_LINE_NO, iRow)
            Next

            _TreatmentModified = True

            If C1Treatment.Rows.Count > 1 Then
                C1Treatment.Focus()
                C1Treatment.Select(C1Treatment.Rows.Count - 1, COL_CPT_CODE)
            End If

        End If
    End Sub

    Public Function GetTreatment() As ArrayList
        Dim arrTreatment As New ArrayList
        Dim LastICDRevision As Integer = 0
        Try
            If C1Treatment.Rows.Count > 1 Then

                '' REMOVE BLANK ROWS ''

                'Dim oRange As CellRange
                'For iRow As Integer = C1Treatment.Rows.Count - 1 To 1 Step -1
                '    oRange = C1Treatment.GetCellRange(iRow, COL_CPT_CODE, iRow, COL_MOD4_CODE)
                '    If oRange.Clip.Trim = "" Then
                '        C1Treatment.Rows.Remove(iRow)
                '    End If
                'Next


                '' REMOVE REPEATED ROWS ''
                'If C1Treatment.Rows.Count > 2 Then
                '    Dim oBottomRange As CellRange
                '    Dim oTopRange As CellRange

                '    For iRow As Integer = C1Treatment.Rows.Count - 2 To 1 Step -1
                '        oBottomRange = New CellRange
                '        oTopRange = New CellRange

                '        oBottomRange = C1Treatment.GetCellRange(iRow + 1, COL_CPT_CODE, iRow + 1, COL_UNIT)
                '        oTopRange = C1Treatment.GetCellRange(iRow, COL_CPT_CODE, iRow, COL_UNIT)

                '        If oBottomRange.Clip.ToString.Trim = oTopRange.Clip.ToString.Trim And _
                '        C1Treatment.GetData(iRow, COL_CPT_CODE) = "" Then
                '            C1Treatment.Rows.Remove(iRow)
                '        End If
                '    Next

                '    oBottomRange = Nothing
                '    oTopRange = Nothing
                'End If

                Dim oList As gloEMRGeneralLibrary.Glogeneral.myList
                Dim oRange As CellRange

                For iRow As Integer = 1 To C1Treatment.Rows.Count - 1

                    '' REMOVE BLANK ROWS '' ''if condition added for Bug #92395 empty line getting added   
                    If (Convert.ToString(C1Treatment.GetData(iRow, COL_CPT_CODE)).Trim() = "") Then
                        If (Convert.ToString(C1Treatment.GetData(iRow, COL_DX1_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX2_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX3_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX4_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX5_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX6_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX7_CODE)).Trim() = "" And Convert.ToString(C1Treatment.GetData(iRow, COL_DX8_CODE)).Trim() = "") Then
                            Continue For
                        End If
                    End If
                    oRange = C1Treatment.GetCellRange(iRow, COL_CPT_CODE, iRow, COL_MOD4_CODE)
                    If oRange.Clip.Trim = "" Then
                        Continue For
                    End If


                    '' CPT ''
                    ' oList = New gloEMRGeneralLibrary.Glogeneral.myList
                    oList = GetCPT(iRow, iRow)
                    LastICDRevision = oList.nICDRevision
                    arrTreatment.Add(oList)

                    '' ICD9 1 ''
                    If C1Treatment.GetData(iRow, COL_DX1_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX1_CODE, iRow)
                        If LastICDRevision <> 0 Then
                            If LastICDRevision <> oList.nICDRevision Then
                                MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'C1Treatment.Rows(iRow).Selected() = True
                                Return Nothing
                            Else
                                LastICDRevision = oList.nICDRevision
                            End If
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 2 ''
                    If C1Treatment.GetData(iRow, COL_DX2_CODE) <> "" Then
                        ' oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX2_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 3 ''
                    If C1Treatment.GetData(iRow, COL_DX3_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX3_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 4 ''
                    If C1Treatment.GetData(iRow, COL_DX4_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX4_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 5 ''
                    If C1Treatment.GetData(iRow, COL_DX5_CODE) <> "" Then
                        ' oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX5_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 6 ''
                    If C1Treatment.GetData(iRow, COL_DX6_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX6_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 7 ''
                    If C1Treatment.GetData(iRow, COL_DX7_CODE) <> "" Then
                        ' oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX7_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' ICD9 8 ''
                    If C1Treatment.GetData(iRow, COL_DX8_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetICD9(iRow, COL_DX8_CODE, iRow)
                        If LastICDRevision <> oList.nICDRevision Then
                            MessageBox.Show("ICD Type Mismatch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            C1Treatment.Rows(iRow).Selected() = True
                            Return Nothing
                        Else
                            LastICDRevision = oList.nICDRevision
                        End If
                        arrTreatment.Add(oList)
                    End If

                    '' MODIFIER 1 ''
                    If C1Treatment.GetData(iRow, COL_MOD1_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetModifier(iRow, COL_MOD1_CODE, iRow)
                        arrTreatment.Add(oList)
                    End If

                    '' MODIFIER 2 ''
                    If C1Treatment.GetData(iRow, COL_MOD2_CODE) <> "" Then
                        '   oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetModifier(iRow, COL_MOD2_CODE, iRow)
                        arrTreatment.Add(oList)
                    End If

                    '' MODIFIER 3 ''
                    If C1Treatment.GetData(iRow, COL_MOD3_CODE) <> "" Then
                        ' oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetModifier(iRow, COL_MOD3_CODE, iRow)
                        arrTreatment.Add(oList)
                    End If

                    '' MODIFIER 4 ''
                    If C1Treatment.GetData(iRow, COL_MOD4_CODE) <> "" Then
                        '  oList = New gloEMRGeneralLibrary.Glogeneral.myList
                        oList = GetModifier(iRow, COL_MOD4_CODE, iRow)
                        arrTreatment.Add(oList)
                    End If

                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return arrTreatment
    End Function

    Public Sub FillTreatment(ByVal arrTreatment As ArrayList)
        _TreatmentFilling = True
        Try
            'If arrTreatment IsNot Nothing Then
            '    If arrTreatment.Count > 0 Then
            Dim oList As gloEMRGeneralLibrary.Glogeneral.myList
            Dim oItem As gloGeneralItem.gloItem
            Dim oSubItem As gloGeneralItem.gloSubItem

            C1Treatment.Rows.Count = 1
            Dim _FoundRow As Integer = -1
            If arrTreatment IsNot Nothing Then
                If arrTreatment.Count > 0 Then


                    For i As Integer = 0 To arrTreatment.Count - 1
                        oList = CType(arrTreatment(i), gloEMRGeneralLibrary.Glogeneral.myList)

                        If C1Treatment.Rows.Count > oList.ICD9No Then
                            If oList.Code <> "" Then
                                '' ADD ICD9
                                oItem = New gloGeneralItem.gloItem
                                oItem.Code = oList.Code
                                oItem.Description = oList.Description

                                oSubItem = New gloGeneralItem.gloSubItem
                                oSubItem.Code = oList.SnomedID
                                oSubItem.Description = oList.SnomedDesc
                                oItem.SubItems.Add(oSubItem)

                                AddICD9(oItem, oList.ICD9No, COL_DX8_CODE)
                                oSubItem.Dispose()
                                oItem.SubItems.Clear()
                                oItem.Dispose()
                            Else
                                '' ADD MODIFIER ''
                                oItem = New gloGeneralItem.gloItem
                                oItem.Code = oList.Value
                                oItem.Description = oList.ParameterName
                                AddModifier(oItem, oList.ICD9No, COL_MOD4_CODE)
                                oItem.Dispose()
                            End If
                        Else
                            AddRow()
                            Dim _LastRow As Integer = C1Treatment.Rows.Count - 1
                            oItem = New gloGeneralItem.gloItem
                            oItem.Code = oList.HistoryCategory
                            oItem.Description = oList.HistoryItem
                            AddCPT(oItem, _LastRow)

                            'C1Treatment.SetData(_LastRow, COL_UNIT, oList.TemplateResult)
                            C1Treatment.SetData(_LastRow, COL_UNIT, CType(oList.TemplateResult, Double))

                            If Not String.IsNullOrEmpty(oList.TimedTherapy) Then
                                C1Treatment.SetData(_LastRow, COL_PT_TIMEDTHERAPY, CType(oList.TimedTherapy, Integer))
                            End If

                            If Not String.IsNullOrEmpty(oList.UnTimedTherapy) Then
                                C1Treatment.SetData(_LastRow, COL_PT_UNTIMEDTHERAPY, CType(oList.UnTimedTherapy, Integer))
                            End If

                            If Not String.IsNullOrEmpty(oList.ReasonConceptID) Then
                                C1Treatment.SetData(_LastRow, Col_PT_ReasonConceptID, CType(oList.ReasonConceptID, String))
                            End If

                            If Not String.IsNullOrEmpty(oList.ReasonConceptDesc) Then
                                C1Treatment.SetData(_LastRow, Col_PT_ReasonConceptDesc, CType(oList.ReasonConceptDesc, String))
                            End If

                            'Resolving Bug No. 71345 ::Problem list> open exam> Problem List> It shows only one problem to which the exam is associated.
                            If oItem.Code = "" And oItem.Description = "" Then
                                i = i - 1
                            End If
                            oItem.Dispose()
                            '
                        End If
                    Next
                End If
            End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _TreatmentFilling = False
        End Try
    End Sub

    Public Sub SelectLastCPT()
        C1Treatment.Focus()
        C1Treatment.Select(C1Treatment.Rows.Count - 1, COL_CPT_CODE, True)
    End Sub

    Public Sub SelectCurrentRowDOS()
        If C1Treatment.Row > 0 Then
            C1Treatment.Focus()
            C1Treatment.Select(C1Treatment.Row, COL_DOS, True)
        End If
    End Sub

    Public Sub MoveUp()
        Try
            If C1Treatment.Rows.Count > 1 And C1Treatment.RowSel > -1 Then
                If C1Treatment.RowSel > 1 Then
                    C1Treatment.SetData(C1Treatment.RowSel - 1, COL_LINE_NO, C1Treatment.RowSel)
                    C1Treatment.SetData(C1Treatment.RowSel, COL_LINE_NO, C1Treatment.RowSel - 1)
                    C1Treatment.Rows.Move(C1Treatment.RowSel, C1Treatment.RowSel - 1)
                    _TreatmentModified = True
                End If
            End If
        Catch
        End Try
    End Sub

    Public Sub MoveDown()
        Try
            If C1Treatment.Rows.Count > 1 And C1Treatment.RowSel > -1 Then
                If C1Treatment.RowSel <> C1Treatment.Rows.Count - 1 Then
                    C1Treatment.SetData(C1Treatment.RowSel + 1, COL_LINE_NO, C1Treatment.RowSel)
                    C1Treatment.SetData(C1Treatment.RowSel, COL_LINE_NO, C1Treatment.RowSel + 1)
                    C1Treatment.Rows.Move(C1Treatment.RowSel, C1Treatment.RowSel + 1)
                    _TreatmentModified = True
                End If
            End If
        Catch
        End Try
    End Sub

    Public Sub MoveRight()
        Try
            If C1Treatment.Rows.Count > 1 And C1Treatment.RowSel > -1 Then
                Dim _Row As Integer = C1Treatment.RowSel
                Dim _Col As Integer = C1Treatment.ColSel

                If (_Col >= COL_DX1_CODE And _Col <= COL_DX7_CODE) Or (_Col >= COL_MOD1_CODE And _Col <= COL_MOD3_CODE) Then
                    If C1Treatment.GetData(_Row, _Col + 3) <> "" Then

                        Dim _Code As String = C1Treatment.GetData(_Row, _Col + 3)
                        Dim _Description As String = C1Treatment.GetData(_Row, _Col + 4)

                        C1Treatment.SetData(_Row, _Col + 3, C1Treatment.GetData(_Row, _Col)) '' SHIFT CODE ''
                        C1Treatment.SetData(_Row, _Col + 4, C1Treatment.GetData(_Row, _Col + 1)) '' SHIFT DESCRIPTION ''

                        C1Treatment.SetData(_Row, _Col, _Code) '' SHIFT CODE ''
                        C1Treatment.SetData(_Row, _Col + 1, _Description) '' SHIFT DESCRIPTION ''

                        '' MAKE SELECTION ''
                        C1Treatment.Focus()
                        C1Treatment.Select(_Row, _Col + 3, _Row, _Col + 3, True)
                        _TreatmentModified = True

                    Else

                        '' MAKE SELECTION ''
                        C1Treatment.Focus()

                    End If
                Else

                    '' MAKE SELECTION ''
                    C1Treatment.Focus()
                End If
            End If
        Catch
        End Try
    End Sub

    Public Sub MoveLeft()
        Try
            If C1Treatment.Rows.Count > 1 And C1Treatment.RowSel > -1 Then
                Dim _Row As Integer = C1Treatment.RowSel
                Dim _Col As Integer = C1Treatment.ColSel

                If (_Col >= COL_DX2_CODE And _Col <= COL_DX8_CODE) Or (_Col >= COL_MOD2_CODE And _Col <= COL_MOD4_CODE) Then
                    If C1Treatment.GetData(_Row, _Col) <> "" Then

                        Dim _Code As String = C1Treatment.GetData(_Row, _Col - 3)
                        Dim _Description As String = C1Treatment.GetData(_Row, _Col - 2)

                        C1Treatment.SetData(_Row, _Col - 3, C1Treatment.GetData(_Row, _Col)) '' SHIFT CODE ''
                        C1Treatment.SetData(_Row, _Col - 2, C1Treatment.GetData(_Row, _Col + 1)) '' SHIFT DESCRIPTION ''

                        C1Treatment.SetData(_Row, _Col, _Code) '' SHIFT CODE ''
                        C1Treatment.SetData(_Row, _Col + 1, _Description) '' SHIFT DESCRIPTION ''

                        '' MAKE SELECTION ''
                        C1Treatment.Focus()
                        C1Treatment.Select(_Row, _Col - 3, _Row, _Col - 3, True)
                        _TreatmentModified = True

                    Else

                        '' MAKE SELECTION ''
                        C1Treatment.Focus()

                    End If
                Else

                    '' MAKE SELECTION ''
                    C1Treatment.Focus()
                End If
            End If
        Catch
        End Try
    End Sub

    Public Function GetICDRevisionOfSelectedCell() As Int16
        Dim sICDRevision As String = String.Empty
        Dim nReturned As Int16 = 0
        Dim ndescriptionColumnIndex As Int16 = -1

        Try
            If C1Treatment IsNot Nothing Then
                If C1Treatment.RowSel > 0 AndAlso (C1Treatment.ColSel >= COL_DX1_CODE AndAlso C1Treatment.ColSel <= COL_DX8_CODE) Then

                    Select Case C1Treatment.ColSel
                        Case COL_DX1_CODE
                            ndescriptionColumnIndex = COL_DX1_DESC
                        Case COL_DX2_CODE
                            ndescriptionColumnIndex = COL_DX2_DESC
                        Case COL_DX3_CODE
                            ndescriptionColumnIndex = COL_DX3_DESC
                        Case COL_DX4_CODE
                            ndescriptionColumnIndex = COL_DX4_DESC
                        Case COL_DX5_CODE
                            ndescriptionColumnIndex = COL_DX5_DESC
                        Case COL_DX6_CODE
                            ndescriptionColumnIndex = COL_DX6_DESC
                        Case COL_DX7_CODE
                            ndescriptionColumnIndex = COL_DX7_DESC
                        Case COL_DX8_CODE
                            ndescriptionColumnIndex = COL_DX8_DESC
                        Case Else
                            ndescriptionColumnIndex = -1
                    End Select

                    If ndescriptionColumnIndex > 0 Then
                        sICDRevision = Convert.ToString(C1Treatment.GetData(C1Treatment.RowSel, ndescriptionColumnIndex))
                        If sICDRevision.Trim() <> "" AndAlso sICDRevision.Contains("~") Then
                            sICDRevision = Convert.ToString(C1Treatment.GetData(C1Treatment.RowSel, ndescriptionColumnIndex)).Split("~")(1)
                            Int16.TryParse(sICDRevision, nReturned)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            nReturned = 0
        End Try

        Return nReturned
    End Function

    Public Sub GetIcdRevision(ByVal nrevision As Integer)
        ICDRevision = nrevision
        If pnlInternalControl.Visible Then
            C1Treatment.SetData(C1Treatment.RowSel, C1Treatment.ColSel, "")
        End If
        CloseInternalControl()

    End Sub
    Public Function isDiagnosisColumn(ByVal nColumnNumber As Integer) As Boolean
        Try
            If nColumnNumber >= COL_DX1_CODE And nColumnNumber <= COL_DX8_CODE Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Function GetSelectedCellDxCode() As ArrayList

        Dim cellDx As ArrayList = Nothing

        Try
            If Not IsNothing(C1Treatment) AndAlso C1Treatment.Rows.Count > 0 Then
                If C1Treatment.RowSel > 0 Then

                    If C1Treatment.ColSel >= COL_DX1_CODE And C1Treatment.ColSel <= COL_DX8_CODE Then

                        If Not IsNothing(C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel)) Then
                            cellDx = New ArrayList()
                            cellDx.Add(Convert.ToString(C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel))) 'Code
                            cellDx.Add(Convert.ToString(C1Treatment.GetData(C1Treatment.RowSel, C1Treatment.ColSel + 1))) 'Dx Description
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally

        End Try

        Return cellDx

    End Function


    Public Function TreatmentAvailable() As Boolean
        Try
            If C1Treatment.Rows.Count > 1 Then
                Dim oRange As CellRange
                For iRow As Integer = 1 To C1Treatment.Rows.Count - 1
                    oRange = C1Treatment.GetCellRange(iRow, COL_CPT_CODE, iRow, COL_MOD4_CODE)
                    If oRange.Clip.Trim <> "" Then
                        Return True
                    End If
                Next
            End If
            Return False
        Catch
            Return True
        End Try
    End Function
    Public Function GetC1TreatementCount() As Integer
        Dim _cnt As Integer = 0
        _cnt = C1Treatment.Rows.Count
        Return _cnt
    End Function
#End Region

#Region " Private Methods "
    Private Sub DesignTreatmentGrid()
        Try

            C1Treatment.Clear()
            C1Treatment.Rows.Count = 2
            C1Treatment.Rows.Fixed = 1
            C1Treatment.Cols.Count = COL_COUNT
            C1Treatment.Cols.Fixed = 1

            C1Treatment.AllowSorting = AllowSortingEnum.None
            C1Treatment.AllowResizing = AllowResizingEnum.None
            C1Treatment.AllowDragging = AllowDraggingEnum.None
            C1Treatment.AllowEditing = True

            '' DATA TYPE ''
            C1Treatment.Cols(COL_DOS).DataType = Type.GetType("System.DateTime")
            C1Treatment.Cols(COL_UNIT).DataType = Type.GetType("System.Decimal")
            'C1Treatment.Cols(COL_UNIT).EditMask = "###"

            '' WIDTH ''
            C1Treatment.Cols(COL_LINE_NO).Width = 30
            C1Treatment.Cols(COL_CPT_ID).Width = 60
            C1Treatment.Cols(COL_CPT_CODE).Width = 60
            C1Treatment.Cols(COL_CPT_DESC).Width = 60
            C1Treatment.Cols(COL_DX1_ID).Width = 60
            C1Treatment.Cols(COL_DX1_DESC).Width = 60
            C1Treatment.Cols(COL_DX2_ID).Width = 60
            C1Treatment.Cols(COL_DX2_DESC).Width = 60
            C1Treatment.Cols(COL_DX3_ID).Width = 60
            C1Treatment.Cols(COL_DX3_DESC).Width = 60
            C1Treatment.Cols(COL_DX4_ID).Width = 60
            C1Treatment.Cols(COL_DX4_DESC).Width = 60
            C1Treatment.Cols(COL_DX5_ID).Width = 60
            C1Treatment.Cols(COL_DX5_DESC).Width = 60
            C1Treatment.Cols(COL_DX6_ID).Width = 60
            C1Treatment.Cols(COL_DX6_DESC).Width = 60
            C1Treatment.Cols(COL_DX7_ID).Width = 60
            C1Treatment.Cols(COL_DX7_DESC).Width = 60
            C1Treatment.Cols(COL_DX8_ID).Width = 60
            C1Treatment.Cols(COL_DX8_DESC).Width = 60
            If _ICD9count = enumIC9Count.Show_8_ICD9 Then
                C1Treatment.Cols(COL_DX1_CODE).Width = 60
                C1Treatment.Cols(COL_DX2_CODE).Width = 60
                C1Treatment.Cols(COL_DX3_CODE).Width = 60
                C1Treatment.Cols(COL_DX4_CODE).Width = 60
                C1Treatment.Cols(COL_DX5_CODE).Width = 60
                C1Treatment.Cols(COL_DX6_CODE).Width = 60
                C1Treatment.Cols(COL_DX7_CODE).Width = 60
                C1Treatment.Cols(COL_DX8_CODE).Width = 60
            Else
                C1Treatment.Cols(COL_DX1_CODE).Width = 80
                C1Treatment.Cols(COL_DX2_CODE).Width = 80
                C1Treatment.Cols(COL_DX3_CODE).Width = 80
                C1Treatment.Cols(COL_DX4_CODE).Width = 80
                C1Treatment.Cols(COL_DX5_CODE).Width = 80
                C1Treatment.Cols(COL_DX6_CODE).Width = 80
                C1Treatment.Cols(COL_DX7_CODE).Width = 80
                C1Treatment.Cols(COL_DX8_CODE).Width = 80
            End If

            C1Treatment.Cols(COL_MOD1_ID).Width = 60
            C1Treatment.Cols(COL_MOD1_DESC).Width = 60
            C1Treatment.Cols(COL_MOD2_ID).Width = 60
            C1Treatment.Cols(COL_MOD2_DESC).Width = 60
            C1Treatment.Cols(COL_MOD3_ID).Width = 60
            C1Treatment.Cols(COL_MOD3_DESC).Width = 60
            C1Treatment.Cols(COL_MOD4_ID).Width = 60
            C1Treatment.Cols(COL_MOD4_DESC).Width = 60

            If _ModifierCount = enumModifierCount.Show_4_Modifier Then
                C1Treatment.Cols(COL_MOD1_CODE).Width = 60
                C1Treatment.Cols(COL_MOD2_CODE).Width = 60
                C1Treatment.Cols(COL_MOD3_CODE).Width = 60
                C1Treatment.Cols(COL_MOD4_CODE).Width = 60
            Else
                C1Treatment.Cols(COL_MOD1_CODE).Width = 80
                C1Treatment.Cols(COL_MOD2_CODE).Width = 80
                C1Treatment.Cols(COL_MOD3_CODE).Width = 80
                C1Treatment.Cols(COL_MOD4_CODE).Width = 80
            End If


            C1Treatment.Cols(COL_UNIT).Width = 60
            C1Treatment.Cols(COL_PT_TIMEDTHERAPY).Width = 80
            C1Treatment.Cols(COL_PT_UNTIMEDTHERAPY).Width = 80
            C1Treatment.Cols(Col_PT_ReasonConceptID).Width = 0
            C1Treatment.Cols(Col_PT_ReasonConceptDesc).Width = 0

            C1Treatment.SetData(0, COL_LINE_NO, "No.")
            C1Treatment.SetData(0, COL_DOS, "DOS")
            C1Treatment.SetData(0, COL_CPT_ID, "CPT ID")
            C1Treatment.SetData(0, COL_CPT_CODE, "CPT")
            C1Treatment.SetData(0, COL_CPT_DESC, "CPT DESC")
            C1Treatment.SetData(0, COL_DX1_ID, "Dx1 ID")
            C1Treatment.SetData(0, COL_DX1_CODE, "Dx1")
            C1Treatment.SetData(0, COL_DX1_DESC, "Dx1 DESC")
            C1Treatment.SetData(0, COL_DX2_ID, "Dx2 ID")
            C1Treatment.SetData(0, COL_DX2_CODE, "Dx2")
            C1Treatment.SetData(0, COL_DX2_DESC, "Dx2 DESC")
            C1Treatment.SetData(0, COL_DX3_ID, "Dx3 ID")
            C1Treatment.SetData(0, COL_DX3_CODE, "Dx3")
            C1Treatment.SetData(0, COL_DX3_DESC, "Dx3 DESC")
            C1Treatment.SetData(0, COL_DX4_ID, "Dx4 ID")
            C1Treatment.SetData(0, COL_DX4_CODE, "Dx4")
            C1Treatment.SetData(0, COL_DX4_DESC, "Dx4 DESC")
            C1Treatment.SetData(0, COL_DX5_ID, "Dx5 ID")
            C1Treatment.SetData(0, COL_DX5_CODE, "Dx5")
            C1Treatment.SetData(0, COL_DX5_DESC, "Dx5 DESC")
            C1Treatment.SetData(0, COL_DX6_ID, "Dx61 ID")
            C1Treatment.SetData(0, COL_DX6_CODE, "Dx6")
            C1Treatment.SetData(0, COL_DX6_DESC, "Dx6 DESC")
            C1Treatment.SetData(0, COL_DX7_ID, "Dx7 ID")
            C1Treatment.SetData(0, COL_DX7_CODE, "Dx7")
            C1Treatment.SetData(0, COL_DX7_DESC, "Dx7 DESC")
            C1Treatment.SetData(0, COL_DX8_ID, "Dx8 ID")
            C1Treatment.SetData(0, COL_DX8_CODE, "Dx8")
            C1Treatment.SetData(0, COL_DX8_DESC, "Dx8 DESC")
            C1Treatment.SetData(0, COL_MOD1_ID, "M1 ID")
            C1Treatment.SetData(0, COL_MOD1_CODE, "M1")
            C1Treatment.SetData(0, COL_MOD1_DESC, "M1 DESC")
            C1Treatment.SetData(0, COL_MOD2_ID, "M2 ID")
            C1Treatment.SetData(0, COL_MOD2_CODE, "M2")
            C1Treatment.SetData(0, COL_MOD2_DESC, "M2 DESC")
            C1Treatment.SetData(0, COL_MOD3_ID, "M3 ID")
            C1Treatment.SetData(0, COL_MOD3_CODE, "M3")
            C1Treatment.SetData(0, COL_MOD3_DESC, "M3 DESC")
            C1Treatment.SetData(0, COL_MOD4_ID, "M4 ID")
            C1Treatment.SetData(0, COL_MOD4_CODE, "M4")
            C1Treatment.SetData(0, COL_MOD4_DESC, "M4 DESC")
            C1Treatment.SetData(0, COL_UNIT, "Units")
            C1Treatment.SetData(0, COL_PT_TIMEDTHERAPY, "Timed PT")
            C1Treatment.SetData(0, COL_PT_UNTIMEDTHERAPY, "Untimed PT")
            C1Treatment.SetData(0, Col_PT_ReasonConceptID, "Reason Concept ID")
            C1Treatment.SetData(0, Col_PT_ReasonConceptDesc, "Reason Concept Desc")

            '' HIDE ALL ID COLS ''
            C1Treatment.Cols(COL_CPT_ID).Visible = False
            C1Treatment.Cols(COL_DX1_ID).Visible = False
            C1Treatment.Cols(COL_DX2_ID).Visible = False
            C1Treatment.Cols(COL_DX3_ID).Visible = False
            C1Treatment.Cols(COL_DX4_ID).Visible = False
            C1Treatment.Cols(COL_DX5_ID).Visible = False
            C1Treatment.Cols(COL_DX6_ID).Visible = False
            C1Treatment.Cols(COL_DX7_ID).Visible = False
            C1Treatment.Cols(COL_DX8_ID).Visible = False
            C1Treatment.Cols(COL_MOD1_ID).Visible = False
            C1Treatment.Cols(COL_MOD2_ID).Visible = False
            C1Treatment.Cols(COL_MOD3_ID).Visible = False
            C1Treatment.Cols(COL_MOD4_ID).Visible = False

            '' HIDE ALL DESCRIPTIONS ''
            C1Treatment.Cols(COL_CPT_DESC).Visible = False
            C1Treatment.Cols(COL_DX1_DESC).Visible = False
            C1Treatment.Cols(COL_DX2_DESC).Visible = False
            C1Treatment.Cols(COL_DX3_DESC).Visible = False
            C1Treatment.Cols(COL_DX4_DESC).Visible = False
            C1Treatment.Cols(COL_DX5_DESC).Visible = False
            C1Treatment.Cols(COL_DX6_DESC).Visible = False
            C1Treatment.Cols(COL_DX7_DESC).Visible = False
            C1Treatment.Cols(COL_DX8_DESC).Visible = False
            C1Treatment.Cols(COL_MOD1_DESC).Visible = False
            C1Treatment.Cols(COL_MOD2_DESC).Visible = False
            C1Treatment.Cols(COL_MOD3_DESC).Visible = False
            C1Treatment.Cols(COL_MOD4_DESC).Visible = False

            '' HIDE ICD9 ''
            If _ICD9count = enumIC9Count.Show_4_ICD9 Then
                C1Treatment.Cols(COL_DX5_CODE).Visible = False
                C1Treatment.Cols(COL_DX6_CODE).Visible = False
                C1Treatment.Cols(COL_DX7_CODE).Visible = False
                C1Treatment.Cols(COL_DX8_CODE).Visible = False
            End If

            '' HIDE MODIFIER ''
            If _ModifierCount = enumModifierCount.Show_2_MOdifier Then
                C1Treatment.Cols(COL_MOD3_CODE).Visible = False
                C1Treatment.Cols(COL_MOD4_CODE).Visible = False
            End If

            '' HIDE PT BILLING SETTINGS ''
            If IsExamPTBillingEnabled Then
                C1Treatment.Cols(COL_PT_TIMEDTHERAPY).Visible = True
                C1Treatment.Cols(COL_PT_UNTIMEDTHERAPY).Visible = True
                C1Treatment.Cols(COL_PT_TIMEDTHERAPY).DataType = GetType(Integer)
                C1Treatment.Cols(COL_PT_UNTIMEDTHERAPY).DataType = GetType(Integer)

            Else
                C1Treatment.Cols(COL_PT_TIMEDTHERAPY).Visible = False
                C1Treatment.Cols(COL_PT_UNTIMEDTHERAPY).Visible = False
            End If


            C1Treatment.Cols(COL_DX1_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX1_SNOMEDDESC).Visible = False

            '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
            C1Treatment.Cols(COL_DX1_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX2_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX3_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX4_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX5_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX6_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX7_SnoMedOneToOne).Visible = False
            C1Treatment.Cols(COL_DX8_SnoMedOneToOne).Visible = False

            C1Treatment.Cols(COL_DX2_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX2_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX3_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX3_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX4_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX4_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX5_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX5_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX6_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX6_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX7_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX7_SNOMEDDESC).Visible = False
            C1Treatment.Cols(COL_DX8_SNOMEDCODE).Visible = False
            C1Treatment.Cols(COL_DX8_SNOMEDDESC).Visible = False


            C1Treatment.Cols(Col_PT_ReasonConceptID).Visible = False
            C1Treatment.Cols(Col_PT_ReasonConceptDesc).Visible = False

            '' ALIGHNMENT ''
            For iCol As Integer = 1 To COL_COUNT - 1
                C1Treatment.Cols(iCol).TextAlign = TextAlignEnum.LeftCenter
            Next

            '' FIRST LINE NUMBER ''
            C1Treatment.SetData(1, COL_LINE_NO, "1")
            C1Treatment.SetData(1, COL_DOS, _DOS.Date.ToString())
            C1Treatment.SetData(1, COL_UNIT, "1")

            '' NON EDITABLE ''
            C1Treatment.Cols(COL_DOS).AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function OpenInternalControl(ByVal ControlType As gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
        Dim _result As Boolean = False
        Try
            If oGridListControl IsNot Nothing Then
                CloseInternalControl()
            End If
            oGridListControl = New gloUC_GridList(ControlType, False, pnlInternalControl.Width, RowIndex, ColIndex, _DatabaseConnectionString)
            AddHandler oGridListControl.ItemSelected, AddressOf oGridListControl_ItemSelected
            AddHandler oGridListControl.InternalGridKeyDown, AddressOf oGridListControl_InternalGridKeyDown
            oGridListControl.ControlHeader = ControlHeader
            pnlInternalControl.Controls.Add(oGridListControl)
            oGridListControl.Dock = DockStyle.Fill
            If SearchText <> "" Then
                oGridListControl.Search(SearchText, SearchColumn.Code)
            End If
            oGridListControl.Show()

            Dim _x As Integer = C1Treatment.Cols(ColIndex).Left
            Dim _y As Integer = C1Treatment.Rows(RowIndex).Bottom
            Dim _width As Integer = pnlInternalControl.Width
            Dim _height As Integer = pnlInternalControl.Height
            Dim _parentleft As Integer = Me.Parent.Bounds.Left
            Dim _parentwidth As Integer = Me.Parent.Bounds.Width
            Dim _diffFactor As Integer = _parentwidth - _x

            If _diffFactor < _width Then
                _x = Me.Parent.Bounds.Width + (_diffFactor)
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            Else
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            End If

            pnlInternalControl.Visible = True
            RaiseEvent GridListLoaded()
            pnlInternalControl.BringToFront()
            _result = True
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _result = False
        Finally
            RePositionInternalControl()
        End Try
        Return _result
    End Function

    Private Sub RePositionInternalControl()
        Try
            If pnlInternalControl.Visible = True And oGridListControl IsNot Nothing Then
                pnlInternalControl.SetBounds((C1Treatment.Cols(C1Treatment.ColSel).Left + C1Treatment.ScrollPosition.X), C1Treatment.Rows(C1Treatment.RowSel).Bottom, 0, 0, BoundsSpecified.Location)

                Dim _BottomMove As Boolean = False
                '' IF BELOW BOTTOM ''
                If pnlInternalControl.Bottom > Me.Bottom Then
                    pnlInternalControl.SetBounds((C1Treatment.Cols(C1Treatment.ColSel).Right + C1Treatment.ScrollPosition.X + 1), (Me.Height - pnlInternalControl.Height - 1), 0, 0, BoundsSpecified.Location)
                    _BottomMove = True
                End If

                '' IF ABOVE RIGHT ''
                If pnlInternalControl.Right > Me.Right Then
                    If _BottomMove Then
                        pnlInternalControl.SetBounds(((C1Treatment.Cols(C1Treatment.ColSel).Left + C1Treatment.ScrollPosition.X - 1) - pnlInternalControl.Width), pnlInternalControl.Top, 0, 0, BoundsSpecified.Location)
                    Else
                        pnlInternalControl.SetBounds(((C1Treatment.Cols(C1Treatment.ColSel).Left + C1Treatment.ScrollPosition.X - 1) - pnlInternalControl.Width), C1Treatment.Rows(C1Treatment.RowSel).Top, 0, 0, BoundsSpecified.Location)
                    End If
                End If

                '' IF ABOVE TOP ''
                If pnlInternalControl.Top < 0 Then
                    pnlInternalControl.SetBounds(pnlInternalControl.Left, 0, 0, 0, BoundsSpecified.Location)
                End If

            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Function CloseInternalControl() As Boolean
        Dim _result As Boolean = False
        Try
            'SLR: Changed on 4/4/2014
            For i As Integer = pnlInternalControl.Controls.Count - 1 To 0 Step -1
                pnlInternalControl.Controls.RemoveAt(i)
            Next
            If oGridListControl IsNot Nothing Then
                Try
                    RemoveHandler oGridListControl.ItemSelected, AddressOf oGridListControl_ItemSelected
                    RemoveHandler oGridListControl.InternalGridKeyDown, AddressOf oGridListControl_InternalGridKeyDown
                Catch ex As Exception

                End Try

                oGridListControl.Dispose()
                oGridListControl = Nothing
            End If




            pnlInternalControl.Visible = False
            RaiseEvent GridListClosed()
            pnlInternalControl.SendToBack()

            _result = True
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _result = False
        Finally
        End Try
        Return _result
    End Function

    Private Sub oGridListControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try




            If oGridListControl.SelectedItems IsNot Nothing Then
                If oGridListControl.SelectedItems.Count > 0 Then
                    Select Case oGridListControl.ControlType
                        Case gloGridListControlType.CPT
                            If (C1Treatment.Rows.Count > oGridListControl.ParentRowIndex) Then ''added for bugid 92671
                                AddCPT(oGridListControl.SelectedItems(0), oGridListControl.ParentRowIndex)
                            End If
                        Case gloGridListControlType.ICD9, gloGridListControlType.ICD10
                            If (C1Treatment.Rows.Count > oGridListControl.ParentRowIndex) Then
                                If AddICD9(oGridListControl.SelectedItems(0), oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex) Then
                                    RemoveICD9(_LastCode, _LastDesc)
                                Else

                                    C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, _LastCode)
                                End If
                            End If
                        Case gloGridListControlType.Modifier
                            If (C1Treatment.Rows.Count > oGridListControl.ParentRowIndex) Then    ''added for bugid 92671
                                If AddModifier(oGridListControl.SelectedItems(0), oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex) = False Then
                                    C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, _LastCode)
                                End If
                            End If
                    End Select
                    _TreatmentModified = True

                    CloseInternalControl()
                Else
                    If (C1Treatment.Rows.Count > oGridListControl.ParentRowIndex) Then              ''added for bugid 92671
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, "")
                    End If
                    End If
            Else
                If (C1Treatment.Rows.Count > oGridListControl.ParentRowIndex) Then                  ''added for bugid 92671
                    C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, "")
                End If
                End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

    End Sub

    Private Sub oGridListControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If _ItemInserted = False Then
                If oGridListControl IsNot Nothing Then
                    If _LastCode = "" Then
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, "")
                    Else
                        C1Treatment.SetData(oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex, _LastCode)
                        _LastCode = ""
                        _LastDesc = ""
                    End If
                End If
            End If

            CloseInternalControl()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
        End Try
    End Sub

    Private Sub AddCPT(ByVal oCPT As gloGeneralItem.gloItem, ByVal nRow As Integer)
        Try

            C1Treatment.SetData(nRow, COL_CPT_ID, oCPT.ID)
            C1Treatment.SetData(nRow, COL_CPT_CODE, oCPT.Code)
            C1Treatment.SetData(nRow, COL_CPT_DESC, oCPT.Description)

            MoveNext(COL_CPT_CODE)
        Catch ex As Exception
        End Try
    End Sub

    Private Function AddICD9(ByVal oICD9 As gloGeneralItem.gloItem, ByVal nRow As Integer, ByVal nCol As Integer) As Boolean
        Try


            '' CHECK WHETHER SAME ICD9 PRESENT ''
            '' IF NOT THEN ONLY ADD NEW ''
            If oICD9.Code <> "" Then
                If (oICD9.Code = C1Treatment.GetData(nRow, COL_DX1_CODE) And nCol <> COL_DX1_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX2_CODE) And nCol <> COL_DX2_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX3_CODE) And nCol <> COL_DX3_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX4_CODE) And nCol <> COL_DX4_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX5_CODE) And nCol <> COL_DX5_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX6_CODE) And nCol <> COL_DX6_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX7_CODE) And nCol <> COL_DX7_CODE) Or _
                    (oICD9.Code = C1Treatment.GetData(nRow, COL_DX8_CODE) And nCol <> COL_DX8_CODE) Then
                    Dim strMsg As String = ""
                    If ICDRevision = 9 Then
                        strMsg = "Duplicate ICD9 is not allowed."
                    ElseIf ICDRevision = 10 Then
                        strMsg = "Duplicate ICD10 is not allowed."
                    End If
                    If _TreatmentFilling = False Then MessageBox.Show(strMsg, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If


                C1Treatment.SetData(nRow, nCol, "")


                If C1Treatment.GetData(nRow, COL_DX1_CODE) = "" Or COL_DX1_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX1_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX1_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX1_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX1_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX1_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX1_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX1_CODE)

                ElseIf C1Treatment.GetData(nRow, COL_DX2_CODE) = "" Or COL_DX2_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX2_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX2_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX2_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX2_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX2_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX2_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX2_CODE)

                ElseIf C1Treatment.GetData(nRow, COL_DX3_CODE) = "" Or COL_DX3_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX3_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX3_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX3_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX3_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX3_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX3_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX3_CODE)

                ElseIf C1Treatment.GetData(nRow, COL_DX4_CODE) = "" Or COL_DX4_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX4_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX4_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX4_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX4_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX4_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX4_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    If _ICD9count = enumIC9Count.Show_8_ICD9 Then
                        MoveNext(COL_DX4_CODE)
                    Else
                        MoveNext(COL_DX8_CODE)
                    End If

                ElseIf C1Treatment.GetData(nRow, COL_DX5_CODE) = "" Or COL_DX5_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX5_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX5_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX5_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX5_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX5_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX5_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX5_CODE)

                    '' IF SHOW 4 DX SETTING IS ON AND ICD9 OVERFLOWS THEN REDESIGN IT FOR DX8''
                    If _ICD9count = enumIC9Count.Show_4_ICD9 Then
                        _ICD9count = enumIC9Count.Show_8_ICD9
                        ReDesignGrid()
                    End If
                ElseIf C1Treatment.GetData(nRow, COL_DX6_CODE) = "" Or COL_DX6_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX6_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX6_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX6_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX6_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX6_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX6_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX6_CODE)

                ElseIf C1Treatment.GetData(nRow, COL_DX7_CODE) = "" Or COL_DX7_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX7_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX7_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX7_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX7_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX7_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX7_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX7_CODE)

                ElseIf C1Treatment.GetData(nRow, COL_DX8_CODE) = "" Or COL_DX8_CODE = nCol Then
                    C1Treatment.SetData(nRow, COL_DX8_ID, oICD9.ID)
                    C1Treatment.SetData(nRow, COL_DX8_CODE, oICD9.Code)
                    C1Treatment.SetData(nRow, COL_DX8_DESC, oICD9.Description + "~" + ICDRevision.ToString())
                    C1Treatment.SetData(nRow, COL_DX8_SNOMEDCODE, oICD9.SubItems(0).Code)
                    C1Treatment.SetData(nRow, COL_DX8_SNOMEDDESC, oICD9.SubItems(0).Description)
                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    C1Treatment.SetData(nRow, COL_DX8_SnoMedOneToOne, oICD9.SubItems(0).IsSnoMedOneToOneMapping)
                    MoveNext(COL_DX8_CODE)
                End If
                RaiseEvent ICD9_Inserted(oICD9)
            End If

            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function AddModifier(ByVal oModifier As gloGeneralItem.gloItem, ByVal nRow As Integer, ByVal nCol As Integer) As Boolean
        Try
            C1Treatment.SetData(nRow, nCol, "")
            '' CHECK WHETHER SAME MODIFIER PRESENT ''
            '' IF NOT THEN ONLY ADD NEW ''
            If oModifier.Code = C1Treatment.GetData(nRow, COL_MOD1_CODE) Or _
                oModifier.Code = C1Treatment.GetData(nRow, COL_MOD2_CODE) Or _
                oModifier.Code = C1Treatment.GetData(nRow, COL_MOD3_CODE) Or _
                oModifier.Code = C1Treatment.GetData(nRow, COL_MOD4_CODE) Then
                If _TreatmentFilling = False Then MessageBox.Show("Duplicate Modifier is not allowed.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            If C1Treatment.GetData(nRow, COL_MOD1_CODE) = "" Or COL_MOD1_CODE = nCol Then
                C1Treatment.SetData(nRow, COL_MOD1_ID, oModifier.ID)
                C1Treatment.SetData(nRow, COL_MOD1_CODE, oModifier.Code)
                C1Treatment.SetData(nRow, COL_MOD1_DESC, oModifier.Description)
                MoveNext(COL_MOD1_CODE)
            ElseIf C1Treatment.GetData(nRow, COL_MOD2_CODE) = "" Or COL_MOD2_CODE = nCol Then
                C1Treatment.SetData(nRow, COL_MOD2_ID, oModifier.ID)
                C1Treatment.SetData(nRow, COL_MOD2_CODE, oModifier.Code)
                C1Treatment.SetData(nRow, COL_MOD2_DESC, oModifier.Description)

                If _ModifierCount = enumModifierCount.Show_4_Modifier Then
                    MoveNext(COL_MOD2_CODE)
                Else
                    MoveNext(COL_MOD4_CODE)
                End If

            ElseIf C1Treatment.GetData(nRow, COL_MOD3_CODE) = "" Or COL_MOD3_CODE = nCol Then
                C1Treatment.SetData(nRow, COL_MOD3_ID, oModifier.ID)
                C1Treatment.SetData(nRow, COL_MOD3_CODE, oModifier.Code)
                C1Treatment.SetData(nRow, COL_MOD3_DESC, oModifier.Description)
                MoveNext(COL_MOD3_CODE)

                '' IF SHOW 2 DX SETTING IS ON AND MODIFIER OVERFLOWS THEN REDESIGN IT FOR MOD4''
                If _ModifierCount = enumModifierCount.Show_2_MOdifier Then
                    _ModifierCount = enumModifierCount.Show_4_Modifier
                    ReDesignGrid()
                End If

            ElseIf C1Treatment.GetData(nRow, COL_MOD4_CODE) = "" Or COL_MOD4_CODE = nCol Then
                C1Treatment.SetData(nRow, COL_MOD4_ID, oModifier.ID)
                C1Treatment.SetData(nRow, COL_MOD4_CODE, oModifier.Code)
                C1Treatment.SetData(nRow, COL_MOD4_DESC, oModifier.Description)
                MoveNext(COL_MOD4_CODE)
            End If
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub MoveNext(ByVal nCurrenColumn As Integer)
        _ItemInserted = True
        Dim _NextColumn As Integer = 0
        Select Case nCurrenColumn
            Case COL_CPT_CODE
                _NextColumn = COL_DX1_CODE
            Case COL_DX1_CODE
                _NextColumn = COL_DX2_CODE
            Case COL_DX2_CODE
                _NextColumn = COL_DX3_CODE
            Case COL_DX3_CODE
                _NextColumn = COL_DX4_CODE
            Case COL_DX4_CODE
                _NextColumn = COL_DX5_CODE
            Case COL_DX5_CODE
                _NextColumn = COL_DX6_CODE
            Case COL_DX6_CODE
                _NextColumn = COL_DX7_CODE
            Case COL_DX7_CODE
                _NextColumn = COL_DX8_CODE
            Case COL_DX8_CODE
                _NextColumn = COL_MOD1_CODE
            Case COL_MOD1_CODE
                _NextColumn = COL_MOD2_CODE
            Case COL_MOD2_CODE
                _NextColumn = COL_MOD3_CODE
            Case COL_MOD3_CODE
                _NextColumn = COL_MOD4_CODE
            Case COL_MOD4_CODE
                If IsExamPTBillingEnabled Then
                    _NextColumn = COL_PT_TIMEDTHERAPY
                Else
                    _NextColumn = COL_UNIT
                End If
            Case COL_PT_TIMEDTHERAPY
                _NextColumn = COL_PT_UNTIMEDTHERAPY
            Case COL_PT_UNTIMEDTHERAPY
                _NextColumn = COL_UNIT
        End Select

        If _NextColumn > 0 Then
            C1Treatment.Select(C1Treatment.RowSel, _NextColumn)
            C1Treatment.ColSel = _NextColumn
        End If

        _ItemInserted = False
    End Sub

    Private Sub RearrangeAfterDelete(ByVal nRow As Integer, ByVal nCol As Integer)
        Try
            Select Case nCol
                Case COL_DX1_CODE
                    If C1Treatment.GetData(nRow, COL_DX2_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX1_CODE, C1Treatment.GetData(nRow, COL_DX2_CODE))
                        C1Treatment.SetData(nRow, COL_DX1_DESC, C1Treatment.GetData(nRow, COL_DX2_DESC))
                        C1Treatment.SetData(nRow, COL_DX1_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX1_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX1_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX2_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX2_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX2_CODE)
                    End If

                Case COL_DX2_CODE
                    If C1Treatment.GetData(nRow, COL_DX3_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX2_CODE, C1Treatment.GetData(nRow, COL_DX3_CODE))
                        C1Treatment.SetData(nRow, COL_DX2_DESC, C1Treatment.GetData(nRow, COL_DX3_DESC))
                        C1Treatment.SetData(nRow, COL_DX2_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX2_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX2_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX3_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX3_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX3_CODE)
                    End If

                Case COL_DX3_CODE
                    If C1Treatment.GetData(nRow, COL_DX4_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX3_CODE, C1Treatment.GetData(nRow, COL_DX4_CODE))
                        C1Treatment.SetData(nRow, COL_DX3_DESC, C1Treatment.GetData(nRow, COL_DX4_DESC))
                        C1Treatment.SetData(nRow, COL_DX3_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX3_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX3_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX4_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX4_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX4_CODE)
                    End If

                Case COL_DX4_CODE
                    If C1Treatment.GetData(nRow, COL_DX5_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX4_CODE, C1Treatment.GetData(nRow, COL_DX5_CODE))
                        C1Treatment.SetData(nRow, COL_DX4_DESC, C1Treatment.GetData(nRow, COL_DX5_DESC))
                        C1Treatment.SetData(nRow, COL_DX4_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX4_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX4_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX5_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX5_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX5_CODE)
                    End If

                Case COL_DX5_CODE
                    If C1Treatment.GetData(nRow, COL_DX6_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX5_CODE, C1Treatment.GetData(nRow, COL_DX6_CODE))
                        C1Treatment.SetData(nRow, COL_DX5_DESC, C1Treatment.GetData(nRow, COL_DX6_DESC))
                        C1Treatment.SetData(nRow, COL_DX5_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX5_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX5_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX6_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX6_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX6_CODE)
                    End If

                Case COL_DX6_CODE
                    If C1Treatment.GetData(nRow, COL_DX7_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX6_CODE, C1Treatment.GetData(nRow, COL_DX7_CODE))
                        C1Treatment.SetData(nRow, COL_DX6_DESC, C1Treatment.GetData(nRow, COL_DX7_DESC))
                        C1Treatment.SetData(nRow, COL_DX6_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX6_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX6_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX7_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX7_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX7_CODE)
                    End If

                Case COL_DX7_CODE
                    If C1Treatment.GetData(nRow, COL_DX8_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_DX7_CODE, C1Treatment.GetData(nRow, COL_DX8_CODE))
                        C1Treatment.SetData(nRow, COL_DX7_DESC, C1Treatment.GetData(nRow, COL_DX8_DESC))
                        C1Treatment.SetData(nRow, COL_DX7_SNOMEDCODE, "")
                        C1Treatment.SetData(nRow, COL_DX7_SNOMEDDESC, "")
                        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                        C1Treatment.SetData(nRow, COL_DX7_SnoMedOneToOne, False)

                        C1Treatment.SetData(nRow, COL_DX8_CODE, "")
                        C1Treatment.SetData(nRow, COL_DX8_DESC, "")
                        RearrangeAfterDelete(nRow, COL_DX8_CODE)
                    End If

                Case COL_MOD1_CODE
                    If C1Treatment.GetData(nRow, COL_MOD2_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_MOD1_CODE, C1Treatment.GetData(nRow, COL_MOD2_CODE))
                        C1Treatment.SetData(nRow, COL_MOD1_DESC, C1Treatment.GetData(nRow, COL_MOD2_DESC))

                        C1Treatment.SetData(nRow, COL_MOD2_CODE, "")
                        C1Treatment.SetData(nRow, COL_MOD2_DESC, "")
                        RearrangeAfterDelete(nRow, COL_MOD2_CODE)
                    End If

                Case COL_MOD2_CODE
                    If C1Treatment.GetData(nRow, COL_MOD3_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_MOD2_CODE, C1Treatment.GetData(nRow, COL_MOD3_CODE))
                        C1Treatment.SetData(nRow, COL_MOD2_DESC, C1Treatment.GetData(nRow, COL_MOD3_DESC))

                        C1Treatment.SetData(nRow, COL_MOD3_CODE, "")
                        C1Treatment.SetData(nRow, COL_MOD3_DESC, "")
                        RearrangeAfterDelete(nRow, COL_MOD3_CODE)
                    End If

                Case COL_MOD3_CODE
                    If C1Treatment.GetData(nRow, COL_MOD4_CODE) <> "" Then
                        C1Treatment.SetData(nRow, COL_MOD3_CODE, C1Treatment.GetData(nRow, COL_MOD4_CODE))
                        C1Treatment.SetData(nRow, COL_MOD3_DESC, C1Treatment.GetData(nRow, COL_MOD4_DESC))

                        C1Treatment.SetData(nRow, COL_MOD4_CODE, "")
                        C1Treatment.SetData(nRow, COL_MOD4_DESC, "")
                        RearrangeAfterDelete(nRow, COL_MOD4_CODE)
                    End If

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetModifier(ByVal nRow As Integer, ByVal nModifierColumn As Integer, ByVal nModifierCount As Integer) As gloEMRGeneralLibrary.gloGeneral.myList
        Dim oList As New gloEMRGeneralLibrary.gloGeneral.myList
        Try
            oList.HistoryCategory = C1Treatment.GetData(nRow, COL_CPT_CODE)
            oList.HistoryItem = C1Treatment.GetData(nRow, COL_CPT_DESC)
            oList.Value = C1Treatment.GetData(nRow, nModifierColumn)
            oList.ParameterName = C1Treatment.GetData(nRow, nModifierColumn + 1)
            oList.TemplateResult = C1Treatment.GetData(nRow, COL_UNIT)
            oList.TimedTherapy = C1Treatment.GetData(nRow, COL_PT_TIMEDTHERAPY)
            oList.UnTimedTherapy = C1Treatment.GetData(nRow, COL_PT_UNTIMEDTHERAPY)
            oList.ICD9No = nModifierCount
            oList.nICDRevision = ICDRevision
            Return oList
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Dim nColumnAdded As Integer = 0

    Private Function GetICD9(ByVal nRow As Integer, ByVal nICD9Column As Integer, ByVal nICD9Count As Integer) As gloEMRGeneralLibrary.gloGeneral.myList

        Dim oList As New gloEMRGeneralLibrary.gloGeneral.myList

        Try
            oList.Code = C1Treatment.GetData(nRow, nICD9Column)

            Dim sDesc As String = C1Treatment.GetData(nRow, nICD9Column + 1)
            If Not IsNothing(sDesc) Then
                Dim sDescSplit() As String = sDesc.Split("~")
                If sDescSplit.Length > 1 Then
                    oList.Description = sDescSplit(0)
                    oList.nICDRevision = sDescSplit(1)
                Else
                    oList.Description = C1Treatment.GetData(nRow, nICD9Column + 1)
                End If
            Else
                oList.Description = C1Treatment.GetData(nRow, nICD9Column + 1)
            End If

            oList.SnomedID = C1Treatment.GetData(nRow, nICD9Column + (36 + 2))
            oList.SnomedDesc = C1Treatment.GetData(nRow, nICD9Column + (37 + 2))
            '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
            oList.SnoMedOneToOneMapping = C1Treatment.GetData(nRow, nICD9Column + (38 + 2))

            '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
            'nColumnAdded = nColumnAdded + 1
            oList.HistoryCategory = C1Treatment.GetData(nRow, COL_CPT_CODE)
            oList.HistoryItem = C1Treatment.GetData(nRow, COL_CPT_DESC)
            oList.Value = ""
            oList.ParameterName = ""
            oList.TemplateResult = C1Treatment.GetData(nRow, COL_UNIT)
            oList.TimedTherapy = C1Treatment.GetData(nRow, COL_PT_TIMEDTHERAPY)
            oList.UnTimedTherapy = C1Treatment.GetData(nRow, COL_PT_UNTIMEDTHERAPY)
            oList.ICD9No = nICD9Count

            Return (oList)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function GetCPT(ByVal nRow As Integer, ByVal nCPTCount As Integer) As gloEMRGeneralLibrary.gloGeneral.myList
        Dim oList As New gloEMRGeneralLibrary.gloGeneral.myList
        Try
            oList.Code = ""
            oList.Description = ""
            oList.HistoryCategory = C1Treatment.GetData(nRow, COL_CPT_CODE)
            oList.HistoryItem = C1Treatment.GetData(nRow, COL_CPT_DESC)
            oList.Value = ""
            oList.ParameterName = ""
            oList.TemplateResult = C1Treatment.GetData(nRow, COL_UNIT)
            oList.TimedTherapy = C1Treatment.GetData(nRow, COL_PT_TIMEDTHERAPY)
            oList.UnTimedTherapy = C1Treatment.GetData(nRow, COL_PT_UNTIMEDTHERAPY)
            oList.ReasonConceptID = C1Treatment.GetData(nRow, Col_PT_ReasonConceptID)
            oList.ReasonConceptDesc = C1Treatment.GetData(nRow, Col_PT_ReasonConceptDesc)
            oList.ICD9No = nCPTCount
            oList.nICDRevision = ICDRevision
            Return oList
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub AddRow()
        C1Treatment.Rows.Add()
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_LINE_NO, Convert.ToString(C1Treatment.Rows.Count - 1))
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_DOS, _DOS.ToString)
        C1Treatment.SetData(C1Treatment.Rows.Count - 1, COL_UNIT, "1")
    End Sub

    Private Sub RemoveICD9(ByVal ICD9Code As String, ByVal ICD9Description As String)
        Try
            If isICD9Present(ICD9Code) = False Then
                Dim oICD9s As New gloGeneralItem.gloItems
                oICD9s.Add(0, ICD9Code, ICD9Description)
                'SLR: check whether oICD9s is freeed inside the event?
                RaiseEvent ICD9_Removed(oICD9s)
            End If
        Catch
        End Try
    End Sub

    Private Function isICD9Present(ByVal ICD9Code As String) As Boolean
        Try
            For iRow As Integer = 1 To C1Treatment.Rows.Count - 1
                If C1Treatment.GetData(iRow, COL_DX1_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX1_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX2_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX3_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX4_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX5_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX6_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX7_CODE) = ICD9Code Or _
                C1Treatment.GetData(iRow, COL_DX8_CODE) = ICD9Code Then
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ReDesignGrid()
        Try
            If _ICD9count = enumIC9Count.Show_8_ICD9 Then
                C1Treatment.Cols(COL_DX1_CODE).Width = 60
                C1Treatment.Cols(COL_DX2_CODE).Width = 60
                C1Treatment.Cols(COL_DX3_CODE).Width = 60
                C1Treatment.Cols(COL_DX4_CODE).Width = 60
                C1Treatment.Cols(COL_DX5_CODE).Width = 60
                C1Treatment.Cols(COL_DX6_CODE).Width = 60
                C1Treatment.Cols(COL_DX7_CODE).Width = 60
                C1Treatment.Cols(COL_DX8_CODE).Width = 60

                C1Treatment.Cols(COL_DX5_CODE).Visible = True
                C1Treatment.Cols(COL_DX6_CODE).Visible = True
                C1Treatment.Cols(COL_DX7_CODE).Visible = True
                C1Treatment.Cols(COL_DX8_CODE).Visible = True
            Else
                C1Treatment.Cols(COL_DX1_CODE).Width = 80
                C1Treatment.Cols(COL_DX2_CODE).Width = 80
                C1Treatment.Cols(COL_DX3_CODE).Width = 80
                C1Treatment.Cols(COL_DX4_CODE).Width = 80
                C1Treatment.Cols(COL_DX5_CODE).Width = 80
                C1Treatment.Cols(COL_DX6_CODE).Width = 80
                C1Treatment.Cols(COL_DX7_CODE).Width = 80
                C1Treatment.Cols(COL_DX8_CODE).Width = 80

                C1Treatment.Cols(COL_DX5_CODE).Visible = False
                C1Treatment.Cols(COL_DX6_CODE).Visible = False
                C1Treatment.Cols(COL_DX7_CODE).Visible = False
                C1Treatment.Cols(COL_DX8_CODE).Visible = False
            End If

            If _ModifierCount = enumModifierCount.Show_4_Modifier Then
                C1Treatment.Cols(COL_MOD1_CODE).Width = 60
                C1Treatment.Cols(COL_MOD2_CODE).Width = 60
                C1Treatment.Cols(COL_MOD3_CODE).Width = 60
                C1Treatment.Cols(COL_MOD4_CODE).Width = 60

                C1Treatment.Cols(COL_MOD3_CODE).Visible = True
                C1Treatment.Cols(COL_MOD4_CODE).Visible = True
            Else
                C1Treatment.Cols(COL_MOD1_CODE).Width = 80
                C1Treatment.Cols(COL_MOD2_CODE).Width = 80
                C1Treatment.Cols(COL_MOD3_CODE).Width = 80
                C1Treatment.Cols(COL_MOD4_CODE).Width = 80

                C1Treatment.Cols(COL_MOD3_CODE).Visible = False
                C1Treatment.Cols(COL_MOD4_CODE).Visible = False
            End If
        Catch
        End Try
    End Sub
    Public Function FormatNumber(ByVal Number As Decimal) As Decimal
        Dim _result As [Decimal] = Number
        Try
            Dim no As [String]() = _result.ToString().Split("."c)
            If no.GetUpperBound(0) > 0 Then
                If no(1).ToString().Length > 4 Then
                    no(1) = no(1).Substring(0, 4)
                End If
                _result = Convert.ToDecimal(no(0) + "." + no(1))
            End If
            _result = Convert.ToDecimal(_result.ToString("0.####"))
        Catch
            _result = Number
        End Try
        Return _result
    End Function

    Public Function ValidateDiagnosisUnit() As Boolean

        Dim i As Integer
        Dim intUnits As System.Double
        Dim _Node As C1.Win.C1FlexGrid.Node


        Try

            '07-Jan-2016 Aniket; Resolving Bug #92409: gloEMR: Dx-CPT: Application gives exception
            'C1Treatment.Select(0, COL_UNIT, True)
            If IsNothing(C1Treatment) = False Then

                If IsNothing(C1Treatment.Rows) = False Then

                    For i = 1 To C1Treatment.Rows.Count - 1

                        _Node = C1Treatment.Rows(i).Node
                        intUnits = C1Treatment.GetData(i, COL_UNIT)

                        If (intUnits > 999.9999) Then
                            C1Treatment.Select(i, COL_UNIT, True)
                            Return False
                        End If

                    Next

                End If
            End If

            Return True

        Catch ex As Exception
            Return True
        End Try

    End Function

#End Region

    Private Sub C1Treatment_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Treatment.MouseEnterCell
        Try
            'Dim oHit As C1.Win.C1FlexGrid.HitTestInfo
            '= C1Treatment.HitTest(Windows.Forms.Cursor.Position.X, Windows.Forms.Cursor.Position.Y)
            If C1Treatment.ColSel >= COL_CPT_CODE And e.Col <= COL_MOD4_CODE And e.Row > 0 Then
                Dim _Desc As String = C1Treatment.GetData(e.Row, e.Col) ''Col +1 chnage to Col for bugid 78709
                If Convert.ToString(_Desc).Trim <> "" Then
                    oToolTip.SetToolTip(C1Treatment, _Desc.Trim)
                Else
                    oToolTip.RemoveAll()
                End If

            Else
                'oToolTip.RemoveAll()
                If (e.Col = COL_UNIT) Then
                    If (e.Row > 0) Then
                        If (C1Treatment.GetData(e.Row, e.Col).ToString().Length > 8) Then
                            oToolTip.SetToolTip(C1Treatment, C1Treatment.GetData(e.Row, e.Col).ToString())
                        End If
                    End If
                Else

                    oToolTip.RemoveAll()
                End If
            End If
        Catch
            oToolTip.RemoveAll()
        End Try
    End Sub

    Private Sub C1Treatment_ValidateEdit(sender As Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1Treatment.ValidateEdit
        If (e.Col = COL_PT_TIMEDTHERAPY Or e.Col = COL_PT_UNTIMEDTHERAPY) Then
            Try
                If Not String.IsNullOrEmpty(C1Treatment.Editor.Text) Then
                    Dim val As Integer = Integer.Parse(C1Treatment.Editor.Text)
                    If (val > 0 And val < 1000) Then
                        Return
                    Else
                        MessageBox.Show("Timed/Untimed Therapy should be less than 999 and greater than 0.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RemoveHandler C1Treatment.ValidateEdit, AddressOf C1Treatment_ValidateEdit
                        C1Treatment.Editor.Text = Nothing
                        AddHandler C1Treatment.ValidateEdit, AddressOf C1Treatment_ValidateEdit
                        e.Cancel = True
                    End If
                End If
            Catch
                MessageBox.Show("Timed/Untimed Therapy should be less than 999 and greater than 0.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RemoveHandler C1Treatment.ValidateEdit, AddressOf C1Treatment_ValidateEdit
                C1Treatment.Editor.Text = Nothing
                AddHandler C1Treatment.ValidateEdit, AddressOf C1Treatment_ValidateEdit
                e.Cancel = True
            End Try
        End If
    End Sub


    Public Sub SetRefusalReason(ByVal rCode As String, ByVal rDesc As String, ByVal selectedRow As Int16)
        Try
            With C1Treatment
                If .Row > 0 Then
                    .SetData(selectedRow, Col_PT_ReasonConceptID, rCode)
                    .SetData(selectedRow, Col_PT_ReasonConceptDesc, rDesc)
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

End Class
