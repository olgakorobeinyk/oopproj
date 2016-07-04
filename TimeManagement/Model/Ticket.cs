using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagement.Resource;

namespace TimeManagement.Model
{
    public class Ticket : Model
    {
        public string TaskName { get; set; }
        public string EstimationTime { get; set; }


        public Project Project;
        public long Id;
        public long UserId;
        private User User;
        private long projectId;
        private ProjectResource ProjectResource = new ProjectResource();
        private TicketResource TicketResource = new TicketResource();
        private UserResource UserResource = new UserResource();

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

            return this.Project = this.ProjectResource.getProject(ProjectId);
        }

        public override void save()
        {
            if (this.Id == 0)
            {
                this.Id = this.TicketResource.createTicket(this.TaskName, this.EstimationTime.ToString(), this.ProjectId, this.UserId);
            }
            else
            {
                this.TicketResource.updateTicket(TaskName, EstimationTime, ProjectId, UserId, Id);
            }

        }

        public override void delete()
        {
            //this.ProjectResource.DeleteProject
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
            if (this.User != null)
            {
                return this.User;
            }
            
            if (this.UserId != 0)
            {
                this.User = this.UserResource.getUser(this.UserId);
            }

            return this.User;
        }

        public override string ToString()
        {
            return this.TaskName;
        }
    }
}
