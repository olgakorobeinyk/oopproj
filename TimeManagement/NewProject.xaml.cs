using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using TimeManagement.Model;

namespace TimeManagement
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : Window
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewProjectViewModel projectModel = (NewProjectViewModel)this.DataContext;
            ObservableCollection<User> users = new ObservableCollection<User>();
            projectModel.setUsers(this.UserList.SelectedItems);
            projectModel.Save();
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            NewProjectViewModel projectModel = (NewProjectViewModel)this.DataContext;

            foreach (User user in lb.ItemsSource)
            {
                if (projectModel.project.hasUser(user))
                {
                    lb.SelectedItems.Add(user);
                }
                
            }
        }

        private void UserList_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
