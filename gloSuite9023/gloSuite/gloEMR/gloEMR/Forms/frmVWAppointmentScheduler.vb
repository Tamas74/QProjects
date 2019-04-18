Imports C1.Win.C1FlexGrid

Public Class frmVWAppointmentScheduler
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents trvAppointmentTypes As System.Windows.Forms.TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents c1Appointment As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents dgData As clsDataGrid ' System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWAppointmentScheduler))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.c1Appointment = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.dgData = New gloEMR.clsDataGrid
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.trvAppointmentTypes = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        CType(Me.c1Appointment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLeft.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.c1Appointment)
        Me.pnlMain.Controls.Add(Me.dgData)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 79)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(772, 377)
        Me.pnlMain.TabIndex = 7
        '
        'c1Appointment
        '
        Me.c1Appointment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Appointment.AllowEditing = False
        Me.c1Appointment.BackColor = System.Drawing.Color.GhostWhite
        Me.c1Appointment.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.c1Appointment.ColumnInfo = "1,0,0,0,0,95,Columns:0{TextAlign:LeftCenter;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1Appointment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Appointment.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1Appointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Appointment.Location = New System.Drawing.Point(191, 214)
        Me.c1Appointment.Name = "c1Appointment"
        Me.c1Appointment.Rows.Count = 1
        Me.c1Appointment.Rows.DefaultSize = 19
        Me.c1Appointment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Appointment.Size = New System.Drawing.Size(581, 163)
        Me.c1Appointment.StyleInfo = resources.GetString("c1Appointment.StyleInfo")
        Me.c1Appointment.TabIndex = 16
        '
        'dgData
        '
        Me.dgData.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.Black
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.HeaderForeColor = System.Drawing.Color.White
        Me.dgData.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.Location = New System.Drawing.Point(191, 0)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgData.ReadOnly = True
        Me.dgData.Size = New System.Drawing.Size(581, 214)
        Me.dgData.TabIndex = 10
        Me.dgData.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(190, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1, 377)
        Me.Splitter1.TabIndex = 9
        Me.Splitter1.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.trvAppointmentTypes)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(190, 377)
        Me.pnlLeft.TabIndex = 8
        '
        'trvAppointmentTypes
        '
        Me.trvAppointmentTypes.BackColor = System.Drawing.Color.GhostWhite
        Me.trvAppointmentTypes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAppointmentTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAppointmentTypes.ForeColor = System.Drawing.Color.Black
        Me.trvAppointmentTypes.HideSelection = False
        Me.trvAppointmentTypes.ImageIndex = 0
        Me.trvAppointmentTypes.ImageList = Me.ImageList1
        Me.trvAppointmentTypes.ItemHeight = 21
        Me.trvAppointmentTypes.Location = New System.Drawing.Point(0, 0)
        Me.trvAppointmentTypes.Name = "trvAppointmentTypes"
        Me.trvAppointmentTypes.SelectedImageIndex = 0
        Me.trvAppointmentTypes.Size = New System.Drawing.Size(190, 377)
        Me.trvAppointmentTypes.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "calendar.ico")
        Me.ImageList1.Images.SetKeyName(1, "Arrow_02.ico")
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImage = CType(resources.GetObject("pnlTop.BackgroundImage"), System.Drawing.Image)
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.PictureBox1)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(772, 26)
        Me.pnlTop.TabIndex = 6
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(519, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(33, 16)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(553, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Appointment Scheduler"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 26)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(772, 53)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.btn_img
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(772, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(36, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&Add"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(54, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(61, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(46, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmVWAppointmentScheduler
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(772, 456)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWAppointmentScheduler"
        Me.ShowInTaskbar = False
        Me.Text = "Appointment Scheduler"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        CType(Me.c1Appointment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Fill_AppointmentSchedulerTypes()
        trvAppointmentTypes.Nodes.Clear()
        Dim clAppointmentSchedulerTypes As New Collection
        Dim objAppointmentSchedulerTypes As New clsAppointmentScheduler
        clAppointmentSchedulerTypes = objAppointmentSchedulerTypes.FillAppointmentSchedulerTypes
        objAppointmentSchedulerTypes = Nothing
        With trvAppointmentTypes
            .Nodes.Add("Appointment Type")
            Dim nCount As Integer
            For nCount = 1 To clAppointmentSchedulerTypes.Count
                .Nodes(0).Nodes.Add(clAppointmentSchedulerTypes.Item(nCount))
            Next
            .ExpandAll()
            If trvAppointmentTypes.Nodes(0).GetNodeCount(True) >= 1 Then
                trvAppointmentTypes.SelectedNode = trvAppointmentTypes.Nodes(0).Nodes(0)
                Call Fill_AppointmentSchedulerTypesDetails()
            End If
        End With
    End Sub

    Private Sub frmVWAppointmentScheduler_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub trvAppointmentTypes_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAppointmentTypes.AfterSelect

    End Sub

    Private Sub trvAppointmentTypes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAppointmentTypes.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvAppointmentTypes.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvAppointmentTypes.SelectedNode = trvNode
                If Not (trvAppointmentTypes.SelectedNode Is trvAppointmentTypes.Nodes(0)) Then
                    Call Fill_AppointmentSchedulerTypesDetails()
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Fill_AppointmentSchedulerTypesDetails()
        Dim dtData As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtData.Columns.Add(clmnPropertyName)
        dtData.Columns.Add(clmnPropertyValue)

        Dim drRow As DataRow

        Dim objAppointmentScheduler As New clsAppointmentScheduler
        objAppointmentScheduler.FillAppointmentSchedulerTypesDetails(Trim(trvAppointmentTypes.SelectedNode.Text))
        drRow = dtData.NewRow
        drRow(0) = "Appointment Type"
        drRow(1) = trvAppointmentTypes.SelectedNode.Text
        dtData.Rows.Add(drRow)

        drRow = dtData.NewRow
        drRow(0) = "Appointment Up To"
        Select Case objAppointmentScheduler.AppointmentUpToDurationType
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
                drRow(1) = objAppointmentScheduler.AppointmentUpToDuration & " Days"
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
                drRow(1) = objAppointmentScheduler.AppointmentUpToDuration & " Weeks"
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
                drRow(1) = objAppointmentScheduler.AppointmentUpToDuration & " Months"
        End Select
        dtData.Rows.Add(drRow)


        drRow = dtData.NewRow
        drRow(0) = "Appointment Interval"
        Select Case objAppointmentScheduler.AppointmentIntervalType
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Daily
                drRow(1) = "Daily - After Every " & objAppointmentScheduler.AppointmentInterval & " Days"
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
                Dim strInterval As String
                strInterval = "Weekly - On Every "
                If objAppointmentScheduler.AppointmentInterval Mod 2 = 0 Then
                    strInterval = strInterval & " Monday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 3 = 0 Then
                    strInterval = strInterval & " Tuesday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 5 = 0 Then
                    strInterval = strInterval & " Wednesday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 7 = 0 Then
                    strInterval = strInterval & " Thursday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 11 = 0 Then
                    strInterval = strInterval & " Friday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 13 = 0 Then
                    strInterval = strInterval & " Saturday,"
                End If
                If objAppointmentScheduler.AppointmentInterval Mod 17 = 0 Then
                    strInterval = strInterval & " Sunday,"
                End If
                strInterval = Mid(strInterval, 1, Len(strInterval) - 1)
                drRow(1) = strInterval
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
                drRow(1) = "Monthly - After Every " & objAppointmentScheduler.AppointmentInterval & " Months"
        End Select
        dtData.Rows.Add(drRow)

        drRow = dtData.NewRow
        drRow(0) = "Appointment Duration"
        drRow(1) = objAppointmentScheduler.AppointmentDuration & " minutes"
        dtData.Rows.Add(drRow)

        drRow = dtData.NewRow
        drRow(0) = "Color Code"
        drRow(1) = Color.FromArgb(CType(objAppointmentScheduler.ColorCode, Integer))
        dtData.Rows.Add(drRow)


        Dim grdTableStyle As New clsDataGridTableStyle(dtData.TableName)

        Dim grdColStylePropertyName As New DataGridTextBoxColumn
        With grdColStylePropertyName
            .HeaderText = "Property Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = clmnPropertyName.ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width
        End With

        Dim grdColStylePropertyValue As New DataGridTextBoxColumn
        With grdColStylePropertyValue
            .HeaderText = "Property Value"
            .Alignment = HorizontalAlignment.Left
            .MappingName = clmnPropertyValue.ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width - 10
        End With

        'grdTableStyle.GridColumnStyles.Clear()
        'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyValue})

        'dgData.TableStyles.Clear()
        'dgData.TableStyles.Add(grdTableStyle)
        'dgData.DataSource = dtData

        With c1Appointment
            .DrawMode = DrawModeEnum.OwnerDraw
            .AllowEditing = False

            .BringToFront()
            .Rows.Fixed = 1
            .Cols.Fixed = 0

            .Cols.Count = 2
            .Rows.Count = dtData.Rows.Count + 1

            .Cols(0).Width = .Width * 0.5
            .Cols(1).Width = .Width * 0.5


            .SetData(0, 0, "Property Name")
            .SetData(0, 1, "Property Value")


            Dim i As Integer
            For i = 0 To dtData.Rows.Count - 1

                .SetData(i + 1, 0, dtData.Rows(i)(0))
                .SetData(i + 1, 1, dtData.Rows(i)(1))

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                If i = dtData.Rows.Count - 1 Then
                    .Cols(1).UserData = objAppointmentScheduler.ColorCode
                    .SetData(i + 1, 1, Space(6) & dtData.Rows(i)(1))
                    ' Dim cForecolor As Color
                    'Dim cBackcolor As Color
                    Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, 1)

                    ' cStyle = .Styles.Add("BubbleValues")
                    Try
                        If (.Styles.Contains("BubbleValues")) Then
                            cStyle = .Styles("BubbleValues")
                        Else
                            cStyle = .Styles.Add("BubbleValues")

                        End If
                    Catch ex As Exception
                        cStyle = .Styles.Add("BubbleValues")

                    End Try
                    cStyle.BackColor = Color.Blue
                End If
            Next
        End With

    End Sub
    Private Sub c1Appointment_OwnerDrawCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles c1Appointment.OwnerDrawCell
        Try

            If Not c1Appointment.Cols(e.Col).UserData Is Nothing AndAlso e.Row = 5 AndAlso e.Col = 1 Then
                'Dim value As Double

                'calculate bar extent 
                Dim rc As Rectangle = e.Bounds

                rc.Width = 25

                'draw background
                e.DrawCell(DrawCellFlags.Background Or DrawCellFlags.Border)

                'draw bar
                '   Dim colBackColor As Object
                '  Dim brBackColor As System.Drawing.Brush

                Dim _bdrBrush As SolidBrush
                _bdrBrush = New SolidBrush(Color.FromArgb(CType(c1Appointment.Cols(1).UserData, Integer)))


                e.Graphics.FillRectangle(_bdrBrush, rc)

                'draw cell content
                e.DrawCell(DrawCellFlags.Content)
                _bdrBrush.Dispose()
                _bdrBrush = Nothing

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    'Private Sub AddShadow(ByVal e As PaintEventArgs)
    '    ' Create two SizeF objects.
    '    Dim shadowSize As New SizeF(20.0F, 10.0F)

    '    ' Add them together and save the result in shadowSize.
    '    'shadowSize = SizeF.op_Addition(shadowSize, addSize)

    '    ' Get the location of the ListBox and convert it to a PointF.
    '    Dim shadowLocation As PointF = Point.op_Implicit(c1Appointment.Location)

    '    ' Create a rectangleF. 
    '    Dim rectFToFill As New RectangleF(shadowLocation, shadowSize)

    '    ' Create a custom brush using a semi-transparent color, and 
    '    ' then fill in the rectangle.
    '    Dim customColor As Color = Color.FromArgb(50, Color.Gray)
    '    Dim shadowBrush As SolidBrush = New SolidBrush(customColor)
    '    e.Graphics.FillRectangles(shadowBrush, _
    '        New RectangleF() {rectFToFill})

    '    ' Dispose of the brush.
    '    shadowBrush.Dispose()

    'End Sub


    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub AddCategory()
        Try
            Dim frm As New frmMSTAppointmentScheduler
            frm.blnModify = True
            frm.FillAppointmentUpToIntervals()
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            frm.Dispose()
            frm = Nothing
            Call Fill_AppointmentSchedulerTypes()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            ''''''''IF Statement (i.e.,If not IsNothing(trvAppointmentTypes.SelectedNode) Then)Added by Anil on 05/10/2007 at 3:30 p.m.
            ''''''''This code is added because the application was giving Error:"NullReferenceException" on click of Modify Button.
            If Not IsNothing(trvAppointmentTypes.SelectedNode) Then

                If Not (trvAppointmentTypes.SelectedNode Is trvAppointmentTypes.Nodes(0)) Then
                    Dim objAppointmentSchedulerType As New clsAppointmentScheduler

                    objAppointmentSchedulerType.FillAppointmentSchedulerTypesDetails(Trim(trvAppointmentTypes.SelectedNode.Text))
                    Dim frmAppointmentSchedulerType As New frmMSTAppointmentScheduler
                    frmAppointmentSchedulerType.blnModify = True
                    frmAppointmentSchedulerType.FillAppointmentUpToIntervals()
                    frmAppointmentSchedulerType.txtAppointmentType.Tag = objAppointmentSchedulerType.AppointmentSchedulerTypeID
                    frmAppointmentSchedulerType.txtAppointmentType.Text = Trim(trvAppointmentTypes.SelectedNode.Text)
                    frmAppointmentSchedulerType.numAppointmentUpTo.Value = objAppointmentSchedulerType.AppointmentUpToDuration
                    Select Case objAppointmentSchedulerType.AppointmentUpToDurationType
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
                            frmAppointmentSchedulerType.cmbAppointmentUpToInterval.Text = "Days"
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
                            frmAppointmentSchedulerType.cmbAppointmentUpToInterval.Text = "Weeks"
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
                            frmAppointmentSchedulerType.cmbAppointmentUpToInterval.Text = "Months"
                    End Select
                    frmAppointmentSchedulerType.pnlDaily.Visible = False
                    frmAppointmentSchedulerType.pnlWeekly.Visible = False
                    frmAppointmentSchedulerType.pnlMonthly.Visible = False
                    Select Case objAppointmentSchedulerType.AppointmentIntervalType
                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Daily
                            frmAppointmentSchedulerType.optDaily.Checked = True
                            frmAppointmentSchedulerType.pnlDaily.Visible = True
                            frmAppointmentSchedulerType.numDailyFrequency.Value = objAppointmentSchedulerType.AppointmentInterval

                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
                            frmAppointmentSchedulerType.optWeekly.Checked = True
                            frmAppointmentSchedulerType.pnlWeekly.Visible = True

                            If objAppointmentSchedulerType.AppointmentInterval Mod 2 = 0 Then
                                frmAppointmentSchedulerType.chkMon.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 3 = 0 Then
                                frmAppointmentSchedulerType.chkTue.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 5 = 0 Then
                                frmAppointmentSchedulerType.chkWed.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 7 = 0 Then
                                frmAppointmentSchedulerType.chkThu.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 11 = 0 Then
                                frmAppointmentSchedulerType.chkFri.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 13 = 0 Then
                                frmAppointmentSchedulerType.chkSat.Checked = True
                            End If
                            If objAppointmentSchedulerType.AppointmentInterval Mod 17 = 0 Then
                                frmAppointmentSchedulerType.chkSun.Checked = True
                            End If
                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
                            frmAppointmentSchedulerType.optMonthly.Checked = True
                            frmAppointmentSchedulerType.pnlMonthly.Visible = True
                            frmAppointmentSchedulerType.numMonthlyFrequency.Value = objAppointmentSchedulerType.AppointmentInterval
                    End Select
                    frmAppointmentSchedulerType.numAppointmentDuration.Value = objAppointmentSchedulerType.AppointmentDuration
                    frmAppointmentSchedulerType.picColor.BackColor = Color.FromArgb(CType(objAppointmentSchedulerType.ColorCode, Integer))
                    objAppointmentSchedulerType = Nothing

                    frmAppointmentSchedulerType.ShowDialog(IIf(IsNothing(frmAppointmentSchedulerType.Parent), Me, frmAppointmentSchedulerType.Parent))
                    frmAppointmentSchedulerType.Dispose()
                    frmAppointmentSchedulerType = Nothing
                    Call Fill_AppointmentSchedulerTypes()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            ''''''''IF Statement (i.e.,If not IsNothing(trvAppointmentTypes.SelectedNode) Then)Added by Anil on 05/10/2007 at 3:30 p.m.
            ''''''''This code is added because the application was giving Error:"NullReferenceException" on click of Modify Button.
            If Not IsNothing(trvAppointmentTypes.SelectedNode) Then
                If Not (trvAppointmentTypes.SelectedNode Is trvAppointmentTypes.Nodes(0)) Then
                    If MessageBox.Show("Are you sure, you want to delete " & trvAppointmentTypes.SelectedNode.Text & " Appointment Scheduler Type?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Dim objAppointmentSchedulerType As New clsAppointmentScheduler
                        If objAppointmentSchedulerType.DeleteAppointmentSchedulerType(Trim(trvAppointmentTypes.SelectedNode.Text)) = False Then
                            MessageBox.Show("Unable to delete " & trvAppointmentTypes.SelectedNode.Text, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            objAppointmentSchedulerType = Nothing
                            Exit Sub
                        End If
                        objAppointmentSchedulerType = Nothing
                        Call Fill_AppointmentSchedulerTypes()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            Call Fill_AppointmentSchedulerTypes()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Add"
                    Call AddCategory()
                Case "Modify"
                    Call UpdateCategory()
                Case "Delete"
                    Call DeleteCategory()
                Case "Refresh"
                    Call RefreshCategory()
                Case "Close"
                    Call FormClose()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   
    Private Sub frmVWAppointmentScheduler_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'turn on ownerdraw
            c1Appointment.DrawMode = DrawModeEnum.OwnerDraw

            Call Fill_AppointmentSchedulerTypes()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
