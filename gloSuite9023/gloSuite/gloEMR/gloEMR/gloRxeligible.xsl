<?xml version="1.0" encoding="iso-8859-1"?>
<!-- DWXMLSource="rxelg_442012111544841.xml" -->
<!DOCTYPE xsl:stylesheet  [
  <!ENTITY nbsp   "&#160;">
  <!ENTITY copy   "&#169;">
  <!ENTITY reg    "&#174;">
  <!ENTITY trade  "&#8482;">
  <!ENTITY mdash  "&#8212;">
  <!ENTITY ldquo  "&#8220;">
  <!ENTITY rdquo  "&#8221;">
  <!ENTITY pound  "&#163;">
  <!ENTITY yen    "&#165;">
  <!ENTITY euro   "&#8364;">
]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="iso-8859-1" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
  <xsl:template match="/">

    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
        <title>Untitled Document</title>
      </head>
      <style type="text/css">
        a hover{
        color:#F0B310;
        text-decoration:underline;
        }
        a selected{
        color:#F0B310;
        text-decoration:none;
        }

        a{
        color: #0069aa;
        text-decoration:underline;
        }
        .header
        {
        background-color: #0069AA;
        background: -webkit-gradient(linear, left top, left bottom, from(#0069AA), to(#0E92F7));
        background: -moz-linear-gradient(top,  #0069AA,  #0E92F7);
        background: -ms-linear-gradient(top, #0069AA, #0E92F7);
        background: -o-linear-gradient(top, #0069AA, #0E92F7);
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0069AA', endColorstr='#0E92F7');
        zoom:1;
        /*background-color: #0069aa;*/
        /*border-top: 2px solid #F0B310;*/
        color: white;
        display: block;
        font-family: Tahoma,Geneva,sans-serif;
        font-size: 13px;
        font-weight: bold;
        /*margin-top: 5px;*/
        padding: 2px;
        text-decoration: none;

        }
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
        font-size: 18px;
        font-weight: bold;
        margin-left:7px;
        /*padding-left:2px;*/
        text-decoration:none;

        }
        .Topheader
        {
        color: #F0B310;
        display: block;
        font-family: Calibri;
        font-size: 28px;
        font-weight: bold;
        margin-left: 10px;
        /*margin-bottom: 5px;
        padding-bottom: 2px;*/
        width: 100%;
        margin-top: -10px;
        }
        .TopLink
        {
        color: white;
        display: block;
        font-family: Tahoma,Geneva,sans-serif;
        font-size: 12px;
        font-weight: bold;
        padding: 2px;
        text-decoration: underline;
        margin-right:5px;
        }

        .LinkHeader
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:14px;
        /* font-weight:bold*/
        color:#0069aa;
        text-decoration:underline;
        /*border-bottom: 1px dotted #0069aa;
        padding-bottom:12px;*/
        }
        .physician
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        /* font-weight:bold*/
        color:#0069aa;
        text-decoration:none;
        /*background-color:#effaff;*/
        padding:2px;
        margin-left:5px;
        margin-top:5px;
        word-wrap: break-word;
        padding-right:2px;
        }

        .physicianValue
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        color:#0069aa;
        text-decoration:none;
        padding:2px;
        word-wrap: break-word;
        font-weight:normal;
        margin-left:370px;
        padding-right:2px;
        /*float:right;*/
        /*background-color:#effaff;*/
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
        margin-left:5px;
        padding-left:2px;
        }

        .orangeheader
        {
        background-color:#fdf4df;
        border: 1px solid #f98800;
        color:black
        }
        .physicianCapfont
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        font-weight:bold;
        text-decoration:none;
        color:#000;
        }
        .gradient
        {
        background-color:#ffffff;
        border:1px solid #0069AA;
        }
        .GrayGradient
        {
        background-color:#ffffff;
        border:1px solid #5a5959;
        }
        .GrayPhysicianCap
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        font-weight:bold;
        color:#666;
        text-decoration:none;
        padding:2px;
        width:370px;
        float:left;
        word-wrap: break-word;
        }
        .GrayPhysician
        {
        font-family:Tahoma, Geneva, sans-serif;
        font-size:12px;
        color:#666;
        text-decoration:none;
        font-weight:normal;
        padding:2px;
        margin-left:370px;
        margin-top:3px;
        padding-right:2px;
        word-wrap: break-word;
        }
        .GrayHeader
        {
        background-color: #5a5959;
        background: -webkit-gradient(linear, left top, left bottom, from(#5a5959), to(#8d8d8d));
        background: -moz-linear-gradient(top,  #5a5959,  #8d8d8d);
        background: -ms-linear-gradient(top, #5a5959, #8d8d8d);
        background: -o-linear-gradient(top, #5a5959, #8d8d8d);
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#5a5959', endColorstr='#8d8d8d');
        zoom:1;
        color: white;
        display: block;
        font-family: Tahoma,Geneva,sans-serif;
        font-size: 13px;
        font-weight: bold;
        margin-top: 5px;
        padding: 2px;
        text-decoration: none;
        }

      </style>

      <body>
        <div class="Topheader">
          Pharmacy Benefit Response<a name="top"></a>
        </div>
        <ul class="LinkHeader">
          <xsl:for-each select="NewDataSet/eligibility">
            <li >
              <a href="#{generate-id(.)}" >
                Benefit Source
                <xsl:value-of select="round(sSTLoopControlID[.])"/> : <xsl:value-of select="sPBM_PayerName"/>
                <!--<xsl:value-of select="sSTLoopControlID"/>-->
              </a>
            </li>
          </xsl:for-each>
        </ul>
        <!--
		<div style="padding:7px;font-family:Tahoma;font-size:13px;font-weight:bold;color:#000;">
		Eligibility Date Time : <xsl:value-of select="/NewDataSet/eligibility/dtResquestDateTimeStamp"/>
		</div>-->

        <xsl:for-each select="NewDataSet/eligibility">
          <div style="margin-top:5px;padding-right:2px;">
            <div class="Mainheader">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="80%">
                    <a name="{generate-id(.)}" class="Mainheader">
                      Benefit Source
                      <xsl:variable name="cnt" select="sSTLoopControlID"/>
                      <xsl:choose>
                        <xsl:when test="$cnt='0001'">1 </xsl:when>
                        <xsl:when test="$cnt='0002'">2 </xsl:when>
                        <xsl:when test="$cnt='0003'">3 </xsl:when>
                      </xsl:choose>
                      : <xsl:value-of select="sPBM_PayerName"/>
                      <!--<xsl:value-of select="sSTLoopControlID"/>-->
                    </a>
                  </td>
                  <td width="20%" align="right" valign="middle">
                    <a href="#top" class="TopLink">Top</a>
                  </td>
                </tr>
              </table>
            </div>
            <div class="Orgphysician">
              <table width="100%" class="orangeheader" >
                <tr width="100%">
                  <td width="30%" class="physicianCapfont">Physician Name : </td>
                  <td width="20%">
                    <xsl:value-of select="sPhysicianName"/>
                    <xsl:value-of select="sPhysicianSuffix"/>
                  </td>
                  <td width="30%" class="physicianCapfont">PBM participant ID :</td>
                  <td width="20%">
                    <xsl:value-of select="sPBM_PayerParticipantID"/>
                  </td>
                </tr>
                <tr>
                  <td class="physicianCapfont">NPI No :</td>
                  <td>
                    <xsl:value-of select="sNPINumber"/>
                  </td>
                  <td class="physicianCapfont">Subscriber Transaction Identifier :</td>
                  <td>
                    <xsl:value-of select="sTRNReferenceIdentification"/>
                  </td>
                </tr>
                <tr>
                  <td class="physicianCapfont">Pharmacy Benefit Source Response :</td>
                  <td>
                    <xsl:value-of select="sSTLoopControlID"/>
                  </td>
                  <td class="physicianCapfont">Employee identification No :</td>
                  <td>
                    <xsl:value-of select="sTRNOrignationCompanyIdentifier"/>
                  </td>
                </tr>
                <tr>
                  <td class="physicianCapfont">PBM name :</td>
                  <td>
                    <xsl:value-of select="sPBM_PayerName"/>
                  </td>
                  <td class="physicianCapfont">Subscriber transaction division or group :</td>
                  <td>
                    <xsl:value-of select="sTRNDivisionorGroup"/>
                  </td>
                </tr>
              </table>
            </div>
          </div>
          <div style="color:red;font-weight:bold;background-color:#FFFFFF;padding-left:5px;margin-top:10px;font-family:Tahoma;font-size:12px;">
            <xsl:if test="sRespDtlMessageType[.!='']">
              <span>
                *Note : PBM <xsl:value-of select="sPBM_PayerName"/> received rejection message <xsl:value-of select="sRespDtlMessageType"/>
              </span>
            </xsl:if>
          </div>

          <xsl:if test="SubscriberName[.!=''] or SubscriberSuffix[.!=''] or sPBM_PayerMemberID[.!=''] or sSubscriberDOB[.!=''] or sSubscriberGender[.!=''] or sSubscriberAddress1[.!=''] or sSubscriberAddress2[.!=''] or sSubscriberCity[.!=''] or sSubscriberState[.!=''] or sSubscriberZip[.!=''] or sSubscriberSSN[.!=''] or sRelationshipDescription[.!=''] or sServiceDate[.!=''] or sEligiblityDate[.!=''] or IsSubscriberdemoChange[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">PBM Member Information</td>
                </tr>
                <tr>
                  <td>
                    <div></div>
                    <xsl:if test="IsSubscriberdemoChange[.!='']">
                      <div class="physicianCap">
                        Subscriber demographics changed :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes']">
                            <span style="color:#FF0000">
                              <xsl:value-of select="IsSubscriberdemoChange"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="IsSubscriberdemoChange"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>
                    <xsl:if test="SubscriberName[.!=''] and SubscriberName[.!='  ']">
                      <div class="physicianCap">
                        Member name :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate(SubscriberName[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatName[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ') or SubscriberSuffix[.!='']">
                            <span style="color:#FF0000">
                              <xsl:value-of select="SubscriberName"/>
                              <xsl:if test="SubscriberSuffix[.!='']">
                                , <xsl:value-of select="SubscriberSuffix"/>
                              </xsl:if>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="SubscriberName"/>
                            <xsl:if test="SubscriberSuffix[.!='']">
                              , <xsl:value-of select="SubscriberSuffix"/>
                            </xsl:if>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPBM_PayerMemberID[.!='']">
                      <div class="physicianCap">
                        Member identification No :
                      </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPBM_PayerMemberID"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberDOB[.!='']">
                      <div class="physicianCap">
                        Member DOB :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and sSubscriberDOB[.!=/NewDataSet/Patient/PatDOB[.]]">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberDOB"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberDOB"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberGender[.!='']">
                      <div class="physicianCap">
                        Member gender :
                      </div>
                      <div class="physicianValue">
                        <xsl:variable name="sex" select="sSubscriberGender"/>
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate($sex, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatGender[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')">
                            <span style="color:#FF0000">
                              <xsl:choose>
                                <xsl:when test="$sex='M'">Male</xsl:when>
                                <xsl:when test="$sex='F'">Female</xsl:when>
                                <xsl:when test="$sex='U'">Unknown</xsl:when>
                              </xsl:choose>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:choose>
                              <xsl:when test="$sex='M'">Male</xsl:when>
                              <xsl:when test="$sex='F'">Female</xsl:when>
                              <xsl:when test="$sex='U'">Unknown</xsl:when>
                            </xsl:choose>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberAddress1[.!='']">
                      <div class="physicianCap">
                        Member address line 1 :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate(sSubscriberAddress1[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatAddressLine1[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberAddress1"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberAddress1"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberAddress2[.!='']">
                      <div class="physicianCap">
                        Member address line 2 :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate(sSubscriberAddress2[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatAddressLine2[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberAddress2"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberAddress2"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberCity[.!='']">
                      <div class="physicianCap">
                        Member city :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate(sSubscriberCity[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatCity[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberCity"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberCity"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberState[.!='']">
                      <div class="physicianCap">
                        Member state :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and translate(sSubscriberState[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')!=translate(/NewDataSet/Patient/PatState[.], 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberState"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberState"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberZip[.!='']">
                      <div class="physicianCap">
                        Member Zip :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and sSubscriberZip[.!=/NewDataSet/Patient/PatZip[.]]">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberZip"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberZip"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSubscriberSSN[.!='']">
                      <div class="physicianCap">
                        Member SSN :
                      </div>
                      <div class="physicianValue">
                        <xsl:choose>
                          <xsl:when test="IsSubscriberdemoChange[.='Yes'] and sSubscriberSSN[.!=/NewDataSet/Patient/PatSSN[.]]">
                            <span style="color:#FF0000">
                              <xsl:value-of select="sSubscriberSSN"/>
                            </span>
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of select="sSubscriberSSN"/>
                          </xsl:otherwise>
                        </xsl:choose>
                      </div>
                    </xsl:if>

                    <xsl:if test="sRelationshipDescription[.!='']">
                      <div class="physicianCap">Relationship :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sRelationshipDescription"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sServiceDate[.!='']">
                      <div class="physicianCap">Service date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sServiceDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sEligiblityDate[.!='']">
                      <div class="physicianCap">Eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sEligiblityDate"/>
                      </div>
                    </xsl:if>


                    <xsl:if test="sRelationshipDescription[.='Dependent']">

                      <xsl:if test="DependentName[.!='']">
                        <div class="physicianCap">Dependent name :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="DependentName"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDGender[.!='']">
                        <div class="physicianCap">Dependent gender :</div>
                        <div class="physicianValue">
                          <xsl:variable name="sex" select="sDGender"/>
                          <xsl:choose>
                            <xsl:when test="$sex='M'">Male</xsl:when>
                            <xsl:when test="$sex='F'">Female</xsl:when>
                            <xsl:when test="$sex='U'">Unknown</xsl:when>
                          </xsl:choose>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDDOB[.!='']">
                        <div class="physicianCap">Dependent DOB :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDDOB"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDSSN[.!='']">
                        <div class="physicianCap">Dependent SSN :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDSSN"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDAddress1[.!='']">
                        <div class="physicianCap">Dependent address line 1 :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDAddress1"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDAddress2[.!='']">
                        <div class="physicianCap">Dependent address line 2 :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDAddress2"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDCity[.!='']">
                        <div class="physicianCap">Dependent city :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDCity"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDState[.!='']">
                        <div class="physicianCap">Dependent state :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDState"/>
                        </div>
                      </xsl:if>

                      <xsl:if test="sDZip[.!='']">
                        <div class="physicianCap">Dependent ZIP :</div>
                        <div class="physicianValue">
                          <xsl:value-of select="sDZip"/>
                        </div>
                      </xsl:if>
                    </xsl:if>

                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sCardHolderID[.!=''] or sSocialSecurityNumber[.!=''] or sCardHolderName[.!=''] or sPatientAccountNumber[.!=''] ">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">PBM Member Details</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sCardHolderID[.!='']">
                      <div class="physicianCap">Identity card No :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sCardHolderID"/>
                      </div>
                    </xsl:if>
                    <xsl:if test="sSocialSecurityNumber[.!='']">
                      <div class="physicianCap">Social security No : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sSocialSecurityNumber"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sCardHolderName[.!='']">
                      <div class="physicianCap">Card holder name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sCardHolderName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPatientAccountNumber[.!='']">
                      <div class="physicianCap">Patient account No :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPatientAccountNumber"/>
                      </div>
                    </xsl:if>
                    <!--<xsl:if test="sPersonCode[.!='']">
                  <div class="physicianCap">Familiy unit member (*Person code):</div>
                  <div class="physicianValue">
                    <xsl:value-of select="sPersonCode"/>
                  </div>
                </xsl:if>-->
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sHealthPlanName[.!=''] or sEmployeeID[.!=''] or sHealthPlanNumber[.!=''] or sBINNumberPCNNumber[.!=''] or sGroupName[.!=''] or sGroupID[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">Subscriber Information More Details</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sHealthPlanName[.!='']">
                      <div class="physicianCap">Health plan name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sHealthPlanName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sEmployeeID[.!='']">
                      <div class="physicianCap">Employee ID :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sEmployeeID"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sHealthPlanNumber[.!='']">
                      <div class="physicianCap">Health plan No :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sHealthPlanNumber"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sBINNumberPCNNumber[.!='']">
                      <div class="physicianCap">Plan Network Id (*BIN/PCN) :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sBINNumberPCNNumber"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sGroupName[.!='']">
                      <div class="physicianCap">Group name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sGroupName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sGroupID[.!='']">
                      <div class="physicianCap">Group No :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sGroupID"/>
                      </div>
                    </xsl:if>
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sHealthPlanBenefitEligibilityInfo[.!=''] or sHealthPlanBenefitCoverageName[.!=''] or sHlthPlnCovInsTypeCode[.!=''] or HtlthPlnCovBenftEligDate[.!=''] or HtlthPlnCovBenftServiceDate[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">Health Plan Benefit Coverage Details</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sHealthPlanBenefitEligibilityInfo[.!='']">
                      <div class="physicianCap">Health plan benefit status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sHealthPlanBenefitEligibilityInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sHealthPlanBenefitCoverageName[.!='']">
                      <div class="physicianCap">Health plan benefit plan coverage name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sHealthPlanBenefitCoverageName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sHlthPlnCovInsTypeCode[.!='']">
                      <div class="physicianCap">Health plan benefit insurance code :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sHlthPlnCovInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="HtlthPlnCovBenftEligDate[.!='']">
                      <div class="physicianCap">Health plan benefit eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="HtlthPlnCovBenftEligDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="HtlthPlnCovBenftServiceDate[.!='']">
                      <div class="physicianCap">Health plan benefit service date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="HtlthPlnCovBenftServiceDate"/>
                      </div>
                    </xsl:if>
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sRetailPhEligiblityorBenefitInfo[.!=''] or sRetailPharmacyCoverageName[.!=''] or sRetailInsTypeCode[.!=''] or sRetailMonetaryAmount[.!=''] or RetailPharmEligDate[.!=''] or RetailPharmServiceDate[.!=''] or sMailOrdEligiblityorBenefitInfo[.!=''] or sMailOrderRxDrugCoverageName[.!=''] or sMailOrderInsTypeCode[.!=''] or sMailOrderMonetaryAmount[.!=''] or MailOrdPharmEligDate[.!=''] or MailOrdPharmServiceDate[.!=''] or LTCPhEligiblityorBenefitInfo[.!=''] or LTCPharmCovName[.!=''] or sLTCPharmacyInsTypeCode[.!=''] or LTCPharmEligDate[.!=''] or LTCPharmServiceDate[.!=''] or LTCPharmServiceDate[.!=''] or SpecialityPhEligiblityorBenefitInfo[.!=''] or SpecialtyPharmCovName[.!=''] or sSpecialtyPharmacyInsTypeCode[.!=''] or SpecialtyPharmEligDate[.!=''] or SpecialtyPharmServiceDate[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">Pharmacy Coverage Information</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sRetailPhEligiblityorBenefitInfo[.!='']">
                      <div class="physicianCap">Retail pharmacy status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sRetailPhEligiblityorBenefitInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sRetailPharmacyCoverageName[.!='']">
                      <div class="physicianCap">Retail pharmacy plan coverage name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sRetailPharmacyCoverageName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sRetailInsTypeCode[.!='']">
                      <div class="physicianCap">Retail pharmacy insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sRetailInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sRetailMonetaryAmount[.!='']">
                      <div class="physicianCap">Retail pharmacy monetary amount :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sRetailMonetaryAmount"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="RetailPharmEligDate[.!='']">
                      <div class="physicianCap">Retail pharmacy eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="RetailPharmEligDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="RetailPharmServiceDate[.!='']">
                      <div class="physicianCap">Retail pharmacy service date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="RetailPharmServiceDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sMailOrdEligiblityorBenefitInfo[.!='']">
                      <div class="physicianCap">Mail order pharmacy status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sMailOrdEligiblityorBenefitInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sMailOrderRxDrugCoverageName[.!='']">
                      <div class="physicianCap">Mail order pharmacy plan coverage name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sMailOrderRxDrugCoverageName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sMailOrderInsTypeCode[.!='']">
                      <div class="physicianCap">Mail order pharmacy insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sMailOrderInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sMailOrderMonetaryAmount[.!='']">
                      <div class="physicianCap">Mail order pharmacy monetary amount : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sMailOrderMonetaryAmount"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="MailOrdPharmEligDate[.!='']">
                      <div class="physicianCap">Mail order pharmacy eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="MailOrdPharmEligDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="MailOrdPharmServiceDate[.!='']">
                      <div class="physicianCap">Mail order pharmacy service date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="MailOrdPharmServiceDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="LTCPhEligiblityorBenefitInfo[.!='']">
                      <div class="physicianCap">LTC pharmacy status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="LTCPhEligiblityorBenefitInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="LTCPharmCovName[.!='']">
                      <div class="physicianCap">LTC pharmacy plan coverage name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="LTCPharmCovName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sLTCPharmacyInsTypeCode[.!='']">
                      <div class="physicianCap">LTC pharmacy insurance name : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sLTCPharmacyInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="LTCPharmEligDate[.!='']">
                      <div class="physicianCap">LTC pharmacy eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="LTCPharmEligDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="LTCPharmServiceDate[.!='']">
                      <div class="physicianCap">LTC pharmacy service date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="LTCPharmServiceDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="SpecialityPhEligiblityorBenefitInfo[.!='']">
                      <div class="physicianCap">Specialty pharmacy status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="SpecialityPhEligiblityorBenefitInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="SpecialtyPharmCovName[.!='']">
                      <div class="physicianCap">Specialty pharmacy plan coverage name : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="SpecialtyPharmCovName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sSpecialtyPharmacyInsTypeCode[.!='']">
                      <div class="physicianCap">Specialty pharmacy insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sSpecialtyPharmacyInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="SpecialtyPharmEligDate[.!='']">
                      <div class="physicianCap">Specialty pharmacy eligibility date :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="SpecialtyPharmEligDate"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="SpecialtyPharmServiceDate[.!='']">
                      <div class="physicianCap">Specialty pharmacy service date : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="SpecialtyPharmServiceDate"/>
                      </div>
                    </xsl:if>
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sIsPrimaryPayer[.!=''] or sPrimaryPayerName[.!=''] or sPrimaryPayerNumber[.!=''] or sPrimaryPayerRetailsEligible[.!=''] or sPrimaryPayerRetailCoverageInfo[.!=''] or sPrimaryPayerRetailInsTypeCode[.!=''] or sPrimaryPayerRetailMonetaryAmt[.!=''] or sPrimaryPayerMailOrderEligible[.!=''] or sPrimaryPayerMailOrderCoverageInfo[.!=''] or sPrimaryPayerMailOrderInsTypeCode[.!=''] or sPrimaryPayerMailOrderMonetaryAmt[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">Retail/Mail Pharmacy Payer Information</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sIsPrimaryPayer[.!='']">
                      <div class="physicianCap">Primary payer :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sIsPrimaryPayer"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerName[.!='']">
                      <div class="physicianCap">Payer name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerNumber[.!='']">
                      <div class="physicianCap">Payer No :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerNumber"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerRetailsEligible[.!='']">
                      <div class="physicianCap">Retail pharmacy primary payer eligible :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerRetailsEligible"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerRetailCoverageInfo[.!='']">
                      <div class="physicianCap">Retail Pharmacy primary payer coverage status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerRetailCoverageInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerRetailInsTypeCode[.!='']">
                      <div class="physicianCap">Retail Pharmacy primary payer insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerRetailInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerRetailMonetaryAmt[.!='']">
                      <div class="physicianCap">Retail Pharmacy primary payer monetary amount :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerRetailMonetaryAmt"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerMailOrderEligible[.!='']">
                      <div class="physicianCap">Mail order pharmacy primary payer eligible :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerMailOrderEligible"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerMailOrderCoverageInfo[.!='']">
                      <div class="physicianCap">Mail order Pharmacy primary payer coverage status : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerMailOrderCoverageInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerMailOrderInsTypeCode[.!='']">
                      <div class="physicianCap">Mail oreder Pharmacy primary payer insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerMailOrderInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sPrimaryPayerMailOrderMonetaryAmt[.!='']">
                      <div class="physicianCap">Mail order Pharmacy primary payer monetary amount :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sPrimaryPayerMailOrderMonetaryAmt"/>
                      </div>
                    </xsl:if>
                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="sIsContractedProvider[.!=''] or sContractedProviderName[.!=''] or sContractedProviderNumber[.!=''] or sContProvRetailsEligible[.!=''] or sContProvRetailCoverageInfo[.!=''] or sContProvRetailInsTypeCode[.!=''] or sContProvRetailMonetaryAmt[.!=''] or sContProvMailOrderEligible[.!=''] or sContProvMailOrderCoverageInfo[.!=''] or sContProvMailOrderInsTypeCode[.!=''] or sContProvMailOrderMonetaryAmt[.!='']">
            <div class="physician">
              <table cellpadding="0" cellspacing="0" class="gradient" width="100%">
                <tr>
                  <td class="header">Retail/Mail Pharmacy Contract Provider Information</td>
                </tr>
                <tr>
                  <td>
                    <xsl:if test="sIsContractedProvider[.!='']">
                      <div class="physicianCap">Contracted provider :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sIsContractedProvider"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContractedProviderName[.!='']">
                      <div class="physicianCap">Contract provider name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContractedProviderName"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContractedProviderNumber[.!='']">
                      <div class="physicianCap">Contract provider No : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContractedProviderNumber"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvRetailsEligible[.!='']">
                      <div class="physicianCap">Retail pharmacy contract provider eligible :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvRetailsEligible"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvRetailCoverageInfo[.!='']">
                      <div class="physicianCap">Retail Pharmacy contract provider coverage status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvRetailCoverageInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvRetailInsTypeCode[.!='']">
                      <div class="physicianCap">Retail Pharmacy contract provider insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvRetailInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvRetailMonetaryAmt[.!='']">
                      <div class="physicianCap">Retail Pharmacy contract provider monetary amount :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvRetailMonetaryAmt"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvMailOrderEligible[.!='']">
                      <div class="physicianCap">Mail order pharmacy contract provider eligible : </div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvMailOrderEligible"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvMailOrderCoverageInfo[.!='']">
                      <div class="physicianCap">Mail order Pharmacy contract provider coverage status :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvMailOrderCoverageInfo"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvMailOrderInsTypeCode[.!='']">
                      <div class="physicianCap">Mail order Pharmacy contract provider insurance name :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvMailOrderInsTypeCode"/>
                      </div>
                    </xsl:if>

                    <xsl:if test="sContProvMailOrderMonetaryAmt[.!='']">
                      <div class="physicianCap">Mail order Pharmacy contract provider monetary amount :</div>
                      <div class="physicianValue">
                        <xsl:value-of select="sContProvMailOrderMonetaryAmt"/>
                      </div>
                    </xsl:if>

                  </td>
                </tr>
              </table>
            </div>
          </xsl:if>

          <xsl:if test="IsSubscriberdemoChange[.='Yes']">

            <xsl:for-each select="/NewDataSet/Patient">
              <div class="physician">
                <table cellpadding="0" cellspacing="0" class="GrayGradient" width="100%">
                  <tr>
                    <td class="GrayHeader">Patient Demographics on Record</td>
                  </tr>
                  <tr>
                    <td>
                      <div class="GrayPhysicianCap">Patient name :</div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatName"/>
                      </div>

                      <div class="GrayPhysicianCap">Patient DOB :</div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatDOB"/>
                      </div>

                      <div class="GrayPhysicianCap">Patient gender :</div>
                      <div class="GrayPhysician">
                        <xsl:variable name="sex" select="PatGender"/>
                        <xsl:choose>
                          <xsl:when test="$sex='M'">Male</xsl:when>
                          <xsl:when test="$sex='F'">Female</xsl:when>
                          <xsl:when test="$sex='U'">Unknown</xsl:when>
                        </xsl:choose>
                      </div>

                      <xsl:variable name="PatAddressLine1" select="PatAddressLine1"/>
                      <div class="GrayPhysicianCap">Patient address line 1 :</div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatAddressLine1"/>&nbsp;
                      </div>

                      <div class="GrayPhysicianCap">Patient address line 2 : </div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatAddressLine2"/>&nbsp;
                      </div>

                      <div class="GrayPhysicianCap">Patient city :</div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatCity"/>&nbsp;
                      </div>

                      <div class="GrayPhysicianCap">Patient state : </div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatState"/>&nbsp;
                      </div>

                      <div class="GrayPhysicianCap">Patient Zip :</div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatZip"/>&nbsp;
                      </div>

                      <div class="GrayPhysicianCap">Patient SSN : </div>
                      <div class="GrayPhysician">
                        <xsl:value-of select="PatSSN"/>&nbsp;
                      </div>
                    </td>
                  </tr>
                </table>
              </div>
            </xsl:for-each>
          </xsl:if>
        </xsl:for-each>
      </body>
    </html>

  </xsl:template>
</xsl:stylesheet>