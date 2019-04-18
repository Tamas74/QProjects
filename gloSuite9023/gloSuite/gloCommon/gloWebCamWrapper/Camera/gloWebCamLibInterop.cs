using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace gloStreamWebCamera.Camera
{
    internal static class gloWebCamLibInterop
    {

        //[DllImport("gloWebCamLib.dll", EntryPoint = "Initialize")]
        //public static extern int WebCamInitialize();

        //[DllImport("gloWebCamLib.dll", EntryPoint = "Cleanup")]
        //public static extern int WebCamCleanup();

        //[DllImport("gloWebCamLib.dll", EntryPoint = "RefreshCameraList")]
        //public static extern int WebCamRefreshCameraList(ref int count);

        //[DllImport("gloWebCamLib.dll", EntryPoint = "GetCameraDetails")]
        //public static extern int WebCamGetCameraDetails(int index,
        //    [Out, MarshalAs(UnmanagedType.Interface)] out object nativeInterface,
        //    out IntPtr name);

        public delegate void CaptureCallbackProc(
                int dwSize,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 0)] byte[] abData);

        //[DllImport("gloWebCamLib.dll", EntryPoint = "StartCamera")]
        //public static extern int WebCamStartCamera(
        //    [In, MarshalAs(UnmanagedType.Interface)] object nativeInterface,
        //    CaptureCallbackProc lpCaptureFunc,
        //    ref int width,
        //    ref int height
        //    );

        //[DllImport("gloWebCamLib.dll", EntryPoint = "StopCamera")]
        //public static extern int WebCamStopCamera();

        //[DllImport("gloWebCamLib.dll", EntryPoint = "DisplayCameraPropertiesDialog")]
        //public static extern int WebCamDisplayCameraPropertiesDialog(
        //    [In, MarshalAs(UnmanagedType.Interface)] object nativeInterface);
    }
}
