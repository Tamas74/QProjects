using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace gloScanWIA
{
	/// <summary>
	/// Die Ausnahme, die ausgelöst wird, wenn ein WIA-Fehler aufgetreten ist
	/// </summary>
	[Serializable]
	public class WiaException : Exception
	{
		/// <summary>
		/// Enthält die Beschreibungen der WIA-Fehler
		/// </summary>
		private static Dictionary<WiaError,string> messageMappings = new Dictionary<WiaError,string>
		{
			{ WiaError.GeneralError,"Es ist ein nicht genau angegebener Fehler mit dem WIA-Gerät aufgetreten." },
			{ WiaError.PaperJam,"Das Papier im Papiereinzug des Scanners ist verklemmt." },
			{ WiaError.PaperEmpty,"Es liegt kein Papier im Papiereinzug." },
			{ WiaError.PaperProblem,"Es ist ein nicht genau angegebenes Problem mit dem Papiereinzug des Scanners aufgetreten." },
			{ WiaError.Offline,"Das WIA-Gerät ist nicht eingeschaltet." },
			{ WiaError.Busy,"Das Gerät ist noch beschäftigt." },
			{ WiaError.WarmingUp,"Das Gerät wärmt gerade auf." },
			{ WiaError.UserIntervention,"Es ist ein nicht genau angegebener Fehler aufgetreten, der einen Eingriff des Bedieners erfordert. Bitte prüfen Sie, ob dass das Gerät eingeschaltet und verbunden ist und alle Kabel ordnungsgemäß angeschlossen sind." },
			{ WiaError.ItemDeleted,"Das WIA-Gerät wurde gelöscht. Es kann nicht mehr darauf zugegriffen werden." },
			{ WiaError.DeviceCommunication,"Es ist ein nicht genau angegeben Fehler während eines Kommunikationsversuchs mit dem WIA-Gerät aufgetreten." },
			{ WiaError.InvalidCommand,"Das Gerät unterstützt diesen Befehl nicht." },
			{ WiaError.IncorrectHardwareSetting,"Es gibt eine fehlerhafte Einstellung am WIA-Gerät." },
			{ WiaError.DeviceLocked,"Der Scanner-Kopf ist festgesetzt." },
			{ WiaError.ExceptionInDriver,"Der Gerätetreiber hat eine Ausnahme ausgelöst." },
			{ WiaError.InvalidDriverResponse,"Die Antwort vom Treiber ist ungültig." },
			{ WiaError.CoverOpen,"Die Abdeckung des Scanners ist geöffnet." },
			{ WiaError.LampOff,"Die Beleuchtung des Scanners ist aus." },
			{ WiaError.NoDeviceAvailable,"Es ist kein WIA-Gerät des angegeben Typs verfügbar." },
		};


		/// <summary>
		/// Initialisiert eine neue Instanz der WiaException-Klasse mit einer angegebenen Fehlermeldung und einem Verweis auf die innere Ausnahme, die diese Ausnahme ausgelöst hat. 
		/// </summary>
		/// <param name="message">die Fehlermeldung</param>
		/// <param name="innerException">die innere Ausnahme, die diese Ausnahme ausgelöst hat</param>
		public WiaException(string message,Exception innerException) : base(message,innerException)
		{}


		/// <summary>
		/// Initialisiert eine neue Instanz der WiaException-Klasse mit COM-Ausnahme
		/// </summary>
		/// <param name="comException">die COM-Ausnahme</param>
		/// <returns>die aus der COM-Ausnahme initialisierte WIA-Ausnahme</returns>
		public static WiaException FromComException(COMException comException)
		{
			return new WiaException(WiaException.GetMessageFromComException(comException),comException);
		}

		
		/// <summary>
		/// Ermittelt für eine (WIA-)COM-Ausnahme die zugehörige Fehlermeldung
		/// </summary>
		/// <param name="ex">die (WIA-)COM-Ausnahme</param>
		/// <returns>die zur (WIA-)COM-Ausnahme gehörende Fehlermeldung</returns>
		public static string GetMessageFromComException(COMException ex)
		{
			WiaError wiaError = (WiaError)ex.ErrorCode;
			int errorCode;
			if (!Int32.TryParse(wiaError.ToString(),out errorCode))
			{				
				string message = wiaError.ToString();
				WiaException.messageMappings.TryGetValue(wiaError,out message);
				return message;
			}
			else
				return ex.Message;
		}

	}
}
