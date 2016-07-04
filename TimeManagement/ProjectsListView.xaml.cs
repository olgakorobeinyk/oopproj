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
    /// Interaction logic for ProjectsListView.xaml
    /// </summary>
    public partial class ProjectsListView : Window
    {
        public ProjectsListView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button initiator = (Button)sender;

            ((ProjectListViewModel)this.DataContext).DeleteProject((Project)initiator.DataContext);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Project project = (Project) button.DataContext;
            NewProject projectView = new NewProject();
            NewProjectViewModel contextUserModel = (NewProjectViewModel)(projectView.DataContext);
            contextUserModel.SetProject(project);

            projectView.Show();
            projectView.Closing += (closingSender, closingE) => onUserUpdated(closingSender, closingE, (ProjectListViewModel)this.DataContext, project);
        }

        void onUserUpdated(object sender, EventArgs e, ProjectListViewModel context, Project project)
        {
            context.UsersPropertyChanged(project);
        }
}
}
