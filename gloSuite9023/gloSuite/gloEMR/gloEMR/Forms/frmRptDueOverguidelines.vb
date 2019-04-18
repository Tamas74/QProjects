Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Public Class frmRptDueOverguidelines
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
    Friend WithEvents CrReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptDueOverguidelines))
        Me.CrReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CrReportViewer
        '
        Me.CrReportViewer.ActiveViewIndex = -1
        Me.CrReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrReportViewer.DisplayGroupTree = False
        Me.CrReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrReportViewer.EnableDrillDown = False
        Me.CrReportViewer.Location = New System.Drawing.Point(3, 3)
        Me.CrReportViewer.Name = "CrReportViewer"
        Me.CrReportViewer.SelectionFormula = ""
        Me.CrReportViewer.ShowCloseButton = False
        Me.CrReportViewer.ShowExportButton = False
        Me.CrReportViewer.ShowGroupTreeButton = False
        Me.CrReportViewer.Size = New System.Drawing.Size(1022, 687)
        Me.CrReportViewer.TabIndex = 0
        Me.CrReportViewer.ViewTimeSelectionFormula = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tls)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 53)
        Me.Panel1.TabIndex = 1
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = CType(resources.GetObject("tls.BackgroundImage"), System.Drawing.Image)
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnCancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(1028, 53)
        Me.tls.TabIndex = 1
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Close"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Controls.Add(Me.CrReportViewer)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1028, 693)
        Me.Panel2.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 689)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1020, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 686)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1024, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 686)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1022, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmRptDueOverguidelines
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRptDueOverguidelines"
        Me.Text = "Due Over Guidelines"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim orpt As ReportDocument
    Private m_Type As String
    Private Sub frmDueOverguidelines_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            orpt = New ReportDocument
            orpt.Load(Application.StartupPath & "\Reports\rptDueOverDue.rpt")
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            ' Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = orpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            Dim objreport As TextObject
            objreport = orpt.ReportDefinition.ReportObjects.Item("Text6")
            objreport.Text = m_Type & " Report"
            CrReportViewer.ReportSource = orpt

            'orpt.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public WriteOnly Property Type() As String
        Set(ByVal Value As String)
            m_Type = Value
        End Set
    End Property

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()
    End Sub
End Class
