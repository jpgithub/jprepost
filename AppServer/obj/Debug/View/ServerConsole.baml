   M S B A M L   `   `   `  ����  	AppServer"    AppServer.View.ServerConsole   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  Virtualize Hardware��5   	    � SizeToContent$ WidthAndHeight=�6%   .��5      � $	��390��6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $��28 �6    � ColumnDefinitions 5   
   �� 5      $	��275 �6   �� 5      $	��275 �6   .+�5   
   o�  o� 
IsMainMenu . $��2��6!   .@�5      m� $
��File��6   .@�5      m� $��	Open File��6    m� Command 6.   �� *OpenFileCmd+5      5      �� 5   
   $��1��6   $��0��6   $��2��6-   $��32(�6A   $��Virtualize Hardware��6O   $��Center=�6m   $��0q�6�   $��Center=�6�   $��55��6�   $��Magneto)�6�   � 5   
   $��2��6   $��0��6"   $��2��62    � Orientation$ 
Horizontal=�6F   .+�5      �� $��Start��6   {�. $��0,0,10,0q�6S   ��6%   �� *
StartCmd+6S   �� 5      $��Port #:��6   $��Segoe UI Semibold)�6&   �� 5      -   5        $��22��5       �� TextWrapping$
 Wrap=�6"   $��50��6\   $��0,0,10,0q�6g   ��66   �� *PortModel.PortNumber+{�6y   �� *PortModel.IsEditable+6�   �� 5      $
��Stop��6   {�. $��0,0,10,0q�6Q   ��6$   �� *	StopCmd+6Q   �� 5      $��Debug Console:��6   $��Segoe UI Semibold)�6-   �� 5      $��Connection Console:��6   $��Segoe UI Semibold)�62   5       �� 5!   
   $��3��6   $��0��6$   $
��Auto=�64   $
��Auto=�6Y   $��0,0,0,10q�6|   $��2��6�   .��5"      �  5#      �� 5$      $
��Auto �6'   �� 5%       5'      �� 5(      $
��Auto �6$   .+�5*      �� -   5         ��Name$ ConsoleBorder��5*      $��1��60   $��5q�6@   �� 5+      $	��200��5,      $��1��5-      $��5q�5.      $��Center=�5/      $
��Wrap=�50      $
��Auto��51   @   ��5+      �� *ActualWidth+	 �� ElementName$	 ConsoleBorder����51      �� *LogStreamOutput.DebugLog+6@   52      53      �� 54   
   $��3��6   $��1��6$   $
��Auto=�64   $
��Auto=�6Y   $��0,0,0,10q�6|   $��2��6�   .��55      �  56      �� 57      $
��Auto �6'   �� 58       5:      �� 5;      $
��Auto �6$   .+�5=      �� -   5        $ Console2Border��5=      $��1��61   $��5q�6A   �� 5>      $	��200��5?      $��1��5@      $��5q�5A      $��Center=�5B      $
��Wrap=�5C      $
��Auto��5D   E   ��5>      �� *ActualWidth+$	 Console2Border����5D      �� *LogStreamOutput.ConnectionLog+6E   5E      5F      5H      5I      