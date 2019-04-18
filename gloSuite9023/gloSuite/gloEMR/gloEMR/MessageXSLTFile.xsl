<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml"/>
  <xsl:template match="/">
    <xsl:element name="Message">
      <xsl:copy-of select="NewDataSet/Message/@version"/>
      <xsl:copy-of select="NewDataSet/Message/@release"/>
      <xsl:element name="Header">
        <xsl:copy-of select="NewDataSet/Message/Header/To"/>
        <xsl:copy-of select="NewDataSet/Message/Header/From"/>
        <xsl:copy-of select="NewDataSet/Message/Header/MessageID"/>
        <xsl:copy-of select="NewDataSet/Message/Header/RelatesToMessageID"/>
        <xsl:copy-of select="NewDataSet/Message/Header/SentTime"/>
        <xsl:copy-of select="NewDataSet/Message/Header/Security"/>
        <xsl:copy-of select="NewDataSet/Message/Header/SenderSoftware"/>
        <xsl:copy-of select="NewDataSet/Message/Header/Mailbox"/>
        <xsl:copy-of select="NewDataSet/Message/Header/TestMessage"/>
        <xsl:copy-of select="NewDataSet/Message/Header/RxReferenceNumber"/>
        <xsl:copy-of select="NewDataSet/Message/Header/TertiaryIdentifier"/>
        <xsl:copy-of select="NewDataSet/Message/Header/PrescriberOrderNumber"/>
        <xsl:copy-of select="NewDataSet/Message/Header/DigitalSignature"/>
      </xsl:element>
      <xsl:element name="Body">
        <xsl:element name="RefillResponse">
          <xsl:if test="NewDataSet/Message/Body/RefillResponse/Request[text() != '']">
            <xsl:element name="Request">
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Request/ReturnReceipt"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Request/RequestReferenceNumber"/>
            </xsl:element>
          </xsl:if>
        
            <xsl:choose>
              <xsl:when test= "NewDataSet/Message/Body/RefillResponse/Response/Denied[text() != '']" >
                <xsl:element name="Response">
                <xsl:element name="Denied">
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/DenialReasonCode"/>
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/ReferenceNumber"/>
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/Denied/DenialReason"/>
                </xsl:element>
                </xsl:element>
              </xsl:when>
              <xsl:when test= "NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow[text() != '']" >
                <xsl:element name="Response">
                <xsl:element name="DeniedNewPrescriptionToFollow">
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/DenialReasonCode"/>
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/ReferenceNumber"/>
                  <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response/DeniedNewPrescriptionToFollow/DenialReason"/>
                </xsl:element>
                </xsl:element>
              </xsl:when>
              <xsl:otherwise>
                <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Response"/>
              </xsl:otherwise>
            </xsl:choose>


        
          <xsl:element name="Pharmacy">
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Identification"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Specialty"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Pharmacist"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/StoreName"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/Address"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Pharmacy/CommunicationNumbers"/>
          </xsl:element>
          <xsl:element name="Prescriber">
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Identification"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Specialty"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/ClinicName"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Name"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/Address"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/PrescriberAgent"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Prescriber/CommunicationNumbers"/>
          </xsl:element>
          <xsl:if test="NewDataSet/Message/Body/RefillResponse/Supervisor[text() != '']">
            <xsl:element name="Supervisor">
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Identification"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Specialty"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Name"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/ClinicName"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/Address"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Supervisor/CommunicationNumbers"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="NewDataSet/Message/Body/RefillResponse/Facility[text() != '']">
            <xsl:element name="Facility">
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Facility/Identification"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Facility/FacilityName"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Facility/Address"/>
              <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Facility/CommunicationNumbers"/>
            </xsl:element>
          </xsl:if>

          <xsl:element name="Patient">
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/PatientRelationship"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/Identification"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/Name"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/Gender"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/DateOfBirth"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/Address"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/CommunicationNumbers"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Patient/PatientLocation"/>
          </xsl:element>
          <xsl:element name="MedicationPrescribed">
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugDescription"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoded"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Quantity"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DaysSupply"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Directions"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Note"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Refills"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Substitutions"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/WrittenDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/LastFillDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/ExpirationDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/EffectiveDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PeriodEnd"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DeliveredOnDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DateValidated"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/Diagnosis"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorization"/>
            <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugUseEvaluation">
              <xsl:element name="DrugUseEvaluation">
                <xsl:copy-of select="./ServiceReasonCode"/>
              <xsl:copy-of select="./ProfessionalServiceCode"/>
              <xsl:copy-of select="./ServiceResultCode"/>
              <xsl:copy-of select="./CoAgent"/>
              <xsl:copy-of select="./ClinicalSignificanceCode"/>
              <xsl:copy-of select="./AcknowledgementReason"/>
             
              </xsl:element>
            </xsl:for-each>
            <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/DrugCoverageStatusCode">
              <xsl:copy-of select="./DrugCoverageStatusCode" />
            </xsl:for-each>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/PriorAuthorizationStatus"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationPrescribed/StructuredSIG"/>
          </xsl:element>
          <xsl:element name="MedicationDispensed">
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugDescription"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoded"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Quantity"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DaysSupply"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Directions"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Note"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Refills"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Substitutions"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/WrittenDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/LastFillDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/ExpirationDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/EffectiveDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PeriodEnd"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DeliveredOnDate"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DateValidated"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/Diagnosis"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PriorAuthorization"/>
            <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugUseEvaluation">
              <xsl:element name ="DrugUseEvaluation">
              <xsl:copy-of select="./ServiceReasonCode"/>
              <xsl:copy-of select="./ProfessionalServiceCode"/>
              <xsl:copy-of select="./ServiceResultCode"/>
              <xsl:copy-of select="./CoAgent"/>
              <xsl:copy-of select="./ClinicalSignificanceCode"/>
              <xsl:copy-of select="./AcknowledgementReason"/>
              </xsl:element>
            </xsl:for-each>
            <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/DrugCoverageStatusCode">
              <xsl:copy-of select="./DrugCoverageStatusCode" />
            </xsl:for-each>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/PriorAuthorizationStatus"/>
            <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/MedicationDispensed/StructuredSIG"/>
          </xsl:element>
          <xsl:if test="NewDataSet/Message/Body/RefillResponse/Observation[text() != '']">
        <xsl:element name ="Observation">
          <xsl:for-each select="NewDataSet/Message/Body/RefillResponse/Observation/Measurement">
            <xsl:element name ="Measurement">
            <xsl:copy-of select="./Dimension"/>
            <xsl:copy-of select="./Value"/>
            <xsl:copy-of select="./ObservationDate"/>
            <xsl:copy-of select="./MeasurementDataQualifier"/>
            <xsl:copy-of select="./MeasurementSourceCode"/>
            <xsl:copy-of select="./MeasurementUnitCode"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:copy-of select="NewDataSet/Message/Body/RefillResponse/Observation/ObservationNotes"/>
        </xsl:element>
            </xsl:if >
         </xsl:element>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--<xsl:template name="formatDate">
    <xsl:param name="date"/>
    <xsl:value-of select="substring ($date, 1, 10)"/>
  </xsl:template>-->
</xsl:stylesheet>