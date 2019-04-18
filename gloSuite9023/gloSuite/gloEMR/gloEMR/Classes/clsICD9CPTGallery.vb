Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class ICD9CPT
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub


    Private _ICD9Code As String
    Private _ICD9Indicator As String
    Private _ICD9CodeStatus As String
    Private _ICD9DescriptionShort As String
    Private _ICD9DescriptionMedium As String
    Private _ICD9DescriptionLong As String

    Private _CPTCode As String
    Private _CPTDescription As String
    Public Property ICD9Code() As String
        Get
            Return _ICD9Code
        End Get
        Set(ByVal value As String)
            _ICD9Code = value
        End Set
    End Property
    Public Property ICD9Indicator() As String
        Get
            Return _ICD9Indicator
        End Get
        Set(ByVal value As String)
            _ICD9Indicator = value
        End Set
    End Property
    Public Property ICD9CodeStatus() As String
        Get
            Return _ICD9CodeStatus
        End Get
        Set(ByVal value As String)
            _ICD9CodeStatus = value
        End Set
    End Property
    Public Property ICD9DescriptionShort() As String
        Get
            Return _ICD9DescriptionShort
        End Get
        Set(ByVal value As String)
            _ICD9DescriptionShort = value
        End Set
    End Property

    Public Property ICD9DescriptionMedium() As String
        Get
            Return _ICD9DescriptionMedium
        End Get
        Set(ByVal value As String)
            _ICD9DescriptionMedium = value
        End Set
    End Property

    Public Property ICD9DescriptionLong() As String
        Get
            Return _ICD9DescriptionLong
        End Get
        Set(ByVal value As String)
            _ICD9DescriptionLong = value
        End Set
    End Property
    Public Property CPTCode() As String
        Get
            Return _CPTCode
        End Get
        Set(ByVal value As String)
            _CPTCode = value
        End Set
    End Property
    Public Property CPTDescription() As String
        Get
            Return _CPTDescription
        End Get
        Set(ByVal value As String)
            _CPTDescription = value
        End Set
    End Property
End Class

