
Public Class gloToolStrip

#Region " Enumerations "
    Public Enum enumButtonNameType
        ShowToolTipText
        ShowTagText
    End Enum
#End Region

#Region " Private Variables "
    Private _UserID As Int64
    Private _ModuleName As String
    Private _FinishTemplate As Boolean
    Private _DefaultToolStrip As New ArrayList
    Private _SeparatorInButtons As Boolean
    Private _ButtonNameType As enumButtonNameType = enumButtonNameType.ShowToolTipText
    Private _ButtonsToHide As New ArrayList
    Private oContextMenu As ContextMenuStrip = Nothing
#End Region

#Region " Public Properties "
    Public Property ConnectionString() As String
        Get
            Return sConnectionString
        End Get
        Set(ByVal value As String)
            sConnectionString = value
        End Set
    End Property

    Public Property ModuleName() As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property
    Public Property FinishTemplate() As Boolean
        Get
            Return _FinishTemplate
        End Get
        Set(ByVal value As Boolean)
            _FinishTemplate = value
        End Set
    End Property

    Public Property UserID() As Int64
        Get
            Return _UserID
        End Get
        Set(ByVal value As Int64)
            _UserID = value
        End Set
    End Property

    Public Property AddSeparatorsBetweenEachButton() As Boolean
        Get
            Return _SeparatorInButtons
        End Get
        Set(ByVal value As Boolean)
            _SeparatorInButtons = value
        End Set
    End Property

    Public Property CustomizeButtonNameType() As enumButtonNameType
        Get
            Return _ButtonNameType
        End Get
        Set(ByVal value As enumButtonNameType)
            _ButtonNameType = value
        End Set
    End Property

    Public Property ButtonsToHide() As ArrayList
        Get
            Return _ButtonsToHide
        End Get
        Set(ByVal value As ArrayList)
            _ButtonsToHide = value
        End Set
    End Property
#End Region

#Region " ToolStrip Loading "

    Private Sub gloToolStrip_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed

        Try
            'SLR: 5/23/2015 Bug #83549: OB vitals : Application shows an exception when user open new exam: 
            'SLR: Removehanlder should be inside nothing=false check
            If IsNothing(oContextMenu) = False Then
                Try
                    RemoveHandler oContextMenu.Items(0).Click, AddressOf On_Customize_Click
                    RemoveHandler oContextMenu.Opened, AddressOf oContextMenu_Opened
                Catch exhandler As Exception

                End Try

                oContextMenu.Dispose()
                oContextMenu = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub gloToolStrip_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        InitializeContextMenu()
    End Sub

#End Region

