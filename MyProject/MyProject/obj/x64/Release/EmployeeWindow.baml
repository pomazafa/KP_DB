   M S B A M L   `   `   `  ���� D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject      MyProject.EmployeeWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyProject     -     � Title$  EmployeeWindow��   MyWindow#��[  $	��400��$	��800��.��� $��20q� � RowDefinitions �� �� $	��100 � � ColumnDefinitions �� �� �� �� .+�&   System.Windows.Controls.DataGrid  -   $��ResSet��$��0��$��0��$��4��  
MyDataGrid#���  $��10q�   SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Client��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Client.LASTNAME+  $	 Date of trip��$

 auto  �� *
DateOfTrip+ �� StringFormat$ 
dd.MM.yyyy��  $	 Cost per station��$

 auto  �� *Trip.Train.COSTPERSTATION+�� -   $
��Exit��$
��Exit��  MyButton#���  $��0��$��1���� -   $��Export��$��Export database��#���  $��1��$��1���� -   $��Import��$��Import Clients��#���  $��2��$��1���� -   $��Process��$��Process ticket��#���  $��3��{�.  $��1��