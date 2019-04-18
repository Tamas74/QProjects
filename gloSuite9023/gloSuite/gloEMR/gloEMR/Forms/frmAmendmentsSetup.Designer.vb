<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAmendmentsSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpAcceptDeniedAmendmentDate, dtpAcceptDeniedAmendmentDate}
                Dim cntControls() As System.Windows.Forms.Control = {dtpAcceptDeniedAmendmentDate, dtpAcceptDeniedAmendmentDate}
                Try

                    components.Dispose()

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


                Catch
                End Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try


            End If
            Try
                If Not IsNothing(gloUC_PatientStrip) Then
                    gloUC_PatientStrip.Dispose()
                    gloUC_PatientStrip = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAmendmentsSetup))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.txtAmedmentScan = New System.Windows.Forms.TextBox()
        Me.lblAmedmentScan = New System.Windows.Forms.Label()
        Me.CmbProvider = New System.Windows.Forms.ComboBox()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.lbldetailsMandatory = New System.Windows.Forms.Label()
        Me.lblReasonMandatory = New System.Windows.Forms.Label()
        Me.lblOthernamemandatory = New System.Windows.Forms.Label()
        Me.dtRequestDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAcceptedDeniedAmendmentUser = New System.Windows.Forms.ComboBox()
        Me.grpDeniedStatus = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkDeniedReasonOne = New System.Windows.Forms.CheckBox()
        Me.chkDeniedReasonFour = New System.Windows.Forms.CheckBox()
        Me.chkDeniedReasonTwo = New System.Windows.Forms.CheckBox()
        Me.chkDeniedReasonThree = New System.Windows.Forms.CheckBox()
        Me.msktxtRequestorPhone = New gloMaskControl.gloMaskBox()
        Me.txtAcceptedDeniedNotes = New System.Windows.Forms.TextBox()
        Me.txtAmendmentDetails = New System.Windows.Forms.TextBox()
        Me.txtAmendmentReason = New System.Windows.Forms.TextBox()
        Me.txtOtherRequestor = New System.Windows.Forms.TextBox()
        Me.grpAmendmentStatus = New System.Windows.Forms.GroupBox()
        Me.rbStatusPending = New System.Windows.Forms.RadioButton()
        Me.rbStatusDenied = New System.Windows.Forms.RadioButton()
        Me.rbStatusAccepted = New System.Windows.Forms.RadioButton()
        Me.grpRequestorType = New System.Windows.Forms.GroupBox()
        Me.rbRequestorProvider = New System.Windows.Forms.RadioButton()
        Me.rbRequestorOther = New System.Windows.Forms.RadioButton()
        Me.rbRequestorPatient = New System.Windows.Forms.RadioButton()
        Me.lblRequestorPhone = New System.Windows.Forms.Label()
        Me.lblAcceptedDeniedNotes = New System.Windows.Forms.Label()
        Me.lblAmendmentStatus = New System.Windows.Forms.Label()
        Me.lblAmendmentDetails = New System.Windows.Forms.Label()
        Me.lblAmendmentReason = New System.Windows.Forms.Label()
        Me.lblOtherRequestor = New System.Windows.Forms.Label()
        Me.lblRequestDate = New System.Windows.Forms.Label()
        Me.lblRequestorType = New System.Windows.Forms.Label()
        Me.lblAmendmentAcceptedDeniedUser = New System.Windows.Forms.Label()
        Me.lblAmendmentRequestDate = New System.Windows.Forms.Label()
        Me.dtpAcceptDeniedAmendmentDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tls_AmendmentsControl = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain.SuspendLayout()
        Me.grpDeniedStatus.SuspendLayout()
        Me.grpAmendmentStatus.SuspendLayout()
        Me.grpRequestorType.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_AmendmentsControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.btnView)
        Me.pnlMain.Controls.Add(Me.btnScan)
        Me.pnlMain.Controls.Add(Me.txtAmedmentScan)
        Me.pnlMain.Controls.Add(Me.lblAmedmentScan)
        Me.pnlMain.Controls.Add(Me.CmbProvider)
        Me.pnlMain.Controls.Add(Me.lblProvider)
        Me.pnlMain.Controls.Add(Me.lbldetailsMandatory)
        Me.pnlMain.Controls.Add(Me.lblReasonMandatory)
        Me.pnlMain.Controls.Add(Me.lblOthernamemandatory)
        Me.pnlMain.Controls.Add(Me.dtRequestDate)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.cmbAcceptedDeniedAmendmentUser)
        Me.pnlMain.Controls.Add(Me.grpDeniedStatus)
        Me.pnlMain.Controls.Add(Me.msktxtRequestorPhone)
        Me.pnlMain.Controls.Add(Me.txtAcceptedDeniedNotes)
        Me.pnlMain.Controls.Add(Me.txtAmendmentDetails)
        Me.pnlMain.Controls.Add(Me.txtAmendmentReason)
        Me.pnlMain.Controls.Add(Me.txtOtherRequestor)
        Me.pnlMain.Controls.Add(Me.grpAmendmentStatus)
        Me.pnlMain.Controls.Add(Me.grpRequestorType)
        Me.pnlMain.Controls.Add(Me.lblRequestorPhone)
        Me.pnlMain.Controls.Add(Me.lblAcceptedDeniedNotes)
        Me.pnlMain.Controls.Add(Me.lblAmendmentStatus)
        Me.pnlMain.Controls.Add(Me.lblAmendmentDetails)
        Me.pnlMain.Controls.Add(Me.lblAmendmentReason)
        Me.pnlMain.Controls.Add(Me.lblOtherRequestor)
        Me.pnlMain.Controls.Add(Me.lblRequestDate)
        Me.pnlMain.Controls.Add(Me.lblRequestorType)
        Me.pnlMain.Controls.Add(Me.lblAmendmentAcceptedDeniedUser)
        Me.pnlMain.Controls.Add(Me.lblAmendmentRequestDate)
        Me.pnlMain.Controls.Add(Me.dtpAcceptDeniedAmendmentDate)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(791, 628)
        Me.pnlMain.TabIndex = 0
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Location = New System.Drawing.Point(709, 74)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(24, 23)
        Me.btnView.TabIndex = 68
        Me.btnView.UseVisualStyleBackColor = True
        Me.btnView.Visible = False
        '
        'btnScan
        '
        Me.btnScan.BackgroundImage = CType(resources.GetObject("btnScan.BackgroundImage"), System.Drawing.Image)
        Me.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.Location = New System.Drawing.Point(682, 74)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(24, 23)
        Me.btnScan.TabIndex = 67
        Me.btnScan.UseVisualStyleBackColor = True
        Me.btnScan.Visible = False
        '
        'txtAmedmentScan
        '
        Me.txtAmedmentScan.Enabled = False
        Me.txtAmedmentScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmedmentScan.ForeColor = System.Drawing.Color.Black
        Me.txtAmedmentScan.Location = New System.Drawing.Point(505, 75)
        Me.txtAmedmentScan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAmedmentScan.MaxLength = 1000
        Me.txtAmedmentScan.Name = "txtAmedmentScan"
        Me.txtAmedmentScan.Size = New System.Drawing.Size(175, 22)
        Me.txtAmedmentScan.TabIndex = 66
        Me.txtAmedmentScan.Visible = False
        '
        'lblAmedmentScan
        '
        Me.lblAmedmentScan.AutoSize = True
        Me.lblAmedmentScan.BackColor = System.Drawing.Color.Transparent
        Me.lblAmedmentScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmedmentScan.Location = New System.Drawing.Point(389, 79)
        Me.lblAmedmentScan.Name = "lblAmedmentScan"
        Me.lblAmedmentScan.Size = New System.Drawing.Size(113, 14)
        Me.lblAmedmentScan.TabIndex = 65
        Me.lblAmedmentScan.Text = "Amendment Scan :"
        Me.lblAmedmentScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmedmentScan.Visible = False
        '
        'CmbProvider
        '
        Me.CmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbProvider.FormattingEnabled = True
        Me.CmbProvider.Location = New System.Drawing.Point(171, 45)
        Me.CmbProvider.Name = "CmbProvider"
        Me.CmbProvider.Size = New System.Drawing.Size(165, 22)
        Me.CmbProvider.TabIndex = 3
        Me.CmbProvider.Visible = False
        '
        'lblProvider
        '
        Me.lblProvider.AutoSize = True
        Me.lblProvider.Location = New System.Drawing.Point(109, 49)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(59, 14)
        Me.lblProvider.TabIndex = 64
        Me.lblProvider.Text = "Provider :"
        Me.lblProvider.Visible = False
        '
        'lbldetailsMandatory
        '
        Me.lbldetailsMandatory.AutoSize = True
        Me.lbldetailsMandatory.ForeColor = System.Drawing.Color.Red
        Me.lbldetailsMandatory.Location = New System.Drawing.Point(34, 133)
        Me.lbldetailsMandatory.Name = "lbldetailsMandatory"
        Me.lbldetailsMandatory.Size = New System.Drawing.Size(14, 14)
        Me.lbldetailsMandatory.TabIndex = 63
        Me.lbldetailsMandatory.Text = "*"
        '
        'lblReasonMandatory
        '
        Me.lblReasonMandatory.AutoSize = True
        Me.lblReasonMandatory.ForeColor = System.Drawing.Color.Red
        Me.lblReasonMandatory.Location = New System.Drawing.Point(30, 108)
        Me.lblReasonMandatory.Name = "lblReasonMandatory"
        Me.lblReasonMandatory.Size = New System.Drawing.Size(14, 14)
        Me.lblReasonMandatory.TabIndex = 62
        Me.lblReasonMandatory.Text = "*"
        '
        'lblOthernamemandatory
        '
        Me.lblOthernamemandatory.AutoSize = True
        Me.lblOthernamemandatory.ForeColor = System.Drawing.Color.Red
        Me.lblOthernamemandatory.Location = New System.Drawing.Point(13, 49)
        Me.lblOthernamemandatory.Name = "lblOthernamemandatory"
        Me.lblOthernamemandatory.Size = New System.Drawing.Size(14, 14)
        Me.lblOthernamemandatory.TabIndex = 61
        Me.lblOthernamemandatory.Text = "*"
        Me.lblOthernamemandatory.Visible = False
        '
        'dtRequestDate
        '
        Me.dtRequestDate.CustomFormat = "MM/dd/yyyy hh:mm:tt"
        Me.dtRequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRequestDate.Location = New System.Drawing.Point(502, 11)
        Me.dtRequestDate.Name = "dtRequestDate"
        Me.dtRequestDate.Size = New System.Drawing.Size(175, 22)
        Me.dtRequestDate.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(787, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 620)
        Me.Label4.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 620)
        Me.Label3.TabIndex = 58
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 624)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(785, 1)
        Me.Label2.TabIndex = 57
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(785, 1)
        Me.Label1.TabIndex = 56
        '
        'cmbAcceptedDeniedAmendmentUser
        '
        Me.cmbAcceptedDeniedAmendmentUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAcceptedDeniedAmendmentUser.FormattingEnabled = True
        Me.cmbAcceptedDeniedAmendmentUser.Location = New System.Drawing.Point(488, 263)
        Me.cmbAcceptedDeniedAmendmentUser.Name = "cmbAcceptedDeniedAmendmentUser"
        Me.cmbAcceptedDeniedAmendmentUser.Size = New System.Drawing.Size(165, 22)
        Me.cmbAcceptedDeniedAmendmentUser.TabIndex = 10
        '
        'grpDeniedStatus
        '
        Me.grpDeniedStatus.Controls.Add(Me.Label5)
        Me.grpDeniedStatus.Controls.Add(Me.chkDeniedReasonOne)
        Me.grpDeniedStatus.Controls.Add(Me.chkDeniedReasonFour)
        Me.grpDeniedStatus.Controls.Add(Me.chkDeniedReasonTwo)
        Me.grpDeniedStatus.Controls.Add(Me.chkDeniedReasonThree)
        Me.grpDeniedStatus.Location = New System.Drawing.Point(16, 288)
        Me.grpDeniedStatus.Name = "grpDeniedStatus"
        Me.grpDeniedStatus.Size = New System.Drawing.Size(717, 163)
        Me.grpDeniedStatus.TabIndex = 11
        Me.grpDeniedStatus.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 14)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "If denied, check reason for denial :"
        '
        'chkDeniedReasonOne
        '
        Me.chkDeniedReasonOne.AutoSize = True
        Me.chkDeniedReasonOne.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkDeniedReasonOne.Location = New System.Drawing.Point(20, 45)
        Me.chkDeniedReasonOne.Name = "chkDeniedReasonOne"
        Me.chkDeniedReasonOne.Size = New System.Drawing.Size(523, 18)
        Me.chkDeniedReasonOne.TabIndex = 1
        Me.chkDeniedReasonOne.Tag = "DeniedReasonOne"
        Me.chkDeniedReasonOne.Text = "The protected health information or record was not created by this organization"
        Me.chkDeniedReasonOne.UseVisualStyleBackColor = True
        '
        'chkDeniedReasonFour
        '
        Me.chkDeniedReasonFour.AutoSize = True
        Me.chkDeniedReasonFour.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkDeniedReasonFour.Location = New System.Drawing.Point(20, 131)
        Me.chkDeniedReasonFour.Name = "chkDeniedReasonFour"
        Me.chkDeniedReasonFour.Size = New System.Drawing.Size(448, 18)
        Me.chkDeniedReasonFour.TabIndex = 4
        Me.chkDeniedReasonFour.Tag = "DeniedReasonFour"
        Me.chkDeniedReasonFour.Text = "The protected health information or record is accurate and complete"
        Me.chkDeniedReasonFour.UseVisualStyleBackColor = True
        '
        'chkDeniedReasonTwo
        '
        Me.chkDeniedReasonTwo.AutoSize = True
        Me.chkDeniedReasonTwo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkDeniedReasonTwo.Location = New System.Drawing.Point(20, 69)
        Me.chkDeniedReasonTwo.Name = "chkDeniedReasonTwo"
        Me.chkDeniedReasonTwo.Size = New System.Drawing.Size(551, 18)
        Me.chkDeniedReasonTwo.TabIndex = 2
        Me.chkDeniedReasonTwo.Tag = "DeniedReasonTwo"
        Me.chkDeniedReasonTwo.Text = "The protected health information is not part of the patient's ""designated record " & _
    "set"""
        Me.chkDeniedReasonTwo.UseVisualStyleBackColor = True
        '
        'chkDeniedReasonThree
        '
        Me.chkDeniedReasonThree.AutoSize = True
        Me.chkDeniedReasonThree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkDeniedReasonThree.Location = New System.Drawing.Point(20, 93)
        Me.chkDeniedReasonThree.Name = "chkDeniedReasonThree"
        Me.chkDeniedReasonThree.Size = New System.Drawing.Size(663, 32)
        Me.chkDeniedReasonThree.TabIndex = 3
        Me.chkDeniedReasonThree.Tag = "DeniedReasonThree"
        Me.chkDeniedReasonThree.Text = "The protected health information or record is not available to the patient for in" & _
    "spection as required by " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "federal law (e.g., psychotherapy notes.)"
        Me.chkDeniedReasonThree.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkDeniedReasonThree.UseVisualStyleBackColor = True
        '
        'msktxtRequestorPhone
        '
        Me.msktxtRequestorPhone.AllowValidate = True
        Me.msktxtRequestorPhone.IncludeLiteralsAndPrompts = False
        Me.msktxtRequestorPhone.Location = New System.Drawing.Point(171, 75)
        Me.msktxtRequestorPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.msktxtRequestorPhone.Name = "msktxtRequestorPhone"
        Me.msktxtRequestorPhone.ReadOnly = False
        Me.msktxtRequestorPhone.Size = New System.Drawing.Size(102, 22)
        Me.msktxtRequestorPhone.TabIndex = 5
        '
        'txtAcceptedDeniedNotes
        '
        Me.txtAcceptedDeniedNotes.Location = New System.Drawing.Point(171, 462)
        Me.txtAcceptedDeniedNotes.Multiline = True
        Me.txtAcceptedDeniedNotes.Name = "txtAcceptedDeniedNotes"
        Me.txtAcceptedDeniedNotes.Size = New System.Drawing.Size(562, 78)
        Me.txtAcceptedDeniedNotes.TabIndex = 12
        '
        'txtAmendmentDetails
        '
        Me.txtAmendmentDetails.Location = New System.Drawing.Point(171, 133)
        Me.txtAmendmentDetails.MaxLength = 5000
        Me.txtAmendmentDetails.Multiline = True
        Me.txtAmendmentDetails.Name = "txtAmendmentDetails"
        Me.txtAmendmentDetails.Size = New System.Drawing.Size(562, 96)
        Me.txtAmendmentDetails.TabIndex = 7
        '
        'txtAmendmentReason
        '
        Me.txtAmendmentReason.Location = New System.Drawing.Point(171, 104)
        Me.txtAmendmentReason.MaxLength = 100
        Me.txtAmendmentReason.Name = "txtAmendmentReason"
        Me.txtAmendmentReason.Size = New System.Drawing.Size(562, 22)
        Me.txtAmendmentReason.TabIndex = 6
        '
        'txtOtherRequestor
        '
        Me.txtOtherRequestor.Location = New System.Drawing.Point(171, 45)
        Me.txtOtherRequestor.MaxLength = 100
        Me.txtOtherRequestor.Name = "txtOtherRequestor"
        Me.txtOtherRequestor.Size = New System.Drawing.Size(329, 22)
        Me.txtOtherRequestor.TabIndex = 4
        Me.txtOtherRequestor.Visible = False
        '
        'grpAmendmentStatus
        '
        Me.grpAmendmentStatus.Controls.Add(Me.rbStatusPending)
        Me.grpAmendmentStatus.Controls.Add(Me.rbStatusDenied)
        Me.grpAmendmentStatus.Controls.Add(Me.rbStatusAccepted)
        Me.grpAmendmentStatus.Location = New System.Drawing.Point(171, 228)
        Me.grpAmendmentStatus.Name = "grpAmendmentStatus"
        Me.grpAmendmentStatus.Size = New System.Drawing.Size(248, 29)
        Me.grpAmendmentStatus.TabIndex = 8
        Me.grpAmendmentStatus.TabStop = False
        '
        'rbStatusPending
        '
        Me.rbStatusPending.AutoSize = True
        Me.rbStatusPending.Location = New System.Drawing.Point(163, 10)
        Me.rbStatusPending.Name = "rbStatusPending"
        Me.rbStatusPending.Size = New System.Drawing.Size(64, 17)
        Me.rbStatusPending.TabIndex = 0
        Me.rbStatusPending.Tag = "PENDING"
        Me.rbStatusPending.Text = "Pending"
        Me.rbStatusPending.UseVisualStyleBackColor = True
        '
        'rbStatusDenied
        '
        Me.rbStatusDenied.AutoSize = True
        Me.rbStatusDenied.Location = New System.Drawing.Point(93, 10)
        Me.rbStatusDenied.Name = "rbStatusDenied"
        Me.rbStatusDenied.Size = New System.Drawing.Size(59, 17)
        Me.rbStatusDenied.TabIndex = 0
        Me.rbStatusDenied.Tag = "DENIED"
        Me.rbStatusDenied.Text = "Denied"
        Me.rbStatusDenied.UseVisualStyleBackColor = True
        '
        'rbStatusAccepted
        '
        Me.rbStatusAccepted.AutoSize = True
        Me.rbStatusAccepted.Location = New System.Drawing.Point(8, 10)
        Me.rbStatusAccepted.Name = "rbStatusAccepted"
        Me.rbStatusAccepted.Size = New System.Drawing.Size(71, 17)
        Me.rbStatusAccepted.TabIndex = 0
        Me.rbStatusAccepted.Tag = "ACCEPTED"
        Me.rbStatusAccepted.Text = "Accepted"
        Me.rbStatusAccepted.UseVisualStyleBackColor = True
        '
        'grpRequestorType
        '
        Me.grpRequestorType.Controls.Add(Me.rbRequestorProvider)
        Me.grpRequestorType.Controls.Add(Me.rbRequestorOther)
        Me.grpRequestorType.Controls.Add(Me.rbRequestorPatient)
        Me.grpRequestorType.Location = New System.Drawing.Point(171, 6)
        Me.grpRequestorType.Name = "grpRequestorType"
        Me.grpRequestorType.Size = New System.Drawing.Size(224, 32)
        Me.grpRequestorType.TabIndex = 1
        Me.grpRequestorType.TabStop = False
        '
        'rbRequestorProvider
        '
        Me.rbRequestorProvider.AutoSize = True
        Me.rbRequestorProvider.Location = New System.Drawing.Point(80, 10)
        Me.rbRequestorProvider.Name = "rbRequestorProvider"
        Me.rbRequestorProvider.Size = New System.Drawing.Size(64, 17)
        Me.rbRequestorProvider.TabIndex = 1
        Me.rbRequestorProvider.Tag = "PROVIDER"
        Me.rbRequestorProvider.Text = "Provider"
        Me.rbRequestorProvider.UseVisualStyleBackColor = True
        '
        'rbRequestorOther
        '
        Me.rbRequestorOther.AutoSize = True
        Me.rbRequestorOther.Location = New System.Drawing.Point(156, 10)
        Me.rbRequestorOther.Name = "rbRequestorOther"
        Me.rbRequestorOther.Size = New System.Drawing.Size(51, 17)
        Me.rbRequestorOther.TabIndex = 0
        Me.rbRequestorOther.Tag = "OTHER"
        Me.rbRequestorOther.Text = "Other"
        Me.rbRequestorOther.UseVisualStyleBackColor = True
        '
        'rbRequestorPatient
        '
        Me.rbRequestorPatient.AutoSize = True
        Me.rbRequestorPatient.Location = New System.Drawing.Point(7, 10)
        Me.rbRequestorPatient.Name = "rbRequestorPatient"
        Me.rbRequestorPatient.Size = New System.Drawing.Size(58, 17)
        Me.rbRequestorPatient.TabIndex = 0
        Me.rbRequestorPatient.Tag = "PATIENT"
        Me.rbRequestorPatient.Text = "Patient"
        Me.rbRequestorPatient.UseVisualStyleBackColor = True
        '
        'lblRequestorPhone
        '
        Me.lblRequestorPhone.AutoSize = True
        Me.lblRequestorPhone.Location = New System.Drawing.Point(7, 79)
        Me.lblRequestorPhone.Name = "lblRequestorPhone"
        Me.lblRequestorPhone.Size = New System.Drawing.Size(162, 14)
        Me.lblRequestorPhone.TabIndex = 48
        Me.lblRequestorPhone.Text = "Patient/Requester's Phone :"
        '
        'lblAcceptedDeniedNotes
        '
        Me.lblAcceptedDeniedNotes.AutoSize = True
        Me.lblAcceptedDeniedNotes.Location = New System.Drawing.Point(18, 466)
        Me.lblAcceptedDeniedNotes.Name = "lblAcceptedDeniedNotes"
        Me.lblAcceptedDeniedNotes.Size = New System.Drawing.Size(147, 14)
        Me.lblAcceptedDeniedNotes.TabIndex = 48
        Me.lblAcceptedDeniedNotes.Text = "Accepted/Denied Notes :"
        '
        'lblAmendmentStatus
        '
        Me.lblAmendmentStatus.AutoSize = True
        Me.lblAmendmentStatus.Location = New System.Drawing.Point(46, 240)
        Me.lblAmendmentStatus.Name = "lblAmendmentStatus"
        Me.lblAmendmentStatus.Size = New System.Drawing.Size(122, 14)
        Me.lblAmendmentStatus.TabIndex = 48
        Me.lblAmendmentStatus.Text = "Amendment Status :"
        '
        'lblAmendmentDetails
        '
        Me.lblAmendmentDetails.AutoSize = True
        Me.lblAmendmentDetails.Location = New System.Drawing.Point(46, 133)
        Me.lblAmendmentDetails.Name = "lblAmendmentDetails"
        Me.lblAmendmentDetails.Size = New System.Drawing.Size(122, 14)
        Me.lblAmendmentDetails.TabIndex = 48
        Me.lblAmendmentDetails.Text = "Amendment Details :"
        '
        'lblAmendmentReason
        '
        Me.lblAmendmentReason.AutoSize = True
        Me.lblAmendmentReason.Location = New System.Drawing.Point(42, 108)
        Me.lblAmendmentReason.Name = "lblAmendmentReason"
        Me.lblAmendmentReason.Size = New System.Drawing.Size(126, 14)
        Me.lblAmendmentReason.TabIndex = 48
        Me.lblAmendmentReason.Text = "Amendment Reason :"
        '
        'lblOtherRequestor
        '
        Me.lblOtherRequestor.AutoSize = True
        Me.lblOtherRequestor.Location = New System.Drawing.Point(26, 49)
        Me.lblOtherRequestor.Name = "lblOtherRequestor"
        Me.lblOtherRequestor.Size = New System.Drawing.Size(142, 14)
        Me.lblOtherRequestor.TabIndex = 48
        Me.lblOtherRequestor.Text = "If Other, please specify :"
        Me.lblOtherRequestor.Visible = False
        '
        'lblRequestDate
        '
        Me.lblRequestDate.AutoSize = True
        Me.lblRequestDate.Location = New System.Drawing.Point(409, 15)
        Me.lblRequestDate.Name = "lblRequestDate"
        Me.lblRequestDate.Size = New System.Drawing.Size(90, 14)
        Me.lblRequestDate.TabIndex = 48
        Me.lblRequestDate.Text = "Request Date :"
        '
        'lblRequestorType
        '
        Me.lblRequestorType.AutoSize = True
        Me.lblRequestorType.Location = New System.Drawing.Point(97, 18)
        Me.lblRequestorType.Name = "lblRequestorType"
        Me.lblRequestorType.Size = New System.Drawing.Size(71, 14)
        Me.lblRequestorType.TabIndex = 48
        Me.lblRequestorType.Text = "Requester :"
        '
        'lblAmendmentAcceptedDeniedUser
        '
        Me.lblAmendmentAcceptedDeniedUser.AutoSize = True
        Me.lblAmendmentAcceptedDeniedUser.Location = New System.Drawing.Point(346, 267)
        Me.lblAmendmentAcceptedDeniedUser.Name = "lblAmendmentAcceptedDeniedUser"
        Me.lblAmendmentAcceptedDeniedUser.Size = New System.Drawing.Size(139, 14)
        Me.lblAmendmentAcceptedDeniedUser.TabIndex = 48
        Me.lblAmendmentAcceptedDeniedUser.Text = "Accepted/Denied User :"
        '
        'lblAmendmentRequestDate
        '
        Me.lblAmendmentRequestDate.AutoSize = True
        Me.lblAmendmentRequestDate.Location = New System.Drawing.Point(27, 267)
        Me.lblAmendmentRequestDate.Name = "lblAmendmentRequestDate"
        Me.lblAmendmentRequestDate.Size = New System.Drawing.Size(141, 14)
        Me.lblAmendmentRequestDate.TabIndex = 48
        Me.lblAmendmentRequestDate.Text = "Accepted/Denied Date :"
        '
        'dtpAcceptDeniedAmendmentDate
        '
        Me.dtpAcceptDeniedAmendmentDate.CustomFormat = "MM/dd/yyyy hh:mm:tt"
        Me.dtpAcceptDeniedAmendmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAcceptDeniedAmendmentDate.Location = New System.Drawing.Point(171, 263)
        Me.dtpAcceptDeniedAmendmentDate.Name = "dtpAcceptDeniedAmendmentDate"
        Me.dtpAcceptDeniedAmendmentDate.Size = New System.Drawing.Size(165, 22)
        Me.dtpAcceptDeniedAmendmentDate.TabIndex = 9
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tls_AmendmentsControl)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(791, 54)
        Me.pnlToolStrip.TabIndex = 25
        '
        'tls_AmendmentsControl
        '
        Me.tls_AmendmentsControl.BackColor = System.Drawing.Color.Transparent
        Me.tls_AmendmentsControl.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_AmendmentsControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_AmendmentsControl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_AmendmentsControl.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_AmendmentsControl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_Save, Me.tsBtn_Close})
        Me.tls_AmendmentsControl.Location = New System.Drawing.Point(0, 0)
        Me.tls_AmendmentsControl.Name = "tls_AmendmentsControl"
        Me.tls_AmendmentsControl.Size = New System.Drawing.Size(791, 53)
        Me.tls_AmendmentsControl.TabIndex = 3
        Me.tls_AmendmentsControl.Text = "ToolStrip1"
        '
        'tsBtn_Save
        '
        Me.tsBtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsBtn_Save.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsBtn_Save.Image = CType(resources.GetObject("tsBtn_Save.Image"), System.Drawing.Image)
        Me.tsBtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_Save.Name = "tsBtn_Save"
        Me.tsBtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tsBtn_Save.Tag = "Save"
        Me.tsBtn_Save.Text = "Save&&Cls"
        Me.tsBtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_Save.ToolTipText = "Save and Close"
        '
        'tsBtn_Close
        '
        Me.tsBtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsBtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsBtn_Close.Image = CType(resources.GetObject("tsBtn_Close.Image"), System.Drawing.Image)
        Me.tsBtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_Close.Name = "tsBtn_Close"
        Me.tsBtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_Close.Tag = "Close"
        Me.tsBtn_Close.Text = "&Close"
        Me.tsBtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_Close.ToolTipText = "Close"
        '
        'frmAmendmentsSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(791, 682)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAmendmentsSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Amendment"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.grpDeniedStatus.ResumeLayout(False)
        Me.grpDeniedStatus.PerformLayout()
        Me.grpAmendmentStatus.ResumeLayout(False)
        Me.grpAmendmentStatus.PerformLayout()
        Me.grpRequestorType.ResumeLayout(False)
        Me.grpRequestorType.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_AmendmentsControl.ResumeLayout(False)
        Me.tls_AmendmentsControl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tls_AmendmentsControl As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblAmendmentRequestDate As System.Windows.Forms.Label
    Friend WithEvents dtpAcceptDeniedAmendmentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAmendmentReason As System.Windows.Forms.TextBox
    Friend WithEvents txtOtherRequestor As System.Windows.Forms.TextBox
    Friend WithEvents grpRequestorType As System.Windows.Forms.GroupBox
    Friend WithEvents rbRequestorOther As System.Windows.Forms.RadioButton
    Friend WithEvents rbRequestorPatient As System.Windows.Forms.RadioButton
    Friend WithEvents lblRequestorPhone As System.Windows.Forms.Label
    Friend WithEvents lblAmendmentReason As System.Windows.Forms.Label
    Friend WithEvents lblOtherRequestor As System.Windows.Forms.Label
    Friend WithEvents lblRequestorType As System.Windows.Forms.Label
    Friend WithEvents msktxtRequestorPhone As gloMaskControl.gloMaskBox
    Friend WithEvents lblAmendmentStatus As System.Windows.Forms.Label
    Friend WithEvents cmbAcceptedDeniedAmendmentUser As System.Windows.Forms.ComboBox
    Friend WithEvents grpDeniedStatus As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkDeniedReasonOne As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeniedReasonFour As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeniedReasonTwo As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeniedReasonThree As System.Windows.Forms.CheckBox
    Friend WithEvents txtAmendmentDetails As System.Windows.Forms.TextBox
    Friend WithEvents lblAmendmentDetails As System.Windows.Forms.Label
    Friend WithEvents lblAmendmentAcceptedDeniedUser As System.Windows.Forms.Label
    Friend WithEvents txtAcceptedDeniedNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblAcceptedDeniedNotes As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtRequestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRequestDate As System.Windows.Forms.Label
    Friend WithEvents grpAmendmentStatus As System.Windows.Forms.GroupBox
    Friend WithEvents rbStatusPending As System.Windows.Forms.RadioButton
    Friend WithEvents rbStatusDenied As System.Windows.Forms.RadioButton
    Friend WithEvents rbStatusAccepted As System.Windows.Forms.RadioButton
    Friend WithEvents lbldetailsMandatory As System.Windows.Forms.Label
    Friend WithEvents lblReasonMandatory As System.Windows.Forms.Label
    Friend WithEvents lblOthernamemandatory As System.Windows.Forms.Label
    Friend WithEvents rbRequestorProvider As System.Windows.Forms.RadioButton
    Friend WithEvents CmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents txtAmedmentScan As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblAmedmentScan As System.Windows.Forms.Label
End Class
