   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	       TimeManagement.NewProject   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7eo(http://schemas.essentialobjects.com/wpf/     5   	   -userclr-namespace:TimeManagement     5   	     � Title$  Project��5	   	   $	��500��6   $	��700��6&   ���#cce6ff � DataContext 5
      (  "TimeManagement.NewProjectViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��200 �6   �� 5      $��90 �6   �� 5      $��90 �6   .+�5   
   �� $��20(�6   $��Right=�6   $��Center=�6:   $��0��6U   .��6e   Project name*:6u   �� 5   
   $��20(�6   $��1��6    $	��350��60   $��40��6=   $��5q�6I   ��6T   �� *name+6l   �� 5   
   $��20(�6   $��Right=�6   $	��Top=�6:   $��0��6R   $��1��6b   .��6o   Assign users for the project:6�   �� 5   
   -   5        $��UserList��5       $��20(�6   $	��350��60   $	��400��6<   $��1��6J   $��1��6Z    �� SelectionMode$ Multiple=�6g   ��6�   �� *Users+5      �� 5   
   -   5        $��
SaveButton��5       $��20(�6   $��40��63   $
��Save��6?   $��2��6N   $��1��6[   ���#fff2cc6�   5      5      