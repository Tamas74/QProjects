Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRLab

Public Class frmLab_ContactInformation

    Public nEditID As Int64
    Public sEditContactName As String
    Public sEditFirstName As String
    Public sEditMiddleName As String
    Public sEditLastName As String
    Public blnIsModify As Boolean = False
    Public blnContactType As LabActor.enumContactType

    'sarika 15th oct 07
    Public m_contactID As Long

    Public Property ContactID() As Long
        Get
            Return m_contactID
        End Get
        Set(ByVal value As Long)
            m_contactID = value
        End Set
    End Property
    '------
    Dim oAddresscontrol As gloAddress.gloAddressControl = Nothing
    Private Sub SaveContactInformation()

        ' Dim oLabContactInfo As New LabActor.LabContactInformation
        Dim oContactInfo As New gloEMRLabContactInfo


        Try




            'Blank


            If blnContactType = LabActor.enumContactType.PreferredLab Then
                If txtContactName.Text = "" Then
                    MessageBox.Show("Please enter Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtContactName.Select()
                    Exit Sub
                End If
            Else
                'if referred by/sampled by
                If txtFirstName.Text = "" And txtLastName.Text = "" Then
                    MessageBox.Show("Please enter First Name and Last Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFirstName.Select()
                    Exit Sub
                End If
                If txtFirstName.Text = "" Then
                    MessageBox.Show("Please enter First Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFirstName.Select()
                    Exit Sub
                End If
                If txtLastName.Text = "" Then
                    MessageBox.Show("Please enter Last Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtLastName.Select()
                    Exit Sub
                End If
            End If

            With oContactInfo.LabContactInfo
                .ContactName = txtContactName.Text.Trim
                .FirstName = txtFirstName.Text.Trim
                .MiddleName = txtMiddleName.Text.Trim
                .LastName = txtLastName.Text.Trim
                .Type = blnContactType
                .Address1 = oAddresscontrol.txtAddress1.Text.Trim()
                .Address2 = oAddresscontrol.txtAddress2.Text.Trim()
                .City = oAddresscontrol.txtCity.Text.Trim()
                .Country = oAddresscontrol.cmbCountry.Text.Trim()
                .Zip = oAddresscontrol.txtZip.Text.Trim()
                .State = oAddresscontrol.cmbState.Text.Trim()
                .County = oAddresscontrol.txtCounty.Text.Trim()
                .Phone = mskGIPhone.Text.Trim()

            End With


            If blnIsModify = True Then
                'open in add mode

                'check for Duplicate entries for add

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if Preferred lab
                    If oContactInfo.IsExists(Trim(txtContactName.Text), "", "", "", blnContactType) Then
                        MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtContactName.Text = ""
                        txtContactName.Select()
                        Exit Sub
                    End If
                Else
                    'If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if referredby  / sampled by

                    If oContactInfo.IsExists("", Trim(txtFirstName.Text), Trim(txtMiddleName.Text), Trim(txtLastName.Text), blnContactType) Then
                        MessageBox.Show("Duplicate Contact Name. Please enter another Contact Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtFirstName.Text = ""
                        txtLastName.Text = ""
                        txtMiddleName.Text = ""
                        txtFirstName.Select()
                        Exit Sub
                    End If

                    'ElseIf blnContactType = LabActor.enumContactType.PreferredLab Then
                    '    'if

                End If

                'sarika 16th oct 07
                'oContactInfo.Add(blnContactType)
                m_contactID = oContactInfo.Add(blnContactType)
                '-----------
                '    oContactInfo.Modify(nEditID, blnContactType)

            Else
                'modify Lab contact information

                'check for Duplicate entries for modify

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if Preferred lab
                    If sEditContactName <> "" Then
                        If UCase(txtContactName.Text) <> UCase(sEditContactName) Then
                            If oContactInfo.IsExists(Trim(txtContactName.Text), "", "", "", blnContactType) Then
                                MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtContactName.Text = ""
                                txtContactName.Select()
                                Exit Sub
                            End If
                        End If
                    Else
                        'sarika 16th oct 07
                        'i.e. the form is loaded from the usre control in Lab Orders form 
                        If oContactInfo.IsExists(nEditID, Trim(txtContactName.Text), "", "", "", blnContactType) Then
                            MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtContactName.Text = ""
                            txtContactName.Select()
                            Exit Sub
                        End If
                    End If
                Else
                    'If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if referredby  / sampled by

                    If UCase(txtFirstName.Text) <> UCase(sEditFirstName) Or UCase(txtMiddleName.Text) <> UCase(sEditMiddleName) Or UCase(txtLastName.Text) <> UCase(sEditLastName) Then
                        If oContactInfo.IsExists("", Trim(txtFirstName.Text), Trim(txtMiddleName.Text), Trim(txtLastName.Text), blnContactType) Then
                            MessageBox.Show("Duplicate Contact Name. Please enter another Contact Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtFirstName.Text = ""
                            txtLastName.Text = ""
                            txtMiddleName.Text = ""
                            txtFirstName.Select()
                            Exit Sub
                        End If
                    End If

                    'ElseIf blnContactType = LabActor.enumContactType.PreferredLab Then
                    '    'if

                End If

                oContactInfo.Modify(nEditID, blnContactType)
            End If
            


            If blnIsModify = True Then

                txtContactName.Text = ""
                txtFirstName.Text = ""
                txtMiddleName.Text = ""
                txtLastName.Text = ""

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    txtContactName.Select()
                ElseIf blnContactType = LabActor.enumContactType.ReferredBy Or blnContactType = LabActor.enumContactType.SampledBy Then
                    txtFirstName.Select()
                End If
            End If



            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            ' oLabContactInfo = Nothing
            If (IsNothing(oContactInfo) = False) Then
                oContactInfo.Dispose()
                oContactInfo = Nothing
            End If


        End Try
    End Sub

    Private Sub frmLab_ContactInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim _databaseconnectionstring As String = ""
            oAddresscontrol = New gloAddress.gloAddressControl(GetConnectionString())
            oAddresscontrol.Dock = DockStyle.Fill
            oAddresscontrol.Name = "LabAddressControl"
            pnlAddresssControl.Controls.Add(oAddresscontrol)


            If blnContactType = LabActor.enumContactType.PreferredLab Then
                
                pnlContactInfo.Visible = True
                pnlRefSampledBy.Visible = False
                Me.Text = "Preferred/Performing Lab Master"
            Else
                pnlContactInfo.Visible = False
                pnlRefSampledBy.Visible = True
                If blnContactType = LabActor.enumContactType.ReferredBy Then
                    Me.Text = "Referred By Master"
                ElseIf blnContactType = LabActor.enumContactType.SampledBy Then
                    Me.Text = "Sampled By Master"
                End If
            End If


            If nEditID > 0 Then
                Dim oContactInfo As LabActor.LabContactInformation
                Dim oContactInfoDtl As New gloEMRLabContactInfo
                oContactInfo = oContactInfoDtl.GetContactInformation(nEditID, blnContactType)
                If Not oContactInfo Is Nothing Then
                    txtContactName.Text = oContactInfo.ContactName
                    txtFirstName.Text = oContactInfo.FirstName
                    txtMiddleName.Text = oContactInfo.MiddleName
                    txtLastName.Text = oContactInfo.LastName

                    oAddresscontrol.txtAddress1.Text = oContactInfo.Address1
                    oAddresscontrol.txtAddress2.Text = oContactInfo.Address2
                    oAddresscontrol.isFormLoading = True
                    oAddresscontrol.txtZip.Text = oContactInfo.Zip
                    oAddresscontrol.isFormLoading = False
                    oAddresscontrol.txtCity.Text = oContactInfo.City
                    oAddresscontrol.cmbCountry.Text = oContactInfo.Country
                    oAddresscontrol.cmbState.Text = oContactInfo.State
                    oAddresscontrol.txtCounty.Text = oContactInfo.County
                    mskGIPhone.Text = oContactInfo.Phone
                    oContactInfo.Dispose()
                    oContactInfo = Nothing
                End If
                ' oContactInfo = Nothing
                oContactInfoDtl.Dispose()
                oContactInfoDtl = Nothing
            End If

            If blnContactType = LabActor.enumContactType.PreferredLab Then
                txtContactName.Select()
            ElseIf blnContactType = LabActor.enumContactType.ReferredBy Or blnContactType = LabActor.enumContactType.SampledBy Then
                txtFirstName.Select()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub tlsp_ContactInformation_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ContactInformation.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveContactInformation()
            Case "Close"
                Me.Close()

        End Select
    End Sub
End Class