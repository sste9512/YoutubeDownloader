   M S B A M L   `   `   `  ����  YoutubeDownloaderC    =YoutubeDownloader.Controls.VideoInfoPanel.View.VideoInfoPanel   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      ��   �� Orientation$  Vertical=�6   $	��400��5	      $��10,10,10,10q�5
      .+�5   
   �� $	��620��6   $	��620��5      $��5,5,5,5q�5      .��5      �� $  Vertical=�6   .+�5      �� -   5        $��
VideoTitle��5      $��Video Title��5      
���   �$��20(�5      $��DemiBold"�5      �� 5      $  
Horizontal=�6   .+�5      �� -   5        $��VideoThumbnail��5      $	��150��5      $	��100��5      $
��Left=�5      �� 5      $  Vertical=�6!   .+�5      �� $  
Horizontal=�6%   $��5q�5   %   .+�5       '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ ThumbUp=�66    J� Opacity$ 1Q�5!   6   
�������$��25��5#   6   $��25��5$   6   $��Center=�5%   6   �� 5&      -   5        $��
LikesCount��5'   $   $��Likes��5&   $   
�������$��12(�5)   $   $��Center=�5*   $   $��Center=�5+   $   $��Center=�5,   $   $
��auto��5-   $   5.      �� 50      $  
Horizontal=�6%   $��5q�51   %   .+�53        $ 	ThumbDown=�66   $ 1Q�54   6   
�������$��25��57   6   $��25��58   6   $��Center=�59   6   �� 5:      -   5        $��DislikesCount��5;   $   $�� ThumbDown ��5:   $   
�������$��12(�5=   $   $��Center=�5>   $   $��Center=�5?   $   $��Center=�5@   $   $
��auto��5A   $   5B      �� 5D      -   5        $��	ViewCount��5E       $��Views��5D       
�������$��12(�5G       $
��Left=�5H       $��Center=�5I       $
��auto��5J       5K      5L      �� 5N      -   5        $��VideoAuthor��5N   <   $��Author��6   
���   �$��14(�6O   �� 5O      -   5        $��VideoPublishedDate��5O   V   $��Published Date��6   
�������$��12(�6H   6V   �� 5P      -   5        $��VideoDesciption��5P   =   $��Caption��6   
���   �6=   �� 5Q      $��Author��6   
���   �6)   �� 5S      
�������$	��0.7��62   $��5,5,5,5q�6?   �� 5U      $��
Video Info��6   
���   �$��19(�6@   $��DemiBold"�6N   �� 5V      -	   5        $��VideoDuration��5V   A   $��Duration��6   
�������6A   �� 5W      -
   5        $��VideoUrl��5W   <   $	��URL��6   
�������6<   �� 5X      -   5        $��VideoExtension��5X   B   $��	File Type��6   
�������6B   �� 5Z      
�������$	��0.7��62   $��5,5,5,5q�6?   �� 5\      $��Availability��6   
���   �$��17(�6B   �� 5]      -   5        $��VideoAvailable��5]   C   $��
Video Only��6   
�������6C   �� 5^      -   5        $��SoundAvailable��5^   C   $��
Sound Only��6   
�������6C   �� 5_      -   5        $��MixedAvailable��5_   >   $��Mixed��6   
�������6>   5`      5a      �� 5c   
   -   5        $��DownloadButton��5j      $	��Top=�5c      $��Center=�5d      $��5,5,5,5q�5e      $��50��5f      $	��260��5g      
������ +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�5i       � ToolTip 5k      _� 5m      ���#212121
�����_�$��1q�6G   $��12,12,12,12q�6[   $ Depth3=�5n      .��5p      �� .+�5q      �� $
��Bold"�6$   $��14(�5r   $   $��0,0,0,5q�5s   $   	Open file6@   �� 5t      JHChoose the a custom output directory for the current images to be placed�� 5v      5w      �� 5x      
�������$��	0,0.6,0,0q�5y   !   $	��0,8q�5z   !   � 5{      .+�5}      �� $��Italic$�6(   Press F1 for more help6S   5~      5      5�      .��5�      �� $  
Horizontal=�6   $��5q�5�      .+�5�      �� $��/Resources/file_icon.png��6   $��20��5�      $��20��5�      $��0,0,0,0q�5�      �� 5�      $��  Download Video ��6   
�������$��14(�5�      $��Center=�5�      $��Center=�5�      $
��auto��5�      �� 5�      6   5�      5�      �� 5�   
   $	��Top=�6   $��Center=�5�      $��5,5,5,5q�5�      $��50��5�      $	��260��5�      
������ $ Depth2=�5�       5�      _� 5�      ���#212121
�����_�$��1q�5�      $��12,12,12,12q�5�      $ Depth3=�5�      .��5�      �� .+�5�      �� $
��Bold"�6$   $��14(�5�   $   $��0,0,0,5q�5�   $   	Open file6@   �� 5�      JHChoose the a custom output directory for the current images to be placed�� 5�      5�      �� 5�      
�������$��	0,0.6,0,0q�5�   !   $	��0,8q�6=   � 5�      .+�5�      �� $��Italic$�6(   Press F1 for more help6S   5�      5�      5�      .��5�      �� $  
Horizontal=�6   $��5q�5�      .+�5�      �� $��/Resources/file_icon.png��6   $��20��5�      $��20��5�      $��0,0,0,0q�5�      �� 5�      $#��  Download and Add to Plylst ��6   
�������$��Center=�5�      $��Center=�5�      $
��auto��5�      �� 5�      6   5�      5�      5�      5�      