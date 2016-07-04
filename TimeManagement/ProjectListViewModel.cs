using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using TimeManagement.Model;
using TimeManagement.Resource;

namespace TimeManagement
{
    class ProjectListViewModel : ViewModelBase
    {
        public ObservableCollection<Project> Projects { set; get;}
        public ProjectListViewModel() : base()
        {
            Projects = new ProjectResource().getProjects();
        }

        public void DeleteProject(Project project)
        {
            Projects.Remove(project);
            project.delete();

        }

        public void UsersPropertyChanged(Project project)
        {
            int index = this.Projects.IndexOf(project);
            this.Projects.RemoveAt(index);
            this.Projects.Insert(index, project);
            this.RaisePropertyChanged("Projects");
        }
    }
}
