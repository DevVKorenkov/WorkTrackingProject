   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      H DWork_Tracking, Version=2.0.7.8, Culture=neutral, PublicKeyToken=nullC&clr-namespace:NewWorkTracking.BehaviorNewWorkTracking.Behavior 5      1clr-namespace:NewWorkTrackingNewWorkTracking 5	      G(clr-namespace:NewWorkTracking.ViewModelsNewWorkTracking.ViewModels 5
      !   NewWorkTracking.ProgressBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      binterPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      6beh&clr-namespace:NewWorkTracking.Behavior     5      /localclr-namespace:NewWorkTracking     5	      7vm(clr-namespace:NewWorkTracking.ViewModels     5
      -   5        $��ProgrBar��5         WindowStyle#���   5        
WhiteBrush#��[ 5        � WindowStartupLocation$  CenterScreen=�5       � WindowStyle$
 none=�5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ Height=�5      $��99.501��5      $��429.143��5       � Title 5      �� *Status+{�5      �� *ControlEnable+ � DataContext 5      5 /NewWorkTracking.ViewModels.ProgressBarViewModel  5   
   .   (System.Windows.Interactivity.Interaction   	Behaviors 5      - 'NewWorkTracking.Behavior.WindowBehavior  5   
   .��5       �  � RowDefinitions 5!   
   �� 5"      $	��49* �5#      �� 5$      $	��23* �5%      �� 5&      $	��28* �5'      .+�5)   
   �� $��0��5*      .+�5,      � $��3��5-       � 	Resources 5/      	 �� BasedOn
 �� 
TargetType �� Triggers �� Binding �� ElementName �� Path �� Value �� Property �� Value��%y  '�       7��� 50      8	   "
 �52       53      �� 54       55   !   �� $ 
ButtonDock��$ 
Visibility� 	Collapsed56   !   .a�57   "   ��  �@�IsIndeterminate . 59   %   5:      �� 5;       5<   !   �� $ 
ButtonDock��$ 
Visibility� Visible5=   !   .a�5>   "   ��  �@�IsIndeterminate .  5@   %   5A      5C      5D      5E      �� 5G      $��
20 20 20 0q�5H      $��Center=�5I      $��Center=�5J      $
��Wrap=�5L      ��5K      �� *Status+5L      5N      ]� 5P   
   -   5         ]�Name$ 
ButtonDock��5Q      $��1��5R      $��Center=�5S      $��10q�5T      y�5U      �� *
ButtonsVis+.+�5W      �� $��5 0 5 0q�5X      $
��Да��5Y      $	��Yes��5[      ��5Z      �� *
ChouseMenu+5[      �� 5\      $��5 0 5 0q�5]      $��Нет��5^      $��No��5`      ��5_      �� *
ChouseMenu+5`      �� 5a      $��5 0 5 0q�5b      $��Повторить��5c      $��Repeat��5e      ��5d      �� *
ChouseMenu+5e      5g      �� 5i   
   $��2��5j      y�5k      �� *ChangeTextBoxVis+.+�5m      �� -   5         ��Name$ txbChangeServer��5n      $��	20 0 20 5q�5o      $��Center=�5p      ��5q      �� *	NewServer+ �� UpdateSourceTrigger$ PropertyChanged=�.��6Q    J� InputBindings 5s      �� 5t      
 �� Key$ Enter��5u       �� Command 5v      �� *ChangeServer+.��5w   )   5y      �� 5{      $��5 0 5 5q�5|      $��Применить��5}      ��5~      �� *ChangeServer+5�      �� 5�   
   $	��top=�5�      $��Right=�5�      $��15��5�      $��15��5�      
���  ��  ButtonTemplate#���  5�      $��No��5�      ��5�      �� *
ChouseMenu+.��5�      �� $��/Resources/Close.png��5�      $��20��5�      $��20��5�      $��Center=�5�      $��Center=�5�       5�       :� RoutedEvent �� TargetProperty	 W� To g� Duration��%6  '��       �� 5�      "
 ��5�       5�      :� 5�      $ 
MouseEnter��5�   !   [�5�   "   �� 5�   &   .��5�   *   �� $ Width�5�   -   .��5�   .   W� $ 40X�5�   1   $ 0:0:0.1D�5�   1   5�   +   5�   '   5�      :� 5�      $ 
MouseLeave��5�   !   [�5�   "   �� 5�   &   .��5�   *   �� $ Width�5�   -   .��5�   .   W� $ 20X�5�   1   $ 0:0:0.1D�5�   1   5�   +   5�   '   5�      :� 5�      $ 
MouseEnter��5�   !   [�5�   "   �� 5�   &   .��5�   *   �� $ Height�5�   -   .��5�   .   W� $ 40X�5�   1   $ 0:0:0.1D�5�   1   5�   +   5�   '   5�      :� 5�      $ 
MouseLeave��5�   !   [�5�   "   �� 5�   &   .��5�   *   �� $ Height�5�   -   .��5�   .   W� $ 20X�5�   1   $ 0:0:0.1D�5�   1   5�   +   5�   '   5�      5�      5�      5�       5�       �� 
TargetType ��Name J� SnapsToDevicePixels ��Name  �� ContentStringFormat! �� ContentStringFormat" �� Triggers# P� Property$ P� Value% �� 
TargetName��%�  &	       �� 5�      " ��5�      .R�5�      �� $ border��5�      #��z��5�      #��z��5�       . ._�5�      �� $ contentPresenter��5�      #��z��5�      #��z��5�      #! z  5�      |�.  #��z��5�      #��z��5�      ��. # z 5�      #��z��5�      5�      " 5�      P� 5�      # �@��IsDefaulted$ . . �5�      �� $% border��5�   !   # � ��5�   !    � ��5�      P� 5�      # �@J�IsMouseOver$ . . �5�      �� $% border��5�   !    � ��
 �  ��5�   !   �� 5�      $% border��5�   !    � �� �#1d1d1d5�   !   5�      P� 5�      # � ��$ . . �5�      �� $% border��5�   !    � �� �	#FFC4E5F65   !   �� 5     $% border��5  !    � �� �	#FF2C628B5  !   5     P� 5     # �@d�	IsChecked$
$ TrueY�5     . �5	     �� $% border��5  !    � �� �	#FFBCDDEE5  !   �� 5     $% border��5  !    � �� �	#FF245A835  !   5     P� 5     # � {�$ .  . �5     �� $% border��5  !    � �� �	#FFF4F4F45  !   �� 5     $% border��5  !    � �� �	#FFADB2B55  !   �� 5      � �� �	#FF8383835  !   5      :� 5!     $ 
MouseEnter��6'   [�5"     �� 5#  "   .��5$  &   �� $ Width�5%  )   .��5&  *   W� $ 20X�5'  -   $ 0:0:0.1D�5(  -   5)  '   5*  #   5,     :� 5-     $ 
MouseLeave��5.     [�5/     �� 50  "   .��51  &   �� $ Width�52  )   .��53  *   W� $ 15X�54  -   $ 0:0:0.1D�55  -   56  '   57  #   59     :� 5:     $ 
MouseEnter��5;     [�5<     �� 5=  "   .��5>  &   �� $ Height�5?  )   .��5@  *   W� $ 20X�5A  -   $ 0:0:0.1D�5B  -   5C  '   5D  #   5F     :� 5G     $ 
MouseLeave��5H     [�5I     �� 5J  "   .��5K  &   �� $ Height�5L  )   .��5M  *   W� $ 15X�5N  -   $ 0:0:0.1D�5O  -   5P  '   5Q  #   5S     5U     5V     5W     5X     5Y     