   M S B A M L   `   `   `  ���� D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  !    MyProject.SearchTrainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyProject       � Title$  SearchTrainWindow��   MyWindow#��[  $	��400��$	��800��.��� $��20q� � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� $
��0.2* ��� $
��0.2* ��� $��10 ��� $
��0.6* �.+�� $��0��$��0��$��2��$��Stretch=�$��Stretch=�  brushWatermarkBackground#��[   EntryFieldStyle#��[ .+��� $	��5,2q�$��Date (dd.mm.yyyy)��  brushWatermarkForeground#��[ y���  �� ElementName$ Date1�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� -   $��Date1�� �� 	MaxLength$ 10��
������   brushForegroudTextfield#��[   brushWatermarkBorder#��[ � $��0��$��1��$��Stretch=�$��Stretch=�#��[ #��[ .+��� $	��5,2q�$��	Station 1��#��[ y��� $ Station1��$ Text.IsEmpty�# [ �� -   $��Station1��
������ #��[ {�.  #��[ �� -   $��Choose_Station1��$��Choose��  MyButton#���  $��1��$��1��� $��0��$��2��$��Stretch=�$��Stretch=�#��[ #��[ .+��� $	��5,2q�$��	Station 2��#��[ y��� $ Station2��$ Text.IsEmpty�# [ �� -   $��Station2��
������ {�.  #��[ �� -   $��Choose_Station2��$��Choose��#���  $��1��$��2���� -   $��Search��$��Search��$��2��#���  $��0��$��3���� -   $
��Exit��$
��Exit��#���  $��0��$��4��$��2��&   System.Windows.Controls.DataGrid  -   $��ResSet�� �� AlternationCount$ 2��$��0��$��3��$��3��  
MyDataGrid#���     SelectionMode$ Single=�	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     CanUserReorderColumns .     
IsReadOnly .    RowHeaderTemplate �� .R�d�  d�Name$ RowHeaderToggleButton�� � Cursor$
 Hand��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Train number��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Train.TRAINNUMBER+  $ Count of seats��$
 auto  �� *Train.COUNTSEATS+  $ Cost per station��$
 auto  �� *Train.COSTPERSTATION+  $ Start station��$ *  �� *Station1.Name+  $ End station��$ *  �� *Station2.Name+�� -	   $
��Info��$��Trip information��{�.  #���  $��3��$��3���� -
   $��Choose��$��Book ticket��{�.  #���  $��3��$��4��