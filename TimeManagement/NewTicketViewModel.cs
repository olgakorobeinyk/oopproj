using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TimeManagement.Model;

namespace TimeManagement
{
    class NewTicketViewModel: ViewModelBase
    {
        public Project CurrentProject {
            get; set;
        }

        public User CurrentUser
        {
            set;get;
        }

        public ObservableCollection<User> Users{
            get;set;
        }

        public Ticket ticket;

        public ObservableCollection<Project> Projects
        {
            set;get;
        }
        
        public NewTicketViewModel() : base() 
        {
            this.ticket = new Ticket();
            this.Projects = DBHelper.getInstance().getProjects();
            this.Users = DBHelper.getInstance().getUsers();
        }

        public NewTicketViewModel(IMessenger message) : base(message)
        {
            this.ticket = new Ticket();
            this.Projects = DBHelper.getInstance().getProjects();
            this.Users = DBHelper.getInstance().getUsers();
        }

        public string Title { get { return "Task Manager"; } }

        public string Task 
        {
            get { return this.ticket.TaskName; } 
            set
            {
                this.ticket.TaskName = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Project> projectList
        {
            get {
                return this.Projects;
            }
        }

        public string Time
        {
            get { return this.ticket.EstimationTime; }
            set
            {
                this.ticket.EstimationTime = value;
                RaisePropertyChanged();
            }
        }


        public void DoSave()
        {
            this.ticket.Project = this.CurrentProject;
            this.ticket.setUser(this.CurrentUser);
            this.ticket.save();
        }

        public System.Action<object, SelectionChangedEventArgs> onSelectionChanged  {
            get
            {
                return (object ob, SelectionChangedEventArgs st) =>
                {
                    int test = 1;

                };
            }
        }

        public void setProject(Project pr)
        {
            this.ticket.Project = pr;
        }

        public void updateUserList(Project pr)
        {
            ObservableCollection<UserProj> up = DBHelper.getInstance().obtainUserProjects(pr);
            this.Users.Clear();

            foreach (UserProj userProjModel in up)
            {
                this.Users.Add(userProjModel.getUser());
            }

        }
    }

}
