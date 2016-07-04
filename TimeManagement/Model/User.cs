using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TimeManagement.Resource;

namespace TimeManagement.Model
{
    public class User : Model
    {
        private long Id;
        public string Username { set; get; }
        public string Email { set; get; }
        private bool isNew = false;
        private ObservableCollection<UserProj> UserProjects = new ObservableCollection<UserProj>();

        private UserProjResource UserProjResource = new UserProjResource();
        private UserResource UserResource = new UserResource();



        public void setUsername(string name)
        {
            this.Username = name;
        }

        public string getUsername()
        {
            return this.Username;
        }

        public void setEmail(string email)
        {
            this.Email = email;
        }

        public string getEmail()
        {
            return this.Email;
        }

        public void setId(long Id)
        {
            this.Id = Id;
        }

        public long getId()
        {
            return this.Id;
        }

        public void setUserProjects(ObservableCollection<UserProj> up)
        {
            this.UserProjects = up;
        }

        public void addProject(Project project)
        {
            bool found = false;
            foreach (UserProj p in this.UserProjects)
            {
                if (p.getProject().getId() == project.getId())
                {
                    p.unsetDelete();
                    found = true;
                }
            }

            if (!found)
            {
                this.UserProjects.Add(new UserProj(this, project));
            }
        }

        public void removeProject(Project project)
        {
            bool found = false;

            foreach (UserProj p in this.UserProjects)
            {
                if (p.getProject().getId() == project.getId())
                {
                    p.setDelete();
                    found = true;
                }
            }

            if (!found)
            {
                this.UserProjects.Add(new UserProj(this, project, true));
            }
        }

        public ObservableCollection<UserProj> getUserProjects()
        {
            return this.UserProjects;
        }

        public override void save()
        {
            if (this.Id == 0)
            {
                this.UserResource.createUser(this);
            } else {
                this.UserResource.updateUser(this);
            }

            foreach (UserProj up in this.UserProjects)
            {
                this.UserProjResource.processUserProj(up);
            }
        }

        public override string ToString()
        {
            return this.Username;
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }

            try
            {
                User user = (User)obj;
                if (user.getId() == this.getId())
                {
                    return true;
                }
            } catch(Exception e)
            {
                return false;
            }

            return false;
        }

        public override void delete()
        {
            this.UserResource.deleteUser(this);
            isNew = true;
        }
    }
}
