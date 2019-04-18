<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRxFillNotification
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmRxFillNotification

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                Try


                Catch ex As Exception

                End Try
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal PatientId As Int64, ByVal PrescriptionID As Int64, ByVal dtVisitDate As Date) As frmRxFillNotification
        If frm Is Nothing Then
            frm = New frmRxFillNotification(PatientId, PrescriptionID, dtVisitDate)
        End If

        Return frm
    End Function

    Public Shared Function GetInstance(ByVal MessageID As String, ByVal dtVisitDate As Date) As frmRxFillNotification
        If frm Is Nothing Then
            frm = New frmRxFillNotification(MessageID, dtVisitDate)
        End If

        Return frm
    End Function
#End Region

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRxFillNotification))
        Me.pnl_toolstrip = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gloRxFillNotification = New gloUserControlLibrary.gloRxFillNotification()
        Me.pnl_toolstrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_toolstrip
        '
        Me.pnl_toolstrip.AutoSize = True
        Me.pnl_toolstrip.Controls.Add(Me.ToolStrip1)
        Me.pnl_toolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_toolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_toolstrip.Name = "pnl_toolstrip"
        Me.pnl_toolstrip.Size = New System.Drawing.Size(1164, 53)
        Me.pnl_toolstrip.TabIndex = 6
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1164, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh Pending RxFill Notifications"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.gloRxFillNotification)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1164, 634)
        Me.pnlMain.TabIndex = 242
        '
        'gloRxFillNotification
        '
        Me.gloRxFillNotification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloRxFillNotification.dtRxFillReqs = Nothing
        Me.gloRxFillNotification.Location = New System.Drawing.Point(3, 3)
        Me.gloRxFillNotification.MessageID = Nothing
        Me.gloRxFillNotification.Name = "gloRxFillNotification"
        Me.gloRxFillNotification.PatientID = CType(0, Long)
        Me.gloRxFillNotification.PrescriberOrderNumber = CType(0, Long)
        Me.gloRxFillNotification.rxRequestMsgID = ""
        Me.gloRxFillNotification.Size = New System.Drawing.Size(1158, 628)
        Me.gloRxFillNotification.SSMessageData = Nothing
        Me.gloRxFillNotification.SSRxFillRequest = Nothing
        Me.gloRxFillNotification.TabIndex = 0
        '
        'frmRxFillNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1164, 687)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_toolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRxFillNotification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RxFill Notifications"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnl_toolstrip.ResumeLayout(False)
        Me.pnl_toolstrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnl_toolstrip As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    'Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents gloRxFillNotification As gloUserControlLibrary.gloRxFillNotification
End Class
