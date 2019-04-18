using System;
using WIA;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Eigenschaften bereit, um einfach auf die Properties eines WIA-Gerätes zugreifen zu können
	/// </summary>
	public class DeviceSettings : WiaSettings
	{
		/// <summary>
		/// Erstellt eine neue Instanz zum vereinfachten Zugriff auf die Eigenschaften eines WIA-Gerätes
		/// </summary>
		/// <param name="properties">die Eigenschaften des WIA-Geräts</param>
		public DeviceSettings(IProperties properties)
			: base(properties)
		{ }



		/// <summary> 
		/// Unique Device ID 
		/// The DeviceID property contains the device identifier (ID) string for a WIA minidriver. The WIA service creates and maintains this property. 
		/// </summary>
		[WiaPropertyId(2)]
		public string DeviceID
		{
			get { return this.GetPropertyValue<string>(2); }
		} /*WIA_DIP_DEV_ID*/  // 0x2


		/// <summary> 
		/// Manufacturer 
		/// The VendorDescription property contains a vendor description string for the WIA minidriver. The WIA service creates and maintains this property.
		/// The vendor description is obtained from the INF file. An application reads the VendorDescription property to get a description of the device vendor. 
		/// </summary>
		[WiaPropertyId(3)]
		public string VendorDescription
		{
			get { return this.GetPropertyValue<string>(3); }
		} /*WIA_DIP_VEND_DESC*/  // 0x3


		/// <summary> 
		/// Description
		/// The DeviceDescription property contains the device description string for a WIA minidriver. The WIA service creates and maintains this property.
		/// The device description string that the DeviceDescription property contains is obtained from the driver's INF file. An application reads this property to get a description of the device. 
		/// </summary>
		[WiaPropertyId(4)]
		public string DeviceDescription
		{
			get { return this.GetPropertyValue<string>(4); }
		} /*WIA_DIP_DEV_DESC*/  // 0x4


		/// <summary> 
		/// Device Type 
		/// The DeviceType property contains a device type and device subtype. The WIA service creates and maintains this property 
		/// The device type and subtype are obtained from the driver's INF file of the device file. An application reads the DeviceType property to determine whether it is using a scanner, camera, or video device.
		/// For more information about INF files, see INF Files for WIA Devices. The StiDeviceTypeXxx constants are defined in Sti.h.
		/// </summary>
		[WiaPropertyId(5)]
		public WiaDeviceType DeviceType
		{
			get { return this.GetPropertyValue<WiaDeviceType>(5); }
		} /*WIA_DIP_DEV_TYPE*/  // 0x5


		/// <summary> 
		/// Port 
		/// The PortName property contains an installed device's port name, which is assigned by the kernel-mode driver that operates the device. The WIA service creates and maintains this property.
		/// An application reads the PortName property to determine the port name. 
		/// </summary>
		[WiaPropertyId(6)]
		public string PortName
		{
			get { return this.GetPropertyValue<string>(6); }
		} /*WIA_DIP_PORT_NAME*/  // 0x6


		/// <summary>
		/// Name 
		/// The DeviceName property contains the name of a device. The WIA service creates and maintains this property.
		/// The device name that is contained in the DeviceName property is obtained from the driver's INF file. An application reads this property to obtain the name of the device. 
		/// </summary>
		[WiaPropertyId(7)]
		public string DeviceName
		{
			get { return this.GetPropertyValue<string>(7); }
		} /*WIA_DIP_DEV_NAME*/  // 0x7


		/// <summary>
		/// Server 
		/// The ServerName property contains the name of the server that a WIA minidriver is running on.
		/// The default value of ServerName is "local". This property should contain the string "local" when an application is connected to a device on the same computer. 
		/// Versions: Optional for Microsoft Windows XP and later operating systems.
		/// </summary>
		[WiaPropertyId(8)]
		public string ServerName
		{
			get { return this.GetPropertyValue<string>(8); }
		} /*WIA_DIP_SERVER_NAME*/  // 0x8


		/// <summary> 
		/// Remote Device ID 
		/// The RemoteDeviceID property contains the device identifier (ID) of a WIA device that is installed on a remote computer. The WIA service creates and maintains this property. 
		/// </summary>
		[WiaPropertyId(9)]
		public string RemoteDeviceID
		{
			get { return this.GetPropertyValue<string>(9); }
		} /*WIA_DIP_REMOTE_DEV_ID*/  // 0x9


		/// <summary> 
		/// UI Class ID
		/// The UiClassID property contains the vendor-supplied class identifier (CLSID) for any user interface (UI) extension COM object that is installed with a WIA minidriver. The WIA service creates and maintains this property.
		/// The UI CLSID value that is contained in the UiClassID property is obtained from the driver's INF file. If no UI CLSID is specified, the WIA service supplies a default value. This property is used only internally by the WIA service when UI is being displayed. 
		/// </summary>
		[WiaPropertyId(10)]
		public string UiClassID
		{
			get { return this.GetPropertyValue<string>(10); }
		} /*WIA_DIP_UI_CLSID*/  // 0xa


		/// <summary> 
		/// Hardware Configuration 
		/// The HardwareConfiguration property indicates the type of connection that a device is using. The WIA service creates and maintains this property, and only the WIA service can change it. 
		/// An application reads the HardwareConfiguration property to determine the device's connection type.
		/// </summary>
		[WiaPropertyId(11)]
		public HardwareConfiguration HardwareConfiguration
		{
			get { return this.GetPropertyValue<HardwareConfiguration>(11); }
		} /*WIA_DIP_HW_CONFIG*/  // 0xb


		/// <summary> 
		/// BaudRate 
		/// The Baudrate property contains the current baud rate setting for a device. The WIA service creates and maintains this property.
		/// The value of the Baudrate property should be "Empty" if the device is not connected by a serial cable. 
		/// </summary>
		[WiaPropertyId(12)]
		public string Baudrate
		{
			get { return this.GetPropertyValue<string>(12); }
		} /*WIA_DIP_BAUDRATE*/  // 0xc


		/// <summary> 
		/// STI Generic Capabilities
		/// The StiGenericCapabilities property contains the generic STI capabilities for a device, which are obtained from the driver's INF file. The WIA service creates and maintains this property.
		/// An application reads the StiGenericCapabilities property to determine the generic STI capabilities of the device. 
		/// </summary>
		[WiaPropertyId(13)]
		public int StiGenericCapabilities
		{
			get { return this.GetPropertyValue<int>(13); }
		} /*WIA_DIP_STI_GEN_CAPABILITIES*/  // 0xd


		/// <summary> 
		/// WIA Version 
		/// The WiaVersion property contains the number (as a string) of the current WIA version that is installed on a computer. The WIA service creates and maintains this property. 
		/// An application reads WiaVersion to determine the version of WIA that is installed on the computer. 
		/// Versions: Available in Microsoft Windows XP and later operating systems.
		/// </summary>
		[WiaPropertyId(14)]
		public string WiaVersion
		{
			get { return this.GetPropertyValue<string>(14); }
		} /*WIA_DIP_WIA_VERSION*/  // 0xe


		/// <summary> 
		/// Driver Version
		/// The DriverVersion property contains the current DLL version of a WIA minidriver. The WIA service creates and maintains this property. 
		/// If the WIA minidriver does not supply a version resource, the WIA service supplies the value "0.0.0.0" as a default. An application reads DriverVersion to determine the version of the WIA minidriver DLL.
		/// Note:  Beginning with Windows Vista, the wildcard IP address 0.0.0.0 is not available.
		/// Also beginning with Windows Vista, if the IPAutoconfigurationEnabled registry key is set to a value of 0, automatic IP address assignment is disabled, and no IP address is assigned. In this case, the ipconfig command line tool will not display an IP address. If the key is set to a nonzero value, an IP address is automatically assigned. This key can be located at the following paths in the registry:
		/// HKEY_LOCAL_MACHINE\SYSTEM\Current Control Set\Services\Tcpip\Parameters\IPAutoconfigurationEnabled
		/// HKEY_LOCAL_MACHINE\SYSTEM\Current Control Set\Services\Tcpip\Parameters\Interfaces\GUID\IPAutoconfigurationEnabled
		/// Versions: Available in Microsoft Windows XP and later operating systems.
		/// </summary>
		[WiaPropertyId(15)]
		public string DriverVersion
		{
			get { return this.GetPropertyValue<string>(15); }
		} /*WIA_DIP_DRIVER_VERSION*/  // 0xf


		/// <summary> 
		/// PnP ID String 
		/// The current PnP id for the device. The WIA service creates and maintains this property. This property is available in Windows Vista and later.
		/// </summary>
		[WiaPropertyId(16)]
		public string PnpID
		{
			get { return this.GetPropertyValue<string>(16); }
		} /*WIA_DIP_PNP_ID*/  // 0x10


		/// <summary> 
		/// STI Driver Version
		/// The generic STI driver version. The WIA service creates and maintains this property. An application reads this property to determine the generic STI driver version. This property is available in Windows Vista and later.
		/// </summary>
		[WiaPropertyId(17)]
		public string StiDriverVersion
		{
			get { return this.GetPropertyValue<string>(17); }
		} /*WIA_DIP_STI_DRIVER_VERSION*/  // 0x11


		/// <summary>
		/// Firmware Version
		/// The FirmwareVersion property contains a device firmware version. The minidriver creates and maintains this property.
		/// The value of the FirmwareVersion property must be a string value, such as "1.0.4" or "1.0abc".
		/// </summary>
		[WiaPropertyId(1026)]
		public string FirmwareVersion
		{
			get { return this.GetPropertyValue<string>(1026); }
		} /*WIA_DPA_FIRMWARE_VERSION*/  // 0x402


		/// <summary> 
		/// Connect Status 
		/// The ConnectStatus property contains the current connection status for a device. The WIA minidriver creates and maintains this property.
		/// </summary>
		[WiaPropertyId(1027)]
		public ConnectionStatus ConnectionStatus
		{
			get { return this.GetPropertyValue<ConnectionStatus>(1027); }
		} /*WIA_DPA_CONNECT_STATUS*/  // 0x403


		/// <summary> 
		/// Device Time 
		/// The DeviceTime property contains the current clock time that is stored on a device. The minidriver creates and maintains this property.
		/// When the DeviceTime property is read, the minidriver should check the device's current clock time and should always return the current time. This property is supported only by devices that have an internal clock. If the device clock can be set, this property is read/write; otherwise, it is read-only. WIA devices report time in a SYSTEMTIME structure (which is described in the Microsoft Windows SDK documentation).
		/// </summary>
		[WiaPropertyId(1028)]
		public UInt16 DeviceTime
		{
			get { return this.GetPropertyValue<UInt16>(1028); }
			set { this.SetPropertyValue(1028,value); }
		} /*WIA_DPA_DEVICE_TIME*/  // 0x404
	}
}