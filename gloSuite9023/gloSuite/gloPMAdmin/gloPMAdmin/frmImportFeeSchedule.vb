Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Win32
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
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
    Dim flag As Boolean = False
    Dim _IsValidate As Boolean = True
    Dim nFeeScheduleID As Int64 = 0
    Private Const COL_CPTNAME = 0
    Private Const COL_Modifier = 1

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

    Public Function IsDuplicate(ByVal sFeeScheduleName As String, ByVal _StdFeeScheeduleID As Int64) As Boolean
        Dim _sqlQuery As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        oDB.Connect(False)
        Dim _dtDuplicateCPT As New DataTable()
        Try
            '' _sqlQuery = "SELECT BL_FeeSchedule_MST.sFeeScheduleName,BL_FeeSchedule_Allocation.nFromDate,BL_FeeSchedule_Allocation.nToDate  FROM   BL_FeeSchedule_MST " & "INNER JOIN dbo.BL_FeeSchedule_Allocation ON   BL_FeeSchedule_MST.nFeeScheduleID=BL_FeeSchedule_Allocation.nFeeScheduleID " & "AND (" & DateAsNumber(mskStartDate) & " BETWEEN  BL_FeeSchedule_Allocation.nFromDate AND BL_FeeSchedule_Allocation.nToDate " & "OR " & DateAsNumber(mskEndDate) & " BETWEEN  BL_FeeSchedule_Allocation.nFromDate AND BL_FeeSchedule_Allocation.nToDate )" & "AND (BL_FeeSchedule_MST.nClinicID = " & Convert.ToString(_ClinicID) & ")" & "WHERE  (sFeeScheduleName = '" & sFeeScheduleName.ToString().Replace("'", "''") & "')  AND (BL_FeeSchedule_MST.nFeeScheduleID <> " & Convert.ToString(_StdFeeScheeduleID) & ") "
            _sqlQuery = " SELECT BL_FeeSchedule_MST.sFeeScheduleName From BL_FeeSchedule_MST  WHERE  sFeeScheduleName =  '" & sFeeScheduleName.ToString().Replace("'", "''") & "'  AND BL_FeeSchedule_MST.nFeeScheduleID <> " & Convert.ToString(_StdFeeScheeduleID) & "  "
            oDB.Retrive_Query(_sqlQuery, _dtDuplicateCPT)
            If _dtDuplicateCPT.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB.Dispose()

            End If
        End Try
        Return False
    End Function
    Private Sub tsb_Import_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsb_Import.Click
        Dim _Result As Boolean = False
        Dim dtDuplicateCptAndModifier As New DataTable()
        Dim _FilePath As String
        _FilePath = AppDomain.CurrentDomain.BaseDirectory + "FeeScheduleDuplicateValidation.txt"
        Try
            tls_SetupResource.Select()
            If _IsValidate = False Then
                Return
            End If
            nFeeScheduleID = GetFeeScheduleID()
            If ValidateData() = True Then
                tsb_Import.Enabled = False
                Me.Cursor = Cursors.WaitCursor
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
                                dtDuplicateCptAndModifier = GetDuplicateCptModifier(nFeeScheduleID)
                                dtDuplicateCptAndModifier.Columns.Remove("Column1")
                                If (dtDuplicateCptAndModifier.Rows.Count > 0) Then
                                    C1Settings.DataSource = dtDuplicateCptAndModifier.DefaultView
                                    Design()
                                    If File.Exists(_FilePath) = True Then
                                        File.Delete(_FilePath)
                                    End If
                                    MessageBox.Show("Duplicate CPT/Modifier combinations exist.  Please remove all duplicates and re-import fee schedule. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    C1Settings.SaveGrid(_FilePath, C1.Win.C1FlexGrid.FileFormatEnum.TextTab)
                                    System.Diagnostics.Process.Start(_FilePath)
                                    DeleteData(nFeeScheduleID)
                                    _Result = False
                                Else
                                    SaveFeeSchedule(nFeeScheduleID)
                                    _Result = True
                                End If


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
                    If flag = False Then
                        Me.Cursor = Cursors.[Default]
                        MessageBox.Show("Error while reading source file. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.ToString.Contains("because it is being used by another process") = True Then
                MsgBox("Close the file and try again.", MsgBoxStyle.Critical)
                flag = True
                ' end
            ElseIf ex.ToString.Contains(" It is already opened exclusively by another user") = True Then

                MsgBox("Close the file and try again.", MsgBoxStyle.Critical)
                flag = True
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            End If
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
    Public Shared Function DateAsDateString(ByVal datevalue As Int64) As String
        Dim _internaldate As String = ""
        Try
            If datevalue.ToString().Length = 8 Then
                Dim _internalresult As String = datevalue.ToString()


                _internaldate = _internalresult.Substring(4, 2) & "/" & _internalresult.Substring(6, 2) & "/" & _internalresult.Substring(0, 4)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return _internaldate
    End Function
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
            If Not mskStartDate.MaskCompleted Then
                MessageBox.Show("Enter Effective Start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskStartDate.Focus()
                Return False
            End If
            If Not mskEndDate.MaskCompleted Then
                MessageBox.Show("Enter Effective End Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskEndDate.Focus()
                Return False
            End If
            If Convert.ToDateTime((mskEndDate.Text)) < Convert.ToDateTime((mskStartDate.Text)) AndAlso Convert.ToDateTime(mskEndDate.Text) <> Convert.ToDateTime(mskStartDate.Text) Then
                MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskEndDate.Focus()
                Return False
            End If

            Dim _isDuplicateCPT As Boolean
            _isDuplicateCPT = IsDuplicate(txtFeeScheduleName.Text.Trim(), nFeeScheduleID)
            If _isDuplicateCPT Then
                'Dim FeeSchedulename As String = _dtDuplicateCPT.Rows(0)("sFeeScheduleName").ToString()
                'Dim FromDate As String = DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows(0)("nFromDate")))
                'Dim ToDate As String = DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows(0)("nToDate")))
                'MessageBox.Show(("The effective date range overlaps with the following fee schedule:" + Environment.NewLine & FeeSchedulename & " [" & FromDate & "-" & ToDate & "]") + Environment.NewLine & "Please enter a different effective date range", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("Fee Schedule already exist", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '_StdFeeScheeduleID = 0;
                Return False
            End If

            'oDB.Connect(False)
            'Dim _sqlQuery As String = " SELECT Count(*)  FROM BL_FeeSchedule_MST  WHERE sFeeScheduleName = '" & txtFeeScheduleName.Text.Trim() & "' AND nClinicID = " & _ClinicID & " "

            'Dim _Name As Object = oDB.ExecuteScalar_Query(_sqlQuery)
            'oDB.Disconnect()

            'If Convert.ToInt64(_Name) > 0 Then
            '    MessageBox.Show("Fee schedule name is already exists. Please select a unique name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If
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

    Public Function RemoveDuplicate(ByVal dt As DataTable) As Boolean

        Dim ht As Hashtable = New Hashtable()
        Dim Duplicate As DataTable = New DataTable()
        Duplicate.Columns.Add("CptCode")
        Dim _FilePath As String = AppDomain.CurrentDomain.BaseDirectory
        Duplicate.Columns.Add("Modifer")
        Dim sDuplicateCPTandModifer As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            If ht.ContainsValue(dt.Rows(i)(0) & " " & dt.Rows(i)(1)) Then
                sDuplicateCPTandModifer = sDuplicateCPTandModifer + Environment.NewLine + dt.Rows(i)(0).ToString() & " " & dt.Rows(i)(1).ToString()
                ''Duplicate.Rows.Add(dt.Rows(i)(0), dt.Rows(i)(1))
            Else
                ht.Add(i, dt.Rows(i)(0).ToString() & " " & dt.Rows(i)(1).ToString())
            End If
        Next
        If sDuplicateCPTandModifer <> "" Then
            _FilePath = _FilePath + "FeeScheduleDuplicateValidation.txt"
            Dim oStreamWriter As StreamWriter = New System.IO.StreamWriter(_FilePath, False)
            oStreamWriter.WriteLine("Remove Duplicate CPT CODE With Modifer " + Environment.NewLine + sDuplicateCPTandModifer)
            oStreamWriter.Close()
            oStreamWriter.Dispose()
            System.Diagnostics.Process.Start(_FilePath)
            Return False
        Else
            Return True
        End If

    End Function
    Private Shared Function GetDataTableFromCsv(ByVal path__1 As String, ByVal isFirstRowHeader As Boolean) As DataTable
        Dim header As String = If(isFirstRowHeader, "Yes", "No")

        Dim pathOnly As String = Path.GetDirectoryName(path__1)
        Dim fileName As String = Path.GetFileName(path__1)

        Dim sql As String = "SELECT * FROM [" & fileName & "]"

        Using connection As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pathOnly & ";Extended Properties=""Text;HDR=" & header & """")
            Using command As New OleDbCommand(sql, connection)
                Using adapter As New OleDbDataAdapter(command)
                    Dim dataTable As New DataTable()
                    dataTable.Locale = CultureInfo.CurrentCulture
                    adapter.Fill(dataTable)
                    Return dataTable
                End Using
            End Using
        End Using
    End Function

    Public Function GetDuplicateCptModifier(ByVal nFeeScheduleID As Int64) As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = " SELECT Count(nfeescheduleID),sHCPCS,sModifier FROM dbo.BL_FeeSchedule_DTL  GROUP BY sHCPCS,sModifier,nfeescheduleID HAVING COUNT(nfeescheduleID)>1 AND  nfeescheduleID=" & nFeeScheduleID
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function DeleteData(ByVal nFeeScheduleID As Int64)
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)

            Dim _sqlQuery As String = "Delete From  BL_FeeSchedule_DTL  Where nFeeScheduleID=" & nFeeScheduleID
            oDB.Execute_Query(_sqlQuery)
            oDB.Disconnect()
        Catch odbEx As gloDatabaseLayer.DBException
            odbEx.ERROR_Log(odbEx.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function Design()
        C1Settings.AllowEditing = True
        C1Settings.Cols(COL_CPTNAME).AllowEditing = True
        C1Settings.Cols(COL_Modifier).AllowEditing = True
        C1Settings.Cols(COL_CPTNAME).Width = C1Settings.Width * 0.5
        C1Settings.Cols(COL_Modifier).Width = C1Settings.Width * 0.5
    End Function


    Private Function FormatCSVFile(ByVal sFileName As String, ByVal nFeeScheduleID As Int64) As String
        Dim _result As String = ""
        Dim sNewFileName As String = sFileName.ToUpper().Replace(".CSV", "_New.CSV")
        Dim oReader As New StreamReader(sFileName)
        If File.Exists(sNewFileName) Then
            File.Delete(sNewFileName)
        End If
        Dim oWriter As New StreamWriter(sNewFileName)
        Dim sLineToWrite As String = ""
        Dim sSourceLine As String = ""

        Try
            'Dim dtCharges As DataTable = GetDataTableFromCsv(sFileName, False)
            'dtCharges.Columns.RemoveAt(2)
            'dtCharges.Columns.RemoveAt(2)
            'If RemoveDuplicate(dtCharges) Then



            While oReader.EndOfStream <> True
                'Added by Shweta 20091117
                'if end of file get then dont Format the CSV
                sSourceLine = oReader.ReadLine()
                If sSourceLine <> "" Then
                    'End Shweta 
                    ' Line Format 
                    '--------------------- 
                    ' nFeeScheduleID, + SourceFileLine + sSpecialtyID,nClinicCharges,nLimitCharges,nAllowedCharges,nClinicID,nChargePercentage,nVariantAmount 
                    ' SourceFileLine = (sYear,sCarrierNumber,sLocality,sHCPCS,sModifier,nNonFacilityFeeScheduleAmount,nFacilityFeeScheduleAmount, ,nPCTCIndicator,sStatusCode) 
                    '---------------------- 

                    ' sLineToWrite = nFeeScheduleID + "," + sSourceLine.Replace("/"", "") + ",0,0,0,0," + _ClinicID + "," + Convert.ToInt32(numChargePercentage.Value) + ",0"

                    Dim FacilityCharge As Double = 0, NonFacilityCharge As Double = 0

                    Dim sSource() As String = sSourceLine.Replace("""", "").Split(",")

                    If sSource.GetUpperBound(0) > 1 Then
                        NonFacilityCharge = Convert.ToDouble(sSource(2))
                        FacilityCharge = Convert.ToDouble(sSource(3))
                    End If

                    sLineToWrite = nFeeScheduleID & "," & sSourceLine.Replace("""", "") & ",0,0,0,0,0" & _ClinicID & "," & Convert.ToInt32(numChargePercentage.Value) & ",0," & CalculateChargeAmount(NonFacilityCharge) & "," & CalculateChargeAmount(FacilityCharge) & ""
                    oWriter.WriteLine(sLineToWrite.ToCharArray())
                End If
            End While
            oReader.Close()
            oWriter.Close()

            _result = sNewFileName
            'Else
            '_result = ""
            'flag = True
            'End If
        Catch ex As Exception
            'solving mantis id-473 issue
            If ex.ToString.Contains("because it is being used by another process") = True Then
                MsgBox("Cannot Import Fee Schedule as the file is open or used by another process. Close the file and try again.", MsgBoxStyle.Critical)
                flag = True
                ' end
            ElseIf ex.ToString.Contains(" It is already opened exclusively by another user") = True Then

                MsgBox("Cannot Import Fee Schedule as the file is open or used by another process. Close the file and try again.", MsgBoxStyle.Critical)
                flag = True
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            End If
            oReader.Close()
            oWriter.Close()
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
            '  Dim sBCPUtilityPath As String = "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp.exe"
            Dim sBCPUtilityPath As String = "C:\Program Files\Microsoft SQL Server\100\Tools\Binn\bcp.exe"

            If File.Exists(sBatchFileName) = True Then
                File.Delete(sBatchFileName)
            End If

            If File.Exists(sBCPUtilityPath) = False Then
                sBCPUtilityPath = "C:\Program Files\Microsoft SQL Server\90\Tools\Binn\bcp.exe"
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
                sLineToWrite = (((((" """ & sBCPUtilityPath & """ ") + "[" + _DatabaseName & "].[dbo].[BL_FeeSchedule_DTL] in """) + sSourceFileName & """ -c -C RAW -t "","" -r ""\n"" -U ") + _SQLLoginName & " -P ") + _SQLPassword & " -S ") + _SQLServerName & " -a 8192 "
            Else
                sLineToWrite = (((" """ & sBCPUtilityPath & """ ") + "[" + _DatabaseName & "].[dbo].[BL_FeeSchedule_DTL] in """) + sSourceFileName & """ -c -C RAW -t "","" -r ""\n"" -S ") + _SQLServerName & " -T -a 8192 "
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
            Dim _sqlQuery As String = " SELECT  dbo.GetUniqueID_V2()"
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

    Public Shared Function DateAsNumber(ByVal datevalue As String) As Int32
        Dim _result As Int32 = 0
        Dim _internaldate As DateTime = Convert.ToDateTime(datevalue)
        datevalue = String.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy")
        Try
            If datevalue.Length = 10 Then
                Dim _internalresult As String = ""
                _internalresult = datevalue.Substring(6, 4) & datevalue.Substring(0, 2) & datevalue.Substring(3, 2)
                _result = Convert.ToInt32(_internalresult)
            ElseIf datevalue.Length = 9 Then
                Dim _internalresult As String = ""
                If _internaldate.Month <= 9 Then
                    ' 1/11/2007
                    _internalresult = datevalue.Substring(5, 4) & "0" & datevalue.Substring(0, 1) & datevalue.Substring(2, 2)
                ElseIf _internaldate.Day <= 9 Then
                    ' 11/2/2007
                    _internalresult = datevalue.Substring(5, 4) & datevalue.Substring(0, 2) & "0" & datevalue.Substring(3, 1)
                End If


                _result = Convert.ToInt32(_internalresult)
            ElseIf datevalue.Length = 8 Then
                Dim _internalresult As String = ""
                _internalresult = datevalue.Substring(4, 4) & "0" & datevalue.Substring(0, 1) & "0" & datevalue.Substring(2, 1)
                _result = Convert.ToInt32(_internalresult)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return _result
    End Function

    Private Sub SaveFeeSchedule(ByVal nFeeScheduleID As Long)
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)

            Dim _sqlQuery As String = "INSERT INTO BL_FeeSchedule_MST (nFeeScheduleID, nFeeScheduleType, sFeeScheduleName, nClinicID) " _
                                    & " VALUES (" & nFeeScheduleID & ", 0, '" & txtFeeScheduleName.Text.Trim() & "', " & _ClinicID & ")"
            oDB.Execute_Query(_sqlQuery)

            _sqlQuery = "INSERT INTO BL_FeeSchedule_Allocation (nFeeScheduleID, nFromDate, nToDate, nClinicID) " _
                                   & " VALUES (" & nFeeScheduleID & "," & DateAsNumber(mskStartDate.Text) & ", " & DateAsNumber(mskEndDate.Text) & ", " & _ClinicID & ")"
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

    Private Function CalculateChargeAmount(ByVal Charges As Double) As Double

        Dim ChargeAmount As Double = Charges

        'If rdbPerAmount.Checked = True Then
        ChargeAmount = (ChargeAmount) + (ChargeAmount * Convert.ToDouble(numChargePercentage.Value) / 100)
        If rdbNone.Checked <> True Then
            If rdbroundUp.Checked = True Then
                ChargeAmount = Math.Ceiling(ChargeAmount)
            Else
                ChargeAmount = Math.Floor(ChargeAmount)
            End If
        End If
        'ElseIf rdbflatamount.Checked = True Then
        'ChargeAmount = Convert.ToDouble(numChargePercentage.Value)
        'End If

        Return Math.Round(ChargeAmount, 2)

    End Function

    'Private Sub rdbPerAmount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPerAmount.CheckedChanged
    '    If rdbPerAmount.Checked = True Then
    '        rdbPerAmount.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdbPerAmount.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdbflatamount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbflatamount.CheckedChanged
    '    If rdbflatamount.Checked = True Then
    '        rdbflatamount.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdbflatamount.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    Private Sub rdbroundUp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbroundUp.CheckedChanged
        If rdbroundUp.Checked = True Then
            rdbroundUp.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbroundUp.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbroundDown_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbroundDown.CheckedChanged
        If rdbroundDown.Checked = True Then
            rdbroundDown.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbroundDown.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNone.CheckedChanged
        If rdbNone.Checked = True Then
            rdbNone.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbNone.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Function IsValidDate(ByVal strDate As Object) As Boolean
        Dim Success As Boolean
        Try
            Dim validatedDate As New DateTime
            Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, validatedDate)
            If Success = True Then
                If validatedDate < DateTime.MaxValue AndAlso validatedDate >= Convert.ToDateTime("01/01/1900") Then
                    Success = True
                Else
                    Success = False

                End If
            End If
        Catch generatedExceptionName As FormatException

            Success = False
        End Try
        Return Success
    End Function
    Private Sub mskStartDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskStartDate.Validating

        Try
            Dim mskStartDate As MaskedTextBox = DirectCast(sender, MaskedTextBox)
            mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            Dim strDate As String = mskStartDate.Text
            mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals
            _IsValidate = True
            If mskStartDate IsNot Nothing Then
                If strDate.Length > 0 Then
                    If IsValidDate(mskStartDate.Text.Trim()) = False Then
                        MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Specifies that the Date is InValid

                        e.Cancel = True
                        _IsValidate = False

                    End If
                End If
            End If
        Catch generatedExceptionName As Exception
            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
            _IsValidate = False
        End Try
    End Sub


    Private Sub mskEndDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskEndDate.Validating
        Try
            Dim mskEndDate As MaskedTextBox = DirectCast(sender, MaskedTextBox)
            mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            Dim strDate As String = mskEndDate.Text
            mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals
            _IsValidate = True
            If mskEndDate IsNot Nothing Then
                If strDate.Length > 0 Then
                    If IsValidDate(mskEndDate.Text.Trim()) = False Then
                        MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Specifies that the Date is InValid

                        e.Cancel = True
                        _IsValidate = False

                    End If
                End If
            End If
        Catch generatedExceptionName As Exception
            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
            _IsValidate = False
        End Try
    End Sub

    Private Sub mskStartDate_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mskStartDate.MouseClick
        DirectCast(sender, MaskedTextBox).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        If DirectCast(sender, MaskedTextBox).Text.Trim() = "" Then
            DirectCast(sender, MaskedTextBox).SelectionStart = 0
            DirectCast(sender, MaskedTextBox).SelectionLength = 0
        End If
        DirectCast(sender, MaskedTextBox).TextMaskFormat = MaskFormat.IncludePromptAndLiterals

    End Sub

    Private Sub mskEndDate_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mskEndDate.MouseClick
        DirectCast(sender, MaskedTextBox).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        If DirectCast(sender, MaskedTextBox).Text.Trim() = "" Then
            DirectCast(sender, MaskedTextBox).SelectionStart = 0
            DirectCast(sender, MaskedTextBox).SelectionLength = 0
        End If
        DirectCast(sender, MaskedTextBox).TextMaskFormat = MaskFormat.IncludePromptAndLiterals

    End Sub
    Private Function ValidateParameters() As Boolean
        Dim _ReturnValue As Boolean = True
        Try

            If Not mskStartDate.MaskCompleted Then
                MessageBox.Show("Enter Effective Start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _ReturnValue = False
                mskStartDate.Focus()
                Return _ReturnValue
            End If
            If Not mskEndDate.MaskCompleted Then
                MessageBox.Show("Enter End Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _ReturnValue = False
                mskEndDate.Focus()
                Return _ReturnValue
            End If
            If Convert.ToDateTime((mskEndDate.Text)) < Convert.ToDateTime((mskStartDate.Text)) AndAlso Convert.ToDateTime(mskEndDate.Text) <> Convert.ToDateTime(mskStartDate.Text) Then
                MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskEndDate.Focus()
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return _ReturnValue
    End Function
End Class


