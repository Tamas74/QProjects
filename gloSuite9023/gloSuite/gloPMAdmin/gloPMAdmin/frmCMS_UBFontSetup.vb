Public Class frmCMS_UBFontSetup
    Dim sfont As String = "Arial"
    Dim sfontsize As String = "10"
    Private _sSelectedfont As String
    Public Property sSelectedfont() As String
        Get
            Return _sSelectedfont
        End Get
        Set(ByVal value As String)
            _sSelectedfont = value
        End Set
    End Property

    Private _sSelectedfontSize As String
    Public Property sSelectedfontSize() As String
        Get
            Return _sSelectedfontSize
        End Get
        Set(ByVal value As String)
            _sSelectedfontSize = value
        End Set
    End Property
    Private _bIsSave As Boolean = False
    Public Property bIsSave() As String
        Get
            Return _bIsSave
        End Get
        Set(ByVal value As String)
            _bIsSave = value
        End Set
    End Property

    Public Sub New(sfont As String, sfontsize As String)
        MyBase.New()
        Me.sfont = sfont
        Me.sfontsize = sfontsize
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmCMS_UBFontSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dtFontNames As DataTable = Nothing
        Dim dtFontSizes As DataTable = Nothing

        dtFontNames = GetFontSetup(1)
        dtFontSizes = GetFontSetup(2)

        If Not IsNothing(dtFontNames) Then
            If dtFontNames.Rows.Count > 0 Then
                cmbFontName.DataSource = dtFontNames
                cmbFontName.DisplayMember = "sName"
                cmbFontName.ValueMember = "sName"
                cmbFontName.SelectedValue = sfont
            End If
        End If

        If Not IsNothing(dtFontSizes) And dtFontSizes.Rows.Count > 0 Then
            cmbFontSize.DataSource = dtFontSizes
            cmbFontSize.DisplayMember = "sName"
            cmbFontSize.ValueMember = "sName"
            cmbFontSize.SelectedValue = sfontsize
        End If
        If (cmbFontName.SelectedValue Is Nothing) Then
            cmbFontName.SelectedValue = "Arial"
        End If
        If (cmbFontSize.SelectedValue Is Nothing) Then
            cmbFontSize.SelectedValue = "10"
        End If
    End Sub

    Public Function GetFontSetup(ByVal nType As Integer) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dtFontSetup As New DataTable
        Try
            oDB.Connect(False)
            oDBParameters.Add("@nType", nType, ParameterDirection.Input, SqlDbType.Int)
            oDB.Retrive("gsp_GetFontSetup_CMS_UB", oDBParameters, dtFontSetup)
            Return dtFontSetup
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDBParameters.Dispose()
        End Try
    End Function

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

        Try
            If Validation() = False Then
                _bIsSave = False
                Exit Sub
            End If

          

            If (Not IsNothing(cmbFontName.SelectedValue) AndAlso cmbFontName.SelectedValue <> "") Then
                _sSelectedfont = cmbFontName.Text
            End If

            If (Not IsNothing(cmbFontSize.SelectedValue) AndAlso cmbFontSize.SelectedValue <> "") Then
                _sSelectedfontSize = cmbFontSize.Text
            End If
            _bIsSave = True
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub
    Public Function Validation() As Boolean
        Dim _result As Boolean = True

        If (IsNothing(cmbFontName.SelectedValue) AndAlso cmbFontName.SelectedValue = "") Then
            MessageBox.Show("Select font name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbFontName.Focus()
            _result = False
        ElseIf (IsNothing(cmbFontSize.SelectedValue) AndAlso cmbFontSize.SelectedValue = "") Then
            MessageBox.Show("Select font size.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbFontSize.Focus()
            _result = False
        End If

        If cmbFontSize.SelectedValue <> sfontsize Or cmbFontName.SelectedValue <> sfont Then
            Dim r As DialogResult = Windows.Forms.DialogResult.None
            r = MessageBox.Show("Warning : Changing font/font size for paper claim print data may require print coordinate shift/change." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            If r = DialogResult.Cancel Or r = DialogResult.None Then
                _result = False
            End If
        End If

        Return _result

    End Function
End Class