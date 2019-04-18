
Imports C1.Win.C1FlexGrid

Public Class frmBaseForm

#Region "Variable Declaration"
    Public _isChanges As Boolean 'FOR TRACKING IF ANY CHANGES IS DONE
    Public _isLoaded As Boolean ' DO NOT FIRE ANY HANDLER ON THE LOAD OF FORM
    Public _isClose As Boolean = False
    Public _isSaveClicked As Boolean = False 'TO CHECK IF WE CLICK ON SAVE BUTTON THEN DON'T CALL 
    Public _IsValidationFailed As Boolean = False
    Public _isHistory As Boolean = False
#End Region

    Event SaveFunction()

    Private Sub frmBaceForm1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim dlgResult As DialogResult
        Try

            If (_isChanges = True AndAlso _isSaveClicked = False AndAlso _isClose = False) And Not _isHistory Then

                dlgResult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If (dlgResult = Windows.Forms.DialogResult.Yes) Then
                    _IsValidationFailed = False
                    RaiseEvent SaveFunction()
                    If (_IsValidationFailed = True) Then
                        e.Cancel = True
                        _IsValidationFailed = False
                    Else
                        _isClose = True
                    End If

                ElseIf (dlgResult = Windows.Forms.DialogResult.No) Then
                    _isClose = True
                ElseIf (dlgResult = Windows.Forms.DialogResult.Cancel) Then
                    e.Cancel = True
                End If

            End If
            If (_isClose) Then
                For Each oControl As Control In Me.Controls
                    RemoveEvent(oControl)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmBaceForm1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Dim i As Int64

        'FUNCTION FOR ADDING HANDLER TO THE EVENT OF THE CONTROL
        For Each oControl As Control In Me.Controls
            BindEvent(oControl)
        Next

    End Sub
