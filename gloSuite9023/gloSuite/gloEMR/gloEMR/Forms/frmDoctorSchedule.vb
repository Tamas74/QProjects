Public Class frmDoctorSchedule
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
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtFromDate, dtToDate, tmFromTime, tmToTime}
            Dim cntControls() As System.Windows.Forms.Control = {dtFromDate, dtToDate, tmFromTime, tmToTime}
            Dim CmpMControls() As System.Windows.Forms.ContextMenu = {ContextMenu1}
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

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



            If (IsNothing(CmpMControls) = False) Then
                If CmpMControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpMControls)
                End If
            End If

            If (IsNothing(CmpMControls) = False) Then
                If CmpMControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenu(CmpMControls)
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
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDoctor As System.Windows.Forms.ComboBox
    Friend WithEvents optCalendar As System.Windows.Forms.RadioButton
    Friend WithEvents optCustomize As System.Windows.Forms.RadioButton
    Friend WithEvents pnlCalendar As System.Windows.Forms.Panel
    Friend WithEvents pnlCustomize As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents HolidayCalendar As ComponentGo.Calendars.ComboCalendar
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuSetHoliday As System.Windows.Forms.MenuItem
    Friend WithEvents tmFromTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents tmToTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents mnuSetHolidatNotes As System.Windows.Forms.MenuItem
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDoctorSchedule))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.optCustomize = New System.Windows.Forms.RadioButton
        Me.optCalendar = New System.Windows.Forms.RadioButton
        Me.cmbDoctor = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlCalendar = New System.Windows.Forms.Panel
        Me.HolidayCalendar = New ComponentGo.Calendars.ComboCalendar
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuSetHoliday = New System.Windows.Forms.MenuItem
        Me.mnuSetHolidatNotes = New System.Windows.Forms.MenuItem
        Me.pnlCustomize = New System.Windows.Forms.Panel
        Me.tmToTime = New System.Windows.Forms.DateTimePicker
        Me.tmFromTime = New System.Windows.Forms.DateTimePicker
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtToDate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtFromDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnl_tls_ = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton
        Me.pnlTop.SuspendLayout()
        Me.pnlCalendar.SuspendLayout()
        Me.pnlCustomize.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.optCustomize)
        Me.pnlTop.Controls.Add(Me.optCalendar)
        Me.pnlTop.Controls.Add(Me.cmbDoctor)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(452, 60)
        Me.pnlTop.TabIndex = 2
        '
        'optCustomize
        '
        Me.optCustomize.Location = New System.Drawing.Point(194, 36)
        Me.optCustomize.Name = "optCustomize"
        Me.optCustomize.Size = New System.Drawing.Size(139, 18)
        Me.optCustomize.TabIndex = 3
        Me.optCustomize.Text = " C&ustomize"
        '
        'optCalendar
        '
        Me.optCalendar.Checked = True
        Me.optCalendar.Location = New System.Drawing.Point(106, 36)
        Me.optCalendar.Name = "optCalendar"
        Me.optCalendar.Size = New System.Drawing.Size(90, 18)
        Me.optCalendar.TabIndex = 2
        Me.optCalendar.TabStop = True
        Me.optCalendar.Text = "&Calendar"
        '
        'cmbDoctor
        '
        Me.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoctor.Location = New System.Drawing.Point(106, 5)
        Me.cmbDoctor.Name = "cmbDoctor"
        Me.cmbDoctor.Size = New System.Drawing.Size(222, 22)
        Me.cmbDoctor.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Doctor Name :"
        '
        'pnlCalendar
        '
        Me.pnlCalendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCalendar.Controls.Add(Me.HolidayCalendar)
        Me.pnlCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCalendar.Location = New System.Drawing.Point(0, 53)
        Me.pnlCalendar.Name = "pnlCalendar"
        Me.pnlCalendar.Size = New System.Drawing.Size(452, 374)
        Me.pnlCalendar.TabIndex = 3
        '
        'HolidayCalendar
        '
        Me.HolidayCalendar.AmPmMode = ComponentGo.Calendars.AmPmMode.AmPm
        Me.HolidayCalendar.BackColor = System.Drawing.Color.WhiteSmoke
        Me.HolidayCalendar.ContextMenu = Me.ContextMenu1
        Me.HolidayCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Monthly
        '
        '
        '
        Me.HolidayCalendar.DailyCalendar.BackColor = System.Drawing.Color.LawnGreen
        Me.HolidayCalendar.DailyCalendar.DateLabelFormat = "{0}"
        Me.HolidayCalendar.DailyCalendar.DaysLabelColor = System.Drawing.Color.Orange
        Me.HolidayCalendar.DailyCalendar.DaysLabelColorSecond = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.DailyCalendar.DaysWorkspaceColor = System.Drawing.Color.LightSteelBlue
        Me.HolidayCalendar.DailyCalendar.DaysWorkspaceColorSecond = System.Drawing.Color.DodgerBlue
        Me.HolidayCalendar.DailyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.HolidayCalendar.DailyCalendar.HoursLabelColor = System.Drawing.Color.Orange
        Me.HolidayCalendar.DailyCalendar.HoursLabelColorSecond = System.Drawing.Color.Linen
        Me.HolidayCalendar.DailyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.HolidayCalendar.DailyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HolidayCalendar.DailyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.HolidayCalendar.DailyCalendar.SelectedAppointmentColor = System.Drawing.Color.Brown
        Me.HolidayCalendar.DailyCalendar.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.DailyCalendar.SelectedDayColor = System.Drawing.Color.Tomato
        Me.HolidayCalendar.DailyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.HolidayCalendar.DailyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        Me.HolidayCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HolidayCalendar.EnableEditInPlace = False
        Me.HolidayCalendar.FirstDateTime = New Date(2005, 9, 19, 0, 0, 0, 0)
        Me.HolidayCalendar.Location = New System.Drawing.Point(0, 0)
        Me.HolidayCalendar.MaxDate = New Date(2200, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.HolidayCalendar.MonthlyCalendar.AlternativeMonthColor = System.Drawing.Color.GhostWhite
        Me.HolidayCalendar.MonthlyCalendar.DaysLabelColor = System.Drawing.Color.Orange
        Me.HolidayCalendar.MonthlyCalendar.DaysLabelColorSecond = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.MonthlyCalendar.FirstDate = New Date(2005, 9, 19, 0, 0, 0, 0)
        Me.HolidayCalendar.MonthlyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.HolidayCalendar.MonthlyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.HolidayCalendar.MonthlyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HolidayCalendar.MonthlyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.HolidayCalendar.MonthlyCalendar.SelectedAppointmentColor = System.Drawing.Color.Brown
        Me.HolidayCalendar.MonthlyCalendar.SelectedAppointmentTextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.MonthlyCalendar.SelectedColor = System.Drawing.Color.DarkOrange
        Me.HolidayCalendar.MonthlyCalendar.ShowAnalogClock = True
        Me.HolidayCalendar.MonthlyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.HolidayCalendar.MonthlyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        Me.HolidayCalendar.Name = "HolidayCalendar"
        Me.HolidayCalendar.Size = New System.Drawing.Size(450, 372)
        Me.HolidayCalendar.TabIndex = 2
        Me.HolidayCalendar.ToolbarColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.HolidayCalendar.ToolbarColorSecond = System.Drawing.Color.DeepSkyBlue
        Me.HolidayCalendar.Views = CType(((ComponentGo.Calendars.CalendarView.Daily Or ComponentGo.Calendars.CalendarView.Weekly) _
                    Or ComponentGo.Calendars.CalendarView.Monthly), ComponentGo.Calendars.CalendarView)
        Me.HolidayCalendar.VisibleDays = 35
        '
        '
        '
        Me.HolidayCalendar.WeeklyCalendar.BackColor = System.Drawing.Color.LawnGreen
        Me.HolidayCalendar.WeeklyCalendar.DaysLabelColor = System.Drawing.Color.Orange
        Me.HolidayCalendar.WeeklyCalendar.DaysLabelColorSecond = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.WeeklyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.HolidayCalendar.WeeklyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.HolidayCalendar.WeeklyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HolidayCalendar.WeeklyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.HolidayCalendar.WeeklyCalendar.SelectedAppointmentColor = System.Drawing.Color.Brown
        Me.HolidayCalendar.WeeklyCalendar.SelectedAppointmentTextColor = System.Drawing.SystemColors.ControlText
        Me.HolidayCalendar.WeeklyCalendar.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.HolidayCalendar.WeeklyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.HolidayCalendar.WeeklyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSetHoliday, Me.mnuSetHolidatNotes})
        '
        'mnuSetHoliday
        '
        Me.mnuSetHoliday.Index = 0
        Me.mnuSetHoliday.Text = "Set Holiday"
        '
        'mnuSetHolidatNotes
        '
        Me.mnuSetHolidatNotes.Index = 1
        Me.mnuSetHolidatNotes.Text = "Set Holiday with Notes"
        '
        'pnlCustomize
        '
        Me.pnlCustomize.BackColor = System.Drawing.Color.Transparent
        Me.pnlCustomize.Controls.Add(Me.tmToTime)
        Me.pnlCustomize.Controls.Add(Me.tmFromTime)
        Me.pnlCustomize.Controls.Add(Me.txtComments)
        Me.pnlCustomize.Controls.Add(Me.Label4)
        Me.pnlCustomize.Controls.Add(Me.dtToDate)
        Me.pnlCustomize.Controls.Add(Me.Label3)
        Me.pnlCustomize.Controls.Add(Me.dtFromDate)
        Me.pnlCustomize.Controls.Add(Me.Label2)
        Me.pnlCustomize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCustomize.Location = New System.Drawing.Point(0, 113)
        Me.pnlCustomize.Name = "pnlCustomize"
        Me.pnlCustomize.Size = New System.Drawing.Size(452, 314)
        Me.pnlCustomize.TabIndex = 4
        Me.pnlCustomize.Visible = False
        '
        'tmToTime
        '
        Me.tmToTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.tmToTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.tmToTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.tmToTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.tmToTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.tmToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmToTime.Location = New System.Drawing.Point(214, 43)
        Me.tmToTime.Name = "tmToTime"
        Me.tmToTime.ShowUpDown = True
        Me.tmToTime.Size = New System.Drawing.Size(106, 22)
        Me.tmToTime.TabIndex = 15
        '
        'tmFromTime
        '
        Me.tmFromTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.tmFromTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.tmFromTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.tmFromTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.tmFromTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.tmFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmFromTime.Location = New System.Drawing.Point(214, 12)
        Me.tmFromTime.Name = "tmFromTime"
        Me.tmFromTime.ShowUpDown = True
        Me.tmFromTime.Size = New System.Drawing.Size(106, 22)
        Me.tmFromTime.TabIndex = 14
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.White
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComments.Location = New System.Drawing.Point(8, 98)
        Me.txtComments.MaxLength = 255
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComments.Size = New System.Drawing.Size(430, 202)
        Me.txtComments.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Comments"
        '
        'dtToDate
        '
        Me.dtToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtToDate.CustomFormat = "MM/dd/yyyy"
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDate.Location = New System.Drawing.Point(94, 43)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(110, 22)
        Me.dtToDate.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "To Date :"
        '
        'dtFromDate
        '
        Me.dtFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFromDate.CustomFormat = "MM/dd/yyyy"
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDate.Location = New System.Drawing.Point(94, 12)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(110, 22)
        Me.dtFromDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From Date :"
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tls)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(452, 53)
        Me.pnl_tls_.TabIndex = 5
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = CType(resources.GetObject("tls.BackgroundImage"), System.Drawing.Image)
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok, Me.btn_tls_Cancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(452, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(36, 50)
        Me.btn_tls_Ok.Text = " &Ok"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = " Ok"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(59, 50)
        Me.btn_tls_Cancel.Text = " &Cancel"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Cancel.ToolTipText = "Cancel"
        '
        'frmDoctorSchedule
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(452, 427)
        Me.Controls.Add(Me.pnlCustomize)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlCalendar)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoctorSchedule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Doctor Busy / Holiday Schedule"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlCalendar.ResumeLayout(False)
        Me.pnlCustomize.ResumeLayout(False)
        Me.pnlCustomize.PerformLayout()
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public blnModify As Boolean

    Private Sub optCalendar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCalendar.Click
        Try
            pnlCalendar.Visible = True
            pnlCustomize.Visible = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub optCustomize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCustomize.Click
        Try
            pnlCalendar.Visible = False
            pnlCustomize.Visible = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuSetHoliday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetHoliday.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            btnOK_Click(sender, e)
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub Fill_Doctors()
        With cmbDoctor
            .Items.Clear()
            Dim clProviders As New Collection
            Dim objProvider As New clsProvider
            clProviders = objProvider.Fill_Providers
            objProvider.Dispose()
            objProvider = Nothing
            Dim nCount As Int16
            For nCount = 1 To clProviders.Count
                .Items.Add(clProviders.Item(nCount))
            Next
        End With
    End Sub



    Private Sub mnuSetHolidatNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetHolidatNotes.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            dtFromDate.Value = HolidayCalendar.SelectedDateBegin
            If HolidayCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                tmFromTime.Value = HolidayCalendar.SelectedDateBegin
                dtToDate.Value = HolidayCalendar.SelectedDateEnd
                tmToTime.Value = HolidayCalendar.SelectedDateEnd
            Else
                tmFromTime.Value = HolidayCalendar.SelectedDateBegin.Date & " 09:00:00 AM"
                dtToDate.Value = HolidayCalendar.SelectedDateEnd.AddDays(-1)
                tmToTime.Value = HolidayCalendar.SelectedDateEnd.Date & " 23:59:59"
            End If
            pnlCustomize.Visible = True
            pnlCalendar.Visible = False
            optCustomize.Checked = True
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Cancel.Click
        Try
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click
        Try
            If Trim(cmbDoctor.Text) = "" Then
                MessageBox.Show("Please select Doctor", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDoctor.Focus()
                Exit Sub
            End If
            If optCustomize.Checked = True Then
                If dtFromDate.Value.Date > dtToDate.Value.Date Then
                    MessageBox.Show("From Date must be less than or equal to To Date", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    dtFromDate.Focus()
                    Exit Sub
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim strDoctor As String
            Dim FromDate As DateTime
            Dim ToDate As DateTime
            Dim strComments As String

            strDoctor = cmbDoctor.Text
            If optCustomize.Checked = True Then
                strComments = txtComments.Text
                FromDate = dtFromDate.Value.Date & " " & Format(tmFromTime.Value, "Medium Time")
                ToDate = dtToDate.Value.Date & " " & Format(tmToTime.Value, "Medium Time")
            Else
                strComments = ""
                If HolidayCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                    FromDate = HolidayCalendar.SelectedDateBegin
                    ToDate = HolidayCalendar.SelectedDateEnd
                Else
                    FromDate = HolidayCalendar.SelectedDateBegin.Date & " 09:00:00 AM"
                    ToDate = HolidayCalendar.SelectedDateEnd.AddDays(-1) & " 23:59:59"
                End If
            End If
            Dim objSchedule As New clsDoctorHolidaySchedule
            If blnModify = False Then
                'To add Doctor Holiday
                If objSchedule.AddSchedule(strDoctor, FromDate, ToDate, strComments) = False Then
                    MessageBox.Show("Unable to add the Doctor History Shedule", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objSchedule = Nothing
                    Exit Sub
                End If
            Else
                'To Modify Doctor Holiday
                If objSchedule.UpdateSchedule(cmbDoctor.Tag, strDoctor, FromDate, ToDate, strComments) = False Then
                    MessageBox.Show("Unable to Update the Doctor History Shedule", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objSchedule = Nothing
                    Exit Sub
                End If
            End If
            objSchedule = Nothing
            Me.Close()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDoctorSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            HolidayCalendar.FirstDateTime = System.DateTime.Now
            'HolidayCalendar.DailyCalendar.SelectedDateBegin = System.DateTime.Now
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
