Public Class frmMessageHistory
    Inherits System.Windows.Forms.Form

    Private _MessageID As Long

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal MessageID As Long)
        MyBase.New()
        _MessageID = MessageID

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
            Try

                If (IsNothing(grdMessages) = False) Then
                    grdMessages.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdMessages)
                    grdMessages.Dispose()
                    grdMessages = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents grdMessages As System.Windows.Forms.DataGrid
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstripDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMessageHistory))
        Me.pnlGrid = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.grdMessages = New System.Windows.Forms.DataGrid
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstripDiagnosis = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlGrid.SuspendLayout()
        CType(Me.grdMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripDiagnosis.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.Label5)
        Me.pnlGrid.Controls.Add(Me.Label6)
        Me.pnlGrid.Controls.Add(Me.Label7)
        Me.pnlGrid.Controls.Add(Me.Label8)
        Me.pnlGrid.Controls.Add(Me.grdMessages)
        Me.pnlGrid.Location = New System.Drawing.Point(0, 56)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(426, 152)
        Me.pnlGrid.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(418, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 148)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(422, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 148)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(420, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'grdMessages
        '
        Me.grdMessages.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdMessages.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdMessages.BackgroundColor = System.Drawing.Color.White
        Me.grdMessages.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdMessages.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdMessages.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMessages.CaptionForeColor = System.Drawing.Color.White
        Me.grdMessages.CaptionVisible = False
        Me.grdMessages.DataMember = ""
        Me.grdMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMessages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdMessages.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdMessages.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdMessages.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMessages.HeaderForeColor = System.Drawing.Color.White
        Me.grdMessages.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdMessages.Location = New System.Drawing.Point(3, 0)
        Me.grdMessages.Name = "grdMessages"
        Me.grdMessages.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdMessages.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdMessages.ReadOnly = True
        Me.grdMessages.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdMessages.SelectionForeColor = System.Drawing.Color.Black
        Me.grdMessages.Size = New System.Drawing.Size(420, 149)
        Me.grdMessages.TabIndex = 5
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlsp_Top.Controls.Add(Me.tstripDiagnosis)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(426, 53)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstripDiagnosis
        '
        Me.tstripDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.tstripDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstripDiagnosis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstripDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstripDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstripDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnClose})
        Me.tstripDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.tstripDiagnosis.Name = "tstripDiagnosis"
        Me.tstripDiagnosis.Size = New System.Drawing.Size(426, 53)
        Me.tstripDiagnosis.TabIndex = 0
        Me.tstripDiagnosis.Text = "ToolStrip1"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'frmMessageHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(426, 208)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Controls.Add(Me.pnlGrid)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMessageHistory"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Message History"
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.grdMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripDiagnosis.ResumeLayout(False)
        Me.tstripDiagnosis.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmMessageHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim objclsMessage As New clsMessage
            Dim dt As DataTable
            dt = objclsMessage.SelectMessage(_MessageID)
            grdMessages.DataSource = dt
            CustomGridStyle(dt)
            objclsMessage.Dispose()
            objclsMessage = Nothing

        Catch ex As Exception
        End Try
    End Sub

    Public Sub CustomGridStyle(ByVal dt As DataTable)

        Dim ts As New clsDataGridTableStyle(dt.TableName())

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dt.Columns(0).ColumnName
        IDCol.HeaderText = "Message ID"
        IDCol.Alignment = HorizontalAlignment.Left

        Dim MsgDateCol As New DataGridTextBoxColumn
        With MsgDateCol
            .Width = 0.4 * grdMessages.Width
            .MappingName = dt.Columns(1).ColumnName
            .HeaderText = "Message Date"
            .Alignment = HorizontalAlignment.Left
            .NullText = ""
        End With

        Dim FromCol As New DataGridTextBoxColumn
        With FromCol
            .Width = 0.3 * grdMessages.Width
            .MappingName = dt.Columns(2).ColumnName
            .HeaderText = "From"
            .Alignment = HorizontalAlignment.Left
            .NullText = ""
        End With

        Dim ToCol As New DataGridTextBoxColumn
        With ToCol
            .Width = 0.3 * grdMessages.Width
            .MappingName = dt.Columns(3).ColumnName
            .HeaderText = "To"
            .Alignment = HorizontalAlignment.Left
            .NullText = ""
        End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, MsgDateCol, FromCol, ToCol})
        grdMessages.TableStyles.Clear()
        grdMessages.TableStyles.Add(ts)

    End Sub

   
    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
    End Sub
End Class
