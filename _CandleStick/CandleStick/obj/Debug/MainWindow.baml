   M S B A M L   `   `   `  ����F  BCandleStick, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CandleStickCandleStick  5   	       CandleStick.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CandleStick     5   	   
������   � WindowStyle$
  None=�5	   	    � AllowsTransparency .  � Title$ 
MainWindow��5   	   $	��800��6   $
��1400��6)   .��5      � .+�5   
   � $��20q�6   ���#1C2F3E J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 30Q�6#      ShadowDepth$ 2Q�63      	Direction$	 -90Q�6C      Color$
 Gray��6S   6b   .+�5      �� $	��100��6   $
��Left=�6%   ���#14212B.+�5      �� -   5         ��Name$
 Home��5      $	��100��6'   
������ 
������ $��
0,180,0,20q�6g   .��5      �� $"��icons/icons8-dog-bone-64.png��6   5      �� 5      -   5        $ Profile��5      $	��100��6*   
������ 
������ $��0,0,0,10q�6j   .��5       �� $#��icons/icons8-fish-bone-64.png��6   6E   5"      �� 5#      -   5        $ Setting��5#      $	��100��6*   
������ 
������ $��0,10,0,0q�6j   .��5$      �� $��icons/icons8-lamb-64.png��6   6@   5%      5&      � 5(      $��80��6   $	��Top=�6   ���#233C4F.+�5)      �� $��Candle stick configurator��6   $��open sans condensed)�6=   $��26(�6^   $��Center=�6l   $��Center=�6�   ���#E0FFFF5*      �� 5,      -   5        $��40��5,      $��40��6%   
������ $��0q�6I   $��Center=�6]   $��Right=�6x   $��0,0,20,0q�6�   .��5-      �� $!��icons/icons8-dog-paw-64.png��6   6C   5.      5/      � 51      $	��900��6   $	��500��6   $��Center=�6,   $��Center=�6I   .+�52      �� -   5        	 ��Name$	 OutputCSV_TextBox��52      
 �� TextWrapping$

 Wrap=�65   
������ $��open sans condensed)�6b   $��26(�6�   ���#E0FFFF6�   53      � 55      $	��125��6   $��Bottom=�6    ���#233C4F.+�56      �� -   5        $	 Warning_TextBox��56      $

 Wrap=�63   
������ $��open sans condensed)�6`   $��26(�6�   
���  ��6�   58      5;      5>      5?      