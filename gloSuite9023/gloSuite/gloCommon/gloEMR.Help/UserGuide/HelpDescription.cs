using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System;

namespace gloEMR.Help
{
    /// <summary>
    /// Help description class.
    /// </summary>
    //[XmlRoot("XmlHelp", Namespace = "http://schemas.catenalogic.com/winforms/2009/HelpDescription")]
    [XmlRoot("XmlHelp", Namespace = "")]
    public class HelpDescription
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether to append the extension or not.
        /// </summary>
        [XmlAttribute("AppendExtension")]
        public bool AppendExtension = true;

        /// <summary>
        /// Gets or sets the topic description.
        /// </summary>
        [XmlElement("Control", typeof(ControlHelpDescription))]
        public List<ControlHelpDescription> TopicDescription = new List<ControlHelpDescription>();

        /// <summary>
        /// Gets or sets the help file.
        /// </summary>
        /// <value>The help file.</value>
        [XmlAttribute("HelpFile")]
        public string HelpFile { get; set; }

       
        /// <summary>
        /// Gets the help file path.
        /// </summary>
        /// <value>The help file path.</value>
        public string HelpFilePath
        {
            get
            {

                //if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloEMR"))
                //{
                //    return (!string.IsNullOrEmpty(HelpFile)) ? Path.Combine(Path.Combine(Application.StartupPath, PathHelper.HelpDirectory + "\\gloEMR"), HelpFile) : string.Empty;
                //}
                //else if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloPM"))
                //{
                //    return (!string.IsNullOrEmpty(HelpFile)) ? Path.Combine(Path.Combine(Application.StartupPath, PathHelper.HelpDirectory + "\\gloPM"), HelpFile) : string.Empty;
                //}
                //else
                //{
                   
               // }
                    return (!string.IsNullOrEmpty(HelpFile)) ? Path.Combine(Path.Combine(Application.StartupPath, PathHelper.HelpDirectory), HelpFile) : string.Empty;
            }
        }
        #endregion

        #region Description
        /// <summary>
        /// Clears the empty descriptions.
        /// </summary>
        internal void ClearEmptyDescriptions()
        {
            ClearEmptyDescriptions(TopicDescription);
        }

        /// <summary>
        /// Clears the empty descriptions from a specific collection.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        private void ClearEmptyDescriptions(List<ControlHelpDescription> descriptions)
        {
            for (int i = 0; i < descriptions.Count; i++)
            {
                // Check if this one does not have children and is empty
                if (string.IsNullOrEmpty(descriptions[i].HelpKeyword) && (descriptions[i].TopicDescription.Count == 0))
                {
                    descriptions.RemoveAt(i--);
                }
                else
                {
                    ClearEmptyDescriptions(descriptions[i].TopicDescription);
                }
            }
        }



          public static Array CreateSubArray(Array array, int offset, int length)
             {
            Array ret = Array.CreateInstance(array.GetType().GetElementType(), length);
            for (int i = offset; i < offset + length; i++)
            {
                ret.SetValue(array.GetValue(i), i - offset);
            }
            return ret;
      
       
           }

          //public static Array CreateSubArray(Array array, int length)
          //{
          //    return CreateSubArray(array, 0, length);
          //}
        /// <summary>
        /// Creates the exact description.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public ControlHelpDescription CreateExactDescription(Control control, bool appendFormNameFlag, string formName)
        {

            return CreateExactDescription( UserHelper.GetControlIDPath(control),   appendFormNameFlag,  formName  );
        }

        /// <summary>
        /// Creates the exact description.
        /// </summary>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        public ControlHelpDescription CreateExactDescription(string[] controlPath, bool appendFormNameFlag, string formName)
        {
            ControlHelpDescription description = FindExactDescription(controlPath, appendFormNameFlag,formName );

            if (description == null) return CreateExactDescription(TopicDescription, controlPath, appendFormNameFlag,  formName);

            return description;
        }

        /// <summary>
        /// Creates the exact description.
        /// </summary>
        /// <param name="topicDescription">The topic description.</param>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        private ControlHelpDescription CreateExactDescription(List<ControlHelpDescription> topicDescription, string[] controlPath, bool appendFormNameFlag, string formName)
        {
            ControlHelpDescription childDescription = null;

            // szukaj opisu pasuj¹cego do prefiksu opisu
            foreach (ControlHelpDescription description in topicDescription)
            {
                if (appendFormNameFlag)
                {
                    if (description.Name == controlPath[0] + "_" + formName)
                    {
                        childDescription = description;
                    }
                }
                else
                {
                    if (description.Name == controlPath[0])
                    {
                        childDescription = description;
                    }
                }

            }

            // jeœli nie znaleziono to twórz
            if (childDescription == null)
            {
                childDescription = new ControlHelpDescription();
                childDescription.Name = controlPath[0];
                childDescription.FormName = formName;
                childDescription.AppendFormFlag = appendFormNameFlag;
                if (childDescription.AppendFormFlag)
                {
                    childDescription.Name += "_" + formName;
                }
                topicDescription.Add(childDescription);
            }

            // If this is not the last one, check the parent
            if (controlPath.Length > 1)
            {
                return CreateExactDescription(childDescription.TopicDescription,
                    (string[])CreateSubArray(controlPath, 1, controlPath.Length - 1), appendFormNameFlag,  formName);
            }
            else
            {
                return childDescription;
            }
        }

