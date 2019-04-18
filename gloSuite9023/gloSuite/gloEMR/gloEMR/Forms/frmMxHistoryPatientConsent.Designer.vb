<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMxHistoryPatientConsent
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtTo) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtTo)
                        Catch ex As Exception

                        End Try


                        dtTo.Dispose()
                        dtTo = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtFrom) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFrom)
                        Catch ex As Exception

                        End Try


                        dtFrom.Dispose()
                        dtFrom = Nothing
                    End If
                Catch
                End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMxHistoryPatientConsent))
        Me.tlsp_LabResultGraph = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOK = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblEnddate = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.grpBxConsentFlags = New System.Windows.Forms.GroupBox()
        Me.rbtnForPrescriber = New System.Windows.Forms.RadioButton()
        Me.rbtnPrescriber = New System.Windows.Forms.RadioButton()
        Me.rbtnFromAnyPrescriber = New System.Windows.Forms.RadioButton()
        Me.rbtnNoConsent = New System.Windows.Forms.RadioButton()
        Me.rbtnConsentgiven = New System.Windows.Forms.RadioButton()
        Me.pnlLevel3 = New System.Windows.Forms.Panel()
        Me.pnlLevel1 = New System.Windows.Forms.Panel()
        Me.rbtnLevel3 = New System.Windows.Forms.RadioButton()
        Me.rbtnLevel2 = New System.Windows.Forms.RadioButton()
        Me.rbtnLevel1 = New System.Windows.Forms.RadioButton()
        Me.pnlLevel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPBM = New System.Windows.Forms.ComboBox()
        Me.rdbtn_SelectedPBM = New System.Windows.Forms.RadioButton()
        Me.rdbtn_AllPBM = New System.Windows.Forms.RadioButton()
        Me.grpRxhubDisclaimer = New System.Windows.Forms.GroupBox()
        Me.txtRxHubDisclaimer = New System.Windows.Forms.TextBox()
        Me.lblRxHubDisClaimer = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tlsp_LabResultGraph.SuspendLayout()
        Me.grpBxConsentFlags.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpRxhubDisclaimer.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlsp_LabResultGraph
        '
        Me.tlsp_LabResultGraph.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_LabResultGraph.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_LabResultGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_LabResultGraph.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_LabResultGraph.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_LabResultGraph.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOK, Me.ts_btnClose})
        Me.tlsp_LabResultGraph.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_LabResultGraph.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_LabResultGraph.Name = "tlsp_LabResultGraph"
        Me.tlsp_LabResultGraph.Size = New System.Drawing.Size(590, 53)
        Me.tlsp_LabResultGraph.TabIndex = 8
        Me.tlsp_LabResultGraph.Text = "toolStrip1"
        '
        'ts_btnOK
        '
        Me.ts_btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOK.Image = CType(resources.GetObject("ts_btnOK.Image"), System.Drawing.Image)
        Me.ts_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOK.Name = "ts_btnOK"
        Me.ts_btnOK.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOK.Tag = "OK"
        Me.ts_btnOK.Text = "&Save&&Cls"
        Me.ts_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOK.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(468, 143)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(114, 22)
        Me.dtTo.TabIndex = 1
        Me.dtTo.Visible = False
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(474, 109)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(112, 22)
        Me.dtFrom.TabIndex = 0
        Me.dtFrom.Value = New Date(2009, 5, 11, 0, 0, 0, 0)
        Me.dtFrom.Visible = False
        '
        'lblEnddate
        '
        Me.lblEnddate.AutoSize = True
        Me.lblEnddate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnddate.Location = New System.Drawing.Point(401, 147)
        Me.lblEnddate.Name = "lblEnddate"
        Me.lblEnddate.Size = New System.Drawing.Size(66, 14)
        Me.lblEnddate.TabIndex = 10
        Me.lblEnddate.Text = "End Date :"
        Me.lblEnddate.Visible = False
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(401, 113)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(72, 14)
        Me.lblStartDate.TabIndex = 9
        Me.lblStartDate.Text = "Start Date :"
        Me.lblStartDate.Visible = False
        '
        'grpBxConsentFlags
        '
        Me.grpBxConsentFlags.Controls.Add(Me.rbtnForPrescriber)
        Me.grpBxConsentFlags.Controls.Add(Me.rbtnPrescriber)
        Me.grpBxConsentFlags.Controls.Add(Me.rbtnFromAnyPrescriber)
        Me.grpBxConsentFlags.Controls.Add(Me.rbtnNoConsent)
        Me.grpBxConsentFlags.Controls.Add(Me.rbtnConsentgiven)
        Me.grpBxConsentFlags.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBxConsentFlags.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpBxConsentFlags.Location = New System.Drawing.Point(12, 13)
        Me.grpBxConsentFlags.Name = "grpBxConsentFlags"
        Me.grpBxConsentFlags.Size = New System.Drawing.Size(386, 180)
        Me.grpBxConsentFlags.TabIndex = 13
        Me.grpBxConsentFlags.TabStop = False
        Me.grpBxConsentFlags.Text = "Patient Consent"
        '
        'rbtnForPrescriber
        '
        Me.rbtnForPrescriber.AutoSize = True
        Me.rbtnForPrescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnForPrescriber.Location = New System.Drawing.Point(27, 148)
        Me.rbtnForPrescriber.Name = "rbtnForPrescriber"
        Me.rbtnForPrescriber.Size = New System.Drawing.Size(335, 18)
        Me.rbtnForPrescriber.TabIndex = 4
        Me.rbtnForPrescriber.TabStop = True
        Me.rbtnForPrescriber.Tag = "Y"
        Me.rbtnForPrescriber.Text = "Received Medication history for this prescriber prescribed"
        Me.rbtnForPrescriber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtnForPrescriber.UseVisualStyleBackColor = True
        '
        'rbtnPrescriber
        '
        Me.rbtnPrescriber.AutoSize = True
        Me.rbtnPrescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnPrescriber.Location = New System.Drawing.Point(27, 88)
        Me.rbtnPrescriber.Name = "rbtnPrescriber"
        Me.rbtnPrescriber.Size = New System.Drawing.Size(78, 18)
        Me.rbtnPrescriber.TabIndex = 4
        Me.rbtnPrescriber.TabStop = True
        Me.rbtnPrescriber.Tag = "Y"
        Me.rbtnPrescriber.Text = "Prescriber"
        Me.rbtnPrescriber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtnPrescriber.UseVisualStyleBackColor = True
        '
        'rbtnFromAnyPrescriber
        '
        Me.rbtnFromAnyPrescriber.AutoSize = True
        Me.rbtnFromAnyPrescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnFromAnyPrescriber.Location = New System.Drawing.Point(27, 118)
        Me.rbtnFromAnyPrescriber.Name = "rbtnFromAnyPrescriber"
        Me.rbtnFromAnyPrescriber.Size = New System.Drawing.Size(286, 18)
        Me.rbtnFromAnyPrescriber.TabIndex = 3
        Me.rbtnFromAnyPrescriber.TabStop = True
        Me.rbtnFromAnyPrescriber.Tag = "Y"
        Me.rbtnFromAnyPrescriber.Text = "Received medication history from any prescriber"
        Me.rbtnFromAnyPrescriber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtnFromAnyPrescriber.UseVisualStyleBackColor = True
        '
        'rbtnNoConsent
        '
        Me.rbtnNoConsent.AutoSize = True
        Me.rbtnNoConsent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnNoConsent.Location = New System.Drawing.Point(27, 58)
        Me.rbtnNoConsent.Name = "rbtnNoConsent"
        Me.rbtnNoConsent.Size = New System.Drawing.Size(93, 18)
        Me.rbtnNoConsent.TabIndex = 3
        Me.rbtnNoConsent.TabStop = True
        Me.rbtnNoConsent.Tag = "Y"
        Me.rbtnNoConsent.Text = "No Consent "
        Me.rbtnNoConsent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtnNoConsent.UseVisualStyleBackColor = True
        '
        'rbtnConsentgiven
        '
        Me.rbtnConsentgiven.AutoSize = True
        Me.rbtnConsentgiven.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnConsentgiven.Location = New System.Drawing.Point(27, 28)
        Me.rbtnConsentgiven.Name = "rbtnConsentgiven"
        Me.rbtnConsentgiven.Size = New System.Drawing.Size(103, 18)
        Me.rbtnConsentgiven.TabIndex = 2
        Me.rbtnConsentgiven.TabStop = True
        Me.rbtnConsentgiven.Tag = "Y"
        Me.rbtnConsentgiven.Text = "Consent given"
        Me.rbtnConsentgiven.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtnConsentgiven.UseVisualStyleBackColor = True
        '
        'pnlLevel3
        '
        Me.pnlLevel3.Location = New System.Drawing.Point(202, 512)
        Me.pnlLevel3.Name = "pnlLevel3"
        Me.pnlLevel3.Size = New System.Drawing.Size(167, 24)
        Me.pnlLevel3.TabIndex = 9
        Me.pnlLevel3.Visible = False
        '
        'pnlLevel1
        '
        Me.pnlLevel1.Location = New System.Drawing.Point(202, 442)
        Me.pnlLevel1.Name = "pnlLevel1"
        Me.pnlLevel1.Size = New System.Drawing.Size(169, 22)
        Me.pnlLevel1.TabIndex = 8
        Me.pnlLevel1.Visible = False
        '
        'rbtnLevel3
        '
        Me.rbtnLevel3.AutoSize = True
        Me.rbtnLevel3.Location = New System.Drawing.Point(46, 515)
        Me.rbtnLevel3.Name = "rbtnLevel3"
        Me.rbtnLevel3.Size = New System.Drawing.Size(109, 18)
        Me.rbtnLevel3.TabIndex = 7
        Me.rbtnLevel3.Text = "Physician Only :"
        Me.rbtnLevel3.UseVisualStyleBackColor = True
        Me.rbtnLevel3.Visible = False
        '
        'rbtnLevel2
        '
        Me.rbtnLevel2.AutoSize = True
        Me.rbtnLevel2.Location = New System.Drawing.Point(46, 476)
        Me.rbtnLevel2.Name = "rbtnLevel2"
        Me.rbtnLevel2.Size = New System.Drawing.Size(83, 18)
        Me.rbtnLevel2.TabIndex = 6
        Me.rbtnLevel2.Text = "No Drugs :"
        Me.rbtnLevel2.UseVisualStyleBackColor = True
        Me.rbtnLevel2.Visible = False
        '
        'rbtnLevel1
        '
        Me.rbtnLevel1.AutoSize = True
        Me.rbtnLevel1.Location = New System.Drawing.Point(46, 442)
        Me.rbtnLevel1.Name = "rbtnLevel1"
        Me.rbtnLevel1.Size = New System.Drawing.Size(129, 18)
        Me.rbtnLevel1.TabIndex = 5
        Me.rbtnLevel1.Text = "Parental/Guardian :"
        Me.rbtnLevel1.UseVisualStyleBackColor = True
        Me.rbtnLevel1.Visible = False
        '
        'pnlLevel2
        '
        Me.pnlLevel2.Location = New System.Drawing.Point(202, 476)
        Me.pnlLevel2.Name = "pnlLevel2"
        Me.pnlLevel2.Size = New System.Drawing.Size(63, 23)
        Me.pnlLevel2.TabIndex = 9
        Me.pnlLevel2.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.grpRxhubDisclaimer)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.dtFrom)
        Me.Panel1.Controls.Add(Me.lblStartDate)
        Me.Panel1.Controls.Add(Me.lblEnddate)
        Me.Panel1.Controls.Add(Me.dtTo)
        Me.Panel1.Controls.Add(Me.grpBxConsentFlags)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(590, 361)
        Me.Panel1.TabIndex = 14
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPBM)
        Me.GroupBox1.Controls.Add(Me.rdbtn_SelectedPBM)
        Me.GroupBox1.Controls.Add(Me.rdbtn_AllPBM)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(404, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(174, 84)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "PBM"
        Me.GroupBox1.Visible = False
        '
        'cmbPBM
        '
        Me.cmbPBM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPBM.FormattingEnabled = True
        Me.cmbPBM.Location = New System.Drawing.Point(85, 21)
        Me.cmbPBM.Name = "cmbPBM"
        Me.cmbPBM.Size = New System.Drawing.Size(78, 22)
        Me.cmbPBM.TabIndex = 4
        '
        'rdbtn_SelectedPBM
        '
        Me.rdbtn_SelectedPBM.AutoSize = True
        Me.rdbtn_SelectedPBM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_SelectedPBM.Location = New System.Drawing.Point(15, 55)
        Me.rdbtn_SelectedPBM.Name = "rdbtn_SelectedPBM"
        Me.rdbtn_SelectedPBM.Size = New System.Drawing.Size(100, 18)
        Me.rdbtn_SelectedPBM.TabIndex = 3
        Me.rdbtn_SelectedPBM.Tag = "X"
        Me.rdbtn_SelectedPBM.Text = "Selected PBM"
        Me.rdbtn_SelectedPBM.UseVisualStyleBackColor = True
        '
        'rdbtn_AllPBM
        '
        Me.rdbtn_AllPBM.AutoSize = True
        Me.rdbtn_AllPBM.Checked = True
        Me.rdbtn_AllPBM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_AllPBM.Location = New System.Drawing.Point(15, 24)
        Me.rdbtn_AllPBM.Name = "rdbtn_AllPBM"
        Me.rdbtn_AllPBM.Size = New System.Drawing.Size(64, 18)
        Me.rdbtn_AllPBM.TabIndex = 2
        Me.rdbtn_AllPBM.TabStop = True
        Me.rdbtn_AllPBM.Tag = "Y"
        Me.rdbtn_AllPBM.Text = "All PBM"
        Me.rdbtn_AllPBM.UseVisualStyleBackColor = True
        '
        'grpRxhubDisclaimer
        '
        Me.grpRxhubDisclaimer.BackColor = System.Drawing.Color.Transparent
        Me.grpRxhubDisclaimer.Controls.Add(Me.txtRxHubDisclaimer)
        Me.grpRxhubDisclaimer.Controls.Add(Me.lblRxHubDisClaimer)
        Me.grpRxhubDisclaimer.ForeColor = System.Drawing.Color.Black
        Me.grpRxhubDisclaimer.Location = New System.Drawing.Point(12, 197)
        Me.grpRxhubDisclaimer.Name = "grpRxhubDisclaimer"
        Me.grpRxhubDisclaimer.Size = New System.Drawing.Size(566, 152)
        Me.grpRxhubDisclaimer.TabIndex = 78
        Me.grpRxhubDisclaimer.TabStop = False
        '
        'txtRxHubDisclaimer
        '
        Me.txtRxHubDisclaimer.BackColor = System.Drawing.Color.White
        Me.txtRxHubDisclaimer.Location = New System.Drawing.Point(16, 37)
        Me.txtRxHubDisclaimer.MaxLength = 500
        Me.txtRxHubDisclaimer.Multiline = True
        Me.txtRxHubDisclaimer.Name = "txtRxHubDisclaimer"
        Me.txtRxHubDisclaimer.ReadOnly = True
        Me.txtRxHubDisclaimer.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRxHubDisclaimer.Size = New System.Drawing.Size(539, 102)
        Me.txtRxHubDisclaimer.TabIndex = 8
        '
        'lblRxHubDisClaimer
        '
        Me.lblRxHubDisClaimer.AutoSize = True
        Me.lblRxHubDisClaimer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRxHubDisClaimer.Location = New System.Drawing.Point(19, 14)
        Me.lblRxHubDisClaimer.Name = "lblRxHubDisClaimer"
        Me.lblRxHubDisClaimer.Size = New System.Drawing.Size(86, 14)
        Me.lblRxHubDisClaimer.TabIndex = 44
        Me.lblRxHubDisClaimer.Text = "Disclaimer :"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(586, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 353)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Parental/Guardian"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 353)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Parental/Guardian"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(3, 357)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(584, 1)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Parental/Guardian"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(584, 1)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Parental/Guardian"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tlsp_LabResultGraph)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(590, 55)
        Me.Panel2.TabIndex = 14
        '
        'frmMxHistoryPatientConsent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(590, 416)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlLevel2)
        Me.Controls.Add(Me.rbtnLevel3)
        Me.Controls.Add(Me.pnlLevel3)
        Me.Controls.Add(Me.pnlLevel1)
        Me.Controls.Add(Me.rbtnLevel2)
        Me.Controls.Add(Me.rbtnLevel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMxHistoryPatientConsent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consent"
        Me.tlsp_LabResultGraph.ResumeLayout(False)
        Me.tlsp_LabResultGraph.PerformLayout()
        Me.grpBxConsentFlags.ResumeLayout(False)
        Me.grpBxConsentFlags.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpRxhubDisclaimer.ResumeLayout(False)
        Me.grpRxhubDisclaimer.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tlsp_LabResultGraph As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEnddate As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents grpBxConsentFlags As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnConsentgiven As System.Windows.Forms.RadioButton
    Friend WithEvents pnlLevel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlLevel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnLevel3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnLevel2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnLevel1 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlLevel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpRxhubDisclaimer As System.Windows.Forms.GroupBox
    Friend WithEvents txtRxHubDisclaimer As System.Windows.Forms.TextBox
    Friend WithEvents lblRxHubDisClaimer As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbtn_SelectedPBM As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_AllPBM As System.Windows.Forms.RadioButton
    Friend WithEvents cmbPBM As System.Windows.Forms.ComboBox
    Friend WithEvents rbtnNoConsent As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnForPrescriber As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPrescriber As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnFromAnyPrescriber As System.Windows.Forms.RadioButton
End Class
