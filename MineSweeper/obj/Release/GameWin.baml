   M S B A M L   `   `   `  ���� F  BMineSweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MineSweeperMineSweeper      MineSweeper.GameWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MineSweeper     -     �Name$	  Win�� � Title$ MineSweeper�� � 
ResizeMode$ CanMinimize=� � SizeToContent$ WidthAndHeight=� � 	Resources   MineSweeper.MineButton �� 
TargetType   MineSweeper.MineButton   Click �� Property �� Value��%�   '        �� "  .�;�  MineButton_Click��  � ��$ 18(���  � ��$ 30����  � ��$ 30����  � ��
 �����.��� -   	 �Name$	 MainGrid��.+��� -   
 ��Name$
 
GameBorder��$��8,65,8,8q�
���   �$��2q�$
��Auto=�$
��Auto=�._�� -   $	 GameGrid��$��0q�$��Hidden=��� -   $��Button_Back��$��
悔棋...?��$
��Left=�$
��27.6��$��	10,10,0,0q�$	��Top=�$
��61.2��{�.  �� -   $��Button_Exit��$��结束��$
��Left=�$
��27.6��$��92.997,10,0,0q�$	��Top=�$
��61.2��