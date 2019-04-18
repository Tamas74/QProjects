Imports System.IO
Public Class frmCCDEncryption    
    Public sEncryptKey As String = ""    
    Dim _CCDFilePath As String
    Dim _PatientID As Int64
    Public _GeneratedFrom As String
    Public _issave As Boolean = False    
    Public Event On_Save_Click()           
    Public FilePath As String
    Dim _PatientLastName As String
    Dim _FileType As String = "CCD"
    Dim _bIsRestricted As Boolean = False
    Dim _PatientCode As String
    Dim _sPurposeofUse As String = "Treatment"
    Public SumCareType As String
    Private Sub chkEncryption_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEncryption.CheckedChanged
        If chkEncryption.Checked = True Then
            txtEncryption.Focus()
            txtEncryption.ReadOnly = False
        Else
            txtEncryption.ReadOnly = True
        End If
    End Sub

    Public Sub New(ByVal CCDFilePath As String, ByVal PatientID As Int64, ByVal GeneratedFrom As String, ByVal strFilePath As String, ByVal CCDSection As String, ByVal PatientLastName As String, Optional ByVal PatientCode As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _CCDFilePath = CCDFilePath
        _PatientID = PatientID
        _GeneratedFrom = GeneratedFrom
        _PatientLastName = PatientLastName
        _PatientCode = PatientCode
    End Sub

    Public Sub New(ByVal CCDFilePath As String, ByVal PatientID As Int64, ByVal PatientLastName As String, ByVal FileType As String, Optional ByVal bIsRestricted As Boolean = False, Optional ByVal PatientCode As String = "", Optional ByVal sPurposeofUse As String = "Treatment")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _CCDFilePath = CCDFilePath
        _PatientID = PatientID
        _PatientLastName = PatientLastName
        If FileType = "CDA" Then
            _FileType = "CDA"
            lblSaveAs.Text = "Export CDA As :"
            Me.Text = "Export CDA"
            Me.Icon = Global.gloEMR.My.Resources.Export_CDA
        End If
        _bIsRestricted = bIsRestricted
        _PatientCode = PatientCode
        _sPurposeofUse = sPurposeofUse
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim FileName As String = ""

        Dim dtdate As DateTime = Date.UtcNow
        If _PatientCode = "" Then
            If _PatientLastName = "" Then
                If _FileType = "CDA" Then
                    FileName = "CDA_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                Else
                    FileName = "CCD_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                End If
            Else
                If _FileType = "CDA" Then
                    FileName = "CDA_" & _PatientLastName & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                Else
                    FileName = "CCD_" & _PatientLastName & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                End If
            End If
        Else
            If _FileType = "CDA" Then
                FileName = "CDA_" & _PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
            Else
                FileName = "CCD_" & _PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
            End If
        End If




        With SaveFileDialog1
            .FileName = FileName
            .InitialDirectory = gstrCCDFilePath
            .Filter = "XML Files (*.xml)|*.xml|XSL Files (*.xsl)|*.xsl|All files(*.*)|*.*"
            If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                txtCCDFilePath.Text = .FileName
                Label5.Text = .FileName
            End If
        End With
    End Sub

    Public Function IsSecureDocument(ByRef EncryptionKey As String) As Boolean
        Try
            Dim blnSecureContinue As Boolean = False
            Dim sEncryotKey As String = ""
            Dim bEncryptedExe As Boolean = True

            If chkEncryption.Checked = True Then
                If txtEncryption.Text.Trim() <> "" Then
                    sEncryotKey = sEncryptKey
                    blnSecureContinue = True
                Else
                    blnSecureContinue = False
                End If
            End If

            EncryptionKey = sEncryotKey
            Return blnSecureContinue

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Private Sub frmCCDEncryption_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCCDFilePath.ReadOnly = True
        txtEncryption.ReadOnly = True

        txtCCDFilePath.Text = _CCDFilePath
        Dim dtdate As DateTime = Date.UtcNow
        Dim FileName As String = ""
        If FilePath = "" Then
            If _PatientCode = "" Then
                If _PatientLastName = "" Then
                    If _FileType = "CDA" Then
                        FileName = "CDA_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                    Else
                        FileName = "CCD_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                    End If
                Else
                    If _FileType = "CDA" Then
                        FileName = "CDA_" & _PatientLastName & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                    Else
                        FileName = "CCD_" & _PatientLastName & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                    End If
                End If
            Else
                If _FileType = "CDA" Then
                    FileName = "CDA_" & _PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                Else
                    FileName = "CCD_" & _PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                End If
            End If



            FilePath = txtCCDFilePath.Text & FileName & ".xml"
        End If
        txtCCDFilePath.Text = FilePath
        Label5.Text = FilePath
    End Sub

    Private Sub tsb_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsb_Save.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim sText As String = ""

            FilePath = Label5.Text

            If chkEncryption.Checked = True Then
                If txtEncryption.Text.Trim.Length <= 0 Then
                    MessageBox.Show("Please enter the ‘Encryption File Password’.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEncryption.Focus()
                    Exit Sub
                ElseIf gloSecurity.gloEncryption.ValidateKey(txtEncryption.Text.Trim()) = False Then
                    txtEncryption.Focus()
                    Exit Sub
                End If
                If _bIsRestricted Then
                    sText = "CCDA File Exported. Encryption: Enabled; with privacy restrictions"
                Else
                    sText = "CCDA File Exported. Encryption: Enabled"
                End If

                sText = sText & "; Purpose of Use: " & _sPurposeofUse

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, SumCareType & sText, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If _bIsRestricted Then
                    sText = "CCDA File Exported. Encryption: Disabled; with privacy restrictions"
                Else
                    sText = "CCDA File Exported. Encryption: Disabled"
                End If

                sText = sText & "; Purpose of Use: " & _sPurposeofUse

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, SumCareType & sText, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            sEncryptKey = txtEncryption.Text

            Dim i As Integer = 0
            Dim _Path() As String
            Dim path As String = ""
            Dim oFileInfo1 As System.IO.FileInfo
            oFileInfo1 = New System.IO.FileInfo(FilePath)
            Do While oFileInfo1.Exists
                oFileInfo1 = New System.IO.FileInfo(FilePath)
                If oFileInfo1.Exists Then
                    FilePath = FilePath.Replace(".xml", "")
                    If FilePath.Contains("-") Then
                        _Path = FilePath.Split("-")
                        If _Path.Length > 1 Then
                            path = _Path(0)
                        End If
                    Else
                        path = FilePath
                    End If
                    i += 1
                    FilePath = path & "-" & i & ".xml"
                Else
                    Exit Do
                End If
            Loop
            _issave = True
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tsb_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsb_Close.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseHover
        If _FileType = "CDA" Then
            tlTooltip.SetToolTip(btnBrowse, "Browse CDA")
        Else
            tlTooltip.SetToolTip(btnBrowse, "Browse CCD")
        End If

    End Sub

    Private Sub txtCCDFilePath_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCDFilePath.MouseHover
        tlTooltip.SetToolTip(txtCCDFilePath, txtCCDFilePath.Text)
    End Sub

#Region "For form being called as an Attachment"
    Private bSetAsAttachment As Boolean
    Public Property IsAttachment() As Boolean
        Get
            Return bSetAsAttachment
        End Get
        Set(ByVal value As Boolean)
            bSetAsAttachment = value

            If value Then
                Me.tsb_Save.Text = "Attach&&Cls"
                Me.tsb_Save.ToolTipText = "Attach and Close"
                Me.Text = "Attach CDA"
            End If

        End Set
    End Property
#End Region
    
End Class