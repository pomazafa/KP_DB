   M S B A M L   `   `   `  ���� D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject  +    %MyProject.CreateTripSelectTrainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyProject     -     � Title$!  CreateTripSelectTrainWindow��   MyWindow#��[  $	��400��$	��800��.��� $��20q� � RowDefinitions �� �� $	��100 � � ColumnDefinitions �� �� .+�&   System.Windows.Controls.DataGrid  -   $��ResSet��$��0��$��2��  
MyDataGrid#���  $��10q�   SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     
IsReadOnly .    RowHeaderTemplate �� .R�d� 	 d�Name$	 RowHeaderToggleButton��
 � Cursor$

 Hand��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Train number��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *TRAINNUMBER+  $ Count seats��$
 auto  �� *
COUNTSEATS+  $ Cost per station��$
 auto  �� *COSTPERSTATION+�� -   $
��Back��$
��Back��  MyButton#���  $��1��$��0���� -   $��Ok��$��Ok��#���  $��1��$��1��