
namespace gloScanWIA
{
	/// <summary>
	/// WIA-Fehler
	/// </summary>
	public enum WiaError
	{
		/// <summary> An unknown error has occurred with the Microsoft Windows Image Acquisition (WIA) device. </summary>
		GeneralError=-2145320959,
		/// <summary> Paper is jammed in the scanner's document feeder. </summary>
		PaperJam=-2145320958,
		/// <summary> The user requested a scan and there are no documents left in the document feeder. </summary>
		PaperEmpty=-2145320957,
		/// <summary> An unspecified problem occurred with the scanner's document feeder. </summary>
		PaperProblem=-2145320956,
		/// <summary> The WIA device is not online. </summary>
		Offline=-2145320955,
		/// <summary> The WIA device is busy. </summary>
		Busy=-2145320954,
		/// <summary> The WIA device is warming up. </summary>
		WarmingUp=-2145320953,
		/// <summary> An unspecified error has occurred with the WIA device that requires user intervention. The user should ensure that the device is turned on, online, and any cables are properly connected. </summary>
		UserIntervention=-2145320952,
		/// <summary> The WIA device was deleted. It can no longer be accessed. </summary>
		ItemDeleted=-2145320951,
		/// <summary> An unspecified error occurred during an attempted communication with the WIA device. </summary>
		DeviceCommunication=-2145320950,
		/// <summary> The device does not support this command. </summary>
		InvalidCommand=-2145320949,
		/// <summary> There is an incorrect setting on the WIA device. </summary>
		IncorrectHardwareSetting=-2145320948,
		/// <summary> The scanner head is locked. </summary>
		DeviceLocked=-2145320947,
		/// <summary> The device driver threw an exception. </summary>
		ExceptionInDriver=-2145320946,
		/// <summary> The response from the driver is invalid. </summary>
		InvalidDriverResponse=-2145320945,
		/// <summary> The scanner's cover is opened. </summary>
		CoverOpen=-2145320944,
		/// <summary> The scanner's lamp is off.  </summary>
		LampOff=-2145320943,

		Destination=-2145320942,
		NetworkReservationFailed=-2145320941,

		/// <summary> No WIA device of the selected type is available. </summary>
		NoDeviceAvailable=-2145320939
	}
}
