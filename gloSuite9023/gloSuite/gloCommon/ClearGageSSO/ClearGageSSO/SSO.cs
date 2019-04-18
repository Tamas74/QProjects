using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClearGage
{
	namespace SSO
	{
		namespace Response
		{
			public class Transaction
			{
				public string PatientId { get; set; }
				public string PatientName { get; set; }
				public string PlanId { get; set; }
				public string TransactionId { get; set; }
				public string OriginalTransactionId { get; set; }
				public double Amount { get; set; }
				public string PayMethod { get; set; }
				public string Action { get; set; }
				public DateTime Timestamp { get; set; }
				public string TrackingId { get; set; }
				public string PaymentProfileId { get; set; }
				public string AccountType { get; set; }
				public string AccountNumber { get; set; }
				public string Reference { get; set; }
				public string InternalAction { get; set; }
			}

			public class PaymentPlan
			{
				public string PatientId { get; set; }
				public string PlanId { get; set; }
				public double Amount { get; set; }
				public double Balance { get; set; }
				public string Status { get; set; }
				public DateTime? EffectiveDate { get; set; }
			}

			public class Subscription
			{
				public string PatientId { get; set; }
				public string SubscriptionId { get; set; }
				public double Amount { get; set; }
				public string Status { get; set; }
				public DateTime? EffectiveDate { get; set; }
			}

			public class Encounter
			{
				public string EncounterId { get; set; }
				public string ClaimNumber { get; set; }
				public DateTime? ServiceDate { get; set; }
				public string Description { get; set; }
				public double Amount { get; set; }
			}

			public class Procedure
			{
				public string Code { get; set; }
				public string Description { get; set; }
				public double Amount { get; set; }
				public int Quantity { get; set; }
				public double TotalAmount { get; set; }
			}

			public class PaymentPlanDetails
			{
				public PaymentPlan PaymentPlan { get; set; }
				public Encounter[] Encounters { get; set; }
				public Procedure[] Procedures { get; set; }
				public Transaction[] Transactions { get; set; }
			}

			public class SubscriptionDetails
			{
				public Subscription Subscription { get; set; }
				public Transaction[] Transactions { get; set; }
			}

			public class Payment
			{
				public string Results { get; set; }
				public string Message { get; set; }
				public Transaction[] Transactions { get; set; }
			}

			public class PaymentProfile
			{
				public string PaymentProfileId { get; set; }
				public string Type { get; set; }
				public string AccountType { get; set; }
				public string AccountNumber { get; set; }
				public bool Expiring30 { get; set; }

				public const string PAYMENT_PROFILE_TYPE_CARD = "CARD";
				public const string PAYMENT_PROFILE_TYPE_ACH = "ACH";

				public const string CARD_ACCOUNT_TYPE_VISA = "VI";
				public const string CARD_ACCOUNT_TYPE_MASTERCARD = "MC";
				public const string CARD_ACCOUNT_TYPE_DISCOVER = "DI";
				public const string CARD_ACCOUNT_TYPE_AMEX = "AX";

				public const string ACH_ACCOUNT_TYPE_CHECKING = "C";
				public const string ACH_ACCOUNT_TYPE_SAVINGS = "S";

				public string GetAccountTypeLabel()
				{
					return SsoHelper.GetAccountTypeLabel(AccountType);
				}
			}

			public class Patient
			{
				public string PatientId { get; set; }
				public string LastName { get; set; }
				public string FirstName { get; set; }
				public string Gender { get; set; }
				public string BirthDate { get; set; }
				public string Address1 { get; set; }
				public string Address2 { get; set; }
				public string City { get; set; }
				public string State { get; set; }
				public string Zip { get; set; }
				public string Phone { get; set; }
				public string MobilePhone { get; set; }
				public string EmailAddress { get; set; }
			}
			
			public class Guarantor : Patient
			{
			}

			public class Subscriber : Patient
			{
			}

			public class PatientConsent
			{
				public DateTime? RequestSentDate { get; set; }
				public DateTime? ResponseReceivedDate { get; set; }
				public bool Esign { get; set; }
				public bool Sms { get; set; }
				public bool Email { get; set; }
				public string MobilePhone { get; set; }
				public string EmailAddress { get; set; }
			}

			public class AutoPayAgreement
			{
				public string AutoPayAgreementId { get; set; }
				public string PaymentProfileId { get; set; }
				public string AltPaymentProfileId { get; set; }
				public double PerEventAmountMax { get; set; }
				public int EventCountMax { get; set; }
				public int AgreementDurationMax { get; set; }
				public string AgreementStatus { get; set; }
				public DateTime? UnsuspendDate { get; set; }
				public DateTime? EffectiveDate { get; set; }
			}
			public class ConnectionStatus
			{
				public bool Connected { get; set; }
			}

			public class ErrorMessage
			{
				private string message;

				public string Error { get; set; }
				public string Message
				{
					get
					{
						return message != null ? message : Error;
					}
					set
					{
						message = value;
					}
				}
				public string ExtendedInfo { get; set; }

				public Exception GetException()
				{
					Exception exception = null;

					if (Error.StartsWith("SSO.INVALID"))
					{
						exception = new ArgumentException(Message);
					}
					else
					{
						exception = new SystemException(Message);
					}

					exception.Data["error"] = Error;
					exception.Data["message"] = Message;
					exception.Data["extendedInfo"] = ExtendedInfo;

					return exception;
				}
			}
		}

		public delegate void OneTimePaymentDialogResponseHandler(Response.Transaction[] transactions);
		public delegate void RecentTransactionsDialogResponseHandler(Response.Transaction[] transactions);
		public delegate void AddPaymentPlanDialogResponseHandler(Response.PaymentPlanDetails planDetails);
		public delegate void EditPaymentPlanDialogResponseHandler(Response.PaymentPlanDetails planDetails);
		public delegate void AddSubscriptionDialogResponseHandler(Response.SubscriptionDetails planDetails);
		public delegate void EditSubscriptionDialogResponseHandler(Response.SubscriptionDetails planDetails);
		public delegate void DialogErrorResponseHandler(Response.ErrorMessage errorMessage);
		public delegate void AccountOnFileResponseHandler(Response.Transaction[] transactions);

		public class PatientAbstract
		{
			public string PatientId { get; set; }
			public string LastName { get; set; }
			public string FirstName { get; set; }
			public string Gender { get; set; }
			public string BirthDate { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string State { get; set; }
			public string Zip { get; set; }
			public string Phone { get; set; }
			public string MobilePhone { get; set; }
			public string EmailAddress { get; set; }
		}

		public class Patient : PatientAbstract
		{
			public string Ssn { get; set; }
			public string DriversLicenseNumber { get; set; }
			public string DriversLicenseState { get; set; }
		}

		public class Guarantor : PatientAbstract
		{
		}

		public class Subscriber : PatientAbstract
		{
		}

		public enum PatientRelationship
		{
			Subscriber,
			Guarantor
		}


		[System.Runtime.InteropServices.ComVisible(true)]
		public sealed class SsoHelper : SsoHelperInterface
		{
			private string clientId;
			private string username;
			private string password;
			private string key;
			private string name;
			private string host;

			const string SDK_VERSION = "N" + "0.8.2";

			const int DEFAULT_DIALOG_WIDTH = 984;
			const int DEFAULT_DIALOG_HEIGHT = 600;
			const int DEFAULT_REQUEST_TIMEOUT = 60 * 1000;

			public int DefaultDialogWidth { get; set; }
			public int DefaultDialogHeight { get; set; }

			public OneTimePaymentDialogResponseHandler OneTimePaymentDialogCallback { get; set; }
			public RecentTransactionsDialogResponseHandler RecentTransactionsDialogCallback { get; set; }
			public AddPaymentPlanDialogResponseHandler AddPaymentPlanDialogCallback { get; set; }
			public EditPaymentPlanDialogResponseHandler EditPaymentPlanDialogCallback { get; set; }
			public AddSubscriptionDialogResponseHandler AddSubscriptionDialogCallback { get; set; }
			public EditSubscriptionDialogResponseHandler EditSubscriptionDialogCallback { get; set; }
			public DialogErrorResponseHandler DialogErrorCallback { get; set; }
			

			public int RequestTimeout { get; set; }

			public enum Mode
			{
				MODE_LIVE,
				MODE_DEMO,
				MODE_DEMO2
			}

			private Mode mode = Mode.MODE_LIVE;

			private enum RequestType
			{
				REQUEST_TYPE_UI,
				REQUEST_TYPE_WEBSERVICE
			}

			public class PostData
			{
				private List<KeyValuePair<string, string>> data;

				public PostData()
				{
					this.data = new List<KeyValuePair<string, string>>();
				}

				public void Add(string key, string value)
				{
					this.data.Add(new KeyValuePair<string, string>(key, value));
				}

				public string GetDataString()
				{
					this.data.Sort((a, b) => a.Key.CompareTo(b.Key));

					StringBuilder sb = new StringBuilder();
					foreach (var nameValue in this.data)
					{
						if (sb.Length > 0)
						{
							sb.Append('&');
						}
						sb.Append(nameValue.Key);
						sb.Append('=');
						sb.Append(nameValue.Value);
					}

					return sb.ToString();
				}

				public string GetContent()
				{
					this.data.Sort((a, b) => a.Key.CompareTo(b.Key));

					StringBuilder sb = new StringBuilder();
					foreach (var nameValue in this.data)
					{
						if (sb.Length > 0)
						{
							sb.Append('&');
						}
						sb.Append(nameValue.Key);
						sb.Append('=');
						sb.Append(SsoHelper.UrlEncode(nameValue.Value.Trim()));
					}

					return sb.ToString();
				}
			}

			public SsoHelper(
				string clientId,
				string username,
				string password,
				string key,
				string name
			)
			{
				this.clientId = clientId;
				this.username = username;
				this.password = password;
				this.key = key;
				this.name = name;
				

				this.DefaultDialogWidth = DEFAULT_DIALOG_WIDTH;
				this.DefaultDialogHeight = DEFAULT_DIALOG_HEIGHT;
				this.RequestTimeout = DEFAULT_REQUEST_TIMEOUT;
			}

			public void SetMode(Mode mode)
			{
				this.mode = mode;
			}

			public Mode GetMode()
			{
				return this.mode;
			}

			public void SetHost(string host)
			{
				this.host = host.Trim();
			}

			public Boolean VerifyConnection()
			{
				string url = GetVerifyConnectionUrl();

				string response = GetSsoResponse(url);

				Response.ConnectionStatus connectionStatus = (Response.ConnectionStatus)DeserializeResponse(response, typeof(Response.ConnectionStatus));

				return connectionStatus.Connected;
			}

			public string GetVerifyConnectionUrl()
			{
				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "verify");
			}
			
			public string GetOneTimePaymentDialogHtml(
				Patient patient,
				double amount,
				string trackingId = "",
				string planId = "",
				int width = -1,
				int height = -1
			)
			{
				string url = GetOneTimePaymentDialogRequestUrl(patient, amount, trackingId, planId);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetOneTimePaymentDialogRequestUrl(
				Patient patient,
				double amount,
				string trackingId = "",
				string planId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("trackingId", trackingId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "pay", options, patient);
			}

			public string GetRecentTransactionsDialogHtml(
				string patientId = "",
				string planId = "",
				string transactionId = "",
				int width = -1,
				int height = -1
			)
			{
				string url = GetRecentTransactionsDialogRequestUrl(patientId, planId, transactionId);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetRecentTransactionsDialogRequestUrl(
				string patientId = "",
				string planId = "",
				string transactionId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);
				options.Add("planId", planId);
				options.Add("transactionId", transactionId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "transactions", options);
			}

			public string GetAddPaymentPlanDialogHtml(
				Patient patient,
				double amount,
				string encounterId = "",
				string claimNumber = "",
				DateTime? serviceDate = null,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			)
			{
				string url = GetAddPaymentPlanDialogRequestUrl(patient, amount, encounterId, claimNumber, serviceDate, guarantor);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetAddPaymentPlanDialogRequestUrl(
				Patient patient,
				double amount,
				string encounterId = "",
				string claimNumber = "",
				DateTime? serviceDate = null,
				Guarantor guarantor = null
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				if (!String.IsNullOrWhiteSpace(encounterId))
				{
					options.Add("encounterId", encounterId);
				}
				if (!String.IsNullOrWhiteSpace(claimNumber))
				{
					options.Add("claimNumber", claimNumber);
				}
				if (serviceDate != null)
				{
					options.Add("serviceDate", serviceDate);
				}
				if (guarantor != null)
				{
					AddGuarantorUrlOptions(ref options, guarantor);
				}

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "addPlan", options, patient);
			}


			public string GetEditPaymentPlanDialogHtml(
				string planId,
				double adjustment = 0.00,
				int width = -1,
				int height = -1
			)
			{
				string url = GetEditPaymentPlanDialogRequestUrl(planId, adjustment);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetEditPaymentPlanDialogRequestUrl(
				string planId,
				double adjustment = 0.00
			)
			{
				Hashtable options = new Hashtable();
				options.Add("planId", planId);
				options.Add("adjustment", adjustment);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "editPlan", options);
			}

			public string GetAddSubscriptionDialogHtml(
				Patient patient,
				double amount,
				string paymentFrequency = "",
				string paymentDate = "",
				string paymentStartDate = "",
				string paymentEndDate = "",
				string planDescription = "",
				int width = -1,
				int height = -1
			)
			{
				string url = GetAddSubscriptionDialogRequestUrl(
					patient,
					amount,
					paymentFrequency,
					paymentDate,
					paymentStartDate,
					paymentEndDate,
					planDescription
				);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetAddSubscriptionDialogRequestUrl(
				Patient patient,
				double amount,
				string paymentFrequency = "",
				string paymentDate = "",
				string paymentStartDate = "",
				string paymentEndDate = "",
				string planDescription = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("paymentFrequency", paymentFrequency);
				options.Add("paymentDate", paymentDate);
				options.Add("paymentStartDate", paymentStartDate);
				options.Add("paymentEndDate", paymentEndDate);
				options.Add("planDescription", planDescription);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "addSubscription", options, patient);
			}

			public string GetEditSubscriptionDialogHtml(
				string subscriptionId,
				int width = -1,
				int height = -1
			)
			{
				string url = GetEditSubscriptionDialogRequestUrl(subscriptionId);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetEditSubscriptionDialogRequestUrl(
				string subscriptionId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("subscriptionId", subscriptionId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "editSubscription", options);
			}

			public Response.AutoPayAgreement GetAutoPayAgreement(
				Patient oPatient
			)
			{
				Response.AutoPayAgreement agreement = null;
				try
				{
					string url = GetAutoPayAgreementRequestUrl(oPatient);

					string response = GetSsoResponse(url);

					agreement = (Response.AutoPayAgreement)DeserializeResponse(response, typeof(Response.AutoPayAgreement));
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return agreement;
			}

			public string GetAutoPayAgreementRequestUrl(
				Patient oPatient
			)
			{
				//Hashtable options = new Hashtable();
				//options.Add("patientId", patientId);
				//gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageURL, gloAuditTrail.ActivityType.General, "In Get AutoPay Agreement Request Url()", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getAutoPayAgreement",null, oPatient);
			}
			public Response.PatientConsent GetPatientConsent(
				string patientId = ""
			)
			{
				string url = GetPatientConsentRequestUrl(patientId);

				string response = GetSsoResponse(url);

				Response.PatientConsent consent = (Response.PatientConsent)DeserializeResponse(response, typeof(Response.PatientConsent));

				return consent;
			}

			public string GetPatientConsentRequestUrl(
				string patientId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPatientConsent", options);
			}

			public string GetAccountOnFileDialogHtml(
				Patient patient,
				int width = -1,
				int height = -1
			)
			{
				string url = GetAccountOnFileDialogRequestUrl(patient);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetAccountOnFileDialogRequestUrl(
				Patient patient
			)
			{
				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "autoPay", null, patient);
			}

			public Response.Transaction[] GetAccountOnFile(
				string patientId
			)
			{
				string url = GetPatientAccountOnFileRequestUrl(patientId);

				string response = GetSsoResponse(url);

				Response.Transaction[] procedures = (Response.Transaction[])DeserializeResponse(response, typeof(Response.Transaction[]));

				return procedures;
			}

			public string GetPatientAccountOnFileRequestUrl(
				string planId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getSubscriptions", options);
			}

			public string GetPatientDashboardDialogHtml(
				string patientId,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			)
			{
				string url = GetPatientDashboardDialogRequestUrl(patientId, guarantor);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetPatientDashboardDialogRequestUrl(
				string patientId,
				Guarantor guarantor = null
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);
				if (guarantor != null)
				{
					AddGuarantorUrlOptions(ref options, guarantor);
				}

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "dashboard", options);
			}

			public string GetPatientDashboardDialogHtml(
				Patient patient,
				Guarantor guarantor = null,
				int width = -1,
				int height = -1
			)
			{
				string url = GetPatientDashboardDialogRequestUrl(patient, guarantor);
				string html = GetParentFrameHtml(url, width, height);

				return html;
			}

			public string GetPatientDashboardDialogRequestUrl(
				Patient patient,
				Guarantor guarantor = null
			)
			{
				Hashtable options = new Hashtable();
				if (guarantor != null)
				{
					AddGuarantorUrlOptions(ref options, guarantor);
				}

				return BuildRequestUrl(RequestType.REQUEST_TYPE_UI, "dashboard", options, patient);
			}

			public Response.Transaction[] GetPaymentTransactions(
				DateTime startTime = default(DateTime),
				DateTime endTime = default(DateTime),
				string patientId = "",
				string planId = "",
				string trackingId = ""
			)
			{
				string url = GetPaymentTransactionsRequestUrl(startTime, endTime, patientId, planId, trackingId);

				string response = GetSsoResponse(url);

				Response.Transaction[] transactions = (Response.Transaction[])DeserializeResponse(response, typeof(Response.Transaction[]));

				return transactions;
			}

			public string GetPaymentTransactionsRequestUrl(
				DateTime startTime = default(DateTime),
				DateTime endTime = default(DateTime),
				string patientId = "",
				string planId = "",
				string trackingId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("startTime", startTime);
				options.Add("endTime", endTime);
				options.Add("patientId", patientId);
				options.Add("planId", planId);
				options.Add("trackingId", trackingId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getTransactions", options);
			}

			public Response.PaymentPlan[] GetPaymentPlans(
				string patientId = "",
				string planId = ""
			)
			{
				string url = GetPaymentPlansRequestUrl(patientId, planId);

				string response = GetSsoResponse(url);

				Response.PaymentPlan[] plans = (Response.PaymentPlan[])DeserializeResponse(response, typeof(Response.PaymentPlan[]));

				return plans;
			}

			public string GetPaymentPlansRequestUrl(
				string patientId = "",
				string planId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPlans", options);
			}

			public Response.Encounter[] GetPaymentPlanEncounters(
				string planId
			)
			{
				string url = GetPaymentPlanEncountersRequestUrl(planId);

				string response = GetSsoResponse(url);

				Response.Encounter[] encounters = (Response.Encounter[])DeserializeResponse(response, typeof(Response.Encounter[]));

				return encounters;
			}

			public string GetPaymentPlanEncountersRequestUrl(
				string planId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPlanEncounters", options);
			}

			public Response.Procedure[] GetPaymentPlanProcedures(
				string planId
			)
			{
				string url = GetPaymentPlanProceduresRequestUrl(planId);

				string response = GetSsoResponse(url);

				Response.Procedure[] procedures = (Response.Procedure[])DeserializeResponse(response, typeof(Response.Procedure[]));

				return procedures;
			}

			public string GetPaymentPlanProceduresRequestUrl(
				string planId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPlanProcedures", options);
			}

			public Response.Subscription[] GetSubscriptions(
				string patientId = "",
				string subscriptionId = ""
			)
			{
				string url = GetSubscriptionsRequestUrl(patientId, subscriptionId);

				string response = GetSsoResponse(url);

				Response.Subscription[] subscriptions = (Response.Subscription[])DeserializeResponse(response, typeof(Response.Subscription[]));

				return subscriptions;
			}

			public string GetSubscriptionsRequestUrl(
				string patientId = "",
				string subscriptionId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);
				options.Add("subscriptionId", subscriptionId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getSubscriptions", options);
			}

			public Response.Payment MakeSwipedCardPayment(
				Patient patient,
				double amount,
				string trackData,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "CARD");
				postData.Add("trackData", trackData);
				postData.Add("saveProfile", saveProfile ? "1" : "0");

				string url = GetMakeSwipedCardPaymentRequestUrl(patient, amount, postData, trackingId, planId);

				string response = PostSsoData(url, postData);

				Response.Payment payment = (Response.Payment)DeserializeResponse(response, typeof(Response.Payment));

				return payment;
			}

			public string GetMakeSwipedCardPaymentRequestUrl(
				Patient patient,
				double amount,
				PostData postData,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("trackingId", trackingId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "makePayment", options, patient, postData);
			}

			public Response.Payment MakeKeyedCardPayment(
				Patient patient,
				double amount,
				string cardNumber,
				string cardExp,
				string cardCvv,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "CARD");
				postData.Add("cardNumber", cardNumber);
				postData.Add("cardExp", cardExp);
				postData.Add("cardCvv", cardCvv);
				postData.Add("saveProfile", saveProfile ? "1" : "0");

				string url = GetMakeKeyedCardPaymentRequestUrl(patient, amount, postData, trackingId, planId);

				string response = PostSsoData(url, postData);

				Response.Payment payment = (Response.Payment)DeserializeResponse(response, typeof(Response.Payment));

				return payment;
			}

			public string GetMakeKeyedCardPaymentRequestUrl(
				Patient patient,
				double amount,
				PostData postData,
				string trackingId = "",
				string planId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("trackingId", trackingId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "makePayment", options, patient, postData);
			}

			public Response.Payment MakeAchPayment(
				Patient patient,
				double amount,
				string routingNumber,
				string accountNumber,
				string accountType,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "ACH");
				postData.Add("routingNumber", routingNumber);
				postData.Add("accountNumber", accountNumber);
				postData.Add("accountType", accountType);
				postData.Add("saveProfile", saveProfile ? "1" : "0");

				string url = GetMakeAchPaymentRequestUrl(patient, amount, postData, trackingId, planId);

				string response = PostSsoData(url, postData);

				Response.Payment payment = (Response.Payment)DeserializeResponse(response, typeof(Response.Payment));

				return payment;
			}

			public string GetMakeAchPaymentRequestUrl(
				Patient patient,
				double amount,
				PostData postData,
				string trackingId = "",
				string planId = "",
				bool saveProfile = false
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("trackingId", trackingId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "makePayment", options, patient, postData);
			}

			public Response.Payment MakePayment(
				Patient patient,
				double amount,
				string paymentProfileId,
				string trackingId = "",
				string planId = ""
			)
			{
				PostData postData = new PostData();
				postData.Add("paymentProfileId", paymentProfileId);

				string url = GetMakeTokenPaymentRequestUrl(patient, amount, postData, trackingId, planId);

				string response = PostSsoData(url, postData);

				Response.Payment payment = (Response.Payment)DeserializeResponse(response, typeof(Response.Payment));

				return payment;
			}

			public string GetMakeTokenPaymentRequestUrl(
				Patient patient,
				double amount,
				PostData postData,
				string trackingId = "",
				string planId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("amount", amount);
				options.Add("trackingId", trackingId);
				options.Add("planId", planId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "makePayment", options, patient, postData);
			}

			public Response.Payment RefundPayment(
				string transactionId
			)
			{
				PostData postData = new PostData();
				string url = GetRefundPaymentRequestUrl(transactionId);

				string response = GetSsoResponse(url);

				Response.Payment payment = (Response.Payment)DeserializeResponse(response, typeof(Response.Payment));

				return payment;
			}

			public string GetRefundPaymentRequestUrl(
				string transactionId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("transactionId", transactionId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "refundPayment", options);
			}

			public Response.PaymentProfile SaveSwipedCardPaymentProfile(
				Patient patient,
				string trackData,
				string paymentProfileId = ""
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "CARD");
				postData.Add("trackData", trackData);

				string url = GetSaveSwipedCardPaymentProfileRequestUrl(patient, postData, paymentProfileId);

				string response = PostSsoData(url, postData);

				Response.PaymentProfile paymentProfile = (Response.PaymentProfile)DeserializeResponse(response, typeof(Response.PaymentProfile));

				return paymentProfile;
			}

			public string GetSaveSwipedCardPaymentProfileRequestUrl(
				Patient patient,
				PostData postData,
				string paymentProfileId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("paymentProfileId", paymentProfileId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "savePaymentProfile", options, patient, postData);
			}

			public Response.PaymentProfile SaveKeyedCardPaymentProfile(
				Patient patient,
				string cardNumber,
				string cardExp,
				string cardCvv,
				string paymentProfileId = ""
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "CARD");
				postData.Add("cardNumber", cardNumber);
				postData.Add("cardExp", cardExp);
				postData.Add("cardCvv", cardCvv);

				string url = GetSaveKeyedCardPaymentProfileRequestUrl(patient, postData, paymentProfileId);

				string response = PostSsoData(url, postData);

				Response.PaymentProfile paymentProfile = (Response.PaymentProfile)DeserializeResponse(response, typeof(Response.PaymentProfile));

				return paymentProfile;
			}

			public string GetSaveKeyedCardPaymentProfileRequestUrl(
				Patient patient,
				PostData postData,
				string paymentProfileId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("paymentProfileId", paymentProfileId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "savePaymentProfile", options, patient, postData);
			}

			public Response.PaymentProfile SaveAchPaymentProfile(
				Patient patient,
				string routingNumber,
				string accountNumber,
				string accountType,
				string paymentProfileId = ""
			)
			{
				PostData postData = new PostData();
				postData.Add("payMethod", "ACH");
				postData.Add("routingNumber", routingNumber);
				postData.Add("accountNumber", accountNumber);
				postData.Add("accountType", accountType);

				string url = GetSaveAchPaymentProfileRequestUrl(patient, postData, paymentProfileId);

				string response = PostSsoData(url, postData);

				Response.PaymentProfile paymentProfile = (Response.PaymentProfile)DeserializeResponse(response, typeof(Response.PaymentProfile));

				return paymentProfile;
			}

			public string GetSaveAchPaymentProfileRequestUrl(
				Patient patient,
				PostData postData,
				string paymentProfileId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("paymentProfileId", paymentProfileId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "savePaymentProfile", options, patient, postData);
			}

			public Response.PaymentProfile DeletePaymentProfile(
				string paymentProfileId
			)
			{
				string url = GetDeletePaymentProfileRequestUrl(paymentProfileId);

				string response = GetSsoResponse(url);

				Response.PaymentProfile paymentProfile = (Response.PaymentProfile)DeserializeResponse(response, typeof(Response.PaymentProfile));

				return paymentProfile;
			}

			public string GetDeletePaymentProfileRequestUrl(
				string paymentProfileId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("paymentProfileId", paymentProfileId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "deletePaymentProfile", options);
			}

			public Response.PaymentProfile[] GetPaymentProfiles(
				string patientId,
				string paymentProfileId = ""
			)
			{
				string url = GetPaymentProfilesRequestUrl(patientId, paymentProfileId);

				string response = GetSsoResponse(url);

				Response.PaymentProfile[] paymentProfiles = (Response.PaymentProfile[])DeserializeResponse(response, typeof(Response.PaymentProfile[]));

				return paymentProfiles;
			}

			public string GetPaymentProfilesRequestUrl(
				string patientId = "",
				string paymentProfileId = ""
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);
				options.Add("paymentProfileId", paymentProfileId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPaymentProfiles", options);
			}

			public static string GetAccountTypeLabel(string type)
			{
				switch (type)
				{
					case "VI": return "Visa";
					case "MC": return "MasterCard";
					case "DI": return "Discover";
					case "AX": return "American Express";
					case "C": return "Checking";
					case "S": return "Savings";
					case "M": return "Cash";
					default: return type;
				}
			}

			public Response.Patient SavePatient(
				   Patient patient
			)
			{
				string url = GetSavePatientRequestUrl(patient);

				string response = GetSsoResponse(url);

				return (Response.Patient)DeserializeResponse(response, typeof(Response.Patient));
			}

			public string GetSavePatientRequestUrl(
				Patient patient
			)
			{
				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "savePatient", null, patient);
			}

			public Response.Patient GetPatient(
				   string patientId
			)
			{
				string url = GetPatientRequestUrl(patientId);

				string response = GetSsoResponse(url);

				Response.Patient patient = (Response.Patient)DeserializeResponse(response, typeof(Response.Patient));

				return patient;
			}

			public string GetPatientRequestUrl(
				   string patientId
			)
			{
				Hashtable options = new Hashtable();
				options.Add("patientId", patientId);

				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "getPatient", options);
			}

			public Boolean SavePatientRelationship(
				string patientId,
				string relatedPatientId,
				PatientRelationship relationship
			)
			{
				PostData postData = new PostData();
				postData.Add("patientId", patientId);
				postData.Add("relatedPatientId", relatedPatientId);
				postData.Add("relationshipType", GetRelationshipType(relationship));

				string url = GetSavePatientRelationshipRequestUrl(postData);

				PostSsoData(url, postData);

				return true;
			}

			public string GetSavePatientRelationshipRequestUrl(
				PostData postData
			)
			{
				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "savePatientRelationship", null, null, postData);
			}

			public Boolean DeletePatientRelationship(
				string patientId,
				string relatedPatientId,
				PatientRelationship relationship
			)
			{
				PostData postData = new PostData();
				postData.Add("patientId", patientId);
				postData.Add("relatedPatientId", relatedPatientId);
				postData.Add("relationshipType", GetRelationshipType(relationship));

				string url = GetDeletePatientRelationshipRequestUrl(postData);

				PostSsoData(url, postData);

				return true;
			}

			public string GetDeletePatientRelationshipRequestUrl(
				PostData postData
			)
			{
				return BuildRequestUrl(RequestType.REQUEST_TYPE_WEBSERVICE, "deletePatientRelationship", null, null, postData);
			}

			public static string GetRelationshipType(PatientRelationship relationship)
			{
				switch (relationship)
				{
					case PatientRelationship.Subscriber: return "SUBS";
					case PatientRelationship.Guarantor: return "GRNT";
					default: return null;
				}
			}

			public string GetSsoResponse(string url)
			{
				string results = "";

				System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
				request.Method = "GET";
				try
				{
					ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
					using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
					{
						using (System.IO.Stream responseStream = response.GetResponseStream())
						{
							using (System.IO.StreamReader responseStreamData = new System.IO.StreamReader(responseStream))
							{
								results = responseStreamData.ReadToEnd();
								results = results.TrimEnd('"').TrimStart('"').Replace(@"\", "");
								responseStreamData.Close();
							}
							responseStream.Close();
						}
						response.Close();
					}
				}
				catch
				{
				}

				return results;
			}

			public string PostSsoData(string url, PostData postData)
			{
				string results = "";

				System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				using (System.IO.StreamWriter requestStream = new System.IO.StreamWriter(request.GetRequestStream()))
				{
					requestStream.Write(postData.GetContent());
				}

				try
				{
					using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
					{
						using (System.IO.Stream responseStream = response.GetResponseStream())
						{
							using (System.IO.StreamReader responseStreamData = new System.IO.StreamReader(responseStream))
							{
								results = responseStreamData.ReadToEnd();
								results = results.TrimEnd('"').TrimStart('"').Replace(@"\", "");
								responseStreamData.Close();
							}
							responseStream.Close();
						}
						response.Close();
					}
				}
				catch
				{
				}

				return results;
			}

			private Object DeserializeResponse(string response, Type type)
			{
				Response.ErrorMessage errorMessage = null;

				Object obj = null;

				try
				{
					JContainer results = null;

					if (type.IsArray)
					{
						try
						{
							results = JArray.Parse(response);
						}
						catch (Newtonsoft.Json.JsonReaderException)
						{
							results = JObject.Parse(response);

							if (results["error"] != null)
							{
								errorMessage = results.ToObject<Response.ErrorMessage>();
							}
							else
							{
								throw new Exception();
							}
						}
					}
					else
					{
						results = JObject.Parse(response);

						if (results["error"] != null)
						{
							errorMessage = results.ToObject<Response.ErrorMessage>();
						}
					}

					if (errorMessage == null)
					{
						obj = results.ToObject(type);
					}
				}
				catch (Exception)
				{
					errorMessage = new Response.ErrorMessage();
					errorMessage.Error = "SSO.UNKNOWN";
					errorMessage.Message = response;
				}

				if (errorMessage != null)
				{
					HandleError(errorMessage);
				}

				return obj;
			}

			private Object DeserializeResponseResults(string response, Type type)
			{
				Response.ErrorMessage errorMessage = null;

				Object obj = null;

				try
				{
					JObject results = JObject.Parse(response);

					if (results["RESPONSE"] != null)
					{
						obj = results["RESPONSE"].ToObject(type);
					}
					else if (results["error"] != null)
					{
						errorMessage = results.ToObject<Response.ErrorMessage>();
					}
					else
					{
						throw new Exception();
					}
				}
				catch (Exception)
				{
					errorMessage = new Response.ErrorMessage();
					errorMessage.Error = "SSO.UNKNOWN";
					errorMessage.Message = response;
				}

				if (errorMessage != null)
				{
					HandleError(errorMessage);
				}

				return obj;
			}

			private string BuildRequestUrl(
				RequestType type,
				string method,
				Hashtable options = null,
				Patient patient = null,
				PostData postData = null
			)
			{
				String url = GetHostUrl();

				if (type == RequestType.REQUEST_TYPE_UI)
				{
					url += "sso.cfm";
				}
				else
				{
					url += "webservices/sso.cfc";
				}

				String arguments = "method=" + method + BuildPatientUrlArguments(patient) + BuildOptionsUrlArguments(options) + BuildAuthenticationUrlArguments() + BuildConfigUrlArguments();

				url += "?" + arguments;

				if (postData != null)
				{
					arguments += "&" + postData.GetDataString();
				}

				url += "&token=" + GetToken(arguments);
				string sPatientID = string.Empty;
				if (patient != null)
				{
					sPatientID = Convert.ToString(patient.PatientId);
				}
				else if (options != null)
				{
					sPatientID = Convert.ToString(options["patientId"]);
				}
				string sAuditMessage = string.Format("Cleargage {0} URL generated for patient {3} with Method:{1}  URL: {2}",type==RequestType.REQUEST_TYPE_UI?"HTML":"Web Service",method,url,sPatientID);
				if (clsCleargage.IsFromPortal==false)
				{
					gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageURL, gloAuditTrail.ActivityType.Generate, sAuditMessage, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
					
				}
				return url;
			}

			private string BuildPatientUrlArguments(Patient patient)
			{
				string arguments = "";

				if (patient != null)
				{
					if (!String.IsNullOrWhiteSpace(patient.PatientId))
					{
						arguments += "&patientId=" + UrlEncode(patient.PatientId.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.LastName))
					{
						arguments += "&lastName=" + UrlEncode(patient.LastName.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.FirstName))
					{
						arguments += "&firstName=" + UrlEncode(patient.FirstName.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Gender))
					{
						arguments += "&gender=" + UrlEncode(patient.Gender.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.BirthDate))
					{
						arguments += "&birthdate=" + UrlEncode(patient.BirthDate.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Address1))
					{
						arguments += "&address1=" + UrlEncode(patient.Address1.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Address2))
					{
						arguments += "&address2=" + UrlEncode(patient.Address2.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.City))
					{
						arguments += "&city=" + UrlEncode(patient.City.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.State))
					{
						arguments += "&state=" + UrlEncode(patient.State.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Zip))
					{
						arguments += "&zip=" + UrlEncode(patient.Zip.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Phone))
					{
						arguments += "&phone=" + UrlEncode(patient.Phone.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.MobilePhone))
					{
						arguments += "&mobilePhone=" + UrlEncode(patient.MobilePhone.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.EmailAddress))
					{
						arguments += "&emailAddress=" + UrlEncode(patient.EmailAddress.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.Ssn))
					{
						arguments += "&ssn=" + UrlEncode(patient.Ssn.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.DriversLicenseNumber))
					{
						arguments += "&driversLicenseNumber=" + UrlEncode(patient.DriversLicenseNumber.Trim());
					}
					if (!String.IsNullOrWhiteSpace(patient.DriversLicenseState))
					{
						arguments += "&driversLicenseState=" + UrlEncode(patient.DriversLicenseState.Trim());
					}
				}

				return arguments;
			}

			private string BuildOptionsUrlArguments(Hashtable options)
			{
				string arguments = "";

				if (options != null)
				{
					foreach (DictionaryEntry entry in options)
					{
						string value;

						if (entry.Value.GetType().Equals(typeof(System.DateTime)))
						{
							if (entry.Value.Equals(default(DateTime)))
							{
								continue;
							}

							value = ((DateTime)entry.Value).ToString("M/d/yyyy HH:mm:ss");
						}
						else
						{
							value = entry.Value.ToString();
						}

						if (String.IsNullOrWhiteSpace(value))
						{
							continue;
						}

						arguments += "&" + entry.Key + "=" + UrlEncode(value);
					}
				}

				return arguments;
			}

			private string BuildAuthenticationUrlArguments()
			{
				return "&clientId=" + UrlEncode(this.clientId) + "&username=" + UrlEncode(this.username) + "&password=" + UrlEncode(this.password) + "&name=" + UrlEncode(this.name);
			}

			private string BuildConfigUrlArguments()
			{
				TimeSpan timeZoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

				return "&condensed=false&localTime=" + DateTime.Now.Ticks + "&timeZoneOffset=" + -timeZoneOffset.TotalHours + "&v=" + SsoHelper.SDK_VERSION;
			}

			private string GetHostUrl()
			{
				String url = this.host;

				if (String.IsNullOrWhiteSpace(url))
				{
					url = "https://";

					if (this.mode == Mode.MODE_DEMO)
					{
						url += "demo";
					}
					else if (this.mode == Mode.MODE_DEMO2)
					{
						url += "demo2";
					}
					else
					{
						url += "www";
					}

					url += ".acceleratepayments.com/";
				}

				if (!url.EndsWith("/"))
				{
					url += "/";
				}

				return url;
			}

			private string GetToken(string arguments)
			{
				HMACMD5 hmac = new HMACMD5(Encoding.UTF8.GetBytes(this.key));

				byte[] data = hmac.ComputeHash(Encoding.UTF8.GetBytes(arguments));

				StringBuilder token = new StringBuilder();

				for (int index = 0; index < data.Length; ++index)
				{
					token.Append(data[index].ToString("x2"));
				}

				return token.ToString();
			}

			public static string UrlEncode(string value)
			{
				return Uri.EscapeDataString(value).Replace("%20", "+");
			}

			public string SerializeJson(object value, Boolean formatted = false)
			{
				if (formatted)
				{
					return JsonConvert.SerializeObject(value, Formatting.Indented);
				}
				else
				{
					return JsonConvert.SerializeObject(value);
				}
			}

			public string GetParentFrameHtml(string url, int width = -1, int height = -1)
			{
				if (width == -1)
				{
					width = this.DefaultDialogWidth;
				}
				if (height == -1)
				{
					height = this.DefaultDialogHeight;
				}

				string host = GetHostUrl();
				string html = @"
<html>
<head>
	<meta http-equiv=""X-UA-Compatible"" content=""IE=9""/>
	<script src=""{{host}}media/js/json2.min.js"" type=""text/javascript""></script>
	<script src=""{{host}}media/js/porthole.min.js"" type=""text/javascript""></script>
	<script type=""text/javascript"">
		function onSsoMessage(messageEvent) {
			if (messageEvent.origin == '{{origin}}') {
				var response = messageEvent.data;
				if (typeof response !== 'string') {
					response = JSON.stringify(response);
				}
				window.external.HandleSsoDialogResponse(response);
			}
		}
		var windowProxy;
		window.onload = function() {
			windowProxy = new Porthole.WindowProxy('', 'ssoFrame');
			windowProxy.addEventListener(onSsoMessage);
		};
	</script>
</head>
<body><iframe id=""ssoFrame"" name=""ssoFrame"" src=""{{url}}"" style=""border:0"" width=""{{width}}"" height=""{{height}}""></iframe></body>
</html>";

				return html.Trim().Replace("{{host}}", host).Replace("{{origin}}", host.TrimEnd('/')).Replace("{{url}}", url).Replace("{{width}}", System.Convert.ToString(width)).Replace("{{height}}", System.Convert.ToString(height));
			}

			public void HandleSsoDialogResponse(string response)
			{
				try
				{
					JObject results = JObject.Parse(response);

					string method = (string)results["METHOD"];

					if (method != null)
					{
						if (method == "PAY")
						{
							if (this.OneTimePaymentDialogCallback != null)
							{
								this.OneTimePaymentDialogCallback(results["RESPONSE"].ToObject<Response.Transaction[]>());
							}
						}
						else if (method == "TRANSACTIONS")
						{
							if (this.RecentTransactionsDialogCallback != null)
							{
								this.RecentTransactionsDialogCallback(results["RESPONSE"].ToObject<Response.Transaction[]>());
							}
						}
						else if (method == "ADDPLAN")
						{
							if (this.AddPaymentPlanDialogCallback != null)
							{
								FixPaymentPlansJsonResponse(results);

								this.AddPaymentPlanDialogCallback(results["RESPONSE"].ToObject<Response.PaymentPlanDetails>());
							}
						}
						else if (method == "EDITPLAN")
						{
							if (this.EditPaymentPlanDialogCallback != null)
							{
								FixPaymentPlansJsonResponse(results);

								this.EditPaymentPlanDialogCallback(results["RESPONSE"].ToObject<Response.PaymentPlanDetails>());
							}
						}
						else if (method == "ADDSUBSCRIPTION")
						{
							if (this.AddSubscriptionDialogCallback != null)
							{
								FixSubscriptionsJsonResponse(results);

								this.AddSubscriptionDialogCallback(results["RESPONSE"].ToObject<Response.SubscriptionDetails>());
							}
						}
						else if (method == "EDITSUBSCRIPTION")
						{
							if (this.EditSubscriptionDialogCallback != null)
							{
								FixSubscriptionsJsonResponse(results);

								this.EditSubscriptionDialogCallback(results["RESPONSE"].ToObject<Response.SubscriptionDetails>());
							}
						}
					}
					else if (results["error"] != null)
					{
						if (this.DialogErrorCallback != null)
						{
							this.DialogErrorCallback(results.ToObject<Response.ErrorMessage>());
						}
					}
					else
					{
						throw new Exception();
					}
				}
				catch (Exception)
				{
					Response.ErrorMessage errorMessage = new Response.ErrorMessage();
					errorMessage.Error = "SSO.UNKNOWN";
					errorMessage.Message = response;

					this.DialogErrorCallback(errorMessage);
				}
			}

			private void HandleError(Response.ErrorMessage errorMessage)
			{
				throw errorMessage.GetException();
			}

			private void FixPaymentPlansJsonResponse(JToken results)
			{
				JToken plans = results["RESPONSE"].SelectToken("PLAN");
				results["RESPONSE"].Last.AddAfterSelf(new JProperty("PAYMENTPLAN", plans.First));
				plans.Parent.Remove();
			}

			private void FixSubscriptionsJsonResponse(JToken results)
			{
				JToken subscriptions = results["RESPONSE"].SelectToken("SUBSCRIPTION");
				subscriptions.Replace(subscriptions.First);
			}

			private void AddGuarantorUrlOptions(ref Hashtable options, Guarantor guarantor)
			{
				options.Add("grnt_id", guarantor.PatientId);
				if (!String.IsNullOrWhiteSpace(guarantor.LastName))
				{
					options.Add("grnt_lastName", guarantor.LastName);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.FirstName))
				{
					options.Add("grnt_firstName", guarantor.FirstName);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.Gender))
				{
					options.Add("grnt_gender", guarantor.Gender);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.BirthDate))
				{
					options.Add("grnt_birthDate", guarantor.BirthDate);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.Address1))
				{
					options.Add("grnt_address1", guarantor.Address1);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.Address2))
				{
					options.Add("grnt_address2", guarantor.Address2);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.City))
				{
					options.Add("grnt_city", guarantor.City);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.State))
				{
					options.Add("grnt_state", guarantor.State);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.Zip))
				{
					options.Add("grnt_zip", guarantor.Zip);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.Phone))
				{
					options.Add("grnt_phone", guarantor.Phone);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.MobilePhone))
				{
					options.Add("grnt_mobilePhone", guarantor.MobilePhone);
				}
				if (!String.IsNullOrWhiteSpace(guarantor.EmailAddress))
				{
					options.Add("grnt_emailAddress", guarantor.EmailAddress);
				}
			}
		}
	}
}