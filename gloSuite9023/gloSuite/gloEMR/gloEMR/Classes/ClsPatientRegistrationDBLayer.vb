Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ClsPatientRegistrationDBLayer
    Implements IDisposable
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        'Type = ContactType
    End Sub
    Private Conn As SqlConnection
    Private Dv As DataView
    '  Private Cmd As System.Data.SqlClient.SqlCommand
    Private ArrInsuranceCol As New ArrayList 'Arraylist of PatientInsurances

    'sarika  2nd nov 07
    Dim ArrInsuranceColOld As ArrayList
    '----


    'Shubhangi 20090826 
    'For Zip form

    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get
    End Property
    'End 

    Public Function FetchData()
        Dim adpt As New SqlDataAdapter

        Try

            Dim dt As New DataTable
            Dim Cmd As SqlCommand = New SqlCommand("gsp_ViewPatient", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            Dv = dt.Copy().DefaultView
            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If Not IsNothing(adpt) Then

                adpt.Dispose()
                adpt = Nothing
            End If

        End Try
        Return Nothing
    End Function

    '''' Patient Change History
    '''' 20061225 - Mahesh
    Public Sub ADD_ChangeHistory()
        Try

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub UpdateData_old(ByVal ArrList As ArrayList, ByVal arr1 As Array, ByVal ArrPatientOriginalData As ArrayList, Optional ByVal blnPhotoModify As Boolean = False, Optional ByVal IsChange As Boolean = False)

        Dim TrPatient As SqlTransaction = Nothing
        Dim cmddetails1 As SqlCommand = Nothing
        Dim cmddetails As SqlCommand = Nothing
        Dim cmddetails2 As SqlCommand = Nothing
        Dim cmddetails3 As SqlCommand = Nothing

        Dim objParamPatientId As SqlParameter
        Dim objParam As SqlParameter
        Dim sqlPara As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpPatient", Conn)

            Cmd.CommandType = CommandType.StoredProcedure

            '@nPatientID 			numeric(18,0), 
            '@sExternalPatientId		varchar(10),
            '@sFirstName			varchar(50),
            '@sMiddleName			varchar(50),
            '@sLastName			varchar(50),
            '@sSSN				varchar(20),
            '@dtDOB				datetime,
            '@sGender			varchar(10),
            '@sMaritalStatus			varchar(10),
            '@sstreet			varchar(50),
            '@sCity				varchar(50),
            '@sState				varchar(50),
            '@sZip				varchar(50),
            '@sPhone				varchar(50),
            '@sMobile			varchar(50),




            objParamPatientId = Cmd.Parameters.AddWithValue("@nPatientId", CType(ArrList.Item(0), Long))
            objParamPatientId.Direction = ParameterDirection.InputOutput
            'objParamPatientId.Value = CType(ArrList.Item(0), Long)

            objParam = Cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(1), System.String)

            objParam = Cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), System.String)

            objParam = Cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.String)

            objParam = Cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(4), System.String)

            'sarika SSN Datatype Change
            'objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.BigInt)
            objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.VarChar)
            '---------
            objParam.Direction = ParameterDirection.Input

            'sarika SSN Datatype Change
            If Len(Trim(ArrList.Item(5))) <> 9 Then 'Or ArrList.Item(5) = 0 Then
                objParam.Value = ""
            Else
                objParam.Value = CType(ArrList.Item(5), System.String)
            End If
            'objParam.Value = CType(ArrList.Item(5), System.Int64)

            'objParam = Cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CType(ArrList.Item(6), System.DateTime)

            objParam = Cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(7), System.String)

            objParam = Cmd.Parameters.Add("@sMaritalStatus", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(8), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(9), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(10), System.String)

            objParam = Cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(11), System.String)

            objParam = Cmd.Parameters.Add("@sState", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(12), System.String)

            objParam = Cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(13), System.String)

            objParam = Cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(14), System.String)

            objParam = Cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(15), System.String)

            '       @sEmail				varchar(50),
            '@sFax				varchar(50),
            '@sOccupation			varchar(50),
            '@sEmploymentStatus		varchar(20),
            '@sPlaceofEmployment		varchar(50),
            '@sWorkAddress			varchar(50),
            '@sWorkCity			varchar(50),
            '@sWorkState			varchar(50),
            '@sWorkZip			varchar(50),
            '@sWorkPhone			varchar(50),
            '@sWorkFax			varchar(50),
            '@sChiefComplaints		varchar(50),
            '@dRegistrationDate		datetime,
            '@sPCP				varchar(50),
            '@sGuarantor			varchar(50),


            objParam = Cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(16), System.String)

            objParam = Cmd.Parameters.Add("@sFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(17), System.String)

            objParam = Cmd.Parameters.Add("@sOccupation", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(18), System.String)

            objParam = Cmd.Parameters.Add("@sEmploymentStatus", SqlDbType.VarChar, 20)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(19), System.String)

            objParam = Cmd.Parameters.Add("@sPlaceofEmployment", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(20), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(21), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(22), System.String)

            'If Type = True Then
            objParam = Cmd.Parameters.Add("@sWorkCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(23), System.String)


            objParam = Cmd.Parameters.Add("@sWorkState", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(24), System.String)

            objParam = Cmd.Parameters.Add("@sWorkZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(25), System.String)


            objParam = Cmd.Parameters.Add("@sWorkPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(26), System.String)

            objParam = Cmd.Parameters.Add("@sWorkFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(27), System.String)

            objParam = Cmd.Parameters.Add("@sInsuranceNotes", SqlDbType.VarChar, 1500)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(28), System.String)

            objParam = Cmd.Parameters.Add("@nPCPID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(29), System.Int64)

            objParam = Cmd.Parameters.Add("@sGuarantor", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(30), System.String)

            '   @sPrimaryInsurancetype		varchar(50),
            '@sPrimarySubscriberName	varchar(50),
            '@sPrimarysubscriberID		varchar(50),
            '@sPrimarySubscriberPolicy#	varchar(50),
            '@sSecondarySubscriberID	varchar(50),
            '@sSecondarySubscriberName	varchar(50),
            '@sSecondarySubscriberPolicy#	varchar(50),
            '@sTertiarySubscriberId		varchar(50),
            '@sTertiarySubscriberName	varchar(50),
            '@sTertiarySubscriberPolicy#	varchar(50),
            '@sSpouseName		varchar(50),
            '@sSpousePhone		varchar(50),
            '@sRace			varchar(50),
            '@sPatientStatus			varchar(50),


            objParam = Cmd.Parameters.Add("@sSpouseName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(31), System.String)

            objParam = Cmd.Parameters.Add("@sSpousePhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(32), System.String)

            objParam = Cmd.Parameters.Add("@sRace", SqlDbType.VarChar, 1000)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(33), System.String)

            objParam = Cmd.Parameters.Add("@sPatientStatus", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(34), System.String)

            '   @nProviderId			numeric(18,0)=null,
            '@nReferralID			numeric(18,0)=null,
            '@nPharmacyID			numeric(18,0)=null,
            '@nPrimaryInsuranceId		numeric(18,0)=null

            objParam = Cmd.Parameters.Add("@Check1", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(35), System.Boolean)

            objParam = Cmd.Parameters.Add("@nProviderId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(36), System.Int64)


            'objParam = Cmd.Parameters.Add("@nReferralID", SqlDbType.Int)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CType(ArrList.Item(47), System.Int64)

            objParam = Cmd.Parameters.Add("@nPharmacyID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(37), System.Int64)

            objParam = Cmd.Parameters.Add("@sCounty", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(38), System.String)

            objParam = Cmd.Parameters.Add("@dtRegistrationdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(39), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check2", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(40), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtInjurydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(41), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check3", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(42), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtSurgerydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(43), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check4", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(44), System.Boolean)

            objParam = Cmd.Parameters.Add("@sHandDominance", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(45), System.String)

            objParam = Cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(46), System.String)

            'If blnPhotoModify = True Then
            '    objParam = Cmd.Parameters.Add("@PhotoModified", SqlDbType.Bit)
            '    objParam.Direction = ParameterDirection.Input
            '    objParam.Value = 1
            '    If IsNothing(ArrList.Item(47)) = False Then
            '        If IsDBNull(ArrList.Item(47)) = False Then
            '            'SLR: Why not directly assign arrList.Item on 12/22?
            '            'Dim ms As New MemoryStream
            '            'CType(ArrList.Item(47), Image).Save(ms, Imaging.ImageFormat.Bmp)
            '            'Dim arrImage() As Byte = ms.ToArray()
            '            'ms.Close()
            '            'ms.Dispose()
            '            'ms = Nothing
            '            objParam = Cmd.Parameters.Add("@Photo", SqlDbType.Image)
            '            objParam.Direction = ParameterDirection.Input
            '            objParam.Value = CType(ArrList.Item(47), Byte()).Clone() 'arrImage
            '        End If
            '    End If
            'End If

            ''''Code added by Ravikiran on 27/01/2007

            ''''Mother Details
            objParam = Cmd.Parameters.Add("@sMother_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(48), System.String)

            objParam = Cmd.Parameters.Add("@sMother_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(49), System.String)

            objParam = Cmd.Parameters.Add("@sMother_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(50), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(51), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(52), System.String)

            objParam = Cmd.Parameters.Add("@sMother_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(53), System.String)

            objParam = Cmd.Parameters.Add("@sMother_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(54), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(55), System.String)

            objParam = Cmd.Parameters.Add("@sMother_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(56), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(57), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(58), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(59), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(60), System.String)

            ''''Father Details

            objParam = Cmd.Parameters.Add("@sFather_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(61), System.String)

            objParam = Cmd.Parameters.Add("@sFather_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(62), System.String)

            objParam = Cmd.Parameters.Add("@sFather_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(63), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(64), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(65), System.String)

            objParam = Cmd.Parameters.Add("@sFather_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(66), System.String)

            objParam = Cmd.Parameters.Add("@sFather_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(67), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(68), System.String)

            objParam = Cmd.Parameters.Add("@sFather_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(69), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(70), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(71), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(72), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(73), System.String)

            ''''Guardian Details

            objParam = Cmd.Parameters.Add("@sGuardian_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(74), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(75), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(76), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(77), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(78), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(79), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(80), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(81), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(82), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(83), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(84), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(85), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(86), System.String)

            '// Patient Directive 5 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nPatientDirective", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(87), System.String)
            '// Patient Directive 5 Feb 2007 // - Vinayak

            '// Patient Directive 15 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nExemptFromRpt", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(88), System.String)
            '// Patient Directive 15 Feb 2007 // - Vinayak

            '''' Updation Ends
            Conn.Open()
            TrPatient = Conn.BeginTransaction
            Cmd.Transaction = TrPatient
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            If blnPhotoModify = True Then
                If IsNothing(ArrList.Item(47)) = False Then
                    If IsDBNull(ArrList.Item(47)) = False Then
                        Dim oPatientPhoto As New gloPatient.PatientPhoto()
                        oPatientPhoto.InsertPhoto(CType(ArrList.Item(0), Long), CType(ArrList.Item(47), Byte()).Clone())
                        oPatientPhoto.Dispose()
                        oPatientPhoto = Nothing
                    End If
                End If
            End If

            cmddetails1 = New SqlCommand("gsp_DeletePatient_DTL", Conn)
            cmddetails1.CommandType = CommandType.StoredProcedure
            cmddetails1.Transaction = TrPatient

            objParam = cmddetails1.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
            objParam.Direction = ParameterDirection.Input

            cmddetails1.ExecuteNonQuery()
            Dim i As Integer
            For i = 0 To arr1.Length - 1
                cmddetails = New SqlCommand("gsp_InsertPatient_DTL", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = TrPatient
                objParam = cmddetails.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = CType(ArrList.Item(0), Long)

                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr1(i)
                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next




            cmddetails2 = New SqlCommand("gsp_DeletePatientInsurance_DTL", Conn)
            cmddetails2.CommandType = CommandType.StoredProcedure
            cmddetails2.Transaction = TrPatient

            objParam = cmddetails2.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(0), Long)

            cmddetails2.ExecuteNonQuery()
            cmddetails2.Parameters.Clear()
            For i = 0 To ArrInsuranceCol.Count - 1
                Dim objPatientInsurance As ClsPatientInsurance

                objPatientInsurance = ArrInsuranceCol(i)

                cmddetails3 = New SqlCommand("gsp_InsertPatientInsurance_DTL", Conn)
                cmddetails3.CommandType = CommandType.StoredProcedure

                cmddetails3.Transaction = TrPatient
                objParam = cmddetails3.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = CType(ArrList.Item(0), Long)

                objParam = cmddetails3.Parameters.Add("@nInsuranceID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.InsuranceId

                objParam = cmddetails3.Parameters.Add("@sSubscribername", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Subscribername

                objParam = cmddetails3.Parameters.Add("@sSubscriberpolicy", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberPolicy

                objParam = cmddetails3.Parameters.Add("@sSubscriberId", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberId

                objParam = cmddetails3.Parameters.Add("@sGroup", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Group

                objParam = cmddetails3.Parameters.Add("@sEmployer", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Employer

                objParam = cmddetails3.Parameters.Add("@sPhone", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Phone

                objParam = cmddetails3.Parameters.Add("@primary", SqlDbType.Bit)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Primaryflag

                If objPatientInsurance.Checked = True Then

                    objParam = cmddetails3.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = objPatientInsurance.DOB
                End If


                'objParam = cmddetails3.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.DOB

                'objParam = cmddetails3.Parameters.Add("@checked", SqlDbType.Bit)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.Checked

                'objParam = cmddetails3.Parameters.Add("@primary", SqlDbType.Bit)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.Primaryflag


                cmddetails3.ExecuteNonQuery()
                cmddetails3.Parameters.Clear()
                cmddetails3.Dispose()
                cmddetails3 = Nothing
            Next

            If IsChange = True Then
                '''' To Add Patient Information Change History while Updating Patient's Information
                Dim cmdPatHistory As New SqlCommand("gsp_Insert_PatientChangeHistory", Conn, TrPatient)
                '@PatientID         Numeric(18,0),
                '@PatientChangeID   Numeric(18,0), 
                '@dtChangeDateTime  DateTime, 
                '@FirstName         Varchar(50), 
                '@MiddleName        Varchar(50), 
                '@LastName          Varchar(50), 
                '@dtDOB             DateTime, 
                '@Gender            Varchar(10), 
                '@AddressLine1      Varchar(255), 
                '@AddressLine2      Varchar(255), 
                '@City              Varchar(255), 
                '@State             Varchar(50), 
                '@ZIP               Varchar(50), 
                '@County            Varchar(50), 
                '@Phone             Varchar(50)
                cmdPatHistory.CommandType = CommandType.StoredProcedure

                With ArrPatientOriginalData
                    sqlPara = cmdPatHistory.Parameters.AddWithValue("@PatientId", CType(ArrList.Item(0), Long))
                    sqlPara.Direction = ParameterDirection.Input

                    sqlPara = cmdPatHistory.Parameters.Add("@dtChangeDateTime", SqlDbType.DateTime)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = Format(Now, "MM/dd/yyyy hh:mm tt")

                    sqlPara = cmdPatHistory.Parameters.Add("@FirstName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(1).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = ArrPatientOriginalData.Item(2).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@LastName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(3).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                    sqlPara.Direction = ParameterDirection.Input
                    Dim IsDOB As Int16
                    If IsDate(.Item(4)) = False Then
                        'sqlPara.Value = ""
                        sqlPara.Value = DBNull.Value
                        IsDOB = 0
                    Else
                        sqlPara.Value = CType(ArrPatientOriginalData.Item(4), DateTime)
                        IsDOB = 1
                    End If


                    sqlPara = cmdPatHistory.Parameters.Add("@Gender", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = ArrPatientOriginalData.Item(5).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(7).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(8).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@City", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(9).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@State", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(10).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@ZIP", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(11).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@County", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(12).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@Phone", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(13).ToString

                    'sqlPara = cmdPatHistory.Parameters.Add("@IsDate", SqlDbType.Bit)
                    'sqlPara.Direction = ParameterDirection.Input
                    'sqlPara.Value = IsDOB

                    cmdPatHistory.ExecuteNonQuery()
                End With
                ''''''

                If cmdPatHistory IsNot Nothing Then
                    cmdPatHistory.Parameters.Clear()
                    cmdPatHistory.Dispose()
                    cmdPatHistory = Nothing
                End If
            End If

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", CType(ArrList.Item(0), Long), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Modify, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            TrPatient.Commit()
            Conn.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            If IsNothing(TrPatient) = False Then
                TrPatient.Rollback()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(TrPatient) = False Then
                TrPatient.Dispose()
                TrPatient = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If cmddetails IsNot Nothing Then
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            End If
            If cmddetails1 IsNot Nothing Then
                cmddetails1.Parameters.Clear()
                cmddetails1.Dispose()
                cmddetails1 = Nothing
            End If
            If cmddetails2 IsNot Nothing Then
                cmddetails2.Parameters.Clear()
                cmddetails2.Dispose()
                cmddetails2 = Nothing
            End If

            If cmddetails3 IsNot Nothing Then
                cmddetails3.Parameters.Clear()
                cmddetails3.Dispose()
                cmddetails3 = Nothing
            End If

            objParamPatientId = Nothing
            objParam = Nothing
            sqlPara = Nothing
        End Try
    End Sub

    Public Sub UpdateData(ByVal ArrList As ArrayList, ByVal arr1 As Array, ByVal ArrPatientOriginalData As ArrayList, Optional ByVal blnPhotoModify As Boolean = False, Optional ByVal IsChange As Boolean = False)

        Dim TrPatient As SqlTransaction = Nothing

        Dim cmddetails As SqlCommand = Nothing
        Dim cmddetails1 As SqlCommand = Nothing
        Dim cmddetails2 As SqlCommand = Nothing
        Dim cmddetails3 As SqlCommand = Nothing

        Dim objParamPatientId As SqlParameter
        Dim sqlPara As SqlParameter
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpPatient", Conn)

            Cmd.CommandType = CommandType.StoredProcedure

            '@nPatientID 			numeric(18,0), 
            '@sExternalPatientId		varchar(10),
            '@sFirstName			varchar(50),
            '@sMiddleName			varchar(50),
            '@sLastName			varchar(50),
            '@sSSN				varchar(20),
            '@dtDOB				datetime,
            '@sGender			varchar(10),
            '@sMaritalStatus			varchar(10),
            '@sstreet			varchar(50),
            '@sCity				varchar(50),
            '@sState				varchar(50),
            '@sZip				varchar(50),
            '@sPhone				varchar(50),
            '@sMobile			varchar(50),




            objParamPatientId = Cmd.Parameters.AddWithValue("@nPatientId", CType(ArrList.Item(0), Long))
            objParamPatientId.Direction = ParameterDirection.InputOutput
            'objParamPatientId.Value = CType(ArrList.Item(0), Long)

            objParam = Cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(1), System.String)

            objParam = Cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), System.String)

            objParam = Cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.String)

            objParam = Cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(4), System.String)

            'sarika SSN Datatype Change
            'objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.BigInt)
            objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.VarChar)
            '-------------------
            objParam.Direction = ParameterDirection.Input

            If Len(Trim(ArrList.Item(5))) <> 9 Then 'Or ArrList.Item(5) = 0 Then
                ''sarika SSN Datatype Change
                'objParam.Value = 0
                objParam.Value = ""
            Else
                'objParam.Value = CType(ArrList.Item(5), System.Int64)
                objParam.Value = CType(ArrList.Item(5), System.String)
            End If
            'objParam.Value = CType(ArrList.Item(5), System.Int64)

            objParam = Cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(6), System.DateTime)

            objParam = Cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(7), System.String)

            objParam = Cmd.Parameters.Add("@sMaritalStatus", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(8), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(9), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(10), System.String)

            objParam = Cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(11), System.String)

            objParam = Cmd.Parameters.Add("@sState", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(12), System.String)

            objParam = Cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(13), System.String)

            objParam = Cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(14), System.String)

            objParam = Cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(15), System.String)

            '       @sEmail				varchar(50),
            '@sFax				varchar(50),
            '@sOccupation			varchar(50),
            '@sEmploymentStatus		varchar(20),
            '@sPlaceofEmployment		varchar(50),
            '@sWorkAddress			varchar(50),
            '@sWorkCity			varchar(50),
            '@sWorkState			varchar(50),
            '@sWorkZip			varchar(50),
            '@sWorkPhone			varchar(50),
            '@sWorkFax			varchar(50),
            '@sChiefComplaints		varchar(50),
            '@dRegistrationDate		datetime,
            '@sPCP				varchar(50),
            '@sGuarantor			varchar(50),


            objParam = Cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(16), System.String)

            objParam = Cmd.Parameters.Add("@sFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(17), System.String)

            objParam = Cmd.Parameters.Add("@sOccupation", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(18), System.String)

            objParam = Cmd.Parameters.Add("@sEmploymentStatus", SqlDbType.VarChar, 20)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(19), System.String)

            objParam = Cmd.Parameters.Add("@sPlaceofEmployment", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(20), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(21), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(22), System.String)

            'If Type = True Then
            objParam = Cmd.Parameters.Add("@sWorkCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(23), System.String)


            objParam = Cmd.Parameters.Add("@sWorkState", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(24), System.String)

            objParam = Cmd.Parameters.Add("@sWorkZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(25), System.String)


            objParam = Cmd.Parameters.Add("@sWorkPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(26), System.String)

            objParam = Cmd.Parameters.Add("@sWorkFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(27), System.String)

            objParam = Cmd.Parameters.Add("@sInsuranceNotes", SqlDbType.VarChar, 1500)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(28), System.String)

            objParam = Cmd.Parameters.Add("@nPCPID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(29), System.Int64)

            objParam = Cmd.Parameters.Add("@sGuarantor", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(30), System.String)

            '   @sPrimaryInsurancetype		varchar(50),
            '@sPrimarySubscriberName	varchar(50),
            '@sPrimarysubscriberID		varchar(50),
            '@sPrimarySubscriberPolicy#	varchar(50),
            '@sSecondarySubscriberID	varchar(50),
            '@sSecondarySubscriberName	varchar(50),
            '@sSecondarySubscriberPolicy#	varchar(50),
            '@sTertiarySubscriberId		varchar(50),
            '@sTertiarySubscriberName	varchar(50),
            '@sTertiarySubscriberPolicy#	varchar(50),
            '@sSpouseName		varchar(50),
            '@sSpousePhone		varchar(50),
            '@sRace			varchar(50),
            '@sPatientStatus			varchar(50),


            objParam = Cmd.Parameters.Add("@sSpouseName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(31), System.String)

            objParam = Cmd.Parameters.Add("@sSpousePhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(32), System.String)

            objParam = Cmd.Parameters.Add("@sRace", SqlDbType.VarChar, 1000)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(33), System.String)

            objParam = Cmd.Parameters.Add("@sPatientStatus", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(34), System.String)

            '   @nProviderId			numeric(18,0)=null,
            '@nReferralID			numeric(18,0)=null,
            '@nPharmacyID			numeric(18,0)=null,
            '@nPrimaryInsuranceId		numeric(18,0)=null

            objParam = Cmd.Parameters.Add("@Check1", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(35), System.Boolean)

            objParam = Cmd.Parameters.Add("@nProviderId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(36), System.Int64)


            'objParam = Cmd.Parameters.Add("@nReferralID", SqlDbType.Int)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CType(ArrList.Item(47), System.Int64)

            objParam = Cmd.Parameters.Add("@nPharmacyID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(37), System.Int64)

            objParam = Cmd.Parameters.Add("@sCounty", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(38), System.String)

            objParam = Cmd.Parameters.Add("@dtRegistrationdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(39), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check2", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(40), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtInjurydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(41), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check3", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(42), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtSurgerydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(43), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check4", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(44), System.Boolean)

            objParam = Cmd.Parameters.Add("@sHandDominance", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(45), System.String)

            objParam = Cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(46), System.String)

            If blnPhotoModify = True Then
                objParam = Cmd.Parameters.Add("@PhotoModified", SqlDbType.Bit)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 1
                If IsNothing(ArrList.Item(47)) = False Then
                    If IsDBNull(ArrList.Item(47)) = False Then
                        'SLR: Why not directly assign the same: 12/22
                        'Dim ms As New MemoryStream
                        'CType(ArrList.Item(47), Image).Save(ms, Imaging.ImageFormat.Bmp)
                        'Dim arrImage() As Byte = ms.ToArray()
                        'ms.Close()
                        'ms.Dispose()
                        'ms = Nothing
                        objParam = Cmd.Parameters.Add("@Photo", SqlDbType.Image)
                        objParam.Direction = ParameterDirection.Input
                        objParam.Value = CType(ArrList.Item(47), Byte()).Clone() 'arrImage
                    End If
                End If
            End If

            ''''Code added by Ravikiran on 27/01/2007

            ''''Mother Details
            objParam = Cmd.Parameters.Add("@sMother_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(48), System.String)

            objParam = Cmd.Parameters.Add("@sMother_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(49), System.String)

            objParam = Cmd.Parameters.Add("@sMother_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(50), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(51), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(52), System.String)

            objParam = Cmd.Parameters.Add("@sMother_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(53), System.String)

            objParam = Cmd.Parameters.Add("@sMother_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(54), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(55), System.String)

            objParam = Cmd.Parameters.Add("@sMother_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(56), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(57), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(58), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(59), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(60), System.String)

            ''''Father Details

            objParam = Cmd.Parameters.Add("@sFather_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(61), System.String)

            objParam = Cmd.Parameters.Add("@sFather_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(62), System.String)

            objParam = Cmd.Parameters.Add("@sFather_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(63), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(64), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(65), System.String)

            objParam = Cmd.Parameters.Add("@sFather_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(66), System.String)

            objParam = Cmd.Parameters.Add("@sFather_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(67), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(68), System.String)

            objParam = Cmd.Parameters.Add("@sFather_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(69), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(70), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(71), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(72), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(73), System.String)

            ''''Guardian Details

            objParam = Cmd.Parameters.Add("@sGuardian_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(74), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(75), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(76), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(77), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(78), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(79), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(80), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(81), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(82), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(83), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(84), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(85), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(86), System.String)

            '// Patient Directive 5 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nPatientDirective", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(87), System.String)
            '// Patient Directive 5 Feb 2007 // - Vinayak

            '// Patient Directive 15 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nExemptFromRpt", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(88), System.String)
            '// Patient Directive 15 Feb 2007 // - Vinayak


            'sarika Workers Comp 7th May 08
            'bIsWorkersComp
            objParam = Cmd.Parameters.Add("@bIsWorkersComp", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            '  objParam.Value = CType(ArrList.Item(89), System.Boolean)
            objParam.Value = ArrList.Item(89)

            'sWorkersCompClaimNo
            objParam = Cmd.Parameters.Add("@sWorkersCompClaimNo", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(90), System.String)


            'sarika Workers Comp 7th May 08

            'sarika Auto 7th May 08
            'bIsAuto
            objParam = Cmd.Parameters.Add("@bIsAuto", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            '  objParam.Value = CType(ArrList.Item(91), System.Boolean)
            objParam.Value = ArrList.Item(91)

            'sAutoClaimNo
            objParam = Cmd.Parameters.Add("@sAutoClaimNo", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(92), System.String)


            'sarika Auto 7th May 08


            '''' Updation Ends
            Conn.Open()
            TrPatient = Conn.BeginTransaction
            Cmd.Transaction = TrPatient
            Cmd.ExecuteNonQuery()

            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            If blnPhotoModify = True Then
                If IsNothing(ArrList.Item(47)) = False Then
                    If IsDBNull(ArrList.Item(47)) = False Then
                        Dim oPatientPhoto As New gloPatient.PatientPhoto()
                        oPatientPhoto.InsertPhoto(CType(ArrList.Item(0), Long), CType(ArrList.Item(47), Byte()).Clone())
                        oPatientPhoto.Dispose()
                        oPatientPhoto = Nothing
                    End If
                End If
            End If

            cmddetails1 = New SqlCommand("gsp_DeletePatient_DTL", Conn)
            cmddetails1.CommandType = CommandType.StoredProcedure
            cmddetails1.Transaction = TrPatient

            objParam = cmddetails1.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
            objParam.Direction = ParameterDirection.Input

            cmddetails1.ExecuteNonQuery()
            Dim i As Integer
            For i = 0 To arr1.Length - 1

                cmddetails = New SqlCommand("gsp_InsertPatient_DTL", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = TrPatient
                objParam = cmddetails.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = CType(ArrList.Item(0), Long)

                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr1(i)
                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next




            cmddetails2 = New SqlCommand("gsp_DeletePatientInsurance_DTL", Conn)
            cmddetails2.CommandType = CommandType.StoredProcedure
            cmddetails2.Transaction = TrPatient

            objParam = cmddetails2.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(0), Long)

            cmddetails2.ExecuteNonQuery()
            cmddetails2.Parameters.Clear()
            For i = 0 To ArrInsuranceCol.Count - 1
                Dim objPatientInsurance As ClsPatientInsurance

                objPatientInsurance = ArrInsuranceCol(i)

                cmddetails3 = New SqlCommand("gsp_InsertPatientInsurance_DTL", Conn)
                cmddetails3.CommandType = CommandType.StoredProcedure

                cmddetails3.Transaction = TrPatient
                objParam = cmddetails3.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(0), Long))
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = CType(ArrList.Item(0), Long)

                objParam = cmddetails3.Parameters.Add("@nInsuranceID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.InsuranceId

                objParam = cmddetails3.Parameters.Add("@sSubscribername", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Subscribername

                objParam = cmddetails3.Parameters.Add("@sSubscriberpolicy", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberPolicy

                objParam = cmddetails3.Parameters.Add("@sSubscriberId", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberId

                objParam = cmddetails3.Parameters.Add("@sGroup", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Group

                objParam = cmddetails3.Parameters.Add("@sEmployer", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Employer

                objParam = cmddetails3.Parameters.Add("@sPhone", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Phone

                objParam = cmddetails3.Parameters.Add("@primary", SqlDbType.Bit)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Primaryflag

                If objPatientInsurance.Checked = True Then

                    objParam = cmddetails3.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = objPatientInsurance.DOB
                End If


                'objParam = cmddetails3.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.DOB

                'objParam = cmddetails3.Parameters.Add("@checked", SqlDbType.Bit)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.Checked

                'objParam = cmddetails3.Parameters.Add("@primary", SqlDbType.Bit)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.Primaryflag


                cmddetails3.ExecuteNonQuery()
                cmddetails3.Parameters.Clear()

                cmddetails3.Dispose()
                cmddetails3 = Nothing
            Next

            If IsChange = True Then
                '''' To Add Patient Information Change History while Updating Patient's Information
                Dim cmdPatHistory As New SqlCommand("gsp_Insert_PatientChangeHistory", Conn, TrPatient)
                '@PatientID         Numeric(18,0),
                '@PatientChangeID   Numeric(18,0), 
                '@dtChangeDateTime  DateTime, 
                '@FirstName         Varchar(50), 
                '@MiddleName        Varchar(50), 
                '@LastName          Varchar(50), 
                '@dtDOB             DateTime, 
                '@Gender            Varchar(10), 
                '@AddressLine1      Varchar(255), 
                '@AddressLine2      Varchar(255), 
                '@City              Varchar(255), 
                '@State             Varchar(50), 
                '@ZIP               Varchar(50), 
                '@County            Varchar(50), 
                '@Phone             Varchar(50)
                cmdPatHistory.CommandType = CommandType.StoredProcedure

                With ArrPatientOriginalData
                    sqlPara = cmdPatHistory.Parameters.AddWithValue("@PatientId", CType(ArrList.Item(0), Long))
                    sqlPara.Direction = ParameterDirection.Input

                    sqlPara = cmdPatHistory.Parameters.Add("@dtChangeDateTime", SqlDbType.DateTime)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = Format(Now, "MM/dd/yyyy hh:mm tt")

                    sqlPara = cmdPatHistory.Parameters.Add("@FirstName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(1).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = ArrPatientOriginalData.Item(2).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@LastName", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(3).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                    sqlPara.Direction = ParameterDirection.Input
                    Dim IsDOB As Int16
                    If IsDate(.Item(4)) = False Then
                        'sqlPara.Value = ""
                        sqlPara.Value = DBNull.Value
                        IsDOB = 0
                    Else
                        sqlPara.Value = CType(ArrPatientOriginalData.Item(4), DateTime)
                        IsDOB = 1
                    End If


                    sqlPara = cmdPatHistory.Parameters.Add("@Gender", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = ArrPatientOriginalData.Item(5).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(7).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(8).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@City", SqlDbType.VarChar, 255)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(9).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@State", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(10).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@ZIP", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(11).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@County", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(12).ToString

                    sqlPara = cmdPatHistory.Parameters.Add("@Phone", SqlDbType.VarChar, 50)
                    sqlPara.Direction = ParameterDirection.Input
                    sqlPara.Value = .Item(13).ToString

                    'sqlPara = cmdPatHistory.Parameters.Add("@IsDate", SqlDbType.Bit)
                    'sqlPara.Direction = ParameterDirection.Input
                    'sqlPara.Value = IsDOB

                    cmdPatHistory.ExecuteNonQuery()
                End With
                ''''''

                If cmdPatHistory IsNot Nothing Then
                    cmdPatHistory.Parameters.Clear()
                    cmdPatHistory.Dispose()
                    cmdPatHistory = Nothing
                End If
            End If

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", CType(ArrList.Item(0), Long), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Modify, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            TrPatient.Commit()
            Conn.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            If IsNothing(TrPatient) = False Then
                TrPatient.Rollback()
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(TrPatient) = False Then
                TrPatient.Dispose()
                TrPatient = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If cmddetails IsNot Nothing Then
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            End If

            If cmddetails1 IsNot Nothing Then
                cmddetails1.Parameters.Clear()
                cmddetails1.Dispose()
                cmddetails1 = Nothing
            End If

            If cmddetails2 IsNot Nothing Then
                cmddetails2.Parameters.Clear()
                cmddetails2.Dispose()
                cmddetails2 = Nothing
            End If

            If cmddetails3 IsNot Nothing Then
                cmddetails3.Parameters.Clear()
                cmddetails3.Dispose()
                cmddetails3 = Nothing
            End If

            objParamPatientId = Nothing
            sqlPara = Nothing
            objParam = Nothing
        End Try
    End Sub

    Public Function AddData(ByVal ArrList As ArrayList, ByVal arr1 As Array, Optional ByVal blnWebCam As Boolean = False) As Int64
        '// Vinayak + Supriya 5 Feb 2007 //
        '// Its convert from sub to function and return patient id
        '//Public Sub AddData(ByVal ArrList As ArrayList, ByVal arr1 As Array, Optional ByVal blnWebCam As Boolean = False)
        '// Vinayak + Supriya 5 Feb 2007 //

        Dim TrPatient As SqlTransaction = Nothing
        Dim _Result As Int64 = 0
        Dim cmddetails As SqlCommand = Nothing
        Dim cmddetails1 As SqlCommand = Nothing

        Dim objParamPatientId As SqlParameter
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpPatient", Conn)

            Cmd.CommandType = CommandType.StoredProcedure

            '@nPatientID 			numeric(18,0), 
            '@sExternalPatientId		varchar(10),
            '@sFirstName			varchar(50),
            '@sMiddleName			varchar(50),
            '@sLastName			varchar(50),
            '@sSSN				varchar(20),
            '@dtDOB				datetime,
            '@sGender			varchar(10),
            '@sMaritalStatus			varchar(10),
            '@sstreet			varchar(50),
            '@sCity				varchar(50),
            '@sState				varchar(50),
            '@sZip				varchar(50),
            '@sPhone				varchar(50),
            '@sMobile			varchar(50),



            ' modified on 20070522 - By Bipin

            objParamPatientId = Cmd.Parameters.AddWithValue("@nPatientId", 0)
            'objParamPatientId = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.InputOutput
            objParamPatientId.Value = CType(ArrList.Item(0), System.Int64)
            'objParamPatientId.Value = 0

            objParam = Cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(1), System.String)

            objParam = Cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), System.String)

            objParam = Cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.String)

            objParam = Cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(4), System.String)

            'sarika SSN Datatype Change
            '  objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.BigInt)
            objParam = Cmd.Parameters.Add("@nSSN", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            If Len(Trim(ArrList.Item(5))) <> 9 Then
                'objParam.Value = 0
                objParam.Value = ""
            Else
                'objParam.Value = CType(ArrList.Item(5), System.Int64)
                objParam.Value = CType(ArrList.Item(5), System.String)
            End If


            objParam = Cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(6), System.DateTime)

            objParam = Cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(7), System.String)

            objParam = Cmd.Parameters.Add("@sMaritalStatus", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(8), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(9), System.String)

            objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(10), System.String)

            objParam = Cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(11), System.String)

            objParam = Cmd.Parameters.Add("@sState", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(12), System.String)

            objParam = Cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(13), System.String)

            objParam = Cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(14), System.String)

            objParam = Cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(15), System.String)

            '       @sEmail				varchar(50),
            '@sFax				varchar(50),
            '@sOccupation			varchar(50),
            '@sEmploymentStatus		varchar(20),
            '@sPlaceofEmployment		varchar(50),
            '@sWorkAddress			varchar(50),
            '@sWorkCity			varchar(50),
            '@sWorkState			varchar(50),
            '@sWorkZip			varchar(50),
            '@sWorkPhone			varchar(50),
            '@sWorkFax			varchar(50),
            '@sChiefComplaints		varchar(50),
            '@dRegistrationDate		datetime,
            '@sPCP				varchar(50),
            '@sGuarantor			varchar(50),


            objParam = Cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(16), System.String)

            objParam = Cmd.Parameters.Add("@sFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(17), System.String)

            objParam = Cmd.Parameters.Add("@sOccupation", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(18), System.String)

            objParam = Cmd.Parameters.Add("@sEmploymentStatus", SqlDbType.VarChar, 20)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(19), System.String)

            objParam = Cmd.Parameters.Add("@sPlaceofEmployment", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(20), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine1", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(21), System.String)

            objParam = Cmd.Parameters.Add("@sWorkAddressLine2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(22), System.String)

            'If Type = True Then
            objParam = Cmd.Parameters.Add("@sWorkCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(23), System.String)


            objParam = Cmd.Parameters.Add("@sWorkState", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(24), System.String)

            objParam = Cmd.Parameters.Add("@sWorkZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(25), System.String)


            objParam = Cmd.Parameters.Add("@sWorkPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(26), System.String)

            objParam = Cmd.Parameters.Add("@sWorkFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(27), System.String)

            objParam = Cmd.Parameters.Add("@sInsuranceNotes", SqlDbType.VarChar, 1500)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(28), System.String)

            objParam = Cmd.Parameters.Add("@nPCPID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(29), System.Int64)

            objParam = Cmd.Parameters.Add("@sGuarantor", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(30), System.String)

            '   @sPrimaryInsurancetype		varchar(50),
            '@sPrimarySubscriberName	varchar(50),
            '@sPrimarysubscriberID		varchar(50),
            '@sPrimarySubscriberPolicy#	varchar(50),
            '@sSecondarySubscriberID	varchar(50),
            '@sSecondarySubscriberName	varchar(50),
            '@sSecondarySubscriberPolicy#	varchar(50),
            '@sTertiarySubscriberId		varchar(50),
            '@sTertiarySubscriberName	varchar(50),
            '@sTertiarySubscriberPolicy#	varchar(50),
            '@sSpouseName		varchar(50),
            '@sSpousePhone		varchar(50),
            '@sRace			varchar(50),
            '@sPatientStatus			varchar(50),


            objParam = Cmd.Parameters.Add("@sSpouseName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(31), System.String)

            objParam = Cmd.Parameters.Add("@sSpousePhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(32), System.String)

            objParam = Cmd.Parameters.Add("@sRace", SqlDbType.VarChar, 1000)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(33), System.String)

            objParam = Cmd.Parameters.Add("@sPatientStatus", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(34), System.String)

            '   @nProviderId			numeric(18,0)=null,
            '@nReferralID			numeric(18,0)=null,
            '@nPharmacyID			numeric(18,0)=null,
            '@nPrimaryInsuranceId		numeric(18,0)=null

            objParam = Cmd.Parameters.Add("@Check1", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(35), System.Boolean)

            objParam = Cmd.Parameters.Add("@nProviderId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(36), System.Int64)


            'objParam = Cmd.Parameters.Add("@nReferralID", SqlDbType.Int)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CType(ArrList.Item(47), System.Int64)

            objParam = Cmd.Parameters.Add("@nPharmacyID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(37), System.Int64)

            objParam = Cmd.Parameters.Add("@sCounty", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(38), System.String)

            objParam = Cmd.Parameters.Add("@dtRegistrationdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(39), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check2", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(40), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtInjurydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(41), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check3", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(42), System.Boolean)

            objParam = Cmd.Parameters.Add("@dtSurgerydate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(43), System.DateTime)

            objParam = Cmd.Parameters.Add("@Check4", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(44), System.Boolean)

            objParam = Cmd.Parameters.Add("@sHandDominance", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(45), System.String)

            '''' -- Location
            objParam = Cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(46), System.String)
            ''''

            ''Dim imgPhoto As Image
            ''imgPhoto = CType(ArrList.Item(39), Image)
            'objParam = Cmd.Parameters.Add("@Photo", SqlDbType.Image)
            'objParam.Direction = ParameterDirection.Input
            'If IsNothing(ArrList.Item(47)) = False Then
            '    If IsDBNull(ArrList.Item(47)) = False Then
            '        'SLR: Why not add directly the array on the same: 12/22
            '        'Dim ms As New MemoryStream
            '        'If blnWebCam = True Then
            '        '    CType(ArrList.Item(47), Image).Save(ms, Imaging.ImageFormat.Bmp)
            '        'Else
            '        '    CType(ArrList.Item(47), Image).Save(ms, Imaging.ImageFormat.Bmp)
            '        'End If
            '        'Dim arrImage() As Byte = ms.ToArray()
            '        'ms.Close()
            '        'ms.Dispose()
            '        'ms = Nothing
            '        objParam.Value = CType(ArrList.Item(47), Byte()).Clone() 'arrImage
            '    End If
            'End If

            objParam = Cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID(CDate(ArrList.Item(6))))
            objParam.Direction = ParameterDirection.Input

            ''''Code added by Ravikiran on 27/01/2007

            ''''Mother Details
            objParam = Cmd.Parameters.Add("@sMother_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(48), System.String)

            objParam = Cmd.Parameters.Add("@sMother_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(49), System.String)

            objParam = Cmd.Parameters.Add("@sMother_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(50), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(51), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(52), System.String)

            objParam = Cmd.Parameters.Add("@sMother_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(53), System.String)

            objParam = Cmd.Parameters.Add("@sMother_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(54), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(55), System.String)

            objParam = Cmd.Parameters.Add("@sMother_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(56), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(57), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(58), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(59), System.String)

            objParam = Cmd.Parameters.Add("@sMother_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(60), System.String)

            ''''Father Details

            objParam = Cmd.Parameters.Add("@sFather_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(61), System.String)

            objParam = Cmd.Parameters.Add("@sFather_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(62), System.String)

            objParam = Cmd.Parameters.Add("@sFather_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(63), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(64), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(65), System.String)

            objParam = Cmd.Parameters.Add("@sFather_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(66), System.String)

            objParam = Cmd.Parameters.Add("@sFather_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(67), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(68), System.String)

            objParam = Cmd.Parameters.Add("@sFather_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(69), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(70), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(71), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(72), System.String)

            objParam = Cmd.Parameters.Add("@sFather_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(73), System.String)

            ''''Guardian Details

            objParam = Cmd.Parameters.Add("@sGuardian_fName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(74), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_mName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(75), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_lName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(76), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address1", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(77), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Address2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(78), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_City", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(79), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_State", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(80), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Zip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(81), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_County", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(82), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Phone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(83), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Mobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(84), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Fax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(85), System.String)

            objParam = Cmd.Parameters.Add("@sGuardian_Email", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(86), System.String)

            '// Patient Directive 5 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nPatientDirective", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(87), System.String)
            '// Patient Directive 5 Feb 2007 // - Vinayak

            '// Patient Exempt From Report 15 Feb 2007 //
            objParam = Cmd.Parameters.Add("@nExemptFromRpt", SqlDbType.Int, 4)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(88), System.String)
            '// Patient Directive 15 Feb 2007 // - Vinayak




            'sarika Workers Comp 7th May 08
            'bIsWorkersComp
            objParam = Cmd.Parameters.Add("@bIsWorkersComp", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            '  objParam.Value = CType(ArrList.Item(88), System.Boolean)
            objParam.Value = ArrList.Item(89)

            'sWorkersCompClaimNo
            objParam = Cmd.Parameters.Add("@sWorkersCompClaimNo", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(90), System.String)


            'sarika Workers Comp 7th May 08


            'sarika Auto 7th May 08
            'bIsAuto
            objParam = Cmd.Parameters.Add("@bIsAuto", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            '  objParam.Value = CType(ArrList.Item(91), System.Boolean)
            objParam.Value = ArrList.Item(91)

            'sAutoClaimNo
            objParam = Cmd.Parameters.Add("@sAutoClaimNo", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(92), System.String)


            'sarika Auto 7th May 08


            '''' Updation Ends

            'objParam = Cmd.Parameters.Add("@nPrimaryInsuranceId", SqlDbType.Int)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = CType(ArrList.Item(48), System.Int64)
            Conn.Open()
            TrPatient = Conn.BeginTransaction
            Cmd.Transaction = TrPatient
            Cmd.ExecuteNonQuery()
            Dim myObject As Object = objParamPatientId.Value
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing


            If IsNothing(ArrList.Item(47)) = False Then
                If IsDBNull(ArrList.Item(47)) = False Then
                    Dim oPatientPhoto As New gloPatient.PatientPhoto()
                    oPatientPhoto.InsertPhoto(CType(myObject, Long), CType(ArrList.Item(47), Byte()).Clone())
                    oPatientPhoto.Dispose()
                    oPatientPhoto = Nothing
                End If
            End If


            Dim i As Integer
            For i = 0 To arr1.Length - 1
                cmddetails1 = New SqlCommand("gsp_InsertPatient_DTL", Conn)
                cmddetails1.CommandType = CommandType.StoredProcedure

                cmddetails1.Transaction = TrPatient
                objParam = cmddetails1.Parameters.AddWithValue("@nPatientID", myObject)
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = objParamPatientId.Value

                objParam = cmddetails1.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr1(i)
                cmddetails1.ExecuteNonQuery()
                cmddetails1.Parameters.Clear()

                cmddetails1.Dispose()
                cmddetails1 = Nothing
            Next

            For i = 0 To ArrInsuranceCol.Count - 1
                Dim objPatientInsurance As ClsPatientInsurance
                objPatientInsurance = ArrInsuranceCol(i)
                cmddetails = New SqlCommand("gsp_InsertPatientInsurance_DTL", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = TrPatient
                objParam = cmddetails.Parameters.AddWithValue("@nPatientID", myObject)
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = objParamPatientId.Value

                objParam = cmddetails.Parameters.Add("@nInsuranceID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.InsuranceId

                objParam = cmddetails.Parameters.Add("@sSubscribername", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Subscribername

                objParam = cmddetails.Parameters.Add("@sSubscriberpolicy", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberPolicy

                objParam = cmddetails.Parameters.Add("@sSubscriberId", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.SubscriberId

                objParam = cmddetails.Parameters.Add("@sGroup", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Group

                objParam = cmddetails.Parameters.Add("@sEmployer", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Employer

                objParam = cmddetails.Parameters.Add("@sPhone", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Phone

                objParam = cmddetails.Parameters.Add("@primary", SqlDbType.Bit)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objPatientInsurance.Primaryflag

                If objPatientInsurance.Checked = True Then

                    objParam = cmddetails.Parameters.Add("@dtDOB", SqlDbType.DateTime)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = objPatientInsurance.DOB
                End If

                'objParam = cmddetails.Parameters.Add("@checked", SqlDbType.Bit)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = objPatientInsurance.Checked

                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()

                cmddetails.Dispose()
                cmddetails = Nothing
            Next

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.RegisterEMRPatient, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Added", CType(ArrList.Item(0), Long), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, CType(ArrList.Item(2), System.String) & " " & CType(ArrList.Item(3), System.String) & " " & CType(ArrList.Item(4), System.String) & " Patient Added", gstrLoginName, gstrClientMachineName, objParamPatientId.Value)

            'objAudit = Nothing
            TrPatient.Commit()
            Conn.Close()

            If Not myObject Is Nothing Then
                _Result = myObject
            End If

            Return _Result

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(TrPatient) = False Then
                TrPatient.Rollback()
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return 0
        Finally
            If IsNothing(TrPatient) = False Then
                TrPatient.Dispose()
                TrPatient = Nothing
            End If
            If cmddetails IsNot Nothing Then
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            End If
            If cmddetails1 IsNot Nothing Then
                cmddetails1.Parameters.Clear()
                cmddetails1.Dispose()
                cmddetails1 = Nothing
            End If

            objParamPatientId = Nothing
            objParam = Nothing
        End Try
    End Function
    'not in use
    'Public Sub DeleteData(ByVal PatientID As Long, Optional ByVal PatientCode As String = "", Optional ByVal PatientName As String = "")
    '    Try
    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeletePatient", Conn)
    '        Cmd.CommandType = CommandType.StoredProcedure
    '        Dim objParam As SqlParameter
    '        objParam = Cmd.Parameters.Add("@nPatientId", PatientID)
    '        objParam.Direction = ParameterDirection.Input
    '        'objParam.Value = PatientID
    '        Conn.Open()
    '        Cmd.ExecuteNonQuery()

    '        'Dim objAudit As New clsAudit
    '        If PatientCode = "" Then
    '            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", gloAuditTrail.ActivityOutCome.Success)
    '            ''Added Rahul P on 20101009
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ''
    '            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        Else
    '            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, PatientCode & " - " & PatientName & " " & " Patient Deleted", gloAuditTrail.ActivityOutCome.Success)
    '            ''Added Rahul P on 20101009
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, PatientCode & " - " & PatientName & " " & " Patient Deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ''
    '            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, PatientCode & " - " & PatientName & " " & " Patient Deleted", gstrLoginName, gstrClientMachineName, PatientID)
    '        End If

    '        'objAudit = Nothing

    '        Conn.Close()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '    End Try
    'End Sub

    Public Function RetrievePatientDetails(ByVal PatientID As Long) As ArrayList
        Dim arrlist As New ArrayList
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanPatientDetails", Conn)
            Cmd.CommandType = CommandType.StoredProcedure            

            objParam = Cmd.Parameters.AddWithValue("@nPatientId", PatientID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = PatientID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader(CommandBehavior.SingleRow)
            If dreader.HasRows = True Then
                dreader.Read()
                arrlist.Add(dreader.Item("Occupation"))
                arrlist.Add(dreader.Item("PCP"))
            End If
            dreader.Close()
            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return arrlist
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objParam = Nothing
        End Try

    End Function

    Public Function FetchDataForUpdate(ByVal PatientID As Long) As ArrayList
        Dim objParam As SqlParameter
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanPatient", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.AddWithValue("@nPatientId", PatientID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = PatientID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            If dreader.HasRows = True Then
                dreader.Read()
                Dim i As Int16

                'sarika Workers Comp 7th May 08
                '  For i = 0 To 86
                'sarika Auto 7th May 08
                'For i = 0 To 88
                For i = 0 To 90
                    '-------sarika Workers Comp 11th May 08
                    arrlist.Add(dreader.Item(i))
                Next

                dreader.Close()
            End If
            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return arrlist
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objParam = Nothing
        End Try
    End Function
    Public Function FetchReferrals(ByVal PatientID As Long) As ArrayList  'for update
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanPatient_DTL", Conn)
            Cmd.CommandType = CommandType.StoredProcedure            
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.AddWithValue("@nPatientId", PatientID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = PatientID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(New myList(CType(dreader.Item(0), Long), CType(dreader.Item(1), System.String)))
            Loop
            dreader.Close()
            Conn.Close()
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return arrlist
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return Ds
    '        'Return Ds
    '    End Get
    'End Property
    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get

    End Property
    'Public Sub SortDataview(ByVal strsort As String)
    '    Dv.Sort = "[" & strsort & "]"
    'End Sub


    Public Function FillControls(ByVal FillType As String) As DataTable

        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable

        Try

            Dim Cmd As SqlCommand = Nothing
            If FillType = "S" Then      'Specialty
                Cmd = New SqlCommand("gsp_FillSpecialty_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
            ElseIf FillType = "R" Then 'Provider
                Cmd = New SqlCommand("gsp_FillProvider_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
            ElseIf FillType = "C" Then 'Race
                Cmd = New SqlCommand("gsp_FillCategory_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = "Race"
                objParam = Nothing

            ElseIf FillType = "T" Then 'City,County Details
                Cmd = New SqlCommand("gsp_FillCSZ_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure

            Else
                Cmd = New SqlCommand("gsp_FillContacts_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@Type", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = FillType

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 1
                objParam = Nothing
            End If

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                Dv = dt.DefaultView
            End If

            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return dt
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If

        End Try
    End Function

    Public Function FillLocation() As DataTable
        Try
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable

            Dim Cmd As SqlCommand = New SqlCommand("gsp_FillCategory_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "Location"

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                Dv = dt.DefaultView
            End If

            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            objParam = Nothing
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If

            Return dt
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function FetchAddressInfo(ByVal zip As Long) As DataTable 'against a Zipcode
        Try
            Dim adpt As New SqlDataAdapter
            'Dim arrlist As New ArrayList
            Dim dt As New DataTable
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanCSZ_MST", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nZip", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = zip
            adpt.SelectCommand = Cmd
            adpt.Fill(dt)

            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function FetchContactsDetail(ByVal id As Long, ByVal stype As Char) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanContacts_Detail", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = Cmd.Parameters.Add("@sType", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = stype

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)


            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()
            'Dim arr() As Integer
            'Dim i As Integer
            'Do While dreader.Read()
            '    arr(i) = dreader.Item(0)
            '    i = i + 1
            'Loop
            Conn.Close()
            objParam = Nothing

            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = Dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        'strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '%" & txtSearch & "%'"
        strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Private Function Splittext(ByVal strsplittext As String) As String

        Dim arrstring() As String
        Try
            If Trim(strsplittext) <> "" Then

                arrstring = Split(LTrim(strsplittext), " ")
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return strsplittext
        End Try
    End Function
    Public Function PopulateArraylist(ByVal objPatientInsurance As ClsPatientInsurance)
        If IsNothing(ArrInsuranceCol) Then
            ArrInsuranceCol = New ArrayList
        End If
        ArrInsuranceCol.Add(objPatientInsurance)
        Return Nothing
    End Function
    Public Function GetInsuranceCol(ByRef PatientInsurance As ClsPatientInsurance, ByVal id As Int16)
        Try

            If ArrInsuranceCol.Count > id Then
                'PatientInsurance.InsuranceId = objPatientInsurance.InsuranceId
                'PatientInsurance.InsuranceName = objPatientInsurance.InsuranceName
                'PatientInsurance.Subscribername = objPatientInsurance.Subscribername
                'PatientInsurance.SubscriberPolicy = objPatientInsurance.SubscriberPolicy
                'PatientInsurance.SubscriberId = objPatientInsurance.SubscriberId
                'PatientInsurance.Group = objPatientInsurance.Group
                'PatientInsurance.DOB = objPatientInsurance.DOB
                'PatientInsurance.Employer = objPatientInsurance.Employer
                'PatientInsurance.Phone = objPatientInsurance.Phone
                Dim objPatientInsurance As ClsPatientInsurance
                objPatientInsurance = ArrInsuranceCol.Item(id)
                PatientInsurance.InsuranceId = objPatientInsurance.InsuranceId
                PatientInsurance.InsuranceName = objPatientInsurance.InsuranceName
                PatientInsurance.Subscribername = objPatientInsurance.Subscribername
                PatientInsurance.SubscriberPolicy = objPatientInsurance.SubscriberPolicy
                PatientInsurance.SubscriberId = objPatientInsurance.SubscriberId
                PatientInsurance.Group = objPatientInsurance.Group
                PatientInsurance.Employer = objPatientInsurance.Employer
                PatientInsurance.Phone = objPatientInsurance.Phone

                If IsDBNull(objPatientInsurance.DOB) Then
                    'objPatientInsurance.Checked = False
                Else
                    'objPatientInsurance.Checked = True
                    PatientInsurance.DOB = objPatientInsurance.DOB

                End If
                PatientInsurance.Checked = objPatientInsurance.Checked
                PatientInsurance.Primaryflag = objPatientInsurance.Primaryflag
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    'code commented by sarika 6th nov 07
    'Public Function FetchPatientInsurance(ByVal PatientID As Long) As Int16 'fetch data against a patient
    '    Try
    '        Dim dreader As SqlDataReader

    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanPatientInsurance_DTL", Conn)
    '        Cmd.CommandType = CommandType.StoredProcedure
    '        Dim objParam As SqlParameter

    '        objParam = Cmd.Parameters.Add("@nPatientID", PatientID)
    '        objParam.Direction = ParameterDirection.Input
    '        'objParam.Value = PatientID

    '        Conn.Open()
    '        dreader = Cmd.ExecuteReader
    '        Do While dreader.Read()
    '            '   select p.nInsuranceID,p.sSubscribername,p.sSubscriberPolicy#,
    '            'p.sSubscriberID,p.sGroup,p.sEmployer,p.sPhone,p.dtDOB,c.sname
    '            Dim objPatientInsurance As New ClsPatientInsurance
    '            objPatientInsurance.InsuranceId = CType(dreader.Item(0), System.Int64)
    '            objPatientInsurance.Subscribername = CType(dreader.Item(1), System.String)
    '            objPatientInsurance.SubscriberPolicy = CType(dreader.Item(2), System.String)
    '            objPatientInsurance.SubscriberId = CType(dreader.Item(3), System.String)
    '            objPatientInsurance.Group = CType(dreader.Item(4), System.String)
    '            objPatientInsurance.Employer = CType(dreader.Item(5), System.String)
    '            objPatientInsurance.Phone = CType(dreader.Item(6), System.String)
    '            If IsDBNull(dreader.Item(7)) Then
    '                objPatientInsurance.Checked = False
    '            Else
    '                objPatientInsurance.Checked = True
    '                objPatientInsurance.DOB = CType(dreader.Item(7), System.DateTime)
    '            End If
    '            objPatientInsurance.InsuranceName = CType(dreader.Item(8), System.String)
    '            objPatientInsurance.Primaryflag = CType(dreader.Item(9), System.Boolean)
    '            PopulateArraylist(objPatientInsurance)
    '        Loop
    '        Conn.Close()


    '        ''sarika 2nd nov 07
    '        ArrInsuranceColOld = New ArrayList
    '        For i As Integer = 0 To ArrInsuranceCol.Count - 1
    '            Dim objPatientins As New ClsPatientInsurance
    '            objPatientins = CType(ArrInsuranceCol.Item(i), ClsPatientInsurance)
    '            ArrInsuranceColOld.Add(objPatientins)
    '            objPatientins = Nothing
    '        Next
    '        ''------




    '        Return ArrInsuranceCol.Count
    '    Catch ex As Exception
    '        Conn.Close()
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function

    ' 'code added by sarika 6th nov 07
    Public Function FetchPatientInsurance(ByVal PatientID As Long) As Int16 'fetch data against a patient
        Dim objParam As SqlParameter
        Try
            Dim dreader As SqlDataReader

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanPatientInsurance_DTL", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = PatientID

            Conn.Open()
            dreader = Cmd.ExecuteReader
            Dim objPatientInsurance As ClsPatientInsurance
            Do While dreader.Read()
                '   select p.nInsuranceID,p.sSubscribername,p.sSubscriberPolicy#,
                'p.sSubscriberID,p.sGroup,p.sEmployer,p.sPhone,p.dtDOB,c.sname
                objPatientInsurance = New ClsPatientInsurance
                objPatientInsurance.InsuranceId = CType(dreader.Item(0), System.Int64)
                objPatientInsurance.Subscribername = CType(dreader.Item(1), System.String)
                objPatientInsurance.SubscriberPolicy = CType(dreader.Item(2), System.String)
                objPatientInsurance.SubscriberId = CType(dreader.Item(3), System.String)
                objPatientInsurance.Group = CType(dreader.Item(4), System.String)
                objPatientInsurance.Employer = CType(dreader.Item(5), System.String)
                objPatientInsurance.Phone = CType(dreader.Item(6), System.String)
                If IsDBNull(dreader.Item(7)) Then
                    objPatientInsurance.Checked = False
                Else
                    objPatientInsurance.Checked = True
                    objPatientInsurance.DOB = CType(dreader.Item(7), System.DateTime)
                End If
                objPatientInsurance.InsuranceName = CType(dreader.Item(8), System.String)
                objPatientInsurance.Primaryflag = CType(dreader.Item(9), System.Boolean)
                PopulateArraylist(objPatientInsurance)
                objPatientInsurance = Nothing
            Loop
            dreader.Close()
            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            ''sarika 2nd nov 07
            ArrInsuranceColOld = New ArrayList

            Dim objPatientInsuranceOld As ClsPatientInsurance
            For i As Integer = 0 To ArrInsuranceCol.Count - 1

                objPatientInsuranceOld = New ClsPatientInsurance
                objPatientInsurance = CType(ArrInsuranceCol.Item(i), ClsPatientInsurance)
                objPatientInsuranceOld.InsuranceId = objPatientInsurance.InsuranceId
                objPatientInsuranceOld.InsuranceName = objPatientInsurance.InsuranceName
                objPatientInsuranceOld.Group = objPatientInsurance.Group
                objPatientInsuranceOld.Phone = objPatientInsurance.Phone
                objPatientInsuranceOld.Employer = objPatientInsurance.Employer
                objPatientInsuranceOld.DOB = objPatientInsurance.DOB
                objPatientInsuranceOld.Primaryflag = objPatientInsurance.Primaryflag
                objPatientInsuranceOld.SubscriberId = objPatientInsurance.SubscriberId
                objPatientInsuranceOld.Subscribername = objPatientInsurance.Subscribername
                objPatientInsuranceOld.SubscriberPolicy = objPatientInsurance.SubscriberPolicy
                objPatientInsuranceOld.Checked = objPatientInsurance.Checked
                ArrInsuranceColOld.Add(objPatientInsuranceOld)
                objPatientInsuranceOld = Nothing
                objPatientInsurance = Nothing
            Next
            ''------

            Return ArrInsuranceCol.Count
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objParam = Nothing
        End Try
    End Function

    '----------------------------

    Public Function SetInsuranceCol(ByRef PatientInsurance As ClsPatientInsurance, ByVal id As Int64, Optional ByVal oldid As Int64 = 0) As Boolean
        Try
            Dim objPatientInsurance As ClsPatientInsurance
            If oldid <> -1 Then
                If ArrInsuranceCol.Count > id Then
                    objPatientInsurance = ArrInsuranceCol.Item(id)


                    objPatientInsurance.InsuranceId = PatientInsurance.InsuranceId
                    objPatientInsurance.InsuranceName = PatientInsurance.InsuranceName
                    objPatientInsurance.PatientId = PatientInsurance.PatientId
                    objPatientInsurance.Phone = PatientInsurance.Phone
                    objPatientInsurance.SubscriberId = PatientInsurance.SubscriberId
                    objPatientInsurance.Subscribername = PatientInsurance.Subscribername
                    objPatientInsurance.SubscriberPolicy = PatientInsurance.SubscriberPolicy
                    objPatientInsurance.Group = PatientInsurance.Group
                    objPatientInsurance.Employer = PatientInsurance.Employer
                    objPatientInsurance.Primaryflag = PatientInsurance.Primaryflag
                    objPatientInsurance.DOB = PatientInsurance.DOB
                    objPatientInsurance.Checked = PatientInsurance.Checked

                    ArrInsuranceCol.Item(id) = objPatientInsurance
                End If
            Else
                'add
                PopulateArraylist(PatientInsurance)
            End If
            If PatientInsurance.Primaryflag = True Then
                Dim objinsurance As ClsPatientInsurance
                For Each objinsurance In ArrInsuranceCol
                    If objinsurance.InsuranceId <> PatientInsurance.InsuranceId Then
                        objinsurance.Primaryflag = False
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Public Function SetasPrimary(ByVal id As Int64, ByVal blnChecked As Boolean, Optional ByVal oldid As Int64 = 0)
        If oldid <> -1 Then
            If ArrInsuranceCol.Count > id Then
                CType(ArrInsuranceCol.Item(id), ClsPatientInsurance).Primaryflag = blnChecked
                If blnChecked = True Then
                    Dim objinsurance As ClsPatientInsurance
                    For Each objinsurance In ArrInsuranceCol
                        If objinsurance.InsuranceId <> CType(ArrInsuranceCol.Item(id), ClsPatientInsurance).InsuranceId Then
                            objinsurance.Primaryflag = False
                        End If
                    Next
                End If
            End If
        End If
        Return Nothing
    End Function
    Public Function DeleteInsurance(ByVal id As Int16)
        If ArrInsuranceCol.Count >= id Then
            ArrInsuranceCol.RemoveAt(id)
        End If
        Return Nothing
    End Function
    Public Function ValidateDescription(ByVal PatientID As Long, ByVal str1 As String) As Boolean
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_checkPatient", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1
            objParam = Cmd.Parameters.AddWithValue("@nPatientid", PatientID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = PatientID

            Dim dreader As SqlDataReader
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = Cmd.ExecuteReader
            Dim i As Long
            Do While dreader.Read
                i = CType(dreader.Item(0), Long)
                If i > 0 Then
                    Conn.Close()
                    dreader.Close()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing

                    Return False
                Else
                    Conn.Close()
                    dreader.Close()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing

                    Return True
                End If
            Loop
            dreader.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            objParam = Nothing
        End Try
    End Function

    '' -- To Update/Change the Location of the Patient (from Main Menu)
    Public Function ChangeLocation(ByVal PatientID As Long, ByVal Location As String) As Boolean
        Try
            Dim Cmd As SqlCommand = New SqlCommand("gsp_UpdatePatientLocation", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = Cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Location
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            objParam = Nothing
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try

    End Function


#Region " Locking of Patient Records"

    'Public Function PatientStatus(ByVal PatientID As Long) As String
    '    Dim cmd As SqlCommand

    '    Try
    '        cmd = New SqlCommand("gsp_GetPatientStatus", Conn)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlpara As SqlParameter


    '        sqlpara = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
    '        sqlpara.Value = PatientID


    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If

    '        Dim strStatus As String = ""
    '        strStatus = cmd.ExecuteScalar()

    '        Return strStatus

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return ""
    '    Finally
    '        cmd = Nothing
    '        Conn = Nothing
    '    End Try

    'End Function

    '''' 20090921
    Public Function PatientStatus(ByVal PatientID As Long, Optional ByVal PatientCode As String = "") As String

        Dim cmd As SqlCommand = Nothing
        Dim sqlpara As SqlParameter
        Dim strStatus As String = ""


        Try
            cmd = New SqlCommand("gsp_GetPatientStatus", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlpara = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlpara.Value = PatientID

            sqlpara = cmd.Parameters.Add("@PatientCode", SqlDbType.VarChar)
            sqlpara.Value = PatientCode


            'Aniket: If condition added to avoid database trips when both patient code and ID are blank
            If PatientID <> 0 Or PatientCode <> "" Then

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                strStatus = cmd.ExecuteScalar()
            End If
            If IsNothing(strStatus) Then
                strStatus = ""
            End If
            Return strStatus

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        Finally
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            If Not IsNothing(Conn) Then  ''openconnection close
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If

            sqlpara = Nothing
            Conn = Nothing
        End Try

    End Function
#End Region
    Public Function viewPatientsHistory(ByVal PatientID As Long) As DataView
        Try
            Dim da As SqlDataAdapter
            'Dim ds As New DataSet
            Dim dt As DataTable
            Dim dv As DataView = Nothing

            Dim Cmd As SqlCommand = New SqlCommand("gsp_viewPatientHistory", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            Conn.Open()
            'cmd.ExecuteNonQuery()
            'ds.Clear()
            da = New SqlDataAdapter
            da.SelectCommand = Cmd
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            If (IsNothing(dt) = False) Then
                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
            End If
           
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            
            Return dv

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try

    End Function


    Public Function Fill_LockPatientRegistration(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            Conn.Close()
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    'sarika 3rd oct 07
    Public Function GetInsurancePhone(ByVal InsID As Int64) As String

        Dim insPhone As String = ""
        Dim conn As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""

        Try
            conn.Open()

            _strSQL = "select isnull(sPhone,'') as sPhone from Contacts_mst where nContactId = " & InsID
            cmd = New SqlCommand(_strSQL, conn)
            insPhone = cmd.ExecuteScalar()

            Return insPhone

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function
    '-------------------------------------


    'sarika 2nd nov 07
    Public Function chkInsChanged() As Boolean

        Dim blnInsChanged As Boolean = False
        Try
            If ArrInsuranceCol.Count <> ArrInsuranceColOld.Count Then
                blnInsChanged = True
                Return blnInsChanged
            End If


            For i As Integer = 0 To ArrInsuranceCol.Count - 1
                Dim objPatientInsurance As ClsPatientInsurance


                objPatientInsurance = CType(ArrInsuranceCol.Item(i), ClsPatientInsurance)


                'compare the two insurance object
                For j As Integer = 0 To ArrInsuranceColOld.Count - 1
                    Dim objPatientInsuranceOld As ClsPatientInsurance
                    objPatientInsuranceOld = CType(ArrInsuranceColOld.Item(j), ClsPatientInsurance)
                    If objPatientInsurance.InsuranceId = objPatientInsuranceOld.InsuranceId Then
                        If objPatientInsurance.InsuranceName <> objPatientInsuranceOld.InsuranceName Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Group <> objPatientInsuranceOld.Group Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Phone <> objPatientInsuranceOld.Phone Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Employer <> objPatientInsuranceOld.Employer Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.DOB <> objPatientInsuranceOld.DOB Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Primaryflag <> objPatientInsuranceOld.Primaryflag Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.SubscriberId <> objPatientInsuranceOld.SubscriberId Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Subscribername <> objPatientInsuranceOld.Subscribername Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.SubscriberPolicy <> objPatientInsuranceOld.SubscriberPolicy Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                        If objPatientInsurance.Checked <> objPatientInsuranceOld.Checked Then
                            blnInsChanged = True
                            Return blnInsChanged
                        End If
                    End If
                Next
            Next

            Return blnInsChanged
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    ' //'sarika 23rd july 08
    '//for Patient Reg Advance Directive

    Public Sub UpdateAdvDirective(ByVal nPatientID As Long)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand()
            Cmd.Connection = Conn

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "Update Patient set nPatientDirective = 0 where nPatientID = " & nPatientID

            Conn.Open()

            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try
    End Sub
#Region "to check for the duplicate values"
    'shubhangi 20090826
    'Add funtion for Zip code
    'Check the zip code and related details into the database
    ''' <summary>
    ''' Dhruv 20100121
    ''' Checking for the duplicates values present or not 
    ''' </summary>
    ''' <param name="City"></param>
    ''' <param name="State"></param>
    ''' <param name="ZIPCode"></param>
    ''' <param name="County"></param>
    ''' <param name="AreaCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsReocrdPresent(ByVal City As String, ByVal State As String, ByVal ZIPCode As String, ByVal County As String, ByVal AreaCode As Int64) As Boolean
        'Public Function IsReocrdPresent(ByVal City As String, ByVal State As String, ByVal ZIPCode As String, ByVal County As String, ByVal AreaCode As String) As Boolean

        Dim conn As New System.Data.SqlClient.SqlConnection(GetConnectionString())
        Dim oCmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim _result As Object

        Try

            conn.Open()
            ' _strSQL = "SELECT COUNT(nID) FROM CSZ_MST WHERE City = '" & City & "' AND ST = '" & State & "' AND Zip = '" & ZIPCode & "' AND county ='" & County & "' AND AreaCode = " & AreaCode & " "
            _strSQL = "SELECT COUNT(nID) FROM CSZ_MST WHERE City = '" & Replace(City, "'", "''") & "' AND ST = '" & Replace(State, "'", "''") & "' AND Zip = '" & Replace(ZIPCode, "'", "''") & "' AND county ='" & Replace(County, "'", "''") & "' AND AreaCode ='" & Replace(AreaCode, "'", "''") & "'"

            ''AND Areacode = '" & AreaCode & "' 
            oCmd = New System.Data.SqlClient.SqlCommand()
            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _strSQL

            _result = oCmd.ExecuteScalar()
            If Not IsNothing(_result) Then
                If Convert.ToInt16(_result) = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()

                End If
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Function
#End Region

    'Fetch the Zip details form the database
    Public Function FetchZip() As DataView
        Dim conn As New SqlConnection(GetConnectionString())
        Dim oCmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        '     Dim _result As Object
      

        Try
            _strSQL = "Select top(100) nID,ISNULL(Zip,0)As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST , ISNULL(county,'') AS county , ISNULL(AreaCode,0) AS AreaCode from CSZ_MST Order By ZIP"
            '_strSQL = "Select  nID,ISNULL(Zip,0)As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST , ISNULL(county,'') AS county , ISNULL(AreaCode,0) AS AreaCode from CSZ_MST Order By ZIP"
            oCmd = New SqlCommand()

            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _strSQL
            conn.Open()

            Dim ad As SqlDataAdapter
            ad = New SqlDataAdapter(oCmd)
            Dim dt As New DataTable
            ad.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                Dv = New DataView(dt.Copy)
                dt.Dispose()
                dt = Nothing
            End If
            ad.Dispose()
            ad = Nothing
            Return Dv


        Catch ex As Exception
            FetchZip = Nothing
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()

                End If
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Function

    Public Function GetZIPCodes(ByVal txtSearch As String) As DataView
        Dim conn As New SqlConnection(GetConnectionString())
        Dim oCmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        'Dim _result As Object

        Try
            '_strSQL = "Select top(100) nID,ISNULL(Zip,0)As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST , ISNULL(county,'') AS county , ISNULL(AreaCode,0) AS AreaCode from CSZ_MST Order By ZIP"
            ' _strSQL = "Select  nID,ISNULL(Zip,0)As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST , ISNULL(county,'') AS county , ISNULL(AreaCode,0) AS AreaCode from CSZ_MST Order By ZIP"
            _strSQL = "Select  top(100) nID,ISNULL(Zip,0)As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST , ISNULL(county,'') AS county , ISNULL(AreaCode,0) AS AreaCode from CSZ_MST " _
                     & "WHERE Zip LIKE '%" & txtSearch & "%' or City LIKE '%" & txtSearch & "%' or ST LIKE '%" & txtSearch & "%' or county LIKE '%" & txtSearch & "%' or AreaCode LIKE '%" & txtSearch & "%'"

            oCmd = New SqlCommand()

            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _strSQL
            conn.Open()
            Dim ad As SqlDataAdapter
            ad = New SqlDataAdapter(oCmd)
            Dim dt As New DataTable
            ad.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                Dv = New DataView(dt.Copy)
                dt.Dispose()
                dt = Nothing
            End If
            ad.Dispose()
            ad = Nothing
            Return Dv



        Catch ex As Exception
            GetZIPCodes = Nothing
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()

                End If
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Function


    'Add new zip entry in database & use function to return nID which is used inSave function of MSTZip form
    Public Function AddCity(ByVal sCity As String, ByVal sState As String, ByVal sZip As String, ByVal sAreaCode As Int64, ByVal sCounty As String) As Int64
        Dim conn As New SqlConnection(GetConnectionString())
        Dim oCmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim _result As Object

        Try
            _strSQL = "SELECT MAX(ISNULL(nID,0)) + 1 From csz_mst "

            oCmd = New SqlCommand()

            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _strSQL
            conn.Open()
            _result = oCmd.ExecuteScalar()


            _strSQL = ""
            _strSQL = "Insert into csz_mst (City,ST,Zip,Areacode,county,nID) values  ('" & Replace(sCity, "'", "''") & "','" & Replace(sState, "'", "''") & "','" & Replace(sZip, "'", "''") & "','" & Replace(sAreaCode, "'", "''") & "','" & Replace(sCounty, "'", "''") & "'," & Convert.ToInt64(_result) & ")" ' where zip = '" & sZip & "'"
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
            oCmd = New SqlCommand()
            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _strSQL

            oCmd.ExecuteNonQuery()
            If IsNothing(_result) = False Then
                Return _result
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()

                End If
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Function
#Region "To update the zip's any field"
    'Update zip entry into the database

    'Public Sub UpdateCity(ByVal sCity As String, ByVal sZip As String, ByVal ID As Int64, ByVal mCounty as String)
    ''' <summary>
    ''' Dhruv 20100121 
    ''' 
    ''' </summary>
    ''' <param name="sCity"></param>
    ''' <param name="sZip"></param>
    ''' <param name="nID"></param>
    ''' <param name="mCounty"></param>
    ''' <param name="sST"></param>
    ''' <param name="sAreaCode"></param>
    ''' <remarks></remarks>
    Public Sub UpdateCity(ByVal sCity As String, ByVal sZip As String, ByVal nID As Int64, ByVal mCounty As String, ByVal sST As String, ByVal sAreaCode As String)

        Dim conn As New SqlConnection(GetConnectionString())
        Dim oCmd As SqlCommand = Nothing
        Dim _strSQL As String = ""

        Try

            '_strSQL = "update csz_mst set city = '" & Replace(sCity, "'", "''") & "', County = '" & Replace(mCounty, "'", "''") & "' where zip = '" & sZip & "' AND nID = " & ID & ""
            _strSQL = "UPDATE csz_mst" _
                       & " SET city = '" & Replace(sCity, "'", "''") & "',County ='" & Replace(mCounty, "'", "''") & "', zip ='" & Replace(sZip, "'", "''") & "', ST ='" & Replace(sST, "'", "''") & "', AreaCode ='" & Replace(sAreaCode, "'", "''") & "'" _
                        & " WHERE nID = " & nID & ""                    ''Finding the zip against the nID


            oCmd = New SqlCommand()                                     ''Command in the sql
            oCmd.Connection = conn                                      ''Binding the connection to the command
            oCmd.CommandType = CommandType.Text                         ''which type of command is used for bind text/procedure
            oCmd.CommandText = _strSQL                                  ''binding the string

            conn.Open()                                                 ''Opening the Connection

            oCmd.ExecuteNonQuery()                                      ''Executing the query
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()

                End If
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Sub
#End Region

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        Dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub

    'To search the ZIP details 
    Public Function SearchZIP(ByVal SearchText As String, ByVal lblSearch As String) As DataTable
        Dim conn As New System.Data.SqlClient.SqlConnection(GetConnectionString())
        Dim oCmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim _strSQL As String = ""
        ' Dim _result As Object
        Dim _sqlQuery As String = ""

        Try
            SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "")

            'Search against provided search lable
            Select Case Trim(lblSearch)
                'Search against ZIP code
                Case "ZIP"
                    If SearchText.Trim() = "" Then
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip"
                    Else
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST where zip like '" & SearchText.Trim & "%'"
                    End If
                    'Search against City
                Case "City"
                    If SearchText.Trim() = "" Then
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip "
                    Else
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST where City like '" & SearchText.Trim & "%'"
                    End If
                Case ("State")
                    'Search against state 
                    If SearchText.Trim() = "" Then
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip"
                    Else
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST where ST like '" & SearchText.Trim & "%'"
                    End If
                Case ("County")
                    'Search against county
                    If SearchText.Trim() = "" Then
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip"
                    Else
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST where County like '" & SearchText.Trim & "%'"
                    End If
                Case ("Area Code")
                    'Search against area code
                    If SearchText.Trim() = "" Then
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip"
                    Else
                        _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST where AreaCode like '" & SearchText.Trim & "%'"
                    End If
                Case ""
                    _sqlQuery = "Select distinct nID,isnull(Zip,0)As zip, City,county,ST,AreaCode from CSZ_MST order by zip"


            End Select

            conn.Open()
            oCmd = New System.Data.SqlClient.SqlCommand()
            oCmd.Connection = conn
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = _sqlQuery
            Dim ad As SqlDataAdapter
            Dim dt As New DataTable

            ad = New SqlDataAdapter(oCmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            Return dt
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally

            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If oCmd IsNot Nothing Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try



    End Function

    'To delete zip entry from the database
    Public Sub DeleteZIP(ByVal ID As Int64)
        'Dim dt As New DataTable
        Dim oCmd As SqlCommand = Nothing
        Try
            Dim conn As New SqlConnection(GetConnectionString())

            Dim _strSQL As String = ""
            '  Dim _result As Object

            ' Dim ad As SqlDataAdapter

            Try
                _strSQL = "Delete FROM CSZ_MST WHERE nID= " & ID & ""

                oCmd = New SqlCommand()

                oCmd.Connection = conn
                oCmd.CommandType = CommandType.Text
                oCmd.CommandText = _strSQL
                conn.Open()

                oCmd.ExecuteNonQuery()

            Catch ex As Exception

            Finally

                If Not IsNothing(conn) Then
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                End If

            End Try
        Catch ex As Exception

        Finally
            If oCmd IsNot Nothing Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
    End Sub

#Region "Called from MergePatient"
    Public Sub DeletePatientData(ByVal PatientID As Long, ByVal DestPatientID As Long, Optional ByVal PatientCode As String = "")
        Dim objParam As SqlParameter
        Dim objParam2 As SqlParameter
        Try
            If Not IsNothing(Conn) Then
                Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeletePatient", Conn)
                If Not IsNothing(Cmd) Then
                    Cmd.CommandType = CommandType.StoredProcedure

                    objParam = Cmd.Parameters.AddWithValue("@nPatientId", PatientID)
                    objParam.Direction = ParameterDirection.Input


                    objParam2 = Cmd.Parameters.AddWithValue("@nDestPatientId", DestPatientID)
                    objParam2.Direction = ParameterDirection.Input


                    'objParam.Value = PatientID
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    Cmd.ExecuteNonQuery()

                    If PatientCode = "" Then
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''
                        'objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrPatientFirstName & " " & gstrPatientLastName & " " & " Patient Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
                    Else
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, PatientCode & " - " & " " & " Patient Deleted", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, PatientCode & " - " & " Patient Deleted", PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''
                        'objAudit.CreateLog(clsAudit.enmActivityType.Delete, PatientCode & " - " & PatientName & " " & " Patient Deleted", gstrLoginName, gstrClientMachineName, PatientID)
                    End If
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                'objAudit = Nothing
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        Finally
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            End If
           
            objParam = Nothing
            objParam2 = Nothing
        End Try
    End Sub
#End Region

    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Conn IsNot Nothing Then
                    Conn.Dispose()
                    Conn = Nothing

                End If
                If Dv IsNot Nothing Then
                    Dv.Dispose()
                    Dv = Nothing

                End If
                If IsNothing(ArrInsuranceCol) Then
                    ArrInsuranceCol.Clear()
                    ArrInsuranceCol = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
