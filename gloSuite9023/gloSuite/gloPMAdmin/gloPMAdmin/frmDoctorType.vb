Imports gloEMR.gloStreamAdmin
Public Class frmDoctorType
    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDoctorType As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDoctorType))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtDoctorType = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.txtDoctorType)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(372, 65)
        Me.pnlMain.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(364, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 58)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(368, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 58)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(366, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'txtDoctorType
        '
        Me.txtDoctorType.ForeColor = System.Drawing.Color.Black
        Me.txtDoctorType.Location = New System.Drawing.Point(119, 21)
        Me.txtDoctorType.Name = "txtDoctorType"
        Me.txtDoctorType.Size = New System.Drawing.Size(221, 22)
        Me.txtDoctorType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(32, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Doctor Type :"
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(372, 56)
        Me.pnl_tlsp_Top.TabIndex = 1
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(372, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "Sa&ve&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'frmDoctorType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(372, 121)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoctorType"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Provider Type"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDoctorType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDoctorType.Focus()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Unable to close the doctor type due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Trim(txtDoctorType.Text) = "" Then
                MessageBox.Show("Enter doctor type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDoctorType.Focus()
                Exit Sub
            End If
            Dim objProviderType As New clsProviderType(gstrConnectionString)
            'Check Provider already exists or not
            If blnModify = True Then
                If objProviderType.CheckProviderTypeExists(txtDoctorType.Text, txtDoctorType.Tag) = True Then
                    MessageBox.Show("Dcotor type already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objProviderType = Nothing
                    txtDoctorType.Focus()
                    Exit Sub
                End If
            Else
                If objProviderType.CheckProviderTypeExists(txtDoctorType.Text) = True Then
                    MessageBox.Show("Dcotor type already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objProviderType = Nothing
                    txtDoctorType.Focus()
                    Exit Sub
                End If
            End If

            objProviderType.ProviderType = Trim(txtDoctorType.Text)

            gstrCategory = Trim(txtDoctorType.Text)
            If blnModify = True Then
                If objProviderType.UpdateProviderType(txtDoctorType.Tag, txtDoctorType.Text) = True Then
                    UpdateErrorLog("Doctor type successfully modified", mdlFunctions.enmErrorOccuredForm.DoctorType, mdlFunctions.enmOperation.Update)
                    objProviderType = Nothing
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Unable to update doctor type details due to " & objProviderType.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UpdateErrorLog("Unable to update doctor type details due to " & objProviderType.ErrorMessage, mdlFunctions.enmErrorOccuredForm.DoctorType, mdlFunctions.enmOperation.Update, True)
                    objProviderType = Nothing
                End If
            Else
                If objProviderType.InsertProviderType = True Then
                    objProviderType = Nothing
                    UpdateErrorLog("Doctor type successfully modified", mdlFunctions.enmErrorOccuredForm.DoctorType, mdlFunctions.enmOperation.Add)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Unable to insert doctor type details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UpdateErrorLog("Unable to add doctor type due to " & objProviderType.ErrorMessage, mdlFunctions.enmErrorOccuredForm.DoctorType, mdlFunctions.enmOperation.Add, True)
                    objProviderType = Nothing
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Unable to add/update doctor type details due to " & objErr.ToString, mdlFunctions.enmErrorOccuredForm.DoctorType, mdlFunctions.enmOperation.Others, True)
        End Try

    End Sub
End Class
