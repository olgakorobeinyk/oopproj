using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TimeManagement.Model;

namespace TimeManagement
{
    class NewUserViewModel : ViewModelBase
    {
        private User user;
        public string name { set; get; }
        public string email { set; get; }

        public NewUserViewModel() : base()
        {
            this.user = new User();
        }


        public void setUser(User user)
        {
            this.user = user;
            this.name = user.getUsername();
            this.email = user.getEmail();
            RaisePropertyChanged("name");
            RaisePropertyChanged("email");
        }

        public void save()
        {
            this.user.setUsername(this.name);
            this.user.setEmail(this.email);
            this.user.save();
        }
    }
}