#Region " Private Methods "
    Private Sub On_Customize_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oToolCustomize As New frmToolCustomize(Me, _DefaultToolStrip)
            oToolCustomize.UserID = _UserID
            oToolCustomize.ModuleName = _ModuleName
            oToolCustomize.ShowSeparator = Not _SeparatorInButtons
            oToolCustomize.ToolStripHeight = Me.Height
            oToolCustomize.ButtonNameType = _ButtonNameType
            oToolCustomize.FinishTemplate = _FinishTemplate

            '' GLO2011-0013707 : 0595 - message errors
            '' Add buttons to ButtonsToHide list for hide from customization screen.
            '' ---- Start ----
            If _ModuleName = "gloWordToolStrip_Messages" Then
                If Not _ButtonsToHide.Contains(Me.Items("Save").Name) Then
                    _ButtonsToHide.Add(Me.Items("Save").Name)
                End If
                If Not _ButtonsToHide.Contains(Me.Items("Save & Close").Name) Then
                    _ButtonsToHide.Add(Me.Items("Save & Close").Name)
                End If
                If Not _ButtonsToHide.Contains(Me.Items("Save & Finish").Name) Then
                    _ButtonsToHide.Add(Me.Items("Save & Finish").Name)
                End If
                If Not _ButtonsToHide.Contains(Me.Items("Close").Name) Then
                    _ButtonsToHide.Add(Me.Items("Close").Name)
                End If
            End If


            '' ---- End ----Insert Sign
            If FinishTemplate = True Then
                If _ModuleName = "gloWordToolStrip_LabOrder" Then
                    If Not _ButtonsToHide.Contains(Me.Items("Insert File").Name) Then
                        _ButtonsToHide.Add(Me.Items("Insert File").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Scan Documents").Name) Then
                        _ButtonsToHide.Add(Me.Items("Scan Documents").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Undo").Name) Then
                        _ButtonsToHide.Add(Me.Items("Undo").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Redo").Name) Then
                        _ButtonsToHide.Add(Me.Items("Redo").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Insert Associated Provider Signature").Name) Then
                        _ButtonsToHide.Add(Me.Items("Insert Associated Provider Signature").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Insert Sign").Name) Then
                        _ButtonsToHide.Add(Me.Items("Insert Sign").Name)
                    End If
                    If Not _ButtonsToHide.Contains(Me.Items("Save").Name) Then
                        _ButtonsToHide.Add(Me.Items("Save").Name)
                    End If
                    'If Not _ButtonsToHide.Contains(Me.Items("Save & Close").Name) Then
                    '    _ButtonsToHide.Add(Me.Items("Save & Close").Name)
                    'End If
                    'If Not _ButtonsToHide.Contains(Me.Items("Save & Finish").Name) Then
                    '    _ButtonsToHide.Add(Me.Items("Save & Finish").Name)
                    'End If
                    'If Not _ButtonsToHide.Contains(Me.Items("Close").Name) Then
                    '    _ButtonsToHide.Add(Me.Items("Close").Name)
                    'End If
                    'If Not _ButtonsToHide.Contains(Me.Items("finish").Name) Then
                    '    _ButtonsToHide.Add(Me.Items("finish").Name)
                    'End If
                End If
           
            End If
            '' ---- End ----



            oToolCustomize.ButtonsToHide = _ButtonsToHide
            If oToolCustomize.ShowDialog(IIf(IsNothing(oToolCustomize.Parent), Me, oToolCustomize.Parent)) = DialogResult.OK Then
                '' GLO2011-0013707 : 0595 - message errors
                '' Remove buttons from ButtonsToHide list for display it on toolbars.
                '' ------ Start ----
                If _ModuleName = "gloWordToolStrip_Messages" Then
                    _ButtonsToHide.Remove(Me.Items("Save").Name)
                    _ButtonsToHide.Remove(Me.Items("Save & Close").Name)
                    _ButtonsToHide.Remove(Me.Items("Save & Finish").Name)
                    _ButtonsToHide.Remove(Me.Items("Close").Name)
                End If
                '' ---- End ----
                If _FinishTemplate = False Then
                    If _ModuleName = "gloWordToolStrip_LabOrder" Then
                        _ButtonsToHide.Remove(Me.Items("Finish").Name)
                        _ButtonsToHide.Remove(Me.Items("Save").Name)
                        _ButtonsToHide.Remove(Me.Items("Save & Close").Name)
                        _ButtonsToHide.Remove(Me.Items("Insert Sign").Name)
                        _ButtonsToHide.Remove(Me.Items("Undo").Name)
                        _ButtonsToHide.Remove(Me.Items("Redo").Name)
                        _ButtonsToHide.Remove(Me.Items("Insert File").Name)
                        _ButtonsToHide.Remove(Me.Items("Scan Documents").Name)
                        _ButtonsToHide.Remove(Me.Items("Insert Associated Provider Signature").Name)

                    End If
               
                End If



                CustomizeToolStrip(oToolCustomize.SelectedButtons)
            End If
            oToolCustomize.Dispose()
            oToolCustomize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetDefaultToolStrip()
        Try
            For i As Integer = 0 To Me.Items.Count - 1

                If Me.Items(i).Tag IsNot Nothing Then

                    If Me.Items(i).Tag = "" Then
                        Continue For
                    End If

                    If Me.Items(i).Visible = True Or Me.Items(i).IsOnOverflow = True Then
                        _DefaultToolStrip.Add(Me.Items(i).Name.Trim & ".Visible")
                    Else
                        _DefaultToolStrip.Add(Me.Items(i).Name.Trim & ".Invisible")
                    End If

                Else

                    If Me.Items(i).Visible = True Then
                        _DefaultToolStrip.Add("|")
                    End If

                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CustomizeToolStrip(Optional ByVal arrSelectedButtons As ArrayList = Nothing)
        Try
            '' FETCHING USER SETTING FROM DATABASE ''

            Dim _ButtonSetting As ArrayList
            Dim _ToolSeperator As ToolStripSeparator

            If arrSelectedButtons Is Nothing Then
                Dim oToolStrip As New clsToolStrip
                _ButtonSetting = oToolStrip.GetButtons(_UserID, _ModuleName)
                oToolStrip = Nothing
            Else
                _ButtonSetting = arrSelectedButtons
            End If


            If _ButtonSetting IsNot Nothing Then
                If _ButtonSetting.Count > 0 Then

                    '' HIDE ALL BUTTONS OF CURRENT TOOLSTRIP ''
                    For i As Integer = Me.Items.Count - 1 To 0 Step -1
                        If Me.Items(i).Tag <> Nothing Then
                            If Me.Items(i).Text.Trim() <> "Mic" Then
                                Me.Items(i).Visible = False
                            End If
                        Else
                            Me.Items.RemoveAt(i) '' REMOVE SEPERATOR (IF ANY) ''
                        End If
                    Next

                    '' SHOW SELECTED BUTTONS ONE BY ONE ''
                    '' BY SHIFTING BUTTON LOCATIONS ''
                    For i As Integer = 0 To _ButtonSetting.Count - 1

                        If _ButtonSetting(i) = "|" Then  '' PIPE CHARACTER INDICATES SEPARATOR ''
                            _ToolSeperator = New ToolStripSeparator
                            Me.Items.Add(_ToolSeperator)
                        ElseIf _ButtonSetting(i) <> "" Then
                            FindButton(_ButtonSetting(i))
                        End If

                    Next

                Else
                    If _SeparatorInButtons Then
                        For i As Integer = Me.Items.Count - 1 To 1 Step -1
                            _ToolSeperator = New ToolStripSeparator
                            Me.Items.Insert(i, _ToolSeperator)
                        Next
                    End If
                End If

                '' HIDE BUTTONS FROM ARRAYLIST ''
                If _ButtonsToHide IsNot Nothing Then
                    If _ButtonsToHide.Count > 0 Then
                        For i As Integer = 0 To Me.Items.Count - 1
                            If _ButtonsToHide.Contains(Me.Items(i).Name) Then
                                Me.Items(i).Visible = False
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FindButton(ByVal sButtonName As String)
        Try
            For i As Integer = 0 To Me.Items.Count - 1

                If _ButtonNameType = enumButtonNameType.ShowTagText Then
                    If Me.Items(i).Tag <> Nothing Then
                        If Me.Items(i).Tag.Trim = sButtonName Then
                            '' MOVING BUTTON ''
                            InsertButton(i)
                            Exit Sub
                            '' MOVING BUTTON ''
                        End If
                    End If

                ElseIf _ButtonNameType = enumButtonNameType.ShowToolTipText Then
                    If Me.Items(i).ToolTipText <> Nothing Then
                        If Me.Items(i).ToolTipText.Trim = sButtonName Then
                            '' MOVING BUTTON ''
                            InsertButton(i)
                            Exit Sub
                            '' MOVING BUTTON ''
                        End If
                    End If
                End If

            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertButton(ByVal nButtonIndex As Integer)

        '' IF BUTTON IS MIC THEN KEEP IT AT FIRST PLACE '' THIS CODE WILL NOT LET MIC MOVE ''
        If Me.Items(nButtonIndex).Name.Trim = "Mic" Then
            Exit Sub
        End If

        Dim _ToolButton As ToolStripItem
        _ToolButton = Me.Items(nButtonIndex)
        Me.Items.RemoveAt(nButtonIndex)
        _ToolButton.Visible = True
        Me.Items.Add(_ToolButton)

        If _SeparatorInButtons Then
            Dim _ToolSeperator As New ToolStripSeparator
            Me.Items.Add(_ToolSeperator)
        End If
    End Sub
#End Region

#Region " Public Methods "
    Public Shadows Sub Refresh()
        CustomizeToolStrip()
    End Sub

    Public Sub InitializeContextMenu()
        Try
            If sConnectionString <> "" And _ModuleName <> "" And Me.Visible = True Then
                '' BIND CONTEXT MENU FIRST TIME ONLY ''
                If Me.ContextMenuStrip Is Nothing Then
                    '' SAVE ITS DEFAULT SETTINGS LOCALY ''
                    GetDefaultToolStrip()
                    If _ModuleName = "frmPatientSynopsis" Then
                        Dim CloseButton As String = "tlsbtnClose.Visible"
                        If _DefaultToolStrip.Contains(CloseButton) Then
                            _DefaultToolStrip.Remove(CloseButton)
                            _DefaultToolStrip.Add(CloseButton)
                        End If
                    End If
                    '' GET SETTINGS FROM DATABASE AND REARRANGE BUTTONS ''
                    CustomizeToolStrip()

                    oContextMenu = New ContextMenuStrip

                    oContextMenu.Items.Add("Customize")
                    oContextMenu.Items(0).Image = Global.gloToolStrip.My.Resources.Customize
                    oContextMenu.Items(0).ForeColor = Color.FromArgb(31, 73, 125)
                    oContextMenu.Items(0).Font = gloGlobal.clsgloFont.gFont_SMALL

                    '22-May-15 Aniket: Resolving Bug #83509: EMR: Patient History- Window get minimized as user right click on toolbar
                    AddHandler oContextMenu.Items(0).Click, AddressOf On_Customize_Click
                    AddHandler oContextMenu.Opened, AddressOf oContextMenu_Opened

                    Try
                        If (IsNothing(Me.ContextMenuStrip) = False) Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(Me.ContextMenuStrip)
                            If (IsNothing(Me.ContextMenuStrip.Items) = False) Then
                                Me.ContextMenuStrip.Items.Clear()
                            End If
                            Me.ContextMenuStrip.Dispose()
                            Me.ContextMenuStrip = Nothing
                        End If
                    Catch

                    End Try

                    Me.ContextMenuStrip = oContextMenu

                End If

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub oContextMenu_Opened(sender As Object, e As System.EventArgs)
        ChangeZOrder()
    End Sub


    Private Sub ChangeZOrder()
        Try
            If (IsNothing(Me.TopLevelControl) = False) Then
                Me.TopLevelControl.BringToFront()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
