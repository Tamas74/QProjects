<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="*">
        <xsl:element name="{name()}" namespace="http://www.ncpdp.org/schema/SCRIPT">
            <xsl:apply-templates select="@*|node()"/>
        </xsl:element>
    </xsl:template>
    <xsl:template match="Message">
        <xsl:element name="{name()}" namespace="http://www.ncpdp.org/schema/SCRIPT">
            <xsl:attribute name="xsi:schemaLocation">http://www.ncpdp.org/schema/SCRIPT SS_SCRIPT_XML_10_6MU.xsd</xsl:attribute>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:element>
		
    </xsl:template>
	
</xsl:stylesheet>