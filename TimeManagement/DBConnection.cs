using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace TimeManagement
{
    public class DBConnection
    {
        static private SQLiteConnection SqlConnection;

        private SQLiteConnection OpenSQLiteConnection()
        {
            if (DBConnection.SqlConnection != null)
            {
                return DBConnection.SqlConnection;
            }

            var inputFile = "TaskManagement.s3db";
            string connectionString = String.Format("Data Source={0}", inputFile);

            DBConnection.SqlConnection = new SQLiteConnection(connectionString);
            try
            {
                DBConnection.SqlConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return DBConnection.SqlConnection;
        }

        private void CloseSQLiteConnection(SQLiteConnection connection)
        {
            connection.Close();
        }

        public void executeQuery(string query)
        {
            SQLiteTransaction sqlTransaction = this.OpenSQLiteConnection().BeginTransaction();
            SQLiteCommand createCommand = new SQLiteCommand(query, DBConnection.SqlConnection);
            createCommand.ExecuteNonQuery();
            createCommand.Dispose();
            sqlTransaction.Commit();
        }

        public SQLiteCommand getCommand(string query)
        {
            SQLiteCommand command = new SQLiteCommand(OpenSQLiteConnection());
            command.CommandText = query;
            return command;
        }

        public long getLastInsertId()
        {
            return (long)this.getCommand("select last_insert_rowid()").ExecuteScalar();
        }
    }
}
