Imports System.Text
Imports System.Linq

Public Class frmDisplayCDAErrors
    Private r As RootObject = Nothing
    Private _cdaerrorid As Int64 = 0
    'Private sFilename As String = ""
    Public Property SetRootOBJECT As RootObject
        Get
            Return r
        End Get
        Set(value As RootObject)
            r = value
        End Set
    End Property
    Public Sub New(ByVal ncdaerrorid As Int64)
        _cdaerrorid = ncdaerrorid
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Public Property DocumentName As String
    '    Get
    '        Return sFilename
    '    End Get
    '    Set(value As String)
    '        sFilename = value
    '    End Set
    'End Property
    Private Sub frmDisplayCDAErrors_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DisplayCDAError()
    End Sub
    Private Sub DisplayCDAError()
        'lblVocabErrors.Text = "      ONC 2015 S&&CC Vocabulary Validation Conformance Error"
        Try
            Dim restURL As StringBuilder = New StringBuilder()
            Dim errorcount As Integer = 0
            Dim warningcount As Integer = 0
            Dim Infocount As Integer = 0
            Dim Vocaberrorcount As Integer = 0
            Dim Vocabwarningcount As Integer = 0
            Dim Vocabinfocount As Integer = 0
            Dim Referenceerrorcount As Integer = 0
            Dim Referencewarningcount As Integer = 0
            Dim Referenceinfocount As Integer = 0
            If r.Validation.resultsMetaData.resultMetaData IsNot Nothing Then
                If r.Validation.resultsMetaData.resultMetaData.Count > 0 Then
                    Dim ErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "C-CDA MDHT Conformance Error").First()
                    errorcount = ErrorList.count
                    lblHeader.Text = "       C-CDA MDHT Conformance Error : " & errorcount & ""
                    tbCCDAIG.Text = "C-CDA IG Errors : " & errorcount & ""
                    tbCCDAIG.Tag = "Error Count : " & errorcount & ""
                    Dim WarningList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "C-CDA MDHT Conformance Warning").First()
                    warningcount = DirectCast(WarningList, gloCCDLibrary.ResultMetaData).count
                    lblWarningHeader.Text = "       C-CDA MDHT Conformance Warning : " & warningcount & ""
                    Dim InfoList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "C-CDA MDHT Conformance Info").First()
                    Infocount = DirectCast(InfoList, gloCCDLibrary.ResultMetaData).count
                    lblInfoHeader.Text = "       C-CDA MDHT Conformance Info : " & Infocount & ""
                    Dim VocabErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Vocabulary Validation Conformance Error").First()
                    Vocaberrorcount = DirectCast(VocabErrorList, gloCCDLibrary.ResultMetaData).count
                    lblVocabErrors.Text = "       ONC 2015 S&&CC Vocabulary Validation Conformance Error : " & Vocaberrorcount & ""
                    tbCCDAVOCAB.Text = "C-CDA VOCAB Errors : " & Vocaberrorcount & ""
                    tbCCDAVOCAB.Tag = "Error Count : " & Vocaberrorcount & ""
                    Dim VocabwarningList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Vocabulary Validation Conformance Warning").First()
                    Vocabwarningcount = DirectCast(VocabwarningList, gloCCDLibrary.ResultMetaData).count
                    lblVocabWarnings.Text = "       ONC 2015 S&&CC Vocabulary Validation Conformance Warning : " & Vocabwarningcount & ""
                    Dim VocabInfoList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Vocabulary Validation Conformance Info").First()
                    Vocabinfocount = DirectCast(VocabInfoList, gloCCDLibrary.ResultMetaData).count
                    lblVocabInfo.Text = "       ONC 2015 S&&CC Vocabulary Validation Conformance Info : " & Vocabinfocount & ""
                    Dim ReferenceErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Reference C-CDA Validation Error").First()
                    Referenceerrorcount = DirectCast(ReferenceErrorList, gloCCDLibrary.ResultMetaData).count
                    lblReferenceError.Text = "       ONC 2015 S&&CC Reference C-CDA Validation Error : " & Referenceerrorcount & ""
                    tbRefCCDA.Text = "REF C-CDA Errors : " & Referenceerrorcount & ""
                    tbRefCCDA.Tag = "Error Count : " & Referenceerrorcount & ""
                    Dim ReferencewarningList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Reference C-CDA Validation Warning").First()
                    Referencewarningcount = DirectCast(ReferencewarningList, gloCCDLibrary.ResultMetaData).count
                    lblReferenceWarning.Text = "       ONC 2015 S&&CC Reference C-CDA Validation Warning : " & Referencewarningcount & ""
                    Dim ReferenceInfoList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Reference C-CDA Validation Info").First()
                    Referenceinfocount = DirectCast(ReferenceInfoList, gloCCDLibrary.ResultMetaData).count
                    lblReferenceInfo.Text = "       ONC 2015 S&&CC Reference C-CDA Validation Info : " & Referenceinfocount & ""
                End If
            End If



            restURL.Append("{\rtf1\ansi")
            If errorcount > 0 Then
                'restURL.Append(" \b  Error Count in file: \b0 " & errorcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "C-CDA MDHT Conformance Error" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList

                    Dim type As String = ccdavalidation.type
                    If type.Equals("C-CDA MDHT Conformance Error", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next

                restURL.Append("}")
            End If
            Me.txtCDAErrorDisplay.Rtf = restURL.ToString()

            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If warningcount > 0 Then
                'restURL.Append(" \b  Warnings in file: \b0 " & warningcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "C-CDA MDHT Conformance Warning" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("C-CDA MDHT Conformance Warning", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtDisplayWarnings.Rtf = restURL.ToString()


            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Infocount > 0 Then
                'restURL.Append(" \b  Information in file: \b0 " & Infocount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "C-CDA MDHT Conformance Info" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("C-CDA MDHT Conformance Info", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtDisplayInfo.Rtf = restURL.ToString()

            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Vocaberrorcount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Vocabulary Validation Conformance Error: \b0 " & Vocaberrorcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Vocabulary Validation Conformance Error" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Vocabulary Validation Conformance Error", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtVocabErrors.Rtf = restURL.ToString()

            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Vocabwarningcount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Vocabulary Validation Conformance Warning: \b0 " & Vocabwarningcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Vocabulary Validation Conformance Warning" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Vocabulary Validation Conformance Warning", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtVocabWarnings.Rtf = restURL.ToString()

            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Vocabinfocount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Vocabulary Validation Conformance Info: \b0 " & Vocabinfocount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Vocabulary Validation Conformance Info" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Vocabulary Validation Conformance Info", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtVocabInfo.Rtf = restURL.ToString()

            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Referenceerrorcount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Reference C-CDA Validation Error: \b0 " & Referenceerrorcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Reference C-CDA Validation Error" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Reference C-CDA Validation Error", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtRefErrors.Rtf = restURL.ToString()


            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Referencewarningcount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Reference C-CDA Validation Warning: \b0 " & Referencewarningcount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Reference C-CDA Validation Warning" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Reference C-CDA Validation Warning", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtRefWarnings.Rtf = restURL.ToString()


            restURL = New StringBuilder
            restURL.Append("{\rtf1\ansi")
            If Referenceinfocount > 0 Then
                'restURL.Append(" \b  ONC 2015 S&CC Reference C-CDA Validation Info: \b0 " & Referenceinfocount)
                'restURL.Append("\line")
                'restURL.Append("\line")
                'restURL.Append("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                Dim ccdavalidationErrorList = (From ccdavalidation In r.Validation.ccdaValidationResults Where ccdavalidation.type = "ONC 2015 S&CC Reference C-CDA Validation Info" Select ccdavalidation).ToList()
                Dim count As Integer = 1
                For Each ccdavalidation As CcdaValidationResult In ccdavalidationErrorList
                    Dim type As String = ccdavalidation.type
                    If type.Equals("ONC 2015 S&CC Reference C-CDA Validation Info", StringComparison.OrdinalIgnoreCase) Then
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" " & count & ")\b  Description: \b0 " & ccdavalidation.description)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append(" \b xPath: \b0 " & ccdavalidation.xPath)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("\b DocumentLineNumber: \b0 " & ccdavalidation.documentLineNumber)
                        restURL.Append("\line")
                        restURL.Append("\line")
                        restURL.Append("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        count += 1
                    End If
                Next
                restURL.Append("}")
            End If
            Me.txtRefInfo.Rtf = restURL.ToString()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, "CDA Errors Viewed.", 0, _cdaerrorid, 0, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        
    End Sub


    Private Sub btnPrv_UpWarnings_Click(sender As System.Object, e As System.EventArgs) Handles btnPrv_UpWarnings.Click
        PnlWarnings.Visible = False
        btnPrv_UpWarnings.Visible = False
        btnPrv_DownWarnings.Visible = True
    End Sub

    Private Sub btnPrv_DownWarnings_Click(sender As System.Object, e As System.EventArgs) Handles btnPrv_DownWarnings.Click
        PnlWarnings.Visible = True
        btnPrv_DownWarnings.Visible = False
        btnPrv_UpWarnings.Visible = True
    End Sub

    Private Sub btnPrv_UpInformation_Click(sender As System.Object, e As System.EventArgs) Handles btnPrv_UpInformation.Click
        PnlInformation.Visible = False
        btnPrv_UpInformation.Visible = False
        btnPrv_DownInformation.Visible = True
    End Sub

    Private Sub btnPrv_DownInformation_Click(sender As System.Object, e As System.EventArgs) Handles btnPrv_DownInformation.Click
        PnlInformation.Visible = True
        btnPrv_DownInformation.Visible = False
        btnPrv_UpInformation.Visible = True
    End Sub

    Private Sub btnUpVocabWarning_Click(sender As System.Object, e As System.EventArgs) Handles btnUpVocabWarning.Click
        PnlVocabWarnings.Visible = False
        btnUpVocabWarning.Visible = False
        btnDownVocabWarning.Visible = True
    End Sub

    Private Sub btnDownVocabWarning_Click(sender As System.Object, e As System.EventArgs) Handles btnDownVocabWarning.Click
        PnlVocabWarnings.Visible = True
        btnUpVocabWarning.Visible = True
        btnDownVocabWarning.Visible = False
    End Sub

    Private Sub btnUpReferenceWarning_Click(sender As System.Object, e As System.EventArgs) Handles btnUpReferenceWarning.Click
        PnlRefWarnings.Visible = False
        btnUpReferenceWarning.Visible = False
        btnDownReferenceWarning.Visible = True
    End Sub

    Private Sub btnDownReferenceWarning_Click(sender As System.Object, e As System.EventArgs) Handles btnDownReferenceWarning.Click
        PnlRefWarnings.Visible = True
        btnUpReferenceWarning.Visible = True
        btnDownReferenceWarning.Visible = False
    End Sub

    Private Sub btnUpReferenceInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnUpReferenceInfo.Click
        PnlRefInfo.Visible = False
        btnUpReferenceInfo.Visible = False
        btnDownReferInfo.Visible = True
    End Sub

    Private Sub btnDownReferInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnDownReferInfo.Click
        PnlRefInfo.Visible = True
        btnUpReferenceInfo.Visible = True
        btnDownReferInfo.Visible = False
    End Sub

    Private Sub btnUpVocabInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnUpVocabInfo.Click
        PnlVocabInfo.Visible = False
        btnUpVocabInfo.Visible = False
        btnDownVocabInfo.Visible = True
    End Sub

    Private Sub btnDownVocabInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnDownVocabInfo.Click
        PnlVocabInfo.Visible = True
        btnUpVocabInfo.Visible = True
        btnDownVocabInfo.Visible = False
    End Sub

    Private Sub tblClose_Click(sender As System.Object, e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub
End Class