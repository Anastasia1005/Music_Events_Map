   M S B A M L   `   `   `  ����D  @Music_Map, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Music_Map	Music_Map  5   	   c _GMap.NET.WindowsPresentation, Version=2.0.1.0, Culture=neutral, PublicKeyToken=b85b9027b614afefqPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentationGMap.NET.WindowsPresentation 5   	       Music_Map.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Music_Map     5   	   pWindowsPresentationPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentation     5   	     � Title$  
MainWindow��5	   	   $	��453��6   $	��928��6)   .��5
      � .+�5   
   .  (GMap.NET.WindowsPresentation.GMapControl  -   5          Name$	 Map��5   *   $	��390��67   $	��570��6D   $��
210,15,9,0q�6d   $	��Top=�6x   $��Right=�6�   6�   �� 5   
   -   5         ��Name$ artistTextBox��5      $
��Left=�6)   $��30��6D   $��	10,45,0,0q�6P    �� TextWrapping$
 Wrap=�6c   $��Twenty One Pilots��6w   $	��Top=�6�   $	��230��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ SearchDataBut��5      $��
Поиск��6(   $
��Left=�68   $��
245,45,0,0q�6S   $	��Top=�6g   $��75��6   $��30��6�   �� 5   
   $��Кого ищем?��6   $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   $��30��6k   $	��100��6w   $��Center=�6�   �� 5   
   -   5         ��Name$ eventDataTxtBlock��5      $
��Left=�6/   $��
10,181,0,0q�6J   $
��Wrap=�6^   $	��Top=�6r   $	��222��6�   $	��310��6�   
�������6�   �� 5   
   $D��>Сортировка по стране и/или городу:��6   $
��Left=�6=   $��	10,80,0,0q�6X   $	��Top=�6k   $	��230��6�   �� 5   
   -   5        $ countryFiltrTxtBox��5      $
��Left=�6.   $��30��6I   $��
66,111,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $	��174��6�   $��Center=�6�   �� 5   
   $��Страна:��6   $
��Left=�6"   $��
10,111,0,0q�6=   $	��Top=�6Q   $��30��6i   $��51��6u   $��Center=�6�   �� 5   
   -   5        $ cityFiltrTxtBox��5      $
��Left=�6+   $��30��6F   $��
66,147,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��174��6�   $��Center=�6�   �� 5   
   $��Город:��6   $
��Left=�6!   $��
10,147,0,0q�6<   $	��Top=�6P   $��30��6h   $��51��6t   $��Center=�6   �� 5   
   -   5        $ VenueFiltrBut��5      $��Сортировка��6(   $
��Left=�6=   $��245,129,0,0q�6X   $	��Top=�6m   $��75��6�   $��30��6�   6�   5      5      