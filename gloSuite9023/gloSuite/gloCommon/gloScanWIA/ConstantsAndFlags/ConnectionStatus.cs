namespace gloScanWIA
{
	/// <summary>
	/// Connection Status
	/// </summary>
	[WiaPropertyId(1027)]
	public enum ConnectionStatus /*WIA_DPA_CONNECT_STATUS*/
	{
		NotConnected=0,
		Connected=1,
	}
}