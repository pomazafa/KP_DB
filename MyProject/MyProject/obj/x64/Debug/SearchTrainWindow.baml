   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	   !    MyProject.SearchTrainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	     � Title$  SearchTrainWindow��5   	      MyWindow#��[  6#   $	��400��5	   	   $	��800��6   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��0.2* �6   6.   �� 5      $
��0.2* �6   6.   �� 5      $��10 �6   6,   �� 5      $
��0.6* �6   6.   .+�5   
   � $��0��6   $��0��6   $��2��6,   $��Stretch=�6@   $��Stretch=�6^     brushWatermarkBackground#��[ 6z     EntryFieldStyle#��[ 6�   .+�5      �� $	��5,2q�6   $��Date (dd.mm.yyyy)��6%     brushWatermarkForeground#��[ 6>   y�5      ��  �� ElementName$ Date1�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5      -   5        $��Date1��5       �� 	MaxLength$ 10��6#   
������   brushForegroudTextfield#��[ 6K     brushWatermarkBorder#��[ 6�   5      � 5   
   $��0��6   $��1��6   $��Stretch=�6,   $��Stretch=�6J   #��[ 6f   #��[ 6�   .+�5      �� $	��5,2q�6   $��	Station 1��6%   #��[ 66   y�5       �� $ Station1��$ Text.IsEmpty�# [ �� 5!      -   5        $��Station1��5!      
������ #��[ 6?   {�.  #��[ 6�   5"      �� 5#   
   -   5        $��Choose_Station1��5#   "   $��Choose��6     MyButton#���  5$      $��1��5%      $��1��6!   60   � 5&   
   $��0��6   $��2��6   $��Stretch=�6,   $��Stretch=�6J   #��[ 6f   #��[ 6�   .+�5'      �� $	��5,2q�6   $��	Station 2��6%   #��[ 66   y�5(      �� $ Station2��$ Text.IsEmpty�# [ �� 5)      -   5        $��Station2��5)      
������ {�.  #��[ 6Q   5*      �� 5+   
   -   5        $��Choose_Station2��5+   "   $��Choose��6   #���  5,      $��1��5-      $��2��6!   60   �� 5/   
   -   5        $��Search��5/   "   $��Search��6   $��2��60   #���  50      $��0��51      $��3��6!   60   �� 53   
   -   5        $
��Exit��53       $
��Exit��6   #���  54      $��0��55      $��4��6!   $��2��6.   6D   &   System.Windows.Controls.DataGrid  57   
   -   5        $��ResSet��58       �� AlternationCount$ 2��57      $��0��6)   $��3��66   $��3��6F     
MyDataGrid#���  6W      SelectionMode$ Single=�58   "   	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     CanUserReorderColumns .     
IsReadOnly .    RowHeaderTemplate 5:      �� 5;      .R�5<      d�  d�Name$ RowHeaderToggleButton��6#    � Cursor$
 Hand��5=      5>         Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$ Train number��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *Train.TRAINNUMBER+6p     5B      $ Count of seats��6%   $
 auto 6b    6=   �� *Train.COUNTSEATS+6q     5C      $ Cost per station��6%   $
 auto 6h    6?   �� *Train.COSTPERSTATION+6w     5D      $ Start station��6%   $ * 6^    6<   �� *Station1.Name+6j     5E      $ End station��6%   $ * 6\    6:   �� *Station2.Name+6h   5G      �� 5H   
   -	   5        $
��Info��5H   ,   $��Trip information��6   {�.  #���  5I      $��3��5J      $��3��6!   60   �� 5K   
   -
   5        $��Choose��5K   '   $��Book ticket��6   {�.  #���  5L      $��3��5M      $��4��6!   60   5N      5O      