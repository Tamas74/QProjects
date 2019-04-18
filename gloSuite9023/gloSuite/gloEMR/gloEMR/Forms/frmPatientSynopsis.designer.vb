<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientSynopsis
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
                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                       
                    Catch ex As Exception

                    End Try
                End If
              
                If (IsNothing(_PatientSynopsisUC) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(_PatientSynopsisUC)
                       
                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    GloUC_TransactionHistory1.Dispose()
                    GloUC_TransactionHistory1 = Nothing
                End If
                'If (IsNothing(_PatientSynopsisUC) = False) Then
                '    _PatientSynopsisUC.Dispose()
                '    _PatientSynopsisUC = Nothing
                'End If
               
                Dim dtpContextMenustrip As ContextMenuStrip() = {cntPatient}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenustrip)
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpContextMenustrip)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientSynopsis))
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New gloToolStrip.gloToolStrip()
        Me.tblbtn_SelectPatient = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_StressTest = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_ElectroPhysio = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_CardioDevice = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_EjectionFraction = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Intervention = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Risk = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnCheckedInPatients = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnNext = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnEchocard = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Catheterization = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_ECG = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Spiro = New System.Windows.Forms.ToolStripButton()
        Me.pnlPatientListView = New System.Windows.Forms.Panel()
        Me.pnlPatientStrip = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlPatientTab = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cntPatient = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteTab = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlPatientStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.cntPatient.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(1284, 55)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.AddSeparatorsBetweenEachButton = False
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.ButtonsToHide = CType(resources.GetObject("tstrip.ButtonsToHide"), System.Collections.ArrayList)
        Me.tstrip.ConnectionString = Nothing
        Me.tstrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_SelectPatient, Me.tblbtn_StressTest, Me.tblbtn_ElectroPhysio, Me.tblbtn_CardioDevice, Me.tblbtn_EjectionFraction, Me.tblbtn_Intervention, Me.tblbtn_Risk, Me.tlsbtnCheckedInPatients, Me.tlsbtnPrevious, Me.tlsbtnNext, Me.tlsbtnClose, Me.tlsbtnEchocard, Me.tblbtn_Catheterization, Me.tblbtn_ECG, Me.tblbtn_Spiro})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.ModuleName = Nothing
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(1284, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        Me.tstrip.UserID = CType(0, Long)
        '
        'tblbtn_SelectPatient
        '
        Me.tblbtn_SelectPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_SelectPatient.Image = CType(resources.GetObject("tblbtn_SelectPatient.Image"), System.Drawing.Image)
        Me.tblbtn_SelectPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_SelectPatient.Name = "tblbtn_SelectPatient"
        Me.tblbtn_SelectPatient.Size = New System.Drawing.Size(97, 50)
        Me.tblbtn_SelectPatient.Tag = "Select Patient"
        Me.tblbtn_SelectPatient.Text = "&Select Patient"
        Me.tblbtn_SelectPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_SelectPatient.ToolTipText = "Select Patient"
        '
        'tblbtn_StressTest
        '
        Me.tblbtn_StressTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_StressTest.Image = CType(resources.GetObject("tblbtn_StressTest.Image"), System.Drawing.Image)
        Me.tblbtn_StressTest.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_StressTest.Name = "tblbtn_StressTest"
        Me.tblbtn_StressTest.Size = New System.Drawing.Size(79, 50)
        Me.tblbtn_StressTest.Tag = "Stress Test"
        Me.tblbtn_StressTest.Text = "S&tress Test"
        Me.tblbtn_StressTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_ElectroPhysio
        '
        Me.tblbtn_ElectroPhysio.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_ElectroPhysio.Image = CType(resources.GetObject("tblbtn_ElectroPhysio.Image"), System.Drawing.Image)
        Me.tblbtn_ElectroPhysio.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ElectroPhysio.Name = "tblbtn_ElectroPhysio"
        Me.tblbtn_ElectroPhysio.Size = New System.Drawing.Size(119, 50)
        Me.tblbtn_ElectroPhysio.Tag = "Electro Physiology"
        Me.tblbtn_ElectroPhysio.Text = "&Electrophysiology"
        Me.tblbtn_ElectroPhysio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_ElectroPhysio.ToolTipText = "Electrophysiology"
        '
        'tblbtn_CardioDevice
        '
        Me.tblbtn_CardioDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_CardioDevice.Image = CType(resources.GetObject("tblbtn_CardioDevice.Image"), System.Drawing.Image)
        Me.tblbtn_CardioDevice.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_CardioDevice.Name = "tblbtn_CardioDevice"
        Me.tblbtn_CardioDevice.Size = New System.Drawing.Size(102, 50)
        Me.tblbtn_CardioDevice.Tag = "Implant Device"
        Me.tblbtn_CardioDevice.Text = "Implant &Device"
        Me.tblbtn_CardioDevice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_EjectionFraction
        '
        Me.tblbtn_EjectionFraction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_EjectionFraction.Image = CType(resources.GetObject("tblbtn_EjectionFraction.Image"), System.Drawing.Image)
        Me.tblbtn_EjectionFraction.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_EjectionFraction.Name = "tblbtn_EjectionFraction"
        Me.tblbtn_EjectionFraction.Size = New System.Drawing.Size(113, 50)
        Me.tblbtn_EjectionFraction.Tag = "Ejection Fraction"
        Me.tblbtn_EjectionFraction.Text = "Ejection &Fraction"
        Me.tblbtn_EjectionFraction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Intervention
        '
        Me.tblbtn_Intervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Intervention.Image = CType(resources.GetObject("tblbtn_Intervention.Image"), System.Drawing.Image)
        Me.tblbtn_Intervention.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Intervention.Name = "tblbtn_Intervention"
        Me.tblbtn_Intervention.Size = New System.Drawing.Size(89, 50)
        Me.tblbtn_Intervention.Tag = "Intervention"
        Me.tblbtn_Intervention.Text = "&Intervention"
        Me.tblbtn_Intervention.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Risk
        '
        Me.tblbtn_Risk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Risk.Image = CType(resources.GetObject("tblbtn_Risk.Image"), System.Drawing.Image)
        Me.tblbtn_Risk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Risk.Name = "tblbtn_Risk"
        Me.tblbtn_Risk.Size = New System.Drawing.Size(56, 50)
        Me.tblbtn_Risk.Tag = "Risk"
        Me.tblbtn_Risk.Text = "C&V Risk"
        Me.tblbtn_Risk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Risk.ToolTipText = "Patient Risk"
        '
        'tlsbtnCheckedInPatients
        '
        Me.tlsbtnCheckedInPatients.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnCheckedInPatients.Image = CType(resources.GetObject("tlsbtnCheckedInPatients.Image"), System.Drawing.Image)
        Me.tlsbtnCheckedInPatients.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCheckedInPatients.Name = "tlsbtnCheckedInPatients"
        Me.tlsbtnCheckedInPatients.Size = New System.Drawing.Size(134, 50)
        Me.tlsbtnCheckedInPatients.Tag = "Check In Patient"
        Me.tlsbtnCheckedInPatients.Text = "C&hecked In Patients"
        Me.tlsbtnCheckedInPatients.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnCheckedInPatients.ToolTipText = "Checked In Patients"
        '
        'tlsbtnPrevious
        '
        Me.tlsbtnPrevious.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnPrevious.Image = CType(resources.GetObject("tlsbtnPrevious.Image"), System.Drawing.Image)
        Me.tlsbtnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnPrevious.Name = "tlsbtnPrevious"
        Me.tlsbtnPrevious.Size = New System.Drawing.Size(63, 50)
        Me.tlsbtnPrevious.Tag = "Previous"
        Me.tlsbtnPrevious.Text = "&Previous"
        Me.tlsbtnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnPrevious.ToolTipText = "Previous"
        '
        'tlsbtnNext
        '
        Me.tlsbtnNext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnNext.Image = CType(resources.GetObject("tlsbtnNext.Image"), System.Drawing.Image)
        Me.tlsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnNext.Name = "tlsbtnNext"
        Me.tlsbtnNext.Size = New System.Drawing.Size(39, 50)
        Me.tlsbtnNext.Tag = "Next"
        Me.tlsbtnNext.Text = "&Next"
        Me.tlsbtnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnNext.ToolTipText = "Next"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.BackColor = System.Drawing.Color.Transparent
        Me.tlsbtnClose.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Tag = "Close"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'tlsbtnEchocard
        '
        Me.tlsbtnEchocard.Image = CType(resources.GetObject("tlsbtnEchocard.Image"), System.Drawing.Image)
        Me.tlsbtnEchocard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnEchocard.Name = "tlsbtnEchocard"
        Me.tlsbtnEchocard.Size = New System.Drawing.Size(108, 50)
        Me.tlsbtnEchocard.Tag = "EchoCardio"
        Me.tlsbtnEchocard.Text = "Echoca&rdiogram"
        Me.tlsbtnEchocard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Catheterization
        '
        Me.tblbtn_Catheterization.Image = CType(resources.GetObject("tblbtn_Catheterization.Image"), System.Drawing.Image)
        Me.tblbtn_Catheterization.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Catheterization.Name = "tblbtn_Catheterization"
        Me.tblbtn_Catheterization.Size = New System.Drawing.Size(106, 50)
        Me.tblbtn_Catheterization.Tag = "Catheterization"
        Me.tblbtn_Catheterization.Text = "Catheteri&zation"
        Me.tblbtn_Catheterization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_ECG
        '
        Me.tblbtn_ECG.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_ECG.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblbtn_ECG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_ECG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_ECG.Image = CType(resources.GetObject("tblbtn_ECG.Image"), System.Drawing.Image)
        Me.tblbtn_ECG.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ECG.Name = "tblbtn_ECG"
        Me.tblbtn_ECG.Size = New System.Drawing.Size(127, 50)
        Me.tblbtn_ECG.Tag = "ECG"
        Me.tblbtn_ECG.Text = "Electrocardiogra&ms"
        Me.tblbtn_ECG.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_ECG.Visible = False
        '
        'tblbtn_Spiro
        '
        Me.tblbtn_Spiro.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Spiro.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblbtn_Spiro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Spiro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Spiro.Image = CType(resources.GetObject("tblbtn_Spiro.Image"), System.Drawing.Image)
        Me.tblbtn_Spiro.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Spiro.Name = "tblbtn_Spiro"
        Me.tblbtn_Spiro.Size = New System.Drawing.Size(79, 50)
        Me.tblbtn_Spiro.Tag = "Spirometry"
        Me.tblbtn_Spiro.Text = "Spir&ometry"
        Me.tblbtn_Spiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Spiro.Visible = False
        '
        'pnlPatientListView
        '
        Me.pnlPatientListView.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientListView.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientListView.Name = "pnlPatientListView"
        Me.pnlPatientListView.Size = New System.Drawing.Size(1284, 204)
        Me.pnlPatientListView.TabIndex = 1
        '
        'pnlPatientStrip
        '
        Me.pnlPatientStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientStrip.Controls.Add(Me.Label1)
        Me.pnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientStrip.Location = New System.Drawing.Point(0, 207)
        Me.pnlPatientStrip.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlPatientStrip.Name = "pnlPatientStrip"
        Me.pnlPatientStrip.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientStrip.Size = New System.Drawing.Size(1284, 26)
        Me.pnlPatientStrip.TabIndex = 21
        Me.pnlPatientStrip.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "pnlPatientStrip"
        '
        'pnlPatientTab
        '
        Me.pnlPatientTab.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientTab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientTab.Location = New System.Drawing.Point(0, 233)
        Me.pnlPatientTab.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlPatientTab.Name = "pnlPatientTab"
        Me.pnlPatientTab.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPatientTab.Size = New System.Drawing.Size(1284, 714)
        Me.pnlPatientTab.TabIndex = 22
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 204)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1284, 3)
        Me.Splitter1.TabIndex = 23
        Me.Splitter1.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.Controls.Add(Me.pnlPatientTab)
        Me.pnlMain.Controls.Add(Me.pnlPatientStrip)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlPatientListView)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 55)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1284, 947)
        Me.pnlMain.TabIndex = 0
        '
        'cntPatient
        '
        Me.cntPatient.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteTab})
        Me.cntPatient.Name = "cntPatient"
        Me.cntPatient.Size = New System.Drawing.Size(133, 26)
        Me.cntPatient.Text = "Remove Patient"
        '
        'mnuDeleteTab
        '
        Me.mnuDeleteTab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteTab.Name = "mnuDeleteTab"
        Me.mnuDeleteTab.Size = New System.Drawing.Size(132, 22)
        Me.mnuDeleteTab.Text = "Close Tab"
        '
        'frmPatientSynopsis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 1002)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientSynopsis"
        Me.Text = "Patient Synopsis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlPatientStrip.ResumeLayout(False)
        Me.pnlPatientStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.cntPatient.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    'Friend WithEvents tstrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tstrip As gloToolStrip.gloToolStrip
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlPatientStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientTab As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientListView As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cntPatient As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tblbtn_StressTest As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_ElectroPhysio As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_CardioDevice As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_EjectionFraction As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_SelectPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Intervention As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Risk As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnCheckedInPatients As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnEchocard As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Catheterization As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_ECG As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Spiro As System.Windows.Forms.ToolStripButton

End Class
