   M S B A M L   `   `   `  ���� D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject       MyProject.CreateTripWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyProject     -   $��
CreateTrip��  � Title$  CreateTripWindow��   MyWindow#��[  $	��400��$	��800��.��� $��20q� � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� $
��0.2* ��� $
��0.2* ��� $��10 ��� $
��0.6* �.+�� $��0��$��0��$��2��$��Stretch=�$��Stretch=�  brushWatermarkBackground#��[   EntryFieldStyle#��[ .+��� $	��5,2q�$��Arrival datetime��  brushWatermarkForeground#��[ y���  �� ElementName$ Date1�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� $
��5,27q�$��(dd.mm.yyyy hh:mi)��#��[ y��� $ Date1��$ Text.IsEmpty�# [ �� -   $��Date1�� �� 	MaxLength$ 16��
������   brushForegroudTextfield#��[   brushWatermarkBorder#��[ � $��0��$��1��$��2��$��Stretch=�$��Stretch=�#��[ #��[ .+��� $	��5,2q�$��Departure datetime��#��[ y��� $ Date2��$ Text.IsEmpty�# [ �� $
��5,27q�$��(dd.mm.yyyy hh:mi)��#��[ y��� $ Date2��$ Text.IsEmpty�# [ �� -   $��Date2��$ 16��
������ #��[ #��[ � $��0��$��2��$��Stretch=�$��Stretch=�#��[ #��[ .+��� $	��5,2q�$��Station��#��[ y��� $ Station1��$ Text.IsEmpty�# [ �� -   $��Station1��
������ #��[ {�.  #��[ �� -   $��Choose_Station1��$��Choose��  MyButton#���  $��1��$��2���� -   $	��Add��$��Add stop��$��2��#���  $��0��$��3���� -   $
��Exit��$��Cancel��#���  $��0��$��4��$��2��&   System.Windows.Controls.DataGrid  -   $��ResSet��$��0��  
MyDataGrid#���  $��10q�   SelectionMode$ Single=�$��3��$��3��   CanUserAddRows .  	   CanUserDeleteRows	 .  
   CanUserReorderColumns
 .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Station name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Station.Name+  $ Arrival time��$ *  �� *Arrival_datetime+  $ Departure time��$ *  �� *Departure_datetime+�� -	   $��Delete��$��Delete stop��{�.  #���  $��3��$��3���� -
   $
��Next��$
��Next��{�.  #���  $��3��$��4��