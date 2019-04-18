using System;
using System.Collections.Generic;
using System.Diagnostics;
using WIA;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Methoden bereit, um WIA-Eigenschaften zu Lesen und zu Schreiben
	/// </summary>
	[DebuggerStepThrough]
	public abstract class WiaSettings
	{
		private Dictionary<int,IProperty> properties = new Dictionary<int,IProperty>();	// die IDs und Property-Objekte der Eigenschaften, die ausgelesen und gesetzt werden können


		/// <summary>
		/// Erstellt eine neue Instanz zum Auslesen und Setzen von Eigenschaften
		/// </summary>
		/// <param name="properties">die Eigenschaften, die Ausgelesen und gesetzt werden können</param>
		public WiaSettings(IProperties properties)
		{
			foreach (IProperty property in properties)
				this.properties.Add(property.PropertyID,property);
		}


		/// <summary>
		/// Liest die WIA-Eigenschaft am angegeben Index des angegeben Objekts aus
		/// </summary>
		/// <param name="index">der Index der Eigenschaft, die ausgelesen werden soll</param>
		/// <param name="value">das Objekt, dessen Eigenschaft ausgelesen werden soll</param>
		protected void SetPropertyValue(int index,object value)
		{
			IProperty property;
			this.properties.TryGetValue(index,out property);
			if (property==null)
				throw new NotSupportedException("Property wird nicht unterstützt.");
			else
				property.set_Value(ref value);
		}


		/// <summary>
		/// Liest die WIA-Eigenschaft am angegeben Index des angegeben Objekts aus
		/// </summary>
		/// <typeparam name="T">der Typ der Eigenschaft</typeparam>
		/// <param name="index">der Index der Eigenschaft, die ausgelesen werden soll</param>
		/// <returns>den Wert der ausgelesenene Eigenschaft</returns>
		protected T GetPropertyValue<T>(int index)
		{
			IProperty property;
			this.properties.TryGetValue(index,out property);
			if (property==null)
				throw new NotSupportedException("Property wird nicht unterstützt.");
			else
				return (T)property.get_Value();
		}
	}
}
