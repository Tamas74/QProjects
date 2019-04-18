<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_Echocardiogram
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

            components.Dispose()

            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

            End If
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch

            End Try
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_Echocardiogram))
        Me.tsCatheterization = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlDateProcedure = New System.Windows.Forms.Panel
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.dtdop = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_Nativesum = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txt_lvmass = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txt_lvsystvol = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txt_lvdiastvol = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.txt_mvarea = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtav_area = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.txt_laarea = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txt_mitral = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txt_arotic = New System.Windows.Forms.TextBox
        Me.pnlPressure = New System.Windows.Forms.Panel
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblP_RA = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.txt_lvedd = New System.Windows.Forms.TextBox
        Me.lblP_LA = New System.Windows.Forms.Label
        Me.txt_lvesd = New System.Windows.Forms.TextBox
        Me.lblP_RightPulmonary = New System.Windows.Forms.Label
        Me.txt_lvthick = New System.Windows.Forms.TextBox
        Me.lblP_LeftPulmonary = New System.Windows.Forms.Label
        Me.txt_septhik = New System.Windows.Forms.TextBox
        Me.pnlSelectFromList = New System.Windows.Forms.Panel
        Me.lblid = New System.Windows.Forms.Label
        Me.bntidclr = New System.Windows.Forms.Button
        Me.btnidcls = New System.Windows.Forms.Button
        Me.btnidbr = New System.Windows.Forms.Button
        Me.lstIDPhy = New System.Windows.Forms.ListBox
        Me.lblproc = New System.Windows.Forms.Label
        Me.btnpnameclr = New System.Windows.Forms.Button
        Me.btnpnamecls = New System.Windows.Forms.Button
        Me.btnpnamebr = New System.Windows.Forms.Button
        Me.lstProcName = New System.Windows.Forms.ListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCPTCode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btncptclr = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btncptcls = New System.Windows.Forms.Button
        Me.btncptbr = New System.Windows.Forms.Button
        Me.lstCPTcode = New System.Windows.Forms.ListBox
        Me.pnlcustomTask = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.tsCatheterization.SuspendLayout()
        Me.pnlDateProcedure.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlPressure.SuspendLayout()
        Me.pnlSelectFromList.SuspendLayout()
        Me.pnlcustomTask.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.SuspendLayout()
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
        Me.tsCatheterization.Size = New System.Drawing.Size(1001, 53)
        Me.tsCatheterization.TabIndex = 2
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
        'pnlDateProcedure
        '
        Me.pnlDateProcedure.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDateProcedure.Controls.Add(Me.Label67)
        Me.pnlDateProcedure.Controls.Add(Me.Label66)
        Me.pnlDateProcedure.Controls.Add(Me.Label65)
        Me.pnlDateProcedure.Controls.Add(Me.Label64)
        Me.pnlDateProcedure.Controls.Add(Me.dtdop)
        Me.pnlDateProcedure.Controls.Add(Me.lblDate)
        Me.pnlDateProcedure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDateProcedure.Location = New System.Drawing.Point(0, 0)
        Me.pnlDateProcedure.Name = "pnlDateProcedure"
        Me.pnlDateProcedure.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDateProcedure.Size = New System.Drawing.Size(1001, 45)
        Me.pnlDateProcedure.TabIndex = 3
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(4, 41)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(993, 1)
        Me.Label67.TabIndex = 301
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(4, 3)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(993, 1)
        Me.Label66.TabIndex = 300
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(997, 3)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 39)
        Me.Label65.TabIndex = 299
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(3, 3)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 39)
        Me.Label64.TabIndex = 298
        '
        'dtdop
        '
        Me.dtdop.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtdop.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtdop.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtdop.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtdop.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtdop.CustomFormat = "MM/dd/yyyy"
        Me.dtdop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtdop.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtdop.Location = New System.Drawing.Point(137, 11)
        Me.dtdop.Name = "dtdop"
        Me.dtdop.Size = New System.Drawing.Size(117, 22)
        Me.dtdop.TabIndex = 1
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(18, 14)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(116, 14)
        Me.lblDate.TabIndex = 12
        Me.lblDate.Text = "Date of Procedure :"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Controls.Add(Me.pnlPressure)
        Me.pnlMain.Controls.Add(Me.pnlSelectFromList)
        Me.pnlMain.Controls.Add(Me.pnlDateProcedure)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1001, 552)
        Me.pnlMain.TabIndex = 302
        '
        'Panel3
        '
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.txt_Nativesum)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 390)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(1001, 162)
        Me.Panel3.TabIndex = 303
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(10, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(141, 19)
        Me.Label2.TabIndex = 306
        Me.Label2.Text = "Narrative Summary :"
        '
        'txt_Nativesum
        '
        Me.txt_Nativesum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Nativesum.ForeColor = System.Drawing.Color.Black
        Me.txt_Nativesum.Location = New System.Drawing.Point(79, 25)
        Me.txt_Nativesum.MaxLength = 2000
        Me.txt_Nativesum.Multiline = True
        Me.txt_Nativesum.Name = "txt_Nativesum"
        Me.txt_Nativesum.Size = New System.Drawing.Size(903, 125)
        Me.txt_Nativesum.TabIndex = 25
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 158)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(993, 1)
        Me.Label12.TabIndex = 296
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(993, 1)
        Me.Label17.TabIndex = 295
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 159)
        Me.Label19.TabIndex = 297
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(997, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 159)
        Me.Label21.TabIndex = 298
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label47)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label45)
        Me.Panel2.Controls.Add(Me.Label34)
        Me.Panel2.Controls.Add(Me.Label46)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txt_lvmass)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txt_lvsystvol)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txt_lvdiastvol)
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.Label28)
        Me.Panel2.Controls.Add(Me.Label29)
        Me.Panel2.Controls.Add(Me.txt_mvarea)
        Me.Panel2.Controls.Add(Me.Label30)
        Me.Panel2.Controls.Add(Me.Label31)
        Me.Panel2.Controls.Add(Me.txtav_area)
        Me.Panel2.Controls.Add(Me.Label32)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Controls.Add(Me.txt_laarea)
        Me.Panel2.Controls.Add(Me.Label35)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 304)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(1001, 86)
        Me.Panel2.TabIndex = 7
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(831, 16)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(10, 11)
        Me.Label47.TabIndex = 312
        Me.Label47.Text = "2"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(542, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(10, 11)
        Me.Label22.TabIndex = 312
        Me.Label22.Text = "2"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(285, 15)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(10, 11)
        Me.Label45.TabIndex = 312
        Me.Label45.Text = "2"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(813, 53)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(14, 14)
        Me.Label34.TabIndex = 311
        Me.Label34.Text = "g"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(812, 16)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(23, 14)
        Me.Label46.TabIndex = 308
        Me.Label46.Text = "cm"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(523, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 14)
        Me.Label10.TabIndex = 308
        Me.Label10.Text = "cm"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(266, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 14)
        Me.Label9.TabIndex = 308
        Me.Label9.Text = "cm"
        '
        'txt_lvmass
        '
        Me.txt_lvmass.ForeColor = System.Drawing.Color.Black
        Me.txt_lvmass.Location = New System.Drawing.Point(726, 49)
        Me.txt_lvmass.MaxLength = 50
        Me.txt_lvmass.Name = "txt_lvmass"
        Me.txt_lvmass.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvmass.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(666, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 14)
        Me.Label3.TabIndex = 302
        Me.Label3.Text = "LV Mass :"
        '
        'txt_lvsystvol
        '
        Me.txt_lvsystvol.ForeColor = System.Drawing.Color.Black
        Me.txt_lvsystvol.Location = New System.Drawing.Point(437, 49)
        Me.txt_lvsystvol.MaxLength = 50
        Me.txt_lvsystvol.Name = "txt_lvsystvol"
        Me.txt_lvsystvol.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvsystvol.TabIndex = 23
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(359, 53)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 14)
        Me.Label8.TabIndex = 300
        Me.Label8.Text = "LV Syst vol :"
        '
        'txt_lvdiastvol
        '
        Me.txt_lvdiastvol.ForeColor = System.Drawing.Color.Black
        Me.txt_lvdiastvol.Location = New System.Drawing.Point(179, 49)
        Me.txt_lvdiastvol.MaxLength = 50
        Me.txt_lvdiastvol.Name = "txt_lvdiastvol"
        Me.txt_lvdiastvol.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvdiastvol.TabIndex = 22
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(4, 82)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(993, 1)
        Me.Label27.TabIndex = 296
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(99, 53)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(78, 14)
        Me.Label28.TabIndex = 283
        Me.Label28.Text = "LV Diast vol :"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(993, 1)
        Me.Label29.TabIndex = 295
        '
        'txt_mvarea
        '
        Me.txt_mvarea.ForeColor = System.Drawing.Color.Black
        Me.txt_mvarea.Location = New System.Drawing.Point(726, 12)
        Me.txt_mvarea.MaxLength = 50
        Me.txt_mvarea.Name = "txt_mvarea"
        Me.txt_mvarea.Size = New System.Drawing.Size(83, 22)
        Me.txt_mvarea.TabIndex = 21
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 83)
        Me.Label30.TabIndex = 297
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(663, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(61, 14)
        Me.Label31.TabIndex = 281
        Me.Label31.Text = "MV Area :"
        '
        'txtav_area
        '
        Me.txtav_area.ForeColor = System.Drawing.Color.Black
        Me.txtav_area.Location = New System.Drawing.Point(437, 11)
        Me.txtav_area.MaxLength = 50
        Me.txtav_area.Name = "txtav_area"
        Me.txtav_area.Size = New System.Drawing.Size(83, 22)
        Me.txtav_area.TabIndex = 20
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(997, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 83)
        Me.Label32.TabIndex = 298
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(374, 15)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(60, 14)
        Me.Label33.TabIndex = 279
        Me.Label33.Text = "AV Area :"
        '
        'txt_laarea
        '
        Me.txt_laarea.ForeColor = System.Drawing.Color.Black
        Me.txt_laarea.Location = New System.Drawing.Point(179, 11)
        Me.txt_laarea.MaxLength = 50
        Me.txt_laarea.Name = "txt_laarea"
        Me.txt_laarea.Size = New System.Drawing.Size(83, 22)
        Me.txt_laarea.TabIndex = 19
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(119, 15)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(58, 14)
        Me.Label35.TabIndex = 277
        Me.Label35.Text = "LA Area :"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label36)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txt_mitral)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.txt_arotic)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 240)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1001, 64)
        Me.Panel1.TabIndex = 6
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(266, 29)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(42, 14)
        Me.Label36.TabIndex = 312
        Me.Label36.Text = "mmHg"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(4, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(10, 5, 0, 0)
        Me.Label23.Size = New System.Drawing.Size(135, 19)
        Me.Label23.TabIndex = 263
        Me.Label23.Text = "Doppler Gradients :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(4, 60)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(993, 1)
        Me.Label13.TabIndex = 296
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(993, 1)
        Me.Label15.TabIndex = 295
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 61)
        Me.Label16.TabIndex = 297
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(997, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 61)
        Me.Label18.TabIndex = 298
        '
        'txt_mitral
        '
        Me.txt_mitral.ForeColor = System.Drawing.Color.Black
        Me.txt_mitral.Location = New System.Drawing.Point(437, 25)
        Me.txt_mitral.MaxLength = 50
        Me.txt_mitral.Name = "txt_mitral"
        Me.txt_mitral.Size = New System.Drawing.Size(83, 22)
        Me.txt_mitral.TabIndex = 18
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(130, 29)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(47, 14)
        Me.Label24.TabIndex = 265
        Me.Label24.Text = "Arotic :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(391, 29)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(43, 14)
        Me.Label25.TabIndex = 269
        Me.Label25.Text = "Mitral :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_arotic
        '
        Me.txt_arotic.ForeColor = System.Drawing.Color.Black
        Me.txt_arotic.Location = New System.Drawing.Point(179, 25)
        Me.txt_arotic.MaxLength = 50
        Me.txt_arotic.Name = "txt_arotic"
        Me.txt_arotic.Size = New System.Drawing.Size(83, 22)
        Me.txt_arotic.TabIndex = 17
        '
        'pnlPressure
        '
        Me.pnlPressure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPressure.Controls.Add(Me.Label44)
        Me.pnlPressure.Controls.Add(Me.Label43)
        Me.pnlPressure.Controls.Add(Me.Label42)
        Me.pnlPressure.Controls.Add(Me.Label37)
        Me.pnlPressure.Controls.Add(Me.lblP_RA)
        Me.pnlPressure.Controls.Add(Me.Label26)
        Me.pnlPressure.Controls.Add(Me.Label38)
        Me.pnlPressure.Controls.Add(Me.Label39)
        Me.pnlPressure.Controls.Add(Me.Label40)
        Me.pnlPressure.Controls.Add(Me.Label41)
        Me.pnlPressure.Controls.Add(Me.txt_lvedd)
        Me.pnlPressure.Controls.Add(Me.lblP_LA)
        Me.pnlPressure.Controls.Add(Me.txt_lvesd)
        Me.pnlPressure.Controls.Add(Me.lblP_RightPulmonary)
        Me.pnlPressure.Controls.Add(Me.txt_lvthick)
        Me.pnlPressure.Controls.Add(Me.lblP_LeftPulmonary)
        Me.pnlPressure.Controls.Add(Me.txt_septhik)
        Me.pnlPressure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPressure.Location = New System.Drawing.Point(0, 147)
        Me.pnlPressure.Name = "pnlPressure"
        Me.pnlPressure.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPressure.Size = New System.Drawing.Size(1001, 93)
        Me.pnlPressure.TabIndex = 5
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(523, 29)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(27, 14)
        Me.Label44.TabIndex = 315
        Me.Label44.Text = "mm"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(266, 60)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(27, 14)
        Me.Label43.TabIndex = 315
        Me.Label43.Text = "mm"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(523, 60)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(27, 14)
        Me.Label42.TabIndex = 314
        Me.Label42.Text = "mm"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(266, 27)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(27, 14)
        Me.Label37.TabIndex = 313
        Me.Label37.Text = "mm"
        '
        'lblP_RA
        '
        Me.lblP_RA.AutoSize = True
        Me.lblP_RA.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblP_RA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_RA.Location = New System.Drawing.Point(4, 1)
        Me.lblP_RA.Name = "lblP_RA"
        Me.lblP_RA.Padding = New System.Windows.Forms.Padding(10, 5, 0, 0)
        Me.lblP_RA.Size = New System.Drawing.Size(75, 19)
        Me.lblP_RA.TabIndex = 257
        Me.lblP_RA.Text = "M-Mode :"
        Me.lblP_RA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(125, 27)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(52, 14)
        Me.Label26.TabIndex = 307
        Me.Label26.Text = "LVEDD :"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(4, 89)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(993, 1)
        Me.Label38.TabIndex = 296
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(993, 1)
        Me.Label39.TabIndex = 295
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 90)
        Me.Label40.TabIndex = 297
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(997, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 90)
        Me.Label41.TabIndex = 298
        '
        'txt_lvedd
        '
        Me.txt_lvedd.ForeColor = System.Drawing.Color.Black
        Me.txt_lvedd.Location = New System.Drawing.Point(179, 23)
        Me.txt_lvedd.MaxLength = 50
        Me.txt_lvedd.Name = "txt_lvedd"
        Me.txt_lvedd.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvedd.TabIndex = 13
        '
        'lblP_LA
        '
        Me.lblP_LA.AutoSize = True
        Me.lblP_LA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_LA.Location = New System.Drawing.Point(383, 27)
        Me.lblP_LA.Name = "lblP_LA"
        Me.lblP_LA.Size = New System.Drawing.Size(51, 14)
        Me.lblP_LA.TabIndex = 259
        Me.lblP_LA.Text = "LVESD :"
        Me.lblP_LA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_lvesd
        '
        Me.txt_lvesd.ForeColor = System.Drawing.Color.Black
        Me.txt_lvesd.Location = New System.Drawing.Point(437, 23)
        Me.txt_lvesd.MaxLength = 50
        Me.txt_lvesd.Name = "txt_lvesd"
        Me.txt_lvesd.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvesd.TabIndex = 14
        '
        'lblP_RightPulmonary
        '
        Me.lblP_RightPulmonary.AutoSize = True
        Me.lblP_RightPulmonary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_RightPulmonary.Location = New System.Drawing.Point(13, 60)
        Me.lblP_RightPulmonary.Name = "lblP_RightPulmonary"
        Me.lblP_RightPulmonary.Size = New System.Drawing.Size(164, 14)
        Me.lblP_RightPulmonary.TabIndex = 263
        Me.lblP_RightPulmonary.Text = "LV Posterior Wall Thickness :"
        Me.lblP_RightPulmonary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_lvthick
        '
        Me.txt_lvthick.ForeColor = System.Drawing.Color.Black
        Me.txt_lvthick.Location = New System.Drawing.Point(179, 56)
        Me.txt_lvthick.MaxLength = 50
        Me.txt_lvthick.Name = "txt_lvthick"
        Me.txt_lvthick.Size = New System.Drawing.Size(83, 22)
        Me.txt_lvthick.TabIndex = 15
        '
        'lblP_LeftPulmonary
        '
        Me.lblP_LeftPulmonary.AutoSize = True
        Me.lblP_LeftPulmonary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblP_LeftPulmonary.Location = New System.Drawing.Point(328, 60)
        Me.lblP_LeftPulmonary.Name = "lblP_LeftPulmonary"
        Me.lblP_LeftPulmonary.Size = New System.Drawing.Size(106, 14)
        Me.lblP_LeftPulmonary.TabIndex = 265
        Me.lblP_LeftPulmonary.Text = "Septal Thickness :"
        Me.lblP_LeftPulmonary.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_septhik
        '
        Me.txt_septhik.ForeColor = System.Drawing.Color.Black
        Me.txt_septhik.Location = New System.Drawing.Point(437, 56)
        Me.txt_septhik.MaxLength = 50
        Me.txt_septhik.Name = "txt_septhik"
        Me.txt_septhik.Size = New System.Drawing.Size(83, 22)
        Me.txt_septhik.TabIndex = 16
        '
        'pnlSelectFromList
        '
        Me.pnlSelectFromList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectFromList.Controls.Add(Me.lblid)
        Me.pnlSelectFromList.Controls.Add(Me.bntidclr)
        Me.pnlSelectFromList.Controls.Add(Me.btnidcls)
        Me.pnlSelectFromList.Controls.Add(Me.btnidbr)
        Me.pnlSelectFromList.Controls.Add(Me.lstIDPhy)
        Me.pnlSelectFromList.Controls.Add(Me.lblproc)
        Me.pnlSelectFromList.Controls.Add(Me.btnpnameclr)
        Me.pnlSelectFromList.Controls.Add(Me.btnpnamecls)
        Me.pnlSelectFromList.Controls.Add(Me.btnpnamebr)
        Me.pnlSelectFromList.Controls.Add(Me.lstProcName)
        Me.pnlSelectFromList.Controls.Add(Me.Label4)
        Me.pnlSelectFromList.Controls.Add(Me.Label1)
        Me.pnlSelectFromList.Controls.Add(Me.lblCPTCode)
        Me.pnlSelectFromList.Controls.Add(Me.Label5)
        Me.pnlSelectFromList.Controls.Add(Me.btncptclr)
        Me.pnlSelectFromList.Controls.Add(Me.Label6)
        Me.pnlSelectFromList.Controls.Add(Me.btncptcls)
        Me.pnlSelectFromList.Controls.Add(Me.btncptbr)
        Me.pnlSelectFromList.Controls.Add(Me.lstCPTcode)
        Me.pnlSelectFromList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectFromList.Location = New System.Drawing.Point(0, 45)
        Me.pnlSelectFromList.Name = "pnlSelectFromList"
        Me.pnlSelectFromList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSelectFromList.Size = New System.Drawing.Size(1001, 102)
        Me.pnlSelectFromList.TabIndex = 4
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.BackColor = System.Drawing.Color.Transparent
        Me.lblid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblid.Location = New System.Drawing.Point(620, 16)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(82, 14)
        Me.lblid.TabIndex = 308
        Me.lblid.Text = " Physician(s) :"
        Me.lblid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bntidclr
        '
        Me.bntidclr.BackColor = System.Drawing.Color.Transparent
        Me.bntidclr.BackgroundImage = CType(resources.GetObject("bntidclr.BackgroundImage"), System.Drawing.Image)
        Me.bntidclr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.bntidclr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.bntidclr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bntidclr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bntidclr.Image = CType(resources.GetObject("bntidclr.Image"), System.Drawing.Image)
        Me.bntidclr.Location = New System.Drawing.Point(922, 65)
        Me.bntidclr.Name = "bntidclr"
        Me.bntidclr.Size = New System.Drawing.Size(21, 21)
        Me.bntidclr.TabIndex = 12
        Me.C1SuperTooltip1.SetToolTip(Me.bntidclr, "Clear All Physicians")
        Me.bntidclr.UseVisualStyleBackColor = False
        '
        'btnidcls
        '
        Me.btnidcls.BackColor = System.Drawing.Color.Transparent
        Me.btnidcls.BackgroundImage = CType(resources.GetObject("btnidcls.BackgroundImage"), System.Drawing.Image)
        Me.btnidcls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnidcls.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnidcls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnidcls.Image = CType(resources.GetObject("btnidcls.Image"), System.Drawing.Image)
        Me.btnidcls.Location = New System.Drawing.Point(922, 39)
        Me.btnidcls.Name = "btnidcls"
        Me.btnidcls.Size = New System.Drawing.Size(21, 21)
        Me.btnidcls.TabIndex = 11
        Me.C1SuperTooltip1.SetToolTip(Me.btnidcls, "Clear Physician")
        Me.btnidcls.UseVisualStyleBackColor = False
        '
        'btnidbr
        '
        Me.btnidbr.BackColor = System.Drawing.Color.Transparent
        Me.btnidbr.BackgroundImage = CType(resources.GetObject("btnidbr.BackgroundImage"), System.Drawing.Image)
        Me.btnidbr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnidbr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnidbr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnidbr.Image = CType(resources.GetObject("btnidbr.Image"), System.Drawing.Image)
        Me.btnidbr.Location = New System.Drawing.Point(922, 14)
        Me.btnidbr.Name = "btnidbr"
        Me.btnidbr.Size = New System.Drawing.Size(21, 21)
        Me.btnidbr.TabIndex = 10
        Me.C1SuperTooltip1.SetToolTip(Me.btnidbr, "Browse Physician")
        Me.btnidbr.UseVisualStyleBackColor = False
        '
        'lstIDPhy
        '
        Me.lstIDPhy.FormattingEnabled = True
        Me.lstIDPhy.ItemHeight = 14
        Me.lstIDPhy.Location = New System.Drawing.Point(705, 13)
        Me.lstIDPhy.Name = "lstIDPhy"
        Me.lstIDPhy.Size = New System.Drawing.Size(212, 74)
        Me.lstIDPhy.TabIndex = 9
        '
        'lblproc
        '
        Me.lblproc.AutoSize = True
        Me.lblproc.BackColor = System.Drawing.Color.Transparent
        Me.lblproc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblproc.Location = New System.Drawing.Point(269, 16)
        Me.lblproc.Name = "lblproc"
        Me.lblproc.Size = New System.Drawing.Size(106, 14)
        Me.lblproc.TabIndex = 303
        Me.lblproc.Text = "Procedure Name :"
        Me.lblproc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnpnameclr
        '
        Me.btnpnameclr.BackColor = System.Drawing.Color.Transparent
        Me.btnpnameclr.BackgroundImage = CType(resources.GetObject("btnpnameclr.BackgroundImage"), System.Drawing.Image)
        Me.btnpnameclr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnpnameclr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnpnameclr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpnameclr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpnameclr.Image = CType(resources.GetObject("btnpnameclr.Image"), System.Drawing.Image)
        Me.btnpnameclr.Location = New System.Drawing.Point(593, 65)
        Me.btnpnameclr.Name = "btnpnameclr"
        Me.btnpnameclr.Size = New System.Drawing.Size(21, 21)
        Me.btnpnameclr.TabIndex = 8
        Me.C1SuperTooltip1.SetToolTip(Me.btnpnameclr, "Clear All Procedures")
        Me.btnpnameclr.UseVisualStyleBackColor = False
        '
        'btnpnamecls
        '
        Me.btnpnamecls.BackColor = System.Drawing.Color.Transparent
        Me.btnpnamecls.BackgroundImage = CType(resources.GetObject("btnpnamecls.BackgroundImage"), System.Drawing.Image)
        Me.btnpnamecls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnpnamecls.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnpnamecls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpnamecls.Image = CType(resources.GetObject("btnpnamecls.Image"), System.Drawing.Image)
        Me.btnpnamecls.Location = New System.Drawing.Point(593, 39)
        Me.btnpnamecls.Name = "btnpnamecls"
        Me.btnpnamecls.Size = New System.Drawing.Size(21, 21)
        Me.btnpnamecls.TabIndex = 7
        Me.C1SuperTooltip1.SetToolTip(Me.btnpnamecls, "Clear Procedure")
        Me.btnpnamecls.UseVisualStyleBackColor = False
        '
        'btnpnamebr
        '
        Me.btnpnamebr.BackColor = System.Drawing.Color.Transparent
        Me.btnpnamebr.BackgroundImage = CType(resources.GetObject("btnpnamebr.BackgroundImage"), System.Drawing.Image)
        Me.btnpnamebr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnpnamebr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnpnamebr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpnamebr.Image = CType(resources.GetObject("btnpnamebr.Image"), System.Drawing.Image)
        Me.btnpnamebr.Location = New System.Drawing.Point(593, 14)
        Me.btnpnamebr.Name = "btnpnamebr"
        Me.btnpnamebr.Size = New System.Drawing.Size(21, 21)
        Me.btnpnamebr.TabIndex = 6
        Me.C1SuperTooltip1.SetToolTip(Me.btnpnamebr, "Browse Procedure")
        Me.btnpnamebr.UseVisualStyleBackColor = False
        '
        'lstProcName
        '
        Me.lstProcName.FormattingEnabled = True
        Me.lstProcName.ItemHeight = 14
        Me.lstProcName.Location = New System.Drawing.Point(377, 13)
        Me.lstProcName.Name = "lstProcName"
        Me.lstProcName.Size = New System.Drawing.Size(212, 74)
        Me.lstProcName.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(993, 1)
        Me.Label4.TabIndex = 296
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(993, 1)
        Me.Label1.TabIndex = 295
        '
        'lblCPTCode
        '
        Me.lblCPTCode.AutoSize = True
        Me.lblCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPTCode.Location = New System.Drawing.Point(6, 16)
        Me.lblCPTCode.Name = "lblCPTCode"
        Me.lblCPTCode.Size = New System.Drawing.Size(68, 14)
        Me.lblCPTCode.TabIndex = 294
        Me.lblCPTCode.Text = "CPT code :"
        Me.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 99)
        Me.Label5.TabIndex = 297
        '
        'btncptclr
        '
        Me.btncptclr.BackColor = System.Drawing.Color.Transparent
        Me.btncptclr.BackgroundImage = CType(resources.GetObject("btncptclr.BackgroundImage"), System.Drawing.Image)
        Me.btncptclr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncptclr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncptclr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncptclr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncptclr.Image = CType(resources.GetObject("btncptclr.Image"), System.Drawing.Image)
        Me.btncptclr.Location = New System.Drawing.Point(233, 65)
        Me.btncptclr.Name = "btncptclr"
        Me.btncptclr.Size = New System.Drawing.Size(21, 21)
        Me.btncptclr.TabIndex = 4
        Me.C1SuperTooltip1.SetToolTip(Me.btncptclr, "Clear All CPT")
        Me.btncptclr.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(997, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 99)
        Me.Label6.TabIndex = 298
        '
        'btncptcls
        '
        Me.btncptcls.BackColor = System.Drawing.Color.Transparent
        Me.btncptcls.BackgroundImage = CType(resources.GetObject("btncptcls.BackgroundImage"), System.Drawing.Image)
        Me.btncptcls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncptcls.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncptcls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncptcls.Image = CType(resources.GetObject("btncptcls.Image"), System.Drawing.Image)
        Me.btncptcls.Location = New System.Drawing.Point(233, 39)
        Me.btncptcls.Name = "btncptcls"
        Me.btncptcls.Size = New System.Drawing.Size(21, 21)
        Me.btncptcls.TabIndex = 3
        Me.C1SuperTooltip1.SetToolTip(Me.btncptcls, "Clear CPT")
        Me.btncptcls.UseVisualStyleBackColor = False
        '
        'btncptbr
        '
        Me.btncptbr.BackColor = System.Drawing.Color.Transparent
        Me.btncptbr.BackgroundImage = CType(resources.GetObject("btncptbr.BackgroundImage"), System.Drawing.Image)
        Me.btncptbr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncptbr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncptbr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncptbr.Image = CType(resources.GetObject("btncptbr.Image"), System.Drawing.Image)
        Me.btncptbr.Location = New System.Drawing.Point(233, 14)
        Me.btncptbr.Name = "btncptbr"
        Me.btncptbr.Size = New System.Drawing.Size(21, 21)
        Me.btncptbr.TabIndex = 2
        Me.C1SuperTooltip1.SetToolTip(Me.btncptbr, "Browse CPT ")
        Me.btncptbr.UseVisualStyleBackColor = False
        '
        'lstCPTcode
        '
        Me.lstCPTcode.FormattingEnabled = True
        Me.lstCPTcode.ItemHeight = 14
        Me.lstCPTcode.Location = New System.Drawing.Point(76, 13)
        Me.lstCPTcode.Name = "lstCPTcode"
        Me.lstCPTcode.Size = New System.Drawing.Size(152, 74)
        Me.lstCPTcode.TabIndex = 0
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Controls.Add(Me.Label20)
        Me.pnlcustomTask.Controls.Add(Me.Label14)
        Me.pnlcustomTask.Controls.Add(Me.Label11)
        Me.pnlcustomTask.Controls.Add(Me.Label7)
        Me.pnlcustomTask.Location = New System.Drawing.Point(233, 138)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(381, 97)
        Me.pnlcustomTask.TabIndex = 309
        Me.pnlcustomTask.Visible = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 95)
        Me.Label20.TabIndex = 300
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(380, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 95)
        Me.Label14.TabIndex = 299
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(381, 1)
        Me.Label11.TabIndex = 298
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(381, 1)
        Me.Label7.TabIndex = 297
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tsCatheterization)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1001, 56)
        Me.pnlToolStrip.TabIndex = 303
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmCV_Echocardiogram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1001, 608)
        Me.Controls.Add(Me.pnlcustomTask)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_Echocardiogram"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Echocardiogram"
        Me.tsCatheterization.ResumeLayout(False)
        Me.tsCatheterization.PerformLayout()
        Me.pnlDateProcedure.ResumeLayout(False)
        Me.pnlDateProcedure.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlPressure.ResumeLayout(False)
        Me.pnlPressure.PerformLayout()
        Me.pnlSelectFromList.ResumeLayout(False)
        Me.pnlSelectFromList.PerformLayout()
        Me.pnlcustomTask.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsCatheterization As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlDateProcedure As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents dtdop As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents pnlSelectFromList As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCPTCode As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btncptclr As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btncptcls As System.Windows.Forms.Button
    Friend WithEvents btncptbr As System.Windows.Forms.Button
    Friend WithEvents lstCPTcode As System.Windows.Forms.ListBox
    Friend WithEvents pnlPressure As System.Windows.Forms.Panel
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblP_RA As System.Windows.Forms.Label
    Friend WithEvents txt_lvedd As System.Windows.Forms.TextBox
    Friend WithEvents lblP_LA As System.Windows.Forms.Label
    Friend WithEvents txt_lvesd As System.Windows.Forms.TextBox
    Friend WithEvents lblP_RightPulmonary As System.Windows.Forms.Label
    Friend WithEvents txt_lvthick As System.Windows.Forms.TextBox
    Friend WithEvents lblP_LeftPulmonary As System.Windows.Forms.Label
    Friend WithEvents txt_septhik As System.Windows.Forms.TextBox
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents bntidclr As System.Windows.Forms.Button
    Friend WithEvents btnidcls As System.Windows.Forms.Button
    Friend WithEvents btnidbr As System.Windows.Forms.Button
    Friend WithEvents lstIDPhy As System.Windows.Forms.ListBox
    Friend WithEvents lblproc As System.Windows.Forms.Label
    Friend WithEvents btnpnameclr As System.Windows.Forms.Button
    Friend WithEvents btnpnamecls As System.Windows.Forms.Button
    Friend WithEvents btnpnamebr As System.Windows.Forms.Button
    Friend WithEvents lstProcName As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txt_mitral As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txt_arotic As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txt_laarea As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtav_area As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txt_mvarea As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txt_lvdiastvol As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_lvsystvol As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_lvmass As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_Nativesum As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label

End Class
