<?xml version="1.0" encoding="utf-8"?>
<!-- DWXMLSource="ResponseXMLFile.xml" -->
<!DOCTYPE xsl:stylesheet [
  <!ENTITY nbsp "&#160;">
  <!ENTITY copy "&#169;">
  <!ENTITY reg "&#174;">
  <!ENTITY trade "&#8482;">
  <!ENTITY mdash "&#8212;">
  <!ENTITY ldquo "&#8220;">
  <!ENTITY rdquo "&#8221;">
  <!ENTITY pound "&#163;">
  <!ENTITY yen "&#165;">
  <!ENTITY euro "&#8364;">
]>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:n3="http://www.w3.org/1999/xhtml" xmlns:n1="urn:hl7-org:v3" xmlns:n2="urn:hl7-org:v3/meta/voc" xmlns:voc="urn:hl7-org:v3/voc" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1.0">
  <xsl:output method="html" indent="yes" version="4.01" encoding="ISO-8859-1" doctype-public="-//W3C//DTD HTML 4.01//EN"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Untitled Document</title>
        <style type="text/css">
          .Mainheader
          {
          background-color: #f7b210;
          background: -webkit-gradient(linear, left top, left bottom, from(#f7b210), to(#f79218));
          background: -moz-linear-gradient(top,  #f7b210,  #f79218);
          background: -ms-linear-gradient(top, #f7b210, #f79218);
          background: -o-linear-gradient(top, #f7b210, #f79218);
          filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f7b210', endColorstr='#f79218');
          zoom:1;
          color:white;
          font-family: Tahoma,Geneva,sans-serif;
          font-size: 15px;
          font-weight: bold;
          padding:2px;
          /*padding-left:2px;*/
          text-decoration:none;

          }
          .physicianCap
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          color:#0069aa;
          text-decoration:none;
          width:370px;
          float:left;
          padding:2px;
          word-wrap: break-word;
          font-weight:bold;
          }
          .physicianCapNoWidth
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          color:#0069aa;
          text-decoration:none;
          width:70px;
          float:left;
          padding:2px;
          word-wrap: break-word;
          font-weight:bold;
          }

          .Orgphysician
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          color:#0069aa;
          text-decoration:none;
          background-color:#effaff;
          padding-bottom:5px;

          }
          .OrgphysicianSUB
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          color:#0069aa;
          text-decoration:none;
          background-color:#fdf4df;
          padding:10px;
          border: 1px solid #f98800;
          }
          .orangeheader
          {
          background-color:#fdf4df;
          border: 1px solid #f98800;
          color:black;
          padding-left: 5px;
          }

          .physicianCapfont
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          font-weight:bold;
          text-decoration:none;
          color:#000;
          }
          .sub-mini-note {
          margin-left: 150px;
          }
          .sub-physicianCapfont
          {
          font-family:Tahoma, Geneva, sans-serif;
          font-size:12px;
          font-weight:bold;
          text-decoration:none;
          color:#000;
          padding:4px 4px 4px 0px;
          }
          .gradient
          {
          background-color:#ffffff;
          border:1px solid #0069AA;
          }
          .sub-td{
          /*text-decoration:underline;*/
          }
          .sub-td-padding{
          /*padding-left:20px;*/
          }
          .left{
          width:15%;
          }
          .right{
          width:25%;
          }
          .tr-margin{
          padding:2px;
          }

          .SUBMed {
          border: 1px solid #f98800;
          border-collapse: collapse;
          width:100%;
          color:black;
          padding: 5px;
          }
          .SUBMedth {
          background-color:#ffc99e;
          border: 1px solid #f98800;
          text-align: left;
          }
          

        </style>
      </head>
      <body>

        <xsl:if test="Message/Body/CancelRxResponse">

          <xsl:if test="Message/Body/CancelRxResponse/Response">
            
            <xsl:if test="Message/Body/CancelRxResponse/Response/Approved">
              <div class="Mainheader">
                <table width="100%" cellspacing="0" cellpadding="0" >
                  <tr  width="100%">
                    <td width="80%" style="padding-left:5px;">
                      Approved
                    </td>
                  </tr>
                </table>
              </div>
              <div class="Orgphysician">
                <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
                  <tr  width="100%">
                    <td class="left">Note : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Response/Approved/Note"/>
                    </td>
                  </tr>
                </table>
              </div>
            </xsl:if>

            <xsl:if test="Message/Body/CancelRxResponse/Response/Denied">
              <div class="Mainheader">
                <table width="100%" cellspacing="0" cellpadding="0" >
                  <tr  width="100%">
                    <td width="80%" style="padding-left:5px;">
                      Denied
                    </td>
                  </tr>
                </table>
              </div>
              <div class="Orgphysician">
                <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
                  <tr  width="100%">
                    <td class="left">Note : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Response/Denied/Note"/>
                    </td>
                  </tr>
                  <tr  width="100%">
                    <td class="left">Denied Reason : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Response/Denied/DenialReason"/>
                    </td>
                  </tr>
                  <tr  width="100%">
                    <td class="left">Reference Number : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Response/Denied/ReferenceNumber"/>
                    </td>
                  </tr>
                  <tr  width="100%">
                    <td class="left" style="text-align:left;vertical-align:top;">Denied reason codes: </td>
                    <td  class="physicianCapfont right">
                      <table   width="100%" cellpadding="0" cellspacing="0">
                      <xsl:for-each select="Message/Body/CancelRxResponse/Response/Denied/DenialReasonCode">
                          <tr  width="100%">
                            <td  class="left">
                                <xsl:call-template name="ReasonCodes"/>
                              <!--<xsl:value-of select="."/>-->
                            </td>
                            <td></td>
                          </tr>
                      </xsl:for-each>
                      </table>
                    </td>
                  </tr>
                </table>
              </div>
            </xsl:if>
            
          </xsl:if>

          <xsl:if test="Message/Body/CancelRxResponse/Request">
            
            <xsl:if test="Message/Body/CancelRxResponse/Request/ChangeRequestType">
              
              <div class="Mainheader">
                <table width="100%" cellspacing="0" cellpadding="0" >
                  <tr  width="100%">
                    <td width="80%" style="padding-left:5px;">
                      Change Request Details
                    </td>
                  </tr>
                </table>
              </div>

              <div class="Orgphysician">
                <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
                  <tr  width="100%">
                    <td class="left">Change request type : </td>
                    <td  class="physicianCapfont right">
                      <xsl:variable name="ChangeRequestType" select="Message/Body/CancelRxResponse/Request/ChangeRequestType"/>
                      <xsl:choose>
                        <xsl:when test="$ChangeRequestType!='C1'">
                          Significant change (Any changes to the Drug, form, strength, dosage, or route)
                        </xsl:when>
                        <xsl:when test="$ChangeRequestType!='C2'">
                          Frequency Change (Any change to the frequency or hours of administration for the drug)
                        </xsl:when>
                        <xsl:when test="$ChangeRequestType!='C3'">
                          Insignificant Change (All other changes)
                        </xsl:when>
                        <xsl:otherwise>
                          ERROR
                        </xsl:otherwise>
                      </xsl:choose>
                    </td>
                  </tr>
             
                  <tr  width="100%">
                    <td class="left">Reference Number : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Request/RequestReferenceNumber"/>
                    </td>
                  </tr>
            
                  <tr  width="100%">
                    <td class="left">Return Receipt : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/CancelRxResponse/Request/ReturnReceipt"/>
                    </td>
                  </tr>
             
            
                  <tr  width="100%">
                    <td class="left">Change of Prescription Status Flag : </td>
                    <td  class="physicianCapfont right">
                      <xsl:variable name="PrescriptionStatusFlag" select="Message/Body/CancelRxResponse/Request/ChangeofPrescriptionStatusFlag"/>
                      <xsl:choose>
                        <xsl:when test="$PrescriptionStatusFlag!='C'">
                          Cancel
                        </xsl:when>
                        <xsl:when test="$PrescriptionStatusFlag!='D'">
                          Discontinue
                        </xsl:when>
                      </xsl:choose>
                    </td>
                  </tr>
                </table>
              </div>
            </xsl:if>
          </xsl:if>
        </xsl:if>
        
        <xsl:if test="Message/Body/RxFill">
   
          
          <!--Medication Prescribed STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Medication Prescribed
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <tr  width="100%">
                <td class="left"  >Drug : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/DrugDescription"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug Qty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Quantity/Value"/>&#160;
                  <xsl:value-of select="Message/PotencyDescription/MP"/>
                </td>
                <td class="left"  >Duration : </td>
                <td  class="physicianCapfont right">
                  <xsl:variable name="Days" select="Message/Body/RxFill/MedicationPrescribed/DaysSupply"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>
              
              <tr  width="100%">
                <td class="left"  >Code Qty Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:choose>
                    <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier='38'">
                      38&#160;(Original Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier='40'">
                      40&#160;(Remaining Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier='87'">
                      87&#160;(Quantity Received)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier='QS'">
                      QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier='CF'">
                      CF&#160;(Compound Final Quantity)
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Quantity/CodeListQualifier"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Source Code List : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Quantity/UnitSourceCode"/> &#160;(NCPDP Drug quantity unit of measure terminology)
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>
              
              <tr  width="100%">
                <td class="left"  >Drug Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Notes : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Note"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Ref Qlf : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Refills/Qualifier"/>-->
                  <xsl:variable name="RefQlf" select="Message/Body/RxFill/MedicationPrescribed/Refills/Qualifier"/>
                  <xsl:choose>
                    <xsl:when test="$RefQlf ='R'">
                      R - Number of Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='A'">
                      A - Additional Refills Authorized
                    </xsl:when>
                    <xsl:when test="$RefQlf ='P'">
                      P - Pharmacy Requested Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='PRN'">
                      PRN - As Needed
                    </xsl:when>
                    <xsl:otherwise>
                      Transaction designation
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Refills :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Refills/Value"/>
                  <!--<xsl:call-template name="RxFillRefillsdata"/>-->
                </td>
                <!--</tr>

                 <tr  width="100%">-->
                <td class="left"  >Substitution : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="RxFillMedicationPrescribedNarcoticFlag"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Written Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="formatDate">
                    <xsl:with-param name="datestr" select="translate(Message/Body/RxFill/MedicationPrescribed/WrittenDate/Date,'-','/')"/>
                  </xsl:call-template>
                </td>

                <td class="left"  >Last Fill Date : </td>
      
                <xsl:if test="Message/Body/RxFill/MedicationPrescribed/LastFillDate/Date">
                  <td  class="physicianCapfont right">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(Message/Body/RxFill/MedicationPrescribed/LastFillDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </td>
                </xsl:if>
          
              </tr>

              <xsl:if test="Message/Body/RxFill/MedicationPrescribed/Diagnosis">
                <tr  width="100%">
                  <td colspan="7">
                    <xsl:variable name="vClr1">
                      <xsl:text>#fde9d9</xsl:text>
                    </xsl:variable>
                    <xsl:variable name="vClr2">
                      <xsl:text>#fff</xsl:text>
                    </xsl:variable>
                    <div style="padding-bottom:5px;">
                      <table class="SUBMed" width="90%" cellpadding="5" cellspacing="0" border="1px">
                        <tr >
                          <th class="SUBMedth" >Clinical Info Qualifier</th>
                          <th class="SUBMedth" >Diagnosis</th>
                          <th class="SUBMedth" >Code List Qualifier</th>
                          <th class="SUBMedth" >ICD Code</th>
                        </tr>
                        <xsl:if test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary">
                          <tr style="background-color: {$vClr1}; ">
                            <td  style="border: 1px solid #f98800;" rowspan="2">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='1'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='2'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">Primary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Primary/Value"/>
                            </td>

                          </tr>
                        </xsl:if>
                        <xsl:if test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary">
                          <tr style="background-color: {$vClr2};">
                            <td  style="border: 1px solid #f98800;">Secondary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxFill/MedicationPrescribed/Diagnosis/Secondary/Value"/>
                            </td>
                          </tr>
                        </xsl:if>
                      </table>
                    </div>
                  </td>
                </tr>
              </xsl:if>
              
              <!--Blank TR added for allignment-->
              <tr  width="100%">
                <td class="left"   ></td>
                <td  class="physicianCapfont right" ></td>
                <td class="left" ></td>
                <td  class="physicianCapfont right" ></td>
                <td class="left"  ></td>
                <td  class="physicianCapfont right" ></td>
              </tr>
            </table>

          </div>
          <!--Medication Prescribed ENDs HERE-->

          <!--Medication Dispensed STARTs HERE-->
          <xsl:if test="Message/Body/RxFill/MedicationDispensed">
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Medication Dispensed
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <tr  width="100%">
                <td class="left"  >Drug : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/DrugDescription"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug Qty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Quantity/Value"/>&#160;
                  <xsl:value-of select="Message/PotencyDescription/MP"/>
                </td>
                <td class="left"  >Duration : </td>
                <td  class="physicianCapfont right">
                  <xsl:variable name="Days" select="Message/Body/RxFill/MedicationDispensed/DaysSupply"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Code Qty Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:choose>
                    <xsl:when test="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier='38'">
                      38&#160;(Original Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier='40'">
                      40&#160;(Remaining Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier='87'">
                      87&#160;(Quantity Received)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier='QS'">
                      QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier='CF'">
                      CF&#160;(Compound Final Quantity)
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Quantity/CodeListQualifier"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Source Code List : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Quantity/UnitSourceCode"/> &#160;(NCPDP Drug quantity unit of measure terminology)
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Notes : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Note"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Ref Qlf : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Refills/Qualifier"/>-->
                  <xsl:variable name="RefQlf" select="Message/Body/RxFill/MedicationDispensed/Refills/Qualifier"/>
                  <xsl:choose>
                    <xsl:when test="$RefQlf ='R'">
                      R - Number of Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='A'">
                      A - Additional Refills Authorized
                    </xsl:when>
                    <xsl:when test="$RefQlf ='P'">
                      P - Pharmacy Requested Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='PRN'">
                      PRN - As Needed
                    </xsl:when>
                    <xsl:otherwise>
                      Transaction designation
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Refills :</td>
                <!--the caption was Value :-->
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Refills/Value"/>
                  <!--<xsl:call-template name="RxFillRefillsdata"/>-->
                </td>
                <!--</tr>

                 <tr  width="100%">-->
                <td class="left"  >Substitution : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="RxFillMedicationDispensedNarcoticFlag"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Written Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="formatDate">
                    <xsl:with-param name="datestr" select="translate(Message/Body/RxFill/MedicationDispensed/WrittenDate/Date,'-','/')"/>
                  </xsl:call-template>
                </td>

                <td class="left"  >Last Fill Date : </td>

                <xsl:if test="Message/Body/RxFill/MedicationDispensed/LastFillDate/Date">
                  <td  class="physicianCapfont right">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(Message/Body/RxFill/MedicationDispensed/LastFillDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </td>
                </xsl:if>

              </tr>

              <xsl:if test="Message/Body/RxFill/MedicationDispensed/Diagnosis">
                <tr  width="100%">
                  <td colspan="7">
                    <xsl:variable name="vClr1">
                      <xsl:text>#fde9d9</xsl:text>
                    </xsl:variable>
                    <xsl:variable name="vClr2">
                      <xsl:text>#fff</xsl:text>
                    </xsl:variable>
                    <div style="padding-bottom:5px;">
                      <table class="SUBMed" width="90%" cellpadding="5" cellspacing="0" border="1px">
                        <tr >
                          <th class="SUBMedth" >Clinical Info Qualifier</th>
                          <th class="SUBMedth" >Diagnosis</th>
                          <th class="SUBMedth" >Code List Qualifier</th>
                          <th class="SUBMedth" >ICD Code</th>
                        </tr>
                        <xsl:if test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary">
                          <tr style="background-color: {$vClr1}; ">
                            <td  style="border: 1px solid #f98800;" rowspan="2">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/ClinicalInformationQualifier='1'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/ClinicalInformationQualifier='2'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">Primary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Primary/Value"/>
                            </td>

                          </tr>
                        </xsl:if>
                        <xsl:if test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary">
                          <tr style="background-color: {$vClr2};">
                            <td  style="border: 1px solid #f98800;">Secondary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxFill/MedicationDispensed/Diagnosis/Secondary/Value"/>
                            </td>
                          </tr>
                        </xsl:if>
                      </table>
                    </div>
                  </td>
                </tr>
              </xsl:if>
              
            </table>

          </div>
          </xsl:if>
          <!--Medication Dispensed ENDs HERE-->

          <!--Fill Status STARTs HERE-->

          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Fill Status
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <tr  width="100%">
                <td class="left">Status : </td>
                <td colspan="5" class="physicianCapfont right">
                  <xsl:if test="Message/Body/RxFill/FillStatus/Filled">
                    Filled
                  </xsl:if>
                  <xsl:if test="Message/Body/RxFill/FillStatus/PartialFill">
                    Partial fill
                  </xsl:if>
                  <xsl:if test="Message/Body/RxFill/FillStatus/NotFilled">
                    Not filled
                  </xsl:if>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Note : </td>
                <td colspan="5" class="physicianCapfont right">
                  <xsl:if test="Message/Body/RxFill/FillStatus/Filled/Note">
                    <xsl:value-of select="Message/Body/RxFill/FillStatus/Filled/Note"/>
                  </xsl:if>
                  <xsl:if test="Message/Body/RxFill/FillStatus/PartialFill/Note">
                    <xsl:value-of select="Message/Body/RxFill/FillStatus/PartialFill/Note"/>
                  </xsl:if>
                  <xsl:if test="Message/Body/RxFill/FillStatus/NotFilled/Note">
                    <xsl:value-of select="Message/Body/RxFill/FillStatus/NotFilled/Note"/>
                  </xsl:if>
                </td>
              </tr>
              <!--Blank TR added for allignment-->
              <tr  width="100%">
                <td class="left"   ></td>
                <td  class="physicianCapfont right" ></td>
                <td class="left" ></td>
                <td  class="physicianCapfont right" ></td>
                <td class="left"  ></td>
                <td  class="physicianCapfont right" ></td>
              </tr>
            </table>
          </div>

          <!--Fill Status ENDs HERE-->
          
          <!--Patient INFO STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Patient Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <tr  width="100%">
                <td class="left"  >Patient Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Name/LastName"/>&#160;<xsl:value-of select="Message/Body/RxFill/Patient/Name/FirstName"/>
                </td>
                <td class="left"  >&#160;</td>
                <td  class="physicianCapfont right">
                  &#160;
                </td>
              </tr>
              
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <xsl:variable name="sex" select="Message/Body/RxFill/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>
                </td>
                </tr>
              <tr  width="100%">
                <td > Date of Birth : </td>
                  <xsl:variable name="DateOfBirth" select="Message/Body/RxFill/Patient/DateOfBirth/Date"/>
                  <xsl:choose>
                    <xsl:when test="$DateOfBirth !=''">
                      <td class="physicianCapfont right">
                        <xsl:call-template name="formatDate">
                          <xsl:with-param name="datestr" select="translate(Message/Body/RxFill/Patient/DateOfBirth/Date,'-','/')"/>
                        </xsl:call-template>
                      </td>
                    </xsl:when>
                    <xsl:otherwise>
                      <td class="physicianCapfont right"> &#160;</td>
                    </xsl:otherwise>
                  </xsl:choose>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/State"/>
                </td>
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/ZipCode"/>
                </td>
                </tr>
              <!--<tr  width="100%">-->
              
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Patient/Address/PlaceLocationQualifier"/>
                </td>-->
              <!--</tr>-->

              <xsl:for-each select="Message/Body/RxFill/Patient/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                    <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                    <td class="left"  >Telephone (TE) : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Number"/>
                    </td>
                    </tr>
                    </xsl:when>
                  
                    <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                      </tr>
                    </xsl:when>
                  
                </xsl:choose>
                </xsl:for-each>
            </table>
          </div>
          <!-- Patient INFO ENDs HERE-->

          <!--Pharmacy INFO STARTS here-->
          <div class="Mainheader">
          <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Pharmacy Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%">
              <!--<tr  width="100%">
              <td class="left" > NCPDPID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/NPI"/>
                </td>
                <td class="left"  >NCPDPID : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/NCPDPID"/>
                </td>
             </tr>

              <tr  width="100%">
                <td class="left" >Pharmacy Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/State"/>
                </td>
                <!--</tr>
                 <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <tr  width="100%">
                <xsl:for-each select="Message/Body/RxFill/Pharmacy/CommunicationNumbers/Communication">
                  <xsl:variable name="Qualifier" select="Qualifier"/>

                  <xsl:choose>
                    <xsl:when test="$Qualifier='TE'" >

                      <td class="left"  >Telephone (TE) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                      <!--</tr>-->
                    </xsl:when>

                    <xsl:when test="$Qualifier='FX'">
                      <!--<tr  width="100%">-->
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>

                    </xsl:when>

                  </xsl:choose>

                </xsl:for-each>
              </tr>
            </table>
          </div>
          <!--Pharmacy INFO ENDS here-->

          <!--Provider INFO STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Provider Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Identification/NPI"/>
                </td>
                <td class="left"  >DEA Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Identification/DEANumber"/>
                </td>
              </tr>
              
              <!--<tr  width="100%">
                <xsl:for-each select="Message/Body/RxFill/Prescriber/Identification/*">
                  <td class="left" >
                    <xsl:variable name="TagVal" select="name()"/>
                    <xsl:choose>
                      <xsl:when test="$TagVal='NPI'">
                        <xsl:value-of select="concat(name(),' :')"/>
                        <td  class="physicianCapfont right">
                          <xsl:value-of select="current()"/>
                        </td>
                      </xsl:when>
                    </xsl:choose>
                  </td>
                </xsl:for-each>
              </tr>-->

              <!--<xsl:for-each select="Message/Body/RxFill/Prescriber/Identification/*">
                <xsl:variable name="TagVal" select="Identification"/>
                <xsl:choose>
                  <xsl:when test="$TagVal='DEANumber'">
                    <tr  width="100%">
                      <td class="left"  >DEA Number : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="DEANumber"/>
                      </td>
                    </tr>
                  </xsl:when>

                  <xsl:when test="$TagVal='SocialSecurity'">
                    <tr  width="100%">
                      <td class="left"  >Social Security : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="SocialSecurity"/>
                      </td>
                    </tr>
                  </xsl:when>

                  <xsl:when test="$TagVal='NPI'">
                    <tr  width="100%">
                      <td class="left"  >NPI : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="NPI"/>
                      </td>
                    </tr>
                  </xsl:when>

                </xsl:choose>
              </xsl:for-each>-->


              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Specialty"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/ClinicName"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Provider Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/LastName"/>&#160; <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/FirstName"/>&#160; <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/MiddleName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/State"/>
                </td>
                <!--</tr>
                 <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->

              <tr  width="100%">
                <xsl:for-each select="Message/Body/RxFill/Prescriber/CommunicationNumbers/Communication">
                  <xsl:variable name="Qualifier" select="Qualifier"/>
                  <xsl:choose>
                    <xsl:when test="$Qualifier='TE'">
                      <td class="left"  >Telephone (TE) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </xsl:when>

                    <xsl:when test="$Qualifier='FX'">
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </xsl:when>
                  </xsl:choose>
                </xsl:for-each>
              </tr>


              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->
            </table>
          </div>
          <!--Provider INFO ENDs HERE-->

          <!--Facility STARTs HERE-->
          <xsl:if test="Message/Body/RxFill/Facility">
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Facility
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%">
              <!--<tr  width="100%">
              <td class="left" > NCPDPID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxFill/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Identification/NPI"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left" >Facility Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/FacilityName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Address/State"/>
                </td>
                <!--</tr>
                 <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Facility/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxFill/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <tr  width="100%">
                <xsl:for-each select="Message/Body/RxFill/Facility/CommunicationNumbers/Communication">
                  <xsl:variable name="Qualifier" select="Qualifier"/>

                  <xsl:choose>
                    <xsl:when test="$Qualifier='TE'" >

                      <td class="left"  >Telephone (TE) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                      <!--</tr>-->
                    </xsl:when>

                    <xsl:when test="$Qualifier='FX'">
                      <!--<tr  width="100%">-->
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>

                    </xsl:when>

                  </xsl:choose>

                </xsl:for-each>
              </tr>
            </table>
          </div>
          </xsl:if>
          <!--Facility ENDs HERE-->


        </xsl:if>
                        
        <xsl:if test="Message/Body/RxChangeRequest">

          <!--Medication Prescribed STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Medication Prescribed
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <tr  width="100%">
                <td class="left"  >Drug</td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugDescription"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left"  >Drug Qty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/Value"/>&#160;
                  <xsl:value-of select="Message/PotencyDescription/MP"/>
                </td>
                <!--<tr  width="100%">-->
                <td class="left"  >Duration : </td>
                <td  class="physicianCapfont right">
                  <xsl:variable name="Days" select="Message/Body/RxChangeRequest/MedicationPrescribed/DaysSupply"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Code Qty Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:choose>
                    <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier='38'">
                      38&#160;(Original Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier='40'">
                      40&#160;(Remaining Quantity)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier='87'">
                      87&#160;(Quantity Received)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier='QS'">
                      QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                    </xsl:when>
                    <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier='CF'">
                      CF&#160;(Compound Final Quantity)
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/CodeListQualifier"/>
                    </xsl:otherwise>
                  </xsl:choose>
                  
                  <!--<xsl:value-of select="Message/PotencyDescription/MP"/>-->
                </td>
                <!--<tr  width="100%">-->
                <td class="left"  >Source Code List : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/UnitSourceCode"/> &#160;(NCPDP Drug quantity unit of measure terminology)
                  
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                  <!--<xsl:variable name="Days" select="Message/Body/RxChangeRequest/MedicationPrescribed/Quantity/UnitSourceCode"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>-->
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Notes : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Note"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Ref Qlf : </td>
                <td  class="physicianCapfont right">
                  <xsl:variable name="RefQlf" select="Message/Body/RxChangeRequest/MedicationPrescribed/Refills/Qualifier"/>
                  <xsl:choose>
                    <xsl:when test="$RefQlf ='R'">
                      R - Number of Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='A'">
                      A - Additional Refills Authorized
                    </xsl:when>
                    <xsl:when test="$RefQlf ='P'">
                      P - Pharmacy Requested Refills
                    </xsl:when>
                    <xsl:when test="$RefQlf ='PRN'">
                      PRN - As Needed
                    </xsl:when>
                    <xsl:otherwise>
                      Transaction designation
                    </xsl:otherwise>
                  </xsl:choose>
                  
                </td>
                <td class="left"  >Refills :</td>
                <!--the caption was Value :-->
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Refills/Value"/>
                  <!--<xsl:call-template name="RxChangeRequestRefillsdata"/>-->
                </td>
                <!--</tr>

                 <tr  width="100%">-->
                <td class="left"  >Substitution : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="RxChangeRequestNarcoticFlag"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Written Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="formatDate">                    
                    <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/MedicationPrescribed/WrittenDate/Date,'-','/')"/>
                  </xsl:call-template>                  
                </td>

                <td class="left"  >Last Fill Date : </td>
                <!--<td  class="physicianCapfont right">
                  <xsl:call-template name="formatDate">
                    <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/MedicationPrescribed/LastFillDate/Date,'-','/')"/>
                  </xsl:call-template>
                </td>-->
                <xsl:if test="Message/Body/RxChangeRequest/MedicationPrescribed/LastFillDate/Date">
                  <td  class="physicianCapfont right">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/MedicationPrescribed/LastFillDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </td>
                </xsl:if>
                
                <!--</td>-->
                <!--<xsl:choose>
                  <xsl:when test="$LastFillDate !=''">
                    <td class="physicianCapfont right">
                      <xsl:call-template name="formatDate">
                        <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/MedicationPrescribed/LastFillDate/Date,'-','/')"/>
                      </xsl:call-template>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="physicianCapfont right"> &#160;</td>
                  </xsl:otherwise>
                </xsl:choose>-->


                <!--</td>-->
              </tr>
              <xsl:if test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis">
                <tr  width="100%">
                  <td colspan="7">
                    <xsl:variable name="vClr1">
                      <xsl:text>#fde9d9</xsl:text>
                    </xsl:variable>
                    <xsl:variable name="vClr2">
                      <xsl:text>#fff</xsl:text>
                    </xsl:variable>
                    <div style="padding-bottom:5px;" >
                      <table class="SUBMed" width="90%" cellpadding="5" cellspacing="0" border="1px">
                        <tr width="100%">
                          <th class="SUBMedth" >Clinical Info Qualifier</th>
                          <th class="SUBMedth" >Diagnosis</th>
                          <th class="SUBMedth" >Code List Qualifier</th>
                          <th class="SUBMedth" >ICD Code</th>
                        </tr>
                        <xsl:if test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary">
                          <tr style="background-color: {$vClr1}; ">
                            <td rowspan="2">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='1'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                </xsl:when>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='2'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">Primary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Value"/>
                            </td>

                          </tr>
                        </xsl:if>
                        <xsl:if test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary">
                          <tr style="background-color: {$vClr2};">
                            <td  style="border: 1px solid #f98800;">Secondary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier='ABF'">
                                  <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Value"/>
                            </td>
                          </tr>
                        </xsl:if>
                      </table>
                    </div>
                  </td>
                </tr>
              </xsl:if>
            </table> 
            
              
            <xsl:if test="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation">
                    <div class="OrgphysicianSUB"  >
                      <table class="SUBMed"   width="100%" cellpadding="5" cellspacing="0" border ="1">
                        <tr  width="100%">
                          <th colspan="7" style="background-color:#fcdcc2 ;" class="orangeheader left" >
                            Drug Use Evaluation
                          </th>
                        </tr>
                        <tr  width="100%">
                          <th class="SUBMedth"  >Service Reason Code</th>
                          <th class="SUBMedth"  >Professional Service Code</th>
                          <th class="SUBMedth" >Service Result Code</th>
                          <th class="SUBMedth"  >CoAgent Id</th>
                          <th class="SUBMedth"   >CoAgent Qualifier</th>
                          <th class="SUBMedth"   >Clinical Significance Code</th>
                          <th class="SUBMedth"  >Acknowledgement Reason</th>
                        </tr>
                        <xsl:for-each select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation">
                          <xsl:variable name="vColor">
                            <xsl:choose>
                              <xsl:when test="position() mod 2 = 1">
                                <xsl:text>#fde9d9</xsl:text>
                              </xsl:when>
                              <xsl:otherwise>#fff</xsl:otherwise>
                            </xsl:choose>
                          </xsl:variable>
                          <tr  class="odd" style="background-color: {$vColor}; ">
                            <!--ServiceReasonCode-->
                            <xsl:variable name="ServiceReasonCode" select="ServiceReasonCode"/>
                            <xsl:choose>
                              <xsl:when test="$ServiceReasonCode !=''">
                                <!--<td class="physicianCapfont right">
                                   <xsl:value-of select="$ServiceReasonCode"/>
                                 </td>-->
                                <xsl:choose>
                                  <xsl:when test="ServiceReasonCode[text() = 'AD']">
                                    <td style="border: 1px solid #f98800;" >AD-Additional Drug Needed</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'AN']">
                                    <td style="border: 1px solid #f98800;" >AN-Prescription Authentication</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'AR']">
                                    <td style="border: 1px solid #f98800;" >Adverse Drug Reaction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'AT']">
                                    <td style="border: 1px solid #f98800;" >Additive Toxicity</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'CD']">
                                    <td style="border: 1px solid #f98800;" >Chronic Disease Management</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'CH']">
                                    <td style="border: 1px solid #f98800;" >Call Help Desk</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'CS']">
                                    <td style="border: 1px solid #f98800;" >Patient Complaint/Symptom</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DA']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Allergy</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DC']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Disease (Inferred)</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DD']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Drug Interaction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DF']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Food interaction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DI']">
                                    <td style="border: 1px solid #f98800;" >Drug Incompatibility</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DL']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Lab Conflict</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DM']">
                                    <td style="border: 1px solid #f98800;" >Apparent Drug Misuse</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DR']">
                                    <td style="border: 1px solid #f98800;" >Dose Range Conflict</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'DS']">
                                    <td style="border: 1px solid #f98800;" >Tobacco Use</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'ED']">
                                    <td style="border: 1px solid #f98800;" >Patient Education/Instruction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'ER']">
                                    <td style="border: 1px solid #f98800;" >Overuse</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'EX']">
                                    <td style="border: 1px solid #f98800;" >Excessive Quantity</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'HD']">
                                    <td style="border: 1px solid #f98800;" >High Dose</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'IC']">
                                    <td style="border: 1px solid #f98800;" >Iatrogenic Condition</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'ID']">
                                    <td style="border: 1px solid #f98800;" >Ingredient Duplication</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'LD']">
                                    <td style="border: 1px solid #f98800;" >Low Dose</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'LK']">
                                    <td style="border: 1px solid #f98800;" >Lock In Recipient</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'LR']">
                                    <td style="border: 1px solid #f98800;" >Underuse</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'MC']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Disease (Reported)</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'MN']">
                                    <td style="border: 1px solid #f98800;" >Insufficient Duration</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'MS']">
                                    <td style="border: 1px solid #f98800;" >Missing Information/Clarification</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'MX']">
                                    <td style="border: 1px solid #f98800;" >Excessive Duration</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NA']">
                                    <td style="border: 1px solid #f98800;" >Drug Not Available</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NC']">
                                    <td style="border: 1px solid #f98800;" >Non‐covered Drug Purchase</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'ND']">
                                    <td style="border: 1px solid #f98800;" >New Disease/Diagnosis</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NF']">
                                    <td style="border: 1px solid #f98800;" >Non‐Formulary Drug</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NN']">
                                    <td style="border: 1px solid #f98800;" >Unnecessary Drug</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NP']">
                                    <td style="border: 1px solid #f98800;" >New Patient Processing</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NR']">
                                    <td style="border: 1px solid #f98800;" >Lactation/Nursing Interaction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'NS']">
                                    <td style="border: 1px solid #f98800;" >Insufficient Quantity</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'OH']">
                                    <td style="border: 1px solid #f98800;" >Alcohol Conflict</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PA']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Age</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PC']">
                                    <td style="border: 1px solid #f98800;" >Patient Question/Concern</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PG']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Pregnancy</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PH']">
                                    <td style="border: 1px solid #f98800;" >Preventive Health Care</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PN']">
                                    <td style="border: 1px solid #f98800;" >Prescriber Consultation</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PP']">
                                    <td style="border: 1px solid #f98800;" >Plan Protocol</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PR']">
                                    <td style="border: 1px solid #f98800;" >Prior Adverse Reaction</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'PS']">
                                    <td style="border: 1px solid #f98800;" >Product Selection Opportunity</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'RE']">
                                    <td style="border: 1px solid #f98800;" >Suspected Environmental Risk</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'RF']">
                                    <td style="border: 1px solid #f98800;" >Health Provider Referral</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SC']">
                                    <td style="border: 1px solid #f98800;" >Suboptimal Compliance</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SD']">
                                    <td style="border: 1px solid #f98800;" >Suboptimal Drug/Indication</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SE']">
                                    <td style="border: 1px solid #f98800;" >Side Effect</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SF']">
                                    <td style="border: 1px solid #f98800;" >Suboptimal Dosage Form</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SR']">
                                    <td style="border: 1px solid #f98800;" >Suboptimal Regimen</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'SX']">
                                    <td style="border: 1px solid #f98800;" >Drug‐Gender</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'TD']">
                                    <td style="border: 1px solid #f98800;" >Therapeutic</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'TN']">
                                    <td style="border: 1px solid #f98800;" >Laboratory Test Needed</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'TP']">
                                    <td style="border: 1px solid #f98800;" >Payer/Processor Question Code</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceReasonCode[text() = 'UD']">
                                    <td style="border: 1px solid #f98800;" >Duplicate Drug</td>
                                  </xsl:when>
                                </xsl:choose>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;" > &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                            <!--ProfessionalServiceCode-->
                            <xsl:variable name="ProfessionalServiceCode" select="ProfessionalServiceCode"/>
                            <xsl:choose>
                              <xsl:when test="$ProfessionalServiceCode !=''">
                                <!--<td style="border: 1px solid #f98800;"  >
                                  <xsl:value-of select="$ProfessionalServiceCode"/>
                                </td>-->
                                <xsl:choose>
                                  <xsl:when test="ProfessionalServiceCode[text() = '00']">
                                    <td style="border: 1px solid #f98800;" >00-No intervention</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'AS']">
                                    <td style="border: 1px solid #f98800;" >AS-Patient assessment</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'CC']">
                                    <td style="border: 1px solid #f98800;" >CC-Coordination of care</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'DE']">
                                    <td style="border: 1px solid #f98800;" >DE-Dosing evaluation/determination</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'DP']">
                                    <td style="border: 1px solid #f98800;" >DP-Dosage evaluated</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'FE']">
                                    <td style="border: 1px solid #f98800;" >FE-Formulary enforcement</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'GP']">
                                    <td style="border: 1px solid #f98800;" >GP-Generic product selection</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'M0']">
                                    <td style="border: 1px solid #f98800;" >M0-Prescriber consulted</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'MA']">
                                    <td style="border: 1px solid #f98800;" >MA-Medication administration</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'MB']">
                                    <td style="border: 1px solid #f98800;" >MB-Overriding benefit</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'MP']">
                                    <td style="border: 1px solid #f98800;" >MP-Patient will be monitored</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'MR']">
                                    <td style="border: 1px solid #f98800;" >MR-Medication review</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'PA']">
                                    <td style="border: 1px solid #f98800;" >PA-Previous patient tolerance</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'PE']">
                                    <td style="border: 1px solid #f98800;" >PE-Patient education/instruction</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'PH']">
                                    <td style="border: 1px solid #f98800;" >PH-Patient medication history</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'PM']">
                                    <td style="border: 1px solid #f98800;" >PM-Patient monitoring</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'P0']">
                                    <td style="border: 1px solid #f98800;" >P0-Patient consulted</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'PT']">
                                    <td style="border: 1px solid #f98800;" >PT-Perform laboratory test</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'R0']">
                                    <td style="border: 1px solid #f98800;" >R0-Pharmacist consulted other source</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'RT']">
                                    <td style="border: 1px solid #f98800;" >RT-Recommend laboratory test</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'SC']">
                                    <td style="border: 1px solid #f98800;" >SC-Self‐care consultation</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'SW']">
                                    <td style="border: 1px solid #f98800;" >SW-Literature search/review</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'TC']">
                                    <td style="border: 1px solid #f98800;" >TC-Payer/processor consulted</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'TH']">
                                    <td style="border: 1px solid #f98800;" >TH-Therapeutic product interchange</td>
                                  </xsl:when>
                                  <xsl:when test="ProfessionalServiceCode[text() = 'ZZ']">
                                    <td style="border: 1px solid #f98800;" >ZZ-Other Acknowledgement</td>
                                  </xsl:when>
                                </xsl:choose>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;" > &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                            <!--ServiceResultCode-->
                            <xsl:variable name="ServiceResultCode" select="ServiceResultCode"/>
                            <xsl:choose>
                              <xsl:when test="$ServiceResultCode !=''">
                                <!--<td class="physicianCapfont right">
                                   <xsl:value-of select="$ServiceResultCode"/> 
                                 </td>-->
                                <xsl:choose>
                                  <xsl:when test="ServiceResultCode[text() = '00']">
                                    <td style="border: 1px solid #f98800;" >00-Not Specified</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1A']">
                                    <td style="border: 1px solid #f98800;" >1A-Filled As Is,False Positive</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1B']">
                                    <td style="border: 1px solid #f98800;" >1B-Filled Prescription As Is</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1C']">
                                    <td style="border: 1px solid #f98800;">1C-Filled, With Different Dose</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1D']">
                                    <td style="border: 1px solid #f98800;">1D-Filled, With Different Directions</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1E']">
                                    <td style="border: 1px solid #f98800;">1E-Filled, With Different Drug</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1F']">
                                    <td style="border: 1px solid #f98800;">1F-Filled, With Different Quantity</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1G']">
                                    <td style="border: 1px solid #f98800;">1G-Filled, With Prescriber Approval</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1H']">
                                    <td style="border: 1px solid #f98800;">1H-Brand‐to‐Generic Change</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1J']">
                                    <td style="border: 1px solid #f98800;">1J-Rx‐to‐OTC Change</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '1K']">
                                    <td style="border: 1px solid #f98800;">1K-Filled with Different Dosage Form</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '2A']">
                                    <td style="border: 1px solid #f98800;">2A-Prescription Not Filled</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '2B']">
                                    <td style="border: 1px solid #f98800;">2B-Not Filled, Directions Clarified</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3A']">
                                    <td style="border: 1px solid #f98800;">3A-Recommendation Accepted</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3B']">
                                    <td style="border: 1px solid #f98800;">3B-Recommendation Not Accepted</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3C']">
                                    <td style="border: 1px solid #f98800;">3C-Discontinued Drug</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3D']">
                                    <td style="border: 1px solid #f98800;">3D-Regimen Changed</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3E']">
                                    <td style="border: 1px solid #f98800;">3E-Therapy Changed</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3F']">
                                    <td style="border: 1px solid #f98800;">3F-Therapy Changed‐cost increased acknowledged</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3G']">
                                    <td style="border: 1px solid #f98800;">3G-Drug Therapy Unchanged</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3H']">
                                    <td style="border: 1px solid #f98800;">3H-Follow‐Up/Report</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3J']">
                                    <td style="border: 1px solid #f98800;">3J-Patient Referral</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3K']">
                                    <td style="border: 1px solid #f98800;">3K-Instructions Understood</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3M']">
                                    <td style="border: 1px solid #f98800;">3M-Compliance Aid Provided</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '3N']">
                                    <td style="border: 1px solid #f98800;">3N-Medication Administered</td>
                                  </xsl:when>
                                  <xsl:when test="ServiceResultCode[text() = '4A']">
                                    <td style="border: 1px solid #f98800;">4A-Prescribed with acknowledgements</td>
                                  </xsl:when>
                                </xsl:choose>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;" > &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                            <!--CoAgentID-->
                            <xsl:variable name="CoAgentID" select="CoAgent/CoAgentID"/>
                            <xsl:choose>
                              <xsl:when test="$CoAgentID !=''">
                                <td style="border: 1px solid #f98800;">
                                  <xsl:value-of select="$CoAgentID"/>
                                </td>
                                <xsl:variable name="CoAgentQualifier" select="CoAgent/CoAgentQualifier"/>
                                <xsl:choose>
                                  <xsl:when test="$CoAgentQualifier !=''">
                                    <xsl:choose>
                                      <xsl:when test="CoAgentQualifier[text() = '00']">
                                        <td style="border: 1px solid #f98800;" >00-Not Specified</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '01']">
                                        <td style="border: 1px solid #f98800;" >01-Universal Product Code(UPC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '02']">
                                        <td style="border: 1px solid #f98800;" >02-Health Related Item(HRI)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '03']">
                                        <td style="border: 1px solid #f98800;" >03-National Drug Code(NDC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '04']">
                                        <td style="border: 1px solid #f98800;" >04-Health Industry Communications Council (HIBCC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '06']">
                                        <td style="border: 1px solid #f98800;" >06-Drug Use Review/Professional Pharmacy Service (DUR/PPS)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '07']">
                                        <td style="border: 1px solid #f98800;" >07-Common Procedure Terminology (CPT4)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '08']">
                                        <td style="border: 1px solid #f98800;" >08-Common Procedure Terminology (CPT5)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '09']">
                                        <td style="border: 1px solid #f98800;" >09-Health Care Financing Administration Common Procedural Coding System (HCPCS)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '10']">
                                        <td style="border: 1px solid #f98800;" >10-Pharmacy Practice Activity Classification (PPAC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '11']">
                                        <td style="border: 1px solid #f98800;" >11-National Pharmaceutical Product Interface Code (NAPPI)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '12']">
                                        <td style="border: 1px solid #f98800;" >12-Global Trade Identification Number(GTIN)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '14']">
                                        <td style="border: 1px solid #f98800;" >14-Medi-Span Product Line Generic Product Identifier (GPI)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '15']">
                                        <td style="border: 1px solid #f98800;" >15-First DataBank Formulation ID (GCN)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '16']">
                                        <td style="border: 1px solid #f98800;" >16-Micromedex/Medical Economics Generic Formulation Code(GFC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '17']">
                                        <td style="border: 1px solid #f98800;" >17-Medi-Span Product Line Drug Descriptor ID(DDID)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '18']">
                                        <td style="border: 1px solid #f98800;" >18-First DataBank SmartKey</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '19']">
                                        <td style="border: 1px solid #f98800;" >19-Micromedex/Medical Economics Generic Master (GM)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '20']">
                                        <td style="border: 1px solid #f98800;" >20-International Classification of Diseases (ICD9)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '21']">
                                        <td style="border: 1px solid #f98800;" >21-International Classification of Diseases-10-Clinical Modifications (ICD-10-CM)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '23']">
                                        <td style="border: 1px solid #f98800;" >23-National Criteria Care Institute (NCCI)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '24']">
                                        <td style="border: 1px solid #f98800;" >24-The Systematized Nomenclature of Medicine Clinical Terms (SNOMED)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '25']">
                                        <td style="border: 1px solid #f98800;" >25-Common Dental Terminology (CDT)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '26']">
                                        <td style="border: 1px solid #f98800;" >26-American Psychiatric Association Diagnostic Statistical Manual of Mental Disorders (DSMIV)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '27']">
                                        <td style="border: 1px solid #f98800;" >27-International Classification of Diseases-10-Procedure Coding System (ICD-10-PCS)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '28']">
                                        <td style="border: 1px solid #f98800;" >28-First DataBank Medication Name ID(FDB Med Name ID)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '29']">
                                        <td style="border: 1px solid #f98800;" >29-First DataBank Routed Medication ID (FDB Routed Med ID)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '30']">
                                        <td style="border: 1px solid #f98800;" >30-First DataBank Routed Dosage Form ID (FDB Routed Dosage Form Med ID)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '31']">
                                        <td style="border: 1px solid #f98800;" >31-First DataBank Medication ID (FDB Med ID)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '32']">
                                        <td style="border: 1px solid #f98800;" >32-First DataBank Clinical Formulation ID Sequence Number(GCN_SEQ_NO)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '33']">
                                        <td style="border: 1px solid #f98800;" >33-First DataBank Ingredient List ID(HICL_SEQ_NO)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '34']">
                                        <td style="border: 1px solid #f98800;" >34-Universal Product Number (UPN)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '35']">
                                        <td style="border: 1px solid #f98800;" >35-Logical Observation Identifier Names and Codes (LOINC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '36']">
                                        <td style="border: 1px solid #f98800;" >36-Representative National Drug Code(NDC)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '37']">
                                        <td style="border: 1px solid #f98800;" >37-American Hospital Formulary Service(AHFS)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '38']">
                                        <td style="border: 1px solid #f98800;" >38-RxNorm Semantic Clinical Drug (SCD)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '39']">
                                        <td style="border: 1px solid #f98800;" >39-RxNorm Semantic Branded Drug (SBD)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '40']">
                                        <td style="border: 1px solid #f98800;" >40-RxNorm Generic Package (GPCK)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '41']">
                                        <td style="border: 1px solid #f98800;" >41-RxNorm Branded Package (BPCK)</td>
                                      </xsl:when>
                                      <xsl:when test="CoAgentQualifier[text() = '99']">
                                        <td style="border: 1px solid #f98800;" >99-Other</td>
                                      </xsl:when>
                                    </xsl:choose>
                                  </xsl:when>
                                  <xsl:otherwise>
                                    <td style="border: 1px solid #f98800;">Not Specified</td>
                                  </xsl:otherwise>
                                </xsl:choose>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;"> &#160;</td>
                                <td style="border: 1px solid #f98800;"> &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                            <!--CoAgentQualifier-->

                            <!--ClinicalSignificanceCode-->
                            <xsl:variable name="ClinicalSignificanceCode" select="ClinicalSignificanceCode"/>
                            <xsl:choose>
                              <xsl:when test="$ClinicalSignificanceCode !=''">
                                <!--<td class="physicianCapfont right">
                                   <xsl:value-of select="$ClinicalSignificanceCode"/>
                                 </td>-->
                                <xsl:choose>
                                  <xsl:when test="ClinicalSignificanceCode[text() = '1']">
                                    <td style="border: 1px solid #f98800;" >Major</td>
                                  </xsl:when>
                                  <xsl:when test="ClinicalSignificanceCode[text() = '2']">
                                    <td style="border: 1px solid #f98800;" >Moderate</td>
                                  </xsl:when>
                                  <xsl:when test="ClinicalSignificanceCode[text() = '3']">
                                    <td style="border: 1px solid #f98800;" >Minor</td>
                                  </xsl:when>
                                  <xsl:when test="ClinicalSignificanceCode[text() = '9']">
                                    <td style="border: 1px solid #f98800;" >Undetermined</td>
                                  </xsl:when>
                                </xsl:choose>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;" > &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                            <!--AcknowledgementReason-->
                            <xsl:variable name="AcknowledgementReason" select="AcknowledgementReason"/>
                            <xsl:choose>
                              <xsl:when test="$AcknowledgementReason !=''">
                                <td style="border: 1px solid #f98800;" >
                                  <xsl:value-of select="$AcknowledgementReason"/>
                                </td>
                              </xsl:when>
                              <xsl:otherwise>
                                <td style="border: 1px solid #f98800;" > &#160;</td>
                              </xsl:otherwise>
                            </xsl:choose>
                          </tr>
                        </xsl:for-each>

                      </table>
                    </div>
                  </xsl:if>
            
          </div>
          <!--Medication Prescribed ENDs HERE-->

          <!--Medication Requested STARTs HERE-->
          <xsl:if test="Message/Body/RxChangeRequest/MedicationRequested">
            
            <div class="Mainheader">
              <table width="100%" cellspacing="0" cellpadding="0" >
                <tr >
                  <td style="padding-left:5px;">
                    Medication Requested
                  </td>
                </tr>
              </table>
            </div>
            
            <div class="Orgphysician" >

              <div class="OrgphysicianSUB" >
                <table class="SUBMed" width="100%" cellpadding="5" cellspacing="0" border="1px">
                  <tr >
                    <th class="SUBMedth" width="9%" >Drug</th>
                    <th class="SUBMedth" width="5%" >Quantity</th>
                    <th class="SUBMedth" width="9%" >Code Qty Qlf</th>
                    <th class="SUBMedth" width="13%" >Source Code</th>
                    <th class="SUBMedth" width="9%" >Potency Code</th>
                    <th class="SUBMedth" width="7%" >Duration</th>
                    <th class="SUBMedth" width="15%" >Directions</th>
                    <th class="SUBMedth" width="9%" >Notes</th>
                    <th class="SUBMedth" width="9%" >Ref Qlf</th>
                    <th class="SUBMedth" width="5%" >Refills</th>
                    <th class="SUBMedth" width="9%" >Substitution</th>
                  </tr>
                  <xsl:for-each select="Message/Body/RxChangeRequest/MedicationRequested">
                    <xsl:variable name="vColor">
                      <xsl:choose>
                        <xsl:when test="position() mod 2 = 1">
                          <xsl:text>#fde9d9</xsl:text>
                        </xsl:when>
                        <xsl:otherwise>#fff</xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    <tr class="odd" style="background-color: {$vColor}; ">
                      <xsl:variable name="DrugDescription" select="DrugDescription"/>
                      <xsl:choose>
                        <xsl:when test="$DrugDescription !=''">
                          <td   style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$DrugDescription"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td  style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="QtyValue" select="Quantity/Value"/>
                      <xsl:choose>
                        <xsl:when test="$QtyValue !=''">
                          <td style="border: 1px solid #f98800;"  >
                            <xsl:value-of select="$QtyValue"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td  style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="QtyQlf" select="Quantity/CodeListQualifier"/>
                      <xsl:choose>
                        <xsl:when test="$QtyQlf ='38'">
                          <td style="border: 1px solid #f98800;"  >
                            38&#160;(Original Quantity)
                          </td>
                        </xsl:when>
                        <xsl:when test="$QtyQlf ='40'">
                          <td style="border: 1px solid #f98800;"  >
                            40&#160;(Remaining Quantity)
                          </td>
                        </xsl:when>
                        <xsl:when test="$QtyQlf ='87'">
                          <td style="border: 1px solid #f98800;"  >
                            87&#160;(Quantity Received)
                          </td>
                        </xsl:when>
                        <xsl:when test="$QtyQlf ='QS'">
                          <td style="border: 1px solid #f98800;"  >
                            QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                          </td>
                        </xsl:when>
                        <xsl:when test="$QtyQlf ='CF'">
                          <td style="border: 1px solid #f98800;"  >
                            CF&#160;(Compound Final Quantity)
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td  style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                     
                      <td style="border: 1px solid #f98800;"  >
                        <xsl:value-of select="Quantity/UnitSourceCode"/>&#160;(NCPDP Drug quantity unit of measure terminology)
                      </td>
                       

                   
                      <td style="border: 1px solid #f98800;"  >
                        <xsl:value-of select="Quantity/PotencyUnitCode"/>
                      </td>
                       

                      <xsl:variable name="DaysSupply" select="DaysSupply"/>
                      <xsl:choose>
                        <xsl:when test="$DaysSupply !=''">
                          <td style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$DaysSupply"/> Days
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="Directions" select="Directions"/>
                      <xsl:choose>
                        <xsl:when test="$Directions !=''">
                          <td style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$Directions"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="Note" select="Note"/>
                      <xsl:choose>
                        <xsl:when test="$Note !=''">
                          <td style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$Note"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="RefQlf" select="Refills/Qualifier"/>
                      <xsl:choose>
                        <xsl:when test="$RefQlf ='R'">
                          <td style="border: 1px solid #f98800;" >R - Number of Refills </td>
                        </xsl:when>
                        <xsl:when test="$RefQlf ='A'">
                          <td style="border: 1px solid #f98800;" >A - Additional Refills Authorized </td>
                        </xsl:when>
                        <xsl:when test="$RefQlf ='P'">
                          <td style="border: 1px solid #f98800;" >P - Pharmacy Requested Refills </td>
                        </xsl:when>
                        <xsl:when test="$RefQlf ='PRN'">
                          <td style="border: 1px solid #f98800;" >PRN - As Needed </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;" >Transaction designation </td>
                        </xsl:otherwise>
                      </xsl:choose>
                      
                      <xsl:variable name="Refills" select="Refills/Value"/>
                      <xsl:choose>
                        <xsl:when test="$Refills !=''">
                          <td style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$Refills"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:choose>
                        <xsl:when test="Substitutions[text() = '0']">
                          <td style="border: 1px solid #f98800;" >Yes</td>
                        </xsl:when>
                        <xsl:when test="Substitutions[text() = '1']">
                          <td style="border: 1px solid #f98800;" >No</td>
                        </xsl:when>
                      </xsl:choose>
                    </tr>
                    <xsl:if test="Diagnosis">
                      <tr width="100%">
                        <td colspan ="11"> 
                        <xsl:variable name="vClr1">
                          <xsl:text>#fde9d9</xsl:text>
                        </xsl:variable>
                        <xsl:variable name="vClr2">
                          <xsl:text>#fff</xsl:text>
                        </xsl:variable>
                          <div style="padding:5px;" >
                          <table class="SUBMed" width="90%" cellpadding="5" cellspacing="0" border="1px">
                            <tr >
                              <th class="SUBMedth" >Clinical Info Qualifier</th>
                              <th class="SUBMedth" >Diagnosis</th>
                              <th class="SUBMedth" >Code List Qualifier</th>
                              <th class="SUBMedth" >ICD Code</th>
                            </tr>
                            <xsl:if test="Diagnosis/Primary">
                              <tr style="background-color: {$vClr1}; ">
                                <td  style="border: 1px solid #f98800;" rowspan="2">
                                  <xsl:choose>
                                    <xsl:when test="Diagnosis/ClinicalInformationQualifier='1'">
                                      <xsl:value-of select="Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                    </xsl:when>
                                    <xsl:when test="Diagnosis/ClinicalInformationQualifier='2'">
                                      <xsl:value-of select="Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                    </xsl:when>
                                  </xsl:choose>
                                </td>
                                <td  style="border: 1px solid #f98800;">Primary</td>
                                <td  style="border: 1px solid #f98800;">
                                  <xsl:choose>
                                    <xsl:when test="Diagnosis/Primary/Qualifier='DX'">
                                      <xsl:value-of select="Diagnosis/Primary/Qualifier"/> - ICD-9
                                    </xsl:when>
                                    <xsl:when test="Diagnosis/Primary/Qualifier='ABF'">
                                      <xsl:value-of select="Diagnosis/Primary/Qualifier"/> - ICD-10
                                    </xsl:when>
                                  </xsl:choose>
                                </td>
                                <td  style="border: 1px solid #f98800;">
                                  <xsl:value-of select="Diagnosis/Primary/Value"/>
                                </td>

                              </tr>
                            </xsl:if>
                            <xsl:if test="Diagnosis/Secondary">
                              <tr style="background-color: {$vClr2};">
                                <td  style="border: 1px solid #f98800;">Secondary</td>
                                <td  style="border: 1px solid #f98800;">
                                  <xsl:choose>
                                    <xsl:when test="Diagnosis/Secondary/Qualifier='DX'">
                                      <xsl:value-of select="Diagnosis/Secondary/Qualifier"/> - ICD-9
                                    </xsl:when>
                                    <xsl:when test="Diagnosis/Secondary/Qualifier='ABF'">
                                      <xsl:value-of select="Diagnosis/Secondary/Qualifier"/> - ICD-10
                                    </xsl:when>
                                  </xsl:choose>
                                </td>
                                <td  style="border: 1px solid #f98800;">
                                  <xsl:value-of select="Diagnosis/Secondary/Value"/>
                                </td>
                              </tr>
                            </xsl:if>
                          </table>
                        </div>
                        </td>
                      </tr>
                    </xsl:if>
                    
                  </xsl:for-each>
                </table>
              </div>
              <!--<xsl:if test="Message/Body/RxChangeRequest/MedicationRequested/DrugCoverageStatusCode">
                    <div class="OrgphysicianSUB" >
                      <table class="SUBMed" width="100%" cellpadding="0" cellspacing="0" border="1px">
                        <tr >
                          <th class="SUBMedth" >Drug Coverage Status Codes : </th>
                        </tr>
                        <xsl:for-each select="Message/Body/RxChangeRequest/MedicationRequested/DrugCoverageStatusCode">
                          <xsl:variable name="DrugCoverageStatusCode" select="Message/Body/RxChangeRequest/MedicationRequested/DrugCoverageStatusCode"/>
                              <xsl:choose>
                                <xsl:when test="$DrugCoverageStatusCode='PR'">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Preferred
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='AP']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Approved
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='PA']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Prior Authorization Required
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='NF']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Non Formulary
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='NR']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Not Reimbursed
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='DC']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Differential Co-Pay
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='UN']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Unknown
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:when test="DrugCoverageStatusCode[text()='ST']">
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                      Step Therapy Required
                                    </td>
                                  </tr>
                                </xsl:when>
                                <xsl:otherwise>
                                  <tr  width="100%">
                                    <td style="border: 1px solid #f98800;" >
                                  &#160;TEST
                                    </td>
                                  </tr>
                                </xsl:otherwise>
                              </xsl:choose>
                        </xsl:for-each>
                      </table>
                    </div>
              </xsl:if>-->
    
            </div>

          </xsl:if>
          <!--Medication Requested ENDs HERE-->

          <!--Observation STARTs HERE-->
          <xsl:if test="Message/Body/RxChangeRequest/Observation">

            <div class="Mainheader">
              <table width="100%" cellspacing="0" cellpadding="0" >
                <tr >
                  <td  style="padding-left:5px;">
                    Observation
                  </td>
                </tr>
              </table>
            </div>
            <div class="Orgphysician">
              <div class="OrgphysicianSUB" >
                <table class="SUBMed"  width="100%" cellpadding="0" cellspacing="0" border ="1px">
                  <tr>
                    <th class="SUBMedth" width="17%"  >Measurement Dimension</th>
                    <th class="SUBMedth" width="17%" >Measurement Value</th>
                    <th class="SUBMedth" width="17%" >Observation Date</th>
                    <th class="SUBMedth" width="17%" >Data Qualifier</th>
                    <th class="SUBMedth" width="17%" >Source Code</th>
                    <th class="SUBMedth" width="17%" >Unit Code</th>
                  </tr>
                  <xsl:for-each select="Message/Body/RxChangeRequest/Observation/Measurement">
                    <xsl:variable name="vColor">
                      <xsl:choose>
                        <xsl:when test="position() mod 2 = 1">
                          <xsl:text>#fde9d9</xsl:text>
                        </xsl:when>
                        <xsl:otherwise>#fff</xsl:otherwise>
                      </xsl:choose>
                    </xsl:variable>
                    <tr class="odd" style="background-color: {$vColor}; ">
                      <xsl:variable name="Dimension" select="Dimension"/>
                      <xsl:choose>
                        <xsl:when test="$Dimension !=''">
                          <!--<td class="physicianCapfont right">
                          <xsl:value-of select="$Dimension"/>
                        </td>-->
                          <xsl:choose>
                            <xsl:when test="Dimension[text() = 'HT']">
                              <td  style="border: 1px solid #f98800;" >Height</td>
                            </xsl:when>
                            <xsl:when test="Dimension[text() = 'WG']">
                              <td  style="border: 1px solid #f98800;">Weight</td>
                            </xsl:when>
                            <xsl:when test="Dimension[text() = 'ZZS']">
                              <td  style="border: 1px solid #f98800;" >Blood Pressure–Systolic</td>
                            </xsl:when>
                            <xsl:when test="Dimension[text() = 'ZZD']">
                              <td  style="border: 1px solid #f98800;" >Blood Pressure–Diastolic</td>
                            </xsl:when>
                          </xsl:choose>
                        </xsl:when>
                        <xsl:otherwise>
                          <td  style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="Value" select="Value"/>
                      <xsl:choose>
                        <xsl:when test="$Value !=''">
                          <td  style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$Value"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td  style="border: 1px solid #f98800;"> &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>


                      <xsl:variable name="ObservationDate" select="ObservationDate/Date"/>
                      <xsl:choose>
                        <xsl:when test="$ObservationDate !=''">
                          <td style="border: 1px solid #f98800;" >
                            <!--<xsl:value-of select="$ObservationDate"/>-->
                            <xsl:call-template name="formatDate">
                              <xsl:with-param name="datestr" select="translate(ObservationDate/Date,'-','/')"/>
                            </xsl:call-template>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;" > &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="MeasurementDataQualifier" select="MeasurementDataQualifier"/>
                      <xsl:choose>
                        <xsl:when test="$MeasurementDataQualifier !=''">
                          <td style="border: 1px solid #f98800;" >
                            <xsl:value-of select="$MeasurementDataQualifier"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;"> &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="MeasurementSourceCode" select="MeasurementSourceCode"/>
                      <xsl:choose>
                        <xsl:when test="$MeasurementSourceCode !=''">
                          <td style="border: 1px solid #f98800;">
                            <xsl:value-of select="$MeasurementSourceCode"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;"> &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>

                      <xsl:variable name="MeasurementUnitCode" select="MeasurementUnitCode"/>
                      <xsl:choose>
                        <xsl:when test="$MeasurementUnitCode !=''">
                          <td style="border: 1px solid #f98800;">
                            <xsl:value-of select="$MeasurementUnitCode"/>
                          </td>
                        </xsl:when>
                        <xsl:otherwise>
                          <td style="border: 1px solid #f98800;"> &#160;</td>
                        </xsl:otherwise>
                      </xsl:choose>
                    </tr>
                  </xsl:for-each>
                  <xsl:if test="Message/Body/RxChangeRequest/Observation/ObservationNotes">
                     <tr  width="100%">
                      <td style="border: 1px solid #f98800; backgroung-color:#fcdcc2;" class="left"  >Observation Notes : </td>
                      <td style="border: 1px solid #f98800;" class="physicianCapfont right" colspan="5"  >
                        <xsl:value-of select="Message/Body/RxChangeRequest/Observation/ObservationNotes"/>&#160;
                      </td>
                    </tr>
                  </xsl:if>
                </table>
              </div>
             </div>
          </xsl:if>
          <!--Observation ENDs HERE-->

          <!--BenefitsCoordination STARTs HERE-->
          <xsl:if test="Message/Body/RxChangeRequest/BenefitsCoordination">
            <div class="Mainheader">
              <table width="100%" cellspacing="0" cellpadding="0" >
                <tr  width="100%">
                  <td width="80%" style="padding-left:5px;">
                    Benefits Coordination
                  </td>
                </tr>
              </table>
            </div>
            <div class="Orgphysician">
              <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
                <tr width="100%" >
                  <td class="left"  >Card Holder Name : </td>
                  <td  colspan="18"  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/CardHolderName/LastName"/>&#160;<xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/CardHolderName/FirstName"/>
                  </td>
                  <td class="left"  >Card Holder Id : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/CardholderID"/>&#160;
                  </td>
                  <td class="left"  >Group Id  : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/GroupID"/>&#160;
                  </td>
                </tr>
                <tr  >
                  <td class="left"  >Payer Name  : </td>
                  <td colspan="18"  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/PayerName"/>&#160;
                  </td>
                  <td class="left"  >Payer Id : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/PayerIdentification/PayerID"/>&#160;
                  </td>
                  <td class="left"  >BIN Location Number : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/PayerIdentification/BINLocationNumber"/>&#160;
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left" >Payer Mutually Defined Id  : </td>
                  <td colspan="18"   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/PayerIdentification/MutuallyDefined"/>&#160;
                  </td>
                  <td class="left"  >Processor Identification Number : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/BenefitsCoordination/PayerIdentification/ProcessorIdentificationNumber"/>&#160;
                  </td>
                  <td class="left"  >&#160;</td>
                  <td  class="physicianCapfont right">
                    &#160;
                  </td>
                </tr>
              </table>
            </div>

          </xsl:if>
          <!--BenefitsCoordination ENDs HERE-->

          <!--	<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Writtren Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Observation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Measurement : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dimension : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Observation/ObservationNotes"/>
								</td>
							</tr>-->

          <!--	<tr  width="100%">
								<td  class="sub-physicianCapfont"> Supervisor </td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Identification : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >NPI : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeRequest/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->

          <!--Patient INFO STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Patient Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <!--<tr  width="100%">
                <td class="left"  >Patient Relationship : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/PatientRelationship"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Identification/FileID"/>
                  </td>
                  <td class="left"  >Medicare Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Identification/MedicareNumber"/>
                  </td>
                </tr>-->
              <!--<xsl:for-each select="Message/Body/RxChangeRequest/Patient/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>-->

              <tr  width="100%">
                <td class="left"  >Patient Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/LastName"/>&#160;<xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/FirstName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Name/Prefix"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="Message/Body/RxChangeRequest/Patient/Gender"/>-->
                  <xsl:variable name="sex" select="Message/Body/RxChangeRequest/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>
                </td>
                <!--</tr>
              <tr  width="100%">-->
                <td > Date of Birth : </td>
                <!--<td  class="physicianCapfont right">
                  -->
                <!--<xsl:variable name="newtext" select="translate($text,'a','b')"/>-->
                <!--
                     <xsl:call-template name="formatDate">
                      -->
                <!--<xsl:with-param name="datestr" select="Message/Body/RxChangeRequest/Patient/DateOfBirth/Date"/>-->
                <!--
                       <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/Patient/DateOfBirth/Date,'-','/')"/>
                    </xsl:call-template>
                  -->
                <!--<xsl:value-of select="Message/Body/RxChangeRequest/Patient/DateOfBirth/Date"/>-->
                <!-- 
                </td>-->
                <xsl:variable name="DateOfBirth" select="Message/Body/RxChangeRequest/Patient/DateOfBirth/Date"/>
                <xsl:choose>
                  <xsl:when test="$DateOfBirth !=''">
                    <td class="physicianCapfont right">
                      <xsl:call-template name="formatDate">
                        <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeRequest/Patient/DateOfBirth/Date,'-','/')"/>
                      </xsl:call-template>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="physicianCapfont right"> &#160;</td>
                  </xsl:otherwise>
                </xsl:choose>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/State"/>
                </td>
                <!--</tr>
              <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Patient/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <xsl:for-each select="Message/Body/RxChangeRequest/Patient/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                    <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                    <td class="left"  >Telephone (TE) : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Number"/>
                    </td>
                    </tr>
                    </xsl:when>
                  
                    <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                      </tr>
                    </xsl:when>
                  
                </xsl:choose>
                </xsl:for-each>
            </table>
          </div>
          <!--Patient INFO ENDs HERE-->

          <!--Pharmacy INFO STARTS here-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Pharmacy Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%">
              <!--<tr  width="100%">
              <td class="left" > NCPDPID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Identification/NPI"/>
                </td>
                <xsl:variable name="NCPDPID" select="Message/Body/RxChangeRequest/Pharmacy/Identification/NCPDPID"/>
                <xsl:choose>
                  <xsl:when test="$NCPDPID !=''">
                    <td class="left"  >NCPDPID : </td>
                    <td class="physicianCapfont right">
                      <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Identification/NCPDPID"/>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="physicianCapfont right"> &#160;</td>
                  </xsl:otherwise>
                </xsl:choose>
             
                <!--<xsl:for-each select="Message/Body/RxChangeRequest/Pharmacy/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>-->

                <!--<xsl:for-each select="Message/Body/RxChangeRequest/Pharmacy/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:variable name="TagVal" select="name()"/>
                    <xsl:choose>
                      <xsl:when test="$TagVal='NPI'">
                        <xsl:value-of select="concat(name(),' :')"/>
                        <td  class="physicianCapfont right">
                          <xsl:value-of select="current()"/>
                        </td>
                      </xsl:when>
                    </xsl:choose>
                  </td>

                </tr>
              </xsl:for-each>-->

              </tr>
              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Specialty"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Pharmacist/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Pharmacist/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Pharmacist/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Pharmacist/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Pharmacist/Prefix"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left" >Pharmacy Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/State"/>
                </td>
                <!--</tr>
                 <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Pharmacy/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <xsl:for-each select="Message/Body/RxChangeRequest/Pharmacy/CommunicationNumbers/Communication">
                  <xsl:variable name="Qualifier" select="Qualifier"/>

                  <xsl:choose>
                    <xsl:when test="$Qualifier='TE'" >

                      <td class="left"  >Telephone (TE) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/> 
                      </td>
                      <!--</tr>-->
                    </xsl:when>

                    <xsl:when test="$Qualifier='FX'">
                      <!--<tr  width="100%">-->
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>

                    </xsl:when>

                  </xsl:choose>

                </xsl:for-each>
              </tr>
            </table>
          </div>
          <!--Pharmacy INFO ENDS here-->

          <!--Provider INFO STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Provider Information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <!--<tr  width="100%">
                  <td class="left"  >NPI : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Identification/NPI"/>
                  </td>
                  <td class="left" >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >State License Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Identification/StateLicenseNumber"/>
                  </td>
                </tr>-->
              <!--<tr  width="100%">
                <xsl:for-each select="Message/Body/RxChangeRequest/Prescriber/Identification/*">
                  <td class="left" >
                    <xsl:variable name="TagVal" select="name()"/>
                    <xsl:choose>
                      <xsl:when test="$TagVal='NPI'">
                        <xsl:value-of select="concat(name(),' :')"/>
                        <td  class="physicianCapfont right">
                          <xsl:value-of select="current()"/>
                        </td>
                      </xsl:when>
                    </xsl:choose>
                  </td>
                </xsl:for-each>
              </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Identification/NPI"/>
                </td>
                <td class="left"  >DEA Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Identification/DEANumber"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Specialty"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/ClinicName"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Provider Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/LastName"/>&#160; <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/FirstName"/>&#160; <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/MiddleName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/State"/>
                </td>
                <!--</tr>
                 <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->

              <tr  width="100%">
              <xsl:for-each select="Message/Body/RxChangeRequest/Prescriber/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                  <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                      <td class="left"  >Telephone (TE) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                  </xsl:when>

                  <xsl:when test="$Qualifier='FX'">
                      <td class="left"  >Fax (FX) : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>
              </tr>


              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeRequest/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->
            </table>
          </div>
          <!--Provider INFO ENDs HERE-->
        </xsl:if>

      </body>
    </html>
  </xsl:template>

  <xsl:template name="ReasonCodes">
    <xsl:choose>
      <xsl:when test="text() = 'AA'">
        * Patient unknown to the prescriber
      </xsl:when>
      <xsl:when test="text() = 'AB'">
        * Patient never under provider care
      </xsl:when>
      <xsl:when test="text() = 'AC'">
        * Patient no longer under provider care
      </xsl:when>
      <xsl:when test="text() = 'AD'">
        * Refill too soon
      </xsl:when>
      <xsl:when test="text() = 'AE'">
        * Medication never prescribed for patient
      </xsl:when>
      <xsl:when test="text() = 'AF'">
        * Patient should contact provider
      </xsl:when>
      <xsl:when test="text() = 'AG'">
        * Refill not appropriate
      </xsl:when>
      <xsl:when test="text() = 'AH'">
        * Patient has picked up prescription
      </xsl:when>
      <xsl:when test="text() = 'AJ'">
        * Patient has picked up partial fill of prescription
      </xsl:when>
      <xsl:when test="text() = 'AK'">
        * Patient has not picked up prescription, drug returned to stock
      </xsl:when>
      <xsl:when test="text() = 'AL'">
        * Change not appropriate
      </xsl:when>
      <xsl:when test="text() = 'AM'">
        * Patient needs appointment
      </xsl:when>
      <xsl:when test="text() = 'AN'">
        * Prescriber not associated with this practice or location
      </xsl:when>
      <xsl:when test="text() = 'AO'">
        * No attempt will be made to obtain Prior Authorization
      </xsl:when>
      <xsl:when test="text() = 'AP'">
        * Request already responded to by other means (e.g. phone or fax)
      </xsl:when>
      <xsl:when test="text() = 'AQ'">
        * More medication history available
      </xsl:when>
      <xsl:otherwise>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="NarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/RefillResponse/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/RefillResponse/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RxChangeRequestNarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RxFillMedicationDispensedNarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/RxFill/MedicationDispensed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/RxFill/MedicationDispensed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RxFillMedicationPrescribedNarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/RxFill/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="Refillsdata">
    <xsl:choose>
      <xsl:when test="Message/Body/RefillResponse/MedicationDispensed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('1 current dispense', ' + 0 refills')" />
      </xsl:when>
      <xsl:when test="Message/Body/RefillResponse/MedicationDispensed/Refills/Value[text() = '1']">
        <xsl:value-of select="concat('1 current dispense', ' + 0 refill')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="refvalue">
          <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Refills/Value"/>
        </xsl:variable>
        <xsl:value-of select="concat('1 current dispense + ',$refvalue -1 ,' refills')" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RxChangeRequestRefillsdata">
    <xsl:choose>
      <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('1 current dispense',' + ',' 0 refills')" />
      </xsl:when>
      <xsl:when test="Message/Body/RxChangeRequest/MedicationPrescribed/Refills/Value[text() = '1']">
        <xsl:value-of select="concat('1 current dispense',' + ',' 1 refill')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat('1 current dispense + ', Message/Body/RxChangeRequest/MedicationPrescribed/Refills/Value,' more refills')" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="formatDate">
    <xsl:param name="datestr" />

    <xsl:variable name="datepart">
      <!--<xsl:value-of select="substring($datestr,8,9)" />-->
      <xsl:value-of select="substring-after($datestr, '/')"/>
    </xsl:variable>

    <xsl:variable name="yearpart">
      <!--<xsl:value-of select="substring($datestr,6,7)" />-->
      <xsl:value-of select="substring-before($datestr, '/')"/>
    </xsl:variable>

    <xsl:value-of select="$datepart" />
    <xsl:value-of select="'/'" />
    <xsl:value-of select="$yearpart" />

    <!--input format ddmmyyyy 
     output format dd/mm/yyyy-->
    <!--

    -->
    <!--<xsl:variable name="dd">
      -->
    <!--<xsl:value-of select="substring($datestr,8,9)" />-->
    <!--
    </xsl:variable>
       
    <xsl:variable name="mm">
      -->
    <!--<xsl:value-of select="substring($datestr,6,7)" />-->
    <!--
    </xsl:variable>-->
    <!--
   

    -->
    <!--<xsl:variable name="yyyy">
      <xsl:value-of select="substring($datestr,1,4)" />
    </xsl:variable>
    <xsl:value-of select="$yyyy" />-->
    <!--
    
    -->
    <!--<xsl:value-of select="$dd" />
    <xsl:value-of select="'/'" />
    <xsl:value-of select="$mm" />-->
    <!--
    -->
    <!--<xsl:value-of select="'/'" />
    <xsl:value-of select="$yyyy" />
    <xsl:value-of select="$datestr" />-->


  </xsl:template>


  </xsl:stylesheet>
