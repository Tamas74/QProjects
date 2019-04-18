using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace gloSurescriptSecureMessage
{
    class PagingViewModel : INotifyPropertyChanged
    {
        private int _numberOfPages;
        private int _pageIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,new PropertyChangedEventArgs(name));
            }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; OnPropertyChanged("PageIndex"); }
        }

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; OnPropertyChanged("NumberOfPages"); }
        }

        public void GetData(string DirectAddress, RequestFrom oRequestFrom)
        {
            clsSecureMessageDB oSecureMsg = new clsSecureMessageDB();
            //dsInboxAndMailCount = new DataSet();
            //dsInboxAndMailCount = oSecureMsg.RetrieveMails(DirectAddress, oRequestFrom);

            //dtInbox = dsInboxAndMailCount.Tables[0];
            //var dataReturned = oSecureMsg.RetrieveMails(DirectAddress, oRequestFrom);
            //NumberOfPages = dataReturned.PageCount;
            //PageIndex = dataReturned.PageIndex;
        }
    }
}
