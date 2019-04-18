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

		.Orgphysician
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        /* font-weight:bold*/
        color:#0069aa;
        text-decoration:none;
        background-color:#effaff;
        padding-bottom:5px;
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

				</style>
			</head>
      <body>
        
           <xsl:if test="Message/Body/RxChangeResponse">

          <!--Medication Prescribed STARTs HERE-->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Drug Summary
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
               <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

                 <tr  width="100%">
                   <td class="left"  >Drug Description : </td>
                   <td colspan="6" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugDescription"/>
                   </td>
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>-->
                 <tr  width="100%">
                   <td class="left"  >Patient Directions : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Directions"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Drug Quantity : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Quantity/Value"/>&#160;
                     <xsl:value-of select="Message/PotencyDescription/MP"/>
                     <xsl:value-of select="PotencyDescription/MP"/>
                   </td>
                   <td class="left"  >Drug Duration : </td>
                   <td  class="physicianCapfont right">
                     <xsl:variable name="Days" select="Message/Body/RxChangeResponse/MedicationPrescribed/DaysSupply"/>
                     <xsl:choose>
                       <xsl:when test="$Days!=' '">
                         <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DaysSupply"/>&#160;Days
                       </xsl:when>
                     </xsl:choose>
                   </td>
                   <!--<td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Quantity/CodeListQualifier"/>
                </td>-->
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
               
              </tr>-->
                 <tr  width="100%">
                   <td class="left"  >Pharmacy Notes : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Note"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Refill Qualifier : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Refills/Qualifier"/>
                   </td>
                   <td class="left"  >Refills :  </td>
                   <td  class="physicianCapfont right">
                     <!--<xsl:call-template name="RxChangeRefillsData"/>-->
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Refills/Value"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Substitutions : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="RxChangeResponseNarcoticFlag"/>
                   </td>

                   <td class="left"  >Written Date : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="formatDate">
                       <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeResponse/MedicationPrescribed/WrittenDate/Date,'-','/')"/>
                     </xsl:call-template>
                   </td>
                 </tr>
                 
                 <xsl:if test="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorization">
                   <tr>
                   <td class="left"  >Prior Authorization # : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorization/Value"/>
                   </td>
                   <td class="left"  >Prior Authorization Status : </td>
                     <xsl:variable name="PriorAuthorizationStatus" select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
                     <xsl:choose>
                       <xsl:when test="$PriorAuthorizationStatus='A'">
                         <td  class="physicianCapfont right">Approved</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='D'">
                         <td  class="physicianCapfont right">Denied</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='F'">
                         <td  class="physicianCapfont right">Deferred</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='N'">
                         <td  class="physicianCapfont right">Not Required</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='R'">
                         <td  class="physicianCapfont right">Requested</td>
                       </xsl:when>
                       <xsl:otherwise>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
                         </td>
                       </xsl:otherwise>
                     </xsl:choose>
                 </tr>
                 </xsl:if>
               </table>
             </div>                                                
          <!--Medication Prescribed ENDs HERE-->

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
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Identification/NPI"/>
                </td>
              </tr>
              
              <!--<tr  width="100%">
                <xsl:for-each select="Message/Body/RxChangeResponse/Prescriber/Identification/*">
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

              <!--<xsl:for-each select="Message/Body/RxChangeResponse/Prescriber/Identification/*">
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
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Specialty"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/ClinicName"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/LastName"/>&#160; <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/FirstName"/>&#160; <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/MiddleName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/State"/>
                </td>
              </tr>
              
                <!--</tr>
                 <tr  width="100%">-->
                
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->                
              
              <tr>
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/Address/ZipCode"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->

              <xsl:for-each select="Message/Body/RxChangeResponse/Prescriber/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                      <td class="left"  >Phone : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>

              <xsl:for-each select="Message/Body/RxChangeResponse/Prescriber/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>
            </table>
          </div>
          <!--Provider INFO ENDs HERE-->

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
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/PatientRelationship"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Identification/FileID"/>
                  </td>
                  <td class="left"  >Medicare Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Identification/MedicareNumber"/>
                  </td>
                </tr>-->
              <!--<xsl:for-each select="Message/Body/RxChangeResponse/Patient/Identification/*">
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
                <td class="left"  >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/LastName"/>&#160;<xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/FirstName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Name/Prefix"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="Message/Body/RxChangeResponse/Patient/Gender"/>-->
                  <xsl:variable name="sex" select="Message/Body/RxChangeResponse/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>
                  <!--<xsl:value-of select="Message/Body/RxChangeResponse/Patient/Gender"/>--><!--
                  <xsl:variable name="sex" select="Message/Body/RxChangeResponse/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>-->
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
                <!--<xsl:with-param name="datestr" select="Message/Body/RxChangeResponse/Patient/DateOfBirth/Date"/>-->
                <!--
                       <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeResponse/Patient/DateOfBirth/Date,'-','/')"/>
                    </xsl:call-template>
                  -->
                <!--<xsl:value-of select="Message/Body/RxChangeResponse/Patient/DateOfBirth/Date"/>-->
                <!-- 
                </td>-->
                <xsl:variable name="DateOfBirth" select="Message/Body/RxChangeResponse/Patient/DateOfBirth/Date"/>
                <xsl:choose>
                  <xsl:when test="$DateOfBirth !=''">
                    <td class="physicianCapfont right">
                      <xsl:call-template name="formatDate">
                        <xsl:with-param name="datestr" select="translate(Message/Body/RxChangeResponse/Patient/DateOfBirth/Date,'-','/')"/>
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
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Address/State"/>
                </td>                                              
              </tr>

              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Patient/Address/ZipCode"/>
                </td>              
              </tr>
              
              <xsl:for-each select="Message/Body/RxChangeResponse/Patient/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                      <td class="left"  >Phone : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>
              
              <xsl:for-each select="Message/Body/RxChangeResponse/Patient/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
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
                    <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Identification/NPI"/>
                </td>                
                

              </tr>                            
              <tr  width="100%">
                <td class="left" >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Address/State"/>
                </td>                            
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RxChangeResponse/Pharmacy/Address/ZipCode"/>
                </td>               
              </tr>

              <xsl:for-each select="Message/Body/RxChangeResponse/Pharmacy/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                      <td class="left"  >Phone : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>
              
              <xsl:for-each select="Message/Body/RxChangeResponse/Pharmacy/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:for-each>
            </table>
          </div>
          <!--Pharmacy INFO ENDS here-->
          
          <!--Observation STARTs HERE-->
          <xsl:if test="Message/Body/RxChangeResponse/Observation">

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
                  <xsl:for-each select="Message/Body/RxChangeResponse/Observation/Measurement">
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
                  <xsl:if test="Message/Body/RxChangeResponse/Observation/ObservationNotes">
                    <tr  width="100%">
                      <td style="border: 1px solid #f98800; backgroung-color:#fcdcc2;" class="left"  >Observation Notes : </td>
                      <td style="border: 1px solid #f98800;" class="physicianCapfont right" colspan="5"  >
                        <xsl:value-of select="Message/Body/RxChangeResponse/Observation/ObservationNotes"/>&#160;
                      </td>
                    </tr>
                  </xsl:if>
                </table>
              </div>
            </div>
          </xsl:if>
          <!--Observation ENDs HERE-->

          <!--BenefitsCoordination STARTs HERE-->
          <xsl:if test="Message/Body/RxChangeResponse/BenefitsCoordination">
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
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/CardHolderName/LastName"/>&#160;<xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/CardHolderName/FirstName"/>
                  </td>
                  <td class="left"  >Card Holder Id : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/CardholderID"/>&#160;
                  </td>
                  <td class="left"  >Group Id  : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/GroupID"/>&#160;
                  </td>
                </tr>
                <tr  >
                  <td class="left"  >Payer Name  : </td>
                  <td colspan="18"  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/PayerName"/>&#160;
                  </td>
                  <td class="left"  >Payer Id : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/PayerIdentification/PayerID"/>&#160;
                  </td>
                  <td class="left"  >BIN Location Number : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/PayerIdentification/BINLocationNumber"/>&#160;
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left" >Payer Mutually Defined Id  : </td>
                  <td colspan="18"   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/PayerIdentification/MutuallyDefined"/>&#160;
                  </td>
                  <td class="left"  >Processor Identification Number : </td>
                  <td   class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RxChangeResponse/BenefitsCoordination/PayerIdentification/ProcessorIdentificationNumber"/>&#160;
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
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Written Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
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
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Observation/ObservationNotes"/>
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
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->

          <!--<div id="Main-Container" style="margin-top:5px;padding-right:2px;">
						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										Message
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader" width="100%">
							<tr  width="100%">
								<td >
										 Release :
								</td>
								<td class="left"  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
						</table>
					</div>

					<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
									Header	
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader"  width="100%">
							<tr  width="100%">
								<td class="left" > To :</td>
								<td   class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/PrescriberOrderNumber"/>
								</td>
							</tr>
						</table>
					</div> 

						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										 Refill Response
								</td>
							</tr>
						</table>
					</div> 

					<div class="Orgphysician">
						<table class="orangeheader" width="100%"  >
								<tr  width="100%">
								<td  class="sub-physicianCapfont"> Request </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Return Receipt : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RxChangeResponse/Response/Denied/DenialReason"/>
								</td>
							</tr> -->

          

          
        </xsl:if>
        
           <xsl:if test="Message/Body/NewRx">


             <div class="Mainheader">
               <table width="100%" cellspacing="0" cellpadding="0" >
                 <tr  width="100%">
                   <td width="80%" style="padding-left:5px;">
                     Drug Summary
                   </td>
                 </tr>
               </table>
             </div>
             <div class="Orgphysician">
               <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

                 <tr  width="100%">
                   <td class="left"  >Drug Description : </td>
                   <td colspan="6" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugDescription"/>
                   </td>
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>-->

                 <tr  width="100%">
                   <td class="left"  >Patient Directions : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Directions"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Drug Quantity : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Quantity/Value"/>&#160;
                     <xsl:value-of select="Message/PotencyDescription/MP"/>
                   </td>
                   <!--<td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Quantity/CodeListQualifier"/>
                </td>-->
                   <!--</tr>-->

                   <!--<tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>-->

                   <!--<tr  width="100%">-->
                   <td class="left"  >Drug Duration : </td>
                   <td  class="physicianCapfont right">
                     <xsl:variable name="Days" select="Message/Body/NewRx/MedicationPrescribed/DaysSupply"/>
                     <xsl:choose>
                       <xsl:when test="$Days!=' '">
                         <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DaysSupply"/> Days
                       </xsl:when>
                     </xsl:choose>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Pharmacy Notes : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Note"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Refill Qualifier : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Refills/Qualifier"/>
                   </td>
                   <td class="left"  ></td>
                   <!--the caption was Value :-->
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="NewRxRefillsdata"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Substitution : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="NewRxNarcoticFlag"/>
                   </td>
                   <!--</tr>

              <tr  width="100%">-->
                   <td class="left"  >Written Date : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="formatDate">
                       <!--<xsl:with-param name="datestr" select="Message/Body/NewRx/Patient/DateOfBirth/Date"/>-->
                       <xsl:with-param name="datestr" select="translate(Message/Body/NewRx/MedicationPrescribed/WrittenDate/Date,'-','/')"/>
                     </xsl:call-template>
                     <!--<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/WrittenDate/Date"/>-->
                   </td>
                 </tr>

                 <xsl:if test="Message/Body/NewRx/MedicationPrescribed/PriorAuthorization">
                   <tr>
                     <td class="left"  >Prior Authorization # : </td>
                     <td  class="physicianCapfont right">
                       <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorization/Value"/>
                     </td>
                     <td class="left"  >Prior Authorization Status : </td>
                     <xsl:variable name="PriorAuthorizationStatus" select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorizationStatus"/>
                     <xsl:choose>
                       <xsl:when test="$PriorAuthorizationStatus='A'">
                         <td  class="physicianCapfont right">Approved</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='D'">
                         <td  class="physicianCapfont right">Denied</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='F'">
                         <td  class="physicianCapfont right">Deferred</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='N'">
                         <td  class="physicianCapfont right">Not Required</td>
                       </xsl:when>
                       <xsl:when test="$PriorAuthorizationStatus='R'">
                         <td  class="physicianCapfont right">Requested</td>
                       </xsl:when>
                       <xsl:otherwise>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorizationStatus"/>
                         </td>
                       </xsl:otherwise>
                     </xsl:choose>
                   </tr>
                 </xsl:if>
               </table>
             </div>

             <!--	<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Written Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
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
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Observation/ObservationNotes"/>
								</td>
							</tr>-->
             
          
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
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/Identification/NPI"/>
                  </td>
                  <td class="left" >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >State License Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/Identification/StateLicenseNumber"/>
                  </td>
                </tr>-->
                            
              <xsl:for-each select="Message/Body/NewRx/Prescriber/Identification/*">
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
              </xsl:for-each>



              <!--<xsl:for-each select="Message/Body/NewRx/Prescriber/Identification/*">
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
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Specialty"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/ClinicName"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/LastName"/>&#160; <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/FirstName"/>&#160; <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/MiddleName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->


              <xsl:for-each select="Message/Body/NewRx/Prescriber/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                      <td class="left"  >Phone : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>

                  <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>

                </xsl:choose>
              </xsl:for-each>
              
              
              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->
            </table>
          </div>

          <!--	<tr  width="100%">
								<td  class="sub-physicianCapfont"> Supervisor </td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Identification : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >NPI : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->
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
                  <xsl:value-of select="Message/Body/NewRx/Patient/PatientRelationship"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Patient/Identification/FileID"/>
                  </td>
                  <td class="left"  >Medicare Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Patient/Identification/MedicareNumber"/>
                  </td>
                </tr>-->
              <!--<xsl:for-each select="Message/Body/NewRx/Patient/Identification/*">
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
                <td class="left"  >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Name/LastName"/>&#160;<xsl:value-of select="Message/Body/NewRx/Patient/Name/FirstName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Name/Prefix"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="Message/Body/NewRx/Patient/Gender"/>-->
                  <xsl:variable name="sex" select="Message/Body/NewRx/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>
                </td>
              <!--</tr>
              <tr  width="100%">-->
                <td > Date of Birth : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:variable name="newtext" select="translate($text,'a','b')"/>-->
                     <xsl:call-template name="formatDate">
                      <!--<xsl:with-param name="datestr" select="Message/Body/NewRx/Patient/DateOfBirth/Date"/>-->
                       <xsl:with-param name="datestr" select="translate(Message/Body/NewRx/Patient/DateOfBirth/Date,'-','/')"/>
                    </xsl:call-template>
                  <!--<xsl:value-of select="Message/Body/NewRx/Patient/DateOfBirth/Date"/>--> 
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Patient/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <xsl:for-each select="Message/Body/NewRx/Patient/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                    <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                    <td class="left"  >Phone : </td>
                    <td  class="physicianCapfont right">
                      <xsl:value-of select="Number"/>
                    </td>
                    </tr>
                    </xsl:when>
                  
                    <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                      </tr>
                    </xsl:when>
                  
                </xsl:choose>
                </xsl:for-each>
               </table>
          </div>



             <!--<div id="Main-Container" style="margin-top:5px;padding-right:2px;">
						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										Message
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader" width="100%">
							<tr  width="100%">
								<td >
										 Release :
								</td>
								<td class="left"  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
						</table>
					</div>

					<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
									Header	
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader"  width="100%">
							<tr  width="100%">
								<td class="left" > To :</td>
								<td   class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/PrescriberOrderNumber"/>
								</td>
							</tr>
						</table>
					</div> 

						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										 Refill Response
								</td>
							</tr>
						</table>
					</div> 

					<div class="Orgphysician">
						<table class="orangeheader" width="100%"  >
								<tr  width="100%">
								<td  class="sub-physicianCapfont"> Request </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Return Receipt : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/NewRx/Response/Denied/DenialReason"/>
								</td>
							</tr> -->
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
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>-->
                 <tr  width="100%">
                   <td class="left"  >NPI : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Identification/NPI"/>
                   </td>
                   <!--<xsl:for-each select="Message/Body/NewRx/Pharmacy/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>-->

                   <!--<xsl:for-each select="Message/Body/NewRx/Pharmacy/Identification/*">
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
                  <xsl:value-of select="Message/Body/NewRx/Pharmacy/Specialty"/>
                </td>
              </tr>-->

                 <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Pharmacist/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Pharmacist/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Pharmacist/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Pharmacist/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/NewRx/Pharmacy/Pharmacist/Prefix"/>
                  </td>
                </tr>-->
                 <tr  width="100%">
                   <td class="left" >Name : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/StoreName"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Address Line 1 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/AddressLine1"/>
                   </td>
                   <td class="left"  > Address Line 2 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/AddressLine2"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >City : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/City"/>
                   </td>
                   <td class="left"  > State : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/State"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Zip Code : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/ZipCode"/>
                   </td>
                   <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
                 </tr>

                 <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Pharmacy/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/NewRx/Pharmacy/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->


                 <xsl:for-each select="Message/Body/NewRx/Pharmacy/CommunicationNumbers/Communication">
                   <xsl:variable name="Qualifier" select="Qualifier"/>
                   <xsl:choose>
                     <xsl:when test="$Qualifier='TE'">
                       <tr  width="100%">
                         <td class="left"  >Phone : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                     <xsl:when test="$Qualifier='FX'">
                       <tr  width="100%">
                         <td class="left"  >Fax : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                   </xsl:choose>
                 </xsl:for-each>

               </table>
             </div>

        </xsl:if>
     
           <xsl:if test="Message/Body/RefillResponse">


          <!--<div id="Main-Container" style="margin-top:5px;padding-right:2px;">
						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										Message
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader" width="100%">
							<tr  width="100%">
								<td >
										 Release :
								</td>
								<td class="left"  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
						</table>
					</div>

					<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
									Header	
								</td>
							</tr>
						</table>
					</div>

					<div class="Orgphysician">
						<table class="orangeheader"  width="100%">
							<tr  width="100%">
								<td class="left" > To :</td>
								<td   class="physicianCapfont right">
									<xsl:value-of select="Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="Message/Header/PrescriberOrderNumber"/>
								</td>
							</tr>
						</table>
					</div> 

						<div class="Mainheader">
						<table width="100%" cellspacing="0" cellpadding="0" >
							<tr  width="100%">
								<td width="80%" style="padding-left:5px;">
										 Refill Response
								</td>
							</tr>
						</table>
					</div> 

					<div class="Orgphysician">
						<table class="orangeheader" width="100%"  >
								<tr  width="100%">
								<td  class="sub-physicianCapfont"> Request </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Return Receipt : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Response/Denied/DenialReason"/>
								</td>
							</tr> -->
             <!--Drug Summary Start-->
             <div class="Mainheader">
               <table width="100%" cellspacing="0" cellpadding="0" >
                 <tr  width="100%">
                   <td width="80%" style="padding-left:5px;">
                     Drug Summary
                   </td>
                 </tr>
               </table>
             </div>
             <div class="Orgphysician">
               <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

                 <tr  width="100%">
                   <td class="left"  >Drug Description : </td>
                   <td colspan="6" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
                   </td>
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>-->
                 <tr  width="100%">
                   <td class="left"  >Patient Directions : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Directions"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Drug Quantity : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/Value"/>&#160;
                     <xsl:value-of select="Message/PotencyDescription/MP"/>
                     <xsl:value-of select="PotencyDescription/MP"/>
                   </td>
                   <td class="left"  >Drug Duration : </td>
                   <td  class="physicianCapfont right">
                     <xsl:variable name="Days" select="Message/Body/RefillResponse/MedicationDispensed/DaysSupply"/>
                     <xsl:choose>
                       <xsl:when test="$Days!=' '">
                         <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DaysSupply"/> Days
                       </xsl:when>
                     </xsl:choose>
                   </td>
                   <!--<td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/CodeListQualifier"/>
                </td>-->
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>-->
                 <!--<tr  width="100%">
               
              </tr>-->
                 <tr  width="100%">
                   <td class="left"  >Pharmacy Notes : </td>
                   <td colspan="10" class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Note"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Refill Qualifier : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Refills/Qualifier"/>
                   </td>
                   <td class="left"  >  </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="Refillsdata"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Substitutions : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="NarcoticFlag"/>
                   </td>
                 </tr>

                 <tr  width="100%">
                   <td class="left"  >Written Date : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="formatDate">
                       <!--<xsl:with-param name="datestr" select="Message/Body/NewRx/Patient/DateOfBirth/Date"/>-->
                       <xsl:with-param name="datestr" select="translate(Message/Body/RefillResponse/MedicationDispensed/WrittenDate/Date,'-','/')"/>
                     </xsl:call-template>
                     <!--<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/WrittenDate/Date"/>-->
                   </td>
                 </tr>
               </table>
             </div>
             <!--Drug Summary End-->

             <!--Provider Information Start-->
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
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Identification/NPI"/>
                  </td>
                  <td class="left" >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >State License Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Identification/StateLicenseNumber"/>
                  </td>
                </tr>-->
                 <xsl:for-each select="Message/Body/RefillResponse/Prescriber/Identification/*">
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
                 </xsl:for-each>
                 <!--<xsl:for-each select="Message/Body/RefillResponse/Prescriber/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>-->
                 <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Specialty"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/ClinicName"/>
                </td>
              </tr>-->

                 <tr  width="100%">
                   <td class="left"  >Name : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Name/LastName"/> &#160;  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Name/FirstName"/>
                   </td>
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

                 <tr  width="100%">
                   <td class="left"  >Address Line 1 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/AddressLine1"/>
                   </td>
                   <td class="left"  > Address Line 2 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/AddressLine2"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >City : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/City"/>
                   </td>
                   <td class="left"  > State : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/State"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Zip Code : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/ZipCode"/>
                   </td>
                   <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->
                 </tr>

                 <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->

                 <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->
                 <xsl:for-each select="Message/Body/RefillResponse/Prescriber/CommunicationNumbers/Communication">
                   <xsl:variable name="Qualifier" select="Qualifier"/>
                   <xsl:choose>
                     <xsl:when test="$Qualifier='TE'">
                       <tr  width="100%">
                         <td class="left"  >Phone : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                     <xsl:when test="$Qualifier='FX'">
                       <tr  width="100%">
                         <td class="left"  >Fax : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                   </xsl:choose>
                 </xsl:for-each>
               </table>
             </div>
             <!--Provider Information End-->

             <!--Patient Information Start-->
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
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/PatientRelationship"/>
                </td>
              </tr>-->

                 <!--<tr  width="100%">
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Patient/Identification/FileID"/>
                  </td>
                  <td class="left"  >Medicare Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Patient/Identification/MedicareNumber"/>
                  </td>
                </tr>-->
                 <xsl:for-each select="Message/Body/RefillResponse/Patient/Identification/*">
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
                 </xsl:for-each>
                 <tr  width="100%">
                   <td class="left"  >Name : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/LastName"/> &#160; <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/FirstName"/> &#160; <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/MiddleName"/>
                   </td>
                   <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/FirstName"/>
                </td>-->
                 </tr>
                 <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/Name/Prefix"/>
                </td>
              </tr>-->
                 <tr  width="100%">
                   <td class="left" >Gender : </td>
                   <td  class="physicianCapfont right">
                     <xsl:variable name="sex" select="Message/Body/RefillResponse/Patient/Gender"/>
                     <xsl:choose>
                       <xsl:when test="$sex='M'">Male</xsl:when>
                       <xsl:when test="$sex='F'">Female</xsl:when>
                       <xsl:when test="$sex='U'">Unknown</xsl:when>
                     </xsl:choose>
                   </td>
                   <td > Date of Birth : </td>
                   <td  class="physicianCapfont right">
                     <xsl:call-template name="formatDate">
                       <!--<xsl:with-param name="datestr" select="Message/Body/NewRx/Patient/DateOfBirth/Date"/>-->
                       <xsl:with-param name="datestr" select="translate(Message/Body/RefillResponse/Patient/DateOfBirth/Date,'-','/')"/>
                     </xsl:call-template>
                   </td>
                 </tr>
                <tr  width="100%">
                   <td class="left"  >Address Line 1 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/AddressLine1"/>
                   </td>
                   <td class="left"  > Address Line 2 : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/AddressLine2"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >City : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/City"/>
                   </td>
                   <td class="left"  > State : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/State"/>
                   </td>
                 </tr>
                 <tr  width="100%">
                   <td class="left"  >Zip Code : </td>
                   <td  class="physicianCapfont right">
                     <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/ZipCode"/>
                   </td>
                   <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/Address/PlaceLocationQualifier"/>
                </td>-->
                 </tr>
                 <xsl:for-each select="Message/Body/RefillResponse/Patient/CommunicationNumbers/Communication">
                   <xsl:variable name="Qualifier" select="Qualifier"/>
                   <xsl:choose>
                     <xsl:when test="$Qualifier='TE'">
                       <tr  width="100%">
                         <td class="left"  >Phone : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                     <xsl:when test="$Qualifier='FX'">
                       <tr  width="100%">
                         <td class="left"  >Fax : </td>
                         <td  class="physicianCapfont right">
                           <xsl:value-of select="Number"/>
                         </td>
                       </tr>
                     </xsl:when>

                   </xsl:choose>
                 </xsl:for-each>
                 <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Patient/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->

               </table>
             </div>
             <!--Patient Information End-->

             <!--Pharmacy Information Start-->
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
              <!--<tr  width="100%">-->
              <!--<td class="left" > NCPDPID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >NPI : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Identification/NPI"/>
                  </td>-->
              <xsl:for-each select="Message/Body/RefillResponse/Pharmacy/Identification/*">
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
              </xsl:for-each>
              <!--</tr>-->
              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Specialty"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Pharmacist/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Pharmacist/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Pharmacist/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Pharmacist/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Pharmacist/Prefix"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left" >Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="Message/Body/RefillResponse/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <xsl:for-each select="Message/Body/RefillResponse/Pharmacy/CommunicationNumbers/Communication">
                <xsl:variable name="Qualifier" select="Qualifier"/>
                <xsl:choose>
                  <xsl:when test="$Qualifier='TE'">
                    <tr  width="100%">
                      <td class="left"  >Phone : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>

                  <xsl:when test="$Qualifier='FX'">
                    <tr  width="100%">
                      <td class="left"  >Fax : </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="Number"/>
                      </td>
                    </tr>
                  </xsl:when>

                </xsl:choose>
              </xsl:for-each>
            </table>
          </div>
             <!--Pharmacy Information End-->


             <!--	<tr  width="100%">
								<td  class="sub-physicianCapfont"> Supervisor </td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Identification : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >NPI : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->
        
             <!--	<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Written Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
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
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="Message/Body/RefillResponse/Observation/ObservationNotes"/>
								</td>
							</tr>-->



        </xsl:if>

      </body>
		</html>
	</xsl:template>
  <xsl:template name="RxChangeResponseNarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/RxChangeResponse/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/RxChangeResponse/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
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
  <xsl:template name="NewRxNarcoticFlag">
    <xsl:choose>
      <xsl:when test="Message/Body/NewRx/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="Message/Body/NewRx/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="RxChangeRefillsData">
    <xsl:choose>
      <xsl:when test="Message/Body/RxChangeResponse/MedicationPrescribed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('1 current dispense', ' + 0 refills')" />
      </xsl:when>
      <xsl:when test="Message/Body/RxChangeResponse/MedicationPrescribed/Refills/Value[text() = '1']">
        <xsl:value-of select="concat('1 current dispense', ' + 0 refill')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="refvalue">
          <xsl:value-of select="Message/Body/RxChangeResponse/MedicationPrescribed/Refills/Value"/>
        </xsl:variable>
        <xsl:value-of select="concat('1 current dispense + ',$refvalue -1 ,' refills')" />
      </xsl:otherwise>
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
  <xsl:template name="NewRxRefillsdata">
    <xsl:choose>
      <xsl:when test="Message/Body/NewRx/MedicationPrescribed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('1 current dispense',' + ',' 0 refills')" />
      </xsl:when>
      <xsl:when test="Message/Body/NewRx/MedicationPrescribed/Refills/Value[text() = '1']">
        <xsl:value-of select="concat('1 current dispense',' + ',' 1 refill')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat('1 current dispense + ', Message/Body/NewRx/MedicationPrescribed/Refills/Value,' more refills')" />
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
     output format dd/mm/yyyy--><!--

    --><!--<xsl:variable name="dd">
      --><!--<xsl:value-of select="substring($datestr,8,9)" />--><!--
    </xsl:variable>
       
    <xsl:variable name="mm">
      --><!--<xsl:value-of select="substring($datestr,6,7)" />--><!--
    </xsl:variable>--><!--
   

    --><!--<xsl:variable name="yyyy">
      <xsl:value-of select="substring($datestr,1,4)" />
    </xsl:variable>
    <xsl:value-of select="$yyyy" />--><!--
    
    --><!--<xsl:value-of select="$dd" />
    <xsl:value-of select="'/'" />
    <xsl:value-of select="$mm" />--><!--
    --><!--<xsl:value-of select="'/'" />
    <xsl:value-of select="$yyyy" />
    <xsl:value-of select="$datestr" />-->

   
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
  
</xsl:stylesheet>
