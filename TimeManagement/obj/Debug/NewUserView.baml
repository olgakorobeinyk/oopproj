   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	        TimeManagement.NewUserView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TimeManagement     5   	     � Title$
  User��5   	   $	��500��6   $	��700��6#   ���#cce6ff � DataContext 5	   
   %  TimeManagement.NewUserViewModel  5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $��50 �6   6*   �� 5      $��50 �6   6*   .+�5   
   �� $��20(�6   $��Right=�6   $��Center=�6:   $��0��6U   $��0��6e   $��0,6,10,9q�6r   .��6�   	User Name6�   �� 5   
   $��20(�6   $��Right=�6   $��Center=�6:   $��0��6U   $��1��6e   $��0,5,10,8q�6r   .��6�   
User Email6�   �� 5   
   -   5        $��UserName��5   G   $	��200��6   $��1��6   $��0��6.   $��40��6;   ��6X   �� *name+�� 5   
   -   5        $��	UserEmail��5   G   $	��200��6   $��1��6   $��1��6.   $��40��6;   ��6Y   �� *email+�� 5   
   -   5        $��SaveUser��5   7   $��20(�6   $	��200��6   $��40��6+   $
��Save��6H   $��1��6W   $��2��6g   $
��Left=�6t   ���#fff2cc6�   5      5      