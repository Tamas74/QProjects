Imports System.IO
Module mdlFAX
    Enum enmFAXPriority
        NormalPriority
        SendImmediately
    End Enum

    Public CurrentSendingFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority

    Public gstrFAXContactPerson As String
    Public gstrFAXContactPersonFAXNo As String
    Public gstrFAXCoverPage As String = ""
    Public gstrFAXType As String = ""
    Private _Owner As Form = Nothing
    'variable  added by dipak 20091110 for temp storage of gblnnFAXCoverPage to fix 4714 -Labs fax cover page
    Private nFAXCoverPage As Boolean

    Public Property Owner() As Form
        Get
            Return _Owner
        End Get
        Set(ByVal value As Form)
            _Owner = value
        End Set
    End Property

    Public Enum enmFAXType
        PatientExam
        PatientLetters
        PatientMessages
        PatientOrders
        FormGallery
        ReferralLetter
        Prescription
        PTProtocols
        Medication
        PatientMaterials
        NurseNotes
        'sarika 10th oct 07
        Labs
        'sarika bug 877 30th july 08
        PatientConsent
        DisclosureManagement
    End Enum

    'sarika 13th nov 07   -- one fax to multiple recipients
    Public multipleRecipients As Boolean = False
    Public gstrFAXContacts As Collection = Nothing
    
    Public Function isPrinterSettingsSet(Optional ByVal blnShowMessageBox As Boolean = False) As Boolean
        If gblnInternetFax = True Then
            Return True
        End If
        'Check TIFF Printer is set or not
        If Trim(gstrFAXPrinterName) = "" Then
            'TIFF Printer is NOT set
            If blnShowMessageBox = True Then
                MessageBox.Show("FAX Printer has not been set. Please set the FAX Printer in Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        End If
        'Check FAX Output Directory is set or not
        If Trim(gstrFAXOutputDirectory) = "" Then
            'FAX Output Directory is not SET
            If blnShowMessageBox = True Then
                MessageBox.Show("FAX Output directory has not been set. Please set the FAX Output Directory in Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        End If
        'Check FAX Output Directory is exists or not
        If Directory.Exists(gstrFAXOutputDirectory) = False Then
            'FAX Output Directory is NOT exists
            If blnShowMessageBox = True Then
                MessageBox.Show(gstrFAXOutputDirectory & " is not valid path." & vbCrLf & "Please set it again Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        End If
        Return True
    End Function
    ''For Fax Module Changes
    Public gstrfaxCollection As New Collection    
    
    Public Function RetrieveFAXDetails(ByVal enmFAXDocumentType As enmFAXType, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sFAXTypeDetails As String, Optional ByVal nReferralID As Long = 0, Optional ByVal nVisitID As Long = 0, Optional ByVal nEXAMID As Long = 0, Optional ByVal blnFirstReferralLetter As Boolean = True, Optional ByVal oParentForm As Form = Nothing) As Boolean
        UpdateLog("In the RetrieveFAXDetails method")

        'sarika 13th nov 07   -- one fax to multiple recipients
        multipleRecipients = False
        gstrFAXContacts = Nothing

        gblnFAXPrinterSettingsSet = isPrinterSettingsSet(True)

        'Check Printer settings are set or not
        If gblnFAXPrinterSettingsSet = False Then
            Return False
        End If
        'Check FAX Cover Page is enabled or not
        If gblnFAXCoverPage Then
            UpdateLog("Cover Page FAX Setting is enabled")
            'Cover Page FAX Setting is enabled
            'Check the 'Patient Exam' Cover Page Template exists or not
            Dim blnTemplateAvailable As Boolean = False
            'Before, User has to design the different cover page for different FAX types
            'Now cover will be same for all types by the Name = 'Cover Page'
            UpdateLog("Check Template is exists or not")
            Dim objTemplate As New clsPatientExams
            blnTemplateAvailable = objTemplate.CheckFAXCoverPageTemplateExists()
            objTemplate.Dispose()
            objTemplate = Nothing
            If blnTemplateAvailable = False Then
                'Patient Exam Cover Page does not exists
                MessageBox.Show("FAX cover page template does not exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            UpdateLog("Template exists")
        End If
        'FAX Type - Patient Exam with Exam Name
        UpdateLog("Retrieveing FAX Document Type")
        Select Case enmFAXDocumentType
            Case enmFAXType.FormGallery
                gstrFAXType = "Form Gallery"
            Case enmFAXType.PatientExam
                gstrFAXType = "Patient Exam"
            Case enmFAXType.PatientLetters
                gstrFAXType = "Patient Letter"
            Case enmFAXType.PatientMessages
                gstrFAXType = "Patient Message"
            Case enmFAXType.PatientOrders
                gstrFAXType = "Patient Order"
            Case enmFAXType.Prescription
                gstrFAXType = "Prescription"
            Case enmFAXType.PTProtocols
                gstrFAXType = "PT Protocol"
            Case enmFAXType.ReferralLetter
                gstrFAXType = "Referral Letter"
            Case enmFAXType.PatientMaterials
                gstrFAXType = "Patient Materials"
            Case enmFAXType.Labs
                gstrFAXType = "Labs"
                'sarika bug 877 30 july 08
            Case enmFAXType.PatientConsent
                gstrFAXType = "Patient Consent"
            Case enmFAXType.NurseNotes
                gstrFAXType = "Nurse Notes"
            Case enmFAXType.DisclosureManagement
                gstrFAXType = "Disclosure Management"
        End Select

        UpdateLog("FAX Document Type retrieved")


        '' GLO2011-0012778
        '' Patient Exam > Ref Letters > Fax /FaxAll was not working as gstrfaxCollection was holding last sent contacts so cleared the colletion 
        '' "Fax" Without any cover page from the Exam Referrals Sends the Fax to same person each and every time.
        If (IsNothing(gstrfaxCollection) = False) Then
            gstrfaxCollection.Clear()
        Else
            gstrfaxCollection = New Collection
        End If

        'Retrieve the FAX To & FAX No
        Select Case enmFAXDocumentType
            Case enmFAXType.PatientExam, enmFAXType.PatientLetters, enmFAXType.PatientMessages, enmFAXType.PatientOrders, enmFAXType.PTProtocols, enmFAXType.FormGallery, enmFAXType.PatientMaterials, enmFAXType.Labs, enmFAXType.PatientConsent, enmFAXType.NurseNotes, enmFAXType.DisclosureManagement
                gstrFAXContactPerson = ""
                gstrFAXContactPersonFAXNo = ""
                UpdateLog("Creating object of frmSelectContactFAXWithFAXCoverPage")
              
                Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                frm.IsOpenedFromExam = True  ''Sandip Darade 20100204
                frmSelectContactFAXWithFAXCoverPage.PatientID = nPatientID
                UpdateLog("Object created")

                'sarika 20090717 Hide Cover Page for Lab Orders
                'line  added by dipak 20091110 for temp storage of gblnnFAXCoverPage to fix 4714 -Labs fax cover page
                nFAXCoverPage = gblnFAXCoverPage
                If enmFAXDocumentType = enmFAXType.Labs Then
                    gblnFAXCoverPage = False
                End If

                If gblnFAXCoverPage = False Then
                    UpdateLog("No FAX Cover Page")
                    'frm.pnlCoverPage.Visible = False
                    frm.pnlFaxCoverPg.Visible = False
                    frm.Panel4.Visible = False
                    frm.Splitter1.Visible = False
                    frm.btnUp1.Visible = True
                    frm.btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
                    frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                    frm.btnDown1.Visible = False
                    frm.pnlSelectFAXNo.Dock = DockStyle.Fill
                Else
                    UpdateLog("FAX Cover Page enabled.")
                    frmSelectContactFAXWithFAXCoverPage.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    frm.btnUp1.Visible = True
                    frm.btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
                    frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                    frm.btnDown1.Visible = False
                    UpdateLog("Loading FAX Cover Page")
                    frm.LoadFAXCoverPage()
                    UpdateLog("FAX Cover Page loaded")
                End If
                If frm.ShowDialog(oParentForm) = DialogResult.OK Then
                    gstrFAXCoverPage = frm.strFAXCoverPage
                Else
                    gblnFAXCoverPage = nFAXCoverPage
                    'Change made to solve memory Leak and word crash issue   
                    frm.Close()
                    frm.Dispose()
                    frm = Nothing
                    Return False

                End If

                ''Fax Module Change.
                gstrFAXCoverPage = Nothing
              
                '' Fax Collection Details 
                'SLR: Commented on 7/27/2015 gstrfaxCollection = frm.FAXContactsCollection
                ''Fax Module Change.

                'Line  added by dipak 20091110 for temp storage of gblnnFAXCoverPage to fix 4714 -Labs fax cover page
                'previous value restore
                gblnFAXCoverPage = nFAXCoverPage
                'end code added by dipak 20091110
                'Change made to solve memory Leak and word crash issue
                frm.Close()
                frm.Dispose()
                frm = Nothing
            Case enmFAXType.Prescription
                If gblnFAXCoverPage Then
                   
                    Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                    frm.pnlSelectFAXNo.Visible = False
                    frm.ts_btnShowGrid.Visible = False
                    frmSelectContactFAXWithFAXCoverPage.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    frm.LoadFAXCoverPage()
                    frm.lblContactPerson.Text = sFAXTo
                    frm.txtFAXNo.Text = sFAXNo
                    frm.pnlCoverPage.Dock = DockStyle.Fill
                    frm.btnUpdateFAXNo1.Visible = False
                    'Dim objSender As Object = Nothing
                    'Dim objE As EventArgs = Nothing
                    'frm.btnRefreshCoverPage_Click(objSender, objE)
                    If frm.ShowDialog(oParentForm) = DialogResult.OK Then
                        gstrFAXCoverPage = frm.strFAXCoverPage
                        '  File.Copy(gstrFAXCoverPage, "C:\gloEMRPrescriptionCoverPage.docx", True)
                    Else
                        frm.Close()
                        frm.Dispose()
                        frm = Nothing
                        Return False
                    End If
                    gstrFAXCoverPage = frm.strFAXCoverPage
                    ' File.Copy(gstrFAXCoverPage, "C:\gloEMRPrescriptionCoverPage.docx", True)
                    ''Fax Module Change
                    '' Fax Collection Details 
                  
                    'SLR: Commented on 7/27/2015 gstrfaxCollection = frm.FAXContactsCollection
                    ''Fax Module Change
                    'Change made to solve memory Leak and word crash issue
                    frm.Close()
                    frm.Dispose()
                    frm = Nothing
                    frm = Nothing
                End If

            Case enmFAXType.ReferralLetter
                'Find the Referral FAX No
                gstrFAXContactPerson = sFAXTo
                Dim objReferralNo As New clsFAX
                gstrFAXContactPersonFAXNo = objReferralNo.GetContactFAXNo(nReferralID)

                objReferralNo.Dispose() 'Change made to solve memory Leak and word crash issue
                objReferralNo = Nothing
                If gblnFAXCoverPage = False Then
                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox(sFAXTo & " FAX No. has not been set." & vbCrLf & "Please enter the FAX No", gstrMessageBoxCaption)
                    End If
                Else
                    
                    Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                    frm.btnUp1.Visible = False
                    frm.btnDown1.Visible = False
                    frm.pnlSelectFAXNo.Visible = False
                    frm.ts_btnShowGrid.Visible = False
                    frmSelectContactFAXWithFAXCoverPage.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    UpdateLog("***********LoadFaxCoverpage Started**************")
                    frm.IsFirstReferralLetter = blnFirstReferralLetter
                    frm.LoadFAXCoverPage(blnFirstReferralLetter)
                    UpdateLog("***********LoadFaxCoverpage End**************")
                    frm.lblContactPerson.Text = gstrFAXContactPerson
                    frm.txtFAXNo.Text = gstrFAXContactPersonFAXNo
                    frm.mskFaxNo.Text = gstrFAXContactPersonFAXNo ''Sandip Darade 20100120
                    frm.btnUpdateFAXNo1.Visible = False
                    'sarika 29th nov 07
                    frm.pnlLeft.Visible = False
                    frm.pnlBottom.Dock = DockStyle.Fill
                    frm.Panel2.Dock = DockStyle.Top
                    frm.dsoFAXPreview.Dock = DockStyle.Fill
                    '-------------
                    'Dim objSender As Object = Nothing
                    'Dim objE As EventArgs = Nothing
                    'frm.btnRefreshCoverPage_Click(objSender, objE)
                    If frm.ShowDialog(oParentForm) = DialogResult.OK Then
                    Else
                        ''For Fax Module Changes
                        'Change made to solve memory Leak and word crash issue
                        frm.Close()
                        frm.Dispose()
                        frm = Nothing
                        ''For Fax Module Changes
                        Return False
                    End If
                    ''For Fax Module change
                    '' Fax Collection Details 

                    'SLR: Commented on 7/27/2015 gstrfaxCollection = frm.FAXContactsCollection
                    ''For Fax Module change
                    'Change made to solve memory Leak and word crash issue
                    frm.Close()
                    frm.Dispose()
                    frm = Nothing
                End If

        End Select
        ''For Fax Module Changes.
        If gstrfaxCollection.Count = 0 Then
            Dim node As New myTreeNode
            node.FaxCoverPage = ""
            node.FaxTo = gstrFAXContactPersonFAXNo
            node.FaxName = gstrFAXContactPerson
            gstrfaxCollection.Add(node)
            node = Nothing 'Change made to solve memory Leak and word crash issue
        End If
        ''For Fax Module Changes.

        'code added by sarika 13th nov 07
        'Select the FAX To & FAX No
        If multipleRecipients = False Then
            If Trim(gstrFAXContactPersonFAXNo) = "" And enmFAXDocumentType <> enmFAXType.Medication Then
                MessageBox.Show("The fax number has not been set up for the recipient in Contacts. This fax will not be sent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Else
            ''Added fax Module Change
            If gstrfaxCollection.Count = 0 And enmFAXDocumentType <> enmFAXType.Medication Then
                ''Added fax Module Change
                MessageBox.Show("The fax number has not been set up for the recipient in Contacts. This fax will not be sent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        End If
        '----------------------
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Fax, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, gstrFAXType & "(s)" & " faxed successfully", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
        If Trim(sFAXTypeDetails) <> "" Then
            gstrFAXType = gstrFAXType & " - " & sFAXTypeDetails
        End If



        Return True
    End Function

    Public Function BatchReferralsFAXDetails(ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sFAXTypeDetails As String, ByVal nReferralID As Int64, Optional ByVal blnFirstReferralLetter As Boolean = True) As String
        UpdateLog("In the RetrieveFAXDetails method")
        'Check Printer settings are set or not
        If gblnFAXPrinterSettingsSet = False Then
            'All necessary FAX Settings are not set
            UpdateLog("Setting FAX Printer settings")
            ' isPrinterSettingsSet(True)
            UpdateLog("FAX Printer settings set")
            Return "FAX Printer settings not set"
        End If
        'Check FAX Cover Page is enabled or not
        If gblnFAXCoverPage Then
            UpdateLog("Cover Page FAX Setting is enabled")
            'Cover Page FAX Setting is enabled
            'Check the 'Patient Exam' Cover Page Template exists or not
            Dim blnTemplateAvailable As Boolean = False
            'Before, User has to design the different cover page for different FAX types
            'Now cover will be same for all types by the Name = 'Cover Page'
            UpdateLog("Check Template is exists or not")
            Dim objTemplate As New clsPatientExams
            blnTemplateAvailable = objTemplate.CheckFAXCoverPageTemplateExists()
            objTemplate.Dispose() 'Change made to solve memory Leak and word crash issue
            objTemplate = Nothing
            If blnTemplateAvailable = False Then
                ' 'Patient Exam Cover Page does not exists
                Return "Patient Exam Cover Page does not exists"

            End If
            UpdateLog("Template exists")
        End If
        'FAX Type - Patient Exam with Exam Name
        UpdateLog("Retrieveing FAX Document Type")
        gstrFAXType = "Referral Letter"
        UpdateLog("FAX Document Type retrieved")

        'Retrieve the FAX To & FAX No
        gstrFAXContactPerson = sFAXTo
        Dim objReferralNo As New clsFAX
        If (gblnIsSelectRefContact = False) Then ''Sandip Darade 2010213
            gstrFAXContactPersonFAXNo = objReferralNo.GetContactFAXNo(nReferralID)
        Else
            gstrFAXContactPersonFAXNo = sFAXNo
        End If
        objReferralNo.Dispose() 'Change made to solve memory Leak and word crash issue
        objReferralNo = Nothing
        If Trim(gstrFAXContactPersonFAXNo) = "" Then
            Return "FAX No. has not been set"
        End If
        If Trim(sFAXTypeDetails) <> "" Then
            gstrFAXType = gstrFAXType & " - " & sFAXTypeDetails
        End If
        Return "Success"
    End Function
End Module
