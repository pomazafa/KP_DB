   M S B A M L   `   `   `  ����D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  5   	        MyProject.OccupationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyProject     5   	     � Title$  OccupationWindow��5   	   $	��430��6"   $	��340��62      MyWindow#��[  5	   	   .��5
      � $��40, 10q�6    � RowDefinitions 5   
   �� 5      $��15 �6   6*   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��15 �6   6*   .+�5   
   �� -   5        $��Existing��5      $ ��Select existing occupation��5        MyButton#���  5      $	��370��5      $��1��5      � 5   
   $��0��6   $��2��6   $	��400��6,   .+�5      � $��Stretch=�6   $��Stretch=�61     brushWatermarkBackground#��[ 6M     EntryFieldStyle#��[ 6�   .+�5      �� $	��5,2q�6   $��Occupation name��6)     brushWatermarkForeground#��[ 6@   y�5      ��  �� ElementName$
 name�� �� Path$ Text.IsEmpty�   BooleanToVisibilityConverter �� 	Converter# [ �� 5      -   5        $
��name��5       �� 	MaxLength$ 40��6&     brushForegroudTextfield#��[ 65   
������   brushWatermarkBorder#��[ 6�   5      5       � 5!   
   $��0��6   $��3��6   $	��400��6,   .+�5"      � $��Stretch=�6   $��Stretch=�61   #��[ 6M   #��[ 6�   .+�5#      �� $	��5,2q�6   $��Cost per hour��6)   #��[ 6>   y�5$      �� $
 cost��$ Text.IsEmpty�# [ �� 5%      -   5        $
��cost��5%      $ 6��6&   #��[ 64   
������ #��[ 6�   5&      5'      � 5(   
   $��0��6   $��4��6   $	��400��6,   .+�5)      � $��Stretch=�6   $��Stretch=�61   #��[ 6M   #��[ 6�   .+�5*      �� $	��5,2q�6   $��Hours per week��6)   #��[ 6?   y�5+      �� $ Hours��$ Text.IsEmpty�# [ �� 5,      -   5        $��Hours��5,      $ 2��6'   #��[ 65   
������ #��[ 6�   5-      5.      �� 5/   
   -   5        $��Create��5/   $   $��70��6   $��Create Occupation��50      #���  51      $	��370��52      $��5��53      �� 54   
   -   5        $
��back��54      $
��Back��55      #���  56      $	��370��57      $��6��58      59      5:      