using System;
using System.Runtime.InteropServices; 
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using System.Text;

namespace gloEDocumentV3
{
    class Cls_NativeSqlClient
    {
        public enum DesiredAccess : uint
        {
            Read,
            Write,
            ReadWrite,
        }
        
        [DllImport("sqlncli10.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern SafeFileHandle OpenSqlFilestream(
           string path,
           uint access,
           uint options,
           byte[] txnToken,
           uint txnTokenLength,
           Sql64 allocationSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct Sql64
        {
            public Int64 QuadPart;
            public Sql64(Int64 quadPart)
            {
                this.QuadPart = quadPart;
            }
        }
        
        public static SafeFileHandle GetSqlFilestreamHandle(string filePath, DesiredAccess access, byte[] txnToken)
        {
            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "GetSqlFilestreamHandle start in Cls_nativeSqlClient.cs at Line 39");
            SafeFileHandle handle = OpenSqlFilestream(
               filePath,
               (uint)access,
               0,
              txnToken,
               (uint)txnToken.Length,
               new Sql64(0));
            
            return handle;

        }
    }
}


  

    
  
