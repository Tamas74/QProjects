<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReviewOfHistory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then

            Try
                If (IsNothing(dtReviewDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtReviewDate)
                    Catch ex As Exception

                    End Try


                    dtReviewDate.Dispose()
                    dtReviewDate = Nothing
                End If
            Catch
            End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReviewOfHistory))
        Me.lblUserName = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.lblReviwDate = New System.Windows.Forms.Label
        Me.dtReviewDate = New System.Windows.Forms.DateTimePicker
        Me.lblComment = New System.Windows.Forms.Label
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tlsp_ReviewofHistory = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_ReviewofHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.Location = New System.Drawing.Point(28, 22)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(74, 14)
        Me.lblUserName.TabIndex = 0
        Me.lblUserName.Text = "User Name :"
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.White
        Me.txtUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(105, 18)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(226, 22)
        Me.txtUserName.TabIndex = 0
        '
        'lblReviwDate
        '
        Me.lblReviwDate.AutoSize = True
        Me.lblReviwDate.BackColor = System.Drawing.Color.Transparent
        Me.lblReviwDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReviwDate.Location = New System.Drawing.Point(18, 55)
        Me.lblReviwDate.Name = "lblReviwDate"
        Me.lblReviwDate.Size = New System.Drawing.Size(84, 14)
        Me.lblReviwDate.TabIndex = 3
        Me.lblReviwDate.Text = "Review Date :"
        '
        'dtReviewDate
        '
        Me.dtReviewDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtReviewDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtReviewDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtReviewDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtReviewDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtReviewDate.CustomFormat = "MM/dd/yyyy"
        Me.dtReviewDate.Enabled = False
        Me.dtReviewDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReviewDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtReviewDate.Location = New System.Drawing.Point(105, 51)
        Me.dtReviewDate.Name = "dtReviewDate"
        Me.dtReviewDate.Size = New System.Drawing.Size(172, 22)
        Me.dtReviewDate.TabIndex = 1
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.BackColor = System.Drawing.Color.Transparent
        Me.lblComment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComment.Location = New System.Drawing.Point(34, 88)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(68, 14)
        Me.lblComment.TabIndex = 5
        Me.lblComment.Text = "Comment :"
        '
        'txtComment
        '
        Me.txtComment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(105, 88)
        Me.txtComment.MaxLength = 255
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(226, 61)
        Me.txtComment.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Controls.Add(Me.dtReviewDate)
        Me.Panel2.Controls.Add(Me.lblUserName)
        Me.Panel2.Controls.Add(Me.txtComment)
        Me.Panel2.Controls.Add(Me.txtUserName)
        Me.Panel2.Controls.Add(Me.lblComment)
        Me.Panel2.Controls.Add(Me.lblReviwDate)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(348, 163)
        Me.Panel2.TabIndex = 7
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 159)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(340, 1)
        Me.lbl_BottomBrd.TabIndex = 9
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 156)
        Me.lbl_LeftBrd.TabIndex = 8
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(344, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 156)
        Me.lbl_RightBrd.TabIndex = 7
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(342, 1)
        Me.lbl_TopBrd.TabIndex = 6
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_ReviewofHistory)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(348, 53)
        Me.pnl_tlspTOP.TabIndex = 8
        '
        'tlsp_ReviewofHistory
        '
        Me.tlsp_ReviewofHistory.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ReviewofHistory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_ReviewofHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ReviewofHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_ReviewofHistory.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ReviewofHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_ReviewofHistory.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ReviewofHistory.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_ReviewofHistory.Name = "tlsp_ReviewofHistory"
        Me.tlsp_ReviewofHistory.Size = New System.Drawing.Size(348, 53)
        Me.tlsp_ReviewofHistory.TabIndex = 0
        Me.tlsp_ReviewofHistory.Text = "toolStrip1"
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
        'frmReviewOfHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(348, 216)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReviewOfHistory"
        Me.Text = "Review of History"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_ReviewofHistory.ResumeLayout(False)
        Me.tlsp_ReviewofHistory.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents lblReviwDate As System.Windows.Forms.Label
    Friend WithEvents dtReviewDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_ReviewofHistory As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
End Class
