   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	   "    TimeManagement.NewTicketView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -userclr-namespace:TimeManagement     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Ticket��5	   	   $	��500��6   $	��700��6%   ���#cce6ff � DataContext 5
      '  !TimeManagement.NewTicketViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��20(�6   $��
*Task name��6   $��Right=�63   $��32��6O    J� RenderTransformOrigin$ 0.206,0.464&�6[   $��	0,6,28,13q�6   �� 5   
   $��20(�6   $��1��6    $
��Left=�60   $��40��6K    �� TextWrapping$
 Wrap=�6X   $	��342��6m   .��6y   ��5      �� 5       �� Path$
 Task�6   5      .��5       5!      �� 5"   
   $��20(�6   $��*Enter time��6   $��Right=�64   $��32��6P   $��0��6\   $��1��6l   $��	0,5,28,14q�6y   �� 5#   
   $��20(�6   $
��Left=�6    $��40��6;   $
 Wrap=�6G   $	��200��6r   $��1��6~   $��1��6�   ��6[   �� *Time+6�   �� 5$   
   $��20(�6   $��0��6   $��2��6.   $��*Select Project��6;   $��Right=�6U   $��32��6q   $��	0,6,28,13q�6}   �� 5%   
   -   5         ��Name$ comboBox��5%   >   $��20(�6   $��1��6!   $��2��61   $
��Left=�6P   $	��200��6l   $��40��6x   ���#fff2cc��6�   �� *
Projects+ �� SelectedItem 6�   �� *CurrentProject+6  �� 5&   
   $��20(�6   $��3��6   $��*Select User��6+   $��32��6C   $��Right=�6O   $��	0,5,28,14q�6k   �� 5'   
   -   5        $ UsersBox��5'   >   $��20(�6   $��1��6!   $��3��61   $
��Left=�6P   $	��200��6l   $��40��6x   ���#fff2cc��6�   �� *Users+ 6�   �� *CurrentUser+6�   �� 5(   
   -   5        $��20(�5(      $��1��6   $��5��6/   $
��Save��6<   $
��Left=�6`   $	��Top=�6{   $	��200��6�   $��40��6�   ���#fff2cc6�   5)      5*      