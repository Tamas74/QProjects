using System;

namespace gloScanWIA
{
	/// <summary>
	/// Suppress a property page
	/// </summary>
	[Flags]
	[WiaPropertyId(4124)]
	public enum SuppressPropertyPage /*WIA_IPA_SUPPRESS_PROPERTY_PAGE*/
	{
		/// <summary>Suppress the general item property page for a scanner.</summary>
		ScannerItemGeneral=0x00000001,
		/// <summary>Suppress the general item property page for a camera.</summary>
		CameraItemGeneral=0x00000002,
		/// <summary>Suppress the general item property page for a device.</summary>
		DeviceGeneral=0x00000004,
	}
}