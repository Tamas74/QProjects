Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmViewLabReport

#Region "Private variables"
    Private m_OrderID As Long = 0

    'sarika Lab Order History PrintFax Fix 20090511
    Private m_TestNames As ArrayList = Nothing
    '--
#End Region

#Region "Properties"
    Public Property OrderID() As Long
        Get
            Return m_OrderID
        End Get
        Set(ByVal value As Long)
            m_OrderID = value
        End Set
    End Property

    'sarika Lab Order History PrintFax Fix 20090511
    Public Property arrTestNames() As ArrayList
        Get
            Return m_TestNames
        End Get
        Set(ByVal value As ArrayList)
            m_TestNames = value
        End Set
    End Property
    '--

#End Region

    Dim orpt As ReportDocument

    Private Sub frmViewLabReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            PrintReport()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub PrintReport()
        orpt = New ReportDocument
        orpt.Load(Application.StartupPath & "\Reports\rptLabOrderReport.rpt")

        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        ' Dim TableCounter


        Try

       
            With crConnectionInfo
                .AllowCustomConnection = True
                .ServerName = gstrSQLServerName
                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 
                .DatabaseName = gstrDatabaseName
                '.UserID = "Your User ID"
                '.Password = "Your Password"
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = orpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

            Next
         

            'sarika Lab Order History PrintFax Fix 20090511

            'oRpt.Load(_strPath)
            ' orpt.SetParameterValue("OrderID", m_OrderID.ToString)

            Dim prm As ParameterValues
            Dim discreteval As ParameterDiscreteValue

            orpt.Refresh()

            ''//patientid
            prm = orpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = CType(OrderID, String)
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)


            prm = orpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            prm.Clear()

            Dim valcnt As Integer = 0
            For valcnt = 0 To arrTestNames.Count - 1
                discreteval = New ParameterDiscreteValue
                discreteval.Value = CType(arrTestNames.Item(valcnt), String)
                prm.Add(discreteval)
                discreteval = Nothing
            Next
            orpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            '-----------

            'If blnPrint = True Then
            '    oRpt.PrintToPrinter(1, False, 0, 0)
            'Else
            '    oRpt.PrintToPrinter(1, False, 0, 0)
            'End If

            CrystalReportViewer1.ReportSource = orpt

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    'Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

    '    Dim crConnectionInfo As New ConnectionInfo

    '    With crConnectionInfo
    '        .ServerName = gstrSQLServerName

    '        'If you are connecting to Oracle there is no 
    '        'DatabaseName. Use an empty string. 
    '        'For example, .DatabaseName = "" 

    '        .DatabaseName = gstrDatabaseName
    '        .IntegratedSecurity = True

    '        '.UserID = "Your User ID"
    '        '.Password = "Your Password"
    '    End With

    'End Sub
    'Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
    '    Dim crtableLogoninfos As New TableLogOnInfos
    '    Dim crtableLogoninfo As New TableLogOnInfo

    '    Dim CrTables As Tables
    '    Dim CrTable As Table
    '    Dim TableCounter
    '    'This code works for both user tables and stored 
    '    'procedures. Set the CrTables to the Tables collection 
    '    'of the report 

    '    CrTables = rpt.Database.Tables

    '    'Loop through each table in the report and apply the 
    '    'LogonInfo information 

    '    For Each CrTable In CrTables
    '        crtableLogoninfo = CrTable.LogOnInfo
    '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '        CrTable.ApplyLogOnInfo(crtableLogoninfo)

    '        'If your DatabaseName is changing at runtime, specify 
    '        'the table location. 
    '        'For example, when you are reporting off of a 
    '        'Northwind database on SQL server you 
    '        'should have the following line of code: 

    '        CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
    '    Next
    'End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class