Imports C1.Win.C1FlexGrid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyDay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
            ofrmMyDay = Nothing
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Shared ofrmMyDay As frmMyDay
    Public Shared Function GetInstance() As frmMyDay
        If IsNothing(ofrmMyDay) = True Then
            ofrmMyDay = New frmMyDay()
        End If
        Return ofrmMyDay
    End Function
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMyDay))
        Me.pnlHeaderMain = New System.Windows.Forms.Panel
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblHeader = New System.Windows.Forms.Label
        Me.label17 = New System.Windows.Forms.Label
        Me.label18 = New System.Windows.Forms.Label
        Me.label19 = New System.Windows.Forms.Label
        Me.label20 = New System.Windows.Forms.Label
        Me.pnlPatientStatusGrid = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.C1Calendar = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.mntCalendar = New System.Windows.Forms.MonthCalendar
        Me.UiPanelManager1 = New Janus.Windows.UI.Dock.UIPanelManager(Me.components)
        Me.pnlTriage = New Janus.Windows.UI.Dock.UIPanel
        Me.uiPanel0Container = New Janus.Windows.UI.Dock.UIPanelInnerContainer
        Me.C1Triage = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlMessages = New Janus.Windows.UI.Dock.UIPanel
        Me.pnlMessagesContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer
        Me.C1Message = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlTask = New Janus.Windows.UI.Dock.UIPanel
        Me.pnlTaskContainer = New Janus.Windows.UI.Dock.UIPanelInnerContainer
        Me.C1Task = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.imgList_Common = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnNext = New System.Windows.Forms.Button
        Me.lblTodayDate = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMntCalendar = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlHeaderMain.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlPatientStatusGrid.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.C1Calendar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.UiPanelManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlTriage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTriage.SuspendLayout()
        Me.uiPanel0Container.SuspendLayout()
        CType(Me.C1Triage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMessages.SuspendLayout()
        Me.pnlMessagesContainer.SuspendLayout()
        CType(Me.C1Message, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlTask, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTask.SuspendLayout()
        Me.pnlTaskContainer.SuspendLayout()
        CType(Me.C1Task, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMntCalendar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeaderMain
        '
        Me.pnlHeaderMain.Controls.Add(Me.pnlHeader)
        Me.pnlHeaderMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeaderMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlHeaderMain.Name = "pnlHeaderMain"
        Me.pnlHeaderMain.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlHeaderMain.Size = New System.Drawing.Size(1023, 26)
        Me.pnlHeaderMain.TabIndex = 17
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.label17)
        Me.pnlHeader.Controls.Add(Me.label18)
        Me.pnlHeader.Controls.Add(Me.label19)
        Me.pnlHeader.Controls.Add(Me.label20)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1017, 23)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHeader.Location = New System.Drawing.Point(1, 1)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(1015, 21)
        Me.lblHeader.TabIndex = 13
        Me.lblHeader.Text = "      My Day "
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label17
        '
        Me.label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label17.Location = New System.Drawing.Point(1, 22)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(1015, 1)
        Me.label17.TabIndex = 12
        Me.label17.Text = "label2"
        '
        'label18
        '
        Me.label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.Location = New System.Drawing.Point(0, 1)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(1, 22)
        Me.label18.TabIndex = 11
        Me.label18.Text = "label4"
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label19.Location = New System.Drawing.Point(1016, 1)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(1, 22)
        Me.label19.TabIndex = 10
        Me.label19.Text = "label3"
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.Location = New System.Drawing.Point(0, 0)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(1017, 1)
        Me.label20.TabIndex = 9
        Me.label20.Text = "label1"
        '
        'pnlPatientStatusGrid
        '
        Me.pnlPatientStatusGrid.Controls.Add(Me.Panel3)
        Me.pnlPatientStatusGrid.Controls.Add(Me.Panel4)
        Me.pnlPatientStatusGrid.Controls.Add(Me.Label4)
        Me.pnlPatientStatusGrid.Controls.Add(Me.Label3)
        Me.pnlPatientStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientStatusGrid.Location = New System.Drawing.Point(3, 135)
        Me.pnlPatientStatusGrid.Name = "pnlPatientStatusGrid"
        Me.pnlPatientStatusGrid.Size = New System.Drawing.Size(230, 287)
        Me.pnlPatientStatusGrid.TabIndex = 153
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.C1Calendar)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(1, 24)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(228, 263)
        Me.Panel3.TabIndex = 2
        '
        'C1Calendar
        '
        Me.C1Calendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Calendar.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Calendar.ColumnInfo = "0,0,0,0,0,90,Columns:"
        Me.C1Calendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Calendar.ExtendLastCol = True
        Me.C1Calendar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Calendar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Calendar.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Calendar.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Calendar.Location = New System.Drawing.Point(0, 0)
        Me.C1Calendar.Name = "C1Calendar"
        Me.C1Calendar.Rows.Count = 5
        Me.C1Calendar.Rows.DefaultSize = 18
        Me.C1Calendar.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Calendar.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Calendar.ShowCellLabels = True
        Me.C1Calendar.Size = New System.Drawing.Size(228, 263)
        Me.C1Calendar.StyleInfo = resources.GetString("C1Calendar.StyleInfo")
        Me.C1Calendar.TabIndex = 35
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(1, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(228, 24)
        Me.Panel4.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 23)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "      Calendar"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(228, 1)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(229, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 287)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "label3"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 287)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "label4"
        '
        'mntCalendar
        '
        Me.mntCalendar.BackColor = System.Drawing.Color.White
        Me.mntCalendar.ForeColor = System.Drawing.Color.Tomato
        Me.mntCalendar.Location = New System.Drawing.Point(0, 0)
        Me.mntCalendar.Name = "mntCalendar"
        Me.mntCalendar.TabIndex = 36
        Me.mntCalendar.TitleBackColor = System.Drawing.Color.Orange
        Me.mntCalendar.TitleForeColor = System.Drawing.Color.Maroon
        '
        'UiPanelManager1
        '
        Me.UiPanelManager1.ContainerControl = Me
        Me.UiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        Me.pnlTriage.Id = New System.Guid("f063ddbf-c903-4251-a375-92130f5ea30f")
        Me.UiPanelManager1.Panels.Add(Me.pnlTriage)
        Me.pnlMessages.Id = New System.Guid("6089dcd8-704a-4d0c-a284-e337e8ed0376")
        Me.UiPanelManager1.Panels.Add(Me.pnlMessages)
        Me.pnlTask.Id = New System.Guid("26c3e845-e12d-46b3-b5e2-8d2d720efa6e")
        Me.UiPanelManager1.Panels.Add(Me.pnlTask)
        '
        'Design Time Panel Info:
        '
        Me.UiPanelManager1.BeginPanelInfo()
        Me.UiPanelManager1.AddDockPanelInfo(New System.Guid("f063ddbf-c903-4251-a375-92130f5ea30f"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, New System.Drawing.Size(1017, 296), True)
        Me.UiPanelManager1.AddDockPanelInfo(New System.Guid("6089dcd8-704a-4d0c-a284-e337e8ed0376"), Janus.Windows.UI.Dock.PanelDockStyle.Right, New System.Drawing.Size(381, 287), True)
        Me.UiPanelManager1.AddDockPanelInfo(New System.Guid("26c3e845-e12d-46b3-b5e2-8d2d720efa6e"), Janus.Windows.UI.Dock.PanelDockStyle.Right, New System.Drawing.Size(406, 287), True)
        Me.UiPanelManager1.AddFloatingPanelInfo(New System.Guid("f063ddbf-c903-4251-a375-92130f5ea30f"), New System.Drawing.Point(557, 728), New System.Drawing.Size(200, 200), False)
        Me.UiPanelManager1.AddFloatingPanelInfo(New System.Guid("6089dcd8-704a-4d0c-a284-e337e8ed0376"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), False)
        Me.UiPanelManager1.AddFloatingPanelInfo(New System.Guid("26c3e845-e12d-46b3-b5e2-8d2d720efa6e"), New System.Drawing.Point(44, 58), New System.Drawing.Size(200, 200), False)
        Me.UiPanelManager1.EndPanelInfo()
        '
        'pnlTriage
        '
        Me.pnlTriage.CaptionDisplayMode = Janus.Windows.UI.Dock.PanelCaptionDisplayMode.ImageAndText
        Me.pnlTriage.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None
        Me.pnlTriage.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTriage.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlTriage.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlTriage.FloatingLocation = New System.Drawing.Point(557, 728)
        Me.pnlTriage.Icon = CType(resources.GetObject("pnlTriage.Icon"), System.Drawing.Icon)
        Me.pnlTriage.InnerContainer = Me.uiPanel0Container
        Me.pnlTriage.Location = New System.Drawing.Point(3, 422)
        Me.pnlTriage.Name = "pnlTriage"
        Me.pnlTriage.Size = New System.Drawing.Size(1017, 296)
        Me.pnlTriage.TabIndex = 4
        Me.pnlTriage.Text = "Triage"
        '
        'uiPanel0Container
        '
        Me.uiPanel0Container.Controls.Add(Me.C1Triage)
        Me.uiPanel0Container.Location = New System.Drawing.Point(1, 27)
        Me.uiPanel0Container.Name = "uiPanel0Container"
        Me.uiPanel0Container.Size = New System.Drawing.Size(1015, 268)
        Me.uiPanel0Container.TabIndex = 0
        '
        'C1Triage
        '
        Me.C1Triage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Triage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Triage.ColumnInfo = "0,0,0,0,0,90,Columns:"
        Me.C1Triage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Triage.ExtendLastCol = True
        Me.C1Triage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Triage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Triage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Triage.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Triage.Location = New System.Drawing.Point(0, 0)
        Me.C1Triage.Name = "C1Triage"
        Me.C1Triage.Rows.Count = 5
        Me.C1Triage.Rows.DefaultSize = 18
        Me.C1Triage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Triage.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Triage.ShowCellLabels = True
        Me.C1Triage.Size = New System.Drawing.Size(1015, 268)
        Me.C1Triage.StyleInfo = resources.GetString("C1Triage.StyleInfo")
        Me.C1Triage.TabIndex = 35
        '
        'pnlMessages
        '
        Me.pnlMessages.CaptionDisplayMode = Janus.Windows.UI.Dock.PanelCaptionDisplayMode.ImageAndText
        Me.pnlMessages.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None
        Me.pnlMessages.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.pnlMessages.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlMessages.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlMessages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMessages.Icon = CType(resources.GetObject("pnlMessages.Icon"), System.Drawing.Icon)
        Me.pnlMessages.InnerContainer = Me.pnlMessagesContainer
        Me.pnlMessages.Location = New System.Drawing.Point(639, 135)
        Me.pnlMessages.Name = "pnlMessages"
        Me.pnlMessages.Size = New System.Drawing.Size(381, 287)
        Me.pnlMessages.TabIndex = 4
        Me.pnlMessages.Text = "Messages"
        '
        'pnlMessagesContainer
        '
        Me.pnlMessagesContainer.Controls.Add(Me.C1Message)
        Me.pnlMessagesContainer.Location = New System.Drawing.Point(5, 23)
        Me.pnlMessagesContainer.Name = "pnlMessagesContainer"
        Me.pnlMessagesContainer.Size = New System.Drawing.Size(375, 263)
        Me.pnlMessagesContainer.TabIndex = 0
        '
        'C1Message
        '
        Me.C1Message.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Message.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Message.ColumnInfo = "0,0,0,0,0,90,Columns:"
        Me.C1Message.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Message.ExtendLastCol = True
        Me.C1Message.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Message.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Message.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Message.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Message.Location = New System.Drawing.Point(0, 0)
        Me.C1Message.Name = "C1Message"
        Me.C1Message.Rows.Count = 5
        Me.C1Message.Rows.DefaultSize = 18
        Me.C1Message.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Message.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Message.ShowCellLabels = True
        Me.C1Message.Size = New System.Drawing.Size(375, 263)
        Me.C1Message.StyleInfo = resources.GetString("C1Message.StyleInfo")
        Me.C1Message.TabIndex = 34
        '
        'pnlTask
        '
        Me.pnlTask.CaptionDisplayMode = Janus.Windows.UI.Dock.PanelCaptionDisplayMode.ImageAndText
        Me.pnlTask.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None
        Me.pnlTask.CaptionFormatStyle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.pnlTask.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        Me.pnlTask.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.pnlTask.FloatingLocation = New System.Drawing.Point(44, 58)
        Me.pnlTask.Icon = CType(resources.GetObject("pnlTask.Icon"), System.Drawing.Icon)
        Me.pnlTask.InnerContainer = Me.pnlTaskContainer
        Me.pnlTask.Location = New System.Drawing.Point(233, 135)
        Me.pnlTask.Name = "pnlTask"
        Me.pnlTask.Size = New System.Drawing.Size(406, 287)
        Me.pnlTask.TabIndex = 4
        Me.pnlTask.Text = "Task"
        '
        'pnlTaskContainer
        '
        Me.pnlTaskContainer.Controls.Add(Me.C1Task)
        Me.pnlTaskContainer.Location = New System.Drawing.Point(5, 23)
        Me.pnlTaskContainer.Name = "pnlTaskContainer"
        Me.pnlTaskContainer.Size = New System.Drawing.Size(400, 263)
        Me.pnlTaskContainer.TabIndex = 0
        '
        'C1Task
        '
        Me.C1Task.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Task.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Task.ColumnInfo = "0,0,0,0,0,90,Columns:"
        Me.C1Task.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Task.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Task.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Task.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Task.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Task.Location = New System.Drawing.Point(0, 0)
        Me.C1Task.Name = "C1Task"
        Me.C1Task.Rows.Count = 5
        Me.C1Task.Rows.DefaultSize = 18
        Me.C1Task.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Task.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Task.ShowCellLabels = True
        Me.C1Task.Size = New System.Drawing.Size(400, 263)
        Me.C1Task.StyleInfo = resources.GetString("C1Task.StyleInfo")
        Me.C1Task.TabIndex = 35
        '
        'imgList_Common
        '
        Me.imgList_Common.ImageStream = CType(resources.GetObject("imgList_Common.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_Common.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Common.Images.SetKeyName(0, "")
        Me.imgList_Common.Images.SetKeyName(1, "")
        Me.imgList_Common.Images.SetKeyName(2, "")
        Me.imgList_Common.Images.SetKeyName(3, "")
        Me.imgList_Common.Images.SetKeyName(4, "")
        Me.imgList_Common.Images.SetKeyName(5, "Mail small.ico")
        Me.imgList_Common.Images.SetKeyName(6, "Change Doctor.ico")
        Me.imgList_Common.Images.SetKeyName(7, "Chief Complaints.ico")
        Me.imgList_Common.Images.SetKeyName(8, "Exempt Form Report.ico")
        Me.imgList_Common.Images.SetKeyName(9, "New Exam.ico")
        Me.imgList_Common.Images.SetKeyName(10, "Pull Charts.ico")
        Me.imgList_Common.Images.SetKeyName(11, "Set  Patient Alert.ico")
        Me.imgList_Common.Images.SetKeyName(12, "Set Surgical alert.ico")
        Me.imgList_Common.Images.SetKeyName(13, "Calender DUE.ico")
        Me.imgList_Common.Images.SetKeyName(14, "High PriorityRed.png")
        Me.imgList_Common.Images.SetKeyName(15, "Low Priority.ico")
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btnNext)
        Me.Panel1.Controls.Add(Me.lblTodayDate)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.btnPrevious)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1017, 49)
        Me.Panel1.TabIndex = 36
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Transparent
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Location = New System.Drawing.Point(474, 1)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(37, 47)
        Me.btnNext.TabIndex = 18
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'lblTodayDate
        '
        Me.lblTodayDate.BackColor = System.Drawing.Color.Transparent
        Me.lblTodayDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblTodayDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTodayDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTodayDate.Location = New System.Drawing.Point(133, 1)
        Me.lblTodayDate.Name = "lblTodayDate"
        Me.lblTodayDate.Size = New System.Drawing.Size(341, 47)
        Me.lblTodayDate.TabIndex = 17
        Me.lblTodayDate.Text = "Today"
        Me.lblTodayDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1016, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 47)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "label4"
        '
        'btnPrevious
        '
        Me.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPrevious.FlatAppearance.BorderSize = 0
        Me.btnPrevious.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevious.Location = New System.Drawing.Point(96, 1)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(37, 47)
        Me.btnPrevious.TabIndex = 18
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Button1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(1, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(95, 47)
        Me.Panel5.TabIndex = 19
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(9, -6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 74)
        Me.Button1.TabIndex = 16
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1016, 1)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 48)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1017, 1)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "label2"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel2.Size = New System.Drawing.Size(1023, 52)
        Me.Panel2.TabIndex = 36
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1023, 54)
        Me.pnlToolStrip.TabIndex = 154
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1023, 54)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 51)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMntCalendar
        '
        Me.pnlMntCalendar.Controls.Add(Me.Label12)
        Me.pnlMntCalendar.Controls.Add(Me.Label11)
        Me.pnlMntCalendar.Controls.Add(Me.Label10)
        Me.pnlMntCalendar.Controls.Add(Me.Label9)
        Me.pnlMntCalendar.Controls.Add(Me.mntCalendar)
        Me.pnlMntCalendar.Location = New System.Drawing.Point(738, 0)
        Me.pnlMntCalendar.Name = "pnlMntCalendar"
        Me.pnlMntCalendar.Size = New System.Drawing.Size(199, 165)
        Me.pnlMntCalendar.TabIndex = 36
        Me.pnlMntCalendar.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Tomato
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1, 164)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(197, 1)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Tomato
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(197, 1)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Tomato
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(198, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 165)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Tomato
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 165)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "label4"
        '
        'frmMyDay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1023, 721)
        Me.Controls.Add(Me.pnlMntCalendar)
        Me.Controls.Add(Me.pnlPatientStatusGrid)
        Me.Controls.Add(Me.pnlTask)
        Me.Controls.Add(Me.pnlMessages)
        Me.Controls.Add(Me.pnlTriage)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeaderMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMyDay"
        Me.ShowInTaskbar = False
        Me.Text = "My Day"
        Me.pnlHeaderMain.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlPatientStatusGrid.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.C1Calendar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.UiPanelManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlTriage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTriage.ResumeLayout(False)
        Me.uiPanel0Container.ResumeLayout(False)
        CType(Me.C1Triage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMessages.ResumeLayout(False)
        Me.pnlMessagesContainer.ResumeLayout(False)
        CType(Me.C1Message, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlTask, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTask.ResumeLayout(False)
        Me.pnlTaskContainer.ResumeLayout(False)
        CType(Me.C1Task, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMntCalendar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents pnlHeaderMain As System.Windows.Forms.Panel
    Private WithEvents pnlHeader As System.Windows.Forms.Panel
    Private WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientStatusGrid As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UiPanelManager1 As Janus.Windows.UI.Dock.UIPanelManager
    Friend WithEvents pnlTask As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlTaskContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents pnlMessages As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents pnlMessagesContainer As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Private WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlTriage As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents uiPanel0Container As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Private WithEvents imgList_Common As System.Windows.Forms.ImageList
    Friend WithEvents C1Message As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Calendar As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Task As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Triage As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblTodayDate As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents mntCalendar As System.Windows.Forms.MonthCalendar
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMntCalendar As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label

End Class
