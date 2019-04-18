using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace gloGlobal.Common
{
    [DataContract]
    public class ServiceObjectBase
    {
        #region Address
        [DataContract]
        public class Address : IDisposable
        {
            [DataMember]
            public string add1 { get; set; }

            [DataMember]
            public string add2 { get; set; }

            [DataMember]
            public string city { get; set; }

            [DataMember]
            public string state { get; set; }

            [DataMember]
            public string zip { get; set; }

            [DataMember]
            public string country { get; set; }

            public void Dispose()
            {                
                this.add1 = null;
                this.add2 = null;
                this.city = null;
                this.state = null;
                this.zip = null;
                this.country = null;
            }
        } 
        #endregion

        #region Communication
        [DataContract]
        public class Communication : IDisposable
        {
            [DataMember]
            public string phone { get; set; }

            [DataMember]
            public string fax { get; set; }

            [DataMember]
            public string email { get; set; }

            public void Dispose()
            {             
                this.phone = null;
                this.fax = null;
                this.email = null;
            }
        } 
        #endregion

        #region "PBM Info"

        [DataContract]
        public class BenifitCoordination : IDisposable
        {
            public BenifitCoordination()
            {
                payer = new Payer();
                cardHolder = new CardHolder();
                group = new Group();
            }

            [DataMember]
            public string relatestomessageID { get; set; }

            [DataMember]
            public string memberID { get; set; }

            [DataMember]
            public Payer payer { get; set; }

            [DataMember]
            public CardHolder cardHolder { get; set; }

            [DataMember]
            public Group group { get; set; }

            public void Dispose()
            {
                this.relatestomessageID = null;
                this.memberID = null;

                if (this.payer != null) 
                {
                    this.payer.Dispose();
                    this.payer = null;
                }

                if (this.cardHolder != null)
                {
                    this.cardHolder.Dispose();
                    this.cardHolder = null;
                }

                if (this.group != null)
                {
                    this.group.Dispose();
                    this.group = null;
                }
            }

            public static BenifitCoordination GetBenefitsCoordination
            (
                string PayerID,
                string ProcessorIdentificationNumber,
                string BINLocationNumber,
                string MutuallyDefined,
                string PayerName,
                string CardHolderID,
                string FirstName,
                string LastName,
                string MiddleName,
                string GroupID,
                string GroupName,
                string PBMMemberID,
                string RelatesToMessageID
            )
            {
                BenifitCoordination returned = new BenifitCoordination();

                Payer payer = returned.payer;

                payer.id = PayerID;
                payer.pin = ProcessorIdentificationNumber;
                payer.bin = BINLocationNumber;
                payer.mutuallyDefined = MutuallyDefined;
                payer.name = PayerName;
                payer = null;

                Group grp = returned.group;
                grp.id = GroupID;
                grp.name = GroupName;
                grp = null;

                returned.memberID = PBMMemberID;
                returned.relatestomessageID = RelatesToMessageID;
                CardHolder c = returned.cardHolder;
                c.fname = FirstName;
                c.mname = MiddleName;
                c.lname = LastName;
                c.id = CardHolderID;
                c = null;

                return returned;
            }
        }

        [DataContract]
        public class Payer : IDisposable
        {
            [DataMember]
            public string id { get; set; }

            [DataMember]
            public string name { get; set; }

            [DataMember]
            public string pin { get; set; }

            [DataMember]
            public string bin { get; set; }

            [DataMember]
            public string mutuallyDefined { get; set; }

            public void Dispose()
            {               
                this.id = null;
                this.name = null;
                this.pin = null;
                this.bin = null;
                this.mutuallyDefined = null;
            }
        }

        [DataContract]
        public class CardHolder : IDisposable
        {
            [DataMember]
            public string id { get; set; }

            [DataMember]
            public string lname { get; set; }

            [DataMember]
            public string fname { get; set; }

            [DataMember]
            public string mname { get; set; }


            public void Dispose()
            {             
                this.id = null;
                this.lname = null;
                this.fname = null;
                this.mname = null;
            }
        }

        [DataContract]
        public class Group : IDisposable
        {
            [DataMember]
            public string id { get; set; }

            [DataMember]
            public string name { get; set; }

            public void Dispose()
            {                
                this.id = null;
                this.name = null;                                
            }
        }

        #endregion

        #region "Patient Info"

        [DataContract]
        public class Patient : IDisposable
        {
            [DataMember]
            public string mrnNumber { get; set; }

            [DataMember]
            public string ssn { get; set; }

            [DataMember]
            public string lname { get; set; }

            [DataMember]
            public string fname { get; set; }

            [DataMember]
            public string mname { get; set; }

            [DataMember]
            public string gender { get; set; }

            [DataMember]
            public string dob { get; set; }

            [DataMember]
            public Address address { get; set; }

            [DataMember]
            public Communication communication { get; set; }

            public Patient() 
            {
                this.address = new Address();
                this.communication = new Communication();
            }

            public void Dispose()
            {              
                this.mrnNumber = null;
                this.ssn = null;
                this.lname = null;
                this.fname = null;
                this.mname = null;
                this.gender = null;
                this.dob = null;

                if (this.address != null) { this.address.Dispose(); this.address = null; }
                if (this.communication != null) { this.communication.Dispose(); this.communication = null; }
            }
        }

        #endregion

        #region Pharmacy Info

        [DataContract]
        public class Pharmacy : IDisposable
        {
            [DataMember]
            public string npi { get; set; }

            [DataMember]
            public string ncpdp { get; set; }

            [DataMember]
            public string name { get; set; }

            [DataMember]
            public Address address { get; set; }

            [DataMember]
            public Communication communication { get; set; }

            public Pharmacy()
            {
                this.address = new Address();
                this.communication = new Communication();
            }

            public void Dispose()
            {
                this.npi = null;
                this.ncpdp = null;
                this.name = null;

                if (this.address != null) { this.address.Dispose(); this.address = null; }
                if (this.communication != null) { this.communication.Dispose(); this.communication = null; }
            }
        }

        #endregion

        #region Prescriber Info

        [DataContract]
        public class Prescriber : IDisposable
        {
            [DataMember]
            public string npi { get; set; }

            [DataMember]
            public string ncpdp { get; set; }

            [DataMember]
            public string lname { get; set; }

            [DataMember]
            public string fname { get; set; }

            [DataMember]
            public string mname { get; set; }

            [DataMember]
            public string suffix { get; set; }

            [DataMember]
            public Address address { get; set; }

            [DataMember]
            public Communication communication { get; set; }

            public void AddTelephone(string Phone)
            {
                if (this.communication == null)
                { this.communication = new Communication(); }

                this.communication.phone = Phone;
            }

            public void Dispose()
            {              
                this.npi = null;
                this.ncpdp = null;
                this.lname = null;
                this.fname = null;
                this.mname = null;
                this.suffix = null;

                if (this.address != null)
                {
                    this.address.Dispose();
                    this.address = null;
                }

                if (this.communication != null)
                {
                    this.communication.Dispose();
                    this.communication = null;
                }
            }

            public Prescriber()
            {
                this.address = new Address();
                this.communication = new Communication();
            }

            public static Prescriber GetPrescriber(
                string FirstName,
                string MiddleName,
                string LastName,
                string NPI,
                string NCPDPID,
                string AddressLine1,
                string AddressLine2,
                string City,
                string CountryCode,
                string StateProvince,
                string PostalCode,
                string ProviderFax,
                string ProviderPhone
                )
            {
                Prescriber prescriber = new Prescriber();


                gloGlobal.EPA.PAInitRequest.Address a = prescriber.address;

                a.add1 = AddressLine1;
                a.city = City;
                a.country = CountryCode;
                a.state = StateProvince;
                a.zip = PostalCode;
                a = null;

                gloGlobal.EPA.PAInitRequest.Communication c = prescriber.communication;
                c.fax = ProviderFax;
                c.phone = ProviderPhone;
                c = null;

                prescriber.fname = FirstName;
                prescriber.lname = LastName;
                prescriber.npi = NPI;
                prescriber.ncpdp = NCPDPID;

                return prescriber;
            }

           
        }

        #endregion

        #region Medication prescribed

        [DataContract]
        public class MedicationBase : IDisposable
        {
            [DataMember]
            public string medication { get; set; }

            [DataMember]
            public string mpid { get; set; }

            [DataMember]
            public string ndc { get; set; }

            [DataMember]
            public string qty { get; set; }

            [DataMember]
            public string days { get; set; }

            public void Dispose()
            {               
                this.medication = null;
                this.mpid = null;
                this.ndc = null;
                this.qty = null;
                this.days = null;
            }
        }

        [DataContract]
        public class Medication : MedicationBase
        {
            [DataMember]
            public string drugCode { get; set; }

            [DataMember]
            public string drugQual { get; set; }

            [DataMember]
            public string qtyUnit { get; set; }

            [DataMember]
            public string direction { get; set; }

            [DataMember]
            public Strength strength { get; set; }

            [DataMember]
            public Diagnosis diagnosis { get; set; }

            [DataMember]
            public string sigText { get; set; }

            public void AddDiagnosis(string CodeField, string DescriptionField)
            {
                if (this.diagnosis == null) { this.diagnosis = new Diagnosis(); }

                this.diagnosis.Code = CodeField;
                this.diagnosis.Description = DescriptionField;
                this.diagnosis.Qualifier = "DX";
                this.diagnosis.ClinicalInformationQualifier = "1";
            }

            public void Dispose()
            {               
                this.drugCode = null;
                this.drugQual = null;
                this.qtyUnit = null;
                this.direction = null;

                if (this.strength != null)
                {
                    this.strength.Dispose();
                    this.strength = null;
                }

                if (this.diagnosis != null)
                {
                    this.diagnosis.Dispose();
                    this.diagnosis = null;
                }
            }
        }

        [DataContract]
        public class MedPrescribed : Medication
        {
            [DataMember]
            public string refill { get; set; }

            [DataMember]
            public string dea { get; set; }

            [DataMember]
            public bool substitute { get; set; }

            [DataMember]
            public DateTime written { get; set; }

            [DataMember]
            public string note { get; set; }

            /// <summary>
            /// PriorAuthorizationNumber
            /// </summary>
            [DataMember]
            public string pan { get; set; }

            /// <summary>
            /// PriorAuthorizationStatus
            /// </summary>
            [DataMember]
            public string pas { get; set; }

            [DataMember]
            public string DxQual1 { get; set; }

            [DataMember]
            public string DxVal1 { get; set; }

            [DataMember]
            public string DxQual2 { get; set; }

            [DataMember]
            public string DxVal2 { get; set; }
        }

        [DataContract]
        public class Strength : IDisposable
        {
            public Strength()
            { }

            [DataMember]
            public string value { get; set; }

            [DataMember]
            public string form { get; set; }

            [DataMember]
            public string unit { get; set; }

            public void Dispose()
            {            
                this.value = null;
                this.form = null;
                this.unit = null;
            }
        }

        [DataContract]
        public class Diagnosis : IDisposable
        {
            public Diagnosis() { }

            [DataMember]
            public string ClinicalInformationQualifier { get; set; }

            [DataMember]
            public string Code { get; set; }

            [DataMember]
            public string Qualifier { get; set; }

            [DataMember]
            public string Description { get; set; }

            public void Dispose()
            {
                this.ClinicalInformationQualifier = null;
                this.Code = null;
                this.Qualifier = null;
                this.Description = null;
            }
        }
        #endregion
    }
}

