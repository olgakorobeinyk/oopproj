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
using TimeManagement.Model;

namespace TimeManagement
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : Window
    {
        public UserListView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int test = 1;
            Button button = (Button)sender;
            User user = (User) button.DataContext;
            UserListViewModel userListViewModel = (UserListViewModel)this.DataContext;
            userListViewModel.removeUser(user);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.DataContext;
            NewUserView userView = new NewUserView();
            NewUserViewModel contextUserModel = (NewUserViewModel)(userView.DataContext);
            contextUserModel.setUser(user);
            
            userView.Show();
            userView.Closing += (closingSender, closingE) => onUserUpdated(closingSender, closingE, (UserListViewModel)this.DataContext, user);
        }

        void onUserUpdated(object sender, EventArgs e, UserListViewModel context, User user)
        {
            context.UsersPropertyChanged(user);
        }
    }
}
