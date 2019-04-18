using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;


namespace gloCommon
{
    public class Cls_TabIndexSettings
    {

        #region " Class For Comparing two controls in the selected tab scheme. "

        private class TabSchemeComparer : IComparer
        {
            private TabScheme comparisonScheme;

            #region IComparer Members

            public int Compare(object x, object y)
            {
                Control control1 = x as Control;
                Control control2 = y as Control;

                if (control1 == null || control2 == null)
                {
                    Debug.Assert(false, "Attempting to compare a non-control");
                    return 0;
                }

                if (comparisonScheme == TabScheme.None)
                {
                    return 0;
                }

                if (comparisonScheme == TabScheme.AcrossFirst)
                {
                    // The primary direction to sort is the y direction (using the Top property).
                    // If two controls have the same y coordination, then we sort them by their x's.
                    if (control1.Top < control2.Top)
                    {
                        return -1;
                    }
                    else if (control1.Top > control2.Top)
                    {
                        return 1;
                    }
                    else
                    {
                        return (control1.Left.CompareTo(control2.Left));
                    }
                }
                else    // comparisonScheme = TabScheme.DownFirst
                {
                    // The primary direction to sort is the x direction (using the Left property).
                    // If two controls have the same x coordination, then we sort them by their y's.
                    if (control1.Left < control2.Left)
                    {
                        return -1;
                    }
                    else if (control1.Left > control2.Left)
                    {
                        return 1;
                    }
                    else
                    {
                        return (control1.Top.CompareTo(control2.Top));
                    }
                }
            }

            #endregion


            public TabSchemeComparer(TabScheme scheme)
            {
                comparisonScheme = scheme;
            }
        }

        #endregion

        #region " Variable and Enum "

        private Control container;
        private Hashtable schemeOverrides;
        private int curTabIndex = 0;

        public enum TabScheme
        {
            None,
            AcrossFirst,
            DownFirst
        }

        #endregion

        #region " Public and Private Methods "

        public Cls_TabIndexSettings(Control container)
        {
            this.container = container;
            this.curTabIndex = 0;
            this.schemeOverrides = new Hashtable();
        }

        private Cls_TabIndexSettings(Control container, int curTabIndex, Hashtable schemeOverrides)
        {
            this.container = container;
            this.curTabIndex = curTabIndex;
            this.schemeOverrides = schemeOverrides;
        }

        public void SetSchemeForControl(Control c, TabScheme scheme)
        {
            schemeOverrides[c] = scheme;
        }

        public int SetTabOrder(TabScheme scheme)
        {
            ArrayList controlArraySorted = null;
            try
            {
                controlArraySorted = new ArrayList();
                controlArraySorted.AddRange(container.Controls);
                controlArraySorted.Sort(new TabSchemeComparer(scheme));

                foreach (Control c in controlArraySorted)
                {
                    Debug.WriteLine("Tab Index Settings:  Changing tab index for " + c.Name);


                    c.TabIndex = curTabIndex++;
                    if (c.Controls.Count > 0)
                    {
                        // Control has children -- recurse.
                        TabScheme childScheme = scheme;
                        curTabIndex = (new Cls_TabIndexSettings(c, curTabIndex, schemeOverrides)).SetTabOrder(childScheme);
                    }
                }

                return curTabIndex;
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Exception in SetTabOrder:  " + e.Message);
                return 0;
            }
            finally
            {
                controlArraySorted = null;
            }
        }

        #endregion

    }
    
}
