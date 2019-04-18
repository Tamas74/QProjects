
Public Class ClseNum
    'Code Start- Added by Rohit on 20101007..
    Public Enum cls_enum_Fields
        ''Patien
        Patien_Code = 1
        Patient_First_Name = 2
        Patient_Middle_Name = 3
        Patient_Last_Name = 4
        SSN_Number = 5
        Patient_Date_Time_of_Birth = 6
        Patient_Gender = 7
        Patient_Marital_Status = 8
        Patient_Address_Line1 = 9
        Patient_Address_Line2 = 10
        Patient_City = 11
        Patient_State = 12
        Patient_ZIP = 13
        Patient_County = 14
        Patient_Country = 15
        Patient_Phone_Number = 16
        Patient_Mobile_Number = 17
        Patient_Email = 18
        Mother_First_Name = 19
        Patient_Occupation = 20
        Employment_Status = 21
        Place_of_Employment = 22
        Patient_Work_Address_Line1 = 23
        Patient_Work_Address_Line2 = 24
        Patient_Work_City = 25
        Patient_Work_State = 26
        Patient_Work_ZIP = 27
        Patient_Work_Phone_Number = 28
        Race = 29
        Mother_Middle_Name = 30
        Mother_Last_Name = 31
        Patient_External_Code = 32
        Patient_Registration_Date = 33
        Patient_Fax = 34
        Patient_Work_Fax = 35
        Patient_Emergency_Contact = 36
        Patient_Emergency_Phone = 37
        Patient_Status = 38

        ''Provider_MST
        Provider_External_Code = 39
        Provider_First_Name = 40
        Provider_Middle_Name = 41
        Provider_Last_Name = 42
        Provider_UPIN = 43
        Provider_Medical_License_No = 44
        Provider_NPI = 45

        ''Contacts_MST (Referrals)
        Referrals_Street = 46
        Referrals_City = 47
        Referrals_State = 48
        Referrals_ZIP = 49
        Referrals_Phone_Number = 50
        Referrals_Email = 51
        Referrals_First_Name = 52
        Referrals_Middle_Name = 53
        Referrals_Last_Name = 54
        Referrals_External_Code = 55
        Referrals_Fax = 56
        Referrals_Gender = 57
        Referrals_Degree = 58

        'Patient_DTL
        Referrals_Name = 59
        Referrals_UPIN = 60
        Referrals_NPI = 61

        'Contacts_MST Primary Care Phy.
        Primary_Care_Physician_Street = 62
        Primary_Care_Physician_City = 63
        Primary_Care_Physician_State = 64
        Primary_Care_Physician_ZIP = 65
        Primary_Care_Physician_Phone_Number = 66
        Primary_Care_Physician_Email = 67
        Primary_Care_Physician_First_Name = 68
        Primary_Care_Physician_Middle_Name = 69
        Primary_Care_Physician_Last_Name = 70
        Primary_Care_Physician_External_Code = 71

        'Contacts_MSTInsurance
        Insurance_Address_Line1 = 72
        Insurance_City = 73
        Insurance_Contact = 74
        Insurance_Name = 75
        Insurance_External_Code = 76
        Insurance_State = 77
        Insurance_ZIP = 78
        Insurance_Address_Line2 = 79
        Insurance_Phone = 80
        Insurance_Fax = 81
        Insurance_Mobile = 82
        Insurance_Pager = 83
        Insurance_Email = 84
        Insurance_URL = 85
        Insurance_Notes = 86
        Insurance_Gender = 87
        Insurance_SpecialtyID = 88
        Insurance_HospitalAffiliation = 89
        Insurance_ContactType = 90
        Insurance_Degree = 91
        Insurance_Active_StartTime = 92
        Insurance_Active_EndTime = 93
        Insurance_ServiceLevel = 94
        Insurance_NCPDPID = 95
        Insurance_AddressLine1 = 96
        Insurance_IsBlocked = 97
        Insurance_BMAddressLine1 = 98
        Insurance_BMAddressLine2 = 99
        Insurance_BMCity = 100
        Insurance_BMState = 101
        Insurance_BMZip = 102
        Insurance_BMPhone = 103
        Insurance_BMFax = 104
        Insurance_BMEail = 105
        Insurance_BMURL = 106
        Insurance_PracAddressLine1 = 107
        Insurance_PracAddressLine2 = 108
        Insurance_PracCity = 109
        Insurance_PracState = 110
        Insurance_PracZIP = 111
        Insurance_PracPhone = 112
        Insurance_PracFax = 113
        Insurance_PracEmail = 114
        Insurance_PracURL = 115

        ''Contacts_Insurance_DTL
        Insurance_TypeCode = 116
        Insurance_TypeDesc = 117
        PayerId = 118
        Access_Assignment = 119
        Statement_To_Patient_ = 120
        Medigap = 121
        ReferringID_InBox_19 = 122
        NameOfacilityinBox33 = 123
        Do_Not_Print_Facility = 124
        stPointer = 125
        Box_31_Blank = 126
        ShowPayment = 127
        TypeOBilling = 128
        ClearingHouse = 129
        IsClaims = 130
        IsRemittanceAdvice = 131
        Is_RealTime_Eligibility = 132
        IsElectronic_COB = 133
        IsRealTime_Claim_Status = 134
        IsEnrollment_Required = 135
        Payer_Phone = 136
        Website = 137
        Servicing_State = 138
        IComments = 139
        Payer_Phone_Extn = 140

        ''PatientInsurance_DTL
        Subscriber_Policy_Number = 141
        Insurance_Subscriber_ID = 142
        Insurance_Group = 143
        Insurance_Employer = 144
        Insureds_Date_of_Birth = 145
        CopayER = 146
        CopayOV = 147
        CopaySP = 148
        CopayUC = 149
        Effective_Date = 150
        Expiry_Date = 151
        Subscriber_First_Name = 152
        Subscriber_Middle_Name = 153
        Subscriber_Last_Name = 154
        RelationShipID = 155
        RelationShip = 156
        Deductable_amount = 157
        Coverage_Percent = 158
        CoPay = 159
        Assignment_of_Benifit = 160
        Start_Date = 161
        End_Date = 162
        Insurance_Flag = 163
        Subscriber_Gender = 164
        Subscriber_AddressLine1 = 165
        Subscriber_AddressLine2 = 166
        Subscriber_State = 167
        Subscriber_City = 168
        Subscriber_Zip = 169
        Patient_Insurance_External_Code = 170
        Patient_Insurance_Phone = 171
        Patient_Insurance_Name = 172
        Patient_Insurance_AddressLine1 = 173
        Patient_Insurance_AddressLine2 = 174
        Patient_Insurance_City = 175
        Patient_Insurance_State = 176
        Patient_Insurance_ZIP = 177
        Patient_Insurance_Fax = 178
        Patient_Insurance_Email = 179
        Patient_Insurance_URL = 180
        Subscriber_Phone = 181

        ''Patient_OtherContacts(Guarantor)
        Guarantor_First_Name = 182 '184
        Guarantor_Last_Name = 183
        Guarantor_Full_Name = 184 '182
        Guarantor_Middle_Name = 185
        Guarantor_Date_of_Birth = 186
        Guarantor_Sex = 187
        Guarantor_Relationship = 188
        Guarantor_Address_Line1 = 189
        Guarantor_Address_Line2 = 190
        Guarantor_City = 191
        Guarantor_State = 192
        Guarantor_ZIP = 193
        Guarantor_Country = 194
        Guarantor_Phone = 195
        Guarantor_SSN_Number = 196

        ''Medication
        Medication_Date = 197
        Medication_RxNorm_Code = 198
        Medication_Name = 199
        Dosage_of_Medication = 200
        Frequency_of_Medication = 201
        Medication_Start_Date = 202
        Medication_End_Date = 203
        Medication_Reason = 204
        Medication_Status = 205
        Medication_Amount = 206
        Medication_unit = 207
        Medication_Route = 208
        Refills = 209

        ''History
        History_Item = 210
        Drug_Name = 211
        History_Reaction = 212
        HComments = 213
        History_Date = 229
        History_Status = 230
        History_RxNorm_Code = 231
        History_Snomed_Code = 232

        'Encounter
        Visit_Date = 214

        ''Vital
        Vital_Date = 215
        Blood_Pressure_Standing_Max = 216
        Blood_Pressure_Standing_Min = 217
        Blood_Pressure_Sitting_Max = 218
        Blood_Pressure_Sitting_Min = 219
        Temperature_in_Celcius = 220
        Pulse_Per_Minute = 221
        '' Weight_in_Kg = 222
        ''Solved case related to Vitals->Weight lbs instead of kg:20111013
        Weight_in_lbs = 222
        Respiratory_Rate = 223
        Height_in_inch = 224
        VComments = 225
        dPulseOx = 280
        ''Problem List
        Date_Of_Service = 226
        Cheif_Complaint = 227
        ICD9_Code = 228
        Problem_Status = 245
        'Problem_ID = 246

        'Famili History
        Family_History_Item = 233
        Family_Comments = 234
        Family_History_Date = 235
        Family_History_Status = 236
        Family_RxNorm_Code = 237
        Family_Snomed_Code = 238

        'Social History
        Social_History_Item = 239
        Social_Comments = 240
        Social_History_Date = 241
        Social_History_Status = 242
        Social_RxNorm_Code = 243
        Social_Snomed_Code = 244

        'Immunization

        Immunization_Item_Name = 246
        Im_Visit_Date = 247
        Im_Immunization_Date = 248
        Im_Immunization_Notes = 249
        Im_Transaction_time_given = 250
        Im_Vaccine_code = 251
        Im_Lot_number = 252
        Im_Site = 253
        Im_Route = 254
        Im_Dose = 255
        Im_Manufacturer = 256
        Im_Expiration_date = 257
        Im_Due_Date = 258
        Im_CPT_code = 259

        'Lab_Result
        Lab_Test_Name = 262
        Lab_OrderNo_ID = 263
        Lab_Test_Code = 264
        Lab_Result_Name = 265
        Lab_Result_Value = 266
        Lab_Result_Units = 267
        Lab_Result_Range = 268
        Lab_Result_Abnormal_Flag = 269
        Lab_Result_Type = 270
        Lab_Result_Data_Type = 271
        Lab_Result_LOINCID = 272
        Lab_Specimen_Received_Date = 273
        Lab_Result_Transfer_DateTime = 274
        Lab_Result_Alternate_Name = 275
        Lab_Result_Alternate_Code = 276
        Lab_File_Order_Identifier = 277
        Lab_Producer_Identifier = 278
        Lab_Order_Provider_Code = 279
    End Enum
    'Code End- Added by Rohit on 20101007
End Class
