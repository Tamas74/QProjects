'
' FlexGroup.vb
'
' Implements Outlook-style grouping using the C1FlexGrid control
'
' Version history:
'
' 1.0: Jun 2002    Ported from C# sample                       Ron MacKee
' 1.1: Jun 2003    Updated (minor fixes and improvements)      Bernardo Castilho
' 1.2: Jan 2004    Updated (minor fixes and improvements)      Bernardo Castilho
' 1.5: Dec 2004    Updated (allow sorting ungrouped cols)      Bernardo Castilho
'
'
Imports System.Text
Imports System.ComponentModel
Imports System.Reflection
Imports C1.Win.C1FlexGrid

Public Class FlexGroupControl
    Inherits System.Windows.Forms.PictureBox
    Implements ISupportInitialize, IDisposable

    ' fields
    Dim WithEvents _flex As C1FlexGrid         ' grid control
    Dim _groups As ArrayList        ' list of fields (columns) in the group area
    Dim _dragger As FieldDragger    ' aux control to drag fields
    Dim _styleGroup As CellStyle    ' used to paint groups
    Dim _styleEmpty As CellStyle    ' used to paint empty area
    Dim _showGroups As Boolean      ' show/hide grouping area
    Dim _insGroup As Boolean        ' whether column being inserted is a group/column 
    Dim _dirty As Boolean           ' need to refresh groups
    Dim _insIndex As Integer        ' index where group/column should be inserted
    Dim _insRect As Rectangle       ' rectangle where insert indicator is drawn
    Dim _insRectLast As Rectangle   ' rectangle where last insert indicator was drawn
    Dim _brBack As SolidBrush       ' gdi objects used for painting group area
    Dim _brFore As SolidBrush
    Dim _brGrp As SolidBrush
    Dim _brBdr As SolidBrush
    Dim _filterRow As FilterRow     ' filter row (control visibility with FilterRow property)
    Dim _groupMessage As String     ' message displayed in the empty group area
    Dim _SavedFlexGroup As String = ""
    Shared _sf As StringFormat
    Shared _bmpInsert As Bitmap     ' insert icon
    Shared _bmpSortUp As Bitmap     ' sort icon
    Shared _bmpSortDn As Bitmap     ' sort icon

    Const SPACING As Integer = 8               ' spacing between groups, edges
    Const SCROLLSTEP As Integer = 15           ' scroll step (while dragging mouse)
    Const DRAGTHRESHOLD As Integer = 8         ' pixels before starting column drag
    Const GROUP_MSG As String = "Drag column headers here to create groups"
    Dim imgList_Common As ImageList

    Public Event _FlexDoubleClick()
    Public Event _FlexMouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs)
    Public Event _FlexMouseMove(ByVal Sender As Object, ByVal e As MouseEventArgs)
    Public Event _flexMouseUp()
    Public Event _FlexAfterScroll(ByVal Sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs)

    Dim FilterColomnOldindex As Integer
    Dim CurrentGroup As String = ""

    Private Const Col_Task_DueDate = 1
    Private Const Col_Task_PatientName = 2
    Private Const Col_Task_SSN = 3
    Private Const Col_Task_DOB = 4
    Private Const Col_Task_Subject = 5
    Private Const Col_Task_Priority = 6
    Private Const Col_Task_Status = 7
    Private Const Col_Task_TaskType = 8


    Private Const Col_Task_TaskID = 9
    Private Const Col_Task_No = 10
    Private Const Col_Task_Completed = 11
    Private Const Col_Task_TaskDate = 12
    Private Const Col_Task_PatientID = 13
    Private Const Col_Task_Assigned = 14
    Private Const Col_Task_PatientCode = 15
    Private Const Col_Task_ProviderID = 16
    Private Const Col_Task_CategoryID = 17
    Private Const Col_Task_ProviderName = 18
    Private Const Col_Task_ColCount = 19
    Private Const Col_Task_GroupID = 20
    Private Const Col_Task_BisOwner_Custom = 21   ''added owner column 8080 iteration2
    Private Const Col_Task_BisOwnerAssigned_Custom = 22  ''added ownerassigned column 8080 iteration2
    Private Const Col_Task_Resp = 23  ''added new responsibility column 8080 iteration2
    Private Col_Task_DueDate_Custom As Int16 = 1
    Private Col_Task_PatientName_Custom As Int16 = 2
    Private Col_Task_SSN_Custom As Int16 = 3
    Private Col_Task_DOB_Custom As Int16 = 4
    Private Col_Task_Subject_Custom As Int16 = 5
    Private Col_Task_Priority_Custom As Int16 = 6
    Private Col_Task_Status_Custom As Int16 = 7
    Private Col_Task_TaskType_Custom As Int16 = 8
    Private Col_Task_Resp_Custom As Int16 = 23
    Private Col_Task_DueDateVisible_Custom As Boolean = True
    Private Col_Task_PatientNameVisible_Custom As Boolean = True
    Private Col_Task_SSNVisible_Custom As Boolean = True
    Private Col_Task_DOBVisible_Custom As Boolean = True
    Private Col_Task_SubjectVisible_Custom As Boolean = True
    Private Col_Task_PriorityVisible_Custom As Boolean = True
    Private Col_Task_StatusVisible_Custom As Boolean = True
    Private Col_Task_TaskTypeVisible_Custom As Boolean = True
    Private Col_Task_VisibleBisOwner As Boolean = True
    Private Col_Task_VisibleBisOwnerAssigned As Boolean = True
    Private Col_Task_RespVisible_Custom As Boolean = True
    Private Const FixedColumnCount As Int16 = 9
    'Public _blnIsAllowownerDraw As Boolean = False
    'Property blnIsAllowownerDraw As Boolean
    '    Get
    '        Return _blnIsAllowownerDraw
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _blnIsAllowownerDraw = value
    '    End Set
    'End Property


    Public blnIsFlexDatasourceReset = True


    Public _FlexColumnWidthInfoCustom As String
    Property FlexColumnWidthInfoCustom As String
        Get
            Return _FlexColumnWidthInfoCustom
        End Get
        Set(ByVal value As String)
            _FlexColumnWidthInfoCustom = value
        End Set
    End Property


    Public _FlexColumnInfoCustom As String
    Property FlexColumnInfoCustom As String
        Get
            Return _FlexColumnInfoCustom
        End Get
        Set(ByVal value As String)
            _FlexColumnInfoCustom = value
            SetColumnPreference()
        End Set
    End Property

    Public _FlexColumnInfo As String
    Private components As System.ComponentModel.IContainer
    Property FlexColumnInfo As String
        Get
            Return _FlexColumnInfo
        End Get
        Set(ByVal value As String)
            _FlexColumnInfo = value
            ' repaint the control
            If Not IsNothing(_FlexColumnInfo) Then
                _flex.BeginInit()
                _flex.ColumnInfo = _FlexColumnInfo
                _flex.EndInit()
                Invalidate()
            End If
        End Set
    End Property
    ' Flag: Has Dispose already been called? 
    Dim bdisposed As Boolean = False

    '' Public implementation of Dispose pattern callable by consumers. 
    'Public Sub Dispose() Implements IDisposable.Dispose
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub

    ' Protected implementation of Dispose pattern. 
    Protected Overrides Sub Dispose(disposing As Boolean)


        If bdisposed Then Return

        If disposing Then
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {_flex.ContextMenuStrip}

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try


                If (IsNothing(_groups) = False) Then
                    _groups.Clear()
                    _groups = Nothing
                End If

                If (IsNothing(_dragger) = False) Then
                    _dragger.Dispose()
                    _dragger = Nothing
                End If
                _styleGroup = Nothing
                _styleEmpty = Nothing
                If (IsNothing(_brBack) = False) Then
                    _brBack.Dispose()
                    _brBack = Nothing
                End If

                If (IsNothing(_brFore) = False) Then
                    _brFore.Dispose()
                    _brFore = Nothing
                End If

                If (IsNothing(_brGrp) = False) Then
                    _brGrp.Dispose()
                    _brGrp = Nothing
                End If

                If (IsNothing(_brBdr) = False) Then
                    _brBdr.Dispose()
                    _brBdr = Nothing
                End If

                If (IsNothing(_filterRow) = False) Then
                    _filterRow.Clear()
                    _filterRow = Nothing
                End If

                If (IsNothing(_bmpInsert) = False) Then
                    _bmpInsert.Dispose()
                    _bmpInsert = Nothing
                End If

                If (IsNothing(_bmpSortUp) = False) Then
                    _bmpSortUp.Dispose()
                    _bmpSortUp = Nothing
                End If

                If (IsNothing(_bmpSortDn) = False) Then
                    _bmpSortDn.Dispose()
                    _bmpSortDn = Nothing
                End If

                If (IsNothing(imgList_Common) = False) Then
                    imgList_Common.Dispose()
                    imgList_Common = Nothing
                End If
                If (IsNothing(_sf) = False) Then
                    _sf.Dispose()
                    _sf = Nothing
                End If
                If (IsNothing(_filterRow) = False) Then
                    _filterRow.Dispose()
                    _filterRow = Nothing
                End If


                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                    End If
                End If


                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                    End If
                End If
                If (IsNothing(_flex) = False) Then

                    _flex.Styles.Clear()
                    _flex.Dispose()
                    _flex = Nothing
                End If
            Catch ex As Exception

            End Try
            ' Free any other managed objects here. 
            ' 
        End If
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
        ' Free any unmanaged objects here. 
        '
        bdisposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Sub New()
        MyBase.New()

        ' initialize static (shared) members
        If _sf Is Nothing Then
            _sf = New StringFormat(StringFormat.GenericDefault)
            _sf.Alignment = StringAlignment.Center
            _sf.LineAlignment = StringAlignment.Center
            _bmpInsert = LoadBitmap("InsertPoint", Color.White)
            _bmpSortUp = LoadBitmap("SortUp", Color.Red)
            _bmpSortDn = LoadBitmap("SortDn", Color.Red)
        End If

        imgList_Common = New ImageList()

        imgList_Common.Images.Add(My.Resources.HighPriority)
        imgList_Common.Images.Add(My.Resources.NormalPriority)
        imgList_Common.Images.Add(My.Resources.LowPriority)
        imgList_Common.Images.Add(My.Resources.Task_NoOwner)
        imgList_Common.Images.Add(My.Resources.Task_OtherTaken)
        imgList_Common.Images.Add(My.Resources.Task_Owner)
        imgList_Common.Images.Add(My.Resources.Task_Single)
        ' initialize contained Flex control
        _flex = New C1FlexGrid()
        _flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        _flex.Dock = DockStyle.Bottom
        _flex.Size = New Size(10, 10)
        _flex.AllowSorting = AllowSortingEnum.None
        _flex.AllowMerging = AllowMergingEnum.Nodes
        _flex.Cols(0).Width = _flex.Rows.DefaultSize
        _flex.ShowCursor = True
        _flex.Tree.Style = TreeStyleFlags.Symbols
        _flex.DrawMode = DrawModeEnum.OwnerDraw

        ' initialize styles
        _flex.Styles.Normal.Border.Direction = BorderDirEnum.Horizontal
        Try
            If (_flex.Styles.Contains("Group")) Then
                _styleGroup = _flex.Styles("Group")
            Else
                _styleGroup = _flex.Styles.Add("Group", _flex.Styles.Fixed)
            End If
        Catch ex As Exception
            _styleGroup = _flex.Styles.Add("Group", _flex.Styles.Fixed)
        End Try
        ' _styleGroup = _flex.Styles.Add("Group", _flex.Styles.Fixed)
        Try
            If (_flex.Styles.Contains("Empty")) Then
                _styleEmpty = _flex.Styles("Empty")
            Else
                _styleEmpty = _flex.Styles.Add("Empty", _flex.Styles.EmptyArea)
            End If
        Catch ex As Exception
            _styleEmpty = _flex.Styles.Add("Empty", _flex.Styles.EmptyArea)
        End Try
        '_styleEmpty = _flex.Styles.Add("Empty", _flex.Styles.EmptyArea)
        _styleEmpty.BackColor = SystemColors.ControlDarkDark
        _styleEmpty.ForeColor = SystemColors.ControlLightLight


        ' initialize internal members
        _groupMessage = GROUP_MSG
        _groups = New ArrayList()
        _showGroups = True
        _insIndex = -1

        ' initialize field dragger control
        _dragger = New FieldDragger(Me)

        ' add filter row (control visibility with 'FilterRow' property)
        _filterRow = New FilterRow(_flex)
        _filterRow.Visible = False

        ' initialize parent control
        SuspendLayout()
        BorderStyle = BorderStyle.Fixed3D
        BackColor = SystemColors.ControlDark
        ForeColor = SystemColors.ControlLightLight
        Controls.AddRange(New System.Windows.Forms.Control() {_dragger, _flex})
        ResumeLayout(False)

    End Sub

    ' ** ISupportInitialize

    Sub BeginInit() Implements ISupportInitialize.BeginInit
        _flex.BeginInit()
    End Sub

    Sub EndInit() Implements ISupportInitialize.EndInit

        ' don't call EndInit without BeginInit first <<B4>>
        _flex.BeginInit()
        _flex.EndInit()

        ' flex has re-created the styles, 
        ' so get a fresh reference to the custom ones we'll use
        _styleGroup = _flex.Styles("Group")
        _styleEmpty = _flex.Styles("Empty")

        ' make sure grid is visible <<B4>>
        _flex.Visible = True
        UpdateLayout()

    End Sub

    ' ** object model

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    ReadOnly Property Grid() As C1FlexGrid
        Get
            Grid = _flex
        End Get
    End Property

    Property ShowGroups() As Boolean
        Get
            Return _showGroups
        End Get
        Set(ByVal Value As Boolean)
            _showGroups = Value
            UpdateLayout()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    ReadOnly Property StyleGroupRows() As CellStyle
        Get
            Return _styleGroup
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    ReadOnly Property StyleGroupArea() As CellStyle
        Get
            Return _styleGroup
        End Get
    End Property

    <Description("Gets or sets the message shown in the empty group area."), _
    Localizable(True), _
    DefaultValue(GROUP_MSG)> _
    Public Property GroupMessage() As String ' <<B4>>
        Get
            Return _groupMessage
        End Get
        Set(ByVal Value As String)
            _groupMessage = Value
            Invalidate()
        End Set
    End Property

    <Description("Gets or sets a comma-delimited list of the groups (by column name)."), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Groups() As String
        Get
            ' build string with column names
            Dim sb As New StringBuilder()
            Dim col As Column
            For Each col In _groups
                If sb.Length > 0 Then sb.Append(", ")
                sb.Append(col.Name)
            Next
            Return sb.ToString()
        End Get
        Set(ByVal Value As String)

            ' make current group columns visible
            If (IsNothing(_flex) = False) Then
                _flex.Redraw = False
            End If


            Dim col As Column
            Dim isPrevGroup As Boolean = False
            If (IsNothing(_groups)) Then
                _groups = New ArrayList()
            End If
            For Each col In _groups
                If col.Name <> Value Then
                    col.Visible = True
                Else
                    isPrevGroup = True
                End If
            Next
            ' rebuild _groups collection
            If isPrevGroup = False Then
                _groups.Clear()
                _dirty = True
                UpdateLayout()
                ArrangeColumn()
                If (String.IsNullOrEmpty(Value) = False) Then
                    Dim colName As String
                    For Each colName In Value.Split(",")
                        Dim trimColName As String = colName.Trim()
                        If (String.IsNullOrEmpty(trimColName) = False) AndAlso (IsNothing(_flex) = False) Then
                            If (_flex.Cols.Contains(trimColName)) Then
                                col = _flex.Cols(trimColName)
                                If (IsNothing(col) = False) AndAlso (String.IsNullOrEmpty(col.Name) = False) Then
                                    _groups.Add(col)
                                    CurrentGroup = col.Name
                                End If
                            End If
                        End If
                    Next
                End If
                ' apply new collection
                UpdateGroups()
                UpdateLayout()
                'ArrangeColumn()
                ' done
            End If
            If (IsNothing(_flex) = False) Then
                _flex.Redraw = True
            End If
        End Set
    End Property

    <Description("Gets or sets whether the control should display a filter row above the data."), _
     DefaultValue(False)> _
    Public Property FilterRow() As Boolean
        Get
            Return _filterRow.Visible
        End Get
        Set(ByVal Value As Boolean)
            If _filterRow.Visible <> Value Then
                '_groups.Clear()
                '_dirty = True
                'UpdateLayout()
                'ArrangeColumn()
                _filterRow.Clear()
                _filterRow.Visible = Value

            End If
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Shadows Property Image() As Image
        Get
            Return MyBase.Image
        End Get
        Set(ByVal Value As Image)
            MyBase.Image = Value
        End Set
    End Property

    ' ** overrides

    ' adjust layout when the control is resized
    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        UpdateLayout()
        ArrangeColumnWidth()
        MyBase.OnResize(e)
    End Sub

    ' start dragging groups
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)

        ' we're interested in the left button
        Dim i As Integer, rc As Rectangle
        If (e.Button And MouseButtons.Left) <> 0 Then
            For i = 0 To _groups.Count - 1
                rc = GetGroupRectangle(i)
                If rc.Contains(e.X, e.Y) Then
                    _dragger.StartDragging(_groups(i), rc)
                    Exit Sub
                End If
            Next
        End If

        ' allow base class processing
        MyBase.OnMouseDown(e)
    End Sub

    ' paint group area
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Try

            UpdateObjects()
            Dim g As Graphics = e.Graphics

            ' get group area
            Dim rc As Rectangle = ClientRectangle
            rc.Height = _flex.Top

            ' draw background
            g.FillRectangle(_brBack, rc)
            If _groups.Count = 0 Then
                g.DrawString(_groupMessage, _styleEmpty.Font, _brFore, ToRCF(rc), _sf)
            Else ' draw groups
                Dim i As Integer
                For i = 0 To _groups.Count - 1
                    rc = GetGroupRectangle(i)
                    PaintGroup(g, rc, _groups(i))
                Next
            End If

            '' show insert position while dragging
            'If _dragger.Visible Then
            '    DrawImageCentered(g, _bmpInsert, _insRect)
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    ' update GDI objects based on grid style
    Sub UpdateObjects()
        Dim clr As Color

        ' update objects used to draw group area
        clr = _styleEmpty.BackColor
        If (_brBack Is Nothing) OrElse (Not _brBack.Color.Equals(clr)) Then
            If (IsNothing(_brBack) = False) Then
                _brBack.Dispose()
                _brBack = Nothing
            End If
            _brBack = New SolidBrush(clr)
        End If


        clr = _styleEmpty.ForeColor
        If (_brFore Is Nothing) OrElse (Not _brFore.Color.Equals(clr)) Then
            If (IsNothing(_brFore) = False) Then
                _brFore.Dispose()
                _brFore = Nothing
            End If
            _brFore = New SolidBrush(clr)
        End If


        ' update objects used to draw grid
        clr = _styleGroup.BackColor
        If (_brGrp Is Nothing) OrElse (Not _brGrp.Color.Equals(clr)) Then
            If (IsNothing(_brGrp) = False) Then
                _brGrp.Dispose()
                _brGrp = Nothing
            End If
            _brGrp = New SolidBrush(clr)
        End If


        clr = _styleGroup.Border.Color
        If (_brBdr Is Nothing) OrElse (Not _brBdr.Color.Equals(clr)) Then
            If (IsNothing(_brBdr) = False) Then
                _brBdr.Dispose()
                _brBdr = Nothing
            End If
            _brBdr = New SolidBrush(clr)
        End If

    End Sub


    ' ** event handlers

    ' cancel dragging when a key is pressed
    Private Sub _flex_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _flex.KeyPress
        If _dragger.Visible Then
            _dragger.Visible = False
            Invalidate()
        End If
    End Sub

    ' start dragging columns
    Private Sub _flex_BeforeMouseDown(ByVal sender As Object, ByVal e As BeforeMouseDownEventArgs) Handles _flex.BeforeMouseDown


        ' we're interested in the left button
        If (e.Button And MouseButtons.Left) = 0 Then Exit Sub

        ' check that the click was on a column header
        Dim hti As HitTestInfo = _flex.HitTest(e.X, e.Y)
        If hti.Type <> HitTestTypeEnum.ColumnHeader Then Exit Sub

        ' check that the click was on a scrollable column
        Dim cols As ColumnCollection = _flex.Cols
        If hti.Column < cols.Fixed Then Exit Sub

        ' check that the click was on the first row
        ' (in case there's additional fixed rows)
        If hti.Row > 0 Then Exit Sub

        ' eat the event 
        e.Cancel = True

        ' check that we have at least one non-grouped column
        If _groups.Count >= cols.Count - cols.Fixed - 1 Then Exit Sub

        ' start dragging column
        _dragger.StartDragging(cols(hti.Column))


    End Sub

    ' prevent editing group headers // <<B5>>
    Private Sub _flexBeforeEdit(ByVal sender As Object, ByVal e As RowColEventArgs) Handles _flex.BeforeEdit
        If _flex.Rows(e.Row).IsNode Then
            e.Cancel = True
        End If
    End Sub

    ' update layout after resizing columns
    Private Sub _flex_AfterResizeColumn(ByVal sender As Object, ByVal e As RowColEventArgs) Handles _flex.AfterResizeColumn


        UpdateLayout()
        FlexColumnWidthInfoCustom = Convert.ToString(_flex.Cols("Due Date").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Subject").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Status").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Patient Name").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Priority").Width) & ":" &
                                    Convert.ToString(_flex.Cols("SSN").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Date Of Birth").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Task Type").Width) & ":" &
                                    Convert.ToString(_flex.Cols("Resp").Width)





    End Sub


    ' draw cells to make them look like Outlook

    Public Sub _flex_OwnerDrawCell(ByVal sender As Object, ByVal e As OwnerDrawCellEventArgs) Handles _flex.OwnerDrawCell




        ' don't draw while measuring
        If e.Measuring Then Exit Sub

        ' custom draw only scrollable rows on tree column
        If _groups.Count = 0 Then Exit Sub
        'If e.Col <> _flex.Tree.Column Then Exit Sub
        'If e.Row < _flex.Rows.Fixed Then Exit Sub

        ' make sure we got the gdi objects we need
        UpdateObjects()

        ' get parameters we'll need
        Dim row As Row = _flex.Rows(e.Row)
        If row.Node Is Nothing Then Return
        Dim idt As Integer = _flex.Tree.Indent
        Dim x As Integer = _flex.ScrollPosition.X
        Dim lvl As Integer = row.Node.Level
        Dim rc As Rectangle

        '' custom draw nodes
        If row.IsNode Then

            ' draw background and content
            e.Style = _styleGroup
            e.DrawCell(DrawCellFlags.Background Or DrawCellFlags.Content)

            ' draw line above
            If (lvl = 0) OrElse (Not _flex.Rows(e.Row - 1).IsNode) Then
                rc = e.Bounds
                OffsetLeft(rc, lvl * idt + x)
                rc.Height = 1
                e.Graphics.FillRectangle(_brBdr, rc)
            End If

            ' if the node is expanded, draw line below
            If row.Node.Expanded Then
                rc = e.Bounds
                OffsetLeft(rc, (lvl + 1) * idt + x)
                rc.Y = rc.Bottom - 1
                rc.Height = 1
                e.Graphics.FillRectangle(_brBdr, rc)
            End If

            ' draw vertical lines to the left of the symbol
            rc = e.Bounds
            rc.X += x
            rc.Width = 1
            Dim i As Integer
            For i = 0 To lvl
                e.Graphics.FillRectangle(_brBdr, rc)
                rc.Offset(idt, 0)
            Next

        Else ' custom draw data

            ' base painting
            e.DrawCell()
            'Application.DoEvents()

            '' fill area on the left
            'rc = e.Bounds
            'rc.Width = (lvl + 1) * idt
            'e.Graphics.FillRectangle(_brGrp, rc)

            ' draw vertical lines over filled area
            'rc = e.Bounds
            'rc.Width = 1
            'Dim i As Integer
            'For i = 0 To lvl + 1
            '    e.Graphics.FillRectangle(_brBdr, rc)
            '    rc.Offset(idt, 0)
            'Next
        End If

    End Sub

    Public Sub OffsetLeft(ByRef rc As Rectangle, ByVal x As Integer)
        rc.X = rc.X + x
        rc.Width = rc.Width - x
    End Sub

    ' reset groups when data source changes <<1.1>>
    Private Sub _flex_DataChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs) Handles _flex.AfterDataRefresh


        If e.ListChangedType = ListChangedType.Reset Then
            UpdateGroups()
            If (Not _dirty) AndAlso (Not UpdateGroupNeeded()) Then
                Dim strPrevGroup = Groups
                Groups = ""
                Groups = strPrevGroup
                setPriorityImage()
                strPrevGroup = Nothing
            Else
                If Groups = "" Then
                    setPriorityImage()
                End If
            End If
        End If
    End Sub

    ' repaint control when styles change
    Private Sub _flex_GridChanged(ByVal sender As Object, ByVal e As GridChangedEventArgs) Handles _flex.GridChanged
        If e.GridChangedType = GridChangedTypeEnum.RepaintGrid Then
            Invalidate()
        End If
    End Sub

    ' ** utilities



    'Sub SetColumnPreference()
    '    Dim strColumns() As String
    '    If Not IsNothing(_FlexColumnInfoCustom) Then
    '        If _FlexColumnInfoCustom <> "" Then
    '            strColumns = _FlexColumnInfoCustom.Split(",")
    '            ''Added To Check Customize column index should not be exeed the set limit(Currently 8-Will Change if new columns are added in Custom column list)
    '            For Each Col As String In strColumns
    '                Dim strColDetails() As String = Col.Split(":")
    '                If strColDetails.Length > 1 Then
    '                    If Convert.ToInt32(strColDetails(1)) > 8 And Convert.ToInt32(strColDetails(1)) <> 23 Then
    '                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "Dash Board Load Task Grid Set To Default", gloAuditTrail.ActivityOutCome.Success)
    '                        strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
    '                    End If
    '                End If
    '            Next
    '            ''
    '        Else
    '            strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
    '        End If
    '    Else
    '        strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
    '    End If

    '    Dim strColName As String = ""
    '    Dim StrColPosition As String = ""
    '    Dim nColCount As Int16 = 0

    '    Dim CurrentGroup As String = ""
    '    If _groups.Count > 0 Then
    '        Dim col As Column
    '        col = _groups(0)
    '        CurrentGroup = col.Name
    '    End If
    '    Try
    '        Dim strColInfo = "24,1,0,0,0,100," +
    '                         "Columns:" +
    '                         "0{Width:18;Visible:False;}"
    '        If strColumns.Length > 0 Then
    '            For Each Col As String In strColumns
    '                Dim strColDetails() As String = Col.Split(":")
    '                If strColDetails.Count > 0 Then

    '                    nColCount = nColCount + 1
    '                    If strColDetails(0) = "Due Date" Then
    '                        Col_Task_DueDate_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_DueDateVisible_Custom = True
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:56;Name:""Due Date"";Caption:""Due Date"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Patient Name" Then
    '                        Col_Task_PatientName_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_PatientNameVisible_Custom = True
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:75;Name:""Patient Name"";Caption:""Patient Name"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Subject" Then
    '                        Col_Task_Subject_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_SubjectVisible_Custom = True
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:141;Name:""Subject"";Caption:""Subject"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "SSN" Then
    '                        Col_Task_SSN_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_SSNVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:47;Name:""SSN"";Caption:""SSN"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Date Of Birth" Then
    '                        Col_Task_DOB_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_DOBVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:47;Name:""Date Of Birth"";Caption:""Date Of Birth"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Priority" Then
    '                        Col_Task_Priority_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_PriorityVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:47;Name:""Priority"";Caption:""Priority"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Status" Then
    '                        Col_Task_Status_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_StatusVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:56;Name:""Status"";Caption:""Status"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Task Type" Then
    '                        Col_Task_TaskType_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_TaskTypeVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        strColInfo = strColInfo + "" + Convert.ToString(nColCount) + "{Width:56;Name:""Task Type"";Caption:""Task Type"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
    '                    ElseIf strColDetails(0) = "Resp" Then
    '                        Col_Task_Resp_Custom = Convert.ToInt16(strColDetails(1))
    '                        Col_Task_RespVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
    '                        'strColInfo = strColInfo + "" + "23" + "{Width:56;Name:""Resp"";Caption:""Resp"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"


    '                    End If
    '                End If
    '                strColDetails = Nothing
    '            Next

    '            strColInfo = strColInfo + "9{Width:0;Name:""TaskID"";Caption:""TaskID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "10{Width:0;Name:""No."";Caption:""No."";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
    '                                        "11{Width:0;Name:""% Completed"";Caption:""% Completed"";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
    '                                        "12{Width:0;Name:""TaskDate"";Caption:""TaskDate"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "13{Width:0;Name:""PatientID"";Caption:""PatientID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "14{Width:0;Name:""AssignStatus"";Caption:""AssignStatus"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "15{Width:0;Name:""PatientCode"";Caption:""PatientCode"";Visible:False;AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	" +
    '                                        "16{Width:0;Name:""nProviderID"";Caption:""nProviderID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "17{Width:0;Name:""nCategoryID"";Caption:""nCategoryID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "18{Width:0;Name:""ProviderName"";Caption:""ProviderName"";Visible:False;AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	" +
    '                                        "19{Width:0;Name:""Total Count"";Caption:""Total Count"";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
    '                                       "20{Width:0;Name:""TaskGroupID"";Caption:""TaskGroupID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "21{Width:0;Name:""bIsOwner"";Caption:""bIsOwner"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
    '                                        "22{Width:0;Name:""bIsOwnerAssigned"";Caption:""bIsOwnerAssigned"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}" +
    '                                        "23{Width:56;Name:""Resp"";Caption:""Resp"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"



    '        End If

    '        If FlexColumnWidthInfoCustom = "" Then
    '            FlexColumnWidthInfoCustom = "56:75:141:47:47:47:56:56:30"
    '        End If


    '        If _groups.Count > 0 Then
    '            Groups = ""
    '        End If
    '        If Not IsNothing(strColInfo) Then
    '            _flex.BeginInit()
    '            _flex.ColumnInfo = strColInfo
    '            _flex.Update()
    '            _flex.EndInit()
    '            Invalidate()
    '        End If

    '        Groups = CurrentGroup
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
    '    Finally
    '        strColumns = Nothing
    '    End Try
    'End Sub


    Sub SetColumnPreference()
        Dim strColumns() As String
        If Not IsNothing(_FlexColumnInfoCustom) Then
            If _FlexColumnInfoCustom <> "" Then
                _FlexColumnInfoCustom = _FlexColumnInfoCustom.Replace("9", "23") ''added 23 for responsibility column

                strColumns = _FlexColumnInfoCustom.Split(",")
                ''Added To Check Customize column index should not be exeed the set limit(Currently 8-Will Change if new columns are added in Custom column list)
                For Each Col As String In strColumns
                    Dim strColDetails() As String = Col.Split(":")
                    If strColDetails.Length > 1 Then
                       
                        If Convert.ToInt32(strColDetails(1)) > 8 And Convert.ToInt32(strColDetails(1)) <> 23 Then  ''added condition 23 for responsibility column 
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "Dash Board Load Task Grid Set To Default", gloAuditTrail.ActivityOutCome.Success)
                            strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
                        End If
                    End If
                Next
                ''
            Else
                strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
            End If
        Else
            strColumns = ("Due Date:1:1,Patient Name:2:1,Subject:3:1,Task Type:4:1,Status:5:1,Priority:6:1,SSN:7:1,Date Of Birth:8:1,Resp:23:1").Split(",")
        End If

        Dim strColName As String = ""
        Dim StrColPosition As String = ""
        Dim nColCount As Int16 = 0

        Dim CurrentGroup As String = ""
        If _groups.Count > 0 Then
            Dim col As Column
            col = _groups(0)
            CurrentGroup = col.Name
        End If
        Try
            Dim strColInfo = "24,1,0,0,0,100," +
                             "Columns:" +
                             "0{Width:18;Visible:False;}"
            If strColumns.Length > 0 Then
                For Each Col As String In strColumns
                    Dim strColDetails() As String = Col.Split(":")
                    If strColDetails.Count > 0 Then

                        nColCount = nColCount + 1
                        If strColDetails(0) = "Due Date" Then
                            Col_Task_DueDate_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_DueDateVisible_Custom = True
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:56;Name:""Due Date"";Caption:""Due Date"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Patient Name" Then
                            Col_Task_PatientName_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_PatientNameVisible_Custom = True
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:75;Name:""Patient Name"";Caption:""Patient Name"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Subject" Then
                            Col_Task_Subject_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_SubjectVisible_Custom = True
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:141;Name:""Subject"";Caption:""Subject"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "SSN" Then
                            Col_Task_SSN_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_SSNVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:47;Name:""SSN"";Caption:""SSN"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Date Of Birth" Then
                            Col_Task_DOB_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_DOBVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:47;Name:""Date Of Birth"";Caption:""Date Of Birth"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Priority" Then
                            Col_Task_Priority_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_PriorityVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:47;Name:""Priority"";Caption:""Priority"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Status" Then
                            Col_Task_Status_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_StatusVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:56;Name:""Status"";Caption:""Status"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Task Type" Then
                            Col_Task_TaskType_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_TaskTypeVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:56;Name:""Task Type"";Caption:""Task Type"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"
                        ElseIf strColDetails(0) = "Resp" Then
                            Col_Task_Resp_Custom = Convert.ToInt16(strColDetails(1))
                            Col_Task_RespVisible_Custom = Convert.ToBoolean(Convert.ToInt16(strColDetails(2)))
                            strColInfo = strColInfo + "" + Convert.ToString(strColDetails(1)) + "{Width:56;Name:""Resp"";Caption:""Resp"";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	"


                        End If
                    End If
                    strColDetails = Nothing
                Next

                strColInfo = strColInfo + "9{Width:0;Name:""TaskID"";Caption:""TaskID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "10{Width:0;Name:""No."";Caption:""No."";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
                                            "11{Width:0;Name:""% Completed"";Caption:""% Completed"";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
                                            "12{Width:0;Name:""TaskDate"";Caption:""TaskDate"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "13{Width:0;Name:""PatientID"";Caption:""PatientID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "14{Width:0;Name:""AssignStatus"";Caption:""AssignStatus"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "15{Width:0;Name:""PatientCode"";Caption:""PatientCode"";Visible:False;AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	" +
                                            "16{Width:0;Name:""nProviderID"";Caption:""nProviderID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "17{Width:0;Name:""nCategoryID"";Caption:""nCategoryID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "18{Width:0;Name:""ProviderName"";Caption:""ProviderName"";Visible:False;AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}	" +
                                            "19{Width:0;Name:""Total Count"";Caption:""Total Count"";Visible:False;AllowEditing:False;Style:""DataType:System.Int32;TextAlign:GeneralCenter;"";}	" +
                                           "20{Width:0;Name:""TaskGroupID"";Caption:""TaskGroupID"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "21{Width:0;Name:""bIsOwner"";Caption:""bIsOwner"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}	" +
                                            "22{Width:0;Name:""bIsOwnerAssigned"";Caption:""bIsOwnerAssigned"";Visible:False;AllowEditing:False;Style:""DataType:System.Decimal;TextAlign:GeneralCenter;"";}"



            End If

            If FlexColumnWidthInfoCustom = "" Then
                FlexColumnWidthInfoCustom = "56:75:141:47:47:47:56:56:30"
            End If


            If _groups.Count > 0 Then
                Groups = ""
            End If
            If Not IsNothing(strColInfo) Then
                _flex.BeginInit()
                _flex.ColumnInfo = strColInfo
                _flex.Update()
                _flex.EndInit()
                Invalidate()
            End If

            Groups = CurrentGroup
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        Finally
            strColumns = Nothing
        End Try
    End Sub

    Sub ArrangeColumn()
        Dim col As Column = Nothing
        If _groups.Count > 0 Then
            col = _groups(0)
        End If
        ''added 23 for responsibility column
        Try
            If (Col_Task_DueDate_Custom > FixedColumnCount And Col_Task_DueDate_Custom <> 23) Or
                (Col_Task_Subject_Custom > FixedColumnCount And Col_Task_Subject_Custom <> 23) Or
                (Col_Task_PatientName_Custom > FixedColumnCount And Col_Task_PatientName_Custom <> 23) Or
                (Col_Task_Priority_Custom > FixedColumnCount And Col_Task_Priority_Custom <> 23) Or
                (Col_Task_Status_Custom > FixedColumnCount And Col_Task_Status_Custom <> 23) Or
                (Col_Task_TaskType_Custom > FixedColumnCount And Col_Task_TaskType_Custom <> 23) Or
                (Col_Task_SSN_Custom > FixedColumnCount And Col_Task_SSN_Custom <> 23) Or
                (Col_Task_DOB_Custom > FixedColumnCount And Col_Task_DOB_Custom <> 23) Then

                '' Col_Task_Resp_Custom > FixedColumnCount
                SetColumnPreference()
            End If

            If Not IsNothing(col) Then
                If col.Name <> "Due Date" Then
                    If FilterColomnOldindex > Col_Task_DueDate_Custom Then
                        _flex.Cols("Due Date").Move(Col_Task_DueDate_Custom + 1)
                    Else
                        _flex.Cols("Due Date").Move(Col_Task_DueDate_Custom)
                    End If
                End If
                If col.Name <> "Subject" Then
                    If FilterColomnOldindex > Col_Task_Subject_Custom Then
                        _flex.Cols("Subject").Move(Col_Task_Subject_Custom + 1)
                    Else
                        _flex.Cols("Subject").Move(Col_Task_Subject_Custom)
                    End If
                End If
                If col.Name <> "Status" Then
                    If FilterColomnOldindex > Col_Task_Status_Custom Then
                        _flex.Cols("Status").Move(Col_Task_Status_Custom + 1)
                    Else
                        _flex.Cols("Status").Move(Col_Task_Status_Custom)
                    End If
                End If
                If col.Name <> "Priority" Then
                    If FilterColomnOldindex > Col_Task_Priority_Custom Then
                        _flex.Cols("Priority").Move(Col_Task_Priority_Custom + 1)
                    Else
                        _flex.Cols("Priority").Move(Col_Task_Priority_Custom)
                    End If
                End If
                If col.Name <> "Patient Name" Then
                    If FilterColomnOldindex > Col_Task_PatientName_Custom Then
                        _flex.Cols("Patient Name").Move(Col_Task_PatientName_Custom + 1)
                    Else
                        _flex.Cols("Patient Name").Move(Col_Task_PatientName_Custom)
                    End If
                End If
                If col.Name <> "SSN" Then
                    If FilterColomnOldindex > Col_Task_SSN_Custom Then
                        _flex.Cols("SSN").Move(Col_Task_SSN_Custom + 1)
                    Else
                        _flex.Cols("SSN").Move(Col_Task_SSN_Custom)
                    End If
                End If
                If col.Name <> "Date Of Birth" Then
                    If FilterColomnOldindex > Col_Task_DOB_Custom Then
                        _flex.Cols("Date Of Birth").Move(Col_Task_DOB_Custom + 1)
                    Else
                        _flex.Cols("Date Of Birth").Move(Col_Task_DOB_Custom)
                    End If
                End If
                If col.Name <> "Task Type" Then
                    If FilterColomnOldindex > Col_Task_TaskType_Custom Then
                        _flex.Cols("Task Type").Move(Col_Task_TaskType_Custom + 1)
                    Else
                        _flex.Cols("Task Type").Move(Col_Task_TaskType_Custom)
                    End If
                End If

                If col.Name <> "Resp" Then
                    If FilterColomnOldindex > Col_Task_Resp_Custom Then
                        _flex.Cols("Resp").Move(Col_Task_Resp_Custom + 1)
                    Else
                        _flex.Cols("Resp").Move(Col_Task_Resp_Custom)
                    End If
                End If

                'If col.Name <> "bisOwner" Then
                '    If FilterColomnOldindex > Col_Task_BisOwner_Custom Then
                '        _flex.Cols("bisOwner").Move(Col_Task_BisOwner_Custom + 1)
                '    Else
                '        _flex.Cols("bisOwner").Move(Col_Task_BisOwner_Custom)
                '    End If
                'End If

                'If col.Name <> "bIsOwnerAssigned" Then
                '    If FilterColomnOldindex > Col_Task_BisOwnerAssigned_Custom Then
                '        _flex.Cols("bIsOwnerAssigned").Move(Col_Task_BisOwnerAssigned_Custom + 1)
                '    Else
                '        _flex.Cols("bIsOwnerAssigned").Move(Col_Task_BisOwnerAssigned_Custom)
                '    End If
                'End If

                If col.Name = "Due Date" Then
                    _flex.Cols("Due Date").Visible = False
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    '_flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Subject" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = False
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    '  _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Patient Name" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = False
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    '   _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    '   _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Status" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = False
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Priority" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = False
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    '_flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "SSN" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = False
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    '  _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Date Of Birth" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = False
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                ElseIf col.Name = "Task Type" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = False
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                ElseIf col.Name = "Resp" Then
                    _flex.Cols("Due Date").Visible = True
                    _flex.Cols("Patient Name").Visible = True
                    _flex.Cols("Subject").Visible = True
                    _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                    _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                    _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                    _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                    _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                    _flex.Cols("Resp").Visible = False
                    ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                    ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
                End If

            Else
                _flex.Cols("Due Date").Move(Col_Task_DueDate_Custom)
                _flex.Cols("Patient Name").Move(Col_Task_PatientName_Custom)
                _flex.Cols("SSN").Move(Col_Task_SSN_Custom)
                _flex.Cols("Date Of Birth").Move(Col_Task_DOB_Custom)
                _flex.Cols("Subject").Move(Col_Task_Subject_Custom)
                _flex.Cols("Priority").Move(Col_Task_Priority_Custom)
                _flex.Cols("Status").Move(Col_Task_Status_Custom)
                _flex.Cols("Task Type").Move(Col_Task_TaskType_Custom)
                _flex.Cols("Resp").Move(Col_Task_Resp_Custom)
                ' _flex.Cols("bisOwner").Move(Col_Task_BisOwner_Custom)
                ' _flex.Cols("bIsOwnerAssigned").Move(Col_Task_BisOwnerAssigned_Custom)

                _flex.Cols("Due Date").Visible = Col_Task_DueDateVisible_Custom
                _flex.Cols("Patient Name").Visible = Col_Task_PatientNameVisible_Custom
                _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                _flex.Cols("Subject").Visible = Col_Task_SubjectVisible_Custom
                _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                ' _flex.Cols("bIsOwner").Visible = Col_Task_VisibleBisOwner
                ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
            End If


            If IsNothing(_flex.Cols("TaskID")) = False Then
                _flex.Cols("TaskID").Move(Col_Task_TaskID)
            End If



            If IsNothing(_flex.Cols("No.")) = False Then
                _flex.Cols("No.").Move(Col_Task_No)
            End If



            If IsNothing(_flex.Cols("% Completed")) = False Then
                _flex.Cols("% Completed").Move(Col_Task_Completed)
            End If


            If IsNothing(_flex.Cols("TaskDate")) = False Then
                _flex.Cols("TaskDate").Move(Col_Task_TaskDate)
            End If


            If IsNothing(_flex.Cols("PatientID")) = False Then
                _flex.Cols("PatientID").Move(Col_Task_PatientID)
            End If


            If IsNothing(_flex.Cols("AssignStatus")) = False Then
                _flex.Cols("AssignStatus").Move(Col_Task_Assigned)
            End If


            If IsNothing(_flex.Cols("PatientCode")) = False Then
                _flex.Cols("PatientCode").Move(Col_Task_PatientCode)
            End If


            If IsNothing(_flex.Cols("nProviderID")) = False Then
                _flex.Cols("nProviderID").Move(Col_Task_ProviderID)
            End If


            If IsNothing(_flex.Cols("nCategoryID")) = False Then
                _flex.Cols("nCategoryID").Move(Col_Task_CategoryID)
            End If


            If IsNothing(_flex.Cols("ProviderName")) = False Then
                _flex.Cols("ProviderName").Move(Col_Task_ProviderName)
            End If


            If IsNothing(_flex.Cols("Total Count")) = False Then
                _flex.Cols("Total Count").Move(Col_Task_ColCount)
            End If


            If IsNothing(_flex.Cols("TaskGroupID")) = False Then
                _flex.Cols("TaskGroupID").Move(Col_Task_GroupID)
            End If

            If IsNothing(_flex.Cols("bIsOwner")) = False Then
                _flex.Cols("bIsOwner").Move(Col_Task_BisOwner_Custom)
            End If
            If IsNothing(_flex.Cols("bIsOwnerAssigned")) = False Then
                _flex.Cols("bIsOwnerAssigned").Move(Col_Task_BisOwnerAssigned_Custom)
            End If

            If IsNothing(_flex.Cols("TaskID")) = False Then
                _flex.Cols("TaskID").Visible = False
            End If


            If IsNothing(_flex.Cols("No.")) = False Then
                _flex.Cols("No.").Visible = False
            End If


            If IsNothing(_flex.Cols("% Completed")) = False Then
                _flex.Cols("% Completed").Visible = False
            End If


            If IsNothing(_flex.Cols("TaskDate")) = False Then
                _flex.Cols("TaskDate").Visible = False
            End If


            If IsNothing(_flex.Cols("PatientID")) = False Then
                _flex.Cols("PatientID").Visible = False
            End If


            If IsNothing(_flex.Cols("AssignStatus")) = False Then
                _flex.Cols("AssignStatus").Visible = False
            End If


            If IsNothing(_flex.Cols("PatientCode")) = False Then
                _flex.Cols("PatientCode").Visible = False
            End If


            If IsNothing(_flex.Cols("nProviderID")) = False Then
                _flex.Cols("nProviderID").Visible = False
            End If


            If IsNothing(_flex.Cols("nCategoryID")) = False Then
                _flex.Cols("nCategoryID").Visible = False
            End If


            If IsNothing(_flex.Cols("ProviderName")) = False Then
                _flex.Cols("ProviderName").Visible = False
            End If


            If IsNothing(_flex.Cols("Total Count")) = False Then
                _flex.Cols("Total Count").Visible = False
            End If


            If IsNothing(_flex.Cols("TaskGroupID")) = False Then
                _flex.Cols("TaskGroupID").Visible = False
            End If
            If IsNothing(_flex.Cols("bIsOwner")) = False Then
                _flex.Cols("bIsOwner").Visible = False
            End If
            If IsNothing(_flex.Cols("bIsOwnerAssigned")) = False Then
                _flex.Cols("bIsOwnerAssigned").Visible = False
            End If


            ArrangeColumnWidth()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    Sub ArrangeColumnWidth()
        Try
            Dim col As Column = Nothing
            If (IsNothing(_groups) = False) Then
                If _groups.Count > 0 Then
                    col = _groups(0)
                End If
            End If

            Dim nVisibleCols As Int16 = 0

            If FlexColumnWidthInfoCustom <> "" Then
                Dim flexColWidth() As String = FlexColumnWidthInfoCustom.Split(":")
                If flexColWidth.Length > 0 Then
                    _flex.Cols("Due Date").Width = flexColWidth(0)
                End If
                If flexColWidth.Length > 1 Then
                    _flex.Cols("Subject").Width = flexColWidth(1)
                End If
                If flexColWidth.Length > 2 Then
                    _flex.Cols("Status").Width = flexColWidth(2)
                End If
                If flexColWidth.Length > 3 Then
                    _flex.Cols("Patient Name").Width = flexColWidth(3)
                End If
                If flexColWidth.Length > 4 Then
                    _flex.Cols("Priority").Width = flexColWidth(4)
                End If
                If flexColWidth.Length > 5 Then
                    _flex.Cols("SSN").Width = flexColWidth(5)
                End If
                If flexColWidth.Length > 6 Then
                    _flex.Cols("Date Of Birth").Width = flexColWidth(6)
                End If
                If flexColWidth.Length > 7 Then
                    _flex.Cols("Task Type").Width = flexColWidth(7)
                End If
                If flexColWidth.Length > 8 Then
                    If (_flex.Cols.Contains("Resp")) Then
                        _flex.Cols("Resp").Width = flexColWidth(8)
                    End If
                End If
                'If flexColWidth.Length > 8 Then
                '    _flex.Cols("bisOwner").Width = flexColWidth(8)
                'End If
                'If flexColWidth.Length > 9 Then
                '    _flex.Cols("bisOwnerAssigned").Width = flexColWidth(9)
                'End If
                Dim colWidth As Long = 0
                Dim colWhiteSpcae As Long = 0
                For Each Column As Column In _flex.Cols
                    If Column.Visible Then
                        If Column.Name = "Subject" Then
                            nVisibleCols = nVisibleCols + 3
                        ElseIf Column.Name = "Patient Name" Then
                            nVisibleCols = nVisibleCols + 2
                        Else
                            nVisibleCols = nVisibleCols + 1
                        End If
                        colWidth = colWidth + Column.Width
                    End If
                Next


                If colWidth < _flex.Parent.Width Then
                    colWhiteSpcae = _flex.Parent.Width - colWidth
                    For Each Column As Column In _flex.Cols
                        If Column.Visible Then
                            If Column.Name = "Subject" Then
                                _flex.Cols(Column.Index).Width = _flex.Cols(Column.Index).Width + ((colWhiteSpcae / nVisibleCols) * 3)
                            ElseIf Column.Name = "Patient Name" Then
                                _flex.Cols(Column.Index).Width = _flex.Cols(Column.Index).Width + ((colWhiteSpcae / nVisibleCols) * 2)
                            Else
                                _flex.Cols(Column.Index).Width = _flex.Cols(Column.Index).Width + (colWhiteSpcae / nVisibleCols)
                            End If
                        End If
                    Next
                End If

            Else
                For Each Column As Column In _flex.Cols
                    If Column.Visible Then
                        If Column.Name = "Subject" Then
                            nVisibleCols = nVisibleCols + 3
                        ElseIf Column.Name = "Patient Name" Then
                            nVisibleCols = nVisibleCols + 2
                        Else
                            nVisibleCols = nVisibleCols + 1
                        End If
                    End If
                Next

                For Each Column As Column In _flex.Cols
                    If Column.Visible Then
                        If Column.Name = "Subject" Then
                            _flex.Cols(Column.Index).Width = (_flex.Parent.Width / nVisibleCols) * 3
                        ElseIf Column.Name = "Patient Name" Then
                            _flex.Cols(Column.Index).Width = (_flex.Parent.Width / nVisibleCols) * 2
                        Else
                            _flex.Cols(Column.Index).Width = _flex.Parent.Width / nVisibleCols
                        End If
                    End If
                Next
            End If


            If IsNothing(col) Then
                _flex.Cols("Due Date").Visible = Col_Task_DueDateVisible_Custom
                _flex.Cols("Subject").Visible = Col_Task_SubjectVisible_Custom
                _flex.Cols("Status").Visible = Col_Task_StatusVisible_Custom
                _flex.Cols("Patient Name").Visible = Col_Task_PatientNameVisible_Custom
                _flex.Cols("Priority").Visible = Col_Task_PriorityVisible_Custom
                _flex.Cols("SSN").Visible = Col_Task_SSNVisible_Custom
                _flex.Cols("Date Of Birth").Visible = Col_Task_DOBVisible_Custom
                _flex.Cols("Task Type").Visible = Col_Task_TaskTypeVisible_Custom
                If (_flex.Cols.Contains("Resp")) Then
                    _flex.Cols("Resp").Visible = Col_Task_RespVisible_Custom
                End If
                ' _flex.Cols("bisOwner").Visible = Col_Task_VisibleBisOwner
                ' _flex.Cols("bIsOwnerAssigned").Visible = Col_Task_VisibleBisOwnerAssigned
            End If

            _flex.Cols(0).Visible = False


            If IsNothing(_flex.Cols("TaskID")) = False Then
                _flex.Cols("TaskID").Width = 0
            End If

            If IsNothing(_flex.Cols("No.")) = False Then
                _flex.Cols("No.").Width = 0
            End If

            If IsNothing(_flex.Cols("No.")) = False Then
                _flex.Cols("% Completed").Width = 0
            End If


            If IsNothing(_flex.Cols("TaskDate")) = False Then
                _flex.Cols("TaskDate").Width = 0
            End If


            If IsNothing(_flex.Cols("PatientID")) = False Then
                _flex.Cols("PatientID").Width = 0
            End If


            If IsNothing(_flex.Cols("AssignStatus")) = False Then
                _flex.Cols("AssignStatus").Width = 0
            End If


            If IsNothing(_flex.Cols("PatientCode")) = False Then
                _flex.Cols("PatientCode").Width = 0
            End If


            If IsNothing(_flex.Cols("nProviderID")) = False Then
                _flex.Cols("nProviderID").Width = 0
            End If


            If IsNothing(_flex.Cols("nCategoryID")) = False Then
                _flex.Cols("nCategoryID").Width = 0
            End If


            If IsNothing(_flex.Cols("ProviderName")) = False Then
                _flex.Cols("ProviderName").Width = 0
            End If


            If IsNothing(_flex.Cols("Total Count")) = False Then
                _flex.Cols("Total Count").Width = 0
            End If


            If IsNothing(_flex.Cols("TaskGroupID")) = False Then
                _flex.Cols("TaskGroupID").Width = 0
            End If
            If IsNothing(_flex.Cols("bisOwner")) = False Then
                _flex.Cols("bisOwner").Width = 0
            End If
            If IsNothing(_flex.Cols("bIsOwnerAssigned")) = False Then
                _flex.Cols("bIsOwnerAssigned").Width = 0
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try


    End Sub

    ' update the size of the group area
    Sub UpdateLayout()

        ' calculate size of grouping area
        Dim hei As Integer = 0
        If _showGroups Then
            If _groups.Count > 0 Then
                hei = GetGroupRectangle(_groups.Count - 1).Bottom + SPACING
            Else
                hei = _flex.Rows.DefaultSize + 2 * SPACING ' <<B5>>
            End If
        End If

        ' move grid to remaining area
        _flex.Height = ClientRectangle.Height - hei

        ' repaint the control
        Invalidate()

        ' and update groups
        'UpdateGroups()
    End Sub

    ' update grouping/sorting
    Function UpdateGroupNeeded() As Boolean

        ' reset groups if the data source has changed <<1.1>>
        Dim i As Integer
        For i = 0 To _groups.Count - 1
            Dim col As Column = _groups(i)

            ' if index is bad, try fixing it
            If col.Index < 0 Then
                If _flex.Cols.Contains(col.Name) Then
                    _groups(i) = _flex.Cols(col.Name)
                Else
                    _groups.Clear()
                    Return True
                End If
            End If
        Next

        ' groups must be the first columns and must be hidden
        Dim cols As ColumnCollection = _flex.Cols
        Dim iFixed As Integer = cols.Fixed
        For i = 0 To _groups.Count - 1
            Dim col As Column = cols(i + iFixed)
            If (col.Visible) OrElse (Not _groups(i).Equals(col)) Then Return True
        Next

        ' remaining columns must be visible
        For i = iFixed + _groups.Count To cols.Count - 1
            If Not cols(i).Visible Then Return True
            Return False
        Next
        Return False
    End Function

    ' update grouping/sorting
    Sub UpdateGroups()

        ' check whether we need to update the groups
        If (Not _dirty) AndAlso (Not UpdateGroupNeeded()) Then Return

        If _groups.Count > 0 Then
            ' stop painting for a while
            _flex.Redraw = False

            ' remove subtotals
            _flex.Subtotal(AggregateEnum.Clear)

            ' adjust group columns
            Dim cols As ColumnCollection = _flex.Cols
            Dim index As Integer = cols.Fixed
            Dim col As Column

            For Each col In _groups
                If col.Index = 0 Then
                    _groups.Clear()
                    _dirty = True
                    UpdateLayout()
                    ArrangeColumn()
                    ''setPriorityImage()
                    Exit Sub
                End If

                ' adjust column position/visibility
                col.Visible = False
                If col.Index <> 0 Then
                    FilterColomnOldindex = col.Index
                    cols.Move(col.Index, index)
                    ArrangeColumn()
                End If

                ' initialize sorting direction if necessary
                col.Sort = SortFlags.None

                If (col.Sort And (SortFlags.Ascending Or SortFlags.Descending)) = 0 Then
                    If col.Caption = "Due Date" Then
                        col.Sort = SortFlags.Descending
                    Else
                        col.Sort = SortFlags.Ascending
                    End If
                End If
            Next


            'For Each col1 As Column In _flex.Cols
            '    If col1.Caption = "Subject" Or col1.Caption = "Patient Name" Or col1.Caption = "Due Date" Or col1.Caption = "Task Type" Then '
            '        If col1.Sort = SortFlags.None Then
            '            col1.Sort = SortFlags.Ascending
            '        End If
            '    End If
            'Next

            ' sort columns
            '_flex.Sort(SortFlags.UseColSort, cols.Fixed, _groups.Count)
            RemoveHandler _flex.AfterDataRefresh, AddressOf _flex_DataChanged
            RemoveHandler _flex.GridChanged, AddressOf _flex_GridChanged
            _flex.Sort(SortFlags.UseColSort, cols.Fixed, cols.Count - 1) ' <<B5>>
            AddHandler _flex.AfterDataRefresh, AddressOf _flex_DataChanged
            AddHandler _flex.GridChanged, AddressOf _flex_GridChanged

            ' make sure tree is in the right position
            _flex.Tree.Column = cols.Fixed + _groups.Count

            ' and make sure tree contents are left-aligned (or there's no spill merge) <<1.1>>
            _flex.Cols(_flex.Tree.Column).TextAlign = TextAlignEnum.LeftCenter

            ' and insert subtotals
            For index = 0 To _groups.Count - 1
                Dim icol As Integer = index + cols.Fixed
                Dim fmt As String
                _flex.Subtotal(AggregateEnum.Clear)
                fmt = cols(icol).Caption + ": {0}"
                _flex.Subtotal(AggregateEnum.Sum, index, icol, 0, fmt)
            Next

            ''setPriorityImage()
            setGroupTotal()

            ' done
            _dirty = False
            _flex.Redraw = True
        End If
    End Sub
    'Function Added to show group total for groups 
    'Case No. 00042704:Tasks, high priority tasks are not showing up unless the option is turned on
    Private Sub setGroupTotal()
        Dim childnodeCount As Integer = 0
        Dim strOldData As String
        Dim i, j As Integer
        Dim strNewData As String


        If _flex.Cols("Due Date").Index = 2 Then
            _flex.Cols("Due Date").DataType = GetType(String)
        End If
        For i = 0 To _flex.Rows.Count - 1
            If _flex.Rows(i).IsNode Then
                For j = i + 1 To _flex.Rows.Count - 1
                    If _flex.Rows(j).IsNode = False Then
                        childnodeCount = childnodeCount + 1
                    Else
                        Exit For
                    End If
                Next

                strOldData = Convert.ToString(_flex.GetData(i, 2))
                strNewData = strOldData & " [" & childnodeCount & "]"

                _flex.SetData(i, 2, strNewData)

                i = i + childnodeCount
                childnodeCount = 0
            End If
        Next
        If _flex.Cols("Due Date").Index = 2 Then
            _flex.Cols("Due Date").DataType = GetType(Date)
        End If
    End Sub

    'Function Added to show Images for priority column
    'Case No. 00042704:Tasks, images are missing for priority column
    Public Sub setPriorityImage()


        Try
           
            For i As Integer = 0 To _flex.Rows.Count - 1
                If _flex.Rows(i).IsNode = False Then
                    _flex.SetCellImage(i, _flex.Cols("Due Date").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Subject").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Status").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Patient Name").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Priority").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("SSN").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Date Of Birth").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Task Type").Index, Nothing)
                    _flex.SetCellImage(i, _flex.Cols("Resp").Index, Nothing)
                    ' _flex.SetCellImage(i, _flex.Cols("bisOwner").Index, Nothing)
                    If IsNothing(_flex.GetData(i, _flex.Cols("Priority").Index)) = False Then
                        If _flex.GetData(i, _flex.Cols("Priority").Index).ToString() = "1 - High" Then
                            _flex.SetCellImage(i, _flex.Cols("Priority").Index, imgList_Common.Images(0))
                        ElseIf _flex.GetData(i, _flex.Cols("Priority").Index).ToString() = "2 - Normal" Then
                            _flex.SetCellImage(i, _flex.Cols("Priority").Index, imgList_Common.Images(1))
                        ElseIf _flex.GetData(i, _flex.Cols("Priority").Index).ToString() = "3 - Low" Then
                            _flex.SetCellImage(i, _flex.Cols("Priority").Index, imgList_Common.Images(2))
                        ElseIf _flex.GetData(i, _flex.Cols("Priority").Index).ToString() = "2 - Normal " Then
                            _flex.SetCellImage(i, _flex.Cols("Priority").Index, imgList_Common.Images(1))
                        End If
                    End If
                End If

                'imgList_Common.Images.Add(My.Resources.Task_NoOwner)
                'imgList_Common.Images.Add(My.Resources.Task_OtherTaken)
                'imgList_Common.Images.Add(My.Resources.Task_Owner)
                'imgList_Common.Images.Add(My.Resources.Task_Single)
                ' initialize contained Flex control
                'imgList_Common.Images.Add(My.Resources.HighPriority)
                'imgList_Common.Images.Add(My.Resources.NormalPriority)
                'imgList_Common.Images.Add(My.Resources.LowPriority)
                'imgList_Common.Images.Add(My.Resources.Task_NoOwner)
                'imgList_Common.Images.Add(My.Resources.Task_OtherTaken)
                'imgList_Common.Images.Add(My.Resources.Task_Owner)
                'imgList_Common.Images.Add(My.Resources.Task_Single)

                If (_flex.Cols.Contains("Resp")) Then
                    If (_flex.GetData(i, _flex.Cols("TaskGroupID").Index) = "0") Then
                        _flex.SetCellImage(i, _flex.Cols("Resp").Index, imgList_Common.Images(6))
                    End If
                    If ((_flex.GetData(i, _flex.Cols("bIsOwnerAssigned").Index) = "True") And (_flex.GetData(i, _flex.Cols("bisOwner").Index) = "True")) Then
                        _flex.SetCellImage(i, _flex.Cols("Resp").Index, imgList_Common.Images(5))
                    End If
                    If ((_flex.GetData(i, _flex.Cols("bIsOwnerAssigned").Index) = "True") And (_flex.GetData(i, _flex.Cols("bisOwner").Index) = "False")) Then
                        _flex.SetCellImage(i, _flex.Cols("Resp").Index, imgList_Common.Images(4))
                    End If
                    If ((_flex.GetData(i, _flex.Cols("bIsOwnerAssigned").Index) = "False") And (_flex.GetData(i, _flex.Cols("bisOwner").Index) = "False") And (_flex.GetData(i, _flex.Cols("TaskGroupID").Index) <> "0")) Then
                        _flex.SetCellImage(i, _flex.Cols("Resp").Index, imgList_Common.Images(3))
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub


    Public Sub RemovePriorityImage()
        For i As Integer = 0 To _flex.Rows.Count - 1
            If _flex.Rows(i).IsNode = False Then

                _flex.SetCellImage(i, _flex.Cols("Due Date").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Subject").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Status").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Patient Name").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Priority").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("SSN").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Date Of Birth").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Task Type").Index, Nothing)
                _flex.SetCellImage(i, _flex.Cols("Resp").Index, Nothing)
                ' _flex.SetCellImage(i, _flex.Cols("bisOwner").Index, Nothing)

            End If
        Next
    End Sub
    ' update the position of the insertion indicator
    Sub UpdateInsertLocation()
        Dim loc As Point = PointToClient(Control.MousePosition)
        Dim rc As Rectangle

        ' initialize members
        _insRect = Rectangle.Empty
        _insIndex = -1

        ' insert into group list
        If loc.Y < _flex.Top Then

            ' find position where new group should be inserted
            Dim index As Integer = _groups.Count
            Dim i As Integer
            For i = 0 To _groups.Count - 1
                rc = GetGroupRectangle(i)
                If rc.X + rc.Width / 2 > loc.X Then
                    index = i
                    Exit For
                End If
            Next

            ' update insert info
            _insGroup = True
            _insIndex = index

            ' update insert position
            If index < _groups.Count Then
                _insRect = GetGroupRectangle(index)
                _insRect.X = _insRect.X - SPACING
            Else
                _insRect = GetGroupRectangle(_groups.Count - 1)
                _insRect.X = _insRect.Right
            End If
            If index > 0 AndAlso index < _groups.Count Then
                _insRect.Y = _insRect.Y - _insRect.Height / 2
                _insRect.Height = _insRect.Height + _insRect.Height / 2
            End If
            _insRect.Width = SPACING

        Else ' remove from group list (insert into grid)

            ' find position where grid column should be inserted
            Dim index As Integer = _flex.Cols.Count
            Dim i As Integer
            For i = _flex.Cols.Fixed To _flex.Cols.Count - 1
                rc = _flex.GetCellRect(0, i, False)
                If rc.X + rc.Width / 2 > loc.X Then
                    index = i
                    Exit For
                End If
            Next

            ' and update insert info
            _insGroup = False
            _insIndex = index

            ' update insert position
            If index < _flex.Cols.Count Then
                _insRect = _flex.GetCellRect(0, index, False)
                _insRect.Width = 0
            Else
                _insRect = _flex.GetCellRect(0, index - 1, False)
                _insRect.X = _insRect.Right
                _insRect.Width = 0
            End If
            _insRect.Inflate(SPACING / 2, 5)
            _insRect.Offset(0, _flex.Top)
        End If

        ' invalidate to show new insertion point
        If Not _insRect.Equals(_insRectLast) Then
            Invalidate()
            _insRectLast = _insRect
        End If
    End Sub

    ' finish dragging a column (or group)
    Sub FinishDragging(ByVal col As Column, ByVal dragged As Boolean)
        Try

            ' didn't drag? then it was a click: apply/reverse sort
            If Not dragged Then
                RaiseEvent _flexMouseUp()
                If _groups.Count > 0 Then
                    _groups.Clear()
                    _dirty = True
                    UpdateLayout()
                    ArrangeColumn()
                End If


                'reverse column sort
                If (col.Sort And SortFlags.Ascending) <> 0 Then
                    col.Sort = SortFlags.Descending
                Else
                    col.Sort = SortFlags.Ascending
                End If


                ' if this is a non-grouped column, reset Sort property on all other <<B5>>
                ' non-grouped columns and re-apply sort
                If Not _groups.Contains(col) Then
                    ' reset sort order on other columns
                    Dim colIndex As Integer = col.Index
                    Dim cols As ColumnCollection = _flex.Cols
                    Dim c As Integer
                    For c = _groups.Count To cols.Count - 1
                        If c <> colIndex Then
                            _flex.Cols(c).Sort = SortFlags.None
                        End If
                    Next
                    ' apply sort
                    _flex.Col = colIndex
                    _flex.Sort(SortFlags.UseColSort, cols.Fixed, cols.Count - 1)
                    _flex.ShowSortAt(col.Sort, colIndex)
                    setPriorityImage()
                    Return

                End If


                ' OLD CODE
                ' if this is a non-grouped column, sort only when unbound
                ' (to avoid screwing up the groups)
                'If Not _groups.Contains(col) Then
                '    If (_groups.Count = 0) OrElse (_flex.DataSource Is Nothing) Then
                '        _flex.Sort(col.Sort, col.Index)
                '        Return
                '    End If
                'End If

                ' insert column into group collection
            ElseIf _insGroup Then

                '' add group to list at the proper position (col->grp, grp->grp)
                '_groups.Insert(_insIndex, col)
                'Dim i As Integer
                'For i = 0 To _groups.Count - 1
                '    If (i <> _insIndex) AndAlso (_groups(i).Equals(col)) Then
                '        _groups.RemoveAt(i)
                '        Exit For
                '    End If
                '    col.Visible = False
                'Next

            Else ' insert column into grid

                ' move column to a new position (col->col, grp->col)

                If _insIndex <= FixedColumnCount Then

                    If _groups.Count > 0 Then
                        If _insIndex > 2 AndAlso col.Index > 2 Then
                            Dim oldIndex As Integer = col.Index
                            Dim newIndex As Integer = _insIndex

                            If newIndex > oldIndex Then newIndex = newIndex - 1

                            'If _flex.Cols("Due Date").Index > FixedColumnCount Or _flex.Cols("Subject").Index > FixedColumnCount Or _flex.Cols("Status").Index > FixedColumnCount Or
                            '   _flex.Cols("Patient Name").Index > FixedColumnCount Or _flex.Cols("Priority").Index > FixedColumnCount Or _flex.Cols("SSN").Index > FixedColumnCount Or
                            '   _flex.Cols("Date Of Birth").Index > FixedColumnCount Or _flex.Cols("Task Type").Index > FixedColumnCount Then
                            '    ArrangeColumn()
                            '    ArrangeColumnWidth()
                            '    Exit Sub
                            'Else
                            _flex.Cols.Move(oldIndex, newIndex)
                            'End If

                            '_flex.Cols.Move(oldIndex, newIndex)
                            col.Visible = True
                            ' remove from group list, if it's there (grp->col)
                            If _groups.Contains(col) Then
                                _groups.Remove(col)
                            End If
                        End If
                    Else

                        Dim oldIndex As Integer = col.Index
                        Dim newIndex As Integer = _insIndex
                        If newIndex > oldIndex Then newIndex = newIndex - 1

                        'If _flex.Cols("Due Date").Index > FixedColumnCount Or _flex.Cols("Subject").Index > FixedColumnCount Or _flex.Cols("Status").Index > FixedColumnCount Or
                        '     _flex.Cols("Patient Name").Index > FixedColumnCount Or _flex.Cols("Priority").Index > FixedColumnCount Or _flex.Cols("SSN").Index > FixedColumnCount Or
                        '     _flex.Cols("Date Of Birth").Index > FixedColumnCount Or _flex.Cols("Task Type").Index > FixedColumnCount Then
                        '    ArrangeColumn()
                        '    ArrangeColumnWidth()
                        '    Exit Sub
                        'Else
                        _flex.Cols.Move(oldIndex, newIndex)
                        'End If

                        '_flex.Cols.Move(oldIndex, newIndex)
                        col.Visible = True
                        ' remove from group list, if it's there (grp->col)
                        If _groups.Contains(col) Then
                            _groups.Remove(col)
                        End If
                    End If
                End If
                Col_Task_DueDate_Custom = _flex.Cols("Due Date").Index
                Col_Task_Subject_Custom = _flex.Cols("Subject").Index
                Col_Task_PatientName_Custom = _flex.Cols("Patient Name").Index
                Col_Task_Priority_Custom = _flex.Cols("Priority").Index
                Col_Task_Status_Custom = _flex.Cols("Status").Index
                Col_Task_TaskType_Custom = _flex.Cols("Task Type").Index
                Col_Task_SSN_Custom = _flex.Cols("SSN").Index
                Col_Task_DOB_Custom = _flex.Cols("Date Of Birth").Index
                Col_Task_Resp_Custom = _flex.Cols("Resp").Index
                '    Col_Task_BisOwner_Custom = _flex.Cols("bisOwner").Index
                '  Col_Task_BisOwnerAssigned_Custom = _flex.Cols("bIsOwnerAssigned").Index
            End If

            ' update layout and repaint
            _dirty = True
            'UpdateLayout()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    ' get the position of a group in the group area
    Function GetGroupRectangle(ByVal index As Integer) As Rectangle

        ' build skinny rectangle at the top
        Dim rc As Rectangle = New Rectangle(SPACING, SPACING, 0, _flex.Rows.DefaultSize)

        ' set width if we can
        ' (don't use WidthDisplay because the group column is invisible,
        ' so WidthDisplay will return zero)
        Dim wid As Integer
        If (index > -1) AndAlso (index < _groups.Count) Then
            wid = _groups(index).Width ' <<B1.2>>
            If wid < 0 Then wid = Grid.Cols.DefaultSize
            rc.Width = wid
        End If

        ' loop/adjust position
        Dim i As Integer
        For i = 0 To index - 1
            wid = _groups(i).Width ' <<B1.2>>
            If wid < 0 Then wid = Grid.Cols.DefaultSize
            rc.X = rc.X + wid + SPACING
            rc.Y = rc.Y + rc.Height / 2
        Next

        ' return the rectangle
        Return rc
    End Function

    ' draw image centering it on rectangle
    Sub DrawImageCentered(ByVal g As Graphics, ByVal img As Image, ByVal rc As Rectangle)
        Dim sz As Size = img.Size
        rc.Offset((rc.Width - sz.Width) / 2, (rc.Height - sz.Height) / 2)
        rc.Size = sz
        g.DrawImageUnscaled(img, rc)
        Console.WriteLine("rc is {0} {1} {2} {3}", rc.X, rc.Y, rc.Width, rc.Height)
    End Sub

    ' load a bitmap from the assembly resources
    Function LoadBitmap(ByVal name As String, ByVal transparent As Color) As Bitmap
        'Dim a As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        'Dim resName As String = String.Format("{0}.{1}.bmp", a.GetName().Name, name)
        'Dim s As String() = a.GetManifestResourceNames()
        'Dim bmp As Bitmap = New Bitmap(a.GetManifestResourceStream(resName))
        'bmp.MakeTransparent(transparent)
        'Return bmp
        Return Nothing
    End Function

    ' paint group using dragger control
    Sub PaintGroup(ByVal g As Graphics, ByVal rc As Rectangle, ByVal col As Column)
        Try
            ' draw control
            _dragger.PaintControl(g, rc, col.Caption, False)

            ' draw sorting glyph
            Dim img As Image = Nothing
            If _flex.ShowSort Then
                If (col.Sort And SortFlags.Ascending) <> 0 Then
                    img = _bmpSortUp
                ElseIf (col.Sort And SortFlags.Descending) <> 0 Then
                    img = _bmpSortDn
                End If
                If Not img Is Nothing Then
                    rc.X = rc.Right - rc.Height
                    rc.Width = rc.Height
                    DrawImageCentered(g, img, rc)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message + "Paint", False)
        End Try
    End Sub

    Function ToRCF(ByVal rc As Rectangle) As RectangleF
        Return New RectangleF(rc.X, rc.Y, rc.Width, rc.Height)
    End Function

    '/ <summary>
    '/ FieldDragger
    '/ internal class used to implement field dragging
    '/ </summary>
    Private Class FieldDragger
        Inherits Label
        Implements IDisposable
        Dim _owner As FlexGroupControl
        Dim _column As Column
        Dim _offset As Point
        Dim _ptDown As Point
        Dim _dragging As Boolean
        Dim _rcClip As Rectangle
        Dim _brBack As SolidBrush
        Dim _brFore As SolidBrush
        Dim _brDrag As SolidBrush

        Shared _sf As StringFormat
        Shared _pDark As Pen
        Shared _pLite As Pen
        ' Flag: Has Dispose already been called? 
        Dim bdisposed As Boolean = False

        '' Public implementation of Dispose pattern callable by consumers. 
        'Public Sub Dispose() Implements IDisposable.Dispose
        '    Dispose(True)
        '    GC.SuppressFinalize(Me)
        'End Sub

        ' Protected implementation of Dispose pattern. 
        Protected Overrides Sub Dispose(disposing As Boolean)


            If bdisposed Then Return

            If disposing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                If (IsNothing(_brBack) = False) Then
                    _brBack.Dispose()
                    _brBack = Nothing
                End If

                If (IsNothing(_brFore) = False) Then
                    _brFore.Dispose()
                    _brFore = Nothing
                End If

                If (IsNothing(_brDrag) = False) Then
                    _brDrag.Dispose()
                    _brDrag = Nothing
                End If

                If (IsNothing(_sf) = False) Then
                    _sf.Dispose()
                    _sf = Nothing
                End If
                If (IsNothing(_owner) = False) Then
                    ' _owner.Dispose()
                    _owner = Nothing
                End If
                ' Free any other managed objects here. 
                ' 
            End If
            Try
                MyBase.Dispose(disposing)
            Catch ex As Exception

            End Try
            ' Free any unmanaged objects here. 
            '
            bdisposed = True
        End Sub

        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub
        ' ** ctor
        Public Sub New(ByVal owner As FlexGroupControl)

            ' initialize static members
            If _sf Is Nothing Then
                _pDark = SystemPens.ControlDark
                _pLite = SystemPens.ControlLightLight
                _sf = New StringFormat(StringFormat.GenericDefault)
                _sf.Alignment = StringAlignment.Near
                _sf.LineAlignment = StringAlignment.Center
                _sf.FormatFlags = _sf.FormatFlags Or StringFormatFlags.NoWrap
            End If

            ' initialize control
            _owner = owner
            Visible = False
            BackColor = Color.Transparent
        End Sub

        ' ** overrides

        ' use custom painting routine
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Try


                Dim rc As Rectangle = ClientRectangle
                If Not IsNothing(_column) Then
                    PaintControl(e.Graphics, rc, _column.Caption, True)
                End If
            Catch ex As Exception

            End Try
        End Sub

        ' move with the mouse
        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            Try

                ' drag while the left button is down
                If (e.Button And MouseButtons.Left) = 0 Then Return

                ' make sure the mouse moved at least a little
                If Not _dragging Then
                    Dim ptNow As Point = Control.MousePosition
                    If Math.Abs(ptNow.X - _ptDown.X) < DRAGTHRESHOLD AndAlso _
                        Math.Abs(ptNow.Y - _ptDown.Y) < DRAGTHRESHOLD Then Return
                    _dragging = True
                End If

                ' calculate new position for the control
                Dim pos As Point = _owner.PointToClient(Control.MousePosition)
                Dim loc As Point = pos
                loc.Offset(-_offset.X, -_offset.Y)

                ' clip to grouping area, scroll
                Dim rc As Rectangle = _rcClip
                Dim g As C1FlexGrid = _owner.Grid
                Dim pt As Point = g.ScrollPosition
                If loc.X + Width > rc.Right Then
                    loc.X = rc.Right - Width
                    pt.X = pt.X - SCROLLSTEP
                End If
                If loc.X < 0 Then
                    loc.X = 0
                    pt.X = pt.X + SCROLLSTEP
                End If
                If loc.Y + Height > rc.Bottom Then loc.Y = rc.Bottom - Height
                If loc.Y < 0 Then loc.Y = 0

                ' move dragger control
                If Not Location.Equals(loc) Then Location = loc

                ' scroll grid
                Dim scroll As Boolean = False
                If (pos.Y >= _owner.Grid.Top) AndAlso (Not g.ScrollPosition.Equals(pt)) Then
                    Dim oldPos As Point = g.ScrollPosition
                    g.ScrollPosition = pt
                    scroll = Not g.ScrollPosition.Equals(oldPos)
                End If

                ' update insert location (after scrolling grid)
                _owner.UpdateInsertLocation()

                ' keep scrolling
                If scroll Then
                    _owner.Update()
                    OnMouseMove(e)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            End Try

        End Sub

        ' finish dragging when the left mouse button is released
        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)

            ' make sure left button is up
            If (Control.MouseButtons And MouseButtons.Left) <> 0 Then Return

            ' make sure we're visible
            If Not Visible Then Return

            ' hide and finish dragging
            Visible = False
            _owner.FinishDragging(_column, _dragging)

        End Sub

        ' lost focus? cancel
        Protected Overrides Sub OnLeave(ByVal a As EventArgs)
            Visible = False
        End Sub

        ' ** utilities

        ' update GDI objects based on grid style
        Sub UpdateObjects()

            Dim clr As Color

            Dim cs As CellStyle = _owner.Grid.Styles("Group")

            clr = cs.BackColor
            If (_brBack Is Nothing) OrElse (Not _brBack.Color.Equals(clr)) Then
                If (IsNothing(_brBack) = False) Then
                    _brBack.Dispose()
                    _brBack = Nothing
                End If
                _brBack = New SolidBrush(clr)
                If (IsNothing(_brDrag) = False) Then
                    _brDrag.Dispose()
                    _brDrag = Nothing
                End If
                _brDrag = New SolidBrush(Color.FromArgb(100, clr))
            End If

            clr = cs.ForeColor
            If (_brFore Is Nothing) OrElse (Not _brFore.Color.Equals(clr)) Then
                If (IsNothing(_brFore) = False) Then
                    _brFore.Dispose()
                    _brFore = Nothing
                End If
                _brFore = New SolidBrush(clr)
            End If

            Font = cs.Font
        End Sub

        ' start dragging a grid column
        Sub StartDragging(ByVal col As Column)
            Dim g As C1FlexGrid = _owner.Grid
            Dim rc As Rectangle = g.GetCellRect(0, col.Index, False)
            rc.Width = col.WidthDisplay ' <<B1.2>>
            rc = g.RectangleToScreen(rc)
            rc = _owner.RectangleToClient(rc)
            StartDragging(col, rc)
        End Sub

        ' start dragging a group
        Sub StartDragging(ByVal col As Column, ByVal rc As Rectangle)
            Try


                Dim g As C1FlexGrid = _owner.Grid
                _column = col

                ' initialize position/visibility
                Size = New Size(rc.Width, rc.Height)
                Location = New Point(rc.X, rc.Y)
                Visible = True

                ' calculate clip rectangle
                _rcClip = _owner.ClientRectangle
                _rcClip.Height = g.Top + g.Rows(0).HeightDisplay

                ' keep track of the mouse position
                _ptDown = Control.MousePosition
                _offset = PointToClient(_ptDown)
                _dragging = False

                ' capture mouse to track MouseMove event
                Capture = True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            End Try
        End Sub

        ' custom paint routine, usable by anyone
        Sub PaintControl(ByVal g As Graphics, ByVal rc As Rectangle, ByVal text As String, ByVal dragging As Boolean)
            Try
                ' paint control
                UpdateObjects()
                If dragging Then
                    g.FillRectangle(_brDrag, rc)
                Else
                    g.FillRectangle(_brBack, rc)
                End If
                g.DrawString(text, Font, _brFore, ToRCF(rc), _sf)

                ' paint border 
                ' note: ControlPaint.DrawBorder3D is not good with transparent stuff
                rc.Width = rc.Width - 1 : rc.Height = rc.Height - 1
                g.DrawLine(_pDark, rc.Left + 1, rc.Bottom, rc.Right, rc.Bottom)
                g.DrawLine(_pDark, rc.Right, rc.Bottom, rc.Right, rc.Top + 1)
                g.DrawLine(_pLite, rc.Left, rc.Bottom - 1, rc.Left, rc.Top)
                g.DrawLine(_pLite, rc.Left, rc.Top, rc.Right - 1, rc.Top)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message + "Paint Control", False)

            End Try
        End Sub

        Function ToRCF(ByVal rc As Rectangle) As RectangleF
            Return New RectangleF(rc.X, rc.Y, rc.Width, rc.Height)
        End Function

    End Class

    Private Sub _flex_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _flex.DoubleClick
        RaiseEvent _FlexDoubleClick()
    End Sub

    Private Sub _flex_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _flex.MouseDown
        RaiseEvent _FlexMouseDown(sender, e)
    End Sub

    Private Sub _flex_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _flex.MouseDown
        RaiseEvent _FlexMouseMove(sender, e)
    End Sub

    Private Sub _Flex_AfterScroll(ByVal Sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _flex.AfterScroll
        RaiseEvent _FlexAfterScroll(Sender, e)
    End Sub



    ''Private Sub InitializeComponent()
    ''    CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
    ''    Me.SuspendLayout()
    ''    CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    ''    Me.ResumeLayout(False)

    ''End Sub
End Class
