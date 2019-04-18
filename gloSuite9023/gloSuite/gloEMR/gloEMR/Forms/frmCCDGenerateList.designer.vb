<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDGenerateList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

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
                ' PrintDialog1 Clean up
                Try

                    If Not IsNothing(PrintDialog1) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If

                Catch
                End Try

                Try
                    If (IsNothing(dtpFrom) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFrom)
                        Catch ex As Exception

                        End Try


                        dtpFrom.Dispose()
                        dtpFrom = Nothing
                    End If
                Catch
                End Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
                Try
                    If (IsNothing(SaveFileDialog1) = False) Then
                        SaveFileDialog1.Dispose()
                        SaveFileDialog1 = Nothing
                    End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDGenerateList))
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlPrintMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ChkDemographics = New System.Windows.Forms.CheckBox()
        Me.ChkVitals = New System.Windows.Forms.CheckBox()
        Me.ChkFamilyHistory = New System.Windows.Forms.CheckBox()
        Me.ChkAll = New System.Windows.Forms.CheckBox()
        Me.ChkAdvanceDirectives = New System.Windows.Forms.CheckBox()
        Me.ChkResults = New System.Windows.Forms.CheckBox()
        Me.ChkImmunization = New System.Windows.Forms.CheckBox()
        Me.ChkProcedures = New System.Windows.Forms.CheckBox()
        Me.ChkMedications = New System.Windows.Forms.CheckBox()
        Me.ChkEncounter = New System.Windows.Forms.CheckBox()
        Me.ChkSocialHistory = New System.Windows.Forms.CheckBox()
        Me.ChkAllergy = New System.Windows.Forms.CheckBox()
        Me.chkProblems = New System.Windows.Forms.CheckBox()
        Me.chkPatientRequest = New System.Windows.Forms.CheckBox()
        Me.lblCCDMessage = New System.Windows.Forms.Label()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Email = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblShowCCD = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.tblCDA = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PnlMail = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.cmdSend = New System.Windows.Forms.ToolStripButton()
        Me.cmdCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAttachment = New System.Windows.Forms.TextBox()
        Me.txtMailTo = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtMailFrom = New System.Windows.Forms.TextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PnlMain.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlPrintMessage.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PnlMail.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlMain.Controls.Add(Me.Panel3)
        Me.PnlMain.Controls.Add(Me.Label14)
        Me.PnlMain.Controls.Add(Me.Label11)
        Me.PnlMain.Controls.Add(Me.Label12)
        Me.PnlMain.Controls.Add(Me.pnlPrintMessage)
        Me.PnlMain.Controls.Add(Me.Label13)
        Me.PnlMain.Controls.Add(Me.ChkDemographics)
        Me.PnlMain.Controls.Add(Me.ChkVitals)
        Me.PnlMain.Controls.Add(Me.ChkFamilyHistory)
        Me.PnlMain.Controls.Add(Me.ChkAll)
        Me.PnlMain.Controls.Add(Me.ChkAdvanceDirectives)
        Me.PnlMain.Controls.Add(Me.ChkResults)
        Me.PnlMain.Controls.Add(Me.ChkImmunization)
        Me.PnlMain.Controls.Add(Me.ChkProcedures)
        Me.PnlMain.Controls.Add(Me.ChkMedications)
        Me.PnlMain.Controls.Add(Me.ChkEncounter)
        Me.PnlMain.Controls.Add(Me.ChkSocialHistory)
        Me.PnlMain.Controls.Add(Me.ChkAllergy)
        Me.PnlMain.Controls.Add(Me.chkProblems)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(0, 54)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlMain.Size = New System.Drawing.Size(481, 225)
        Me.PnlMain.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dtpToDate)
        Me.Panel3.Controls.Add(Me.lblToDate)
        Me.Panel3.Controls.Add(Me.dtpFrom)
        Me.Panel3.Controls.Add(Me.chkDate)
        Me.Panel3.Controls.Add(Me.lblFromDate)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(473, 28)
        Me.Panel3.TabIndex = 72
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
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(292, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(108, 22)
        Me.dtpToDate.TabIndex = 69
        '
        'lblToDate
        '
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(213, 5)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(79, 23)
        Me.lblToDate.TabIndex = 68
        Me.lblToDate.Text = "To Date : "
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(105, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(108, 22)
        Me.dtpFrom.TabIndex = 67
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDate.Location = New System.Drawing.Point(90, 5)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.chkDate.Size = New System.Drawing.Size(15, 23)
        Me.chkDate.TabIndex = 70
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(0, 5)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(90, 23)
        Me.lblFromDate.TabIndex = 66
        Me.lblFromDate.Text = "From Date : "
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(477, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 217)
        Me.Label14.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 217)
        Me.Label11.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(3, 221)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(475, 1)
        Me.Label12.TabIndex = 16
        '
        'pnlPrintMessage
        '
        Me.pnlPrintMessage.BackColor = System.Drawing.Color.White
        Me.pnlPrintMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlPrintMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPrintMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrintMessage.Controls.Add(Me.Label24)
        Me.pnlPrintMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlPrintMessage.Location = New System.Drawing.Point(91, 77)
        Me.pnlPrintMessage.Name = "pnlPrintMessage"
        Me.pnlPrintMessage.Size = New System.Drawing.Size(228, 69)
        Me.pnlPrintMessage.TabIndex = 61
        Me.pnlPrintMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(20, 7)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 19)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.AutoSize = True
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(21, 33)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(184, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Printing CCD Information… "
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(475, 1)
        Me.Label13.TabIndex = 15
        '
        'ChkDemographics
        '
        Me.ChkDemographics.AutoSize = True
        Me.ChkDemographics.Checked = True
        Me.ChkDemographics.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkDemographics.Enabled = False
        Me.ChkDemographics.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDemographics.Location = New System.Drawing.Point(67, 70)
        Me.ChkDemographics.Name = "ChkDemographics"
        Me.ChkDemographics.Size = New System.Drawing.Size(102, 18)
        Me.ChkDemographics.TabIndex = 0
        Me.ChkDemographics.Text = "Demographics"
        Me.ChkDemographics.UseVisualStyleBackColor = True
        '
        'ChkVitals
        '
        Me.ChkVitals.AutoSize = True
        Me.ChkVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkVitals.Location = New System.Drawing.Point(190, 95)
        Me.ChkVitals.Name = "ChkVitals"
        Me.ChkVitals.Size = New System.Drawing.Size(54, 18)
        Me.ChkVitals.TabIndex = 2
        Me.ChkVitals.Text = "Vitals"
        Me.ChkVitals.UseVisualStyleBackColor = True
        '
        'ChkFamilyHistory
        '
        Me.ChkFamilyHistory.AutoSize = True
        Me.ChkFamilyHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFamilyHistory.Location = New System.Drawing.Point(190, 145)
        Me.ChkFamilyHistory.Name = "ChkFamilyHistory"
        Me.ChkFamilyHistory.Size = New System.Drawing.Size(99, 18)
        Me.ChkFamilyHistory.TabIndex = 6
        Me.ChkFamilyHistory.Text = "Family History"
        Me.ChkFamilyHistory.UseVisualStyleBackColor = True
        '
        'ChkAll
        '
        Me.ChkAll.AutoSize = True
        Me.ChkAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAll.Location = New System.Drawing.Point(65, 42)
        Me.ChkAll.Name = "ChkAll"
        Me.ChkAll.Size = New System.Drawing.Size(82, 18)
        Me.ChkAll.TabIndex = 0
        Me.ChkAll.Text = "Select All"
        Me.ChkAll.UseVisualStyleBackColor = True
        '
        'ChkAdvanceDirectives
        '
        Me.ChkAdvanceDirectives.AutoSize = True
        Me.ChkAdvanceDirectives.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAdvanceDirectives.Location = New System.Drawing.Point(190, 195)
        Me.ChkAdvanceDirectives.Name = "ChkAdvanceDirectives"
        Me.ChkAdvanceDirectives.Size = New System.Drawing.Size(129, 18)
        Me.ChkAdvanceDirectives.TabIndex = 10
        Me.ChkAdvanceDirectives.Text = "Advance Directives"
        Me.ChkAdvanceDirectives.UseVisualStyleBackColor = True
        '
        'ChkResults
        '
        Me.ChkResults.AutoSize = True
        Me.ChkResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkResults.Location = New System.Drawing.Point(190, 120)
        Me.ChkResults.Name = "ChkResults"
        Me.ChkResults.Size = New System.Drawing.Size(87, 18)
        Me.ChkResults.TabIndex = 4
        Me.ChkResults.Text = "Lab Results"
        Me.ChkResults.UseVisualStyleBackColor = True
        '
        'ChkImmunization
        '
        Me.ChkImmunization.AutoSize = True
        Me.ChkImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkImmunization.Location = New System.Drawing.Point(67, 145)
        Me.ChkImmunization.Name = "ChkImmunization"
        Me.ChkImmunization.Size = New System.Drawing.Size(98, 18)
        Me.ChkImmunization.TabIndex = 5
        Me.ChkImmunization.Text = "Immunization"
        Me.ChkImmunization.UseVisualStyleBackColor = True
        '
        'ChkProcedures
        '
        Me.ChkProcedures.AutoSize = True
        Me.ChkProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkProcedures.Location = New System.Drawing.Point(190, 170)
        Me.ChkProcedures.Name = "ChkProcedures"
        Me.ChkProcedures.Size = New System.Drawing.Size(87, 18)
        Me.ChkProcedures.TabIndex = 8
        Me.ChkProcedures.Text = "Procedures"
        Me.ChkProcedures.UseVisualStyleBackColor = True
        '
        'ChkMedications
        '
        Me.ChkMedications.AutoSize = True
        Me.ChkMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMedications.Location = New System.Drawing.Point(67, 120)
        Me.ChkMedications.Name = "ChkMedications"
        Me.ChkMedications.Size = New System.Drawing.Size(89, 18)
        Me.ChkMedications.TabIndex = 3
        Me.ChkMedications.Text = "Medications"
        Me.ChkMedications.UseVisualStyleBackColor = True
        '
        'ChkEncounter
        '
        Me.ChkEncounter.AutoSize = True
        Me.ChkEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkEncounter.Location = New System.Drawing.Point(67, 195)
        Me.ChkEncounter.Name = "ChkEncounter"
        Me.ChkEncounter.Size = New System.Drawing.Size(83, 18)
        Me.ChkEncounter.TabIndex = 9
        Me.ChkEncounter.Text = "Encounter"
        Me.ChkEncounter.UseVisualStyleBackColor = True
        '
        'ChkSocialHistory
        '
        Me.ChkSocialHistory.AutoSize = True
        Me.ChkSocialHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSocialHistory.Location = New System.Drawing.Point(67, 170)
        Me.ChkSocialHistory.Name = "ChkSocialHistory"
        Me.ChkSocialHistory.Size = New System.Drawing.Size(97, 18)
        Me.ChkSocialHistory.TabIndex = 7
        Me.ChkSocialHistory.Text = "Social History"
        Me.ChkSocialHistory.UseVisualStyleBackColor = True
        '
        'ChkAllergy
        '
        Me.ChkAllergy.AutoSize = True
        Me.ChkAllergy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAllergy.Location = New System.Drawing.Point(67, 95)
        Me.ChkAllergy.Name = "ChkAllergy"
        Me.ChkAllergy.Size = New System.Drawing.Size(62, 18)
        Me.ChkAllergy.TabIndex = 1
        Me.ChkAllergy.Text = "Allergy"
        Me.ChkAllergy.UseVisualStyleBackColor = True
        '
        'chkProblems
        '
        Me.chkProblems.AutoSize = True
        Me.chkProblems.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProblems.Location = New System.Drawing.Point(190, 70)
        Me.chkProblems.Name = "chkProblems"
        Me.chkProblems.Size = New System.Drawing.Size(75, 18)
        Me.chkProblems.TabIndex = 0
        Me.chkProblems.Text = "Problems"
        Me.chkProblems.UseVisualStyleBackColor = False
        '
        'chkPatientRequest
        '
        Me.chkPatientRequest.AutoSize = True
        Me.chkPatientRequest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientRequest.Location = New System.Drawing.Point(67, 9)
        Me.chkPatientRequest.Name = "chkPatientRequest"
        Me.chkPatientRequest.Size = New System.Drawing.Size(191, 18)
        Me.chkPatientRequest.TabIndex = 73
        Me.chkPatientRequest.Text = "Generated at Patient Request"
        Me.chkPatientRequest.UseVisualStyleBackColor = True
        '
        'lblCCDMessage
        '
        Me.lblCCDMessage.AutoEllipsis = True
        Me.lblCCDMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCCDMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblCCDMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCCDMessage.Location = New System.Drawing.Point(0, 0)
        Me.lblCCDMessage.Name = "lblCCDMessage"
        Me.lblCCDMessage.Size = New System.Drawing.Size(458, 56)
        Me.lblCCDMessage.TabIndex = 71
        Me.lblCCDMessage.Text = "CCD file saved successfully."
        Me.lblCCDMessage.Visible = False
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(23, 22)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(406, 112)
        Me.WebBrowser1.TabIndex = 17
        Me.WebBrowser1.Visible = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblMedication)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(481, 54)
        Me.pnlToolStrip.TabIndex = 4
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Print, Me.tlbbtn_Email, Me.tblSave, Me.tblShowCCD, Me.tblClose, Me.tblCDA})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(481, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tlbbtn_Print
        '
        Me.tlbbtn_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Print.Image = CType(resources.GetObject("tlbbtn_Print.Image"), System.Drawing.Image)
        Me.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Print.Name = "tlbbtn_Print"
        Me.tlbbtn_Print.Size = New System.Drawing.Size(45, 50)
        Me.tlbbtn_Print.Tag = "Print"
        Me.tlbbtn_Print.Text = " &Print"
        Me.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Email
        '
        Me.tlbbtn_Email.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Email.Image = CType(resources.GetObject("tlbbtn_Email.Image"), System.Drawing.Image)
        Me.tlbbtn_Email.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Email.Name = "tlbbtn_Email"
        Me.tlbbtn_Email.Size = New System.Drawing.Size(42, 50)
        Me.tlbbtn_Email.Tag = "Email"
        Me.tlbbtn_Email.Text = "&Email"
        Me.tlbbtn_Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Email.ToolTipText = "Email"
        Me.tlbbtn_Email.Visible = False
        '
        'tblSave
        '
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(69, 50)
        Me.tblSave.Tag = "Save CCD"
        Me.tblSave.Text = "&Save CCD"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save CCD"
        '
        'tblShowCCD
        '
        Me.tblShowCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblShowCCD.Image = CType(resources.GetObject("tblShowCCD.Image"), System.Drawing.Image)
        Me.tblShowCCD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblShowCCD.Name = "tblShowCCD"
        Me.tblShowCCD.Size = New System.Drawing.Size(88, 50)
        Me.tblShowCCD.Tag = "Preview CCD"
        Me.tblShowCCD.Text = "Pre&view CCD"
        Me.tblShowCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblShowCCD.ToolTipText = "Preview CCD"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'tblCDA
        '
        Me.tblCDA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCDA.Image = CType(resources.GetObject("tblCDA.Image"), System.Drawing.Image)
        Me.tblCDA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblCDA.Name = "tblCDA"
        Me.tblCDA.Size = New System.Drawing.Size(96, 50)
        Me.tblCDA.Tag = "Preview CCD"
        Me.tblCDA.Text = "Generate CDA"
        Me.tblCDA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCDA.ToolTipText = "Generate CDA"
        Me.tblCDA.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.WebBrowser1)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(3, 267)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(414, 45)
        Me.Panel1.TabIndex = 19
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Location = New System.Drawing.Point(4, 41)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(406, 1)
        Me.Label18.TabIndex = 17
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(406, 1)
        Me.Label17.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(3, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 42)
        Me.Label16.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(410, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 42)
        Me.Label15.TabIndex = 7
        '
        'PnlMail
        '
        Me.PnlMail.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlMail.Controls.Add(Me.Panel2)
        Me.PnlMail.Controls.Add(Me.Label10)
        Me.PnlMail.Controls.Add(Me.Label9)
        Me.PnlMail.Controls.Add(Me.Label8)
        Me.PnlMail.Controls.Add(Me.Label7)
        Me.PnlMail.Controls.Add(Me.Label3)
        Me.PnlMail.Controls.Add(Me.Label5)
        Me.PnlMail.Controls.Add(Me.Label4)
        Me.PnlMail.Controls.Add(Me.Label1)
        Me.PnlMail.Controls.Add(Me.txtAttachment)
        Me.PnlMail.Controls.Add(Me.txtMailTo)
        Me.PnlMail.Controls.Add(Me.txtPassword)
        Me.PnlMail.Controls.Add(Me.txtMailFrom)
        Me.PnlMail.Location = New System.Drawing.Point(63, 132)
        Me.PnlMail.Name = "PnlMail"
        Me.PnlMail.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlMail.Size = New System.Drawing.Size(12, 74)
        Me.PnlMail.TabIndex = 62
        Me.PnlMail.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ToolStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(4, 45)
        Me.Panel2.TabIndex = 10
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSend, Me.cmdCancel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(4, 44)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdSend
        '
        Me.cmdSend.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSend.Image = CType(resources.GetObject("cmdSend.Image"), System.Drawing.Image)
        Me.cmdSend.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(65, 41)
        Me.cmdSend.Text = "Send Mail"
        Me.cmdSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(48, 41)
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(8, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 66)
        Me.Label10.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(3, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 66)
        Me.Label9.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(3, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(6, 1)
        Me.Label8.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(6, 1)
        Me.Label7.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Attachment :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(64, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 14)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "To :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 14)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Password :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(52, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From :"
        '
        'txtAttachment
        '
        Me.txtAttachment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttachment.Location = New System.Drawing.Point(97, 153)
        Me.txtAttachment.Name = "txtAttachment"
        Me.txtAttachment.ReadOnly = True
        Me.txtAttachment.Size = New System.Drawing.Size(277, 22)
        Me.txtAttachment.TabIndex = 0
        '
        'txtMailTo
        '
        Me.txtMailTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailTo.Location = New System.Drawing.Point(97, 124)
        Me.txtMailTo.Name = "txtMailTo"
        Me.txtMailTo.Size = New System.Drawing.Size(277, 22)
        Me.txtMailTo.TabIndex = 7
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(97, 95)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(277, 22)
        Me.txtPassword.TabIndex = 4
        '
        'txtMailFrom
        '
        Me.txtMailFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMailFrom.Location = New System.Drawing.Point(97, 68)
        Me.txtMailFrom.Name = "txtMailFrom"
        Me.txtMailFrom.Size = New System.Drawing.Size(277, 22)
        Me.txtMailFrom.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.chkPatientRequest)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 279)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(481, 107)
        Me.Panel4.TabIndex = 63
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblCCDMessage)
        Me.Panel5.Location = New System.Drawing.Point(12, 42)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(460, 58)
        Me.Panel5.TabIndex = 74
        Me.Panel5.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(477, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 102)
        Me.Label2.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 102)
        Me.Label6.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(3, 103)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(475, 1)
        Me.Label19.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(3, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(475, 1)
        Me.Label20.TabIndex = 3
        '
        'frmCCDGenerateList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(481, 386)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.PnlMail)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(699, 967)
        Me.MinimumSize = New System.Drawing.Size(303, 288)
        Me.Name = "frmCCDGenerateList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate CCD"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PnlMain.ResumeLayout(False)
        Me.PnlMain.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlPrintMessage.ResumeLayout(False)
        Me.pnlPrintMessage.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PnlMail.ResumeLayout(False)
        Me.PnlMail.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents ChkDemographics As System.Windows.Forms.CheckBox
    Friend WithEvents ChkVitals As System.Windows.Forms.CheckBox
    Friend WithEvents ChkFamilyHistory As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAll As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAdvanceDirectives As System.Windows.Forms.CheckBox
    Friend WithEvents ChkResults As System.Windows.Forms.CheckBox
    Friend WithEvents ChkImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents ChkProcedures As System.Windows.Forms.CheckBox
    Friend WithEvents ChkMedications As System.Windows.Forms.CheckBox
    Friend WithEvents ChkEncounter As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSocialHistory As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAllergy As System.Windows.Forms.CheckBox
    Friend WithEvents chkProblems As System.Windows.Forms.CheckBox
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Print As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_Email As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCancelelel As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents pnlPrintMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
    Friend WithEvents tblShowCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlMail As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAttachment As System.Windows.Forms.TextBox
    Friend WithEvents txtMailTo As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtMailFrom As System.Windows.Forms.TextBox
    Public WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents cmdSend As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCancel As System.Windows.Forms.ToolStripButton
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblCCDMessage As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkPatientRequest As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents tblCDA As System.Windows.Forms.ToolStripButton
    Private WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
End Class
