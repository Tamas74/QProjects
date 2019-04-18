using System;
using WIA;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Eigenschaften bereit, um einfach auf die Scanner-Properties eines WIA-Gerätes zugreifen zu können
	/// </summary>
	public class ScannerDeviceSettings : DeviceSettings
	{
		/// <summary>
		/// Erstellt eine neue Instanz zum vereinfachten Zugriff auf die Scanner-Eigenschaften eines WIA-Gerätes
		/// </summary>
		/// <param name="properties">die Eigenschaften des WIA-Geräts</param>
		public ScannerDeviceSettings(IProperties properties)
			: base(properties)
		{ }



		/// <summary> 
		/// Horizontal Bed Size 
		/// The HorizontalBedSize property contains the physical dimensions of a scanner's flatbed, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the HorizontalBedSize property is still available, but it has been replaced by the WIA_IPS_MAX_HORIZONTAL_SIZE property, so you should consider it optional.
		/// </summary>
		[WiaPropertyId(3074)]
		public int HorizontalBedSize
		{
			get { return this.GetPropertyValue<int>(3074); }
		} /*WIA_DPS_HORIZONTAL_BED_SIZE*/ // 0xc02


		/// <summary> 
		/// Vertical Bed Size 
		/// The VerticalBedSize property contains the physical vertical dimensions of a scanner's flatbed, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the VerticalBedSize property is still available at the root level of the WIA driver. But this property has been replaced with the WIA_IPS_MAX_VERTICAL_SIZE property, and you should consider it to be optional.
		/// </summary>
		[WiaPropertyId(3075)]
		public int VerticalBedSize
		{
			get { return this.GetPropertyValue<int>(3075); }
		} /*WIA_DPS_VERTICAL_BED_SIZE*/ // 0xc03


		/// <summary> 
		/// Horizontal Sheet Feed Size
		/// The HorizontalSheetFeedSize property contains the physical horizontal dimensions of  scanner's document feeder, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the HorizontalSheetFeedSize property is still available at the root level of the WIA driver, but it has been replaced by the WIA_IPS_MAX_HORIZONTAL_SIZE property, so you should consider it optional.
		/// </summary>
		[WiaPropertyId(3076)]
		public int HorizontalSheetFeedSize
		{
			get { return this.GetPropertyValue<int>(3076); }
		} /*WIA_DPS_HORIZONTAL_SHEET_FEED_SIZE*/ // 0xc04


		/// <summary> 
		/// Vertical Sheet Feed Size 
		/// The VerticalSheetFeedSize property contains the physical vertical dimensions of a scanner's document feeder, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the VerticalSheetFeedSize property is still available at the root level of the WIA driver But this property has been replaced with the WIA_IPS_MAX_VERTICAL_SIZE property, and you should consider it to be optional.
		/// </summary>
		[WiaPropertyId(3077)]
		public int VerticalSheetFeedSize
		{
			get { return this.GetPropertyValue<int>(3077); }
		} /*WIA_DPS_VERTICAL_SHEET_FEED_SIZE*/ // 0xc05


		/// <summary> 
		/// Sheet Feeder Registration
		/// The SheetFeederRegistration property contains the registration, or alignment and edge detection, for documents that are placed on the flatbed of a scanner. The WIA minidriver creates and maintains this property.
		/// The SheetFeederRegistration property indicates how a document is horizontally positioned on the scanning head of a handheld or sheet-fed scanner. The scanner uses the property to predict where a user places a document on the scanning head.
		/// For scanners that support more than one scanning head, the SheetFeederRegistration property is relative to the topmost scanning head. This property is required for sheet-fed, scroll-fed, and handheld scanners.
		/// Versions: For Windows Vista and later operating systems, use the WIA_IPS_SHEET_FEEDER_REGISTRATION property instead.
		/// </summary>
		[WiaPropertyId(3078)]
		public HorizontalRegistration SheetFeederRegistration
		{
			get { return this.GetPropertyValue<HorizontalRegistration>(3078); }
		} /*WIA_DPS_SHEET_FEEDER_REGISTRATION*/ // 0xc06


		/// <summary> 
		/// Horizontal Bed Registration 
		/// The HorizontalBedRegistration property contains the registration, or horizontal alignment, for documents that are placed on the flatbed of a scanner. The WIA minidriver creates and maintains this property.
		/// Versions: Obsolete in Windows Vista and later operating systems and should no longer be used. However, this property is still defined in Windows Vista for compatibility with applications and devices designed for Windows Server 2003, Windows XP, and previous versions of Windows.
		/// </summary>
		[WiaPropertyId(3079)]
		public HorizontalRegistration HorizontalBedRegistration
		{
			get { return this.GetPropertyValue<HorizontalRegistration>(3079); }
		} /*WIA_DPS_HORIZONTAL_BED_REGISTRATION*/ // 0xc07


		/// <summary> 
		/// Vertical Bed Registration
		/// The VerticalBedRegistration property contains the registration, or vertical alignment and edge detection, for documents that are placed on the flatbed of a scanner. The WIA minidriver creates and maintains this property.
		/// Versions: Obsolete in Windows Vista and later operating systems and should no longer be used. However, this property is still defined in Windows Vista for compatibility with applications and devices designed for Windows Server 2003, Windows XP, and previous versions of Windows.
		/// </summary>
		[WiaPropertyId(3080)]
		public VerticalRegistration VerticalBedRegistration
		{
			get { return this.GetPropertyValue<VerticalRegistration>(3080); }
		} /*WIA_DPS_VERTICAL_BED_REGISTRATION*/ // 0xc08


		/// <summary>
		/// Platen Color 
		/// The PlatenColor property contains the current platen color. 
		/// A minidriver should report the PlatenColor  as a vector of four BYTE values in the form of an RGBQUAD structure (which is described in the Microsoft Windows SDK documentation). The WIA minidriver creates and maintains this property.
		/// An application reads PlatenColor to get the scanner's platen color. This color can help the application post-process the final image.
		/// </summary>
		[WiaPropertyId(3081)]
		public int PlatenColor
		{
			get { return this.GetPropertyValue<int>(3081); }
		} /*WIA_DPS_PLATEN_COLOR*/ // 0xc09


		/// <summary> 
		/// Pad Color
		/// The PadColor property contains the current pad color that is used when the WIA minidriver pads unaligned data. The WIA minidriver creates and maintains this property.
		/// The PadColor property should be reported as a vector of four BYTE values in the form of an RGBQUAD structure (which is described in the Microsoft Windows SDK documentation). 
		/// An application reads PadColor to get the padding color that is used.
		/// </summary>
		[WiaPropertyId(3082)]
		public UInt16 PadColor
		{
			get { return this.GetPropertyValue<UInt16>(3082); }
			set { this.SetPropertyValue(3082,value); }
		} /*WIA_DPS_PAD_COLOR*/ // 0xc0a


		/// <summary> 
		/// Filter Select 
		/// The FilterSelect property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(3083)]
		public int FilterSelect
		{
			get { return this.GetPropertyValue<int>(3083); }
		} /*WIA_DPS_FILTER_SELECT*/  // 0xc0b


		/// <summary> 
		/// Dither Select
		/// The DitherSelect property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(3084)]
		public int DitherSelect
		{
			get { return this.GetPropertyValue<int>(3084); }
		} /*WIA_DPS_DITHER_SELECT*/  // 0xc0c


		/// <summary> 
		/// Dither Pattern Data
		/// The DitherPatternData property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(3085)]
		public int DitherPatternData
		{
			get { return this.GetPropertyValue<int>(3085); }
		} /*WIA_DPS_DITHER_PATTERN_DATA*/  // 0xc0d


		/// <summary> 
		/// Document Handling Capabilities
		/// The DocumentHandlingCapabilities property contains the capabilities of a scanner. 
		/// </summary>
		[WiaPropertyId(3086)]
		public DocumentHandlingCapabilities DocumentHandlingCapabilities
		{
			get { return this.GetPropertyValue<DocumentHandlingCapabilities>(3086); }
		} /*WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES*/ // 0xc0e


		/// <summary> 
		/// Document Handling Status
		/// The DocumentHandlingStatus property contains the current state of a scanner's installed flatbed, document feeder, or duplexer. 
		/// An application reads the DocumentHandlingStatus property to determine whether a scanner device is ready to use. Reading this property is an ideal way to check whether paper is in the feeder before a user acquires an image. The WIA minidriver creates and maintains this property.
		/// Note: There are no custom-defined base definitions. You cannot create custom extensions for status flag values. If you need custom status reporting, you should define a custom property.
		/// </summary>
		[WiaPropertyId(3087)]
		public DocumentHandlingStatus DocumentHandlingStatus
		{
			get { return this.GetPropertyValue<DocumentHandlingStatus>(3087); }
		} /*WIA_DPS_DOCUMENT_HANDLING_STATUS*/ // 0xc0f


		/// <summary>
		/// Document Handling Select
		/// The DocumentHandlingSelect property contains the current scanner acquisition source and mode. 
		/// An application reads the DocumentHandlingSelect property to determine the current acquisition source of a scanner, or an application write this property to set the source and mode of the scanner. In addition, applications use this property to enable and disable duplexer functionality. The WIA minidriver creates and maintains this property.
		/// The values DUPLEX and FRONT_ONLY are mutually exclusive set one or the other, but not both.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the WIA_IPS_DOCUMENT_HANDLING_SELECT property.
		/// </summary>
		[WiaPropertyId(3088)]
		public DocumentHandlingSelect DocumentHandlingSelect
		{
			get { return this.GetPropertyValue<DocumentHandlingSelect>(3088); }
			set { this.SetPropertyValue(3088,value); }
		} /*WIA_DPS_DOCUMENT_HANDLING_SELECT*/ // 0xc10


		/// <summary>
		/// Document Handling Capacity
		/// The DocumentHandlingCapacity property is obsolete and should not be used.
		/// </summary>
		[Obsolete]
		public int DocumentHandlingCapacity
		{
			get { return this.GetPropertyValue<int>(3089); }
		} /*WIA_DPS_DOCUMENT_HANDLING_CAPACITY*/ // 0xc11


		/// <summary>
		/// Horizontal Optical Resolution
		/// The HorizontalOpticalResolution property contains the highest-supported horizontal optical resolution of the device, in dots per inch (dpi). The WIA minidriver creates and maintains this property.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_OPTICAL_XRES property.
		/// </summary>
		[WiaPropertyId(3090)]
		public int HorizontalOpticalResolution
		{
			get { return this.GetPropertyValue<int>(3090); }
		} /*WIA_DPS_OPTICAL_XRES*/ // 0xc12


		/// <summary>
		/// Vertical Optical Resolution
		/// The VerticalOpticalResolution property contains the highest-supported vertical optical resolution of the device, in dots per inch (dpi). The WIA minidriver creates and maintains this property.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_OPTICAL_YRES property.
		/// </summary>
		[WiaPropertyId(3091)]
		public int VerticalOpticalResolution
		{
			get { return this.GetPropertyValue<int>(3091); }
		} /*WIA_DPS_OPTICAL_YRES*/ // 0xc13


		/// <summary>
		/// Endorser Characters
		/// The EndorserCharacters property contains all of the valid characters that an application can use to create valid endorser strings. 
		/// An endorser is a printer that is installed on a scanner that imprints a text message on every page that is scanned. The WIA minidriver should validate the setting of the WIA_DPS_ENDORSER_STRING property against the valid character set in the EndorserCharacters property. The minidriver creates and maintains this property.
		/// </summary>
		[WiaPropertyId(3092)]
		public int EndorserCharacters
		{
			get { return this.GetPropertyValue<int>(3092); }
		} /*WIA_DPS_ENDORSER_CHARACTERS*/ // 0xc14


		/// <summary> Endorser String
		/// The EndorserString property contains a string that is to be endorsed (that is, printed) on each page that the minidriver scans. 
		/// An application sets the EndorserString property by using the valid character set that is reported in the WIA_DPS_ENDORSER_CHARACTERS property. The WIA minidriver should endorse documents only if a string is set in EndorserString. An empty string means that the endorser functionality is disabled.
		/// Because the driver must interpret the endorser string, your driver can use special characters in EndorserString. However, only your applications will understand these characters.
		/// A driver that supports the EndorserString property must support the following list of tokens:
		/// $DATE$	The date in the form YYYY/MM/DD.
		/// $DAY$	The day, in the form DD.
		/// $MONTH$	The month of the year, in the form MM.
		/// $PAGE_COUNT$	The number of pages that are transferred.
		/// $TIME$	The time of day, in the form HH:MM:SS.
		/// $YEAR$	The year, in the form YYYY.
		/// </summary>
		[WiaPropertyId(3093)]
		public string EndorserString
		{
			get { return this.GetPropertyValue<string>(3093); }
			set { this.SetPropertyValue(3093,value); }
		} /*WIA_DPS_ENDORSER_STRING*/ // 0xc15


		/// <summary>
		/// Scan Ahead Pages
		/// The ScanAheadPages property contains a value that indicates whether a scanner will cache pages in a scanner buffer before sending them to an application.
		/// If the ScanAheadPages property is zero, scan ahead is disabled, and the scanner will not scan ahead any pages. 
		/// If the scanner performs data transfers on the buffered scan-ahead item, the scanner will retrieve the buffered pages. WIA properties cannot be changed during a scan-ahead operation. ScanAheadPages is optional.
		/// </summary>
		[WiaPropertyId(3094)]
		public int ScanAheadPages
		{
			get { return this.GetPropertyValue<int>(3094); }
			set { this.SetPropertyValue(3094,value); }
		} /*WIA_DPS_SCAN_AHEAD_PAGES*/ // 0xc16


		/// <summary>
		/// Max Scan Time
		/// The MaxScanTime property contains the maximum time to scan a single page with the current property settings, in milliseconds.
		/// An application reads the MaxScanTime property to estimate how much the time it will take to scan a page. This estimate is helpful when you are determining the conditions of a device that has stopped responding. The WIA minidriver creates and maintains this property.
		/// </summary>
		[WiaPropertyId(3095)]
		public int MaxScanTime
		{
			get { return this.GetPropertyValue<int>(3095); }
		} /*WIA_DPS_MAX_SCAN_TIME*/ // 0xc17


		/// <summary>
		/// Pages
		/// The Pages property contains the current number of pages to acquire from an automatic document feeder. 
		/// An application reads the Pages property to determine a document feeder's page capacity. The application also sets this property to the number of pages it is going to scan. The WIA minidriver creates and maintains Pages.
		/// If you set Pages to zero (0)the scanner will process continuously until no more documents are fed into the ADF.
		/// Note: If duplex mode is enabled (that is, the WIA_DPS_DOCUMENT_HANDLING_SELECT property is set to FEEDER | DUPLEX), Pages is still equal to the number of pages to scan.One sheet of paper will automatically contain two pages if DUPLEX is enabled, even if the back side of the page is blank.
		/// If you set Pages to 1, the scanner will process one of the sides of the page. If a scanner is unable to scan only one side of a page while in duplex mode, you should change the Pages value for the Inc member of the WIA_PROPERTY_INFO structure to 2. This value signals to the application that it must request pages in multiples of two. If Pages is zero, the scanner will scan all pages that are currently loaded into the document feeder.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGES property.
		/// </summary>
		[WiaPropertyId(3096)]
		public int Pages
		{
			get { return this.GetPropertyValue<int>(3096); }
			set { this.SetPropertyValue(3096,value); }
		} /*WIA_DPS_PAGES*/ // 0xc18


		/// <summary>
		/// Page Size
		/// The PageSize property contains the size of the page that is currently selected to be scanned. 
		/// To select the dimensions of the page to scan, an application sets PageSize. The WIA minidriver creates and maintains this property.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGE_SIZE property.
		/// </summary>
		[WiaPropertyId(3097)]
		public PageSize PageSize
		{
			get { return this.GetPropertyValue<PageSize>(3097); }
			set { this.SetPropertyValue(3097,value); }
		} /*WIA_DPS_PAGE_SIZE*/ // 0xc19


		/// <summary>
		/// Page Width
		/// The PageWidth property contains the width of the currently selected page, in thousandths of an inch (.001).
		/// An application reads the PageWidth property to determine the physical dimensions of the page that is being scanned. If the extent settings are different from known page sizes, this property reports the width of the page whose WIA_DPS_PAGE_SIZE property is set to WIA_PAGE_CUSTOM. The WIA minidriver creates and maintains PageWidth.
		/// PageWidth must provide a measurement equivalent to the value of the WIA_IPS_XEXTENT property, which reports the width, in pixels, of the page to scan.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGE_WIDTH property.
		/// </summary>
		[WiaPropertyId(3098)]
		public int PageWidth
		{
			get { return this.GetPropertyValue<int>(3098); }
		} /*WIA_DPS_PAGE_WIDTH*/ // 0xc1a


		/// <summary>
		/// Page Height
		/// The PageHeight property contains the height, in thousandths of an inch (.001), of the currently selected page. The WIA minidriver creates and maintains this property.
		/// An application reads PageHeight to determine the physical dimensions of the page that is being scanned. If the extent settings are different from the known page sizes, this property reports the height of the page whose WIA_DPS_PAGE_SIZE property is set to WIA_PAGE_CUSTOM (which is a value of the WIA_DPS_PAGE_SIZE property).
		/// PageHeight must provide a measurement in thousandths of an inch that is equivalent to the pixel value reported by the WIA_IPS_YEXTENT property, which reports the height, in pixels, of the page to be scanned.
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGE_HEIGHT property.
		/// </summary>
		[WiaPropertyId(3099)]
		public int PageHeight
		{
			get { return this.GetPropertyValue<int>(3099); }
		} /*WIA_DPS_PAGE_HEIGHT*/ // 0xc1b


		/// <summary>
		/// Preview
		/// The Preview property indicates the preview mode for a device. An application sets this property to place the device into a preview mode. 
		/// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PREVIEW property.
		/// </summary>
		[WiaPropertyId(3100)]
		public Preview Preview
		{
			get { return this.GetPropertyValue<Preview>(3100); }
			set { this.SetPropertyValue(3100,value); }
		} /*WIA_DPS_PREVIEW*/ // 0xc1c


		/// <summary>
		/// Transparency Adapter
		/// The Transparency property is obsolete and should not be used.
		/// </summary>
		[Obsolete]
		[WiaPropertyId(3101)]
		public Transparency Transparency
		{
			get { return this.GetPropertyValue<Transparency>(3101); }
		} /*WIA_DPS_TRANSPARENCY*/ // 0xc1d


		/// <summary>
		/// Transparency Adapter Select
		/// The TransparencySelect property is obsolete and should not be used.
		/// </summary>
		[Obsolete]
		[WiaPropertyId(3102)]
		public TransparencySelect TransparencySelect
		{
			get { return this.GetPropertyValue<TransparencySelect>(3102); }
		} /*WIA_DPS_TRANSPARENCY_SELECT*/ // 0xc1e


		/// <summary>
		/// Show preview control
		/// The ShowPreviewControl property indicates whether an item needs a preview control displayed to the user. The WIA minidriver creates and maintains this property.
		/// The ShowPreviewControl property helps control devices that cannot preview. For example, some feeder-driven devices cannot reload the paper for a preview scan.
		/// Versions: Available with Microsoft Windows XP. ForWindows Vista and later, use the WIA_IPS_SHOW_PREVIEW_CONTROL property.
		/// </summary>
		[WiaPropertyId(3103)]
		public ShowPreviewControl ShowPreviewControl
		{
			get { return this.GetPropertyValue<ShowPreviewControl>(3103); }
		} /*WIA_DPS_SHOW_PREVIEW_CONTROL*/ // 0xc1f


		/// <summary>
		/// Minimum Horizontal Sheet Feed Size
		/// The MinHorizontalSheetFeedSize property contains the physical horizontal dimensions of the smallest page that a scanner's document feeder can scan, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the MinHorizontalSheetFeedSize property is still available at the root level of the WIA driver, but it has been replaced by the WIA_IPS_MIN_HORIZONTAL_SIZE property, so you should consider it optional.
		/// </summary>
		[WiaPropertyId(3104)]
		public int MinHorizontalSheetFeedSize
		{
			get { return this.GetPropertyValue<int>(3104); }
		} /*WIA_DPS_MIN_HORIZONTAL_SHEET_FEED_SIZE*/ // 0xc20


		/// <summary>
		/// Minimum Vertical Sheet Feed Size
		/// The MinVerticalSheetFeedSize property contains the physical vertical dimensions of the smallest page that a scanner's document feeder can scan, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Beginning with Windows Vista, the WIAMinVerticalSheetFeedSize property is still available at the root level of the WIA driver, but it has been replaced by the WIA_IPS_MIN_VERTICAL_SIZE property, so you should consider it optional.
		/// </summary>
		[WiaPropertyId(3105)]
		public int MinVerticalSheetFeedSize
		{
			get { return this.GetPropertyValue<int>(3105); }
		} /*WIA_DPS_MIN_VERTICAL_SHEET_FEED_SIZE*/ // 0xc21


		/// <summary>
		/// Transparency Adapter Capabilities
		/// The TransparencyCapabilities property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(3106)]
		public TransparencyCapabilities TransparencyCapabilities
		{
			get { return this.GetPropertyValue<TransparencyCapabilities>(3106); }
		} /*WIA_DPS_TRANSPARENCY_CAPABILITIES*/  // 0xc22


		/// <summary>
		/// Transparency Adapter Status 
		/// The TransparencyStatus property is obsolete and should not be used.
		/// </summary>
		[Obsolete]
		[WiaPropertyId(3107)]
		public Transparency TransparencyStatus
		{
			get { return this.GetPropertyValue<Transparency>(3107); }
		} /*WIA_DPS_TRANSPARENCY_STATUS*/  // 0xc23


		/// <summary>
		/// User Name
		/// The UserName property on the WIA driver root item allows drivers to get the domain name or the machine name and user name of the user who is accessing the WIA device at a given point in time. The WIA service creates and maintains this property.
		/// Versions: Available on Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(3112)]
		public string UserName
		{
			get { return this.GetPropertyValue<string>(3112); }
		} /*WIA_DPS_USER_NAME*/  // 0xc28


		/// <summary>
		/// Service ID
		/// The ServiceID property contains the service ID of a Web services scanner device. The WIA minidriver creates and maintains this property.
		/// VT_BSTR	Read-only
		/// The WIA minidriver initializes this property at run time by reading the PKEY_PNPX_ServiceId device property from the Function Instance object.
		/// </summary>
		[WiaPropertyId(3113)]
		public string ServiceID
		{
			get { return this.GetPropertyValue<string>(3113); }
		} /*WIA_DPS_SERVICE_ID*/  // 0xc29


		/// <summary>
		/// Device ID
		/// The DeviceID property contains a unique Function Instance identifier for a Web services scanner device. This identifier represents the Web service on the scanner device with which the WIA minidriver is communicating. No assumptions about the form of this identifier should be made. The WIA minidriver creates and maintains this property.WIA applications can use the value of DeviceID to find, using the Function Discovery API, the Function Instance object that represents the Web services scanner device used in the current WIA session.
		/// The WIA minidriver initializes this property at run time by reading the PKEY_PNPX_ID device property from the Function Instance object. 
		/// </summary>
		[WiaPropertyId(3114)]
		public string ScannerDeviceID
		{
			get { return this.GetPropertyValue<string>(3114); }
		} /*WIA_DPS_DEVICE_ID*/  // 0xc2a


		/// <summary>
		/// Global Identity
		/// The GlobalIdentity property contains the SOAP address of a Web services scanner device. The WIA minidriver creates and maintains this property. 
		/// The WIA minidriver initializes this property at run time by reading the PKEY_PNPX_GlobalIdentity device property from the Function Instance object.
		/// Both PKEY_PNPX_GlobalIdentity and PKEY_PNPX_ID contain a unique ID of the UPnP Device. The difference is that PKEY_PNPX_GlobalIdentity always contains the UUID of the root device for all Function Instances, while PKEY_PNPX_ID contains the UUID of the Device/Sub-Device that the Function Instance represents.
		/// </summary>
		[WiaPropertyId(3115)]
		public string GlobalIdentity
		{
			get { return this.GetPropertyValue<string>(3115); }
		} /*WIA_DPS_GLOBAL_IDENTITY*/  // 0xc2b
	}
}