<?xml version="1.0"?>
<!-- DWXMLSource="CDA_Mickey_Mouse_92520134444204.xml" -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:n3="http://www.w3.org/1999/xhtml" xmlns:n1="urn:hl7-org:v3"
xmlns:n2="urn:hl7-org:v3/meta/voc" xmlns:voc="urn:hl7-org:v3/voc" xmlns:sdtc="urn:hl7-org:sdtc" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <xsl:output method="html" indent="yes" version="4.01" encoding="ISO-8859-1" doctype-public="-//W3C//DTD HTML 4.01//EN"/>

  <!-- CDA document -->
  <xsl:variable name="javascript-injection-warning">WARNING: Javascript injection attempt detected in source CDA document. Terminating</xsl:variable>
  <xsl:variable name="malicious-content-warning">WARNING: Potentially malicious content found in CDA document.</xsl:variable>
  <xsl:variable name="lc" select="'abcdefghijklmnopqrstuvwxyz'" />
  <xsl:variable name="uc" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />
  <xsl:variable name="simple-sanitizer-match">
    <xsl:text>&#10;&#13;&#34;&#39;&#58;&#59;&#63;&#96;&#123;&#125;&#8220;&#8221;&#8222;&#8218;&#8217;</xsl:text>
  </xsl:variable>

  <xsl:variable name="simple-sanitizer-replace" select="'***************'"/>
  <xsl:variable name="tableWidth">50%</xsl:variable>

  <xsl:variable name="title">
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:title">
        <xsl:value-of select="/n1:ClinicalDocument/n1:title"/>
      </xsl:when>
      <xsl:otherwise>Clinical Document</xsl:otherwise>
    </xsl:choose>
  </xsl:variable>

  <!--<xsl:variable name="Confi">
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:confidentialityCode">
        <xsl:value-of select="concat('(Confidentiality:',/n1:ClinicalDocument/n1:confidentialityCode/@displayName)"/>
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:variable>-->

  <xsl:variable name="Confi">
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:confidentialityCode">
        <xsl:if test="/n1:ClinicalDocument/n1:confidentialityCode/@displayName">
          <xsl:if test="/n1:ClinicalDocument/n1:confidentialityCode/@displayName!=''">
            <xsl:value-of select="concat(' (Confidentiality:',/n1:ClinicalDocument/n1:confidentialityCode/@displayName,')')"/>
          </xsl:if>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
  </xsl:variable>

  <!--<xsl:variable name="Confi" select="/n1:ClinicalDocument/n1:confidentialityCode/@displayName">
    <xsl:if test="$Confi">
      <xsl:choose>
        <xsl:when test="$Confi!=''">
          <xsl:value-of select="concat('(Confidentiality:',/n1:ClinicalDocument/n1:confidentialityCode/@displayName)"/>
        </xsl:when>
        
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </xsl:if >
  </xsl:variable>-->

  <xsl:template match="/">
    <xsl:apply-templates select="n1:ClinicalDocument"/>
  </xsl:template>
  <xsl:param name="limit-external-images" select="'yes'"/>
  <!-- A vertical bar separated list of URI prefixes, such as "http://www.example.com|https://www.example.com" -->
  <xsl:param name="external-image-whitelist"/>
  <xsl:template match="n1:ClinicalDocument">
    <html>
      <head>
        <!-- <meta name='Generator' content='&CDA-Stylesheet;'/> -->
        <xsl:comment>
          Do NOT edit this HTML directly, it was generated via an XSLt
          transformation from the original release 2 CDA Document.
        </xsl:comment>
        <title>
          <xsl:value-of select="$title"/>
        </title>

        <style type="text/css">



          h1 {
          margin:2px;
          padding:0px;
          }

          h3 {
          margin:5px;
          padding:0px;
          }

          body
          {
          border: 0;
          font-family: arial;
          font-size: 12px;
          color: #0069aa;
          background: #fefdfd;
          margin:0px;
          padding:8px;
          }

          table {
          border-collapse: collapse;
          border-spacing: 0;
          max-width: 100%;
          margin-bottom:20px;
          }

          .table tr, .table td {
          border: 1px solid #DDDDDD;
          line-height: 17px;
          padding: 5px;
          text-align: left;
          vertical-align: top;
          }

          li{
          padding: 5px;
          text-outline:none;
          }

          ul li a:link
          {
          font-size: 14px;
          font-family:Arial, sans-serif;
          color : #026CAA;
          font-weight: bold;
          text-decoration:none;
          }

          ul li a:hover
          {
          font-size: 14px;
          font-family: Arial, sans-serif;
          color : #F1A812;
          font-weight: bold;
          text-decoration:none;
          }

          ul li a:active
          {
          font-size: 14px;
          font-family:  Arial, sans-serif;
          color : #000;
          font-weight: bold;
          text-decoration:none;
          }

          .table-bordered-header {
          border: 2px solid #0072BC;
          }

          th
          {
          background:#E9E9E9;
          color: #000;
          font-size: 12px;
          padding: 2px;
          text-align: left;
          border-left: 1px solid #c7c7c7;
          }

          .page-header{
          background-color: #E9E9E9;
          border-bottom: 1px solid #DDDDDD;
          margin: 0 0 0px;
          padding: 5px 8px 5px;
          }

          .logoCenter{
          background: none repeat scroll 0 0 #026CAA;
          color: #FFFFFF;
          filter: none;
          padding: 1px;
          }

          .contenth2{
          font-size: 16px;
          font-family: Arial, sans-serif;
          color: #333333;
          }

          .contenth3{
          font-size: 24px;
          font-family:  Arial, sans-serif;
          }

          a:link
          {
          font-size: 16px;
          font-family:  Arial, sans-serif;
          color : #000;
          font-weight: bold;
          text-decoration:none;
          }

          a:visited
          {
          font-size: 16px;
          font-family: Arial, sans-serif;
          color : #000;
          font-weight: bold;
          text-decoration:none;
          }

          a:active
          {
          color: #000;
          text-decoration:none;
          }

          a:hover
          {
          font-size: 16px;
          font-family: Arial, sans-serif;
          color : #F1A812;
          font-weight: bold;
          text-decoration:none;
          }

        </style>
      </head>
      <xsl:comment>

      </xsl:comment>
      <body>

        <!-- <img src="SageLogo.gif" align="right"/> -->

        <div class="logoCenter">
          <h1 align="center" class="contenth3"  >
            <xsl:value-of select="concat($title,$Confi)"/>
          </h1>

          <xsl:call-template name="generatedline"/>
        </div>

        <table width='100%' class='table table-condensed table-bordered-header'   >
          <xsl:variable name="patientRole" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole"/>
          <xsl:variable name="assignedAuthor" select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor"/>
          <tr padding-top="15px" >

            <td style="padding-left:15px" width='12%' valign="top">
              <xsl:text>Patient: </xsl:text>
            </td>
            <td width='38%' valign="top"   >
              <b>
                <xsl:variable name="pname" select="$patientRole/n1:patient/n1:name"/>
                <xsl:for-each select="$pname">
                  <xsl:choose>
                    <xsl:when test="@use='L'">
                      <xsl:if test="./n1:prefix">
                        <xsl:text> </xsl:text>
                        <xsl:value-of select="./n1:prefix"/>
                        <xsl:text> </xsl:text>
                      </xsl:if>
                      <xsl:for-each select="./n1:given">
                        <xsl:if test="@qualifier">
                          <xsl:text>(</xsl:text>
                        </xsl:if>
                        <xsl:value-of select="."/>
                        <xsl:if test="@qualifier">
                          <xsl:text>)</xsl:text>
                        </xsl:if>
                        <xsl:text> </xsl:text>
                      </xsl:for-each>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="./n1:family"/>
                      <xsl:if test="./n1:suffix">
                        <xsl:text>,</xsl:text>
                        <xsl:value-of select="./n1:suffix"/>
                      </xsl:if>
                    </xsl:when>
                    <xsl:otherwise>
                      <br/>
                      <xsl:text> Previous Name: </xsl:text>
                      <xsl:if test="./n1:prefix">
                        <xsl:text> </xsl:text>
                        <xsl:value-of select="./n1:prefix"/>
                        <xsl:text> </xsl:text>
                      </xsl:if>
                      <xsl:for-each select="./n1:given">
                        <xsl:value-of select="."/>
                        <xsl:text> </xsl:text>
                      </xsl:for-each>
                      <xsl:text> </xsl:text>
                      <xsl:value-of select="./n1:family"/>
                      <xsl:if test="./n1:suffix">
                        <xsl:text>,</xsl:text>
                        <xsl:value-of select="./n1:suffix"/>
                      </xsl:if>
                      <xsl:text></xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:for-each>

                <xsl:if test="$patientRole/n1:addr">
                  <xsl:call-template name="getAddress">
                    <xsl:with-param name="addr" select="$patientRole/n1:addr"/>
                  </xsl:call-template>
                </xsl:if>

                <xsl:if test="$patientRole/n1:telecom">
                  <xsl:for-each select="$patientRole/n1:telecom">
                    <xsl:call-template name="getTelecom">
                      <xsl:with-param name="telecom"
											   select="."/>
                    </xsl:call-template>
                  </xsl:for-each>
                </xsl:if>

                <xsl:call-template name="getCode">
                  <xsl:with-param name="code" select="$patientRole/n1:id"/>
                </xsl:call-template>
              </b>
            </td>
            <td width="12%"  valign="top">
              <xsl:text>Provider : </xsl:text>
            </td>
            <td width="38%" valign="top">
              <b>
                <xsl:call-template name="getName">
                  <xsl:with-param name="name" select="$assignedAuthor/n1:assignedPerson/n1:name"/>
                </xsl:call-template>

                <xsl:if test="$assignedAuthor/n1:addr">
                  <xsl:call-template name="getAddress">
                    <xsl:with-param name="addr" select="$assignedAuthor/n1:addr"/>
                  </xsl:call-template>
                </xsl:if>

                <xsl:if test="$assignedAuthor/n1:telecom">
                  <xsl:for-each select="$assignedAuthor/n1:telecom">
                    <xsl:call-template name="getTelecom">
                      <xsl:with-param name="telecom"
											   select="."/>
                    </xsl:call-template>
                  </xsl:for-each>
                </xsl:if>
              </b>
            </td>
          </tr>
          <tr>
            <td style="padding-left:15px" width='12%' valign="top" >
              <xsl:text>Birthdate: </xsl:text>
            </td>
            <td width='35%' valign="top">
              <b>
                <xsl:if test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:birthTime/@value">
                  <xsl:call-template name="formatDate">
                    <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:birthTime/@value"/>
                  </xsl:call-template>
                </xsl:if>
              </b>
            </td>
            <td width="12%" valign="top">Marital Status: </td>
            <td width="38%" valign="top">
              <b>
                <xsl:variable name="maritalStatus" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:maritalStatusCode/@displayName"/>
                <xsl:choose>
                  <xsl:when test="$maritalStatus">
                    <xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:maritalStatusCode/@displayName"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:text>Information not available</xsl:text>
                  </xsl:otherwise>
                </xsl:choose>
              </b>
            </td>
          </tr>
          <tr>
            <xsl:if test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode | (/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode) | (/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/sdtc:raceCode)">

              <td style="padding-left:15px" width='12%' valign="top" >

                <xsl:text>Race: </xsl:text>

              </td>
              <td width="38%" valign="top">
                <b>
                  <xsl:choose>
                    <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode | /n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/sdtc:raceCode">
                      <xsl:for-each select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode |/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/n1:translation | (/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/sdtc:raceCode)">
                        <xsl:call-template name="show-race"/>
                      </xsl:for-each>
                    </xsl:when>
                    <!-- <xsl:otherwise>
                        <xsl:text>Information not available</xsl:text>
                      </xsl:otherwise>-->
                  </xsl:choose>
                </b>
              </td>
              <td width="12%" valign="top">

                <xsl:text>Ethnicity: </xsl:text>

              </td>
              <td width="38%" valign="top">
                <b>
                  <xsl:choose>
                    <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode | /n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/sdtc:ethnicGroupCode">
                      <xsl:for-each select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode | (/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/sdtc:ethnicGroupCode)">
                        <xsl:call-template name="show-ethnicity"/>

                      </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:text>Information not available</xsl:text>
                    </xsl:otherwise>
                  </xsl:choose>
                </b>
              </td>

            </xsl:if>
          </tr>
          <tr>
            <td style="padding-left:15px" width='12%'  valign="top">
              <xsl:text>Sex: </xsl:text>
            </td>
            <td width='35%' valign="top">

              <b>
                <xsl:variable name="Genwithcode" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:administrativeGenderCode/@code"/>
                <xsl:variable name="Genunk" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:administrativeGenderCode/@nullFlavor"/>
                <xsl:if test="$Genwithcode">
                  <xsl:choose>
                    <xsl:when test="$Genwithcode='M'">Male</xsl:when>
                    <xsl:when test="$Genwithcode='F'">Female</xsl:when>
                    <xsl:when test="$Genwithcode='UN'">Other</xsl:when>
                  </xsl:choose>

                </xsl:if>
                <xsl:if test="$Genunk">
                  <xsl:choose>
                    <xsl:when test="$Genunk='UNK'">UnKnown</xsl:when>

                  </xsl:choose>
                </xsl:if>

              </b>
            </td>

            <xsl:variable name="language" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode"/>
            <xsl:choose>
              <xsl:when test="$language">
                <td width="12%"   valign="top">Language: </td>
                <td width="38%" valign="top">
                  <b>
                    <xsl:apply-templates select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode"/>
                  </b>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td width="12%">&#160;</td>
                <td width="38%">&#160;</td>
              </xsl:otherwise>
            </xsl:choose>

          </tr>

          <xsl:for-each select="n1:componentOf/n1:encompassingEncounter">

            <tr>
              <td style="padding-left:15px" width='12%' valign="top" >

                <xsl:text>Date of Visit: </xsl:text>

              </td>
              <td width="38%" valign="top">
                <b>
                  <xsl:if test="n1:effectiveTime">
                    <xsl:choose>
                      <xsl:when test="n1:effectiveTime/@value">
                        <xsl:text>&#160;at&#160;</xsl:text>
                        <xsl:call-template name="show-time">
                          <xsl:with-param name="datetime" select="n1:effectiveTime"/>
                        </xsl:call-template>
                      </xsl:when>
                      <xsl:when test="n1:effectiveTime/n1:low/@value">

                        <xsl:call-template name="show-time">
                          <xsl:with-param name="datetime" select="n1:effectiveTime/n1:low"/>
                        </xsl:call-template>

                      </xsl:when>
                    </xsl:choose>
                  </xsl:if>
                </b>
              </td>
              <td width='12%'  valign="top" >

                <xsl:text>Location of Visit: </xsl:text>

              </td>
              <td width="38%" valign="top">
                <b>
                  <xsl:if test="n1:location/n1:healthCareFacility">
                    <!-- <xsl:choose>-->
                    <xsl:if test="n1:location/n1:healthCareFacility/n1:location/n1:name">
                      <xsl:call-template name="getName">
                        <xsl:with-param name="name" select="n1:location/n1:healthCareFacility/n1:location/n1:name"/>
                      </xsl:call-template>

                    </xsl:if>
                    <xsl:if test="n1:location/n1:healthCareFacility/n1:location/n1:addr">
                      <xsl:call-template name="getAddress">
                        <xsl:with-param name="addr" select="n1:location/n1:healthCareFacility/n1:location/n1:addr"/>
                      </xsl:call-template>
                    </xsl:if>
                    <xsl:if test="n1:location/n1:healthCareFacility/n1:location/n1:telecom">
                      <xsl:for-each select="n1:location/n1:healthCareFacility/n1:location/n1:telecom">
                        <xsl:call-template name="getTelecom">
                          <xsl:with-param name="telecom"
                             select="."/>
                        </xsl:call-template>
                      </xsl:for-each>
                    </xsl:if>
                    <!--<xsl:when test="n1:location/n1:healthCareFacility/n1:code">
                              <xsl:call-template name="show-code">
                                <xsl:with-param name="code" select="n1:location/n1:healthCareFacility/n1:code"/>
                              </xsl:call-template>
                            </xsl:when>-->

                    <!-- </xsl:choose>-->
                  </xsl:if>
                </b>
              </td>
            </tr>
            <tr >
              <td style="padding-left:15px" width='12%' valign="top" >
                <xsl:text>Referral Provider: </xsl:text>
              </td>
              <td width="38%" valign="top" colspan="3">
                <b>
                  <xsl:call-template name="show-assignedEntity">
                    <xsl:with-param name="asgnEntity" select="n1:responsibleParty/n1:assignedEntity"/>
                  </xsl:call-template>
                  <xsl:if test="string-length(n1:responsibleParty/n1:assignedEntity/n1:addr/n1:streetAddressLine)>0">
                    <xsl:text>,&#160;</xsl:text>
                  </xsl:if>
                  <xsl:call-template name="show-contactInfo">
                    <xsl:with-param name="contact" select="n1:responsibleParty/n1:assignedEntity"/>
                  </xsl:call-template>
                </b>
              </td>

              <!--<td  width='15%' valign="top" >

                <xsl:text>Office contact info: </xsl:text>

              </td>

              <td width="35%" valign="top">
                <b>
                 
                </b>
              </td>-->


            </tr>

            <!--  <b>
                  <xsl:call-template name="observation">
                    <xsl:with-param name="purpuse" select="n1:component/n1:observation"/>
                  </xsl:call-template>
                </b>-->

            <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody">
              <xsl:for-each select="n1:component/n1:section">
                <xsl:if test="n1:templateId/@root='2.16.840.1.113883.3.445.22'">
                  <tr >
                    <td style="padding-left:15px" width='12%' valign="top" >
                      <xsl:text>Purpose of Use: </xsl:text>
                    </td>
                    <td width="38%" valign="top" colspan="3">
                      <xsl:call-template name="section-text"/>
                    </td>
                  </tr>
                </xsl:if>


                <xsl:if test="n1:templateId/@root!='2.16.840.1.113883.3.445.22'">
                  <xsl:for-each select="n1:entry/n1:organizer/n1:component/n1:observation">
                    <xsl:if test="n1:templateId/@root='2.16.840.1.113883.3.445.22'">
                      <tr >
                        <td style="padding-left:15px" width='12%' valign="top" >
                          <xsl:text>Purpose of Use: </xsl:text>
                        </td>
                        <td width="38%" valign="top" colspan="3">

                          <xsl:value-of select="n1:value"/>

                        </td>
                      </tr>
                    </xsl:if>
                  </xsl:for-each>
                </xsl:if>
              </xsl:for-each>
            </xsl:for-each>


            <!--<td  width='15%' valign="top" >

                <xsl:text>Office contact info: </xsl:text>

              </td>

              <td width="35%" valign="top">
                <b>
                 
                </b>
              </td>-->





          </xsl:for-each>

          <!--<tr>
            <xsl:variable name="guardian" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:guardian"/>

            <td style="padding-left:20px" width="15%" valign="top">Guardian: </td>
            <td width="35%" valign="top">
              <b>
                <xsl:call-template name="getGuardian">
                  <xsl:with-param name="guardian" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:guardian"/>
                </xsl:call-template>
              </b>
            </td>

            <xsl:variable name="emergencycontact" select="/n1:ClinicalDocument/n1:participant[@typeCode='IND']/n1:associatedEntity[@classCode='ECON']"/>

            <td width="15%" align="right" valign="top">Emergency Contact: </td>
            <td width="35%" valign="top" >
              <b>
                <xsl:call-template name="getParticipant">
                  <xsl:with-param name="participant" select="/n1:ClinicalDocument/n1:participant[@typeCode='IND']/n1:associatedEntity[@classCode='ECON']"/>
                </xsl:call-template>
              </b>
            </td>

          </tr>-->
        </table>

        <!-- <xsl:call-template name="documentationOf"/>-->

        <div>
          <h3 class="contenth2 page-header">
            <xsl:text> Office Contact </xsl:text>
          </h3>
          <div style="margin-top:-1px;">


            <table width='100%'>
              <xsl:variable name="patdataent" select="/n1:ClinicalDocument/n1:dataEnterer"/>
              <tr padding-top="15px" >

                <td style="padding-left:15px" width='12%' valign="top">

                  <xsl:call-template name="getName">
                    <xsl:with-param name="name"
                     select="/n1:ClinicalDocument/n1:dataEnterer/n1:assignedEntity/n1:assignedPerson/n1:name"/>
                  </xsl:call-template>


                  <xsl:call-template name="getAddress">
                    <xsl:with-param name="addr" select="$patdataent/n1:assignedEntity/n1:addr"/>
                  </xsl:call-template>

                  <!-- <xsl:value-of  select="$patdataent/n1:assignedEntity/n1:addr/n1:streetAddressLine"/>

                  <br/>
                  <xsl:value-of select="$patdataent/n1:assignedEntity/n1:addr/n1:city"/>
                  <br/>
                  <xsl:value-of select="$patdataent/n1:assignedEntity/n1:addr/n1:state"/>
                  <br/>
                  <xsl:value-of select="$patdataent/n1:assignedEntity/n1:addr/n1:postalCode"/>
                  <br/>
                  <xsl:value-of select="$patdataent/n1:assignedEntity/n1:addr/n1:country"/>-->

                  <xsl:if test="$patdataent/n1:assignedEntity/n1:telecom">
                    <xsl:for-each select="$patdataent/n1:assignedEntity/n1:telecom">
                      <xsl:call-template name="getTelecom">
                        <xsl:with-param name="telecom"
                           select="."/>
                      </xsl:call-template>
                    </xsl:for-each>
                  </xsl:if>

                </td>

              </tr >
            </table>














          </div>

        </div>





        <div>
          <h3 class="contenth2 page-header">
            <xsl:text> Care Team </xsl:text>
          </h3>
          <div style="margin-top:-1px;">
            <xsl:call-template name="documentationOf"/>

          </div>

        </div>



        <!--<div>
          <h3 class="contenth2 page-header">
            Table of Contents
          </h3>

          <ul >
            <xsl:for-each select="n1:component/n1:structuredBody/n1:component/n1:section/n1:title">
              <li>
                <a href="#{generate-id(.)}">
                  <xsl:value-of select="."/>
                </a>
              </li>
            </xsl:for-each>
          </ul>

        </div>
        <xsl:apply-templates select="n1:component/n1:structuredBody"/>-->


        <xsl:if test="not(//n1:nonXMLBody)">
          <xsl:if test="count(/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section]) &gt; 1">
            <xsl:call-template name="make-tableofcontents"/>

          </xsl:if>
        </xsl:if>

        <!-- produce human readable document content -->
        <xsl:apply-templates select="n1:component/n1:structuredBody|n1:component/n1:nonXMLBody"/>
        <br/>
        <br/>
      </body>
    </html>
  </xsl:template>
  <xsl:template name="section-text">
    <div class="sectiontext">
      <xsl:apply-templates select="n1:text"/>
    </div>
  </xsl:template>
  <xsl:template name="observation">
    <xsl:if test="n1:templateId/@root='2.16.840.1.113883.3.445.22'">
      <xsl:value-of select="n1:value"/>
    </xsl:if>
  </xsl:template>
  <xsl:template name="make-tableofcontents">
    <h3 class="contenth2 page-header">
      Table of Contents
    </h3>
    <ul>
      <xsl:for-each select="n1:component/n1:structuredBody/n1:component/n1:section/n1:title">
        <li>
          <a href="#{generate-id(.)}">
            <xsl:value-of select="."/>
          </a>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
  <xsl:template match='n1:component/n1:nonXMLBody'>
    <xsl:choose>
      <!-- if there is a reference, use that in an IFRAME -->
      <xsl:when test='n1:text/n1:reference'>
        <xsl:variable name="source" select="string(n1:text/n1:reference/@value)"/>
        <xsl:variable name="lcSource" select="translate($source, $uc, $lc)"/>
        <xsl:variable name="scrubbedSource" select="translate($source, $simple-sanitizer-match, $simple-sanitizer-replace)"/>
        <xsl:message>
          <xsl:value-of select="$source"/>, <xsl:value-of select="$lcSource"/>
        </xsl:message>
        <xsl:choose>
          <xsl:when test="contains($lcSource,'javascript')">
            <p>
              <xsl:value-of select="$javascript-injection-warning"/>
            </p>
            <xsl:message>
              <xsl:value-of select="$javascript-injection-warning"/>
            </xsl:message>
          </xsl:when>
          <xsl:when test="not($source = $scrubbedSource)">
            <p>
              <xsl:value-of select="$malicious-content-warning"/>
            </p>
            <xsl:message>
              <xsl:value-of select="$malicious-content-warning"/>
            </xsl:message>
          </xsl:when>
          <xsl:otherwise>
            <iframe name='nonXMLBody' id='nonXMLBody' WIDTH='80%' HEIGHT='600' src='{$source}' sandbox=""/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:when test='n1:text/@mediaType="text/plain"'>
        <pre>
          <xsl:value-of select='n1:text/text()'/>
        </pre>
      </xsl:when>
      <xsl:otherwise>
        <pre>Cannot display the text</pre>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="show-actClassCode">
    <xsl:param name="clsCode"/>
    <xsl:choose>
      <xsl:when test=" $clsCode = 'ACT' ">
        <xsl:text>healthcare service</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'ACCM' ">
        <xsl:text>accommodation</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'ACCT' ">
        <xsl:text>account</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'ACSN' ">
        <xsl:text>accession</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'ADJUD' ">
        <xsl:text>financial adjudication</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'CONS' ">
        <xsl:text>consent</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'CONTREG' ">
        <xsl:text>container registration</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'CTTEVENT' ">
        <xsl:text>clinical trial timepoint event</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'DISPACT' ">
        <xsl:text>disciplinary action</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'ENC' ">
        <xsl:text>encounter</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'INC' ">
        <xsl:text>incident</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'INFRM' ">
        <xsl:text>inform</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'INVE' ">
        <xsl:text>invoice element</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'LIST' ">
        <xsl:text>working list</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'MPROT' ">
        <xsl:text>monitoring program</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'PCPR' ">
        <xsl:text>care provision</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'PROC' ">
        <xsl:text>procedure</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'REG' ">
        <xsl:text>registration</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'REV' ">
        <xsl:text>review</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'SBADM' ">
        <xsl:text>substance administration</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'SPCTRT' ">
        <xsl:text>speciment treatment</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'SUBST' ">
        <xsl:text>substitution</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'TRNS' ">
        <xsl:text>transportation</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'VERIF' ">
        <xsl:text>verification</xsl:text>
      </xsl:when>
      <xsl:when test=" $clsCode = 'XACT' ">
        <xsl:text>financial transaction</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- convert to upper case -->
  <xsl:template name="caseUp">
    <xsl:param name="data"/>
    <xsl:if test="$data">
      <xsl:value-of select="translate($data,'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
    </xsl:if>
  </xsl:template>
  <!-- convert first character to upper case -->
  <xsl:template name="firstCharCaseUp">
    <xsl:param name="data"/>
    <xsl:if test="$data">
      <xsl:call-template name="caseUp">
        <xsl:with-param name="data" select="substring($data,1,1)"/>
      </xsl:call-template>
      <xsl:value-of select="substring($data,2)"/>
    </xsl:if>
  </xsl:template>


  <!-- Show Name with list IDs below -->
  <xsl:template name="show-nameAndId">
    <xsl:param name="name" />
    <xsl:param name="id" />

    <!-- Show the name -->
    <xsl:if test="$name">
      <xsl:call-template name="getName">
        <xsl:with-param name="name" select="$name"/>
      </xsl:call-template>
    </xsl:if>

    <xsl:if test="$name">
      <br />
    </xsl:if>


  </xsl:template>



  <!-- show participationFunction -->
  <xsl:template name="show-participationFunction">
    <xsl:param name="pFunction"/>
    <xsl:choose>
      <!-- From the HL7 v3 ParticipationFunction code system -->
      <xsl:when test=" $pFunction = 'ADMPHYS' ">
        <xsl:text>(admitting physician)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'ANEST' ">
        <xsl:text>(anesthesist)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'ANRS' ">
        <xsl:text>(anesthesia nurse)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'ATTPHYS' ">
        <xsl:text>(attending physician)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'DISPHYS' ">
        <xsl:text>(discharging physician)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'FASST' ">
        <xsl:text>(first assistant surgeon)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'MDWF' ">
        <xsl:text>(midwife)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'NASST' ">
        <xsl:text>(nurse assistant)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'PCP' ">
        <xsl:text>(primary care physician)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'PRISURG' ">
        <xsl:text>(primary surgeon)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'RNDPHYS' ">
        <xsl:text>(rounding physician)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'SASST' ">
        <xsl:text>(second assistant surgeon)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'SNRS' ">
        <xsl:text>(scrub nurse)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'TASST' ">
        <xsl:text>(third assistant)</xsl:text>
      </xsl:when>
      <!-- From the HL7 v2 Provider Role code system (2.16.840.1.113883.12.443) which is used by HITSP -->
      <xsl:when test=" $pFunction = 'CP' ">
        <xsl:text> (Consulting Provider)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'AT' ">
        <xsl:text> (Attending)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'AD' ">
        <xsl:text> (Admitting)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'FHCP' ">
        <xsl:text> (Family Health Care Professional)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'PP' ">
        <xsl:text> (Primary Care Provider)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'RT' ">
        <xsl:text> (Referred to Provider)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'RP' ">
        <xsl:text> (Referring Provider)</xsl:text>
      </xsl:when>
      <xsl:when test=" $pFunction = 'MP' ">
        <xsl:text> (medical home provider)</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!-- show participationType -->
  <xsl:template name="show-participationType">
    <xsl:param name="ptype"/>
    <xsl:choose>
      <xsl:when test=" $ptype='PPRF' ">
        <xsl:text>primary performer</xsl:text>
      </xsl:when>
      <xsl:when test=" $ptype='PRF' ">
        <xsl:text>performer</xsl:text>
      </xsl:when>
      <xsl:when test=" $ptype='VRF' ">
        <xsl:text>verifier</xsl:text>
      </xsl:when>
      <xsl:when test=" $ptype='SPRF' ">
        <xsl:text>secondary performer</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="documentationOf">
    <xsl:if test="n1:documentationOf">

      <table  class='table' width='100%'>
        <tbody>
          <xsl:for-each select="n1:documentationOf">

            <!--<xsl:if test="n1:serviceEvent/@classCode and n1:serviceEvent/n1:code">

              <xsl:variable name="displayName">
                <xsl:call-template name="show-actClassCode">
                  <xsl:with-param name="clsCode" select="n1:serviceEvent/@classCode"/>
                </xsl:call-template>
              </xsl:variable>
              <xsl:if test="$displayName">
                <tr>
                  <td width='15%' valign="top" >
                    <span class="td_label">
                      <xsl:call-template name="firstCharCaseUp">
                        <xsl:with-param name="data" select="$displayName"/>
                      </xsl:call-template>
                    </span>
                  </td>
                  <td width='35%' valign="top" colspan="3">
                    <xsl:call-template name="show-code">
                      <xsl:with-param name="code" select="n1:serviceEvent/n1:code"/>
                    </xsl:call-template>
                    <xsl:if test="n1:serviceEvent/n1:effectiveTime">
                      <xsl:choose>
                        <xsl:when test="n1:serviceEvent/n1:effectiveTime/@value">
                          <xsl:text>&#160;at&#160;</xsl:text>
                          <xsl:call-template name="show-time">
                            <xsl:with-param name="datetime" select="n1:serviceEvent/n1:effectiveTime"/>
                          </xsl:call-template>
                        </xsl:when>
                        <xsl:when test="n1:serviceEvent/n1:effectiveTime/n1:low">
                          <xsl:text>&#160;from&#160;</xsl:text>
                          <xsl:call-template name="show-time">
                            <xsl:with-param name="datetime" select="n1:serviceEvent/n1:effectiveTime/n1:low"/>
                          </xsl:call-template>
                          <xsl:if test="n1:serviceEvent/n1:effectiveTime/n1:high">
                            <xsl:text> to </xsl:text>
                            <xsl:call-template name="show-time">
                              <xsl:with-param name="datetime" select="n1:serviceEvent/n1:effectiveTime/n1:high"/>
                            </xsl:call-template>
                          </xsl:if>
                        </xsl:when>
                      </xsl:choose>
                    </xsl:if>
                  </td>
                </tr>
              </xsl:if>
            </xsl:if>-->
            <!-- Documentation Of / Performer -->
            <xsl:for-each select="n1:serviceEvent/n1:performer">
              <xsl:variable name="displayName">
                <xsl:call-template name="show-participationType">

                  <xsl:with-param name="ptype" select="@typeCode"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>

              </xsl:variable>
              <tr>

                <td width="30%" >


                  <xsl:call-template name="show-assignedEntity">
                    <xsl:with-param name="asgnEntity" select="n1:assignedEntity"/>
                  </xsl:call-template>
                  <!--<xsl:text>&#160;</xsl:text>-->

                  <xsl:if test="n1:assignedEntity/n1:addr">
                    <xsl:call-template name="getAddress">
                      <xsl:with-param name="addr" select="n1:assignedEntity/n1:addr"/>
                    </xsl:call-template>

                  </xsl:if>
                  <xsl:if test="n1:assignedEntity/n1:telecom">
                    <xsl:for-each select="n1:assignedEntity/n1:telecom">
                      <xsl:call-template name="getTelecom">
                        <xsl:with-param name="telecom"
                             select="."/>
                      </xsl:call-template>

                    </xsl:for-each>
                  </xsl:if>
                  <xsl:text> </xsl:text>
                  <xsl:text> </xsl:text>
                  <xsl:if test="n1:functionCode/@code">
                    <xsl:call-template name="show-participationFunction">
                      <xsl:with-param name="pFunction" select="n1:functionCode/@code"/>
                    </xsl:call-template>
                  </xsl:if>
                </td>

              </tr>
            </xsl:for-each>
          </xsl:for-each>
        </tbody>
      </table>
    </xsl:if>
  </xsl:template>
  <!--<xsl:template name="show-language">
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'eng'">
        <xsl:text>English</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'spa'">
        <xsl:text>Spanish</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'en'">
        <xsl:text>English</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'chi (B)'">
        <xsl:text>Chinese</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'fre (B)'">
        <xsl:text>French</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'ger (B)'">
        <xsl:text>German</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'el'">
        <xsl:text>Greek</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'guj'">
        <xsl:text>Gujarati</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'jpn'">
        <xsl:text>Japanese</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code = 'mar'">
        <xsl:text>Marathi</xsl:text>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@nullFlavor = 'ASKU'">
        <xsl:text>Declined to specify</xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:languageCommunication/n1:languageCode/@code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>-->
  <xsl:template match="n1:languageCode">
    <xsl:choose>

      <xsl:when test="@code='mnc'">Manchu</xsl:when>
      <xsl:when test="@code='alt'">Southern Altai</xsl:when>
      <xsl:when test="@code='hmn'">Hmong</xsl:when>
      <xsl:when test="@code='ban'">Balinese</xsl:when>
      <xsl:when test="@code='kaa'">Kara-Kalpak</xsl:when>
      <xsl:when test="@code='arc'">Official Aramaic (700-300 BCE); Imperial Aramaic (700-300 BCE)</xsl:when>
      <xsl:when test="@code='pag'">Pangasinan</xsl:when>
      <xsl:when test="@code='crp'">Creoles and pidgins (Other)</xsl:when>
      <xsl:when test="@code='wak'">Wakashan languages</xsl:when>
      <xsl:when test="@code='him'">Himachali</xsl:when>
      <xsl:when test="@code='zgh'">Standard Moroccan Tamazight</xsl:when>
      <xsl:when test="@code='tyv'">Tuvinian</xsl:when>
      <xsl:when test="@code='tig'">Tigre</xsl:when>
      <xsl:when test="@code='sla'">Slavic (Other)</xsl:when>
      <xsl:when test="@code='jpr'">Judeo-Persian</xsl:when>
      <xsl:when test="@code='smi'">Sami languages (Other)</xsl:when>
      <xsl:when test="@code='vol'">Volapük</xsl:when>
      <xsl:when test="@code='sux'">Sumerian</xsl:when>
      <xsl:when test="@code='nym'">Nyamwezi</xsl:when>
      <xsl:when test="@code='wal'">Walamo</xsl:when>
      <xsl:when test="@code='phi'">Philippine (Other)</xsl:when>
      <xsl:when test="@code='osa'">Osage</xsl:when>
      <xsl:when test="@code='cpf'">Creoles and pidgins, French-based (Other)</xsl:when>
      <xsl:when test="@code='chr'">Cherokee</xsl:when>
      <xsl:when test="@code='nld'">Dutch; Flemish</xsl:when>
      <xsl:when test="@code='ber'">Berber (Other)</xsl:when>
      <xsl:when test="@code='tib'">Tibetan</xsl:when>
      <xsl:when test="@code='phn'">Phoenician</xsl:when>
      <xsl:when test="@code='peo'">Persian, Old (ca.600-400 B.C.)</xsl:when>
      <xsl:when test="@code='chy'">Cheyenne</xsl:when>
      <xsl:when test="@code='ath'">Athapascan languages</xsl:when>
      <xsl:when test="@code='ger'">German</xsl:when>
      <xsl:when test="@code='ell'">Greek, Modern (1453-)</xsl:when>
      <xsl:when test="@code='zap'">Zapotec</xsl:when>
      <xsl:when test="@code='mdr'">Mandar</xsl:when>
      <xsl:when test="@code='chu'">Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church  Slavonic</xsl:when>
      <xsl:when test="@code='sog'">Sogdian</xsl:when>
      <xsl:when test="@code='kos'">Kosraean</xsl:when>
      <xsl:when test="@code='bla'">Siksika</xsl:when>
      <xsl:when test="@code='gmh'">German, Middle High (ca.1050-1500)</xsl:when>
      <xsl:when test="@code='ach'">Acoli</xsl:when>
      <xsl:when test="@code='jrb'">Judeo-Arabic</xsl:when>
      <xsl:when test="@code='xal'">Kalmyk; Oirat</xsl:when>
      <xsl:when test="@code='mai'">Maithili</xsl:when>
      <xsl:when test="@code='fan'">Fang</xsl:when>
      <xsl:when test="@code='tsi'">Tsimshian</xsl:when>
      <xsl:when test="@code='aus'">Australian languages</xsl:when>
      <xsl:when test="@code='kum'">Kumyk</xsl:when>
      <xsl:when test="@code='kbd'">Kabardian</xsl:when>
      <xsl:when test="@code='zun'">Zuni</xsl:when>
      <xsl:when test="@code='kru'">Kurukh</xsl:when>
      <xsl:when test="@code='gaa'">Ga</xsl:when>
      <xsl:when test="@code='lus'">Lushai</xsl:when>
      <xsl:when test="@code='kal'">Kalaallisut; Greenlandic</xsl:when>
      <xsl:when test="@code='mag'">Magahi</xsl:when>
      <xsl:when test="@code='roa'">Romance (Other)</xsl:when>
      <xsl:when test="@code='sem'">Semitic (Other)</xsl:when>
      <xsl:when test="@code='wen'">Sorbian languages</xsl:when>
      <xsl:when test="@code='fre'">French</xsl:when>
      <xsl:when test="@code='bih'">Bihari</xsl:when>
      <xsl:when test="@code='goh'">German, Old High (ca.750-1050)</xsl:when>
      <xsl:when test="@code='car'">Galibi Carib</xsl:when>
      <xsl:when test="@code='srn'">Sranan Tongo</xsl:when>
      <xsl:when test="@code='tog'">Tonga (Nyasa)</xsl:when>
      <xsl:when test="@code='map'">Austronesian (Other)</xsl:when>
      <xsl:when test="@code='egy'">Egyptian (Ancient)</xsl:when>
      <xsl:when test="@code='chb'">Chibcha</xsl:when>
      <xsl:when test="@code='den'">Slave (Athapascan)</xsl:when>
      <xsl:when test="@code='luo'">Luo (Kenya and Tanzania)</xsl:when>
      <xsl:when test="@code='cpe'">Creoles and pidgins, English based (Other)</xsl:when>
      <xsl:when test="@code='ypk'">Yupik languages</xsl:when>
      <xsl:when test="@code='btk'">Batak languages</xsl:when>
      <xsl:when test="@code='sid'">Sidamo</xsl:when>
      <xsl:when test="@code='awa'">Awadhi</xsl:when>
      <xsl:when test="@code='lun'">Lunda</xsl:when>
      <xsl:when test="@code='day'">Land Dayak languages</xsl:when>
      <xsl:when test="@code='bad'">Banda languages</xsl:when>
      <xsl:when test="@code='gsw'">Swiss German; Alemannic; Alsatian</xsl:when>
      <xsl:when test="@code='myn'">Mayan languages</xsl:when>
      <xsl:when test="@code='und'">Undetermined</xsl:when>
      <xsl:when test="@code='khm'">Central Khmer</xsl:when>
      <xsl:when test="@code='fon'">Fon</xsl:when>
      <xsl:when test="@code='alg'">Algonquian languages</xsl:when>
      <xsl:when test="@code='kaw'">Kawi</xsl:when>
      <xsl:when test="@code='ira'">Iranian (Other)</xsl:when>
      <xsl:when test="@code='war'">Waray</xsl:when>
      <xsl:when test="@code='ina'">Interlingua (International Auxiliary Language Association)</xsl:when>
      <xsl:when test="@code='oci'">Occitan (post 1500); Provençal</xsl:when>
      <xsl:when test="@code='sat'">Santali</xsl:when>
      <xsl:when test="@code='lah'">Lahnda</xsl:when>
      <xsl:when test="@code='dra'">Dravidian (Other)</xsl:when>
      <xsl:when test="@code='mdf'">Moksha</xsl:when>
      <xsl:when test="@code='bik'">Bikol</xsl:when>
      <xsl:when test="@code='pal'">Pahlavi</xsl:when>
      <xsl:when test="@code='tkl'">Tokelau</xsl:when>
      <xsl:when test="@code='dsb'">Lower Sorbian</xsl:when>
      <xsl:when test="@code='zxx'">No linguistic content; Not applicable</xsl:when>
      <xsl:when test="@code='fur'">Friulian</xsl:when>
      <xsl:when test="@code='tai'">Tai (Other)</xsl:when>
      <xsl:when test="@code='cho'">Choctaw</xsl:when>
      <xsl:when test="@code='ada'">Adangme</xsl:when>
      <xsl:when test="@code='mas'">Masai</xsl:when>
      <xsl:when test="@code='bug'">Buginese</xsl:when>
      <xsl:when test="@code='haw'">Hawaiian</xsl:when>
      <xsl:when test="@code='chn'">Chinook jargon</xsl:when>
      <xsl:when test="@code='sma'">Southern Sami</xsl:when>
      <xsl:when test="@code='kac'">Kachin; Jingpho</xsl:when>
      <xsl:when test="@code='dua'">Duala</xsl:when>
      <xsl:when test="@code='gem'">Germanic (Other)</xsl:when>
      <xsl:when test="@code='cus'">Cushitic (Other)</xsl:when>
      <xsl:when test="@code='kro'">Kru languages</xsl:when>
      <xsl:when test="@code='zha'">Zhuang; Chuang</xsl:when>
      <xsl:when test="@code='kut'">Kutenai</xsl:when>
      <xsl:when test="@code='chm'">Mari</xsl:when>
      <xsl:when test="@code='div'">Divehi; Dhivehi; Maldivian</xsl:when>
      <xsl:when test="@code='sit'">Sino-Tibetan (Other)</xsl:when>
      <xsl:when test="@code='bal'">Baluchi</xsl:when>
      <xsl:when test="@code='chi'">Chinese</xsl:when>
      <xsl:when test="@code='son'">Songhai languages</xsl:when>
      <xsl:when test="@code='cpp'">Creoles and pidgins, Portuguese-based (Other)</xsl:when>
      <xsl:when test="@code='sam'">Samaritan Aramaic</xsl:when>
      <xsl:when test="@code='dak'">Dakota</xsl:when>
      <xsl:when test="@code='ain'">Ainu</xsl:when>
      <xsl:when test="@code='raj'">Rajasthani</xsl:when>
      <xsl:when test="@code='chg'">Chagatai</xsl:when>
      <xsl:when test="@code='gor'">Gorontalo</xsl:when>
      <xsl:when test="@code='inc'">Indic (Other)</xsl:when>
      <xsl:when test="@code='kir'">Kirghiz; Kyrgyz</xsl:when>
      <xsl:when test="@code='scn'">Sicilian</xsl:when>
      <xsl:when test="@code='sus'">Susu</xsl:when>
      <xsl:when test="@code='man'">Mandingo</xsl:when>
      <xsl:when test="@code='gwi'">Gwich'in</xsl:when>
      <xsl:when test="@code='inh'">Ingush</xsl:when>
      <xsl:when test="@code='alb'">Albanian</xsl:when>
      <xsl:when test="@code='moh'">Mohawk</xsl:when>
      <xsl:when test="@code='gba'">Gbaya</xsl:when>
      <xsl:when test="@code='mkh'">Mon-Khmer (Other)</xsl:when>
      <xsl:when test="@code='rup'">Aromanian; Arumanian; Macedo-Romanian</xsl:when>
      <xsl:when test="@code='hup'">Hupa</xsl:when>
      <xsl:when test="@code='snk'">Soninke</xsl:when>
      <xsl:when test="@code='bem'">Bemba</xsl:when>
      <xsl:when test="@code='pus'">Pushto; Pashto</xsl:when>
      <xsl:when test="@code='cop'">Coptic</xsl:when>
      <xsl:when test="@code='nob'">Bokmål, Norwegian; Norwegian Bokmål</xsl:when>
      <xsl:when test="@code='gla'">Gaelic; Scottish Gaelic</xsl:when>
      <xsl:when test="@code='hil'">Hiligaynon</xsl:when>
      <xsl:when test="@code='nai'">North American Indian</xsl:when>
      <xsl:when test="@code='enm'">English, Middle (1100-1500)</xsl:when>
      <xsl:when test="@code='nde'">Ndebele, North; North Ndebele</xsl:when>
      <xsl:when test="@code='vai'">Vai</xsl:when>
      <xsl:when test="@code='chp'">Chipewyan; Dene Suline</xsl:when>
      <xsl:when test="@code='del'">Delaware</xsl:when>
      <xsl:when test="@code='art'">Artificial (Other)</xsl:when>
      <xsl:when test="@code='ace'">Achinese</xsl:when>
      <xsl:when test="@code='was'">Washo</xsl:when>
      <xsl:when test="@code='mga'">Irish, Middle (900-1200)</xsl:when>
      <xsl:when test="@code='nub'">Nubian languages</xsl:when>
      <xsl:when test="@code='nyo'">Nyoro</xsl:when>
      <xsl:when test="@code='sms'">Skolt Sami</xsl:when>
      <xsl:when test="@code='chk'">Chuukese</xsl:when>
      <xsl:when test="@code='men'">Mende</xsl:when>
      <xsl:when test="@code='gil'">Gilbertese</xsl:when>
      <xsl:when test="@code='bho'">Bhojpuri</xsl:when>
      <xsl:when test="@code='bej'">Beja; Bedawiyet</xsl:when>
      <xsl:when test="@code='cel'">Celtic (Other)</xsl:when>
      <xsl:when test="@code='kmb'">Kimbundu</xsl:when>
      <xsl:when test="@code='ang'">English, Old (ca.450-1100)</xsl:when>
      <xsl:when test="@code='bua'">Buriat</xsl:when>
      <xsl:when test="@code='frr'">Northern Frisian</xsl:when>
      <xsl:when test="@code='ota'">Turkish, Ottoman (1500-1928)</xsl:when>
      <xsl:when test="@code='ice'">Icelandic</xsl:when>
      <xsl:when test="@code='mac'">Macedonian</xsl:when>
      <xsl:when test="@code='sai'">South American Indian (Other)</xsl:when>
      <xsl:when test="@code='fro'">French, Old (842-ca.1400)</xsl:when>
      <xsl:when test="@code='nog'">Nogai</xsl:when>
      <xsl:when test="@code='bat'">Baltic (Other)</xsl:when>
      <xsl:when test="@code='pan'">Panjabi; Punjabi</xsl:when>
      <xsl:when test="@code='cmc'">Chamic languages</xsl:when>
      <xsl:when test="@code='pam'">Pampanga; Kapampangan</xsl:when>
      <xsl:when test="@code='nap'">Neapolitan</xsl:when>
      <xsl:when test="@code='srr'">Serer</xsl:when>
      <xsl:when test="@code='mus'">Creek</xsl:when>
      <xsl:when test="@code='anp'">Angika</xsl:when>
      <xsl:when test="@code='paa'">Papuan (Other)</xsl:when>
      <xsl:when test="@code='bnt'">Bantu (Other)</xsl:when>
      <xsl:when test="@code='cau'">Caucasian (Other)</xsl:when>
      <xsl:when test="@code='krc'">Karachay-Balkar</xsl:when>
      <xsl:when test="@code='suk'">Sukuma</xsl:when>
      <xsl:when test="@code='dar'">Dargwa</xsl:when>
      <xsl:when test="@code='nds'">Low German; Low Saxon; German, Low; Saxon, Low</xsl:when>
      <xsl:when test="@code='rar'">Rarotongan; Cook Islands Maori</xsl:when>
      <xsl:when test="@code='bur'">Burmese</xsl:when>
      <xsl:when test="@code='nso'">Pedi; Sepedi; Northern Sotho</xsl:when>
      <xsl:when test="@code='lui'">Luiseno</xsl:when>
      <xsl:when test="@code='lim'">Limburgan; Limburger; Limburgish</xsl:when>
      <xsl:when test="@code='hsb'">Upper Sorbian</xsl:when>
      <xsl:when test="@code='kua'">Kuanyama; Kwanyama</xsl:when>
      <xsl:when test="@code='kpe'">Kpelle</xsl:when>
      <xsl:when test="@code='kar'">Karen languages</xsl:when>
      <xsl:when test="@code='baq'">Basque</xsl:when>
      <xsl:when test="@code='arp'">Arapaho</xsl:when>
      <xsl:when test="@code='mni'">Manipuri</xsl:when>
      <xsl:when test="@code='iro'">Iroquoian languages</xsl:when>
      <xsl:when test="@code='mol'">Moldavian; Moldovan</xsl:when>
      <xsl:when test="@code='new'">Nepal Bhasa; Newari</xsl:when>
      <xsl:when test="@code='kik'">Kikuyu; Gikuyu</xsl:when>
      <xsl:when test="@code='dum'">Dutch, Middle (ca.1050-1350)</xsl:when>
      <xsl:when test="@code='nav'">Navajo; Navaho</xsl:when>
      <xsl:when test="@code='roh'">Romansh</xsl:when>
      <xsl:when test="@code='ewo'">Ewondo</xsl:when>
      <xsl:when test="@code='ton'">Tonga (Tonga Islands)</xsl:when>
      <xsl:when test="@code='kab'">Kabyle</xsl:when>
      <xsl:when test="@code='ceb'">Cebuano</xsl:when>
      <xsl:when test="@code='ilo'">Iloko</xsl:when>
      <xsl:when test="@code='bas'">Basa</xsl:when>
      <xsl:when test="@code='crh'">Crimean Tatar; Crimean Turkish</xsl:when>
      <xsl:when test="@code='bra'">Braj</xsl:when>
      <xsl:when test="@code='ter'">Tereno</xsl:when>
      <xsl:when test="@code='sas'">Sasak</xsl:when>
      <xsl:when test="@code='frs'">Eastern Frisian</xsl:when>
      <xsl:when test="@code='sad'">Sandawe</xsl:when>
      <xsl:when test="@code='zbl'">Blissymbols; Blissymbolics; Bliss</xsl:when>
      <xsl:when test="@code='ron'">Romanian; Moldavian; Moldovan</xsl:when>
      <xsl:when test="@code='nno'">Norwegian Nynorsk; Nynorsk, Norwegian</xsl:when>
      <xsl:when test="@code='hat'">Haitian; Haitian Creole</xsl:when>
      <xsl:when test="@code='tem'">Timne</xsl:when>
      <xsl:when test="@code='gre'">Greek, Modern (1453-)</xsl:when>
      <xsl:when test="@code='geo'">Georgian</xsl:when>
      <xsl:when test="@code='iba'">Iban</xsl:when>
      <xsl:when test="@code='smn'">Inari Sami</xsl:when>
      <xsl:when test="@code='vot'">Votic</xsl:when>
      <xsl:when test="@code='ine'">Indo-European (Other)</xsl:when>
      <xsl:when test="@code='sot'">Sotho, Southern</xsl:when>
      <xsl:when test="@code='mul'">Multiple languages</xsl:when>
      <xsl:when test="@code='tet'">Tetum</xsl:when>
      <xsl:when test="@code='nya'">Chichewa; Chewa; Nyanja</xsl:when>
      <xsl:when test="@code='frm'">French, Middle (ca.1400-1600)</xsl:when>
      <xsl:when test="@code='kam'">Kamba</xsl:when>
      <xsl:when test="@code='fat'">Fanti</xsl:when>
      <xsl:when test="@code='spa'">Spanish; Castilian</xsl:when>
      <xsl:when test="@code='sah'">Yakut</xsl:when>
      <xsl:when test="@code='mak'">Makasar</xsl:when>
      <xsl:when test="@code='smj'">Lule Sami</xsl:when>
      <xsl:when test="@code='mad'">Madurese</xsl:when>
      <xsl:when test="@code='akk'">Akkadian</xsl:when>
      <xsl:when test="@code='slo'">Slovak</xsl:when>
      <xsl:when test="@code='ijo'">Ijo languages</xsl:when>
      <xsl:when test="@code='cad'">Caddo</xsl:when>
      <xsl:when test="@code='nah'">Nahuatl languages</xsl:when>
      <xsl:when test="@code='syc'">Classical Syriac</xsl:when>
      <xsl:when test="@code='bin'">Bini; Edo</xsl:when>
      <xsl:when test="@code='ady'">Adyghe; Adygei</xsl:when>
      <xsl:when test="@code='sco'">Scots</xsl:when>
      <xsl:when test="@code='rom'">Romany</xsl:when>
      <xsl:when test="@code='syr'">Syriac</xsl:when>
      <xsl:when test="@code='mic'">Mi'kmaq; Micmac</xsl:when>
      <xsl:when test="@code='znd'">Zande languages</xsl:when>
      <xsl:when test="@code='sga'">Irish, Old (to 900)</xsl:when>
      <xsl:when test="@code='rum'">Romanian</xsl:when>
      <xsl:when test="@code='mis'">Uncoded languages</xsl:when>
      <xsl:when test="@code='gay'">Gayo</xsl:when>
      <xsl:when test="@code='afa'">Afro-Asiatic (Other)</xsl:when>
      <xsl:when test="@code='oss'">Ossetian; Ossetic</xsl:when>
      <xsl:when test="@code='nwc'">Classical Newari; Old Newari; Classical Nepal Bhasa</xsl:when>
      <xsl:when test="@code='byn'">Blin; Bilin</xsl:when>
      <xsl:when test="@code='lez'">Lezghian</xsl:when>
      <xsl:when test="@code='jbo'">Lojban</xsl:when>
      <xsl:when test="@code='rap'">Rapanui</xsl:when>
      <xsl:when test="@code='nia'">Nias</xsl:when>
      <xsl:when test="@code='cai'">Central American Indian (Other)</xsl:when>
      <xsl:when test="@code='tup'">Tupi languages</xsl:when>
      <xsl:when test="@code='kho'">Khotanese</xsl:when>
      <xsl:when test="@code='nbl'">Ndebele, South; South Ndebele</xsl:when>
      <xsl:when test="@code='lua'">Luba-Lulua</xsl:when>
      <xsl:when test="@code='tiv'">Tiv</xsl:when>
      <xsl:when test="@code='mwr'">Marwari</xsl:when>
      <xsl:when test="@code='kha'">Khasi</xsl:when>
      <xsl:when test="@code='kok'">Konkani</xsl:when>
      <xsl:when test="@code='loz'">Lozi</xsl:when>
      <xsl:when test="@code='arw'">Arawak</xsl:when>
      <xsl:when test="@code='krl'">Karelian</xsl:when>
      <xsl:when test="@code='nzi'">Nzima</xsl:when>
      <xsl:when test="@code='tut'">Altaic (Other)</xsl:when>
      <xsl:when test="@code='ale'">Aleut</xsl:when>
      <xsl:when test="@code='dgr'">Dogrib</xsl:when>
      <xsl:when test="@code='mno'">Manobo languages</xsl:when>
      <xsl:when test="@code='afh'">Afrihili</xsl:when>
      <xsl:when test="@code='lol'">Mongo</xsl:when>
      <xsl:when test="@code='run'">Rundi</xsl:when>
      <xsl:when test="@code='tvl'">Tuvalu</xsl:when>
      <xsl:when test="@code='hit'">Hittite</xsl:when>
      <xsl:when test="@code='umb'">Umbundu</xsl:when>
      <xsl:when test="@code='pau'">Palauan</xsl:when>
      <xsl:when test="@code='mos'">Mossi</xsl:when>
      <xsl:when test="@code='iii'">Sichuan Yi; Nuosu</xsl:when>
      <xsl:when test="@code='tmh'">Tamashek</xsl:when>
      <xsl:when test="@code='fiu'">Finno-Ugrian (Other)</xsl:when>
      <xsl:when test="@code='ile'">Interlingue; Occidental</xsl:when>
      <xsl:when test="@code='pap'">Papiamento</xsl:when>
      <xsl:when test="@code='nyn'">Nyankole</xsl:when>
      <xsl:when test="@code='doi'">Dogri</xsl:when>
      <xsl:when test="@code='din'">Dinka</xsl:when>
      <xsl:when test="@code='cat'">Catalan; Valencian</xsl:when>
      <xsl:when test="@code='udm'">Udmurt</xsl:when>
      <xsl:when test="@code='yap'">Yapese</xsl:when>
      <xsl:when test="@code='ssa'">Nilo-Saharan (Other)</xsl:when>
      <xsl:when test="@code='per'">Persian</xsl:when>
      <xsl:when test="@code='may'">Malay</xsl:when>
      <xsl:when test="@code='arm'">Armenian</xsl:when>
      <xsl:when test="@code='grc'">Greek, Ancient (to 1453)</xsl:when>
      <xsl:when test="@code='khi'">Khoisan (Other)</xsl:when>
      <xsl:when test="@code='csb'">Kashubian</xsl:when>
      <xsl:when test="@code='niu'">Niuean</xsl:when>
      <xsl:when test="@code='dut'">Dutch; Flemish</xsl:when>
      <xsl:when test="@code='myv'">Erzya</xsl:when>
      <xsl:when test="@code='sio'">Siouan languages</xsl:when>
      <xsl:when test="@code='sal'">Salishan languages</xsl:when>
      <xsl:when test="@code='uig'">Uighur; Uyghur</xsl:when>
      <xsl:when test="@code='gon'">Gondi</xsl:when>
      <xsl:when test="@code='pon'">Pohnpeian</xsl:when>
      <xsl:when test="@code='sin'">Sinhala; Sinhalese</xsl:when>
      <xsl:when test="@code='tlh'">Klingon; tlhIngan-Hol</xsl:when>
      <xsl:when test="@code='nqo'">N'Ko</xsl:when>
      <xsl:when test="@code='sgn'">Sign Languages</xsl:when>
      <xsl:when test="@code='wel'">Welsh</xsl:when>
      <xsl:when test="@code='qaa-qtz'">Reserved for local use</xsl:when>
      <xsl:when test="@code='pro'">Provençal, Old (to 1500)</xsl:when>
      <xsl:when test="@code='mun'">Munda languages</xsl:when>
      <xsl:when test="@code='shn'">Shan</xsl:when>
      <xsl:when test="@code='uga'">Ugaritic</xsl:when>
      <xsl:when test="@code='mao'">Maori</xsl:when>
      <xsl:when test="@code='hai'">Haida</xsl:when>
      <xsl:when test="@code='non'">Norse, Old</xsl:when>
      <xsl:when test="@code='bai'">Bamileke languages</xsl:when>
      <xsl:when test="@code='dyu'">Dyula</xsl:when>
      <xsl:when test="@code='yao'">Yao</xsl:when>
      <xsl:when test="@code='zza'">Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki</xsl:when>
      <xsl:when test="@code='tpi'">Tok Pisin</xsl:when>
      <xsl:when test="@code='ltz'">Luxembourgish; Letzeburgesch</xsl:when>
      <xsl:when test="@code='tum'">Tumbuka</xsl:when>
      <xsl:when test="@code='urd'">Urdu</xsl:when>
      <xsl:when test="@code='glv'">Manx</xsl:when>
      <xsl:when test="@code='dzo'">Dzongkha</xsl:when>
      <xsl:when test="@code='snd'">Sindhi</xsl:when>
      <xsl:when test="@code='kj'">Kwanyama</xsl:when>
      <xsl:when test="@code='lat'">Latin</xsl:when>
      <xsl:when test="@code='chv'">Chuvash</xsl:when>
      <xsl:when test="@code='mlt'">Maltese</xsl:when>
      <xsl:when test="@code='bak'">Bashkir</xsl:when>
      <xsl:when test="@code='aze'">Azerbaijani</xsl:when>
      <xsl:when test="@code='aym'">Aymara</xsl:when>
      <xsl:when test="@code='oc'">Occitan</xsl:when>
      <xsl:when test="@code='ny'">Chichewa</xsl:when>
      <xsl:when test="@code='mo'">Moldavian</xsl:when>
      <xsl:when test="@code='oji'">Ojibwa</xsl:when>
      <xsl:when test="@code='km'">Khmer</xsl:when>
      <xsl:when test="@code='pol'">Polish</xsl:when>
      <xsl:when test="@code='ara'">Arabic</xsl:when>
      <xsl:when test="@code='slk'">Slovak</xsl:when>
      <xsl:when test="@code='ukr'">Ukrainian</xsl:when>
      <xsl:when test="@code='nl'">Dutch</xsl:when>
      <xsl:when test="@code='ful'">Fulah</xsl:when>
      <xsl:when test="@code='vie'">Vietnamese</xsl:when>
      <xsl:when test="@code='ps'">Pashto</xsl:when>
      <xsl:when test="@code='mah'">Marshallese</xsl:when>
      <xsl:when test="@code='lin'">Lingala</xsl:when>
      <xsl:when test="@code='ita'">Italian</xsl:when>
      <xsl:when test="@code='ipk'">Inupiaq</xsl:when>
      <xsl:when test="@code='sh'">Serbo-Croatian</xsl:when>
      <xsl:when test="@code='pli'">Pali</xsl:when>
      <xsl:when test="@code='wln'">Walloon</xsl:when>
      <xsl:when test="@code='sun'">Sundanese</xsl:when>
      <xsl:when test="@code='amh'">Amharic</xsl:when>
      <xsl:when test="@code='hau'">Hausa</xsl:when>
      <xsl:when test="@code='ht'">Haitian</xsl:when>
      <xsl:when test="@code='ndo'">Ndonga</xsl:when>
      <xsl:when test="@code='os'">Ossetian</xsl:when>
      <xsl:when test="@code='nau'">Nauru</xsl:when>
      <xsl:when test="@code='wol'">Wolof</xsl:when>
      <xsl:when test="@code='xho'">Xhosa</xsl:when>
      <xsl:when test="@code='sqi'">Albanian</xsl:when>
      <xsl:when test="@code='ava'">Avaric</xsl:when>
      <xsl:when test="@code='bh'">Bihari</xsl:when>
      <xsl:when test="@code='abk'">Abkhazian</xsl:when>
      <xsl:when test="@code='ia'">Interlingua</xsl:when>
      <xsl:when test="@code='ie'">Interlingue</xsl:when>
      <xsl:when test="@code='ind'">Indonesian</xsl:when>
      <xsl:when test="@code='grn'">Guarani</xsl:when>
      <xsl:when test="@code='tso'">Tsonga</xsl:when>
      <xsl:when test="@code='nr'">South Ndebele</xsl:when>
      <xsl:when test="@code='mlg'">Malagasy</xsl:when>
      <xsl:when test="@code='isl'">Icelandic</xsl:when>
      <xsl:when test="@code='hrv'">Croatian</xsl:when>
      <xsl:when test="@code='cos'">Corsican</xsl:when>
      <xsl:when test="@code='som'">Somali</xsl:when>
      <xsl:when test="@code='bis'">Bislama</xsl:when>
      <xsl:when test="@code='epo'">Esperanto</xsl:when>
      <xsl:when test="@code='tur'">Turkish</xsl:when>
      <xsl:when test="@code='dan'">Danish</xsl:when>
      <xsl:when test="@code='hye'">Armenian</xsl:when>
      <xsl:when test="@code='uzb'">Uzbek</xsl:when>
      <xsl:when test="@code='tuk'">Turkmen</xsl:when>
      <xsl:when test="@code='zul'">Zulu</xsl:when>
      <xsl:when test="@code='ug'">Uighur</xsl:when>
      <xsl:when test="@code='st'">Sotho</xsl:when>
      <xsl:when test="@code='kl'">Kalaallisut</xsl:when>
      <xsl:when test="@code='tgk'">Tajik</xsl:when>
      <xsl:when test="@code='sme'">Northern Sami</xsl:when>
      <xsl:when test="@code='za'">Zhuang</xsl:when>
      <xsl:when test="@code='tgl'">Tagalog</xsl:when>
      <xsl:when test="@code='orm'">Oromo</xsl:when>
      <xsl:when test="@code='nv'">Navajo</xsl:when>
      <xsl:when test="@code='fry'">Western Frisian</xsl:when>
      <xsl:when test="@code='tam'">Tamil</xsl:when>
      <xsl:when test="@code='hun'">Hungarian</xsl:when>
      <xsl:when test="@code='nd'">North Ndebele</xsl:when>
      <xsl:when test="@code='tsn'">Tswana</xsl:when>
      <xsl:when test="@code='kat'">Georgian</xsl:when>
      <xsl:when test="@code='hin'">Hindi</xsl:when>
      <xsl:when test="@code='san'">Sanskrit</xsl:when>
      <xsl:when test="@code='ori'">Oriya</xsl:when>
      <xsl:when test="@code='mya'">Burmese</xsl:when>
      <xsl:when test="@code='iku'">Inuktitut</xsl:when>
      <xsl:when test="@code='sna'">Shona</xsl:when>
      <xsl:when test="@code='cu'">Church Slavic</xsl:when>
      <xsl:when test="@code='tat'">Tatar</xsl:when>
      <xsl:when test="@code='mal'">Malayalam</xsl:when>
      <xsl:when test="@code='kor'">Korean</xsl:when>
      <xsl:when test="@code='afr'">Afrikaans</xsl:when>
      <xsl:when test="@code='kaz'">Kazakh</xsl:when>
      <xsl:when test="@code='cor'">Cornish</xsl:when>
      <xsl:when test="@code='ces'">Czech</xsl:when>
      <xsl:when test="@code='li'">Limburgish</xsl:when>
      <xsl:when test="@code='rus'">Russian</xsl:when>
      <xsl:when test="@code='ii'">Sichuan Yi</xsl:when>
      <xsl:when test="@code='ven'">Venda</xsl:when>
      <xsl:when test="@code='zho'">Chinese</xsl:when>
      <xsl:when test="@code='yor'">Yoruba</xsl:when>
      <xsl:when test="@code='lit'">Lithuanian</xsl:when>
      <xsl:when test="@code='mkd'">Macedonian</xsl:when>
      <xsl:when test="@code='kas'">Kashmiri</xsl:when>
      <xsl:when test="@code='swa'">Swahili</xsl:when>
      <xsl:when test="@code='sag'">Sango</xsl:when>
      <xsl:when test="@code='nep'">Nepali</xsl:when>
      <xsl:when test="@code='fao'">Faroese</xsl:when>
      <xsl:when test="@code='deu'">German</xsl:when>
      <xsl:when test="@code='fra'">French</xsl:when>
      <xsl:when test="@code='bel'">Belarusian</xsl:when>
      <xsl:when test="@code='lub'">Luba-Katanga</xsl:when>
      <xsl:when test="@code='cym'">Welsh</xsl:when>
      <xsl:when test="@code='eus'">Basque</xsl:when>
      <xsl:when test="@code='hmo'">Hiri Motu</xsl:when>
      <xsl:when test="@code='que'">Quechua</xsl:when>
      <xsl:when test="@code='kom'">Komi</xsl:when>
      <xsl:when test="@code='ro'">Romanian</xsl:when>
      <xsl:when test="@code='rn'">Kirundi</xsl:when>
      <xsl:when test="@code='ssw'">Swati</xsl:when>
      <xsl:when test="@code='kin'">Kinyarwanda</xsl:when>
      <xsl:when test="@code='es'">Spanish</xsl:when>
      <xsl:when test="@code='ca'">Catalan</xsl:when>
      <xsl:when test="@code='rm'">Raeto-Romance</xsl:when>
      <xsl:when test="@code='fij'">Fijian</xsl:when>
      <xsl:when test="@code='dv'">Divehi</xsl:when>
      <xsl:when test="@code='heb'">Hebrew</xsl:when>
      <xsl:when test="@code='aka'">Akan</xsl:when>
      <xsl:when test="@code='to'">Tonga</xsl:when>
      <xsl:when test="@code='vo'">Volapuk</xsl:when>
      <xsl:when test="@code='bre'">Breton</xsl:when>
      <xsl:when test="@code='guj'">Gujarati</xsl:when>
      <xsl:when test="@code='glg'">Galician</xsl:when>
      <xsl:when test="@code='lug'">Ganda</xsl:when>
      <xsl:when test="@code='arg'">Aragonese</xsl:when>
      <xsl:when test="@code='ben'">Bengali</xsl:when>
      <xsl:when test="@code='aar'">Afar</xsl:when>
      <xsl:when test="@code='mar'">Marathi</xsl:when>
      <xsl:when test="@code='swe'">Swedish</xsl:when>
      <xsl:when test="@code='si'">Sinhalese</xsl:when>
      <xsl:when test="@code='bos'">Bosnian</xsl:when>
      <xsl:when test="@code='che'">Chechen</xsl:when>
      <xsl:when test="@code='bam'">Bambara</xsl:when>
      <xsl:when test="@code='lav'">Latvian</xsl:when>
      <xsl:when test="@code='mon'">Mongolian</xsl:when>
      <xsl:when test="@code='eng'">English</xsl:when>
      <xsl:when test="@code='en-US'">English</xsl:when>
      <xsl:when test="@code='asm'">Assamese</xsl:when>
      <xsl:when test="@code='jav'">Javanese</xsl:when>
      <xsl:when test="@code='tel'">Telugu</xsl:when>
      <xsl:when test="@code='slv'">Slovenian</xsl:when>
      <xsl:when test="@code='por'">Portuguese</xsl:when>
      <xsl:when test="@code='nor'">Norwegian</xsl:when>
      <xsl:when test="@code='jpn'">Japanese</xsl:when>
      <xsl:when test="@code='srd'">Sardinian</xsl:when>
      <xsl:when test="@code='lb'">Luxembourgish</xsl:when>
      <xsl:when test="@code='fin'">Finnish</xsl:when>
      <xsl:when test="@code='ave'">Avestan</xsl:when>
      <xsl:when test="@code='tah'">Tahitian</xsl:when>
      <xsl:when test="@code='gd'">Scottish Gaelic</xsl:when>
      <xsl:when test="@code='ewe'">Ewe</xsl:when>
      <xsl:when test="@code='tir'">Tigrinya</xsl:when>
      <xsl:when test="@code='ki'">Kikuyu</xsl:when>
      <xsl:when test="@code='tha'">Thai</xsl:when>
      <xsl:when test="@code='kan'">Kannada</xsl:when>
      <xsl:when test="@code='ibo'">Igbo</xsl:when>
      <xsl:when test="@code='fas'">Persian</xsl:when>
      <xsl:when test="@code='ido'">Ido</xsl:when>
      <xsl:when test="@code='her'">Herero</xsl:when>
      <xsl:when test="@code='bod'">Tibetan</xsl:when>
      <xsl:when test="@code='smo'">Samoan</xsl:when>
      <xsl:when test="@code='lao'">Lao</xsl:when>
      <xsl:when test="@code='kur'">Kurdish</xsl:when>
      <xsl:when test="@code='bul'">Bulgarian</xsl:when>
      <xsl:when test="@code='cre'">Cree</xsl:when>
      <xsl:when test="@code='cha'">Chamorro</xsl:when>
      <xsl:when test="@code='nb'">Norwegian Bokmal</xsl:when>
      <xsl:when test="@code='srp'">Serbian</xsl:when>
      <xsl:when test="@code='gle'">Irish</xsl:when>
      <xsl:when test="@code='ky'">Kirghiz</xsl:when>
      <xsl:when test="@code='kau'">Kanuri</xsl:when>
      <xsl:when test="@code='kon'">Kongo</xsl:when>
      <xsl:when test="@code='pa'">Panjabi</xsl:when>
      <xsl:when test="@code='twi'">Twi</xsl:when>
      <xsl:when test="@code='yid'">Yiddish</xsl:when>
      <xsl:when test="@code='mri'">Maori</xsl:when>
      <xsl:when test="@code='est'">Estonian</xsl:when>
      <xsl:when test="@code='msa'">Malay</xsl:when>
      <xsl:when test="@code='el'">Greek</xsl:when>
      <xsl:when test="@code='apa'">Apache languages</xsl:when>
      <xsl:when test="@code='elx'">Elamite</xsl:when>
      <xsl:when test="@code='eka'">Ekajuk</xsl:when>
      <xsl:when test="@code='zen'">Zenaga</xsl:when>
      <xsl:when test="@code='ast'">Asturian; Bable; Leonese; asturleonese</xsl:when>
      <xsl:when test="@code='tli'">Tlingit</xsl:when>
      <xsl:when test="@code='lad'">Ladino</xsl:when>
      <xsl:when test="@code='pra'">Prakrit languages</xsl:when>
      <xsl:when test="@code='gez'">Geez</xsl:when>
      <xsl:when test="@code='fil'">Filipino; Pilipino</xsl:when>
      <xsl:when test="@code='nic'">Niger-Kordofanian (Other)</xsl:when>
      <xsl:when test="@code='arn'">Mapudungun; Mapuche</xsl:when>
      <xsl:when test="@code='grb'">Grebo</xsl:when>
      <xsl:when test="@code='lam'">Lamba</xsl:when>
      <xsl:when test="@code='min'">Minangkabau</xsl:when>
      <xsl:when test="@code='efi'">Efik</xsl:when>
      <xsl:when test="@code='mwl'">Mirandese</xsl:when>
      <xsl:when test="@code='got'">Gothic</xsl:when>
      <xsl:when test="@code='cze'">Czech</xsl:when>
      <xsl:when test="@code='oto'">Otomian languages</xsl:when>
      <xsl:when test="@code='sel'">Selkup</xsl:when>
      <xsl:when test="@code='aa'">Afar</xsl:when>
      <xsl:when test="@code='ab'">Abkhazian</xsl:when>
      <xsl:when test="@code='af'">Afrikaans</xsl:when>
      <xsl:when test="@code='ak'">Akan</xsl:when>
      <xsl:when test="@code='sq'">Albanian</xsl:when>
      <xsl:when test="@code='am'">Amharic</xsl:when>
      <xsl:when test="@code='ar'">Arabic</xsl:when>
      <xsl:when test="@code='an'">Aragonese</xsl:when>
      <xsl:when test="@code='hy'">Armenian</xsl:when>
      <xsl:when test="@code='as'">Assamese</xsl:when>
      <xsl:when test="@code='av'">Avaric</xsl:when>
      <xsl:when test="@code='ae'">Avestan</xsl:when>
      <xsl:when test="@code='ay'">Aymara</xsl:when>
      <xsl:when test="@code='az'">Azerbaijani</xsl:when>
      <xsl:when test="@code='ba'">Bashkir</xsl:when>
      <xsl:when test="@code='bm'">Bambara</xsl:when>
      <xsl:when test="@code='eu'">Basque</xsl:when>
      <xsl:when test="@code='be'">Belarusian</xsl:when>
      <xsl:when test="@code='bn'">Bengali</xsl:when>
      <xsl:when test="@code='bh'">Bihari languages</xsl:when>
      <xsl:when test="@code='bi'">Bislama</xsl:when>
      <xsl:when test="@code='bs'">Bosnian</xsl:when>
      <xsl:when test="@code='br'">Breton</xsl:when>
      <xsl:when test="@code='bg'">Bulgarian</xsl:when>
      <xsl:when test="@code='my'">Burmese</xsl:when>
      <xsl:when test="@code='ca'">Catalan; Valencian</xsl:when>
      <xsl:when test="@code='cs'">Czech</xsl:when>
      <xsl:when test="@code='ch'">Chamorro</xsl:when>
      <xsl:when test="@code='ce'">Chechen</xsl:when>
      <xsl:when test="@code='zh'">Chinese</xsl:when>
      <xsl:when test="@code='cu'">Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic</xsl:when>
      <xsl:when test="@code='cv'">Chuvash</xsl:when>
      <xsl:when test="@code='kw'">Cornish</xsl:when>
      <xsl:when test="@code='co'">Corsican</xsl:when>
      <xsl:when test="@code='cr'">Cree</xsl:when>
      <xsl:when test="@code='da'">Danish</xsl:when>
      <xsl:when test="@code='dv'">Divehi; Dhivehi; Maldivian</xsl:when>
      <xsl:when test="@code='nl'">Dutch; Flemish</xsl:when>
      <xsl:when test="@code='dz'">Dzongkha</xsl:when>
      <xsl:when test="@code='en'">English</xsl:when>
      <xsl:when test="@code='eo'">Esperanto</xsl:when>
      <xsl:when test="@code='et'">Estonian</xsl:when>
      <xsl:when test="@code='ee'">Ewe</xsl:when>
      <xsl:when test="@code='fo'">Faroese</xsl:when>
      <xsl:when test="@code='fj'">Fijian</xsl:when>
      <xsl:when test="@code='fi'">Finnish</xsl:when>
      <xsl:when test="@code='fr'">French</xsl:when>
      <xsl:when test="@code='fy'">Western Frisian</xsl:when>
      <xsl:when test="@code='ff'">Fulah</xsl:when>
      <xsl:when test="@code='ka'">Georgian</xsl:when>
      <xsl:when test="@code='de'">German</xsl:when>
      <xsl:when test="@code='gd'">Gaelic; Scottish Gaelic</xsl:when>
      <xsl:when test="@code='ga'">Irish</xsl:when>
      <xsl:when test="@code='gl'">Galician</xsl:when>
      <xsl:when test="@code='gv'">Manx</xsl:when>
      <xsl:when test="@code='el'">Greek, Modern (1453-)</xsl:when>
      <xsl:when test="@code='gn'">Guarani</xsl:when>
      <xsl:when test="@code='gu'">Gujarati</xsl:when>
      <xsl:when test="@code='ht'">Haitian; Haitian Creole</xsl:when>
      <xsl:when test="@code='ha'">Hausa</xsl:when>
      <xsl:when test="@code='he'">Hebrew</xsl:when>
      <xsl:when test="@code='hz'">Herero</xsl:when>
      <xsl:when test="@code='hi'">Hindi</xsl:when>
      <xsl:when test="@code='ho'">Hiri Motu</xsl:when>
      <xsl:when test="@code='hr'">Croatian</xsl:when>
      <xsl:when test="@code='hu'">Hungarian</xsl:when>
      <xsl:when test="@code='ig'">Igbo</xsl:when>
      <xsl:when test="@code='is'">Icelandic</xsl:when>
      <xsl:when test="@code='io'">Ido</xsl:when>
      <xsl:when test="@code='ii'">Sichuan Yi; Nuosu</xsl:when>
      <xsl:when test="@code='iu'">Inuktitut</xsl:when>
      <xsl:when test="@code='ie'">Interlingue; Occidental</xsl:when>
      <xsl:when test="@code='ia'">Interlingua (International Auxiliary Language Association)</xsl:when>
      <xsl:when test="@code='id'">Indonesian</xsl:when>
      <xsl:when test="@code='ik'">Inupiaq</xsl:when>
      <xsl:when test="@code='it'">Italian</xsl:when>
      <xsl:when test="@code='jv'">Javanese</xsl:when>
      <xsl:when test="@code='ja'">Japanese</xsl:when>
      <xsl:when test="@code='kl'">Kalaallisut; Greenlandic</xsl:when>
      <xsl:when test="@code='kn'">Kannada</xsl:when>
      <xsl:when test="@code='ks'">Kashmiri</xsl:when>
      <xsl:when test="@code='kr'">Kanuri</xsl:when>
      <xsl:when test="@code='kk'">Kazakh</xsl:when>
      <xsl:when test="@code='km'">Central Khmer</xsl:when>
      <xsl:when test="@code='ki'">Kikuyu; Gikuyu</xsl:when>
      <xsl:when test="@code='rw'">Kinyarwanda</xsl:when>
      <xsl:when test="@code='ky'">Kirghiz; Kyrgyz</xsl:when>
      <xsl:when test="@code='kv'">Komi</xsl:when>
      <xsl:when test="@code='kg'">Kongo</xsl:when>
      <xsl:when test="@code='ko'">Korean</xsl:when>
      <xsl:when test="@code='kj'">Kuanyama; Kwanyama</xsl:when>
      <xsl:when test="@code='ku'">Kurdish</xsl:when>
      <xsl:when test="@code='lo'">Lao</xsl:when>
      <xsl:when test="@code='la'">Latin</xsl:when>
      <xsl:when test="@code='lv'">Latvian</xsl:when>
      <xsl:when test="@code='li'">Limburgan; Limburger; Limburgish</xsl:when>
      <xsl:when test="@code='ln'">Lingala</xsl:when>
      <xsl:when test="@code='lt'">Lithuanian</xsl:when>
      <xsl:when test="@code='lb'">Luxembourgish; Letzeburgesch</xsl:when>
      <xsl:when test="@code='lu'">Luba-Katanga</xsl:when>
      <xsl:when test="@code='lg'">Ganda</xsl:when>
      <xsl:when test="@code='mk'">Macedonian</xsl:when>
      <xsl:when test="@code='mh'">Marshallese</xsl:when>
      <xsl:when test="@code='ml'">Malayalam</xsl:when>
      <xsl:when test="@code='mi'">Maori</xsl:when>
      <xsl:when test="@code='mr'">Marathi</xsl:when>
      <xsl:when test="@code='ms'">Malay</xsl:when>
      <xsl:when test="@code='mg'">Malagasy</xsl:when>
      <xsl:when test="@code='mt'">Maltese</xsl:when>
      <xsl:when test="@code='mn'">Mongolian</xsl:when>
      <xsl:when test="@code='na'">Nauru</xsl:when>
      <xsl:when test="@code='nv'">Navajo; Navaho</xsl:when>
      <xsl:when test="@code='nr'">Ndebele, South; South Ndebele</xsl:when>
      <xsl:when test="@code='nd'">Ndebele, North; North Ndebele</xsl:when>
      <xsl:when test="@code='ng'">Ndonga</xsl:when>
      <xsl:when test="@code='ne'">Nepali</xsl:when>
      <xsl:when test="@code='nn'">Norwegian Nynorsk; Nynorsk, Norwegian</xsl:when>
      <xsl:when test="@code='nb'">Bokmål, Norwegian; Norwegian Bokmål</xsl:when>
      <xsl:when test="@code='no'">Norwegian</xsl:when>
      <xsl:when test="@code='ny'">Chichewa; Chewa; Nyanja</xsl:when>
      <xsl:when test="@code='oc'">Occitan (post 1500)</xsl:when>
      <xsl:when test="@code='oj'">Ojibwa</xsl:when>
      <xsl:when test="@code='or'">Oriya</xsl:when>
      <xsl:when test="@code='om'">Oromo</xsl:when>
      <xsl:when test="@code='os'">Ossetian; Ossetic</xsl:when>
      <xsl:when test="@code='pa'">Panjabi; Punjabi</xsl:when>
      <xsl:when test="@code='fa'">Persian</xsl:when>
      <xsl:when test="@code='pi'">Pali</xsl:when>
      <xsl:when test="@code='pl'">Polish</xsl:when>
      <xsl:when test="@code='pt'">Portuguese</xsl:when>
      <xsl:when test="@code='ps'">Pushto; Pashto</xsl:when>
      <xsl:when test="@code='qu'">Quechua</xsl:when>
      <xsl:when test="@code='rm'">Romansh</xsl:when>
      <xsl:when test="@code='ro'">Romanian; Moldavian; Moldovan</xsl:when>
      <xsl:when test="@code='rn'">Rundi</xsl:when>
      <xsl:when test="@code='ru'">Russian</xsl:when>
      <xsl:when test="@code='sg'">Sango</xsl:when>
      <xsl:when test="@code='sa'">Sanskrit</xsl:when>
      <xsl:when test="@code='si'">Sinhala; Sinhalese</xsl:when>
      <xsl:when test="@code='sk'">Slovak</xsl:when>
      <xsl:when test="@code='sl'">Slovenian</xsl:when>
      <xsl:when test="@code='se'">Northern Sami</xsl:when>
      <xsl:when test="@code='sm'">Samoan</xsl:when>
      <xsl:when test="@code='sn'">Shona</xsl:when>
      <xsl:when test="@code='sd'">Sindhi</xsl:when>
      <xsl:when test="@code='so'">Somali</xsl:when>
      <xsl:when test="@code='st'">Sotho, Southern</xsl:when>
      <xsl:when test="@code='es'">Spanish; Castilian</xsl:when>
      <xsl:when test="@code='sc'">Sardinian</xsl:when>
      <xsl:when test="@code='sr'">Serbian</xsl:when>
      <xsl:when test="@code='ss'">Swati</xsl:when>
      <xsl:when test="@code='su'">Sundanese</xsl:when>
      <xsl:when test="@code='sw'">Swahili</xsl:when>
      <xsl:when test="@code='sv'">Swedish</xsl:when>
      <xsl:when test="@code='ty'">Tahitian</xsl:when>
      <xsl:when test="@code='ta'">Tamil</xsl:when>
      <xsl:when test="@code='tt'">Tatar</xsl:when>
      <xsl:when test="@code='te'">Telugu</xsl:when>
      <xsl:when test="@code='tg'">Tajik</xsl:when>
      <xsl:when test="@code='tl'">Tagalog</xsl:when>
      <xsl:when test="@code='th'">Thai</xsl:when>
      <xsl:when test="@code='bo'">Tibetan</xsl:when>
      <xsl:when test="@code='ti'">Tigrinya</xsl:when>
      <xsl:when test="@code='to'">Tonga (Tonga Islands)</xsl:when>
      <xsl:when test="@code='tn'">Tswana</xsl:when>
      <xsl:when test="@code='ts'">Tsonga</xsl:when>
      <xsl:when test="@code='tk'">Turkmen</xsl:when>
      <xsl:when test="@code='tr'">Turkish</xsl:when>
      <xsl:when test="@code='tw'">Twi</xsl:when>
      <xsl:when test="@code='ug'">Uighur; Uyghur</xsl:when>
      <xsl:when test="@code='uk'">Ukrainian</xsl:when>
      <xsl:when test="@code='ur'">Urdu</xsl:when>
      <xsl:when test="@code='uz'">Uzbek</xsl:when>
      <xsl:when test="@code='ve'">Venda</xsl:when>
      <xsl:when test="@code='vi'">Vietnamese</xsl:when>
      <xsl:when test="@code='vo'">Volapük</xsl:when>
      <xsl:when test="@code='cy'">Welsh</xsl:when>
      <xsl:when test="@code='wa'">Walloon</xsl:when>
      <xsl:when test="@code='wo'">Wolof</xsl:when>
      <xsl:when test="@code='xh'">Xhosa</xsl:when>
      <xsl:when test="@code='yi'">Yiddish</xsl:when>
      <xsl:when test="@code='yo'">Yoruba</xsl:when>
      <xsl:when test="@code='za'">Zhuang; Chuang</xsl:when>
      <xsl:when test="@code='zu'">Zulu</xsl:when>

      <xsl:otherwise>
        <xsl:value-of select="@code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show-race  -->
  <xsl:template name="show-race">
    <xsl:choose>
      <xsl:when test="@displayName">
        <xsl:value-of select="@displayName"/>
        <xsl:if test="position() &lt; last()">
          <xsl:text>, </xsl:text>
        </xsl:if>


      </xsl:when>
      <!--<xsl:when test="@nullFlavor">
	  <xsl:text>not </xsl:text></xsl:when>-->

      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='ASKU'">
        <xsl:text>Declined to specify</xsl:text>

      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='UNK'">
        <xsl:text>Unknown</xsl:text>
        <xsl:if test="position() &lt; last()">
          <xsl:text>,</xsl:text>
        </xsl:if>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='NA'">
        <xsl:text>Information not available</xsl:text>

      </xsl:when>

      <!--<xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='ASKU'">
        <xsl:text>Declined to specify</xsl:text>

      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='NA'">
        <xsl:text>Information not available</xsl:text>

      </xsl:when>
 <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='UNK'">
        <xsl:text>Unknown</xsl:text>
 <xsl:if test="position() &lt; last()">
          <xsl:text>,</xsl:text>
        </xsl:if>
      </xsl:when>-->
      <xsl:otherwise>
        <xsl:value-of select="@code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- Component of section -->
  <!-- show-code-->
  <!-- show-ethnicity  -->
  <xsl:template name="show-ethnicity">
    <xsl:choose>
      <xsl:when test="@displayName">
        <xsl:value-of select="@displayName"/>
        <xsl:if test="position() &lt; last()">
          <xsl:text>, </xsl:text>
        </xsl:if>


      </xsl:when>
      <!--<xsl:when test="@nullFlavor">
	  <xsl:text>not </xsl:text></xsl:when>-->

      <!--<xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='ASKU'">
        <xsl:text>Declined to specify</xsl:text>

      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='UNK'">
        <xsl:text>Unknown</xsl:text>
        <xsl:if test="position() &lt; last()">
          <xsl:text>,</xsl:text>
        </xsl:if>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@nullFlavor='NA'">
        <xsl:text>Information not available</xsl:text>

      </xsl:when>-->

      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='ASKU'">
        <xsl:text>Declined to specify</xsl:text>

      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='NA'">
        <xsl:text>Information not available</xsl:text>

      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:ethnicGroupCode/@nullFlavor='UNK'">
        <xsl:text>Unknown</xsl:text>
        <xsl:if test="position() &lt; last()">
          <xsl:text>,</xsl:text>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="@code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- Component of section -->
  <!-- show-code-->
  <xsl:template name="show-code">
    <xsl:param name="code"/>
    <xsl:variable name="this-codeSystem">
      <xsl:value-of select="$code/@codeSystem"/>
    </xsl:variable>
    <xsl:variable name="this-code">
      <xsl:value-of select="$code/@code"/>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$code/n1:originalText">
        <xsl:value-of select="$code/n1:originalText"/>
      </xsl:when>
      <xsl:when test="$code/@displayName">
        <xsl:value-of select="$code/@displayName"/>
      </xsl:when>
      <!--
      <xsl:when test="$the-valuesets/*/voc:system[@root=$this-codeSystem]/voc:code[@value=$this-code]/@displayName">
        <xsl:value-of select="$the-valuesets/*/voc:system[@root=$this-codeSystem]/voc:code[@value=$this-code]/@displayName"/>
      </xsl:when>
      -->
      <xsl:otherwise>
        <xsl:value-of select="$this-code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show-time-->
  <xsl:template name="show-time">
    <xsl:param name="datetime"/>
    <xsl:choose>
      <xsl:when test="not($datetime)">
        <xsl:call-template name="formatDate">
          <xsl:with-param name="date" select="@value"/>
        </xsl:call-template>
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="formatDate">
          <xsl:with-param name="date" select="$datetime/@value"/>
        </xsl:call-template>
        <xsl:text> </xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="show-assignedEntity">
    <xsl:param name="asgnEntity"/>
    <xsl:choose>
      <xsl:when test="$asgnEntity/n1:assignedPerson/n1:name">
        <xsl:call-template name="getName">
          <xsl:with-param name="name" select="$asgnEntity/n1:assignedPerson/n1:name"/>
        </xsl:call-template>
        <xsl:if test="$asgnEntity/n1:representedOrganization/n1:name">
          <xsl:text> of </xsl:text>
          <xsl:value-of select="$asgnEntity/n1:representedOrganization/n1:name"/>
        </xsl:if>
      </xsl:when>
      <xsl:when test="$asgnEntity/n1:representedOrganization">
        <xsl:value-of select="$asgnEntity/n1:representedOrganization/n1:name"/>
        <xsl:text> </xsl:text>
      </xsl:when>

    </xsl:choose>
  </xsl:template>

  <!-- show-contactInfo -->
  <xsl:template name="show-contactInfo">
    <xsl:param name="contact"/>
    <xsl:call-template name="getOfficeAddress">
      <xsl:with-param name="addr" select="$contact/n1:addr"/>
    </xsl:call-template>
    <xsl:call-template name="getTelecom">
      <xsl:with-param name="telecom" select="$contact/n1:telecom"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="show-telecom">
    <xsl:param name="telecom"/>
    <xsl:choose>
      <xsl:when test="$telecom">
        <xsl:variable name="type" select="substring-before($telecom/@value, ':')"/>
        <xsl:variable name="value" select="substring-after($telecom/@value, ':')"/>
        <xsl:if test="$type">
          <xsl:call-template name="translateTelecomCode">
            <xsl:with-param name="code" select="$type"/>
          </xsl:call-template>
          <xsl:if test="@use">
            <xsl:text> (</xsl:text>
            <xsl:call-template name="translateTelecomCode">
              <xsl:with-param name="code" select="@use"/>
            </xsl:call-template>
            <xsl:text>)</xsl:text>
          </xsl:if>
          <xsl:text>: </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$value"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>Telecom information not available</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
    <br/>
  </xsl:template>
  <xsl:template name="translateTelecomCode">
    <xsl:param name="code"/>
    <!--xsl:value-of select="document('voc.xml')/systems/system[@root=$code/@codeSystem]/code[@value=$code/@code]/@displayName"/-->
    <!--xsl:value-of select="document('codes.xml')/*/code[@code=$code]/@display"/-->
    <xsl:choose>
      <!-- lookup table Telecom URI -->
      <xsl:when test="$code='tel'">
        <xsl:text>Tel</xsl:text>
      </xsl:when>
      <xsl:when test="$code='fax'">
        <xsl:text>Fax</xsl:text>
      </xsl:when>
      <xsl:when test="$code='http'">
        <xsl:text>Web</xsl:text>
      </xsl:when>
      <xsl:when test="$code='mailto'">
        <xsl:text>Mail</xsl:text>
      </xsl:when>
      <xsl:when test="$code='H'">
        <xsl:text>Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='HV'">
        <xsl:text>Vacation Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='HP'">
        <xsl:text>Primary Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='WP'">
        <xsl:text>Work Place</xsl:text>
      </xsl:when>
      <xsl:when test="$code='WP AS'">
        <xsl:text>Work Place Answering Service</xsl:text>
      </xsl:when>
      <xsl:when test="$code='PUB'">
        <xsl:text>Pub</xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>{$code='</xsl:text>
        <xsl:value-of select="$code"/>
        <xsl:text>'?}</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="show-address">
    <xsl:param name="address"/>
    <xsl:choose>
      <xsl:when test="$address">
        <xsl:if test="$address/@use">
          <xsl:text> </xsl:text>
          <xsl:call-template name="translateTelecomCode">
            <xsl:with-param name="code" select="$address/@use"/>
          </xsl:call-template>
          <xsl:text>:</xsl:text>
          <br/>
        </xsl:if>
        <xsl:for-each select="$address/n1:streetAddressLine">
          <xsl:value-of select="."/>
          <!--<xsl:text >,&#160;</xsl:text>-->
        </xsl:for-each>
        <xsl:if test="$address/n1:streetName">
          <xsl:value-of select="$address/n1:streetName"/>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$address/n1:houseNumber"/>

        </xsl:if>
        <xsl:if test="string-length($address/n1:city)>0">
          <br/>
          <xsl:value-of select="$address/n1:city"/>
        </xsl:if>
        <xsl:if test="string-length($address/n1:state)>0">
          <!--<xsl:text>,&#160;</xsl:text>-->
          <br/>
          <xsl:value-of select="$address/n1:state"/>
        </xsl:if>
        <xsl:if test="string-length($address/n1:postalCode)>0">
          <!--<xsl:text>&#160;</xsl:text>-->
          <br/>
          <xsl:value-of select="$address/n1:postalCode"/>
        </xsl:if>
        <xsl:if test="string-length($address/n1:country)>0">
          <!--<xsl:text>,&#160;</xsl:text>-->
          <br/>
          <xsl:value-of select="$address/n1:country"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>address not available</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- End Component Of Section-->
  <xsl:template name="getParticipant">
    <xsl:param name="participant"/>
    <p>
      <xsl:call-template name="getName">
        <xsl:with-param name="name"
				select="$participant/n1:associatedPerson/n1:name"/>
      </xsl:call-template>
      <xsl:if test="$participant/n1:addr">
        <xsl:call-template name="getAddress">
          <xsl:with-param name="addr" select="$participant/n1:addr"/>
        </xsl:call-template>
      </xsl:if>
      <xsl:if test="$participant/n1:telecom">
        <xsl:for-each select="$participant/n1:telecom">
          <xsl:call-template name="getTelecom">
            <xsl:with-param name="telecom"
						 select="."/>
          </xsl:call-template>
        </xsl:for-each>
      </xsl:if>
    </p>
  </xsl:template>

  <xsl:template name="getGuardian">
    <xsl:param name="guardian"/>
    <p>
      <xsl:call-template name="getName">
        <xsl:with-param name="name"	select="$guardian/n1:guardianPerson/n1:name"/>
      </xsl:call-template>

      <xsl:if test="$guardian/n1:addr">
        <xsl:call-template name="getAddress">
          <xsl:with-param name="addr" select="$guardian/n1:addr"/>
        </xsl:call-template>
      </xsl:if>

      <xsl:if test="$guardian/n1:telecom">
        <xsl:for-each select="$guardian/n1:telecom">
          <xsl:call-template name="getTelecom">
            <xsl:with-param name="telecom"
						   select="."/>
          </xsl:call-template>
        </xsl:for-each>
      </xsl:if>

    </p>
  </xsl:template>

  <xsl:template name="getAddress">
    <xsl:param name="addr"/>

    <xsl:if test="$addr/n1:streetAddressLine[.!='']">
      <xsl:for-each select="$addr/n1:streetAddressLine">
        <xsl:if test=".!=''">
          <br></br>
          <xsl:value-of select="."/>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <br/>
    <xsl:if test="$addr/n1:city[.!='']">
      <xsl:value-of select="$addr/n1:city"/>
      <xsl:text>, </xsl:text>
    </xsl:if>

    <xsl:if test="$addr/n1:state[.!='']">
      <xsl:value-of select="$addr/n1:state"/>
      <xsl:text> </xsl:text>
    </xsl:if>

    <xsl:value-of select="$addr/n1:postalCode"/>
    <xsl:if test="$addr/n1:country">
      <br/>
      <xsl:value-of select="$addr/n1:country"/>
    </xsl:if>
  </xsl:template>

  <xsl:template name="getCode">
    <xsl:param name="code"/>
    <xsl:choose>
      <xsl:when test="$code/@extension">
        <br/>
        <xsl:text>Patient Identifier : </xsl:text>
        <xsl:value-of select="$code/@extension"/>
      </xsl:when>
      <!--<xsl:otherwise>
        <xsl:text>Information not available</xsl:text>
      </xsl:otherwise>-->
    </xsl:choose>
  </xsl:template>

  <xsl:template name="getOfficeAddress">
    <xsl:param name="addr"/>

    <xsl:if test="$addr/n1:streetAddressLine[.!='']">
      <xsl:for-each select="$addr/n1:streetAddressLine">
        <xsl:if test=".!=''">
          <xsl:value-of select="."/>,
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="$addr/n1:city[.!='']">
      <xsl:value-of select="$addr/n1:city"/>,
    </xsl:if>
    <xsl:if test="$addr/n1:state[.!='']">
      <xsl:value-of select="$addr/n1:state"/>,
    </xsl:if>

    <xsl:value-of select="$addr/n1:postalCode"/>

  </xsl:template>
  <xsl:template name="getTelecom">
    <xsl:param name="telecom"/>
    <xsl:variable name="phoneType" select="$telecom/@use"/>
    <xsl:if test="$telecom/@value[.!='']">
      <xsl:choose>
        <xsl:when test="$phoneType='HP'">
          <br></br>
          <xsl:value-of select="$telecom/@value"/>

        </xsl:when>
        <xsl:when test="$phoneType='WP'">
          <br></br>
          <xsl:value-of select="$telecom/@value"/>

        </xsl:when>
        <xsl:when test="$phoneType='MC'">
          <br></br>
          <xsl:value-of select="$telecom/@value"/>

        </xsl:when>
        <xsl:otherwise>
          <br></br>
          <xsl:value-of select="$telecom/@value"/>

        </xsl:otherwise>
      </xsl:choose>
    </xsl:if>

  </xsl:template>


  <!--<xsl:template name="getCareTeamTelecom">
    <xsl:param name="telecom"/>
    <xsl:variable name="phoneType" select="$telecom/@use"/>
    <xsl:choose>
      <xsl:when test="$phoneType='HP'">
        <xsl:value-of select="$telecom/@value"/> (Home)
      </xsl:when>
      <xsl:when test="$phoneType='WP'">
        <xsl:value-of select="$telecom/@value"/> (Work)
      </xsl:when>
      <xsl:when test="$phoneType='MC'">
        <xsl:value-of select="$telecom/@value"/> (Mobile)
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$telecom/@value"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>-->


  <!-- Get a Name  -->
  <xsl:template name="getName">
    <xsl:param name="name"/>

    <xsl:choose>


      <xsl:when test="$name/n1:family">

        <xsl:if test="$name/n1:prefix">
          <xsl:text>  </xsl:text>
          <xsl:value-of select="$name/n1:prefix"/>
          <xsl:text> </xsl:text>
        </xsl:if>

        <!--<xsl:value-of select="$name/n1:given"/>-->
        <xsl:if test="$name/n1:given[.!='']">

          <xsl:for-each select="$name/n1:given">
            <xsl:if test="@qualifier">
              <xsl:text> (</xsl:text>
            </xsl:if>
            <xsl:value-of select="."/>


            <xsl:if test="@qualifier">
              <xsl:text>)</xsl:text>
            </xsl:if>
            <xsl:text> </xsl:text>

          </xsl:for-each>

        </xsl:if>
        <xsl:text> </xsl:text>

        <!--<xsl:if test="$name/n1:middle[.!='']">
          <xsl:value-of select="$name/n1:middle"/>
          <xsl:text> </xsl:text>
        </xsl:if>-->

        <xsl:value-of select="$name/n1:family"/>
        <xsl:if test="$name/n1:suffix">
          <xsl:text>, </xsl:text>
          <xsl:value-of select="$name/n1:suffix"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$name"/>
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <!--  Format Date 
    
      outputs a date in Month Day, Year form
      e.g., 19991207  ==> December 07, 1999
-->
  <xsl:template name="formatDate">
    <xsl:param name="date"/>
    <xsl:variable name="month" select="substring ($date, 5, 2)"/>
    <xsl:choose>
      <xsl:when test="$month='01'">
        <xsl:text>January </xsl:text>
      </xsl:when>
      <xsl:when test="$month='02'">
        <xsl:text>February </xsl:text>
      </xsl:when>
      <xsl:when test="$month='03'">
        <xsl:text>March </xsl:text>
      </xsl:when>
      <xsl:when test="$month='04'">
        <xsl:text>April </xsl:text>
      </xsl:when>
      <xsl:when test="$month='05'">
        <xsl:text>May </xsl:text>
      </xsl:when>
      <xsl:when test="$month='06'">
        <xsl:text>June </xsl:text>
      </xsl:when>
      <xsl:when test="$month='07'">
        <xsl:text>July </xsl:text>
      </xsl:when>
      <xsl:when test="$month='08'">
        <xsl:text>August </xsl:text>
      </xsl:when>
      <xsl:when test="$month='09'">
        <xsl:text>September </xsl:text>
      </xsl:when>
      <xsl:when test="$month='10'">
        <xsl:text>October </xsl:text>
      </xsl:when>
      <xsl:when test="$month='11'">
        <xsl:text>November </xsl:text>
      </xsl:when>
      <xsl:when test="$month='12'">
        <xsl:text>December </xsl:text>
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test='substring ($date, 7, 1)="0"'>
        <xsl:value-of select="substring ($date, 8, 1)"/>
        <xsl:text>, </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="substring ($date, 7, 2)"/>
        <xsl:text>, </xsl:text>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:value-of select="substring ($date, 1, 4)"/>
  </xsl:template>

  <!-- StructuredBody -->
  <xsl:template match="n1:component/n1:structuredBody">
    <xsl:apply-templates select="n1:component/n1:section"/>
  </xsl:template>

  <!-- Component/Section -->
  <xsl:template match="n1:component/n1:section">
    <xsl:apply-templates select="n1:title"/>

    <xsl:apply-templates select="n1:text"/>

    <xsl:apply-templates select="n1:component/n1:section"/>
  </xsl:template>

  <!--   Title  -->
  <xsl:template match="n1:title">
    <h3 align="left" class="contenth3" >
      <span style="font-weight:bold;">
        <a name="{generate-id(.)}" href="#toc">
          <xsl:value-of select="."/>
        </a>
      </span>
    </h3>
  </xsl:template>

  <!--   Text   -->
  <xsl:template match="n1:text">
    <xsl:apply-templates />
  </xsl:template>

  <!--   paragraph  -->
  <xsl:template match="n1:paragraph">
    <p>
      <xsl:apply-templates/>
    </p>
  </xsl:template>

  <!--   line break  -->
  <xsl:template match="n1:br">
    <xsl:apply-templates/>
    <br/>
  </xsl:template>

  <!--     Content w/ deleted text is hidden -->
  <xsl:template match="n1:content[@revised='delete']"/>

  <!--   content  -->
  <xsl:template match="n1:content">
    <span>
      <xsl:apply-templates select="@styleCode"/>
      <xsl:apply-templates/>
    </span>
  </xsl:template>

  <!--   list  -->
  <xsl:template match="n1:list">
    <xsl:if test="n1:caption">
      <span style="font-weight:bold; ">
        <xsl:apply-templates select="n1:caption"/>
      </span>
    </xsl:if>
    <ul>
      <xsl:for-each select="n1:item">
        <li>
          <xsl:apply-templates />
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

  <xsl:template match="n1:list[@listType='ordered']">
    <xsl:if test="n1:caption">
      <span style="font-weight:bold; ">
        <xsl:apply-templates select="n1:caption"/>
      </span>
    </xsl:if>
    <ol>
      <xsl:for-each select="n1:item">
        <li>
          <xsl:apply-templates />
        </li>
      </xsl:for-each>
    </ol>
  </xsl:template>


  <!--   caption  -->
  <xsl:template match="n1:caption">
    <xsl:apply-templates/>
    <xsl:text>: </xsl:text>
  </xsl:template>

  <!--      Tables   -->
  <xsl:template match="n1:table/@*|n1:thead/@*|n1:tfoot/@*|n1:tbody/@*|n1:colgroup/@*|n1:col/@*|n1:tr/@*|n1:th/@*|n1:td/@*">
    <xsl:copy>

      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="n1:table">
    <table  class='table table-condensed'>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </table>
  </xsl:template>

  <xsl:template match="n1:thead">
    <thead>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </thead>
  </xsl:template>

  <xsl:template match="n1:tfoot">
    <tfoot>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </tfoot>
  </xsl:template>

  <xsl:template match="n1:tbody">
    <tbody>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </tbody>
  </xsl:template>

  <xsl:template match="n1:colgroup">
    <colgroup>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </colgroup>
  </xsl:template>

  <xsl:template match="n1:col">
    <col>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </col>
  </xsl:template>

  <xsl:template match="n1:tr">
    <tr>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </tr>
  </xsl:template>

  <xsl:template match="n1:th">
    <th>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </th>
  </xsl:template>

  <xsl:template match="n1:td">
    <td>
      <xsl:copy-of select="@*"/>
      <xsl:apply-templates/>
    </td>
  </xsl:template>

  <xsl:template match="n1:table/n1:caption">
    <span style="font-weight:bold; ">
      <xsl:apply-templates/>
    </span>
  </xsl:template>

  <!--   RenderMultiMedia 
         this currently only handles GIF's and JPEG's.  It could, however,
	 be extended by including other image MIME types in the predicate
	 and/or by generating <object> or <applet> tag with the correct
	 params depending on the media type  @ID  =$imageRef     referencedObject
 -->
  <xsl:template name="check-external-image-whitelist">
    <xsl:param name="current-whitelist"/>
    <xsl:param name="image-uri"/>
    <xsl:choose>
      <xsl:when test="string-length($current-whitelist) &gt; 0">
        <xsl:variable name="whitelist-item">
          <xsl:choose>
            <xsl:when test="contains($current-whitelist,'|')">
              <xsl:value-of select="substring-before($current-whitelist,'|')"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="$current-whitelist"/>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:variable>
        <xsl:choose>
          <xsl:when test="starts-with($image-uri,$whitelist-item)">
            <br clear="all"/>
            <xsl:element name="img">
              <xsl:attribute name="src">
                <xsl:value-of select="$image-uri"/>
              </xsl:attribute>
            </xsl:element>
            <xsl:message>
              <xsl:value-of select="$image-uri"/> is in the whitelist
            </xsl:message>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="check-external-image-whitelist">
              <xsl:with-param name="current-whitelist" select="substring-after($current-whitelist,'|')"/>
              <xsl:with-param name="image-uri" select="$image-uri"/>
            </xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>

      </xsl:when>
      <xsl:otherwise>
        <p>
          WARNING: non-local image found <xsl:value-of select="$image-uri"/>. Removing. If you wish non-local images preserved please set the limit-external-images param to 'no'.
        </p>
        <xsl:message>
          WARNING: non-local image found <xsl:value-of select="$image-uri"/>. Removing. If you wish non-local images preserved please set the limit-external-images param to 'no'.
        </xsl:message>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>


  <xsl:template match="n1:renderMultiMedia">
    <xsl:variable name="imageRef" select="@referencedObject"/>
    <xsl:choose>
      <xsl:when test="//n1:regionOfInterest[@ID=$imageRef]">
        <!-- Here is where the Region of Interest image referencing goes -->
        <xsl:if test="//n1:regionOfInterest[@ID=$imageRef]//n1:observationMedia/n1:value[@mediaType='image/gif' or @mediaType='image/jpeg']">
          <xsl:variable name="image-uri" select="//n1:regionOfInterest[@ID=$imageRef]//n1:observationMedia/n1:value/n1:reference/@value"/>

          <xsl:choose>
            <xsl:when test="$limit-external-images='yes' and (contains($image-uri,':') or starts-with($image-uri,'\\'))">
              <xsl:call-template name="check-external-image-whitelist">
                <xsl:with-param name="current-whitelist" select="$external-image-whitelist"/>
                <xsl:with-param name="image-uri" select="$image-uri"/>
              </xsl:call-template>
              <!--
                            <p>WARNING: non-local image found <xsl:value-of select="$image-uri"/>. Removing. If you wish non-local images preserved please set the limit-external-images param to 'no'.</p>
                            <xsl:message>WARNING: non-local image found <xsl:value-of select="$image-uri"/>. Removing. If you wish non-local images preserved please set the limit-external-images param to 'no'.</xsl:message>
                            -->
            </xsl:when>
            <!--
                        <xsl:when test="$limit-external-images='yes' and starts-with($image-uri,'\\')">
                            <p>WARNING: non-local image found <xsl:value-of select="$image-uri"/></p>
                            <xsl:message>WARNING: non-local image found <xsl:value-of select="$image-uri"/>. Removing. If you wish non-local images preserved please set the limit-external-images param to 'no'.</xsl:message>
                        </xsl:when>
                        -->
            <xsl:otherwise>
              <br clear="all"/>
              <xsl:element name="img">
                <xsl:attribute name="src">
                  <xsl:value-of select="$image-uri"/>
                </xsl:attribute>
              </xsl:element>
            </xsl:otherwise>
          </xsl:choose>

        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <!-- Here is where the direct MultiMedia image referencing goes -->
        <xsl:if test="//n1:observationMedia[@ID=$imageRef]/n1:value[@mediaType='image/gif' or @mediaType='image/jpeg']">
          <br clear="all"/>
          <xsl:element name="img">
            <xsl:attribute name="src">
              <xsl:value-of select="//n1:observationMedia[@ID=$imageRef]/n1:value/n1:reference/@value"/>
            </xsl:attribute>
          </xsl:element>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- 	Stylecode processing   
	  Supports Bold, Underline and Italics display
-->
  <xsl:template match="@styleCode">
    <xsl:attribute name="class">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <!--<xsl:template match="//n1:*[@styleCode]">
    <xsl:if test="@styleCode='Bold'">
      <xsl:element name='b'>
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="@styleCode='Italics'">
      <xsl:element name='i'>
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="@styleCode='Underline'">
      <xsl:element name='u'>
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Bold') and contains(@styleCode,'Italics') and not (contains(@styleCode, 'Underline'))">
      <xsl:element name='b'>
        <xsl:element name='i'>
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Bold') and contains(@styleCode,'Underline') and not (contains(@styleCode, 'Italics'))">
      <xsl:element name='b'>
        <xsl:element name='u'>
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Italics') and contains(@styleCode,'Underline') and not (contains(@styleCode, 'Bold'))">
      <xsl:element name='i'>
        <xsl:element name='u'>
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Italics') and contains(@styleCode,'Underline') and contains(@styleCode, 'Bold')">
      <xsl:element name='b'>
        <xsl:element name='i'>
          <xsl:element name='u'>
            <xsl:apply-templates/>
          </xsl:element>
        </xsl:element>
      </xsl:element>
    </xsl:if>
  </xsl:template>-->

  <!-- 	Superscript or Subscript   -->
  <xsl:template match="n1:sup">
    <xsl:element name='sup'>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="n1:sub">
    <xsl:element name='sub'>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>

  <xsl:template name="generatedline">
    <p align="center" style="margin:5px;padding:0px;font-style: italic;">
      <xsl:text>Generated </xsl:text>
      <xsl:choose>
        <xsl:when test="/n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:assignedPerson/n1:name and /n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:assignedPerson/n1:name != ''">
          <xsl:text> by: </xsl:text>
          <xsl:call-template name="getName">
            <xsl:with-param name="name"
										 select="/n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:assignedPerson/n1:name"/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="/n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:representedOrganization/n1:name and /n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:representedOrganization/n1:name != ''">
          <xsl:text> by: </xsl:text>
          <xsl:call-template name="getName">
            <xsl:with-param name="name"
										 select="/n1:ClinicalDocument/n1:legalAuthenticator/n1:assignedEntity/n1:representedOrganization/n1:name"/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name and /n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name != ''">
          <xsl:text> by: </xsl:text>
          <xsl:call-template name="getName">
            <xsl:with-param name="name"
						 select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name"/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name and /n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name != ''">
          <xsl:text> by: </xsl:text>
          <xsl:call-template name="getName">
            <xsl:with-param name="name"
						 select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name"/>
          </xsl:call-template>
        </xsl:when>
        <xsl:when test="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedAuthoringDevice/n1:softwareName and /n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedAuthoringDevice/n1:softwareName != ''">
          <xsl:text> by: </xsl:text>
          <xsl:call-template name="getName">
            <xsl:with-param name="name"
						 select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedAuthoringDevice/n1:softwareName"/>
          </xsl:call-template>
        </xsl:when>
      </xsl:choose>
      <xsl:text> on </xsl:text>
      <xsl:call-template name="formatDate">
        <xsl:with-param name="date"
				  select="//n1:ClinicalDocument/n1:effectiveTime/@value"/>
      </xsl:call-template>
      <xsl:if test="/n1:ClinicalDocument/n1:id/@extension = 'Patient Copy'">
        <h3 align="center" style="margin:5px;padding:0px;">
          <xsl:value-of select="/n1:ClinicalDocument/n1:id/@extension"></xsl:value-of>
        </h3>
      </xsl:if>
    </p>
  </xsl:template>

</xsl:stylesheet>
<!-- Stylus Studio meta-information - (c) 2004-2009. Progress Software Corporation. All rights reserved.

<metaInformation>
	<scenarios>
		<scenario default="yes" name="Scenario1" userelativepaths="yes" externalpreview="no" url="..\Sage_Sample_C32_Dede.xml" htmlbaseurl="" outputurl="" processortype="saxon8" useresolver="no" profilemode="0" profiledepth="" profilelength=""
		          urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext="" validateoutput="no" validator="internal"
		          customvalidator="">
			<advancedProp name="sInitialMode" value=""/>
			<advancedProp name="bXsltOneIsOkay" value="true"/>
			<advancedProp name="bSchemaAware" value="false"/>
			<advancedProp name="bXml11" value="false"/>
			<advancedProp name="iValidation" value="0"/>
			<advancedProp name="bExtensions" value="true"/>
			<advancedProp name="iWhitespace" value="0"/>
			<advancedProp name="sInitialTemplate" value=""/>
			<advancedProp name="bTinyTree" value="true"/>
			<advancedProp name="bWarnings" value="true"/>
			<advancedProp name="bUseDTD" value="false"/>
			<advancedProp name="iErrorHandling" value="fatal"/>
		</scenario>
	</scenarios>
	<MapperMetaTag>
		<MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no"/>
		<MapperBlockPosition></MapperBlockPosition>
		<TemplateContext></TemplateContext>
		<MapperFilter side="source"></MapperFilter>
	</MapperMetaTag>
</metaInformation>
-->