   M S B A M L   `   `   `  ����  YoutubeDownloader.Wpf.App.    (YoutubeDownloader.PlaylistCreationWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��500��5   	   $	��300��5	   	   
������   � WindowStyle$
  None=�5   	    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ 	CanResize=�5   	   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�5   
    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         WindowFrameBrush �� 
StartPoint �� EndPoint � Color	 � Offset��%�   &	        �� 5   
   $ 0,0.5&�67   $ 1,0.5&�6J   .��5      � $ 	CadetBlue��6   $		 0.4Q�6-   � 5      $ 	CadetBlue��6   $	 1Q�6-   5      5      .��5      �� 
�������
 �� CornerRadius$
 7,7,7,7��5      
���  ��$��1,1,1,1q�5      ._�5   
   ��  �� Orientation$ Vertical=�6   .+�5      ]� .+�5      ]� -   5         ]�Name$ 
RootWindow��5      $	��Top=�60   $��40��6E   ���#ff0000$	��Top=�6f   .+�5       ]� -   5        $ TitleBar��5        $	��500��62   $��Right=�6>   .+�5"      �� -   5         ��Name$ CloseButton��5"   !   $��Right=�66   $
��auto��6N   $
��auto��6\   
������ 
������ .��5#      �� $ 
Horizontal=�6)   $��0q�6B   .+�5$   "   �� $��/Resources/close_icon.png��6(   $��14��6K   $��14��6V   5%      5&      �� 5'      $��Create Playlist��6    $��17(�6:   
�������$��Center=�6[   $��Center=�6}   $��10,0,0,0q�6�   5(      5)      5*      �� 5,      ��6   �� *CurrentPage+�� 5.      $��15,15,15,15q�5/      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Playlist Name��50      .  (MaterialDesignThemes.Wpf.TextFieldAssist   TextBoxViewMargin$ 1 0 1 0q�51         FloatingScale$
 0.50Q�52       % !MaterialDesignFloatingHintTextBox#��[ 53      $��20(�54      �� 56      $��15,15,15,15q�57      $ Genre��58      $ 1 0 1 0q�59      $
 0.50Q�5:      #��[ 5;      $��20(�5<      �� 5>      $ 
Horizontal=�6   $��Center=�62   $��15,15,15,15q�6P   .+�5@      �� $��Create and Add��6   $��10,10,10,10q�62   $��50��6G   $	��200��6S   �� 5B      $��Cancel��6   $��10,10,10,10q�6+   $��50��6@   $	��200��6L   5C      5D      5E      5F      