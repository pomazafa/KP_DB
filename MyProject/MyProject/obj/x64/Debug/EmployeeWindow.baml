   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	       MyProject.EmployeeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	   -   5          � Title$  EmployeeWindow��5   	      MyWindow#��[  6    $	��400��5	   	   $	��800��6   .��5
      � $��20q�6    � RowDefinitions 5   
   �� 5      6   �� 5      $	��100 �6   6+    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ResSet��5      $��0��5      $��0��6    $��4��60     
MyDataGrid#���  6D   $��10q�5         SelectionMode$ Single=�6-      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Client��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Client.LASTNAME+6e     5      $	 Date of trip��6%   $

 auto 6s    6;   �� *
DateOfTrip+ �� StringFormat$ 
dd.MM.yyyy��6�     5      $	 Cost per station��6%   $

 auto 6m    6?   �� *Trip.Train.COSTPERSTATION+6|   5      �� 5   
   -   5        $
��Exit��5       $
��Exit��6     MyButton#���  5       $��0��64   $��1��5!      6    �� 5#   
   -   5        $��Export��5#   +   $��Export database��6   #���  5$      $��1��64   $��1��5%      6    �� 5'   
   -   5        $��Import��5'   *   $��Import Clients��6   #���  5(      $��2��64   $��1��5)      6    �� 5+   
   -   5        $��Process��5+   *   $��Process ticket��6   #���  5,      $��3��64   {�.  $��1��5-      6    5.      5/      