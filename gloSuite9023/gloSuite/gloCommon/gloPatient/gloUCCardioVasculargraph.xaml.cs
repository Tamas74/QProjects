using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Cui.Controls;
using Microsoft.Cui.SamplePages; 
using System.Windows.Input;
using System.Data; 

namespace gloPatient
{
    /// <summary>
    /// Interaction logic for gloUCCardioVasculargraph.xaml
    /// </summary>
   public partial class gloUCCardioVasculargraph : UserControl
   {


     //  Boolean FirstLoad = true; 

       #region Attached Properties
        /// <summary>
        /// Identifies the ParentScrollViewer attached property.
        /// </summary>
        ArrayList arrgraph = new ArrayList();
        public static readonly DependencyProperty ParentScrollViewerProperty =
            DependencyProperty.RegisterAttached("ParentScrollViewer2", typeof(ScrollViewer), typeof(TimeActivityGraphHostPage), null);
        #endregion

        private DataSet ds = null;
        DataTable dtScenario = null;
        DataTable dtSection = null;
        DataTable dtRow = null;
        DataTable dtItem = null;
        DataTable dtEvent = null;
        DataTable dtEvents = null;

        public delegate void onExamClick(object sender, RoutedEventArgs e);
        public event onExamClick onclick; //(object sender, RoutedEventArgs e);
        public string strFileName = "";
        public delegate void onProblemClick(object sender, RoutedEventArgs e);
        public event onProblemClick onProblemEventclick; //(object sender, RoutedEventArgs e);


        #region Private members
        /// <summary>
        /// Constant to denote the minimum level of detail to show activities.
        /// </summary>
        private const int MinimumLevelOfDetailForActivities = 4;
       // int flagdata = 1;
        int top = 0;
        int rowcnt = 0;
        /// <summary>
        /// Member variable to hold time event graph data from XML file.
        /// </summary>
        private Collection<TimelineGraphData> xmlGraphData = new Collection<TimelineGraphData>();

        /// <summary>
        /// Member variable to hold selected patient id.
        /// </summary>
        private string selectedPatientId;

        /// <summary>
        /// Member variable to hold time frequency selected index.
        /// </summary>
        private int timeFrequencySelectedIndex;

        /// <summary>
        /// Member variable to hold the last focused element before opening the dialog.
        /// </summary>
        private Control lastFocusedElement;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeActivityGraphHostPage"/> class.
        /// </summary>
        /// 

        public static int lvlno;

        public gloUCCardioVasculargraph(DataSet glods)
        {
           
                
                 lvlno = 0;
          InitializeComponent();
        //  this.timeFrequencySelectedIndex = 17;  // by default setting time frequency it to 1 yr
          ds = glods;
          dtScenario = ds.Tables["Scenario"];
          dtSection = ds.Tables["Section"];
          dtRow = ds.Tables["Row"];
          dtItem = ds.Tables["Item"];
          dtEvent = ds.Tables["Event"];
          dtEvents = ds.Tables["Events"]; 
                
                
                addcheckbox();
            this.Loaded += new System.Windows.RoutedEventHandler(this.TimeActivityGraphHostPage_Loaded);
            this.KeyDown += new KeyEventHandler(this.TimeActivityGraphHostPage_KeyDown);

       
            
            }
    


       
       




        #endregion


        #region Attached Properties Set/Get
        /// <summary>
        /// Gets the parent scroll viewer.
        /// </summary>
        /// <param name="obj">The object for which the parent scrollbar needs to be retreived.</param>
        /// <returns>Returns the scrollviewer control associated with the scrollbar.</returns>
        public static ScrollViewer GetParentScrollViewer(DependencyObject obj)
        {
            return (ScrollViewer)obj.GetValue(ParentScrollViewerProperty);
        }
        private string Exam_ID = "";
        private string Problem_ID = "";
        private string Exam_Name = "";
        private string Exam_Dos = "";
        private bool Exam_Status = false;
        private string Visit_ID = "";

        public string ExamID
        {
            get
            {
                return Exam_ID;
            }
            set
            {
                Exam_ID = value;
            }
        }

        public string ProblemID
        {
            get
            {
                return Problem_ID;
            }
            set
            {
                Problem_ID = value;
            }
        }

        public string VisitID
        {
            get
            {
                return Visit_ID;
            }
            set
            {
                Visit_ID = value;
            }
        }

        public string ExamName
        {
            get
            {
                return Exam_Name;
            }
            set
            {
                Exam_Name = value;
            }
        }


        public string ExamDos
        {
            get
            {
                return Exam_Dos;
            }
            set
            {
                Exam_Dos = value;
            }
        }

        public bool ExamStatus
        {

            get
            {
                return Exam_Status;
            }
            set
            {
                Exam_Status = value;
            }
        }


        /// <summary>
        /// Sets the parent scroll viewer.
        /// </summary>
        /// <param name="obj">The object for which the parent scroll viewer control needs to be set.</param>
        /// <param name="value">The parent scrollviewer control associated with the object.</param>
        public static void SetParentScrollViewer(DependencyObject obj, ScrollViewer value)
        {
            obj.SetValue(ParentScrollViewerProperty, value);
        }

        /// <summary>
        /// Initializes the graphs.
        /// </summary>
        /// <param name="resetTimeFrequency">If set to <c>true</c> [reset time frequency].</param>

        public void addcheckbox()
        {

            CheckBox chkexam = new CheckBox();
            chkexam.Content = "CardioVascular";
            chkexam.Name = "CardioVascular";
            chkexam.ToolTip = "CardioVascular";
            chkexam.Tag = "-1";  
            chkexam.IsChecked = true;
            chkexam.Click += new RoutedEventHandler(chk_Click);
            RowDefinition rde = new RowDefinition();
            grdaddchild.RowDefinitions.Add(rde);
            chkexam.Margin = new Thickness(5, top, 5, 0 - 500);
            grdaddchild.Children.Insert(rowcnt, chkexam);
            rowcnt += 1;
            top = top + 15;

            //CheckBox chkprob = new CheckBox();
            //chkprob.Content = "Problems";
            //chkprob.Name = "Problems";
            //chkprob.IsChecked = true;
            //chkprob.Click += new RoutedEventHandler(chk_Click);
            //RowDefinition rd = new RowDefinition();
            //grdaddchild.RowDefinitions.Add(rd);
            //chkprob.Margin = new Thickness(5, top, 5, 0 - 500);

            //grdaddchild.Children.Insert(rowcnt, chkprob);

            //rowcnt += 1;
            //top = top + 15;

            //CheckBox chkmed = new CheckBox();
            //chkmed.Content = "Medications";
            //chkmed.IsChecked = true;
            //chkmed.Name = "Medications";
            //chkmed.Click += new RoutedEventHandler(chk_Click);
            //RowDefinition rd1 = new RowDefinition();
            //grdaddchild.RowDefinitions.Add(rd1);
            //chkmed.Margin = new Thickness(5, top, 5, 0 - 500);

            //grdaddchild.Children.Insert(rowcnt, chkmed);
            //rowcnt += 1;
            //top = top + 15;



            //CheckBox chkord = new CheckBox();
            //chkord.Content = "Lab Orders";
            //chkord.Name = "LabOrders";
            //chkord.IsChecked = true;
            //chkord.Click += new RoutedEventHandler(chk_Click);
            //RowDefinition rdord = new RowDefinition();
            //grdaddchild.RowDefinitions.Add(rdord);
            //chkord.Margin = new Thickness(5, top, 5, 0 - 500);

            //grdaddchild.Children.Insert(rowcnt, chkord);
            //top = top + 15;





            //CheckBox chkmea = new CheckBox();
            //chkmea.Content = "Vitals";
            //chkmea.Name = "Vitals";
            //chkmea.IsChecked = true;
            //chkmea.Click += new RoutedEventHandler(chk_Click);
            //RowDefinition rd2 = new RowDefinition();
            //grdaddchild.RowDefinitions.Add(rd2);
            //chkmea.Margin = new Thickness(5, top, 5, 0 - 500);

            //top = top + 15;
            //grdaddchild.Children.Insert(rowcnt, chkmea);

            //rowcnt += 1;


            //arrgraph.Add("Exams");
            //arrgraph.Add("Problems");
            //arrgraph.Add("Medications");
            //arrgraph.Add("Vitals");
            arrgraph.Add("CardioVascular");
        }
        public static string GrphName = "";
        public void chk_Click(object sender, RoutedEventArgs arg)
        {
            //try
            //{
            

            //GrphName = ((CheckBox)sender).Content.ToString();
            //if (((CheckBox)sender).IsChecked == false)
            //{
            //    flagdata = 0;
            //    this.xmlGraphData.Clear();
            //    int ind = -1;
            //    ind = arrgraph.IndexOf(GrphName);
            //    if (ind >= 0)
            //        arrgraph.RemoveAt(ind);


            //  //  this.ReadGraphData(GrphName);
            //    this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
            //    this.TimeActivityGraphHost.Graphs.Clear();
            //    this.AddGraphsToHost();
            //    this.TimeActivityGraphHost.RefreshLayout();

            //    for (int childcnt = 0; childcnt < grdaddchild.Children.Count; childcnt++)
            //    {
            //        CheckBox chk = (CheckBox)grdaddchild.Children[childcnt];
            //        if (chk.Name == GrphName)
            //        {
            //            chk.IsChecked = false;
            //            ind = -1;
            //            ind = arrgraph.IndexOf(chk.Content.ToString().Trim());
            //            if (ind >= 0)
            //                arrgraph.RemoveAt(ind);

            //        }

            //    }
            //}


            //if (((CheckBox)sender).IsChecked == true)
            //{
            //    flagdata = 1;
            //    int ind = arrgraph.IndexOf(GrphName);
            //    if (ind < 0)
            //        arrgraph.Add(GrphName);


            //    for (int childcnt = 0; childcnt < grdaddchild.Children.Count; childcnt++)
            //    {
            //        CheckBox chk = (CheckBox)grdaddchild.Children[childcnt];
            //        if (chk.Name == GrphName)
            //        {
            //            chk.IsChecked = true;
            //            ind = -1;
            //            ind = arrgraph.IndexOf(chk.Content.ToString().Trim());
            //            if (ind == -1)
            //                arrgraph.Add(chk.Content.ToString().Trim());


            //        }

            //    }


            //    this.xmlGraphData.Clear();
            //    this.ReadGraphData();
            //    this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
            //    this.TimeActivityGraphHost.Graphs.Clear();
            //    this.AddGraphsToHost();
            //    this.TimeActivityGraphHost.RefreshLayout();



            //}


            //}
            //catch
            //{
            //}
            GrphName = ((CheckBox)sender).Content.ToString();


            if (((CheckBox)sender).IsChecked == false)
            {
                //flagdata = 0;
                // this.xmlGraphData.Clear();
                int ind = -1;
                ind = arrgraph.IndexOf(GrphName);
                if (ind >= 0)
                    arrgraph.RemoveAt(ind);

                // FirstLoad = true;  
                //  this.ReadGraphData(GrphName);
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                //   this.TimeActivityGraphHost.Graphs.Clear();
                this.RemoveGraphsFromHost(GrphName, ((CheckBox)sender).Tag.ToString());
                //    this.TimeActivityGraphHost.RefreshLayout();

                for (int childcnt = 0; childcnt < grdaddchild.Children.Count; childcnt++)
                {
                    CheckBox chk = (CheckBox)grdaddchild.Children[childcnt];
                    if (chk.Name == GrphName)
                    {
                        chk.IsChecked = false;
                        ind = -1;
                        ind = arrgraph.IndexOf(chk.Content.ToString().Trim());
                        if (ind >= 0)
                            arrgraph.RemoveAt(ind);

                    }

                }
                if (arrgraph.Count == 1)
                {
                    if (Convert.ToString(arrgraph[0]) == ((CheckBox)sender).Name.ToString())
                    {
                        arrgraph.Clear();  
                    }
                }
            }


            if (((CheckBox)sender).IsChecked == true)
            {
                this.TimeActivityGraphHost.Visibility = Visibility.Visible; 
                //flagdata = 1;
                int ind = arrgraph.IndexOf(GrphName);
                if (ind < 0)
                    arrgraph.Add(GrphName);

                int parentchecked = 1;
                for (int childcnt = 0; childcnt < grdaddchild.Children.Count; childcnt++)
                {
                    CheckBox chk = (CheckBox)grdaddchild.Children[childcnt];
                    if (chk.Name == GrphName)
                    {
                        chk.IsChecked = true;
                        ind = -1;
                        ind = arrgraph.IndexOf(chk.Content.ToString().Trim());
                        if (ind == -1)
                            arrgraph.Add(chk.Content.ToString().Trim());


                    }
                    if (chk.Content.ToString()  == ((CheckBox)sender).Name.ToString() )
                    {
                        if (chk.IsChecked == false)
                        {
                            parentchecked = 0;
                        }
                    
                    }
                    if (chk.Name.ToString() == chk.Content.ToString())
                    {
                        chk.IsChecked = true;
                        if (arrgraph.Contains(chk.Content) == false)
                            arrgraph.Add(chk.Content.ToString()); 
                    }
                }
                if (parentchecked == 1) // check whether parent is checked
                {
                    this.AddGraphToHost(GrphName, ((CheckBox)sender).Tag.ToString(),parentchecked );
                }
                else
                {
                    this.AddGraphToHost(GrphName, ((CheckBox)sender).Tag.ToString(),parentchecked );
                }

            }

               
            }

