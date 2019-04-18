using System;
using WIA;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Eigenschaften bereit, um einfach auf die Bild-Properties eines WIA-Gerätes zugreifen zu können
	/// </summary>
	public class PictureSettings : WiaSettings
	{
		/// <summary>
		/// Erstellt eine neue Instanz zum vereinfachten Zugriff auf die Bild-Eigenschaften eines WIA-Gerätes
		/// </summary>
		/// <param name="properties">die Eigenschaften des WIA-Geräts</param>
		public PictureSettings(IProperties properties)
			: base(properties)
		{ }



		/// <summary> 
		/// Item Name 
		/// The ItemName property contains a WIA item name. 
		/// The item name is the same as the item name that is specified in a call to the wiasCreateDrvItem service utility function. 
		/// An application reads the ItemName property to determine which item it is currently using. Each item must have a unique name. The WIA service creates and maintains ItemName.
		/// </summary>
		[WiaPropertyId(4098)]
		public string ItemName
		{
			get { return this.GetPropertyValue<string>(4098); }
		} /*WIA_IPA_ITEM_NAME*/ // 0x1002


		/// <summary>
		/// Full Item Name
		/// The FullItemName property contains the full item name (the item name with path information). 
		/// The full item name is the same as the bstrFullItemName parameter of the wiasCreateDrvItem service utility function. An application reads the FullItemName property to determine which item it is currently using and where that item is located in the WIA item tree. Each item should have a unique name. Applications commonly use the full item name to search for items in the WIA item tree. The WIA service creates and maintains FullItemName.
		/// An application reads FullItemName to determine the format of the image that it is about to receive. An application writes this property to set the format. FullItemName depends on the WIA_IPA_TYMED property. The WIA minidriver creates and maintains FullItemName.
		/// </summary>
		[WiaPropertyId(4099)]
		public string FullItemName
		{
			get { return this.GetPropertyValue<string>(4099); }
		} /*WIA_IPA_FULL_ITEM_NAME*/ // 0x1003


		/// <summary> 
		/// Item Time Stamp
		/// The ItemTime property contains the time that an image was originally captured.
		/// </summary>
		[WiaPropertyId(4100)]
		public UInt16 ItemTime
		{
			get { return this.GetPropertyValue<UInt16>(4100); }
			set { this.SetPropertyValue(4100,value); }
		} /*WIA_IPA_ITEM_TIME*/ // 0x1004


		/// <summary>
		/// Item Flags
		/// The ItemFlags property contains the descriptive flags for a WIA item. 
		/// The WIA item flags are the same as those in the lObjectFlags parameter of the wiasCreateDrvItem service utility function. The WIA service creates and maintains the ItemFlags property.
		/// An application reads ItemFlags to determine a WIA item's descriptive flag values. 
		/// </summary>
		[WiaPropertyId(4101)]
		public WiaItemFlag ItemFlags
		{
			get { return this.GetPropertyValue<WiaItemFlag>(4101); }
		} /*WIA_IPA_ITEM_FLAGS*/ // 0x1005


		/// <summary>
		/// Access Rights
		/// The AccessRights property contains the access rights for a WIA item. 
		/// Access rights control the ability of an application to delete items in the WIA item tree. The WIA minidriver creates and maintains the AccessRights property.
		/// </summary>
		[WiaPropertyId(4102)]
		public AccessRights AccessRights
		{
			get { return this.GetPropertyValue<AccessRights>(4102); }
			set { this.SetPropertyValue(4102,value); }
		} /*WIA_IPA_ACCESS_RIGHTS*/ // 0x1006


		/// <summary>
		/// Data Type
		/// The DataType property contains the current data type setting for a device. A WIA minidriver creates and maintains this property.
		/// An application reads the DataType property to determine the data type of an image. The application writes this property to set the current data type of the image that is about to be transferred. 
		/// The DataType property usually contains a single value for cameras.
		/// </summary>
		[WiaPropertyId(4103)]
		public DataType DataType
		{
			get { return this.GetPropertyValue<DataType>(4103); }
			set { this.SetPropertyValue(4103,value); }
		} /*WIA_IPA_DATATYPE*/ // 0x1007


		/// <summary>
		/// Bits Per Pixel
		/// The Depth property contains the bit depth setting of an image. The WIA minidriver creates and maintains this property.
		/// An application reads the Depth property to determine the bit depth setting of an image. The application might also set this value to the desired bit depth. 
		/// If you can set the device to only a single value, create a WIA_PROP_LIST type and place the valid value in it.
		/// </summary>
		[WiaPropertyId(4104)]
		public int Depth
		{
			get { return this.GetPropertyValue<int>(4104); }
			set { this.SetPropertyValue(4104,value); }
		} /*WIA_IPA_DEPTH*/ // 0x1008


		/// <summary>
		/// Preferred Format
		/// The PreferredFormat property contains the preferred format for images that the WIA minidriver transfers. The minidriver creates and maintains this property.
		/// </summary>
		[WiaPropertyId(4105)]
		public string PreferredFormat
		{
			get { return this.GetPropertyValue<string>(4105); }
		} /*WIA_IPA_PREFERRED_FORMAT*/ // 0x1009


		/// <summary>
		/// Format
		/// The Format property contains the current format of the image that is about to be transferred. The WIA minidriver creates and maintains this property.
		/// If you can set the device to only a single value, create a WIA_PROP_LIST type, and place the valid value in it.
		/// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is WiaImgFmt_BMP.
		/// </summary>
		[WiaPropertyId(4106)]
		public string Format
		{
			get { return this.GetPropertyValue<string>(4106); }
			set { this.SetPropertyValue(4106,value); }
		} /*WIA_IPA_FORMAT*/ // 0x100a


		/// <summary>
		/// Compression
		/// The Compression property contains the current compression type that is used. The WIA minidriver creates and maintains this property.
		/// An application reads the Compression property to determine the image compression type, or the application sets this property to configure the compression setting.
		/// Note: When the file format is WiaImgFmt_XPS or WiaImgFmt_PDFA, WIA_COMPRESSION_NONE means €œnot defined€; the device cannot choose the internal compression (if any) for images that are stored in these two document formats.
		/// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is WIA_COMPRESSION_NONE.
		/// The access rights of the Compression property are read/write for all image acquisitions but read-only for stored image items.
		/// </summary>
		[WiaPropertyId(4107)]
		public Compression Compression
		{
			get { return this.GetPropertyValue<Compression>(4107); }
			set { this.SetPropertyValue(4107,value); }
		} /*WIA_IPA_COMPRESSION*/ // 0x100b


		/// <summary>
		/// Media Type
		/// The MediaType property contains the method setting for image transfer . The WIA minidriver creates and maintains this property.
		/// An application reads the MediaType property to determine the minidriver's method of data transfer.
		/// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is TYMED_FILE.
		/// </summary>
		[WiaPropertyId(4108)]
		public MediaType MediaType
		{
			get { return this.GetPropertyValue<MediaType>(4108); }
			set { this.SetPropertyValue(4108,value); }
		} /*WIA_IPA_TYMED*/ // 0x100c



		/// <summary> 
		/// Channels Per Pixel
		/// The ChannelsPerPixel property contains the number of channels per pixel for an image. The WIA minidriver creates and maintains this property.
		/// </summary>
		[WiaPropertyId(4109)]
		public int ChannelsPerPixel
		{
			get { return this.GetPropertyValue<int>(4109); }
		} /*WIA_IPA_CHANNELS_PER_PIXEL*/ // 0x100d


		/// <summary>
		/// Bits Per Channel
		/// The BitsPerChannel property contains the number of bits per channel for the image. The WIA minidriver creates and maintains this property.
		/// The BitsPerChannel property is similar to the WIA_IPA_RAW_BITS_PER_CHANNEL property (which is used for the raw formats).
		/// </summary>
		[WiaPropertyId(4110)]
		public int BitsPerChannel
		{
			get { return this.GetPropertyValue<int>(4110); }
		} /*WIA_IPA_BITS_PER_CHANNEL*/ // 0x100e


		/// <summary>
		/// Planar
		/// The Planar property contains image data packing options. The WIA minidriver creates and maintains this property.
		/// An application reads Planar to determine the image packing options or sets the current image packing options. 
		/// If a device can be set to only a single value, you can implement the Planar property as WIA_PROP_NONE and read-only.
		/// Versions: Obsolete in Windows Vista and later operating system.
		/// </summary>
		[WiaPropertyId(4111)]
		public Planar Planar
		{
			get { return this.GetPropertyValue<Planar>(4111); }
			set { this.SetPropertyValue(4111,value); }
		} /*WIA_IPA_PLANAR*/ // 0x100f


		/// <summary>
		/// Pixels Per Line
		/// The PixelsPerLine property contains the number of pixels in each line of an image (that is, the width of the image, in pixels). The WIA minidriver creates and maintains this property.
		/// The PixelsPerLine property is optional for Windows Vista drivers for all transfer-enabled items. If PixelsPerLine, WIA_IPA_BYTES_PER_LINE, and WIA_IPA_NUMBER_OF_LINES are implemented, applications written for Windows Server 2003, Windows XP, and previous Windows versions can estimate the number of pixels per line, the number of bytes that are required for each scan line, and the total number of scan lines in the image. These values are not accurate because the image processing filter might modify the actual values, which PixelsPerLine, WIA_IPA_BYTES_PER_LINE, and WIA_IPA_NUMBER_OF_LINES represent.
		/// If Windows Vista driver does not supply these properties, the compatibility layer in the WIA service will add these properties. When the WIA service adds these properties, they will be updated by using the WIA_IPA_DEPTH, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties.
		/// Windows Vista applications should always parse the image header data to get information about the image that is more accurate than the information that is available from the preceding properties.
		/// Versions: Optional for Windows Vista drivers for all transfer-enabled items.
		/// </summary>
		[WiaPropertyId(4112)]
		public int PixelsPerLine
		{
			get { return this.GetPropertyValue<int>(4112); }
		} /*WIA_IPA_PIXELS_PER_LINE*/ // 0x1010


		/// <summary>
		/// Bytes Per Line
		/// The BytesPerLine property contains the number of bytes in one scan line of an image. The WIA minidriver creates and maintains this property.
		/// The BytesPerLine property is optional for Windows Vista drivers for all transfer-enabled items. If this property, together with the WIA_IPA_NUMBER_OF_LINES and WIA_IPA_PIXELS_PER_LINE properties are implemented, applications designed for Windows Server 2003, Windows XP, and previous versions of Windows can estimate the number of pixels for each line, the number of bytes that are required for each scan line, and the total number of scan lines in the image. These values are not accurate because the image processing filter might modify the actual values that these properties represent.
		/// If the Windows Vista driver does not supply these properties, the compatibility layer in a WIA service will add these properties. When the WIA service adds these properties, they will be updated by using the WIA_IPA_DEPTH, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties.
		/// Windows Vista applications should always parse the image header data to get more accurate information on the image then is available from these properties.
		/// Versions: Optional for Windows Vista drivers for all transfer-enabled items.
		/// </summary>
		[WiaPropertyId(4113)]
		public int BytesPerLine
		{
			get { return this.GetPropertyValue<int>(4113); }
		} /*WIA_IPA_BYTES_PER_LINE*/ // 0x1011


		/// <summary>
		/// Number of Lines
		/// The NumberOfLines property contains the number of lines that are contained in an image (that is, the vertical height of the image, in pixels). The WIA minidriver creates and maintains this property.
		/// The NumberOfLines property is optional for Windows Vista drivers for all transfer-enabled items. If NumberOfLines, WIA_IPA_BYTES_PER_LINE, and WIA_IPA_PIXELS_PER_LINE are implemented,  applications written for Windows Server 2003, Windows XP, and previous Windows versions can estimate the number of pixels per line, the number of bytes that are required for each scan line, and the total number of scan lines in the image. These values are not accurate because the image processing filter might modify the actual values, which NumberOfLines, WIA_IPA_BYTES_PER_LINE, and WIA_IPA_PIXELS_PER_LINE represent.
		/// If a Windows Vista driver does not supply these properties, the compatibility layer in the WIA service will add these properties. When the WIA service adds these properties, they will be updated by using the WIA_IPA_DEPTH, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties.
		/// Windows Vista applications should always parse the image header data to get information about the image that is more accurate than the information that is available from the preceding properties.
		/// Versions: Optional for Windows Vista drivers for all transfer-enabled items.
		/// </summary>
		[WiaPropertyId(4114)]
		public int NumberOfLines
		{
			get { return this.GetPropertyValue<int>(4114); }
		} /*WIA_IPA_NUMBER_OF_LINES*/ // 0x1012


		/// <summary>
		/// Gamma Curves
		/// The GammaCurves property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(4115)]
		public int GammaCurves
		{
			get { return this.GetPropertyValue<int>(4115); }
		} /*WIA_IPA_GAMMA_CURVES*/ // 0x1013


		/// <summary>
		/// Item Size
		/// The ItemSize property contains the current size, in bytes, of the data that is associated with a WIA item. The WIA minidriver creates and maintains this property.
		/// The value that the ItemSize property contains is the total size of the data that is being transferred. If this value is zero, the WIA minidriver has no information about the exact size of the data. (This situation is common for compressed data.) 
		/// An application reads ItemSize to determine the size of the data before it is transferred. The WIA service reads this property to assist in allocating memory for data transfers. For more information about data transfers, see Transferring Data to a WIA Application. 
		/// If ItemSize is set to zero and TYMED is configured for a file transfer, the WIA service does not allocate any memory for the WIA minidriver.
		/// Note: In Windows Vista and later versions of the operating system only set the ItemSize property to 0 for the ADF item when automatic document size detection is enabled.
		/// </summary>
		[WiaPropertyId(4116)]
		public int ItemSize
		{
			get { return this.GetPropertyValue<int>(4116); }
		} /*WIA_IPA_ITEM_SIZE*/ // 0x1014


		/// <summary>
		/// Color Profiles
		/// The ColorProfile property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(4117)]
		public int ColorProfile
		{
			get { return this.GetPropertyValue<int>(4117); }
		} /*WIA_IPA_COLOR_PROFILE*/ // 0x1015


		/// <summary>
		/// Buffer Size
		/// The MinBufferSize property specifies the minimum buffer size that is used in data transfers.
		/// If a data transfer is performed through a callback mechanism, the MinBufferSize property value can be as small as 64 KB. However, if the transfer is to file, the property value is the number of bytes that are needed to transfer one page of data at a time. The WIA minidriver creates and maintains this WIA property. 
		/// MinBufferSize is identical to the WIA_IPA_BUFFER_SIZE property.
		/// An application can read MinBufferSize to determine the driver-specified buffer size for data transfers. The WIA service also reads this property to allocate memory for the minidriver during the data transfer.
		/// Note: The value that the MinBufferSize property contains is the minimum amount of data that an application can request at any given time. The larger the buffer size, the larger the requests to the device will be. This larger buffer size can make the device appear slow and unresponsive, can slow the overall computer performance, and can consume excessive resources. Buffer sizes that are too small can slow performance of the data transfer by requiring many smaller requests. Choose a reasonable buffer size by considering the typical size of a data request to your device, the number of requests, and the size of those requests.
		/// Versions: Optional for Windows Vista drivers for all transfer-enabled items. If this property is implemented, applications written for Windows Server 2003, Windows XP, and previous Windows versions can estimate the transfer buffer size, and, therefore, the transfer rate will be optimal.
		/// </summary>
		[WiaPropertyId(4118)]
		public int MinBufferSize
		{
			get { return this.GetPropertyValue<int>(4118); }
		} /*WIA_IPA_MIN_BUFFER_SIZE*/ // 0x1016


		/// <summary>
		/// Buffer Size
		/// The BufferSize property contains the size of the buffer, in bytes, that is used during a data transfer. The WIA minidriver creates and maintains this property. 
		/// VT_I4	Read-only
		/// The BufferSize property is identical to the WIA_IPA_MIN_BUFFER_SIZE property.
		/// An application can read BufferSize to determine the driver-specified buffer size for data transfers. The WIA service also reads this property to allocate memory for the minidriver during the data transfer.
		/// Note: The value that the BufferSize property contains is the minimum amount of data that an application can request at any given time. The larger the buffer size, the larger the requests to the device will be. This larger buffer size can make the device seem slow and unresponsive, can slow the overall computer performance, and can consume excessive resources. Buffer sizes that are too small can slow performance of the data transfer by requiring many smaller requests. Choose a reasonable buffer size by considering the typical size of a data request to your device, the number of requests, and the size of those requests.
		/// Versions: Optional for Windows Vista drivers for all transfer-enabled items. If you implement this property, applications that are designed for Windows Server 2003, Windows XP, and previous versions of Windows can estimate the transfer buffer size and, therefore, the transfer rate will be optimal.
		/// </summary>
		[WiaPropertyId(4118)]
		public int BufferSize
		{
			get { return this.GetPropertyValue<int>(4118); }
		} /*WIA_IPA_BUFFER_SIZE*/ // 0x1016


		/// <summary>
		/// Region Type
		/// The RegionType property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(4119)]
		public int RegionType
		{
			get { return this.GetPropertyValue<int>(4119); }
		} /*WIA_IPA_REGION_TYPE*/ // 0x1017


		/// <summary>
		/// Color Profile Name
		/// The IcmProfileName property contains the image color management (ICM) profile name that is needed to properly decode an image. 
		/// An application reads the IcmProfileName property to determine the ICM profile to use when processing the image. The WIA service creates and maintains this property based on the ICMProfiles entry in the driver installation file.
		/// </summary>
		[WiaPropertyId(4120)]
		public string IcmProfileName
		{
			get { return this.GetPropertyValue<string>(4120); }
			set { this.SetPropertyValue(4120,value); }
		} /*WIA_IPA_ICM_PROFILE_NAME*/ // 0x1018


		/// <summary>
		/// Application Applies Color Mapping
		/// The AppColorMapping property is reserved by Microsoft for future use and is not implemented at this time.
		/// </summary>
		[WiaPropertyId(4121)]
		public int AppColorMapping
		{
			get { return this.GetPropertyValue<int>(4121); }
		} /*WIA_IPA_APP_COLOR_MAPPING*/ // 0x1019


		/// <summary>
		/// Stream Compatibility ID 
		/// The PropStreamCompatID property specifies a class identifier (CLSID) that represents a set of device property values. 
		/// If a device driver implements the PropStreamCompatID property, applications use this property to determine whether the device supports a set of values.
		/// </summary>
		[WiaPropertyId(4122)]
		public string StreamCompatibilityID
		{
			get { return this.GetPropertyValue<string>(4122); }
		} /*WIA_IPA_PROP_STREAM_COMPAT_ID*/ // 0x101a


		/// <summary>
		/// Filename extension
		/// The FilenameExtension property contains the file name extension for a particular file format. The WIA minidriver creates and maintains this property.
		/// The minidriver updates the FilenameExtension property to reflect the current value of the WIA_IPA_FORMAT property.
		/// For example, if WIA_IPA_FORMAT is WiaImgFmt_JPEG, FilenameExtension should be "jpg". If WIA_IPA_FORMAT is WiaImgFmt_BMP, FilenameExtension should be "bmp". Note that the file name extension does not include the period (".").
		/// The FilenameExtension property is recommended for drivers that support standard formats and is required for drivers that implement custom-defined formats. FilenameExtension informs the application of the correct file name extension to use during the transfer of privately formatted files. For example, if the A. Datum Corporation created a WIA driver that transferred a file in a new format, the company could specify an extension of "adc". This extension enables applications to transfer data in that format to a file and to create a file name such as Myfile.adc, which is useful to others who understand the new extension. 
		/// </summary>
		[WiaPropertyId(4123)]
		public string FilenameExtension
		{
			get { return this.GetPropertyValue<string>(4123); }
		} /*WIA_IPA_FILENAME_EXTENSION*/ // 0x101b


		/// <summary>
		/// Suppress a property page
		/// The SuppressPropertyPage property specifies whether to suppress the general property pages for items on a device.
		/// Versions: Available on Microsoft Windows XP and later operating systems.
		/// </summary>
		[WiaPropertyId(4124)]
		public SuppressPropertyPage SuppressPropertyPage
		{
			get { return this.GetPropertyValue<SuppressPropertyPage>(4124); }
		} /*WIA_IPA_SUPPRESS_PROPERTY_PAGE*/ // 0x101c


		/// <summary>
		/// Item Category
		/// The ItemCategory property contains grouped categories for WIA items. 
		/// The WIA service creates and maintains this property.
		/// Versions: Available in Windows Vista and later versions of the operating system. 
		/// </summary>
		[WiaPropertyId(4125)]
		public string ItemCategory
		{
			get { return this.GetPropertyValue<string>(4125); }
		} /*WIA_IPA_ITEM_CATEGORY*/  // 0x101d


		/// <summary>
		/// Upload Item Size
		/// The UploadItemSize property is used by applications to specify the number of bytes to upload for an item. The application creates and maintains this property.
		/// Versions: Available on Windows Vista and later operating systems.
		/// </summary>
		[WiaPropertyId(4126)]
		public int UploadItemSize
		{
			get { return this.GetPropertyValue<int>(4126); }
			set { this.SetPropertyValue(4126,value); }
		} /*WIA_IPA_UPLOAD_ITEM_SIZE*/  // 0x101e


		/// <summary>
		/// Items Stored
		/// The ItemsStored property specifies how many items are stored in the storage (WIA_CATEGORY_FOLDER) item. The WIA minidriver creates and maintains this WIA property. 
		/// Versions: Available in Windows Vista and later versions of the operating system. 
		/// </summary>
		[WiaPropertyId(4127)]
		public int ItemsStored
		{
			get { return this.GetPropertyValue<int>(4127); }
		} /*WIA_IPA_ITEMS_STORED*/  // 0x101f


		/// <summary>
		/// Raw Bits Per Channel
		/// The RawBitsPerChannel property contains the number of bits in each color channel. 
		/// The RawBitsPerChannel property should be reported as a vector that contains as many byte values as there are channels, where the first byte corresponds to the number of bits in the first channel, the second byte to the number of bits in the second channel, and so on. The vector must contain as many entries as the WIA_IPA_CHANNELS_PER_PIXEL property reports there are channels. The driver sets WIA_IPA_CHANNELS PER_PIXEL when the application sets WIA_IPA_FORMAT to WiaImgFmt_RAW.
		/// RawBitsPerChannel is similar to the WIA_IPA_BITS_PER_CHANNEL property (which is used for formats other than RAW).
		/// </summary>
		[WiaPropertyId(4128)]
		public UInt16 RawBitsPerChannel
		{
			get { return this.GetPropertyValue<UInt16>(4128); }
		} /*WIA_IPA_RAW_BITS_PER_CHANNEL*/  // 0x1020
	}
}