Imports System.Data.SqlClient
'Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clsViewSummaryCareRecord
    Implements IDisposable
    Public Shared Connectionstring As String = ""
    Private dt As DataTable
    Private dv As DataView
    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return dt

        End Get
    End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            Return dv

        End Get
    End Property
    Public gstrMessageBoxCaption As String = "gloEMR"
    Public Function Get_SummaryCareRecord(ByVal PatientID As Long) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetPatientSummaryCareRecord", oParameters, dt)
            Return dt

        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return Nothing
        Catch ex As Exception
            'MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Public Function DeleteSummaryCareRecord(ByVal SummaryOfCareRecordStatusID As Int64, ByVal PatientID As Int64)
        
 
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        Dim dt As New DataTable()

        Try
            oDB.Connect(False)

            oParameters.Add("@SummaryOfCareRecordStatusID", SummaryOfCareRecordStatusID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
          
            oDB.Retrive("gsp_DeleteSummaryCareRecord", oParameters, dt)
            Return dt
            '  Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return -1
        Catch ex As Exception
            'MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return -1
        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function SummaryCareRecordDetails(patientID As Long, SummaryOfCareRecordStatusID As Int64, dtTransactionDate As Date, bRecordUnavailable As Boolean, strRecordRemark As String) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim sumofcare_ID As Int64 = 0
        '   Dim dt As New DataTable()

        Try
            oDB.Connect(False)

            oParameters.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@SummaryOfCareRecordStatusID", SummaryOfCareRecordStatusID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@dtTransactionDate", dtTransactionDate, ParameterDirection.Input, SqlDbType.DateTime)
            oParameters.Add("@bRecordUnavailable", bRecordUnavailable, ParameterDirection.Input, SqlDbType.Bit)
            oParameters.Add("@bRecordRemark", strRecordRemark, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@nProviderID", gloGlobal.gloPMGlobal.LoginProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            sumofcare_ID = oDB.ExecuteScalar("gsp_InUpPatientSummaryCareRecord", oParameters)
            Return sumofcare_ID
            '  Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return -1
        Catch ex As Exception
            'MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return -1
        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

End Class
