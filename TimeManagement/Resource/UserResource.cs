using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Collections.ObjectModel;
using TimeManagement.Model;

namespace TimeManagement.Resource
{
    public class UserResource : DBConnection
    {
        public User getUser(long id)
        {
            string query = String.Format("Select * from Users where Id = {0}", id);
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

        public void getUserProjects(User user)
        {
            ObservableCollection<UserProj> up = new ObservableCollection<UserProj>();
            string query = String.Format("Select * FROM UserProject where UserID = {0}",
                user.getId()
                );
            SQLiteCommand command = this.getCommand(query);
            SQLiteDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
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

        public void deleteUser(User user)
        {

            this.getCommand(String.Format("Delete from Users where Id = {0}", user.getId()))
                .ExecuteNonQuery();
        }

        
    }
}
