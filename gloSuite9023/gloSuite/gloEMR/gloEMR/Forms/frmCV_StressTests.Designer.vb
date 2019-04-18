<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_StressTests
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DtDateOfStudy}
            Dim cntControls() As System.Windows.Forms.Control = {DtDateOfStudy}
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_StressTests))
        Me.DtDateOfStudy = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.tsCatheterization = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlNarrativeSummary = New System.Windows.Forms.Panel
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.txtNarrativeSummary = New System.Windows.Forms.TextBox
        Me.lblNarrativeSummary = New System.Windows.Forms.Label
        Me.pnlLV = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lbl_PeakHeartRt = New System.Windows.Forms.Label
        Me.lbl_PeakBPMin = New System.Windows.Forms.Label
        Me.lbl__PeakBPMax = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.txt_PeakBPMax = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.txt_PeakBPMin = New System.Windows.Forms.TextBox
        Me.Label54 = New System.Windows.Forms.Label
        Me.txt_PeakHeartRt = New System.Windows.Forms.TextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.pnlSaturation = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lbl_RestHeartRt = New System.Windows.Forms.Label
        Me.txt_RestHeartRt = New System.Windows.Forms.TextBox
        Me.lbl_RestBPMin = New System.Windows.Forms.Label
        Me.txt_RestBPMin = New System.Windows.Forms.TextBox
        Me.lbl_RestBPMax = New System.Windows.Forms.Label
        Me.txt_RestBPMax = New System.Windows.Forms.TextBox
        Me.pnlPressure = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txt_EjectionFraction = New System.Windows.Forms.TextBox
        Me.lbl_EjectionFraction = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.lbl_TotExTime = New System.Windows.Forms.Label
        Me.txt_TotExTime = New System.Windows.Forms.TextBox
        Me.pnlSelectFromList = New System.Windows.Forms.Panel
        Me.cmbresult = New System.Windows.Forms.ComboBox
        Me.lbl_Result = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCPTCode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnClearAllCPT = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnClearCPT = New System.Windows.Forms.Button
        Me.btnBrowseCPT = New System.Windows.Forms.Button
        Me.lstCPTcode = New System.Windows.Forms.ListBox
        Me.lstPhysicians = New System.Windows.Forms.ListBox
        Me.btnBrowsePhyID = New System.Windows.Forms.Button
        Me.btnClearPhyID = New System.Windows.Forms.Button
        Me.BtnClearAllPhyID = New System.Windows.Forms.Button
        Me.lblPhyID = New System.Windows.Forms.Label
        Me.lstTestType = New System.Windows.Forms.ListBox
        Me.btnBrowseTesttype = New System.Windows.Forms.Button
        Me.lblTesttype = New System.Windows.Forms.Label
        Me.btnClearTesttype = New System.Windows.Forms.Button
        Me.BtnClearAllTesttype = New System.Windows.Forms.Button
        Me.pnlDateProcedure = New System.Windows.Forms.Panel
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.pnlCustomTask = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.C1CPTTest = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlPatientStrip = New System.Windows.Forms.Panel
        Me.C1CPTTstResult = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.tsCatheterization.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlNarrativeSummary.SuspendLayout()
        Me.pnlLV.SuspendLayout()
        Me.pnlSaturation.SuspendLayout()
        Me.pnlPressure.SuspendLayout()
        Me.pnlSelectFromList.SuspendLayout()
        Me.pnlDateProcedure.SuspendLayout()
        Me.pnlCustomTask.SuspendLayout()
        CType(Me.C1CPTTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        CType(Me.C1CPTTstResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DtDateOfStudy
        '
        Me.DtDateOfStudy.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DtDateOfStudy.CalendarMonthBackground = System.Drawing.Color.White
        Me.DtDateOfStudy.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DtDateOfStudy.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DtDateOfStudy.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DtDateOfStudy.CustomFormat = "MM/dd/yyyy"
        Me.DtDateOfStudy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtDateOfStudy.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtDateOfStudy.Location = New System.Drawing.Point(134, 9)
        Me.DtDateOfStudy.Name = "DtDateOfStudy"
        Me.DtDateOfStudy.Size = New System.Drawing.Size(109, 22)
        Me.DtDateOfStudy.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(14, 13)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(116, 14)
        Me.lblDate.TabIndex = 12
        Me.lblDate.Text = "Date of Procedure :"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tsCatheterization
        '
        Me.tsCatheterization.BackgroundImage = CType(resources.GetObject("tsCatheterization.BackgroundImage"), System.Drawing.Image)
        Me.tsCatheterization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsCatheterization.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsCatheterization.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsCatheterization.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tsCatheterization.Location = New System.Drawing.Point(0, 0)
        Me.tsCatheterization.Name = "tsCatheterization"
        Me.tsCatheterization.Size = New System.Drawing.Size(876, 53)
        Me.tsCatheterization.TabIndex = 1
        Me.tsCatheterization.TabStop = True
        Me.tsCatheterization.Text = "ToolStrip1"
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save.Tag = "Save"
        Me.tblbtn_Save.Text = "&Save&&Cls"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save.ToolTipText = "Save & Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlNarrativeSummary)
        Me.pnlMain.Controls.Add(Me.pnlLV)
        Me.pnlMain.Controls.Add(Me.pnlSaturation)
        Me.pnlMain.Controls.Add(Me.pnlPressure)
        Me.pnlMain.Controls.Add(Me.pnlSelectFromList)
        Me.pnlMain.Controls.Add(Me.pnlDateProcedure)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(876, 498)
        Me.pnlMain.TabIndex = 0
        '
        'pnlNarrativeSummary
        '
        Me.pnlNarrativeSummary.Controls.Add(Me.Label48)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label49)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label50)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label51)
        Me.pnlNarrativeSummary.Controls.Add(Me.txtNarrativeSummary)
        Me.pnlNarrativeSummary.Controls.Add(Me.lblNarrativeSummary)
        Me.pnlNarrativeSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNarrativeSummary.Location = New System.Drawing.Point(0, 327)
        Me.pnlNarrativeSummary.Name = "pnlNarrativeSummary"
        Me.pnlNarrativeSummary.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlNarrativeSummary.Size = New System.Drawing.Size(876, 171)
        Me.pnlNarrativeSummary.TabIndex = 6
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(4, 167)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(868, 1)
        Me.Label48.TabIndex = 296
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(4, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(868, 1)
        Me.Label49.TabIndex = 295
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(3, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 168)
        Me.Label50.TabIndex = 297
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(872, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 168)
        Me.Label51.TabIndex = 298
        '
        'txtNarrativeSummary
        '
        Me.txtNarrativeSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNarrativeSummary.ForeColor = System.Drawing.Color.Black
        Me.txtNarrativeSummary.Location = New System.Drawing.Point(149, 13)
        Me.txtNarrativeSummary.MaxLength = 5000
        Me.txtNarrativeSummary.Multiline = True
        Me.txtNarrativeSummary.Name = "txtNarrativeSummary"
        Me.txtNarrativeSummary.Size = New System.Drawing.Size(711, 138)
        Me.txtNarrativeSummary.TabIndex = 0
        '
        'lblNarrativeSummary
        '
        Me.lblNarrativeSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNarrativeSummary.AutoSize = True
        Me.lblNarrativeSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNarrativeSummary.Location = New System.Drawing.Point(30, 16)
        Me.lblNarrativeSummary.Name = "lblNarrativeSummary"
        Me.lblNarrativeSummary.Size = New System.Drawing.Size(117, 14)
        Me.lblNarrativeSummary.TabIndex = 289
        Me.lblNarrativeSummary.Text = "Narrative Summary :"
        Me.lblNarrativeSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLV
        '
        Me.pnlLV.Controls.Add(Me.Label16)
        Me.pnlLV.Controls.Add(Me.Label14)
        Me.pnlLV.Controls.Add(Me.Label11)
        Me.pnlLV.Controls.Add(Me.lbl_PeakHeartRt)
        Me.pnlLV.Controls.Add(Me.lbl_PeakBPMin)
        Me.pnlLV.Controls.Add(Me.lbl__PeakBPMax)
        Me.pnlLV.Controls.Add(Me.Label63)
        Me.pnlLV.Controls.Add(Me.txt_PeakBPMax)
        Me.pnlLV.Controls.Add(Me.Label52)
        Me.pnlLV.Controls.Add(Me.Label53)
        Me.pnlLV.Controls.Add(Me.txt_PeakBPMin)
        Me.pnlLV.Controls.Add(Me.Label54)
        Me.pnlLV.Controls.Add(Me.txt_PeakHeartRt)
        Me.pnlLV.Controls.Add(Me.Label55)
        Me.pnlLV.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLV.Location = New System.Drawing.Point(0, 260)
        Me.pnlLV.Name = "pnlLV"
        Me.pnlLV.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlLV.Size = New System.Drawing.Size(876, 67)
        Me.pnlLV.TabIndex = 4
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(529, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 11)
        Me.Label16.TabIndex = 307
        Me.Label16.Text = "(Diastolic)"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(457, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(43, 11)
        Me.Label14.TabIndex = 306
        Me.Label14.Text = "(Systolic)"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(507, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(15, 27)
        Me.Label11.TabIndex = 305
        Me.Label11.Text = "/"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_PeakHeartRt
        '
        Me.lbl_PeakHeartRt.AutoSize = True
        Me.lbl_PeakHeartRt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PeakHeartRt.Location = New System.Drawing.Point(73, 29)
        Me.lbl_PeakHeartRt.Name = "lbl_PeakHeartRt"
        Me.lbl_PeakHeartRt.Size = New System.Drawing.Size(74, 14)
        Me.lbl_PeakHeartRt.TabIndex = 302
        Me.lbl_PeakHeartRt.Text = "Heart Rate :"
        Me.lbl_PeakHeartRt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PeakBPMin
        '
        Me.lbl_PeakBPMin.AutoSize = True
        Me.lbl_PeakBPMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PeakBPMin.Location = New System.Drawing.Point(679, 29)
        Me.lbl_PeakBPMin.Name = "lbl_PeakBPMin"
        Me.lbl_PeakBPMin.Size = New System.Drawing.Size(51, 14)
        Me.lbl_PeakBPMin.TabIndex = 303
        Me.lbl_PeakBPMin.Text = "BP Min :"
        Me.lbl_PeakBPMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_PeakBPMin.Visible = False
        '
        'lbl__PeakBPMax
        '
        Me.lbl__PeakBPMax.AutoSize = True
        Me.lbl__PeakBPMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl__PeakBPMax.Location = New System.Drawing.Point(426, 29)
        Me.lbl__PeakBPMax.Name = "lbl__PeakBPMax"
        Me.lbl__PeakBPMax.Size = New System.Drawing.Size(29, 14)
        Me.lbl__PeakBPMax.TabIndex = 304
        Me.lbl__PeakBPMax.Text = "BP :"
        Me.lbl__PeakBPMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(4, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.Label63.Size = New System.Drawing.Size(134, 24)
        Me.Label63.TabIndex = 301
        Me.Label63.Text = " Peak Heart Rate :"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_PeakBPMax
        '
        Me.txt_PeakBPMax.ForeColor = System.Drawing.Color.Black
        Me.txt_PeakBPMax.Location = New System.Drawing.Point(457, 25)
        Me.txt_PeakBPMax.MaxLength = 10
        Me.txt_PeakBPMax.Name = "txt_PeakBPMax"
        Me.txt_PeakBPMax.Size = New System.Drawing.Size(45, 22)
        Me.txt_PeakBPMax.TabIndex = 1
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(4, 63)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(868, 1)
        Me.Label52.TabIndex = 296
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(4, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(868, 1)
        Me.Label53.TabIndex = 295
        '
        'txt_PeakBPMin
        '
        Me.txt_PeakBPMin.ForeColor = System.Drawing.Color.Black
        Me.txt_PeakBPMin.Location = New System.Drawing.Point(530, 25)
        Me.txt_PeakBPMin.MaxLength = 10
        Me.txt_PeakBPMin.Name = "txt_PeakBPMin"
        Me.txt_PeakBPMin.Size = New System.Drawing.Size(45, 22)
        Me.txt_PeakBPMin.TabIndex = 2
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(3, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 64)
        Me.Label54.TabIndex = 297
        '
        'txt_PeakHeartRt
        '
        Me.txt_PeakHeartRt.ForeColor = System.Drawing.Color.Black
        Me.txt_PeakHeartRt.Location = New System.Drawing.Point(149, 25)
        Me.txt_PeakHeartRt.MaxLength = 10
        Me.txt_PeakHeartRt.Name = "txt_PeakHeartRt"
        Me.txt_PeakHeartRt.Size = New System.Drawing.Size(109, 22)
        Me.txt_PeakHeartRt.TabIndex = 0
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(872, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 64)
        Me.Label55.TabIndex = 298
        '
        'pnlSaturation
        '
        Me.pnlSaturation.Controls.Add(Me.Label13)
        Me.pnlSaturation.Controls.Add(Me.Label12)
        Me.pnlSaturation.Controls.Add(Me.Label15)
        Me.pnlSaturation.Controls.Add(Me.Label61)
        Me.pnlSaturation.Controls.Add(Me.Label43)
        Me.pnlSaturation.Controls.Add(Me.Label45)
        Me.pnlSaturation.Controls.Add(Me.Label46)
        Me.pnlSaturation.Controls.Add(Me.Label47)
        Me.pnlSaturation.Controls.Add(Me.lbl_RestHeartRt)
        Me.pnlSaturation.Controls.Add(Me.txt_RestHeartRt)
        Me.pnlSaturation.Controls.Add(Me.lbl_RestBPMin)
        Me.pnlSaturation.Controls.Add(Me.txt_RestBPMin)
        Me.pnlSaturation.Controls.Add(Me.lbl_RestBPMax)
        Me.pnlSaturation.Controls.Add(Me.txt_RestBPMax)
        Me.pnlSaturation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSaturation.Location = New System.Drawing.Point(0, 191)
        Me.pnlSaturation.Name = "pnlSaturation"
        Me.pnlSaturation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSaturation.Size = New System.Drawing.Size(876, 69)
        Me.pnlSaturation.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(530, 49)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 11)
        Me.Label13.TabIndex = 302
        Me.Label13.Text = "(Diastolic)"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(457, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 11)
        Me.Label12.TabIndex = 301
        Me.Label12.Text = "(Systolic)"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(507, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(15, 27)
        Me.Label15.TabIndex = 300
        Me.Label15.Text = "/"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(4, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.Label61.Size = New System.Drawing.Size(148, 24)
        Me.Label61.TabIndex = 299
        Me.Label61.Text = "Resting Heart Rate :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(4, 65)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(868, 1)
        Me.Label43.TabIndex = 296
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(4, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(868, 1)
        Me.Label45.TabIndex = 295
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(3, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 66)
        Me.Label46.TabIndex = 297
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(872, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 66)
        Me.Label47.TabIndex = 298
        '
        'lbl_RestHeartRt
        '
        Me.lbl_RestHeartRt.AutoSize = True
        Me.lbl_RestHeartRt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RestHeartRt.Location = New System.Drawing.Point(73, 29)
        Me.lbl_RestHeartRt.Name = "lbl_RestHeartRt"
        Me.lbl_RestHeartRt.Size = New System.Drawing.Size(74, 14)
        Me.lbl_RestHeartRt.TabIndex = 257
        Me.lbl_RestHeartRt.Text = "Heart Rate :"
        Me.lbl_RestHeartRt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_RestHeartRt
        '
        Me.txt_RestHeartRt.ForeColor = System.Drawing.Color.Black
        Me.txt_RestHeartRt.Location = New System.Drawing.Point(149, 25)
        Me.txt_RestHeartRt.MaxLength = 10
        Me.txt_RestHeartRt.Name = "txt_RestHeartRt"
        Me.txt_RestHeartRt.Size = New System.Drawing.Size(109, 22)
        Me.txt_RestHeartRt.TabIndex = 0
        '
        'lbl_RestBPMin
        '
        Me.lbl_RestBPMin.AutoSize = True
        Me.lbl_RestBPMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RestBPMin.Location = New System.Drawing.Point(678, 29)
        Me.lbl_RestBPMin.Name = "lbl_RestBPMin"
        Me.lbl_RestBPMin.Size = New System.Drawing.Size(51, 14)
        Me.lbl_RestBPMin.TabIndex = 259
        Me.lbl_RestBPMin.Text = "BP Min :"
        Me.lbl_RestBPMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_RestBPMin.Visible = False
        '
        'txt_RestBPMin
        '
        Me.txt_RestBPMin.ForeColor = System.Drawing.Color.Black
        Me.txt_RestBPMin.Location = New System.Drawing.Point(530, 25)
        Me.txt_RestBPMin.MaxLength = 10
        Me.txt_RestBPMin.Name = "txt_RestBPMin"
        Me.txt_RestBPMin.Size = New System.Drawing.Size(45, 22)
        Me.txt_RestBPMin.TabIndex = 2
        '
        'lbl_RestBPMax
        '
        Me.lbl_RestBPMax.AutoSize = True
        Me.lbl_RestBPMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RestBPMax.Location = New System.Drawing.Point(426, 29)
        Me.lbl_RestBPMax.Name = "lbl_RestBPMax"
        Me.lbl_RestBPMax.Size = New System.Drawing.Size(29, 14)
        Me.lbl_RestBPMax.TabIndex = 261
        Me.lbl_RestBPMax.Text = "BP :"
        Me.lbl_RestBPMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_RestBPMax
        '
        Me.txt_RestBPMax.ForeColor = System.Drawing.Color.Black
        Me.txt_RestBPMax.Location = New System.Drawing.Point(457, 25)
        Me.txt_RestBPMax.MaxLength = 10
        Me.txt_RestBPMax.Name = "txt_RestBPMax"
        Me.txt_RestBPMax.Size = New System.Drawing.Size(45, 22)
        Me.txt_RestBPMax.TabIndex = 1
        '
        'pnlPressure
        '
        Me.pnlPressure.Controls.Add(Me.Label10)
        Me.pnlPressure.Controls.Add(Me.Label9)
        Me.pnlPressure.Controls.Add(Me.txt_EjectionFraction)
        Me.pnlPressure.Controls.Add(Me.lbl_EjectionFraction)
        Me.pnlPressure.Controls.Add(Me.Label38)
        Me.pnlPressure.Controls.Add(Me.Label39)
        Me.pnlPressure.Controls.Add(Me.Label40)
        Me.pnlPressure.Controls.Add(Me.Label41)
        Me.pnlPressure.Controls.Add(Me.lbl_TotExTime)
        Me.pnlPressure.Controls.Add(Me.txt_TotExTime)
        Me.pnlPressure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPressure.Location = New System.Drawing.Point(0, 153)
        Me.pnlPressure.Name = "pnlPressure"
        Me.pnlPressure.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPressure.Size = New System.Drawing.Size(876, 38)
        Me.pnlPressure.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(572, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(18, 13)
        Me.Label10.TabIndex = 302
        Me.Label10.Text = "%"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(261, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 301
        Me.Label9.Text = "min."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_EjectionFraction
        '
        Me.txt_EjectionFraction.ForeColor = System.Drawing.Color.Black
        Me.txt_EjectionFraction.Location = New System.Drawing.Point(457, 7)
        Me.txt_EjectionFraction.MaxLength = 50
        Me.txt_EjectionFraction.Name = "txt_EjectionFraction"
        Me.txt_EjectionFraction.Size = New System.Drawing.Size(109, 22)
        Me.txt_EjectionFraction.TabIndex = 2
        '
        'lbl_EjectionFraction
        '
        Me.lbl_EjectionFraction.AutoSize = True
        Me.lbl_EjectionFraction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EjectionFraction.Location = New System.Drawing.Point(349, 11)
        Me.lbl_EjectionFraction.Name = "lbl_EjectionFraction"
        Me.lbl_EjectionFraction.Size = New System.Drawing.Size(106, 14)
        Me.lbl_EjectionFraction.TabIndex = 299
        Me.lbl_EjectionFraction.Text = "Ejection Fraction :"
        Me.lbl_EjectionFraction.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(4, 34)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(868, 1)
        Me.Label38.TabIndex = 296
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(868, 1)
        Me.Label39.TabIndex = 295
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 35)
        Me.Label40.TabIndex = 297
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(872, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 35)
        Me.Label41.TabIndex = 298
        '
        'lbl_TotExTime
        '
        Me.lbl_TotExTime.AutoSize = True
        Me.lbl_TotExTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TotExTime.Location = New System.Drawing.Point(26, 10)
        Me.lbl_TotExTime.Name = "lbl_TotExTime"
        Me.lbl_TotExTime.Size = New System.Drawing.Size(122, 14)
        Me.lbl_TotExTime.TabIndex = 259
        Me.lbl_TotExTime.Text = "Total Exercise Time :"
        Me.lbl_TotExTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_TotExTime
        '
        Me.txt_TotExTime.ForeColor = System.Drawing.Color.Black
        Me.txt_TotExTime.Location = New System.Drawing.Point(149, 6)
        Me.txt_TotExTime.MaxLength = 50
        Me.txt_TotExTime.Name = "txt_TotExTime"
        Me.txt_TotExTime.Size = New System.Drawing.Size(109, 22)
        Me.txt_TotExTime.TabIndex = 1
        '
        'pnlSelectFromList
        '
        Me.pnlSelectFromList.Controls.Add(Me.cmbresult)
        Me.pnlSelectFromList.Controls.Add(Me.lbl_Result)
        Me.pnlSelectFromList.Controls.Add(Me.Label4)
        Me.pnlSelectFromList.Controls.Add(Me.Label1)
        Me.pnlSelectFromList.Controls.Add(Me.lblCPTCode)
        Me.pnlSelectFromList.Controls.Add(Me.Label5)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllCPT)
        Me.pnlSelectFromList.Controls.Add(Me.Label6)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearCPT)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseCPT)
        Me.pnlSelectFromList.Controls.Add(Me.lstCPTcode)
        Me.pnlSelectFromList.Controls.Add(Me.lstPhysicians)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowsePhyID)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearPhyID)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllPhyID)
        Me.pnlSelectFromList.Controls.Add(Me.lblPhyID)
        Me.pnlSelectFromList.Controls.Add(Me.lstTestType)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseTesttype)
        Me.pnlSelectFromList.Controls.Add(Me.lblTesttype)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearTesttype)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllTesttype)
        Me.pnlSelectFromList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectFromList.Location = New System.Drawing.Point(0, 41)
        Me.pnlSelectFromList.Name = "pnlSelectFromList"
        Me.pnlSelectFromList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSelectFromList.Size = New System.Drawing.Size(876, 112)
        Me.pnlSelectFromList.TabIndex = 1
        '
        'cmbresult
        '
        Me.cmbresult.AllowDrop = True
        Me.cmbresult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbresult.FormattingEnabled = True
        Me.cmbresult.Items.AddRange(New Object() {"Abnormal", "Indeterminate", "Normal"})
        Me.cmbresult.Location = New System.Drawing.Point(398, 83)
        Me.cmbresult.Name = "cmbresult"
        Me.cmbresult.Size = New System.Drawing.Size(177, 22)
        Me.cmbresult.Sorted = True
        Me.cmbresult.TabIndex = 8
        '
        'lbl_Result
        '
        Me.lbl_Result.AutoSize = True
        Me.lbl_Result.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Result.Location = New System.Drawing.Point(346, 87)
        Me.lbl_Result.Name = "lbl_Result"
        Me.lbl_Result.Size = New System.Drawing.Size(48, 14)
        Me.lbl_Result.TabIndex = 300
        Me.lbl_Result.Text = "Result :"
        Me.lbl_Result.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(868, 1)
        Me.Label4.TabIndex = 296
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(868, 1)
        Me.Label1.TabIndex = 295
        '
        'lblCPTCode
        '
        Me.lblCPTCode.AutoSize = True
        Me.lblCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPTCode.Location = New System.Drawing.Point(16, 9)
        Me.lblCPTCode.Name = "lblCPTCode"
        Me.lblCPTCode.Size = New System.Drawing.Size(37, 14)
        Me.lblCPTCode.TabIndex = 294
        Me.lblCPTCode.Text = "CPT :"
        Me.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 109)
        Me.Label5.TabIndex = 297
        '
        'BtnClearAllCPT
        '
        Me.BtnClearAllCPT.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllCPT.BackgroundImage = CType(resources.GetObject("BtnClearAllCPT.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllCPT.Image = CType(resources.GetObject("BtnClearAllCPT.Image"), System.Drawing.Image)
        Me.BtnClearAllCPT.Location = New System.Drawing.Point(212, 58)
        Me.BtnClearAllCPT.Name = "BtnClearAllCPT"
        Me.BtnClearAllCPT.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllCPT.TabIndex = 3
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllCPT, "Clear All CPT")
        Me.BtnClearAllCPT.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(872, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 109)
        Me.Label6.TabIndex = 298
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(212, 32)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnClearCPT.TabIndex = 2
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearCPT, "Clear CPT")
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'btnBrowseCPT
        '
        Me.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCPT.BackgroundImage = CType(resources.GetObject("btnBrowseCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCPT.Image = CType(resources.GetObject("btnBrowseCPT.Image"), System.Drawing.Image)
        Me.btnBrowseCPT.Location = New System.Drawing.Point(212, 7)
        Me.btnBrowseCPT.Name = "btnBrowseCPT"
        Me.btnBrowseCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseCPT.TabIndex = 1
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseCPT, "Browse CPT")
        Me.btnBrowseCPT.UseVisualStyleBackColor = False
        '
        'lstCPTcode
        '
        Me.lstCPTcode.FormattingEnabled = True
        Me.lstCPTcode.ItemHeight = 14
        Me.lstCPTcode.Location = New System.Drawing.Point(56, 6)
        Me.lstCPTcode.Name = "lstCPTcode"
        Me.lstCPTcode.Size = New System.Drawing.Size(152, 74)
        Me.lstCPTcode.TabIndex = 0
        '
        'lstPhysicians
        '
        Me.lstPhysicians.FormattingEnabled = True
        Me.lstPhysicians.ItemHeight = 14
        Me.lstPhysicians.Location = New System.Drawing.Point(682, 6)
        Me.lstPhysicians.Name = "lstPhysicians"
        Me.lstPhysicians.Size = New System.Drawing.Size(152, 74)
        Me.lstPhysicians.TabIndex = 9
        '
        'btnBrowsePhyID
        '
        Me.btnBrowsePhyID.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePhyID.BackgroundImage = CType(resources.GetObject("btnBrowsePhyID.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsePhyID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePhyID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePhyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePhyID.Image = CType(resources.GetObject("btnBrowsePhyID.Image"), System.Drawing.Image)
        Me.btnBrowsePhyID.Location = New System.Drawing.Point(838, 6)
        Me.btnBrowsePhyID.Name = "btnBrowsePhyID"
        Me.btnBrowsePhyID.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowsePhyID.TabIndex = 10
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowsePhyID, "Browse Physician")
        Me.btnBrowsePhyID.UseVisualStyleBackColor = False
        '
        'btnClearPhyID
        '
        Me.btnClearPhyID.BackColor = System.Drawing.Color.Transparent
        Me.btnClearPhyID.BackgroundImage = CType(resources.GetObject("btnClearPhyID.BackgroundImage"), System.Drawing.Image)
        Me.btnClearPhyID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPhyID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearPhyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPhyID.Image = CType(resources.GetObject("btnClearPhyID.Image"), System.Drawing.Image)
        Me.btnClearPhyID.Location = New System.Drawing.Point(838, 32)
        Me.btnClearPhyID.Name = "btnClearPhyID"
        Me.btnClearPhyID.Size = New System.Drawing.Size(22, 22)
        Me.btnClearPhyID.TabIndex = 11
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearPhyID, "Clear Physician")
        Me.btnClearPhyID.UseVisualStyleBackColor = False
        '
        'BtnClearAllPhyID
        '
        Me.BtnClearAllPhyID.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllPhyID.BackgroundImage = CType(resources.GetObject("BtnClearAllPhyID.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllPhyID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllPhyID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllPhyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllPhyID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllPhyID.Image = CType(resources.GetObject("BtnClearAllPhyID.Image"), System.Drawing.Image)
        Me.BtnClearAllPhyID.Location = New System.Drawing.Point(838, 58)
        Me.BtnClearAllPhyID.Name = "BtnClearAllPhyID"
        Me.BtnClearAllPhyID.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllPhyID.TabIndex = 12
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllPhyID, "Clear All Physicians")
        Me.BtnClearAllPhyID.UseVisualStyleBackColor = False
        '
        'lblPhyID
        '
        Me.lblPhyID.AutoSize = True
        Me.lblPhyID.BackColor = System.Drawing.Color.Transparent
        Me.lblPhyID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhyID.Location = New System.Drawing.Point(601, 9)
        Me.lblPhyID.Name = "lblPhyID"
        Me.lblPhyID.Size = New System.Drawing.Size(78, 14)
        Me.lblPhyID.TabIndex = 233
        Me.lblPhyID.Text = "Physician(s) :"
        Me.lblPhyID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstTestType
        '
        Me.lstTestType.FormattingEnabled = True
        Me.lstTestType.ItemHeight = 14
        Me.lstTestType.Location = New System.Drawing.Point(398, 6)
        Me.lstTestType.Name = "lstTestType"
        Me.lstTestType.Size = New System.Drawing.Size(152, 74)
        Me.lstTestType.TabIndex = 4
        '
        'btnBrowseTesttype
        '
        Me.btnBrowseTesttype.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseTesttype.BackgroundImage = CType(resources.GetObject("btnBrowseTesttype.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseTesttype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseTesttype.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseTesttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseTesttype.Image = CType(resources.GetObject("btnBrowseTesttype.Image"), System.Drawing.Image)
        Me.btnBrowseTesttype.Location = New System.Drawing.Point(553, 6)
        Me.btnBrowseTesttype.Name = "btnBrowseTesttype"
        Me.btnBrowseTesttype.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseTesttype.TabIndex = 5
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseTesttype, "Browse Test Type")
        Me.btnBrowseTesttype.UseVisualStyleBackColor = False
        '
        'lblTesttype
        '
        Me.lblTesttype.AutoSize = True
        Me.lblTesttype.BackColor = System.Drawing.Color.Transparent
        Me.lblTesttype.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTesttype.Location = New System.Drawing.Point(257, 9)
        Me.lblTesttype.Name = "lblTesttype"
        Me.lblTesttype.Size = New System.Drawing.Size(137, 14)
        Me.lblTesttype.TabIndex = 218
        Me.lblTesttype.Text = "CPT Coded Test Type :"
        Me.lblTesttype.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClearTesttype
        '
        Me.btnClearTesttype.BackColor = System.Drawing.Color.Transparent
        Me.btnClearTesttype.BackgroundImage = CType(resources.GetObject("btnClearTesttype.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTesttype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearTesttype.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearTesttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearTesttype.Image = CType(resources.GetObject("btnClearTesttype.Image"), System.Drawing.Image)
        Me.btnClearTesttype.Location = New System.Drawing.Point(553, 32)
        Me.btnClearTesttype.Name = "btnClearTesttype"
        Me.btnClearTesttype.Size = New System.Drawing.Size(22, 22)
        Me.btnClearTesttype.TabIndex = 6
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearTesttype, "Clear Test Type")
        Me.btnClearTesttype.UseVisualStyleBackColor = False
        '
        'BtnClearAllTesttype
        '
        Me.BtnClearAllTesttype.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllTesttype.BackgroundImage = CType(resources.GetObject("BtnClearAllTesttype.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllTesttype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllTesttype.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllTesttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllTesttype.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllTesttype.Image = CType(resources.GetObject("BtnClearAllTesttype.Image"), System.Drawing.Image)
        Me.BtnClearAllTesttype.Location = New System.Drawing.Point(553, 58)
        Me.BtnClearAllTesttype.Name = "BtnClearAllTesttype"
        Me.BtnClearAllTesttype.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllTesttype.TabIndex = 7
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllTesttype, "Clear All Test Types")
        Me.BtnClearAllTesttype.UseVisualStyleBackColor = False
        '
        'pnlDateProcedure
        '
        Me.pnlDateProcedure.Controls.Add(Me.Label67)
        Me.pnlDateProcedure.Controls.Add(Me.Label66)
        Me.pnlDateProcedure.Controls.Add(Me.Label65)
        Me.pnlDateProcedure.Controls.Add(Me.Label64)
        Me.pnlDateProcedure.Controls.Add(Me.DtDateOfStudy)
        Me.pnlDateProcedure.Controls.Add(Me.lblDate)
        Me.pnlDateProcedure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDateProcedure.Location = New System.Drawing.Point(0, 0)
        Me.pnlDateProcedure.Name = "pnlDateProcedure"
        Me.pnlDateProcedure.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDateProcedure.Size = New System.Drawing.Size(876, 41)
        Me.pnlDateProcedure.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(4, 37)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(868, 1)
        Me.Label67.TabIndex = 301
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(4, 3)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(868, 1)
        Me.Label66.TabIndex = 300
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(872, 3)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 35)
        Me.Label65.TabIndex = 299
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(3, 3)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 35)
        Me.Label64.TabIndex = 298
        '
        'pnlCustomTask
        '
        Me.pnlCustomTask.Controls.Add(Me.Label8)
        Me.pnlCustomTask.Controls.Add(Me.Label7)
        Me.pnlCustomTask.Controls.Add(Me.Label3)
        Me.pnlCustomTask.Controls.Add(Me.Label2)
        Me.pnlCustomTask.Location = New System.Drawing.Point(210, 127)
        Me.pnlCustomTask.Name = "pnlCustomTask"
        Me.pnlCustomTask.Size = New System.Drawing.Size(230, 105)
        Me.pnlCustomTask.TabIndex = 299
        Me.pnlCustomTask.TabStop = True
        Me.pnlCustomTask.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(228, 1)
        Me.Label8.TabIndex = 301
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(228, 1)
        Me.Label7.TabIndex = 300
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(229, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 105)
        Me.Label3.TabIndex = 299
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 105)
        Me.Label2.TabIndex = 298
        '
        'C1CPTTest
        '
        Me.C1CPTTest.ColumnInfo = "3,0,0,0,0,95,Columns:0{AllowEditing:False;Style:""DataType:System.String;TextAlign" & _
            ":LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{AllowSorting:False;AllowEditing:False;Style:""DataType:System.S" & _
            "tring;TextAlign:LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1CPTTest.ExtendLastCol = True
        Me.C1CPTTest.Location = New System.Drawing.Point(117, 1)
        Me.C1CPTTest.Name = "C1CPTTest"
        Me.C1CPTTest.Rows.Count = 1
        Me.C1CPTTest.Rows.DefaultSize = 19
        Me.C1CPTTest.Rows.Fixed = 0
        Me.C1CPTTest.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None
        Me.C1CPTTest.Size = New System.Drawing.Size(367, 105)
        Me.C1CPTTest.StyleInfo = resources.GetString("C1CPTTest.StyleInfo")
        Me.C1CPTTest.TabIndex = 299
        Me.C1CPTTest.Visible = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tsCatheterization)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(876, 56)
        Me.pnlToolStrip.TabIndex = 1
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlPatientStrip
        '
        Me.pnlPatientStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientStrip.Location = New System.Drawing.Point(167, 261)
        Me.pnlPatientStrip.Name = "pnlPatientStrip"
        Me.pnlPatientStrip.Size = New System.Drawing.Size(765, 54)
        Me.pnlPatientStrip.TabIndex = 300
        Me.pnlPatientStrip.Visible = False
        '
        'C1CPTTstResult
        '
        Me.C1CPTTstResult.ColumnInfo = "3,0,0,0,0,95,Columns:0{AllowEditing:False;Style:""DataType:System.String;TextAlign" & _
            ":LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{AllowSorting:False;AllowEditing:False;Style:""DataType:System.S" & _
            "tring;TextAlign:LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1CPTTstResult.ExtendLastCol = True
        Me.C1CPTTstResult.Location = New System.Drawing.Point(496, 0)
        Me.C1CPTTstResult.Name = "C1CPTTstResult"
        Me.C1CPTTstResult.Rows.Count = 1
        Me.C1CPTTstResult.Rows.DefaultSize = 19
        Me.C1CPTTstResult.Rows.Fixed = 0
        Me.C1CPTTstResult.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None
        Me.C1CPTTstResult.Size = New System.Drawing.Size(367, 105)
        Me.C1CPTTstResult.StyleInfo = resources.GetString("C1CPTTstResult.StyleInfo")
        Me.C1CPTTstResult.TabIndex = 301
        Me.C1CPTTstResult.Visible = False
        '
        'frmCV_StressTests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(876, 554)
        Me.Controls.Add(Me.pnlCustomTask)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlPatientStrip)
        Me.Controls.Add(Me.C1CPTTstResult)
        Me.Controls.Add(Me.C1CPTTest)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_StressTests"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Stress Test"
        Me.tsCatheterization.ResumeLayout(False)
        Me.tsCatheterization.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlNarrativeSummary.ResumeLayout(False)
        Me.pnlNarrativeSummary.PerformLayout()
        Me.pnlLV.ResumeLayout(False)
        Me.pnlLV.PerformLayout()
        Me.pnlSaturation.ResumeLayout(False)
        Me.pnlSaturation.PerformLayout()
        Me.pnlPressure.ResumeLayout(False)
        Me.pnlPressure.PerformLayout()
        Me.pnlSelectFromList.ResumeLayout(False)
        Me.pnlSelectFromList.PerformLayout()
        Me.pnlDateProcedure.ResumeLayout(False)
        Me.pnlDateProcedure.PerformLayout()
        Me.pnlCustomTask.ResumeLayout(False)
        CType(Me.C1CPTTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        CType(Me.C1CPTTstResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsCatheterization As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents DtDateOfStudy As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblTesttype As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllTesttype As System.Windows.Forms.Button
    Friend WithEvents btnClearTesttype As System.Windows.Forms.Button
    Friend WithEvents btnBrowseTesttype As System.Windows.Forms.Button
    Friend WithEvents lstTestType As System.Windows.Forms.ListBox
    Friend WithEvents lblPhyID As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllPhyID As System.Windows.Forms.Button
    Friend WithEvents btnClearPhyID As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePhyID As System.Windows.Forms.Button
    Friend WithEvents lstPhysicians As System.Windows.Forms.ListBox
    Friend WithEvents lbl_TotExTime As System.Windows.Forms.Label
    Friend WithEvents txt_TotExTime As System.Windows.Forms.TextBox
    Friend WithEvents txt_RestBPMax As System.Windows.Forms.TextBox
    Friend WithEvents lbl_RestBPMax As System.Windows.Forms.Label
    Friend WithEvents txt_RestBPMin As System.Windows.Forms.TextBox
    Friend WithEvents lbl_RestBPMin As System.Windows.Forms.Label
    Friend WithEvents txt_RestHeartRt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_RestHeartRt As System.Windows.Forms.Label
    Friend WithEvents txt_PeakBPMin As System.Windows.Forms.TextBox
    Friend WithEvents txt_PeakHeartRt As System.Windows.Forms.TextBox
    Friend WithEvents txt_PeakBPMax As System.Windows.Forms.TextBox
    Friend WithEvents txtNarrativeSummary As System.Windows.Forms.TextBox
    Friend WithEvents lblNarrativeSummary As System.Windows.Forms.Label
    Friend WithEvents lblCPTCode As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllCPT As System.Windows.Forms.Button
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button
    Friend WithEvents btnBrowseCPT As System.Windows.Forms.Button
    Friend WithEvents lstCPTcode As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlLV As System.Windows.Forms.Panel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents pnlNarrativeSummary As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents pnlSaturation As System.Windows.Forms.Panel
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents pnlPressure As System.Windows.Forms.Panel
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectFromList As System.Windows.Forms.Panel
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlDateProcedure As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents pnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents C1CPTTest As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlPatientStrip As System.Windows.Forms.Panel
    Friend WithEvents lbl_PeakHeartRt As System.Windows.Forms.Label
    Friend WithEvents lbl_PeakBPMin As System.Windows.Forms.Label
    Friend WithEvents lbl__PeakBPMax As System.Windows.Forms.Label
    Friend WithEvents txt_EjectionFraction As System.Windows.Forms.TextBox
    Friend WithEvents lbl_EjectionFraction As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbresult As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Result As System.Windows.Forms.Label
    Friend WithEvents C1CPTTstResult As C1.Win.C1FlexGrid.C1FlexGrid
End Class
