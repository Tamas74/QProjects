Imports gloEMRGeneralLibrary
Imports System.Data
Imports System.Data.SqlClient


Public Class gloUC_TaskInfo

    Private nTaskId As Int64
    Private TestNames As String = "" '' added to show testnames on EmdeonScreen ,v8022
    Private strDiag As String = ""
    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal TaskID As Int64, Optional ByVal _TestNames As String = "", Optional ByVal _strDiag As String = "")
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        nTaskId = TaskID
        TestNames = _TestNames
        strDiag = _strDiag
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FillTaskDetails(ByVal Taskid As Int64)

        Dim ds As DataSet = Nothing
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Taskid
                oPara.Name = "@nTaskID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ds = .GetDataSet("gsp_TaskInfo")

            End With

            If ds.Tables(0).Rows.Count > 0 Then
                lblDueDateValue.Text = ds.Tables(0).Rows(0)("nDueDate")
                lblStartDateValue.Text = ds.Tables(0).Rows(0)("nStartDate")
                lblSubjectName.Text = ds.Tables(0).Rows(0)("sSubject")
                lblProviderName.Text = ds.Tables(0).Rows(0)("ProviderName")
                lblStatusValue.Text = ds.Tables(0).Rows(0)("Status")
                lblPriorityValue.Text = ds.Tables(0).Rows(0)("Priority")
                Dim str As String = ds.Tables(0).Rows(0)("sDescription")
                If str.Contains("Diagnosis") Then

                    Dim strspl As String() = str.Split(New String() {"Diagnosis:"}, StringSplitOptions.RemoveEmptyEntries)
                    ''code change to solve bugid 69962
                    Dim strdesc As String = ""
                    If strspl.Length > 0 Then
                        Dim len As Integer = 0
                        While (len < strspl.Length)
                            If len = 1 Then
                                strdesc = strdesc & " Diagnosis: " & strspl(len).ToString() & vbNewLine

                            Else
                                strdesc = strdesc & strspl(len).ToString() & vbNewLine
                            End If
                            len += 1
                        End While
                        rchtxt_TaskDescription.Text = strdesc
                    End If
                Else
                    rchtxt_TaskDescription.Text = ds.Tables(0).Rows(0)("sDescription")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub

    Private Sub gloUC_TaskInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If nTaskId <> -1 Then   '' added to show testnames on EmdeonScreen ,v8022
                FillTaskDetails(nTaskId)
            Else
                If TestNames.Trim() <> "" Then '' added to show testnames on EmdeonScreen ,v8022
                    Dim splstrtestnames As String() = TestNames.ToString().Split("~")
                    For i As Int32 = 0 To splstrtestnames.Length - 1
                        If i = 0 Then
                            If strDiag.Trim() <> "" Then
                                rchtxt_TaskDescription.Text = "Lab Tests" & vbNewLine
                            End If
                            rchtxt_TaskDescription.Text = rchtxt_TaskDescription.Text & (i + 1).ToString() & ". " & splstrtestnames(i) & "   "   ''formatting done to show testnames with number
                        Else
                            rchtxt_TaskDescription.Text = rchtxt_TaskDescription.Text & (i + 1).ToString() & ". " & splstrtestnames(i) & "   "
                        End If

                    Next
                    

                End If

                If strDiag.Trim() <> "" Then '' added to show testnames on EmdeonScreen ,v8022
                    Dim splstrDiag As String() = strDiag.ToString().Split(",")
                    For i As Int32 = 0 To splstrDiag.Length - 1
                        If i = 0 Then
                            rchtxt_TaskDescription.Text = rchtxt_TaskDescription.Text & vbNewLine & vbNewLine & " Diagnosis " & vbNewLine & (i + 1).ToString() & ". " & splstrDiag(i) & "   "   ''formatting done to show testnames with number
                        Else
                            If (splstrDiag(i).Trim() <> "") Then
                                rchtxt_TaskDescription.Text = rchtxt_TaskDescription.Text & (i + 1).ToString() & ". " & splstrDiag(i) & "   "
                            End If
                        End If

                    Next
                End If
                lblTaskHeader.Text = "Lab Tests"
                lblTaskHeader.TextAlign = ContentAlignment.MiddleCenter
                pnlrchtxt_TaskDescription.Dock = DockStyle.Fill
                pnlTaskDetails.Visible = False
                ' Me.Height = 78
            End If

         
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
