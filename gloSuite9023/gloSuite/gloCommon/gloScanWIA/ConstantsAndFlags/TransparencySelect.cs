using System;

namespace gloScanWIA
{
	/// <summary> 
	/// Transparency Adapter Select
	/// </summary>
	[Flags]
	[WiaPropertyId(3102)]
	public enum TransparencySelect /*WIA_DPS_TRANSPARENCY_SELECT*/
	{
		Select=0x001, // currently not used
		Positive=0x002,
		Negative=0x004,
	}
}