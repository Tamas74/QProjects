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
using gloPatientPortal.UserControls;

namespace gloPatientPortal
{
    /// <summary>
    /// Interaction logic for frmHealthFormGrp.xaml
    /// </summary>
    public partial class frmHistoryForm : Window
    {
        UcHealthFormGrp objHealthFormGrp = null;
        string _strConnectionString = string.Empty;
        long _nLoginID;

        public frmHistoryForm(string strConnectionString, long nLoginID)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
        }
      
        private void btnGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objHealthFormGrp == null)
                {
                    objHealthFormGrp = new UcHealthFormGrp(_strConnectionString, _nLoginID);
                    stkPanel.Children.Add(objHealthFormGrp);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

      
    }
}
