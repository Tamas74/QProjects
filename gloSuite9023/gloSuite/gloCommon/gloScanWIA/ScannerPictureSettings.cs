using WIA;
using System.Diagnostics;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Eigenschaften bereit, um einfach auf die Scannerbild-Properties eines WIA-Gerätes zugreifen zu können
	/// </summary>
	[DebuggerStepThrough]
	public class ScannerPictureSettings : PictureSettings
	{
		/// <summary>
		/// Erstellt eine neue Instanz zum vereinfachten Zugriff auf die Scannerbild-Eigenschaften eines WIA-Gerätes
		/// </summary>
		/// <param name="properties">die Eigenschaften des WIA-Geräts</param>
		public ScannerPictureSettings(IProperties properties)
			: base(properties)
		{ }



		/// <summary> 
		/// Current Intent 
		/// The CurrentIntent property contains the current settings for an application's intended use of an image. The WIA minidriver creates and maintains this property.
		/// A driver uses the intent settings to pre-set item properties based on an application's intended use of an image. These properties might include, for example, maximum quality and minimum size.
		/// The driver chooses the bit depth, in dots per inch, and other settings that it determines are appropriate for the selected intent. The application must read the current settings to determine which properties were changed.
		/// An application sets the WIA_IPS_CUR_INTENT property to auto-set the WIA properties for specific acquisition intent. Note that flags can be combined with a bitwise OR operator, but an image cannot be both grayscale and color.
		/// WIA_IPS_CUR_INTENT is required for all image acquisition enabled items; it is not available for storage items or stored image items.
		/// </summary>
		[WiaPropertyId(6146)]
		public CurrentIntent CurrentIntent
		{
			get { return this.GetPropertyValue<CurrentIntent>(6146); }
			set { this.SetPropertyValue(6146,value); }
		} /*WIA_IPS_CUR_INTENT*/ // 0x1802


		/// <summary>
		/// Horizontal Resolution
		/// The HorizontalResolution property contains the current horizontal resolution, in pixels per inch, for a device. 
		/// An application sets the HorizontalResolution property to set the horizontal resolution. The WIA minidriver creates and maintains this property.
		/// If a device can be set to only a single value, create a WIA_PROP_LIST type and place the valid value in it. This situation also applies when one resolution setting depends on another resolution. (For example, the vertical resolution can depend on the horizontal resolution.)
		/// HorizontalResolution is required for all image acquisition-enabled items and stored image items; it is not available for storage items.
		/// </summary>
		[WiaPropertyId(6147)]
		public int HorizontalResolution
		{
			get { return this.GetPropertyValue<int>(6147); }
			set { this.SetPropertyValue(6147,value); }
		} /*WIA_IPS_XRES*/ // 0x1803


		/// <summary>
		/// Vertical Resolution
		/// The VerticalResolution property contains the current vertical resolution setting, in pixels per inch, for a device. 
		/// An application sets the VerticalResolution property to set the vertical resolution. The WIA minidriver creates and maintains this property. 
		/// If a device can be set to only a single value, create a WIA_PROP_LIST type and place the valid value in it. This situation also applies when one resolution setting depends on another resolution. (For example, the vertical resolution can depend on the horizontal resolution.)
		/// VerticalResolution is required for all image acquisition-enabled items and stored image items; it is not available for storage items.
		/// </summary>
		[WiaPropertyId(6148)]
		public int VerticalResolution
		{
			get { return this.GetPropertyValue<int>(6148); }
			set { this.SetPropertyValue(6148,value); }
		} /*WIA_IPS_YRES*/ // 0x1804


		/// <summary>
		/// Horizontal Start Position
		/// The HorizontalStartPosition property contains the x-coordinate, in pixels, of the upper-left corner of a selected image. The WIA minidriver creates and maintains this property.
		/// An application sets the HorizontalStartPosition property to mark the upper-left corner of a selection area.
		/// HorizontalStartPosition is required for all image acquisition-enabled items and child items of these items; this property is not available for storage items or stored image items.
		/// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_YEXTENT,  and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set HorizontalStartPosition to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
		/// When the origin or one extent is changed, the driver has to update WIA_IPS_PAGE_SIZE to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
		/// A driver must also update the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
		/// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_XRES, WIA_IPS_YEXTENT, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
		/// </summary>
		[WiaPropertyId(6149)]
		public int HorizontalStartPosition
		{
			get { return this.GetPropertyValue<int>(6149); }
			set { this.SetPropertyValue(6149,value); }
		} /*WIA_IPS_XPOS*/ // 0x1805


		/// <summary>
		/// Vertical Start Position
		/// The VerticalStartPosition property contains the current y-coordinate, in pixels, of the upper-left corner of a selected image. The WIA minidriver creates and maintains this property.
		/// An application sets the VerticalStartPosition property to mark the upper-left corner of a selection area.
		/// VerticalStartPosition is required for all image acquisition-enabled items and child items of these items; this property is not available for storage items or stored image items.
		/// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_YEXTENT,  and VerticalStartPosition properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and VerticalStartPosition to ((scan area height - document height) / 2) * resolution [DPI]).
		/// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE property to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
		/// A driver must also update WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and VerticalStartPosition properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
		/// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_XRES, WIA_IPS_YEXTENT, VerticalStartPosition, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
		/// </summary>
		[WiaPropertyId(6150)]
		public int VerticalStartPosition
		{
			get { return this.GetPropertyValue<int>(6150); }
			set { this.SetPropertyValue(6150,value); }
		} /*WIA_IPS_YPOS*/ // 0x1806


		/// <summary>
		/// Horizontal Extent
		/// The HorizontalExtent property contains the current width, in pixels, of a selected image to acquire. 
		/// An application sets the HorizontalExtent property to mark the upper-left corner (that is, the width) of the selection area to acquire. HorizontalExtent must agree with the WIA_IPA_PIXELS_PER_LINE property. The minidriver creates and maintains this property.
		/// HorizontalExtent is required for all image acquisition enabled items and child items of these items; this property is not available for storage items or stored image items.
		/// When a fixed page size is set, the driver has to set the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
		/// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE property to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
		/// A driver must also to update the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties when WIA_IPS_XRES and WIA_IPS_YRES are changed.
		/// Note: Flatbed and Film child items must support only the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_XRES, WIA_IPS_YEXTENT, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
		/// </summary>
		[WiaPropertyId(6151)]
		public int HorizontalExtent
		{
			get { return this.GetPropertyValue<int>(6151); }
			set { this.SetPropertyValue(6151,value); }
		} /*WIA_IPS_XEXTENT*/ // 0x1807


		/// <summary>
		/// Vertical Extent
		/// The VerticalExtent property contains the current height, in pixels, of a selected image to acquire. 
		/// An application sets the VerticalExtent property to mark the upper-left corner (that is, the height) of a selection area to acquire. VerticalExtent must agree with the value of the WIA_IPA_NUMBER_OF_LINES property. The WIA minidriver creates and maintains this property.
		/// VerticalExtent is required for all image acquisition enabled items and child items of these items; this property is not available for storage items or stored image items.
		/// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, WIA_IPS_XPOS, VerticalExtent, and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
		/// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
		/// A driver must also update the WIA_IPS_XEXTENT, WIA_IPS_XPOS, VerticalExtent, and WIA_IPS_YPOS properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
		/// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_XRES, VerticalExtent, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
		/// </summary>
		[WiaPropertyId(6152)]
		public int VerticalExtent
		{
			get { return this.GetPropertyValue<int>(6152); }
			set { this.SetPropertyValue(6152,value); }
		} /*WIA_IPS_YEXTENT*/ // 0x1808


		/// <summary>
		/// Photometric Interpretation
		/// The PhotometricInterpretation property contains the current setting for white and black pixels. The WIA minidriver creates and maintains this property.
		/// An application reads the PhotometricInterpretation property to determine the value assigned to white or black pixels (depending on what the application is doing). 
		/// If a device can be set to only a single value, create a WIA_PROP_LIST type, and place the valid value in it.
		/// The PhotometricInterpretation property is required for all image acquisition items and stored images.
		/// </summary>
		[WiaPropertyId(6153)]
		public PhotometricInterpretation PhotometricInterpretation
		{
			get { return this.GetPropertyValue<PhotometricInterpretation>(6153); }
			set { this.SetPropertyValue(6153,value); }
		} /*WIA_IPS_PHOTOMETRIC_INTERP*/ // 0x1809


		/// <summary>
		/// Brightness
		/// The Brightness property contains the current hardware brightness setting for a device.
		/// An application sets the Brightness property to the hardware's brightness value. The WIA minidriver creates and maintains this property.
		/// Values for Brightness should be mapped in a range from ˆ’1000 through 1000, where 1000 corresponds to the maximum brightness, 0 corresponds to normal brightness, and ˆ’1000 corresponds to the minimum brightness.
		/// Brightness is required for all image acquisition items.
		/// </summary>
		[WiaPropertyId(6154)]
		public int Brightness
		{
			get { return this.GetPropertyValue<int>(6154); }
			set { this.SetPropertyValue(6154,value); }
		} /*WIA_IPS_BRIGHTNESS*/ // 0x180a


		/// <summary>
		/// Contrast
		/// The Contrast property contains the current hardware contrast setting for a device.
		/// An application sets the Contrast property to the hardware's contrast value. The WIA minidriver creates and maintains this property.
		/// Values for Contrast should be mapped in a range from ˆ’1000 through 1000, where ˆ’1000 corresponds to the minimum contrast, 0 corresponds to normal contrast, and 1000 corresponds to the maximum contrast.
		/// Contrast is required for all image acquisition items.
		/// </summary>
		[WiaPropertyId(6155)]
		public int Contrast
		{
			get { return this.GetPropertyValue<int>(6155); }
			set { this.SetPropertyValue(6155,value); }
		} /*WIA_IPS_CONTRAST*/ // 0x180b


		/// <summary>
		/// Orientation
		/// The Orientation property describes the current orientation of the document to scan. The WIA minidriver creates and maintains this property. 
		/// An application sets the Orientation property to define the original orientation of a page or image to be acquired. For more information about how to use Orientation, see WIA_DPS_PAGE_SIZE.
		/// The Orientation property describes the orientation of the document to scan. This property affects the current scan frame and available page sizes. 
		/// Orientationis different from the WIA_IPS_ROTATION property, which refers to a rotation that is applied to an image after it is scanned. So, a ROT180 value for Orientation is different from a ROT180 value for WIA_IPS_ROTATION. For Orientation, ROT180 describes the orientation of the physical document to scan, relative to the scan direction, and for WIA_IPS_ROTATION, ROT180 describes the rotation to apply to an image after it is scanned.
		/// The Orientation property is required for ADF items and optional for all other image acquisition items.
		/// Note: The compatibility layer within the WIA service does not add support for Orientation to the ADF item that is translated from a Microsoft Windows XP WIA device if the property is not supported on the child item of the device. Applications should not expect that an ADF item will always support this property and should always check if Orientation is supported at run time.
		/// </summary>
		[WiaPropertyId(6156)]
		public OrientationAndRotation Orientation
		{
			get { return this.GetPropertyValue<OrientationAndRotation>(6156); }
			set { this.SetPropertyValue(6156,value); }
		} /*WIA_IPS_ORIENTATION*/ // 0x180c


		/// <summary>
		/// Rotation
		/// The Rotation property contains the current rotation setting for image rotation, if it is implemented. The WIA minidriver creates and maintains this property. 
		/// An application sets the Rotation property to inform a driver how much (if at all) to rotate an image before the driver returns it to the application. 
		/// The WIA minidriver is responsible for rotating image data before sending it back to the application. The application is responsible for checking the image headers to see the newly rotated values.
		/// It can be difficult to understand the effect of rotation on the current image's selection area (which is defined by the WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties). 
		/// Selection area refers to the selected area on the physical scanner bed that an image is be acquired from. The Rotation property does not modify the selection area. The driver applies a counterclockwise rotation according to Rotation only after the driver has acquired the appropriate selection area. Rotation does affect the dimensions of the output image, so these dimensions must be reflected in the resulting image's data header.
		/// WIA_IPS_YEXTENT is not related to WIA_IPS_ORIENTATION. WIA_IPS_ORIENTATION describes the orientation of the document to be scanned relative to the direction of the scan; in contrast, Rotation describes the rotation that is to be applied to an image after it is scanned. 
		/// WIA_IPS_ORIENTATION can impact the area to be scanned. Not all page sizes are available in both landscape and portrait, and the extents of the image from an change in WIA_IPS_ORIENTATION could crop the image. Rotation does not impact the image extents and is not related to the orientation of the document that is to be scanned.
		/// </summary>
		[WiaPropertyId(6157)]
		public OrientationAndRotation Rotation
		{
			get { return this.GetPropertyValue<OrientationAndRotation>(6157); }
			set { this.SetPropertyValue(6157,value); }
		} /*WIA_IPS_ROTATION*/ // 0x180d


		/// <summary>
		/// Mirror
		/// The Mirror property is reserved by Microsoft for future use and is not implemented at this time.
		/// The Mirror property is not related to the WIA_IPS_ORIENTATION propety. WIA_IPS_ORIENTATION specifies the orientation of the document to be scanned in relationship to the direction of the scan; in contrast, Mirror specifies an operation that is to be applied to an image after it is scanned.
		/// </summary>
		[WiaPropertyId(6158)]
		public Mirror Mirror
		{
			get { return this.GetPropertyValue<Mirror>(6158); }
		} /*WIA_IPS_MIRROR*/ // 0x180e


		/// <summary>
		/// Threshold
		/// The Threshold property contains the current hardware threshold setting for a device. The WIA minidriver creates and maintains this property.
		/// You should map values for the Threshold property in a range from 0 through 255. The default value is 128.
		/// An application sets Threshold to change the hardware threshold value. This value is valid only if the WIA_IPA_DATATYPE property is equal to WIA_DATA_THRESHOLD. If a device does not allow WIA_DATA_THRESHOLD to be changed, it should report the default value of 128.
		/// </summary>
		[WiaPropertyId(6159)]
		public int Threshold
		{
			get { return this.GetPropertyValue<int>(6159); }
			set { this.SetPropertyValue(6159,value); }
		} /*WIA_IPS_THRESHOLD*/ // 0x180f


		/// <summary>
		/// Invert
		/// The Invert property is reserved by Microsoft for future use and is not implemented at this time.
		/// The WIA_IPS_FILM_SCAN_MODE property is not related to WIA_IPS_ORIENTATION. WIA_IPS_ORIENTATION describes the orientation of the document to be scanned in relationship to the direction of the scan; in contrast, WIA_IPS_FILM_SCAN_MODE describes an operation that is to be applied to an image after it is scanned.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(6160)]
		public int Invert
		{
			get { return this.GetPropertyValue<int>(6160); }
		} /*WIA_IPS_INVERT*/ // 0x1810


		/// <summary> 
		/// Lamp Warm up Time
		/// The WarmUpTime property contains the maximum warm-up time, in milliseconds, that a device needs before starting the scanning operation. The WIA minidriver creates and maintains this property.
		/// An application can read the WarmUpTime property to determine the maximum warm-up time for a device. The application can then present a "waiting for the device to warm up" dialog box to let users know that a wait or pause might occur before anything happens. 
		/// </summary>
		[WiaPropertyId(6161)]
		public int WarmUpTime
		{
			get { return this.GetPropertyValue<int>(6161); }
		} /*WIA_IPS_WARM_UP_TIME*/ // 0x1811


		/// <summary>
		/// DeskewX
		/// The DeskewX property, together with the WIA_IPS_DESKEW_Y property, describes the upper two corners of a skewed image. The WIA minidriver creates and maintains this property. 
		/// The DeskewX and WIA_IPS_DESKEW_Y properties describe where the two upper corners of a skewed image are located within the bounding rectangle that WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties define.
		/// The valid values for DeskewX must be between 0 and (WIA_IPS_XEXTENT - 1). A value of 0 means that no skew correction should be performed.
		/// DeskewX contains the number of pixels in the x-direction from WIA_IPS_XPOS to the x-coordinate of the uppermost corner of the image to be corrected.
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(6162)]
		public int DeskewX
		{
			get { return this.GetPropertyValue<int>(6162); }
			set { this.SetPropertyValue(6162,value); }
		} /*WIA_IPS_DESKEW_X*/  // 0x1812


		/// <summary>
		/// DeskewY 
		/// The DeskewY property, together with the WIA_IPS_DESKEW_X property, describes the upper two corners of a skewed image. The WIA minidriver creates and maintains this property. 
		/// The WIA_IPS_DESKEW_X and DeskewY properties describe where the two upper corners of a skewed image are located within the bounding rectangle that the WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties define.
		/// The valid values for DeskewY must be between 0 and (WIA_IPS_YEXTENT - 1). A value of 0 means that no deskew should be performed.
		/// DeskewY contains the number of pixels in the y-direction from WIA_IPS_YPOS to the y-coordinate of the leftmost corner of the image to be deskewed.
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(6163)]
		public int DeskewY
		{
			get { return this.GetPropertyValue<int>(6163); }
			set { this.SetPropertyValue(6163,value); }
		} /*WIA_IPS_DESKEW_Y*/  // 0x1813


		/// <summary>
		/// Segmentation
		/// You must implement Segmentation  for scanner flatbed and film items if they support the creation of child items with a segmentation filter or if the driver itself creates child items for fixed frames.
		/// You can package a driver with a segmentation filter and still have Segmentation set to WIA_DONT_USE_SEGMENTATION_FILTER for one of its items (for example, the film item). This situation could occur if the scanner uses fixed frames for film scanning, but not for scanning from the flatbed.
		/// </summary>
		[WiaPropertyId(6164)]
		public SegmentationFilter Segmentation
		{
			get { return this.GetPropertyValue<SegmentationFilter>(6164); }
			set { this.SetPropertyValue(6164,value); }
		} /*WIA_IPS_SEGMENTATION*/  // 0x1814


		/// <summary>
		/// Maximum Horizontal Scan Size
		/// The MaxHorizontalSize property contains the physical horizontal dimension of a scanner's flatbed, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(6165)]
		public int MaxHorizontalSize
		{
			get { return this.GetPropertyValue<int>(6165); }
		} /*WIA_IPS_MAX_HORIZONTAL_SIZE*/  // 0x1815


		/// <summary>
		/// Maximum Vertical Scan Size
		/// The MaxVerticalSize property contains the physical vertical dimension of a scanner's flatbed, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(6166)]
		public int MaxVerticalSize
		{
			get { return this.GetPropertyValue<int>(6166); }
		} /*WIA_IPS_MAX_VERTICAL_SIZE*/  // 0x1816


		/// <summary>
		/// Minimum Horizontal Scan Size
		/// The MinHorizontalSize property contains the physical horizontal dimensions of the smallest page that a scanner's document feeder can scan, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(6167)]
		public int MinHorizontalSize
		{
			get { return this.GetPropertyValue<int>(6167); }
		} /*WIA_IPS_MIN_HORIZONTAL_SIZE*/  // 0x1817


		/// <summary>
		/// Minimum Vertical Scan Size
		/// The MinVerticalSize property contains the physical vertical dimensions of the smallest page that a scanner's document feeder can scan, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(6168)]
		public int MinVerticalSize
		{
			get { return this.GetPropertyValue<int>(6168); }
		} /*WIA_IPS_MIN_VERTICAL_SIZE*/  // 0x1818


		/// <summary>
		/// Transfer Capabilities
		/// The TransferCapabilities property indicates if a device can transfer parent and child items together. The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(6169)]
		public TransferCapabilities TransferCapabilities
		{
			get { return this.GetPropertyValue<TransferCapabilities>(6169); }
		} /*WIA_IPS_TRANSFER_CAPABILITIES*/  // 0x1819


		/// <summary>
		/// Sheet Feeder Registration
		/// The SheetFeederRegistration property contains the registration, or alignment and edge detection, for documents that are placed on the flatbed of a scanner. The WIA minidriver creates and maintains this property. 
		/// The SheetFeederRegistration property indicates how a document is horizontally positioned on the scanning head of a handheld or sheet-fed scanner. You can use SheetFeederRegistration to predict where across the scanning head a document is placed.
		/// For scanners that support more than one scanning head, the SheetFeederRegistration property is relative to the topmost scanning head. This property is mandatory for sheet-fed, scroll-fed, and handheld scanners.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_SHEET_FEEDER_REGISTRATION property instead.
		/// </summary>
		[WiaPropertyId(3078)]
		public HorizontalRegistration SheetFeederRegistration
		{
			get { return this.GetPropertyValue<HorizontalRegistration>(3078); }
		} /*WIA_IPS_SHEET_FEEDER_REGISTRATION*/  // 0xc06


		/// <summary>
		/// Document Handling Select
		/// The DocumentHandlingSelect property contains the current scanner acquisition source and mode.
		/// An application reads the DocumentHandlingSelect property to determine the current acquisition source of a scanner, or the application writes this property to set the source and mode of the scanner. In addition, applications use this property to enable and disable duplexer functionality. The WIA minidriver creates and maintains this property.
		/// The values DUPLEX and FRONT_ONLY are mutually exclusive - set one or the other, but not both.
		/// A WIA 2.0 minidriver must set the initial value of this property to its default value, FRONT_ONLY. Failure to observe this requirement might make the minidriver incompatible with the WIA 1.0 common scan dialog and with some WIA 1.0 applications.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the identical WIA_DPS_DOCUMENT_HANDLING_SELECT property.
		/// </summary>
		[WiaPropertyId(3088)]
		public DocumentHandlingSelect DocumentHandlingSelect
		{
			get { return this.GetPropertyValue<DocumentHandlingSelect>(3088); }
			set { this.SetPropertyValue(3088,value); }
		} /*WIA_IPS_DOCUMENT_HANDLING_SELECT*/  // 0xc10


		/// <summary>
		/// Horizontal Optical Resolution
		/// The HorizontalOpticalResolution property contains the highest-supported horizontal optical resolution of a device, in dots per inch (dpi). The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_OPTICAL_XRES property instead.
		/// </summary>
		[WiaPropertyId(3090)]
		public int HorizontalOpticalResolution
		{
			get { return this.GetPropertyValue<int>(3090); }
		} /*WIA_IPS_OPTICAL_XRES*/  // 0xc12


		/// <summary>
		/// Vertical Optical Resolution
		/// The VerticalOpticalResolution property contains the highest-supported vertical optical resolution of a device, in dots per inch (dpi). The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_OPTICAL_YRES property instead.
		/// </summary>
		[WiaPropertyId(3091)]
		public int VerticalOpticalResolution
		{
			get { return this.GetPropertyValue<int>(3091); }
		} /*WIA_IPS_OPTICAL_YRES*/  // 0xc13


		/// <summary>
		/// Pages
		/// The Pages property contains the current number of pages to acquire from an automatic document feeder.
		/// An application reads Pages to determine a document feeder's page capacity. The application sets this property to the maximum number of pages it is willing to scan in the current WIA session. The WIA minidriver creates and maintains this property.
		/// Note: If duplex mode is enabled (that is, if WIA_IPS_DOCUMENT_HANDLING_SELECT is set to FEEDER | DUPLEX), Pages is still equal to the number of pages to scan.One sheet of paper will automatically contain two pages if DUPLEX is enabled, even if the back side of the page is blank.
		/// If you set Pages to 1, the scanner will process one of the sides of the page. We recommend that, if a scanner is unable to scan only one side of a page while in duplex mode, you should change the Pages value for the Inc member of the WIA_PROPERTY_INFO structure to 2. With this value, the application must request pages in multiples of two. A value of zero means that all pages that are currently loaded into the document feeder are to be scanned.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGES property instead.
		/// </summary>
		[WiaPropertyId(3096)]
		public int Pages
		{
			get { return this.GetPropertyValue<int>(3096); }
			set { this.SetPropertyValue(3096,value); }
		} /*WIA_IPS_PAGES*/  // 0xc18


		/// <summary>
		/// Page Size
		/// The PageSize property contains the size of the page that is currently selected to be scanned. 
		/// To select the dimensions of the page to scan, an application sets the PageSize property. The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGE_SIZE property instead.
		/// </summary>
		[WiaPropertyId(3097)]
		public PageSize PageSize
		{
			get { return this.GetPropertyValue<PageSize>(3097); }
			set { this.SetPropertyValue(3097,value); }
		} /*WIA_IPS_PAGE_SIZE*/  // 0xc19


		/// <summary>
		/// Page Width
		/// The PageWidth property contains the width of the current page selected, in thousandths of an inch (.001). The WIA minidriver creates and maintains this property.
		/// An application reads PageWidth to determine the physical dimensions of the page that is being scanned. If the extent settings are different from known page sizes, this property reports the width of the page whose WIA_IPS_PAGE_SIZE property is set to WIA_PAGE_CUSTOM.
		/// PageWidth must be in sync with the value of WIA_IPS_XEXTENT, which reports the width, in pixels, of the page to be scanned.
		/// Note: The compatibility layer within the WIA service does not add support for the PageWidth property to the ADF item that is translated from a Windows XP WIA device if the property is not supported on the child item of the device. Applications should not expect an ADF item to always support PageWidth and should always check if it is supported at run time. (Typically, applications should check the support for any property to be negotiated.)
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGE_WIDTH property instead.
		/// </summary>
		[WiaPropertyId(3098)]
		public int PageWidth
		{
			get { return this.GetPropertyValue<int>(3098); }
		} /*WIA_IPS_PAGE_WIDTH*/  // 0xc1a


		/// <summary>
		/// Page Height
		/// The PageHeight property contains the height, in thousandths of an inch (.001), of the currently selected page. The WIA minidriver creates and maintains this property.
		/// An application reads PageHeight to determine the physical dimensions of the page that is being scanned. If the extent settings are different from the known page sizes, this property reports the height of the page whose WIA_IPS_PAGE_SIZE property is set to WIA_PAGE_CUSTOM.
		/// PageHeight must provide a measurement in thousandths of an inch that is equivalent to the pixel value that is reported by WIA_IPS_YEXTENT, which reports the height, in pixels, of the page to be scanned.
		/// Note: The compatibility layer within the WIA service does not add support for the PageHeight property to the ADF item that is translated from a Windows XP WIA device if the property is not supported on the child item of the device. Applications should not expect that an ADF item will always support PageHeight and should always check if it is supported at run time. (Applications should typically check for this support for any property that is be negotiated.)
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGE_HEIGHT property instead.
		/// </summary>
		[WiaPropertyId(3099)]
		public int PageHeight
		{
			get { return this.GetPropertyValue<int>(3099); }
		} /*WIA_IPS_PAGE_HEIGHT*/  // 0xc1b


		/// <summary>
		/// Preview
		/// The Preview property indicates the preview mode for a device. 
		/// An application sets Preview to place a device into a preview mode. 
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PREVIEW property instead.
		/// </summary>
		[WiaPropertyId(3100)]
		public Preview Preview
		{
			get { return this.GetPropertyValue<Preview>(3100); }
			set { this.SetPropertyValue(3100,value); }
		} /*WIA_IPS_PREVIEW*/  // 0xc1c


		/// <summary>
		/// Show preview control
		/// The ShowPreviewControl property indicates whether an item needs a preview control displayed to a user. The WIA minidriver creates and maintains this property.
		/// You can use the ShowPreviewControl property to help control devices that cannot preview. For example, some feeder-driven devices cannot reload the paper for a preview scan.
		/// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_SHOW_PREVIEW_CONTROL property instead.
		/// </summary>
		[WiaPropertyId(3103)]
		public ShowPreviewControl ShowPreviewControl
		{
			get { return this.GetPropertyValue<ShowPreviewControl>(3103); }
		} /*WIA_IPS_SHOW_PREVIEW_CONTROL*/  // 0xc1f


		/// <summary>
		/// Film Scan Mode
		/// The FilmScanMode property contains the current film scan configuration settings. The WIA minidriver creates and maintains this property. 
		/// This property is required for the root item in the WIA item tree of film scanners and transparency adapters.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(3104)]
		public FilmScanMode FilmScanMode
		{
			get { return this.GetPropertyValue<FilmScanMode>(3104); }
			set { this.SetPropertyValue(3104,value); }
		} /*WIA_IPS_FILM_SCAN_MODE*/  // 0xc20


		/// <summary>
		/// Lamp
		/// The Lamp property contains the current configuration setting for a scanner's lamp. The WIA minidriver creates and maintains this property. 
		/// The Lamp property enables the programmatic control of the scanner lamp; this lamp could be a dedicated lamp (for a transparency adapter) or the main scanner lamp (for dedicated film scanners).
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(3105)]
		public Lamp Lamp
		{
			get { return this.GetPropertyValue<Lamp>(3105); }
			set { this.SetPropertyValue(3105,value); }
		} /*WIA_IPS_LAMP*/  // 0xc21


		/// <summary>
		/// Lamp Auto Off
		/// The AutoOff property contains the current configuration setting for automatically shutting off a scanner's lamp. The WIA minidriver creates and maintains this property. 
		/// The AutoOff property enables the programmatic control of how long a lamp will be kept on when a scanner is not in use; this lamp could be a dedicated lamp (for a transparency adapter) or the main scanner lamp (for dedicated film scanners).
		/// You should implement AutoOff only if the device supports an automatic lamp-off feature.
		/// The valid values for AutoOff range from 0 through 4095 seconds.
		/// Versions: Available in Windows Vista and later operating systems. 
		/// </summary>
		[WiaPropertyId(3106)]
		public int AutoOff
		{
			get { return this.GetPropertyValue<int>(3106); }
			set { this.SetPropertyValue(3106,value); }
		} /*WIA_IPS_LAMP_AUTO_OFF*/  // 0xc22


		/// <summary>
		/// Automatic Deskew
		/// The AutoDeskew property indicates if a device should use automatic skew correction. The WIA minidriver creates and maintains this property.
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(3107)]
		public AutoDeskew AutoDeskew
		{
			get { return this.GetPropertyValue<AutoDeskew>(3107); }
			set { this.SetPropertyValue(3107,value); }
		} /*WIA_IPS_AUTO_DESKEW*/  // 0xc23


		/// <summary>
		/// Supports Child Item Creation 
		/// The SupportsChildItemCreation property indicates if a device supports the creation of child items. The WIA minidriver creates and maintains this property
		/// Items that support the WIA_IPS_SEGMENTATION property and the WIA_USE_SEGMENTATION_FILTER value must also support the SupportsChildItemCreation property and have it set to TRUE.
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(3108)]
		public int SupportsChildItemCreation
		{
			get { return this.GetPropertyValue<int>(3108); }
		} /*WIA_IPS_SUPPORTS_CHILD_ITEM_CREATION*/  // 0xc24


		/// <summary>
		/// Horizontal Scaling
		/// The HorizontalScaling property indicates if scaling along the x-axis should be applied to a scan. The WIA minidriver creates and maintains this property.
		/// Valid values for the HorizontalScaling property range from 1 through 65535.
		/// HorizontalScaling indicates only scaling along the x-axis. If you want to scale an image uniformly, you must set a similar value in HorizontalScaling and in the WIA_IPS_YSCALING property.
		/// Consider the following examples:
		/// 100, no scaling (1x, 100%). The image is not changed.
		/// 050, 1/2 scaling (1/2x, 50%). The image size is reduced along the x-axis by 50% (1/2 the original size).
		/// 200, 2x scaling (200%). The image size is enlarged along the x-axis by 200% (double).
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(3109)]
		public int HorizontalScaling
		{
			get { return this.GetPropertyValue<int>(3109); }
			set { this.SetPropertyValue(3109,value); }
		} /*WIA_IPS_XSCALING*/  // 0xc25


		/// <summary>
		/// Vertical Scaling
		/// The VerticalScaling property indicates if scaling along the y-axis should be applied to a scan. The WIA minidriver creates and maintains this property.
		/// Valid values for the VerticalScaling property range from 1 through 65535.
		/// VerticalScaling indicates only scaling along the y-axis. If you want to scale an image uniformly, you must set a similar value in VerticalScaling and in the WIA_IPS_XSCALING property.
		/// Consider the following examples:
		/// 100, no scaling (1x, 100%). The image is not changed.
		/// 050, 1/2 scaling (1/2x, 50%). The image size is reduced along the y-axis by 50% (1/2 the original size).
		/// 200, 2x scaling (200%). The image size is enlarged along the y-axis by 200% (double).
		/// Versions: Available in Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(3110)]
		public int VerticalScaling
		{
			get { return this.GetPropertyValue<int>(3110); }
			set { this.SetPropertyValue(3110,value); }
		} /*WIA_IPS_YSCALING*/  // 0xc26


		/// <summary>
		/// Preview Type
		/// The PreviewType property indicates if WIA_IPA_DATATYPE and WIA_IPA_DEPTH are changed, without having to request a new preview scan. The WIA minidriver creates and maintains this property.
		/// Note: PreviewType should describe only the WIA_IPA_DATATYPE and WIA_IPA_DEPTH properties.
		/// </summary>
		[WiaPropertyId(3111)]
		public PreviewType PreviewType
		{
			get { return this.GetPropertyValue<PreviewType>(3111); }
		} /*WIA_IPS_PREVIEW_TYPE*/  // 0xc27


		/// <summary>
		/// Film Node Name
		/// Enables specification of a particular film scanning attachment when there is more than one.
		/// This property is required for the WIA_CATEGORY_FILM items when there are multiple film scan items. If the device supports only one root scanner film item then this property is optional. 
		/// Note: This property is supported only by Windows Vista and later.
		/// </summary>
		[WiaPropertyId(4129)]
		public string FilmNodeName
		{
			get { return this.GetPropertyValue<string>(4129); }
			set { this.SetPropertyValue(4129,value); }
		} /*WIA_IPS_FILM_NODE_NAME*/  // 0x1021
	}
}