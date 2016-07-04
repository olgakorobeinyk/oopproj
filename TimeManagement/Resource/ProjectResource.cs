using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using TimeManagement.Model;

namespace TimeManagement.Resource
{
    public class ProjectResource : DBConnection
    {
        public Project getProject(long Id)
        {

            SQLiteCommand command = this.getCommand(String.Format("SELECT * FROM Project WHERE ID={0};", Id));
            SQLiteDataReader reader = command.ExecuteReader();

            Project project = new Project();
            if (reader.Read())
            {
                project.setId(reader.GetInt64(0));
                project.setName(reader.GetString(1));
                project.setTickets(this.getTicketsByProject(Id));
                this.getUserProjects(project);
            }

            return project;
        }

        public void DeleteProject(Project project)
        {
            this.getCommand(String.Format("Delete from Project where Id = {0}", project.getId()))
                .ExecuteNonQuery();
        }

        public ObservableCollection<Ticket> getTicketsByProject(Project project)
        {
            SQLiteCommand command = this.getCommand(
                String.Format("SELECT * FROM Tickets WHERE ProjectID={0};",
                project.getId())
            );

            SQLiteDataReader reader = command.ExecuteReader();
            ObservableCollection<Ticket> Tickets = new ObservableCollection<Ticket>();

            while (reader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.Id = reader.GetInt64(0);
                ticket.TaskName = reader.GetString(1);
                ticket.EstimationTime = reader.GetString(3);
                ticket.UserId = reader.GetInt64(6);
                ticket.ProjectId = project.getId();
                ticket.Project = project;

                Tickets.Add(ticket);

            }

            return Tickets;
        }

        public ObservableCollection<Ticket> getTicketsByProject(long ID)
        {
            SQLiteCommand command = this.getCommand(String.Format("SELECT * FROM Tickets WHERE ProjectID={0};", ID));
            SQLiteDataReader reader = command.ExecuteReader();
            ObservableCollection<Ticket> Tickets = new ObservableCollection<Ticket>();

            while (reader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.Id = reader.GetInt64(0);
                ticket.TaskName = reader.GetString(1);
                ticket.EstimationTime = reader.GetString(3);
                ticket.UserId = reader.GetInt64(6);
                ticket.ProjectId = ID;
                Tickets.Add(ticket);

            }

            return Tickets;
        }

        public void getUserProjects(Project project)
        {
            ObservableCollection<UserProj> up = this.obtainUserProjects(project);
            project.setUserProj(up);
        }

        public ObservableCollection<UserProj> obtainUserProjects(Project project)
        {
            ObservableCollection<UserProj> up = new ObservableCollection<UserProj>();
            string query = String.Format("Select * FROM UserProject where ProjectId = {0}",
                project.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                up.Add(new UserProj(dr.GetInt64(0), dr.GetInt64(1)));
            }
            return up;
        }

        public void CreateProject(Project project)
        {
            string query = String.Format("INSERT INTO [Project] (Name) values ('{0}')", project.getName());
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            project.setId(this.getLastInsertId());

        }

        public void UpdateProject(Project project)
        {
            string query = String.Format("UPDATE [Project] SET Name = '{0}' WHERE Id={1}",
                project.getName(), project.getId());
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader reader = command.ExecuteReader();
        }

        public ObservableCollection<Project> getProjects()
        {
            SQLiteCommand command = this.getCommand("SELECT * FROM Project;");
            SQLiteDataReader reader = command.ExecuteReader();
            ObservableCollection<Project> oc = new ObservableCollection<Project>();

            while (reader.Read())
            {
                Project project = new Project();
                project.setId(reader.GetInt64(0));
                project.setName(reader.GetString(1));
                project.setTickets(this.getTicketsByProject(project.getId()));
                this.getUserProjects(project);
                oc.Add(project);
            }

            return oc;
        }
    }
}
