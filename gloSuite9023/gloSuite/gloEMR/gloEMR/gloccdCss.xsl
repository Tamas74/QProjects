<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:n3="http://www.w3.org/1999/xhtml" xmlns:n1="urn:hl7-org:v3" xmlns:n2="urn:hl7-org:v3/meta/voc" xmlns:voc="urn:hl7-org:v3/voc" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <xsl:output method="html" indent="yes" version="4.01" encoding="ISO-8859-1" doctype-public="-//W3C//DTD HTML 4.01//EN"/>

  <!-- CDA document -->

  <xsl:variable name="tableWidth">50%</xsl:variable>

  <xsl:variable name="title">
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:title">
        <xsl:value-of select="/n1:ClinicalDocument/n1:title"/>
      </xsl:when>
      <xsl:otherwise>Clinical Document</xsl:otherwise>
    </xsl:choose>
  </xsl:variable>

  <xsl:template match="/">
    <xsl:apply-templates select="n1:ClinicalDocument"/>
  </xsl:template>

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
          html
          {

          }

          body
          {
          border: 0;
          font-family: arial;
          font-size: 12px;
          color: #0069aa; <!--003c6a      0069aa-->
          background: #fefdfd;    <!--fefdfd-->
          <!--border-color:#9fb5dd;-->

          }
          table
          {
          background:url('http://www.glostream.com//css/XSLT/$assets/Bluheader.png')
          border: 0px;
          padding: 0px;
          margin: 0px;
          cell-spacing: 0px;
          <!--background-color:#f4fcff;-->
          border-color:#4e93be;
          <!--border-color:#9fb5dd;-->
          border-collapse: collapse;

          }

          td
          {
          border-color:#4e93be;
          <!--border-color:#9fb5dd;-->
          padding: 3px;

          }
          tr
          {

          border-color:#4e93be;
          <!--border-color:#9fb5dd;-->
          <!--background: #f0f7ff;-->
          }

          th
          {
          font-family:Arial;
          font-size: 12px;
          font-weight: bold;
          background:url('http://www.glostream.com//css/XSLT/$assets/gridheader.jpg');
          color: #ffffff;
          border-color:#4e93be;
          text-align:Left;
          }

          .tralt
          {
          border-color:#9fb5dd;
          <!--background: black;-->
          }

          .TableOverwite
          {
          color : white;
          background:url('http://www.glostream.com//css/XSLT/$assets/Bluheader.png');
          border-color:#9fb5dd;
          border-collapse: collapse;
          padding-left:15px;
          }

          A:link
          {
          font-family :Arial;
          font-size : 12px;
          color : #0069aa;
          font-weight: bold;
          }

          A:visited
          {
          font-family :Arial;
          font-size : 12px;
          color : #666666;
          font-weight: bold;
          }

          A:active
          {
          color: #008469;
          }

          A:hover
          {
          font-family :Arial;
          font-size : 12px;
          color : #f1bc2f;
          font-weight: bold;
          }


        </style>
      </head>
      <xsl:comment>

      </xsl:comment>
      <body>

        <!-- <img src="SageLogo.gif" align="right"/> -->
        <br/>
        <h2 align="center"  >
          <xsl:value-of select="$title"/>
        </h2>
        <xsl:call-template name="generatedline"/>
        <table width='100%' class='TableOverwite'   >
          <xsl:variable name="patientRole" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole"/>
          <tr padding-top="15px" >

            <td width='15%' valign="top"  >
              <br/>
              <xsl:text>Patient: </xsl:text>
            </td>
            <td width='35%' valign="top"   >
              <br/>
              <b>
                <xsl:call-template name="getName">
                  <xsl:with-param name="name"
						   select="$patientRole/n1:patient/n1:name"/>
                </xsl:call-template>
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
              </b>
            </td>
            <td width='15%' align='right' valign="top">
              <br/>
              <!--<xsl:text>Patient Number: </xsl:text>-->
            </td>
            <td width='35%' valign="top">
              <b>
                <!--<br/>
								<xsl:value-of select="$patientRole/n1:id/@extension" disable-output-escaping="yes" />-->
              </b>
            </td>
          </tr>
          <tr>
            <td width='15%' valign="top" >
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
            <td width='15%' align='right' valign="top">
              <xsl:text>Sex: </xsl:text>
            </td>
            <td width='35%' valign="top">
              <b>
                <xsl:variable name="sex"
					select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:administrativeGenderCode/@code"/>
                <xsl:choose>
                  <xsl:when test="$sex='M'">Male</xsl:when>
                  <xsl:when test="$sex='F'">Female</xsl:when>
                  <xsl:when test="$sex='UN'">Other</xsl:when>
                </xsl:choose>
              </b>
            </td>
          </tr>
          <tr>
            <xsl:variable name="maritalStatus" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:maritalStatusCode/@displayName"/>
            <xsl:choose>
              <xsl:when test="$maritalStatus">
                <td width="15%" valign="top">Marital Status: </td>
                <td width="35%" valign="top">
                  <b>
                    <xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:maritalStatusCode/@displayName"/>
                  </b>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td width="15%">&#160;</td>
                <td width="35%">&#160;</td>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:variable name="race" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@displayName"/>
            <xsl:choose>
              <xsl:when test="$race">
                <td width="15%"  align='right' valign="top">Race: </td>
                <td width="35%" valign="top">
                  <b>
                    <xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:raceCode/@displayName"/>
                  </b>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td width="15%">&#160;</td>
                <td width="35%">&#160;</td>
              </xsl:otherwise>
            </xsl:choose>
          </tr>
          <tr>
            <xsl:variable name="guardian" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:guardian"/>
            <xsl:choose>
              <xsl:when test="$guardian">
                <td width="15%" valign="top">Guardian: </td>
                <td width="35%" valign="top">
                  <b>
                    <xsl:call-template name="getGuardian">
                      <xsl:with-param name="guardian" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:guardian"/>
                    </xsl:call-template>
                  </b>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td width="15%">&#160;</td>
                <td width="35%">&#160;</td>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:variable name="emergencycontact" select="/n1:ClinicalDocument/n1:participant[@typeCode='IND']/n1:associatedEntity[@classCode='ECON']"/>
            <xsl:choose>
              <xsl:when test="$emergencycontact">
                <td width="15%" align="right" valign="top">Emergency Contact: </td>
                <td width="35%" valign="top" >
                  <b>
                    <xsl:call-template name="getParticipant">
                      <xsl:with-param name="participant" select="/n1:ClinicalDocument/n1:participant[@typeCode='IND']/n1:associatedEntity[@classCode='ECON']"/>
                    </xsl:call-template>
                  </b>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td width="15%">&#160;</td>
                <td width="35%">&#160;</td>
              </xsl:otherwise>
            </xsl:choose>
          </tr>
        </table>
        <div>
          <h3>
            <a name="toc">Table of Contents</a>
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
        </div>
        <xsl:apply-templates select="n1:component/n1:structuredBody"/>

      </body>
    </html>
  </xsl:template>

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
        <br/>
        <xsl:value-of select="."/>
      </xsl:for-each>
    </xsl:if>
    <br/>
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
    <br/>
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
  </xsl:template>

  <!-- Get a Name  -->
  <xsl:template name="getName">
    <xsl:param name="name"/>
    <xsl:choose>
      <xsl:when test="$name/n1:family">
        <xsl:value-of select="$name/n1:given"/>
        <xsl:text> </xsl:text>

        <xsl:if test="$name/n1:middle[.!='']">
          <xsl:value-of select="$name/n1:middle"/>
          <xsl:text> </xsl:text>
        </xsl:if>

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
    <h3>
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
    <xsl:apply-templates/>
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
    <table>
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
  <xsl:template match="n1:renderMultiMedia">
    <xsl:variable name="imageRef" select="@referencedObject"/>
    <xsl:choose>
      <xsl:when test="//n1:regionOfInterest[@ID=$imageRef]">
        <!-- Here is where the Region of Interest image referencing goes -->
        <xsl:if test='//n1:regionOfInterest[@ID=$imageRef]//n1:observationMedia/n1:value[@mediaType="image/gif" or @mediaType="image/jpeg"]'>
          <br clear='all'/>
          <xsl:element name='img'>
            <xsl:attribute name='src'>
              <xsl:value-of select='//n1:regionOfInterest[@ID=$imageRef]//n1:observationMedia/n1:value/n1:reference/@value'/>
            </xsl:attribute>
          </xsl:element>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <!-- Here is where the direct MultiMedia image referencing goes -->
        <xsl:if test='//n1:observationMedia[@ID=$imageRef]/n1:value[@mediaType="image/gif" or @mediaType="image/jpeg"]'>
          <br clear='all'/>
          <xsl:element name='img'>
            <xsl:attribute name='src'>
              <xsl:value-of select='//n1:observationMedia[@ID=$imageRef]/n1:value/n1:reference/@value'/>
            </xsl:attribute>
          </xsl:element>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- 	Stylecode processing   
	  Supports Bold, Underline and Italics display
-->
  <xsl:template match="//n1:*[@styleCode]">
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
  </xsl:template>

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
    <p align="center">
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