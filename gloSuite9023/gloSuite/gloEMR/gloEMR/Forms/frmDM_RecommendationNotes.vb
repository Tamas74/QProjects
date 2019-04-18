Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloEMR.gloStream.DiseaseManagement

Public Class frmDM_RecommendationNotes

#Region " Form level variable declaration "

    Private _RecommendationId As Int64 = 0
    Private _CriteriaID As Int64 = 0
    Private _nPatientId As Int64 = 0
    Private oDM As DiseaseManagement = Nothing
    Private _sNote As String = ""
    Private _nNoteUserID As Int64 = 0
    Private _sNoteUserName As String = ""
    Private _bIsCalledForRuleActivationNotes As Boolean = False
    Private _dlgResult As DialogResult = Windows.Forms.DialogResult.None
    

#End Region ' Form level variable declaration '

#Region " Form level property procedures "

    Public ReadOnly Property Note() As String
        Get
            Return _sNote
        End Get
    End Property

    Public ReadOnly Property NoteUserName() As String
        Get
            Return _sNoteUserName
        End Get
    End Property

    Public ReadOnly Property NoteUserID() As Int64
        Get
            Return _nNoteUserID
        End Get
    End Property

    Public ReadOnly Property SatisfiedDate() As DateTime
        Get
            Return dtpSatisFiedDate.Value
        End Get
    End Property

    Public ReadOnly Property FormDialogResult() As DialogResult
        Get
            Return _dlgResult
        End Get
    End Property

#End Region ' Form level property procedures '




#Region " Constructor "

    Public Sub New(ByVal RecommendationId As Int64, ByVal CreteriaID As Int64, ByVal PatientID As Int64)

        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        oDM = New DiseaseManagement
        _RecommendationId = RecommendationId
        _CriteriaID = CreteriaID
        _nPatientId = PatientID
        _sNoteUserName = gloEMR.mdlGeneral.gstrLoginName
        _nNoteUserID = gloEMR.mdlGeneral.gnLoginID
        dtpSatisFiedDate.Enabled = True
        pnlSnoozeDetails.Visible = False


    End Sub

    Public Sub New(ByVal RecommendationId As Int64, ByVal RuleActivationNoteCall As Boolean)

        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _bIsCalledForRuleActivationNotes = RuleActivationNoteCall
        _RecommendationId = RecommendationId
        _CriteriaID = 0
        _nPatientId = 0
        _sNoteUserName = gloEMR.mdlGeneral.gstrLoginName
        _nNoteUserID = gloEMR.mdlGeneral.gnLoginID
        dtpSatisFiedDate.Visible = False
        pnlSnoozeDetails.Visible = False

    End Sub

    Public Sub New(ByVal RecommendationId As Int64, ByVal CreteriaID As Int64, ByVal PatientID As Int64, ByVal ShowSnoozeDetails As Boolean)

        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        oDM = New DiseaseManagement
        _RecommendationId = RecommendationId
        _CriteriaID = CreteriaID
        _nPatientId = PatientID
        _sNoteUserName = gloEMR.mdlGeneral.gstrLoginName
        _nNoteUserID = gloEMR.mdlGeneral.gnLoginID
        dtpSatisFiedDate.Enabled = True
        pnlSnoozeDetails.Visible = ShowSnoozeDetails


    End Sub

#End Region ' Constructor '

#Region " Form Load and Closing events "

    Private Sub frmRecommendationNotes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtNotes.Text = ""
        txtNotes.Focus()
    End Sub

    Private Sub frmDM_RecommendationNotes_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try

            If Not IsNothing(oDM) Then
                oDM.Dispose()
                oDM = Nothing
            End If

        Catch ex As Exception
            'Blank catch 
        End Try

    End Sub

#End Region 'Form Load and Closing events'

#Region " Toolstrip button click events "

    Private Sub btn_tls_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click
        _sNote = txtNotes.Text.Trim()
        _dlgResult = Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub btn_tls_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Cancel.Click

        _dlgResult = Windows.Forms.DialogResult.Cancel
        Me.Close()

    End Sub

#End Region ' Toolstrip button click events '

    Private Sub txtNotes_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNotes.KeyPress
        If txtNotes.Text.Length > 255 And e.KeyChar <> "" Then
            e.Handled = True
        End If
    End Sub
End Class
