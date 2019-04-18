<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewPendingLabOrder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                If (IsNothing(octlPatientList) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(octlPatientList)
                       
                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                       
                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(octlPatientList) = False) Then
                    Try
                        octlPatientList.Dispose()
                        octlPatientList = Nothing
                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    Try
                        GloUC_TransactionHistory1.Dispose()
                        GloUC_TransactionHistory1 = Nothing

                    Catch ex As Exception

                    End Try
                End If
                
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewPendingLabOrder))
        Me.pnlPendinglab = New System.Windows.Forms.Panel
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.pnlPatientList = New System.Windows.Forms.Panel
        Me.pnlPatientLab = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlToolBar.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPendinglab
        '
        Me.pnlPendinglab.Location = New System.Drawing.Point(13, 164)
        Me.pnlPendinglab.Name = "pnlPendinglab"
        Me.pnlPendinglab.Size = New System.Drawing.Size(200, 100)
        Me.pnlPendinglab.TabIndex = 1
        '
        'pnlToolBar
        '
        Me.pnlToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolBar.Controls.Add(Me.ToolStrip1)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(962, 54)
        Me.pnlToolBar.TabIndex = 3
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_Close})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(962, 53)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'pnlPatientList
        '
        Me.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientList.Location = New System.Drawing.Point(0, 54)
        Me.pnlPatientList.Name = "pnlPatientList"
        Me.pnlPatientList.Size = New System.Drawing.Size(962, 186)
        Me.pnlPatientList.TabIndex = 4
        '
        'pnlPatientLab
        '
        Me.pnlPatientLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientLab.Location = New System.Drawing.Point(0, 241)
        Me.pnlPatientLab.Name = "pnlPatientLab"
        Me.pnlPatientLab.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientLab.Size = New System.Drawing.Size(962, 398)
        Me.pnlPatientLab.TabIndex = 5
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 240)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(962, 1)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'tsb_Close
        '
        Me.tsb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Tag = "Close"
        Me.tsb_Close.Text = "Close"
        Me.tsb_Close.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmViewPendingLabOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(962, 639)
        Me.Controls.Add(Me.pnlPatientLab)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlPatientList)
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "frmViewPendingLabOrder"
        Me.Text = "View Pending Lab Order"
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlPendinglab As System.Windows.Forms.Panel
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents pnlPatientList As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientLab As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
End Class
