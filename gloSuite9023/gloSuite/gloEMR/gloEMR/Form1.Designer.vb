<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tlbStripMain = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Masters = New System.Windows.Forms.ToolStripSplitButton()
        Me.tlbbtn_NewPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ModifyPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ScanCard = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_History = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Prescription = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Medication = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Orders = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_LabOrders = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_NewExam = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_PastExam = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_UnFinishedExams = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_Calender = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_ScanDocs = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_DocMGMT = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_Billing = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Payment = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_FormGallery = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_LockScreen = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.AxFramerControl1 = New AxDSOFramer.AxFramerControl()
        Me.AxDgnEngineControl1 = New AxDNSTools.AxDgnEngineControl()
        CType(Me.AxFramerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxDgnEngineControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlbStripMain
        '
        Me.tlbStripMain.BackColor = System.Drawing.Color.Transparent
        Me.tlbStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbStripMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlbStripMain.ImageScalingSize = New System.Drawing.Size(42, 42)
        Me.tlbStripMain.Location = New System.Drawing.Point(0, 0)
        Me.tlbStripMain.Name = "tlbStripMain"
        Me.tlbStripMain.Size = New System.Drawing.Size(902, 25)
        Me.tlbStripMain.TabIndex = 54
        '
        'tlbbtn_Masters
        '
        Me.tlbbtn_Masters.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Masters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Masters.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Masters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Masters.Image = CType(resources.GetObject("tlbbtn_Masters.Image"), System.Drawing.Image)
        Me.tlbbtn_Masters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Masters.Name = "tlbbtn_Masters"
        Me.tlbbtn_Masters.Size = New System.Drawing.Size(58, 59)
        Me.tlbbtn_Masters.Tag = "Masters"
        Me.tlbbtn_Masters.Text = "Edit"
        Me.tlbbtn_Masters.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Masters.ToolTipText = "Masters"
        Me.tlbbtn_Masters.Visible = False
        '
        'tlbbtn_NewPatient
        '
        Me.tlbbtn_NewPatient.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_NewPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_NewPatient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_NewPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_NewPatient.Image = CType(resources.GetObject("tlbbtn_NewPatient.Image"), System.Drawing.Image)
        Me.tlbbtn_NewPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_NewPatient.Name = "tlbbtn_NewPatient"
        Me.tlbbtn_NewPatient.Size = New System.Drawing.Size(56, 59)
        Me.tlbbtn_NewPatient.Tag = "NewPatient"
        Me.tlbbtn_NewPatient.Text = "New Pat"
        Me.tlbbtn_NewPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_NewPatient.ToolTipText = "Add New Patient"
        '
        'tlbbtn_ModifyPatient
        '
        Me.tlbbtn_ModifyPatient.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ModifyPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ModifyPatient.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_ModifyPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_ModifyPatient.Image = CType(resources.GetObject("tlbbtn_ModifyPatient.Image"), System.Drawing.Image)
        Me.tlbbtn_ModifyPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ModifyPatient.Name = "tlbbtn_ModifyPatient"
        Me.tlbbtn_ModifyPatient.Size = New System.Drawing.Size(57, 59)
        Me.tlbbtn_ModifyPatient.Tag = "ModifyPatient"
        Me.tlbbtn_ModifyPatient.Text = "Mod Pat"
        Me.tlbbtn_ModifyPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ModifyPatient.ToolTipText = "Modify Patient"
        '
        'tlbbtn_ScanCard
        '
        Me.tlbbtn_ScanCard.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ScanCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_ScanCard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_ScanCard.Image = CType(resources.GetObject("tlbbtn_ScanCard.Image"), System.Drawing.Image)
        Me.tlbbtn_ScanCard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ScanCard.Name = "tlbbtn_ScanCard"
        Me.tlbbtn_ScanCard.Size = New System.Drawing.Size(67, 59)
        Me.tlbbtn_ScanCard.Tag = "ScanCard"
        Me.tlbbtn_ScanCard.Text = "Scan Card"
        Me.tlbbtn_ScanCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ScanCard.ToolTipText = "Form Gallery"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_History
        '
        Me.tlbbtn_History.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_History.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_History.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_History.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_History.Image = CType(resources.GetObject("tlbbtn_History.Image"), System.Drawing.Image)
        Me.tlbbtn_History.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_History.Name = "tlbbtn_History"
        Me.tlbbtn_History.Size = New System.Drawing.Size(52, 59)
        Me.tlbbtn_History.Tag = "History"
        Me.tlbbtn_History.Text = "History"
        Me.tlbbtn_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Prescription
        '
        Me.tlbbtn_Prescription.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Prescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Prescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Prescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Prescription.Image = CType(resources.GetObject("tlbbtn_Prescription.Image"), System.Drawing.Image)
        Me.tlbbtn_Prescription.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Prescription.Name = "tlbbtn_Prescription"
        Me.tlbbtn_Prescription.Size = New System.Drawing.Size(50, 59)
        Me.tlbbtn_Prescription.Tag = "Prescription"
        Me.tlbbtn_Prescription.Text = "    Rx    "
        Me.tlbbtn_Prescription.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Prescription.ToolTipText = "Prescription"
        '
        'tlbbtn_Medication
        '
        Me.tlbbtn_Medication.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Medication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Medication.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Medication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Medication.Image = CType(resources.GetObject("tlbbtn_Medication.Image"), System.Drawing.Image)
        Me.tlbbtn_Medication.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Medication.Name = "tlbbtn_Medication"
        Me.tlbbtn_Medication.Size = New System.Drawing.Size(47, 59)
        Me.tlbbtn_Medication.Tag = "Medication"
        Me.tlbbtn_Medication.Text = " Meds "
        Me.tlbbtn_Medication.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Medication.ToolTipText = "Medication"
        '
        'tlbbtn_Orders
        '
        Me.tlbbtn_Orders.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Orders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Orders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Orders.Image = CType(resources.GetObject("tlbbtn_Orders.Image"), System.Drawing.Image)
        Me.tlbbtn_Orders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Orders.Name = "tlbbtn_Orders"
        Me.tlbbtn_Orders.Size = New System.Drawing.Size(49, 59)
        Me.tlbbtn_Orders.Tag = "Orders"
        Me.tlbbtn_Orders.Text = "Radios"
        Me.tlbbtn_Orders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Orders.ToolTipText = "Radiology Orders"
        '
        'tlbbtn_LabOrders
        '
        Me.tlbbtn_LabOrders.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_LabOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_LabOrders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_LabOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_LabOrders.Image = CType(resources.GetObject("tlbbtn_LabOrders.Image"), System.Drawing.Image)
        Me.tlbbtn_LabOrders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_LabOrders.Name = "tlbbtn_LabOrders"
        Me.tlbbtn_LabOrders.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_LabOrders.Tag = "LabsOrders"
        Me.tlbbtn_LabOrders.Text = " Labs "
        Me.tlbbtn_LabOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_LabOrders.ToolTipText = " Lab Orders"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_NewExam
        '
        Me.tlbbtn_NewExam.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_NewExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_NewExam.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_NewExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_NewExam.Image = CType(resources.GetObject("tlbbtn_NewExam.Image"), System.Drawing.Image)
        Me.tlbbtn_NewExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_NewExam.Name = "tlbbtn_NewExam"
        Me.tlbbtn_NewExam.Size = New System.Drawing.Size(68, 59)
        Me.tlbbtn_NewExam.Tag = "NewExam"
        Me.tlbbtn_NewExam.Text = "New Exam"
        Me.tlbbtn_NewExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_PastExam
        '
        Me.tlbbtn_PastExam.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_PastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_PastExam.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_PastExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_PastExam.Image = CType(resources.GetObject("tlbbtn_PastExam.Image"), System.Drawing.Image)
        Me.tlbbtn_PastExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_PastExam.Name = "tlbbtn_PastExam"
        Me.tlbbtn_PastExam.Size = New System.Drawing.Size(70, 59)
        Me.tlbbtn_PastExam.Tag = "PastExams"
        Me.tlbbtn_PastExam.Text = "Past Exam"
        Me.tlbbtn_PastExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_UnFinishedExams
        '
        Me.tlbbtn_UnFinishedExams.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_UnFinishedExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_UnFinishedExams.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_UnFinishedExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_UnFinishedExams.Image = CType(resources.GetObject("tlbbtn_UnFinishedExams.Image"), System.Drawing.Image)
        Me.tlbbtn_UnFinishedExams.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_UnFinishedExams.Name = "tlbbtn_UnFinishedExams"
        Me.tlbbtn_UnFinishedExams.Size = New System.Drawing.Size(70, 59)
        Me.tlbbtn_UnFinishedExams.Tag = "UnFinishedExams"
        Me.tlbbtn_UnFinishedExams.Text = "Unfinished"
        Me.tlbbtn_UnFinishedExams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_UnFinishedExams.ToolTipText = "Unfinished Exams"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_Calender
        '
        Me.tlbbtn_Calender.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Calender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Calender.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Calender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Calender.Image = CType(resources.GetObject("tlbbtn_Calender.Image"), System.Drawing.Image)
        Me.tlbbtn_Calender.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Calender.Name = "tlbbtn_Calender"
        Me.tlbbtn_Calender.Size = New System.Drawing.Size(61, 59)
        Me.tlbbtn_Calender.Tag = "Calender"
        Me.tlbbtn_Calender.Text = "Calendar"
        Me.tlbbtn_Calender.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_ScanDocs
        '
        Me.tlbbtn_ScanDocs.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ScanDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ScanDocs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_ScanDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_ScanDocs.Image = CType(resources.GetObject("tlbbtn_ScanDocs.Image"), System.Drawing.Image)
        Me.tlbbtn_ScanDocs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ScanDocs.Name = "tlbbtn_ScanDocs"
        Me.tlbbtn_ScanDocs.Size = New System.Drawing.Size(68, 59)
        Me.tlbbtn_ScanDocs.Tag = "ScanDocs"
        Me.tlbbtn_ScanDocs.Text = "Scan Docs"
        Me.tlbbtn_ScanDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ScanDocs.ToolTipText = "Scan Documents"
        '
        'tlbbtn_DocMGMT
        '
        Me.tlbbtn_DocMGMT.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_DocMGMT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_DocMGMT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_DocMGMT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_DocMGMT.Image = CType(resources.GetObject("tlbbtn_DocMGMT.Image"), System.Drawing.Image)
        Me.tlbbtn_DocMGMT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_DocMGMT.Name = "tlbbtn_DocMGMT"
        Me.tlbbtn_DocMGMT.Size = New System.Drawing.Size(57, 59)
        Me.tlbbtn_DocMGMT.Tag = "DOCMGMT"
        Me.tlbbtn_DocMGMT.Text = "Vw Docs"
        Me.tlbbtn_DocMGMT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_DocMGMT.ToolTipText = "View Documents"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_Billing
        '
        Me.tlbbtn_Billing.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Billing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Billing.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Billing.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Billing.Image = CType(resources.GetObject("tlbbtn_Billing.Image"), System.Drawing.Image)
        Me.tlbbtn_Billing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Billing.Name = "tlbbtn_Billing"
        Me.tlbbtn_Billing.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_Billing.Tag = "Billing"
        Me.tlbbtn_Billing.Text = "Billing"
        Me.tlbbtn_Billing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Billing.ToolTipText = "Form Gallery"
        '
        'tlbbtn_Payment
        '
        Me.tlbbtn_Payment.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Payment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Payment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Payment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Payment.Image = CType(resources.GetObject("tlbbtn_Payment.Image"), System.Drawing.Image)
        Me.tlbbtn_Payment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Payment.Name = "tlbbtn_Payment"
        Me.tlbbtn_Payment.Size = New System.Drawing.Size(62, 59)
        Me.tlbbtn_Payment.Tag = "Payment"
        Me.tlbbtn_Payment.Text = "Payment"
        Me.tlbbtn_Payment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Payment.ToolTipText = "Form Gallery"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_FormGallery
        '
        Me.tlbbtn_FormGallery.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_FormGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_FormGallery.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_FormGallery.Image = CType(resources.GetObject("tlbbtn_FormGallery.Image"), System.Drawing.Image)
        Me.tlbbtn_FormGallery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_FormGallery.Name = "tlbbtn_FormGallery"
        Me.tlbbtn_FormGallery.Size = New System.Drawing.Size(73, 59)
        Me.tlbbtn_FormGallery.Tag = "FormGallery"
        Me.tlbbtn_FormGallery.Text = "Form Galry"
        Me.tlbbtn_FormGallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_FormGallery.ToolTipText = "Form Gallery"
        '
        'tlbbtn_LockScreen
        '
        Me.tlbbtn_LockScreen.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_LockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_LockScreen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_LockScreen.Image = CType(resources.GetObject("tlbbtn_LockScreen.Image"), System.Drawing.Image)
        Me.tlbbtn_LockScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_LockScreen.Name = "tlbbtn_LockScreen"
        Me.tlbbtn_LockScreen.Size = New System.Drawing.Size(79, 59)
        Me.tlbbtn_LockScreen.Tag = "LockScreen"
        Me.tlbbtn_LockScreen.Text = "Lock Screen"
        Me.tlbbtn_LockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_LockScreen.ToolTipText = "Log Out"
        Me.tlbbtn_LockScreen.Visible = False
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(6, 62)
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(46, 59)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "Exit"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Close.ToolTipText = "Exit Application"
        '
        'AxFramerControl1
        '
        Me.AxFramerControl1.Enabled = True
        Me.AxFramerControl1.Location = New System.Drawing.Point(0, 0)
        Me.AxFramerControl1.Name = "AxFramerControl1"
        Me.AxFramerControl1.TabIndex = 0
        '
        'AxDgnEngineControl1
        '
        Me.AxDgnEngineControl1.Enabled = True
        Me.AxDgnEngineControl1.Location = New System.Drawing.Point(0, 0)
        Me.AxDgnEngineControl1.Name = "AxDgnEngineControl1"
        Me.AxDgnEngineControl1.OcxState = CType(resources.GetObject("AxDgnEngineControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxDgnEngineControl1.Size = New System.Drawing.Size(16, 15)
        Me.AxDgnEngineControl1.TabIndex = 55
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(902, 456)
        Me.Controls.Add(Me.AxDgnEngineControl1)
        Me.Controls.Add(Me.tlbStripMain)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.AxFramerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxDgnEngineControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlbStripMain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Masters As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlbbtn_NewPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_ModifyPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_ScanCard As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_History As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Prescription As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Medication As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Orders As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_LabOrders As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_NewExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_PastExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_UnFinishedExams As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_Calender As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_ScanDocs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_DocMGMT As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_Billing As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Payment As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_FormGallery As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_LockScreen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents AxFramerControl1 As AxDSOFramer.AxFramerControl
    Friend WithEvents AxDgnEngineControl1 As AxDNSTools.AxDgnEngineControl
    'Friend WithEvents AxGrowthChart1 As AxGROWTHCHARTLib.AxGrowthChart
    'Friend WithEvents AxezDICOMX1 As AxezDICOMax.AxezDICOMX
    'Friend WithEvents AxMSChart1 As AxMSChart20Lib.AxMSChart

End Class
