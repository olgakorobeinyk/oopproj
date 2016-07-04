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
    public class TicketResource : DBConnection
    {
        private ProjectResource ProjectResource = new ProjectResource();
        private UserResource UserResource = new UserResource();

        public long createTicket(string TaskName, string EstimationTime, long ProjectId, long UserId)
        {
            string query = "INSERT INTO Tickets (TaskName, Estimation, ProjectID, UserId) values('{0}', '{1}', '{2}', '{3}');";
            SQLiteCommand command = this.getCommand(
                String.Format(query, TaskName, EstimationTime, ProjectId, UserId)
             );
            command.ExecuteNonQuery();
            return this.getLastInsertId();
        }

        public void updateTicket(string TaskName, string EstimationTime, long ProjectId, long UserId, long Id)
        {
            this.getCommand(
                String.Format(
                    "UPDATE Tickets SET TaskName = '{0}', Estimation = '{1}', ProjectId='{2}', UserId='{3}' WHERE ID = {4};",
                    TaskName, EstimationTime, ProjectId, UserId, Id
                )
            ).ExecuteNonQuery();

        }

        public bool removeTicket(long id)
        {
            string query = String.Format("DELETE FROM Tickets WHERE ID={0}", id);
            SQLiteCommand command = this.getCommand(query);
            command.ExecuteNonQuery();
            return true;
        }

        public ObservableCollection<Ticket> getAllTickets()
        {
            SQLiteCommand command = this.getCommand("SELECT * FROM Tickets");
            SQLiteDataReader reader = command.ExecuteReader();
            ObservableCollection<Ticket> Tickets = new ObservableCollection<Ticket>();

            while (reader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.Id = reader.GetInt64(0);
                ticket.TaskName = reader.GetString(1);
                ticket.EstimationTime = reader.GetString(3);
                ticket.UserId = reader.GetInt64(6);
                ticket.ProjectId = reader.GetInt64(2);
                Tickets.Add(ticket);
            }

            return Tickets;
        }
    }
}
