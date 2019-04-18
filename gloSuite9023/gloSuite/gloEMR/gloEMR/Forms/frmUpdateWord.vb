Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports gloGlobal
Public Class frmUpdateWord

    ' Dim objclsPatientExams As New clsPatientExams
    'Dim objTemplate As clsTemplateGallery

    Private WithEvents oCurDoc As Wd.Document
    ' Private WithEvents oTempDoc As Wd.Document
    Dim objWord As clsWordDocument
    'Private Arrlist As ArrayList
    ' Dim dtDataDictionary As DataTable
    Private WithEvents oWordApp As Wd.Application
    Dim strIDColumn As String = ""
    Dim strResultColumn As String = ""
    Dim strTableName As String = ""

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnUpdate.Click
        Dim objDT As DataTable
        Try


            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            ts_btnClose.Enabled = False
            ts_btnUpdate.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            objWord = New clsWordDocument
            Application.DoEvents()
            If GetTableName(cmbCategory.Text) = False Then
                objWord = Nothing
                Exit Sub
            End If

            'Dim objCon As New SqlConnection(GetConnectionString)
            'Dim objDa As New SqlDataAdapter("Select " & strIDColumn & " from " & strTableName, objCon)
            'Dim objCb As New SqlCommandBuilder(objDa)
            'Dim objDs As New DataSet

            'objDa.Fill(objDs)
            lblStatus.Visible = True
            lblStatus.Text = "Initializing ..."

            objDT = GetDataforUpdate(cmbCategory.Text)

            If IsNothing(objDT) = False Then
                If objDT.Rows.Count > 0 Then

                    pgrbarStatus.Minimum = 1
                    pgrbarStatus.Maximum = objDT.Rows.Count
                    pgrbarStatus.Step = 1
                    pgrbarStatus.Visible = True
                    Application.DoEvents()

                    For i As Int32 = 0 To objDT.Rows.Count - 1
                        lblStatus.Text = "Updating ..." & i + 1 & " of " & objDT.Rows.Count - 1
                        If IsDBNull(objDT.Rows(i)(strIDColumn)) = False Then
                            UpdateWordDoc(objDT.Rows(i)(strIDColumn))
                            pgrbarStatus.Value = i + 1
                            Application.DoEvents()
                        End If

                    Next
                    Application.DoEvents()
                    lblStatus.Text = "Finalizing ..."
                    'If objDs.HasChanges Then
                    '    objDa.Update(objDs.Tables(0))
                    '    objDs.AcceptChanges()
                    lblStatus.Text = "Done ..."
                Else
                    lblStatus.Text = "No records to update ..."
                End If
                objDT.Dispose()
                objDT = Nothing
            Else
                lblStatus.Text = "No records to update ..."
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ' MsgBox(ex.ToString)
            lblStatus.Text = "Error while updating ..."
        Finally
            objDT = Nothing
            Me.Cursor = Cursors.Default
            ts_btnClose.Enabled = True
            ts_btnUpdate.Enabled = True
        End Try

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmUpdateWord_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If Not oWordApp Is Nothing Then
        '    oWordApp.RecentFiles.Maximum = 0
        '    oWordApp.DisplayRecentFiles = False
        '    Marshal.FinalReleaseComObject(oWordApp)
        'End If
    End Sub

    Private Sub frmUpdateWord_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadCategories()
    End Sub

    Private Sub loadCategories()
        With cmbCategory.Items
            .Clear()

            .Add("Template Gallery")

            .Add("Past Exams")

            .Add("Patient Messages")

            .Add("Radiology Orders")

            .Add("Patient Education")

            .Add("Patient Material")

            ' .Add("Guideline")

            '.Add("DueGuideline")

            .Add("Patient Letters")

            .Add("PT Protocols")

            .Add("Patient Concent")

            .Add("Form Gallery")

            .Add("Referral Letters")

        End With
        cmbCategory.SelectedIndex = 0
    End Sub

    Private Sub UpdateWordDoc(ByVal nTemplateId As Int64)
        'on error Resume Next

        Try

            Dim strReqFileName As String = GetDocumentcontents(nTemplateId)

            If File.Exists(strReqFileName) = False Then
                Exit Sub
            End If
            'Dim missing As Object = System.Reflection.Missing.Value
          

            ' oCurDoc = oWordApp.Documents.Open(strReqFileName, missing, missing, False, missing, missing, missing, missing, missing, missing, missing, False)

            ' wdTemplate.Open(strReqFileName)
            ' Dim oWordApp As Wd.Application = Nothing

            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdTemplate, strReqFileName, oCurDoc, oWordApp)
            'oCurDoc.ActiveWindow.SetFocus()
            oCurDoc = wdTemplate.ActiveDocument
            oWordApp = oCurDoc.Application

            Try
                If (oCurDoc.CompatibilityMode <> 65535) Then 'wdcurrent: but unable to enumerate:
                    If (oCurDoc.CompatibilityMode <> oWordApp.Version) Then
                        oCurDoc.Convert()
                    End If
                End If
            Catch ex As COMException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                'If ex.Message = "This command is not available" Then
                '    Exit Try
                'End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Dim strConvertedFileName As String = ExamNewDocumentName
            'oCurDoc.SaveAs(strConvertedFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            ''oCurDoc.Close()
            'wdTemplate.Close()
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdTemplate, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            Dim objcmd As New SqlCommand
            Dim objCon As New SqlConnection(GetConnectionString)
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text


            objcmd.Parameters.Add("@nTemplateId", SqlDbType.BigInt)
            objcmd.Parameters("@nTemplateId").Value = nTemplateId

            objcmd.Parameters.Add("@PatientNotes", SqlDbType.Image)

            '   objWord = New clsWordDocument
            If (IsNothing(myBinaray) = False) Then
                objcmd.Parameters("@PatientNotes").Value = myBinaray 'objWord.ConvertFiletoBinary(strConvertedFileName)
            Else
                objcmd.Parameters("@PatientNotes").Value = DBNull.Value
            End If

            '  objWord = Nothing
            Dim strSql As String = "Update " & strTableName & " set " & strResultColumn & " = @PatientNotes where " & strIDColumn & " = @nTemplateId"
            objcmd.CommandText = strSql

            objCon.Open()
            objcmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            If objcmd IsNot Nothing Then
                objcmd.Parameters.Clear()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            
            'If File.Exists(strConvertedFileName) Then
            '    File.Delete(strConvertedFileName)
            'End If
            If File.Exists(strReqFileName) Then
                File.Delete(strReqFileName)
            End If

            If (IsNothing(oCurDoc) = False) Then
                Try
                    Marshal.ReleaseComObject(oCurDoc)
                Catch ex As Exception


                End Try
                oCurDoc = Nothing

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.ToString)
        Finally
            'objTemplate = Nothing
            
        End Try
    End Sub
    Private Function GetDocumentcontents(ByVal nTemplateId As Int64) As String
        Dim strFileName As String = ""
        Dim oResultTable As DataTable
        Dim oDB As New DataBaseLayer
        objWord = New clsWordDocument
        Try

            Dim strSQL As String = "Select " & strResultColumn & " from " & strTableName & " where " & strIDColumn & " =  " & nTemplateId
            oResultTable = oDB.GetDataTable_Query(strSQL)

            If Not oResultTable Is Nothing Then
                If oResultTable.Rows.Count > 0 Then

                    strFileName = TempDocumentName
                    If Not IsDBNull(oResultTable.Rows(0)(0)) Then
                        strFileName = objWord.GenerateFile(oResultTable.Rows(0)(0), strFileName)

                    End If
                
                End If
                oResultTable.Dispose()
                oResultTable = Nothing
            End If
            Return strFileName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            oDB.Dispose()
            oDB = Nothing
            objWord = Nothing
        End Try
          

    End Function
    ''' <summary>
    ''' Get Data for updating the Documents 
    ''' </summary>
    ''' <param name="strTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataforUpdate(ByVal strTable As String) As DataTable

        If GetTableName(strTable) Then
            If strTableName <> "" Then
                Dim oResultTable As DataTable
                Dim oDB As New DataBaseLayer
                Try

                    Dim strSQL As String = "Select " & strIDColumn & " from " & strTableName
                    oResultTable = oDB.GetDataTable_Query(strSQL)

                    If Not oResultTable Is Nothing Then
                        If oResultTable.Rows.Count > 0 Then
                            Return oResultTable
                        Else
                            Return Nothing
                        End If
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    Return Nothing
                Finally
                    oDB.Dispose()
                    oDB = Nothing
                End Try
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
       
    End Function

    'Private Sub UpdateData(ByVal nId As Int64, ByVal sFile As String)
    '    'Dim oResultTable As New DataTable
    '    'Dim oDB As New DataBaseLayer
    '    'Try
    '    '    objWord = New clsWordDocument
    '    '    objWord.ConvertFiletoBinary(sFile)
    '    '    Dim strSQL As String = "Update " & strTableName & " set " & strResultColumn = &  objWord.ConvertFiletoBinary(sFile) " where " & strIDColumn = nId
    '    '    oResultTable = oDB.Add()


    '    'Catch ex As Exception

    '    'Finally
    '    '    oDB = Nothing
    '    'End Try
    'End Sub

    Private Function GetTableName(ByVal strTable As String) As Boolean
        Select Case strTable
            Case "Template Gallery"
                strIDColumn = "nTemplateID"
                strResultColumn = "sDescription"
                strTableName = "TemplateGallery_MST"
                Return True
            Case "Past Exams"
                strIDColumn = "nExamID"
                strResultColumn = "sPatientNotes"
                strTableName = "PatientExams"
                Return True

            Case "Patient Messages"
                strTableName = "Message"
                strIDColumn = "nMessageID"
                strResultColumn = "sResult"
                Return True

            Case "Radiology Orders"
                strTableName = "Orders"
                strIDColumn = "nOrderID"
                strResultColumn = "sResult"
                Return True

            Case "Patient Education"
                strTableName = "ExamEducation"
                strIDColumn = "nEducationID"
                strResultColumn = "sPENotes"

                Return True
            Case "Patient Material"
                strTableName = "PatientMaterial"
                strIDColumn = "nID"
                strResultColumn = "sTemplate"
                Return True

                'Case "Guideline"
                '    strTableName = "DM_Tran"
                '    Return True
                'Case "DueGuideline"
                '    strTableName = "DM_DueGuideline"
                '    Return True
            Case "Patient Letters"
                strTableName = "PatientLetters"
                strIDColumn = "nLetterID"
                strResultColumn = "sPatientLetter"
                Return True
            Case "PT Protocols"
                strTableName = "PTProtocols"
                strIDColumn = "nProtocolID"
                strResultColumn = "sProtocol"
                Return True
            Case "Patient Concent"
                strTableName = "PatientConsent"
                strIDColumn = "nConsentId"
                strResultColumn = "sPatientConsent"
                Return True
            Case "Form Gallery"
                strTableName = "FormGallery"
                strIDColumn = "nFormID"
                strResultColumn = "sResult"
                Return True
            Case "Referral Letters"
                strTableName = "Referrals"
                strIDColumn = "nReferralID"
                strResultColumn = "imgTemplate"
                Return True
            Case Else
                Return False

        End Select

    End Function

    Public ReadOnly Property TempDocumentName() As String
        Get
            'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            'Dim _NewDocumentName As String = ""
            'Dim _Extension As String = ".doc"
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _Extension
            'While File.Exists(_Path & "\" & _NewDocumentName) = True And i < Integer.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _Extension
            'End While
            'Return _Path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".doc", "MMddyyyyHHmmssffff")

        End Get
    End Property

    Private Sub wdTemplate_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdTemplate.BeforeDocumentClosed
        Try

            If Not oWordApp Is Nothing Then
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdTemplate_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdTemplate.OnDocumentClosed
        Try
            ''Release the Document and application object References
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '    Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdTemplate_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdTemplate.OnDocumentOpened
        oCurDoc = e.document
        oWordApp = oCurDoc.Application

    End Sub
End Class