Public Class CollICD9CPT
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index  

    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a widget at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no object exists, a messagebox is shown and the operation is 
            ' cancelled.
            'System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As ICD9CPT
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), ICD9CPT)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As ICD9CPT)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                Me.Clear()
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class
Public Class DBICD9CPT

    Public Sub New()

    End Sub

    Public Sub New(ByVal sConnectionString As String)
        If sConnectionString <> "" Then
            sConnectionStr = sConnectionString
        End If
    End Sub

    Public Sub Dispose()
       

       
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'Dim Cmd As SqlCommand
    'Dim sda As SqlDataAdapter
    ' Dim Con As SqlConnection = Nothing
    ' Dim dt As DataTable
    Dim sConnectionStr As String = ""
    Public Function FillICD9Gallery(ByVal oICD9Coll As CollICD9CPT, ByVal oProgressBar As ProgressBar, ByVal Deleteprev As Boolean) As Boolean
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim ICD9Code As String = ""
            Dim ICD9Indicator As String = ""
            Dim ICD9CodeStatus As String = ""
            Dim ICD9DescriptionShort As String = ""
            Dim ICD9DescriptionMedium As String = ""
            Dim ICD9DescriptionLong As String = ""
            sConnectionStr = GetConnectionString()
            Dim Con As SqlConnection = New SqlConnection(sConnectionStr)
            Con.Open()
            If Deleteprev = True Then
                Dim strSelectGallery As String = " Select nICD9ID from ICD9Gallery"
                Cmd = New SqlCommand(strSelectGallery, Con)
                Dim myObject = Cmd.ExecuteScalar()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
                If Not IsNothing(myObject) Then

                    If MessageBox.Show("Are You Sure to Delete Previous ICD9", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        Application.DoEvents()
                        Dim strTrucateQry As String = "Truncate Table ICD9Gallery"

                        Cmd = New SqlCommand(strTrucateQry, Con)
                        Cmd.ExecuteNonQuery()
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                End If
            End If

            oProgressBar.Enabled = True
            oProgressBar.Visible = True
            oProgressBar.Minimum = 0
            oProgressBar.Maximum = oICD9Coll.Count
            oProgressBar.Value = oProgressBar.Minimum
            For i As Integer = 0 To oICD9Coll.Count - 1

                ICD9Code = oICD9Coll.Item(i).ICD9Code
                ICD9Indicator = oICD9Coll.Item(i).ICD9Indicator
                ICD9CodeStatus = oICD9Coll.Item(i).ICD9CodeStatus
                ICD9DescriptionShort = oICD9Coll.Item(i).ICD9DescriptionShort
                ICD9DescriptionMedium = oICD9Coll.Item(i).ICD9DescriptionMedium
                ICD9DescriptionLong = oICD9Coll.Item(i).ICD9DescriptionLong

                Dim strSelectIDQry As String = "Select Max(nICD9ID)+1 from ICD9Gallery"
                Dim ICD9ID As Int64
                Cmd = New SqlCommand(strSelectIDQry, Con)
                Dim myObject = Cmd.ExecuteScalar
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing

                If IsNothing(myObject) OrElse IsDBNull(myObject) Then
                    ICD9ID = 1
                Else
                    ICD9ID = myObject
                End If

                Dim strInsertQry As String = "INSERT INTO ICD9Gallery(nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong) Values(" & ICD9ID & ",'" & ICD9Code & "','" & ICD9Indicator & "','" & ICD9CodeStatus & "','" & Replace(ICD9DescriptionShort, "'", "''") & "','" & Replace(ICD9DescriptionMedium, "'", "''") & "','" & Replace(ICD9DescriptionLong, "'", "''") & "')"
                Cmd = New SqlCommand(strInsertQry, Con)
                Cmd.ExecuteNonQuery()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing

                oProgressBar.Value = oProgressBar.Value + oProgressBar.Step
            Next

            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oProgressBar.Enabled = False
            oProgressBar.Visible = False
        End Try
    End Function

    Public Function FillCPTGallery(ByVal oCPTColl As CollICD9CPT, ByVal oProgressBar As ProgressBar, ByVal Deleteprev As Boolean) As Boolean
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim CPTCode As String = ""
            Dim CPTDescription As String = ""
          
            sConnectionStr = GetConnectionString()
            Dim Con As SqlConnection = New SqlConnection(sConnectionStr)
            Con.Open()

            If Deleteprev = True Then
                Dim strSelectQry As String = "Select nCPTID from CPTGallery "
                Cmd = New SqlCommand(strSelectQry, Con)
                Cmd.ExecuteNonQuery()
                Dim myObject = Cmd.ExecuteScalar()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
                If Not IsNothing(myObject) Then
                    If MessageBox.Show("Are You Sure to Delete Previous ICD9", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        Application.DoEvents()
                        Dim strTrucateQry As String = "Truncate Table CPTGallery"
                        Cmd = New SqlCommand(strTrucateQry, Con)
                        Cmd.ExecuteNonQuery()
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing

                    End If
                End If
            End If
            oProgressBar.Enabled = True
            oProgressBar.Visible = True
            oProgressBar.Minimum = 0
            oProgressBar.Maximum = oCPTColl.Count
            oProgressBar.Value = oProgressBar.Minimum
            For i As Integer = 0 To oCPTColl.Count - 1
                CPTCode = oCPTColl.Item(i).CPTCode
                CPTDescription = oCPTColl.Item(i).CPTDescription
                Dim strSelectIDQry As String = "Select Max(nCPTID)+1 from CPTGallery "
                Dim CPTID As Int64
                Cmd = New SqlCommand(strSelectIDQry, Con)
                Dim myObject = Cmd.ExecuteScalar
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing

                If IsNothing(myObject) OrElse IsDBNull(myObject) Then
                    CPTID = 1
                Else
                    CPTID = myObject
                End If

                Dim strInsertQry As String = "INSERT INTO CPTGallery(nCPTID, sCPTCode, sDescription) Values(" & CPTID & ",'" & CPTCode & "','" & Replace(CPTDescription, "'", "''") & "')"
                Cmd = New SqlCommand(strInsertQry, Con)
                Cmd.ExecuteNonQuery()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing

                oProgressBar.Value = oProgressBar.Value + oProgressBar.Step
            Next
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oProgressBar.Enabled = False
            oProgressBar.Visible = False
        End Try
    End Function
    'Code Added by Mayuri:20091003
    'To delete CPT from either CPTGallery or CurrentCPT 

    Public Sub DeleteCPT(ByVal ID As Long, ByVal _isSelectedCPTGallery As Boolean)
        Try
            Dim oCmd As SqlCommand
            Dim _strSQL As String = ""
            
            If _isSelectedCPTGallery = True Then
                _strSQL = "delete CPTGallery where nCPTID=" & ID & ""
            Else
                _strSQL = "delete CPT_MST where nCPTID=" & ID & ""
            End If

            sConnectionStr = GetConnectionString()
            Dim Con As SqlConnection = New SqlConnection(sConnectionStr)
                Con.Open()
            
            oCmd = New SqlCommand(_strSQL, Con)
            oCmd.ExecuteNonQuery()
            oCmd.Parameters.Clear()
            oCmd.Dispose()
            oCmd = Nothing

                Con.Close()
                Con.Dispose()
            Con = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="flag">0 for ICD9Code and 1 for ICD9Description</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetICD9Gallery(ByVal flag As Int16, ByVal strIndicator As String) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Try

            '            All()
            'New
            '            Revised()
            '            Blank()
           

            sConnectionStr = GetConnectionString()
            Dim Con As SqlConnection = New SqlConnection(sConnectionStr)

            Dim sda As SqlDataAdapter
            'code commented for optimization
            'Dim strSelectICD9 As String = ""
            'If strIndicator = "All" Then
            '    If flag = 0 Then ''Code
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery order by sICD9code"
            '    Else ''''Description
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery order by sDescriptionMedium"
            '    End If
            'ElseIf strIndicator = "New" Then
            '    If flag = 0 Then ''Code
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator = 'N' order by sICD9code "
            '    Else ''''Description
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator = 'N' order by sDescriptionMedium "
            '    End If
            'ElseIf strIndicator = "Revised" Then
            '    If flag = 0 Then ''Code
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator = 'R' order by sICD9code "
            '    Else ''''Description
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator = 'R' order by sDescriptionMedium "
            '    End If
            'ElseIf strIndicator = "No Change" Then
            '    If flag = 0 Then ''Code
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator <> 'N' and sIndicator <> 'R' order by sICD9code "
            '    Else ''''Description
            '        strSelectICD9 = "Select nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong From ICD9Gallery Where sIndicator <> 'N' and sIndicator <> 'R' order by sDescriptionMedium"
            '    End If
            'End If

            'Con.Open()
            'sda = New SqlDataAdapter(strSelectICD9, Con)
            'sda.Fill(dt)
            ''Con.Close()
            'Return dt
            ''''''''''''''''''''''''''''''''''''''
            '''''converted queries into stored procedure'''''''''''''
            Cmd = New SqlCommand("gsp_GetICD9Gallery", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@strIndicator", SqlDbType.VarChar, 10)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strIndicator
            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = flag

            Con.Open()
            'cmd.ExecuteNonQuery()

            sda = New SqlDataAdapter
            sda.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            sda.Fill(dt)
            If IsNothing(sda) = False Then
                sda.Dispose()
                sda = Nothing
            End If
            sqlParam = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing
            Return dt
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
         
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
       
        End Try

    End Function
    Public Function GetCPTGallery(ByVal flag As Int16) As DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            '''''code commented for optimization'''''''''''''
            'Con = New SqlConnection(GetConnectionString())
            'dt = New DataTable
            'Dim strSelectICD9 As String = ""
            'If flag = 0 Then
            '    strSelectICD9 = "Select nCPTID, sCPTCode, sDescription From CPTGallery order by sCPTCode"
            'Else
            '    strSelectICD9 = "Select nCPTID, sCPTCode, sDescription From CPTGallery order by sDescription"
            'End If

            'Con.Open()
            'sda = New SqlDataAdapter(strSelectICD9, Con)
            'sda.Fill(dt)
            'Con.Close()
            'Return dt
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''converted queries into stored procedure'''''''''''''
            Dim Con As SqlConnection = New SqlConnection(sConnectionStr)

            Cmd = New SqlCommand("gsp_GetCPTGallery", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = flag

            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            Con.Dispose()
            Con = Nothing
            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
        End Try

    End Function
End Class
