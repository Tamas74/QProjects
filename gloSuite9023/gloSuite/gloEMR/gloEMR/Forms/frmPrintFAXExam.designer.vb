<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintFAXExam
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpContextMenuStrip As ContextMenuStrip() = {cmnu_Diagnosis}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)

                Catch ex As Exception

                End Try


                Dim dtpControls As DateTimePicker() = {dtpicTo, dtpicFrom}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)

                Catch ex As Exception

                End Try
                Try
                    gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try
               

                

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintFAXExam))
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_SelectAll_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_FAX_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Preview_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtnSendCharges = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnMore = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.btnMore = New System.Windows.Forms.Button()
        Me.btnShowExams = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.dtpicTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpicFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbAllPatients = New System.Windows.Forms.RadioButton()
        Me.rdbSelectedPatient = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.pnlDrugDiagnosis = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rdbDiagnosisDesc = New System.Windows.Forms.RadioButton()
        Me.rdbDiagnosisCode = New System.Windows.Forms.RadioButton()
        Me.chkDiagnosis = New System.Windows.Forms.CheckedListBox()
        Me.lblSearchDiagnosis = New System.Windows.Forms.Label()
        Me.txtSearchDiagnosis = New System.Windows.Forms.TextBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.lblAgeTo = New System.Windows.Forms.Label()
        Me.lblAgeFrom = New System.Windows.Forms.Label()
        Me.cmbAgeTo = New System.Windows.Forms.ComboBox()
        Me.cmbAgeFrom = New System.Windows.Forms.ComboBox()
        Me.cmbAge = New System.Windows.Forms.ComboBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        '   Me.wdTemp = New AxDSOFramer.AxFramerControl()
        Me.lblSelectedDrugs = New System.Windows.Forms.Label()
        Me.chkMedication = New System.Windows.Forms.CheckedListBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAddToSearch = New System.Windows.Forms.Button()
        Me.trvDrugs = New System.Windows.Forms.TreeView()
        Me.lblSearchDrug = New System.Windows.Forms.Label()
        Me.txtSearchDrug = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C1grdExams = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlDSO = New System.Windows.Forms.Panel()
        Me.wdExam = New AxDSOFramer.AxFramerControl()
        Me.pnlTempDSO = New System.Windows.Forms.Panel()
        Me.lblExamName = New System.Windows.Forms.Label()
        Me.picPriviewClose = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        '  Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintProgress = New System.Windows.Forms.ProgressBar()
        Me.lblPrinting = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cmnu_Diagnosis = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.imgList_Common = New System.Windows.Forms.ImageList(Me.components)
        Me.rbICD10 = New System.Windows.Forms.RadioButton()
        Me.rbICD9 = New System.Windows.Forms.RadioButton()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlDrugDiagnosis.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        '  CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1grdExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDSO.SuspendLayout()
        CType(Me.wdExam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTempDSO.SuspendLayout()
        CType(Me.picPriviewClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_SelectAll_32, Me.tblbtn_Print_32, Me.tblbtn_FAX_32, Me.tblbtn_Preview_32, Me.tblbtnSendCharges, Me.ToolStripButton2, Me.tsbtnMore, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(1166, 53)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_SelectAll_32
        '
        Me.tblbtn_SelectAll_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_SelectAll_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_SelectAll_32.Image = CType(resources.GetObject("tblbtn_SelectAll_32.Image"), System.Drawing.Image)
        Me.tblbtn_SelectAll_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_SelectAll_32.Name = "tblbtn_SelectAll_32"
        Me.tblbtn_SelectAll_32.Size = New System.Drawing.Size(67, 50)
        Me.tblbtn_SelectAll_32.Text = "Select &All"
        Me.tblbtn_SelectAll_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(41, 50)
        Me.tblbtn_Print_32.Text = "&Print"
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_FAX_32
        '
        Me.tblbtn_FAX_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_FAX_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_FAX_32.Image = CType(resources.GetObject("tblbtn_FAX_32.Image"), System.Drawing.Image)
        Me.tblbtn_FAX_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_FAX_32.Name = "tblbtn_FAX_32"
        Me.tblbtn_FAX_32.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_FAX_32.Text = "&Fax"
        Me.tblbtn_FAX_32.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblbtn_FAX_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Preview_32
        '
        Me.tblbtn_Preview_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Preview_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Preview_32.Image = CType(resources.GetObject("tblbtn_Preview_32.Image"), System.Drawing.Image)
        Me.tblbtn_Preview_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Preview_32.Name = "tblbtn_Preview_32"
        Me.tblbtn_Preview_32.Size = New System.Drawing.Size(59, 50)
        Me.tblbtn_Preview_32.Text = "Pre&view"
        Me.tblbtn_Preview_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtnSendCharges
        '
        Me.tblbtnSendCharges.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnSendCharges.Image = CType(resources.GetObject("tblbtnSendCharges.Image"), System.Drawing.Image)
        Me.tblbtnSendCharges.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnSendCharges.Name = "tblbtnSendCharges"
        Me.tblbtnSendCharges.Size = New System.Drawing.Size(102, 50)
        Me.tblbtnSendCharges.Text = "S&end Charges"
        Me.tblbtnSendCharges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(99, 50)
        Me.ToolStripButton2.Text = "&Show Reports"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbtnMore
        '
        Me.tsbtnMore.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtnMore.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnMore.Image = Global.gloEMR.My.Resources.Resources.Show
        Me.tsbtnMore.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnMore.Name = "tsbtnMore"
        Me.tsbtnMore.Size = New System.Drawing.Size(42, 50)
        Me.tsbtnMore.Text = "&More"
        Me.tsbtnMore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_RightBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_TopBrd)
        Me.pnlSearch.Controls.Add(Me.btnMore)
        Me.pnlSearch.Controls.Add(Me.btnShowExams)
        Me.pnlSearch.Controls.Add(Me.GroupBox4)
        Me.pnlSearch.Controls.Add(Me.GroupBox2)
        Me.pnlSearch.Controls.Add(Me.GroupBox1)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 53)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(1166, 71)
        Me.pnlSearch.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 67)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1158, 1)
        Me.lbl_BottomBrd.TabIndex = 9
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 64)
        Me.lbl_LeftBrd.TabIndex = 8
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1162, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 64)
        Me.lbl_RightBrd.TabIndex = 7
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1160, 1)
        Me.lbl_TopBrd.TabIndex = 6
        Me.lbl_TopBrd.Text = "label1"
        '
        'btnMore
        '
        Me.btnMore.BackgroundImage = CType(resources.GetObject("btnMore.BackgroundImage"), System.Drawing.Image)
        Me.btnMore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMore.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMore.Location = New System.Drawing.Point(1022, 21)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(74, 24)
        Me.btnMore.TabIndex = 6
        Me.btnMore.Text = "More >>"
        Me.btnMore.UseVisualStyleBackColor = True
        Me.btnMore.Visible = False
        '
        'btnShowExams
        '
        Me.btnShowExams.BackgroundImage = CType(resources.GetObject("btnShowExams.BackgroundImage"), System.Drawing.Image)
        Me.btnShowExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowExams.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnShowExams.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnShowExams.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowExams.Location = New System.Drawing.Point(918, 21)
        Me.btnShowExams.Name = "btnShowExams"
        Me.btnShowExams.Size = New System.Drawing.Size(94, 24)
        Me.btnShowExams.TabIndex = 5
        Me.btnShowExams.Text = "Show Exams"
        Me.btnShowExams.UseVisualStyleBackColor = True
        Me.btnShowExams.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox4.Controls.Add(Me.dtpicTo)
        Me.GroupBox4.Controls.Add(Me.dtpicFrom)
        Me.GroupBox4.Controls.Add(Me.lblTo)
        Me.GroupBox4.Controls.Add(Me.lblFrom)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(593, 10)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(315, 46)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Date"
        '
        'dtpicTo
        '
        Me.dtpicTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicTo.CustomFormat = "MM/dd/yyyy"
        Me.dtpicTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicTo.Location = New System.Drawing.Point(196, 18)
        Me.dtpicTo.Name = "dtpicTo"
        Me.dtpicTo.Size = New System.Drawing.Size(107, 22)
        Me.dtpicTo.TabIndex = 4
        '
        'dtpicFrom
        '
        Me.dtpicFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpicFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicFrom.Location = New System.Drawing.Point(51, 18)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.Size = New System.Drawing.Size(107, 22)
        Me.dtpicFrom.TabIndex = 3
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(171, 22)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(22, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(14, 22)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(34, 14)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox2.Controls.Add(Me.cmbProvider)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(325, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 46)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Provider"
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(12, 18)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(174, 22)
        Me.cmbProvider.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.rdbAllPatients)
        Me.GroupBox1.Controls.Add(Me.rdbSelectedPatient)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(251, 46)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Type"
        '
        'rdbAllPatients
        '
        Me.rdbAllPatients.AutoSize = True
        Me.rdbAllPatients.BackColor = System.Drawing.Color.Transparent
        Me.rdbAllPatients.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbAllPatients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rdbAllPatients.Location = New System.Drawing.Point(146, 22)
        Me.rdbAllPatients.Name = "rdbAllPatients"
        Me.rdbAllPatients.Size = New System.Drawing.Size(85, 18)
        Me.rdbAllPatients.TabIndex = 1
        Me.rdbAllPatients.Text = "All Patients"
        Me.rdbAllPatients.UseVisualStyleBackColor = False
        '
        'rdbSelectedPatient
        '
        Me.rdbSelectedPatient.AutoSize = True
        Me.rdbSelectedPatient.BackColor = System.Drawing.Color.Transparent
        Me.rdbSelectedPatient.Checked = True
        Me.rdbSelectedPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSelectedPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rdbSelectedPatient.Location = New System.Drawing.Point(11, 22)
        Me.rdbSelectedPatient.Name = "rdbSelectedPatient"
        Me.rdbSelectedPatient.Size = New System.Drawing.Size(126, 18)
        Me.rdbSelectedPatient.TabIndex = 0
        Me.rdbSelectedPatient.TabStop = True
        Me.rdbSelectedPatient.Text = "Selected Patient"
        Me.rdbSelectedPatient.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox3.Controls.Add(Me.cmbStatus)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(14, 7)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(251, 55)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(17, 23)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(216, 22)
        Me.cmbStatus.TabIndex = 0
        '
        'pnlDrugDiagnosis
        '
        Me.pnlDrugDiagnosis.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDrugDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDrugDiagnosis.Controls.Add(Me.Label3)
        Me.pnlDrugDiagnosis.Controls.Add(Me.Label2)
        Me.pnlDrugDiagnosis.Controls.Add(Me.btnReset)
        Me.pnlDrugDiagnosis.Controls.Add(Me.GroupBox6)
        Me.pnlDrugDiagnosis.Controls.Add(Me.GroupBox10)
        Me.pnlDrugDiagnosis.Controls.Add(Me.GroupBox9)
        Me.pnlDrugDiagnosis.Controls.Add(Me.GroupBox5)
        Me.pnlDrugDiagnosis.Controls.Add(Me.GroupBox3)
        Me.pnlDrugDiagnosis.Controls.Add(Me.Label1)
        Me.pnlDrugDiagnosis.Controls.Add(Me.Label4)
        Me.pnlDrugDiagnosis.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDrugDiagnosis.Location = New System.Drawing.Point(0, 124)
        Me.pnlDrugDiagnosis.Name = "pnlDrugDiagnosis"
        Me.pnlDrugDiagnosis.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDrugDiagnosis.Size = New System.Drawing.Size(1166, 301)
        Me.pnlDrugDiagnosis.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1162, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 296)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 296)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label4"
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnReset.BackgroundImage = CType(resources.GetObject("btnReset.BackgroundImage"), System.Drawing.Image)
        Me.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(990, 22)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(74, 24)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox6.Controls.Add(Me.rdbDiagnosisDesc)
        Me.GroupBox6.Controls.Add(Me.rdbDiagnosisCode)
        Me.GroupBox6.Controls.Add(Me.chkDiagnosis)
        Me.GroupBox6.Controls.Add(Me.lblSearchDiagnosis)
        Me.GroupBox6.Controls.Add(Me.txtSearchDiagnosis)
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox6.Location = New System.Drawing.Point(769, 72)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(327, 217)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Diagnosis"
        '
        'rdbDiagnosisDesc
        '
        Me.rdbDiagnosisDesc.AutoSize = True
        Me.rdbDiagnosisDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.rdbDiagnosisDesc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbDiagnosisDesc.Location = New System.Drawing.Point(211, 48)
        Me.rdbDiagnosisDesc.Name = "rdbDiagnosisDesc"
        Me.rdbDiagnosisDesc.Size = New System.Drawing.Size(85, 18)
        Me.rdbDiagnosisDesc.TabIndex = 12
        Me.rdbDiagnosisDesc.Text = "Description"
        Me.rdbDiagnosisDesc.UseVisualStyleBackColor = False
        '
        'rdbDiagnosisCode
        '
        Me.rdbDiagnosisCode.AutoSize = True
        Me.rdbDiagnosisCode.Checked = True
        Me.rdbDiagnosisCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbDiagnosisCode.Location = New System.Drawing.Point(73, 48)
        Me.rdbDiagnosisCode.Name = "rdbDiagnosisCode"
        Me.rdbDiagnosisCode.Size = New System.Drawing.Size(56, 18)
        Me.rdbDiagnosisCode.TabIndex = 11
        Me.rdbDiagnosisCode.TabStop = True
        Me.rdbDiagnosisCode.Text = "Code"
        Me.rdbDiagnosisCode.UseVisualStyleBackColor = True
        '
        'chkDiagnosis
        '
        Me.chkDiagnosis.CheckOnClick = True
        Me.chkDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiagnosis.ForeColor = System.Drawing.Color.Black
        Me.chkDiagnosis.FormattingEnabled = True
        Me.chkDiagnosis.HorizontalScrollbar = True
        Me.chkDiagnosis.Location = New System.Drawing.Point(69, 99)
        Me.chkDiagnosis.Name = "chkDiagnosis"
        Me.chkDiagnosis.Size = New System.Drawing.Size(231, 106)
        Me.chkDiagnosis.TabIndex = 14
        '
        'lblSearchDiagnosis
        '
        Me.lblSearchDiagnosis.AutoSize = True
        Me.lblSearchDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchDiagnosis.Location = New System.Drawing.Point(8, 76)
        Me.lblSearchDiagnosis.Name = "lblSearchDiagnosis"
        Me.lblSearchDiagnosis.Size = New System.Drawing.Size(56, 14)
        Me.lblSearchDiagnosis.TabIndex = 11
        Me.lblSearchDiagnosis.Text = "Search :"
        '
        'txtSearchDiagnosis
        '
        Me.txtSearchDiagnosis.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchDiagnosis.Location = New System.Drawing.Point(69, 72)
        Me.txtSearchDiagnosis.Name = "txtSearchDiagnosis"
        Me.txtSearchDiagnosis.Size = New System.Drawing.Size(231, 22)
        Me.txtSearchDiagnosis.TabIndex = 13
        '
        'GroupBox10
        '
        Me.GroupBox10.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox10.Controls.Add(Me.lblAge)
        Me.GroupBox10.Controls.Add(Me.lblAgeTo)
        Me.GroupBox10.Controls.Add(Me.lblAgeFrom)
        Me.GroupBox10.Controls.Add(Me.cmbAgeTo)
        Me.GroupBox10.Controls.Add(Me.cmbAgeFrom)
        Me.GroupBox10.Controls.Add(Me.cmbAge)
        Me.GroupBox10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox10.Location = New System.Drawing.Point(593, 7)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(369, 55)
        Me.GroupBox10.TabIndex = 2
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Age"
        '
        'lblAge
        '
        Me.lblAge.AutoSize = True
        Me.lblAge.BackColor = System.Drawing.Color.Transparent
        Me.lblAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAge.Location = New System.Drawing.Point(15, 27)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(33, 14)
        Me.lblAge.TabIndex = 11
        Me.lblAge.Text = "Age "
        '
        'lblAgeTo
        '
        Me.lblAgeTo.AutoSize = True
        Me.lblAgeTo.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeTo.Location = New System.Drawing.Point(269, 27)
        Me.lblAgeTo.Name = "lblAgeTo"
        Me.lblAgeTo.Size = New System.Drawing.Size(26, 14)
        Me.lblAgeTo.TabIndex = 10
        Me.lblAgeTo.Text = "To "
        '
        'lblAgeFrom
        '
        Me.lblAgeFrom.AutoSize = True
        Me.lblAgeFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeFrom.Location = New System.Drawing.Point(163, 27)
        Me.lblAgeFrom.Name = "lblAgeFrom"
        Me.lblAgeFrom.Size = New System.Drawing.Size(34, 14)
        Me.lblAgeFrom.TabIndex = 9
        Me.lblAgeFrom.Text = "From"
        '
        'cmbAgeTo
        '
        Me.cmbAgeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeTo.FormattingEnabled = True
        Me.cmbAgeTo.Location = New System.Drawing.Point(296, 23)
        Me.cmbAgeTo.Name = "cmbAgeTo"
        Me.cmbAgeTo.Size = New System.Drawing.Size(64, 22)
        Me.cmbAgeTo.TabIndex = 4
        '
        'cmbAgeFrom
        '
        Me.cmbAgeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeFrom.FormattingEnabled = True
        Me.cmbAgeFrom.Location = New System.Drawing.Point(202, 23)
        Me.cmbAgeFrom.Name = "cmbAgeFrom"
        Me.cmbAgeFrom.Size = New System.Drawing.Size(64, 22)
        Me.cmbAgeFrom.TabIndex = 3
        '
        'cmbAge
        '
        Me.cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAge.FormattingEnabled = True
        Me.cmbAge.Location = New System.Drawing.Point(51, 23)
        Me.cmbAge.Name = "cmbAge"
        Me.cmbAge.Size = New System.Drawing.Size(107, 22)
        Me.cmbAge.TabIndex = 2
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox9.Controls.Add(Me.cmbGender)
        Me.GroupBox9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox9.Location = New System.Drawing.Point(325, 7)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(200, 55)
        Me.GroupBox9.TabIndex = 1
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Gender"
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Location = New System.Drawing.Point(12, 23)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(174, 22)
        Me.cmbGender.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox5.Controls.Add(Me.Panel3)
        Me.GroupBox5.Controls.Add(Me.lblSelectedDrugs)
        Me.GroupBox5.Controls.Add(Me.chkMedication)
        Me.GroupBox5.Controls.Add(Me.btnRemove)
        Me.GroupBox5.Controls.Add(Me.btnAddToSearch)
        Me.GroupBox5.Controls.Add(Me.trvDrugs)
        Me.GroupBox5.Controls.Add(Me.lblSearchDrug)
        Me.GroupBox5.Controls.Add(Me.txtSearchDrug)
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(14, 72)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(736, 217)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Drugs"
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Location = New System.Drawing.Point(360, 111)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(55, 25)
        Me.Panel3.TabIndex = 9
        Me.Panel3.Visible = False
        '
        'Panel1
        '
        'Me.Panel1.Controls.Add(Me.wdTemp)
        'Me.Panel1.Location = New System.Drawing.Point(8, 8)
        'Me.Panel1.Name = "Panel1"
        'Me.Panel1.Size = New System.Drawing.Size(20, 10)
        'Me.Panel1.TabIndex = 11
        'Me.Panel1.Visible = False
        ''
        ''wdTemp
        ''
        'Me.wdTemp.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.wdTemp.Enabled = True
        'Me.wdTemp.Location = New System.Drawing.Point(0, 0)
        'Me.wdTemp.Name = "wdTemp"
        'Me.wdTemp.OcxState = CType(resources.GetObject("wdTemp.OcxState"), System.Windows.Forms.AxHost.State)
        'Me.wdTemp.Size = New System.Drawing.Size(20, 10)
        'Me.wdTemp.TabIndex = 0
        ''
        'lblSelectedDrugs
        '
        Me.lblSelectedDrugs.AutoSize = True
        Me.lblSelectedDrugs.BackColor = System.Drawing.Color.Transparent
        Me.lblSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedDrugs.Location = New System.Drawing.Point(369, 30)
        Me.lblSelectedDrugs.Name = "lblSelectedDrugs"
        Me.lblSelectedDrugs.Size = New System.Drawing.Size(107, 14)
        Me.lblSelectedDrugs.TabIndex = 8
        Me.lblSelectedDrugs.Text = "Selected Drugs :"
        '
        'chkMedication
        '
        Me.chkMedication.CheckOnClick = True
        Me.chkMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMedication.ForeColor = System.Drawing.Color.Black
        Me.chkMedication.FormattingEnabled = True
        Me.chkMedication.HorizontalScrollbar = True
        Me.chkMedication.Location = New System.Drawing.Point(483, 54)
        Me.chkMedication.Name = "chkMedication"
        Me.chkMedication.Size = New System.Drawing.Size(231, 140)
        Me.chkMedication.TabIndex = 10
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.Location = New System.Drawing.Point(345, 54)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(92, 23)
        Me.btnRemove.TabIndex = 9
        Me.btnRemove.Text = "<< Remove"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'btnAddToSearch
        '
        Me.btnAddToSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAddToSearch.BackgroundImage = CType(resources.GetObject("btnAddToSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnAddToSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddToSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnAddToSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAddToSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddToSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToSearch.Location = New System.Drawing.Point(345, 171)
        Me.btnAddToSearch.Name = "btnAddToSearch"
        Me.btnAddToSearch.Size = New System.Drawing.Size(92, 23)
        Me.btnAddToSearch.TabIndex = 8
        Me.btnAddToSearch.Text = "Add >>"
        Me.btnAddToSearch.UseVisualStyleBackColor = False
        '
        'trvDrugs
        '
        Me.trvDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvDrugs.Location = New System.Drawing.Point(70, 54)
        Me.trvDrugs.Name = "trvDrugs"
        Me.trvDrugs.Size = New System.Drawing.Size(231, 140)
        Me.trvDrugs.TabIndex = 7
        '
        'lblSearchDrug
        '
        Me.lblSearchDrug.AutoSize = True
        Me.lblSearchDrug.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchDrug.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchDrug.Location = New System.Drawing.Point(11, 30)
        Me.lblSearchDrug.Name = "lblSearchDrug"
        Me.lblSearchDrug.Size = New System.Drawing.Size(56, 14)
        Me.lblSearchDrug.TabIndex = 1
        Me.lblSearchDrug.Text = "Search :"
        '
        'txtSearchDrug
        '
        Me.txtSearchDrug.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchDrug.Location = New System.Drawing.Point(70, 26)
        Me.txtSearchDrug.Name = "txtSearchDrug"
        Me.txtSearchDrug.Size = New System.Drawing.Size(231, 22)
        Me.txtSearchDrug.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 297)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1160, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1160, 1)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label1"
        '
        'C1grdExams
        '
        Me.C1grdExams.BackColor = System.Drawing.Color.GhostWhite
        Me.C1grdExams.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1grdExams.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1grdExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1grdExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1grdExams.Location = New System.Drawing.Point(4, 1)
        Me.C1grdExams.Name = "C1grdExams"
        Me.C1grdExams.Rows.Count = 1
        Me.C1grdExams.Rows.DefaultSize = 21
        Me.C1grdExams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1grdExams.Size = New System.Drawing.Size(1158, 191)
        Me.C1grdExams.StyleInfo = resources.GetString("C1grdExams.StyleInfo")
        Me.C1grdExams.TabIndex = 0
        '
        'pnlDSO
        '
        Me.pnlDSO.Controls.Add(Me.wdExam)
        Me.pnlDSO.Controls.Add(Me.pnlTempDSO)
        Me.pnlDSO.Controls.Add(Me.Label9)
        Me.pnlDSO.Controls.Add(Me.Label10)
        Me.pnlDSO.Controls.Add(Me.Label11)
        Me.pnlDSO.Controls.Add(Me.Label12)
        Me.pnlDSO.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDSO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlDSO.Location = New System.Drawing.Point(48, 64)
        Me.pnlDSO.Name = "pnlDSO"
        Me.pnlDSO.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDSO.Size = New System.Drawing.Size(208, 152)
        Me.pnlDSO.TabIndex = 8
        Me.pnlDSO.Visible = False
        '
        'wdExam
        '
        Me.wdExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdExam.Enabled = True
        Me.wdExam.Location = New System.Drawing.Point(4, 28)
        Me.wdExam.Name = "wdExam"
        Me.wdExam.OcxState = CType(resources.GetObject("wdExam.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdExam.Size = New System.Drawing.Size(200, 120)
        Me.wdExam.TabIndex = 0
        '
        'pnlTempDSO
        '
        Me.pnlTempDSO.BackColor = System.Drawing.Color.Transparent
        Me.pnlTempDSO.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTempDSO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTempDSO.Controls.Add(Me.lblExamName)
        Me.pnlTempDSO.Controls.Add(Me.picPriviewClose)
        Me.pnlTempDSO.Controls.Add(Me.Label13)
        Me.pnlTempDSO.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTempDSO.Location = New System.Drawing.Point(4, 4)
        Me.pnlTempDSO.Name = "pnlTempDSO"
        Me.pnlTempDSO.Size = New System.Drawing.Size(200, 24)
        Me.pnlTempDSO.TabIndex = 13
        '
        'lblExamName
        '
        Me.lblExamName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExamName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExamName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblExamName.Location = New System.Drawing.Point(0, 0)
        Me.lblExamName.Name = "lblExamName"
        Me.lblExamName.Size = New System.Drawing.Size(176, 23)
        Me.lblExamName.TabIndex = 13
        Me.lblExamName.Text = "Exam Name"
        Me.lblExamName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picPriviewClose
        '
        Me.picPriviewClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picPriviewClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.picPriviewClose.Image = CType(resources.GetObject("picPriviewClose.Image"), System.Drawing.Image)
        Me.picPriviewClose.Location = New System.Drawing.Point(176, 0)
        Me.picPriviewClose.Name = "picPriviewClose"
        Me.picPriviewClose.Size = New System.Drawing.Size(24, 23)
        Me.picPriviewClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picPriviewClose.TabIndex = 14
        Me.picPriviewClose.TabStop = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(200, 1)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(4, 148)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(200, 1)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 145)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(204, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 145)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(202, 1)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "label1"
        '
        'PrintProgress
        '
        Me.PrintProgress.BackColor = System.Drawing.Color.White
        Me.PrintProgress.ForeColor = System.Drawing.Color.LimeGreen
        Me.PrintProgress.Location = New System.Drawing.Point(842, 34)
        Me.PrintProgress.Name = "PrintProgress"
        Me.PrintProgress.Size = New System.Drawing.Size(162, 13)
        Me.PrintProgress.TabIndex = 2
        '
        'lblPrinting
        '
        Me.lblPrinting.AutoSize = True
        Me.lblPrinting.BackColor = System.Drawing.Color.Transparent
        Me.lblPrinting.Location = New System.Drawing.Point(845, 9)
        Me.lblPrinting.Name = "lblPrinting"
        Me.lblPrinting.Size = New System.Drawing.Size(72, 14)
        Me.lblPrinting.TabIndex = 0
        Me.lblPrinting.Text = "Printing......"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlDSO)
        Me.Panel2.Controls.Add(Me.C1grdExams)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 425)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(1166, 196)
        Me.Panel2.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1158, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 192)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1162, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 192)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1160, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cmnu_Diagnosis
        '
        Me.cmnu_Diagnosis.Name = "cmnu_Diagnosis"
        Me.cmnu_Diagnosis.Size = New System.Drawing.Size(61, 4)
        '
        'imgList_Common
        '
        Me.imgList_Common.ImageStream = CType(resources.GetObject("imgList_Common.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_Common.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Common.Images.SetKeyName(0, "DX01.ico")
        '
        'rbICD10
        '
        Me.rbICD10.AutoSize = True
        Me.rbICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD10.Location = New System.Drawing.Point(127, 10)
        Me.rbICD10.Name = "rbICD10"
        Me.rbICD10.Size = New System.Drawing.Size(58, 18)
        Me.rbICD10.TabIndex = 16
        Me.rbICD10.Text = "ICD10"
        Me.rbICD10.UseVisualStyleBackColor = True
        '
        'rbICD9
        '
        Me.rbICD9.AutoSize = True
        Me.rbICD9.Checked = True
        Me.rbICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9.Location = New System.Drawing.Point(36, 10)
        Me.rbICD9.Name = "rbICD9"
        Me.rbICD9.Size = New System.Drawing.Size(51, 18)
        Me.rbICD9.TabIndex = 15
        Me.rbICD9.TabStop = True
        Me.rbICD9.Text = "ICD9"
        Me.rbICD9.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.rbICD10)
        Me.GroupBox7.Controls.Add(Me.rbICD9)
        Me.GroupBox7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(73, 8)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(222, 33)
        Me.GroupBox7.TabIndex = 14
        Me.GroupBox7.TabStop = False
        '
        'frmPrintFAXExam
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1166, 621)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PrintProgress)
        Me.Controls.Add(Me.lblPrinting)
        Me.Controls.Add(Me.pnlDrugDiagnosis)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.tblStrip_32)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmPrintFAXExam"
        Me.ShowInTaskbar = False
        Me.Text = "Exams Print / Fax"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.pnlDrugDiagnosis.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        ' CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1grdExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDSO.ResumeLayout(False)
        CType(Me.wdExam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTempDSO.ResumeLayout(False)
        CType(Me.picPriviewClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_SelectAll_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_FAX_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Preview_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbAllPatients As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSelectedPatient As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDateTo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDateFrom As System.Windows.Forms.ComboBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents pnlDrugDiagnosis As System.Windows.Forms.Panel
    Friend WithEvents btnMore As System.Windows.Forms.Button
    Friend WithEvents btnShowExams As System.Windows.Forms.Button
    Friend WithEvents C1grdExams As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents dtpicTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblAgeFrom As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAgeTo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeFrom As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAge As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents lblAgeTo As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearchDiagnosis As System.Windows.Forms.Label
    Friend WithEvents txtSearchDiagnosis As System.Windows.Forms.TextBox
    Friend WithEvents chkDiagnosis As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlDSO As System.Windows.Forms.Panel
    Friend WithEvents wdExam As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlTempDSO As System.Windows.Forms.Panel
    Friend WithEvents lblExamName As System.Windows.Forms.Label
    Friend WithEvents picPriviewClose As System.Windows.Forms.PictureBox
    '  Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    ' Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents lblSelectedDrugs As System.Windows.Forms.Label
    Friend WithEvents chkMedication As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAddToSearch As System.Windows.Forms.Button
    Friend WithEvents trvDrugs As System.Windows.Forms.TreeView
    Friend WithEvents lblSearchDrug As System.Windows.Forms.Label
    Friend WithEvents txtSearchDrug As System.Windows.Forms.TextBox
    Friend WithEvents PrintProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents lblPrinting As System.Windows.Forms.Label
    Friend WithEvents rdbDiagnosisCode As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDiagnosisDesc As System.Windows.Forms.RadioButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnMore As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmnu_Diagnosis As System.Windows.Forms.ContextMenuStrip
    Private WithEvents imgList_Common As System.Windows.Forms.ImageList
    Friend WithEvents tblbtnSendCharges As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents rbICD10 As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD9 As System.Windows.Forms.RadioButton
End Class
