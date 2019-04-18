Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid



Public Class frmMigrationByTemplate

    Private _databaseconnectionstring As String = ""
    Private _dtAllCommunicationLettersAndMessages As DataTable
    Private _dtGeneratedCommunicationLetterAndMessages As DataTable
    Public gstrMessageBoxCaption As String = "gloEMR Admin"

    Public Sub New(ByVal _databaseconnectionstring As String)
        InitializeComponent()
        _databaseconnectionstring = _databaseconnectionstring
    End Sub

    Private Sub frmMobileMgnt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _dtAllCommunicationLettersAndMessages = GetAllCommunicationLettersORMessages()
        If _dtAllCommunicationLettersAndMessages IsNot Nothing Then
            c1LetterAndMessage.BeginUpdate()
            c1LetterAndMessage.DataSource = Nothing
            c1LetterAndMessage.DataSource = _dtAllCommunicationLettersAndMessages.DefaultView
            c1LetterAndMessage.EndUpdate()
            designGrid()
        End If

    End Sub

    'Private Function GetAllCommunicationLettersORMessages() As DataTable

    '    Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
    '    If oDB IsNot Nothing Then
    '        If oDB.Connect(False) Then
    '            Try
    '                Dim dtCommunicationType As New DataTable()
    '                Dim _sqlQuery As String = "SELECT DISTINCT  CONVERT(Bit,0) as bSelect,sTemplateName AS TemplateName,nTemplateID AS TemplateID,'PatientLetter' AS Category FROM dbo.PatientLetters where isnull(bisUnscheduledCare,0)=0 and isnull(nCommunicationType,0)=0 " &
    '                                       " UNION ALL " &
    '                                         "SELECT DISTINCT CONVERT(Bit,0) as bSelect,sTemplateName AS TemplateName,nTemplateID AS TemplateID ,'Message' AS Category FROM dbo.Message where isnull(bisUnscheduledCare,0)=0 and isnull(nCommunicationType,0)=0 "

    '                oDB.Retrive_Query(_sqlQuery, dtCommunicationType)

    '                Return dtCommunicationType

    '            Catch ex As gloDatabaseLayer.DBException
    '                ex.ERROR_Log(ex.ToString())
    '                If oDB IsNot Nothing Then
    '                    oDB.Disconnect()
    '                    oDB.Dispose()
    '                    oDB = Nothing
    '                End If
    '            Catch ex As Exception
    '                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '                If oDB IsNot Nothing Then
    '                    oDB.Disconnect()
    '                    oDB.Dispose()
    '                    oDB = Nothing
    '                End If
    '            Finally

    '                If oDB IsNot Nothing Then
    '                    oDB.Disconnect()
    '                    oDB.Dispose()
    '                    oDB = Nothing
    '                End If
    '            End Try

    '        End If
    '    End If
    'End Function

    Private Function GetAllCommunicationLettersORMessages() As DataTable

        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""

        If dtpFromDate IsNot Nothing Then
            sStartdate = dtpFromDate.Value.ToShortDateString()
        End If

        If dtpToDate IsNot Nothing Then
            sToDate = dtpToDate.Value.ToShortDateString()
        End If
        If oDB IsNot Nothing Then
            If oDB.Connect(False) Then
                Try
                    oParamater.Add("@IsFilter", ChkEnableFilter.Checked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                    oParamater.Add("@sdtStartDate", sStartdate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@sdtToDate", sToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("GetCommunicationLettersORMessages", oParamater, _dt)

                    Return _dt
                Catch ex As gloDatabaseLayer.DBException
                    ex.ERROR_Log(ex.ToString())
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If

                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Finally
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End If
        End If


    End Function

    Private Sub designGrid()
        c1LetterAndMessage.Cols("bSelect").Visible = True
        c1LetterAndMessage.SetData(0, 0, "Select")
        c1LetterAndMessage.SetData(0, 1, "Template Name")

        c1LetterAndMessage.Cols("TemplateName").Visible = True
        '' c1LetterAndMessage.Cols("TemplateID").Visible = False
        c1LetterAndMessage.Cols("Category").Visible = True

        c1LetterAndMessage.Cols("bSelect").Width = 60
        c1LetterAndMessage.Cols("TemplateName").Width = 325
        c1LetterAndMessage.Cols("Category").Width = 150
        c1LetterAndMessage.Cols("TemplateName").AllowEditing = False
        c1LetterAndMessage.Cols("Category").AllowEditing = False


    End Sub

    Private Sub ChkEnableFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEnableFilter.CheckedChanged
        If ChkEnableFilter.Checked Then
            dtpFromDate.Enabled = True
            dtpToDate.Enabled = True
        Else
            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
        End If
        tsbGenerate_Click(sender, e)
    End Sub

    Private Sub tsbGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGenerate.Click
        Dim _dv As DataView
        Try
          

            _dtGeneratedCommunicationLetterAndMessages = GetAllCommunicationLettersORMessages()
            If _dtGeneratedCommunicationLetterAndMessages IsNot Nothing Then
                c1LetterAndMessage.BeginUpdate()
                c1LetterAndMessage.DataSource = Nothing
                c1LetterAndMessage.DataSource = _dtGeneratedCommunicationLetterAndMessages.DefaultView
                c1LetterAndMessage.EndUpdate()

            End If


            If Not CmbCategory.Text = "" Then
                _dv = c1LetterAndMessage.DataSource
                _dv.RowFilter = _dv.Table.Columns("Category").ColumnName + "='" + CmbCategory.Text + "'"
                c1LetterAndMessage.BeginUpdate()
                c1LetterAndMessage.DataSource = Nothing
                c1LetterAndMessage.DataSource = _dv
                c1LetterAndMessage.EndUpdate()
            End If
            If c1LetterAndMessage.Rows.Count > 0 Then
                designGrid()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
           
        Finally
            _dv = Nothing
        End Try
       

    End Sub

    Private Sub tsbRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRun.Click

        If c1LetterAndMessage.Rows.Count > 1 Then
            Dim oResult As DialogResult
            oResult = MessageBox.Show("This utility will mark all Patient Letters or Messages using" & Environment.NewLine & "the selected templates as 'Reminder of Unscheduled Care'." & Environment.NewLine & "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If oResult = Windows.Forms.DialogResult.OK Then

                For RowCount As Int16 = 1 To c1LetterAndMessage.Rows.Count - 1
                    If c1LetterAndMessage.GetCellCheck(RowCount, 0) = CheckEnum.Checked Then
                        MigrationByTemplate(c1LetterAndMessage.GetData(RowCount, "TemplateName").ToString(), dtpFromDate.Value.ToShortDateString(), dtpToDate.Value.ToShortDateString(), c1LetterAndMessage.GetData(RowCount, "Category").ToString())
                    End If
                Next

            End If

        tsbGenerate_Click(sender, e)
        End If
       


    End Sub

    Private Sub TsCancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsCancelBtn.Click
        Me.Close()
    End Sub

    Private Sub frmMigrationByTemplate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If _dtAllCommunicationLettersAndMessages IsNot Nothing Then
            _dtAllCommunicationLettersAndMessages.Dispose()
        End If

        If _dtGeneratedCommunicationLetterAndMessages IsNot Nothing Then
            _dtGeneratedCommunicationLetterAndMessages.Dispose()
        End If
    End Sub

 
    Private Function MigrationByTemplate(ByVal sTemplateName As String, ByVal sStartdate As String, ByVal sToDate As String, ByVal sCategoryType As String)

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oParamater As New gloDatabaseLayer.DBParameters()

        If oDB IsNot Nothing Then
            If oDB.Connect(False) Then
                Try
                    oParamater.Add("@sCategoryType", sCategoryType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@IsFilter", ChkEnableFilter.Checked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                    oParamater.Add("@sStartdate", sStartdate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@sToDate", sToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@sTemplateName", sTemplateName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Execute("MarkTemplateAsUnscheduleCare", oParamater)
                Catch ex As gloDatabaseLayer.DBException
                    ex.ERROR_Log(ex.ToString())
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If

                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                Finally
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End If
        End If

    End Function
End Class