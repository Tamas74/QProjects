Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord

Public Class frmCCD_ViewDetails
    Private mCCDFileName As String = ""

    Private WithEvents oCurDoc1 As Wd.Document
    Dim oWordApp As Wd.Application = Nothing
    Private Sub frmCCD_ViewDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim TempFilePath As String
            TempFilePath = RetrieveDocumentFile(mCCDFileName)

            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdMessages, TempFilePath, oCurDoc1, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, strError, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(strError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            '  wdMessages.Open(TempFilePath)
            ' wdMessages.Open("C:\ProblemStatement.docx")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function RetrieveDocumentFile(ByVal CCDFileName As String) As String
        Dim oResult As Object = Nothing
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand("CCD_RetrieveFile", conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CCDFileName
            conn.Open()
            oResult = cmd.ExecuteScalar()

            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = ExamNewDocumentName
                '' generate Physical file
                strFileName = GenerateFile(oResult, strFileName)
                Return strFileName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try


    End Function

    Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
        Try
            If Not cntFromDB Is Nothing Then
                Dim content() As Byte = CType(cntFromDB, Byte())
                'Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                ' stream.WriteTo(oFile)
                oFile.Close()
                oFile.Dispose()
                'stream.Close()
                'stream.Dispose()
                content = Nothing
                'stream = Nothing
                oFile = Nothing
                Return strFileName
            Else
                Return Nothing

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try

    End Function

    Public Sub New(ByVal CCDFileName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mCCDFileName = CCDFileName
    End Sub

    Private Sub wdMessages_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdMessages.BeforeDocumentClosed

        Try
           
            If Not oCurDoc1 Is Nothing Then
                If (IsNothing(oWordApp)) Then
                    Try
                        oWordApp = oCurDoc1.Application
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                End If
                If (IsNothing(oWordApp) = False) Then


                    For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                        If (IsNothing(oFile) = False) Then
                            Try
                                If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                    Try
                                        oFile.Delete()
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
                                        ex = Nothing
                                    End Try
                                End If
                            Catch ex As Exception
                                
                            End Try
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub


    Private Sub wdMessages_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdMessages.OnDocumentClosed

        Try

            If Not oCurDoc1 Is Nothing Then
                '' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit
                Marshal.ReleaseComObject(oCurDoc1)
                oCurDoc1 = Nothing
            End If
            'If Not oWordApp Is Nothing Then

            '    '  Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdMessages_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdMessages.OnDocumentOpened
        oCurDoc1 = wdMessages.ActiveDocument
        '  oWordApp = oCurDoc1.Application
        Try

        Catch ex As Exception
        Finally

        End Try

        oCurDoc1.FormFields.Shaded = False
        oCurDoc1.ActiveWindow.SetFocus()
    End Sub

End Class