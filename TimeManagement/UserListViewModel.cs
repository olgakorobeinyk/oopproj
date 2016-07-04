using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using TimeManagement.Model;

namespace TimeManagement
{


    class UserListViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users
        {
            set;
            get;
        }

        public UserListViewModel() : base()
        {
            this.Users = DBHelper.getInstance().getUsers();
        }

        public void removeUser(User user)
        {
            user.delete();
            this.Users.Remove(user);
            RaisePropertyChanged("Users");
        }

        public void UsersPropertyChanged(User user)
        {
            int index = this.Users.IndexOf(user);
            this.Users.RemoveAt(index);
            this.Users.Insert(index, user);
            this.RaisePropertyChanged("Users");
        }
    }
}
