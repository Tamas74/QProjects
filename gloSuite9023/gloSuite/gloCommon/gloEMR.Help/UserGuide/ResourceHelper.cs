using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Diagnostics;

namespace gloEMR.Help
{
	/// <summary>
	/// Resource helper class.
	/// </summary>
    //public static class ResourceHelper
    //{
       
    //    private static HelpDescription _helpDescription;
       
    //    public static HelpDescription HelpDescription
    //    {
    //        get
    //        {
    //            if (_helpDescription == null)
    //            {
    //                _helpDescription = HelpDescription.Empty;

    //                try
    //                {
    //                    Stream stream = null;
                      
    //                    try
    //                    {
    //                        stream = File.Open(PathHelper.FullHelpMappingPath, FileMode.Open, FileAccess.Read);
    //                    }
    //                    catch { }
                       
    //                   // if (stream == null)
    //                      //  stream = GetApplicationStream(PathHelper.RelativeHelpMappingPath);

    //                    // serializer
    //                    XmlSerializer xs = new XmlSerializer(typeof(HelpDescription));

    //                    if (stream != null)
    //                    {
    //                        _helpDescription = (HelpDescription)xs.Deserialize(stream);
    //                        stream.Dispose();
    //                    }

    //                    // Clear empty descriptions
    //                    _helpDescription.ClearEmptyDescriptions();
    //                }
    //                catch (Exception ex)
    //                {
    //                    Trace.TraceError(ex.Message);
    //                }
    //            }

    //            return _helpDescription;
    //        }
    //    }
     

    //    #region Methods - builder
    //    /// <summary>
    //    /// Saves the help description.
    //    /// </summary>
    //    /// <param name="helpDescription">The help description.</param>
    //    public static void SaveHelpDescription(HelpDescription helpDescription)
    //    {
    //        if (helpDescription != null)
    //        {
    //            using (FileStream fs = File.Create(PathHelper.FullHelpMappingPath))
    //            {
    //                XmlSerializer xs = new XmlSerializer(typeof(HelpDescription));
    //                xs.Serialize(fs, helpDescription);
    //            }
    //        }
    //    }
    //    #endregion

      
    //    //static readonly List<string> _examinedAssemblies = new List<string>();

    //    /// <summary>
    //    /// Gets the application stream.
    //    /// </summary>
    //    /// <param name="resourceName">Name of the resource.</param>
    //    /// <returns></returns>
    //    //public static Stream GetApplicationStream(string resourceName)
    //    //{
    //    //    _examinedAssemblies.Clear();
    //    //    return GetResource(Assembly.GetEntryAssembly(), resourceName);
    //    //}
  

       
    //    /// <summary>
    //    /// Gets the resource.
    //    /// </summary>
    //    /// <param name="TheAssembly">The assembly.</param>
    //    /// <param name="ResourceName">Name of the resource.</param>
    //    /// <returns></returns>
    //    //private static Stream GetResource(Assembly TheAssembly, string ResourceName)
    //    //{
           
    //    //    foreach (string resName in TheAssembly.GetManifestResourceNames())
    //    //        if (resName.EndsWith(ResourceName))
    //    //            return TheAssembly.GetManifestResourceStream(resName);

            
    //    //    foreach (AssemblyName RefAssembly in TheAssembly.GetReferencedAssemblies())
    //    //    {
    //    //        if (!_examinedAssemblies.Contains(RefAssembly.FullName))
    //    //        {
    //    //            _examinedAssemblies.Add(RefAssembly.FullName);
    //    //            try
    //    //            {
    //    //                Assembly ChildAssembly = Assembly.Load(RefAssembly);
    //    //                if (ChildAssembly != null)
    //    //                {
    //    //                    Stream resStream = GetResource(ChildAssembly, ResourceName);
    //    //                    if (resStream != null)
    //    //                        return resStream;
    //    //                }
    //    //            }
    //    //            catch { }
    //    //        }
    //    //    }

    //    //    return null;
    //    //}
      
    //}
}
