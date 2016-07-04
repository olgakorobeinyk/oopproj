using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using TimeManagement.Model;

namespace TimeManagement.Resource
{
    public class UserProjResource : DBConnection
    {
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

        public void processUserProjects(Project project)
        {
            string query = String.Format("Delete FROM UserProject WHERE ProjectId = {0}",
                project.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
        }

        public void processUserProjects(User User)
        {
            string query = String.Format("Delete FROM UserProject WHERE UserId = {0}",
                User.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
        }

        public void deleteUserProj(long UserId, long ProjectId)
        {
            string query = String.Format("Delete FROM UserProject WHERE UserID = {0} AND ProjectId = {1}",
                UserId,
                ProjectId
                );
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
        }

    }
}