        /// <summary>
        /// Finds the exact description.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public ControlHelpDescription FindExactDescription(Control control,bool appendFormNameFlag, string formName)
        {
            return FindExactDescription(UserHelper.GetControlIDPath(control), appendFormNameFlag,formName);
        }

        /// <summary>
        /// Finds the exact description.
        /// </summary>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        public ControlHelpDescription FindExactDescription(string[] controlPath, bool appendFormNameFlag, string formName)
        {
            return FindExactDescription(TopicDescription, controlPath, appendFormNameFlag,formName);
        }

        /// <summary>
        /// Finds the exact description.
        /// </summary>
        /// <param name="topicDescription">The topic description.</param>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        private ControlHelpDescription FindExactDescription(List<ControlHelpDescription> topicDescription, string[] controlPath,bool appendFormNameFlag, string formName)
        {
            if (controlPath.Length > 0)
            {
                foreach (ControlHelpDescription description in topicDescription)
                {

                    if (appendFormNameFlag)
                    {
                        if (description.Name == controlPath[0] + "_" + formName)
                        {
                            if (controlPath.Length == 1)
                            {
                                return description;
                            }
                            else
                            {
                                return FindExactDescription(description.TopicDescription,
                                    (string[])CreateSubArray(controlPath, 1, controlPath.Length - 1), appendFormNameFlag, formName);
                            }
                        }
                    }
                    else
                    {
                        if (description.Name == controlPath[0])
                        {
                            if (controlPath.Length == 1)
                            {
                                return description;
                            }
                            else
                            {
                                return FindExactDescription(description.TopicDescription,
                                    (string[])CreateSubArray(controlPath, 1, controlPath.Length - 1), appendFormNameFlag, formName);
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the description.
        /// </summary>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        public ControlHelpDescription FindDescription(string[] controlPath, bool appendFormNameFlag, string formName)
        {
            return FindDescription(TopicDescription, controlPath, appendFormNameFlag,formName );
        }

        /// <summary>
        /// Finds the description.
        /// </summary>
        /// <param name="topicDescription">The topic description.</param>
        /// <param name="controlPath">The control path.</param>
        /// <returns></returns>
        private ControlHelpDescription FindDescription(List<ControlHelpDescription> topicDescription, string[] controlPath, bool appendFormNameFlag, string formName)
        {
            if (controlPath.Length > 0)
            {
                foreach (ControlHelpDescription description in topicDescription)
                {
                    if (appendFormNameFlag)
                    {
                        if (description.Name == (controlPath[0] + "_" + formName))
                        {
                            // Get child description
                            ControlHelpDescription childDescription = FindDescription(description.TopicDescription,
                                (string[])CreateSubArray(controlPath, 1, controlPath.Length - 1), appendFormNameFlag, formName);

                            // Check if the child description is valid
                            if ((childDescription != null) && !string.IsNullOrEmpty(childDescription.HelpKeyword))
                            {
                                return childDescription;
                            }
                            else
                            {
                                return description;
                            }
                        }
                    }
                    else
                    {
                        if (description.Name == controlPath[0])
                        {
                            // Get child description
                            ControlHelpDescription childDescription = FindDescription(description.TopicDescription,
                                (string[])CreateSubArray(controlPath, 1, controlPath.Length - 1), appendFormNameFlag, formName);

                            // Check if the child description is valid
                            if ((childDescription != null) && !string.IsNullOrEmpty(childDescription.HelpKeyword))
                            {
                                return childDescription;
                            }
                            else
                            {
                                return description;
                            }
                        }

                    }
                    }
            }

            return null;
        }

        /// <summary>
        /// Gets an empty instance of the HelpDescription.
        /// </summary>
        /// <value>An empty instance of the HelpDescription.</value>
        public static HelpDescription Empty
        {
            get { return new HelpDescription(); }
        }
        #endregion
    }

    /// <summary>
    /// Control help description.
    /// </summary>
    public class ControlHelpDescription
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlAttribute("Name")]
        public string Name;

        /// <summary>
        /// Gets or sets the help keyword.
        /// </summary>
        [XmlAttribute("HelpKeyword")]
        public string HelpKeyword;

        /// <summary>
        /// Gets or sets the help navigator.
        /// </summary>
        [XmlAttribute("HelpNavigator")]
        public HelpNavigator HelpNavigator = HelpNavigator.Topic;

        /// <summary>
        /// Gets or sets whether to show the help or not.
        /// </summary>
        [XmlAttribute("ShowHelp")]
        public bool ShowHelp = true;
        
        [XmlAttribute("AppendFormFlag")]
        public bool AppendFormFlag { get; set; }

        [XmlAttribute("FormName")]
        public string FormName { get; set; }


        /// <summary>
        /// Gets or sets the topic description.
        /// </summary>
        [XmlElement("Control", typeof(ControlHelpDescription))]
        public List<ControlHelpDescription> TopicDescription = new List<ControlHelpDescription>();
    }











   

}