        private void RemoveGraphsFromHost(string _GraphName, string Id)
        {


            List<TimelineGraphData> SecData = (from data in this.xmlGraphData
                                               where string.Compare(data.SectionName, _GraphName, StringComparison.CurrentCultureIgnoreCase) == 0
                                               select data).ToList();
            if (SecData.Count > 0)
            {
                this.xmlGraphData.Clear();
                this.ReadGraphData();
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                this.TimeActivityGraphHost.Graphs.Clear();
                this.AddGraphsToHost();
                this.TimeActivityGraphHost.RefreshLayout();

            }

            else
            {
                List<TimelineGraphData> graphData = (from data in this.xmlGraphData
                                                     where string.Compare(data.Title, _GraphName, StringComparison.CurrentCultureIgnoreCase) == 0
                                                     select data).ToList();
                if (graphData.Count > 0)
                {
                    List<TimelineGraphData> SecData2 = (from data in this.xmlGraphData
                                                        where (string.Compare(data.SectionName, graphData[0].SectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                                                                 && string.Compare(data.Vis, "t", StringComparison.CurrentCultureIgnoreCase) == 0)
                                                        select data).ToList();
                    if (SecData2.Count == 1)
                    {

                        for (int childcnt = 0; childcnt < grdaddchild.Children.Count; childcnt++)
                        {
                            CheckBox chk = (CheckBox)grdaddchild.Children[childcnt];
                            if (Convert.ToString(chk.Content) == graphData[0].SectionName)
                            {
                                chk.IsChecked = false;
                                break;

                            }

                        }

                        //this.xmlGraphData.Clear();
                        // this.ReadGraphData();
                        //this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                        //this.TimeActivityGraphHost.Graphs.Clear();
                        //this.AddGraphsToHost();
                        //this.TimeActivityGraphHost.RefreshLayout();
                        this.TimeActivityGraphHost.Visibility = Visibility.Hidden;    
                    }

                    else
                    {


                        for (int grphcnt = 0; grphcnt < graphData.Count; grphcnt++)
                        {
                            graphData[grphcnt].Vis = "f";
                        }
                        if (graphData.Count > 0)
                        {

                            // this.xmlGraphData.Remove(graphData[0]);

                            foreach (TimeGraphBase tg in TimeActivityGraphHost.Graphs)
                            {

                                if ((tg.Title == _GraphName) && (tg.Tag.ToString() == Id))
                                {
                                    try
                                    {
                                        tg.Visibility = Visibility.Collapsed;

                                        break;
                                        //  UpdateGraphData();
                                        //this.TimeActivityGraphHost.Graphs.Remove(tg);
                                    }
                                    catch (Exception exp)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(exp.ToString(), false);
                                    }

                                }
                            }
                        }
                        else
                        {

                            this.TimeActivityGraphHost.RefreshLayout();
                        }
                    }


                }




            }

        }


        private void AddGraphToHost(string _GraphName, string Id,int parentchecked)
        {
            int founddata = 0;
            List<TimelineGraphData> graphData = (from data in this.xmlGraphData
                                                 where (string.Compare(data.Title, _GraphName, StringComparison.CurrentCultureIgnoreCase) == 0)
                                                 && (string.Compare(data.GraphId, Id, StringComparison.CurrentCultureIgnoreCase) == 0)
                                                 select data).ToList();




            if (graphData.Count > 0)
            {
                graphData[0].Vis = "t";


                foreach (TimeGraphBase tg in TimeActivityGraphHost.Graphs)
                {

                    if ((tg.Title == _GraphName) && (tg.Tag.ToString() == Id))
                    {
                        try
                        {
                            tg.Visibility = Visibility.Visible;
                            founddata = 1;
                            break;


                        }
                        catch (Exception exp)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(exp.ToString(), false);
                        }

                    }
                }

            }

