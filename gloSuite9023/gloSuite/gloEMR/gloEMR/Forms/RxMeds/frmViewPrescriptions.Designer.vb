<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewPrescriptions
    Inherits System.Windows.Forms.Form

    Private Shared frm As frmViewPrescriptions

    Public Shared Function GetInstance(ByVal ProviderID As Int64, ByVal PrescriberID As Int64) As frmViewPrescriptions
        If frm Is Nothing Then
            frm = New frmViewPrescriptions(ProviderID, PrescriberID)
        End If

        Return frm
    End Function
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
            frm = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewPrescriptions))
        Me.pnlToostrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlRefReqFlexGrid = New System.Windows.Forms.Panel()
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlDateRange = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbMethod = New System.Windows.Forms.ComboBox()
        Me.lblMethod = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkDateFilter = New System.Windows.Forms.CheckBox()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ImgLstFlex = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnl_trv = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.trvPrescribers = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Prescriber = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnlCancelProgress = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlwbBrowser = New System.Windows.Forms.Panel()
        Me.lblNoResponses = New System.Windows.Forms.Label()
        Me.requestViewer = New System.Windows.Forms.WebBrowser()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlRefReqFlexGrid.SuspendLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDateRange.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_trv.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCancelProgress.SuspendLayout()
        Me.pnlwbBrowser.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToostrip
        '
        Me.pnlToostrip.AutoSize = True
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(1168, 53)
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1168, 53)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
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
        'pnlRefReqFlexGrid
        '
        Me.pnlRefReqFlexGrid.Controls.Add(Me._Flex)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label8)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label7)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label2)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label1)
        Me.pnlRefReqFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRefReqFlexGrid.Location = New System.Drawing.Point(213, 83)
        Me.pnlRefReqFlexGrid.Name = "pnlRefReqFlexGrid"
        Me.pnlRefReqFlexGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlRefReqFlexGrid.Size = New System.Drawing.Size(955, 247)
        Me.pnlRefReqFlexGrid.TabIndex = 18
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
        Me._Flex.Location = New System.Drawing.Point(4, 1)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(947, 245)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 2
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(951, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 245)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 245)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(949, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 246)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(949, 1)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "label2"
        '
        'pnlDateRange
        '
        Me.pnlDateRange.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDateRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDateRange.Controls.Add(Me.Label3)
        Me.pnlDateRange.Controls.Add(Me.chkCancelled)
        Me.pnlDateRange.Controls.Add(Me.Label9)
        Me.pnlDateRange.Controls.Add(Me.cmbMethod)
        Me.pnlDateRange.Controls.Add(Me.lblMethod)
        Me.pnlDateRange.Controls.Add(Me.dtpToDate)
        Me.pnlDateRange.Controls.Add(Me.lblToDate)
        Me.pnlDateRange.Controls.Add(Me.dtpFrom)
        Me.pnlDateRange.Controls.Add(Me.chkDateFilter)
        Me.pnlDateRange.Controls.Add(Me.lblFromDate)
        Me.pnlDateRange.Controls.Add(Me.Label4)
        Me.pnlDateRange.Controls.Add(Me.Label5)
        Me.pnlDateRange.Controls.Add(Me.Label6)
        Me.pnlDateRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDateRange.Location = New System.Drawing.Point(3, 3)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(949, 24)
        Me.pnlDateRange.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(948, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "label3"
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.Color.Transparent
        Me.chkCancelled.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkCancelled.Location = New System.Drawing.Point(581, 1)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.Size = New System.Drawing.Size(77, 22)
        Me.chkCancelled.TabIndex = 47
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(537, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 22)
        Me.Label9.TabIndex = 51
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMethod
        '
        Me.cmbMethod.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbMethod.FormattingEnabled = True
        Me.cmbMethod.Location = New System.Drawing.Point(416, 1)
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Size = New System.Drawing.Size(121, 22)
        Me.cmbMethod.TabIndex = 45
        '
        'lblMethod
        '
        Me.lblMethod.BackColor = System.Drawing.Color.Transparent
        Me.lblMethod.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMethod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMethod.Location = New System.Drawing.Point(345, 1)
        Me.lblMethod.Name = "lblMethod"
        Me.lblMethod.Size = New System.Drawing.Size(71, 22)
        Me.lblMethod.TabIndex = 50
        Me.lblMethod.Text = "Method"
        Me.lblMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(247, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpToDate.TabIndex = 39
        '
        'lblToDate
        '
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblToDate.Location = New System.Drawing.Point(210, 1)
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
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(112, 1)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtpFrom.TabIndex = 37
        '
        'chkDateFilter
        '
        Me.chkDateFilter.BackColor = System.Drawing.Color.Transparent
        Me.chkDateFilter.Checked = True
        Me.chkDateFilter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDateFilter.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDateFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDateFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkDateFilter.Location = New System.Drawing.Point(58, 1)
        Me.chkDateFilter.Name = "chkDateFilter"
        Me.chkDateFilter.Size = New System.Drawing.Size(54, 22)
        Me.chkDateFilter.TabIndex = 48
        Me.chkDateFilter.Text = "Date"
        Me.chkDateFilter.UseVisualStyleBackColor = False
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFromDate.Location = New System.Drawing.Point(1, 1)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(57, 22)
        Me.lblFromDate.TabIndex = 36
        Me.lblFromDate.Text = "From"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.Label5.Size = New System.Drawing.Size(949, 1)
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
        Me.Label6.Size = New System.Drawing.Size(949, 1)
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
        Me.ImgLstFlex.Images.SetKeyName(1, "CancelRx.ico")
        Me.ImgLstFlex.Images.SetKeyName(2, "CancelRxNotification.ico")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlDateRange)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(213, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(955, 30)
        Me.Panel1.TabIndex = 19
        '
        'pnl_trv
        '
        Me.pnl_trv.Controls.Add(Me.Panel5)
        Me.pnl_trv.Controls.Add(Me.Panel6)
        Me.pnl_trv.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_trv.Location = New System.Drawing.Point(0, 53)
        Me.pnl_trv.Name = "pnl_trv"
        Me.pnl_trv.Size = New System.Drawing.Size(213, 452)
        Me.pnl_trv.TabIndex = 20
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label39)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.trvPrescribers)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(213, 422)
        Me.Panel5.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(4, 418)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(208, 1)
        Me.Label39.TabIndex = 8
        Me.Label39.Text = "label2"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 418)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "label4"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(212, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 418)
        Me.Label41.TabIndex = 6
        Me.Label41.Text = "label3"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(210, 1)
        Me.Label42.TabIndex = 5
        Me.Label42.Text = "label1"
        '
        'trvPrescribers
        '
        Me.trvPrescribers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPrescribers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPrescribers.ForeColor = System.Drawing.Color.Black
        Me.trvPrescribers.ImageIndex = 0
        Me.trvPrescribers.ImageList = Me.ImageList1
        Me.trvPrescribers.ItemHeight = 20
        Me.trvPrescribers.Location = New System.Drawing.Point(3, 0)
        Me.trvPrescribers.Name = "trvPrescribers"
        Me.trvPrescribers.SelectedImageIndex = 0
        Me.trvPrescribers.Size = New System.Drawing.Size(210, 419)
        Me.trvPrescribers.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Approved Rx.ico")
        Me.ImageList1.Images.SetKeyName(1, "Denied Prescription.ico")
        Me.ImageList1.Images.SetKeyName(2, "Denied Rx to Follow New Rx.ico")
        Me.ImageList1.Images.SetKeyName(3, "Provider.ico")
        Me.ImageList1.Images.SetKeyName(4, "Cancel.png")
        Me.ImageList1.Images.SetKeyName(5, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(6, "Bullet06.ico")
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(213, 30)
        Me.Panel6.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lbl_Prescriber)
        Me.Panel2.Controls.Add(Me.Label43)
        Me.Panel2.Controls.Add(Me.Label44)
        Me.Panel2.Controls.Add(Me.Label45)
        Me.Panel2.Controls.Add(Me.Label46)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(210, 24)
        Me.Panel2.TabIndex = 1
        '
        'lbl_Prescriber
        '
        Me.lbl_Prescriber.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Prescriber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Prescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Prescriber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_Prescriber.Location = New System.Drawing.Point(1, 1)
        Me.lbl_Prescriber.Name = "lbl_Prescriber"
        Me.lbl_Prescriber.Size = New System.Drawing.Size(208, 22)
        Me.lbl_Prescriber.TabIndex = 0
        Me.lbl_Prescriber.Text = " Prescriber "
        Me.lbl_Prescriber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(208, 1)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label2"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 23)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(209, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 23)
        Me.Label45.TabIndex = 6
        Me.Label45.Text = "label3"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(210, 1)
        Me.Label46.TabIndex = 5
        Me.Label46.Text = "label1"
        '
        'pnlCancelProgress
        '
        Me.pnlCancelProgress.BackColor = System.Drawing.Color.White
        Me.pnlCancelProgress.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlCancelProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCancelProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancelProgress.Controls.Add(Me.Label28)
        Me.pnlCancelProgress.Controls.Add(Me.Label35)
        Me.pnlCancelProgress.Location = New System.Drawing.Point(434, 212)
        Me.pnlCancelProgress.Name = "pnlCancelProgress"
        Me.pnlCancelProgress.Size = New System.Drawing.Size(301, 80)
        Me.pnlCancelProgress.TabIndex = 21
        Me.pnlCancelProgress.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(19, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(119, 19)
        Me.Label28.TabIndex = 61
        Me.Label28.Text = "Please wait..."
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(20, 46)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(227, 16)
        Me.Label35.TabIndex = 61
        Me.Label35.Text = "Sending Rx cancellation request..."
        '
        'pnlwbBrowser
        '
        Me.pnlwbBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlwbBrowser.Controls.Add(Me.lblNoResponses)
        Me.pnlwbBrowser.Controls.Add(Me.requestViewer)
        Me.pnlwbBrowser.Controls.Add(Me.Panel14)
        Me.pnlwbBrowser.Controls.Add(Me.Label96)
        Me.pnlwbBrowser.Controls.Add(Me.Label95)
        Me.pnlwbBrowser.Controls.Add(Me.Label94)
        Me.pnlwbBrowser.Controls.Add(Me.Label90)
        Me.pnlwbBrowser.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlwbBrowser.Location = New System.Drawing.Point(213, 333)
        Me.pnlwbBrowser.Name = "pnlwbBrowser"
        Me.pnlwbBrowser.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlwbBrowser.Size = New System.Drawing.Size(955, 172)
        Me.pnlwbBrowser.TabIndex = 23
        Me.pnlwbBrowser.Visible = False
        '
        'lblNoResponses
        '
        Me.lblNoResponses.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNoResponses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNoResponses.Font = New System.Drawing.Font("Tahoma", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoResponses.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.lblNoResponses.Location = New System.Drawing.Point(4, 26)
        Me.lblNoResponses.Name = "lblNoResponses"
        Me.lblNoResponses.Size = New System.Drawing.Size(947, 142)
        Me.lblNoResponses.TabIndex = 238
        Me.lblNoResponses.Text = "No response found"
        Me.lblNoResponses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNoResponses.Visible = False
        '
        'requestViewer
        '
        Me.requestViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.requestViewer.Location = New System.Drawing.Point(4, 26)
        Me.requestViewer.MinimumSize = New System.Drawing.Size(20, 20)
        Me.requestViewer.Name = "requestViewer"
        Me.requestViewer.Size = New System.Drawing.Size(947, 142)
        Me.requestViewer.TabIndex = 15
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_2007Header1
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label33)
        Me.Panel14.Controls.Add(Me.Label34)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(4, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(947, 25)
        Me.Panel14.TabIndex = 237
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(0, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(947, 1)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(947, 25)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = " Response Details"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(951, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 167)
        Me.Label96.TabIndex = 14
        Me.Label96.Text = "label4"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(3, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 167)
        Me.Label95.TabIndex = 13
        Me.Label95.Text = "label4"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label94.Location = New System.Drawing.Point(3, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(949, 1)
        Me.Label94.TabIndex = 12
        Me.Label94.Text = "label2"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label90.Location = New System.Drawing.Point(3, 168)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(949, 1)
        Me.Label90.TabIndex = 11
        Me.Label90.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(213, 330)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(955, 3)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh Prescriptions List"
        '
        'frmViewPrescriptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1168, 505)
        Me.Controls.Add(Me.pnlCancelProgress)
        Me.Controls.Add(Me.pnlRefReqFlexGrid)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlwbBrowser)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_trv)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewPrescriptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prescriptions"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlRefReqFlexGrid.ResumeLayout(False)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnl_trv.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCancelProgress.ResumeLayout(False)
        Me.pnlCancelProgress.PerformLayout()
        Me.pnlwbBrowser.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlRefReqFlexGrid As System.Windows.Forms.Panel
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ImgLstFlex As System.Windows.Forms.ImageList
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Friend WithEvents cmbMethod As System.Windows.Forms.ComboBox
    Friend WithEvents chkDateFilter As System.Windows.Forms.CheckBox
    Friend WithEvents lblMethod As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnl_trv As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents trvPrescribers As System.Windows.Forms.TreeView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Prescriber As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlCancelProgress As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlwbBrowser As System.Windows.Forms.Panel
    Friend WithEvents requestViewer As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents lblNoResponses As System.Windows.Forms.Label
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
End Class
