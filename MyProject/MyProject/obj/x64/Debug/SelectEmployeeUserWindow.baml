   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	   (    "MyProject.SelectEmployeeUserWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	   -   5          � Title$  SelectEmployeeUserWindow��5   	      MyWindow#��[  6*   $	��400��5	   	   $	��800��6   .��5
      � $��20q�6    � RowDefinitions 5   
   �� 5      6   �� 5      $	��100 �6   6+    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ResSet��5      $��0��5        
MyDataGrid#���  6    $��2��6E   $��10q�5         SelectionMode$ Single=�6-      CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     
IsReadOnly .    RowHeaderTemplate 5      �� 5      .R�5      d� 	 d�Name$	 RowHeaderToggleButton��6#   
 � Cursor$

 Hand��5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Login��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *LOGIN+6Z     5      $ Lastname��6%   $ * 6T    67   �� *
LASTNAME+6`     5      $ 	Firstname��6%   $ * 6V    68   �� *	FIRSTNAME+6b   5!      �� 5"   
   -   5        $
��Back��5"       $
��Back��6     MyButton#���  5#      $��1��5$      $��0��6   60   �� 5%   
   -   5        $��Choose��5%   "   $��Choose��6   #���  5&      $��1��5'      $��1��6   60   5(      5)      