Imports System.Data.SqlClient
Public Class clsViewGraphs


    ' function for Get calculated AGE, Height, Weight, DOB, Vital Date
    Public Function ScanAgeHtWt(ByVal PatientID As Long) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim cmd As New SqlCommand
        Try
            ' connectin string
            Dim Con As New SqlConnection
            Con.ConnectionString = GetConnectionString()

            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                ' get vital data for the patient
                .CommandText = "gsp_viewGraph"
                '.CommandText = "gsp_weightAge"
            End With

            cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            cmd.Parameters(0).Value = PatientID

            da = New SqlDataAdapter(cmd)
            dt = New DataTable
            ' fill data into the datatable
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            da.Dispose()
            da = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing

            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetRespiratoryData(ByVal _PatientId As Long) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim cmd As New SqlCommand

        Try
            ' connectin string
            Dim Con As New SqlConnection
            Con.ConnectionString = GetConnectionString()

            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                ' get vital data for the patient
                .CommandText = "gsp_GetRespiratory_Rate_Celcius"

            End With
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            cmd.Parameters(0).Value = _PatientId

            da = New SqlDataAdapter(cmd)
            dt = New DataTable
            ' fill data into the datatable
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            da.Dispose()
            da = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ' function for Get standard value for the patient according to the age, male/female/others
    Public Function GetMinMaxValues(ByVal Gender As frmShowGraphs.Gender, ByVal GraphType As frmShowGraphs.GraphType) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim cmd As New SqlCommand
        Try
            Dim Con As New SqlConnection
            Con.ConnectionString = GetConnectionString()

            cmd = New SqlCommand
            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                '.CommandText = "gsp_viewGraphMinMax"
                '.CommandText = "gsp_weightAge"
                .CommandText = "gVG_GetStandardVitals"
            End With

            cmd.Parameters.Add("@Sex", SqlDbType.Int)
            cmd.Parameters(0).Value = Gender
            cmd.Parameters.Add("@pType", SqlDbType.Int)
            cmd.Parameters(1).Value = GraphType

            dt = New DataTable
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            da.Dispose()
            da = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try
    End Function

    '' 20101020 - Commneted as no referance found 
    'Public Function GetRespiratoryData() As DataTable
    '    Try
    '        ' connectin string
    '        Dim Con As New SqlConnection
    '        Con.ConnectionString = GetConnectionString()

    '        With cmd
    '            .Connection = Con
    '            .CommandType = CommandType.StoredProcedure
    '            ' get vital data for the patient
    '            .CommandText = "gsp_GetRespiratory_Rate_Celcius"

    '        End With
    '        cmd.Parameters.Clear()
    '        cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
    '        cmd.Parameters(0).Value = gnPatientID

    '        da = New SqlDataAdapter(cmd)
    '        dt = New DataTable
    '        ' fill data into the datatable
    '        da.Fill(dt)
    '        Return dt
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)

    '    End Try
    'End Function


    Public Function getminmaxvalues20yrs(ByVal Gender As frmShowGraphs.Gender, ByVal GraphType As frmShowGraphs.GraphType, ByVal ageInMonths As Integer) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim cmd As New SqlCommand
        Try
            Dim Con As New SqlConnection
            Con.ConnectionString = GetConnectionString()

            cmd = New SqlCommand
            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                '.CommandText = "gsp_viewGraphMinMax"
                '.CommandText = "gsp_weightAge"
                .CommandText = "gVG_GetStandardVitals20yrs"
            End With

            cmd.Parameters.Add("@Sex", SqlDbType.Int)
            cmd.Parameters(0).Value = Gender
            cmd.Parameters.Add("@pType", SqlDbType.Int)
            cmd.Parameters(1).Value = GraphType
            cmd.Parameters.Add("@pAgeInMonths", SqlDbType.Int)
            cmd.Parameters(2).Value = ageInMonths

            dt = New DataTable
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            da.Dispose()
            da = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function getminmaxvaluesWtHt(ByVal Gender As frmShowGraphs.Gender) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim cmd As New SqlCommand
        Try
            Dim Con As New SqlConnection
            Con.ConnectionString = GetConnectionString()
            cmd = New SqlCommand
            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                '.CommandText = "gsp_viewGraphMinMax"
                '.CommandText = "gsp_weightAge"
                .CommandText = "gVG_GetStandardVitalsWtHt"
            End With

            cmd.Parameters.Add("@Sex", SqlDbType.Int)
            cmd.Parameters(0).Value = Gender

            dt = New DataTable
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            da.Dispose()
            da = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetPatientGender(ByVal nPatientId As Long) As Integer
        Dim Con As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try

            Con.ConnectionString = GetConnectionString()

            Dim strGender As String
            cmd = New SqlCommand("gsp_GetPatientGender", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientId

            Con.Open()
            strGender = cmd.ExecuteScalar()
            Con.Close()
            Select Case strGender
                Case "Male"
                    GetPatientGender = 1
                Case "Female"
                    GetPatientGender = 2
                Case Else
                    GetPatientGender = 3
            End Select


        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Vital Graphs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Vital Graphs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Sub New()

    End Sub
End Class
Public Class Hardcopy

    Public Shared Function CreateBitmap(ByVal Control As Control) As Bitmap

        Dim gDest As Graphics

        Dim hdcDest As IntPtr

        Dim hdcSrc As Integer

        Dim hWnd As Integer = Control.Handle.ToInt32

        CreateBitmap = New Bitmap(Control.Width, Control.Height)

        gDest = Graphics.FromImage(CreateBitmap)

        hdcSrc = Win32.GetWindowDC(hWnd)

        hdcDest = gDest.GetHdc

        Win32.BitBlt(hdcDest.ToInt32, 0, 0, Control.Width, Control.Height, hdcSrc, 0, 0, Win32.SRCCOPY)

        gDest.ReleaseHdc(hdcDest)

        Win32.ReleaseDC(hWnd, hdcSrc)
        gDest.Dispose()
        Return CreateBitmap
    End Function

End Class
Public Class Win32

    Public Declare Function BitBlt Lib "gdi32" Alias "BitBlt" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer

    Public Declare Function GetWindowDC Lib "user32" Alias "GetWindowDC" (ByVal hwnd As Integer) As Integer

    Public Declare Function ReleaseDC Lib "user32" Alias "ReleaseDC" (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer

    Public Const SRCCOPY As Integer = &HCC0020

End Class
