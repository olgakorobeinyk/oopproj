   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	       TimeManagement.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -userclr-namespace:TimeManagement     5   	   $	��350��5   !   $	��525��6.     � Title  6	   �� *Title+ � DataContext 5      '   !TimeManagement.TasksListViewModel  5   
   .��5
      � .+�5   
   �� $��Create new task��6   $
��Left=�6H   $��	10,10,0,0q�6c   $	��Top=�6v   $	��103��6�   ��6+   �� *
OpenList+6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ MyList��5   ~   $	��241��6      
IsReadOnly .     AutoGenerateColumns .  $	��496��6r   $
��Left=�6�   $	��Top=�6�   $��2��6�    J� RenderTransformOrigin$ 0.648,0.453&�6�   $��0,3,0,0q�6�   ��65   �� *	Tickets+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Task description��6&      Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 6]   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6@   �� *
TaskName+6]     5      $ 
Estimation��6%   $	 350 6\   	 69   �� *EstimationTime+6\   4  .System.Windows.Controls.DataGridTemplateColumn  5      
   CellTemplate
 5      �� 5      .R�5      �� -   5        .��5   6   Delete6>   5      5      5      5      5      