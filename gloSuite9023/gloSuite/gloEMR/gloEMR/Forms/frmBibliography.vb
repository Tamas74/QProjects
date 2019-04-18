Public Class frmBibliography

    Public nParentForm As Integer
    Public sBibliography As String
    Public sDeveloper As String

    Public Property _sBibliography() As String
        Get
            Return sBibliography
        End Get
        Set(ByVal value As String)
            sBibliography = value
        End Set
    End Property

    Public Property _sDeveloper() As String
        Get
            Return sDeveloper
        End Get
        Set(ByVal value As String)
            sDeveloper = value
        End Set
    End Property

    Public Sub New(ByVal nParent As Integer)
        MyBase.New()
        nParentForm = nParent
        InitializeComponent()
    End Sub

    Private Sub tls_Bibliography_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Bibliography.ItemClicked
        If e.ClickedItem.Tag = "Save&Close" Then
            sBibliography = txtBibliography.Text
            sDeveloper = txtDeveloper.Text
            Me.Close()
        ElseIf e.ClickedItem.Tag = "Close" Then
            Me.Close()
        End If
    End Sub


    Private Sub txtBibliography_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles txtBibliography.LinkClicked
        Dim p As Process = Process.Start("IExplore.exe", e.LinkText)
    End Sub

    Private Sub frmBibliography_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If nParentForm = 2 Then
            tls_Bibliography.Items("ts_Saveandclose").Enabled = False
            txtBibliography.ReadOnly = True
            txtDeveloper.ReadOnly = True
        End If

        txtBibliography.Text = Convert.ToString(sBibliography)
        txtDeveloper.Text = Convert.ToString(sDeveloper)

    End Sub
End Class