Imports gloEMRGeneralLibrary.gloEMRPrescription

Public Class frmeRxSummary
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event eRxClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ApproveClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ApprovewithChangesClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event DTNFclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Discardclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event DiscardAllclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private blnApprove As Boolean = False
    Private blnApprovewithchanges As Boolean = False
    Private blnDNTF As Boolean = False
    Private blnNewRx As Boolean = False
    Private blnnarcotic As Boolean = False
    Private mFileData As Byte() = Nothing
    Public _selectedItem As Integer = Nothing
    Private FileName As String = ""


    ''for any other btn click like sendnewRx/approve/approvewithchanges/dtnf we do not have to close the form.
    ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form. hence added the blnSSTranClicked boolean variable logic.
    Dim blnSSTranClicked As Boolean = False ''if the sendNewRx/approve/approvewithchanges button is clicked then do not call the form closing event.
    Public Property NewRx() As Boolean
        Get
            Return blnNewRx
        End Get
        Set(ByVal value As Boolean)
            blnNewRx = value
        End Set
    End Property
    Public Property Approve() As Boolean
        Get
            Return blnApprove
        End Get
        Set(ByVal value As Boolean)
            blnApprove = value
        End Set
    End Property
    Public Property selectedItem() As Integer
        Get
            Return _selectedItem
        End Get
        Set(ByVal value As Integer)
            _selectedItem = value
        End Set
    End Property
    Public Property Approvewithchanges() As Boolean
        Get
            Return blnApprovewithchanges
        End Get
        Set(ByVal value As Boolean)
            blnApprovewithchanges = value
        End Set
    End Property

    Public Property DNTF() As Boolean
        Get
            Return blnDNTF
        End Get
        Set(ByVal value As Boolean)
            blnDNTF = value
        End Set
    End Property
    Public Property DTNFNarcotic() As Boolean
        Get
            Return blnnarcotic
        End Get
        Set(ByVal value As Boolean)
            blnnarcotic = value
        End Set
    End Property
    Public Property FileData() As Byte()
        Get
            Return mFileData
        End Get
        Set(ByVal value As Byte())
            mFileData = value
        End Set
    End Property
    Public Property XMLFile As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmeRxSummary_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form.
        ''for any other btn click like sendnewRx/approve/approvewithchanges/dtnf we do not have to close the form.
        If blnSSTranClicked = False Then
            RaiseEvent CloseClick(sender, e)
            If Me.IsDisposed Then
                Exit Sub
            Else
                Me.Dispose()
            End If
        End If

    End Sub
    Private Sub frmeRxSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tlbbtnApprove.Text = "Send this Approved Rx Renewal"
        tlbbtnApprovewithChanges.Text = "Send this Approved with Changes Rx Renewal"
        tlbbtnDTNF.Text = "Send this Modified Rx [DNTF]"
        tlbbtnNewRx.Text = "Send this New Rx"


        tlbbtnApprove.Visible = Approve
        tlbbtnApprovewithChanges.Visible = Approvewithchanges
        tlbbtnDTNF.Visible = DNTF
        tlbbtnNewRx.Visible = NewRx
        If XMLFile <> "" Then
            pnlWebBrowser.BringToFront()
            WebBrowser1.Navigate(XMLFile)
        Else
            pnlDrugSummary.BringToFront()
        End If
    End Sub

    Private Sub tlbbtnNewRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnNewRx.Click
        blnSSTranClicked = True ''do not call form closing event
        RaiseEvent eRxClick(sender, e)
        Me.Close()
    End Sub

    Private Sub tlbbtnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnApprove.Click
        blnSSTranClicked = True ''do not call form closing event
        RaiseEvent ApproveClick(sender, e)
        Me.Close()
    End Sub

    Private Sub tlbbtnApprovewithChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnApprovewithChanges.Click
        blnSSTranClicked = True ''do not call form closing event
        RaiseEvent ApprovewithChangesClick(sender, e)
        Me.Close()
    End Sub

    Private Sub tlbbtnDTNF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnDTNF.Click
        blnSSTranClicked = True '' do not call form closing event
        RaiseEvent DTNFclick(sender, e)
        Me.Close()
    End Sub

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click

        ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form.
        blnSSTranClicked = False ''since we cannot dispose the form in formclosing event when new/approve/DTNF btn is clicked, set this variable to false so that we can dispose the form on form closing event.


        '''''email from phil dt: 28 Oct 2013 sub: v8000 notes
        'Dim dialogResult As DialogResult
        'dialogResult = System.Windows.Forms.MessageBox.Show("All the messages will be discarded. Do you wnat to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If dialogResult = Windows.Forms.DialogResult.Yes Then
        RaiseEvent CloseClick(sender, e)
        Me.Close()
        'Else
        'Exit Sub
        'End If
    End Sub

    Private Sub tlbbtnDiscard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent Discardclick(sender, e)
        Me.Close()
    End Sub

    Private Sub tlbbtnDiscardAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent DiscardAllclick(sender, e)
        Me.Close()
    End Sub
End Class
