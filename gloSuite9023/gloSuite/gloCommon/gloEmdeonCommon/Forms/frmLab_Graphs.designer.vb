<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_Graphs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_Graphs))
        Me.cmbResults = New System.Windows.Forms.ComboBox
        Me.cmbTests = New System.Windows.Forms.ComboBox
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.lblResults = New System.Windows.Forms.Label
        Me.lblTests = New System.Windows.Forms.Label
        Me.lblTodate = New System.Windows.Forms.Label
        Me.lblFrmDate = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tlsp_LabResultGraph = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.tlsp_LabResultGraph.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbResults
        '
        Me.cmbResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbResults.FormattingEnabled = True
        Me.cmbResults.Location = New System.Drawing.Point(261, 59)
        Me.cmbResults.Name = "cmbResults"
        Me.cmbResults.Size = New System.Drawing.Size(98, 22)
        Me.cmbResults.TabIndex = 8
        '
        'cmbTests
        '
        Me.cmbTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTests.FormattingEnabled = True
        Me.cmbTests.Location = New System.Drawing.Point(88, 59)
        Me.cmbTests.Name = "cmbTests"
        Me.cmbTests.Size = New System.Drawing.Size(97, 22)
        Me.cmbTests.TabIndex = 7
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
        Me.dtTo.Location = New System.Drawing.Point(261, 25)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(98, 22)
        Me.dtTo.TabIndex = 5
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
        Me.dtFrom.Location = New System.Drawing.Point(88, 25)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(97, 22)
        Me.dtFrom.TabIndex = 4
        Me.dtFrom.Value = New Date(2007, 7, 4, 0, 0, 0, 0)
        '
        'lblResults
        '
        Me.lblResults.AutoSize = True
        Me.lblResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResults.Location = New System.Drawing.Point(204, 63)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(53, 14)
        Me.lblResults.TabIndex = 3
        Me.lblResults.Text = "Results :"
        '
        'lblTests
        '
        Me.lblTests.AutoSize = True
        Me.lblTests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTests.Location = New System.Drawing.Point(39, 63)
        Me.lblTests.Name = "lblTests"
        Me.lblTests.Size = New System.Drawing.Size(45, 14)
        Me.lblTests.TabIndex = 2
        Me.lblTests.Text = "Tests :"
        '
        'lblTodate
        '
        Me.lblTodate.AutoSize = True
        Me.lblTodate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTodate.Location = New System.Drawing.Point(197, 29)
        Me.lblTodate.Name = "lblTodate"
        Me.lblTodate.Size = New System.Drawing.Size(60, 14)
        Me.lblTodate.TabIndex = 1
        Me.lblTodate.Text = "To Date :"
        '
        'lblFrmDate
        '
        Me.lblFrmDate.AutoSize = True
        Me.lblFrmDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrmDate.Location = New System.Drawing.Point(12, 29)
        Me.lblFrmDate.Name = "lblFrmDate"
        Me.lblFrmDate.Size = New System.Drawing.Size(72, 14)
        Me.lblFrmDate.TabIndex = 0
        Me.lblFrmDate.Text = "From Date :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlsp_LabResultGraph)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(396, 53)
        Me.Panel1.TabIndex = 1
        '
        'tlsp_LabResultGraph
        '
        Me.tlsp_LabResultGraph.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_LabResultGraph.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_LabResultGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_LabResultGraph.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_LabResultGraph.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_LabResultGraph.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_LabResultGraph.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_LabResultGraph.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_LabResultGraph.Name = "tlsp_LabResultGraph"
        Me.tlsp_LabResultGraph.Size = New System.Drawing.Size(396, 53)
        Me.tlsp_LabResultGraph.TabIndex = 7
        Me.tlsp_LabResultGraph.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(86, 50)
        Me.ts_btnSave.Tag = "ShowResultGraph"
        Me.ts_btnSave.Text = "Show &Graph"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.cmbResults)
        Me.pnl_Base.Controls.Add(Me.cmbTests)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.dtTo)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.dtFrom)
        Me.pnl_Base.Controls.Add(Me.lblResults)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_Base.Controls.Add(Me.lblTests)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Controls.Add(Me.lblTodate)
        Me.pnl_Base.Controls.Add(Me.lblFrmDate)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 53)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(396, 105)
        Me.pnl_Base.TabIndex = 2
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 101)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(388, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 98)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(392, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 98)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(390, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'frmLab_Graphs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(396, 158)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_Graphs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab Result Graph"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlsp_LabResultGraph.ResumeLayout(False)
        Me.tlsp_LabResultGraph.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblResults As System.Windows.Forms.Label
    Friend WithEvents lblTests As System.Windows.Forms.Label
    Friend WithEvents lblTodate As System.Windows.Forms.Label
    Friend WithEvents lblFrmDate As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbResults As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTests As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents tlsp_LabResultGraph As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
End Class
