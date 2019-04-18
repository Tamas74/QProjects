<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_TaskInfo
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlTask = New System.Windows.Forms.Panel()
        Me.lblTaskHeader = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnlTaskDetailsMain = New System.Windows.Forms.Panel()
        Me.pnlrchtxt_TaskDescription = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rchtxt_TaskDescription = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlTaskDetails = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSubjectName = New System.Windows.Forms.Label()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.lblPriorityValue = New System.Windows.Forms.Label()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.lblDueDateValue = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.lblStartDateValue = New System.Windows.Forms.Label()
        Me.lblPriority = New System.Windows.Forms.Label()
        Me.lblStatusValue = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlTask.SuspendLayout()
        Me.pnlTaskDetailsMain.SuspendLayout()
        Me.pnlrchtxt_TaskDescription.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlTaskDetails.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTask
        '
        Me.pnlTask.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTask.Controls.Add(Me.lblTaskHeader)
        Me.pnlTask.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlTask.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTask.Controls.Add(Me.lbl_pnlRight)
        Me.pnlTask.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTask.Location = New System.Drawing.Point(0, 3)
        Me.pnlTask.Name = "pnlTask"
        Me.pnlTask.Size = New System.Drawing.Size(1255, 25)
        Me.pnlTask.TabIndex = 34
        '
        'lblTaskHeader
        '
        Me.lblTaskHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblTaskHeader.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTaskHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTaskHeader.Location = New System.Drawing.Point(1, 1)
        Me.lblTaskHeader.Name = "lblTaskHeader"
        Me.lblTaskHeader.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblTaskHeader.Size = New System.Drawing.Size(140, 23)
        Me.lblTaskHeader.TabIndex = 13
        Me.lblTaskHeader.Text = "Task Information"
        Me.lblTaskHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 24)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(1253, 1)
        Me.lbl_pnlBottom.TabIndex = 12
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlLeft.TabIndex = 11
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(1254, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlRight.TabIndex = 10
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(1255, 1)
        Me.lbl_pnlTop.TabIndex = 9
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlTaskDetailsMain
        '
        Me.pnlTaskDetailsMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTaskDetailsMain.Controls.Add(Me.pnlrchtxt_TaskDescription)
        Me.pnlTaskDetailsMain.Controls.Add(Me.pnlTaskDetails)
        Me.pnlTaskDetailsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTaskDetailsMain.Location = New System.Drawing.Point(0, 28)
        Me.pnlTaskDetailsMain.Name = "pnlTaskDetailsMain"
        Me.pnlTaskDetailsMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlTaskDetailsMain.Size = New System.Drawing.Size(1255, 72)
        Me.pnlTaskDetailsMain.TabIndex = 35
        '
        'pnlrchtxt_TaskDescription
        '
        Me.pnlrchtxt_TaskDescription.Controls.Add(Me.Panel4)
        Me.pnlrchtxt_TaskDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlrchtxt_TaskDescription.Location = New System.Drawing.Point(660, 3)
        Me.pnlrchtxt_TaskDescription.Name = "pnlrchtxt_TaskDescription"
        Me.pnlrchtxt_TaskDescription.Size = New System.Drawing.Size(595, 69)
        Me.pnlrchtxt_TaskDescription.TabIndex = 34
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.rchtxt_TaskDescription)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(595, 69)
        Me.Panel4.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(594, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 67)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "label4"
        '
        'rchtxt_TaskDescription
        '
        Me.rchtxt_TaskDescription.BackColor = System.Drawing.Color.White
        Me.rchtxt_TaskDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxt_TaskDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxt_TaskDescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchtxt_TaskDescription.ForeColor = System.Drawing.Color.Black
        Me.rchtxt_TaskDescription.Location = New System.Drawing.Point(1, 1)
        Me.rchtxt_TaskDescription.Name = "rchtxt_TaskDescription"
        Me.rchtxt_TaskDescription.ReadOnly = True
        Me.rchtxt_TaskDescription.Size = New System.Drawing.Size(594, 67)
        Me.rchtxt_TaskDescription.TabIndex = 33
        Me.rchtxt_TaskDescription.Text = ""
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 67)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(0, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(595, 1)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(595, 1)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "label1"
        '
        'pnlTaskDetails
        '
        Me.pnlTaskDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTaskDetails.Controls.Add(Me.Label3)
        Me.pnlTaskDetails.Controls.Add(Me.Label2)
        Me.pnlTaskDetails.Controls.Add(Me.Label4)
        Me.pnlTaskDetails.Controls.Add(Me.Label1)
        Me.pnlTaskDetails.Controls.Add(Me.lblSubjectName)
        Me.pnlTaskDetails.Controls.Add(Me.lblProvider)
        Me.pnlTaskDetails.Controls.Add(Me.lblEndDate)
        Me.pnlTaskDetails.Controls.Add(Me.lblPriorityValue)
        Me.pnlTaskDetails.Controls.Add(Me.lblSubject)
        Me.pnlTaskDetails.Controls.Add(Me.lblDueDateValue)
        Me.pnlTaskDetails.Controls.Add(Me.lblStartDate)
        Me.pnlTaskDetails.Controls.Add(Me.lblStartDateValue)
        Me.pnlTaskDetails.Controls.Add(Me.lblPriority)
        Me.pnlTaskDetails.Controls.Add(Me.lblStatusValue)
        Me.pnlTaskDetails.Controls.Add(Me.lblStatus)
        Me.pnlTaskDetails.Controls.Add(Me.lblProviderName)
        Me.pnlTaskDetails.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlTaskDetails.Location = New System.Drawing.Point(0, 3)
        Me.pnlTaskDetails.Name = "pnlTaskDetails"
        Me.pnlTaskDetails.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlTaskDetails.Size = New System.Drawing.Size(660, 69)
        Me.pnlTaskDetails.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(656, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 67)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 67)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(657, 1)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(657, 1)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "label2"
        '
        'lblSubjectName
        '
        Me.lblSubjectName.AutoEllipsis = True
        Me.lblSubjectName.AutoSize = True
        Me.lblSubjectName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubjectName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubjectName.Location = New System.Drawing.Point(80, 6)
        Me.lblSubjectName.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSubjectName.Name = "lblSubjectName"
        Me.lblSubjectName.Size = New System.Drawing.Size(45, 14)
        Me.lblSubjectName.TabIndex = 27
        Me.lblSubjectName.Text = "SName"
        Me.lblSubjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblProvider
        '
        Me.lblProvider.AutoSize = True
        Me.lblProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProvider.Location = New System.Drawing.Point(13, 27)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(66, 14)
        Me.lblProvider.TabIndex = 24
        Me.lblProvider.Text = "Provider :"
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblEndDate.Location = New System.Drawing.Point(373, 27)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(72, 14)
        Me.lblEndDate.TabIndex = 22
        Me.lblEndDate.Text = "Due Date :"
        '
        'lblPriorityValue
        '
        Me.lblPriorityValue.AutoEllipsis = True
        Me.lblPriorityValue.AutoSize = True
        Me.lblPriorityValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPriorityValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPriorityValue.Location = New System.Drawing.Point(447, 48)
        Me.lblPriorityValue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPriorityValue.Name = "lblPriorityValue"
        Me.lblPriorityValue.Size = New System.Drawing.Size(78, 14)
        Me.lblPriorityValue.TabIndex = 32
        Me.lblPriorityValue.Text = "Priority Value"
        Me.lblPriorityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubject.Location = New System.Drawing.Point(17, 6)
        Me.lblSubject.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(62, 14)
        Me.lblSubject.TabIndex = 17
        Me.lblSubject.Text = "Subject :"
        Me.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDueDateValue
        '
        Me.lblDueDateValue.AutoEllipsis = True
        Me.lblDueDateValue.AutoSize = True
        Me.lblDueDateValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDateValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDueDateValue.Location = New System.Drawing.Point(447, 27)
        Me.lblDueDateValue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDueDateValue.Name = "lblDueDateValue"
        Me.lblDueDateValue.Size = New System.Drawing.Size(93, 14)
        Me.lblDueDateValue.TabIndex = 31
        Me.lblDueDateValue.Text = "Due Date Value"
        Me.lblDueDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStartDate.Location = New System.Drawing.Point(365, 6)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(80, 14)
        Me.lblStartDate.TabIndex = 20
        Me.lblStartDate.Text = "Start Date :"
        '
        'lblStartDateValue
        '
        Me.lblStartDateValue.AutoEllipsis = True
        Me.lblStartDateValue.AutoSize = True
        Me.lblStartDateValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDateValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStartDateValue.Location = New System.Drawing.Point(447, 6)
        Me.lblStartDateValue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblStartDateValue.Name = "lblStartDateValue"
        Me.lblStartDateValue.Size = New System.Drawing.Size(98, 14)
        Me.lblStartDateValue.TabIndex = 30
        Me.lblStartDateValue.Text = "Start Date Value"
        Me.lblStartDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPriority
        '
        Me.lblPriority.AutoSize = True
        Me.lblPriority.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPriority.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPriority.Location = New System.Drawing.Point(385, 48)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(60, 14)
        Me.lblPriority.TabIndex = 26
        Me.lblPriority.Text = "Priority :"
        '
        'lblStatusValue
        '
        Me.lblStatusValue.AutoEllipsis = True
        Me.lblStatusValue.AutoSize = True
        Me.lblStatusValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStatusValue.Location = New System.Drawing.Point(80, 48)
        Me.lblStatusValue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(42, 14)
        Me.lblStatusValue.TabIndex = 29
        Me.lblStatusValue.Text = "Status"
        Me.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(23, 48)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(56, 14)
        Me.lblStatus.TabIndex = 25
        Me.lblStatus.Text = "Status :"
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoEllipsis = True
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProviderName.Location = New System.Drawing.Point(80, 27)
        Me.lblProviderName.Margin = New System.Windows.Forms.Padding(0)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(45, 14)
        Me.lblProviderName.TabIndex = 28
        Me.lblProviderName.Text = "PName"
        Me.lblProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlTask)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(1255, 28)
        Me.Panel3.TabIndex = 36
        '
        'gloUC_TaskInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlTaskDetailsMain)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_TaskInfo"
        Me.Size = New System.Drawing.Size(1255, 100)
        Me.pnlTask.ResumeLayout(False)
        Me.pnlTaskDetailsMain.ResumeLayout(False)
        Me.pnlrchtxt_TaskDescription.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlTaskDetails.ResumeLayout(False)
        Me.pnlTaskDetails.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTask As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents pnlTaskDetailsMain As System.Windows.Forms.Panel
    Friend WithEvents lblTaskHeader As System.Windows.Forms.Label
    Private WithEvents lblStartDate As System.Windows.Forms.Label
    Private WithEvents lblSubject As System.Windows.Forms.Label
    Private WithEvents lblEndDate As System.Windows.Forms.Label
    Private WithEvents lblProvider As System.Windows.Forms.Label
    Private WithEvents lblSubjectName As System.Windows.Forms.Label
    Private WithEvents lblStatus As System.Windows.Forms.Label
    Private WithEvents lblPriority As System.Windows.Forms.Label
    Private WithEvents lblDueDateValue As System.Windows.Forms.Label
    Private WithEvents lblStartDateValue As System.Windows.Forms.Label
    Private WithEvents lblStatusValue As System.Windows.Forms.Label
    Private WithEvents lblProviderName As System.Windows.Forms.Label
    Friend WithEvents rchtxt_TaskDescription As System.Windows.Forms.RichTextBox
    Private WithEvents lblPriorityValue As System.Windows.Forms.Label
    Friend WithEvents pnlTaskDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlrchtxt_TaskDescription As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label

End Class
