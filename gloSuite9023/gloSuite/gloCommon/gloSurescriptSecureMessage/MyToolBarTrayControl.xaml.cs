using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Data;
using System.ComponentModel;
using System.Windows.Interop;
using System.Collections.Generic;
using gloSurescriptSecureMessage;
using System.Linq;
using System.Collections;

namespace gloSurescriptSecureMessage_InBox
{
    public class InboxProviderChangedEventArgs : EventArgs
    {
        private DirectUserProviderAssociation _ChangedToProvider = null;

        public InboxProviderChangedEventArgs(DirectUserProviderAssociation ChangedToProvider)
        {
            this._ChangedToProvider = DirectUserProviderAssociation.CloneObject(ChangedToProvider);                        
        }

        #region "Public Properties"

        public Int64 AssociationID { get { return this._ChangedToProvider.AssociationID; } }

        public Int64 UserID { get{return this._ChangedToProvider.UserID;} }

        public Int64 ProviderID { get { return this._ChangedToProvider.ProviderID; } }

        public string FirstName { get { return this._ChangedToProvider.FirstName; } }

        //public string MiddleName { get { return this._ChangedToProvider.MiddleName; } }

        public string LastName { get { return this._ChangedToProvider.LastName; } }

        public string DirectAddress { get { return this._ChangedToProvider.DirectAddress; } }

        public string SSPID { get { return this._ChangedToProvider.SSPID; } }

        public string TextRepresentation { get { return this.FirstName + " - " + this.DirectAddress; } }

        public string Name { get { return this._ChangedToProvider.Name;} }

        public string FirstAndLastName { get { return this.FirstName + " " + this.LastName; } }
        #endregion
    }

    public delegate void ProviderInboxChangedEventHandler(object sender, InboxProviderChangedEventArgs InboxProviderChangedArgs);    

    public partial class MyToolBarTrayControl
    {

        /** 
        * Event handlers for MyImageTextMenuButton controls.
        */
        private RoutedEventHandler contextMenuClosedHandler;
        private Border buttonBorder;
                             
