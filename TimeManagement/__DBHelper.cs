using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.IO;
using System.Data;
using System.Collections.ObjectModel;
using TimeManagement.Model;
namespace TimeManagement
{
    public class DBHelper : Window
    {
        private SQLiteConnection connection;
        private SQLiteConnection OpenSQLiteConnection()
        {
            if (connection != null)
            {
                return connection;
            }

            var inputFile = "TaskManagement.s3db";
            string connectionString = String.Format("Data Source={0}", inputFile); 

            connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return connection;
        }

        private static DBHelper _helper;
        
        public static DBHelper getInstance() {
            
            if (_helper == null)
            {
                _helper = new DBHelper();
            }

            return _helper;
        }

        private void CloseSQLiteConnection(SQLiteConnection connection)
        {
            connection.Close();
        }

        private void CreateDB(SQLiteConnection connection)
        {
            string createTableSQL = "CREATE TABLE [Tickets] (" +
        "[ID] INTEGER PRIMARY KEY AUTOINCREMENT," +
        "[TaskName] TEXT  NOT NULL," +
        "[Estimation] REAL  NOT NULL," +
        "[CreatedDate] DATETIME DEFAULT CURRENT_TIMESTAMP," +
        "[Duration] REAL  NULL" +
        ")";
            this.executeQuery(createTableSQL);

            string createTableProject = "CREATE TABLE [Project] (" +
                "[ID] INTEGER PRIMARY KEY AUTOINCREMENT," +
                "[Name] VARCHAR NOT NULL" +
            ")";

            this.executeQuery(createTableProject);
            
        }

        public void executeQuery(string query)
        {
            SQLiteTransaction sqlTransaction = this.OpenSQLiteConnection().BeginTransaction();
            SQLiteCommand createCommand = new SQLiteCommand(query, connection);
            createCommand.ExecuteNonQuery();
            createCommand.Dispose();
            sqlTransaction.Commit();
        }

         void createTicket (Ticket ticket)
        {
            string query = "INSERT INTO Tickets (TaskName, Estimation, ProjectID, UserId) values('{0}', '{1}', '{2}', '{3}');";
            SQLiteCommand command = this.getCommand(
                String.Format(query, ticket.TaskName, ticket.EstimationTime.ToString(), ticket.ProjectId, ticket.UserId)
             );
            command.ExecuteNonQuery();
            ticket.Id = this.getLastInsertId();
        }

        public void updateTicket(Ticket ticket)
        {
            this.getCommand(
                String.Format(
                    "UPDATE Tickets SET TaskName = '{0}', Estimation = '{1}', ProjectId='{2}', UserId='{3}' WHERE ID = {4};",
                    ticket.TaskName, ticket.EstimationTime, ticket.ProjectId, ticket.UserId, ticket.Id
                )
            ).ExecuteNonQuery();
            
        }

        public DataTable GetAllTasks()
        {
            string query = "SELECT ID as ID, TaskName as Name, Estimation as Estimation FROM Tickets;";
            DataTable dt = new DataTable();  
            try
            {
                SQLiteConnection cnn = OpenSQLiteConnection();
                SQLiteCommand command = new SQLiteCommand(cnn);
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }  
            catch (Exception e)  
            {
                MessageBox.Show(e.Message);
            }  
            return dt;  
        }

        public SQLiteCommand getCommand(string query)
        {
            SQLiteCommand command = new SQLiteCommand(OpenSQLiteConnection());
            command.CommandText = query;
            return command;
        }

