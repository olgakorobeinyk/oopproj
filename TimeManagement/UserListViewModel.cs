using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

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
        }
    }
}
