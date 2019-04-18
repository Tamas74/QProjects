<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_Catheterization
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DtProcDate}
            Dim cntControls() As System.Windows.Forms.Control = {DtProcDate}

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
        Try
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch

            End Try
        Catch ex As Exception

        End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_Catheterization))
        Me.DtProcDate = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.tsCatheterization = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlNarrativeSummary = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNarrativeSummary = New System.Windows.Forms.TextBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.lblNarrativeSummary = New System.Windows.Forms.Label
        Me.pnlLV = New System.Windows.Forms.Panel
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.txtLV_SystolicVolume = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.lblLV_SystolicVolume = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.txtLV_DiastolicVolume = New System.Windows.Forms.TextBox
        Me.lblLV_DiastolicVolume = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.txtLV_EjectionFraction = New System.Windows.Forms.TextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.lblLV_EjectionFraction = New System.Windows.Forms.Label
        Me.pnlSaturation = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lblS_IVC = New System.Windows.Forms.Label
        Me.txtS_IVC = New System.Windows.Forms.TextBox
        Me.lblS_SVC = New System.Windows.Forms.Label
        Me.txtS_SVC = New System.Windows.Forms.TextBox
        Me.txtS_PA = New System.Windows.Forms.TextBox
        Me.lblS_RA = New System.Windows.Forms.Label
        Me.lblS_PA = New System.Windows.Forms.Label
        Me.txtS_RA = New System.Windows.Forms.TextBox
        Me.lblS_RV = New System.Windows.Forms.Label
        Me.txtS_RV = New System.Windows.Forms.TextBox
        Me.pnlPressure = New System.Windows.Forms.Panel
        Me.Label60 = New System.Windows.Forms.Label
        Me.txtP_Mean = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.lblP_Mean = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.txtP_Diastolic = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.lblP_Diastolic = New System.Windows.Forms.Label
        Me.txtP_Peak = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.lblP_Peak = New System.Windows.Forms.Label
        Me.txtP_PA = New System.Windows.Forms.TextBox
        Me.lblP_RA = New System.Windows.Forms.Label
        Me.lblP_PA = New System.Windows.Forms.Label
        Me.txtP_RA = New System.Windows.Forms.TextBox
        Me.lblP_LA = New System.Windows.Forms.Label
        Me.txtP_LA = New System.Windows.Forms.TextBox
        Me.txtP_LV = New System.Windows.Forms.TextBox
        Me.lblP_RightPulmonary = New System.Windows.Forms.Label
        Me.lblP_LVPeak = New System.Windows.Forms.Label
        Me.txtP_RightPulmonary = New System.Windows.Forms.TextBox
        Me.txtP_RV = New System.Windows.Forms.TextBox
        Me.lblP_LeftPulmonary = New System.Windows.Forms.Label
        Me.lblP_RVPeak = New System.Windows.Forms.Label
        Me.txtP_LeftPulmonary = New System.Windows.Forms.TextBox
        Me.pnlSelectFromList = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCPTCode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnClearAllCPT = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnClearCPT = New System.Windows.Forms.Button
        Me.LblIntervention = New System.Windows.Forms.Label
        Me.btnBrowseCPT = New System.Windows.Forms.Button
        Me.BtnClearAllIntervn = New System.Windows.Forms.Button
        Me.btnClearIntervn = New System.Windows.Forms.Button
        Me.lstCPTcode = New System.Windows.Forms.ListBox
        Me.btnBrowseIntervn = New System.Windows.Forms.Button
        Me.lstPhysicians = New System.Windows.Forms.ListBox
        Me.lstIntervention = New System.Windows.Forms.ListBox
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
        Me.SuspendLayout()
        '
        'DtProcDate
        '
        Me.DtProcDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DtProcDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.DtProcDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DtProcDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DtProcDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DtProcDate.CustomFormat = "MM/dd/yyyy"
        Me.DtProcDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtProcDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtProcDate.Location = New System.Drawing.Point(131, 12)
        Me.DtProcDate.Name = "DtProcDate"
        Me.DtProcDate.Size = New System.Drawing.Size(121, 22)
        Me.DtProcDate.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(11, 16)
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
        Me.tsCatheterization.Size = New System.Drawing.Size(1156, 53)
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
        Me.tblbtn_Save.ToolTipText = "Save and Close"
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
        Me.pnlMain.Size = New System.Drawing.Size(1156, 551)
        Me.pnlMain.TabIndex = 0
        '
        'pnlNarrativeSummary
        '
        Me.pnlNarrativeSummary.Controls.Add(Me.Label9)
        Me.pnlNarrativeSummary.Controls.Add(Me.txtNarrativeSummary)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label49)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label50)
        Me.pnlNarrativeSummary.Controls.Add(Me.Label51)
        Me.pnlNarrativeSummary.Controls.Add(Me.lblNarrativeSummary)
        Me.pnlNarrativeSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNarrativeSummary.Location = New System.Drawing.Point(0, 367)
        Me.pnlNarrativeSummary.Name = "pnlNarrativeSummary"
        Me.pnlNarrativeSummary.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlNarrativeSummary.Size = New System.Drawing.Size(1156, 184)
        Me.pnlNarrativeSummary.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 180)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1148, 1)
        Me.Label9.TabIndex = 300
        '
        'txtNarrativeSummary
        '
        Me.txtNarrativeSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNarrativeSummary.Location = New System.Drawing.Point(131, 18)
        Me.txtNarrativeSummary.MaxLength = 5000
        Me.txtNarrativeSummary.Multiline = True
        Me.txtNarrativeSummary.Name = "txtNarrativeSummary"
        Me.txtNarrativeSummary.Size = New System.Drawing.Size(986, 141)
        Me.txtNarrativeSummary.TabIndex = 0
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(4, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1148, 1)
        Me.Label49.TabIndex = 295
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(3, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 181)
        Me.Label50.TabIndex = 297
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(1152, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 181)
        Me.Label51.TabIndex = 298
        '
        'lblNarrativeSummary
        '
        Me.lblNarrativeSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNarrativeSummary.AutoSize = True
        Me.lblNarrativeSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNarrativeSummary.Location = New System.Drawing.Point(12, 19)
        Me.lblNarrativeSummary.Name = "lblNarrativeSummary"
        Me.lblNarrativeSummary.Size = New System.Drawing.Size(117, 14)
        Me.lblNarrativeSummary.TabIndex = 289
        Me.lblNarrativeSummary.Text = "Narrative Summary :"
        Me.lblNarrativeSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLV
        '
        Me.pnlLV.Controls.Add(Me.Label17)
        Me.pnlLV.Controls.Add(Me.Label16)
        Me.pnlLV.Controls.Add(Me.Label15)
        Me.pnlLV.Controls.Add(Me.Label63)
        Me.pnlLV.Controls.Add(Me.txtLV_SystolicVolume)
        Me.pnlLV.Controls.Add(Me.Label52)
        Me.pnlLV.Controls.Add(Me.lblLV_SystolicVolume)
        Me.pnlLV.Controls.Add(Me.Label53)
        Me.pnlLV.Controls.Add(Me.txtLV_DiastolicVolume)
        Me.pnlLV.Controls.Add(Me.lblLV_DiastolicVolume)
        Me.pnlLV.Controls.Add(Me.Label54)
        Me.pnlLV.Controls.Add(Me.txtLV_EjectionFraction)
        Me.pnlLV.Controls.Add(Me.Label55)
        Me.pnlLV.Controls.Add(Me.lblLV_EjectionFraction)
        Me.pnlLV.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLV.Location = New System.Drawing.Point(0, 307)
        Me.pnlLV.Name = "pnlLV"
        Me.pnlLV.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlLV.Size = New System.Drawing.Size(1156, 60)
        Me.pnlLV.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(206, 30)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(19, 14)
        Me.Label17.TabIndex = 305
        Me.Label17.Text = "%"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(647, 30)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 14)
        Me.Label16.TabIndex = 304
        Me.Label16.Text = "mL"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(416, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(23, 14)
        Me.Label15.TabIndex = 303
        Me.Label15.Text = "mL"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(4, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.Label63.Size = New System.Drawing.Size(1148, 24)
        Me.Label63.TabIndex = 301
        Me.Label63.Text = "Left Ventricular"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLV_SystolicVolume
        '
        Me.txtLV_SystolicVolume.ForeColor = System.Drawing.Color.Black
        Me.txtLV_SystolicVolume.Location = New System.Drawing.Point(571, 26)
        Me.txtLV_SystolicVolume.MaxLength = 50
        Me.txtLV_SystolicVolume.Name = "txtLV_SystolicVolume"
        Me.txtLV_SystolicVolume.Size = New System.Drawing.Size(74, 22)
        Me.txtLV_SystolicVolume.TabIndex = 2
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(4, 56)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1148, 1)
        Me.Label52.TabIndex = 296
        '
        'lblLV_SystolicVolume
        '
        Me.lblLV_SystolicVolume.AutoSize = True
        Me.lblLV_SystolicVolume.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLV_SystolicVolume.Location = New System.Drawing.Point(470, 30)
        Me.lblLV_SystolicVolume.Name = "lblLV_SystolicVolume"
        Me.lblLV_SystolicVolume.Size = New System.Drawing.Size(100, 14)
        Me.lblLV_SystolicVolume.TabIndex = 263
        Me.lblLV_SystolicVolume.Text = "Systolic Volume :"
        Me.lblLV_SystolicVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(4, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1148, 1)
        Me.Label53.TabIndex = 295
        '
        'txtLV_DiastolicVolume
        '
        Me.txtLV_DiastolicVolume.ForeColor = System.Drawing.Color.Black
        Me.txtLV_DiastolicVolume.Location = New System.Drawing.Point(336, 26)
        Me.txtLV_DiastolicVolume.MaxLength = 50
        Me.txtLV_DiastolicVolume.Name = "txtLV_DiastolicVolume"
        Me.txtLV_DiastolicVolume.Size = New System.Drawing.Size(74, 22)
        Me.txtLV_DiastolicVolume.TabIndex = 1
        '
        'lblLV_DiastolicVolume
        '
        Me.lblLV_DiastolicVolume.AutoSize = True
        Me.lblLV_DiastolicVolume.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLV_DiastolicVolume.Location = New System.Drawing.Point(231, 30)
        Me.lblLV_DiastolicVolume.Name = "lblLV_DiastolicVolume"
        Me.lblLV_DiastolicVolume.Size = New System.Drawing.Size(103, 14)
        Me.lblLV_DiastolicVolume.TabIndex = 261
        Me.lblLV_DiastolicVolume.Text = "Diastolic Volume :"
        Me.lblLV_DiastolicVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(3, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 57)
        Me.Label54.TabIndex = 297
        '
        'txtLV_EjectionFraction
        '
        Me.txtLV_EjectionFraction.ForeColor = System.Drawing.Color.Black
        Me.txtLV_EjectionFraction.Location = New System.Drawing.Point(131, 26)
        Me.txtLV_EjectionFraction.MaxLength = 50
        Me.txtLV_EjectionFraction.Name = "txtLV_EjectionFraction"
        Me.txtLV_EjectionFraction.Size = New System.Drawing.Size(74, 22)
        Me.txtLV_EjectionFraction.TabIndex = 0
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(1152, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 57)
        Me.Label55.TabIndex = 298
        '
        'lblLV_EjectionFraction
        '
        Me.lblLV_EjectionFraction.AutoSize = True
        Me.lblLV_EjectionFraction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLV_EjectionFraction.Location = New System.Drawing.Point(21, 30)
        Me.lblLV_EjectionFraction.Name = "lblLV_EjectionFraction"
        Me.lblLV_EjectionFraction.Size = New System.Drawing.Size(106, 14)
        Me.lblLV_EjectionFraction.TabIndex = 257
        Me.lblLV_EjectionFraction.Text = "Ejection Fraction :"
        Me.lblLV_EjectionFraction.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSaturation
        '
        Me.pnlSaturation.Controls.Add(Me.Label14)
        Me.pnlSaturation.Controls.Add(Me.Label13)
        Me.pnlSaturation.Controls.Add(Me.Label12)
        Me.pnlSaturation.Controls.Add(Me.Label11)
        Me.pnlSaturation.Controls.Add(Me.Label10)
        Me.pnlSaturation.Controls.Add(Me.Label61)
        Me.pnlSaturation.Controls.Add(Me.Label43)
        Me.pnlSaturation.Controls.Add(Me.Label45)
        Me.pnlSaturation.Controls.Add(Me.Label46)
        Me.pnlSaturation.Controls.Add(Me.Label47)
        Me.pnlSaturation.Controls.Add(Me.lblS_IVC)
        Me.pnlSaturation.Controls.Add(Me.txtS_IVC)
        Me.pnlSaturation.Controls.Add(Me.lblS_SVC)
        Me.pnlSaturation.Controls.Add(Me.txtS_SVC)
        Me.pnlSaturation.Controls.Add(Me.txtS_PA)
        Me.pnlSaturation.Controls.Add(Me.lblS_RA)
        Me.pnlSaturation.Controls.Add(Me.lblS_PA)
        Me.pnlSaturation.Controls.Add(Me.txtS_RA)
        Me.pnlSaturation.Controls.Add(Me.lblS_RV)
        Me.pnlSaturation.Controls.Add(Me.txtS_RV)
        Me.pnlSaturation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSaturation.Location = New System.Drawing.Point(0, 247)
        Me.pnlSaturation.Name = "pnlSaturation"
        Me.pnlSaturation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSaturation.Size = New System.Drawing.Size(1156, 60)
        Me.pnlSaturation.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(1029, 29)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(19, 14)
        Me.Label14.TabIndex = 304
        Me.Label14.Text = "%"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(885, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(19, 14)
        Me.Label13.TabIndex = 303
        Me.Label13.Text = "%"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(647, 29)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(19, 14)
        Me.Label12.TabIndex = 302
        Me.Label12.Text = "%"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(416, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(19, 14)
        Me.Label11.TabIndex = 301
        Me.Label11.Text = "%"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(211, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(19, 14)
        Me.Label10.TabIndex = 300
        Me.Label10.Text = "%"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(4, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.Label61.Size = New System.Drawing.Size(1148, 24)
        Me.Label61.TabIndex = 299
        Me.Label61.Text = "Saturation "
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(4, 56)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1148, 1)
        Me.Label43.TabIndex = 296
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(4, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1148, 1)
        Me.Label45.TabIndex = 295
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(3, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 57)
        Me.Label46.TabIndex = 297
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(1152, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 57)
        Me.Label47.TabIndex = 298
        '
        'lblS_IVC
        '
        Me.lblS_IVC.AutoSize = True
        Me.lblS_IVC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS_IVC.Location = New System.Drawing.Point(93, 29)
        Me.lblS_IVC.Name = "lblS_IVC"
        Me.lblS_IVC.Size = New System.Drawing.Size(34, 14)
        Me.lblS_IVC.TabIndex = 257
        Me.lblS_IVC.Text = "IVC :"
        Me.lblS_IVC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtS_IVC
        '
        Me.txtS_IVC.ForeColor = System.Drawing.Color.Black
        Me.txtS_IVC.Location = New System.Drawing.Point(131, 25)
        Me.txtS_IVC.MaxLength = 50
        Me.txtS_IVC.Name = "txtS_IVC"
        Me.txtS_IVC.Size = New System.Drawing.Size(74, 22)
        Me.txtS_IVC.TabIndex = 0
        '
        'lblS_SVC
        '
        Me.lblS_SVC.AutoSize = True
        Me.lblS_SVC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS_SVC.Location = New System.Drawing.Point(297, 29)
        Me.lblS_SVC.Name = "lblS_SVC"
        Me.lblS_SVC.Size = New System.Drawing.Size(37, 14)
        Me.lblS_SVC.TabIndex = 259
        Me.lblS_SVC.Text = "SVC :"
        Me.lblS_SVC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtS_SVC
        '
        Me.txtS_SVC.ForeColor = System.Drawing.Color.Black
        Me.txtS_SVC.Location = New System.Drawing.Point(336, 25)
        Me.txtS_SVC.MaxLength = 50
        Me.txtS_SVC.Name = "txtS_SVC"
        Me.txtS_SVC.Size = New System.Drawing.Size(74, 22)
        Me.txtS_SVC.TabIndex = 1
        '
        'txtS_PA
        '
        Me.txtS_PA.ForeColor = System.Drawing.Color.Black
        Me.txtS_PA.Location = New System.Drawing.Point(955, 25)
        Me.txtS_PA.MaxLength = 50
        Me.txtS_PA.Name = "txtS_PA"
        Me.txtS_PA.Size = New System.Drawing.Size(74, 22)
        Me.txtS_PA.TabIndex = 4
        '
        'lblS_RA
        '
        Me.lblS_RA.AutoSize = True
        Me.lblS_RA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS_RA.Location = New System.Drawing.Point(540, 29)
        Me.lblS_RA.Name = "lblS_RA"
        Me.lblS_RA.Size = New System.Drawing.Size(30, 14)
        Me.lblS_RA.TabIndex = 261
        Me.lblS_RA.Text = "RA :"
        Me.lblS_RA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS_PA
        '
        Me.lblS_PA.AutoSize = True
        Me.lblS_PA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS_PA.Location = New System.Drawing.Point(923, 29)
        Me.lblS_PA.Name = "lblS_PA"
        Me.lblS_PA.Size = New System.Drawing.Size(30, 14)
        Me.lblS_PA.TabIndex = 269
        Me.lblS_PA.Text = "PA :"
        Me.lblS_PA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtS_RA
        '
        Me.txtS_RA.ForeColor = System.Drawing.Color.Black
        Me.txtS_RA.Location = New System.Drawing.Point(571, 25)
        Me.txtS_RA.MaxLength = 50
        Me.txtS_RA.Name = "txtS_RA"
        Me.txtS_RA.Size = New System.Drawing.Size(74, 22)
        Me.txtS_RA.TabIndex = 2
        '
        'lblS_RV
        '
        Me.lblS_RV.AutoSize = True
        Me.lblS_RV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS_RV.Location = New System.Drawing.Point(775, 29)
        Me.lblS_RV.Name = "lblS_RV"
        Me.lblS_RV.Size = New System.Drawing.Size(30, 14)
        Me.lblS_RV.TabIndex = 263
        Me.lblS_RV.Text = "RV :"
        Me.lblS_RV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtS_RV
        '
        Me.txtS_RV.ForeColor = System.Drawing.Color.Black
        Me.txtS_RV.Location = New System.Drawing.Point(809, 25)
        Me.txtS_RV.MaxLength = 50
        Me.txtS_RV.Name = "txtS_RV"
        Me.txtS_RV.Size = New System.Drawing.Size(74, 22)
        Me.txtS_RV.TabIndex = 3
        '
        'pnlPressure
        '
        Me.pnlPressure.Controls.Add(Me.Label60)
        Me.pnlPressure.Controls.Add(Me.txtP_Mean)
        Me.pnlPressure.Controls.Add(Me.Label38)
        Me.pnlPressure.Controls.Add(Me.lblP_Mean)
        Me.pnlPressure.Controls.Add(Me.Label39)
        Me.pnlPressure.Controls.Add(Me.txtP_Diastolic)
        Me.pnlPressure.Controls.Add(Me.Label40)
        Me.pnlPressure.Controls.Add(Me.lblP_Diastolic)
        Me.pnlPressure.Controls.Add(Me.txtP_Peak)
        Me.pnlPressure.Controls.Add(Me.Label41)
        Me.pnlPressure.Controls.Add(Me.lblP_Peak)
        Me.pnlPressure.Controls.Add(Me.txtP_PA)
        Me.pnlPressure.Controls.Add(Me.lblP_RA)
        Me.pnlPressure.Controls.Add(Me.lblP_PA)
        Me.pnlPressure.Controls.Add(Me.txtP_RA)
        Me.pnlPressure.Controls.Add(Me.lblP_LA)
        Me.pnlPressure.Controls.Add(Me.txtP_LA)
        Me.pnlPressure.Controls.Add(Me.txtP_LV)
        Me.pnlPressure.Controls.Add(Me.lblP_RightPulmonary)
        Me.pnlPressure.Controls.Add(Me.lblP_LVPeak)
        Me.pnlPressure.Controls.Add(Me.txtP_RightPulmonary)
        Me.pnlPressure.Controls.Add(Me.txtP_RV)
        Me.pnlPressure.Controls.Add(Me.lblP_LeftPulmonary)
        Me.pnlPressure.Controls.Add(Me.lblP_RVPeak)
        Me.pnlPressure.Controls.Add(Me.txtP_LeftPulmonary)
        Me.pnlPressure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPressure.Location = New System.Drawing.Point(0, 148)
        Me.pnlPressure.Name = "pnlPressure"
        Me.pnlPressure.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPressure.Size = New System.Drawing.Size(1156, 99)
        Me.pnlPressure.TabIndex = 2
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.Transparent
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(4, 1)
        Me.Label60.Name = "Label60"
        Me.Label60.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.Label60.Size = New System.Drawing.Size(1148, 24)
        Me.Label60.TabIndex = 257
        Me.Label60.Text = "Pressure "
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtP_Mean
        '
        Me.txtP_Mean.ForeColor = System.Drawing.Color.Black
        Me.txtP_Mean.Location = New System.Drawing.Point(809, 60)
        Me.txtP_Mean.MaxLength = 50
        Me.txtP_Mean.Name = "txtP_Mean"
        Me.txtP_Mean.Size = New System.Drawing.Size(74, 22)
        Me.txtP_Mean.TabIndex = 9
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(4, 95)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1148, 1)
        Me.Label38.TabIndex = 296
        '
        'lblP_Mean
        '
        Me.lblP_Mean.AutoSize = True
        Me.lblP_Mean.Location = New System.Drawing.Point(761, 63)
        Me.lblP_Mean.Name = "lblP_Mean"
        Me.lblP_Mean.Size = New System.Drawing.Size(44, 14)
        Me.lblP_Mean.TabIndex = 283
        Me.lblP_Mean.Text = "Mean :"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1148, 1)
        Me.Label39.TabIndex = 295
        '
        'txtP_Diastolic
        '
        Me.txtP_Diastolic.ForeColor = System.Drawing.Color.Black
        Me.txtP_Diastolic.Location = New System.Drawing.Point(571, 60)
        Me.txtP_Diastolic.MaxLength = 50
        Me.txtP_Diastolic.Name = "txtP_Diastolic"
        Me.txtP_Diastolic.Size = New System.Drawing.Size(74, 22)
        Me.txtP_Diastolic.TabIndex = 8
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 96)
        Me.Label40.TabIndex = 297
        '
        'lblP_Diastolic
        '
        Me.lblP_Diastolic.AutoSize = True
        Me.lblP_Diastolic.Location = New System.Drawing.Point(512, 64)
        Me.lblP_Diastolic.Name = "lblP_Diastolic"
        Me.lblP_Diastolic.Size = New System.Drawing.Size(58, 14)
        Me.lblP_Diastolic.TabIndex = 281
        Me.lblP_Diastolic.Text = "Diastolic :"
        '
        'txtP_Peak
        '
        Me.txtP_Peak.ForeColor = System.Drawing.Color.Black
        Me.txtP_Peak.Location = New System.Drawing.Point(336, 59)
        Me.txtP_Peak.MaxLength = 50
        Me.txtP_Peak.Name = "txtP_Peak"
        Me.txtP_Peak.Size = New System.Drawing.Size(74, 22)
        Me.txtP_Peak.TabIndex = 7
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(1152, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 96)
        Me.Label41.TabIndex = 298
        '
        'lblP_Peak
        '
        Me.lblP_Peak.AutoSize = True
        Me.lblP_Peak.Location = New System.Drawing.Point(293, 63)
        Me.lblP_Peak.Name = "lblP_Peak"
        Me.lblP_Peak.Size = New System.Drawing.Size(41, 14)
        Me.lblP_Peak.TabIndex = 279
        Me.lblP_Peak.Text = "Peak :"
        '
        'txtP_PA
        '
        Me.txtP_PA.ForeColor = System.Drawing.Color.Black
        Me.txtP_PA.Location = New System.Drawing.Point(131, 59)
        Me.txtP_PA.MaxLength = 50
        Me.txtP_PA.Name = "txtP_PA"
        Me.txtP_PA.Size = New System.Drawing.Size(74, 22)
        Me.txtP_PA.TabIndex = 6
        '
        'lblP_RA
        '
        Me.lblP_RA.AutoSize = True
        Me.lblP_RA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_RA.Location = New System.Drawing.Point(97, 35)
        Me.lblP_RA.Name = "lblP_RA"
        Me.lblP_RA.Size = New System.Drawing.Size(30, 14)
        Me.lblP_RA.TabIndex = 257
        Me.lblP_RA.Text = "RA :"
        Me.lblP_RA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblP_PA
        '
        Me.lblP_PA.AutoSize = True
        Me.lblP_PA.Location = New System.Drawing.Point(97, 63)
        Me.lblP_PA.Name = "lblP_PA"
        Me.lblP_PA.Size = New System.Drawing.Size(30, 14)
        Me.lblP_PA.TabIndex = 277
        Me.lblP_PA.Text = "PA :"
        '
        'txtP_RA
        '
        Me.txtP_RA.ForeColor = System.Drawing.Color.Black
        Me.txtP_RA.Location = New System.Drawing.Point(131, 31)
        Me.txtP_RA.MaxLength = 50
        Me.txtP_RA.Name = "txtP_RA"
        Me.txtP_RA.Size = New System.Drawing.Size(74, 22)
        Me.txtP_RA.TabIndex = 0
        '
        'lblP_LA
        '
        Me.lblP_LA.AutoSize = True
        Me.lblP_LA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_LA.Location = New System.Drawing.Point(305, 35)
        Me.lblP_LA.Name = "lblP_LA"
        Me.lblP_LA.Size = New System.Drawing.Size(29, 14)
        Me.lblP_LA.TabIndex = 259
        Me.lblP_LA.Text = "LA :"
        Me.lblP_LA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtP_LA
        '
        Me.txtP_LA.ForeColor = System.Drawing.Color.Black
        Me.txtP_LA.Location = New System.Drawing.Point(336, 31)
        Me.txtP_LA.MaxLength = 50
        Me.txtP_LA.Name = "txtP_LA"
        Me.txtP_LA.Size = New System.Drawing.Size(74, 22)
        Me.txtP_LA.TabIndex = 1
        '
        'txtP_LV
        '
        Me.txtP_LV.ForeColor = System.Drawing.Color.Black
        Me.txtP_LV.Location = New System.Drawing.Point(1072, 31)
        Me.txtP_LV.MaxLength = 50
        Me.txtP_LV.Name = "txtP_LV"
        Me.txtP_LV.Size = New System.Drawing.Size(74, 22)
        Me.txtP_LV.TabIndex = 5
        '
        'lblP_RightPulmonary
        '
        Me.lblP_RightPulmonary.AutoSize = True
        Me.lblP_RightPulmonary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_RightPulmonary.Location = New System.Drawing.Point(423, 35)
        Me.lblP_RightPulmonary.Name = "lblP_RightPulmonary"
        Me.lblP_RightPulmonary.Size = New System.Drawing.Size(147, 14)
        Me.lblP_RightPulmonary.TabIndex = 263
        Me.lblP_RightPulmonary.Text = "Right Pulmonary Wedge :"
        Me.lblP_RightPulmonary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblP_LVPeak
        '
        Me.lblP_LVPeak.AutoSize = True
        Me.lblP_LVPeak.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_LVPeak.Location = New System.Drawing.Point(1042, 35)
        Me.lblP_LVPeak.Name = "lblP_LVPeak"
        Me.lblP_LVPeak.Size = New System.Drawing.Size(29, 14)
        Me.lblP_LVPeak.TabIndex = 271
        Me.lblP_LVPeak.Text = "LV :"
        Me.lblP_LVPeak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtP_RightPulmonary
        '
        Me.txtP_RightPulmonary.ForeColor = System.Drawing.Color.Black
        Me.txtP_RightPulmonary.Location = New System.Drawing.Point(571, 31)
        Me.txtP_RightPulmonary.MaxLength = 50
        Me.txtP_RightPulmonary.Name = "txtP_RightPulmonary"
        Me.txtP_RightPulmonary.Size = New System.Drawing.Size(74, 22)
        Me.txtP_RightPulmonary.TabIndex = 2
        '
        'txtP_RV
        '
        Me.txtP_RV.ForeColor = System.Drawing.Color.Black
        Me.txtP_RV.Location = New System.Drawing.Point(955, 31)
        Me.txtP_RV.MaxLength = 50
        Me.txtP_RV.Name = "txtP_RV"
        Me.txtP_RV.Size = New System.Drawing.Size(74, 22)
        Me.txtP_RV.TabIndex = 4
        '
        'lblP_LeftPulmonary
        '
        Me.lblP_LeftPulmonary.AutoSize = True
        Me.lblP_LeftPulmonary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_LeftPulmonary.Location = New System.Drawing.Point(664, 35)
        Me.lblP_LeftPulmonary.Name = "lblP_LeftPulmonary"
        Me.lblP_LeftPulmonary.Size = New System.Drawing.Size(141, 14)
        Me.lblP_LeftPulmonary.TabIndex = 265
        Me.lblP_LeftPulmonary.Text = "Left Pulmonary Wedge :"
        Me.lblP_LeftPulmonary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblP_RVPeak
        '
        Me.lblP_RVPeak.AutoSize = True
        Me.lblP_RVPeak.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_RVPeak.Location = New System.Drawing.Point(923, 35)
        Me.lblP_RVPeak.Name = "lblP_RVPeak"
        Me.lblP_RVPeak.Size = New System.Drawing.Size(30, 14)
        Me.lblP_RVPeak.TabIndex = 269
        Me.lblP_RVPeak.Text = "RV :"
        Me.lblP_RVPeak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtP_LeftPulmonary
        '
        Me.txtP_LeftPulmonary.ForeColor = System.Drawing.Color.Black
        Me.txtP_LeftPulmonary.Location = New System.Drawing.Point(809, 31)
        Me.txtP_LeftPulmonary.MaxLength = 50
        Me.txtP_LeftPulmonary.Name = "txtP_LeftPulmonary"
        Me.txtP_LeftPulmonary.Size = New System.Drawing.Size(74, 22)
        Me.txtP_LeftPulmonary.TabIndex = 3
        '
        'pnlSelectFromList
        '
        Me.pnlSelectFromList.Controls.Add(Me.Label4)
        Me.pnlSelectFromList.Controls.Add(Me.Label1)
        Me.pnlSelectFromList.Controls.Add(Me.lblCPTCode)
        Me.pnlSelectFromList.Controls.Add(Me.Label5)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllCPT)
        Me.pnlSelectFromList.Controls.Add(Me.Label6)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearCPT)
        Me.pnlSelectFromList.Controls.Add(Me.LblIntervention)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseCPT)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllIntervn)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearIntervn)
        Me.pnlSelectFromList.Controls.Add(Me.lstCPTcode)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseIntervn)
        Me.pnlSelectFromList.Controls.Add(Me.lstPhysicians)
        Me.pnlSelectFromList.Controls.Add(Me.lstIntervention)
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
        Me.pnlSelectFromList.Location = New System.Drawing.Point(0, 47)
        Me.pnlSelectFromList.Name = "pnlSelectFromList"
        Me.pnlSelectFromList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSelectFromList.Size = New System.Drawing.Size(1156, 101)
        Me.pnlSelectFromList.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1148, 1)
        Me.Label4.TabIndex = 296
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1148, 1)
        Me.Label1.TabIndex = 295
        '
        'lblCPTCode
        '
        Me.lblCPTCode.AutoSize = True
        Me.lblCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPTCode.Location = New System.Drawing.Point(10, 15)
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
        Me.Label5.Size = New System.Drawing.Size(1, 98)
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
        Me.BtnClearAllCPT.Location = New System.Drawing.Point(205, 64)
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
        Me.Label6.Location = New System.Drawing.Point(1152, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 98)
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
        Me.btnClearCPT.Location = New System.Drawing.Point(205, 38)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnClearCPT.TabIndex = 2
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearCPT, "Clear CPT")
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'LblIntervention
        '
        Me.LblIntervention.AutoSize = True
        Me.LblIntervention.BackColor = System.Drawing.Color.Transparent
        Me.LblIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIntervention.Location = New System.Drawing.Point(556, 15)
        Me.LblIntervention.Name = "LblIntervention"
        Me.LblIntervention.Size = New System.Drawing.Size(145, 14)
        Me.LblIntervention.TabIndex = 228
        Me.LblIntervention.Text = "Type of Intervention(s) :"
        Me.LblIntervention.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowseCPT
        '
        Me.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCPT.BackgroundImage = CType(resources.GetObject("btnBrowseCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCPT.Image = CType(resources.GetObject("btnBrowseCPT.Image"), System.Drawing.Image)
        Me.btnBrowseCPT.Location = New System.Drawing.Point(205, 12)
        Me.btnBrowseCPT.Name = "btnBrowseCPT"
        Me.btnBrowseCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseCPT.TabIndex = 1
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseCPT, "Browse CPT")
        Me.btnBrowseCPT.UseVisualStyleBackColor = False
        '
        'BtnClearAllIntervn
        '
        Me.BtnClearAllIntervn.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllIntervn.BackgroundImage = CType(resources.GetObject("BtnClearAllIntervn.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllIntervn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllIntervn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllIntervn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllIntervn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllIntervn.Image = CType(resources.GetObject("BtnClearAllIntervn.Image"), System.Drawing.Image)
        Me.BtnClearAllIntervn.Location = New System.Drawing.Point(860, 64)
        Me.BtnClearAllIntervn.Name = "BtnClearAllIntervn"
        Me.BtnClearAllIntervn.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllIntervn.TabIndex = 11
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllIntervn, "Clear All Type of Interventions ")
        Me.BtnClearAllIntervn.UseVisualStyleBackColor = False
        '
        'btnClearIntervn
        '
        Me.btnClearIntervn.BackColor = System.Drawing.Color.Transparent
        Me.btnClearIntervn.BackgroundImage = CType(resources.GetObject("btnClearIntervn.BackgroundImage"), System.Drawing.Image)
        Me.btnClearIntervn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearIntervn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearIntervn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearIntervn.Image = CType(resources.GetObject("btnClearIntervn.Image"), System.Drawing.Image)
        Me.btnClearIntervn.Location = New System.Drawing.Point(860, 38)
        Me.btnClearIntervn.Name = "btnClearIntervn"
        Me.btnClearIntervn.Size = New System.Drawing.Size(22, 22)
        Me.btnClearIntervn.TabIndex = 10
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearIntervn, "Clear Type of Intervention")
        Me.btnClearIntervn.UseVisualStyleBackColor = False
        '
        'lstCPTcode
        '
        Me.lstCPTcode.FormattingEnabled = True
        Me.lstCPTcode.ItemHeight = 14
        Me.lstCPTcode.Location = New System.Drawing.Point(50, 12)
        Me.lstCPTcode.Name = "lstCPTcode"
        Me.lstCPTcode.Size = New System.Drawing.Size(152, 74)
        Me.lstCPTcode.TabIndex = 0
        '
        'btnBrowseIntervn
        '
        Me.btnBrowseIntervn.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseIntervn.BackgroundImage = CType(resources.GetObject("btnBrowseIntervn.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseIntervn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseIntervn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseIntervn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseIntervn.Image = CType(resources.GetObject("btnBrowseIntervn.Image"), System.Drawing.Image)
        Me.btnBrowseIntervn.Location = New System.Drawing.Point(860, 13)
        Me.btnBrowseIntervn.Name = "btnBrowseIntervn"
        Me.btnBrowseIntervn.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseIntervn.TabIndex = 9
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseIntervn, "Browse Type of Intervention")
        Me.btnBrowseIntervn.UseVisualStyleBackColor = False
        '
        'lstPhysicians
        '
        Me.lstPhysicians.FormattingEnabled = True
        Me.lstPhysicians.ItemHeight = 14
        Me.lstPhysicians.Location = New System.Drawing.Point(965, 12)
        Me.lstPhysicians.Name = "lstPhysicians"
        Me.lstPhysicians.Size = New System.Drawing.Size(152, 74)
        Me.lstPhysicians.TabIndex = 12
        '
        'lstIntervention
        '
        Me.lstIntervention.FormattingEnabled = True
        Me.lstIntervention.ItemHeight = 14
        Me.lstIntervention.Location = New System.Drawing.Point(704, 12)
        Me.lstIntervention.Name = "lstIntervention"
        Me.lstIntervention.Size = New System.Drawing.Size(152, 74)
        Me.lstIntervention.TabIndex = 8
        '
        'btnBrowsePhyID
        '
        Me.btnBrowsePhyID.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePhyID.BackgroundImage = CType(resources.GetObject("btnBrowsePhyID.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsePhyID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePhyID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePhyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePhyID.Image = CType(resources.GetObject("btnBrowsePhyID.Image"), System.Drawing.Image)
        Me.btnBrowsePhyID.Location = New System.Drawing.Point(1121, 12)
        Me.btnBrowsePhyID.Name = "btnBrowsePhyID"
        Me.btnBrowsePhyID.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowsePhyID.TabIndex = 13
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
        Me.btnClearPhyID.Location = New System.Drawing.Point(1121, 38)
        Me.btnClearPhyID.Name = "btnClearPhyID"
        Me.btnClearPhyID.Size = New System.Drawing.Size(22, 22)
        Me.btnClearPhyID.TabIndex = 14
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
        Me.BtnClearAllPhyID.Location = New System.Drawing.Point(1121, 64)
        Me.BtnClearAllPhyID.Name = "BtnClearAllPhyID"
        Me.BtnClearAllPhyID.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllPhyID.TabIndex = 15
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllPhyID, "Clear All Physicians")
        Me.BtnClearAllPhyID.UseVisualStyleBackColor = False
        '
        'lblPhyID
        '
        Me.lblPhyID.AutoSize = True
        Me.lblPhyID.BackColor = System.Drawing.Color.Transparent
        Me.lblPhyID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhyID.Location = New System.Drawing.Point(885, 16)
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
        Me.lstTestType.Location = New System.Drawing.Point(370, 12)
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
        Me.btnBrowseTesttype.Location = New System.Drawing.Point(525, 12)
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
        Me.lblTesttype.Location = New System.Drawing.Point(231, 15)
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
        Me.btnClearTesttype.Location = New System.Drawing.Point(525, 38)
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
        Me.BtnClearAllTesttype.Location = New System.Drawing.Point(525, 64)
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
        Me.pnlDateProcedure.Controls.Add(Me.DtProcDate)
        Me.pnlDateProcedure.Controls.Add(Me.lblDate)
        Me.pnlDateProcedure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDateProcedure.Location = New System.Drawing.Point(0, 0)
        Me.pnlDateProcedure.Name = "pnlDateProcedure"
        Me.pnlDateProcedure.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDateProcedure.Size = New System.Drawing.Size(1156, 47)
        Me.pnlDateProcedure.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(4, 43)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1148, 1)
        Me.Label67.TabIndex = 301
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(4, 3)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(1148, 1)
        Me.Label66.TabIndex = 300
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(1152, 3)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 41)
        Me.Label65.TabIndex = 299
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(3, 3)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 41)
        Me.Label64.TabIndex = 298
        '
        'pnlCustomTask
        '
        Me.pnlCustomTask.Controls.Add(Me.Label8)
        Me.pnlCustomTask.Controls.Add(Me.Label7)
        Me.pnlCustomTask.Controls.Add(Me.Label3)
        Me.pnlCustomTask.Controls.Add(Me.Label2)
        Me.pnlCustomTask.Location = New System.Drawing.Point(675, 161)
        Me.pnlCustomTask.Name = "pnlCustomTask"
        Me.pnlCustomTask.Size = New System.Drawing.Size(383, 92)
        Me.pnlCustomTask.TabIndex = 299
        Me.pnlCustomTask.TabStop = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(381, 1)
        Me.Label8.TabIndex = 301
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(381, 1)
        Me.Label7.TabIndex = 300
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(382, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 92)
        Me.Label3.TabIndex = 299
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 92)
        Me.Label2.TabIndex = 298
        '
        'C1CPTTest
        '
        Me.C1CPTTest.ColumnInfo = "2,0,0,0,0,95,Columns:0{AllowEditing:False;Style:""DataType:System.String;TextAlign" & _
            ":LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{AllowSorting:False;AllowEditing:False;Style:""DataType:System.S" & _
            "tring;TextAlign:LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1CPTTest.ExtendLastCol = True
        Me.C1CPTTest.Location = New System.Drawing.Point(311, 195)
        Me.C1CPTTest.Name = "C1CPTTest"
        Me.C1CPTTest.Rows.Count = 1
        Me.C1CPTTest.Rows.DefaultSize = 19
        Me.C1CPTTest.Rows.Fixed = 0
        Me.C1CPTTest.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None
        Me.C1CPTTest.Size = New System.Drawing.Size(366, 165)
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(1156, 56)
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
        'frmCV_Catheterization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1156, 607)
        Me.Controls.Add(Me.pnlCustomTask)
        Me.Controls.Add(Me.C1CPTTest)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlPatientStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_Catheterization"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cardiovascular Catheterization"
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsCatheterization As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents DtProcDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblTesttype As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllTesttype As System.Windows.Forms.Button
    Friend WithEvents btnClearTesttype As System.Windows.Forms.Button
    Friend WithEvents btnBrowseTesttype As System.Windows.Forms.Button
    Friend WithEvents lstTestType As System.Windows.Forms.ListBox
    Friend WithEvents LblIntervention As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllIntervn As System.Windows.Forms.Button
    Friend WithEvents btnClearIntervn As System.Windows.Forms.Button
    Friend WithEvents btnBrowseIntervn As System.Windows.Forms.Button
    Friend WithEvents lstIntervention As System.Windows.Forms.ListBox
    Friend WithEvents lblPhyID As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllPhyID As System.Windows.Forms.Button
    Friend WithEvents btnClearPhyID As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePhyID As System.Windows.Forms.Button
    Friend WithEvents lstPhysicians As System.Windows.Forms.ListBox
    Friend WithEvents txtP_LA As System.Windows.Forms.TextBox
    Friend WithEvents lblP_LA As System.Windows.Forms.Label
    Friend WithEvents txtP_RA As System.Windows.Forms.TextBox
    Friend WithEvents lblP_RA As System.Windows.Forms.Label
    Friend WithEvents txtP_RightPulmonary As System.Windows.Forms.TextBox
    Friend WithEvents lblP_RightPulmonary As System.Windows.Forms.Label
    Friend WithEvents txtP_LeftPulmonary As System.Windows.Forms.TextBox
    Friend WithEvents lblP_LeftPulmonary As System.Windows.Forms.Label
    Friend WithEvents txtP_LV As System.Windows.Forms.TextBox
    Friend WithEvents lblP_LVPeak As System.Windows.Forms.Label
    Friend WithEvents txtP_RV As System.Windows.Forms.TextBox
    Friend WithEvents lblP_RVPeak As System.Windows.Forms.Label
    Friend WithEvents txtP_Mean As System.Windows.Forms.TextBox
    Friend WithEvents lblP_Mean As System.Windows.Forms.Label
    Friend WithEvents txtP_Diastolic As System.Windows.Forms.TextBox
    Friend WithEvents lblP_Diastolic As System.Windows.Forms.Label
    Friend WithEvents txtP_Peak As System.Windows.Forms.TextBox
    Friend WithEvents lblP_Peak As System.Windows.Forms.Label
    Friend WithEvents txtP_PA As System.Windows.Forms.TextBox
    Friend WithEvents lblP_PA As System.Windows.Forms.Label
    Friend WithEvents txtS_PA As System.Windows.Forms.TextBox
    Friend WithEvents lblS_PA As System.Windows.Forms.Label
    Friend WithEvents txtS_RV As System.Windows.Forms.TextBox
    Friend WithEvents lblS_RV As System.Windows.Forms.Label
    Friend WithEvents txtS_RA As System.Windows.Forms.TextBox
    Friend WithEvents lblS_RA As System.Windows.Forms.Label
    Friend WithEvents txtS_SVC As System.Windows.Forms.TextBox
    Friend WithEvents lblS_SVC As System.Windows.Forms.Label
    Friend WithEvents txtS_IVC As System.Windows.Forms.TextBox
    Friend WithEvents lblS_IVC As System.Windows.Forms.Label
    Friend WithEvents txtLV_DiastolicVolume As System.Windows.Forms.TextBox
    Friend WithEvents lblLV_DiastolicVolume As System.Windows.Forms.Label
    Friend WithEvents txtLV_EjectionFraction As System.Windows.Forms.TextBox
    Friend WithEvents lblLV_EjectionFraction As System.Windows.Forms.Label
    Friend WithEvents txtLV_SystolicVolume As System.Windows.Forms.TextBox
    Friend WithEvents lblLV_SystolicVolume As System.Windows.Forms.Label
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
    Friend WithEvents Label60 As System.Windows.Forms.Label
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
    Friend WithEvents txtNarrativeSummary As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