#Region "Control Change Event"

    Public Function BindEvent(ByVal cntrl As Control)

        Dim c As Control
        Try

            If cntrl.Controls.Count > 0 Then

                For Each c In cntrl.Controls
                    BindEvent(c)
                Next

            Else

                'FOR TEXT CHANGED EVENT
                If TypeOf (cntrl) Is TextBox Then
                    cntrl = DirectCast(cntrl, TextBox)
                    AddHandler cntrl.TextChanged, AddressOf Text_Changed 'ZIP Master

                    'FOR SELECTED INDEX CHANGED EVENT
                ElseIf TypeOf (cntrl) Is ComboBox Then
                    Dim tempcnt As ComboBox
                    tempcnt = DirectCast(cntrl, ComboBox)
                    AddHandler tempcnt.SelectedIndexChanged, AddressOf Text_Changed

                    'FOR TREE VIEW DOUBLE CLICK EVENT (FOR ADDING ADDING BY DOUBLE CLICKING )
                ElseIf TypeOf (cntrl) Is TreeView Then
                    Dim tempcnt As TreeView
                    tempcnt = DirectCast(cntrl, TreeView)
                    AddHandler tempcnt.DoubleClick, AddressOf Text_Changed

                    'ElseIf TypeOf (cntrl) Is ListBox Then
                    '    Dim tempcnt As ListBox
                    '    tempcnt = DirectCast(cntrl, ListBox)

                    'FOR C1 KEYPRESSEDIT OR START EDIT EVENT
                ElseIf TypeOf (cntrl) Is C1FlexGrid Then
                    Dim tempcnt As C1FlexGrid
                    tempcnt = DirectCast(cntrl, C1FlexGrid)
                    AddHandler tempcnt.KeyPressEdit, AddressOf Texta_Changed ' CALL FOR THE EVENT TO MODIFY THE CELL OF C1 I.E. ENTER ANY TEXT INTO CELL OF C1
                    AddHandler tempcnt.StartEdit, AddressOf Textb_Changed ' IN FLOWSHEET CLICK FOR THE COLUMN CONTAINING DATE FORMAT

                    'FOR RADIO BUTTON CHECKED CHANGES
                ElseIf TypeOf (cntrl) Is RadioButton Then
                    Dim tempcnt As RadioButton
                    tempcnt = DirectCast(cntrl, RadioButton)
                    AddHandler tempcnt.CheckedChanged, AddressOf Text_Changed

                    'FOR DATE TIME PICKER VALUE CHANGED EVENT
                ElseIf TypeOf (cntrl) Is DateTimePicker Then
                    Dim tempcnt As DateTimePicker
                    tempcnt = DirectCast(cntrl, DateTimePicker)
                    AddHandler tempcnt.ValueChanged, AddressOf Text_Changed

                    'FOR CHECK BOX CHECKED CHANGED EVENT
                ElseIf TypeOf (cntrl) Is CheckBox Then
                    Dim tempcnt As CheckBox
                    tempcnt = DirectCast(cntrl, CheckBox)
                    AddHandler tempcnt.CheckedChanged, AddressOf Text_Changed

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
    Public Function RemoveEvent(ByVal cntrl As Control)

        Dim c As Control
        Try

            If cntrl.Controls.Count > 0 Then

                For Each c In cntrl.Controls
                    RemoveEvent(c)
                Next

            Else
                Dim myAddressEvent As System.EventHandler = AddressOf Text_Changed
                Dim myC1AddressEvent As C1.Win.C1FlexGrid.KeyPressEditEventHandler = AddressOf Texta_Changed
                Dim myC2AddressEvent As C1.Win.C1FlexGrid.RowColEventHandler = AddressOf Textb_Changed
                'FOR TEXT CHANGED EVENT
                If TypeOf (cntrl) Is TextBox Then
                    cntrl = DirectCast(cntrl, TextBox)
                    Try
                        RemoveHandler cntrl.TextChanged, myAddressEvent 'ZIP Master
                    Catch ex As Exception

                    End Try


                    'FOR SELECTED INDEX CHANGED EVENT
                ElseIf TypeOf (cntrl) Is ComboBox Then
                    Dim tempcnt As ComboBox
                    tempcnt = DirectCast(cntrl, ComboBox)
                    Try
                        RemoveHandler tempcnt.SelectedIndexChanged, myAddressEvent

                    Catch ex As Exception

                    End Try
               
                    'FOR TREE VIEW DOUBLE CLICK EVENT (FOR ADDING ADDING BY DOUBLE CLICKING )
                ElseIf TypeOf (cntrl) Is TreeView Then
                    Dim tempcnt As TreeView
                    tempcnt = DirectCast(cntrl, TreeView)
                    Try
                        RemoveHandler tempcnt.DoubleClick, myAddressEvent
                    Catch ex As Exception

                    End Try


                    'ElseIf TypeOf (cntrl) Is ListBox Then
                    '    Dim tempcnt As ListBox
                    '    tempcnt = DirectCast(cntrl, ListBox)

                    'FOR C1 KEYPRESSEDIT OR START EDIT EVENT
                ElseIf TypeOf (cntrl) Is C1FlexGrid Then
                    Dim tempcnt As C1FlexGrid
                    tempcnt = DirectCast(cntrl, C1FlexGrid)
                    Try
                        RemoveHandler tempcnt.KeyPressEdit, myC1AddressEvent ' CALL FOR THE EVENT TO MODIFY THE CELL OF C1 I.E. ENTER ANY TEXT INTO CELL OF C1
                        RemoveHandler tempcnt.StartEdit, myC2AddressEvent ' IN FLOWSHEET CLICK FOR THE COLUMN CONTAINING DATE FORMAT

                    Catch ex As Exception

                    End Try
                  
                    'FOR RADIO BUTTON CHECKED CHANGES
                ElseIf TypeOf (cntrl) Is RadioButton Then
                    Dim tempcnt As RadioButton
                    tempcnt = DirectCast(cntrl, RadioButton)
                    Try
                        RemoveHandler tempcnt.CheckedChanged, myAddressEvent
                    Catch ex As Exception

                    End Try


                    'FOR DATE TIME PICKER VALUE CHANGED EVENT
                ElseIf TypeOf (cntrl) Is DateTimePicker Then
                    Dim tempcnt As DateTimePicker
                    tempcnt = DirectCast(cntrl, DateTimePicker)
                    Try
                        RemoveHandler tempcnt.ValueChanged, myAddressEvent
                    Catch ex As Exception

                    End Try


                    'FOR CHECK BOX CHECKED CHANGED EVENT
                ElseIf TypeOf (cntrl) Is CheckBox Then
                    Dim tempcnt As CheckBox
                    tempcnt = DirectCast(cntrl, CheckBox)
                    Try
                        RemoveHandler tempcnt.CheckedChanged, myAddressEvent
                    Catch ex As Exception

                    End Try


                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
    Public Sub Text_Changed()
        'SET FLAG TO CHECK WHETHER ANY CHANGES DONE IN ANY OF THE CONTROL
        If _isLoaded = True Then
            _isChanges = True
        End If

    End Sub
    Public Sub Texta_Changed()
        'SET FLAG TO CHECK WHETHER ANY CHANGES DONE IN ANY OF THE CONTROL
        If _isLoaded = True Then
            _isChanges = True
        End If

    End Sub
    Public Sub Textb_Changed()
        'SET FLAG TO CHECK WHETHER ANY CHANGES DONE IN ANY OF THE CONTROL
        If _isLoaded = True Then
            _isChanges = True
        End If

    End Sub
#End Region
End Class