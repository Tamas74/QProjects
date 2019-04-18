using System;

namespace gloScanWIA
{
	/// <summary>
	/// Gibt die ID der WIA-Eigenschaft an
	/// </summary>
	[AttributeUsage(AttributeTargets.All,AllowMultiple=true)]
	public class WiaPropertyIdAttribute : Attribute
	{
		/// <summary>
		/// Gibt die WIA-Eigenschaften-ID zurück.
		/// </summary>
		public int PropertyID { get; private set; }


		/// <summary>
		/// Erstellt ein neues WiaProperty-Attribut
		/// </summary>
		/// <param name="propertyID">die WIA-Eigenschaften-ID</param>
		public WiaPropertyIdAttribute(int propertyID)
		{
			this.PropertyID = propertyID;
		}
	}
}
