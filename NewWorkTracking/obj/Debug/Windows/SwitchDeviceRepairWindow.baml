   M S B A M L   `   `   `  ����H  DWork_Tracking, Version=2.0.7.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:NewWorkTracking.WindowsNewWorkTracking.Windows  5      G(clr-namespace:NewWorkTracking.ViewModelsNewWorkTracking.ViewModels  5      C&clr-namespace:NewWorkTracking.BehaviorNewWorkTracking.Behavior  5	      6    0NewWorkTracking.Windows.SwitchDeviceRepairWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      7local%clr-namespace:NewWorkTracking.Windows     5      7vm(clr-namespace:NewWorkTracking.ViewModels     5      6beh&clr-namespace:NewWorkTracking.Behavior     5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Finter:http://schemas.microsoft.com/expression/2010/interactivity  5
      -   5        $"��SwitchDeviceRepairWindowXaml��5        � Title$+  %Перемещение ремонта��5      $	��181��5      $	��300��5         WindowStyle#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5       � DataContext 5      =  7NewWorkTracking.ViewModels.SwitchDevicesRepairViewModel  5   
   .  (System.Windows.Interactivity.Interaction   	Behaviors 5      -  'NewWorkTracking.Behavior.WindowBehavior  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��5* �5      �� 5       $	��100 �5!      �� 5"      $��5* �5#      �� 5$      $	��100 �5%      �� 5&      $��7* �5'       � RowDefinitions 5)   
   �� 5*      $	��64* �5+      �� 5,      $	��56* �5-      �� 5.      $	��35* �5/      �� 50      $	��18* �51      .+�54   
   �� $��1��55      $��3��56      $	��Top=�57      $M��GВыберите инв.номер для переноса работы��58      $��Center=�59      �� 5;   
   $��	20,0,20,0q�5<      $��1��5=      $
��Wrap=�5>      $��Center=�5?      $��Center=�5@      $��3��5B      $��1��5C      ��5A      �� *Description+5C      �� 5E   
   $��
20,40,20,0q�5F       �� DisplayMemberPath$ 	InvNumber��5I      $��3��5J      $��1��5K      ��5G      �� *	Devices+ �� Text 5H      �� *SelectedDeviceInv+5K      �� 5M   
   $��Переместить��5O      $��2��5P      $��1��5Q      $
��Auto��5R      $
��Auto��5S      ��5N      �� *SwitchDeviceRepair+5S      �� 5U   
   $��Закрыть��5W      $��2��5X      $��3��5Y      $
��Auto��5Z      $
��Auto��5[      ��5V      �� *CloseWindow+5[      5\      5]      