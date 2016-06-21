using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace TimeManagement
{
    class MainView : ViewModelBase
    {
        public ObservableCollection<Project> Projects
        {
            set;
            get;
        }
        public ObservableCollection<User> Users
        {
            set;
            get;
        }

        public ObservableCollection<Ticket> Tickets {
            get;set;
        }

        public  MainView() : base()
        {
            this.Projects = DBHelper.getInstance().getProjects();
            this.Users = new ObservableCollection<User>();
            this.Tickets = new ObservableCollection<Ticket>();
        }
        
        public void setUsersByProject(Project project)
        {
            Users = project.getUsers();
            RaisePropertyChanged("Users");
        }

        public void setTickets(Project project)
        {
            this.Tickets = project.getTickets();
            RaisePropertyChanged("Tickets");
        }

        public void setTickets(Project project, User user)
        {
            this.Tickets = project.getTickets(user);
            RaisePropertyChanged("Tickets");
        }

        public void reloadProjects()
        {
            this.Projects = DBHelper.getInstance().getProjects();
            RaisePropertyChanged("Projects");
            this.Users = new ObservableCollection<User>();
            RaisePropertyChanged("Users");
            
            this.Tickets = new ObservableCollection<Ticket>();
            RaisePropertyChanged("Tickets");
            
            
        }
    }
}
