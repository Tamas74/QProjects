Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Namespace gloprintfax
    Interface IDataDictionary
        'Function GetDictionary(ByVal m_flag As Boolean) As DataTable
        Function getReportData(ByVal strselect As String) As System.Data.DataTable
        Function GetClinicLogo() As DataTable
        Function GetProviderSign() As DataTable
        Function GetProviders() As ArrayList
        Function GetData(ByVal strfield As String) As String
        Function GetReport() As DataTable
    End Interface
    Friend Class CustomPrintDBLayer
        Implements IDataDictionary, IDisposable

        'Public Function GetDictionary(ByVal m_flag As Boolean) As System.Data.DataTable Implements IDataDictionary.GetDictionary
        '    Try

        '        Dim objconn As SqlConnection
        '        Dim strconn As String
        '        strconn = GetConnectionString()
        '        objconn = New SqlConnection(strconn)
        '        Dim objadapter As SqlDataAdapter
        '        If m_flag Then
        '            'objadapter = New SqlDataAdapter("SELECT patient.nPatientID, patient.sPatientCode,patient.sFirstName, patient.sMiddleName, patient.sLastName, patient.dtDOB, patient.nSSN, patient.sGender, patient.sMaritalStatus, patient.sAddressLine2, patient.sAddressLine1, patient.sCity,patient.sState, patient.sZIP, patient.sCounty, patient.sPhone, patient.sMobile, patient.sEmail, patient.sFAX, patient.sOccupation, patient.sEmploymentStatus, patient.sPlaceofEmployment, patient.sWorkAddressLine1,patient.sWorkAddressLine2, patient.sWorkCity, patient.sWorkState, patient.sWorkZIP, patient.sWorkPhone, patient.sWorkFAX, patient.sChiefComplaints, patient.nProviderID, patient.nPCPId, patient.sGuarantor, patient.nPharmacyID,patient.sSpousePhone, patient.sSpouseName, patient.sRace, patient.sPatientStatus, patient.iPhoto, patient.dtRegistrationDate, patient.dtInjuryDate, patient.dtSurgeryDate, patient.sHandDominance, patient.sLocation FROM Patient where 1=0", objconn)
        '            objadapter = New SqlDataAdapter("SELECT * FROM RxPrintFaxReport", objconn)
        '        Else
        '            ' objadapter = New SqlDataAdapter("SELECT provider_mst.nProviderID, provider_mst.sFirstName, provider_mst.sMiddleName, provider_mst.sLastName, provider_mst.sGender,provider_mst.sDEA,provider_mst.sAddress,provider_mst.sStreet, provider_mst.sCity,provider_mst. sState,provider_mst.sZIP, provider_mst.sPhoneNo,provider_mst.sFAX, provider_mst.sMobileNo,provider_mst.sPagerNo, provider_mst.sEmail, provider_mst.sURL, provider_mst.imgSignature FROM Provider_MST where 1=0", objconn)
        '        End If
        '        Dim objdatatable As New DataTable
        '        objadapter.Fill(objdatatable)
        '        Return objdatatable
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'End Function
        Private disposedValue As Boolean = False        ' To detect redundant calls

        Public Function getReportData(ByVal strselect As String) As System.Data.DataTable Implements IDataDictionary.getReportData
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable
            Try

                dt = _gloEMRDatabase.GetDataTable(strselect, False)
                Return dt
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Report Data"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Public Function GetClinicLogo() As DataTable Implements IDataDictionary.GetClinicLogo
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable
            Try

                dt =  _gloEMRDatabase.GetDataTable("Select imgClinicLogo as ClinicLogo from Clinic_MST", False)
                Return dt
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Clinic Logo"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Public Function GetProviderSign() As DataTable Implements IDataDictionary.GetProviderSign
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable
            Try


                Dim strselect As String = "SELECT Provider_MST.imgSignature AS ProviderSignature FROM Provider_MST INNER JOIN PrescriptionReport ON Provider_MST.nProviderID = PrescriptionReport.nProviderID"
                dt = _gloEMRDatabase.GetDataTable(strselect, False)
                Return dt
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Provider Signature"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Public Function GetProviders() As ArrayList Implements IDataDictionary.GetProviders
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As New DataTable
            Try
                Dim strQuery As String
                strQuery = "SELECT distinct ProviderSettings.nOthersID FROM PrescriptionReport INNER JOIN ProviderSettings ON PrescriptionReport.nProviderID = ProviderSettings.nProviderID and sSettingstype='ProviderSeniorAssignment'"
                dt = _gloEMRDatabase.GetDataTable(strQuery, False)
                Dim objProviders As New ArrayList
                Dim strProviderName As String
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For cntRow As Int16 = 0 To dt.Rows.Count - 1
                            strQuery = "select isnull(Provider_MST.sFirstName,'') + SPACE(1) + isnull(Provider_MST.sMiddleName,'') + SPACE(1) + isnull(Provider_MST.sLastName,'') + Space(1) + isnull(Provider_mst.sDEA,'') AS ProviderName"
                            strQuery &= " from Provider_MST where nProviderID=" & dt.Rows(cntRow).Item(0)
                            strProviderName = getProviderName(strQuery)
                            If Not strProviderName Is Nothing Then
                                objProviders.Add(strProviderName)
                            End If
                            strProviderName = Nothing
                            strQuery = Nothing
                        Next
                        Return objProviders
                    Else
                        Return Nothing
                    End If
                    dt.Dispose()
                    dt = Nothing
                Else
                    Return Nothing
                End If
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Providers List"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Public Function getProviderName(ByVal strSQL As String) As String
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim strProviderName As String
            Try
                strProviderName = _gloEMRDatabase.GetDataValue(strSQL)
                Return strProviderName
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching ProviderName"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function GetData(ByVal strfield As String) As String Implements IDataDictionary.GetData
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim strProviderName As String
            Dim strquery As String
            Try
                strquery = "select " & strfield & " from RxPrintFaxReport"
                strProviderName = _gloEMRDatabase.GetDataValue(strquery)
                Return strProviderName
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Report Data"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Public Function GetReport() As System.Data.DataTable Implements IDataDictionary.GetReport
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim oTable As DataTable
            Dim objParameter As DBParameter
            Try
                objParameter = New DBParameter
                objParameter.Value = "RxReport"
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sReportType"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                oTable = _gloEMRDatabase.GetDataTable("ScanReports_MST")
                Return oTable
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As PrintException
                objex = New PrintException
                objex.ErrMessage = "Error Fetching Report Data"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function


        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

        Public Sub New()
            MyBase.new()
        End Sub
    End Class
End Namespace