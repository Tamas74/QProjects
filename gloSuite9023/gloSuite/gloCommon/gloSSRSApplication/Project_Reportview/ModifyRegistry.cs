using System;
using Microsoft.Win32;      
using System.Windows.Forms; 

namespace Utility.ModifyRegistry
{
	
	public class ModifyRegistry
	{
		private bool showError = false;
		
		public bool ShowError
		{
			get { return showError; }
			set	{ showError = value; }
		}

		private string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
		
		public string SubKey
		{
			get { return subKey; }
			set	{ subKey = value; }
		}

        private RegistryKey baseRegistryKey = Registry.LocalMachine;
		
		public RegistryKey BaseRegistryKey
		{
			get { return baseRegistryKey; }
			set	{ baseRegistryKey = value; }
		}

		//---------------------------------------------------------------------------------
		 

		
		public string Read(string KeyName)
		{
			
			RegistryKey rk = baseRegistryKey ;			
			RegistryKey sk1 = rk.OpenSubKey(subKey);
            string myString = null;
			
			if ( sk1 == null )
			{
				return null;
			}
			else
			{
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    myString = (string)sk1.GetValue(KeyName.ToUpper());
                    return myString;
                }
                catch (Exception e)
                {

                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
                finally
                {
                    if (sk1 != null)
                    {
                        sk1.Close();
                        sk1.Dispose();
                    }
                }
			}
		}	

		
		
		public bool Write(string KeyName, object Value)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				
				RegistryKey sk1 = rk.CreateSubKey(subKey);
                if (sk1 != null)
                {
                    sk1.SetValue(KeyName.ToUpper(), Value);
                    sk1.Close();
                    sk1.Dispose();
                }

				return true;
			}
			catch (Exception e)
			{
				
				ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
				return false;
			}
		}

		
		public bool DeleteKey(string KeyName)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				// If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                {
                    sk1.DeleteValue(KeyName);
                    sk1.Close();
                    sk1.Dispose();
                }

				return true;
			}
			catch (Exception e)
			{
				
				ShowErrorMessage(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		
		public bool DeleteSubKeyTree()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);

                if (sk1 != null)
                {
                    sk1.Close();
                    sk1.Dispose();
                    rk.DeleteSubKeyTree(subKey);
                }

				return true;
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		
		public int SubKeyCount()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				// If the RegistryKey exists...
                if (sk1 != null)
                {
                    int myCount = sk1.SubKeyCount;
                    sk1.Close();
                    sk1.Dispose();
                    return myCount;
                }
                else
                {
                    return 0;
                }
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Retriving subkeys of " + subKey);
				return 0;
			}
		}

		
		public int ValueCount()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				// If the RegistryKey exists...
                if (sk1 != null)
                {
                    int myCount = sk1.ValueCount;
                    sk1.Close();
                    sk1.Dispose();

                    return myCount;
                }
                else
                    return 0; 
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Retriving keys of " + subKey);
				return 0;
			}
		}

		
		
		private void ShowErrorMessage(Exception e, string Title)
		{
			if (showError == true)
				MessageBox.Show(e.Message,
								Title
								,MessageBoxButtons.OK
								,MessageBoxIcon.Error);
		}
	}
}
