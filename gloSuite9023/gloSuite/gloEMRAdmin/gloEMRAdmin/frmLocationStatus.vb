Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

Public Class frmLocationStatus
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents c1LocationStatus As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLocationStatus))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.c1LocationStatus = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOk = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2.SuspendLayout()
        CType(Me.c1LocationStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.c1LocationStatus)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 56)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(586, 454)
        Me.Panel2.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 450)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(578, 1)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 447)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(582, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 447)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(580, 1)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "label1"
        '
        'c1LocationStatus
        '
        Me.c1LocationStatus.BackColor = System.Drawing.Color.GhostWhite
        Me.c1LocationStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1LocationStatus.ColumnInfo = resources.GetString("c1LocationStatus.ColumnInfo")
        Me.c1LocationStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1LocationStatus.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1LocationStatus.ForeColor = System.Drawing.Color.Black
        Me.c1LocationStatus.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1LocationStatus.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1LocationStatus.Location = New System.Drawing.Point(3, 3)
        Me.c1LocationStatus.Name = "c1LocationStatus"
        Me.c1LocationStatus.Rows.Count = 5
        Me.c1LocationStatus.Rows.DefaultSize = 19
        Me.c1LocationStatus.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1LocationStatus.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1LocationStatus.Size = New System.Drawing.Size(580, 448)
        Me.c1LocationStatus.StyleInfo = resources.GetString("c1LocationStatus.StyleInfo")
        Me.c1LocationStatus.TabIndex = 2
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(586, 56)
        Me.pnl_tlsp_Top.TabIndex = 2
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Toolstrip
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOk, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(586, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(66, 50)
        Me.btnOk.Text = "&Save&&Cls"
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.ToolTipText = "Save and Close"
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
        'frmLocationStatus
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(586, 510)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(2, 44)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLocationStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinic Workflow Settings"
        Me.Panel2.ResumeLayout(False)
        CType(Me.c1LocationStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "variables"
    Dim COL_COUNT As Int16 = 5

    Dim COL_ID As Integer = 0
    Dim COL_CLIENTID As Integer = 1
    Dim COL_MACHINENAME As Integer = 2
    Dim COL_LOCATION As Integer = 3
    Dim COL_STATUS As Integer = 4

    Dim _ErrorMessage As String = ""

#End Region

    Private Sub frmLocationStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1LocationStatus)

        'Sarika 23rd Apr
        Dim dtLocationStatus As DataTable
        Dim cmd As SqlCommand
        Dim conn As SqlConnection
        Dim da As SqlDataAdapter
        Dim _strSQL As String = ""

        Try
            dtLocationStatus = New DataTable
            _strSQL = "SELECT ClinicWorkFlow_Settings.nID, ClientSettings_MST.nClientID, ClientSettings_MST.sMachineName, ClinicWorkFlow_Settings.sLocation, ClinicWorkFlow_Settings.sStatus FROM ClinicWorkFlow_Settings RIGHT OUTER JOIN ClientSettings_MST ON ClinicWorkFlow_Settings.nClientID = ClientSettings_MST.nClientID"
            conn = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dtLocationStatus)

            If dtLocationStatus.Rows.Count > 0 Then
                SetGridStyle(dtLocationStatus)
            Else

                MessageBox.Show("No clients available to set Clinic Workflow.", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Close()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Error while retrieving location Status." & ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub SetGridStyle(ByVal dt As DataTable)
        With c1LocationStatus
            '' bind the Data
            '.DataSource = dt.DefaultView
            '
            Dim i As Int16
            Dim _TotalWidth As Single = (.Width)
            'sarika 25th june 07
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
            '----

            .Cols.Count = COL_COUNT
            .Rows.Count = 1
            .AllowEditing = True
            '  .AllowAddNew = True
            '.Width = .Width - 10
            '******By Sandip Deshmukh 13 Oct 07 5.48PM Bug# 335
            '******Code of line Added so that the Columns header 
            '******on Drag does not Change the column Header Position
            .AllowDragging = AllowDraggingEnum.None
            '****** 13 Oct 07 5.48PM
            .Styles.ClearUnused()

            .Cols(COL_ID).Width = .Width * 0
            .Cols(COL_ID).AllowEditing = False
            .SetData(0, COL_ID, "nID")
            .Cols(COL_ID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_CLIENTID).Width = .Width * 0
            .Cols(COL_CLIENTID).AllowEditing = False
            .SetData(0, COL_CLIENTID, "nClientID")
            .Cols(COL_CLIENTID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_MACHINENAME).Width = _TotalWidth * 0.25
            .SetData(0, COL_MACHINENAME, "sMachineName")
            .Cols(COL_MACHINENAME).Caption = "Machine Name"
            .Cols(COL_MACHINENAME).DataType = GetType(String)
            .Cols(COL_MACHINENAME).AllowEditing = False
            .Cols(COL_MACHINENAME).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LOCATION).Width = _TotalWidth * 0.25
            .SetData(0, COL_LOCATION, "sLocation")
            .Cols(COL_LOCATION).AllowEditing = True
            .Cols(COL_LOCATION).Caption = "Location"
            .Cols(COL_LOCATION).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_STATUS).Width = _TotalWidth * 0.45
            .SetData(0, COL_STATUS, "sStatus")
            .Cols(COL_STATUS).AllowEditing = True
            .Cols(COL_STATUS).Caption = "Status"
            .Cols(COL_STATUS).TextAlignFixed = TextAlignEnum.LeftCenter

            Dim dtLocation As New DataTable
            Dim _strLocation As String = " "

            dtLocation = GetLocation()
            If IsNothing(dtLocation) = False Then
                For i = 0 To dtLocation.Rows.Count - 1
                    _strLocation = _strLocation & "|" & dtLocation.Rows(i)("sDescription")
                Next
            End If

            Dim csLocation As CellStyle = .Styles.Add("Location")
            '' Fill Values In ComboBox
            csLocation.ComboList = _strLocation
            .Cols(COL_LOCATION).Style = csLocation

            Dim dtStatus As New DataTable
            Dim _strStatus As String = " "

            dtStatus = GetStatus()
            If IsNothing(dtStatus) = False Then
                For i = 0 To dtStatus.Rows.Count - 1
                    _strStatus = _strStatus & "|" & dtStatus.Rows(i)("sDescription")
                Next
            End If

            Dim csStatus As CellStyle = .Styles.Add("Status")
            '' Fill Values In ComboBox
            csStatus.ComboList = _strStatus
            ''
            .Cols(COL_STATUS).Style = csStatus


            '.Cols(COL_PRESCRIPTION).Style = csPresc
            '.Cols(COL_STATUS).Style = csStatus

            'cStyle = .Styles.Add("BubbleValues")
            'cStyle.ComboList = "..."
            '.Cols(COL_DIAGNOSISBUTTON).Style = cStyle

            '.Cols(COL_COMPLAINTS).Style = Nothing

            ''
            'dtpDOS.Value = Format(GetVisitdate(_VisitID), "MM/dd/yyyy")

            '' Table dt Contains following Columns
            '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()

                'Set Column Style 
                ' Assinge the Cell for ComboBox
                'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                'rgDia.Style = csDia  '' .Styles.Add("Dia")

                ' Assinge the Cell for ComboBox
                'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                'rgStatus.Style = csStatus '''  .Styles.Add("Status")

                '' Fill the Retrived information to relative controls
                'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

                .SetData(i + 1, COL_ID, dt.Rows(i)("nID"))
                .SetData(i + 1, COL_CLIENTID, dt.Rows(i)("nClientID"))
                .SetData(i + 1, COL_MACHINENAME, dt.Rows(i)("sMachineName"))
                .SetData(i + 1, COL_LOCATION, dt.Rows(i)("sLocation"))
                .SetData(i + 1, COL_STATUS, dt.Rows(i)("sStatus"))

                ' By Mahesh, 20070317
                '  .SetData(i + 1, COL_USER, dt.Rows(i)("nUserID"))

                '' By Mahesh, 20070326 
                ''  SET Diagnosis Comment Button
                '  .SetData(i + 1, COL_DIAGNOSISBUTTON, "")
                '    Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_DIAGNOSISBUTTON, i + 1, COL_DIAGNOSISBUTTON)
                '   rgDig.Style = cStyle
            Next

            Dim objAudit As New clsAudit


            objAudit = Nothing

            '.Cols(COL_DIAGNOSIS).AllowEditing = False
            '.Cols(COL_STATUS).AllowEditing = False
        End With
    End Sub

    Public Function GetLocation() As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim da As SqlDataAdapter

        Try
            dt = New DataTable

            _strSQL = "select nCategoryID,sDescription from Category_MST where sCategoryType='Location'"
            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return Nothing
            MessageBox.Show("Error while retrieving location." & ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   conn.Close()
        End Try
    End Function

    Public Function GetStatus() As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim da As SqlDataAdapter

        Try
            dt = New DataTable

            _strSQL = "select nCategoryID,sDescription from Category_MST where sCategoryType='Status'"
            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return Nothing
            MessageBox.Show("Error while retrieving Status." & ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Finally
            '   conn.Close()
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i As Integer
        Dim cmdDelete As SqlCommand
        Dim cmdInsert As SqlCommand
        Dim _strSQL As String = ""
        Dim conn As SqlConnection
        'sarika 26th june 07
        'Dim myTrans As SqlTransaction
        Dim myTrans As SqlTransaction = Nothing
        '----------


        conn = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())

        Try
            conn.Open()
            myTrans = conn.BeginTransaction
            _strSQL = "delete  from  ClinicWorkFlow_Settings"

            cmdDelete = New SqlCommand(_strSQL, conn)
            cmdDelete.Transaction = myTrans

            cmdDelete.ExecuteNonQuery()
            'cmd.Connection = conn
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "gsp_InsClinicWorkFlow_Settings"


            'save the grid values in ClinicWorkFlow_Settings table
            With c1LocationStatus
                For i = 1 To c1LocationStatus.Rows.Count - 1
                    _strSQL = "insert into ClinicWorkFlow_Settings(nID,nClientID,sLocation,sStatus) values(" & i & "," & .GetData(i, COL_CLIENTID) & ",'" & .GetData(i, COL_LOCATION).ToString().Replace("'", "''") & "','" & .GetData(i, COL_STATUS).ToString().Replace("'", "''") & "')"
                    cmdInsert = New SqlCommand(_strSQL, conn)
                    cmdInsert.Transaction = myTrans

                    cmdInsert.ExecuteNonQuery()
                    cmdInsert = Nothing
                Next
            End With


            myTrans.Commit()
            'generate audit log
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user as modified the Clinic WorkFlow Settings.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing

            Me.Close()

        Catch ex As Exception
            'if some error occur when inserting in any of the tables then all the transactions are rollbacked
            Try
                myTrans.Rollback()
            Catch ex1 As SqlException
                If Not myTrans.Connection Is Nothing Then
                    'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                    '" was encountered while attempting to roll back the transaction.")
                    _ErrorMessage = ex.Message
                End If
            End Try

            _ErrorMessage = ex.Message
            '_ErrorMessage &= " Neither record was written to database."
            MessageBox.Show(_ErrorMessage, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " Error modifying the Clinic WorkFlow Settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
        Finally
            conn.Close()

        End Try
    End Sub
End Class
