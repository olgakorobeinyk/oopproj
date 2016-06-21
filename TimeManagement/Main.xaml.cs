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

namespace TimeManagement
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTicketView ticketView = new NewTicketView();
            ticketView.Show();
            ticketView.Closing += this.CloseWindow;
        }
        public void CloseWindow(object sender, EventArgs e)
        {
            ((MainView)this.DataContext).reloadProjects();
        }
        private void Closing_Child_Windows(object sender, EventArgs e)
        {
            int test = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewProject projectView = new NewProject();
            projectView.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NewUserView userView = new NewUserView();
            userView.Show();
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox projectComboModel = (ComboBox)sender;
            MainView dc = (MainView)this.DataContext;
            if (projectComboModel.SelectedItem != null)
            {
                dc.setUsersByProject((Project)projectComboModel.SelectedItem);
                dc.setTickets((Project)projectComboModel.SelectedItem);
            }
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ProjectCBModel = (ComboBox)this.ProjectList;
            ComboBox UserCBModel = (ComboBox)sender;
            MainView dc = (MainView)this.DataContext;
            if (UserCBModel.SelectedItem != null)
            {
                dc.setTickets((Project)ProjectCBModel.SelectedItem, (User)UserCBModel.SelectedItem);
            } 
        }
    }
}
