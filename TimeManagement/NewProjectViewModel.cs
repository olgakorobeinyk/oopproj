using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections;
using TimeManagement.Model;

namespace TimeManagement
{
    class NewProjectViewModel : ViewModelBase
    {

        public Project project;

        public ObservableCollection<User> Users
        {
            set;get;
        }

        public string name
        {
            set; get;
        }

        public NewProjectViewModel() : base()
        {
            this.project = new Project();
            this.Users = DBHelper.getInstance().getUsers();
            
        }

        public void SetProject(Project project) 
        {
            this.project = project;
            this.name = this.project.getName();
            RaisePropertyChanged("name");
        }

        public Project GetProject()
        {
            return this.project;
        }

        public void Save()
        {
            this.project.setName(this.name);
            this.project.save();
        }

        public void setUsers(IList Users)
        {

            this.project.clearUsers();
            foreach (User us in Users)
            {
                this.project.addUser(us);
            }
        }
    }
}
