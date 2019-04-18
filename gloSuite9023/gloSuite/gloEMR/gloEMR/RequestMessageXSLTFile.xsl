
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml"/>
    <xsl:template match="/">
     <xsl:element name="Message">
		<xsl:copy-of select="Message/@version"/>
      <xsl:copy-of select="Message/@release"/>
       <xsl:copy-of select="Message/Header"/>
	         
      <xsl:element name="Body">
        <xsl:element name="RefillRequest">
		  <xsl:copy-of select="Message/Body/RefillRequest/Request"/>
          <xsl:copy-of select="Message/Body/RefillRequest/Pharmacy"/>
          <xsl:copy-of select="Message/Body/RefillRequest/Prescriber"/>
          <xsl:copy-of select="Message/Body/RefillRequest/Supervisor"/>
          <xsl:copy-of select="Message/Body/RefillRequest/Facility"/>
          <xsl:copy-of select="Message/Body/RefillRequest/Patient"/>
                     	 
   
          <xsl:element name="MedicationPrescribed">
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DrugDescription"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DrugCoded"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Quantity"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DaysSupply"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Directions"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Note"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Refills"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Substitutions"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/WrittenDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/LastFillDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/ExpirationDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/EffectiveDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/PeriodEnd"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DeliveredOnDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DateValidated"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/Diagnosis"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/PriorAuthorization"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/DrugUseEvaluation"/>
            <xsl:for-each select="Message/Body/RefillRequest/MedicationPrescribed/DrugCoverageStatusCode">
              <xsl:element name="DrugCoverageStatusCode">
               
                  <xsl:copy-of select="." />
               
              </xsl:element>
            </xsl:for-each>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/PriorAuthorizationStatus"/>
             <xsl:copy-of select="Message/Body/RefillRequest/MedicationPrescribed/StructuredSIG"/>
		      </xsl:element>
          <xsl:element name="MedicationDispensed">
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DrugDescription"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DrugCoded"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Quantity"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DaysSupply"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Directions"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Note"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Refills"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Substitutions"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/WrittenDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/LastFillDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/ExpirationDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/EffectiveDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/PeriodEnd"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DeliveredOnDate"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DateValidated"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/Diagnosis"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/PriorAuthorization"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/DrugUseEvaluation"/>
            <xsl:for-each select="Message/Body/RefillRequest/MedicationDispensed/DrugCoverageStatusCode">
             
                <xsl:element name="DrugCoverageStatusCode">
                  <xsl:copy-of select="." />
               
              </xsl:element>
            </xsl:for-each>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/PriorAuthorizationStatus"/>
            <xsl:copy-of select="Message/Body/RefillRequest/MedicationDispensed/StructuredSIG"/>
          </xsl:element>
          <xsl:copy-of select="Message/Body/RefillRequest/Observation"/>
         </xsl:element>
     </xsl:element>
    </xsl:element>
  </xsl:template>
  <xsl:template name="formatDate">
		<xsl:param name="date"/>
		<xsl:value-of select="substring ($date, 1, 10)"/>
	</xsl:template>
	</xsl:stylesheet>