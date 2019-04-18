using System;
using WIA;
using System.Collections.Generic;

namespace gloScanWIA
{
	/// <summary>
	/// Stellt Methoden Bereit, um auf ein WIA-Gerät zuzugreifen
	/// </summary>
	public static class WiaDevice
	{
		/// <summary>
		/// Ermittelt die Referenz auf ein WIA-Gerät
		/// </summary>
		/// <param name="deviceID">ID des Geräts</param>
		/// <returns>Referenz auf das WIA-Gerät mit der angegebenen ID</returns>
		public static Device FromDeviceId(string deviceID)
		{
			DeviceManager deviceManager = new DeviceManagerClass();
			foreach (DeviceInfo deviceInfo in deviceManager.DeviceInfos)
				if (deviceInfo.DeviceID == deviceID)
					return deviceInfo.Connect();
			throw new ArgumentException("Kein Device mit der ID '"+deviceID+"' gefunden.");
		}
        private static  CommonDialogClass _commonDialogClass = null;

        public static CommonDialogClass CommonDialogClass
        {
            get
            {
                if (_commonDialogClass == null)
                {
                   _commonDialogClass = new CommonDialogClass();
                }
                return _commonDialogClass;
            }
            
        }
            

		/// <summary>
		/// Zeigt den Dialog zur Geräteauswahl an
		/// </summary>
		/// <returns>das Gerät, das der Benutzer ausgewählt hat</returns>
		public static Device FromUserDialog(WiaDeviceType deviceType = WiaDeviceType.UnspecifiedDeviceType,bool alwaysSelectDevice = false)
		{			
			CommonDialogClass wiaDialog = CommonDialogClass;
			return wiaDialog.ShowSelectDevice(deviceType,alwaysSelectDevice,false);
		}
		

		/// <summary>
		/// Ermittelt das erste verfügbare WIA-Gerät
		/// </summary>
		/// <returns>das erste verfügbare WIA-Gerät</returns>
        public static Device GetFirstScannerDevice(string sWIAscannername = "")//string wiascannername
		{
            DeviceManager deviceManager;
            try
            {
                deviceManager = new DeviceManagerClass();
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(sWIAscannername))
                {
                    return GetFirstScannerDevice("");
                }
                else
                {
                    return null;
                }

            }

            if (!string.IsNullOrEmpty(sWIAscannername))
            {
                sWIAscannername = sWIAscannername.Substring(4);
            }
            try
            {

                foreach (DeviceInfo deviceInfo in deviceManager.DeviceInfos)
                {
                    try
                    {
                        if (deviceInfo.Type == WiaDeviceType.ScannerDeviceType) //SLR: Add only scanners to the list:
                        {
                            if (string.IsNullOrEmpty(sWIAscannername))
                            {
                                try
                                {
                                    return deviceInfo.Connect();
                                }
                                catch (Exception)
                                {
                                    return null;
                                }
                            }
                            else
                            {






                                try
                                {
                                    Property propertyName = deviceInfo.Properties["Name"];
                                    if (propertyName != null)
                                    {
                                        if (propertyName.get_Value().ToString() == sWIAscannername)
                                        {
                                            return deviceInfo.Connect();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }



                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                deviceManager = null;
            }
            if (string.IsNullOrEmpty(sWIAscannername))
            {
                return null;
            }
            else
            {
                return GetFirstScannerDevice("");
            }
		}

        public static List<string> GetWIAScannersList()//string wiascannername
        {
            List<string> arrScanners = new List<string>();
            DeviceManager deviceManager = null;
            try
            {
                deviceManager = new DeviceManagerClass();
            }
            catch (Exception)
            {
                return arrScanners;
            }

          //  string fn =@"D:\LOG\" + System.Guid.NewGuid().ToString() + ".txt";
            try
            {

                foreach (DeviceInfo deviceInfo in deviceManager.DeviceInfos)
                {
                    //System.IO.File.AppendAllText(fn, "deviceInfo : " + deviceInfo.DeviceID + Environment.NewLine);
                    //foreach (Property property in deviceInfo.Properties)
                    //{
                    try
                    {
                        if (deviceInfo.Type == WiaDeviceType.ScannerDeviceType) //SLR: Add only scanners to the list:
                        {
                            Property propertyName = deviceInfo.Properties["Name"];
                            if (propertyName != null)
                            {

                                arrScanners.Add("WIA-" + propertyName.get_Value().ToString());

                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    //System.IO.File.AppendAllText(fn, property.Name + "  " + property.get_Value().ToString() + Environment.NewLine);

                    //Console.WriteLine();
                    //}
                }
            }
            catch (Exception)
            {
               // Console.WriteLine(e.ToString());
            }

            deviceManager = null;

            return arrScanners;
        }

		public static ScannerDevice AsScannerDevice(this Device device)
		{
			return new ScannerDevice(device);
		}
	}
}
