Imports System.Windows.Forms
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Generic

Public Class frmCustomLink

#Region "Global variables declaration"
    Private _dupLinkName As String = String.Empty
    Private _dupURL As String = String.Empty
    Private _txtmaxLength_Editor As New TextBox

#End Region

#Region "C1 Constant"
    Private Const C1CUSTLINK_COL_LINK_ID As Byte = 0
    Private Const C1CUSTLINK_COL_LINK_NAME As Byte = 1
    Private Const C1CUSTLINK_COL_URL As Byte = 2
    Private Const C1CUSTLINK_COL_LINK_DESCRIPTION As Byte = 3
    'Private Const C1CUSTLINK_COL_DATE_TIME_STAMP As Byte = 4
#End Region

#Region "User Defined Functions"
    ''Function to set format the grid
    Private Sub DesignC1CUSTLNKGrid()
        flxData.DataSource = Nothing
        flxData.Clear(ClearFlags.All)

        With flxData
            .Rows.Fixed = 1
            .Cols.Count = 4
            .Rows.Count = 1

            .Cols(C1CUSTLINK_COL_LINK_ID).Width = 0.1 * .Width
            .Cols(C1CUSTLINK_COL_LINK_NAME).Width = 0.2 * Width
            .Cols(C1CUSTLINK_COL_URL).Width = 0.35 * Width
            .Cols(C1CUSTLINK_COL_LINK_DESCRIPTION).Width = 0.45 * Width
            
            flxData.SetData(0, C1CUSTLINK_COL_LINK_ID, "Link ID")
            flxData.SetData(0, C1CUSTLINK_COL_LINK_NAME, "Link Name")
            flxData.SetData(0, C1CUSTLINK_COL_URL, "URL")
            flxData.SetData(0, C1CUSTLINK_COL_LINK_DESCRIPTION, "Link Description")
 
            .Cols(C1CUSTLINK_COL_LINK_ID).Visible = False

            .Cols(C1CUSTLINK_COL_LINK_NAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLINK_COL_LINK_NAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(C1CUSTLINK_COL_URL).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLINK_COL_URL).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(C1CUSTLINK_COL_LINK_DESCRIPTION).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLINK_COL_LINK_DESCRIPTION).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter


            .AllowSorting = AllowSortingEnum.None
            .SelectionMode = SelectionModeEnum.Row
            .AllowDragging = AllowDraggingEnum.None
        End With
    End Sub

    'Use added textbox as an item in C1 Cell for assigning max length.
    Private Sub flxData_EnterCell(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxData.EnterCell
        If flxData.RowSel > 0 Then
            If flxData.ColSel = C1CUSTLINK_COL_LINK_NAME Then
                _txtmaxLength_Editor.MaxLength = 100
                flxData.Cols(C1CUSTLINK_COL_LINK_NAME).Editor = _txtmaxLength_Editor
            ElseIf flxData.ColSel = C1CUSTLINK_COL_URL Then
                _txtmaxLength_Editor.MaxLength = 1024
                flxData.Cols(C1CUSTLINK_COL_URL).Editor = _txtmaxLength_Editor
            ElseIf flxData.ColSel = C1CUSTLINK_COL_LINK_DESCRIPTION Then
                _txtmaxLength_Editor.MaxLength = 255
                flxData.Cols(C1CUSTLINK_COL_LINK_DESCRIPTION).Editor = _txtmaxLength_Editor

            End If
        End If
    End Sub

    ''' <summary>
    ''' code to check duplicate records
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckDuplicateRecords() As Boolean
        _dupLinkName = String.Empty
        _dupURL = String.Empty
        Dim _lstLinkListCollection As New List(Of String)
        Dim _lstURLListCollection As New List(Of String)

        Try
            If flxData.Rows.Count > 0 Then
                Dim nCount As Int16
                For nCount = 1 To flxData.Rows.Count - 1
                    If Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() <> String.Empty And Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() <> String.Empty Then
                        'check if link is duplicate
                        If Not _lstLinkListCollection.Contains(Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim().ToLower()) Then
                            'check if URL is duplicate
                            If Not _lstURLListCollection.Contains(Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim().ToLower()) Then
                                _lstLinkListCollection.Add(Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim().ToLower())
                                _lstURLListCollection.Add(Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim().ToLower())
                            Else
                                MessageBox.Show("Multiple records found for URL '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                flxData.Select(nCount, C1CUSTLINK_COL_LINK_NAME)
                                Return False
                            End If
                        Else
                            If Not _lstURLListCollection.Contains(Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim()) Then
                                MessageBox.Show("Multiple records found for link name '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                MessageBox.Show("Multiple records found for link name '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() & " and URL '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            flxData.Select(nCount, C1CUSTLINK_COL_LINK_NAME)
                            Return False
                        End If 'end of checking fields are in collection of list loop
                    ElseIf Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() <> String.Empty And Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() = String.Empty Then 'check if URL is blank
                        MessageBox.Show("Enter the URL for link name '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        flxData.Select(nCount, C1CUSTLINK_COL_LINK_NAME)
                        Return False
                    ElseIf Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_LINK_NAME)).Trim() = String.Empty And Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() <> String.Empty Then 'check if Link Name is blank
                        MessageBox.Show("Enter the link name for URL '" & Convert.ToString(flxData.GetData(nCount, C1CUSTLINK_COL_URL)).Trim() & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        flxData.Select(nCount, C1CUSTLINK_COL_LINK_NAME)
                        Return False
                    End If 'end of checking fields are empty loop
                Next
                Return True
            End If 'end of  loop for checking if flex data has row
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateErrorLog("Unable to Load the provider Form due to " & ex.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Retrieve, True)

            Return False
        Finally
            _lstLinkListCollection = Nothing
            _lstURLListCollection = Nothing
            _dupLinkName = String.Empty
            _dupURL = String.Empty
        End Try
    End Function
#End Region

#Region "Form Event"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(flxData)
        _txtmaxLength_Editor.Text = String.Empty
        BindDataToGrid()
    End Sub

    Private Sub BindDataToGrid()
        Try
            Call DesignC1CUSTLNKGrid()
            retriveCustomLink()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub tstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked
        Dim _intLinkID As Int64 = 0
        Try
            Select Case Convert.ToString(e.ClickedItem.Tag).ToLower()
                Case "saveclose"
                    AddCustomLink()
                    'retriveCustomLink()
                Case "close"
                    Me.Close()
                    If Not IsNothing(_txtmaxLength_Editor) Then
                        _txtmaxLength_Editor.Dispose()
                        _txtmaxLength_Editor = Nothing
                    End If
                Case "delete"
                    If flxData.RowSel > 0 Then
                        If Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLINK_COL_LINK_ID)).Trim() <> String.Empty Then
                            If MessageBox.Show("Are you sure you want to delete '" & Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLINK_COL_LINK_NAME)).Trim & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                _intLinkID = Convert.ToInt64(flxData.GetData(flxData.RowSel, C1CUSTLINK_COL_LINK_ID))
                                If DeleteCustomLink(Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLINK_COL_LINK_NAME)).Trim, _intLinkID) Then ', Convert.ToString(flxData.GetData(flxData.RowSel, COL_URL)).Trim()) = True Then
                                    flxData.Rows.Remove(flxData.RowSel)
                                End If
                                'retriveCustomLink()
                            
                            End If
                        Else
                            'Code to check if user added record in grid and delete immediately by RK on 20110921
                            If Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLINK_COL_LINK_NAME)).Trim() <> String.Empty Then
                                If MessageBox.Show("Are you sure you want to delete the selected record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                    flxData.Rows.Remove(flxData.RowSel)
                                End If
                            End If
                        End If
                    End If
            End Select
            'retriveCustomLink()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            _intLinkID = 0
        End Try
    End Sub

    ''' <summary>
    ''' Code to delete record from Table
    ''' </summary>
    ''' <param name="_strLink_Name"></param>
    ''' <param name="_nLink_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCustomLink(ByVal _strLink_Name As String, ByVal _nLink_ID As Int64) As Boolean ', ByVal strURL As String) As Boolean
        'Dim nUserId As Int64 = 0
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Try
            _oDbLayer.Connect(False)
            'Checking if link is configure for the user or not
            'If _oDbLayer.ExecuteScalar_Query("select COUNT(*) FROM User_ExternalCodes WHERE nLinkID=(SELECT nLinkID FROM User_CustomLinks_MST WHERE sLinkName='" & _strLink_Name & "')") > 0 Then 
            If _oDbLayer.ExecuteScalar_Query("select COUNT(*) FROM User_ExternalCodes WHERE nLinkID=" & _nLink_ID & "") > 0 Then '(SELECT nLinkID FROM User_CustomLinks_MST WHERE sLinkName='" & _strLink_Name & "')
                _oDbLayer.Disconnect()
                MessageBox.Show("Cannot delete '" & _strLink_Name & "'. It is already in use.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
                Exit Function
            End If
            'Deleting the link from table.
            _oDbLayer.Execute_Query("delete from User_CustomLinks_MST where nLinkID=" & _nLink_ID & "")
            _oDbLayer.Disconnect()
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
            Return False
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' code for checking duplicate and validation and save method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCustomLink()
        flxData.FinishEditing()
        Dim _strLinkName As String = String.Empty
        Dim _strURL As String = String.Empty
        Dim _strLinkDescription As String = String.Empty
        Dim _intLinkID As Int64 = 0
        Dim intRecCnt As Int32 = 0
        Try
            If flxData.Rows.Count > 0 Then
                If Not CheckDuplicateRecords() Then 'checking for duplicates records
                    Exit Sub
                End If
                'Loop for adding data
                For intRecCnt = 0 To flxData.Rows.Count - 1
                    If intRecCnt > flxData.Rows.Count Then
                        Exit For
                    End If
                    If intRecCnt = 0 Then
                        Continue For
                    End If
                    _strLinkName = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLINK_COL_LINK_NAME)).Trim()
                    _strURL = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLINK_COL_URL)).Trim()
                    _strLinkDescription = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLINK_COL_LINK_DESCRIPTION)).Trim()

                    If (_strLinkName <> String.Empty) AndAlso (Not IsNothing(_strLinkName)) Then
                        If (Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLINK_COL_LINK_ID)).Trim()) <> String.Empty Then
                            _intLinkID = Convert.ToInt64(flxData.GetData(intRecCnt, C1CUSTLINK_COL_LINK_ID))
                        Else
                            _intLinkID = 0
                        End If
                        '_strURL = System.Text.RegularExpressions.Regex.Replace(_strURL, System.Text.RegularExpressions.Regex.Escape("|-uid-|"), "|-UID-|", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                        '_strURL = System.Text.RegularExpressions.Regex.Replace(_strURL, System.Text.RegularExpressions.Regex.Escape("|-pwd-|"), "|-PWD-|", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                        _strURL = Microsoft.VisualBasic.Strings.Replace(_strURL, "|-uid-|", "|-UID-|", 1, -1, CompareMethod.Text)
                        _strURL = Microsoft.VisualBasic.Strings.Replace(_strURL, "|-pwd-|", "|-PWD-|", 1, -1, CompareMethod.Text)
                        SaveCustomLink(_intLinkID, _strLinkName, _strURL, _strLinkDescription) 'storing information to table
                    Else
                        Continue For
                    End If
                    'Clean up the local variable
                    _strLinkName = String.Empty
                    _strURL = String.Empty
                    _strLinkDescription = String.Empty
                    _intLinkID = 0
                Next
            End If
            retriveCustomLink()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    ''' <summary>
    ''' code to save/update records in table
    ''' </summary>
    ''' <param name="_nUID"></param>
    ''' <param name="_sLinkName"></param>
    ''' <param name="_sURL"></param>
    ''' <param name="_sLinkDescription"></param>
    ''' <remarks></remarks>
    Private Sub SaveCustomLink(ByVal _nUID As Int64, ByVal _sLinkName As String, ByVal _sURL As String, ByVal _sLinkDescription As String)
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _oDbParameters As New gloDatabaseLayer.DBParameters()
        Try
            _oDbLayer.Connect(False)
            _oDbParameters.Add("@UID", _nUID, ParameterDirection.Input, SqlDbType.BigInt)
            _oDbParameters.Add("@sLinkName", _sLinkName, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@sURL", _sURL, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@LikStatus", 1, ParameterDirection.Input, SqlDbType.BigInt)
            _oDbParameters.Add("@LinkDesc", _sLinkDescription, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@dtLastAccess", Now, ParameterDirection.Input, SqlDbType.DateTime)
            _oDbLayer.Execute("INUP_User_CustomLinks_MST", _oDbParameters)
            _oDbLayer.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
            If Not IsNothing(_oDbParameters) Then
                _oDbParameters.Dispose()
                _oDbParameters = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Code to retrieve records and shown them in flxgrid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub retriveCustomLink() ''' To display records into flex grid
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim dtCustomLink As DataTable = New DataTable()
        Dim _strSql As String = String.Empty
        Dim intRecCnt As Int32
        Try
            _strSql = "select ISNULL(nLinkID,0) [Link ID], ISNULL(sLinkName,'') [Link Name], ISNULL(sURL,'') [URL], ISNULL(sLinkDesc,'') [Link Description] FROM User_CustomLinks_MST Order by sLinkName"

            _oDbLayer.Connect(False)
            _oDbLayer.Retrive_Query(_strSql, dtCustomLink)

            _oDbLayer.Disconnect()
            If (Not IsNothing(dtCustomLink)) AndAlso (dtCustomLink.Rows.Count > 0) Then
                If flxData.Rows.Count < dtCustomLink.Rows.Count + 1 Then
                    While flxData.Rows.Count < dtCustomLink.Rows.Count + 1
                        flxData.Rows.Add()
                    End While
                End If
                For intRecCnt = 1 To dtCustomLink.Rows.Count
                    flxData.SetData(intRecCnt, C1CUSTLINK_COL_LINK_ID, Convert.ToInt64(dtCustomLink.Rows(intRecCnt - 1)(C1CUSTLINK_COL_LINK_ID)))
                    flxData.SetData(intRecCnt, C1CUSTLINK_COL_LINK_NAME, dtCustomLink.Rows(intRecCnt - 1)(C1CUSTLINK_COL_LINK_NAME))
                    flxData.SetData(intRecCnt, C1CUSTLINK_COL_URL, dtCustomLink.Rows(intRecCnt - 1)(C1CUSTLINK_COL_URL))
                    flxData.SetData(intRecCnt, C1CUSTLINK_COL_LINK_DESCRIPTION, dtCustomLink.Rows(intRecCnt - 1)(C1CUSTLINK_COL_LINK_DESCRIPTION))
                    'flxData.SetData(i, COL_DateTimeStamp, dtCustomLink.Rows(i - 1)(COL_DateTimeStamp))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            _strSql = String.Empty
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
            If Not IsNothing(dtCustomLink) Then
                dtCustomLink.Dispose()
                dtCustomLink = Nothing
            End If
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        flxData.Rows.Add() 'For adding new Row in C1 flex grid
    End Sub
#End Region
 End Class