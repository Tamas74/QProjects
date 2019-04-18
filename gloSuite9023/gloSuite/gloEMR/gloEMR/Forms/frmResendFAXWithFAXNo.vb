Imports gloAuditTrail

Public Class frmResendFAXWithFAXNo
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblFAXType As System.Windows.Forms.Label
    Friend WithEvents lblOldFAXNo As System.Windows.Forms.Label
    Friend WithEvents lblFAXTo As System.Windows.Forms.Label
    Friend WithEvents lblFAXID As System.Windows.Forms.Label
    Friend WithEvents txtNewFAXNo As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_ResendFax As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResendFAXWithFAXNo))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.lblFAXID = New System.Windows.Forms.Label
        Me.txtNewFAXNo = New System.Windows.Forms.TextBox
        Me.lblFAXTo = New System.Windows.Forms.Label
        Me.lblOldFAXNo = New System.Windows.Forms.Label
        Me.lblFAXType = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tlsp_ResendFax = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_ResendFax.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMain.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMain.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMain.Controls.Add(Me.lblFAXID)
        Me.pnlMain.Controls.Add(Me.txtNewFAXNo)
        Me.pnlMain.Controls.Add(Me.lblFAXTo)
        Me.pnlMain.Controls.Add(Me.lblOldFAXNo)
        Me.pnlMain.Controls.Add(Me.lblFAXType)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(358, 151)
        Me.pnlMain.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 147)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(350, 1)
        Me.lbl_BottomBrd.TabIndex = 12
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 144)
        Me.lbl_LeftBrd.TabIndex = 11
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(354, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 144)
        Me.lbl_RightBrd.TabIndex = 10
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(352, 1)
        Me.lbl_TopBrd.TabIndex = 9
        Me.lbl_TopBrd.Text = "label1"
        '
        'lblFAXID
        '
        Me.lblFAXID.AutoSize = True
        Me.lblFAXID.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXID.Location = New System.Drawing.Point(289, 19)
        Me.lblFAXID.Name = "lblFAXID"
        Me.lblFAXID.Size = New System.Drawing.Size(38, 14)
        Me.lblFAXID.TabIndex = 8
        Me.lblFAXID.Text = "Fax id"
        Me.lblFAXID.Visible = False
        '
        'txtNewFAXNo
        '
        Me.txtNewFAXNo.ForeColor = System.Drawing.Color.Black
        Me.txtNewFAXNo.Location = New System.Drawing.Point(108, 111)
        Me.txtNewFAXNo.Name = "txtNewFAXNo"
        Me.txtNewFAXNo.Size = New System.Drawing.Size(219, 22)
        Me.txtNewFAXNo.TabIndex = 8
        '
        'lblFAXTo
        '
        Me.lblFAXTo.AutoSize = True
        Me.lblFAXTo.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXTo.Location = New System.Drawing.Point(108, 19)
        Me.lblFAXTo.Name = "lblFAXTo"
        Me.lblFAXTo.Size = New System.Drawing.Size(48, 14)
        Me.lblFAXTo.TabIndex = 7
        Me.lblFAXTo.Text = "Fax To "
        '
        'lblOldFAXNo
        '
        Me.lblOldFAXNo.AutoSize = True
        Me.lblOldFAXNo.BackColor = System.Drawing.Color.Transparent
        Me.lblOldFAXNo.Location = New System.Drawing.Point(108, 83)
        Me.lblOldFAXNo.Name = "lblOldFAXNo"
        Me.lblOldFAXNo.Size = New System.Drawing.Size(48, 14)
        Me.lblOldFAXNo.TabIndex = 6
        Me.lblOldFAXNo.Text = "Fax To "
        '
        'lblFAXType
        '
        Me.lblFAXType.AutoSize = True
        Me.lblFAXType.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXType.Location = New System.Drawing.Point(108, 51)
        Me.lblFAXType.Name = "lblFAXType"
        Me.lblFAXType.Size = New System.Drawing.Size(48, 14)
        Me.lblFAXType.TabIndex = 5
        Me.lblFAXType.Text = "Fax To "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(38, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Fax Type :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(22, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "New Fax No :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(29, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Old Fax No :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(51, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fax To :"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_ResendFax)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(358, 53)
        Me.pnl_tlspTOP.TabIndex = 10
        '
        'tlsp_ResendFax
        '
        Me.tlsp_ResendFax.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ResendFax.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_ResendFax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ResendFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_ResendFax.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ResendFax.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_ResendFax.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ResendFax.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_ResendFax.Name = "tlsp_ResendFax"
        Me.tlsp_ResendFax.Size = New System.Drawing.Size(358, 53)
        Me.tlsp_ResendFax.TabIndex = 0
        Me.tlsp_ResendFax.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmResendFAXWithFAXNo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(358, 204)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmResendFAXWithFAXNo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resend Fax"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_ResendFax.ResumeLayout(False)
        Me.tlsp_ResendFax.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CancelResendFax()
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.None, ActivityCategory.None, ActivityType.Close, objErr.ToString(), ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OkResendFax()
        Try
            If Trim(txtNewFAXNo.Text) = "" Then
                MessageBox.Show("Please enter new FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNewFAXNo.Focus()
                Exit Sub
            End If
            Dim objFAX As New clsFAX
            objFAX.ReInitialisePendingFAX(lblFAXID.Text, Trim(txtNewFAXNo.Text))
            objFAX = Nothing
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.None, ActivityCategory.None, ActivityType.Fax, objErr.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tlsp_ResendFax_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ResendFax.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OkResendFax()
                Case "Cancel"
                    CancelResendFax()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
