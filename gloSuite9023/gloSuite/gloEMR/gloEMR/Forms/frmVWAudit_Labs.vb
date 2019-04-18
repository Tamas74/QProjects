Imports C1.Win.C1FlexGrid
Imports gloOffice

Public Class frmVWAudit_Labs

#Region "Attributes and Properties"
    Dim oclsAudit As clsAuditHistory
    Dim ds As DataSet = Nothing
   
    Private _nTransactionID As Int64
    Private _sActivityType As String
    Private _sActivityCategory As String
    
#End Region

#Region "Form Initialization"

    Private Sub frmVWAudit_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            c1Order.AllowEditing = False
            c1Test.AllowEditing = False
            c1Result.AllowEditing = False

            Select Case _sActivityCategory
                Case "Labs"
                    FillLabOrder(_nTransactionID, _sActivityType)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub New(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal sActivityCategory As String)
        _nTransactionID = nTransactionID
        _sActivityType = sActivityType
        _sActivityCategory = sActivityCategory

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub tsbtn_CloseAudit_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_CloseAudit.Click
        Try
            If ds IsNot Nothing Then
                ds.Clear()
                ds.Dispose()
                ds = Nothing
            End If

            If oclsAudit IsNot Nothing Then
                oclsAudit = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

#End Region

#Region "Grid Filling Procedures"

    Private Sub FillLabOrder(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()

        Try
            ds = New DataSet
            ds = oclsAudit.GetLabOrderHistory(nTransactionID, sActivityType)

            If Not IsNothing(ds) Then
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    c1Order.Clear()
                    c1Order.DataSource = ds.Tables(0).DefaultView
                    c1Order.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Order.AllowSorting = False

                    c1Order.Cols("Activity Date Time").DataType = GetType(String)
                    c1Order.Cols("Activity Date Time").Format = "MM/dd/yyyy h:mm tt"
                    c1Order.Cols("Activity Date Time").Width = 150

                    c1Order.Cols("User Action").Width = 100
                    c1Order.Cols("Order #").Width = 100
                    c1Order.Cols("Reference #").Width = 130

                    c1Order.Cols("Order Date").DataType = GetType(String)
                    c1Order.Cols("Order Date").Format = "MM/dd/yyyy h:mm tt"
                    c1Order.Cols("Order Date").Width = 150

                    c1Order.Cols("Order Status").Width = 150
                    c1Order.Cols("Billing Type").Width = 130
                    c1Order.Cols("Provider Name").Width = 130
                    c1Order.Cols("Electronic Order Status").Width = 150

                    c1Test.Clear()
                    c1Test.DataSource = ds.Tables(1).DefaultView
                    c1Test.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Test.AllowSorting = False

                    c1Test.Cols("Activity Date Time").DataType = GetType(String)
                    c1Test.Cols("Activity Date Time").Format = "MM/dd/yyyy h:mm tt"
                    c1Test.Cols("Activity Date Time").Width = 150

                    c1Test.Cols("User Action").Width = 100
                    c1Test.Cols("Test").Width = 150
                    c1Test.Cols("Instruction").Width = 150
                    c1Test.Cols("Precaution").Width = 150
                    c1Test.Cols("Comments").Width = 150

                    c1Test.Cols("Scheduled").DataType = GetType(String)
                    c1Test.Cols("Scheduled").Format = "MM/dd/yyyy h:mm tt"
                    c1Test.Cols("Scheduled").Width = 150

                    c1Test.Cols("Status").Width = 150

                    c1Test.Cols("DateTime").DataType = GetType(String)
                    c1Test.Cols("DateTime").Format = "MM/dd/yyyy h:mm tt"
                    c1Test.Cols("DateTime").Width = 150

                    c1Test.Cols("labotd_TestID").Width = 0
                    c1Test.Cols("labotd_TestID").Visible = False
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub FillLabResults(ByVal dv As DataView)
        Try

            If c1Test.RowSel < 0 Then Exit Sub

            dv.RowFilter = "labotrd_TestID = " & Convert.ToInt64(c1Test.GetData(c1Test.RowSel, "labotd_TestID"))

            c1Result.DataSource = dv
            c1Result.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
            c1Result.AllowSorting = False

            c1Result.Cols("Activity Date Time").DataType = GetType(String)
            c1Result.Cols("Activity Date Time").Format = "MM/dd/yyyy h:mm tt"
            c1Result.Cols("Activity Date Time").Width = 150

            c1Result.Cols("User Action").Width = 100
            c1Result.Cols("Result").Width = 150
            c1Result.Cols("Value").Width = 100
            c1Result.Cols("Unit").Width = 100
            c1Result.Cols("Normal Range").Width = 100
            c1Result.Cols("Flag").Width = 100
            c1Result.Cols("Result Type").Width = 100

            c1Result.Cols("Collected").DataType = GetType(String)
            c1Result.Cols("Collected").Format = "MM/dd/yyyy h:mm tt"
            c1Result.Cols("Collected").Width = 150

            c1Result.Cols("LOINC Code").Width = 100
            c1Result.Cols("Lab Test Code").Width = 100

            c1Result.Cols("DateTime").DataType = GetType(String)
            c1Result.Cols("DateTime").Format = "MM/dd/yyyy h:mm tt"
            c1Result.Cols("DateTime").Width = 150

            c1Result.Cols("labotrd_TestID").Width = 0
            c1Result.Cols("labotrd_TestID").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
#End Region

    Private Sub c1Test_AfterSelChange(sender As System.Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles c1Test.AfterSelChange
        Try
            FillLabResults(ds.Tables(2).DefaultView)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
End Class