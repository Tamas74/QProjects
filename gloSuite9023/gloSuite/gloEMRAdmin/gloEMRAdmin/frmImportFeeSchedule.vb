Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Win32

Partial Public Class frmImportFeeSchedule
    Inherits Form

#Region " Declarations "

    Private _databaseconnectionstring As String = gstrConnectionString
    Private _messageBoxCaption As String = gstrMessageBoxCaption

    Private _DatabaseName As String = gstrDatabaseName
    Private _SQLServerName As String = gstrSQLServerName
    Private _SQLLoginName As String = gstrSQLUser
    Private _SQLPassword As String = gstrSQLPassword
    Private _IsWindowAuthentication As Boolean = gblnWindowsAuthentication

    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private _ClinicID As Int64 = gnClinicID

#End Region

#Region "Contructor"

    Public Sub New()
      
        InitializeComponent()
    End Sub

#End Region

    Private Sub frmImportFeeSchedule_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub

#Region "Button Click Events"

    Private Sub btn_Browse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Browse.Click
        Try
            dlgBrowseFile.Title = " Browse File "
            dlgBrowseFile.Filter = "Excel Files(*.csv)|*.csv"
            dlgBrowseFile.CheckFileExists = True
            dlgBrowseFile.Multiselect = False
            dlgBrowseFile.ShowHelp = False
            dlgBrowseFile.ShowReadOnly = False

            If dlgBrowseFile.ShowDialog() = DialogResult.OK Then

                txtImportFile.Text = dlgBrowseFile.FileName
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub tsb_Import_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsb_Import.Click
        Dim _Result As Boolean = False
        Try
            If ValidateData() = True Then
                tsb_Import.Enabled = False
                Me.Cursor = Cursors.WaitCursor
                Dim nFeeScheduleID As Int64 = 0
                nFeeScheduleID = GetFeeScheduleID()

                Dim sFormatedCSVFileName As String = FormatCSVFile(txtImportFile.Text.Trim(), nFeeScheduleID)

                If sFormatedCSVFileName.Trim() <> "" Then
                    Dim sBatchFilePath As String = CreateBatchFile(sFormatedCSVFileName)

                    If sBatchFilePath.Trim() <> "" Then
                        Dim proc As New System.Diagnostics.Process()
                        proc.EnableRaisingEvents = False
                        proc.StartInfo.FileName = sBatchFilePath
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        proc.Start()
                        proc.WaitForExit()
                        If proc.HasExited = True Then
                            If proc.ExitCode = 0 Then
                                SaveFeeSchedule(nFeeScheduleID)
                                _Result = True
                            Else
                                Me.Cursor = Cursors.[Default]
                                MessageBox.Show("Error while inserting data. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                            End If
                        End If

                        If File.Exists(sBatchFilePath) = True Then
                            File.Delete(sBatchFilePath)
                        End If
                    Else
                        Me.Cursor = Cursors.[Default]
                        MessageBox.Show("Error while creating batch file. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    End If


                    If File.Exists(sFormatedCSVFileName) = True Then
                        File.Delete(sFormatedCSVFileName)
                    End If
                Else
                    Me.Cursor = Cursors.[Default]
                    MessageBox.Show("Error while reading source file. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            Me.Cursor = Cursors.[Default]
            tsb_Import.Enabled = True
        End Try

        If _Result = True Then
            Me.Close()
        End If
    End Sub

    Private Sub tsb_Close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsb_Close.Click
        Me.Close()
    End Sub

#End Region

    Private Function ValidateData() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            If txtFeeScheduleName.Text.Trim() = "" Then
                MessageBox.Show("Please enter fee schedule name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtFeeScheduleName.Focus()
                Return False
            End If
            If txtImportFile.Text.Trim() = "" Then
                MessageBox.Show("Please select file to import. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btn_Browse.Focus()
                Return False
            End If

            If File.Exists(txtImportFile.Text.Trim()) = False Then
                MessageBox.Show("Source file does not exists. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btn_Browse.Focus()
                Return False
            End If

            oDB.Connect(False)
            Dim _sqlQuery As String = " SELECT Count(*)  FROM BL_FeeSchedule_MST  WHERE sFeeScheduleName = '" & txtFeeScheduleName.Text.Trim() & "' AND nClinicID = " & _ClinicID & " "

            Dim _Name As Object = oDB.ExecuteScalar_Query(_sqlQuery)
            oDB.Disconnect()

            If Convert.ToInt64(_Name) > 0 Then
                MessageBox.Show("Fee schedule name is already exists. Please select a unique name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        Catch odbEx As gloDatabaseLayer.DBException
            odbEx.ERROR_Log(odbEx.ToString())
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False
        Finally
            oDB.Dispose()
        End Try

        Return True
    End Function

    Private Function FormatCSVFile(ByVal sFileName As String, ByVal nFeeScheduleID As Int64) As String
        Dim _result As String = ""

        Try

            Dim sNewFileName As String = sFileName.ToUpper().Replace(".CSV", "_New.CSV")
            Dim oReader As New StreamReader(sFileName)
            If File.Exists(sNewFileName) Then
                File.Delete(sNewFileName)
            End If
            Dim oWriter As New StreamWriter(sNewFileName)
            Dim sLineToWrite As String = ""
            Dim sSourceLine As String = ""

            While oReader.EndOfStream <> True
                sSourceLine = oReader.ReadLine()

                ' Line Format 
                '--------------------- 
                ' nFeeScheduleID, + SourceFileLine + sSpecialtyID,nClinicCharges,nLimitCharges,nAllowedCharges,nClinicID,nChargePercentage,nVariantAmount 
                ' SourceFileLine = (sYear,sCarrierNumber,sLocality,sHCPCS,sModifier,nNonFacilityFeeScheduleAmount,nFacilityFeeScheduleAmount,nPCTCIndicator,sStatusCode) 
                '---------------------- 

                ' sLineToWrite = nFeeScheduleID + "," + sSourceLine.Replace("/"", "") + ",0,0,0,0," + _ClinicID + "," + Convert.ToInt32(numChargePercentage.Value) + ",0"
                sLineToWrite = nFeeScheduleID & "," & sSourceLine.Replace("""", "") & ",0,0,0,0," & _ClinicID & "," & Convert.ToInt32(numChargePercentage.Value) & ",0"

                oWriter.WriteLine(sLineToWrite.ToCharArray())
            End While
            oReader.Close()
            oWriter.Close()

            _result = sNewFileName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
        Return _result
    End Function

    Private Function CreateBatchFile(ByVal sSourceFileName As String) As String
        Dim _result As String = ""
        Try

            If File.Exists(sSourceFileName) = False Then
                Return _result
            End If

            Dim sBatchFileName As String = sSourceFileName.ToUpper().Replace(".CSV", ".bat")
            Dim sBCPUtilityPath As String = "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp.exe"

            If File.Exists(sBatchFileName) = True Then
                File.Delete(sBatchFileName)
            End If

            If File.Exists(sBCPUtilityPath) = False Then
                MessageBox.Show("Please select BCP utility path. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                dlgBrowseFile.Title = " Browse File "
                dlgBrowseFile.Filter = "(*.exe)|*.exe"
                dlgBrowseFile.CheckFileExists = True
                dlgBrowseFile.Multiselect = False
                dlgBrowseFile.ShowHelp = False
                dlgBrowseFile.ShowReadOnly = False

                If dlgBrowseFile.ShowDialog() = DialogResult.OK Then

                    sBCPUtilityPath = dlgBrowseFile.FileName
                Else
                    Return _result
                End If
            End If

            sBCPUtilityPath = sBCPUtilityPath.ToUpper().Replace(".EXE", "")

            Dim oWriter As New StreamWriter(sBatchFileName)
            Dim sLineToWrite As String = ""

            ' Batch command i.e 
            '------------------------------------------------------------------- 
            ' (SQL Authentication) 
            ' "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp" ImportTesting.dbo.BL_FeeSchedule_DTL in "E:\BCP\PFALL09A_New.csv" -c -C RAW -t "," -r "\n" -U sa -P sadev13 -S dev13 -a 8192 

            ' (Win Authentication) 
            ' "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp" ImportTesting.dbo.BL_FeeSchedule_DTL in "E:\BCP\PFALL09A_New.csv" -c -C RAW -t "," -r "\n" -S dev13 -T -a 8192 
            '------------------------------------------------------------------- 


            If _IsWindowAuthentication = False Then
                sLineToWrite = (((((" """ & sBCPUtilityPath & """ ") + _DatabaseName & ".dbo.BL_FeeSchedule_DTL in """) + sSourceFileName & """ -c -C RAW -t "","" -r ""\n"" -U ") + _SQLLoginName & " -P ") + _SQLPassword & " -S ") + _SQLServerName & " -a 8192 "
            Else
                sLineToWrite = (((" """ & sBCPUtilityPath & """ ") + _DatabaseName & ".dbo.BL_FeeSchedule_DTL in """) + sSourceFileName & """ -c -C RAW -t "","" -r ""\n"" -S ") + _SQLServerName & " -T -a 8192 "
            End If

            oWriter.WriteLine(sLineToWrite.ToCharArray())
            oWriter.Close()

            _result = sBatchFileName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return _result
    End Function

    Private Function GetFeeScheduleID() As Long
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)

        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = " SELECT ISNULL(MAX(nFeeScheduleID),0) + 20 FROM BL_FeeSchedule_MST"
            Dim _retID As Object = oDB.ExecuteScalar_Query(_sqlQuery)
            oDB.Disconnect()


            If _retID IsNot Nothing AndAlso Convert.ToString(_retID) <> "" Then
                Return Convert.ToInt64(_retID)
            End If
        Catch odbEx As gloDatabaseLayer.DBException
            odbEx.ERROR_Log(odbEx.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Dispose()
        End Try
        Return 0
    End Function

    Private Sub SaveFeeSchedule(ByVal nFeeScheduleID As Long)
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)

        Try
            oDB.Connect(False)

            Dim _sqlQuery As String = "INSERT INTO BL_FeeSchedule_MST (nFeeScheduleID, nFeeScheduleType, sFeeScheduleName, nClinicID) " _
                                    & " VALUES (" & nFeeScheduleID & ", 0, '" & txtFeeScheduleName.Text.Trim() & "', " & _ClinicID & ")"
            oDB.Execute_Query(_sqlQuery)


            oDB.Disconnect()
        Catch odbEx As gloDatabaseLayer.DBException
            odbEx.ERROR_Log(odbEx.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Dispose()
        End Try
    End Sub


End Class

