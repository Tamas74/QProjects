using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearGage
{
	namespace SSO
	{
		interface SsoHelperInterface
		{
			Boolean VerifyConnection();

			/********************************
			 *                              *
			 *  Hosted UI Methods           *
			 *                              *
			 *******************************/

			string GetOneTimePaymentDialogHtml(
				Patient patient,
				double amount,
				string trackingId = "",
				string planId = "",
				int width = -1,
				int height = -1
			);

			string GetRecentTransactionsDialogHtml(
				string patientId = "",
				string planId = "",
				string transactionId = "",
				int width = -1,
				int height = -1
			);

			string GetAddPaymentPlanDialogHtml(
				Patient patient,
				double amount,
				string encounterId = "",
				string claimNumber = "",
				DateTime? serviceDate = null,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			);

			string GetEditPaymentPlanDialogHtml(
				string planId,
				double adjustment = 0.00,
				int width = -1,
				int height = -1
			);

			string GetAddSubscriptionDialogHtml(
				Patient patient,
				double amount,
				string paymentFrequency = "",
				string paymentDate = "",
				string paymentStartDate = "",
				string paymentEndDate = "",
				string planDescription = "",
				int width = -1,
				int height = -1
			);

			string GetEditSubscriptionDialogHtml(
				string subscriptionId,
				int width = -1,
				int height = -1
			);

			string GetPatientDashboardDialogHtml(
				string patientId,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			);

			string GetPatientDashboardDialogHtml(
				Patient patient,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			);

			/********************************
			 *                              *
			 *  Web Service Methods         *
			 *                              *
			 *******************************/

			Response.Encounter[] GetPaymentPlanEncounters(
				string planId
			);

			Response.Procedure[] GetPaymentPlanProcedures(
				string planId
			);

			Response.Subscription[] GetSubscriptions(
				string patientId = "",
				string subscriptionId = ""
			);

			Response.Payment MakeSwipedCardPayment(
				Patient patient,
				double amount,
				string trackData,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			);

			Response.Payment MakeKeyedCardPayment(
				Patient patient,
				double amount,
				string cardNumber,
				string cardExp,
				string cardCvv,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			);

			Response.Payment MakeAchPayment(
				Patient patient,
				double amount,
				string routingNumber,
				string accountNumber,
				string accountType,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			);

			Response.Payment MakePayment(
				Patient patient,
				double amount,
				string paymentProfileId,
				string trackingId = "",
				string planId = ""
			);

			Response.Payment RefundPayment(
				string transactionId
			);

			Response.PaymentProfile SaveSwipedCardPaymentProfile(
				Patient patient,
				string trackData,
				string paymentProfileId = ""
			);

			Response.PaymentProfile SaveKeyedCardPaymentProfile(
				Patient patient,
				string cardNumber,
				string cardExp,
				string cardCvv,
				string paymentProfileId = ""
			);

			Response.PaymentProfile SaveAchPaymentProfile(
				Patient patient,
				string routingNumber,
				string accountNumber,
				string accountType,
				string paymentProfileId = ""
			);

			Response.PaymentProfile DeletePaymentProfile(
				string paymentProfileId
			);

			Response.PaymentProfile[] GetPaymentProfiles(
				string patientId,
				string paymentProfileId = ""
			);

			Response.Patient SavePatient(
				   Patient patient
			);

			Response.Patient GetPatient(
				   string patientId
			);

			Boolean SavePatientRelationship(
				string patientId,
				string relatedPatientId,
				PatientRelationship relationship
			);

			Boolean DeletePatientRelationship(
				string patientId,
				string relatedPatientId,
				PatientRelationship relationship
			);
		}
	}
}
