﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagement
{
    public class UserProj
    {
        private User User;
        private Project Project;
        private long UserId;
        private long ProjectId;
        private bool Delete = false; 

        public UserProj(User User, Project Project)
        {
            this.setUser(User);
            this.setProject(Project);
        }

        public UserProj(User User, Project Project, bool delete)
        {
            this.setUser(User);
            this.setProject(Project);
            
            this.Delete = delete;
        }

        public UserProj(long UserId, long ProjectId)
        {
            this.UserId = UserId;
            this.ProjectId = ProjectId;
        }

        public UserProj(long UserId, long ProjectId, bool delete)
        {
            this.UserId = UserId;
            this.ProjectId = ProjectId;
            this.Delete = delete;
        }

        public void setProject(Project project) {
            this.Project = project;
            this.ProjectId = project.getId();
        }

        public Project getProject()
        {
            if (this.Project == null)
            {
                this.Project = DBHelper.getInstance().getProject(this.ProjectId);
            }

            return this.Project;
        }

        public User getUser()
        {
            if (this.User == null)
            {
                this.User = DBHelper.getInstance().getUser(this.UserId);
            }
            return this.User;
        }

        public void setUser(User user)
        {
            this.User = user;
            this.UserId = user.getId();
        }

        public bool isDelete()
        {
            return this.Delete;
        }

        public void setDelete()
        {
            this.Delete = true;
        }

        public void unsetDelete()
        {
            this.Delete = false;
        }
    }
}
