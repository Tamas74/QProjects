Public Class frmModifyAppointment
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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuSetAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmAppointmentTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AppointmentCalendar As ComponentGo.Calendars.ComboCalendar
    Friend WithEvents tlsCalendar As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsDay As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlsWeek As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlsMonth As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsToday As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_ModifyAppointment As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnSetAppointment As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblDoctorName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmModifyAppointment))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.AppointmentCalendar = New ComponentGo.Calendars.ComboCalendar
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuSetAppointment = New System.Windows.Forms.MenuItem
        Me.tlsCalendar = New System.Windows.Forms.ToolStrip
        Me.tlsToday = New System.Windows.Forms.ToolStripButton
        Me.tlsDay = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tlsWeek = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tlsMonth = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblDoctorName = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.tmAppointmentTime = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_ModifyAppointment = New System.Windows.Forms.ToolStrip
        Me.ts_btnSetAppointment = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.tlsCalendar.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_ModifyAppointment.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.AppointmentCalendar)
        Me.pnlMain.Controls.Add(Me.tlsCalendar)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(614, 445)
        Me.pnlMain.TabIndex = 1
        '
        'AppointmentCalendar
        '
        Me.AppointmentCalendar.AmPmMode = ComponentGo.Calendars.AmPmMode.AmPm
        Me.AppointmentCalendar.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AppointmentCalendar.ContextMenu = Me.ContextMenu1
        Me.AppointmentCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AppointmentCalendar.EnableEditInPlace = False
        Me.AppointmentCalendar.Location = New System.Drawing.Point(0, 58)
        Me.AppointmentCalendar.Name = "AppointmentCalendar"
        Me.AppointmentCalendar.Size = New System.Drawing.Size(614, 387)
        Me.AppointmentCalendar.TabIndex = 5
        Me.AppointmentCalendar.ToolbarColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.AppointmentCalendar.ToolbarColorSecond = System.Drawing.Color.DeepSkyBlue
        Me.AppointmentCalendar.ToolbarVisible = False
        Me.AppointmentCalendar.Views = CType(((ComponentGo.Calendars.CalendarView.Daily Or ComponentGo.Calendars.CalendarView.Weekly) _
                    Or ComponentGo.Calendars.CalendarView.Monthly), ComponentGo.Calendars.CalendarView)
        '
        '
        '
        Me.AppointmentCalendar.WeeklyCalendar.AllowWeekChange = False
        Me.AppointmentCalendar.WeeklyCalendar.BackColor = System.Drawing.Color.LawnGreen
        Me.AppointmentCalendar.WeeklyCalendar.DaysLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.DaysLabelColorSecond = System.Drawing.Color.Orange
        Me.AppointmentCalendar.WeeklyCalendar.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppointmentCalendar.WeeklyCalendar.ForeColor = System.Drawing.Color.Black
        Me.AppointmentCalendar.WeeklyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.AppointmentCalendar.WeeklyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.AppointmentCalendar.WeeklyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.AppointmentCalendar.WeeklyCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AppointmentCalendar.WeeklyCalendar.SelectedAppointmentColor = System.Drawing.Color.SkyBlue
        Me.AppointmentCalendar.WeeklyCalendar.SelectedAppointmentTextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.SelectedColor = System.Drawing.Color.Tomato
        Me.AppointmentCalendar.WeeklyCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AppointmentCalendar.WeeklyCalendar.TodayMarkerColor = System.Drawing.Color.Blue
        Me.AppointmentCalendar.WeeklyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.AppointmentCalendar.WeeklyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSetAppointment})
        '
        'mnuSetAppointment
        '
        Me.mnuSetAppointment.Index = 0
        Me.mnuSetAppointment.Text = "Set Appointment"
        '
        'tlsCalendar
        '
        Me.tlsCalendar.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsCalendar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsToday, Me.tlsDay, Me.ToolStripSeparator1, Me.tlsWeek, Me.ToolStripSeparator2, Me.tlsMonth})
        Me.tlsCalendar.Location = New System.Drawing.Point(0, 33)
        Me.tlsCalendar.Name = "tlsCalendar"
        Me.tlsCalendar.Size = New System.Drawing.Size(614, 25)
        Me.tlsCalendar.TabIndex = 6
        Me.tlsCalendar.Text = "ToolStrip1"
        '
        'tlsToday
        '
        Me.tlsToday.Image = CType(resources.GetObject("tlsToday.Image"), System.Drawing.Image)
        Me.tlsToday.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsToday.Name = "tlsToday"
        Me.tlsToday.Size = New System.Drawing.Size(67, 22)
        Me.tlsToday.Text = "Today"
        '
        'tlsDay
        '
        Me.tlsDay.ForeColor = System.Drawing.Color.Navy
        Me.tlsDay.Image = CType(resources.GetObject("tlsDay.Image"), System.Drawing.Image)
        Me.tlsDay.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDay.Name = "tlsDay"
        Me.tlsDay.Size = New System.Drawing.Size(52, 22)
        Me.tlsDay.Text = "Day"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tlsWeek
        '
        Me.tlsWeek.ForeColor = System.Drawing.Color.Navy
        Me.tlsWeek.Image = CType(resources.GetObject("tlsWeek.Image"), System.Drawing.Image)
        Me.tlsWeek.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsWeek.Name = "tlsWeek"
        Me.tlsWeek.Size = New System.Drawing.Size(63, 22)
        Me.tlsWeek.Text = "Week"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tlsMonth
        '
        Me.tlsMonth.ForeColor = System.Drawing.Color.Navy
        Me.tlsMonth.Image = CType(resources.GetObject("tlsMonth.Image"), System.Drawing.Image)
        Me.tlsMonth.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsMonth.Name = "tlsMonth"
        Me.tlsMonth.Size = New System.Drawing.Size(66, 22)
        Me.tlsMonth.Text = "Month"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblDoctorName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.tmAppointmentTime)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(614, 33)
        Me.Panel1.TabIndex = 0
        '
        'lblDoctorName
        '
        Me.lblDoctorName.AutoSize = True
        Me.lblDoctorName.Location = New System.Drawing.Point(88, 8)
        Me.lblDoctorName.Name = "lblDoctorName"
        Me.lblDoctorName.Size = New System.Drawing.Size(0, 14)
        Me.lblDoctorName.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 14)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Doctor Name"
        '
        'tmAppointmentTime
        '
        Me.tmAppointmentTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.tmAppointmentTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.tmAppointmentTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.tmAppointmentTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.tmAppointmentTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.tmAppointmentTime.Enabled = False
        Me.tmAppointmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmAppointmentTime.Location = New System.Drawing.Point(478, 4)
        Me.tmAppointmentTime.Name = "tmAppointmentTime"
        Me.tmAppointmentTime.ShowUpDown = True
        Me.tmAppointmentTime.Size = New System.Drawing.Size(126, 22)
        Me.tmAppointmentTime.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(349, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Appointment Time :"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_ModifyAppointment)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(614, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_ModifyAppointment
        '
        Me.tlsp_ModifyAppointment.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ModifyAppointment.BackgroundImage = CType(resources.GetObject("tlsp_ModifyAppointment.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_ModifyAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ModifyAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsp_ModifyAppointment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ModifyAppointment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSetAppointment, Me.ts_btnClose})
        Me.tlsp_ModifyAppointment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ModifyAppointment.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_ModifyAppointment.Name = "tlsp_ModifyAppointment"
        Me.tlsp_ModifyAppointment.Size = New System.Drawing.Size(614, 53)
        Me.tlsp_ModifyAppointment.TabIndex = 0
        Me.tlsp_ModifyAppointment.Text = "toolStrip1"
        '
        'ts_btnSetAppointment
        '
        Me.ts_btnSetAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_btnSetAppointment.Image = CType(resources.GetObject("ts_btnSetAppointment.Image"), System.Drawing.Image)
        Me.ts_btnSetAppointment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSetAppointment.Name = "ts_btnSetAppointment"
        Me.ts_btnSetAppointment.Size = New System.Drawing.Size(120, 50)
        Me.ts_btnSetAppointment.Tag = "SetAppointment"
        Me.ts_btnSetAppointment.Text = "&Set Appointment"
        Me.ts_btnSetAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(47, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmModifyAppointment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(614, 498)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmModifyAppointment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modify Appointment"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tlsCalendar.ResumeLayout(False)
        Me.tlsCalendar.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_ModifyAppointment.ResumeLayout(False)
        Me.tlsp_ModifyAppointment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CloseSetAppointment()

        Try
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AppointmentCalendar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppointmentCalendar.Click
        Try
            AppointmentCalendar.MonthlyCalendar.TodayMarkerVisible = False
            If AppointmentCalendar.Appointments.SelectedCount <= 0 Then
                ts_btnSetAppointment.Enabled = True
                mnuSetAppointment.Enabled = True
            Else
                ts_btnSetAppointment.Enabled = False
                mnuSetAppointment.Enabled = False
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuSetAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetAppointment.Click
        Try
            btnSetAppointment()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSetAppointment()

        Try
            'Check Appointment Module Level
            If gnAppointmentModuleLevel = 0 Then
                If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                    If IsNothing(AppointmentCalendar.DailyCalendar.SelectedDateBegin.Date) = False Then
                        frmAppointment.strAppointmentDate = AppointmentCalendar.DailyCalendar.SelectedDateBegin.Date
                        frmAppointment.strAppointmentTime = Format(AppointmentCalendar.DailyCalendar.SelectedDateBegin, "Medium Time")
                    End If
                Else
                    If IsNothing(AppointmentCalendar.DailyCalendar.SelectedDateBegin.Date) = False Then
                        frmAppointment.strAppointmentDate = AppointmentCalendar.SelectedDateBegin.Date
                        frmAppointment.strAppointmentTime = Format(tmAppointmentTime.Value, "Medium Time")
                    End If
                End If
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
                Exit Sub
            End If
            If AppointmentCalendar.Appointments.SelectedCount <= 0 Then
                'Check Clinic Timing
                Dim nAppointmentTime As Long
                Dim nStartTime As Long
                Dim nEndTime As Long

                nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
                nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
                If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                    nAppointmentTime = New TimeSpan(AppointmentCalendar.SelectedDateBegin.Hour, AppointmentCalendar.SelectedDateBegin.Minute, AppointmentCalendar.SelectedDateBegin.Second).Ticks
                Else
                    nAppointmentTime = New TimeSpan(tmAppointmentTime.Value.Hour, tmAppointmentTime.Value.Minute, tmAppointmentTime.Value.Second).Ticks
                End If

                'Check Appointment Module Level
                If gnAppointmentModuleLevel <> 0 Then
                    If nAppointmentTime < nStartTime Or nAppointmentTime > nEndTime Then
                        MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "So appointment is not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    'Check Doctor's Availability
                    Dim objDoctor As New clsDoctorHolidaySchedule
                    If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                        If objDoctor.IsDoctorAvailable(lblDoctorName.Text, AppointmentCalendar.SelectedDateBegin) = False Then
                            MessageBox.Show("Doctor is not available. So you can not add appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        frmAppointment.strAppointmentDate = AppointmentCalendar.DailyCalendar.SelectedDateBegin.Date
                        frmAppointment.strAppointmentTime = Format(AppointmentCalendar.DailyCalendar.SelectedDateBegin, "Medium Time")
                    Else
                        If objDoctor.IsDoctorAvailable(lblDoctorName.Text, AppointmentCalendar.SelectedDateBegin.Date & " " & Format(tmAppointmentTime.Value, "Medium Time")) = False Then
                            MessageBox.Show("Doctor is not available. So you can not add appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        frmAppointment.strAppointmentDate = AppointmentCalendar.SelectedDateBegin.Date
                        frmAppointment.strAppointmentTime = Format(tmAppointmentTime.Value, "Medium Time")
                    End If
                    objDoctor = Nothing
                End If
            Else
                MessageBox.Show("Appointment is already registered. You can not add new appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AppointmentCalendar_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            btnSetAppointment()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AppointmentCalendar_CurrentViewChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppointmentCalendar.CurrentViewChanged
        Try
            AppointmentCalendar.MonthlyCalendar.TodayMarkerVisible = False
            If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Then
                tmAppointmentTime.Enabled = False
            Else
                tmAppointmentTime.Enabled = True
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''''****Code till end is Added by Anil on 18/10/2007
    '''''''****These codes were added to resolve the bug no-370, Where on clicking the "Today" button of toolbar the Application was giving error.

    Private Sub frmModifyAppointment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AppointmentCalendar.ToolbarVisible = False
        AppointmentCalendar.FirstDateTime = Now.Date
    End Sub

    Private Sub tlsToday_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDay.Click
        Try
            AppointmentCalendar.MonthlyCalendar.TodayMarkerVisible = False
            AppointmentCalendar.DailyCalendar.Visible = True
            AppointmentCalendar.FirstDateTime = Now.Date
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlsWeek_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsWeek.Click
        If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Daily Or AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Monthly Then
            AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Weekly
        Else
            AppointmentCalendar.DailyCalendar.Visible = False
        End If
        AppointmentCalendar.WeeklyCalendar.TodayMarkerVisible = False
    End Sub

    Private Sub tlsMonth_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsMonth.Click
        AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Monthly
        AppointmentCalendar.MonthlyCalendar.TodayMarkerVisible = False
    End Sub

    Private Sub tlsToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsToday.Click
        If AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Weekly Then
            AppointmentCalendar.SelectedDateBegin = Today
            AppointmentCalendar.SelectedDateEnd = Today
            AppointmentCalendar.WeeklyCalendar.TodayMarkerVisible = True
            AppointmentCalendar.Focus()
            AppointmentCalendar.FirstDateTime = Now.Date
        ElseIf AppointmentCalendar.CurrentView = ComponentGo.Calendars.CalendarView.Monthly Then
            AppointmentCalendar.SelectedDateBegin = Today
            AppointmentCalendar.SelectedDateEnd = Today
            AppointmentCalendar.MonthlyCalendar.TodayMarkerVisible = True
            AppointmentCalendar.Focus()
            AppointmentCalendar.FirstDateTime = Now.Date
        End If
    End Sub

    Private Sub tlsp_ModifyAppointment_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ModifyAppointment.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "SetAppointment"
                    btnSetAppointment()
                Case "Close"
                    CloseSetAppointment()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub
End Class