         bool removeColumn(Ticket tik)
        {
            string query = String.Format("DELETE FROM Tickets WHERE ID={0}", tik.Id);
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            return true;
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

        public Project getProject(long Id) {
            
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

        public void linkUserProject(Project project, User user) {
            string query = String.Format("INSERT INTO [UserProject] (UserID, ProjectId) values ('{0}', '{1}')",
                user.getId(),
                project.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
        }

        public User getUser(long id)
        {
            string query = String.Format("Select * from Users where Id = {0}", id );
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            User user = new User();
            user.setId(reader.GetInt64(0));
            user.setUsername(reader.GetString(1));
            user.setEmail(reader.GetString(2));
            this.getUserProjects(user);
            return user;
        }

        public void deleteUser(User user)
        {

            this.getCommand(String.Format("Delete from Users where Id = {0}", user.getId()))
                .ExecuteNonQuery();
        }

        public ObservableCollection<User> getUsers()
        {
            ObservableCollection<User> up = new ObservableCollection<User>();
            SQLiteCommand command = this.getCommand("Select * from Users");
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.setId(reader.GetInt64(0));
                user.setUsername(reader.GetString(1));
                user.setEmail(reader.GetString(2));
                up.Add(user);
            }
            return up;
        }

        public ObservableCollection<User> getUsers(Project project)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            ObservableCollection<UserProj> up = this.obtainUserProjects(project);
            foreach (UserProj userProj in up)
            {
                users.Add(userProj.getUser());
            }
            return users;
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

        public void getUserProjects(User user)
        {
            ObservableCollection<UserProj> up = new ObservableCollection<UserProj>();
            string query = String.Format("Select * FROM UserProject where UserID = {0}", 
                user.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader dr = command.ExecuteReader();
            while(dr.Read()) {
                    up.Add(new UserProj(dr.GetInt64(0), dr.GetInt64(1)));
            }
            user.setUserProjects(up);
        }

        public User createUser(User user)
        {
            string query = String.Format("INSERT INTO [Users] (Username, Email) values ('{0}', '{1}')", 
                user.getUsername(),
                user.getEmail()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            user.setId(this.getLastInsertId());
            
            return user;
        }

        public User updateUser(User user)
        {
            string query = String.Format("UPDATE [Users] SET Username = '{0}', Email = '{1}' WHERE Id = {2}",
                user.getUsername(),
                user.getEmail(),
                user.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            return user;
        }

        public void processUserProjects(Project project)
        {
            string query = String.Format("Delete FROM UserProject WHERE ProjectId = {0}",
                project.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
        }

        public void processUserProj(UserProj up)
        {
            if (up.isDelete() == false)
            {
                this.insertUserProj(up);
            }
            else
            {
                string query = String.Format("Delete FROM UserProject WHERE UserID = {0} AND ProjectId = {1}",
                up.getUser().getId(),
                up.getProject().getId()
                );
                SQLiteCommand command = this.getCommand(query);
                command.ExecuteNonQuery();
            }
        }

        public void insertUserProj(UserProj up)
        {
            string query = String.Format("SELECT * FROM [UserProject] WHERE UserID = {0} AND ProjectId = {1}",
                up.getUser().getId(),
                up.getProject().getId()
                );
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                string insertQuery = String.Format("INSERT INTO [UserProject] (UserID, ProjectId) values ({0}, {1}) ",
                    up.getUser().getId(),
                    up.getProject().getId()
                );
                SQLiteCommand commandInsert = this.getCommand(insertQuery);
                commandInsert.ExecuteNonQuery();
            }
        }
        
        public void CreateProject(Project project)
        {
            string query = String.Format("INSERT INTO [Project] (Name) values ('{0}')", project.getName());
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            project.setId(this.getLastInsertId());
            
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

        public long getLastInsertId()
        {
            return (long)this.getCommand("select last_insert_rowid()").ExecuteScalar();
        }

        public void UpdateProject(Project project)
        {
            string query = String.Format("UPDATE [Project] SET Name = '{0}' WHERE Id={1}", 
                project.getName(), project.getId());
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader reader = command.ExecuteReader();
        }

        public bool SaveProject(Project project)
        {
            if (project.isNew())
            {
                this.CreateProject(project);
            }
            else
            {
                this.UpdateProject(project);
            }

            return true;
        }
    }
}

