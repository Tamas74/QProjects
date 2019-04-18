Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_SpecimenMaster
    ' Used for comman type

    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Public blnIsModify As Boolean = False
    Public enumtype As Int16


    Private Sub SaveSpecimenMaster()

        'Dim oLabSpecimen As New gloEMRLabSpecimen
        Dim ogloEMRLabCSST As New gloEMRLabCSST

        Try
            If txtCode.Text = "" And txtSpecimen.Text = "" Then
                If enumtype = 3 Then
                    MessageBox.Show("Please enter Code and Temperature value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Please enter Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If

            If txtCode.Text = "" Then
                MessageBox.Show("Please enter Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtSpecimen.Text = "" Then
                If enumtype = 3 Then
                    MessageBox.Show("Please enter Temperature value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Please enter Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                txtSpecimen.Text = ""
                txtSpecimen.Select()
                Exit Sub
            End If



            'Do the Result grid validations
            If blnIsModify = True Then
                'check for duplicate entries for add
                '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                If ogloEMRLabCSST.IsCodeExistsType(txtCode.Text, enumtype) = True And ogloEMRLabCSST.IsExistsType(txtSpecimen.Text, enumtype) = True Then
                    If enumtype = 3 Then
                        MessageBox.Show("Duplicate Code and Temperature. Please enter another Code and Temperature value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Duplicate Code and Name. Please enter another Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    txtCode.Text = ""
                    txtSpecimen.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If

                '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                'If enumtype = 3 Then
                If ogloEMRLabCSST.IsCodeExistsType(txtCode.Text, enumtype) = True Then
                    MessageBox.Show("Duplicate Code. Please enter another Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                'End If

                '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                If ogloEMRLabCSST.IsExistsType(txtSpecimen.Text, enumtype) = True Then
                    If enumtype = 3 Then
                        MessageBox.Show("Duplicate Temperature value. Please enter another Temperature value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Duplicate Name. Please enter another Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    txtSpecimen.Text = ""
                    txtSpecimen.Select()
                    Exit Sub
                End If

                With ogloEMRLabCSST.LabCSST
                    .LabCSST_ID = 0
                    .LabCSST_Code = txtCode.Text
                    .LabCSST_Name = txtSpecimen.Text
                    .LabCSST_Type = enumtype
                End With
                ogloEMRLabCSST.Add()
            Else

                'check for duplicate entries for modify
                If txtCode.Text <> sEditCode And txtSpecimen.Text <> sEditName Then
                    '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                    If ogloEMRLabCSST.IsCodeExistsType(txtCode.Text, enumtype) = True And ogloEMRLabCSST.IsExistsType(txtSpecimen.Text, enumtype) = True Then
                        MessageBox.Show("Duplicate Code and Name. Please enter another Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtSpecimen.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If txtCode.Text <> sEditCode Then
                    '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                    If ogloEMRLabCSST.IsCodeExistsType(txtCode.Text, enumtype) = True Then
                        MessageBox.Show("Duplicate Code. Please enter another Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If

                If txtSpecimen.Text <> sEditName Then
                    '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
                    If ogloEMRLabCSST.IsExistsType(txtSpecimen.Text, enumtype) = True Then
                        If enumtype = 3 Then
                            MessageBox.Show("Duplicate Temperature value. Please enter another Temperature value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Duplicate Name. Please enter another Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        txtSpecimen.Text = ""
                        txtSpecimen.Select()
                        Exit Sub
                    End If
                End If

                With ogloEMRLabCSST.LabCSST
                    .LabCSST_ID = nEditID
                    .LabCSST_Code = txtCode.Text
                    .LabCSST_Name = txtSpecimen.Text
                    .LabCSST_Type = enumtype
                End With

                ogloEMRLabCSST.Modify(nEditID, enumtype)

            End If

            If blnIsModify = True Then

                txtCode.Text = ""
                txtSpecimen.Text = ""

            End If

            Me.Close()


        Catch ex As Exception
            If blnIsModify = True Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If

        End Try


    End Sub

    Private Sub frmLab_SpecimenMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

     
            If nEditID > 0 Then
                'Modify mode
                'Dim oLabSpecimen As New LabActor.LabSpecimen
                'Dim oSpecimen As New gloEMRLabSpecimen
                Dim oLabCSST As LabActor.LabCSST
                Dim ogloEMRLabCSST As New gloEMRLabCSST

                oLabCSST = ogloEMRLabCSST.GetLabCSSTTypeInfo(nEditID, enumtype)
                If (IsNothing(oLabCSST) = False) Then
                    txtCode.Text = oLabCSST.LabCSST_Code
                    txtSpecimen.Text = oLabCSST.LabCSST_Name
                    oLabCSST.Dispose()
                    oLabCSST = Nothing
                End If
                ogloEMRLabCSST.Dispose()
                ogloEMRLabCSST = Nothing
            End If
            txtCode.Focus()
            txtCode.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub


    Private Sub tlsp_SpecimenMaster_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_SpecimenMaster.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveSpecimenMaster()
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class