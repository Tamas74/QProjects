using System.Windows.Forms;
using System.Collections.Generic;
namespace gloEMR.Help
{
    /// <summary>
    /// Client help processor.
    /// </summary>
    public class EndUserHelp : IHelpProcessor
    {
        #region IHelpProcessor Members
        /// <summary>
        /// Processes the help for a <see cref="Control"/>.
        /// </summary>
        /// <param name="control">The control.</param>
        public void ProcessControlHelp(Control control)
        {
            string controlId = UserHelper.GetControlID(control);
            string[] controlIdParts = UserHelper.GetControlIDPath(controlId);

            if (ResourceHelper.HelpDescription != HelpDescription.Empty)
            {
                ControlHelpDescription topicDescription = ResourceHelper.HelpDescription.FindDescription(controlIdParts,true,UserHelper.GetFormName( control  as Control) );
                if (topicDescription == null)
                {
                    topicDescription = ResourceHelper.HelpDescription.FindDescription(controlIdParts, false, UserHelper.GetFormName(control as Control));
                }
                if ((topicDescription != null) && (!string.IsNullOrEmpty(topicDescription.HelpKeyword) && topicDescription.ShowHelp))
                {
                    // Check if an extension should be automatically be appended
                    string keyword = topicDescription.HelpKeyword;
                   
                    if (ResourceHelper.HelpDescription.AppendExtension && (topicDescription.HelpNavigator == HelpNavigator.Topic))
                    {
                        if (keyword != string.Empty)
                        {
                            keyword = keyword.Replace(".htm", "");

                        }
                        
                        // Append extension
                        keyword += ".htm";
                       
                    }
                  //  keyword = "Orders.htm"; 
                    
                    System.Windows.Forms.Help.ShowHelp(control, ResourceHelper.HelpDescription.HelpFilePath, topicDescription.HelpNavigator, keyword);
                }
                else
                {
                    if (!string.IsNullOrEmpty(ResourceHelper.HelpDescription.HelpFile))
                    {
                        System.Windows.Forms.Help.ShowHelp(control, ResourceHelper.HelpDescription.HelpFilePath);
                    }
                }
            }
        }

