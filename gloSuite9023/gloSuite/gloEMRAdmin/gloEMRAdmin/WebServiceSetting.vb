Imports System.Data.SqlClient
Imports System.Data
Public Class frmWebServiceSetting
    Dim Sessiontimeout As UInt16

    Dim PassAttempt As UInt16
    Dim AutomaticUnblock As UInt16
    Dim IsClose As Boolean = False

    Private Sub WebServiceSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        c1Setting.SetData(1, 0, "Session Timeout : (Value in Minutes)")
        c1Setting.SetData(2, 0, "No of wrong login attempts to block user : (Value in Count)")
        c1Setting.SetData(3, 0, "Duration of automatic unblock user : (Value in Hours)")
        RetriveSetting()
        c1Setting.Cols(0).AllowEditing = False
        Sessiontimeout = Convert.ToUInt16(c1Setting.GetData(1, "SettingValue"))
        PassAttempt = Convert.ToUInt16(c1Setting.GetData(2, "SettingValue"))
        AutomaticUnblock = Convert.ToUInt16(c1Setting.GetData(3, "SettingValue"))

    End Sub

    Private Sub TsSaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsSaveBtn.Click
        c1Setting.Update()
        c1Setting.FinishEditing()
        UpdateSetting()
        Me.Close()
    End Sub

    Private Sub TsCancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsCancelBtn.Click
        c1Setting.Update()
        c1Setting.FinishEditing()

        If (Sessiontimeout <> c1Setting.GetData(1, "SettingValue") Or PassAttempt <> c1Setting.GetData(2, "SettingValue") Or AutomaticUnblock <> c1Setting.GetData(3, "SettingValue")) Then
            Dim result As DialogResult
            result = MessageBox.Show("Do you want to save web setting , Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If (result = DialogResult.Yes) Then
                UpdateSetting()
                IsClose = True
                Me.Close()
            ElseIf (result = DialogResult.No) Then
                IsClose = True
                Me.Close()
            Else

            End If
        Else
            Me.Close()
        End If



    End Sub
    Public Function UpdateSetting()
        Dim dt As DataTable
        Dim ds As DataSet
        Dim objCon As New SqlConnection
        Try
            c1Setting.Update()
            c1Setting.FinishEditing()

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_Setting"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@IsRetriveSetting", SqlDbType.Bit)
            oparam.Value = False
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@SessionTimeOut", SqlDbType.Int)
            oparam.Value = c1Setting.Rows(1)(1)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@MaxPasswordAtmpt", SqlDbType.Int)
            oparam.Value = c1Setting.Rows(2)(1)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@AutomaticUserUnBlock", SqlDbType.Int)
            oparam.Value = c1Setting.Rows(3)(1)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Public Function RetriveSetting()
        Dim dt As DataTable
        Dim ds As DataSet
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_Setting"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@IsRetriveSetting", SqlDbType.Bit)
            oparam.Value = True
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            ds = New DataSet
            objDA.Fill(ds, "SettingData")
            dt = ds.Tables("SettingData")

            For i As Integer = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)(0).ToString().Trim() = "MaxPasswordAtmpt") Then
                    c1Setting.SetData(2, 1, dt.Rows(i)(1))
                End If
                If (dt.Rows(i)(0).ToString().Trim() = "SessionTimeOut") Then
                    c1Setting.SetData(1, 1, dt.Rows(i)(1))
                End If
                If (dt.Rows(i)(0).ToString().Trim() = "AutomaticUserUnBlock") Then
                    c1Setting.SetData(3, 1, dt.Rows(i)(1))
                End If
            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

   
    Private Sub frmWebServiceSetting_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        c1Setting.Update()
        c1Setting.FinishEditing()
        If (IsClose = True) Then
            e.Cancel = False
            Return
        Else
            If (Sessiontimeout <> c1Setting.GetData(1, "SettingValue") Or PassAttempt <> c1Setting.GetData(2, "SettingValue") Or AutomaticUnblock <> c1Setting.GetData(3, "SettingValue")) Then
                Dim result As DialogResult
                result = MessageBox.Show("Do you want to save web setting , Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                If (result = DialogResult.Yes) Then
                    UpdateSetting()
                ElseIf (result = DialogResult.No) Then

                Else
                    e.Cancel = True
                End If
            End If

        End If

    End Sub
End Class