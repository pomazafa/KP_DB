   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	        MyProject.CreateTripWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	   -   5        $��
CreateTrip��5	   )     � Title$  CreateTripWindow��5   	      MyWindow#��[  6"   $	��400��5	   	   $	��800��6   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��0.2* �6   6.   �� 5      $
��0.2* �6   6.   �� 5      $��10 �6   6,   �� 5      $
��0.6* �6   6.   .+�5   
   � $��0��6   $��0��6   $��2��6,   $��Stretch=�6@   $��Stretch=�6^     brushWatermarkBackground#��[ 6z     EntryFieldStyle#��[ 6�   .+�5      �� $	��5,2q�6   $��Arrival datetime��6%     brushWatermarkForeground#��[ 6?   y�5      ��  �� ElementName$ Date1�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5      $
��5,27q�6   $��(dd.mm.yyyy hh:mi)��6&   #��[ 6A   y�5      �� $ Date1��$ Text.IsEmpty�# [ �� 5      -   5        $��Date1��5       �� 	MaxLength$ 16��6#   
������   brushForegroudTextfield#��[ 6K     brushWatermarkBorder#��[ 6�   5      � 5    
   $��0��6   $��1��6   $��2��6,   $��Stretch=�6@   $��Stretch=�6^   #��[ 6z   #��[ 6�   .+�5!      �� $	��5,2q�6   $��Departure datetime��6%   #��[ 6A   y�5"      �� $ Date2��$ Text.IsEmpty�# [ �� 5#      $
��5,27q�6   $��(dd.mm.yyyy hh:mi)��6&   #��[ 6A   y�5$      �� $ Date2��$ Text.IsEmpty�# [ �� 5%      -   5        $��Date2��5%      $ 16��6#   
������ #��[ 6K   #��[ 6�   5&      � 5'   
   $��0��6   $��2��6   $��Stretch=�6,   $��Stretch=�6J   #��[ 6f   #��[ 6�   .+�5(      �� $	��5,2q�6   $��Station��6%   #��[ 64   y�5)      �� $ Station1��$ Text.IsEmpty�# [ �� 5*      -   5        $��Station1��5*      
������ #��[ 6?   {�.  #��[ 6�   5+      �� 5,   
   -   5        $��Choose_Station1��5,   "   $��Choose��6     MyButton#���  5-      $��1��5.      $��2��6!   60   �� 5/   
   -   5        $	��Add��5/   $   $��Add stop��6   $��2��6/   #���  50      $��0��51      $��3��6!   60   �� 53   
   -   5        $
��Exit��53   "   $��Cancel��6   #���  54      $��0��55      $��4��6!   $��2��6.   6D   &   System.Windows.Controls.DataGrid  57   
   -   5        $��ResSet��58      $��0��57        
MyDataGrid#���  6    $��10q�58         SelectionMode$ Single=�6-   $��3��6D   $��3��6T      CanUserAddRows .  	   CanUserDeleteRows	 .  
   CanUserReorderColumns
 .     
IsReadOnly .    Columns 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn   Header$ Station name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *Station.Name+6h     5<      $ Arrival time��6%   $ * 6`    6;   �� *Arrival_datetime+6l     5=      $ Departure time��6%   $ * 6d    6=   �� *Departure_datetime+6p   5?      �� 5@   
   -	   5        $��Delete��5@   '   $��Delete stop��6   {�.  #���  5A      $��3��5B      $��3��6!   60   �� 5C   
   -
   5        $
��Next��5C       $
��Next��6   {�.  #���  5D      $��3��5E      $��4��6!   60   5F      5G      