       //new function added  for showing cms measure help
        public static void StaticProcessControlHelp(Control control)
        {
            string controlId = UserHelper.GetControlID(control);
            string[] controlIdParts = UserHelper.GetControlIDPath(controlId);

            if (ResourceHelper.HelpDescription != HelpDescription.Empty)
            {
                ControlHelpDescription topicDescription = ResourceHelper.HelpDescription.FindDescription(controlIdParts, true, UserHelper.GetFormName(control as Control));
                if (topicDescription == null)
                {
                    topicDescription = ResourceHelper.HelpDescription.FindDescription(controlIdParts, false, UserHelper.GetFormName(control as Control));
                }
                if ((topicDescription != null) && (!string.IsNullOrEmpty(topicDescription.HelpKeyword) && topicDescription.ShowHelp))
                {
                    // Check if an extension should be automatically be appended
                    string keyword = topicDescription.HelpKeyword;

                    if (ResourceHelper.HelpDescription.AppendExtension && (topicDescription.HelpNavigator == HelpNavigator.Topic))
                    {
                        if (keyword != string.Empty)
                        {
                            keyword = keyword.Replace(".htm", "");

                        }

                        // Append extension
                        keyword += ".htm";

                    }
                    //  keyword = "Orders.htm"; 

                    System.Windows.Forms.Help.ShowHelp(control, ResourceHelper.HelpDescription.HelpFilePath, topicDescription.HelpNavigator, keyword);
                }
                else
                {
                    if (!string.IsNullOrEmpty(ResourceHelper.HelpDescription.HelpFile))
                    {
                        System.Windows.Forms.Help.ShowHelp(control, ResourceHelper.HelpDescription.HelpFilePath);
                    }
                }
            }
        }
        #endregion
    }
    public static class UserHelper
    {
        #region Control ID
        /// <summary>
        /// Gets the control ID.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static string GetControlID(Control control)
        {
            if (control is Form || control.Parent == null)
            {
                return control.GetType().FullName;
            }
            else if (control is TreeView)
            {
                TreeView t = control as TreeView;
                if (t.SelectedNode != null)
                {
                    string myString = t.SelectedNode.Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }


            else if ( control is ListView )     
                 {         ListView l = control as ListView;
                 if (l.SelectedItems.Count > 0 && l.SelectedItems[0].Text != null)
                 {
                     string myString = l.SelectedItems[0].Text.Replace('/', '_');
                     return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                 }
                 else
                 {
                     return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                 } 
                 }



                else if ( control is TabControl )     
                {          
                    TabControl tb = control as TabControl;  
                    if ( tb.SelectedTab != null )           
                    { 
                       
                        string myString = tb.SelectedTab.Text.Replace('/', '_');
                        return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                    }
                    else
                    {
                        return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                    }  
                }


           

            else
            {
                return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);

            }
        }
        public static string GetFormName(Control control)
        {

           
            if (control is Form || control.Parent == null)
            {
                return control.Text.Replace('/','_');
            }
            else if (control is TreeView)
            {
                TreeView t = control as TreeView;
                if (t.SelectedNode != null)
                {
                    string myString = t.SelectedNode.Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }


            else if (control is ListView)
            {
                ListView l = control as ListView;
                if (l.SelectedItems.Count > 0 && l.SelectedItems[0].Text != null)
                {
                    string myString = l.SelectedItems[0].Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }



            else if (control is TabControl)
            {
                TabControl tb = control as TabControl;
                if (tb.SelectedTab != null)
                {

                    string myString = tb.SelectedTab.Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }
            
            else
            {
                return GetFormName(control.Parent);

            }
        }
        /// <summary>
        /// Gets the control ID path.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static string[] GetControlIDPath(Control control)
        {
            return GetControlIDPath(GetControlID(control));
        }

        /// <summary>
        /// Gets the control ID path.
        /// </summary>
        /// <param name="controlID">The control ID.</param>
        /// <returns></returns>
        public static string[] GetControlIDPath(string controlID)
        {
            return controlID.Split('/');
        }

        /// <summary>
        /// Gets the control tree.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static List<Control> GetControlTree(Control control)
        {
            List<Control> ret = new List<Control>();

            ret = GetControlTree(control, ret);
            ret.Reverse();

            return ret;
        }

        /// <summary>
        /// Gets the control tree.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="ret">The ret.</param>
        /// <returns></returns>
        private static List<Control> GetControlTree(Control control, List<Control> ret)
        {
            if (control == null) return ret;

            ret.Add(control);
            return GetControlTree(control.Parent, ret);
        }

        /// <summary>
        /// Gets the control description.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static string GetControlDescription(Control control)
        {
            if (control == null) return "[pusty]";

            if (control is Form)
            {
                return string.Format("{0}", control.GetType().FullName);
            }
            else if (control is TreeView)
            {
                TreeView t = control as TreeView;
                // return t.SelectedNode.Text;
                if (t.SelectedNode != null)
                {
                    string myString = t.SelectedNode.Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
             }



            else if (control is ListView)
            {
                ListView l = control as ListView;
                if (l.SelectedItems.Count > 0 && l.SelectedItems[0].Text != null)
                {
                    string myString = l.SelectedItems[0].Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }



            else if (control is TabControl)
            {
                TabControl tb = control as TabControl;
                if (tb.SelectedTab != null)
                {

                    string myString = tb.SelectedTab.Text.Replace('/', '_');
                    return string.Format("{0}/{1}", GetControlID(control.Parent), myString);
                }
                else
                {
                    return string.Format("{0}/{1}", GetControlID(control.Parent), control.Name);
                }
            }

             

            else
            {
                return string.Format("{0} [{1}]", control.Name, control.GetType().Name);
            }
        }
        #endregion
    }
}
