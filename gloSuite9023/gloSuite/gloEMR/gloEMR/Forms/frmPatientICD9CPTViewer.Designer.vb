<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrpt_PatientICD9CPTViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrpt_PatientICD9CPTViewer))
        Me.crtlrptview_patientICD9CPT = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.pnlPatientICD9CPTViewer = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlPatientICD9CPTViewer.SuspendLayout()
        Me.SuspendLayout()
        '
        'crtlrptview_patientICD9CPT
        '
        Me.crtlrptview_patientICD9CPT.ActiveViewIndex = -1
        Me.crtlrptview_patientICD9CPT.AutoScroll = True
        Me.crtlrptview_patientICD9CPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crtlrptview_patientICD9CPT.Cursor = System.Windows.Forms.Cursors.Default
        Me.crtlrptview_patientICD9CPT.DisplayStatusBar = False
        Me.crtlrptview_patientICD9CPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crtlrptview_patientICD9CPT.EnableDrillDown = False
        Me.crtlrptview_patientICD9CPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.crtlrptview_patientICD9CPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.crtlrptview_patientICD9CPT.Location = New System.Drawing.Point(4, 4)
        Me.crtlrptview_patientICD9CPT.Name = "crtlrptview_patientICD9CPT"
        Me.crtlrptview_patientICD9CPT.SelectionFormula = ""
        Me.crtlrptview_patientICD9CPT.ShowGotoPageButton = False
        Me.crtlrptview_patientICD9CPT.ShowGroupTreeButton = False
        Me.crtlrptview_patientICD9CPT.ShowRefreshButton = False
        Me.crtlrptview_patientICD9CPT.ShowTextSearchButton = False
        Me.crtlrptview_patientICD9CPT.Size = New System.Drawing.Size(1191, 795)
        Me.crtlrptview_patientICD9CPT.TabIndex = 1
        Me.crtlrptview_patientICD9CPT.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.crtlrptview_patientICD9CPT.ViewTimeSelectionFormula = ""
        '
        'pnlPatientICD9CPTViewer
        '
        Me.pnlPatientICD9CPTViewer.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientICD9CPTViewer.Controls.Add(Me.crtlrptview_patientICD9CPT)
        Me.pnlPatientICD9CPTViewer.Controls.Add(Me.Label4)
        Me.pnlPatientICD9CPTViewer.Controls.Add(Me.Label3)
        Me.pnlPatientICD9CPTViewer.Controls.Add(Me.Label2)
        Me.pnlPatientICD9CPTViewer.Controls.Add(Me.Label1)
        Me.pnlPatientICD9CPTViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientICD9CPTViewer.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientICD9CPTViewer.Name = "pnlPatientICD9CPTViewer"
        Me.pnlPatientICD9CPTViewer.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPatientICD9CPTViewer.Size = New System.Drawing.Size(1199, 803)
        Me.pnlPatientICD9CPTViewer.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(1195, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 795)
        Me.Label4.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 795)
        Me.Label3.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 799)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1193, 1)
        Me.Label2.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1193, 1)
        Me.Label1.TabIndex = 2
        '
        'frmrpt_PatientICD9CPTViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1199, 803)
        Me.Controls.Add(Me.pnlPatientICD9CPTViewer)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmrpt_PatientICD9CPTViewer"
        Me.Text = "Patient ICD9/10-CPT Viewer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlPatientICD9CPTViewer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlPatientICD9CPTViewer As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents crtlrptview_patientICD9CPT As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
