Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmDM_VWrptDueGuidelines

    Inherits System.Windows.Forms.Form
    Dim orpt As ReportDocument

    Dim COL_PATCODE As Int16 = 0
    Dim COL_PATNAME As Int16 = 1
    Dim COL_CriteriaName As Int16 = 2
    Dim COL_Order As Int16 = 3
    Dim COL_DueOn As Int16 = 4
    Dim COL_DueDate As Int16 = 5
    Dim COL_Reason As Int16 = 6
    Dim COL_Notes As Int16 = 7
    Friend WithEvents pnltlsDueOverDue As System.Windows.Forms.Panel
    Friend WithEvents tlsDueReport As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsPrintPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnShowReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    'Dim COL_TEMPLATE As Int16 = 2
    'Dim COL_DATE As Int16 = 3

    Dim COL_COUNT As Int16 = 8

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
                If (IsNothing(dtpToDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                    Catch ex As Exception

                    End Try


                    dtpToDate.Dispose()
                    dtpToDate = Nothing
                End If
            Catch
            End Try

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
    Friend WithEvents pnlPatient As System.Windows.Forms.Panel
    Friend WithEvents pnlChkBox As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblPatientDetails As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents btnShowReport1 As System.Windows.Forms.Button
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents c1Templates As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblCriteria As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_VWrptDueGuidelines))
        Me.pnlPatient = New System.Windows.Forms.Panel
        Me.pnlChkBox = New System.Windows.Forms.Panel
        Me.c1Templates = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.lblCriteria = New System.Windows.Forms.Label
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker
        Me.lblMessage = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblPatientCode = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblPatientDetails = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnShowReport1 = New System.Windows.Forms.Button
        Me.pnltlsDueOverDue = New System.Windows.Forms.Panel
        Me.tlsDueReport = New gloGlobal.gloToolStripIgnoreFocus
        Me.btnShowReport = New System.Windows.Forms.ToolStripButton
        Me.tlsPrint = New System.Windows.Forms.ToolStripButton
        Me.tlsPrintPreview = New System.Windows.Forms.ToolStripButton
        Me.tlsClose = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlPatient.SuspendLayout()
        Me.pnlChkBox.SuspendLayout()
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnltlsDueOverDue.SuspendLayout()
        Me.tlsDueReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPatient
        '
        Me.pnlPatient.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatient.Controls.Add(Me.pnlChkBox)
        Me.pnlPatient.Controls.Add(Me.Panel4)
        Me.pnlPatient.Controls.Add(Me.Panel1)
        Me.pnlPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatient.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatient.Location = New System.Drawing.Point(0, 56)
        Me.pnlPatient.Name = "pnlPatient"
        Me.pnlPatient.Size = New System.Drawing.Size(861, 582)
        Me.pnlPatient.TabIndex = 4
        '
        'pnlChkBox
        '
        Me.pnlChkBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlChkBox.Controls.Add(Me.c1Templates)
        Me.pnlChkBox.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlChkBox.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlChkBox.Controls.Add(Me.lbl_RightBrd)
        Me.pnlChkBox.Controls.Add(Me.lbl_TopBrd)
        Me.pnlChkBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChkBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlChkBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlChkBox.Location = New System.Drawing.Point(0, 27)
        Me.pnlChkBox.Name = "pnlChkBox"
        Me.pnlChkBox.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlChkBox.Size = New System.Drawing.Size(861, 555)
        Me.pnlChkBox.TabIndex = 68
        '
        'c1Templates
        '
        Me.c1Templates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Templates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Templates.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1Templates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Templates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Templates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Templates.Location = New System.Drawing.Point(4, 2)
        Me.c1Templates.Name = "c1Templates"
        Me.c1Templates.Rows.DefaultSize = 19
        Me.c1Templates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Templates.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Templates.Size = New System.Drawing.Size(853, 549)
        Me.c1Templates.StyleInfo = resources.GetString("c1Templates.StyleInfo")
        Me.c1Templates.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 551)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(853, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 550)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(857, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 550)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(855, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.cmbCategory)
        Me.Panel4.Controls.Add(Me.lblCriteria)
        Me.Panel4.Controls.Add(Me.dtpToDate)
        Me.Panel4.Controls.Add(Me.lblMessage)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(861, 27)
        Me.Panel4.TabIndex = 1
        '
        'cmbCategory
        '
        Me.cmbCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.Items.AddRange(New Object() {"Due", "Overdue", "Given"})
        Me.cmbCategory.Location = New System.Drawing.Point(365, 1)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(116, 22)
        Me.cmbCategory.TabIndex = 1
        '
        'lblCriteria
        '
        Me.lblCriteria.BackColor = System.Drawing.Color.Transparent
        Me.lblCriteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.Location = New System.Drawing.Point(255, 1)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(110, 22)
        Me.lblCriteria.TabIndex = 6
        Me.lblCriteria.Text = "     Criteria :"
        Me.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(150, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(105, 22)
        Me.dtpToDate.TabIndex = 0
        Me.dtpToDate.Value = New Date(2007, 3, 1, 0, 0, 0, 0)
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(4, 1)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(146, 22)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Select Report Date :"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(853, 1)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(857, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(855, 1)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblPatientCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lblPatientDetails)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(861, 29)
        Me.Panel1.TabIndex = 51
        Me.Panel1.Visible = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(472, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 21)
        Me.Label3.TabIndex = 68
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatientCode
        '
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientCode.Location = New System.Drawing.Point(380, 4)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(92, 21)
        Me.lblPatientCode.TabIndex = 67
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(310, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 21)
        Me.Label6.TabIndex = 66
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientDetails
        '
        Me.lblPatientDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientDetails.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientDetails.Location = New System.Drawing.Point(4, 4)
        Me.lblPatientDetails.Name = "lblPatientDetails"
        Me.lblPatientDetails.Size = New System.Drawing.Size(306, 21)
        Me.lblPatientDetails.TabIndex = 69
        Me.lblPatientDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(853, 1)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 22)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(857, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 22)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(855, 1)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "label1"
        '
        'btnShowReport1
        '
        Me.btnShowReport1.BackColor = System.Drawing.Color.Transparent
        Me.btnShowReport1.BackgroundImage = CType(resources.GetObject("btnShowReport1.BackgroundImage"), System.Drawing.Image)
        Me.btnShowReport1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowReport1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnShowReport1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnShowReport1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowReport1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReport1.Location = New System.Drawing.Point(368, 12)
        Me.btnShowReport1.Name = "btnShowReport1"
        Me.btnShowReport1.Size = New System.Drawing.Size(95, 23)
        Me.btnShowReport1.TabIndex = 2
        Me.btnShowReport1.Text = "&Show Report"
        Me.btnShowReport1.UseVisualStyleBackColor = False
        Me.btnShowReport1.Visible = False
        '
        'pnltlsDueOverDue
        '
        Me.pnltlsDueOverDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltlsDueOverDue.Controls.Add(Me.btnShowReport1)
        Me.pnltlsDueOverDue.Controls.Add(Me.tlsDueReport)
        Me.pnltlsDueOverDue.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltlsDueOverDue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltlsDueOverDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltlsDueOverDue.Location = New System.Drawing.Point(0, 0)
        Me.pnltlsDueOverDue.Name = "pnltlsDueOverDue"
        Me.pnltlsDueOverDue.Size = New System.Drawing.Size(861, 56)
        Me.pnltlsDueOverDue.TabIndex = 69
        '
        'tlsDueReport
        '
        Me.tlsDueReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDueReport.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDueReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDueReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDueReport.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDueReport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnShowReport, Me.tlsPrint, Me.tlsPrintPreview, Me.tlsClose})
        Me.tlsDueReport.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDueReport.Location = New System.Drawing.Point(0, 0)
        Me.tlsDueReport.Name = "tlsDueReport"
        Me.tlsDueReport.Size = New System.Drawing.Size(861, 53)
        Me.tlsDueReport.TabIndex = 0
        Me.tlsDueReport.Text = "ToolStrip1"
        '
        'btnShowReport
        '
        Me.btnShowReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReport.Image = CType(resources.GetObject("btnShowReport.Image"), System.Drawing.Image)
        Me.btnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnShowReport.Name = "btnShowReport"
        Me.btnShowReport.Size = New System.Drawing.Size(97, 50)
        Me.btnShowReport.Tag = "ShowReport "
        Me.btnShowReport.Text = "&Show Report "
        Me.btnShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnShowReport.Visible = False
        '
        'tlsPrint
        '
        Me.tlsPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPrint.Image = CType(resources.GetObject("tlsPrint.Image"), System.Drawing.Image)
        Me.tlsPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsPrint.Name = "tlsPrint"
        Me.tlsPrint.Size = New System.Drawing.Size(45, 50)
        Me.tlsPrint.Tag = "Print"
        Me.tlsPrint.Text = "&Print "
        Me.tlsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsPrint.Visible = False
        '
        'tlsPrintPreview
        '
        Me.tlsPrintPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPrintPreview.Image = CType(resources.GetObject("tlsPrintPreview.Image"), System.Drawing.Image)
        Me.tlsPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsPrintPreview.Name = "tlsPrintPreview"
        Me.tlsPrintPreview.Size = New System.Drawing.Size(69, 50)
        Me.tlsPrintPreview.Tag = "Print Preview"
        Me.tlsPrintPreview.Text = "P&rint Prv "
        Me.tlsPrintPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsPrintPreview.ToolTipText = "Print Preview"
        Me.tlsPrintPreview.Visible = False
        '
        'tlsClose
        '
        Me.tlsClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsClose.Image = CType(resources.GetObject("tlsClose.Image"), System.Drawing.Image)
        Me.tlsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsClose.Name = "tlsClose"
        Me.tlsClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsClose.Tag = "Close"
        Me.tlsClose.Text = "&Close"
        Me.tlsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDM_VWrptDueGuidelines
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(861, 638)
        Me.Controls.Add(Me.pnlPatient)
        Me.Controls.Add(Me.pnltlsDueOverDue)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_VWrptDueGuidelines"
        Me.Text = "Health Plan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlPatient.ResumeLayout(False)
        Me.pnlChkBox.ResumeLayout(False)
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnltlsDueOverDue.ResumeLayout(False)
        Me.pnltlsDueOverDue.PerformLayout()
        Me.tlsDueReport.ResumeLayout(False)
        Me.tlsDueReport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmDM_VWrptDueGuidelines_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1Templates)

        Try
            ''System(or Today's) date is set to the date picker at form load, by Anil on 20071213
            dtpToDate.Value = Date.Now
            ''
            Call DesignGrid()
            'Call FillCombo()
            If (cmbCategory.Items.Count > 0) Then
                cmbCategory.SelectedIndex = 0
            End If

            Call Fillguidelines()
            'tlsPrint.Enabled = False
            'tlsPrintPreview.Enabled = False
            'c1Templates.Rows.Count = 1
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignGrid()
        With c1Templates
            ''''Set General Properties
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .AllowEditing = False



            ''''Set Header   
            .SetData(0, COL_PATCODE, "Patient Code")
            .SetData(0, COL_PATNAME, "Patient Name")
            .SetData(0, COL_CriteriaName, "Criteria Name")
            .SetData(0, COL_Order, "Order")
            .SetData(0, COL_DueOn, "Due On")
            .SetData(0, COL_DueDate, "Due")
            .SetData(0, COL_Reason, "Reason")
            .SetData(0, COL_Notes, "Notes")


            '.SetData(0, COL_DATE, "Due Date")
            '.SetData(0, COL_TEMPLATE, "Guideline Name")

            'Dim _Width As Single = (.Width - 20) / 7


            ''''Set Column Width
            .Cols(COL_PATCODE).Width = .Width * 0.15
            .Cols(COL_PATNAME).Width = .Width * 0.25
            .Cols(COL_CriteriaName).Width = .Width * 0.13
            .Cols(COL_Order).Width = .Width * 0.18
            .Cols(COL_DueOn).Width = .Width * 0.1
            .Cols(COL_DueDate).Width = .Width * 0.1
            .Cols(COL_Order).Width = .Width * 0.1
            .Cols(COL_DueOn).Width = .Width * 0.1
            .Cols(COL_DueDate).Width = .Width * 0.1
            .Cols(COL_Reason).Width = .Width * 0.15
            .Cols(COL_Notes).Width = .Width * 0.15

            ''''Set Column Text Alignment
            .Cols(COL_PATCODE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_DueDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            '.Cols(COL_TEMPLATE).Width = .Width * 0.3
            '.Cols(COL_DATE).Width = .Width * 0.18
        End With
    End Sub

    'Private Sub FillCombo()
    '    With cmbCategory
    '        .Items.Clear()
    '        .Items.Add("Due")
    '        .Items.Add("Overdue")
    '        .SelectedIndex = 0
    '    End With
    'End Sub

    Private Sub btnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReport1.Click, btnShowReport.Click
        Try
            tlsPrint.Enabled = True
            tlsPrintPreview.Enabled = True

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDT As New DataTable
            Dim oResult As New Collection
            Dim oGuideline As GuidelineResults

            oDB.Connect(GetConnectionString)
            With oDB
                .DBParameters.Add("@type", "Due", ParameterDirection.Input, SqlDbType.VarChar, 50)
                oDT = .ReadData("DM_GetDueOverdueGuideline")
            End With
            oDB.Disconnect()
            oDB = Nothing

            Dim oclsDM_Template As New clsDM_Template

            If cmbCategory.Text = "Due" Then
                oResult = oclsDM_Template.CheckForDueGuideline(oDT, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Due)
            ElseIf cmbCategory.Text = "Overdue" Then
                oResult = oclsDM_Template.CheckForDueGuideline(oDT, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.OverDue)
            End If
            oclsDM_Template = Nothing


            With c1Templates
                .Rows.Count = 1
                '.Rows.Fixed = 1
                '.Cols.Count = COL_COUNT
                '.Cols.Fixed = 0

                '.SetData(0, COL_PATCODE, "Patient Code")
                '.SetData(0, COL_PATNAME, "Patient Name")
                '.SetData(0, COL_DATE, "Due Date")
                '.SetData(0, COL_TEMPLATE, "Guideline Name")

                ''Dim _Width As Single = (.Width - 20) / 7

                '.Cols(COL_PATCODE).Width = .Width * 0.2
                '.Cols(COL_PATNAME).Width = .Width * 0.3
                '.Cols(COL_TEMPLATE).Width = .Width * 0.3
                '.Cols(COL_DATE).Width = .Width * 0.18

                For i As Integer = 1 To oResult.Count
                    .Rows.Add()
                    oGuideline = New GuidelineResults
                    oGuideline = CType(oResult(i), GuidelineResults)

                    '.SetData(.Rows.Count - 1, COL_PATCODE, oGuideline.PatientCode)
                    '.SetData(.Rows.Count - 1, COL_PATNAME, oGuideline.PatientName)
                    '.SetData(.Rows.Count - 1, COL_TEMPLATE, oGuideline.TemplateName)
                    '.SetData(.Rows.Count - 1, COL_DATE, oGuideline.OnSetDate)
                    oGuideline = Nothing

                Next
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDueReport()
        Try
            If c1Templates.Rows.Count > 1 Then
                If Insert_TempTable() = False Then
                    Exit Sub
                End If
            End If


            orpt = New ReportDocument
            orpt.Load(Application.StartupPath & "\Reports\rptDueOverDue.rpt")
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            ' Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = orpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            Dim objreport As TextObject
            objreport = orpt.ReportDefinition.ReportObjects.Item("Text6")
            objreport.Text = cmbCategory.Text & " Report"
            orpt.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function Insert_TempTable() As Boolean
        'Dim i As Integer
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim _strSQL As String = ""
        oDB.Connect(GetConnectionString)
        _strSQL = "DELETE FROM tmp_DueOverdueReport "

        oDB.ExecuteNonSQLQuery(_strSQL)

        With c1Templates
            'For i = 1 To .Rows.Count - 1
            '    _strSQL = "INSERT INTO  tmp_DueOverdueReport ( nID, sPatientCode, sPatientName, sSetDate, sGuideLine ) " _
            '    & " VALUES(" & i & ", '" & .GetData(i, COL_PATCODE) & "', '" & .GetData(i, COL_PATNAME) & "', '" & .GetData(i, COL_DATE) & "', '" & .GetData(i, COL_TEMPLATE) & "')  "
            '    oDB.ExecuteNonSQLQuery(_strSQL)
            'Next

        End With

        oDB.Disconnect()
        oDB = Nothing
        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsClose.Click
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub PreviewDueReport()
        Try
            If c1Templates.Rows.Count > 1 Then
                If Insert_TempTable() = False Then
                    Exit Sub
                End If
            End If
            Dim frm As New frmRptDueOverguidelines
            With frm
                .Type = cmbCategory.Text
                If cmbCategory.Text.ToUpper = "Due".ToUpper Then
                    .Text = " Report for Due Guidelines"
                ElseIf cmbCategory.Text.ToUpper = "Overdue".ToUpper Then
                    .Text = " Report for Overdue Guidelines"
                End If
                .WindowState = FormWindowState.Maximized
                .MdiParent = CType(Me.ParentForm, MainMenu)
                .Show()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectionChangeCommitted
        Try
            Fillguidelines()
            'tlsPrint.Enabled = False
            'tlsPrintPreview.Enabled = False
            'c1Templates.Rows.Count = 1
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        Try
            Fillguidelines()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        'tlsPrint.Enabled = False
        'tlsPrintPreview.Enabled = False
        'c1Templates.Rows.Count = 1
    End Sub
    Private Sub dtpToDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged

    End Sub
    Private Sub Fillguidelines()
        Try
            tlsPrint.Enabled = False
            tlsPrintPreview.Enabled = False

            '''' Intialize Variables
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDTLabs As New DataTable
            Dim oDTRadiology As New DataTable
            Dim oDTDrugs As New DataTable
            Dim oDTTemplates As New DataTable
            Dim oResult As New DueOverDueResults


            'Dim oDueResult As New DueOverDueResults
            'Dim oOverDueResult As New DueOverDueResults

            'Dim oGuideline As GuidelineResults
            Dim oDueResults As DueOverDueResult
            Dim strSelectQry As String = ""
            oDB.Connect(GetConnectionString)

            With oDB
                ''''Fill Table with Labs Orders Start
                'strSelectQry = "SELECT     p.sPatientCode, ISNULL(p.sFirstName, '') + SPACE(1) + ISNULL(p.sMiddleName, '') + SPACE(1) + ISNULL(p.sLastName, '') AS PatientName, " _
                '               & "DM_Patient.DM_sResult, DM_Patient.DM_nPrint, DM_Patient.DM_nFax, DM_Patient.DM_nType, DM_Patient.DM_DueType, " _
                '               & "DM_Patient.DM_bIsOverride, DM_Patient.DM_DueValue, DM_Patient.DM_sReason, DM_Patient.DM_sNotes, DM_Patient.DM_bIsGiven, " _
                '               & "DM_Patient.DM_bIsRecurring, DM_Patient.DM_nCriteriaID, Lab_Order_Test_ResultDtl.labotrd_ResultName, " _
                '               & "Lab_Order_Test_ResultDtl.labotrd_ResultNameID, DM_Criteria_MST.dm_mst_CriteriaName, p.dtDOB " _
                '               & "FROM         Lab_Order_Test_ResultDtl INNER JOIN " _
                '               & "DM_Criteria_MST INNER JOIN " _
                '               & "DM_Patient ON DM_Criteria_MST.dm_mst_Id = DM_Patient.DM_nCriteriaID INNER JOIN " _
                '               & "Patient AS p ON DM_Patient.DM_nPatientID = p.nPatientID ON Lab_Order_Test_ResultDtl.labotrd_TestID = DM_Patient.DM_nTriggerID where DM_Patient.DM_DueType <> ''"

                strSelectQry = " SELECT     p.sPatientCode, ISNULL(p.sFirstName, '') + SPACE(1) + ISNULL(p.sMiddleName, '') + SPACE(1) + ISNULL(p.sLastName, '') AS PatientName, " _
                               & " DM_Patient.DM_sResult, DM_Patient.DM_nPrint, DM_Patient.DM_nFax, DM_Patient.DM_nType, DM_Patient.DM_DueType, " _
                               & " DM_Patient.DM_bIsOverride, DM_Patient.DM_DueValue, DM_Patient.DM_sReason, DM_Patient.DM_sNotes, DM_Patient.DM_bIsGiven, " _
                               & " DM_Patient.DM_bIsRecurring, DM_Patient.DM_nCriteriaID, DM_Criteria_MST.dm_mst_CriteriaName, p.dtDOB, Lab_Test_Mst.labtm_Name " _
                               & " FROM         Lab_Test_Mst INNER JOIN " _
                               & " DM_Criteria_MST INNER JOIN " _
                               & " DM_Patient ON DM_Criteria_MST.dm_mst_Id = DM_Patient.DM_nCriteriaID INNER JOIN " _
                               & " Patient AS p ON DM_Patient.DM_nPatientID = p.nPatientID ON Lab_Test_Mst.labtm_ID = DM_Patient.DM_nTriggerID where DM_Patient.DM_nType = 1"
                '& " WHERE DM_Patient.DM_DueType <> ''"
                oDTLabs = .ReadQueryDataTable(strSelectQry)
                ''''Fill Table with Labs Orders end

                ''''Fill Table with Radilogy Order Start
                strSelectQry = "SELECT     p.sPatientCode, ISNULL(p.sFirstName, '') + SPACE(1) + ISNULL(p.sMiddleName, '') + SPACE(1) + ISNULL(p.sLastName, '') AS PatientName, " _
                              & "DM_Patient.DM_sResult, DM_Patient.DM_nPrint, DM_Patient.DM_nFax, DM_Patient.DM_nType, DM_Patient.DM_DueType, " _
                              & "DM_Patient.DM_bIsOverride, DM_Patient.DM_DueValue, DM_Patient.DM_sReason, DM_Patient.DM_sNotes, DM_Patient.DM_bIsGiven, " _
                              & "DM_Patient.DM_bIsRecurring, DM_Patient.DM_nCriteriaID, LM_Test.lm_test_Name, DM_Criteria_MST.dm_mst_CriteriaName, p.dtDOB " _
                              & "FROM DM_Criteria_MST INNER JOIN " _
                              & "DM_Patient ON DM_Criteria_MST.dm_mst_Id = DM_Patient.DM_nCriteriaID INNER JOIN " _
                              & "Patient AS p ON DM_Patient.DM_nPatientID = p.nPatientID INNER JOIN " _
                              & " LM_Test ON DM_Patient.DM_nTriggerID = LM_Test.lm_test_ID where DM_Patient.DM_nType = 2" ' where DM_Patient.DM_DueType <> ''"
                oDTRadiology = .ReadQueryDataTable(strSelectQry)
                ''''Fill Table with Radilogy Order End

                ''''Fill Table with Drugs Start
                strSelectQry = "SELECT     p.sPatientCode, ISNULL(p.sFirstName, '') + SPACE(1) + ISNULL(p.sMiddleName, '') + SPACE(1) + ISNULL(p.sLastName, '') AS PatientName, " _
                               & "DM_Patient.DM_sResult, DM_Patient.DM_nPrint, DM_Patient.DM_nFax, DM_Patient.DM_nType, DM_Patient.DM_DueType, " _
                               & "DM_Patient.DM_bIsOverride, DM_Patient.DM_DueValue, DM_Patient.DM_sReason, DM_Patient.DM_sNotes, DM_Patient.DM_bIsGiven, " _
                               & "DM_Patient.DM_bIsRecurring, DM_Patient.DM_nCriteriaID, Drugs_MST.sDrugName, DM_Criteria_MST.dm_mst_CriteriaName, p.dtDOB " _
                               & "FROM         DM_Criteria_MST INNER JOIN " _
                               & "DM_Patient ON DM_Criteria_MST.dm_mst_Id = DM_Patient.DM_nCriteriaID INNER JOIN " _
                               & "Patient AS p ON DM_Patient.DM_nPatientID = p.nPatientID INNER JOIN " _
                               & "Drugs_MST ON DM_Patient.DM_nTriggerID = Drugs_MST.nDrugsID where DM_Patient.DM_nType = 4" ' where DM_Patient.DM_DueType <> '' "
                oDTDrugs = .ReadQueryDataTable(strSelectQry)
                ''''Fill Table with Drugs End

                ''''Fill Table with Guidlines Templates Start
                strSelectQry = "SELECT     p.sPatientCode, ISNULL(p.sFirstName, '') + SPACE(1) + ISNULL(p.sMiddleName, '') + SPACE(1) + ISNULL(p.sLastName, '') AS PatientName, " _
                               & "t.sTemplateName, DM_Patient.DM_sResult, DM_Patient.DM_nPrint, DM_Patient.DM_nFax, DM_Patient.DM_nType, DM_Patient.DM_DueType, " _
                               & "DM_Patient.DM_bIsOverride, DM_Patient.DM_DueValue, DM_Patient.DM_sReason, DM_Patient.DM_sNotes, DM_Patient.DM_bIsGiven, " _
                               & "DM_Patient.DM_bIsRecurring, DM_Patient.DM_nCriteriaID, DM_Criteria_MST.dm_mst_CriteriaName, p.dtDOB " _
                               & "FROM DM_Criteria_MST INNER JOIN " _
                               & "DM_Patient ON DM_Criteria_MST.dm_mst_Id = DM_Patient.DM_nCriteriaID INNER JOIN " _
                               & "Patient AS p ON DM_Patient.DM_nPatientID = p.nPatientID INNER JOIN " _
                               & "TemplateGallery_MST AS t ON DM_Patient.DM_nTriggerID = t.nTemplateID where DM_Patient.DM_nType = 0" ' where DM_Patient.DM_DueType <> '' "
                oDTTemplates = .ReadQueryDataTable(strSelectQry)
                ''''Fill Table with Guidlines Templates End
            End With


            'With oDB
            '    .DBParameters.Add("@type", "Due", ParameterDirection.Input, SqlDbType.VarChar, 50)
            '    oDT = .ReadData("DM_GetDueOverdueGuideline")
            'End With
            oDB.Disconnect()
            oDB = Nothing

            Dim oclsDM_Template As New clsDM_Template

            If cmbCategory.Text = "Due" Then
                If oDTLabs.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTLabs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Due, EnumTriggerType.Labs)
                End If
                If oDTRadiology.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTRadiology, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Due, EnumTriggerType.Radiology)
                End If
                If oDTDrugs.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTDrugs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Due, EnumTriggerType.Drugs)
                End If
                If oDTTemplates.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTTemplates, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Due, EnumTriggerType.Guidelines)
                End If
            ElseIf cmbCategory.Text = "Overdue" Then
                If oDTLabs.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTLabs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.OverDue, EnumTriggerType.Labs)
                End If
                If oDTRadiology.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTRadiology, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.OverDue, EnumTriggerType.Radiology)
                End If
                If oDTDrugs.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTDrugs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.OverDue, EnumTriggerType.Drugs)
                End If
                If oDTTemplates.Rows.Count >= 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTTemplates, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.OverDue, EnumTriggerType.Guidelines)
                End If
            ElseIf cmbCategory.Text = "Given" Then

                If oDTLabs.Rows.Count <> 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTLabs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Given, EnumTriggerType.Labs)
                End If
                If oDTRadiology.Rows.Count <> 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTRadiology, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Given, EnumTriggerType.Radiology)
                End If
                If oDTDrugs.Rows.Count <> 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTDrugs, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Given, EnumTriggerType.Drugs)
                End If
                If oDTTemplates.Rows.Count <> 0 Then
                    oResult = oclsDM_Template.CheckDueForLabs(oDTTemplates, Format(dtpToDate.Value, "MM/dd/yyyy"), EnumGuidelineResult.Given, EnumTriggerType.Guidelines)
                End If
            End If

            oclsDM_Template = Nothing

            ''''Set Data to FlexGrid Start   
            With c1Templates
                .Rows.Count = 1
                If oResult.Count > 0 Then
                    'tlsPrint.Enabled = True
                    'tlsPrintPreview.Enabled = True

                    For i As Integer = 1 To oResult.Count
                        .Rows.Add()
                        oDueResults = New DueOverDueResult
                        oDueResults = CType(oResult(i), DueOverDueResult)

                        .SetData(.Rows.Count - 1, COL_PATCODE, oDueResults.PatientCode)
                        .SetData(.Rows.Count - 1, COL_PATNAME, oDueResults.PatientName)
                        .SetData(.Rows.Count - 1, COL_CriteriaName, oDueResults.CriteriaName)
                        .SetData(.Rows.Count - 1, COL_Order, oDueResults.TriggerName)
                        .SetData(.Rows.Count - 1, COL_DueOn, oDueResults.DueOn)
                        .SetData(.Rows.Count - 1, COL_DueDate, oDueResults.DueDate)
                        .SetData(.Rows.Count - 1, COL_Reason, oDueResults.Reason)
                        .SetData(.Rows.Count - 1, COL_Notes, oDueResults.Notes)
                        oDueResults = Nothing
                    Next
                End If

            End With

            ''''Set Data to FlexGrid End
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1Templates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1Templates.Click

    End Sub


    Private Sub tlsDueReport_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDueReport.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Print"
                PrintDueReport()
            Case "Print Preview"
                PreviewDueReport()
            Case "Close"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    Private Sub c1Templates_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Templates.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
