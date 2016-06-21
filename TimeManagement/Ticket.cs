using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagement
{
    public class Ticket
    {
        public string TaskName { get; set; }
        public string EstimationTime { get; set; }


        public Project Project;
        public long Id;
        public long UserId;
        private User User;
        private long projectId;
        public long ProjectId
        {
            get {
                 if (this.Project != null)
                {
                    this.projectId = this.Project.getId();
                }

                return this.projectId;
            }
            set
            {
                this.projectId = value;
            }
        }

        public Project getProject()
        {
            if (Project != null)
            {
                return this.Project;
            }

            return this.Project = DBHelper.getInstance().getProject(ProjectId);
        }

        public void save()
        {
            if (this.Id == 0)
            {
                DBHelper.getInstance().createTicket(this);
            }
            else
            {
                DBHelper.getInstance().updateTicket(this);
            }

        }

        public void setUser(User user)
        {
            this.User = user;
            if (this.User != null)
            {
                this.UserId = user.getId();
            }
            
        }

        public User getUser()
        {
            if (this.User != null )
            {
                return this.User;
            }
            
            if (this.UserId != 0)
            {
                this.User = DBHelper.getInstance().getUser(this.UserId);
            }

            return this.User;
        }

        public override string ToString()
        {
            return this.TaskName;
        }
    }
}
