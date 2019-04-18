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

Partial Public Class frmPrinterSetup
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
    Private dtPrinter As DataTable
    Private isOpenForUB04Settings As Boolean = False
#End Region

#Region "Contructor"

    Public Sub New()

        InitializeComponent()

    End Sub
    Public Sub New(ByVal printerDataTable As DataTable)
        InitializeComponent()
        dtPrinter = printerDataTable

    End Sub

    Public Sub New(ByVal printerDataTable As DataTable, ByVal isOpenForUB04 As Boolean)
        InitializeComponent()
        dtPrinter = printerDataTable
        isOpenForUB04Settings = isOpenForUB04
    End Sub
#End Region

    Private Sub getPrinters()
        Try

            With C1PrinterList
                .Cols.Count = 1
                .Rows.Fixed = 1
                .AllowEditing = False
                .SetData(.Rows.Count - 1, 0, "Printer Name")
                Dim pName As String = ""
                Dim listConnPrinters As New List(Of String)()

                'all connectcted printers added to list
                For Each printerName As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
                    listConnPrinters.Add(printerName)
                Next
                'those printer present in dtprinters are removes from list
                For Each row As DataRow In dtPrinter.Rows
                    If listConnPrinters.Contains(row("sPrinterName")) Then
                        listConnPrinters.Remove(row("sPrinterName"))
                    End If

                Next
                'Binding thel ist to c1flex grid to display list in new dialog box

                For Each itm As String In listConnPrinters
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, 0, itm)
                Next
                If .Rows.Count > 0 Then
                    C1PrinterList.Select()
                End If
                listConnPrinters = Nothing
                .ExtendLastCol = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub frmPrinterSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            getPrinters()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub tsb_Close_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Close.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub OpenCMSPrinterSettingForm()
        Dim selectedPrinter As String = C1PrinterList.Selection.Data
        Dim oFrm As New frmCMSPrinterSettingsNew(selectedPrinter)
        oFrm._IsSelected = True
        oFrm.ShowDialog()
        oFrm.Dispose()
        If oFrm.DialogResult.GetHashCode() = Windows.Forms.DialogResult.OK.GetHashCode() Then
            Me.Close()
        End If
        oFrm = Nothing
    End Sub

    Private Sub OpenUB04PrinterSettingForm()
        Dim selectedPrinter As String = C1PrinterList.Selection.Data
        Dim oFrm As New frmUB04PrinterSettings(selectedPrinter)
        oFrm.ShowDialog()
        oFrm.Dispose()
        If oFrm.DialogResult.GetHashCode() = Windows.Forms.DialogResult.OK.GetHashCode() Then
            Me.Close()
        End If
        oFrm = Nothing
    End Sub

    Private Sub tsb_Select_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Select.Click
         Try
            If C1PrinterList.RowSel > 0 Then
                If isOpenForUB04Settings = True Then
                    OpenUB04PrinterSettingForm()
                Else
                    OpenCMSPrinterSettingForm()
                End If
            Else
                MessageBox.Show("No more printer available to add.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub C1PrinterList_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1PrinterList.MouseDoubleClick
        Try
            If C1PrinterList.RowSel > 0 Then
                If isOpenForUB04Settings = True Then
                    OpenUB04PrinterSettingForm()
                Else
                    OpenCMSPrinterSettingForm()
                End If
            Else
                MessageBox.Show("No more printer available to add", "Printer Not Found", MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub
End Class


