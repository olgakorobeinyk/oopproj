using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TimeManagement
{
    public class Project
    {
        private long Id;
        private string Name;
        private ObservableCollection<Ticket> Tickets = new ObservableCollection<Ticket>();
        private ObservableCollection<UserProj> UserProjects = new ObservableCollection<UserProj>();

        public void setName(string name)
        {
            this.Name = name;
        }

        public void setId(long ID)
        {
            this.Id = ID;
        }

        public string getName()
        {
            return this.Name;
        }

        public long getId()
        {
            return this.Id;
        }

        public bool isNew()
        {
            return this.Id == 0 ? true: false;
        }
        
        public void setTickets(ObservableCollection<Ticket> Tickets)
        {
            this.Tickets = Tickets;
        }

        public ObservableCollection<Ticket> getTickets()
        {
            return this.Tickets;
        }

        public ObservableCollection<Ticket> getTickets(User user)
        {
            ObservableCollection<Ticket> TicketsCollection = new ObservableCollection<Ticket>();
            foreach (Ticket ticket in this.getTickets())
            {
                User userObj = ticket.getUser();
                if (userObj == null)
                {
                    continue;
                }

                if (userObj.Equals(user))
                {
                    TicketsCollection.Add(ticket);
                }
            }

            return TicketsCollection;
        }

        public void addUser(User user)
        {
            bool found = false;
            foreach (UserProj p in this.UserProjects)
            {
                if (p.getUser().getId() == user.getId())
                {
                    p.unsetDelete();
                    found = true;
                }
            }

            if (!found)
            {
                this.UserProjects.Add(new UserProj(user, this));
            }
        }

        public ObservableCollection<User> getUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            foreach(UserProj us in this.UserProjects)
            {
                users.Add(us.getUser());
            }

            return users;
        }

        public void setUserProj(ObservableCollection<UserProj> UserProjects)
        {
            this.UserProjects = UserProjects;
        }

        public void removeUser(User user)
        {
            bool found = false;

            foreach (UserProj p in this.UserProjects)
            {
                if (p.getUser().getId() == user.getId())
                {
                    p.setDelete();
                    found = true;
                }
            }

            if (!found)
            {
                this.UserProjects.Add(new UserProj(user, this, true));
            }
        }

        public void save()
        {
            if (this.Id == 0)
            {
                DBHelper.getInstance().CreateProject(this);
            }
            else
            {
                DBHelper.getInstance().UpdateProject(this);
            }

            foreach (Ticket ticket in this.Tickets)
            {
                ticket.ProjectId = this.getId();
                DBHelper.getInstance().updateTicket(ticket);            
            }

            DBHelper.getInstance().processUserProjects(this);

            foreach (UserProj up in this.UserProjects)
            {
                DBHelper.getInstance().processUserProj(up);
            }
        }

        public Boolean hasUser(User user)
        {
            foreach(UserProj up in this.UserProjects)
            {
                if (up.getUser().getId() == user.getId())
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return this.getName();
        }

        public void clearUsers()
        {

            UserProjects.Clear();
        }
    }
}
