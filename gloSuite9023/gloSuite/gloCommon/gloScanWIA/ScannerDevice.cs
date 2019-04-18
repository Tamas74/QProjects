using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WIA;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Methoden Bereit, um auf ein WIA-Scanner-Gerät zuzugreifen
	/// </summary>
	public class ScannerDevice
	{
		public Device Device { get; private set; }
		public ScannerDeviceSettings DeviceSettings { get; private set; }
		public ScannerPictureSettings PictureSettings { get; private set; }

		public ScannerDevice(Device device)
		{
			this.Device = device;
            if (device == null)
            {
                this.DeviceSettings = null;
                this.PictureSettings = null;
            }
            else
            {
                this.DeviceSettings = new ScannerDeviceSettings(device.Properties);
                this.PictureSettings = new ScannerPictureSettings(device.Items[1].Properties);
            }
		}

        public Items ShowUIForWIAScan(bool bShowUI,ScannerDevice device)
        {
            Items objItem = device.Device.Items;
            

            if (bShowUI)
            {
                try
                {
                    CommonDialogClass wiaDialog = new CommonDialogClass();

                    WiaImageIntent wiaIntent = (WiaImageIntent)(device.PictureSettings.CurrentIntent);

                    objItem = wiaDialog.ShowSelectItems(this.Device, wiaIntent, WiaImageBias.MaximizeQuality, true, true, false);
                    //wiaDialog.ShowDeviceProperties(this.Device, false);
                }
                catch 
                {
                }
            }
            return objItem;
        }

		/// <summary>
		/// Führt den für angegebene Gerät konfigurierten Scan-Job aus und liest die gescannten Bilder aus
		/// </summary>
		/// <param name="scannerDevice"></param>
		/// <returns></returns>
		public List<byte[]> PerformScan(Items objItem,string formatID = FormatID.wiaFormatTIFF)
		{
            //Items objItem = objItem;// this.Device.Items;
            if (objItem == null) { return null; }
            List<byte[]> lstImg = new List<byte[]>();
            ImageFile imageFile;
            try
            {

                imageFile = (ImageFile)objItem[1].Transfer(formatID);
                //CommonDialogClass wiaDialog = new CommonDialogClass();
                //imageFile = (ImageFile)wiaDialog.ShowTransfer(objItem[1], formatID, false);
                for (int frame = 1; frame <= imageFile.FrameCount; frame++)
                {
                    imageFile.ActiveFrame = frame;
                    ImageFile argbImage = imageFile.ARGBData.get_ImageFile(imageFile.Width, imageFile.Height);

                    byte[] result = (byte[])argbImage.FileData.get_BinaryData();
                    lstImg.Add(result);
                    result = null;
                    argbImage = null;
                }
            }
            catch //(System.Exception ex)
            {
                
            }

            imageFile = null;
            return lstImg;
		}
		
	}
}
