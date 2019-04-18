Imports gloEMR.gloStream.gloAlert

Public Class frmPatientAlerts
    Inherits System.Windows.Forms.Form
    Private _PatientID As Long
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlAlerts As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_PatientAlerts As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents txtAlertDescription As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientAlerts))
        Me.pnlAlerts = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.cmbStatus = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAlertDescription = New System.Windows.Forms.TextBox
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_PatientAlerts = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlAlerts.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_PatientAlerts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlAlerts
        '
        Me.pnlAlerts.BackColor = System.Drawing.Color.Transparent
        Me.pnlAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAlerts.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlAlerts.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlAlerts.Controls.Add(Me.lbl_pnlRight)
        Me.pnlAlerts.Controls.Add(Me.lbl_pnlTop)
        Me.pnlAlerts.Controls.Add(Me.cmbStatus)
        Me.pnlAlerts.Controls.Add(Me.Label2)
        Me.pnlAlerts.Controls.Add(Me.Label1)
        Me.pnlAlerts.Controls.Add(Me.txtAlertDescription)
        Me.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAlerts.Location = New System.Drawing.Point(0, 53)
        Me.pnlAlerts.Name = "pnlAlerts"
        Me.pnlAlerts.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlAlerts.Size = New System.Drawing.Size(387, 131)
        Me.pnlAlerts.TabIndex = 1
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 127)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(379, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 124)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(383, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 124)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(381, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.White
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.Location = New System.Drawing.Point(91, 94)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(144, 22)
        Me.cmbStatus.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(38, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Status :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(13, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Description :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAlertDescription
        '
        Me.txtAlertDescription.BackColor = System.Drawing.Color.White
        Me.txtAlertDescription.ForeColor = System.Drawing.Color.Black
        Me.txtAlertDescription.Location = New System.Drawing.Point(91, 13)
        Me.txtAlertDescription.MaxLength = 255
        Me.txtAlertDescription.Multiline = True
        Me.txtAlertDescription.Name = "txtAlertDescription"
        Me.txtAlertDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAlertDescription.Size = New System.Drawing.Size(280, 73)
        Me.txtAlertDescription.TabIndex = 0
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_PatientAlerts)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(387, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_PatientAlerts
        '
        Me.tlsp_PatientAlerts.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientAlerts.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientAlerts.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientAlerts.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientAlerts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_PatientAlerts.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientAlerts.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientAlerts.Name = "tlsp_PatientAlerts"
        Me.tlsp_PatientAlerts.Size = New System.Drawing.Size(387, 53)
        Me.tlsp_PatientAlerts.TabIndex = 0
        Me.tlsp_PatientAlerts.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmPatientAlerts
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(387, 184)
        Me.Controls.Add(Me.pnlAlerts)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientAlerts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Alerts"
        Me.pnlAlerts.ResumeLayout(False)
        Me.pnlAlerts.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_PatientAlerts.ResumeLayout(False)
        Me.tlsp_PatientAlerts.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPatientAlerts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Fill_Status()
            Call Get_PatientDetails()
            Dim oclsAlert As New gloStream.gloAlert.Alert
            Dim ColAlert As New Collection
            ColAlert = oclsAlert.GetAlert(strPatientCode, gloStream.gloAlert.Alert.Alert_Type.General)

            If ColAlert.Count > 0 Then
                txtAlertDescription.Text = ColAlert.Item(1).ToString
                cmbStatus.Text = oclsAlert.StausToString(ColAlert.Item(2))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   objclsAddendum = Nothing
        End Try
    End Sub
    Private Sub Fill_Status()
        cmbStatus.Items.AddRange([Enum].GetNames(GetType(gloStream.gloAlert.Alert.Alert_Status)))
        cmbStatus.SelectedIndex = 0
    End Sub

    Private Sub SavePatientAlert()

        Dim cls As New Alert
        Dim strAlert As String

        Try

            '16-Nov-15 Aniket: Resolving Bug #91280: gloEMR: EMR Alerts: Application should not add empty EMR alerts
            strAlert = Trim(txtAlertDescription.Text.Replace(vbCr, "").Replace(vbLf, "").Replace(vbTab, ""))

            If strAlert <> "" Then
                With cls
                    If .SetAlert(strPatientCode, Alert.Alert_Type.General, txtAlertDescription.Text, .StausAsStatus(cmbStatus.Text)) = True Then
                        Me.Close()
                    Else
                        Exit Sub
                    End If
                End With
            Else
                MessageBox.Show("You must enter the alerts description.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtAlertDescription.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            cls = Nothing
        End Try

    End Sub

    Private Sub tlsp_PatientAlerts_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientAlerts.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SavePatientAlert()

                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            'dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
        Return Nothing
    End Function
End Class
