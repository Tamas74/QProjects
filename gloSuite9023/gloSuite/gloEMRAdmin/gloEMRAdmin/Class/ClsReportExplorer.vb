Imports System.Data.SqlClient
Imports gloSSRSApplication.SSRS

Public Class ClsReportExplorer
    Private Shared m_SelectionCriteria As String
    Private Shared m_CriteriaField As String
    Private Shared m_CriteriaValue As String
    Private Shared m_Condition As String

    Public Function GetFieldValue(ByVal mfield As String) As System.Data.DataTable
        Dim objconn As SqlConnection
        Dim strconn As String
        strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
        objconn = New SqlConnection(strconn)
        Dim objadapter As SqlDataAdapter
        Dim strstring As String
        strstring = "select " & mfield & " from " & Splittext(mfield)
        objadapter = New SqlDataAdapter(strstring, objconn)
        Dim objdatatable As New DataTable
        objadapter.Fill(objdatatable)
        Return objdatatable
    End Function
    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, ".")
            If arrstring.Length > 0 Then
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Else
            Return ""
        End If
    End Function
    Public Shared Property SelectionCriteria() As String
        Get
            Return m_SelectionCriteria
        End Get
        Set(ByVal Value As String)
            m_SelectionCriteria = Value
        End Set
    End Property

    Public Shared Property CriteriaField() As String
        Get
            Return m_CriteriaField
        End Get
        Set(ByVal Value As String)
            m_CriteriaField = Value
        End Set
    End Property
    Public Shared Property CriteriaValue() As String
        Get
            Return m_CriteriaValue
        End Get
        Set(ByVal Value As String)
            m_CriteriaValue = Value
        End Set
    End Property
    Public Shared Property Condition() As String
        Get
            Return m_Condition
        End Get
        Set(ByVal Value As String)
            m_Condition = Value
        End Set
    End Property
    Public Shared Function IsDeployReport(ByVal fileName As String) As Boolean
        Dim sReportProtocol As String = String.Empty
        Dim sReportfolder As String = String.Empty
        Dim sReportserver As String = String.Empty
        Dim sVirtualDir As String = String.Empty
        Dim oSetting As New gloSettings.GeneralSettings(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oValue As New Object()

        Try
            oSetting.GetSetting("ReportProtocol", oValue)
            If oValue IsNot Nothing Then
                sReportProtocol = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportServer", oValue)
            If oValue IsNot Nothing Then
                sReportserver = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportFolder", oValue)
            If oValue IsNot Nothing Then
                sReportfolder = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportVirtualDirectory", oValue)
            If oValue IsNot Nothing Then
                sVirtualDir = oValue.ToString()
                oValue = Nothing
            End If

            If sReportProtocol = "" OrElse sReportserver = "" OrElse sReportfolder = "" OrElse sVirtualDir = "" Then
                Cursor.Current = Cursors.[Default]
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "CustumizeRxReportType", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Dim rs As New ReportingService2005()
            Dim items As CatalogItem() = Nothing
            Dim condition As New SearchCondition()

            rs.Credentials = System.Net.CredentialCache.DefaultCredentials

            rs.Url = sReportProtocol & "://" & sReportserver & "/" & sVirtualDir & "/ReportService2005.asmx"

            condition.Condition = ConditionEnum.Equals
            condition.ConditionSpecified = True
            condition.Name = "Name"
            condition.Value = fileName

            Dim conditions As SearchCondition() = New SearchCondition(0) {}
            conditions(0) = condition

            items = rs.FindItems("/" & sReportfolder, BooleanOperatorEnum.[Or], conditions)
            Dim flgRDL As Boolean = False
            If items IsNot Nothing Then
                For Each ci As CatalogItem In items
                    flgRDL = True
                Next
            End If
            If Not IsNothing(rs) Then
                rs.Dispose()
                rs = Nothing
            End If
            Return flgRDL
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            If ex.Message = "Unable to connect to the remote server" OrElse ex.Message = "The request failed with HTTP status 404: ." OrElse ex.Message = "The underlying connection was closed: An unexpected error occurred on a send." Then
                Return False
            End If
        Finally
            If Not IsNothing(oSetting) Then
                oSetting.Dispose()
                oSetting = Nothing
            End If
            If Not IsNothing(oValue) Then
                oValue = Nothing
            End If
        End Try

    End Function
End Class
