using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using Microsoft.Win32;


namespace gloEMRLibrary
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
       
        public Installer1()
        {
            InitializeComponent();
        }


        public override void Install(System.Collections.IDictionary savedState)
        {
            UninstallgloEMR();
        }



        public void UninstallgloEMR()
        {
            RegistryKey oKey = null;
            try
            {
                oKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{5AE656CB-F224-4D7F-9EC9-5543210FCCF3}", true);
                if (oKey != null && oKey.ToString() != "")
                {
                    Registry.LocalMachine.DeleteSubKey("InstallShield_{5AE656CB-F224-4D7F-9EC9-5543210FCCF3}",false);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (oKey != null)
                {
                    oKey.Close();
                    oKey.Dispose();
                }
            }
           



        }

    }
           
}