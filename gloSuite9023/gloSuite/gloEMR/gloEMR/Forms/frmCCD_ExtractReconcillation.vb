Imports gloCCDLibrary
Imports System.IO
Imports System.Xml

Public Class frmCCD_ExtractReconcillation

    Private _SourceName As String = ""
    Private _CCDID As Int64
    Private _CCDPatientID As Int64
    Private _ListStatus As Integer
    Private ToolTip1 As New System.Windows.Forms.ToolTip


#Region "Constructor and Form Load"


    Public Sub New(ByVal CCDID As Int64, ByVal CCDPatientID As Int64, ByVal SourceName As String, Optional ByVal ListStatus As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()
        _CCDID = CCDID
        _CCDPatientID = CCDPatientID

        _SourceName = SourceName
        _ListStatus = ListStatus
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmCCD_ExtractReconcillation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ToolTip1.Dispose()
        ToolTip1 = Nothing
    End Sub

    Private Sub frmCCD_ExtractReconcillation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtReconcileListName.Text = gloReconciliation.GenerateReconcileListName(_SourceName)

        ToolTip1.SetToolTip(Me.txtReconcileListName, txtReconcileListName.Text)

    End Sub

#End Region

#Region "Toolstrip Button Click "

    Private Sub tblReconcilation_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblReconcilation.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save&Close"
                    ExtractClinicalReconcilation()
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region "Extract Reconciliation List"


    Private Sub ExtractClinicalReconcilation()


        Dim oReconcileList As ReconcileList = Nothing
        Dim sFileType As String = ""
        Dim CCDFilePath As String = ""
        Try

            If txtReconcileListName.Text.Trim = "" Then
                MessageBox.Show("Reconcile List Name is Required. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            CCDFilePath = gloReconciliation.RetrieveDocumentFile(_CCDID)
            If (IsNothing(CCDFilePath) = False) Then
                If File.Exists(CCDFilePath) = False Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Error extracting clinical information. The file your trying to extract is invalid or not a recognized cda format", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                Me.Cursor = Cursors.Default
                MessageBox.Show("Error extracting clinical information. The file your trying to extract is invalid or not a recognized cda format", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
          

            sFileType = gloReconciliation.GetFileType(_CCDID)

            oReconcileList = New ReconcileList()
            oReconcileList.PatientID = _CCDPatientID
            oReconcileList.FileType = sFileType
            oReconcileList.FilePath = CCDFilePath
            oReconcileList.UserID = mdlGeneral.gnLoginID
            oReconcileList.UserName = mdlGeneral.gstrLoginName
            oReconcileList.SourceName = _SourceName
            oReconcileList.ListName = txtReconcileListName.Text.Trim
            oReconcileList.ProviderName = gloReconciliation.GetPatientProviderName(mdlGeneral.gnPatientProviderID)
            oReconcileList.ProviderID = mdlGeneral.gnPatientProviderID
            oReconcileList.Status = ListStatus.Ready
            oReconcileList.CCDID = _CCDID



            Dim _GeneratedLists As String
            _GeneratedLists = gloReconciliation.ExtractAndSaveReconcileList(oReconcileList)

            Me.Cursor = Cursors.Default

            If _GeneratedLists.Trim = "" Then
                _GeneratedLists = "No List Generated. "
            End If

            If _GeneratedLists.Trim <> "" Then
                If _GeneratedLists = "No List Generated. " Then
                    MessageBox.Show(_GeneratedLists, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.Cancel
                    Exit Sub
                Else
                    MessageBox.Show(_GeneratedLists, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                   
                End If
               
            End If

            Me.DialogResult = DialogResult.OK

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Extract, "Extracted " & sFileType & " data.", _CCDPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Throw ex

        Finally
            If IsNothing(oReconcileList) = False Then
                oReconcileList.Dispose()
                oReconcileList = Nothing
            End If
        End Try
    End Sub


#End Region


End Class