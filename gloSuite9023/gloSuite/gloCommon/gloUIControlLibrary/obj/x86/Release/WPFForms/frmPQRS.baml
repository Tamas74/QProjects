   M S B A M L   `   `   `  ���� P  LgloUIControlLibrary, Version=1.0.0.293, Culture=neutral, PublicKeyToken=nullU/clr-namespace:gloUIControlLibrary.WPFForms.PQRS!gloUIControlLibrary.WPFForms.PQRS  S.clr-namespace:gloUIControlLibrary.Classes.PQRS gloUIControlLibrary.Classes.PQRS  /    )gloUIControlLibrary.WPFForms.PQRS.frmPQRS   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Alocal/clr-namespace:gloUIControlLibrary.WPFForms.PQRS     =dw.clr-namespace:gloUIControlLibrary.Classes.PQRS     -   $��13(�  � Title$  Quality Data Codes (QDCs)��$	��500��$	��300�� � WindowStartupLocation$ CenterScreen=� � Icon$7 1/gloUIControlLibrary;component/Resources/QDCs.ico�� � 	Resources  �� MergedDictionaries �� Source   TreeViewItemStyle �� 
TargetType �� Property �� Value	 �� Mode-  'System.Windows.Input.KeyboardNavigation:   4gloUIControlLibrary.Classes.PQRS.VirtualToggleButton  CheckBoxItemTemplate
 �� ItemsSource �� Orientation d� 	IsChecked ^� ToolTip �� RelativeSource �� Path��  �� $> 8/gloUIControlLibrary;component/Themes/gloGUIStyleV8.xaml?��� $9 3/gloUIControlLibrary;component/Themes/gloIcons.xaml?�%�  &	        &	 �     �� " Q�.���  �@Q�
IsExpanded . ��  �@Q�
IsSelected �� *IsInitiallySelected+$	 OneTime=���  �@ AcceptsReturn . ��  �@ IsVirtualToggleButton . ��  �@ 	IsChecked �� *	IsChecked+�� 
 �� *
Children+$	 OneTime=�.R��� $ 
Horizontal=�.+��� |�.  $��Center=� �� *	IsChecked+�� -   $	��2,0q����� *Name+$	 OneTime=� _� ����  �� *Self+$ PlacementTarget.Content�.��]� .+��� -    ��Name$ ToolBar��$	��Top=�$ 
Horizontal=�$��0q�$��Stretch=�  Img_ToolstripStyle#���  $
��Auto��.+��� -    ��Name$ 
SaveButton��  ToolStripButtonHover#���  $��1q�$��Stretch=�$��Stretch=�$
��Auto��$��4,0,0,0q�._�� .+��� -    ��Name$ btnSave��  SimpleButton#���  $��Center=�$	��Top=�$
��Auto��$��0,2,0,0q� � Cursor$
 Hand��.���� -   $ 
pnlNewMail��$��1q�$��Stretch=�$��Stretch=�$
��Auto��$��4,0,0,0q�._��� .+��� $P��Jpack://application:,,,/gloUIControlLibrary;component/Resources/SaveCls.png���� $��	_Save&Cls��  BaseForeColor#���  $��Bottom=�$��Center=�$��0q�$
��Bold"�  BaseFontFamily#���  �� -   $ CloseButton��#���  $��1q�$��Stretch=�$��Stretch=�$
��Auto��$��4,0,0,0q�._�� .+��� -   $ btnClose��#���  $��Center=�$	��Top=�$
��Auto��$��0,2,0,0q�$
 Hand��.���� -	   $ pnlClose��$��1q�$��Stretch=�$��Stretch=�$
��Auto��$��4,0,0,0q�._��� .+��� $N��Hpack://application:,,,/gloUIControlLibrary;component/Resources/close.png���� $��_Close��#���  $��Bottom=�$��Center=�$��0q�$
��Bold"�#���  �� -
   $ pnlPatientCode��$��0��$��0��#���  $	��0.5q� �� Padding$ 3q�._�R� -    R�Name$
 tree��#��[  #��[ ���� $	 OneTime=