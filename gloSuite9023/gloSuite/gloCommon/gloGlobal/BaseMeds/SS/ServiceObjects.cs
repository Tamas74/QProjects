using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloGlobal.Common;
using schema = gloGlobal.Schemas.Surescript;


namespace gloGlobal.SS
{
    public class RxChangeRequest
    {
        //private bool bIsChangeDrugAdded;

        //public bool IsChangeDrugAdded
        //{
        //    get { return bIsChangeDrugAdded; }
        //    set { bIsChangeDrugAdded = value; }
        //}
        

        private string nMessageID;

        private Int64 nPatientID;
        private Int64 nPharmacyID;
        private Int64 nTransactionID;

        private schema.MessageType sfileData;
        public schema.MessageType FileData
        {
            get { return sfileData; }
            set { sfileData = value; }
        }

        private string sfileDataXML;
        public string FileDataXML
        {
            get { return sfileDataXML; }
            set { sfileDataXML = value; }
        }

        public string MessageID
        {
            get { return nMessageID; }
            set { nMessageID = value; }
        }

        public Int64 PatientID
        {
            get { return nPatientID; }
            set { nPatientID = value; }
        }

        public Int64 TransactionID
        {
            get { return nTransactionID; }
            set { nTransactionID = value; }
        }

        public Int64 PharmacyID
        {
            get { return nPharmacyID; }
            set { nPharmacyID = value; }
        }

        private string sTransactionRefNumber;

        public string TransactionRefNumber
        {
            get { return sTransactionRefNumber; }
            set { sTransactionRefNumber = value; }
        }
        

        public ChangeRequestType Type { get; set; }

        public schema.RxChangeDispensedMedicationType MedRequested { get; set; }

        public schema.RxChangePrescribedMedicationType MedPrescribed { get; set; }

        public schema.BenefitsCoordinationType BenifitsOfCord { get; set; }

        public RxChangeRequest(string MessageId, Int64 TransactionID, Int64 PatientID, Int64 PharmacyID, string TransactionReferenceNumber, string RequestType, schema.RxChangePrescribedMedicationType MedicationPrescribed, schema.RxChangeDispensedMedicationType MedicationRequested, schema.BenefitsCoordinationType BenifitsOfCord, string FileDataXML)
        {
            this.nMessageID = MessageId;

            this.MedRequested = MedicationRequested;
            this.MedPrescribed = MedicationPrescribed;
            this.BenifitsOfCord = BenifitsOfCord;
            this.TransactionRefNumber = TransactionRefNumber;
            this.sfileDataXML = FileDataXML;
           
            this.nTransactionID = TransactionID;
            this.nPatientID = PatientID;
            this.nPharmacyID = PharmacyID;

            if (RequestType.Equals("G"))
            { this.Type = ChangeRequestType.GenericSubstitution; }
            else if (RequestType.Equals("T"))
            { this.Type = ChangeRequestType.TherapeuticSubstitution; }
            else if (RequestType.Equals("P"))
            { this.Type = ChangeRequestType.PriorAuthorizationRequired; }

        }
    }
}