            if (founddata == 0)  // this code gets executed when any section is checked so all the graphs gets loaded again
            {
              
                this.xmlGraphData.Clear();
                this.ReadGraphData();
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                this.TimeActivityGraphHost.Graphs.Clear();
                this.AddGraphsToHost();
                this.TimeActivityGraphHost.RefreshLayout();

            
            }
            if (parentchecked == 0)
            {

                foreach (TimeGraphBase tg in TimeActivityGraphHost.Graphs)
                {

                    if ((tg.Title == _GraphName) && (tg.Tag.ToString() == Id))
                    {
                        try
                        {
                            tg.Visibility = Visibility.Visible;
                            founddata = 1;
                            //break;


                        }
                        catch (Exception exp)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(exp.ToString(), false);
                        }

                    }
                    else
                    {
                        tg.Visibility = Visibility.Collapsed ;
                        if (arrgraph.Contains(tg.Title))
                            arrgraph.Remove(tg.Title);   
                    }
                }
            }
        }

        public void InitializeGraphs(bool resetTimeFrequency)
        {
            if (resetTimeFrequency)
            {
                this.xmlGraphData.Clear();
                this.ReadGraphData();
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                this.TimeActivityGraphHost.Graphs.Clear();
                this.AddGraphsToHost();
            }
            else
            {
                this.ReadGraphData();
                this.UpdateGraphData();
            }

            this.TimeActivityGraphHost.RefreshLayout();
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Updates the out of view values.
        /// </summary>
        /// <param name="scrollViewer">The scroll viewer.</param>
        private static void UpdateOutOfViewValues(ScrollViewer scrollViewer)
        {
            ItemsControl itemsControl = scrollViewer.FindName("ItemsControl") as ItemsControl;
            GraphSection graphSection = scrollViewer.DataContext as GraphSection;

            UpdateGraphSectionValues(scrollViewer, itemsControl, graphSection);
        }

        /// <summary>
        /// Updates the graph section values.
        /// </summary>
        /// <param name="scrollViewer">The scroll viewer.</param>
        /// <param name="itemsControl">The items control.</param>
        /// <param name="graphSection">The graph section.</param>
        /// 

        private static void UpdateGraphSectionValues(ScrollViewer scrollViewer, ItemsControl itemsControl, GraphSection graphSection)
        {

            graphSection.ItemsOutViewTop = graphSection.ItemsOutViewBottom = 0;
            string title = string.Empty;
            LevelOfDetail lodControl = scrollViewer.FindName("LevelOfDetail") as LevelOfDetail;
            //if (graphSection.SectionName == "EXAMS" && lvlno == 0)
            //{
            //    lodControl.CurrentLevel = 3;
            //    lvlno = 1;
            //}
            if (lodControl != null && lodControl.CurrentLevel != 1)
            {
                foreach (object o in itemsControl.Items)
                {
                    TimeGraphBase graph = o as TimeGraphBase;
                    if (!string.IsNullOrEmpty(graph.Title))
                    {
                        Rect itemBounds = LayoutInformation.GetLayoutSlot(graph);

                        if (!double.IsNaN(itemBounds.Top) && !double.IsNaN(itemBounds.Bottom))
                        {
                            double scrollOffset = (double)scrollViewer.GetValue(ScrollViewer.VerticalOffsetProperty);

                            if (itemBounds.Top < scrollOffset && itemBounds.Bottom < scrollOffset)
                            {
                                // Completely out of view on top
                                graphSection.ItemsOutViewTop++;
                            }
                            else if (itemBounds.Top > scrollOffset + scrollViewer.ViewportHeight && itemBounds.Bottom > scrollOffset + scrollViewer.ViewportHeight)
                            {
                                // Completely out of view at bottom
                                graphSection.ItemsOutViewBottom++;
                            }
                            else if ((itemBounds.Top + (itemBounds.Height / 2)) < scrollOffset)
                            {
                                // Partially out of view on top
                                graphSection.ItemsOutViewTop++;
                            }
                            else if ((itemBounds.Bottom - (itemBounds.Height / 2)) > scrollOffset + scrollViewer.ViewportHeight)
                            {
                                // Partially out of view at bottom
                                graphSection.ItemsOutViewBottom++;
                            }
                        }
                    }
                }

                string formatText = "({0} rows are not in view - scroll {1})";
                if (graphSection.ItemsOutViewTop > 0 || graphSection.ItemsOutViewBottom > 0)
                {
                    string scrollDirection = string.Empty;
                    if (graphSection.ItemsOutViewTop > 0 && graphSection.ItemsOutViewBottom > 0)
                    {
                        scrollDirection = "UP/DOWN";
                    }
                    else if (graphSection.ItemsOutViewTop > 0)
                    {
                        scrollDirection = "UP";
                    }
                    else
                    {
                        scrollDirection = "DOWN";
                    }

                    if (graphSection.ItemsOutViewTop + graphSection.ItemsOutViewBottom == 1)
                    {
                        formatText = "({0} row is not in view - scroll {1})";
                    }

                    title = string.Format(CultureInfo.CurrentCulture, formatText, graphSection.ItemsOutViewTop + graphSection.ItemsOutViewBottom, scrollDirection);
                }
            }

            TextBlock outOfViewTitle = scrollViewer.FindName("OutOfViewTitle") as TextBlock;
            //  TreeViewItem trv = new TreeViewItem();
            //  trv.IsSelected = true;
            //  TreeViewItem tr = new TreeViewItem();
            //  trv.Items.Add(tr);  

            if (outOfViewTitle != null)
            {
                outOfViewTitle.Text = title;
            }
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private static void SetVisibility(object element, bool visible)
        {
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (visible)
            {
                frameworkElement.Visibility = Visibility.Visible;
            }
            else
            {
                frameworkElement.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Prints the time event data.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private static void PrintData(TimeGraphBase graph)
        {
            TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;
            if (timeActivityGraph != null)
            {
                IEnumerable enumerable = timeActivityGraph.DataContext as IEnumerable;
                if (enumerable != null)
                {
                    System.Diagnostics.Debug.WriteLine("Graph :" + timeActivityGraph.Name);
                    foreach (TimeActivityPoint point in enumerable)
                    {
                        System.Diagnostics.Debug.WriteLine("Start :" + point.StartDate + " End: " + point.EndDate);
                    }

                    System.Diagnostics.Debug.WriteLine("-------------------------------");
                }
            }
            else
            {
                IEnumerable enumerable = graph.DataContext as IEnumerable;
                if (enumerable != null)
                {
                    System.Diagnostics.Debug.WriteLine("Graph :" + graph.Name);
                    foreach (TimePoint point in enumerable)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("Date :" + point.DateTime);
                        sb.Append(" Y1 :" + point.Y1);
                        sb.Append(" Y2 :" + (double.IsNaN(point.Y2) ? string.Empty : point.Y2.ToString(CultureInfo.CurrentCulture)));
                        System.Diagnostics.Debug.WriteLine(sb.ToString());
                    }

                    System.Diagnostics.Debug.WriteLine("-------------------------------");
                }
            }
        }

        /// <summary>
        /// Gets the resource from app.
        /// </summary>
        /// <typeparam name="T">Type of the resource.</typeparam>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>Resource from the App.</returns>
        private static T GetResourceFromApp<T>(string keyName)
        {
            object obj = null;
            if (!string.IsNullOrEmpty(keyName))
            {
                keyName = keyName.Replace("{StaticResource ", string.Empty).Replace("}", string.Empty);
                //if (this.Resources.Contains(keyName))
                //{
                //    obj = this.Resources[keyName];
                //}
            }

            return (T)obj;
        }

        /// <summary>
        /// Gets the brush from color code.
        /// </summary>
        /// <param name="colorCode">The color code.</param>
        /// <returns>SolidColorBrush from the color code.</returns>
        private static SolidColorBrush GetBrushFromColorCode(string colorCode)
        {
            if (!string.IsNullOrEmpty(colorCode))
            {
                colorCode = colorCode.Replace("#", string.Empty);
                byte a = System.Convert.ToByte("ff", 16);
                byte pos = 0;
                if (colorCode.Length == 8)
                {
                    a = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);
                    pos = 2;
                }

                byte r = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                pos += 2;
                byte g = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                pos += 2;
                byte b = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                Color col = Color.FromArgb(a, r, g, b);
                return new SolidColorBrush(col);
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <returns>Gets the datetime from the string.</returns>
        private static DateTime? GetDate(string dateString)
        {
            DateTime? dateTime = null;

            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime parsedValue;
                if (DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedValue))
                {
                    dateTime = parsedValue;
                }
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <param name="adjustment">The adjustment.</param>
        /// <returns>Gets the adjusted date time from the string.</returns>
        private static DateTime? GetDate(string dateString, TimeSpan adjustment)
        {
            DateTime? dateTime = GetDate(dateString);
            if (dateTime.HasValue && adjustment != TimeSpan.Zero)
            {
                dateTime = dateTime.Value.Add(adjustment);
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the adjusted base date.
        /// </summary>
        /// <param name="baseDate">The base date.</param>
        /// <returns>TimeSpan for the date adjustment.</returns>
        private static TimeSpan GetAdjustedBaseDate(DateTime? baseDate)
        {
            TimeSpan scenarioBaseDate = TimeSpan.Zero;

            // assumption: the BaseDate must be in the past, so all dates are only adjusted forward
            if (baseDate.HasValue && baseDate < DateTime.Now)
            {
                scenarioBaseDate = DateTime.Now.Subtract(baseDate.Value);
                scenarioBaseDate = new TimeSpan(scenarioBaseDate.Ticks - (scenarioBaseDate.Ticks % TimeSpan.TicksPerSecond)); // truncate milliseconds
            }

            return scenarioBaseDate;
        }

        /// <summary>
        /// Gets the medication details.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="administrationEvent">The administration event.</param>
        /// <param name="dateAdjustment">TimeSpan to be added to dates for adjustment.</param>
        /// <returns>Returns TimelineMedicationDetails with medication details.</returns>
        //private static TimelineMedicationDetails GetMedicationDetails(TimelineSampleDataScenarioSectionRowItem item, TimelineSampleDataScenarioSectionRowItemEvent administrationEvent, TimeSpan dateAdjustment)
        //{
        //    TimelineMedicationDetails additionalInfo = new TimelineMedicationDetails();
        //    if (item != null)
        //    {
        //        additionalInfo.BrandName = item.Brand;
        //        additionalInfo.Dose = item.Dose;
        //        additionalInfo.DoseDuration = item.DoseDuration;
        //        additionalInfo.DoseLabel = item.DoseLabel;
        //        additionalInfo.Form = item.Form;
        //        additionalInfo.Frequency = item.Frequency;
        //        additionalInfo.MedicationName = item.Name;
        //        additionalInfo.Route = item.Route;
        //        additionalInfo.FluidStrength = item.FluidStrength;
        //        additionalInfo.SolidStrength = item.SolidStrength;
        //        additionalInfo.MedicationID = item.PatExamID;
        //        additionalInfo.ExamStatus = item.ExamStatus;
        //        additionalInfo.VisitID = item.VisitID;
        //    }

        //    if (administrationEvent != null)
        //    {
        //        additionalInfo.PlannedStartDate = GetDate(administrationEvent.PlannedStartDate, dateAdjustment);
        //        additionalInfo.PlannedEndDate = GetDate(administrationEvent.PlannedEndDate, dateAdjustment);
        //        additionalInfo.Status = administrationEvent.Status;
        //    }

        //    return additionalInfo;
        //}

          private static TimelineMedicationDetails GetMedicationDetails(DataRow  item, DataRow  administrationEvent, TimeSpan dateAdjustment)
        {
            TimelineMedicationDetails additionalInfo = new TimelineMedicationDetails();
            if (item != null)
            {
                additionalInfo.BrandName = item["Brand"].ToString() ;
                additionalInfo.Dose = item["Dose"].ToString();
                additionalInfo.DoseDuration = item["DoseDuration"].ToString() ;
                additionalInfo.DoseLabel = item["DoseLabel"].ToString() ;
                additionalInfo.Form = item["Form"].ToString() ;
                additionalInfo.Frequency = item["Frequency"].ToString() ;
                additionalInfo.MedicationName = item["Name"].ToString() ;
                additionalInfo.Route = item["Route"].ToString() ;
                additionalInfo.FluidStrength = item["FluidStrength"].ToString() ;
                additionalInfo.SolidStrength = item["SolidStrength"].ToString();
                additionalInfo.MedicationID = item["ExamID"].ToString(); //item["PatExamID"].ToString() ;
                additionalInfo.ExamStatus =  item["ExamStatus"].ToString() ;
                additionalInfo.VisitID =  item["VisitID"].ToString() ;
                additionalInfo.sTemplateName =  item["sTemplateName"].ToString() ;
            }

            if (administrationEvent != null)
            {
                additionalInfo.PlannedStartDate = GetDate(administrationEvent["PlannedStartDate"].ToString() , dateAdjustment);
                additionalInfo.PlannedEndDate = GetDate(administrationEvent["PlannedEndDate"].ToString() , dateAdjustment);
                additionalInfo.Status = administrationEvent["Status"].ToString() ;
            }

            return additionalInfo;
        }





        /// <summary>
        /// Registers the scroll position for the srollviewer at current lod.
        /// </summary>
        /// <param name="lodControl">Level of detail control.</param>
        /// <param name="scrollViewer">Scroll viewer control.</param>
        /// <param name="section">Graph section.</param>
        private static void RegisterScrollViewerPosition(LevelOfDetail lodControl, ScrollViewer scrollViewer, GraphSection section)
        {
            if (scrollViewer.VerticalOffset == 0)
            {
                lodControl.Tag = int.MinValue;
            }
            else if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                lodControl.Tag = int.MaxValue;
            }
            else
            {
                int sectionGraphsCount = section.Graphs.Count;
                if (string.IsNullOrEmpty(section.Graphs[sectionGraphsCount - 1].Title))
                {
                    sectionGraphsCount--;
                }

                int midGraphIndex = (int)((sectionGraphsCount - section.ItemsOutViewBottom - section.ItemsOutViewTop) / 2);
                lodControl.Tag = midGraphIndex + section.ItemsOutViewTop;
            }

        }

        /// <summary>
        /// Updates the graph properties.
        /// </summary>
        /// <param name="level">The level of detail.</param>
        /// <param name="section">The section containing graphs.</param>
        private static void UpdateGraphProperties(int level, GraphSection section)
        {
            foreach (TimeGraphBase graph in section.Graphs)
            {
                graph.BeginEdit();
                graph.Reset();
                TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;

                if (level == 1)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        timeActivityGraph.Minimized = true;
                        timeActivityGraph.IsTabStop = false;
                        timeActivityGraph.Activities.Clear();
                    }
                    else
                    {
                        graph.ShowDataPointLabels = Visibility.Collapsed;
                    }
                }
                else if (level == 2)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Activities.Clear();
                        timeActivityGraph.ShowDataPointLabels = Visibility.Collapsed;
                    }
                    else
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                    }
                }
                else if (level == 3)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.StackLabels = false;
                        timeActivityGraph.Activities.Clear();
                        if (timeActivityGraph.LabelMode != LabelMode.Simple)
                        {
                            timeActivityGraph.LabelMode = LabelMode.Partial;
                        }
                    }
                }
                else if (level == 4)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.StackLabels = true;
                        if (timeActivityGraph.LabelMode != LabelMode.Simple)
                        {
                            timeActivityGraph.LabelMode = LabelMode.Full;
                        }
                    }
                }

                graph.Refresh();
                graph.EndEdit();
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Adds the graphs to host.
        /// </summary>
        private void AddGraphsToHost()
        {
            foreach (TimelineGraphData graphData in this.xmlGraphData)
            {
                TimeGraphBase graph = this.GetGraphFromGraphData(graphData);
                graphData.Vis = "t";  
                DockPanel.SetDock(graph, Dock.Top);
                this.TimeActivityGraphHost.Graphs.Add(graph);
            }
            flagsecmousedown = 0;

        }

        /// <summary>
        /// Updates the graph data.
        /// </summary>
        private void UpdateGraphData()
        {
            foreach (TimeGraphBase graph in this.TimeActivityGraphHost.Graphs)
            {
                if (graph.Tag != null)
                {
                    string graphId = graph.Tag.ToString();
                    List<TimelineGraphData> graphData = (from data in this.xmlGraphData
                                                         where string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0
                                                         select data).ToList();
                    if (graphData.Count > 0)
                    {
                        graph.DataContext = graphData[0].Data;
                    }

                    TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;
                    if (graphData.Count > 0 && timeActivityGraph != null)
                    {
                        timeActivityGraph.Activities.Clear();
                        if (graphData[0].ActivityTypesDisplayed.Count > 0)
                        {
                            foreach (string activityType in graphData[0].ActivityTypesDisplayed)
                            {
                                timeActivityGraph.Activities.Add(graphData[0].Activities[activityType]);
                            }
                        }
                    }
                }

                graph.Refresh();
            }
        }

        /// <summary>
        /// Reads the graph data from the XML.
        /// </summary>
        /// 

        //private void ReadGraphData(string GrphName)
        //{
        //    lvlno = 0;
        //    XmlSerializer ser = new XmlSerializer(typeof(TimelineSampleData));
        //    XmlReader reader = XmlReader.Create(strFileName); 
        //    if (ser.CanDeserialize(reader))
        //    {
        //        TimelineSampleData sampleData = (TimelineSampleData)ser.Deserialize(reader);
        //        foreach (TimelineSampleDataScenario scenario in sampleData.Scenario)
        //        {
        //            if (string.Compare(scenario.PatientId, this.selectedPatientId, StringComparison.CurrentCultureIgnoreCase) == 0)
        //            {
        //                DateTime? scenarioBaseDate = GetDate(scenario.BaseDate);
        //                TimeSpan dateAdjustement = GetAdjustedBaseDate(scenarioBaseDate);

        //                Array.Resize(ref frsecroot, 0);
        //                cntsec = 0;
        //                Array.Resize(ref frsecroot, scenario.Section.Length);
        //                arrsecname.Clear();
        //                flagsecmousedown = 0;

        //                foreach (TimelineSampleDataScenarioSection section in scenario.Section)
        //                {

        //                    if (section.Name == GrphName) ;

        //                    else
        //                    {
        //                        int ind = arrgraph.IndexOf(section.Name);
        //                        if (ind >= 0)
        //                            this.CreateGraphData(section, dateAdjustement);

        //                    }
                           


        //                    this.timeFrequencySelectedIndex = (int)scenario.TimeFrequencySelectedIndex;

        //                }
        //            }
        //        }
        //    }
        //}


       // private void ReadGraphData()
       // {
       //     lvlno = 0;
       //     XmlSerializer ser = new XmlSerializer(typeof(TimelineSampleData));
       ////     XmlReader reader = XmlReader.Create(System.Windows.Forms.Application.StartupPath + "\\XMLFile" + "\\TimelineData.xml");
       //     XmlReader reader = XmlReader.Create(strFileName);
       //     if (ser.CanDeserialize(reader))
       //     {
       //         TimelineSampleData sampleData = (TimelineSampleData)ser.Deserialize(reader);
       //         try
       //         {

       //             foreach (TimelineSampleDataScenario scenario in sampleData.Scenario)
       //             {
       //                 if (string.Compare(scenario.PatientId, this.selectedPatientId, StringComparison.CurrentCultureIgnoreCase) == 0)
       //                 {
       //                     DateTime? scenarioBaseDate = GetDate(scenario.BaseDate);
       //                     TimeSpan dateAdjustement = GetAdjustedBaseDate(scenarioBaseDate);
       //                     bool addlabsgrph = false;
       //                     Array.Resize(ref frsecroot, 0);
       //                     Array.Resize(ref frsecroot, scenario.Section.Length);
       //                     arrsecname.Clear();
       //                     cntsec = 0;
       //                     flagsecmousedown = 0;
       //                     foreach (TimelineSampleDataScenarioSection section in scenario.Section)
       //                     {

       //                         if (section.Name == "CardioVascular")
       //                         {

                                
       //                         if (grdaddchild.RowDefinitions.Count <=1)  
       //                                 addcheckbox(section);// Add CardioVascular Graph Name
                                      

       //                         }
                          
       //                         int ind = arrgraph.IndexOf(section.Name);
       //                         if (ind >= 0)
       //                             this.CreateGraphData(section, dateAdjustement);


       //                         //if (FirstLoad == true)
       //                         //{
       //                         //    scenario.TimeFrequencySelectedIndex = (decimal)17; // by default setting it to 1 yr
       //                         //    this.timeFrequencySelectedIndex = 17; // by default setting it to 1 yr
       //                         //}
       //                         //else
       //                         //{

       //                             this.timeFrequencySelectedIndex = (int)scenario.TimeFrequencySelectedIndex;
       //                        // }
                               

       //                     }
       //                 }
       //             }

       //           //  FirstLoad = false; 
       //         }
       //         catch (Exception exp)
       //         {

       //         }
       //     }
       // }




        private void ReadGraphData()
        {
            lvlno = 0;
          //  XmlSerializer ser = new XmlSerializer(typeof(TimelineSampleData));
            //     XmlReader reader = XmlReader.Create(System.Windows.Forms.Application.StartupPath + "\\XMLFile" + "\\TimelineData.xml");
          //  XmlReader reader = XmlReader.Create(strFileName);
           // if (ser.CanDeserialize(reader))
          //  {
               // TimelineSampleData sampleData = (TimelineSampleData)ser.Deserialize(reader);
                try
                {
                    DataRow[] scenario = dtScenario.Select("PatientId=1");
                    this.timeFrequencySelectedIndex = Convert.ToInt32(dtScenario.Rows[0]["TimeFrequencySelectedIndex"].ToString());

                    string str = DateTime.Now.ToString();
                    DateTime? scenarioBaseDate = GetDate(scenario[0]["BaseDate"].ToString());
                    TimeSpan dateAdjustement = GetAdjustedBaseDate(scenarioBaseDate);
                  //  bool addlabsgrph = false;
              
                   // foreach (TimelineSampleDataScenario scenario in sampleData.Scenario)
                    //{
                       // if (string.Compare(scenario.PatientId, this.selectedPatientId, StringComparison.CurrentCultureIgnoreCase) == 0)
                       // {
                         //   DateTime? scenarioBaseDate = GetDate(scenario.BaseDate);
                          //  TimeSpan dateAdjustement = GetAdjustedBaseDate(scenarioBaseDate);
                         //   bool addlabsgrph = false;
                            Array.Resize(ref frsecroot, 0);
                            Array.Resize(ref frsecroot, dtSection.Rows.Count);
                            arrsecname.Clear();
                            //cntsec = 0;
                            flagsecmousedown = 0;
                            //foreach (TimelineSampleDataScenarioSection section in scenario.Section)
                            //{
                            foreach (DataRow section in dtSection.Rows)
                            {
                                if (Convert.ToString(section["Name"] )== "CardioVascular")
                                {


                                    if (grdaddchild.RowDefinitions.Count <= 1)
                                        addcheckbox(section);// Add CardioVascular Graph Name


                                }

                                int ind = arrgraph.IndexOf(section["Name"]);
                                if (ind >= 0)
                                    this.CreateGraphData(section, dateAdjustement);


                                //if (FirstLoad == true)
                                //{
                                //    scenario.TimeFrequencySelectedIndex = (decimal)17; // by default setting it to 1 yr
                                //    this.timeFrequencySelectedIndex = 17; // by default setting it to 1 yr
                                //}
                                //else
                                //{

                               // this.timeFrequencySelectedIndex = (int)scenario.TimeFrequencySelectedIndex;
                                // }


                            }
                       // }
                    //}

                    //  FirstLoad = false; 
                }
                catch (Exception exp)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(exp.ToString(), false);
                }
            //}
        }










        /// <summary>
        /// Creates the graph data.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="dateAdjustment">The date adjustment.</param>
        //public void addcheckbox(TimelineSampleDataScenarioSection section)
        //{
        public void addcheckbox(DataRow  section)
        {
            try
            {
               // foreach (TimelineSampleDataScenarioSectionRow row in section.Row)
               // {
                DataRow[] row = dtRow.Select("Section_Id='" + section["Section_Id"].ToString() + "'");
               for(int introw=0;introw<row.Length;introw++)
               {
                   CheckBox chkmea = new CheckBox();
                    chkmea.Content = row[introw]["Name"].ToString();
                    chkmea.ToolTip = row[introw]["Name"].ToString(); 
                    chkmea.Name = section["Name"].ToString() ;
                    chkmea.Tag = row[introw]["Id"].ToString();    
                   chkmea.IsChecked = true;
                    chkmea.Click += new RoutedEventHandler(chk_Click);
                    RowDefinition rd2 = new RowDefinition();
                    grdaddchild.RowDefinitions.Add(rd2);
                    chkmea.Margin = new Thickness(15, top + 1, 5, rowcnt - 500);
                    top += 15;
                    grdaddchild.Children.Insert(rowcnt, chkmea);
                    rowcnt += 1;
                    arrgraph.Add(row[introw]["Name"].ToString());

                }
             }
              catch (Exception exp)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(exp.Message, false);
             }
        }
        //private void CreateGraphData(TimelineSampleDataScenarioSection section, TimeSpan dateAdjustment)
        //{
        //    try
        //    {
        //        int flagprob = 0;
        //        int flagmed = 0;
        //        int flagmeas = 0;

        //        foreach (TimelineSampleDataScenarioSectionRow row in section.Row)
        //        {
        //            TimelineGraphData graphData = null;
        //            FilteredCollection data = new FilteredCollection();
        //            Dictionary<string, FilteredCollection> events = new Dictionary<string, FilteredCollection>();


        //            if (row.IdSpecified)
        //            {
        //                var graphs = (from graph in this.xmlGraphData
        //                              where string.Compare(graph.GraphId, row.Id.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCultureIgnoreCase) == 0
        //                              select graph).ToList<TimelineGraphData>();
        //                if (graphs.Count > 0)
        //                {
        //                    graphData = graphs[0];
        //                }
        //            }

        //            if (graphData == null)
        //            {
        //                graphData = new TimelineGraphData();
        //                graphData.GraphId = row.Id.ToString(CultureInfo.CurrentCulture);
        //            }

        //            graphData.Title = row.Name;
        //            switch (section.Name)
        //            {
                      


        //                case "CardioVascular":



        //                    if ((GrphName == "CardioVascular") && (flagdata == 0))
        //                        goto nextgraph;
        //                    else
        //                    {
        //                        if (arrgraph.Contains(row.Name) == true)

        //                            break;
        //                        else
        //                            goto nextgraph;
        //                    }



                       
        //            }

        //            graphData.SectionName = section.Name;
        //            graphData.Description = row.Description;
        //            graphData.Data = data;
        //            graphData.Background = GetBrushFromColorCode(row.Background);

        //            if (row.MaxLabelStackLevelsSpecified)
        //            {
        //                graphData.MaxLabelStackLevels = (int)row.MaxLabelStackLevels;
        //            }
        //            else
        //            {
        //                graphData.MaxLabelStackLevels = 2;
        //            }

        //            if (!string.IsNullOrEmpty(row.ShowLabelOvercrowdingNotifications))
        //            {
        //                graphData.ShowLabelOvercrowdingNotifications = bool.Parse(row.ShowLabelOvercrowdingNotifications);
        //            }
        //            else
        //            {
        //                graphData.ShowLabelOvercrowdingNotifications = false;
        //            }

        //            if (row.Item != null)
        //            {
        //                foreach (TimelineSampleDataScenarioSectionRowItem item in row.Item)
        //                {
        //                    graphData.Style = GetResourceFromApp<Style>(item.Style);
        //                    graphData.LabelTemplate = GetResourceFromApp<DataTemplate>(item.LabelTemplate);
        //                    graphData.PointTemplate = GetResourceFromApp<DataTemplate>(item.PointTemplate);
        //                    graphData.DataMarkerTemplate = GetResourceFromApp<DataTemplate>(item.DataMarkerTemplate);
        //                    graphData.InterpolationLineColor = GetBrushFromColorCode(item.InterpolationLineColor);
        //                    graphData.NormalRangeBrush = GetBrushFromColorCode(item.NormalRangeBrush);
        //                    graphData.UnitsDescription = item.UnitsDescription;
        //                    graphData.Units = item.Units;

        //                    graphData.NormalRangeDescription = item.NormalRangeDescription;
        //                    graphData.ShowNormalRange = string.IsNullOrEmpty(item.ShowNormalRange) ? false : bool.Parse(item.ShowNormalRange);

        //                    if (item.YAxisPaddingSpecified)
        //                    {
        //                        graphData.YAxisPadding = new Thickness((double)item.YAxisPadding);
        //                    }
        //                    else
        //                    {
        //                        graphData.YAxisPadding = new Thickness(5);
        //                    }

        //                    if (item.NormalRangeMaximumValueSpecified)
        //                    {
        //                        graphData.NormalRangeMaxValue = (double)item.NormalRangeMaximumValue;
        //                    }

        //                    if (item.NormalRangeMinimumValueSpecified)
        //                    {
        //                        graphData.NormalRangeMinValue = (double)item.NormalRangeMinimumValue;
        //                    }

        //                    if (item.YAxisMaxValueSpecified)
        //                    {
        //                        graphData.YAxisMaxValue = (double)item.YAxisMaxValue;
        //                    }

        //                    if (item.YAxisMinValueSpecified)
        //                    {
        //                        graphData.YAxisMinValue = (double)item.YAxisMinValue;
        //                    }

        //                    if (item.YAxisMajorIntervalSpecified)
        //                    {
        //                        graphData.YAxisMajorInterval = (double)item.YAxisMajorInterval;
        //                    }

        //                    if (item.YAxisIntervalMinimumHeightSpecified)
        //                    {
        //                        graphData.YAxisIntervalMinHeight = (double)item.YAxisIntervalMinimumHeight;
        //                    }

        //                    if (!string.IsNullOrEmpty(item.StartDate))
        //                    {
        //                        TimeActivityPoint timeActivityPoint = new TimeActivityPoint();
        //                        timeActivityPoint.StartDate = GetDate(item.StartDate, dateAdjustment).Value;
        //                        timeActivityPoint.EndDate = GetDate(item.EndDate, dateAdjustment);
        //                        timeActivityPoint.AdditionalInformation = GetMedicationDetails(item, null, dateAdjustment);
        //                        MedicationLabel label = new MedicationLabel();
        //                        if (!string.IsNullOrEmpty(item.Type) && string.Compare(item.Type, "SecondaryCareMedication", StringComparison.CurrentCultureIgnoreCase) == 0)
        //                        {
        //                            label.Mode = LabelMode.Full;
        //                        }
        //                        else
        //                        {
        //                            label.Mode = LabelMode.Simple;
        //                        }
                             


        //                        if (label != null)
        //                        {
        //                            label.MedicationName = item.Name;
        //                            label.Dose = item.Dose;
        //                            label.Frequency = item.Frequency;
        //                            label.Route = item.Route;
        //                            label.FluidStrength = item.FluidStrength;
        //                            label.SolidStrength = item.SolidStrength;
        //                            label.Dose = item.Dose;
        //                            label.DoseDuration = item.DoseDuration;
        //                            label.DoseLabel = item.DoseLabel;
        //                            label.BrandName = item.Brand;
        //                            label.Form = item.Form;
        //                        }

        //                        string templateShortName;

        //                        if (timeActivityPoint.EndDate.HasValue)
        //                        {
        //                            if (timeActivityPoint.StartDate != timeActivityPoint.EndDate)
        //                            {
        //                                templateShortName = "InterpolationLine";
        //                            }
        //                            else
        //                            {
        //                                templateShortName = "InterpolationLineOneTime";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            templateShortName = "InterpolationLineOpenDuration";
        //                        }
        //                        string str = section.Name.ToUpper(CultureInfo.CurrentCulture).ToString();

        //                        timeActivityPoint.DataMarkerTemplate = this.LayoutRoot.Resources[section.Name.ToUpper(CultureInfo.CurrentCulture) + "_" + templateShortName] as DataTemplate;
        //                        timeActivityPoint.Label = label;
        //                        graphData.LabelMode = label.Mode;
        //                        data.Add(timeActivityPoint);
        //                    }

        //                    if (item.Events != null)
        //                    {
        //                        foreach (TimelineSampleDataScenarioSectionRowItemEvent medsEvent in item.Events)
        //                        {
        //                            if (medsEvent.Type == "GraphData")
        //                            {
        //                                TimePoint timePoint = new TimePoint();
        //                                timePoint.DateTime = GetDate(medsEvent.ActualStartDate, dateAdjustment).Value;
        //                                timePoint.Y1 = (double)medsEvent.Y1;
        //                                graphData.Height = (double)item.Height;
        //                                if (medsEvent.Y2Specified)
        //                                {
        //                                    timePoint.Y2 = (double)medsEvent.Y2;
        //                                    graphData.GraphType = GraphType.TimeIBar;
        //                                }
        //                                else
        //                                {
        //                                    graphData.GraphType = GraphType.TimeLine;
        //                                }

        //                                data.Add(timePoint);
        //                            }
        //                            else
        //                            {
        //                                TimeActivityPoint administrationPoint = new TimeActivityPoint();
        //                                if (string.IsNullOrEmpty(medsEvent.ActualStartDate))
        //                                {
        //                                    administrationPoint.StartDate = GetDate(medsEvent.PlannedStartDate, dateAdjustment).Value;
        //                                }
        //                                else
        //                                {
        //                                    administrationPoint.StartDate = GetDate(medsEvent.ActualStartDate, dateAdjustment).Value;
        //                                }

        //                                administrationPoint.EndDate = GetDate(medsEvent.ActualEndDate, dateAdjustment);
        //                                administrationPoint.AdditionalInformation = GetMedicationDetails(item, medsEvent, dateAdjustment);

        //                                string templateShortName = "MedsAdminEvent";
        //                                if (administrationPoint.EndDate.HasValue && administrationPoint.EndDate.Value > administrationPoint.StartDate)
        //                                {
        //                                    templateShortName = "ContinuousMedsAdminEvent";
        //                                }
        //                                else if (medsEvent.Type == "prescription issue events")
        //                                {
        //                                    templateShortName = "PrescriptionEvent";
        //                                }

        //                                administrationPoint.DataMarkerTemplate = this.LayoutRoot.Resources[templateShortName + "_" + medsEvent.Status.ToLower(CultureInfo.CurrentCulture)] as DataTemplate;

        //                                string eventType = medsEvent.Type;
        //                                if (!events.ContainsKey(eventType))
        //                                {
        //                                    events.Add(eventType, new FilteredCollection());
        //                                }

        //                                //// Do not display medication administration events of Significant Duration in the Timeline sample.
        //                                //// The display of medications of Significant Duration was not explored in the
        //                                //// 'Design Guidance – Timeline View' document. 
        //                                //// Any visual representation of these events in Timeline will need to be re-evaluated in line
        //                                //// with the more up-to-date guidance published in the 'Design Guidance – Drug Administration' document.
        //                                if (templateShortName != "ContinuousMedsAdminEvent" && String.Compare(medsEvent.Status, "Started", StringComparison.CurrentCultureIgnoreCase) != 0)
        //                                {
        //                                    events[eventType].Add(administrationPoint);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                foreach (KeyValuePair<string, FilteredCollection> medsEvents in events)
        //                {
        //                    if (!graphData.Activities.ContainsKey(medsEvents.Key))
        //                    {
        //                        graphData.Activities.Add(medsEvents.Key, new FilteredCollection());
        //                    }

        //                    graphData.Activities[medsEvents.Key] = medsEvents.Value;
        //                }
        //            }

        //            this.xmlGraphData.Add(graphData);
        //        nextgraph: int tmpgrp = 0;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //    }
        //}

        private void CreateGraphData(DataRow section, TimeSpan dateAdjustment)
        {
            try
            {
                //int flagprob = 0;
                //int flagmed = 0;
                //int flagmeas = 0;
                DataRow[] row = dtRow.Select("Section_Id='" + section["Section_Id"].ToString() + "'");

                for (int cnt = 0; cnt < row.Length; cnt++)
                {

                    TimelineGraphData graphData = null;
                    FilteredCollection data = new FilteredCollection();
                    Dictionary<string, FilteredCollection> events = new Dictionary<string, FilteredCollection>();


                    //if (row.IdSpecified)
                    //{
                    //    var graphs = (from graph in this.xmlGraphData
                    //                  where string.Compare(graph.GraphId, row.Id.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCultureIgnoreCase) == 0
                    //                  select graph).ToList<TimelineGraphData>();
                    //    if (graphs.Count > 0)
                    //    {
                    //        graphData = graphs[0];
                    //    }
                    //}

                    if (graphData == null)
                    {
                        graphData = new TimelineGraphData();
                        graphData.GraphId = row[cnt]["Id"].ToString();
                    }

                    graphData.Title = row[cnt]["Name"].ToString();
                    //switch (section["Name"].ToString())
                    //{



                    //    // case "CardioVascular":



                    //    //if ((GrphName == "CardioVascular") && (flagdata == 0))
                    //    //    goto nextgraph;
                    //    //else
                    //    //{
                    //    //    if (arrgraph.Contains(row[cnt]["Name"].ToString() ) == true)

                    //    //        break;
                    //    //    else
                    //    //        goto nextgraph;
                    //    //}



                    //}

                    graphData.SectionName = section["Name"].ToString();
                    graphData.Description = row[cnt]["Description"].ToString();
                    graphData.Data = data;
                    graphData.Background = GetBrushFromColorCode(row[cnt]["Background"].ToString());

                    if (row[cnt]["MaxLabelStackLevels"].ToString().Trim() != "")
                    {
                        graphData.MaxLabelStackLevels = Convert.ToInt32((row[cnt]["MaxLabelStackLevels"].ToString()));
                    }
                    else
                    {
                        graphData.MaxLabelStackLevels = 2;
                    }

                    if (!string.IsNullOrEmpty(row[cnt]["ShowLabelOvercrowdingNotifications"].ToString()))
                    {
                        graphData.ShowLabelOvercrowdingNotifications = bool.Parse(row[cnt]["ShowLabelOvercrowdingNotifications"].ToString());
                    }
                    else
                    {
                        graphData.ShowLabelOvercrowdingNotifications = false;
                    }

                   
                        //foreach (TimelineSampleDataScenarioSectionRowItem item in row.Item)

                        DataRow[] item = dtItem.Select("Row_Id='" + row[cnt]["Row_Id"].ToString() + "'");

                        if (item.Length != 0)
                        {
                            for (int itemcnt = 0; itemcnt < item.Length; itemcnt++)
                            {
                                //graphData.Style = GetResourceFromApp<Style>(item.Style);
                                //graphData.LabelTemplate = GetResourceFromApp<DataTemplate>(item.LabelTemplate);
                                //graphData.PointTemplate = GetResourceFromApp<DataTemplate>(item.PointTemplate);
                                //graphData.DataMarkerTemplate = GetResourceFromApp<DataTemplate>(item.DataMarkerTemplate);
                                //graphData.InterpolationLineColor = GetBrushFromColorCode(item.InterpolationLineColor);
                                //graphData.NormalRangeBrush = GetBrushFromColorCode(item.NormalRangeBrush);
                                //graphData.UnitsDescription = item.UnitsDescription;
                                //graphData.Units = item.Units;

                                //graphData.NormalRangeDescription = item.NormalRangeDescription;
                                //graphData.ShowNormalRange = string.IsNullOrEmpty(item.ShowNormalRange) ? false : bool.Parse(item.ShowNormalRange);

                                //if (item.YAxisPaddingSpecified)
                                //{
                                //    graphData.YAxisPadding = new Thickness((double)item.YAxisPadding);
                                //}
                                //else
                                //{
                                //    graphData.YAxisPadding = new Thickness(5);
                                //}

                                //if (item.NormalRangeMaximumValueSpecified)
                                //{
                                //    graphData.NormalRangeMaxValue = (double)item.NormalRangeMaximumValue;
                                //}

                                //if (item.NormalRangeMinimumValueSpecified)
                                //{
                                //    graphData.NormalRangeMinValue = (double)item.NormalRangeMinimumValue;
                                //}

                                //if (item.YAxisMaxValueSpecified)
                                //{
                                //    graphData.YAxisMaxValue = (double)item.YAxisMaxValue;
                                //}

                                //if (item.YAxisMinValueSpecified)
                                //{
                                //    graphData.YAxisMinValue = (double)item.YAxisMinValue;
                                //}

                                //if (item.YAxisMajorIntervalSpecified)
                                //{
                                //    graphData.YAxisMajorInterval = (double)item.YAxisMajorInterval;
                                //}

                                //if (item.YAxisIntervalMinimumHeightSpecified)
                                //{
                                //    graphData.YAxisIntervalMinHeight = (double)item.YAxisIntervalMinimumHeight;
                                //}

                                //if (!string.IsNullOrEmpty(item.StartDate))
                                //{
                                //    TimeActivityPoint timeActivityPoint = new TimeActivityPoint();
                                //    timeActivityPoint.StartDate = GetDate(item.StartDate, dateAdjustment).Value;
                                //    timeActivityPoint.EndDate = GetDate(item.EndDate, dateAdjustment);
                                //    timeActivityPoint.AdditionalInformation = GetMedicationDetails(item, null, dateAdjustment);
                                //    MedicationLabel label = new MedicationLabel();
                                //    if (!string.IsNullOrEmpty(item.Type) && string.Compare(item.Type, "SecondaryCareMedication", StringComparison.CurrentCultureIgnoreCase) == 0)
                                //    {
                                //        label.Mode = LabelMode.Full;
                                //    }
                                //    else
                                //    {
                                //        label.Mode = LabelMode.Simple;
                                //    }


                                graphData.Style = GetResourceFromApp<Style>(item[itemcnt]["Style"].ToString());
                                graphData.LabelTemplate = GetResourceFromApp<DataTemplate>(item[itemcnt]["LabelTemplate"].ToString());
                                graphData.PointTemplate = GetResourceFromApp<DataTemplate>(item[itemcnt]["PointTemplate"].ToString());
                                graphData.DataMarkerTemplate = GetResourceFromApp<DataTemplate>(item[itemcnt]["DataMarkerTemplate"].ToString());
                                graphData.InterpolationLineColor = GetBrushFromColorCode(item[itemcnt]["InterpolationLineColor"].ToString());
                                graphData.NormalRangeBrush = GetBrushFromColorCode(item[itemcnt]["NormalRangeBrush"].ToString());
                                graphData.UnitsDescription = item[itemcnt]["UnitsDescription"].ToString();
                                graphData.Units = item[itemcnt]["Units"].ToString();

                                graphData.NormalRangeDescription = item[itemcnt]["NormalRangeDescription"].ToString();
                                graphData.ShowNormalRange = string.IsNullOrEmpty(item[itemcnt]["ShowNormalRange"].ToString()) ? false : bool.Parse(item[itemcnt]["ShowNormalRange"].ToString());

                                if (item[itemcnt]["YAxisPadding"].ToString().Trim() != "")
                                {
                                    graphData.YAxisPadding = new Thickness(Convert.ToDouble(item[itemcnt]["YAxisPadding"].ToString()));
                                }
                                else
                                {
                                    graphData.YAxisPadding = new Thickness(5);
                                }

                                if (item[itemcnt]["NormalRangeMaximumValue"].ToString().Trim() != "")
                                {
                                    graphData.NormalRangeMaxValue = Convert.ToDouble(item[itemcnt]["NormalRangeMaximumValue"].ToString());
                                }

                                if (item[itemcnt]["NormalRangeMinimumValue"].ToString().Trim() != "")
                                {
                                    graphData.NormalRangeMinValue = Convert.ToDouble(item[itemcnt]["NormalRangeMinimumValue"].ToString());
                                }

                                if (item[itemcnt]["YAxisMaxValue"].ToString().Trim() != "")
                                {
                                    graphData.YAxisMaxValue = Convert.ToDouble(item[itemcnt]["YAxisMaxValue"].ToString());
                                }

                                if (item[itemcnt]["YAxisMinValue"].ToString().Trim() != "")
                                {
                                    graphData.YAxisMinValue = Convert.ToDouble(item[itemcnt]["YAxisMinValue"].ToString());
                                }

                                if (item[itemcnt]["YAxisMajorInterval"].ToString().Trim() != "")
                                {
                                    graphData.YAxisMajorInterval = Convert.ToDouble(item[itemcnt]["YAxisMajorInterval"].ToString());
                                }

                                if (item[itemcnt]["YAxisIntervalMinimumHeight"].ToString().Trim() != "")
                                {
                                    graphData.YAxisIntervalMinHeight = Convert.ToDouble(item[itemcnt]["YAxisIntervalMinimumHeight"].ToString());
                                }

                                if (!string.IsNullOrEmpty(item[itemcnt]["StartDate"].ToString()))
                                {
                                    TimeActivityPoint timeActivityPoint = new TimeActivityPoint();
                                    timeActivityPoint.StartDate = GetDate(item[itemcnt]["StartDate"].ToString(), dateAdjustment).Value;
                                    timeActivityPoint.EndDate = GetDate(item[itemcnt]["EndDate"].ToString(), dateAdjustment);
                                    timeActivityPoint.AdditionalInformation = GetMedicationDetails(item[itemcnt], null, dateAdjustment);
                                    MedicationLabel label = new MedicationLabel();
                                    if (!string.IsNullOrEmpty(item[itemcnt]["Type"].ToString()) && string.Compare(item[itemcnt]["Type"].ToString(), "SecondaryCareMedication", StringComparison.CurrentCultureIgnoreCase) == 0)
                                    {
                                        label.Mode = LabelMode.Full;
                                    }
                                    else
                                    {
                                        label.Mode = LabelMode.Simple;
                                    }



                                    if (label != null)
                                    {
                                        label.MedicationName = item[itemcnt]["Name"].ToString();
                                        label.Dose = item[itemcnt]["Dose"].ToString();
                                        label.Frequency = item[itemcnt]["Frequency"].ToString();
                                        label.Route = item[itemcnt]["Route"].ToString();
                                        label.FluidStrength = item[itemcnt]["FluidStrength"].ToString();
                                        label.SolidStrength = item[itemcnt]["SolidStrength"].ToString();
                                        label.Dose = item[itemcnt]["Dose"].ToString();
                                        label.DoseDuration = item[itemcnt]["DoseDuration"].ToString();
                                        label.DoseLabel = item[itemcnt]["DoseLabel"].ToString();
                                        label.BrandName = item[itemcnt]["Brand"].ToString();
                                        label.Form = item[itemcnt]["Form"].ToString();
                                    }


                                    string templateShortName;

                                    //    if (timeActivityPoint.EndDate.HasValue)
                                    //    {
                                    //        if (timeActivityPoint.StartDate != timeActivityPoint.EndDate)
                                    //        {
                                    //            templateShortName = "InterpolationLine";
                                    //        }
                                    //        else
                                    //        {
                                    //            templateShortName = "InterpolationLineOneTime";
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        templateShortName = "InterpolationLineOpenDuration";
                                    //    }
                                    //    string str = section.Name.ToUpper(CultureInfo.CurrentCulture).ToString();

                                    //    timeActivityPoint.DataMarkerTemplate = this.LayoutRoot.Resources[section.Name.ToUpper(CultureInfo.CurrentCulture) + "_" + templateShortName] as DataTemplate;
                                    //    timeActivityPoint.Label = label;
                                    //    graphData.LabelMode = label.Mode;
                                    //    data.Add(timeActivityPoint);
                                    //}
                                    if (timeActivityPoint.EndDate.HasValue)
                                    {
                                        if (timeActivityPoint.StartDate != timeActivityPoint.EndDate)
                                        {
                                            templateShortName = "InterpolationLine";
                                        }
                                        else
                                        {
                                            templateShortName = "InterpolationLineOneTime";
                                        }
                                    }
                                    else
                                    {
                                        templateShortName = "InterpolationLineOpenDuration";
                                    }
                                    string str = section["Name"].ToString().ToUpper(CultureInfo.CurrentCulture).ToString();

                                    timeActivityPoint.DataMarkerTemplate = this.LayoutRoot.Resources[section["Name"].ToString().ToUpper(CultureInfo.CurrentCulture) + "_" + templateShortName] as DataTemplate;
                                    timeActivityPoint.Label = label;
                                    graphData.LabelMode = label.Mode;
                                    data.Add(timeActivityPoint);
                                }

                                DataRow[] drevt = dtEvents.Select("Item_Id='" + item[itemcnt]["Item_Id"].ToString() + "'");
                                if (drevt.Length > 0)
                                {
                                    DataRow[] medsEvent = dtEvent.Select("Events_Id='" + drevt[0]["Events_Id"].ToString() + "'");
                                    if (medsEvent.Length > 0)
                                    {


                                        for (int intmedevt = 0; intmedevt < medsEvent.Length; intmedevt++)
                                        {
                                            if (Convert.ToString(medsEvent[intmedevt]["Type"]) == "GraphData")
                                            {
                                                TimePoint timePoint = new TimePoint();
                                                timePoint.DateTime = GetDate(medsEvent[intmedevt]["ActualStartDate"].ToString(), dateAdjustment).Value;
                                                timePoint.Y1 = Math.Round(Convert.ToDouble(medsEvent[intmedevt]["Y1"]), 2);
                                                graphData.Height = Convert.ToDouble(item[itemcnt]["Height"]);
                                                if (medsEvent[intmedevt]["Y2"].ToString().Trim() != "")
                                                {
                                                    timePoint.Y2 = Math.Round(Convert.ToDouble(medsEvent[intmedevt]["Y2"]), 2);
                                                    graphData.GraphType = GraphType.TimeIBar;
                                                }
                                                else
                                                {
                                                    graphData.GraphType = GraphType.TimeLine;
                                                }

                                                data.Add(timePoint);
                                            }
                                            else
                                            {
                                                TimeActivityPoint administrationPoint = new TimeActivityPoint();
                                                if (string.IsNullOrEmpty(medsEvent[intmedevt]["ActualStartDate"].ToString()))
                                                {
                                                    administrationPoint.StartDate = GetDate(medsEvent[intmedevt]["PlannedStartDate"].ToString(), dateAdjustment).Value;
                                                }
                                                else
                                                {
                                                    administrationPoint.StartDate = GetDate(medsEvent[intmedevt]["ActualStartDate"].ToString(), dateAdjustment).Value;
                                                }

                                                administrationPoint.EndDate = GetDate(medsEvent[intmedevt]["ActualEndDate"].ToString(), dateAdjustment);
                                                administrationPoint.AdditionalInformation = GetMedicationDetails(item[itemcnt], medsEvent[intmedevt], dateAdjustment);

                                                string templateShortName = "MedsAdminEvent";
                                                if (administrationPoint.EndDate.HasValue && administrationPoint.EndDate.Value > administrationPoint.StartDate)
                                                {
                                                    templateShortName = "ContinuousMedsAdminEvent";
                                                }
                                                else if (Convert.ToString(medsEvent[intmedevt]["Type"]) == "prescription issue events")
                                                {
                                                    templateShortName = "PrescriptionEvent";
                                                }

                                                administrationPoint.DataMarkerTemplate = this.LayoutRoot.Resources[templateShortName + "_" + medsEvent[intmedevt]["Status"].ToString().ToLower(CultureInfo.CurrentCulture)] as DataTemplate;

                                                string eventType = medsEvent[intmedevt]["Type"].ToString();
                                                if (!events.ContainsKey(eventType))
                                                {
                                                    events.Add(eventType, new FilteredCollection());
                                                }

                                                //// Do not display medication administration events of Significant Duration in the Timeline sample.
                                                //// The display of medications of Significant Duration was not explored in the
                                                //// 'Design Guidance – Timeline View' document. 
                                                //// Any visual representation of these events in Timeline will need to be re-evaluated in line
                                                //// with the more up-to-date guidance published in the 'Design Guidance – Drug Administration' document.
                                                if (templateShortName != "ContinuousMedsAdminEvent" && String.Compare(medsEvent[intmedevt]["Status"].ToString(), "Started", StringComparison.CurrentCultureIgnoreCase) != 0)
                                                {
                                                    events[eventType].Add(administrationPoint);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (KeyValuePair<string, FilteredCollection> medsEvents in events)
                            {
                                if (!graphData.Activities.ContainsKey(medsEvents.Key))
                                {
                                    graphData.Activities.Add(medsEvents.Key, new FilteredCollection());
                                }

                                graphData.Activities[medsEvents.Key] = medsEvents.Value;
                            }

                        }

                        this.xmlGraphData.Add(graphData);
                //    nextgraph: ;
                    //int tmpgrp = 0;
                   
                }
            }
            catch (Exception exp)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exp.Message, false);
            }
        }

        /// <summary>
        /// Gets the graph from graph data.
        /// </summary>
        /// <param name="graphData">The graph data.</param>
        /// <returns>TimeGraphBase object with the data set from the object.</returns>
        private TimeGraphBase GetGraphFromGraphData(TimelineGraphData graphData)
        {
            TimeGraphBase graph = null;
            switch (graphData.GraphType)
            {
                case GraphType.TimeActivity:
                    graph = new TimeActivityGraph();
                    graph.Style = this.Resources["MedsTimelineStyle"] as Style;
                    TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;

                    timeActivityGraph.MaxLabelStackLevels = graphData.MaxLabelStackLevels;
                    timeActivityGraph.ShowLabelOvercrowdingNotifications = graphData.ShowLabelOvercrowdingNotifications;
                    timeActivityGraph.LabelMode = graphData.LabelMode;
                    //if (string.Compare(graphData.SectionName, "Problems", StringComparison.CurrentCultureIgnoreCase) == 0)
                    //{
                    //    timeActivityGraph.LabelClick += new RoutedEventHandler(this.ProblemsGraph_LabelClick);
                    //    timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.ProblemsLine_MouseLeftButtonDown);
                    //}

                    //else
                    //{

                    //    if (string.Compare(graphData.SectionName, "Exams", StringComparison.CurrentCultureIgnoreCase) == 0)
                    //    {
                    //        timeActivityGraph.LabelClick += new RoutedEventHandler(this.Exams_LabelClick);
                    //        timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.Exams_MouseLeftButtonDown);
                    //    }

                    //    else
                    //    {

                    //        if (string.Compare(graphData.SectionName, "Lab Orders", StringComparison.CurrentCultureIgnoreCase) == 0)
                    //        {
                    //            timeActivityGraph.LabelClick += new RoutedEventHandler(this.Tests_LabelClick);
                    //            timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.Tests_MouseLeftButtonDown);
                    //        }

                    //        else
                    //        {
                    //            timeActivityGraph.ShowActivities = true;
                    //            if (timeActivityGraph.LabelMode == LabelMode.Simple)
                    //            {
                    //                timeActivityGraph.LabelClick += new RoutedEventHandler(this.MedicationsGraph_LabelClick);
                    //                timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.MedicationsLine_MouseLeftButtonDown);
                    //            }
                    //            else
                    //            {
                    //                timeActivityGraph.LabelClick += new RoutedEventHandler(this.SecondaryCareMedicationsGraph_LabelClick);
                    //                timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.SecondaryCareMedicationsLine_MouseLeftButtonDown);
                    //            }
                    //        }


                    //    }
                    //}


                  //  timeActivityGraph.ActivityClick += new RoutedEventHandler(this.Event_MouseLeftButtonDown);


                    if (string.IsNullOrEmpty(graphData.Title))
                    {
                        timeActivityGraph.VerticalAlignment = VerticalAlignment.Stretch;
                        timeActivityGraph.IsTabStop = false;
                        timeActivityGraph.Focusable = false;
                        timeActivityGraph.Style = this.Resources["EmptyTimelineStyle"] as Style;
                    }

                    break;
                case GraphType.TimeIBar:
                    graph = new TimeIBarGraph();
                    (graph as TimeIBarGraph).InterpolationLineColor = graphData.InterpolationLineColor;

                    break;
                case GraphType.TimeLine:
                    graph = new TimeLineGraph();
                    (graph as TimeLineGraph).InterpolationLineColor = graphData.InterpolationLineColor;
                    break;
            }

            if (!string.IsNullOrEmpty(graphData.GraphId))
            {
                graph.Tag = graphData.GraphId;
            }

            graph.DataContext = graphData.Data;
            graph.Title = graphData.Title;
            graph.Description = graphData.Description;
            graph.Background = graphData.Background;
            graph.LabelTemplate = graphData.LabelTemplate;
            graph.AddYAxisSeparator = false;

            TimeAndYGraphBase timeYGraph = graph as TimeAndYGraphBase;
            if (timeYGraph != null)
            {
                timeYGraph.ShowDataPointLabels = Visibility.Visible;
                timeYGraph.Style = this.Resources["TimelineStyle"] as Style;
                timeYGraph.Units = graphData.Units;
                timeYGraph.UnitsDescription = graphData.UnitsDescription;
                timeYGraph.DataMarkerTemplate = graphData.DataMarkerTemplate;
                timeYGraph.YAxisIntervalMinimumHeight = graphData.YAxisIntervalMinHeight;
                timeYGraph.YAxisMajorInterval = graphData.YAxisMajorInterval;
                timeYGraph.YAxisMaxValue = graphData.YAxisMaxValue;
                timeYGraph.YAxisMinValue = graphData.YAxisMinValue;
                timeYGraph.NormalRangeBrush = graphData.NormalRangeBrush;
                timeYGraph.ShowNormalRange = graphData.ShowNormalRange;
                timeYGraph.NormalRangeMinimumValue = graphData.NormalRangeMinValue;
                timeYGraph.NormalRangeMaximumValue = graphData.NormalRangeMaxValue;
                timeYGraph.NormalRangeDescription = graphData.NormalRangeDescription;
                timeYGraph.YAxisPadding = graphData.YAxisPadding;
            }

            TimeActivityGraphHost.SetSectionName(graph, graphData.SectionName.ToUpper(CultureInfo.CurrentCulture));

            if (!double.IsNaN(graphData.Height))
            {
                graph.Height = graphData.Height;
            }

            return graph;
        }

        /// <summary>
        /// Adds the Activities to graphs in a section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="activityTypeName">Name of the activity type.</param>
        private void AddActivity(string sectionName, string activityTypeName)
        {
            var graphs = from graph in this.TimeActivityGraphHost.Graphs
                         where string.Compare(TimeActivityGraphHost.GetSectionName(graph), sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graph;

            foreach (TimeActivityGraph graph in graphs)
            {
                string graphId = string.Empty;
                if (graph.Tag != null)
                {
                    graphId = graph.Tag.ToString();
                }

                foreach (TimelineGraphData data in this.xmlGraphData)
                {
                    if (string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (data.Activities.ContainsKey(activityTypeName))
                        {
                            if (!data.ActivityTypesDisplayed.Contains(activityTypeName))
                            {
                                data.ActivityTypesDisplayed.Add(activityTypeName);
                            }

                            graph.Activities.Add(data.Activities[activityTypeName]);
                        }

                        graph.Refresh();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Removes the activity.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="activityTypeName">Name of the activity type.</param>
        private void RemoveActivity(string sectionName, string activityTypeName)
        {
            var graphs = from graph in this.TimeActivityGraphHost.Graphs
                         where string.Compare(TimeActivityGraphHost.GetSectionName(graph), sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graph;

            foreach (TimeActivityGraph graph in graphs)
            {
                string graphId = string.Empty;
                if (graph.Tag != null)
                {
                    graphId = graph.Tag.ToString();
                }

                foreach (TimelineGraphData data in this.xmlGraphData)
                {
                    if (string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (data.Activities.ContainsKey(activityTypeName))
                        {
                            if (data.ActivityTypesDisplayed.Contains(activityTypeName))
                            {
                                data.ActivityTypesDisplayed.Remove(activityTypeName);
                            }

                            graph.Activities.Remove(data.Activities[activityTypeName]);
                        }

                        graph.Refresh();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Restores the focus on close of dialog.
        /// </summary>
        private void RestoreFocus(Control focusedElement)
        {
            if (focusedElement != null)
            {
                focusedElement.IsTabStop = false;
            }

            if (this.lastFocusedElement != null)
            {
                FocusHelper.FocusControl(this.lastFocusedElement);
                this.lastFocusedElement = null;
            }
        }

        /// <summary>
        /// Sets the focus in dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        private void SetFocusInDialog(ModalDialog dialog)
        {
            this.lastFocusedElement = Keyboard.FocusedElement as Control;
            Panel contentPanel = dialog.ContentPlaceHolder.Content as Panel;
            if (contentPanel != null)
            {
                Control elementToFocus = contentPanel.Children[contentPanel.Children.Count - 1] as Control;
                elementToFocus.IsTabStop = true;
                FocusHelper.FocusControl(elementToFocus);
            }
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the Loaded event of the TimeActivityGraphHostPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHostPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //   this.Primarycare.Click += new RoutedEventHandler(this.Scenario_Click);
            //  this.Secondarycare.Click += new RoutedEventHandler(this.Scenario_Click);
            //  this.selectedPatientId = this.Primarycare.IsChecked.Value ? this.Primarycare.Tag.ToString() : this.Secondarycare.Tag.ToString();
            this.selectedPatientId = "1";
            this.InitializeGraphs(true);
        }

        /// <summary>
        /// Handles the Click event of the Scenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Scenario_Click(object sender, RoutedEventArgs e)
        {
            string newPatientId = "1";//this.Primarycare.IsChecked.Value ? this.Primarycare.Tag.ToString() : this.Secondarycare.Tag.ToString();
            if (string.Compare(newPatientId, this.selectedPatientId, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                this.selectedPatientId = newPatientId;
                this.TimeActivityGraphHost.NowDateTime = DateTime.Now;
                this.InitializeGraphs(true);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the TimeActivityGraphHostPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHostPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool ctrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            bool shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

            if (ctrl && shift)
            {
                TimeGraphBase graph = Keyboard.FocusedElement as TimeGraphBase;
                if (graph != null)
                {
                    LevelOfDetail lod = ItemsControl.ItemsControlFromItemContainer(graph).FindName("LevelOfDetail") as LevelOfDetail;
                    if (lod != null)
                    {
                        switch (e.Key)
                        {
                            case Key.Add:
                            case Key.OemPlus:
                                lod.CurrentLevel++;
                                e.Handled = true;
                                break;
                            case Key.Subtract:
                            case Key.OemMinus:
                                lod.CurrentLevel--;
                                e.Handled = true;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the OnLevelOfDetailChange event of the LevelOfDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LevelOfDetail_OnLevelOfDetailChange(object sender, EventArgs e)
        {
            this.TimeActivityGraphHost.ShowLoadingScreen();

            System.Windows.Threading.DispatcherTimer lodTimer = new System.Windows.Threading.DispatcherTimer();
            lodTimer.Tick += delegate(object s, EventArgs args)
            {
                LevelOfDetail lodControl = sender as LevelOfDetail;
                GraphSection section = lodControl.DataContext as GraphSection;
                ScrollViewer scrollViewer = lodControl.FindName("ScrollViewer") as ScrollViewer;
                RegisterScrollViewerPosition(lodControl, scrollViewer, section);

                UpdateGraphProperties(lodControl.CurrentLevel, section);
                if (lodControl.CurrentLevel < MinimumLevelOfDetailForActivities)
                {
                    Panel activityTypeList = lodControl.FindName("ActivityTypeList") as Panel;
                    if (activityTypeList != null)
                    {
                        foreach (UIElement child in activityTypeList.Children)
                        {
                            CheckBox checkBox = child as CheckBox;
                            if (checkBox != null)
                            {
                                checkBox.IsChecked = false;
                            }
                        }
                    }
                }

                FrameworkElement lodMinMessage = lodControl.FindName("LODMinMessage") as FrameworkElement;
                if (lodMinMessage != null)
                {


                    lodMinMessage.Visibility = Visibility.Collapsed;



                }

                lodTimer.Stop();
                this.TimeActivityGraphHost.HideLoadingScreen();
            };

            lodTimer.Interval = TimeSpan.FromTicks(1);
            lodTimer.Start();
        }

        /// <summary>
        /// Handles the SectionInitialized event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_SectionInitialized(object sender, RoutedEventArgs e)
        {
            GraphSection section = sender as GraphSection;
            if (section != null)
            {
                //if ((string.Compare(section.SectionName, "Vitals", StringComparison.CurrentCultureIgnoreCase) == 0) || (string.Compare(section.SectionName, "Labs", StringComparison.CurrentCultureIgnoreCase) == 0))
                //{
                    section.HeaderTemplate = this.LayoutRoot.Resources["ObsSectionTemplate"] as DataTemplate;
                //}
                //else
                //{
                //    section.HeaderTemplate = this.LayoutRoot.Resources["SectionTemplate"] as DataTemplate;
                //}
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the ScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new ScrollChangedEventHandler(this.TimeActivityGraphHost_ScrollChanged);
                UpdateOutOfViewValues(scrollViewer);
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the SectionRoot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        ArrayList arrsecname = new ArrayList();
        FrameworkElement[] frsecroot;
       // int cntsec = 0;
        int flagsecmousedown = 0;
        private void SectionRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (flagsecmousedown == 0)
            {
                FrameworkElement sectionRoot = sender as FrameworkElement;
               // sectionRoot.Height = 200;

                //string name = (sectionRoot.FindName("secname") as TextBlock).Text;
                //if (arrsecname.IndexOf(name) == -1)
                //{
                //    arrsecname.Add(name);
                //    Array.Resize(ref frsecroot, arrsecname.Count);

                //    if (cntsec < frsecroot.Length)
                //    {
                //        frsecroot[cntsec] = sectionRoot;
                //        cntsec += 1;
                //    }
                //}
            }
            //  bool visible = e.NewSize.Height > this.TimeActivityGraphHost.SectionMinHeight;
            //   SetVisibility(sectionRoot.FindName("OutOfViewIndicators"), visible);
            //   SetVisibility(sectionRoot.FindName("GraphArea"), visible);
            //  SetVisibility(sectionRoot.FindName("SectionControlsPanel"), visible);
            //  SetVisibility(sectionRoot.FindName("OutOfViewTitle"), visible);
        }

        /// <summary>
        /// Handles the ScrollChanged event of the Section headers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.ScrollChangedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                UpdateOutOfViewValues(scrollViewer);
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the ItemsControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsControl itemsControl = sender as ItemsControl;
            ScrollViewer scrollViewer = itemsControl.FindName("ScrollViewer") as ScrollViewer;
            GraphSection graphSection = scrollViewer.DataContext as GraphSection;

            if (itemsControl.Items.Count == graphSection.Graphs.Count)
            {
                object focusedElement = Keyboard.FocusedElement;
                bool checkTag = true;
                if (focusedElement != null)
                {
                    if (itemsControl.Items.Contains(focusedElement))
                    {
                        checkTag = false;
                    }
                }

                if (!checkTag)
                {
                    TimeGraphBase graph = focusedElement as TimeGraphBase;
                    if (graph != null)
                    {
                        double margin = (scrollViewer.ViewportHeight - graph.ActualHeight) / 2;
                        Rect slot = LayoutInformation.GetLayoutSlot(graph);
                        scrollViewer.ScrollToVerticalOffset(Math.Max(0, slot.Top - margin));
                    }
                }
                else
                {
                    LevelOfDetail lod = itemsControl.FindName("LevelOfDetail") as LevelOfDetail;
                    if (lod.Tag != null)
                    {
                        int index = int.Parse(lod.Tag.ToString(), CultureInfo.CurrentCulture);
                        if (index == int.MinValue)
                        {
                            scrollViewer.ScrollToTop();
                        }
                        else if (index == int.MaxValue)
                        {
                            scrollViewer.ScrollToBottom();
                        }
                        else
                        {
                            TimeGraphBase graph = itemsControl.Items[index] as TimeGraphBase;
                            double margin = (scrollViewer.ViewportHeight - graph.ActualHeight) / 2;
                            Rect slot = LayoutInformation.GetLayoutSlot(graph);
                            scrollViewer.ScrollToVerticalOffset(Math.Max(0, slot.Top - margin));
                        }

                        lod.Tag = null;
                    }
                    else
                    {
                        scrollViewer.ScrollToTop();
                    }
                }

                UpdateGraphSectionValues(scrollViewer, itemsControl, graphSection);
            }
        }

        /// <summary>
        /// Handles the Click event of the ItemsOutOfViewBottomArrow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemsOutOfViewBottomArrow_Click(object sender, RoutedEventArgs e)
        {
            Button outOfViewButton = sender as Button;
            ScrollViewer scrollViewer = outOfViewButton.FindName("ScrollViewer") as ScrollViewer;
            ItemsControl itemsControl = outOfViewButton.FindName("ItemsControl") as ItemsControl;
            int currentOutOfViewItem = int.Parse(((outOfViewButton.Content as Panel).Children[0] as TextBlock).Text, CultureInfo.CurrentCulture);

            int itemCount = itemsControl.Items.Count;
            if (itemCount > 0)
            {
                TimeActivityGraph dummyGraph = itemsControl.Items[itemCount - 1] as TimeActivityGraph;
                if (dummyGraph != null && string.IsNullOrEmpty(dummyGraph.Title))
                {
                    itemCount--;
                }
            }

            FrameworkElement topElement = itemsControl.Items[itemCount - currentOutOfViewItem] as FrameworkElement;
            Rect bounds = LayoutInformation.GetLayoutSlot(topElement);
            scrollViewer.ScrollToVerticalOffset(bounds.Top);
        }

        /// <summary>
        /// Handles the Click event of the ItemsOutOfViewTopArrow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemsOutOfViewTopArrow_Click(object sender, RoutedEventArgs e)
        {
            Button outOfViewButton = sender as Button;
            ScrollViewer scrollViewer = outOfViewButton.FindName("ScrollViewer") as ScrollViewer;
            ItemsControl itemsControl = outOfViewButton.FindName("ItemsControl") as ItemsControl;
            int currentOutOfViewItem = int.Parse(((outOfViewButton.Content as Panel).Children[1] as TextBlock).Text, CultureInfo.CurrentCulture);
            Rect itemBounds = LayoutInformation.GetLayoutSlot(itemsControl.Items[currentOutOfViewItem - 1] as FrameworkElement);

            scrollViewer.ScrollToVerticalOffset(itemBounds.Top - scrollViewer.ViewportHeight);
        }

        /// <summary>
        /// Handles the SectionReset event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_SectionReset(object sender, RoutedEventArgs e)
        {



           FrameworkElement sectionRoot = sender as FrameworkElement;
            //sectionRoot.Height = 200;

            //string name = (sectionRoot.FindName("secname") as TextBlock).Text;
            //if (arrsecname.IndexOf(name) != -1)
            //{




            //    LevelOfDetail lodControl = sectionRoot.FindName("LevelOfDetail") as LevelOfDetail;

            //    if (lodControl.CurrentLevel != 2)
            //    {
            //        lodControl.CurrentLevel = 2;
            //    }
            //    else
            //    {
            //        UpdateGraphProperties(2, lodControl.DataContext as GraphSection);
            //    }

               SetVisibility(sectionRoot.FindName("OutOfViewIndicators"), true);
                SetVisibility(sectionRoot.FindName("GraphArea"), true);
               SetVisibility(sectionRoot.FindName("SectionControlsPanel"), true);

              ScrollViewer scrollViewer = sectionRoot.FindName("ScrollViewer") as ScrollViewer;
                scrollViewer.ScrollToVerticalOffset(0);


            }






            //FrameworkElement sectionRoot = sender as FrameworkElement;
            //LevelOfDetail lodControl = sectionRoot.FindName("LevelOfDetail") as LevelOfDetail;

            //if (lodControl.CurrentLevel != 2)
            //{
            //    lodControl.CurrentLevel = 2;
            //}
            //else
            //{
            //    UpdateGraphProperties(2, lodControl.DataContext as GraphSection);
            //}

            //SetVisibility(sectionRoot.FindName("OutOfViewIndicators"), true);
            //SetVisibility(sectionRoot.FindName("GraphArea"), true);
            //SetVisibility(sectionRoot.FindName("SectionControlsPanel"), true);

            //ScrollViewer scrollViewer = sectionRoot.FindName("ScrollViewer") as ScrollViewer;
            //scrollViewer.ScrollToVerticalOffset(0);
       //}

        /// <summary>
        /// Handles the Refresh event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_Refresh(object sender, RoutedEventArgs e)
        {
            this.InitializeGraphs(false);
        }

        /// <summary>
        /// Handles the Loaded event of the ActivityTypesList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ActivityTypesList_Loaded(object sender, RoutedEventArgs e)
        {
            Panel activityTypesList = sender as Panel;
            activityTypesList.Children.Clear();

            string sectionName = activityTypesList.Tag.ToString();
            List<string> administrationTypes = new List<string>();
            var result = from graphData in this.xmlGraphData
                         where string.Compare(graphData.SectionName, sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graphData;

            foreach (TimelineGraphData graph in result)
            {
                foreach (string key in graph.Activities.Keys)
                {
                    if (!administrationTypes.Contains(key))
                    {
                        administrationTypes.Add(key);
                        CheckBox checkBox = new CheckBox();
                        checkBox.Content = key;
                        checkBox.Tag = graph.SectionName;
                        checkBox.Margin = new Thickness(5, 0, 0, 0);
                        checkBox.VerticalAlignment = VerticalAlignment.Center;
                        checkBox.Foreground = this.LayoutRoot.Resources["SectionHeader_Foreground_Brush"] as Brush;
                        checkBox.FontWeight = FontWeights.Bold;
                        checkBox.Click += new RoutedEventHandler(this.CheckBox_Click);
                        activityTypesList.Children.Add(checkBox);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.TimeActivityGraphHost.ShowLoadingScreen();

            CheckBox activityTypeCheckBox = sender as CheckBox;
            Panel activityTypeList = (sender as FrameworkElement).Parent as Panel;
            ScrollViewer scrollViewer = activityTypeList.FindName("ScrollViewer") as ScrollViewer;
            LevelOfDetail lodControl = activityTypeList.FindName("LevelOfDetail") as LevelOfDetail;
            RegisterScrollViewerPosition(lodControl, scrollViewer, lodControl.DataContext as GraphSection);

            System.Windows.Threading.DispatcherTimer activityTimer = new System.Windows.Threading.DispatcherTimer();
            activityTimer.Tick += delegate(object s, EventArgs args)
            {
                if (activityTypeCheckBox.IsChecked.Value)
                {
                    if (lodControl.CurrentLevel < MinimumLevelOfDetailForActivities && activityTypeCheckBox.IsChecked.Value)
                    {
                        lodControl.CurrentLevel = MinimumLevelOfDetailForActivities;
                    }

                    this.AddActivity(activityTypeList.Tag.ToString(), activityTypeCheckBox.Content.ToString());
                }
                else
                {
                    this.RemoveActivity(activityTypeList.Tag.ToString(), activityTypeCheckBox.Content.ToString());
                }

                activityTimer.Stop();
                this.TimeActivityGraphHost.HideLoadingScreen();
            };

            activityTimer.Interval = TimeSpan.FromTicks(1);
            activityTimer.Start();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the MedicationsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MedicationsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.MedsDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.MedsDetailsDialog.DataContext = graphPoint.DataContext;
            this.MedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ProblemsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>


        private void ExamsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ExamDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ExamDetailsDialog.DataContext = graphPoint.DataContext;
            this.ExamDetailsDialog.Show();
        }



        private void Exams_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ExamDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ExamDetailsDialog.DataContext = graphPoint.DataContext;
            this.ExamDetailsDialog.Show();
        }
        private void Tests_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.TestDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.TestDetailsDialog.DataContext = graphPoint.DataContext;
            this.TestDetailsDialog.PredictFocus(FocusNavigationDirection.Down);
            this.TestDetailsDialog.Show();
        }

        private void ProblemsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ProblemsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ProblemsDialog.DataContext = graphPoint.DataContext;
            this.ProblemsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Event_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.EventsDialog);
            this.EventsDialog.DataContext = (sender as FrameworkElement).DataContext;
            (this.EventsDialog.DialogContent as ActivityDialog).UpdateDisplay();
            this.EventsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the TimeActivityGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        /// 


        private void Exams_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ExamDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ExamDetailsDialog.DataContext = graphPoint.DataContext;
            this.ExamDetailsDialog.Show();
        }
        private void Tests_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.TestDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.TestDetailsDialog.DataContext = graphPoint.DataContext;
            this.TestDetailsDialog.Show();
        }


        //private void Exams_LabelClick(object sender, RoutedEventArgs e)
        //{
        //    this.SetFocusInDialog(this.ExamDetailsDialog);
        //    GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
        //    this.ExamDetailsDialog.DataContext = graphPoint.DataContext;
        //    this.ExamDetailsDialog.Show();
        //}
        private void MedicationsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.MedsDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.MedsDetailsDialog.DataContext = graphPoint.DataContext;
            this.MedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the ProblemsGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ProblemsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ProblemsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ProblemsDialog.DataContext = graphPoint.DataContext;
            this.ProblemsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the MedicationsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SecondaryCareMedicationsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.SecondaryCareMedsDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.SecondaryCareMedsDetailsDialog.DataContext = graphPoint.DataContext;
            ((this.SecondaryCareMedsDetailsDialog.DialogContent as Grid).Children[1] as MedicationLabel).GetDesiredWidth(this.MaxWidth);
            this.SecondaryCareMedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the TimeActivityGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SecondaryCareMedicationsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.SecondaryCareMedsDetailsDialog);
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.SecondaryCareMedsDetailsDialog.DataContext = graphPoint.DataContext;
            ((this.SecondaryCareMedsDetailsDialog.DialogContent as Grid).Children[1] as MedicationLabel).GetDesiredWidth(this.MaxWidth);
            this.SecondaryCareMedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the Click event of the CloseDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseMedsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.MedsDetailsDialog.Hide();
        }


        private void CloseTestDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.TestDetailsDialog.Hide();
        }


        private void OpenTestDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.TestDetailsDialog.Hide();
        }

        /// <summary>
        /// Handles the Close button click event of the SecondaryCareMedsDetailsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseSecondaryCareMedsDetailsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.SecondaryCareMedsDetailsDialog.Hide();
        }

        /// <summary>
        /// Handles the Click event of the CloseProblemsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseProblemsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.ProblemsDialog.Hide();
        }


        private void OpenProblemsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.ProblemsDialog.Hide();

            this.ProblemID = ((LabeledContentControl)(this.ProblemsDialog.DialogContent as Grid).Children[1]).Content.ToString();
            this.VisitID = ((LabeledContentControl)(this.ProblemsDialog.DialogContent as Grid).Children[2]).Content.ToString();
            onProblemEventclick(sender, e);
        }
        /// <summary>
        /// Handles the Click event of the CloseEventsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseEventsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.EventsDialog.Hide();
        }

        private void OpenExamsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.ExamDetailsDialog.Hide();

            this.ExamName = ((LabeledContentControl)(this.ExamDetailsDialog.DialogContent as Grid).Children[2]).Content.ToString();
            this.ExamID = ((LabeledContentControl)(this.ExamDetailsDialog.DialogContent as Grid).Children[3]).Content.ToString();
            this.ExamDos = ((LabeledContentControl)(this.ExamDetailsDialog.DialogContent as Grid).Children[4]).Content.ToString();
            string strexamstatus = ((LabeledContentControl)(this.ExamDetailsDialog.DialogContent as Grid).Children[5]).Content.ToString();
            this.VisitID = ((LabeledContentControl)(this.ExamDetailsDialog.DialogContent as Grid).Children[6]).Content.ToString();
            // this.Exam_Name = strexamname;
            //  this.Exam_ID = strexamid;
            // this.ExamDos = strexamdos;
            bool exstatus = false;
            if (strexamstatus == "False")
                exstatus = false;
            else
                exstatus = true;
            this.ExamStatus = exstatus;
            onclick(sender, e);
            // this.SecondaryCareMedsDetailsDialog.Show();

            //string str2 = ((Microsoft.Cui.Controls.Label )ExamDetailsDialog.FindName("lblexname")).Text.ToString(); 
        }


        private void CloseExamsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.ExamDetailsDialog.Hide();
        }
        #endregion

        private void chkProb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.xmlGraphData.Clear();
                this.ReadGraphData();
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                this.TimeActivityGraphHost.Graphs.Clear();
                this.AddGraphsToHost();
                this.TimeActivityGraphHost.RefreshLayout();
            }
            catch
            {
            
            }
            }

        private void SectionRoot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
            
            flagsecmousedown = 1;
           // FrameworkElement SenderRoot = (FrameworkElement)sender;
           // SenderRoot.Height = 400;
           // string name = (SenderRoot.FindName("secname") as TextBlock).Text;
            //SetVisibility(SenderRoot.FindName("OutOfViewIndicators"), true );
            //SetVisibility(SenderRoot.FindName("GraphArea"), true);
            //SetVisibility(SenderRoot.FindName("SectionControlsPanel"), true);
            //SetVisibility(SenderRoot.FindName("OutOfViewTitle"), true);
            //for (int countsection = 0; countsection < frsecroot.Length; countsection++)
            //{
            //    FrameworkElement sectionRoot2 = frsecroot[countsection];


            //    string name2 = (sectionRoot2.FindName("secname") as TextBlock).Text;
            //    if (name != name2)
            //    {

            //        for (int arrcnt = 0; arrcnt < arrgraph.Count; arrcnt++)
            //        {
            //            if (string.Compare(name2.Trim(), arrgraph[arrcnt].ToString().Trim(), true) == 0)
            //            {
            //                sectionRoot2.Height = 30;
            //                break;
            //            }
            //        }

            //    }
            //}
            // flagsecmousedown = 0; 

            }
            catch
            { }
        }

}


}
