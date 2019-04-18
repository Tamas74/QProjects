Public Class frmDM_Process
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New() 'ByVal FindCriteriaName As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        '_FindCriteriaName = FindCriteriaName
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblMessages As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmDM_Process))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblMessages = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(308, 144)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblMessages
        '
        Me.lblMessages.BackColor = System.Drawing.Color.Transparent
        Me.lblMessages.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessages.ForeColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(0, Byte), CType(0, Byte))
        Me.lblMessages.Location = New System.Drawing.Point(8, 112)
        Me.lblMessages.Name = "lblMessages"
        Me.lblMessages.Size = New System.Drawing.Size(204, 18)
        Me.lblMessages.TabIndex = 1
        Me.lblMessages.Text = "Finding Health Plans..."
        Me.lblMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmDM_Process
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(308, 144)
        Me.Controls.Add(Me.lblMessages)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDM_Process"
        Me.Text = "Find Health Plan"
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Public WithEvents oDMProcess As New gloStream.DiseaseManagement.DiseaseManagement
    'Dim _oPatients As New Collection
    'Private _FindCriteriaName As String
    'Private _FindCriteriaID As Long
    'Public ofrmMDIForm As Form

    Private Sub SetMyLocation()
        Dim _ScrollHeight As Integer = y - Me.Height - 30
        Me.SetDesktopLocation(x - Me.Width - 10, _ScrollHeight)
        Application.DoEvents()
    End Sub

    'Private Sub StopMyProcess()
    '    IsFindCriteriaInProcess = False
    '    _FindCriteriaName = ""
    'End Sub

    Private Sub frmDM_Process_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetMyLocation()
            'Application.DoEvents()
            'System.Threading.Thread.Sleep(5000)
            'Application.DoEvents()
            'FindCriteria()
        Catch ex As Exception
            lblMessages.Text = ex.Message
            'StopMyProcess()
        End Try
    End Sub

    'Private Sub FindCriteria()
    '    'oDMSetup delete data of selected criteria
    '    Try
    '        Dim _strFind As String = ""
    '        If _FindCriteriaName.Trim <> "" Then
    '            Dim _FindCriteriaID As Long
    '            _FindCriteriaID = GetCriteriaID(_FindCriteriaName)
    '            If _FindCriteriaID > 0 Then
    '                Application.DoEvents()
    '                _oPatients = oDMProcess.FindGuidelinesForMultiplePatient(_FindCriteriaID)
    '                Application.DoEvents()
    '            Else
    '                StopMyProcess()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblMessages.Text = ex.Message
    '        StopMyProcess()
    '    End Try
    'End Sub

    'Private Function GetCriteriaID(ByVal oCriteriaName As String) As Long
    '    Dim _strSQL As String = ""
    '    Dim _Result As String = ""
    '    Dim oDB As New gloStream.gloDataBase.gloDataBase

    '    If Not oCriteriaName = "" Then
    '        'Criteria Master Record
    '        _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST WHERE dm_mst_CriteriaName = '" & oCriteriaName & "' AND dm_mst_CriteriaName IS NOT NULL"
    '        oDB.Connect(GetConnectionString)
    '        _Result = oDB.ExecuteQueryScaler(_strSQL)
    '        oDB.Disconnect()
    '        'Return Object
    '        If Val(_Result) <> 0 Then
    '            Return CLng(_Result)
    '        Else
    '            Return 0
    '        End If
    '    End If

    'End Function

    'Private Sub oDMProcess_FinishCriteria(ByVal status As Boolean, ByVal oPatients As Microsoft.VisualBasic.Collection) Handles oDMProcess.FinishCriteria
    '    If status = True Then
    '        _oPatients = oPatients
    '        If Not _oPatients Is Nothing Then
    '            Dim frm As New frmDM_Template(gnPatientID, _FindCriteriaID, _oPatients)
    '            With frm
    '                .MdiParent = ofrmMDIForm
    '                .ShowInTaskbar = False
    '                .WindowState = FormWindowState.Maximized
    '                .Show()
    '            End With
    '            StopMyProcess()
    '            oDMProcess = Nothing
    '            _oPatients = Nothing
    '        End If
    '    End If
    'End Sub
End Class
