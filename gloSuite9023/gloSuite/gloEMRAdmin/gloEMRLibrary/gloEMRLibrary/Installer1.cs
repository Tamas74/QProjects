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
                oKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{AB5CE44B-3768-4F40-9D29-116D11B8C0B9}", true);
                if (oKey != null && oKey.ToString() != "")
                {
                    Registry.LocalMachine.DeleteSubKey("InstallShield_{AB5CE44B-3768-4F40-9D29-116D11B8C0B9}",false);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                oKey.Close();
            }
           
        }


        }

    }

       
    
           
