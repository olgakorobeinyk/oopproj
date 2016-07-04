using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TimeManagement.Resource;

namespace TimeManagement.Model
{
    public class Project : Model
    {
        private long Id;
        public string Name { set; get; }
        private ObservableCollection<Ticket> Tickets = new ObservableCollection<Ticket>();
        private ObservableCollection<UserProj> UserProjects = new ObservableCollection<UserProj>();
        protected UserProjResource UserProjResource = new UserProjResource();
        protected ProjectResource ProjectResource = new ProjectResource();
        protected TicketResource TicketResource = new TicketResource();

        public void setName(string name)
        {
            this.Name = name;
        }

        public void setId(long ID)
        {
            this.Id = ID;
        }

        public virtual string getName()
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

        public virtual ObservableCollection<Ticket> getTickets()
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

        public override void save()
        {
            if (this.Id == 0)
            {
                this.ProjectResource.CreateProject(this);
            }
            else
            {
                this.ProjectResource.UpdateProject(this);
            }

            foreach (Ticket ticket in this.Tickets)
            {
                ticket.ProjectId = this.getId();
                this.TicketResource.updateTicket(
                    ticket.TaskName, 
                    ticket.EstimationTime, 
                    ticket.ProjectId, 
                    ticket.UserId, 
                    ticket.Id
                );            
            }

            this.UserProjResource.processUserProjects(this);

            foreach (UserProj up in this.UserProjects)
            {
                this.UserProjResource.processUserProj(up);
            }
        }

        public override void delete()
        {
            this.ProjectResource.DeleteProject(this);
        }

        public Boolean hasUser(User user)
        {
            foreach(UserProj up in this.UserProjects)
            {
                if (up.getUser() == null)
                {
                    this.UserProjects.Remove(up);
                    up.setDelete();
                    up.delete();
                    continue;

                }
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
