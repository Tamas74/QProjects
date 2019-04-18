<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_PatientSpecific
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtDueDate, dtStartDate, dtEndDate}
                Dim cntControls() As System.Windows.Forms.Control = {dtDueDate, dtStartDate, dtEndDate}
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cntHealthPlan}


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

                

            End If
            If (IsNothing(oDM) = False) Then
                oDM.Dispose()
                oDM = Nothing
            End If
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_PatientSpecific))
        Me.tlsPatientDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_RecommendationviewRecommendation = New System.Windows.Forms.ToolStripButton()
        Me.tlsSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlCriteria = New System.Windows.Forms.Panel()
        Me.trvHealthPlan = New System.Windows.Forms.TreeView()
        Me.cntHealthPlan = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlEdit = New System.Windows.Forms.Panel()
        Me.pnlReason = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlRecurring = New System.Windows.Forms.Panel()
        Me.chkOnEveryCheckIn = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbDurationType = New System.Windows.Forms.ComboBox()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDueType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chckRecurring = New System.Windows.Forms.CheckBox()
        Me.dtDueDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbValue = New System.Windows.Forms.ComboBox()
        Me.cmbOperator = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblOrder = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnl_tls_ = New System.Windows.Forms.Panel()
        Me.tlsDetails = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlRecomendationAlert = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblLastRecomendationName = New System.Windows.Forms.Label()
        Me.lblRecomendationAlert = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.tlsPatientDM.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlCriteria.SuspendLayout()
        Me.pnlEdit.SuspendLayout()
        Me.pnlReason.SuspendLayout()
        Me.pnlRecurring.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsDetails.SuspendLayout()
        Me.pnlRecomendationAlert.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlsPatientDM
        '
        Me.tlsPatientDM.BackColor = System.Drawing.Color.Transparent
        Me.tlsPatientDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPatientDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPatientDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPatientDM.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsPatientDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPatientDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_RecommendationviewRecommendation, Me.tlsSave, Me.ts_btnClose})
        Me.tlsPatientDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientDM.Name = "tlsPatientDM"
        Me.tlsPatientDM.Size = New System.Drawing.Size(963, 53)
        Me.tlsPatientDM.TabIndex = 0
        Me.tlsPatientDM.Text = "ToolStrip1"
        '
        'btn_tls_RecommendationviewRecommendation
        '
        Me.btn_tls_RecommendationviewRecommendation.Image = CType(resources.GetObject("btn_tls_RecommendationviewRecommendation.Image"), System.Drawing.Image)
        Me.btn_tls_RecommendationviewRecommendation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_RecommendationviewRecommendation.Name = "btn_tls_RecommendationviewRecommendation"
        Me.btn_tls_RecommendationviewRecommendation.Size = New System.Drawing.Size(151, 50)
        Me.btn_tls_RecommendationviewRecommendation.Tag = "ViewRecommendation"
        Me.btn_tls_RecommendationviewRecommendation.Text = "View Recommendation"
        Me.btn_tls_RecommendationviewRecommendation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_RecommendationviewRecommendation.ToolTipText = "View Recommendation"
        Me.btn_tls_RecommendationviewRecommendation.Visible = False
        '
        'tlsSave
        '
        Me.tlsSave.Image = CType(resources.GetObject("tlsSave.Image"), System.Drawing.Image)
        Me.tlsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsSave.Name = "tlsSave"
        Me.tlsSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsSave.Tag = "Save"
        Me.tlsSave.Text = "&Save&&Cls"
        Me.tlsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsPatientDM)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(963, 54)
        Me.pnlToolStrip.TabIndex = 15
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Patient List01.ico")
        Me.ImageList1.Images.SetKeyName(2, "History.ico")
        Me.ImageList1.Images.SetKeyName(3, "ICD 09.ico")
        Me.ImageList1.Images.SetKeyName(4, "Drugs.ico")
        Me.ImageList1.Images.SetKeyName(5, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(6, "Lab.ico")
        Me.ImageList1.Images.SetKeyName(7, "Radiology.ico")
        Me.ImageList1.Images.SetKeyName(8, "Guideline.ico")
        Me.ImageList1.Images.SetKeyName(9, "RX.ico")
        Me.ImageList1.Images.SetKeyName(10, "PatientDetails.ico")
        Me.ImageList1.Images.SetKeyName(11, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(12, "New patientr Criteria.ico")
        Me.ImageList1.Images.SetKeyName(13, "Delete Health Plan.ico")
        Me.ImageList1.Images.SetKeyName(14, "Modify Health Plan.ico")
        Me.ImageList1.Images.SetKeyName(15, "Edit Guideline.ico")
        Me.ImageList1.Images.SetKeyName(16, "Modifer.ico")
        Me.ImageList1.Images.SetKeyName(17, "Health plan (inbetween).ico")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlCriteria)
        Me.pnlMain.Controls.Add(Me.pnlEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(3, 86)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlMain.Size = New System.Drawing.Size(960, 656)
        Me.pnlMain.TabIndex = 20
        '
        'pnlCriteria
        '
        Me.pnlCriteria.Controls.Add(Me.trvHealthPlan)
        Me.pnlCriteria.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlCriteria.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlCriteria.Controls.Add(Me.lbl_RightBrd)
        Me.pnlCriteria.Controls.Add(Me.lbl_TopBrd)
        Me.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCriteria.Location = New System.Drawing.Point(0, 3)
        Me.pnlCriteria.Name = "pnlCriteria"
        Me.pnlCriteria.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlCriteria.Size = New System.Drawing.Size(960, 276)
        Me.pnlCriteria.TabIndex = 16
        '
        'trvHealthPlan
        '
        Me.trvHealthPlan.BackColor = System.Drawing.Color.White
        Me.trvHealthPlan.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHealthPlan.CheckBoxes = True
        Me.trvHealthPlan.ContextMenuStrip = Me.cntHealthPlan
        Me.trvHealthPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHealthPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHealthPlan.ForeColor = System.Drawing.Color.Black
        Me.trvHealthPlan.ImageIndex = 0
        Me.trvHealthPlan.ImageList = Me.ImageList1
        Me.trvHealthPlan.Indent = 20
        Me.trvHealthPlan.ItemHeight = 20
        Me.trvHealthPlan.Location = New System.Drawing.Point(4, 4)
        Me.trvHealthPlan.Name = "trvHealthPlan"
        Me.trvHealthPlan.SelectedImageIndex = 0
        Me.trvHealthPlan.ShowLines = False
        Me.trvHealthPlan.Size = New System.Drawing.Size(952, 268)
        Me.trvHealthPlan.TabIndex = 0
        '
        'cntHealthPlan
        '
        Me.cntHealthPlan.Name = "cntHealthPlan"
        Me.cntHealthPlan.Size = New System.Drawing.Size(61, 4)
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 272)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(952, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 269)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(956, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 269)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(954, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlEdit
        '
        Me.pnlEdit.BackColor = System.Drawing.Color.Transparent
        Me.pnlEdit.Controls.Add(Me.pnlReason)
        Me.pnlEdit.Controls.Add(Me.pnlRecurring)
        Me.pnlEdit.Controls.Add(Me.Panel2)
        Me.pnlEdit.Controls.Add(Me.Panel1)
        Me.pnlEdit.Controls.Add(Me.pnl_tls_)
        Me.pnlEdit.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlEdit.Location = New System.Drawing.Point(0, 279)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(960, 377)
        Me.pnlEdit.TabIndex = 13
        Me.pnlEdit.Visible = False
        '
        'pnlReason
        '
        Me.pnlReason.Controls.Add(Me.Label23)
        Me.pnlReason.Controls.Add(Me.Label24)
        Me.pnlReason.Controls.Add(Me.Label25)
        Me.pnlReason.Controls.Add(Me.Label26)
        Me.pnlReason.Controls.Add(Me.txtNotes)
        Me.pnlReason.Controls.Add(Me.Label4)
        Me.pnlReason.Controls.Add(Me.txtReason)
        Me.pnlReason.Controls.Add(Me.Label2)
        Me.pnlReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReason.Location = New System.Drawing.Point(0, 233)
        Me.pnlReason.Name = "pnlReason"
        Me.pnlReason.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlReason.Size = New System.Drawing.Size(960, 144)
        Me.pnlReason.TabIndex = 16
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 140)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(955, 1)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 140)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(956, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 140)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(957, 1)
        Me.Label26.TabIndex = 5
        Me.Label26.Text = "label1"
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.Color.White
        Me.txtNotes.Location = New System.Drawing.Point(100, 44)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(400, 81)
        Me.txtNotes.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(44, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Notes :"
        '
        'txtReason
        '
        Me.txtReason.BackColor = System.Drawing.Color.White
        Me.txtReason.Location = New System.Drawing.Point(100, 14)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(400, 22)
        Me.txtReason.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(34, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Reason :"
        '
        'pnlRecurring
        '
        Me.pnlRecurring.Controls.Add(Me.chkOnEveryCheckIn)
        Me.pnlRecurring.Controls.Add(Me.Label19)
        Me.pnlRecurring.Controls.Add(Me.Label20)
        Me.pnlRecurring.Controls.Add(Me.Label21)
        Me.pnlRecurring.Controls.Add(Me.Label22)
        Me.pnlRecurring.Controls.Add(Me.Label6)
        Me.pnlRecurring.Controls.Add(Me.cmbDurationType)
        Me.pnlRecurring.Controls.Add(Me.cmbPeriod)
        Me.pnlRecurring.Controls.Add(Me.dtEndDate)
        Me.pnlRecurring.Controls.Add(Me.dtStartDate)
        Me.pnlRecurring.Controls.Add(Me.Label8)
        Me.pnlRecurring.Controls.Add(Me.Label9)
        Me.pnlRecurring.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRecurring.Location = New System.Drawing.Point(0, 157)
        Me.pnlRecurring.Name = "pnlRecurring"
        Me.pnlRecurring.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlRecurring.Size = New System.Drawing.Size(960, 76)
        Me.pnlRecurring.TabIndex = 16
        Me.pnlRecurring.Visible = False
        '
        'chkOnEveryCheckIn
        '
        Me.chkOnEveryCheckIn.AutoSize = True
        Me.chkOnEveryCheckIn.Location = New System.Drawing.Point(286, 44)
        Me.chkOnEveryCheckIn.Name = "chkOnEveryCheckIn"
        Me.chkOnEveryCheckIn.Size = New System.Drawing.Size(128, 18)
        Me.chkOnEveryCheckIn.TabIndex = 17
        Me.chkOnEveryCheckIn.Text = "On Every Check In"
        Me.chkOnEveryCheckIn.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(1, 72)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(955, 1)
        Me.Label19.TabIndex = 16
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 72)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(956, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 72)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(957, 1)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "label1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(235, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 14)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "End Date :"
        '
        'cmbDurationType
        '
        Me.cmbDurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDurationType.FormattingEnabled = True
        Me.cmbDurationType.Items.AddRange(New Object() {"Days", "Weeks", "Months", "Years"})
        Me.cmbDurationType.Location = New System.Drawing.Point(165, 42)
        Me.cmbDurationType.Name = "cmbDurationType"
        Me.cmbDurationType.Size = New System.Drawing.Size(113, 22)
        Me.cmbDurationType.TabIndex = 11
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Date", "Age"})
        Me.cmbPeriod.Location = New System.Drawing.Point(100, 42)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(58, 22)
        Me.cmbPeriod.TabIndex = 10
        '
        'dtEndDate
        '
        Me.dtEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(309, 13)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(104, 22)
        Me.dtEndDate.TabIndex = 9
        '
        'dtStartDate
        '
        Me.dtStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(100, 13)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(112, 22)
        Me.dtStartDate.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(14, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 14)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Start Date :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(7, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 14)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Recur every :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmbDueType)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.chckRecurring)
        Me.Panel2.Controls.Add(Me.dtDueDate)
        Me.Panel2.Controls.Add(Me.cmbValue)
        Me.Panel2.Controls.Add(Me.cmbOperator)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 82)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(960, 75)
        Me.Panel2.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 71)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(955, 1)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 71)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(956, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 71)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(957, 1)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "label1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(35, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Due on :"
        '
        'cmbDueType
        '
        Me.cmbDueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDueType.FormattingEnabled = True
        Me.cmbDueType.Items.AddRange(New Object() {"Date", "Age"})
        Me.cmbDueType.Location = New System.Drawing.Point(100, 10)
        Me.cmbDueType.Name = "cmbDueType"
        Me.cmbDueType.Size = New System.Drawing.Size(181, 22)
        Me.cmbDueType.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Due when :"
        '
        'chckRecurring
        '
        Me.chckRecurring.AutoSize = True
        Me.chckRecurring.Location = New System.Drawing.Point(219, 44)
        Me.chckRecurring.Name = "chckRecurring"
        Me.chckRecurring.Size = New System.Drawing.Size(90, 18)
        Me.chckRecurring.TabIndex = 9
        Me.chckRecurring.Text = "Is Recurring"
        Me.chckRecurring.UseVisualStyleBackColor = True
        '
        'dtDueDate
        '
        Me.dtDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDueDate.Location = New System.Drawing.Point(100, 42)
        Me.dtDueDate.Name = "dtDueDate"
        Me.dtDueDate.Size = New System.Drawing.Size(112, 22)
        Me.dtDueDate.TabIndex = 8
        '
        'cmbValue
        '
        Me.cmbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValue.ForeColor = System.Drawing.Color.Black
        Me.cmbValue.FormattingEnabled = True
        Me.cmbValue.Items.AddRange(New Object() {"Date", "Age"})
        Me.cmbValue.Location = New System.Drawing.Point(286, 42)
        Me.cmbValue.Name = "cmbValue"
        Me.cmbValue.Size = New System.Drawing.Size(65, 22)
        Me.cmbValue.TabIndex = 7
        '
        'cmbOperator
        '
        Me.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperator.ForeColor = System.Drawing.Color.Black
        Me.cmbOperator.FormattingEnabled = True
        Me.cmbOperator.Location = New System.Drawing.Point(100, 42)
        Me.cmbOperator.Name = "cmbOperator"
        Me.cmbOperator.Size = New System.Drawing.Size(181, 22)
        Me.cmbOperator.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(960, 28)
        Me.Panel1.TabIndex = 7
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label12)
        Me.Panel7.Controls.Add(Me.Label13)
        Me.Panel7.Controls.Add(Me.Label14)
        Me.Panel7.Controls.Add(Me.lblOrder)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(957, 22)
        Me.Panel7.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 20)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(956, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 20)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(957, 1)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "label1"
        '
        'lblOrder
        '
        Me.lblOrder.BackColor = System.Drawing.Color.Transparent
        Me.lblOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOrder.Location = New System.Drawing.Point(0, 0)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(957, 21)
        Me.lblOrder.TabIndex = 2
        Me.lblOrder.Text = "  "
        Me.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(0, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(957, 1)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "label2"
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsDetails)
        Me.pnl_tls_.Controls.Add(Me.Label43)
        Me.pnl_tls_.Controls.Add(Me.Label44)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(960, 54)
        Me.pnl_tls_.TabIndex = 21
        '
        'tlsDetails
        '
        Me.tlsDetails.BackColor = System.Drawing.Color.Transparent
        Me.tlsDetails.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDetails.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok, Me.btn_tls_Cancel})
        Me.tlsDetails.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDetails.Location = New System.Drawing.Point(1, 1)
        Me.tlsDetails.Name = "tlsDetails"
        Me.tlsDetails.Size = New System.Drawing.Size(959, 53)
        Me.tlsDetails.TabIndex = 0
        Me.tlsDetails.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Tag = "OK"
        Me.btn_tls_Ok.Text = "&Save&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Cancel.Tag = "Cancel"
        Me.btn_tls_Cancel.Text = "&Close"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 53)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label4"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(960, 1)
        Me.Label44.TabIndex = 9
        Me.Label44.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Cursor = System.Windows.Forms.Cursors.VSplit
        Me.Splitter1.Location = New System.Drawing.Point(0, 86)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 656)
        Me.Splitter1.TabIndex = 21
        Me.Splitter1.TabStop = False
        '
        'pnlRecomendationAlert
        '
        Me.pnlRecomendationAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlRecomendationAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRecomendationAlert.Controls.Add(Me.Label3)
        Me.pnlRecomendationAlert.Controls.Add(Me.lblLastRecomendationName)
        Me.pnlRecomendationAlert.Controls.Add(Me.lblRecomendationAlert)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label10)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label11)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label27)
        Me.pnlRecomendationAlert.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRecomendationAlert.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlRecomendationAlert.Location = New System.Drawing.Point(0, 54)
        Me.pnlRecomendationAlert.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlRecomendationAlert.Name = "pnlRecomendationAlert"
        Me.pnlRecomendationAlert.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlRecomendationAlert.Size = New System.Drawing.Size(963, 32)
        Me.pnlRecomendationAlert.TabIndex = 146
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(959, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 27)
        Me.Label3.TabIndex = 144
        '
        'lblLastRecomendationName
        '
        Me.lblLastRecomendationName.AutoSize = True
        Me.lblLastRecomendationName.BackColor = System.Drawing.Color.Transparent
        Me.lblLastRecomendationName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLastRecomendationName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastRecomendationName.ForeColor = System.Drawing.Color.Black
        Me.lblLastRecomendationName.Location = New System.Drawing.Point(158, 4)
        Me.lblLastRecomendationName.Name = "lblLastRecomendationName"
        Me.lblLastRecomendationName.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lblLastRecomendationName.Size = New System.Drawing.Size(4, 18)
        Me.lblLastRecomendationName.TabIndex = 34
        Me.lblLastRecomendationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRecomendationAlert
        '
        Me.lblRecomendationAlert.AutoSize = True
        Me.lblRecomendationAlert.BackColor = System.Drawing.Color.Transparent
        Me.lblRecomendationAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRecomendationAlert.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecomendationAlert.ForeColor = System.Drawing.Color.Red
        Me.lblRecomendationAlert.Location = New System.Drawing.Point(4, 4)
        Me.lblRecomendationAlert.Name = "lblRecomendationAlert"
        Me.lblRecomendationAlert.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lblRecomendationAlert.Size = New System.Drawing.Size(154, 18)
        Me.lblRecomendationAlert.TabIndex = 33
        Me.lblRecomendationAlert.Text = "Recommendations (12)"
        Me.lblRecomendationAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(4, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(956, 1)
        Me.Label10.TabIndex = 142
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(4, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(956, 1)
        Me.Label11.TabIndex = 143
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 29)
        Me.Label27.TabIndex = 145
        '
        'frmDM_PatientSpecific
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(963, 742)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlRecomendationAlert)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_PatientSpecific"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find Health Plan"
        Me.tlsPatientDM.ResumeLayout(False)
        Me.tlsPatientDM.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlCriteria.ResumeLayout(False)
        Me.pnlEdit.ResumeLayout(False)
        Me.pnlReason.ResumeLayout(False)
        Me.pnlReason.PerformLayout()
        Me.pnlRecurring.ResumeLayout(False)
        Me.pnlRecurring.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsDetails.ResumeLayout(False)
        Me.tlsDetails.PerformLayout()
        Me.pnlRecomendationAlert.ResumeLayout(False)
        Me.pnlRecomendationAlert.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlsPatientDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCriteria As System.Windows.Forms.Panel
    Friend WithEvents trvHealthPlan As System.Windows.Forms.TreeView
    Friend WithEvents pnlEdit As System.Windows.Forms.Panel
    Friend WithEvents pnlReason As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbDueType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblOrder As System.Windows.Forms.Label
    Friend WithEvents cmbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents cmbValue As System.Windows.Forms.ComboBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents dtDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlRecurring As System.Windows.Forms.Panel
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbDurationType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chckRecurring As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsDetails As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cntHealthPlan As System.Windows.Forms.ContextMenuStrip
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    'Friend WithEvents mnu_EditGuideline As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkOnEveryCheckIn As System.Windows.Forms.CheckBox
    Friend WithEvents pnlRecomendationAlert As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblLastRecomendationName As System.Windows.Forms.Label
    Friend WithEvents lblRecomendationAlert As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btn_tls_RecommendationviewRecommendation As System.Windows.Forms.ToolStripButton
End Class
