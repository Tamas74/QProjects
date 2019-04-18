namespace gloScanWIA
{
	/// <summary>
	/// Planar
	/// </summary>
	[WiaPropertyId(4111)]
	public enum Planar /*WIA_IPA_PLANAR*/
	{
		/// <summary>Image data is in packed-pixel format.</summary>
		PackedPixel=0,
		/// <summary>Image data is in planar format.</summary>
		Planar=1,
	}
}