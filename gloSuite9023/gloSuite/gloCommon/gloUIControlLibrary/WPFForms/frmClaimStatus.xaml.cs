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
using gloUIControlLibrary.Classes.ClaimStatus;
using System.ComponentModel;



namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmClaimStatus.xaml
    /// </summary>
    public partial class frmClaimStatus : Window
    {
        ICollectionView view;

        public frmClaimStatus()
        {
            InitializeComponent();
        }


        public frmClaimStatus(List<ClaimStatusInfo> ClaimStatusInfoList)
        {
            InitializeComponent();
            ClaimStatusInfoObservable myList = new ClaimStatusInfoObservable();

            try
            {

                foreach (ClaimStatusInfo claimStatusInfo in ClaimStatusInfoList)
                {
                    txtClaimNumber.Text = claimStatusInfo.ClaimNumber;
                    if (claimStatusInfo.PayerId != "" && claimStatusInfo.PayerName != "")
                    {
                        txtPayer.Text = claimStatusInfo.PayerId + " : " + claimStatusInfo.PayerName;
                    }
                    myList.ClaimStatusInfoList.Add(claimStatusInfo);
                }

                
                view = CollectionViewSource.GetDefaultView(myList.ClaimStatusInfoList);
                this.lbClaimStatus.ItemsSource = view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void btnSaveandClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    
    }
}
