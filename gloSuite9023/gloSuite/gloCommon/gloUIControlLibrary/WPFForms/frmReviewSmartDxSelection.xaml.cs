using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel ;
using gloUIControlLibrary.Classes.SmartDX;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmReviewSmartDxSelection.xaml
    /// </summary>
    public partial class frmReviewSmartDxSelection : Window
    {
        ICollectionView view;
        clsSmartDX myDisplayList = new clsSmartDX();
        clsSmartDX myFullList = new clsSmartDX();

        public frmReviewSmartDxSelection(clsSmartDX myList)
        {

            InitializeComponent();

            myFullList = myList;
            //lbPersonList.ItemsSource = myList;
            if (myList.TreatmentList != null)
            {
                foreach (clsSmartDXDisplay myDisp in myList.TreatmentList)
                {
                    if (myDisplayList.TreatmentList != null)
                    {
                        if (myDisplayList.TreatmentList.Count > 0)
                        {
                            clsSmartDXDisplay myCptDisplay = (from column in myDisplayList.TreatmentList
                                                              where column.DisplayName == myDisp.DisplayName
                                                              select column).FirstOrDefault();

                            if (myCptDisplay == null)
                            {
                                myDisplayList.TreatmentList.Add(myDisp);
                            }
                        }
                        else
                        {
                            myDisplayList.TreatmentList.Add(myDisp);
                        }
                    }
                    else
                    {
                        myDisplayList.TreatmentList = new ObservableCollection<clsSmartDXDisplay>();
                        myDisplayList.TreatmentList.Add(myDisp);
                    }
                }
            }
            view = CollectionViewSource.GetDefaultView(myDisplayList.TreatmentList);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
            view.SortDescriptions.Add(new SortDescription("SortId", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("DisplayName", ListSortDirection.Ascending));
            lbPersonList.ItemsSource = view;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void lbPersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbPersonList.Items.Count > 1)
                {
                    clsSmartDXDisplay item = (clsSmartDXDisplay)lbPersonList.SelectedItem;
                    if (item != null)
                    {
                        if (item.Type == "ICD-10" || item.Type == "ICD-9")
                        {
                            if (lbPersonList.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                            {
                                foreach (var lbitem in lbPersonList.Items)
                                {
                                    ListBoxItem lbitem2 = lbPersonList.ItemContainerGenerator.ContainerFromItem(lbitem) as ListBoxItem;
                                    clsSmartDXDisplay item2 = (clsSmartDXDisplay)lbitem;

                                    if (item2.Type != "ICD-10" && item2.Type != "ICD-9")
                                    {
                                        clsSmartDXDisplay[] myCodeDisplay = (from column in myFullList.TreatmentList
                                                                             where (column.Id == item2.Id && column.ICDId == item.ICDId)
                                                                             select column).ToArray();
                                        if (myCodeDisplay.Length > 0)
                                        {
                                            for (int k = 0; k < myCodeDisplay.Length; k++)
                                            {
                                                if (myCodeDisplay[k].ICDId == item.ICDId && myCodeDisplay[k].Id == item2.Id)
                                                {
                                                    lbitem2.FontWeight = FontWeights.Bold;
                                                    lbitem2.Foreground = Brushes.Black;
                                                    lbitem2.Background = Brushes.Transparent;

                                                }
                                                else
                                                {
                                                    lbitem2.FontWeight = FontWeights.Normal;
                                                    lbitem2.Foreground = Brushes.Black;
                                                    lbitem2.Background = Brushes.Transparent;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lbitem2.FontWeight = FontWeights.Normal;
                                            lbitem2.Foreground = Brushes.Black;
                                            lbitem2.Background = Brushes.Transparent;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //if (lbPersonList.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                            //{
                            //    foreach (var lbitem in lbPersonList.Items)
                            //    {
                            //        ListBoxItem lbitem2 = lbPersonList.ItemContainerGenerator.ContainerFromItem(lbitem) as ListBoxItem;
                            //        clsSmartDXDisplay item2 = (clsSmartDXDisplay)lbitem;

                            //        if (item2.Type == "ICD-10" || item2.Type == "ICD-9")
                            //        {

                            //            clsSmartDXDisplay[] myCodeDisplay = (from column in myFullList.TreatmentList
                            //                                                 where (column.Id == item.Id && column.ICDId==item2.ICDId )
                            //                                                 select column).ToArray();
                            //            if (myCodeDisplay.Length > 0)
                            //            {
                            //                for (int k = 0; k < myCodeDisplay.Length; k++)
                            //                {
                            //                    if (myCodeDisplay[k].ICDId == item2.ICDId && myCodeDisplay[k].Id == item.Id)
                            //                    {
                            //                        lbitem2.FontWeight = FontWeights.Bold;
                            //                        lbitem2.Foreground = Brushes.Black;
                            //                        lbitem2.Background = Brushes.Transparent;
                            //                    }
                            //                    else
                            //                    {
                            //                        lbitem2.FontWeight = FontWeights.Normal;
                            //                        lbitem2.Foreground = Brushes.Black;
                            //                        lbitem2.Background = Brushes.Transparent;
                            //                    }
                            //                }
                            //            }
                            //            else
                            //            {
                            //                lbitem2.FontWeight = FontWeights.Normal;
                            //                lbitem2.Foreground = Brushes.Black;
                            //                lbitem2.Background = Brushes.Transparent;
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void changeHeightAsPerResolution()
        {
            ////this.Height = 
            //Int32 myScreenHeight = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99);
            //if (myScreenHeight < this.Height)
            //{
            //    this.Height = myScreenHeight;
            //}
            ////Int32 myScreenWidth = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 1.1);
            ////if (myScreenWidth > this.width)
            ////{
            ////    this.width = myScreenWidth;
            ////}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Top = 0;
        }
    }
     
}

