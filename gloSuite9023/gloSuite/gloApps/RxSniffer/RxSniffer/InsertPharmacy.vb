Imports RxSniffer.RxGeneral
Imports System.Data.SqlClient
Public Class InsertPharmacy

    Dim _strDbConnection As String


    Public Property strDbConnection() As String
        Get
            Return _strDbConnection
        End Get
        Set(ByVal value As String)
            _strDbConnection = value
        End Set
    End Property


    Dim _insPharmacies As Pharmacies
    Public Property insPharmacies() As Pharmacies
        Get
            Return _insPharmacies
        End Get
        Set(ByVal value As Pharmacies)
            _insPharmacies = value
        End Set
    End Property


    Public Sub InsertPharmacy()
        Dim objCon As New SqlConnection(strDbConnection)
        Try
            Dim opharmacy As Pharmacy
            For icnt As Int64 = 0 To insPharmacies.Count - 1
                opharmacy = insPharmacies.Item(icnt)

                Dim objcmd As New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text
                Dim strSQL As String = "select ncontactId from Contacts_mst where sNCPDPID='" & opharmacy.PharmacyID & "'"

                objcmd.CommandText = strSQL
                objCon.Open()
                Dim ncontactId As Int64 = objcmd.ExecuteScalar

                objcmd.Cancel()
                strSQL = String.Empty
                objcmd = New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text

                If ncontactId <> 0 Then
                    strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sAddressLine2='" & opharmacy.PharmacyAddress.Address2 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "',sPharmacyStatus='" & opharmacy.PharmacyStatus & "' where nContactId=" & ncontactId
                    '' strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "' where nContactId=" & ncontactId
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                Else

                    strSQL = "select isnull(max(isnull(nContactId,0)),0)+1 from Contacts_Mst"
                    objcmd.CommandText = strSQL
                    Dim Id As Int64 = objcmd.ExecuteScalar
                    objcmd.Cancel()
                    strSQL = String.Empty

                    objcmd = New SqlCommand
                    objcmd.Connection = objCon
                    objcmd.CommandType = CommandType.Text

                    'sarika Insert Clinic ID 20090227 
                    'insert clinicid as 1
                    ' strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "')"
                    strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2,nClinicID) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "'," & mdlGeneral.gnClinicID & ")"
                    '----

                    ''strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sexternalcode,ActiveStartTime,ActiveEndTime,sServiceLevel) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "')"
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                End If
                objCon.Close()
            Next
            mdlGeneral.UpdateLog("Pharmacy data was updated")


        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)

        Finally
            'strSQL = String.Empty
            'objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        End Try
    End Sub





End Class
