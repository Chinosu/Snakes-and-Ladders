   M S B A M L   `   `   `  ����K  GSnakesAndLadders, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SnakesAndLaddersSnakesAndLadders  5   	   !    SnakesAndLadders.GameWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SnakesAndLadders     5   	     � UseLayoutRounding$
  True��5	   	    � Title$ Snakes and Ladders��5
   	   $	��450��6$   $	��800��61   $	��850��6=   $
��1000��6M    � Icon$c ]C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Snake.ico��5   	   .��5      �  � ShowGridLines$ False��6    � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��0 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ BackgroundImg��5      $��UniformToFill=�6'   $��2��6?   $��3��6S   �� 5   
   
���   �$��1��6$   $��1��61   �� 5   
   $��2��6   $��1��6$    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%/  '��       '�� �     �� 5      " ��6   .�5      �� 	 � ��
 63   �� *BlackTransparent+�� 5      	 � ��$
 20q�6/   �� 5       	 �@��Padding$
 25q�60   5!      �� 5#      " ��6   .�5$      �� 	 � ��

 �����63   �� 5%      	 � ��$
 20(�61   5&      5'      ��5(      �� 5)      .R�5*      �  � Orientation$ Vertical=�6    5+      .@�5.      �� -   5         ��Name$ BoardHolder��5.   0   $
��Left=�6   60   �� 50      $	��250��6   $
��Left=�6!   ._�51      �� -   5         ��Name$ PlayersItmCtrl��51   Z   $
��Auto��6?   $
��Auto��6L   ��6   �� *	Players+��52      �� 53      .R�54      ��  �� Orientation$ 
Horizontal=�6)   .+�55   "   �� $��20��6J   $��20��6U   ��6(   �� *Profile.Source+6U   �� 56   "   
�������$��25(�6U   ��6,   �� *Name+6U   57      58      ��5:      �� 5;      .R�5<      �� $ Vertical=�6)   5=      5?      5@      �� 5B      $	��150��6   $
��Left=�6!    5C      ��%w   '��       �� 5D      " ��6   .�5E      �� 	 � ��$	
 100��62   �� 5F      	 � ��$
 62��63   5G      5H      ._�5I      �� -   5         ��Name$ RollBtn��5I   H   
���2�2�.��5J      �� $
��Roll��6    
������ 
���   �6E   5K      5L      �� 5N      $	��250��6   $	��250��6!   ._�5O      �� -   5        $ DieImage��5O   I   $	��200��6   $	��200��6$   $��UniformToFill=�61   6I   5P      5Q      5R      5S      