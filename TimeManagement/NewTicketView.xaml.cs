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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeManagement
{
    /// <summary>
    /// Interaction logic for NewTicketView.xaml
    /// </summary>
    public partial class NewTicketView : Window
    {
        //private WindowProject _parent;
        public NewTicketView()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboModel = (ComboBox)sender;
            if (comboModel.SelectedItem != null)
            {
                NewTicketViewModel model = (NewTicketViewModel)this.DataContext;
                Project project = (Project)comboModel.SelectedItem;
                model.setProject(project);
                model.updateUserList(project);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTicketViewModel model = (NewTicketViewModel)this.DataContext;
            model.DoSave();
            this.Close();
        }
    }
}