        public MyToolBarTrayControl()
        {
            try
            {
                this.InitializeComponent();
                contextMenuClosedHandler = new RoutedEventHandler(ContextMenu_Closed);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

       
        private void triangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = sender as Button;
                if (b != null)
                {
                    buttonBorder = ((Grid)((Control)sender).Parent).Parent as Border;
                    if (buttonBorder != null)
                    {
                        buttonBorder.ContextMenu.PlacementTarget = buttonBorder;
                        buttonBorder.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                        ContextMenuService.SetPlacement(buttonBorder, System.Windows.Controls.Primitives.PlacementMode.Bottom);
                        buttonBorder.ContextMenu.IsOpen = true;
                        buttonBorder.Background = (Brush)MyApp.Current.Resources["MyBlueGradientBrush"];
                        buttonBorder.BorderBrush = (Brush)MyApp.Current.Resources["MyDarkBlueSolidBrush"];
                        buttonBorder.ContextMenu.Closed += contextMenuClosedHandler;
                    }
                }
            }
            catch (Exception ex)
            {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            try
            {
                buttonBorder.ClearValue(Border.BackgroundProperty);
                buttonBorder.ClearValue(Border.BorderBrushProperty);
                buttonBorder.ContextMenu.Closed -= contextMenuClosedHandler;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void ComboBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                ComboBox cbx = ((ComboBox)sender);

                if ((bool)e.NewValue && cbx.Text == (string)cbx.ToolTip)
                {
                    cbx.Text = "";
                    cbx.Foreground = Brushes.Black;
                }
                else if (cbx.Text == "")
                {
                    cbx.Text = (string)cbx.ToolTip;
                    cbx.Foreground = Brushes.LightGray;
                }
            }
            catch (Exception ex)
            {
             gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            Window parentwin = Window.GetWindow(myToolBarTrayControl);
            parentwin.Close();
        }

      

        #region "Multiple Providers"

        public event ProviderInboxChangedEventHandler ProviderInboxChangedEvent;

        private List<DirectUserProviderAssociation> _listOfProviders;

        private Dictionary<Int64, DirectUserProviderAssociation> dictionaryProviders;

        public void DisplayProviderInbox(Boolean value)
        {
            try
            {
                if (value)
                { this.stkProviderInbox.Visibility = Visibility.Collapsed; }
                else
                { this.stkProviderInbox.Visibility = Visibility.Visible; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void cmbProviderInbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.dictionaryProviders != null)
                {
                    Int64 nAssociationID = Convert.ToInt64(cmbProviderInbox.SelectedValue);

                    if (dictionaryProviders.ContainsKey(nAssociationID))
                    {
                        DirectUserProviderAssociation SelectedElement = dictionaryProviders[nAssociationID];
                        RaiseProviderChangedEvnt(new InboxProviderChangedEventArgs(SelectedElement));
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SetPreferredProvider()
        {
            try
            {
                System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _EnumDefaultProvider;
                _EnumDefaultProvider = from DirectUserProviderAssociation element in this._listOfProviders
                                       where element.IsProviderInbox
                                       select element;

                if (_EnumDefaultProvider.Count() == 1) //If the Provider has his own Direct Address
                {
                    DirectUserProviderAssociation providerElement = _EnumDefaultProvider.ElementAt(0);
                    ItemCollection objectCollection = cmbProviderInbox.Items;

                    if (objectCollection.Contains(providerElement))
                    {
                        this.IsChangedEventEnabled = true;
                        cmbProviderInbox.SelectedIndex = objectCollection.IndexOf(providerElement);
                    }

                    objectCollection = null;
                    providerElement = null;

                }
                else
                {
                    System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _EnumPreferredProvider;


                    _EnumPreferredProvider = from DirectUserProviderAssociation element in this._listOfProviders
                                             where element.IsPreferred
                                             select element;

                    if (_EnumPreferredProvider.Count() == 1) // If the Provider does not have 
                    {                                        // his own Direct Address check if he has a Preferred Provider
                        DirectUserProviderAssociation providerElement = _EnumPreferredProvider.ElementAt(0);
                        ItemCollection objectCollection = cmbProviderInbox.Items;

                        if (objectCollection.Contains(providerElement))
                        {
                            this.IsChangedEventEnabled = true;
                            cmbProviderInbox.SelectedIndex = objectCollection.IndexOf(providerElement);
                        }

                        objectCollection = null;
                        providerElement = null;
                        _EnumPreferredProvider = null;
                    }
                    else
                    // If Provider does NOT have his own Direct Address
                    // and as well as no Associations then load the first option
                    // in the Combo list
                    {
                        this.IsChangedEventEnabled = true;
                        cmbProviderInbox.SelectedIndex = 0;
                    }
                    _EnumDefaultProvider = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void MultipleProvidersAdded()
        {
            try
            {
                if (this._listOfProviders != null)
                {
                    if (this._listOfProviders.Count > 0)
                    {
                        if (this.dictionaryProviders == null)
                        { this.dictionaryProviders = new Dictionary<Int64, DirectUserProviderAssociation>(); }

                        foreach (DirectUserProviderAssociation Element in this._listOfProviders)
                        {
                            if (!dictionaryProviders.ContainsKey(Element.AssociationID))
                            { dictionaryProviders.Add(Element.AssociationID, Element); }
                        }

                        this.cmbProviderInbox.ItemsSource = ListOfProviders;

                        this.cmbProviderInbox.DisplayMemberPath = "NameAndDirectAddress";
                        this.cmbProviderInbox.SelectedValuePath = "AssociationID";

                        this.SetPreferredProvider();
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        protected virtual void RaiseProviderChangedEvnt(InboxProviderChangedEventArgs Args)
        { if (this.ProviderInboxChangedEvent != null) ProviderInboxChangedEvent(this, Args); }

        public Boolean IsChangedEventEnabled { get; set; }

        public List<gloSurescriptSecureMessage.DirectUserProviderAssociation> ListOfProviders
        {
            get
            {
                return this._listOfProviders;
            }
            set
            {
                if (value != null)
                { this._listOfProviders = value; }
                else
                {
                    this._listOfProviders = null;
                    this.cmbProviderInbox.Items.Clear();
                }

                if (value != null)
                { MultipleProvidersAdded(); }

            }

        }
        #endregion        
    }

    public class MyImageTextMenuButton : Button
    {
        public ImageSource MyButtonImageSource
        {
            get { return base.GetValue(MyButtonImageSourceProperty) as ImageSource; }
            set { base.SetValue(MyButtonImageSourceProperty, value); }
        }

        public String MyButtonText
        {
            get { return base.GetValue(MyButtonTextProperty) as String; }
            set { base.SetValue(MyButtonTextProperty, value); }
        }
        public static readonly DependencyProperty MyButtonImageSourceProperty = DependencyProperty.Register("MyButtonImageSource", typeof(ImageSource), typeof(MyImageTextMenuButton));

        public static readonly DependencyProperty MyButtonTextProperty = DependencyProperty.Register("MyButtonText", typeof(String), typeof(MyImageTextMenuButton));
    }

    public class MyImageTextButton : Button
    {
        public ImageSource MySource
        {
            get { return base.GetValue(MySourceProperty) as ImageSource; }
            set { base.SetValue(MySourceProperty, value); }
        }

        public String MyText
        {
            get { return base.GetValue(MyTextProperty) as String; }
            set { base.SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MySourceProperty = DependencyProperty.Register("MySource", typeof(ImageSource), typeof(MyImageTextButton));

        public static readonly DependencyProperty MyTextProperty = DependencyProperty.Register("MyText", typeof(String), typeof(MyImageTextButton));
    }
}