   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	   $    MyProject.CreateEmployeeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	     � Title$  CreateEmployeeWindow��5   	   $	��500��6&   $	��830��66      MyWindow#��[  5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   � $��0��6   $��0��6   $	��400��6,   .+�5      � $��Stretch=�6   $��Stretch=�61     brushWatermarkBackground#��[ 6M     EntryFieldStyle#��[ 6�   .+�5      �� $	��5,2q�6   $��Card number��6)     brushWatermarkForeground#��[ 6<   y�5      ��  �� ElementName$ Number�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5      -   5        $��Number��5       �� 	MaxLength$ 16��6(     brushForegroudTextfield#��[ 67   
������   brushWatermarkBorder#��[ 6�   5      5      � 5   
   $��0��6   $��1��6   $	��400��6,   .+�5      � $��Stretch=�6   $��Stretch=�61   #��[ 6M   #��[ 6�   .+�5      �� $	��5,2q�6   $��Espiration Date (mm/yy)��6)   #��[ 6H   y�5      �� $ EspDate��$ Text.IsEmpty�# [ �� 5       -   5        $��EspDate��5       $ 5��6)   #��[ 67   
������ #��[ 6�   5!      5"      � 5#   
   $��0��6   $��2��6   $	��400��6,   .+�5$      � $��Stretch=�6   $��Stretch=�61   #��[ 6M   #��[ 6�   .+�5%      �� $	��5,2q�6   $��
CARD OWNER��6)   #��[ 6;   y�5&      �� $ Owner��$ Text.IsEmpty�# [ �� 5'      -   5        $��Owner��5'      $ 30��6'   #��[ 66   
������ #��[ 6�   5(      5)      �� 5*   
   -   5        $
��Next��5*       $
��Next��6   $	��370��6,     MyButton#���  5+      $��0��5,      $��3��6!   60   �� 5.   
   -   5        $
��Exit��5.   "   $��Cancel��6   $	��370��6.   #���  5/      $��0��50      $��4��6!   60   51      52      