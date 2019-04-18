Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms


Partial Public Class frmAutoFollowupUtility
    Inherits Form
    Private _databaseconnectionstring As String
    Private _ClinicID As Int64 = 1
    Private _messageBoxCaption As String
    Private dtAccounts As New DataTable()
    Private dtClaims As New DataTable()
    Dim _StmtCountSet As Integer = 0
    Dim _days As Integer = 0
    Dim _action As String = String.Empty
    Dim _actionDescription As String = String.Empty

    Dim _billdays As Integer = 0
    Dim _billaction As String = String.Empty
    Dim _billactionDescription As String = String.Empty

    Dim _rebilldays As Integer = 0
    Dim _rebillaction As String = String.Empty
    Dim _rebillactionDescription As String = String.Empty
    ''7022Itens: Claim queue reset utility
    ''add new panel is added to show/hide panel according to user selection.
    Friend WithEvents pnlClaimDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlAccountDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlHeaderDetails As System.Windows.Forms.Panel

    Dim _selectedFollowUp As String = String.Empty
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString

        _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption
    End Sub
    ''7022Itens: Claim queue reset utility
    ''add new code to calcuate/recalculate follow-up according to user selection.
    Public Sub New(ByVal _FollowUp As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString

        _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption
        _selectedFollowUp = _FollowUp
    End Sub




    Public Sub New(ByVal StmtCountSet As Integer, ByVal days As Integer, ByVal action As String, ByVal actionDescription As String, ByVal billdays As Integer, ByVal billaction As String, ByVal billactionDescription As String, _
     ByVal rebilldays As Integer, ByVal rebillaction As String, ByVal rebillactionDescription As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString

        _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption

        _StmtCountSet = StmtCountSet
        _days = days
        _action = action
        _actionDescription = actionDescription


        billdays = _billdays
        billaction = _billaction
        billactionDescription = _billactionDescription

        rebilldays = _rebilldays
        rebillaction = _rebillaction
        rebillactionDescription = _rebillactionDescription



    End Sub

    Private Sub GetDataToProcess()

        Dim FollowUpUtility As New clsFollowUpUtility()
        dtAccounts = FollowUpUtility.GetAllAccounts()
        'ProcessAccounts(dtAccounts);         
    End Sub




    Private Sub ProcessAccounts(ByVal dtAccounts As DataTable)

        If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
            Dim FollowUpUtility As New clsFollowUpUtility()
            Dim dtSettings As DataTable = FollowUpUtility.GetDefaultAccFollowupSetting()
            Dim StmtCountSet As Integer = 0
            Dim days As Integer = 0
            Dim action As String = String.Empty
            Dim actionDescription As String = String.Empty

            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                StmtCountSet = Convert.ToInt32(dtSettings.Rows(0)(0))
                days = Convert.ToInt32(dtSettings.Rows(0)(1))
                action = dtSettings.Rows(0)(2).ToString()
                actionDescription = dtSettings.Rows(0)(3).ToString()
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim count As Int64 = dtAccounts.Rows.Count
            For i As Integer = 0 To dtAccounts.Rows.Count - 1
                lblStatus.Text = "Processing Account " & (i + 1).ToString() & " out of " & count.ToString()
                FollowUpUtility.ProcessAccount(Convert.ToInt64(dtAccounts.Rows(i)(0).ToString()), StmtCountSet, days, action, actionDescription)
                ProgressBar.Value = i + 1
                Application.DoEvents()

            Next
        End If
        lblAccountStatus.Text = "Completed"
        Cursor.Current = Cursors.[Default]
        ''Bug #45558: gloPM - V7022 - Button appears in the Utility when control is shifted for recalculate follow up 
        ''Set btnAccount.visible= true
        btnAccount.Visible = True
        btnAccount.Image = gloPMAdmin.My.Resources.Resources.tick
        Application.DoEvents()
        ''7022Itens: Claim queue reset utility
        ''add new code to calcuate/recalculate follow-up according to user selection.
        If _selectedFollowUp = "Both" Then
            GetClaimData()
        End If
        ''7022Itens: Claim queue reset utility
        ''commented old code.
        'GetClaimData()
    End Sub
    Private Sub ProcessClaims(ByVal dtClaims As DataTable)
        If dtClaims IsNot Nothing AndAlso dtClaims.Rows.Count > 0 Then
            Dim FollowUpUtility As New clsFollowUpUtility()
            Dim dtSettings As DataTable = FollowUpUtility.GetDefaultClaimsFollowupSetting()

            Dim billdays As Integer = 0
            Dim billaction As String = String.Empty
            Dim billactionDescription As String = String.Empty

            Dim rebilldays As Integer = 0
            Dim rebillaction As String = String.Empty
            Dim rebillactionDescription As String = String.Empty

            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                billdays = Convert.ToInt32(dtSettings.Rows(0)(0))
                billaction = dtSettings.Rows(0)(1).ToString()
                billactionDescription = dtSettings.Rows(0)(2).ToString()

                rebilldays = Convert.ToInt32(dtSettings.Rows(0)(3))
                rebillaction = dtSettings.Rows(0)(4).ToString()
                rebillactionDescription = dtSettings.Rows(0)(5).ToString()
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim count As Int64 = dtClaims.Rows.Count
            For i As Integer = 0 To dtClaims.Rows.Count - 1
                lblStatus.Text = "Processing Claim " & (i + 1).ToString() & " out of " & count.ToString()
                FollowUpUtility.ProcessClaim(Convert.ToInt64(dtClaims.Rows(i)(0).ToString()), Convert.ToInt64(dtClaims.Rows(i)(1).ToString()), Convert.ToBoolean(dtClaims.Rows(i)(2).ToString()), billdays, billaction, billactionDescription, _
                 rebilldays, rebillaction, rebillactionDescription)
                ProgressBar.Value = i + 1
                Application.DoEvents()

            Next
        End If
        lblClaimStatus.Text = "Completed"
        ''Bug #45558: gloPM - V7022 - Button appears in the Utility when control is shifted for recalculate follow up 
        ''Set btnClaim.visible= true
        btnClaim.Visible = True
        btnClaim.Image = gloPMAdmin.My.Resources.Resources.tick
        Application.DoEvents()
    End Sub

    Private Sub GetClaimData()
        lblClaimStatus.Text = "In Progess"
        ProgressBar.Maximum = 0
        Dim FollowUpUtility As New clsFollowUpUtility()
        dtClaims = FollowUpUtility.GetAllClaims()
        If dtClaims IsNot Nothing AndAlso dtClaims.Rows.Count > 0 Then
            ProgressBar.Maximum = dtClaims.Rows.Count
            lblStatus.Text = "Processing Claims"
            ProcessClaims(dtClaims)
            ''7022Itens: Claim queue reset utility
            ''add new code to calcuate/recalculate follow-up according to user selection.
            If _selectedFollowUp = "Both" Then
                FinishProcess()
            End If

            'FinishProcess()
        End If
    End Sub

    Private Sub FinishProcess()
        Cursor.Current = Cursors.[Default]
        AddSettingToDB("CALCULATEFOLLOWUP", "true", _ClinicID, gnLoginID, SettingFlag.Clinic)
        ''7022Itens: Claim queue reset utility
        ''commented old code.
        'System.Threading.Thread.Sleep(2000)
        'Me.Close()
    End Sub

    Private Sub frmAutoFollowupUtility_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        ''7022Itens: Claim queue reset utility
        ''commented old code.
        'GetDataToProcess()
        'If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
        '    ProgressBar.Maximum = dtAccounts.Rows.Count
        '    lblStatus.Text = "Processing Accounts"
        'End If
        'ProcessAccounts(dtAccounts)

        ''7022Itens: Claim queue reset utility
        ''add new code to calcuate/recalculate follow-up according to user selection.
        Try
            If _selectedFollowUp = "Account" Then
                GetDataToProcess()
                If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
                    ProgressBar.Maximum = dtAccounts.Rows.Count
                    lblStatus.Text = "Processing Accounts"
                End If
                ProcessAccounts(dtAccounts)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.CalculateFollowUp, "Recalculating Account follow-up", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin)
            ElseIf _selectedFollowUp = "Claim" Then
                GetClaimData()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.CalculateFollowUp, "Recalculating Claim follow-up", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin)
            Else
                GetDataToProcess()
                If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
                    ProgressBar.Maximum = dtAccounts.Rows.Count
                    lblStatus.Text = "Processing Accounts"
                End If
                ProcessAccounts(dtAccounts)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.CalculateFollowUp, "Calculating Account and Claim follow-up ", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPMAdmin)
            End If
            System.Threading.Thread.Sleep(2000)
        Catch ex As Exception
            MessageBox.Show("Exception while Calculating follow-up: " + ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.FollowUp, gloAuditTrail.ActivityType.CalculateFollowUp, "Exception while Calculating followup " + ex.Message, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPMAdmin)
        Finally
            Me.Close()
        End Try

    End Sub

    Private Sub frmAutoFollowupUtility_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ''7022Itens: Claim queue reset utility
        ''commented old code.
        'lblAccount.Focus()
        ''7022Itens: Claim queue reset utility
        ''add new code to calcuate/recalculate follow-up according to user selection.
        If _selectedFollowUp = "Account" Then
            lblAccount.Focus()
            pnlClaimDetails.Visible = False
            ''Bug #45558: gloPM - V7022 - Button appears in the Utility when control is shifted for recalculate follow up 
            ''Set btnAccount.visible= false
            btnAccount.Visible = False
            pnlAccountDetails.BringToFront()
            pnlClaimDetails.SendToBack()
            Me.Height = 136
            Me.Width = 560
        ElseIf _selectedFollowUp = "Claim" Then
            lblClaim.Focus()
            pnlAccountDetails.Visible = False
            ''Bug #45558: gloPM - V7022 - Button appears in the Utility when control is shifted for recalculate follow up 
            ''Set btnClaim.visible= false
            btnClaim.Visible = False
            pnlClaimDetails.Dock = DockStyle.Top
            pnlClaimDetails.BringToFront()
            pnlAccountDetails.SendToBack()
            Me.Height = 136
            Me.Width = 560
        Else
            lblAccount.Focus()
            ''Bug #45558: gloPM - V7022 - Button appears in the Utility when control is shifted for recalculate follow up 
            ''Set btnAccount.visible= false, Set btnClaim.visible= false
            btnAccount.Visible = False
            btnClaim.Visible = False
            Me.Height = 155
            Me.Width = 560
        End If

    End Sub
    Public Function AddSettingToDB(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Execute("gsp_InUpSettings", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function
    Private WithEvents btnClaim As System.Windows.Forms.Button
    Private WithEvents btnAccount As System.Windows.Forms.Button
    Private WithEvents lblClaimStatus As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Private WithEvents CPTStatus As System.Windows.Forms.StatusStrip
    Private WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents lblAccountStatus As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents lblClaim As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents lblAccount As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel

    Private Sub InitializeComponent()
        Me.btnClaim = New System.Windows.Forms.Button()
        Me.btnAccount = New System.Windows.Forms.Button()
        Me.lblClaimStatus = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.label8 = New System.Windows.Forms.Label()
        Me.CPTStatus = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.lblAccountStatus = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.lblClaim = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.pnlAccountDetails = New System.Windows.Forms.Panel()
        Me.pnlClaimDetails = New System.Windows.Forms.Panel()
        Me.pnlHeaderDetails = New System.Windows.Forms.Panel()
        Me.panel6.SuspendLayout()
        Me.CPTStatus.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.pnlAccountDetails.SuspendLayout()
        Me.pnlClaimDetails.SuspendLayout()
        Me.pnlHeaderDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClaim
        '
        Me.btnClaim.FlatAppearance.BorderSize = 0
        Me.btnClaim.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClaim.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClaim.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClaim.Location = New System.Drawing.Point(12, 2)
        Me.btnClaim.Name = "btnClaim"
        Me.btnClaim.Size = New System.Drawing.Size(17, 19)
        Me.btnClaim.TabIndex = 34
        Me.btnClaim.UseVisualStyleBackColor = True
        '
        'btnAccount
        '
        Me.btnAccount.FlatAppearance.BorderSize = 0
        Me.btnAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAccount.Location = New System.Drawing.Point(11, 1)
        Me.btnAccount.Name = "btnAccount"
        Me.btnAccount.Size = New System.Drawing.Size(17, 19)
        Me.btnAccount.TabIndex = 33
        Me.btnAccount.UseVisualStyleBackColor = True
        '
        'lblClaimStatus
        '
        Me.lblClaimStatus.AutoSize = True
        Me.lblClaimStatus.Location = New System.Drawing.Point(227, 5)
        Me.lblClaimStatus.Name = "lblClaimStatus"
        Me.lblClaimStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblClaimStatus.TabIndex = 32
        '
        'label6
        '
        Me.label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.label6.Location = New System.Drawing.Point(550, 4)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(1, 72)
        Me.label6.TabIndex = 29
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.label5.Location = New System.Drawing.Point(3, 4)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(1, 72)
        Me.label5.TabIndex = 28
        '
        'panel6
        '
        Me.panel6.BackColor = System.Drawing.Color.Transparent
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Controls.Add(Me.label8)
        Me.panel6.Controls.Add(Me.CPTStatus)
        Me.panel6.Controls.Add(Me.label1)
        Me.panel6.Controls.Add(Me.label7)
        Me.panel6.Controls.Add(Me.label9)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel6.Location = New System.Drawing.Point(0, 77)
        Me.panel6.Name = "panel6"
        Me.panel6.Padding = New System.Windows.Forms.Padding(3)
        Me.panel6.Size = New System.Drawing.Size(554, 50)
        Me.panel6.TabIndex = 32
        '
        'label8
        '
        Me.label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.label8.Location = New System.Drawing.Point(4, 3)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(546, 1)
        Me.label8.TabIndex = 31
        '
        'CPTStatus
        '
        Me.CPTStatus.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.CPTStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CPTStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.ProgressBar})
        Me.CPTStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.CPTStatus.Location = New System.Drawing.Point(4, 3)
        Me.CPTStatus.Name = "CPTStatus"
        Me.CPTStatus.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.CPTStatus.Size = New System.Drawing.Size(546, 43)
        Me.CPTStatus.TabIndex = 21
        Me.CPTStatus.Text = "statusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(119, 14)
        Me.lblStatus.Text = "Precessing Accounts"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(542, 18)
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.label1.Location = New System.Drawing.Point(3, 3)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 43)
        Me.label1.TabIndex = 29
        '
        'label7
        '
        Me.label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.label7.Location = New System.Drawing.Point(550, 3)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(1, 43)
        Me.label7.TabIndex = 30
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(3, 46)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(548, 1)
        Me.label9.TabIndex = 32
        '
        'lblAccountStatus
        '
        Me.lblAccountStatus.AutoSize = True
        Me.lblAccountStatus.Location = New System.Drawing.Point(226, 4)
        Me.lblAccountStatus.Name = "lblAccountStatus"
        Me.lblAccountStatus.Size = New System.Drawing.Size(64, 14)
        Me.lblAccountStatus.TabIndex = 31
        Me.lblAccountStatus.Text = "In Progess"
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.Location = New System.Drawing.Point(241, 5)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(48, 14)
        Me.label10.TabIndex = 30
        Me.label10.Text = "Status"
        '
        'lblClaim
        '
        Me.lblClaim.AutoSize = True
        Me.lblClaim.Location = New System.Drawing.Point(34, 5)
        Me.lblClaim.Name = "lblClaim"
        Me.lblClaim.Size = New System.Drawing.Size(90, 14)
        Me.lblClaim.TabIndex = 28
        Me.lblClaim.Text = "Claim Follow-up"
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label4.Location = New System.Drawing.Point(3, 76)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(548, 1)
        Me.label4.TabIndex = 27
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.label3.Location = New System.Drawing.Point(3, 3)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(548, 1)
        Me.label3.TabIndex = 26
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(33, 4)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(109, 14)
        Me.lblAccount.TabIndex = 26
        Me.lblAccount.Text = "Account Follow-up"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(48, 5)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 14)
        Me.label2.TabIndex = 25
        Me.label2.Text = "Action Item"
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel1.Controls.Add(Me.pnlAccountDetails)
        Me.panel1.Controls.Add(Me.pnlClaimDetails)
        Me.panel1.Controls.Add(Me.pnlHeaderDetails)
        Me.panel1.Controls.Add(Me.label6)
        Me.panel1.Controls.Add(Me.label5)
        Me.panel1.Controls.Add(Me.label4)
        Me.panel1.Controls.Add(Me.label3)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.panel1.Size = New System.Drawing.Size(554, 77)
        Me.panel1.TabIndex = 33
        '
        'pnlAccountDetails
        '
        Me.pnlAccountDetails.Controls.Add(Me.btnAccount)
        Me.pnlAccountDetails.Controls.Add(Me.lblAccount)
        Me.pnlAccountDetails.Controls.Add(Me.lblAccountStatus)
        Me.pnlAccountDetails.Location = New System.Drawing.Point(17, 27)
        Me.pnlAccountDetails.Name = "pnlAccountDetails"
        Me.pnlAccountDetails.Size = New System.Drawing.Size(347, 22)
        Me.pnlAccountDetails.TabIndex = 35
        '
        'pnlClaimDetails
        '
        Me.pnlClaimDetails.Controls.Add(Me.btnClaim)
        Me.pnlClaimDetails.Controls.Add(Me.lblClaim)
        Me.pnlClaimDetails.Controls.Add(Me.lblClaimStatus)
        Me.pnlClaimDetails.Location = New System.Drawing.Point(17, 52)
        Me.pnlClaimDetails.Name = "pnlClaimDetails"
        Me.pnlClaimDetails.Size = New System.Drawing.Size(347, 22)
        Me.pnlClaimDetails.TabIndex = 36
        '
        'pnlHeaderDetails
        '
        Me.pnlHeaderDetails.Controls.Add(Me.label2)
        Me.pnlHeaderDetails.Controls.Add(Me.label10)
        Me.pnlHeaderDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeaderDetails.Location = New System.Drawing.Point(4, 4)
        Me.pnlHeaderDetails.Name = "pnlHeaderDetails"
        Me.pnlHeaderDetails.Size = New System.Drawing.Size(546, 21)
        Me.pnlHeaderDetails.TabIndex = 37
        '
        'frmAutoFollowupUtility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(554, 127)
        Me.ControlBox = False
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.panel6)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmAutoFollowupUtility"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Auto Follow-up Utility"
        Me.panel6.ResumeLayout(False)
        Me.panel6.PerformLayout()
        Me.CPTStatus.ResumeLayout(False)
        Me.CPTStatus.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.pnlAccountDetails.ResumeLayout(False)
        Me.pnlAccountDetails.PerformLayout()
        Me.pnlClaimDetails.ResumeLayout(False)
        Me.pnlClaimDetails.PerformLayout()
        Me.pnlHeaderDetails.ResumeLayout(False)
        Me.pnlHeaderDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents label8 As System.Windows.Forms.Label
End Class

