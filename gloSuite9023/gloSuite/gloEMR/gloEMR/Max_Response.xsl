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
          padding:0px;
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

        <xsl:if test="NewDataSet/Message/Body/RefillResponse">

          <!-- <div id="Main-Container" style="margin-top:5px;padding-right:2px;">
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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/PrescriberOrderNumber"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/DenialReason"/>
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
              <tr  width="100%">
                <!--<td class="left" > NCPDPID : </td>-->
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Identification/NCPDPID"/>-->
                </td>
                <!--<td class="left"  >File ID : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Identification/FileID"/>
							</td>-->
              </tr>
              <tr  width="100%">
                <!--<td class="left"  >NPI : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Identification/NPI"/>
							</td>-->
              </tr>
              <tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Specialty"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist/Prefix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Store Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
            </table>
          </div>


          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Prescriber
                </td>
              </tr>
            </table>
          </div>

          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="1" cellspacing="1">

              <tr  width="100%">
                <!--<td class="left"  >NPI : </td>-->
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/Prescriber/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Identification/NPI"/>-->
                </td>
                <!--<td class="left" >File ID : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Identification/FileID"/>
							</td>-->
              </tr>
              <!--<tr  width="100%">
							<td class="left"  >State License Number : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Identification/StateLicenseNumber"/>
							</td>
						</tr>-->
              <tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Specialty"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/ClinicName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name/Prefix"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent/Prefix"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
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
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Patient
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <tr  width="100%">
                <td class="left"  >Patient Relationship : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/PatientRelationship"/>
                </td>
              </tr>

              <tr  width="100%">
                <!--<td class="left"  >File ID : </td>-->
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/Patient/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Identification/FileID"/>-->
                </td>
                <!--<td class="left"  >Medicare Number : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Identification/MedicareNumber"/>
							</td>-->
              </tr>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name/Prefix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Gender"/>
                </td>
                <td > Date of Birth : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/DateOfBirth/Date"/>
                </td>
              </tr>
               <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Patient/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
            </table>
          </div>



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
                <td class="left"  >Drug Description : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugDescription"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Quantity/Value"/>&#160;
                  <xsl:value-of select="NewDataSet/PotencyDescription/MP"/>
                </td>
                <td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Quantity/CodeListQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Days Supply : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DaysSupply"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Note : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Note"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Refills/Qualifier"/>
                </td>
                <td class="left"  > Value : </td>
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
                <td class="left"  >Writtren Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/WrittenDate/Date"/>
                </td>
              </tr>
            </table>
          </div>


          <!--		<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr> 
         
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Writtren Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillResponse/Observation/ObservationNotes"/>
								</td>
							</tr>-->
        </xsl:if>

        <xsl:if test="NewDataSet/Message/Body/NewRx">


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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/PrescriberOrderNumber"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Response/Denied/DenialReason"/>
								</td>
							</tr> -->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Pharmacy Infor
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%">
              <!--<tr  width="100%">-->
              <!--<td class="left" > NCPDPID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Identification/NCPDPID"/>
                  </td>
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >NPI : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Identification/NPI"/>
                  </td>-->
              <xsl:for-each select="NewDataSet/Message/Body/NewRx/Pharmacy/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>
              <!--</tr>-->
              <tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Specialty"/>
                </td>
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Pharmacist/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Pharmacist/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Pharmacist/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Pharmacist/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Pharmacist/Prefix"/>
                  </td>
                </tr>-->
              <tr  width="100%">
                <td class="left" >Store Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Pharmacy/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
            </table>
          </div>


          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Prescriber
                </td>
              </tr>
            </table>
          </div>

          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <!--<tr  width="100%">
                  <td class="left"  >NPI : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Identification/NPI"/>
                  </td>
                  <td class="left" >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Identification/FileID"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >State License Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Identification/StateLicenseNumber"/>
                  </td>
                </tr>-->
              <xsl:for-each select="NewDataSet/Message/Body/NewRx/Prescriber/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>
              <tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Specialty"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/ClinicName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Name/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Name/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Name/Prefix"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >Last Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/PrescriberAgent/LastName"/>
                  </td>
                  <td class="left"  > First Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/PrescriberAgent/FirstName"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Middle Name : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/PrescriberAgent/MiddleName"/>
                  </td>
                  <td class="left"  > Suffix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/PrescriberAgent/Suffix"/>
                  </td>
                </tr>
                <tr  width="100%">
                  <td class="left"  >Prefix : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/PrescriberAgent/Prefix"/>
                  </td>
                </tr>-->

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
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
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
								</td>
							</tr> -->
          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Patient
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">

              <tr  width="100%">
                <td class="left"  >Patient Relationship : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/PatientRelationship"/>
                </td>
              </tr>

              <!--<tr  width="100%">
                  <td class="left"  >File ID : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Identification/FileID"/>
                  </td>
                  <td class="left"  >Medicare Number : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Identification/MedicareNumber"/>
                  </td>
                </tr>-->
              <xsl:for-each select="NewDataSet/Message/Body/NewRx/Patient/Identification/*">
                <tr  width="100%">
                  <td class="left" >
                    <xsl:value-of select="concat(name(),' :')"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="current()"/>
                  </td>
                </tr>
              </xsl:for-each>

              <tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Name/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Name/FirstName"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Name/Prefix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Gender"/>
                </td>
              </tr>
              <tr  width="100%">
                <td > Date of Birth : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/DateOfBirth/Date"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/State"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/ZipCode"/>
                </td>
                <td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/Address/PlaceLocationQualifier"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/Patient/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>
            </table>
          </div>



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
                <td class="left"  >Drug Description : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugDescription"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                <td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Quantity/Value"/>&#160;
                  <xsl:value-of select="Message/PotencyDescription/MP"/>
                </td>
                <td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Quantity/CodeListQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                 
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Days Supply : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DaysSupply"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Note : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Note"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Refills/Qualifier"/>
                </td>
                <td class="left"  > Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="NewRxRefillsdata"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Substitutions : </td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="NewRxNarcoticFlag"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Writtren Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/WrittenDate/Date"/>
                </td>
              </tr>
            </table>
          </div>


          <!--	<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Medication Dispensed : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug Description : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Drug Coded : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Product Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/ProductCode"/>
								</td>
								<td class="left"  > Product Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/Strength"/>
								</td>
								<td class="left"  > Drug DB Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/DrugDBCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Drug DB Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
								</td>
								<td class="left"  > Form Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/FormSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/FormCode"/>
								</td>
								<td class="left"  > Strength Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Strength Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoded/StrengthCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Quantity : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Quantity/Value"/>
								</td>
								<td class="left"  > Code List Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Quantity/CodeListQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Unit Source Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Quantity/UnitSourceCode"/>
								</td>
								<td class="left"  >Potency Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Quantity/PotencyUnitCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Days Supply : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DaysSupply"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Directions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Directions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Note : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Note"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Refills : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Refills/Qualifier"/>
								</td>
								<td class="left"  > Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Refills/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Substitutions : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Substitutions"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Writtren Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date Time : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/WrittenDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/Dimension"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-physicianCapfont" >Observation Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/ObservationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Data Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/MeasurementDataQualifier"/>
								</td>
								<td class="left" > Measurement Source Code: : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/MeasurementSourceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Measurement Unit Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/Measurement/MeasurementUnitCode"/>
								</td>
								<td class="left" > Observation Notes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/NewRx/Observation/ObservationNotes"/>
								</td>
							</tr>-->



        </xsl:if>

        <xsl:if test="NewDataSet/Message/Body/RefillRequest">

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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>

								<td  >Version :</td>
								<td class="left" class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
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
									<xsl:value-of select="NewDataSet/Message/Header/To"/>
								</td>
								<td class="left" >From :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/From"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/MessageID"/>
								</td>	
								<td class="left" >Relates Message ID :</td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Header/RelatesToMessageID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sent Time : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SentTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont">Sender Software </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Developer :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareDeveloper"/>
								</td>
								<td class="left" >Sender Software Product :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareProduct"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Sender Software Version Release : </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/SenderSoftware/SenderSoftwareVersionRelease"/>
								</td>
								<td class="left" >Text Message :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TestMessage"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Rx Reference Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/RxReferenceNumber"/>
								</td>
								<td class="left" >Tertiary Identifier :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/TertiaryIdentifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prescriber Order Number :  </td>
								<td  class="physicianCapfont right">	
									<xsl:value-of select="NewDataSet/Message/Header/PrescriberOrderNumber"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Request/ReturnReceipt"/>
								</td>
								<td class="left" >Request Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Request/RequestReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-physicianCapfont"> Response </td>
							</tr>
							<tr  width="100%">
								<td class="left" >Aprove : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/Approve"/>
								</td>
								<td class="left" >Aprove with Changes : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/ApprovedWithChanges"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td" >Denied New Prescription To Follow : </td>
								<td  class="physicianCapfont right"/>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Denied : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/Denied/DenialReasonCode"/>
								</td>
								<td class="left"  >Reference Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/Denied/ReferenceNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Denial Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Response/Denied/DenialReason"/>
								</td>
							</tr> -->

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
            <table class="orangeheader"  width="100%" cellpadding="1.9" cellspacing="1.9">

              <tr  width="100%">
                <td class="left"  >Drug : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugDescription"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  colspan="1" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Strength : </td>
                <td colspan="6" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/Strength"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                --><!--<td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/FormSourceCode"/>
                </td>--><!--
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                --><!--<td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/StrengthCode"/>
                </td>--><!--
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Drug Qty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/Value"/>&#160;
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                  <!--<xsl:value-of select="NewDataSet/PotencyDescription/MD"/>-->
                </td>
                <!--<td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier"/>
                </td>-->
              <!--</tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">-->
                <td class="left"  >Duration : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DaysSupply"/>-->
                  <xsl:variable name="Days" select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DaysSupply"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Code Qty Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:choose>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier='38'">
                      38&#160;(Original Quantity)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier='40'">
                      40&#160;(Remaining Quantity)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier='87'">
                      87&#160;(Quantity Received)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier='QS'">
                      QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier='CF'">
                      CF&#160;(Compound Final Quantity)
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/CodeListQualifier"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Source Code List : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/UnitSourceCode"/> &#160;(NCPDP Drug quantity unit of measure terminology)
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>
              
              
              <tr  width="100%">
                <td class="left"  >Drug Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Notes : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Note"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Ref. Qlf : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Refills/Qualifier"/>-->
                  <xsl:variable name="RefQlf" select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Refills/Qualifier"/>
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
                <td class="left"  >Refills : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:call-template name="Refillsdata"/>-->
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Refills/Value"/>
                </td>
              <!--</tr>-->
              <!--<tr  width="100%">-->
                <td class="left"  >Substitution :</td>
                <td  class="physicianCapfont right">
                  <xsl:call-template name="NewRxNarcoticFlag"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Written Date : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/WrittenDate/Date"/>-->
                  <xsl:variable name="AnyDate" select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/WrittenDate/Date"/>
                  <xsl:if test="string-length($AnyDate) != 0">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/WrittenDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </xsl:if>
                  
                
                </td>
              <!--</tr>-->

              <!--<tr  width="100%">-->
                <td class="left"  >Last Fill Date : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/LastFillDate/Date"/>-->
                  <xsl:variable name="AnyDate" select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/LastFillDate/Date"/>
                  <xsl:if test="string-length($AnyDate) != 0">
                      <xsl:call-template name="formatDate">
                        <xsl:with-param name="datestr" select="translate(NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/LastFillDate/Date,'-','/')"/>
                      </xsl:call-template>
                  </xsl:if>
               
                </td>
              </tr>

              <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis">
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
                        <tr >
                          <th class="SUBMedth" >Clinical Info Qualifier</th>
                          <th class="SUBMedth" >Diagnosis</th>
                          <th class="SUBMedth" >Code List Qualifier</th>
                          <th class="SUBMedth" >ICD Code</th>
                        </tr>
                        <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary">
                          <tr style="background-color: {$vClr1}; ">
                            <td  style="border: 1px solid #f98800;" rowspan="2">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='1'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier='2'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">Primary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier='DX'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier='ABF'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Value"/>
                            </td>

                          </tr>
                        </xsl:if>
                        <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary">
                          <tr style="background-color: {$vClr2};">
                            <td  style="border: 1px solid #f98800;">Secondary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier='DX'">
                                  <xsl:value-of select="Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier='ABF'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Value"/>
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
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugDescription"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td  class="sub-td" > Drug Coded : </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Product Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/ProductCode"/>
                </td>
                <td class="left"  > Product Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/ProductCodeQualifier"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Strength : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/Strength"/>
                </td>
                <td class="left"  > Drug DB Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/DrugDBCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Drug DB Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/DrugDBCodeQualifier"/>
                </td>
                --><!--<td class="left"  > Form Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/FormSourceCode"/>
                </td>--><!--
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/FormCode"/>
                </td>
                <td class="left"  > Strength Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/StrengthSourceCode"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Strength Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoded/StrengthCode"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td  class="sub-td" > Quantity : </td>
              </tr>-->
              <tr  width="100%">
                <td class="left"  >Drug Qty : </td>
                <td class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/Value"/>&#160;
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/PotencyUnitCode"/>
                  <!--<xsl:value-of select="NewDataSet/PotencyDescription/MD"/>-->
                </td>
                <!--<td class="left"  > Code List Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier"/>
                </td>-->
              <!--</tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Unit Source Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/UnitSourceCode"/>
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">-->
                <td class="left"  >Duration : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DaysSupply"/>-->
                  <xsl:variable name="Days" select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DaysSupply"/>
                  <xsl:choose>
                    <xsl:when test="$Days!=' '">
                      <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DaysSupply"/> Days
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Code Qty Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:choose>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier='38'">
                      38&#160;(Original Quantity)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier='40'">
                      40&#160;(Remaining Quantity)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier='87'">
                      87&#160;(Quantity Received)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier='QS'">
                      QS&#160;(Quantity sufficient as determined by the dispensing pharmacy)
                    </xsl:when>
                    <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier='CF'">
                      CF&#160;(Compound Final Quantity)
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/CodeListQualifier"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
                <td class="left"  >Source Code List : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/UnitSourceCode"/> &#160;(NCPDP Drug quantity unit of measure terminology)
                </td>
                <td class="left"  >Potency Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Quantity/PotencyUnitCode"/>
                </td>
              </tr>
              
              <tr  width="100%">
                <td class="left"  >Drug Directions : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Directions"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Notes : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Note"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td  class="sub-td" > Refills : </td>
              </tr>-->
              <tr  width="100%">
                <td class="left"  >Ref. Qlf : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Refills/Qualifier"/>-->
                  <xsl:variable name="RefQlf" select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Refills/Qualifier"/>
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
                <td class="left"  >Refills : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Refills/Value"/>
                </td>
              <!--</tr>
              <tr  width="100%">-->
                <td class="left"  >Substitution : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Substitutions"/>-->
                  <xsl:call-template name="NewRxNarcoticFlag"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td  class="sub-td" > Writtren Date : </td>
              </tr>-->
              <tr  width="100%">
                <td class="left"  >Written Date : </td>
                <td  class="physicianCapfont right">
                
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/WrittenDate/Date"/>-->
                  <xsl:variable name="AnyDate" select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/WrittenDate/Date"/>
                  <xsl:if test="string-length($AnyDate) != 0">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(NewDataSet/Message/Body/RefillRequest/MedicationDispensed/WrittenDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </xsl:if>
                </td>
              <!--</tr>-->
              <!--<tr  width="100%">
                <td  class="sub-td" > Last Fill Date : </td>
              </tr>-->
              <!--<tr  width="100%">-->
                <td class="left"  >Last Fill Date : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/LastFillDate/Date"/>-->
                  <xsl:variable name="AnyDate" select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/LastFillDate/Date"/>
                  <xsl:if test="string-length($AnyDate) != 0">
                    <xsl:call-template name="formatDate">
                      <xsl:with-param name="datestr" select="translate(NewDataSet/Message/Body/RefillRequest/MedicationDispensed/LastFillDate/Date,'-','/')"/>
                    </xsl:call-template>
                  </xsl:if>
                </td>
              </tr>

              <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis">
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
                        <tr >
                          <th class="SUBMedth" >Clinical Info Qualifier</th>
                          <th class="SUBMedth" >Diagnosis</th>
                          <th class="SUBMedth" >Code List Qualifier</th>
                          <th class="SUBMedth" >ICD Code</th>
                        </tr>
                        <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary">
                          <tr style="background-color: {$vClr1}; ">
                            <td  style="border: 1px solid #f98800;" rowspan="2">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier='1'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/> - Prescriber Supplied
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier='2'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/> - Pharmacy Inferred
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">Primary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Qualifier='DX'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Qualifier='ABF'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Value"/>
                            </td>

                          </tr>
                        </xsl:if>
                        <xsl:if test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary">
                          <tr style="background-color: {$vClr2};">
                            <td  style="border: 1px solid #f98800;">Secondary</td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:choose>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier='DX'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier"/> - ICD-9
                                </xsl:when>
                                <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier='ABF'">
                                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier"/> - ICD-10
                                </xsl:when>
                              </xsl:choose>
                            </td>
                            <td  style="border: 1px solid #f98800;">
                              <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Value"/>
                            </td>
                          </tr>
                        </xsl:if>
                      </table>
                    </div>
                  </td>
                </tr>
              </xsl:if>
              
              <!--Commented Unwanted fields of dispensed-->

              <!--  <tr  width="100%">
                <td  class="sub-td" > Expiration Date : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/ExpirationDate/Date"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-td" > Effective Date : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/EffectiveDate/DateTime"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-td" > Period End : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/PeriodEnd/Date"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-td" > Delivered On Date : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DeliveredOnDate/Date"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-td" > Date Validated : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DateValidated/Date"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-td" > Diagnosis : </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Clinical Information Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/ClinicalInformationQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Primary : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Qualifier"/>
                </td>
                <td class="left" >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Primary/Value"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Secondary : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Qualifier"/>
                </td>
                <td class="left" >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Diagnosis/Secondary/Value"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Prior Authorization : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/PriorAuthorization/Qualifier"/>
                </td>
                <td class="left" >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/PriorAuthorization/Value"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Drug Use Evaluation : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Service Reason Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/ServiceReasonCode"/>
                </td>
                <td class="left" >Professional Service Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/ProfessionalServiceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Service Result Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/ServiceResultCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >CoAgent : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >CoAgent ID : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentID"/>
                </td>
                <td class="left" >CoAgent Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Clinical Significance Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/ClinicalSignificanceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Acknowledgement Reason : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation/AcknowledgementReason"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Drug Coverage Status Code: </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugCoverageStatusCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left" >Prior Authorization Status : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/PriorAuthorizationStatus"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Structured SIG : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/DrugDescription"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Repeating SIG : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Sig Sequence Position Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
                </td>
                <td class="left" > Multiple Sig Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Code System : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >SNOMED Version : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
                </td>
                <td class="left" > FMT Version : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/CodeSystem/FMTVersion"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Free Text : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Sig Free Text String Indicator : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
                </td>
                <td class="left" > Sig Free Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/FreeText/SigFreeText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Dose : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Composite Indicator : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseCompositeIndicator"/>
                </td>
                <td class="left" > Dose Delivery Method Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
                </td>
                <td class="left" > Dose Delivery Method Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Delivery Method Modifier Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
                </td>
                <td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Delivery Method Modifier Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
                </td>
                <td class="left" > Dose Quantity : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseQuantity"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Form Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormText"/>
                </td>
                <td class="left" > Dose Form Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dose Form Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseFormCode"/>
                </td>
                <td class="left" > Dose Range Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Dose/DoseRangeModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Dose Calculation : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dosing Basis Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
                </td>
                <td class="left" > Dosing Basis Unit of Measure Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
                </td>
                <td class="left" > Dosing Basis Unit of Measure Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Body Metric Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
                </td>
                <td class="left" > Body Metric Value: </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Calculated Dose Numeric : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
                </td>
                <td class="left" > Calculated Dose Unit of Measure Text: </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
                </td>
                <td class="left" > Calculated Dose Unit of Measure Code: </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dosing Basis Range Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Vehicle : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Vehicle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleName"/>
                </td>
                <td class="left" > Vehicle Name Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Vehicle Name Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleNameCode"/>
                </td>
                <td class="left" > Vehicle Quantity : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleQuantity"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Vehicle Unit Of Measure Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
                </td>
                <td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Vehicle Unit Of Measure Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
                </td>
                <td class="left" > Multiple Vehicle Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Route of Administration : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Route of Administration Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
                </td>
                <td class="left" > Route of Administration Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Route of Administration Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
                </td>
                <td class="left" > Multiple Route of Administration Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Route of Administration Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
                </td>
                <td class="left" > Multiple Route of Administration Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Site of Administration : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Site of Administration Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
                </td>
                <td class="left" > Site of Administration Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Site of Administration Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
                </td>
                <td class="left" > Multiple Administration Timing Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Timing : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Administration Timing Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingText"/>
                </td>
                <td class="left" > Administration Timing Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Administration Timing Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/AdministrationTimingCode"/>
                </td>
                <td class="left" > Multiple Administration Timing Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Rate of Administration : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/RateofAdministration"/>
                </td>
                <td class="left" > Rate Unit of Measure Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
                </td>
                <td class="left" > Rate Unit of Measure Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Time Period Basis Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisText"/>
                </td>
                <td class="left" > Time Period Basis Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Time Period Basis Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/TimePeriodBasisCode"/>
                </td>
                <td class="left" > Frequency Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyNumericValue"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Frequency Units Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsText"/>
                </td>
                <td class="left" > Frequency Units Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Frequency Units Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
                </td>
                <td class="left" > Variable Frequency Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Frequency Units Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/FrequencyUnitsCode"/>
                </td>
                <td class="left" > Variable Frequency Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/VariableFrequencyModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Interval Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/IntervalNumericValue"/>
                </td>
                <td class="left" > Interval Units Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Interval Units Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
                </td>
                <td class="left" > Interval Units Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/IntervalUnitsCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Variable Interval Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Timing/VariableIntervalModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Duration : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Duration Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Duration/DurationNumericValue"/>
                </td>
                <td class="left" > Duration Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Duration/DurationText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Duration Text Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
                </td>
                <td class="left" > Duration Text Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Duration/DurationTextCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Maximum Dose Restriction : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
                </td>
                <td class="left" > Maximum Dose Restriction Units Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
                </td>
                <td class="left" > Maximum Dose Restriction Units Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
                </td>
                <td class="left" > Maximum Dose Restriction Variable Units Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
                </td>
                <td class="left" > Maximum Dose Restriction Variable Units Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Indication : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Precursor Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorText"/>
                </td>
                <td class="left" > Indication Precursor Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Precursor Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationPrecursorCode"/>
                </td>
                <td class="left" > Indication Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationText"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Text Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
                </td>
                <td class="left" > Indication Text Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationTextCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Value Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueText"/>
                </td>
                <td class="left" > Indication Value Unit : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnit"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Value Unit of Measure Text : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
                </td>
                <td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Indication Value Unit of Measure Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
                </td>
                <td class="left" > Indication Variable Modifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Indication/IndicationVariableModifier"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Stop : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Stop Indicator : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/StructuredSIG/Stop/StopIndicator"/>
                </td>
              </tr>
              <tr  width="100%">
                <td  class="sub-physicianCapfont"> Observation : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td sub-td-padding" >Measurement : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Dimension : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/Dimension"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Value : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/Value"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-physicianCapfont" >Observation Date : </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Date : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/ObservationDate/Date"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Measurement Data Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/MeasurementDataQualifier"/>
                </td>
                <td class="left" > Measurement Source Code: : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/MeasurementSourceCode"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left sub-td-padding " >Measurement Unit Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/Measurement/MeasurementUnitCode"/>
                </td>
                <td class="left" > Observation Notes : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Observation/ObservationNotes"/>
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
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Identification/NPI"/>
								</td>
								<td class="left" >File ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Identification/FileID"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"   >State License Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Identification/StateLicenseNumber"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Specialty : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Specialty"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Name : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Last Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Name/LastName"/>
								</td>
								<td class="left"  > First Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Name/FirstName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Middle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Name/MiddleName"/>
								</td>
								<td class="left"  > Suffix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Name/Suffix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Prefix : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Name/Prefix"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Clinic Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/ClinicName"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Address : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Address Line 1 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/AddressLine1"/>
								</td>
								<td class="left"  > Address Line 2 : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/AddressLine2"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >City : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/City"/>
								</td>
								<td class="left"  > State : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/State"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Zip Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/ZipCode"/>
								</td>
								<td class="left"  > Place Location Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/Address/PlaceLocationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Communication Numbers : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/CommunicationNumbers/Communication/Number"/>
								</td>
								<td class="left"  > Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Supervisor/CommunicationNumbers/Communication/Qualifier"/>
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
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/PatientRelationship"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
               
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Patient/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                 
                </td>
              
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Patient Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/LastName"/>&#160;<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/FirstName"/>&#160;<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/MiddleName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%" class="">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Name/Prefix"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left" >Gender : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Gender"/>-->
                  <xsl:variable name="sex" select="NewDataSet/Message/Body/RefillRequest/Patient/Gender"/>
                  <xsl:choose>
                    <xsl:when test="$sex='M'">Male</xsl:when>
                    <xsl:when test="$sex='F'">Female</xsl:when>
                    <xsl:when test="$sex='U'">Unknown</xsl:when>
                  </xsl:choose>
                </td>
                <td > Date of Birth : </td>
                <td  class="physicianCapfont right">
                  <!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/DateOfBirth/Date"/>-->
                  <xsl:call-template name="formatDate">
                    <xsl:with-param name="datestr" select="translate(NewDataSet/Message/Body/RefillRequest/Patient/DateOfBirth/Date,'-','/')"/>
                  </xsl:call-template>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/State"/>
                </td>
                <!--</tr>
              <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Patient/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Patient/CommunicationNumbers/Communication">
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
            </table>
          </div>

          <!--Med Pres-->

          <div class="Mainheader">
            <table width="100%" cellspacing="0" cellpadding="0" >
              <tr  width="100%">
                <td width="80%" style="padding-left:5px;">
                  Pharmacy information
                </td>
              </tr>
            </table>
          </div>
          <div class="Orgphysician">
            <table class="orangeheader"  width="100%" cellpadding="2" cellspacing="2">
              <!--<tr  width="100%">
                --><!--<td class="left" > NCPDPID : </td>--><!--
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  --><!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Identification/NCPDPID"/>--><!--
                </td>
                --><!--<td class="left"  >File ID : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Identification/FileID"/>
							</td>--><!--
              </tr>-->
              <tr  width="100%">
                <td class="left"  >NPI : </td>
							  <td  class="physicianCapfont right">
								  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Identification/NPI"/>
							  </td>
                <td class="left"  >NCPDPID : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Identification/NCPDPID"/>
                </td>
              </tr>
              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Specialty"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Pharmacist/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Pharmacist/FirstName"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Pharmacist/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Pharmacist/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Pharmacist/Prefix"/>
                </td>
              </tr>-->
              <tr  width="100%">
                <td class="left" >Pharmacy Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/StoreName"/>
                </td>
              </tr>

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/State"/>
                </td>
              <!--</tr>
              <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier :</td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Pharmacy/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->


              <tr  width="100%">
              <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Pharmacy/CommunicationNumbers/Communication">
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
           
              <!--<xsl:if test="string-length(Teleph) != 0 or string-length(Fax) != 0">
                <tr  width ="100%">
                  <td class="left"  >Telephone (TE) : </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Teleph"/>
                  </td>
                  <td  class="physicianCapfont right">
                    <xsl:value-of select="Fax"/>
                  </td>
                </tr>
                
              </xsl:if>-->
            </table>
          </div>


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
                --><!--<td class="left"  >NPI : </td>--><!--
                <td  class="physicianCapfont right">
                  <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/*">
                    <tr  width="100%">
                      <td class="left" >
                        <xsl:value-of select="concat(name(),' :')"/>
                      </td>
                      <td  class="physicianCapfont right">
                        <xsl:value-of select="current()"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  --><!--<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/NPI"/>--><!--
                </td>
                --><!--<td class="left" >File ID : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/FileID"/>
							</td>--><!--
              </tr>-->
              <!--<tr  width="100%">
							<td class="left"  >State License Number : </td>
							<td  class="physicianCapfont right">
								<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/StateLicenseNumber"/>
							</td>
						</tr>-->

              <!-- <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/*">
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

              <tr>
                <td class="left"  >NPI : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/NPI"/>
                </td>
                <td class="left"  >DEA Number: </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Identification/DEANumber"/>
                </td>
              </tr>
              
              <!--<tr  width="100%">
                <td class="left" >Specialty : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Specialty"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left" >Clinic Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/ClinicName"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Provider Name : </td>
                <td colspan="10" class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/LastName"/>&#160; <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/FirstName"/>
                </td>
                <!--<td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/FirstName"/>
                </td>-->
              </tr>
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Name/Prefix"/>
                </td>
              </tr>-->

              <tr  width="100%">
                <td class="left"  >Address Line 1 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/AddressLine1"/>
                </td>
                <td class="left"  > Address Line 2 : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/AddressLine2"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >City : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/City"/>
                </td>
                <td class="left"  > State : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/State"/>
                </td>
              <!--</tr>
              <tr  width="100%">-->
                <td class="left"  >Zip Code : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/ZipCode"/>
                </td>
                <!--<td class="left"  > Place Location Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/Address/PlaceLocationQualifier"/>
                </td>-->
              </tr>

              <!--<tr  width="100%">
                <td class="left"  >Last Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/PrescriberAgent/LastName"/>
                </td>
                <td class="left"  > First Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/PrescriberAgent/FirstName"/>
                </td>
              </tr>-->
              <!--<tr  width="100%">
                <td class="left"  >Middle Name : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/PrescriberAgent/MiddleName"/>
                </td>
                <td class="left"  > Suffix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/PrescriberAgent/Suffix"/>
                </td>
              </tr>
              <tr  width="100%">
                <td class="left"  >Prefix : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/PrescriberAgent/Prefix"/>
                </td>
              </tr>-->

              <!--<tr  width="100%">
                <td class="left"  >Number : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/CommunicationNumbers/Communication/Number"/>
                </td>
                <td class="left"  > Qualifier : </td>
                <td  class="physicianCapfont right">
                  <xsl:value-of select="NewDataSet/Message/Body/RefillRequest/Prescriber/CommunicationNumbers/Communication/Qualifier"/>
                </td>
              </tr>-->
              <tr  width="100%">
              <xsl:for-each select="NewDataSet/Message/Body/RefillRequest/Prescriber/CommunicationNumbers/Communication">
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
            </table>
          </div>

   


          <!--	<tr  width="100%">
								<td  class="sub-td" > Last Fill Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/LastFillDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Expiration Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/ExpirationDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Effective Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/EffectiveDate/DateTime"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Period End : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/PeriodEnd/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Delivered On Date : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DeliveredOnDate/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Date Validated : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Date : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DateValidated/Date"/>
								</td>
							</tr>
							<tr  width="100%">
								<td  class="sub-td" > Diagnosis : </td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Clinical Information Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/ClinicalInformationQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Primary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Primary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Secondary : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Diagnosis/Secondary/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Prior Authorization : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/PriorAuthorization/Qualifier"/>
								</td>
								<td class="left" >Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/PriorAuthorization/Value"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Drug Use Evaluation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Reason Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/ServiceReasonCode"/>
								</td>
								<td class="left" >Professional Service Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/ProfessionalServiceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Service Result Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/ServiceResultCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >CoAgent : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >CoAgent ID : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentID"/>
								</td>
								<td class="left" >CoAgent Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/CoAgent/CoAgentQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Clinical Significance Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/ClinicalSignificanceCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Acknowledgement Reason : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation/AcknowledgementReason"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Drug Coverage Status Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugCoverageStatusCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" >Prior Authorization Status : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/PriorAuthorizationStatus"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left"  >Structured SIG : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/DrugDescription"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Repeating SIG : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Sequence Position Number : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RepeatingSIG/SigSequencePositionNumber"/>
								</td>
								<td class="left" > Multiple Sig Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RepeatingSIG/MultipleSigModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Code System : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >SNOMED Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/CodeSystem/SNOMEDVersion"/>
								</td>
								<td class="left" > FMT Version : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/CodeSystem/FMTVersion"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Free Text : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Sig Free Text String Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/FreeText/SigFreeTextStringIndicator"/>
								</td>
								<td class="left" > Sig Free Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/FreeText/SigFreeText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Composite Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseCompositeIndicator"/>
								</td>
								<td class="left" > Dose Delivery Method Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCodeQualifier"/>
								</td>
								<td class="left" > Dose Delivery Method Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierText"/>
								</td>
								<td class="left" > Dose Delivery Method Modifier Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Delivery Method Modifier Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseDeliveryMethodModifierCode"/>
								</td>
								<td class="left" > Dose Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormText"/>
								</td>
								<td class="left" > Dose Form Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dose Form Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseFormCode"/>
								</td>
								<td class="left" > Dose Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Dose/DoseRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Dose Calculation : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisNumericValue"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Dosing Basis Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Body Metric Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricQualifier"/>
								</td>
								<td class="left" > Body Metric Value: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/BodyMetricValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Numeric : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseNumeric"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Text: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Calculated Dose Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Calculated Dose Unit of Measure Code: </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/CalculatedDoseUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Dosing Basis Range Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/DoseCalculation/DosingBasisRangeModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Vehicle : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleName"/>
								</td>
								<td class="left" > Vehicle Name Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Name Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleNameCode"/>
								</td>
								<td class="left" > Vehicle Quantity : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleQuantity"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureText"/>
								</td>
								<td class="left" > Vehicle Unit Of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Vehicle Unit Of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/VehicleUnitOfMeasureCode"/>
								</td>
								<td class="left" > Multiple Vehicle Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Vehicle/MultipleVehicleModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Route of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationText"/>
								</td>
								<td class="left" > Route of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Route of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/RouteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Route of Administration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/RouteofAdministration/MultipleRouteofAdministrationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Site of Administration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationText"/>
								</td>
								<td class="left" > Site of Administration Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Site of Administration Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/SiteofAdministrationCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/SiteofAdministration/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Timing : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingText"/>
								</td>
								<td class="left" > Administration Timing Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Administration Timing Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/AdministrationTimingCode"/>
								</td>
								<td class="left" > Multiple Administration Timing Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/MultipleAdministrationTimingModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate of Administration : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/RateofAdministration"/>
								</td>
								<td class="left" > Rate Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Rate Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCodeQualifier"/>
								</td>
								<td class="left" > Rate Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/RateUnitofMeasureCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisText"/>
								</td>
								<td class="left" > Time Period Basis Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Time Period Basis Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/TimePeriodBasisCode"/>
								</td>
								<td class="left" > Frequency Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyNumericValue"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsText"/>
								</td>
								<td class="left" > Frequency Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Frequency Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/FrequencyUnitsCode"/>
								</td>
								<td class="left" > Variable Frequency Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/VariableFrequencyModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalNumericValue"/>
								</td>
								<td class="left" > Interval Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Interval Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCodeQualifier"/>
								</td>
								<td class="left" > Interval Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/IntervalUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Variable Interval Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Timing/VariableIntervalModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Duration : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Duration/DurationNumericValue"/>
								</td>
								<td class="left" > Duration Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Duration/DurationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Duration Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Duration/DurationTextCodeQualifier"/>
								</td>
								<td class="left" > Duration Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Duration/DurationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Maximum Dose Restriction : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Maximum Dose Restriction Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Numeric Value : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableNumericValue"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Units Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCodeQualifier"/>
								</td>
								<td class="left" > Maximum Dose Restriction Variable Units Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableUnitsCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " > Maximum Dose Restriction Variable Duration Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/MaximumDoseRestriction/MaximumDoseRestrictionVariableDurationModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Indication : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorText"/>
								</td>
								<td class="left" > Indication Precursor Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Precursor Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationPrecursorCode"/>
								</td>
								<td class="left" > Indication Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationText"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Text Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCodeQualifier"/>
								</td>
								<td class="left" > Indication Text Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationTextCode"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueText"/>
								</td>
								<td class="left" > Indication Value Unit : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnit"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Text : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureText"/>
								</td>
								<td class="left" > Indication Value Unit of Measure Code Qualifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCodeQualifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Indication Value Unit of Measure Code : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationValueUnitofMeasureCode"/>
								</td>
								<td class="left" > Indication Variable Modifier : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Indication/IndicationVariableModifier"/>
								</td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td sub-td-padding" >Stop : </td>
							</tr>
							<tr  width="100%">
								<td class="left" class="sub-td-padding " >Stop Indicator : </td>
								<td  class="physicianCapfont right">
									<xsl:value-of select="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG/Stop/StopIndicator"/>
								</td>
							</tr> -->



        </xsl:if>

      </body>
    </html>
  </xsl:template>
  <xsl:template name="NarcoticFlag">
    <xsl:choose>
      <xsl:when test="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="NewRxNarcoticFlag">
    <xsl:choose>
      
      <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Substitutions[text() = '0']">
        Yes
      </xsl:when>
      <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationDispensed/Substitutions[text() = '1']">
        No
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="Refillsdata">
    <xsl:choose>
      <xsl:when test="NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('This refill and',' 1 ','more refills')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat('This refill and ', NewDataSet/Message/Body/RefillRequest/MedicationPrescribed/Refills/Value,' more refills')" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="NewRxRefillsdata">
    <xsl:choose>
      <xsl:when test="NewDataSet/Message/Body/NewRx/MedicationPrescribed/Refills/Value[text() = '0']">
        <xsl:value-of select="concat('This refill and',' 1 ','more refills')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat('This refill and ', NewDataSet/Message/Body/NewRx/MedicationPrescribed/Refills/Value,' more refills')" />
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
