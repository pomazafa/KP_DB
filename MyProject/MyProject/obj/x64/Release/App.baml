   M S B A M L   `   `   `  ���� D  @MyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyProject	MyProject      MyProject.App   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     )localclr-namespace:MyProject       �� 	Resources     MyWindow �� 
TargetType �� Property �� Value  MyLabel  brushWatermarkBackground �� Color  brushWatermarkForeground  brushWatermarkBorder   BooleanToVisibilityConverter  brushForegroudTextfield  EntryFieldStyle  	MyTextBox 	 MyButton �� 
TargetType �Name ��Name �� CornerRadius	 �� Triggers
 P� Property P� Value �� 
TargetName :� RoutedEvent �� From	 �� To g� Duration �� 
TargetName �� TargetProperty 
 
MyDataGrid&   System.Windows.Controls.DataGrid*  $System.Windows.Controls.DataGridCell �� Triggers=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Padding �� 
StartPoint �� EndPoint � Offset � Color J� SnapsToDevicePixels)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical  �� ConverterParameter#!   RowDetailsScrollingConverter" �� 	Converter# �� RelativeSource$ �� AncestorType��%Y	  &	        &	 �     &	     &	     &	 .    &	 >    &	 C    &	 V    &	 �    &		 �    &	
 �    '  f    '  �    '  I    �� " �.���  �@�WindowState$ 	Maximized=���  � ��$ 24(���  � ��$ 
sans-serif)���  � �� �#282E33��  �@�Icon$ Images/train.png���� " ��.���  � �� �#ffffff��  � ��$ 24(���  � ��$ Center=��� $ #d0e0f0���� $ #6d6d6d���� $
 Gray���� �� $ #1b1b1b���� " �.���  � ��$ Stretch=���  �@J�Opacity$	 0.8Q���  � ��$ 20q���  � ��$ Stretch=��� " ��.���  � �� �#1b1b1b��  � �� �#d0e0f0��  � ��$ Stretch=���  � ��$ 28(���  � ��$ Stretch=���  �@J�Opacity$	 0.8Q���  � ��$ 20q��� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � ��$ 20q���  � ��
 �������  � �� �� " ��.R�� $
 grid��.+��� $ border��$ 40������ $ #0071e3��._��� $��Center=�$��Center=�	 P� 
 � {� .  . ��� $
 grid�� �@J�Opacity$
 0.25Q�:� $ 
MouseEnter��.[��� .���� .���� $ #0071e3X�$ #52b0caX�$ 0:0:0.5D�$ border��$ Background.Color�:� $ 
MouseLeave��.[��� .���� .���� $ #52b0caX�$ #0071e3X�$ 0:0:0.5D�$ border��$ Background.Color��� "  .���  � �� �#d0e0f0��  � ��$ 20(���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ 20q��� "   P� 
 �@ 
IsSelected . . ���  � �� �#bee2ea��  � �� �#1b1b1b�� "  .���  � ��$ Center=���  � ��$ 35����  � ��
 �������  �@ SeparatorBrush
 ��  ���  � ��$ Black"���  � �� �� "  .R�� .+��� $ columnHeaderBorder��$��1q�$ 3,0,3,0q����� $ Black������ $ 0.5,0&�$ 0.5,1&�.��� $ 0Q�$ #408ca0��� $ 1Q�$ #52b0ca��._��� #��z��#��z��# z �� "  .���  � �� �#e0eeff��  �@=�ErrorTemplate Z� ��  � ��$ 20(���  � �� �� "  .R��� $ 
DGR_Border��#��z��#��z�� . ���� $ #e0eeff��._�   �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z # z   $��1��$��1��#y�z  �� *AreRowDetailsFrozen+#  Z #" Z! # �� "$  