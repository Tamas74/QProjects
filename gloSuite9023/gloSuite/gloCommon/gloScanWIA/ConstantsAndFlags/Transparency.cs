namespace gloScanWIA
{
	/// <summary>
	/// Transparency Adapter & Transparency Adapter Status
	/// </summary>
	[WiaPropertyId(3101)]
	[WiaPropertyId(3107)]
	public enum Transparency /*WIA_DPS_TRANSPARENCY & WIA_DPS_TRANSPARENCY_STATUS*/
	{
		LightSourcePresentDetect=0x01,
		LightSourcePresent=0x02,
		LightSourceDetectReady=0x04,
		LightSourceReady=0x08,
	}
}