<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRxFillNotifications
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpToDate, dtpFrom}
            Dim cntControls() As System.Windows.Forms.Control = {dtpToDate, dtpFrom}
            Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cntListmenuStrip}

            components.Dispose()
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try



            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If



            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                End If
            End If

            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                End If
            End If

        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRxFillNotifications))
        Me.pnlToostrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gloRxRequests = New gloUserControlLibrary.gloRxFillNotification()
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlDateRange = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ImgLstFlex = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlDateRangeMain = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlDateRange.SuspendLayout()
        Me.pnlDateRangeMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(1071, 56)
        Me.pnlToostrip.TabIndex = 17
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnRefresh, Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1071, 53)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlbbtnRefresh
        '
        Me.tlbbtnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnRefresh.Image = CType(resources.GetObject("tlbbtnRefresh.Image"), System.Drawing.Image)
        Me.tlbbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnRefresh.Name = "tlbbtnRefresh"
        Me.tlbbtnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tlbbtnRefresh.Text = "&Refresh"
        Me.tlbbtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnRefresh.ToolTipText = "Refresh Pending RxFill Notifications"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.gloRxRequests)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 201)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(1071, 612)
        Me.pnlMain.TabIndex = 18
        '
        'gloRxRequests
        '
        Me.gloRxRequests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloRxRequests.dtRxFillReqs = Nothing
        Me.gloRxRequests.Location = New System.Drawing.Point(3, 0)
        Me.gloRxRequests.MessageID = Nothing
        Me.gloRxRequests.Name = "gloRxRequests"
        Me.gloRxRequests.PatientID = CType(0, Long)
        Me.gloRxRequests.PrescriberOrderNumber = CType(0, Long)
        Me.gloRxRequests.rxRequestMsgID = ""
        Me.gloRxRequests.Size = New System.Drawing.Size(1065, 609)
        Me.gloRxRequests.SSMessageData = Nothing
        Me.gloRxRequests.SSRxFillRequest = Nothing
        Me.gloRxRequests.TabIndex = 0
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'pnlDateRange
        '
        Me.pnlDateRange.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDateRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDateRange.Controls.Add(Me.Label3)
        Me.pnlDateRange.Controls.Add(Me.dtpToDate)
        Me.pnlDateRange.Controls.Add(Me.lblToDate)
        Me.pnlDateRange.Controls.Add(Me.dtpFrom)
        Me.pnlDateRange.Controls.Add(Me.lblFromDate)
        Me.pnlDateRange.Controls.Add(Me.Label9)
        Me.pnlDateRange.Controls.Add(Me.Label4)
        Me.pnlDateRange.Controls.Add(Me.Label5)
        Me.pnlDateRange.Controls.Add(Me.Label6)
        Me.pnlDateRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDateRange.Location = New System.Drawing.Point(3, 0)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(1065, 24)
        Me.pnlDateRange.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1064, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "label3"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(244, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpToDate.TabIndex = 39
        '
        'lblToDate
        '
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblToDate.Location = New System.Drawing.Point(207, 1)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(37, 22)
        Me.lblToDate.TabIndex = 38
        Me.lblToDate.Text = "To"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(109, 1)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtpFrom.TabIndex = 37
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFromDate.Location = New System.Drawing.Point(60, 1)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(49, 22)
        Me.lblFromDate.TabIndex = 36
        Me.lblFromDate.Text = "From"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 22)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Date :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 22)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(0, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1065, 1)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1065, 1)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "label2"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'ImgLstFlex
        '
        Me.ImgLstFlex.ImageStream = CType(resources.GetObject("ImgLstFlex.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgLstFlex.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgLstFlex.Images.SetKeyName(0, "Rx Status.ico")
        '
        'pnlDateRangeMain
        '
        Me.pnlDateRangeMain.Controls.Add(Me.pnlDateRange)
        Me.pnlDateRangeMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDateRangeMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlDateRangeMain.Name = "pnlDateRangeMain"
        Me.pnlDateRangeMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDateRangeMain.Size = New System.Drawing.Size(1071, 27)
        Me.pnlDateRangeMain.TabIndex = 19
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.Label12)
        Me.pnlGrid.Controls.Add(Me.Label11)
        Me.pnlGrid.Controls.Add(Me.Label10)
        Me.pnlGrid.Controls.Add(Me.Label13)
        Me.pnlGrid.Controls.Add(Me._Flex)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGrid.Location = New System.Drawing.Point(0, 83)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlGrid.Size = New System.Drawing.Size(1071, 115)
        Me.pnlGrid.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 114)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1063, 1)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1063, 1)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 115)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1067, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 115)
        Me.Label13.TabIndex = 45
        '
        '_Flex
        '
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me._Flex.ContextMenuStrip = Me.cntListmenuStrip
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(3, 0)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(1065, 115)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 18
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 198)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1071, 3)
        Me.Splitter1.TabIndex = 21
        Me.Splitter1.TabStop = False
        '
        'frmRxFillNotifications
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1071, 813)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlDateRangeMain)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRxFillNotifications"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RxFill Notifications"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRangeMain.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ImgLstFlex As System.Windows.Forms.ImageList
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlDateRangeMain As System.Windows.Forms.Panel
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents gloRxRequests As gloUserControlLibrary.gloRxFillNotification
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tlbbtnRefresh As System.Windows.Forms.ToolStripButton
End Class
